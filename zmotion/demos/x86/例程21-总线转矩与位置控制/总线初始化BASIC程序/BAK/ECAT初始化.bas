GLOBAL d_mode							'自定义总线回零调试用
d_mode=35
'*******************************************************ECAT总线初始化
global CONST BUS_TYPE = 0				 '总线类型。可用于上位机区分当前总线类型
global CONST Bus_Slot	= 0				'槽位号0（单总线控制器缺省0）
global CONST PUL_AxisStart	 = 0		'本地脉冲轴起始轴号
global CONST PUL_AxisNum	 = 0		'本地脉冲轴轴数量
global CONST Bus_AxisStart	 = 0	    '总线轴起始轴号
global CONST Bus_NodeNum	 = 1	    '总线配置节点数量,用于判断实际检测到的从站数量是否一致
global CONST Bus_ScanNum	 = 5	    '重扫次数


global  MAX_AXISNUM 			 '最大轴数
MAX_AXISNUM = SYS_ZFEATURE(0)
global Bus_InitStatus			'总线初始化完成状态
Bus_InitStatus = -1
global  Bus_TotalAxisnum		'检查扫描的总轴数



delay(3*1000)				'延时3S等待驱动器上电，不同驱动器自身上电时间不同，具体根据驱动器调整延时
dim tmp_add
tmp_add=0
?"总线通讯周期：",SERVO_PERIOD,"us"
Ecat_Init()			'初始化ECAT总线 
while (Bus_InitStatus = 0)
	Ecat_Init()			'初始化ECAT总线 
	tmp_add=tmp_add+1
	if tmp_add >Bus_ScanNum THEN
		?"初始化失败！"
		EXIT while
	ENDIF
wend



end 


'***************************************************ECAT总线初始****************************************
'初始流程:  slot_scan（扫描总线） ->   从站节点映射轴/io  ->  SLOT_START（启动总线） -> 初始化成功
'******************************************************************************************************
global sub Ecat_Init()

	local Node_Num,Temp_Axis,Drive_Vender,Drive_Device,Drive_Alias,i,io_add

	io_add=0

	RAPIDSTOP(2)
	WAIT IDLE

	for i=0 to MAX_AXISNUM - 1								'初始化还原轴类型					
		AXIS_ENABLE(i) = 0
		atype(i)=0	
		AXIS_ADDRESS(i) =0
		DELAY(10)											'防止所有驱动器全部同时切换使能导致瞬间电流过大
	next

	Bus_InitStatus = -1
	Bus_TotalAxisnum = 0	
	SLOT_STOP(Bus_Slot)				
	delay(200)
	SYSTEM_ZSET=129
	'zml_info(19,$41b,$1918)=0.4*SERVO_PERIOD*1000
	
	slot_scan(Bus_Slot)											'扫描总线

	if return then 

		?"总线扫描成功","连接从站设备数："NODE_COUNT(Bus_Slot)
		
'		if NODE_COUNT(Bus_Slot) <> Bus_NodeNum then		'判断总线检测数量是否为实际接线数量
'			?"扫描节点数量与程序配置数量不一致!" ,"配置数量:"Bus_NodeNum,"检测数量："NODE_COUNT(Bus_Slot)
'			Bus_InitStatus = 0		'初始化失败。报警提示
'			return
'		endif 	
		
		
		'"开始映射轴号"
		for Node_Num=0 to NODE_COUNT(Bus_Slot)-1						'遍历扫描到的所有从站节点
			Drive_Vender = NODE_INFO(Bus_Slot,Node_Num,0)				'读取驱动器厂商
			Drive_Device = NODE_INFO(Bus_Slot,Node_Num,1)				'读取设备编号
			Drive_Alias = NODE_INFO(Bus_Slot,Node_Num,3)					'读取设备拨码ID

			if NODE_AXIS_COUNT(Bus_Slot,Node_Num) <> 0  then				'判断当前节点是否有电机
				for j=0 to NODE_AXIS_COUNT(Bus_Slot,Node_Num)-1			'根据节点带的电机数量循环配置轴参数(针对一拖多驱动器)
							
					Temp_Axis = Bus_AxisStart + Bus_TotalAxisnum		'轴号按NODE顺序分配
					'Temp_Axis = Drive_Alias							'轴号按驱动器设定的拨码分配（一拖多需要特殊处理）					
					base(Temp_Axis)
					AXIS_ADDRESS(Temp_Axis)= (Bus_Slot<<16)+ Bus_TotalAxisnum + 1	'映射轴号
					ATYPE=65		'设置控制模式 65-位置 66-速度 67-转矩 	
					Sub_SetPdo(Node_Num,Drive_Vender,Drive_Device,Temp_Axis)		'设定PDO参数			
					Sub_SetNodePara(Node_Num,Drive_Vender,Drive_Device,j)			'设置特殊总线参数
					disable_group(Temp_Axis)											'每轴单独分组，若多轴分组，需考虑wdog，与AXIS_ENABLE的关系
					Bus_TotalAxisnum=Bus_TotalAxisnum+1									'总轴数+1
				next

				
			else														'IO扩展模块
				
				Sub_SetNodeIo(Node_Num,Drive_Vender,Drive_Device,64 + 16*io_add)		'映射扩展模块IO	
				io_add=io_add+1
			endif
		next
		?"轴号映射完成","连接总轴数："Bus_TotalAxisnum
		?"IO模块数："io_add
		
		SLOT_START(Bus_Slot)				'启动总线
		if return then 
			wa 100
			wdog=1							'使能总开关
		
			'?"开始清除驱动器错误"
			for i= Bus_AxisStart to Bus_AxisStart + Bus_TotalAxisnum - 1 
				BASE(i)
				
				DRIVE_CLEAR(0)
				DELAY 50
				'?"驱动器错误清除完成"
				datum(0)						'清除控制器轴状态错误"
				wa 100	
				
				'"轴使能"
				AXIS_ENABLE=1
			next
			Bus_InitStatus  = 1
			?"轴使能完成"
			
			'本地脉冲轴配置
			for i = 0 to PUL_AxisNum - 1
				base(PUL_AxisStart + i)
				AXIS_ADDRESS  = (-1<<16) +  i
				ATYPE = 4
			next
			?"总线开启成功"			
		else
			?"总线开启失败"
			Bus_InitStatus = 0
		endif	
	else
		?"总线扫描失败"
		Bus_InitStatus = 0
	endif

end sub

'***************************************************从站节点特殊参数配置********************************
'通过SDO方式修改对应对象字典的值修改从站参数(具体对象字典查看驱动器手册)
'******************************************************************************************************
global sub Sub_SetNodePara(iNode,iVender,iDevice,Iaxis)
	if	iVender = $41B and iDevice = $1ab0	 then		'正运动24088脉冲扩展轴
		SDO_WRITE(Bus_Slot,iNode,$6011+Iaxis*$800,0,5,3)			'设置扩展脉冲轴ATYPE类型
		SDO_WRITE(Bus_Slot,iNode,$6012+Iaxis*$800,0,6,0)			'设置扩展脉冲轴INVERT_STEP脉冲输出模式
		NODE_IO(Bus_Slot,iNode) = 32 + 32*iNode					'设置240808上IO的起始映射地址
	elseif iVender=$41b and iDevice = $1918 THEN

		SDO_WRITE(Bus_Slot,iNode,$6011+Iaxis*$800,0,5,5)			'设置扩展脉冲轴ATYPE类型
		SDO_WRITE(Bus_Slot,iNode,$6012+Iaxis*$800,0,6,0)			'设置扩展脉冲轴INVERT_STEP脉冲输出模式
		NODE_IO(Bus_Slot,iNode) = 32 + 32*iNode					'设置240808上IO的起始映射地址
		
	elseif iVender = $66f then							'松下驱动器
		SDO_WRITE(Bus_Slot,iNode,$3741,0,3,0)						'以拨码为ID
		SDO_WRITE(Bus_Slot,iNode,$3401,0,4,$10101)				'正限位电平 $818181
		SDO_WRITE(Bus_Slot,iNode,$3402,0,4,$20202)				'负限位电平 $828282
		
	
		SDO_WRITE(Bus_Slot,iNode,$6091,1,7,1)						'齿轮比
		SDO_WRITE(Bus_Slot,iNode,$6091,2,7,1)	
		SDO_WRITE(Bus_Slot,iNode,$6092,1,7,10000)					'电机一圈脉冲数					'
		
		SDO_WRITE(Bus_Slot,iNode,$607E,0,5,224)					'电机方向0  反转224 	
		'SDO_WRITE(Bus_Slot,iNode,$6085,0,7,4290000000)			'异常减速度
		'SDO_WRITE(Bus_Slot,iNode,$1010,1,7,$65766173)				'写EPPROM(写EPPROM后驱动器需要重新上电)
		
		'?"写EPPR0M OK 请断电重启"		
	elseif iVender = $100000 then							'汇川驱动器
		SDO_WRITE(Bus_Slot,iNode,$6091,1,7,2^23)						'齿轮比
		SDO_WRITE(Bus_Slot,iNode,$6091,2,7,10000)	
	elseif iVender =$539 THEN							'安川驱动器

'		SDO_WRITE(Bus_Slot,iNode,$2701,1,7,2^20)		'脉冲齿轮比，分子				
'		SDO_WRITE(Bus_Slot,iNode,$2701,2,7,100000)		'脉冲齿轮比，分母
'		
'		SDO_WRITE(Bus_Slot,iNode,$2702,1,7,2^20)		'速度齿轮比，分子				
'		SDO_WRITE(Bus_Slot,iNode,$2702,2,7,100000)		'速度齿轮比，分母
'		
'		SDO_WRITE(Bus_Slot,iNode,$2703,1,7,2^20)		'加速度齿轮比，分子				
'		SDO_WRITE(Bus_Slot,iNode,$2703,2,7,100000)		'加速度齿轮比，分母
'		
'		SDO_WRITE(Bus_Slot,iNode,$1010,3,7,$65766173)		''在子索引3写入“save”即$65766173时?保存（27xx,60xx）参数。	
	ELSE

	endif
end sub

'***************************************************总线驱动IO映射**************************************
'通过DRIVE_IO指令映射驱动器对象字典中60FD,60FE输入输出状态，要设置正确的DRIVE_PROFILEE或者POD后才可以正常映射
'DRIVE_PROFILE模式包含60FD/60FE
'iAxis - 轴号  iVender - 驱动器类型  i_IoNum - 输入输出起始编号
'******************************************************************************************************
global sub Sub_SetDriverIo(iVender,Iaxis,i_IoNum)
	if	iVender = $66f then		'松下驱动器
		DRIVE_IO(iAxis) = i_IoNum	
		
		REV_IN(iAxis) = -1				
		FWD_IN(iAxis) = -1			
		DATUM_IN(iAxis) = -1			
		
		INVERT_IN(i_IoNum,off)					
		INVERT_IN(i_IoNum + 1,off)
		INVERT_IN(i_IoNum + 2,off)
	ELSE
		
		DRIVE_IO(iAxis) = i_IoNum
		
				REV_IN(iAxis) = -1				
		FWD_IN(iAxis) = -1			
		DATUM_IN(iAxis) = -1	
'		
'		REV_IN(iAxis) = i_IoNum 			
'		FWD_IN(iAxis) = i_IoNum+1		
'		DATUM_IN(iAxis) = i_IoNum+2			
		
		INVERT_IN(i_IoNum,on)					
		INVERT_IN(i_IoNum + 1,on)
		INVERT_IN(i_IoNum + 2,on)			
	endif

end sub

'***************************************************总线IO模块映射**************************************
'通过NODE_IO(Bus_Slot,Node_Num)分配模块IO起始地址
'******************************************************************************************************
global sub Sub_SetNodeIo(iNode,iVender,iDevice,i_IoNum)
	if	iVender = $41B and iDevice = $130	 then		'正运动EIO1616
		NODE_IO(Bus_Slot,iNode) = i_IoNum
	
	ELSEIF iVender=276 THEN	'SMC阀岛
		
		
'		SDO_WRITE (Bus_Slot, iNode, $1c12, 0 ,5 ,0)					'禁用RxPDO,禁用后才可以修改内容
'		wa(50)
'		
'		SDO_WRITE (Bus_Slot, iNode, $1602, $0 ,5 ,0)					' 禁用
'		SDO_WRITE (Bus_Slot, iNode, $1602, $1 ,7 ,$31010120)			'控制字
'		SDO_WRITE (Bus_Slot, iNode, $1602, $0 ,5 ,1)
'		SDO_WRITE (Bus_Slot, iNode, $1c12, 1 ,6 ,$1602)				'pdo分配对象	
'		wa(50)
'		SDO_WRITE (Bus_Slot, iNode, $1c12, 0 ,5 ,1)					'启用PDO
'		WA(50)
'		
'		DRIVE_PROFILE = -1												'使用驱动缺省PDO配置
		NODE_IO(Bus_Slot,iNode) = i_IoNum
	ELSE
		?"iVender:"iVender
		NODE_IO(Bus_Slot,iNode) = i_IoNum
		
		if NODE_INFO (Bus_Slot, iNode, 12)>0 THEN
		
			NODE_AIO(Bus_Slot,iNode,3)=8
		ENDIF
		
		if NODE_INFO (Bus_Slot, iNode, 13)>0 THEN
		
			NODE_AIO(Bus_Slot,iNode,4)=8
		ENDIF
		
	endif

end sub

'***************************************************手动配置PDO****************************************
'部分特殊品牌可能需要手动配置，大部分只需要通过DRIVE_PROFILE设置自动配置相应的POD参数即可
'******************************************************************************************************
global sub Sub_SetPdo(iNode,iVender,iDevice,Iaxis)
	IF  iVender = 0 then									'自定义PDO											
		SDO_WRITE (Bus_Slot, iNode, $1c12, 0 ,5 ,0)					'禁用PDO,禁用后才可以修改内容
		wa(50)
		SDO_WRITE (Bus_Slot, iNode, $1c13, 0 ,5 ,0)
		wa(50)
		
		SDO_WRITE (Bus_Slot, iNode, $1600, $0 ,5 ,0)					'RxPDO 配置对应参数
		SDO_WRITE (Bus_Slot, iNode, $1600, $1 ,7 ,$60400010)			'控制字
		SDO_WRITE (Bus_Slot, iNode, $1600, $2 ,7 ,$607a0020)			'目标位置
		SDO_WRITE (Bus_Slot, iNode, $1600, $3 ,7 ,$60fe0120)			'驱动器IO输入
		SDO_WRITE (Bus_Slot, iNode, $1600, $0 ,5 ,3)
		
		SDO_WRITE (Bus_Slot, iNode, $1a00, $0 ,5 ,0)					'TxPDO 
		SDO_WRITE (Bus_Slot, iNode, $1a00, $1 ,7 ,$60410010)			'状态字
		SDO_WRITE (Bus_Slot, iNode, $1a00, $2 ,7 ,$60640020)			'反馈位置
		SDO_WRITE (Bus_Slot, iNode, $1a00, $3 ,7 ,$60fd0020)			'驱动器IO输出
		SDO_WRITE (Bus_Slot, iNode, $1a00, $0 ,5 ,3)		

		SDO_WRITE (Bus_Slot, iNode, $1c12, 1 ,6 ,$1600)				'pdo分配对象			
		wa(50)
		SDO_WRITE (Bus_Slot, iNode, $1c13, 1 ,6 ,$1a00)				'
		wa(50)
		SDO_WRITE (Bus_Slot, iNode, $1c12, 0 ,5 ,1)					'启用PDO
		wa(50)
		SDO_WRITE (Bus_Slot, iNode, $1c13, 0 ,5 ,1)

		SDO_WRITE (Bus_Slot, iNode, $1C32, $1 ,6 ,2)					'设置DC同步模式
		SDO_WRITE (Bus_Slot, iNode, $1C33, $1 ,6 ,2)
		
		DRIVE_PROFILE = -1												'使用驱动缺省PDO配置		
		Sub_SetDriverIo(iVender,Iaxis,(128*4+64) + 32*Iaxis)		'映射驱动器IO  IO映射到控制器IO32-以后每个驱动器间隔32点	

	elseif iVender = 154 then
		?"154"
		'===========================1600=======================================
		SDO_WRITE (Bus_Slot, iNode, $1c12, 0 ,5 ,0)					'禁用PDO,禁用后才可以修改内容
		wa(50)
		SDO_WRITE (Bus_Slot, iNode, $1607, $0 ,5 ,0)					' 禁用
		SDO_WRITE (Bus_Slot, iNode, $1607, $1 ,7 ,$60400010)			'控制字
		SDO_WRITE (Bus_Slot, iNode, $1607, $2 ,7 ,$607a0020)			'目标位置
		SDO_WRITE (Bus_Slot, iNode, $1607, $3 ,7 ,$60b10020)			'
		SDO_WRITE (Bus_Slot, iNode, $1607, $4 ,7 ,$60b20010)			'
		SDO_WRITE (Bus_Slot, iNode, $1607, $5 ,7 ,$60ff0020)			'
		SDO_WRITE (Bus_Slot, iNode, $1607, $6 ,7 ,$60710010)			'
		SDO_WRITE (Bus_Slot, iNode, $1607, $7 ,7 ,$60600008)			'
		SDO_WRITE (Bus_Slot, iNode, $1607, $8 ,7 ,$8)					'
		SDO_WRITE (Bus_Slot, iNode, $1607, $0 ,5 ,8)					'启用分配
		
		SDO_WRITE (Bus_Slot, iNode, $1c13, 0 ,5 ,0)
		wa(50)
		SDO_WRITE (Bus_Slot, iNode, $1a07, $0 ,5 ,0)					' 禁用
		SDO_WRITE (Bus_Slot, iNode, $1a07, $1 ,7 ,$60410010)			' 状态字
		SDO_WRITE (Bus_Slot, iNode, $1a07, $2 ,7 ,$60640020)			' 反馈位置
		SDO_WRITE (Bus_Slot, iNode, $1a07, $3 ,7 ,$606c0020)			' 反馈速度
		SDO_WRITE (Bus_Slot, iNode, $1a07, $4 ,7 ,$60770010)			' 当前力矩
		SDO_WRITE (Bus_Slot, iNode, $1a07, $5 ,7 ,$60610008)			' 反馈速度
		SDO_WRITE (Bus_Slot, iNode, $1a07, $6 ,7 ,$8)			' 驱动器IO输入
		SDO_WRITE (Bus_Slot, iNode, $1a07, $0 ,5 ,6)					'启用分配
		
		SDO_WRITE (Bus_Slot, iNode, $1c12, 1 ,6 ,$1607)				'pdo分配对象			
		wa(50)
		SDO_WRITE (Bus_Slot, iNode, $1c13, 1 ,6 ,$1a07)				'
		wa(50)
		SDO_WRITE (Bus_Slot, iNode, $1c12, 0 ,5 ,1)					'启用PDO
		wa(50)
		SDO_WRITE (Bus_Slot, iNode, $1c13, 0 ,5 ,1)
				
		DRIVE_PROFILE = -1												'使用驱动缺省PDO配置	
	elseif iVender = 4292872 then
		?"ISMC"
		DRIVE_PROFILE = -1												'使用驱动缺省PDO配置
		
		SDO_WRITE (Bus_Slot, iNode, $1c12, 0 ,5 ,0)					'禁用RxPDO,禁用后才可以修改内容
		wa(50)
		SDO_WRITE (Bus_Slot, iNode, $1c13, 0 ,5 ,0)					'禁用TxPDO,禁用后才可以修改内容
		wa(50)
		'===========================1600=======================================
		SDO_WRITE (Bus_Slot, iNode, $1600, $0 ,5 ,0)					' 禁用
		SDO_WRITE (Bus_Slot, iNode, $1600, $1 ,7 ,$60400010)			'控制字
		SDO_WRITE (Bus_Slot, iNode, $1600, $2 ,7 ,$607a0020)			'目标位置
		SDO_WRITE (Bus_Slot, iNode, $1600, $3 ,7 ,$60b10020)			'
		SDO_WRITE (Bus_Slot, iNode, $1600, $4 ,7 ,$60b20010)			'
		SDO_WRITE (Bus_Slot, iNode, $1600, $5 ,7 ,$60ff0020)			'
		SDO_WRITE (Bus_Slot, iNode, $1600, $6 ,7 ,$60710010)			'
		SDO_WRITE (Bus_Slot, iNode, $1600, $7 ,7 ,$60600008)			'
		SDO_WRITE (Bus_Slot, iNode, $1600, $8 ,7 ,$8)					'
		SDO_WRITE (Bus_Slot, iNode, $1600, $0 ,5 ,8)					'启用分配
		'===========================1601=======================================
		SDO_WRITE (Bus_Slot, iNode, $1601, $0 ,5 ,0)					' 禁用
		SDO_WRITE (Bus_Slot, iNode, $1601, $1 ,7 ,$0)				'控制字
		SDO_WRITE (Bus_Slot, iNode, $1601, $2 ,7 ,$0)				'目标位置
		SDO_WRITE (Bus_Slot, iNode, $1601, $3 ,7 ,$0)				'驱动器IO输入
		SDO_WRITE (Bus_Slot, iNode, $1601, $4 ,7 ,$0)				'驱动器IO输入
		SDO_WRITE (Bus_Slot, iNode, $1601, $0 ,5 ,12)					'启用分配
		'===========================1a00=======================================
		SDO_WRITE (Bus_Slot, iNode, $1a00, $0 ,5 ,0)					' 禁用
		SDO_WRITE (Bus_Slot, iNode, $1a00, $1 ,7 ,$60410010)			' 状态字
		SDO_WRITE (Bus_Slot, iNode, $1a00, $2 ,7 ,$60640020)			' 反馈位置
		SDO_WRITE (Bus_Slot, iNode, $1a00, $3 ,7 ,$606c0020)			' 反馈速度
		SDO_WRITE (Bus_Slot, iNode, $1a00, $4 ,7 ,$60770010)			' 驱动器IO输入
		SDO_WRITE (Bus_Slot, iNode, $1a00, $5 ,7 ,$60610008)			' 反馈速度
		SDO_WRITE (Bus_Slot, iNode, $1a00, $6 ,7 ,$8)			' 驱动器IO输入
		SDO_WRITE (Bus_Slot, iNode, $1a00, $0 ,5 ,6)					'启用分配
		'===========================1a01=======================================
		SDO_WRITE (Bus_Slot, iNode, $1a01, $0 ,5 ,0)					' 禁用
		SDO_WRITE (Bus_Slot, iNode, $1a01, $1 ,7 ,$60780010)			' 状态字
		SDO_WRITE (Bus_Slot, iNode, $1a01, $2 ,7 ,$0)			' 反馈位置
		SDO_WRITE (Bus_Slot, iNode, $1a01, $3 ,7 ,$0)			' 反馈速度
		SDO_WRITE (Bus_Slot, iNode, $1a01, $4 ,7 ,$0)			' 驱动器IO输入
		SDO_WRITE (Bus_Slot, iNode, $1a01, $0 ,5 ,4)			'启用分配
		
		SDO_WRITE (Bus_Slot, iNode, $1c12, $1 ,6 ,$1600) ' RxPDO分配对象
		SDO_WRITE (Bus_Slot, iNode, $1c12, $2 ,6 ,$1601) ' RxPDO分配对象
		SDO_WRITE (Bus_Slot, iNode, $1c12, $0 ,5 ,$2) ' 启用分配
		
		SDO_WRITE (Bus_Slot, iNode, $1c13, $1 ,6 ,$1a00) ' TxPDO分配对象
		SDO_WRITE (Bus_Slot, iNode, $1c13, $2 ,6 ,$1a01) ' TxPDO分配对象
		SDO_WRITE (Bus_Slot, iNode, $1c13, $0 ,5 ,$2) ' 启用分配
		
'		SDO_WRITE (Bus_Slot, iNode, $6040, $0 ,6 ,$0) ' 状态初始化
'		WA(50)
'		SDO_WRITE (Bus_Slot, iNode, $6040, $0 ,6 ,$6) ' 伺服shutdown 
'		WA(50)
'		SDO_WRITE (Bus_Slot, iNode, $6040, $0 ,6 ,$7) ' 伺服disable voltage
'		WA(50)
'		SDO_WRITE (Bus_Slot, iNode, $6040, $0 ,6 ,$F) ' 伺服fault reset
	elseif iVender=$41b  THEN	'EIO不支持配置为-1
		DRIVE_PROFILE = 0						'PDO带驱动器输入输出对象字典
	else
		'?"iVender:"iVender
		DRIVE_PROFILE =0					'PDO带驱动器输入输出对象字典
		'SDO_WRITE (Bus_Slot, iNode, $60fe, 2 ,7 ,$00000)
		Sub_SetDriverIo(iVender,Iaxis,128 + 32*Iaxis)		'映射驱动器IO  IO映射到控制器IO32-以后每个驱动器间隔32点	
	endif
end sub

'***************************************************总线驱动器回零**************************************
'驱动器回零
'******************************************************************************************************
global sub Sub_SetDriverHome(iAxis,Imode)
	
	TRIGGER
	local home_sp1,home_sp2,home_mode,home_offset,home_acc
	home_sp1 = 1000
	home_sp2 = 100
	home_acc = 10000
	home_mode = Imode
	home_offset  = 0
	
	base(iAxis)
	AXIS_STOPREASON = 0
	speed = home_sp1
	CREEP = home_sp2
	ACCEL  =home_acc
	DATUM(21,Imode)
	wait IDLE
	if AXIS_STOPREASON = 0 then
		?"回零成功"
	else
		?"回零失败"	,"停止原因：",AXIS_STOPREASON,"状态字0X",HEX(DRIVE_STATUS)
	endif

end sub

'***************************************功能：循环打印检测
global sub sub_task_print()						'打印检查任务
	local istatus,i_axis
	i_axis = 0 
	istatus = DRIVE_STATUS(i_axis)
	while 1
		if istatus<>DRIVE_STATUS(i_axis) then
			?"上次状态",hEX(istatus),"当前",HEX(DRIVE_STATUS(i_axis)),HEX(DRIVE_CONTROLWORD(i_axis))
			istatus = DRIVE_STATUS(i_axis)
		endif

	wend

end sub

'自定义回零验证
global sub sub_task()	
	'AXIS_ENABLE(0)=0
	DELAY(10)
	'SDO_WRITE(0,0,$607c,0,4,0)
	'DELAY(10)
	'AXIS_ENABLE(0)=1
	SDO_WRITE(0,0,$6098,0,2,d_mode)
	SDO_WRITE(0,0,$6099,1,7,6000*UNITS(0))
	SDO_WRITE(0,0,$6099,2,7,500*UNITS(0))
	SDO_WRITE(0,0,$609a,0,7,10000*UNITS(0))
	DELAY(10)
	DRIVE_MODE(0)=6
	'DRIVE_CW_MODE(0)=1

	
	DRIVE_CONTROLWORD(0)=6     '伺服shutdown 
	DELAY(50)
	DRIVE_CONTROLWORD(0)=7     '伺服shutdown 
	DELAY(50)
	DRIVE_CONTROLWORD(0)=15    '伺服switch on
	DELAY(50)
	DRIVE_CONTROLWORD(0)=$1f     '伺服shutdown 
	DELAY(50)
	?DRIVE_STATUS(0)
	AXIS_ENABLE(0)=0
	DELAY(1000)
	
	DRIVE_MODE(0)=8
	AXIS_ENABLE(0)=1
	
	'DRIVE_CW_MODE(0)=0

	'SDO_WRITE(0,0,$607A,0,7,0)
ENDSUB


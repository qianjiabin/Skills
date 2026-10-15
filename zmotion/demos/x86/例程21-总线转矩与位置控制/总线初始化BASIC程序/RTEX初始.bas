
'*******************************************************RTEX总线初始化
'''''''''
global CONST BUS_TYPE = 1				 '总线类型。用于上位机区分当前模式
global CONST MAX_AXISNUM = 32			 '最大轴数
global CONST Bus_Slot	= 0				'槽位号0
global CONST Bus_AxisStart	 = 0		'总线轴起始轴号

global Bus_InitStatus			'总线初始化完成状态
Bus_InitStatus = -1
global  Bus_TotalAxisnum		'检查扫描的总轴数

delay(3000)				'延时3S等待驱动器上电
'**********************初始化RTEX总线 
Rtex_Init()

end
	
global  sub Rtex_Init()
	local temp_axis
	for i=0 to MAX_AXISNUM - 1		'初始化还原轴类型					
		AXIS_ENABLE(i) = 0
		atype(i)=0	
	next
		
	Bus_InitStatus = -1
	Bus_TotalAxisnum = 0	
	SLOT_STOP(Bus_Slot)
	delay(200)
	slot_scan(Bus_Slot)											'开始扫描
	if return then 
		
		?"总线扫描成功","连接设备数："NODE_COUNT(Bus_Slot)
		?
		?"开始映射轴号"
		for i=0 to NODE_COUNT(Bus_Slot)-1						'遍历总线下所有从站节点

			if NODE_AXIS_COUNT(Bus_Slot,i) <>0 then			'判断当前节点是否有电机
				for j=0 to NODE_AXIS_COUNT(Bus_Slot,i)-1
					temp_axis = Bus_AxisStart +Bus_TotalAxisnum
					base(temp_axis)
					AXIS_ADDRESS=Bus_TotalAxisnum+1			'映射轴号
					ATYPE=50									'设置控制模式 65-位置 66-速度 67-转矩 
					DRIVE_PROFILE= 4							'设置PROFILE功能
					disable_group(temp_axis)					'每轴单独分组

					DRIVE_IO = 128 + (temp_axis)*16		'映射驱动器上的IO状态
					REV_IN = 128 + (temp_axis)*16 
					FWD_IN = 129 + (temp_axis)*16
					DATUM_IN = 130 + (temp_axis)*16
					INVERT_IN(128 + (temp_axis)*16,ON)
					INVERT_IN(129 + (temp_axis)*16,ON)
					INVERT_IN(130 + (temp_axis)*16,ON)
					
					Bus_TotalAxisnum=Bus_TotalAxisnum+1							'总轴数+1
				next
			endif
			
		next
		
		?"轴号映射完成","连接总轴数："Bus_TotalAxisnum
		wa 2000
		
		SLOT_START(Bus_Slot)						'启动总线
		if return then 
			?"总线开启成功"
			
			?"开始清除驱动器错误(根据驱动器数据字典设置)"
			for i= Bus_AxisStart to Bus_AxisStart + Bus_TotalAxisnum - 1 
				
				DRIVE_CONTROLWORD(i)=128					'根据驱动器数据字典
				wa 100
				DRIVE_CONTROLWORD(i)=6
				wa 100
				DRIVE_CONTROLWORD(i)=15
				wa 100
			next
			
			?"驱动器错误清除完成"
			wa 100

			?"清除控制器错误"
			datum(0)
			DRIVE_CLEAR(0)
			?"控制器错误清除完成"
			wa 100
						
			?"轴使能准备"
			for i= Bus_AxisStart to Bus_AxisStart + Bus_TotalAxisnum - 1
				base(i)
				AXIS_ENABLE=1
			next
			wdog=1											'使能总开关
			Bus_InitStatus  = 1
			?"轴使能完成"
		else
			?"总线开启失败"
			Bus_InitStatus = 0
		endif
		
	else
		?"总线扫描失败"
		Bus_InitStatus = 0
	endif
end sub
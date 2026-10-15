?"程序运行"
	'轴参数初始化
 base(0,1,2)
 atype = 1,1,1
 units = 10,10,10
 speed = 100,100,100
 accel = 1000,1000,1000
 decel = 1000,1000,1000
 
 global cmd_data 			'全局变量 上位机可访问
 cmd_data = 0
 
 global array_para(20) '全局数组 上位机可访问

 dim curcmd 
 curcmd = 0
 
 '主程序，通过上位机传递的MODBUS值触发运动
 while 1
	if cmd_data <> 0 then
		curcmd = cmd_data
		cmd_data = 0
		
		if curcmd = 1 then			'执行动作1
			base(0,1,2)
			moveabs(array_para(0),array_para(1),array_para(2))
		elseif curcmd = 2 then			'执行动作2
			base(0,1,2)
			moveabs(array_para(5),array_para(6),array_para(7))
		elseif curcmd = 3 then			'执行动作3
			base(0,1,2)
			cancel(2)
		elseif curcmd = 4 then			'执行动作4
			base(0,1,2)
			cancel(2)
			wait IDLE
			
			dpos(0) = 0
			dpos(1) = 0
			dpos(2) = 0
		endif
	endif 
	
 wend
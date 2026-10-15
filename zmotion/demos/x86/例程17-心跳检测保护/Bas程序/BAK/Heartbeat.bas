Global Heart_Flag 			'通讯标识变量
Heart_Flag = 1

Global TimeOut 				'超时时间
TimeOut = 1000

Global Out_Num					'急停OP数量
Out_Num = 10
Global Out_List(100)			'急停OUT

DIM i

WAIT until Heart_Flag = 0		'等待PC软件连接



WHILE TRUE
	
	if Heart_Flag = 0 then Heart_Flag = 1				'等待PC上修改通讯变量

	ticks = TimeOut										'超时判断处理，等待PC上修改Heart_Flag = 0
	WAIT until ticks < 0
		
	if Heart_Flag = 1 THEN
		RAPIDSTOP(3)
		if Out_Num >=100 Then Out_Num = 99
		for i = 0 to  Out_Num
			op(Out_List(i),off)	
		next
		
	Endif


WEND

end
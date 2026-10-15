End 'End之后的函数只有被主动调用才能运行, 这里加End防止误调用.

GLOBAL Sub MyGlobalBasicSub()
	LOCAL index
	index = 0
	?"当前的电机位置为", DPOS(0)
	WHILE 1
		?"测试循环, 运行次数", index
		index = index + 1
		DELAY(1000)
	WEND
	


End Sub
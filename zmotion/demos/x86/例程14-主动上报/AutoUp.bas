global If_EnableAutoUp						'是否开启上报
If_EnableAutoUp = 0

global AutoUpCode							'上报功能码
AutoUpCode = 1

global AutoCmdString(1024)					'主动上报信息内容

global AutoUpTime							'开启间隔时间
AutoUpTime = 1000


while  1 
	if If_EnableAutoUp = 1 then

		SEND_AUTOUP(-1,AutoUpCode,AutoCmdString)
		DELAY(AutoUpTime)
	endif 
wend

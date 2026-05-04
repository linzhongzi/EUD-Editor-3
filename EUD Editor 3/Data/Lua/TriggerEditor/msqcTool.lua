--[================================[
@Language.ko-KR
@Summary
[Key]를 안전한 문자로 변경합니다.
@Group
키인식
@param.Key.Key


@Summary
将 [Key] 修改为安全的字符。
@Group
按键识别组
@param.Key.Key
要修改的 [key] 值


@Language.en-US
@Summary
Changes [Key] to safe characters.
@Group
Key Recognition
@param.Key.Key
The key.
]================================]
function KeyParse(Key)
	index = -1
	spkeys = {"NUMPAD*", "NUMPAD+", "NUMPAD-", "NUMPAD.", "NUMPAD/", "*", "+", "-", ".", "/", "=", ",", "`", "[", "]", "\\", "'"}
	for k,v in pairs(spkeys) do
		if v == Key then
			index = k
			break
		end
	end

	if index ~= -1 then
		Key = "SP" .. index
	end

	return Key
end



--[================================[
@Language.ko-KR
@Summary
[Player]의 키 [Key]가 눌리는 순간을 감지합니다.
@Group
키인식
@param.Player.TrgPlayer
대상 플레이어입니다.
@param.Key.Key


@Language.zh-CN
@Summary
检测 [Player] 按下 [Key] 键的时刻。
@Group
按键识别
@param.Player.TrgPlayer
目标玩家。
@param.Key.Key
按下的键值


@Language.en-US
@Summary
Detects the moment the key [Key] is pressed by [Player].
@Group
Key Recognition
@param.Player.TrgPlayer
The target player.
@param.Key.Key
The key.
]================================]
function KeyDown(Player, Key)
	Player = ParsePlayer(Player)
	keyarray = "VKeyDown_" .. KeyParse(Key)
	--AddMSQCPlugin("NotTyping ; KeyDown(" .. Key .. ") : " .. keyarray .. ", 1")
	AddMSQCPlugin(Key, keyarray, "KeyDown", "NotTyping")


	echo("MemoryEPD(EPD(msqcvar." .. keyarray .. ") + " .. Player .. ", Exactly, 1)")
end

--[================================[
@Language.ko-KR
@Summary
[Player]의 키 [Key]가 놓는 순간을 감지합니다.
@Group
키인식
@param.Player.TrgPlayer
대상 플레이어입니다.
@param.Key.Key


@Language.zh-CN
@Summary
检测 [Player] 松开 [Key] 键的时刻。
@Group
按键识别
@param.Player.TrgPlayer
目标玩家。
@param.Key.Key
松开的键值


@Language.en-US
@Summary
Detects the moment the key [Key] is released by [Player].
@Group
Key Recognition
@param.Player.TrgPlayer
The target player.
@param.Key.Key
The key.
]================================]
function KeyUp(Player, Key)
	Player = ParsePlayer(Player)
	keyarray = "VKeyUp_" .. KeyParse(Key)
	--AddMSQCPlugin("NotTyping ; KeyDown(" .. Key .. ") : " .. keyarray .. ", 1")
	AddMSQCPlugin(Key, keyarray, "KeyUp", "NotTyping")


	echo("MemoryEPD(EPD(msqcvar." .. keyarray .. ") + " .. Player .. ", Exactly, 1)")
end

--[================================[
@Language.ko-KR
@Summary
[Player]의 키 [Key]를 누르고 있는지 감지합니다.
@Group
키인식
@param.Player.TrgPlayer
대상 플레이어입니다.
@param.Key.Key


@Language.zh-CN
@Summary
检测 [Player] 是否正在按下 [Key] 键。
@Group
按键识别
@param.Player.TrgPlayer
目标玩家。
@param.Key.Key
正按下的 [Key] 值

@Language.en-US
@Summary
Detects if the key [Key] is being held down by [Player].
@Group
Key Recognition
@param.Player.TrgPlayer
The target player.
@param.Key.Key
The key.
]================================]
function KeyPress(Player, Key)
	Player = ParsePlayer(Player)
	keyarray = "VKeyPress_" .. KeyParse(Key)
	--AddMSQCPlugin("NotTyping ; KeyDown(" .. Key .. ") : " .. keyarray .. ", 1")
	AddMSQCPlugin(Key, keyarray, "KeyPress", "NotTyping")

	echo("MemoryEPD(EPD(msqcvar." .. keyarray .. ") + " .. Player .. ", Exactly, 1)")
end

--[================================[
@Language.ko-KR
@Summary
[Player]의 마우스 [Button]가 눌리는 순간을 감지합니다.
@Group
마우스
@param.Player.TrgPlayer
대상 플레이어입니다.
@param.Button.Button


@Language.zh-CN
@Summary
检测 [Player] 的鼠标 [Button] 被按下的时刻。
@Group
鼠标组
@param.Player.TrgPlayer
目标玩家。
@param.Button.Button
鼠标按钮值

@Language.en-US
@Summary
Detects the moment the mouse button [Button] is pressed by [Player].
@Group
Mouse
@param.Player.TrgPlayer
The target player.
@param.Button.Button
The button.
]================================]
function MouseDown(Player, Button)
	Player = ParsePlayer(Player)
	keyarray = "VMouseDown_" .. Button
	--AddMSQCPlugin("NotTyping ; KeyDown(" .. Key .. ") : " .. keyarray .. ", 1")
	AddMSQCPlugin(Button, keyarray, "MouseDown", "")


	echo("MemoryEPD(EPD(msqcvar." .. keyarray .. ") + " .. Player .. ", Exactly, 1)")
end

--[================================[
@Language.ko-KR
@Summary
[Player]의 마우스 [Button]를 놓는 순간을 감지합니다.
@Group
마우스
@param.Player.TrgPlayer
대상 플레이어입니다.
@param.Button.Button


@Language.zh-CN
@Summary
检测 [Player] 的鼠标 [Button] 被松开的时刻。
@Group
鼠标组
@param.Player.TrgPlayer
目标玩家。
@param.Button.Button
鼠标按钮值

@Language.en-US
@Summary
Detects the moment the mouse button [Button] is released by [Player].
@Group
Mouse
@param.Player.TrgPlayer
The target player.
@param.Button.Button
The button.
]================================]
function MouseUp(Player, Button)
	Player = ParsePlayer(Player)
	keyarray = "VMouseUp_" .. Button
	--AddMSQCPlugin("NotTyping ; KeyDown(" .. Key .. ") : " .. keyarray .. ", 1")
	AddMSQCPlugin(Button, keyarray, "MouseUp", "")


	echo("MemoryEPD(EPD(msqcvar." .. keyarray .. ") + " .. Player .. ", Exactly, 1)")
end

--[================================[
@Language.ko-KR
@Summary
[Player]의 마우스 [Button]를 누르고 있는지 감지합니다.
@Group
마우스
@param.Player.TrgPlayer
대상 플레이어입니다.
@param.Button.Button


@Language.zh-CN
@Summary
检测 [Player] 是否正在按住鼠标 [Button].
@Group
鼠标
@param.Player.TrgPlayer
目标玩家。
@param.Button.Button
鼠标按钮值

@Language.en-US
@Summary
Detects if the mouse button [Button] is being held down by [Player].
@Group
Mouse
@param.Player.TrgPlayer
The target player.
@param.Button.Button
The button.
]================================]
function MousePress(Player, Button)
	Player = ParsePlayer(Player)
	keyarray = "VMousePress_" .. Button
	--AddMSQCPlugin("NotTyping ; KeyDown(" .. Key .. ") : " .. keyarray .. ", 1")
	AddMSQCPlugin(Button, keyarray, "MousePress", "")


	echo("MemoryEPD(EPD(msqcvar." .. keyarray .. ") + " .. Player .. ", Exactly, 1)")
end

--[================================[
@Language.ko-KR
@Summary
마우스가 위치한 텍스트의 줄을 반환합니다.
@Group
마우스


@Language.zh-CN
@Summary
返回鼠标所在的文本行。
@Group
鼠标


@Language.en-US
@Summary
Returns the line of text the mouse is positioned over.
@Group
Mouse
]================================]
function LocalMouseLine()
	echo("((dwread_epd(EPD(0x6CDDC8)) - 94) / 16)")
end

--[================================[
@Language.ko-KR
@Summary
[Player]의 마우스가 [Line]번째 줄을 X좌표 [MinX] ~ [MaxX]에서 클릭했을 경우를 인식합니다.
@Group
마우스
@param.Player.TrgPlayer
대상 플레이어입니다.
@param.Line.Number
라인입니다. 1 ~ 11이 유효합니다.
@param.MinX.Number
X좌표 최소 범위입니다.
@param.MaxX.Number
X좌표 최대 범위입니다.


@Language.zh-CN
@Summary
识别 [Player] 的鼠标何时点击位于 X 坐标 [MinX] ~ [MaxX] 的第 [Line] 行。
@Group
鼠标
@param.Player.TrgPlayer
目标玩家。
@param.Line.Number
行号: 1 至 11 有效。
@param.MinX.Number
X 坐标的最小范围。
@param.MaxX.Number
X 坐标的最大范围。


@Language.en-US
@Summary
Recognizes when [Player] 's mouse clicks on line [Line] within X coordinates [MinX] ~ [MaxX].
@Group
Mouse
@param.Player.TrgPlayer
The target player.
@param.Line.Number
The line. 1 to 11 are valid.
@param.MinX.Number
The minimum X coordinate range.
@param.MaxX.Number
The maximum X coordinate range.
]================================]
function MouseClickLine(Player, Line, MinX, MaxX)
	Player = ParsePlayer(Player)
	clickindex = AddMouseEvent(Line, MinX, MaxX)

	echo("MemoryEPD(EPD(msqcvar.VMouseClickIndex" .. clickindex .. ") + " .. Player .. ", Exactly, 1)")
end

--[================================[
@Language.ko-KR
@Summary
[Player]의 채팅[Chat]을 인식합니다.
@Group
채팅인식
@param.Player.TrgPlayer
대상 플레이어입니다.
@param.Chat.TrgString
인식할 채팅입니다.

@Language.zh-CN
@Summary
识别 [Player] 的聊天 [Chat].
@Group
聊天识别
@param.Player.TrgPlayer
目标玩家。
@param.Chat.TrgString
要识别的聊天记录。


@Language.en-US
@Summary
Recognizes the chat [Chat] of [Player].
@Group
Chat Recognition
@param.Player.TrgPlayer
The target player.
@param.Chat.TrgString
The chat to recognize.
]================================]
function ChatEvent(Player ,Chat)
	Player = ParsePlayer(Player)
	chatindex = AddChatEventPlugin(Chat)
	--chatarray = "VChat_" .. chatindex
	chatarray = "VChatIndex"

	
	echo("MemoryEPD(EPD(msqcvar." .. chatarray .. ") + " .. Player .. ", Exactly, " .. chatindex + 1 ..")")
end

--[================================[
@Language.ko-KR
@Summary
[Player]의 채팅[Start] , [Mid] , [End]를 인식합니다.
@Group
채팅인식
@param.Player.TrgPlayer
대상 플레이어입니다.
@param.Start.TrgString
시작 할 앞부분 입니다. 비어있으면 아무 내용이나 가능합니다.
@param.Mid.TrgString
가운대에 포함될 내용입니다.
@param.End.TrgString
끝 부분입니다. 비어있으면 아무 내용이나 가능합니다.

@Language.zh-CN
@Summary
识别 [Player] 的聊天 [Start] , [Mid] , [End] 部分。
@Group
聊天识别
@param.Player.TrgPlayer
目标玩家。
@param.Start.TrgString
聊天记录的开始部分。如果为空，则允许包含任何内容。
@param.Mid.TrgString
聊天记录的中间部分
@param.End.TrgString
聊天记录的结束部分。如果为空，则允许包含任何内容。


@Language.en-US
@Summary
Recognizes the chat [Start] , [Mid] , [End] of [Player].
@Group
Chat Recognition
@param.Player.TrgPlayer
The target player.
@param.Start.TrgString
The starting part. If empty, any content is allowed.
@param.Mid.TrgString
The content to be included in the middle.
@param.End.TrgString
The ending part. If empty, any content is allowed.
]================================]
function ChatEventPattern(Player ,Start ,Mid ,End)
	Player = ParsePlayer(Player)
	Chat = "^" .. Start .. ".*" .. Mid .. ".*" .. End .. "$"
	chatindex = AddChatEventPlugin(Chat)
	--chatarray = "VChat_" .. chatindex
	chatarray = "VChatIndex"

	
	echo("MemoryEPD(EPD(msqcvar." .. chatarray .. ") + " .. Player .. ", Exactly, " .. chatindex + 1 ..")")
end

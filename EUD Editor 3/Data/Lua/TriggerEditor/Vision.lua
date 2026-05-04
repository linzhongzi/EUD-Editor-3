--[================================[
@Language.ko-KR
@Summary
[Player]에게 [DestPlayer]의 시야를 [State]합니다.
@Group
시야
@param.Player.TrgPlayer
@param.DestPlayer.TrgPlayer
@param.State.TrgSwitchState


@Language.zh-CN
@Summary
给 [Player] 提供 [DestPlayer] 的视野，状态为 [State] 。
@Group
视野
@param.Player.TrgPlayer
@param.DestPlayer.TrgPlayer
@param.State.TrgSwitchState


@Language.en-US
@Summary
Sets the vision of [DestPlayer] for [Player] to [State].
@Group
Vision
@param.Player.TrgPlayer
The player receiving vision.
@param.DestPlayer.TrgPlayer
The player whose vision is given.
@param.State.TrgSwitchState
The state (give/remove).
]================================]
function SetVision(Player, DestPlayer, State) -- Vision 组/TrgPlayer, TrgPlayer, TrgSwitchState/给 [Player] 提供 [DestPlayer] 的视野，状态为 [State] 。
	State = ParseSwitchState(State)
	Player = ParsePlayer(Player)
	DestPlayer = ParsePlayer(DestPlayer)
	offsetEPD = VisionEPD(DestPlayer)


	if State == 2 then
		Amount = "0xFF"
	else
		Amount = "0x0"
	end

	if IsNumber(Player) then
		Mask = 2^Player
		rstr = string.format("SetMemoryXEPD(%s, SetTo, %s, %d)", offsetEPD, Amount, Mask)
	else
		preDefine("const VisionMask = EUDVArray(8)(list(1, 2, 4, 8, 16, 32, 64, 128));")
		rstr = string.format("SetMemoryXEPD(%s, SetTo, %s, VisionMask[%s])", offsetEPD, Amount, Player)
	end

	echo(rstr)
end

--[================================[
@Language.ko-KR
@Summary
[Player]가 [DestPlayer]를 볼 수 있는지 확인합니다.
@Group
시야
@param.Player.TrgPlayer
@param.DestPlayer.TrgPlayer


@Language.zh-CN
@Summary
确认 [Player] 是否可以看到 [DestPlayer] 。
@Group
视野
@param.Player.TrgPlayer
@param.DestPlayer.TrgPlayer


@Language.en-US
@Summary
Checks if [Player] can see [DestPlayer].
@Group
Vision
@param.Player.TrgPlayer
The player.
@param.DestPlayer.TrgPlayer
The player to check visibility of.
]================================]
function GetVision(Player, DestPlayer) -- Vision 组/TrgPlayer, TrgPlayer/确认 [Player] 是否可以看到 [DestPlayer] 。
	Player = ParsePlayer(Player)
	DestPlayer = ParsePlayer(DestPlayer)
	offsetEPD = VisionEPD(DestPlayer)

	if IsNumber(Player) then
		Mask = 2^Player

		rstr = string.format("maskread_epd(%s, %d)", offsetEPD, Mask)
	else
		preDefine("const VisionMask = EUDVArray(8)(list(1, 2, 4, 8, 16, 32, 64, 128));")
		rstr = string.format("maskread_epd(%s, VisionMask[%s])", offsetEPD, Player)
	end

	echo(rstr)
end

--[================================[
@Language.ko-KR
@Summary
[Player]의 시야 오프셋을 반환합니다.
@Group
시야
@param.Player.TrgPlayer


@Language.zh-CN
@Summary
返回 [Player] 的视野参数的偏移地址。
@Group
视野
@param.Player.TrgPlayer


@Language.en-US
@Summary
Returns the vision offset for [Player].
@Group
Vision
@param.Player.TrgPlayer
The target player.
]================================]
function VisionEPD(Player) -- Vision 组/TrgPlayer/返回 [Player] 的视野参数的偏移地址。
	Player = ParsePlayer(Player)

	if IsNumber(Player) then
		return string.format("EPD(0x%X)", 0x57F1EC + 0x4 * Player) 
	else
		return string.format("EPD(0x%X) + %s", 0x57F1EC ,Player) 
	end
end

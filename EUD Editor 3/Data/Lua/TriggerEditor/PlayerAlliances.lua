--[================================[
@Language.ko-KR
@Summary
[Player]이 보는 [DestPlayer]와의 동맹 관계를 [AllyStatus]로 설정합니다.
@Group
동맹
@param.Player.TrgPlayer
@param.DestPlayer.TrgPlayer
@param.AllyStatus.TrgAllyStatus


@Language.zh-CN
@Summary
将 [Player] 与 [DestPlayer] 的联盟关系设置为 [AllyStatus].
@Group
结盟
@param.Player.TrgPlayer
@param.DestPlayer.TrgPlayer
@param.AllyStatus.TrgAllyStatus
结盟状态：联盟


@Language.en-US
@Summary
Sets the alliance status from [Player] 's perspective towards [DestPlayer] to [AllyStatus].
@Group
Alliance
@param.Player.TrgPlayer
The source player.
@param.DestPlayer.TrgPlayer
The target player.
@param.AllyStatus.TrgAllyStatus
The alliance status.
]================================]
function SetPlayerAlliances(Player, DestPlayer, AllyStatus) -- 结盟/TrgPlayer, TrgPlayer, TrgAllyStatus/将 [Player] 与 [DestPlayer] 的联盟关系设置为 [AllyStatus].
	Player = ParsePlayer(Player)
	DestPlayer = ParsePlayer(DestPlayer)
	AllyStatus = ParseAllyStatus(AllyStatus)
	offsetEPD = PlayerAlliancesEPD(Player)

	if IsNumber(DestPlayer) then
		Mod = DestPlayer % 4
		RPlayer = DestPlayer - Mod
		RPlayer = RPlayer / 4
		Mask = "0xFF"
		for i = 1, Mod do Mask = Mask .. "00" end

		if IsNumber(AllyStatus) then
			rstr = string.format("SetMemoryXEPD(%s + %s, SetTo, %s, %s)", RPlayer, offsetEPD, AllyStatus * math.pow(256, Mod), Mask)
		else
			rstr = string.format("SetMemoryXEPD(%s + %s, SetTo, %s, %s)", RPlayer, offsetEPD, AllyStatus .. " * " .. math.pow(256, Mod), Mask)
		end

	else
		offset = PlayerAlliances(Player)
		rstr = string.format("bwrite_epd(%s, %s & 3, %s)", offsetEPD, DestPlayer, AllyStatus)
	end

	echo(rstr)
end

--[================================[
@Language.ko-KR
@Summary
[Player]이 보는 [DestPlayer]와의 동맹 관계가 [AllyStatus]인지 확인합니다.
@Group
동맹
@param.Player.TrgPlayer
@param.DestPlayer.TrgPlayer
@param.AllyStatus.TrgAllyStatus


@Language.zh-CN
@Summary
检测 [Player] 与 [DestPlayer] 的联盟关系设置是否为 [AllyStatus].
@Group
结盟
@param.Player.TrgPlayer
@param.DestPlayer.TrgPlayer
@param.AllyStatus.TrgAllyStatus


@Language.en-US
@Summary
Checks if the alliance status from [Player] 's perspective towards [DestPlayer] is [AllyStatus].
@Group
Alliance
@param.Player.TrgPlayer
The source player.
@param.DestPlayer.TrgPlayer
The target player.
@param.AllyStatus.TrgAllyStatus
The alliance status.
]================================]
function CurrentPlayerAlliances(Player, DestPlayer, AllyStatus) -- Alliance 组/TrgPlayer, TrgPlayer, TrgAllyStatus/检测 [Player] 与 [DestPlayer] 的联盟关系设置是否为 [AllyStatus].
	Player = ParsePlayer(Player)
	DestPlayer = ParsePlayer(DestPlayer)
	AllyStatus = ParseAllyStatus(AllyStatus)
	offsetEPD = PlayerAlliancesEPD(Player)

	if IsNumber(DestPlayer) then
		Mod = DestPlayer % 4
		RPlayer = DestPlayer - Mod
		RPlayer = RPlayer / 4
		Mask = "0xFF"
		for i = 1, Mod do Mask = Mask .. "00" end

		if IsNumber(AllyStatus) then
			rstr = string.format("MemoryXEPD(%s + %s, Exactly, %s, %s)", RPlayer, offsetEPD, AllyStatus * math.pow(256, Mod), Mask)
		else
			rstr = string.format("MemoryXEPD(%s + %s, Exactly, %s, %s)", RPlayer, offsetEPD, AllyStatus .. " * " .. math.pow(256, Mod), Mask)
		end

	else
		rstr = string.format("bread_epd(%s, %s & 3) == %s", offsetEPD, DestPlayer, AllyStatus)
	end

	echo(rstr)
end

--[================================[
@Language.ko-KR
@Summary
[Player]이 보는 [DestPlayer]와의 동맹 관계를 반환합니다.
@Group
동맹
@param.Player.TrgPlayer
@param.DestPlayer.TrgPlayer


@Language.zh-CN
@Summary
返回 [Player] 与 [DestPlayer] 的结盟关系。
@Group
结盟
@param.Player.TrgPlayer
@param.DestPlayer.TrgPlayer


@Language.en-US
@Summary
Returns the alliance status from [Player] 's perspective towards [DestPlayer].
@Group
Alliance
@param.Player.TrgPlayer
The source player.
@param.DestPlayer.TrgPlayer
The target player.
]================================]
function GetPlayerAlliances(Player, DestPlayer) -- Alliance 组/TrgPlayer, TrgPlayer/返回 [Player] 与 [DestPlayer] 的结盟关系。
	Player = ParsePlayer(Player)
	DestPlayer = ParsePlayer(DestPlayer)
	offsetEPD = PlayerAlliancesEPD(Player)

	if IsNumber(DestPlayer) then
		Mod = DestPlayer % 4
		RPlayer = DestPlayer - Mod
		RPlayer = RPlayer / 4

		rstr = string.format("bread_epd(%s + %s, %s)", RPlayer, offsetEPD, Mod)
	else
		rstr = string.format("bread_epd(%s, %s & 3)", offsetEPD, DestPlayer)
	end

	echo(rstr)
end

--[================================[
@Language.ko-KR
@Summary
[Player]의 동맹 오프셋을 반환합니다.
@Group
동맹
@param.Player.TrgPlayer


@Language.zh-CN
@Summary
返回 [Player] 的联盟状态值的地址。
@Group
结盟
@param.Player.TrgPlayer


@Language.en-US
@Summary
Returns the alliance offset for [Player].
@Group
Alliance
@param.Player.TrgPlayer
The target player.
]================================]
function PlayerAlliancesEPD(Player) -- Alliance 组/TrgPlayer/返回 [Player] 的联盟状态值的地址。
	Player = ParsePlayer(Player)

	if IsNumber(Player) then
		return string.format("EPD(0x%X)", 0x58D634 + 0xC * Player)
	else
		return string.format("EPD(0x%X) + %s * 3", 0x58D634, Player)
	end
end

--[================================[
@Language.ko-KR
@Summary
[Player]의 동맹 오프셋을 반환합니다.
@Group
동맹
@param.Player.TrgPlayer


@Language.zh-CN
@Summary
返回 [Player] 的联盟状态值的地址。
@Group
结盟
@param.Player.TrgPlayer


@Language.en-US
@Summary
Returns the alliance offset for [Player].
@Group
Alliance
@param.Player.TrgPlayer
The target player.
]================================]
function PlayerAlliances(Player) -- Alliance 组/TrgPlayer/返回 [Player] 的联盟状态值的地址。
	Player = ParsePlayer(Player)

	if IsNumber(Player) then
		return string.format("0x%X", 0x58D634 + 0xC * Player)
	else
		return string.format("0x%X + %s * 12", 0x58D634 ,Player)
	end
end

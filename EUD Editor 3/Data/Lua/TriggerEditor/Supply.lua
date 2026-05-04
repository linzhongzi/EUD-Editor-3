--[================================[
@Language.ko-KR
@Summary
[Player]의 [SupplyType]를 [Amount]만큼 [Modifier]합니다.
@Group
인구수
@param.SupplyType.SupplyType
@param.Player.TrgPlayer
@param.Modifier.TrgModifier
@param.Amount.Number


@Language.zh-CN
@Summary
将 [Player] 的 [SupplyType] 值 [Modifier] 为 [Amount].
@Group
人口
@param.SupplyType.SupplyType
@param.Player.TrgPlayer
@param.Modifier.TrgModifier
@param.Amount.Number


@Language.en-US
@Summary
[Modifier] s the [SupplyType] of [Player] by [Amount].
@Group
Supply
@param.SupplyType.SupplyType
The supply type (used/available).
@param.Player.TrgPlayer
The target player.
@param.Modifier.TrgModifier
The modifier.
@param.Amount.Number
The amount.
]================================]
function SetSupply(SupplyType, Player, Modifier, Amount) -- Supply 组/SupplyType, TrgPlayer, TrgModifier, Number/将 [Player] 的 [SupplyType] 值 [Modifier] 为 [Amount].
	Player = ParsePlayer(Player)
    Modifier = ParseModifier(Modifier)
	OffsetEPD = SupplyEPD(SupplyType, Player)


	rstr = string.format("SetMemoryEPD(%s, %s, %s)",OffsetEPD, Modifier, Amount)
	echo(rstr)
end

--[================================[
@Language.ko-KR
@Summary
[Comparison]: [Player]의 [SupplyType]가 [Amount]인지 확인합니다.
@Group
인구수
@param.SupplyType.SupplyType
@param.Player.TrgPlayer
@param.Comparison.TrgComparison
@param.Amount.Number


@Language.zh-CN
@Summary
[Comparison]: 确认 [Player] 的 [SupplyType] 是否为 [Amount] 。
@Group
人口
@param.SupplyType.SupplyType
@param.Player.TrgPlayer
@param.Comparison.TrgComparison
@param.Amount.Number


@Language.en-US
@Summary
[Comparison]: Checks if the [SupplyType] of [Player] is [Amount].
@Group
Supply
@param.SupplyType.SupplyType
The supply type (used/available).
@param.Player.TrgPlayer
The target player.
@param.Comparison.TrgComparison
The comparison method.
@param.Amount.Number
The amount.
]================================]
function CurrentSupply(SupplyType, Player, Comparison, Amount) -- Supply 组/SupplyType, TrgPlayer, TrgComparison, Number/[Comparison]: 确认 [Player] 的 [SupplyType] 是否为 [Amount] 。
	Player = ParsePlayer(Player)
    Comparison = ParseComparison(Comparison)
	OffsetEPD = SupplyEPD(SupplyType, Player)


	rstr = string.format("MemoryEPD(%s, %s, %s)",OffsetEPD, Comparison, Amount)
	echo(rstr)
end

--[================================[
@Language.ko-KR
@Summary
[Player]의 [SupplyType] 값을 읽습니다.
@Group
인구수
@param.SupplyType.SupplyType
@param.Player.TrgPlayer


@Language.zh-CN
@Summary
读取 [Player] 中 [SupplyType] 的值。
@Group
人口
@param.SupplyType.SupplyType
@param.Player.TrgPlayer


@Language.en-US
@Summary
Reads the [SupplyType] value of [Player].
@Group
Supply
@param.SupplyType.SupplyType
The supply type (used/available).
@param.Player.TrgPlayer
The target player.
]================================]
function GetSupply(SupplyType, Player) -- Supply 组/SupplyType, TrgPlayer/读取 [Player] 中 [SupplyType] 的值。
	Player = ParsePlayer(Player)
	OffsetEPD = SupplyEPD(SupplyType, Player)

	echo(string.format("dwread_epd(%s)", OffsetEPD))
end

--[================================[
@Language.ko-KR
@Summary
[Player]의 [SupplyType] 주소를 반환합니다.
@Group
인구수
@param.SupplyType.SupplyType
@param.Player.TrgPlayer


@Language.zh-CN
@Summary
返回 [Player] 中 [SupplyType] 的地址。
@Group
人口
@param.SupplyType.SupplyType
@param.Player.TrgPlayer


@Language.en-US
@Summary
Returns the address of the [SupplyType] of [Player].
@Group
Supply
@param.SupplyType.SupplyType
The supply type (used/available).
@param.Player.TrgPlayer
The target player.
]================================]
function SupplyEPD(SupplyType, Player) -- Supply 组/SupplyType, TrgPlayer/返回 [Player] 中 [SupplyType] 的地址。
	Player = ParsePlayer(Player)
	SupplyIndex = ParseSupplyType(SupplyType)


	SupplyOffset = GetSupplyOffset(SupplyIndex)
	if IsNumber(Player) then
		SupplyOffset = SupplyOffset + Player * 4
		return string.format("EPD(0x%X)", SupplyOffset)
	else
		return string.format("EPD(%s) + %s", SupplyOffset, Player)
	end
end

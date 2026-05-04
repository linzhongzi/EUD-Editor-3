--[================================[
@Language.ko-KR
@Summary
[Player]의 [Unit]의 유닛보유수를 [Amount]만큼 [Modifier]합니다.
@Group
유닛보유수
@param.Unit.TrgUnit
@param.Player.TrgPlayer
@param.Modifier.TrgModifier
@param.Amount.Number


@Language.zh-CN
@Summary
将 [Player] 的 [Unit] 数量 [Modifier] 为 [Amount].
@Group
持有单位数量
@param.Unit.TrgUnit
@param.Player.TrgPlayer
@param.Modifier.TrgModifier
@param.Amount.Number


@Language.en-US
@Summary
[Modifier] s the unit count of [Unit] for [Player] by [Amount].
@Group
Unit Count
@param.Unit.TrgUnit
The target unit.
@param.Player.TrgPlayer
The target player.
@param.Modifier.TrgModifier
The modifier.
@param.Amount.Number
The amount.
]================================]
function SetUnitCount(Unit, Player, Modifier, Amount) -- General 组/TrgUnit, TrgPlayer, TrgModifier, Number/将 [Player] 的 [Unit] 数量 [Modifier] 为 [Amount].
	Player = ParsePlayer(Player)
    Modifier = ParseModifier(Modifier)
    Unit = ParseUnit(Unit)
    OffsetEPD = UnitCountEPD(0, Player)


	echo(string.format("SetDeaths(%s, %s, %s, %s)", OffsetEPD, Modifier, Amount, Unit))
end

--[================================[
@Language.ko-KR
@Summary
[Player]의 [Unit]의 유닛보유수를 반환합니다.
@Group
유닛보유수
@param.Unit.TrgUnit
@param.Player.TrgPlayer


@Language.zh-CN
@Summary
返回 [Player] 持有的 [Unit] 数量。
@Group
持有单位数量
@param.Unit.TrgUnit
@param.Player.TrgPlayer


@Language.en-US
@Summary
Returns the unit count of [Unit] for [Player].
@Group
Unit Count
@param.Unit.TrgUnit
The target unit.
@param.Player.TrgPlayer
The target player.
]================================]
function GetUnitCount(Unit, Player) -- General 组/TrgUnit, TrgPlayer/返回 [Player] 持有的 [Unit] 数量。
	Player = ParsePlayer(Player)
    Unit = ParseUnit(Unit)
    OffsetEPD = UnitCountEPD(Unit, Player)

	echo(string.format("dwread_epd(%s)", OffsetEPD))
end

--[================================[
@Language.ko-KR
@Summary
[Player]의 [Unit]의 유닛보유수의 주소를 반환합니다.
@Group
유닛보유수
@param.Unit.TrgUnit
@param.Player.TrgPlayer


@Language.zh-CN
@Summary
返回 [Player] 持有的 [Unit] 数量的地址。
@Group
持有单位数量
@param.Unit.TrgUnit
@param.Player.TrgPlayer


@Language.en-US
@Summary
Returns the address of the unit count of [Unit] for [Player].
@Group
Unit Count
@param.Unit.TrgUnit
The target unit.
@param.Player.TrgPlayer
The target player.
]================================]
function UnitCountEPD(Unit, Player) -- General 组/TrgUnit, TrgPlayer/返回 [Player] 持有的 [Unit] 数量的地址。
	Player = ParsePlayer(Player)
    Unit = ParseUnit(Unit)

    Offset = 0x582324
	if IsNumber(Player) and IsNumber(Unit) then
		Offset = Offset + (Unit * 12 + Player) * 4
		return string.format("EPD(0x%X)", Offset)
	else
		return string.format("EPD(0x%X) + %s * 12 + %s", Offset, Unit, Player)
	end
end

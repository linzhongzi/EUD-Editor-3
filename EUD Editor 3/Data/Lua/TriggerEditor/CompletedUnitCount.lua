--[================================[
@Language.ko-KR
@Summary
[Player]의 [Unit]의 완료된 유닛보유수를 [Amount]만큼 [Modifier]합니다.
@Group
유닛보유수
@param.Unit.TrgUnit
대상 유닛입니다. 이 유닛이 필요한 테크가 해금됩니다.
@param.Player.TrgPlayer
대상 플레이어입니다.
@param.Modifier.TrgModifier
수식입니다.
@param.Amount.Number
대상 플레이어입니다.

@Language.zh-CN
@Summary
将指定 [Player] 的 [Unit] 的已完成数量 [Modifier] 为 [Amount] 个 .
@Group
持有单位数量
@param.Unit.TrgUnit
这是目标单位。
需要该单位的技术已解锁。
@param.Player.TrgPlayer
目标玩家。
@param.Modifier.TrgModifier
修改函数。
@param.Amount.Number
目标玩家。


@Language.en-US
@Summary
[Modifier] s the completed unit count of [Unit] for [Player] by [Amount].
@Group
Unit Count
@param.Unit.TrgUnit
The target unit. The tech requiring this unit is unlocked.
@param.Player.TrgPlayer
The target player.
@param.Modifier.TrgModifier
The modifier.
@param.Amount.Number
The amount.
]================================]
function SetCompletedUnitCount(Unit, Player, Modifier, Amount)
	Player = ParsePlayer(Player)
    Modifier = ParseModifier(Modifier)
    Unit = ParseUnit(Unit)
    OffsetEPD = CompletedUnitCountEPD(Unit, Player)


	echo(string.format("SetDeaths(%s, %s, %s, 0)", OffsetEPD, Modifier, Amount))
end

--[================================[
@Language.ko-KR
@Summary
[Player]의 [Unit]의 완료된 유닛보유수를 반환합니다.
@Group
유닛보유수
@param.Unit.TrgUnit
대상 유닛입니다. 이 유닛이 필요한 테크가 해금됩니다.
@param.Player.TrgPlayer
대상 플레이어입니다.

@Language.zh-CN
@Summary
返回 [Player] 的 [Unit] 所持有的已完成单位的数量。
@Group
持有单位数量
@param.Unit.TrgUnit
这是目标单位。
需要该单位的技术已解锁。
@param.Player.TrgPlayer
目标玩家。


@Language.en-US
@Summary
Returns the completed unit count of [Unit] for [Player].
@Group
Unit Count
@param.Unit.TrgUnit
The target unit. The tech requiring this unit is unlocked.
@param.Player.TrgPlayer
The target player.
]================================]
function GetCompletedUnitCount(Unit, Player)
	Player = ParsePlayer(Player)
    Unit = ParseUnit(Unit)
    OffsetEPD = CompletedUnitCountEPD(Unit, Player)

	echo(string.format("dwread_epd(%s)", OffsetEPD))
end

--[================================[
@Language.ko-KR
@Summary
[Player]의 [Unit]의 완료된 유닛보유수의 주소를 반환합니다.
@Group
유닛보유수
@param.Unit.TrgUnit
@param.Player.TrgPlayer

@Language.zh-CN
@Summary
返回 [Player] 的 [Unit] 中保存的已完成单元数的地址。
@Group
单位数量组
@param.Unit.TrgUnit
@param.Player.TrgPlayer

@Language.en-US
@Summary
Returns the address of the completed unit count of [Unit] for [Player].
@Group
Unit Count
@param.Unit.TrgUnit
The target unit.
@param.Player.TrgPlayer
The target player.
]================================]
function CompletedUnitCountEPD(Unit, Player) -- General/TrgUnit,TrgPlayer/返回 [Player] 中 [Unit] 的已完成单位数量的地址。
	Player = ParsePlayer(Player)
    Unit = ParseUnit(Unit)
    Offset = 0x584DE4
	if IsNumber(Player) and IsNumber(Unit) then
		Offset = Offset + (Unit * 12 + Player) * 4
		return string.format("EPD(0x%X)", Offset)
	else
		return string.format("EPD(0x%X) + %s * 12 + %s", Offset, Unit, Player)
	end
end

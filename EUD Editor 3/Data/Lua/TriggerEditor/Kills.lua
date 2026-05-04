--[================================[
@Language.ko-KR
@Summary
[Player]가 [Unit]을 죽인 수를 [Amount]만큼 [Modifier]합니다.
@Group
일반
@param.Player.TrgPlayer
대상 플레이어입니다.
@param.Modifier.TrgModifier
조절할 방법입니다.
@param.Amount.Number
설정할 값입니다.
@param.Unit.TrgUnit
설정할 유닛입니다.


@Language.zh-CN
@Summary
被 [Player] 杀死的 [Unit] 的次数 [Modifier] 为 [Amount].
@Group
普通用户组
@param.Player.TrgPlayer
目标玩家。
@param.Modifier.TrgModifier
修改函数。
@param.Amount.Number
修改值。
@param.Unit.TrgUnit
要修改的单位。


@Language.en-US
@Summary
[Modifier] s the number of [Unit] killed by [Player] by [Amount].
@Group
General
@param.Player.TrgPlayer
The target player.
@param.Modifier.TrgModifier
The method of adjustment.
@param.Amount.Number
The value to set.
@param.Unit.TrgUnit
The unit to set.
]================================]
function SetKills(Player, Modifier, Amount, Unit) -- General 组/TrgPlayer, TrgModifier, Number, TrgUnit/被 [Player] 杀死的 [Unit] 的次数 [Modifier] 为 [Amount].
	Player = ParsePlayer(Player)
    Modifier = ParseModifier(Modifier)
    Unit = ParseUnit(Unit)
    OffsetEPD = KillsEPD(0, Player)


	echo(string.format("SetDeaths(%s, %s, %s, %s)", OffsetEPD, Modifier, Amount, Unit))
end

--[================================[
@Language.ko-KR
@Summary
[Player]가 [Unit]이 죽은 수를 읽습니다.
@Group
일반
@param.Unit.TrgUnit
설정할 유닛입니다.
@param.Player.TrgPlayer
대상 플레이어입니다.


@Language.zh-CN
@Summary
读取被 [Player] 杀死的 [Unit] 的次数
@Group
普通用户组
@param.Unit.TrgUnit
要读取的单位。
@param.Player.TrgPlayer
目标玩家。


@Language.en-US
@Summary
Reads the number of [Unit] killed by [Player].
@Group
General
@param.Unit.TrgUnit
The unit to set.
@param.Player.TrgPlayer
The target player.
]================================]
function GetKills(Unit, Player) -- General 组/TrgUnit, TrgPlayer/读取被 [Player] 杀死的 [Unit] 的次数
	Player = ParsePlayer(Player)
    Unit = ParseUnit(Unit)
    OffsetEPD = KillsEPD(Unit, Player)

	echo(string.format("dwread_epd(%s)", OffsetEPD))
end

--[================================[
@Language.ko-KR
@Summary
[Player]가 [Unit]을 죽인 수의 주소를 반환합니다.
@Group
일반
@param.Unit.TrgUnit
설정할 유닛입니다.
@param.Player.TrgPlayer
대상 플레이어입니다.


@Language.zh-CN
@Summary
返回被 [Player] 杀死的 [Unit] 的数值的地址。
@Group
普通用户组
@param.Unit.TrgUnit
要读取的单位。
@param.Player.TrgPlayer
目标玩家。


@Language.en-US
@Summary
Returns the address of the number of [Unit] killed by [Player].
@Group
General
@param.Unit.TrgUnit
The unit to set.
@param.Player.TrgPlayer
The target player.
]================================]
function KillsEPD(Unit, Player) -- General 组/TrgUnit, TrgPlayer/返回被 [Player] 杀死的 [Unit] 的数值的地址。
	Player = ParsePlayer(Player)
    Unit = ParseUnit(Unit)

    Offset = 0x5878A4
	if IsNumber(Player) and IsNumber(Unit) then
		Offset = Offset + (Unit * 12 + Player) * 4
		return string.format("EPD(0x%X)", Offset)
	else
		return string.format("EPD(0x%X) + %s * 12 + %s", Offset, Unit, Player)
	end
end

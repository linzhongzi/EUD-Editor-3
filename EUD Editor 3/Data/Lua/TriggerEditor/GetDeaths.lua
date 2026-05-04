--[================================[
@Language.ko-KR
@Summary
[Player]의 [Unit] 데스값을 반환합니다.
@Group
일반
@param.Unit.TrgUnit
데스값을 가져올 유닛입니다.
@param.Player.TrgPlayer
대상 플레이어입니다.


@Language.zh-CN
@Summary
返回目标 [Player] 的 [Unit] 的死亡值。
@Group
普通用户组
@param.Unit.TrgUnit
这是将获得死亡值的单位。
@param.Player.TrgPlayer
目标玩家。


@Language.en-US
@Summary
Returns the death count of [Unit] for [Player].
@Group
General
@param.Unit.TrgUnit
The unit to get the death count for.
@param.Player.TrgPlayer
The target player.
]================================]
function GetDeaths(Unit, Player) -- General 组/TrgUnit, TrgPlayer/返回目标 [Player] 的 [Unit] 的死亡值。
    Unit = ParseUnit(Unit)
    Player = ParsePlayer(Player)

    str = string.format("dwread_epd(%s * 12 + %s)", Unit, Player)
    echo(str)
end

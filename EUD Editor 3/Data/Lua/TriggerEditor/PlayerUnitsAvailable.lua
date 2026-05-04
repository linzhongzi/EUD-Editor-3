--[================================[
@Language.ko-KR
@Summary
[Player]의 [Unit] 유닛 사용 가능 값을 [State]합니다.
@Group
플레이어
@param.Player.TrgPlayer
@param.Unit.TrgUnit
@param.State.TrgSwitchState


@Language.zh-CN
@Summary
将 [Player] 的 [Unit] 单位可用值设置为 [State] 。
@Group
玩家
@param.Player.TrgPlayer
@param.Unit.TrgUnit
@param.State.TrgSwitchState


@Language.en-US
@Summary
Sets the availability of unit [Unit] for [Player] to [State].
@Group
Player
@param.Player.TrgPlayer
The target player.
@param.Unit.TrgUnit
The target unit.
@param.State.TrgSwitchState
The state (enabled/disabled).
]================================]
function SetPlayerUnitsAvailable(Player, Unit, State) -- General 组/TrgPlayer, TrgUnit, TrgSwitchState/将 [Player] 的 [Unit] 单位可用值设置为 [State] 。
	Player = ParsePlayer(Player)
    Unit = ParseUnit(Unit)
    State = ParseSwitchState(State)

    if IsNumber(Unit) and IsNumber(Player) then
		Offset = 0x57F27C + Unit + Player * 228

 		if State == 2 then
    		Amount = "0x01010101"
    	else
    		Amount = "0x0"
    	end

		Mod = Unit % 4
		Mask = "0xFF"
		for i = 1, Mod do Mask = Mask .. "00" end

		rstr = string.format("SetMemoryX(0x%X, SetTo, %s, %s)", Offset, Amount, Mask)
    	echo(rstr)
    else
    	if State == 2 then
    		Amount = 1
    	else
    		Amount = 0
    	end

		Offset = string.format("%s + %s + %s * 228", PlayerUnitsAvailableOffset(), Unit, Player)
		echo(string.format("bwrite(%s, %s)", Offset, Amount))
    end
end

--[================================[
@Language.ko-KR
@Summary
[Player]의 [Unit] 유닛 사용 가능 값을 읽어옵니다.
@Group
플레이어
@param.Player.TrgPlayer
@param.Unit.TrgUnit


@Language.zh-CN
@Summary
读取 [Player] 的 [Unit] 是否可用的状态值。
@Group
玩家
@param.Player.TrgPlayer
@param.Unit.TrgUnit


@Language.en-US
@Summary
Reads the availability value of unit [Unit] for [Player].
@Group
Player
@param.Player.TrgPlayer
The target player.
@param.Unit.TrgUnit
The target unit.
]================================]
function GetPlayerUnitsAvailable(Player, Unit) -- General 组/TrgPlayer, TrgUnit/读取 [Player] 的 [Unit] 是否可用的状态值。
	Player = ParsePlayer(Player)
    Unit = ParseUnit(Unit)

	Offset = string.format("%s + %s + %s * 228", PlayerUnitsAvailableOffset(), Unit, Player)

	echo(string.format("bread(%s)", Offset))
end

--[================================[
@Language.ko-KR
@Summary
플레이어 유닛 사용 가능 오프셋을 가져옵니다.
@Group
플레이어


@Language.zh-CN
@Summary
获取玩家单位可用性标识的偏移地址。
@Group
玩家


@Language.en-US
@Summary
Gets the player unit availability offset.
@Group
Player
]================================]
function PlayerUnitsAvailableOffset() -- General 组//获取玩家单位可用性标识的偏移地址。
	return "0x57F27C"
end

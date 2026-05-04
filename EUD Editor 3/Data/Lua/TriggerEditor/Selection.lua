--[================================[
@Language.ko-KR
@Summary
해당플레이어의 선택 유닛의 버튼셋 ID가 [Comparison] [Amount]인지 확인합니다.
@Group
선택인식
@param.Comparison.TrgComparison
@param.Amount.Number


@Language.zh-CN
@Summary
[Comparison]: 确认玩家选择的单位的按钮组 ID 是否为 [Amount] 。
@Group
选择认可
@param.Comparison.TrgComparison
@param.Amount.Number


@Language.en-US
@Summary
[Comparison]: Checks if the button set ID of the player's selected unit is [Amount].
@Group
Selection Recognition
@param.Comparison.TrgComparison
The comparison method.
@param.Amount.Number
The amount.
]================================]
function LocalSelectID(Comparison, Amount)
    Comparison = ParseComparison(Comparison)

	rstr = string.format("MemoryEPD(EPD(0x68C14C), %s, %s)",Comparison, Amount)
	echo(rstr)
end

--[================================[
@Language.ko-KR
@Summary
[Player]의 [Score]가 [Comparison] [Amount]인지 확인합니다.
@Group
선택인식
@param.Comparison.TrgComparison
@param.Amount.Number


@Language.zh-CN
@Summary
[Comparison]: 确认 [Player] 的 [Score] 值是否为 [Amount] 。
@Group
选择认可
@param.Comparison.TrgComparison
@param.Amount.Number


@Language.en-US
@Summary
[Comparison]: Checks if the [Score] of [Player] is [Amount].
@Group
Selection Recognition
@param.Comparison.TrgComparison
The comparison method.
@param.Amount.Number
The amount.
]================================]
function LocalSelectPtr(Comparison, Amount)
    Comparison = ParseComparison(Comparison)

	rstr = string.format("MemoryEPD(EPD(0x6284B8), %s, %s)", Comparison, Amount)
	echo(rstr)
end

--[================================[
@Language.ko-KR
@Summary
[Player]가 [ptr]을 선택했는지 확인합니다.
@Group
선택인식
@param.Player.TrgPlayer
@param.ptr.Number


@Language.zh-CN
@Summary
确认 [Player] 是否已选择 [ptr].
@Group
选择认可
@param.Player.TrgPlayer
@param.ptr.Number


@Language.en-US
@Summary
Checks if [Player] has selected [ptr].
@Group
Selection Recognition
@param.Player.TrgPlayer
The target player.
@param.ptr.Number
The pointer value.
]================================]
function SelectUnit(Player, ptr)
	Player = ParsePlayer(Player)
	
	if IsNumber(Player) then
		address = 0x6284E8 + 36 * Player
		rstr = string.format("MemoryEPD(EPD(%s), Exactly, %s)", address, ptr)
	else
		rstr = string.format("MemoryEPD(EPD(0x6284E8) + 9 * %s, Exactly, %s)", Player, ptr)
	end

	echo(rstr)
end

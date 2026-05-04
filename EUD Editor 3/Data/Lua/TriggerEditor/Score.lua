--[================================[
@Language.ko-KR
@Summary
[Player]의 [Score]를 [Amount]만큼 [Modifier]합니다.
@Group
스코어
@param.Score.EUDScore
@param.Player.TrgPlayer
@param.Modifier.TrgModifier
@param.Amount.Number


@Language.zh-CN
@Summary
将 [Player] 的 [Score] 分数 [Modifier] 为值 [Amount] 。
@Group
分数
@param.Score.EUDScore
@param.Player.TrgPlayer
@param.Modifier.TrgModifier
@param.Amount.Number


@Language.en-US
@Summary
[Modifier] s the [Score] of [Player] by [Amount].
@Group
Score
@param.Score.EUDScore
The score type.
@param.Player.TrgPlayer
The target player.
@param.Modifier.TrgModifier
The modifier.
@param.Amount.Number
The amount.
]================================]
function SetScore(Score, Player, Modifier, Amount) -- General 组/EUDScore, TrgPlayer, TrgModifier, Number/将 [Player] 的 [Score] 分数 [Modifier] 为值 [Amount] 。
	Player = ParsePlayer(Player)
    Modifier = ParseModifier(Modifier)
	OffsetEPD = ScoreEPD(Score, Player)


	rstr = string.format("SetMemoryEPD(%s, %s, %s)",OffsetEPD, Modifier, Amount)
	echo(rstr)
end

--[================================[
@Language.ko-KR
@Summary
[Player]의 [Score]가 [Comparison] [Amount]인지 확인합니다.
@Group
스코어
@param.Score.EUDScore
@param.Player.TrgPlayer
@param.Comparison.TrgComparison
@param.Amount.Number


@Language.zh-CN
@Summary
[Comparison]: 比较 [Player] 的 [Score] 值是否为 [Amount] 。
@Group
分数
@param.Score.EUDScore
@param.Player.TrgPlayer
@param.Comparison.TrgComparison
@param.Amount.Number


@Language.en-US
@Summary
[Comparison]: Checks if the [Score] of [Player] is [Amount].
@Group
Score
@param.Score.EUDScore
The score type.
@param.Player.TrgPlayer
The target player.
@param.Comparison.TrgComparison
The comparison method.
@param.Amount.Number
The amount.
]================================]
function CurrentScore(Score, Player, Comparison, Amount) -- General 组/EUDScore, TrgPlayer, TrgComparison, Number/[Comparison]: 比较 [Player] 的 [Score] 值是否为 [Amount] 。
	Player = ParsePlayer(Player)
    Comparison = ParseComparison(Comparison)
	OffsetEPD = ScoreEPD(Score, Player)


	rstr = string.format("MemoryEPD(%s, %s, %s)",OffsetEPD, Comparison, Amount)
	echo(rstr)
end

--[================================[
@Language.ko-KR
@Summary
[Player]의 [Score] 값을 읽습니다.
@Group
스코어
@param.Score.EUDScore
@param.Player.TrgPlayer


@Language.zh-CN
@Summary
读取 [Player] 中 [Score] 的值。
@Group
分数
@param.Score.EUDScore
@param.Player.TrgPlayer


@Language.en-US
@Summary
Reads the [Score] value of [Player].
@Group
Score
@param.Score.EUDScore
The score type.
@param.Player.TrgPlayer
The target player.
]================================]
function GetScore(Score, Player) -- General 组/EUDScore, TrgPlayer/读取 [Player] 中 [Score] 的值。
	Player = ParsePlayer(Player)
	OffsetEPD = ScoreEPD(Score, Player)

	echo(string.format("dwread_epd(%s)", OffsetEPD))
end

--[================================[
@Language.ko-KR
@Summary
[Player]의 [Score] 주소를 반환합니다.
@Group
스코어
@param.Score.EUDScore
@param.Player.TrgPlayer


@Language.zh-CN
@Summary
返回 [Player] 中 [Score] 的地址。
@Group
分数
@param.Score.EUDScore
@param.Player.TrgPlayer


@Language.en-US
@Summary
Returns the address of the [Score] of [Player].
@Group
Score
@param.Score.EUDScore
The score type.
@param.Player.TrgPlayer
The target player.
]================================]
function ScoreEPD(Score, Player) -- General 组/EUDScore, TrgPlayer/返回 [Player] 中 [Score] 的地址。
	Player = ParsePlayer(Player)
	ScoreIndex = ParseEUDScore(Score)


	ScoreOffset = GetEUDScoreOffset(ScoreIndex)
	if IsNumber(Player) then
		ScoreOffset = ScoreOffset + Player * 4
		return string.format("EPD(0x%X)", ScoreOffset)
	else
		return string.format("EPD(%s) + %s", ScoreOffset, Player)
	end
end

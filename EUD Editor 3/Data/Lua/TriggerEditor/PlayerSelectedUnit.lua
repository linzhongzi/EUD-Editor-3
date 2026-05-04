--[================================[
@Language.ko-KR
@Summary
[Player]의 [Number]번 선택유닛 오프셋을 반환합니다.
@Group
일반
@param.Player.TrgPlayer
@param.Number.Number


@Language.zh-CN
@Summary
返回 [Player] 的第 [Number] 个选择单位偏移地址。
@Group
普通用户组
@param.Player.TrgPlayer
@param.Number.Number


@Language.en-US
@Summary
Returns the offset of the [Number] th selected unit for [Player].
@Group
General
@param.Player.TrgPlayer
The target player.
@param.Number.Number
The selection number.
]================================]
function SeletionEPD(Player, Number) -- General 组/TrgPlayer, Number/返回 [Player] 的第 [Number] 个选择单位偏移地址。
	Player = ParsePlayer(Player)
	if IsNumber(Player) then
		return string.format("EPD(0x%X) + %s", 0x6284E8 + 0x30 * Player, Number) 
	else
		return string.format("EPD(0x%X) + %s + %s * 0xC", 0x6284E8, Number, Player)
	end
end

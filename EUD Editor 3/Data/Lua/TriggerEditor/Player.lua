--[================================[
@Language.ko-KR
@Summary
[Player]의 아이디를 [ID] [Args]로 변경합니다.
@Group
플레이어
@param.Player.TrgPlayer
@param.ID.FormatString
@param.Args.Arguments

@Language.zh-CN
@Summary
将 [Player] 的 ID 修改为 [ID] [Args].
@Group
玩家
@param.Player.TrgPlayer
@param.ID.FormatString
@param.Args.Arguments


@Language.en-US
@Summary
Changes [Player] 's ID to [ID] [Args].
@Group
Player
@param.Player.TrgPlayer
The target player.
@param.ID.FormatString
The format string for the ID.
@param.Args.Arguments
The arguments.
]================================]
function SetPlayerID(Player, ID, Args) 
-- 玩家/TrgPlayer, FormatString, Arguments/将 [Player] 的 ID 修改为 [ID] [Args].
-- SetPNamef(cp, " [玩家] {:n}", cp);
-- SetPNamef(cp, "{:t} \x07 级别： \x04{} {:c}{:n}", title, level, cp, cp);
	Player = ParsePlayer(Player)
	if Args == "" then
		echo("SetPNamef(" .. Player .. ", \"" .. ID .. "\")")
	else
		echo("SetPNamef(" .. Player .. ", \"" .. ID .. "\", " .. Args .. ")")
	end
end

--[================================[
@Language.ko-KR
@Summary
[Player]의 아이디가 [ID]인지 확인합니다.
@Group
플레이어
@param.Player.TrgPlayer
@param.ID.TrgString


@Language.zh-CN
@Summary
检测 [Player] 的 ID 是否为 [ID] 。
@Group
玩家
@param.Player.TrgPlayer
@param.ID.TrgString


@Language.en-US
@Summary
Checks if [Player] 's ID is [ID].
@Group
Player
@param.Player.TrgPlayer
The target player.
@param.ID.TrgString
The ID string.
]================================]
function PlayerID(Player, ID) -- 玩家/TrgPlayer, TrgString/检测 [Player] 的 ID 是否为 [ID] 。
	--IsPName(player, name)
	Player = ParsePlayer(Player)
	echo("IsPName(" .. Player .. ", \"" .. ID .. "\")")
end

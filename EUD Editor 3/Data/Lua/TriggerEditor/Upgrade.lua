--[================================[
@Language.ko-KR
@Summary
[Player]의 [Upgrade]의 현재값을 [Amount]만큼 [Modifier]합니다.
@Group
업그레이드
@param.Upgrade.Upgrade
@param.Player.TrgPlayer
@param.Modifier.TrgModifier
@param.Amount.Number


@Language.zh-CN
@Summary
将 [Player] 中的 [Upgrade] 的当前值通过 [Modifier] 设置为 [Amount].
@Group
升级
@param.Upgrade.Upgrade
@param.Player.TrgPlayer
@param.Modifier.TrgModifier
@param.Amount.Number


@Language.en-US
@Summary
[Modifier] s the current value of [Upgrade] for [Player] by [Amount].
@Group
Upgrade
@param.Upgrade.Upgrade
The upgrade.
@param.Player.TrgPlayer
The target player.
@param.Modifier.TrgModifier
The modifier.
@param.Amount.Number
The amount.
]================================]
function SetUpgrade(Upgrade, Player, Modifier, Amount) -- Upgrade 组/Upgrade, TrgPlayer, TrgModifier, Number/将 [Player] 中的 [Upgrade] 的当前值通过 [Modifier] 设置为 [Amount].
	Upgrade = ParseUpgrades(Upgrade)
	Player = ParsePlayer(Player)
    Modifier = ParseModifier(Modifier)


	offset = UpgradeOffset(Upgrade, Player)

	if IsNumber(offset) then
		Mod = offset % 4
		ROffset = offset - Mod


    	if Mod == 0 then
    		Mask = "0xFF"
    	elseif Mod == 1 then
    		Mask = "0xFF00"
    	elseif Mod == 2 then
    		Mask = "0xFF0000"
    	elseif Mod == 3 then
    		Mask = "0xFF000000"
    	end


    	if IsNumber(Amount) then
    		rstr = string.format("SetMemoryXEPD(EPD(0x%X), %s, 0x%X, %s)", ROffset, Modifier, Amount * math.pow(256, Mod), Mask)
    	else
			rstr = string.format("SetMemoryXEPD(EPD(0x%X), %s, %s, %s)", ROffset, Modifier, Amount .. " * " .. math.pow(256, Mod), Mask)
    	end

	else
		if Modifier == 7 then
			rstr = string.format("bwrite(%s, %s)", offset, Amount)
		elseif Modifier == 8 then
			rstr = string.format("bwrite(%s, bread(%s) + %s)", offset, offset, Amount)
		elseif Modifier == 9 then
			rstr = string.format("bwrite(%s, bread(%s) - %s)", offset, offset, Amount)
		end
	end
	echo(rstr)
end

--[================================[
@Language.ko-KR
@Summary
[Comparison]: [Player]의 [Upgrade]의 현재값이 [Amount]인지 확인합니다.
@Group
업그레이드
@param.Upgrade.Upgrade
@param.Player.TrgPlayer
@param.Comparison.TrgComparison
@param.Amount.Number


@Language.zh-CN
@Summary
[Comparison]: 确认 [Player] 中 [Upgrade] 的当前值是否为 [Amount] 。
@Group
升级
@param.Upgrade.Upgrade
@param.Player.TrgPlayer
@param.Comparison.TrgComparison
@param.Amount.Number


@Language.en-US
@Summary
[Comparison]: Checks if the current value of [Upgrade] for [Player] is [Amount].
@Group
Upgrade
@param.Upgrade.Upgrade
The upgrade.
@param.Player.TrgPlayer
The target player.
@param.Comparison.TrgComparison
The comparison method.
@param.Amount.Number
The amount.
]================================]
function CurrentUpgrade(Upgrade, Player, Comparison, Amount) -- Upgrade 组/Upgrade, TrgPlayer, TrgComparison, Number/[Comparison]: 确认 [Player] 中 [Upgrade] 的当前值是否为 [Amount] 。
	Upgrade = ParseUpgrades(Upgrade)
	Player = ParsePlayer(Player)
    Comparison = ParseComparison(Comparison)


	offset = UpgradeOffset(Upgrade, Player)

	if IsNumber(offset) then
		Mod = offset % 4
		ROffset = offset - Mod


    	if Mod == 0 then
    		Mask = "0xFF"
    	elseif Mod == 1 then
    		Mask = "0xFF00"
    	elseif Mod == 2 then
    		Mask = "0xFF0000"
    	elseif Mod == 3 then
    		Mask = "0xFF000000"
    	end


    	if IsNumber(Amount) then
    		rstr = string.format("MemoryXEPD(EPD(0x%X), %s, 0x%X, %s)", ROffset, Comparison, Amount * math.pow(256, Mod), Mask)
    	else
			rstr = string.format("MemoryXEPD(EPD(0x%X), %s, %s, %s)", ROffset, Comparison, Amount .. " * " .. math.pow(256, Mod), Mask)
    	end

	else
		if Comparison == 0 then
			rstr = string.format("bread(%s) >= %s", offset, Amount)
		elseif Comparison == 1 then
			rstr = string.format("bread(%s) <= %s", offset, Amount)
		elseif Comparison == 10 then
			rstr = string.format("bread(%s) == %s", offset, Amount)
		end
	end
	echo(rstr)
end

--[================================[
@Language.ko-KR
@Summary
[Player]의 [Upgrade]의 현재값을 반환합니다.
@Group
업그레이드
@param.Upgrade.Upgrade
@param.Player.TrgPlayer


@Language.zh-CN
@Summary
返回 [Player] 中 [Upgrade] 的当前值。
@Group
升级
@param.Upgrade.Upgrade
@param.Player.TrgPlayer


@Language.en-US
@Summary
Returns the current value of [Upgrade] for [Player].
@Group
Upgrade
@param.Upgrade.Upgrade
The upgrade.
@param.Player.TrgPlayer
The target player.
]================================]
function GetUpgrade(Upgrade, Player) -- Upgrade 组/Upgrade, TrgPlayer/返回 [Player] 中 [Upgrade] 的当前值。
	Upgrade = ParseUpgrades(Upgrade)
	Player = ParsePlayer(Player)

	offset = UpgradeOffset(Upgrade, Player)

	if IsNumber(offset) then
		rstr = string.format("bread(0x%X)", offset)
	else
		rstr = string.format("bread(%s)", offset)
	end
	echo(rstr)
end

--[================================[
@Language.ko-KR
@Summary
[Player]의 [Upgrade]의 현재값 주소를 반환합니다.
@Group
업그레이드
@param.Upgrade.Upgrade
@param.Player.TrgPlayer


@Language.zh-CN
@Summary
返回 [Player] 中 [Upgrade] 的当前值的地址。
@Group
升级
@param.Upgrade.Upgrade
@param.Player.TrgPlayer


@Language.en-US
@Summary
Returns the address of the current value of [Upgrade] for [Player].
@Group
Upgrade
@param.Upgrade.Upgrade
The upgrade.
@param.Player.TrgPlayer
The target player.
]================================]
function UpgradeOffset(Upgrade, Player) 
-- Upgrade 组/Upgrade, TrgPlayer/返回 [Player] 中 [Upgrade] 的当前值的地址。
-- General 组/58D2B0 0~45
-- General 组/58F32C 46
	Upgrade = ParseUpgrades(Upgrade) + 0 
	Player = ParsePlayer(Player)

	Size = 0
	if Upgrade <= 45 then
		offset = 0x58D2B0
		Size = 46
	else
		offset = 0x58F32C
		Size = 15
		Upgrade = Upgrade - 46
	end
	if IsNumber(Player) then
		offset = offset + Player * Size + Upgrade
		return offset
	else
		offset = string.format("0x%X + %s * %s + %s", offset, Player, Size, Upgrade)
		return offset
	end
end

--[================================[
@Language.ko-KR
@Summary
[Player]의 [Upgrade]의 최대값을 [Amount]만큼 [Modifier]합니다.
@Group
업그레이드
@param.Upgrade.Upgrade
@param.Player.TrgPlayer
@param.Modifier.TrgModifier
@param.Amount.Number


@Language.zh-CN
@Summary
将 [Player] 的 [Upgrade] 的最大值 [Modifier] 为 [Amount] 。
@Group
升级
@param.Upgrade.Upgrade
@param.Player.TrgPlayer
@param.Modifier.TrgModifier
@param.Amount.Number


@Language.en-US
@Summary
[Modifier] s the maximum value of [Upgrade] for [Player] by [Amount].
@Group
Upgrade
@param.Upgrade.Upgrade
The upgrade.
@param.Player.TrgPlayer
The target player.
@param.Modifier.TrgModifier
The modifier.
@param.Amount.Number
The amount.
]================================]
function SetUpgradeMax(Upgrade, Player, Modifier, Amount) -- Upgrade 组/Upgrade,TrgPlayer,TrgModifier,Number/将 [Player] 的 [Upgrade] 的最大值 [Modifier] 为 [Amount] 。
	Upgrade = ParseUpgrades(Upgrade)
	Player = ParsePlayer(Player)
    Modifier = ParseModifier(Modifier)


	offset = UpgradeOffsetMax(Upgrade, Player)

	if IsNumber(offset) then
		Mod = offset % 4
		ROffset = offset - Mod


    	if Mod == 0 then
    		Mask = "0xFF"
    	elseif Mod == 1 then
    		Mask = "0xFF00"
    	elseif Mod == 2 then
    		Mask = "0xFF0000"
    	elseif Mod == 3 then
    		Mask = "0xFF000000"
    	end


    	if IsNumber(Amount) then
    		rstr = string.format("SetMemoryXEPD(EPD(0x%X), %s, 0x%X, %s)", ROffset, Modifier, Amount * math.pow(256, Mod), Mask)
    	else
			rstr = string.format("SetMemoryXEPD(EPD(0x%X), %s, %s, %s)", ROffset, Modifier, Amount .. " * " .. math.pow(256, Mod), Mask)
    	end

	else
		if Modifier == 7 then
			rstr = string.format("bwrite(%s, %s)", offset, Amount)
		elseif Modifier == 8 then
			rstr = string.format("bwrite(%s, bread(%s) + %s)", offset, offset, Amount)
		elseif Modifier == 9 then
			rstr = string.format("bwrite(%s, bread(%s) - %s)", offset, offset, Amount)
		end
	end
	echo(rstr)
end

--[================================[
@Language.ko-KR
@Summary
[Comparison]: [Player]의 [Upgrade]의 최대값이 [Amount]인지 확인합니다.
@Group
업그레이드
@param.Upgrade.Upgrade
@param.Player.TrgPlayer
@param.Comparison.TrgComparison
@param.Amount.Number


@Language.zh-CN
@Summary
[Comparison]: 确认 [Player] 的 [Upgrade] 的最大值是否为 [Amount] 。
@Group
升级
@param.Upgrade.Upgrade
@param.Player.TrgPlayer
@param.Comparison.TrgComparison
@param.Amount.Number


@Language.en-US
@Summary
[Comparison]: Checks if the maximum value of [Upgrade] for [Player] is [Amount].
@Group
Upgrade
@param.Upgrade.Upgrade
The upgrade.
@param.Player.TrgPlayer
The target player.
@param.Comparison.TrgComparison
The comparison method.
@param.Amount.Number
The amount.
]================================]
function CurrentUpgradeMax(Upgrade, Player, Comparison, Amount) -- Upgrade 组/Upgrade, TrgPlayer, TrgComparison, Number/[Comparison]: 确认 [Player] 的 [Upgrade] 的最大值是否为 [Amount] 。
	Upgrade = ParseUpgrades(Upgrade)
	Player = ParsePlayer(Player)
    Comparison = ParseComparison(Comparison)


	offset = UpgradeOffsetMax(Upgrade, Player)

	if IsNumber(offset) then
		Mod = offset % 4
		ROffset = offset - Mod


    	if Mod == 0 then
    		Mask = "0xFF"
    	elseif Mod == 1 then
    		Mask = "0xFF00"
    	elseif Mod == 2 then
    		Mask = "0xFF0000"
    	elseif Mod == 3 then
    		Mask = "0xFF000000"
    	end


    	if IsNumber(Amount) then
    		rstr = string.format("MemoryXEPD(EPD(0x%X), %s, 0x%X, %s)", ROffset, Comparison, Amount * math.pow(256, Mod), Mask)
    	else
			rstr = string.format("MemoryXEPD(EPD(0x%X), %s, %s, %s)", ROffset, Comparison, Amount .. " * " .. math.pow(256, Mod), Mask)
    	end

	else
		if Comparison == 0 then
			rstr = string.format("bread(%s) >= %s", offset, Amount)
		elseif Comparison == 1 then
			rstr = string.format("bread(%s) <= %s", offset, Amount)
		elseif Comparison == 10 then
			rstr = string.format("bread(%s) == %s", offset, Amount)
		end
	end
	echo(rstr)
end

--[================================[
@Language.ko-KR
@Summary
[Player]의 [Upgrade]의 최대값을 반환합니다.
@Group
업그레이드
@param.Upgrade.Upgrade
@param.Player.TrgPlayer


@Language.zh-CN
@Summary
返回 [Player] 的 [Upgrade] 的最大值。
@Group
升级
@param.Upgrade.Upgrade
@param.Player.TrgPlayer


@Language.en-US
@Summary
Returns the maximum value of [Upgrade] for [Player].
@Group
Upgrade
@param.Upgrade.Upgrade
The upgrade.
@param.Player.TrgPlayer
The target player.
]================================]
function GetUpgradeMax(Upgrade, Player) -- Upgrade 组/Upgrade, TrgPlayer/返回 [Player] 的 [Upgrade] 的最大值。
	Upgrade = ParseUpgrades(Upgrade)
	Player = ParsePlayer(Player)

	offset = UpgradeOffsetMax(Upgrade, Player)

	if IsNumber(offset) then
		rstr = string.format("bread(0x%X)", offset)
	else
		rstr = string.format("bread(%s)", offset)
	end
	echo(rstr)
end

--[================================[
@Language.ko-KR
@Summary
[Player]의 [Upgrade]의 현재값 주소를 반환합니다.
@Group
업그레이드
@param.Upgrade.Upgrade
@param.Player.TrgPlayer


@Language.zh-CN
@Summary
返回 [Player] 中 [Upgrade] 的当前值的地址。
@Group
升级
@param.Upgrade.Upgrade
@param.Player.TrgPlayer


@Language.en-US
@Summary
Returns the address of the current value of [Upgrade] for [Player].
@Group
Upgrade
@param.Upgrade.Upgrade
The upgrade.
@param.Player.TrgPlayer
The target player.
]================================]
function UpgradeOffsetMax(Upgrade, Player) 
-- Upgrade 组/Upgrade,TrgPlayer/返回 [Player] 中 [Upgrade] 的当前值的地址。
-- General/58D088 0 ~ 45
-- General/58F278 46
	Upgrade = ParseUpgrades(Upgrade) + 0
	Player = ParsePlayer(Player)

	Size = 0
	if Upgrade <= 45 then
		offset = 0x58D088
		Size = 46
	else
		offset = 0x58F278
		Size = 15
		Upgrade = Upgrade - 46
	end


	if IsNumber(Player) then
		offset = offset + Player * Size + Upgrade
		return offset
	else
		offset = string.format("0x%X + %s * %s + %s", offset, Player, Size, Upgrade)
		return offset
	end
end

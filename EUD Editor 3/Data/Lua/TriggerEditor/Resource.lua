--[================================[
@Language.ko-KR
@Summary
[Player]의 [ResourceType]을 [Amount]만큼 [Modifier]합니다.
@Group
자원
@param.Player.TrgPlayer
@param.Modifier.TrgModifier
@param.Amount.Number
@param.ResourceType.TrgResource


@Language.zh-CN
@Summary
将 [Player] 中的 [ResourceType] 值 [Modifier] 为 [Amount] 。
@Group
资源
@param.Player.TrgPlayer
@param.Modifier.TrgModifier
@param.Amount.Number
@param.ResourceType.TrgResource


@Language.en-US
@Summary
[Modifier] s the [ResourceType] of [Player] by [Amount].
@Group
Resource
@param.Player.TrgPlayer
The target player.
@param.Modifier.TrgModifier
The modifier.
@param.Amount.Number
The amount.
@param.ResourceType.TrgResource
The resource type.
]================================]
function SetResource(Player, Modifier, Amount, ResourceType) -- General 组/TrgPlayer, TrgModifier, Number, TrgResource/将 [Player] 中的 [ResourceType] 值 [Modifier] 为 [Amount] 。
	Player = ParsePlayer(Player)
    Modifier = ParseModifier(Modifier)
    ResourceType = ParseResource(ResourceType)
    Offset = ResourceEPD(Player, ResourceType)
	echo(string.format("SetMemoryEPD(%s, %s, %s)", Offset, Modifier, Amount))
end

--[================================[
@Language.ko-KR
@Summary
[Player]의 [ResourceType]을 읽습니다.
@Group
자원
@param.Player.TrgPlayer
@param.ResourceType.TrgResource


@Language.zh-CN
@Summary
从 [Player] 读取 [ResourceType].
@Group
资源
@param.Player.TrgPlayer
@param.ResourceType.TrgResource


@Language.en-US
@Summary
Reads the [ResourceType] of [Player].
@Group
Resource
@param.Player.TrgPlayer
The target player.
@param.ResourceType.TrgResource
The resource type.
]================================]
function GetResource(Player, ResourceType) -- General 组/TrgPlayer, TrgResource/从 [Player] 读取 [ResourceType].
    Player = ParsePlayer(Player)
    Offset = ResourceEPD(Player, ResourceType)
    ResourceType = ParseResource(ResourceType)

    
	echo(string.format("dwread_epd(%s)", Offset))
end

--[================================[
@Language.ko-KR
@Summary
[Player]의 [Resource]의 주소를 반환합니다.
@Group
자원
@param.Player.TrgPlayer
@param.ResourceType.TrgResource


@Language.zh-CN
@Summary
返回 [Player] 中 [Resource] 的地址。
@Group
资源
@param.Player.TrgPlayer
@param.ResourceType.TrgResource


@Language.en-US
@Summary
Returns the address of the [Resource] of [Player].
@Group
Resource
@param.Player.TrgPlayer
The target player.
@param.ResourceType.TrgResource
The resource type.
]================================]
function ResourceEPD(Player, ResourceType) -- General 组/TrgPlayer, TrgResource/返回 [Player] 中 [Resource] 的地址。
    Player = ParsePlayer(Player)
	ResourceType = ParseResource(ResourceType)


	if ResourceType == 0 then
		-- Ore
		Offset = 0x57F0F0
	elseif ResourceType == 1 then
		-- Gas
		Offset = 0x57F120
	end


	if IsNumber(Player) then
		Offset = Offset + Player * 4
		return string.format("EPD(0x%X)", Offset)
	else
		return string.format("EPD(0x%X) + %s", Offset, Player)
	end
end

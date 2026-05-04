--[================================[
@Language.ko-KR
@Summary
[Index]의 [DatType]의 값을 [Value]로 [Modifier]합니다.
@Group
DatFile
@param.DatType.UnitsDat
@param.Index.TrgUnit
@param.Value.Number
@param.Modifier.TrgModifier


@Language.zh-CN
@Summary
[Modifier] 索引为 [Index] , 类型为 [DatType] 的参数值为 [Value].
@Group
DatFile
@param.DatType.UnitsDat
@param.Index.TrgUnit
@param.Value.Number
@param.Modifier.TrgModifier


@Language.en-US
@Summary
[Modifier] s the value of [DatType] at [Index] by [Value].
@Group
DatFile
@param.DatType.UnitsDat
The dat type.
@param.Index.TrgUnit
The index.
@param.Value.Number
The value.
@param.Modifier.TrgModifier
The modifier.
]================================]
function SetUnitsDat(DatType, Index, Value, Modifier) -- DatFile 组/UnitsDat, TrgUnit, Number, TrgModifier/[Modifier] 索引为 [Index] , 类型为 [DatType] 的参数值为 [Value].
	Unit = ParseUnit(Index)
	Modifier = ParseModifier(Modifier)
    str = SetDatFile("units", DatType, Unit, Value, Modifier)
	echo(str)
end

--[================================[
@Language.ko-KR
@Summary
[Index]의 [DatType]의 값을 [Value]로 [Modifier]합니다.
@Group
DatFile
@param.DatType.WeaponsDat
@param.Index.Weapon
@param.Value.Number
@param.Modifier.TrgModifier


@Language.zh-CN
@Summary
[Modifier] 索引为 [Index] , 类型为 [DatType] 的参数值为 [Value].
@Group
DatFile
@param.DatType.WeaponsDat
@param.Index.Weapon
@param.Value.Number
@param.Modifier.TrgModifier


@Language.en-US
@Summary
[Modifier] s the value of [DatType] at [Index] by [Value].
@Group
DatFile
@param.DatType.WeaponsDat
The dat type.
@param.Index.Weapon
The index.
@param.Value.Number
The value.
@param.Modifier.TrgModifier
The modifier.
]================================]
function SetWeaponsDat(DatType, Index, Value, Modifier) -- DatFile 组/WeaponsDat, Weapon, Number, TrgModifier/[Modifier] 索引为 [Index] , 类型为 [DatType] 的参数值为 [Value].
	Weapon = ParseWeapon(Index)
	Modifier = ParseModifier(Modifier)
    str = SetDatFile("weapons", DatType, Weapon, Value, Modifier)
	echo(str)
end

--[================================[
@Language.ko-KR
@Summary
[Index]의 [DatType]의 값을 [Value]로 [Modifier]합니다.
@Group
DatFile
@param.DatType.FlingyDat
@param.Index.Flingy
@param.Value.Number
@param.Modifier.TrgModifier


@Language.zh-CN
@Summary
[Modifier] 索引为 [Index] , 类型为 [DatType] 的参数值为 [Value].
@Group
DatFile
@param.DatType.FlingyDat
@param.Index.Flingy
@param.Value.Number
@param.Modifier.TrgModifier


@Language.en-US
@Summary
[Modifier] s the value of [DatType] at [Index] by [Value].
@Group
DatFile
@param.DatType.FlingyDat
The dat type.
@param.Index.Flingy
The index.
@param.Value.Number
The value.
@param.Modifier.TrgModifier
The modifier.
]================================]
function SetFlingyDat(DatType, Index, Value, Modifier) -- DatFile 组/FlingyDat, Flingy, Number, TrgModifier/[Modifier] 索引为 [Index] , 类型为 [DatType] 的参数值为 [Value].
	Flingy = ParseFlingy(Index)
	Modifier = ParseModifier(Modifier)
    str = SetDatFile("flingy", DatType, Flingy, Value, Modifier)
	echo(str)
end

--[================================[
@Language.ko-KR
@Summary
[Index]의 [DatType]의 값을 [Value]로 [Modifier]합니다.
@Group
DatFile
@param.DatType.SpritesDat
@param.Index.Sprite
@param.Value.Number
@param.Modifier.TrgModifier


@Language.zh-CN
@Summary
[Modifier] 索引为 [Index] , 类型为 [DatType] 的参数值为 [Value].
@Group
DatFile
@param.DatType.SpritesDat
@param.Index.Sprite
@param.Value.Number
@param.Modifier.TrgModifier


@Language.en-US
@Summary
[Modifier] s the value of [DatType] at [Index] by [Value].
@Group
DatFile
@param.DatType.SpritesDat
The dat type.
@param.Index.Sprite
The index.
@param.Value.Number
The value.
@param.Modifier.TrgModifier
The modifier.
]================================]
function SetSpritesDat(DatType, Index, Value, Modifier) -- DatFile 组/SpritesDat, Sprite, Number, TrgModifier/[Modifier] 索引为 [Index] , 类型为 [DatType] 的参数值为 [Value].
	Sprite = ParseSprites(Index)
	Modifier = ParseModifier(Modifier)
    str = SetDatFile("sprites", DatType, Sprite, Value, Modifier)
	echo(str)
end

--[================================[
@Language.ko-KR
@Summary
[Index]의 [DatType]의 값을 [Value]로 [Modifier]합니다.
@Group
DatFile
@param.DatType.ImagesDat
@param.Index.Image
@param.Value.Number
@param.Modifier.TrgModifier


@Language.zh-CN
@Summary
[Modifier] 索引为 [Index] , 类型为 [DatType] 的参数值为 [Value].
@Group
DatFile
@param.DatType.ImagesDat
@param.Index.Image
@param.Value.Number
@param.Modifier.TrgModifier


@Language.en-US
@Summary
[Modifier] s the value of [DatType] at [Index] by [Value].
@Group
DatFile
@param.DatType.ImagesDat
The dat type.
@param.Index.Image
The index.
@param.Value.Number
The value.
@param.Modifier.TrgModifier
The modifier.
]================================]
function SetImagesDat(DatType, Index, Value, Modifier) -- DatFile 组/ImagesDat, Image, Number, TrgModifier/[Modifier] 索引为 [Index] , 类型为 [DatType] 的参数值为 [Value].
	Image = ParseImages(Index)
	Modifier = ParseModifier(Modifier)
    str = SetDatFile("images", DatType, Image, Value, Modifier)
	echo(str)
end

--[================================[
@Language.ko-KR
@Summary
[Index]의 [DatType]의 값을 [Value]로 [Modifier]합니다.
@Group
DatFile
@param.DatType.UpgradesDat
@param.Index.Upgrade
@param.Value.Number
@param.Modifier.TrgModifier


@Language.zh-CN
@Summary
[Modifier] 索引为 [Index] , 类型为 [DatType] 的参数值为 [Value].
@Group
DatFile
@param.DatType.UpgradesDat
@param.Index.Upgrade
@param.Value.Number
@param.Modifier.TrgModifier


@Language.en-US
@Summary
[Modifier] s the value of [DatType] at [Index] by [Value].
@Group
DatFile
@param.DatType.UpgradesDat
The dat type.
@param.Index.Upgrade
The index.
@param.Value.Number
The value.
@param.Modifier.TrgModifier
The modifier.
]================================]
function SetUpgradesDat(DatType, Index, Value, Modifier) -- DatFile 组/UpgradesDat, Upgrade, Number, TrgModifier/[Modifier] 索引为 [Index] , 类型为 [DatType] 的参数值为 [Value].
	Upgrade = ParseUpgrades(Index)
	Modifier = ParseModifier(Modifier)
    str = SetDatFile("upgrades", DatType, Upgrade, Value, Modifier)
	echo(str)
end

--[================================[
@Language.ko-KR
@Summary
[Index]의 [DatType]의 값을 [Value]로 [Modifier]합니다.
@Group
DatFile
@param.DatType.TechdataDat
@param.Index.Tech
@param.Value.Number
@param.Modifier.TrgModifier


@Language.zh-CN
@Summary
[Modifier] 索引为 [Index] , 类型为 [DatType] 的参数值为 [Value].
@Group
DatFile
@param.DatType.TechdataDat
@param.Index.Tech
@param.Value.Number
@param.Modifier.TrgModifier


@Language.en-US
@Summary
[Modifier] s the value of [DatType] at [Index] by [Value].
@Group
DatFile
@param.DatType.TechdataDat
The dat type.
@param.Index.Tech
The index.
@param.Value.Number
The value.
@param.Modifier.TrgModifier
The modifier.
]================================]
function SetTechdataDat(DatType, Index, Value, Modifier) -- DatFile 组/TechdataDat, Tech, Number, TrgModifier/[Modifier] 索引为 [Index] , 类型为 [DatType] 的参数值为 [Value].
	Tech = ParseTechdata(Index)
	Modifier = ParseModifier(Modifier)
    str = SetDatFile("techdata", DatType, Tech, Value, Modifier)
	echo(str)
end

--[================================[
@Language.ko-KR
@Summary
[Index]의 [DatType]의 값을 [Value]로 [Modifier]합니다.
@Group
DatFile
@param.DatType.OrdersDat
@param.Index.Order
@param.Value.Number
@param.Modifier.TrgModifier


@Language.zh-CN
@Summary
[Modifier] 索引为 [Index] , 类型为 [DatType] 的参数值为 [Value].
@Group
DatFile
@param.DatType.OrdersDat
@param.Index.Order
@param.Value.Number
@param.Modifier.TrgModifier


@Language.en-US
@Summary
[Modifier] s the value of [DatType] at [Index] by [Value].
@Group
DatFile
@param.DatType.OrdersDat
The dat type.
@param.Index.Order
The index.
@param.Value.Number
The value.
@param.Modifier.TrgModifier
The modifier.
]================================]
function SetOrdersDat(DatType, Index, Value, Modifier) -- DatFile 组/OrdersDat, Order, Number, TrgModifier/[Modifier] 索引为 [Index] , 类型为 [DatType] 的参数值为 [Value].
	Order = ParseOrder(Index)
	Modifier = ParseModifier(Modifier)
    str = SetDatFile("orders", DatType, Order, Value, Modifier)
	echo(str)
end

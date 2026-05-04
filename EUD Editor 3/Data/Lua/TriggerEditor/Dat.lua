--[================================[
@Language.ko-KR
@Summary
[Index]의 [DatType]의 값이 [Comparison] [Value]인지 판단합니다.
@Group
DatFile
@param.DatType.UnitsDat
비교할 파라미터입니다.
@param.Index.TrgUnit
비교경할 오브젝트입니다.
@param.Value.Number
값입니다.
@param.Comparison.TrgComparison
비교 방식입니다.

@Language.zh-CN
@Summary
[Comparison]: 比较指定 [Index] 和 [DatType] 的对象是否为值 [Value].
@Group
DatFile
@param.DatType.UnitsDat
要比较的参数类型。
@param.Index.TrgUnit
要比较的对象索引。
@param.Value.Number
比较值。
@param.Comparison.TrgComparison
比较函数。


@Language.en-US
@Summary
[Comparison]: Determines if the value of [DatType] at [Index] is [Value].
@Group
DatFile
@param.DatType.UnitsDat
The parameter to compare.
@param.Index.TrgUnit
The object to compare.
@param.Value.Number
The value.
@param.Comparison.TrgComparison
The comparison method.
]================================]
function UnitsDat(DatType, Index, Value, Comparison) -- DatFile 组/UnitsDat, TrgUnit, Number, TrgComparison/比较 [Comparison]: 索引为 [Index] , 类型为 [DatType] 的参数是否为值 [Value].
	Unit = ParseUnit(Index)
	Comparison = ParseComparison(Comparison)
    str = ConditionDatFile("units", DatType, Unit, Value, Comparison)
	echo(str)
end

--[================================[
@Language.ko-KR
@Summary
[Index]의 [DatType]의 값이 [Comparison] [Value]인지 판단합니다.
@Group
DatFile
@param.DatType.WeaponsDat
비교할 파라미터입니다.
@param.Index.Weapon
비교경할 오브젝트입니다.
@param.Value.Number
값입니다.
@param.Comparison.TrgComparison
비교 방식입니다.

@Language.zh-CN
@Summary
[Comparison]: 比较指定 [Index] 和 [DatType] 的对象是否为值 [Value].
@Group
DatFile
@param.DatType.WeaponsDat
要比较的参数类型。
@param.Index.Weapon
要比较的对象索引。
@param.Value.Number
比较值。
@param.Comparison.TrgComparison
比较函数。


@Language.en-US
@Summary
[Comparison]: Determines if the value of [DatType] at [Index] is [Value].
@Group
DatFile
@param.DatType.WeaponsDat
The parameter to compare.
@param.Index.Weapon
The object to compare.
@param.Value.Number
The value.
@param.Comparison.TrgComparison
The comparison method.
]================================]
function WeaponsDat(DatType, Index, Value, Comparison) -- DatFile 组/WeaponsDat, Weapon, Number, TrgComparison/[Comparison]: 比较指定 [Index] 和 [DatType] 的对象是否为值 [Value].
	Weapon = ParseWeapon(Index)
	Comparison = ParseComparison(Comparison)
    str = ConditionDatFile("weapons", DatType, Weapon, Value, Comparison)
	echo(str)
end

--[================================[
@Language.ko-KR
@Summary
[Index]의 [DatType]의 값이 [Comparison] [Value]인지 판단합니다.
@Group
DatFile
@param.DatType.FlingyDat
비교할 파라미터입니다.
@param.Index.Flingy
비교경할 오브젝트입니다.
@param.Value.Number
값입니다.
@param.Comparison.TrgComparison
비교 방식입니다.

@Language.zh-CN
@Summary
[Comparison]: 比较指定 [Index] 和 [DatType] 的对象是否为值 [Value].
@Group
DatFile
@param.DatType.FlingyDat
要比较的参数类型。
@param.Index.Flingy
要比较的对象索引。
@param.Value.Number
比较值。
@param.Comparison.TrgComparison
比较函数。


@Language.en-US
@Summary
[Comparison]: Determines if the value of [DatType] at [Index] is [Value].
@Group
DatFile
@param.DatType.FlingyDat
The parameter to compare.
@param.Index.Flingy
The object to compare.
@param.Value.Number
The value.
@param.Comparison.TrgComparison
The comparison method.
]================================]
function FlingyDat(DatType, Index, Value, Comparison) -- DatFile 组/FlingyDat, Flingy, Number, TrgComparison/[Comparison]: 比较指定 [Index] 和 [DatType] 的对象是否为值 [Value].
	Flingy = ParseFlingy(Index)
	Comparison = ParseComparison(Comparison)
    str = ConditionDatFile("flingy", DatType, Flingy, Value, Comparison)
	echo(str)
end

--[================================[
@Language.zh-CN
@Language.ko-KR
@Summary
[Index]의 [DatType]의 값이 [Comparison] [Value]인지 판단합니다.
@Group
DatFile
@param.DatType.SpritesDat
비교할 파라미터입니다.
@param.Index.Sprite
비교경할 오브젝트입니다.
@param.Value.Number
값입니다.
@param.Comparison.TrgComparison
비교 방식입니다.

@Summary
[Comparison]: 比较指定 [Index] 和 [DatType] 的对象是否为值 [Value].
@Group
DatFile
@param.DatType.SpritesDat
要比较的参数类型。
@param.Index.Sprite
要比较的对象索引。
@param.Value.Number
比较值。
@param.Comparison.TrgComparison
比较函数。


@Language.en-US
@Summary
[Comparison]: Determines if the value of [DatType] at [Index] is [Value].
@Group
DatFile
@param.DatType.SpritesDat
The parameter to compare.
@param.Index.Sprite
The object to compare.
@param.Value.Number
The value.
@param.Comparison.TrgComparison
The comparison method.
]================================]
function SpritesDat(DatType, Index, Value, Comparison) -- DatFile 组/SpritesDat, Sprite, Number, TrgComparison/[Comparison]: 比较指定 [Index] 和 [DatType] 的对象是否为值 [Value].
	Sprite = ParseSprites(Index)
	Comparison = ParseComparison(Comparison)
    str = ConditionDatFile("sprites", DatType, Sprite, Value, Comparison)
	echo(str)
end

--[================================[
@Language.ko-KR
@Summary
[Index]의 [DatType]의 값이 [Comparison] [Value]인지 판단합니다.
@Group
DatFile
@param.DatType.ImagesDat
비교할 파라미터입니다.
@param.Index.Image
비교경할 오브젝트입니다.
@param.Value.Number
값입니다.
@param.Comparison.TrgComparison
비교 방식입니다.

@Language.zh-CN
@Summary
[Comparison]: 比较指定 [Index] 和 [DatType] 的对象是否为值 [Value].
@Group
DatFile
@param.DatType.ImagesDat
要比较的参数类型。
@param.Index.Image
要比较的对象索引。
@param.Value.Number
比较值。
@param.Comparison.TrgComparison
比较函数。


@Language.en-US
@Summary
[Comparison]: Determines if the value of [DatType] at [Index] is [Value].
@Group
DatFile
@param.DatType.ImagesDat
The parameter to compare.
@param.Index.Image
The object to compare.
@param.Value.Number
The value.
@param.Comparison.TrgComparison
The comparison method.
]================================]
function ImagesDat(DatType, Index, Value, Comparison) -- DatFile 组/ImagesDat, Image, Number, TrgComparison/[Comparison]: 比较指定 [Index] 和 [DatType] 的对象是否为值 [Value].
	Image = ParseImages(Index)
	Comparison = ParseComparison(Comparison)
    str = ConditionDatFile("images", DatType, Image, Value, Comparison)
	echo(str)
end

--[================================[
@Language.ko-KR
@Summary
[Index]의 [DatType]의 값이 [Comparison] [Value]인지 판단합니다.
@Group
DatFile
@param.DatType.UpgradesDat
비교할 파라미터입니다.
@param.Index.Upgrade
비교경할 오브젝트입니다.
@param.Value.Number
값입니다.
@param.Comparison.TrgComparison
비교 방식입니다.

@Language.zh-CN
@Summary
[Comparison]: 比较指定 [Index] 和 [DatType] 的对象是否为值 [Value].
@Group
DatFile
@param.DatType.UpgradesDat
要比较的参数类型。
@param.Index.Upgrade
要比较的对象索引。
@param.Value.Number
比较值。
@param.Comparison.TrgComparison
比较函数。


@Language.en-US
@Summary
[Comparison]: Determines if the value of [DatType] at [Index] is [Value].
@Group
DatFile
@param.DatType.UpgradesDat
The parameter to compare.
@param.Index.Upgrade
The object to compare.
@param.Value.Number
The value.
@param.Comparison.TrgComparison
The comparison method.
]================================]
function UpgradesDat(DatType, Index, Value, Comparison) -- DatFile 组/UpgradesDat, Upgrade, Number, TrgComparison/[Comparison]: 比较指定 [Index] 和 [DatType] 的对象是否为值 [Value].
	Upgrade = ParseUpgrades(Index)
	Comparison = ParseComparison(Comparison)
    str = ConditionDatFile("upgrades", DatType, Upgrade, Value, Comparison)
	echo(str)
end

--[================================[
@Language.ko-KR
@Summary
[Index]의 [DatType]의 값이 [Comparison] [Value]인지 판단합니다.
@Group
DatFile
@param.DatType.TechdataDat
비교할 파라미터입니다.
@param.Index.Tech
비교경할 오브젝트입니다.
@param.Value.Number
값입니다.
@param.Comparison.TrgComparison
비교 방식입니다.

@Language.zh-CN
@Summary
[Comparison]: 比较指定 [Index] 和 [DatType] 的对象是否为值 [Value].
@Group
DatFile
@param.DatType.TechdataDat
要比较的参数类型。
@param.Index.Tech
要比较的对象索引。
@param.Value.Number
比较值。
@param.Comparison.TrgComparison
比较函数。


@Language.en-US
@Summary
[Comparison]: Determines if the value of [DatType] at [Index] is [Value].
@Group
DatFile
@param.DatType.TechdataDat
The parameter to compare.
@param.Index.Tech
The object to compare.
@param.Value.Number
The value.
@param.Comparison.TrgComparison
The comparison method.
]================================]
function TechdataDat(DatType, Index, Value, Comparison) -- DatFile 组/TechdataDat, Tech, Number, TrgComparison/[Comparison]: 比较指定 [Index] 和 [DatType] 的对象是否为值 [Value].
	Tech = ParseTechData(Index)
	Comparison = ParseComparison(Comparison)
    str = ConditionDatFile("techdata", DatType, Tech, Value, Comparison)
	echo(str)
end

--[================================[
@Language.ko-KR
@Summary
[Index]의 [DatType]의 값이 [Comparison] [Value]인지 판단합니다.
@Group
DatFile
@param.DatType.OrdersDat
비교할 파라미터입니다.
@param.Index.Order
비교경할 오브젝트입니다.
@param.Value.Number
값입니다.
@param.Comparison.TrgComparison
비교 방식입니다.

@Language.zh-CN
@Summary
[Comparison]: 比较指定 [Index] 和 [DatType] 的对象是否为值 [Value].
@Group
DatFile
@param.DatType.OrdersDat
要比较的参数类型。
@param.Index.Order
要比较的对象索引。
@param.Value.Number
比较值。
@param.Comparison.TrgComparison
比较函数。


@Language.en-US
@Summary
[Comparison]: Determines if the value of [DatType] at [Index] is [Value].
@Group
DatFile
@param.DatType.OrdersDat
The parameter to compare.
@param.Index.Order
The object to compare.
@param.Value.Number
The value.
@param.Comparison.TrgComparison
The comparison method.
]================================]
function OrdersDat(DatType, Index, Value, Comparison) -- DatFile 组/OrdersDat, Order, Number, TrgComparison/[Comparison]: 比较指定 [Index] 和 [DatType] 的对象是否为值 [Value].
	Order = ParseOrder(Index)
	Comparison = ParseComparison(Comparison)
    str = ConditionDatFile("orders", DatType, Order, Value, Comparison)
	echo(str)
end

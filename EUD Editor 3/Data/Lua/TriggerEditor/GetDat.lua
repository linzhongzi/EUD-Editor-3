--[================================[
@Language.ko-KR
@Summary
[Index]의 [DatType] 값을 반환합니다.
@Group
DatFile
@param.DatType.UnitsDat
파라미터입니다.
@param.Index.TrgUnit
대상 유닛입니다.


@Language.zh-CN
@Summary
返回指定 [Index] 的单位的 [DatType] 值。
@Group
DatFile
@param.DatType.UnitsDat
单位类型参数。
@param.Index.TrgUnit
这是目标单位。


@Language.en-US
@Summary
Returns the value of [DatType] at [Index].
@Group
DatFile
@param.DatType.UnitsDat
The parameter.
@param.Index.TrgUnit
The target unit.
]================================]
function GetUnitsDat(DatType, Index) -- DatFile 组/UnitsDat, TrgUnit/返回指定 [Index] 和 [DatType] 的单位的值。
	Unit = ParseUnit(Index)
    str = GetDatFile("units", DatType, Unit)
	echo(str)
end

--[================================[
@Language.ko-KR
@Summary
[Index]의 [DatType] 값을 반환합니다.
@Group
DatFile
@param.DatType.WeaponsDat
파라미터입니다.
@param.Index.Weapon
대상 무기입니다.

@Language.zh-CN
@Summary
返回指定 [Index] 的单位的 [DatType] 值。
@Group
DatFile
@param.DatType.WeaponsDat
单位类型参数。
@param.Index.Weapon
目标武器。


@Language.en-US
@Summary
Returns the value of [DatType] at [Index].
@Group
DatFile
@param.DatType.WeaponsDat
The parameter.
@param.Index.Weapon
The target weapon.
]================================]
function GetWeaponsDat(DatType, Index) -- 返回 DatFile/WeaponsDat, Weapon/返回指定 [Index] 的单位的 [DatType] 值。
	Weapon = ParseWeapon(Index)
    str = GetDatFile("weapons", DatType, Weapon)
	echo(str)
end

--[================================[
@Language.ko-KR
@Summary
[Index]의 [DatType] 값을 반환합니다.
@Group
DatFile
@param.DatType.FlingyDat
파라미터입니다.
@param.Index.Flingy
대상 비행정보입니다.

@Language.zh-CN
@Summary
返回指定 [Index] 的单位的 [DatType] 值。
@Group
DatFile
@param.DatType.FlingyDat
单位类型参数。
@param.Index.Flingy
目标单位索引类型(Flingy)


@Language.en-US
@Summary
Returns the value of [DatType] at [Index].
@Group
DatFile
@param.DatType.FlingyDat
The parameter.
@param.Index.Flingy
The target flingy.
]================================]
function GetFlingyDat(DatType, Index) -- DatFile 组/FlingyDat, Flingy/返回指定 [Index] 的单位的 [DatType] 值。
	Flingy = ParseFlingy(Index)
    str = GetDatFile("flingy", DatType, Flingy)
	echo(str)
end

--[================================[
@Language.ko-KR
@Summary
[Index]의 [DatType] 값을 반환합니다.
@Group
DatFile
@param.DatType.SpritesDat
파라미터입니다.
@param.Index.Sprite
대상 스프라이트입니다.


@Language.zh-CN
@Summary
返回指定 [Index] 的单位的 [DatType] 值。
@Group
DatFile
@param.DatType.SpritesDat
单位类型参数。
@param.Index.Sprite
目标单位索引类型(Sprite)。


@Language.en-US
@Summary
Returns the value of [DatType] at [Index].
@Group
DatFile
@param.DatType.SpritesDat
The parameter.
@param.Index.Sprite
The target sprite.
]================================]
function GetSpritesDat(DatType, Index) -- DatFile 组/SpritesDat, Sprite/返回指定 [Index] 的单位的 [DatType] 值。
	Sprite = ParseSprites(Index)
    str = GetDatFile("sprites", DatType, Sprite)
	echo(str)
end


--[================================[
@Language.ko-KR
@Summary
[Index]의 [DatType] 값을 반환합니다.
@Group
DatFile
@param.DatType.ImagesDat
파라미터입니다.
@param.Index.Image
대상 이미지입니다.


@Language.zh-CN
@Summary
返回指定 [Index] 的单位的 [DatType] 值。
@Group
DatFile
@param.DatType.ImagesDat
单位类型参数。
@param.Index.Image
目标单位索引类型(Image)。


@Language.en-US
@Summary
Returns the value of [DatType] at [Index].
@Group
DatFile
@param.DatType.ImagesDat
The parameter.
@param.Index.Image
The target image.
]================================]
function GetImagesDat(DatType, Index) -- DatFile 组/ImagesDat, Image/返回指定 [Index] 的单位的 [DatType] 值。
	Image = ParseImages(Index)
    str = GetDatFile("images", DatType, Image)
	echo(str)
end

--[================================[
@Language.ko-KR
@Summary
[Index]의 [DatType] 값을 반환합니다.
@Group
DatFile
@param.DatType.UpgradesDat
파라미터입니다.
@param.Index.Upgrade
대상 업그레이드입니다.


@Language.zh-CN
@Summary
返回指定 [Index] 的单位的 [DatType] 值。
@Group
DatFile
@param.DatType.UpgradesDat
单位类型参数。
@param.Index.Upgrade
目标单位索引类型(Upgrade)。


@Language.en-US
@Summary
Returns the value of [DatType] at [Index].
@Group
DatFile
@param.DatType.UpgradesDat
The parameter.
@param.Index.Upgrade
The target upgrade.
]================================]
function GetUpgradesDat(DatType, Index) -- DatFile 组/UpgradesDat, Upgrade/返回指定 [Index] 的单位的 [DatType] 值。
	Upgrade = ParseUpgrade(Index)
    str = GetDatFile("upgrades", DatType, Upgrade)
	echo(str)
end

--[================================[
@Language.ko-KR
@Summary
[Index]의 [DatType] 값을 반환합니다.
@Group
DatFile
@param.DatType.TechdataDat
파라미터입니다.
@param.Index.Tech
대상 기술입니다.


@Language.zh-CN
@Summary
返回指定 [Index] 的单位的 [DatType] 值。
@Group
DatFile
@param.DatType.TechdataDat
单位类型参数。
@param.Index.Tech
目标单位索引类型(Tech)。


@Language.en-US
@Summary
Returns the value of [DatType] at [Index].
@Group
DatFile
@param.DatType.TechdataDat
The parameter.
@param.Index.Tech
The target tech.
]================================]
function GetTechdataDat(DatType, Index) -- DatFile 组/TechdataDat, Tech/返回指定 [Index] 的单位的 [DatType] 值。
	Tech = ParseTech(Index)
    str = GetDatFile("techdata", DatType, Tech)
	echo(str)
end

--[================================[
@Language.ko-KR
@Summary
[Index]의 [DatType] 값을 반환합니다.
@Group
DatFile
@param.DatType.OrdersDat
파라미터입니다.
@param.Index.Order
대상 명령입니다.


@Language.zh-CN
@Summary
返回指定 [Index] 的单位的 [DatType] 值。
@Group
DatFile
@param.DatType.OrdersDat
单位类型参数。
@param.Index.Order
目标单位索引类型(Order)。


@Language.en-US
@Summary
Returns the value of [DatType] at [Index].
@Group
DatFile
@param.DatType.OrdersDat
The parameter.
@param.Index.Order
The target order.
]================================]
function GetOrdersDat(DatType, Index) -- DatFile 组/OrdersDat, Order/返回指定 [Index] 的单位的 [DatType] 值。
	Order = ParseOrder(Index)
    str = GetDatFile("orders", DatType, Order)
	echo(str)
end

--[================================[
@Language.ko-KR
@Summary
[DatType]의 주소를 반환합니다
@Group
DatFile
@param.DatType.UnitsDat
파라미터입니다.

@Language.zh-CN
@Summary
返回 [DatType] 的地址
@Group
DatFile
@param.DatType.UnitsDat
单位类型值。


@Language.en-US
@Summary
Returns the address of [DatType].
@Group
DatFile
@param.DatType.UnitsDat
The parameter.
]================================]
function UnitsDatOffset(DatType)
    str = DatOffset("units", DatType)
	return str
end

--[================================[
@Language.ko-KR
@Summary
[DatType]의 주소를 반환합니다
@Group
DatFile
@param.DatType.WeaponsDat
파라미터입니다.

@Language.zh-CN
@Summary
返回 [DatType] 的地址
@Group
DatFile
@param.DatType.WeaponsDat
单位类型参数。


@Language.en-US
@Summary
Returns the address of [DatType].
@Group
DatFile
@param.DatType.WeaponsDat
The parameter.
]================================]
function WeaponsDatOffset(DatType)
    str = DatOffset("weapons", DatType)
	return str
end

--[================================[
@Language.ko-KR
@Summary
[DatType]의 주소를 반환합니다
@Group
DatFile
@param.DatType.FlingyDat
파라미터입니다.


@Language.zh-CN
@Summary
返回 [DatType] 的地址
@Group
DatFile
@param.DatType.FlingyDat
单位类型参数。


@Language.en-US
@Summary
Returns the address of [DatType].
@Group
DatFile
@param.DatType.FlingyDat
The parameter.
]================================]
function FlingyDatOffset(DatType)
    str = DatOffset("flingy", DatType)
	return str
end

--[================================[
@Language.ko-KR
@Summary
[DatType]의 주소를 반환합니다
@Group
DatFile
@param.DatType.SpritesDat
파라미터입니다.

@Language.zh-CN
@Summary
返回 [DatType] 的地址
@Group
DatFile
@param.DatType.SpritesDat
单位类型参数。


@Language.en-US
@Summary
Returns the address of [DatType].
@Group
DatFile
@param.DatType.SpritesDat
The parameter.
]================================]
function SpritesDatOffset(DatType)
    str = DatOffset("sprites", DatType)
	return str
end

--[================================[
@Language.ko-KR
@Summary
[DatType]의 주소를 반환합니다
@Group
DatFile
@param.DatType.ImagesDat
파라미터입니다.

@Language.zh-CN
@Summary
返回 [DatType] 的地址
@Group
DatFile
@param.DatType.ImagesDat
单位类型参数。


@Language.en-US
@Summary
Returns the address of [DatType].
@Group
DatFile
@param.DatType.ImagesDat
The parameter.
]================================]
function ImagesDatOffset(DatType)
    str = DatOffset("images", DatType)
	return str
end

--[================================[
@Language.ko-KR
@Summary
[DatType]의 주소를 반환합니다
@Group
DatFile
@param.DatType.UpgradesDat
파라미터입니다.

@Language.zh-CN
@Summary
返回 [DatType] 的地址
@Group
DatFile
@param.DatType.UpgradesDat
单位类型参数。


@Language.en-US
@Summary
Returns the address of [DatType].
@Group
DatFile
@param.DatType.UpgradesDat
The parameter.
]================================]
function UpgradesDatOffset(DatType)
    str = DatOffset("upgrades", DatType)
	return str
end

--[================================[
@Language.ko-KR
@Summary
[DatType]의 주소를 반환합니다
@Group
DatFile
@param.DatType.TechdataDat
파라미터입니다.

@Language.zh-CN
@Summary
返回 [DatType] 的地址
@Group
DatFile
@param.DatType.TechdataDat
单位类型参数。


@Language.en-US
@Summary
Returns the address of [DatType].
@Group
DatFile
@param.DatType.TechdataDat
The parameter.
]================================]
function TechdataDatOffset(DatType)
    str = DatOffset("techdata", DatType)
	return str
end

--[================================[
@Language.ko-KR
@Summary
[DatType]의 주소를 반환합니다
@Group
DatFile
@param.DatType.OrdersDat
파라미터입니다.

@Language.zh-CN
@Summary
返回 [DatType] 的地址
@Group
DatFile
@param.DatType.OrdersDat
单位类型参数。


@Language.en-US
@Summary
Returns the address of [DatType].
@Group
DatFile
@param.DatType.OrdersDat
The parameter.
]================================]
function OrdersDatOffset(DatType)
    str = DatOffset("orders", DatType)
	return str
end

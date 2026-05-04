--[================================[
@Language.ko-KR
@Summary
화면 밝기를 [Amount]만큼 [Modifier]합니다.
@Group
시스템
@param.Modifier.TrgModifier
조절할 방법입니다.
@param.Amount.Number
설정할 값입니다.



@Language.zh-CN
@Summary
将屏幕亮度 [Modifier] 为 [Amount].
@Group
系统
@param.Modifier.TrgModifier
修改函数。
@param.Amount.Number
修改值。


@Language.en-US
@Summary
[Modifier] s the screen brightness by [Amount].
@Group
System
@param.Modifier.TrgModifier
The modifier.
@param.Amount.Number
The amount.
]================================]
function SetLight(Modifier, Amount) -- General 组/TrgModifier, Number/将屏幕亮度 [Modifier] 为 [Amount].
    Modifier = ParseModifier(Modifier)
    Offset = LightOffset()
	echo(string.format("SetMemoryEPD(EPD(%s), %s, %s)", Offset, Modifier, Amount))
end

--[================================[
@Language.ko-KR
@Summary
화면 밝기의 값을 읽습니다.
@Group
시스템


@Language.zh-CN
@Summary
读取屏幕亮度值。
@Group
系统


@Language.en-US
@Summary
Reads the screen brightness value.
@Group
System
]================================]
function GetLight() -- General 组//读取屏幕亮度值。
    Offset = LightOffset()
	echo(string.format("dwread_epd(EPD(%s))", Offset))
end

--[================================[
@Language.ko-KR
@Summary
화면 밝기의 주소를 반환합니다.
@Group
시스템


@Language.zh-CN
@Summary
返回屏幕亮度值的地址。
@Group
系统


@Language.en-US
@Summary
Returns the address of the screen brightness.
@Group
System
]================================]
function LightOffset() -- General 组//返回屏幕亮度值的地址。
	return "0x657A9C"
end

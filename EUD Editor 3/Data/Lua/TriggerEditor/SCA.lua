--[================================[
@Language.ko-KR
@Summary
[ScriptName]을 실행하고 결과를 [ReturnIndex]에 반환합니다.
@Group
SCA
@param.ScriptName.SCAScript
실행할 스크립트의 이름입니다.
@param.ReturnIndex.Number
반환할 인덱스입니다.

@Language.zh-CN
@Summary
执行脚本 [ScriptName] 并返回值为 [ReturnIndex] 的索引。
@Group
SCA
@param.ScriptName.SCAScript
要运行的脚本的名称。
@param.ReturnIndex.Number
要返回的索引。

@Language.en-US
@Summary
Executes [ScriptName] and returns the result in [ReturnIndex].
@Group
SCA
@param.ScriptName.SCAScript
The name of the script to execute.
@param.ReturnIndex.Number
The index to return the result to.
]================================]
function SCAScriptRunFunc(ScriptName, ReturnIndex, ...)
	preDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	mainPreDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	preDefine("const SCAArgArray = EUDArray(100);")
	afterText("scalua.Exec();")

	argcount = 0
	for i,v in ipairs(arg) do
		echo("SCAArgArray[" .. (i - 1) .. "] = " .. tostring(v) .. ";\n") -- 添加换行符
		argcount = argcount + 1
	end

	echo("scalua.scaExecScript(".. ParseSCAScript(ScriptName) .. ", " .. ReturnIndex .. ", " .. argcount .. ", SCAArgArray)") -- 传递数组的 EPD.
end



--[================================[
@Language.ko-KR
@Summary
Script 변수 [Variable]에 [Value]만큼 [Modifier]합니다.
@Group
SCA
@param.Variable.TrgString
변수 이름입니다.
@param.Value.Number
넣을 값 입니다.
@param.Modifier.TrgModifier
연산 종류 입니다.

@Language.zh-CN
@Summary
将 Script 变量 [Variable] 值 [Modifier] 为 [Value].
@Group
SCA
@param.Variable.TrgString
变量名称。
@param.Value.Number
修改值。
@param.Modifier.TrgModifier
修改函数。

@Language.en-US
@Summary
[Modifier] s the script variable [Variable] by [Value].
@Group
SCA
@param.Variable.TrgString
The variable name.
@param.Value.Number
The value to apply.
@param.Modifier.TrgModifier
The operation type.
]================================]
function SCAScriptWriteVariable(Variable, Modifier, Value)
	preDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	mainPreDefine("import TriggerEditor.SCALuaWrapper as scalua;")

	Modifier = ParseModifier(Modifier)

	echo(string.format("SetMemoryEPD(scalua.scf.SCAScriptVarEPD + %s, %s, %s)", ParseSCAScriptVariable(Variable), Modifier, Value))
end



--[================================[
@Language.ko-KR
@Summary
Script 변수 [Variable]를 읽습니다.
@Group
SCA
@param.Variable.TrgString
변수 이름입니다.

@Language.zh-CN
@Summary
读取 Script 变量的 [Variable] 值 .
@Group
SCA
@param.Variable.TrgString
变量名称。

@Language.en-US
@Summary
Reads the script variable [Variable].
@Group
SCA
@param.Variable.TrgString
The variable name.
]================================]
function SCAScriptReadVariable(Variable)
	preDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	mainPreDefine("import TriggerEditor.SCALuaWrapper as scalua;")

	echo(string.format("dwread_epd(scalua.scf.SCAScriptVarEPD + %s)", ParseSCAScriptVariable(Variable)))
end

--[================================[
@Language.ko-KR
@Summary
Script 변수 [Variable]의 오프셋입니다.
@Group
SCA
@param.Variable.TrgString
변수 이름입니다.

@Language.zh-CN
@Summary
Script 的变量 [Variable] 的偏移地址。
@Group
SCA
@param.Variable.TrgString
变量名称。

@Language.en-US
@Summary
The offset of the script variable [Variable].
@Group
SCA
@param.Variable.TrgString
The variable name.
]================================]
function SCAScriptVariableOffset(Variable)
	preDefine("import TriggerEditor.SCALuaWrapper as scalua;")

	echo("scalua.scf.SCAScriptVarEPD + " .. ParseSCAScriptVariable(Variable))
end


--[================================[
@Language.ko-KR
@Summary
[Slot] 슬롯의 데이터를 불러옵니다.
@Group
SCA
@param.Slot.Number

@Language.zh-CN
@Summary
从槽 [Slot] 加载数据。
@Group
SCA
@param.Slot.Number


@Language.en-US
@Summary
Loads data from slot [Slot].
@Group
SCA
@param.Slot.Number
The slot number.
]================================]
function SCALoad(Slot) -- SCA 组/Number/从槽 [Slot] 加载数据。
	preDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	mainPreDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	afterText("scalua.Exec();")


	echo("scalua.scaLoad(".. Slot .. ")")
end



--[================================[
@Language.ko-KR
@Summary
[Slot] 슬롯의 데이터를 저장합니다.
@Group
SCA
@param.Slot.Number


@Language.zh-CN
@Summary
将数据保存在槽 [Slot] 中。
@Group
SCA
@param.Slot.Number
槽位编号

@Language.en-US
@Summary
Saves data to slot [Slot].
@Group
SCA
@param.Slot.Number
The slot number.
]================================]
function SCASave(Slot) -- SCA 组/Number/将数据保存在槽 [Slot] 中。
	preDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	mainPreDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	afterText("scalua.Exec();")


	echo("scalua.scaSave(".. Slot .. ")")
end



--[================================[
@Language.ko-KR
@Summary
해당플레이어를 [BanType]으로 유즈맵에서 차단합니다.
@Group
SCA
@param.BanType.SCABanType


@Language.zh-CN
@Summary
在自定义地图中使用 [BanType] 封禁玩家。。
@Group
SCA
@param.BanType.SCABanType


@Language.en-US
@Summary
Bans the player from the custom map with [BanType].
@Group
SCA
@param.BanType.SCABanType
The ban type.
]================================]
function SCABan(BanType)
	preDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	mainPreDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	afterText("scalua.Exec();")
	
	bancode = {
		["OnlyBan"] = 0,
		["BanWithExit"] = 1
	}
	echo("scalua.scaBan(" .. bancode[BanType] .. ")")
end



--[================================[
@Language.ko-KR
@Summary
시간 정보를 불러옵니다.
@Group
SCA


@Language.zh-CN
@Summary
检索时间信息。
@Group
SCA


@Language.en-US
@Summary
Loads time information.
@Group
SCA
]================================]
function SCALoadTime() -- SCA 组//检索时间信息。
	preDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	mainPreDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	afterText("scalua.Exec();")


	echo("scalua.scaLoadTime()")
end



--[================================[
@Language.ko-KR
@Summary
글로벌 변수를 불러옵니다.
@Group
SCA


@Language.zh-CN
@Summary
加载全局变量。
@Group
SCA


@Language.en-US
@Summary
Loads global variables.
@Group
SCA
]================================]
function SCALoadGlobalData() -- SCA 组//加载全局变量。
	preDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	mainPreDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	afterText("scalua.Exec();")


	echo("scalua.scaLoadGlobal()")
end



--[================================[
@Language.ko-KR
@Summary
시간 정보를 불러옵니다.
@Group
SCA


@Language.zh-CN
@Summary
检索时间信息。
@Group
SCA


@Language.en-US
@Summary
Loads time information.
@Group
SCA
]================================]
function SCALoadTimeOnce() -- SCA 组//检索时间信息。
	preDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	mainPreDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	afterText("scalua.Exec();")


	echo("scalua.scaLoadTimeOnce()")
end



--[================================[
@Language.ko-KR
@Summary
글로벌 변수를 불러옵니다.
@Group
SCA


@Language.zh-CN
@Summary
加载全局变量。
@Group
SCA


@Language.en-US
@Summary
Loads global variables.
@Group
SCA
]================================]
function SCALoadGlobalDataOnce() -- SCA 组//加载全局变量。
	preDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	mainPreDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	afterText("scalua.Exec();")


	echo("scalua.scaLoadGlobalOnce()")
end



--[================================[
@Language.ko-KR
@Summary
불러오기 완료를 확인합니다.
@Group
SCA


@Language.zh-CN
@Summary
确认是否加载完成。
@Group
SCA


@Language.en-US
@Summary
Checks if loading is complete.
@Group
SCA
]================================]
function IsLoadComplete() -- SCA 组//确认是否加载完成。
	preDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	mainPreDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	echo("scalua.IsLoadComplete()")
end



--[================================[
@Language.ko-KR
@Summary
저장 완료를 확인합니다.
@Group
SCA


@Language.zh-CN
@Summary
确认是否完整保存。
@Group
SCA


@Language.en-US
@Summary
Checks if saving is complete.
@Group
SCA
]================================]
function IsSaveComplete() -- SCA 组//确认是否完整保存。
	preDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	mainPreDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	echo("scalua.IsSaveComplete()")
end



--[================================[
@Language.ko-KR
@Summary
글로벌 변수의 불러오기 완료를 확인합니다.
@Group
SCA


@Language.zh-CN
@Summary
确认全局变量是否加载完成。
@Group
SCA


@Language.en-US
@Summary
Checks if loading of global variables is complete.
@Group
SCA
]================================]
function IsGlobalLoadComplete() -- SCA 组//确认全局变量是否加载完成。
	preDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	mainPreDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	echo("scalua.IsGlobalLoadComplete()")
end



--[================================[
@Language.ko-KR
@Summary
시간 정보의 불러오기 완료를 확인합니다.
@Group
SCA


@Language.zh-CN
@Summary
确认时间信息是否加载完成。
@Group
SCA


@Language.en-US
@Summary
Checks if loading of time information is complete.
@Group
SCA
]================================]
function IsTimeLoadComplete() -- SCA 组//确认时间信息是否加载完成。
	preDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	mainPreDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	echo("scalua.IsTimeLoadComplete()")
end



--[================================[
@Language.ko-KR
@Summary
[Index]번 글로벌 데이터의 값을 반환합니다.
@Group
SCA
@param.Index.Number


@Language.zh-CN
@Summary
返回全局数据编号 [Index] 的值。
@Group
SCA
@param.Index.Number


@Language.en-US
@Summary
Returns the value of global data index [Index].
@Group
SCA
@param.Index.Number
The index number.
]================================]
function SCAGetGlobalData(Index) -- SCA 组/Number/返回全局数据编号 [Index] 的值。
	preDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	mainPreDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	afterText("scalua.Exec();")

	echo("scalua.GlobalData(" .. Index .. ")")
end



--[================================[
@Language.ko-KR
@Summary
[Index]번 글로벌 데이터의 값이 [Comparison] [Value]인지 판단합니다.
@Group
SCA
@param.Index.Number
@param.Comparison.TrgComparison
@param.Value.Number


@Language.zh-CN
@Summary
[Comparison]: 比较全局数据 [Index] 的值是否为 [Value].
@Group
SCA
@param.Index.Number
@param.Comparison.TrgComparison
@param.Value.Number


@Language.en-US
@Summary
[Comparison]: Determines if the value of global data index [Index] is [Value].
@Group
SCA
@param.Index.Number
The index number.
@param.Comparison.TrgComparison
The comparison method.
@param.Value.Number
The value.
]================================]
function SCAGlobalData(Index,Comparison,Value) -- SCA 组/Number, TrgComparison, Number/[Comparison]: 比较全局数据 [Index] 的值是否为 [Value].
	preDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	mainPreDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	afterText("scalua.Exec();")
	
    Comparison = ParseComparison(Comparison)
	Variable = "scalua.GlobalData(" .. Index .. ")"
	if Comparison == 0 then
		str = Variable .. " >= " .. Value

		echo(str)
	elseif Comparison == 1 then
		str = Variable .. " <= " .. Value

		echo(str)
	elseif Comparison == 10 then
		str = Variable .. " == " .. Value

		echo(str)
	else
		str = Variable .. Comparison .. Value

		echo(str)
	end
end



--[================================[
@Language.ko-KR
@Summary
[DateType]을 반환합니다.
@Group
SCA
@param.DateType.DateType


@Language.zh-CN
@Summary
返回 [DateType].
@Group
SCA
@param.DateType.DateType


@Language.en-US
@Summary
Returns [DateType].
@Group
SCA
@param.DateType.DateType
The date type.
]================================]
function SCAGetTime(DateType) -- SCA 组/DateType/返回 [DateType].
	preDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	mainPreDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	afterText("scalua.Exec();")

	Variable = ""
	if DateType == "Year" then
		Variable = "scalua.Year()"
	elseif DateType == "Month" then
		Variable = "scalua.Month()"
	elseif DateType == "Day" then
		Variable = "scalua.Day()"
	elseif DateType == "Hour" then
		Variable = "scalua.Hour()"
	elseif DateType == "Min" then
		Variable = "scalua.Min()"
	elseif DateType == "Week" then
		Variable = "scalua.Week()"
	elseif DateType == "Timestamp" then
		Variable = "scalua.Timestamp()"
	end
	echo(Variable)
end



--[================================[
@Language.ko-KR
@Summary
[DateType]이 [Comparison] [Value]인지 판단합니다.
@Group
SCA
@param.DateType.DateType
@param.Comparison.TrgComparison
@param.Value.Number


@Language.zh-CN
@Summary
[Comparison]: 比较 [DateType] 的值是否为 [Value].
@Group
SCA
@param.DateType.DateType
@param.Comparison.TrgComparison
@param.Value.Number


@Language.en-US
@Summary
[Comparison]: Determines if [DateType] is [Value].
@Group
SCA
@param.DateType.DateType
The date type.
@param.Comparison.TrgComparison
The comparison method.
@param.Value.Number
The value.
]================================]
function SCATime(DateType,Comparison,Value) -- SCA 组/DateType, TrgComparison, Number/[Comparison]: 比较 [DateType] 的值是否为 [Value].
	preDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	mainPreDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	afterText("scalua.Exec();")
	
    Comparison = ParseComparison(Comparison)
	Variable = ""
	if DateType == "Year" then
		Variable = "scalua.Year()"
	elseif DateType == "Month" then
		Variable = "scalua.Month()"
	elseif DateType == "Day" then
		Variable = "scalua.Day()"
	elseif DateType == "Hour" then
		Variable = "scalua.Hour()"
	elseif DateType == "Min" then
		Variable = "scalua.Min()"
	elseif DateType == "Week" then
		Variable = "scalua.Week()"
	elseif DateType == "Timestamp" then
		Variable = "scalua.Timestamp()"
	end

	if Comparison == 0 then
		str = Variable .. " >= " .. Value
		echo(str)
	elseif Comparison == 1 then
		str = Variable .. " <= " .. Value

		echo(str)
	elseif Comparison == 10 then
		str = Variable .. " == " .. Value
		echo(str)
	end
end



--[================================[
@Language.ko-KR
@Summary
현재 요일이 [Weekend]인지 확인합니다.
@Group
SCA
@param.Weekend.Weekend


@Language.zh-CN
@Summary
确认当前日期是否为本周的的第 [Weekend] 天（即星期几）。
@Group
SCA
@param.Weekend.Weekend


@Language.en-US
@Summary
Checks if the current day of the week is [Weekend].
@Group
SCA
@param.Weekend.Weekend
The weekend setting.
]================================]
function SCAWeek(Weekend) -- SCA 组/Weekend/确认当前日期是否为本周的的第 [Weekend] 天（即星期几）。
	preDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	mainPreDefine("import TriggerEditor.SCALuaWrapper as scalua;")
	afterText("scalua.Exec();")
	
	Variable = "scalua.Week()"
	weekend = {
		["Monday"] = 0,
		["Tuesday"] = 1,
		["Wednesday"] = 2,
		["Thursday"] = 3,
		["Friday"] = 4,
		["Saturday"] = 5,
		["Sunday"] = 6
	}
	echo(Variable .. " == ")
	echo(weekend[Weekend])

	--_weekval = weekend[Weekend]
	--str = Variable .. " == " .. _weekval
	--echo(str)
end

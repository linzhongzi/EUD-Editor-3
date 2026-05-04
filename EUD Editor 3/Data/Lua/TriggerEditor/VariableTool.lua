--[================================[
@Language.ko-KR
@Summary
[Variable]을 [Value]으로 [Modifier]합니다.
@Group
변수
@param.Variable.Variable
@param.Modifier.TrgModifier
@param.Value.Number


@Language.zh-CN
@Summary
[Variable] 值 [Modifier] 为 [Value].
@Group
变量
@param.Variable.Variable
@param.Modifier.TrgModifier
@param.Value.Number


@Language.en-US
@Summary
[Modifier] s [Variable] by [Value].
@Group
Variable
@param.Variable.Variable
The variable.
@param.Modifier.TrgModifier
The modifier.
@param.Value.Number
The value.
]================================]
function SetVariable(Variable, Modifier, Value) -- Variable 组/Variable, TrgModifier, Number/[Variable] 值 [Modifier] 为 [Value].
	Modifier = ParseModifier(Modifier)

	if Modifier == 7 then
		strStart, strEnd = string.find(Variable, "%.")
		str = ""
		if strStart == nil then
			str = Variable .. " = " .. Value
		else
			str = string.format("SetVariables(%s, %s)", Variable, Value);
		end
		echo(str)
	elseif Modifier == 8 then
		str = Variable .. " += " .. Value

	
		echo(str)
	elseif Modifier == 9 then
		str = Variable .. " -= " .. Value

	
		echo(str)
	end
end

--[================================[
@Language.ko-KR
@Summary
[Variable]이 [Comparison] [Value]인지 판단합니다.
@Group
변수
@param.Variable.Variable
@param.Comparison.TrgComparison
@param.Value.Number


@Language.zh-CN
@Summary
[Comparison] ：比较 [Variable] 是否为 [ [Value].
@Group
变量
@param.Variable.Variable
@param.Comparison.TrgComparison
@param.Value.Number


@Language.en-US
@Summary
[Comparison]: Compare whether [Variable] is [ [Value].
@Group
Variable
@param.Variable.Variable
The variable.
@param.Comparison.TrgComparison
The comparison method.
@param.Value.Number
The value.
]================================]
function Variable(Variable, Comparison, Value) -- Variable 组/Variable, TrgComparison, Number/[Comparison] ：比较 [Variable] 是否为 [ [Value].
	Comparison = ParseComparison(Comparison)

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

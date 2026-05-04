--[================================[
@Language.ko-KR
@Summary
[Text][Args]을 버퍼 [Buffer]를 사용해 출력합니다.
@Group
텍스트출력
@param.Buffer.TrgString
@param.Text.FormatString
@param.Args.Arguments


@Language.zh-CN
@Summary
使用缓冲区 [Buffer] 输出 [Text][Args].
@Group
文本输出
@param.Buffer.TrgString
@param.Text.FormatString
@param.Args.Arguments


@Language.en-US
@Summary
Outputs [Text][Args] using buffer [Buffer].
@Group
Text Output
@param.Buffer.TrgString
The buffer string.
@param.Text.FormatString
The format string.
@param.Args.Arguments
The arguments.
]================================]
function Printf(Buffer, Text, Args) -- Text Output 组/TrgString, FormatString, Arguments/使用缓冲区 [Buffer] 输出 [Text][Args].
	preDefine("const " .. Buffer .. " = StringBuffer();")

	if Args == "" then
		stext = Buffer .. ".printf(\"" .. Text .. "\")"
	else
		stext = Buffer .. ".printf(\"" .. Text .. "\", " .. Args .. ")"
	end

	echo(stext)
end

--[================================[
@Language.ko-KR
@Summary
[Text][Args]을 버퍼 [Buffer]를 사용해 [Line] 라인에 출력합니다.
@Group
텍스트출력
@param.Buffer.TrgString
@param.Line.Number
@param.Text.FormatString
@param.Args.Arguments


@Language.zh-CN
@Summary
使用缓冲区 [Buffer] 将 [Text][Args] 输出到行 [Line].
@Group
文本输出
@param.Buffer.TrgString
@param.Line.Number
@param.Text.FormatString
@param.Args.Arguments


@Language.en-US
@Summary
Outputs [Text][Args] on line [Line] using buffer [Buffer].
@Group
Text Output
@param.Buffer.TrgString
The buffer string.
@param.Line.Number
The line number.
@param.Text.FormatString
The format string.
@param.Args.Arguments
The arguments.
]================================]
function PrintfAt(Buffer, Line, Text, Args) -- Text Output 组/TrgString, Number, FormatString, Arguments/使用缓冲区 [Buffer] 将 [Text][Args] 输出到行 [Line].
	preDefine("const " .. Buffer .. " = StringBuffer();")
		
	if Args == "" then
		stext = Buffer .. ".printfAt(" .. Line .. ", \"" .. Text .. "\")"
	else
		stext = Buffer .. ".printfAt(" .. Line .. ", \"" .. Text .. "\", " .. Args .. ")"
	end

	echo(stext)
end

--[================================[
@Language.ko-KR
@Summary
[TblID]의 [Offset]위치에 [Text][Args]를 씁니다.
@Group
텍스트출력
@param.TblID.Tbl
@param.Offset.Number
@param.Text.FormatString
@param.Args.Arguments


@Language.zh-CN
@Summary
写入 [Text][Args] 到 [TblID] 的 [Offset] 偏移地址 .
@Group
文本输出
@param.TblID.Tbl
@param.Offset.Number
@param.Text.FormatString
@param.Args.Arguments


@Language.en-US
@Summary
Writes [Text][Args] at offset [Offset] of [TblID].
@Group
Text Output
@param.TblID.Tbl
The table ID.
@param.Offset.Number
The offset.
@param.Text.FormatString
The format string.
@param.Args.Arguments
The arguments.
]================================]
function SetTbl(TblID, Offset, Text, Args) -- Tbl/Tbl, Number, FormatString, Arguments/写入 [Text][Args] 到 [TblID] 的 [Offset] 偏移地址 .
	if Args == "" then
		stext = "settblf(" .. TblID .. ", " .. Offset .. ", \"" .. Text .. "\")"
	else
		stext = "settblf(" .. TblID .. ", " .. Offset .. ", \"" .. Text .. "\", " .. Args .. ")"
	end
	echo(stext)
end

--[================================[
@Language.ko-KR
@Summary
[TblID]의 [Offset]위치를 [Text][Args]로 교체합니다.
@Group
텍스트출력
@param.TblID.Tbl
@param.Offset.Number
@param.Text.FormatString
@param.Args.Arguments


@Language.zh-CN
@Summary
将偏移地址为 [Offset] 的 [TblID] 的内容替换为 [Text][Args].
@Group
文本输出
@param.TblID.Tbl
@param.Offset.Number
@param.Text.FormatString
@param.Args.Arguments


@Language.en-US
@Summary
Replaces the content at offset [Offset] of [TblID] with [Text][Args].
@Group
Text Output
@param.TblID.Tbl
The table ID.
@param.Offset.Number
The offset.
@param.Text.FormatString
The format string.
@param.Args.Arguments
The arguments.
]================================]
function ChangeTbl(TblID, Offset, Text, Args) -- Tbl/Tbl, Number, FormatString, Arguments/将偏移地址为 [Offset] 的 [TblID] 的内容替换为 [Text][Args].
	if Args == "" then
		stext = "settblf2(" .. TblID .. ", " .. Offset .. ", \"" .. Text .. "\")"
	else
		stext = "settblf2(" .. TblID .. ", " .. Offset .. ", \"" .. Text .. "\", " .. Args .. ")"
	end
	echo(stext)
end

--[================================[
@Language.ko-KR
@Summary
[Text][Args]을 에러줄에 출력합니다.
@Group
텍스트출력
@param.Text.FormatString
@param.Args.Arguments


@Language.zh-CN
@Summary
[Text][Args] 被输出到错误行。
@Group
文本输出
@param.Text.FormatString
@param.Args.Arguments


@Language.en-US
@Summary
Outputs [Text][Args] to the error line.
@Group
Text Output
@param.Text.FormatString
The format string.
@param.Args.Arguments
The arguments.
]================================]
function ErrorPrintf(Text, Args) -- Text Output 组/FormatString, Arguments/[Text][Args] 被输出到错误行。

	if Args == "" then
		stext = "eprintf(\"" .. Text .. "\")"
	else
		stext = "eprintf(\"" .. Text .. "\", " .. Args .. ")"
	end

	echo(stext)
end

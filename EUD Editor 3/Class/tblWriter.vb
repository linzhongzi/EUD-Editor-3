Imports System.IO
Imports System.Text.RegularExpressions

Public Class tblWriter
    Public Shared Sub WriteTbl(tblStrings As String(), filename As String)
        Dim fs As New FileStream(filename, FileMode.Create)
        Dim bw As New BinaryWriter(fs)

        Dim tblCount As UInt16 = tblStrings.Count

        ' 写入tbl的数量
        bw.Write(tblCount)

        Dim header(tblCount - 1) As UInt16

        fs.Position = tblCount * 2 + 2
        For i = 0 To tblCount - 1
            header(i) = fs.Position


            'Dim rgx As New Regex("<[^>\\]*(?:\\.[^>\\]*)*>", RegexOptions.IgnoreCase)
            Dim tempstr As String = StrExecuter(tblStrings(i))
            tempstr = StringTool.ChangeSlash(tempstr)

            Dim en As Text.Encoding = Text.Encoding.GetEncoding(949, New Text.EncoderExceptionFallback(), New Text.DecoderExceptionFallback())
            'Dim en As Text.Encoding = Text.Encoding.UTF8


            Try
                Dim barray() As Byte
                barray = en.GetBytes(tempstr)
                bw.Write(barray)
            Catch ex As Text.EncoderFallbackException
                bw.Write(Text.Encoding.UTF8.GetBytes(tempstr))
                bw.Write(CByte(&HE2))
                bw.Write(CByte(&H80))
                bw.Write(CByte(&H89))
            End Try



            'bw.Write(CByte(&HE2))
            'bw.Write(CByte(&H80))
            'bw.Write(CByte(&H89))
            'bw.Write(Text.Encoding.UTF8.GetBytes(StringTool.ChangeSlash(tempstr)))


            bw.Write(CByte(0))
        Next

        fs.Position = 2
        For i = 0 To tblCount - 1
            bw.Write(header(i))
        Next


        bw.Close()
        fs.Close()
    End Sub

    Public Shared Function StrExecuter(str As String, Optional DeleteOChar As Boolean = False) As String
        Dim rgx As New Regex("<[^>\\]*(?:\\.[^>\\]*)*>", RegexOptions.IgnoreCase)
        Dim tempstr As String = str

        ' ==================字符串转换========================
        Dim MatchPass As Integer = 0

        Dim TempChar As String = "ᚏ"
        Dim SpecialKeys As New List(Of String)
        Dim SpecialKeyPos As New List(Of Integer)
        Dim OriginalKeys As New List(Of String)

        Dim Matches As Text.RegularExpressions.MatchCollection = rgx.Matches(tempstr)
        ' 替换Rgx为字符并返回相应地址。
        ' 如果在Virtual中，则返回该编号。
        For k = 0 To Matches.Count - 1
            Dim tMatches As Text.RegularExpressions.MatchCollection = rgx.Matches(tempstr)

            Dim pureStr As String = Mid(tMatches(MatchPass).Value, 2, tMatches(MatchPass).Value.Length - 2)
            Dim ResultStr As String = pureStr

            Dim PassFlag As Boolean = False
            ' 检查值的纯净性。（判断是否为Virtual KEY字符或十六进制）
            For keys = 0 To SCConst.ASCIICount
                If scData.ASCIICode(keys) = pureStr Then ' 如果是Virtual KEY
                    ResultStr = Hex(keys).PadLeft(2, "0")
                    PassFlag = True
                End If
            Next
            If Not PassFlag Then
                Try
                    Dim Isnum As Long = "&H" & pureStr ' 判断是否为十六进制
                    ResultStr = Hex(Isnum).PadLeft(2, "0")
                    PassFlag = True
                Catch ex As Exception

                End Try
            End If
            If Not PassFlag Then
                MatchPass += 1
                Continue For
            End If

            OriginalKeys.Add(tMatches(MatchPass).Value)
            SpecialKeys.Add(ResultStr)
            SpecialKeyPos.Add(tMatches(MatchPass).Index)
            tempstr = Replace(tempstr, tMatches(MatchPass).Value, TempChar, 1, 1)
        Next
        ' 完成星号字符


        For k = 0 To SpecialKeyPos.Count - 1
            If DeleteOChar Then
                tempstr = Replace(tempstr, TempChar, "", 1, 1)
            Else
                tempstr = Replace(tempstr, TempChar, Chr("&H" & SpecialKeys(k)), 1, 1)
            End If
        Next
        Return tempstr
    End Function

End Class

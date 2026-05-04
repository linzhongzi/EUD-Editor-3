Imports System.Text

Partial Public Class BuildData
    Private Sub WriteButtonSet(sb As StringBuilder)
        ' 随机指针集合偏移量
        sb.AppendLine("    # 버튼셋")
        Dim Address(SCButtonCount - 1) As String
        For Index = 0 To SCButtonCount - 1
            If pjData.ExtraDat.ButtonData.GetButtonSet(Index).IsDefault Then
                ' 如果未更改，则保持默认地址
                Address(Index) = pjData.ExtraDat.ButtonData.GetButtonSet(Index).DefaultAddress
            Else
                sb.AppendLine("    bytebuffer = bytearray([" & pjData.ExtraDat.ButtonData.GetButtonSet(Index).GetBytesArrayString & "])")
                sb.AppendLine("    btnptr" & Index & " = Db(bytebuffer)")

                Address(Index) = "btnptr" & Index
            End If
        Next





        For Index = 0 To SCButtonCount - 1
            ' 如果按钮集类型更改
            If Not pjData.ExtraDat.DefaultButtonSet(Index) Then
                Dim newButtonIndex As Integer = pjData.ExtraDat.ButtonSet(Index)
                Dim buttonCount As Integer = pjData.ExtraDat.ButtonData.GetButtonSet(newButtonIndex).ButtonS.Count

                sb.AppendLine("    DoActions([")
                sb.AppendLine("        SetMemory(0x" & Hex(Val("&H" & Hex(Tool.GetOffset("FG_BtnAddress"))) + 12 * Index) & ", SetTo, " & Address(newButtonIndex) & "),")
                sb.AppendLine("        SetMemory(0x" & Hex(Val("&H" & Hex(Tool.GetOffset("FG_BtnNum"))) + 12 * Index) & ", SetTo, " & buttonCount & "),")
                sb.AppendLine("    ])")
            ElseIf Not IsNumeric(Address(Index)) Then
                ' 如果是原版但数据已更改？
                Dim newButtonIndex As Integer = Index
                Dim buttonCount As Integer = pjData.ExtraDat.ButtonData.GetButtonSet(Index).ButtonS.Count

                sb.AppendLine("    DoActions([")
                sb.AppendLine("        SetMemory(0x" & Hex(Val("&H" & Hex(Tool.GetOffset("FG_BtnAddress"))) + 12 * Index) & ", SetTo, " & Address(newButtonIndex) & "),")
                sb.AppendLine("        SetMemory(0x" & Hex(Val("&H" & Hex(Tool.GetOffset("FG_BtnNum"))) + 12 * Index) & ", SetTo, " & buttonCount & "),")
                sb.AppendLine("    ])")
            End If
        Next



    End Sub
End Class

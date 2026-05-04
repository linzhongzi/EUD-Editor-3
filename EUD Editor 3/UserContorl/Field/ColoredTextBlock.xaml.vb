Imports System.Globalization
Imports System.Text.RegularExpressions

Public Class ColoredTextBlock
    Public Sub New()

        ' 设计器需要此调用。
        InitializeComponent()

        ' 在 InitializeComponent() 调用后添加初始化代码。
    End Sub
    Public Sub New(text As String)

        ' 设计器需要此调用。
        InitializeComponent()

        ' 在 InitializeComponent() 调用后添加初始化代码。
        TextColred(text)
    End Sub




    Public Sub TextColred(text As String)
        Dim inlines As InlineCollection = Texts.Inlines
        inlines.Clear()
        Dim MainText As String = text.Replace(vbCrLf, "<A>")


        Dim rgx As New Regex("<([A-Za-z0-9])+>", RegexOptions.IgnoreCase)

        Dim LastColor As Integer = 1
        Dim LastCode As Integer = 1
        Dim Startindex As Integer = 1
        For i = 0 To rgx.Matches(MainText).Count - 1
            Dim tMatch As Match = rgx.Matches(MainText).Item(i)

            Dim Value As String = tMatch.Value
            Value = Mid(Value, 2, Value.Length - 2)

            Dim ColorCode As Integer = -1
            Try
                ColorCode = "&H" & Value
                If ColorCode > ColorTable.Count - 1 Then
                    Continue For
                End If
            Catch ex As Exception
                Continue For
            End Try


            ' 一个空格对应4像素

            Dim AddedText As String = Mid(MainText, Startindex, tMatch.Index - Startindex + 1)
            ' MsgBox(AddedText & vbCrLf & "   匹配索引:" & tMatch.Index & "  开始索引:" & Startindex)

            ' 剩余空间


            If LastCode = &H12 Or LastCode = &H13 Then ' 12 右对齐   13 居中
                Dim TextBoxWidth As Integer = Texts.ActualWidth
                Dim TextWidth As Integer
                Dim AddexWidth As Integer = GetTextLen(AddedText)

                If MainText.IndexOf(vbCrLf) = -1 Then ' 如果没有换行
                    TextWidth = GetTextLen(MainText)
                Else

                End If

                MsgBox("TextBoxWidth:" & TextBoxWidth & "    TextWidth:" & TextWidth & "    AddexWidth:" & AddexWidth)
            End If


            'Return New Size(FormattedText.Width, FormattedText.Height);




            Dim Run As New Run(AddedText)
            Run.Foreground = New SolidColorBrush(ColorTable(LastColor))
            inlines.Add(Run)
            Startindex = tMatch.Index + Value.Length + 3
            LastCode = ColorCode
            If ColorCode <> -1 And ColorTable(ColorCode) <> Nothing Then
                LastColor = ColorCode
            End If
            If LastCode = &HA Then
                inlines.Add(vbCrLf)
                LastColor = 1
            End If
        Next

        If True Then
            Dim AddedText As String = Mid(MainText, Startindex, MainText.Length - Startindex + 1)
            Dim Run As New Run(AddedText)
            Run.Foreground = New SolidColorBrush(ColorTable(LastColor))
            inlines.Add(Run)
        End If


    End Sub

    Private Function GetTextLen(str As String) As Integer
        Dim FormattedText As New FormattedText(str, CultureInfo.CurrentCulture, FlowDirection.LeftToRight,
                          New Typeface(Texts.FontFamily, Texts.FontStyle, Texts.FontWeight, Texts.FontStretch),
                           Texts.FontSize, Brushes.Black, New NumberSubstitution(), TextFormattingMode.Display)
        Return FormattedText.Width
    End Function

End Class

Public Class ListItemCodeBlock
    Public _tcode As TriggerCodeBlock
    Private scripter As ScriptEditor

    Public Sub New(_scripter As ScriptEditor, tcode As TriggerCodeBlock)

        ' 设计器需要此调用。
        InitializeComponent()

        ' 在 InitializeComponent() 调用后添加初始化代码。
        _tcode = tcode
        scripter = _scripter
        RefreshItem()
    End Sub


    Public Sub RefreshItem()
        Wrap.Children.Clear()



        ' 重新排列函数后使用。
        Dim t As TriggerFunction = _tcode.GetCodeFunction(scripter)
        Dim isCmpTrigger As Boolean
        Dim IsEmpty As Boolean = False

        If t Is Nothing Then
            Dim ttb As New Label
            ttb.Foreground = tmanager.HighlightBrush
            ttb.Content = _tcode.FName & vbCrLf & "존재하지 않거나 참조할 수 없는 함수입니다.  "
            ttb.VerticalAlignment = VerticalAlignment.Center

            Wrap.Children.Add(ttb)

            ' 不存在的函数
            ' 只输出参数。
            isCmpTrigger = False
            IsEmpty = True
        Else
            isCmpTrigger = t.IsCmpTrigger
        End If

        _tcode.LoadArgText(scripter)
        If _tcode.FGroup <> "" Then
            Dim ttb As New Label
            ttb.Foreground = Brushes.CadetBlue
            ttb.VerticalContentAlignment = VerticalAlignment.Center

            If Not IsEmpty Then
                ttb.Content = _tcode.FGroup + " : "
            End If

            Wrap.Children.Add(ttb)
        End If
        If isCmpTrigger Then
            If t.SortArgList.Count = 0 Then
                Dim ttb As New Label
                ttb.VerticalContentAlignment = VerticalAlignment.Center
                ttb.VerticalAlignment = VerticalAlignment.Center
                If Not IsEmpty Then
                    If t.FSummary = "" Then
                        ttb.Content = t.FName
                    Else
                        ttb.Content = t.FSummary
                    End If
                End If

                Wrap.Children.Add(ttb)
            Else
                ' 触发器
                For i = 0 To t.SortArgList.Count - 1
                    Dim tstr As String = t.SortArgList(i)

                    If TriggerFunction.IsArg(tstr) Then
                        ' Arg文本
                        Dim tindex As Integer = TriggerFunction.GetArgIndex(tstr)

                        Dim ttb As New Label
                        ttb.VerticalAlignment = VerticalAlignment.Center
                        ttb.Foreground = tmanager.HighlightBrush

                        ttb.Content = _tcode.LoadedArgString(tindex)

                        Wrap.Children.Add(ttb)
                    Else
                        Dim ttb As New Label
                        ttb.VerticalContentAlignment = VerticalAlignment.Center

                        ttb.Content = tstr

                        Wrap.Children.Add(ttb)
                    End If
                Next
            End If


        Else
            ' 仅列出参数
            If True Then
                ' 说明
                If t IsNot Nothing Then
                    If t.FSummary <> "" Then
                        Dim ttb As New Label
                        ttb.VerticalContentAlignment = VerticalAlignment.Center

                        If Not IsEmpty Then
                            ttb.Content = t.FSummary
                        End If

                        Wrap.Children.Add(ttb)
                    Else
                        Dim ttb As New Label
                        ttb.VerticalContentAlignment = VerticalAlignment.Center

                        If Not IsEmpty Then
                            ttb.Content = t.FName
                        End If

                        Wrap.Children.Add(ttb)
                    End If
                End If
            End If

            ' 列出参数。
            For i = 0 To _tcode.Args.Count - 1
                ' Arg文本
                Dim ttb As New Label
                ttb.VerticalAlignment = VerticalAlignment.Center
                ttb.Foreground = tmanager.HighlightBrush

                ttb.Content = _tcode.LoadedArgString(i)

                Wrap.Children.Add(ttb)
            Next
        End If

        If Wrap.Children.Count = 0 Then
            If t IsNot Nothing Then
                Dim ttb As New Label
                ttb.VerticalAlignment = VerticalAlignment.Center
                ttb.Content = t.FName

                Wrap.Children.Add(ttb)
            End If
        End If
    End Sub
End Class

Public Class EPSViewer
    Public Sub New(tTEFile As TEFile)

        ' 设计器需要此调用。
        InitializeComponent()

        ' 在 InitializeComponent() 调用后添加初始化代码。

        macro.macroErrorList.Clear()
        CodeText.Text = tTEFile.Scripter.GetFileText("")
        If macro.macroErrorList.Count <> 0 Then
            Dim m As String = ""
            For Each msg In macro.macroErrorList
                m = m + msg + vbCrLf
            Next
            Tool.ErrorMsgBox("Lua Script Inner Error", m)
        End If

        CodeText.TextEditor.IsReadOnly = True
    End Sub
End Class

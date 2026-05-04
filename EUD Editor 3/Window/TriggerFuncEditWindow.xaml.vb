Public Class TriggerFuncEditWindow
    Private scripter As ScriptEditor
    Private Arg As ArgValue
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        If Arg.CodeBlock IsNot Nothing Then
            MainCtrl.OpenEdit(scripter, TriggerCodeEditControl.OpenType.Func, TBlock:=Arg.CodeBlock.DeepCopy)
        Else
            MainCtrl.OpenEdit(scripter, TriggerCodeEditControl.OpenType.Func)
        End If
    End Sub


    Public Sub New(_scripter As ScriptEditor, _Arg As ArgValue)

        ' 设计器需要此调用。
        InitializeComponent()

        ' 在 InitializeComponent() 调用后添加初始化代码。
        Arg = _Arg
        scripter = _scripter

        AddHandler MainCtrl.OkayBtnEvent, Sub(sender As Object, e As RoutedEventArgs)
                                              If Arg.CodeBlock Is Nothing Then
                                                  Arg.CodeBlock = MainCtrl.SelectTBlock.DeepCopy
                                              Else
                                                  MainCtrl.SelectTBlock.CopyTo(Arg.CodeBlock)
                                              End If
                                              Me.Close()
                                          End Sub
        AddHandler MainCtrl.CancelBtnEvent, Sub(sender As Object, e As RoutedEventArgs)
                                                Me.Close()
                                            End Sub
    End Sub
End Class

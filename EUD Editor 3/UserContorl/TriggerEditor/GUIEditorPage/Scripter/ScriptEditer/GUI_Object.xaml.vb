Public Class GUI_Object
    Public Sub CrlInit()
        '//////////////////////////////
        ' 初始化表达式
        vname.Text = scr.value
    End Sub
    Public Sub OkayAction(sender As Object, e As RoutedEventArgs)
        '//////////////////////////////
        ' 更新脚本
        scr.value = vname.Text
    End Sub
    Private Function CheckEditable() As Boolean
        '//////////////////////////////
        ' 检查是否可以按确认键。
        If vname.Text.Trim = "" Then
            Return False
        End If
        Return True
    End Function


    Private p As GUIScriptEditerWindow
    Private scr As ScriptBlock
    Public Sub New(tp As GUIScriptEditerWindow, tscr As ScriptBlock)

        ' 设计器需要此调用。
        InitializeComponent()

        ' 在 InitializeComponent() 调用后添加初始化代码。
        p = tp
        scr = tscr

        AddHandler p.OkayBtnEvent, AddressOf OkayAction

        CrlInit()
        btnRefresh()
    End Sub
    Public Sub btnRefresh()
        If CheckEditable() Then
            p.OkBtn.IsEnabled = True
        Else
            p.OkBtn.IsEnabled = False
        End If
    End Sub

    Private Sub vname_TextChanged(sender As Object, e As TextChangedEventArgs)
        btnRefresh()
    End Sub
End Class

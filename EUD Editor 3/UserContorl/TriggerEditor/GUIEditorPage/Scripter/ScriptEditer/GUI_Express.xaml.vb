Public Class GUI_Express
    Private isload As Boolean = False

    Public Sub CrlInit()
        '//////////////////////////////
        ' 初始化表达式
        'Dim values As List(Of String) = GUIScriptManager.SplitText(scr.value)
        ArgExpress.CrlInit(scr)

        valueEditPanel.Init(p._GUIScriptEditorUI.TEGUIPage.ValueSelecter, dotscr, p._GUIScriptEditorUI)
        AddHandler ArgExpress.ArgExpressRefreshEvent, AddressOf ArgBtnRefreshEvent
        AddHandler ArgExpress.ArgBtnClickEvent, AddressOf ArgBtnClickEvent

        AddHandler valueEditPanel.BtnRefresh, AddressOf ArgExpress.argBtnRefresh





        isload = True
    End Sub


    Private Sub ArgBtnRefreshEvent(sender As Object, e As RoutedEventArgs)
        btnRefresh()
    End Sub
    Private Sub ArgBtnClickEvent(sender As ScriptBlock, e As RoutedEventArgs)
        If sender Is Nothing Then
            valueEditPanel.Visibility = Visibility.Collapsed
            Return
        End If
        If sender.ScriptType = ScriptBlock.EBlockType.sign Then
            valueEditPanel.Visibility = Visibility.Collapsed
        Else
            valueEditPanel.Visibility = Visibility.Visible
            valueEditPanel.ComboboxInit(sender)
        End If

    End Sub



    Public Sub OkayAction(sender As Object, e As RoutedEventArgs)
        '//////////////////////////////
        ' 更新脚本
        ArgExpress.UpdateValue()

    End Sub
    Private Function CheckEditable() As Boolean
        '//////////////////////////////
        ' 检查是否可以按确认键。

        Return True
    End Function


    Private p As GUIScriptEditerWindow
    Private scr As ScriptBlock
    Private dotscr As ScriptBlock
    Public Sub New(tp As GUIScriptEditerWindow, tscr As ScriptBlock, _dotscr As ScriptBlock)

        ' 设计器需要此调用。
        InitializeComponent()

        ' 在 InitializeComponent() 调用后添加初始化代码。
        p = tp
        scr = tscr
        dotscr = _dotscr

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
End Class

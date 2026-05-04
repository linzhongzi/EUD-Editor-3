Public Class GUI_Return
    Private isload As Boolean = False

    Private EditValues As ScriptBlock
    Public Sub CrlInit()
        '//////////////////////////////
        ' 初始化表达式
        EditValues = scr.child(0).DeepCopy

        valueEditPanel.Init(p._GUIScriptEditorUI.TEGUIPage.ValueSelecter, dotscr, p._GUIScriptEditorUI, True)
        AddHandler valueEditPanel.BtnRefresh, AddressOf AgrbtnRefresh
        valueEditPanel.ComboboxInit(EditValues)

        returnVal.Text = "返回值 : " & EditValues.ValueCoder

        isload = True
    End Sub
    Public Sub AgrbtnRefresh(sender As String, e As RoutedEventArgs)
        ' sender.Last 所选值
        returnVal.Text = "返回值 : " & EditValues.ValueCoder
    End Sub

    Public Sub OkayAction(sender As Object, e As RoutedEventArgs)
        '//////////////////////////////
        ' 更新脚本
        scr.child(0).DuplicationBlock(EditValues)
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

Public Class GUI_VarUse
    Private isload As Boolean = False

    Public Sub CrlInit()
        '//////////////////////////////
        ' 初始化表达式
        'Dim values As List(Of String) = GUIScriptManager.SplitText(scr.value)

        ' 判断变量是否为对象。
        If True Then
            Dim n As String() = {"赋值", "加法", "乘法", "减法", "除法"}
            Dim t As String() = {"SetTo", "Add", "Mul", "Sub", "除法"}
            For i = 0 To n.Count - 1
                Dim cbitem As New ComboBoxItem()

                maincombobox.Items.Add(cbitem)
            Next
        End If


        isload = True
    End Sub

    Public Sub OkayAction(sender As Object, e As RoutedEventArgs)
        '//////////////////////////////
        ' 更新脚本

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

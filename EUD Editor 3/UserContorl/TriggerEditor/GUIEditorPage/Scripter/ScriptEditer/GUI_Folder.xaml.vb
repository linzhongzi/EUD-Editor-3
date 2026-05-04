Public Class GUI_Folder
    Public Sub CrlInit()
        '//////////////////////////////
        ' 初始化表达式

        Dim sstr() As String = scr.value.Split("ᗋ")
        Dim head As String = sstr.First
        Dim tail As String = sstr.Last

        headerText.Text = head
        TailText.Text = tail
        If scr.value <> "" Then
            TailText.IsEnabled = False
            If head = tail Then
                TailText.IsEnabled = True
                chbox.IsChecked = True
            End If
        End If
        isLoad = True
    End Sub
    Public Sub OkayAction(sender As Object, e As RoutedEventArgs)
        '//////////////////////////////
        ' 更新脚本
        scr.value = headerText.Text & "ᗋ" & TailText.Text
    End Sub
    Private Function CheckEditable() As Boolean
        '//////////////////////////////
        ' 检查是否可以按确认键。
        If headerText.Text.Trim = "" Then
            Return False
        End If

        Return True
    End Function


    Private isLoad As Boolean = False


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

    Private Sub chbox_Checked(sender As Object, e As RoutedEventArgs)
        If isLoad Then
            TailText.IsEnabled = True
        End If
    End Sub

    Private Sub chbox_Unchecked(sender As Object, e As RoutedEventArgs)
        If isLoad Then
            TailText.Text = headerText.Text
            TailText.IsEnabled = False
        End If
    End Sub

    Private Sub headerText_TextChanged(sender As Object, e As TextChangedEventArgs)
        If Not TailText.IsEnabled Then
            TailText.Text = headerText.Text
        End If
        btnRefresh()
    End Sub

    Private Sub TailText_TextChanged(sender As Object, e As TextChangedEventArgs)
        If TailText.IsEnabled Then
            btnRefresh()
        End If
    End Sub
End Class

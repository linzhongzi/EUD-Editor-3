Public Class GUI_If
    Public Sub CrlInit()
        '//////////////////////////////
        ' 初始化表达式
        Dim hadelse As Boolean = False

        For i = 0 To scr.child.Count - 1
            If scr.child(i).name = "ifelse" Then
                hadelse = True
                ifCB.IsChecked = True
                Exit For
            End If
        Next




        isLoad = True
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


    Private isLoad As Boolean = False
    Private Sub ifCB_Checked(sender As Object, e As RoutedEventArgs)
        If isLoad Then
            Dim nscr As New ScriptBlock(ScriptBlock.EBlockType.ifelse, "ifelse", True, False, "", p._GUIScriptEditorUI.Script)

            nTreeViewItem.Items.Add(nscr.GetTreeviewitem)
            scr.AddChild(nscr)
        End If
    End Sub

    Private Sub ifCB_Unchecked(sender As Object, e As RoutedEventArgs)
        If isLoad Then
            For i = 0 To nTreeViewItem.Items.Count - 1
                Dim tags As ScriptBlock = CType(nTreeViewItem.Items(i), TreeViewItem).Tag
                If tags.name = "ifelse" Then
                    nTreeViewItem.Items.RemoveAt(i)
                    Exit For
                End If
            Next


            For i = 0 To scr.child.Count - 1
                If scr.child(i).name = "ifelse" Then
                    scr.child.RemoveAt(i)
                    Exit For
                End If
            Next
        End If
    End Sub

    Private nTreeViewItem As TreeViewItem

    Private p As GUIScriptEditerWindow
    Private scr As ScriptBlock
    Public Sub New(tp As GUIScriptEditerWindow, tscr As ScriptBlock, _nTreeViewItem As TreeViewItem)

        ' 设计器需要此调用。
        InitializeComponent()

        ' 在 InitializeComponent() 调用后添加初始化代码。
        p = tp
        scr = tscr
        nTreeViewItem = _nTreeViewItem

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

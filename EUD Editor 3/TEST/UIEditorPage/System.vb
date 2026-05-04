Partial Public Class TreeviewExample
    Private PreviewSelectItems As List(Of TreeViewItem)
    Private SelectItems As List(Of TreeViewItem)
    Private PressCtrl As Boolean = False
    Private PressShift As Boolean = False

    Private LastSelectItem As TreeViewItem

    Private OneClick As Integer

    Private IsDrag As Boolean = False


    ' 当按下控件时，如果点击数非常大，则清除上次的选择！
    ' 当未按下控件键时，如果最后点击的项目不是重复的，则除一个外全部清除。
    Private Sub AddSelectItem(SelectItem As TreeViewItem)
        If SelectItems.IndexOf(SelectItem) < 0 Then 
        ' 防止重复。
        ' 如果未按下控件或 Shift，则防止多选！
            If Not PressCtrl And Not PressShift Then

                ' 如果所选项目较多
                If SelectItems.Count > 0 Then
                    SelectListClear()
                End If
            End If



            OneClick += 1
            'log.Text = log.Text & " [" & SelectItem.Header & " " & OneClick & "]"

            If PressCtrl Then
                If OneClick > 1 Then
                    OneClick -= 1
                    SelectListRemoveAt(-1)
                End If
            End If

            ' 常规选择操作
            SelectItems.Add(SelectItem)
            SelectItem.Background = SelectColor()

            LastSelectItem = SelectItem

            If Not PressCtrl And Not PressShift Then
                ' 如果所选项目较多
                If PreviewSelectItems.IndexOf(SelectItem) >= 0 Then
                    SelectListRecover()
                    LastSelectItem = SelectItem
                End If
            End If
        End If
    End Sub

    Private Sub ListClick(sender As TreeViewItem, e As MouseButtonEventArgs)
        If PressShift Then ' 如果按下了 Shift 键
            If LastSelectItem IsNot Nothing Then 
            ' 如果存在第一个选中块。
            ' 判断当前点击的项目与最后选择的项目是否具有相同的父级。
                If LastSelectItem.Parent Is sender.Parent Then
                    Dim RFirstSelectItem As TreeViewItem = LastSelectItem

                    Dim ParentItemCollection As ItemCollection
                    If TypeOf LastSelectItem.Parent Is TreeView Then
                        ParentItemCollection = CType(LastSelectItem.Parent, TreeView).Items
                    Else
                        ParentItemCollection = CType(LastSelectItem.Parent, TreeViewItem).Items
                    End If


                    Dim FirstSelectIndex As Integer = ParentItemCollection.IndexOf(LastSelectItem)
                    Dim senderSelectIndex As Integer = ParentItemCollection.IndexOf(sender)



                    If FirstSelectIndex > senderSelectIndex Then
                        For i = senderSelectIndex To FirstSelectIndex
                            AddSelectItem(ParentItemCollection(i))
                        Next
                    Else
                        For i = FirstSelectIndex To senderSelectIndex
                            AddSelectItem(ParentItemCollection(i))
                        Next
                    End If
                End If
            End If
        Else
            AddSelectItem(sender)
        End If
    End Sub


    Private Sub MainTreeview_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If e.Key = Key.LeftCtrl Or e.Key = Key.RightCtrl Then
            PressCtrl = True
        End If
        If e.Key = Key.LeftShift Or e.Key = Key.RightShift Then
            PressShift = True
        End If

        If e.Key = Key.Delete Then
            SelectListDelete()
        End If

        If PressCtrl Or PressShift Then
            MainTreeview.Background = Brushes.LightSkyBlue
        End If
    End Sub

    Private Sub MainTreeview_PreviewKeyUp(sender As Object, e As KeyEventArgs)
        If PressCtrl Or PressShift Then
            MainTreeview.Background = DefaultColor()
        End If

        PressCtrl = False
        PressShift = False
    End Sub

    Private Sub MainTreeview_PreviewMouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        OneClick = 0

        PreviewSelectItems.Clear()
        For i = 0 To SelectItems.Count - 1
            PreviewSelectItems.Add(SelectItems(i))
        Next

        If Not PressCtrl And Not PressShift Then
            If CheckDragable() Then
                IsDrag = True
            End If
        End If
        DragSelect = False
    End Sub

    Private Sub MainTreeview_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs)
        IsDrag = False
        FirstDragMove = False
        DragImage.Visibility = Visibility.Collapsed
    End Sub



    Private Sub SelectListDelete()
        For i = 0 To SelectItems.Count - 1
            If DeleteItem(SelectItems(i)) Then
                If SelectItems(i) Is LastSelectItem Then
                    LastSelectItem = Nothing
                End If
            Else
                SelectItems(i).Background = DefaultColor()
            End If
        Next
        SelectItems.Clear()
    End Sub
    Private Sub SelectListRecover()
        SelectListClear()
        For i = 0 To PreviewSelectItems.Count - 1
            If SelectItems.IndexOf(PreviewSelectItems(i)) < 0 Then
                SelectItems.Add(PreviewSelectItems(i))
            End If
            PreviewSelectItems(i).Background = SelectColor()
        Next
    End Sub
    Private Sub SelectListClear()
        For i = 0 To SelectItems.Count - 1
            SelectItems(i).Background = DefaultColor()
        Next
        LastSelectItem = Nothing
        SelectItems.Clear()
    End Sub
    Private Sub SelectListRemoveAt(index As Integer)
        If index >= 0 Then
            If SelectItems.Count > index Then
                SelectItems(index).Background = DefaultColor()

                If LastSelectItem Is SelectItems(index) Then
                    LastSelectItem = Nothing
                End If
                SelectItems.RemoveAt(index)
            End If
        Else
            Dim LastIndex As Integer = SelectItems.Count + index
            If SelectItems.Count > LastIndex And SelectItems.Count > 0 Then
                SelectItems(LastIndex).Background = DefaultColor()

                If LastSelectItem Is SelectItems(LastIndex) Then
                    LastSelectItem = Nothing
                End If
                SelectItems.RemoveAt(LastIndex)
            End If
        End If
    End Sub
End Class

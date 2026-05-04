Partial Public Class ProjectExplorer
    Private PreviewSelectItems As List(Of TreeViewItem)
    Private PreviewLastSelectItem As TreeViewItem
    Private SelectItems As List(Of TreeViewItem)
    Private PressCtrl As Boolean = False
    Private PressShift As Boolean = False
    Private PressArrowKey As Boolean = False
    Private CopyItems As List(Of TEFile)

    Private LastSelectItem As TreeViewItem

    Private OneClick As Integer
    Private MouseClick As Boolean = False

    Private IsDrag As Boolean = False
    Private IsSelect As Boolean = False

    Private ParentWindow As TriggerEditor
    Public Sub Init(Parent As TriggerEditor)
        ParentWindow = Parent
    End Sub


    ' 当按下控件时，如果点击数非常大，则清除上次的选择！
    ' 当未按下控件键时，如果最后点击的项目不是重复的，则除一个外全部清除。
    Private Sub AddSelectItem(SelectItem As TreeViewItem)
        If SelectItems.IndexOf(SelectItem) < 0 Then ' 避免重复

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
            SelectItem.IsSelected = True

            LastSelectItem = SelectItem

            If Not PressCtrl And Not PressShift Then
                ' 如果所选项目较多
                If PreviewLastSelectItem Is SelectItem Then
                    SelectListRecover()
                    LastSelectItem = SelectItem
                End If

                'If PreviewSelectItems.IndexOf(SelectItem) >= 0 Then
                '    SelectListRecover()
                '    LastSelectItem = SelectItem
                'End If
            End If
        Else
            If PreviewLastSelectItem IsNot SelectItem Then
                SelectListClear()
            End If
        End If
    End Sub
    Private Sub MainTreeviewItme_PreviewMouseDoubleClick(sender As TreeViewItem, e As MouseButtonEventArgs)
        OpenPage()
    End Sub
    Private Sub MainTreeviewItme_PreviewMouseRightButtonDown(sender As TreeViewItem, e As MouseButtonEventArgs)
        AddSelectItem(sender)
    End Sub
    Private Sub ListClick(sender As TreeViewItem, e As MouseButtonEventArgs)
        IsSelect = True
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
                        For i = FirstSelectIndex To senderSelectIndex Step -1
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


        If e.Key = Key.Down Or e.Key = Key.Up Then
            PressArrowKey = True
        End If
        'If PressCtrl Or PressShift Then
        '    MainTreeview.Background = Brushes.LightSkyBlue
        'End If
    End Sub

    Private Sub MainTreeview_SelectedItemChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Object))
        If PressArrowKey Then
            If MainTreeview.SelectedItem IsNot Nothing Then
                If Not PressShift Then
                    SelectListClear()
                    LastSelectItem = MainTreeview.SelectedItem
                End If


                AddSelectItem(MainTreeview.SelectedItem)
            End If
        End If
    End Sub

    Private Sub MainTreeview_PreviewKeyUp(sender As Object, e As KeyEventArgs)
        If e.Key = Key.Down Or e.Key = Key.Up Then
            PressArrowKey = False
        Else
            PressCtrl = False
            PressShift = False
        End If

        'If PressCtrl Or PressShift Then
        '    MainTreeview.Background = DefaultColor()
        'End If
    End Sub


    Private Sub MainTreeview_PreviewMouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        OneClick = 0

        PreviewLastSelectItem = LastSelectItem
        PreviewSelectItems.Clear()
        For i = 0 To SelectItems.Count - 1
            PreviewSelectItems.Add(SelectItems(i))
        Next

        IsSelect = False
        MouseClick = True
        DragSelect = False
        DragMoving = e.GetPosition(Me)
    End Sub

    Private Sub MainTreeview_PreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs)
        IsDrag = False
        MouseClick = False
        FirstDragMove = False
        DragImage.Visibility = Visibility.Collapsed
    End Sub

    Private Sub MainTreeview_PreviewMouseRightButtonDown(sender As Object, e As MouseButtonEventArgs)
        OneClick = 0

        PreviewLastSelectItem = LastSelectItem
        PreviewSelectItems.Clear()
        For i = 0 To SelectItems.Count - 1
            PreviewSelectItems.Add(SelectItems(i))
        Next
        IsSelect = False
        'SelectListClear()
        DragSelect = False
    End Sub

    Private Sub MainTreeview_PreviewMouseRightButtonUp(sender As Object, e As MouseButtonEventArgs)

    End Sub



    Private Sub SelectListDelete()
        Dim Parent As TreeViewItem = Nothing
        Dim CurrentIndex As Integer


        Dim tisTopFolder As Boolean = True
        If Not GetFile(LastSelectItem).IsTopFolder Then
            Parent = LastSelectItem.Parent
            CurrentIndex = Parent.Items.IndexOf(LastSelectItem)
            tisTopFolder = False
        End If



        For i = 0 To SelectItems.Count - 1
            SelectItems(i).Background = DefaultColor()

            If DeleteItem(SelectItems(i)) Then
                If SelectItems(i) Is LastSelectItem Then
                    LastSelectItem = Nothing
                End If
            End If
        Next
        SelectItems.Clear()

        If Not tisTopFolder Then
            If Parent.Items.Count > CurrentIndex Then
                AddSelectItem(Parent.Items(CurrentIndex))
            ElseIf Parent.Items.Count > 0 Then
                AddSelectItem(Parent.Items(Parent.Items.Count - 1))
            Else
                AddSelectItem(Parent)
            End If
        End If
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

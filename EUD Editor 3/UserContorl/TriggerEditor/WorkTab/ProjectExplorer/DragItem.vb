Partial Public Class ProjectExplorer
    Private DragMoving As Point
    Private FirstDragMove As Boolean = False
    Private DragSelect As Boolean = False

    Private DragSelectItem As TreeViewItem

    Private Function CheckDragable() As Boolean
        ' 判断所选块类型是否一致。
        ' 判断所选块中是否有无法删除、移动或添加的块。
        For i = 0 To SelectItems.Count - 1
            If GetFile(SelectItems(i)).IsTopFolder Or GetFile(SelectItems(i)).FileType = TEFile.EFileType.Setting Then
                Return False
            End If
        Next

        Return True
    End Function

    Private Sub DragWork(FolderNode As TreeViewItem)
        For k = 0 To SelectItems.Count - 1
            If GetFile(SelectItems(k)).FileType = TEFile.EFileType.Setting Then
                Continue For
            End If

            Dim CNode As TreeViewItem = SelectItems(k)
            Dim CFile As TEFile = SelectItems(k).Tag

            Dim PFile As TEFile = CType(CNode.Parent, TreeViewItem).Tag

            If CFile.FileType = TEFile.EFileType.Folder Then
                PFile.FolderRemove(CFile)
            Else
                PFile.FileRemove(CFile)
            End If
            DeleteItem(SelectItems(k))
        Next


        For k = 0 To SelectItems.Count - 1
            If GetFile(SelectItems(k)).FileType = TEFile.EFileType.Setting Then
                Continue For
            End If

            MoveItem(FolderNode, SelectItems(k), IsTop)
        Next
        SortList(FolderNode)
        pjData.SetDirty(True)
        TabItemTool.RefreshExplorer(Me)
    End Sub
    Private Function MoveItem(FolderItem As TreeViewItem, Item As TreeViewItem, Optional IsTop As Boolean = False) As Boolean
        Dim itemC As ItemCollection = FolderItem.Items




        If IsFolder(Item) Then
            Dim CFile As TEFile = Item.Tag
            GetFile(FolderItem).FolderAdd(CFile)

            Dim InsertIndex As Integer = 0

            For i = 0 To itemC.Count - 1
                If CType(CType(itemC(i), TreeViewItem).Tag, TEFile).FileType <> TEFile.EFileType.Folder Then
                    Exit For
                End If
                InsertIndex += 1
            Next
            itemC.Insert(InsertIndex, Item)
        Else
            Dim CFile As TEFile = Item.Tag
            GetFile(FolderItem).FileAdd(CFile)


            itemC.Add(Item)
        End If
        'Dim ItemIndex As Integer = itemC.IndexOf(FriendItem)


        'If IsTop Then
        '    itemC.Insert(ItemIndex, Item)
        'Else
        '    itemC.Insert(ItemIndex + 1, Item)
        'End If


        Return True
    End Function




    Private IsTop As Boolean = False
    Private Sub MainTreeviewItme_PreviewMouseMove(sender As TreeViewItem, e As MouseEventArgs)
        If IsDrag Then
            If Not CheckDragable() Or Not IsSelect Then
                IsDrag = False
                FirstDragMove = False
                DragImage.Visibility = Visibility.Collapsed
            End If

            DragSelect = True

            Dim CurrentYpos As Integer = e.GetPosition(sender).Y
            Dim Heigth As Integer = sender.Header.ActualHeight

            If CurrentYpos > Heigth / 2 Then
                IsTop = False
            Else
                IsTop = True
            End If



            DragSelectItem = sender


        Else
            If MouseClick Then
                Dim x As Integer = e.GetPosition(Me).X - DragMoving.X
                Dim y As Integer = e.GetPosition(Me).Y - DragMoving.Y

                If Math.Abs(x) > 5 Or Math.Abs(y) > 5 Then
                    If Not PressCtrl And Not PressShift Then
                        IsDrag = True
                    End If
                End If
            End If
        End If
    End Sub
    Private Sub MainTreeviewItme_PreviewMouseUp(sender As TreeViewItem, e As MouseEventArgs)
        If DragSelect Then
            If sender Is DragSelectItem Then


                If IsFolder(DragSelectItem) Then
                    If SelectItems.IndexOf(DragSelectItem) < 0 Then
                        ' 如果拖动的位置不是所选块！


                        For i = 0 To SelectItems.Count - 1
                            ' 如果拖动的位置不是所选块的子项！
                            If CheckChild(SelectItems(i), DragSelectItem) Then
                                DragWork(sender)
                            End If
                        Next
                    End If
                End If
            End If
        End If
    End Sub



    Private Function CheckChild(parent As TreeViewItem, Child As TreeViewItem) As Boolean
        ' 循环遍历父级的所有子级！
        For i = 0 To parent.Items.Count - 1
            ' 如果子级中有 Child，则返回 False！
            If CType(parent.Items(i), TreeViewItem) Is Child Then
                Return False
            End If

            ' 也对父级的子级应用该函数。
            If Not CheckChild(parent.Items(i), Child) Then
                Return False
            End If
        Next

        Return True
    End Function


    Private Sub MainTreeview_PreviewMouseMove(sender As Object, e As MouseEventArgs)
        If IsDrag Then
            If Not CheckDragable() Or Not IsSelect Then
                IsDrag = False
                FirstDragMove = False
                DragImage.Visibility = Visibility.Collapsed
                Exit Sub
            End If


            If Not FirstDragMove Then
                CreateDragImage()
                FirstDragMove = True
                DragSelect = False
            End If
            DragImage.Visibility = Visibility.Visible

            DragImage.Margin = New Thickness(e.GetPosition(sender).X + 8, e.GetPosition(sender).Y + 8, 0, 0)
        End If
    End Sub
    Private Sub CreateDragImage()



        Draglistview.Items.Clear()

        For i = 0 To SelectItems.Count - 1
            Dim si As New ListBoxItem
            si.Content = GetTreeNodeHeader(SelectItems(i).Tag)
            si.Background = Brushes.Transparent

            Draglistview.Items.Add(si)
        Next
    End Sub
End Class

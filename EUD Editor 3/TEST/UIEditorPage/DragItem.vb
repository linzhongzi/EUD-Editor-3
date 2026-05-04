Partial Public Class TreeviewExample
    Private FirstDragMove As Boolean = False
    Private DragSelect As Boolean = False

    Private DragSelectItem As TreeViewItem

    Private Function CheckDragable() As Boolean
        ' 判断所选块类型是否一致。
        ' 判断所选块中是否有无法删除、移动或添加的块。

        Return True
    End Function


    Private IsTop As Boolean = False
    Private Sub MainTreeviewItme_PreviewMouseMove(sender As TreeViewItem, e As MouseEventArgs)
        If IsDrag Then
            DragSelect = True

            Dim CurrentYpos As Integer = e.GetPosition(sender).Y
            Dim Heigth As Integer = sender.Header.ActualHeight

            If CurrentYpos > Heigth / 2 Then
                IsTop = False
            Else
                IsTop = True
            End If



            DragSelectItem = sender


            log.Text = "单击一次 : "
            For i = 0 To SelectItems.Count - 1
                log.Text = log.Text & SelectItems(i).Tag & ", "
            Next
            log.Text = log.Text & vbCrLf & "当前选择的块 : " & sender.Tag & "  拖动完成  IsTop : " & IsTop

        End If
    End Sub
    Private Sub MainTreeviewItme_PreviewMouseUp(sender As TreeViewItem, e As MouseEventArgs)
        If DragSelect Then
            If sender Is DragSelectItem Then
                log.Text = "单击一次 : "

                For i = 0 To SelectItems.Count - 1
                    log.Text = log.Text & SelectItems(i).Tag & ", "
                Next
                log.Text = log.Text & vbCrLf & "当前选择的块 : " & DragSelectItem.Tag & "  拖动完成  IsTop : " & IsTop

                If SelectItems.IndexOf(DragSelectItem) < 0 Then
                    ' 如果拖动的位置不是所选块！


                    For i = 0 To SelectItems.Count - 1
                        ' 如果拖动的位置不是所选块的子项！
                        If CheckChild(SelectItems(i), DragSelectItem) Then
                            For k = 0 To SelectItems.Count - 1
                                DeleteItem(SelectItems(k))
                            Next

                            For k = 0 To SelectItems.Count - 1
                                MoveItem(sender, SelectItems(k), IsTop)
                            Next
                        End If
                    Next
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
        Log.Text = "单击一次 : "
        For i = 0 To SelectItems.Count - 1
            Log.Text = Log.Text & SelectItems(i).Tag & ", "
        Next


        DragTreeview.Items.Clear()

        For i = 0 To SelectItems.Count - 1
            Dim si As New TreeViewItem
            si.Header = "临时文本" 'SelectItems(i).Tag
            si.Background = Brushes.Transparent

            DragTreeview.Items.Add(si)
        Next
    End Sub
End Class

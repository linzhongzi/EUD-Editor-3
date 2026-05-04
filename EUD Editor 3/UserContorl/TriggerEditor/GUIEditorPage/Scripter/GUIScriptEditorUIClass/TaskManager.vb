Partial Public Class GUIScriptEditorUI
    Public Tasklist As New List(Of TETask)
    Public TaskIndex As Integer = -1

    Private Function GetTreeviewPos(treenode As TreeViewItem) As List(Of Integer)
        Dim treenodepos As New List(Of Integer)
        Dim tnode As TreeViewItem = treenode
        'Dim indexstr As String = ""
        While (tnode.Parent.GetType Is GetType(TreeViewItem))
            Dim titem As TreeViewItem = tnode.Parent

            Dim i As Integer = titem.Items.IndexOf(tnode)
            treenodepos.Add(i)
            tnode = titem
            'indexstr = indexstr & "," & i
        End While
        Dim ptree As TreeView = tnode.Parent
        Dim ti As Integer = ptree.Items.IndexOf(tnode)
        'indexstr = indexstr & "," & ti
        treenodepos.Add(ti)

        Return treenodepos
    End Function
    Private Function GetScriptPos(scr As ScriptBlock) As List(Of Integer)
        Dim scrpos As New List(Of Integer)

        Dim tscr As ScriptBlock = scr
        While (MainItems.IndexOf(tscr) = -1)
            Dim tparent As ScriptBlock = tscr.Parent
            Dim i As Integer = tparent.child.IndexOf(tscr)
            scrpos.Add(i)

            tscr = tscr.Parent
        End While
        scrpos.Add(MainItems.IndexOf(tscr))

        Return scrpos
    End Function
    Public Sub AddInsertTask(treenode As TreeViewItem)
        Dim sindex As Integer = TaskIndex + 1
        For i = sindex To Tasklist.Count - 1
            Tasklist.RemoveAt(sindex)
        Next


        Dim treenodepos As List(Of Integer) = GetTreeviewPos(treenode)
        Dim scr As ScriptBlock = treenode.Tag
        Dim scrpos As List(Of Integer) = GetScriptPos(scr)



        Tasklist.Add(New TETask(TETask.TASKTYPE.TASKTYPE_INSERT, Nothing, scr, treenodepos, scrpos))


        TaskIndex = Tasklist.Count - 1


        Undobtn.IsEnabled = Undoable()
        Redobtn.IsEnabled = Redoable()
    End Sub
    Public Sub AddEditTask(newscr As ScriptBlock, oldscr As ScriptBlock, treenode As TreeViewItem)
        Dim sindex As Integer = TaskIndex + 1
        For i = sindex To Tasklist.Count - 1
            Tasklist.RemoveAt(sindex)
        Next

        Dim treenodepos As List(Of Integer) = GetTreeviewPos(treenode)
        Dim scr As ScriptBlock = treenode.Tag
        Dim scrpos As List(Of Integer) = GetScriptPos(scr)

        Tasklist.Add(New TETask(TETask.TASKTYPE.TASKTYPE_EDIT, oldscr, newscr, treenodepos, scrpos))


        TaskIndex = Tasklist.Count - 1

        Undobtn.IsEnabled = Undoable()
        Redobtn.IsEnabled = Redoable()
    End Sub
    Public Sub AddDeleteTask(treenode As TreeViewItem)
        Dim sindex As Integer = TaskIndex + 1
        For i = sindex To Tasklist.Count - 1
            Tasklist.RemoveAt(sindex)
        Next


        Dim treenodepos As List(Of Integer) = GetTreeviewPos(treenode)
        Dim scr As ScriptBlock = treenode.Tag
        Dim scrpos As List(Of Integer) = GetScriptPos(scr)

        Tasklist.Add(New TETask(TETask.TASKTYPE.TASKTYPE_REMOVE, scr, Nothing, treenodepos, scrpos))


        TaskIndex = Tasklist.Count - 1

        Undobtn.IsEnabled = Undoable()
        Redobtn.IsEnabled = Redoable()
    End Sub


    Private Function GetTreeviewPos(ctask As TETask) As TreeViewItem
        If ctask.treepos.Count = 1 Then
            Return Nothing
        End If
        Dim ctreeitem As TreeViewItem = MainTreeview.Items(ctask.treepos.Last)
        For i = ctask.treepos.Count - 2 To 1 Step -1
            ctreeitem = ctreeitem.Items(ctask.treepos(i))
        Next
        Return ctreeitem
    End Function
    Private Function GetScriptBlockPos(ctask As TETask) As ScriptBlock
        If ctask.scrpos.Count = 1 Then
            Return Nothing
        End If
        Dim cscritem As ScriptBlock = MainItems(ctask.scrpos.Last)
        For i = ctask.scrpos.Count - 2 To 1 Step -1
            cscritem = cscritem.child(ctask.scrpos(i))
        Next
        Return cscritem
    End Function

    Public Sub UndoTask()
        Dim ctask As TETask = Tasklist(TaskIndex)

        Dim ctreeitem As TreeViewItem = GetTreeviewPos(ctask)
        Dim cscritem As ScriptBlock = GetScriptBlockPos(ctask)
        Select Case ctask.TType
            Case TETask.TASKTYPE.TASKTYPE_EDIT
                Dim oldscr As ScriptBlock = ctask.oldscr.DeepCopy

                If ctreeitem Is Nothing Or cscritem Is Nothing Then
                    Dim rtreeitem As TreeViewItem = MainTreeview.Items(ctask.treepos.First)
                    rtreeitem.Tag = oldscr
                    rtreeitem.Header = oldscr.GetScriptBlockItem
                    GetItems(ctask.scrpos.First).DuplicationBlock(oldscr)
                Else
                    Dim rtreeitem As TreeViewItem = ctreeitem.Items(ctask.treepos.First)
                    rtreeitem.Tag = oldscr
                    rtreeitem.Header = oldscr.GetScriptBlockItem
                    cscritem.child(ctask.scrpos.First).DuplicationBlock(oldscr)
                End If
            Case TETask.TASKTYPE.TASKTYPE_INSERT
                If ctreeitem Is Nothing Or cscritem Is Nothing Then
                    MainTreeview.Items.RemoveAt(ctask.treepos.First)
                    RemoveAtItems(ctask.scrpos.First)
                Else
                    ctreeitem.Items.RemoveAt(ctask.treepos.First)
                    cscritem.child.RemoveAt(ctask.scrpos.First)
                End If
            Case TETask.TASKTYPE.TASKTYPE_REMOVE
                Dim oldscr As ScriptBlock = ctask.oldscr.DeepCopy

                If ctreeitem Is Nothing Or cscritem Is Nothing Then
                    MainTreeview.Items.Insert(ctask.treepos.First, oldscr.GetTreeviewitem)
                    InsertItems(ctask.scrpos.First, oldscr)
                Else
                    ctreeitem.Items.Insert(ctask.treepos.First, oldscr.GetTreeviewitem)
                    cscritem.InsertChild(ctask.scrpos.First, oldscr)
                End If
        End Select

        TaskIndex -= 1
    End Sub
    Public Sub RedoTask()
        TaskIndex += 1
        Dim ctask As TETask = Tasklist(TaskIndex)

        Dim ctreeitem As TreeViewItem = GetTreeviewPos(ctask)
        Dim cscritem As ScriptBlock = GetScriptBlockPos(ctask)
        Select Case ctask.TType
            Case TETask.TASKTYPE.TASKTYPE_EDIT
                Dim newscr As ScriptBlock = ctask.newscr.DeepCopy

                If ctreeitem Is Nothing Or cscritem Is Nothing Then
                    Dim rtreeitem As TreeViewItem = MainTreeview.Items(ctask.treepos.First)
                    rtreeitem.Tag = newscr
                    rtreeitem.Header = newscr.GetScriptBlockItem
                    GetItems(ctask.scrpos.First).DuplicationBlock(newscr)
                Else
                    Dim rtreeitem As TreeViewItem = ctreeitem.Items(ctask.treepos.First)
                    rtreeitem.Tag = newscr
                    rtreeitem.Header = newscr.GetScriptBlockItem
                    cscritem.child(ctask.scrpos.First).DuplicationBlock(newscr)
                End If
            Case TETask.TASKTYPE.TASKTYPE_INSERT
                Dim newscr As ScriptBlock = ctask.newscr.DeepCopy
                If ctreeitem Is Nothing Or cscritem Is Nothing Then
                    MainTreeview.Items.Insert(ctask.treepos.First, newscr.GetTreeviewitem)
                    InsertItems(ctask.scrpos.First, newscr)
                Else
                    ctreeitem.Items.Insert(ctask.treepos.First, newscr.GetTreeviewitem)
                    cscritem.InsertChild(ctask.scrpos.First, newscr)
                End If
            Case TETask.TASKTYPE.TASKTYPE_REMOVE
                If ctreeitem Is Nothing Or cscritem Is Nothing Then
                    MainTreeview.Items.RemoveAt(ctask.treepos.First)
                    RemoveAtItems(ctask.scrpos.First)
                Else
                    ctreeitem.Items.RemoveAt(ctask.treepos.First)
                    cscritem.child.RemoveAt(ctask.scrpos.First)
                End If
        End Select
    End Sub

    Public Function GetRepeatCount(isRedo As Boolean) As Integer
        Dim ctask As TETask
        If isRedo Then
            ctask = Tasklist(TaskIndex + 1)
        Else
            ctask = Tasklist(TaskIndex)
        End If

        Return ctask.RepeatCount
    End Function

    Public Sub SetRepeatCount(repeatcount As Integer)
        For i = 0 To repeatcount - 1
            Dim ctask As TETask = Tasklist(TaskIndex - i)
            ctask.RepeatCount = repeatcount
        Next
    End Sub

    Public Function Undoable() As Boolean
        Return TaskIndex >= 0
    End Function
    Public Function Redoable() As Boolean
        Return TaskIndex < Tasklist.Count - 1
    End Function



    Public Class TETask
        ' 树视图位置
        ' 脚本位置

        ' 脚本文件1
        ' 脚本文件2

        ' 何时放入以及何时放回

        Public TType As TASKTYPE
        Public Enum TASKTYPE
            TASKTYPE_INSERT
            TASKTYPE_REMOVE
            TASKTYPE_EDIT
        End Enum

        Public RepeatCount As Integer = 0

        ' 任务类型
        ' 1. 删除
        ' 放入已删除的脚本。
        ' 记住删除的位置。
        ' # 还原时，将脚本放入删除的位置。


        ' 2. 修改
        ' 记住修改的位置。
        ' # 还原时，修改修改位置上的脚本。


        ' 3. 添加
        ' 放入已添加的脚本。
        ' 记住添加的位置。
        ' # 删除添加位置上的脚本。
        ' 添加项目时，从工作列表中的当前位置开始，删除后续所有项目，然后从那里开始放入。

        Public Sub New(_tType As TASKTYPE, _oldscr As ScriptBlock, _newscr As ScriptBlock, _treepos As List(Of Integer), _scrpos As List(Of Integer))
            TType = _tType

            If _oldscr IsNot Nothing Then
                oldscr = _oldscr.DeepCopy
            End If

            If _newscr IsNot Nothing Then
                newscr = _newscr.DeepCopy
            End If

            treepos = _treepos
            scrpos = _scrpos
        End Sub

        Public oldscr As ScriptBlock
        Public newscr As ScriptBlock

        Public treepos As List(Of Integer)
        Public scrpos As List(Of Integer)
    End Class
End Class

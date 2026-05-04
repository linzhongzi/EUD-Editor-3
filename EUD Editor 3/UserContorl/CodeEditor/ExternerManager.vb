Imports System.Text.RegularExpressions
Imports System.Windows.Forms.VisualStyles

Partial Public Class CodeEditor

    Private ExternFiles As List(Of ExternFile)

    Public Shared Function _FildFile(currentPath As TEFile, paths() As String) As TEFile
        For i = 0 To paths.Count - 1
            'Log.Text = Log.Text & "Path" & i & " : " & paths(i) & "  "

            If paths(i) = "" Then ' 上级文件夹
                If i = 0 Then
                    currentPath = currentPath.Parent.Parent
                Else
                    currentPath = currentPath.Parent
                End If
                Continue For
            End If


            ' currentPath.Parent 该文件的上级文件
            Dim isFind As Boolean = False

            If currentPath.FileType <> TEFile.EFileType.Folder Then
                currentPath = currentPath.Parent
            End If

            For k = 0 To currentPath.FolderCount - 1
                If currentPath.Folders(k).FileName = paths(i) Then ' 如果选择了
                    currentPath = currentPath.Folders(k)
                    isFind = True
                    Exit For
                End If
            Next
            If Not isFind Then
                If currentPath.FileType <> TEFile.EFileType.Folder Then
                    currentPath = currentPath.Parent
                End If

                For k = 0 To currentPath.FileCount - 1
                    If currentPath.Files(k).FileName = paths(i) Then ' 如果找到了文件
                        currentPath = currentPath.Files(k)
                        Exit For
                    End If
                Next
            End If
        Next

        Return currentPath
    End Function
    Public Shared Function FineFile(TEFile As TEFile, path As String) As TEFile
        Dim paths() As String = path.Split(".")

        'Log.Text = Log.Text & "Path : " & path & "   "
        Dim currentPath As TEFile = TEFile
        currentPath = _FildFile(currentPath, paths)


        If path <> currentPath.FileName Then
            currentPath = pjData.TEData.PFIles
            currentPath = _FildFile(currentPath, paths)

        End If





        Return currentPath
        ' 虽然分为选择的是文件夹还是文件，但暂时先获取。
        'Log.Text = Log.Text & currentPath.FileName & vbCrLf
    End Function


    Public Sub ExternerLoader()
        If TEFile IsNot Nothing Then
            For i = 0 To ExternFiles.Count - 1
                ExternFiles(i).CheckFlag = False
            Next



            'Log.Text = ""

            Dim Str As String = TextEditor.Text

            Dim fregex As New Regex("import\s+(.*);")

            Dim matches As MatchCollection = fregex.Matches(Str)

            'MsgBox(TEFile.Parent.FileName)
            For i = 0 To matches.Count - 1
                Dim filePath As String = matches(i).Groups(1).Value.Trim

                Dim val() As String = filePath.Split(" ")

                Dim nameSpaceName As String = ""
                Dim FileName As String = ""
                If val.Count = 1 Then ' 如果没有 As 指定符
                    nameSpaceName = val(0).Trim
                    FileName = val(0).Trim
                ElseIf val(1) = "as" Then
                    nameSpaceName = val.Last.Trim
                    FileName = val.First.Trim
                End If
                nameSpaceName = nameSpaceName.Split(".").Last

                Dim tTEfile As TEFile = Nothing
                Try
                    Dim iscmp As Boolean = False
                    Dim externList As List(Of String) = tescm.GetExternFileList
                    Dim realPath As List(Of String) = tescm.GetExternFileList(True)
                    For k = 0 To externList.Count - 1
                        If externList(k) = FileName & ".eps" Then
                            Dim SCATEFile As New TEFile(FileName, TEFile.EFileType.CUIEps)
                            CType(SCATEFile.Scripter, CUIScriptEditor).StringText = My.Computer.FileSystem.ReadAllText(realPath(k))

                            iscmp = True
                            tTEfile = SCATEFile
                            Exit For
                        End If
                    Next
                    If Not iscmp Then
                        tTEfile = FineFile(TEFile, FileName)
                    End If
                Catch ex As Exception
                    Continue For
                End Try

                'Log.Text = tTEfile.FileName & vbCrLf & "ExternFilesCount : " & ExternFiles.Count
                Dim CheckFlag As Boolean = False
                For k = 0 To ExternFiles.Count - 1
                    If ExternFiles(k).TEFile Is tTEfile Then ' 如果文件相同？
                        ExternFiles(k).CheckFlag = True
                        ExternFiles(k).nameSpaceName = nameSpaceName
                        CheckFlag = True

                        If Not ExternFiles(k).CheckFIleChange Then ' 检查文件，如果不同
                            ExternFiles(k).DateRefresh()
                            ' MsgBox("文件已更新 : " & tTEfile.FileName)
                            'Log.Text = TEFile.FileName & " " & ExternFiles(k).LastDate.ToString
                        End If
                        Exit For
                    End If
                Next

                If Not CheckFlag Then 
                ' 如果不在文件列表中，则新增。
                ' Log.Text = Log.Text & vbCrLf & "添加项目"
                    ExternFiles.Add(New ExternFile(tTEfile, nameSpaceName))
                    ' MsgBox("文件已更新 : " & tTEfile.FileName)
                End If


                'ExternFunc.LoadExtern(nameSpaceName, FileName, TEFile)
            Next

            Dim index As Integer = 0
            For i = 0 To ExternFiles.Count - 1
                If Not ExternFiles(index).CheckFlag Then ' 如果不在列表中时
                    ExternFiles.RemoveAt(index)
                Else
                    index += 1
                End If
            Next
        End If
    End Sub
End Class


Public Class ExternFile
    ' 管理外部文件的地方。
    ' 判断外部文件的最后修改日期。



    '
    Public Property nameSpaceName As String


    Public ReadOnly Property Funcs As CFunc



    Public Property TEFile As TEFile ' 指定的TE文件


    Public LastDate As Date

    Public Property CheckFlag As Boolean = False

    Public Sub New(tTEFile As TEFile, tnameSpaceName As String)
        TEFile = tTEFile
        nameSpaceName = tnameSpaceName
        CheckFlag = True

        Funcs = New CFunc

        DateRefresh()
    End Sub

    Public Sub DateRefresh()
        ' 读取文件并创建 CFunc。
        Funcs.Init()
        Try
            Funcs.LoadFunc(TEFile.Scripter.GetStringText)
        Catch ex As Exception
            Funcs.Init()
        End Try


        'Try
        ' MsgBox("加载数据 : " & TEFile.FileName)
        'Catch ex As Exception
        'End Try


        If TEFile IsNot Nothing Then
            LastDate = TEFile.LastDate ' 创建时记录最后日期。
        End If
    End Sub



    Public Function CheckFIleChange() As Boolean

        If TEFile IsNot Nothing Then
            Return LastDate.ToString = TEFile.LastDate.ToString
        Else
            Return False
        End If
    End Function
End Class

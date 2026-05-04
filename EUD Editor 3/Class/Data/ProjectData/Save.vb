Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary

Partial Public Class ProjectData
    Private SaveData As SaveableData

    Public Function Save(Optional IsSaveAs As Boolean = False) As Boolean
        SaveData.LastVersion = pgData.Version
        TETempData.SaveTabitems()
        TERefreshTabITem()

        Dim lastFileName As String = Filename


        If IsSaveAs = True Then ' 如果是另存为的情况
            Tool.SaveProjectDialog.FileName = SafeFilename

            Dim exten As String() = Tool.SaveProjectDialog.Filter.Split("|")
            For i = 1 To exten.Count - 1 Step 2
                If Extension = exten(i).Split(".").Last Then
                    Tool.SaveProjectDialog.FilterIndex = ((i - 1) \ 2) + 1
                    Exit For
                End If

            Next

            Tool.SaveProjectDialog.InitialDirectory = pgData.Setting(ProgramData.TSetting.SavePath)
            If Tool.SaveProjectDialog.ShowDialog() = Forms.DialogResult.OK Then
                Filename = Tool.SaveProjectDialog.FileName ' 替换文件名
                pgData.Setting(ProgramData.TSetting.SavePath) = Path.GetDirectoryName(Filename)
            Else
                Return False
            End If
        End If

        If Not Extension = "e3s" And Not Extension = "e2s" Then
            Filename = ""
        End If

        If Filename = "" Then ' 新建文件
            Tool.SaveProjectDialog.FileName = SafeFilename
            Tool.SaveProjectDialog.InitialDirectory = pgData.Setting(ProgramData.TSetting.SavePath)
            If Tool.SaveProjectDialog.ShowDialog() = Forms.DialogResult.OK Then
                Filename = Tool.SaveProjectDialog.FileName ' 替换文件名
                pgData.Setting(ProgramData.TSetting.SavePath) = Path.GetDirectoryName(Filename)
            Else
                Return False
            End If
        End If


        If extension = "e3s" Then
            Dim stm As Stream = File.Open(Filename, FileMode.Create, FileAccess.ReadWrite)
            Dim bf As BinaryFormatter = New BinaryFormatter()
            bf.Serialize(stm, Me.SaveData)
            stm.Close()
        Else
            Dim dialog As MsgBoxResult = Tool.CustomMsgBox("兼容性警告" & vbCrLf &
                       "保存e2s文件时可能发生部分错误。" & vbCrLf &
                       "插件保存失败" & vbCrLf &
                       "TE加载保存失败", MessageBoxButton.OKCancel, MessageBoxImage.Error)


            If dialog = MsgBoxResult.Ok Then
                FileSystem.FileCopy(lastFileName, Filename & ".e2sbackup")
            ElseIf dialog = MsgBoxResult.Cancel Then
                Return False
            End If


            Lagacy.LagacySaveLoad.Save(Filename)
        End If

        BackUpFile(Filename)


        tIsLoad = True
        tIsDirty = False

        If Not pgData.IsCompilng Then
            If AutoBuild Then
                pjData.EudplibData.Build()
            End If
        End If


        Return True
    End Function
End Class

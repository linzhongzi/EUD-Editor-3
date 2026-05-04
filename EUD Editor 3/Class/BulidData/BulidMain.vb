Imports System.ComponentModel
Imports System.IO
Imports System.Media
Imports System.Text
Imports System.Windows.Threading

Partial Public Class BuildData

    ' 1. 从内嵌文件在构建时提取的形式
    ' 不内嵌文件，而是提取到外部
    ' 2. 不使用外部提取的文件，而是使用翻转安装文件夹或直接指定位置，使该插件默认可用

    ' 3. 决定在何处创建临时文件，如果文件夹不存在，则保存在默认文件夹中。
    ' 如果设置为与地图文件相同的位置，则地图文件将自动保存为相对路径！

    ' 如果临时文件指定方式为用户指定
    ' 如果指定的文件夹与地图文件夹相同，则以相对路径保存。

    ' 如果指定为地图文件夹，则如果两个地图的文件夹相同，则以相对路径保存。
    ' 如果两个地图的文件夹不同，则保存在默认文件夹中。
    Private MainThread As BackgroundWorker

    Private eudplibShutDown As Boolean
    Public Sub Build(Optional isEdd As Boolean = False)
        If pgData.IsCompilng = True Then
            If eudplibprocess IsNot Nothing Then
                If Not eudplibprocess.HasExited Then
                    eudplibShutDown = True
                    eudplibprocess.Kill()
                End If
            End If
        Else
            eudplibShutDown = False
            If pjData.IsDirty And pjData.Filename <> "" Then ' 新建文件
                pjData.Save()
            End If



            'Tool.GetRelativePath(EudPlibFilePath & "\EUDEditor.eds", pjData.OpenMapName)
            ' Tool.GetRelativePath("zzz\asd\c\哈哈.txt", "zzz\asd\bcx\aqw\zxv\嗨.txt")
            ' Tool.GetRelativePath("zzz\asd\bcx\aqw\zxv\嗨.txt", "zzz\asd\c\哈哈.txt")

            If CheckBuildable() Then
                If Not SoundConverter() Then
                    Tool.ErrorMsgBox("Sound파일 변환 에러")
                    pgData.IsCompilng = False
                    pgData.isEddCompile = False
                    Tool.RefreshMainWindow()

                    Return
                End If


                If Not SoundSCAScriptConverter() Then
                    Tool.ErrorMsgBox("SCAScriptSound파일 변환 에러")
                    pgData.IsCompilng = False
                    pgData.isEddCompile = False
                    Tool.RefreshMainWindow()

                    Return
                End If



                Me.IsEdd = isEdd

                If pjData.TEData.SCArchive.IsUsed Then
                    ' 如果已登录
                    If pjData.TEData.SCArchive.IsLogin Then
                        If Not pjData.TEData.SCArchive.CheckLoginAccount() Then
                            Dim SCALoginWindow As New SCASettingWindows

                            SCALoginWindow.ShowDialog()

                            ' 如果登录失败
                            If Not SCALoginWindow.Result Then
                                Tool.ErrorMsgBox(Tool.GetText("Error SCA") & vbCrLf & "계정 정보가 올바르지 않습니다. SCA사용이 해제됩니다.")
                                pgData.IsCompilng = False
                                pgData.isEddCompile = False
                                Tool.RefreshMainWindow()
                                Return
                            Else
                                pjData.TEData.SCArchive.IsUsed = True
                            End If
                        End If
                    Else
                        Dim SCALoginWindow As New SCASettingWindows

                        SCALoginWindow.ShowDialog()

                        ' 如果登录失败
                        If Not SCALoginWindow.Result Then
                            Tool.ErrorMsgBox(Tool.GetText("Error SCA") & vbCrLf & "계정 정보가 올바르지 않습니다. SCA사용이 해제됩니다.")
                            pgData.IsCompilng = False
                            pgData.isEddCompile = False
                            Tool.RefreshMainWindow()
                            Return
                        Else
                            pjData.TEData.SCArchive.IsUsed = True
                        End If
                    End If
                End If



                MainThread = New BackgroundWorker()
                AddHandler MainThread.DoWork, AddressOf BuildProgressWorker
                AddHandler MainThread.RunWorkerCompleted, AddressOf BuildProgressComplete

                MainThread.RunWorkerAsync()



                'MainThread = New System.Threading.Thread(AddressOf BuildProgress)
                'MainThread.Start(isEdd)
            End If
            ' MsgBox("最终文件夹 :" & TempFloder)
        End If
    End Sub

    Private IsEdd As Boolean
    Private Sub BuildProgressWorker(sender As Object, e As DoWorkEventArgs)
        BuildProgress(IsEdd)
    End Sub


    Private Sub BuildProgressComplete(sender As Object, e As RunWorkerCompletedEventArgs)
        If e.Error IsNot Nothing Then
            Tool.CustomMsgBox(Tool.GetText("CompileFail") + vbCrLf + e.Error.Message, MessageBoxButton.OK, MessageBoxImage.Error)
            pgData.IsCompilng = False
            pgData.isEddCompile = False
            Tool.RefreshMainWindow()

            Return
        End If

        If eudplibShutDown Then
            Tool.CustomMsgBox(Tool.GetText("Error CompileStop"), MessageBoxButton.OK, MessageBoxImage.Error)
            Return
        End If
        If macro.macroErrorList.Count <> 0 Then
            Dim m As String = ""
            For Each msg In macro.macroErrorList
                m = m + msg + vbCrLf
            Next
            Tool.ErrorMsgBox("Lua Script Inner Error", m)
        End If
    End Sub




    ' 判断是否可以构建（连接必需程序）
    Private Function CheckBuildable() As Boolean
        If Not My.Computer.FileSystem.FileExists(pjData.OpenMapName) Then
            If Tool.CustomMsgBox(Tool.GetText("Error OpenMap is not exist reset"), MessageBoxButton.OKCancel) = MsgBoxResult.Ok Then
                If Not Tool.OpenMapSet Then
                    Tool.ErrorMsgBox(Tool.GetText("Error CompileFail OpenMap is not exist!"))
                    Return False
                End If
                Tool.RefreshMainWindow()
            Else
                Tool.ErrorMsgBox(Tool.GetText("Error CompileFail OpenMap is not exist!"))
                Return False
            End If
        End If
        If Not My.Computer.FileSystem.DirectoryExists(pjData.SaveMapdirectory) Then
            If Tool.CustomMsgBox(Tool.GetText("Error SaveMap is not exist reset"), MessageBoxButton.OKCancel) = MsgBoxResult.Ok Then
                If Not Tool.SaveMapSet Then
                    Tool.ErrorMsgBox(Tool.GetText("Error CompileFail SaveMap is not exist!"))
                    Return False
                End If
                Tool.RefreshMainWindow()
            Else
                Tool.ErrorMsgBox(Tool.GetText("Error CompileFail SaveMap is not exist!"))
                Return False
            End If
        End If
        If Not My.Computer.FileSystem.FileExists(pgData.Setting(ProgramData.TSetting.euddraft)) Then
            If Tool.CustomMsgBox(Tool.GetText("Error euddraft is not exist reset"), MessageBoxButton.OKCancel) = MsgBoxResult.Ok Then
                Dim opendialog As New System.Windows.Forms.OpenFileDialog With {
                .Filter = "euddraft.exe|euddraft.exe",
                .FileName = "euddraft.exe",
                .Title = Tool.GetText("euddraftExe Select")
            }


                If opendialog.ShowDialog() = Forms.DialogResult.OK Then
                    pgData.Setting(ProgramData.TSetting.euddraft) = opendialog.FileName
                    Tool.RefreshMainWindow()
                Else
                    Tool.ErrorMsgBox(Tool.GetText("Error CompileFail euddraft is not exist!"))
                    Return False
                End If
            Else
                Tool.ErrorMsgBox(Tool.GetText("Error CompileFail euddraft is not exist!"))
                Return False
            End If
        End If
        If pjData.TempFileLoc <> "0" And pjData.TempFileLoc <> "1" Then
            If pjData.TempFileLoc = "2" Then
                If Not My.Computer.FileSystem.FileExists(pjData.Filename) Then
                    Tool.ErrorMsgBox(Tool.GetText("Error CompileFail NotSaved Project"))
                    Return False
                End If
            Else
                If Not My.Computer.FileSystem.DirectoryExists(pjData.TempFileLoc) Then
                    If Tool.CustomMsgBox(Tool.GetText("Error TempFolder is not exist reset"), MessageBoxButton.OKCancel) = MsgBoxResult.Ok Then
                        Dim folderSelect As New System.Windows.Forms.FolderBrowserDialog

                        If folderSelect.ShowDialog = Forms.DialogResult.OK Then
                            pjData.TempFileLoc = folderSelect.SelectedPath
                        Else
                            Tool.ErrorMsgBox(Tool.GetText("Error CompileFail TempFolder is not exist!"))
                            Return False
                        End If

                        If Not My.Computer.FileSystem.DirectoryExists(pjData.TempFileLoc) Then
                            Tool.ErrorMsgBox(Tool.GetText("Error CompileFail TempFolder is not exist!"))
                            Return False
                        End If
                    Else
                        Tool.ErrorMsgBox(Tool.GetText("Error CompileFail TempFolder is not exist!"))
                        Return False
                    End If
                End If
            End If
        End If

        If My.Computer.FileSystem.FileExists(pjData.SaveMapName) Then
            If IsFileLocked(New FileInfo(pjData.SaveMapName)) Then
                Tool.ErrorMsgBox(Tool.GetText("Error WriteMapFile"))
                Return False
            End If
        End If


        Return True
    End Function
    Private Function IsFileLocked(ByVal file As FileInfo) As Boolean
        Dim stream As FileStream = Nothing

        Try
            stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None)
        Catch __unusedIOException1__ As IOException
            Return True
        Finally
            If stream IsNot Nothing Then stream.Close()
        End Try

        Return False
    End Function





    ' ########################### 构建工作主函数 ############################
    Private Sub BuildProgress(isEdd As Boolean)
        ' 先阻止程序关闭，并显示“正在构建”的提示和进度条。
        isFreezeUse = False

        pgData.IsCompilng = True
        pjData.RefreshOutputWriteTime()
        pgData.isEddCompile = isEdd
        Tool.RefreshMainWindow()


        WriteExternFIle()


        WriteBGMData()
        WriteDotData()

        If pjData.TEData.SCArchive.IsUsed Then
            If Not ConnecterStart() Then
                pgData.IsCompilng = False
                pgData.isEddCompile = False
                Tool.RefreshMainWindow()
                Return
            End If
        End If



        ' 必须创建各个临时文件
        If pjData.TEData.SCArchive.IsUsed Then
            GetEntryPoint()
        End If

        ' 写入Req数据
        WriteRequireData()


        WriteTEFile()
        If macro.macroErrorList.Count <> 0 Then
            pgData.IsCompilng = False
            pgData.isEddCompile = False
            Tool.RefreshMainWindow()
            Return
        End If


        If pjData.TEData.SCArchive.IsUsed Then
            WriteSCAScript()
        End If




        ' 创建一个 Python 脚本来保存数据配置文件
        WriteDatFile()
        WriteExtraDatFile()

        ' 写入Tbl文件（根据选项）
        If pjData.UseCustomtbl Then
            WriteTbl()
        End If


        ' 插入TE相关内容
        ' MSQC, 聊天识别, Unlimiter, AI脚本替换
        ' CT（根据Tbl选项）

        ' 创建eds文件并运行该文件
        Try
            WriteedsFile(isEdd)
        Catch ex As Exception
            Tool.ErrorMsgBox(ex.ToString())
            pgData.IsCompilng = False
            pgData.isEddCompile = False
            Tool.RefreshMainWindow()
            Return
        End Try
        Dim isSucces As Boolean = Starteds(isEdd)


        'Threading.Thread.Sleep(3000)






        pgData.IsCompilng = False
        pgData.isEddCompile = False
        Tool.RefreshMainWindow()

        If isSucces Then
            If pjData.TEData.SCArchive.IsUsed Then
                'If Not isFreezeUse Then
                '    If Not WriteSCADataFile() Then
                '        Tool.ErrorMsgBox(Tool.GetText("Error WriteMapFile"))
                '        Return
                '    End If
                'End If

                If Not WriteChecksum() Then
                    Tool.ErrorMsgBox(Tool.GetText("Error WriteMapFile"))
                    Return
                End If
            End If




            Dim notificationSound As New SoundPlayer(My.Resources.success)
            notificationSound.PlaySync()
        End If
    End Sub

    Private OutputString As String
    Private ErrorString As String



    Private isFreezeUse As Boolean
    Private eudplibprocess As Process
    Private Function Starteds(isEdd As Boolean) As Boolean
        Dim StandardOutput As String = ""
        Dim StandardError As String = ""

        OutputString = ""
        ErrorString = ""

        Dim RestartCount As Integer
        While True
            eudplibprocess = ProcessStart(isEdd)
            'Dim StandardOutputStream As StreamReader = eudplibprocess.StandardOutput



            If Not isEdd Then
                While Not eudplibprocess.HasExited
                    'eudplibprocess.StandardInput.Write(vbCrLf)

                    'StandardOutput = StandardOutput & StandardOutputStream.ReadToEnd
                    'MsgBox(StandardOutput)
                    'MsgBox(OutputString)
                    Threading.Thread.Sleep(200)

                    'Threading.Thread.Sleep(1000)


                    StandardOutput = OutputString
                    StandardError = ErrorString
                    eudplibprocess.StandardInput.Write(vbCrLf)
                    If StandardOutput.IndexOf("Freeze - prompt enabled") >= 0 Then
                        'If pjData.TEData.SCArchive.IsUsed Then
                        '    If Not WriteSCADataFile() Then
                        '        Tool.ErrorMsgBox(Tool.GetText("Error WriteMapFile"))
                        '        Return False
                        '    Else
                        '        isFreezeUse = True
                        '    End If
                        'End If
                        'eudplibprocess.Kill()
                        eudplibprocess.StandardInput.Write(vbCrLf)
                    End If
                    If eudplibShutDown Then
                        'Tool.CustomMsgBox(Tool.GetText("Error CompileStop"), MessageBoxButton.OK, MessageBoxImage.Error)
                        Return False
                    End If
                End While
                StandardOutput = OutputString
                StandardError = ErrorString

                If InStr(StandardError, "zipimport.ZipImportError: can't decompress data; zlib not available") <> 0 Then
                    ' MsgBox("构建失败。重试中，重试次数: " & RestartCount & vbCrLf & StandardOutput & StandardError)
                    StandardError = ""
                    RestartCount += 1
                    Continue While
                End If
                'Dim tempstr() As String = StandardOutput.Split(vbCrLf)

                'MsgBox(tempstr(tempstr.Count - 2))
                ' 临时判断
                If (StandardOutput.IndexOf("Output scenario.chk") < 0) And (StandardOutput.IndexOf("출력된 scenario.chk 크기") < 0) Then
                    If eudplibShutDown Then
                        'Tool.CustomMsgBox(Tool.GetText("Error CompileStop"), MessageBoxButton.OK, MessageBoxImage.Error)
                    Else
                        GetMainWindow.LogTextBoxView(StandardOutput & vbCrLf & "=============================================================" & vbCrLf & StandardError, True)

                        GetMainWindow.ErrorHandleStart(StandardOutput, StandardError)
                    End If
                    Return False
                Else
                    ' 如果构建成功
                    If pjData.ViewLog Then
                        GetMainWindow.LogTextBoxView(StandardOutput, False)
                    End If
                    GetMainWindow.ErrorHandleCloseStart()
                End If
            Else
                While Not eudplibprocess.HasExited
                    Threading.Thread.Sleep(100)
                End While
            End If

            Exit While
        End While
        Return True
    End Function



    Private Function ProcessStart(isEdd As Boolean) As Process
        Dim process As New Process
        Dim startInfo As New ProcessStartInfo

        Dim filename As String
        If isEdd Then
            filename = EddFilePath
        Else
            filename = EdsFilePath
        End If

        startInfo.FileName = pgData.Setting(ProgramData.TSetting.euddraft)
        startInfo.Arguments = """" & filename & """"

        If Not isEdd Then
            startInfo.StandardOutputEncoding = Encoding.UTF8
            startInfo.StandardErrorEncoding = Encoding.UTF8
            startInfo.RedirectStandardOutput = True
            startInfo.RedirectStandardInput = True
            startInfo.RedirectStandardError = True
            startInfo.WindowStyle = ProcessWindowStyle.Hidden
            startInfo.CreateNoWindow = True
            startInfo.UseShellExecute = False
        End If


        process.StartInfo = startInfo



        process.Start()
        AddHandler process.OutputDataReceived, AddressOf OutputReader
        AddHandler process.ErrorDataReceived, AddressOf ErrorReader
        If Not isEdd Then
            process.BeginOutputReadLine()
            process.BeginErrorReadLine()
        End If

        'process.WaitForExit()
        Return process
    End Function
    Public Sub OutputReader(sender As Object, e As DataReceivedEventArgs)
        OutputString = OutputString & e.Data & vbCrLf
    End Sub
    Public Sub ErrorReader(sender As Object, e As DataReceivedEventArgs)
        ErrorString = ErrorString & e.Data & vbCrLf
    End Sub


End Class

Imports ICSharpCode.AvalonEdit
Imports MaterialDesignThemes.Wpf
Imports Newtonsoft.Json.Linq

Public Class TETextEditorPage
    Private PTEFile As TEFile
    Public ReadOnly Property TEFile As TEFile
        Get
            Return PTEFile
        End Get
    End Property

    Public Function CheckTEFile(tTEfile As TEFile) As Boolean
        Return (tTEfile Is PTEFile)
    End Function


    Public Sub Deactivated()

    End Sub

    Public Sub RefreshData()
        'Dim TString As String = PTEFile.RefreshData()
        'If TString <> "" Then
        '    If pgData.Setting(ProgramData.TSetting.TestCodeEditorUse) = "True" Then
        '        NewTextEditor.SetFilePath = PTEFile.FileName
        '        TextEditor.Text = TString
        '    Else
        '        OldTextEditor.ExternerLoader()
        '    End If
        'End If
    End Sub



    Public Sub New(tTEFile As TEFile, Optional highLightLine As Integer = -1, Optional startoffset As Integer = 0)

        ' 设计器需要此调用。
        InitializeComponent()

        ' 在 InitializeComponent() 调用后添加初始化代码。
        PTEFile = tTEFile

        TextEditor.Text = TEFile.Scripter.GetStringText()
    End Sub


    Public Sub SaveData()
        CType(TEFile.Scripter, RawTextScriptEditor).StringText = TextEditor.Text

        TEFile.LastDataRefresh()
    End Sub


    Private Sub UserControl_Loaded(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub UserControl_Unloaded(sender As Object, e As RoutedEventArgs)
        If TEFile.FileType = TEFile.EFileType.CUIEps Then

            CType(TEFile.Scripter, RawTextScriptEditor).StringText = TextEditor.Text

        End If
    End Sub


    Private Sub TextEditor_TextChanged(sender As Object, e As TextChangedEventArgs)
        pjData.SetDirty(True)

    End Sub
End Class

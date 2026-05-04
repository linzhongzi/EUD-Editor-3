Public Class TECUIPage_Old
    Private PTEFile As TEFile
    Public ReadOnly Property TEFile As TEFile
        Get
            Return PTEFile
        End Get
    End Property

    Public Function CheckTEFile(tTEfile As TEFile) As Boolean
        Return (tTEfile Is PTEFile)
    End Function


    Public Sub RefreshData()
        Dim TString As String = PTEFile.RefreshData()
        If TString <> "" Then
            TextEditor.Text = TString
            TextEditor.ExternerLoader()
        End If
    End Sub



    Public Sub New(tTEFile As TEFile, Optional highLightLine As Integer = -1)

        ' 设计器需要此调用。
        InitializeComponent()

        ' 在 InitializeComponent() 调用后添加初始化代码。
        PTEFile = tTEFile
        TextEditor.Init(tTEFile)
        TextEditor.Text = CType(TEFile.Scripter, CUIScriptEditor).StringText

        If highLightLine > -1 Then
            TextEditor.LineHighLight(highLightLine)
        End If
    End Sub


    Public Sub SaveData()
        CType(TEFile.Scripter, CUIScriptEditor).StringText = TextEditor.Text
        TEFile.LastDataRefresh()
    End Sub



    Private Sub UserControl_Loaded(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub UserControl_Unloaded(sender As Object, e As RoutedEventArgs)
        If TEFile.FileType = TEFile.EFileType.CUIEps Then
            CType(TEFile.Scripter, CUIScriptEditor).StringText = TextEditor.Text
        End If
    End Sub
End Class

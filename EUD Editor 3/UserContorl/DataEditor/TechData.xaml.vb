Public Class TechData
    Public Sub ReLoad(DatFiles As SCDatFiles.DatFiles, ObjectID As Integer)
        UsedCodeList.ReLoad(SCDatFiles.DatFiles.techdata, ObjectID)

        Tech_Data.ReLoad(DatFiles, ObjectID)
        RequireUseData.ReLoad(DatFiles, ObjectID, True)
        RequireResearchData.ReLoad(DatFiles, ObjectID)

        TypeListBox.DataContext = pjData.BindingManager.UIManager(SCDatFiles.DatFiles.techdata, ObjectID)
    End Sub

    Public Sub New(tObjectID As Integer)

        ' 设计器需要此调用。
        InitializeComponent()

        ' 在 InitializeComponent() 调用后添加初始化代码。
        UsedCodeList.Init(SCDatFiles.DatFiles.techdata, tObjectID)

        Tech_Data = New Tech_Data(tObjectID)
        RequireUseData = New RequireData(SCDatFiles.DatFiles.techdata, tObjectID, True)
        RequireResearchData = New RequireData(SCDatFiles.DatFiles.techdata, tObjectID)

        _Default.Content = Tech_Data
        ResearchRequir.Content = RequireResearchData
        UseRequir.Content = RequireUseData

        TypeListBox.DataContext = pjData.BindingManager.UIManager(SCDatFiles.DatFiles.techdata, tObjectID)
    End Sub

    Private Tech_Data As Tech_Data
    Private RequireUseData As RequireData
    Private RequireResearchData As RequireData

    Private Sub ListBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        MainTab.SelectedIndex = TypeListBox.SelectedIndex
    End Sub
End Class

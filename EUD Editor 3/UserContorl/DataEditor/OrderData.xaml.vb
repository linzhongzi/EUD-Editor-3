Public Class OrderData
    Public Sub ReLoad(DatFiles As SCDatFiles.DatFiles, ObjectID As Integer)
        UsedCodeList.ReLoad(SCDatFiles.DatFiles.orders, ObjectID)

        Order_Data.ReLoad(DatFiles, ObjectID)
        RequireData.ReLoad(DatFiles, ObjectID)

        TypeListBox.DataContext = pjData.BindingManager.UIManager(SCDatFiles.DatFiles.orders, ObjectID)
    End Sub

    Public Sub New(tObjectID As Integer)

        ' 设计器需要此调用。
        InitializeComponent()

        ' 在 InitializeComponent() 调用后添加初始化代码。
        UsedCodeList.Init(SCDatFiles.DatFiles.orders, tObjectID)

        Order_Data = New Order_Data(tObjectID)
        RequireData = New RequireData(SCDatFiles.DatFiles.orders, tObjectID)

        _Default.Content = Order_Data
        Requir.Content = RequireData

        TypeListBox.DataContext = pjData.BindingManager.UIManager(SCDatFiles.DatFiles.orders, tObjectID)
    End Sub

    Private Order_Data As Order_Data
    Private RequireData As RequireData

    Private Sub ListBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        MainTab.SelectedIndex = TypeListBox.SelectedIndex
    End Sub
End Class

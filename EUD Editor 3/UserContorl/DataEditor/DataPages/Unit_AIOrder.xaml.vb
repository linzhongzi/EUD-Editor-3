Public Class Unit_AIOrder
    Private Const UnitDatPage As Integer = 5

    Private DatFiles As SCDatFiles.DatFiles = SCDatFiles.DatFiles.units

    Public Property ObjectID As Integer


    Public Sub New(tObjectID As Integer)
        ' 设计器需要此调用。
        InitializeComponent()

        ' 在 InitializeComponent() 调用后添加初始化代码。
        DataContext = pjData
        ObjectID = tObjectID

        NameBar.Init(ObjectID, SCDatFiles.DatFiles.units, UnitDatPage)

        CAI.Init(DatFiles, ObjectID, CAI.Tag, 80)
        HAI.Init(DatFiles, ObjectID, HAI.Tag, 80)
        RTI.Init(DatFiles, ObjectID, RTI.Tag, 80)
        AU.Init(DatFiles, ObjectID, AU.Tag, 80)
        AM.Init(DatFiles, ObjectID, AM.Tag, 80)

        RCA.Init(DatFiles, ObjectID, RCA.Tag)
        AI.Init(DatFiles, ObjectID, AI.Tag, 300)

        'test.Text = pjData.Dat.Data(SCDatFiles.DatFiles.units, test.Tag, ObjectID)
    End Sub
    Public Sub ReLoad(DatFiles As SCDatFiles.DatFiles, ObjectID As Integer)
        ObjectID = ObjectID

        NameBar.ReLoad(ObjectID, DatFiles, UnitDatPage)

        CAI.ReLoad(DatFiles, ObjectID, CAI.Tag)
        HAI.ReLoad(DatFiles, ObjectID, HAI.Tag)
        RTI.ReLoad(DatFiles, ObjectID, RTI.Tag)
        AU.ReLoad(DatFiles, ObjectID, AU.Tag)
        AM.ReLoad(DatFiles, ObjectID, AM.Tag)

        RCA.ReLoad(DatFiles, ObjectID, RCA.Tag)
        AI.ReLoad(DatFiles, ObjectID, AI.Tag)
    End Sub
End Class

Public Class Upgrade_Data
    Private DatFiles As SCDatFiles.DatFiles = SCDatFiles.DatFiles.upgrades

    Public Property ObjectID As Integer


    Public Sub New(tObjectID As Integer)
        ' 设计器需要此调用。
        InitializeComponent()

        ' 在 InitializeComponent() 调用后添加初始化代码。
        DataContext = pjData
        ObjectID = tObjectID

        NameBar.Init(ObjectID, DatFiles, 0)

        ICO.Init(DatFiles, ObjectID, ICO.Tag)
        LAB.Init(DatFiles, ObjectID, LAB.Tag)
        MCB.Init(DatFiles, ObjectID, MCB.Tag)
        VCB.Init(DatFiles, ObjectID, VCB.Tag)
        RTB.Init(DatFiles, ObjectID, RTB.Tag)
        MCF.Init(DatFiles, ObjectID, MCF.Tag)
        VCF.Init(DatFiles, ObjectID, VCF.Tag)
        RTF.Init(DatFiles, ObjectID, RTF.Tag)
        MR.Init(DatFiles, ObjectID, MR.Tag)
        RAC.Init(DatFiles, ObjectID, RAC.Tag)
        BF.Init(DatFiles, ObjectID, BF.Tag)
    End Sub
    Public Sub ReLoad(DatFiles As SCDatFiles.DatFiles, ObjectID As Integer)
        ObjectID = ObjectID

        NameBar.ReLoad(ObjectID, DatFiles, 0)

        ICO.ReLoad(DatFiles, ObjectID, ICO.Tag)
        LAB.ReLoad(DatFiles, ObjectID, LAB.Tag)
        MCB.ReLoad(DatFiles, ObjectID, MCB.Tag)
        VCB.ReLoad(DatFiles, ObjectID, VCB.Tag)
        RTB.ReLoad(DatFiles, ObjectID, RTB.Tag)
        MCF.ReLoad(DatFiles, ObjectID, MCF.Tag)
        VCF.ReLoad(DatFiles, ObjectID, VCF.Tag)
        RTF.ReLoad(DatFiles, ObjectID, RTF.Tag)
        MR.ReLoad(DatFiles, ObjectID, MR.Tag)
        RAC.ReLoad(DatFiles, ObjectID, RAC.Tag)
        BF.ReLoad(DatFiles, ObjectID, BF.Tag)
    End Sub
End Class

Public Class SpriteData
    Private DatFiles As SCDatFiles.DatFiles = SCDatFiles.DatFiles.sprites

    Public Property ObjectID As Integer


    Public Sub New(tObjectID As Integer)
        ' 设计器需要此调用。
        InitializeComponent()

        ' 在 InitializeComponent() 调用后添加初始化代码。
        DataContext = pjData
        ObjectID = tObjectID

        UsedCodeList.Init(DatFiles, ObjectID)

        NameBar.Init(ObjectID, DatFiles, 0)

        IFI.Init(DatFiles, ObjectID, IFI.Tag)
        IV.Init(DatFiles, ObjectID, IV.Tag)

        SCI.Init(DatFiles, ObjectID, SCI.Tag)
        SCO.Init(DatFiles, ObjectID, SCO.Tag, InputField.SFlag.None)
        HB.Init(DatFiles, ObjectID, HB.Tag, InputField.SFlag.None)

        Dim ImageID As Integer = pjData.Dat.Data(SCDatFiles.DatFiles.sprites, "Image File", ObjectID)

        GRPImageBox.Init(ImageID, 0, GRPImageBox.BoxType.Sprite, ObjectID)
    End Sub
    Public Sub ReLoad(DatFiles As SCDatFiles.DatFiles, tObjectID As Integer)
        ObjectID = tObjectID

        UsedCodeList.ReLoad(DatFiles, ObjectID)

        NameBar.ReLoad(ObjectID, DatFiles, 0)

        IFI.ReLoad(DatFiles, ObjectID, IFI.Tag)
        IV.ReLoad(DatFiles, ObjectID, IV.Tag)
        SCI.ReLoad(DatFiles, ObjectID, SCI.Tag)
        SCO.ReLoad(DatFiles, ObjectID, SCO.Tag)
        HB.ReLoad(DatFiles, ObjectID, HB.Tag)

        Dim ImageID As Integer = pjData.Dat.Data(SCDatFiles.DatFiles.sprites, "Image File", ObjectID)

        GRPImageBox.Init(ImageID, 0, GRPImageBox.BoxType.Sprite, ObjectID)
    End Sub

    Private Sub IFI_ValueChange(sender As Object, e As RoutedEventArgs)
        Dim ImageID As Integer = pjData.Dat.Data(SCDatFiles.DatFiles.sprites, "Image File", ObjectID)

        GRPImageBox.Init(ImageID, 0, GRPImageBox.BoxType.Sprite, ObjectID)
    End Sub
End Class

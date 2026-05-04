Public Class SampleMapList
    Public CurrentMapPath As String
    Public IsMapLoading As Boolean
    Public Sub New(fullPath As String)

        ' 设计器需要此调用。
        InitializeComponent()

        ' 在 InitializeComponent() 调用后添加初始化代码。
        CurrentMapPath = fullPath

        Dim _MapData As New MapData(fullPath)
        IsMapLoading = _MapData.LoadComplete

        If IsMapLoading Then
            MapPath.Text = fullPath.Split("\").Last
            MapName.Text = _MapData.MapName
            MapDis.Text = _MapData.MapDes
        End If
    End Sub
End Class

Public Class BackUpFileItem
    Public BackFileInfo As IO.FileInfo
    Public Sub New(fileinfo As IO.FileInfo)

        ' 设计器需要此调用。
        InitializeComponent()

        ' 在 InitializeComponent() 调用后添加初始化代码。
        BackFileInfo = fileinfo

        FileName.Text = fileinfo.FullName
        FileDate.Text = fileinfo.LastWriteTime
    End Sub
End Class

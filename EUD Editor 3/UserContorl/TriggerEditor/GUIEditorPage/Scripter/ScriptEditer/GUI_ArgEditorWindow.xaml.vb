Public Class GUI_ArgEditorWindow
    Private gw As GUI_GrayWindow
    Public Sub New(graywindow As GUI_GrayWindow)

        ' 设计器需要此调用。
        InitializeComponent()

        ' 在 InitializeComponent() 调用后添加初始化代码。
        gw = graywindow

    End Sub



    Private Sub Window_Closed(sender As Object, e As EventArgs)
        gw.Close()
    End Sub

    Private Sub OkBtn_Click(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub

End Class

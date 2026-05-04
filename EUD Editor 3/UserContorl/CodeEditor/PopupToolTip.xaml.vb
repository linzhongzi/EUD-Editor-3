Public Class PopupToolTip
    Public Sub New()

        ' 设计器需要此调用。
        InitializeComponent()

        ' 在 InitializeComponent() 调用后添加初始化代码。
        ShowActivated = False
        Focusable = False
        ShowInTaskbar = False
        IsEnabled = False
    End Sub
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)


    End Sub
End Class

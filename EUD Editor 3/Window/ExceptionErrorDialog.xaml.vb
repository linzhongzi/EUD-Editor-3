Imports System.Media

Public Class ExceptionErrorDialog
    Public IsClose As Boolean

    Public Sub New(e As Exception)

        ' 设计器需要此调用。
        InitializeComponent()

        ' 在 InitializeComponent() 调用后添加初始化代码。
        SimpleText.Text = e.Message
        DetailText.Text = e.ToString
        SystemSounds.Hand.Play()
    End Sub

    Private Sub Detail_Click(sender As Object, e As RoutedEventArgs)
        If DetailText.Visibility = Visibility.Collapsed Then
            DetailText.Visibility = Visibility.Visible
        Else
            DetailText.Visibility = Visibility.Collapsed
        End If
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        IsClose = False
        Close()
    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        IsClose = True
        Close()
    End Sub
End Class

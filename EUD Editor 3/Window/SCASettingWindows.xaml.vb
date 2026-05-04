Imports Microsoft.DwayneNeed.Win32.User32

Public Class SCASettingWindows
    Public IsOkay As Boolean = False
    Public SelectSampleMap As String

    Public Result As Boolean = False

    Public Sub New()

        ' 设计器需要此调用。
        InitializeComponent()

        ' 在 InitializeComponent() 调用后添加初始化代码。
        LoginPage.Visibility = Visibility.Visible
        LoginAlert.Visibility = Visibility.Collapsed

        AlertText.Text = Tool.GetLanText("SCADoubleLoginAlert").Replace("\n", System.Environment.NewLine)
    End Sub


    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        ' 读取示例地图。

    End Sub

    Private Sub TextBox_PreviewKeyDown(sender As Object, e As KeyEventArgs)
        If e.Key = Key.Enter Then
            Login()
        End If
    End Sub

    Private Sub SignUp_Click(sender As Object, e As RoutedEventArgs)
        Process.Start("https://scarchive.kr/accounts/signup.php")
    End Sub

    Private Sub LoginProblem_Click(sender As Object, e As RoutedEventArgs)
        Process.Start("https://scarchive.kr/accounts/troubleshooter.php")
    End Sub

    Private hash As String
    Private Sub LoginBtn_Click(sender As Object, e As RoutedEventArgs)
        Login()
    End Sub

    Private Sub Login()
        Dim email As String = EmailTextBox.Text
        Dim pw As String = PasswordBox.Password

        Dim returnval As String = HttpTool.Login(email, pw)
        Select Case returnval
            Case "BANUSER"
                ErrorTextBox.Text = "禁止使用的账户。"
                ErrorTextBox.Visibility = Visibility.Visible
                Return
            Case "NOACCOUNT"
                ErrorTextBox.Text = "请检查邮箱和密码。"
                ErrorTextBox.Visibility = Visibility.Visible
                Return
            Case "ERROR"
                ErrorTextBox.Text = "登录失败。"
                ErrorTextBox.Visibility = Visibility.Visible
                Return
        End Select
        hash = returnval

        If Not pjData.TEData.SCArchive.CheckLoginHash(hash) Then
            LoginPage.Visibility = Visibility.Collapsed
            LoginAlert.Visibility = Visibility.Visible
            Return
        End If


        pjData.TEData.SCArchive.SaveLoginHash(hash)

        If AutoLogin.IsChecked Then
            pjData.TEData.SCArchive.SavePassWord(email, pw)
        End If

        ' 如果成功。
        pjData.TEData.SCArchive.SCAEmail = EmailTextBox.Text
        pjData.TEData.SCArchive.IsLogin = True
        pjData.TEData.SCArchive.TempPassWord = pw
        Result = True

        Close()
    End Sub

    Private Sub MetroWindow_Closing(sender As Object, e As ComponentModel.CancelEventArgs)
    End Sub

    Private Sub LoginApply_Click(sender As Object, e As RoutedEventArgs)
        Dim email As String = EmailTextBox.Text
        Dim pw As String = PasswordBox.Password

        pjData.TEData.SCArchive.MakerServerName = ""
        pjData.TEData.SCArchive.SubTitle = ""
        pjData.TEData.SCArchive.MapName = ""
        pjData.TEData.SCArchive.SaveLoginHash(hash)

        If AutoLogin.IsChecked Then
            pjData.TEData.SCArchive.SavePassWord(email, PW)
        End If

        ' 如果成功。
        pjData.TEData.SCArchive.SCAEmail = EmailTextBox.Text
        pjData.TEData.SCArchive.IsLogin = True
        pjData.TEData.SCArchive.TempPassWord = pw
        Result = True

        Close()
    End Sub

    Private Sub LoginCancel_Click(sender As Object, e As RoutedEventArgs)
        LoginPage.Visibility = Visibility.Visible
        LoginAlert.Visibility = Visibility.Collapsed
    End Sub
End Class

Imports System.Net

Public Class SCASetting

    Private SCADAtas As SCADataList

    Private Binding As SCABinding
    Public Sub New()

        ' 设计器需要此调用。
        InitializeComponent()

        ' 在 InitializeComponent() 调用后添加初始化代码。
        ' 绑定前确认登录信息。
        pjData.TEData.SCArchive.CheckLoginAccount()
        If pjData.TEData.SCArchive.IsLogin Then
            LoginEmail.Text = pjData.TEData.SCArchive.SCAEmail
            SCALoginButton.Content = Tool.GetLanText("SCALogout")
            MapInfor.IsEnabled = True
        Else
            SCALoginButton.Content = Tool.GetLanText("SCALogin")
            MapInfor.IsEnabled = False
        End If

        Binding = New SCABinding
        DataContext = Binding

        SCADAtas = New SCADataList

        MainDockPanel.Children.Add(SCADAtas)

    End Sub

    Private Loadcmp As Boolean = False
    Private Sub UserControl_Loaded(sender As Object, e As RoutedEventArgs)
        MapDetail.IsEnabled = infoCheckbox.IsChecked
        'UMSPassWord.Password = pjData.TEData.SCArchive.PassWord
        Loadcmp = True
    End Sub


    Public Sub Refresh()
        SCADAtas.Refresh()
    End Sub


    Private Sub UMSPassWord_PasswordChanged(sender As Object, e As RoutedEventArgs)
        If Loadcmp Then
            'pjData.TEData.SCArchive.PassWord = UMSPassWord.Password
        End If
    End Sub

    Private Sub UseSCA_Checked(sender As Object, e As RoutedEventArgs)
        If Loadcmp Then
            ' 是否登录成功
            If pjData.TEData.SCArchive.IsLogin = False Or
                MakerID.Text.Trim = "" Or
                subtitle.Text.Trim = "" Or
                UseMapName.Text.Trim = "" Then

                'If infoCheckbox.IsChecked Then
                '    If MapTags.Text.Trim = "" Or
                '        MakerEmail.Text.Trim = "" Or
                '        Maptitle.Text.Trim = "" Or
                '        MapLink.Text.Trim = "" Or
                '        ImageLink.Text.Trim = "" Or
                '        Mapdes.Text.Trim = "" Then
                '        UseSCA.IsChecked = False
                ' Warring.Content = "请输入所有地图信息。"
                '    End If
                'End If
                UseSCA.IsChecked = False
                Warring.Content = "맵 정보를 모두 입력 하세요."
            Else
                Warring.Content = ""
            End If
        End If
    End Sub
    Private Sub UseSCA_Unchecked(sender As Object, e As RoutedEventArgs)
        'MapInfor.IsEnabled = False
    End Sub

    Private Sub CheckBox_Checked(sender As Object, e As RoutedEventArgs)
        If Loadcmp = True Then
            MapDetail.IsEnabled = infoCheckbox.IsChecked
        End If
    End Sub

    Private Sub infoCheckbox_Unchecked(sender As Object, e As RoutedEventArgs)
        If Loadcmp = True Then
            MapDetail.IsEnabled = infoCheckbox.IsChecked
        End If
    End Sub

    Private Sub SCAButton_Click(sender As Object, e As RoutedEventArgs)
        Dim mapCode As String = pjData.EudplibData.GetMapCode()


        If (mapCode = "") Or (pjData.TEData.SCArchive.SubTitle = "") Or
              (pjData.TEData.SCArchive.SCAEmail = "") Or (pjData.TEData.SCArchive.GetPassWord(pjData.TEData.SCArchive.SCAEmail) = "") Then
            Return
        End If
        Dim senddata As String = ""
        senddata = "mapcode=" & WebUtility.UrlEncode(mapCode) & "&"
        senddata = senddata & "subtitle=" & WebUtility.UrlEncode(pjData.TEData.SCArchive.SubTitle) & "&"
        senddata = senddata & "bt=" & WebUtility.UrlEncode(pjData.TEData.SCArchive.SCAEmail) & "&"
        senddata = senddata & "pw=" & WebUtility.UrlEncode(pjData.TEData.SCArchive.GetPassWord(pjData.TEData.SCArchive.SCAEmail))


        Process.Start("https://scarchive.kr/creator/main.php?" + senddata)
    End Sub

    Private Sub SCALoginButton_Click(sender As Object, e As RoutedEventArgs)
        If pjData.TEData.SCArchive.IsLogin Then
            ' 如果已登录，则显示注销按钮。
            SCALoginButton.Content = Tool.GetLanText("SCALogin")
            MapInfor.IsEnabled = False

            LoginEmail.Text = ""

            UseSCA.IsChecked = False
            pjData.TEData.SCArchive.IsLogin = False
        Else
            Dim scasetting As New SCASettingWindows

            scasetting.ShowDialog()

            If scasetting.Result Then
                ' 如果有返回值，则登录成功。
                SCALoginButton.Content = Tool.GetLanText("SCALogout")
                MapInfor.IsEnabled = True

                LoginEmail.Text = pjData.TEData.SCArchive.SCAEmail
                pjData.TEData.SCArchive.IsLogin = True



                Binding.LoginRefresh()
            End If
        End If


    End Sub


End Class

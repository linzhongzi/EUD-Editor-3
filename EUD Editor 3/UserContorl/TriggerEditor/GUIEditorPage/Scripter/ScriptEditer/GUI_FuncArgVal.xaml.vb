Imports System.Text.RegularExpressions

Public Class GUI_FuncArgVal
    Private p As GUIScriptEditerWindow
    Private scr As ScriptBlock

    Private isLoad As Boolean = False
    Public Sub New(tp As GUIScriptEditerWindow, tscr As ScriptBlock)

        ' 设计器需要此调用。
        InitializeComponent()

        ' 在 InitializeComponent() 调用后添加初始化代码。
        p = tp
        scr = tscr


        Dim index As Integer = -1
        For i = 0 To tescm.SCValueType.Count - 1
            Dim comboboxitem As New ComboBoxItem
            comboboxitem.Tag = tescm.SCValueType(i)
            comboboxitem.Content = tescm.SCValueType(i)
            typecombobox.Items.Add(comboboxitem)

            If tescm.SCValueType(i) = scr.name Then
                index = i
            End If
        Next
        typecombobox.SelectedIndex = index


        AddHandler p.OkayBtnEvent, AddressOf OkayAction

        Dim colorcode As String = tescm.Tabkeys("Value")
        colorbox.Background = New SolidColorBrush(ColorConverter.ConvertFromString(colorcode))



        ttb.Text = scr.value
        argtip.Text = scr.value2

        If CheckEditable() Then
            ErrorLog.Content = ""
            p.OkBtn.IsEnabled = True
        Else
            p.OkBtn.IsEnabled = False
        End If

        isLoad = True
    End Sub



    Public Sub OkayAction(sender As Object, e As RoutedEventArgs)


        scr.value = ttb.Text
        scr.value2 = argtip.Text


        scr.name = CType(typecombobox.SelectedItem, ComboBoxItem).Tag
    End Sub

    Private Sub ttb_TextChanged(sender As Object, e As TextChangedEventArgs)
        If CheckEditable() Then
            ErrorLog.Content = ""
            p.OkBtn.IsEnabled = True
        Else
            p.OkBtn.IsEnabled = False
        End If
    End Sub


    Private Function CheckEditable() As Boolean
        Dim nametext As String = ttb.Text

        If typecombobox.SelectedItem Is Nothing Then
            Return False
        End If


        If nametext.Count = 0 Then
            ErrorLog.Content = "变量名不能为空。"
            Return False
        End If

        If IsNumeric(Mid(nametext, 1, 1)) Then
            ErrorLog.Content = "首字符不能是数字。"
            Return False
        End If



        Dim rgx As New Regex("[ !@#$%^&*=]")

        If rgx.IsMatch(nametext) Then
            ErrorLog.Content = "包含无效字符。"
            Return False
        End If


        Return True
    End Function

    Private Sub typecombobox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        If IsLoad Then
            If CheckEditable() Then
                ErrorLog.Content = ""
                p.OkBtn.IsEnabled = True
            Else
                p.OkBtn.IsEnabled = False
            End If
        End If

    End Sub
End Class

Public Class UsedCodeList
    Private Datfile As SCDatFiles.DatFiles
    Private ObjectID As Integer

    Private MyList As CodeCollection
    ' Dat类型，只需对象ID即可

    ' 提前整理好每种Dat类型的来源。
    ' 例如，对于Dat类型Weapon，检查所有单位的UnitDat_GroundWeapon，如果对象ID相同，则添加拥有该ID的单位。

    ' 利用此关系，最好也更改连接到的对象的名称。
    Public Sub Init(_DatFile As SCDatFiles.DatFiles, _ObjectID As Integer)
        Datfile = _DatFile
        ObjectID = _ObjectID

        MyList = pjData.BindingManager.CodeConnecter(Datfile, ObjectID).Items

        Dim bind As New Binding
        bind.Source = MyList
        MainListBox.SetBinding(ListBox.ItemsSourceProperty, bind)

        'MainListBox.ItemsSource = New Binding


        'Me.DataContext = pjData.BindingManager.CodeConnecter(Datfile, ObjectID)
    End Sub


    Public Sub ReLoad(_DatFile As SCDatFiles.DatFiles, _ObjectID As Integer)
        Datfile = _DatFile
        ObjectID = _ObjectID

        pjData.BindingManager.CodeConnecter(Datfile, ObjectID).DeleteList(MyList)
        MyList = pjData.BindingManager.CodeConnecter(Datfile, ObjectID).Items

        Dim bind As New Binding
        bind.Source = MyList
        MainListBox.SetBinding(ListBox.ItemsSourceProperty, bind)


        'Me.DataContext = pjData.BindingManager.CodeConnecter(Datfile, ObjectID)
    End Sub


    Private Sub UserControl_Unloaded(sender As Object, e As RoutedEventArgs)
        Try
            pjData.BindingManager.CodeConnecter(Datfile, ObjectID).DeleteList(MyList)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub MainListBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        'MsgBox(MainListBox.SelectedItem.Tag)
        If MainListBox.SelectedItem IsNot Nothing Then
            Dim Tags() As String = MainListBox.SelectedItem.Tag.ToString.Split(",")

            Dim DatFiles As SCDatFiles.DatFiles = Tags(0)
            Dim index As Integer = Tags(2)
            TabItemTool.WindowTabItem(DatFiles, index)
            MainListBox.SelectedIndex = -1
        End If

    End Sub
End Class

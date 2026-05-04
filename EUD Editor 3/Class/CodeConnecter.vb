Imports System.Collections.Specialized
Imports System.ComponentModel

Public Class CodeConnecter
    Implements INotifyCollectionChanged

    Private Datfile As SCDatFiles.DatFiles
    Private ObjectID As Integer

    Public Event CollectionChanged As NotifyCollectionChangedEventHandler Implements INotifyCollectionChanged.CollectionChanged
    ' Dat类型，只需对象ID即可

    ' 提前整理好每种Dat类型的来源。
    ' 例如，对于Dat类型Weapon，检查所有单位的UnitDat_GroundWeapon，
    ' 如果对象ID相同，则添加拥有该ID的单位。

    ' 利用此关系，最好也更改连接到的对象的名称。
    Private itemCollection As List(Of CodeCollection)


    Public Sub New(tDatfile As SCDatFiles.DatFiles, tObjectID As Integer)
        Datfile = tDatfile
        ObjectID = tObjectID

        itemCollection = New List(Of CodeCollection)
    End Sub


    Public Sub ItemsReferesh()
        ListReset()


    End Sub
    Private Sub PropertyChangedPack()

    End Sub

    Public Sub DeleteList(CC As CodeCollection)
        itemCollection.Remove(CC)
    End Sub
    Public ReadOnly Property Items() As CodeCollection
        Get
            itemCollection.Add(New CodeCollection)
            ListReset()
            Return itemCollection.Last
        End Get

    End Property




    Private Sub ListReset()
        For ci = 0 To itemCollection.Count - 1
            itemCollection(ci).Clear()

            Dim CC As CodeConnectGroup = pjData.BindingManager.CodeConnectGroup(Datfile)
            For i = 0 To CC.Count - 1
                Dim Datfiles As SCDatFiles.DatFiles = CC.GetDatType(i)
                Dim ParamName As String = CC.GetParamName(i)

                If ParamName = "ButtonSet" Then
                    For index = 0 To SCUnitCount - 1
                        Dim value As Long = pjData.ExtraDat.ButtonSet(index)

                        If value = ObjectID Then
                            Dim tListBox As New ListBoxItem
                            tListBox.Tag = Datfiles & "," & ParamName & "," & index
                            tListBox.ToolTip = Tool.GetText(Datfilesname(Datfiles)) & " '" & Tool.GetText(Datfilesname(Datfiles) & "_" & ParamName) & "'"

                            Dim Textblock As New TextBlock
                            Textblock.TextWrapping = TextWrapping.Wrap

                            Dim myBinding As Binding = New Binding("TabName")
                            myBinding.Source = pjData.BindingManager.UIManager(Datfiles, index)
                            Textblock.SetBinding(TextBlock.TextProperty, myBinding)

                            tListBox.Content = Textblock

                            'tListBox.Content = Tool.GetText(Datfilesname(Datfiles)) & ParamName & " " & Realindex
                            itemCollection(ci).Add(tListBox)
                        End If
                    Next
                Else
                    For index = 0 To pjData.Dat.DatFileList(Datfiles).GetParamInfo(ParamName, SCDatFiles.EParamInfo.VarCount) - 1
                        Dim Realindex As Integer = index + pjData.Dat.DatFileList(Datfiles).GetParamInfo(ParamName, SCDatFiles.EParamInfo.VarStart)

                        Dim value As Long = pjData.Dat.Data(Datfiles, ParamName, Realindex)

                        If value = ObjectID Then
                            Dim tListBox As New ListBoxItem
                            tListBox.Tag = Datfiles & "," & ParamName & "," & Realindex
                            tListBox.ToolTip = Tool.GetText(Datfilesname(Datfiles)) & " '" & Tool.GetText(Datfilesname(Datfiles) & "_" & ParamName) & "'"

                            Dim Textblock As New TextBlock
                            Textblock.TextWrapping = TextWrapping.Wrap

                            Dim myBinding As Binding = New Binding("TabName")
                            myBinding.Source = pjData.BindingManager.UIManager(Datfiles, Realindex)
                            Textblock.SetBinding(TextBlock.TextProperty, myBinding)

                            tListBox.Content = Textblock

                            'tListBox.Content = Tool.GetText(Datfilesname(Datfiles)) & ParamName & " " & Realindex
                            itemCollection(ci).Add(tListBox)
                        End If
                        'MsgBox(Tool.GetText(Datfilesname(Datfiles)) & ParamName & " " & Realindex)
                    Next
                End If
            Next
        Next

    End Sub

    Private Sub NotifyPropertyChanged(ByVal info As String)
        For ci = 0 To itemCollection.Count - 1
            itemCollection.Clear()
            Dim CC As CodeConnectGroup = pjData.BindingManager.CodeConnectGroup(Datfile)
            For i = 0 To CC.Count - 1
                Dim Datfiles As SCDatFiles.DatFiles = CC.GetDatType(i)
                Dim ParamName As String = CC.GetParamName(i)
                If ParamName = "ButtonSet" Then
                    If Datfiles = SCDatFiles.DatFiles.units Then

                    End If
                    For index = 0 To SCUnitCount - 1
                        Dim value As Long = pjData.ExtraDat.ButtonSet(index)

                        If value = ObjectID Then
                            Dim tListBox As New ListBoxItem
                            tListBox.Tag = Datfiles & "," & ParamName & "," & index
                            tListBox.ToolTip = Tool.GetText(Datfilesname(Datfiles)) & " '" & Tool.GetText(Datfilesname(Datfiles) & "_" & ParamName) & "'"

                            Dim Textblock As New TextBlock
                            Textblock.TextWrapping = TextWrapping.Wrap

                            Dim myBinding As Binding = New Binding("TabName")
                            myBinding.Source = pjData.BindingManager.UIManager(Datfiles, index)
                            Textblock.SetBinding(TextBlock.TextProperty, myBinding)

                            tListBox.Content = Textblock

                            'tListBox.Content = Tool.GetText(Datfilesname(Datfiles)) & ParamName & " " & Realindex
                            itemCollection(ci).Add(tListBox)
                        End If
                    Next
                Else
                    For index = 0 To pjData.Dat.DatFileList(Datfiles).GetParamInfo(ParamName, SCDatFiles.EParamInfo.VarCount) - 1
                        Dim Realindex As Integer = index + pjData.Dat.DatFileList(Datfiles).GetParamInfo(ParamName, SCDatFiles.EParamInfo.VarStart)

                        Dim value As Long = pjData.Dat.Data(Datfiles, ParamName, Realindex)

                        If value = ObjectID Then
                            Dim tListBox As New ListBoxItem
                            Dim Textblock As New TextBlock
                            Textblock.TextWrapping = TextWrapping.Wrap

                            Dim myBinding As Binding = New Binding("TabName")
                            myBinding.Source = pjData.BindingManager.UIManager(Datfiles, index)
                            Textblock.SetBinding(TextBlock.TextProperty, myBinding)

                            tListBox.Content = Textblock

                            itemCollection(ci).Add(tListBox)
                        End If
                    Next
                End If


            Next
        Next





    End Sub
End Class

Public Class CodeCollection
    Inherits ObjectModel.ObservableCollection(Of ListBoxItem)

End Class

Public Class CodeConnectGroup
    '

    Private DatFile As SCDatFiles.DatFiles

    Public ReadOnly Property GetDatFile As SCDatFiles.DatFiles
        Get
            Return DatFile
        End Get
    End Property


    Private DatType As List(Of SCDatFiles.DatFiles)
    Private ParamName As List(Of String)


    Public Sub New(tDatFile As SCDatFiles.DatFiles)
        DatType = New List(Of SCDatFiles.DatFiles)
        ParamName = New List(Of String)

        DatFile = tDatFile
    End Sub

    Public Sub Add(_DatType As SCDatFiles.DatFiles, _ParamName As String)
        DatType.Add(_DatType)
        ParamName.Add(_ParamName)
    End Sub

    Public ReadOnly Property Count As Integer
        Get
            Return DatType.Count
        End Get
    End Property
    Public ReadOnly Property GetDatType(index As Integer) As SCDatFiles.DatFiles
        Get
            Return DatType(index)
        End Get
    End Property

    Public ReadOnly Property GetParamName(index As Integer) As String
        Get
            Return ParamName(index)
        End Get
    End Property

    Public ReadOnly Property IsParamExist(tParamName As String) As Boolean
        Get
            If ParamName.IndexOf(tParamName) >= 0 Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property
    ' 代码依赖关系
    ' 单位 - 单位_附加单位12, 单位_感染单位
    ' 武器 - 单位_对空对地武器, 命令_指定目标时
    ' 投掷 - 单位_投掷, 武器_图形
    ' 子图形 - 投掷_子图形（Sprite）
    ' 图形 - 单位_生产模样, 子图形_图形
    ' 升级 - 单位_护甲, 武器_升级
    ' 技能 - 武器_不使用, 命令_能量
    ' 命令 - 单位_人类.电脑基本.恢复原状.单位攻击.攻击移动, 命令_未知命令
    ' 文本 - 单位_名称,阶级, 武器_名称, 错误消息, 升级_名称, 技能_名称, 命令_名称

    ' 例如，如果更改升级，则相应的护甲和武器升级的ValueText必须更改。
    ' 例如，如果更改unit_product模样，则必须向更改前的图形发送Refresh命令，更改后也是如此。

End Class

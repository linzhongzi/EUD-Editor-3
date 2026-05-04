Imports System.ComponentModel

Public Class DatBinding
    Implements INotifyPropertyChanged

    ' 绑定时也传递主体。
    ' 如果是错误值或不支持的部分，则将其移除。~

    Private Datfile As SCDatFiles.DatFiles
    Private Parameter As String
    Private ObjectID As Integer

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged


    Private FlagBindingManager As List(Of FlagBinding)
    Public ReadOnly Property GetFlagBinding(index As Integer) As FlagBinding
        Get
            Return FlagBindingManager(index)
        End Get
    End Property

    Public Sub New(tDatfile As SCDatFiles.DatFiles, tParameter As String, tObjectID As Integer)
        Dim ValueStart As Integer = pjData.Dat.ParamInfo(tDatfile, tParameter, SCDatFiles.EParamInfo.VarStart)
        Dim ValueSize As Byte = pjData.Dat.ParamInfo(tDatfile, tParameter, SCDatFiles.EParamInfo.Size)

        Datfile = tDatfile
        Parameter = tParameter
        ObjectID = tObjectID + ValueStart

        FlagBindingManager = New List(Of FlagBinding)
        For i = 0 To ValueSize * 8 - 1
            FlagBindingManager.Add(New FlagBinding(Me, Datfile, Parameter, ObjectID, i))
        Next

        If Parameter = "Rank/Sublabel" Then
            temprsv = pjData.Dat.Data(Datfile, Parameter, ObjectID)
        End If
    End Sub

    Public Sub BackColorRefresh()
        NotifyPropertyChanged("BackColor")
        For i = 0 To FlagBindingManager.Count - 1
            FlagBindingManager(i).PropertyChangedPack()
        Next
    End Sub

    Private Sub PropertyChangedPack()
        pjData.BindingManager.UIManager(Datfile, ObjectID).ChangeProperty()
        NotifyPropertyChanged("HPValue")
        NotifyPropertyChanged("Checked")
        NotifyPropertyChanged("Value")
        NotifyPropertyChanged("BackColor")
        NotifyPropertyChanged("ValueText")
        'NotifyPropertyChanged("ValueTextBinding")
        NotifyPropertyChanged("ValueImage")
        NotifyPropertyChanged("ValueFlag")

        NotifyPropertyChanged("ToolTipText")
        For i = 0 To FlagBindingManager.Count - 1
            FlagBindingManager(i).PropertyChangedPack()
        Next
    End Sub

    Public ReadOnly Property ToolTipText() As TextBlock
        Get
            Dim DefaultValue As Long = scData.DefaultDat.Data(Datfile, Parameter, ObjectID)


            Dim returnStr As String = ""
            returnStr = "<0>" & Tool.GetText("datfliename") & " : <2>" & Tool.GetText(Datfilesname(Datfile)) & "(" & Datfilesname(Datfile) & ")" & vbCrLf &
             "<0>" & Tool.GetText("parameter") & " : <2>" & Tool.GetText(Datfilesname(Datfile) & "_" & Parameter) & "(" & Parameter & ")" & vbCrLf
            ' returnStr = returnStr & "索引 : " & ObjectID & vbCrLf
            ' returnStr = returnStr & "大小 : " & scData.DefaultDat.ParamInfo(Datfile, Parameter, SCDatFiles.EParamInfo.Size) & vbCrLf

            Dim ValueType As SCDatFiles.DatFiles = scData.DefaultDat.ParamInfo(Datfile, Parameter, SCDatFiles.EParamInfo.ValueType)
            If ValueType <> SCDatFiles.DatFiles.None Then
                returnStr = returnStr & "<0>" & Tool.GetText("valuetype") & " : <3>" & Tool.GetText(Datfilesname(ValueType)) & vbCrLf
            End If
            ' returnStr = returnStr & "基址偏移量 : 0x" & Hex(Tool.GetOffset(Datfile, Parameter)).ToUpper & vbCrLf

            If Not pjData.Dat.Values(Datfile, Parameter, ObjectID).IsDefault Then
                returnStr = returnStr & "<0>" & Tool.GetText("orgvalue") & " : <3>" & DefaultValue & vbCrLf
            End If

            If pjData.IsMapLoading Then
                If Not pjData.MapData.DatFile.Values(Datfile, Parameter, ObjectID).IsDefault Then ' 如果要写入默认值
                    returnStr = returnStr & "<0>" & Tool.GetText("mapdatavalue") & " : <3>" & pjData.MapData.DatFile.Data(Datfile, Parameter, ObjectID) & vbCrLf
                End If
            End If

            'If Not pjData.Dat.Values(Datfile, Parameter, ObjectID).IsDefault Then
            ' returnStr = returnStr & "   更改后的值 : " & pjData.Dat.Data(Datfile, Parameter, ObjectID)
            'End If

            returnStr = returnStr & vbCrLf & "<0>" & Tool.GetText(Datfilesname(Datfile) & "_" & Parameter & "_ToolTip")

            Dim Tb As TextBlock = Tool.TextColorBlock(returnStr.Trim)
            Tb.TextWrapping = TextWrapping.WrapWithOverflow
            Tb.MaxWidth = 250

            Return Tb
        End Get
    End Property


    Private temprsv As UInteger
    Public Property Value() As String
        Get
            If Parameter = "Rank/Sublabel" Then
                If Not (1301 <= temprsv And temprsv <= 1556) Then
                    Dim tv As UInteger = temprsv
                    temprsv = pjData.Dat.Data(Datfile, Parameter, ObjectID)
                    Return tv
                End If
                ' MsgBox("异常")
            End If
            ' MsgBox("数据查找获取")
            ' 如果是地图数据中的项目？
            If pjData.IsMapLoading Then
                'MsgBox(pjData.MapData.DatFile.Data(Datfile, Parameter, ObjectID) & vbCrLf &
                'pjData.Dat.Data(Datfile, Parameter, ObjectID))
                If pjData.Dat.Values(Datfile, Parameter, ObjectID).IsDefault And Not pjData.MapData.DatFile.Values(Datfile, Parameter, ObjectID).IsDefault Then ' 如果要写入默认值
                    Return pjData.MapData.DatFile.Data(Datfile, Parameter, ObjectID)
                End If
            End If


            Return pjData.Dat.Data(Datfile, Parameter, ObjectID)
        End Get

        Set(ByVal tvalue As String)
            If Not (tvalue = pjData.Dat.Data(Datfile, Parameter, ObjectID)) Then
                pjData.SetDirty(True)
                ' MsgBox("数据查找设置")

                Dim tData As Long = pjData.Dat.Data(Datfile, Parameter, ObjectID)

                pjData.Dat.Data(Datfile, Parameter, ObjectID) = tvalue
                pjData.Dat.Values(Datfile, Parameter, ObjectID).IsDefault = False
                Try
                    pjData.BindingManager.RefreshCodeUseData(Datfile, Parameter, tData)
                    pjData.BindingManager.RefreshCodeUseData(Datfile, Parameter, tvalue)
                Catch ex As Exception

                End Try


                If Parameter = "Rank/Sublabel" Then
                    temprsv = tvalue
                End If




                ' 如果是需要注意的数据。例如与标签相关的内容。
                If Parameter = "Label" Then
                    pjData.BindingManager.UIManager(Datfile, ObjectID).NameRefresh()
                End If


                PropertyChangedPack()
            End If
        End Set
    End Property

    Public Property ValueFlag() As String
        Get
            ' MsgBox("数据查找获取")
            ' 如果是地图数据中的项目？
            If pjData.IsMapLoading Then
                'MsgBox(pjData.MapData.DatFile.Data(Datfile, Parameter, ObjectID) & vbCrLf &
                'pjData.Dat.Data(Datfile, Parameter, ObjectID))
                If pjData.Dat.Values(Datfile, Parameter, ObjectID).IsDefault And Not pjData.MapData.DatFile.Values(Datfile, Parameter, ObjectID).IsDefault Then ' 如果要写入默认值
                    Return Hex(pjData.MapData.DatFile.Data(Datfile, Parameter, ObjectID))
                End If
            End If


            Return Hex(pjData.Dat.Data(Datfile, Parameter, ObjectID))
        End Get

        Set(ByVal tvalue As String)
            tvalue = "&H" & tvalue
            If Not (tvalue = pjData.Dat.Data(Datfile, Parameter, ObjectID)) Then
                ' MsgBox("数据查找设置")
                pjData.Dat.Data(Datfile, Parameter, ObjectID) = tvalue
                pjData.Dat.Values(Datfile, Parameter, ObjectID).IsDefault = False
                PropertyChangedPack()
            End If
        End Set
    End Property

    Public Property HPValue() As String
        Get
            Dim returnVal As Long
            ' MsgBox("数据查找获取")
            ' 如果是地图数据中的项目？
            If pjData.IsMapLoading Then
                'MsgBox(pjData.MapData.DatFile.Data(Datfile, Parameter, ObjectID) & vbCrLf &
                'pjData.Dat.Data(Datfile, Parameter, ObjectID))
                If pjData.Dat.Values(Datfile, Parameter, ObjectID).IsDefault And Not pjData.MapData.DatFile.Values(Datfile, Parameter, ObjectID).IsDefault Then ' 如果要写入默认值
                    returnVal = pjData.MapData.DatFile.Data(Datfile, Parameter, ObjectID)
                Else
                    returnVal = pjData.Dat.Data(Datfile, Parameter, ObjectID)
                End If
            Else
                returnVal = pjData.Dat.Data(Datfile, Parameter, ObjectID)
            End If



            If returnVal > 2147483647 Then ' 如果是负数
                '2147483648 = -2147483648
                '4294967295 = -1
                returnVal -= 4294967296
            End If


            Return Math.Floor(returnVal / 256)
        End Get

        Set(ByVal tvalue As String)
            If Not (tvalue = pjData.Dat.Data(Datfile, Parameter, ObjectID)) Then
                Dim Setvalue As Long = tvalue * 256

                If Setvalue > Integer.MaxValue Then
                    Setvalue = Integer.MaxValue
                End If
                If Setvalue < Integer.MinValue Then
                    Setvalue = Integer.MinValue
                End If
                ' MsgBox("数据查找设置")
                If Setvalue < 0 Then ' 如果是负数
                    '2147483648 = -2147483648
                    '4294967295 = -1
                    Setvalue += 4294967296
                End If


                pjData.Dat.Data(Datfile, Parameter, ObjectID) = Setvalue
                pjData.Dat.Values(Datfile, Parameter, ObjectID).IsDefault = False
                PropertyChangedPack()
            End If
        End Set
    End Property



    Public Property Checked() As Boolean
        Get
            ' MsgBox("数据查找获取")
            ' 如果是地图数据中的项目？
            If pjData.IsMapLoading Then
                'MsgBox(pjData.MapData.DatFile.Data(Datfile, Parameter, ObjectID) & vbCrLf &
                'pjData.Dat.Data(Datfile, Parameter, ObjectID))
                If pjData.Dat.Values(Datfile, Parameter, ObjectID).IsDefault And Not pjData.MapData.DatFile.Values(Datfile, Parameter, ObjectID).IsDefault Then ' 如果要写入默认值
                    Return pjData.MapData.DatFile.Data(Datfile, Parameter, ObjectID)
                End If
            End If




            Return pjData.Dat.Data(Datfile, Parameter, ObjectID)
        End Get

        Set(ByVal tvalue As Boolean)
            If Not (tvalue = pjData.Dat.Data(Datfile, Parameter, ObjectID)) Then
                ' MsgBox("数据查找设置")
                If tvalue Then
                    pjData.Dat.Data(Datfile, Parameter, ObjectID) = 1
                Else
                    pjData.Dat.Data(Datfile, Parameter, ObjectID) = 0
                End If
                pjData.Dat.Values(Datfile, Parameter, ObjectID).IsDefault = False
                PropertyChangedPack()
            End If
        End Set
    End Property

    Public ReadOnly Property ValueText As String
        Get
            Dim valueType As SCDatFiles.DatFiles = pjData.Dat.ParamInfo(Datfile, Parameter, SCDatFiles.EParamInfo.ValueType)
            If valueType <> SCDatFiles.DatFiles.None Then
                Dim Value As Long = pjData.Dat.Data(Datfile, Parameter, ObjectID)
                If SCCodeCount(valueType) > Value Then
                    Return pjData.CodeLabel(valueType, Value, True)
                Else
                    Return Tool.GetText("None")
                End If
            Else
                Return Tool.GetText("None")
            End If
        End Get
    End Property
    'Public ReadOnly Property ValueTextBinding As UIManager
    '    Get
    ' 'MsgBox("ㅆ:빋") ' 音符占位？
    '        Dim valueType As SCDatFiles.DatFiles = pjData.Dat.ParamInfo(Datfile, Parameter, SCDatFiles.EParamInfo.ValueType)
    '        If valueType <> SCDatFiles.DatFiles.None Then
    '            Dim Value As Long = pjData.Dat.Data(Datfile, Parameter, ObjectID)

    '            If SCCodeCount(valueType) > Value Then
    '                Return pjData.BindingManager.UIManager(valueType, Value)
    '            Else
    '                Return Nothing
    '            End If
    '        Else
    '            Return Nothing
    '        End If
    '    End Get
    'End Property


    Public ReadOnly Property ValueImage As ImageSource
        Get
            Dim isIcon As Boolean = False
            Dim ImageIndex As Integer

            Dim valueType As SCDatFiles.DatFiles = pjData.Dat.ParamInfo(Datfile, Parameter, SCDatFiles.EParamInfo.ValueType)
            Dim value As Long = pjData.Dat.Data(Datfile, Parameter, ObjectID)

            Select Case valueType
                Case SCDatFiles.DatFiles.units
                    Dim tGraphics As Integer = pjData.Dat.Data(SCDatFiles.DatFiles.units, "Graphics", value)
                    Dim tSprite As Integer = pjData.Dat.Data(SCDatFiles.DatFiles.flingy, "Sprite", tGraphics)
                    Dim timage As Integer = pjData.Dat.Data(SCDatFiles.DatFiles.sprites, "Image File", tSprite)

                    ImageIndex = timage
                Case SCDatFiles.DatFiles.weapons
                    Dim tIcon As Integer = pjData.Dat.Data(SCDatFiles.DatFiles.weapons, "Icon", value)

                    isIcon = True
                    ImageIndex = tIcon
                Case SCDatFiles.DatFiles.flingy
                    Dim tSprite As Integer = pjData.Dat.Data(SCDatFiles.DatFiles.flingy, "Sprite", value)
                    Dim timage As Integer = pjData.Dat.Data(SCDatFiles.DatFiles.sprites, "Image File", tSprite)

                    ImageIndex = timage
                Case SCDatFiles.DatFiles.sprites
                    Dim timage As Integer = pjData.Dat.Data(SCDatFiles.DatFiles.sprites, "Image File", value)

                    ImageIndex = timage
                Case SCDatFiles.DatFiles.images
                    ImageIndex = value
                Case SCDatFiles.DatFiles.upgrades
                    Dim tIcon As Integer = pjData.Dat.Data(SCDatFiles.DatFiles.upgrades, "Icon", value)

                    isIcon = True
                    ImageIndex = tIcon
                Case SCDatFiles.DatFiles.techdata
                    Dim tIcon As Integer = pjData.Dat.Data(SCDatFiles.DatFiles.techdata, "Icon", value)

                    isIcon = True
                    ImageIndex = tIcon
                Case SCDatFiles.DatFiles.orders
                    Dim tIcon As Integer = pjData.Dat.Data(SCDatFiles.DatFiles.orders, "Highlight", value)

                    isIcon = True
                    ImageIndex = tIcon
                Case SCDatFiles.DatFiles.Icon
                    isIcon = True
                    ImageIndex = value
            End Select
            If isIcon Then
                Return scData.GetIcon(ImageIndex)
            Else
                Return scData.GetGRPImage(ImageIndex, 12)
            End If
        End Get
    End Property

    Public ReadOnly Property Explain As String
        Get
            Return Tool.GetText(Datfilesname(Datfile) & "_" & Parameter)
        End Get
    End Property

    Public ReadOnly Property ComboxItems As String()
        Get
            Return Tool.GetText(Datfilesname(Datfile) & "_" & Parameter & "_V").Split("|")
        End Get
    End Property


    Public ReadOnly Property BackColor As SolidColorBrush
        Get
            Dim tvalue As Long = pjData.Dat.Data(Datfile, Parameter, ObjectID)
            Dim TrueValue As Long = scData.DefaultDat.Data(Datfile, Parameter, ObjectID)
            Dim IsDefault As Boolean = pjData.Dat.Values(Datfile, Parameter, ObjectID).IsDefault
            Dim IsMapDataDefault As Boolean = True

            If pjData.IsMapLoading Then
                IsMapDataDefault = pjData.MapData.DatFile.Values(Datfile, Parameter, ObjectID).IsDefault
            End If


            ' 如果是地图中定义的情况。

            If IsDefault And Not IsMapDataDefault Then ' 如果存在地图数据
                Return New SolidColorBrush(pgData.FiledMapEditColor)
            Else
                If IsDefault Then

                    ' MsgBox("灰色")
                    Return New SolidColorBrush(pgData.FiledDefault)
                Else

                    ' MsgBox("红色")
                    Return New SolidColorBrush(pgData.FiledEditColor)
                End If
            End If
            Return New SolidColorBrush(pgData.FiledMapEditColor)
        End Get
    End Property

    'MaterialDesignBody

    Public Sub DataReset()
        pjData.SetDirty(True)

        pjData.Dat.Values(Datfile, Parameter, ObjectID).IsDefault = True
        pjData.Dat.Data(Datfile, Parameter, ObjectID) = scData.DefaultDat.Data(Datfile, Parameter, ObjectID)

        PropertyChangedPack()
    End Sub




    Private Sub NotifyPropertyChanged(ByVal info As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
    End Sub

    Public Class FlagBinding
        Implements INotifyPropertyChanged

        Private ParentBinding As DatBinding

        Private Datfile As SCDatFiles.DatFiles
        Private Parameter As String
        Private ObjectID As Integer
        Private FlagIndex As Integer

        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

        Public Sub New(tParentBinding As DatBinding, tDatfile As SCDatFiles.DatFiles, tParameter As String, tObjectID As Integer, tFlagIndex As Integer)
            ParentBinding = tParentBinding

            Datfile = tDatfile
            Parameter = tParameter
            ObjectID = tObjectID
            FlagIndex = tFlagIndex
        End Sub


        Public Property MiniFlag As Boolean
            Get
                Dim value As Long = pjData.Dat.Data(Datfile, Parameter, ObjectID)
                Dim flag As Boolean = (value And Math.Pow(2, FlagIndex))


                Return flag
            End Get

            Set(ByVal tvalue As Boolean)
                Dim value As Long = pjData.Dat.Data(Datfile, Parameter, ObjectID)

                ' 原值
                Dim flag As Boolean = (value And Math.Pow(2, FlagIndex))

                If tvalue <> flag Then ' 值已更改
                    If tvalue Then
                        Dim CValue As Long = value + Math.Pow(2, FlagIndex)
                        pjData.Dat.Data(Datfile, Parameter, ObjectID) = CValue
                    Else
                        Dim CValue As Long = value - Math.Pow(2, FlagIndex)
                        pjData.Dat.Data(Datfile, Parameter, ObjectID) = CValue
                    End If

                    '1111 1001
                    '1111 1101

                    '1111 1001
                    '0000 0100


                    '1111 1101
                    '1111 1001

                    '1111 1011

                    pjData.Dat.Values(Datfile, Parameter, ObjectID).IsDefault = False

                    ParentBinding.PropertyChangedPack()
                    PropertyChangedPack()
                End If




                'If Not (tvalue = pjData.Dat.Data(Datfile, Parameter, ObjectID)) Then
                ' 'MsgBox("数据查找设置")
                '    pjData.Dat.Data(Datfile, Parameter, ObjectID) = tvalue
                '    pjData.Dat.Values(Datfile, Parameter, ObjectID).IsDefault = False

                'End If
            End Set
        End Property

        Public ReadOnly Property MiniBackColor As SolidColorBrush
            Get
                Dim value As Long = pjData.Dat.Data(Datfile, Parameter, ObjectID)
                ' 当前值
                Dim flag As Boolean = (value And Math.Pow(2, FlagIndex))

                Dim Truevalue As Long = scData.DefaultDat.Data(Datfile, Parameter, ObjectID)
                ' 实际值
                Dim Trueflag As Boolean = (Truevalue And Math.Pow(2, FlagIndex))


                Dim IsDefault As Boolean = pjData.Dat.Values(Datfile, Parameter, ObjectID).IsDefault



                If Not IsDefault Then
                    If (flag <> Trueflag) Then ' 如果是修改后的值
                        Return New SolidColorBrush(pgData.FiledFalgColor)
                    Else
                        If flag Then
                            Return New SolidColorBrush(pgData.FiledMapEditColor)
                        Else
                            Return New SolidColorBrush(Color.FromArgb(0, 0, 0, 0))
                        End If
                    End If
                End If


                'New SolidColorBrush(pgData.FiledEditColor)
                If flag Then
                    Return New SolidColorBrush(pgData.FiledMapEditColor)
                Else
                    Return New SolidColorBrush(pgData.FiledDefault)
                End If



                ' If IsDefault And Not IsMapDataDefault Then '如果存在地图数据
                '    Return New SolidColorBrush(pgData.FiledMapEditColor)
                'Else
                '    If IsDefault Then

                ' 'MsgBox("灰色")
                '        Return New SolidColorBrush(pgData.FiledDefault)
                '    Else

                ' 'MsgBox("红色")
                '        Return New SolidColorBrush(pgData.FiledEditColor)
                '    End If
                'End If
                'Return New SolidColorBrush(pgData.FiledMapEditColor)
            End Get
        End Property



        Public Sub PropertyChangedPack()
            NotifyPropertyChanged("MiniFlag")
            NotifyPropertyChanged("MiniBackColor")
        End Sub


        Private Sub NotifyPropertyChanged(ByVal info As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
        End Sub
    End Class
End Class

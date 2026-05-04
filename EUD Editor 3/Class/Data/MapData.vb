Imports System.IO
Imports System.Text

Public Class MapData
    Public ReadOnly Property LoadComplete As Boolean

    Private Dat As SCDatFiles
    Public ReadOnly Property DatFile As SCDatFiles
        Get
            Return Dat
        End Get
    End Property

    Private _MapName As UShort
    Public ReadOnly Property MapName As String
        Get
            Return Strings(_MapName - 1)
        End Get
    End Property

    Private _MapDes As UShort
    Public ReadOnly Property MapDes As String
        Get
            Return Strings(_MapDes - 1)
        End Get
    End Property


    Private _UserInfo As List(Of UserINFOData)
    Public ReadOnly Property UserInfo(index As Integer) As UserINFOData
        Get
            Return _UserInfo(index)
        End Get
    End Property
    Public Class UserINFOData
        Public number As Byte
        Public force As Byte
        Public control As EControl
        Public Enum EControl
            Human
            Computer
            NoPlayer
        End Enum
    End Class





    Private _WavIndex As List(Of Integer)
    Public ReadOnly Property WavIndex(index As Integer) As String
        Get
            If _WavIndex.Count > index Then
                Return Str(_WavIndex(index) - 1).Replace("\", "/")
            Else
                Return "Error"
            End If
        End Get
    End Property
    Public ReadOnly Property WavCount() As Integer
        Get
            Return _WavIndex.Count
        End Get
    End Property


    Private Strings As List(Of String)
    Public ReadOnly Property Str(index As Integer) As String
        Get
            If Strings.Count > index Then
                Return Strings(index)
            Else
                Return "Error"
            End If
        End Get
    End Property

    Private pLocationName(255) As Integer
    Public ReadOnly Property LocationName(index As Integer) As String
        Get
            If 255 >= index Then
                If pLocationName(index) = 0 Then
                    Return ""
                Else
                    Return Strings(pLocationName(index) - 1)
                End If
            Else
                Return "Error"
            End If
        End Get
    End Property


    Private pSwitchName(255) As Integer
    Public ReadOnly Property SwitchName(index As Integer) As String
        Get
            If 255 >= index Then
                If pSwitchName(index) = 0 Then
                    Return "Switch " & (index + 1)
                Else
                    Return Strings(pSwitchName(index) - 1)
                End If
            Else
                Return "Error"
            End If
        End Get
    End Property
    Public Sub New(MapName As String)
        Try
            'LoadDataSFMPQ(MapName)
            LoadDataStormLib(MapName)
        Catch ex As Exception
            Tool.ErrorMsgBox(Tool.GetText("Error MapOpen"), ex.ToString)
            Dat = Nothing
            LoadComplete = False
            Return
        End Try
        LoadComplete = True
    End Sub

    Private Function SearchCHK(chkname As String, binary As BinaryReader)
        Dim _name As String = ""
        Dim _size As UInteger

        binary.BaseStream.Position = 0
        While (True)

            Try
                _name = ""
                _name = _name & ChrW(binary.ReadByte)
                _name = _name & ChrW(binary.ReadByte)
                _name = _name & ChrW(binary.ReadByte)
                _name = _name & ChrW(binary.ReadByte)
            Catch ex As Exception
                Return False
            End Try

            If _name = chkname Then
                Return True
            Else
                _size = binary.ReadUInt32()
                binary.BaseStream.Position += _size
            End If
        End While
        Return True
    End Function

    Public Function ReLoad(MapName As String) As Boolean
        Try
            'LoadDataSFMPQ(MapName)
            LoadDataStormLib(MapName)
        Catch ex As Exception
            Tool.ErrorMsgBox(Tool.GetText("Error MapOpen"), ex.ToString)
            Dat = Nothing
            Return False
        End Try
        Return True
    End Function


    Private Sub LoadDataStormLib(MapName As String)
        Dat = New SCDatFiles(True)
        Strings = New List(Of String)
        _WavIndex = New List(Of Integer)
        _UserInfo = New List(Of UserINFOData)

        Dim hmpq As ULong
        Dim hfile As ULong
        Dim buffer() As Byte
        Dim filesize As UInteger
        Dim size As Integer

        Dim pdwread As IntPtr

        StormLib.SFileOpenArchive(MapName, 0, 0, hmpq)
        Dim openFilename As String = "staredit\scenario.chk"

        StormLib.SFileOpenFileEx(hmpq, openFilename, 0, hfile)
        If hfile <> 0 Then
            filesize = StormLib.SFileGetFileSize(hfile, filesize)
            ReDim buffer(filesize)
            StormLib.SFileReadFile(hfile, buffer, filesize, pdwread, 0)
            StormLib.SFileCloseFile(hfile)
            StormLib.SFileCloseArchive(hmpq)

            Dim mem As MemoryStream = New MemoryStream(buffer)
            Dim binary As BinaryReader = New BinaryReader(mem)





            ' 确认地图的有效性
            Try
                SearchCHK("TYPE", binary)
                size = binary.ReadUInt32
                If (binary.ReadUInt32 <> 1113014610) Then
                    Throw New Exception(Tool.GetText("Error Not support SCM"))
                    Exit Sub
                End If
            Catch ex As Exception
                Throw New Exception(Tool.GetText("Error Not support SCM"))
            End Try


            If True Then
                SearchCHK("OWNR", binary)

                size = binary.ReadUInt32
                ' OWNR 1字节 12个
                ' 03 = 可建造
                ' 05 = 电脑
                ' 06 = 人类
                ' 07 = 中立
                ' 00 = 未使用（表示该玩家处于不可用状态。）
                For i = 0 To 7
                    Dim b As Byte = binary.ReadByte()
                    Dim userINFO As UserINFOData = New UserINFOData()
                    userINFO.number = i

                    Select Case b
                        Case 3, 5, 7
                            userINFO.control = UserINFOData.EControl.Computer
                        Case 6
                            userINFO.control = UserINFOData.EControl.Human
                        Case Else
                            userINFO.control = UserINFOData.EControl.NoPlayer

                    End Select

                    _UserInfo.Add(userINFO)
                Next
            End If



            If True Then
                SearchCHK("FORC", binary)

                size = binary.ReadUInt32
                ' FORC 1字节 8个 2字节 4个
                ' 1400 0000 = 段落长度
                ' 0001 0203 0000 0000 = 各玩家所属势力(Force)，（按顺序 Player 1,2,3,4,5,6,7,8）
                '00 = Force 1
                '01 = Force 2
                '02 = Force 3
                '03 = Force 4
                For i = 0 To 7
                    Dim b As Byte = binary.ReadByte()
                    _UserInfo(i).force = b
                Next
            End If


            If True Then
                SearchCHK("MRGN", binary)

                size = binary.ReadUInt32

                For i = 0 To 255
                    binary.ReadUInt32()
                    binary.ReadUInt32()
                    binary.ReadUInt32()
                    binary.ReadUInt32()

                    pLocationName(i) = binary.ReadUInt16()
                    binary.ReadUInt16()
                Next
            End If

            If True Then
                'mem.Position = SearchCHK("SWNM", buffer)
                SearchCHK("SWNM", binary)
                size = binary.ReadUInt32
                For i = 0 To 255
                    pSwitchName(i) = binary.ReadUInt32
                Next
            End If



            If True Then
                SearchCHK("UPGx", binary)
                size = binary.ReadUInt32

                Dim TEMPIsUPGDefault() As Byte
                ' ## (61个) = 每个升级的允许状态
                ' - 00 = 遵循变化值
                ' - 01 = 遵循默认值
                TEMPIsUPGDefault = binary.ReadBytes(62)


                Dim TEMPUPGMin(60) As UInteger
                ' #### (61个) = 第一次升级的矿物花费
                For i = 0 To 60
                    TEMPUPGMin(i) = binary.ReadUInt16()
                Next

                Dim TEMPUPGADDMin(60) As UInteger
                ' #### (61个) = 额外升级的矿物花费
                For i = 0 To 60
                    TEMPUPGADDMin(i) = binary.ReadUInt16()
                Next

                Dim TEMPUPGGas(60) As UInteger
                ' #### (61个) = 第一次升级的气体花费
                For i = 0 To 60
                    TEMPUPGGas(i) = binary.ReadUInt16()
                Next

                Dim TEMPUPGADDGas(60) As UInteger
                ' #### (61个) = 额外升级的气体花费
                For i = 0 To 60
                    TEMPUPGADDGas(i) = binary.ReadUInt16()
                Next

                Dim TEMPUPGTime(60) As UInteger
                ' #### (61个) = 第一次升级的时间
                For i = 0 To 60
                    TEMPUPGTime(i) = binary.ReadUInt16()
                Next

                Dim TEMPUPGADDTime(60) As UInteger
                ' #### (61个) = 额外升级的时间
                For i = 0 To 60
                    TEMPUPGADDTime(i) = binary.ReadUInt16()
                Next



                For i = 0 To 60
                    If TEMPIsUPGDefault(i) = 0 Then
                        Dim key As String

                        key = "Mineral Cost Base"
                        Dat.Data(SCDatFiles.DatFiles.upgrades, key, i) = TEMPUPGMin(i)
                        Dat.Values(SCDatFiles.DatFiles.upgrades, key, i).IsDefault = False

                        key = "Mineral Cost Factor"
                        Dat.Data(SCDatFiles.DatFiles.upgrades, key, i) = TEMPUPGADDMin(i)
                        Dat.Values(SCDatFiles.DatFiles.upgrades, key, i).IsDefault = False

                        key = "Vespene Cost Base"
                        Dat.Data(SCDatFiles.DatFiles.upgrades, key, i) = TEMPUPGGas(i)
                        Dat.Values(SCDatFiles.DatFiles.upgrades, key, i).IsDefault = False

                        key = "Vespene Cost Factor"
                        Dat.Data(SCDatFiles.DatFiles.upgrades, key, i) = TEMPUPGADDGas(i)
                        Dat.Values(SCDatFiles.DatFiles.upgrades, key, i).IsDefault = False

                        key = "Research Time Base"
                        Dat.Data(SCDatFiles.DatFiles.upgrades, key, i) = TEMPUPGTime(i)
                        Dat.Values(SCDatFiles.DatFiles.upgrades, key, i).IsDefault = False

                        key = "Research Time Factor"
                        Dat.Data(SCDatFiles.DatFiles.upgrades, key, i) = TEMPUPGADDTime(i)
                        Dat.Values(SCDatFiles.DatFiles.upgrades, key, i).IsDefault = False
                    End If
                Next
            End If



            If True Then
                SearchCHK("TECx", binary)
                size = binary.ReadUInt32



                Dim TEMPIsTECHDefault() As Byte
                ' 0# = 技能的允许状态 (00 不可用, 01 可用)
                TEMPIsTECHDefault = binary.ReadBytes(44)


                Dim TEMPTECHMin(43) As UInteger
                ' #### = 矿物花费
                For i = 0 To 43
                    TEMPTECHMin(i) = binary.ReadUInt16()
                Next

                Dim TEMPTECHGas(43) As UInteger
                ' #### = 气体花费
                For i = 0 To 43
                    TEMPTECHGas(i) = binary.ReadUInt16()
                Next

                Dim TEMPTECHTime(43) As UInteger
                ' #### = 所需时间
                For i = 0 To 43
                    TEMPTECHTime(i) = binary.ReadUInt16()
                Next

                Dim TEMPTECHADDEnerge(43) As UInteger
                ' ##00 = 所需法力
                For i = 0 To 43
                    TEMPTECHADDEnerge(i) = binary.ReadUInt16()
                Next

                For i = 0 To 43
                    If TEMPIsTECHDefault(i) = 0 Then
                        Dim Key As String

                        Key = "Mineral Cost"
                        Dat.Data(SCDatFiles.DatFiles.techdata, Key, i) = TEMPTECHMin(i)
                        Dat.Values(SCDatFiles.DatFiles.techdata, Key, i).IsDefault = False

                        Key = "Vespene Cost"
                        Dat.Data(SCDatFiles.DatFiles.techdata, Key, i) = TEMPTECHGas(i)
                        Dat.Values(SCDatFiles.DatFiles.techdata, Key, i).IsDefault = False

                        Key = "Resarch Time"
                        Dat.Data(SCDatFiles.DatFiles.techdata, Key, i) = TEMPTECHTime(i)
                        Dat.Values(SCDatFiles.DatFiles.techdata, Key, i).IsDefault = False

                        Key = "Energy Required"
                        Dat.Data(SCDatFiles.DatFiles.techdata, Key, i) = TEMPTECHADDEnerge(i)
                        Dat.Values(SCDatFiles.DatFiles.techdata, Key, i).IsDefault = False
                    End If
                Next

            End If

            If True Then
                SearchCHK("WAV ", binary)
                size = binary.ReadUInt32
                For i = 0 To (size / 4) - 1
                    Dim v As UInteger = binary.ReadUInt32()
                    If v > 0 Then
                        _WavIndex.Add(v)
                    End If
                Next
            End If





            If True Then
                SearchCHK("UNIx", binary)
                size = binary.ReadUInt32

                Dim TEMPIsUnitDefault() As Byte
                ' ## (228个) = 按单位顺序排列的数组
                ' - 00 (遵循变化值)
                ' - 01 (遵循默认值)
                TEMPIsUnitDefault = binary.ReadBytes(228)

                Dim TEMPUnitHP(227) As UInteger
                ' 00## ##00 (228个) = ????部分是单位的生命值
                For i = 0 To 227
                    TEMPUnitHP(i) = binary.ReadUInt32()
                Next

                Dim TEMPUnitSh(227) As UShort
                ' #### (228个) = 护盾
                For i = 0 To 227
                    TEMPUnitSh(i) = binary.ReadUInt16()
                Next

                Dim TEMPUnitAp() As Byte
                ' ## (228个) = 防御力
                TEMPUnitAp = binary.ReadBytes(228)

                Dim TEMPUnitBt(227) As UShort
                ' #### (228个) = 生产时间
                For i = 0 To 227
                    TEMPUnitBt(i) = binary.ReadUInt16()
                Next

                Dim TEMPMinCost(227) As UShort
                ' #### (228个) = 矿物花费
                For i = 0 To 227
                    TEMPMinCost(i) = binary.ReadUInt16()
                Next

                Dim TEMPGasCost(227) As UShort
                ' #### (228个) = 气体花费
                For i = 0 To 227
                    TEMPGasCost(i) = binary.ReadUInt16()
                Next

                Dim TEMPUnitstr(227) As UShort
                ' #### (228个) = 字符串编号
                For i = 0 To 227
                    TEMPUnitstr(i) = binary.ReadUInt16()
                Next

                Dim TEMPWeaDmg(129) As UShort
                ' #### (130个) = 每个武器的基本攻击力
                For i = 0 To 129
                    TEMPWeaDmg(i) = binary.ReadUInt16()
                Next

                Dim TEMPWeaUmg(129) As UShort
                ' #### (130个) = 升级时增加的攻击力
                For i = 0 To 129
                    TEMPWeaUmg(i) = binary.ReadUInt16()
                Next


                For i = 0 To 227
                    If TEMPIsUnitDefault(i) = 0 Then
                        Dim Key As String
                        Key = "Hit Points"
                        Dat.Data(SCDatFiles.DatFiles.units, Key, i) = TEMPUnitHP(i)
                        Dat.Values(SCDatFiles.DatFiles.units, Key, i).IsDefault = False

                        Key = "Shield Amount"
                        Dat.Data(SCDatFiles.DatFiles.units, Key, i) = TEMPUnitSh(i)
                        Dat.Values(SCDatFiles.DatFiles.units, Key, i).IsDefault = False

                        Key = "Armor"
                        Dat.Data(SCDatFiles.DatFiles.units, Key, i) = TEMPUnitAp(i)
                        Dat.Values(SCDatFiles.DatFiles.units, Key, i).IsDefault = False

                        Key = "Build Time"
                        Dat.Data(SCDatFiles.DatFiles.units, Key, i) = TEMPUnitBt(i)
                        Dat.Values(SCDatFiles.DatFiles.units, Key, i).IsDefault = False

                        Key = "Mineral Cost"
                        Dat.Data(SCDatFiles.DatFiles.units, Key, i) = TEMPMinCost(i)
                        Dat.Values(SCDatFiles.DatFiles.units, Key, i).IsDefault = False

                        Key = "Vespene Cost"
                        Dat.Data(SCDatFiles.DatFiles.units, Key, i) = TEMPGasCost(i)
                        Dat.Values(SCDatFiles.DatFiles.units, Key, i).IsDefault = False

                        Key = "Unit Map String"
                        Dat.Data(SCDatFiles.DatFiles.units, Key, i) = TEMPUnitstr(i)
                        Dat.Values(SCDatFiles.DatFiles.units, Key, i).IsDefault = False


                        Key = "Ground Weapon"
                        Dim weaponnum As Integer = scData.DefaultDat.Data(SCDatFiles.DatFiles.units, Key, i) ' 给出单位i的对地武器和对空武器编号。


                        If weaponnum <> 130 Then
                            Key = "Damage Amount"
                            Dat.Data(SCDatFiles.DatFiles.weapons, Key, weaponnum) = TEMPWeaDmg(weaponnum)
                            Dat.Values(SCDatFiles.DatFiles.weapons, Key, weaponnum).IsDefault = False
                            Key = "Damage Bonus"
                            Dat.Data(SCDatFiles.DatFiles.weapons, Key, weaponnum) = TEMPWeaUmg(weaponnum)
                            Dat.Values(SCDatFiles.DatFiles.weapons, Key, weaponnum).IsDefault = False
                        End If


                        Key = "Air Weapon"
                        weaponnum = scData.DefaultDat.Data(SCDatFiles.DatFiles.units, Key, i) ' 给出单位i的对地武器和对空武器编号。

                        If weaponnum <> 130 Then
                            Key = "Damage Amount"
                            Dat.Data(SCDatFiles.DatFiles.weapons, Key, weaponnum) = TEMPWeaDmg(weaponnum)
                            Dat.Values(SCDatFiles.DatFiles.weapons, Key, weaponnum).IsDefault = False
                            Key = "Damage Bonus"
                            Dat.Data(SCDatFiles.DatFiles.weapons, Key, weaponnum) = TEMPWeaUmg(weaponnum)
                            Dat.Values(SCDatFiles.DatFiles.weapons, Key, weaponnum).IsDefault = False
                        End If
                    End If
                Next
            End If



            Dim f As Boolean = SearchCHK("STRx", binary)
            If f Then
                If pjData.TextEncoding Is Nothing Then
                    pjData.TextEncoding = System.Text.Encoding.UTF8
                End If

                size = binary.ReadUInt32
                Dim BasePos As UInteger = mem.Position
                Dim strCount As UInteger = binary.ReadUInt32()
                Dim lastPos As UInteger = mem.Position

                For i = 0 To strCount - 1 ' 按大小重复
                    mem.Position = lastPos

                    Dim StartPos As UInteger = binary.ReadUInt32()
                    mem.Position = BasePos + StartPos
                    While (binary.ReadByte <> 0)
                    End While
                    Dim Bytecount As Integer = mem.Position - (BasePos + StartPos)
                    mem.Position = BasePos + StartPos

                    Dim b As Byte() = binary.ReadBytes(Bytecount - 1)

                    Dim strstartext As String = pjData.TextEncoding.GetString(b)
                    Strings.Add(strstartext)
                    lastPos += 4
                Next
            Else
                If pjData.TextEncoding Is Nothing Then
                    pjData.TextEncoding = System.Text.Encoding.GetEncoding(0)
                End If

                SearchCHK("STR ", binary)

                size = binary.ReadUInt32



                Dim BasePos As UInteger = mem.Position
                Dim strCount As UInt16 = binary.ReadUInt16()
                Dim lastPos As UInteger = mem.Position

                For i = 0 To strCount - 1 ' 按大小重复
                    mem.Position = lastPos
                    Dim StartPos As UInteger = binary.ReadUInt16()
                    mem.Position = BasePos + StartPos

                    While (binary.ReadByte <> 0)
                    End While
                    Dim Bytecount As Integer = mem.Position - (BasePos + StartPos)
                    mem.Position = BasePos + StartPos

                    Dim b As Byte() = binary.ReadBytes(Bytecount - 1)

                    Dim strstartext As String = pjData.TextEncoding.GetString(b)
                    Strings.Add(strstartext)
                    lastPos += 2
                Next
            End If
            If True Then
                SearchCHK("SPRP", binary)
                size = binary.ReadUInt32
                _MapName = binary.ReadUInt16
                _MapDes = binary.ReadUInt16
            End If
            binary.Close()
            mem.Close()
        Else
            StormLib.SFileCloseArchive(hmpq)
        End If
    End Sub


    Private Sub LoadDataSFMPQ(MapName As String)
        Dat = New SCDatFiles(True)
        Strings = New List(Of String)
        _WavIndex = New List(Of Integer)
        Dim hmpq As UInteger
        Dim hfile As UInteger
        Dim buffer() As Byte
        Dim filesize As UInteger
        Dim size As Integer
        Dim pdwread As IntPtr

        StormLib.SFileOpenArchive(MapName, 0, 0, hmpq)
        Dim openFilename As String = "staredit\scenario.chk"
        StormLib.SFileOpenFileEx(hmpq, openFilename, 0, hfile)
        If hfile <> 0 Then
            filesize = StormLib.SFileGetFileSize(hfile, filesize)
            ReDim buffer(filesize)
            StormLib.SFileReadFile(hfile, buffer, filesize, pdwread, 0)
            StormLib.SFileCloseFile(hfile)
            StormLib.SFileCloseArchive(hmpq)

            Dim mem As MemoryStream = New MemoryStream(buffer)
            Dim binary As BinaryReader = New BinaryReader(mem)





            ' 确认地图的有效性
            Try
                SearchCHK("TYPE", binary)
                size = binary.ReadUInt32
                If (binary.ReadUInt32 <> 1113014610) Then
                    Throw New Exception(Tool.GetText("Error Not support SCM"))
                    Exit Sub
                End If
            Catch ex As Exception
                Throw New Exception(Tool.GetText("Error Not support SCM"))
            End Try



            If True Then
                SearchCHK("MRGN", binary)

                size = binary.ReadUInt32

                For i = 0 To 255
                    binary.ReadUInt32()
                    binary.ReadUInt32()
                    binary.ReadUInt32()
                    binary.ReadUInt32()

                    pLocationName(i) = binary.ReadUInt16()
                    binary.ReadUInt16()
                Next
            End If

            If True Then
                'mem.Position = SearchCHK("SWNM", buffer)
                SearchCHK("SWNM", binary)
                size = binary.ReadUInt32
                For i = 0 To 255
                    pSwitchName(i) = binary.ReadUInt32
                Next
            End If



            If True Then
                SearchCHK("UPGx", binary)
                size = binary.ReadUInt32

                Dim TEMPIsUPGDefault() As Byte
                ' ## (61个) = 每个升级的允许状态
                ' - 00 = 遵循变化值
                ' - 01 = 遵循默认值
                TEMPIsUPGDefault = binary.ReadBytes(62)


                Dim TEMPUPGMin(60) As UInteger
                ' #### (61个) = 第一次升级的矿物花费
                For i = 0 To 60
                    TEMPUPGMin(i) = binary.ReadUInt16()
                Next

                Dim TEMPUPGADDMin(60) As UInteger
                ' #### (61个) = 额外升级的矿物花费
                For i = 0 To 60
                    TEMPUPGADDMin(i) = binary.ReadUInt16()
                Next

                Dim TEMPUPGGas(60) As UInteger
                ' #### (61个) = 第一次升级的气体花费
                For i = 0 To 60
                    TEMPUPGGas(i) = binary.ReadUInt16()
                Next

                Dim TEMPUPGADDGas(60) As UInteger
                ' #### (61个) = 额外升级的气体花费
                For i = 0 To 60
                    TEMPUPGADDGas(i) = binary.ReadUInt16()
                Next

                Dim TEMPUPGTime(60) As UInteger
                ' #### (61个) = 第一次升级的时间
                For i = 0 To 60
                    TEMPUPGTime(i) = binary.ReadUInt16()
                Next

                Dim TEMPUPGADDTime(60) As UInteger
                ' #### (61个) = 额外升级的时间
                For i = 0 To 60
                    TEMPUPGADDTime(i) = binary.ReadUInt16()
                Next



                For i = 0 To 60
                    If TEMPIsUPGDefault(i) = 0 Then
                        Dim key As String

                        key = "Mineral Cost Base"
                        Dat.Data(SCDatFiles.DatFiles.upgrades, key, i) = TEMPUPGMin(i)
                        Dat.Values(SCDatFiles.DatFiles.upgrades, key, i).IsDefault = False

                        key = "Mineral Cost Factor"
                        Dat.Data(SCDatFiles.DatFiles.upgrades, key, i) = TEMPUPGADDMin(i)
                        Dat.Values(SCDatFiles.DatFiles.upgrades, key, i).IsDefault = False

                        key = "Vespene Cost Base"
                        Dat.Data(SCDatFiles.DatFiles.upgrades, key, i) = TEMPUPGGas(i)
                        Dat.Values(SCDatFiles.DatFiles.upgrades, key, i).IsDefault = False

                        key = "Vespene Cost Factor"
                        Dat.Data(SCDatFiles.DatFiles.upgrades, key, i) = TEMPUPGADDGas(i)
                        Dat.Values(SCDatFiles.DatFiles.upgrades, key, i).IsDefault = False

                        key = "Research Time Base"
                        Dat.Data(SCDatFiles.DatFiles.upgrades, key, i) = TEMPUPGTime(i)
                        Dat.Values(SCDatFiles.DatFiles.upgrades, key, i).IsDefault = False

                        key = "Research Time Factor"
                        Dat.Data(SCDatFiles.DatFiles.upgrades, key, i) = TEMPUPGADDTime(i)
                        Dat.Values(SCDatFiles.DatFiles.upgrades, key, i).IsDefault = False
                    End If
                Next
            End If



            If True Then
                SearchCHK("TECx", binary)
                size = binary.ReadUInt32



                Dim TEMPIsTECHDefault() As Byte
                ' 0# = 技能的允许状态 (00 不可用, 01 可用)
                TEMPIsTECHDefault = binary.ReadBytes(44)


                Dim TEMPTECHMin(43) As UInteger
                ' #### = 矿物花费
                For i = 0 To 43
                    TEMPTECHMin(i) = binary.ReadUInt16()
                Next

                Dim TEMPTECHGas(43) As UInteger
                ' #### = 气体花费
                For i = 0 To 43
                    TEMPTECHGas(i) = binary.ReadUInt16()
                Next

                Dim TEMPTECHTime(43) As UInteger
                ' #### = 所需时间
                For i = 0 To 43
                    TEMPTECHTime(i) = binary.ReadUInt16()
                Next

                Dim TEMPTECHADDEnerge(43) As UInteger
                ' ##00 = 所需法力
                For i = 0 To 43
                    TEMPTECHADDEnerge(i) = binary.ReadUInt16()
                Next

                For i = 0 To 43
                    If TEMPIsTECHDefault(i) = 0 Then
                        Dim Key As String

                        Key = "Mineral Cost"
                        Dat.Data(SCDatFiles.DatFiles.techdata, Key, i) = TEMPTECHMin(i)
                        Dat.Values(SCDatFiles.DatFiles.techdata, Key, i).IsDefault = False

                        Key = "Vespene Cost"
                        Dat.Data(SCDatFiles.DatFiles.techdata, Key, i) = TEMPTECHGas(i)
                        Dat.Values(SCDatFiles.DatFiles.techdata, Key, i).IsDefault = False

                        Key = "Resarch Time"
                        Dat.Data(SCDatFiles.DatFiles.techdata, Key, i) = TEMPTECHTime(i)
                        Dat.Values(SCDatFiles.DatFiles.techdata, Key, i).IsDefault = False

                        Key = "Energy Required"
                        Dat.Data(SCDatFiles.DatFiles.techdata, Key, i) = TEMPTECHADDEnerge(i)
                        Dat.Values(SCDatFiles.DatFiles.techdata, Key, i).IsDefault = False
                    End If
                Next

            End If

            If True Then
                SearchCHK("WAV ", binary)
                size = binary.ReadUInt32
                For i = 0 To (size / 4) - 1
                    Dim v As UInteger = binary.ReadUInt32()
                    If v > 0 Then
                        _WavIndex.Add(v)
                    End If
                Next
            End If





            If True Then
                SearchCHK("UNIx", binary)
                size = binary.ReadUInt32

                Dim TEMPIsUnitDefault() As Byte
                ' ## (228个) = 按单位顺序排列的数组
                ' - 00 (遵循变化值)
                ' - 01 (遵循默认值)
                TEMPIsUnitDefault = binary.ReadBytes(228)

                Dim TEMPUnitHP(227) As UInteger
                ' 00## ##00 (228个) = ????部分是单位的生命值
                For i = 0 To 227
                    TEMPUnitHP(i) = binary.ReadUInt32()
                Next

                Dim TEMPUnitSh(227) As UShort
                ' #### (228个) = 护盾
                For i = 0 To 227
                    TEMPUnitSh(i) = binary.ReadUInt16()
                Next

                Dim TEMPUnitAp() As Byte
                ' ## (228个) = 防御力
                TEMPUnitAp = binary.ReadBytes(228)

                Dim TEMPUnitBt(227) As UShort
                ' #### (228个) = 生产时间
                For i = 0 To 227
                    TEMPUnitBt(i) = binary.ReadUInt16()
                Next

                Dim TEMPMinCost(227) As UShort
                ' #### (228个) = 矿物花费
                For i = 0 To 227
                    TEMPMinCost(i) = binary.ReadUInt16()
                Next

                Dim TEMPGasCost(227) As UShort
                ' #### (228个) = 气体花费
                For i = 0 To 227
                    TEMPGasCost(i) = binary.ReadUInt16()
                Next

                Dim TEMPUnitstr(227) As UShort
                ' #### (228个) = 字符串编号
                For i = 0 To 227
                    TEMPUnitstr(i) = binary.ReadUInt16()
                Next

                Dim TEMPWeaDmg(129) As UShort
                ' #### (130个) = 每个武器的基本攻击力
                For i = 0 To 129
                    TEMPWeaDmg(i) = binary.ReadUInt16()
                Next

                Dim TEMPWeaUmg(129) As UShort
                ' #### (130个) = 升级时增加的攻击力
                For i = 0 To 129
                    TEMPWeaUmg(i) = binary.ReadUInt16()
                Next


                For i = 0 To 227
                    If TEMPIsUnitDefault(i) = 0 Then
                        Dim Key As String
                        Key = "Hit Points"
                        Dat.Data(SCDatFiles.DatFiles.units, Key, i) = TEMPUnitHP(i)
                        Dat.Values(SCDatFiles.DatFiles.units, Key, i).IsDefault = False

                        Key = "Shield Amount"
                        Dat.Data(SCDatFiles.DatFiles.units, Key, i) = TEMPUnitSh(i)
                        Dat.Values(SCDatFiles.DatFiles.units, Key, i).IsDefault = False

                        Key = "Armor"
                        Dat.Data(SCDatFiles.DatFiles.units, Key, i) = TEMPUnitAp(i)
                        Dat.Values(SCDatFiles.DatFiles.units, Key, i).IsDefault = False

                        Key = "Build Time"
                        Dat.Data(SCDatFiles.DatFiles.units, Key, i) = TEMPUnitBt(i)
                        Dat.Values(SCDatFiles.DatFiles.units, Key, i).IsDefault = False

                        Key = "Mineral Cost"
                        Dat.Data(SCDatFiles.DatFiles.units, Key, i) = TEMPMinCost(i)
                        Dat.Values(SCDatFiles.DatFiles.units, Key, i).IsDefault = False

                        Key = "Vespene Cost"
                        Dat.Data(SCDatFiles.DatFiles.units, Key, i) = TEMPGasCost(i)
                        Dat.Values(SCDatFiles.DatFiles.units, Key, i).IsDefault = False

                        Key = "Unit Map String"
                        Dat.Data(SCDatFiles.DatFiles.units, Key, i) = TEMPUnitstr(i)
                        Dat.Values(SCDatFiles.DatFiles.units, Key, i).IsDefault = False


                        Key = "Ground Weapon"
                        Dim weaponnum As Integer = scData.DefaultDat.Data(SCDatFiles.DatFiles.units, Key, i) ' 给出单位i的对地武器和对空武器编号。


                        If weaponnum <> 130 Then
                            Key = "Damage Amount"
                            Dat.Data(SCDatFiles.DatFiles.weapons, Key, weaponnum) = TEMPWeaDmg(weaponnum)
                            Dat.Values(SCDatFiles.DatFiles.weapons, Key, weaponnum).IsDefault = False
                            Key = "Damage Bonus"
                            Dat.Data(SCDatFiles.DatFiles.weapons, Key, weaponnum) = TEMPWeaUmg(weaponnum)
                            Dat.Values(SCDatFiles.DatFiles.weapons, Key, weaponnum).IsDefault = False
                        End If


                        Key = "Air Weapon"
                        weaponnum = scData.DefaultDat.Data(SCDatFiles.DatFiles.units, Key, i) ' 给出单位i的对地武器和对空武器编号。

                        If weaponnum <> 130 Then
                            Key = "Damage Amount"
                            Dat.Data(SCDatFiles.DatFiles.weapons, Key, weaponnum) = TEMPWeaDmg(weaponnum)
                            Dat.Values(SCDatFiles.DatFiles.weapons, Key, weaponnum).IsDefault = False
                            Key = "Damage Bonus"
                            Dat.Data(SCDatFiles.DatFiles.weapons, Key, weaponnum) = TEMPWeaUmg(weaponnum)
                            Dat.Values(SCDatFiles.DatFiles.weapons, Key, weaponnum).IsDefault = False
                        End If
                    End If
                Next
            End If



            Dim f As Boolean = SearchCHK("STRx", binary)
            If f Then
                size = binary.ReadUInt32

                Dim BasePos As UInteger = mem.Position
                Dim strCount As UInteger = binary.ReadUInt32()
                Dim lastPos As UInteger = mem.Position

                For i = 0 To strCount - 1 ' 按大小重复
                    mem.Position = lastPos

                    Dim StartPos As UInteger = binary.ReadUInt32()
                    mem.Position = BasePos + StartPos

                    While (binary.ReadByte <> 0)
                    End While
                    Dim Bytecount As Integer = mem.Position - (BasePos + StartPos)

                    mem.Position = BasePos + StartPos

                    Dim str As String = System.Text.Encoding.UTF8.GetString(binary.ReadBytes(Bytecount - 1))

                    Strings.Add(str)

                    lastPos += 4
                Next
            Else
                SearchCHK("STR ", binary)

                size = binary.ReadUInt32



                Dim BasePos As UInteger = mem.Position
                Dim strCount As UInt16 = binary.ReadUInt16()
                Dim lastPos As UInteger = mem.Position

                For i = 0 To strCount - 1 ' 按大小重复
                    mem.Position = lastPos

                    Dim StartPos As UInteger = binary.ReadUInt16()

                    mem.Position = BasePos + StartPos

                    While (binary.ReadByte <> 0)
                    End While
                    Dim Bytecount As Integer = mem.Position - (BasePos + StartPos)

                    mem.Position = BasePos + StartPos

                    Strings.Add(System.Text.Encoding.GetEncoding(0).GetString(binary.ReadBytes(Bytecount - 1)))

                    lastPos += 2
                Next
            End If


            If True Then
                SearchCHK("SPRP", binary)
                size = binary.ReadUInt32

                _MapName = binary.ReadUInt16
                _MapDes = binary.ReadUInt16
            End If





            binary.Close()
            mem.Close()
        Else
            StormLib.SFileCloseArchive(hmpq)
        End If
    End Sub




End Class

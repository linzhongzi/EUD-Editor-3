Public Class GRPImageBox
    Private GRPBox As GRPBox

    Public Enum BoxType
        Image
        Unit
        UnitBrith
        Weapon
        Sprite
    End Enum

    Public Sub Init(ImageNum As Long, AnimHeaderIndex As Integer, Optional Flag As BoxType = BoxType.Image, Optional TFObjectID As Integer = 0)
        If GRPBox IsNot Nothing Then
            GRPBox.Delete()
        End If

        If (ImageNum >= UShort.MaxValue) Then
            ImageNum = UShort.MaxValue - 1
        End If


        ' 自行读写数据。
        GRPBox = New GRPBox(ImageNum, ImageBox, Me, AnimHeaderIndex, Flag, TFObjectID)
    End Sub
    Public Sub ChangeIScriptType(IScrptIndex As Integer)
        ' 替换正在播放的图形脚本类型。
        If GRPBox IsNot Nothing Then
            GRPBox.ChangeIScriptType(IScrptIndex)
        End If
    End Sub

    Private Sub UserControl_Unloaded(sender As Object, e As RoutedEventArgs)
        If GRPBox IsNot Nothing Then
            GRPBox.Delete()
        End If
    End Sub

    Public Sub WriteDebugText(str As String)
        'DebugText.Text = str
    End Sub
End Class

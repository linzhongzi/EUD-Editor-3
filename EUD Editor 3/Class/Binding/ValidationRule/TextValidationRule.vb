Imports System.Globalization

Public Class TextValidationRule
    Inherits ValidationRule
    Private notAbleConst As String = "\/:*?""'<>|"
    Public Overrides Function Validate(value As Object, cultureInfo As CultureInfo) As ValidationResult
        Dim str As String = value

        If str Is Nothing Then
            ' Return New ValidationResult(False, "값을 입력하세요.")
            Return New ValidationResult(False, "值不能为空。")
        End If
        If str = "" Then
            ' Return New ValidationResult(False, "값을 입력하세요.")
            Return New ValidationResult(False, "值不能为空。")
        End If
        If str.Length > 100 Then
            ' Return New ValidationResult(False, "20자 미만으로 입력하세요.")
            Return New ValidationResult(False, "请输入少于20字。")
        End If


        For i = 0 To str.Length - 1
            If notAbleConst.IndexOf(str(i)) >= 0 Then
                ' Return New ValidationResult(False, "\ / : * ? "" ' < > | 는 입력할 수 없습니다.")
                Return New ValidationResult(False, "\ / : * ? "" ' < > | 无法输入“ = ”。")
            End If
        Next

        Return ValidationResult.ValidResult
    End Function
End Class

Imports System.Globalization

Public Class HexValidationRule
    Inherits ValidationRule

    Public Overrides Function Validate(value As Object, cultureInfo As CultureInfo) As ValidationResult
        ' 先判断是否为数字。
        Try
            Dim number As Long = "&H" & value
            Return ValidationResult.ValidResult
        Catch ex As Exception
            Return New ValidationResult(False, "请输入十六进制数")
            'Return New ValidationResult(False, value.ToString)
        End Try


    End Function
End Class

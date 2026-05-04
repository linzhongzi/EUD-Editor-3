Imports System.Globalization

Public Class AlphaValidationRule
    Inherits ValidationRule

    Private AbleConst As String = ".!@#$%^&*()_+0123456789abcdefghijklnmopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"
    Public Overrides Function Validate(value As Object, cultureInfo As CultureInfo) As ValidationResult
        ' 先判断是否为数字。
        Dim str As String = value

        If str Is Nothing Then
            Return New ValidationResult(False, "值不能为空。")
        End If
        If str = "" Then
            Return New ValidationResult(False, "值不能为空。")
        End If
        If str.Length > 30 Then
            Return New ValidationResult(False, "请输入少于30字。")
        End If


        For i = 0 To str.Length - 1
            If AbleConst.IndexOf(str(i)) = -1 Then
                Return New ValidationResult(False, ".!@#$%^&*()_+和数字、字母请输入。")
            End If

        Next

        Return ValidationResult.ValidResult


    End Function


End Class

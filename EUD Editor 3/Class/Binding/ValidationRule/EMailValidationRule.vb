Imports System.Globalization

Public Class EMailValidationRule
    Inherits ValidationRule

    Private AbleConst As String = ".@0123456789abcdefghijklnmopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"
    Public Overrides Function Validate(value As Object, cultureInfo As CultureInfo) As ValidationResult
        ' 先判断是否为数字。
        Dim str As String = value

        If str Is Nothing Then
            Return New ValidationResult(False, "值不能为空。")
        End If
        If str = "" Then
            Return New ValidationResult(False, "值不能为空。")
        End If
        If str.Length > 40 Then
            Return New ValidationResult(False, "请输入少于40字。")
        End If

        If str.IndexOf("@") = -1 Then
            Return New ValidationResult(False, "不是正确的电子邮件格式。")
        End If
        If str.IndexOf("@") <> str.LastIndexOf("@") Then
            Return New ValidationResult(False, "不是正确的电子邮件格式。")
        End If


        If str.IndexOf(".") = -1 Then
            Return New ValidationResult(False, "不是正确的电子邮件格式。")
        End If
        If str.IndexOf(".") <> str.LastIndexOf(".") Then
            Return New ValidationResult(False, "不是正确的电子邮件格式。")
        End If


        If str.IndexOf("@") > str.IndexOf(".") Then
            Return New ValidationResult(False, "不是正确的电子邮件格式。")
        End If

        For i = 0 To str.Length - 1
            If AbleConst.IndexOf(str(i)) = -1 Then
                Return New ValidationResult(False, "请输入字母和数字。")
            End If
        Next

        Return ValidationResult.ValidResult


    End Function


End Class

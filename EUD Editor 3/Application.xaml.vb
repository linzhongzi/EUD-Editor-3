Class Application

    ' 可以在该文件中处理应用程序级别的事件，如tartup, Exit和DispatcherUnhandledException。
    ' 您可以在该文件中对其进行处理。

    Private Sub Application_DispatcherUnhandledException(ByVal sender As Object, ByVal e As System.Windows.Threading.DispatcherUnhandledExceptionEventArgs)
        'e.Handled = False
        'Return

        Dim ExceptionDialog As New ExceptionErrorDialog(e.Exception)
        ExceptionDialog.ShowDialog()
        If ExceptionDialog.IsClose Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    'Private Sub Application_Startup(ByVal sender As Object, ByVal e As StartupEventArgs)
    '    AddHandler AppDomain.CurrentDomain.UnhandledException, New UnhandledExceptionEventHandler(AddressOf CurrentDomain_UnhandledException)
    'End Sub

    'Private Sub CurrentDomain_UnhandledException(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
    '    Dim ex As Exception = TryCast(e.ExceptionObject, Exception)
    '    MessageBox.Show(ex.Message, "Uncaught Thread Exception", MessageBoxButton.OK, MessageBoxImage.[Error])
    'End Sub

End Class

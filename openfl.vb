Public Class openfl
    Property path As String
    Sub run()
        Try
            Process.Start(path)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
End Class

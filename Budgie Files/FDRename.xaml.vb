Public Class FDRename
    Public fltorename As String
    Public isDir As Boolean
    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        rn.Text = My.Computer.FileSystem.GetName(fltorename)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button1.Click
        Try
            If isDir Then
                My.Computer.FileSystem.RenameDirectory(fltorename, rn.Text)
            Else
                My.Computer.FileSystem.RenameFile(fltorename, rn.Text)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
        Close()
    End Sub
End Class

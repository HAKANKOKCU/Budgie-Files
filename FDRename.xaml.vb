Public Class FDRename
    Public fltorename As String
    Public isDir As Boolean
    Dim brushconvert As New BrushConverter
    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        rn.Text = My.Computer.FileSystem.GetName(fltorename)
        If My.Settings.DarkTheme Then
            Me.Background = brushconvert.ConvertFrom("#041014")
            rn.Background = Brushes.Black
            rn.Foreground = Brushes.White
        End If
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

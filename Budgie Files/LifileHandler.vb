Public Class LifileHandler
    Public WithEvents lifile As lifile
    Sub New(ByVal lifileobj As lifile)
        lifile = lifileobj
    End Sub

    Private Sub lifile_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles lifile.MouseDown
        lifile.Background = Brushes.Gray
    End Sub

    Private Sub lifile_MouseEnter(ByVal sender As Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles lifile.MouseEnter
        lifile.Background = Brushes.LightGray
    End Sub
    Private Sub lifile_MouseLeave(ByVal sender As Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles lifile.MouseLeave
        lifile.Background = Brushes.Transparent
    End Sub

    Private Sub lifile_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles lifile.MouseUp
        lifile.Background = Brushes.Transparent
    End Sub
End Class

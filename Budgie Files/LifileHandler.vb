Public Class LifileHandler
    Public WithEvents lifile As lifile
    Property txcolor As Brush = Brushes.Black
    Sub New(ByVal lifileobj As lifile)
        lifile = lifileobj
    End Sub

    Private Sub lifile_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles lifile.MouseDown
        lifile.Background = Brushes.Gray
        lifile.title.Foreground = txcolor
        lifile.subtitle.Foreground = txcolor
        lifile.sizeinf.Foreground = txcolor
    End Sub

    Private Sub lifile_MouseEnter(ByVal sender As Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles lifile.MouseEnter
        lifile.Background = Brushes.LightGray
        lifile.title.Foreground = Brushes.Black
        lifile.subtitle.Foreground = Brushes.Black
        lifile.sizeinf.Foreground = Brushes.Black
    End Sub
    Private Sub lifile_MouseLeave(ByVal sender As Object, ByVal e As System.Windows.Input.MouseEventArgs) Handles lifile.MouseLeave
        lifile.Background = Brushes.Transparent
        lifile.title.Foreground = txcolor
        lifile.subtitle.Foreground = txcolor
        lifile.sizeinf.Foreground = txcolor
    End Sub

    Private Sub lifile_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles lifile.MouseUp
        lifile.Background = Brushes.Transparent
        lifile.title.Foreground = txcolor
        lifile.subtitle.Foreground = txcolor
        lifile.sizeinf.Foreground = txcolor
    End Sub
End Class

Public Class NewDiag
    Public isDir As Boolean
    Public newfilename As String
    Public filecontent As String = ""
    Public crt = False
    Dim brushconvert As New BrushConverter
    Private Sub CrtBtnFl_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles CrtBtnFl.Click
        isDir = False
        newfilename = FileName.Text
        filecontent = flcnt.Text
        Me.DialogResult = True
        crt = True
        Me.Close()
    End Sub

    Private Sub CrtBtnDr_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles CrtBtnDr.Click
        isDir = True
        newfilename = DirName.Text
        crt = True
        Me.Close()
    End Sub

    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        If My.Settings.DarkTheme Then
            Me.Background = brushconvert.ConvertFrom("#041014")
            fl.Background = Brushes.Black
            dr.Background = Brushes.Black
            nmlbl.Foreground = Brushes.White
            dnmlbl.Foreground = Brushes.White
            cntlbl.Foreground = Brushes.White
            FileName.Background = Brushes.Black
            FileName.Foreground = Brushes.White
            DirName.Background = Brushes.Black
            DirName.Foreground = Brushes.White
            flcnt.Background = Brushes.Black
            flcnt.Foreground = Brushes.White
        End If
    End Sub
End Class

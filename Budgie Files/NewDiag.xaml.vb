Public Class NewDiag
    Public isDir As Boolean
    Public newfilename As String
    Public filecontent As String = ""
    Public crt = False
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
End Class

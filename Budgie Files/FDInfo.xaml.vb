Public Class FDInfo
    Property path As String
    Property isDir As Boolean
    Private Sub Window_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        NameLabel.Content = "Name : " + My.Computer.FileSystem.GetName(path)
        PathText.Text = "Path : " + path
        If isDir Then
            Dim fsize As Integer = 0
            Dim totalfiles As Integer = 0
            Dim countnew As Boolean = True
            Try
                For Each a As String In My.Computer.FileSystem.GetFiles(path, FileIO.SearchOption.SearchAllSubDirectories)
                    Dim fileinfo As New IO.FileInfo(a)
                    Try
                        If countnew Then
                            fsize += fileinfo.Length
                        End If
                    Catch ex As Exception
                        'MsgBox(ex.Message, MsgBoxStyle.Critical)
                        countnew = False
                    End Try
                    totalfiles += 1
                Next
                Size.Text = "Size In Bytes " + IIf(countnew, "", "(Not Counting Sizes After Integer Limit) ") + ":" + fsize.ToString + " (Total Files : " + totalfiles.ToString + ")"
            Catch ex As Exception
                Size.Text = "Error : " + ex.Message
            End Try
        Else
            TypeLabel.Visibility = Windows.Visibility.Visible
            Dim fileinfo As New IO.FileInfo(path)
            TypeLabel.Content = "Extension : " + fileinfo.Extension
            Size.Text = "Size In Bytes : " + fileinfo.Length.ToString
        End If
    End Sub
End Class

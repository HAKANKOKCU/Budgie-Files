Public Class FileMan
    Property isDir As Boolean = False
    Property path As String = ""
    Dim mainw As MainWindow
    Sub New(ByVal MainWin As MainWindow)
        mainw = MainWin
    End Sub
    Sub Delete()
        If MsgBox("Are you sure about that?" + vbNewLine + "THIS OPTION DOESNT RECYLE", MsgBoxStyle.YesNo, "Delete File?") = MsgBoxResult.Yes Then
            Try
                If isDir Then
                    My.Computer.FileSystem.DeleteDirectory(path, FileIO.DeleteDirectoryOption.DeleteAllContents)
                    mainw.gotolocat(mainw.currentpath)
                Else
                    My.Computer.FileSystem.DeleteFile(path)
                    mainw.gotolocat(mainw.currentpath)
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
        End If
    End Sub
    Sub Copy()
        mainw.filetocopy = path
        mainw.copyingdir = isDir
    End Sub
    Sub Info()
        Dim infwin As New FDInfo
        infwin.path = path
        infwin.isDir = isDir
        infwin.Show()
    End Sub
    Sub Rename()
        Dim rnm As New FDRename
        rnm.fltorename = path
        rnm.isDir = isDir
        rnm.ShowDialog()
        mainw.gotolocat(mainw.currentpath)
    End Sub
End Class

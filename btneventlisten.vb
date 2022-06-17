Public Class btneventlisten
    Property gotto As String = ""
    Dim mawin As MainWindow
    Sub New(ByVal mainvindow As MainWindow)
        mawin = mainvindow
    End Sub
    Sub gotoloc()
        mawin.gotolocat(gotto)
    End Sub
End Class

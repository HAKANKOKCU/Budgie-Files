Class MainWindow 
    Public currentpath As String = ""
    Dim dbmp As New BitmapImage()
    Dim imbmp As New BitmapImage()
    Public filetocopy As String
    Public copyingdir As Boolean = False
    Dim contexts As New ArrayList
    Private Sub mywin_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        For Each a As IO.DriveInfo In IO.DriveInfo.GetDrives
            Dim btn As New lifile
            Dim eventlisten As New btneventlisten(Me)
            eventlisten.gotto = a.Name
            If a.IsReady Then
                btn.title.Content = a.VolumeLabel
                btn.subtitle.Content = a.Name
            Else
                btn.title.Content = a.DriveType.ToString
                btn.subtitle.Content = a.Name
            End If
            Dim lihand As New LifileHandler(btn)
            AddHandler btn.MouseLeftButtonUp, AddressOf eventlisten.gotoloc
            StackPanel1.Children.Add(btn)
        Next
        dbmp.BeginInit()
        dbmp.UriSource = New Uri("/Budgie Files;component/folder.png", UriKind.Relative)
        dbmp.EndInit()
        imbmp.BeginInit()
        imbmp.UriSource = New Uri("/Budgie Files;component/iconimg.png", UriKind.Relative)
        imbmp.EndInit()
    End Sub
    Sub gotolocat(ByVal location As String)
        currentpath = location
        locationtb.Text = location
        For Each cont As ContextMenu In contexts
            cont.Items.Clear()
        Next
        Try
            fileexplorer.Children.Clear()
            For Each a As String In My.Computer.FileSystem.GetDirectories(location)
                Dim li As New lifile
                Dim lihandler As New LifileHandler(li)
                li.title.Content = My.Computer.FileSystem.GetName(a)
                li.subtitle.Content = a
                li.Icon.Source = dbmp
                'li.Label1.Foreground = Brushes.White
                'li.Label2.Foreground = Brushes.Wheat
                fileexplorer.Children.Add(li)
                Dim eventlisten As New btneventlisten(Me)
                eventlisten.gotto = a
                Dim context As New ContextMenu
                Dim copitem As New MenuItem
                copitem.Header = "Copy"
                context.Items.Add(copitem)
                Dim rnitem As New MenuItem
                rnitem.Header = "Rename"
                context.Items.Add(rnitem)
                Dim propitem As New MenuItem
                propitem.Header = "Directory Info"
                context.Items.Add(propitem)
                Dim delitem As New MenuItem
                delitem.Header = "Delete"
                context.Items.Add(delitem)
                li.ContextMenu = context
                contexts.Add(context)
                Dim fm As New FileMan(Me)
                fm.isDir = True
                fm.path = a
                AddHandler rnitem.Click, AddressOf fm.Rename
                AddHandler delitem.Click, AddressOf fm.Delete
                AddHandler copitem.Click, AddressOf fm.Copy
                AddHandler propitem.Click, AddressOf fm.Info
                AddHandler li.MouseLeftButtonUp, AddressOf eventlisten.gotoloc
            Next
            For Each a As String In My.Computer.FileSystem.GetFiles(location)
                Dim li As New lifile
                Dim lihandler As New LifileHandler(li)
                li.title.Content = My.Computer.FileSystem.GetName(a)
                li.subtitle.Content = a
                Try
                    Dim flinfo As New IO.FileInfo(a)
                    li.sizeinf.Content = GetSize(flinfo.Length)
                    Dim ex = flinfo.Extension.ToLower
                    If ex = ".png" Or ex = ".jpg" Or ex = ".jpeg" Or ex = ".bmp" Then
                        'Dim bmp As New BitmapImage()
                        'bmp.CacheOption = BitmapCacheOption.None
                        'bmp.BeginInit()
                        'bmp.UriSource = New Uri(a, UriKind.Absolute)
                        'bmp.EndInit()
                        li.Icon.Source = imbmp
                    End If
                    'Console.WriteLine(flinfo.Extension)
                Catch ex As Exception
                    Console.WriteLine(ex.Message)
                End Try
                Dim opf As New openfl
                opf.path = a
                Dim context As New ContextMenu
                Dim copitem As New MenuItem
                copitem.Header = "Copy"
                context.Items.Add(copitem)
                Dim rnitem As New MenuItem
                rnitem.Header = "Rename"
                context.Items.Add(rnitem)
                Dim propitem As New MenuItem
                propitem.Header = "File Info"
                context.Items.Add(propitem)
                Dim delitem As New MenuItem
                delitem.Header = "Delete"
                context.Items.Add(delitem)
                li.ContextMenu = context
                contexts.Add(context)
                Dim fm As New FileMan(Me)
                fm.path = a
                AddHandler rnitem.Click, AddressOf fm.Rename
                AddHandler copitem.Click, AddressOf fm.Copy
                AddHandler delitem.Click, AddressOf fm.Delete
                AddHandler propitem.Click, AddressOf fm.Info
                AddHandler li.MouseLeftButtonUp, AddressOf opf.run
                'li.Label1.Foreground = Brushes.White
                'li.Label2.Foreground = Brushes.Wheat
                fileexplorer.Children.Add(li)
            Next
        Catch ex As Exception
            Dim a As New Label
            a.Content = "Uhh.."
            a.FontSize = 25
            Dim b As New Label
            b.Content = ex.Message
            b.FontSize = 14
            fileexplorer.Children.Add(a)
            fileexplorer.Children.Add(b)
        End Try
    End Sub

    Private Sub Label1_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles Label1.MouseUp
        Dim crt As String = currentpath
        Dim list = crt.Split("\")
        Dim path = ""
        For aa As Integer = 0 To list.Length - 2
            If path = "" Then
                path = list(aa) + "\"
            Else
                path += list(aa) + "\"
            End If
        Next
        Console.WriteLine(path)
        gotolocat(path)
    End Sub

    Private Sub Label2_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles Label2.MouseUp
        Dim nw As New NewDiag
        Try
            Dim crt = IIf(currentpath(currentpath.Length - 1) = "\", currentpath, currentpath + "\")
            nw.ShowDialog()
            Try
                If nw.crt Then
                    If nw.isDir Then
                        My.Computer.FileSystem.CreateDirectory(crt + nw.newfilename)
                    Else
                        My.Computer.FileSystem.WriteAllText(crt + nw.newfilename, nw.filecontent, False)
                    End If
                    gotolocat(currentpath)
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
            End Try
        Catch ex As Exception
            MsgBox("Cant Create Anything In Here", MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Label3_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles Label3.MouseUp
        copyhere()
    End Sub
    Sub copyhere()
        Try
            If copyingdir Then
                My.Computer.FileSystem.CopyDirectory(filetocopy, IIf(currentpath(currentpath.Length - 1) = "\", currentpath, currentpath + "\") + My.Computer.FileSystem.GetName(filetocopy))
            Else
                My.Computer.FileSystem.WriteAllBytes(IIf(currentpath(currentpath.Length - 1) = "\", currentpath, currentpath + "\") + My.Computer.FileSystem.GetName(filetocopy), My.Computer.FileSystem.ReadAllBytes(filetocopy), False)
            End If
            gotolocat(currentpath)
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub
    Sub copydir(ByVal path As String)
        For Each a As String In My.Computer.FileSystem.GetDirectories(path)
            copydir(a)
        Next
        For Each a As String In My.Computer.FileSystem.GetFiles(path)

        Next
    End Sub

    Private Sub locationtb_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles locationtb.KeyUp
        If e.Key = Key.Enter Then
            gotolocat(locationtb.Text)
            fileexplorer.Focus()
        End If
    End Sub

    Function GetSize(ByVal bytesize As Integer) As String
        If Math.Floor(bytesize / 1024) > 1024 Then
            Return Math.Floor(Math.Floor(bytesize / 1024) / 1024).ToString + " MB"
        Else
            Return Math.Floor(bytesize / 1024).ToString + " KB"
        End If
    End Function
End Class

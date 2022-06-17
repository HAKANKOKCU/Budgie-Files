Class MainWindow 
    Public currenttabpath As String = ""
    Dim currentpaths As New Dictionary(Of Integer, String)
    Dim dbmp As New BitmapImage()
    Dim imbmp As New BitmapImage()
    Public filetocopy As String
    Public copyingdir As Boolean = False
    Dim contexts As New ArrayList
    Dim brushconvert As New BrushConverter
    Public txcolor As Brush = Brushes.Black
    Public WithEvents fileexplorer As StackPanel
    Sub newtab()
        Dim ti As New ScrollViewer
        Dim tc As New StackPanel
        ti.Content = tc
        ti.VerticalScrollBarVisibility = ScrollBarVisibility.Auto
        fileexplorer = tc
        Dim tii As New TabItem
        tii.Content = ti
        tii.Header = "New Tab"
        tabb.SelectedIndex = tabb.Items.Add(tii)
    End Sub
    Private Sub mywin_Loaded(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles MyBase.Loaded
        If My.Settings.DarkTheme Then
            Me.Background = brushconvert.ConvertFrom("#041014")
            txcolor = Brushes.White
            Label1.Foreground = Brushes.White
            Label2.Foreground = Brushes.White
            Label3.Foreground = Brushes.White
            theme.Foreground = Brushes.White
            locationtb.Foreground = Brushes.White
            locationtb.CaretBrush = Brushes.White
            newtabbtn.Foreground = Brushes.White
            theme.Content = IIf(My.Settings.DarkTheme, "Light Theme", "Dark Theme")
        End If
        For Each a As IO.DriveInfo In IO.DriveInfo.GetDrives
            Dim btn As New lifile
            Dim eventlisten As New btneventlisten(Me)
            eventlisten.gotto = a.Name
            If a.IsReady Then
                btn.title.Content = a.VolumeLabel
                btn.subtitle.Content = a.Name
                btn.szf.Width = ((a.TotalSize - a.AvailableFreeSpace) / a.TotalSize) * 230
                btn.sizeinf.Content = BytesMS(a.TotalSize - a.AvailableFreeSpace) + "/" + BytesMS(a.TotalSize)
            Else
                btn.title.Content = a.DriveType.ToString
                btn.subtitle.Content = a.Name
            End If
            btn.title.Foreground = txcolor
            btn.subtitle.Foreground = txcolor
            btn.sizeinf.Foreground = txcolor
            Dim lihand As New LifileHandler(btn)
            lihand.txcolor = txcolor
            AddHandler btn.MouseLeftButtonUp, AddressOf eventlisten.gotoloc
            StackPanel1.Children.Add(btn)
        Next
        dbmp.BeginInit()
        dbmp.UriSource = New Uri("/Budgie Files;component/folder.png", UriKind.Relative)
        dbmp.EndInit()
        imbmp.BeginInit()
        imbmp.UriSource = New Uri("/Budgie Files;component/iconimg.png", UriKind.Relative)
        imbmp.EndInit()
        newtab()
    End Sub
    Sub gotolocat(ByVal location As String)
        tabb.SelectedItem.header = IIf(My.Computer.FileSystem.GetName(location) = "", location, My.Computer.FileSystem.GetName(location))
        currentpaths(tabb.SelectedIndex) = location
        currenttabpath = location
        locationtb.Text = location
        'For Each cont As ContextMenu In contexts
        '   cont.Items.Clear()
        'Next
        Try
            fileexplorer.Children.Clear()
            For Each a As String In My.Computer.FileSystem.GetDirectories(location)
                Dim li As New lifile
                Dim lihandler As New LifileHandler(li)
                lihandler.txcolor = txcolor
                li.title.Content = My.Computer.FileSystem.GetName(a)
                li.subtitle.Content = a
                li.Icon.Source = dbmp
                'li.Label1.Foreground = Brushes.White
                'li.Label2.Foreground = Brushes.Wheat
                fileexplorer.Children.Add(li)
                Dim eventlisten As New btneventlisten(Me)
                eventlisten.gotto = a
                Dim context As New ContextMenu
                Dim nmitem As New MenuItem
                nmitem.Header = My.Computer.FileSystem.GetName(a)
                nmitem.IsEnabled = False
                context.Items.Add(nmitem)
                Dim ntitem As New MenuItem
                ntitem.Header = "Open In New Tab"
                context.Items.Add(ntitem)
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
                AddHandler ntitem.Click, AddressOf fm.openInNewTab
                li.title.Foreground = txcolor
                li.subtitle.Foreground = txcolor
                li.sizeinf.Foreground = txcolor
            Next
            For Each a As String In My.Computer.FileSystem.GetFiles(location)
                Dim li As New lifile
                Dim lihandler As New LifileHandler(li)
                lihandler.txcolor = txcolor
                li.title.Content = My.Computer.FileSystem.GetName(a)
                li.subtitle.Content = a
                Try
                    Dim flinfo As New IO.FileInfo(a)
                    li.sizeinf.Content = BytesMS(flinfo.Length)
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
                Dim nmitem As New MenuItem
                nmitem.Header = My.Computer.FileSystem.GetName(a)
                nmitem.IsEnabled = False
                context.Items.Add(nmitem)
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
                li.title.Foreground = txcolor
                li.subtitle.Foreground = txcolor
                li.sizeinf.Foreground = txcolor
            Next
        Catch ex As Exception
            Dim a As New Label
            a.Content = "Uhh.."
            a.FontSize = 25
            Dim b As New Label
            b.Content = ex.Message
            a.Foreground = txcolor
            b.Foreground = txcolor
            b.FontSize = 14
            fileexplorer.Children.Add(a)
            fileexplorer.Children.Add(b)
        End Try
    End Sub

    Private Sub Label1_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles Label1.MouseUp
        Dim crt As String = currentpaths(tabb.SelectedIndex)
        If crt(crt.Length - 1) = "\" Then
            crt = crt.Remove(crt.Length - 1, 1)
        End If
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
            Dim crt = IIf(currentpaths(tabb.SelectedIndex)(currentpaths(tabb.SelectedIndex).Length - 1) = "\", currentpaths(tabb.SelectedIndex), currentpaths(tabb.SelectedIndex) + "\")
            nw.ShowDialog()
            Try
                If nw.crt Then
                    If nw.isDir Then
                        My.Computer.FileSystem.CreateDirectory(crt + nw.newfilename)
                    Else
                        My.Computer.FileSystem.WriteAllText(crt + nw.newfilename, nw.filecontent, False)
                    End If
                    gotolocat(currentpaths(tabb.SelectedIndex))
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
                My.Computer.FileSystem.CopyDirectory(filetocopy, IIf(currentpaths(tabb.SelectedIndex)(currentpaths(tabb.SelectedIndex).Length - 1) = "\", currentpaths(tabb.SelectedIndex), currentpaths(tabb.SelectedIndex) + "\") + My.Computer.FileSystem.GetName(filetocopy))
            Else
                My.Computer.FileSystem.WriteAllBytes(IIf(currentpaths(tabb.SelectedIndex)(currentpaths(tabb.SelectedIndex).Length - 1) = "\", currentpaths(tabb.SelectedIndex), currentpaths(tabb.SelectedIndex) + "\") + My.Computer.FileSystem.GetName(filetocopy), My.Computer.FileSystem.ReadAllBytes(filetocopy), False)
            End If
            gotolocat(currentpaths(tabb.SelectedIndex))
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub locationtb_KeyUp(ByVal sender As System.Object, ByVal e As System.Windows.Input.KeyEventArgs) Handles locationtb.KeyUp
        If e.Key = Key.Enter Then
            gotolocat(locationtb.Text)
            fileexplorer.Focus()
        End If
    End Sub


    Private Sub theme_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles theme.MouseUp
        My.Settings.DarkTheme = Not My.Settings.DarkTheme
        theme.Content = IIf(My.Settings.DarkTheme, "Light Theme", "Dark Theme")
        My.Settings.Save()
        MsgBox("Restart App To See Changes", MsgBoxStyle.Information)
    End Sub

    Private Sub tabb_SelectionChanged(ByVal sender As System.Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles tabb.SelectionChanged
        fileexplorer = tabb.SelectedItem.content.content
        Try
            locationtb.Text = currentpaths(tabb.SelectedIndex)
            currenttabpath = currentpaths(tabb.SelectedIndex)
        Catch
        End Try
    End Sub

    Private Sub newtabbtn_MouseUp(ByVal sender As System.Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles newtabbtn.MouseUp
        newtab()
    End Sub
    Public Function BytesMS(ByVal Filesize As Object) As String
        Dim Size As Object
        Select Case VarType(Filesize)
            Case vbByte, vbInteger, vbLong, vbCurrency, vbSingle, vbDouble, vbDecimal
                Filesize = CDec(Filesize)
                If Filesize >= 1024 Then
                    For Each Size In {" kB", " MB", " GB", " TB", " PB", " EB", " ZB", " YB"}
                        Select Case Filesize
                            Case Is < 10240
                                Return Format$(Filesize / 1024, "0.00") & Size
                                Exit For
                            Case Is < 102400
                                Return Format$(Filesize / 1024, "0.0") & Size
                                Exit For
                            Case Is < 1024000
                                Return Format$(Filesize / 1024, "0") & Size
                                Exit For
                            Case Else
                                Filesize = Filesize / 1024
                        End Select
                    Next
                Else
                    Return Filesize & " bytes"
                End If
        End Select
    End Function
End Class

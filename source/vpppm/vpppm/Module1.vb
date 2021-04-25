Imports System.Web.Script.Serialization
Imports System.Net
Imports System.IO
Imports System.Xml

Module Module1
    Public Class vpmpkg
        Public pkgname As String = ""
        Public pkgdev As String = ""
        Public pkgver As String = ""

        Sub New(_pkgname, _pkgdev, _pkgver)
            pkgname = _pkgname
            pkgdev = _pkgdev
            pkgver = _pkgver
        End Sub
    End Class

    Dim args As List(Of String)
    Dim offline = False
    Dim tempfpath = ""
    Dim extractpath = ""

    Dim sc
    Dim pkglist As New Dictionary(Of String, vpmpkg)

    Sub Main()
        Console.Title = "V++ Package Manager"
        args = My.Application.CommandLineArgs.ToList()
        If args.Count < 1 Then
            End
        End If
        Console.WriteLine("V++ Package Manager " + My.Application.Info.Version.ToString())
        Console.WriteLine("Made by VMGP Official")
        Console.WriteLine()
        If args(0) = "--install" Then
            install()
        ElseIf args(0) = "-i" Then
            install()
        ElseIf args(0) = "--uninstall" Then
            uninstall()
        ElseIf args(0) = "-u" Then
            uninstall()
        ElseIf args(0) = "--offlineuninstall" Then
            offline = True
            uninstall()
        ElseIf args(0) = "-ou" Then
            offline = True
            uninstall()
        ElseIf args(0) = "--reinstall" Then
            uninstall()
            install()
        ElseIf args(0) = "--reconfig" Then
            reconfig()
        ElseIf args(0) = "-r" Then
            reconfig()
        ElseIf args(0) = "--help" Then
            help()
        Else
            Console.WriteLine("Invalid syntax.")
        End If
    End Sub

    Sub reconfig()
        Try
            Console.WriteLine("Configuring V++...")
            Threading.Thread.Sleep(5000)

            Dim cfgfile = ""
            cfgfile += "{" + vbNewLine
            cfgfile += Chr(9) + """vppi_allow_gui"": true," + vbNewLine
            cfgfile += Chr(9) + """vppi_exec_delay"": 100," + vbNewLine
            cfgfile += Chr(9) + """vppi_allow_interprocess_communication"": true" + vbNewLine
            cfgfile += "}"

            File.WriteAllText(getappdir() + Path.DirectorySeparatorChar + "config.json", cfgfile, Text.Encoding.Unicode)
        Catch ex As Exception
            MsgBox("Failed to configure V++. Please try again later." + vbNewLine + vbNewLine + "Debug info:" + vbNewLine + "Error code: " + ex.HResult.ToString + vbNewLine + "Error message: " + ex.Message + vbNewLine + "Error stack trace:" + ex.StackTrace, MsgBoxStyle.Critical, "V++ Package Manager")
        End Try
    End Sub

    Sub initproj()
        Threading.Thread.Sleep(3000)
        Console.Clear()
        Dim choice = 0
        Dim consolerk As ConsoleKeyInfo
        While True
            'Drawing
            If choice > 2 Then
                choice = 0
            ElseIf choice < 0 Then
                choice = 2
            End If

            Console.Clear()

            Console.WriteLine("V++ Package Manager " + My.Application.Info.Version.ToString())
            Console.WriteLine("Made by VMGP Official")
            Console.WriteLine()

            Console.WriteLine("Choose the type of project that you want to create. You can navigate with the arrow keys on your keyboard and aftr you selected something press the enter key.")
            Console.WriteLine()

            If choice = 0 Then
                Console.WriteLine(">  Hello world project")
            Else
                Console.WriteLine("   Hello world project")
            End If

            If choice = 1 Then
                Console.WriteLine(">  Empty project")
            Else
                Console.WriteLine("   Empty project")
            End If

            If choice = 2 Then
                Console.WriteLine(">  Exit")
            Else
                Console.WriteLine("   Exit")
            End If

            consolerk = Console.ReadKey(True)
            If consolerk.Key = ConsoleKey.UpArrow Then
                choice -= 1
            ElseIf consolerk.Key = ConsoleKey.DownArrow Then
                choice += 1
            ElseIf consolerk.Key = ConsoleKey.Enter Then
                Exit While
            End If
        End While
        If choice = 0 Then

        ElseIf choice = 1 Then

        ElseIf choice = 2 Then
            End
        End If
    End Sub

    Sub install()
        If args.Count < 2 Then
            Console.WriteLine("Failed to start operation.")
            End
        End If

        Console.WriteLine("Reading local package information database...")
        rgpkgjson()

        Dim pkgver = pkgexistst(args(1))
        Console.WriteLine("Looking for " + args(1) + "...")

        If pkgver = Nothing Then
            Console.WriteLine("Could not find " + args(1) + ".")
            End
        End If

        Console.WriteLine("The package " + args(1) + " (" + pkgver + ") was found.")

        If pkglist.ContainsKey(args(1)) Then
            Console.WriteLine("Removing current version from database...")
            pkglist.Remove(args(1))
        End If

        If offline = False Then
            If Directory.Exists(fixpath(getappdir() + "\libs\" + args(1))) Then
                Console.WriteLine(args(1) + " (" + pkgver + ") is already installed.")
                End
            End If
        End If

        Console.WriteLine("Downloading " + args(1) + " (" + pkgver + ") ...")

        downloadpkg(args(1))

        Console.WriteLine("Installing " + args(1) + " (" + pkgver + ") ...")

        extractpath = fixpath(getappdir() + "\libs\" + args(1))

        unpack()

        If Directory.Exists(getappdir() + "\libs\" + args(1)) = False Then
            Console.WriteLine("Failed to install " + args(1) + " (" + pkgver + "). Please try again later!")
            End
        End If

        Console.WriteLine("Writing to local package information database...")
        pkglist.Add(args(1), New vpmpkg(args(1), "", pkgver))
        wgpkgjson()

        Console.WriteLine(args(1) + "was installed succesfully.")
    End Sub

    Sub uninstall()
        If args.Count < 2 Then
            Console.WriteLine("Failed to start operation.")
            End
        End If

        Console.WriteLine("Reading local package information database...")
        rgpkgjson()

        If offline = False Then
            Console.WriteLine("Looking for " + args(1) + "...")
            If pkgexistst(args(1)) = Nothing Then
                Console.WriteLine("Could not find " + args(1) + ".")
                End
            End If
        Else
            Console.WriteLine("Offline uninstallation of " + args(1) + " was requested.")
        End If

        If Directory.Exists(fixpath(getappdir() + "\libs\" + args(1))) = False Then
            Console.WriteLine(args(1) + " is not installed.")
            End
        End If

        Console.WriteLine("Uninstalling " + args(1) + "...")

        For Each i In Directory.GetFiles(fixpath(getappdir() + "\libs\" + args(1)))
            File.Delete(i)
        Next

        pkglist.Remove(args(1))

        Directory.Delete(fixpath(getappdir() + "\libs\" + args(1)))

        Console.WriteLine("Writing to local package information database...")
        wgpkgjson()

        Console.WriteLine(args(1) + " was uninstalled succesfully.")
    End Sub

    Sub help()
        Console.WriteLine("vpppm --install [packagename]: Install a package")
        Console.WriteLine("vpppm --uninstall [packagename]: Uninstall a package")
        Console.WriteLine("vpppm --offlineuninstall [packagename]: Uninstall a package (offline mode)")
        Console.WriteLine("vpppm --reinstall [packagename]: Reinstall a package")
        Console.WriteLine("vpppm --help: Shows all commands.")
    End Sub

    Function webreq(url As String)
        Dim wc As New WebClient

        Return wc.DownloadString(url)
    End Function

    Function pkgexistst(lookafterv As String)
        Try
            Dim rawresp = webreq("https://vpp-lang.github.io/website/ls/libdata.json")
            Dim jss As New JavaScriptSerializer()
            Dim dict As Dictionary(Of String, Dictionary(Of String, String)) = jss.Deserialize(Of Dictionary(Of String, Dictionary(Of String, String)))(rawresp)

            If dict.ContainsKey(lookafterv) Then
                Return dict(lookafterv)("ver")
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Console.WriteLine("")
        End Try
    End Function

    Sub downloadpkg(namedw As String)
        Dim rawresp = webreq("https://vpp-lang.github.io/website/ls/libdata.json")
        Dim jss As New JavaScriptSerializer()
        Dim dict As Dictionary(Of String, Dictionary(Of String, String)) = jss.Deserialize(Of Dictionary(Of String, Dictionary(Of String, String)))(rawresp)

        If Directory.Exists(fixpath(getappdir() + "\tmp")) = False Then
            Directory.CreateDirectory(fixpath(getappdir() + "\tmp"))
        End If



        If dict.ContainsKey(namedw) Then
            writetofile(fixpath(getappdir() + "\tmp\" + namedw + ".zip"), webreq(dict(namedw)("github-zip-link")), Text.Encoding.UTF8)
            tempfpath = fixpath(getappdir() + "\tmp\" + namedw + ".zip")
        Else

        End If
    End Sub

    Function getappdir()
        Dim vppappdir = Directory.GetParent(Directory.GetParent(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData).ToString).ToString + "\vppi"
        If Directory.Exists(vppappdir) = False Then
            Directory.CreateDirectory(vppappdir)
        End If
        Return vppappdir
    End Function

    Sub writetofile(filepath As String, fdata As String, encodingt As Text.Encoding)
        Dim file = My.Computer.FileSystem.OpenTextFileWriter(filepath, False, encodingt)
        file.Write(fdata)
        file.Close()
    End Sub

    Sub unpack()
        Try
            Dim _7zrproc As New Process()
            _7zrproc.StartInfo.UseShellExecute = False
            _7zrproc.StartInfo.FileName = fixpath(My.Application.Info.DirectoryPath + "\7zr.exe")
            _7zrproc.StartInfo.Arguments = "x " + tempfpath + " -o" + extractpath
            _7zrproc.StartInfo.CreateNoWindow = True
            _7zrproc.Start()
            _7zrproc.WaitForExit()
        Catch ex As Exception
            Console.WriteLine("Failed to install " + args(1) + ". Please try again later!")
        End Try
    End Sub

    Function fixpath(ffpath As String)
        Return ffpath.Replace("\", Path.DirectorySeparatorChar)
    End Function

    Sub wgpkgjson()
        Dim tempdata As String = ""
        Dim tempip As Integer = 0

        If pkglist.Count < 1 Then
            Exit Sub
        End If

        For Each i As KeyValuePair(Of String, vpmpkg) In pkglist
            If tempdata = "" Then
                tempdata += "{" + vbNewLine
            End If

            tempdata += Chr(9) + """" + i.Key + """: {" + vbNewLine
            tempdata += Chr(9) + Chr(9) + """name"": """ + i.Key + """," + vbNewLine
            tempdata += Chr(9) + Chr(9) + """ver"": """ + i.Value.pkgver + """" + vbNewLine

            If tempip = pkglist.Count - 1 Then
                tempdata += Chr(9) + "}" + vbNewLine
            Else
                tempdata += Chr(9) + "}," + vbNewLine
            End If

            tempdata += "}"

            tempip += 1
        Next
        writetofile(getappdir() + "\packages.json", tempdata, Text.Encoding.UTF8)
    End Sub

    Sub rgpkgjson()
        If File.Exists(getappdir() + "\packages.json") = False Then
            writetofile(getappdir() + "\packages.json", "{}", Text.Encoding.UTF8)
        End If
        Dim rawresp = File.ReadAllText(getappdir() + "\packages.json")
        Dim jss As New JavaScriptSerializer()
        Dim dict As Dictionary(Of String, Dictionary(Of String, String)) = jss.Deserialize(Of Dictionary(Of String, Dictionary(Of String, String)))(rawresp)

        If dict Is Nothing Then
            Exit Sub
        End If

        If dict.Count > 0 Then
            For Each i As KeyValuePair(Of String, Dictionary(Of String, String)) In dict
                pkglist.Add(i.Key, New vpmpkg(i.Value("name"), "", i.Value("ver")))
            Next
        End If
    End Sub

End Module

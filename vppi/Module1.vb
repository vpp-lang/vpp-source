Imports System.IO
Imports System.Reflection
Imports System.Web.Script.Serialization

Module Module1

    Dim interpreters As New List(Of VppInterpreter)
    Dim prerelease = False
    Dim versubfix = ""

    Sub Main()
        If prerelease Then
            Console.Title = "[Prelease] V++ Interpreter"
        Else
            Console.Title = "V++ Interpreter"
        End If
        mainfunc(My.Application.CommandLineArgs.ToList(), My.Application.CommandLineArgs.Count)
    End Sub

    Sub mainfunc(args As List(Of String), argc As Integer)
        If argc = 1 Then
            If File.Exists(args(0)) Then
                newinterpreter(args(0))
            ElseIf args(0) = "-v" Then
                Console.WriteLine("V++ Intepreter (vppi) v" + My.Application.Info.Version.ToString + versubfix)
                Console.WriteLine("Made by VMGP Official (2016-2021)")
            ElseIf args(0) = "--version" Then
                Console.WriteLine("V++ Intepreter (vppi) v" + My.Application.Info.Version.ToString + versubfix)
                Console.WriteLine("Made by VMGP Official (2016-2021)")
            ElseIf args(0) = "-?" Then
                Console.WriteLine("V++ Intepreter (vppi) v" + My.Application.Info.Version.ToString + versubfix)
                Console.WriteLine()
                Console.WriteLine("Uses:")
                Console.WriteLine("vppi -?  vppi --help : Help.")
                Console.WriteLine("vppi -v  vppi --version : Show vppi version.")
                Console.WriteLine("vppi [FILE] : Start intepreting a script.")
            ElseIf args(0) = "--help" Then
                Console.WriteLine("V++ Intepreter (vppi) v" + My.Application.Info.Version.ToString + versubfix)
                Console.WriteLine()
                Console.WriteLine("Uses:")
                Console.WriteLine("vppi -?  vppi --help : Help.")
                Console.WriteLine("vppi -v  vppi --version : Show vppi version.")
                Console.WriteLine("vppi [FILE] : Start intepreting a script.")
            End If
        End If
    End Sub

    Sub newinterpreter(fpath As String)
        interpreters.Add(New VppInterpreter(fpath, False, getconfig()))
    End Sub

    Function getappdir()
        Return Directory.GetParent(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData).ToString
    End Function

    Function getapplogsdir()
        If Directory.Exists(getappdir() + Path.DirectorySeparatorChar + "logs") Then

        Else
            Directory.CreateDirectory(getappdir() + Path.DirectorySeparatorChar + "logs")
        End If
        Return getappdir() + Path.DirectorySeparatorChar + "logs"
    End Function

    Function getapplibsdir()
        If Directory.Exists(getappdir() + Path.DirectorySeparatorChar + "libs") Then

        Else
            Directory.CreateDirectory(getappdir() + Path.DirectorySeparatorChar + "libs")
        End If
        Return getappdir() + Path.DirectorySeparatorChar + "libs"
    End Function

    Function getappcomdir()
        If Directory.Exists(getappdir() + Path.DirectorySeparatorChar + "com") Then

        Else
            Directory.CreateDirectory(getappdir() + Path.DirectorySeparatorChar + "com")
        End If
        Return getappdir() + Path.DirectorySeparatorChar + "com"
    End Function

    Function getconfig(Optional fpath As String = Nothing) As Dictionary(Of String, Object)
        Try
            If fpath = Nothing Then
                Dim rawresp = File.ReadAllText(getappdir() + "/config.json")
                Dim jss As New JavaScriptSerializer()
                Dim dict As Dictionary(Of String, Object) = jss.Deserialize(Of Dictionary(Of String, Object))(rawresp)
                Return dict
            Else
                Return Nothing
            End If
        Catch ex As Exception
            MsgBox("Failed to load script. [s_0002]", MsgBoxStyle.Critical, "V++ Interpreter")
            End
        End Try
    End Function
End Module

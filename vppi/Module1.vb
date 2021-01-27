Imports System.IO
Imports System.Reflection

Module Module1

    Dim interpreters As New List(Of VppInterpreter)
    Dim prerelease = True
    Dim versubfix = "-pre2"

    Sub Main()
        Console.Title = "[Prelease] " + System.Reflection.Assembly.GetExecutingAssembly.Location
        mainfunc(My.Application.CommandLineArgs.ToList(), My.Application.CommandLineArgs.Count)
    End Sub

    Sub mainfunc(args As List(Of String), argc As Integer)
        If argc = 1 Then
            If File.Exists(args(0)) Then
                newinterpreter(args(0))
            ElseIf args(0) = "-v" Then
                Console.WriteLine("V++ Intepreter (vppi) v" + My.Application.Info.Version.ToString)
                Console.WriteLine("Made by VMGP Official (2016-2021)")
            ElseIf args(0) = "--version" Then
                Console.WriteLine("V++ Intepreter (vppi) v" + My.Application.Info.Version.ToString)
                Console.WriteLine("Made by VMGP Official (2016-2021)")
            ElseIf args(0) = "-?" Then
                Console.WriteLine("V++ Intepreter (vppi) v" + My.Application.Info.Version.ToString)
                Console.WriteLine()
                Console.WriteLine("Uses:")
                Console.WriteLine("vppi -?  vppi --help : Help.")
                Console.WriteLine("vppi -v  vppi --version : Show vppi version.")
                Console.WriteLine("vppi [FILE] : Start intepreting a script.")
            ElseIf args(0) = "--help" Then
                Console.WriteLine("V++ Intepreter (vppi) v" + My.Application.Info.Version.ToString)
                Console.WriteLine()
                Console.WriteLine("Uses:")
                Console.WriteLine("vppi -?  vppi --help : Help.")
                Console.WriteLine("vppi -v  vppi --version : Show vppi version.")
                Console.WriteLine("vppi [FILE] : Start intepreting a script.")
            End If
        End If
    End Sub

    Sub newinterpreter(fpath As String)
        interpreters.Add(New VppInterpreter(fpath))
    End Sub

    Function getappdir()
        Return Directory.GetParent(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData).ToString
    End Function

    Function getapplogsdir()
        If Directory.Exists(getappdir() + "\logs") Then

        Else
            Directory.CreateDirectory(getappdir() + "\logs")
        End If
        Return getappdir() + "\logs"
    End Function

    Function getapplibsdir()
        If Directory.Exists(getappdir() + "\libs") Then

        Else
            Directory.CreateDirectory(getappdir() + "\libs")
        End If
        Return getappdir() + "\libs"
    End Function
End Module

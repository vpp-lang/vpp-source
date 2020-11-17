Imports System.IO

Module Module1

    Dim interpreters As New List(Of VppInterpreter)

    Sub Main()
        mainfunc(My.Application.CommandLineArgs.ToList(), My.Application.CommandLineArgs.Count)
    End Sub

    Sub mainfunc(args As List(Of String), argc As Integer)
        If argc = 1 Then
            If File.Exists(args(0)) Then
                newinterpreter(args(0))
            End If
        End If
    End Sub

    Sub newinterpreter(fpath As String)
        interpreters.Add(New VppInterpreter(fpath))
    End Sub

    Function getappdir()
        Return Directory.GetParent(Directory.GetParent(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData).ToString).ToString
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

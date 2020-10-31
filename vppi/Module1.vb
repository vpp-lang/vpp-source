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

End Module

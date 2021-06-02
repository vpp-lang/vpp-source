Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Web

Public Class HTTPServer
    Private Const bufferSize As Integer = 1024 * 512
    '512KB
    Private ReadOnly http As HttpListener

    Public Sub New(port As String)
        Try
            http = New HttpListener()
            http.Prefixes.Add("http://localhost:" + port + "/")
            http.Start()
            http.BeginGetContext(AddressOf requestWait, Nothing)
        Catch ex As Exception

        End Try
    End Sub

    Sub requestWait()

    End Sub
End Class

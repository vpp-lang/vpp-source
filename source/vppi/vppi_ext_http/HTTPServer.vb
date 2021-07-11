Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Web

Public Class HTTPServer
    Private Const bufferSize As Integer = 1024 * 512
    '512KB
    Private ReadOnly http As HttpListener

    Public out_reqev As Boolean = False
    Public out_req_url As String = ""
    Public out_req_ip As String = ""

    Public in_resp_body As String = ""
    Public in_resp_code As Integer = 200
    Public in_resp_type As String = ""
    Public in_didresp As Boolean = False

    Public Sub New(port As String)
        Try
            http = New HttpListener()
            http.Prefixes.Add("http://localhost:" + port + "/")
            http.Start()
            http.BeginGetContext(AddressOf requestWait, Nothing)
        Catch ex As Exception

        End Try
    End Sub

    Sub requestWait(ByVal ar As IAsyncResult)
        If Not http.IsListening Then
            Return
        End If
        Dim c = http.EndGetContext(ar)
        Dim responsestr = "hey"
        Dim responsecode = 400
        http.BeginGetContext(AddressOf requestWait, Nothing)

        out_reqev = True
        out_req_url = c.Request.RawUrl

        While in_didresp = False

        End While

        in_didresp = False

        responsestr = in_resp_body
        responsecode = in_resp_code

        c.Response.Headers("Server") = "vppi_ext_http HTTPServer: V++ " + My.Application.Info.Version.ToString

        c.Response.StatusCode = responsecode
        c.Response.ContentType = in_resp_type
        c.Response.OutputStream.Write(System.Text.Encoding.Unicode.GetBytes(responsestr), 0, System.Text.Encoding.Unicode.GetBytes(responsestr).Length)
        c.Response.Close()
        Exit Sub
    End Sub
End Class

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
    Public in_resptype As Integer = 0

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
        Dim TimerStart As DateTime
        TimerStart = Now
        Dim TimeSpent As System.TimeSpan
        Dim c = http.EndGetContext(ar)
        Dim responsestr = ""
        Dim responsecode = 400
        Dim respbyte As Byte()
        http.BeginGetContext(AddressOf requestWait, Nothing)
        If Not http.IsListening Then
            Return
        End If

        c.Response.Headers.Add("Access-Control-Allow-Origin", "*")
        c.Response.Headers("Server") = "vppi_ext_http HTTPServer: V++ " + My.Application.Info.Version.ToString

        Try
            out_reqev = True
            out_req_url = c.Request.RawUrl

            While in_didresp = False
                TimeSpent = Now.Subtract(TimerStart)
                If TimeSpent.TotalSeconds > 15 Then
                    c.Response.StatusCode = 504
                    c.Response.ContentType = "text/html"
                    c.Response.ContentEncoding = Encoding.UTF8
                    responsestr = "Script took too long to respond to HTTP request. [V++ - vppi_ext_http.HTTPServer]"
                    c.Response.OutputStream.Write(System.Text.Encoding.UTF8.GetBytes(responsestr), 0, System.Text.Encoding.UTF8.GetBytes(responsestr).Length)
                    c.Response.Close()
                    Exit Sub
                End If
            End While

            in_didresp = False

            responsestr = in_resp_body
            responsecode = in_resp_code

            respbyte = System.Text.Encoding.UTF8.GetBytes(responsestr)
            c.Response.StatusCode = responsecode
            c.Response.SendChunked = True
            c.Response.ContentEncoding = Encoding.UTF8
            c.Response.ContentType = in_resp_type
            c.Response.OutputStream.Write(respbyte, 0, respbyte.Length)
            c.Response.Close()
            Exit Sub
        Catch ex As Exception

        End Try

    End Sub
End Class

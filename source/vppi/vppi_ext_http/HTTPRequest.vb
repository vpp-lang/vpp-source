Imports System.Net
Imports System.Web

Public Class HTTPRequest

    Public webheaders As New Dictionary(Of String, String)
    Public tmpval2

    ''' <summary>
    ''' Simple web request.
    ''' </summary>
    ''' <param name="requrl">URL Path</param>
    ''' <returns>Response.</returns>
    Function webreq(requrl As String)
        Dim wc As New WebClient

        For Each i As KeyValuePair(Of String, String) In webheaders
            wc.Headers.Add(i.Key, i.Value)
        Next

        Return wc.DownloadString(requrl)
    End Function

    ''' <summary>
    ''' Web request with POST.
    ''' </summary>
    ''' <param name="requrl">URL Path</param>
    ''' <param name="uploadstr">String for post request.</param>
    ''' <returns>Response.</returns>
    Function webreq(requrl As String, uploadstr As String, method As String)
        Dim wc As New WebClient

        tmpval2 = "null"

        For Each i As KeyValuePair(Of String, String) In webheaders
            wc.Headers.Add(i.Key, i.Value)
        Next

        Try
            tmpval2 = wc.UploadString(requrl, method, uploadstr)
        Catch ex As WebException
            tmpval2 = "null"
        End Try

        Return tmpval2
    End Function
End Class

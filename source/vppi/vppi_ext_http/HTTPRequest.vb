Imports System.IO
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

        Dim resp = ""
        resp = wc.DownloadString(requrl)

        Return resp.Replace(vbNullChar, "")
    End Function

    ''' <summary>
    ''' Web request with POST.
    ''' </summary>
    ''' <param name="requrl">URL Path</param>
    ''' <param name="uploadstr">String for post request.</param>
    ''' <returns>Response.</returns>
    Function webreq(requrl As String, uploadstr As String, method As String)
        Dim httpWebRequest = CType(WebRequest.Create(requrl), HttpWebRequest)
        httpWebRequest.ContentType = "application/json"
        httpWebRequest.Method = method

        Using streamWriter = New StreamWriter(httpWebRequest.GetRequestStream())
            streamWriter.Write(uploadstr)
        End Using

        Dim httpResponse = CType(httpWebRequest.GetResponse(), HttpWebResponse)
        Dim responseText As String

        Using streamReader = New StreamReader(httpResponse.GetResponseStream())
            responseText = streamReader.ReadToEnd()
        End Using

        Return responseText
    End Function

    ''' <summary>
    ''' Download file.
    ''' </summary>
    ''' <param name="requrl">URL Path</param>
    ''' <param name="fname">File name</param>
    Sub downloadfile(requrl As String, fname As String)
        Dim wc As New WebClient

        wc.DownloadFile(requrl, fname)
    End Sub
End Class

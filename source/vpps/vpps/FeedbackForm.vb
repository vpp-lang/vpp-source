Imports System.Net.Mail

Public Class FeedbackForm
    Dim _info_osp = ""
    Dim _info_osv = ""
    Dim _info_unm = ""
    Dim _info_udn = ""
    Dim _info_clrver = ""
    Public _info_vppver = ""

    Private Sub FeedbackForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        getudata()
        showuserdata()
    End Sub

    Sub showuserdata()
        DataLabel.Text = "Collected data: "
        DataLabel.Text += vbNewLine + "INFO_CMP_OSP: " + _info_osp
        DataLabel.Text += vbNewLine + "INFO_CMP_OSV: " + _info_osv
        DataLabel.Text += vbNewLine + "INFO_CMP_UNM: " + _info_unm
        DataLabel.Text += vbNewLine + "INFO_CMP_UDN: " + _info_udn
        DataLabel.Text += vbNewLine + "INFO_CLR_VER: " + _info_clrver
        DataLabel.Text += vbNewLine + "INFO_VPP_VER: " + _info_vppver
    End Sub

    Sub getudata()
        _info_osp = My.Computer.Info.OSPlatform
        _info_osv = My.Computer.Info.OSVersion
        _info_unm = Environment.UserName
        _info_udn = Environment.UserDomainName
        _info_clrver = Environment.Version.ToString
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

    End Sub


End Class
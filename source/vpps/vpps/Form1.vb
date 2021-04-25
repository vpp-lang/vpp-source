Imports System.ComponentModel
Imports System.IO
Imports System.Net

Public Class Form1
    Public logfile As StreamWriter
    Public prerelease = False
    Public versub = "-hf1"

    Public egnum = 0

    Private Sub LogButton_Click(sender As Object, e As EventArgs) Handles LogButton.Click
        startproc("explorer.exe", getappdir(), False)
    End Sub

    Function getappdir()
        Return Directory.GetParent(Directory.GetParent(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData).ToString).ToString + Path.DirectorySeparatorChar + "vppi"
    End Function

    Sub startproc(filepath, args, runas)
        Dim p As New Process
        p.StartInfo.FileName = filepath
        p.StartInfo.Arguments = args
        If runas = True Then
            p.StartInfo.Verb = "runas"
        End If
        p.Start()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        checkfu()
    End Sub

    Sub checkfu()
        Try
            Me.UseWaitCursor = True
            UpdateStatusLabel.Text = "Checking for updates..."
            Dim nextver = webrequest("http://vpp-lang.github.io/website/sv.txt")
            If nextver = My.Application.Info.Version.ToString() + versub Then
                UpdateStatusLabel.Text = "Latest version installed."
                Me.UseWaitCursor = False
            Else
                downloadsetup()
                UpdateStatusLabel.Text = "Preparing to install update..."
                Me.UseWaitCursor = False
                If MsgBox("An update is available. Click the ok button to install the update." + vbNewLine + vbNewLine + "Update version: " + nextver + vbNewLine + "Update notes:" + vbNewLine + webrequest("http://vpp-lang.github.io/website/sn.txt")) = MsgBoxResult.Ok Then
                    installupdate()
                End If
            End If
        Catch ex As Exception
            Me.UseWaitCursor = False
            UpdateStatusLabel.Text = "Failed to check for updates!"
            MsgBox("Failed to check for updates. " + vbNewLine + vbNewLine + "Debug info: " + vbNewLine + "Error message and error code: " + ex.Message + " [" + ex.HResult.ToString + "]" + vbNewLine + "Error source: " + ex.Source + vbNewLine, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    Function webrequest(uri)
        Dim wc As New WebClient
        Return wc.DownloadString(uri)
    End Function

    Sub downloadsetup()
        Dim wc As New WebClient
        wc.DownloadFile(New Uri(webrequest("http://vpp-lang.github.io/website/sl.txt")), fixpath(getappdir() + "\vppsetup_latest.exe"))
    End Sub

    Sub installupdate()
        If File.Exists(fixpath(getappdir() + "\vppsetup_latest.exe")) Then
            startproc(fixpath(getappdir() + "\vppsetup_latest.exe"), "-update", True)
            End
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MsgBox("V++ " + My.Application.Info.Version.ToString + versub + vbNewLine + "Made by VMGP Official (2016-2021)", MsgBoxStyle.ApplicationModal, "About V++")
    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        NM_Ver.Text = "V++ Settings " + My.Application.Info.Version.ToString
        NotifyIcon1.ContextMenuStrip = ContextMenuStrip1
        If prerelease = True Then
            Me.Text = "[Prerelease] V++ Settings"
            PrereleaseDownload.Checked = True
            PrereleaseDownload.Enabled = False
            Button1.Enabled = False
        End If
    End Sub

    Function fixpath(ffpath As String)
        Return ffpath.Replace("\", Path.DirectorySeparatorChar)
    End Function

    Private Sub RecfgBtn_Click(sender As Object, e As EventArgs) Handles RecfgBtn.Click
        Process.Start("vpppm", "-R")
    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        e.Cancel = True
        Me.Visible = False
    End Sub

    Private Sub NotifyIcon1_MouseClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseClick
        If e.Button = MouseButtons.Left Then
            Me.Visible = True
        ElseIf e.Button = MouseButtons.Middle Then
            easteregg1()
        End If
    End Sub

    Private Sub NM_Show_Click(sender As Object, e As EventArgs) Handles NM_Show.Click
        Me.Visible = True
    End Sub

    Private Sub NM_Quit_Click(sender As Object, e As EventArgs) Handles NM_Quit.Click
        End
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        checkfu()
    End Sub

    Sub easteregg1()
        If egnum = 0 Then
            MsgBox("But why would you do this?", MsgBoxStyle.Question, "WHYYYYYYYYYY?")
        ElseIf egnum = 1 Then
            MsgBox("Why?", MsgBoxStyle.Question, "WHYYYYYYYYYY?")
        ElseIf egnum = 2 Then
            MsgBox("Stop", MsgBoxStyle.Exclamation, "Please")
        ElseIf egnum = 3 Then
            MsgBox("Stop", MsgBoxStyle.Exclamation, "Please")
        ElseIf egnum = 4 Then
            MsgBox("STOP IT!", MsgBoxStyle.Critical, "STOP")
        ElseIf egnum = 5 Then
            MsgBox("If you do it once more i will restart your pc.", MsgBoxStyle.Critical, "STOP")
        ElseIf egnum = 6 Then
            MsgBox("Goodbye.", MsgBoxStyle.Information, " ")
            Process.Start("shutdown", "-r -t 0")
        End If
        egnum += 1
    End Sub

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If My.Application.CommandLineArgs(0) = "-i" Then
                Me.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub TabPage1_Click(sender As Object, e As EventArgs) Handles TabPage1.Click

    End Sub
End Class

Imports System.ComponentModel
Imports System.IO
Imports System.Net

Public Class Form1
    Public logfile As StreamWriter
    Public prerelease = False
    Public versub = "-hf2"

    Public egnum = 0
    Public pendingup = False
    Dim canreopen = False
    Dim vpps_proc() As Process

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
        If pendingup = True Then
            installupdate()
        Else
            checkfu()
        End If
    End Sub

    Sub checkfu()
        Try
            Me.UseWaitCursor = True
            UpdateStatusLabel.Text = "Checking for updates..."
            Button1.Enabled = False
            Dim nextver = webrequest("http://vpp-lang.github.io/website/sv.txt")
            If nextver = My.Application.Info.Version.ToString() + versub Then
                UpdateStatusLabel.Text = "Latest version installed. Last checked on " + DateString + " " + TimeString
                Me.UseWaitCursor = False
                Button1.Enabled = True
            Else
                pendingup = True
                UpdateStatusLabel.Text = "Downloading update..."
                downloadsetup()
                NotifyIcon1.ShowBalloonTip(1000, "V++ Settings", "An update is available." + vbNewLine + "Update version: " + nextver + vbNewLine + "Update notes: " + webrequest("http://vpp-lang.github.io/website/sn.txt"), ToolTipIcon.None)
                Me.UseWaitCursor = False
                Button1.Text = "Install update"
                Button1.Enabled = True
                UpdateStatusLabel.Text = "Click the button to install the update!"
            End If
        Catch ex As Exception
            Me.UseWaitCursor = False
            UpdateStatusLabel.Text = "Failed to check for updates!"
            'MsgBox("Failed to check for updates. " + vbNewLine + vbNewLine + "Debug info: " + vbNewLine + "Error message and error code: " + ex.Message + " [" + ex.HResult.ToString + "]" + vbNewLine + "Error source: " + ex.Source + vbNewLine, MsgBoxStyle.Critical, Me.Text)
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
        Try
            If File.Exists(fixpath(getappdir() + "\vppsetup_latest.exe")) Then
                startproc(fixpath(getappdir() + "\vppsetup_latest.exe"), "-update", True)
                End
            End If
        Catch ex As Exception
            Me.UseWaitCursor = False
            UpdateStatusLabel.Text = "Failed to check for updates!"
            MsgBox("Failed to install update. " + vbNewLine + vbNewLine + "Debug info: " + vbNewLine + "Error message and error code: " + ex.Message + " [" + ex.HResult.ToString + "]" + vbNewLine + "Error source: " + ex.Source + vbNewLine, MsgBoxStyle.Critical, Me.Text)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        MsgBox("V++ " + My.Application.Info.Version.ToString + versub + vbNewLine + "Made by VMGP Official (2016-2021)", MsgBoxStyle.ApplicationModal, "About V++")
    End Sub

    Private Sub GroupBox2_Enter(sender As Object, e As EventArgs) Handles GroupBox2.Enter

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        NM_Ver.Text = "V++ Settings " + My.Application.Info.Version.ToString + versub
        If prerelease = True Then
            Me.Text = "[Prerelease] V++ Settings"
            PrereleaseDownload.Checked = True
            PrereleaseDownload.Enabled = False
            Button1.Enabled = False
        End If
        checkfu()
        vpps_proc = Process.GetProcessesByName("vpps")
        If vpps_proc.Count > 1 Then
            File.WriteAllText(fixpath(getappdir() + "\vs_osg"), "ok")
            End
        Else
            canreopen = True
        End If
    End Sub

    Function fixpath(ffpath As String)
        Return ffpath.Replace("\", Path.DirectorySeparatorChar)
    End Function

    Private Sub RecfgBtn_Click(sender As Object, e As EventArgs) Handles RecfgBtn.Click
        Process.Start(My.Application.Info.DirectoryPath + "vpppm.exe", "-R")
    End Sub

    Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        e.Cancel = True
        Me.Visible = False
    End Sub

    Private Sub NotifyIcon1_MouseClick(sender As Object, e As MouseEventArgs) Handles NotifyIcon1.MouseClick
        If e.Button = MouseButtons.Left Then
            Me.Visible = True
            Me.Focus()
        ElseIf e.Button = MouseButtons.Middle Then
            easteregg1()
        End If
    End Sub

    Private Sub NM_Show_Click(sender As Object, e As EventArgs) Handles NM_Show.Click
        Me.Visible = True
        Me.Focus()
    End Sub

    Private Sub NM_Quit_Click(sender As Object, e As EventArgs) Handles NM_Quit.Click
        End
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If pendingup = False Then
            checkfu()
        End If
    End Sub

    Sub easteregg1()

    End Sub

    Private Sub Form1_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        Try
            If My.Application.CommandLineArgs(0) = "-i" Then
                Me.Visible = False
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub DSCLink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles DSCLink.LinkClicked
        Process.Start("https://discord.gg/rGAZTFEPXY")
    End Sub

    Private Sub GHDLink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles GHDLink.LinkClicked
        Process.Start("https://github.com/vpp-lang/vpp-source/discussions")
    End Sub

    Private Sub GHILink_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles GHILink.LinkClicked
        Process.Start("https://github.com/vpp-lang/vpp-source/issues")
    End Sub

    Private Sub GFBtn_Click(sender As Object, e As EventArgs) Handles GFBtn.Click
        'Dim fdbfrm As FeedbackForm = New FeedbackForm()
        'fdbfrm._info_vppver = My.Application.Info.Version.ToString + versub
        'fdbfrm.ShowDialog()
        MsgBox("Feauture not ready!", MsgBoxStyle.Critical, "V++")
    End Sub

    Private Sub ShowWinT_Tick(sender As Object, e As EventArgs) Handles ShowWinT.Tick
        If canreopen Then
            If File.Exists(fixpath(getappdir() + "\vs_osg")) Then
                File.Delete(fixpath(getappdir() + "\vs_osg"))
                Me.Visible = True
                Me.Focus()
            End If
        End If
    End Sub

    Private Sub NotifyIcon1_BalloonTipClicked(sender As Object, e As EventArgs) Handles NotifyIcon1.BalloonTipClicked
        Me.Visible = True
        Me.Focus()
        If pendingup Then
            TabControl1.SelectedTab = TabPage2
        End If
    End Sub
End Class

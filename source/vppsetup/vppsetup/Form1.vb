Imports System.IO
Imports Microsoft.Win32
Imports System.Reflection

Public Class Form1

    Dim cpage = 0
    Dim cargl = 0
    Dim cargs As List(Of String)
    Dim state = 0
    Dim vai = False
    Dim versub = "-hf1"
    Dim prerelease = False

    Dim pathseparator = Path.DirectorySeparatorChar

    Dim test = Directory.GetParent(Directory.GetParent(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData).ToString).ToString + "\VPP"

    Sub checkforcompat()
        If Environment.OSVersion.Version.Major = 6 Then

        Else
            LegacyPath.CheckState = CheckState.Checked
            LegacyPath.Enabled = False
            If MsgBox("V++ is incompatible with your OS and might not work correctly. (" + Environment.OSVersion.ToString() + ")" + vbNewLine + "Would you like to continue?", MsgBoxStyle.YesNo, "Compatibility error") = MsgBoxResult.No Then
                End
            End If
        End If
        If Environment.OSVersion.Version.Minor = 0 Then
            LegacyPath.CheckState = CheckState.Checked
            LegacyPath.Enabled = False
            If MsgBox("V++ is incompatible with your OS and might not work correctly. (" + Environment.OSVersion.ToString() + ")" + vbNewLine + "Would you like to continue?", MsgBoxStyle.YesNo, "Compatibility error") = MsgBoxResult.No Then
                End
            End If
        ElseIf Environment.OSVersion.Version.Minor = 1 Then
            LegacyPath.CheckState = CheckState.Checked
            LegacyPath.Enabled = False
            If MsgBox("V++ is incompatible with your OS and might not work correctly. (" + Environment.OSVersion.ToString() + ")" + vbNewLine + "Would you like to continue?", MsgBoxStyle.YesNo, "Compatibility error") = MsgBoxResult.No Then
                End
            End If
        End If
    End Sub

    Private Sub NextBtn_Click(sender As Object, e As EventArgs) Handles NextBtn.Click
        cpage += 1
        If cpage = 0 Then
            WelcomePage.Enabled = True
            InstallationPage.Enabled = False
            ProgressPage.Enabled = False
            BackBtn.Enabled = False
            NextBtn.Enabled = True
            CancelBtn.Enabled = True
            TabControl1.SelectTab(WelcomePage)
        ElseIf cpage = 1 Then
            If vai = False Then
                WelcomePage.Enabled = False
                InstallationPage.Enabled = True
                ProgressPage.Enabled = False
                BackBtn.Enabled = True
                NextBtn.Enabled = True
                CancelBtn.Enabled = True
                TabControl1.SelectTab(InstallationPage)
            Else
                If VAICombo.SelectedIndex = 0 Then
                    cpage = 2
                    state = 1
                    If Environment.GetEnvironmentVariable("PATH").Contains(getpff()) = False Then
                        InstallationPageChk1.CheckState = CheckState.Checked
                    End If
                    BackBtn.Enabled = False
                    NextBtn.Enabled = False
                    InstallationPage.Enabled = False
                    WelcomePage.Enabled = False
                    TabControl1.SelectTab(ProgressPage)
                    installvpp()
                ElseIf VAICombo.SelectedIndex = 1 Then
                    cpage = 2
                    state = 2
                    BackBtn.Enabled = False
                    NextBtn.Enabled = False
                    InstallationPage.Enabled = False
                    WelcomePage.Enabled = False
                    TabControl1.SelectTab(ProgressPage)
                    installvpp()
                Else

                End If
            End If
        ElseIf cpage = 2 Then
            WelcomePage.Enabled = False
            InstallationPage.Enabled = False
            ProgressPage.Enabled = True
            BackBtn.Enabled = False
            NextBtn.Enabled = False
            CancelBtn.Enabled = False
            TabControl1.SelectTab(ProgressPage)
            installvpp()
        End If
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        cargs = My.Application.CommandLineArgs.ToList()
        cargl = My.Application.CommandLineArgs.Count
        Label1.Text = "V++ will be installed in: " + getpff()
        Me.Text = "V++ " + My.Application.Info.Version.ToString + versub + " installer"
        VIALabel2.Visible = False
        If prerelease = True Then
            VIALabel2.Visible = True
        End If
        checkforcompat()
        If cargl = 1 Then
            If cargs(0) = "-update" Then
                InstallationPageChk4.Checked = True
                cpage = 2
                state = 1
                If Environment.GetEnvironmentVariable("PATH").Contains(getpff()) = False Then
                    InstallationPageChk1.CheckState = CheckState.Checked
                End If
                BackBtn.Enabled = False
                NextBtn.Enabled = False
                InstallationPage.Enabled = False
                WelcomePage.Enabled = False
                TabControl1.SelectTab(ProgressPage)
                installvpp()
            ElseIf cargs(0) = "-uninstall" Then
                cpage = 2
                state = 2
                BackBtn.Enabled = False
                NextBtn.Enabled = False
                InstallationPage.Enabled = False
                WelcomePage.Enabled = False
                TabControl1.SelectTab(ProgressPage)
                installvpp()
            Else
                InstallationPageChk1.Checked = True
                InstallationPageChk2.Checked = True
                InstallationPageChk3.Checked = True
                InstallationPageChk4.Checked = True
                If My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\vpp", "DisplayName", Nothing) = Nothing Then
                    InstallationPage.Enabled = False
                    ProgressPage.Enabled = False
                    BackBtn.Enabled = False
                    VAICombo.Visible = False
                    VAILabel.Visible = False
                Else
                    InstallationPage.Enabled = False
                    ProgressPage.Enabled = False
                    BackBtn.Enabled = False
                    VAICombo.Visible = True
                    VAILabel.Visible = True
                    vai = True
                End If
            End If
        Else
            InstallationPageChk1.Checked = True
            InstallationPageChk2.Checked = True
            InstallationPageChk3.Checked = True
            InstallationPageChk4.Checked = True
            If My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\vpp", "DisplayName", Nothing) = Nothing Then
                InstallationPage.Enabled = False
                ProgressPage.Enabled = False
                BackBtn.Enabled = False
                VAICombo.Visible = False
                VAILabel.Visible = False
            Else
                InstallationPage.Enabled = False
                ProgressPage.Enabled = False
                BackBtn.Enabled = False
                VAICombo.Visible = True
                VAILabel.Visible = True
                vai = True
            End If
        End If
    End Sub

    Private Sub BackBtn_Click(sender As Object, e As EventArgs) Handles BackBtn.Click
        cpage -= 1
        If cpage = 0 Then
            WelcomePage.Enabled = True
            InstallationPage.Enabled = False
            ProgressPage.Enabled = False
            BackBtn.Enabled = False
            NextBtn.Enabled = True
            CancelBtn.Enabled = True
            TabControl1.SelectTab(WelcomePage)
        ElseIf cpage = 1 Then
            WelcomePage.Enabled = False
            InstallationPage.Enabled = True
            ProgressPage.Enabled = False
            BackBtn.Enabled = True
            NextBtn.Enabled = True
            CancelBtn.Enabled = True
            TabControl1.SelectTab(InstallationPage)
        ElseIf cpage = 2 Then
            WelcomePage.Enabled = False
            InstallationPage.Enabled = False
            ProgressPage.Enabled = True
            BackBtn.Enabled = False
            NextBtn.Enabled = False
            CancelBtn.Enabled = False
            TabControl1.SelectTab(ProgressPage)
        End If
    End Sub

    Function getpff()
        If Directory.Exists(Directory.GetParent(Directory.GetParent(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData).ToString()).ToString() + pathseparator + "VPP") = False Then
            Directory.CreateDirectory(Directory.GetParent(Directory.GetParent(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData).ToString()).ToString() + pathseparator + "VPP")
        End If
        Return Directory.GetParent(Directory.GetParent(My.Computer.FileSystem.SpecialDirectories.AllUsersApplicationData).ToString()).ToString() + pathseparator + "VPP"
    End Function

    Function getlegacypff()
        Return My.Computer.FileSystem.SpecialDirectories.ProgramFiles
    End Function

    Function getappdir()
        If File.Exists(Directory.GetParent(Directory.GetParent(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData).ToString).ToString + pathseparator + "vppi") Then
            File.Create(Directory.GetParent(Directory.GetParent(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData).ToString).ToString + pathseparator + "vppi")
        End If
        Return Directory.GetParent(Directory.GetParent(My.Computer.FileSystem.SpecialDirectories.CurrentUserApplicationData).ToString).ToString + pathseparator + "vppi"
    End Function

    Sub installvpp()
        For Each p As Process In Process.GetProcessesByName("vppi")
            p.Kill()
        Next
        For Each p As Process In Process.GetProcessesByName("vpps")
            p.Kill()
        Next
        For Each p As Process In Process.GetProcessesByName("vpppm")
            p.Kill()
        Next
        If state = 2 Then
            If My.Application.Info.DirectoryPath = getpff() Then
                If File.Exists(getappdir() + pathseparator + "tmp_vppsetup.exe") Then
                    File.Delete(getappdir() + pathseparator + "tmp_vppsetup.exe")
                End If
                File.Copy(Application.ExecutablePath, getappdir() + pathseparator + "tmp_vppsetup.exe")
                Process.Start(getappdir() + pathseparator + "tmp_vppsetup.exe", "-uninstall")
                End
            End If
            If MsgBox("Are you sure you want to uninstall V++?", MsgBoxStyle.YesNo, "V++ Setup") = MsgBoxResult.No Then
                End
            End If
            ProgressPageTitle.Text = "Uninstalling"
            Threading.Thread.Sleep(5000)
            ProgressPageText.Text = "Removing stuff from registry..."
            ProgressPagePB.Style = ProgressBarStyle.Blocks
            ProgressPagePB.Value = 5
            If Directory.Exists(getpff()) Then

                For Each i In Directory.GetFiles(getpff())
                    Try
                        File.Delete(i)
                    Catch ex As Exception

                    End Try
                Next
                Try
                    Directory.Delete(getpff())
                Catch ex2 As Exception

                End Try
            End If
            If My.Computer.Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Uninstall\vpp") IsNot Nothing Then
                My.Computer.Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Uninstall", True).DeleteSubKey("vpp")
            End If
            If My.Computer.Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Uninstall\vpp") IsNot Nothing Then
                My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True).DeleteValue("V++ Settings")
            End If
            If My.Computer.Registry.ClassesRoot.OpenSubKey(".vpp") IsNot Nothing Then
                My.Computer.Registry.ClassesRoot.DeleteSubKey(".vpp")
            End If
            If My.Computer.Registry.ClassesRoot.OpenSubKey("V++ Script\shell\open\command") IsNot Nothing Then
                My.Computer.Registry.ClassesRoot.DeleteSubKey("V++ Script\shell\open\command")
            End If

            If Environment.GetEnvironmentVariable("PATH").Contains(";" + getpff()) Then
                Environment.GetEnvironmentVariable("PATH").Replace(";" + getpff(), "")
            End If
            If Environment.GetEnvironmentVariable("PATH").Contains(getpff() + ";") Then
                Environment.GetEnvironmentVariable("PATH").Replace(getpff() + ";", "")
            End If
            MsgBox("V++ was succesfully uninstalled.", MsgBoxStyle.Information, "V++ Setup")
            End
        Else
            Try
                If Environment.GetEnvironmentVariable("PATH").Contains(getpff() + ";") Then
                    InstallationPageChk1.CheckState = CheckState.Unchecked
                End If
                If Directory.Exists(getpff()) Then
                    For Each i In Directory.GetFiles(getpff())
                        File.Delete(i)
                    Next
                    Directory.Delete(getpff())
                End If
                ProgressPageTitle.Text = "Installing"
                Threading.Thread.Sleep(5000)
                ProgressPageText.Text = "Terminating all V++ sessions..."
                ProgressPagePB.Style = ProgressBarStyle.Blocks
                ProgressPagePB.Value = 1
                'Sets registry up
                ProgressPageText.Text = "Preparing registry..."
                My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\vpp", "DisplayName", "V++ Runtime Environment", Microsoft.Win32.RegistryValueKind.String)
                ProgressPagePB.Style = ProgressBarStyle.Blocks
                ProgressPagePB.Value = 5
                My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\vpp", "Publisher", "VMGP Official", Microsoft.Win32.RegistryValueKind.String)
                ProgressPagePB.Value = 10
                My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\vpp", "InstallLocation", getpff(), Microsoft.Win32.RegistryValueKind.String)
                ProgressPagePB.Value = 15
                My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\vpp", "NoModify", 1, Microsoft.Win32.RegistryValueKind.DWord)
                ProgressPagePB.Value = 20
                My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\vpp", "DisplayVersion", My.Application.Info.Version.ToString + versub, Microsoft.Win32.RegistryValueKind.String)
                My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\vpp", "NoRepair", 1, Microsoft.Win32.RegistryValueKind.DWord)
                My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\vpp", "UninstallString", getpff() + "\vppsetup.exe -uninstall", Microsoft.Win32.RegistryValueKind.String)
                'Extracts executables
                ProgressPageText.Text = "Extracting executables..."
                '7zr
                If File.Exists(getpff() + pathseparator + "7zr.exe") Then
                    File.Delete(getpff() + pathseparator + "7zr.exe")
                End If
                File.WriteAllBytes(getpff() + pathseparator + "7zr.exe", My.Resources._7zr)

                'V++ ZIP
                If File.Exists(getappdir() + pathseparator + "vpp_inspak.zip") Then
                    File.Delete(getappdir() + pathseparator + "vpp_inspak.zip")
                End If
                File.WriteAllBytes(getappdir() + pathseparator + "vpp_inspak.zip", My.Resources.vppzip)
                unpack()

                ProgressPagePB.Value = 30
                File.Copy(Application.ExecutablePath, getpff() + pathseparator + "vppsetup.exe")
                ProgressPagePB.Value = 40
                ProgressPageText.Text = "Finishing installation..."
                'Setting more registry values
                If My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\vpp", "DisplayIcon", Nothing) = Nothing Then
                    My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\vpp", "DisplayIcon", getpff() + "\vppi.exe", Microsoft.Win32.RegistryValueKind.String)
                End If
                My.Computer.Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True).SetValue("V++ Settings", """" + getpff() + pathseparator + "vpps.exe" + """ -i")

                If InstallationPageChk4.Checked = True Then
                    My.Computer.Registry.ClassesRoot.CreateSubKey(".vpp").SetValue("", "V++ Script", Microsoft.Win32.RegistryValueKind.String)
                    My.Computer.Registry.ClassesRoot.CreateSubKey("V++ Script\shell\open\command").SetValue("", getpff() + pathseparator + "vppi.exe ""%l"" ", Microsoft.Win32.RegistryValueKind.String)
                End If

                ProgressPagePB.Value = 90
                ProgressPageText.Text = "Configuring V++..."
                Dim vpppmi As Process = Process.Start(getpff() + pathseparator + "vpppm.exe", "-r")
                vpppmi.WaitForExit()
                ProgressPagePB.Value = 92
                If InstallationPageChk1.CheckState = CheckState.Checked Then
                    Environment.SetEnvironmentVariable("PATH", Environment.GetEnvironmentVariable("PATH") + ";" + getpff(), EnvironmentVariableTarget.Machine)
                End If
                If InstallationPageChk2.CheckState = CheckState.Checked Then
                    CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) + "\V++ Settings.lnk", getpff() + "\vpps.exe")
                End If
                If InstallationPageChk3.CheckState = CheckState.Checked Then
                    CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.CommonDesktopDirectory) + "\V++.lnk", getpff() + "\vppi.exe")
                End If
                ProgressPagePB.Value = 100
                ProgressPageText.Text = ""
                MsgBox("V++ was succesfully installed!", MsgBoxStyle.Information, "V++ Setup")
                End
            Catch ex As Exception
                MsgBox("Something went wrong! " + vbNewLine + vbNewLine + "Debug info: " + vbNewLine + "Error message and error code: " + ex.Message + " [" + ex.HResult.ToString + "]" + vbNewLine + "Error source: " + ex.Source, MsgBoxStyle.Critical, Me.Text)
                If Directory.Exists(getpff()) Then
                    For Each i In Directory.GetFiles(getpff())
                        Try
                            File.Delete(i)
                        Catch ex1 As Exception

                        End Try
                    Next
                    Try
                        Directory.Delete(getpff())
                    Catch ex2 As Exception

                    End Try
                End If
                If My.Computer.Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Uninstall\vpp") IsNot Nothing Then
                    My.Computer.Registry.LocalMachine.OpenSubKey("Software\Microsoft\Windows\CurrentVersion\Uninstall", True).DeleteSubKey("vpp")
                End If
                End
            End Try
        End If
    End Sub

    Public Sub CreateShortcut(spath As String, targetp As String)
        Dim oShell As Object
        Dim oLink As Object

        oShell = CreateObject("WScript.Shell")
        oLink = oShell.CreateShortcut(spath)

        oLink.TargetPath = targetp
        oLink.WindowStyle = 1
        oLink.Save()
    End Sub

    Private Sub CancelBtn_Click(sender As Object, e As EventArgs) Handles CancelBtn.Click
        End
    End Sub

    Sub associateft()

    End Sub

    Sub unpack()
        Dim _7zrproc As New Process()
        _7zrproc.StartInfo.UseShellExecute = False
        _7zrproc.StartInfo.FileName = getpff() + pathseparator + "7zr.exe"
        _7zrproc.StartInfo.Arguments = "x """ + getappdir() + pathseparator + "vpp_inspak.zip"" -o""" + getpff() + """"
        _7zrproc.StartInfo.CreateNoWindow = True
        _7zrproc.StartInfo.RedirectStandardOutput = True
        _7zrproc.StartInfo.RedirectStandardError = True
        _7zrproc.Start()
        _7zrproc.WaitForExit()
    End Sub

    Private Sub LegacyPath_CheckedChanged(sender As Object, e As EventArgs) Handles LegacyPath.CheckedChanged
        Label1.Text = "V++ will be installed in: " + getpff()
    End Sub

    Private Sub MainPanel_Paint(sender As Object, e As PaintEventArgs) Handles MainPanel.Paint

    End Sub
End Class

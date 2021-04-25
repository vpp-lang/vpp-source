Imports System.Net
Imports System.Web.Script.Serialization
Imports System.IO

Public Class DepForm
    Dim listloaded = False
    Dim oerror = False
    Dim prerelease = False
    Dim litems As Dictionary(Of String, Dictionary(Of String, String))
    Dim showmode = 0
    Dim isoffline = False

    Private Sub DepForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        prerelease = Form1.prerelease
        ToolStripTextBox1.Enabled = False
        If prerelease = True Then
            Me.Text = "[PRERELEASE] V++ Graphical Package Manager"
        End If
        PackageName.Text = "-"
        PackageVer.Text = "-"
        InstallBtn.Enabled = False
        UninstallBtn.Enabled = False
        ReinstallBtn.Enabled = False
        RefreshListToolStripMenuItem.Enabled = False
        ShowAllPackagesToolStripMenuItem.Enabled = False
        ShowOnlyLocalPackagesToolStripMenuItem.Enabled = False
        OfflineModeToolStripMenuItem.Enabled = False
        PStatus.Text = "Attempting to fetch data..."
    End Sub

    Sub refreshlist()
        Try
            ToolStripTextBox1.Enabled = False
            ShowAllPackagesToolStripMenuItem.Enabled = False
            ShowOnlyLocalPackagesToolStripMenuItem.Enabled = False
            OfflineModeToolStripMenuItem.Enabled = False
            PackageName.Text = "-"
            PackageVer.Text = "-"
            InstallBtn.Enabled = False
            UninstallBtn.Enabled = False
            ReinstallBtn.Enabled = False
            RefreshListToolStripMenuItem.Enabled = False
            ListBox1.Items.Clear()
            listloaded = False
            oerror = False
            PStatus.Text = "Attempting to fetch data..."
            Dim wc As New WebClient
            If isoffline = False Then
                Dim rawresp = wc.DownloadString("https://vpppm.000webhostapp.com/list.json")
                Dim jss As New JavaScriptSerializer()

                Dim dict As Dictionary(Of String, Dictionary(Of String, String)) = jss.Deserialize(Of Dictionary(Of String, Dictionary(Of String, String)))(rawresp)
                For Each i In dict
                    MsgBox(i.Value.GetType().ToString())
                Next
                litems = dict
                If showmode = 0 Then
                    For Each i As KeyValuePair(Of String, Dictionary(Of String, String)) In dict
                        ListBox1.Items.Add(i.Key)
                    Next
                ElseIf showmode = 1 Then
                    For Each i In Directory.GetFiles(Form1.getappdir() + "\libs")
                        If litems.ContainsKey(Path.GetFileName(i)) Then
                            ListBox1.Items.Add(Path.GetFileName(i))
                        End If
                    Next
                End If
            Else
                If showmode = 1 Then
                    litems = New Dictionary(Of String, Dictionary(Of String, String))
                    For Each i In Directory.GetDirectories(Form1.fixpath(Form1.getappdir() + "\libs"))
                        litems.Add(Path.GetFileName(i), New Dictionary(Of String, String))
                        ListBox1.Items.Add(Path.GetFileName(i))
                    Next
                End If
            End If
            listloaded = True
            RefreshListToolStripMenuItem.Enabled = True
            If isoffline = False Then
                ShowAllPackagesToolStripMenuItem.Enabled = True
            End If
            ShowOnlyLocalPackagesToolStripMenuItem.Enabled = True
            OfflineModeToolStripMenuItem.Enabled = True
            ToolStripTextBox1.Enabled = True
            PStatus.Text = "Ready."
        Catch ex As Exception
            PackageName.Text = "-"
            PackageVer.Text = "-"
            InstallBtn.Enabled = False
            UninstallBtn.Enabled = False
            ReinstallBtn.Enabled = False
            oerror = True
            PStatus.Text = "Error."
            ListBox1.Items.Clear()
            ListBox1.Items.Add("Failed to fetch data. " + ex.Message)
            RefreshListToolStripMenuItem.Enabled = True
            If isoffline = False Then
                ShowAllPackagesToolStripMenuItem.Enabled = True
            End If
            ShowOnlyLocalPackagesToolStripMenuItem.Enabled = True
            OfflineModeToolStripMenuItem.Enabled = True
            listloaded = True
        End Try
    End Sub

    Private Sub RefreshListToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RefreshListToolStripMenuItem.Click
        If listloaded = True Then
            refreshlist()
        End If
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Timer1.Enabled = False
        refreshlist()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If oerror = False Then
            If ListBox1.SelectedItem = Nothing Then
                PackageName.Text = "-"
                PackageVer.Text = "-"
                InstallBtn.Enabled = False
                UninstallBtn.Enabled = False
                'ReinstallBtn.Enabled = False
            Else
                PackageName.Text = ListBox1.SelectedItem
                PackageVer.Text = litems(ListBox1.SelectedItem)("version")
                If Directory.Exists(Form1.fixpath(Form1.getappdir() + "\libs\" + ListBox1.SelectedItem)) Then
                    InstallBtn.Enabled = False
                    UninstallBtn.Enabled = True
                    'ReinstallBtn.Enabled = True
                Else
                    InstallBtn.Enabled = True
                    UninstallBtn.Enabled = False
                    'ReinstallBtn.Enabled = False
                End If
            End If
        Else
            PackageName.Text = "-"
            PackageVer.Text = "-"
            InstallBtn.Enabled = False
            UninstallBtn.Enabled = False
            'ReinstallBtn.Enabled = False
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles InstallBtn.LinkClicked
        Try
            Dim ivpppmproc As New Process
            ivpppmproc.StartInfo.FileName = Form1.fixpath(My.Application.Info.DirectoryPath + "\vpppm.exe")
            ivpppmproc.StartInfo.Arguments = "--install " + ListBox1.SelectedItem.ToString
            ivpppmproc.Start()
        Catch ex As Exception
            MsgBox("Failed to install package, please try again later." + vbNewLine + vbNewLine + "Debug info: " + vbNewLine + "Error message and error code: " + ex.Message + " [" + ex.HResult.ToString + "]" + vbNewLine + "Error source: " + ex.Source + vbNewLine, MsgBoxStyle.Critical, Me.Text)
            refreshlist()
        End Try
    End Sub

    Private Sub ShowAllPackagesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowAllPackagesToolStripMenuItem.Click
        If ShowAllPackagesToolStripMenuItem.Checked = False Then
            showmode = 0
            ShowAllPackagesToolStripMenuItem.Checked = True
            ShowOnlyLocalPackagesToolStripMenuItem.Checked = False
            refreshlist()
        Else
            showmode = 0
            ShowAllPackagesToolStripMenuItem.Checked = True
            ShowOnlyLocalPackagesToolStripMenuItem.Checked = False
            refreshlist()
        End If
    End Sub

    Private Sub ShowOnlyLocalPackagesToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ShowOnlyLocalPackagesToolStripMenuItem.Click
        If ShowAllPackagesToolStripMenuItem.Checked = True Then
            showmode = 1
            ShowAllPackagesToolStripMenuItem.Checked = False
            ShowOnlyLocalPackagesToolStripMenuItem.Checked = True
            refreshlist()
        Else
            showmode = 1
            ShowAllPackagesToolStripMenuItem.Checked = False
            ShowOnlyLocalPackagesToolStripMenuItem.Checked = True
            refreshlist()
        End If
    End Sub

    Private Sub OfflineModeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles OfflineModeToolStripMenuItem.Click
        If isoffline = True Then
            isoffline = False
            OfflineModeToolStripMenuItem.Checked = False
            ShowAllPackagesToolStripMenuItem.Enabled = True
            ShowAllPackagesToolStripMenuItem.Checked = True
            ShowOnlyLocalPackagesToolStripMenuItem.Checked = False
        Else
            isoffline = True
            OfflineModeToolStripMenuItem.Checked = True
            showmode = 1
            ShowAllPackagesToolStripMenuItem.Checked = False
            ShowAllPackagesToolStripMenuItem.Enabled = False
            ShowOnlyLocalPackagesToolStripMenuItem.Checked = True
        End If
        refreshlist()
    End Sub

    Private Sub UninstallBtn_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles UninstallBtn.LinkClicked
        Try
            Dim uvpppmproc As New Process
            uvpppmproc.StartInfo.FileName = Form1.fixpath(My.Application.Info.DirectoryPath + "\vpppm.exe")
            If isoffline = False Then
                uvpppmproc.StartInfo.Arguments = "--uninstall " + ListBox1.SelectedItem.ToString
            Else
                uvpppmproc.StartInfo.Arguments = "--offlineuninstall " + ListBox1.SelectedItem.ToString
            End If
            uvpppmproc.Start()
        Catch ex As Exception
            MsgBox("Failed to install package, please try again later." + vbNewLine + vbNewLine + "Debug info: " + vbNewLine + "Error message and error code: " + ex.Message + " [" + ex.HResult.ToString + "]" + vbNewLine + "Error source: " + ex.Source + vbNewLine, MsgBoxStyle.Critical, Me.Text)
            refreshlist()
        End Try
    End Sub

    Private Sub ToolStripTextBox1_TextChanged(sender As Object, e As EventArgs) Handles ToolStripTextBox1.TextChanged
        PStatus.Text = "Loading..."
        If ToolStripTextBox1.Text.Length > 0 Then
            ListBox1.Items.Clear()
            For Each i As KeyValuePair(Of String, Dictionary(Of String, String)) In litems
                If i.Key.Contains(ToolStripTextBox1.Text) Then
                    ListBox1.Items.Add(i.Key)
                End If
            Next
        Else
            refreshlist()
        End If
        PStatus.Text = "Ready."
    End Sub

    Private Sub ReinstallBtn_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles ReinstallBtn.LinkClicked

    End Sub

    Private Sub CloseToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles CloseToolStripMenuItem.Click
        Me.Close()
    End Sub
End Class
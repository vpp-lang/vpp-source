<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.RecfgBtn = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.LogButton = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.CUFCB = New System.Windows.Forms.CheckBox()
        Me.PrereleaseDownload = New System.Windows.Forms.CheckBox()
        Me.UpdateStatusLabel = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.NM_Ver = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.NM_Show = New System.Windows.Forms.ToolStripMenuItem()
        Me.NM_Quit = New System.Windows.Forms.ToolStripMenuItem()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(490, 547)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox3)
        Me.TabPage1.Controls.Add(Me.GroupBox2)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(482, 521)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.RecfgBtn)
        Me.GroupBox3.Location = New System.Drawing.Point(9, 169)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(467, 77)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Reconfigure"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(432, 26)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "You can find some settings in the config.json file in the V++ data folder. Press " &
    "the button to" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "reconfigure/reset to default settings"
        '
        'RecfgBtn
        '
        Me.RecfgBtn.Location = New System.Drawing.Point(300, 49)
        Me.RecfgBtn.Name = "RecfgBtn"
        Me.RecfgBtn.Size = New System.Drawing.Size(161, 23)
        Me.RecfgBtn.TabIndex = 0
        Me.RecfgBtn.Text = "Reconfigure"
        Me.RecfgBtn.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Button2)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Location = New System.Drawing.Point(9, 98)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(467, 64)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "V++ Info"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(300, 36)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(161, 23)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "About"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(380, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Here you can check info about V++, like the version and the author/developer."
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.LogButton)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(467, 89)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "V++ data folder"
        '
        'LogButton
        '
        Me.LogButton.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LogButton.Location = New System.Drawing.Point(300, 62)
        Me.LogButton.Name = "LogButton"
        Me.LogButton.Size = New System.Drawing.Size(161, 23)
        Me.LogButton.TabIndex = 2
        Me.LogButton.Text = "Show V++ data folder"
        Me.LogButton.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(7, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(438, 39)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "In the data folder you can find logs, libraries/dependencies and other configurat" &
    "ion files." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "When you execute a script, the interpreter creates a log, in order t" &
    "hat you can debug scripts" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "easier."
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.CUFCB)
        Me.TabPage2.Controls.Add(Me.PrereleaseDownload)
        Me.TabPage2.Controls.Add(Me.UpdateStatusLabel)
        Me.TabPage2.Controls.Add(Me.PictureBox1)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Controls.Add(Me.Button1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(482, 521)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Update"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'CUFCB
        '
        Me.CUFCB.AutoSize = True
        Me.CUFCB.Location = New System.Drawing.Point(12, 58)
        Me.CUFCB.Name = "CUFCB"
        Me.CUFCB.Size = New System.Drawing.Size(81, 17)
        Me.CUFCB.TabIndex = 5
        Me.CUFCB.Text = "CheckBox1"
        Me.CUFCB.UseVisualStyleBackColor = True
        Me.CUFCB.Visible = False
        '
        'PrereleaseDownload
        '
        Me.PrereleaseDownload.AutoSize = True
        Me.PrereleaseDownload.Enabled = False
        Me.PrereleaseDownload.Location = New System.Drawing.Point(12, 81)
        Me.PrereleaseDownload.Name = "PrereleaseDownload"
        Me.PrereleaseDownload.Size = New System.Drawing.Size(221, 17)
        Me.PrereleaseDownload.TabIndex = 4
        Me.PrereleaseDownload.Text = "Allow downloading of Prerelease versions"
        Me.PrereleaseDownload.UseVisualStyleBackColor = True
        Me.PrereleaseDownload.Visible = False
        '
        'UpdateStatusLabel
        '
        Me.UpdateStatusLabel.AutoSize = True
        Me.UpdateStatusLabel.Location = New System.Drawing.Point(4, 471)
        Me.UpdateStatusLabel.Name = "UpdateStatusLabel"
        Me.UpdateStatusLabel.Size = New System.Drawing.Size(10, 13)
        Me.UpdateStatusLabel.TabIndex = 3
        Me.UpdateStatusLabel.Text = "-"
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.vpps.My.Resources.Resources.vppicon1
        Me.PictureBox1.Location = New System.Drawing.Point(394, 7)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(80, 80)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Label2.Location = New System.Drawing.Point(9, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(364, 48)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "This tab is used for downloading, installing and checking" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "for updates. New updat" &
    "es can bring lots of new changes and" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "fix bugs." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(0, 490)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(482, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Check for updates"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "V++ Settings"
        Me.NotifyIcon1.Visible = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NM_Ver, Me.ToolStripSeparator1, Me.NM_Show, Me.NM_Quit})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(149, 76)
        '
        'NM_Ver
        '
        Me.NM_Ver.Enabled = False
        Me.NM_Ver.Name = "NM_Ver"
        Me.NM_Ver.Size = New System.Drawing.Size(148, 22)
        Me.NM_Ver.Text = "..."
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(145, 6)
        '
        'NM_Show
        '
        Me.NM_Show.Name = "NM_Show"
        Me.NM_Show.Size = New System.Drawing.Size(148, 22)
        Me.NM_Show.Text = "Show window"
        '
        'NM_Quit
        '
        Me.NM_Quit.Name = "NM_Quit"
        Me.NM_Quit.Size = New System.Drawing.Size(148, 22)
        Me.NM_Quit.Text = "Quit"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 300000
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(490, 547)
        Me.Controls.Add(Me.TabControl1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "V++ Settings"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents LogButton As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents UpdateStatusLabel As Label
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents PrereleaseDownload As CheckBox
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents Label4 As Label
    Friend WithEvents RecfgBtn As Button
    Friend WithEvents NotifyIcon1 As NotifyIcon
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents NM_Ver As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents NM_Show As ToolStripMenuItem
    Friend WithEvents NM_Quit As ToolStripMenuItem
    Friend WithEvents Timer1 As Timer
    Friend WithEvents CUFCB As CheckBox
End Class

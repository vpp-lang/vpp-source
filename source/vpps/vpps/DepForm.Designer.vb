<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DepForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DepForm))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.RefreshListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ShowOnlyLocalPackagesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ShowAllPackagesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.OfflineModeToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.CloseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripTextBox1 = New System.Windows.Forms.ToolStripTextBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.PStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.ReinstallBtn = New System.Windows.Forms.LinkLabel()
        Me.UninstallBtn = New System.Windows.Forms.LinkLabel()
        Me.InstallBtn = New System.Windows.Forms.LinkLabel()
        Me.PackageVer = New System.Windows.Forms.Label()
        Me.PackageName = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.MenuStrip1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OptionsToolStripMenuItem, Me.ToolStripTextBox1})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(835, 27)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefreshListToolStripMenuItem, Me.ToolStripSeparator1, Me.ShowOnlyLocalPackagesToolStripMenuItem, Me.ShowAllPackagesToolStripMenuItem, Me.ToolStripSeparator2, Me.OfflineModeToolStripMenuItem, Me.ToolStripSeparator3, Me.CloseToolStripMenuItem})
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(61, 23)
        Me.OptionsToolStripMenuItem.Text = "Options"
        '
        'RefreshListToolStripMenuItem
        '
        Me.RefreshListToolStripMenuItem.Name = "RefreshListToolStripMenuItem"
        Me.RefreshListToolStripMenuItem.Size = New System.Drawing.Size(209, 22)
        Me.RefreshListToolStripMenuItem.Text = "Refresh list"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(206, 6)
        '
        'ShowOnlyLocalPackagesToolStripMenuItem
        '
        Me.ShowOnlyLocalPackagesToolStripMenuItem.Name = "ShowOnlyLocalPackagesToolStripMenuItem"
        Me.ShowOnlyLocalPackagesToolStripMenuItem.Size = New System.Drawing.Size(209, 22)
        Me.ShowOnlyLocalPackagesToolStripMenuItem.Text = "Show only local packages"
        '
        'ShowAllPackagesToolStripMenuItem
        '
        Me.ShowAllPackagesToolStripMenuItem.Checked = True
        Me.ShowAllPackagesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked
        Me.ShowAllPackagesToolStripMenuItem.Name = "ShowAllPackagesToolStripMenuItem"
        Me.ShowAllPackagesToolStripMenuItem.Size = New System.Drawing.Size(209, 22)
        Me.ShowAllPackagesToolStripMenuItem.Text = "Show all packages"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(206, 6)
        '
        'OfflineModeToolStripMenuItem
        '
        Me.OfflineModeToolStripMenuItem.Name = "OfflineModeToolStripMenuItem"
        Me.OfflineModeToolStripMenuItem.Size = New System.Drawing.Size(209, 22)
        Me.OfflineModeToolStripMenuItem.Text = "Offline mode"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(206, 6)
        '
        'CloseToolStripMenuItem
        '
        Me.CloseToolStripMenuItem.Name = "CloseToolStripMenuItem"
        Me.CloseToolStripMenuItem.Size = New System.Drawing.Size(209, 22)
        Me.CloseToolStripMenuItem.Text = "Close"
        '
        'ToolStripTextBox1
        '
        Me.ToolStripTextBox1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripTextBox1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ToolStripTextBox1.Name = "ToolStripTextBox1"
        Me.ToolStripTextBox1.Size = New System.Drawing.Size(200, 23)
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PStatus})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 525)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(835, 22)
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'PStatus
        '
        Me.PStatus.Name = "PStatus"
        Me.PStatus.Size = New System.Drawing.Size(119, 17)
        Me.PStatus.Text = "ToolStripStatusLabel1"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Cursor = System.Windows.Forms.Cursors.Default
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 27)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.ListBox1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.BackColor = System.Drawing.Color.White
        Me.SplitContainer1.Panel2.Controls.Add(Me.ReinstallBtn)
        Me.SplitContainer1.Panel2.Controls.Add(Me.UninstallBtn)
        Me.SplitContainer1.Panel2.Controls.Add(Me.InstallBtn)
        Me.SplitContainer1.Panel2.Controls.Add(Me.PackageVer)
        Me.SplitContainer1.Panel2.Controls.Add(Me.PackageName)
        Me.SplitContainer1.Size = New System.Drawing.Size(835, 498)
        Me.SplitContainer1.SplitterDistance = 573
        Me.SplitContainer1.TabIndex = 2
        '
        'ListBox1
        '
        Me.ListBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Location = New System.Drawing.Point(0, 0)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(573, 498)
        Me.ListBox1.TabIndex = 0
        '
        'ReinstallBtn
        '
        Me.ReinstallBtn.AutoSize = True
        Me.ReinstallBtn.Location = New System.Drawing.Point(2, 98)
        Me.ReinstallBtn.Name = "ReinstallBtn"
        Me.ReinstallBtn.Size = New System.Drawing.Size(47, 13)
        Me.ReinstallBtn.TabIndex = 4
        Me.ReinstallBtn.TabStop = True
        Me.ReinstallBtn.Text = "Reinstall"
        Me.ReinstallBtn.VisitedLinkColor = System.Drawing.Color.Blue
        '
        'UninstallBtn
        '
        Me.UninstallBtn.AutoSize = True
        Me.UninstallBtn.Location = New System.Drawing.Point(2, 85)
        Me.UninstallBtn.Name = "UninstallBtn"
        Me.UninstallBtn.Size = New System.Drawing.Size(47, 13)
        Me.UninstallBtn.TabIndex = 3
        Me.UninstallBtn.TabStop = True
        Me.UninstallBtn.Text = "Uninstall"
        Me.UninstallBtn.VisitedLinkColor = System.Drawing.Color.Blue
        '
        'InstallBtn
        '
        Me.InstallBtn.AutoSize = True
        Me.InstallBtn.Location = New System.Drawing.Point(2, 72)
        Me.InstallBtn.Name = "InstallBtn"
        Me.InstallBtn.Size = New System.Drawing.Size(34, 13)
        Me.InstallBtn.TabIndex = 2
        Me.InstallBtn.TabStop = True
        Me.InstallBtn.Text = "Install"
        Me.InstallBtn.VisitedLinkColor = System.Drawing.Color.Blue
        '
        'PackageVer
        '
        Me.PackageVer.AutoSize = True
        Me.PackageVer.Location = New System.Drawing.Point(3, 38)
        Me.PackageVer.Name = "PackageVer"
        Me.PackageVer.Size = New System.Drawing.Size(10, 13)
        Me.PackageVer.TabIndex = 1
        Me.PackageVer.Text = "-"
        '
        'PackageName
        '
        Me.PackageName.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PackageName.Location = New System.Drawing.Point(2, 11)
        Me.PackageName.Name = "PackageName"
        Me.PackageName.Size = New System.Drawing.Size(253, 23)
        Me.PackageName.TabIndex = 0
        Me.PackageName.Text = "-"
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        Me.Timer1.Interval = 5000
        '
        'DepForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(835, 547)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "DepForm"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DepForm"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents OptionsToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents RefreshListToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Timer1 As Timer
    Friend WithEvents PStatus As ToolStripStatusLabel
    Friend WithEvents PackageName As Label
    Friend WithEvents PackageVer As Label
    Friend WithEvents InstallBtn As LinkLabel
    Friend WithEvents ReinstallBtn As LinkLabel
    Friend WithEvents UninstallBtn As LinkLabel
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ShowOnlyLocalPackagesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ShowAllPackagesToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As ToolStripSeparator
    Friend WithEvents OfflineModeToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents ToolStripTextBox1 As ToolStripTextBox
    Friend WithEvents ToolStripSeparator3 As ToolStripSeparator
    Friend WithEvents CloseToolStripMenuItem As ToolStripMenuItem
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FeedbackForm
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.EmailTBox = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.NNTBox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.MessageBox = New System.Windows.Forms.RichTextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.DataLabel = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 104)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Email address"
        '
        'EmailTBox
        '
        Me.EmailTBox.Location = New System.Drawing.Point(93, 101)
        Me.EmailTBox.Name = "EmailTBox"
        Me.EmailTBox.Size = New System.Drawing.Size(299, 20)
        Me.EmailTBox.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 77)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Nickname"
        '
        'NNTBox
        '
        Me.NNTBox.Location = New System.Drawing.Point(93, 74)
        Me.NNTBox.Name = "NNTBox"
        Me.NNTBox.Size = New System.Drawing.Size(299, 20)
        Me.NNTBox.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 36.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(8, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(395, 55)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Give us feedback"
        '
        'MessageBox
        '
        Me.MessageBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MessageBox.Location = New System.Drawing.Point(3, 16)
        Me.MessageBox.Name = "MessageBox"
        Me.MessageBox.Size = New System.Drawing.Size(275, 306)
        Me.MessageBox.TabIndex = 5
        Me.MessageBox.Text = ""
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.MessageBox)
        Me.GroupBox1.Location = New System.Drawing.Point(414, 74)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(281, 325)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Message"
        '
        'DataLabel
        '
        Me.DataLabel.AutoSize = True
        Me.DataLabel.Location = New System.Drawing.Point(15, 144)
        Me.DataLabel.Name = "DataLabel"
        Me.DataLabel.Size = New System.Drawing.Size(70, 13)
        Me.DataLabel.TabIndex = 7
        Me.DataLabel.Text = "Please wait..."
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(582, 402)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(113, 23)
        Me.Button1.TabIndex = 8
        Me.Button1.Text = "Send feedback"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'FeedbackForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(707, 434)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DataLabel)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.NNTBox)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.EmailTBox)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FeedbackForm"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Feedback"
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents EmailTBox As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents NNTBox As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents MessageBox As RichTextBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents DataLabel As Label
    Friend WithEvents Button1 As Button
End Class

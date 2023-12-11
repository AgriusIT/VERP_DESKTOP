<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReGenerateCGV
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar()
        Me.ToolStripStatusLabel1 = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "From Date:"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(93, 34)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(200, 26)
        Me.dtpFromDate.TabIndex = 1
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(93, 60)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(200, 26)
        Me.dtpToDate.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "To Date:"
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(218, 86)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(75, 23)
        Me.btnUpdate.TabIndex = 4
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripProgressBar1, Me.ToolStripStatusLabel1})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 145)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(307, 22)
        Me.StatusStrip1.TabIndex = 5
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        Me.ToolStripProgressBar1.Size = New System.Drawing.Size(200, 24)
        Me.ToolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ToolStripProgressBar1.Visible = False
        '
        'ToolStripStatusLabel1
        '
        Me.ToolStripStatusLabel1.Name = "ToolStripStatusLabel1"
        Me.ToolStripStatusLabel1.Size = New System.Drawing.Size(92, 25)
        Me.ToolStripStatusLabel1.Text = "Proccess..."
        Me.ToolStripStatusLabel1.Visible = False
        '
        'frmReGenerateCGV
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(307, 167)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.dtpToDate)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dtpFromDate)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmReGenerateCGV"
        Me.Text = "Re-Generate CGS Vouchers"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStripProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents ToolStripStatusLabel1 As System.Windows.Forms.ToolStripStatusLabel
End Class

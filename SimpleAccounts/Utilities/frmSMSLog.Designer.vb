<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSMSLog
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
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.lblProcess = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.cmbLog = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnDelete, Me.lblProcess, Me.ToolStripSeparator1, Me.btnSave})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(816, 32)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnDelete
        '
        Me.btnDelete.Image = Global.SimpleAccounts.My.Resources.Resources.BtnDelete_Image
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(90, 29)
        Me.btnDelete.Text = "&Delete"
        '
        'lblProcess
        '
        Me.lblProcess.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.lblProcess.Name = "lblProcess"
        Me.lblProcess.Size = New System.Drawing.Size(0, 29)
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 32)
        '
        'btnSave
        '
        Me.btnSave.Image = Global.SimpleAccounts.My.Resources.Resources.BtnSave_Image
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(98, 29)
        Me.btnSave.Text = "&Update"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.grd)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 72)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(792, 322)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "SMS Log"
        '
        'grd
        '
        Me.grd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.Location = New System.Drawing.Point(3, 22)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(786, 297)
        Me.grd.TabIndex = 0
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'cmbLog
        '
        Me.cmbLog.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLog.FormattingEnabled = True
        Me.cmbLog.Items.AddRange(New Object() {"Drafts", "Outbox", "Sentbox", "All"})
        Me.cmbLog.Location = New System.Drawing.Point(12, 45)
        Me.cmbLog.Name = "cmbLog"
        Me.cmbLog.Size = New System.Drawing.Size(219, 28)
        Me.cmbLog.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 20)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Log Type"
        '
        'frmSMSLog
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(816, 421)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbLog)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSMSLog"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Log-SMS"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents cmbLog As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblProcess As System.Windows.Forms.ToolStripLabel
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmployeeAutoOverTime
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmployeeAutoOverTime))
        Me.grdAutoOT = New Janus.Windows.GridEX.GridEX()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.dtpMonth = New System.Windows.Forms.DateTimePicker()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnImportAttendance = New System.Windows.Forms.ToolStripButton()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.cmbDepartment = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        CType(Me.grdAutoOT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdAutoOT
        '
        Me.grdAutoOT.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdAutoOT.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdAutoOT.CardWidth = 804
        Me.grdAutoOT.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdAutoOT.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdAutoOT.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdAutoOT.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.grdAutoOT.GroupByBoxVisible = False
        Me.grdAutoOT.Location = New System.Drawing.Point(-2, 115)
        Me.grdAutoOT.Name = "grdAutoOT"
        Me.grdAutoOT.RecordNavigator = True
        Me.grdAutoOT.Size = New System.Drawing.Size(617, 338)
        Me.grdAutoOT.TabIndex = 17
        Me.grdAutoOT.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdAutoOT.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdAutoOT.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(15, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(178, 23)
        Me.lblHeader.TabIndex = 18
        Me.lblHeader.Text = "Auto Over Time"
        '
        'dtpMonth
        '
        Me.dtpMonth.CustomFormat = "MMMM yyyy"
        Me.dtpMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpMonth.Location = New System.Drawing.Point(16, 88)
        Me.dtpMonth.Name = "dtpMonth"
        Me.dtpMonth.ShowUpDown = True
        Me.dtpMonth.Size = New System.Drawing.Size(173, 20)
        Me.dtpMonth.TabIndex = 20
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.Location = New System.Drawing.Point(16, 72)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(37, 13)
        Me.lblFrom.TabIndex = 21
        Me.lblFrom.Text = "Month"
        '
        'btnLoad
        '
        Me.btnLoad.Location = New System.Drawing.Point(369, 85)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(67, 23)
        Me.btnLoad.TabIndex = 23
        Me.btnLoad.Text = "Load"
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh, Me.btnSave, Me.btnImportAttendance})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(615, 25)
        Me.ToolStrip1.TabIndex = 24
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(66, 22)
        Me.btnRefresh.Text = "Refresh"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(51, 22)
        Me.btnSave.Text = "&Save"
        '
        'btnImportAttendance
        '
        Me.btnImportAttendance.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnImportAttendance.Image = CType(resources.GetObject("btnImportAttendance.Image"), System.Drawing.Image)
        Me.btnImportAttendance.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnImportAttendance.Name = "btnImportAttendance"
        Me.btnImportAttendance.Size = New System.Drawing.Size(111, 22)
        Me.btnImportAttendance.Text = "Import Attendance"
        '
        'lblProgress
        '
        Me.lblProgress.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(176, 204)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 43
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'cmbDepartment
        '
        Me.cmbDepartment.FormattingEnabled = True
        Me.cmbDepartment.Location = New System.Drawing.Point(195, 88)
        Me.cmbDepartment.Name = "cmbDepartment"
        Me.cmbDepartment.Size = New System.Drawing.Size(168, 21)
        Me.cmbDepartment.TabIndex = 44
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(192, 72)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "Department"
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Nothing
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(577, 0)
        Me.CtrlGrdBar1.MyGrid = Me.grdAutoOT
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(38, 25)
        Me.CtrlGrdBar1.TabIndex = 45
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.pnlHeader.Location = New System.Drawing.Point(0, 25)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(615, 43)
        Me.pnlHeader.TabIndex = 46
        '
        'frmEmployeeAutoOverTime
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(615, 453)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.cmbDepartment)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblFrom)
        Me.Controls.Add(Me.dtpMonth)
        Me.Controls.Add(Me.grdAutoOT)
        Me.Name = "frmEmployeeAutoOverTime"
        Me.Text = "Employee Auto Over Time"
        CType(Me.grdAutoOT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdAutoOT As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents dtpMonth As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnImportAttendance As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents cmbDepartment As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

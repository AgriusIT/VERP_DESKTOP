<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStockAudit
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
        Dim grdStockAudit_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmStockAudit))
        Me.lblDate = New System.Windows.Forms.Label()
        Me.dtpStockAudit = New System.Windows.Forms.DateTimePicker()
        Me.lblStockAuditName = New System.Windows.Forms.Label()
        Me.txtStockAuditName = New System.Windows.Forms.TextBox()
        Me.txtSessionName = New System.Windows.Forms.TextBox()
        Me.lblSessionName = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.lblRemarks = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.grdStockAudit = New Janus.Windows.GridEX.GridEX()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.lstLocations = New SimpleAccounts.uiListControl()
        Me.cbStatus = New System.Windows.Forms.CheckBox()
        Me.pnlHeader.SuspendLayout()
        CType(Me.grdStockAudit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.Location = New System.Drawing.Point(7, 68)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(42, 21)
        Me.lblDate.TabIndex = 1
        Me.lblDate.Text = "Date"
        '
        'dtpStockAudit
        '
        Me.dtpStockAudit.CalendarFont = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpStockAudit.CustomFormat = "dd/MMM/yyyy"
        Me.dtpStockAudit.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpStockAudit.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStockAudit.Location = New System.Drawing.Point(11, 92)
        Me.dtpStockAudit.Name = "dtpStockAudit"
        Me.dtpStockAudit.Size = New System.Drawing.Size(244, 29)
        Me.dtpStockAudit.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.dtpStockAudit, "Please select a date")
        '
        'lblStockAuditName
        '
        Me.lblStockAuditName.AutoSize = True
        Me.lblStockAuditName.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStockAuditName.Location = New System.Drawing.Point(257, 68)
        Me.lblStockAuditName.Name = "lblStockAuditName"
        Me.lblStockAuditName.Size = New System.Drawing.Size(135, 21)
        Me.lblStockAuditName.TabIndex = 3
        Me.lblStockAuditName.Text = "Stock Audit Name"
        '
        'txtStockAuditName
        '
        Me.txtStockAuditName.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStockAuditName.Location = New System.Drawing.Point(261, 92)
        Me.txtStockAuditName.Name = "txtStockAuditName"
        Me.txtStockAuditName.Size = New System.Drawing.Size(244, 29)
        Me.txtStockAuditName.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.txtStockAuditName, "Please type stock audit name")
        '
        'txtSessionName
        '
        Me.txtSessionName.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSessionName.Location = New System.Drawing.Point(11, 152)
        Me.txtSessionName.Name = "txtSessionName"
        Me.txtSessionName.Size = New System.Drawing.Size(494, 29)
        Me.txtSessionName.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.txtSessionName, "Please type session name here")
        '
        'lblSessionName
        '
        Me.lblSessionName.AutoSize = True
        Me.lblSessionName.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSessionName.Location = New System.Drawing.Point(7, 128)
        Me.lblSessionName.Name = "lblSessionName"
        Me.lblSessionName.Size = New System.Drawing.Size(109, 21)
        Me.lblSessionName.TabIndex = 5
        Me.lblSessionName.Text = "Session Name"
        '
        'txtRemarks
        '
        Me.txtRemarks.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(11, 211)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(494, 30)
        Me.txtRemarks.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.txtRemarks, "Please type remarks here.")
        '
        'lblRemarks
        '
        Me.lblRemarks.AutoSize = True
        Me.lblRemarks.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemarks.Location = New System.Drawing.Point(7, 187)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(71, 21)
        Me.lblRemarks.TabIndex = 7
        Me.lblRemarks.Text = "Remarks"
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.CtrlGrdBar1)
        Me.pnlHeader.Controls.Add(Me.Button1)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(815, 50)
        Me.pnlHeader.TabIndex = 0
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(775, 19)
        Me.CtrlGrdBar1.MyGrid = Me.grdStockAudit
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(35, 24)
        Me.CtrlGrdBar1.TabIndex = 21
        '
        'grdStockAudit
        '
        Me.grdStockAudit.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdStockAudit.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdStockAudit_DesignTimeLayout.LayoutString = resources.GetString("grdStockAudit_DesignTimeLayout.LayoutString")
        Me.grdStockAudit.DesignTimeLayout = grdStockAudit_DesignTimeLayout
        Me.grdStockAudit.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdStockAudit.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdStockAudit.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdStockAudit.GroupByBoxVisible = False
        Me.grdStockAudit.Location = New System.Drawing.Point(2, 270)
        Me.grdStockAudit.Name = "grdStockAudit"
        Me.grdStockAudit.RecordNavigator = True
        Me.grdStockAudit.Size = New System.Drawing.Size(812, 229)
        Me.grdStockAudit.TabIndex = 11
        Me.grdStockAudit.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdStockAudit.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdStockAudit.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackgroundImage = CType(resources.GetObject("Button1.BackgroundImage"), System.Drawing.Image)
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(741, 17)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(30, 26)
        Me.Button1.TabIndex = 9
        Me.Button1.UseVisualStyleBackColor = True
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.White
        Me.lblHeader.Location = New System.Drawing.Point(12, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(146, 32)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Stock Audit"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Panel2.Controls.Add(Me.btnCancel)
        Me.Panel2.Controls.Add(Me.btnSave)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.ForeColor = System.Drawing.Color.White
        Me.Panel2.Location = New System.Drawing.Point(0, 502)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(815, 59)
        Me.Panel2.TabIndex = 11
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(652, 19)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(72, 36)
        Me.btnCancel.TabIndex = 0
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnSave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(730, 19)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(82, 36)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'lstLocations
        '
        Me.lstLocations.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstLocations.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstLocations.BackColor = System.Drawing.Color.Transparent
        Me.lstLocations.disableWhenChecked = False
        Me.lstLocations.HeadingLabelName = "lstBelt"
        Me.lstLocations.HeadingText = "Location"
        Me.lstLocations.Location = New System.Drawing.Point(516, 79)
        Me.lstLocations.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.lstLocations.Name = "lstLocations"
        Me.lstLocations.ShowAddNewButton = False
        Me.lstLocations.ShowInverse = True
        Me.lstLocations.ShowMagnifierButton = False
        Me.lstLocations.ShowNoCheck = False
        Me.lstLocations.ShowResetAllButton = False
        Me.lstLocations.ShowSelectall = True
        Me.lstLocations.Size = New System.Drawing.Size(263, 180)
        Me.lstLocations.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.lstLocations, "Please select locations")
        Me.lstLocations.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'cbStatus
        '
        Me.cbStatus.AutoSize = True
        Me.cbStatus.Location = New System.Drawing.Point(11, 247)
        Me.cbStatus.Name = "cbStatus"
        Me.cbStatus.Size = New System.Drawing.Size(58, 17)
        Me.cbStatus.TabIndex = 9
        Me.cbStatus.Text = "Closed"
        Me.cbStatus.UseVisualStyleBackColor = True
        '
        'frmStockAudit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(815, 561)
        Me.Controls.Add(Me.cbStatus)
        Me.Controls.Add(Me.lstLocations)
        Me.Controls.Add(Me.grdStockAudit)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.txtRemarks)
        Me.Controls.Add(Me.lblRemarks)
        Me.Controls.Add(Me.txtSessionName)
        Me.Controls.Add(Me.lblSessionName)
        Me.Controls.Add(Me.txtStockAuditName)
        Me.Controls.Add(Me.lblStockAuditName)
        Me.Controls.Add(Me.dtpStockAudit)
        Me.Controls.Add(Me.lblDate)
        Me.Name = "frmStockAudit"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Stock Audit"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.grdStockAudit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents dtpStockAudit As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblStockAuditName As System.Windows.Forms.Label
    Friend WithEvents txtStockAuditName As System.Windows.Forms.TextBox
    Friend WithEvents txtSessionName As System.Windows.Forms.TextBox
    Friend WithEvents lblSessionName As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents lblRemarks As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents grdStockAudit As Janus.Windows.GridEX.GridEX
    Friend WithEvents lstLocations As SimpleAccounts.uiListControl
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents cbStatus As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAdvanceRequest
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAdvanceRequest))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.tsbTask = New System.Windows.Forms.ToolStripButton()
        Me.tsbConfig = New System.Windows.Forms.ToolStripButton()
        Me.btnApprovalHistory = New System.Windows.Forms.ToolStripButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.pnlDetail = New System.Windows.Forms.Panel()
        Me.lblLoanDetails = New System.Windows.Forms.Label()
        Me.txtLoanDetails = New System.Windows.Forms.TextBox()
        Me.PnlSalaryDeduction = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtPermonthdeduction = New System.Windows.Forms.TextBox()
        Me.cmbStartMonth = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtYear = New System.Windows.Forms.TextBox()
        Me.cmbAdvanceType = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblCostCentre = New System.Windows.Forms.Label()
        Me.cmbCostCentre = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.rbtnByName = New System.Windows.Forms.RadioButton()
        Me.cmbPolicy = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.cmbRequestStatus = New System.Windows.Forms.ComboBox()
        Me.rbtnByCode = New System.Windows.Forms.RadioButton()
        Me.cmbReason = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblLoanReason = New System.Windows.Forms.Label()
        Me.txtLoanAmount = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbEmployees = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txtReqNo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpReqDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblLoanPolicy = New System.Windows.Forms.Label()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.btnRefresReceiveable = New System.Windows.Forms.Button()
        Me.txtReceiveable = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.pnlDetail.SuspendLayout()
        Me.PnlSalaryDeduction.SuspendLayout()
        CType(Me.cmbAdvanceType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbPolicy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbReason, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbEmployees, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.btnPrint, Me.toolStripSeparator, Me.btnDelete, Me.toolStripSeparator1, Me.btnRefresh, Me.HelpToolStripButton, Me.tsbTask, Me.tsbConfig, Me.btnApprovalHistory})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(978, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(51, 22)
        Me.btnNew.Text = "&New"
        '
        'btnEdit
        '
        Me.btnEdit.Image = CType(resources.GetObject("btnEdit.Image"), System.Drawing.Image)
        Me.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(47, 22)
        Me.btnEdit.Text = "&Edit"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(51, 22)
        Me.btnSave.Text = "&Save"
        '
        'btnPrint
        '
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(52, 22)
        Me.btnPrint.Text = "&Print"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(60, 22)
        Me.btnDelete.Text = "D&elete"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(66, 22)
        Me.btnRefresh.Text = "Refresh"
        '
        'HelpToolStripButton
        '
        Me.HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HelpToolStripButton.Image = CType(resources.GetObject("HelpToolStripButton.Image"), System.Drawing.Image)
        Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton.Name = "HelpToolStripButton"
        Me.HelpToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.HelpToolStripButton.Text = "He&lp"
        '
        'tsbTask
        '
        Me.tsbTask.Image = Global.SimpleAccounts.My.Resources.Resources.Untitled_1
        Me.tsbTask.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbTask.Name = "tsbTask"
        Me.tsbTask.Size = New System.Drawing.Size(89, 22)
        Me.tsbTask.Text = "Task Assign"
        '
        'tsbConfig
        '
        Me.tsbConfig.Image = Global.SimpleAccounts.My.Resources.Resources.Advanced_Options
        Me.tsbConfig.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbConfig.Name = "tsbConfig"
        Me.tsbConfig.Size = New System.Drawing.Size(63, 22)
        Me.tsbConfig.Text = "Config"
        '
        'btnApprovalHistory
        '
        Me.btnApprovalHistory.Image = Global.SimpleAccounts.My.Resources.Resources.Copy
        Me.btnApprovalHistory.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnApprovalHistory.Name = "btnApprovalHistory"
        Me.btnApprovalHistory.Size = New System.Drawing.Size(116, 22)
        Me.btnApprovalHistory.Text = "Approval History"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.pnlDetail)
        Me.GroupBox1.Controls.Add(Me.PnlSalaryDeduction)
        Me.GroupBox1.Controls.Add(Me.cmbAdvanceType)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.lblCostCentre)
        Me.GroupBox1.Controls.Add(Me.cmbCostCentre)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.rbtnByName)
        Me.GroupBox1.Controls.Add(Me.cmbPolicy)
        Me.GroupBox1.Controls.Add(Me.cmbRequestStatus)
        Me.GroupBox1.Controls.Add(Me.rbtnByCode)
        Me.GroupBox1.Controls.Add(Me.cmbReason)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.lblLoanReason)
        Me.GroupBox1.Controls.Add(Me.txtLoanAmount)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cmbEmployees)
        Me.GroupBox1.Controls.Add(Me.txtReqNo)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.dtpReqDate)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 64)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(993, 243)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Advance Entry"
        '
        'pnlDetail
        '
        Me.pnlDetail.Controls.Add(Me.lblLoanDetails)
        Me.pnlDetail.Controls.Add(Me.txtLoanDetails)
        Me.pnlDetail.Location = New System.Drawing.Point(360, 114)
        Me.pnlDetail.Name = "pnlDetail"
        Me.pnlDetail.Size = New System.Drawing.Size(356, 124)
        Me.pnlDetail.TabIndex = 22
        '
        'lblLoanDetails
        '
        Me.lblLoanDetails.AutoSize = True
        Me.lblLoanDetails.Location = New System.Drawing.Point(6, 15)
        Me.lblLoanDetails.Name = "lblLoanDetails"
        Me.lblLoanDetails.Size = New System.Drawing.Size(99, 13)
        Me.lblLoanDetails.TabIndex = 15
        Me.lblLoanDetails.Text = "Advance Details"
        '
        'txtLoanDetails
        '
        Me.txtLoanDetails.Location = New System.Drawing.Point(117, 9)
        Me.txtLoanDetails.Multiline = True
        Me.txtLoanDetails.Name = "txtLoanDetails"
        Me.txtLoanDetails.Size = New System.Drawing.Size(234, 111)
        Me.txtLoanDetails.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.txtLoanDetails, "Enter Advance Details")
        '
        'PnlSalaryDeduction
        '
        Me.PnlSalaryDeduction.Controls.Add(Me.Label7)
        Me.PnlSalaryDeduction.Controls.Add(Me.Label5)
        Me.PnlSalaryDeduction.Controls.Add(Me.txtPermonthdeduction)
        Me.PnlSalaryDeduction.Controls.Add(Me.cmbStartMonth)
        Me.PnlSalaryDeduction.Controls.Add(Me.Label6)
        Me.PnlSalaryDeduction.Controls.Add(Me.txtYear)
        Me.PnlSalaryDeduction.Location = New System.Drawing.Point(360, 20)
        Me.PnlSalaryDeduction.Name = "PnlSalaryDeduction"
        Me.PnlSalaryDeduction.Size = New System.Drawing.Size(356, 90)
        Me.PnlSalaryDeduction.TabIndex = 14
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(3, 12)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(125, 13)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Per Month Deduction"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(2, 42)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(134, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Start Deduction Month"
        '
        'txtPermonthdeduction
        '
        Me.txtPermonthdeduction.BackColor = System.Drawing.SystemColors.Info
        Me.txtPermonthdeduction.Location = New System.Drawing.Point(142, 9)
        Me.txtPermonthdeduction.Name = "txtPermonthdeduction"
        Me.txtPermonthdeduction.Size = New System.Drawing.Size(206, 21)
        Me.txtPermonthdeduction.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtPermonthdeduction, "Per Month Deduction from Salary")
        '
        'cmbStartMonth
        '
        Me.cmbStartMonth.FormattingEnabled = True
        Me.cmbStartMonth.Items.AddRange(New Object() {"January", "Feburary", "March", "April", "May", "June", "July", "August", "September", "Octobar", "November", "Decembar"})
        Me.cmbStartMonth.Location = New System.Drawing.Point(142, 39)
        Me.cmbStartMonth.Name = "cmbStartMonth"
        Me.cmbStartMonth.Size = New System.Drawing.Size(206, 21)
        Me.cmbStartMonth.TabIndex = 3
        Me.cmbStartMonth.Text = "---Select Start Month---"
        Me.ToolTip1.SetToolTip(Me.cmbStartMonth, "Select Start Month")
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(3, 70)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(32, 13)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Year"
        '
        'txtYear
        '
        Me.txtYear.Location = New System.Drawing.Point(142, 66)
        Me.txtYear.MaxLength = 4
        Me.txtYear.Name = "txtYear"
        Me.txtYear.Size = New System.Drawing.Size(78, 21)
        Me.txtYear.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.txtYear, "Select Year")
        '
        'cmbAdvanceType
        '
        Me.cmbAdvanceType.CheckedListSettings.CheckStateMember = ""
        Me.cmbAdvanceType.DisplayLayout.MaxColScrollRegions = 1
        Me.cmbAdvanceType.DisplayLayout.MaxRowScrollRegions = 1
        Me.cmbAdvanceType.DisplayLayout.Override.CellPadding = 0
        Me.cmbAdvanceType.Location = New System.Drawing.Point(140, 80)
        Me.cmbAdvanceType.Name = "cmbAdvanceType"
        Me.cmbAdvanceType.Size = New System.Drawing.Size(206, 23)
        Me.cmbAdvanceType.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.cmbAdvanceType, "Select Advance Type")
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(9, 85)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(87, 13)
        Me.Label11.TabIndex = 4
        Me.Label11.Text = "Advance Type"
        '
        'lblCostCentre
        '
        Me.lblCostCentre.AutoSize = True
        Me.lblCostCentre.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCostCentre.Location = New System.Drawing.Point(9, 190)
        Me.lblCostCentre.Name = "lblCostCentre"
        Me.lblCostCentre.Size = New System.Drawing.Size(76, 13)
        Me.lblCostCentre.TabIndex = 12
        Me.lblCostCentre.Text = "Cost Centre"
        '
        'cmbCostCentre
        '
        Me.cmbCostCentre.Enabled = False
        Me.cmbCostCentre.FormattingEnabled = True
        Me.cmbCostCentre.Location = New System.Drawing.Point(140, 187)
        Me.cmbCostCentre.Name = "cmbCostCentre"
        Me.cmbCostCentre.Size = New System.Drawing.Size(206, 21)
        Me.cmbCostCentre.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.cmbCostCentre, "Cost Center")
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(870, 119)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(93, 13)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "Request Status"
        Me.Label9.Visible = False
        '
        'rbtnByName
        '
        Me.rbtnByName.AutoSize = True
        Me.rbtnByName.Checked = True
        Me.rbtnByName.Location = New System.Drawing.Point(219, 135)
        Me.rbtnByName.Name = "rbtnByName"
        Me.rbtnByName.Size = New System.Drawing.Size(77, 17)
        Me.rbtnByName.TabIndex = 9
        Me.rbtnByName.TabStop = True
        Me.rbtnByName.Text = "By Name"
        Me.ToolTip1.SetToolTip(Me.rbtnByName, "Search By Name")
        Me.rbtnByName.UseVisualStyleBackColor = True
        '
        'cmbPolicy
        '
        Me.cmbPolicy.CheckedListSettings.CheckStateMember = ""
        Me.cmbPolicy.DisplayLayout.MaxColScrollRegions = 1
        Me.cmbPolicy.DisplayLayout.MaxRowScrollRegions = 1
        Me.cmbPolicy.DisplayLayout.Override.CellPadding = 0
        Me.cmbPolicy.Location = New System.Drawing.Point(806, 90)
        Me.cmbPolicy.Name = "cmbPolicy"
        Me.cmbPolicy.Size = New System.Drawing.Size(157, 23)
        Me.cmbPolicy.TabIndex = 19
        Me.cmbPolicy.Visible = False
        '
        'cmbRequestStatus
        '
        Me.cmbRequestStatus.FormattingEnabled = True
        Me.cmbRequestStatus.Items.AddRange(New Object() {"Open", "Rejected", "Closed", "Approved"})
        Me.cmbRequestStatus.Location = New System.Drawing.Point(806, 62)
        Me.cmbRequestStatus.Name = "cmbRequestStatus"
        Me.cmbRequestStatus.Size = New System.Drawing.Size(157, 21)
        Me.cmbRequestStatus.TabIndex = 13
        Me.cmbRequestStatus.Visible = False
        '
        'rbtnByCode
        '
        Me.rbtnByCode.AutoSize = True
        Me.rbtnByCode.Location = New System.Drawing.Point(140, 135)
        Me.rbtnByCode.Name = "rbtnByCode"
        Me.rbtnByCode.Size = New System.Drawing.Size(74, 17)
        Me.rbtnByCode.TabIndex = 8
        Me.rbtnByCode.TabStop = True
        Me.rbtnByCode.Text = "By Code"
        Me.ToolTip1.SetToolTip(Me.rbtnByCode, "Search By Code")
        Me.rbtnByCode.UseVisualStyleBackColor = True
        '
        'cmbReason
        '
        Me.cmbReason.CheckedListSettings.CheckStateMember = ""
        Me.cmbReason.DisplayLayout.MaxColScrollRegions = 1
        Me.cmbReason.DisplayLayout.MaxRowScrollRegions = 1
        Me.cmbReason.DisplayLayout.Override.CellPadding = 0
        Me.cmbReason.Location = New System.Drawing.Point(806, 32)
        Me.cmbReason.Name = "cmbReason"
        Me.cmbReason.Size = New System.Drawing.Size(157, 23)
        Me.cmbReason.TabIndex = 21
        Me.cmbReason.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(9, 111)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(104, 13)
        Me.Label8.TabIndex = 6
        Me.Label8.Text = "Advance Amount"
        '
        'lblLoanReason
        '
        Me.lblLoanReason.AutoSize = True
        Me.lblLoanReason.Location = New System.Drawing.Point(722, 37)
        Me.lblLoanReason.Name = "lblLoanReason"
        Me.lblLoanReason.Size = New System.Drawing.Size(80, 13)
        Me.lblLoanReason.TabIndex = 20
        Me.lblLoanReason.Text = "Loan Reason"
        Me.lblLoanReason.Visible = False
        '
        'txtLoanAmount
        '
        Me.txtLoanAmount.BackColor = System.Drawing.SystemColors.Info
        Me.txtLoanAmount.Location = New System.Drawing.Point(141, 111)
        Me.txtLoanAmount.Name = "txtLoanAmount"
        Me.txtLoanAmount.Size = New System.Drawing.Size(206, 21)
        Me.txtLoanAmount.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.txtLoanAmount, "Enter Advance Amount")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 158)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Employee"
        '
        'cmbEmployees
        '
        Me.cmbEmployees.AlwaysInEditMode = True
        Me.cmbEmployees.CheckedListSettings.CheckStateMember = ""
        Me.cmbEmployees.DisplayLayout.InterBandSpacing = 10
        Me.cmbEmployees.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbEmployees.DisplayLayout.Override.RowSpacingBefore = 2
        Me.cmbEmployees.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbEmployees.LimitToList = True
        Me.cmbEmployees.Location = New System.Drawing.Point(140, 158)
        Me.cmbEmployees.MaxDropDownItems = 20
        Me.cmbEmployees.Name = "cmbEmployees"
        Me.cmbEmployees.Size = New System.Drawing.Size(206, 23)
        Me.cmbEmployees.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.cmbEmployees, "Select Employee")
        '
        'txtReqNo
        '
        Me.txtReqNo.Location = New System.Drawing.Point(140, 25)
        Me.txtReqNo.Name = "txtReqNo"
        Me.txtReqNo.ReadOnly = True
        Me.txtReqNo.Size = New System.Drawing.Size(206, 21)
        Me.txtReqNo.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtReqNo, "Request No")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 28)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(48, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Req No"
        '
        'dtpReqDate
        '
        Me.dtpReqDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpReqDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpReqDate.Location = New System.Drawing.Point(140, 52)
        Me.dtpReqDate.Name = "dtpReqDate"
        Me.dtpReqDate.Size = New System.Drawing.Size(206, 21)
        Me.dtpReqDate.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.dtpReqDate, "Request Date")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 55)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Req Date"
        '
        'lblLoanPolicy
        '
        Me.lblLoanPolicy.AutoSize = True
        Me.lblLoanPolicy.Location = New System.Drawing.Point(486, 63)
        Me.lblLoanPolicy.Name = "lblLoanPolicy"
        Me.lblLoanPolicy.Size = New System.Drawing.Size(71, 13)
        Me.lblLoanPolicy.TabIndex = 18
        Me.lblLoanPolicy.Text = "Loan Policy"
        Me.lblLoanPolicy.Visible = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(12, 8)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(307, 23)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Employee Advance Request"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.grd)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 368)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(996, 199)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "History"
        '
        'grd
        '
        Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.Location = New System.Drawing.Point(3, 17)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(990, 179)
        Me.grd.TabIndex = 0
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 27)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(266, 13)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Receiveable At the Time Of Advance Request"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.btnRefresReceiveable)
        Me.GroupBox4.Controls.Add(Me.txtReceiveable)
        Me.GroupBox4.Controls.Add(Me.Label10)
        Me.GroupBox4.Location = New System.Drawing.Point(15, 311)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(797, 53)
        Me.GroupBox4.TabIndex = 3
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Previous Receiveables"
        '
        'btnRefresReceiveable
        '
        Me.btnRefresReceiveable.Location = New System.Drawing.Point(548, 24)
        Me.btnRefresReceiveable.Name = "btnRefresReceiveable"
        Me.btnRefresReceiveable.Size = New System.Drawing.Size(68, 23)
        Me.btnRefresReceiveable.TabIndex = 2
        Me.btnRefresReceiveable.Text = "Refresh"
        Me.btnRefresReceiveable.UseVisualStyleBackColor = True
        '
        'txtReceiveable
        '
        Me.txtReceiveable.Location = New System.Drawing.Point(279, 24)
        Me.txtReceiveable.MaxLength = 4
        Me.txtReceiveable.Name = "txtReceiveable"
        Me.txtReceiveable.ReadOnly = True
        Me.txtReceiveable.Size = New System.Drawing.Size(263, 21)
        Me.txtReceiveable.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtReceiveable, "Receiveable At the Time Of Advance Request")
        '
        'pnlHeader
        '
        Me.pnlHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Location = New System.Drawing.Point(0, 25)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1020, 38)
        Me.pnlHeader.TabIndex = 76
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(979, 0)
        Me.CtrlGrdBar1.MyGrid = Me.grd
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(38, 25)
        Me.CtrlGrdBar1.TabIndex = 77
        '
        'frmAdvanceRequest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(1020, 579)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.lblLoanPolicy)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.Name = "frmAdvanceRequest"
        Me.Text = "Loan Request"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.pnlDetail.ResumeLayout(False)
        Me.pnlDetail.PerformLayout()
        Me.PnlSalaryDeduction.ResumeLayout(False)
        Me.PnlSalaryDeduction.PerformLayout()
        CType(Me.cmbAdvanceType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbPolicy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbReason, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbEmployees, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents txtReqNo As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpReqDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPermonthdeduction As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbEmployees As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbRequestStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtLoanAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbStartMonth As System.Windows.Forms.ComboBox
    Friend WithEvents lblLoanPolicy As System.Windows.Forms.Label
    Friend WithEvents lblLoanDetails As System.Windows.Forms.Label
    Friend WithEvents lblLoanReason As System.Windows.Forms.Label
    Friend WithEvents txtLoanDetails As System.Windows.Forms.TextBox
    Friend WithEvents cmbReason As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents cmbPolicy As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents rbtnByName As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnByCode As System.Windows.Forms.RadioButton
    Friend WithEvents txtYear As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents btnRefresReceiveable As System.Windows.Forms.Button
    Friend WithEvents txtReceiveable As System.Windows.Forms.TextBox
    Friend WithEvents tsbTask As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbConfig As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblCostCentre As System.Windows.Forms.Label
    Friend WithEvents cmbCostCentre As System.Windows.Forms.ComboBox
    Friend WithEvents cmbAdvanceType As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents PnlSalaryDeduction As System.Windows.Forms.Panel
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlDetail As System.Windows.Forms.Panel
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents btnApprovalHistory As System.Windows.Forms.ToolStripButton
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
End Class

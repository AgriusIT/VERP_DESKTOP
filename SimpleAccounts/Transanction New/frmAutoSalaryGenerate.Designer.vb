<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAutoSalaryGenerate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAutoSalaryGenerate))
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.grdSalary = New Janus.Windows.GridEX.GridEX()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblDailyAttendance = New System.Windows.Forms.LinkLabel()
        Me.lblCostCenter = New System.Windows.Forms.Label()
        Me.cmbCostCenter = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbDepartment = New System.Windows.Forms.ComboBox()
        Me.lblValidating = New System.Windows.Forms.Label()
        Me.pgbValidating = New System.Windows.Forms.ProgressBar()
        Me.chkAutoAdjustAbsentAttendance = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtYear = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbMonth = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpSalaryDate = New System.Windows.Forms.DateTimePicker()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdProcess = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnPost = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.btnPrintSheet = New System.Windows.Forms.ToolStripButton()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnConfiguration = New System.Windows.Forms.ToolStripButton()
        Me.btnAddSalaryType = New System.Windows.Forms.ToolStripButton()
        Me.BtnOpenAttendanceSetup = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.BtnTaxSlabs = New System.Windows.Forms.ToolStripButton()
        Me.BtnLoanDeduction = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdSalary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdProcess, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox2)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox1)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1017, 676)
        '
        'pnlHeader
        '
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1017, 38)
        Me.pnlHeader.TabIndex = 76
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(8, 8)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(300, 29)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Auto Salary Generate"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.grdSalary)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 251)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1013, 425)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Salary Sheet"
        '
        'grdSalary
        '
        Me.grdSalary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSalary.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdSalary.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdSalary.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdSalary.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSalary.GroupByBoxVisible = False
        Me.grdSalary.Location = New System.Drawing.Point(3, 20)
        Me.grdSalary.Name = "grdSalary"
        Me.grdSalary.RecordNavigator = True
        Me.grdSalary.Size = New System.Drawing.Size(1007, 402)
        Me.grdSalary.TabIndex = 0
        Me.grdSalary.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.lblDailyAttendance)
        Me.GroupBox1.Controls.Add(Me.lblCostCenter)
        Me.GroupBox1.Controls.Add(Me.cmbCostCenter)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmbDepartment)
        Me.GroupBox1.Controls.Add(Me.lblValidating)
        Me.GroupBox1.Controls.Add(Me.pgbValidating)
        Me.GroupBox1.Controls.Add(Me.chkAutoAdjustAbsentAttendance)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtYear)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cmbMonth)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtpSalaryDate)
        Me.GroupBox1.Controls.Add(Me.btnGenerate)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 45)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(892, 200)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Salary of Month"
        '
        'lblDailyAttendance
        '
        Me.lblDailyAttendance.AutoSize = True
        Me.lblDailyAttendance.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDailyAttendance.Location = New System.Drawing.Point(264, 143)
        Me.lblDailyAttendance.Name = "lblDailyAttendance"
        Me.lblDailyAttendance.Size = New System.Drawing.Size(126, 17)
        Me.lblDailyAttendance.TabIndex = 12
        Me.lblDailyAttendance.TabStop = True
        Me.lblDailyAttendance.Text = "Daily Attendance"
        '
        'lblCostCenter
        '
        Me.lblCostCenter.AutoSize = True
        Me.lblCostCenter.Location = New System.Drawing.Point(16, 33)
        Me.lblCostCenter.Name = "lblCostCenter"
        Me.lblCostCenter.Size = New System.Drawing.Size(93, 17)
        Me.lblCostCenter.TabIndex = 0
        Me.lblCostCenter.Text = "Cost Center"
        '
        'cmbCostCenter
        '
        Me.cmbCostCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCostCenter.FormattingEnabled = True
        Me.cmbCostCenter.Location = New System.Drawing.Point(97, 30)
        Me.cmbCostCenter.Name = "cmbCostCenter"
        Me.cmbCostCenter.Size = New System.Drawing.Size(164, 25)
        Me.cmbCostCenter.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 60)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(92, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Department"
        '
        'cmbDepartment
        '
        Me.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDepartment.FormattingEnabled = True
        Me.cmbDepartment.Location = New System.Drawing.Point(97, 57)
        Me.cmbDepartment.Name = "cmbDepartment"
        Me.cmbDepartment.Size = New System.Drawing.Size(164, 25)
        Me.cmbDepartment.TabIndex = 1
        '
        'lblValidating
        '
        Me.lblValidating.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblValidating.AutoSize = True
        Me.lblValidating.Location = New System.Drawing.Point(420, 176)
        Me.lblValidating.Name = "lblValidating"
        Me.lblValidating.Size = New System.Drawing.Size(291, 17)
        Me.lblValidating.TabIndex = 10
        Me.lblValidating.Text = "Please wait while record are validating... "
        Me.lblValidating.Visible = False
        '
        'pgbValidating
        '
        Me.pgbValidating.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pgbValidating.Location = New System.Drawing.Point(673, 171)
        Me.pgbValidating.Name = "pgbValidating"
        Me.pgbValidating.Size = New System.Drawing.Size(213, 23)
        Me.pgbValidating.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.pgbValidating.TabIndex = 11
        Me.pgbValidating.Visible = False
        '
        'chkAutoAdjustAbsentAttendance
        '
        Me.chkAutoAdjustAbsentAttendance.AutoSize = True
        Me.chkAutoAdjustAbsentAttendance.Location = New System.Drawing.Point(267, 115)
        Me.chkAutoAdjustAbsentAttendance.Name = "chkAutoAdjustAbsentAttendance"
        Me.chkAutoAdjustAbsentAttendance.Size = New System.Drawing.Size(256, 21)
        Me.chkAutoAdjustAbsentAttendance.TabIndex = 6
        Me.chkAutoAdjustAbsentAttendance.Text = "Auto Adjust Absent Attendance"
        Me.chkAutoAdjustAbsentAttendance.UseVisualStyleBackColor = True
        Me.chkAutoAdjustAbsentAttendance.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 141)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(38, 17)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Year"
        '
        'txtYear
        '
        Me.txtYear.Location = New System.Drawing.Point(97, 138)
        Me.txtYear.MaxLength = 4
        Me.txtYear.Name = "txtYear"
        Me.txtYear.Size = New System.Drawing.Size(70, 24)
        Me.txtYear.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 114)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 17)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Month"
        '
        'cmbMonth
        '
        Me.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMonth.FormattingEnabled = True
        Me.cmbMonth.Location = New System.Drawing.Point(97, 111)
        Me.cmbMonth.Name = "cmbMonth"
        Me.cmbMonth.Size = New System.Drawing.Size(164, 25)
        Me.cmbMonth.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 88)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 17)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Salary Date"
        '
        'dtpSalaryDate
        '
        Me.dtpSalaryDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpSalaryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpSalaryDate.Location = New System.Drawing.Point(97, 84)
        Me.dtpSalaryDate.Name = "dtpSalaryDate"
        Me.dtpSalaryDate.Size = New System.Drawing.Size(164, 24)
        Me.dtpSalaryDate.TabIndex = 3
        '
        'btnGenerate
        '
        Me.btnGenerate.Location = New System.Drawing.Point(173, 138)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(88, 23)
        Me.btnGenerate.TabIndex = 9
        Me.btnGenerate.Text = "Generate"
        Me.btnGenerate.UseVisualStyleBackColor = True
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grdProcess)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1017, 676)
        '
        'grdProcess
        '
        Me.grdProcess.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdProcess.AlternatingColors = True
        Me.grdProcess.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdProcess.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdProcess.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdProcess.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdProcess.Location = New System.Drawing.Point(0, 0)
        Me.grdProcess.Name = "grdProcess"
        Me.grdProcess.RecordNavigator = True
        Me.grdProcess.Size = New System.Drawing.Size(1017, 676)
        Me.grdProcess.TabIndex = 0
        Me.grdProcess.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.ToolStripSeparator2, Me.btnPost, Me.toolStripSeparator, Me.btnPrint, Me.btnPrintSheet, Me.btnDelete, Me.toolStripSeparator1, Me.btnConfiguration, Me.btnAddSalaryType, Me.BtnOpenAttendanceSetup, Me.ToolStripSeparator3, Me.BtnTaxSlabs, Me.BtnLoanDeduction, Me.ToolStripSeparator4, Me.HelpToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1019, 27)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(63, 24)
        Me.btnNew.Text = "&New"
        '
        'btnEdit
        '
        Me.btnEdit.Image = CType(resources.GetObject("btnEdit.Image"), System.Drawing.Image)
        Me.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(59, 24)
        Me.btnEdit.Text = "&Edit"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(64, 24)
        Me.btnSave.Text = "&Save"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 27)
        '
        'btnPost
        '
        Me.btnPost.Image = Global.SimpleAccounts.My.Resources.Resources.sendcontactdetails
        Me.btnPost.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(60, 24)
        Me.btnPost.Text = "Post"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 27)
        '
        'btnPrint
        '
        Me.btnPrint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(24, 24)
        Me.btnPrint.Text = "&Print All Salary Voucher"
        '
        'btnPrintSheet
        '
        Me.btnPrintSheet.Image = Global.SimpleAccounts.My.Resources.Resources.print
        Me.btnPrintSheet.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrintSheet.Name = "btnPrintSheet"
        Me.btnPrintSheet.Size = New System.Drawing.Size(148, 24)
        Me.btnPrintSheet.Text = "&Print Salary Sheet"
        '
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(77, 24)
        Me.btnDelete.Text = "D&elete"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 27)
        '
        'btnConfiguration
        '
        Me.btnConfiguration.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnConfiguration.Image = Global.SimpleAccounts.My.Resources.Resources.Control_Panel_1
        Me.btnConfiguration.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnConfiguration.Name = "btnConfiguration"
        Me.btnConfiguration.Size = New System.Drawing.Size(24, 24)
        Me.btnConfiguration.Text = "Configuration"
        '
        'btnAddSalaryType
        '
        Me.btnAddSalaryType.Image = Global.SimpleAccounts.My.Resources.Resources.Compose_Email
        Me.btnAddSalaryType.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAddSalaryType.Name = "btnAddSalaryType"
        Me.btnAddSalaryType.Size = New System.Drawing.Size(140, 24)
        Me.btnAddSalaryType.Text = "Add Salary Type"
        '
        'BtnOpenAttendanceSetup
        '
        Me.BtnOpenAttendanceSetup.Image = Global.SimpleAccounts.My.Resources.Resources.Compose_Email
        Me.BtnOpenAttendanceSetup.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnOpenAttendanceSetup.Name = "BtnOpenAttendanceSetup"
        Me.BtnOpenAttendanceSetup.Size = New System.Drawing.Size(151, 24)
        Me.BtnOpenAttendanceSetup.Text = "Attendance Setup"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 27)
        '
        'BtnTaxSlabs
        '
        Me.BtnTaxSlabs.Image = Global.SimpleAccounts.My.Resources.Resources.Compose_Email
        Me.BtnTaxSlabs.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnTaxSlabs.Name = "BtnTaxSlabs"
        Me.BtnTaxSlabs.Size = New System.Drawing.Size(93, 24)
        Me.BtnTaxSlabs.Text = "Tax Slabs"
        '
        'BtnLoanDeduction
        '
        Me.BtnLoanDeduction.Image = Global.SimpleAccounts.My.Resources.Resources.Compose_Email
        Me.BtnLoanDeduction.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnLoanDeduction.Name = "BtnLoanDeduction"
        Me.BtnLoanDeduction.Size = New System.Drawing.Size(138, 24)
        Me.BtnLoanDeduction.Text = "Loan Deduction"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'HelpToolStripButton
        '
        Me.HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HelpToolStripButton.Image = CType(resources.GetObject("HelpToolStripButton.Image"), System.Drawing.Image)
        Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton.Name = "HelpToolStripButton"
        Me.HelpToolStripButton.Size = New System.Drawing.Size(24, 24)
        Me.HelpToolStripButton.Text = "He&lp"
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 27)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(1019, 700)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 0
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Salary Generate"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "History"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1017, 676)
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(984, 0)
        Me.CtrlGrdBar1.MyGrid = Me.grdSalary
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(34, 25)
        Me.CtrlGrdBar1.TabIndex = 2
        '
        'frmAutoSalaryGenerate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 17.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1019, 727)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmAutoSalaryGenerate"
        Me.Text = "Auto Salary Generate"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grdSalary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdProcess, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
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
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnPost As System.Windows.Forms.ToolStripButton
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents grdSalary As Janus.Windows.GridEX.GridEX
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpSalaryDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnGenerate As System.Windows.Forms.Button
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grdProcess As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnAddSalaryType As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtYear As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbMonth As System.Windows.Forms.ComboBox
    Friend WithEvents BtnOpenAttendanceSetup As System.Windows.Forms.ToolStripButton
    Friend WithEvents chkAutoAdjustAbsentAttendance As System.Windows.Forms.CheckBox
    Friend WithEvents BtnTaxSlabs As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnLoanDeduction As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblValidating As System.Windows.Forms.Label
    Friend WithEvents pgbValidating As System.Windows.Forms.ProgressBar
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnConfiguration As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbDepartment As System.Windows.Forms.ComboBox
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents lblDailyAttendance As System.Windows.Forms.LinkLabel
    Friend WithEvents btnPrintSheet As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblCostCenter As System.Windows.Forms.Label
    Friend WithEvents cmbCostCenter As System.Windows.Forms.ComboBox
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

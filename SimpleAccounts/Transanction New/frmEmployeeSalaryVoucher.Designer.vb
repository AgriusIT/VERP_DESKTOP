<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmployeeSalaryVoucher
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
        Me.components = New System.ComponentModel.Container()
        Dim grd_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmployeeSalaryVoucher))
        Dim grdEmpBalanceSummary_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripSplitButton()
        Me.SalarySlipToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintSelectedRowsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnLoadAll = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnAddSalaryType = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnConfiguration = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmbLang = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.btnSearchHistory = New System.Windows.Forms.ToolStripButton()
        Me.cmbEmployees = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtGrossSalaryCalc = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtGrossSalary = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.grdEmpBalanceSummary = New Janus.Windows.GridEX.GridEX()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtRec = New System.Windows.Forms.TextBox()
        Me.txtBasicSalary = New System.Windows.Forms.TextBox()
        Me.lblDesignation = New System.Windows.Forms.Label()
        Me.lblDepartment = New System.Windows.Forms.Label()
        Me.txtDesignation = New System.Windows.Forms.TextBox()
        Me.txtDepartment = New System.Windows.Forms.TextBox()
        Me.lblEmployee = New System.Windows.Forms.Label()
        Me.txtNetSalary = New System.Windows.Forms.TextBox()
        Me.lblNetSalary = New System.Windows.Forms.Label()
        Me.chkPost = New System.Windows.Forms.CheckBox()
        Me.cmbCostCenter = New System.Windows.Forms.ComboBox()
        Me.lblCostCenter = New System.Windows.Forms.Label()
        Me.lblRemarks = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.lblVoucherDate = New System.Windows.Forms.Label()
        Me.dtpVoucherDate = New System.Windows.Forms.DateTimePicker()
        Me.lblVoucherNo = New System.Windows.Forms.Label()
        Me.txtVoucherNo = New System.Windows.Forms.TextBox()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.cmbSearchByDepartment = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.lblSearchByDepartment = New System.Windows.Forms.Label()
        Me.btnHideSearchBy = New System.Windows.Forms.Button()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.lblSearchByEmployee = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.cmbSearchByDesignation = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.cmbSearchByEmployee = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.lblSearchByDesignation = New System.Windows.Forms.Label()
        Me.grdSaved = New Janus.Windows.GridEX.GridEX()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.UltraTabPageControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.cmbEmployees, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdEmpBalanceSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.cmbSearchByDepartment, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbSearchByDesignation, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbSearchByEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.cmbEmployees)
        Me.UltraTabPageControl1.Controls.Add(Me.lblProgress)
        Me.UltraTabPageControl1.Controls.Add(Me.Label5)
        Me.UltraTabPageControl1.Controls.Add(Me.txtGrossSalaryCalc)
        Me.UltraTabPageControl1.Controls.Add(Me.Button1)
        Me.UltraTabPageControl1.Controls.Add(Me.Label6)
        Me.UltraTabPageControl1.Controls.Add(Me.txtGrossSalary)
        Me.UltraTabPageControl1.Controls.Add(Me.Label4)
        Me.UltraTabPageControl1.Controls.Add(Me.grdEmpBalanceSummary)
        Me.UltraTabPageControl1.Controls.Add(Me.Label3)
        Me.UltraTabPageControl1.Controls.Add(Me.Label2)
        Me.UltraTabPageControl1.Controls.Add(Me.txtRec)
        Me.UltraTabPageControl1.Controls.Add(Me.txtBasicSalary)
        Me.UltraTabPageControl1.Controls.Add(Me.lblDesignation)
        Me.UltraTabPageControl1.Controls.Add(Me.lblDepartment)
        Me.UltraTabPageControl1.Controls.Add(Me.txtDesignation)
        Me.UltraTabPageControl1.Controls.Add(Me.txtDepartment)
        Me.UltraTabPageControl1.Controls.Add(Me.lblEmployee)
        Me.UltraTabPageControl1.Controls.Add(Me.txtNetSalary)
        Me.UltraTabPageControl1.Controls.Add(Me.lblNetSalary)
        Me.UltraTabPageControl1.Controls.Add(Me.grd)
        Me.UltraTabPageControl1.Controls.Add(Me.chkPost)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbCostCenter)
        Me.UltraTabPageControl1.Controls.Add(Me.lblCostCenter)
        Me.UltraTabPageControl1.Controls.Add(Me.lblRemarks)
        Me.UltraTabPageControl1.Controls.Add(Me.txtRemarks)
        Me.UltraTabPageControl1.Controls.Add(Me.lblVoucherDate)
        Me.UltraTabPageControl1.Controls.Add(Me.dtpVoucherDate)
        Me.UltraTabPageControl1.Controls.Add(Me.lblVoucherNo)
        Me.UltraTabPageControl1.Controls.Add(Me.txtVoucherNo)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1026, 533)
        Me.ToolTip1.SetToolTip(Me.UltraTabPageControl1, "Posted Voucher If Checked On")
        '
        'pnlHeader
        '
        Me.pnlHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlHeader.Controls.Add(Me.CtrlGrdBar1)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Controls.Add(Me.ToolStrip1)
        Me.pnlHeader.Location = New System.Drawing.Point(1, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1026, 56)
        Me.pnlHeader.TabIndex = 78
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(982, 0)
        Me.CtrlGrdBar1.MyGrid = Me.grdSaved
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(42, 26)
        Me.CtrlGrdBar1.TabIndex = 2
        '
        'grd
        '
        Me.grd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        grd_DesignTimeLayout.LayoutString = resources.GetString("grd_DesignTimeLayout.LayoutString")
        Me.grd.DesignTimeLayout = grd_DesignTimeLayout
        Me.grd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grd.GroupByBoxVisible = False
        Me.grd.Location = New System.Drawing.Point(1, 242)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(452, 288)
        Me.grd.TabIndex = 27
        Me.grd.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grd, "Employee Salary Detail")
        Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(11, 24)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(189, 23)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Employee Salary"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.btnPrint, Me.toolStripSeparator, Me.btnDelete, Me.toolStripSeparator1, Me.btnRefresh, Me.btnLoadAll, Me.ToolStripSeparator2, Me.btnAddSalaryType, Me.ToolStripSeparator3, Me.btnConfiguration, Me.ToolStripSeparator5, Me.cmbLang, Me.ToolStripSeparator4, Me.HelpToolStripButton, Me.btnSearchHistory})
        Me.ToolStrip1.Location = New System.Drawing.Point(1, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(981, 25)
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
        Me.btnPrint.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SalarySlipToolStripMenuItem, Me.PrintSelectedRowsToolStripMenuItem})
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(64, 22)
        Me.btnPrint.Text = "&Print"
        '
        'SalarySlipToolStripMenuItem
        '
        Me.SalarySlipToolStripMenuItem.Name = "SalarySlipToolStripMenuItem"
        Me.SalarySlipToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.SalarySlipToolStripMenuItem.Text = "Salary Slip"
        '
        'PrintSelectedRowsToolStripMenuItem
        '
        Me.PrintSelectedRowsToolStripMenuItem.Name = "PrintSelectedRowsToolStripMenuItem"
        Me.PrintSelectedRowsToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.PrintSelectedRowsToolStripMenuItem.Text = "Print Selected Rows"
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
        'btnLoadAll
        '
        Me.btnLoadAll.Image = Global.SimpleAccounts.My.Resources.Resources.sendcontactdetails
        Me.btnLoadAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnLoadAll.Name = "btnLoadAll"
        Me.btnLoadAll.Size = New System.Drawing.Size(70, 22)
        Me.btnLoadAll.Text = "Load All"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'btnAddSalaryType
        '
        Me.btnAddSalaryType.Image = Global.SimpleAccounts.My.Resources.Resources.DetailAccount
        Me.btnAddSalaryType.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAddSalaryType.Name = "btnAddSalaryType"
        Me.btnAddSalaryType.Size = New System.Drawing.Size(87, 22)
        Me.btnAddSalaryType.Text = "Salary Type"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'btnConfiguration
        '
        Me.btnConfiguration.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnConfiguration.Image = Global.SimpleAccounts.My.Resources.Resources.Control_Panel_1
        Me.btnConfiguration.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnConfiguration.Name = "btnConfiguration"
        Me.btnConfiguration.Size = New System.Drawing.Size(23, 22)
        Me.btnConfiguration.Text = "Configuration"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'cmbLang
        '
        Me.cmbLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLang.Items.AddRange(New Object() {"English", "Urdu"})
        Me.cmbLang.Name = "cmbLang"
        Me.cmbLang.Size = New System.Drawing.Size(121, 25)
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
        Me.HelpToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.HelpToolStripButton.Text = "He&lp"
        '
        'btnSearchHistory
        '
        Me.btnSearchHistory.Image = Global.SimpleAccounts.My.Resources.Resources.search
        Me.btnSearchHistory.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSearchHistory.Name = "btnSearchHistory"
        Me.btnSearchHistory.Size = New System.Drawing.Size(62, 22)
        Me.btnSearchHistory.Text = "Search"
        '
        'cmbEmployees
        '
        Me.cmbEmployees.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cmbEmployees.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbEmployees.CheckedListSettings.CheckStateMember = ""
        Me.cmbEmployees.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbEmployees.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbEmployees.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbEmployees.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Me.cmbEmployees.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbEmployees.DisplayLayout.Override.CellPadding = 3
        Me.cmbEmployees.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Me.cmbEmployees.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.cmbEmployees.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbEmployees.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbEmployees.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbEmployees.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.cmbEmployees.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbEmployees.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbEmployees.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbEmployees.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbEmployees.LimitToList = True
        Me.cmbEmployees.Location = New System.Drawing.Point(102, 84)
        Me.cmbEmployees.Name = "cmbEmployees"
        Me.cmbEmployees.Size = New System.Drawing.Size(223, 22)
        Me.cmbEmployees.TabIndex = 53
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(190, 289)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 52
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(344, 144)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(159, 13)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "Apply Formula After Basic Salary"
        '
        'txtGrossSalaryCalc
        '
        Me.txtGrossSalaryCalc.AcceptsTab = True
        Me.txtGrossSalaryCalc.Location = New System.Drawing.Point(514, 141)
        Me.txtGrossSalaryCalc.Name = "txtGrossSalaryCalc"
        Me.txtGrossSalaryCalc.ReadOnly = True
        Me.txtGrossSalaryCalc.Size = New System.Drawing.Size(130, 20)
        Me.txtGrossSalaryCalc.TabIndex = 21
        Me.txtGrossSalaryCalc.TabStop = False
        Me.ToolTip1.SetToolTip(Me.txtGrossSalaryCalc, "Net Salary")
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(650, 167)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(25, 23)
        Me.Button1.TabIndex = 24
        Me.Button1.TabStop = False
        Me.Button1.Text = "..."
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(344, 171)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 13)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "Gross Salary"
        '
        'txtGrossSalary
        '
        Me.txtGrossSalary.AcceptsTab = True
        Me.txtGrossSalary.Location = New System.Drawing.Point(514, 168)
        Me.txtGrossSalary.Name = "txtGrossSalary"
        Me.txtGrossSalary.ReadOnly = True
        Me.txtGrossSalary.Size = New System.Drawing.Size(130, 20)
        Me.txtGrossSalary.TabIndex = 23
        Me.txtGrossSalary.TabStop = False
        Me.ToolTip1.SetToolTip(Me.txtGrossSalary, "Net Salary")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(456, 226)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(92, 13)
        Me.Label4.TabIndex = 28
        Me.Label4.Text = "Balance Summary"
        '
        'grdEmpBalanceSummary
        '
        Me.grdEmpBalanceSummary.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdEmpBalanceSummary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        grdEmpBalanceSummary_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grdEmpBalanceSummary.DesignTimeLayout = grdEmpBalanceSummary_DesignTimeLayout
        Me.grdEmpBalanceSummary.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdEmpBalanceSummary.GroupByBoxVisible = False
        Me.grdEmpBalanceSummary.Location = New System.Drawing.Point(458, 242)
        Me.grdEmpBalanceSummary.Name = "grdEmpBalanceSummary"
        Me.grdEmpBalanceSummary.RecordNavigator = True
        Me.grdEmpBalanceSummary.Size = New System.Drawing.Size(468, 288)
        Me.grdEmpBalanceSummary.TabIndex = 29
        Me.grdEmpBalanceSummary.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grdEmpBalanceSummary, "Employee Salary Detail")
        Me.grdEmpBalanceSummary.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdEmpBalanceSummary.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdEmpBalanceSummary.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(344, 118)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(65, 13)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Basic Salary"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(344, 67)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(61, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Receivable"
        Me.Label2.Visible = False
        '
        'txtRec
        '
        Me.txtRec.Location = New System.Drawing.Point(514, 64)
        Me.txtRec.Name = "txtRec"
        Me.txtRec.ReadOnly = True
        Me.txtRec.Size = New System.Drawing.Size(42, 20)
        Me.txtRec.TabIndex = 2
        Me.txtRec.TabStop = False
        Me.ToolTip1.SetToolTip(Me.txtRec, "Net Salary")
        Me.txtRec.Visible = False
        '
        'txtBasicSalary
        '
        Me.txtBasicSalary.Location = New System.Drawing.Point(514, 115)
        Me.txtBasicSalary.Name = "txtBasicSalary"
        Me.txtBasicSalary.ReadOnly = True
        Me.txtBasicSalary.Size = New System.Drawing.Size(130, 20)
        Me.txtBasicSalary.TabIndex = 19
        Me.txtBasicSalary.TabStop = False
        Me.ToolTip1.SetToolTip(Me.txtBasicSalary, "Net Salary")
        '
        'lblDesignation
        '
        Me.lblDesignation.AutoSize = True
        Me.lblDesignation.Location = New System.Drawing.Point(12, 140)
        Me.lblDesignation.Name = "lblDesignation"
        Me.lblDesignation.Size = New System.Drawing.Size(63, 13)
        Me.lblDesignation.TabIndex = 11
        Me.lblDesignation.Text = "Designation"
        '
        'lblDepartment
        '
        Me.lblDepartment.AutoSize = True
        Me.lblDepartment.Location = New System.Drawing.Point(12, 114)
        Me.lblDepartment.Name = "lblDepartment"
        Me.lblDepartment.Size = New System.Drawing.Size(62, 13)
        Me.lblDepartment.TabIndex = 9
        Me.lblDepartment.Text = "Department"
        '
        'txtDesignation
        '
        Me.txtDesignation.Location = New System.Drawing.Point(102, 137)
        Me.txtDesignation.Name = "txtDesignation"
        Me.txtDesignation.ReadOnly = True
        Me.txtDesignation.Size = New System.Drawing.Size(223, 20)
        Me.txtDesignation.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.txtDesignation, "Designation")
        '
        'txtDepartment
        '
        Me.txtDepartment.Location = New System.Drawing.Point(102, 111)
        Me.txtDepartment.Name = "txtDepartment"
        Me.txtDepartment.ReadOnly = True
        Me.txtDepartment.Size = New System.Drawing.Size(223, 20)
        Me.txtDepartment.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.txtDepartment, "Department")
        '
        'lblEmployee
        '
        Me.lblEmployee.AutoSize = True
        Me.lblEmployee.Location = New System.Drawing.Point(12, 87)
        Me.lblEmployee.Name = "lblEmployee"
        Me.lblEmployee.Size = New System.Drawing.Size(53, 13)
        Me.lblEmployee.TabIndex = 7
        Me.lblEmployee.Text = "Employee"
        '
        'txtNetSalary
        '
        Me.txtNetSalary.Location = New System.Drawing.Point(514, 194)
        Me.txtNetSalary.Name = "txtNetSalary"
        Me.txtNetSalary.ReadOnly = True
        Me.txtNetSalary.Size = New System.Drawing.Size(130, 20)
        Me.txtNetSalary.TabIndex = 26
        Me.txtNetSalary.TabStop = False
        Me.ToolTip1.SetToolTip(Me.txtNetSalary, "Net Salary")
        '
        'lblNetSalary
        '
        Me.lblNetSalary.AutoSize = True
        Me.lblNetSalary.Location = New System.Drawing.Point(344, 198)
        Me.lblNetSalary.Name = "lblNetSalary"
        Me.lblNetSalary.Size = New System.Drawing.Size(56, 13)
        Me.lblNetSalary.TabIndex = 25
        Me.lblNetSalary.Text = "Net Salary"
        '
        'chkPost
        '
        Me.chkPost.AutoSize = True
        Me.chkPost.Checked = True
        Me.chkPost.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPost.Location = New System.Drawing.Point(102, 216)
        Me.chkPost.Name = "chkPost"
        Me.chkPost.Size = New System.Drawing.Size(59, 17)
        Me.chkPost.TabIndex = 17
        Me.chkPost.Text = "Posted"
        Me.chkPost.UseVisualStyleBackColor = True
        '
        'cmbCostCenter
        '
        Me.cmbCostCenter.FormattingEnabled = True
        Me.cmbCostCenter.Location = New System.Drawing.Point(102, 163)
        Me.cmbCostCenter.Name = "cmbCostCenter"
        Me.cmbCostCenter.Size = New System.Drawing.Size(223, 21)
        Me.cmbCostCenter.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.cmbCostCenter, "Select Cost Center")
        '
        'lblCostCenter
        '
        Me.lblCostCenter.AutoSize = True
        Me.lblCostCenter.Location = New System.Drawing.Point(12, 166)
        Me.lblCostCenter.Name = "lblCostCenter"
        Me.lblCostCenter.Size = New System.Drawing.Size(62, 13)
        Me.lblCostCenter.TabIndex = 13
        Me.lblCostCenter.Text = "Cost Center"
        '
        'lblRemarks
        '
        Me.lblRemarks.AutoSize = True
        Me.lblRemarks.Location = New System.Drawing.Point(12, 193)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(49, 13)
        Me.lblRemarks.TabIndex = 15
        Me.lblRemarks.Text = "Remarks"
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(102, 190)
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(223, 20)
        Me.txtRemarks.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.txtRemarks, "Remarks for necessary")
        '
        'lblVoucherDate
        '
        Me.lblVoucherDate.AutoSize = True
        Me.lblVoucherDate.Location = New System.Drawing.Point(344, 92)
        Me.lblVoucherDate.Name = "lblVoucherDate"
        Me.lblVoucherDate.Size = New System.Drawing.Size(73, 13)
        Me.lblVoucherDate.TabIndex = 5
        Me.lblVoucherDate.Text = "Voucher Date"
        '
        'dtpVoucherDate
        '
        Me.dtpVoucherDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpVoucherDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpVoucherDate.Location = New System.Drawing.Point(514, 89)
        Me.dtpVoucherDate.Name = "dtpVoucherDate"
        Me.dtpVoucherDate.Size = New System.Drawing.Size(130, 20)
        Me.dtpVoucherDate.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.dtpVoucherDate, "Voucher Date")
        '
        'lblVoucherNo
        '
        Me.lblVoucherNo.AutoSize = True
        Me.lblVoucherNo.Location = New System.Drawing.Point(12, 61)
        Me.lblVoucherNo.Name = "lblVoucherNo"
        Me.lblVoucherNo.Size = New System.Drawing.Size(67, 13)
        Me.lblVoucherNo.TabIndex = 3
        Me.lblVoucherNo.Text = "Voucher No."
        '
        'txtVoucherNo
        '
        Me.txtVoucherNo.Location = New System.Drawing.Point(102, 58)
        Me.txtVoucherNo.Name = "txtVoucherNo"
        Me.txtVoucherNo.ReadOnly = True
        Me.txtVoucherNo.Size = New System.Drawing.Size(126, 20)
        Me.txtVoucherNo.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.txtVoucherNo, "Voucher No")
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.SplitContainer1)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1026, 533)
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(2, 61)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.AutoScroll = True
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmbSearchByDepartment)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblSearchByDepartment)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnHideSearchBy)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpFrom)
        Me.SplitContainer1.Panel1.Controls.Add(Me.btnSearch)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblFrom)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblSearchByEmployee)
        Me.SplitContainer1.Panel1.Controls.Add(Me.dtpTo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmbSearchByDesignation)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblTo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.cmbSearchByEmployee)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblSearchByDesignation)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.grdSaved)
        Me.SplitContainer1.Size = New System.Drawing.Size(1026, 469)
        Me.SplitContainer1.SplitterDistance = 80
        Me.SplitContainer1.TabIndex = 2
        '
        'cmbSearchByDepartment
        '
        Me.cmbSearchByDepartment.AlwaysInEditMode = True
        Me.cmbSearchByDepartment.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cmbSearchByDepartment.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbSearchByDepartment.CheckedListSettings.CheckStateMember = ""
        Me.cmbSearchByDepartment.DisplayLayout.InterBandSpacing = 10
        Me.cmbSearchByDepartment.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbSearchByDepartment.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbSearchByDepartment.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbSearchByDepartment.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbSearchByDepartment.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Me.cmbSearchByDepartment.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.cmbSearchByDepartment.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbSearchByDepartment.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance2.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance2.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance2.ForeColor = System.Drawing.Color.Black
        Me.cmbSearchByDepartment.DisplayLayout.Override.SelectedRowAppearance = Appearance2
        Me.cmbSearchByDepartment.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbSearchByDepartment.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbSearchByDepartment.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbSearchByDepartment.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbSearchByDepartment.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.cmbSearchByDepartment.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbSearchByDepartment.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbSearchByDepartment.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbSearchByDepartment.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbSearchByDepartment.LimitToList = True
        Me.cmbSearchByDepartment.Location = New System.Drawing.Point(88, 29)
        Me.cmbSearchByDepartment.MaxDropDownItems = 20
        Me.cmbSearchByDepartment.Name = "cmbSearchByDepartment"
        Me.cmbSearchByDepartment.Size = New System.Drawing.Size(441, 22)
        Me.cmbSearchByDepartment.TabIndex = 5
        '
        'lblSearchByDepartment
        '
        Me.lblSearchByDepartment.AutoSize = True
        Me.lblSearchByDepartment.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearchByDepartment.Location = New System.Drawing.Point(5, 33)
        Me.lblSearchByDepartment.Name = "lblSearchByDepartment"
        Me.lblSearchByDepartment.Size = New System.Drawing.Size(75, 13)
        Me.lblSearchByDepartment.TabIndex = 4
        Me.lblSearchByDepartment.Text = "Department"
        '
        'btnHideSearchBy
        '
        Me.btnHideSearchBy.Location = New System.Drawing.Point(375, 109)
        Me.btnHideSearchBy.Name = "btnHideSearchBy"
        Me.btnHideSearchBy.Size = New System.Drawing.Size(75, 23)
        Me.btnHideSearchBy.TabIndex = 11
        Me.btnHideSearchBy.Text = "Hide"
        Me.btnHideSearchBy.UseVisualStyleBackColor = True
        '
        'dtpFrom
        '
        Me.dtpFrom.Checked = False
        Me.dtpFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(88, 3)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.ShowCheckBox = True
        Me.dtpFrom.Size = New System.Drawing.Size(202, 20)
        Me.dtpFrom.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.dtpFrom, "Attendance Date")
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(455, 109)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 23)
        Me.btnSearch.TabIndex = 10
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFrom.Location = New System.Drawing.Point(5, 9)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(36, 13)
        Me.lblFrom.TabIndex = 0
        Me.lblFrom.Text = "From"
        '
        'lblSearchByEmployee
        '
        Me.lblSearchByEmployee.AutoSize = True
        Me.lblSearchByEmployee.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearchByEmployee.Location = New System.Drawing.Point(5, 87)
        Me.lblSearchByEmployee.Name = "lblSearchByEmployee"
        Me.lblSearchByEmployee.Size = New System.Drawing.Size(67, 13)
        Me.lblSearchByEmployee.TabIndex = 8
        Me.lblSearchByEmployee.Text = "Employee "
        '
        'dtpTo
        '
        Me.dtpTo.Checked = False
        Me.dtpTo.CustomFormat = "dd/MMM/yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(327, 3)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.ShowCheckBox = True
        Me.dtpTo.Size = New System.Drawing.Size(202, 20)
        Me.dtpTo.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.dtpTo, "Attendance Date")
        '
        'cmbSearchByDesignation
        '
        Me.cmbSearchByDesignation.AlwaysInEditMode = True
        Me.cmbSearchByDesignation.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cmbSearchByDesignation.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbSearchByDesignation.CheckedListSettings.CheckStateMember = ""
        Me.cmbSearchByDesignation.DisplayLayout.InterBandSpacing = 10
        Me.cmbSearchByDesignation.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbSearchByDesignation.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbSearchByDesignation.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbSearchByDesignation.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbSearchByDesignation.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Me.cmbSearchByDesignation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.cmbSearchByDesignation.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbSearchByDesignation.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance4.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance4.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance4.ForeColor = System.Drawing.Color.Black
        Me.cmbSearchByDesignation.DisplayLayout.Override.SelectedRowAppearance = Appearance4
        Me.cmbSearchByDesignation.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbSearchByDesignation.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbSearchByDesignation.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbSearchByDesignation.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbSearchByDesignation.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.cmbSearchByDesignation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbSearchByDesignation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbSearchByDesignation.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbSearchByDesignation.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbSearchByDesignation.LimitToList = True
        Me.cmbSearchByDesignation.Location = New System.Drawing.Point(88, 56)
        Me.cmbSearchByDesignation.MaxDropDownItems = 20
        Me.cmbSearchByDesignation.Name = "cmbSearchByDesignation"
        Me.cmbSearchByDesignation.Size = New System.Drawing.Size(441, 22)
        Me.cmbSearchByDesignation.TabIndex = 7
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTo.Location = New System.Drawing.Point(301, 9)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(21, 13)
        Me.lblTo.TabIndex = 2
        Me.lblTo.Text = "To"
        '
        'cmbSearchByEmployee
        '
        Me.cmbSearchByEmployee.AlwaysInEditMode = True
        Me.cmbSearchByEmployee.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cmbSearchByEmployee.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbSearchByEmployee.CheckedListSettings.CheckStateMember = ""
        Me.cmbSearchByEmployee.DisplayLayout.InterBandSpacing = 10
        Me.cmbSearchByEmployee.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbSearchByEmployee.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbSearchByEmployee.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbSearchByEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbSearchByEmployee.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Me.cmbSearchByEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.cmbSearchByEmployee.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbSearchByEmployee.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance3.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance3.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance3.ForeColor = System.Drawing.Color.Black
        Me.cmbSearchByEmployee.DisplayLayout.Override.SelectedRowAppearance = Appearance3
        Me.cmbSearchByEmployee.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbSearchByEmployee.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbSearchByEmployee.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbSearchByEmployee.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbSearchByEmployee.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.cmbSearchByEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbSearchByEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbSearchByEmployee.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbSearchByEmployee.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbSearchByEmployee.LimitToList = True
        Me.cmbSearchByEmployee.Location = New System.Drawing.Point(88, 83)
        Me.cmbSearchByEmployee.MaxDropDownItems = 20
        Me.cmbSearchByEmployee.Name = "cmbSearchByEmployee"
        Me.cmbSearchByEmployee.Size = New System.Drawing.Size(441, 22)
        Me.cmbSearchByEmployee.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.cmbSearchByEmployee, "Select any Item")
        '
        'lblSearchByDesignation
        '
        Me.lblSearchByDesignation.AutoSize = True
        Me.lblSearchByDesignation.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSearchByDesignation.Location = New System.Drawing.Point(5, 61)
        Me.lblSearchByDesignation.Name = "lblSearchByDesignation"
        Me.lblSearchByDesignation.Size = New System.Drawing.Size(74, 13)
        Me.lblSearchByDesignation.TabIndex = 6
        Me.lblSearchByDesignation.Text = "Designation"
        '
        'grdSaved
        '
        Me.grdSaved.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdSaved.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSaved.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdSaved.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdSaved.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdSaved.GroupByBoxVisible = False
        Me.grdSaved.Location = New System.Drawing.Point(0, 3)
        Me.grdSaved.Name = "grdSaved"
        Me.grdSaved.RecordNavigator = True
        Me.grdSaved.Size = New System.Drawing.Size(1026, 379)
        Me.grdSaved.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.grdSaved, "Saved Record Detail")
        Me.grdSaved.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdSaved.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(1028, 554)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 0
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Employee Salary"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "History"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1026, 533)
        '
        'frmEmployeeSalaryVoucher
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1028, 554)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmEmployeeSalaryVoucher"
        Me.Text = "Employee Salary Voucher"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.cmbEmployees, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdEmpBalanceSummary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl2.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.cmbSearchByDepartment, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbSearchByDesignation, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbSearchByEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents lblVoucherDate As System.Windows.Forms.Label
    Friend WithEvents dtpVoucherDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblVoucherNo As System.Windows.Forms.Label
    Friend WithEvents txtVoucherNo As System.Windows.Forms.TextBox
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents lblRemarks As System.Windows.Forms.Label
    Friend WithEvents lblCostCenter As System.Windows.Forms.Label
    Friend WithEvents cmbCostCenter As System.Windows.Forms.ComboBox
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents grdSaved As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnLoadAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnAddSalaryType As System.Windows.Forms.ToolStripButton
    Friend WithEvents chkPost As System.Windows.Forms.CheckBox
    Friend WithEvents txtNetSalary As System.Windows.Forms.TextBox
    Friend WithEvents lblNetSalary As System.Windows.Forms.Label
    Friend WithEvents lblEmployee As System.Windows.Forms.Label
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents txtDesignation As System.Windows.Forms.TextBox
    Friend WithEvents txtDepartment As System.Windows.Forms.TextBox
    Friend WithEvents lblDesignation As System.Windows.Forms.Label
    Friend WithEvents lblDepartment As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtRec As System.Windows.Forms.TextBox
    Friend WithEvents txtBasicSalary As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents grdEmpBalanceSummary As Janus.Windows.GridEX.GridEX
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtGrossSalary As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtGrossSalaryCalc As System.Windows.Forms.TextBox
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents SalarySlipToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmbLang As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents cmbEmployees As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents btnConfiguration As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PrintSelectedRowsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmbSearchByDepartment As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents lblSearchByDepartment As System.Windows.Forms.Label
    Friend WithEvents btnHideSearchBy As System.Windows.Forms.Button
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents lblSearchByEmployee As System.Windows.Forms.Label
    Friend WithEvents cmbSearchByDesignation As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents cmbSearchByEmployee As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents lblSearchByDesignation As System.Windows.Forms.Label
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnSearchHistory As System.Windows.Forms.ToolStripButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
End Class

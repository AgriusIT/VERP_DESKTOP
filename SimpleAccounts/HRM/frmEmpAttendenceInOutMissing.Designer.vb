<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmpAttendenceInOutMissing
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmpAttendenceInOutMissing))
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.pnlPeriod = New System.Windows.Forms.Panel()
        Me.lblPeriod = New System.Windows.Forms.Label()
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        Me.lblToDate = New System.Windows.Forms.Label()
        Me.lblFromDate = New System.Windows.Forms.Label()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.pnlEmpAttendence = New System.Windows.Forms.Panel()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lstCostCenter = New SimpleAccounts.uiListControl()
        Me.lstHeadCostCenter = New SimpleAccounts.uiListControl()
        Me.lstEmployee = New SimpleAccounts.uiListControl()
        Me.lstEmpDepartment = New SimpleAccounts.uiListControl()
        Me.lstEmpDesignation = New SimpleAccounts.uiListControl()
        Me.lstEmpShiftGroup = New SimpleAccounts.uiListControl()
        Me.lstEmpCity = New SimpleAccounts.uiListControl()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdSaved = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnHelp = New System.Windows.Forms.ToolStripButton()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.pnlPeriod.SuspendLayout()
        Me.pnlEmpAttendence.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.pnlPeriod)
        Me.UltraTabPageControl1.Controls.Add(Me.pnlEmpAttendence)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(767, 517)
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblProgress)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(767, 50)
        Me.pnlHeader.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.pnlHeader, "TItle")
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(345, 0)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(266, 50)
        Me.lblProgress.TabIndex = 1
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lblHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.lblHeader.Location = New System.Drawing.Point(8, 14)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(416, 23)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Employee Attendance In/Out Missing"
        Me.ToolTip1.SetToolTip(Me.lblHeader, "Employee Attendance In/Out Missing")
        '
        'pnlPeriod
        '
        Me.pnlPeriod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlPeriod.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.pnlPeriod.Controls.Add(Me.lblPeriod)
        Me.pnlPeriod.Controls.Add(Me.cmbPeriod)
        Me.pnlPeriod.Controls.Add(Me.lblToDate)
        Me.pnlPeriod.Controls.Add(Me.lblFromDate)
        Me.pnlPeriod.Controls.Add(Me.dtpToDate)
        Me.pnlPeriod.Controls.Add(Me.dtpFromDate)
        Me.pnlPeriod.Location = New System.Drawing.Point(3, 53)
        Me.pnlPeriod.Name = "pnlPeriod"
        Me.pnlPeriod.Size = New System.Drawing.Size(652, 68)
        Me.pnlPeriod.TabIndex = 2
        '
        'lblPeriod
        '
        Me.lblPeriod.AutoSize = True
        Me.lblPeriod.Location = New System.Drawing.Point(8, 9)
        Me.lblPeriod.Name = "lblPeriod"
        Me.lblPeriod.Size = New System.Drawing.Size(37, 13)
        Me.lblPeriod.TabIndex = 0
        Me.lblPeriod.Text = "Period"
        Me.ToolTip1.SetToolTip(Me.lblPeriod, "Period")
        '
        'cmbPeriod
        '
        Me.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPeriod.FormattingEnabled = True
        Me.cmbPeriod.Items.AddRange(New Object() {"Today", "Yesterday", "Current Week", "Current Month", "Current Year"})
        Me.cmbPeriod.Location = New System.Drawing.Point(70, 6)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(266, 21)
        Me.cmbPeriod.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbPeriod, "Select any value")
        '
        'lblToDate
        '
        Me.lblToDate.AutoSize = True
        Me.lblToDate.Location = New System.Drawing.Point(180, 33)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(46, 13)
        Me.lblToDate.TabIndex = 4
        Me.lblToDate.Text = "To Date"
        Me.ToolTip1.SetToolTip(Me.lblToDate, "To date")
        '
        'lblFromDate
        '
        Me.lblFromDate.AutoSize = True
        Me.lblFromDate.Location = New System.Drawing.Point(8, 33)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(56, 13)
        Me.lblFromDate.TabIndex = 2
        Me.lblFromDate.Text = "From Date"
        Me.ToolTip1.SetToolTip(Me.lblFromDate, "From date")
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(232, 33)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(104, 20)
        Me.dtpToDate.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.dtpToDate, "Select To date")
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(70, 33)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(104, 20)
        Me.dtpFromDate.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.dtpFromDate, "Select from date")
        '
        'pnlEmpAttendence
        '
        Me.pnlEmpAttendence.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlEmpAttendence.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.pnlEmpAttendence.Controls.Add(Me.btnShow)
        Me.pnlEmpAttendence.Controls.Add(Me.txtSearch)
        Me.pnlEmpAttendence.Controls.Add(Me.lstCostCenter)
        Me.pnlEmpAttendence.Controls.Add(Me.lstHeadCostCenter)
        Me.pnlEmpAttendence.Controls.Add(Me.lstEmployee)
        Me.pnlEmpAttendence.Controls.Add(Me.lstEmpDepartment)
        Me.pnlEmpAttendence.Controls.Add(Me.lstEmpDesignation)
        Me.pnlEmpAttendence.Controls.Add(Me.lstEmpShiftGroup)
        Me.pnlEmpAttendence.Controls.Add(Me.lstEmpCity)
        Me.pnlEmpAttendence.Location = New System.Drawing.Point(3, 127)
        Me.pnlEmpAttendence.Name = "pnlEmpAttendence"
        Me.pnlEmpAttendence.Size = New System.Drawing.Size(652, 360)
        Me.pnlEmpAttendence.TabIndex = 3
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(551, 330)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(57, 23)
        Me.btnShow.TabIndex = 8
        Me.btnShow.Text = "Show Attendence"
        Me.ToolTip1.SetToolTip(Me.btnShow, "Show Attendence")
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.SystemColors.Info
        Me.txtSearch.Location = New System.Drawing.Point(11, 333)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(282, 20)
        Me.txtSearch.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.txtSearch, "Search Employee")
        '
        'lstCostCenter
        '
        Me.lstCostCenter.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstCostCenter.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstCostCenter.BackColor = System.Drawing.Color.Transparent
        Me.lstCostCenter.disableWhenChecked = False
        Me.lstCostCenter.HeadingLabelName = "lstCostCenter"
        Me.lstCostCenter.HeadingText = "Cost Center"
        Me.lstCostCenter.Location = New System.Drawing.Point(483, 168)
        Me.lstCostCenter.Name = "lstCostCenter"
        Me.lstCostCenter.ShowAddNewButton = False
        Me.lstCostCenter.ShowInverse = True
        Me.lstCostCenter.ShowMagnifierButton = False
        Me.lstCostCenter.ShowNoCheck = False
        Me.lstCostCenter.ShowResetAllButton = False
        Me.lstCostCenter.ShowSelectall = True
        Me.lstCostCenter.Size = New System.Drawing.Size(152, 159)
        Me.lstCostCenter.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.lstCostCenter, "Employee Cost Center list")
        Me.lstCostCenter.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstHeadCostCenter
        '
        Me.lstHeadCostCenter.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstHeadCostCenter.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstHeadCostCenter.BackColor = System.Drawing.Color.Transparent
        Me.lstHeadCostCenter.disableWhenChecked = False
        Me.lstHeadCostCenter.HeadingLabelName = "lstHeadCostCenter"
        Me.lstHeadCostCenter.HeadingText = "Head Cost Center"
        Me.lstHeadCostCenter.Location = New System.Drawing.Point(325, 168)
        Me.lstHeadCostCenter.Name = "lstHeadCostCenter"
        Me.lstHeadCostCenter.ShowAddNewButton = False
        Me.lstHeadCostCenter.ShowInverse = True
        Me.lstHeadCostCenter.ShowMagnifierButton = False
        Me.lstHeadCostCenter.ShowNoCheck = False
        Me.lstHeadCostCenter.ShowResetAllButton = False
        Me.lstHeadCostCenter.ShowSelectall = True
        Me.lstHeadCostCenter.Size = New System.Drawing.Size(152, 159)
        Me.lstHeadCostCenter.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.lstHeadCostCenter, "Employee Head Cost Center list")
        Me.lstHeadCostCenter.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstEmployee
        '
        Me.lstEmployee.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstEmployee.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstEmployee.BackColor = System.Drawing.Color.Transparent
        Me.lstEmployee.disableWhenChecked = False
        Me.lstEmployee.HeadingLabelName = "lstEmployee"
        Me.lstEmployee.HeadingText = "Employee"
        Me.lstEmployee.Location = New System.Drawing.Point(9, 168)
        Me.lstEmployee.Name = "lstEmployee"
        Me.lstEmployee.ShowAddNewButton = False
        Me.lstEmployee.ShowInverse = True
        Me.lstEmployee.ShowMagnifierButton = False
        Me.lstEmployee.ShowNoCheck = False
        Me.lstEmployee.ShowResetAllButton = False
        Me.lstEmployee.ShowSelectall = True
        Me.lstEmployee.Size = New System.Drawing.Size(310, 159)
        Me.lstEmployee.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.lstEmployee, "Active Employee List")
        Me.lstEmployee.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstEmpDepartment
        '
        Me.lstEmpDepartment.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstEmpDepartment.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstEmpDepartment.BackColor = System.Drawing.Color.Transparent
        Me.lstEmpDepartment.disableWhenChecked = False
        Me.lstEmpDepartment.HeadingLabelName = "lstEmpDepartment"
        Me.lstEmpDepartment.HeadingText = "Department"
        Me.lstEmpDepartment.Location = New System.Drawing.Point(9, 3)
        Me.lstEmpDepartment.Name = "lstEmpDepartment"
        Me.lstEmpDepartment.ShowAddNewButton = False
        Me.lstEmpDepartment.ShowInverse = True
        Me.lstEmpDepartment.ShowMagnifierButton = False
        Me.lstEmpDepartment.ShowNoCheck = False
        Me.lstEmpDepartment.ShowResetAllButton = False
        Me.lstEmpDepartment.ShowSelectall = True
        Me.lstEmpDepartment.Size = New System.Drawing.Size(152, 159)
        Me.lstEmpDepartment.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.lstEmpDepartment, "Employee Department list")
        Me.lstEmpDepartment.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstEmpDesignation
        '
        Me.lstEmpDesignation.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstEmpDesignation.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstEmpDesignation.BackColor = System.Drawing.Color.Transparent
        Me.lstEmpDesignation.disableWhenChecked = False
        Me.lstEmpDesignation.HeadingLabelName = "lstDesignation"
        Me.lstEmpDesignation.HeadingText = "Designation"
        Me.lstEmpDesignation.Location = New System.Drawing.Point(167, 3)
        Me.lstEmpDesignation.Name = "lstEmpDesignation"
        Me.lstEmpDesignation.ShowAddNewButton = False
        Me.lstEmpDesignation.ShowInverse = True
        Me.lstEmpDesignation.ShowMagnifierButton = False
        Me.lstEmpDesignation.ShowNoCheck = False
        Me.lstEmpDesignation.ShowResetAllButton = False
        Me.lstEmpDesignation.ShowSelectall = True
        Me.lstEmpDesignation.Size = New System.Drawing.Size(152, 159)
        Me.lstEmpDesignation.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.lstEmpDesignation, "Employee Designation list")
        Me.lstEmpDesignation.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstEmpShiftGroup
        '
        Me.lstEmpShiftGroup.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstEmpShiftGroup.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstEmpShiftGroup.BackColor = System.Drawing.Color.Transparent
        Me.lstEmpShiftGroup.disableWhenChecked = False
        Me.lstEmpShiftGroup.HeadingLabelName = "lstShiftGroup"
        Me.lstEmpShiftGroup.HeadingText = "Shift Group"
        Me.lstEmpShiftGroup.Location = New System.Drawing.Point(325, 3)
        Me.lstEmpShiftGroup.Name = "lstEmpShiftGroup"
        Me.lstEmpShiftGroup.ShowAddNewButton = False
        Me.lstEmpShiftGroup.ShowInverse = True
        Me.lstEmpShiftGroup.ShowMagnifierButton = False
        Me.lstEmpShiftGroup.ShowNoCheck = False
        Me.lstEmpShiftGroup.ShowResetAllButton = False
        Me.lstEmpShiftGroup.ShowSelectall = True
        Me.lstEmpShiftGroup.Size = New System.Drawing.Size(152, 159)
        Me.lstEmpShiftGroup.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.lstEmpShiftGroup, "Employee Shift Group list")
        Me.lstEmpShiftGroup.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstEmpCity
        '
        Me.lstEmpCity.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstEmpCity.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstEmpCity.BackColor = System.Drawing.Color.Transparent
        Me.lstEmpCity.disableWhenChecked = False
        Me.lstEmpCity.HeadingLabelName = "lblCity"
        Me.lstEmpCity.HeadingText = "City"
        Me.lstEmpCity.Location = New System.Drawing.Point(483, 3)
        Me.lstEmpCity.Name = "lstEmpCity"
        Me.lstEmpCity.ShowAddNewButton = False
        Me.lstEmpCity.ShowInverse = True
        Me.lstEmpCity.ShowMagnifierButton = False
        Me.lstEmpCity.ShowNoCheck = False
        Me.lstEmpCity.ShowResetAllButton = False
        Me.lstEmpCity.ShowSelectall = True
        Me.lstEmpCity.Size = New System.Drawing.Size(152, 159)
        Me.lstEmpCity.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.lstEmpCity, "Employee City list")
        Me.lstEmpCity.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grdSaved)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(767, 517)
        '
        'grdSaved
        '
        Me.grdSaved.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdSaved.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSaved.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdSaved.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdSaved.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdSaved.Location = New System.Drawing.Point(0, 0)
        Me.grdSaved.Name = "grdSaved"
        Me.grdSaved.RecordNavigator = True
        Me.grdSaved.Size = New System.Drawing.Size(767, 517)
        Me.grdSaved.TabIndex = 0
        Me.grdSaved.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdSaved.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnPrint, Me.toolStripSeparator1, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(769, 25)
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
        'btnPrint
        '
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(52, 22)
        Me.btnPrint.Text = "&Print"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnHelp
        '
        Me.btnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(23, 22)
        Me.btnHelp.Text = "He&lp"
        Me.btnHelp.Visible = False
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 25)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(769, 538)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 11
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Attendence"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "History"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(767, 517)
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(735, 0)
        Me.CtrlGrdBar1.MyGrid = Me.grdSaved
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(33, 25)
        Me.CtrlGrdBar1.TabIndex = 1
        '
        'frmEmpAttendenceInOutMissing
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(769, 563)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "frmEmpAttendenceInOutMissing"
        Me.Text = "Employee Daily Attendence"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlPeriod.ResumeLayout(False)
        Me.pnlPeriod.PerformLayout()
        Me.pnlEmpAttendence.ResumeLayout(False)
        Me.pnlEmpAttendence.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grdSaved As Janus.Windows.GridEX.GridEX
    Friend WithEvents pnlEmpAttendence As System.Windows.Forms.Panel
    Friend WithEvents lstCostCenter As SimpleAccounts.uiListControl
    Friend WithEvents lstHeadCostCenter As SimpleAccounts.uiListControl
    Friend WithEvents lstEmployee As SimpleAccounts.uiListControl
    Friend WithEvents lstEmpDepartment As SimpleAccounts.uiListControl
    Friend WithEvents lstEmpDesignation As SimpleAccounts.uiListControl
    Friend WithEvents lstEmpShiftGroup As SimpleAccounts.uiListControl
    Friend WithEvents lstEmpCity As SimpleAccounts.uiListControl
    Friend WithEvents pnlPeriod As System.Windows.Forms.Panel
    Friend WithEvents lblToDate As System.Windows.Forms.Label
    Friend WithEvents lblFromDate As System.Windows.Forms.Label
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents lblPeriod As System.Windows.Forms.Label
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents lblProgress As System.Windows.Forms.Label
End Class

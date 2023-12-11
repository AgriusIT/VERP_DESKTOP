<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGrdRptGenerelEmployeeSalary
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGrdRptGenerelEmployeeSalary))
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim UltraTab3 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab4 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlLstBox = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lstCostCenter = New SimpleAccounts.uiListControl()
        Me.lstHeadCostCenter = New SimpleAccounts.uiListControl()
        Me.lstEmployee = New SimpleAccounts.uiListControl()
        Me.btnLateDeductions = New System.Windows.Forms.Button()
        Me.lstEmpDepartment = New SimpleAccounts.uiListControl()
        Me.lstEmpDesignation = New SimpleAccounts.uiListControl()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.lstEmpShiftGroup = New SimpleAccounts.uiListControl()
        Me.lstEmpCity = New SimpleAccounts.uiListControl()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.pnlPeriod = New System.Windows.Forms.Panel()
        Me.rbtnNightShift = New System.Windows.Forms.RadioButton()
        Me.rbtnNormalShift = New System.Windows.Forms.RadioButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbMonth = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtYear = New System.Windows.Forms.TextBox()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.grdEmployee = New Janus.Windows.GridEX.GridEX()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.pnlLstBox.SuspendLayout()
        Me.pnlPeriod.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.grdEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlLstBox)
        Me.UltraTabPageControl1.Controls.Add(Me.pnlPeriod)
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1258, 740)
        '
        'pnlLstBox
        '
        Me.pnlLstBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlLstBox.BackColor = System.Drawing.Color.Transparent
        Me.pnlLstBox.Controls.Add(Me.Label4)
        Me.pnlLstBox.Controls.Add(Me.lstCostCenter)
        Me.pnlLstBox.Controls.Add(Me.lstHeadCostCenter)
        Me.pnlLstBox.Controls.Add(Me.lstEmployee)
        Me.pnlLstBox.Controls.Add(Me.btnLateDeductions)
        Me.pnlLstBox.Controls.Add(Me.lstEmpDepartment)
        Me.pnlLstBox.Controls.Add(Me.lstEmpDesignation)
        Me.pnlLstBox.Controls.Add(Me.btnShow)
        Me.pnlLstBox.Controls.Add(Me.lstEmpShiftGroup)
        Me.pnlLstBox.Controls.Add(Me.lstEmpCity)
        Me.pnlLstBox.Controls.Add(Me.txtSearch)
        Me.pnlLstBox.Location = New System.Drawing.Point(4, 143)
        Me.pnlLstBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlLstBox.Name = "pnlLstBox"
        Me.pnlLstBox.Size = New System.Drawing.Size(1153, 557)
        Me.pnlLstBox.TabIndex = 115
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(4, 509)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(97, 20)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Emp Search"
        Me.ToolTip1.SetToolTip(Me.Label4, "employee search")
        '
        'lstCostCenter
        '
        Me.lstCostCenter.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstCostCenter.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstCostCenter.BackColor = System.Drawing.Color.Transparent
        Me.lstCostCenter.disableWhenChecked = False
        Me.lstCostCenter.HeadingLabelName = "lstCostCenter"
        Me.lstCostCenter.HeadingText = "Cost Center"
        Me.lstCostCenter.Location = New System.Drawing.Point(828, 258)
        Me.lstCostCenter.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstCostCenter.Name = "lstCostCenter"
        Me.lstCostCenter.ShowAddNewButton = False
        Me.lstCostCenter.ShowInverse = True
        Me.lstCostCenter.ShowMagnifierButton = False
        Me.lstCostCenter.ShowNoCheck = False
        Me.lstCostCenter.ShowResetAllButton = False
        Me.lstCostCenter.ShowSelectall = True
        Me.lstCostCenter.Size = New System.Drawing.Size(228, 245)
        Me.lstCostCenter.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.lstCostCenter, "employee cost center list")
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
        Me.lstHeadCostCenter.Location = New System.Drawing.Point(560, 258)
        Me.lstHeadCostCenter.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstHeadCostCenter.Name = "lstHeadCostCenter"
        Me.lstHeadCostCenter.ShowAddNewButton = False
        Me.lstHeadCostCenter.ShowInverse = True
        Me.lstHeadCostCenter.ShowMagnifierButton = False
        Me.lstHeadCostCenter.ShowNoCheck = False
        Me.lstHeadCostCenter.ShowResetAllButton = False
        Me.lstHeadCostCenter.ShowSelectall = True
        Me.lstHeadCostCenter.Size = New System.Drawing.Size(228, 245)
        Me.lstHeadCostCenter.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.lstHeadCostCenter, "employee head cost center list")
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
        Me.lstEmployee.Location = New System.Drawing.Point(14, 258)
        Me.lstEmployee.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstEmployee.Name = "lstEmployee"
        Me.lstEmployee.ShowAddNewButton = False
        Me.lstEmployee.ShowInverse = True
        Me.lstEmployee.ShowMagnifierButton = False
        Me.lstEmployee.ShowNoCheck = False
        Me.lstEmployee.ShowResetAllButton = False
        Me.lstEmployee.ShowSelectall = True
        Me.lstEmployee.Size = New System.Drawing.Size(502, 245)
        Me.lstEmployee.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.lstEmployee, "emplyee list")
        Me.lstEmployee.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'btnLateDeductions
        '
        Me.btnLateDeductions.Location = New System.Drawing.Point(756, 505)
        Me.btnLateDeductions.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnLateDeductions.Name = "btnLateDeductions"
        Me.btnLateDeductions.Size = New System.Drawing.Size(268, 35)
        Me.btnLateDeductions.TabIndex = 5
        Me.btnLateDeductions.Text = "Save Leave and Late Deductions"
        Me.btnLateDeductions.UseVisualStyleBackColor = True
        '
        'lstEmpDepartment
        '
        Me.lstEmpDepartment.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstEmpDepartment.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstEmpDepartment.BackColor = System.Drawing.Color.Transparent
        Me.lstEmpDepartment.disableWhenChecked = False
        Me.lstEmpDepartment.HeadingLabelName = "lstEmpDepartment"
        Me.lstEmpDepartment.HeadingText = "Department"
        Me.lstEmpDepartment.Location = New System.Drawing.Point(18, 5)
        Me.lstEmpDepartment.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstEmpDepartment.Name = "lstEmpDepartment"
        Me.lstEmpDepartment.ShowAddNewButton = False
        Me.lstEmpDepartment.ShowInverse = True
        Me.lstEmpDepartment.ShowMagnifierButton = False
        Me.lstEmpDepartment.ShowNoCheck = False
        Me.lstEmpDepartment.ShowResetAllButton = False
        Me.lstEmpDepartment.ShowSelectall = True
        Me.lstEmpDepartment.Size = New System.Drawing.Size(228, 245)
        Me.lstEmpDepartment.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.lstEmpDepartment, "employee department list")
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
        Me.lstEmpDesignation.Location = New System.Drawing.Point(290, 5)
        Me.lstEmpDesignation.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstEmpDesignation.Name = "lstEmpDesignation"
        Me.lstEmpDesignation.ShowAddNewButton = False
        Me.lstEmpDesignation.ShowInverse = True
        Me.lstEmpDesignation.ShowMagnifierButton = False
        Me.lstEmpDesignation.ShowNoCheck = False
        Me.lstEmpDesignation.ShowResetAllButton = False
        Me.lstEmpDesignation.ShowSelectall = True
        Me.lstEmpDesignation.Size = New System.Drawing.Size(228, 245)
        Me.lstEmpDesignation.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.lstEmpDesignation, "employee designation list")
        Me.lstEmpDesignation.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(624, 505)
        Me.btnShow.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(118, 35)
        Me.btnShow.TabIndex = 4
        Me.btnShow.Text = "Load"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'lstEmpShiftGroup
        '
        Me.lstEmpShiftGroup.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstEmpShiftGroup.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstEmpShiftGroup.BackColor = System.Drawing.Color.Transparent
        Me.lstEmpShiftGroup.disableWhenChecked = False
        Me.lstEmpShiftGroup.HeadingLabelName = "lstShiftGroup"
        Me.lstEmpShiftGroup.HeadingText = "Shift Group"
        Me.lstEmpShiftGroup.Location = New System.Drawing.Point(560, 5)
        Me.lstEmpShiftGroup.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstEmpShiftGroup.Name = "lstEmpShiftGroup"
        Me.lstEmpShiftGroup.ShowAddNewButton = False
        Me.lstEmpShiftGroup.ShowInverse = True
        Me.lstEmpShiftGroup.ShowMagnifierButton = False
        Me.lstEmpShiftGroup.ShowNoCheck = False
        Me.lstEmpShiftGroup.ShowResetAllButton = False
        Me.lstEmpShiftGroup.ShowSelectall = True
        Me.lstEmpShiftGroup.Size = New System.Drawing.Size(228, 245)
        Me.lstEmpShiftGroup.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.lstEmpShiftGroup, "employee shift group list")
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
        Me.lstEmpCity.Location = New System.Drawing.Point(828, 5)
        Me.lstEmpCity.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstEmpCity.Name = "lstEmpCity"
        Me.lstEmpCity.ShowAddNewButton = False
        Me.lstEmpCity.ShowInverse = True
        Me.lstEmpCity.ShowMagnifierButton = False
        Me.lstEmpCity.ShowNoCheck = False
        Me.lstEmpCity.ShowResetAllButton = False
        Me.lstEmpCity.ShowSelectall = True
        Me.lstEmpCity.Size = New System.Drawing.Size(228, 245)
        Me.lstEmpCity.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.lstEmpCity, "employee city list")
        Me.lstEmpCity.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.White
        Me.txtSearch.Location = New System.Drawing.Point(104, 505)
        Me.txtSearch.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(370, 26)
        Me.txtSearch.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.txtSearch, "search employee by name")
        '
        'pnlPeriod
        '
        Me.pnlPeriod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlPeriod.BackColor = System.Drawing.Color.Transparent
        Me.pnlPeriod.Controls.Add(Me.rbtnNightShift)
        Me.pnlPeriod.Controls.Add(Me.rbtnNormalShift)
        Me.pnlPeriod.Controls.Add(Me.Label3)
        Me.pnlPeriod.Controls.Add(Me.cmbMonth)
        Me.pnlPeriod.Controls.Add(Me.Label2)
        Me.pnlPeriod.Controls.Add(Me.txtYear)
        Me.pnlPeriod.Location = New System.Drawing.Point(4, 78)
        Me.pnlPeriod.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlPeriod.Name = "pnlPeriod"
        Me.pnlPeriod.Size = New System.Drawing.Size(1153, 55)
        Me.pnlPeriod.TabIndex = 114
        '
        'rbtnNightShift
        '
        Me.rbtnNightShift.AutoSize = True
        Me.rbtnNightShift.BackColor = System.Drawing.Color.Transparent
        Me.rbtnNightShift.Location = New System.Drawing.Point(756, 15)
        Me.rbtnNightShift.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtnNightShift.Name = "rbtnNightShift"
        Me.rbtnNightShift.Size = New System.Drawing.Size(108, 24)
        Me.rbtnNightShift.TabIndex = 29
        Me.rbtnNightShift.Text = "Night Shift"
        Me.ToolTip1.SetToolTip(Me.rbtnNightShift, "employee night working shift")
        Me.rbtnNightShift.UseVisualStyleBackColor = False
        '
        'rbtnNormalShift
        '
        Me.rbtnNormalShift.AutoSize = True
        Me.rbtnNormalShift.BackColor = System.Drawing.Color.Transparent
        Me.rbtnNormalShift.Checked = True
        Me.rbtnNormalShift.Location = New System.Drawing.Point(624, 15)
        Me.rbtnNormalShift.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtnNormalShift.Name = "rbtnNormalShift"
        Me.rbtnNormalShift.Size = New System.Drawing.Size(121, 24)
        Me.rbtnNormalShift.TabIndex = 28
        Me.rbtnNormalShift.TabStop = True
        Me.rbtnNormalShift.Text = "Normal Shift"
        Me.ToolTip1.SetToolTip(Me.rbtnNormalShift, "employee normal working shift")
        Me.rbtnNormalShift.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 14)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 20)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Year"
        '
        'cmbMonth
        '
        Me.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMonth.FormattingEnabled = True
        Me.cmbMonth.Items.AddRange(New Object() {"January", "February", "March", "April", "May", "June", "Jully", "August", "September", "October", "November", "December"})
        Me.cmbMonth.Location = New System.Drawing.Point(376, 14)
        Me.cmbMonth.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbMonth.Name = "cmbMonth"
        Me.cmbMonth.Size = New System.Drawing.Size(236, 28)
        Me.cmbMonth.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(312, 14)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(54, 20)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Month"
        '
        'txtYear
        '
        Me.txtYear.Location = New System.Drawing.Point(64, 14)
        Me.txtYear.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtYear.Name = "txtYear"
        Me.txtYear.Size = New System.Drawing.Size(236, 26)
        Me.txtYear.TabIndex = 2
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnClose)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1258, 65)
        Me.pnlHeader.TabIndex = 113
        Me.ToolTip1.SetToolTip(Me.pnlHeader, "title")
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.btnClose.Location = New System.Drawing.Point(1186, 9)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(54, 46)
        Me.btnClose.TabIndex = 116
        Me.ToolTip1.SetToolTip(Me.btnClose, "close")
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(15, 11)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(395, 41)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Employee Salaries Detail"
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.GridEX1)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1258, 740)
        '
        'GridEX1
        '
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        GridEX1_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX1.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.GridEX1.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.Location = New System.Drawing.Point(0, 0)
        Me.GridEX1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.Size = New System.Drawing.Size(1258, 740)
        Me.GridEX1.TabIndex = 1
        Me.GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1260, 38)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 35)
        Me.btnRefresh.Text = "Refresh"
        '
        'grdEmployee
        '
        Me.grdEmployee.Location = New System.Drawing.Point(0, 0)
        Me.grdEmployee.Name = "grdEmployee"
        Me.grdEmployee.Size = New System.Drawing.Size(400, 376)
        Me.grdEmployee.TabIndex = 0
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(1203, 0)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar1.MyGrid = Me.GridEX1
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(57, 38)
        Me.CtrlGrdBar1.TabIndex = 5
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1258, 740)
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 38)
        Me.UltraTabControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(1260, 767)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 114
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab3.TabPage = Me.UltraTabPageControl1
        UltraTab3.Text = "Detail"
        UltraTab4.TabPage = Me.UltraTabPageControl2
        UltraTab4.Text = "History"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab3, UltraTab4})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.VisualStudio2005
        '
        'frmGrdRptGenerelEmployeeSalary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1260, 805)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmGrdRptGenerelEmployeeSalary"
        Me.Text = "Employee Salaries Detail"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.pnlLstBox.ResumeLayout(False)
        Me.pnlLstBox.PerformLayout()
        Me.pnlPeriod.ResumeLayout(False)
        Me.pnlPeriod.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.grdEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents grdEmployee As Janus.Windows.GridEX.GridEX
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents btnLateDeductions As System.Windows.Forms.Button
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtYear As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbMonth As System.Windows.Forms.ComboBox
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents pnlPeriod As System.Windows.Forms.Panel
    Friend WithEvents rbtnNightShift As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnNormalShift As System.Windows.Forms.RadioButton
    Friend WithEvents pnlLstBox As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lstCostCenter As SimpleAccounts.uiListControl
    Friend WithEvents lstHeadCostCenter As SimpleAccounts.uiListControl
    Friend WithEvents lstEmployee As SimpleAccounts.uiListControl
    Friend WithEvents lstEmpDepartment As SimpleAccounts.uiListControl
    Friend WithEvents lstEmpDesignation As SimpleAccounts.uiListControl
    Friend WithEvents lstEmpShiftGroup As SimpleAccounts.uiListControl
    Friend WithEvents lstEmpCity As SimpleAccounts.uiListControl
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
End Class

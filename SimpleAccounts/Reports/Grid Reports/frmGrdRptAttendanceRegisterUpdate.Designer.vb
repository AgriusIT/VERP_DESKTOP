<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGrdRptAttendanceRegisterUpdate
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
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.lblShift = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.cmbShift = New System.Windows.Forms.ComboBox()
        Me.lblResultTime = New System.Windows.Forms.Label()
        Me.cmbDepartment = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtnNightShift = New System.Windows.Forms.RadioButton()
        Me.rbtnNormalShift = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lstCostCenter = New SimpleAccounts.uiListControl()
        Me.lstHeadCostCenter = New SimpleAccounts.uiListControl()
        Me.lstEmployee = New SimpleAccounts.uiListControl()
        Me.lstEmpDepartment = New SimpleAccounts.uiListControl()
        Me.lstEmpDesignation = New SimpleAccounts.uiListControl()
        Me.lstEmpShiftGroup = New SimpleAccounts.uiListControl()
        Me.lstEmpCity = New SimpleAccounts.uiListControl()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.lblShift)
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbShift)
        Me.UltraTabPageControl1.Controls.Add(Me.lblResultTime)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbDepartment)
        Me.UltraTabPageControl1.Controls.Add(Me.Label4)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox1)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(892, 546)
        '
        'lblShift
        '
        Me.lblShift.AutoSize = True
        Me.lblShift.BackColor = System.Drawing.Color.Transparent
        Me.lblShift.Location = New System.Drawing.Point(26, 534)
        Me.lblShift.Name = "lblShift"
        Me.lblShift.Size = New System.Drawing.Size(50, 20)
        Me.lblShift.TabIndex = 6
        Me.lblShift.Text = "Shift"
        Me.lblShift.Visible = False
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(892, 41)
        Me.pnlHeader.TabIndex = 0
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(7, 7)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(400, 36)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Attendance Register Update"
        '
        'cmbShift
        '
        Me.cmbShift.FormattingEnabled = True
        Me.cmbShift.Location = New System.Drawing.Point(356, 518)
        Me.cmbShift.Name = "cmbShift"
        Me.cmbShift.Size = New System.Drawing.Size(143, 28)
        Me.cmbShift.TabIndex = 7
        Me.cmbShift.Visible = False
        '
        'lblResultTime
        '
        Me.lblResultTime.AutoSize = True
        Me.lblResultTime.BackColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.lblResultTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.lblResultTime.Location = New System.Drawing.Point(399, 20)
        Me.lblResultTime.Name = "lblResultTime"
        Me.lblResultTime.Size = New System.Drawing.Size(111, 20)
        Me.lblResultTime.TabIndex = 5
        Me.lblResultTime.Text = "Result Time"
        Me.lblResultTime.Visible = False
        '
        'cmbDepartment
        '
        Me.cmbDepartment.FormattingEnabled = True
        Me.cmbDepartment.Location = New System.Drawing.Point(207, 534)
        Me.cmbDepartment.Name = "cmbDepartment"
        Me.cmbDepartment.Size = New System.Drawing.Size(143, 28)
        Me.cmbDepartment.TabIndex = 5
        Me.cmbDepartment.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(94, 521)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(112, 20)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Department"
        Me.Label4.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.rbtnNightShift)
        Me.GroupBox1.Controls.Add(Me.rbtnNormalShift)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.lstCostCenter)
        Me.GroupBox1.Controls.Add(Me.lstHeadCostCenter)
        Me.GroupBox1.Controls.Add(Me.lstEmployee)
        Me.GroupBox1.Controls.Add(Me.lstEmpDepartment)
        Me.GroupBox1.Controls.Add(Me.lstEmpDesignation)
        Me.GroupBox1.Controls.Add(Me.lstEmpShiftGroup)
        Me.GroupBox1.Controls.Add(Me.lstEmpCity)
        Me.GroupBox1.Controls.Add(Me.txtSearch)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtpToDate)
        Me.GroupBox1.Controls.Add(Me.dtpFromDate)
        Me.GroupBox1.Controls.Add(Me.btnShow)
        Me.GroupBox1.Location = New System.Drawing.Point(11, 60)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(867, 437)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Criteria"
        '
        'rbtnNightShift
        '
        Me.rbtnNightShift.AutoSize = True
        Me.rbtnNightShift.BackColor = System.Drawing.Color.Transparent
        Me.rbtnNightShift.Location = New System.Drawing.Point(546, 29)
        Me.rbtnNightShift.Name = "rbtnNightShift"
        Me.rbtnNightShift.Size = New System.Drawing.Size(128, 24)
        Me.rbtnNightShift.TabIndex = 37
        Me.rbtnNightShift.Text = "Night Shift"
        Me.rbtnNightShift.UseVisualStyleBackColor = False
        '
        'rbtnNormalShift
        '
        Me.rbtnNormalShift.AutoSize = True
        Me.rbtnNormalShift.BackColor = System.Drawing.Color.Transparent
        Me.rbtnNormalShift.Checked = True
        Me.rbtnNormalShift.Location = New System.Drawing.Point(444, 29)
        Me.rbtnNormalShift.Name = "rbtnNormalShift"
        Me.rbtnNormalShift.Size = New System.Drawing.Size(143, 24)
        Me.rbtnNormalShift.TabIndex = 36
        Me.rbtnNormalShift.TabStop = True
        Me.rbtnNormalShift.Text = "Normal Shift"
        Me.rbtnNormalShift.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 380)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(113, 20)
        Me.Label1.TabIndex = 16
        Me.Label1.Text = "Emp Search"
        '
        'lstCostCenter
        '
        Me.lstCostCenter.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstCostCenter.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstCostCenter.BackColor = System.Drawing.Color.Transparent
        Me.lstCostCenter.disableWhenChecked = False
        Me.lstCostCenter.HeadingLabelName = "lstCostCenter"
        Me.lstCostCenter.HeadingText = "Cost Center"
        Me.lstCostCenter.Location = New System.Drawing.Point(645, 217)
        Me.lstCostCenter.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.lstCostCenter.Name = "lstCostCenter"
        Me.lstCostCenter.ShowAddNewButton = False
        Me.lstCostCenter.ShowInverse = True
        Me.lstCostCenter.ShowMagnifierButton = False
        Me.lstCostCenter.ShowNoCheck = False
        Me.lstCostCenter.ShowResetAllButton = False
        Me.lstCostCenter.ShowSelectall = True
        Me.lstCostCenter.Size = New System.Drawing.Size(177, 159)
        Me.lstCostCenter.TabIndex = 15
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
        Me.lstHeadCostCenter.Location = New System.Drawing.Point(436, 217)
        Me.lstHeadCostCenter.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.lstHeadCostCenter.Name = "lstHeadCostCenter"
        Me.lstHeadCostCenter.ShowAddNewButton = False
        Me.lstHeadCostCenter.ShowInverse = True
        Me.lstHeadCostCenter.ShowMagnifierButton = False
        Me.lstHeadCostCenter.ShowNoCheck = False
        Me.lstHeadCostCenter.ShowResetAllButton = False
        Me.lstHeadCostCenter.ShowSelectall = True
        Me.lstHeadCostCenter.Size = New System.Drawing.Size(177, 159)
        Me.lstHeadCostCenter.TabIndex = 14
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
        Me.lstEmployee.Location = New System.Drawing.Point(11, 217)
        Me.lstEmployee.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.lstEmployee.Name = "lstEmployee"
        Me.lstEmployee.ShowAddNewButton = False
        Me.lstEmployee.ShowInverse = True
        Me.lstEmployee.ShowMagnifierButton = False
        Me.lstEmployee.ShowNoCheck = False
        Me.lstEmployee.ShowResetAllButton = False
        Me.lstEmployee.ShowSelectall = True
        Me.lstEmployee.Size = New System.Drawing.Size(391, 159)
        Me.lstEmployee.TabIndex = 13
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
        Me.lstEmpDepartment.Location = New System.Drawing.Point(15, 52)
        Me.lstEmpDepartment.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.lstEmpDepartment.Name = "lstEmpDepartment"
        Me.lstEmpDepartment.ShowAddNewButton = False
        Me.lstEmpDepartment.ShowInverse = True
        Me.lstEmpDepartment.ShowMagnifierButton = False
        Me.lstEmpDepartment.ShowNoCheck = False
        Me.lstEmpDepartment.ShowResetAllButton = False
        Me.lstEmpDepartment.ShowSelectall = True
        Me.lstEmpDepartment.Size = New System.Drawing.Size(177, 159)
        Me.lstEmpDepartment.TabIndex = 9
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
        Me.lstEmpDesignation.Location = New System.Drawing.Point(226, 52)
        Me.lstEmpDesignation.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.lstEmpDesignation.Name = "lstEmpDesignation"
        Me.lstEmpDesignation.ShowAddNewButton = False
        Me.lstEmpDesignation.ShowInverse = True
        Me.lstEmpDesignation.ShowMagnifierButton = False
        Me.lstEmpDesignation.ShowNoCheck = False
        Me.lstEmpDesignation.ShowResetAllButton = False
        Me.lstEmpDesignation.ShowSelectall = True
        Me.lstEmpDesignation.Size = New System.Drawing.Size(177, 159)
        Me.lstEmpDesignation.TabIndex = 10
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
        Me.lstEmpShiftGroup.Location = New System.Drawing.Point(436, 52)
        Me.lstEmpShiftGroup.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.lstEmpShiftGroup.Name = "lstEmpShiftGroup"
        Me.lstEmpShiftGroup.ShowAddNewButton = False
        Me.lstEmpShiftGroup.ShowInverse = True
        Me.lstEmpShiftGroup.ShowMagnifierButton = False
        Me.lstEmpShiftGroup.ShowNoCheck = False
        Me.lstEmpShiftGroup.ShowResetAllButton = False
        Me.lstEmpShiftGroup.ShowSelectall = True
        Me.lstEmpShiftGroup.Size = New System.Drawing.Size(177, 159)
        Me.lstEmpShiftGroup.TabIndex = 11
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
        Me.lstEmpCity.Location = New System.Drawing.Point(645, 52)
        Me.lstEmpCity.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.lstEmpCity.Name = "lstEmpCity"
        Me.lstEmpCity.ShowAddNewButton = False
        Me.lstEmpCity.ShowInverse = True
        Me.lstEmpCity.ShowMagnifierButton = False
        Me.lstEmpCity.ShowNoCheck = False
        Me.lstEmpCity.ShowResetAllButton = False
        Me.lstEmpCity.ShowSelectall = True
        Me.lstEmpCity.Size = New System.Drawing.Size(177, 159)
        Me.lstEmpCity.TabIndex = 12
        Me.lstEmpCity.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.White
        Me.txtSearch.Location = New System.Drawing.Point(83, 377)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(235, 28)
        Me.txtSearch.TabIndex = 17
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(232, 29)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 20)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Date To"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 29)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 20)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Date From"
        '
        'dtpToDate
        '
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(289, 25)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(143, 28)
        Me.dtpToDate.TabIndex = 3
        '
        'dtpFromDate
        '
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(83, 25)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(143, 28)
        Me.dtpFromDate.TabIndex = 1
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(329, 377)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(75, 23)
        Me.btnShow.TabIndex = 8
        Me.btnShow.Text = "Show"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grd)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(892, 546)
        '
        'grd
        '
        Me.grd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.grd.Location = New System.Drawing.Point(0, 0)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(892, 546)
        Me.grd.TabIndex = 0
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh, Me.btnSave})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(894, 32)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 29)
        Me.btnRefresh.Text = "&Refresh"
        '
        'btnSave
        '
        Me.btnSave.Image = Global.SimpleAccounts.My.Resources.Resources.save_edit
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(77, 29)
        Me.btnSave.Text = "&Save"
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 32)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(894, 574)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 2
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Criteria"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "Report"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.VisualStudio2005
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(892, 546)
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(856, 0)
        Me.CtrlGrdBar1.MyGrid = Me.grd
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(38, 25)
        Me.CtrlGrdBar1.TabIndex = 1
        '
        'frmGrdRptAttendanceRegisterUpdate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(894, 606)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmGrdRptAttendanceRegisterUpdate"
        Me.Text = "Attendance Register Update"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblResultTime As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbDepartment As System.Windows.Forms.ComboBox
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblShift As System.Windows.Forms.Label
    Friend WithEvents cmbShift As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lstCostCenter As SimpleAccounts.uiListControl
    Friend WithEvents lstHeadCostCenter As SimpleAccounts.uiListControl
    Friend WithEvents lstEmployee As SimpleAccounts.uiListControl
    Friend WithEvents lstEmpDepartment As SimpleAccounts.uiListControl
    Friend WithEvents lstEmpDesignation As SimpleAccounts.uiListControl
    Friend WithEvents lstEmpShiftGroup As SimpleAccounts.uiListControl
    Friend WithEvents lstEmpCity As SimpleAccounts.uiListControl
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents rbtnNightShift As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnNormalShift As System.Windows.Forms.RadioButton
End Class

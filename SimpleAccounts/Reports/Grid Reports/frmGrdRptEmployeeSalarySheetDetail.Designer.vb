<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGrdRptEmployeeSalarySheetDetail
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGrdRptEmployeeSalarySheetDetail))
        Dim grdEmployeeSalarySheet_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.pnlLstBox = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lstHeadCostCenter = New SimpleAccounts.uiListControl()
        Me.lstEmployee = New SimpleAccounts.uiListControl()
        Me.lstCostCenter = New SimpleAccounts.uiListControl()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.lstCity = New SimpleAccounts.uiListControl()
        Me.lstDepartment = New SimpleAccounts.uiListControl()
        Me.lstShiftGroup = New SimpleAccounts.uiListControl()
        Me.lstDesignation = New SimpleAccounts.uiListControl()
        Me.pnlPeriod = New System.Windows.Forms.Panel()
        Me.rbtnNightShift = New System.Windows.Forms.RadioButton()
        Me.rbtnNormalShift = New System.Windows.Forms.RadioButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.tblProgressbar = New System.Windows.Forms.ToolStripProgressBar()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdEmployeeSalarySheet = New Janus.Windows.GridEX.GridEX()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.UltraTabPageControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.pnlLstBox.SuspendLayout()
        Me.pnlPeriod.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdEmployeeSalarySheet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip2.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.pnlLstBox)
        Me.UltraTabPageControl1.Controls.Add(Me.pnlPeriod)
        Me.UltraTabPageControl1.Controls.Add(Me.ToolStrip1)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1144, 822)
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnClose)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 32)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1144, 65)
        Me.pnlHeader.TabIndex = 17
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.btnClose.Location = New System.Drawing.Point(1072, 9)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(54, 46)
        Me.btnClose.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.btnClose, "close")
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(10, 11)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(349, 36)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Employee Salary Report"
        Me.ToolTip1.SetToolTip(Me.lblHeader, "title")
        '
        'pnlLstBox
        '
        Me.pnlLstBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlLstBox.BackColor = System.Drawing.Color.Transparent
        Me.pnlLstBox.Controls.Add(Me.Label2)
        Me.pnlLstBox.Controls.Add(Me.txtSearch)
        Me.pnlLstBox.Controls.Add(Me.lstHeadCostCenter)
        Me.pnlLstBox.Controls.Add(Me.lstEmployee)
        Me.pnlLstBox.Controls.Add(Me.lstCostCenter)
        Me.pnlLstBox.Controls.Add(Me.btnShow)
        Me.pnlLstBox.Controls.Add(Me.lstCity)
        Me.pnlLstBox.Controls.Add(Me.lstDepartment)
        Me.pnlLstBox.Controls.Add(Me.lstShiftGroup)
        Me.pnlLstBox.Controls.Add(Me.lstDesignation)
        Me.pnlLstBox.Location = New System.Drawing.Point(6, 232)
        Me.pnlLstBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlLstBox.Name = "pnlLstBox"
        Me.pnlLstBox.Size = New System.Drawing.Size(1119, 569)
        Me.pnlLstBox.TabIndex = 16
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 525)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(97, 20)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "Emp Search"
        Me.ToolTip1.SetToolTip(Me.Label2, "employee search")
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.White
        Me.txtSearch.Location = New System.Drawing.Point(118, 520)
        Me.txtSearch.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(349, 26)
        Me.txtSearch.TabIndex = 23
        Me.ToolTip1.SetToolTip(Me.txtSearch, "search employee by name or code")
        '
        'lstHeadCostCenter
        '
        Me.lstHeadCostCenter.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstHeadCostCenter.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstHeadCostCenter.BackColor = System.Drawing.Color.Transparent
        Me.lstHeadCostCenter.disableWhenChecked = False
        Me.lstHeadCostCenter.HeadingLabelName = "lstHeadCostCenter"
        Me.lstHeadCostCenter.HeadingText = "Head Cost Center"
        Me.lstHeadCostCenter.Location = New System.Drawing.Point(558, 272)
        Me.lstHeadCostCenter.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstHeadCostCenter.Name = "lstHeadCostCenter"
        Me.lstHeadCostCenter.ShowAddNewButton = False
        Me.lstHeadCostCenter.ShowInverse = True
        Me.lstHeadCostCenter.ShowMagnifierButton = False
        Me.lstHeadCostCenter.ShowNoCheck = False
        Me.lstHeadCostCenter.ShowResetAllButton = False
        Me.lstHeadCostCenter.ShowSelectall = True
        Me.lstHeadCostCenter.Size = New System.Drawing.Size(228, 245)
        Me.lstHeadCostCenter.TabIndex = 21
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
        Me.lstEmployee.Location = New System.Drawing.Point(16, 272)
        Me.lstEmployee.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstEmployee.Name = "lstEmployee"
        Me.lstEmployee.ShowAddNewButton = False
        Me.lstEmployee.ShowInverse = True
        Me.lstEmployee.ShowMagnifierButton = False
        Me.lstEmployee.ShowNoCheck = False
        Me.lstEmployee.ShowResetAllButton = False
        Me.lstEmployee.ShowSelectall = True
        Me.lstEmployee.Size = New System.Drawing.Size(496, 245)
        Me.lstEmployee.TabIndex = 20
        Me.ToolTip1.SetToolTip(Me.lstEmployee, "employee name list")
        Me.lstEmployee.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstCostCenter
        '
        Me.lstCostCenter.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstCostCenter.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstCostCenter.BackColor = System.Drawing.Color.Transparent
        Me.lstCostCenter.disableWhenChecked = False
        Me.lstCostCenter.HeadingLabelName = "lstCostCenter"
        Me.lstCostCenter.HeadingText = "Cost Center"
        Me.lstCostCenter.Location = New System.Drawing.Point(843, 272)
        Me.lstCostCenter.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstCostCenter.Name = "lstCostCenter"
        Me.lstCostCenter.ShowAddNewButton = False
        Me.lstCostCenter.ShowInverse = True
        Me.lstCostCenter.ShowMagnifierButton = False
        Me.lstCostCenter.ShowNoCheck = False
        Me.lstCostCenter.ShowResetAllButton = False
        Me.lstCostCenter.ShowSelectall = True
        Me.lstCostCenter.Size = New System.Drawing.Size(228, 245)
        Me.lstCostCenter.TabIndex = 19
        Me.ToolTip1.SetToolTip(Me.lstCostCenter, "employee cost center list")
        Me.lstCostCenter.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(920, 520)
        Me.btnShow.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(112, 37)
        Me.btnShow.TabIndex = 14
        Me.btnShow.Text = "Show"
        Me.ToolTip1.SetToolTip(Me.btnShow, "search employee")
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'lstCity
        '
        Me.lstCity.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstCity.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstCity.BackColor = System.Drawing.Color.Transparent
        Me.lstCity.disableWhenChecked = False
        Me.lstCity.HeadingLabelName = Nothing
        Me.lstCity.HeadingText = "City"
        Me.lstCity.Location = New System.Drawing.Point(843, 18)
        Me.lstCity.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstCity.Name = "lstCity"
        Me.lstCity.ShowAddNewButton = False
        Me.lstCity.ShowInverse = True
        Me.lstCity.ShowMagnifierButton = False
        Me.lstCity.ShowNoCheck = False
        Me.lstCity.ShowResetAllButton = False
        Me.lstCity.ShowSelectall = True
        Me.lstCity.Size = New System.Drawing.Size(228, 245)
        Me.lstCity.TabIndex = 18
        Me.ToolTip1.SetToolTip(Me.lstCity, "employee city list")
        Me.lstCity.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstDepartment
        '
        Me.lstDepartment.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstDepartment.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstDepartment.BackColor = System.Drawing.Color.Transparent
        Me.lstDepartment.disableWhenChecked = False
        Me.lstDepartment.HeadingLabelName = Nothing
        Me.lstDepartment.HeadingText = "Department"
        Me.lstDepartment.Location = New System.Drawing.Point(14, 18)
        Me.lstDepartment.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstDepartment.Name = "lstDepartment"
        Me.lstDepartment.ShowAddNewButton = False
        Me.lstDepartment.ShowInverse = True
        Me.lstDepartment.ShowMagnifierButton = False
        Me.lstDepartment.ShowNoCheck = False
        Me.lstDepartment.ShowResetAllButton = False
        Me.lstDepartment.ShowSelectall = True
        Me.lstDepartment.Size = New System.Drawing.Size(228, 245)
        Me.lstDepartment.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.lstDepartment, "employee department list")
        Me.lstDepartment.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstShiftGroup
        '
        Me.lstShiftGroup.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstShiftGroup.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstShiftGroup.BackColor = System.Drawing.Color.Transparent
        Me.lstShiftGroup.disableWhenChecked = False
        Me.lstShiftGroup.HeadingLabelName = Nothing
        Me.lstShiftGroup.HeadingText = "Shift Group"
        Me.lstShiftGroup.Location = New System.Drawing.Point(558, 18)
        Me.lstShiftGroup.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstShiftGroup.Name = "lstShiftGroup"
        Me.lstShiftGroup.ShowAddNewButton = False
        Me.lstShiftGroup.ShowInverse = True
        Me.lstShiftGroup.ShowMagnifierButton = False
        Me.lstShiftGroup.ShowNoCheck = False
        Me.lstShiftGroup.ShowResetAllButton = False
        Me.lstShiftGroup.ShowSelectall = True
        Me.lstShiftGroup.Size = New System.Drawing.Size(228, 245)
        Me.lstShiftGroup.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.lstShiftGroup, "employee shift group list")
        Me.lstShiftGroup.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstDesignation
        '
        Me.lstDesignation.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstDesignation.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstDesignation.BackColor = System.Drawing.Color.Transparent
        Me.lstDesignation.disableWhenChecked = False
        Me.lstDesignation.HeadingLabelName = Nothing
        Me.lstDesignation.HeadingText = "Designation"
        Me.lstDesignation.Location = New System.Drawing.Point(270, 18)
        Me.lstDesignation.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstDesignation.Name = "lstDesignation"
        Me.lstDesignation.ShowAddNewButton = False
        Me.lstDesignation.ShowInverse = True
        Me.lstDesignation.ShowMagnifierButton = False
        Me.lstDesignation.ShowNoCheck = False
        Me.lstDesignation.ShowResetAllButton = False
        Me.lstDesignation.ShowSelectall = True
        Me.lstDesignation.Size = New System.Drawing.Size(228, 245)
        Me.lstDesignation.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.lstDesignation, "employee designation list")
        Me.lstDesignation.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'pnlPeriod
        '
        Me.pnlPeriod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlPeriod.BackColor = System.Drawing.Color.Transparent
        Me.pnlPeriod.Controls.Add(Me.rbtnNightShift)
        Me.pnlPeriod.Controls.Add(Me.rbtnNormalShift)
        Me.pnlPeriod.Controls.Add(Me.Label6)
        Me.pnlPeriod.Controls.Add(Me.cmbPeriod)
        Me.pnlPeriod.Controls.Add(Me.Label5)
        Me.pnlPeriod.Controls.Add(Me.Label4)
        Me.pnlPeriod.Controls.Add(Me.dtpTo)
        Me.pnlPeriod.Controls.Add(Me.dtpFrom)
        Me.pnlPeriod.Location = New System.Drawing.Point(6, 117)
        Me.pnlPeriod.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlPeriod.Name = "pnlPeriod"
        Me.pnlPeriod.Size = New System.Drawing.Size(1119, 106)
        Me.pnlPeriod.TabIndex = 14
        '
        'rbtnNightShift
        '
        Me.rbtnNightShift.AutoSize = True
        Me.rbtnNightShift.BackColor = System.Drawing.Color.Transparent
        Me.rbtnNightShift.Location = New System.Drawing.Point(668, 58)
        Me.rbtnNightShift.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtnNightShift.Name = "rbtnNightShift"
        Me.rbtnNightShift.Size = New System.Drawing.Size(108, 24)
        Me.rbtnNightShift.TabIndex = 7
        Me.rbtnNightShift.Text = "Night Shift"
        Me.ToolTip1.SetToolTip(Me.rbtnNightShift, "employee night working shift")
        Me.rbtnNightShift.UseVisualStyleBackColor = False
        '
        'rbtnNormalShift
        '
        Me.rbtnNormalShift.AutoSize = True
        Me.rbtnNormalShift.BackColor = System.Drawing.Color.Transparent
        Me.rbtnNormalShift.Checked = True
        Me.rbtnNormalShift.Location = New System.Drawing.Point(536, 58)
        Me.rbtnNormalShift.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtnNormalShift.Name = "rbtnNormalShift"
        Me.rbtnNormalShift.Size = New System.Drawing.Size(121, 24)
        Me.rbtnNormalShift.TabIndex = 6
        Me.rbtnNormalShift.TabStop = True
        Me.rbtnNormalShift.Text = "Normal Shift"
        Me.ToolTip1.SetToolTip(Me.rbtnNormalShift, "employee normal working shift")
        Me.rbtnNormalShift.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(10, 11)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(54, 20)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Period"
        Me.ToolTip1.SetToolTip(Me.Label6, "period")
        '
        'cmbPeriod
        '
        Me.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPeriod.FormattingEnabled = True
        Me.cmbPeriod.Items.AddRange(New Object() {"Today", "Yesterday", "Current Week", "Current Month", "Current Year"})
        Me.cmbPeriod.Location = New System.Drawing.Point(108, 6)
        Me.cmbPeriod.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(416, 28)
        Me.cmbPeriod.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbPeriod, "select period")
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(280, 58)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 20)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Date To"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 58)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 20)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Date From"
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd/MMM/yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(358, 54)
        Me.dtpTo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(166, 26)
        Me.dtpTo.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.dtpTo, "select date to")
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(108, 54)
        Me.dtpFrom.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(162, 26)
        Me.dtpFrom.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.dtpFrom, "select from date")
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh, Me.tblProgressbar})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1144, 32)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 29)
        Me.btnRefresh.Text = "Refresh"
        '
        'tblProgressbar
        '
        Me.tblProgressbar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tblProgressbar.Name = "tblProgressbar"
        Me.tblProgressbar.Size = New System.Drawing.Size(300, 45)
        Me.tblProgressbar.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.tblProgressbar.Visible = False
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grdEmployeeSalarySheet)
        Me.UltraTabPageControl2.Controls.Add(Me.CtrlGrdBar1)
        Me.UltraTabPageControl2.Controls.Add(Me.ToolStrip2)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1144, 822)
        '
        'grdEmployeeSalarySheet
        '
        Me.grdEmployeeSalarySheet.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        grdEmployeeSalarySheet_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grdEmployeeSalarySheet.DesignTimeLayout = grdEmployeeSalarySheet_DesignTimeLayout
        Me.grdEmployeeSalarySheet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdEmployeeSalarySheet.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdEmployeeSalarySheet.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdEmployeeSalarySheet.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdEmployeeSalarySheet.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdEmployeeSalarySheet.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.grdEmployeeSalarySheet.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdEmployeeSalarySheet.Location = New System.Drawing.Point(0, 38)
        Me.grdEmployeeSalarySheet.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdEmployeeSalarySheet.Name = "grdEmployeeSalarySheet"
        Me.grdEmployeeSalarySheet.Size = New System.Drawing.Size(1144, 784)
        Me.grdEmployeeSalarySheet.TabIndex = 0
        Me.grdEmployeeSalarySheet.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdEmployeeSalarySheet.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdEmployeeSalarySheet.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(1089, -2)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar1.MyGrid = Me.grdEmployeeSalarySheet
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(57, 38)
        Me.CtrlGrdBar1.TabIndex = 1
        '
        'ToolStrip2
        '
        Me.ToolStrip2.AutoSize = False
        Me.ToolStrip2.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnPrint})
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip2.Size = New System.Drawing.Size(1144, 38)
        Me.ToolStrip2.TabIndex = 0
        Me.ToolStrip2.Text = "ToolStrip2"
        '
        'btnPrint
        '
        Me.btnPrint.Image = Global.SimpleAccounts.My.Resources.Resources.print
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(177, 35)
        Me.btnPrint.Text = "Print Salary Sheet"
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.UltraTabControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(1146, 849)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 1
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Criteria"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "Result"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.VisualStudio2005
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1144, 822)
        '
        'frmGrdRptEmployeeSalarySheetDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1146, 849)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmGrdRptEmployeeSalarySheetDetail"
        Me.Text = "Employee Salary Sheet"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlLstBox.ResumeLayout(False)
        Me.pnlLstBox.PerformLayout()
        Me.pnlPeriod.ResumeLayout(False)
        Me.pnlPeriod.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdEmployeeSalarySheet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents ToolStrip2 As System.Windows.Forms.ToolStrip
    Friend WithEvents grdEmployeeSalarySheet As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents tblProgressbar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlPeriod As System.Windows.Forms.Panel
    Friend WithEvents rbtnNightShift As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnNormalShift As System.Windows.Forms.RadioButton
    Friend WithEvents pnlLstBox As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents lstHeadCostCenter As SimpleAccounts.uiListControl
    Friend WithEvents lstEmployee As SimpleAccounts.uiListControl
    Friend WithEvents lstCostCenter As SimpleAccounts.uiListControl
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents lstCity As SimpleAccounts.uiListControl
    Friend WithEvents lstDepartment As SimpleAccounts.uiListControl
    Friend WithEvents lstShiftGroup As SimpleAccounts.uiListControl
    Friend WithEvents lstDesignation As SimpleAccounts.uiListControl
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

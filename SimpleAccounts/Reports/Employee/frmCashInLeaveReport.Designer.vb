<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCashInLeaveReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCashInLeaveReport))
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlLstBox = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lstCostCenter = New SimpleAccounts.uiListControl()
        Me.lstHeadCostCenter = New SimpleAccounts.uiListControl()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.lstEmployee = New SimpleAccounts.uiListControl()
        Me.lstEmpDepartment = New SimpleAccounts.uiListControl()
        Me.lstEmpDesignation = New SimpleAccounts.uiListControl()
        Me.lstEmpShiftGroup = New SimpleAccounts.uiListControl()
        Me.lstEmpCity = New SimpleAccounts.uiListControl()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rbtnNightShift = New System.Windows.Forms.RadioButton()
        Me.rbtnNormalShift = New System.Windows.Forms.RadioButton()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdCashInLeave = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.pnlLstBox.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdCashInLeave, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlLstBox)
        Me.UltraTabPageControl1.Controls.Add(Me.Panel1)
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(815, 480)
        '
        'pnlLstBox
        '
        Me.pnlLstBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlLstBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.pnlLstBox.Controls.Add(Me.Label4)
        Me.pnlLstBox.Controls.Add(Me.lstCostCenter)
        Me.pnlLstBox.Controls.Add(Me.lstHeadCostCenter)
        Me.pnlLstBox.Controls.Add(Me.btnShow)
        Me.pnlLstBox.Controls.Add(Me.lstEmployee)
        Me.pnlLstBox.Controls.Add(Me.lstEmpDepartment)
        Me.pnlLstBox.Controls.Add(Me.lstEmpDesignation)
        Me.pnlLstBox.Controls.Add(Me.lstEmpShiftGroup)
        Me.pnlLstBox.Controls.Add(Me.lstEmpCity)
        Me.pnlLstBox.Controls.Add(Me.txtSearch)
        Me.pnlLstBox.Location = New System.Drawing.Point(3, 100)
        Me.pnlLstBox.Name = "pnlLstBox"
        Me.pnlLstBox.Size = New System.Drawing.Size(729, 361)
        Me.pnlLstBox.TabIndex = 22
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 336)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 13)
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
        Me.lstCostCenter.Location = New System.Drawing.Point(552, 168)
        Me.lstCostCenter.Name = "lstCostCenter"
        Me.lstCostCenter.ShowAddNewButton = False
        Me.lstCostCenter.ShowInverse = True
        Me.lstCostCenter.ShowMagnifierButton = False
        Me.lstCostCenter.ShowNoCheck = False
        Me.lstCostCenter.ShowResetAllButton = False
        Me.lstCostCenter.ShowSelectall = True
        Me.lstCostCenter.Size = New System.Drawing.Size(152, 159)
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
        Me.lstHeadCostCenter.Location = New System.Drawing.Point(373, 168)
        Me.lstHeadCostCenter.Name = "lstHeadCostCenter"
        Me.lstHeadCostCenter.ShowAddNewButton = False
        Me.lstHeadCostCenter.ShowInverse = True
        Me.lstHeadCostCenter.ShowMagnifierButton = False
        Me.lstHeadCostCenter.ShowNoCheck = False
        Me.lstHeadCostCenter.ShowResetAllButton = False
        Me.lstHeadCostCenter.ShowSelectall = True
        Me.lstHeadCostCenter.Size = New System.Drawing.Size(152, 159)
        Me.lstHeadCostCenter.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.lstHeadCostCenter, "employee head cost center list")
        Me.lstHeadCostCenter.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(601, 333)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(75, 23)
        Me.btnShow.TabIndex = 11
        Me.btnShow.Text = "Show"
        Me.ToolTip1.SetToolTip(Me.btnShow, "Press this button to show report")
        Me.btnShow.UseVisualStyleBackColor = True
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
        Me.lstEmployee.Size = New System.Drawing.Size(335, 159)
        Me.lstEmployee.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.lstEmployee, "employee  list")
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
        Me.lstEmpDepartment.Location = New System.Drawing.Point(12, 3)
        Me.lstEmpDepartment.Name = "lstEmpDepartment"
        Me.lstEmpDepartment.ShowAddNewButton = False
        Me.lstEmpDepartment.ShowInverse = True
        Me.lstEmpDepartment.ShowMagnifierButton = False
        Me.lstEmpDepartment.ShowNoCheck = False
        Me.lstEmpDepartment.ShowResetAllButton = False
        Me.lstEmpDepartment.ShowSelectall = True
        Me.lstEmpDepartment.Size = New System.Drawing.Size(152, 159)
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
        Me.lstEmpDesignation.Location = New System.Drawing.Point(193, 3)
        Me.lstEmpDesignation.Name = "lstEmpDesignation"
        Me.lstEmpDesignation.ShowAddNewButton = False
        Me.lstEmpDesignation.ShowInverse = True
        Me.lstEmpDesignation.ShowMagnifierButton = False
        Me.lstEmpDesignation.ShowNoCheck = False
        Me.lstEmpDesignation.ShowResetAllButton = False
        Me.lstEmpDesignation.ShowSelectall = True
        Me.lstEmpDesignation.Size = New System.Drawing.Size(152, 159)
        Me.lstEmpDesignation.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.lstEmpDesignation, "employee designation list")
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
        Me.lstEmpShiftGroup.Location = New System.Drawing.Point(373, 3)
        Me.lstEmpShiftGroup.Name = "lstEmpShiftGroup"
        Me.lstEmpShiftGroup.ShowAddNewButton = False
        Me.lstEmpShiftGroup.ShowInverse = True
        Me.lstEmpShiftGroup.ShowMagnifierButton = False
        Me.lstEmpShiftGroup.ShowNoCheck = False
        Me.lstEmpShiftGroup.ShowResetAllButton = False
        Me.lstEmpShiftGroup.ShowSelectall = True
        Me.lstEmpShiftGroup.Size = New System.Drawing.Size(152, 159)
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
        Me.lstEmpCity.Location = New System.Drawing.Point(552, 3)
        Me.lstEmpCity.Name = "lstEmpCity"
        Me.lstEmpCity.ShowAddNewButton = False
        Me.lstEmpCity.ShowInverse = True
        Me.lstEmpCity.ShowMagnifierButton = False
        Me.lstEmpCity.ShowNoCheck = False
        Me.lstEmpCity.ShowResetAllButton = False
        Me.lstEmpCity.ShowSelectall = True
        Me.lstEmpCity.Size = New System.Drawing.Size(152, 159)
        Me.lstEmpCity.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.lstEmpCity, "employee city list")
        Me.lstEmpCity.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.SystemColors.Info
        Me.txtSearch.Location = New System.Drawing.Point(90, 333)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(226, 20)
        Me.txtSearch.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.txtSearch, "search employee by name or code")
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.Panel1.Controls.Add(Me.rbtnNightShift)
        Me.Panel1.Controls.Add(Me.rbtnNormalShift)
        Me.Panel1.Controls.Add(Me.dtpTo)
        Me.Panel1.Controls.Add(Me.dtpFrom)
        Me.Panel1.Controls.Add(Me.lblFrom)
        Me.Panel1.Controls.Add(Me.lblTo)
        Me.Panel1.Location = New System.Drawing.Point(3, 56)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(728, 38)
        Me.Panel1.TabIndex = 21
        '
        'rbtnNightShift
        '
        Me.rbtnNightShift.AutoSize = True
        Me.rbtnNightShift.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.rbtnNightShift.Location = New System.Drawing.Point(388, 12)
        Me.rbtnNightShift.Name = "rbtnNightShift"
        Me.rbtnNightShift.Size = New System.Drawing.Size(74, 17)
        Me.rbtnNightShift.TabIndex = 33
        Me.rbtnNightShift.Text = "Night Shift"
        Me.ToolTip1.SetToolTip(Me.rbtnNightShift, "employee night working shift")
        Me.rbtnNightShift.UseVisualStyleBackColor = True
        '
        'rbtnNormalShift
        '
        Me.rbtnNormalShift.AutoSize = True
        Me.rbtnNormalShift.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.rbtnNormalShift.Checked = True
        Me.rbtnNormalShift.Location = New System.Drawing.Point(300, 12)
        Me.rbtnNormalShift.Name = "rbtnNormalShift"
        Me.rbtnNormalShift.Size = New System.Drawing.Size(82, 17)
        Me.rbtnNormalShift.TabIndex = 32
        Me.rbtnNormalShift.TabStop = True
        Me.rbtnNormalShift.Text = "Normal Shift"
        Me.ToolTip1.SetToolTip(Me.rbtnNormalShift, "employee normal working shift")
        Me.rbtnNormalShift.UseVisualStyleBackColor = True
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd/MMM/yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(188, 12)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(106, 20)
        Me.dtpTo.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.dtpTo, "Select to date")
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(43, 12)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(113, 20)
        Me.dtpFrom.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.dtpFrom, "Select from date")
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.Location = New System.Drawing.Point(7, 12)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(30, 13)
        Me.lblFrom.TabIndex = 5
        Me.lblFrom.Text = "From"
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.Location = New System.Drawing.Point(162, 12)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(20, 13)
        Me.lblTo.TabIndex = 6
        Me.lblTo.Text = "To"
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnClose)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Controls.Add(Me.lblProgress)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(815, 47)
        Me.pnlHeader.TabIndex = 20
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
        Me.btnClose.Location = New System.Drawing.Point(771, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(36, 30)
        Me.btnClose.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.btnClose, "close")
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.lblHeader.Location = New System.Drawing.Point(3, 8)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(142, 24)
        Me.lblHeader.TabIndex = 4
        Me.lblHeader.Text = "Cash In Leave"
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(271, 0)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 19
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grdCashInLeave)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(815, 480)
        '
        'grdCashInLeave
        '
        Me.grdCashInLeave.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdCashInLeave.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdCashInLeave.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdCashInLeave.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdCashInLeave.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdCashInLeave.GroupByBoxVisible = False
        Me.grdCashInLeave.Location = New System.Drawing.Point(0, 0)
        Me.grdCashInLeave.Name = "grdCashInLeave"
        Me.grdCashInLeave.RecordNavigator = True
        Me.grdCashInLeave.Size = New System.Drawing.Size(815, 480)
        Me.grdCashInLeave.TabIndex = 1
        Me.grdCashInLeave.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnRefresh})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(817, 25)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = Global.SimpleAccounts.My.Resources.Resources.BtnNew_Image
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(51, 22)
        Me.btnNew.Text = "&New"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(66, 22)
        Me.btnRefresh.Text = "&Refresh"
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(783, 0)
        Me.CtrlGrdBar1.MyGrid = Me.grdCashInLeave
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(34, 25)
        Me.CtrlGrdBar1.TabIndex = 3
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
        Me.UltraTabControl1.Size = New System.Drawing.Size(817, 501)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 20
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "CashInLeave"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "History"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(815, 480)
        '
        'frmCashInLeaveReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(817, 526)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.KeyPreview = True
        Me.Name = "frmCashInLeaveReport"
        Me.Text = "Cash In Leave Report"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.pnlLstBox.ResumeLayout(False)
        Me.pnlLstBox.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdCashInLeave, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdCashInLeave As Janus.Windows.GridEX.GridEX
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
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
    Friend WithEvents rbtnNightShift As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnNormalShift As System.Windows.Forms.RadioButton
End Class

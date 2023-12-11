<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGrdRptCustomerItemWiseSummary
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
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.rbtByItemName = New System.Windows.Forms.RadioButton()
        Me.rbtByItemCode = New System.Windows.Forms.RadioButton()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.cmbSubCategory = New SimpleAccounts.uiListControl()
        Me.cmbCategory = New SimpleAccounts.uiListControl()
        Me.cmbType = New SimpleAccounts.uiListControl()
        Me.cmbDepartment = New SimpleAccounts.uiListControl()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.ToolStrip3 = New System.Windows.Forms.ToolStrip()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnBack = New System.Windows.Forms.ToolStripButton()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.CtrlGrdBar2 = New SimpleAccounts.CtrlGrdBar()
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
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.rbtByItemName)
        Me.UltraTabPageControl1.Controls.Add(Me.rbtByItemCode)
        Me.UltraTabPageControl1.Controls.Add(Me.btnShow)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbSubCategory)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbCategory)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbType)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbDepartment)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox1)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1218, 991)
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1218, 65)
        Me.pnlHeader.TabIndex = 10
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(12, 15)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(571, 41)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Customer Wise Item Sales Summary"
        '
        'rbtByItemName
        '
        Me.rbtByItemName.AutoSize = True
        Me.rbtByItemName.BackColor = System.Drawing.Color.Transparent
        Me.rbtByItemName.Location = New System.Drawing.Point(226, 275)
        Me.rbtByItemName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtByItemName.Name = "rbtByItemName"
        Me.rbtByItemName.Size = New System.Drawing.Size(134, 24)
        Me.rbtByItemName.TabIndex = 9
        Me.rbtByItemName.Text = "By Item Name"
        Me.rbtByItemName.UseVisualStyleBackColor = False
        '
        'rbtByItemCode
        '
        Me.rbtByItemCode.AutoSize = True
        Me.rbtByItemCode.BackColor = System.Drawing.Color.Transparent
        Me.rbtByItemCode.Checked = True
        Me.rbtByItemCode.Location = New System.Drawing.Point(86, 275)
        Me.rbtByItemCode.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtByItemCode.Name = "rbtByItemCode"
        Me.rbtByItemCode.Size = New System.Drawing.Size(130, 24)
        Me.rbtByItemCode.TabIndex = 8
        Me.rbtByItemCode.TabStop = True
        Me.rbtByItemCode.Text = "By Item Code"
        Me.rbtByItemCode.UseVisualStyleBackColor = False
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(141, 329)
        Me.btnShow.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(222, 35)
        Me.btnShow.TabIndex = 3
        Me.btnShow.Text = "Show"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'cmbSubCategory
        '
        Me.cmbSubCategory.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.cmbSubCategory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbSubCategory.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.cmbSubCategory.BackColor = System.Drawing.Color.Transparent
        Me.cmbSubCategory.disableWhenChecked = False
        Me.cmbSubCategory.HeadingLabelName = Nothing
        Me.cmbSubCategory.HeadingText = "Sub Category"
        Me.cmbSubCategory.Location = New System.Drawing.Point(706, 567)
        Me.cmbSubCategory.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.cmbSubCategory.Name = "cmbSubCategory"
        Me.cmbSubCategory.ShowAddNewButton = False
        Me.cmbSubCategory.ShowInverse = True
        Me.cmbSubCategory.ShowMagnifierButton = False
        Me.cmbSubCategory.ShowNoCheck = False
        Me.cmbSubCategory.ShowResetAllButton = False
        Me.cmbSubCategory.ShowSelectall = True
        Me.cmbSubCategory.Size = New System.Drawing.Size(274, 288)
        Me.cmbSubCategory.TabIndex = 7
        Me.cmbSubCategory.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'cmbCategory
        '
        Me.cmbCategory.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.cmbCategory.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbCategory.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.cmbCategory.BackColor = System.Drawing.Color.Transparent
        Me.cmbCategory.disableWhenChecked = False
        Me.cmbCategory.HeadingLabelName = Nothing
        Me.cmbCategory.HeadingText = "Category"
        Me.cmbCategory.Location = New System.Drawing.Point(386, 567)
        Me.cmbCategory.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.ShowAddNewButton = False
        Me.cmbCategory.ShowInverse = True
        Me.cmbCategory.ShowMagnifierButton = False
        Me.cmbCategory.ShowNoCheck = False
        Me.cmbCategory.ShowResetAllButton = False
        Me.cmbCategory.ShowSelectall = True
        Me.cmbCategory.Size = New System.Drawing.Size(274, 288)
        Me.cmbCategory.TabIndex = 6
        Me.cmbCategory.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'cmbType
        '
        Me.cmbType.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.MDIMain
        Me.cmbType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbType.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.cmbType.BackColor = System.Drawing.Color.Transparent
        Me.cmbType.disableWhenChecked = False
        Me.cmbType.HeadingLabelName = Nothing
        Me.cmbType.HeadingText = "Type"
        Me.cmbType.Location = New System.Drawing.Point(706, 115)
        Me.cmbType.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.cmbType.Name = "cmbType"
        Me.cmbType.ShowAddNewButton = False
        Me.cmbType.ShowInverse = True
        Me.cmbType.ShowMagnifierButton = False
        Me.cmbType.ShowNoCheck = False
        Me.cmbType.ShowResetAllButton = False
        Me.cmbType.ShowSelectall = True
        Me.cmbType.Size = New System.Drawing.Size(274, 422)
        Me.cmbType.TabIndex = 5
        Me.cmbType.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'cmbDepartment
        '
        Me.cmbDepartment.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.MDIMain
        Me.cmbDepartment.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbDepartment.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.cmbDepartment.BackColor = System.Drawing.Color.Transparent
        Me.cmbDepartment.disableWhenChecked = False
        Me.cmbDepartment.HeadingLabelName = Nothing
        Me.cmbDepartment.HeadingText = "Department"
        Me.cmbDepartment.Location = New System.Drawing.Point(386, 115)
        Me.cmbDepartment.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.cmbDepartment.Name = "cmbDepartment"
        Me.cmbDepartment.ShowAddNewButton = False
        Me.cmbDepartment.ShowInverse = True
        Me.cmbDepartment.ShowMagnifierButton = False
        Me.cmbDepartment.ShowNoCheck = False
        Me.cmbDepartment.ShowResetAllButton = False
        Me.cmbDepartment.ShowSelectall = True
        Me.cmbDepartment.Size = New System.Drawing.Size(274, 422)
        Me.cmbDepartment.TabIndex = 4
        Me.cmbDepartment.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cmbPeriod)
        Me.GroupBox1.Controls.Add(Me.lblTo)
        Me.GroupBox1.Controls.Add(Me.lblFrom)
        Me.GroupBox1.Controls.Add(Me.dtpToDate)
        Me.GroupBox1.Controls.Add(Me.dtpFromDate)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 115)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(358, 151)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Date Range"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(15, 31)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 20)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Period"
        '
        'cmbPeriod
        '
        Me.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPeriod.FormattingEnabled = True
        Me.cmbPeriod.Items.AddRange(New Object() {"Today", "Yesterday", "Current Week", "Current Month", "Current Year"})
        Me.cmbPeriod.Location = New System.Drawing.Point(123, 26)
        Me.cmbPeriod.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(220, 28)
        Me.cmbPeriod.TabIndex = 1
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.Location = New System.Drawing.Point(15, 117)
        Me.lblTo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(27, 20)
        Me.lblTo.TabIndex = 4
        Me.lblTo.Text = "To"
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.Location = New System.Drawing.Point(15, 74)
        Me.lblFrom.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(46, 20)
        Me.lblFrom.TabIndex = 2
        Me.lblFrom.Text = "From"
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(123, 108)
        Me.dtpToDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(220, 26)
        Me.dtpToDate.TabIndex = 5
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(123, 68)
        Me.dtpFromDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(220, 26)
        Me.dtpFromDate.TabIndex = 3
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grd)
        Me.UltraTabPageControl2.Controls.Add(Me.CtrlGrdBar1)
        Me.UltraTabPageControl2.Controls.Add(Me.ToolStrip3)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1218, 991)
        '
        'grd
        '
        Me.grd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.GroupByBoxVisible = False
        Me.grd.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.Location = New System.Drawing.Point(0, 0)
        Me.grd.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(1218, 991)
        Me.grd.TabIndex = 2
        Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Nothing
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(1192, 2)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar1.MyGrid = Me.grd
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(57, 38)
        Me.CtrlGrdBar1.TabIndex = 1
        '
        'ToolStrip3
        '
        Me.ToolStrip3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ToolStrip3.AutoSize = False
        Me.ToolStrip3.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip3.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip3.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip3.Name = "ToolStrip3"
        Me.ToolStrip3.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip3.Size = New System.Drawing.Size(1192, 38)
        Me.ToolStrip3.TabIndex = 0
        Me.ToolStrip3.Text = "ToolStrip3"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 29)
        Me.btnRefresh.Text = "Refresh"
        '
        'btnBack
        '
        Me.btnBack.Image = Global.SimpleAccounts.My.Resources.Resources.back
        Me.btnBack.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(52, 22)
        Me.btnBack.Text = "Back"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1220, 32)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 32)
        Me.UltraTabControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(1220, 1018)
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
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1218, 991)
        '
        'CtrlGrdBar2
        '
        Me.CtrlGrdBar2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar2.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar2.Email = Nothing
        Me.CtrlGrdBar2.FormName = Me
        Me.CtrlGrdBar2.Location = New System.Drawing.Point(1162, 0)
        Me.CtrlGrdBar2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar2.MyGrid = Me.grd
        Me.CtrlGrdBar2.Name = "CtrlGrdBar2"
        Me.CtrlGrdBar2.Size = New System.Drawing.Size(57, 38)
        Me.CtrlGrdBar2.TabIndex = 10
        '
        'frmGrdRptCustomerItemWiseSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1220, 1050)
        Me.Controls.Add(Me.CtrlGrdBar2)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmGrdRptCustomerItemWiseSummary"
        Me.Text = "Customer Wise Item Sales Summary"
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
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents cmbSubCategory As SimpleAccounts.uiListControl
    Friend WithEvents cmbCategory As SimpleAccounts.uiListControl
    Friend WithEvents cmbType As SimpleAccounts.uiListControl
    Friend WithEvents cmbDepartment As SimpleAccounts.uiListControl
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents ToolStrip3 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnBack As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents rbtByItemName As System.Windows.Forms.RadioButton
    Friend WithEvents rbtByItemCode As System.Windows.Forms.RadioButton
    Friend WithEvents CtrlGrdBar2 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

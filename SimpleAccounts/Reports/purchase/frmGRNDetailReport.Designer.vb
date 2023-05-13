<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGRNDetailReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGRNDetailReport))
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlLstBox = New System.Windows.Forms.Panel()
        Me.rbtnPack = New System.Windows.Forms.RadioButton()
        Me.rbtnLoose = New System.Windows.Forms.RadioButton()
        Me.btnPrintPreview = New System.Windows.Forms.Button()
        Me.lstSize = New SimpleAccounts.uiListControl()
        Me.lstColor = New SimpleAccounts.uiListControl()
        Me.lstSubCatagory = New SimpleAccounts.uiListControl()
        Me.lstCatagory = New SimpleAccounts.uiListControl()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.lstCostCenter = New SimpleAccounts.uiListControl()
        Me.lstHeadCostCenter = New SimpleAccounts.uiListControl()
        Me.lstItem = New SimpleAccounts.uiListControl()
        Me.lstDepartment = New SimpleAccounts.uiListControl()
        Me.lstType = New SimpleAccounts.uiListControl()
        Me.lstVendorType = New SimpleAccounts.uiListControl()
        Me.lstVendor = New SimpleAccounts.uiListControl()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.pnlPeriod = New System.Windows.Forms.Panel()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        Me.lblToDate = New System.Windows.Forms.Label()
        Me.lblDateFrom = New System.Windows.Forms.Label()
        Me.lblPeriod = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.lblHeader = New System.Windows.Forms.Label()
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
        Me.pnlLstBox.SuspendLayout()
        Me.pnlPeriod.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlLstBox)
        Me.UltraTabPageControl1.Controls.Add(Me.pnlPeriod)
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1049, 520)
        '
        'pnlLstBox
        '
        Me.pnlLstBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlLstBox.AutoScroll = True
        Me.pnlLstBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.pnlLstBox.Controls.Add(Me.rbtnPack)
        Me.pnlLstBox.Controls.Add(Me.rbtnLoose)
        Me.pnlLstBox.Controls.Add(Me.btnPrintPreview)
        Me.pnlLstBox.Controls.Add(Me.lstSize)
        Me.pnlLstBox.Controls.Add(Me.lstColor)
        Me.pnlLstBox.Controls.Add(Me.lstSubCatagory)
        Me.pnlLstBox.Controls.Add(Me.lstCatagory)
        Me.pnlLstBox.Controls.Add(Me.lblSearch)
        Me.pnlLstBox.Controls.Add(Me.lstCostCenter)
        Me.pnlLstBox.Controls.Add(Me.lstHeadCostCenter)
        Me.pnlLstBox.Controls.Add(Me.lstItem)
        Me.pnlLstBox.Controls.Add(Me.lstDepartment)
        Me.pnlLstBox.Controls.Add(Me.lstType)
        Me.pnlLstBox.Controls.Add(Me.lstVendorType)
        Me.pnlLstBox.Controls.Add(Me.lstVendor)
        Me.pnlLstBox.Controls.Add(Me.txtSearch)
        Me.pnlLstBox.Controls.Add(Me.btnShow)
        Me.pnlLstBox.Location = New System.Drawing.Point(11, 131)
        Me.pnlLstBox.Name = "pnlLstBox"
        Me.pnlLstBox.Size = New System.Drawing.Size(1027, 377)
        Me.pnlLstBox.TabIndex = 2
        '
        'rbtnPack
        '
        Me.rbtnPack.AutoSize = True
        Me.rbtnPack.Location = New System.Drawing.Point(770, 346)
        Me.rbtnPack.Name = "rbtnPack"
        Me.rbtnPack.Size = New System.Drawing.Size(50, 17)
        Me.rbtnPack.TabIndex = 16
        Me.rbtnPack.Text = "Pack"
        Me.rbtnPack.UseVisualStyleBackColor = True
        '
        'rbtnLoose
        '
        Me.rbtnLoose.AutoSize = True
        Me.rbtnLoose.Checked = True
        Me.rbtnLoose.Location = New System.Drawing.Point(710, 346)
        Me.rbtnLoose.Name = "rbtnLoose"
        Me.rbtnLoose.Size = New System.Drawing.Size(54, 17)
        Me.rbtnLoose.TabIndex = 15
        Me.rbtnLoose.TabStop = True
        Me.rbtnLoose.Text = "Loose"
        Me.rbtnLoose.UseVisualStyleBackColor = True
        '
        'btnPrintPreview
        '
        Me.btnPrintPreview.Location = New System.Drawing.Point(936, 343)
        Me.btnPrintPreview.Name = "btnPrintPreview"
        Me.btnPrintPreview.Size = New System.Drawing.Size(57, 23)
        Me.btnPrintPreview.TabIndex = 14
        Me.btnPrintPreview.Text = "Print"
        Me.ToolTip1.SetToolTip(Me.btnPrintPreview, "Print")
        Me.btnPrintPreview.UseVisualStyleBackColor = True
        '
        'lstSize
        '
        Me.lstSize.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstSize.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstSize.BackColor = System.Drawing.Color.Transparent
        Me.lstSize.disableWhenChecked = False
        Me.lstSize.HeadingLabelName = "lblSize"
        Me.lstSize.HeadingText = "Size"
        Me.lstSize.Location = New System.Drawing.Point(868, 178)
        Me.lstSize.Name = "lstSize"
        Me.lstSize.ShowAddNewButton = False
        Me.lstSize.ShowInverse = True
        Me.lstSize.ShowMagnifierButton = False
        Me.lstSize.ShowNoCheck = False
        Me.lstSize.ShowResetAllButton = False
        Me.lstSize.ShowSelectall = True
        Me.lstSize.Size = New System.Drawing.Size(152, 159)
        Me.lstSize.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.lstSize, "Size List")
        Me.lstSize.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstColor
        '
        Me.lstColor.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstColor.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstColor.BackColor = System.Drawing.Color.Transparent
        Me.lstColor.disableWhenChecked = False
        Me.lstColor.HeadingLabelName = "lblColor"
        Me.lstColor.HeadingText = "Color"
        Me.lstColor.Location = New System.Drawing.Point(710, 178)
        Me.lstColor.Name = "lstColor"
        Me.lstColor.ShowAddNewButton = False
        Me.lstColor.ShowInverse = True
        Me.lstColor.ShowMagnifierButton = False
        Me.lstColor.ShowNoCheck = False
        Me.lstColor.ShowResetAllButton = False
        Me.lstColor.ShowSelectall = True
        Me.lstColor.Size = New System.Drawing.Size(152, 159)
        Me.lstColor.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.lstColor, "Color List")
        Me.lstColor.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstSubCatagory
        '
        Me.lstSubCatagory.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstSubCatagory.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstSubCatagory.BackColor = System.Drawing.Color.Transparent
        Me.lstSubCatagory.disableWhenChecked = False
        Me.lstSubCatagory.HeadingLabelName = "lblSubCatagory"
        Me.lstSubCatagory.HeadingText = "Sub Catagory"
        Me.lstSubCatagory.Location = New System.Drawing.Point(167, 178)
        Me.lstSubCatagory.Name = "lstSubCatagory"
        Me.lstSubCatagory.ShowAddNewButton = False
        Me.lstSubCatagory.ShowInverse = True
        Me.lstSubCatagory.ShowMagnifierButton = False
        Me.lstSubCatagory.ShowNoCheck = False
        Me.lstSubCatagory.ShowResetAllButton = False
        Me.lstSubCatagory.ShowSelectall = True
        Me.lstSubCatagory.Size = New System.Drawing.Size(152, 159)
        Me.lstSubCatagory.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.lstSubCatagory, "Sub Catagory List")
        Me.lstSubCatagory.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstCatagory
        '
        Me.lstCatagory.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstCatagory.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstCatagory.BackColor = System.Drawing.Color.Transparent
        Me.lstCatagory.disableWhenChecked = False
        Me.lstCatagory.HeadingLabelName = "lblCatagory"
        Me.lstCatagory.HeadingText = "Catagory"
        Me.lstCatagory.Location = New System.Drawing.Point(9, 178)
        Me.lstCatagory.Name = "lstCatagory"
        Me.lstCatagory.ShowAddNewButton = False
        Me.lstCatagory.ShowInverse = True
        Me.lstCatagory.ShowMagnifierButton = False
        Me.lstCatagory.ShowNoCheck = False
        Me.lstCatagory.ShowResetAllButton = False
        Me.lstCatagory.ShowSelectall = True
        Me.lstCatagory.Size = New System.Drawing.Size(152, 159)
        Me.lstCatagory.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.lstCatagory, "Catagory List")
        Me.lstCatagory.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Location = New System.Drawing.Point(322, 348)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(64, 13)
        Me.lblSearch.TabIndex = 7
        Me.lblSearch.Text = "Search Item"
        '
        'lstCostCenter
        '
        Me.lstCostCenter.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstCostCenter.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstCostCenter.BackColor = System.Drawing.Color.Transparent
        Me.lstCostCenter.disableWhenChecked = False
        Me.lstCostCenter.HeadingLabelName = "lstCostCenter"
        Me.lstCostCenter.HeadingText = "Cost Center"
        Me.lstCostCenter.Location = New System.Drawing.Point(167, 3)
        Me.lstCostCenter.Name = "lstCostCenter"
        Me.lstCostCenter.ShowAddNewButton = False
        Me.lstCostCenter.ShowInverse = True
        Me.lstCostCenter.ShowMagnifierButton = False
        Me.lstCostCenter.ShowNoCheck = False
        Me.lstCostCenter.ShowResetAllButton = False
        Me.lstCostCenter.ShowSelectall = True
        Me.lstCostCenter.Size = New System.Drawing.Size(152, 159)
        Me.lstCostCenter.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.lstCostCenter, "Cost Center List")
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
        Me.lstHeadCostCenter.Location = New System.Drawing.Point(9, 3)
        Me.lstHeadCostCenter.Name = "lstHeadCostCenter"
        Me.lstHeadCostCenter.ShowAddNewButton = False
        Me.lstHeadCostCenter.ShowInverse = True
        Me.lstHeadCostCenter.ShowMagnifierButton = False
        Me.lstHeadCostCenter.ShowNoCheck = False
        Me.lstHeadCostCenter.ShowResetAllButton = False
        Me.lstHeadCostCenter.ShowSelectall = True
        Me.lstHeadCostCenter.Size = New System.Drawing.Size(152, 159)
        Me.lstHeadCostCenter.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.lstHeadCostCenter, "Head Cost Center List")
        Me.lstHeadCostCenter.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstItem
        '
        Me.lstItem.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstItem.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstItem.BackColor = System.Drawing.Color.Transparent
        Me.lstItem.disableWhenChecked = False
        Me.lstItem.HeadingLabelName = "lblItem"
        Me.lstItem.HeadingText = "Item"
        Me.lstItem.Location = New System.Drawing.Point(325, 178)
        Me.lstItem.Name = "lstItem"
        Me.lstItem.ShowAddNewButton = False
        Me.lstItem.ShowInverse = True
        Me.lstItem.ShowMagnifierButton = False
        Me.lstItem.ShowNoCheck = False
        Me.lstItem.ShowResetAllButton = False
        Me.lstItem.ShowSelectall = True
        Me.lstItem.Size = New System.Drawing.Size(379, 159)
        Me.lstItem.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.lstItem, "Item List")
        Me.lstItem.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstDepartment
        '
        Me.lstDepartment.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstDepartment.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstDepartment.BackColor = System.Drawing.Color.Transparent
        Me.lstDepartment.disableWhenChecked = False
        Me.lstDepartment.HeadingLabelName = "lblDepartment"
        Me.lstDepartment.HeadingText = "Department"
        Me.lstDepartment.Location = New System.Drawing.Point(710, 3)
        Me.lstDepartment.Name = "lstDepartment"
        Me.lstDepartment.ShowAddNewButton = False
        Me.lstDepartment.ShowInverse = True
        Me.lstDepartment.ShowMagnifierButton = False
        Me.lstDepartment.ShowNoCheck = False
        Me.lstDepartment.ShowResetAllButton = False
        Me.lstDepartment.ShowSelectall = True
        Me.lstDepartment.Size = New System.Drawing.Size(152, 159)
        Me.lstDepartment.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.lstDepartment, "Item Department List")
        Me.lstDepartment.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstType
        '
        Me.lstType.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstType.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstType.BackColor = System.Drawing.Color.Transparent
        Me.lstType.disableWhenChecked = False
        Me.lstType.HeadingLabelName = "lblType"
        Me.lstType.HeadingText = "Type"
        Me.lstType.Location = New System.Drawing.Point(868, 3)
        Me.lstType.Name = "lstType"
        Me.lstType.ShowAddNewButton = False
        Me.lstType.ShowInverse = True
        Me.lstType.ShowMagnifierButton = False
        Me.lstType.ShowNoCheck = False
        Me.lstType.ShowResetAllButton = False
        Me.lstType.ShowSelectall = True
        Me.lstType.Size = New System.Drawing.Size(152, 159)
        Me.lstType.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.lstType, "Item Type List")
        Me.lstType.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstVendorType
        '
        Me.lstVendorType.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstVendorType.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstVendorType.BackColor = System.Drawing.Color.Transparent
        Me.lstVendorType.disableWhenChecked = False
        Me.lstVendorType.HeadingLabelName = "lblVendorType"
        Me.lstVendorType.HeadingText = "Vendor Type"
        Me.lstVendorType.Location = New System.Drawing.Point(325, 3)
        Me.lstVendorType.Name = "lstVendorType"
        Me.lstVendorType.ShowAddNewButton = False
        Me.lstVendorType.ShowInverse = True
        Me.lstVendorType.ShowMagnifierButton = False
        Me.lstVendorType.ShowNoCheck = False
        Me.lstVendorType.ShowResetAllButton = False
        Me.lstVendorType.ShowSelectall = True
        Me.lstVendorType.Size = New System.Drawing.Size(152, 159)
        Me.lstVendorType.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.lstVendorType, "Vendor Type List")
        Me.lstVendorType.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstVendor
        '
        Me.lstVendor.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstVendor.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstVendor.BackColor = System.Drawing.Color.Transparent
        Me.lstVendor.disableWhenChecked = False
        Me.lstVendor.HeadingLabelName = "lblVendor"
        Me.lstVendor.HeadingText = "Vendor"
        Me.lstVendor.Location = New System.Drawing.Point(483, 3)
        Me.lstVendor.Name = "lstVendor"
        Me.lstVendor.ShowAddNewButton = False
        Me.lstVendor.ShowInverse = True
        Me.lstVendor.ShowMagnifierButton = False
        Me.lstVendor.ShowNoCheck = False
        Me.lstVendor.ShowResetAllButton = False
        Me.lstVendor.ShowSelectall = True
        Me.lstVendor.Size = New System.Drawing.Size(221, 159)
        Me.lstVendor.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.lstVendor, "Vendor List")
        Me.lstVendor.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.SystemColors.Info
        Me.txtSearch.Location = New System.Drawing.Point(436, 345)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(241, 20)
        Me.txtSearch.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.txtSearch, "Search Employee by Name or Code")
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(868, 343)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(57, 23)
        Me.btnShow.TabIndex = 9
        Me.btnShow.Text = "Show"
        Me.ToolTip1.SetToolTip(Me.btnShow, "Show")
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'pnlPeriod
        '
        Me.pnlPeriod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlPeriod.AutoScroll = True
        Me.pnlPeriod.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.pnlPeriod.Controls.Add(Me.dtpToDate)
        Me.pnlPeriod.Controls.Add(Me.dtpFromDate)
        Me.pnlPeriod.Controls.Add(Me.cmbPeriod)
        Me.pnlPeriod.Controls.Add(Me.lblToDate)
        Me.pnlPeriod.Controls.Add(Me.lblDateFrom)
        Me.pnlPeriod.Controls.Add(Me.lblPeriod)
        Me.pnlPeriod.Location = New System.Drawing.Point(10, 55)
        Me.pnlPeriod.Name = "pnlPeriod"
        Me.pnlPeriod.Size = New System.Drawing.Size(1028, 70)
        Me.pnlPeriod.TabIndex = 1
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(215, 38)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(104, 20)
        Me.dtpToDate.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.dtpToDate, "To Date")
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(78, 38)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(104, 20)
        Me.dtpFromDate.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.dtpFromDate, "From Date")
        '
        'cmbPeriod
        '
        Me.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPeriod.FormattingEnabled = True
        Me.cmbPeriod.Items.AddRange(New Object() {"Today", "Yesterday", "Current Week", "Current Month", "Current Year"})
        Me.cmbPeriod.Location = New System.Drawing.Point(78, 8)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(241, 21)
        Me.cmbPeriod.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbPeriod, "Select Period")
        '
        'lblToDate
        '
        Me.lblToDate.AutoSize = True
        Me.lblToDate.Location = New System.Drawing.Point(189, 44)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(20, 13)
        Me.lblToDate.TabIndex = 4
        Me.lblToDate.Text = "To"
        Me.ToolTip1.SetToolTip(Me.lblToDate, "Select to date")
        '
        'lblDateFrom
        '
        Me.lblDateFrom.AutoSize = True
        Me.lblDateFrom.Location = New System.Drawing.Point(7, 44)
        Me.lblDateFrom.Name = "lblDateFrom"
        Me.lblDateFrom.Size = New System.Drawing.Size(30, 13)
        Me.lblDateFrom.TabIndex = 2
        Me.lblDateFrom.Text = "From"
        Me.ToolTip1.SetToolTip(Me.lblDateFrom, "From date")
        '
        'lblPeriod
        '
        Me.lblPeriod.AutoSize = True
        Me.lblPeriod.Location = New System.Drawing.Point(7, 11)
        Me.lblPeriod.Name = "lblPeriod"
        Me.lblPeriod.Size = New System.Drawing.Size(37, 13)
        Me.lblPeriod.TabIndex = 0
        Me.lblPeriod.Text = "Period"
        Me.ToolTip1.SetToolTip(Me.lblPeriod, "period")
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblProgress)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1049, 50)
        Me.pnlHeader.TabIndex = 0
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(373, -1)
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
        Me.lblHeader.Location = New System.Drawing.Point(8, 10)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(207, 23)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "GRN Detail Report"
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grdSaved)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1049, 520)
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
        Me.grdSaved.Size = New System.Drawing.Size(1049, 520)
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
        Me.ToolStrip1.Size = New System.Drawing.Size(1051, 25)
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
        Me.btnPrint.Visible = False
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
        Me.UltraTabControl1.Size = New System.Drawing.Size(1051, 541)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 2
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Criteria"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "History"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1049, 520)
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(1016, 0)
        Me.CtrlGrdBar1.MyGrid = Me.grdSaved
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(34, 25)
        Me.CtrlGrdBar1.TabIndex = 1
        '
        'frmGRNDetailReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1051, 566)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "frmGRNDetailReport"
        Me.Text = "GRN Detail Report"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.pnlLstBox.ResumeLayout(False)
        Me.pnlLstBox.PerformLayout()
        Me.pnlPeriod.ResumeLayout(False)
        Me.pnlPeriod.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
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
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents pnlPeriod As System.Windows.Forms.Panel
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents lblToDate As System.Windows.Forms.Label
    Friend WithEvents lblDateFrom As System.Windows.Forms.Label
    Friend WithEvents lblPeriod As System.Windows.Forms.Label
    Friend WithEvents pnlLstBox As System.Windows.Forms.Panel
    Friend WithEvents lstCostCenter As SimpleAccounts.uiListControl
    Friend WithEvents lstHeadCostCenter As SimpleAccounts.uiListControl
    Friend WithEvents lstItem As SimpleAccounts.uiListControl
    Friend WithEvents lstDepartment As SimpleAccounts.uiListControl
    Friend WithEvents lstType As SimpleAccounts.uiListControl
    Friend WithEvents lstVendorType As SimpleAccounts.uiListControl
    Friend WithEvents lstVendor As SimpleAccounts.uiListControl
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents lstSize As SimpleAccounts.uiListControl
    Friend WithEvents lstColor As SimpleAccounts.uiListControl
    Friend WithEvents lstSubCatagory As SimpleAccounts.uiListControl
    Friend WithEvents lstCatagory As SimpleAccounts.uiListControl
    Friend WithEvents btnPrintPreview As System.Windows.Forms.Button
    Friend WithEvents rbtnPack As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnLoose As System.Windows.Forms.RadioButton
End Class

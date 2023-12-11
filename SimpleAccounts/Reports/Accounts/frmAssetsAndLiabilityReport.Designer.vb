<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAssetsAndLiabilityReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAssetsAndLiabilityReport))
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlLstBox = New System.Windows.Forms.Panel()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.btnPrintPreview = New System.Windows.Forms.Button()
        Me.lstCostCenter = New SimpleAccounts.uiListControl()
        Me.lstHeadCostCenter = New SimpleAccounts.uiListControl()
        Me.lstSubSub = New SimpleAccounts.uiListControl()
        Me.lstDetail = New SimpleAccounts.uiListControl()
        Me.lstMain = New SimpleAccounts.uiListControl()
        Me.lstSub = New SimpleAccounts.uiListControl()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.pnlPeriod = New System.Windows.Forms.Panel()
        Me.cmbCompany = New System.Windows.Forms.ComboBox()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.lblToDate = New System.Windows.Forms.Label()
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
        Me.UltraTabPageControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1574, 812)
        '
        'pnlLstBox
        '
        Me.pnlLstBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlLstBox.AutoScroll = True
        Me.pnlLstBox.BackColor = System.Drawing.Color.Transparent
        Me.pnlLstBox.Controls.Add(Me.lblSearch)
        Me.pnlLstBox.Controls.Add(Me.txtSearch)
        Me.pnlLstBox.Controls.Add(Me.btnPrintPreview)
        Me.pnlLstBox.Controls.Add(Me.lstCostCenter)
        Me.pnlLstBox.Controls.Add(Me.lstHeadCostCenter)
        Me.pnlLstBox.Controls.Add(Me.lstSubSub)
        Me.pnlLstBox.Controls.Add(Me.lstDetail)
        Me.pnlLstBox.Controls.Add(Me.lstMain)
        Me.pnlLstBox.Controls.Add(Me.lstSub)
        Me.pnlLstBox.Controls.Add(Me.btnShow)
        Me.pnlLstBox.Location = New System.Drawing.Point(16, 202)
        Me.pnlLstBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlLstBox.Name = "pnlLstBox"
        Me.pnlLstBox.Size = New System.Drawing.Size(1540, 600)
        Me.pnlLstBox.TabIndex = 2
        '
        'lblSearch
        '
        Me.lblSearch.AutoSize = True
        Me.lblSearch.Location = New System.Drawing.Point(483, 523)
        Me.lblSearch.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(123, 20)
        Me.lblSearch.TabIndex = 15
        Me.lblSearch.Text = "Search Account"
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.White
        Me.txtSearch.Location = New System.Drawing.Point(651, 512)
        Me.txtSearch.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(271, 26)
        Me.txtSearch.TabIndex = 16
        '
        'btnPrintPreview
        '
        Me.btnPrintPreview.Location = New System.Drawing.Point(1071, 512)
        Me.btnPrintPreview.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnPrintPreview.Name = "btnPrintPreview"
        Me.btnPrintPreview.Size = New System.Drawing.Size(142, 35)
        Me.btnPrintPreview.TabIndex = 14
        Me.btnPrintPreview.Text = "Print"
        Me.btnPrintPreview.UseVisualStyleBackColor = True
        '
        'lstCostCenter
        '
        Me.lstCostCenter.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstCostCenter.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstCostCenter.BackColor = System.Drawing.Color.Transparent
        Me.lstCostCenter.disableWhenChecked = False
        Me.lstCostCenter.HeadingLabelName = "lstCostCenter"
        Me.lstCostCenter.HeadingText = "Cost Center"
        Me.lstCostCenter.Location = New System.Drawing.Point(250, 5)
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
        Me.lstHeadCostCenter.Location = New System.Drawing.Point(14, 5)
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
        Me.lstHeadCostCenter.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstSubSub
        '
        Me.lstSubSub.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstSubSub.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstSubSub.BackColor = System.Drawing.Color.Transparent
        Me.lstSubSub.disableWhenChecked = False
        Me.lstSubSub.HeadingLabelName = ""
        Me.lstSubSub.HeadingText = "Sub Sub Account"
        Me.lstSubSub.Location = New System.Drawing.Point(14, 258)
        Me.lstSubSub.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstSubSub.Name = "lstSubSub"
        Me.lstSubSub.ShowAddNewButton = False
        Me.lstSubSub.ShowInverse = True
        Me.lstSubSub.ShowMagnifierButton = False
        Me.lstSubSub.ShowNoCheck = False
        Me.lstSubSub.ShowResetAllButton = False
        Me.lstSubSub.ShowSelectall = True
        Me.lstSubSub.Size = New System.Drawing.Size(465, 245)
        Me.lstSubSub.TabIndex = 0
        Me.lstSubSub.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstDetail
        '
        Me.lstDetail.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstDetail.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstDetail.BackColor = System.Drawing.Color.Transparent
        Me.lstDetail.disableWhenChecked = False
        Me.lstDetail.HeadingLabelName = ""
        Me.lstDetail.HeadingText = "Detail Account"
        Me.lstDetail.Location = New System.Drawing.Point(488, 258)
        Me.lstDetail.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstDetail.Name = "lstDetail"
        Me.lstDetail.ShowAddNewButton = False
        Me.lstDetail.ShowInverse = True
        Me.lstDetail.ShowMagnifierButton = False
        Me.lstDetail.ShowNoCheck = False
        Me.lstDetail.ShowResetAllButton = False
        Me.lstDetail.ShowSelectall = True
        Me.lstDetail.Size = New System.Drawing.Size(478, 245)
        Me.lstDetail.TabIndex = 1
        Me.lstDetail.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstMain
        '
        Me.lstMain.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstMain.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstMain.BackColor = System.Drawing.Color.Transparent
        Me.lstMain.disableWhenChecked = False
        Me.lstMain.HeadingLabelName = ""
        Me.lstMain.HeadingText = "Main Account"
        Me.lstMain.Location = New System.Drawing.Point(488, 5)
        Me.lstMain.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstMain.Name = "lstMain"
        Me.lstMain.ShowAddNewButton = False
        Me.lstMain.ShowInverse = True
        Me.lstMain.ShowMagnifierButton = False
        Me.lstMain.ShowNoCheck = False
        Me.lstMain.ShowResetAllButton = False
        Me.lstMain.ShowSelectall = True
        Me.lstMain.Size = New System.Drawing.Size(478, 245)
        Me.lstMain.TabIndex = 2
        Me.lstMain.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstSub
        '
        Me.lstSub.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstSub.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstSub.BackColor = System.Drawing.Color.Transparent
        Me.lstSub.disableWhenChecked = False
        Me.lstSub.HeadingLabelName = ""
        Me.lstSub.HeadingText = "Sub Account"
        Me.lstSub.Location = New System.Drawing.Point(975, 5)
        Me.lstSub.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstSub.Name = "lstSub"
        Me.lstSub.ShowAddNewButton = False
        Me.lstSub.ShowInverse = True
        Me.lstSub.ShowMagnifierButton = False
        Me.lstSub.ShowNoCheck = False
        Me.lstSub.ShowResetAllButton = False
        Me.lstSub.ShowSelectall = True
        Me.lstSub.Size = New System.Drawing.Size(412, 245)
        Me.lstSub.TabIndex = 3
        Me.lstSub.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(1222, 512)
        Me.btnShow.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(134, 35)
        Me.btnShow.TabIndex = 9
        Me.btnShow.Text = "Show"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'pnlPeriod
        '
        Me.pnlPeriod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlPeriod.AutoScroll = True
        Me.pnlPeriod.BackColor = System.Drawing.Color.Transparent
        Me.pnlPeriod.Controls.Add(Me.cmbCompany)
        Me.pnlPeriod.Controls.Add(Me.dtpToDate)
        Me.pnlPeriod.Controls.Add(Me.lblToDate)
        Me.pnlPeriod.Controls.Add(Me.lblPeriod)
        Me.pnlPeriod.Location = New System.Drawing.Point(15, 85)
        Me.pnlPeriod.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlPeriod.Name = "pnlPeriod"
        Me.pnlPeriod.Size = New System.Drawing.Size(1542, 108)
        Me.pnlPeriod.TabIndex = 1
        '
        'cmbCompany
        '
        Me.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCompany.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.cmbCompany.FormattingEnabled = True
        Me.cmbCompany.Items.AddRange(New Object() {"Loose", "Pack"})
        Me.cmbCompany.Location = New System.Drawing.Point(118, 57)
        Me.cmbCompany.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbCompany.Name = "cmbCompany"
        Me.cmbCompany.Size = New System.Drawing.Size(360, 28)
        Me.cmbCompany.TabIndex = 6
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(118, 8)
        Me.dtpToDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(360, 26)
        Me.dtpToDate.TabIndex = 5
        '
        'lblToDate
        '
        Me.lblToDate.AutoSize = True
        Me.lblToDate.Location = New System.Drawing.Point(10, 17)
        Me.lblToDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(66, 20)
        Me.lblToDate.TabIndex = 4
        Me.lblToDate.Text = "To Date"
        '
        'lblPeriod
        '
        Me.lblPeriod.AutoSize = True
        Me.lblPeriod.Location = New System.Drawing.Point(10, 69)
        Me.lblPeriod.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPeriod.Name = "lblPeriod"
        Me.lblPeriod.Size = New System.Drawing.Size(76, 20)
        Me.lblPeriod.TabIndex = 0
        Me.lblPeriod.Text = "Comapny"
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblProgress)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1574, 77)
        Me.pnlHeader.TabIndex = 0
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(560, -2)
        Me.lblProgress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(399, 77)
        Me.lblProgress.TabIndex = 1
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(12, 15)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(381, 36)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Asset And Liability Report"
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grdSaved)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1574, 812)
        '
        'grdSaved
        '
        Me.grdSaved.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdSaved.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSaved.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdSaved.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdSaved.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdSaved.Location = New System.Drawing.Point(0, 0)
        Me.grdSaved.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdSaved.Name = "grdSaved"
        Me.grdSaved.RecordNavigator = True
        Me.grdSaved.Size = New System.Drawing.Size(1574, 812)
        Me.grdSaved.TabIndex = 0
        Me.grdSaved.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdSaved.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnPrint, Me.toolStripSeparator1, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1576, 32)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(75, 29)
        Me.btnNew.Text = "&New"
        '
        'btnPrint
        '
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(76, 29)
        Me.btnPrint.Text = "&Print"
        Me.btnPrint.Visible = False
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 32)
        '
        'btnHelp
        '
        Me.btnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(28, 29)
        Me.btnHelp.Text = "He&lp"
        Me.btnHelp.Visible = False
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
        Me.UltraTabControl1.Size = New System.Drawing.Size(1576, 839)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 3
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Criteria"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "History"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.VisualStudio2005
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1574, 812)
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Nothing
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(1524, 0)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar1.MyGrid = Me.grdSaved
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(51, 38)
        Me.CtrlGrdBar1.TabIndex = 5
        '
        'frmAssetsAndLiabilityReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1576, 871)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmAssetsAndLiabilityReport"
        Me.Text = "AssetsAndLiabilityReport"
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
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents pnlLstBox As System.Windows.Forms.Panel
    Friend WithEvents btnPrintPreview As System.Windows.Forms.Button
    Friend WithEvents lstCostCenter As SimpleAccounts.uiListControl
    Friend WithEvents lstHeadCostCenter As SimpleAccounts.uiListControl
    Friend WithEvents lstSubSub As SimpleAccounts.uiListControl
    Friend WithEvents lstDetail As SimpleAccounts.uiListControl
    Friend WithEvents lstMain As SimpleAccounts.uiListControl
    Friend WithEvents lstSub As SimpleAccounts.uiListControl
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents pnlPeriod As System.Windows.Forms.Panel
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblToDate As System.Windows.Forms.Label
    Friend WithEvents lblPeriod As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grdSaved As Janus.Windows.GridEX.GridEX
    Friend WithEvents cmbCompany As System.Windows.Forms.ComboBox
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents lblSearch As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
End Class

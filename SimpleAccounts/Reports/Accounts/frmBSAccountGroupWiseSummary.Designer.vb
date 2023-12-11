<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBSAccountGroupWiseSummary
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
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn5 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Id")
        Dim UltraGridColumn6 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleDescription")
        Dim UltraGridColumn7 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Size")
        Dim UltraGridColumn8 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Color")
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBSAccountGroupWiseSummary))
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlLstBox = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.cmbBSCategory = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.lstCostCenter = New SimpleAccounts.uiListControl()
        Me.lstHeadCostCenter = New SimpleAccounts.uiListControl()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.pnlPeriod = New System.Windows.Forms.Panel()
        Me.chkShowWithOutCostCenter = New System.Windows.Forms.CheckBox()
        Me.chkIncludeUnPosted = New System.Windows.Forms.CheckBox()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.lblToDate = New System.Windows.Forms.Label()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdSaved = New Janus.Windows.GridEX.GridEX()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
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
        CType(Me.cmbBSCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlPeriod.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlLstBox)
        Me.UltraTabPageControl1.Controls.Add(Me.pnlPeriod)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1216, 553)
        '
        'pnlLstBox
        '
        Me.pnlLstBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlLstBox.BackColor = System.Drawing.Color.Transparent
        Me.pnlLstBox.Controls.Add(Me.Label1)
        Me.pnlLstBox.Controls.Add(Me.lblProgress)
        Me.pnlLstBox.Controls.Add(Me.cmbBSCategory)
        Me.pnlLstBox.Controls.Add(Me.lstCostCenter)
        Me.pnlLstBox.Controls.Add(Me.lstHeadCostCenter)
        Me.pnlLstBox.Controls.Add(Me.btnShow)
        Me.pnlLstBox.Location = New System.Drawing.Point(16, 129)
        Me.pnlLstBox.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlLstBox.Name = "pnlLstBox"
        Me.pnlLstBox.Size = New System.Drawing.Size(1183, 395)
        Me.pnlLstBox.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(18, 17)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(112, 21)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Report Title"
        Me.ToolTip1.SetToolTip(Me.Label1, "From date")
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(699, 48)
        Me.lblProgress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(399, 77)
        Me.lblProgress.TabIndex = 1
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'cmbBSCategory
        '
        Me.cmbBSCategory.AlwaysInEditMode = True
        Me.cmbBSCategory.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append
        Me.cmbBSCategory.CheckedListSettings.CheckStateMember = ""
        Appearance7.BackColor = System.Drawing.Color.White
        Appearance7.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        Me.cmbBSCategory.DisplayLayout.Appearance = Appearance7
        UltraGridColumn5.Header.VisiblePosition = 0
        UltraGridColumn5.Hidden = True
        UltraGridColumn6.Header.VisiblePosition = 1
        UltraGridColumn7.Header.VisiblePosition = 2
        UltraGridColumn8.Header.VisiblePosition = 3
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn5, UltraGridColumn6, UltraGridColumn7, UltraGridColumn8})
        Me.cmbBSCategory.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbBSCategory.DisplayLayout.InterBandSpacing = 10
        Me.cmbBSCategory.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbBSCategory.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbBSCategory.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Appearance8.BackColor = System.Drawing.Color.Transparent
        Me.cmbBSCategory.DisplayLayout.Override.CardAreaAppearance = Appearance8
        Me.cmbBSCategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbBSCategory.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance9.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance9.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance9.ForeColor = System.Drawing.Color.White
        Appearance9.TextHAlignAsString = "Left"
        Appearance9.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent
        Me.cmbBSCategory.DisplayLayout.Override.HeaderAppearance = Appearance9
        Me.cmbBSCategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance10.BorderColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbBSCategory.DisplayLayout.Override.RowAppearance = Appearance10
        Appearance11.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance11.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.cmbBSCategory.DisplayLayout.Override.RowSelectorAppearance = Appearance11
        Me.cmbBSCategory.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbBSCategory.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance12.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance12.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance12.ForeColor = System.Drawing.Color.Black
        Me.cmbBSCategory.DisplayLayout.Override.SelectedRowAppearance = Appearance12
        Me.cmbBSCategory.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbBSCategory.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbBSCategory.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbBSCategory.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbBSCategory.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.cmbBSCategory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbBSCategory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbBSCategory.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbBSCategory.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbBSCategory.LimitToList = True
        Me.cmbBSCategory.Location = New System.Drawing.Point(14, 48)
        Me.cmbBSCategory.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbBSCategory.MaxDropDownItems = 20
        Me.cmbBSCategory.Name = "cmbBSCategory"
        Me.cmbBSCategory.Size = New System.Drawing.Size(188, 29)
        Me.cmbBSCategory.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbBSCategory, "Select any Account Group")
        '
        'lstCostCenter
        '
        Me.lstCostCenter.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstCostCenter.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstCostCenter.BackColor = System.Drawing.Color.Transparent
        Me.lstCostCenter.disableWhenChecked = False
        Me.lstCostCenter.HeadingLabelName = "lstCostCenter"
        Me.lstCostCenter.HeadingText = "Cost Center"
        Me.lstCostCenter.Location = New System.Drawing.Point(282, 91)
        Me.lstCostCenter.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstCostCenter.Name = "lstCostCenter"
        Me.lstCostCenter.ShowAddNewButton = False
        Me.lstCostCenter.ShowInverse = True
        Me.lstCostCenter.ShowMagnifierButton = False
        Me.lstCostCenter.ShowNoCheck = False
        Me.lstCostCenter.ShowResetAllButton = False
        Me.lstCostCenter.ShowSelectall = True
        Me.lstCostCenter.Size = New System.Drawing.Size(228, 245)
        Me.lstCostCenter.TabIndex = 3
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
        Me.lstHeadCostCenter.Location = New System.Drawing.Point(14, 91)
        Me.lstHeadCostCenter.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstHeadCostCenter.Name = "lstHeadCostCenter"
        Me.lstHeadCostCenter.ShowAddNewButton = False
        Me.lstHeadCostCenter.ShowInverse = True
        Me.lstHeadCostCenter.ShowMagnifierButton = False
        Me.lstHeadCostCenter.ShowNoCheck = False
        Me.lstHeadCostCenter.ShowResetAllButton = False
        Me.lstHeadCostCenter.ShowSelectall = True
        Me.lstHeadCostCenter.Size = New System.Drawing.Size(228, 245)
        Me.lstHeadCostCenter.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.lstHeadCostCenter, "Employee Head Cost Center list")
        Me.lstHeadCostCenter.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(363, 343)
        Me.btnShow.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(106, 35)
        Me.btnShow.TabIndex = 4
        Me.btnShow.Text = "Show"
        Me.ToolTip1.SetToolTip(Me.btnShow, "Show")
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'pnlPeriod
        '
        Me.pnlPeriod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlPeriod.BackColor = System.Drawing.Color.Transparent
        Me.pnlPeriod.Controls.Add(Me.chkShowWithOutCostCenter)
        Me.pnlPeriod.Controls.Add(Me.chkIncludeUnPosted)
        Me.pnlPeriod.Controls.Add(Me.dtpToDate)
        Me.pnlPeriod.Controls.Add(Me.lblToDate)
        Me.pnlPeriod.Location = New System.Drawing.Point(15, 12)
        Me.pnlPeriod.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlPeriod.Name = "pnlPeriod"
        Me.pnlPeriod.Size = New System.Drawing.Size(1185, 108)
        Me.pnlPeriod.TabIndex = 1
        '
        'chkShowWithOutCostCenter
        '
        Me.chkShowWithOutCostCenter.AutoSize = True
        Me.chkShowWithOutCostCenter.Location = New System.Drawing.Point(278, 58)
        Me.chkShowWithOutCostCenter.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkShowWithOutCostCenter.Name = "chkShowWithOutCostCenter"
        Me.chkShowWithOutCostCenter.Size = New System.Drawing.Size(213, 24)
        Me.chkShowWithOutCostCenter.TabIndex = 7
        Me.chkShowWithOutCostCenter.Text = "Show without cost center"
        Me.chkShowWithOutCostCenter.UseVisualStyleBackColor = True
        '
        'chkIncludeUnPosted
        '
        Me.chkIncludeUnPosted.AutoSize = True
        Me.chkIncludeUnPosted.Checked = True
        Me.chkIncludeUnPosted.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIncludeUnPosted.Location = New System.Drawing.Point(24, 58)
        Me.chkIncludeUnPosted.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkIncludeUnPosted.Name = "chkIncludeUnPosted"
        Me.chkIncludeUnPosted.Size = New System.Drawing.Size(234, 24)
        Me.chkIncludeUnPosted.TabIndex = 6
        Me.chkIncludeUnPosted.Text = "Include UnPosted Vouchers"
        Me.ToolTip1.SetToolTip(Me.chkIncludeUnPosted, "Include UnPosted Vouchers")
        Me.chkIncludeUnPosted.UseVisualStyleBackColor = True
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(58, 8)
        Me.dtpToDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(190, 26)
        Me.dtpToDate.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.dtpToDate, "To Date")
        '
        'lblToDate
        '
        Me.lblToDate.AutoSize = True
        Me.lblToDate.Location = New System.Drawing.Point(20, 17)
        Me.lblToDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(27, 20)
        Me.lblToDate.TabIndex = 4
        Me.lblToDate.Text = "To"
        Me.ToolTip1.SetToolTip(Me.lblToDate, "Select to date")
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grdSaved)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1216, 553)
        '
        'grdSaved
        '
        Me.grdSaved.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdSaved.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSaved.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdSaved.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdSaved.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdSaved.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.grdSaved.Location = New System.Drawing.Point(0, 0)
        Me.grdSaved.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdSaved.Name = "grdSaved"
        Me.grdSaved.RecordNavigator = True
        Me.grdSaved.Size = New System.Drawing.Size(1216, 553)
        Me.grdSaved.TabIndex = 0
        Me.grdSaved.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdSaved.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.Label3)
        Me.pnlHeader.Controls.Add(Me.btnClose)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 32)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1218, 77)
        Me.pnlHeader.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold)
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(666, 25)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(0, 26)
        Me.Label3.TabIndex = 5
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.btnClose.Location = New System.Drawing.Point(1148, 15)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(54, 46)
        Me.btnClose.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.btnClose, "Close")
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(12, 15)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(556, 36)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Category Wise Balance Sheet Summary"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnPrint, Me.toolStripSeparator1, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1218, 32)
        Me.ToolStrip1.TabIndex = 0
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
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 109)
        Me.UltraTabControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(1218, 580)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 2
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
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1216, 553)
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(1166, 0)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar1.MyGrid = Me.grdSaved
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(51, 38)
        Me.CtrlGrdBar1.TabIndex = 1
        '
        'frmBSAccountGroupWiseSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1218, 689)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmBSAccountGroupWiseSummary"
        Me.Text = "Category Wise Balance Sheet Summary"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.pnlLstBox.ResumeLayout(False)
        Me.pnlLstBox.PerformLayout()
        CType(Me.cmbBSCategory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlPeriod.ResumeLayout(False)
        Me.pnlPeriod.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
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
    Friend WithEvents lblToDate As System.Windows.Forms.Label
    Friend WithEvents pnlLstBox As System.Windows.Forms.Panel
    Friend WithEvents lstCostCenter As SimpleAccounts.uiListControl
    Friend WithEvents lstHeadCostCenter As SimpleAccounts.uiListControl
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbBSCategory As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents chkIncludeUnPosted As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkShowWithOutCostCenter As System.Windows.Forms.CheckBox
End Class

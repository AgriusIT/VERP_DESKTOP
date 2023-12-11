<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGrdRptAgingReceiveablesOld
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
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn1 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("coa_detail_id")
        Dim UltraGridColumn2 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("detail_title")
        Dim UltraGridColumn3 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("detail_code")
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim grd_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand2 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn9 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Id")
        Dim UltraGridColumn10 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleDescription")
        Dim UltraGridColumn11 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Size")
        Dim UltraGridColumn12 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Color")
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance15 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance16 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance17 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance18 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGrdRptAgingReceiveablesOld))
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.btnLoadNew = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbZone = New System.Windows.Forms.ComboBox()
        Me.cmbBelt = New System.Windows.Forms.ComboBox()
        Me.cmbRegion = New System.Windows.Forms.ComboBox()
        Me.lblCostCenter = New System.Windows.Forms.Label()
        Me.cmbCostCenter = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.lblSubSub = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnAddTemplate = New System.Windows.Forms.ToolStripButton()
        Me.btnSMSTempSettings = New System.Windows.Forms.ToolStripButton()
        Me.btnSendSMS = New System.Windows.Forms.ToolStripButton()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.cmbSubSub = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.chkIncludeUnPosted = New System.Windows.Forms.CheckBox()
        Me.cmbFormate = New System.Windows.Forms.ComboBox()
        Me.lnkRefresh = New System.Windows.Forms.LinkLabel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pnlFollowUp = New System.Windows.Forms.Panel()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.lbldtpFollowUp = New System.Windows.Forms.Label()
        Me.dtpFollowUp = New System.Windows.Forms.DateTimePicker()
        Me.lblFollowUpRemakrs = New System.Windows.Forms.Label()
        Me.txtFollowUpRemarks = New System.Windows.Forms.TextBox()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.CtrlGrdBar2 = New SimpleAccounts.CtrlGrdBar()
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip3 = New System.Windows.Forms.ToolStrip()
        Me.btnBack = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.UltraTabPageControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        CType(Me.cmbCostCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.cmbSubSub, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFollowUp.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip3.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.Label6)
        Me.UltraTabPageControl1.Controls.Add(Me.Label4)
        Me.UltraTabPageControl1.Controls.Add(Me.Label3)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbZone)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbBelt)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbRegion)
        Me.UltraTabPageControl1.Controls.Add(Me.lblCostCenter)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbCostCenter)
        Me.UltraTabPageControl1.Controls.Add(Me.CtrlGrdBar1)
        Me.UltraTabPageControl1.Controls.Add(Me.lblSubSub)
        Me.UltraTabPageControl1.Controls.Add(Me.ToolStrip1)
        Me.UltraTabPageControl1.Controls.Add(Me.btnSearch)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbSubSub)
        Me.UltraTabPageControl1.Controls.Add(Me.grd)
        Me.UltraTabPageControl1.Controls.Add(Me.chkIncludeUnPosted)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbFormate)
        Me.UltraTabPageControl1.Controls.Add(Me.lnkRefresh)
        Me.UltraTabPageControl1.Controls.Add(Me.Label2)
        Me.UltraTabPageControl1.Controls.Add(Me.pnlFollowUp)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1274, 935)
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnLoadNew)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 32)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1274, 54)
        Me.pnlHeader.TabIndex = 19
        '
        'btnLoadNew
        '
        Me.btnLoadNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLoadNew.BackColor = System.Drawing.Color.Teal
        Me.btnLoadNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoadNew.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoadNew.ForeColor = System.Drawing.Color.White
        Me.btnLoadNew.Location = New System.Drawing.Point(1156, 9)
        Me.btnLoadNew.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnLoadNew.Name = "btnLoadNew"
        Me.btnLoadNew.Size = New System.Drawing.Size(112, 35)
        Me.btnLoadNew.TabIndex = 3
        Me.btnLoadNew.Text = "New"
        Me.btnLoadNew.UseVisualStyleBackColor = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(27, 6)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(264, 36)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Aging Receivables"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(490, 188)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(46, 20)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "Zone"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(90, 188)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 20)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Region"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(90, 229)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 20)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Belt"
        '
        'cmbZone
        '
        Me.cmbZone.FormattingEnabled = True
        Me.cmbZone.Location = New System.Drawing.Point(592, 183)
        Me.cmbZone.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbZone.Name = "cmbZone"
        Me.cmbZone.Size = New System.Drawing.Size(216, 28)
        Me.cmbZone.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.cmbZone, "Zone")
        '
        'cmbBelt
        '
        Me.cmbBelt.FormattingEnabled = True
        Me.cmbBelt.Location = New System.Drawing.Point(166, 225)
        Me.cmbBelt.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbBelt.Name = "cmbBelt"
        Me.cmbBelt.Size = New System.Drawing.Size(300, 28)
        Me.cmbBelt.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.cmbBelt, "Blet")
        '
        'cmbRegion
        '
        Me.cmbRegion.FormattingEnabled = True
        Me.cmbRegion.Location = New System.Drawing.Point(166, 183)
        Me.cmbRegion.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbRegion.Name = "cmbRegion"
        Me.cmbRegion.Size = New System.Drawing.Size(300, 28)
        Me.cmbRegion.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.cmbRegion, "Region")
        '
        'lblCostCenter
        '
        Me.lblCostCenter.AutoSize = True
        Me.lblCostCenter.Location = New System.Drawing.Point(490, 146)
        Me.lblCostCenter.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCostCenter.Name = "lblCostCenter"
        Me.lblCostCenter.Size = New System.Drawing.Size(94, 20)
        Me.lblCostCenter.TabIndex = 14
        Me.lblCostCenter.Text = "Cost Centre"
        '
        'cmbCostCenter
        '
        Appearance1.BackColor = System.Drawing.Color.White
        Me.cmbCostCenter.Appearance = Appearance1
        Me.cmbCostCenter.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend
        Me.cmbCostCenter.CheckedListSettings.CheckStateMember = ""
        Appearance2.BackColor = System.Drawing.Color.White
        Me.cmbCostCenter.DisplayLayout.Appearance = Appearance2
        UltraGridColumn1.Header.Caption = "ID"
        UltraGridColumn1.Header.VisiblePosition = 0
        UltraGridColumn1.Hidden = True
        UltraGridColumn1.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(8, 0)
        UltraGridColumn2.Header.Caption = "Account"
        UltraGridColumn2.Header.VisiblePosition = 1
        UltraGridColumn2.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(128, 0)
        UltraGridColumn3.Header.Caption = "Code"
        UltraGridColumn3.Header.VisiblePosition = 2
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn1, UltraGridColumn2, UltraGridColumn3})
        Me.cmbCostCenter.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbCostCenter.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbCostCenter.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbCostCenter.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbCostCenter.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Appearance3.BackColor = System.Drawing.Color.Transparent
        Me.cmbCostCenter.DisplayLayout.Override.CardAreaAppearance = Appearance3
        Me.cmbCostCenter.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbCostCenter.DisplayLayout.Override.CellPadding = 3
        Me.cmbCostCenter.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance4.TextHAlignAsString = "Left"
        Me.cmbCostCenter.DisplayLayout.Override.HeaderAppearance = Appearance4
        Me.cmbCostCenter.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance5.BorderColor = System.Drawing.Color.LightGray
        Appearance5.TextVAlignAsString = "Middle"
        Me.cmbCostCenter.DisplayLayout.Override.RowAppearance = Appearance5
        Appearance6.BackColor = System.Drawing.Color.LightSteelBlue
        Appearance6.BorderColor = System.Drawing.Color.Black
        Appearance6.ForeColor = System.Drawing.Color.Black
        Me.cmbCostCenter.DisplayLayout.Override.SelectedRowAppearance = Appearance6
        Me.cmbCostCenter.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbCostCenter.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbCostCenter.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbCostCenter.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.cmbCostCenter.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbCostCenter.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbCostCenter.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbCostCenter.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbCostCenter.LimitToList = True
        Me.cmbCostCenter.Location = New System.Drawing.Point(592, 140)
        Me.cmbCostCenter.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbCostCenter.MaxDropDownItems = 16
        Me.cmbCostCenter.Name = "cmbCostCenter"
        Me.cmbCostCenter.Size = New System.Drawing.Size(216, 29)
        Me.cmbCostCenter.TabIndex = 13
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(1220, -2)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar1.MyGrid = Me.grd
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(57, 38)
        Me.CtrlGrdBar1.TabIndex = 8
        '
        'grd
        '
        Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grd_DesignTimeLayout.LayoutString = resources.GetString("grd_DesignTimeLayout.LayoutString")
        Me.grd.DesignTimeLayout = grd_DesignTimeLayout
        Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grd.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.grd.Location = New System.Drawing.Point(3, 265)
        Me.grd.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(1266, 515)
        Me.grd.TabIndex = 2
        Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'lblSubSub
        '
        Me.lblSubSub.AutoSize = True
        Me.lblSubSub.Location = New System.Drawing.Point(90, 146)
        Me.lblSubSub.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSubSub.Name = "lblSubSub"
        Me.lblSubSub.Size = New System.Drawing.Size(71, 20)
        Me.lblSubSub.TabIndex = 12
        Me.lblSubSub.Text = "Sub Sub"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh, Me.btnPrint, Me.ToolStripSeparator1, Me.btnAddTemplate, Me.btnSMSTempSettings, Me.btnSendSMS})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1274, 32)
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
        'btnPrint
        '
        Me.btnPrint.Image = Global.SimpleAccounts.My.Resources.Resources.print
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(76, 29)
        Me.btnPrint.Text = "Print"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 32)
        '
        'btnAddTemplate
        '
        Me.btnAddTemplate.Image = Global.SimpleAccounts.My.Resources.Resources.addIcon
        Me.btnAddTemplate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAddTemplate.Name = "btnAddTemplate"
        Me.btnAddTemplate.Size = New System.Drawing.Size(178, 29)
        Me.btnAddTemplate.Text = "Add aging layout"
        '
        'btnSMSTempSettings
        '
        Me.btnSMSTempSettings.Image = Global.SimpleAccounts.My.Resources.Resources.Attach
        Me.btnSMSTempSettings.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSMSTempSettings.Name = "btnSMSTempSettings"
        Me.btnSMSTempSettings.Size = New System.Drawing.Size(210, 29)
        Me.btnSMSTempSettings.Text = "SMS template setting"
        '
        'btnSendSMS
        '
        Me.btnSendSMS.Image = CType(resources.GetObject("btnSendSMS.Image"), System.Drawing.Image)
        Me.btnSendSMS.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSendSMS.Name = "btnSendSMS"
        Me.btnSendSMS.Size = New System.Drawing.Size(121, 29)
        Me.btnSendSMS.Text = "Send SMS"
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(698, 225)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(112, 35)
        Me.btnSearch.TabIndex = 11
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'cmbSubSub
        '
        Me.cmbSubSub.AlwaysInEditMode = True
        Me.cmbSubSub.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append
        Me.cmbSubSub.CheckedListSettings.CheckStateMember = ""
        Appearance13.BackColor = System.Drawing.Color.White
        Appearance13.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        Me.cmbSubSub.DisplayLayout.Appearance = Appearance13
        UltraGridColumn9.Header.VisiblePosition = 0
        UltraGridColumn9.Hidden = True
        UltraGridColumn10.Header.VisiblePosition = 1
        UltraGridColumn11.Header.VisiblePosition = 2
        UltraGridColumn12.Header.VisiblePosition = 3
        UltraGridBand2.Columns.AddRange(New Object() {UltraGridColumn9, UltraGridColumn10, UltraGridColumn11, UltraGridColumn12})
        Me.cmbSubSub.DisplayLayout.BandsSerializer.Add(UltraGridBand2)
        Me.cmbSubSub.DisplayLayout.InterBandSpacing = 10
        Me.cmbSubSub.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbSubSub.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbSubSub.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Appearance14.BackColor = System.Drawing.Color.Transparent
        Me.cmbSubSub.DisplayLayout.Override.CardAreaAppearance = Appearance14
        Me.cmbSubSub.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbSubSub.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance15.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance15.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance15.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance15.ForeColor = System.Drawing.Color.White
        Appearance15.TextHAlignAsString = "Left"
        Appearance15.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent
        Me.cmbSubSub.DisplayLayout.Override.HeaderAppearance = Appearance15
        Me.cmbSubSub.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance16.BorderColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbSubSub.DisplayLayout.Override.RowAppearance = Appearance16
        Appearance17.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance17.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance17.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.cmbSubSub.DisplayLayout.Override.RowSelectorAppearance = Appearance17
        Me.cmbSubSub.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbSubSub.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance18.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance18.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance18.ForeColor = System.Drawing.Color.Black
        Me.cmbSubSub.DisplayLayout.Override.SelectedRowAppearance = Appearance18
        Me.cmbSubSub.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbSubSub.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbSubSub.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbSubSub.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbSubSub.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.cmbSubSub.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbSubSub.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbSubSub.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbSubSub.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbSubSub.LimitToList = True
        Me.cmbSubSub.Location = New System.Drawing.Point(166, 140)
        Me.cmbSubSub.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbSubSub.MaxDropDownItems = 20
        Me.cmbSubSub.Name = "cmbSubSub"
        Me.cmbSubSub.Size = New System.Drawing.Size(302, 29)
        Me.cmbSubSub.TabIndex = 10
        '
        'chkIncludeUnPosted
        '
        Me.chkIncludeUnPosted.AutoSize = True
        Me.chkIncludeUnPosted.Checked = True
        Me.chkIncludeUnPosted.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIncludeUnPosted.Location = New System.Drawing.Point(572, 109)
        Me.chkIncludeUnPosted.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkIncludeUnPosted.Name = "chkIncludeUnPosted"
        Me.chkIncludeUnPosted.Size = New System.Drawing.Size(225, 24)
        Me.chkIncludeUnPosted.TabIndex = 9
        Me.chkIncludeUnPosted.Text = "Include Unposted Voucher"
        Me.chkIncludeUnPosted.UseVisualStyleBackColor = True
        '
        'cmbFormate
        '
        Me.cmbFormate.FormattingEnabled = True
        Me.cmbFormate.Location = New System.Drawing.Point(166, 102)
        Me.cmbFormate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbFormate.Name = "cmbFormate"
        Me.cmbFormate.Size = New System.Drawing.Size(300, 28)
        Me.cmbFormate.TabIndex = 5
        '
        'lnkRefresh
        '
        Me.lnkRefresh.AutoSize = True
        Me.lnkRefresh.Location = New System.Drawing.Point(490, 106)
        Me.lnkRefresh.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkRefresh.Name = "lnkRefresh"
        Me.lnkRefresh.Size = New System.Drawing.Size(66, 20)
        Me.lnkRefresh.TabIndex = 7
        Me.lnkRefresh.TabStop = True
        Me.lnkRefresh.Text = "Refresh"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(90, 106)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 20)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Layout"
        '
        'pnlFollowUp
        '
        Me.pnlFollowUp.BackColor = System.Drawing.Color.Transparent
        Me.pnlFollowUp.Controls.Add(Me.btnSave)
        Me.pnlFollowUp.Controls.Add(Me.lbldtpFollowUp)
        Me.pnlFollowUp.Controls.Add(Me.dtpFollowUp)
        Me.pnlFollowUp.Controls.Add(Me.lblFollowUpRemakrs)
        Me.pnlFollowUp.Controls.Add(Me.txtFollowUpRemarks)
        Me.pnlFollowUp.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFollowUp.Location = New System.Drawing.Point(0, 786)
        Me.pnlFollowUp.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlFollowUp.Name = "pnlFollowUp"
        Me.pnlFollowUp.Size = New System.Drawing.Size(1274, 149)
        Me.pnlFollowUp.TabIndex = 15
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(1018, 85)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(112, 42)
        Me.btnSave.TabIndex = 4
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'lbldtpFollowUp
        '
        Me.lbldtpFollowUp.AutoSize = True
        Me.lbldtpFollowUp.Location = New System.Drawing.Point(825, 20)
        Me.lbldtpFollowUp.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbldtpFollowUp.Name = "lbldtpFollowUp"
        Me.lbldtpFollowUp.Size = New System.Drawing.Size(114, 20)
        Me.lbldtpFollowUp.TabIndex = 3
        Me.lbldtpFollowUp.Text = "FollowUp Date"
        '
        'dtpFollowUp
        '
        Me.dtpFollowUp.CustomFormat = "dd/MM/yyyy"
        Me.dtpFollowUp.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFollowUp.Location = New System.Drawing.Point(830, 45)
        Me.dtpFollowUp.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpFollowUp.Name = "dtpFollowUp"
        Me.dtpFollowUp.Size = New System.Drawing.Size(298, 26)
        Me.dtpFollowUp.TabIndex = 2
        '
        'lblFollowUpRemakrs
        '
        Me.lblFollowUpRemakrs.AutoSize = True
        Me.lblFollowUpRemakrs.Location = New System.Drawing.Point(4, 20)
        Me.lblFollowUpRemakrs.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFollowUpRemakrs.Name = "lblFollowUpRemakrs"
        Me.lblFollowUpRemakrs.Size = New System.Drawing.Size(143, 20)
        Me.lblFollowUpRemakrs.TabIndex = 1
        Me.lblFollowUpRemakrs.Text = "FollowUp Remarks"
        '
        'txtFollowUpRemarks
        '
        Me.txtFollowUpRemarks.Location = New System.Drawing.Point(158, 15)
        Me.txtFollowUpRemarks.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtFollowUpRemarks.Multiline = True
        Me.txtFollowUpRemarks.Name = "txtFollowUpRemarks"
        Me.txtFollowUpRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtFollowUpRemarks.Size = New System.Drawing.Size(648, 109)
        Me.txtFollowUpRemarks.TabIndex = 0
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.Button1)
        Me.UltraTabPageControl2.Controls.Add(Me.DateTimePicker2)
        Me.UltraTabPageControl2.Controls.Add(Me.DateTimePicker1)
        Me.UltraTabPageControl2.Controls.Add(Me.CtrlGrdBar2)
        Me.UltraTabPageControl2.Controls.Add(Me.GridEX1)
        Me.UltraTabPageControl2.Controls.Add(Me.ToolStrip3)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1274, 935)
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(400, 43)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(112, 35)
        Me.Button1.TabIndex = 10
        Me.Button1.Text = "Show"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.CustomFormat = "dd/MMM/yyyy"
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2.Location = New System.Drawing.Point(208, 48)
        Me.DateTimePicker2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(181, 26)
        Me.DateTimePicker2.TabIndex = 9
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd/MMM/yyyy"
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(16, 48)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(181, 26)
        Me.DateTimePicker1.TabIndex = 8
        '
        'CtrlGrdBar2
        '
        Me.CtrlGrdBar2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar2.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar2.Email = Nothing
        Me.CtrlGrdBar2.FormName = Nothing
        Me.CtrlGrdBar2.Location = New System.Drawing.Point(1218, 0)
        Me.CtrlGrdBar2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar2.MyGrid = Me.GridEX1
        Me.CtrlGrdBar2.Name = "CtrlGrdBar2"
        Me.CtrlGrdBar2.Size = New System.Drawing.Size(57, 38)
        Me.CtrlGrdBar2.TabIndex = 1
        '
        'GridEX1
        '
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        GridEX1_DesignTimeLayout.LayoutString = resources.GetString("GridEX1_DesignTimeLayout.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX1.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.GridEX1.Location = New System.Drawing.Point(0, 88)
        Me.GridEX1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.Size = New System.Drawing.Size(1269, 675)
        Me.GridEX1.TabIndex = 2
        Me.GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'ToolStrip3
        '
        Me.ToolStrip3.AutoSize = False
        Me.ToolStrip3.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnBack, Me.ToolStripButton2})
        Me.ToolStrip3.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip3.Name = "ToolStrip3"
        Me.ToolStrip3.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip3.Size = New System.Drawing.Size(1274, 38)
        Me.ToolStrip3.TabIndex = 0
        Me.ToolStrip3.Text = "ToolStrip3"
        '
        'btnBack
        '
        Me.btnBack.Image = Global.SimpleAccounts.My.Resources.Resources.back
        Me.btnBack.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(76, 35)
        Me.btnBack.Text = "Back"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Image = Global.SimpleAccounts.My.Resources.Resources.print
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(76, 35)
        Me.ToolStripButton2.Text = "Print"
        '
        'BackgroundWorker1
        '
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(79, 22)
        Me.ToolStripButton1.Text = "Send SMS"
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
        Me.UltraTabControl1.Size = New System.Drawing.Size(1276, 962)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 13
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        Appearance7.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        UltraTab1.Appearance = Appearance7
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Simple"
        Appearance8.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        UltraTab2.Appearance = Appearance8
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "Invoice wise"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.VisualStudio2005
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1274, 935)
        '
        'frmGrdRptAgingReceiveablesOld
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1276, 962)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmGrdRptAgingReceiveablesOld"
        Me.Text = "Aging Receivable Old"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.cmbCostCenter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.cmbSubSub, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFollowUp.ResumeLayout(False)
        Me.pnlFollowUp.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip3.ResumeLayout(False)
        Me.ToolStrip3.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbFormate As System.Windows.Forms.ComboBox
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnAddTemplate As System.Windows.Forms.ToolStripButton
    Friend WithEvents lnkRefresh As System.Windows.Forms.LinkLabel
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents chkIncludeUnPosted As System.Windows.Forms.CheckBox
    Friend WithEvents lblSubSub As System.Windows.Forms.Label
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents cmbSubSub As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents btnSMSTempSettings As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSendSMS As System.Windows.Forms.ToolStripButton
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents CtrlGrdBar2 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents ToolStrip3 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnBack As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbCostCenter As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents lblCostCenter As System.Windows.Forms.Label
    Friend WithEvents pnlFollowUp As System.Windows.Forms.Panel
    Friend WithEvents lblFollowUpRemakrs As System.Windows.Forms.Label
    Friend WithEvents txtFollowUpRemarks As System.Windows.Forms.TextBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents lbldtpFollowUp As System.Windows.Forms.Label
    Friend WithEvents dtpFollowUp As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents cmbZone As System.Windows.Forms.ComboBox
    Friend WithEvents cmbBelt As System.Windows.Forms.ComboBox
    Friend WithEvents cmbRegion As System.Windows.Forms.ComboBox
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents btnLoadNew As System.Windows.Forms.Button
End Class

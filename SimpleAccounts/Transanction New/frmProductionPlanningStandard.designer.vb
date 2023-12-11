<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProductionPlanningStandard
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
        Dim grd_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProductionPlanningStandard))
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn11 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ID")
        Dim UltraGridColumn12 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Name")
        Dim UltraGridColumn13 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Territory")
        Dim UltraGridColumn14 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("City")
        Dim UltraGridColumn15 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("State")
        Dim UltraGridColumn16 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("AcId")
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance15 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand2 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn1 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Id")
        Dim UltraGridColumn2 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleDescription")
        Dim UltraGridColumn3 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Size")
        Dim UltraGridColumn4 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Color")
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.lblArticleAliasName = New System.Windows.Forms.Label()
        Me.txtArticleAliasName = New System.Windows.Forms.TextBox()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.rdoName = New System.Windows.Forms.RadioButton()
        Me.rdoCode = New System.Windows.Forms.RadioButton()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cmbVendor = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbPo = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cmbSalesMan = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.dtpStartDate = New System.Windows.Forms.DateTimePicker()
        Me.lblStartDate = New System.Windows.Forms.Label()
        Me.dtpCompletionDate = New System.Windows.Forms.DateTimePicker()
        Me.lblCompletionDate = New System.Windows.Forms.Label()
        Me.txtPONo = New System.Windows.Forms.TextBox()
        Me.dtpPODate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbItem = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtReceivingID = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtPackQty = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbCategory = New System.Windows.Forms.ComboBox()
        Me.cmbUnit = New System.Windows.Forms.ComboBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.txtQty = New System.Windows.Forms.TextBox()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.txtRate = New System.Windows.Forms.TextBox()
        Me.gbTicket = New System.Windows.Forms.GroupBox()
        Me.txtAvailableQty = New System.Windows.Forms.TextBox()
        Me.lblAvailableQty = New System.Windows.Forms.Label()
        Me.btnAddTicket = New System.Windows.Forms.Button()
        Me.txtTicketNo = New System.Windows.Forms.TextBox()
        Me.txtTicketQty = New System.Windows.Forms.TextBox()
        Me.lblQty = New System.Windows.Forms.Label()
        Me.lbldate = New System.Windows.Forms.Label()
        Me.dtpTicketDate = New System.Windows.Forms.DateTimePicker()
        Me.lblTicketNo = New System.Windows.Forms.Label()
        Me.lblProductName = New System.Windows.Forms.Label()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdSaved = New Janus.Windows.GridEX.GridEX()
        Me.txtPaid = New System.Windows.Forms.TextBox()
        Me.txtBalance = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.BtnNew = New System.Windows.Forms.ToolStripButton()
        Me.BtnEdit = New System.Windows.Forms.ToolStripButton()
        Me.BtnSave = New System.Windows.Forms.ToolStripButton()
        Me.BtnDelete = New System.Windows.Forms.ToolStripButton()
        Me.BtnTransferAccounts = New System.Windows.Forms.ToolStripButton()
        Me.BtnPrint = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.BtnLoadAll = New System.Windows.Forms.ToolStripButton()
        Me.BtnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbTask = New System.Windows.Forms.ToolStripButton()
        Me.tsbConfig = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolTip2 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolTip3 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.cmbVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.cmbItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbTicket.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.Panel1)
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.grd)
        Me.UltraTabPageControl1.Controls.Add(Me.txtReceivingID)
        Me.UltraTabPageControl1.Controls.Add(Me.txtPackQty)
        Me.UltraTabPageControl1.Controls.Add(Me.gbTicket)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(890, 589)
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(890, 39)
        Me.pnlHeader.TabIndex = 24
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(11, 6)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(335, 23)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Production Planning Standard"
        '
        'lblArticleAliasName
        '
        Me.lblArticleAliasName.AutoSize = True
        Me.lblArticleAliasName.Location = New System.Drawing.Point(697, 146)
        Me.lblArticleAliasName.Name = "lblArticleAliasName"
        Me.lblArticleAliasName.Size = New System.Drawing.Size(71, 13)
        Me.lblArticleAliasName.TabIndex = 20
        Me.lblArticleAliasName.Text = "Alias Name"
        '
        'txtArticleAliasName
        '
        Me.txtArticleAliasName.Location = New System.Drawing.Point(700, 161)
        Me.txtArticleAliasName.Name = "txtArticleAliasName"
        Me.txtArticleAliasName.Size = New System.Drawing.Size(96, 21)
        Me.txtArticleAliasName.TabIndex = 16
        Me.txtArticleAliasName.TabStop = False
        Me.ToolTip1.SetToolTip(Me.txtArticleAliasName, "Total Amount")
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(589, 91)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 17
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'grd
        '
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grd_DesignTimeLayout.LayoutString = resources.GetString("grd_DesignTimeLayout.LayoutString")
        Me.grd.DesignTimeLayout = grd_DesignTimeLayout
        Me.grd.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.grd.GroupByBoxVisible = False
        Me.grd.Location = New System.Drawing.Point(3, 236)
        Me.grd.Name = "grd"
        Me.grd.Size = New System.Drawing.Size(883, 349)
        Me.grd.TabIndex = 23
        Me.ToolTip1.SetToolTip(Me.grd, "Plan Detail")
        Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'rdoName
        '
        Me.rdoName.AutoSize = True
        Me.rdoName.Location = New System.Drawing.Point(286, 143)
        Me.rdoName.Name = "rdoName"
        Me.rdoName.Size = New System.Drawing.Size(58, 17)
        Me.rdoName.TabIndex = 10
        Me.rdoName.Text = "Name"
        Me.ToolTip1.SetToolTip(Me.rdoName, "Search Item By Name")
        Me.rdoName.UseVisualStyleBackColor = True
        '
        'rdoCode
        '
        Me.rdoCode.AutoSize = True
        Me.rdoCode.Checked = True
        Me.rdoCode.Location = New System.Drawing.Point(222, 143)
        Me.rdoCode.Name = "rdoCode"
        Me.rdoCode.Size = New System.Drawing.Size(55, 17)
        Me.rdoCode.TabIndex = 9
        Me.rdoCode.TabStop = True
        Me.rdoCode.Text = "Code"
        Me.ToolTip1.SetToolTip(Me.rdoCode, "Search Item By Code")
        Me.rdoCode.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtRemarks)
        Me.GroupBox3.Location = New System.Drawing.Point(589, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(221, 109)
        Me.GroupBox3.TabIndex = 4
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Remarks"
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(6, 21)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(209, 74)
        Me.txtRemarks.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.txtRemarks, "Remarks for necessary")
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmbVendor)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.cmbPo)
        Me.GroupBox2.Controls.Add(Me.Label16)
        Me.GroupBox2.Controls.Add(Me.cmbSalesMan)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Location = New System.Drawing.Point(258, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(325, 127)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        '
        'cmbVendor
        '
        Me.cmbVendor.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cmbVendor.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbVendor.CheckedListSettings.CheckStateMember = ""
        Appearance1.BackColor = System.Drawing.Color.White
        Me.cmbVendor.DisplayLayout.Appearance = Appearance1
        UltraGridColumn11.Header.VisiblePosition = 0
        UltraGridColumn11.Hidden = True
        UltraGridColumn12.Header.VisiblePosition = 1
        UltraGridColumn12.Width = 141
        UltraGridColumn13.Header.VisiblePosition = 2
        UltraGridColumn14.Header.VisiblePosition = 3
        UltraGridColumn15.Header.VisiblePosition = 4
        UltraGridColumn16.Header.VisiblePosition = 5
        UltraGridColumn16.Hidden = True
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn11, UltraGridColumn12, UltraGridColumn13, UltraGridColumn14, UltraGridColumn15, UltraGridColumn16})
        Me.cmbVendor.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbVendor.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbVendor.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbVendor.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Appearance12.BackColor = System.Drawing.Color.Transparent
        Me.cmbVendor.DisplayLayout.Override.CardAreaAppearance = Appearance12
        Me.cmbVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbVendor.DisplayLayout.Override.CellPadding = 3
        Me.cmbVendor.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance13.TextHAlignAsString = "Left"
        Me.cmbVendor.DisplayLayout.Override.HeaderAppearance = Appearance13
        Me.cmbVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance14.BorderColor = System.Drawing.Color.LightGray
        Appearance14.TextVAlignAsString = "Middle"
        Me.cmbVendor.DisplayLayout.Override.RowAppearance = Appearance14
        Appearance15.BackColor = System.Drawing.Color.LightSteelBlue
        Appearance15.BorderColor = System.Drawing.Color.Black
        Appearance15.ForeColor = System.Drawing.Color.Black
        Me.cmbVendor.DisplayLayout.Override.SelectedRowAppearance = Appearance15
        Me.cmbVendor.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbVendor.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbVendor.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbVendor.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.cmbVendor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbVendor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbVendor.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbVendor.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbVendor.LimitToList = True
        Me.cmbVendor.Location = New System.Drawing.Point(103, 19)
        Me.cmbVendor.Name = "cmbVendor"
        Me.cmbVendor.Size = New System.Drawing.Size(215, 23)
        Me.cmbVendor.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbVendor, "Select Customer")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Customer"
        '
        'cmbPo
        '
        Me.cmbPo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPo.FormattingEnabled = True
        Me.cmbPo.Location = New System.Drawing.Point(103, 48)
        Me.cmbPo.Name = "cmbPo"
        Me.cmbPo.Size = New System.Drawing.Size(215, 21)
        Me.cmbPo.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.cmbPo, "Select Sale order ")
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(8, 51)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(69, 13)
        Me.Label16.TabIndex = 2
        Me.Label16.Text = "Sale Order"
        '
        'cmbSalesMan
        '
        Me.cmbSalesMan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSalesMan.FormattingEnabled = True
        Me.cmbSalesMan.Location = New System.Drawing.Point(103, 74)
        Me.cmbSalesMan.Name = "cmbSalesMan"
        Me.cmbSalesMan.Size = New System.Drawing.Size(215, 21)
        Me.cmbSalesMan.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.cmbSalesMan, "Select Sale Person")
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(8, 78)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(75, 13)
        Me.Label15.TabIndex = 4
        Me.Label15.Text = "Sale Person"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.dtpStartDate)
        Me.GroupBox1.Controls.Add(Me.lblStartDate)
        Me.GroupBox1.Controls.Add(Me.dtpCompletionDate)
        Me.GroupBox1.Controls.Add(Me.lblCompletionDate)
        Me.GroupBox1.Controls.Add(Me.txtPONo)
        Me.GroupBox1.Controls.Add(Me.dtpPODate)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(249, 127)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'dtpStartDate
        '
        Me.dtpStartDate.Checked = False
        Me.dtpStartDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStartDate.Location = New System.Drawing.Point(109, 74)
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.ShowCheckBox = True
        Me.dtpStartDate.Size = New System.Drawing.Size(135, 21)
        Me.dtpStartDate.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.dtpStartDate, "Select start date")
        '
        'lblStartDate
        '
        Me.lblStartDate.AutoSize = True
        Me.lblStartDate.Location = New System.Drawing.Point(3, 77)
        Me.lblStartDate.Name = "lblStartDate"
        Me.lblStartDate.Size = New System.Drawing.Size(66, 13)
        Me.lblStartDate.TabIndex = 4
        Me.lblStartDate.Text = "Start Date"
        '
        'dtpCompletionDate
        '
        Me.dtpCompletionDate.Checked = False
        Me.dtpCompletionDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpCompletionDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpCompletionDate.Location = New System.Drawing.Point(109, 101)
        Me.dtpCompletionDate.Name = "dtpCompletionDate"
        Me.dtpCompletionDate.ShowCheckBox = True
        Me.dtpCompletionDate.Size = New System.Drawing.Size(135, 21)
        Me.dtpCompletionDate.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.dtpCompletionDate, "Completion Date")
        '
        'lblCompletionDate
        '
        Me.lblCompletionDate.AutoSize = True
        Me.lblCompletionDate.Location = New System.Drawing.Point(3, 104)
        Me.lblCompletionDate.Name = "lblCompletionDate"
        Me.lblCompletionDate.Size = New System.Drawing.Size(103, 13)
        Me.lblCompletionDate.TabIndex = 6
        Me.lblCompletionDate.Text = "Completion Date"
        '
        'txtPONo
        '
        Me.txtPONo.Location = New System.Drawing.Point(109, 20)
        Me.txtPONo.Name = "txtPONo"
        Me.txtPONo.ReadOnly = True
        Me.txtPONo.Size = New System.Drawing.Size(134, 21)
        Me.txtPONo.TabIndex = 1
        Me.txtPONo.TabStop = False
        Me.ToolTip1.SetToolTip(Me.txtPONo, "Plan No")
        '
        'dtpPODate
        '
        Me.dtpPODate.CustomFormat = "dd/MM/yyyy"
        Me.dtpPODate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPODate.Location = New System.Drawing.Point(109, 47)
        Me.dtpPODate.Name = "dtpPODate"
        Me.dtpPODate.Size = New System.Drawing.Size(134, 21)
        Me.dtpPODate.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.dtpPODate, "Plan Date")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(1, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Plan Date"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(1, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Plan No"
        '
        'cmbItem
        '
        Me.cmbItem.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cmbItem.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbItem.CheckedListSettings.CheckStateMember = ""
        Appearance2.BackColor = System.Drawing.Color.White
        Appearance2.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        Me.cmbItem.DisplayLayout.Appearance = Appearance2
        UltraGridColumn1.Header.VisiblePosition = 0
        UltraGridColumn1.Hidden = True
        UltraGridColumn2.Header.VisiblePosition = 1
        UltraGridColumn3.Header.VisiblePosition = 2
        UltraGridColumn4.Header.VisiblePosition = 3
        UltraGridBand2.Columns.AddRange(New Object() {UltraGridColumn1, UltraGridColumn2, UltraGridColumn3, UltraGridColumn4})
        Me.cmbItem.DisplayLayout.BandsSerializer.Add(UltraGridBand2)
        Me.cmbItem.DisplayLayout.InterBandSpacing = 10
        Me.cmbItem.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbItem.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbItem.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Appearance3.BackColor = System.Drawing.Color.Transparent
        Me.cmbItem.DisplayLayout.Override.CardAreaAppearance = Appearance3
        Me.cmbItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbItem.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance4.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance4.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance4.ForeColor = System.Drawing.Color.White
        Appearance4.TextHAlignAsString = "Left"
        Appearance4.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent
        Me.cmbItem.DisplayLayout.Override.HeaderAppearance = Appearance4
        Me.cmbItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance5.BorderColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbItem.DisplayLayout.Override.RowAppearance = Appearance5
        Appearance6.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance6.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.cmbItem.DisplayLayout.Override.RowSelectorAppearance = Appearance6
        Me.cmbItem.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbItem.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance7.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance7.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance7.ForeColor = System.Drawing.Color.Black
        Me.cmbItem.DisplayLayout.Override.SelectedRowAppearance = Appearance7
        Me.cmbItem.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbItem.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbItem.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbItem.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbItem.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.cmbItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbItem.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbItem.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbItem.LimitToList = True
        Me.cmbItem.Location = New System.Drawing.Point(141, 160)
        Me.cmbItem.MaxDropDownItems = 20
        Me.cmbItem.Name = "cmbItem"
        Me.cmbItem.Size = New System.Drawing.Size(200, 23)
        Me.cmbItem.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.cmbItem, "Select Item")
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(4, 145)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(54, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Location"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(138, 146)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(34, 13)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Item"
        '
        'txtReceivingID
        '
        Me.txtReceivingID.Location = New System.Drawing.Point(705, 18)
        Me.txtReceivingID.Name = "txtReceivingID"
        Me.txtReceivingID.Size = New System.Drawing.Size(37, 21)
        Me.txtReceivingID.TabIndex = 3
        Me.txtReceivingID.TabStop = False
        Me.txtReceivingID.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(347, 145)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(29, 13)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Unit"
        '
        'txtPackQty
        '
        Me.txtPackQty.Location = New System.Drawing.Point(703, 18)
        Me.txtPackQty.Name = "txtPackQty"
        Me.txtPackQty.Size = New System.Drawing.Size(40, 21)
        Me.txtPackQty.TabIndex = 2
        Me.txtPackQty.TabStop = False
        Me.txtPackQty.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(434, 146)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(27, 13)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Qty"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(517, 147)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(33, 13)
        Me.Label9.TabIndex = 15
        Me.Label9.Text = "Rate"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(599, 147)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(34, 13)
        Me.Label10.TabIndex = 18
        Me.Label10.Text = "Total"
        '
        'cmbCategory
        '
        Me.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategory.FormattingEnabled = True
        Me.cmbCategory.Location = New System.Drawing.Point(7, 161)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(128, 21)
        Me.cmbCategory.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.cmbCategory, "Select Location")
        '
        'cmbUnit
        '
        Me.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUnit.FormattingEnabled = True
        Me.cmbUnit.Items.AddRange(New Object() {"Loose", "Pack"})
        Me.cmbUnit.Location = New System.Drawing.Point(347, 161)
        Me.cmbUnit.Name = "cmbUnit"
        Me.cmbUnit.Size = New System.Drawing.Size(81, 21)
        Me.cmbUnit.TabIndex = 12
        Me.cmbUnit.TabStop = False
        Me.ToolTip1.SetToolTip(Me.cmbUnit, "Select Unit")
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(802, 160)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(43, 23)
        Me.btnAdd.TabIndex = 17
        Me.btnAdd.TabStop = False
        Me.btnAdd.Text = "+"
        Me.ToolTip1.SetToolTip(Me.btnAdd, "Add Item To Grid")
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'txtQty
        '
        Me.txtQty.Location = New System.Drawing.Point(434, 161)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(77, 21)
        Me.txtQty.TabIndex = 13
        Me.txtQty.TabStop = False
        Me.ToolTip1.SetToolTip(Me.txtQty, "Quantity")
        '
        'txtTotal
        '
        Me.txtTotal.Location = New System.Drawing.Point(599, 161)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.Size = New System.Drawing.Size(96, 21)
        Me.txtTotal.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.txtTotal, "Total Amount")
        '
        'txtRate
        '
        Me.txtRate.Location = New System.Drawing.Point(517, 161)
        Me.txtRate.Name = "txtRate"
        Me.txtRate.Size = New System.Drawing.Size(76, 21)
        Me.txtRate.TabIndex = 14
        Me.txtRate.TabStop = False
        Me.ToolTip1.SetToolTip(Me.txtRate, "Rate")
        '
        'gbTicket
        '
        Me.gbTicket.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbTicket.Controls.Add(Me.txtAvailableQty)
        Me.gbTicket.Controls.Add(Me.lblAvailableQty)
        Me.gbTicket.Controls.Add(Me.btnAddTicket)
        Me.gbTicket.Controls.Add(Me.txtTicketNo)
        Me.gbTicket.Controls.Add(Me.txtTicketQty)
        Me.gbTicket.Controls.Add(Me.lblQty)
        Me.gbTicket.Controls.Add(Me.lbldate)
        Me.gbTicket.Controls.Add(Me.dtpTicketDate)
        Me.gbTicket.Controls.Add(Me.lblTicketNo)
        Me.gbTicket.Controls.Add(Me.lblProductName)
        Me.gbTicket.Location = New System.Drawing.Point(3, 412)
        Me.gbTicket.Name = "gbTicket"
        Me.gbTicket.Size = New System.Drawing.Size(885, 179)
        Me.gbTicket.TabIndex = 22
        Me.gbTicket.TabStop = False
        Me.gbTicket.Text = "Create Tickets"
        Me.gbTicket.Visible = False
        '
        'txtAvailableQty
        '
        Me.txtAvailableQty.Enabled = False
        Me.txtAvailableQty.Location = New System.Drawing.Point(341, 34)
        Me.txtAvailableQty.Name = "txtAvailableQty"
        Me.txtAvailableQty.Size = New System.Drawing.Size(106, 21)
        Me.txtAvailableQty.TabIndex = 25
        '
        'lblAvailableQty
        '
        Me.lblAvailableQty.AutoSize = True
        Me.lblAvailableQty.Location = New System.Drawing.Point(338, 17)
        Me.lblAvailableQty.Name = "lblAvailableQty"
        Me.lblAvailableQty.Size = New System.Drawing.Size(83, 13)
        Me.lblAvailableQty.TabIndex = 24
        Me.lblAvailableQty.Text = "Available Qty"
        '
        'btnAddTicket
        '
        Me.btnAddTicket.Location = New System.Drawing.Point(680, 32)
        Me.btnAddTicket.Name = "btnAddTicket"
        Me.btnAddTicket.Size = New System.Drawing.Size(43, 23)
        Me.btnAddTicket.TabIndex = 21
        Me.btnAddTicket.Text = "+"
        Me.ToolTip1.SetToolTip(Me.btnAddTicket, "Add Item To Grid")
        Me.btnAddTicket.UseVisualStyleBackColor = True
        '
        'txtTicketNo
        '
        Me.txtTicketNo.Enabled = False
        Me.txtTicketNo.Location = New System.Drawing.Point(13, 34)
        Me.txtTicketNo.Name = "txtTicketNo"
        Me.txtTicketNo.Size = New System.Drawing.Size(106, 21)
        Me.txtTicketNo.TabIndex = 9
        '
        'txtTicketQty
        '
        Me.txtTicketQty.Location = New System.Drawing.Point(596, 33)
        Me.txtTicketQty.Name = "txtTicketQty"
        Me.txtTicketQty.Size = New System.Drawing.Size(78, 21)
        Me.txtTicketQty.TabIndex = 7
        Me.txtTicketQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblQty
        '
        Me.lblQty.AutoSize = True
        Me.lblQty.Location = New System.Drawing.Point(593, 17)
        Me.lblQty.Name = "lblQty"
        Me.lblQty.Size = New System.Drawing.Size(27, 13)
        Me.lblQty.TabIndex = 6
        Me.lblQty.Text = "Qty"
        '
        'lbldate
        '
        Me.lbldate.AutoSize = True
        Me.lbldate.Location = New System.Drawing.Point(452, 17)
        Me.lbldate.Name = "lbldate"
        Me.lbldate.Size = New System.Drawing.Size(34, 13)
        Me.lbldate.TabIndex = 5
        Me.lbldate.Text = "Date"
        '
        'dtpTicketDate
        '
        Me.dtpTicketDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpTicketDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTicketDate.Location = New System.Drawing.Point(455, 33)
        Me.dtpTicketDate.Name = "dtpTicketDate"
        Me.dtpTicketDate.Size = New System.Drawing.Size(134, 21)
        Me.dtpTicketDate.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.dtpTicketDate, "Ticket Date")
        '
        'lblTicketNo
        '
        Me.lblTicketNo.AutoSize = True
        Me.lblTicketNo.Location = New System.Drawing.Point(10, 17)
        Me.lblTicketNo.Name = "lblTicketNo"
        Me.lblTicketNo.Size = New System.Drawing.Size(60, 13)
        Me.lblTicketNo.TabIndex = 1
        Me.lblTicketNo.Text = "Ticket No"
        '
        'lblProductName
        '
        Me.lblProductName.AutoSize = True
        Me.lblProductName.Location = New System.Drawing.Point(124, 17)
        Me.lblProductName.Name = "lblProductName"
        Me.lblProductName.Size = New System.Drawing.Size(87, 13)
        Me.lblProductName.TabIndex = 0
        Me.lblProductName.Text = "Product Name"
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grdSaved)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(890, 589)
        '
        'grdSaved
        '
        Me.grdSaved.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdSaved.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSaved.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdSaved.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdSaved.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdSaved.GroupByBoxVisible = False
        Me.grdSaved.Location = New System.Drawing.Point(0, 0)
        Me.grdSaved.Name = "grdSaved"
        Me.grdSaved.RecordNavigator = True
        Me.grdSaved.Size = New System.Drawing.Size(890, 589)
        Me.grdSaved.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.grdSaved, "Saved Record Detail")
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'txtPaid
        '
        Me.txtPaid.Location = New System.Drawing.Point(550, 409)
        Me.txtPaid.Name = "txtPaid"
        Me.txtPaid.Size = New System.Drawing.Size(131, 21)
        Me.txtPaid.TabIndex = 3
        Me.txtPaid.Visible = False
        '
        'txtBalance
        '
        Me.txtBalance.Location = New System.Drawing.Point(550, 435)
        Me.txtBalance.Name = "txtBalance"
        Me.txtBalance.Size = New System.Drawing.Size(131, 21)
        Me.txtBalance.TabIndex = 5
        Me.txtBalance.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(472, 412)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(64, 13)
        Me.Label12.TabIndex = 2
        Me.Label12.Text = "Cash Paid"
        Me.Label12.Visible = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(472, 435)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(52, 13)
        Me.Label13.TabIndex = 4
        Me.Label13.Text = "Balance"
        Me.Label13.Visible = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BtnNew, Me.BtnEdit, Me.BtnSave, Me.BtnDelete, Me.BtnTransferAccounts, Me.BtnPrint, Me.toolStripSeparator, Me.HelpToolStripButton, Me.BtnLoadAll, Me.BtnRefresh, Me.ToolStripSeparator2, Me.tsbTask, Me.tsbConfig, Me.ToolStripSeparator1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(892, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'BtnNew
        '
        Me.BtnNew.Image = CType(resources.GetObject("BtnNew.Image"), System.Drawing.Image)
        Me.BtnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnNew.Name = "BtnNew"
        Me.BtnNew.Size = New System.Drawing.Size(51, 22)
        Me.BtnNew.Text = "&New"
        '
        'BtnEdit
        '
        Me.BtnEdit.Image = CType(resources.GetObject("BtnEdit.Image"), System.Drawing.Image)
        Me.BtnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnEdit.Name = "BtnEdit"
        Me.BtnEdit.Size = New System.Drawing.Size(47, 22)
        Me.BtnEdit.Text = "&Edit"
        '
        'BtnSave
        '
        Me.BtnSave.Image = CType(resources.GetObject("BtnSave.Image"), System.Drawing.Image)
        Me.BtnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(51, 22)
        Me.BtnSave.Text = "&Save"
        '
        'BtnDelete
        '
        Me.BtnDelete.Image = CType(resources.GetObject("BtnDelete.Image"), System.Drawing.Image)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.RightToLeftAutoMirrorImage = True
        Me.BtnDelete.Size = New System.Drawing.Size(60, 22)
        Me.BtnDelete.Text = "&Delete"
        '
        'BtnTransferAccounts
        '
        Me.BtnTransferAccounts.Image = Global.SimpleAccounts.My.Resources.Resources.sendcontactdetails
        Me.BtnTransferAccounts.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnTransferAccounts.Name = "BtnTransferAccounts"
        Me.BtnTransferAccounts.Size = New System.Drawing.Size(136, 22)
        Me.BtnTransferAccounts.Text = "Transfer to Accounts"
        Me.BtnTransferAccounts.Visible = False
        '
        'BtnPrint
        '
        Me.BtnPrint.Image = CType(resources.GetObject("BtnPrint.Image"), System.Drawing.Image)
        Me.BtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(52, 22)
        Me.BtnPrint.Text = "&Print"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'HelpToolStripButton
        '
        Me.HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HelpToolStripButton.Image = CType(resources.GetObject("HelpToolStripButton.Image"), System.Drawing.Image)
        Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton.Name = "HelpToolStripButton"
        Me.HelpToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.HelpToolStripButton.Text = "He&lp"
        '
        'BtnLoadAll
        '
        Me.BtnLoadAll.Image = Global.SimpleAccounts.My.Resources.Resources.sendcontactdetails
        Me.BtnLoadAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnLoadAll.Name = "BtnLoadAll"
        Me.BtnLoadAll.Size = New System.Drawing.Size(70, 22)
        Me.BtnLoadAll.Text = "Load All"
        '
        'BtnRefresh
        '
        Me.BtnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.BtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnRefresh.Name = "BtnRefresh"
        Me.BtnRefresh.Size = New System.Drawing.Size(66, 22)
        Me.BtnRefresh.Text = "Refresh"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'tsbTask
        '
        Me.tsbTask.Image = Global.SimpleAccounts.My.Resources.Resources.Untitled_1
        Me.tsbTask.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbTask.Name = "tsbTask"
        Me.tsbTask.Size = New System.Drawing.Size(88, 22)
        Me.tsbTask.Text = "Task Assign"
        '
        'tsbConfig
        '
        Me.tsbConfig.Image = Global.SimpleAccounts.My.Resources.Resources.Advanced_Options
        Me.tsbConfig.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbConfig.Name = "tsbConfig"
        Me.tsbConfig.Size = New System.Drawing.Size(63, 22)
        Me.tsbConfig.Text = "Config"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
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
        Me.UltraTabControl1.Size = New System.Drawing.Size(892, 610)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 1
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Production Planning"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "History"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(890, 589)
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(854, 0)
        Me.CtrlGrdBar1.MyGrid = Me.grdSaved
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(38, 25)
        Me.CtrlGrdBar1.TabIndex = 6
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.lblProgress)
        Me.Panel1.Controls.Add(Me.lblArticleAliasName)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Controls.Add(Me.txtArticleAliasName)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.cmbCategory)
        Me.Panel1.Controls.Add(Me.rdoName)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.cmbUnit)
        Me.Panel1.Controls.Add(Me.rdoCode)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.btnAdd)
        Me.Panel1.Controls.Add(Me.cmbItem)
        Me.Panel1.Controls.Add(Me.txtQty)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.txtTotal)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.txtRate)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 39)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(890, 190)
        Me.Panel1.TabIndex = 25
        '
        'frmProductionPlanningStandard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(892, 635)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtBalance)
        Me.Controls.Add(Me.txtPaid)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmProductionPlanningStandard"
        Me.Tag = "frmProductionPlanningStandard"
        Me.Text = " Production Plan Standard"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.cmbVendor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.cmbItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbTicket.ResumeLayout(False)
        Me.gbTicket.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPONo As System.Windows.Forms.TextBox
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents dtpPODate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmbCategory As System.Windows.Forms.ComboBox
    Friend WithEvents cmbUnit As System.Windows.Forms.ComboBox
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents txtRate As System.Windows.Forms.TextBox
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents txtPaid As System.Windows.Forms.TextBox
    Friend WithEvents txtBalance As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtPackQty As System.Windows.Forms.TextBox
    Friend WithEvents txtReceivingID As System.Windows.Forms.TextBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents BtnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents BtnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbSalesMan As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents cmbItem As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents BtnTransferAccounts As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbPo As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cmbVendor As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grdSaved As Janus.Windows.GridEX.GridEX
    Friend WithEvents BtnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnLoadAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents rdoName As System.Windows.Forms.RadioButton
    Friend WithEvents rdoCode As System.Windows.Forms.RadioButton
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents ToolTip2 As System.Windows.Forms.ToolTip
    Friend WithEvents ToolTip3 As System.Windows.Forms.ToolTip
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbTask As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbConfig As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents gbTicket As System.Windows.Forms.GroupBox
    Friend WithEvents txtTicketNo As System.Windows.Forms.TextBox
    Friend WithEvents txtTicketQty As System.Windows.Forms.TextBox
    Friend WithEvents lblQty As System.Windows.Forms.Label
    Friend WithEvents lbldate As System.Windows.Forms.Label
    Friend WithEvents dtpTicketDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblTicketNo As System.Windows.Forms.Label
    Friend WithEvents lblProductName As System.Windows.Forms.Label
    Friend WithEvents btnAddTicket As System.Windows.Forms.Button
    Friend WithEvents txtAvailableQty As System.Windows.Forms.TextBox
    Friend WithEvents lblAvailableQty As System.Windows.Forms.Label
    Friend WithEvents lblArticleAliasName As System.Windows.Forms.Label
    Friend WithEvents txtArticleAliasName As System.Windows.Forms.TextBox
    Friend WithEvents dtpCompletionDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblCompletionDate As System.Windows.Forms.Label
    Friend WithEvents dtpStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblStartDate As System.Windows.Forms.Label
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel

End Class

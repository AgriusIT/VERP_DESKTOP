<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmployeeWiseLedger
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmployeeWiseLedger))
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn1 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("coa_detail_id")
        Dim UltraGridColumn2 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("detail_title")
        Dim UltraGridColumn3 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("detail_code")
        Dim UltraGridColumn4 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("account_type")
        Dim UltraGridColumn5 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("sub_sub_title")
        Dim UltraGridColumn6 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("sub_title")
        Dim UltraGridColumn7 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("main_title")
        Dim UltraGridColumn8 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("main_type")
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim grdLedger_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab3 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.optByCode = New System.Windows.Forms.RadioButton()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        Me.cmbAccount = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.lnkRefresh = New System.Windows.Forms.LinkLabel()
        Me.optByName = New System.Windows.Forms.RadioButton()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.pnlCost = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.rbActive = New System.Windows.Forms.RadioButton()
        Me.rbInactive = New System.Windows.Forms.RadioButton()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.lstEmployee = New SimpleAccounts.uiListControl()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.cmbCurrency = New System.Windows.Forms.ComboBox()
        Me.chkMultiCurrency = New System.Windows.Forms.CheckBox()
        Me.chkUnPostedVouchers = New System.Windows.Forms.CheckBox()
        Me.chkIncludeCostCenter = New System.Windows.Forms.CheckBox()
        Me.lblAdvanceRequest = New System.Windows.Forms.Label()
        Me.cmbAdvanceRequest = New System.Windows.Forms.ComboBox()
        Me.lblCostCenter = New System.Windows.Forms.Label()
        Me.cmbCostCenter = New System.Windows.Forms.ComboBox()
        Me.cmbCostCenterHead = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.optSummary = New System.Windows.Forms.RadioButton()
        Me.optDetail = New System.Windows.Forms.RadioButton()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.UltraTabPageControl3 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cmbCurrCostCentre = New System.Windows.Forms.ComboBox()
        Me.chkIncUnPosted = New System.Windows.Forms.CheckBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.rbtSummary = New System.Windows.Forms.RadioButton()
        Me.rbtDetail = New System.Windows.Forms.RadioButton()
        Me.btnGo2 = New System.Windows.Forms.Button()
        Me.btnGo1 = New System.Windows.Forms.Button()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.rbtPrint = New System.Windows.Forms.RadioButton()
        Me.rbtView = New System.Windows.Forms.RadioButton()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rbtIndividual = New System.Windows.Forms.RadioButton()
        Me.rbtContinues = New System.Windows.Forms.RadioButton()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.btnRemove = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.lstAddedAccounts = New SimpleAccounts.uiListControl()
        Me.lstAccounts = New SimpleAccounts.uiListControl()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.lblCurrency = New System.Windows.Forms.Label()
        Me.lblBalance = New System.Windows.Forms.Label()
        Me.lblTotalCredit = New System.Windows.Forms.Label()
        Me.lblTotalDebit = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblToDate = New System.Windows.Forms.Label()
        Me.lblFromDate = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.lblAccount_Code = New System.Windows.Forms.Label()
        Me.lblAccount_Title = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblOpeningBalance = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.grdLedger = New Janus.Windows.GridEX.GridEX()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.btnLedgerPrint = New System.Windows.Forms.ToolStripSplitButton()
        Me.DetailLederToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SummaryLedgerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnOpenVoucher = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.BackgroundWorker2 = New System.ComponentModel.BackgroundWorker()
        Me.CtrlGrdBar3 = New SimpleAccounts.CtrlGrdBar()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        CType(Me.cmbAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.pnlCost.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.UltraTabPageControl3.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.grdLedger, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.optByCode)
        Me.UltraTabPageControl1.Controls.Add(Me.DateTimePicker1)
        Me.UltraTabPageControl1.Controls.Add(Me.DateTimePicker2)
        Me.UltraTabPageControl1.Controls.Add(Me.Label1)
        Me.UltraTabPageControl1.Controls.Add(Me.Label2)
        Me.UltraTabPageControl1.Controls.Add(Me.Label4)
        Me.UltraTabPageControl1.Controls.Add(Me.Label6)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbPeriod)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbAccount)
        Me.UltraTabPageControl1.Controls.Add(Me.lnkRefresh)
        Me.UltraTabPageControl1.Controls.Add(Me.optByName)
        Me.UltraTabPageControl1.Controls.Add(Me.FlowLayoutPanel1)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1060, 635)
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnClose)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1060, 47)
        Me.pnlHeader.TabIndex = 0
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
        Me.btnClose.Location = New System.Drawing.Point(1016, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(36, 30)
        Me.btnClose.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.btnClose, "close")
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(11, 14)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(357, 36)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Employee Ledger Report"
        '
        'optByCode
        '
        Me.optByCode.AutoSize = True
        Me.optByCode.BackColor = System.Drawing.Color.Transparent
        Me.optByCode.Location = New System.Drawing.Point(123, 52)
        Me.optByCode.Name = "optByCode"
        Me.optByCode.Size = New System.Drawing.Size(105, 24)
        Me.optByCode.TabIndex = 1
        Me.optByCode.Text = "By Code"
        Me.ToolTip1.SetToolTip(Me.optByCode, "Search Account By Code")
        Me.optByCode.UseVisualStyleBackColor = False
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker1.Location = New System.Drawing.Point(124, 127)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(140, 28)
        Me.DateTimePicker1.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.DateTimePicker1, "From Date")
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateTimePicker2.Location = New System.Drawing.Point(124, 153)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(140, 28)
        Me.DateTimePicker2.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.DateTimePicker2, "To Date")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(14, 130)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 20)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "From Date"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(14, 156)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 20)
        Me.Label2.TabIndex = 10
        Me.Label2.Text = "To Date:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(14, 104)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(62, 20)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Period"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(14, 78)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(121, 20)
        Me.Label6.TabIndex = 4
        Me.Label6.Text = "Account Title"
        '
        'cmbPeriod
        '
        Me.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPeriod.FormattingEnabled = True
        Me.cmbPeriod.Items.AddRange(New Object() {"Today", "Yesterday", "Current Week", "Current Month", "Current Year"})
        Me.cmbPeriod.Location = New System.Drawing.Point(124, 101)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(289, 28)
        Me.cmbPeriod.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.cmbPeriod, "Select Period And Gets Date Range")
        '
        'cmbAccount
        '
        Appearance1.BackColor = System.Drawing.Color.White
        Me.cmbAccount.Appearance = Appearance1
        Me.cmbAccount.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append
        Me.cmbAccount.CheckedListSettings.CheckStateMember = ""
        Appearance2.BackColor = System.Drawing.Color.White
        Me.cmbAccount.DisplayLayout.Appearance = Appearance2
        UltraGridColumn1.Header.Caption = "ID"
        UltraGridColumn1.Header.VisiblePosition = 0
        UltraGridColumn1.Hidden = True
        UltraGridColumn1.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(8, 0)
        UltraGridColumn2.Header.Caption = "Account"
        UltraGridColumn2.Header.VisiblePosition = 1
        UltraGridColumn2.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(128, 0)
        UltraGridColumn3.Header.Caption = "Code"
        UltraGridColumn3.Header.VisiblePosition = 2
        UltraGridColumn3.Width = 125
        UltraGridColumn4.Header.Caption = "Type"
        UltraGridColumn4.Header.VisiblePosition = 3
        UltraGridColumn4.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(96, 0)
        UltraGridColumn5.Header.Caption = "Sub Sub Ac"
        UltraGridColumn5.Header.VisiblePosition = 4
        UltraGridColumn5.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(106, 0)
        UltraGridColumn6.Header.Caption = "Sub Ac"
        UltraGridColumn6.Header.VisiblePosition = 5
        UltraGridColumn6.Hidden = True
        UltraGridColumn6.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(102, 0)
        UltraGridColumn7.Header.Caption = "Main Ac"
        UltraGridColumn7.Header.VisiblePosition = 6
        UltraGridColumn7.Hidden = True
        UltraGridColumn7.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(82, 0)
        UltraGridColumn8.Header.Caption = "Ac Head"
        UltraGridColumn8.Header.VisiblePosition = 7
        UltraGridColumn8.Hidden = True
        UltraGridColumn8.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(84, 0)
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn1, UltraGridColumn2, UltraGridColumn3, UltraGridColumn4, UltraGridColumn5, UltraGridColumn6, UltraGridColumn7, UltraGridColumn8})
        Me.cmbAccount.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbAccount.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbAccount.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbAccount.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Appearance3.BackColor = System.Drawing.Color.Transparent
        Me.cmbAccount.DisplayLayout.Override.CardAreaAppearance = Appearance3
        Me.cmbAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbAccount.DisplayLayout.Override.CellPadding = 3
        Me.cmbAccount.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance4.TextHAlignAsString = "Left"
        Me.cmbAccount.DisplayLayout.Override.HeaderAppearance = Appearance4
        Me.cmbAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance5.BorderColor = System.Drawing.Color.LightGray
        Appearance5.TextVAlignAsString = "Middle"
        Me.cmbAccount.DisplayLayout.Override.RowAppearance = Appearance5
        Appearance6.BackColor = System.Drawing.Color.LightSteelBlue
        Appearance6.BorderColor = System.Drawing.Color.Black
        Appearance6.ForeColor = System.Drawing.Color.Black
        Me.cmbAccount.DisplayLayout.Override.SelectedRowAppearance = Appearance6
        Me.cmbAccount.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbAccount.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbAccount.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbAccount.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.cmbAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbAccount.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbAccount.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbAccount.LimitToList = True
        Me.cmbAccount.Location = New System.Drawing.Point(124, 73)
        Me.cmbAccount.MaxDropDownItems = 16
        Me.cmbAccount.Name = "cmbAccount"
        Me.cmbAccount.Size = New System.Drawing.Size(289, 31)
        Me.cmbAccount.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.cmbAccount, "Select Any Account")
        '
        'lnkRefresh
        '
        Me.lnkRefresh.AutoSize = True
        Me.lnkRefresh.Location = New System.Drawing.Point(308, 54)
        Me.lnkRefresh.Name = "lnkRefresh"
        Me.lnkRefresh.Size = New System.Drawing.Size(90, 20)
        Me.lnkRefresh.TabIndex = 3
        Me.lnkRefresh.TabStop = True
        Me.lnkRefresh.Text = "(Refresh)"
        '
        'optByName
        '
        Me.optByName.AutoSize = True
        Me.optByName.BackColor = System.Drawing.Color.Transparent
        Me.optByName.Checked = True
        Me.optByName.Location = New System.Drawing.Point(216, 52)
        Me.optByName.Name = "optByName"
        Me.optByName.Size = New System.Drawing.Size(112, 24)
        Me.optByName.TabIndex = 2
        Me.optByName.TabStop = True
        Me.optByName.Text = "By Name"
        Me.ToolTip1.SetToolTip(Me.optByName, "Search Account By Name")
        Me.optByName.UseVisualStyleBackColor = False
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.BackColor = System.Drawing.Color.Transparent
        Me.FlowLayoutPanel1.Controls.Add(Me.pnlCost)
        Me.FlowLayoutPanel1.Controls.Add(Me.Panel1)
        Me.FlowLayoutPanel1.Controls.Add(Me.TableLayoutPanel1)
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(8, 177)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(425, 443)
        Me.FlowLayoutPanel1.TabIndex = 12
        '
        'pnlCost
        '
        Me.pnlCost.Controls.Add(Me.Panel3)
        Me.pnlCost.Controls.Add(Me.Label20)
        Me.pnlCost.Controls.Add(Me.Label30)
        Me.pnlCost.Controls.Add(Me.lstEmployee)
        Me.pnlCost.Controls.Add(Me.txtSearch)
        Me.pnlCost.Controls.Add(Me.cmbCurrency)
        Me.pnlCost.Controls.Add(Me.chkMultiCurrency)
        Me.pnlCost.Controls.Add(Me.chkUnPostedVouchers)
        Me.pnlCost.Controls.Add(Me.chkIncludeCostCenter)
        Me.pnlCost.Controls.Add(Me.lblAdvanceRequest)
        Me.pnlCost.Controls.Add(Me.cmbAdvanceRequest)
        Me.pnlCost.Controls.Add(Me.lblCostCenter)
        Me.pnlCost.Controls.Add(Me.cmbCostCenter)
        Me.pnlCost.Controls.Add(Me.cmbCostCenterHead)
        Me.pnlCost.Controls.Add(Me.Label3)
        Me.pnlCost.Location = New System.Drawing.Point(3, 3)
        Me.pnlCost.Name = "pnlCost"
        Me.pnlCost.Size = New System.Drawing.Size(406, 391)
        Me.pnlCost.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.rbActive)
        Me.Panel3.Controls.Add(Me.rbInactive)
        Me.Panel3.Location = New System.Drawing.Point(101, 170)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(200, 23)
        Me.Panel3.TabIndex = 11
        '
        'rbActive
        '
        Me.rbActive.AutoSize = True
        Me.rbActive.Checked = True
        Me.rbActive.Location = New System.Drawing.Point(15, 3)
        Me.rbActive.Name = "rbActive"
        Me.rbActive.Size = New System.Drawing.Size(87, 24)
        Me.rbActive.TabIndex = 0
        Me.rbActive.TabStop = True
        Me.rbActive.Text = "Active"
        Me.ToolTip1.SetToolTip(Me.rbActive, "Search Account By Code")
        Me.rbActive.UseVisualStyleBackColor = True
        '
        'rbInactive
        '
        Me.rbInactive.AutoSize = True
        Me.rbInactive.Location = New System.Drawing.Point(108, 3)
        Me.rbInactive.Name = "rbInactive"
        Me.rbInactive.Size = New System.Drawing.Size(106, 24)
        Me.rbInactive.TabIndex = 1
        Me.rbInactive.Text = "InActive"
        Me.ToolTip1.SetToolTip(Me.rbInactive, "Search Account By Name")
        Me.rbInactive.UseVisualStyleBackColor = True
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(2, 365)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(113, 20)
        Me.Label20.TabIndex = 13
        Me.Label20.Text = "Search Emp"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(3, 147)
        Me.Label30.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(94, 20)
        Me.Label30.TabIndex = 9
        Me.Label30.Text = "Currency:"
        '
        'lstEmployee
        '
        Me.lstEmployee.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstEmployee.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstEmployee.BackColor = System.Drawing.Color.Transparent
        Me.lstEmployee.disableWhenChecked = False
        Me.lstEmployee.HeadingLabelName = "lstEmployee"
        Me.lstEmployee.HeadingText = "Employee"
        Me.lstEmployee.Location = New System.Drawing.Point(113, 194)
        Me.lstEmployee.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.lstEmployee.Name = "lstEmployee"
        Me.lstEmployee.ShowAddNewButton = False
        Me.lstEmployee.ShowInverse = True
        Me.lstEmployee.ShowMagnifierButton = False
        Me.lstEmployee.ShowNoCheck = False
        Me.lstEmployee.ShowResetAllButton = False
        Me.lstEmployee.ShowSelectall = True
        Me.lstEmployee.Size = New System.Drawing.Size(292, 162)
        Me.lstEmployee.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.lstEmployee, "Active Employee list")
        Me.lstEmployee.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.White
        Me.txtSearch.Location = New System.Drawing.Point(112, 362)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(261, 28)
        Me.txtSearch.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.txtSearch, "Search Employee by Name or Code")
        '
        'cmbCurrency
        '
        Me.cmbCurrency.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbCurrency.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCurrency.FormattingEnabled = True
        Me.cmbCurrency.Location = New System.Drawing.Point(113, 144)
        Me.cmbCurrency.Name = "cmbCurrency"
        Me.cmbCurrency.Size = New System.Drawing.Size(108, 28)
        Me.cmbCurrency.TabIndex = 10
        '
        'chkMultiCurrency
        '
        Me.chkMultiCurrency.AutoSize = True
        Me.chkMultiCurrency.Location = New System.Drawing.Point(112, 124)
        Me.chkMultiCurrency.Name = "chkMultiCurrency"
        Me.chkMultiCurrency.Size = New System.Drawing.Size(160, 24)
        Me.chkMultiCurrency.TabIndex = 8
        Me.chkMultiCurrency.Text = "Multi Currency"
        Me.chkMultiCurrency.UseVisualStyleBackColor = True
        '
        'chkUnPostedVouchers
        '
        Me.chkUnPostedVouchers.AutoSize = True
        Me.chkUnPostedVouchers.Checked = True
        Me.chkUnPostedVouchers.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkUnPostedVouchers.Location = New System.Drawing.Point(112, 101)
        Me.chkUnPostedVouchers.Name = "chkUnPostedVouchers"
        Me.chkUnPostedVouchers.Size = New System.Drawing.Size(270, 24)
        Me.chkUnPostedVouchers.TabIndex = 7
        Me.chkUnPostedVouchers.Text = "Include UnPosted Vouchers"
        Me.chkUnPostedVouchers.UseVisualStyleBackColor = True
        '
        'chkIncludeCostCenter
        '
        Me.chkIncludeCostCenter.AutoSize = True
        Me.chkIncludeCostCenter.Location = New System.Drawing.Point(112, 81)
        Me.chkIncludeCostCenter.Name = "chkIncludeCostCenter"
        Me.chkIncludeCostCenter.Size = New System.Drawing.Size(280, 24)
        Me.chkIncludeCostCenter.TabIndex = 6
        Me.chkIncludeCostCenter.Text = "Include inactive Cost Center"
        Me.ToolTip1.SetToolTip(Me.chkIncludeCostCenter, "Include inactive Cost Center")
        Me.chkIncludeCostCenter.UseVisualStyleBackColor = True
        '
        'lblAdvanceRequest
        '
        Me.lblAdvanceRequest.AutoSize = True
        Me.lblAdvanceRequest.Location = New System.Drawing.Point(3, 58)
        Me.lblAdvanceRequest.Name = "lblAdvanceRequest"
        Me.lblAdvanceRequest.Size = New System.Drawing.Size(158, 20)
        Me.lblAdvanceRequest.TabIndex = 4
        Me.lblAdvanceRequest.Text = "Advance Request"
        '
        'cmbAdvanceRequest
        '
        Me.cmbAdvanceRequest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAdvanceRequest.FormattingEnabled = True
        Me.cmbAdvanceRequest.Location = New System.Drawing.Point(113, 54)
        Me.cmbAdvanceRequest.Name = "cmbAdvanceRequest"
        Me.cmbAdvanceRequest.Size = New System.Drawing.Size(289, 28)
        Me.cmbAdvanceRequest.TabIndex = 5
        '
        'lblCostCenter
        '
        Me.lblCostCenter.AutoSize = True
        Me.lblCostCenter.Location = New System.Drawing.Point(3, 32)
        Me.lblCostCenter.Name = "lblCostCenter"
        Me.lblCostCenter.Size = New System.Drawing.Size(68, 20)
        Me.lblCostCenter.TabIndex = 2
        Me.lblCostCenter.Text = "Project"
        '
        'cmbCostCenter
        '
        Me.cmbCostCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCostCenter.FormattingEnabled = True
        Me.cmbCostCenter.Location = New System.Drawing.Point(113, 28)
        Me.cmbCostCenter.Name = "cmbCostCenter"
        Me.cmbCostCenter.Size = New System.Drawing.Size(289, 28)
        Me.cmbCostCenter.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.cmbCostCenter, "Select Any Project ")
        '
        'cmbCostCenterHead
        '
        Me.cmbCostCenterHead.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCostCenterHead.FormattingEnabled = True
        Me.cmbCostCenterHead.Location = New System.Drawing.Point(113, 3)
        Me.cmbCostCenterHead.Name = "cmbCostCenterHead"
        Me.cmbCostCenterHead.Size = New System.Drawing.Size(289, 28)
        Me.cmbCostCenterHead.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbCostCenterHead, "Select Any Project Head")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 7)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(118, 20)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Project Head"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.optSummary)
        Me.Panel1.Controls.Add(Me.optDetail)
        Me.Panel1.Location = New System.Drawing.Point(3, 400)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(158, 34)
        Me.Panel1.TabIndex = 1
        Me.Panel1.Visible = False
        '
        'optSummary
        '
        Me.optSummary.AutoSize = True
        Me.optSummary.Location = New System.Drawing.Point(74, 8)
        Me.optSummary.Name = "optSummary"
        Me.optSummary.Size = New System.Drawing.Size(118, 24)
        Me.optSummary.TabIndex = 1
        Me.optSummary.Text = "Summary"
        Me.ToolTip1.SetToolTip(Me.optSummary, "Show Summary Ledger Report")
        Me.optSummary.UseVisualStyleBackColor = True
        Me.optSummary.Visible = False
        '
        'optDetail
        '
        Me.optDetail.AutoSize = True
        Me.optDetail.Checked = True
        Me.optDetail.Location = New System.Drawing.Point(13, 8)
        Me.optDetail.Name = "optDetail"
        Me.optDetail.Size = New System.Drawing.Size(84, 24)
        Me.optDetail.TabIndex = 0
        Me.optDetail.TabStop = True
        Me.optDetail.Text = "Detail"
        Me.ToolTip1.SetToolTip(Me.optDetail, "Show Detail Ledger Report")
        Me.optDetail.UseVisualStyleBackColor = True
        Me.optDetail.Visible = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Button1, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(167, 400)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(118, 34)
        Me.TableLayoutPanel1.TabIndex = 2
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Button1.Location = New System.Drawing.Point(3, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(111, 28)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Generate Ledger"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'UltraTabPageControl3
        '
        Me.UltraTabPageControl3.Controls.Add(Me.Label16)
        Me.UltraTabPageControl3.Controls.Add(Me.GroupBox1)
        Me.UltraTabPageControl3.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl3.Name = "UltraTabPageControl3"
        Me.UltraTabPageControl3.Size = New System.Drawing.Size(1060, 635)
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.Black
        Me.Label16.Location = New System.Drawing.Point(9, 12)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(112, 36)
        Me.Label16.TabIndex = 0
        Me.Label16.Text = "Ledger"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.GroupBox5)
        Me.GroupBox1.Controls.Add(Me.GroupBox4)
        Me.GroupBox1.Controls.Add(Me.btnGo2)
        Me.GroupBox1.Controls.Add(Me.btnGo1)
        Me.GroupBox1.Controls.Add(Me.btnGenerate)
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.ComboBox1)
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Controls.Add(Me.TextBox2)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Controls.Add(Me.btnRemove)
        Me.GroupBox1.Controls.Add(Me.btnAdd)
        Me.GroupBox1.Controls.Add(Me.lstAddedAccounts)
        Me.GroupBox1.Controls.Add(Me.lstAccounts)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 45)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1045, 567)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'GroupBox5
        '
        Me.GroupBox5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox5.Controls.Add(Me.Label19)
        Me.GroupBox5.Controls.Add(Me.cmbCurrCostCentre)
        Me.GroupBox5.Controls.Add(Me.chkIncUnPosted)
        Me.GroupBox5.Controls.Add(Me.Label18)
        Me.GroupBox5.Controls.Add(Me.Label17)
        Me.GroupBox5.Controls.Add(Me.dtpFrom)
        Me.GroupBox5.Controls.Add(Me.dtpTo)
        Me.GroupBox5.Location = New System.Drawing.Point(810, 230)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(207, 124)
        Me.GroupBox5.TabIndex = 9
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Date Range"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(3, 76)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(82, 20)
        Me.Label19.TabIndex = 5
        Me.Label19.Text = "Project :"
        '
        'cmbCurrCostCentre
        '
        Me.cmbCurrCostCentre.FormattingEnabled = True
        Me.cmbCurrCostCentre.Location = New System.Drawing.Point(60, 73)
        Me.cmbCurrCostCentre.Name = "cmbCurrCostCentre"
        Me.cmbCurrCostCentre.Size = New System.Drawing.Size(138, 28)
        Me.cmbCurrCostCentre.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.cmbCurrCostCentre, "Select Any Project ")
        '
        'chkIncUnPosted
        '
        Me.chkIncUnPosted.AutoSize = True
        Me.chkIncUnPosted.Checked = True
        Me.chkIncUnPosted.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIncUnPosted.Location = New System.Drawing.Point(45, 101)
        Me.chkIncUnPosted.Name = "chkIncUnPosted"
        Me.chkIncUnPosted.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.chkIncUnPosted.Size = New System.Drawing.Size(224, 24)
        Me.chkIncUnPosted.TabIndex = 4
        Me.chkIncUnPosted.Text = "Inc UnPosted Voucher"
        Me.ToolTip1.SetToolTip(Me.chkIncUnPosted, "Include Unposted Voucher")
        Me.chkIncUnPosted.UseVisualStyleBackColor = True
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(4, 51)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(35, 20)
        Me.Label18.TabIndex = 2
        Me.Label18.Text = "To:"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(4, 24)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(61, 20)
        Me.Label17.TabIndex = 0
        Me.Label17.Text = "From:"
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(60, 20)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(138, 28)
        Me.dtpFrom.TabIndex = 1
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd/MMM/yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(60, 47)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(138, 28)
        Me.dtpTo.TabIndex = 3
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.rbtSummary)
        Me.GroupBox4.Controls.Add(Me.rbtDetail)
        Me.GroupBox4.Location = New System.Drawing.Point(810, 360)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(207, 44)
        Me.GroupBox4.TabIndex = 10
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Report Type"
        '
        'rbtSummary
        '
        Me.rbtSummary.AutoSize = True
        Me.rbtSummary.Location = New System.Drawing.Point(71, 21)
        Me.rbtSummary.Name = "rbtSummary"
        Me.rbtSummary.Size = New System.Drawing.Size(118, 24)
        Me.rbtSummary.TabIndex = 1
        Me.rbtSummary.Text = "Summary"
        Me.rbtSummary.UseVisualStyleBackColor = True
        '
        'rbtDetail
        '
        Me.rbtDetail.AutoSize = True
        Me.rbtDetail.Checked = True
        Me.rbtDetail.Location = New System.Drawing.Point(7, 21)
        Me.rbtDetail.Name = "rbtDetail"
        Me.rbtDetail.Size = New System.Drawing.Size(84, 24)
        Me.rbtDetail.TabIndex = 0
        Me.rbtDetail.TabStop = True
        Me.rbtDetail.Text = "Detail"
        Me.rbtDetail.UseVisualStyleBackColor = True
        '
        'btnGo2
        '
        Me.btnGo2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo2.Location = New System.Drawing.Point(726, 47)
        Me.btnGo2.Name = "btnGo2"
        Me.btnGo2.Size = New System.Drawing.Size(34, 23)
        Me.btnGo2.TabIndex = 7
        Me.btnGo2.Text = "Go"
        Me.btnGo2.UseVisualStyleBackColor = True
        '
        'btnGo1
        '
        Me.btnGo1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGo1.Location = New System.Drawing.Point(338, 46)
        Me.btnGo1.Name = "btnGo1"
        Me.btnGo1.Size = New System.Drawing.Size(32, 23)
        Me.btnGo1.TabIndex = 2
        Me.btnGo1.Text = "Go"
        Me.btnGo1.UseVisualStyleBackColor = True
        '
        'btnGenerate
        '
        Me.btnGenerate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnGenerate.Location = New System.Drawing.Point(810, 510)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(207, 23)
        Me.btnGenerate.TabIndex = 13
        Me.btnGenerate.Text = "Generate"
        Me.btnGenerate.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.rbtPrint)
        Me.GroupBox3.Controls.Add(Me.rbtView)
        Me.GroupBox3.Location = New System.Drawing.Point(810, 460)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(207, 44)
        Me.GroupBox3.TabIndex = 12
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Print Options"
        '
        'rbtPrint
        '
        Me.rbtPrint.AutoSize = True
        Me.rbtPrint.Location = New System.Drawing.Point(65, 20)
        Me.rbtPrint.Name = "rbtPrint"
        Me.rbtPrint.Size = New System.Drawing.Size(74, 24)
        Me.rbtPrint.TabIndex = 1
        Me.rbtPrint.Text = "Print"
        Me.rbtPrint.UseVisualStyleBackColor = True
        '
        'rbtView
        '
        Me.rbtView.AutoSize = True
        Me.rbtView.Checked = True
        Me.rbtView.Location = New System.Drawing.Point(7, 20)
        Me.rbtView.Name = "rbtView"
        Me.rbtView.Size = New System.Drawing.Size(75, 24)
        Me.rbtView.TabIndex = 0
        Me.rbtView.TabStop = True
        Me.rbtView.Text = "View"
        Me.rbtView.UseVisualStyleBackColor = True
        '
        'ComboBox1
        '
        Me.ComboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.ComboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(6, 20)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(326, 28)
        Me.ComboBox1.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.rbtIndividual)
        Me.GroupBox2.Controls.Add(Me.rbtContinues)
        Me.GroupBox2.Location = New System.Drawing.Point(810, 410)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(207, 44)
        Me.GroupBox2.TabIndex = 11
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Page Options"
        '
        'rbtIndividual
        '
        Me.rbtIndividual.AutoSize = True
        Me.rbtIndividual.Checked = True
        Me.rbtIndividual.Location = New System.Drawing.Point(94, 20)
        Me.rbtIndividual.Name = "rbtIndividual"
        Me.rbtIndividual.Size = New System.Drawing.Size(121, 24)
        Me.rbtIndividual.TabIndex = 1
        Me.rbtIndividual.TabStop = True
        Me.rbtIndividual.Text = "Individual"
        Me.rbtIndividual.UseVisualStyleBackColor = True
        '
        'rbtContinues
        '
        Me.rbtContinues.AutoSize = True
        Me.rbtContinues.Location = New System.Drawing.Point(7, 21)
        Me.rbtContinues.Name = "rbtContinues"
        Me.rbtContinues.Size = New System.Drawing.Size(120, 24)
        Me.rbtContinues.TabIndex = 0
        Me.rbtContinues.Text = "Continuse"
        Me.rbtContinues.UseVisualStyleBackColor = True
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(396, 47)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(324, 28)
        Me.TextBox2.TabIndex = 6
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(6, 47)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(326, 28)
        Me.TextBox1.TabIndex = 1
        '
        'btnRemove
        '
        Me.btnRemove.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnRemove.Location = New System.Drawing.Point(346, 271)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(44, 23)
        Me.btnRemove.TabIndex = 5
        Me.btnRemove.Text = "<<"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnAdd.Location = New System.Drawing.Point(346, 242)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(44, 23)
        Me.btnAdd.TabIndex = 4
        Me.btnAdd.Text = ">>"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'lstAddedAccounts
        '
        Me.lstAddedAccounts.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstAddedAccounts.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lstAddedAccounts.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstAddedAccounts.BackColor = System.Drawing.Color.Transparent
        Me.lstAddedAccounts.disableWhenChecked = False
        Me.lstAddedAccounts.HeadingLabelName = "lblAddedAccountsList"
        Me.lstAddedAccounts.HeadingText = "Added Accounts List"
        Me.lstAddedAccounts.Location = New System.Drawing.Point(396, 75)
        Me.lstAddedAccounts.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.lstAddedAccounts.Name = "lstAddedAccounts"
        Me.lstAddedAccounts.ShowAddNewButton = False
        Me.lstAddedAccounts.ShowInverse = True
        Me.lstAddedAccounts.ShowMagnifierButton = False
        Me.lstAddedAccounts.ShowNoCheck = False
        Me.lstAddedAccounts.ShowResetAllButton = False
        Me.lstAddedAccounts.ShowSelectall = True
        Me.lstAddedAccounts.Size = New System.Drawing.Size(364, 486)
        Me.lstAddedAccounts.TabIndex = 8
        Me.lstAddedAccounts.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstAccounts
        '
        Me.lstAccounts.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstAccounts.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lstAccounts.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstAccounts.BackColor = System.Drawing.Color.Transparent
        Me.lstAccounts.disableWhenChecked = False
        Me.lstAccounts.HeadingLabelName = "lblAccountsList"
        Me.lstAccounts.HeadingText = "Accounts List"
        Me.lstAccounts.Location = New System.Drawing.Point(6, 75)
        Me.lstAccounts.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.lstAccounts.Name = "lstAccounts"
        Me.lstAccounts.ShowAddNewButton = False
        Me.lstAccounts.ShowInverse = True
        Me.lstAccounts.ShowMagnifierButton = False
        Me.lstAccounts.ShowNoCheck = False
        Me.lstAccounts.ShowResetAllButton = False
        Me.lstAccounts.ShowSelectall = True
        Me.lstAccounts.Size = New System.Drawing.Size(364, 486)
        Me.lstAccounts.TabIndex = 3
        Me.lstAccounts.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.lblCurrency)
        Me.UltraTabPageControl2.Controls.Add(Me.lblBalance)
        Me.UltraTabPageControl2.Controls.Add(Me.lblTotalCredit)
        Me.UltraTabPageControl2.Controls.Add(Me.lblTotalDebit)
        Me.UltraTabPageControl2.Controls.Add(Me.Label15)
        Me.UltraTabPageControl2.Controls.Add(Me.Label14)
        Me.UltraTabPageControl2.Controls.Add(Me.Label13)
        Me.UltraTabPageControl2.Controls.Add(Me.lblToDate)
        Me.UltraTabPageControl2.Controls.Add(Me.lblFromDate)
        Me.UltraTabPageControl2.Controls.Add(Me.Label10)
        Me.UltraTabPageControl2.Controls.Add(Me.Label11)
        Me.UltraTabPageControl2.Controls.Add(Me.lblAccount_Code)
        Me.UltraTabPageControl2.Controls.Add(Me.lblAccount_Title)
        Me.UltraTabPageControl2.Controls.Add(Me.Label9)
        Me.UltraTabPageControl2.Controls.Add(Me.Label8)
        Me.UltraTabPageControl2.Controls.Add(Me.Label7)
        Me.UltraTabPageControl2.Controls.Add(Me.lblOpeningBalance)
        Me.UltraTabPageControl2.Controls.Add(Me.Label5)
        Me.UltraTabPageControl2.Controls.Add(Me.Panel2)
        Me.UltraTabPageControl2.Controls.Add(Me.Label12)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1060, 635)
        '
        'lblCurrency
        '
        Me.lblCurrency.AutoSize = True
        Me.lblCurrency.BackColor = System.Drawing.Color.Transparent
        Me.lblCurrency.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrency.ForeColor = System.Drawing.Color.Black
        Me.lblCurrency.Location = New System.Drawing.Point(11, 105)
        Me.lblCurrency.Name = "lblCurrency"
        Me.lblCurrency.Size = New System.Drawing.Size(109, 22)
        Me.lblCurrency.TabIndex = 21
        Me.lblCurrency.Text = "Currency:"
        '
        'lblBalance
        '
        Me.lblBalance.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblBalance.BackColor = System.Drawing.Color.Transparent
        Me.lblBalance.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBalance.ForeColor = System.Drawing.Color.Black
        Me.lblBalance.Location = New System.Drawing.Point(658, 499)
        Me.lblBalance.Name = "lblBalance"
        Me.lblBalance.Size = New System.Drawing.Size(152, 13)
        Me.lblBalance.TabIndex = 19
        Me.lblBalance.Text = "0"
        Me.lblBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalCredit
        '
        Me.lblTotalCredit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotalCredit.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalCredit.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalCredit.ForeColor = System.Drawing.Color.Black
        Me.lblTotalCredit.Location = New System.Drawing.Point(658, 480)
        Me.lblTotalCredit.Name = "lblTotalCredit"
        Me.lblTotalCredit.Size = New System.Drawing.Size(152, 13)
        Me.lblTotalCredit.TabIndex = 17
        Me.lblTotalCredit.Text = "0"
        Me.lblTotalCredit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTotalDebit
        '
        Me.lblTotalDebit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotalDebit.BackColor = System.Drawing.Color.Transparent
        Me.lblTotalDebit.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalDebit.ForeColor = System.Drawing.Color.Black
        Me.lblTotalDebit.Location = New System.Drawing.Point(658, 461)
        Me.lblTotalDebit.Name = "lblTotalDebit"
        Me.lblTotalDebit.Size = New System.Drawing.Size(152, 13)
        Me.lblTotalDebit.TabIndex = 15
        Me.lblTotalDebit.Text = "0"
        Me.lblTotalDebit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label15
        '
        Me.Label15.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label15.AutoSize = True
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(569, 499)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(83, 20)
        Me.Label15.TabIndex = 18
        Me.Label15.Text = "Balance"
        '
        'Label14
        '
        Me.Label14.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Black
        Me.Label14.Location = New System.Drawing.Point(569, 480)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(121, 20)
        Me.Label14.TabIndex = 16
        Me.Label14.Text = "Total Credit"
        '
        'Label13
        '
        Me.Label13.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.AutoSize = True
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(568, 461)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(115, 20)
        Me.Label13.TabIndex = 14
        Me.Label13.Text = "Total Debit"
        '
        'lblToDate
        '
        Me.lblToDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblToDate.AutoSize = True
        Me.lblToDate.BackColor = System.Drawing.Color.Transparent
        Me.lblToDate.Location = New System.Drawing.Point(732, 80)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(120, 20)
        Me.lblToDate.TabIndex = 8
        Me.lblToDate.Text = "01/Jan/9998"
        Me.lblToDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblFromDate
        '
        Me.lblFromDate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFromDate.AutoSize = True
        Me.lblFromDate.BackColor = System.Drawing.Color.Transparent
        Me.lblFromDate.Location = New System.Drawing.Point(732, 56)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(120, 20)
        Me.lblFromDate.TabIndex = 6
        Me.lblFromDate.Text = "01/Jan/1999"
        Me.lblFromDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(568, 79)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(90, 20)
        Me.Label10.TabIndex = 7
        Me.Label10.Text = "To Date:"
        '
        'Label11
        '
        Me.Label11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.Black
        Me.Label11.Location = New System.Drawing.Point(568, 55)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(115, 20)
        Me.Label11.TabIndex = 5
        Me.Label11.Text = "From Date:"
        '
        'lblAccount_Code
        '
        Me.lblAccount_Code.AutoSize = True
        Me.lblAccount_Code.Location = New System.Drawing.Point(118, 80)
        Me.lblAccount_Code.Name = "lblAccount_Code"
        Me.lblAccount_Code.Size = New System.Drawing.Size(0, 20)
        Me.lblAccount_Code.TabIndex = 4
        '
        'lblAccount_Title
        '
        Me.lblAccount_Title.AutoSize = True
        Me.lblAccount_Title.Location = New System.Drawing.Point(118, 56)
        Me.lblAccount_Title.Name = "lblAccount_Title"
        Me.lblAccount_Title.Size = New System.Drawing.Size(0, 20)
        Me.lblAccount_Title.TabIndex = 2
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Black
        Me.Label9.Location = New System.Drawing.Point(11, 79)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(146, 20)
        Me.Label9.TabIndex = 3
        Me.Label9.Text = "Account Code:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.Black
        Me.Label8.Location = New System.Drawing.Point(11, 55)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(142, 20)
        Me.Label8.TabIndex = 1
        Me.Label8.Text = "Account Title:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.Black
        Me.Label7.Location = New System.Drawing.Point(7, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(244, 35)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Ledger Report"
        '
        'lblOpeningBalance
        '
        Me.lblOpeningBalance.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblOpeningBalance.BackColor = System.Drawing.Color.Transparent
        Me.lblOpeningBalance.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOpeningBalance.ForeColor = System.Drawing.Color.Black
        Me.lblOpeningBalance.Location = New System.Drawing.Point(692, 105)
        Me.lblOpeningBalance.Name = "lblOpeningBalance"
        Me.lblOpeningBalance.Size = New System.Drawing.Size(118, 14)
        Me.lblOpeningBalance.TabIndex = 13
        Me.lblOpeningBalance.Text = "0"
        Me.lblOpeningBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(568, 105)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(181, 22)
        Me.Label5.TabIndex = 12
        Me.Label5.Text = "Opening Balance"
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.Controls.Add(Me.grdLedger)
        Me.Panel2.Location = New System.Drawing.Point(0, 130)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(840, 321)
        Me.Panel2.TabIndex = 20
        '
        'grdLedger
        '
        Me.grdLedger.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdLedger.AutomaticSort = False
        grdLedger_DesignTimeLayout.LayoutString = resources.GetString("grdLedger_DesignTimeLayout.LayoutString")
        Me.grdLedger.DesignTimeLayout = grdLedger_DesignTimeLayout
        Me.grdLedger.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdLedger.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdLedger.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdLedger.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdLedger.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.grdLedger.GroupByBoxVisible = False
        Me.grdLedger.Location = New System.Drawing.Point(0, 0)
        Me.grdLedger.Name = "grdLedger"
        Me.grdLedger.Size = New System.Drawing.Size(840, 321)
        Me.grdLedger.TabIndex = 0
        Me.grdLedger.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdLedger.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdLedger.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'Label12
        '
        Me.Label12.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label12.Location = New System.Drawing.Point(11, 86)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(825, 19)
        Me.Label12.TabIndex = 9
        Me.Label12.Text = "_________________________________________________________________________________" & _
    "_____________________________"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.toolStripSeparator, Me.btnLedgerPrint, Me.btnOpenVoucher})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1062, 32)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(79, 29)
        Me.btnNew.Text = "&Clear"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 32)
        '
        'btnLedgerPrint
        '
        Me.btnLedgerPrint.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DetailLederToolStripMenuItem, Me.SummaryLedgerToolStripMenuItem})
        Me.btnLedgerPrint.Image = Global.SimpleAccounts.My.Resources.Resources.print
        Me.btnLedgerPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnLedgerPrint.Name = "btnLedgerPrint"
        Me.btnLedgerPrint.Size = New System.Drawing.Size(152, 29)
        Me.btnLedgerPrint.Text = "Print Ledger"
        Me.btnLedgerPrint.Visible = False
        '
        'DetailLederToolStripMenuItem
        '
        Me.DetailLederToolStripMenuItem.Name = "DetailLederToolStripMenuItem"
        Me.DetailLederToolStripMenuItem.Size = New System.Drawing.Size(231, 30)
        Me.DetailLederToolStripMenuItem.Text = "Detail Ledger"
        '
        'SummaryLedgerToolStripMenuItem
        '
        Me.SummaryLedgerToolStripMenuItem.Name = "SummaryLedgerToolStripMenuItem"
        Me.SummaryLedgerToolStripMenuItem.Size = New System.Drawing.Size(231, 30)
        Me.SummaryLedgerToolStripMenuItem.Text = "Summary Ledger"
        '
        'btnOpenVoucher
        '
        Me.btnOpenVoucher.Image = Global.SimpleAccounts.My.Resources.Resources.BtnEdit_Image
        Me.btnOpenVoucher.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOpenVoucher.Name = "btnOpenVoucher"
        Me.btnOpenVoucher.Size = New System.Drawing.Size(198, 29)
        Me.btnOpenVoucher.Text = "Open Voucher Entry"
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        Me.ToolStripProgressBar1.Size = New System.Drawing.Size(200, 22)
        Me.ToolStripProgressBar1.Visible = False
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl3)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 32)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(1062, 663)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 1
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Single Criteria"
        UltraTab3.TabPage = Me.UltraTabPageControl3
        UltraTab3.Text = "Multi Criteria"
        UltraTab3.Visible = False
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "Ledger Report"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab3, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.VisualStudio2005
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1060, 635)
        '
        'BackgroundWorker1
        '
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
        'BackgroundWorker2
        '
        '
        'CtrlGrdBar3
        '
        Me.CtrlGrdBar3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar3.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar3.Email = Nothing
        Me.CtrlGrdBar3.FormName = Me
        Me.CtrlGrdBar3.Location = New System.Drawing.Point(1024, 0)
        Me.CtrlGrdBar3.MyGrid = Me.grdLedger
        Me.CtrlGrdBar3.Name = "CtrlGrdBar3"
        Me.CtrlGrdBar3.Size = New System.Drawing.Size(38, 25)
        Me.CtrlGrdBar3.TabIndex = 1
        Me.CtrlGrdBar3.TabStop = False
        '
        'frmEmployeeWiseLedger
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1062, 695)
        Me.Controls.Add(Me.CtrlGrdBar3)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEmployeeWiseLedger"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Ledger Report"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.cmbAccount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.pnlCost.ResumeLayout(False)
        Me.pnlCost.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.UltraTabPageControl3.ResumeLayout(False)
        Me.UltraTabPageControl3.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        Me.UltraTabPageControl2.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        CType(Me.grdLedger, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbAccount As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents optByCode As System.Windows.Forms.RadioButton
    Friend WithEvents optByName As System.Windows.Forms.RadioButton
    Friend WithEvents optSummary As System.Windows.Forms.RadioButton
    Friend WithEvents optDetail As System.Windows.Forms.RadioButton
    Friend WithEvents lblCostCenter As System.Windows.Forms.Label
    Friend WithEvents cmbCostCenter As System.Windows.Forms.ComboBox
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents pnlCost As System.Windows.Forms.Panel
    Friend WithEvents cmbCostCenterHead As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkUnPostedVouchers As System.Windows.Forms.CheckBox
    Friend WithEvents lnkRefresh As System.Windows.Forms.LinkLabel
    Friend WithEvents chkIncludeCostCenter As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents grdLedger As Janus.Windows.GridEX.GridEX
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents lblOpeningBalance As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblAccount_Code As System.Windows.Forms.Label
    Friend WithEvents lblAccount_Title As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents lblFromDate As System.Windows.Forms.Label
    Friend WithEvents lblToDate As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblTotalCredit As System.Windows.Forms.Label
    Friend WithEvents lblTotalDebit As System.Windows.Forms.Label
    Friend WithEvents lblBalance As System.Windows.Forms.Label
    Friend WithEvents btnLedgerPrint As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents DetailLederToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SummaryLedgerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents ToolStripProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents UltraTabPageControl3 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents lstAddedAccounts As SimpleAccounts.uiListControl
    Friend WithEvents lstAccounts As SimpleAccounts.uiListControl
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents rbtContinues As System.Windows.Forms.RadioButton
    Friend WithEvents btnGenerate As System.Windows.Forms.Button
    Friend WithEvents rbtPrint As System.Windows.Forms.RadioButton
    Friend WithEvents rbtView As System.Windows.Forms.RadioButton
    Friend WithEvents rbtIndividual As System.Windows.Forms.RadioButton
    Friend WithEvents btnGo2 As System.Windows.Forms.Button
    Friend WithEvents btnGo1 As System.Windows.Forms.Button
    Friend WithEvents BackgroundWorker2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtSummary As System.Windows.Forms.RadioButton
    Friend WithEvents rbtDetail As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents chkIncUnPosted As System.Windows.Forms.CheckBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkMultiCurrency As System.Windows.Forms.CheckBox
    Friend WithEvents lblCurrency As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents cmbCurrency As System.Windows.Forms.ComboBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents cmbCurrCostCentre As System.Windows.Forms.ComboBox
    Friend WithEvents btnOpenVoucher As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents lstEmployee As SimpleAccounts.uiListControl
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents CtrlGrdBar3 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblAdvanceRequest As System.Windows.Forms.Label
    Friend WithEvents cmbAdvanceRequest As System.Windows.Forms.ComboBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents rbActive As System.Windows.Forms.RadioButton
    Friend WithEvents rbInactive As System.Windows.Forms.RadioButton

End Class

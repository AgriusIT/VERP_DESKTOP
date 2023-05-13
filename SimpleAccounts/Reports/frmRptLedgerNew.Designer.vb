<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptLedgerNew
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
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn1 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ID")
        Dim UltraGridColumn2 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Account")
        Dim UltraGridColumn3 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Code")
        Dim UltraGridColumn4 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Type")
        Dim UltraGridColumn5 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("City", 0)
        Dim RowLayout1 As Infragistics.Win.UltraWinGrid.RowLayout = New Infragistics.Win.UltraWinGrid.RowLayout("cmbAccountLegendLedger")
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRptLedgerNew))
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.lblAcName = New System.Windows.Forms.Label()
        Me.grpDateRange = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        Me.lblToDate = New System.Windows.Forms.Label()
        Me.lblFromDate = New System.Windows.Forms.Label()
        Me.dtpTo = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.dtpFrom = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.cmbAccountLevel = New System.Windows.Forms.ComboBox()
        Me.cmbAccountType = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CmbCostHead = New System.Windows.Forms.ComboBox()
        Me.CmbCostCenter = New System.Windows.Forms.ComboBox()
        Me.lblCostHead = New System.Windows.Forms.Label()
        Me.lblCostCenter = New System.Windows.Forms.Label()
        Me.chkPostDatedCheque = New System.Windows.Forms.CheckBox()
        Me.BtnGenerate = New System.Windows.Forms.Button()
        Me.RbtDetailLgr = New System.Windows.Forms.RadioButton()
        Me.RbtSummaryLgr = New System.Windows.Forms.RadioButton()
        Me.chkUnPostedVoucher = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmbAccounts = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.rbtByCode = New System.Windows.Forms.RadioButton()
        Me.rbtByName = New System.Windows.Forms.RadioButton()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.chkProjectWiseGroup = New System.Windows.Forms.CheckBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.chkIncludeActiveAccount = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmbCompany = New System.Windows.Forms.ComboBox()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblCompany = New System.Windows.Forms.Label()
        Me.grpDateRange.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.cmbAccounts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(22, 15)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(221, 36)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Legend Ledger"
        '
        'lblAcName
        '
        Me.lblAcName.AutoSize = True
        Me.lblAcName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAcName.Location = New System.Drawing.Point(18, 391)
        Me.lblAcName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAcName.Name = "lblAcName"
        Me.lblAcName.Size = New System.Drawing.Size(134, 20)
        Me.lblAcName.TabIndex = 8
        Me.lblAcName.Text = "Account Name"
        '
        'grpDateRange
        '
        Me.grpDateRange.Controls.Add(Me.Label4)
        Me.grpDateRange.Controls.Add(Me.cmbPeriod)
        Me.grpDateRange.Controls.Add(Me.lblToDate)
        Me.grpDateRange.Controls.Add(Me.lblFromDate)
        Me.grpDateRange.Controls.Add(Me.dtpTo)
        Me.grpDateRange.Controls.Add(Me.dtpFrom)
        Me.grpDateRange.Location = New System.Drawing.Point(18, 94)
        Me.grpDateRange.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grpDateRange.Name = "grpDateRange"
        Me.grpDateRange.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grpDateRange.Size = New System.Drawing.Size(568, 143)
        Me.grpDateRange.TabIndex = 2
        Me.grpDateRange.TabStop = False
        Me.grpDateRange.Text = "Date Range"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(18, 28)
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
        Me.cmbPeriod.Location = New System.Drawing.Point(158, 23)
        Me.cmbPeriod.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(216, 28)
        Me.cmbPeriod.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbPeriod, "Select Period And Gets Date Range")
        '
        'lblToDate
        '
        Me.lblToDate.AutoSize = True
        Me.lblToDate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblToDate.Location = New System.Drawing.Point(18, 109)
        Me.lblToDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(27, 20)
        Me.lblToDate.TabIndex = 4
        Me.lblToDate.Text = "To"
        '
        'lblFromDate
        '
        Me.lblFromDate.AutoSize = True
        Me.lblFromDate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromDate.Location = New System.Drawing.Point(18, 71)
        Me.lblFromDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(53, 20)
        Me.lblFromDate.TabIndex = 2
        Me.lblFromDate.Text = "From"
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd/MMM/yyyy"
        Me.dtpTo.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtpTo.DropDownCalendar.Name = ""
        Me.dtpTo.Location = New System.Drawing.Point(158, 103)
        Me.dtpTo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(218, 26)
        Me.dtpTo.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.dtpTo, "To Date")
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFrom.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtpFrom.DropDownCalendar.Name = ""
        Me.dtpFrom.Location = New System.Drawing.Point(158, 65)
        Me.dtpFrom.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(218, 26)
        Me.dtpFrom.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.dtpFrom, "From Date")
        '
        'cmbAccountLevel
        '
        Me.cmbAccountLevel.FormattingEnabled = True
        Me.cmbAccountLevel.Items.AddRange(New Object() {"...Select Any Value...", "Main", "Sub", "SubSub", "Detail"})
        Me.cmbAccountLevel.Location = New System.Drawing.Point(176, 283)
        Me.cmbAccountLevel.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbAccountLevel.Name = "cmbAccountLevel"
        Me.cmbAccountLevel.Size = New System.Drawing.Size(408, 28)
        Me.cmbAccountLevel.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.cmbAccountLevel, "Select Any Account Level")
        '
        'cmbAccountType
        '
        Me.cmbAccountType.FormattingEnabled = True
        Me.cmbAccountType.Items.AddRange(New Object() {"...Select Any Value...", "General", "Cash", "Bank", "Customer", "Vendor", "Expense", "Inventory"})
        Me.cmbAccountType.Location = New System.Drawing.Point(170, 9)
        Me.cmbAccountType.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbAccountType.Name = "cmbAccountType"
        Me.cmbAccountType.Size = New System.Drawing.Size(408, 28)
        Me.cmbAccountType.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbAccountType, "Select Any Account Type")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 288)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 20)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Account Level"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 14)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(106, 20)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Account Type"
        '
        'CmbCostHead
        '
        Me.CmbCostHead.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.CmbCostHead.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbCostHead.FormattingEnabled = True
        Me.CmbCostHead.Location = New System.Drawing.Point(176, 471)
        Me.CmbCostHead.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CmbCostHead.Name = "CmbCostHead"
        Me.CmbCostHead.Size = New System.Drawing.Size(408, 28)
        Me.CmbCostHead.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.CmbCostHead, "Select Any Project Head")
        '
        'CmbCostCenter
        '
        Me.CmbCostCenter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.CmbCostCenter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CmbCostCenter.FormattingEnabled = True
        Me.CmbCostCenter.Location = New System.Drawing.Point(176, 508)
        Me.CmbCostCenter.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CmbCostCenter.Name = "CmbCostCenter"
        Me.CmbCostCenter.Size = New System.Drawing.Size(408, 28)
        Me.CmbCostCenter.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.CmbCostCenter, "Select Any Project Name")
        '
        'lblCostHead
        '
        Me.lblCostHead.AutoSize = True
        Me.lblCostHead.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCostHead.Location = New System.Drawing.Point(18, 477)
        Me.lblCostHead.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCostHead.Name = "lblCostHead"
        Me.lblCostHead.Size = New System.Drawing.Size(118, 20)
        Me.lblCostHead.TabIndex = 11
        Me.lblCostHead.Text = "Project Head"
        '
        'lblCostCenter
        '
        Me.lblCostCenter.AutoSize = True
        Me.lblCostCenter.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCostCenter.Location = New System.Drawing.Point(18, 514)
        Me.lblCostCenter.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCostCenter.Name = "lblCostCenter"
        Me.lblCostCenter.Size = New System.Drawing.Size(124, 20)
        Me.lblCostCenter.TabIndex = 14
        Me.lblCostCenter.Text = "Project Name"
        '
        'chkPostDatedCheque
        '
        Me.chkPostDatedCheque.AutoSize = True
        Me.chkPostDatedCheque.Checked = True
        Me.chkPostDatedCheque.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPostDatedCheque.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkPostDatedCheque.Location = New System.Drawing.Point(174, 585)
        Me.chkPostDatedCheque.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkPostDatedCheque.Name = "chkPostDatedCheque"
        Me.chkPostDatedCheque.Size = New System.Drawing.Size(279, 24)
        Me.chkPostDatedCheque.TabIndex = 17
        Me.chkPostDatedCheque.Text = "Include Post Dated Cheques"
        Me.ToolTip1.SetToolTip(Me.chkPostDatedCheque, "Include Post Dated Cheques")
        Me.chkPostDatedCheque.UseVisualStyleBackColor = True
        '
        'BtnGenerate
        '
        Me.BtnGenerate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnGenerate.Location = New System.Drawing.Point(418, 748)
        Me.BtnGenerate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.BtnGenerate.Name = "BtnGenerate"
        Me.BtnGenerate.Size = New System.Drawing.Size(166, 35)
        Me.BtnGenerate.TabIndex = 23
        Me.BtnGenerate.Text = "Generate"
        Me.ToolTip1.SetToolTip(Me.BtnGenerate, "Generate Report")
        Me.BtnGenerate.UseVisualStyleBackColor = True
        '
        'RbtDetailLgr
        '
        Me.RbtDetailLgr.AutoSize = True
        Me.RbtDetailLgr.Checked = True
        Me.RbtDetailLgr.Location = New System.Drawing.Point(174, 714)
        Me.RbtDetailLgr.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RbtDetailLgr.Name = "RbtDetailLgr"
        Me.RbtDetailLgr.Size = New System.Drawing.Size(75, 24)
        Me.RbtDetailLgr.TabIndex = 21
        Me.RbtDetailLgr.TabStop = True
        Me.RbtDetailLgr.Text = "Detail"
        Me.ToolTip1.SetToolTip(Me.RbtDetailLgr, "Show Detail Ledger Report")
        Me.RbtDetailLgr.UseVisualStyleBackColor = True
        '
        'RbtSummaryLgr
        '
        Me.RbtSummaryLgr.AutoSize = True
        Me.RbtSummaryLgr.Location = New System.Drawing.Point(258, 714)
        Me.RbtSummaryLgr.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RbtSummaryLgr.Name = "RbtSummaryLgr"
        Me.RbtSummaryLgr.Size = New System.Drawing.Size(101, 24)
        Me.RbtSummaryLgr.TabIndex = 22
        Me.RbtSummaryLgr.Text = "Summary"
        Me.ToolTip1.SetToolTip(Me.RbtSummaryLgr, "Show Summary Ledger Report")
        Me.RbtSummaryLgr.UseVisualStyleBackColor = True
        '
        'chkUnPostedVoucher
        '
        Me.chkUnPostedVoucher.AutoSize = True
        Me.chkUnPostedVoucher.Checked = True
        Me.chkUnPostedVoucher.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkUnPostedVoucher.Location = New System.Drawing.Point(174, 617)
        Me.chkUnPostedVoucher.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkUnPostedVoucher.Name = "chkUnPostedVoucher"
        Me.chkUnPostedVoucher.Size = New System.Drawing.Size(238, 24)
        Me.chkUnPostedVoucher.TabIndex = 18
        Me.chkUnPostedVoucher.Text = "Include Un Posted Vouchers"
        Me.ToolTip1.SetToolTip(Me.chkUnPostedVoucher, "Include Inactive Accounts")
        Me.chkUnPostedVoucher.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.cmbAccountType)
        Me.Panel1.Location = New System.Drawing.Point(6, 325)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(602, 49)
        Me.Panel1.TabIndex = 7
        '
        'cmbAccounts
        '
        Appearance1.BackColor = System.Drawing.Color.White
        Me.cmbAccounts.Appearance = Appearance1
        Me.cmbAccounts.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append
        Me.cmbAccounts.CheckedListSettings.CheckStateMember = ""
        Appearance2.BackColor = System.Drawing.Color.White
        Me.cmbAccounts.DisplayLayout.Appearance = Appearance2
        UltraGridColumn1.Header.VisiblePosition = 0
        UltraGridColumn1.Hidden = True
        UltraGridColumn1.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(8, 0)
        UltraGridColumn2.AutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.VisibleRows
        UltraGridColumn2.Header.Caption = "Account Description"
        UltraGridColumn2.Header.VisiblePosition = 1
        UltraGridColumn2.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(128, 0)
        UltraGridColumn3.Header.Caption = "Account Code"
        UltraGridColumn3.Header.VisiblePosition = 2
        UltraGridColumn4.Header.VisiblePosition = 3
        UltraGridColumn4.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(96, 0)
        UltraGridColumn5.Header.VisiblePosition = 4
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn1, UltraGridColumn2, UltraGridColumn3, UltraGridColumn4, UltraGridColumn5})
        UltraGridBand1.RowLayouts.AddRange(New Infragistics.Win.UltraWinGrid.RowLayout() {RowLayout1})
        Me.cmbAccounts.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbAccounts.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbAccounts.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbAccounts.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbAccounts.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Appearance3.BackColor = System.Drawing.Color.Transparent
        Me.cmbAccounts.DisplayLayout.Override.CardAreaAppearance = Appearance3
        Me.cmbAccounts.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbAccounts.DisplayLayout.Override.CellPadding = 3
        Me.cmbAccounts.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance4.TextHAlignAsString = "Left"
        Me.cmbAccounts.DisplayLayout.Override.HeaderAppearance = Appearance4
        Me.cmbAccounts.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance5.BorderColor = System.Drawing.Color.LightGray
        Appearance5.TextVAlignAsString = "Middle"
        Me.cmbAccounts.DisplayLayout.Override.RowAppearance = Appearance5
        Appearance6.BackColor = System.Drawing.Color.LightSteelBlue
        Appearance6.BorderColor = System.Drawing.Color.Black
        Appearance6.ForeColor = System.Drawing.Color.Black
        Me.cmbAccounts.DisplayLayout.Override.SelectedRowAppearance = Appearance6
        Me.cmbAccounts.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbAccounts.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbAccounts.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbAccounts.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.cmbAccounts.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbAccounts.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbAccounts.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbAccounts.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbAccounts.LimitToList = True
        Me.cmbAccounts.Location = New System.Drawing.Point(176, 383)
        Me.cmbAccounts.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbAccounts.MaxDropDownItems = 16
        Me.cmbAccounts.Name = "cmbAccounts"
        Me.cmbAccounts.Size = New System.Drawing.Size(411, 29)
        Me.cmbAccounts.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.cmbAccounts, "Select Any Account")
        '
        'rbtByCode
        '
        Me.rbtByCode.AutoSize = True
        Me.rbtByCode.Location = New System.Drawing.Point(4, 5)
        Me.rbtByCode.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtByCode.Name = "rbtByCode"
        Me.rbtByCode.Size = New System.Drawing.Size(94, 24)
        Me.rbtByCode.TabIndex = 0
        Me.rbtByCode.Text = "By Code"
        Me.ToolTip1.SetToolTip(Me.rbtByCode, "Search Account By Code")
        Me.rbtByCode.UseVisualStyleBackColor = True
        '
        'rbtByName
        '
        Me.rbtByName.AutoSize = True
        Me.rbtByName.Checked = True
        Me.rbtByName.Location = New System.Drawing.Point(141, 5)
        Me.rbtByName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtByName.Name = "rbtByName"
        Me.rbtByName.Size = New System.Drawing.Size(98, 24)
        Me.rbtByName.TabIndex = 1
        Me.rbtByName.TabStop = True
        Me.rbtByName.Text = "By Name"
        Me.ToolTip1.SetToolTip(Me.rbtByName, "Search Account By Name")
        Me.rbtByName.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.rbtByCode)
        Me.Panel2.Controls.Add(Me.rbtByName)
        Me.Panel2.Location = New System.Drawing.Point(176, 426)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(248, 35)
        Me.Panel2.TabIndex = 10
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(626, 32)
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
        'chkProjectWiseGroup
        '
        Me.chkProjectWiseGroup.AutoSize = True
        Me.chkProjectWiseGroup.Location = New System.Drawing.Point(174, 685)
        Me.chkProjectWiseGroup.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkProjectWiseGroup.Name = "chkProjectWiseGroup"
        Me.chkProjectWiseGroup.Size = New System.Drawing.Size(176, 24)
        Me.chkProjectWiseGroup.TabIndex = 20
        Me.chkProjectWiseGroup.Text = "Project Wise Group "
        Me.ToolTip1.SetToolTip(Me.chkProjectWiseGroup, "Option Project Wise Group")
        Me.chkProjectWiseGroup.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(174, 549)
        Me.CheckBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(235, 24)
        Me.CheckBox1.TabIndex = 16
        Me.CheckBox1.Text = "Include Inactive Cost Center"
        Me.ToolTip1.SetToolTip(Me.CheckBox1, "Include Cost Center If(Cost Center Inactive)")
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'chkIncludeActiveAccount
        '
        Me.chkIncludeActiveAccount.AutoSize = True
        Me.chkIncludeActiveAccount.Location = New System.Drawing.Point(174, 651)
        Me.chkIncludeActiveAccount.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkIncludeActiveAccount.Name = "chkIncludeActiveAccount"
        Me.chkIncludeActiveAccount.Size = New System.Drawing.Size(200, 24)
        Me.chkIncludeActiveAccount.TabIndex = 19
        Me.chkIncludeActiveAccount.Text = "Include Inctive Account"
        Me.chkIncludeActiveAccount.UseVisualStyleBackColor = True
        '
        'cmbCompany
        '
        Me.cmbCompany.FormattingEnabled = True
        Me.cmbCompany.Items.AddRange(New Object() {"...Select Any Value...", "Main", "Sub", "SubSub", "Detail"})
        Me.cmbCompany.Location = New System.Drawing.Point(176, 242)
        Me.cmbCompany.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbCompany.Name = "cmbCompany"
        Me.cmbCompany.Size = New System.Drawing.Size(408, 28)
        Me.cmbCompany.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.cmbCompany, "Select Any Account Level")
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(176, 477)
        Me.lblProgress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(394, 69)
        Me.lblProgress.TabIndex = 13
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 32)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(626, 52)
        Me.pnlHeader.TabIndex = 1
        '
        'lblCompany
        '
        Me.lblCompany.AutoSize = True
        Me.lblCompany.Location = New System.Drawing.Point(18, 246)
        Me.lblCompany.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCompany.Name = "lblCompany"
        Me.lblCompany.Size = New System.Drawing.Size(76, 20)
        Me.lblCompany.TabIndex = 3
        Me.lblCompany.Text = "Company"
        '
        'frmRptLedgerNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(626, 802)
        Me.Controls.Add(Me.lblCompany)
        Me.Controls.Add(Me.cmbCompany)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.chkIncludeActiveAccount)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.chkProjectWiseGroup)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.cmbAccounts)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbAccountLevel)
        Me.Controls.Add(Me.chkUnPostedVoucher)
        Me.Controls.Add(Me.RbtSummaryLgr)
        Me.Controls.Add(Me.RbtDetailLgr)
        Me.Controls.Add(Me.grpDateRange)
        Me.Controls.Add(Me.BtnGenerate)
        Me.Controls.Add(Me.chkPostDatedCheque)
        Me.Controls.Add(Me.lblCostCenter)
        Me.Controls.Add(Me.lblCostHead)
        Me.Controls.Add(Me.lblAcName)
        Me.Controls.Add(Me.CmbCostCenter)
        Me.Controls.Add(Me.CmbCostHead)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmRptLedgerNew"
        Me.Text = "Legend Ledger Report"
        Me.grpDateRange.ResumeLayout(False)
        Me.grpDateRange.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.cmbAccounts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents lblAcName As System.Windows.Forms.Label
    Friend WithEvents grpDateRange As System.Windows.Forms.GroupBox
    Friend WithEvents dtpTo As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents dtpFrom As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents lblFromDate As System.Windows.Forms.Label
    Friend WithEvents lblToDate As System.Windows.Forms.Label
    Friend WithEvents cmbAccountLevel As System.Windows.Forms.ComboBox
    Friend WithEvents cmbAccountType As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents CmbCostHead As System.Windows.Forms.ComboBox
    Friend WithEvents CmbCostCenter As System.Windows.Forms.ComboBox
    Friend WithEvents lblCostHead As System.Windows.Forms.Label
    Friend WithEvents lblCostCenter As System.Windows.Forms.Label
    Friend WithEvents chkPostDatedCheque As System.Windows.Forms.CheckBox
    Friend WithEvents BtnGenerate As System.Windows.Forms.Button
    Friend WithEvents RbtDetailLgr As System.Windows.Forms.RadioButton
    Friend WithEvents RbtSummaryLgr As System.Windows.Forms.RadioButton
    Friend WithEvents chkUnPostedVoucher As System.Windows.Forms.CheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cmbAccounts As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents rbtByCode As System.Windows.Forms.RadioButton
    Friend WithEvents rbtByName As System.Windows.Forms.RadioButton
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents chkProjectWiseGroup As System.Windows.Forms.CheckBox
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents chkIncludeActiveAccount As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblCompany As System.Windows.Forms.Label
    Friend WithEvents cmbCompany As System.Windows.Forms.ComboBox
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptInvoiceAging
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
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn7 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ID")
        Dim UltraGridColumn8 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Name")
        Dim UltraGridColumn9 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Territory")
        Dim UltraGridColumn10 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("City")
        Dim UltraGridColumn11 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("State")
        Dim UltraGridColumn12 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("AcId")
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand2 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn1 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ID")
        Dim UltraGridColumn2 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Name")
        Dim UltraGridColumn3 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Territory")
        Dim UltraGridColumn4 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("City")
        Dim UltraGridColumn5 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("State")
        Dim UltraGridColumn6 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("AcId")
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRptInvoiceAging))
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkIncludeInvoices = New System.Windows.Forms.CheckBox()
        Me.lstCostCenter = New SimpleAccounts.uiListControl()
        Me.lstHeadCostCenter = New SimpleAccounts.uiListControl()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.pnlPeriod = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbCompany = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.lblCustomer = New System.Windows.Forms.Label()
        Me.cmbCustomer = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        Me.lblToDate = New System.Windows.Forms.Label()
        Me.lblDateFrom = New System.Windows.Forms.Label()
        Me.lblPeriod = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnHelp = New System.Windows.Forms.ToolStripButton()
        Me.lblRemainingCreditLimit = New System.Windows.Forms.Label()
        Me.lblCreditDays = New System.Windows.Forms.Label()
        Me.lblReceivables = New System.Windows.Forms.Label()
        Me.lblCreditLimit = New System.Windows.Forms.Label()
        Me.lblOpeningBalance = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label100 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraTabSharedControlsPage2 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlPeriod.SuspendLayout()
        CType(Me.cmbCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.Panel2)
        Me.UltraTabPageControl1.Controls.Add(Me.pnlPeriod)
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1194, 614)
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel2.Controls.Add(Me.chkIncludeInvoices)
        Me.Panel2.Controls.Add(Me.lstCostCenter)
        Me.Panel2.Controls.Add(Me.lstHeadCostCenter)
        Me.Panel2.Controls.Add(Me.btnShow)
        Me.Panel2.Location = New System.Drawing.Point(15, 225)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1165, 246)
        Me.Panel2.TabIndex = 2
        '
        'chkIncludeInvoices
        '
        Me.chkIncludeInvoices.AutoSize = True
        Me.chkIncludeInvoices.Location = New System.Drawing.Point(12, 210)
        Me.chkIncludeInvoices.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkIncludeInvoices.Name = "chkIncludeInvoices"
        Me.chkIncludeInvoices.Size = New System.Drawing.Size(212, 21)
        Me.chkIncludeInvoices.TabIndex = 3
        Me.chkIncludeInvoices.Text = "Include Knocked Off Invoices"
        Me.chkIncludeInvoices.UseVisualStyleBackColor = True
        '
        'lstCostCenter
        '
        Me.lstCostCenter.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstCostCenter.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstCostCenter.BackColor = System.Drawing.Color.Transparent
        Me.lstCostCenter.disableWhenChecked = False
        Me.lstCostCenter.HeadingLabelName = "lstCostCenter"
        Me.lstCostCenter.HeadingText = "Cost Center"
        Me.lstCostCenter.Location = New System.Drawing.Point(251, 4)
        Me.lstCostCenter.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.lstCostCenter.Name = "lstCostCenter"
        Me.lstCostCenter.ShowAddNewButton = False
        Me.lstCostCenter.ShowInverse = True
        Me.lstCostCenter.ShowMagnifierButton = False
        Me.lstCostCenter.ShowNoCheck = False
        Me.lstCostCenter.ShowResetAllButton = False
        Me.lstCostCenter.ShowSelectall = True
        Me.lstCostCenter.Size = New System.Drawing.Size(203, 196)
        Me.lstCostCenter.TabIndex = 1
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
        Me.lstHeadCostCenter.Location = New System.Drawing.Point(12, 4)
        Me.lstHeadCostCenter.Margin = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.lstHeadCostCenter.Name = "lstHeadCostCenter"
        Me.lstHeadCostCenter.ShowAddNewButton = False
        Me.lstHeadCostCenter.ShowInverse = True
        Me.lstHeadCostCenter.ShowMagnifierButton = False
        Me.lstHeadCostCenter.ShowNoCheck = False
        Me.lstHeadCostCenter.ShowResetAllButton = False
        Me.lstHeadCostCenter.ShowSelectall = True
        Me.lstHeadCostCenter.Size = New System.Drawing.Size(203, 196)
        Me.lstHeadCostCenter.TabIndex = 0
        Me.lstHeadCostCenter.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(329, 206)
        Me.btnShow.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(95, 28)
        Me.btnShow.TabIndex = 2
        Me.btnShow.Text = "Show"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'pnlPeriod
        '
        Me.pnlPeriod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlPeriod.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.pnlPeriod.Controls.Add(Me.Label2)
        Me.pnlPeriod.Controls.Add(Me.cmbCompany)
        Me.pnlPeriod.Controls.Add(Me.lblCustomer)
        Me.pnlPeriod.Controls.Add(Me.cmbCustomer)
        Me.pnlPeriod.Controls.Add(Me.dtpToDate)
        Me.pnlPeriod.Controls.Add(Me.dtpFromDate)
        Me.pnlPeriod.Controls.Add(Me.cmbPeriod)
        Me.pnlPeriod.Controls.Add(Me.lblToDate)
        Me.pnlPeriod.Controls.Add(Me.lblDateFrom)
        Me.pnlPeriod.Controls.Add(Me.lblPeriod)
        Me.pnlPeriod.Location = New System.Drawing.Point(13, 68)
        Me.pnlPeriod.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnlPeriod.Name = "pnlPeriod"
        Me.pnlPeriod.Size = New System.Drawing.Size(1166, 150)
        Me.pnlPeriod.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 16)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 17)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Company"
        '
        'cmbCompany
        '
        Me.cmbCompany.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cmbCompany.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbCompany.CheckedListSettings.CheckStateMember = ""
        Appearance1.BackColor = System.Drawing.Color.White
        Appearance1.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        Me.cmbCompany.DisplayLayout.Appearance = Appearance1
        UltraGridColumn7.Header.VisiblePosition = 0
        UltraGridColumn7.Hidden = True
        UltraGridColumn8.Header.VisiblePosition = 1
        UltraGridColumn8.Width = 141
        UltraGridColumn9.Header.VisiblePosition = 2
        UltraGridColumn10.Header.VisiblePosition = 3
        UltraGridColumn11.Header.VisiblePosition = 4
        UltraGridColumn12.Header.VisiblePosition = 5
        UltraGridColumn12.Hidden = True
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn7, UltraGridColumn8, UltraGridColumn9, UltraGridColumn10, UltraGridColumn11, UltraGridColumn12})
        Me.cmbCompany.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbCompany.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbCompany.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbCompany.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbCompany.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Appearance2.BackColor = System.Drawing.Color.Transparent
        Me.cmbCompany.DisplayLayout.Override.CardAreaAppearance = Appearance2
        Me.cmbCompany.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbCompany.DisplayLayout.Override.CellPadding = 3
        Me.cmbCompany.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance3.TextHAlignAsString = "Left"
        Me.cmbCompany.DisplayLayout.Override.HeaderAppearance = Appearance3
        Me.cmbCompany.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance4.BorderColor = System.Drawing.Color.LightGray
        Appearance4.TextVAlignAsString = "Middle"
        Me.cmbCompany.DisplayLayout.Override.RowAppearance = Appearance4
        Appearance5.BackColor = System.Drawing.Color.LightSteelBlue
        Appearance5.BorderColor = System.Drawing.Color.Black
        Appearance5.ForeColor = System.Drawing.Color.Black
        Me.cmbCompany.DisplayLayout.Override.SelectedRowAppearance = Appearance5
        Me.cmbCompany.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbCompany.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbCompany.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbCompany.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.cmbCompany.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbCompany.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbCompany.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbCompany.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbCompany.LimitToList = True
        Me.cmbCompany.Location = New System.Drawing.Point(104, 9)
        Me.cmbCompany.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbCompany.Name = "cmbCompany"
        Me.cmbCompany.Size = New System.Drawing.Size(321, 25)
        Me.cmbCompany.TabIndex = 24
        '
        'lblCustomer
        '
        Me.lblCustomer.AutoSize = True
        Me.lblCustomer.Location = New System.Drawing.Point(9, 46)
        Me.lblCustomer.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(68, 17)
        Me.lblCustomer.TabIndex = 3
        Me.lblCustomer.Text = "Customer"
        '
        'cmbCustomer
        '
        Me.cmbCustomer.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cmbCustomer.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbCustomer.CheckedListSettings.CheckStateMember = ""
        Appearance6.BackColor = System.Drawing.Color.White
        Appearance6.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        Me.cmbCustomer.DisplayLayout.Appearance = Appearance6
        UltraGridColumn1.Header.VisiblePosition = 0
        UltraGridColumn1.Hidden = True
        UltraGridColumn2.Header.VisiblePosition = 1
        UltraGridColumn2.Width = 141
        UltraGridColumn3.Header.VisiblePosition = 2
        UltraGridColumn4.Header.VisiblePosition = 3
        UltraGridColumn5.Header.VisiblePosition = 4
        UltraGridColumn6.Header.VisiblePosition = 5
        UltraGridColumn6.Hidden = True
        UltraGridBand2.Columns.AddRange(New Object() {UltraGridColumn1, UltraGridColumn2, UltraGridColumn3, UltraGridColumn4, UltraGridColumn5, UltraGridColumn6})
        Me.cmbCustomer.DisplayLayout.BandsSerializer.Add(UltraGridBand2)
        Me.cmbCustomer.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbCustomer.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbCustomer.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Appearance7.BackColor = System.Drawing.Color.Transparent
        Me.cmbCustomer.DisplayLayout.Override.CardAreaAppearance = Appearance7
        Me.cmbCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbCustomer.DisplayLayout.Override.CellPadding = 3
        Me.cmbCustomer.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance8.TextHAlignAsString = "Left"
        Me.cmbCustomer.DisplayLayout.Override.HeaderAppearance = Appearance8
        Me.cmbCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance9.BorderColor = System.Drawing.Color.LightGray
        Appearance9.TextVAlignAsString = "Middle"
        Me.cmbCustomer.DisplayLayout.Override.RowAppearance = Appearance9
        Appearance10.BackColor = System.Drawing.Color.LightSteelBlue
        Appearance10.BorderColor = System.Drawing.Color.Black
        Appearance10.ForeColor = System.Drawing.Color.Black
        Me.cmbCustomer.DisplayLayout.Override.SelectedRowAppearance = Appearance10
        Me.cmbCustomer.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbCustomer.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbCustomer.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbCustomer.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.cmbCustomer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbCustomer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbCustomer.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbCustomer.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbCustomer.LimitToList = True
        Me.cmbCustomer.Location = New System.Drawing.Point(104, 42)
        Me.cmbCustomer.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbCustomer.Name = "cmbCustomer"
        Me.cmbCustomer.Size = New System.Drawing.Size(321, 25)
        Me.cmbCustomer.TabIndex = 24
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(287, 111)
        Me.dtpToDate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(137, 22)
        Me.dtpToDate.TabIndex = 5
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(104, 111)
        Me.dtpFromDate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(137, 22)
        Me.dtpFromDate.TabIndex = 3
        '
        'cmbPeriod
        '
        Me.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPeriod.FormattingEnabled = True
        Me.cmbPeriod.Items.AddRange(New Object() {"Today", "Yesterday", "Current Week", "Current Month", "Current Year"})
        Me.cmbPeriod.Location = New System.Drawing.Point(104, 74)
        Me.cmbPeriod.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(320, 24)
        Me.cmbPeriod.TabIndex = 1
        '
        'lblToDate
        '
        Me.lblToDate.AutoSize = True
        Me.lblToDate.Location = New System.Drawing.Point(252, 118)
        Me.lblToDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(25, 17)
        Me.lblToDate.TabIndex = 4
        Me.lblToDate.Text = "To"
        '
        'lblDateFrom
        '
        Me.lblDateFrom.AutoSize = True
        Me.lblDateFrom.Location = New System.Drawing.Point(9, 118)
        Me.lblDateFrom.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDateFrom.Name = "lblDateFrom"
        Me.lblDateFrom.Size = New System.Drawing.Size(40, 17)
        Me.lblDateFrom.TabIndex = 2
        Me.lblDateFrom.Text = "From"
        '
        'lblPeriod
        '
        Me.lblPeriod.AutoSize = True
        Me.lblPeriod.Location = New System.Drawing.Point(9, 78)
        Me.lblPeriod.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPeriod.Name = "lblPeriod"
        Me.lblPeriod.Size = New System.Drawing.Size(49, 17)
        Me.lblPeriod.TabIndex = 0
        Me.lblPeriod.Text = "Period"
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.Button2)
        Me.pnlHeader.Controls.Add(Me.lblProgress)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1194, 62)
        Me.pnlHeader.TabIndex = 0
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.Button2.BackgroundImage = CType(resources.GetObject("Button2.BackgroundImage"), System.Drawing.Image)
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Button2.Location = New System.Drawing.Point(1132, 12)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(48, 37)
        Me.Button2.TabIndex = 2
        Me.Button2.UseVisualStyleBackColor = False
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(544, -1)
        Me.lblProgress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(355, 62)
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
        Me.lblHeader.Location = New System.Drawing.Point(21, 14)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(370, 29)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Knocked Off Invoice Aging"
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.CtrlGrdBar1)
        Me.UltraTabPageControl2.Controls.Add(Me.ToolStrip1)
        Me.UltraTabPageControl2.Controls.Add(Me.lblRemainingCreditLimit)
        Me.UltraTabPageControl2.Controls.Add(Me.lblCreditDays)
        Me.UltraTabPageControl2.Controls.Add(Me.lblReceivables)
        Me.UltraTabPageControl2.Controls.Add(Me.lblCreditLimit)
        Me.UltraTabPageControl2.Controls.Add(Me.lblOpeningBalance)
        Me.UltraTabPageControl2.Controls.Add(Me.Label5)
        Me.UltraTabPageControl2.Controls.Add(Me.Label4)
        Me.UltraTabPageControl2.Controls.Add(Me.Label100)
        Me.UltraTabPageControl2.Controls.Add(Me.Label3)
        Me.UltraTabPageControl2.Controls.Add(Me.Label1)
        Me.UltraTabPageControl2.Controls.Add(Me.grd)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-13333, -12308)
        Me.UltraTabPageControl2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1194, 614)
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(1150, 0)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CtrlGrdBar1.MyGrid = Me.grd
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(45, 31)
        Me.CtrlGrdBar1.TabIndex = 3
        '
        'grd
        '
        Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.grd.Location = New System.Drawing.Point(0, 122)
        Me.grd.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(1194, 500)
        Me.grd.TabIndex = 0
        Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnPrint, Me.toolStripSeparator1, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1194, 27)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(63, 24)
        Me.btnNew.Text = "&New"
        '
        'btnPrint
        '
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(63, 24)
        Me.btnPrint.Text = "&Print"
        Me.btnPrint.Visible = False
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 27)
        '
        'btnHelp
        '
        Me.btnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(24, 24)
        Me.btnHelp.Text = "He&lp"
        Me.btnHelp.Visible = False
        '
        'lblRemainingCreditLimit
        '
        Me.lblRemainingCreditLimit.AutoSize = True
        Me.lblRemainingCreditLimit.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemainingCreditLimit.Location = New System.Drawing.Point(801, 76)
        Me.lblRemainingCreditLimit.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRemainingCreditLimit.Name = "lblRemainingCreditLimit"
        Me.lblRemainingCreditLimit.Size = New System.Drawing.Size(0, 29)
        Me.lblRemainingCreditLimit.TabIndex = 1
        '
        'lblCreditDays
        '
        Me.lblCreditDays.AutoSize = True
        Me.lblCreditDays.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCreditDays.Location = New System.Drawing.Point(624, 76)
        Me.lblCreditDays.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCreditDays.Name = "lblCreditDays"
        Me.lblCreditDays.Size = New System.Drawing.Size(0, 29)
        Me.lblCreditDays.TabIndex = 1
        '
        'lblReceivables
        '
        Me.lblReceivables.AutoSize = True
        Me.lblReceivables.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblReceivables.Location = New System.Drawing.Point(468, 76)
        Me.lblReceivables.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblReceivables.Name = "lblReceivables"
        Me.lblReceivables.Size = New System.Drawing.Size(0, 29)
        Me.lblReceivables.TabIndex = 1
        '
        'lblCreditLimit
        '
        Me.lblCreditLimit.AutoSize = True
        Me.lblCreditLimit.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCreditLimit.Location = New System.Drawing.Point(284, 76)
        Me.lblCreditLimit.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCreditLimit.Name = "lblCreditLimit"
        Me.lblCreditLimit.Size = New System.Drawing.Size(0, 29)
        Me.lblCreditLimit.TabIndex = 1
        '
        'lblOpeningBalance
        '
        Me.lblOpeningBalance.AutoSize = True
        Me.lblOpeningBalance.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOpeningBalance.Location = New System.Drawing.Point(47, 76)
        Me.lblOpeningBalance.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblOpeningBalance.Name = "lblOpeningBalance"
        Me.lblOpeningBalance.Size = New System.Drawing.Size(0, 29)
        Me.lblOpeningBalance.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.Navy
        Me.Label5.Location = New System.Drawing.Point(789, 44)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(241, 29)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Remaining Credit Limit"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Navy
        Me.Label4.Location = New System.Drawing.Point(615, 44)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(134, 29)
        Me.Label4.TabIndex = 1
        Me.Label4.Text = "Credit Days:"
        '
        'Label100
        '
        Me.Label100.AutoSize = True
        Me.Label100.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label100.ForeColor = System.Drawing.Color.Navy
        Me.Label100.Location = New System.Drawing.Point(459, 44)
        Me.Label100.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label100.Name = "Label100"
        Me.Label100.Size = New System.Drawing.Size(110, 29)
        Me.Label100.TabIndex = 1
        Me.Label100.Text = "Received:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Navy
        Me.Label3.Location = New System.Drawing.Point(276, 44)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(136, 29)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Credit Limit:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Navy
        Me.Label1.Location = New System.Drawing.Point(39, 44)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(187, 29)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Opening Balance:"
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.UltraTabControl1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage2
        Me.UltraTabControl1.Size = New System.Drawing.Size(1196, 636)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 5
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
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-13333, -12308)
        Me.UltraTabSharedControlsPage1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1079, 535)
        '
        'UltraTabSharedControlsPage2
        '
        Me.UltraTabSharedControlsPage2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage2.Name = "UltraTabSharedControlsPage2"
        Me.UltraTabSharedControlsPage2.Size = New System.Drawing.Size(1194, 614)
        '
        'frmRptInvoiceAging
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1196, 636)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmRptInvoiceAging"
        Me.Text = "Knocked Off Invoice Aging"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnlPeriod.ResumeLayout(False)
        Me.pnlPeriod.PerformLayout()
        CType(Me.cmbCompany, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        Me.UltraTabPageControl2.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lstCostCenter As SimpleAccounts.uiListControl
    Friend WithEvents lstHeadCostCenter As SimpleAccounts.uiListControl
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents pnlPeriod As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbCompany As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents lblCustomer As System.Windows.Forms.Label
    Friend WithEvents cmbCustomer As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents lblToDate As System.Windows.Forms.Label
    Friend WithEvents lblDateFrom As System.Windows.Forms.Label
    Friend WithEvents lblPeriod As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents UltraTabSharedControlsPage2 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents lblCreditLimit As System.Windows.Forms.Label
    Friend WithEvents lblOpeningBalance As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents lblReceivables As System.Windows.Forms.Label
    Friend WithEvents Label100 As System.Windows.Forms.Label
    Friend WithEvents chkIncludeInvoices As System.Windows.Forms.CheckBox
    Friend WithEvents lblCreditDays As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblRemainingCreditLimit As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class

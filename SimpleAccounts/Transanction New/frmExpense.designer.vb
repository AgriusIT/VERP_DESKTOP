<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExpense
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
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand2 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn7 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("coa_detail_id")
        Dim UltraGridColumn8 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("detail_title")
        Dim UltraGridColumn9 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("detail_code")
        Dim UltraGridColumn10 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("account_type")
        Dim UltraGridColumn11 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("sub_sub_title")
        Dim UltraGridColumn12 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("sub_title")
        Dim UltraGridColumn13 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("main_title")
        Dim UltraGridColumn14 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("main_type")
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn1 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ID")
        Dim UltraGridColumn2 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Name")
        Dim UltraGridColumn3 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Territory")
        Dim UltraGridColumn4 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("City")
        Dim UltraGridColumn5 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("State")
        Dim UltraGridColumn6 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("AcId")
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExpense))
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.CtrlGrdBar3 = New SimpleAccounts.CtrlGrdBar()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblBalanceNumberConvertor = New System.Windows.Forms.Label()
        Me.lblCashBlncNumberConvertor = New System.Windows.Forms.Label()
        Me.lblAmountNumberConvertor = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtMemo = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.lblPostedBy = New System.Windows.Forms.Label()
        Me.cmbEmployee = New System.Windows.Forms.ComboBox()
        Me.lblCheckedBy = New System.Windows.Forms.Label()
        Me.txtReference = New Infragistics.Win.UltraWinEditors.UltraTextEditor()
        Me.lblCurrency = New System.Windows.Forms.Label()
        Me.cmbCashAccount = New System.Windows.Forms.ComboBox()
        Me.lblCurrencyRate = New System.Windows.Forms.Label()
        Me.cmbVoucherType = New System.Windows.Forms.ComboBox()
        Me.txtCurrencyRate = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cmbCurrency = New System.Windows.Forms.ComboBox()
        Me.txtCustomerBalance = New System.Windows.Forms.TextBox()
        Me.cmbCompany = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.chkEnableDepositAc = New System.Windows.Forms.CheckBox()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.chkChecked = New System.Windows.Forms.CheckBox()
        Me.dtVoucherDate = New System.Windows.Forms.DateTimePicker()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cmbCMFA = New System.Windows.Forms.ComboBox()
        Me.txtVoucherNo = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblCostCenter = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbCostCenter = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cmbChequeBook = New System.Windows.Forms.ComboBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.txtChequeNo = New System.Windows.Forms.TextBox()
        Me.dtChequeDate = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbAccounts = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.chkPost = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblCashBalance = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCashBalances = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.lblPrintStatus = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.BtnNew = New System.Windows.Forms.ToolStripButton()
        Me.BtnEdit = New System.Windows.Forms.ToolStripButton()
        Me.BtnSave = New System.Windows.Forms.ToolStripButton()
        Me.BtnDelete = New System.Windows.Forms.ToolStripButton()
        Me.BtnPrint = New System.Windows.Forms.ToolStripSplitButton()
        Me.PrintPaymentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintVoucherAttachmentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintUpdatedVoucherToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSplitButton1 = New System.Windows.Forms.ToolStripSplitButton()
        Me.ExpensesDetailToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.btnUpdateTimes = New System.Windows.Forms.ToolStripSplitButton()
        Me.btnReminder = New System.Windows.Forms.ToolStripButton()
        Me.BtnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnAttachment = New System.Windows.Forms.ToolStripSplitButton()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnSMSTemplate = New System.Windows.Forms.ToolStripButton()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbConfig = New System.Windows.Forms.ToolStripButton()
        Me.btnApprovalHistory = New System.Windows.Forms.ToolStripButton()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.CtrlGrdBar2 = New SimpleAccounts.CtrlGrdBar()
        Me.grdVouchers = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.btnSearchEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSearchPrint = New System.Windows.Forms.ToolStripSplitButton()
        Me.PrintPaymentToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintSelectedVouchersToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintAttachmentVoucherToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.btnUpdatedVoucher = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnSearchDelete = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnSearchLoadAll = New System.Windows.Forms.ToolStripButton()
        Me.btnSearchDocument = New System.Windows.Forms.ToolStripButton()
        Me.btnReminder1 = New System.Windows.Forms.ToolStripButton()
        Me.Btn_SaveAttachment = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnTasks = New System.Windows.Forms.ToolStripButton()
        Me.btnGetAllRecord = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Search = New System.Windows.Forms.Button()
        Me.dtpSearchChequeDate = New System.Windows.Forms.DateTimePicker()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtFromAmount = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtToAmount = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtSearchChequeNo = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtSearchComments = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtSearchVoucherNo = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.cmbSearchAccount = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.cmbSearchVoucherType = New System.Windows.Forms.ComboBox()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.BackgroundWorker2 = New System.ComponentModel.BackgroundWorker()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.btnReplace = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.UltraTabPageControl1.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.txtMemo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.txtReference, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.cmbAccounts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdVouchers, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip2.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.cmbSearchAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.CtrlGrdBar3)
        Me.UltraTabPageControl1.Controls.Add(Me.Panel1)
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.lblProgress)
        Me.UltraTabPageControl1.Controls.Add(Me.lblPrintStatus)
        Me.UltraTabPageControl1.Controls.Add(Me.ToolStrip1)
        Me.UltraTabPageControl1.Controls.Add(Me.Label11)
        Me.UltraTabPageControl1.Controls.Add(Me.Label13)
        Me.UltraTabPageControl1.Controls.Add(Me.grd)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1044, 591)
        '
        'CtrlGrdBar3
        '
        Me.CtrlGrdBar3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar3.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar3.Email = Nothing
        Me.CtrlGrdBar3.FormName = Me
        Me.CtrlGrdBar3.Location = New System.Drawing.Point(1005, -1)
        Me.CtrlGrdBar3.MyGrid = Me.grd
        Me.CtrlGrdBar3.Name = "CtrlGrdBar3"
        Me.CtrlGrdBar3.Size = New System.Drawing.Size(38, 27)
        Me.CtrlGrdBar3.TabIndex = 80
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
        Me.grd.Location = New System.Drawing.Point(1, 402)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(1042, 188)
        Me.grd.TabIndex = 39
        Me.grd.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        Me.grd.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grd, "Expense Detail")
        Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.lblBalanceNumberConvertor)
        Me.Panel1.Controls.Add(Me.lblCashBlncNumberConvertor)
        Me.Panel1.Controls.Add(Me.lblAmountNumberConvertor)
        Me.Panel1.Controls.Add(Me.Label30)
        Me.Panel1.Controls.Add(Me.txtMemo)
        Me.Panel1.Controls.Add(Me.lblPostedBy)
        Me.Panel1.Controls.Add(Me.cmbEmployee)
        Me.Panel1.Controls.Add(Me.lblCheckedBy)
        Me.Panel1.Controls.Add(Me.txtReference)
        Me.Panel1.Controls.Add(Me.lblCurrency)
        Me.Panel1.Controls.Add(Me.cmbCashAccount)
        Me.Panel1.Controls.Add(Me.lblCurrencyRate)
        Me.Panel1.Controls.Add(Me.cmbVoucherType)
        Me.Panel1.Controls.Add(Me.txtCurrencyRate)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.cmbCurrency)
        Me.Panel1.Controls.Add(Me.txtCustomerBalance)
        Me.Panel1.Controls.Add(Me.cmbCompany)
        Me.Panel1.Controls.Add(Me.Label15)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.chkEnableDepositAc)
        Me.Panel1.Controls.Add(Me.txtAmount)
        Me.Panel1.Controls.Add(Me.chkChecked)
        Me.Panel1.Controls.Add(Me.dtVoucherDate)
        Me.Panel1.Controls.Add(Me.Label29)
        Me.Panel1.Controls.Add(Me.Label12)
        Me.Panel1.Controls.Add(Me.cmbCMFA)
        Me.Panel1.Controls.Add(Me.txtVoucherNo)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.lblCostCenter)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.cmbCostCenter)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label28)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.cmbAccounts)
        Me.Panel1.Controls.Add(Me.chkPost)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.lblCashBalance)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtCashBalances)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.btnCancel)
        Me.Panel1.Controls.Add(Me.btnReplace)
        Me.Panel1.Controls.Add(Me.btnAdd)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(0, 67)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1044, 335)
        Me.Panel1.TabIndex = 79
        '
        'lblBalanceNumberConvertor
        '
        Me.lblBalanceNumberConvertor.AutoSize = True
        Me.lblBalanceNumberConvertor.Location = New System.Drawing.Point(526, 97)
        Me.lblBalanceNumberConvertor.Name = "lblBalanceNumberConvertor"
        Me.lblBalanceNumberConvertor.Size = New System.Drawing.Size(12, 13)
        Me.lblBalanceNumberConvertor.TabIndex = 59
        Me.lblBalanceNumberConvertor.Text = "-"
        '
        'lblCashBlncNumberConvertor
        '
        Me.lblCashBlncNumberConvertor.AutoSize = True
        Me.lblCashBlncNumberConvertor.Location = New System.Drawing.Point(527, 187)
        Me.lblCashBlncNumberConvertor.Name = "lblCashBlncNumberConvertor"
        Me.lblCashBlncNumberConvertor.Size = New System.Drawing.Size(12, 13)
        Me.lblCashBlncNumberConvertor.TabIndex = 58
        Me.lblCashBlncNumberConvertor.Text = "-"
        '
        'lblAmountNumberConvertor
        '
        Me.lblAmountNumberConvertor.AutoSize = True
        Me.lblAmountNumberConvertor.Location = New System.Drawing.Point(112, 108)
        Me.lblAmountNumberConvertor.Name = "lblAmountNumberConvertor"
        Me.lblAmountNumberConvertor.Size = New System.Drawing.Size(12, 13)
        Me.lblAmountNumberConvertor.TabIndex = 57
        Me.lblAmountNumberConvertor.Text = "-"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(4, 5)
        Me.Label30.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(62, 13)
        Me.Label30.TabIndex = 2
        Me.Label30.Text = "Company"
        '
        'txtMemo
        '
        Me.txtMemo.Location = New System.Drawing.Point(524, 201)
        Me.txtMemo.Name = "txtMemo"
        Me.txtMemo.Size = New System.Drawing.Size(253, 22)
        Me.txtMemo.TabIndex = 30
        '
        'lblPostedBy
        '
        Me.lblPostedBy.AutoSize = True
        Me.lblPostedBy.Location = New System.Drawing.Point(248, 279)
        Me.lblPostedBy.Name = "lblPostedBy"
        Me.lblPostedBy.Size = New System.Drawing.Size(14, 13)
        Me.lblPostedBy.TabIndex = 56
        Me.lblPostedBy.Text = "p"
        '
        'cmbEmployee
        '
        Me.cmbEmployee.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbEmployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEmployee.FormattingEnabled = True
        Me.cmbEmployee.Location = New System.Drawing.Point(111, 180)
        Me.cmbEmployee.Name = "cmbEmployee"
        Me.cmbEmployee.Size = New System.Drawing.Size(259, 21)
        Me.cmbEmployee.TabIndex = 28
        '
        'lblCheckedBy
        '
        Me.lblCheckedBy.AutoSize = True
        Me.lblCheckedBy.Location = New System.Drawing.Point(108, 279)
        Me.lblCheckedBy.Name = "lblCheckedBy"
        Me.lblCheckedBy.Size = New System.Drawing.Size(13, 13)
        Me.lblCheckedBy.TabIndex = 55
        Me.lblCheckedBy.Text = "c"
        '
        'txtReference
        '
        Me.txtReference.Location = New System.Drawing.Point(111, 152)
        Me.txtReference.Name = "txtReference"
        Me.txtReference.Size = New System.Drawing.Size(259, 22)
        Me.txtReference.TabIndex = 24
        '
        'lblCurrency
        '
        Me.lblCurrency.AutoSize = True
        Me.lblCurrency.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrency.Location = New System.Drawing.Point(401, 5)
        Me.lblCurrency.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCurrency.Name = "lblCurrency"
        Me.lblCurrency.Size = New System.Drawing.Size(60, 13)
        Me.lblCurrency.TabIndex = 53
        Me.lblCurrency.Text = "Currency"
        '
        'cmbCashAccount
        '
        Me.cmbCashAccount.BackColor = System.Drawing.SystemColors.Info
        Me.cmbCashAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCashAccount.FormattingEnabled = True
        Me.cmbCashAccount.Location = New System.Drawing.Point(524, 137)
        Me.cmbCashAccount.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbCashAccount.Name = "cmbCashAccount"
        Me.cmbCashAccount.Size = New System.Drawing.Size(253, 21)
        Me.cmbCashAccount.TabIndex = 21
        Me.ToolTip1.SetToolTip(Me.cmbCashAccount, "Select Payment Account")
        '
        'lblCurrencyRate
        '
        Me.lblCurrencyRate.AutoSize = True
        Me.lblCurrencyRate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrencyRate.Location = New System.Drawing.Point(401, 32)
        Me.lblCurrencyRate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCurrencyRate.Name = "lblCurrencyRate"
        Me.lblCurrencyRate.Size = New System.Drawing.Size(90, 13)
        Me.lblCurrencyRate.TabIndex = 54
        Me.lblCurrencyRate.Text = "Currency Rate"
        '
        'cmbVoucherType
        '
        Me.cmbVoucherType.BackColor = System.Drawing.SystemColors.Info
        Me.cmbVoucherType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbVoucherType.FormattingEnabled = True
        Me.cmbVoucherType.Location = New System.Drawing.Point(111, 125)
        Me.cmbVoucherType.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbVoucherType.Name = "cmbVoucherType"
        Me.cmbVoucherType.Size = New System.Drawing.Size(259, 21)
        Me.cmbVoucherType.TabIndex = 19
        Me.ToolTip1.SetToolTip(Me.cmbVoucherType, "Select Payment Method")
        '
        'txtCurrencyRate
        '
        Me.txtCurrencyRate.Location = New System.Drawing.Point(524, 29)
        Me.txtCurrencyRate.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtCurrencyRate.Name = "txtCurrencyRate"
        Me.txtCurrencyRate.Size = New System.Drawing.Size(253, 21)
        Me.txtCurrencyRate.TabIndex = 52
        Me.ToolTip1.SetToolTip(Me.txtCurrencyRate, "Receipt Amount")
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(4, 33)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(76, 13)
        Me.Label16.TabIndex = 4
        Me.Label16.Text = "Voucher No."
        '
        'cmbCurrency
        '
        Me.cmbCurrency.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbCurrency.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCurrency.FormattingEnabled = True
        Me.cmbCurrency.Location = New System.Drawing.Point(524, 2)
        Me.cmbCurrency.Name = "cmbCurrency"
        Me.cmbCurrency.Size = New System.Drawing.Size(253, 21)
        Me.cmbCurrency.TabIndex = 51
        '
        'txtCustomerBalance
        '
        Me.txtCustomerBalance.BackColor = System.Drawing.SystemColors.ControlLight
        Me.txtCustomerBalance.Location = New System.Drawing.Point(524, 77)
        Me.txtCustomerBalance.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtCustomerBalance.Name = "txtCustomerBalance"
        Me.txtCustomerBalance.ReadOnly = True
        Me.txtCustomerBalance.Size = New System.Drawing.Size(140, 21)
        Me.txtCustomerBalance.TabIndex = 13
        Me.txtCustomerBalance.TabStop = False
        Me.ToolTip1.SetToolTip(Me.txtCustomerBalance, "Current Expense Balance")
        '
        'cmbCompany
        '
        Me.cmbCompany.BackColor = System.Drawing.SystemColors.Info
        Me.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCompany.FormattingEnabled = True
        Me.cmbCompany.Location = New System.Drawing.Point(111, 2)
        Me.cmbCompany.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbCompany.Name = "cmbCompany"
        Me.cmbCompany.Size = New System.Drawing.Size(259, 21)
        Me.cmbCompany.TabIndex = 3
        Me.cmbCompany.TabStop = False
        Me.ToolTip1.SetToolTip(Me.cmbCompany, "Select Company")
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(218, 33)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(39, 13)
        Me.Label15.TabIndex = 6
        Me.Label15.Text = "Date:"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.Navy
        Me.Label14.Location = New System.Drawing.Point(392, 112)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(389, 16)
        Me.Label14.TabIndex = 16
        Me.Label14.Text = "Withdraw From: _____________________________"
        '
        'chkEnableDepositAc
        '
        Me.chkEnableDepositAc.AutoSize = True
        Me.chkEnableDepositAc.Location = New System.Drawing.Point(784, 139)
        Me.chkEnableDepositAc.Name = "chkEnableDepositAc"
        Me.chkEnableDepositAc.Size = New System.Drawing.Size(71, 17)
        Me.chkEnableDepositAc.TabIndex = 22
        Me.chkEnableDepositAc.TabStop = False
        Me.chkEnableDepositAc.Text = "Enabled"
        Me.chkEnableDepositAc.UseVisualStyleBackColor = True
        '
        'txtAmount
        '
        Me.txtAmount.Location = New System.Drawing.Point(111, 86)
        Me.txtAmount.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(259, 21)
        Me.txtAmount.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.txtAmount, "Expense Amount")
        '
        'chkChecked
        '
        Me.chkChecked.AutoSize = True
        Me.chkChecked.Checked = True
        Me.chkChecked.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkChecked.Location = New System.Drawing.Point(111, 261)
        Me.chkChecked.Name = "chkChecked"
        Me.chkChecked.Size = New System.Drawing.Size(76, 17)
        Me.chkChecked.TabIndex = 35
        Me.chkChecked.TabStop = False
        Me.chkChecked.Text = "Checked"
        Me.chkChecked.UseVisualStyleBackColor = True
        '
        'dtVoucherDate
        '
        Me.dtVoucherDate.CustomFormat = "dd/MM/yyyy"
        Me.dtVoucherDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtVoucherDate.Location = New System.Drawing.Point(263, 29)
        Me.dtVoucherDate.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.dtVoucherDate.Name = "dtVoucherDate"
        Me.dtVoucherDate.Size = New System.Drawing.Size(105, 21)
        Me.dtVoucherDate.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.dtVoucherDate, "Voucher Date")
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label29.Location = New System.Drawing.Point(4, 237)
        Me.Label29.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(38, 13)
        Me.Label29.TabIndex = 33
        Me.Label29.Text = "CMFA"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.OrangeRed
        Me.Label12.Location = New System.Drawing.Point(375, 118)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(19, 18)
        Me.Label12.TabIndex = 29
        Me.Label12.Text = "*"
        '
        'cmbCMFA
        '
        Me.cmbCMFA.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbCMFA.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCMFA.FormattingEnabled = True
        Me.cmbCMFA.Location = New System.Drawing.Point(111, 234)
        Me.cmbCMFA.Name = "cmbCMFA"
        Me.cmbCMFA.Size = New System.Drawing.Size(259, 21)
        Me.cmbCMFA.TabIndex = 34
        '
        'txtVoucherNo
        '
        Me.txtVoucherNo.Location = New System.Drawing.Point(111, 30)
        Me.txtVoucherNo.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtVoucherNo.Name = "txtVoucherNo"
        Me.txtVoucherNo.ReadOnly = True
        Me.txtVoucherNo.Size = New System.Drawing.Size(108, 21)
        Me.txtVoucherNo.TabIndex = 5
        Me.txtVoucherNo.TabStop = False
        Me.ToolTip1.SetToolTip(Me.txtVoucherNo, "Voucher No")
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(401, 141)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(61, 13)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "Pay From"
        '
        'lblCostCenter
        '
        Me.lblCostCenter.AutoSize = True
        Me.lblCostCenter.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCostCenter.Location = New System.Drawing.Point(4, 211)
        Me.lblCostCenter.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCostCenter.Name = "lblCostCenter"
        Me.lblCostCenter.Size = New System.Drawing.Size(76, 13)
        Me.lblCostCenter.TabIndex = 31
        Me.lblCostCenter.Text = "Cost Center"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Navy
        Me.Label2.Location = New System.Drawing.Point(392, 55)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(394, 16)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Balance:  ___________________________________"
        '
        'cmbCostCenter
        '
        Me.cmbCostCenter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbCostCenter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCostCenter.FormattingEnabled = True
        Me.cmbCostCenter.Location = New System.Drawing.Point(111, 207)
        Me.cmbCostCenter.Name = "cmbCostCenter"
        Me.cmbCostCenter.Size = New System.Drawing.Size(259, 21)
        Me.cmbCostCenter.TabIndex = 32
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(397, 81)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(127, 13)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "Expense A/c Balance"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(4, 184)
        Me.Label28.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(63, 13)
        Me.Label28.TabIndex = 27
        Me.Label28.Text = "Employee"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cmbChequeBook)
        Me.GroupBox1.Controls.Add(Me.Label31)
        Me.GroupBox1.Controls.Add(Me.txtChequeNo)
        Me.GroupBox1.Controls.Add(Me.dtChequeDate)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Location = New System.Drawing.Point(388, 224)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(395, 70)
        Me.GroupBox1.TabIndex = 37
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Bank Information"
        '
        'cmbChequeBook
        '
        Me.cmbChequeBook.BackColor = System.Drawing.SystemColors.Info
        Me.cmbChequeBook.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbChequeBook.FormattingEnabled = True
        Me.cmbChequeBook.Location = New System.Drawing.Point(135, 16)
        Me.cmbChequeBook.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbChequeBook.Name = "cmbChequeBook"
        Me.cmbChequeBook.Size = New System.Drawing.Size(253, 21)
        Me.cmbChequeBook.TabIndex = 56
        Me.ToolTip1.SetToolTip(Me.cmbChequeBook, "Select Cheque Book")
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(16, 23)
        Me.Label31.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(84, 13)
        Me.Label31.TabIndex = 55
        Me.Label31.Text = "Cheque Book"
        '
        'txtChequeNo
        '
        Me.txtChequeNo.Location = New System.Drawing.Point(136, 43)
        Me.txtChequeNo.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtChequeNo.Name = "txtChequeNo"
        Me.txtChequeNo.Size = New System.Drawing.Size(96, 21)
        Me.txtChequeNo.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtChequeNo, "Cheque No")
        '
        'dtChequeDate
        '
        Me.dtChequeDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtChequeDate.Location = New System.Drawing.Point(284, 42)
        Me.dtChequeDate.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.dtChequeDate.Name = "dtChequeDate"
        Me.dtChequeDate.Size = New System.Drawing.Size(105, 21)
        Me.dtChequeDate.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.dtChequeDate, "Check Date")
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(16, 47)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(74, 13)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Cheque No."
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(242, 47)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(34, 13)
        Me.Label17.TabIndex = 2
        Me.Label17.Text = "Date"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(401, 205)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 13)
        Me.Label6.TabIndex = 29
        Me.Label6.Text = "Memo"
        '
        'cmbAccounts
        '
        Appearance7.BackColor = System.Drawing.SystemColors.Info
        Me.cmbAccounts.Appearance = Appearance7
        Me.cmbAccounts.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cmbAccounts.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbAccounts.CheckedListSettings.CheckStateMember = ""
        Me.cmbAccounts.DisplayLayout.AddNewBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance8.BackColor = System.Drawing.Color.White
        Me.cmbAccounts.DisplayLayout.Appearance = Appearance8
        UltraGridColumn7.Header.Caption = "ID"
        UltraGridColumn7.Header.VisiblePosition = 0
        UltraGridColumn7.Hidden = True
        UltraGridColumn7.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(8, 0)
        UltraGridColumn8.Header.Caption = "Account"
        UltraGridColumn8.Header.VisiblePosition = 1
        UltraGridColumn8.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(128, 0)
        UltraGridColumn9.Header.Caption = "Code"
        UltraGridColumn9.Header.VisiblePosition = 2
        UltraGridColumn10.Header.Caption = "Type"
        UltraGridColumn10.Header.VisiblePosition = 3
        UltraGridColumn10.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(96, 0)
        UltraGridColumn11.Header.Caption = "Sub Sub Ac"
        UltraGridColumn11.Header.VisiblePosition = 4
        UltraGridColumn11.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(106, 0)
        UltraGridColumn12.Header.Caption = "Sub Ac"
        UltraGridColumn12.Header.VisiblePosition = 5
        UltraGridColumn12.Hidden = True
        UltraGridColumn12.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(102, 0)
        UltraGridColumn13.Header.Caption = "Main Ac"
        UltraGridColumn13.Header.VisiblePosition = 6
        UltraGridColumn13.Hidden = True
        UltraGridColumn13.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(82, 0)
        UltraGridColumn14.Header.Caption = "Ac Head"
        UltraGridColumn14.Header.VisiblePosition = 7
        UltraGridColumn14.Hidden = True
        UltraGridColumn14.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(84, 0)
        UltraGridBand2.Columns.AddRange(New Object() {UltraGridColumn7, UltraGridColumn8, UltraGridColumn9, UltraGridColumn10, UltraGridColumn11, UltraGridColumn12, UltraGridColumn13, UltraGridColumn14})
        Me.cmbAccounts.DisplayLayout.BandsSerializer.Add(UltraGridBand2)
        Me.cmbAccounts.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbAccounts.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbAccounts.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbAccounts.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Appearance9.BackColor = System.Drawing.Color.Transparent
        Me.cmbAccounts.DisplayLayout.Override.CardAreaAppearance = Appearance9
        Me.cmbAccounts.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbAccounts.DisplayLayout.Override.CellPadding = 3
        Me.cmbAccounts.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance10.TextHAlignAsString = "Left"
        Me.cmbAccounts.DisplayLayout.Override.HeaderAppearance = Appearance10
        Me.cmbAccounts.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance11.BorderColor = System.Drawing.Color.LightGray
        Appearance11.TextVAlignAsString = "Middle"
        Me.cmbAccounts.DisplayLayout.Override.RowAppearance = Appearance11
        Appearance12.BackColor = System.Drawing.Color.LightSteelBlue
        Appearance12.BorderColor = System.Drawing.Color.Black
        Appearance12.ForeColor = System.Drawing.Color.Black
        Me.cmbAccounts.DisplayLayout.Override.SelectedRowAppearance = Appearance12
        Me.cmbAccounts.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbAccounts.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbAccounts.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbAccounts.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.cmbAccounts.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbAccounts.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbAccounts.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbAccounts.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbAccounts.LimitToList = True
        Me.cmbAccounts.Location = New System.Drawing.Point(111, 57)
        Me.cmbAccounts.MaxDropDownItems = 16
        Me.cmbAccounts.Name = "cmbAccounts"
        Me.cmbAccounts.Size = New System.Drawing.Size(259, 23)
        Me.cmbAccounts.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.cmbAccounts, "Select Expense Account")
        '
        'chkPost
        '
        Me.chkPost.AutoSize = True
        Me.chkPost.Checked = True
        Me.chkPost.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPost.Location = New System.Drawing.Point(251, 261)
        Me.chkPost.Name = "chkPost"
        Me.chkPost.Size = New System.Drawing.Size(64, 17)
        Me.chkPost.TabIndex = 36
        Me.chkPost.TabStop = False
        Me.chkPost.Text = "Posted"
        Me.chkPost.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(4, 157)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 13)
        Me.Label5.TabIndex = 23
        Me.Label5.Text = "Reference"
        '
        'lblCashBalance
        '
        Me.lblCashBalance.AutoSize = True
        Me.lblCashBalance.Location = New System.Drawing.Point(401, 168)
        Me.lblCashBalance.Name = "lblCashBalance"
        Me.lblCashBalance.Size = New System.Drawing.Size(90, 13)
        Me.lblCashBalance.TabIndex = 25
        Me.lblCashBalance.Text = "Cash Balance:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(4, 129)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(102, 13)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "Payment Method"
        '
        'txtCashBalances
        '
        Me.txtCashBalances.BackColor = System.Drawing.SystemColors.ControlLight
        Me.txtCashBalances.Location = New System.Drawing.Point(524, 164)
        Me.txtCashBalances.Name = "txtCashBalances"
        Me.txtCashBalances.ReadOnly = True
        Me.txtCashBalances.Size = New System.Drawing.Size(140, 21)
        Me.txtCashBalances.TabIndex = 26
        Me.txtCashBalances.TabStop = False
        Me.ToolTip1.SetToolTip(Me.txtCashBalances, "Current Cash Balance")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(4, 90)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(51, 13)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Amount"
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(728, 300)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(55, 23)
        Me.btnAdd.TabIndex = 38
        Me.btnAdd.Text = "+"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(4, 62)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 13)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Expense A/c"
        '
        'pnlHeader
        '
        Me.pnlHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Location = New System.Drawing.Point(0, 25)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1044, 42)
        Me.pnlHeader.TabIndex = 78
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(12, 9)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(100, 23)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Expense"
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(358, 420)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 40
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'lblPrintStatus
        '
        Me.lblPrintStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPrintStatus.AutoSize = True
        Me.lblPrintStatus.Location = New System.Drawing.Point(872, 34)
        Me.lblPrintStatus.Name = "lblPrintStatus"
        Me.lblPrintStatus.Size = New System.Drawing.Size(161, 13)
        Me.lblPrintStatus.TabIndex = 41
        Me.lblPrintStatus.Text = "Print Status : Print Pending"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BtnNew, Me.BtnEdit, Me.BtnSave, Me.BtnDelete, Me.BtnPrint, Me.toolStripSeparator1, Me.ToolStripSplitButton1, Me.ToolStripButton2, Me.btnUpdateTimes, Me.btnReminder, Me.BtnRefresh, Me.ToolStripSeparator2, Me.btnAttachment, Me.ToolStripSeparator5, Me.btnSMSTemplate, Me.HelpToolStripButton, Me.ToolStripSeparator7, Me.tsbConfig, Me.btnApprovalHistory})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1004, 25)
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
        'BtnPrint
        '
        Me.BtnPrint.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrintPaymentToolStripMenuItem, Me.PrintVoucherAttachmentToolStripMenuItem, Me.PrintUpdatedVoucherToolStripMenuItem})
        Me.BtnPrint.Image = CType(resources.GetObject("BtnPrint.Image"), System.Drawing.Image)
        Me.BtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(64, 22)
        Me.BtnPrint.Text = "&Print"
        '
        'PrintPaymentToolStripMenuItem
        '
        Me.PrintPaymentToolStripMenuItem.Name = "PrintPaymentToolStripMenuItem"
        Me.PrintPaymentToolStripMenuItem.Size = New System.Drawing.Size(211, 22)
        Me.PrintPaymentToolStripMenuItem.Text = "Print Payment"
        '
        'PrintVoucherAttachmentToolStripMenuItem
        '
        Me.PrintVoucherAttachmentToolStripMenuItem.Name = "PrintVoucherAttachmentToolStripMenuItem"
        Me.PrintVoucherAttachmentToolStripMenuItem.Size = New System.Drawing.Size(211, 22)
        Me.PrintVoucherAttachmentToolStripMenuItem.Text = "Print Attachment Voucher"
        '
        'PrintUpdatedVoucherToolStripMenuItem
        '
        Me.PrintUpdatedVoucherToolStripMenuItem.Name = "PrintUpdatedVoucherToolStripMenuItem"
        Me.PrintUpdatedVoucherToolStripMenuItem.Size = New System.Drawing.Size(211, 22)
        Me.PrintUpdatedVoucherToolStripMenuItem.Text = "Print Updated Voucher"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripSplitButton1
        '
        Me.ToolStripSplitButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripSplitButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExpensesDetailToolStripMenuItem})
        Me.ToolStripSplitButton1.Image = CType(resources.GetObject("ToolStripSplitButton1.Image"), System.Drawing.Image)
        Me.ToolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripSplitButton1.Name = "ToolStripSplitButton1"
        Me.ToolStripSplitButton1.Size = New System.Drawing.Size(79, 22)
        Me.ToolStripSplitButton1.Text = "Reports"
        '
        'ExpensesDetailToolStripMenuItem
        '
        Me.ExpensesDetailToolStripMenuItem.Name = "ExpensesDetailToolStripMenuItem"
        Me.ExpensesDetailToolStripMenuItem.Size = New System.Drawing.Size(154, 22)
        Me.ExpensesDetailToolStripMenuItem.Text = "Expenses Detail"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(129, 22)
        Me.ToolStripButton2.Text = "Add New Expense A/C"
        '
        'btnUpdateTimes
        '
        Me.btnUpdateTimes.Image = CType(resources.GetObject("btnUpdateTimes.Image"), System.Drawing.Image)
        Me.btnUpdateTimes.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnUpdateTimes.Name = "btnUpdateTimes"
        Me.btnUpdateTimes.Size = New System.Drawing.Size(141, 22)
        Me.btnUpdateTimes.Text = "No of update times"
        '
        'btnReminder
        '
        Me.btnReminder.Image = CType(resources.GetObject("btnReminder.Image"), System.Drawing.Image)
        Me.btnReminder.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnReminder.Name = "btnReminder"
        Me.btnReminder.Size = New System.Drawing.Size(78, 22)
        Me.btnReminder.Text = "Reminder"
        '
        'BtnRefresh
        '
        Me.BtnRefresh.Image = CType(resources.GetObject("BtnRefresh.Image"), System.Drawing.Image)
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
        'btnAttachment
        '
        Me.btnAttachment.Image = CType(resources.GetObject("btnAttachment.Image"), System.Drawing.Image)
        Me.btnAttachment.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAttachment.Name = "btnAttachment"
        Me.btnAttachment.Size = New System.Drawing.Size(102, 22)
        Me.btnAttachment.Text = "Attachment"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'btnSMSTemplate
        '
        Me.btnSMSTemplate.Image = CType(resources.GetObject("btnSMSTemplate.Image"), System.Drawing.Image)
        Me.btnSMSTemplate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSMSTemplate.Name = "btnSMSTemplate"
        Me.btnSMSTemplate.Size = New System.Drawing.Size(142, 20)
        Me.btnSMSTemplate.Text = "SMS Template Setting"
        '
        'HelpToolStripButton
        '
        Me.HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HelpToolStripButton.Image = CType(resources.GetObject("HelpToolStripButton.Image"), System.Drawing.Image)
        Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton.Name = "HelpToolStripButton"
        Me.HelpToolStripButton.Size = New System.Drawing.Size(23, 20)
        Me.HelpToolStripButton.Text = "He&lp"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(6, 25)
        '
        'tsbConfig
        '
        Me.tsbConfig.Image = Global.SimpleAccounts.My.Resources.Resources.Advanced_Options
        Me.tsbConfig.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbConfig.Name = "tsbConfig"
        Me.tsbConfig.Size = New System.Drawing.Size(63, 20)
        Me.tsbConfig.Text = "Config"
        '
        'btnApprovalHistory
        '
        Me.btnApprovalHistory.Image = Global.SimpleAccounts.My.Resources.Resources.Copy
        Me.btnApprovalHistory.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnApprovalHistory.Name = "btnApprovalHistory"
        Me.btnApprovalHistory.Size = New System.Drawing.Size(116, 20)
        Me.btnApprovalHistory.Text = "Approval History"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.OrangeRed
        Me.Label11.Location = New System.Drawing.Point(605, 184)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(19, 18)
        Me.Label11.TabIndex = 17
        Me.Label11.Text = "*"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.OrangeRed
        Me.Label13.Location = New System.Drawing.Point(605, 129)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(19, 18)
        Me.Label13.TabIndex = 9
        Me.Label13.Text = "*"
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.CtrlGrdBar2)
        Me.UltraTabPageControl2.Controls.Add(Me.ToolStrip2)
        Me.UltraTabPageControl2.Controls.Add(Me.SplitContainer1)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(854, 591)
        '
        'CtrlGrdBar2
        '
        Me.CtrlGrdBar2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar2.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar2.Email = Nothing
        Me.CtrlGrdBar2.FormName = Me
        Me.CtrlGrdBar2.Location = New System.Drawing.Point(817, -2)
        Me.CtrlGrdBar2.MyGrid = Me.grdVouchers
        Me.CtrlGrdBar2.Name = "CtrlGrdBar2"
        Me.CtrlGrdBar2.Size = New System.Drawing.Size(38, 27)
        Me.CtrlGrdBar2.TabIndex = 2
        '
        'grdVouchers
        '
        Me.grdVouchers.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdVouchers.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdVouchers.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdVouchers.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdVouchers.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdVouchers.GroupByBoxVisible = False
        Me.grdVouchers.Location = New System.Drawing.Point(0, 0)
        Me.grdVouchers.Name = "grdVouchers"
        Me.grdVouchers.RecordNavigator = True
        Me.grdVouchers.Size = New System.Drawing.Size(854, 412)
        Me.grdVouchers.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.grdVouchers, "Saved Record Detail")
        Me.grdVouchers.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'ToolStrip2
        '
        Me.ToolStrip2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ToolStrip2.AutoSize = False
        Me.ToolStrip2.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnSearchEdit, Me.btnSearchPrint, Me.toolStripSeparator, Me.btnUpdatedVoucher, Me.ToolStripSeparator6, Me.btnSearchDelete, Me.toolStripSeparator3, Me.btnSearchLoadAll, Me.btnSearchDocument, Me.btnReminder1, Me.Btn_SaveAttachment, Me.ToolStripSeparator4, Me.btnTasks, Me.btnGetAllRecord, Me.ToolStripSeparator8, Me.HelpToolStripButton1})
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(816, 25)
        Me.ToolStrip2.TabIndex = 0
        Me.ToolStrip2.Text = "ToolStrip2"
        '
        'btnSearchEdit
        '
        Me.btnSearchEdit.Image = CType(resources.GetObject("btnSearchEdit.Image"), System.Drawing.Image)
        Me.btnSearchEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSearchEdit.Name = "btnSearchEdit"
        Me.btnSearchEdit.Size = New System.Drawing.Size(47, 22)
        Me.btnSearchEdit.Text = "&Edit"
        '
        'btnSearchPrint
        '
        Me.btnSearchPrint.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrintPaymentToolStripMenuItem1, Me.PrintSelectedVouchersToolStripMenuItem, Me.PrintAttachmentVoucherToolStripMenuItem})
        Me.btnSearchPrint.Image = CType(resources.GetObject("btnSearchPrint.Image"), System.Drawing.Image)
        Me.btnSearchPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSearchPrint.Name = "btnSearchPrint"
        Me.btnSearchPrint.Size = New System.Drawing.Size(64, 22)
        Me.btnSearchPrint.Text = "&Print"
        '
        'PrintPaymentToolStripMenuItem1
        '
        Me.PrintPaymentToolStripMenuItem1.Name = "PrintPaymentToolStripMenuItem1"
        Me.PrintPaymentToolStripMenuItem1.Size = New System.Drawing.Size(211, 22)
        Me.PrintPaymentToolStripMenuItem1.Text = "Print Payment"
        '
        'PrintSelectedVouchersToolStripMenuItem
        '
        Me.PrintSelectedVouchersToolStripMenuItem.Name = "PrintSelectedVouchersToolStripMenuItem"
        Me.PrintSelectedVouchersToolStripMenuItem.Size = New System.Drawing.Size(211, 22)
        Me.PrintSelectedVouchersToolStripMenuItem.Text = "Print Selected Vouchers"
        '
        'PrintAttachmentVoucherToolStripMenuItem
        '
        Me.PrintAttachmentVoucherToolStripMenuItem.Name = "PrintAttachmentVoucherToolStripMenuItem"
        Me.PrintAttachmentVoucherToolStripMenuItem.Size = New System.Drawing.Size(211, 22)
        Me.PrintAttachmentVoucherToolStripMenuItem.Text = "Print Attachment Voucher"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'btnUpdatedVoucher
        '
        Me.btnUpdatedVoucher.Image = CType(resources.GetObject("btnUpdatedVoucher.Image"), System.Drawing.Image)
        Me.btnUpdatedVoucher.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnUpdatedVoucher.Name = "btnUpdatedVoucher"
        Me.btnUpdatedVoucher.Size = New System.Drawing.Size(146, 22)
        Me.btnUpdatedVoucher.Text = "Print Updated Voucher"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(6, 25)
        '
        'btnSearchDelete
        '
        Me.btnSearchDelete.Image = CType(resources.GetObject("btnSearchDelete.Image"), System.Drawing.Image)
        Me.btnSearchDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSearchDelete.Name = "btnSearchDelete"
        Me.btnSearchDelete.Size = New System.Drawing.Size(60, 22)
        Me.btnSearchDelete.Text = "D&elete"
        '
        'toolStripSeparator3
        '
        Me.toolStripSeparator3.Name = "toolStripSeparator3"
        Me.toolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'btnSearchLoadAll
        '
        Me.btnSearchLoadAll.Image = CType(resources.GetObject("btnSearchLoadAll.Image"), System.Drawing.Image)
        Me.btnSearchLoadAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSearchLoadAll.Name = "btnSearchLoadAll"
        Me.btnSearchLoadAll.Size = New System.Drawing.Size(70, 22)
        Me.btnSearchLoadAll.Text = "Load All"
        '
        'btnSearchDocument
        '
        Me.btnSearchDocument.Image = CType(resources.GetObject("btnSearchDocument.Image"), System.Drawing.Image)
        Me.btnSearchDocument.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSearchDocument.Name = "btnSearchDocument"
        Me.btnSearchDocument.Size = New System.Drawing.Size(62, 22)
        Me.btnSearchDocument.Text = "Search"
        '
        'btnReminder1
        '
        Me.btnReminder1.Image = CType(resources.GetObject("btnReminder1.Image"), System.Drawing.Image)
        Me.btnReminder1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnReminder1.Name = "btnReminder1"
        Me.btnReminder1.Size = New System.Drawing.Size(78, 22)
        Me.btnReminder1.Text = "Reminder"
        '
        'Btn_SaveAttachment
        '
        Me.Btn_SaveAttachment.Image = CType(resources.GetObject("Btn_SaveAttachment.Image"), System.Drawing.Image)
        Me.Btn_SaveAttachment.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Btn_SaveAttachment.Name = "Btn_SaveAttachment"
        Me.Btn_SaveAttachment.Size = New System.Drawing.Size(117, 22)
        Me.Btn_SaveAttachment.Text = "Save Attachment"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'btnTasks
        '
        Me.btnTasks.Image = Global.SimpleAccounts.My.Resources.Resources.assign
        Me.btnTasks.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnTasks.Name = "btnTasks"
        Me.btnTasks.Size = New System.Drawing.Size(55, 22)
        Me.btnTasks.Text = "Tasks"
        '
        'btnGetAllRecord
        '
        Me.btnGetAllRecord.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnGetAllRecord.Name = "btnGetAllRecord"
        Me.btnGetAllRecord.Size = New System.Drawing.Size(86, 19)
        Me.btnGetAllRecord.Text = "Get All Record"
        Me.btnGetAllRecord.Visible = False
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(6, 25)
        '
        'HelpToolStripButton1
        '
        Me.HelpToolStripButton1.Image = CType(resources.GetObject("HelpToolStripButton1.Image"), System.Drawing.Image)
        Me.HelpToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton1.Name = "HelpToolStripButton1"
        Me.HelpToolStripButton1.Size = New System.Drawing.Size(52, 22)
        Me.HelpToolStripButton1.Text = "He&lp"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 25)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.grdVouchers)
        Me.SplitContainer1.Size = New System.Drawing.Size(854, 566)
        Me.SplitContainer1.SplitterDistance = 150
        Me.SplitContainer1.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Search)
        Me.GroupBox2.Controls.Add(Me.dtpSearchChequeDate)
        Me.GroupBox2.Controls.Add(Me.Label27)
        Me.GroupBox2.Controls.Add(Me.Label26)
        Me.GroupBox2.Controls.Add(Me.txtFromAmount)
        Me.GroupBox2.Controls.Add(Me.Label25)
        Me.GroupBox2.Controls.Add(Me.txtToAmount)
        Me.GroupBox2.Controls.Add(Me.Label24)
        Me.GroupBox2.Controls.Add(Me.txtSearchChequeNo)
        Me.GroupBox2.Controls.Add(Me.Label23)
        Me.GroupBox2.Controls.Add(Me.txtSearchComments)
        Me.GroupBox2.Controls.Add(Me.Label22)
        Me.GroupBox2.Controls.Add(Me.txtSearchVoucherNo)
        Me.GroupBox2.Controls.Add(Me.Label21)
        Me.GroupBox2.Controls.Add(Me.cmbSearchAccount)
        Me.GroupBox2.Controls.Add(Me.Label20)
        Me.GroupBox2.Controls.Add(Me.dtpFrom)
        Me.GroupBox2.Controls.Add(Me.Label19)
        Me.GroupBox2.Controls.Add(Me.dtpTo)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.cmbSearchVoucherType)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 7)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(804, 134)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Voucher Search"
        '
        'Search
        '
        Me.Search.Location = New System.Drawing.Point(543, 102)
        Me.Search.Name = "Search"
        Me.Search.Size = New System.Drawing.Size(75, 23)
        Me.Search.TabIndex = 20
        Me.Search.Text = "Search"
        Me.ToolTip1.SetToolTip(Me.Search, "Search")
        Me.Search.UseVisualStyleBackColor = True
        '
        'dtpSearchChequeDate
        '
        Me.dtpSearchChequeDate.Checked = False
        Me.dtpSearchChequeDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpSearchChequeDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpSearchChequeDate.Location = New System.Drawing.Point(318, 51)
        Me.dtpSearchChequeDate.Name = "dtpSearchChequeDate"
        Me.dtpSearchChequeDate.ShowCheckBox = True
        Me.dtpSearchChequeDate.Size = New System.Drawing.Size(120, 21)
        Me.dtpSearchChequeDate.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.dtpSearchChequeDate, "Cheque Date")
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(227, 55)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(82, 13)
        Me.Label27.TabIndex = 6
        Me.Label27.Text = "Cheque Date"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(227, 105)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(112, 13)
        Me.Label26.TabIndex = 18
        Me.Label26.Text = "Less Than Amount"
        '
        'txtFromAmount
        '
        Me.txtFromAmount.Location = New System.Drawing.Point(359, 77)
        Me.txtFromAmount.Name = "txtFromAmount"
        Me.txtFromAmount.Size = New System.Drawing.Size(79, 21)
        Me.txtFromAmount.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.txtFromAmount, "Larger Than Amount")
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(227, 80)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(124, 13)
        Me.Label25.TabIndex = 12
        Me.Label25.Text = "Larger Than Amount"
        '
        'txtToAmount
        '
        Me.txtToAmount.Location = New System.Drawing.Point(359, 102)
        Me.txtToAmount.Name = "txtToAmount"
        Me.txtToAmount.Size = New System.Drawing.Size(79, 21)
        Me.txtToAmount.TabIndex = 19
        Me.ToolTip1.SetToolTip(Me.txtToAmount, "Less Than Amount")
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(227, 27)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(70, 13)
        Me.Label24.TabIndex = 2
        Me.Label24.Text = "Cheque No"
        '
        'txtSearchChequeNo
        '
        Me.txtSearchChequeNo.Location = New System.Drawing.Point(318, 24)
        Me.txtSearchChequeNo.Name = "txtSearchChequeNo"
        Me.txtSearchChequeNo.Size = New System.Drawing.Size(120, 21)
        Me.txtSearchChequeNo.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtSearchChequeNo, "Cheque No")
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(457, 81)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(69, 13)
        Me.Label23.TabIndex = 14
        Me.Label23.Text = "Comments"
        '
        'txtSearchComments
        '
        Me.txtSearchComments.Location = New System.Drawing.Point(543, 78)
        Me.txtSearchComments.Name = "txtSearchComments"
        Me.txtSearchComments.Size = New System.Drawing.Size(220, 21)
        Me.txtSearchComments.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.txtSearchComments, "Comments")
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(11, 104)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(72, 13)
        Me.Label22.TabIndex = 16
        Me.Label22.Text = "Voucher No"
        '
        'txtSearchVoucherNo
        '
        Me.txtSearchVoucherNo.Location = New System.Drawing.Point(101, 101)
        Me.txtSearchVoucherNo.Name = "txtSearchVoucherNo"
        Me.txtSearchVoucherNo.Size = New System.Drawing.Size(120, 21)
        Me.txtSearchVoucherNo.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.txtSearchVoucherNo, "Voucher No")
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(457, 54)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(80, 13)
        Me.Label21.TabIndex = 8
        Me.Label21.Text = "Account Title"
        '
        'cmbSearchAccount
        '
        Appearance1.BackColor = System.Drawing.SystemColors.Info
        Me.cmbSearchAccount.Appearance = Appearance1
        Me.cmbSearchAccount.CheckedListSettings.CheckStateMember = ""
        Appearance2.BackColor = System.Drawing.Color.White
        Me.cmbSearchAccount.DisplayLayout.Appearance = Appearance2
        UltraGridColumn1.Header.VisiblePosition = 0
        UltraGridColumn1.Hidden = True
        UltraGridColumn2.Header.VisiblePosition = 1
        UltraGridColumn2.Width = 141
        UltraGridColumn3.Header.VisiblePosition = 2
        UltraGridColumn4.Header.VisiblePosition = 3
        UltraGridColumn5.Header.VisiblePosition = 4
        UltraGridColumn6.Header.VisiblePosition = 5
        UltraGridColumn6.Hidden = True
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn1, UltraGridColumn2, UltraGridColumn3, UltraGridColumn4, UltraGridColumn5, UltraGridColumn6})
        Me.cmbSearchAccount.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbSearchAccount.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbSearchAccount.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbSearchAccount.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbSearchAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Appearance3.BackColor = System.Drawing.Color.Transparent
        Me.cmbSearchAccount.DisplayLayout.Override.CardAreaAppearance = Appearance3
        Me.cmbSearchAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbSearchAccount.DisplayLayout.Override.CellPadding = 3
        Me.cmbSearchAccount.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance4.TextHAlignAsString = "Left"
        Me.cmbSearchAccount.DisplayLayout.Override.HeaderAppearance = Appearance4
        Me.cmbSearchAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance5.BorderColor = System.Drawing.Color.LightGray
        Appearance5.TextVAlignAsString = "Middle"
        Me.cmbSearchAccount.DisplayLayout.Override.RowAppearance = Appearance5
        Appearance6.BackColor = System.Drawing.Color.LightSteelBlue
        Appearance6.BorderColor = System.Drawing.Color.Black
        Appearance6.ForeColor = System.Drawing.Color.Black
        Me.cmbSearchAccount.DisplayLayout.Override.SelectedRowAppearance = Appearance6
        Me.cmbSearchAccount.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbSearchAccount.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbSearchAccount.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbSearchAccount.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.cmbSearchAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbSearchAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbSearchAccount.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbSearchAccount.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbSearchAccount.LimitToList = True
        Me.cmbSearchAccount.Location = New System.Drawing.Point(543, 50)
        Me.cmbSearchAccount.Name = "cmbSearchAccount"
        Me.cmbSearchAccount.Size = New System.Drawing.Size(220, 23)
        Me.cmbSearchAccount.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.cmbSearchAccount, "Select Any Account")
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(11, 77)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(84, 13)
        Me.Label20.TabIndex = 10
        Me.Label20.Text = "Voucher Type"
        '
        'dtpFrom
        '
        Me.dtpFrom.Checked = False
        Me.dtpFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(102, 20)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.ShowCheckBox = True
        Me.dtpFrom.Size = New System.Drawing.Size(120, 21)
        Me.dtpFrom.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.dtpFrom, "From Date")
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(11, 50)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(51, 13)
        Me.Label19.TabIndex = 4
        Me.Label19.Text = "Date To"
        '
        'dtpTo
        '
        Me.dtpTo.Checked = False
        Me.dtpTo.CustomFormat = "dd/MMM/yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(101, 46)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.ShowCheckBox = True
        Me.dtpTo.Size = New System.Drawing.Size(120, 21)
        Me.dtpTo.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.dtpTo, "To Date")
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(11, 24)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(67, 13)
        Me.Label18.TabIndex = 0
        Me.Label18.Text = "Date From"
        '
        'cmbSearchVoucherType
        '
        Me.cmbSearchVoucherType.FormattingEnabled = True
        Me.cmbSearchVoucherType.Location = New System.Drawing.Point(101, 74)
        Me.cmbSearchVoucherType.Name = "cmbSearchVoucherType"
        Me.cmbSearchVoucherType.Size = New System.Drawing.Size(121, 21)
        Me.cmbSearchVoucherType.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.cmbSearchVoucherType, "Select Any Voucher Type")
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(1046, 612)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 0
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Expenses"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "History"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1044, 591)
        '
        'BackgroundWorker1
        '
        '
        'BackgroundWorker2
        '
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        Me.OpenFileDialog1.Multiselect = True
        Me.OpenFileDialog1.ShowHelp = True
        Me.OpenFileDialog1.SupportMultiDottedExtensions = True
        Me.OpenFileDialog1.Title = "Images"
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(868, 0)
        Me.CtrlGrdBar1.MyGrid = Me.grd
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(33, 25)
        Me.CtrlGrdBar1.TabIndex = 1
        Me.CtrlGrdBar1.TabStop = False
        '
        'btnReplace
        '
        Me.btnReplace.Location = New System.Drawing.Point(654, 300)
        Me.btnReplace.Name = "btnReplace"
        Me.btnReplace.Size = New System.Drawing.Size(68, 23)
        Me.btnReplace.TabIndex = 38
        Me.btnReplace.Text = "Replace"
        Me.btnReplace.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(593, 300)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(55, 23)
        Me.btnCancel.TabIndex = 38
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'frmExpense
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Menu
        Me.ClientSize = New System.Drawing.Size(1046, 612)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "frmExpense"
        Me.Text = "Expense"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.txtMemo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.txtReference, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.cmbAccounts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdVouchers, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.cmbSearchAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cmbCashAccount As System.Windows.Forms.ComboBox
    Friend WithEvents cmbVoucherType As System.Windows.Forms.ComboBox
    Friend WithEvents txtCustomerBalance As System.Windows.Forms.TextBox
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents dtVoucherDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtVoucherNo As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents BtnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtChequeNo As System.Windows.Forms.TextBox
    Friend WithEvents dtChequeDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents cmbAccounts As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents ToolStripSplitButton1 As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents ExpensesDetailToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents grdVouchers As Janus.Windows.GridEX.GridEX
    Friend WithEvents BtnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtCashBalances As System.Windows.Forms.TextBox
    Friend WithEvents lblCashBalance As System.Windows.Forms.Label
    Friend WithEvents chkPost As System.Windows.Forms.CheckBox
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Search As System.Windows.Forms.Button
    Friend WithEvents dtpSearchChequeDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtFromAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtToAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtSearchChequeNo As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtSearchComments As System.Windows.Forms.TextBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtSearchVoucherNo As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents cmbSearchAccount As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents cmbSearchVoucherType As System.Windows.Forms.ComboBox
    Friend WithEvents ToolStrip2 As System.Windows.Forms.ToolStrip
    Friend WithEvents CtrlGrdBar2 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents btnSearchEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSearchDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSearchLoadAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSearchDocument As System.Windows.Forms.ToolStripButton
    Friend WithEvents HelpToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundWorker2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblPrintStatus As System.Windows.Forms.Label
    Friend WithEvents txtReference As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents txtMemo As Infragistics.Win.UltraWinEditors.UltraTextEditor
    Friend WithEvents cmbEmployee As System.Windows.Forms.ComboBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents lblCostCenter As System.Windows.Forms.Label
    Friend WithEvents cmbCostCenter As System.Windows.Forms.ComboBox
    Friend WithEvents btnReminder1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnReminder As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BtnPrint As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents PrintPaymentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnSearchPrint As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents PrintPaymentToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintSelectedVouchersToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents cmbCMFA As System.Windows.Forms.ComboBox
    Friend WithEvents chkChecked As System.Windows.Forms.CheckBox
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnAttachment As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents PrintVoucherAttachmentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintAttachmentVoucherToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents chkEnableDepositAc As System.Windows.Forms.CheckBox
    Friend WithEvents btnSMSTemplate As System.Windows.Forms.ToolStripButton
    Friend WithEvents Btn_SaveAttachment As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbCompany As System.Windows.Forms.ComboBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents btnUpdateTimes As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents btnUpdatedVoucher As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PrintUpdatedVoucherToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnTasks As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbConfig As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblCurrency As System.Windows.Forms.Label
    Friend WithEvents lblCurrencyRate As System.Windows.Forms.Label
    Friend WithEvents txtCurrencyRate As System.Windows.Forms.TextBox
    Friend WithEvents cmbCurrency As System.Windows.Forms.ComboBox
    Friend WithEvents btnGetAllRecord As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents cmbChequeBook As System.Windows.Forms.ComboBox
    Friend WithEvents lblPostedBy As System.Windows.Forms.Label
    Friend WithEvents lblCheckedBy As System.Windows.Forms.Label
    Friend WithEvents btnApprovalHistory As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblAmountNumberConvertor As System.Windows.Forms.Label
    Friend WithEvents lblCashBlncNumberConvertor As System.Windows.Forms.Label
    Friend WithEvents lblBalanceNumberConvertor As System.Windows.Forms.Label
    Friend WithEvents CtrlGrdBar3 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnReplace As System.Windows.Forms.Button
End Class

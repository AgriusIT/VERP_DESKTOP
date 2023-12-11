<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReconciliation
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
        Dim grd_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReconciliation))
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblStatementId = New System.Windows.Forms.Label()
        Me.cmbLoadRecords = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtReconciledBalance = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lnkOpenLedger = New System.Windows.Forms.LinkLabel()
        Me.txtEndingBalance = New System.Windows.Forms.TextBox()
        Me.txtStatementTitle = New System.Windows.Forms.TextBox()
        Me.btnProcess = New System.Windows.Forms.Button()
        Me.dtpStatementDate = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbBankAccount = New System.Windows.Forms.ComboBox()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
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
        Me.Label31 = New System.Windows.Forms.Label()
        Me.txtSearchVoucherNo = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.cmbSearchAccount = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.cmbSearchVoucherType = New System.Windows.Forms.ComboBox()
        Me.grdSaved = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnUnReconcile = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.btnSearch = New System.Windows.Forms.ToolStripButton()
        Me.btnOpenScreen = New System.Windows.Forms.ToolStripSplitButton()
        Me.OpenLedgerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VoucherEntryToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PaymentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReceiptToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExpenseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.BackToOld = New System.Windows.Forms.ToolStripButton()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.cmbSearchAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.lblProgress)
        Me.UltraTabPageControl1.Controls.Add(Me.grd)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox1)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(973, 407)
        '
        'pnlHeader
        '
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(973, 38)
        Me.pnlHeader.TabIndex = 23
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(11, 5)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(223, 26)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Bank Reconciliation"
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(330, 216)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 2
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'grd
        '
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grd_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grd.DesignTimeLayout = grd_DesignTimeLayout
        Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grd.FrozenColumns = 2
        Me.grd.GroupByBoxVisible = False
        Me.grd.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.grd.Location = New System.Drawing.Point(-1, 147)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(975, 260)
        Me.grd.TabIndex = 3
        Me.grd.TabStop = False
        Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.lblStatementId)
        Me.GroupBox1.Controls.Add(Me.cmbLoadRecords)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtReconciledBalance)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.lnkOpenLedger)
        Me.GroupBox1.Controls.Add(Me.txtEndingBalance)
        Me.GroupBox1.Controls.Add(Me.txtStatementTitle)
        Me.GroupBox1.Controls.Add(Me.btnProcess)
        Me.GroupBox1.Controls.Add(Me.dtpStatementDate)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.cmbBankAccount)
        Me.GroupBox1.Location = New System.Drawing.Point(11, 39)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(951, 102)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'lblStatementId
        '
        Me.lblStatementId.AutoSize = True
        Me.lblStatementId.Location = New System.Drawing.Point(504, 15)
        Me.lblStatementId.Name = "lblStatementId"
        Me.lblStatementId.Size = New System.Drawing.Size(13, 13)
        Me.lblStatementId.TabIndex = 5
        Me.lblStatementId.Text = "0"
        Me.lblStatementId.Visible = False
        '
        'cmbLoadRecords
        '
        Me.cmbLoadRecords.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLoadRecords.FormattingEnabled = True
        Me.cmbLoadRecords.Items.AddRange(New Object() {"Reconciled", "Un Reconciled"})
        Me.cmbLoadRecords.Location = New System.Drawing.Point(281, 72)
        Me.cmbLoadRecords.Name = "cmbLoadRecords"
        Me.cmbLoadRecords.Size = New System.Drawing.Size(239, 21)
        Me.cmbLoadRecords.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.cmbLoadRecords, "Load Records")
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(278, 55)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(74, 13)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Load Records"
        '
        'txtReconciledBalance
        '
        Me.txtReconciledBalance.Enabled = False
        Me.txtReconciledBalance.Location = New System.Drawing.Point(741, 32)
        Me.txtReconciledBalance.Name = "txtReconciledBalance"
        Me.txtReconciledBalance.Size = New System.Drawing.Size(100, 20)
        Me.txtReconciledBalance.TabIndex = 11
        Me.txtReconciledBalance.Text = "0"
        Me.txtReconciledBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtReconciledBalance, "Reconciled Balance")
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(738, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(103, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Reconciled Balance"
        '
        'lnkOpenLedger
        '
        Me.lnkOpenLedger.AutoSize = True
        Me.lnkOpenLedger.Location = New System.Drawing.Point(88, 15)
        Me.lnkOpenLedger.Name = "lnkOpenLedger"
        Me.lnkOpenLedger.Size = New System.Drawing.Size(81, 13)
        Me.lnkOpenLedger.TabIndex = 1
        Me.lnkOpenLedger.TabStop = True
        Me.lnkOpenLedger.Text = "( Open Ledger )"
        Me.lnkOpenLedger.Visible = False
        '
        'txtEndingBalance
        '
        Me.txtEndingBalance.Location = New System.Drawing.Point(635, 32)
        Me.txtEndingBalance.Name = "txtEndingBalance"
        Me.txtEndingBalance.Size = New System.Drawing.Size(100, 20)
        Me.txtEndingBalance.TabIndex = 9
        Me.txtEndingBalance.Text = "0"
        Me.txtEndingBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtEndingBalance, "Ending Balance")
        '
        'txtStatementTitle
        '
        Me.txtStatementTitle.Location = New System.Drawing.Point(278, 32)
        Me.txtStatementTitle.Name = "txtStatementTitle"
        Me.txtStatementTitle.Size = New System.Drawing.Size(242, 20)
        Me.txtStatementTitle.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.txtStatementTitle, "Reconciliation Statement Title")
        '
        'btnProcess
        '
        Me.btnProcess.Location = New System.Drawing.Point(847, 30)
        Me.btnProcess.Name = "btnProcess"
        Me.btnProcess.Size = New System.Drawing.Size(97, 23)
        Me.btnProcess.TabIndex = 12
        Me.btnProcess.Text = "Process"
        Me.ToolTip1.SetToolTip(Me.btnProcess, "Press to process Reconciliation")
        Me.btnProcess.UseVisualStyleBackColor = True
        '
        'dtpStatementDate
        '
        Me.dtpStatementDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpStatementDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStatementDate.Location = New System.Drawing.Point(526, 32)
        Me.dtpStatementDate.Name = "dtpStatementDate"
        Me.dtpStatementDate.Size = New System.Drawing.Size(103, 20)
        Me.dtpStatementDate.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.dtpStatementDate, "Reconciliation Statement Date")
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(632, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(82, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Ending Balance"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(275, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(78, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Statement Title"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(523, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Statement Date"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Bank Account"
        '
        'cmbBankAccount
        '
        Me.cmbBankAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBankAccount.FormattingEnabled = True
        Me.cmbBankAccount.Items.AddRange(New Object() {"JV", "CPV", "BPV", "CRV", "BRV", "SV", "PV"})
        Me.cmbBankAccount.Location = New System.Drawing.Point(9, 32)
        Me.cmbBankAccount.Name = "cmbBankAccount"
        Me.cmbBankAccount.Size = New System.Drawing.Size(263, 21)
        Me.cmbBankAccount.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.cmbBankAccount, "Please select the Bank Name")
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.SplitContainer1)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(973, 407)
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox3)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.grdSaved)
        Me.SplitContainer1.Size = New System.Drawing.Size(973, 407)
        Me.SplitContainer1.SplitterDistance = 150
        Me.SplitContainer1.TabIndex = 1
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Search)
        Me.GroupBox3.Controls.Add(Me.dtpSearchChequeDate)
        Me.GroupBox3.Controls.Add(Me.Label27)
        Me.GroupBox3.Controls.Add(Me.Label26)
        Me.GroupBox3.Controls.Add(Me.txtFromAmount)
        Me.GroupBox3.Controls.Add(Me.Label25)
        Me.GroupBox3.Controls.Add(Me.txtToAmount)
        Me.GroupBox3.Controls.Add(Me.Label24)
        Me.GroupBox3.Controls.Add(Me.txtSearchChequeNo)
        Me.GroupBox3.Controls.Add(Me.Label23)
        Me.GroupBox3.Controls.Add(Me.txtSearchComments)
        Me.GroupBox3.Controls.Add(Me.Label31)
        Me.GroupBox3.Controls.Add(Me.txtSearchVoucherNo)
        Me.GroupBox3.Controls.Add(Me.Label32)
        Me.GroupBox3.Controls.Add(Me.cmbSearchAccount)
        Me.GroupBox3.Controls.Add(Me.Label33)
        Me.GroupBox3.Controls.Add(Me.dtpFrom)
        Me.GroupBox3.Controls.Add(Me.Label34)
        Me.GroupBox3.Controls.Add(Me.dtpTo)
        Me.GroupBox3.Controls.Add(Me.Label35)
        Me.GroupBox3.Controls.Add(Me.cmbSearchVoucherType)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 7)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(804, 134)
        Me.GroupBox3.TabIndex = 0
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Voucher Search"
        '
        'Search
        '
        Me.Search.Location = New System.Drawing.Point(543, 102)
        Me.Search.Name = "Search"
        Me.Search.Size = New System.Drawing.Size(75, 23)
        Me.Search.TabIndex = 20
        Me.Search.Text = "Search"
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
        Me.dtpSearchChequeDate.Size = New System.Drawing.Size(120, 20)
        Me.dtpSearchChequeDate.TabIndex = 7
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(227, 55)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(70, 13)
        Me.Label27.TabIndex = 6
        Me.Label27.Text = "Cheque Date"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(227, 105)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(96, 13)
        Me.Label26.TabIndex = 18
        Me.Label26.Text = "Less Than Amount"
        '
        'txtFromAmount
        '
        Me.txtFromAmount.Location = New System.Drawing.Point(359, 77)
        Me.txtFromAmount.Name = "txtFromAmount"
        Me.txtFromAmount.Size = New System.Drawing.Size(79, 20)
        Me.txtFromAmount.TabIndex = 13
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(227, 80)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(104, 13)
        Me.Label25.TabIndex = 12
        Me.Label25.Text = "Larger Than Amount"
        '
        'txtToAmount
        '
        Me.txtToAmount.Location = New System.Drawing.Point(359, 102)
        Me.txtToAmount.Name = "txtToAmount"
        Me.txtToAmount.Size = New System.Drawing.Size(79, 20)
        Me.txtToAmount.TabIndex = 19
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(227, 27)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(61, 13)
        Me.Label24.TabIndex = 2
        Me.Label24.Text = "Cheque No"
        '
        'txtSearchChequeNo
        '
        Me.txtSearchChequeNo.Location = New System.Drawing.Point(318, 24)
        Me.txtSearchChequeNo.Name = "txtSearchChequeNo"
        Me.txtSearchChequeNo.Size = New System.Drawing.Size(120, 20)
        Me.txtSearchChequeNo.TabIndex = 3
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(457, 81)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(56, 13)
        Me.Label23.TabIndex = 14
        Me.Label23.Text = "Comments"
        '
        'txtSearchComments
        '
        Me.txtSearchComments.Location = New System.Drawing.Point(543, 78)
        Me.txtSearchComments.Name = "txtSearchComments"
        Me.txtSearchComments.Size = New System.Drawing.Size(220, 20)
        Me.txtSearchComments.TabIndex = 15
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(11, 104)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(64, 13)
        Me.Label31.TabIndex = 16
        Me.Label31.Text = "Voucher No"
        '
        'txtSearchVoucherNo
        '
        Me.txtSearchVoucherNo.Location = New System.Drawing.Point(101, 101)
        Me.txtSearchVoucherNo.Name = "txtSearchVoucherNo"
        Me.txtSearchVoucherNo.Size = New System.Drawing.Size(120, 20)
        Me.txtSearchVoucherNo.TabIndex = 17
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(457, 54)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(70, 13)
        Me.Label32.TabIndex = 8
        Me.Label32.Text = "Account Title"
        '
        'cmbSearchAccount
        '
        Appearance1.BackColor = System.Drawing.SystemColors.Info
        Me.cmbSearchAccount.Appearance = Appearance1
        Me.cmbSearchAccount.CheckedListSettings.CheckStateMember = ""
        Appearance2.BackColor = System.Drawing.Color.White
        Me.cmbSearchAccount.DisplayLayout.Appearance = Appearance2
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
        Me.cmbSearchAccount.Size = New System.Drawing.Size(220, 22)
        Me.cmbSearchAccount.TabIndex = 9
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(11, 77)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(74, 13)
        Me.Label33.TabIndex = 10
        Me.Label33.Text = "Voucher Type"
        '
        'dtpFrom
        '
        Me.dtpFrom.Checked = False
        Me.dtpFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(102, 20)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.ShowCheckBox = True
        Me.dtpFrom.Size = New System.Drawing.Size(120, 20)
        Me.dtpFrom.TabIndex = 1
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(11, 50)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(46, 13)
        Me.Label34.TabIndex = 4
        Me.Label34.Text = "Date To"
        '
        'dtpTo
        '
        Me.dtpTo.Checked = False
        Me.dtpTo.CustomFormat = "dd/MMM/yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(101, 46)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.ShowCheckBox = True
        Me.dtpTo.Size = New System.Drawing.Size(120, 20)
        Me.dtpTo.TabIndex = 5
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(11, 24)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(56, 13)
        Me.Label35.TabIndex = 0
        Me.Label35.Text = "Date From"
        '
        'cmbSearchVoucherType
        '
        Me.cmbSearchVoucherType.FormattingEnabled = True
        Me.cmbSearchVoucherType.Location = New System.Drawing.Point(101, 74)
        Me.cmbSearchVoucherType.Name = "cmbSearchVoucherType"
        Me.cmbSearchVoucherType.Size = New System.Drawing.Size(121, 21)
        Me.cmbSearchVoucherType.TabIndex = 11
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
        Me.grdSaved.Size = New System.Drawing.Size(973, 253)
        Me.grdSaved.TabIndex = 0
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'ToolStrip1
        '
        Me.ToolStrip1.BackColor = System.Drawing.SystemColors.Control
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnSave, Me.btnUnReconcile, Me.ToolStripSeparator1, Me.btnRefresh, Me.btnDelete, Me.btnEdit, Me.btnPrint, Me.btnSearch, Me.btnOpenScreen, Me.BackToOld})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(975, 25)
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
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(78, 22)
        Me.btnSave.Text = "Reconcile"
        '
        'btnUnReconcile
        '
        Me.btnUnReconcile.Image = CType(resources.GetObject("btnUnReconcile.Image"), System.Drawing.Image)
        Me.btnUnReconcile.Name = "btnUnReconcile"
        Me.btnUnReconcile.RightToLeftAutoMirrorImage = True
        Me.btnUnReconcile.Size = New System.Drawing.Size(93, 22)
        Me.btnUnReconcile.Text = "UnReconcile"
        Me.btnUnReconcile.Visible = False
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(66, 22)
        Me.btnRefresh.Text = "Refresh"
        '
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.RightToLeftAutoMirrorImage = True
        Me.btnDelete.Size = New System.Drawing.Size(60, 22)
        Me.btnDelete.Text = "&Delete"
        Me.btnDelete.Visible = False
        '
        'btnEdit
        '
        Me.btnEdit.Image = CType(resources.GetObject("btnEdit.Image"), System.Drawing.Image)
        Me.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(47, 22)
        Me.btnEdit.Text = "&Edit"
        Me.btnEdit.Visible = False
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
        'btnSearch
        '
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(62, 22)
        Me.btnSearch.Text = "Search"
        Me.btnSearch.Visible = False
        '
        'btnOpenScreen
        '
        Me.btnOpenScreen.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OpenLedgerToolStripMenuItem, Me.VoucherEntryToolStripMenuItem, Me.PaymentToolStripMenuItem, Me.ReceiptToolStripMenuItem, Me.ExpenseToolStripMenuItem})
        Me.btnOpenScreen.Image = Global.SimpleAccounts.My.Resources.Resources.add_green
        Me.btnOpenScreen.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnOpenScreen.Name = "btnOpenScreen"
        Me.btnOpenScreen.Size = New System.Drawing.Size(111, 22)
        Me.btnOpenScreen.Text = "Open Screens"
        '
        'OpenLedgerToolStripMenuItem
        '
        Me.OpenLedgerToolStripMenuItem.Name = "OpenLedgerToolStripMenuItem"
        Me.OpenLedgerToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.OpenLedgerToolStripMenuItem.Text = "Ledger"
        '
        'VoucherEntryToolStripMenuItem
        '
        Me.VoucherEntryToolStripMenuItem.Name = "VoucherEntryToolStripMenuItem"
        Me.VoucherEntryToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.VoucherEntryToolStripMenuItem.Text = "Voucher Entry"
        '
        'PaymentToolStripMenuItem
        '
        Me.PaymentToolStripMenuItem.Name = "PaymentToolStripMenuItem"
        Me.PaymentToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.PaymentToolStripMenuItem.Text = "Payment"
        '
        'ReceiptToolStripMenuItem
        '
        Me.ReceiptToolStripMenuItem.Name = "ReceiptToolStripMenuItem"
        Me.ReceiptToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.ReceiptToolStripMenuItem.Text = "Receipt"
        '
        'ExpenseToolStripMenuItem
        '
        Me.ExpenseToolStripMenuItem.Name = "ExpenseToolStripMenuItem"
        Me.ExpenseToolStripMenuItem.Size = New System.Drawing.Size(148, 22)
        Me.ExpenseToolStripMenuItem.Text = "Expense"
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
        Me.UltraTabControl1.Size = New System.Drawing.Size(975, 428)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 1
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Reconciliation"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "History"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(973, 407)
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(936, 0)
        Me.CtrlGrdBar1.MyGrid = Me.grd
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(38, 25)
        Me.CtrlGrdBar1.TabIndex = 2
        '
        'BackToOld
        '
        Me.BackToOld.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.BackToOld.Image = CType(resources.GetObject("BackToOld.Image"), System.Drawing.Image)
        Me.BackToOld.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BackToOld.Name = "BackToOld"
        Me.BackToOld.Size = New System.Drawing.Size(75, 22)
        Me.BackToOld.Text = "Back To Old"
        '
        'frmReconciliation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(975, 453)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "frmReconciliation"
        Me.Text = "Bank Reconciliation"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.cmbSearchAccount, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnProcess As System.Windows.Forms.Button
    Friend WithEvents dtpStatementDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbBankAccount As System.Windows.Forms.ComboBox
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents txtEndingBalance As System.Windows.Forms.TextBox
    Friend WithEvents txtStatementTitle As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
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
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents txtSearchVoucherNo As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents cmbSearchAccount As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents cmbSearchVoucherType As System.Windows.Forms.ComboBox
    Friend WithEvents grdSaved As Janus.Windows.GridEX.GridEX
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lnkOpenLedger As System.Windows.Forms.LinkLabel
    Friend WithEvents txtReconciledBalance As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnOpenScreen As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents VoucherEntryToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PaymentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReceiptToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExpenseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents OpenLedgerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmbLoadRecords As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnUnReconcile As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblStatementId As System.Windows.Forms.Label
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents BackToOld As System.Windows.Forms.ToolStripButton
End Class

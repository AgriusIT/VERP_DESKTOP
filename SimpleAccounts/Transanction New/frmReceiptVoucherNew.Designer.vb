<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReceiptVoucherNew
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
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn6 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("detail_id")
        Dim UltraGridColumn7 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("detail_title")
        Dim UltraGridColumn8 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("cityname")
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand2 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn1 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("SalesID")
        Dim UltraGridColumn2 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("SalesNo", 0)
        Dim UltraGridColumn3 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("SalesDate", 1)
        Dim UltraGridColumn4 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("InvoiceAmount", 2)
        Dim UltraGridColumn9 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Adjustment", 3)
        Dim UltraGridColumn10 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Credit Amount", 4)
        Dim UltraGridColumn11 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("InvoiceBalance", 5)
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim GrdReceiptDetail_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lblCostCenter = New System.Windows.Forms.Label()
        Me.cmbCompany = New System.Windows.Forms.ComboBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.DtpChequeDate = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.lblChequeDate = New System.Windows.Forms.Label()
        Me.lblChequeNo = New System.Windows.Forms.Label()
        Me.txtChequeNo = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbPaymentMethod = New System.Windows.Forms.ComboBox()
        Me.cmbPayFrom = New System.Windows.Forms.ComboBox()
        Me.cmbCostCenter = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtCurrentBalance = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.ChkPost = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cmbAccounts = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.DtpFillDate = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtReference = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpDate = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.txtVoucherNo = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtTaxAmount = New System.Windows.Forms.TextBox()
        Me.BtnAdd = New System.Windows.Forms.Button()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtWHTax = New System.Windows.Forms.TextBox()
        Me.txtInvoicoAmount = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtReceivedAmt = New System.Windows.Forms.TextBox()
        Me.txtNetAmount = New System.Windows.Forms.TextBox()
        Me.txtDueAmount = New System.Windows.Forms.TextBox()
        Me.cmbInvoiceList = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtOtherTaxAmount = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtSalesTaxAmount = New System.Windows.Forms.TextBox()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.lblPrintStatus = New System.Windows.Forms.Label()
        Me.txtReceiptId = New System.Windows.Forms.TextBox()
        Me.GrdReceiptDetail = New Janus.Windows.GridEX.GridEX()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.GrdSaved = New Janus.Windows.GridEX.GridEX()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.CtrlGrdBar2 = New SimpleAccounts.CtrlGrdBar()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.BtnNew = New System.Windows.Forms.ToolStripButton()
        Me.BtnEdit = New System.Windows.Forms.ToolStripButton()
        Me.BtnSave = New System.Windows.Forms.ToolStripButton()
        Me.BtnPrint = New System.Windows.Forms.ToolStripSplitButton()
        Me.PrintVoucherToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.BtnDelete = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.BtnLoadAll = New System.Windows.Forms.ToolStripButton()
        Me.BtnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnSMSTemplate = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnUpdateTimes = New System.Windows.Forms.ToolStripSplitButton()
        Me.tsbTask = New System.Windows.Forms.ToolStripButton()
        Me.tsbConfig = New System.Windows.Forms.ToolStripButton()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.UltraTabPageControl1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.cmbAccounts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.cmbInvoiceList, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GrdReceiptDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.GrdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.Panel1)
        Me.UltraTabPageControl1.Controls.Add(Me.lblProgress)
        Me.UltraTabPageControl1.Controls.Add(Me.lblPrintStatus)
        Me.UltraTabPageControl1.Controls.Add(Me.txtReceiptId)
        Me.UltraTabPageControl1.Controls.Add(Me.GrdReceiptDetail)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1550, 582)
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.Label18)
        Me.Panel1.Controls.Add(Me.lblCostCenter)
        Me.Panel1.Controls.Add(Me.cmbCompany)
        Me.Panel1.Controls.Add(Me.Label20)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.cmbCostCenter)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.txtAmount)
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Controls.Add(Me.Label19)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.txtTaxAmount)
        Me.Panel1.Controls.Add(Me.BtnAdd)
        Me.Panel1.Controls.Add(Me.Label17)
        Me.Panel1.Controls.Add(Me.txtRemarks)
        Me.Panel1.Controls.Add(Me.txtWHTax)
        Me.Panel1.Controls.Add(Me.txtInvoicoAmount)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.txtReceivedAmt)
        Me.Panel1.Controls.Add(Me.txtNetAmount)
        Me.Panel1.Controls.Add(Me.txtDueAmount)
        Me.Panel1.Controls.Add(Me.cmbInvoiceList)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Label14)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.txtOtherTaxAmount)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label13)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.txtSalesTaxAmount)
        Me.Panel1.Location = New System.Drawing.Point(0, 39)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1550, 326)
        Me.Panel1.TabIndex = 35
        '
        'Label18
        '
        Me.Label18.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(4, 9)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(74, 17)
        Me.Label18.TabIndex = 4
        Me.Label18.Text = "Company"
        '
        'lblCostCenter
        '
        Me.lblCostCenter.AutoSize = True
        Me.lblCostCenter.Location = New System.Drawing.Point(363, 272)
        Me.lblCostCenter.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCostCenter.Name = "lblCostCenter"
        Me.lblCostCenter.Size = New System.Drawing.Size(82, 17)
        Me.lblCostCenter.TabIndex = 10
        Me.lblCostCenter.Text = "Cost Center"
        '
        'cmbCompany
        '
        Me.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCompany.FormattingEnabled = True
        Me.cmbCompany.Location = New System.Drawing.Point(95, 5)
        Me.cmbCompany.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbCompany.Name = "cmbCompany"
        Me.cmbCompany.Size = New System.Drawing.Size(236, 24)
        Me.cmbCompany.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.cmbCompany, "Select Cost Center")
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(1368, 272)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(64, 17)
        Me.Label20.TabIndex = 31
        Me.Label20.Text = "Amount"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.DtpChequeDate)
        Me.GroupBox2.Controls.Add(Me.lblChequeDate)
        Me.GroupBox2.Controls.Add(Me.lblChequeNo)
        Me.GroupBox2.Controls.Add(Me.txtChequeNo)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.cmbPaymentMethod)
        Me.GroupBox2.Controls.Add(Me.cmbPayFrom)
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Location = New System.Drawing.Point(8, 36)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Size = New System.Drawing.Size(327, 233)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Method"
        '
        'DtpChequeDate
        '
        Me.DtpChequeDate.Checked = False
        Me.DtpChequeDate.CustomFormat = "dd/MMM/yyyy"
        Me.DtpChequeDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.DtpChequeDate.DropDownCalendar.Name = ""
        Me.DtpChequeDate.DropDownCalendar.Visible = False
        Me.DtpChequeDate.Location = New System.Drawing.Point(15, 192)
        Me.DtpChequeDate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DtpChequeDate.Name = "DtpChequeDate"
        Me.DtpChequeDate.Size = New System.Drawing.Size(137, 22)
        Me.DtpChequeDate.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.DtpChequeDate, "Cheque Date")
        '
        'lblChequeDate
        '
        Me.lblChequeDate.AutoSize = True
        Me.lblChequeDate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChequeDate.Location = New System.Drawing.Point(13, 172)
        Me.lblChequeDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblChequeDate.Name = "lblChequeDate"
        Me.lblChequeDate.Size = New System.Drawing.Size(99, 17)
        Me.lblChequeDate.TabIndex = 6
        Me.lblChequeDate.Text = "Cheque Date"
        '
        'lblChequeNo
        '
        Me.lblChequeNo.AutoSize = True
        Me.lblChequeNo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChequeNo.Location = New System.Drawing.Point(13, 128)
        Me.lblChequeNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblChequeNo.Name = "lblChequeNo"
        Me.lblChequeNo.Size = New System.Drawing.Size(90, 17)
        Me.lblChequeNo.TabIndex = 4
        Me.lblChequeNo.Text = "Cheque No."
        '
        'txtChequeNo
        '
        Me.txtChequeNo.Location = New System.Drawing.Point(15, 146)
        Me.txtChequeNo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtChequeNo.Name = "txtChequeNo"
        Me.txtChequeNo.Size = New System.Drawing.Size(303, 22)
        Me.txtChequeNo.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.txtChequeNo, "Cheque No")
        '
        'Label12
        '
        Me.Label12.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(13, 23)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(116, 17)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Receipt Method"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(13, 78)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 17)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Deposit in"
        '
        'cmbPaymentMethod
        '
        Me.cmbPaymentMethod.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbPaymentMethod.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbPaymentMethod.FormattingEnabled = True
        Me.cmbPaymentMethod.Location = New System.Drawing.Point(15, 46)
        Me.cmbPaymentMethod.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbPaymentMethod.Name = "cmbPaymentMethod"
        Me.cmbPaymentMethod.Size = New System.Drawing.Size(303, 24)
        Me.cmbPaymentMethod.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbPaymentMethod, "Select Receipt Method")
        '
        'cmbPayFrom
        '
        Me.cmbPayFrom.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbPayFrom.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbPayFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPayFrom.FormattingEnabled = True
        Me.cmbPayFrom.Location = New System.Drawing.Point(15, 97)
        Me.cmbPayFrom.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbPayFrom.Name = "cmbPayFrom"
        Me.cmbPayFrom.Size = New System.Drawing.Size(303, 24)
        Me.cmbPayFrom.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.cmbPayFrom, "Select Deposit Account")
        '
        'cmbCostCenter
        '
        Me.cmbCostCenter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbCostCenter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCostCenter.FormattingEnabled = True
        Me.cmbCostCenter.Location = New System.Drawing.Point(365, 292)
        Me.cmbCostCenter.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbCostCenter.Name = "cmbCostCenter"
        Me.cmbCostCenter.Size = New System.Drawing.Size(229, 24)
        Me.cmbCostCenter.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.cmbCostCenter, "Select Cost Center")
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtCurrentBalance)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.ChkPost)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.cmbAccounts)
        Me.GroupBox1.Controls.Add(Me.DtpFillDate)
        Me.GroupBox1.Location = New System.Drawing.Point(343, 36)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(311, 233)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Invoice"
        '
        'txtCurrentBalance
        '
        Me.txtCurrentBalance.Location = New System.Drawing.Point(148, 97)
        Me.txtCurrentBalance.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtCurrentBalance.Name = "txtCurrentBalance"
        Me.txtCurrentBalance.ReadOnly = True
        Me.txtCurrentBalance.Size = New System.Drawing.Size(145, 22)
        Me.txtCurrentBalance.TabIndex = 3
        '
        'Label15
        '
        Me.Label15.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(15, 101)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(120, 17)
        Me.Label15.TabIndex = 2
        Me.Label15.Text = "Current Balance"
        '
        'ChkPost
        '
        Me.ChkPost.AutoSize = True
        Me.ChkPost.Location = New System.Drawing.Point(147, 198)
        Me.ChkPost.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ChkPost.Name = "ChkPost"
        Me.ChkPost.Size = New System.Drawing.Size(74, 21)
        Me.ChkPost.TabIndex = 6
        Me.ChkPost.Text = "Posted"
        Me.ChkPost.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 150)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(120, 17)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Due on / before"
        '
        'Label11
        '
        Me.Label11.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(15, 25)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(110, 17)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Received From"
        '
        'cmbAccounts
        '
        Me.cmbAccounts.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.cmbAccounts.CheckedListSettings.CheckStateMember = ""
        Appearance7.BackColor = System.Drawing.Color.White
        Appearance7.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        Me.cmbAccounts.DisplayLayout.Appearance = Appearance7
        UltraGridColumn6.Header.VisiblePosition = 0
        UltraGridColumn6.Hidden = True
        UltraGridColumn7.Header.Caption = "Customer Name"
        UltraGridColumn7.Header.VisiblePosition = 1
        UltraGridColumn8.Header.Caption = "City"
        UltraGridColumn8.Header.VisiblePosition = 2
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn6, UltraGridColumn7, UltraGridColumn8})
        Me.cmbAccounts.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbAccounts.DisplayLayout.InterBandSpacing = 10
        Me.cmbAccounts.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbAccounts.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbAccounts.DisplayLayout.Override.AllowGroupBy = Infragistics.Win.DefaultableBoolean.[True]
        Me.cmbAccounts.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Appearance8.BackColor = System.Drawing.Color.Transparent
        Me.cmbAccounts.DisplayLayout.Override.CardAreaAppearance = Appearance8
        Me.cmbAccounts.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Appearance9.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance9.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance9.ForeColor = System.Drawing.Color.White
        Appearance9.TextHAlignAsString = "Left"
        Appearance9.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent
        Me.cmbAccounts.DisplayLayout.Override.HeaderAppearance = Appearance9
        Me.cmbAccounts.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance10.BorderColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbAccounts.DisplayLayout.Override.RowAppearance = Appearance10
        Appearance11.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance11.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.cmbAccounts.DisplayLayout.Override.RowSelectorAppearance = Appearance11
        Me.cmbAccounts.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbAccounts.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbAccounts.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance12.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance12.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance12.ForeColor = System.Drawing.Color.Black
        Me.cmbAccounts.DisplayLayout.Override.SelectedRowAppearance = Appearance12
        Me.cmbAccounts.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbAccounts.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbAccounts.DisplayLayout.Override.SelectTypeGroupByRow = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbAccounts.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Extended
        Me.cmbAccounts.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbAccounts.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.cmbAccounts.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbAccounts.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell
        Me.cmbAccounts.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.cmbAccounts.LimitToList = True
        Me.cmbAccounts.Location = New System.Drawing.Point(16, 44)
        Me.cmbAccounts.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbAccounts.Name = "cmbAccounts"
        Me.cmbAccounts.Size = New System.Drawing.Size(279, 23)
        Me.cmbAccounts.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbAccounts, "Select Customer Account")
        '
        'DtpFillDate
        '
        Me.DtpFillDate.Checked = False
        Me.DtpFillDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.DtpFillDate.CustomFormat = "dd/MMM/yyyy"
        Me.DtpFillDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.DtpFillDate.DropDownCalendar.Name = ""
        Me.DtpFillDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard
        Me.DtpFillDate.Location = New System.Drawing.Point(147, 146)
        Me.DtpFillDate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.DtpFillDate.Name = "DtpFillDate"
        Me.DtpFillDate.ShowCheckBox = True
        Me.DtpFillDate.Size = New System.Drawing.Size(148, 22)
        Me.DtpFillDate.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.DtpFillDate, "Due On/Before Date")
        '
        'txtAmount
        '
        Me.txtAmount.BackColor = System.Drawing.SystemColors.Info
        Me.txtAmount.Location = New System.Drawing.Point(1373, 293)
        Me.txtAmount.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.ReadOnly = True
        Me.txtAmount.Size = New System.Drawing.Size(96, 22)
        Me.txtAmount.TabIndex = 32
        Me.txtAmount.TabStop = False
        Me.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtAmount, "Due Amount")
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Controls.Add(Me.txtReference)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.dtpDate)
        Me.GroupBox3.Controls.Add(Me.txtVoucherNo)
        Me.GroupBox3.Location = New System.Drawing.Point(661, 36)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox3.Size = New System.Drawing.Size(261, 233)
        Me.GroupBox3.TabIndex = 7
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Document"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(7, 126)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(77, 17)
        Me.Label10.TabIndex = 4
        Me.Label10.Text = "Reference"
        '
        'txtReference
        '
        Me.txtReference.Location = New System.Drawing.Point(8, 148)
        Me.txtReference.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtReference.Name = "txtReference"
        Me.txtReference.Size = New System.Drawing.Size(240, 22)
        Me.txtReference.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.txtReference, "Reference for necessary")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(7, 76)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(94, 17)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Voucher No."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 23)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 17)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Date"
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtpDate.DropDownCalendar.Name = ""
        Me.dtpDate.DropDownCalendar.VisualStyle = Janus.Windows.CalendarCombo.VisualStyle.Standard
        Me.dtpDate.Location = New System.Drawing.Point(8, 48)
        Me.dtpDate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(241, 22)
        Me.dtpDate.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.dtpDate, "Document Date")
        '
        'txtVoucherNo
        '
        Me.txtVoucherNo.BackColor = System.Drawing.SystemColors.Info
        Me.txtVoucherNo.Location = New System.Drawing.Point(8, 97)
        Me.txtVoucherNo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtVoucherNo.Name = "txtVoucherNo"
        Me.txtVoucherNo.ReadOnly = True
        Me.txtVoucherNo.Size = New System.Drawing.Size(240, 22)
        Me.txtVoucherNo.TabIndex = 3
        Me.txtVoucherNo.TabStop = False
        Me.ToolTip1.SetToolTip(Me.txtVoucherNo, "Voucher No")
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(1268, 272)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(93, 17)
        Me.Label19.TabIndex = 29
        Me.Label19.Text = "Tax Amount"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(4, 272)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 17)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Invoice List"
        '
        'txtTaxAmount
        '
        Me.txtTaxAmount.BackColor = System.Drawing.SystemColors.Info
        Me.txtTaxAmount.Location = New System.Drawing.Point(1273, 293)
        Me.txtTaxAmount.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtTaxAmount.Name = "txtTaxAmount"
        Me.txtTaxAmount.ReadOnly = True
        Me.txtTaxAmount.Size = New System.Drawing.Size(96, 22)
        Me.txtTaxAmount.TabIndex = 30
        Me.txtTaxAmount.TabStop = False
        Me.txtTaxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtTaxAmount, "Due Amount")
        '
        'BtnAdd
        '
        Me.BtnAdd.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnAdd.Location = New System.Drawing.Point(1475, 292)
        Me.BtnAdd.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.BtnAdd.Name = "BtnAdd"
        Me.BtnAdd.Size = New System.Drawing.Size(63, 28)
        Me.BtnAdd.TabIndex = 33
        Me.BtnAdd.Text = "+"
        Me.ToolTip1.SetToolTip(Me.BtnAdd, "Add Sales Invoice Detail to Grid")
        Me.BtnAdd.UseVisualStyleBackColor = True
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.Location = New System.Drawing.Point(1181, 273)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(82, 17)
        Me.Label17.TabIndex = 27
        Me.Label17.Text = "WH Tax %"
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(208, 293)
        Me.txtRemarks.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(149, 22)
        Me.txtRemarks.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.txtRemarks, "Remarks for necessary")
        '
        'txtWHTax
        '
        Me.txtWHTax.Location = New System.Drawing.Point(1185, 293)
        Me.txtWHTax.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtWHTax.Name = "txtWHTax"
        Me.txtWHTax.Size = New System.Drawing.Size(84, 22)
        Me.txtWHTax.TabIndex = 28
        Me.txtWHTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtWHTax, "Received Amount")
        '
        'txtInvoicoAmount
        '
        Me.txtInvoicoAmount.BackColor = System.Drawing.SystemColors.Info
        Me.txtInvoicoAmount.Location = New System.Drawing.Point(603, 293)
        Me.txtInvoicoAmount.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtInvoicoAmount.Name = "txtInvoicoAmount"
        Me.txtInvoicoAmount.ReadOnly = True
        Me.txtInvoicoAmount.Size = New System.Drawing.Size(124, 22)
        Me.txtInvoicoAmount.TabIndex = 15
        Me.txtInvoicoAmount.TabStop = False
        Me.txtInvoicoAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtInvoicoAmount, "Sales Invoice Amount")
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(1080, 272)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(93, 17)
        Me.Label16.TabIndex = 25
        Me.Label16.Text = "Net Amount"
        '
        'txtReceivedAmt
        '
        Me.txtReceivedAmt.Location = New System.Drawing.Point(731, 293)
        Me.txtReceivedAmt.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtReceivedAmt.Name = "txtReceivedAmt"
        Me.txtReceivedAmt.Size = New System.Drawing.Size(84, 22)
        Me.txtReceivedAmt.TabIndex = 17
        Me.txtReceivedAmt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtReceivedAmt, "Received Amount")
        '
        'txtNetAmount
        '
        Me.txtNetAmount.BackColor = System.Drawing.SystemColors.Info
        Me.txtNetAmount.Location = New System.Drawing.Point(1085, 293)
        Me.txtNetAmount.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtNetAmount.Name = "txtNetAmount"
        Me.txtNetAmount.ReadOnly = True
        Me.txtNetAmount.Size = New System.Drawing.Size(96, 22)
        Me.txtNetAmount.TabIndex = 26
        Me.txtNetAmount.TabStop = False
        Me.txtNetAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtNetAmount, "Due Amount")
        '
        'txtDueAmount
        '
        Me.txtDueAmount.BackColor = System.Drawing.SystemColors.Info
        Me.txtDueAmount.Location = New System.Drawing.Point(985, 293)
        Me.txtDueAmount.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtDueAmount.Name = "txtDueAmount"
        Me.txtDueAmount.ReadOnly = True
        Me.txtDueAmount.Size = New System.Drawing.Size(96, 22)
        Me.txtDueAmount.TabIndex = 24
        Me.txtDueAmount.TabStop = False
        Me.txtDueAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtDueAmount, "Due Amount")
        '
        'cmbInvoiceList
        '
        Me.cmbInvoiceList.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.cmbInvoiceList.CheckedListSettings.CheckStateMember = ""
        Appearance1.BackColor = System.Drawing.Color.White
        Appearance1.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        Me.cmbInvoiceList.DisplayLayout.Appearance = Appearance1
        UltraGridColumn1.Header.VisiblePosition = 0
        UltraGridColumn1.Hidden = True
        UltraGridColumn2.Header.Caption = "Invoice No"
        UltraGridColumn2.Header.VisiblePosition = 1
        UltraGridColumn3.Header.Caption = "Invoice Date"
        UltraGridColumn3.Header.VisiblePosition = 2
        UltraGridColumn4.Header.Caption = "Invoice Amount"
        UltraGridColumn4.Header.VisiblePosition = 3
        UltraGridColumn9.Header.VisiblePosition = 4
        UltraGridColumn10.Header.VisiblePosition = 5
        UltraGridColumn11.Header.Caption = "Due Balance"
        UltraGridColumn11.Header.VisiblePosition = 6
        UltraGridBand2.Columns.AddRange(New Object() {UltraGridColumn1, UltraGridColumn2, UltraGridColumn3, UltraGridColumn4, UltraGridColumn9, UltraGridColumn10, UltraGridColumn11})
        Me.cmbInvoiceList.DisplayLayout.BandsSerializer.Add(UltraGridBand2)
        Me.cmbInvoiceList.DisplayLayout.InterBandSpacing = 10
        Me.cmbInvoiceList.DisplayLayout.MaxColScrollRegions = 1
        Me.cmbInvoiceList.DisplayLayout.MaxRowScrollRegions = 1
        Appearance2.BackColor = System.Drawing.Color.Transparent
        Me.cmbInvoiceList.DisplayLayout.Override.CardAreaAppearance = Appearance2
        Me.cmbInvoiceList.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Appearance3.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance3.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance3.ForeColor = System.Drawing.Color.White
        Appearance3.TextHAlignAsString = "Left"
        Appearance3.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent
        Me.cmbInvoiceList.DisplayLayout.Override.HeaderAppearance = Appearance3
        Me.cmbInvoiceList.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance4.BorderColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbInvoiceList.DisplayLayout.Override.RowAppearance = Appearance4
        Appearance5.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance5.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.cmbInvoiceList.DisplayLayout.Override.RowSelectorAppearance = Appearance5
        Me.cmbInvoiceList.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbInvoiceList.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance6.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance6.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance6.ForeColor = System.Drawing.Color.Black
        Me.cmbInvoiceList.DisplayLayout.Override.SelectedRowAppearance = Appearance6
        Me.cmbInvoiceList.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbInvoiceList.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.cmbInvoiceList.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbInvoiceList.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbInvoiceList.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbInvoiceList.LimitToList = True
        Me.cmbInvoiceList.Location = New System.Drawing.Point(8, 293)
        Me.cmbInvoiceList.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbInvoiceList.Name = "cmbInvoiceList"
        Me.cmbInvoiceList.Size = New System.Drawing.Size(193, 23)
        Me.cmbInvoiceList.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.cmbInvoiceList, "Select Sales Invoice")
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(203, 272)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(69, 17)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Remarks"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(903, 272)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(48, 17)
        Me.Label14.TabIndex = 21
        Me.Label14.Text = "Other"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(597, 272)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(118, 17)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Invoice Amount"
        '
        'txtOtherTaxAmount
        '
        Me.txtOtherTaxAmount.Location = New System.Drawing.Point(903, 293)
        Me.txtOtherTaxAmount.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtOtherTaxAmount.Name = "txtOtherTaxAmount"
        Me.txtOtherTaxAmount.Size = New System.Drawing.Size(79, 22)
        Me.txtOtherTaxAmount.TabIndex = 22
        Me.txtOtherTaxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtOtherTaxAmount, "Received Amount")
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(980, 272)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(97, 17)
        Me.Label8.TabIndex = 23
        Me.Label8.Text = "Due Amount"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(816, 272)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(66, 17)
        Me.Label13.TabIndex = 19
        Me.Label13.Text = "Sale Tax"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(729, 272)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(69, 17)
        Me.Label9.TabIndex = 16
        Me.Label9.Text = "Received"
        '
        'txtSalesTaxAmount
        '
        Me.txtSalesTaxAmount.Location = New System.Drawing.Point(820, 292)
        Me.txtSalesTaxAmount.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtSalesTaxAmount.Name = "txtSalesTaxAmount"
        Me.txtSalesTaxAmount.Size = New System.Drawing.Size(79, 22)
        Me.txtSalesTaxAmount.TabIndex = 20
        Me.txtSalesTaxAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtSalesTaxAmount, "Received Amount")
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(339, 455)
        Me.lblProgress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(351, 55)
        Me.lblProgress.TabIndex = 0
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'lblPrintStatus
        '
        Me.lblPrintStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPrintStatus.AutoSize = True
        Me.lblPrintStatus.Location = New System.Drawing.Point(1350, 11)
        Me.lblPrintStatus.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPrintStatus.Name = "lblPrintStatus"
        Me.lblPrintStatus.Size = New System.Drawing.Size(178, 17)
        Me.lblPrintStatus.TabIndex = 2
        Me.lblPrintStatus.Text = "Print Status : Print Pending"
        '
        'txtReceiptId
        '
        Me.txtReceiptId.Location = New System.Drawing.Point(825, 7)
        Me.txtReceiptId.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtReceiptId.Name = "txtReceiptId"
        Me.txtReceiptId.Size = New System.Drawing.Size(60, 22)
        Me.txtReceiptId.TabIndex = 1
        Me.txtReceiptId.Visible = False
        '
        'GrdReceiptDetail
        '
        Me.GrdReceiptDetail.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GrdReceiptDetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        GrdReceiptDetail_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.GrdReceiptDetail.DesignTimeLayout = GrdReceiptDetail_DesignTimeLayout
        Me.GrdReceiptDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GrdReceiptDetail.GroupByBoxVisible = False
        Me.GrdReceiptDetail.Location = New System.Drawing.Point(1, 367)
        Me.GrdReceiptDetail.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GrdReceiptDetail.Name = "GrdReceiptDetail"
        Me.GrdReceiptDetail.RecordNavigator = True
        Me.GrdReceiptDetail.Size = New System.Drawing.Size(1548, 217)
        Me.GrdReceiptDetail.TabIndex = 34
        Me.GrdReceiptDetail.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        Me.GrdReceiptDetail.TabStop = False
        Me.ToolTip1.SetToolTip(Me.GrdReceiptDetail, "Receipts Detail")
        Me.GrdReceiptDetail.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GrdReceiptDetail.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GrdReceiptDetail.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.GrdSaved)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-13333, -12308)
        Me.UltraTabPageControl2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1550, 582)
        '
        'GrdSaved
        '
        Me.GrdSaved.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GrdSaved.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GrdSaved.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GrdSaved.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GrdSaved.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GrdSaved.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GrdSaved.GroupByBoxVisible = False
        Me.GrdSaved.Location = New System.Drawing.Point(0, -1)
        Me.GrdSaved.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GrdSaved.Name = "GrdSaved"
        Me.GrdSaved.RecordNavigator = True
        Me.GrdSaved.Size = New System.Drawing.Size(1550, 585)
        Me.GrdSaved.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.GrdSaved, "Saved Record Detail")
        Me.GrdSaved.UseGroupRowSelector = True
        Me.GrdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'pnlHeader
        '
        Me.pnlHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.CtrlGrdBar2)
        Me.pnlHeader.Controls.Add(Me.CtrlGrdBar1)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Controls.Add(Me.ToolStrip1)
        Me.pnlHeader.Location = New System.Drawing.Point(0, -2)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1552, 91)
        Me.pnlHeader.TabIndex = 0
        '
        'CtrlGrdBar2
        '
        Me.CtrlGrdBar2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar2.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar2.Email = Nothing
        Me.CtrlGrdBar2.FormName = Me
        Me.CtrlGrdBar2.Location = New System.Drawing.Point(1503, 0)
        Me.CtrlGrdBar2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CtrlGrdBar2.MyGrid = Me.GrdSaved
        Me.CtrlGrdBar2.Name = "CtrlGrdBar2"
        Me.CtrlGrdBar2.Size = New System.Drawing.Size(48, 32)
        Me.CtrlGrdBar2.TabIndex = 3
        Me.CtrlGrdBar2.TabStop = False
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(1500, 1)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CtrlGrdBar1.MyGrid = Me.GrdReceiptDetail
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(51, 31)
        Me.CtrlGrdBar1.TabIndex = 3
        Me.CtrlGrdBar1.TabStop = False
        '
        'lblHeader
        '
        Me.lblHeader.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(15, 41)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(311, 29)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Invoice Based Receipt"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BtnNew, Me.BtnEdit, Me.BtnSave, Me.BtnPrint, Me.toolStripSeparator, Me.BtnDelete, Me.toolStripSeparator1, Me.HelpToolStripButton, Me.BtnLoadAll, Me.BtnRefresh, Me.ToolStripSeparator2, Me.btnSMSTemplate, Me.ToolStripSeparator3, Me.btnUpdateTimes, Me.tsbTask, Me.tsbConfig})
        Me.ToolStrip1.Location = New System.Drawing.Point(-3, 1)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1505, 31)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'BtnNew
        '
        Me.BtnNew.Image = Global.SimpleAccounts.My.Resources.Resources.BtnNew_Image
        Me.BtnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnNew.Name = "BtnNew"
        Me.BtnNew.Size = New System.Drawing.Size(60, 28)
        Me.BtnNew.Text = "&New"
        '
        'BtnEdit
        '
        Me.BtnEdit.Image = Global.SimpleAccounts.My.Resources.Resources.BtnEdit_Image
        Me.BtnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnEdit.Name = "BtnEdit"
        Me.BtnEdit.Size = New System.Drawing.Size(56, 28)
        Me.BtnEdit.Text = "&Edit"
        '
        'BtnSave
        '
        Me.BtnSave.Image = Global.SimpleAccounts.My.Resources.Resources.BtnSave_Image
        Me.BtnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(61, 28)
        Me.BtnSave.Text = "&Save"
        '
        'BtnPrint
        '
        Me.BtnPrint.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrintVoucherToolStripMenuItem})
        Me.BtnPrint.Image = Global.SimpleAccounts.My.Resources.Resources.print
        Me.BtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(77, 28)
        Me.BtnPrint.Text = "&Print"
        '
        'PrintVoucherToolStripMenuItem
        '
        Me.PrintVoucherToolStripMenuItem.Name = "PrintVoucherToolStripMenuItem"
        Me.PrintVoucherToolStripMenuItem.Size = New System.Drawing.Size(167, 26)
        Me.PrintVoucherToolStripMenuItem.Text = "Print Voucher"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 31)
        '
        'BtnDelete
        '
        Me.BtnDelete.Image = Global.SimpleAccounts.My.Resources.Resources.BtnDelete_Image
        Me.BtnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.Size = New System.Drawing.Size(72, 28)
        Me.BtnDelete.Text = "D&elete"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 31)
        '
        'HelpToolStripButton
        '
        Me.HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HelpToolStripButton.Image = Global.SimpleAccounts.My.Resources.Resources.HelpToolStripButton_Image
        Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton.Name = "HelpToolStripButton"
        Me.HelpToolStripButton.Size = New System.Drawing.Size(24, 28)
        Me.HelpToolStripButton.Text = "He&lp"
        '
        'BtnLoadAll
        '
        Me.BtnLoadAll.Image = Global.SimpleAccounts.My.Resources.Resources.sendcontactdetails
        Me.BtnLoadAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnLoadAll.Name = "BtnLoadAll"
        Me.BtnLoadAll.Size = New System.Drawing.Size(82, 28)
        Me.BtnLoadAll.Text = "Load All"
        '
        'BtnRefresh
        '
        Me.BtnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.BtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnRefresh.Name = "BtnRefresh"
        Me.BtnRefresh.Size = New System.Drawing.Size(78, 28)
        Me.BtnRefresh.Text = "Refresh"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 31)
        '
        'btnSMSTemplate
        '
        Me.btnSMSTemplate.Image = Global.SimpleAccounts.My.Resources.Resources.Attach
        Me.btnSMSTemplate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSMSTemplate.Name = "btnSMSTemplate"
        Me.btnSMSTemplate.Size = New System.Drawing.Size(164, 28)
        Me.btnSMSTemplate.Text = "SMS template setting"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 31)
        '
        'btnUpdateTimes
        '
        Me.btnUpdateTimes.Image = Global.SimpleAccounts.My.Resources.Resources.save_edit
        Me.btnUpdateTimes.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnUpdateTimes.Name = "btnUpdateTimes"
        Me.btnUpdateTimes.Size = New System.Drawing.Size(166, 28)
        Me.btnUpdateTimes.Text = "No of update times"
        '
        'tsbTask
        '
        Me.tsbTask.Image = Global.SimpleAccounts.My.Resources.Resources.Untitled_1
        Me.tsbTask.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbTask.Name = "tsbTask"
        Me.tsbTask.Size = New System.Drawing.Size(102, 28)
        Me.tsbTask.Text = "Task Assign"
        '
        'tsbConfig
        '
        Me.tsbConfig.Image = Global.SimpleAccounts.My.Resources.Resources.Advanced_Options
        Me.tsbConfig.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbConfig.Name = "tsbConfig"
        Me.tsbConfig.Size = New System.Drawing.Size(73, 28)
        Me.tsbConfig.Text = "Config"
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 87)
        Me.UltraTabControl1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(1552, 604)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 0
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Invoice Based Receipt"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "History"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.TabStop = False
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1550, 582)
        '
        'frmReceiptVoucherNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(1552, 692)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.pnlHeader)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmReceiptVoucherNew"
        Me.Text = "Invoice Based Receipt"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.cmbAccounts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.cmbInvoiceList, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GrdReceiptDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.GrdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents BtnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BtnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents DtpChequeDate As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents lblChequeDate As System.Windows.Forms.Label
    Friend WithEvents lblChequeNo As System.Windows.Forms.Label
    Friend WithEvents txtChequeNo As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbPaymentMethod As System.Windows.Forms.ComboBox
    Friend WithEvents cmbPayFrom As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtReference As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpDate As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents txtVoucherNo As System.Windows.Forms.TextBox
    Friend WithEvents cmbInvoiceList As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents txtDueAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtReceivedAmt As System.Windows.Forms.TextBox
    Friend WithEvents txtInvoicoAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents DtpFillDate As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents BtnAdd As System.Windows.Forms.Button
    Friend WithEvents GrdReceiptDetail As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents GrdSaved As Janus.Windows.GridEX.GridEX
    Friend WithEvents BtnLoadAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtReceiptId As System.Windows.Forms.TextBox
    Friend WithEvents cmbAccounts As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents cmbCostCenter As System.Windows.Forms.ComboBox
    Friend WithEvents lblCostCenter As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lblPrintStatus As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtSalesTaxAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtOtherTaxAmount As System.Windows.Forms.TextBox
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents ChkPost As System.Windows.Forms.CheckBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtCurrentBalance As System.Windows.Forms.TextBox
    Friend WithEvents BtnPrint As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents PrintVoucherToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSMSTemplate As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbCompany As System.Windows.Forms.ComboBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnUpdateTimes As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents tsbTask As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbConfig As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtTaxAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtWHTax As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtNetAmount As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents CtrlGrdBar2 As SimpleAccounts.CtrlGrdBar
End Class

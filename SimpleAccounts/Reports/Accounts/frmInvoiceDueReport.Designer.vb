<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInvoiceDueReport
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
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance27 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance28 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance29 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance30 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance31 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInvoiceDueReport))
        Dim Appearance32 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance33 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance34 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance35 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance36 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance22 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn26 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ID")
        Dim UltraGridColumn27 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Name")
        Dim UltraGridColumn28 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Territory")
        Dim UltraGridColumn29 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("City")
        Dim UltraGridColumn30 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("State")
        Dim UltraGridColumn31 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("AcId")
        Dim Appearance23 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance24 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance25 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance26 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.CtrlGrdBar4 = New SimpleAccounts.CtrlGrdBar()
        Me.grdSaved = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.btnSearchEdit = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnSearchPrint = New System.Windows.Forms.ToolStripSplitButton()
        Me.SelectedQuotationPrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintAttachmentQuotationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintQuotationQtyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintQuotationTaxToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CustomPrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintQuotationItemSpecificationToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintQuotationWithImageToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintDuplicateQuotationToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintSelectedRevisionToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnSearchDelete = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnSearchLoadAll = New System.Windows.Forms.ToolStripButton()
        Me.btnSearchDocument = New System.Windows.Forms.ToolStripButton()
        Me.HelpToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtInquiryNo = New System.Windows.Forms.TextBox()
        Me.Label46 = New System.Windows.Forms.Label()
        Me.txtCustomerInquiryNo = New System.Windows.Forms.TextBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.txtSearchRemarks = New System.Windows.Forms.TextBox()
        Me.txtPurchaseNo = New System.Windows.Forms.TextBox()
        Me.cmbSearchLocation = New System.Windows.Forms.ComboBox()
        Me.txtFromAmount = New System.Windows.Forms.TextBox()
        Me.txtToAmount = New System.Windows.Forms.TextBox()
        Me.txtSearchDocNo = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pnlhideitem = New System.Windows.Forms.Panel()
        Me.txtManualSerialNo = New System.Windows.Forms.TextBox()
        Me.Label48 = New System.Windows.Forms.Label()
        Me.Label45 = New System.Windows.Forms.Label()
        Me.lblSubject = New System.Windows.Forms.Label()
        Me.txtItemPackRate = New System.Windows.Forms.TextBox()
        Me.txtSubject = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtPackQty = New System.Windows.Forms.TextBox()
        Me.cmbItem = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.dtpDeliveryDate = New System.Windows.Forms.DateTimePicker()
        Me.txtStock = New System.Windows.Forms.TextBox()
        Me.lblDeliveryDate = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblStock = New System.Windows.Forms.Label()
        Me.txtTotalQuantity = New System.Windows.Forms.TextBox()
        Me.rdoCode = New System.Windows.Forms.RadioButton()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.rdoName = New System.Windows.Forms.RadioButton()
        Me.cmbCategory = New System.Windows.Forms.ComboBox()
        Me.cmbUnit = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtSchemeQty = New System.Windows.Forms.TextBox()
        Me.GroupBox7 = New System.Windows.Forms.GroupBox()
        Me.txtTenderSrNo = New System.Windows.Forms.TextBox()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.cmbItemDescription = New System.Windows.Forms.ComboBox()
        Me.txtTradePrice = New System.Windows.Forms.TextBox()
        Me.txtItemRegNo = New System.Windows.Forms.TextBox()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.txtSpecs = New System.Windows.Forms.TextBox()
        Me.txtBrand = New System.Windows.Forms.TextBox()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.GroupBox9 = New System.Windows.Forms.GroupBox()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.cmbCurrency = New System.Windows.Forms.ComboBox()
        Me.txtCurrencyRate = New System.Windows.Forms.TextBox()
        Me.lblCurrencyRate = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lnkLblRevisions = New System.Windows.Forms.LinkLabel()
        Me.cmbRevisionNumber = New System.Windows.Forms.ComboBox()
        Me.lblRev = New System.Windows.Forms.Label()
        Me.LnkLoadAll = New System.Windows.Forms.LinkLabel()
        Me.ChkApproved = New System.Windows.Forms.CheckBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.dtpPDate = New System.Windows.Forms.DateTimePicker()
        Me.txtPONo = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.dtpPODate = New System.Windows.Forms.DateTimePicker()
        Me.txtCustPONo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.chkPost = New System.Windows.Forms.CheckBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.cmbSalesMan = New System.Windows.Forms.ComboBox()
        Me.Label44 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.cmbProject = New System.Windows.Forms.ComboBox()
        Me.Label53 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtCustomerAddress = New System.Windows.Forms.ComboBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtCustomerMobile = New System.Windows.Forms.ComboBox()
        Me.cmbCompany = New System.Windows.Forms.ComboBox()
        Me.lblInvType = New System.Windows.Forms.Label()
        Me.cmbVendor = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.chkAddItemDescription = New System.Windows.Forms.CheckBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtNewCustomer = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.txtPackRate = New System.Windows.Forms.TextBox()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.rbtAdjPercentage = New System.Windows.Forms.RadioButton()
        Me.rbtAdjFlat = New System.Windows.Forms.RadioButton()
        Me.txtSpecialAdjustment = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtNetBill = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.CtrlGrdBar3 = New SimpleAccounts.CtrlGrdBar()
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX()
        Me.SplitItemDetail = New System.Windows.Forms.SplitContainer()
        Me.utcItems = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage2 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.cmbSearchAccount = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Vendor = New System.Windows.Forms.RadioButton()
        Me.Customer = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbCustomer = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.pnlHeader.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.UltraTabPageControl1.SuspendLayout()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip2.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlhideitem.SuspendLayout()
        CType(Me.cmbItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox7.SuspendLayout()
        Me.GroupBox9.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.cmbVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox6.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitItemDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitItemDetail.SuspendLayout()
        CType(Me.utcItems, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbSearchAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox5.SuspendLayout()
        CType(Me.cmbCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Controls.Add(Me.CtrlGrdBar1)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1467, 47)
        Me.pnlHeader.TabIndex = 13
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.lblHeader.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.lblHeader.Location = New System.Drawing.Point(9, 5)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(264, 37)
        Me.lblHeader.TabIndex = 10
        Me.lblHeader.Text = "Invoice Due Report"
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(1400, 10)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4)
        Me.CtrlGrdBar1.MyGrid = Me.grd
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(51, 31)
        Me.CtrlGrdBar1.TabIndex = 7
        Me.CtrlGrdBar1.TabStop = False
        '
        'grd
        '
        Me.grd.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grd.ColumnAutoResize = True
        Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.grd.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.grd.Location = New System.Drawing.Point(0, 97)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both
        Me.grd.ScrollBarWidth = 23
        Me.grd.Size = New System.Drawing.Size(1467, 446)
        Me.grd.TabIndex = 25
        Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(200, 100)
        Me.UltraTabControl1.TabIndex = 0
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(1, 24)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(196, 73)
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.CtrlGrdBar4)
        Me.UltraTabPageControl1.Controls.Add(Me.ToolStrip2)
        Me.UltraTabPageControl1.Controls.Add(Me.SplitContainer1)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1465, 442)
        '
        'CtrlGrdBar4
        '
        Me.CtrlGrdBar4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar4.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.CtrlGrdBar4.Email = Nothing
        Me.CtrlGrdBar4.FormName = Nothing
        Me.CtrlGrdBar4.Location = New System.Drawing.Point(1429, 0)
        Me.CtrlGrdBar4.MyGrid = Me.grdSaved
        Me.CtrlGrdBar4.Name = "CtrlGrdBar4"
        Me.CtrlGrdBar4.Size = New System.Drawing.Size(34, 25)
        Me.CtrlGrdBar4.TabIndex = 3
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
        Me.grdSaved.Size = New System.Drawing.Size(1462, 281)
        Me.grdSaved.TabIndex = 0
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'ToolStrip2
        '
        Me.ToolStrip2.AutoSize = False
        Me.ToolStrip2.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnSearchEdit, Me.toolStripSeparator2, Me.btnSearchPrint, Me.btnSearchDelete, Me.toolStripSeparator3, Me.btnSearchLoadAll, Me.btnSearchDocument, Me.HelpToolStripButton1})
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(1465, 25)
        Me.ToolStrip2.TabIndex = 0
        Me.ToolStrip2.Text = "ToolStrip2"
        '
        'btnSearchEdit
        '
        Me.btnSearchEdit.Image = Global.SimpleAccounts.My.Resources.Resources.BtnEdit_Image
        Me.btnSearchEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSearchEdit.Name = "btnSearchEdit"
        Me.btnSearchEdit.Size = New System.Drawing.Size(59, 22)
        Me.btnSearchEdit.Text = "&Edit"
        '
        'toolStripSeparator2
        '
        Me.toolStripSeparator2.Name = "toolStripSeparator2"
        Me.toolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'btnSearchPrint
        '
        Me.btnSearchPrint.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SelectedQuotationPrintToolStripMenuItem, Me.PrintAttachmentQuotationToolStripMenuItem, Me.PrintQuotationQtyToolStripMenuItem, Me.PrintQuotationTaxToolStripMenuItem, Me.CustomPrintToolStripMenuItem, Me.PrintQuotationItemSpecificationToolStripMenuItem1, Me.PrintQuotationWithImageToolStripMenuItem1, Me.PrintDuplicateQuotationToolStripMenuItem1, Me.PrintSelectedRevisionToolStripMenuItem1})
        Me.btnSearchPrint.Image = Global.SimpleAccounts.My.Resources.Resources.print
        Me.btnSearchPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSearchPrint.Name = "btnSearchPrint"
        Me.btnSearchPrint.Size = New System.Drawing.Size(78, 22)
        Me.btnSearchPrint.Text = "&Print"
        '
        'SelectedQuotationPrintToolStripMenuItem
        '
        Me.SelectedQuotationPrintToolStripMenuItem.Name = "SelectedQuotationPrintToolStripMenuItem"
        Me.SelectedQuotationPrintToolStripMenuItem.Size = New System.Drawing.Size(309, 26)
        Me.SelectedQuotationPrintToolStripMenuItem.Text = "Selected Quotation Print"
        '
        'PrintAttachmentQuotationToolStripMenuItem
        '
        Me.PrintAttachmentQuotationToolStripMenuItem.Name = "PrintAttachmentQuotationToolStripMenuItem"
        Me.PrintAttachmentQuotationToolStripMenuItem.Size = New System.Drawing.Size(309, 26)
        Me.PrintAttachmentQuotationToolStripMenuItem.Text = "Print Attachment Quotation"
        '
        'PrintQuotationQtyToolStripMenuItem
        '
        Me.PrintQuotationQtyToolStripMenuItem.Name = "PrintQuotationQtyToolStripMenuItem"
        Me.PrintQuotationQtyToolStripMenuItem.Size = New System.Drawing.Size(309, 26)
        Me.PrintQuotationQtyToolStripMenuItem.Text = "Print Quotation Qty"
        '
        'PrintQuotationTaxToolStripMenuItem
        '
        Me.PrintQuotationTaxToolStripMenuItem.Name = "PrintQuotationTaxToolStripMenuItem"
        Me.PrintQuotationTaxToolStripMenuItem.Size = New System.Drawing.Size(309, 26)
        Me.PrintQuotationTaxToolStripMenuItem.Text = "Print Quotation Tax"
        '
        'CustomPrintToolStripMenuItem
        '
        Me.CustomPrintToolStripMenuItem.Name = "CustomPrintToolStripMenuItem"
        Me.CustomPrintToolStripMenuItem.Size = New System.Drawing.Size(309, 26)
        Me.CustomPrintToolStripMenuItem.Text = "Custom Print"
        '
        'PrintQuotationItemSpecificationToolStripMenuItem1
        '
        Me.PrintQuotationItemSpecificationToolStripMenuItem1.Name = "PrintQuotationItemSpecificationToolStripMenuItem1"
        Me.PrintQuotationItemSpecificationToolStripMenuItem1.Size = New System.Drawing.Size(309, 26)
        Me.PrintQuotationItemSpecificationToolStripMenuItem1.Text = "Print Quotation Item Specification"
        '
        'PrintQuotationWithImageToolStripMenuItem1
        '
        Me.PrintQuotationWithImageToolStripMenuItem1.Name = "PrintQuotationWithImageToolStripMenuItem1"
        Me.PrintQuotationWithImageToolStripMenuItem1.Size = New System.Drawing.Size(309, 26)
        Me.PrintQuotationWithImageToolStripMenuItem1.Text = "Print Quotation with Image"
        '
        'PrintDuplicateQuotationToolStripMenuItem1
        '
        Me.PrintDuplicateQuotationToolStripMenuItem1.Name = "PrintDuplicateQuotationToolStripMenuItem1"
        Me.PrintDuplicateQuotationToolStripMenuItem1.Size = New System.Drawing.Size(309, 26)
        Me.PrintDuplicateQuotationToolStripMenuItem1.Text = "Print Duplicate Quotation"
        '
        'PrintSelectedRevisionToolStripMenuItem1
        '
        Me.PrintSelectedRevisionToolStripMenuItem1.Name = "PrintSelectedRevisionToolStripMenuItem1"
        Me.PrintSelectedRevisionToolStripMenuItem1.Size = New System.Drawing.Size(309, 26)
        Me.PrintSelectedRevisionToolStripMenuItem1.Text = "Print Selected Revision"
        '
        'btnSearchDelete
        '
        Me.btnSearchDelete.Image = Global.SimpleAccounts.My.Resources.Resources.BtnDelete_Image
        Me.btnSearchDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSearchDelete.Name = "btnSearchDelete"
        Me.btnSearchDelete.Size = New System.Drawing.Size(77, 22)
        Me.btnSearchDelete.Text = "Dele&te"
        '
        'toolStripSeparator3
        '
        Me.toolStripSeparator3.Name = "toolStripSeparator3"
        Me.toolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'btnSearchLoadAll
        '
        Me.btnSearchLoadAll.Image = Global.SimpleAccounts.My.Resources.Resources.sendcontactdetails
        Me.btnSearchLoadAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSearchLoadAll.Name = "btnSearchLoadAll"
        Me.btnSearchLoadAll.Size = New System.Drawing.Size(88, 22)
        Me.btnSearchLoadAll.Text = "&Load All"
        '
        'btnSearchDocument
        '
        Me.btnSearchDocument.Image = Global.SimpleAccounts.My.Resources.Resources.search_32
        Me.btnSearchDocument.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSearchDocument.Name = "btnSearchDocument"
        Me.btnSearchDocument.Size = New System.Drawing.Size(77, 22)
        Me.btnSearchDocument.Text = "Search"
        '
        'HelpToolStripButton1
        '
        Me.HelpToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton1.Name = "HelpToolStripButton1"
        Me.HelpToolStripButton1.Size = New System.Drawing.Size(45, 22)
        Me.HelpToolStripButton1.Text = "He&lp"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 28)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.grdSaved)
        Me.SplitContainer1.Size = New System.Drawing.Size(1462, 415)
        Me.SplitContainer1.SplitterDistance = 130
        Me.SplitContainer1.TabIndex = 2
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.txtInquiryNo)
        Me.GroupBox2.Controls.Add(Me.Label46)
        Me.GroupBox2.Controls.Add(Me.txtCustomerInquiryNo)
        Me.GroupBox2.Controls.Add(Me.btnSearch)
        Me.GroupBox2.Controls.Add(Me.Label28)
        Me.GroupBox2.Controls.Add(Me.Label29)
        Me.GroupBox2.Controls.Add(Me.Label30)
        Me.GroupBox2.Controls.Add(Me.Label31)
        Me.GroupBox2.Controls.Add(Me.Label32)
        Me.GroupBox2.Controls.Add(Me.Label33)
        Me.GroupBox2.Controls.Add(Me.Label34)
        Me.GroupBox2.Controls.Add(Me.txtSearchRemarks)
        Me.GroupBox2.Controls.Add(Me.txtPurchaseNo)
        Me.GroupBox2.Controls.Add(Me.cmbSearchLocation)
        Me.GroupBox2.Controls.Add(Me.txtFromAmount)
        Me.GroupBox2.Controls.Add(Me.txtToAmount)
        Me.GroupBox2.Controls.Add(Me.txtSearchDocNo)
        Me.GroupBox2.Controls.Add(Me.Label35)
        Me.GroupBox2.Controls.Add(Me.Label36)
        Me.GroupBox2.Controls.Add(Me.dtpFrom)
        Me.GroupBox2.Controls.Add(Me.dtpTo)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1056, 112)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Document Search"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(760, 61)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(72, 17)
        Me.Label14.TabIndex = 22
        Me.Label14.Text = "Inquiry No"
        '
        'txtInquiryNo
        '
        Me.txtInquiryNo.Location = New System.Drawing.Point(893, 58)
        Me.txtInquiryNo.Name = "txtInquiryNo"
        Me.txtInquiryNo.Size = New System.Drawing.Size(145, 22)
        Me.txtInquiryNo.TabIndex = 21
        '
        'Label46
        '
        Me.Label46.AutoSize = True
        Me.Label46.Location = New System.Drawing.Point(760, 37)
        Me.Label46.Name = "Label46"
        Me.Label46.Size = New System.Drawing.Size(136, 17)
        Me.Label46.TabIndex = 20
        Me.Label46.Text = "Customer Inquiry No"
        '
        'txtCustomerInquiryNo
        '
        Me.txtCustomerInquiryNo.Location = New System.Drawing.Point(893, 31)
        Me.txtCustomerInquiryNo.Name = "txtCustomerInquiryNo"
        Me.txtCustomerInquiryNo.Size = New System.Drawing.Size(145, 22)
        Me.txtCustomerInquiryNo.TabIndex = 19
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(963, 88)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 23)
        Me.btnSearch.TabIndex = 18
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(525, 64)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(64, 17)
        Me.Label28.TabIndex = 14
        Me.Label28.Text = "Remarks"
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(525, 37)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(68, 17)
        Me.Label29.TabIndex = 12
        Me.Label29.Text = "Customer"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(244, 31)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(50, 17)
        Me.Label30.TabIndex = 6
        Me.Label30.Text = "SO No"
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(244, 86)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(127, 17)
        Me.Label31.TabIndex = 10
        Me.Label31.Text = "Less Than Amount"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(244, 59)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(139, 17)
        Me.Label32.TabIndex = 8
        Me.Label32.Text = "Larger Than Amount"
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(523, 88)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(67, 17)
        Me.Label33.TabIndex = 16
        Me.Label33.Text = "Company"
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(6, 86)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(94, 17)
        Me.Label34.TabIndex = 4
        Me.Label34.Text = "Document No"
        '
        'txtSearchRemarks
        '
        Me.txtSearchRemarks.Location = New System.Drawing.Point(609, 60)
        Me.txtSearchRemarks.Name = "txtSearchRemarks"
        Me.txtSearchRemarks.Size = New System.Drawing.Size(145, 22)
        Me.txtSearchRemarks.TabIndex = 15
        '
        'txtPurchaseNo
        '
        Me.txtPurchaseNo.Location = New System.Drawing.Point(374, 28)
        Me.txtPurchaseNo.Name = "txtPurchaseNo"
        Me.txtPurchaseNo.Size = New System.Drawing.Size(145, 22)
        Me.txtPurchaseNo.TabIndex = 7
        '
        'cmbSearchLocation
        '
        Me.cmbSearchLocation.FormattingEnabled = True
        Me.cmbSearchLocation.Location = New System.Drawing.Point(609, 85)
        Me.cmbSearchLocation.Name = "cmbSearchLocation"
        Me.cmbSearchLocation.Size = New System.Drawing.Size(145, 24)
        Me.cmbSearchLocation.TabIndex = 17
        '
        'txtFromAmount
        '
        Me.txtFromAmount.Location = New System.Drawing.Point(374, 55)
        Me.txtFromAmount.Name = "txtFromAmount"
        Me.txtFromAmount.Size = New System.Drawing.Size(145, 22)
        Me.txtFromAmount.TabIndex = 9
        '
        'txtToAmount
        '
        Me.txtToAmount.Location = New System.Drawing.Point(374, 82)
        Me.txtToAmount.Name = "txtToAmount"
        Me.txtToAmount.Size = New System.Drawing.Size(145, 22)
        Me.txtToAmount.TabIndex = 11
        '
        'txtSearchDocNo
        '
        Me.txtSearchDocNo.Location = New System.Drawing.Point(93, 82)
        Me.txtSearchDocNo.Name = "txtSearchDocNo"
        Me.txtSearchDocNo.Size = New System.Drawing.Size(145, 22)
        Me.txtSearchDocNo.TabIndex = 5
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(6, 59)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(59, 17)
        Me.Label35.TabIndex = 2
        Me.Label35.Text = "To Date"
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(6, 32)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(74, 17)
        Me.Label36.TabIndex = 0
        Me.Label36.Text = "From Date"
        '
        'dtpFrom
        '
        Me.dtpFrom.Checked = False
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(93, 28)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.ShowCheckBox = True
        Me.dtpFrom.Size = New System.Drawing.Size(145, 22)
        Me.dtpFrom.TabIndex = 1
        '
        'dtpTo
        '
        Me.dtpTo.Checked = False
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(93, 55)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.ShowCheckBox = True
        Me.dtpTo.Size = New System.Drawing.Size(145, 22)
        Me.dtpTo.TabIndex = 3
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.Panel1)
        Me.UltraTabPageControl2.Controls.Add(Me.Panel2)
        Me.UltraTabPageControl2.Controls.Add(Me.CtrlGrdBar3)
        Me.UltraTabPageControl2.Controls.Add(Me.SplitItemDetail)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(0, 0)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(196, 73)
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.pnlhideitem)
        Me.Panel1.Controls.Add(Me.GroupBox7)
        Me.Panel1.Controls.Add(Me.GroupBox9)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.GroupBox3)
        Me.Panel1.Controls.Add(Me.GroupBox6)
        Me.Panel1.Controls.Add(Me.GroupBox4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 44)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(196, 256)
        Me.Panel1.TabIndex = 19
        '
        'pnlhideitem
        '
        Me.pnlhideitem.Controls.Add(Me.txtManualSerialNo)
        Me.pnlhideitem.Controls.Add(Me.Label48)
        Me.pnlhideitem.Controls.Add(Me.Label45)
        Me.pnlhideitem.Controls.Add(Me.lblSubject)
        Me.pnlhideitem.Controls.Add(Me.txtItemPackRate)
        Me.pnlhideitem.Controls.Add(Me.txtSubject)
        Me.pnlhideitem.Controls.Add(Me.Label5)
        Me.pnlhideitem.Controls.Add(Me.Label7)
        Me.pnlhideitem.Controls.Add(Me.txtPackQty)
        Me.pnlhideitem.Controls.Add(Me.cmbItem)
        Me.pnlhideitem.Controls.Add(Me.dtpDeliveryDate)
        Me.pnlhideitem.Controls.Add(Me.txtStock)
        Me.pnlhideitem.Controls.Add(Me.lblDeliveryDate)
        Me.pnlhideitem.Controls.Add(Me.Label6)
        Me.pnlhideitem.Controls.Add(Me.lblStock)
        Me.pnlhideitem.Controls.Add(Me.txtTotalQuantity)
        Me.pnlhideitem.Controls.Add(Me.rdoCode)
        Me.pnlhideitem.Controls.Add(Me.Label42)
        Me.pnlhideitem.Controls.Add(Me.rdoName)
        Me.pnlhideitem.Controls.Add(Me.cmbCategory)
        Me.pnlhideitem.Controls.Add(Me.cmbUnit)
        Me.pnlhideitem.Controls.Add(Me.Label4)
        Me.pnlhideitem.Controls.Add(Me.Label17)
        Me.pnlhideitem.Controls.Add(Me.txtSchemeQty)
        Me.pnlhideitem.Location = New System.Drawing.Point(940, 106)
        Me.pnlhideitem.Name = "pnlhideitem"
        Me.pnlhideitem.Size = New System.Drawing.Size(99, 36)
        Me.pnlhideitem.TabIndex = 8
        Me.pnlhideitem.Visible = False
        '
        'txtManualSerialNo
        '
        Me.txtManualSerialNo.Location = New System.Drawing.Point(139, 186)
        Me.txtManualSerialNo.Name = "txtManualSerialNo"
        Me.txtManualSerialNo.Size = New System.Drawing.Size(174, 22)
        Me.txtManualSerialNo.TabIndex = 10
        '
        'Label48
        '
        Me.Label48.AutoSize = True
        Me.Label48.Location = New System.Drawing.Point(736, 80)
        Me.Label48.Name = "Label48"
        Me.Label48.Size = New System.Drawing.Size(73, 17)
        Me.Label48.TabIndex = 38
        Me.Label48.Text = "Pack Rate"
        Me.Label48.Visible = False
        '
        'Label45
        '
        Me.Label45.AutoSize = True
        Me.Label45.Location = New System.Drawing.Point(60, 193)
        Me.Label45.Name = "Label45"
        Me.Label45.Size = New System.Drawing.Size(70, 17)
        Me.Label45.TabIndex = 8
        Me.Label45.Text = "Serial No."
        '
        'lblSubject
        '
        Me.lblSubject.AutoSize = True
        Me.lblSubject.Location = New System.Drawing.Point(19, 16)
        Me.lblSubject.Name = "lblSubject"
        Me.lblSubject.Size = New System.Drawing.Size(55, 17)
        Me.lblSubject.TabIndex = 4
        Me.lblSubject.Text = "Subject"
        '
        'txtItemPackRate
        '
        Me.txtItemPackRate.Location = New System.Drawing.Point(740, 96)
        Me.txtItemPackRate.Name = "txtItemPackRate"
        Me.txtItemPackRate.ReadOnly = True
        Me.txtItemPackRate.Size = New System.Drawing.Size(86, 22)
        Me.txtItemPackRate.TabIndex = 39
        Me.txtItemPackRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtItemPackRate.Visible = False
        '
        'txtSubject
        '
        Me.txtSubject.Location = New System.Drawing.Point(96, 13)
        Me.txtSubject.Name = "txtSubject"
        Me.txtSubject.Size = New System.Drawing.Size(375, 22)
        Me.txtSubject.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(19, 39)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(62, 17)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Location"
        Me.Label5.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(20, 79)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(33, 17)
        Me.Label7.TabIndex = 11
        Me.Label7.Text = "Unit"
        Me.Label7.Visible = False
        '
        'txtPackQty
        '
        Me.txtPackQty.Location = New System.Drawing.Point(97, 96)
        Me.txtPackQty.Name = "txtPackQty"
        Me.txtPackQty.Size = New System.Drawing.Size(52, 22)
        Me.txtPackQty.TabIndex = 14
        Me.txtPackQty.TabStop = False
        Me.txtPackQty.Visible = False
        '
        'cmbItem
        '
        Me.cmbItem.AlwaysInEditMode = True
        Me.cmbItem.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append
        Me.cmbItem.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbItem.CheckedListSettings.CheckStateMember = ""
        Appearance1.BackColor = System.Drawing.Color.White
        Appearance1.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        Me.cmbItem.DisplayLayout.Appearance = Appearance1
        Me.cmbItem.DisplayLayout.InterBandSpacing = 10
        Me.cmbItem.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbItem.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbItem.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Appearance2.BackColor = System.Drawing.Color.Transparent
        Me.cmbItem.DisplayLayout.Override.CardAreaAppearance = Appearance2
        Me.cmbItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbItem.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance3.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance3.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance3.ForeColor = System.Drawing.Color.White
        Appearance3.TextHAlignAsString = "Left"
        Appearance3.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent
        Me.cmbItem.DisplayLayout.Override.HeaderAppearance = Appearance3
        Me.cmbItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance4.BorderColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbItem.DisplayLayout.Override.RowAppearance = Appearance4
        Appearance27.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance27.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance27.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.cmbItem.DisplayLayout.Override.RowSelectorAppearance = Appearance27
        Me.cmbItem.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbItem.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance28.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance28.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance28.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance28.ForeColor = System.Drawing.Color.Black
        Me.cmbItem.DisplayLayout.Override.SelectedRowAppearance = Appearance28
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
        Me.cmbItem.Location = New System.Drawing.Point(172, 55)
        Me.cmbItem.MaxDropDownItems = 20
        Me.cmbItem.Name = "cmbItem"
        Me.cmbItem.Size = New System.Drawing.Size(296, 25)
        Me.cmbItem.TabIndex = 4
        Me.cmbItem.Visible = False
        '
        'dtpDeliveryDate
        '
        Me.dtpDeliveryDate.Checked = False
        Me.dtpDeliveryDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDeliveryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDeliveryDate.Location = New System.Drawing.Point(139, 162)
        Me.dtpDeliveryDate.Name = "dtpDeliveryDate"
        Me.dtpDeliveryDate.ShowCheckBox = True
        Me.dtpDeliveryDate.Size = New System.Drawing.Size(174, 22)
        Me.dtpDeliveryDate.TabIndex = 7
        '
        'txtStock
        '
        Me.txtStock.Location = New System.Drawing.Point(778, 54)
        Me.txtStock.Name = "txtStock"
        Me.txtStock.ReadOnly = True
        Me.txtStock.Size = New System.Drawing.Size(41, 22)
        Me.txtStock.TabIndex = 10
        Me.txtStock.TabStop = False
        Me.txtStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtStock.Visible = False
        '
        'lblDeliveryDate
        '
        Me.lblDeliveryDate.AutoSize = True
        Me.lblDeliveryDate.Location = New System.Drawing.Point(60, 166)
        Me.lblDeliveryDate.Name = "lblDeliveryDate"
        Me.lblDeliveryDate.Size = New System.Drawing.Size(62, 17)
        Me.lblDeliveryDate.TabIndex = 6
        Me.lblDeliveryDate.Text = "DR Date"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(178, 39)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(34, 17)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "Item"
        Me.Label6.Visible = False
        '
        'lblStock
        '
        Me.lblStock.AutoSize = True
        Me.lblStock.Location = New System.Drawing.Point(775, 39)
        Me.lblStock.Name = "lblStock"
        Me.lblStock.Size = New System.Drawing.Size(43, 17)
        Me.lblStock.TabIndex = 9
        Me.lblStock.Text = "Stock"
        Me.lblStock.Visible = False
        '
        'txtTotalQuantity
        '
        Me.txtTotalQuantity.Location = New System.Drawing.Point(234, 96)
        Me.txtTotalQuantity.Name = "txtTotalQuantity"
        Me.txtTotalQuantity.Size = New System.Drawing.Size(78, 22)
        Me.txtTotalQuantity.TabIndex = 18
        Me.txtTotalQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'rdoCode
        '
        Me.rdoCode.AutoSize = True
        Me.rdoCode.Checked = True
        Me.rdoCode.Location = New System.Drawing.Point(273, 38)
        Me.rdoCode.Name = "rdoCode"
        Me.rdoCode.Size = New System.Drawing.Size(62, 21)
        Me.rdoCode.TabIndex = 5
        Me.rdoCode.TabStop = True
        Me.rdoCode.Text = "Code"
        Me.rdoCode.UseVisualStyleBackColor = True
        Me.rdoCode.Visible = False
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(231, 80)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(66, 17)
        Me.Label42.TabIndex = 17
        Me.Label42.Text = "Total Qty"
        '
        'rdoName
        '
        Me.rdoName.AutoSize = True
        Me.rdoName.Location = New System.Drawing.Point(366, 38)
        Me.rdoName.Name = "rdoName"
        Me.rdoName.Size = New System.Drawing.Size(66, 21)
        Me.rdoName.TabIndex = 6
        Me.rdoName.Text = "Name"
        Me.rdoName.UseVisualStyleBackColor = True
        Me.rdoName.Visible = False
        '
        'cmbCategory
        '
        Me.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategory.FormattingEnabled = True
        Me.cmbCategory.Location = New System.Drawing.Point(22, 55)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(144, 25)
        Me.cmbCategory.TabIndex = 2
        Me.cmbCategory.Visible = False
        '
        'cmbUnit
        '
        Me.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUnit.FormattingEnabled = True
        Me.cmbUnit.Items.AddRange(New Object() {"Loose", "Pack"})
        Me.cmbUnit.Location = New System.Drawing.Point(23, 96)
        Me.cmbUnit.Name = "cmbUnit"
        Me.cmbUnit.Size = New System.Drawing.Size(68, 25)
        Me.cmbUnit.TabIndex = 12
        Me.cmbUnit.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(94, 79)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 17)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "P Qty"
        Me.Label4.Visible = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(767, 154)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(58, 17)
        Me.Label17.TabIndex = 35
        Me.Label17.Text = "Sch Qty"
        Me.Label17.Visible = False
        '
        'txtSchemeQty
        '
        Me.txtSchemeQty.Location = New System.Drawing.Point(769, 170)
        Me.txtSchemeQty.Name = "txtSchemeQty"
        Me.txtSchemeQty.Size = New System.Drawing.Size(50, 22)
        Me.txtSchemeQty.TabIndex = 36
        Me.txtSchemeQty.Text = "0"
        Me.txtSchemeQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.txtSchemeQty.Visible = False
        '
        'GroupBox7
        '
        Me.GroupBox7.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox7.Controls.Add(Me.txtTenderSrNo)
        Me.GroupBox7.Controls.Add(Me.Label41)
        Me.GroupBox7.Controls.Add(Me.cmbItemDescription)
        Me.GroupBox7.Controls.Add(Me.txtTradePrice)
        Me.GroupBox7.Controls.Add(Me.txtItemRegNo)
        Me.GroupBox7.Controls.Add(Me.Label40)
        Me.GroupBox7.Controls.Add(Me.txtSpecs)
        Me.GroupBox7.Controls.Add(Me.txtBrand)
        Me.GroupBox7.Controls.Add(Me.Label39)
        Me.GroupBox7.Controls.Add(Me.Label38)
        Me.GroupBox7.Controls.Add(Me.Label37)
        Me.GroupBox7.Controls.Add(Me.Label27)
        Me.GroupBox7.Location = New System.Drawing.Point(800, 6)
        Me.GroupBox7.Name = "GroupBox7"
        Me.GroupBox7.Size = New System.Drawing.Size(13888, 253)
        Me.GroupBox7.TabIndex = 0
        Me.GroupBox7.TabStop = False
        Me.GroupBox7.Text = "Item Description"
        Me.GroupBox7.Visible = False
        '
        'txtTenderSrNo
        '
        Me.txtTenderSrNo.Location = New System.Drawing.Point(6, 227)
        Me.txtTenderSrNo.Name = "txtTenderSrNo"
        Me.txtTenderSrNo.Size = New System.Drawing.Size(119, 22)
        Me.txtTenderSrNo.TabIndex = 1
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.Location = New System.Drawing.Point(6, 212)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(94, 17)
        Me.Label41.TabIndex = 0
        Me.Label41.Text = "Tender Sr No"
        '
        'cmbItemDescription
        '
        Me.cmbItemDescription.FormattingEnabled = True
        Me.cmbItemDescription.Location = New System.Drawing.Point(3, 33)
        Me.cmbItemDescription.Name = "cmbItemDescription"
        Me.cmbItemDescription.Size = New System.Drawing.Size(121, 25)
        Me.cmbItemDescription.TabIndex = 1
        '
        'txtTradePrice
        '
        Me.txtTradePrice.Location = New System.Drawing.Point(5, 148)
        Me.txtTradePrice.Name = "txtTradePrice"
        Me.txtTradePrice.Size = New System.Drawing.Size(119, 22)
        Me.txtTradePrice.TabIndex = 7
        '
        'txtItemRegNo
        '
        Me.txtItemRegNo.Location = New System.Drawing.Point(5, 108)
        Me.txtItemRegNo.Name = "txtItemRegNo"
        Me.txtItemRegNo.Size = New System.Drawing.Size(119, 22)
        Me.txtItemRegNo.TabIndex = 5
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(3, 18)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(79, 17)
        Me.Label40.TabIndex = 0
        Me.Label40.Text = "Description"
        '
        'txtSpecs
        '
        Me.txtSpecs.Location = New System.Drawing.Point(3, 68)
        Me.txtSpecs.Name = "txtSpecs"
        Me.txtSpecs.Size = New System.Drawing.Size(119, 22)
        Me.txtSpecs.TabIndex = 3
        '
        'txtBrand
        '
        Me.txtBrand.Location = New System.Drawing.Point(5, 188)
        Me.txtBrand.Name = "txtBrand"
        Me.txtBrand.Size = New System.Drawing.Size(119, 22)
        Me.txtBrand.TabIndex = 10
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(6, 132)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(82, 17)
        Me.Label39.TabIndex = 6
        Me.Label39.Text = "Trade Price"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(3, 92)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(60, 17)
        Me.Label38.TabIndex = 4
        Me.Label38.Text = "Reg. No"
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(3, 52)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(95, 17)
        Me.Label37.TabIndex = 2
        Me.Label37.Text = "Specifications"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(6, 172)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(87, 17)
        Me.Label27.TabIndex = 9
        Me.Label27.Text = "Brand Name"
        '
        'GroupBox9
        '
        Me.GroupBox9.Controls.Add(Me.Label57)
        Me.GroupBox9.Controls.Add(Me.cmbCurrency)
        Me.GroupBox9.Controls.Add(Me.txtCurrencyRate)
        Me.GroupBox9.Controls.Add(Me.lblCurrencyRate)
        Me.GroupBox9.Location = New System.Drawing.Point(3, 169)
        Me.GroupBox9.Name = "GroupBox9"
        Me.GroupBox9.Size = New System.Drawing.Size(265, 68)
        Me.GroupBox9.TabIndex = 4
        Me.GroupBox9.TabStop = False
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label57.Location = New System.Drawing.Point(4, 15)
        Me.Label57.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(71, 17)
        Me.Label57.TabIndex = 0
        Me.Label57.Text = "Currency"
        '
        'cmbCurrency
        '
        Me.cmbCurrency.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbCurrency.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCurrency.FormattingEnabled = True
        Me.cmbCurrency.Location = New System.Drawing.Point(85, 12)
        Me.cmbCurrency.Name = "cmbCurrency"
        Me.cmbCurrency.Size = New System.Drawing.Size(174, 25)
        Me.cmbCurrency.TabIndex = 1
        '
        'txtCurrencyRate
        '
        Me.txtCurrencyRate.Location = New System.Drawing.Point(85, 39)
        Me.txtCurrencyRate.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtCurrencyRate.Name = "txtCurrencyRate"
        Me.txtCurrencyRate.Size = New System.Drawing.Size(174, 22)
        Me.txtCurrencyRate.TabIndex = 3
        '
        'lblCurrencyRate
        '
        Me.lblCurrencyRate.AutoSize = True
        Me.lblCurrencyRate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrencyRate.Location = New System.Drawing.Point(4, 42)
        Me.lblCurrencyRate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCurrencyRate.Name = "lblCurrencyRate"
        Me.lblCurrencyRate.Size = New System.Drawing.Size(40, 17)
        Me.lblCurrencyRate.TabIndex = 2
        Me.lblCurrencyRate.Text = "Rate"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lnkLblRevisions)
        Me.GroupBox1.Controls.Add(Me.cmbRevisionNumber)
        Me.GroupBox1.Controls.Add(Me.lblRev)
        Me.GroupBox1.Controls.Add(Me.LnkLoadAll)
        Me.GroupBox1.Controls.Add(Me.ChkApproved)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.dtpPDate)
        Me.GroupBox1.Controls.Add(Me.txtPONo)
        Me.GroupBox1.Controls.Add(Me.Label20)
        Me.GroupBox1.Controls.Add(Me.dtpPODate)
        Me.GroupBox1.Controls.Add(Me.txtCustPONo)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.chkPost)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(265, 161)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'lnkLblRevisions
        '
        Me.lnkLblRevisions.AutoSize = True
        Me.lnkLblRevisions.Location = New System.Drawing.Point(186, 17)
        Me.lnkLblRevisions.Name = "lnkLblRevisions"
        Me.lnkLblRevisions.Size = New System.Drawing.Size(33, 17)
        Me.lnkLblRevisions.TabIndex = 2
        Me.lnkLblRevisions.TabStop = True
        Me.lnkLblRevisions.Text = "Rev"
        '
        'cmbRevisionNumber
        '
        Me.cmbRevisionNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRevisionNumber.FormattingEnabled = True
        Me.cmbRevisionNumber.Location = New System.Drawing.Point(217, 15)
        Me.cmbRevisionNumber.Name = "cmbRevisionNumber"
        Me.cmbRevisionNumber.Size = New System.Drawing.Size(42, 25)
        Me.cmbRevisionNumber.TabIndex = 3
        '
        'lblRev
        '
        Me.lblRev.AutoSize = True
        Me.lblRev.Location = New System.Drawing.Point(185, 18)
        Me.lblRev.Name = "lblRev"
        Me.lblRev.Size = New System.Drawing.Size(33, 17)
        Me.lblRev.TabIndex = 2
        Me.lblRev.Text = "Rev"
        '
        'LnkLoadAll
        '
        Me.LnkLoadAll.AutoSize = True
        Me.LnkLoadAll.Location = New System.Drawing.Point(8, 83)
        Me.LnkLoadAll.Name = "LnkLoadAll"
        Me.LnkLoadAll.Size = New System.Drawing.Size(59, 17)
        Me.LnkLoadAll.TabIndex = 3
        Me.LnkLoadAll.TabStop = True
        Me.LnkLoadAll.Text = "Load All"
        Me.LnkLoadAll.Visible = False
        '
        'ChkApproved
        '
        Me.ChkApproved.AutoSize = True
        Me.ChkApproved.Location = New System.Drawing.Point(145, 120)
        Me.ChkApproved.Name = "ChkApproved"
        Me.ChkApproved.Size = New System.Drawing.Size(91, 21)
        Me.ChkApproved.TabIndex = 0
        Me.ChkApproved.Text = "Approved"
        Me.ChkApproved.UseVisualStyleBackColor = True
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(6, 69)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(84, 17)
        Me.Label22.TabIndex = 11
        Me.Label22.Text = "Inquiry Date"
        '
        'dtpPDate
        '
        Me.dtpPDate.Checked = False
        Me.dtpPDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpPDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPDate.Location = New System.Drawing.Point(85, 69)
        Me.dtpPDate.Name = "dtpPDate"
        Me.dtpPDate.ShowCheckBox = True
        Me.dtpPDate.Size = New System.Drawing.Size(174, 22)
        Me.dtpPDate.TabIndex = 12
        '
        'txtPONo
        '
        Me.txtPONo.Location = New System.Drawing.Point(85, 14)
        Me.txtPONo.Name = "txtPONo"
        Me.txtPONo.ReadOnly = True
        Me.txtPONo.Size = New System.Drawing.Size(95, 22)
        Me.txtPONo.TabIndex = 1
        Me.txtPONo.TabStop = False
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(6, 96)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(76, 17)
        Me.Label20.TabIndex = 14
        Me.Label20.Text = "Inquiry No."
        '
        'dtpPODate
        '
        Me.dtpPODate.CustomFormat = "dd/MM/yyyy"
        Me.dtpPODate.Enabled = False
        Me.dtpPODate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPODate.Location = New System.Drawing.Point(85, 41)
        Me.dtpPODate.Name = "dtpPODate"
        Me.dtpPODate.Size = New System.Drawing.Size(174, 22)
        Me.dtpPODate.TabIndex = 5
        '
        'txtCustPONo
        '
        Me.txtCustPONo.Location = New System.Drawing.Point(85, 96)
        Me.txtCustPONo.Name = "txtCustPONo"
        Me.txtCustPONo.Size = New System.Drawing.Size(174, 22)
        Me.txtCustPONo.TabIndex = 15
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 43)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 17)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Doc Date"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 17)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 17)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Doc No."
        '
        'chkPost
        '
        Me.chkPost.AutoSize = True
        Me.chkPost.Location = New System.Drawing.Point(89, 120)
        Me.chkPost.Name = "chkPost"
        Me.chkPost.Size = New System.Drawing.Size(58, 21)
        Me.chkPost.TabIndex = 16
        Me.chkPost.Text = "Post"
        Me.chkPost.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cmbSalesMan)
        Me.GroupBox3.Controls.Add(Me.Label44)
        Me.GroupBox3.Controls.Add(Me.Label21)
        Me.GroupBox3.Controls.Add(Me.cmbProject)
        Me.GroupBox3.Controls.Add(Me.Label53)
        Me.GroupBox3.Controls.Add(Me.Label25)
        Me.GroupBox3.Controls.Add(Me.txtCustomerAddress)
        Me.GroupBox3.Controls.Add(Me.Label16)
        Me.GroupBox3.Controls.Add(Me.txtCustomerMobile)
        Me.GroupBox3.Controls.Add(Me.cmbCompany)
        Me.GroupBox3.Controls.Add(Me.lblInvType)
        Me.GroupBox3.Controls.Add(Me.cmbVendor)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.txtRemarks)
        Me.GroupBox3.Location = New System.Drawing.Point(274, 3)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(281, 211)
        Me.GroupBox3.TabIndex = 5
        Me.GroupBox3.TabStop = False
        '
        'cmbSalesMan
        '
        Me.cmbSalesMan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSalesMan.FormattingEnabled = True
        Me.cmbSalesMan.Location = New System.Drawing.Point(101, 96)
        Me.cmbSalesMan.Name = "cmbSalesMan"
        Me.cmbSalesMan.Size = New System.Drawing.Size(165, 25)
        Me.cmbSalesMan.TabIndex = 7
        '
        'Label44
        '
        Me.Label44.AutoSize = True
        Me.Label44.Location = New System.Drawing.Point(7, 99)
        Me.Label44.Name = "Label44"
        Me.Label44.Size = New System.Drawing.Size(69, 17)
        Me.Label44.TabIndex = 6
        Me.Label44.Text = "SalesPer:"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(6, 43)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(52, 17)
        Me.Label21.TabIndex = 2
        Me.Label21.Text = "Project"
        '
        'cmbProject
        '
        Me.cmbProject.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProject.FormattingEnabled = True
        Me.cmbProject.Location = New System.Drawing.Point(101, 40)
        Me.cmbProject.Name = "cmbProject"
        Me.cmbProject.Size = New System.Drawing.Size(165, 25)
        Me.cmbProject.TabIndex = 3
        '
        'Label53
        '
        Me.Label53.AutoSize = True
        Me.Label53.Location = New System.Drawing.Point(6, 181)
        Me.Label53.Name = "Label53"
        Me.Label53.Size = New System.Drawing.Size(100, 17)
        Me.Label53.TabIndex = 2
        Me.Label53.Text = "Payment Term"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(6, 154)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(71, 17)
        Me.Label25.TabIndex = 2
        Me.Label25.Text = "Inco Term"
        '
        'txtCustomerAddress
        '
        Me.txtCustomerAddress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.txtCustomerAddress.FormattingEnabled = True
        Me.txtCustomerAddress.Items.AddRange(New Object() {"Credit", "Cash", "Advance", "Advance (100%)", "Quarterly - Credit", "Quarterly - Advance", "Quarterly - Advance (100%)", "Payment at Sight", "50% Advance 50% on Delivery", "Payment on Delivery", "Payment on Invoice Submission", "D/P at Sight"})
        Me.txtCustomerAddress.Location = New System.Drawing.Point(101, 178)
        Me.txtCustomerAddress.Name = "txtCustomerAddress"
        Me.txtCustomerAddress.Size = New System.Drawing.Size(165, 25)
        Me.txtCustomerAddress.TabIndex = 1
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(7, 126)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(64, 17)
        Me.Label16.TabIndex = 8
        Me.Label16.Text = "Remarks"
        '
        'txtCustomerMobile
        '
        Me.txtCustomerMobile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.txtCustomerMobile.FormattingEnabled = True
        Me.txtCustomerMobile.Items.AddRange(New Object() {"EX-Works", "DDU", "DDP"})
        Me.txtCustomerMobile.Location = New System.Drawing.Point(101, 151)
        Me.txtCustomerMobile.Name = "txtCustomerMobile"
        Me.txtCustomerMobile.Size = New System.Drawing.Size(165, 25)
        Me.txtCustomerMobile.TabIndex = 1
        '
        'cmbCompany
        '
        Me.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCompany.FormattingEnabled = True
        Me.cmbCompany.Location = New System.Drawing.Point(101, 13)
        Me.cmbCompany.Name = "cmbCompany"
        Me.cmbCompany.Size = New System.Drawing.Size(165, 25)
        Me.cmbCompany.TabIndex = 1
        '
        'lblInvType
        '
        Me.lblInvType.AutoSize = True
        Me.lblInvType.Location = New System.Drawing.Point(6, 17)
        Me.lblInvType.Name = "lblInvType"
        Me.lblInvType.Size = New System.Drawing.Size(62, 17)
        Me.lblInvType.TabIndex = 0
        Me.lblInvType.Text = "Inv Type"
        '
        'cmbVendor
        '
        Me.cmbVendor.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend
        Me.cmbVendor.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbVendor.CheckedListSettings.CheckStateMember = ""
        Appearance29.BackColor = System.Drawing.Color.White
        Appearance29.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance29.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        Me.cmbVendor.DisplayLayout.Appearance = Appearance29
        Me.cmbVendor.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbVendor.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbVendor.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Appearance11.BackColor = System.Drawing.Color.Transparent
        Me.cmbVendor.DisplayLayout.Override.CardAreaAppearance = Appearance11
        Me.cmbVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbVendor.DisplayLayout.Override.CellPadding = 3
        Me.cmbVendor.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance12.TextHAlignAsString = "Left"
        Me.cmbVendor.DisplayLayout.Override.HeaderAppearance = Appearance12
        Me.cmbVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance30.BorderColor = System.Drawing.Color.LightGray
        Appearance30.TextVAlignAsString = "Middle"
        Me.cmbVendor.DisplayLayout.Override.RowAppearance = Appearance30
        Appearance31.BackColor = System.Drawing.Color.LightSteelBlue
        Appearance31.BorderColor = System.Drawing.Color.Black
        Appearance31.ForeColor = System.Drawing.Color.Black
        Me.cmbVendor.DisplayLayout.Override.SelectedRowAppearance = Appearance31
        Me.cmbVendor.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbVendor.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbVendor.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbVendor.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.cmbVendor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbVendor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbVendor.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbVendor.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbVendor.LimitToList = True
        Me.cmbVendor.Location = New System.Drawing.Point(101, 67)
        Me.cmbVendor.Name = "cmbVendor"
        Me.cmbVendor.Size = New System.Drawing.Size(165, 25)
        Me.cmbVendor.TabIndex = 5
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 72)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(68, 17)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "Customer"
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(101, 123)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(165, 22)
        Me.txtRemarks.TabIndex = 9
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.chkAddItemDescription)
        Me.GroupBox6.Controls.Add(Me.Label26)
        Me.GroupBox6.Controls.Add(Me.txtNewCustomer)
        Me.GroupBox6.Controls.Add(Me.Label15)
        Me.GroupBox6.Location = New System.Drawing.Point(274, 233)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(474, 57)
        Me.GroupBox6.TabIndex = 6
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "New Customer Information"
        Me.GroupBox6.Visible = False
        '
        'chkAddItemDescription
        '
        Me.chkAddItemDescription.AutoSize = True
        Me.chkAddItemDescription.Location = New System.Drawing.Point(65, 77)
        Me.chkAddItemDescription.Name = "chkAddItemDescription"
        Me.chkAddItemDescription.Size = New System.Drawing.Size(263, 21)
        Me.chkAddItemDescription.TabIndex = 6
        Me.chkAddItemDescription.Text = "Do you want to add item description?"
        Me.chkAddItemDescription.UseVisualStyleBackColor = True
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(6, 53)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(60, 17)
        Me.Label26.TabIndex = 4
        Me.Label26.Text = "Address"
        '
        'txtNewCustomer
        '
        Me.txtNewCustomer.Location = New System.Drawing.Point(65, 23)
        Me.txtNewCustomer.Name = "txtNewCustomer"
        Me.txtNewCustomer.Size = New System.Drawing.Size(163, 22)
        Me.txtNewCustomer.TabIndex = 1
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(6, 26)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(45, 17)
        Me.Label15.TabIndex = 0
        Me.Label15.Text = "Name"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.txtPackRate)
        Me.GroupBox4.Controls.Add(Me.lblProgress)
        Me.GroupBox4.Controls.Add(Me.Label23)
        Me.GroupBox4.Controls.Add(Me.rbtAdjPercentage)
        Me.GroupBox4.Controls.Add(Me.rbtAdjFlat)
        Me.GroupBox4.Controls.Add(Me.txtSpecialAdjustment)
        Me.GroupBox4.Controls.Add(Me.Label18)
        Me.GroupBox4.Controls.Add(Me.Label11)
        Me.GroupBox4.Controls.Add(Me.txtNetBill)
        Me.GroupBox4.Location = New System.Drawing.Point(566, 2)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(227, 136)
        Me.GroupBox4.TabIndex = 7
        Me.GroupBox4.TabStop = False
        '
        'txtPackRate
        '
        Me.txtPackRate.Location = New System.Drawing.Point(84, 100)
        Me.txtPackRate.Name = "txtPackRate"
        Me.txtPackRate.Size = New System.Drawing.Size(134, 22)
        Me.txtPackRate.TabIndex = 8
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(-13, 52)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 4
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(9, 103)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(73, 17)
        Me.Label23.TabIndex = 7
        Me.Label23.Text = "Pack Rate"
        '
        'rbtAdjPercentage
        '
        Me.rbtAdjPercentage.AutoSize = True
        Me.rbtAdjPercentage.Location = New System.Drawing.Point(129, 41)
        Me.rbtAdjPercentage.Name = "rbtAdjPercentage"
        Me.rbtAdjPercentage.Size = New System.Drawing.Size(102, 21)
        Me.rbtAdjPercentage.TabIndex = 3
        Me.rbtAdjPercentage.Text = "Percentage"
        Me.rbtAdjPercentage.UseVisualStyleBackColor = True
        '
        'rbtAdjFlat
        '
        Me.rbtAdjFlat.AutoSize = True
        Me.rbtAdjFlat.Checked = True
        Me.rbtAdjFlat.Location = New System.Drawing.Point(83, 41)
        Me.rbtAdjFlat.Name = "rbtAdjFlat"
        Me.rbtAdjFlat.Size = New System.Drawing.Size(52, 21)
        Me.rbtAdjFlat.TabIndex = 2
        Me.rbtAdjFlat.TabStop = True
        Me.rbtAdjFlat.Text = "Flat"
        Me.rbtAdjFlat.UseVisualStyleBackColor = True
        '
        'txtSpecialAdjustment
        '
        Me.txtSpecialAdjustment.Location = New System.Drawing.Point(83, 14)
        Me.txtSpecialAdjustment.Name = "txtSpecialAdjustment"
        Me.txtSpecialAdjustment.Size = New System.Drawing.Size(134, 22)
        Me.txtSpecialAdjustment.TabIndex = 1
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(9, 76)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(52, 17)
        Me.Label18.TabIndex = 5
        Me.Label18.Text = "Net Bill"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 17)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(78, 17)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Adjustment"
        '
        'txtNetBill
        '
        Me.txtNetBill.Location = New System.Drawing.Point(84, 73)
        Me.txtNetBill.Name = "txtNetBill"
        Me.txtNetBill.ReadOnly = True
        Me.txtNetBill.Size = New System.Drawing.Size(134, 22)
        Me.txtNetBill.TabIndex = 6
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Label9)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(196, 44)
        Me.Panel2.TabIndex = 18
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.Navy
        Me.Label9.Location = New System.Drawing.Point(11, 9)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(145, 29)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "Quotation"
        '
        'CtrlGrdBar3
        '
        Me.CtrlGrdBar3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar3.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.CtrlGrdBar3.Email = Nothing
        Me.CtrlGrdBar3.FormName = Nothing
        Me.CtrlGrdBar3.Location = New System.Drawing.Point(1424, -1)
        Me.CtrlGrdBar3.MyGrid = Me.GridEX1
        Me.CtrlGrdBar3.Name = "CtrlGrdBar3"
        Me.CtrlGrdBar3.Size = New System.Drawing.Size(34, 25)
        Me.CtrlGrdBar3.TabIndex = 13
        '
        'GridEX1
        '
        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridEX1.AutoEdit = True
        GridEX1_DesignTimeLayout.LayoutString = resources.GetString("GridEX1_DesignTimeLayout.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.Location = New System.Drawing.Point(-1, 119)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.Size = New System.Drawing.Size(1464, 0)
        Me.GridEX1.TabIndex = 1
        Me.GridEX1.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        Me.GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'SplitItemDetail
        '
        Me.SplitItemDetail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitItemDetail.Location = New System.Drawing.Point(758, 50)
        Me.SplitItemDetail.Name = "SplitItemDetail"
        Me.SplitItemDetail.Size = New System.Drawing.Size(695, 265)
        Me.SplitItemDetail.SplitterDistance = 228
        Me.SplitItemDetail.TabIndex = 8
        '
        'utcItems
        '
        Me.utcItems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.utcItems.Location = New System.Drawing.Point(0, 0)
        Me.utcItems.Name = "utcItems"
        Me.utcItems.SharedControlsPage = Me.UltraTabSharedControlsPage2
        Me.utcItems.Size = New System.Drawing.Size(200, 100)
        Me.utcItems.TabIndex = 0
        '
        'UltraTabSharedControlsPage2
        '
        Me.UltraTabSharedControlsPage2.Location = New System.Drawing.Point(1, 24)
        Me.UltraTabSharedControlsPage2.Name = "UltraTabSharedControlsPage2"
        Me.UltraTabSharedControlsPage2.Size = New System.Drawing.Size(196, 73)
        '
        'cmbSearchAccount
        '
        Me.cmbSearchAccount.CheckedListSettings.CheckStateMember = ""
        Appearance32.BackColor = System.Drawing.Color.White
        Me.cmbSearchAccount.DisplayLayout.Appearance = Appearance32
        Me.cmbSearchAccount.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbSearchAccount.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbSearchAccount.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbSearchAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Appearance33.BackColor = System.Drawing.Color.Transparent
        Me.cmbSearchAccount.DisplayLayout.Override.CardAreaAppearance = Appearance33
        Me.cmbSearchAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbSearchAccount.DisplayLayout.Override.CellPadding = 3
        Me.cmbSearchAccount.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance34.TextHAlignAsString = "Left"
        Me.cmbSearchAccount.DisplayLayout.Override.HeaderAppearance = Appearance34
        Me.cmbSearchAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance35.BorderColor = System.Drawing.Color.LightGray
        Appearance35.TextVAlignAsString = "Middle"
        Me.cmbSearchAccount.DisplayLayout.Override.RowAppearance = Appearance35
        Appearance36.BackColor = System.Drawing.Color.LightSteelBlue
        Appearance36.BorderColor = System.Drawing.Color.Black
        Appearance36.ForeColor = System.Drawing.Color.Black
        Me.cmbSearchAccount.DisplayLayout.Override.SelectedRowAppearance = Appearance36
        Me.cmbSearchAccount.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbSearchAccount.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbSearchAccount.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbSearchAccount.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.cmbSearchAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbSearchAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbSearchAccount.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbSearchAccount.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbSearchAccount.LimitToList = True
        Me.cmbSearchAccount.Location = New System.Drawing.Point(609, 32)
        Me.cmbSearchAccount.Name = "cmbSearchAccount"
        Me.cmbSearchAccount.Size = New System.Drawing.Size(145, 25)
        Me.cmbSearchAccount.TabIndex = 13
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.Vendor)
        Me.GroupBox5.Controls.Add(Me.Customer)
        Me.GroupBox5.Location = New System.Drawing.Point(14, 57)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(288, 28)
        Me.GroupBox5.TabIndex = 26
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Report Type"
        '
        'Vendor
        '
        Me.Vendor.AutoSize = True
        Me.Vendor.Location = New System.Drawing.Point(199, 5)
        Me.Vendor.Name = "Vendor"
        Me.Vendor.Size = New System.Drawing.Size(75, 21)
        Me.Vendor.TabIndex = 1
        Me.Vendor.Text = "Vendor"
        Me.Vendor.UseVisualStyleBackColor = True
        '
        'Customer
        '
        Me.Customer.AutoSize = True
        Me.Customer.Location = New System.Drawing.Point(104, 5)
        Me.Customer.Name = "Customer"
        Me.Customer.Size = New System.Drawing.Size(89, 21)
        Me.Customer.TabIndex = 0
        Me.Customer.Text = "Customer"
        Me.Customer.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(528, 64)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 17)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Customer:"
        '
        'cmbCustomer
        '
        Me.cmbCustomer.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cmbCustomer.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbCustomer.CheckedListSettings.CheckStateMember = ""
        Appearance22.BackColor = System.Drawing.Color.White
        Me.cmbCustomer.DisplayLayout.Appearance = Appearance22
        UltraGridColumn26.Header.VisiblePosition = 0
        UltraGridColumn26.Hidden = True
        UltraGridColumn27.Header.VisiblePosition = 1
        UltraGridColumn27.Width = 141
        UltraGridColumn28.Header.VisiblePosition = 2
        UltraGridColumn29.Header.VisiblePosition = 3
        UltraGridColumn30.Header.VisiblePosition = 4
        UltraGridColumn31.Header.VisiblePosition = 5
        UltraGridColumn31.Hidden = True
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn26, UltraGridColumn27, UltraGridColumn28, UltraGridColumn29, UltraGridColumn30, UltraGridColumn31})
        Me.cmbCustomer.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbCustomer.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbCustomer.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbCustomer.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Appearance23.BackColor = System.Drawing.Color.Transparent
        Me.cmbCustomer.DisplayLayout.Override.CardAreaAppearance = Appearance23
        Me.cmbCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbCustomer.DisplayLayout.Override.CellPadding = 3
        Me.cmbCustomer.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance24.TextHAlignAsString = "Left"
        Me.cmbCustomer.DisplayLayout.Override.HeaderAppearance = Appearance24
        Me.cmbCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance25.BorderColor = System.Drawing.Color.LightGray
        Appearance25.TextVAlignAsString = "Middle"
        Me.cmbCustomer.DisplayLayout.Override.RowAppearance = Appearance25
        Appearance26.BackColor = System.Drawing.Color.LightSteelBlue
        Appearance26.BorderColor = System.Drawing.Color.Black
        Appearance26.ForeColor = System.Drawing.Color.Black
        Me.cmbCustomer.DisplayLayout.Override.SelectedRowAppearance = Appearance26
        Me.cmbCustomer.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbCustomer.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbCustomer.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbCustomer.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.cmbCustomer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbCustomer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbCustomer.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbCustomer.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbCustomer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear
        Me.cmbCustomer.LimitToList = True
        Me.cmbCustomer.Location = New System.Drawing.Point(616, 59)
        Me.cmbCustomer.Name = "cmbCustomer"
        Me.cmbCustomer.Size = New System.Drawing.Size(182, 25)
        Me.cmbCustomer.TabIndex = 28
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(811, 59)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(75, 23)
        Me.btnPrint.TabIndex = 29
        Me.btnPrint.Text = "Print"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'frmInvoiceDueReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1467, 542)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbCustomer)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.pnlHeader)
        Me.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmInvoiceDueReport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Invoice Due Report"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.ResumeLayout(False)
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.pnlhideitem.ResumeLayout(False)
        Me.pnlhideitem.PerformLayout()
        CType(Me.cmbItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox7.ResumeLayout(False)
        Me.GroupBox7.PerformLayout()
        Me.GroupBox9.ResumeLayout(False)
        Me.GroupBox9.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.cmbVendor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SplitItemDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitItemDetail.ResumeLayout(False)
        CType(Me.utcItems, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbSearchAccount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        CType(Me.cmbCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents CtrlGrdBar4 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents grdSaved As Janus.Windows.GridEX.GridEX
    Friend WithEvents ToolStrip2 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnSearchEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSearchPrint As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents SelectedQuotationPrintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintAttachmentQuotationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintQuotationQtyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintQuotationTaxToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CustomPrintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintQuotationItemSpecificationToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintQuotationWithImageToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintDuplicateQuotationToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintSelectedRevisionToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnSearchDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSearchLoadAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSearchDocument As System.Windows.Forms.ToolStripButton
    Friend WithEvents HelpToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtInquiryNo As System.Windows.Forms.TextBox
    Friend WithEvents Label46 As System.Windows.Forms.Label
    Friend WithEvents txtCustomerInquiryNo As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents txtSearchRemarks As System.Windows.Forms.TextBox
    Friend WithEvents txtPurchaseNo As System.Windows.Forms.TextBox
    Friend WithEvents cmbSearchLocation As System.Windows.Forms.ComboBox
    Friend WithEvents txtFromAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtToAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtSearchDocNo As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pnlhideitem As System.Windows.Forms.Panel
    Friend WithEvents txtManualSerialNo As System.Windows.Forms.TextBox
    Friend WithEvents Label48 As System.Windows.Forms.Label
    Friend WithEvents Label45 As System.Windows.Forms.Label
    Friend WithEvents lblSubject As System.Windows.Forms.Label
    Friend WithEvents txtItemPackRate As System.Windows.Forms.TextBox
    Friend WithEvents txtSubject As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtPackQty As System.Windows.Forms.TextBox
    Friend WithEvents cmbItem As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents dtpDeliveryDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtStock As System.Windows.Forms.TextBox
    Friend WithEvents lblDeliveryDate As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblStock As System.Windows.Forms.Label
    Friend WithEvents txtTotalQuantity As System.Windows.Forms.TextBox
    Friend WithEvents rdoCode As System.Windows.Forms.RadioButton
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents rdoName As System.Windows.Forms.RadioButton
    Friend WithEvents cmbCategory As System.Windows.Forms.ComboBox
    Friend WithEvents cmbUnit As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtSchemeQty As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox7 As System.Windows.Forms.GroupBox
    Friend WithEvents txtTenderSrNo As System.Windows.Forms.TextBox
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents cmbItemDescription As System.Windows.Forms.ComboBox
    Friend WithEvents txtTradePrice As System.Windows.Forms.TextBox
    Friend WithEvents txtItemRegNo As System.Windows.Forms.TextBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents txtSpecs As System.Windows.Forms.TextBox
    Friend WithEvents txtBrand As System.Windows.Forms.TextBox
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents GroupBox9 As System.Windows.Forms.GroupBox
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents cmbCurrency As System.Windows.Forms.ComboBox
    Friend WithEvents txtCurrencyRate As System.Windows.Forms.TextBox
    Friend WithEvents lblCurrencyRate As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lnkLblRevisions As System.Windows.Forms.LinkLabel
    Friend WithEvents cmbRevisionNumber As System.Windows.Forms.ComboBox
    Friend WithEvents lblRev As System.Windows.Forms.Label
    Friend WithEvents LnkLoadAll As System.Windows.Forms.LinkLabel
    Friend WithEvents ChkApproved As System.Windows.Forms.CheckBox
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents dtpPDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtPONo As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents dtpPODate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtCustPONo As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkPost As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbSalesMan As System.Windows.Forms.ComboBox
    Friend WithEvents Label44 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents cmbProject As System.Windows.Forms.ComboBox
    Friend WithEvents Label53 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtCustomerAddress As System.Windows.Forms.ComboBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtCustomerMobile As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCompany As System.Windows.Forms.ComboBox
    Friend WithEvents lblInvType As System.Windows.Forms.Label
    Friend WithEvents cmbVendor As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents chkAddItemDescription As System.Windows.Forms.CheckBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtNewCustomer As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents txtPackRate As System.Windows.Forms.TextBox
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents rbtAdjPercentage As System.Windows.Forms.RadioButton
    Friend WithEvents rbtAdjFlat As System.Windows.Forms.RadioButton
    Friend WithEvents txtSpecialAdjustment As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtNetBill As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents CtrlGrdBar3 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents SplitItemDetail As System.Windows.Forms.SplitContainer
    Friend WithEvents utcItems As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage2 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents cmbSearchAccount As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Vendor As System.Windows.Forms.RadioButton
    Friend WithEvents Customer As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbCustomer As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents btnPrint As System.Windows.Forms.Button
End Class

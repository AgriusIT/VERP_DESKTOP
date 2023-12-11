<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStockReceive
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
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn1 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Id")
        Dim UltraGridColumn2 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleDescription")
        Dim UltraGridColumn3 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Size")
        Dim UltraGridColumn4 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Color")
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand2 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn5 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Stock", 0)
        Dim UltraGridColumn6 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("BatchNo", 1)
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmStockReceive))
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.pnlSale = New System.Windows.Forms.Panel()
        Me.lblSalePrice = New System.Windows.Forms.Label()
        Me.lblSaleTotal = New System.Windows.Forms.Label()
        Me.txtSaleTotal = New System.Windows.Forms.TextBox()
        Me.txtSalePrice = New System.Windows.Forms.TextBox()
        Me.pnlCost = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.txtRate = New System.Windows.Forms.TextBox()
        Me.lblTicketNo = New System.Windows.Forms.Label()
        Me.cmbTicket = New System.Windows.Forms.ComboBox()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.lblQty = New System.Windows.Forms.Label()
        Me.txtQty = New System.Windows.Forms.TextBox()
        Me.lblPackQty = New System.Windows.Forms.Label()
        Me.lblPrintStatus = New System.Windows.Forms.Label()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.BtnNew = New System.Windows.Forms.ToolStripButton()
        Me.BtnEdit = New System.Windows.Forms.ToolStripButton()
        Me.BtnSave = New System.Windows.Forms.ToolStripButton()
        Me.BtnDelete = New System.Windows.Forms.ToolStripButton()
        Me.BtnTransferAccounts = New System.Windows.Forms.ToolStripButton()
        Me.BtnPrint = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.BtnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnAddNewItem = New System.Windows.Forms.ToolStripButton()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.tsbAssign1 = New System.Windows.Forms.ToolStripButton()
        Me.tsbConfig = New System.Windows.Forms.ToolStripButton()
        Me.rdoName = New System.Windows.Forms.RadioButton()
        Me.rdoCode = New System.Windows.Forms.RadioButton()
        Me.cmbItem = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.lblStock = New System.Windows.Forms.Label()
        Me.txtStock = New System.Windows.Forms.TextBox()
        Me.cmbBatchNo = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbLocation = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbPo = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtPONo = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.cmbSalesMan = New System.Windows.Forms.ComboBox()
        Me.dtpPODate = New System.Windows.Forms.DateTimePicker()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtReceivingID = New System.Windows.Forms.TextBox()
        Me.lblTotalQty = New System.Windows.Forms.Label()
        Me.txtPackQty = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.cmbCategory = New System.Windows.Forms.ComboBox()
        Me.cmbUnit = New System.Windows.Forms.ComboBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.txtTotalQuantity = New System.Windows.Forms.TextBox()
        Me.txtBatchNo = New System.Windows.Forms.TextBox()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.CtrlGrdBar2 = New SimpleAccounts.CtrlGrdBar()
        Me.grdSaved = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.btnSearchEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSearchPrint = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnSearchDelete = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnUpdateAll = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnSearchLoadAll = New System.Windows.Forms.ToolStripButton()
        Me.btnSearchDocument = New System.Windows.Forms.ToolStripButton()
        Me.HelpToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.tsbAssign = New System.Windows.Forms.ToolStripButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.cmbSearchCostCenter = New System.Windows.Forms.ComboBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtSearchRemarks = New System.Windows.Forms.TextBox()
        Me.txtPurchaseNo = New System.Windows.Forms.TextBox()
        Me.cmbSearchLocation = New System.Windows.Forms.ComboBox()
        Me.txtFromAmount = New System.Windows.Forms.TextBox()
        Me.txtToAmount = New System.Windows.Forms.TextBox()
        Me.txtSearchDocNo = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.txtPaid = New System.Windows.Forms.TextBox()
        Me.txtBalance = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.BackgroundWorker2 = New System.ComponentModel.BackgroundWorker()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.pnlSale.SuspendLayout()
        Me.pnlCost.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.cmbItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbBatchNo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip2.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.pnlSale)
        Me.UltraTabPageControl1.Controls.Add(Me.pnlCost)
        Me.UltraTabPageControl1.Controls.Add(Me.lblTicketNo)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbTicket)
        Me.UltraTabPageControl1.Controls.Add(Me.lblProgress)
        Me.UltraTabPageControl1.Controls.Add(Me.lblQty)
        Me.UltraTabPageControl1.Controls.Add(Me.txtQty)
        Me.UltraTabPageControl1.Controls.Add(Me.lblPackQty)
        Me.UltraTabPageControl1.Controls.Add(Me.lblPrintStatus)
        Me.UltraTabPageControl1.Controls.Add(Me.CtrlGrdBar1)
        Me.UltraTabPageControl1.Controls.Add(Me.ToolStrip1)
        Me.UltraTabPageControl1.Controls.Add(Me.rdoName)
        Me.UltraTabPageControl1.Controls.Add(Me.rdoCode)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbItem)
        Me.UltraTabPageControl1.Controls.Add(Me.lblStock)
        Me.UltraTabPageControl1.Controls.Add(Me.txtStock)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbBatchNo)
        Me.UltraTabPageControl1.Controls.Add(Me.Label1)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbLocation)
        Me.UltraTabPageControl1.Controls.Add(Me.Label2)
        Me.UltraTabPageControl1.Controls.Add(Me.TableLayoutPanel1)
        Me.UltraTabPageControl1.Controls.Add(Me.Label3)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbPo)
        Me.UltraTabPageControl1.Controls.Add(Me.Label4)
        Me.UltraTabPageControl1.Controls.Add(Me.Label16)
        Me.UltraTabPageControl1.Controls.Add(Me.txtPONo)
        Me.UltraTabPageControl1.Controls.Add(Me.txtRemarks)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbSalesMan)
        Me.UltraTabPageControl1.Controls.Add(Me.dtpPODate)
        Me.UltraTabPageControl1.Controls.Add(Me.Label15)
        Me.UltraTabPageControl1.Controls.Add(Me.Label5)
        Me.UltraTabPageControl1.Controls.Add(Me.Label6)
        Me.UltraTabPageControl1.Controls.Add(Me.Label7)
        Me.UltraTabPageControl1.Controls.Add(Me.txtReceivingID)
        Me.UltraTabPageControl1.Controls.Add(Me.lblTotalQty)
        Me.UltraTabPageControl1.Controls.Add(Me.txtPackQty)
        Me.UltraTabPageControl1.Controls.Add(Me.Label17)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbCategory)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbUnit)
        Me.UltraTabPageControl1.Controls.Add(Me.btnAdd)
        Me.UltraTabPageControl1.Controls.Add(Me.txtTotalQuantity)
        Me.UltraTabPageControl1.Controls.Add(Me.txtBatchNo)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(817, 616)
        '
        'pnlHeader
        '
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 25)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(817, 41)
        Me.pnlHeader.TabIndex = 78
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(11, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(182, 23)
        Me.lblHeader.TabIndex = 2
        Me.lblHeader.Text = "Stock Receiving"
        '
        'pnlSale
        '
        Me.pnlSale.Controls.Add(Me.lblSalePrice)
        Me.pnlSale.Controls.Add(Me.lblSaleTotal)
        Me.pnlSale.Controls.Add(Me.txtSaleTotal)
        Me.pnlSale.Controls.Add(Me.txtSalePrice)
        Me.pnlSale.Location = New System.Drawing.Point(237, 241)
        Me.pnlSale.Name = "pnlSale"
        Me.pnlSale.Size = New System.Drawing.Size(200, 42)
        Me.pnlSale.TabIndex = 38
        '
        'lblSalePrice
        '
        Me.lblSalePrice.AutoSize = True
        Me.lblSalePrice.Location = New System.Drawing.Point(5, 3)
        Me.lblSalePrice.Name = "lblSalePrice"
        Me.lblSalePrice.Size = New System.Drawing.Size(64, 13)
        Me.lblSalePrice.TabIndex = 0
        Me.lblSalePrice.Text = "Sale Price"
        '
        'lblSaleTotal
        '
        Me.lblSaleTotal.AutoSize = True
        Me.lblSaleTotal.Location = New System.Drawing.Point(99, 3)
        Me.lblSaleTotal.Name = "lblSaleTotal"
        Me.lblSaleTotal.Size = New System.Drawing.Size(34, 13)
        Me.lblSaleTotal.TabIndex = 2
        Me.lblSaleTotal.Text = "Total"
        '
        'txtSaleTotal
        '
        Me.txtSaleTotal.Location = New System.Drawing.Point(102, 18)
        Me.txtSaleTotal.Name = "txtSaleTotal"
        Me.txtSaleTotal.ReadOnly = True
        Me.txtSaleTotal.Size = New System.Drawing.Size(94, 21)
        Me.txtSaleTotal.TabIndex = 3
        Me.txtSaleTotal.TabStop = False
        Me.txtSaleTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtSaleTotal, "Total Amount")
        '
        'txtSalePrice
        '
        Me.txtSalePrice.Location = New System.Drawing.Point(8, 18)
        Me.txtSalePrice.Name = "txtSalePrice"
        Me.txtSalePrice.Size = New System.Drawing.Size(88, 21)
        Me.txtSalePrice.TabIndex = 1
        Me.txtSalePrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtSalePrice, "Rate")
        '
        'pnlCost
        '
        Me.pnlCost.Controls.Add(Me.Label8)
        Me.pnlCost.Controls.Add(Me.Label9)
        Me.pnlCost.Controls.Add(Me.txtTotal)
        Me.pnlCost.Controls.Add(Me.txtRate)
        Me.pnlCost.Location = New System.Drawing.Point(237, 240)
        Me.pnlCost.Name = "pnlCost"
        Me.pnlCost.Size = New System.Drawing.Size(200, 42)
        Me.pnlCost.TabIndex = 39
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(5, 3)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(33, 13)
        Me.Label8.TabIndex = 3
        Me.Label8.Text = "Rate"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(99, 3)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(34, 13)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Total"
        '
        'txtTotal
        '
        Me.txtTotal.Location = New System.Drawing.Point(102, 18)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.Size = New System.Drawing.Size(94, 21)
        Me.txtTotal.TabIndex = 1
        Me.txtTotal.TabStop = False
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtTotal, "Total Amount")
        '
        'txtRate
        '
        Me.txtRate.Location = New System.Drawing.Point(8, 18)
        Me.txtRate.Name = "txtRate"
        Me.txtRate.Size = New System.Drawing.Size(88, 21)
        Me.txtRate.TabIndex = 0
        Me.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtRate, "Rate")
        '
        'lblTicketNo
        '
        Me.lblTicketNo.AutoSize = True
        Me.lblTicketNo.Location = New System.Drawing.Point(324, 129)
        Me.lblTicketNo.Name = "lblTicketNo"
        Me.lblTicketNo.Size = New System.Drawing.Size(60, 13)
        Me.lblTicketNo.TabIndex = 16
        Me.lblTicketNo.Text = "Ticket No"
        '
        'cmbTicket
        '
        Me.cmbTicket.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTicket.FormattingEnabled = True
        Me.cmbTicket.Location = New System.Drawing.Point(405, 126)
        Me.cmbTicket.Name = "cmbTicket"
        Me.cmbTicket.Size = New System.Drawing.Size(151, 21)
        Me.cmbTicket.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.cmbTicket, "Select ticket no")
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(260, 301)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 42
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'lblQty
        '
        Me.lblQty.AutoSize = True
        Me.lblQty.Location = New System.Drawing.Point(85, 244)
        Me.lblQty.Name = "lblQty"
        Me.lblQty.Size = New System.Drawing.Size(27, 13)
        Me.lblQty.TabIndex = 34
        Me.lblQty.Text = "Qty"
        '
        'txtQty
        '
        Me.txtQty.Location = New System.Drawing.Point(88, 259)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(70, 21)
        Me.txtQty.TabIndex = 35
        Me.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtQty, "Quantity")
        '
        'lblPackQty
        '
        Me.lblPackQty.AutoSize = True
        Me.lblPackQty.Location = New System.Drawing.Point(12, 244)
        Me.lblPackQty.Name = "lblPackQty"
        Me.lblPackQty.Size = New System.Drawing.Size(38, 13)
        Me.lblPackQty.TabIndex = 32
        Me.lblPackQty.Text = "P Qty"
        '
        'lblPrintStatus
        '
        Me.lblPrintStatus.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPrintStatus.AutoSize = True
        Me.lblPrintStatus.Location = New System.Drawing.Point(650, 34)
        Me.lblPrintStatus.Name = "lblPrintStatus"
        Me.lblPrintStatus.Size = New System.Drawing.Size(161, 13)
        Me.lblPrintStatus.TabIndex = 3
        Me.lblPrintStatus.Text = "Print Status : Print Pending"
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(785, 0)
        Me.CtrlGrdBar1.MyGrid = Me.grd
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(33, 25)
        Me.CtrlGrdBar1.TabIndex = 1
        Me.CtrlGrdBar1.TabStop = False
        '
        'grd
        '
        Me.grd.ColumnAutoResize = True
        grd_DesignTimeLayout.LayoutString = resources.GetString("grd_DesignTimeLayout.LayoutString")
        Me.grd.DesignTimeLayout = grd_DesignTimeLayout
        Me.grd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd.GroupByBoxVisible = False
        Me.grd.Location = New System.Drawing.Point(3, 3)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(815, 324)
        Me.grd.TabIndex = 0
        Me.grd.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grd, "Stock Receiving Detail")
        Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BtnNew, Me.BtnEdit, Me.BtnSave, Me.BtnDelete, Me.BtnTransferAccounts, Me.BtnPrint, Me.toolStripSeparator, Me.BtnRefresh, Me.btnAddNewItem, Me.HelpToolStripButton, Me.tsbAssign1, Me.tsbConfig})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(817, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'BtnNew
        '
        Me.BtnNew.Image = CType(resources.GetObject("BtnNew.Image"), System.Drawing.Image)
        Me.BtnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnNew.Name = "BtnNew"
        Me.BtnNew.Size = New System.Drawing.Size(50, 22)
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
        Me.BtnSave.Size = New System.Drawing.Size(50, 22)
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
        Me.BtnTransferAccounts.Size = New System.Drawing.Size(131, 22)
        Me.BtnTransferAccounts.Text = "Transfer to Accounts"
        Me.BtnTransferAccounts.Visible = False
        '
        'BtnPrint
        '
        Me.BtnPrint.Image = CType(resources.GetObject("BtnPrint.Image"), System.Drawing.Image)
        Me.BtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(51, 22)
        Me.BtnPrint.Text = "&Print"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'BtnRefresh
        '
        Me.BtnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.BtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnRefresh.Name = "BtnRefresh"
        Me.BtnRefresh.Size = New System.Drawing.Size(66, 22)
        Me.BtnRefresh.Text = "Refresh"
        '
        'btnAddNewItem
        '
        Me.btnAddNewItem.Image = Global.SimpleAccounts.My.Resources.Resources.Attendanceadd
        Me.btnAddNewItem.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAddNewItem.Name = "btnAddNewItem"
        Me.btnAddNewItem.Size = New System.Drawing.Size(99, 22)
        Me.btnAddNewItem.Text = "Add New Item"
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
        'tsbAssign1
        '
        Me.tsbAssign1.Image = Global.SimpleAccounts.My.Resources.Resources.assign
        Me.tsbAssign1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbAssign1.Name = "tsbAssign1"
        Me.tsbAssign1.Size = New System.Drawing.Size(48, 22)
        Me.tsbAssign1.Text = "Task"
        '
        'tsbConfig
        '
        Me.tsbConfig.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbConfig.Image = Global.SimpleAccounts.My.Resources.Resources.Advanced_Options
        Me.tsbConfig.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbConfig.Name = "tsbConfig"
        Me.tsbConfig.Size = New System.Drawing.Size(23, 22)
        Me.tsbConfig.Text = "Config"
        '
        'rdoName
        '
        Me.rdoName.AutoSize = True
        Me.rdoName.Location = New System.Drawing.Point(224, 194)
        Me.rdoName.Name = "rdoName"
        Me.rdoName.Size = New System.Drawing.Size(58, 17)
        Me.rdoName.TabIndex = 25
        Me.rdoName.Text = "Name"
        Me.ToolTip1.SetToolTip(Me.rdoName, "Search Item By Name")
        Me.rdoName.UseVisualStyleBackColor = True
        '
        'rdoCode
        '
        Me.rdoCode.AutoSize = True
        Me.rdoCode.Checked = True
        Me.rdoCode.Location = New System.Drawing.Point(169, 194)
        Me.rdoCode.Name = "rdoCode"
        Me.rdoCode.Size = New System.Drawing.Size(55, 17)
        Me.rdoCode.TabIndex = 24
        Me.rdoCode.TabStop = True
        Me.rdoCode.Text = "Code"
        Me.ToolTip1.SetToolTip(Me.rdoCode, "Search Item By Code")
        Me.rdoCode.UseVisualStyleBackColor = True
        '
        'cmbItem
        '
        Me.cmbItem.AlwaysInEditMode = True
        Me.cmbItem.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append
        Me.cmbItem.CheckedListSettings.CheckStateMember = ""
        Appearance1.BackColor = System.Drawing.Color.White
        Appearance1.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        Me.cmbItem.DisplayLayout.Appearance = Appearance1
        UltraGridColumn1.Header.VisiblePosition = 0
        UltraGridColumn1.Hidden = True
        UltraGridColumn2.Header.VisiblePosition = 1
        UltraGridColumn3.Header.VisiblePosition = 2
        UltraGridColumn4.Header.VisiblePosition = 3
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn1, UltraGridColumn2, UltraGridColumn3, UltraGridColumn4})
        Me.cmbItem.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
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
        Appearance5.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance5.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.cmbItem.DisplayLayout.Override.RowSelectorAppearance = Appearance5
        Me.cmbItem.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbItem.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance6.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance6.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance6.ForeColor = System.Drawing.Color.Black
        Me.cmbItem.DisplayLayout.Override.SelectedRowAppearance = Appearance6
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
        Me.cmbItem.Location = New System.Drawing.Point(118, 212)
        Me.cmbItem.MaxDropDownItems = 20
        Me.cmbItem.Name = "cmbItem"
        Me.cmbItem.Size = New System.Drawing.Size(190, 23)
        Me.cmbItem.TabIndex = 23
        Me.ToolTip1.SetToolTip(Me.cmbItem, "Select any Item")
        '
        'lblStock
        '
        Me.lblStock.AutoSize = True
        Me.lblStock.Location = New System.Drawing.Point(311, 196)
        Me.lblStock.Name = "lblStock"
        Me.lblStock.Size = New System.Drawing.Size(39, 13)
        Me.lblStock.TabIndex = 28
        Me.lblStock.Text = "Stock"
        '
        'txtStock
        '
        Me.txtStock.Location = New System.Drawing.Point(314, 212)
        Me.txtStock.Name = "txtStock"
        Me.txtStock.ReadOnly = True
        Me.txtStock.Size = New System.Drawing.Size(63, 21)
        Me.txtStock.TabIndex = 29
        Me.txtStock.TabStop = False
        Me.txtStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtStock, "Current Stock")
        '
        'cmbBatchNo
        '
        Me.cmbBatchNo.CheckedListSettings.CheckStateMember = ""
        Appearance7.BackColor = System.Drawing.Color.White
        Appearance7.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        Me.cmbBatchNo.DisplayLayout.Appearance = Appearance7
        UltraGridColumn5.Header.VisiblePosition = 1
        UltraGridColumn5.MaxWidth = 60
        UltraGridColumn6.Header.Caption = "Batch No"
        UltraGridColumn6.Header.VisiblePosition = 0
        UltraGridColumn6.Width = 90
        UltraGridBand2.Columns.AddRange(New Object() {UltraGridColumn5, UltraGridColumn6})
        Me.cmbBatchNo.DisplayLayout.BandsSerializer.Add(UltraGridBand2)
        Me.cmbBatchNo.DisplayLayout.InterBandSpacing = 10
        Me.cmbBatchNo.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbBatchNo.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbBatchNo.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Appearance8.BackColor = System.Drawing.Color.Transparent
        Me.cmbBatchNo.DisplayLayout.Override.CardAreaAppearance = Appearance8
        Me.cmbBatchNo.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbBatchNo.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance9.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance9.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance9.ForeColor = System.Drawing.Color.White
        Appearance9.TextHAlignAsString = "Left"
        Appearance9.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent
        Me.cmbBatchNo.DisplayLayout.Override.HeaderAppearance = Appearance9
        Me.cmbBatchNo.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance10.BorderColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbBatchNo.DisplayLayout.Override.RowAppearance = Appearance10
        Appearance11.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance11.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.cmbBatchNo.DisplayLayout.Override.RowSelectorAppearance = Appearance11
        Me.cmbBatchNo.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbBatchNo.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance12.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance12.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance12.ForeColor = System.Drawing.Color.Black
        Me.cmbBatchNo.DisplayLayout.Override.SelectedRowAppearance = Appearance12
        Me.cmbBatchNo.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbBatchNo.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbBatchNo.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbBatchNo.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbBatchNo.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.cmbBatchNo.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbBatchNo.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbBatchNo.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbBatchNo.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbBatchNo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbBatchNo.LimitToList = True
        Me.cmbBatchNo.Location = New System.Drawing.Point(224, 212)
        Me.cmbBatchNo.MaxDropDownItems = 20
        Me.cmbBatchNo.Name = "cmbBatchNo"
        Me.cmbBatchNo.Size = New System.Drawing.Size(84, 23)
        Me.cmbBatchNo.TabIndex = 27
        Me.cmbBatchNo.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 75)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Document No."
        '
        'cmbLocation
        '
        Me.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLocation.FormattingEnabled = True
        Me.cmbLocation.Location = New System.Drawing.Point(107, 126)
        Me.cmbLocation.Name = "cmbLocation"
        Me.cmbLocation.Size = New System.Drawing.Size(201, 21)
        Me.cmbLocation.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.cmbLocation, "Select Location From")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(322, 75)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 13)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Document Date"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 1
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.grd, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(-2, 288)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 330.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 330.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(821, 330)
        Me.TableLayoutPanel1.TabIndex = 41
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 129)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(87, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "From Location"
        '
        'cmbPo
        '
        Me.cmbPo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPo.FormattingEnabled = True
        Me.cmbPo.Location = New System.Drawing.Point(107, 99)
        Me.cmbPo.Name = "cmbPo"
        Me.cmbPo.Size = New System.Drawing.Size(201, 21)
        Me.cmbPo.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.cmbPo, "Select Dispatch No.")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 156)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(58, 13)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Remarks"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(12, 102)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(79, 13)
        Me.Label16.TabIndex = 6
        Me.Label16.Text = "Dispatch No."
        '
        'txtPONo
        '
        Me.txtPONo.Location = New System.Drawing.Point(107, 72)
        Me.txtPONo.Name = "txtPONo"
        Me.txtPONo.ReadOnly = True
        Me.txtPONo.Size = New System.Drawing.Size(201, 21)
        Me.txtPONo.TabIndex = 5
        Me.txtPONo.TabStop = False
        Me.ToolTip1.SetToolTip(Me.txtPONo, "Document No")
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(107, 153)
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(449, 21)
        Me.txtRemarks.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.txtRemarks, "Remarks for  necessary")
        '
        'cmbSalesMan
        '
        Me.cmbSalesMan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSalesMan.FormattingEnabled = True
        Me.cmbSalesMan.Location = New System.Drawing.Point(405, 99)
        Me.cmbSalesMan.Name = "cmbSalesMan"
        Me.cmbSalesMan.Size = New System.Drawing.Size(151, 21)
        Me.cmbSalesMan.TabIndex = 15
        Me.cmbSalesMan.TabStop = False
        Me.cmbSalesMan.Visible = False
        '
        'dtpPODate
        '
        Me.dtpPODate.CustomFormat = "dd/MM/yyyy"
        Me.dtpPODate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPODate.Location = New System.Drawing.Point(429, 72)
        Me.dtpPODate.Name = "dtpPODate"
        Me.dtpPODate.Size = New System.Drawing.Size(127, 21)
        Me.dtpPODate.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.dtpPODate, "Document Date")
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(324, 102)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(75, 13)
        Me.Label15.TabIndex = 14
        Me.Label15.Text = "Sale Person"
        Me.Label15.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 195)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(70, 13)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "In Location"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(115, 196)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(34, 13)
        Me.Label6.TabIndex = 22
        Me.Label6.Text = "Item"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(379, 196)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(29, 13)
        Me.Label7.TabIndex = 30
        Me.Label7.Text = "Unit"
        '
        'txtReceivingID
        '
        Me.txtReceivingID.Location = New System.Drawing.Point(597, 99)
        Me.txtReceivingID.Name = "txtReceivingID"
        Me.txtReceivingID.Size = New System.Drawing.Size(116, 21)
        Me.txtReceivingID.TabIndex = 19
        Me.txtReceivingID.TabStop = False
        Me.txtReceivingID.Visible = False
        '
        'lblTotalQty
        '
        Me.lblTotalQty.AutoSize = True
        Me.lblTotalQty.Location = New System.Drawing.Point(159, 244)
        Me.lblTotalQty.Name = "lblTotalQty"
        Me.lblTotalQty.Size = New System.Drawing.Size(58, 13)
        Me.lblTotalQty.TabIndex = 36
        Me.lblTotalQty.Text = "Total Qty"
        '
        'txtPackQty
        '
        Me.txtPackQty.Location = New System.Drawing.Point(15, 259)
        Me.txtPackQty.Name = "txtPackQty"
        Me.txtPackQty.Size = New System.Drawing.Size(70, 21)
        Me.txtPackQty.TabIndex = 33
        Me.txtPackQty.TabStop = False
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(221, 198)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(63, 13)
        Me.Label17.TabIndex = 26
        Me.Label17.Text = "Batch No:"
        Me.Label17.Visible = False
        '
        'cmbCategory
        '
        Me.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategory.FormattingEnabled = True
        Me.cmbCategory.Location = New System.Drawing.Point(14, 212)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(100, 21)
        Me.cmbCategory.TabIndex = 21
        Me.ToolTip1.SetToolTip(Me.cmbCategory, "Select any Location")
        '
        'cmbUnit
        '
        Me.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUnit.FormattingEnabled = True
        Me.cmbUnit.Items.AddRange(New Object() {"Loose", "Pack"})
        Me.cmbUnit.Location = New System.Drawing.Point(382, 212)
        Me.cmbUnit.Name = "cmbUnit"
        Me.cmbUnit.Size = New System.Drawing.Size(75, 21)
        Me.cmbUnit.TabIndex = 31
        Me.ToolTip1.SetToolTip(Me.cmbUnit, "Select Unit")
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(443, 258)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(43, 23)
        Me.btnAdd.TabIndex = 40
        Me.btnAdd.Text = "+"
        Me.ToolTip1.SetToolTip(Me.btnAdd, "Add Item To Grid")
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'txtTotalQuantity
        '
        Me.txtTotalQuantity.Location = New System.Drawing.Point(162, 259)
        Me.txtTotalQuantity.Name = "txtTotalQuantity"
        Me.txtTotalQuantity.Size = New System.Drawing.Size(70, 21)
        Me.txtTotalQuantity.TabIndex = 37
        Me.txtTotalQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtTotalQuantity, "Quantity")
        '
        'txtBatchNo
        '
        Me.txtBatchNo.Location = New System.Drawing.Point(597, 72)
        Me.txtBatchNo.Name = "txtBatchNo"
        Me.txtBatchNo.Size = New System.Drawing.Size(116, 21)
        Me.txtBatchNo.TabIndex = 18
        Me.txtBatchNo.TabStop = False
        Me.txtBatchNo.Visible = False
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.CtrlGrdBar2)
        Me.UltraTabPageControl2.Controls.Add(Me.ToolStrip2)
        Me.UltraTabPageControl2.Controls.Add(Me.SplitContainer1)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(817, 616)
        '
        'CtrlGrdBar2
        '
        Me.CtrlGrdBar2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar2.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar2.Email = Nothing
        Me.CtrlGrdBar2.FormName = Me
        Me.CtrlGrdBar2.Location = New System.Drawing.Point(779, 0)
        Me.CtrlGrdBar2.MyGrid = Me.grdSaved
        Me.CtrlGrdBar2.Name = "CtrlGrdBar2"
        Me.CtrlGrdBar2.Size = New System.Drawing.Size(38, 27)
        Me.CtrlGrdBar2.TabIndex = 20
        Me.ToolTip1.SetToolTip(Me.CtrlGrdBar2, "Settings")
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
        Me.grdSaved.Size = New System.Drawing.Size(817, 457)
        Me.grdSaved.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.grdSaved, "Saved Record Detail")
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'ToolStrip2
        '
        Me.ToolStrip2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ToolStrip2.AutoSize = False
        Me.ToolStrip2.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnSearchEdit, Me.btnSearchPrint, Me.toolStripSeparator2, Me.btnSearchDelete, Me.toolStripSeparator3, Me.btnUpdateAll, Me.ToolStripSeparator1, Me.btnSearchLoadAll, Me.btnSearchDocument, Me.HelpToolStripButton1, Me.tsbAssign})
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(779, 25)
        Me.ToolStrip2.TabIndex = 2
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
        Me.btnSearchPrint.Image = CType(resources.GetObject("btnSearchPrint.Image"), System.Drawing.Image)
        Me.btnSearchPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSearchPrint.Name = "btnSearchPrint"
        Me.btnSearchPrint.Size = New System.Drawing.Size(52, 22)
        Me.btnSearchPrint.Text = "&Print"
        '
        'toolStripSeparator2
        '
        Me.toolStripSeparator2.Name = "toolStripSeparator2"
        Me.toolStripSeparator2.Size = New System.Drawing.Size(6, 25)
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
        'btnUpdateAll
        '
        Me.btnUpdateAll.Image = Global.SimpleAccounts.My.Resources.Resources.Warehouse
        Me.btnUpdateAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnUpdateAll.Name = "btnUpdateAll"
        Me.btnUpdateAll.Size = New System.Drawing.Size(82, 22)
        Me.btnUpdateAll.Text = "Update All"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnSearchLoadAll
        '
        Me.btnSearchLoadAll.Image = Global.SimpleAccounts.My.Resources.Resources.sendcontactdetails
        Me.btnSearchLoadAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSearchLoadAll.Name = "btnSearchLoadAll"
        Me.btnSearchLoadAll.Size = New System.Drawing.Size(70, 22)
        Me.btnSearchLoadAll.Text = "Load All"
        '
        'btnSearchDocument
        '
        Me.btnSearchDocument.Image = Global.SimpleAccounts.My.Resources.Resources.search_32
        Me.btnSearchDocument.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSearchDocument.Name = "btnSearchDocument"
        Me.btnSearchDocument.Size = New System.Drawing.Size(62, 22)
        Me.btnSearchDocument.Text = "Search"
        '
        'HelpToolStripButton1
        '
        Me.HelpToolStripButton1.Image = CType(resources.GetObject("HelpToolStripButton1.Image"), System.Drawing.Image)
        Me.HelpToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton1.Name = "HelpToolStripButton1"
        Me.HelpToolStripButton1.Size = New System.Drawing.Size(52, 22)
        Me.HelpToolStripButton1.Text = "He&lp"
        '
        'tsbAssign
        '
        Me.tsbAssign.Image = Global.SimpleAccounts.My.Resources.Resources.assign
        Me.tsbAssign.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbAssign.Name = "tsbAssign"
        Me.tsbAssign.Size = New System.Drawing.Size(50, 22)
        Me.tsbAssign.Text = "Task"
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox6)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.grdSaved)
        Me.SplitContainer1.Size = New System.Drawing.Size(817, 591)
        Me.SplitContainer1.SplitterDistance = 130
        Me.SplitContainer1.TabIndex = 1
        '
        'GroupBox6
        '
        Me.GroupBox6.Controls.Add(Me.cmbSearchCostCenter)
        Me.GroupBox6.Controls.Add(Me.btnSearch)
        Me.GroupBox6.Controls.Add(Me.Label26)
        Me.GroupBox6.Controls.Add(Me.Label25)
        Me.GroupBox6.Controls.Add(Me.Label24)
        Me.GroupBox6.Controls.Add(Me.Label23)
        Me.GroupBox6.Controls.Add(Me.Label22)
        Me.GroupBox6.Controls.Add(Me.Label21)
        Me.GroupBox6.Controls.Add(Me.Label18)
        Me.GroupBox6.Controls.Add(Me.txtSearchRemarks)
        Me.GroupBox6.Controls.Add(Me.txtPurchaseNo)
        Me.GroupBox6.Controls.Add(Me.cmbSearchLocation)
        Me.GroupBox6.Controls.Add(Me.txtFromAmount)
        Me.GroupBox6.Controls.Add(Me.txtToAmount)
        Me.GroupBox6.Controls.Add(Me.txtSearchDocNo)
        Me.GroupBox6.Controls.Add(Me.Label19)
        Me.GroupBox6.Controls.Add(Me.Label27)
        Me.GroupBox6.Controls.Add(Me.dtpFrom)
        Me.GroupBox6.Controls.Add(Me.dtpTo)
        Me.GroupBox6.Location = New System.Drawing.Point(6, 7)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(802, 112)
        Me.GroupBox6.TabIndex = 3
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Document Search"
        '
        'cmbSearchCostCenter
        '
        Me.cmbSearchCostCenter.FormattingEnabled = True
        Me.cmbSearchCostCenter.Location = New System.Drawing.Point(563, 28)
        Me.cmbSearchCostCenter.Name = "cmbSearchCostCenter"
        Me.cmbSearchCostCenter.Size = New System.Drawing.Size(233, 21)
        Me.cmbSearchCostCenter.TabIndex = 19
        Me.ToolTip1.SetToolTip(Me.cmbSearchCostCenter, "Select Any Cost Center")
        Me.cmbSearchCostCenter.Visible = False
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(721, 84)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(75, 23)
        Me.btnSearch.TabIndex = 18
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(479, 64)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(58, 13)
        Me.Label26.TabIndex = 14
        Me.Label26.Text = "Remarks"
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(479, 37)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(76, 13)
        Me.Label25.TabIndex = 12
        Me.Label25.Text = "Cost Center"
        Me.Label25.Visible = False
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(244, 32)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(75, 13)
        Me.Label24.TabIndex = 6
        Me.Label24.Text = "Dispatch No"
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(244, 86)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(112, 13)
        Me.Label23.TabIndex = 10
        Me.Label23.Text = "Less Than Amount"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(244, 59)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(124, 13)
        Me.Label22.TabIndex = 8
        Me.Label22.Text = "Larger Than Amount"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(479, 89)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(54, 13)
        Me.Label21.TabIndex = 16
        Me.Label21.Text = "Location"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(6, 86)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(84, 13)
        Me.Label18.TabIndex = 4
        Me.Label18.Text = "Document No"
        '
        'txtSearchRemarks
        '
        Me.txtSearchRemarks.Location = New System.Drawing.Point(563, 56)
        Me.txtSearchRemarks.Name = "txtSearchRemarks"
        Me.txtSearchRemarks.Size = New System.Drawing.Size(233, 21)
        Me.txtSearchRemarks.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.txtSearchRemarks, "Remarks")
        '
        'txtPurchaseNo
        '
        Me.txtPurchaseNo.Location = New System.Drawing.Point(373, 29)
        Me.txtPurchaseNo.Name = "txtPurchaseNo"
        Me.txtPurchaseNo.Size = New System.Drawing.Size(100, 21)
        Me.txtPurchaseNo.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.txtPurchaseNo, "Dispatch No")
        '
        'cmbSearchLocation
        '
        Me.cmbSearchLocation.FormattingEnabled = True
        Me.cmbSearchLocation.Location = New System.Drawing.Point(563, 82)
        Me.cmbSearchLocation.Name = "cmbSearchLocation"
        Me.cmbSearchLocation.Size = New System.Drawing.Size(144, 21)
        Me.cmbSearchLocation.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.cmbSearchLocation, "Select Any Location")
        '
        'txtFromAmount
        '
        Me.txtFromAmount.Location = New System.Drawing.Point(374, 55)
        Me.txtFromAmount.Name = "txtFromAmount"
        Me.txtFromAmount.Size = New System.Drawing.Size(100, 21)
        Me.txtFromAmount.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.txtFromAmount, "Larger Than Amount")
        '
        'txtToAmount
        '
        Me.txtToAmount.Location = New System.Drawing.Point(374, 82)
        Me.txtToAmount.Name = "txtToAmount"
        Me.txtToAmount.Size = New System.Drawing.Size(100, 21)
        Me.txtToAmount.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.txtToAmount, "Less Than Amount")
        '
        'txtSearchDocNo
        '
        Me.txtSearchDocNo.Location = New System.Drawing.Point(93, 82)
        Me.txtSearchDocNo.Name = "txtSearchDocNo"
        Me.txtSearchDocNo.Size = New System.Drawing.Size(145, 21)
        Me.txtSearchDocNo.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.txtSearchDocNo, "Document No")
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(6, 59)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(51, 13)
        Me.Label19.TabIndex = 2
        Me.Label19.Text = "To Date"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(6, 32)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(67, 13)
        Me.Label27.TabIndex = 0
        Me.Label27.Text = "From Date"
        '
        'dtpFrom
        '
        Me.dtpFrom.Checked = False
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(93, 28)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.ShowCheckBox = True
        Me.dtpFrom.Size = New System.Drawing.Size(145, 21)
        Me.dtpFrom.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.dtpFrom, "From Date")
        '
        'dtpTo
        '
        Me.dtpTo.Checked = False
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(93, 55)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.ShowCheckBox = True
        Me.dtpTo.Size = New System.Drawing.Size(145, 21)
        Me.dtpTo.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.dtpTo, "To Date")
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
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(819, 637)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 1
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Stock Receiving"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "History"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(817, 616)
        '
        'BackgroundWorker1
        '
        '
        'BackgroundWorker2
        '
        '
        'frmStockReceive
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(819, 637)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.txtBalance)
        Me.Controls.Add(Me.txtPaid)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmStockReceive"
        Me.Text = "Stock Receiving"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlSale.ResumeLayout(False)
        Me.pnlSale.PerformLayout()
        Me.pnlCost.ResumeLayout(False)
        Me.pnlCost.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.cmbItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbBatchNo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPONo As System.Windows.Forms.TextBox
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents dtpPODate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblTotalQty As System.Windows.Forms.Label
    Friend WithEvents cmbCategory As System.Windows.Forms.ComboBox
    Friend WithEvents cmbUnit As System.Windows.Forms.ComboBox
    Friend WithEvents txtTotalQuantity As System.Windows.Forms.TextBox
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
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtBatchNo As System.Windows.Forms.TextBox
    Friend WithEvents cmbLocation As System.Windows.Forms.ComboBox
    Friend WithEvents cmbBatchNo As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grdSaved As Janus.Windows.GridEX.GridEX
    Friend WithEvents BtnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents txtStock As System.Windows.Forms.TextBox
    Friend WithEvents lblStock As System.Windows.Forms.Label
    Friend WithEvents rdoCode As System.Windows.Forms.RadioButton
    Friend WithEvents rdoName As System.Windows.Forms.RadioButton
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents btnAddNewItem As System.Windows.Forms.ToolStripButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbSearchCostCenter As System.Windows.Forms.ComboBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtSearchRemarks As System.Windows.Forms.TextBox
    Friend WithEvents txtPurchaseNo As System.Windows.Forms.TextBox
    Friend WithEvents cmbSearchLocation As System.Windows.Forms.ComboBox
    Friend WithEvents txtFromAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtToAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtSearchDocNo As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents CtrlGrdBar2 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents ToolStrip2 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnSearchEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSearchPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnSearchDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSearchLoadAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSearchDocument As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundWorker2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblPrintStatus As System.Windows.Forms.Label
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents btnUpdateAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbAssign1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbAssign As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbConfig As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblQty As System.Windows.Forms.Label
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents lblPackQty As System.Windows.Forms.Label
    Friend WithEvents lblTicketNo As System.Windows.Forms.Label
    Friend WithEvents cmbTicket As System.Windows.Forms.ComboBox
    Friend WithEvents pnlSale As System.Windows.Forms.Panel
    Friend WithEvents lblSalePrice As System.Windows.Forms.Label
    Friend WithEvents lblSaleTotal As System.Windows.Forms.Label
    Friend WithEvents txtSaleTotal As System.Windows.Forms.TextBox
    Friend WithEvents txtSalePrice As System.Windows.Forms.TextBox
    Friend WithEvents pnlCost As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents txtRate As System.Windows.Forms.TextBox
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel

End Class

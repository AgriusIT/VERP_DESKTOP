<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPOSEntry
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPOSEntry))
        Dim grd_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.BtnNew = New System.Windows.Forms.ToolStripButton()
        Me.BtnEdit = New System.Windows.Forms.ToolStripButton()
        Me.BtnSave = New System.Windows.Forms.ToolStripButton()
        Me.BtnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnHold = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.grpDocNo = New System.Windows.Forms.GroupBox()
        Me.lnkLblRevisions = New System.Windows.Forms.LinkLabel()
        Me.cmbRevisionNumber = New System.Windows.Forms.ComboBox()
        Me.cmbPackingMan = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbBillMaker = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbSalesPerson = New System.Windows.Forms.ComboBox()
        Me.lblSalesPerson = New System.Windows.Forms.Label()
        Me.txtDocNo = New System.Windows.Forms.TextBox()
        Me.dtpPOSDate = New System.Windows.Forms.DateTimePicker()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.lblDocNo = New System.Windows.Forms.Label()
        Me.lblRev = New System.Windows.Forms.Label()
        Me.grbItemInfo = New System.Windows.Forms.GroupBox()
        Me.lblItemPrice = New System.Windows.Forms.Label()
        Me.lblItemPackQty = New System.Windows.Forms.Label()
        Me.lblProductName = New System.Windows.Forms.Label()
        Me.lblProductPrice = New System.Windows.Forms.Label()
        Me.lblItemName = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.dtpChequeDate1 = New System.Windows.Forms.DateTimePicker()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.grpCustomerInfo = New System.Windows.Forms.GroupBox()
        Me.txtBalance = New System.Windows.Forms.TextBox()
        Me.txtCreditLimit = New System.Windows.Forms.TextBox()
        Me.txtCNIC = New System.Windows.Forms.TextBox()
        Me.txtCustomer = New System.Windows.Forms.TextBox()
        Me.txtMobile = New System.Windows.Forms.TextBox()
        Me.lblCreditLimit = New System.Windows.Forms.Label()
        Me.lblMobile = New System.Windows.Forms.Label()
        Me.lblCustomer = New System.Windows.Forms.Label()
        Me.lblCNIC = New System.Windows.Forms.Label()
        Me.lblBalance = New System.Windows.Forms.Label()
        Me.grpBlank = New System.Windows.Forms.GroupBox()
        Me.chkAutoLoad = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtStock = New System.Windows.Forms.TextBox()
        Me.txtBardCodeScan = New System.Windows.Forms.TextBox()
        Me.lblItemDescription = New System.Windows.Forms.Label()
        Me.lblTotalQty = New System.Windows.Forms.Label()
        Me.txtTotalQty = New System.Windows.Forms.TextBox()
        Me.lblQty = New System.Windows.Forms.Label()
        Me.txtQty = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblPackQty = New System.Windows.Forms.Label()
        Me.txtPackQty = New System.Windows.Forms.TextBox()
        Me.lblRate = New System.Windows.Forms.Label()
        Me.txtRate = New System.Windows.Forms.TextBox()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.lblRemarks = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.grpCash = New System.Windows.Forms.GroupBox()
        Me.txtDiscount = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtNetTotal = New System.Windows.Forms.TextBox()
        Me.txtTax = New System.Windows.Forms.TextBox()
        Me.txtDisPercentage = New System.Windows.Forms.TextBox()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.lblTax = New System.Windows.Forms.Label()
        Me.lblTotal = New System.Windows.Forms.Label()
        Me.lblDiscount = New System.Windows.Forms.Label()
        Me.lblNetTotal = New System.Windows.Forms.Label()
        Me.grpPayment = New System.Windows.Forms.GroupBox()
        Me.pnlCreditCard = New System.Windows.Forms.Panel()
        Me.lblCreditCardNo = New System.Windows.Forms.Label()
        Me.cmbCCAccount = New System.Windows.Forms.ComboBox()
        Me.txtCreditCardNo = New System.Windows.Forms.TextBox()
        Me.lblCCAccount = New System.Windows.Forms.Label()
        Me.pnlBank = New System.Windows.Forms.Panel()
        Me.chkOnline = New System.Windows.Forms.CheckBox()
        Me.cmbBank = New System.Windows.Forms.ComboBox()
        Me.lblBank = New System.Windows.Forms.Label()
        Me.txtChequeNo = New System.Windows.Forms.TextBox()
        Me.lblChequeDate = New System.Windows.Forms.Label()
        Me.lblChequeNo = New System.Windows.Forms.Label()
        Me.txtPaymentBalance = New System.Windows.Forms.TextBox()
        Me.cmbPayMode = New System.Windows.Forms.ComboBox()
        Me.txtCash = New System.Windows.Forms.TextBox()
        Me.lblPaymentBalance = New System.Windows.Forms.Label()
        Me.lblPayMode = New System.Windows.Forms.Label()
        Me.lblCash = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CtrlGrdBar2 = New SimpleAccounts.CtrlGrdBar()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.chkDirectPrint = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ToolStrip1.SuspendLayout()
        Me.grpDocNo.SuspendLayout()
        Me.grbItemInfo.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCustomerInfo.SuspendLayout()
        Me.grpBlank.SuspendLayout()
        Me.grpCash.SuspendLayout()
        Me.grpPayment.SuspendLayout()
        Me.pnlCreditCard.SuspendLayout()
        Me.pnlBank.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BtnNew, Me.BtnEdit, Me.BtnSave, Me.BtnRefresh, Me.btnHold, Me.btnPrint})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1027, 25)
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
        'BtnRefresh
        '
        Me.BtnRefresh.Image = CType(resources.GetObject("BtnRefresh.Image"), System.Drawing.Image)
        Me.BtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnRefresh.Name = "BtnRefresh"
        Me.BtnRefresh.Size = New System.Drawing.Size(66, 22)
        Me.BtnRefresh.Text = "&Refresh"
        '
        'btnHold
        '
        Me.btnHold.Image = CType(resources.GetObject("btnHold.Image"), System.Drawing.Image)
        Me.btnHold.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHold.Name = "btnHold"
        Me.btnHold.Size = New System.Drawing.Size(53, 22)
        Me.btnHold.Text = "Hold"
        '
        'btnPrint
        '
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(99, 22)
        Me.btnPrint.Text = "&Print Revision"
        '
        'grpDocNo
        '
        Me.grpDocNo.Controls.Add(Me.lnkLblRevisions)
        Me.grpDocNo.Controls.Add(Me.cmbRevisionNumber)
        Me.grpDocNo.Controls.Add(Me.cmbPackingMan)
        Me.grpDocNo.Controls.Add(Me.Label4)
        Me.grpDocNo.Controls.Add(Me.cmbBillMaker)
        Me.grpDocNo.Controls.Add(Me.Label3)
        Me.grpDocNo.Controls.Add(Me.cmbSalesPerson)
        Me.grpDocNo.Controls.Add(Me.lblSalesPerson)
        Me.grpDocNo.Controls.Add(Me.txtDocNo)
        Me.grpDocNo.Controls.Add(Me.dtpPOSDate)
        Me.grpDocNo.Controls.Add(Me.lblDate)
        Me.grpDocNo.Controls.Add(Me.lblDocNo)
        Me.grpDocNo.Controls.Add(Me.lblRev)
        Me.grpDocNo.Location = New System.Drawing.Point(15, 5)
        Me.grpDocNo.Name = "grpDocNo"
        Me.grpDocNo.Size = New System.Drawing.Size(316, 162)
        Me.grpDocNo.TabIndex = 0
        Me.grpDocNo.TabStop = False
        '
        'lnkLblRevisions
        '
        Me.lnkLblRevisions.AutoSize = True
        Me.lnkLblRevisions.Location = New System.Drawing.Point(237, 20)
        Me.lnkLblRevisions.Name = "lnkLblRevisions"
        Me.lnkLblRevisions.Size = New System.Drawing.Size(27, 13)
        Me.lnkLblRevisions.TabIndex = 10
        Me.lnkLblRevisions.TabStop = True
        Me.lnkLblRevisions.Text = "Rev"
        '
        'cmbRevisionNumber
        '
        Me.cmbRevisionNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRevisionNumber.FormattingEnabled = True
        Me.cmbRevisionNumber.Location = New System.Drawing.Point(268, 18)
        Me.cmbRevisionNumber.Name = "cmbRevisionNumber"
        Me.cmbRevisionNumber.Size = New System.Drawing.Size(42, 21)
        Me.cmbRevisionNumber.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.cmbRevisionNumber, "Select Revision ")
        '
        'cmbPackingMan
        '
        Me.cmbPackingMan.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbPackingMan.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbPackingMan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPackingMan.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPackingMan.FormattingEnabled = True
        Me.cmbPackingMan.Location = New System.Drawing.Point(88, 131)
        Me.cmbPackingMan.Name = "cmbPackingMan"
        Me.cmbPackingMan.Size = New System.Drawing.Size(222, 23)
        Me.cmbPackingMan.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.cmbPackingMan, "Select Sales Person")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(13, 134)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 15)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Packing Man"
        '
        'cmbBillMaker
        '
        Me.cmbBillMaker.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbBillMaker.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbBillMaker.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBillMaker.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbBillMaker.FormattingEnabled = True
        Me.cmbBillMaker.Location = New System.Drawing.Point(88, 102)
        Me.cmbBillMaker.Name = "cmbBillMaker"
        Me.cmbBillMaker.Size = New System.Drawing.Size(222, 23)
        Me.cmbBillMaker.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.cmbBillMaker, "Select Sales Person")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(13, 105)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 15)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Bill Maker"
        '
        'cmbSalesPerson
        '
        Me.cmbSalesPerson.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbSalesPerson.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbSalesPerson.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSalesPerson.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbSalesPerson.FormattingEnabled = True
        Me.cmbSalesPerson.Location = New System.Drawing.Point(88, 73)
        Me.cmbSalesPerson.Name = "cmbSalesPerson"
        Me.cmbSalesPerson.Size = New System.Drawing.Size(222, 23)
        Me.cmbSalesPerson.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.cmbSalesPerson, "Select Sales Person")
        '
        'lblSalesPerson
        '
        Me.lblSalesPerson.AutoSize = True
        Me.lblSalesPerson.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesPerson.Location = New System.Drawing.Point(13, 76)
        Me.lblSalesPerson.Name = "lblSalesPerson"
        Me.lblSalesPerson.Size = New System.Drawing.Size(72, 15)
        Me.lblSalesPerson.TabIndex = 4
        Me.lblSalesPerson.Text = "Sales Person"
        '
        'txtDocNo
        '
        Me.txtDocNo.BackColor = System.Drawing.SystemColors.ControlLight
        Me.txtDocNo.Enabled = False
        Me.txtDocNo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDocNo.Location = New System.Drawing.Point(88, 16)
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.ReadOnly = True
        Me.txtDocNo.Size = New System.Drawing.Size(143, 23)
        Me.txtDocNo.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtDocNo, "Document No")
        '
        'dtpPOSDate
        '
        Me.dtpPOSDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpPOSDate.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpPOSDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPOSDate.Location = New System.Drawing.Point(88, 45)
        Me.dtpPOSDate.Name = "dtpPOSDate"
        Me.dtpPOSDate.Size = New System.Drawing.Size(222, 23)
        Me.dtpPOSDate.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.dtpPOSDate, "POS Date")
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.Location = New System.Drawing.Point(13, 49)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(31, 15)
        Me.lblDate.TabIndex = 2
        Me.lblDate.Text = "Date"
        '
        'lblDocNo
        '
        Me.lblDocNo.AutoSize = True
        Me.lblDocNo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocNo.Location = New System.Drawing.Point(13, 20)
        Me.lblDocNo.Name = "lblDocNo"
        Me.lblDocNo.Size = New System.Drawing.Size(50, 15)
        Me.lblDocNo.TabIndex = 0
        Me.lblDocNo.Text = "Doc No."
        '
        'lblRev
        '
        Me.lblRev.AutoSize = True
        Me.lblRev.Location = New System.Drawing.Point(237, 20)
        Me.lblRev.Name = "lblRev"
        Me.lblRev.Size = New System.Drawing.Size(27, 13)
        Me.lblRev.TabIndex = 12
        Me.lblRev.Text = "Rev"
        '
        'grbItemInfo
        '
        Me.grbItemInfo.Controls.Add(Me.lblItemPrice)
        Me.grbItemInfo.Controls.Add(Me.lblItemPackQty)
        Me.grbItemInfo.Controls.Add(Me.lblProductName)
        Me.grbItemInfo.Controls.Add(Me.lblProductPrice)
        Me.grbItemInfo.Controls.Add(Me.lblItemName)
        Me.grbItemInfo.Controls.Add(Me.Label7)
        Me.grbItemInfo.Location = New System.Drawing.Point(337, 257)
        Me.grbItemInfo.Name = "grbItemInfo"
        Me.grbItemInfo.Size = New System.Drawing.Size(485, 71)
        Me.grbItemInfo.TabIndex = 21
        Me.grbItemInfo.TabStop = False
        '
        'lblItemPrice
        '
        Me.lblItemPrice.AutoSize = True
        Me.lblItemPrice.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold)
        Me.lblItemPrice.Location = New System.Drawing.Point(46, 31)
        Me.lblItemPrice.Name = "lblItemPrice"
        Me.lblItemPrice.Size = New System.Drawing.Size(0, 18)
        Me.lblItemPrice.TabIndex = 19
        '
        'lblItemPackQty
        '
        Me.lblItemPackQty.AutoSize = True
        Me.lblItemPackQty.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold)
        Me.lblItemPackQty.Location = New System.Drawing.Point(71, 50)
        Me.lblItemPackQty.Name = "lblItemPackQty"
        Me.lblItemPackQty.Size = New System.Drawing.Size(0, 18)
        Me.lblItemPackQty.TabIndex = 20
        '
        'lblProductName
        '
        Me.lblProductName.AutoSize = True
        Me.lblProductName.Location = New System.Drawing.Point(6, 14)
        Me.lblProductName.Name = "lblProductName"
        Me.lblProductName.Size = New System.Drawing.Size(99, 13)
        Me.lblProductName.TabIndex = 15
        Me.lblProductName.Text = "Last Scanned Item:"
        '
        'lblProductPrice
        '
        Me.lblProductPrice.AutoSize = True
        Me.lblProductPrice.Location = New System.Drawing.Point(6, 34)
        Me.lblProductPrice.Name = "lblProductPrice"
        Me.lblProductPrice.Size = New System.Drawing.Size(34, 13)
        Me.lblProductPrice.TabIndex = 17
        Me.lblProductPrice.Text = "Price:"
        '
        'lblItemName
        '
        Me.lblItemName.AutoSize = True
        Me.lblItemName.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemName.Location = New System.Drawing.Point(111, 11)
        Me.lblItemName.Name = "lblItemName"
        Me.lblItemName.Size = New System.Drawing.Size(0, 18)
        Me.lblItemName.TabIndex = 16
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 52)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(54, 13)
        Me.Label7.TabIndex = 18
        Me.Label7.Text = "Pack Qty:"
        '
        'dtpChequeDate1
        '
        Me.dtpChequeDate1.CustomFormat = "dd/MMM/yy"
        Me.dtpChequeDate1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpChequeDate1.Location = New System.Drawing.Point(200, 30)
        Me.dtpChequeDate1.Name = "dtpChequeDate1"
        Me.dtpChequeDate1.Size = New System.Drawing.Size(78, 20)
        Me.dtpChequeDate1.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.dtpChequeDate1, "Enter Cheque date")
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
        Me.grd.GroupByBoxVisible = False
        Me.grd.Location = New System.Drawing.Point(0, 402)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(1027, 224)
        Me.grd.TabIndex = 6
        Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'grpCustomerInfo
        '
        Me.grpCustomerInfo.Controls.Add(Me.txtBalance)
        Me.grpCustomerInfo.Controls.Add(Me.txtCreditLimit)
        Me.grpCustomerInfo.Controls.Add(Me.txtCNIC)
        Me.grpCustomerInfo.Controls.Add(Me.txtCustomer)
        Me.grpCustomerInfo.Controls.Add(Me.txtMobile)
        Me.grpCustomerInfo.Controls.Add(Me.lblCreditLimit)
        Me.grpCustomerInfo.Controls.Add(Me.lblMobile)
        Me.grpCustomerInfo.Controls.Add(Me.lblCustomer)
        Me.grpCustomerInfo.Controls.Add(Me.lblCNIC)
        Me.grpCustomerInfo.Controls.Add(Me.lblBalance)
        Me.grpCustomerInfo.Location = New System.Drawing.Point(15, 173)
        Me.grpCustomerInfo.Name = "grpCustomerInfo"
        Me.grpCustomerInfo.Size = New System.Drawing.Size(316, 152)
        Me.grpCustomerInfo.TabIndex = 1
        Me.grpCustomerInfo.TabStop = False
        '
        'txtBalance
        '
        Me.txtBalance.BackColor = System.Drawing.SystemColors.ControlLight
        Me.txtBalance.Enabled = False
        Me.txtBalance.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBalance.Location = New System.Drawing.Point(88, 123)
        Me.txtBalance.Name = "txtBalance"
        Me.txtBalance.ReadOnly = True
        Me.txtBalance.Size = New System.Drawing.Size(222, 23)
        Me.txtBalance.TabIndex = 9
        Me.txtBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtBalance, "Customer Previous Balance")
        '
        'txtCreditLimit
        '
        Me.txtCreditLimit.BackColor = System.Drawing.SystemColors.ControlLight
        Me.txtCreditLimit.Enabled = False
        Me.txtCreditLimit.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCreditLimit.Location = New System.Drawing.Point(88, 95)
        Me.txtCreditLimit.Name = "txtCreditLimit"
        Me.txtCreditLimit.ReadOnly = True
        Me.txtCreditLimit.Size = New System.Drawing.Size(222, 23)
        Me.txtCreditLimit.TabIndex = 7
        Me.txtCreditLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtCreditLimit, "Customer Credit Limit")
        '
        'txtCNIC
        '
        Me.txtCNIC.BackColor = System.Drawing.Color.White
        Me.txtCNIC.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCNIC.Location = New System.Drawing.Point(88, 67)
        Me.txtCNIC.Name = "txtCNIC"
        Me.txtCNIC.Size = New System.Drawing.Size(222, 23)
        Me.txtCNIC.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.txtCNIC, "Customer CNIC")
        '
        'txtCustomer
        '
        Me.txtCustomer.BackColor = System.Drawing.Color.White
        Me.txtCustomer.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCustomer.Location = New System.Drawing.Point(88, 39)
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.Size = New System.Drawing.Size(222, 23)
        Me.txtCustomer.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtCustomer, "Customer Name")
        '
        'txtMobile
        '
        Me.txtMobile.BackColor = System.Drawing.Color.White
        Me.txtMobile.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtMobile.Location = New System.Drawing.Point(88, 11)
        Me.txtMobile.Name = "txtMobile"
        Me.txtMobile.Size = New System.Drawing.Size(222, 23)
        Me.txtMobile.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtMobile, "Customer Mobile")
        '
        'lblCreditLimit
        '
        Me.lblCreditLimit.AutoSize = True
        Me.lblCreditLimit.Location = New System.Drawing.Point(13, 100)
        Me.lblCreditLimit.Name = "lblCreditLimit"
        Me.lblCreditLimit.Size = New System.Drawing.Size(58, 13)
        Me.lblCreditLimit.TabIndex = 6
        Me.lblCreditLimit.Text = "Credit Limit"
        '
        'lblMobile
        '
        Me.lblMobile.AutoSize = True
        Me.lblMobile.Location = New System.Drawing.Point(13, 16)
        Me.lblMobile.Name = "lblMobile"
        Me.lblMobile.Size = New System.Drawing.Size(38, 13)
        Me.lblMobile.TabIndex = 0
        Me.lblMobile.Text = "Mobile"
        '
        'lblCustomer
        '
        Me.lblCustomer.AutoSize = True
        Me.lblCustomer.Location = New System.Drawing.Point(13, 44)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(51, 13)
        Me.lblCustomer.TabIndex = 2
        Me.lblCustomer.Text = "Customer"
        '
        'lblCNIC
        '
        Me.lblCNIC.AutoSize = True
        Me.lblCNIC.Location = New System.Drawing.Point(13, 73)
        Me.lblCNIC.Name = "lblCNIC"
        Me.lblCNIC.Size = New System.Drawing.Size(32, 13)
        Me.lblCNIC.TabIndex = 4
        Me.lblCNIC.Text = "CNIC"
        '
        'lblBalance
        '
        Me.lblBalance.AutoSize = True
        Me.lblBalance.Location = New System.Drawing.Point(13, 128)
        Me.lblBalance.Name = "lblBalance"
        Me.lblBalance.Size = New System.Drawing.Size(46, 13)
        Me.lblBalance.TabIndex = 8
        Me.lblBalance.Text = "Balance"
        '
        'grpBlank
        '
        Me.grpBlank.BackColor = System.Drawing.Color.White
        Me.grpBlank.Controls.Add(Me.chkAutoLoad)
        Me.grpBlank.Controls.Add(Me.Label5)
        Me.grpBlank.Controls.Add(Me.txtStock)
        Me.grpBlank.Controls.Add(Me.txtBardCodeScan)
        Me.grpBlank.Controls.Add(Me.lblItemDescription)
        Me.grpBlank.Controls.Add(Me.lblTotalQty)
        Me.grpBlank.Controls.Add(Me.txtTotalQty)
        Me.grpBlank.Controls.Add(Me.lblQty)
        Me.grpBlank.Controls.Add(Me.txtQty)
        Me.grpBlank.Controls.Add(Me.Label2)
        Me.grpBlank.Controls.Add(Me.lblPackQty)
        Me.grpBlank.Controls.Add(Me.txtPackQty)
        Me.grpBlank.Controls.Add(Me.lblRate)
        Me.grpBlank.Controls.Add(Me.txtRate)
        Me.grpBlank.Location = New System.Drawing.Point(337, 5)
        Me.grpBlank.Name = "grpBlank"
        Me.grpBlank.Size = New System.Drawing.Size(347, 142)
        Me.grpBlank.TabIndex = 2
        Me.grpBlank.TabStop = False
        '
        'chkAutoLoad
        '
        Me.chkAutoLoad.AutoSize = True
        Me.chkAutoLoad.Checked = True
        Me.chkAutoLoad.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkAutoLoad.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAutoLoad.Location = New System.Drawing.Point(321, 23)
        Me.chkAutoLoad.Name = "chkAutoLoad"
        Me.chkAutoLoad.Size = New System.Drawing.Size(15, 14)
        Me.chkAutoLoad.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.chkAutoLoad, "Auto Load")
        Me.chkAutoLoad.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(272, 81)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(36, 15)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Stock"
        '
        'txtStock
        '
        Me.txtStock.BackColor = System.Drawing.SystemColors.ControlLight
        Me.txtStock.Enabled = False
        Me.txtStock.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtStock.Location = New System.Drawing.Point(275, 99)
        Me.txtStock.Name = "txtStock"
        Me.txtStock.ReadOnly = True
        Me.txtStock.Size = New System.Drawing.Size(60, 23)
        Me.txtStock.TabIndex = 12
        Me.txtStock.TabStop = False
        Me.txtStock.Text = "0"
        Me.txtStock.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtStock, "Available Stock")
        '
        'txtBardCodeScan
        '
        Me.txtBardCodeScan.BackColor = System.Drawing.Color.White
        Me.txtBardCodeScan.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBardCodeScan.Location = New System.Drawing.Point(11, 19)
        Me.txtBardCodeScan.Name = "txtBardCodeScan"
        Me.txtBardCodeScan.Size = New System.Drawing.Size(304, 25)
        Me.txtBardCodeScan.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.txtBardCodeScan, "BarCode Scan")
        '
        'lblItemDescription
        '
        Me.lblItemDescription.AutoSize = True
        Me.lblItemDescription.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblItemDescription.Location = New System.Drawing.Point(43, 50)
        Me.lblItemDescription.Name = "lblItemDescription"
        Me.lblItemDescription.Size = New System.Drawing.Size(0, 15)
        Me.lblItemDescription.TabIndex = 2
        '
        'lblTotalQty
        '
        Me.lblTotalQty.AutoSize = True
        Me.lblTotalQty.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalQty.Location = New System.Drawing.Point(140, 81)
        Me.lblTotalQty.Name = "lblTotalQty"
        Me.lblTotalQty.Size = New System.Drawing.Size(56, 15)
        Me.lblTotalQty.TabIndex = 7
        Me.lblTotalQty.Text = "Total Qty"
        '
        'txtTotalQty
        '
        Me.txtTotalQty.BackColor = System.Drawing.Color.White
        Me.txtTotalQty.Enabled = False
        Me.txtTotalQty.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalQty.Location = New System.Drawing.Point(143, 99)
        Me.txtTotalQty.Name = "txtTotalQty"
        Me.txtTotalQty.Size = New System.Drawing.Size(60, 23)
        Me.txtTotalQty.TabIndex = 8
        Me.txtTotalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblQty
        '
        Me.lblQty.AutoSize = True
        Me.lblQty.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQty.Location = New System.Drawing.Point(74, 81)
        Me.lblQty.Name = "lblQty"
        Me.lblQty.Size = New System.Drawing.Size(26, 15)
        Me.lblQty.TabIndex = 5
        Me.lblQty.Text = "Qty"
        '
        'txtQty
        '
        Me.txtQty.BackColor = System.Drawing.Color.White
        Me.txtQty.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQty.Location = New System.Drawing.Point(77, 99)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(60, 23)
        Me.txtQty.TabIndex = 6
        Me.txtQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(9, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(34, 15)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Item:"
        '
        'lblPackQty
        '
        Me.lblPackQty.AutoSize = True
        Me.lblPackQty.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPackQty.Location = New System.Drawing.Point(8, 81)
        Me.lblPackQty.Name = "lblPackQty"
        Me.lblPackQty.Size = New System.Drawing.Size(54, 15)
        Me.lblPackQty.TabIndex = 3
        Me.lblPackQty.Text = "Pack Qty"
        '
        'txtPackQty
        '
        Me.txtPackQty.BackColor = System.Drawing.Color.White
        Me.txtPackQty.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPackQty.Location = New System.Drawing.Point(11, 99)
        Me.txtPackQty.Name = "txtPackQty"
        Me.txtPackQty.Size = New System.Drawing.Size(60, 23)
        Me.txtPackQty.TabIndex = 4
        Me.txtPackQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblRate
        '
        Me.lblRate.AutoSize = True
        Me.lblRate.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRate.Location = New System.Drawing.Point(206, 81)
        Me.lblRate.Name = "lblRate"
        Me.lblRate.Size = New System.Drawing.Size(30, 15)
        Me.lblRate.TabIndex = 9
        Me.lblRate.Text = "Rate"
        '
        'txtRate
        '
        Me.txtRate.BackColor = System.Drawing.Color.White
        Me.txtRate.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRate.Location = New System.Drawing.Point(209, 99)
        Me.txtRate.Name = "txtRate"
        Me.txtRate.Size = New System.Drawing.Size(60, 23)
        Me.txtRate.TabIndex = 10
        Me.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(409, 413)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 2
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'lblRemarks
        '
        Me.lblRemarks.AutoSize = True
        Me.lblRemarks.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRemarks.Location = New System.Drawing.Point(8, 10)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(52, 15)
        Me.lblRemarks.TabIndex = 0
        Me.lblRemarks.Text = "Remarks"
        '
        'txtRemarks
        '
        Me.txtRemarks.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRemarks.Location = New System.Drawing.Point(11, 34)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(324, 68)
        Me.txtRemarks.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtRemarks, "Remarks")
        '
        'grpCash
        '
        Me.grpCash.Controls.Add(Me.txtDiscount)
        Me.grpCash.Controls.Add(Me.Label1)
        Me.grpCash.Controls.Add(Me.txtNetTotal)
        Me.grpCash.Controls.Add(Me.txtTax)
        Me.grpCash.Controls.Add(Me.txtDisPercentage)
        Me.grpCash.Controls.Add(Me.txtTotal)
        Me.grpCash.Controls.Add(Me.lblTax)
        Me.grpCash.Controls.Add(Me.lblTotal)
        Me.grpCash.Controls.Add(Me.lblDiscount)
        Me.grpCash.Controls.Add(Me.lblNetTotal)
        Me.grpCash.Location = New System.Drawing.Point(690, 5)
        Me.grpCash.Name = "grpCash"
        Me.grpCash.Size = New System.Drawing.Size(292, 142)
        Me.grpCash.TabIndex = 4
        Me.grpCash.TabStop = False
        '
        'txtDiscount
        '
        Me.txtDiscount.BackColor = System.Drawing.Color.White
        Me.txtDiscount.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDiscount.Location = New System.Drawing.Point(197, 52)
        Me.txtDiscount.Name = "txtDiscount"
        Me.txtDiscount.Size = New System.Drawing.Size(87, 23)
        Me.txtDiscount.TabIndex = 5
        Me.txtDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtDiscount, "Discount in Flat")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(137, 55)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 15)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Discount"
        '
        'txtNetTotal
        '
        Me.txtNetTotal.BackColor = System.Drawing.SystemColors.ControlLight
        Me.txtNetTotal.Enabled = False
        Me.txtNetTotal.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNetTotal.Location = New System.Drawing.Point(74, 112)
        Me.txtNetTotal.Name = "txtNetTotal"
        Me.txtNetTotal.ReadOnly = True
        Me.txtNetTotal.Size = New System.Drawing.Size(210, 23)
        Me.txtNetTotal.TabIndex = 9
        Me.txtNetTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtNetTotal, "Net Amount")
        '
        'txtTax
        '
        Me.txtTax.BackColor = System.Drawing.Color.White
        Me.txtTax.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTax.Location = New System.Drawing.Point(74, 82)
        Me.txtTax.Name = "txtTax"
        Me.txtTax.ReadOnly = True
        Me.txtTax.Size = New System.Drawing.Size(210, 23)
        Me.txtTax.TabIndex = 7
        Me.txtTax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtTax, "Tax in Percentage")
        '
        'txtDisPercentage
        '
        Me.txtDisPercentage.BackColor = System.Drawing.Color.White
        Me.txtDisPercentage.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDisPercentage.Location = New System.Drawing.Point(74, 50)
        Me.txtDisPercentage.Name = "txtDisPercentage"
        Me.txtDisPercentage.Size = New System.Drawing.Size(45, 23)
        Me.txtDisPercentage.TabIndex = 3
        Me.txtDisPercentage.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtDisPercentage, "Discount in Percentage")
        '
        'txtTotal
        '
        Me.txtTotal.BackColor = System.Drawing.SystemColors.ControlLight
        Me.txtTotal.Enabled = False
        Me.txtTotal.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotal.Location = New System.Drawing.Point(74, 22)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.Size = New System.Drawing.Size(210, 23)
        Me.txtTotal.TabIndex = 1
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtTotal, "Total Amount")
        '
        'lblTax
        '
        Me.lblTax.AutoSize = True
        Me.lblTax.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTax.Location = New System.Drawing.Point(9, 82)
        Me.lblTax.Name = "lblTax"
        Me.lblTax.Size = New System.Drawing.Size(25, 15)
        Me.lblTax.TabIndex = 6
        Me.lblTax.Text = "Tax"
        '
        'lblTotal
        '
        Me.lblTotal.AutoSize = True
        Me.lblTotal.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(9, 26)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.Size = New System.Drawing.Size(34, 15)
        Me.lblTotal.TabIndex = 0
        Me.lblTotal.Text = "Total"
        '
        'lblDiscount
        '
        Me.lblDiscount.AutoSize = True
        Me.lblDiscount.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDiscount.Location = New System.Drawing.Point(9, 54)
        Me.lblDiscount.Name = "lblDiscount"
        Me.lblDiscount.Size = New System.Drawing.Size(33, 15)
        Me.lblDiscount.TabIndex = 2
        Me.lblDiscount.Text = "Dis%"
        '
        'lblNetTotal
        '
        Me.lblNetTotal.AutoSize = True
        Me.lblNetTotal.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetTotal.Location = New System.Drawing.Point(9, 117)
        Me.lblNetTotal.Name = "lblNetTotal"
        Me.lblNetTotal.Size = New System.Drawing.Size(56, 15)
        Me.lblNetTotal.TabIndex = 8
        Me.lblNetTotal.Text = "Net Total"
        '
        'grpPayment
        '
        Me.grpPayment.Controls.Add(Me.pnlCreditCard)
        Me.grpPayment.Controls.Add(Me.pnlBank)
        Me.grpPayment.Controls.Add(Me.txtPaymentBalance)
        Me.grpPayment.Controls.Add(Me.cmbPayMode)
        Me.grpPayment.Controls.Add(Me.txtCash)
        Me.grpPayment.Controls.Add(Me.lblPaymentBalance)
        Me.grpPayment.Controls.Add(Me.lblPayMode)
        Me.grpPayment.Controls.Add(Me.lblCash)
        Me.grpPayment.Location = New System.Drawing.Point(690, 146)
        Me.grpPayment.Name = "grpPayment"
        Me.grpPayment.Size = New System.Drawing.Size(292, 109)
        Me.grpPayment.TabIndex = 5
        Me.grpPayment.TabStop = False
        '
        'pnlCreditCard
        '
        Me.pnlCreditCard.Controls.Add(Me.lblCreditCardNo)
        Me.pnlCreditCard.Controls.Add(Me.cmbCCAccount)
        Me.pnlCreditCard.Controls.Add(Me.txtCreditCardNo)
        Me.pnlCreditCard.Controls.Add(Me.lblCCAccount)
        Me.pnlCreditCard.Location = New System.Drawing.Point(6, 36)
        Me.pnlCreditCard.Name = "pnlCreditCard"
        Me.pnlCreditCard.Size = New System.Drawing.Size(280, 67)
        Me.pnlCreditCard.TabIndex = 6
        '
        'lblCreditCardNo
        '
        Me.lblCreditCardNo.AutoSize = True
        Me.lblCreditCardNo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCreditCardNo.Location = New System.Drawing.Point(9, 42)
        Me.lblCreditCardNo.Name = "lblCreditCardNo"
        Me.lblCreditCardNo.Size = New System.Drawing.Size(55, 15)
        Me.lblCreditCardNo.TabIndex = 2
        Me.lblCreditCardNo.Text = "Trans No"
        '
        'cmbCCAccount
        '
        Me.cmbCCAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCCAccount.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCCAccount.FormattingEnabled = True
        Me.cmbCCAccount.Items.AddRange(New Object() {"Cash", "Credit", "Bank", "Credit Card", "Mix"})
        Me.cmbCCAccount.Location = New System.Drawing.Point(69, 6)
        Me.cmbCCAccount.Name = "cmbCCAccount"
        Me.cmbCCAccount.Size = New System.Drawing.Size(208, 23)
        Me.cmbCCAccount.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbCCAccount, "Select A Credit Card Account")
        '
        'txtCreditCardNo
        '
        Me.txtCreditCardNo.BackColor = System.Drawing.Color.White
        Me.txtCreditCardNo.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCreditCardNo.Location = New System.Drawing.Point(69, 38)
        Me.txtCreditCardNo.Name = "txtCreditCardNo"
        Me.txtCreditCardNo.Size = New System.Drawing.Size(208, 23)
        Me.txtCreditCardNo.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtCreditCardNo, "Enter Transection No")
        '
        'lblCCAccount
        '
        Me.lblCCAccount.AutoSize = True
        Me.lblCCAccount.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCCAccount.Location = New System.Drawing.Point(9, 9)
        Me.lblCCAccount.Name = "lblCCAccount"
        Me.lblCCAccount.Size = New System.Drawing.Size(32, 15)
        Me.lblCCAccount.TabIndex = 0
        Me.lblCCAccount.Text = "Card"
        '
        'pnlBank
        '
        Me.pnlBank.Controls.Add(Me.chkOnline)
        Me.pnlBank.Controls.Add(Me.cmbBank)
        Me.pnlBank.Controls.Add(Me.lblBank)
        Me.pnlBank.Controls.Add(Me.txtChequeNo)
        Me.pnlBank.Controls.Add(Me.dtpChequeDate1)
        Me.pnlBank.Controls.Add(Me.lblChequeDate)
        Me.pnlBank.Controls.Add(Me.lblChequeNo)
        Me.pnlBank.Location = New System.Drawing.Point(5, 39)
        Me.pnlBank.Name = "pnlBank"
        Me.pnlBank.Size = New System.Drawing.Size(281, 55)
        Me.pnlBank.TabIndex = 7
        '
        'chkOnline
        '
        Me.chkOnline.AutoSize = True
        Me.chkOnline.Location = New System.Drawing.Point(226, 7)
        Me.chkOnline.Name = "chkOnline"
        Me.chkOnline.Size = New System.Drawing.Size(56, 17)
        Me.chkOnline.TabIndex = 6
        Me.chkOnline.Text = "Online"
        Me.chkOnline.UseVisualStyleBackColor = True
        '
        'cmbBank
        '
        Me.cmbBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBank.DropDownWidth = 200
        Me.cmbBank.FormattingEnabled = True
        Me.cmbBank.Location = New System.Drawing.Point(69, 6)
        Me.cmbBank.Name = "cmbBank"
        Me.cmbBank.Size = New System.Drawing.Size(151, 21)
        Me.cmbBank.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbBank, "Select a Bank ")
        '
        'lblBank
        '
        Me.lblBank.AutoSize = True
        Me.lblBank.Location = New System.Drawing.Point(4, 9)
        Me.lblBank.Name = "lblBank"
        Me.lblBank.Size = New System.Drawing.Size(32, 13)
        Me.lblBank.TabIndex = 0
        Me.lblBank.Text = "Bank"
        '
        'txtChequeNo
        '
        Me.txtChequeNo.BackColor = System.Drawing.Color.White
        Me.txtChequeNo.Location = New System.Drawing.Point(69, 31)
        Me.txtChequeNo.Name = "txtChequeNo"
        Me.txtChequeNo.Size = New System.Drawing.Size(87, 20)
        Me.txtChequeNo.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtChequeNo, "Enter Cheque No.")
        '
        'lblChequeDate
        '
        Me.lblChequeDate.AutoSize = True
        Me.lblChequeDate.Location = New System.Drawing.Point(162, 33)
        Me.lblChequeDate.Name = "lblChequeDate"
        Me.lblChequeDate.Size = New System.Drawing.Size(33, 13)
        Me.lblChequeDate.TabIndex = 4
        Me.lblChequeDate.Text = "Date:"
        '
        'lblChequeNo
        '
        Me.lblChequeNo.AutoSize = True
        Me.lblChequeNo.Location = New System.Drawing.Point(3, 33)
        Me.lblChequeNo.Name = "lblChequeNo"
        Me.lblChequeNo.Size = New System.Drawing.Size(40, 13)
        Me.lblChequeNo.TabIndex = 2
        Me.lblChequeNo.Text = "Ch No:"
        '
        'txtPaymentBalance
        '
        Me.txtPaymentBalance.BackColor = System.Drawing.SystemColors.ControlLight
        Me.txtPaymentBalance.Enabled = False
        Me.txtPaymentBalance.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtPaymentBalance.Location = New System.Drawing.Point(74, 70)
        Me.txtPaymentBalance.Name = "txtPaymentBalance"
        Me.txtPaymentBalance.ReadOnly = True
        Me.txtPaymentBalance.Size = New System.Drawing.Size(210, 23)
        Me.txtPaymentBalance.TabIndex = 5
        Me.txtPaymentBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtPaymentBalance, "Remaining Balance ")
        '
        'cmbPayMode
        '
        Me.cmbPayMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPayMode.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPayMode.FormattingEnabled = True
        Me.cmbPayMode.Items.AddRange(New Object() {"Cash", "Credit", "Bank", "Credit Card", "Mix"})
        Me.cmbPayMode.Location = New System.Drawing.Point(75, 10)
        Me.cmbPayMode.Name = "cmbPayMode"
        Me.cmbPayMode.Size = New System.Drawing.Size(209, 23)
        Me.cmbPayMode.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbPayMode, "Select a Mode of Payment")
        '
        'txtCash
        '
        Me.txtCash.BackColor = System.Drawing.Color.White
        Me.txtCash.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.txtCash.Location = New System.Drawing.Point(74, 41)
        Me.txtCash.Name = "txtCash"
        Me.txtCash.Size = New System.Drawing.Size(210, 23)
        Me.txtCash.TabIndex = 3
        Me.txtCash.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtCash, "Enter Cash")
        '
        'lblPaymentBalance
        '
        Me.lblPaymentBalance.AutoSize = True
        Me.lblPaymentBalance.Location = New System.Drawing.Point(9, 75)
        Me.lblPaymentBalance.Name = "lblPaymentBalance"
        Me.lblPaymentBalance.Size = New System.Drawing.Size(46, 13)
        Me.lblPaymentBalance.TabIndex = 4
        Me.lblPaymentBalance.Text = "Balance"
        '
        'lblPayMode
        '
        Me.lblPayMode.AutoSize = True
        Me.lblPayMode.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPayMode.Location = New System.Drawing.Point(9, 14)
        Me.lblPayMode.Name = "lblPayMode"
        Me.lblPayMode.Size = New System.Drawing.Size(60, 15)
        Me.lblPayMode.TabIndex = 0
        Me.lblPayMode.Text = "Pay Mode"
        '
        'lblCash
        '
        Me.lblCash.AutoSize = True
        Me.lblCash.Location = New System.Drawing.Point(8, 46)
        Me.lblCash.Name = "lblCash"
        Me.lblCash.Size = New System.Drawing.Size(31, 13)
        Me.lblCash.TabIndex = 2
        Me.lblCash.Text = "Cash"
        '
        'CtrlGrdBar2
        '
        Me.CtrlGrdBar2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar2.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBar2.Email = Nothing
        Me.CtrlGrdBar2.FormName = Me
        Me.CtrlGrdBar2.Location = New System.Drawing.Point(989, 1)
        Me.CtrlGrdBar2.MyGrid = Me.grd
        Me.CtrlGrdBar2.Name = "CtrlGrdBar2"
        Me.CtrlGrdBar2.Size = New System.Drawing.Size(38, 24)
        Me.CtrlGrdBar2.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.CtrlGrdBar2, "Settings")
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnClose)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 25)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1027, 44)
        Me.pnlHeader.TabIndex = 1
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.Transparent
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.btnClose.Location = New System.Drawing.Point(977, 7)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(38, 30)
        Me.btnClose.TabIndex = 1
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.White
        Me.lblHeader.Location = New System.Drawing.Point(12, 7)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(151, 30)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Point Of Sales"
        '
        'chkDirectPrint
        '
        Me.chkDirectPrint.AutoSize = True
        Me.chkDirectPrint.Checked = True
        Me.chkDirectPrint.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkDirectPrint.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDirectPrint.Location = New System.Drawing.Point(260, 9)
        Me.chkDirectPrint.Name = "chkDirectPrint"
        Me.chkDirectPrint.Size = New System.Drawing.Size(85, 19)
        Me.chkDirectPrint.TabIndex = 3
        Me.chkDirectPrint.Text = "Direct Print"
        Me.chkDirectPrint.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkDirectPrint)
        Me.GroupBox1.Controls.Add(Me.txtRemarks)
        Me.GroupBox1.Controls.Add(Me.lblRemarks)
        Me.GroupBox1.Location = New System.Drawing.Point(337, 146)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(347, 109)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.grbItemInfo)
        Me.Panel1.Controls.Add(Me.grpDocNo)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.grpCash)
        Me.Panel1.Controls.Add(Me.grpBlank)
        Me.Panel1.Controls.Add(Me.grpCustomerInfo)
        Me.Panel1.Controls.Add(Me.grpPayment)
        Me.Panel1.Location = New System.Drawing.Point(0, 68)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1027, 328)
        Me.Panel1.TabIndex = 2
        '
        'frmPOSEntry
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1027, 626)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.CtrlGrdBar2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.ToolStrip1)
        Me.KeyPreview = True
        Me.Name = "frmPOSEntry"
        Me.Text = "Point Of Sales Entry"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.grpDocNo.ResumeLayout(False)
        Me.grpDocNo.PerformLayout()
        Me.grbItemInfo.ResumeLayout(False)
        Me.grbItemInfo.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCustomerInfo.ResumeLayout(False)
        Me.grpCustomerInfo.PerformLayout()
        Me.grpBlank.ResumeLayout(False)
        Me.grpBlank.PerformLayout()
        Me.grpCash.ResumeLayout(False)
        Me.grpCash.PerformLayout()
        Me.grpPayment.ResumeLayout(False)
        Me.grpPayment.PerformLayout()
        Me.pnlCreditCard.ResumeLayout(False)
        Me.pnlCreditCard.PerformLayout()
        Me.pnlBank.ResumeLayout(False)
        Me.pnlBank.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents grpDocNo As System.Windows.Forms.GroupBox
    Friend WithEvents cmbSalesPerson As System.Windows.Forms.ComboBox
    Friend WithEvents lblSalesPerson As System.Windows.Forms.Label
    Friend WithEvents txtDocNo As System.Windows.Forms.TextBox
    Friend WithEvents dtpChequeDate1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents lblDocNo As System.Windows.Forms.Label
    Friend WithEvents BtnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents grpCustomerInfo As System.Windows.Forms.GroupBox
    Friend WithEvents txtBalance As System.Windows.Forms.TextBox
    Friend WithEvents txtCreditLimit As System.Windows.Forms.TextBox
    Friend WithEvents txtCNIC As System.Windows.Forms.TextBox
    Friend WithEvents txtMobile As System.Windows.Forms.TextBox
    Friend WithEvents lblCreditLimit As System.Windows.Forms.Label
    Friend WithEvents lblMobile As System.Windows.Forms.Label
    Friend WithEvents lblCustomer As System.Windows.Forms.Label
    Friend WithEvents lblCNIC As System.Windows.Forms.Label
    Friend WithEvents lblBalance As System.Windows.Forms.Label
    Friend WithEvents grpBlank As System.Windows.Forms.GroupBox
    Friend WithEvents lblRemarks As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents grpCash As System.Windows.Forms.GroupBox
    Friend WithEvents txtNetTotal As System.Windows.Forms.TextBox
    Friend WithEvents txtTax As System.Windows.Forms.TextBox
    Friend WithEvents txtDisPercentage As System.Windows.Forms.TextBox
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents lblTax As System.Windows.Forms.Label
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents lblDiscount As System.Windows.Forms.Label
    Friend WithEvents lblNetTotal As System.Windows.Forms.Label
    Friend WithEvents grpPayment As System.Windows.Forms.GroupBox
    Friend WithEvents cmbPayMode As System.Windows.Forms.ComboBox
    Friend WithEvents txtPaymentBalance As System.Windows.Forms.TextBox
    Friend WithEvents txtCash As System.Windows.Forms.TextBox
    Friend WithEvents lblPaymentBalance As System.Windows.Forms.Label
    Friend WithEvents lblPayMode As System.Windows.Forms.Label
    Friend WithEvents lblCash As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents txtDiscount As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents txtBardCodeScan As System.Windows.Forms.TextBox
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents cmbBank As System.Windows.Forms.ComboBox
    Friend WithEvents dtpPOSDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents pnlBank As System.Windows.Forms.Panel
    Friend WithEvents lblBank As System.Windows.Forms.Label
    Friend WithEvents lblChequeDate As System.Windows.Forms.Label
    Friend WithEvents lblChequeNo As System.Windows.Forms.Label
    Friend WithEvents txtChequeNo As System.Windows.Forms.TextBox
    Friend WithEvents pnlCreditCard As System.Windows.Forms.Panel
    Friend WithEvents lblCreditCardNo As System.Windows.Forms.Label
    Friend WithEvents txtCreditCardNo As System.Windows.Forms.TextBox
    Friend WithEvents chkDirectPrint As System.Windows.Forms.CheckBox
    Friend WithEvents cmbCCAccount As System.Windows.Forms.ComboBox
    Friend WithEvents lblCCAccount As System.Windows.Forms.Label
    Friend WithEvents txtCustomer As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkOnline As System.Windows.Forms.CheckBox
    Friend WithEvents btnHold As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblItemDescription As System.Windows.Forms.Label
    Friend WithEvents lblTotalQty As System.Windows.Forms.Label
    Friend WithEvents txtTotalQty As System.Windows.Forms.TextBox
    Friend WithEvents lblQty As System.Windows.Forms.Label
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents lblPackQty As System.Windows.Forms.Label
    Friend WithEvents txtPackQty As System.Windows.Forms.TextBox
    Friend WithEvents lblRate As System.Windows.Forms.Label
    Friend WithEvents txtRate As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbPackingMan As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbBillMaker As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtStock As System.Windows.Forms.TextBox
    Friend WithEvents chkAutoLoad As System.Windows.Forms.CheckBox
    Friend WithEvents lnkLblRevisions As System.Windows.Forms.LinkLabel
    Friend WithEvents cmbRevisionNumber As System.Windows.Forms.ComboBox
    Friend WithEvents lblRev As System.Windows.Forms.Label
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents CtrlGrdBar2 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents lblItemName As System.Windows.Forms.Label
    Friend WithEvents lblProductName As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblProductPrice As System.Windows.Forms.Label
    Friend WithEvents lblItemPackQty As System.Windows.Forms.Label
    Friend WithEvents lblItemPrice As System.Windows.Forms.Label
    Friend WithEvents grbItemInfo As System.Windows.Forms.GroupBox
End Class

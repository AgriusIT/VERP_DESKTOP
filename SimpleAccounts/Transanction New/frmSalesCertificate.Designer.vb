<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSalesCertificate
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
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn7 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ID")
        Dim UltraGridColumn8 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Name")
        Dim UltraGridColumn9 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Territory")
        Dim UltraGridColumn10 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("City")
        Dim UltraGridColumn11 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("State")
        Dim UltraGridColumn12 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("AcId")
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnPrintCertificate = New System.Windows.Forms.ToolStripSplitButton()
        Me.PrintSalesInvoiceWithDealerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintSalesInvoiceToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnAgreementLetter = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbTask = New System.Windows.Forms.ToolStripButton()
        Me.tsbConfig = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtReturned = New System.Windows.Forms.RadioButton()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.txtSearchRemarks = New System.Windows.Forms.TextBox()
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbVendor = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.rbtAll = New System.Windows.Forms.RadioButton()
        Me.rbtPending = New System.Windows.Forms.RadioButton()
        Me.rbtIssued = New System.Windows.Forms.RadioButton()
        Me.grdSaved = New Janus.Windows.GridEX.GridEX()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtChassisNo = New System.Windows.Forms.TextBox()
        Me.txtEngine_No = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnReturn = New System.Windows.Forms.Button()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.txtMeterNo = New System.Windows.Forms.TextBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.dtpContractDate = New System.Windows.Forms.DateTimePicker()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.txtRegistrationNo = New System.Windows.Forms.TextBox()
        Me.txtInstallment = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtAdvanceAmount = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cmbCast = New System.Windows.Forms.ComboBox()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.txtFatherName = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.lblRemarks = New System.Windows.Forms.Label()
        Me.txtDC = New System.Windows.Forms.TextBox()
        Me.lblDcNo = New System.Windows.Forms.Label()
        Me.txtNetAmount = New System.Windows.Forms.TextBox()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.txtColor = New System.Windows.Forms.TextBox()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtReferenceNo = New System.Windows.Forms.TextBox()
        Me.txtTaxPercent = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtRegistrationFor = New System.Windows.Forms.TextBox()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtNTN = New System.Windows.Forms.TextBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.txtSalesTax = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtInvoiceAmount = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtModelCode = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.txtComments = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtBaseWheel = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtRearWheel = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtFrontWheel = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtRearAxel = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtFrontAxl = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtLadenWt = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtModelDesc = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtCrtChassisNo = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtCrtEnginNo = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtDeliveredTo = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dtpCertificateDate = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtCertificateNo = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.CtrlGrdBar3 = New SimpleAccounts.CtrlGrdBar()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.cmbVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnRefresh, Me.ToolStripSeparator1, Me.btnPrintCertificate, Me.ToolStripSeparator2, Me.btnAgreementLetter, Me.ToolStripSeparator3, Me.tsbTask, Me.tsbConfig, Me.ToolStripSeparator4})
        Me.ToolStrip1.Location = New System.Drawing.Point(2, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(890, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = Global.SimpleAccounts.My.Resources.Resources.BtnNew_Image
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(51, 22)
        Me.btnNew.Text = "New"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(66, 22)
        Me.btnRefresh.Text = "Refresh"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnPrintCertificate
        '
        Me.btnPrintCertificate.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrintSalesInvoiceWithDealerToolStripMenuItem, Me.PrintSalesInvoiceToolStripMenuItem})
        Me.btnPrintCertificate.Image = Global.SimpleAccounts.My.Resources.Resources.print
        Me.btnPrintCertificate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrintCertificate.Name = "btnPrintCertificate"
        Me.btnPrintCertificate.Size = New System.Drawing.Size(121, 22)
        Me.btnPrintCertificate.Text = "Print Certificate"
        '
        'PrintSalesInvoiceWithDealerToolStripMenuItem
        '
        Me.PrintSalesInvoiceWithDealerToolStripMenuItem.Name = "PrintSalesInvoiceWithDealerToolStripMenuItem"
        Me.PrintSalesInvoiceWithDealerToolStripMenuItem.Size = New System.Drawing.Size(220, 22)
        Me.PrintSalesInvoiceWithDealerToolStripMenuItem.Text = "Print Certificate With Dealer"
        '
        'PrintSalesInvoiceToolStripMenuItem
        '
        Me.PrintSalesInvoiceToolStripMenuItem.Name = "PrintSalesInvoiceToolStripMenuItem"
        Me.PrintSalesInvoiceToolStripMenuItem.Size = New System.Drawing.Size(220, 22)
        Me.PrintSalesInvoiceToolStripMenuItem.Text = "Print Sales Invoice"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'btnAgreementLetter
        '
        Me.btnAgreementLetter.Image = Global.SimpleAccounts.My.Resources.Resources.print
        Me.btnAgreementLetter.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAgreementLetter.Name = "btnAgreementLetter"
        Me.btnAgreementLetter.Size = New System.Drawing.Size(119, 22)
        Me.btnAgreementLetter.Text = "Agreement Letter"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'tsbTask
        '
        Me.tsbTask.Image = Global.SimpleAccounts.My.Resources.Resources.Untitled_1
        Me.tsbTask.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbTask.Name = "tsbTask"
        Me.tsbTask.Size = New System.Drawing.Size(89, 22)
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
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(8, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(188, 23)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Sales Certificate"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.rbtReturned)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.Label34)
        Me.GroupBox1.Controls.Add(Me.txtSearchRemarks)
        Me.GroupBox1.Controls.Add(Me.txtInvoiceNo)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cmbVendor)
        Me.GroupBox1.Controls.Add(Me.Label33)
        Me.GroupBox1.Controls.Add(Me.rbtAll)
        Me.GroupBox1.Controls.Add(Me.rbtPending)
        Me.GroupBox1.Controls.Add(Me.rbtIssued)
        Me.GroupBox1.Controls.Add(Me.grdSaved)
        Me.GroupBox1.Controls.Add(Me.btnLoad)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtChassisNo)
        Me.GroupBox1.Controls.Add(Me.txtEngine_No)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(2, 73)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(402, 638)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Criteria"
        '
        'rbtReturned
        '
        Me.rbtReturned.AutoSize = True
        Me.rbtReturned.Location = New System.Drawing.Point(217, 19)
        Me.rbtReturned.Name = "rbtReturned"
        Me.rbtReturned.Size = New System.Drawing.Size(69, 17)
        Me.rbtReturned.TabIndex = 2
        Me.rbtReturned.Text = "Returned"
        Me.rbtReturned.UseVisualStyleBackColor = True
        '
        'Label22
        '
        Me.Label22.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label22.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label22.Location = New System.Drawing.Point(9, 209)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(386, 2)
        Me.Label22.TabIndex = 15
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(6, 151)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(49, 13)
        Me.Label34.TabIndex = 12
        Me.Label34.Text = "Remarks"
        '
        'txtSearchRemarks
        '
        Me.txtSearchRemarks.Location = New System.Drawing.Point(85, 148)
        Me.txtSearchRemarks.Name = "txtSearchRemarks"
        Me.txtSearchRemarks.Size = New System.Drawing.Size(308, 20)
        Me.txtSearchRemarks.TabIndex = 13
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.Location = New System.Drawing.Point(85, 42)
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.Size = New System.Drawing.Size(308, 20)
        Me.txtInvoiceNo.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 45)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 13)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Invoice No"
        '
        'cmbVendor
        '
        Me.cmbVendor.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append
        Me.cmbVendor.CheckedListSettings.CheckStateMember = ""
        Appearance6.BackColor = System.Drawing.Color.White
        Appearance6.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        Me.cmbVendor.DisplayLayout.Appearance = Appearance6
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
        Me.cmbVendor.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbVendor.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbVendor.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbVendor.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Appearance7.BackColor = System.Drawing.Color.Transparent
        Me.cmbVendor.DisplayLayout.Override.CardAreaAppearance = Appearance7
        Me.cmbVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbVendor.DisplayLayout.Override.CellPadding = 3
        Me.cmbVendor.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance8.TextHAlignAsString = "Left"
        Me.cmbVendor.DisplayLayout.Override.HeaderAppearance = Appearance8
        Me.cmbVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance9.BorderColor = System.Drawing.Color.LightGray
        Appearance9.TextVAlignAsString = "Middle"
        Me.cmbVendor.DisplayLayout.Override.RowAppearance = Appearance9
        Appearance10.BackColor = System.Drawing.Color.LightSteelBlue
        Appearance10.BorderColor = System.Drawing.Color.Black
        Appearance10.ForeColor = System.Drawing.Color.Black
        Me.cmbVendor.DisplayLayout.Override.SelectedRowAppearance = Appearance10
        Me.cmbVendor.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbVendor.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbVendor.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbVendor.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.cmbVendor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbVendor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbVendor.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbVendor.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbVendor.LimitToList = True
        Me.cmbVendor.Location = New System.Drawing.Point(85, 120)
        Me.cmbVendor.Name = "cmbVendor"
        Me.cmbVendor.Size = New System.Drawing.Size(308, 22)
        Me.cmbVendor.TabIndex = 11
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(6, 125)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(51, 13)
        Me.Label33.TabIndex = 10
        Me.Label33.Text = "Customer"
        '
        'rbtAll
        '
        Me.rbtAll.AutoSize = True
        Me.rbtAll.Checked = True
        Me.rbtAll.Location = New System.Drawing.Point(292, 19)
        Me.rbtAll.Name = "rbtAll"
        Me.rbtAll.Size = New System.Drawing.Size(36, 17)
        Me.rbtAll.TabIndex = 3
        Me.rbtAll.TabStop = True
        Me.rbtAll.Text = "All"
        Me.rbtAll.UseVisualStyleBackColor = True
        '
        'rbtPending
        '
        Me.rbtPending.AutoSize = True
        Me.rbtPending.Location = New System.Drawing.Point(85, 19)
        Me.rbtPending.Name = "rbtPending"
        Me.rbtPending.Size = New System.Drawing.Size(64, 17)
        Me.rbtPending.TabIndex = 0
        Me.rbtPending.Text = "Pending"
        Me.rbtPending.UseVisualStyleBackColor = True
        '
        'rbtIssued
        '
        Me.rbtIssued.AutoSize = True
        Me.rbtIssued.Location = New System.Drawing.Point(155, 19)
        Me.rbtIssued.Name = "rbtIssued"
        Me.rbtIssued.Size = New System.Drawing.Size(56, 17)
        Me.rbtIssued.TabIndex = 1
        Me.rbtIssued.Text = "Issued"
        Me.rbtIssued.UseVisualStyleBackColor = True
        '
        'grdSaved
        '
        Me.grdSaved.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdSaved.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSaved.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdSaved.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdSaved.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdSaved.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSaved.GroupByBoxVisible = False
        Me.grdSaved.Location = New System.Drawing.Point(9, 214)
        Me.grdSaved.Name = "grdSaved"
        Me.grdSaved.RecordNavigator = True
        Me.grdSaved.Size = New System.Drawing.Size(387, 418)
        Me.grdSaved.TabIndex = 16
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btnLoad
        '
        Me.btnLoad.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnLoad.Location = New System.Drawing.Point(321, 174)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(72, 23)
        Me.btnLoad.TabIndex = 14
        Me.btnLoad.Text = "Load"
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 97)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Chassis No"
        '
        'txtChassisNo
        '
        Me.txtChassisNo.Location = New System.Drawing.Point(85, 94)
        Me.txtChassisNo.Name = "txtChassisNo"
        Me.txtChassisNo.Size = New System.Drawing.Size(308, 20)
        Me.txtChassisNo.TabIndex = 9
        '
        'txtEngine_No
        '
        Me.txtEngine_No.Location = New System.Drawing.Point(85, 68)
        Me.txtEngine_No.Name = "txtEngine_No"
        Me.txtEngine_No.Size = New System.Drawing.Size(308, 20)
        Me.txtEngine_No.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 71)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Engine No"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.btnReturn)
        Me.GroupBox2.Controls.Add(Me.Label39)
        Me.GroupBox2.Controls.Add(Me.txtMeterNo)
        Me.GroupBox2.Controls.Add(Me.Label38)
        Me.GroupBox2.Controls.Add(Me.dtpContractDate)
        Me.GroupBox2.Controls.Add(Me.Label37)
        Me.GroupBox2.Controls.Add(Me.txtRegistrationNo)
        Me.GroupBox2.Controls.Add(Me.txtInstallment)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.txtAdvanceAmount)
        Me.GroupBox2.Controls.Add(Me.Label20)
        Me.GroupBox2.Controls.Add(Me.Label13)
        Me.GroupBox2.Controls.Add(Me.cmbCast)
        Me.GroupBox2.Controls.Add(Me.Label36)
        Me.GroupBox2.Controls.Add(Me.Label35)
        Me.GroupBox2.Controls.Add(Me.txtFatherName)
        Me.GroupBox2.Controls.Add(Me.txtRemarks)
        Me.GroupBox2.Controls.Add(Me.lblRemarks)
        Me.GroupBox2.Controls.Add(Me.txtDC)
        Me.GroupBox2.Controls.Add(Me.lblDcNo)
        Me.GroupBox2.Controls.Add(Me.txtNetAmount)
        Me.GroupBox2.Controls.Add(Me.Label32)
        Me.GroupBox2.Controls.Add(Me.txtColor)
        Me.GroupBox2.Controls.Add(Me.Label31)
        Me.GroupBox2.Controls.Add(Me.Label30)
        Me.GroupBox2.Controls.Add(Me.txtReferenceNo)
        Me.GroupBox2.Controls.Add(Me.txtTaxPercent)
        Me.GroupBox2.Controls.Add(Me.Label29)
        Me.GroupBox2.Controls.Add(Me.txtRegistrationFor)
        Me.GroupBox2.Controls.Add(Me.Label28)
        Me.GroupBox2.Controls.Add(Me.Label27)
        Me.GroupBox2.Controls.Add(Me.txtNTN)
        Me.GroupBox2.Controls.Add(Me.Label26)
        Me.GroupBox2.Controls.Add(Me.txtAddress)
        Me.GroupBox2.Controls.Add(Me.txtSalesTax)
        Me.GroupBox2.Controls.Add(Me.Label24)
        Me.GroupBox2.Controls.Add(Me.txtInvoiceAmount)
        Me.GroupBox2.Controls.Add(Me.Label25)
        Me.GroupBox2.Controls.Add(Me.txtModelCode)
        Me.GroupBox2.Controls.Add(Me.Label23)
        Me.GroupBox2.Controls.Add(Me.btnPrint)
        Me.GroupBox2.Controls.Add(Me.btnDelete)
        Me.GroupBox2.Controls.Add(Me.btnSave)
        Me.GroupBox2.Controls.Add(Me.txtComments)
        Me.GroupBox2.Controls.Add(Me.Label21)
        Me.GroupBox2.Controls.Add(Me.txtBaseWheel)
        Me.GroupBox2.Controls.Add(Me.Label17)
        Me.GroupBox2.Controls.Add(Me.txtRearWheel)
        Me.GroupBox2.Controls.Add(Me.Label18)
        Me.GroupBox2.Controls.Add(Me.txtFrontWheel)
        Me.GroupBox2.Controls.Add(Me.Label19)
        Me.GroupBox2.Controls.Add(Me.txtRearAxel)
        Me.GroupBox2.Controls.Add(Me.Label16)
        Me.GroupBox2.Controls.Add(Me.txtFrontAxl)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.txtLadenWt)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.txtModelDesc)
        Me.GroupBox2.Controls.Add(Me.Label12)
        Me.GroupBox2.Controls.Add(Me.Label11)
        Me.GroupBox2.Controls.Add(Me.txtCrtChassisNo)
        Me.GroupBox2.Controls.Add(Me.Label10)
        Me.GroupBox2.Controls.Add(Me.txtCrtEnginNo)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.txtDeliveredTo)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.dtpCertificateDate)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.txtCertificateNo)
        Me.GroupBox2.Location = New System.Drawing.Point(4, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(461, 638)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Certificate"
        '
        'btnReturn
        '
        Me.btnReturn.Location = New System.Drawing.Point(275, 596)
        Me.btnReturn.Name = "btnReturn"
        Me.btnReturn.Size = New System.Drawing.Size(75, 23)
        Me.btnReturn.TabIndex = 68
        Me.btnReturn.Text = "Return"
        Me.btnReturn.UseVisualStyleBackColor = True
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.Location = New System.Drawing.Point(6, 320)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(51, 13)
        Me.Label39.TabIndex = 31
        Me.Label39.Text = "Meter No"
        '
        'txtMeterNo
        '
        Me.txtMeterNo.BackColor = System.Drawing.SystemColors.Info
        Me.txtMeterNo.Location = New System.Drawing.Point(111, 317)
        Me.txtMeterNo.Name = "txtMeterNo"
        Me.txtMeterNo.Size = New System.Drawing.Size(104, 20)
        Me.txtMeterNo.TabIndex = 32
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(6, 75)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(73, 13)
        Me.Label38.TabIndex = 6
        Me.Label38.Text = "Contract Date"
        '
        'dtpContractDate
        '
        Me.dtpContractDate.CalendarMonthBackground = System.Drawing.SystemColors.Info
        Me.dtpContractDate.Checked = False
        Me.dtpContractDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpContractDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpContractDate.Location = New System.Drawing.Point(111, 73)
        Me.dtpContractDate.Name = "dtpContractDate"
        Me.dtpContractDate.ShowCheckBox = True
        Me.dtpContractDate.Size = New System.Drawing.Size(119, 20)
        Me.dtpContractDate.TabIndex = 7
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(272, 101)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(44, 13)
        Me.Label37.TabIndex = 10
        Me.Label37.Text = "Reg No"
        '
        'txtRegistrationNo
        '
        Me.txtRegistrationNo.BackColor = System.Drawing.SystemColors.Info
        Me.txtRegistrationNo.Location = New System.Drawing.Point(321, 98)
        Me.txtRegistrationNo.Name = "txtRegistrationNo"
        Me.txtRegistrationNo.Size = New System.Drawing.Size(109, 20)
        Me.txtRegistrationNo.TabIndex = 11
        '
        'txtInstallment
        '
        Me.txtInstallment.Location = New System.Drawing.Point(321, 395)
        Me.txtInstallment.Name = "txtInstallment"
        Me.txtInstallment.Size = New System.Drawing.Size(106, 20)
        Me.txtInstallment.TabIndex = 42
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(259, 398)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(57, 13)
        Me.Label7.TabIndex = 41
        Me.Label7.Text = "Installment"
        '
        'txtAdvanceAmount
        '
        Me.txtAdvanceAmount.Location = New System.Drawing.Point(113, 395)
        Me.txtAdvanceAmount.Name = "txtAdvanceAmount"
        Me.txtAdvanceAmount.Size = New System.Drawing.Size(106, 20)
        Me.txtAdvanceAmount.TabIndex = 40
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(6, 398)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(89, 13)
        Me.Label20.TabIndex = 39
        Me.Label20.Text = "Advance Amount"
        '
        'Label13
        '
        Me.Label13.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label13.Location = New System.Drawing.Point(9, 474)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(431, 2)
        Me.Label13.TabIndex = 51
        '
        'cmbCast
        '
        Me.cmbCast.FormattingEnabled = True
        Me.cmbCast.Location = New System.Drawing.Point(321, 150)
        Me.cmbCast.Name = "cmbCast"
        Me.cmbCast.Size = New System.Drawing.Size(107, 21)
        Me.cmbCast.TabIndex = 17
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(288, 153)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(28, 13)
        Me.Label36.TabIndex = 16
        Me.Label36.Text = "Cast"
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(6, 153)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(85, 13)
        Me.Label35.TabIndex = 14
        Me.Label35.Text = "Father/Guardian"
        '
        'txtFatherName
        '
        Me.txtFatherName.Location = New System.Drawing.Point(111, 150)
        Me.txtFatherName.Name = "txtFatherName"
        Me.txtFatherName.Size = New System.Drawing.Size(151, 20)
        Me.txtFatherName.TabIndex = 15
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(111, 255)
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(316, 20)
        Me.txtRemarks.TabIndex = 25
        '
        'lblRemarks
        '
        Me.lblRemarks.AutoSize = True
        Me.lblRemarks.Location = New System.Drawing.Point(6, 258)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(49, 13)
        Me.lblRemarks.TabIndex = 24
        Me.lblRemarks.Text = "Remarks"
        '
        'txtDC
        '
        Me.txtDC.Location = New System.Drawing.Point(111, 229)
        Me.txtDC.Name = "txtDC"
        Me.txtDC.Size = New System.Drawing.Size(316, 20)
        Me.txtDC.TabIndex = 23
        '
        'lblDcNo
        '
        Me.lblDcNo.AutoSize = True
        Me.lblDcNo.Location = New System.Drawing.Point(6, 232)
        Me.lblDcNo.Name = "lblDcNo"
        Me.lblDcNo.Size = New System.Drawing.Size(39, 13)
        Me.lblDcNo.TabIndex = 22
        Me.lblDcNo.Text = "DC No"
        '
        'txtNetAmount
        '
        Me.txtNetAmount.Location = New System.Drawing.Point(113, 446)
        Me.txtNetAmount.Name = "txtNetAmount"
        Me.txtNetAmount.Size = New System.Drawing.Size(104, 20)
        Me.txtNetAmount.TabIndex = 48
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(6, 449)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(63, 13)
        Me.Label32.TabIndex = 47
        Me.Label32.Text = "Net Amount"
        '
        'txtColor
        '
        Me.txtColor.BackColor = System.Drawing.SystemColors.Info
        Me.txtColor.Location = New System.Drawing.Point(322, 343)
        Me.txtColor.Name = "txtColor"
        Me.txtColor.Size = New System.Drawing.Size(106, 20)
        Me.txtColor.TabIndex = 36
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Location = New System.Drawing.Point(285, 346)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(31, 13)
        Me.Label31.TabIndex = 35
        Me.Label31.Text = "Color"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(275, 49)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(41, 13)
        Me.Label30.TabIndex = 4
        Me.Label30.Text = "Ref No"
        '
        'txtReferenceNo
        '
        Me.txtReferenceNo.Location = New System.Drawing.Point(321, 47)
        Me.txtReferenceNo.Name = "txtReferenceNo"
        Me.txtReferenceNo.Size = New System.Drawing.Size(106, 20)
        Me.txtReferenceNo.TabIndex = 5
        '
        'txtTaxPercent
        '
        Me.txtTaxPercent.Location = New System.Drawing.Point(322, 420)
        Me.txtTaxPercent.Name = "txtTaxPercent"
        Me.txtTaxPercent.Size = New System.Drawing.Size(105, 20)
        Me.txtTaxPercent.TabIndex = 46
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(280, 423)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(36, 13)
        Me.Label29.TabIndex = 45
        Me.Label29.Text = "Tax %"
        '
        'txtRegistrationFor
        '
        Me.txtRegistrationFor.BackColor = System.Drawing.SystemColors.Info
        Me.txtRegistrationFor.Location = New System.Drawing.Point(111, 98)
        Me.txtRegistrationFor.Name = "txtRegistrationFor"
        Me.txtRegistrationFor.Size = New System.Drawing.Size(151, 20)
        Me.txtRegistrationFor.TabIndex = 9
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(6, 101)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(81, 13)
        Me.Label28.TabIndex = 8
        Me.Label28.Text = "Registration For"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(6, 205)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(80, 13)
        Me.Label27.TabIndex = 20
        Me.Label27.Text = "NTN/CNIC No."
        '
        'txtNTN
        '
        Me.txtNTN.Location = New System.Drawing.Point(111, 202)
        Me.txtNTN.Name = "txtNTN"
        Me.txtNTN.Size = New System.Drawing.Size(316, 20)
        Me.txtNTN.TabIndex = 21
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(6, 179)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(45, 13)
        Me.Label26.TabIndex = 18
        Me.Label26.Text = "Address"
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(111, 176)
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(316, 20)
        Me.txtAddress.TabIndex = 19
        '
        'txtSalesTax
        '
        Me.txtSalesTax.Location = New System.Drawing.Point(322, 446)
        Me.txtSalesTax.Name = "txtSalesTax"
        Me.txtSalesTax.Size = New System.Drawing.Size(105, 20)
        Me.txtSalesTax.TabIndex = 50
        Me.txtSalesTax.Text = " "
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(262, 449)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(54, 13)
        Me.Label24.TabIndex = 49
        Me.Label24.Text = "Sales Tax"
        '
        'txtInvoiceAmount
        '
        Me.txtInvoiceAmount.Location = New System.Drawing.Point(111, 420)
        Me.txtInvoiceAmount.Name = "txtInvoiceAmount"
        Me.txtInvoiceAmount.Size = New System.Drawing.Size(106, 20)
        Me.txtInvoiceAmount.TabIndex = 44
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(6, 423)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(81, 13)
        Me.Label25.TabIndex = 43
        Me.Label25.Text = "Invoice Amount"
        '
        'txtModelCode
        '
        Me.txtModelCode.BackColor = System.Drawing.SystemColors.Info
        Me.txtModelCode.Location = New System.Drawing.Point(113, 343)
        Me.txtModelCode.Name = "txtModelCode"
        Me.txtModelCode.Size = New System.Drawing.Size(104, 20)
        Me.txtModelCode.TabIndex = 34
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(6, 349)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(64, 13)
        Me.Label23.TabIndex = 33
        Me.Label23.Text = "Model Code"
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(353, 596)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(75, 23)
        Me.btnPrint.TabIndex = 69
        Me.btnPrint.Text = "Print"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(194, 596)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 67
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(113, 596)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 66
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'txtComments
        '
        Me.txtComments.BackColor = System.Drawing.SystemColors.Info
        Me.txtComments.Location = New System.Drawing.Point(111, 566)
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtComments.Size = New System.Drawing.Size(316, 27)
        Me.txtComments.TabIndex = 65
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(6, 566)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(56, 13)
        Me.Label21.TabIndex = 64
        Me.Label21.Text = "Comments"
        '
        'txtBaseWheel
        '
        Me.txtBaseWheel.BackColor = System.Drawing.SystemColors.Info
        Me.txtBaseWheel.Location = New System.Drawing.Point(321, 536)
        Me.txtBaseWheel.Name = "txtBaseWheel"
        Me.txtBaseWheel.Size = New System.Drawing.Size(105, 20)
        Me.txtBaseWheel.TabIndex = 63
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(248, 540)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(68, 13)
        Me.Label17.TabIndex = 62
        Me.Label17.Text = "Base Wheel:"
        '
        'txtRearWheel
        '
        Me.txtRearWheel.BackColor = System.Drawing.SystemColors.Info
        Me.txtRearWheel.Location = New System.Drawing.Point(321, 510)
        Me.txtRearWheel.Name = "txtRearWheel"
        Me.txtRearWheel.Size = New System.Drawing.Size(105, 20)
        Me.txtRearWheel.TabIndex = 59
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(249, 514)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(67, 13)
        Me.Label18.TabIndex = 58
        Me.Label18.Text = "Rear Wheel:"
        '
        'txtFrontWheel
        '
        Me.txtFrontWheel.BackColor = System.Drawing.SystemColors.Info
        Me.txtFrontWheel.Location = New System.Drawing.Point(321, 484)
        Me.txtFrontWheel.Name = "txtFrontWheel"
        Me.txtFrontWheel.Size = New System.Drawing.Size(105, 20)
        Me.txtFrontWheel.TabIndex = 55
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(248, 488)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(68, 13)
        Me.Label19.TabIndex = 54
        Me.Label19.Text = "Front Wheel:"
        '
        'txtRearAxel
        '
        Me.txtRearAxel.BackColor = System.Drawing.SystemColors.Info
        Me.txtRearAxel.Location = New System.Drawing.Point(111, 536)
        Me.txtRearAxel.Name = "txtRearAxel"
        Me.txtRearAxel.Size = New System.Drawing.Size(106, 20)
        Me.txtRearAxel.TabIndex = 61
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(6, 540)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(96, 13)
        Me.Label16.TabIndex = 60
        Me.Label16.Text = "Max Wt Rear Axel:"
        '
        'txtFrontAxl
        '
        Me.txtFrontAxl.BackColor = System.Drawing.SystemColors.Info
        Me.txtFrontAxl.Location = New System.Drawing.Point(111, 510)
        Me.txtFrontAxl.Name = "txtFrontAxl"
        Me.txtFrontAxl.Size = New System.Drawing.Size(106, 20)
        Me.txtFrontAxl.TabIndex = 57
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(6, 514)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(97, 13)
        Me.Label15.TabIndex = 56
        Me.Label15.Text = "Max Wt Front Axel:"
        '
        'txtLadenWt
        '
        Me.txtLadenWt.BackColor = System.Drawing.SystemColors.Info
        Me.txtLadenWt.Location = New System.Drawing.Point(111, 484)
        Me.txtLadenWt.Name = "txtLadenWt"
        Me.txtLadenWt.Size = New System.Drawing.Size(106, 20)
        Me.txtLadenWt.TabIndex = 53
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(6, 488)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(80, 13)
        Me.Label14.TabIndex = 52
        Me.Label14.Text = "Max Leden Wt:"
        '
        'txtModelDesc
        '
        Me.txtModelDesc.BackColor = System.Drawing.SystemColors.Info
        Me.txtModelDesc.Location = New System.Drawing.Point(113, 369)
        Me.txtModelDesc.Name = "txtModelDesc"
        Me.txtModelDesc.Size = New System.Drawing.Size(315, 20)
        Me.txtModelDesc.TabIndex = 38
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 375)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(92, 13)
        Me.Label12.TabIndex = 37
        Me.Label12.Text = "Model Description"
        '
        'Label11
        '
        Me.Label11.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.Label11.Location = New System.Drawing.Point(7, 281)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(436, 2)
        Me.Label11.TabIndex = 26
        '
        'txtCrtChassisNo
        '
        Me.txtCrtChassisNo.Location = New System.Drawing.Point(321, 291)
        Me.txtCrtChassisNo.Name = "txtCrtChassisNo"
        Me.txtCrtChassisNo.ReadOnly = True
        Me.txtCrtChassisNo.Size = New System.Drawing.Size(106, 20)
        Me.txtCrtChassisNo.TabIndex = 30
        Me.txtCrtChassisNo.Text = " "
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(256, 294)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(60, 13)
        Me.Label10.TabIndex = 29
        Me.Label10.Text = "Chassis No"
        '
        'txtCrtEnginNo
        '
        Me.txtCrtEnginNo.Location = New System.Drawing.Point(113, 291)
        Me.txtCrtEnginNo.Name = "txtCrtEnginNo"
        Me.txtCrtEnginNo.ReadOnly = True
        Me.txtCrtEnginNo.Size = New System.Drawing.Size(104, 20)
        Me.txtCrtEnginNo.TabIndex = 28
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 294)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(57, 13)
        Me.Label9.TabIndex = 27
        Me.Label9.Text = "Engine No"
        '
        'txtDeliveredTo
        '
        Me.txtDeliveredTo.BackColor = System.Drawing.SystemColors.Info
        Me.txtDeliveredTo.Location = New System.Drawing.Point(111, 124)
        Me.txtDeliveredTo.Name = "txtDeliveredTo"
        Me.txtDeliveredTo.Size = New System.Drawing.Size(317, 20)
        Me.txtDeliveredTo.TabIndex = 13
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 127)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(68, 13)
        Me.Label8.TabIndex = 12
        Me.Label8.Text = "Delivered To"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 51)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 13)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Certificate Date"
        '
        'dtpCertificateDate
        '
        Me.dtpCertificateDate.CalendarMonthBackground = System.Drawing.SystemColors.Info
        Me.dtpCertificateDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpCertificateDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpCertificateDate.Location = New System.Drawing.Point(111, 47)
        Me.dtpCertificateDate.Name = "dtpCertificateDate"
        Me.dtpCertificateDate.Size = New System.Drawing.Size(119, 20)
        Me.dtpCertificateDate.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Certificate No"
        '
        'txtCertificateNo
        '
        Me.txtCertificateNo.Location = New System.Drawing.Point(111, 21)
        Me.txtCertificateNo.Name = "txtCertificateNo"
        Me.txtCertificateNo.ReadOnly = True
        Me.txtCertificateNo.Size = New System.Drawing.Size(119, 20)
        Me.txtCertificateNo.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Location = New System.Drawing.Point(410, 73)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(468, 645)
        Me.Panel1.TabIndex = 4
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(884, 705)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(0, 13)
        Me.Label40.TabIndex = 3
        '
        'pnlHeader
        '
        Me.pnlHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Location = New System.Drawing.Point(0, 25)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(884, 42)
        Me.pnlHeader.TabIndex = 1
        '
        'CtrlGrdBar3
        '
        Me.CtrlGrdBar3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar3.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar3.Email = Nothing
        Me.CtrlGrdBar3.FormName = Me
        Me.CtrlGrdBar3.Location = New System.Drawing.Point(858, 0)
        Me.CtrlGrdBar3.MyGrid = Me.grdSaved
        Me.CtrlGrdBar3.Name = "CtrlGrdBar3"
        Me.CtrlGrdBar3.Size = New System.Drawing.Size(34, 25)
        Me.CtrlGrdBar3.TabIndex = 18
        '
        'frmSalesCertificate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.AutoScrollMargin = New System.Drawing.Size(5, 0)
        Me.AutoScrollMinSize = New System.Drawing.Size(5, 0)
        Me.ClientSize = New System.Drawing.Size(912, 713)
        Me.Controls.Add(Me.CtrlGrdBar3)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.Label40)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.KeyPreview = True
        Me.Name = "frmSalesCertificate"
        Me.Text = "Sales Certificate"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.cmbVendor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtEngine_No As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtChassisNo As System.Windows.Forms.TextBox
    Friend WithEvents grdSaved As Janus.Windows.GridEX.GridEX
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dtpCertificateDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtCertificateNo As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtCrtChassisNo As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtCrtEnginNo As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtDeliveredTo As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtModelDesc As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtLadenWt As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtFrontAxl As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtRearAxel As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtBaseWheel As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtRearWheel As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtFrontWheel As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents txtModelCode As System.Windows.Forms.TextBox
    Friend WithEvents txtSalesTax As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtInvoiceAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents btnPrintCertificate As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents PrintSalesInvoiceToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents txtNTN As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents PrintSalesInvoiceWithDealerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtTaxPercent As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtRegistrationFor As System.Windows.Forms.TextBox
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtReferenceNo As System.Windows.Forms.TextBox
    Friend WithEvents txtColor As System.Windows.Forms.TextBox
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents rbtPending As System.Windows.Forms.RadioButton
    Friend WithEvents rbtIssued As System.Windows.Forms.RadioButton
    Friend WithEvents txtNetAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents lblDcNo As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents lblRemarks As System.Windows.Forms.Label
    Friend WithEvents txtDC As System.Windows.Forms.TextBox
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents rbtAll As System.Windows.Forms.RadioButton
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents txtInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbVendor As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents txtSearchRemarks As System.Windows.Forms.TextBox
    Friend WithEvents btnAgreementLetter As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbCast As System.Windows.Forms.ComboBox
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents txtFatherName As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtAdvanceAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents txtInstallment As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents txtMeterNo As System.Windows.Forms.TextBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents dtpContractDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents txtRegistrationNo As System.Windows.Forms.TextBox
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbTask As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbConfig As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents CtrlGrdBar3 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents rbtReturned As System.Windows.Forms.RadioButton
    Friend WithEvents btnReturn As System.Windows.Forms.Button
End Class

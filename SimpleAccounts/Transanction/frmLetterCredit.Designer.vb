<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLetterCredit
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLetterCredit))
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
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
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripSplitButton()
        Me.CashAccountToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BankAccountToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CostCenterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.VendorLCAccountToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbTask = New System.Windows.Forms.ToolStripButton()
        Me.tsbConfig = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.txtCostOfMaterial = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.chkClose = New System.Windows.Forms.CheckBox()
        Me.btnSaveCreditLimit = New System.Windows.Forms.Button()
        Me.lblBankCreditLimit = New System.Windows.Forms.Label()
        Me.txtCreditLimit = New System.Windows.Forms.TextBox()
        Me.lblOrigin = New System.Windows.Forms.Label()
        Me.cmbOrigin = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtFreight = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtInsurrance = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.dtpDBRDate = New System.Windows.Forms.DateTimePicker()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.DTBDate = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.dtpDDDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpNNDate = New System.Windows.Forms.DateTimePicker()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.dtpLSBDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpLatestDateofShipment = New System.Windows.Forms.DateTimePicker()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbPortOfDischarge = New System.Windows.Forms.ComboBox()
        Me.cmbPortOfLoading = New System.Windows.Forms.ComboBox()
        Me.cmbVendor = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.grpCurrency = New System.Windows.Forms.GroupBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtCurrencyRate = New System.Windows.Forms.TextBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.cmbCurrency = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dtpPerformaDate = New System.Windows.Forms.DateTimePicker()
        Me.txtPerformaNo = New System.Windows.Forms.TextBox()
        Me.Label70 = New System.Windows.Forms.Label()
        Me.dtpExpiryDate = New System.Windows.Forms.DateTimePicker()
        Me.Label76 = New System.Windows.Forms.Label()
        Me.cmbOpenedBy = New System.Windows.Forms.ComboBox()
        Me.Label85 = New System.Windows.Forms.Label()
        Me.Label84 = New System.Windows.Forms.Label()
        Me.Label81 = New System.Windows.Forms.Label()
        Me.Label80 = New System.Windows.Forms.Label()
        Me.Label79 = New System.Windows.Forms.Label()
        Me.Label78 = New System.Windows.Forms.Label()
        Me.Label77 = New System.Windows.Forms.Label()
        Me.cmbTransporter = New System.Windows.Forms.ComboBox()
        Me.cmbClearingAgent = New System.Windows.Forms.ComboBox()
        Me.dtpETADate = New System.Windows.Forms.DateTimePicker()
        Me.dtpETDDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpBLDate = New System.Windows.Forms.DateTimePicker()
        Me.txtBLNo = New System.Windows.Forms.TextBox()
        Me.txtVessel = New System.Windows.Forms.TextBox()
        Me.Label68 = New System.Windows.Forms.Label()
        Me.txtRefNo = New System.Windows.Forms.TextBox()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.Label73 = New System.Windows.Forms.Label()
        Me.txtSpecialInstruction = New System.Windows.Forms.TextBox()
        Me.txtAdvisingBank = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbCostCenter = New System.Windows.Forms.ComboBox()
        Me.chkActive = New System.Windows.Forms.CheckBox()
        Me.Groupbox = New System.Windows.Forms.GroupBox()
        Me.lblchequeno = New System.Windows.Forms.Label()
        Me.dateCheque = New System.Windows.Forms.DateTimePicker()
        Me.lbldatecheque = New System.Windows.Forms.Label()
        Me.txtChequeNo = New System.Windows.Forms.TextBox()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.txtremarks = New System.Windows.Forms.TextBox()
        Me.txtretiringamount = New System.Windows.Forms.TextBox()
        Me.txtpaidamount = New System.Windows.Forms.TextBox()
        Me.txtLCamount = New System.Windows.Forms.TextBox()
        Me.txtdocnumber = New System.Windows.Forms.TextBox()
        Me.cmbtypeCFD = New System.Windows.Forms.ComboBox()
        Me.cmbbank = New System.Windows.Forms.ComboBox()
        Me.cmbpaymentfrom = New System.Windows.Forms.ComboBox()
        Me.cmbmethod = New System.Windows.Forms.ComboBox()
        Me.cmbdoctype = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblCrunRate = New System.Windows.Forms.Label()
        Me.LBLCURRENCY = New System.Windows.Forms.Label()
        Me.lblTFDFOB = New System.Windows.Forms.Label()
        Me.lblvandor = New System.Windows.Forms.Label()
        Me.lblBank = New System.Windows.Forms.Label()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblNo = New System.Windows.Forms.Label()
        Me.lblType = New System.Windows.Forms.Label()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdLetterOfCreadit = New Janus.Windows.GridEX.GridEX()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.cmbVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpCurrency.SuspendLayout()
        Me.Groupbox.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdLetterOfCreadit, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.AutoScroll = True
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.txtCostOfMaterial)
        Me.UltraTabPageControl1.Controls.Add(Me.Label19)
        Me.UltraTabPageControl1.Controls.Add(Me.Label16)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbStatus)
        Me.UltraTabPageControl1.Controls.Add(Me.chkClose)
        Me.UltraTabPageControl1.Controls.Add(Me.btnSaveCreditLimit)
        Me.UltraTabPageControl1.Controls.Add(Me.lblBankCreditLimit)
        Me.UltraTabPageControl1.Controls.Add(Me.txtCreditLimit)
        Me.UltraTabPageControl1.Controls.Add(Me.lblOrigin)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbOrigin)
        Me.UltraTabPageControl1.Controls.Add(Me.Label15)
        Me.UltraTabPageControl1.Controls.Add(Me.txtFreight)
        Me.UltraTabPageControl1.Controls.Add(Me.Label14)
        Me.UltraTabPageControl1.Controls.Add(Me.txtInsurrance)
        Me.UltraTabPageControl1.Controls.Add(Me.Label13)
        Me.UltraTabPageControl1.Controls.Add(Me.dtpDBRDate)
        Me.UltraTabPageControl1.Controls.Add(Me.Label12)
        Me.UltraTabPageControl1.Controls.Add(Me.DTBDate)
        Me.UltraTabPageControl1.Controls.Add(Me.Label8)
        Me.UltraTabPageControl1.Controls.Add(Me.Label9)
        Me.UltraTabPageControl1.Controls.Add(Me.dtpDDDate)
        Me.UltraTabPageControl1.Controls.Add(Me.dtpNNDate)
        Me.UltraTabPageControl1.Controls.Add(Me.Label17)
        Me.UltraTabPageControl1.Controls.Add(Me.Label18)
        Me.UltraTabPageControl1.Controls.Add(Me.dtpLSBDate)
        Me.UltraTabPageControl1.Controls.Add(Me.dtpLatestDateofShipment)
        Me.UltraTabPageControl1.Controls.Add(Me.Label11)
        Me.UltraTabPageControl1.Controls.Add(Me.Label10)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbPortOfDischarge)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbPortOfLoading)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbVendor)
        Me.UltraTabPageControl1.Controls.Add(Me.grpCurrency)
        Me.UltraTabPageControl1.Controls.Add(Me.Label7)
        Me.UltraTabPageControl1.Controls.Add(Me.Label6)
        Me.UltraTabPageControl1.Controls.Add(Me.dtpPerformaDate)
        Me.UltraTabPageControl1.Controls.Add(Me.txtPerformaNo)
        Me.UltraTabPageControl1.Controls.Add(Me.Label70)
        Me.UltraTabPageControl1.Controls.Add(Me.dtpExpiryDate)
        Me.UltraTabPageControl1.Controls.Add(Me.Label76)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbOpenedBy)
        Me.UltraTabPageControl1.Controls.Add(Me.Label85)
        Me.UltraTabPageControl1.Controls.Add(Me.Label84)
        Me.UltraTabPageControl1.Controls.Add(Me.Label81)
        Me.UltraTabPageControl1.Controls.Add(Me.Label80)
        Me.UltraTabPageControl1.Controls.Add(Me.Label79)
        Me.UltraTabPageControl1.Controls.Add(Me.Label78)
        Me.UltraTabPageControl1.Controls.Add(Me.Label77)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbTransporter)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbClearingAgent)
        Me.UltraTabPageControl1.Controls.Add(Me.dtpETADate)
        Me.UltraTabPageControl1.Controls.Add(Me.dtpETDDate)
        Me.UltraTabPageControl1.Controls.Add(Me.dtpBLDate)
        Me.UltraTabPageControl1.Controls.Add(Me.txtBLNo)
        Me.UltraTabPageControl1.Controls.Add(Me.txtVessel)
        Me.UltraTabPageControl1.Controls.Add(Me.Label68)
        Me.UltraTabPageControl1.Controls.Add(Me.txtRefNo)
        Me.UltraTabPageControl1.Controls.Add(Me.Label74)
        Me.UltraTabPageControl1.Controls.Add(Me.Label73)
        Me.UltraTabPageControl1.Controls.Add(Me.txtSpecialInstruction)
        Me.UltraTabPageControl1.Controls.Add(Me.txtAdvisingBank)
        Me.UltraTabPageControl1.Controls.Add(Me.Label5)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbCostCenter)
        Me.UltraTabPageControl1.Controls.Add(Me.chkActive)
        Me.UltraTabPageControl1.Controls.Add(Me.Groupbox)
        Me.UltraTabPageControl1.Controls.Add(Me.DateTimePicker1)
        Me.UltraTabPageControl1.Controls.Add(Me.txtremarks)
        Me.UltraTabPageControl1.Controls.Add(Me.txtretiringamount)
        Me.UltraTabPageControl1.Controls.Add(Me.txtpaidamount)
        Me.UltraTabPageControl1.Controls.Add(Me.txtLCamount)
        Me.UltraTabPageControl1.Controls.Add(Me.txtdocnumber)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbtypeCFD)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbbank)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbpaymentfrom)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbmethod)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbdoctype)
        Me.UltraTabPageControl1.Controls.Add(Me.Label2)
        Me.UltraTabPageControl1.Controls.Add(Me.Label1)
        Me.UltraTabPageControl1.Controls.Add(Me.lblCrunRate)
        Me.UltraTabPageControl1.Controls.Add(Me.LBLCURRENCY)
        Me.UltraTabPageControl1.Controls.Add(Me.lblTFDFOB)
        Me.UltraTabPageControl1.Controls.Add(Me.lblvandor)
        Me.UltraTabPageControl1.Controls.Add(Me.lblBank)
        Me.UltraTabPageControl1.Controls.Add(Me.lblDate)
        Me.UltraTabPageControl1.Controls.Add(Me.Label4)
        Me.UltraTabPageControl1.Controls.Add(Me.Label3)
        Me.UltraTabPageControl1.Controls.Add(Me.lblNo)
        Me.UltraTabPageControl1.Controls.Add(Me.lblType)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(838, 692)
        '
        'pnlHeader
        '
        Me.pnlHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(838, 42)
        Me.pnlHeader.TabIndex = 86
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(9, 8)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(175, 23)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Letter of Credit"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.btnPrint, Me.toolStripSeparator, Me.btnDelete, Me.toolStripSeparator1, Me.btnRefresh, Me.ToolStripSeparator4, Me.ToolStripButton1, Me.ToolStripSeparator2, Me.tsbTask, Me.tsbConfig, Me.ToolStripSeparator3})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(840, 25)
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
        'btnEdit
        '
        Me.btnEdit.Image = CType(resources.GetObject("btnEdit.Image"), System.Drawing.Image)
        Me.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(47, 22)
        Me.btnEdit.Text = "&Edit"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(51, 22)
        Me.btnSave.Text = "&Save"
        '
        'btnPrint
        '
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(52, 22)
        Me.btnPrint.Text = "&Print"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(60, 22)
        Me.btnDelete.Text = "D&elete"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(66, 22)
        Me.btnRefresh.Text = "Refresh"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CashAccountToolStripMenuItem, Me.BankAccountToolStripMenuItem, Me.CostCenterToolStripMenuItem, Me.VendorLCAccountToolStripMenuItem})
        Me.ToolStripButton1.Image = Global.SimpleAccounts.My.Resources.Resources.sendcontactdetails
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(109, 22)
        Me.ToolStripButton1.Text = "Add Account"
        '
        'CashAccountToolStripMenuItem
        '
        Me.CashAccountToolStripMenuItem.Name = "CashAccountToolStripMenuItem"
        Me.CashAccountToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.CashAccountToolStripMenuItem.Text = "Cash Account"
        '
        'BankAccountToolStripMenuItem
        '
        Me.BankAccountToolStripMenuItem.Name = "BankAccountToolStripMenuItem"
        Me.BankAccountToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.BankAccountToolStripMenuItem.Text = "Bank Account"
        '
        'CostCenterToolStripMenuItem
        '
        Me.CostCenterToolStripMenuItem.Name = "CostCenterToolStripMenuItem"
        Me.CostCenterToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.CostCenterToolStripMenuItem.Text = "Cost Center"
        '
        'VendorLCAccountToolStripMenuItem
        '
        Me.VendorLCAccountToolStripMenuItem.Name = "VendorLCAccountToolStripMenuItem"
        Me.VendorLCAccountToolStripMenuItem.Size = New System.Drawing.Size(178, 22)
        Me.VendorLCAccountToolStripMenuItem.Text = "Vendor/LC Account"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'tsbTask
        '
        Me.tsbTask.Image = Global.SimpleAccounts.My.Resources.Resources.Untitled_1
        Me.tsbTask.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbTask.Name = "tsbTask"
        Me.tsbTask.Size = New System.Drawing.Size(88, 22)
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
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'txtCostOfMaterial
        '
        Me.txtCostOfMaterial.Location = New System.Drawing.Point(126, 668)
        Me.txtCostOfMaterial.Name = "txtCostOfMaterial"
        Me.txtCostOfMaterial.Size = New System.Drawing.Size(220, 20)
        Me.txtCostOfMaterial.TabIndex = 85
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(10, 671)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(80, 13)
        Me.Label19.TabIndex = 84
        Me.Label19.Text = "Cost of Material"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(10, 647)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(37, 13)
        Me.Label16.TabIndex = 82
        Me.Label16.Tag = ""
        Me.Label16.Text = "Status"
        '
        'cmbStatus
        '
        Me.cmbStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Location = New System.Drawing.Point(126, 641)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(220, 21)
        Me.cmbStatus.TabIndex = 83
        '
        'chkClose
        '
        Me.chkClose.AutoSize = True
        Me.chkClose.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.chkClose.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chkClose.Location = New System.Drawing.Point(558, 666)
        Me.chkClose.Name = "chkClose"
        Me.chkClose.Size = New System.Drawing.Size(64, 18)
        Me.chkClose.TabIndex = 81
        Me.chkClose.Text = "Closed"
        Me.chkClose.UseVisualStyleBackColor = True
        '
        'btnSaveCreditLimit
        '
        Me.btnSaveCreditLimit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnSaveCreditLimit.Image = Global.SimpleAccounts.My.Resources.Resources.save_labled
        Me.btnSaveCreditLimit.Location = New System.Drawing.Point(350, 175)
        Me.btnSaveCreditLimit.Name = "btnSaveCreditLimit"
        Me.btnSaveCreditLimit.Size = New System.Drawing.Size(24, 21)
        Me.btnSaveCreditLimit.TabIndex = 11
        Me.btnSaveCreditLimit.UseVisualStyleBackColor = True
        '
        'lblBankCreditLimit
        '
        Me.lblBankCreditLimit.AutoSize = True
        Me.lblBankCreditLimit.Location = New System.Drawing.Point(10, 178)
        Me.lblBankCreditLimit.Name = "lblBankCreditLimit"
        Me.lblBankCreditLimit.Size = New System.Drawing.Size(58, 13)
        Me.lblBankCreditLimit.TabIndex = 9
        Me.lblBankCreditLimit.Text = "Credit Limit"
        '
        'txtCreditLimit
        '
        Me.txtCreditLimit.Location = New System.Drawing.Point(126, 176)
        Me.txtCreditLimit.Name = "txtCreditLimit"
        Me.txtCreditLimit.Size = New System.Drawing.Size(220, 20)
        Me.txtCreditLimit.TabIndex = 10
        '
        'lblOrigin
        '
        Me.lblOrigin.AutoSize = True
        Me.lblOrigin.Location = New System.Drawing.Point(10, 620)
        Me.lblOrigin.Name = "lblOrigin"
        Me.lblOrigin.Size = New System.Drawing.Size(34, 13)
        Me.lblOrigin.TabIndex = 39
        Me.lblOrigin.Tag = ""
        Me.lblOrigin.Text = "Origin"
        '
        'cmbOrigin
        '
        Me.cmbOrigin.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbOrigin.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbOrigin.FormattingEnabled = True
        Me.cmbOrigin.Location = New System.Drawing.Point(126, 614)
        Me.cmbOrigin.Name = "cmbOrigin"
        Me.cmbOrigin.Size = New System.Drawing.Size(220, 21)
        Me.cmbOrigin.TabIndex = 40
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(10, 436)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(39, 13)
        Me.Label15.TabIndex = 30
        Me.Label15.Text = "Freight"
        '
        'txtFreight
        '
        Me.txtFreight.Location = New System.Drawing.Point(126, 433)
        Me.txtFreight.Name = "txtFreight"
        Me.txtFreight.Size = New System.Drawing.Size(129, 20)
        Me.txtFreight.TabIndex = 31
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(10, 436)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(57, 13)
        Me.Label14.TabIndex = 27
        Me.Label14.Text = "Insurrance"
        Me.Label14.Visible = False
        '
        'txtInsurrance
        '
        Me.txtInsurrance.Location = New System.Drawing.Point(126, 433)
        Me.txtInsurrance.Name = "txtInsurrance"
        Me.txtInsurrance.Size = New System.Drawing.Size(220, 20)
        Me.txtInsurrance.TabIndex = 28
        Me.txtInsurrance.Visible = False
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(10, 490)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(56, 13)
        Me.Label13.TabIndex = 34
        Me.Label13.Text = "BDR Date"
        '
        'dtpDBRDate
        '
        Me.dtpDBRDate.Checked = False
        Me.dtpDBRDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpDBRDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDBRDate.Location = New System.Drawing.Point(126, 486)
        Me.dtpDBRDate.Name = "dtpDBRDate"
        Me.dtpDBRDate.ShowCheckBox = True
        Me.dtpDBRDate.Size = New System.Drawing.Size(129, 20)
        Me.dtpDBRDate.TabIndex = 35
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(357, 552)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(55, 13)
        Me.Label12.TabIndex = 73
        Me.Label12.Text = "DTB Date"
        '
        'DTBDate
        '
        Me.DTBDate.Checked = False
        Me.DTBDate.CustomFormat = "dd/MMM/yyyy"
        Me.DTBDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTBDate.Location = New System.Drawing.Point(493, 548)
        Me.DTBDate.Name = "DTBDate"
        Me.DTBDate.ShowCheckBox = True
        Me.DTBDate.Size = New System.Drawing.Size(151, 20)
        Me.DTBDate.TabIndex = 74
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(357, 526)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(49, 13)
        Me.Label8.TabIndex = 71
        Me.Label8.Text = "DD Date"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(357, 500)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(49, 13)
        Me.Label9.TabIndex = 69
        Me.Label9.Text = "NN Date"
        '
        'dtpDDDate
        '
        Me.dtpDDDate.Checked = False
        Me.dtpDDDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpDDDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDDDate.Location = New System.Drawing.Point(493, 522)
        Me.dtpDDDate.Name = "dtpDDDate"
        Me.dtpDDDate.ShowCheckBox = True
        Me.dtpDDDate.Size = New System.Drawing.Size(151, 20)
        Me.dtpDDDate.TabIndex = 72
        '
        'dtpNNDate
        '
        Me.dtpNNDate.Checked = False
        Me.dtpNNDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpNNDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpNNDate.Location = New System.Drawing.Point(493, 496)
        Me.dtpNNDate.Name = "dtpNNDate"
        Me.dtpNNDate.ShowCheckBox = True
        Me.dtpNNDate.Size = New System.Drawing.Size(151, 20)
        Me.dtpNNDate.TabIndex = 70
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(357, 615)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(53, 13)
        Me.Label17.TabIndex = 77
        Me.Label17.Text = "LSB Date"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(357, 578)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(109, 13)
        Me.Label18.TabIndex = 75
        Me.Label18.Text = "Latest Date Shipment"
        '
        'dtpLSBDate
        '
        Me.dtpLSBDate.Checked = False
        Me.dtpLSBDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpLSBDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpLSBDate.Location = New System.Drawing.Point(493, 611)
        Me.dtpLSBDate.Name = "dtpLSBDate"
        Me.dtpLSBDate.ShowCheckBox = True
        Me.dtpLSBDate.Size = New System.Drawing.Size(151, 20)
        Me.dtpLSBDate.TabIndex = 78
        '
        'dtpLatestDateofShipment
        '
        Me.dtpLatestDateofShipment.Checked = False
        Me.dtpLatestDateofShipment.CustomFormat = "dd/MMM/yyyy"
        Me.dtpLatestDateofShipment.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpLatestDateofShipment.Location = New System.Drawing.Point(493, 574)
        Me.dtpLatestDateofShipment.Name = "dtpLatestDateofShipment"
        Me.dtpLatestDateofShipment.ShowCheckBox = True
        Me.dtpLatestDateofShipment.Size = New System.Drawing.Size(151, 20)
        Me.dtpLatestDateofShipment.TabIndex = 76
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(357, 365)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(89, 13)
        Me.Label11.TabIndex = 59
        Me.Label11.Text = "Port of Discharge"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(357, 338)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(75, 13)
        Me.Label10.TabIndex = 57
        Me.Label10.Tag = ""
        Me.Label10.Text = "Port of loading"
        '
        'cmbPortOfDischarge
        '
        Me.cmbPortOfDischarge.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbPortOfDischarge.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbPortOfDischarge.FormattingEnabled = True
        Me.cmbPortOfDischarge.Location = New System.Drawing.Point(493, 361)
        Me.cmbPortOfDischarge.Name = "cmbPortOfDischarge"
        Me.cmbPortOfDischarge.Size = New System.Drawing.Size(218, 21)
        Me.cmbPortOfDischarge.TabIndex = 60
        '
        'cmbPortOfLoading
        '
        Me.cmbPortOfLoading.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbPortOfLoading.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbPortOfLoading.FormattingEnabled = True
        Me.cmbPortOfLoading.Location = New System.Drawing.Point(493, 334)
        Me.cmbPortOfLoading.Name = "cmbPortOfLoading"
        Me.cmbPortOfLoading.Size = New System.Drawing.Size(218, 21)
        Me.cmbPortOfLoading.TabIndex = 58
        '
        'cmbVendor
        '
        Me.cmbVendor.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cmbVendor.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbVendor.CheckedListSettings.CheckStateMember = ""
        Appearance6.BackColor = System.Drawing.Color.White
        Appearance6.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        Me.cmbVendor.DisplayLayout.Appearance = Appearance6
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
        Me.cmbVendor.Location = New System.Drawing.Point(126, 330)
        Me.cmbVendor.Name = "cmbVendor"
        Me.cmbVendor.Size = New System.Drawing.Size(220, 22)
        Me.cmbVendor.TabIndex = 23
        '
        'grpCurrency
        '
        Me.grpCurrency.Controls.Add(Me.Label30)
        Me.grpCurrency.Controls.Add(Me.txtCurrencyRate)
        Me.grpCurrency.Controls.Add(Me.Label29)
        Me.grpCurrency.Controls.Add(Me.cmbCurrency)
        Me.grpCurrency.Location = New System.Drawing.Point(126, 542)
        Me.grpCurrency.Name = "grpCurrency"
        Me.grpCurrency.Size = New System.Drawing.Size(222, 66)
        Me.grpCurrency.TabIndex = 38
        Me.grpCurrency.TabStop = False
        Me.grpCurrency.Text = "Currency"
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(9, 33)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(30, 13)
        Me.Label30.TabIndex = 2
        Me.Label30.Text = "Rate"
        '
        'txtCurrencyRate
        '
        Me.txtCurrencyRate.Location = New System.Drawing.Point(75, 29)
        Me.txtCurrencyRate.Name = "txtCurrencyRate"
        Me.txtCurrencyRate.Size = New System.Drawing.Size(141, 20)
        Me.txtCurrencyRate.TabIndex = 3
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.Location = New System.Drawing.Point(9, 13)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(49, 13)
        Me.Label29.TabIndex = 0
        Me.Label29.Text = "Currency"
        '
        'cmbCurrency
        '
        Me.cmbCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCurrency.FormattingEnabled = True
        Me.cmbCurrency.Location = New System.Drawing.Point(75, 5)
        Me.cmbCurrency.Name = "cmbCurrency"
        Me.cmbCurrency.Size = New System.Drawing.Size(141, 21)
        Me.cmbCurrency.TabIndex = 1
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(10, 304)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(52, 13)
        Me.Label7.TabIndex = 20
        Me.Label7.Text = "Perf Date"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(10, 279)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(66, 13)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "Performa No"
        '
        'dtpPerformaDate
        '
        Me.dtpPerformaDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpPerformaDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPerformaDate.Location = New System.Drawing.Point(126, 304)
        Me.dtpPerformaDate.Name = "dtpPerformaDate"
        Me.dtpPerformaDate.Size = New System.Drawing.Size(129, 20)
        Me.dtpPerformaDate.TabIndex = 21
        '
        'txtPerformaNo
        '
        Me.txtPerformaNo.Location = New System.Drawing.Point(126, 278)
        Me.txtPerformaNo.Name = "txtPerformaNo"
        Me.txtPerformaNo.Size = New System.Drawing.Size(220, 20)
        Me.txtPerformaNo.TabIndex = 19
        '
        'Label70
        '
        Me.Label70.AutoSize = True
        Me.Label70.Location = New System.Drawing.Point(357, 642)
        Me.Label70.Name = "Label70"
        Me.Label70.Size = New System.Drawing.Size(61, 13)
        Me.Label70.TabIndex = 79
        Me.Label70.Text = "Expiry Date"
        '
        'dtpExpiryDate
        '
        Me.dtpExpiryDate.Checked = False
        Me.dtpExpiryDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpExpiryDate.Location = New System.Drawing.Point(493, 638)
        Me.dtpExpiryDate.Name = "dtpExpiryDate"
        Me.dtpExpiryDate.ShowCheckBox = True
        Me.dtpExpiryDate.Size = New System.Drawing.Size(151, 20)
        Me.dtpExpiryDate.TabIndex = 80
        '
        'Label76
        '
        Me.Label76.AutoSize = True
        Me.Label76.Location = New System.Drawing.Point(357, 232)
        Me.Label76.Name = "Label76"
        Me.Label76.Size = New System.Drawing.Size(60, 13)
        Me.Label76.TabIndex = 49
        Me.Label76.Text = "Opened By"
        '
        'cmbOpenedBy
        '
        Me.cmbOpenedBy.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbOpenedBy.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbOpenedBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOpenedBy.FormattingEnabled = True
        Me.cmbOpenedBy.Location = New System.Drawing.Point(493, 229)
        Me.cmbOpenedBy.Name = "cmbOpenedBy"
        Me.cmbOpenedBy.Size = New System.Drawing.Size(218, 21)
        Me.cmbOpenedBy.TabIndex = 50
        '
        'Label85
        '
        Me.Label85.AutoSize = True
        Me.Label85.Location = New System.Drawing.Point(357, 419)
        Me.Label85.Name = "Label85"
        Me.Label85.Size = New System.Drawing.Size(61, 13)
        Me.Label85.TabIndex = 63
        Me.Label85.Text = "Transporter"
        '
        'Label84
        '
        Me.Label84.AutoSize = True
        Me.Label84.Location = New System.Drawing.Point(357, 392)
        Me.Label84.Name = "Label84"
        Me.Label84.Size = New System.Drawing.Size(76, 13)
        Me.Label84.TabIndex = 61
        Me.Label84.Text = "Clearing Agent"
        '
        'Label81
        '
        Me.Label81.AutoSize = True
        Me.Label81.Location = New System.Drawing.Point(357, 474)
        Me.Label81.Name = "Label81"
        Me.Label81.Size = New System.Drawing.Size(54, 13)
        Me.Label81.TabIndex = 67
        Me.Label81.Text = "ETA Date"
        '
        'Label80
        '
        Me.Label80.AutoSize = True
        Me.Label80.Location = New System.Drawing.Point(357, 448)
        Me.Label80.Name = "Label80"
        Me.Label80.Size = New System.Drawing.Size(55, 13)
        Me.Label80.TabIndex = 65
        Me.Label80.Text = "ETD Date"
        '
        'Label79
        '
        Me.Label79.AutoSize = True
        Me.Label79.Location = New System.Drawing.Point(357, 311)
        Me.Label79.Name = "Label79"
        Me.Label79.Size = New System.Drawing.Size(51, 13)
        Me.Label79.TabIndex = 55
        Me.Label79.Text = "B/L Date"
        '
        'Label78
        '
        Me.Label78.AutoSize = True
        Me.Label78.Location = New System.Drawing.Point(357, 285)
        Me.Label78.Name = "Label78"
        Me.Label78.Size = New System.Drawing.Size(45, 13)
        Me.Label78.TabIndex = 53
        Me.Label78.Text = "B/L No."
        '
        'Label77
        '
        Me.Label77.AutoSize = True
        Me.Label77.Location = New System.Drawing.Point(357, 259)
        Me.Label77.Name = "Label77"
        Me.Label77.Size = New System.Drawing.Size(38, 13)
        Me.Label77.TabIndex = 51
        Me.Label77.Text = "Vessel"
        '
        'cmbTransporter
        '
        Me.cmbTransporter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbTransporter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbTransporter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTransporter.FormattingEnabled = True
        Me.cmbTransporter.Location = New System.Drawing.Point(493, 416)
        Me.cmbTransporter.Name = "cmbTransporter"
        Me.cmbTransporter.Size = New System.Drawing.Size(218, 21)
        Me.cmbTransporter.TabIndex = 64
        '
        'cmbClearingAgent
        '
        Me.cmbClearingAgent.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbClearingAgent.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbClearingAgent.FormattingEnabled = True
        Me.cmbClearingAgent.Location = New System.Drawing.Point(493, 389)
        Me.cmbClearingAgent.Name = "cmbClearingAgent"
        Me.cmbClearingAgent.Size = New System.Drawing.Size(218, 21)
        Me.cmbClearingAgent.TabIndex = 62
        '
        'dtpETADate
        '
        Me.dtpETADate.Checked = False
        Me.dtpETADate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpETADate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpETADate.Location = New System.Drawing.Point(493, 470)
        Me.dtpETADate.Name = "dtpETADate"
        Me.dtpETADate.ShowCheckBox = True
        Me.dtpETADate.Size = New System.Drawing.Size(151, 20)
        Me.dtpETADate.TabIndex = 68
        '
        'dtpETDDate
        '
        Me.dtpETDDate.Checked = False
        Me.dtpETDDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpETDDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpETDDate.Location = New System.Drawing.Point(493, 444)
        Me.dtpETDDate.Name = "dtpETDDate"
        Me.dtpETDDate.ShowCheckBox = True
        Me.dtpETDDate.Size = New System.Drawing.Size(151, 20)
        Me.dtpETDDate.TabIndex = 66
        '
        'dtpBLDate
        '
        Me.dtpBLDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpBLDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpBLDate.Location = New System.Drawing.Point(493, 308)
        Me.dtpBLDate.Name = "dtpBLDate"
        Me.dtpBLDate.Size = New System.Drawing.Size(151, 20)
        Me.dtpBLDate.TabIndex = 56
        '
        'txtBLNo
        '
        Me.txtBLNo.Location = New System.Drawing.Point(493, 282)
        Me.txtBLNo.Name = "txtBLNo"
        Me.txtBLNo.Size = New System.Drawing.Size(151, 20)
        Me.txtBLNo.TabIndex = 54
        '
        'txtVessel
        '
        Me.txtVessel.Location = New System.Drawing.Point(493, 256)
        Me.txtVessel.Name = "txtVessel"
        Me.txtVessel.Size = New System.Drawing.Size(218, 20)
        Me.txtVessel.TabIndex = 52
        '
        'Label68
        '
        Me.Label68.AutoSize = True
        Me.Label68.Location = New System.Drawing.Point(10, 252)
        Me.Label68.Name = "Label68"
        Me.Label68.Size = New System.Drawing.Size(41, 13)
        Me.Label68.TabIndex = 16
        Me.Label68.Text = "Ref No"
        '
        'txtRefNo
        '
        Me.txtRefNo.Location = New System.Drawing.Point(126, 252)
        Me.txtRefNo.Name = "txtRefNo"
        Me.txtRefNo.Size = New System.Drawing.Size(220, 20)
        Me.txtRefNo.TabIndex = 17
        '
        'Label74
        '
        Me.Label74.AutoSize = True
        Me.Label74.Location = New System.Drawing.Point(10, 226)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(94, 13)
        Me.Label74.TabIndex = 14
        Me.Label74.Text = "Sepcial Instruction"
        '
        'Label73
        '
        Me.Label73.AutoSize = True
        Me.Label73.Location = New System.Drawing.Point(10, 202)
        Me.Label73.Name = "Label73"
        Me.Label73.Size = New System.Drawing.Size(75, 13)
        Me.Label73.TabIndex = 12
        Me.Label73.Text = "Advising Bank"
        '
        'txtSpecialInstruction
        '
        Me.txtSpecialInstruction.Location = New System.Drawing.Point(126, 226)
        Me.txtSpecialInstruction.Name = "txtSpecialInstruction"
        Me.txtSpecialInstruction.Size = New System.Drawing.Size(220, 20)
        Me.txtSpecialInstruction.TabIndex = 15
        '
        'txtAdvisingBank
        '
        Me.txtAdvisingBank.Location = New System.Drawing.Point(126, 201)
        Me.txtAdvisingBank.Name = "txtAdvisingBank"
        Me.txtAdvisingBank.Size = New System.Drawing.Size(220, 20)
        Me.txtAdvisingBank.TabIndex = 13
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(357, 208)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(62, 13)
        Me.Label5.TabIndex = 47
        Me.Label5.Text = "Cost Center"
        '
        'cmbCostCenter
        '
        Me.cmbCostCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCostCenter.FormattingEnabled = True
        Me.cmbCostCenter.Items.AddRange(New Object() {"LC", "TT"})
        Me.cmbCostCenter.Location = New System.Drawing.Point(493, 204)
        Me.cmbCostCenter.Name = "cmbCostCenter"
        Me.cmbCostCenter.Size = New System.Drawing.Size(218, 21)
        Me.cmbCostCenter.TabIndex = 48
        '
        'chkActive
        '
        Me.chkActive.AutoSize = True
        Me.chkActive.Checked = True
        Me.chkActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkActive.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.chkActive.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.chkActive.Location = New System.Drawing.Point(493, 666)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(62, 18)
        Me.chkActive.TabIndex = 41
        Me.chkActive.Text = "Active"
        Me.chkActive.UseVisualStyleBackColor = True
        '
        'Groupbox
        '
        Me.Groupbox.Controls.Add(Me.lblchequeno)
        Me.Groupbox.Controls.Add(Me.dateCheque)
        Me.Groupbox.Controls.Add(Me.lbldatecheque)
        Me.Groupbox.Controls.Add(Me.txtChequeNo)
        Me.Groupbox.Controls.Add(Me.lblProgress)
        Me.Groupbox.Location = New System.Drawing.Point(493, 125)
        Me.Groupbox.Name = "Groupbox"
        Me.Groupbox.Size = New System.Drawing.Size(222, 70)
        Me.Groupbox.TabIndex = 46
        Me.Groupbox.TabStop = False
        Me.Groupbox.Text = "Cheque Detail"
        '
        'lblchequeno
        '
        Me.lblchequeno.AutoSize = True
        Me.lblchequeno.Location = New System.Drawing.Point(7, 18)
        Me.lblchequeno.Name = "lblchequeno"
        Me.lblchequeno.Size = New System.Drawing.Size(61, 13)
        Me.lblchequeno.TabIndex = 0
        Me.lblchequeno.Text = "Cheque No"
        '
        'dateCheque
        '
        Me.dateCheque.CustomFormat = "dd/MMM/yyyy"
        Me.dateCheque.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dateCheque.Location = New System.Drawing.Point(85, 40)
        Me.dateCheque.Name = "dateCheque"
        Me.dateCheque.Size = New System.Drawing.Size(133, 20)
        Me.dateCheque.TabIndex = 3
        '
        'lbldatecheque
        '
        Me.lbldatecheque.AutoSize = True
        Me.lbldatecheque.Location = New System.Drawing.Point(7, 44)
        Me.lbldatecheque.Name = "lbldatecheque"
        Me.lbldatecheque.Size = New System.Drawing.Size(70, 13)
        Me.lbldatecheque.TabIndex = 2
        Me.lbldatecheque.Text = "Cheque Date"
        '
        'txtChequeNo
        '
        Me.txtChequeNo.Location = New System.Drawing.Point(85, 14)
        Me.txtChequeNo.Name = "txtChequeNo"
        Me.txtChequeNo.Size = New System.Drawing.Size(133, 20)
        Me.txtChequeNo.TabIndex = 1
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(145, 117)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 4
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd/MMM/yyyy"
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(126, 97)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(220, 20)
        Me.DateTimePicker1.TabIndex = 4
        '
        'txtremarks
        '
        Me.txtremarks.Location = New System.Drawing.Point(126, 512)
        Me.txtremarks.Multiline = True
        Me.txtremarks.Name = "txtremarks"
        Me.txtremarks.Size = New System.Drawing.Size(220, 24)
        Me.txtremarks.TabIndex = 37
        '
        'txtretiringamount
        '
        Me.txtretiringamount.Location = New System.Drawing.Point(126, 408)
        Me.txtretiringamount.Name = "txtretiringamount"
        Me.txtretiringamount.ReadOnly = True
        Me.txtretiringamount.Size = New System.Drawing.Size(220, 20)
        Me.txtretiringamount.TabIndex = 29
        '
        'txtpaidamount
        '
        Me.txtpaidamount.Location = New System.Drawing.Point(126, 383)
        Me.txtpaidamount.Name = "txtpaidamount"
        Me.txtpaidamount.Size = New System.Drawing.Size(220, 20)
        Me.txtpaidamount.TabIndex = 27
        '
        'txtLCamount
        '
        Me.txtLCamount.Location = New System.Drawing.Point(126, 356)
        Me.txtLCamount.Name = "txtLCamount"
        Me.txtLCamount.Size = New System.Drawing.Size(220, 20)
        Me.txtLCamount.TabIndex = 25
        '
        'txtdocnumber
        '
        Me.txtdocnumber.Location = New System.Drawing.Point(126, 71)
        Me.txtdocnumber.Name = "txtdocnumber"
        Me.txtdocnumber.Size = New System.Drawing.Size(220, 20)
        Me.txtdocnumber.TabIndex = 2
        '
        'cmbtypeCFD
        '
        Me.cmbtypeCFD.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbtypeCFD.FormattingEnabled = True
        Me.cmbtypeCFD.Items.AddRange(New Object() {"CFR", "FOB"})
        Me.cmbtypeCFD.Location = New System.Drawing.Point(126, 459)
        Me.cmbtypeCFD.Name = "cmbtypeCFD"
        Me.cmbtypeCFD.Size = New System.Drawing.Size(220, 21)
        Me.cmbtypeCFD.TabIndex = 33
        '
        'cmbbank
        '
        Me.cmbbank.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbbank.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbbank.FormattingEnabled = True
        Me.cmbbank.Location = New System.Drawing.Point(126, 150)
        Me.cmbbank.Name = "cmbbank"
        Me.cmbbank.Size = New System.Drawing.Size(220, 21)
        Me.cmbbank.TabIndex = 8
        '
        'cmbpaymentfrom
        '
        Me.cmbpaymentfrom.FormattingEnabled = True
        Me.cmbpaymentfrom.Items.AddRange(New Object() {"LC", "TT"})
        Me.cmbpaymentfrom.Location = New System.Drawing.Point(493, 97)
        Me.cmbpaymentfrom.Name = "cmbpaymentfrom"
        Me.cmbpaymentfrom.Size = New System.Drawing.Size(218, 21)
        Me.cmbpaymentfrom.TabIndex = 45
        '
        'cmbmethod
        '
        Me.cmbmethod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbmethod.FormattingEnabled = True
        Me.cmbmethod.Items.AddRange(New Object() {"LC", "TT"})
        Me.cmbmethod.Location = New System.Drawing.Point(493, 71)
        Me.cmbmethod.Name = "cmbmethod"
        Me.cmbmethod.Size = New System.Drawing.Size(218, 21)
        Me.cmbmethod.TabIndex = 43
        '
        'cmbdoctype
        '
        Me.cmbdoctype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbdoctype.FormattingEnabled = True
        Me.cmbdoctype.Items.AddRange(New Object() {"LC", "TT"})
        Me.cmbdoctype.Location = New System.Drawing.Point(126, 123)
        Me.cmbdoctype.Name = "cmbdoctype"
        Me.cmbdoctype.Size = New System.Drawing.Size(220, 21)
        Me.cmbdoctype.TabIndex = 6
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 515)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 36
        Me.Label2.Text = "Remarks"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 411)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(82, 13)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "Retiring Amount"
        '
        'lblCrunRate
        '
        Me.lblCrunRate.AutoSize = True
        Me.lblCrunRate.Location = New System.Drawing.Point(10, 385)
        Me.lblCrunRate.Name = "lblCrunRate"
        Me.lblCrunRate.Size = New System.Drawing.Size(67, 13)
        Me.lblCrunRate.TabIndex = 26
        Me.lblCrunRate.Text = "Paid Amount"
        '
        'LBLCURRENCY
        '
        Me.LBLCURRENCY.AutoSize = True
        Me.LBLCURRENCY.Location = New System.Drawing.Point(10, 358)
        Me.LBLCURRENCY.Name = "LBLCURRENCY"
        Me.LBLCURRENCY.Size = New System.Drawing.Size(59, 13)
        Me.LBLCURRENCY.TabIndex = 24
        Me.LBLCURRENCY.Text = "LC Amount"
        '
        'lblTFDFOB
        '
        Me.lblTFDFOB.AutoSize = True
        Me.lblTFDFOB.Location = New System.Drawing.Point(10, 463)
        Me.lblTFDFOB.Name = "lblTFDFOB"
        Me.lblTFDFOB.Size = New System.Drawing.Size(31, 13)
        Me.lblTFDFOB.TabIndex = 32
        Me.lblTFDFOB.Text = "Type"
        '
        'lblvandor
        '
        Me.lblvandor.AutoSize = True
        Me.lblvandor.Location = New System.Drawing.Point(10, 331)
        Me.lblvandor.Name = "lblvandor"
        Me.lblvandor.Size = New System.Drawing.Size(63, 13)
        Me.lblvandor.TabIndex = 22
        Me.lblvandor.Text = "LC Account"
        '
        'lblBank
        '
        Me.lblBank.AutoSize = True
        Me.lblBank.Location = New System.Drawing.Point(10, 154)
        Me.lblBank.Name = "lblBank"
        Me.lblBank.Size = New System.Drawing.Size(32, 13)
        Me.lblBank.TabIndex = 7
        Me.lblBank.Text = "Bank"
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.Location = New System.Drawing.Point(10, 101)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(82, 13)
        Me.lblDate.TabIndex = 3
        Me.lblDate.Text = "Document Date"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(357, 101)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(74, 13)
        Me.Label4.TabIndex = 44
        Me.Label4.Text = "Payment From"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(357, 75)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 13)
        Me.Label3.TabIndex = 42
        Me.Label3.Text = "Method"
        '
        'lblNo
        '
        Me.lblNo.AutoSize = True
        Me.lblNo.Location = New System.Drawing.Point(10, 75)
        Me.lblNo.Name = "lblNo"
        Me.lblNo.Size = New System.Drawing.Size(73, 13)
        Me.lblNo.TabIndex = 1
        Me.lblNo.Text = "Document No"
        '
        'lblType
        '
        Me.lblType.AutoSize = True
        Me.lblType.Location = New System.Drawing.Point(10, 127)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(83, 13)
        Me.lblType.TabIndex = 5
        Me.lblType.Text = "Document Type"
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.AutoScroll = True
        Me.UltraTabPageControl2.Controls.Add(Me.grdLetterOfCreadit)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(838, 692)
        '
        'grdLetterOfCreadit
        '
        Me.grdLetterOfCreadit.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdLetterOfCreadit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdLetterOfCreadit.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdLetterOfCreadit.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdLetterOfCreadit.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdLetterOfCreadit.GroupByBoxVisible = False
        Me.grdLetterOfCreadit.Location = New System.Drawing.Point(0, 0)
        Me.grdLetterOfCreadit.Name = "grdLetterOfCreadit"
        Me.grdLetterOfCreadit.RecordNavigator = True
        Me.grdLetterOfCreadit.Size = New System.Drawing.Size(838, 692)
        Me.grdLetterOfCreadit.TabIndex = 0
        Me.grdLetterOfCreadit.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
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
        Me.UltraTabControl1.Size = New System.Drawing.Size(840, 713)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 1
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Letter of Credit"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "History"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(838, 692)
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(798, 0)
        Me.CtrlGrdBar1.MyGrid = Me.grdLetterOfCreadit
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(42, 26)
        Me.CtrlGrdBar1.TabIndex = 18
        '
        'frmLetterCredit
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(840, 738)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.KeyPreview = True
        Me.Name = "frmLetterCredit"
        Me.Text = "Letter of Credit"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.cmbVendor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpCurrency.ResumeLayout(False)
        Me.grpCurrency.PerformLayout()
        Me.Groupbox.ResumeLayout(False)
        Me.Groupbox.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdLetterOfCreadit, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents lblCrunRate As System.Windows.Forms.Label
    Friend WithEvents LBLCURRENCY As System.Windows.Forms.Label
    Friend WithEvents lblTFDFOB As System.Windows.Forms.Label
    Friend WithEvents lblvandor As System.Windows.Forms.Label
    Friend WithEvents lblBank As System.Windows.Forms.Label
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents lblNo As System.Windows.Forms.Label
    Friend WithEvents lblType As System.Windows.Forms.Label
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents cmbtypeCFD As System.Windows.Forms.ComboBox
    Friend WithEvents cmbbank As System.Windows.Forms.ComboBox
    Friend WithEvents cmbdoctype As System.Windows.Forms.ComboBox
    Friend WithEvents txtpaidamount As System.Windows.Forms.TextBox
    Friend WithEvents txtLCamount As System.Windows.Forms.TextBox
    Friend WithEvents txtdocnumber As System.Windows.Forms.TextBox
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents grdLetterOfCreadit As Janus.Windows.GridEX.GridEX
    Friend WithEvents txtremarks As System.Windows.Forms.TextBox
    Friend WithEvents txtretiringamount As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Groupbox As System.Windows.Forms.GroupBox
    Friend WithEvents lbldatecheque As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblchequeno As System.Windows.Forms.Label
    Friend WithEvents cmbpaymentfrom As System.Windows.Forms.ComboBox
    Friend WithEvents cmbmethod As System.Windows.Forms.ComboBox
    Friend WithEvents dateCheque As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtChequeNo As System.Windows.Forms.TextBox
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbCostCenter As System.Windows.Forms.ComboBox
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents CashAccountToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BankAccountToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CostCenterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents Label73 As System.Windows.Forms.Label
    Friend WithEvents txtSpecialInstruction As System.Windows.Forms.TextBox
    Friend WithEvents txtAdvisingBank As System.Windows.Forms.TextBox
    Friend WithEvents Label68 As System.Windows.Forms.Label
    Friend WithEvents txtRefNo As System.Windows.Forms.TextBox
    Friend WithEvents Label85 As System.Windows.Forms.Label
    Friend WithEvents Label84 As System.Windows.Forms.Label
    Friend WithEvents Label81 As System.Windows.Forms.Label
    Friend WithEvents Label80 As System.Windows.Forms.Label
    Friend WithEvents Label79 As System.Windows.Forms.Label
    Friend WithEvents Label78 As System.Windows.Forms.Label
    Friend WithEvents Label77 As System.Windows.Forms.Label
    Friend WithEvents cmbTransporter As System.Windows.Forms.ComboBox
    Friend WithEvents cmbClearingAgent As System.Windows.Forms.ComboBox
    Friend WithEvents dtpETADate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpETDDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpBLDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtBLNo As System.Windows.Forms.TextBox
    Friend WithEvents txtVessel As System.Windows.Forms.TextBox
    Friend WithEvents Label76 As System.Windows.Forms.Label
    Friend WithEvents cmbOpenedBy As System.Windows.Forms.ComboBox
    Friend WithEvents Label70 As System.Windows.Forms.Label
    Friend WithEvents dtpExpiryDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dtpPerformaDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtPerformaNo As System.Windows.Forms.TextBox
    Friend WithEvents grpCurrency As System.Windows.Forms.GroupBox
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtCurrencyRate As System.Windows.Forms.TextBox
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents cmbCurrency As System.Windows.Forms.ComboBox
    Friend WithEvents VendorLCAccountToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmbVendor As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmbPortOfDischarge As System.Windows.Forms.ComboBox
    Friend WithEvents cmbPortOfLoading As System.Windows.Forms.ComboBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents dtpLSBDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpLatestDateofShipment As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents txtInsurrance As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents dtpDBRDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents DTBDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents dtpDDDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpNNDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtFreight As System.Windows.Forms.TextBox
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsbTask As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbConfig As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents lblOrigin As System.Windows.Forms.Label
    Friend WithEvents cmbOrigin As System.Windows.Forms.ComboBox
    Friend WithEvents lblBankCreditLimit As System.Windows.Forms.Label
    Friend WithEvents txtCreditLimit As System.Windows.Forms.TextBox
    Friend WithEvents btnSaveCreditLimit As System.Windows.Forms.Button
    Friend WithEvents chkClose As System.Windows.Forms.CheckBox
    Friend WithEvents txtCostOfMaterial As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
End Class

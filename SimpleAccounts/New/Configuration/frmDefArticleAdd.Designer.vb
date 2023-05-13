'' 19-Dec-2013   Imran Ali Unlimited Space Required in New Inventory Item Tab 
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDefArticleAdd
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
        Dim lblHeader As System.Windows.Forms.Label
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance17 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance20 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance22 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance18 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance16 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance15 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance21 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance19 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDefArticleAdd))
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn1 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Id")
        Dim UltraGridColumn2 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleDescription")
        Dim UltraGridColumn3 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Size")
        Dim UltraGridColumn4 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Combination")
        Dim UltraGridColumn5 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Price", 0)
        Dim UltraGridColumn6 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Stock", 1)
        Dim UltraGridColumn7 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("PurchasePrice", 2)
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand2 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn8 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Id")
        Dim UltraGridColumn9 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleDescription")
        Dim UltraGridColumn10 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Size")
        Dim UltraGridColumn11 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Combination")
        Dim UltraGridColumn12 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Price", 0)
        Dim UltraGridColumn13 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Stock", 1)
        Dim UltraGridColumn14 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("PurchasePrice", 2)
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim grdVendorItems_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdCustomerList_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdPackQty_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.cmbLPO = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.chkManufacturing = New System.Windows.Forms.CheckBox()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.btnAddBrand = New System.Windows.Forms.Button()
        Me.lblBrand = New System.Windows.Forms.Label()
        Me.Label41 = New System.Windows.Forms.Label()
        Me.cmbBrand = New System.Windows.Forms.ComboBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.Label39 = New System.Windows.Forms.Label()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.txtHSCode = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.chkApplyAdjustmentFuelExpense = New System.Windows.Forms.CheckBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtCostPrice = New System.Windows.Forms.TextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.txtLargestPackQty = New System.Windows.Forms.TextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.txtItemWeight = New System.Windows.Forms.TextBox()
        Me.uitxtSortOrder = New System.Windows.Forms.TextBox()
        Me.label8 = New System.Windows.Forms.Label()
        Me.label9 = New System.Windows.Forms.Label()
        Me.uitxtPackQty = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.uitxtPrice = New System.Windows.Forms.TextBox()
        Me.uitxtSalePrice = New System.Windows.Forms.TextBox()
        Me.uichkActive = New System.Windows.Forms.CheckBox()
        Me.lnkUploadPic = New System.Windows.Forms.LinkLabel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.BtnNew = New System.Windows.Forms.ToolStripButton()
        Me.BtnSave = New System.Windows.Forms.ToolStripButton()
        Me.BtnDelete = New System.Windows.Forms.ToolStripButton()
        Me.BtnCancel = New System.Windows.Forms.ToolStripButton()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.BtnPrint = New System.Windows.Forms.ToolStripSplitButton()
        Me.PrintCostSheetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintAllCostSheetToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnAddAccounts = New System.Windows.Forms.ToolStripSplitButton()
        Me.AddNewVendorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnAttachments = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnPriceUpdate = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnCostPriceUpdate = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.chkServerItem = New System.Windows.Forms.CheckBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.tabAriticalDetail = New System.Windows.Forms.TabControl()
        Me.tabPgInfo = New System.Windows.Forms.TabPage()
        Me.lnkAddModel = New System.Windows.Forms.Label()
        Me.lblModelList = New System.Windows.Forms.Label()
        Me.lstModelList = New SimpleAccounts.uiListControl()
        Me.lnkAddCombination = New System.Windows.Forms.Label()
        Me.lnkAddSize = New System.Windows.Forms.Label()
        Me.txtOldSalePrice = New System.Windows.Forms.TextBox()
        Me.txtOldPurchasePrice = New System.Windows.Forms.TextBox()
        Me.lblSize = New System.Windows.Forms.Label()
        Me.lblCombinition = New System.Windows.Forms.Label()
        Me.lstSizes = New SimpleAccounts.uiListControl()
        Me.lstCombinitions = New SimpleAccounts.uiListControl()
        Me.tabPgArticalDetail = New System.Windows.Forms.TabPage()
        Me.grdAriticals = New Janus.Windows.GridEX.GridEX()
        Me.tabPgLocation = New System.Windows.Forms.TabPage()
        Me.grdItemLocation = New Janus.Windows.GridEX.GridEX()
        Me.tabArticleAlias = New System.Windows.Forms.TabPage()
        Me.btnAddArticleAlias = New System.Windows.Forms.Button()
        Me.chkActive = New System.Windows.Forms.CheckBox()
        Me.Label42 = New System.Windows.Forms.Label()
        Me.cmbVendor = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txtArticleAliasName = New System.Windows.Forms.TextBox()
        Me.txtArticleAliasCode = New System.Windows.Forms.TextBox()
        Me.Label43 = New System.Windows.Forms.Label()
        Me.lblArticleAliasCode = New System.Windows.Forms.Label()
        Me.grpArticleAlias = New System.Windows.Forms.GroupBox()
        Me.grdArticleAlias = New Janus.Windows.GridEX.GridEX()
        Me.TabPgCostSheet = New System.Windows.Forms.TabPage()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.rdoName = New System.Windows.Forms.RadioButton()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.cmbItem = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.RdoCode = New System.Windows.Forms.RadioButton()
        Me.cmbRemarks = New System.Windows.Forms.ComboBox()
        Me.txtCostSheet = New System.Windows.Forms.TextBox()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.btnAddCostSheet = New System.Windows.Forms.Button()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtTaxPercent = New System.Windows.Forms.TextBox()
        Me.cmbCostSheetUnit = New System.Windows.Forms.ComboBox()
        Me.cmbCategorys = New System.Windows.Forms.ComboBox()
        Me.grpSpecification = New System.Windows.Forms.GroupBox()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Label40 = New System.Windows.Forms.Label()
        Me.cmbSpecification = New System.Windows.Forms.ComboBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.grdCostSheet = New Janus.Windows.GridEX.GridEX()
        Me.TabPgVendorItems = New System.Windows.Forms.TabPage()
        Me.grdVendorItems = New Janus.Windows.GridEX.GridEX()
        Me.btnadd = New System.Windows.Forms.Button()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.lblvendorslist = New System.Windows.Forms.Label()
        Me.cmbvendorslist = New System.Windows.Forms.ComboBox()
        Me.tbCustomer = New System.Windows.Forms.TabPage()
        Me.grdCustomerList = New Janus.Windows.GridEX.GridEX()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.CostManagement = New System.Windows.Forms.TabPage()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.txtFlatRate = New System.Windows.Forms.TextBox()
        Me.rbtFlatRate = New System.Windows.Forms.RadioButton()
        Me.rbtTax = New System.Windows.Forms.RadioButton()
        Me.txtMarketReturns = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtFreight = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtTradePrice = New System.Windows.Forms.TextBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.TabPackQuantity = New System.Windows.Forms.TabPage()
        Me.grdPackQty = New Janus.Windows.GridEX.GridEX()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.btnAddPackQty = New System.Windows.Forms.Button()
        Me.txtQuantity = New System.Windows.Forms.TextBox()
        Me.txtPackName = New System.Windows.Forms.TextBox()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.btnAddUnit = New System.Windows.Forms.Button()
        Me.btnAddCompany = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbCompany = New System.Windows.Forms.ComboBox()
        Me.lblUnit = New System.Windows.Forms.Label()
        Me.lblCompany = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblCategory = New System.Windows.Forms.Label()
        Me.btnAddLPO = New System.Windows.Forms.Button()
        Me.btnAddType = New System.Windows.Forms.Button()
        Me.btnAddGender = New System.Windows.Forms.Button()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cmbUnit = New System.Windows.Forms.ComboBox()
        Me.lblType = New System.Windows.Forms.Label()
        Me.cmbGender = New System.Windows.Forms.ComboBox()
        Me.cmbType = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.uitxtStockLevelMaximum = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.uitxtStockLevelOptimal = New System.Windows.Forms.TextBox()
        Me.uitxtStockLevel = New System.Windows.Forms.TextBox()
        Me.cmbCategory = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.uitxtItemName = New System.Windows.Forms.TextBox()
        Me.uitxtItemCode = New System.Windows.Forms.TextBox()
        Me.lblGender = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.grdAllRecords = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.btnHistoryCancel = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.btnPrintItems = New System.Windows.Forms.ToolStripSplitButton()
        Me.PrintCostSheetToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.PrintAllCostSheetToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnHistoryHelp = New System.Windows.Forms.ToolStripButton()
        Me.BindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.FontDialog1 = New System.Windows.Forms.FontDialog()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.OpenFileDialog2 = New System.Windows.Forms.OpenFileDialog()
        lblHeader = New System.Windows.Forms.Label()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        CType(Me.cmbLPO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.tabAriticalDetail.SuspendLayout()
        Me.tabPgInfo.SuspendLayout()
        Me.tabPgArticalDetail.SuspendLayout()
        CType(Me.grdAriticals, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabPgLocation.SuspendLayout()
        CType(Me.grdItemLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabArticleAlias.SuspendLayout()
        CType(Me.cmbVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpArticleAlias.SuspendLayout()
        CType(Me.grdArticleAlias, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPgCostSheet.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        CType(Me.cmbItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpSpecification.SuspendLayout()
        CType(Me.grdCostSheet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPgVendorItems.SuspendLayout()
        CType(Me.grdVendorItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbCustomer.SuspendLayout()
        CType(Me.grdCustomerList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.CostManagement.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.TabPackQuantity.SuspendLayout()
        CType(Me.grdPackQty, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdAllRecords, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip2.SuspendLayout()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblHeader
        '
        lblHeader.AutoSize = True
        lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        lblHeader.ForeColor = System.Drawing.Color.Black
        lblHeader.Location = New System.Drawing.Point(10, 7)
        lblHeader.Name = "lblHeader"
        lblHeader.Size = New System.Drawing.Size(231, 36)
        lblHeader.TabIndex = 1
        lblHeader.Text = "Inventory Items"
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.AutoScroll = True
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbLPO)
        Me.UltraTabPageControl1.Controls.Add(Me.chkManufacturing)
        Me.UltraTabPageControl1.Controls.Add(Me.lblProgress)
        Me.UltraTabPageControl1.Controls.Add(Me.btnAddBrand)
        Me.UltraTabPageControl1.Controls.Add(Me.lblBrand)
        Me.UltraTabPageControl1.Controls.Add(Me.Label41)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbBrand)
        Me.UltraTabPageControl1.Controls.Add(Me.Label38)
        Me.UltraTabPageControl1.Controls.Add(Me.Label39)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbStatus)
        Me.UltraTabPageControl1.Controls.Add(Me.Label29)
        Me.UltraTabPageControl1.Controls.Add(Me.txtHSCode)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox3)
        Me.UltraTabPageControl1.Controls.Add(Me.uichkActive)
        Me.UltraTabPageControl1.Controls.Add(Me.lnkUploadPic)
        Me.UltraTabPageControl1.Controls.Add(Me.PictureBox1)
        Me.UltraTabPageControl1.Controls.Add(Me.ToolStrip1)
        Me.UltraTabPageControl1.Controls.Add(Me.chkServerItem)
        Me.UltraTabPageControl1.Controls.Add(Me.Button1)
        Me.UltraTabPageControl1.Controls.Add(Me.tabAriticalDetail)
        Me.UltraTabPageControl1.Controls.Add(Me.Label3)
        Me.UltraTabPageControl1.Controls.Add(Me.txtRemarks)
        Me.UltraTabPageControl1.Controls.Add(Me.btnAddUnit)
        Me.UltraTabPageControl1.Controls.Add(Me.btnAddCompany)
        Me.UltraTabPageControl1.Controls.Add(Me.Label10)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbCompany)
        Me.UltraTabPageControl1.Controls.Add(Me.lblUnit)
        Me.UltraTabPageControl1.Controls.Add(Me.lblCompany)
        Me.UltraTabPageControl1.Controls.Add(Me.Label7)
        Me.UltraTabPageControl1.Controls.Add(Me.lblCategory)
        Me.UltraTabPageControl1.Controls.Add(Me.btnAddLPO)
        Me.UltraTabPageControl1.Controls.Add(Me.btnAddType)
        Me.UltraTabPageControl1.Controls.Add(Me.btnAddGender)
        Me.UltraTabPageControl1.Controls.Add(Me.Label14)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbUnit)
        Me.UltraTabPageControl1.Controls.Add(Me.lblType)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbGender)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbType)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox1)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbCategory)
        Me.UltraTabPageControl1.Controls.Add(Me.Label2)
        Me.UltraTabPageControl1.Controls.Add(Me.Label19)
        Me.UltraTabPageControl1.Controls.Add(Me.Label6)
        Me.UltraTabPageControl1.Controls.Add(Me.uitxtItemName)
        Me.UltraTabPageControl1.Controls.Add(Me.uitxtItemCode)
        Me.UltraTabPageControl1.Controls.Add(Me.lblGender)
        Me.UltraTabPageControl1.Controls.Add(Me.Label20)
        Me.UltraTabPageControl1.Controls.Add(Me.Label1)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(869, 706)
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 25)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(869, 38)
        Me.pnlHeader.TabIndex = 89
        '
        'cmbLPO
        '
        Me.cmbLPO.CheckedListSettings.CheckStateMember = ""
        Appearance11.BackColor = System.Drawing.SystemColors.Window
        Appearance11.BorderColor = System.Drawing.SystemColors.InactiveCaption
        Me.cmbLPO.DisplayLayout.Appearance = Appearance11
        Me.cmbLPO.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.cmbLPO.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance12.BackColor = System.Drawing.SystemColors.ActiveBorder
        Appearance12.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance12.BorderColor = System.Drawing.SystemColors.Window
        Me.cmbLPO.DisplayLayout.GroupByBox.Appearance = Appearance12
        Appearance14.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cmbLPO.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance14
        Me.cmbLPO.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance13.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance13.BackColor2 = System.Drawing.SystemColors.Control
        Appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance13.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cmbLPO.DisplayLayout.GroupByBox.PromptAppearance = Appearance13
        Me.cmbLPO.DisplayLayout.MaxColScrollRegions = 1
        Me.cmbLPO.DisplayLayout.MaxRowScrollRegions = 1
        Appearance17.BackColor = System.Drawing.SystemColors.Window
        Appearance17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmbLPO.DisplayLayout.Override.ActiveCellAppearance = Appearance17
        Appearance20.BackColor = System.Drawing.SystemColors.Highlight
        Appearance20.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.cmbLPO.DisplayLayout.Override.ActiveRowAppearance = Appearance20
        Me.cmbLPO.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.cmbLPO.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance22.BackColor = System.Drawing.SystemColors.Window
        Me.cmbLPO.DisplayLayout.Override.CardAreaAppearance = Appearance22
        Appearance18.BorderColor = System.Drawing.Color.Silver
        Appearance18.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.cmbLPO.DisplayLayout.Override.CellAppearance = Appearance18
        Me.cmbLPO.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.cmbLPO.DisplayLayout.Override.CellPadding = 0
        Appearance16.BackColor = System.Drawing.SystemColors.Control
        Appearance16.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance16.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance16.BorderColor = System.Drawing.SystemColors.Window
        Me.cmbLPO.DisplayLayout.Override.GroupByRowAppearance = Appearance16
        Appearance15.TextHAlignAsString = "Left"
        Me.cmbLPO.DisplayLayout.Override.HeaderAppearance = Appearance15
        Me.cmbLPO.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.cmbLPO.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance21.BackColor = System.Drawing.SystemColors.Window
        Appearance21.BorderColor = System.Drawing.Color.Silver
        Me.cmbLPO.DisplayLayout.Override.RowAppearance = Appearance21
        Me.cmbLPO.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance19.BackColor = System.Drawing.SystemColors.ControlLight
        Me.cmbLPO.DisplayLayout.Override.TemplateAddRowAppearance = Appearance19
        Me.cmbLPO.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbLPO.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbLPO.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.cmbLPO.DropDownStyle = Infragistics.Win.UltraWinGrid.UltraComboStyle.DropDownList
        Me.cmbLPO.Location = New System.Drawing.Point(216, 335)
        Me.cmbLPO.Name = "cmbLPO"
        Me.cmbLPO.Size = New System.Drawing.Size(175, 29)
        Me.cmbLPO.TabIndex = 88
        '
        'chkManufacturing
        '
        Me.chkManufacturing.AutoSize = True
        Me.chkManufacturing.BackColor = System.Drawing.Color.Transparent
        Me.chkManufacturing.Location = New System.Drawing.Point(548, 280)
        Me.chkManufacturing.Name = "chkManufacturing"
        Me.chkManufacturing.Size = New System.Drawing.Size(137, 24)
        Me.chkManufacturing.TabIndex = 87
        Me.chkManufacturing.Text = "Manufacturing"
        Me.chkManufacturing.UseVisualStyleBackColor = False
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(289, 410)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 45
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'btnAddBrand
        '
        Me.btnAddBrand.Location = New System.Drawing.Point(397, 124)
        Me.btnAddBrand.Name = "btnAddBrand"
        Me.btnAddBrand.Size = New System.Drawing.Size(29, 23)
        Me.btnAddBrand.TabIndex = 13
        Me.btnAddBrand.TabStop = False
        Me.btnAddBrand.Text = "+"
        Me.btnAddBrand.UseVisualStyleBackColor = True
        '
        'lblBrand
        '
        Me.lblBrand.AutoSize = True
        Me.lblBrand.BackColor = System.Drawing.Color.Transparent
        Me.lblBrand.Location = New System.Drawing.Point(12, 128)
        Me.lblBrand.Name = "lblBrand"
        Me.lblBrand.Size = New System.Drawing.Size(54, 20)
        Me.lblBrand.TabIndex = 10
        Me.lblBrand.Text = "Grade"
        '
        'Label41
        '
        Me.Label41.AutoSize = True
        Me.Label41.BackColor = System.Drawing.Color.Transparent
        Me.Label41.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label41.ForeColor = System.Drawing.Color.Blue
        Me.Label41.Location = New System.Drawing.Point(144, 129)
        Me.Label41.Name = "Label41"
        Me.Label41.Size = New System.Drawing.Size(76, 20)
        Me.Label41.TabIndex = 11
        Me.Label41.Text = "(Refresh)"
        '
        'cmbBrand
        '
        Me.cmbBrand.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBrand.FormattingEnabled = True
        Me.cmbBrand.Location = New System.Drawing.Point(216, 126)
        Me.cmbBrand.Name = "cmbBrand"
        Me.cmbBrand.Size = New System.Drawing.Size(175, 28)
        Me.cmbBrand.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.cmbBrand, "Select Article Type")
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.BackColor = System.Drawing.Color.Transparent
        Me.Label38.Location = New System.Drawing.Point(10, 392)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(56, 20)
        Me.Label38.TabIndex = 35
        Me.Label38.Text = "Status"
        '
        'Label39
        '
        Me.Label39.AutoSize = True
        Me.Label39.BackColor = System.Drawing.Color.Transparent
        Me.Label39.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label39.ForeColor = System.Drawing.Color.Blue
        Me.Label39.Location = New System.Drawing.Point(144, 393)
        Me.Label39.Name = "Label39"
        Me.Label39.Size = New System.Drawing.Size(76, 20)
        Me.Label39.TabIndex = 36
        Me.Label39.Text = "(Refresh)"
        '
        'cmbStatus
        '
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Location = New System.Drawing.Point(216, 389)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(175, 28)
        Me.cmbStatus.TabIndex = 37
        Me.ToolTip1.SetToolTip(Me.cmbStatus, "Select Article Type")
        '
        'Label29
        '
        Me.Label29.AutoSize = True
        Me.Label29.BackColor = System.Drawing.Color.Transparent
        Me.Label29.Location = New System.Drawing.Point(12, 258)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(74, 20)
        Me.Label29.TabIndex = 18
        Me.Label29.Text = "HS Code"
        '
        'txtHSCode
        '
        Me.txtHSCode.Location = New System.Drawing.Point(216, 255)
        Me.txtHSCode.MaxLength = 50
        Me.txtHSCode.Name = "txtHSCode"
        Me.txtHSCode.Size = New System.Drawing.Size(175, 26)
        Me.txtHSCode.TabIndex = 19
        Me.ToolTip1.SetToolTip(Me.txtHSCode, "Item Name")
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.chkApplyAdjustmentFuelExpense)
        Me.GroupBox3.Controls.Add(Me.Label15)
        Me.GroupBox3.Controls.Add(Me.txtCostPrice)
        Me.GroupBox3.Controls.Add(Me.Label30)
        Me.GroupBox3.Controls.Add(Me.txtLargestPackQty)
        Me.GroupBox3.Controls.Add(Me.Label27)
        Me.GroupBox3.Controls.Add(Me.txtItemWeight)
        Me.GroupBox3.Controls.Add(Me.uitxtSortOrder)
        Me.GroupBox3.Controls.Add(Me.label8)
        Me.GroupBox3.Controls.Add(Me.label9)
        Me.GroupBox3.Controls.Add(Me.uitxtPackQty)
        Me.GroupBox3.Controls.Add(Me.Label4)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.uitxtPrice)
        Me.GroupBox3.Controls.Add(Me.uitxtSalePrice)
        Me.GroupBox3.Location = New System.Drawing.Point(434, 135)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(388, 139)
        Me.GroupBox3.TabIndex = 41
        Me.GroupBox3.TabStop = False
        '
        'chkApplyAdjustmentFuelExpense
        '
        Me.chkApplyAdjustmentFuelExpense.AutoSize = True
        Me.chkApplyAdjustmentFuelExpense.Location = New System.Drawing.Point(117, 114)
        Me.chkApplyAdjustmentFuelExpense.Name = "chkApplyAdjustmentFuelExpense"
        Me.chkApplyAdjustmentFuelExpense.Size = New System.Drawing.Size(293, 24)
        Me.chkApplyAdjustmentFuelExpense.TabIndex = 14
        Me.chkApplyAdjustmentFuelExpense.Text = "Apply Adjustment And Fuel Expense"
        Me.chkApplyAdjustmentFuelExpense.UseVisualStyleBackColor = True
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(212, 39)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(81, 20)
        Me.Label15.TabIndex = 6
        Me.Label15.Text = "Cost Price"
        '
        'txtCostPrice
        '
        Me.txtCostPrice.Location = New System.Drawing.Point(300, 36)
        Me.txtCostPrice.MaxLength = 50
        Me.txtCostPrice.Name = "txtCostPrice"
        Me.txtCostPrice.ReadOnly = True
        Me.txtCostPrice.Size = New System.Drawing.Size(82, 26)
        Me.txtCostPrice.TabIndex = 7
        Me.txtCostPrice.Text = "0"
        Me.ToolTip1.SetToolTip(Me.txtCostPrice, "Sale Price")
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Location = New System.Drawing.Point(9, 65)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(130, 20)
        Me.Label30.TabIndex = 8
        Me.Label30.Text = "Largest Pack Qty"
        '
        'txtLargestPackQty
        '
        Me.txtLargestPackQty.Location = New System.Drawing.Point(117, 62)
        Me.txtLargestPackQty.MaxLength = 50
        Me.txtLargestPackQty.Name = "txtLargestPackQty"
        Me.txtLargestPackQty.Size = New System.Drawing.Size(82, 26)
        Me.txtLargestPackQty.TabIndex = 9
        Me.txtLargestPackQty.Text = "1"
        Me.ToolTip1.SetToolTip(Me.txtLargestPackQty, "Pack Quantity")
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Location = New System.Drawing.Point(9, 91)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(59, 20)
        Me.Label27.TabIndex = 12
        Me.Label27.Text = "Weight"
        '
        'txtItemWeight
        '
        Me.txtItemWeight.Location = New System.Drawing.Point(117, 88)
        Me.txtItemWeight.Name = "txtItemWeight"
        Me.txtItemWeight.Size = New System.Drawing.Size(82, 26)
        Me.txtItemWeight.TabIndex = 13
        '
        'uitxtSortOrder
        '
        Me.uitxtSortOrder.Location = New System.Drawing.Point(300, 62)
        Me.uitxtSortOrder.MaxLength = 50
        Me.uitxtSortOrder.Name = "uitxtSortOrder"
        Me.uitxtSortOrder.Size = New System.Drawing.Size(82, 26)
        Me.uitxtSortOrder.TabIndex = 11
        Me.uitxtSortOrder.Text = "1"
        Me.ToolTip1.SetToolTip(Me.uitxtSortOrder, "Sort Order")
        '
        'label8
        '
        Me.label8.AutoSize = True
        Me.label8.Location = New System.Drawing.Point(9, 39)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(72, 20)
        Me.label8.TabIndex = 4
        Me.label8.Text = "Pack Qty"
        '
        'label9
        '
        Me.label9.AutoSize = True
        Me.label9.Location = New System.Drawing.Point(212, 65)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(83, 20)
        Me.label9.TabIndex = 10
        Me.label9.Text = "Sort Order"
        '
        'uitxtPackQty
        '
        Me.uitxtPackQty.Location = New System.Drawing.Point(117, 36)
        Me.uitxtPackQty.MaxLength = 50
        Me.uitxtPackQty.Name = "uitxtPackQty"
        Me.uitxtPackQty.Size = New System.Drawing.Size(82, 26)
        Me.uitxtPackQty.TabIndex = 5
        Me.uitxtPackQty.Text = "1"
        Me.ToolTip1.SetToolTip(Me.uitxtPackQty, "Pack Quantity")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(212, 14)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 20)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Sale Price"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 14)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(115, 20)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Purchase Price"
        '
        'uitxtPrice
        '
        Me.uitxtPrice.Location = New System.Drawing.Point(117, 10)
        Me.uitxtPrice.MaxLength = 50
        Me.uitxtPrice.Name = "uitxtPrice"
        Me.uitxtPrice.Size = New System.Drawing.Size(82, 26)
        Me.uitxtPrice.TabIndex = 1
        Me.uitxtPrice.Text = "0"
        Me.ToolTip1.SetToolTip(Me.uitxtPrice, "Purchase Price")
        '
        'uitxtSalePrice
        '
        Me.uitxtSalePrice.Location = New System.Drawing.Point(300, 10)
        Me.uitxtSalePrice.MaxLength = 50
        Me.uitxtSalePrice.Name = "uitxtSalePrice"
        Me.uitxtSalePrice.Size = New System.Drawing.Size(82, 26)
        Me.uitxtSalePrice.TabIndex = 3
        Me.uitxtSalePrice.Text = "0"
        Me.ToolTip1.SetToolTip(Me.uitxtSalePrice, "Sale Price")
        '
        'uichkActive
        '
        Me.uichkActive.AutoSize = True
        Me.uichkActive.BackColor = System.Drawing.Color.Transparent
        Me.uichkActive.Checked = True
        Me.uichkActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.uichkActive.Location = New System.Drawing.Point(659, 280)
        Me.uichkActive.Name = "uichkActive"
        Me.uichkActive.Size = New System.Drawing.Size(78, 24)
        Me.uichkActive.TabIndex = 39
        Me.uichkActive.Text = "Active"
        Me.uichkActive.UseVisualStyleBackColor = False
        '
        'lnkUploadPic
        '
        Me.lnkUploadPic.AutoSize = True
        Me.lnkUploadPic.BackColor = System.Drawing.Color.Transparent
        Me.lnkUploadPic.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.lnkUploadPic.Location = New System.Drawing.Point(718, 425)
        Me.lnkUploadPic.Name = "lnkUploadPic"
        Me.lnkUploadPic.Size = New System.Drawing.Size(113, 20)
        Me.lnkUploadPic.TabIndex = 44
        Me.lnkUploadPic.TabStop = True
        Me.lnkUploadPic.Text = "Upload Picture"
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Location = New System.Drawing.Point(690, 311)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(126, 111)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 86
        Me.PictureBox1.TabStop = False
        Me.ToolTip1.SetToolTip(Me.PictureBox1, "Article Logo")
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BtnNew, Me.BtnSave, Me.BtnDelete, Me.BtnCancel, Me.btnRefresh, Me.BtnPrint, Me.toolStripSeparator1, Me.btnAddAccounts, Me.btnAttachments, Me.ToolStripSeparator4, Me.btnPriceUpdate, Me.ToolStripSeparator3, Me.btnCostPriceUpdate, Me.ToolStripSeparator2, Me.HelpToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(869, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'BtnNew
        '
        Me.BtnNew.Image = CType(resources.GetObject("BtnNew.Image"), System.Drawing.Image)
        Me.BtnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnNew.Name = "BtnNew"
        Me.BtnNew.Size = New System.Drawing.Size(75, 22)
        Me.BtnNew.Text = "&New"
        '
        'BtnSave
        '
        Me.BtnSave.Image = CType(resources.GetObject("BtnSave.Image"), System.Drawing.Image)
        Me.BtnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(77, 22)
        Me.BtnSave.Text = "&Save"
        '
        'BtnDelete
        '
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.RightToLeftAutoMirrorImage = True
        Me.BtnDelete.Size = New System.Drawing.Size(66, 22)
        Me.BtnDelete.Text = "&Delete"
        '
        'BtnCancel
        '
        Me.BtnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(67, 22)
        Me.BtnCancel.Text = "&Cancel"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 22)
        Me.btnRefresh.Text = "Refresh"
        '
        'BtnPrint
        '
        Me.BtnPrint.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrintCostSheetToolStripMenuItem, Me.PrintAllCostSheetToolStripMenuItem1})
        Me.BtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(69, 22)
        Me.BtnPrint.Text = "&Print"
        '
        'PrintCostSheetToolStripMenuItem
        '
        Me.PrintCostSheetToolStripMenuItem.Name = "PrintCostSheetToolStripMenuItem"
        Me.PrintCostSheetToolStripMenuItem.Size = New System.Drawing.Size(247, 30)
        Me.PrintCostSheetToolStripMenuItem.Text = "Print Cost Sheet"
        '
        'PrintAllCostSheetToolStripMenuItem1
        '
        Me.PrintAllCostSheetToolStripMenuItem1.Name = "PrintAllCostSheetToolStripMenuItem1"
        Me.PrintAllCostSheetToolStripMenuItem1.Size = New System.Drawing.Size(247, 30)
        Me.PrintAllCostSheetToolStripMenuItem1.Text = "Print All Cost Sheet"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnAddAccounts
        '
        Me.btnAddAccounts.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btnAddAccounts.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddNewVendorToolStripMenuItem})
        Me.btnAddAccounts.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAddAccounts.Name = "btnAddAccounts"
        Me.btnAddAccounts.Size = New System.Drawing.Size(137, 22)
        Me.btnAddAccounts.Text = "Add Account"
        '
        'AddNewVendorToolStripMenuItem
        '
        Me.AddNewVendorToolStripMenuItem.Name = "AddNewVendorToolStripMenuItem"
        Me.AddNewVendorToolStripMenuItem.Size = New System.Drawing.Size(232, 30)
        Me.AddNewVendorToolStripMenuItem.Text = "Add New Vendor"
        '
        'btnAttachments
        '
        Me.btnAttachments.Image = Global.SimpleAccounts.My.Resources.Resources._3292_3760_mail_attachment_32x32
        Me.btnAttachments.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAttachments.Name = "btnAttachments"
        Me.btnAttachments.Size = New System.Drawing.Size(132, 22)
        Me.btnAttachments.Text = "Attachment"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'btnPriceUpdate
        '
        Me.btnPriceUpdate.Image = Global.SimpleAccounts.My.Resources.Resources.document_letter_edit
        Me.btnPriceUpdate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPriceUpdate.Name = "btnPriceUpdate"
        Me.btnPriceUpdate.Size = New System.Drawing.Size(145, 29)
        Me.btnPriceUpdate.Text = "Price Update "
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'btnCostPriceUpdate
        '
        Me.btnCostPriceUpdate.Image = Global.SimpleAccounts.My.Resources.Resources.document_letter_edit
        Me.btnCostPriceUpdate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCostPriceUpdate.Name = "btnCostPriceUpdate"
        Me.btnCostPriceUpdate.Size = New System.Drawing.Size(181, 29)
        Me.btnCostPriceUpdate.Text = "Cost Price Update"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'HelpToolStripButton
        '
        Me.HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HelpToolStripButton.Image = CType(resources.GetObject("HelpToolStripButton.Image"), System.Drawing.Image)
        Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton.Name = "HelpToolStripButton"
        Me.HelpToolStripButton.Size = New System.Drawing.Size(28, 28)
        Me.HelpToolStripButton.Text = "He&lp"
        Me.HelpToolStripButton.Visible = False
        '
        'chkServerItem
        '
        Me.chkServerItem.AutoSize = True
        Me.chkServerItem.BackColor = System.Drawing.Color.Transparent
        Me.chkServerItem.Location = New System.Drawing.Point(446, 280)
        Me.chkServerItem.Name = "chkServerItem"
        Me.chkServerItem.Size = New System.Drawing.Size(123, 24)
        Me.chkServerItem.TabIndex = 38
        Me.chkServerItem.Text = "Service Item"
        Me.chkServerItem.UseVisualStyleBackColor = False
        Me.chkServerItem.Visible = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(397, 70)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(29, 23)
        Me.Button1.TabIndex = 5
        Me.Button1.TabStop = False
        Me.Button1.Text = "+"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'tabAriticalDetail
        '
        Me.tabAriticalDetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabAriticalDetail.Controls.Add(Me.tabPgInfo)
        Me.tabAriticalDetail.Controls.Add(Me.tabPgArticalDetail)
        Me.tabAriticalDetail.Controls.Add(Me.tabPgLocation)
        Me.tabAriticalDetail.Controls.Add(Me.tabArticleAlias)
        Me.tabAriticalDetail.Controls.Add(Me.TabPgCostSheet)
        Me.tabAriticalDetail.Controls.Add(Me.TabPgVendorItems)
        Me.tabAriticalDetail.Controls.Add(Me.tbCustomer)
        Me.tabAriticalDetail.Controls.Add(Me.CostManagement)
        Me.tabAriticalDetail.Controls.Add(Me.TabPackQuantity)
        Me.tabAriticalDetail.Location = New System.Drawing.Point(3, 458)
        Me.tabAriticalDetail.Name = "tabAriticalDetail"
        Me.tabAriticalDetail.SelectedIndex = 0
        Me.tabAriticalDetail.Size = New System.Drawing.Size(867, 253)
        Me.tabAriticalDetail.TabIndex = 46
        Me.ToolTip1.SetToolTip(Me.tabAriticalDetail, "Cost Management")
        '
        'tabPgInfo
        '
        Me.tabPgInfo.AutoScroll = True
        Me.tabPgInfo.Controls.Add(Me.lnkAddModel)
        Me.tabPgInfo.Controls.Add(Me.lblModelList)
        Me.tabPgInfo.Controls.Add(Me.lstModelList)
        Me.tabPgInfo.Controls.Add(Me.lnkAddCombination)
        Me.tabPgInfo.Controls.Add(Me.lnkAddSize)
        Me.tabPgInfo.Controls.Add(Me.txtOldSalePrice)
        Me.tabPgInfo.Controls.Add(Me.txtOldPurchasePrice)
        Me.tabPgInfo.Controls.Add(Me.lblSize)
        Me.tabPgInfo.Controls.Add(Me.lblCombinition)
        Me.tabPgInfo.Controls.Add(Me.lstSizes)
        Me.tabPgInfo.Controls.Add(Me.lstCombinitions)
        Me.tabPgInfo.Location = New System.Drawing.Point(4, 29)
        Me.tabPgInfo.Name = "tabPgInfo"
        Me.tabPgInfo.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPgInfo.Size = New System.Drawing.Size(859, 221)
        Me.tabPgInfo.TabIndex = 0
        Me.tabPgInfo.Text = "Size & Color Information"
        Me.ToolTip1.SetToolTip(Me.tabPgInfo, "Size & Color Information")
        Me.tabPgInfo.UseVisualStyleBackColor = True
        '
        'lnkAddModel
        '
        Me.lnkAddModel.AutoSize = True
        Me.lnkAddModel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lnkAddModel.ForeColor = System.Drawing.Color.Blue
        Me.lnkAddModel.Location = New System.Drawing.Point(623, 6)
        Me.lnkAddModel.Name = "lnkAddModel"
        Me.lnkAddModel.Size = New System.Drawing.Size(60, 20)
        Me.lnkAddModel.TabIndex = 10
        Me.lnkAddModel.Text = "(Add...)"
        '
        'lblModelList
        '
        Me.lblModelList.AutoSize = True
        Me.lblModelList.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblModelList.ForeColor = System.Drawing.Color.Blue
        Me.lblModelList.Location = New System.Drawing.Point(675, 6)
        Me.lblModelList.Name = "lblModelList"
        Me.lblModelList.Size = New System.Drawing.Size(76, 20)
        Me.lblModelList.TabIndex = 9
        Me.lblModelList.Text = "(Refresh)"
        '
        'lstModelList
        '
        Me.lstModelList.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstModelList.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lstModelList.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstModelList.BackColor = System.Drawing.Color.Transparent
        Me.lstModelList.disableWhenChecked = False
        Me.lstModelList.HeadingLabelName = Nothing
        Me.lstModelList.HeadingText = "Model List"
        Me.lstModelList.Location = New System.Drawing.Point(541, 6)
        Me.lstModelList.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.lstModelList.Name = "lstModelList"
        Me.lstModelList.ShowAddNewButton = False
        Me.lstModelList.ShowInverse = True
        Me.lstModelList.ShowMagnifierButton = False
        Me.lstModelList.ShowNoCheck = False
        Me.lstModelList.ShowResetAllButton = False
        Me.lstModelList.ShowSelectall = True
        Me.lstModelList.Size = New System.Drawing.Size(209, 201)
        Me.lstModelList.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.lstModelList, "Select Multiple Combination")
        Me.lstModelList.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lnkAddCombination
        '
        Me.lnkAddCombination.AutoSize = True
        Me.lnkAddCombination.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lnkAddCombination.ForeColor = System.Drawing.Color.Blue
        Me.lnkAddCombination.Location = New System.Drawing.Point(405, 6)
        Me.lnkAddCombination.Name = "lnkAddCombination"
        Me.lnkAddCombination.Size = New System.Drawing.Size(60, 20)
        Me.lnkAddCombination.TabIndex = 7
        Me.lnkAddCombination.Text = "(Add...)"
        '
        'lnkAddSize
        '
        Me.lnkAddSize.AutoSize = True
        Me.lnkAddSize.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lnkAddSize.ForeColor = System.Drawing.Color.Blue
        Me.lnkAddSize.Location = New System.Drawing.Point(140, 6)
        Me.lnkAddSize.Name = "lnkAddSize"
        Me.lnkAddSize.Size = New System.Drawing.Size(60, 20)
        Me.lnkAddSize.TabIndex = 6
        Me.lnkAddSize.Text = "(Add...)"
        '
        'txtOldSalePrice
        '
        Me.txtOldSalePrice.Location = New System.Drawing.Point(772, 23)
        Me.txtOldSalePrice.MaxLength = 50
        Me.txtOldSalePrice.Name = "txtOldSalePrice"
        Me.txtOldSalePrice.Size = New System.Drawing.Size(79, 26)
        Me.txtOldSalePrice.TabIndex = 4
        Me.txtOldSalePrice.Text = "0"
        Me.txtOldSalePrice.Visible = False
        '
        'txtOldPurchasePrice
        '
        Me.txtOldPurchasePrice.Location = New System.Drawing.Point(772, 49)
        Me.txtOldPurchasePrice.MaxLength = 50
        Me.txtOldPurchasePrice.Name = "txtOldPurchasePrice"
        Me.txtOldPurchasePrice.Size = New System.Drawing.Size(79, 26)
        Me.txtOldPurchasePrice.TabIndex = 5
        Me.txtOldPurchasePrice.Text = "0"
        Me.txtOldPurchasePrice.Visible = False
        '
        'lblSize
        '
        Me.lblSize.AutoSize = True
        Me.lblSize.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblSize.ForeColor = System.Drawing.Color.Blue
        Me.lblSize.Location = New System.Drawing.Point(187, 6)
        Me.lblSize.Name = "lblSize"
        Me.lblSize.Size = New System.Drawing.Size(76, 20)
        Me.lblSize.TabIndex = 1
        Me.lblSize.Text = "(Refresh)"
        '
        'lblCombinition
        '
        Me.lblCombinition.AutoSize = True
        Me.lblCombinition.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblCombinition.ForeColor = System.Drawing.Color.Blue
        Me.lblCombinition.Location = New System.Drawing.Point(452, 6)
        Me.lblCombinition.Name = "lblCombinition"
        Me.lblCombinition.Size = New System.Drawing.Size(76, 20)
        Me.lblCombinition.TabIndex = 3
        Me.lblCombinition.Text = "(Refresh)"
        '
        'lstSizes
        '
        Me.lstSizes.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstSizes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lstSizes.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstSizes.BackColor = System.Drawing.Color.Transparent
        Me.lstSizes.disableWhenChecked = False
        Me.lstSizes.HeadingLabelName = Nothing
        Me.lstSizes.HeadingText = "Sizes"
        Me.lstSizes.Location = New System.Drawing.Point(12, 6)
        Me.lstSizes.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.lstSizes.Name = "lstSizes"
        Me.lstSizes.ShowAddNewButton = False
        Me.lstSizes.ShowInverse = True
        Me.lstSizes.ShowMagnifierButton = False
        Me.lstSizes.ShowNoCheck = False
        Me.lstSizes.ShowResetAllButton = False
        Me.lstSizes.ShowSelectall = True
        Me.lstSizes.Size = New System.Drawing.Size(258, 209)
        Me.lstSizes.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.lstSizes, "Select Multiple Sizes")
        Me.lstSizes.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstCombinitions
        '
        Me.lstCombinitions.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstCombinitions.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lstCombinitions.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstCombinitions.BackColor = System.Drawing.Color.Transparent
        Me.lstCombinitions.disableWhenChecked = False
        Me.lstCombinitions.HeadingLabelName = Nothing
        Me.lstCombinitions.HeadingText = "Combinitions"
        Me.lstCombinitions.Location = New System.Drawing.Point(276, 6)
        Me.lstCombinitions.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.lstCombinitions.Name = "lstCombinitions"
        Me.lstCombinitions.ShowAddNewButton = False
        Me.lstCombinitions.ShowInverse = True
        Me.lstCombinitions.ShowMagnifierButton = False
        Me.lstCombinitions.ShowNoCheck = False
        Me.lstCombinitions.ShowResetAllButton = False
        Me.lstCombinitions.ShowSelectall = True
        Me.lstCombinitions.Size = New System.Drawing.Size(258, 209)
        Me.lstCombinitions.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.lstCombinitions, "Select Multiple Combination")
        Me.lstCombinitions.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'tabPgArticalDetail
        '
        Me.tabPgArticalDetail.AutoScroll = True
        Me.tabPgArticalDetail.Controls.Add(Me.grdAriticals)
        Me.tabPgArticalDetail.Location = New System.Drawing.Point(4, 29)
        Me.tabPgArticalDetail.Name = "tabPgArticalDetail"
        Me.tabPgArticalDetail.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPgArticalDetail.Size = New System.Drawing.Size(859, 221)
        Me.tabPgArticalDetail.TabIndex = 1
        Me.tabPgArticalDetail.Text = "Size & Color Information Detail"
        Me.ToolTip1.SetToolTip(Me.tabPgArticalDetail, "Size & Color Information Detail")
        Me.tabPgArticalDetail.UseVisualStyleBackColor = True
        '
        'grdAriticals
        '
        Me.grdAriticals.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdAriticals.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdAriticals.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdAriticals.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdAriticals.GroupByBoxVisible = False
        Me.grdAriticals.Location = New System.Drawing.Point(3, 3)
        Me.grdAriticals.Name = "grdAriticals"
        Me.grdAriticals.RecordNavigator = True
        Me.grdAriticals.Size = New System.Drawing.Size(853, 215)
        Me.grdAriticals.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.grdAriticals, "Define Articles With Size & Combination")
        Me.grdAriticals.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'tabPgLocation
        '
        Me.tabPgLocation.Controls.Add(Me.grdItemLocation)
        Me.tabPgLocation.Location = New System.Drawing.Point(4, 29)
        Me.tabPgLocation.Name = "tabPgLocation"
        Me.tabPgLocation.Padding = New System.Windows.Forms.Padding(3)
        Me.tabPgLocation.Size = New System.Drawing.Size(859, 221)
        Me.tabPgLocation.TabIndex = 2
        Me.tabPgLocation.Text = "Article Location Information"
        Me.ToolTip1.SetToolTip(Me.tabPgLocation, "Article Location Information")
        Me.tabPgLocation.UseVisualStyleBackColor = True
        '
        'grdItemLocation
        '
        Me.grdItemLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdItemLocation.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdItemLocation.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdItemLocation.GroupByBoxVisible = False
        Me.grdItemLocation.Location = New System.Drawing.Point(3, 3)
        Me.grdItemLocation.Name = "grdItemLocation"
        Me.grdItemLocation.RecordNavigator = True
        Me.grdItemLocation.Size = New System.Drawing.Size(853, 215)
        Me.grdItemLocation.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.grdItemLocation, "Define Articles With Locations")
        Me.grdItemLocation.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'tabArticleAlias
        '
        Me.tabArticleAlias.AutoScroll = True
        Me.tabArticleAlias.Controls.Add(Me.btnAddArticleAlias)
        Me.tabArticleAlias.Controls.Add(Me.chkActive)
        Me.tabArticleAlias.Controls.Add(Me.Label42)
        Me.tabArticleAlias.Controls.Add(Me.cmbVendor)
        Me.tabArticleAlias.Controls.Add(Me.txtArticleAliasName)
        Me.tabArticleAlias.Controls.Add(Me.txtArticleAliasCode)
        Me.tabArticleAlias.Controls.Add(Me.Label43)
        Me.tabArticleAlias.Controls.Add(Me.lblArticleAliasCode)
        Me.tabArticleAlias.Controls.Add(Me.grpArticleAlias)
        Me.tabArticleAlias.Location = New System.Drawing.Point(4, 29)
        Me.tabArticleAlias.Name = "tabArticleAlias"
        Me.tabArticleAlias.Padding = New System.Windows.Forms.Padding(3)
        Me.tabArticleAlias.Size = New System.Drawing.Size(859, 221)
        Me.tabArticleAlias.TabIndex = 9
        Me.tabArticleAlias.Text = "Article Alias"
        Me.tabArticleAlias.UseVisualStyleBackColor = True
        '
        'btnAddArticleAlias
        '
        Me.btnAddArticleAlias.Location = New System.Drawing.Point(454, 68)
        Me.btnAddArticleAlias.Name = "btnAddArticleAlias"
        Me.btnAddArticleAlias.Size = New System.Drawing.Size(77, 23)
        Me.btnAddArticleAlias.TabIndex = 8
        Me.btnAddArticleAlias.Text = "Add"
        Me.btnAddArticleAlias.UseVisualStyleBackColor = True
        '
        'chkActive
        '
        Me.chkActive.AutoSize = True
        Me.chkActive.Checked = True
        Me.chkActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkActive.Location = New System.Drawing.Point(126, 68)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(78, 24)
        Me.chkActive.TabIndex = 7
        Me.chkActive.Text = "Active"
        Me.chkActive.UseVisualStyleBackColor = True
        '
        'Label42
        '
        Me.Label42.AutoSize = True
        Me.Label42.Location = New System.Drawing.Point(9, 45)
        Me.Label42.Name = "Label42"
        Me.Label42.Size = New System.Drawing.Size(78, 20)
        Me.Label42.TabIndex = 6
        Me.Label42.Text = "Customer"
        '
        'cmbVendor
        '
        Me.cmbVendor.CheckedListSettings.CheckStateMember = ""
        UltraGridColumn1.Header.VisiblePosition = 0
        UltraGridColumn1.Hidden = True
        UltraGridColumn2.Header.VisiblePosition = 1
        UltraGridColumn3.Header.VisiblePosition = 2
        UltraGridColumn3.Width = 70
        UltraGridColumn4.Header.VisiblePosition = 3
        UltraGridColumn4.Width = 80
        UltraGridColumn5.Header.VisiblePosition = 4
        UltraGridColumn5.MaxWidth = 80
        UltraGridColumn6.Header.VisiblePosition = 5
        UltraGridColumn6.MaxWidth = 60
        UltraGridColumn7.Header.VisiblePosition = 6
        UltraGridColumn7.Hidden = True
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn1, UltraGridColumn2, UltraGridColumn3, UltraGridColumn4, UltraGridColumn5, UltraGridColumn6, UltraGridColumn7})
        Me.cmbVendor.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbVendor.DisplayLayout.InterBandSpacing = 10
        Me.cmbVendor.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbVendor.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbVendor.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Appearance1.BackColor = System.Drawing.Color.Transparent
        Me.cmbVendor.DisplayLayout.Override.CardAreaAppearance = Appearance1
        Me.cmbVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbVendor.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance2.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance2.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance2.ForeColor = System.Drawing.Color.White
        Appearance2.TextHAlignAsString = "Left"
        Appearance2.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent
        Me.cmbVendor.DisplayLayout.Override.HeaderAppearance = Appearance2
        Me.cmbVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance3.BorderColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbVendor.DisplayLayout.Override.RowAppearance = Appearance3
        Appearance4.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance4.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.cmbVendor.DisplayLayout.Override.RowSelectorAppearance = Appearance4
        Me.cmbVendor.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbVendor.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance5.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance5.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance5.ForeColor = System.Drawing.Color.Black
        Me.cmbVendor.DisplayLayout.Override.SelectedRowAppearance = Appearance5
        Me.cmbVendor.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbVendor.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbVendor.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbVendor.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbVendor.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.cmbVendor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbVendor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbVendor.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbVendor.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbVendor.LimitToList = True
        Me.cmbVendor.Location = New System.Drawing.Point(126, 40)
        Me.cmbVendor.MaxDropDownItems = 20
        Me.cmbVendor.Name = "cmbVendor"
        Me.cmbVendor.Size = New System.Drawing.Size(405, 29)
        Me.cmbVendor.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.cmbVendor, "Select Article")
        '
        'txtArticleAliasName
        '
        Me.txtArticleAliasName.Location = New System.Drawing.Point(126, 14)
        Me.txtArticleAliasName.Name = "txtArticleAliasName"
        Me.txtArticleAliasName.Size = New System.Drawing.Size(405, 26)
        Me.txtArticleAliasName.TabIndex = 4
        '
        'txtArticleAliasCode
        '
        Me.txtArticleAliasCode.Location = New System.Drawing.Point(126, 14)
        Me.txtArticleAliasCode.Name = "txtArticleAliasCode"
        Me.txtArticleAliasCode.Size = New System.Drawing.Size(405, 26)
        Me.txtArticleAliasCode.TabIndex = 3
        Me.txtArticleAliasCode.Visible = False
        '
        'Label43
        '
        Me.Label43.AutoSize = True
        Me.Label43.Location = New System.Drawing.Point(9, 17)
        Me.Label43.Name = "Label43"
        Me.Label43.Size = New System.Drawing.Size(137, 20)
        Me.Label43.TabIndex = 2
        Me.Label43.Text = "Article Alias Name"
        '
        'lblArticleAliasCode
        '
        Me.lblArticleAliasCode.AutoSize = True
        Me.lblArticleAliasCode.Location = New System.Drawing.Point(9, 17)
        Me.lblArticleAliasCode.Name = "lblArticleAliasCode"
        Me.lblArticleAliasCode.Size = New System.Drawing.Size(133, 20)
        Me.lblArticleAliasCode.TabIndex = 1
        Me.lblArticleAliasCode.Text = "Article Alias Code"
        Me.lblArticleAliasCode.Visible = False
        '
        'grpArticleAlias
        '
        Me.grpArticleAlias.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpArticleAlias.Controls.Add(Me.grdArticleAlias)
        Me.grpArticleAlias.Location = New System.Drawing.Point(6, 91)
        Me.grpArticleAlias.Name = "grpArticleAlias"
        Me.grpArticleAlias.Size = New System.Drawing.Size(845, 124)
        Me.grpArticleAlias.TabIndex = 0
        Me.grpArticleAlias.TabStop = False
        Me.grpArticleAlias.Text = "Article Alias"
        '
        'grdArticleAlias
        '
        Me.grdArticleAlias.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdArticleAlias.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdArticleAlias.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdArticleAlias.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdArticleAlias.GroupByBoxVisible = False
        Me.grdArticleAlias.Location = New System.Drawing.Point(3, 22)
        Me.grdArticleAlias.Name = "grdArticleAlias"
        Me.grdArticleAlias.RecordNavigator = True
        Me.grdArticleAlias.Size = New System.Drawing.Size(839, 99)
        Me.grdArticleAlias.TabIndex = 18
        Me.grdArticleAlias.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grdArticleAlias, "Cost Sheet Detail")
        Me.grdArticleAlias.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdArticleAlias.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdArticleAlias.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'TabPgCostSheet
        '
        Me.TabPgCostSheet.AutoScroll = True
        Me.TabPgCostSheet.Controls.Add(Me.GroupBox5)
        Me.TabPgCostSheet.Controls.Add(Me.grpSpecification)
        Me.TabPgCostSheet.Controls.Add(Me.grdCostSheet)
        Me.TabPgCostSheet.Location = New System.Drawing.Point(4, 29)
        Me.TabPgCostSheet.Name = "TabPgCostSheet"
        Me.TabPgCostSheet.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPgCostSheet.Size = New System.Drawing.Size(859, 221)
        Me.TabPgCostSheet.TabIndex = 3
        Me.TabPgCostSheet.Text = "Cost Sheet"
        Me.ToolTip1.SetToolTip(Me.TabPgCostSheet, "Cost Sheet")
        Me.TabPgCostSheet.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox5.Controls.Add(Me.Label11)
        Me.GroupBox5.Controls.Add(Me.rdoName)
        Me.GroupBox5.Controls.Add(Me.Label37)
        Me.GroupBox5.Controls.Add(Me.cmbItem)
        Me.GroupBox5.Controls.Add(Me.Label36)
        Me.GroupBox5.Controls.Add(Me.RdoCode)
        Me.GroupBox5.Controls.Add(Me.cmbRemarks)
        Me.GroupBox5.Controls.Add(Me.txtCostSheet)
        Me.GroupBox5.Controls.Add(Me.Label35)
        Me.GroupBox5.Controls.Add(Me.btnAddCostSheet)
        Me.GroupBox5.Controls.Add(Me.Label34)
        Me.GroupBox5.Controls.Add(Me.Label12)
        Me.GroupBox5.Controls.Add(Me.Label21)
        Me.GroupBox5.Controls.Add(Me.txtTaxPercent)
        Me.GroupBox5.Controls.Add(Me.cmbCostSheetUnit)
        Me.GroupBox5.Controls.Add(Me.cmbCategorys)
        Me.GroupBox5.Location = New System.Drawing.Point(6, 57)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(843, 63)
        Me.GroupBox5.TabIndex = 19
        Me.GroupBox5.TabStop = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(5, 16)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(61, 20)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Item by"
        '
        'rdoName
        '
        Me.rdoName.AutoSize = True
        Me.rdoName.Location = New System.Drawing.Point(108, 14)
        Me.rdoName.Name = "rdoName"
        Me.rdoName.Size = New System.Drawing.Size(76, 24)
        Me.rdoName.TabIndex = 3
        Me.rdoName.Text = "Name"
        Me.ToolTip1.SetToolTip(Me.rdoName, "Article Search By Name")
        Me.rdoName.UseVisualStyleBackColor = True
        '
        'Label37
        '
        Me.Label37.AutoSize = True
        Me.Label37.Location = New System.Drawing.Point(218, 16)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(38, 20)
        Me.Label37.TabIndex = 5
        Me.Label37.Text = "Unit"
        '
        'cmbItem
        '
        Me.cmbItem.CheckedListSettings.CheckStateMember = ""
        UltraGridColumn8.Header.VisiblePosition = 0
        UltraGridColumn8.Hidden = True
        UltraGridColumn9.Header.VisiblePosition = 1
        UltraGridColumn10.Header.VisiblePosition = 2
        UltraGridColumn10.Width = 70
        UltraGridColumn11.Header.VisiblePosition = 3
        UltraGridColumn11.Width = 80
        UltraGridColumn12.Header.VisiblePosition = 4
        UltraGridColumn12.MaxWidth = 80
        UltraGridColumn13.Header.VisiblePosition = 5
        UltraGridColumn13.MaxWidth = 60
        UltraGridColumn14.Header.VisiblePosition = 6
        UltraGridColumn14.Hidden = True
        UltraGridBand2.Columns.AddRange(New Object() {UltraGridColumn8, UltraGridColumn9, UltraGridColumn10, UltraGridColumn11, UltraGridColumn12, UltraGridColumn13, UltraGridColumn14})
        Me.cmbItem.DisplayLayout.BandsSerializer.Add(UltraGridBand2)
        Me.cmbItem.DisplayLayout.InterBandSpacing = 10
        Me.cmbItem.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbItem.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbItem.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Appearance6.BackColor = System.Drawing.Color.Transparent
        Me.cmbItem.DisplayLayout.Override.CardAreaAppearance = Appearance6
        Me.cmbItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbItem.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance7.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance7.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance7.ForeColor = System.Drawing.Color.White
        Appearance7.TextHAlignAsString = "Left"
        Appearance7.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent
        Me.cmbItem.DisplayLayout.Override.HeaderAppearance = Appearance7
        Me.cmbItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance8.BorderColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbItem.DisplayLayout.Override.RowAppearance = Appearance8
        Appearance9.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance9.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.cmbItem.DisplayLayout.Override.RowSelectorAppearance = Appearance9
        Me.cmbItem.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbItem.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance10.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance10.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance10.ForeColor = System.Drawing.Color.Black
        Me.cmbItem.DisplayLayout.Override.SelectedRowAppearance = Appearance10
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
        Me.cmbItem.Location = New System.Drawing.Point(8, 32)
        Me.cmbItem.MaxDropDownItems = 20
        Me.cmbItem.Name = "cmbItem"
        Me.cmbItem.Size = New System.Drawing.Size(207, 29)
        Me.cmbItem.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbItem, "Select Article")
        '
        'Label36
        '
        Me.Label36.AutoSize = True
        Me.Label36.Location = New System.Drawing.Point(522, 15)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(73, 20)
        Me.Label36.TabIndex = 13
        Me.Label36.Text = "Remarks"
        '
        'RdoCode
        '
        Me.RdoCode.AutoSize = True
        Me.RdoCode.Checked = True
        Me.RdoCode.Location = New System.Drawing.Point(52, 14)
        Me.RdoCode.Name = "RdoCode"
        Me.RdoCode.Size = New System.Drawing.Size(72, 24)
        Me.RdoCode.TabIndex = 2
        Me.RdoCode.TabStop = True
        Me.RdoCode.Text = "Code"
        Me.ToolTip1.SetToolTip(Me.RdoCode, "Article Search By Item Code")
        Me.RdoCode.UseVisualStyleBackColor = True
        '
        'cmbRemarks
        '
        Me.cmbRemarks.FormattingEnabled = True
        Me.cmbRemarks.Location = New System.Drawing.Point(525, 32)
        Me.cmbRemarks.Name = "cmbRemarks"
        Me.cmbRemarks.Size = New System.Drawing.Size(103, 28)
        Me.cmbRemarks.TabIndex = 14
        '
        'txtCostSheet
        '
        Me.txtCostSheet.Location = New System.Drawing.Point(306, 32)
        Me.txtCostSheet.MaxLength = 50
        Me.txtCostSheet.Name = "txtCostSheet"
        Me.txtCostSheet.Size = New System.Drawing.Size(48, 26)
        Me.txtCostSheet.TabIndex = 8
        Me.txtCostSheet.Text = "1"
        Me.txtCostSheet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtCostSheet, "Quantity")
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Location = New System.Drawing.Point(418, 16)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(73, 20)
        Me.Label35.TabIndex = 11
        Me.Label35.Text = "Category"
        '
        'btnAddCostSheet
        '
        Me.btnAddCostSheet.Location = New System.Drawing.Point(634, 32)
        Me.btnAddCostSheet.Name = "btnAddCostSheet"
        Me.btnAddCostSheet.Size = New System.Drawing.Size(29, 23)
        Me.btnAddCostSheet.TabIndex = 15
        Me.btnAddCostSheet.Text = "+"
        Me.ToolTip1.SetToolTip(Me.btnAddCostSheet, "Add Article To Grid")
        Me.btnAddCostSheet.UseVisualStyleBackColor = True
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Location = New System.Drawing.Point(357, 15)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(34, 20)
        Me.Label34.TabIndex = 9
        Me.Label34.Text = "Tax"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label12.ForeColor = System.Drawing.Color.Blue
        Me.Label12.Location = New System.Drawing.Point(165, 16)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(76, 20)
        Me.Label12.TabIndex = 4
        Me.Label12.Text = "(Refresh)"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(303, 15)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(33, 20)
        Me.Label21.TabIndex = 7
        Me.Label21.Text = "Qty"
        '
        'txtTaxPercent
        '
        Me.txtTaxPercent.Location = New System.Drawing.Point(360, 32)
        Me.txtTaxPercent.MaxLength = 50
        Me.txtTaxPercent.Name = "txtTaxPercent"
        Me.txtTaxPercent.Size = New System.Drawing.Size(55, 26)
        Me.txtTaxPercent.TabIndex = 10
        Me.txtTaxPercent.Text = "0"
        Me.txtTaxPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtTaxPercent, "Quantity")
        '
        'cmbCostSheetUnit
        '
        Me.cmbCostSheetUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCostSheetUnit.FormattingEnabled = True
        Me.cmbCostSheetUnit.Items.AddRange(New Object() {"Loose", "Batch"})
        Me.cmbCostSheetUnit.Location = New System.Drawing.Point(221, 32)
        Me.cmbCostSheetUnit.Name = "cmbCostSheetUnit"
        Me.cmbCostSheetUnit.Size = New System.Drawing.Size(80, 28)
        Me.cmbCostSheetUnit.TabIndex = 6
        '
        'cmbCategorys
        '
        Me.cmbCategorys.FormattingEnabled = True
        Me.cmbCategorys.Location = New System.Drawing.Point(421, 32)
        Me.cmbCategorys.Name = "cmbCategorys"
        Me.cmbCategorys.Size = New System.Drawing.Size(98, 28)
        Me.cmbCategorys.TabIndex = 12
        '
        'grpSpecification
        '
        Me.grpSpecification.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpSpecification.Controls.Add(Me.Button5)
        Me.grpSpecification.Controls.Add(Me.Label40)
        Me.grpSpecification.Controls.Add(Me.cmbSpecification)
        Me.grpSpecification.Controls.Add(Me.Button3)
        Me.grpSpecification.Location = New System.Drawing.Point(8, 6)
        Me.grpSpecification.Name = "grpSpecification"
        Me.grpSpecification.Size = New System.Drawing.Size(843, 45)
        Me.grpSpecification.TabIndex = 18
        Me.grpSpecification.TabStop = False
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(305, 15)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(29, 23)
        Me.Button5.TabIndex = 17
        Me.Button5.Text = "+"
        Me.ToolTip1.SetToolTip(Me.Button5, "Add Article To Grid")
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Label40
        '
        Me.Label40.AutoSize = True
        Me.Label40.Location = New System.Drawing.Point(3, 19)
        Me.Label40.Name = "Label40"
        Me.Label40.Size = New System.Drawing.Size(100, 20)
        Me.Label40.TabIndex = 17
        Me.Label40.Text = "Specification"
        '
        'cmbSpecification
        '
        Me.cmbSpecification.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSpecification.FormattingEnabled = True
        Me.cmbSpecification.Items.AddRange(New Object() {"Loose", "Batch"})
        Me.cmbSpecification.Location = New System.Drawing.Point(94, 16)
        Me.cmbSpecification.Name = "cmbSpecification"
        Me.cmbSpecification.Size = New System.Drawing.Size(205, 28)
        Me.cmbSpecification.TabIndex = 17
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(340, 15)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(125, 23)
        Me.Button3.TabIndex = 16
        Me.Button3.Text = "Specification Copy"
        Me.ToolTip1.SetToolTip(Me.Button3, "Add Article To Grid")
        Me.Button3.UseVisualStyleBackColor = True
        '
        'grdCostSheet
        '
        Me.grdCostSheet.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdCostSheet.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdCostSheet.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdCostSheet.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdCostSheet.GroupByBoxVisible = False
        Me.grdCostSheet.Location = New System.Drawing.Point(3, 126)
        Me.grdCostSheet.Name = "grdCostSheet"
        Me.grdCostSheet.RecordNavigator = True
        Me.grdCostSheet.Size = New System.Drawing.Size(853, 84)
        Me.grdCostSheet.TabIndex = 17
        Me.grdCostSheet.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grdCostSheet, "Cost Sheet Detail")
        Me.grdCostSheet.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdCostSheet.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdCostSheet.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'TabPgVendorItems
        '
        Me.TabPgVendorItems.AutoScroll = True
        Me.TabPgVendorItems.Controls.Add(Me.grdVendorItems)
        Me.TabPgVendorItems.Controls.Add(Me.btnadd)
        Me.TabPgVendorItems.Controls.Add(Me.LinkLabel1)
        Me.TabPgVendorItems.Controls.Add(Me.lblvendorslist)
        Me.TabPgVendorItems.Controls.Add(Me.cmbvendorslist)
        Me.TabPgVendorItems.Location = New System.Drawing.Point(4, 29)
        Me.TabPgVendorItems.Name = "TabPgVendorItems"
        Me.TabPgVendorItems.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPgVendorItems.Size = New System.Drawing.Size(859, 221)
        Me.TabPgVendorItems.TabIndex = 4
        Me.TabPgVendorItems.Text = "Vendors List"
        Me.ToolTip1.SetToolTip(Me.TabPgVendorItems, "Vendor List")
        Me.TabPgVendorItems.UseVisualStyleBackColor = True
        '
        'grdVendorItems
        '
        Me.grdVendorItems.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdVendorItems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdVendorItems_DesignTimeLayout.LayoutString = resources.GetString("grdVendorItems_DesignTimeLayout.LayoutString")
        Me.grdVendorItems.DesignTimeLayout = grdVendorItems_DesignTimeLayout
        Me.grdVendorItems.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdVendorItems.GroupByBoxVisible = False
        Me.grdVendorItems.Location = New System.Drawing.Point(2, 49)
        Me.grdVendorItems.Name = "grdVendorItems"
        Me.grdVendorItems.Size = New System.Drawing.Size(855, 170)
        Me.grdVendorItems.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.grdVendorItems, "Vendor Detail")
        Me.grdVendorItems.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'btnadd
        '
        Me.btnadd.Location = New System.Drawing.Point(258, 20)
        Me.btnadd.Name = "btnadd"
        Me.btnadd.Size = New System.Drawing.Size(43, 23)
        Me.btnadd.TabIndex = 3
        Me.btnadd.Text = "+"
        Me.ToolTip1.SetToolTip(Me.btnadd, "Add Vendor To Grid")
        Me.btnadd.UseVisualStyleBackColor = True
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(168, 6)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(66, 20)
        Me.LinkLabel1.TabIndex = 2
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Refresh"
        '
        'lblvendorslist
        '
        Me.lblvendorslist.AutoSize = True
        Me.lblvendorslist.Location = New System.Drawing.Point(12, 6)
        Me.lblvendorslist.Name = "lblvendorslist"
        Me.lblvendorslist.Size = New System.Drawing.Size(98, 20)
        Me.lblvendorslist.TabIndex = 1
        Me.lblvendorslist.Text = "Vendors List"
        '
        'cmbvendorslist
        '
        Me.cmbvendorslist.FormattingEnabled = True
        Me.cmbvendorslist.Location = New System.Drawing.Point(6, 22)
        Me.cmbvendorslist.Name = "cmbvendorslist"
        Me.cmbvendorslist.Size = New System.Drawing.Size(246, 28)
        Me.cmbvendorslist.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.cmbvendorslist, "Select Vendor")
        '
        'tbCustomer
        '
        Me.tbCustomer.AutoScroll = True
        Me.tbCustomer.Controls.Add(Me.grdCustomerList)
        Me.tbCustomer.Controls.Add(Me.Button2)
        Me.tbCustomer.Controls.Add(Me.LinkLabel2)
        Me.tbCustomer.Controls.Add(Me.Label28)
        Me.tbCustomer.Controls.Add(Me.ComboBox1)
        Me.tbCustomer.Location = New System.Drawing.Point(4, 29)
        Me.tbCustomer.Name = "tbCustomer"
        Me.tbCustomer.Padding = New System.Windows.Forms.Padding(3)
        Me.tbCustomer.Size = New System.Drawing.Size(859, 221)
        Me.tbCustomer.TabIndex = 7
        Me.tbCustomer.Text = "Customer List"
        Me.tbCustomer.UseVisualStyleBackColor = True
        '
        'grdCustomerList
        '
        Me.grdCustomerList.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdCustomerList.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdCustomerList_DesignTimeLayout.LayoutString = resources.GetString("grdCustomerList_DesignTimeLayout.LayoutString")
        Me.grdCustomerList.DesignTimeLayout = grdCustomerList_DesignTimeLayout
        Me.grdCustomerList.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdCustomerList.GroupByBoxVisible = False
        Me.grdCustomerList.Location = New System.Drawing.Point(2, 47)
        Me.grdCustomerList.Name = "grdCustomerList"
        Me.grdCustomerList.Size = New System.Drawing.Size(855, 170)
        Me.grdCustomerList.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.grdCustomerList, "Vendor Detail")
        Me.grdCustomerList.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(258, 18)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(43, 23)
        Me.Button2.TabIndex = 8
        Me.Button2.Text = "+"
        Me.ToolTip1.SetToolTip(Me.Button2, "Add Vendor To Grid")
        Me.Button2.UseVisualStyleBackColor = True
        '
        'LinkLabel2
        '
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.Location = New System.Drawing.Point(168, 4)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(66, 20)
        Me.LinkLabel2.TabIndex = 7
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "Refresh"
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.Location = New System.Drawing.Point(12, 4)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(107, 20)
        Me.Label28.TabIndex = 6
        Me.Label28.Text = "Customer List"
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(6, 20)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(246, 28)
        Me.ComboBox1.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.ComboBox1, "Select Vendor")
        '
        'CostManagement
        '
        Me.CostManagement.AutoScroll = True
        Me.CostManagement.Controls.Add(Me.Label22)
        Me.CostManagement.Controls.Add(Me.GroupBox2)
        Me.CostManagement.Location = New System.Drawing.Point(4, 29)
        Me.CostManagement.Name = "CostManagement"
        Me.CostManagement.Padding = New System.Windows.Forms.Padding(3)
        Me.CostManagement.Size = New System.Drawing.Size(859, 221)
        Me.CostManagement.TabIndex = 6
        Me.CostManagement.Text = "Cost Management"
        Me.CostManagement.UseVisualStyleBackColor = True
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Times New Roman", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.Black
        Me.Label22.Location = New System.Drawing.Point(12, 6)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(198, 26)
        Me.Label22.TabIndex = 1
        Me.Label22.Text = "Cost Management"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label26)
        Me.GroupBox2.Controls.Add(Me.txtFlatRate)
        Me.GroupBox2.Controls.Add(Me.rbtFlatRate)
        Me.GroupBox2.Controls.Add(Me.rbtTax)
        Me.GroupBox2.Controls.Add(Me.txtMarketReturns)
        Me.GroupBox2.Controls.Add(Me.Label25)
        Me.GroupBox2.Controls.Add(Me.txtFreight)
        Me.GroupBox2.Controls.Add(Me.Label24)
        Me.GroupBox2.Controls.Add(Me.txtTradePrice)
        Me.GroupBox2.Controls.Add(Me.Label23)
        Me.GroupBox2.Location = New System.Drawing.Point(15, 27)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(347, 168)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Cost"
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.Navy
        Me.Label26.Location = New System.Drawing.Point(6, 108)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(465, 20)
        Me.Label26.TabIndex = 9
        Me.Label26.Text = "Applicable Option : _____________________________"
        '
        'txtFlatRate
        '
        Me.txtFlatRate.Location = New System.Drawing.Point(252, 132)
        Me.txtFlatRate.Name = "txtFlatRate"
        Me.txtFlatRate.Size = New System.Drawing.Size(78, 26)
        Me.txtFlatRate.TabIndex = 8
        '
        'rbtFlatRate
        '
        Me.rbtFlatRate.AutoSize = True
        Me.rbtFlatRate.Location = New System.Drawing.Point(175, 133)
        Me.rbtFlatRate.Name = "rbtFlatRate"
        Me.rbtFlatRate.Size = New System.Drawing.Size(104, 24)
        Me.rbtFlatRate.TabIndex = 7
        Me.rbtFlatRate.Text = "Flat Rate "
        Me.rbtFlatRate.UseVisualStyleBackColor = True
        '
        'rbtTax
        '
        Me.rbtTax.AutoSize = True
        Me.rbtTax.Checked = True
        Me.rbtTax.Location = New System.Drawing.Point(126, 133)
        Me.rbtTax.Name = "rbtTax"
        Me.rbtTax.Size = New System.Drawing.Size(59, 24)
        Me.rbtTax.TabIndex = 6
        Me.rbtTax.TabStop = True
        Me.rbtTax.Text = "Tax"
        Me.rbtTax.UseVisualStyleBackColor = True
        '
        'txtMarketReturns
        '
        Me.txtMarketReturns.Location = New System.Drawing.Point(126, 75)
        Me.txtMarketReturns.Name = "txtMarketReturns"
        Me.txtMarketReturns.Size = New System.Drawing.Size(204, 26)
        Me.txtMarketReturns.TabIndex = 5
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(6, 78)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(119, 20)
        Me.Label25.TabIndex = 4
        Me.Label25.Text = "Market Returns"
        '
        'txtFreight
        '
        Me.txtFreight.Location = New System.Drawing.Point(126, 49)
        Me.txtFreight.Name = "txtFreight"
        Me.txtFreight.Size = New System.Drawing.Size(204, 26)
        Me.txtFreight.TabIndex = 3
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Location = New System.Drawing.Point(6, 52)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(59, 20)
        Me.Label24.TabIndex = 2
        Me.Label24.Text = "Freight"
        '
        'txtTradePrice
        '
        Me.txtTradePrice.Location = New System.Drawing.Point(126, 23)
        Me.txtTradePrice.Name = "txtTradePrice"
        Me.txtTradePrice.Size = New System.Drawing.Size(204, 26)
        Me.txtTradePrice.TabIndex = 1
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Location = New System.Drawing.Point(6, 26)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(89, 20)
        Me.Label23.TabIndex = 0
        Me.Label23.Text = "Trade Price"
        '
        'TabPackQuantity
        '
        Me.TabPackQuantity.AutoScroll = True
        Me.TabPackQuantity.Controls.Add(Me.grdPackQty)
        Me.TabPackQuantity.Controls.Add(Me.Label31)
        Me.TabPackQuantity.Controls.Add(Me.GroupBox4)
        Me.TabPackQuantity.Location = New System.Drawing.Point(4, 29)
        Me.TabPackQuantity.Name = "TabPackQuantity"
        Me.TabPackQuantity.Size = New System.Drawing.Size(859, 220)
        Me.TabPackQuantity.TabIndex = 8
        Me.TabPackQuantity.Text = "Unit Detail"
        Me.ToolTip1.SetToolTip(Me.TabPackQuantity, "Pack Quantity")
        Me.TabPackQuantity.UseVisualStyleBackColor = True
        '
        'grdPackQty
        '
        Me.grdPackQty.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdPackQty.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdPackQty_DesignTimeLayout.LayoutString = resources.GetString("grdPackQty_DesignTimeLayout.LayoutString")
        Me.grdPackQty.DesignTimeLayout = grdPackQty_DesignTimeLayout
        Me.grdPackQty.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdPackQty.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdPackQty.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdPackQty.GroupByBoxVisible = False
        Me.grdPackQty.Location = New System.Drawing.Point(268, 6)
        Me.grdPackQty.Name = "grdPackQty"
        Me.grdPackQty.RecordNavigator = True
        Me.grdPackQty.Size = New System.Drawing.Size(588, 211)
        Me.grdPackQty.TabIndex = 2
        Me.grdPackQty.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'Label31
        '
        Me.Label31.AutoSize = True
        Me.Label31.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.ForeColor = System.Drawing.Color.Navy
        Me.Label31.Location = New System.Drawing.Point(12, 6)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(111, 20)
        Me.Label31.TabIndex = 0
        Me.Label31.Text = "Unit Detail"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Button4)
        Me.GroupBox4.Controls.Add(Me.btnAddPackQty)
        Me.GroupBox4.Controls.Add(Me.txtQuantity)
        Me.GroupBox4.Controls.Add(Me.txtPackName)
        Me.GroupBox4.Controls.Add(Me.Label33)
        Me.GroupBox4.Controls.Add(Me.Label32)
        Me.GroupBox4.Location = New System.Drawing.Point(15, 22)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(247, 102)
        Me.GroupBox4.TabIndex = 1
        Me.GroupBox4.TabStop = False
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(192, 71)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(44, 23)
        Me.Button4.TabIndex = 5
        Me.Button4.Text = "Reset"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'btnAddPackQty
        '
        Me.btnAddPackQty.Location = New System.Drawing.Point(122, 71)
        Me.btnAddPackQty.Name = "btnAddPackQty"
        Me.btnAddPackQty.Size = New System.Drawing.Size(66, 23)
        Me.btnAddPackQty.TabIndex = 4
        Me.btnAddPackQty.Text = "Save"
        Me.btnAddPackQty.UseVisualStyleBackColor = True
        '
        'txtQuantity
        '
        Me.txtQuantity.Location = New System.Drawing.Point(73, 45)
        Me.txtQuantity.Name = "txtQuantity"
        Me.txtQuantity.Size = New System.Drawing.Size(163, 26)
        Me.txtQuantity.TabIndex = 3
        '
        'txtPackName
        '
        Me.txtPackName.Location = New System.Drawing.Point(73, 19)
        Me.txtPackName.Name = "txtPackName"
        Me.txtPackName.Size = New System.Drawing.Size(163, 26)
        Me.txtPackName.TabIndex = 1
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Location = New System.Drawing.Point(9, 49)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(68, 20)
        Me.Label33.TabIndex = 2
        Me.Label33.Text = "Quantity"
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Location = New System.Drawing.Point(6, 23)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(84, 20)
        Me.Label32.TabIndex = 0
        Me.Label32.Text = "Unit Name"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(12, 75)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(143, 20)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Department/Group"
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(443, 322)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRemarks.Size = New System.Drawing.Size(241, 86)
        Me.txtRemarks.TabIndex = 43
        Me.ToolTip1.SetToolTip(Me.txtRemarks, "Remarks")
        '
        'btnAddUnit
        '
        Me.btnAddUnit.Location = New System.Drawing.Point(397, 360)
        Me.btnAddUnit.Name = "btnAddUnit"
        Me.btnAddUnit.Size = New System.Drawing.Size(29, 23)
        Me.btnAddUnit.TabIndex = 34
        Me.btnAddUnit.TabStop = False
        Me.btnAddUnit.Text = "+"
        Me.btnAddUnit.UseVisualStyleBackColor = True
        '
        'btnAddCompany
        '
        Me.btnAddCompany.Location = New System.Drawing.Point(397, 306)
        Me.btnAddCompany.Name = "btnAddCompany"
        Me.btnAddCompany.Size = New System.Drawing.Size(29, 23)
        Me.btnAddCompany.TabIndex = 27
        Me.btnAddCompany.TabStop = False
        Me.btnAddCompany.Text = "+"
        Me.btnAddCompany.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Location = New System.Drawing.Point(443, 306)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(73, 20)
        Me.Label10.TabIndex = 42
        Me.Label10.Text = "Remarks"
        '
        'cmbCompany
        '
        Me.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCompany.FormattingEnabled = True
        Me.cmbCompany.Location = New System.Drawing.Point(216, 308)
        Me.cmbCompany.Name = "cmbCompany"
        Me.cmbCompany.Size = New System.Drawing.Size(175, 28)
        Me.cmbCompany.TabIndex = 26
        Me.ToolTip1.SetToolTip(Me.cmbCompany, "Category")
        '
        'lblUnit
        '
        Me.lblUnit.AutoSize = True
        Me.lblUnit.BackColor = System.Drawing.Color.Transparent
        Me.lblUnit.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblUnit.ForeColor = System.Drawing.Color.Blue
        Me.lblUnit.Location = New System.Drawing.Point(144, 365)
        Me.lblUnit.Name = "lblUnit"
        Me.lblUnit.Size = New System.Drawing.Size(76, 20)
        Me.lblUnit.TabIndex = 32
        Me.lblUnit.Text = "(Refresh)"
        '
        'lblCompany
        '
        Me.lblCompany.AutoSize = True
        Me.lblCompany.BackColor = System.Drawing.Color.Transparent
        Me.lblCompany.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblCompany.ForeColor = System.Drawing.Color.Blue
        Me.lblCompany.Location = New System.Drawing.Point(144, 311)
        Me.lblCompany.Name = "lblCompany"
        Me.lblCompany.Size = New System.Drawing.Size(76, 20)
        Me.lblCompany.TabIndex = 25
        Me.lblCompany.Text = "(Refresh)"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Location = New System.Drawing.Point(12, 311)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(167, 20)
        Me.Label7.TabIndex = 24
        Me.Label7.Text = "Category/Manufacture"
        '
        'lblCategory
        '
        Me.lblCategory.AutoSize = True
        Me.lblCategory.BackColor = System.Drawing.Color.Transparent
        Me.lblCategory.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblCategory.ForeColor = System.Drawing.Color.Blue
        Me.lblCategory.Location = New System.Drawing.Point(144, 75)
        Me.lblCategory.Name = "lblCategory"
        Me.lblCategory.Size = New System.Drawing.Size(76, 20)
        Me.lblCategory.TabIndex = 3
        Me.lblCategory.Text = "(Refresh)"
        '
        'btnAddLPO
        '
        Me.btnAddLPO.Location = New System.Drawing.Point(397, 333)
        Me.btnAddLPO.Name = "btnAddLPO"
        Me.btnAddLPO.Size = New System.Drawing.Size(29, 23)
        Me.btnAddLPO.TabIndex = 30
        Me.btnAddLPO.TabStop = False
        Me.btnAddLPO.Text = "+"
        Me.btnAddLPO.UseVisualStyleBackColor = True
        '
        'btnAddType
        '
        Me.btnAddType.Location = New System.Drawing.Point(397, 97)
        Me.btnAddType.Name = "btnAddType"
        Me.btnAddType.Size = New System.Drawing.Size(29, 23)
        Me.btnAddType.TabIndex = 9
        Me.btnAddType.TabStop = False
        Me.btnAddType.Text = "+"
        Me.btnAddType.UseVisualStyleBackColor = True
        '
        'btnAddGender
        '
        Me.btnAddGender.Location = New System.Drawing.Point(397, 279)
        Me.btnAddGender.Name = "btnAddGender"
        Me.btnAddGender.Size = New System.Drawing.Size(29, 23)
        Me.btnAddGender.TabIndex = 23
        Me.btnAddGender.TabStop = False
        Me.btnAddGender.Text = "+"
        Me.btnAddGender.UseVisualStyleBackColor = True
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Location = New System.Drawing.Point(12, 101)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(43, 20)
        Me.Label14.TabIndex = 6
        Me.Label14.Text = "Type"
        '
        'cmbUnit
        '
        Me.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUnit.FormattingEnabled = True
        Me.cmbUnit.Location = New System.Drawing.Point(216, 362)
        Me.cmbUnit.Name = "cmbUnit"
        Me.cmbUnit.Size = New System.Drawing.Size(175, 28)
        Me.cmbUnit.TabIndex = 33
        Me.ToolTip1.SetToolTip(Me.cmbUnit, "Select Article Unit")
        '
        'lblType
        '
        Me.lblType.AutoSize = True
        Me.lblType.BackColor = System.Drawing.Color.Transparent
        Me.lblType.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblType.ForeColor = System.Drawing.Color.Blue
        Me.lblType.Location = New System.Drawing.Point(144, 102)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(76, 20)
        Me.lblType.TabIndex = 7
        Me.lblType.Text = "(Refresh)"
        '
        'cmbGender
        '
        Me.cmbGender.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGender.FormattingEnabled = True
        Me.cmbGender.Location = New System.Drawing.Point(216, 281)
        Me.cmbGender.Name = "cmbGender"
        Me.cmbGender.Size = New System.Drawing.Size(175, 28)
        Me.cmbGender.TabIndex = 22
        Me.ToolTip1.SetToolTip(Me.cmbGender, "Gender")
        '
        'cmbType
        '
        Me.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbType.FormattingEnabled = True
        Me.cmbType.Location = New System.Drawing.Point(216, 99)
        Me.cmbType.Name = "cmbType"
        Me.cmbType.Size = New System.Drawing.Size(175, 28)
        Me.cmbType.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.cmbType, "Select Article Type")
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.uitxtStockLevelMaximum)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.uitxtStockLevelOptimal)
        Me.GroupBox1.Controls.Add(Me.uitxtStockLevel)
        Me.GroupBox1.Location = New System.Drawing.Point(434, 70)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(388, 59)
        Me.GroupBox1.TabIndex = 40
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Stock Level"
        '
        'Label18
        '
        Me.Label18.Location = New System.Drawing.Point(9, 13)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(98, 13)
        Me.Label18.TabIndex = 0
        Me.Label18.Text = "Minimum"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'uitxtStockLevelMaximum
        '
        Me.uitxtStockLevelMaximum.Location = New System.Drawing.Point(225, 28)
        Me.uitxtStockLevelMaximum.MaxLength = 50
        Me.uitxtStockLevelMaximum.Name = "uitxtStockLevelMaximum"
        Me.uitxtStockLevelMaximum.Size = New System.Drawing.Size(98, 26)
        Me.uitxtStockLevelMaximum.TabIndex = 5
        Me.uitxtStockLevelMaximum.Text = "0"
        Me.uitxtStockLevelMaximum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip1.SetToolTip(Me.uitxtStockLevelMaximum, "Maximum Stock")
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(222, 13)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(98, 13)
        Me.Label17.TabIndex = 4
        Me.Label17.Text = "Maximum"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(114, 12)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(98, 13)
        Me.Label16.TabIndex = 2
        Me.Label16.Text = "Optimal"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'uitxtStockLevelOptimal
        '
        Me.uitxtStockLevelOptimal.Location = New System.Drawing.Point(117, 28)
        Me.uitxtStockLevelOptimal.MaxLength = 50
        Me.uitxtStockLevelOptimal.Name = "uitxtStockLevelOptimal"
        Me.uitxtStockLevelOptimal.Size = New System.Drawing.Size(98, 26)
        Me.uitxtStockLevelOptimal.TabIndex = 3
        Me.uitxtStockLevelOptimal.Text = "0"
        Me.uitxtStockLevelOptimal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip1.SetToolTip(Me.uitxtStockLevelOptimal, "Optimal Stock")
        '
        'uitxtStockLevel
        '
        Me.uitxtStockLevel.Location = New System.Drawing.Point(9, 28)
        Me.uitxtStockLevel.MaxLength = 50
        Me.uitxtStockLevel.Name = "uitxtStockLevel"
        Me.uitxtStockLevel.Size = New System.Drawing.Size(98, 26)
        Me.uitxtStockLevel.TabIndex = 1
        Me.uitxtStockLevel.Text = "0"
        Me.uitxtStockLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ToolTip1.SetToolTip(Me.uitxtStockLevel, "Minimum Stock")
        '
        'cmbCategory
        '
        Me.cmbCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCategory.FormattingEnabled = True
        Me.cmbCategory.Location = New System.Drawing.Point(216, 72)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(175, 28)
        Me.cmbCategory.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.cmbCategory, "Select Article Department ")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(12, 182)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 20)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "Item Name"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Location = New System.Drawing.Point(12, 284)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(50, 20)
        Me.Label19.TabIndex = 20
        Me.Label19.Text = "Origin"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(12, 365)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 20)
        Me.Label6.TabIndex = 31
        Me.Label6.Text = "Unit"
        '
        'uitxtItemName
        '
        Me.uitxtItemName.Location = New System.Drawing.Point(216, 179)
        Me.uitxtItemName.MaxLength = 1500
        Me.uitxtItemName.Multiline = True
        Me.uitxtItemName.Name = "uitxtItemName"
        Me.uitxtItemName.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.uitxtItemName.Size = New System.Drawing.Size(175, 70)
        Me.uitxtItemName.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.uitxtItemName, "Item Name")
        '
        'uitxtItemCode
        '
        Me.uitxtItemCode.Location = New System.Drawing.Point(216, 153)
        Me.uitxtItemCode.MaxLength = 1500
        Me.uitxtItemCode.Name = "uitxtItemCode"
        Me.uitxtItemCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.uitxtItemCode.Size = New System.Drawing.Size(175, 26)
        Me.uitxtItemCode.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.uitxtItemCode, "Item Code")
        '
        'lblGender
        '
        Me.lblGender.AutoSize = True
        Me.lblGender.BackColor = System.Drawing.Color.Transparent
        Me.lblGender.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblGender.ForeColor = System.Drawing.Color.Blue
        Me.lblGender.Location = New System.Drawing.Point(144, 285)
        Me.lblGender.Name = "lblGender"
        Me.lblGender.Size = New System.Drawing.Size(76, 20)
        Me.lblGender.TabIndex = 21
        Me.lblGender.Text = "(Refresh)"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Location = New System.Drawing.Point(12, 338)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(153, 20)
        Me.Label20.TabIndex = 28
        Me.Label20.Text = "Sub Category/Brand"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(12, 156)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 20)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Item Code"
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.AutoScroll = True
        Me.UltraTabPageControl2.Controls.Add(Me.CtrlGrdBar1)
        Me.UltraTabPageControl2.Controls.Add(Me.ToolStrip2)
        Me.UltraTabPageControl2.Controls.Add(Me.grdAllRecords)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(869, 706)
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(832, 1)
        Me.CtrlGrdBar1.MyGrid = Me.grdAllRecords
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(38, 27)
        Me.CtrlGrdBar1.TabIndex = 1
        '
        'grdAllRecords
        '
        Me.grdAllRecords.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdAllRecords.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdAllRecords.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdAllRecords.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdAllRecords.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdAllRecords.GroupByBoxVisible = False
        Me.grdAllRecords.Location = New System.Drawing.Point(0, 27)
        Me.grdAllRecords.Name = "grdAllRecords"
        Me.grdAllRecords.RecordNavigator = True
        Me.grdAllRecords.Size = New System.Drawing.Size(868, 679)
        Me.grdAllRecords.TabIndex = 1
        Me.grdAllRecords.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'ToolStrip2
        '
        Me.ToolStrip2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ToolStrip2.AutoSize = False
        Me.ToolStrip2.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip2.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnHistoryCancel, Me.toolStripSeparator, Me.btnPrintItems, Me.ToolStripSeparator5, Me.btnHistoryHelp})
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 1)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(832, 25)
        Me.ToolStrip2.TabIndex = 0
        Me.ToolStrip2.Text = "ToolStrip2"
        '
        'btnHistoryCancel
        '
        Me.btnHistoryCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHistoryCancel.Name = "btnHistoryCancel"
        Me.btnHistoryCancel.Size = New System.Drawing.Size(67, 22)
        Me.btnHistoryCancel.Text = "&Cancel"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'btnPrintItems
        '
        Me.btnPrintItems.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrintCostSheetToolStripMenuItem1, Me.PrintAllCostSheetToolStripMenuItem})
        Me.btnPrintItems.Image = Global.SimpleAccounts.My.Resources.Resources.print
        Me.btnPrintItems.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrintItems.Name = "btnPrintItems"
        Me.btnPrintItems.Size = New System.Drawing.Size(93, 22)
        Me.btnPrintItems.Text = "Print"
        '
        'PrintCostSheetToolStripMenuItem1
        '
        Me.PrintCostSheetToolStripMenuItem1.Name = "PrintCostSheetToolStripMenuItem1"
        Me.PrintCostSheetToolStripMenuItem1.Size = New System.Drawing.Size(247, 30)
        Me.PrintCostSheetToolStripMenuItem1.Text = "Print Cost Sheet"
        '
        'PrintAllCostSheetToolStripMenuItem
        '
        Me.PrintAllCostSheetToolStripMenuItem.Name = "PrintAllCostSheetToolStripMenuItem"
        Me.PrintAllCostSheetToolStripMenuItem.Size = New System.Drawing.Size(247, 30)
        Me.PrintAllCostSheetToolStripMenuItem.Text = "Print All Cost Sheet"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'btnHistoryHelp
        '
        Me.btnHistoryHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHistoryHelp.Name = "btnHistoryHelp"
        Me.btnHistoryHelp.Size = New System.Drawing.Size(53, 22)
        Me.btnHistoryHelp.Text = "He&lp"
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(871, 733)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 1
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Define Article"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "All Articles"
        UltraTab2.Visible = False
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.VisualStudio2005
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(869, 706)
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'OpenFileDialog2
        '
        Me.OpenFileDialog2.FileName = "OpenFileDialog2"
        '
        'frmDefArticleAdd
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(871, 733)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmDefArticleAdd"
        Me.Text = "Article Definition"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.cmbLPO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.tabAriticalDetail.ResumeLayout(False)
        Me.tabPgInfo.ResumeLayout(False)
        Me.tabPgInfo.PerformLayout()
        Me.tabPgArticalDetail.ResumeLayout(False)
        CType(Me.grdAriticals, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabPgLocation.ResumeLayout(False)
        CType(Me.grdItemLocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabArticleAlias.ResumeLayout(False)
        Me.tabArticleAlias.PerformLayout()
        CType(Me.cmbVendor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpArticleAlias.ResumeLayout(False)
        CType(Me.grdArticleAlias, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPgCostSheet.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        CType(Me.cmbItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpSpecification.ResumeLayout(False)
        Me.grpSpecification.PerformLayout()
        CType(Me.grdCostSheet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPgVendorItems.ResumeLayout(False)
        Me.TabPgVendorItems.PerformLayout()
        CType(Me.grdVendorItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbCustomer.ResumeLayout(False)
        Me.tbCustomer.PerformLayout()
        CType(Me.grdCustomerList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.CostManagement.ResumeLayout(False)
        Me.CostManagement.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.TabPackQuantity.ResumeLayout(False)
        Me.TabPackQuantity.PerformLayout()
        CType(Me.grdPackQty, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdAllRecords, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        CType(Me.BindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents BtnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbType As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCategory As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents uitxtItemCode As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblGender As System.Windows.Forms.Label
    Friend WithEvents uitxtItemName As System.Windows.Forms.TextBox
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents uitxtStockLevelMaximum As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents uitxtStockLevelOptimal As System.Windows.Forms.TextBox
    Friend WithEvents uitxtStockLevel As System.Windows.Forms.TextBox
    Friend WithEvents uitxtSalePrice As System.Windows.Forms.TextBox
    Friend WithEvents lblType As System.Windows.Forms.Label
    Friend WithEvents uitxtPrice As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblCategory As System.Windows.Forms.Label
    Private WithEvents uichkActive As System.Windows.Forms.CheckBox
    Friend WithEvents uitxtPackQty As System.Windows.Forms.TextBox
    Friend WithEvents label9 As System.Windows.Forms.Label
    Friend WithEvents label8 As System.Windows.Forms.Label
    Friend WithEvents uitxtSortOrder As System.Windows.Forms.TextBox
    Friend WithEvents cmbGender As System.Windows.Forms.ComboBox
    Friend WithEvents grdAllRecords As Janus.Windows.GridEX.GridEX
    Friend WithEvents BindingSource1 As System.Windows.Forms.BindingSource
    Friend WithEvents btnAddType As System.Windows.Forms.Button
    Friend WithEvents btnAddLPO As System.Windows.Forms.Button
    Friend WithEvents btnAddGender As System.Windows.Forms.Button
    Friend WithEvents btnAddCompany As System.Windows.Forms.Button
    Friend WithEvents cmbCompany As System.Windows.Forms.ComboBox
    Friend WithEvents lblCompany As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbUnit As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnAddUnit As System.Windows.Forms.Button
    Friend WithEvents lblUnit As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents tabAriticalDetail As System.Windows.Forms.TabControl
    Friend WithEvents tabPgInfo As System.Windows.Forms.TabPage
    Friend WithEvents tabPgArticalDetail As System.Windows.Forms.TabPage
    Friend WithEvents txtOldSalePrice As System.Windows.Forms.TextBox
    Friend WithEvents txtOldPurchasePrice As System.Windows.Forms.TextBox
    Friend WithEvents lblSize As System.Windows.Forms.Label
    Friend WithEvents lblCombinition As System.Windows.Forms.Label
    Friend WithEvents lstSizes As uiListControl
    Friend WithEvents lstCombinitions As uiListControl
    Friend WithEvents grdAriticals As Janus.Windows.GridEX.GridEX
    Friend WithEvents tabPgLocation As System.Windows.Forms.TabPage
    Friend WithEvents grdItemLocation As Janus.Windows.GridEX.GridEX
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents TabPgCostSheet As System.Windows.Forms.TabPage
    Friend WithEvents RdoCode As System.Windows.Forms.RadioButton
    Friend WithEvents cmbItem As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents rdoName As System.Windows.Forms.RadioButton
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnAddCostSheet As System.Windows.Forms.Button
    Friend WithEvents txtCostSheet As System.Windows.Forms.TextBox
    Friend WithEvents grdCostSheet As Janus.Windows.GridEX.GridEX
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents TabPgVendorItems As System.Windows.Forms.TabPage
    Friend WithEvents cmbvendorslist As System.Windows.Forms.ComboBox
    Friend WithEvents lblvendorslist As System.Windows.Forms.Label
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents btnadd As System.Windows.Forms.Button
    Friend WithEvents grdVendorItems As Janus.Windows.GridEX.GridEX
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnAddAccounts As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents AddNewVendorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents chkServerItem As System.Windows.Forms.CheckBox
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lnkUploadPic As System.Windows.Forms.LinkLabel
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents ToolStrip2 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnHistoryCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHistoryHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents CostManagement As System.Windows.Forms.TabPage
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtMarketReturns As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents txtFreight As System.Windows.Forms.TextBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents txtTradePrice As System.Windows.Forms.TextBox
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents rbtTax As System.Windows.Forms.RadioButton
    Friend WithEvents rbtFlatRate As System.Windows.Forms.RadioButton
    Friend WithEvents txtFlatRate As System.Windows.Forms.TextBox
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents txtItemWeight As System.Windows.Forms.TextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents tbCustomer As System.Windows.Forms.TabPage
    Friend WithEvents grdCustomerList As Janus.Windows.GridEX.GridEX
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents txtHSCode As System.Windows.Forms.TextBox
    Friend WithEvents FontDialog1 As System.Windows.Forms.FontDialog
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents txtLargestPackQty As System.Windows.Forms.TextBox
    Friend WithEvents TabPackQuantity As System.Windows.Forms.TabPage
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents txtQuantity As System.Windows.Forms.TextBox
    Friend WithEvents txtPackName As System.Windows.Forms.TextBox
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents btnAddPackQty As System.Windows.Forms.Button
    Friend WithEvents grdPackQty As Janus.Windows.GridEX.GridEX
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents btnPriceUpdate As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents cmbCostSheetUnit As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCategorys As System.Windows.Forms.ComboBox
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents btnCostPriceUpdate As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtCostPrice As System.Windows.Forms.TextBox
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtTaxPercent As System.Windows.Forms.TextBox
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents cmbRemarks As System.Windows.Forms.ComboBox
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents Label39 As System.Windows.Forms.Label
    Friend WithEvents BtnPrint As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents PrintCostSheetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnPrintItems As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents PrintCostSheetToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents PrintAllCostSheetToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PrintAllCostSheetToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnAddBrand As System.Windows.Forms.Button
    Friend WithEvents lblBrand As System.Windows.Forms.Label
    Friend WithEvents Label41 As System.Windows.Forms.Label
    Friend WithEvents cmbBrand As System.Windows.Forms.ComboBox
    Friend WithEvents chkApplyAdjustmentFuelExpense As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents grpSpecification As System.Windows.Forms.GroupBox
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Label40 As System.Windows.Forms.Label
    Friend WithEvents cmbSpecification As System.Windows.Forms.ComboBox
    Friend WithEvents tabArticleAlias As System.Windows.Forms.TabPage
    Friend WithEvents btnAddArticleAlias As System.Windows.Forms.Button
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents Label42 As System.Windows.Forms.Label
    Friend WithEvents cmbVendor As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents txtArticleAliasName As System.Windows.Forms.TextBox
    Friend WithEvents txtArticleAliasCode As System.Windows.Forms.TextBox
    Friend WithEvents Label43 As System.Windows.Forms.Label
    Friend WithEvents lblArticleAliasCode As System.Windows.Forms.Label
    Friend WithEvents grpArticleAlias As System.Windows.Forms.GroupBox
    Friend WithEvents grdArticleAlias As Janus.Windows.GridEX.GridEX
    Friend WithEvents lnkAddCombination As System.Windows.Forms.Label
    Friend WithEvents lnkAddSize As System.Windows.Forms.Label
    Friend WithEvents btnAttachments As System.Windows.Forms.ToolStripButton
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents OpenFileDialog2 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents chkManufacturing As System.Windows.Forms.CheckBox
    Friend WithEvents lnkAddModel As System.Windows.Forms.Label
    Friend WithEvents lblModelList As System.Windows.Forms.Label
    Friend WithEvents lstModelList As SimpleAccounts.uiListControl
    Friend WithEvents cmbLPO As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCostSheet
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
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn12 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Id")
        Dim UltraGridColumn13 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleDescription")
        Dim UltraGridColumn14 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Size")
        Dim UltraGridColumn15 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Combination")
        Dim UltraGridColumn16 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Price", 0)
        Dim UltraGridColumn17 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Stock", 1)
        Dim UltraGridColumn18 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("PurchasePrice", 2)
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance15 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim grdProductionOverHeads_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCostSheet))
        Dim UltraGridBand2 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn9 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleId")
        Dim UltraGridColumn10 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleDescription")
        Dim UltraGridColumn11 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleCode")
        Dim UltraGridColumn19 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Combination")
        Dim Appearance16 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance17 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance18 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance19 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance20 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim grdByProduct_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim UltraGridBand3 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn1 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleId")
        Dim UltraGridColumn2 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleDescription")
        Dim UltraGridColumn3 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleCode")
        Dim UltraGridColumn4 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Combination")
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim grdLabourAllocation_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim UltraTab3 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab4 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab5 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab6 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraGridBand4 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn5 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleId")
        Dim UltraGridColumn6 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleDescription")
        Dim UltraGridColumn7 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleCode")
        Dim UltraGridColumn8 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Combination")
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtTotalQty = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtPercentageCon = New System.Windows.Forms.TextBox()
        Me.lblSubDepartment = New System.Windows.Forms.Label()
        Me.cmbSubDepartment = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmbRemarks = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtTaxPercent = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblQty = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbCategorys = New System.Windows.Forms.ComboBox()
        Me.cmbCostSheetUnit = New System.Windows.Forms.ComboBox()
        Me.btnCopy = New System.Windows.Forms.Button()
        Me.btnAddCostSheet = New System.Windows.Forms.Button()
        Me.txtCostSheet = New System.Windows.Forms.TextBox()
        Me.RdoCode = New System.Windows.Forms.RadioButton()
        Me.cmbItem = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.rdoName = New System.Windows.Forms.RadioButton()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.grdCostSheet = New Janus.Windows.GridEX.GridEX()
        Me.UltraTabPageControl3 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.btnAddProductionOverHeads = New System.Windows.Forms.Button()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.cmbAccount = New System.Windows.Forms.ComboBox()
        Me.cmbProductionStep = New System.Windows.Forms.ComboBox()
        Me.grdProductionOverHeads = New Janus.Windows.GridEX.GridEX()
        Me.lblAmount = New System.Windows.Forms.Label()
        Me.lblRemarks = New System.Windows.Forms.Label()
        Me.lblAccount = New System.Windows.Forms.Label()
        Me.lblProductionStage = New System.Windows.Forms.Label()
        Me.UltraTabPageControl5 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.txtByProductQty = New System.Windows.Forms.TextBox()
        Me.lblByProductQty = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rboByProductName = New System.Windows.Forms.RadioButton()
        Me.rboByProductCode = New System.Windows.Forms.RadioButton()
        Me.cmbByProduct = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.btnByProductAdd = New System.Windows.Forms.Button()
        Me.txtByProductRemarks = New System.Windows.Forms.TextBox()
        Me.txtByProductRate = New System.Windows.Forms.TextBox()
        Me.grdByProduct = New Janus.Windows.GridEX.GridEX()
        Me.lblByProductRate = New System.Windows.Forms.Label()
        Me.lblByProductRemarks = New System.Windows.Forms.Label()
        Me.lblProduct = New System.Windows.Forms.Label()
        Me.UltraTabPageControl6 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.btnAddLabourAllocation = New System.Windows.Forms.Button()
        Me.cmbLabourType = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txtPerUnitRate = New System.Windows.Forms.TextBox()
        Me.cmbStage = New System.Windows.Forms.ComboBox()
        Me.grdLabourAllocation = New Janus.Windows.GridEX.GridEX()
        Me.lblPerUnitRate = New System.Windows.Forms.Label()
        Me.lblByProductLabourType = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.UltraTabControl3 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage2 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.RdoCode1 = New System.Windows.Forms.RadioButton()
        Me.RdoName1 = New System.Windows.Forms.RadioButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbDepartment = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtSalePrice = New System.Windows.Forms.TextBox()
        Me.txtPurchasePrice = New System.Windows.Forms.TextBox()
        Me.txtPackQty = New System.Windows.Forms.TextBox()
        Me.cmbMasterItem = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.UltraTabPageControl4 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdSaved = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnPriceUpdate = New System.Windows.Forms.ToolStripButton()
        Me.btnCostPriceUpdate = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnCostSheetDetail = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.UltraTabPageControl2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.cmbItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdCostSheet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl3.SuspendLayout()
        CType(Me.grdProductionOverHeads, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl5.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.cmbByProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdByProduct, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl6.SuspendLayout()
        CType(Me.cmbLabourType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdLabourAllocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl1.SuspendLayout()
        CType(Me.UltraTabControl3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.cmbMasterItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl4.SuspendLayout()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl2.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.lblProgress)
        Me.UltraTabPageControl2.Controls.Add(Me.GroupBox1)
        Me.UltraTabPageControl2.Controls.Add(Me.grdCostSheet)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(1, 20)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(678, 214)
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(180, 114)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 4
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.txtTotalQty)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtPercentageCon)
        Me.GroupBox1.Controls.Add(Me.lblSubDepartment)
        Me.GroupBox1.Controls.Add(Me.cmbSubDepartment)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.cmbRemarks)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtTaxPercent)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.lblQty)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.cmbCategorys)
        Me.GroupBox1.Controls.Add(Me.cmbCostSheetUnit)
        Me.GroupBox1.Controls.Add(Me.btnCopy)
        Me.GroupBox1.Controls.Add(Me.btnAddCostSheet)
        Me.GroupBox1.Controls.Add(Me.txtCostSheet)
        Me.GroupBox1.Controls.Add(Me.RdoCode)
        Me.GroupBox1.Controls.Add(Me.cmbItem)
        Me.GroupBox1.Controls.Add(Me.rdoName)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(660, 95)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Article Detail"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(426, 15)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(67, 13)
        Me.Label12.TabIndex = 10
        Me.Label12.Text = "Cons. with %"
        '
        'txtTotalQty
        '
        Me.txtTotalQty.Location = New System.Drawing.Point(429, 31)
        Me.txtTotalQty.MaxLength = 50
        Me.txtTotalQty.Name = "txtTotalQty"
        Me.txtTotalQty.Size = New System.Drawing.Size(63, 20)
        Me.txtTotalQty.TabIndex = 11
        Me.txtTotalQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(357, 15)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(33, 13)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "%age"
        '
        'txtPercentageCon
        '
        Me.txtPercentageCon.Location = New System.Drawing.Point(360, 31)
        Me.txtPercentageCon.MaxLength = 50
        Me.txtPercentageCon.Name = "txtPercentageCon"
        Me.txtPercentageCon.Size = New System.Drawing.Size(63, 20)
        Me.txtPercentageCon.TabIndex = 9
        Me.txtPercentageCon.Text = "0"
        Me.txtPercentageCon.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSubDepartment
        '
        Me.lblSubDepartment.AutoSize = True
        Me.lblSubDepartment.Location = New System.Drawing.Point(3, 55)
        Me.lblSubDepartment.Name = "lblSubDepartment"
        Me.lblSubDepartment.Size = New System.Drawing.Size(87, 13)
        Me.lblSubDepartment.TabIndex = 14
        Me.lblSubDepartment.Text = "Prod Department"
        '
        'cmbSubDepartment
        '
        Me.cmbSubDepartment.FormattingEnabled = True
        Me.cmbSubDepartment.Location = New System.Drawing.Point(6, 70)
        Me.cmbSubDepartment.Name = "cmbSubDepartment"
        Me.cmbSubDepartment.Size = New System.Drawing.Size(193, 21)
        Me.cmbSubDepartment.TabIndex = 15
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(356, 55)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(49, 13)
        Me.Label9.TabIndex = 18
        Me.Label9.Text = "Remarks"
        '
        'cmbRemarks
        '
        Me.cmbRemarks.FormattingEnabled = True
        Me.cmbRemarks.Location = New System.Drawing.Point(359, 70)
        Me.cmbRemarks.Name = "cmbRemarks"
        Me.cmbRemarks.Size = New System.Drawing.Size(203, 21)
        Me.cmbRemarks.TabIndex = 19
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(495, 14)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(36, 13)
        Me.Label8.TabIndex = 12
        Me.Label8.Text = "Tax %"
        '
        'txtTaxPercent
        '
        Me.txtTaxPercent.Location = New System.Drawing.Point(498, 31)
        Me.txtTaxPercent.MaxLength = 50
        Me.txtTaxPercent.Name = "txtTaxPercent"
        Me.txtTaxPercent.Size = New System.Drawing.Size(65, 20)
        Me.txtTaxPercent.TabIndex = 13
        Me.txtTaxPercent.Text = "1"
        Me.txtTaxPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(202, 55)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Category"
        '
        'lblQty
        '
        Me.lblQty.AutoSize = True
        Me.lblQty.Location = New System.Drawing.Point(288, 15)
        Me.lblQty.Name = "lblQty"
        Me.lblQty.Size = New System.Drawing.Size(23, 13)
        Me.lblQty.TabIndex = 6
        Me.lblQty.Text = "Qty"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(203, 14)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(26, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Unit"
        '
        'cmbCategorys
        '
        Me.cmbCategorys.FormattingEnabled = True
        Me.cmbCategorys.Location = New System.Drawing.Point(205, 70)
        Me.cmbCategorys.Name = "cmbCategorys"
        Me.cmbCategorys.Size = New System.Drawing.Size(148, 21)
        Me.cmbCategorys.TabIndex = 17
        '
        'cmbCostSheetUnit
        '
        Me.cmbCostSheetUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCostSheetUnit.FormattingEnabled = True
        Me.cmbCostSheetUnit.Items.AddRange(New Object() {"Loose", "Batch"})
        Me.cmbCostSheetUnit.Location = New System.Drawing.Point(206, 31)
        Me.cmbCostSheetUnit.Name = "cmbCostSheetUnit"
        Me.cmbCostSheetUnit.Size = New System.Drawing.Size(80, 21)
        Me.cmbCostSheetUnit.TabIndex = 5
        '
        'btnCopy
        '
        Me.btnCopy.Location = New System.Drawing.Point(601, 68)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(46, 23)
        Me.btnCopy.TabIndex = 21
        Me.btnCopy.Text = "Copy"
        Me.btnCopy.UseVisualStyleBackColor = True
        '
        'btnAddCostSheet
        '
        Me.btnAddCostSheet.Location = New System.Drawing.Point(568, 68)
        Me.btnAddCostSheet.Name = "btnAddCostSheet"
        Me.btnAddCostSheet.Size = New System.Drawing.Size(29, 23)
        Me.btnAddCostSheet.TabIndex = 20
        Me.btnAddCostSheet.Text = "+"
        Me.btnAddCostSheet.UseVisualStyleBackColor = True
        '
        'txtCostSheet
        '
        Me.txtCostSheet.Location = New System.Drawing.Point(291, 31)
        Me.txtCostSheet.MaxLength = 50
        Me.txtCostSheet.Name = "txtCostSheet"
        Me.txtCostSheet.Size = New System.Drawing.Size(63, 20)
        Me.txtCostSheet.TabIndex = 7
        Me.txtCostSheet.Text = "1"
        Me.txtCostSheet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'RdoCode
        '
        Me.RdoCode.AutoSize = True
        Me.RdoCode.Checked = True
        Me.RdoCode.Location = New System.Drawing.Point(96, 12)
        Me.RdoCode.Name = "RdoCode"
        Me.RdoCode.Size = New System.Drawing.Size(50, 17)
        Me.RdoCode.TabIndex = 2
        Me.RdoCode.TabStop = True
        Me.RdoCode.Text = "Code"
        Me.RdoCode.UseVisualStyleBackColor = True
        '
        'cmbItem
        '
        Me.cmbItem.CheckedListSettings.CheckStateMember = ""
        UltraGridColumn12.Header.VisiblePosition = 0
        UltraGridColumn12.Hidden = True
        UltraGridColumn13.Header.VisiblePosition = 1
        UltraGridColumn14.Header.VisiblePosition = 2
        UltraGridColumn14.Width = 70
        UltraGridColumn15.Header.VisiblePosition = 3
        UltraGridColumn15.Width = 80
        UltraGridColumn16.Header.VisiblePosition = 4
        UltraGridColumn16.MaxWidth = 80
        UltraGridColumn17.Header.VisiblePosition = 5
        UltraGridColumn17.MaxWidth = 60
        UltraGridColumn18.Header.VisiblePosition = 6
        UltraGridColumn18.Hidden = True
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn12, UltraGridColumn13, UltraGridColumn14, UltraGridColumn15, UltraGridColumn16, UltraGridColumn17, UltraGridColumn18})
        Me.cmbItem.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbItem.DisplayLayout.InterBandSpacing = 10
        Me.cmbItem.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbItem.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbItem.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Appearance11.BackColor = System.Drawing.Color.Transparent
        Me.cmbItem.DisplayLayout.Override.CardAreaAppearance = Appearance11
        Me.cmbItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbItem.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance12.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance12.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance12.ForeColor = System.Drawing.Color.White
        Appearance12.TextHAlignAsString = "Left"
        Appearance12.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent
        Me.cmbItem.DisplayLayout.Override.HeaderAppearance = Appearance12
        Me.cmbItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance13.BorderColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbItem.DisplayLayout.Override.RowAppearance = Appearance13
        Appearance14.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance14.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.cmbItem.DisplayLayout.Override.RowSelectorAppearance = Appearance14
        Me.cmbItem.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbItem.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance15.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance15.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance15.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance15.ForeColor = System.Drawing.Color.Black
        Me.cmbItem.DisplayLayout.Override.SelectedRowAppearance = Appearance15
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
        Me.cmbItem.Location = New System.Drawing.Point(6, 30)
        Me.cmbItem.MaxDropDownItems = 20
        Me.cmbItem.Name = "cmbItem"
        Me.cmbItem.Size = New System.Drawing.Size(194, 22)
        Me.cmbItem.TabIndex = 1
        '
        'rdoName
        '
        Me.rdoName.AutoSize = True
        Me.rdoName.Location = New System.Drawing.Point(152, 12)
        Me.rdoName.Name = "rdoName"
        Me.rdoName.Size = New System.Drawing.Size(53, 17)
        Me.rdoName.TabIndex = 3
        Me.rdoName.Text = "Name"
        Me.rdoName.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(3, 14)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(27, 13)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Item"
        '
        'grdCostSheet
        '
        Me.grdCostSheet.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdCostSheet.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdCostSheet.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdCostSheet.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdCostSheet.GroupByBoxVisible = False
        Me.grdCostSheet.Location = New System.Drawing.Point(3, 101)
        Me.grdCostSheet.Name = "grdCostSheet"
        Me.grdCostSheet.RecordNavigator = True
        Me.grdCostSheet.Size = New System.Drawing.Size(670, 110)
        Me.grdCostSheet.TabIndex = 3
        Me.grdCostSheet.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdCostSheet.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdCostSheet.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'UltraTabPageControl3
        '
        Me.UltraTabPageControl3.Controls.Add(Me.btnAddProductionOverHeads)
        Me.UltraTabPageControl3.Controls.Add(Me.txtRemarks)
        Me.UltraTabPageControl3.Controls.Add(Me.txtAmount)
        Me.UltraTabPageControl3.Controls.Add(Me.cmbAccount)
        Me.UltraTabPageControl3.Controls.Add(Me.cmbProductionStep)
        Me.UltraTabPageControl3.Controls.Add(Me.grdProductionOverHeads)
        Me.UltraTabPageControl3.Controls.Add(Me.lblAmount)
        Me.UltraTabPageControl3.Controls.Add(Me.lblRemarks)
        Me.UltraTabPageControl3.Controls.Add(Me.lblAccount)
        Me.UltraTabPageControl3.Controls.Add(Me.lblProductionStage)
        Me.UltraTabPageControl3.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl3.Name = "UltraTabPageControl3"
        Me.UltraTabPageControl3.Size = New System.Drawing.Size(678, 214)
        '
        'btnAddProductionOverHeads
        '
        Me.btnAddProductionOverHeads.Location = New System.Drawing.Point(599, 40)
        Me.btnAddProductionOverHeads.Name = "btnAddProductionOverHeads"
        Me.btnAddProductionOverHeads.Size = New System.Drawing.Size(46, 23)
        Me.btnAddProductionOverHeads.TabIndex = 8
        Me.btnAddProductionOverHeads.Text = "Add"
        Me.btnAddProductionOverHeads.UseVisualStyleBackColor = True
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(98, 33)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(494, 31)
        Me.txtRemarks.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.txtRemarks, "Type remarks here")
        '
        'txtAmount
        '
        Me.txtAmount.Location = New System.Drawing.Point(515, 6)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(77, 20)
        Me.txtAmount.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.txtAmount, "Type amount here")
        '
        'cmbAccount
        '
        Me.cmbAccount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbAccount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbAccount.FormattingEnabled = True
        Me.cmbAccount.Location = New System.Drawing.Point(309, 6)
        Me.cmbAccount.Name = "cmbAccount"
        Me.cmbAccount.Size = New System.Drawing.Size(153, 21)
        Me.cmbAccount.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.cmbAccount, "Select a account")
        '
        'cmbProductionStep
        '
        Me.cmbProductionStep.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbProductionStep.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbProductionStep.FormattingEnabled = True
        Me.cmbProductionStep.Location = New System.Drawing.Point(98, 6)
        Me.cmbProductionStep.Name = "cmbProductionStep"
        Me.cmbProductionStep.Size = New System.Drawing.Size(152, 21)
        Me.cmbProductionStep.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbProductionStep, "Select production stage")
        '
        'grdProductionOverHeads
        '
        Me.grdProductionOverHeads.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdProductionOverHeads_DesignTimeLayout.LayoutString = resources.GetString("grdProductionOverHeads_DesignTimeLayout.LayoutString")
        Me.grdProductionOverHeads.DesignTimeLayout = grdProductionOverHeads_DesignTimeLayout
        Me.grdProductionOverHeads.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdProductionOverHeads.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdProductionOverHeads.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdProductionOverHeads.Location = New System.Drawing.Point(3, 70)
        Me.grdProductionOverHeads.Name = "grdProductionOverHeads"
        Me.grdProductionOverHeads.RecordNavigator = True
        Me.grdProductionOverHeads.Size = New System.Drawing.Size(669, 141)
        Me.grdProductionOverHeads.TabIndex = 9
        Me.grdProductionOverHeads.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'lblAmount
        '
        Me.lblAmount.AutoSize = True
        Me.lblAmount.Location = New System.Drawing.Point(468, 9)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(43, 13)
        Me.lblAmount.TabIndex = 4
        Me.lblAmount.Text = "Amount"
        '
        'lblRemarks
        '
        Me.lblRemarks.AutoSize = True
        Me.lblRemarks.Location = New System.Drawing.Point(43, 33)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(49, 13)
        Me.lblRemarks.TabIndex = 6
        Me.lblRemarks.Text = "Remarks"
        '
        'lblAccount
        '
        Me.lblAccount.AutoSize = True
        Me.lblAccount.Location = New System.Drawing.Point(256, 9)
        Me.lblAccount.Name = "lblAccount"
        Me.lblAccount.Size = New System.Drawing.Size(47, 13)
        Me.lblAccount.TabIndex = 2
        Me.lblAccount.Text = "Account"
        '
        'lblProductionStage
        '
        Me.lblProductionStage.AutoSize = True
        Me.lblProductionStage.Location = New System.Drawing.Point(3, 9)
        Me.lblProductionStage.Name = "lblProductionStage"
        Me.lblProductionStage.Size = New System.Drawing.Size(89, 13)
        Me.lblProductionStage.TabIndex = 0
        Me.lblProductionStage.Text = "Production Stage"
        '
        'UltraTabPageControl5
        '
        Me.UltraTabPageControl5.Controls.Add(Me.txtByProductQty)
        Me.UltraTabPageControl5.Controls.Add(Me.lblByProductQty)
        Me.UltraTabPageControl5.Controls.Add(Me.Panel1)
        Me.UltraTabPageControl5.Controls.Add(Me.cmbByProduct)
        Me.UltraTabPageControl5.Controls.Add(Me.btnByProductAdd)
        Me.UltraTabPageControl5.Controls.Add(Me.txtByProductRemarks)
        Me.UltraTabPageControl5.Controls.Add(Me.txtByProductRate)
        Me.UltraTabPageControl5.Controls.Add(Me.grdByProduct)
        Me.UltraTabPageControl5.Controls.Add(Me.lblByProductRate)
        Me.UltraTabPageControl5.Controls.Add(Me.lblByProductRemarks)
        Me.UltraTabPageControl5.Controls.Add(Me.lblProduct)
        Me.UltraTabPageControl5.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl5.Name = "UltraTabPageControl5"
        Me.UltraTabPageControl5.Size = New System.Drawing.Size(678, 214)
        '
        'txtByProductQty
        '
        Me.txtByProductQty.Location = New System.Drawing.Point(391, 24)
        Me.txtByProductQty.Name = "txtByProductQty"
        Me.txtByProductQty.Size = New System.Drawing.Size(87, 20)
        Me.txtByProductQty.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.txtByProductQty, "Type Qty here")
        '
        'lblByProductQty
        '
        Me.lblByProductQty.AutoSize = True
        Me.lblByProductQty.Location = New System.Drawing.Point(362, 27)
        Me.lblByProductQty.Name = "lblByProductQty"
        Me.lblByProductQty.Size = New System.Drawing.Size(23, 13)
        Me.lblByProductQty.TabIndex = 4
        Me.lblByProductQty.Text = "Qty"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rboByProductName)
        Me.Panel1.Controls.Add(Me.rboByProductCode)
        Me.Panel1.Location = New System.Drawing.Point(99, 1)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(120, 19)
        Me.Panel1.TabIndex = 22
        '
        'rboByProductName
        '
        Me.rboByProductName.AutoSize = True
        Me.rboByProductName.Location = New System.Drawing.Point(64, 2)
        Me.rboByProductName.Name = "rboByProductName"
        Me.rboByProductName.Size = New System.Drawing.Size(53, 17)
        Me.rboByProductName.TabIndex = 1
        Me.rboByProductName.Text = "Name"
        Me.rboByProductName.UseVisualStyleBackColor = True
        '
        'rboByProductCode
        '
        Me.rboByProductCode.AutoSize = True
        Me.rboByProductCode.Checked = True
        Me.rboByProductCode.Location = New System.Drawing.Point(0, 2)
        Me.rboByProductCode.Name = "rboByProductCode"
        Me.rboByProductCode.Size = New System.Drawing.Size(50, 17)
        Me.rboByProductCode.TabIndex = 0
        Me.rboByProductCode.TabStop = True
        Me.rboByProductCode.Text = "Code"
        Me.rboByProductCode.UseVisualStyleBackColor = True
        '
        'cmbByProduct
        '
        Me.cmbByProduct.CheckedListSettings.CheckStateMember = ""
        UltraGridColumn9.Header.VisiblePosition = 0
        UltraGridColumn9.Hidden = True
        UltraGridColumn10.Header.VisiblePosition = 1
        UltraGridColumn11.Header.VisiblePosition = 2
        UltraGridColumn11.Width = 70
        UltraGridColumn19.Header.VisiblePosition = 3
        UltraGridColumn19.Width = 80
        UltraGridBand2.Columns.AddRange(New Object() {UltraGridColumn9, UltraGridColumn10, UltraGridColumn11, UltraGridColumn19})
        Me.cmbByProduct.DisplayLayout.BandsSerializer.Add(UltraGridBand2)
        Me.cmbByProduct.DisplayLayout.InterBandSpacing = 10
        Me.cmbByProduct.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbByProduct.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbByProduct.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Appearance16.BackColor = System.Drawing.Color.Transparent
        Me.cmbByProduct.DisplayLayout.Override.CardAreaAppearance = Appearance16
        Me.cmbByProduct.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbByProduct.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance17.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance17.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance17.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance17.ForeColor = System.Drawing.Color.White
        Appearance17.TextHAlignAsString = "Left"
        Appearance17.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent
        Me.cmbByProduct.DisplayLayout.Override.HeaderAppearance = Appearance17
        Me.cmbByProduct.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance18.BorderColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbByProduct.DisplayLayout.Override.RowAppearance = Appearance18
        Appearance19.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance19.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance19.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.cmbByProduct.DisplayLayout.Override.RowSelectorAppearance = Appearance19
        Me.cmbByProduct.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbByProduct.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance20.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance20.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance20.ForeColor = System.Drawing.Color.Black
        Me.cmbByProduct.DisplayLayout.Override.SelectedRowAppearance = Appearance20
        Me.cmbByProduct.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbByProduct.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbByProduct.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbByProduct.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbByProduct.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.cmbByProduct.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbByProduct.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbByProduct.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbByProduct.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbByProduct.LimitToList = True
        Me.cmbByProduct.Location = New System.Drawing.Point(57, 23)
        Me.cmbByProduct.MaxDropDownItems = 20
        Me.cmbByProduct.Name = "cmbByProduct"
        Me.cmbByProduct.Size = New System.Drawing.Size(162, 22)
        Me.cmbByProduct.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbByProduct, "Select a product")
        '
        'btnByProductAdd
        '
        Me.btnByProductAdd.Location = New System.Drawing.Point(484, 55)
        Me.btnByProductAdd.Name = "btnByProductAdd"
        Me.btnByProductAdd.Size = New System.Drawing.Size(46, 23)
        Me.btnByProductAdd.TabIndex = 8
        Me.btnByProductAdd.Text = "Add"
        Me.ToolTip1.SetToolTip(Me.btnByProductAdd, "Add to grid")
        Me.btnByProductAdd.UseVisualStyleBackColor = True
        '
        'txtByProductRemarks
        '
        Me.txtByProductRemarks.Location = New System.Drawing.Point(57, 50)
        Me.txtByProductRemarks.Multiline = True
        Me.txtByProductRemarks.Name = "txtByProductRemarks"
        Me.txtByProductRemarks.Size = New System.Drawing.Size(421, 27)
        Me.txtByProductRemarks.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.txtByProductRemarks, "Type remarks here")
        '
        'txtByProductRate
        '
        Me.txtByProductRate.Location = New System.Drawing.Point(261, 24)
        Me.txtByProductRate.Name = "txtByProductRate"
        Me.txtByProductRate.Size = New System.Drawing.Size(95, 20)
        Me.txtByProductRate.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtByProductRate, "Type rate here")
        '
        'grdByProduct
        '
        Me.grdByProduct.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdByProduct_DesignTimeLayout.LayoutString = resources.GetString("grdByProduct_DesignTimeLayout.LayoutString")
        Me.grdByProduct.DesignTimeLayout = grdByProduct_DesignTimeLayout
        Me.grdByProduct.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdByProduct.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdByProduct.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdByProduct.Location = New System.Drawing.Point(5, 84)
        Me.grdByProduct.Name = "grdByProduct"
        Me.grdByProduct.RecordNavigator = True
        Me.grdByProduct.Size = New System.Drawing.Size(669, 126)
        Me.grdByProduct.TabIndex = 9
        Me.grdByProduct.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'lblByProductRate
        '
        Me.lblByProductRate.AutoSize = True
        Me.lblByProductRate.Location = New System.Drawing.Point(225, 27)
        Me.lblByProductRate.Name = "lblByProductRate"
        Me.lblByProductRate.Size = New System.Drawing.Size(30, 13)
        Me.lblByProductRate.TabIndex = 2
        Me.lblByProductRate.Text = "Rate"
        '
        'lblByProductRemarks
        '
        Me.lblByProductRemarks.AutoSize = True
        Me.lblByProductRemarks.Location = New System.Drawing.Point(4, 50)
        Me.lblByProductRemarks.Name = "lblByProductRemarks"
        Me.lblByProductRemarks.Size = New System.Drawing.Size(49, 13)
        Me.lblByProductRemarks.TabIndex = 6
        Me.lblByProductRemarks.Text = "Remarks"
        '
        'lblProduct
        '
        Me.lblProduct.AutoSize = True
        Me.lblProduct.Location = New System.Drawing.Point(9, 27)
        Me.lblProduct.Name = "lblProduct"
        Me.lblProduct.Size = New System.Drawing.Size(44, 13)
        Me.lblProduct.TabIndex = 0
        Me.lblProduct.Text = "Product"
        '
        'UltraTabPageControl6
        '
        Me.UltraTabPageControl6.Controls.Add(Me.btnAddLabourAllocation)
        Me.UltraTabPageControl6.Controls.Add(Me.cmbLabourType)
        Me.UltraTabPageControl6.Controls.Add(Me.txtPerUnitRate)
        Me.UltraTabPageControl6.Controls.Add(Me.cmbStage)
        Me.UltraTabPageControl6.Controls.Add(Me.grdLabourAllocation)
        Me.UltraTabPageControl6.Controls.Add(Me.lblPerUnitRate)
        Me.UltraTabPageControl6.Controls.Add(Me.lblByProductLabourType)
        Me.UltraTabPageControl6.Controls.Add(Me.Label16)
        Me.UltraTabPageControl6.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl6.Name = "UltraTabPageControl6"
        Me.UltraTabPageControl6.Size = New System.Drawing.Size(678, 214)
        '
        'btnAddLabourAllocation
        '
        Me.btnAddLabourAllocation.Location = New System.Drawing.Point(639, 5)
        Me.btnAddLabourAllocation.Name = "btnAddLabourAllocation"
        Me.btnAddLabourAllocation.Size = New System.Drawing.Size(35, 23)
        Me.btnAddLabourAllocation.TabIndex = 6
        Me.btnAddLabourAllocation.Text = "Add"
        Me.btnAddLabourAllocation.UseVisualStyleBackColor = True
        '
        'cmbLabourType
        '
        Me.cmbLabourType.CheckedListSettings.CheckStateMember = ""
        UltraGridColumn1.Header.VisiblePosition = 0
        UltraGridColumn1.Hidden = True
        UltraGridColumn2.Header.VisiblePosition = 1
        UltraGridColumn3.Header.VisiblePosition = 2
        UltraGridColumn3.Width = 70
        UltraGridColumn4.Header.VisiblePosition = 3
        UltraGridColumn4.Width = 80
        UltraGridBand3.Columns.AddRange(New Object() {UltraGridColumn1, UltraGridColumn2, UltraGridColumn3, UltraGridColumn4})
        Me.cmbLabourType.DisplayLayout.BandsSerializer.Add(UltraGridBand3)
        Me.cmbLabourType.DisplayLayout.InterBandSpacing = 10
        Me.cmbLabourType.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbLabourType.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbLabourType.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Appearance1.BackColor = System.Drawing.Color.Transparent
        Me.cmbLabourType.DisplayLayout.Override.CardAreaAppearance = Appearance1
        Me.cmbLabourType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbLabourType.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance2.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance2.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance2.ForeColor = System.Drawing.Color.White
        Appearance2.TextHAlignAsString = "Left"
        Appearance2.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent
        Me.cmbLabourType.DisplayLayout.Override.HeaderAppearance = Appearance2
        Me.cmbLabourType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance3.BorderColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbLabourType.DisplayLayout.Override.RowAppearance = Appearance3
        Appearance4.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance4.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.cmbLabourType.DisplayLayout.Override.RowSelectorAppearance = Appearance4
        Me.cmbLabourType.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbLabourType.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance5.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance5.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance5.ForeColor = System.Drawing.Color.Black
        Me.cmbLabourType.DisplayLayout.Override.SelectedRowAppearance = Appearance5
        Me.cmbLabourType.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbLabourType.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbLabourType.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbLabourType.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbLabourType.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.cmbLabourType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbLabourType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbLabourType.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbLabourType.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbLabourType.LimitToList = True
        Me.cmbLabourType.Location = New System.Drawing.Point(325, 5)
        Me.cmbLabourType.MaxDropDownItems = 20
        Me.cmbLabourType.Name = "cmbLabourType"
        Me.cmbLabourType.Size = New System.Drawing.Size(159, 22)
        Me.cmbLabourType.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.cmbLabourType, "Select labour type")
        '
        'txtPerUnitRate
        '
        Me.txtPerUnitRate.Location = New System.Drawing.Point(566, 7)
        Me.txtPerUnitRate.Name = "txtPerUnitRate"
        Me.txtPerUnitRate.Size = New System.Drawing.Size(69, 20)
        Me.txtPerUnitRate.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.txtPerUnitRate, "Type Per Unit Rate")
        '
        'cmbStage
        '
        Me.cmbStage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbStage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbStage.FormattingEnabled = True
        Me.cmbStage.Location = New System.Drawing.Point(98, 5)
        Me.cmbStage.Name = "cmbStage"
        Me.cmbStage.Size = New System.Drawing.Size(153, 21)
        Me.cmbStage.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbStage, "Select a stage")
        '
        'grdLabourAllocation
        '
        Me.grdLabourAllocation.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdLabourAllocation_DesignTimeLayout.LayoutString = resources.GetString("grdLabourAllocation_DesignTimeLayout.LayoutString")
        Me.grdLabourAllocation.DesignTimeLayout = grdLabourAllocation_DesignTimeLayout
        Me.grdLabourAllocation.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdLabourAllocation.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdLabourAllocation.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdLabourAllocation.Location = New System.Drawing.Point(5, 32)
        Me.grdLabourAllocation.Name = "grdLabourAllocation"
        Me.grdLabourAllocation.RecordNavigator = True
        Me.grdLabourAllocation.Size = New System.Drawing.Size(669, 178)
        Me.grdLabourAllocation.TabIndex = 7
        Me.grdLabourAllocation.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'lblPerUnitRate
        '
        Me.lblPerUnitRate.AutoSize = True
        Me.lblPerUnitRate.Location = New System.Drawing.Point(489, 10)
        Me.lblPerUnitRate.Name = "lblPerUnitRate"
        Me.lblPerUnitRate.Size = New System.Drawing.Size(71, 13)
        Me.lblPerUnitRate.TabIndex = 4
        Me.lblPerUnitRate.Text = "Per Unit Rate"
        '
        'lblByProductLabourType
        '
        Me.lblByProductLabourType.AutoSize = True
        Me.lblByProductLabourType.Location = New System.Drawing.Point(254, 10)
        Me.lblByProductLabourType.Name = "lblByProductLabourType"
        Me.lblByProductLabourType.Size = New System.Drawing.Size(67, 13)
        Me.lblByProductLabourType.TabIndex = 2
        Me.lblByProductLabourType.Text = "Labour Type"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(3, 10)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(89, 13)
        Me.Label16.TabIndex = 0
        Me.Label16.Text = "Production Stage"
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.UltraTabControl3)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox2)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(682, 355)
        '
        'UltraTabControl3
        '
        Me.UltraTabControl3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UltraTabControl3.Controls.Add(Me.UltraTabSharedControlsPage2)
        Me.UltraTabControl3.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl3.Controls.Add(Me.UltraTabPageControl3)
        Me.UltraTabControl3.Controls.Add(Me.UltraTabPageControl5)
        Me.UltraTabControl3.Controls.Add(Me.UltraTabPageControl6)
        Me.UltraTabControl3.Location = New System.Drawing.Point(3, 115)
        Me.UltraTabControl3.Name = "UltraTabControl3"
        Me.UltraTabControl3.SharedControlsPage = Me.UltraTabSharedControlsPage2
        Me.UltraTabControl3.Size = New System.Drawing.Size(680, 235)
        Me.UltraTabControl3.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl3.TabIndex = 5
        UltraTab3.TabPage = Me.UltraTabPageControl2
        UltraTab3.Text = "Detail"
        UltraTab4.TabPage = Me.UltraTabPageControl3
        UltraTab4.Text = "Production Over Heads"
        UltraTab5.TabPage = Me.UltraTabPageControl5
        UltraTab5.Text = "By Product"
        UltraTab6.TabPage = Me.UltraTabPageControl6
        UltraTab6.Text = "Labour Allocation"
        Me.UltraTabControl3.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab3, UltraTab4, UltraTab5, UltraTab6})
        Me.UltraTabControl3.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007
        '
        'UltraTabSharedControlsPage2
        '
        Me.UltraTabSharedControlsPage2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage2.Name = "UltraTabSharedControlsPage2"
        Me.UltraTabSharedControlsPage2.Size = New System.Drawing.Size(678, 214)
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.RdoCode1)
        Me.GroupBox2.Controls.Add(Me.RdoName1)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.cmbDepartment)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.txtSalePrice)
        Me.GroupBox2.Controls.Add(Me.txtPurchasePrice)
        Me.GroupBox2.Controls.Add(Me.txtPackQty)
        Me.GroupBox2.Controls.Add(Me.cmbMasterItem)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Location = New System.Drawing.Point(4, 45)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(672, 64)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Article Master"
        '
        'RdoCode1
        '
        Me.RdoCode1.AutoSize = True
        Me.RdoCode1.Checked = True
        Me.RdoCode1.Location = New System.Drawing.Point(212, 19)
        Me.RdoCode1.Name = "RdoCode1"
        Me.RdoCode1.Size = New System.Drawing.Size(50, 17)
        Me.RdoCode1.TabIndex = 4
        Me.RdoCode1.TabStop = True
        Me.RdoCode1.Text = "Code"
        Me.RdoCode1.UseVisualStyleBackColor = True
        '
        'RdoName1
        '
        Me.RdoName1.AutoSize = True
        Me.RdoName1.Location = New System.Drawing.Point(268, 19)
        Me.RdoName1.Name = "RdoName1"
        Me.RdoName1.Size = New System.Drawing.Size(53, 17)
        Me.RdoName1.TabIndex = 5
        Me.RdoName1.Text = "Name"
        Me.RdoName1.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(3, 21)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(62, 13)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Department"
        '
        'cmbDepartment
        '
        Me.cmbDepartment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbDepartment.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbDepartment.FormattingEnabled = True
        Me.cmbDepartment.Location = New System.Drawing.Point(6, 37)
        Me.cmbDepartment.Name = "cmbDepartment"
        Me.cmbDepartment.Size = New System.Drawing.Size(113, 21)
        Me.cmbDepartment.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(490, 23)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Sale Price"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(405, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(79, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Purchase Price"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(322, 23)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Pack Qty"
        '
        'txtSalePrice
        '
        Me.txtSalePrice.Location = New System.Drawing.Point(492, 39)
        Me.txtSalePrice.Name = "txtSalePrice"
        Me.txtSalePrice.ReadOnly = True
        Me.txtSalePrice.Size = New System.Drawing.Size(100, 20)
        Me.txtSalePrice.TabIndex = 11
        '
        'txtPurchasePrice
        '
        Me.txtPurchasePrice.Location = New System.Drawing.Point(408, 39)
        Me.txtPurchasePrice.Name = "txtPurchasePrice"
        Me.txtPurchasePrice.ReadOnly = True
        Me.txtPurchasePrice.Size = New System.Drawing.Size(78, 20)
        Me.txtPurchasePrice.TabIndex = 9
        '
        'txtPackQty
        '
        Me.txtPackQty.Location = New System.Drawing.Point(325, 39)
        Me.txtPackQty.Name = "txtPackQty"
        Me.txtPackQty.ReadOnly = True
        Me.txtPackQty.Size = New System.Drawing.Size(78, 20)
        Me.txtPackQty.TabIndex = 7
        '
        'cmbMasterItem
        '
        Me.cmbMasterItem.CheckedListSettings.CheckStateMember = ""
        UltraGridColumn5.Header.VisiblePosition = 0
        UltraGridColumn5.Hidden = True
        UltraGridColumn6.Header.VisiblePosition = 1
        UltraGridColumn7.Header.VisiblePosition = 2
        UltraGridColumn7.Width = 70
        UltraGridColumn8.Header.VisiblePosition = 3
        UltraGridColumn8.Width = 80
        UltraGridBand4.Columns.AddRange(New Object() {UltraGridColumn5, UltraGridColumn6, UltraGridColumn7, UltraGridColumn8})
        Me.cmbMasterItem.DisplayLayout.BandsSerializer.Add(UltraGridBand4)
        Me.cmbMasterItem.DisplayLayout.InterBandSpacing = 10
        Me.cmbMasterItem.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbMasterItem.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbMasterItem.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Appearance6.BackColor = System.Drawing.Color.Transparent
        Me.cmbMasterItem.DisplayLayout.Override.CardAreaAppearance = Appearance6
        Me.cmbMasterItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbMasterItem.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance7.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance7.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance7.ForeColor = System.Drawing.Color.White
        Appearance7.TextHAlignAsString = "Left"
        Appearance7.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent
        Me.cmbMasterItem.DisplayLayout.Override.HeaderAppearance = Appearance7
        Me.cmbMasterItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance8.BorderColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbMasterItem.DisplayLayout.Override.RowAppearance = Appearance8
        Appearance9.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance9.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.cmbMasterItem.DisplayLayout.Override.RowSelectorAppearance = Appearance9
        Me.cmbMasterItem.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbMasterItem.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance10.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance10.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance10.ForeColor = System.Drawing.Color.Black
        Me.cmbMasterItem.DisplayLayout.Override.SelectedRowAppearance = Appearance10
        Me.cmbMasterItem.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbMasterItem.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbMasterItem.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbMasterItem.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbMasterItem.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.cmbMasterItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbMasterItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbMasterItem.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbMasterItem.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbMasterItem.LimitToList = True
        Me.cmbMasterItem.Location = New System.Drawing.Point(125, 37)
        Me.cmbMasterItem.MaxDropDownItems = 20
        Me.cmbMasterItem.Name = "cmbMasterItem"
        Me.cmbMasterItem.Size = New System.Drawing.Size(194, 22)
        Me.cmbMasterItem.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(122, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Master Article"
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(7, 7)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(205, 23)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Define Cost Sheet"
        '
        'UltraTabPageControl4
        '
        Me.UltraTabPageControl4.Controls.Add(Me.grdSaved)
        Me.UltraTabPageControl4.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl4.Name = "UltraTabPageControl4"
        Me.UltraTabPageControl4.Size = New System.Drawing.Size(682, 355)
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
        Me.grdSaved.Size = New System.Drawing.Size(682, 355)
        Me.grdSaved.TabIndex = 0
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.toolStripSeparator, Me.btnDelete, Me.ToolStripSeparator1, Me.btnPriceUpdate, Me.btnCostPriceUpdate, Me.ToolStripSeparator2, Me.btnRefresh, Me.ToolStripSeparator3, Me.btnCostSheetDetail, Me.btnPrint})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(684, 25)
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
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnPriceUpdate
        '
        Me.btnPriceUpdate.Image = Global.SimpleAccounts.My.Resources.Resources.document_letter_edit
        Me.btnPriceUpdate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPriceUpdate.Name = "btnPriceUpdate"
        Me.btnPriceUpdate.Size = New System.Drawing.Size(94, 22)
        Me.btnPriceUpdate.Text = "Price Update"
        '
        'btnCostPriceUpdate
        '
        Me.btnCostPriceUpdate.Image = Global.SimpleAccounts.My.Resources.Resources.document_letter_edit
        Me.btnCostPriceUpdate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCostPriceUpdate.Name = "btnCostPriceUpdate"
        Me.btnCostPriceUpdate.Size = New System.Drawing.Size(121, 22)
        Me.btnCostPriceUpdate.Text = "Cost Price Update"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(66, 22)
        Me.btnRefresh.Text = "Refresh"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'btnCostSheetDetail
        '
        Me.btnCostSheetDetail.Image = Global.SimpleAccounts.My.Resources.Resources._1323245115_inventory_categories
        Me.btnCostSheetDetail.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCostSheetDetail.Name = "btnCostSheetDetail"
        Me.btnCostSheetDetail.Size = New System.Drawing.Size(179, 20)
        Me.btnCostSheetDetail.Text = "Cost Sheet Calculation Detail"
        '
        'btnPrint
        '
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(52, 20)
        Me.btnPrint.Text = "&Print"
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Nothing
        Me.UltraTabControl1.Size = New System.Drawing.Size(200, 100)
        Me.UltraTabControl1.TabIndex = 0
        '
        'UltraTabControl2
        '
        Me.UltraTabControl2.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl2.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl2.Controls.Add(Me.UltraTabPageControl4)
        Me.UltraTabControl2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl2.Location = New System.Drawing.Point(0, 25)
        Me.UltraTabControl2.Name = "UltraTabControl2"
        Me.UltraTabControl2.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl2.Size = New System.Drawing.Size(684, 376)
        Me.UltraTabControl2.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl2.TabIndex = 2
        Me.UltraTabControl2.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Cost Sheet"
        UltraTab2.TabPage = Me.UltraTabPageControl4
        UltraTab2.Text = "History"
        Me.UltraTabControl2.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl2.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(682, 355)
        '
        'BackgroundWorker1
        '
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(646, 0)
        Me.CtrlGrdBar1.MyGrid = Me.grdSaved
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(38, 25)
        Me.CtrlGrdBar1.TabIndex = 1
        '
        'pnlHeader
        '
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(682, 38)
        Me.pnlHeader.TabIndex = 16
        '
        'frmCostSheet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(684, 401)
        Me.Controls.Add(Me.UltraTabControl2)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.KeyPreview = True
        Me.Name = "frmCostSheet"
        Me.Text = "Cost Sheet"
        Me.UltraTabPageControl2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.cmbItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdCostSheet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl3.ResumeLayout(False)
        Me.UltraTabPageControl3.PerformLayout()
        CType(Me.grdProductionOverHeads, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl5.ResumeLayout(False)
        Me.UltraTabPageControl5.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.cmbByProduct, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdByProduct, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl6.ResumeLayout(False)
        Me.UltraTabPageControl6.PerformLayout()
        CType(Me.cmbLabourType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdLabourAllocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl1.ResumeLayout(False)
        CType(Me.UltraTabControl3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl3.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.cmbMasterItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl4.ResumeLayout(False)
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTabControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl2.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnPriceUpdate As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnCostPriceUpdate As System.Windows.Forms.ToolStripButton
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabControl2 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents UltraTabPageControl4 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grdCostSheet As Janus.Windows.GridEX.GridEX
    Friend WithEvents grdSaved As Janus.Windows.GridEX.GridEX
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbCategorys As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCostSheetUnit As System.Windows.Forms.ComboBox
    Friend WithEvents btnCopy As System.Windows.Forms.Button
    Friend WithEvents btnAddCostSheet As System.Windows.Forms.Button
    Friend WithEvents txtCostSheet As System.Windows.Forms.TextBox
    Friend WithEvents RdoCode As System.Windows.Forms.RadioButton
    Friend WithEvents cmbItem As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents rdoName As System.Windows.Forms.RadioButton
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbMasterItem As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtSalePrice As System.Windows.Forms.TextBox
    Friend WithEvents txtPurchasePrice As System.Windows.Forms.TextBox
    Friend WithEvents txtPackQty As System.Windows.Forms.TextBox
    Friend WithEvents lblQty As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbDepartment As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents RdoCode1 As System.Windows.Forms.RadioButton
    Friend WithEvents RdoName1 As System.Windows.Forms.RadioButton
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtTaxPercent As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbRemarks As System.Windows.Forms.ComboBox
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnCostSheetDetail As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblSubDepartment As System.Windows.Forms.Label
    Friend WithEvents cmbSubDepartment As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtTotalQty As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtPercentageCon As System.Windows.Forms.TextBox
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents UltraTabControl3 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage2 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl3 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl5 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl6 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents lblAmount As System.Windows.Forms.Label
    Friend WithEvents lblRemarks As System.Windows.Forms.Label
    Friend WithEvents lblAccount As System.Windows.Forms.Label
    Friend WithEvents lblProductionStage As System.Windows.Forms.Label
    Friend WithEvents cmbAccount As System.Windows.Forms.ComboBox
    Friend WithEvents cmbProductionStep As System.Windows.Forms.ComboBox
    Friend WithEvents grdProductionOverHeads As Janus.Windows.GridEX.GridEX
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents btnAddProductionOverHeads As System.Windows.Forms.Button
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents btnByProductAdd As System.Windows.Forms.Button
    Friend WithEvents txtByProductRemarks As System.Windows.Forms.TextBox
    Friend WithEvents txtByProductRate As System.Windows.Forms.TextBox
    Friend WithEvents grdByProduct As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblByProductRate As System.Windows.Forms.Label
    Friend WithEvents lblByProductRemarks As System.Windows.Forms.Label
    Friend WithEvents lblProduct As System.Windows.Forms.Label
    Friend WithEvents cmbByProduct As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents rboByProductName As System.Windows.Forms.RadioButton
    Friend WithEvents rboByProductCode As System.Windows.Forms.RadioButton
    Friend WithEvents txtByProductQty As System.Windows.Forms.TextBox
    Friend WithEvents lblByProductQty As System.Windows.Forms.Label
    Friend WithEvents txtPerUnitRate As System.Windows.Forms.TextBox
    Friend WithEvents cmbStage As System.Windows.Forms.ComboBox
    Friend WithEvents grdLabourAllocation As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblPerUnitRate As System.Windows.Forms.Label
    Friend WithEvents lblByProductLabourType As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents cmbLabourType As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents btnAddLabourAllocation As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SimpleItemDefForm
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(SimpleItemDefForm))
        Dim grdTarget_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.BtnNew = New System.Windows.Forms.ToolStripButton()
        Me.BtnEdit = New System.Windows.Forms.ToolStripButton()
        Me.BtnSave = New System.Windows.Forms.ToolStripButton()
        Me.BtnDelete = New System.Windows.Forms.ToolStripButton()
        Me.BtnPrint = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.ArticleId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ArticleGroupId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ArticleTypeName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ArticleCode = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ArticleDescription = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ArticleGroupName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Gender = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.LPO = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PackQty = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PurchasePrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SalePrice = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ArticleColorName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ArticleSizeName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StockLevel = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StockLevelOpt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StockLevelMaximum = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SortOrder = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Active = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ArticleTypeId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ArticleColorId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SizeRangeId = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.AccountID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.uitxtItemName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.uitxtItemCode = New System.Windows.Forms.TextBox()
        Me.uitxtSalePrice = New System.Windows.Forms.TextBox()
        Me.uitxtPrice = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Type = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.uichkActive = New System.Windows.Forms.CheckBox()
        Me.label9 = New System.Windows.Forms.Label()
        Me.uitxtSortOrder = New System.Windows.Forms.TextBox()
        Me.label8 = New System.Windows.Forms.Label()
        Me.uitxtPackQty = New System.Windows.Forms.TextBox()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.uitxtStockLevel = New System.Windows.Forms.TextBox()
        Me.uicmbCategory = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.uicmbType = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.uicmbColor = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.uicmbSize = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.uitxtStockLevelMaximum = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.uitxtStockLevelOptimal = New System.Windows.Forms.TextBox()
        Me.uiCmbGender = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.uicmbDistributor = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtOldPurchasePrice = New System.Windows.Forms.TextBox()
        Me.txtOldSalePrice = New System.Windows.Forms.TextBox()
        Me.txtAccountID = New System.Windows.Forms.TextBox()
        Me.grdTarget = New Janus.Windows.GridEX.GridEX()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.uicmbCategory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.uicmbType, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.uicmbColor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.uicmbSize, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.uiCmbGender, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.uicmbDistributor, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdTarget, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BtnNew, Me.BtnEdit, Me.BtnSave, Me.BtnDelete, Me.BtnPrint, Me.toolStripSeparator1, Me.HelpToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(834, 25)
        Me.ToolStrip1.TabIndex = 34
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
        Me.BtnPrint.Image = CType(resources.GetObject("BtnPrint.Image"), System.Drawing.Image)
        Me.BtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(52, 22)
        Me.BtnPrint.Text = "&Print"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 25)
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
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(14, 92)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Category"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ArticleId, Me.ArticleGroupId, Me.ArticleTypeName, Me.ArticleCode, Me.ArticleDescription, Me.ArticleGroupName, Me.Gender, Me.LPO, Me.PackQty, Me.PurchasePrice, Me.SalePrice, Me.ArticleColorName, Me.ArticleSizeName, Me.StockLevel, Me.StockLevelOpt, Me.StockLevelMaximum, Me.SortOrder, Me.Active, Me.ArticleTypeId, Me.ArticleColorId, Me.SizeRangeId, Me.AccountID})
        Me.DataGridView1.Location = New System.Drawing.Point(0, 296)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.Size = New System.Drawing.Size(834, 265)
        Me.DataGridView1.TabIndex = 33
        '
        'ArticleId
        '
        Me.ArticleId.DataPropertyName = "ArticleId"
        Me.ArticleId.HeaderText = "ArticleId"
        Me.ArticleId.Name = "ArticleId"
        Me.ArticleId.ReadOnly = True
        Me.ArticleId.Visible = False
        '
        'ArticleGroupId
        '
        Me.ArticleGroupId.DataPropertyName = "ArticleGroupId"
        Me.ArticleGroupId.HeaderText = "ArticleGroupId"
        Me.ArticleGroupId.Name = "ArticleGroupId"
        Me.ArticleGroupId.ReadOnly = True
        Me.ArticleGroupId.Visible = False
        '
        'ArticleTypeName
        '
        Me.ArticleTypeName.DataPropertyName = "ArticleTypeName"
        Me.ArticleTypeName.HeaderText = "Type"
        Me.ArticleTypeName.Name = "ArticleTypeName"
        Me.ArticleTypeName.ReadOnly = True
        '
        'ArticleCode
        '
        Me.ArticleCode.DataPropertyName = "ArticleCode"
        Me.ArticleCode.HeaderText = "Code"
        Me.ArticleCode.Name = "ArticleCode"
        Me.ArticleCode.ReadOnly = True
        Me.ArticleCode.Width = 80
        '
        'ArticleDescription
        '
        Me.ArticleDescription.DataPropertyName = "ArticleDescription"
        Me.ArticleDescription.HeaderText = "Item Name"
        Me.ArticleDescription.Name = "ArticleDescription"
        Me.ArticleDescription.ReadOnly = True
        Me.ArticleDescription.Width = 160
        '
        'ArticleGroupName
        '
        Me.ArticleGroupName.DataPropertyName = "ArticleGroupName"
        Me.ArticleGroupName.HeaderText = "Category"
        Me.ArticleGroupName.Name = "ArticleGroupName"
        Me.ArticleGroupName.ReadOnly = True
        Me.ArticleGroupName.Visible = False
        Me.ArticleGroupName.Width = 125
        '
        'Gender
        '
        Me.Gender.DataPropertyName = "ArticleGenderName"
        Me.Gender.HeaderText = "Gender"
        Me.Gender.Name = "Gender"
        Me.Gender.ReadOnly = True
        '
        'LPO
        '
        Me.LPO.DataPropertyName = "LPO"
        Me.LPO.HeaderText = "LPO"
        Me.LPO.Name = "LPO"
        Me.LPO.ReadOnly = True
        Me.LPO.Width = 200
        '
        'PackQty
        '
        Me.PackQty.DataPropertyName = "PackQty"
        Me.PackQty.HeaderText = "Pack Qty"
        Me.PackQty.Name = "PackQty"
        Me.PackQty.ReadOnly = True
        Me.PackQty.Width = 80
        '
        'PurchasePrice
        '
        Me.PurchasePrice.DataPropertyName = "PurchasePrice"
        Me.PurchasePrice.HeaderText = "Pur Price"
        Me.PurchasePrice.Name = "PurchasePrice"
        Me.PurchasePrice.ReadOnly = True
        Me.PurchasePrice.Width = 80
        '
        'SalePrice
        '
        Me.SalePrice.DataPropertyName = "SalePrice"
        Me.SalePrice.HeaderText = "Sale Price"
        Me.SalePrice.Name = "SalePrice"
        Me.SalePrice.ReadOnly = True
        Me.SalePrice.Width = 80
        '
        'ArticleColorName
        '
        Me.ArticleColorName.DataPropertyName = "ArticleColorName"
        Me.ArticleColorName.HeaderText = "Color"
        Me.ArticleColorName.Name = "ArticleColorName"
        Me.ArticleColorName.ReadOnly = True
        '
        'ArticleSizeName
        '
        Me.ArticleSizeName.DataPropertyName = "ArticleSizeName"
        Me.ArticleSizeName.HeaderText = "Size"
        Me.ArticleSizeName.Name = "ArticleSizeName"
        Me.ArticleSizeName.ReadOnly = True
        '
        'StockLevel
        '
        Me.StockLevel.DataPropertyName = "StockLevel"
        Me.StockLevel.HeaderText = "Stk Min"
        Me.StockLevel.Name = "StockLevel"
        Me.StockLevel.ReadOnly = True
        Me.StockLevel.Width = 80
        '
        'StockLevelOpt
        '
        Me.StockLevelOpt.DataPropertyName = "StockLevelOpt"
        Me.StockLevelOpt.HeaderText = "Stk Opt"
        Me.StockLevelOpt.Name = "StockLevelOpt"
        Me.StockLevelOpt.ReadOnly = True
        Me.StockLevelOpt.Width = 80
        '
        'StockLevelMaximum
        '
        Me.StockLevelMaximum.DataPropertyName = "StockLevelMax"
        Me.StockLevelMaximum.HeaderText = "Stk Max"
        Me.StockLevelMaximum.Name = "StockLevelMaximum"
        Me.StockLevelMaximum.ReadOnly = True
        Me.StockLevelMaximum.Width = 80
        '
        'SortOrder
        '
        Me.SortOrder.DataPropertyName = "SortOrder"
        Me.SortOrder.HeaderText = "Sort"
        Me.SortOrder.Name = "SortOrder"
        Me.SortOrder.ReadOnly = True
        Me.SortOrder.Width = 60
        '
        'Active
        '
        Me.Active.DataPropertyName = "Active"
        Me.Active.HeaderText = "Active"
        Me.Active.Name = "Active"
        Me.Active.ReadOnly = True
        Me.Active.Width = 50
        '
        'ArticleTypeId
        '
        Me.ArticleTypeId.DataPropertyName = "ArticleTypeId"
        Me.ArticleTypeId.HeaderText = "ArticleTypeId"
        Me.ArticleTypeId.Name = "ArticleTypeId"
        Me.ArticleTypeId.ReadOnly = True
        Me.ArticleTypeId.Visible = False
        '
        'ArticleColorId
        '
        Me.ArticleColorId.DataPropertyName = "ArticleColorId"
        Me.ArticleColorId.HeaderText = "ArticleColorId"
        Me.ArticleColorId.Name = "ArticleColorId"
        Me.ArticleColorId.ReadOnly = True
        Me.ArticleColorId.Visible = False
        '
        'SizeRangeId
        '
        Me.SizeRangeId.DataPropertyName = "SizeRangeId"
        Me.SizeRangeId.HeaderText = "SizeRangeId"
        Me.SizeRangeId.Name = "SizeRangeId"
        Me.SizeRangeId.ReadOnly = True
        Me.SizeRangeId.Visible = False
        '
        'AccountID
        '
        Me.AccountID.DataPropertyName = "AccountID"
        Me.AccountID.HeaderText = "Account ID"
        Me.AccountID.Name = "AccountID"
        Me.AccountID.ReadOnly = True
        Me.AccountID.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 172)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Item Name"
        '
        'uitxtItemName
        '
        Me.uitxtItemName.Location = New System.Drawing.Point(132, 171)
        Me.uitxtItemName.MaxLength = 50
        Me.uitxtItemName.Name = "uitxtItemName"
        Me.uitxtItemName.Size = New System.Drawing.Size(231, 20)
        Me.uitxtItemName.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(14, 146)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(55, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Item Code"
        '
        'uitxtItemCode
        '
        Me.uitxtItemCode.Location = New System.Drawing.Point(132, 145)
        Me.uitxtItemCode.MaxLength = 10
        Me.uitxtItemCode.Name = "uitxtItemCode"
        Me.uitxtItemCode.Size = New System.Drawing.Size(231, 20)
        Me.uitxtItemCode.TabIndex = 8
        '
        'uitxtSalePrice
        '
        Me.uitxtSalePrice.Location = New System.Drawing.Point(632, 184)
        Me.uitxtSalePrice.MaxLength = 50
        Me.uitxtSalePrice.Name = "uitxtSalePrice"
        Me.uitxtSalePrice.Size = New System.Drawing.Size(79, 20)
        Me.uitxtSalePrice.TabIndex = 26
        Me.uitxtSalePrice.Text = "0"
        '
        'uitxtPrice
        '
        Me.uitxtPrice.Location = New System.Drawing.Point(469, 184)
        Me.uitxtPrice.MaxLength = 50
        Me.uitxtPrice.Name = "uitxtPrice"
        Me.uitxtPrice.Size = New System.Drawing.Size(79, 20)
        Me.uitxtPrice.TabIndex = 24
        Me.uitxtPrice.Text = "0"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(559, 188)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 13)
        Me.Label4.TabIndex = 25
        Me.Label4.Text = "Sale Price"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(373, 188)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(82, 13)
        Me.Label5.TabIndex = 23
        Me.Label5.Text = "Purchase Price:"
        '
        'Type
        '
        Me.Type.AutoSize = True
        Me.Type.Location = New System.Drawing.Point(14, 198)
        Me.Type.Name = "Type"
        Me.Type.Size = New System.Drawing.Size(65, 13)
        Me.Type.TabIndex = 11
        Me.Type.Text = "Combination"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(14, 228)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(27, 13)
        Me.Label7.TabIndex = 14
        Me.Label7.Text = "Size"
        '
        'uichkActive
        '
        Me.uichkActive.AutoSize = True
        Me.uichkActive.Checked = True
        Me.uichkActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.uichkActive.Location = New System.Drawing.Point(376, 241)
        Me.uichkActive.Name = "uichkActive"
        Me.uichkActive.Size = New System.Drawing.Size(56, 17)
        Me.uichkActive.TabIndex = 31
        Me.uichkActive.Text = "Active"
        Me.uichkActive.UseVisualStyleBackColor = True
        '
        'label9
        '
        Me.label9.AutoSize = True
        Me.label9.Location = New System.Drawing.Point(559, 213)
        Me.label9.Name = "label9"
        Me.label9.Size = New System.Drawing.Size(55, 13)
        Me.label9.TabIndex = 29
        Me.label9.Text = "Sort Order"
        '
        'uitxtSortOrder
        '
        Me.uitxtSortOrder.Location = New System.Drawing.Point(632, 210)
        Me.uitxtSortOrder.MaxLength = 50
        Me.uitxtSortOrder.Name = "uitxtSortOrder"
        Me.uitxtSortOrder.Size = New System.Drawing.Size(79, 20)
        Me.uitxtSortOrder.TabIndex = 30
        Me.uitxtSortOrder.Text = "1"
        '
        'label8
        '
        Me.label8.AutoSize = True
        Me.label8.Location = New System.Drawing.Point(373, 214)
        Me.label8.Name = "label8"
        Me.label8.Size = New System.Drawing.Size(51, 13)
        Me.label8.TabIndex = 27
        Me.label8.Text = "Pack Qty"
        '
        'uitxtPackQty
        '
        Me.uitxtPackQty.Location = New System.Drawing.Point(469, 210)
        Me.uitxtPackQty.MaxLength = 50
        Me.uitxtPackQty.Name = "uitxtPackQty"
        Me.uitxtPackQty.Size = New System.Drawing.Size(79, 20)
        Me.uitxtPackQty.TabIndex = 28
        Me.uitxtPackQty.Text = "1"
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(8, 10)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(230, 23)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "New Inventory Item"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label6.ForeColor = System.Drawing.Color.Blue
        Me.Label6.Location = New System.Drawing.Point(76, 92)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(50, 13)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "(Refresh)"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label11.ForeColor = System.Drawing.Color.Blue
        Me.Label11.Location = New System.Drawing.Point(76, 199)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(50, 13)
        Me.Label11.TabIndex = 12
        Me.Label11.Text = "(Refresh)"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label12.ForeColor = System.Drawing.Color.Blue
        Me.Label12.Location = New System.Drawing.Point(76, 228)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(50, 13)
        Me.Label12.TabIndex = 15
        Me.Label12.Text = "(Refresh)"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label13.ForeColor = System.Drawing.Color.Blue
        Me.Label13.Location = New System.Drawing.Point(76, 119)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(50, 13)
        Me.Label13.TabIndex = 5
        Me.Label13.Text = "(Refresh)"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(14, 119)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(31, 13)
        Me.Label14.TabIndex = 4
        Me.Label14.Text = "Type"
        '
        'uitxtStockLevel
        '
        Me.uitxtStockLevel.Location = New System.Drawing.Point(9, 28)
        Me.uitxtStockLevel.MaxLength = 50
        Me.uitxtStockLevel.Name = "uitxtStockLevel"
        Me.uitxtStockLevel.Size = New System.Drawing.Size(98, 20)
        Me.uitxtStockLevel.TabIndex = 1
        Me.uitxtStockLevel.Text = "0"
        Me.uitxtStockLevel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'uicmbCategory
        '
        Me.uicmbCategory.CheckedListSettings.CheckStateMember = ""
        Me.uicmbCategory.DisplayLayout.InterBandSpacing = 10
        Me.uicmbCategory.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.uicmbCategory.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.uicmbCategory.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.uicmbCategory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.uicmbCategory.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Me.uicmbCategory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.uicmbCategory.DisplayLayout.Override.RowSelectorWidth = 12
        Me.uicmbCategory.DisplayLayout.Override.RowSpacingBefore = 2
        Me.uicmbCategory.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.uicmbCategory.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.uicmbCategory.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.uicmbCategory.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.uicmbCategory.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.uicmbCategory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.uicmbCategory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.uicmbCategory.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.uicmbCategory.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.uicmbCategory.LimitToList = True
        Me.uicmbCategory.Location = New System.Drawing.Point(132, 89)
        Me.uicmbCategory.Name = "uicmbCategory"
        Me.uicmbCategory.Size = New System.Drawing.Size(231, 22)
        Me.uicmbCategory.TabIndex = 3
        '
        'uicmbType
        '
        Me.uicmbType.CheckedListSettings.CheckStateMember = ""
        Me.uicmbType.DisplayLayout.InterBandSpacing = 10
        Me.uicmbType.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.uicmbType.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.uicmbType.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.uicmbType.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.uicmbType.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Me.uicmbType.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.uicmbType.DisplayLayout.Override.RowSelectorWidth = 12
        Me.uicmbType.DisplayLayout.Override.RowSpacingBefore = 2
        Me.uicmbType.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.uicmbType.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.uicmbType.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.uicmbType.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.uicmbType.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.uicmbType.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.uicmbType.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.uicmbType.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.uicmbType.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.uicmbType.LimitToList = True
        Me.uicmbType.Location = New System.Drawing.Point(132, 117)
        Me.uicmbType.Name = "uicmbType"
        Me.uicmbType.Size = New System.Drawing.Size(231, 22)
        Me.uicmbType.TabIndex = 6
        '
        'uicmbColor
        '
        Me.uicmbColor.CheckedListSettings.CheckStateMember = ""
        Me.uicmbColor.DisplayLayout.InterBandSpacing = 10
        Me.uicmbColor.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.uicmbColor.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.uicmbColor.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.uicmbColor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.uicmbColor.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Me.uicmbColor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.uicmbColor.DisplayLayout.Override.RowSelectorWidth = 12
        Me.uicmbColor.DisplayLayout.Override.RowSpacingBefore = 2
        Me.uicmbColor.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.uicmbColor.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.uicmbColor.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.uicmbColor.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.uicmbColor.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.uicmbColor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.uicmbColor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.uicmbColor.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.uicmbColor.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.uicmbColor.LimitToList = True
        Me.uicmbColor.Location = New System.Drawing.Point(132, 197)
        Me.uicmbColor.Name = "uicmbColor"
        Me.uicmbColor.Size = New System.Drawing.Size(231, 22)
        Me.uicmbColor.TabIndex = 13
        '
        'uicmbSize
        '
        Me.uicmbSize.CheckedListSettings.CheckStateMember = ""
        Me.uicmbSize.DisplayLayout.InterBandSpacing = 10
        Me.uicmbSize.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.uicmbSize.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.uicmbSize.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.uicmbSize.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.uicmbSize.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Me.uicmbSize.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.uicmbSize.DisplayLayout.Override.RowSelectorWidth = 12
        Me.uicmbSize.DisplayLayout.Override.RowSpacingBefore = 2
        Me.uicmbSize.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.uicmbSize.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.uicmbSize.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.uicmbSize.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.uicmbSize.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.uicmbSize.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.uicmbSize.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.uicmbSize.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.uicmbSize.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.uicmbSize.LimitToList = True
        Me.uicmbSize.Location = New System.Drawing.Point(132, 225)
        Me.uicmbSize.Name = "uicmbSize"
        Me.uicmbSize.Size = New System.Drawing.Size(231, 22)
        Me.uicmbSize.TabIndex = 16
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.uitxtStockLevelMaximum)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.uitxtStockLevelOptimal)
        Me.GroupBox1.Controls.Add(Me.uitxtStockLevel)
        Me.GroupBox1.Location = New System.Drawing.Point(376, 119)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(335, 59)
        Me.GroupBox1.TabIndex = 22
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
        Me.uitxtStockLevelMaximum.Size = New System.Drawing.Size(98, 20)
        Me.uitxtStockLevelMaximum.TabIndex = 5
        Me.uitxtStockLevelMaximum.Text = "0"
        Me.uitxtStockLevelMaximum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
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
        Me.uitxtStockLevelOptimal.Size = New System.Drawing.Size(98, 20)
        Me.uitxtStockLevelOptimal.TabIndex = 3
        Me.uitxtStockLevelOptimal.Text = "0"
        Me.uitxtStockLevelOptimal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'uiCmbGender
        '
        Me.uiCmbGender.CheckedListSettings.CheckStateMember = ""
        Me.uiCmbGender.DisplayLayout.InterBandSpacing = 10
        Me.uiCmbGender.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.uiCmbGender.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.uiCmbGender.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.uiCmbGender.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.uiCmbGender.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Me.uiCmbGender.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.uiCmbGender.DisplayLayout.Override.RowSelectorWidth = 12
        Me.uiCmbGender.DisplayLayout.Override.RowSpacingBefore = 2
        Me.uiCmbGender.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.uiCmbGender.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.uiCmbGender.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.uiCmbGender.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.uiCmbGender.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.uiCmbGender.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.uiCmbGender.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.uiCmbGender.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.uiCmbGender.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.uiCmbGender.LimitToList = True
        Me.uiCmbGender.Location = New System.Drawing.Point(132, 253)
        Me.uiCmbGender.Name = "uiCmbGender"
        Me.uiCmbGender.Size = New System.Drawing.Size(231, 22)
        Me.uiCmbGender.TabIndex = 19
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label15.ForeColor = System.Drawing.Color.Blue
        Me.Label15.Location = New System.Drawing.Point(76, 256)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(50, 13)
        Me.Label15.TabIndex = 18
        Me.Label15.Text = "(Refresh)"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(14, 256)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(42, 13)
        Me.Label19.TabIndex = 17
        Me.Label19.Text = "Gender"
        '
        'uicmbDistributor
        '
        Me.uicmbDistributor.CheckedListSettings.CheckStateMember = ""
        Me.uicmbDistributor.DisplayLayout.InterBandSpacing = 10
        Me.uicmbDistributor.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.uicmbDistributor.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.uicmbDistributor.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.uicmbDistributor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.uicmbDistributor.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Me.uicmbDistributor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.uicmbDistributor.DisplayLayout.Override.RowSelectorWidth = 12
        Me.uicmbDistributor.DisplayLayout.Override.RowSpacingBefore = 2
        Me.uicmbDistributor.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.uicmbDistributor.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.uicmbDistributor.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.uicmbDistributor.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.uicmbDistributor.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.uicmbDistributor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.uicmbDistributor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.uicmbDistributor.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.uicmbDistributor.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.uicmbDistributor.LimitToList = True
        Me.uicmbDistributor.Location = New System.Drawing.Point(462, 89)
        Me.uicmbDistributor.Name = "uicmbDistributor"
        Me.uicmbDistributor.Size = New System.Drawing.Size(249, 22)
        Me.uicmbDistributor.TabIndex = 21
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Location = New System.Drawing.Point(373, 92)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(80, 13)
        Me.Label20.TabIndex = 20
        Me.Label20.Text = "Distributor/LPO"
        '
        'txtOldPurchasePrice
        '
        Me.txtOldPurchasePrice.Location = New System.Drawing.Point(469, 236)
        Me.txtOldPurchasePrice.MaxLength = 50
        Me.txtOldPurchasePrice.Name = "txtOldPurchasePrice"
        Me.txtOldPurchasePrice.Size = New System.Drawing.Size(79, 20)
        Me.txtOldPurchasePrice.TabIndex = 32
        Me.txtOldPurchasePrice.Text = "0"
        Me.txtOldPurchasePrice.Visible = False
        '
        'txtOldSalePrice
        '
        Me.txtOldSalePrice.Location = New System.Drawing.Point(632, 236)
        Me.txtOldSalePrice.MaxLength = 50
        Me.txtOldSalePrice.Name = "txtOldSalePrice"
        Me.txtOldSalePrice.Size = New System.Drawing.Size(79, 20)
        Me.txtOldSalePrice.TabIndex = 38
        Me.txtOldSalePrice.Text = "0"
        Me.txtOldSalePrice.Visible = False
        '
        'txtAccountID
        '
        Me.txtAccountID.Location = New System.Drawing.Point(717, 92)
        Me.txtAccountID.MaxLength = 50
        Me.txtAccountID.Name = "txtAccountID"
        Me.txtAccountID.Size = New System.Drawing.Size(79, 20)
        Me.txtAccountID.TabIndex = 39
        Me.txtAccountID.Text = "0"
        Me.txtAccountID.Visible = False
        '
        'grdTarget
        '
        Me.grdTarget.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdTarget.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdTarget_DesignTimeLayout.LayoutString = resources.GetString("grdTarget_DesignTimeLayout.LayoutString")
        Me.grdTarget.DesignTimeLayout = grdTarget_DesignTimeLayout
        Me.grdTarget.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdTarget.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdTarget.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdTarget.GroupByBoxVisible = False
        Me.grdTarget.Location = New System.Drawing.Point(158, 92)
        Me.grdTarget.Name = "grdTarget"
        Me.grdTarget.RecordNavigator = True
        Me.grdTarget.Size = New System.Drawing.Size(519, 208)
        Me.grdTarget.TabIndex = 96
        Me.grdTarget.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(234, 261)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 97
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'pnlHeader
        '
        Me.pnlHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Location = New System.Drawing.Point(0, 29)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(834, 45)
        Me.pnlHeader.TabIndex = 98
        '
        'SimpleItemDefForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(834, 562)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.txtAccountID)
        Me.Controls.Add(Me.txtOldSalePrice)
        Me.Controls.Add(Me.txtOldPurchasePrice)
        Me.Controls.Add(Me.uicmbDistributor)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.uiCmbGender)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.uicmbSize)
        Me.Controls.Add(Me.uicmbColor)
        Me.Controls.Add(Me.uicmbType)
        Me.Controls.Add(Me.uicmbCategory)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.uichkActive)
        Me.Controls.Add(Me.label9)
        Me.Controls.Add(Me.uitxtSortOrder)
        Me.Controls.Add(Me.label8)
        Me.Controls.Add(Me.uitxtPackQty)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Type)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.uitxtPrice)
        Me.Controls.Add(Me.uitxtSalePrice)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.uitxtItemName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.uitxtItemCode)
        Me.Controls.Add(Me.grdTarget)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "SimpleItemDefForm"
        Me.Text = "New Inventory Item"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.uicmbCategory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.uicmbType, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.uicmbColor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.uicmbSize, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.uiCmbGender, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.uicmbDistributor, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdTarget, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents BtnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents uitxtItemName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents uitxtItemCode As System.Windows.Forms.TextBox
    Friend WithEvents uitxtSalePrice As System.Windows.Forms.TextBox
    Friend WithEvents uitxtPrice As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Type As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents uichkActive As System.Windows.Forms.CheckBox
    Friend WithEvents label9 As System.Windows.Forms.Label
    Friend WithEvents uitxtSortOrder As System.Windows.Forms.TextBox
    Friend WithEvents label8 As System.Windows.Forms.Label
    Friend WithEvents uitxtPackQty As System.Windows.Forms.TextBox
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents uitxtStockLevel As System.Windows.Forms.TextBox
    Friend WithEvents BtnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents uicmbCategory As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents uicmbType As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents uicmbColor As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents uicmbSize As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents uitxtStockLevelMaximum As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents uitxtStockLevelOptimal As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents uiCmbGender As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents uicmbDistributor As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtOldPurchasePrice As System.Windows.Forms.TextBox
    Friend WithEvents txtOldSalePrice As System.Windows.Forms.TextBox
    Friend WithEvents ArticleId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ArticleGroupId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ArticleTypeName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ArticleCode As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ArticleDescription As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ArticleGroupName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Gender As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LPO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PackQty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PurchasePrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SalePrice As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ArticleColorName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ArticleSizeName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StockLevel As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StockLevelOpt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StockLevelMaximum As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SortOrder As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Active As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents ArticleTypeId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ArticleColorId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SizeRangeId As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents AccountID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents txtAccountID As System.Windows.Forms.TextBox
    Friend WithEvents grdTarget As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

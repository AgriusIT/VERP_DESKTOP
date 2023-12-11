<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptGridItemSalesHistory
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
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RptGridItemSalesHistory))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtByName = New System.Windows.Forms.RadioButton()
        Me.rbtByCode = New System.Windows.Forms.RadioButton()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtsearch = New System.Windows.Forms.TextBox()
        Me.lstItems = New SimpleAccounts.uiListControl()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.DtpTo = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.DtpFrom = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.BtnGenerate = New System.Windows.Forms.Button()
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnSaveLayout = New System.Windows.Forms.ToolStripButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rbtnImports = New System.Windows.Forms.RadioButton()
        Me.rdoStockReceiving = New System.Windows.Forms.RadioButton()
        Me.rdoProduction = New System.Windows.Forms.RadioButton()
        Me.rdoStoreIssueance = New System.Windows.Forms.RadioButton()
        Me.rdoDispatch = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.RBtnSalesReturn = New System.Windows.Forms.RadioButton()
        Me.RBtnPurchase = New System.Windows.Forms.RadioButton()
        Me.RBtnSales = New System.Windows.Forms.RadioButton()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtByName)
        Me.GroupBox1.Controls.Add(Me.rbtByCode)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtsearch)
        Me.GroupBox1.Controls.Add(Me.lstItems)
        Me.GroupBox1.Location = New System.Drawing.Point(526, 115)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(500, 380)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Item List"
        '
        'rbtByName
        '
        Me.rbtByName.AutoSize = True
        Me.rbtByName.Checked = True
        Me.rbtByName.Location = New System.Drawing.Point(186, 75)
        Me.rbtByName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtByName.Name = "rbtByName"
        Me.rbtByName.Size = New System.Drawing.Size(98, 24)
        Me.rbtByName.TabIndex = 3
        Me.rbtByName.TabStop = True
        Me.rbtByName.Text = "By Name"
        Me.rbtByName.UseVisualStyleBackColor = True
        '
        'rbtByCode
        '
        Me.rbtByCode.AutoSize = True
        Me.rbtByCode.Location = New System.Drawing.Point(80, 75)
        Me.rbtByCode.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtByCode.Name = "rbtByCode"
        Me.rbtByCode.Size = New System.Drawing.Size(94, 24)
        Me.rbtByCode.TabIndex = 2
        Me.rbtByCode.Text = "By Code"
        Me.rbtByCode.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 40)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 20)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Search"
        '
        'txtsearch
        '
        Me.txtsearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtsearch.Location = New System.Drawing.Point(80, 35)
        Me.txtsearch.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtsearch.Name = "txtsearch"
        Me.txtsearch.Size = New System.Drawing.Size(409, 26)
        Me.txtsearch.TabIndex = 1
        '
        'lstItems
        '
        Me.lstItems.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstItems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstItems.AutoScroll = True
        Me.lstItems.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstItems.BackColor = System.Drawing.Color.Transparent
        Me.lstItems.disableWhenChecked = False
        Me.lstItems.HeadingLabelName = Nothing
        Me.lstItems.HeadingText = "Items"
        Me.lstItems.Location = New System.Drawing.Point(9, 122)
        Me.lstItems.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstItems.Name = "lstItems"
        Me.lstItems.ShowAddNewButton = False
        Me.lstItems.ShowInverse = True
        Me.lstItems.ShowMagnifierButton = False
        Me.lstItems.ShowNoCheck = False
        Me.lstItems.ShowResetAllButton = False
        Me.lstItems.ShowSelectall = True
        Me.lstItems.Size = New System.Drawing.Size(482, 246)
        Me.lstItems.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.lstItems, "Items List")
        Me.lstItems.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.cmbPeriod)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.DtpTo)
        Me.GroupBox2.Controls.Add(Me.DtpFrom)
        Me.GroupBox2.Location = New System.Drawing.Point(18, 115)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Size = New System.Drawing.Size(500, 331)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Date Rnage"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 40)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(54, 20)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Period"
        '
        'cmbPeriod
        '
        Me.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPeriod.FormattingEnabled = True
        Me.cmbPeriod.Items.AddRange(New Object() {"Today", "Yesterday", "Current Week", "Current Month", "Current Year"})
        Me.cmbPeriod.Location = New System.Drawing.Point(78, 35)
        Me.cmbPeriod.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(198, 28)
        Me.cmbPeriod.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbPeriod, "Select Period And Gets Date Range")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 122)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 20)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "To"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 82)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 20)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "From"
        '
        'DtpTo
        '
        Me.DtpTo.CustomFormat = "dd/MMM/yyyy"
        Me.DtpTo.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.DtpTo.DropDownCalendar.Name = ""
        Me.DtpTo.Location = New System.Drawing.Point(78, 115)
        Me.DtpTo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DtpTo.Name = "DtpTo"
        Me.DtpTo.Size = New System.Drawing.Size(200, 26)
        Me.DtpTo.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.DtpTo, "To Date")
        '
        'DtpFrom
        '
        Me.DtpFrom.CustomFormat = "dd/MMM/yyyy"
        Me.DtpFrom.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.DtpFrom.DropDownCalendar.Name = ""
        Me.DtpFrom.Location = New System.Drawing.Point(78, 75)
        Me.DtpFrom.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DtpFrom.Name = "DtpFrom"
        Me.DtpFrom.Size = New System.Drawing.Size(200, 26)
        Me.DtpFrom.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.DtpFrom, "From Date")
        '
        'BtnGenerate
        '
        Me.BtnGenerate.Location = New System.Drawing.Point(362, 457)
        Me.BtnGenerate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.BtnGenerate.Name = "BtnGenerate"
        Me.BtnGenerate.Size = New System.Drawing.Size(156, 38)
        Me.BtnGenerate.TabIndex = 6
        Me.BtnGenerate.Text = "Generate "
        Me.ToolTip1.SetToolTip(Me.BtnGenerate, "Generate Report")
        Me.BtnGenerate.UseVisualStyleBackColor = True
        '
        'GridEX1
        '
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        GridEX1_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /><TotalRow>True</TotalRow></RootTab" & _
    "le></GridEXLayoutData>"
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX1.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GridEX1.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.GridEX1.Location = New System.Drawing.Point(3, 505)
        Me.GridEX1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.Size = New System.Drawing.Size(1038, 383)
        Me.GridEX1.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.GridEX1, "Items Detail History")
        Me.GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(12, 15)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(278, 36)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Item Detail History"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh, Me.btnSaveLayout})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1044, 38)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 35)
        Me.btnRefresh.Text = "Refresh"
        '
        'btnSaveLayout
        '
        Me.btnSaveLayout.Image = CType(resources.GetObject("btnSaveLayout.Image"), System.Drawing.Image)
        Me.btnSaveLayout.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSaveLayout.Name = "btnSaveLayout"
        Me.btnSaveLayout.Size = New System.Drawing.Size(190, 35)
        Me.btnSaveLayout.Text = "Save Group Layout"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rbtnImports)
        Me.Panel1.Controls.Add(Me.rdoStockReceiving)
        Me.Panel1.Controls.Add(Me.rdoProduction)
        Me.Panel1.Controls.Add(Me.rdoStoreIssueance)
        Me.Panel1.Controls.Add(Me.rdoDispatch)
        Me.Panel1.Controls.Add(Me.RadioButton1)
        Me.Panel1.Controls.Add(Me.RBtnSalesReturn)
        Me.Panel1.Controls.Add(Me.RBtnPurchase)
        Me.Panel1.Controls.Add(Me.RBtnSales)
        Me.Panel1.Location = New System.Drawing.Point(32, 275)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(477, 160)
        Me.Panel1.TabIndex = 5
        '
        'rbtnImports
        '
        Me.rbtnImports.AutoSize = True
        Me.rbtnImports.Location = New System.Drawing.Point(369, 9)
        Me.rbtnImports.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtnImports.Name = "rbtnImports"
        Me.rbtnImports.Size = New System.Drawing.Size(88, 24)
        Me.rbtnImports.TabIndex = 8
        Me.rbtnImports.TabStop = True
        Me.rbtnImports.Text = "Imports"
        Me.rbtnImports.UseVisualStyleBackColor = True
        '
        'rdoStockReceiving
        '
        Me.rdoStockReceiving.AutoSize = True
        Me.rdoStockReceiving.Location = New System.Drawing.Point(201, 115)
        Me.rdoStockReceiving.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rdoStockReceiving.Name = "rdoStockReceiving"
        Me.rdoStockReceiving.Size = New System.Drawing.Size(148, 24)
        Me.rdoStockReceiving.TabIndex = 7
        Me.rdoStockReceiving.Text = "Stock Receiving"
        Me.ToolTip1.SetToolTip(Me.rdoStockReceiving, "View Item Detail History By Stock Receiving")
        Me.rdoStockReceiving.UseVisualStyleBackColor = True
        '
        'rdoProduction
        '
        Me.rdoProduction.AutoSize = True
        Me.rdoProduction.Location = New System.Drawing.Point(4, 115)
        Me.rdoProduction.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rdoProduction.Name = "rdoProduction"
        Me.rdoProduction.Size = New System.Drawing.Size(153, 24)
        Me.rdoProduction.TabIndex = 3
        Me.rdoProduction.TabStop = True
        Me.rdoProduction.Text = "Production Store"
        Me.ToolTip1.SetToolTip(Me.rdoProduction, "View Item Detail History By Production")
        Me.rdoProduction.UseVisualStyleBackColor = True
        '
        'rdoStoreIssueance
        '
        Me.rdoStoreIssueance.AutoSize = True
        Me.rdoStoreIssueance.Location = New System.Drawing.Point(201, 80)
        Me.rdoStoreIssueance.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rdoStoreIssueance.Name = "rdoStoreIssueance"
        Me.rdoStoreIssueance.Size = New System.Drawing.Size(142, 24)
        Me.rdoStoreIssueance.TabIndex = 6
        Me.rdoStoreIssueance.Text = "Store Issuance"
        Me.ToolTip1.SetToolTip(Me.rdoStoreIssueance, "View Item Detail History By Store Issuence")
        Me.rdoStoreIssueance.UseVisualStyleBackColor = True
        '
        'rdoDispatch
        '
        Me.rdoDispatch.AutoSize = True
        Me.rdoDispatch.Location = New System.Drawing.Point(4, 80)
        Me.rdoDispatch.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rdoDispatch.Name = "rdoDispatch"
        Me.rdoDispatch.Size = New System.Drawing.Size(97, 24)
        Me.rdoDispatch.TabIndex = 2
        Me.rdoDispatch.Text = "Dispatch"
        Me.ToolTip1.SetToolTip(Me.rdoDispatch, "View Item Detail History By Dispatch")
        Me.rdoDispatch.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(201, 45)
        Me.RadioButton1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(154, 24)
        Me.RadioButton1.TabIndex = 5
        Me.RadioButton1.Text = "Purchase Return"
        Me.ToolTip1.SetToolTip(Me.RadioButton1, "View Item Detail History By Purchases Return")
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RBtnSalesReturn
        '
        Me.RBtnSalesReturn.AutoSize = True
        Me.RBtnSalesReturn.Location = New System.Drawing.Point(4, 45)
        Me.RBtnSalesReturn.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RBtnSalesReturn.Name = "RBtnSalesReturn"
        Me.RBtnSalesReturn.Size = New System.Drawing.Size(127, 24)
        Me.RBtnSalesReturn.TabIndex = 1
        Me.RBtnSalesReturn.Tag = ""
        Me.RBtnSalesReturn.Text = "Sales Return"
        Me.ToolTip1.SetToolTip(Me.RBtnSalesReturn, "View Item Detail History By Sales Return")
        Me.RBtnSalesReturn.UseVisualStyleBackColor = True
        '
        'RBtnPurchase
        '
        Me.RBtnPurchase.AutoSize = True
        Me.RBtnPurchase.Location = New System.Drawing.Point(201, 9)
        Me.RBtnPurchase.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RBtnPurchase.Name = "RBtnPurchase"
        Me.RBtnPurchase.Size = New System.Drawing.Size(101, 24)
        Me.RBtnPurchase.TabIndex = 4
        Me.RBtnPurchase.Text = "Purchase"
        Me.ToolTip1.SetToolTip(Me.RBtnPurchase, "View Item Detail History By Purchases")
        Me.RBtnPurchase.UseVisualStyleBackColor = True
        '
        'RBtnSales
        '
        Me.RBtnSales.AutoSize = True
        Me.RBtnSales.Checked = True
        Me.RBtnSales.Location = New System.Drawing.Point(4, 9)
        Me.RBtnSales.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RBtnSales.Name = "RBtnSales"
        Me.RBtnSales.Size = New System.Drawing.Size(74, 24)
        Me.RBtnSales.TabIndex = 0
        Me.RBtnSales.TabStop = True
        Me.RBtnSales.Text = "Sales"
        Me.ToolTip1.SetToolTip(Me.RBtnSales, "View Item Detail History By Sales")
        Me.RBtnSales.UseVisualStyleBackColor = True
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(986, 2)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar1.MyGrid = Me.GridEX1
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(57, 37)
        Me.CtrlGrdBar1.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.CtrlGrdBar1, "Settings")
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 38)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1044, 65)
        Me.pnlHeader.TabIndex = 2
        '
        'RptGridItemSalesHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1044, 889)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.BtnGenerate)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "RptGridItemSalesHistory"
        Me.Text = "Items Detail History"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents lstItems As SimpleAccounts.uiListControl
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DtpTo As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents DtpFrom As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents BtnGenerate As System.Windows.Forms.Button
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RBtnPurchase As System.Windows.Forms.RadioButton
    Friend WithEvents RBtnSales As System.Windows.Forms.RadioButton
    Friend WithEvents RBtnSalesReturn As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents rdoDispatch As System.Windows.Forms.RadioButton
    Friend WithEvents rdoStoreIssueance As System.Windows.Forms.RadioButton
    Friend WithEvents rdoProduction As System.Windows.Forms.RadioButton
    Friend WithEvents rdoStockReceiving As System.Windows.Forms.RadioButton
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtsearch As System.Windows.Forms.TextBox
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents rbtByName As System.Windows.Forms.RadioButton
    Friend WithEvents rbtByCode As System.Windows.Forms.RadioButton
    Friend WithEvents btnSaveLayout As System.Windows.Forms.ToolStripButton
    Friend WithEvents rbtnImports As System.Windows.Forms.RadioButton
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

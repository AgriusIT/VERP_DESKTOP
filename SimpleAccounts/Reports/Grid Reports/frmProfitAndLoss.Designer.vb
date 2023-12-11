<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProfitAndLoss
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProfitAndLoss))
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn17 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("coa_detail_id")
        Dim UltraGridColumn18 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("detail_title")
        Dim UltraGridColumn19 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("detail_code")
        Dim UltraGridColumn20 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("account_type")
        Dim UltraGridColumn21 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("sub_sub_title")
        Dim UltraGridColumn22 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("sub_title")
        Dim UltraGridColumn23 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("main_title")
        Dim UltraGridColumn24 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("main_type")
        Dim Appearance15 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance16 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance17 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance18 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand2 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn1 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("coa_detail_id")
        Dim UltraGridColumn2 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("detail_title")
        Dim UltraGridColumn3 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("detail_code")
        Dim UltraGridColumn4 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("account_type")
        Dim UltraGridColumn5 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("sub_sub_title")
        Dim UltraGridColumn6 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("sub_title")
        Dim UltraGridColumn7 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("main_title")
        Dim UltraGridColumn8 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("main_type")
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.lnkSalesNet = New System.Windows.Forms.LinkLabel()
        Me.lnkCostGoodSold = New System.Windows.Forms.LinkLabel()
        Me.lnkNonOperatingIncome = New System.Windows.Forms.LinkLabel()
        Me.lnkAdministrativeExp = New System.Windows.Forms.LinkLabel()
        Me.lnkSellingExpense = New System.Windows.Forms.LinkLabel()
        Me.lnkOtherOperatingExp = New System.Windows.Forms.LinkLabel()
        Me.lnkFinanceCost = New System.Windows.Forms.LinkLabel()
        Me.lnkTaxation = New System.Windows.Forms.LinkLabel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.lblSalesNet = New System.Windows.Forms.Label()
        Me.lblCostGoodSold = New System.Windows.Forms.Label()
        Me.lblNoneOperatingIncome = New System.Windows.Forms.Label()
        Me.lblAdministrativeExpense = New System.Windows.Forms.Label()
        Me.lblSellingExpense = New System.Windows.Forms.Label()
        Me.lblOperatingExp = New System.Windows.Forms.Label()
        Me.lblFinanceCost = New System.Windows.Forms.Label()
        Me.lblProfitBeforeTaxation = New System.Windows.Forms.Label()
        Me.lblProfitAfterTaxation = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblGrossProfit = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblTotalProfit = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lblTotalOperatingExpense = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblOepratingProfit = New System.Windows.Forms.Label()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.lblTaxation = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.chkExcludeClosingVoucher = New System.Windows.Forms.CheckBox()
        Me.chkShowSelectCurrency = New System.Windows.Forms.CheckBox()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.pnlProperty = New System.Windows.Forms.Panel()
        Me.cmbBranch = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.lblBranch = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmbCompany = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.lblCostCenter = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkposted = New System.Windows.Forms.CheckBox()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.chkinclsalescomission = New System.Windows.Forms.CheckBox()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        CType(Me.cmbBranch, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbCompany, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrintToolStripButton, Me.toolStripSeparator, Me.ToolStripButton1, Me.ToolStripButton3, Me.ToolStripButton2, Me.HelpToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1230, 31)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'PrintToolStripButton
        '
        Me.PrintToolStripButton.Image = CType(resources.GetObject("PrintToolStripButton.Image"), System.Drawing.Image)
        Me.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintToolStripButton.Name = "PrintToolStripButton"
        Me.PrintToolStripButton.Size = New System.Drawing.Size(67, 28)
        Me.PrintToolStripButton.Text = "&Print"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 31)
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Image = Global.SimpleAccounts.My.Resources.Resources.Copy
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(122, 28)
        Me.ToolStripButton1.Text = "Print Preview"
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.Image = Global.SimpleAccounts.My.Resources.Resources.copy_doc
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(80, 28)
        Me.ToolStripButton3.Text = "Export"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Image = Global.SimpleAccounts.My.Resources.Resources.Email_Envelope
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(111, 28)
        Me.ToolStripButton2.Text = "Send Email"
        '
        'HelpToolStripButton
        '
        Me.HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HelpToolStripButton.Image = CType(resources.GetObject("HelpToolStripButton.Image"), System.Drawing.Image)
        Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton.Name = "HelpToolStripButton"
        Me.HelpToolStripButton.Size = New System.Drawing.Size(28, 28)
        Me.HelpToolStripButton.Text = "He&lp"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(437, 59)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(193, 24)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Profit before Taxation"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(437, 137)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(146, 24)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "Operating Profit"
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(11, 14)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(202, 31)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Profit and Loss "
        '
        'lnkSalesNet
        '
        Me.lnkSalesNet.AutoSize = True
        Me.lnkSalesNet.Location = New System.Drawing.Point(12, 20)
        Me.lnkSalesNet.Name = "lnkSalesNet"
        Me.lnkSalesNet.Size = New System.Drawing.Size(88, 24)
        Me.lnkSalesNet.TabIndex = 0
        Me.lnkSalesNet.TabStop = True
        Me.lnkSalesNet.Text = "Sales-Net"
        '
        'lnkCostGoodSold
        '
        Me.lnkCostGoodSold.AutoSize = True
        Me.lnkCostGoodSold.LinkColor = System.Drawing.Color.Blue
        Me.lnkCostGoodSold.Location = New System.Drawing.Point(12, 45)
        Me.lnkCostGoodSold.Name = "lnkCostGoodSold"
        Me.lnkCostGoodSold.Size = New System.Drawing.Size(161, 24)
        Me.lnkCostGoodSold.TabIndex = 2
        Me.lnkCostGoodSold.TabStop = True
        Me.lnkCostGoodSold.Text = "Cost of Good Sold"
        '
        'lnkNonOperatingIncome
        '
        Me.lnkNonOperatingIncome.AutoSize = True
        Me.lnkNonOperatingIncome.Location = New System.Drawing.Point(13, 20)
        Me.lnkNonOperatingIncome.Name = "lnkNonOperatingIncome"
        Me.lnkNonOperatingIncome.Size = New System.Drawing.Size(201, 24)
        Me.lnkNonOperatingIncome.TabIndex = 0
        Me.lnkNonOperatingIncome.TabStop = True
        Me.lnkNonOperatingIncome.Text = "Non-Operating Income"
        '
        'lnkAdministrativeExp
        '
        Me.lnkAdministrativeExp.AutoSize = True
        Me.lnkAdministrativeExp.Location = New System.Drawing.Point(14, 20)
        Me.lnkAdministrativeExp.Name = "lnkAdministrativeExp"
        Me.lnkAdministrativeExp.Size = New System.Drawing.Size(207, 24)
        Me.lnkAdministrativeExp.TabIndex = 0
        Me.lnkAdministrativeExp.TabStop = True
        Me.lnkAdministrativeExp.Text = "Administrative Expense"
        '
        'lnkSellingExpense
        '
        Me.lnkSellingExpense.AutoSize = True
        Me.lnkSellingExpense.Location = New System.Drawing.Point(14, 45)
        Me.lnkSellingExpense.Name = "lnkSellingExpense"
        Me.lnkSellingExpense.Size = New System.Drawing.Size(146, 24)
        Me.lnkSellingExpense.TabIndex = 2
        Me.lnkSellingExpense.TabStop = True
        Me.lnkSellingExpense.Text = "Selling Expenses"
        '
        'lnkOtherOperatingExp
        '
        Me.lnkOtherOperatingExp.AutoSize = True
        Me.lnkOtherOperatingExp.Location = New System.Drawing.Point(14, 68)
        Me.lnkOtherOperatingExp.Name = "lnkOtherOperatingExp"
        Me.lnkOtherOperatingExp.Size = New System.Drawing.Size(222, 24)
        Me.lnkOtherOperatingExp.TabIndex = 4
        Me.lnkOtherOperatingExp.TabStop = True
        Me.lnkOtherOperatingExp.Text = "Other Operating Expense"
        '
        'lnkFinanceCost
        '
        Me.lnkFinanceCost.AutoSize = True
        Me.lnkFinanceCost.Location = New System.Drawing.Point(14, 19)
        Me.lnkFinanceCost.Name = "lnkFinanceCost"
        Me.lnkFinanceCost.Size = New System.Drawing.Size(116, 24)
        Me.lnkFinanceCost.TabIndex = 0
        Me.lnkFinanceCost.TabStop = True
        Me.lnkFinanceCost.Text = "Finance Cost"
        '
        'lnkTaxation
        '
        Me.lnkTaxation.AutoSize = True
        Me.lnkTaxation.Location = New System.Drawing.Point(12, 19)
        Me.lnkTaxation.Name = "lnkTaxation"
        Me.lnkTaxation.Size = New System.Drawing.Size(81, 24)
        Me.lnkTaxation.TabIndex = 0
        Me.lnkTaxation.TabStop = True
        Me.lnkTaxation.Text = "Taxation"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(424, 54)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(215, 29)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Profit After Taxation"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(512, 88)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(30, 18)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "To "
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(363, 88)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(44, 18)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "From"
        '
        'dtpToDate
        '
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpToDate.Location = New System.Drawing.Point(542, 85)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(102, 24)
        Me.dtpToDate.TabIndex = 9
        '
        'dtpFromDate
        '
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFromDate.Location = New System.Drawing.Point(404, 85)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(102, 24)
        Me.dtpFromDate.TabIndex = 7
        '
        'Label38
        '
        Me.Label38.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(12, 135)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(777, 23)
        Me.Label38.TabIndex = 18
        Me.Label38.Text = "_________________________________________________________________________________" & _
    "_______________________________________________________"
        '
        'lblSalesNet
        '
        Me.lblSalesNet.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesNet.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSalesNet.Location = New System.Drawing.Point(603, 20)
        Me.lblSalesNet.Name = "lblSalesNet"
        Me.lblSalesNet.Size = New System.Drawing.Size(160, 16)
        Me.lblSalesNet.TabIndex = 1
        Me.lblSalesNet.Text = "0"
        Me.lblSalesNet.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCostGoodSold
        '
        Me.lblCostGoodSold.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCostGoodSold.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCostGoodSold.Location = New System.Drawing.Point(603, 45)
        Me.lblCostGoodSold.Name = "lblCostGoodSold"
        Me.lblCostGoodSold.Size = New System.Drawing.Size(160, 16)
        Me.lblCostGoodSold.TabIndex = 3
        Me.lblCostGoodSold.Text = "0"
        Me.lblCostGoodSold.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblNoneOperatingIncome
        '
        Me.lblNoneOperatingIncome.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNoneOperatingIncome.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblNoneOperatingIncome.Location = New System.Drawing.Point(603, 21)
        Me.lblNoneOperatingIncome.Name = "lblNoneOperatingIncome"
        Me.lblNoneOperatingIncome.Size = New System.Drawing.Size(160, 16)
        Me.lblNoneOperatingIncome.TabIndex = 1
        Me.lblNoneOperatingIncome.Text = "0"
        Me.lblNoneOperatingIncome.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblAdministrativeExpense
        '
        Me.lblAdministrativeExpense.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAdministrativeExpense.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAdministrativeExpense.Location = New System.Drawing.Point(603, 20)
        Me.lblAdministrativeExpense.Name = "lblAdministrativeExpense"
        Me.lblAdministrativeExpense.Size = New System.Drawing.Size(160, 16)
        Me.lblAdministrativeExpense.TabIndex = 1
        Me.lblAdministrativeExpense.Text = "0"
        Me.lblAdministrativeExpense.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblSellingExpense
        '
        Me.lblSellingExpense.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSellingExpense.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSellingExpense.Location = New System.Drawing.Point(603, 45)
        Me.lblSellingExpense.Name = "lblSellingExpense"
        Me.lblSellingExpense.Size = New System.Drawing.Size(160, 16)
        Me.lblSellingExpense.TabIndex = 3
        Me.lblSellingExpense.Text = "0"
        Me.lblSellingExpense.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblOperatingExp
        '
        Me.lblOperatingExp.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOperatingExp.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblOperatingExp.Location = New System.Drawing.Point(603, 68)
        Me.lblOperatingExp.Name = "lblOperatingExp"
        Me.lblOperatingExp.Size = New System.Drawing.Size(160, 16)
        Me.lblOperatingExp.TabIndex = 5
        Me.lblOperatingExp.Text = "0"
        Me.lblOperatingExp.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblFinanceCost
        '
        Me.lblFinanceCost.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFinanceCost.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFinanceCost.Location = New System.Drawing.Point(603, 20)
        Me.lblFinanceCost.Name = "lblFinanceCost"
        Me.lblFinanceCost.Size = New System.Drawing.Size(160, 16)
        Me.lblFinanceCost.TabIndex = 1
        Me.lblFinanceCost.Text = "0"
        Me.lblFinanceCost.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblProfitBeforeTaxation
        '
        Me.lblProfitBeforeTaxation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblProfitBeforeTaxation.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProfitBeforeTaxation.ForeColor = System.Drawing.Color.Navy
        Me.lblProfitBeforeTaxation.Location = New System.Drawing.Point(603, 58)
        Me.lblProfitBeforeTaxation.Name = "lblProfitBeforeTaxation"
        Me.lblProfitBeforeTaxation.Size = New System.Drawing.Size(160, 21)
        Me.lblProfitBeforeTaxation.TabIndex = 3
        Me.lblProfitBeforeTaxation.Text = "0"
        Me.lblProfitBeforeTaxation.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblProfitAfterTaxation
        '
        Me.lblProfitAfterTaxation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblProfitAfterTaxation.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProfitAfterTaxation.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblProfitAfterTaxation.Location = New System.Drawing.Point(603, 53)
        Me.lblProfitAfterTaxation.Name = "lblProfitAfterTaxation"
        Me.lblProfitAfterTaxation.Size = New System.Drawing.Size(160, 23)
        Me.lblProfitAfterTaxation.TabIndex = 3
        Me.lblProfitAfterTaxation.Text = "0"
        Me.lblProfitAfterTaxation.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(567, 117)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(77, 28)
        Me.Button1.TabIndex = 17
        Me.Button1.Text = "Generate Report"
        Me.ToolTip1.SetToolTip(Me.Button1, "Press button to generate the report.")
        Me.Button1.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblGrossProfit)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.lnkSalesNet)
        Me.GroupBox1.Controls.Add(Me.lnkCostGoodSold)
        Me.GroupBox1.Controls.Add(Me.lblSalesNet)
        Me.GroupBox1.Controls.Add(Me.lblCostGoodSold)
        Me.GroupBox1.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 155)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(774, 101)
        Me.GroupBox1.TabIndex = 19
        Me.GroupBox1.TabStop = False
        '
        'lblGrossProfit
        '
        Me.lblGrossProfit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblGrossProfit.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGrossProfit.ForeColor = System.Drawing.Color.Navy
        Me.lblGrossProfit.Location = New System.Drawing.Point(603, 73)
        Me.lblGrossProfit.Name = "lblGrossProfit"
        Me.lblGrossProfit.Size = New System.Drawing.Size(160, 21)
        Me.lblGrossProfit.TabIndex = 5
        Me.lblGrossProfit.Text = "0"
        Me.lblGrossProfit.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(437, 75)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(160, 16)
        Me.Label12.TabIndex = 4
        Me.Label12.Text = "Gross Profit"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblTotalProfit)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.lnkNonOperatingIncome)
        Me.GroupBox2.Controls.Add(Me.lblNoneOperatingIncome)
        Me.GroupBox2.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(12, 259)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(774, 72)
        Me.GroupBox2.TabIndex = 20
        Me.GroupBox2.TabStop = False
        '
        'lblTotalProfit
        '
        Me.lblTotalProfit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalProfit.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalProfit.ForeColor = System.Drawing.Color.Navy
        Me.lblTotalProfit.Location = New System.Drawing.Point(603, 44)
        Me.lblTotalProfit.Name = "lblTotalProfit"
        Me.lblTotalProfit.Size = New System.Drawing.Size(160, 21)
        Me.lblTotalProfit.TabIndex = 3
        Me.lblTotalProfit.Text = "0"
        Me.lblTotalProfit.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(437, 46)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(160, 16)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "Total Profit"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lblTotalOperatingExpense)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.lblOepratingProfit)
        Me.GroupBox3.Controls.Add(Me.lnkAdministrativeExp)
        Me.GroupBox3.Controls.Add(Me.lnkSellingExpense)
        Me.GroupBox3.Controls.Add(Me.lnkOtherOperatingExp)
        Me.GroupBox3.Controls.Add(Me.lblAdministrativeExpense)
        Me.GroupBox3.Controls.Add(Me.lblOperatingExp)
        Me.GroupBox3.Controls.Add(Me.lblSellingExpense)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(12, 337)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(774, 165)
        Me.GroupBox3.TabIndex = 21
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Operating Expense"
        '
        'lblTotalOperatingExpense
        '
        Me.lblTotalOperatingExpense.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalOperatingExpense.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalOperatingExpense.ForeColor = System.Drawing.Color.Navy
        Me.lblTotalOperatingExpense.Location = New System.Drawing.Point(603, 105)
        Me.lblTotalOperatingExpense.Name = "lblTotalOperatingExpense"
        Me.lblTotalOperatingExpense.Size = New System.Drawing.Size(160, 21)
        Me.lblTotalOperatingExpense.TabIndex = 7
        Me.lblTotalOperatingExpense.Text = "0"
        Me.lblTotalOperatingExpense.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(437, 106)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(168, 24)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Operating Expense"
        '
        'lblOepratingProfit
        '
        Me.lblOepratingProfit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblOepratingProfit.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOepratingProfit.ForeColor = System.Drawing.Color.Navy
        Me.lblOepratingProfit.Location = New System.Drawing.Point(603, 136)
        Me.lblOepratingProfit.Name = "lblOepratingProfit"
        Me.lblOepratingProfit.Size = New System.Drawing.Size(160, 21)
        Me.lblOepratingProfit.TabIndex = 9
        Me.lblOepratingProfit.Text = "0"
        Me.lblOepratingProfit.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.lnkFinanceCost)
        Me.GroupBox4.Controls.Add(Me.lblFinanceCost)
        Me.GroupBox4.Controls.Add(Me.lblProfitBeforeTaxation)
        Me.GroupBox4.Controls.Add(Me.Label6)
        Me.GroupBox4.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(12, 508)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(774, 85)
        Me.GroupBox4.TabIndex = 22
        Me.GroupBox4.TabStop = False
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.lblTaxation)
        Me.GroupBox5.Controls.Add(Me.lblProfitAfterTaxation)
        Me.GroupBox5.Controls.Add(Me.lnkTaxation)
        Me.GroupBox5.Controls.Add(Me.Label3)
        Me.GroupBox5.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.Location = New System.Drawing.Point(12, 599)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(774, 82)
        Me.GroupBox5.TabIndex = 23
        Me.GroupBox5.TabStop = False
        '
        'lblTaxation
        '
        Me.lblTaxation.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTaxation.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTaxation.Location = New System.Drawing.Point(603, 19)
        Me.lblTaxation.Name = "lblTaxation"
        Me.lblTaxation.Size = New System.Drawing.Size(160, 16)
        Me.lblTaxation.TabIndex = 1
        Me.lblTaxation.Text = "0"
        Me.lblTaxation.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"Today", "Yesterday", "Current Week", "Current Month", "Current Year"})
        Me.ComboBox1.Location = New System.Drawing.Point(244, 85)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(115, 26)
        Me.ComboBox1.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.ComboBox1, "Please select a project")
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.DefaultExt = "pdf"
        Me.SaveFileDialog1.Filter = "Portable Doc Format|*.pdf"
        Me.SaveFileDialog1.SupportMultiDottedExtensions = True
        Me.SaveFileDialog1.Title = "Export File"
        '
        'chkExcludeClosingVoucher
        '
        Me.chkExcludeClosingVoucher.AutoSize = True
        Me.chkExcludeClosingVoucher.Checked = True
        Me.chkExcludeClosingVoucher.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkExcludeClosingVoucher.Location = New System.Drawing.Point(384, 121)
        Me.chkExcludeClosingVoucher.Name = "chkExcludeClosingVoucher"
        Me.chkExcludeClosingVoucher.Size = New System.Drawing.Size(171, 22)
        Me.chkExcludeClosingVoucher.TabIndex = 16
        Me.chkExcludeClosingVoucher.Text = "Excl Closing Voucher"
        Me.chkExcludeClosingVoucher.UseVisualStyleBackColor = True
        '
        'chkShowSelectCurrency
        '
        Me.chkShowSelectCurrency.AutoSize = True
        Me.chkShowSelectCurrency.Location = New System.Drawing.Point(951, 117)
        Me.chkShowSelectCurrency.Name = "chkShowSelectCurrency"
        Me.chkShowSelectCurrency.Size = New System.Drawing.Size(151, 22)
        Me.chkShowSelectCurrency.TabIndex = 15
        Me.chkShowSelectCurrency.Text = "Selected Currency"
        Me.chkShowSelectCurrency.UseVisualStyleBackColor = True
        Me.chkShowSelectCurrency.Visible = False
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Controls.Add(Me.pnlProperty)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 31)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1230, 50)
        Me.pnlHeader.TabIndex = 1
        '
        'pnlProperty
        '
        Me.pnlProperty.Location = New System.Drawing.Point(664, 49)
        Me.pnlProperty.Name = "pnlProperty"
        Me.pnlProperty.Size = New System.Drawing.Size(178, 31)
        Me.pnlProperty.TabIndex = 10
        '
        'cmbBranch
        '
        Appearance13.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.cmbBranch.Appearance = Appearance13
        Me.cmbBranch.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cmbBranch.AutoSize = False
        Me.cmbBranch.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbBranch.CheckedListSettings.CheckStateMember = ""
        Appearance14.BackColor = System.Drawing.Color.White
        Me.cmbBranch.DisplayLayout.Appearance = Appearance14
        UltraGridColumn17.Header.Caption = "ID"
        UltraGridColumn17.Header.VisiblePosition = 0
        UltraGridColumn17.Hidden = True
        UltraGridColumn17.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(8, 0)
        UltraGridColumn18.Header.Caption = "Account"
        UltraGridColumn18.Header.VisiblePosition = 1
        UltraGridColumn18.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(128, 0)
        UltraGridColumn19.Header.Caption = "Code"
        UltraGridColumn19.Header.VisiblePosition = 2
        UltraGridColumn20.Header.Caption = "Type"
        UltraGridColumn20.Header.VisiblePosition = 3
        UltraGridColumn20.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(96, 0)
        UltraGridColumn21.Header.Caption = "Sub Sub Ac"
        UltraGridColumn21.Header.VisiblePosition = 4
        UltraGridColumn21.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(106, 0)
        UltraGridColumn22.Header.Caption = "Sub Ac"
        UltraGridColumn22.Header.VisiblePosition = 5
        UltraGridColumn22.Hidden = True
        UltraGridColumn22.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(102, 0)
        UltraGridColumn23.Header.Caption = "Main Ac"
        UltraGridColumn23.Header.VisiblePosition = 6
        UltraGridColumn23.Hidden = True
        UltraGridColumn23.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(82, 0)
        UltraGridColumn24.Header.Caption = "Ac Head"
        UltraGridColumn24.Header.VisiblePosition = 7
        UltraGridColumn24.Hidden = True
        UltraGridColumn24.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(84, 0)
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn17, UltraGridColumn18, UltraGridColumn19, UltraGridColumn20, UltraGridColumn21, UltraGridColumn22, UltraGridColumn23, UltraGridColumn24})
        Me.cmbBranch.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbBranch.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbBranch.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbBranch.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbBranch.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Appearance15.BackColor = System.Drawing.Color.Transparent
        Me.cmbBranch.DisplayLayout.Override.CardAreaAppearance = Appearance15
        Me.cmbBranch.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbBranch.DisplayLayout.Override.CellPadding = 3
        Me.cmbBranch.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance16.TextHAlignAsString = "Left"
        Me.cmbBranch.DisplayLayout.Override.HeaderAppearance = Appearance16
        Me.cmbBranch.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance17.BorderColor = System.Drawing.Color.LightGray
        Appearance17.TextVAlignAsString = "Middle"
        Me.cmbBranch.DisplayLayout.Override.RowAppearance = Appearance17
        Appearance18.BackColor = System.Drawing.Color.LightSteelBlue
        Appearance18.BorderColor = System.Drawing.Color.Black
        Appearance18.ForeColor = System.Drawing.Color.Black
        Me.cmbBranch.DisplayLayout.Override.SelectedRowAppearance = Appearance18
        Me.cmbBranch.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbBranch.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbBranch.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbBranch.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.cmbBranch.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbBranch.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbBranch.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbBranch.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbBranch.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbBranch.LimitToList = True
        Me.cmbBranch.Location = New System.Drawing.Point(48, 85)
        Me.cmbBranch.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbBranch.MaxDropDownItems = 16
        Me.cmbBranch.Name = "cmbBranch"
        Me.cmbBranch.Size = New System.Drawing.Size(105, 22)
        Me.cmbBranch.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.cmbBranch, "Please select a branch")
        '
        'lblBranch
        '
        Me.lblBranch.AutoSize = True
        Me.lblBranch.Location = New System.Drawing.Point(4, 88)
        Me.lblBranch.Name = "lblBranch"
        Me.lblBranch.Size = New System.Drawing.Size(43, 18)
        Me.lblBranch.TabIndex = 2
        Me.lblBranch.Text = "Head"
        '
        'cmbCompany
        '
        Appearance1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.cmbCompany.Appearance = Appearance1
        Me.cmbCompany.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cmbCompany.AutoSize = False
        Me.cmbCompany.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbCompany.CheckedListSettings.CheckStateMember = ""
        Appearance2.BackColor = System.Drawing.SystemColors.Control
        Me.cmbCompany.DisplayLayout.Appearance = Appearance2
        UltraGridColumn1.Header.Caption = "ID"
        UltraGridColumn1.Header.VisiblePosition = 0
        UltraGridColumn1.Hidden = True
        UltraGridColumn1.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(8, 0)
        UltraGridColumn2.Header.Caption = "Account"
        UltraGridColumn2.Header.VisiblePosition = 1
        UltraGridColumn2.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(128, 0)
        UltraGridColumn3.Header.Caption = "Code"
        UltraGridColumn3.Header.VisiblePosition = 2
        UltraGridColumn4.Header.Caption = "Type"
        UltraGridColumn4.Header.VisiblePosition = 3
        UltraGridColumn4.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(96, 0)
        UltraGridColumn5.Header.Caption = "Sub Sub Ac"
        UltraGridColumn5.Header.VisiblePosition = 4
        UltraGridColumn5.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(106, 0)
        UltraGridColumn6.Header.Caption = "Sub Ac"
        UltraGridColumn6.Header.VisiblePosition = 5
        UltraGridColumn6.Hidden = True
        UltraGridColumn6.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(102, 0)
        UltraGridColumn7.Header.Caption = "Main Ac"
        UltraGridColumn7.Header.VisiblePosition = 6
        UltraGridColumn7.Hidden = True
        UltraGridColumn7.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(82, 0)
        UltraGridColumn8.Header.Caption = "Ac Head"
        UltraGridColumn8.Header.VisiblePosition = 7
        UltraGridColumn8.Hidden = True
        UltraGridColumn8.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(84, 0)
        UltraGridBand2.Columns.AddRange(New Object() {UltraGridColumn1, UltraGridColumn2, UltraGridColumn3, UltraGridColumn4, UltraGridColumn5, UltraGridColumn6, UltraGridColumn7, UltraGridColumn8})
        Me.cmbCompany.DisplayLayout.BandsSerializer.Add(UltraGridBand2)
        Me.cmbCompany.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbCompany.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbCompany.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbCompany.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Appearance3.BackColor = System.Drawing.Color.Transparent
        Me.cmbCompany.DisplayLayout.Override.CardAreaAppearance = Appearance3
        Me.cmbCompany.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbCompany.DisplayLayout.Override.CellPadding = 3
        Me.cmbCompany.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance4.TextHAlignAsString = "Left"
        Me.cmbCompany.DisplayLayout.Override.HeaderAppearance = Appearance4
        Me.cmbCompany.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance5.BorderColor = System.Drawing.Color.LightGray
        Appearance5.TextVAlignAsString = "Middle"
        Me.cmbCompany.DisplayLayout.Override.RowAppearance = Appearance5
        Appearance6.BackColor = System.Drawing.Color.LightSteelBlue
        Appearance6.BorderColor = System.Drawing.Color.Black
        Appearance6.ForeColor = System.Drawing.Color.Black
        Me.cmbCompany.DisplayLayout.Override.SelectedRowAppearance = Appearance6
        Me.cmbCompany.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbCompany.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbCompany.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbCompany.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.cmbCompany.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbCompany.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbCompany.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbCompany.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbCompany.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCompany.LimitToList = True
        Me.cmbCompany.Location = New System.Drawing.Point(832, 117)
        Me.cmbCompany.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbCompany.MaxDropDownItems = 16
        Me.cmbCompany.Name = "cmbCompany"
        Me.cmbCompany.Size = New System.Drawing.Size(110, 22)
        Me.cmbCompany.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.cmbCompany, "Please select a branch")
        Me.cmbCompany.Visible = False
        '
        'lblCostCenter
        '
        Me.lblCostCenter.AutoSize = True
        Me.lblCostCenter.Location = New System.Drawing.Point(156, 88)
        Me.lblCostCenter.Name = "lblCostCenter"
        Me.lblCostCenter.Size = New System.Drawing.Size(88, 18)
        Me.lblCostCenter.TabIndex = 4
        Me.lblCostCenter.Text = "Cost Center"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(829, 95)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 18)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Company"
        Me.Label1.Visible = False
        '
        'chkposted
        '
        Me.chkposted.AutoSize = True
        Me.chkposted.Checked = True
        Me.chkposted.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkposted.Location = New System.Drawing.Point(199, 121)
        Me.chkposted.Name = "chkposted"
        Me.chkposted.Size = New System.Drawing.Size(183, 22)
        Me.chkposted.TabIndex = 24
        Me.chkposted.Text = "Incl Unposted Voucher "
        Me.chkposted.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Checked = True
        Me.CheckBox1.CheckState = System.Windows.Forms.CheckState.Checked
        Me.CheckBox1.Location = New System.Drawing.Point(951, 89)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(183, 22)
        Me.CheckBox1.TabIndex = 25
        Me.CheckBox1.Text = "Incl Unposted Voucher "
        Me.CheckBox1.UseVisualStyleBackColor = True
        Me.CheckBox1.Visible = False
        '
        'chkinclsalescomission
        '
        Me.chkinclsalescomission.AutoSize = True
        Me.chkinclsalescomission.Location = New System.Drawing.Point(107, 121)
        Me.chkinclsalescomission.Name = "chkinclsalescomission"
        Me.chkinclsalescomission.Size = New System.Drawing.Size(86, 22)
        Me.chkinclsalescomission.TabIndex = 26
        Me.chkinclsalescomission.Text = "Incl SCV"
        Me.chkinclsalescomission.UseVisualStyleBackColor = True
        '
        'frmProfitAndLoss
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1230, 698)
        Me.Controls.Add(Me.chkinclsalescomission)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.chkposted)
        Me.Controls.Add(Me.cmbCompany)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblCostCenter)
        Me.Controls.Add(Me.cmbBranch)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.lblBranch)
        Me.Controls.Add(Me.chkShowSelectCurrency)
        Me.Controls.Add(Me.chkExcludeClosingVoucher)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.dtpToDate)
        Me.Controls.Add(Me.dtpFromDate)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.Label38)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox5)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmProfitAndLoss"
        Me.Text = "Profit And Loss"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.cmbBranch, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbCompany, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents PrintToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents lnkSalesNet As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkCostGoodSold As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkNonOperatingIncome As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkAdministrativeExp As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkSellingExpense As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkOtherOperatingExp As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkFinanceCost As System.Windows.Forms.LinkLabel
    Friend WithEvents lnkTaxation As System.Windows.Forms.LinkLabel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents lblSalesNet As System.Windows.Forms.Label
    Friend WithEvents lblCostGoodSold As System.Windows.Forms.Label
    Friend WithEvents lblNoneOperatingIncome As System.Windows.Forms.Label
    Friend WithEvents lblAdministrativeExpense As System.Windows.Forms.Label
    Friend WithEvents lblSellingExpense As System.Windows.Forms.Label
    Friend WithEvents lblOperatingExp As System.Windows.Forms.Label
    Friend WithEvents lblFinanceCost As System.Windows.Forms.Label
    Friend WithEvents lblProfitBeforeTaxation As System.Windows.Forms.Label
    Friend WithEvents lblProfitAfterTaxation As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lblOepratingProfit As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents lblTaxation As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents lblTotalOperatingExpense As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblGrossProfit As System.Windows.Forms.Label
    Friend WithEvents lblTotalProfit As System.Windows.Forms.Label
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents chkExcludeClosingVoucher As System.Windows.Forms.CheckBox
    Friend WithEvents chkShowSelectCurrency As System.Windows.Forms.CheckBox
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents pnlProperty As System.Windows.Forms.Panel
    Friend WithEvents lblBranch As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents cmbBranch As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents lblCostCenter As System.Windows.Forms.Label
    Friend WithEvents cmbCompany As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkposted As System.Windows.Forms.CheckBox
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents chkinclsalescomission As System.Windows.Forms.CheckBox
End Class

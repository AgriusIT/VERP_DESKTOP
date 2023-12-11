<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBalanceSheet
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBalanceSheet))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.lblCostCentre = New System.Windows.Forms.ToolStripLabel()
        Me.cmbCostCentre = New System.Windows.Forms.ToolStripComboBox()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel3 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel4 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel5 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel6 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel7 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel8 = New System.Windows.Forms.LinkLabel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.LinkLabel9 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel10 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel12 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel13 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel14 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel16 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel17 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel18 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel19 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel20 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel21 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel23 = New System.Windows.Forms.LinkLabel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Label28 = New System.Windows.Forms.Label()
        Me.Label29 = New System.Windows.Forms.Label()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.Label31 = New System.Windows.Forms.Label()
        Me.Label32 = New System.Windows.Forms.Label()
        Me.Label33 = New System.Windows.Forms.Label()
        Me.Label34 = New System.Windows.Forms.Label()
        Me.Label35 = New System.Windows.Forms.Label()
        Me.Label36 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.lblTotalNoneCurrentAssets = New System.Windows.Forms.Label()
        Me.lblTotalCurrentAssets = New System.Windows.Forms.Label()
        Me.lblTotalOwnerEquity = New System.Windows.Forms.Label()
        Me.lblTotalNoneLiabilities = New System.Windows.Forms.Label()
        Me.lblCurrentLiabilities = New System.Windows.Forms.Label()
        Me.lblTotalCapital = New System.Windows.Forms.Label()
        Me.lblTotalAssets = New System.Windows.Forms.Label()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lnkSharedDeposit = New System.Windows.Forms.LinkLabel()
        Me.lblSharedDeposit = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.LinkLabel15 = New System.Windows.Forms.LinkLabel()
        Me.chkIncludeUnPostedVouchers = New System.Windows.Forms.CheckBox()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrintToolStripButton, Me.toolStripSeparator, Me.ToolStripButton1, Me.ToolStripButton3, Me.ToolStripButton2, Me.ToolStripSeparator1, Me.btnRefresh, Me.HelpToolStripButton, Me.lblCostCentre, Me.cmbCostCentre})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(891, 33)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'PrintToolStripButton
        '
        Me.PrintToolStripButton.Image = CType(resources.GetObject("PrintToolStripButton.Image"), System.Drawing.Image)
        Me.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintToolStripButton.Name = "PrintToolStripButton"
        Me.PrintToolStripButton.Size = New System.Drawing.Size(76, 30)
        Me.PrintToolStripButton.Text = "&Print"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 33)
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Image = Global.SimpleAccounts.My.Resources.Resources.Copy
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(141, 30)
        Me.ToolStripButton1.Text = "Print Preview"
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.Image = Global.SimpleAccounts.My.Resources.Resources.copy_doc
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(91, 30)
        Me.ToolStripButton3.Text = "Export"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Image = Global.SimpleAccounts.My.Resources.Resources.Email_Envelope
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(127, 30)
        Me.ToolStripButton2.Text = "Send Email"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 33)
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 30)
        Me.btnRefresh.Text = "Refresh"
        '
        'HelpToolStripButton
        '
        Me.HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HelpToolStripButton.Image = CType(resources.GetObject("HelpToolStripButton.Image"), System.Drawing.Image)
        Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton.Name = "HelpToolStripButton"
        Me.HelpToolStripButton.Size = New System.Drawing.Size(28, 30)
        Me.HelpToolStripButton.Text = "He&lp"
        '
        'lblCostCentre
        '
        Me.lblCostCentre.Image = Global.SimpleAccounts.My.Resources.Resources.Bank
        Me.lblCostCentre.Name = "lblCostCentre"
        Me.lblCostCentre.Size = New System.Drawing.Size(128, 30)
        Me.lblCostCentre.Text = "Cost Centre"
        '
        'cmbCostCentre
        '
        Me.cmbCostCentre.FlatStyle = System.Windows.Forms.FlatStyle.Standard
        Me.cmbCostCentre.Name = "cmbCostCentre"
        Me.cmbCostCentre.Size = New System.Drawing.Size(150, 33)
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(22, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(348, 41)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Balance Sheet Report"
        '
        'dtpToDate
        '
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpToDate.Location = New System.Drawing.Point(662, 96)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(116, 26)
        Me.dtpToDate.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(608, 99)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 20)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "To Date"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.LinkArea = New System.Windows.Forms.LinkArea(0, 14)
        Me.LinkLabel1.Location = New System.Drawing.Point(11, 25)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(140, 34)
        Me.LinkLabel1.TabIndex = 9
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Share Capital "
        Me.LinkLabel1.UseCompatibleTextRendering = True
        '
        'LinkLabel2
        '
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.Location = New System.Drawing.Point(11, 73)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(319, 28)
        Me.LinkLabel2.TabIndex = 10
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "Un-Appropriated Profit And Loss"
        '
        'LinkLabel3
        '
        Me.LinkLabel3.AutoSize = True
        Me.LinkLabel3.Location = New System.Drawing.Point(11, 94)
        Me.LinkLabel3.Name = "LinkLabel3"
        Me.LinkLabel3.Size = New System.Drawing.Size(98, 28)
        Me.LinkLabel3.TabIndex = 11
        Me.LinkLabel3.TabStop = True
        Me.LinkLabel3.Text = "Reserves"
        '
        'LinkLabel4
        '
        Me.LinkLabel4.AutoSize = True
        Me.LinkLabel4.Location = New System.Drawing.Point(11, 27)
        Me.LinkLabel4.Name = "LinkLabel4"
        Me.LinkLabel4.Size = New System.Drawing.Size(156, 28)
        Me.LinkLabel4.TabIndex = 12
        Me.LinkLabel4.TabStop = True
        Me.LinkLabel4.Text = "Financial Lease"
        '
        'LinkLabel5
        '
        Me.LinkLabel5.AutoSize = True
        Me.LinkLabel5.Location = New System.Drawing.Point(11, 49)
        Me.LinkLabel5.Name = "LinkLabel5"
        Me.LinkLabel5.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.LinkLabel5.Size = New System.Drawing.Size(206, 28)
        Me.LinkLabel5.TabIndex = 13
        Me.LinkLabel5.TabStop = True
        Me.LinkLabel5.Text = "Long Term Advances"
        '
        'LinkLabel6
        '
        Me.LinkLabel6.AutoSize = True
        Me.LinkLabel6.Location = New System.Drawing.Point(11, 71)
        Me.LinkLabel6.Name = "LinkLabel6"
        Me.LinkLabel6.Size = New System.Drawing.Size(181, 28)
        Me.LinkLabel6.TabIndex = 14
        Me.LinkLabel6.TabStop = True
        Me.LinkLabel6.Text = "Deffered Taxation"
        '
        'LinkLabel7
        '
        Me.LinkLabel7.Location = New System.Drawing.Point(14, 26)
        Me.LinkLabel7.Name = "LinkLabel7"
        Me.LinkLabel7.Size = New System.Drawing.Size(244, 39)
        Me.LinkLabel7.TabIndex = 15
        Me.LinkLabel7.TabStop = True
        Me.LinkLabel7.Text = "Current Portion of long term financing"
        Me.LinkLabel7.UseCompatibleTextRendering = True
        '
        'LinkLabel8
        '
        Me.LinkLabel8.AutoSize = True
        Me.LinkLabel8.Location = New System.Drawing.Point(14, 68)
        Me.LinkLabel8.Name = "LinkLabel8"
        Me.LinkLabel8.Size = New System.Drawing.Size(229, 28)
        Me.LinkLabel8.TabIndex = 16
        Me.LinkLabel8.TabStop = True
        Me.LinkLabel8.Text = "Short Term Borrowings"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(72, 121)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(203, 29)
        Me.Label6.TabIndex = 18
        Me.Label6.Text = "Total Owner Equity"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(72, 102)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(209, 29)
        Me.Label8.TabIndex = 20
        Me.Label8.Text = "Total Non Liabilities"
        '
        'LinkLabel9
        '
        Me.LinkLabel9.AutoSize = True
        Me.LinkLabel9.Location = New System.Drawing.Point(14, 90)
        Me.LinkLabel9.Name = "LinkLabel9"
        Me.LinkLabel9.Size = New System.Drawing.Size(216, 28)
        Me.LinkLabel9.TabIndex = 22
        Me.LinkLabel9.TabStop = True
        Me.LinkLabel9.Text = "Provision for Taxation"
        '
        'LinkLabel10
        '
        Me.LinkLabel10.Location = New System.Drawing.Point(14, 111)
        Me.LinkLabel10.Name = "LinkLabel10"
        Me.LinkLabel10.Size = New System.Drawing.Size(212, 38)
        Me.LinkLabel10.TabIndex = 23
        Me.LinkLabel10.TabStop = True
        Me.LinkLabel10.Text = "Creditor Accrued and Other Liabilities"
        Me.LinkLabel10.UseCompatibleTextRendering = True
        '
        'LinkLabel12
        '
        Me.LinkLabel12.AutoSize = True
        Me.LinkLabel12.Location = New System.Drawing.Point(10, 25)
        Me.LinkLabel12.Name = "LinkLabel12"
        Me.LinkLabel12.Size = New System.Drawing.Size(233, 28)
        Me.LinkLabel12.TabIndex = 25
        Me.LinkLabel12.TabStop = True
        Me.LinkLabel12.Text = "Long Term Investments"
        '
        'LinkLabel13
        '
        Me.LinkLabel13.AutoSize = True
        Me.LinkLabel13.Location = New System.Drawing.Point(10, 47)
        Me.LinkLabel13.Name = "LinkLabel13"
        Me.LinkLabel13.Size = New System.Drawing.Size(243, 28)
        Me.LinkLabel13.TabIndex = 26
        Me.LinkLabel13.TabStop = True
        Me.LinkLabel13.Text = "Capital Work In Progress"
        '
        'LinkLabel14
        '
        Me.LinkLabel14.AutoSize = True
        Me.LinkLabel14.Location = New System.Drawing.Point(10, 69)
        Me.LinkLabel14.Name = "LinkLabel14"
        Me.LinkLabel14.Size = New System.Drawing.Size(228, 28)
        Me.LinkLabel14.TabIndex = 27
        Me.LinkLabel14.TabStop = True
        Me.LinkLabel14.Text = "Operating Fixed Assets"
        '
        'LinkLabel16
        '
        Me.LinkLabel16.AutoSize = True
        Me.LinkLabel16.Location = New System.Drawing.Point(10, 71)
        Me.LinkLabel16.Name = "LinkLabel16"
        Me.LinkLabel16.Size = New System.Drawing.Size(145, 28)
        Me.LinkLabel16.TabIndex = 30
        Me.LinkLabel16.TabStop = True
        Me.LinkLabel16.Text = "Trade Debtors"
        '
        'LinkLabel17
        '
        Me.LinkLabel17.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel17.Location = New System.Drawing.Point(436, 373)
        Me.LinkLabel17.Name = "LinkLabel17"
        Me.LinkLabel17.Size = New System.Drawing.Size(229, 22)
        Me.LinkLabel17.TabIndex = 31
        Me.LinkLabel17.TabStop = True
        Me.LinkLabel17.Text = "Stores Spare Parts and Loose Tools"
        Me.LinkLabel17.UseCompatibleTextRendering = True
        '
        'LinkLabel18
        '
        Me.LinkLabel18.AutoSize = True
        Me.LinkLabel18.Location = New System.Drawing.Point(10, 49)
        Me.LinkLabel18.Name = "LinkLabel18"
        Me.LinkLabel18.Size = New System.Drawing.Size(144, 28)
        Me.LinkLabel18.TabIndex = 32
        Me.LinkLabel18.TabStop = True
        Me.LinkLabel18.Text = "Stock In Trade"
        '
        'LinkLabel19
        '
        Me.LinkLabel19.Location = New System.Drawing.Point(13, 94)
        Me.LinkLabel19.Name = "LinkLabel19"
        Me.LinkLabel19.Size = New System.Drawing.Size(189, 36)
        Me.LinkLabel19.TabIndex = 34
        Me.LinkLabel19.TabStop = True
        Me.LinkLabel19.Text = "Loans,Advances,Payments and other Receivables"
        Me.LinkLabel19.UseCompatibleTextRendering = True
        '
        'LinkLabel20
        '
        Me.LinkLabel20.AutoSize = True
        Me.LinkLabel20.Location = New System.Drawing.Point(10, 134)
        Me.LinkLabel20.Name = "LinkLabel20"
        Me.LinkLabel20.Size = New System.Drawing.Size(295, 28)
        Me.LinkLabel20.TabIndex = 35
        Me.LinkLabel20.TabStop = True
        Me.LinkLabel20.Text = "Investment Available For Sale"
        '
        'LinkLabel21
        '
        Me.LinkLabel21.AutoSize = True
        Me.LinkLabel21.Location = New System.Drawing.Point(14, 155)
        Me.LinkLabel21.Name = "LinkLabel21"
        Me.LinkLabel21.Size = New System.Drawing.Size(148, 28)
        Me.LinkLabel21.TabIndex = 36
        Me.LinkLabel21.TabStop = True
        Me.LinkLabel21.Text = "Cash and Bank"
        '
        'LinkLabel23
        '
        Me.LinkLabel23.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel23.Location = New System.Drawing.Point(26, 297)
        Me.LinkLabel23.Name = "LinkLabel23"
        Me.LinkLabel23.Size = New System.Drawing.Size(244, 39)
        Me.LinkLabel23.TabIndex = 38
        Me.LinkLabel23.TabStop = True
        Me.LinkLabel23.Text = "Surplus on Revaluation of Operating Fixed Assets"
        Me.LinkLabel23.UseCompatibleTextRendering = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(14, 700)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(289, 36)
        Me.Label12.TabIndex = 39
        Me.Label12.Text = "Total Capital/Liabilities"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(443, 700)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(159, 36)
        Me.Label13.TabIndex = 40
        Me.Label13.Text = "Total Assets"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(72, 165)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(244, 29)
        Me.Label14.TabIndex = 41
        Me.Label14.Text = "Total Current Liabilities"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(23, 121)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(261, 29)
        Me.Label15.TabIndex = 42
        Me.Label15.Text = "Total Non Current Assets"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.Location = New System.Drawing.Point(18, 314)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(214, 29)
        Me.Label16.TabIndex = 43
        Me.Label16.Text = "Total Current Assets"
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(244, 28)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(130, 19)
        Me.Label17.TabIndex = 44
        Me.Label17.Text = "0"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label18
        '
        Me.Label18.Location = New System.Drawing.Point(244, 72)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(130, 19)
        Me.Label18.TabIndex = 45
        Me.Label18.Text = "0"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label19
        '
        Me.Label19.Location = New System.Drawing.Point(244, 94)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(130, 19)
        Me.Label19.TabIndex = 46
        Me.Label19.Text = "0"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label20
        '
        Me.Label20.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(256, 316)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(130, 19)
        Me.Label20.TabIndex = 47
        Me.Label20.Text = "0"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label21
        '
        Me.Label21.Location = New System.Drawing.Point(244, 27)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(130, 19)
        Me.Label21.TabIndex = 48
        Me.Label21.Text = "0"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label22
        '
        Me.Label22.Location = New System.Drawing.Point(244, 49)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(130, 19)
        Me.Label22.TabIndex = 49
        Me.Label22.Text = "0"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label23
        '
        Me.Label23.Location = New System.Drawing.Point(244, 68)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(130, 19)
        Me.Label23.TabIndex = 50
        Me.Label23.Text = "0"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label24
        '
        Me.Label24.Location = New System.Drawing.Point(244, 33)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(130, 19)
        Me.Label24.TabIndex = 51
        Me.Label24.Text = "0"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label25
        '
        Me.Label25.Location = New System.Drawing.Point(244, 62)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(130, 19)
        Me.Label25.TabIndex = 52
        Me.Label25.Text = "0"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label26
        '
        Me.Label26.Location = New System.Drawing.Point(244, 84)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(130, 19)
        Me.Label26.TabIndex = 53
        Me.Label26.Text = "0"
        Me.Label26.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label27
        '
        Me.Label27.Location = New System.Drawing.Point(244, 114)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(130, 19)
        Me.Label27.TabIndex = 54
        Me.Label27.Text = "0"
        Me.Label27.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label28
        '
        Me.Label28.Location = New System.Drawing.Point(215, 25)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(130, 19)
        Me.Label28.TabIndex = 55
        Me.Label28.Text = "0"
        Me.Label28.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label29
        '
        Me.Label29.Location = New System.Drawing.Point(215, 47)
        Me.Label29.Name = "Label29"
        Me.Label29.Size = New System.Drawing.Size(130, 19)
        Me.Label29.TabIndex = 56
        Me.Label29.Text = "0"
        Me.Label29.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label30
        '
        Me.Label30.Location = New System.Drawing.Point(215, 69)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(130, 19)
        Me.Label30.TabIndex = 57
        Me.Label30.Text = "0"
        Me.Label30.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label31
        '
        Me.Label31.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label31.Location = New System.Drawing.Point(638, 314)
        Me.Label31.Name = "Label31"
        Me.Label31.Size = New System.Drawing.Size(130, 19)
        Me.Label31.TabIndex = 58
        Me.Label31.Text = "0"
        Me.Label31.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label32
        '
        Me.Label32.Location = New System.Drawing.Point(212, 49)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(130, 19)
        Me.Label32.TabIndex = 59
        Me.Label32.Text = "0"
        Me.Label32.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label33
        '
        Me.Label33.Location = New System.Drawing.Point(212, 71)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(130, 19)
        Me.Label33.TabIndex = 60
        Me.Label33.Text = "0"
        Me.Label33.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label34
        '
        Me.Label34.Location = New System.Drawing.Point(212, 103)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(130, 19)
        Me.Label34.TabIndex = 61
        Me.Label34.Text = "0"
        Me.Label34.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label35
        '
        Me.Label35.Location = New System.Drawing.Point(212, 134)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(130, 19)
        Me.Label35.TabIndex = 62
        Me.Label35.Text = "0"
        Me.Label35.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label36
        '
        Me.Label36.Location = New System.Drawing.Point(212, 155)
        Me.Label36.Name = "Label36"
        Me.Label36.Size = New System.Drawing.Size(130, 19)
        Me.Label36.TabIndex = 63
        Me.Label36.Text = "0"
        Me.Label36.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label37
        '
        Me.Label37.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.Location = New System.Drawing.Point(211, 27)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(130, 19)
        Me.Label37.TabIndex = 64
        Me.Label37.Text = "0"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotalNoneCurrentAssets
        '
        Me.lblTotalNoneCurrentAssets.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalNoneCurrentAssets.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalNoneCurrentAssets.ForeColor = System.Drawing.Color.Navy
        Me.lblTotalNoneCurrentAssets.Location = New System.Drawing.Point(215, 120)
        Me.lblTotalNoneCurrentAssets.Name = "lblTotalNoneCurrentAssets"
        Me.lblTotalNoneCurrentAssets.Size = New System.Drawing.Size(130, 19)
        Me.lblTotalNoneCurrentAssets.TabIndex = 65
        Me.lblTotalNoneCurrentAssets.Text = "0"
        Me.lblTotalNoneCurrentAssets.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotalCurrentAssets
        '
        Me.lblTotalCurrentAssets.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalCurrentAssets.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalCurrentAssets.ForeColor = System.Drawing.Color.Navy
        Me.lblTotalCurrentAssets.Location = New System.Drawing.Point(212, 310)
        Me.lblTotalCurrentAssets.Name = "lblTotalCurrentAssets"
        Me.lblTotalCurrentAssets.Size = New System.Drawing.Size(130, 23)
        Me.lblTotalCurrentAssets.TabIndex = 66
        Me.lblTotalCurrentAssets.Text = "0"
        Me.lblTotalCurrentAssets.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotalOwnerEquity
        '
        Me.lblTotalOwnerEquity.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalOwnerEquity.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalOwnerEquity.ForeColor = System.Drawing.Color.Navy
        Me.lblTotalOwnerEquity.Location = New System.Drawing.Point(244, 121)
        Me.lblTotalOwnerEquity.Name = "lblTotalOwnerEquity"
        Me.lblTotalOwnerEquity.Size = New System.Drawing.Size(130, 19)
        Me.lblTotalOwnerEquity.TabIndex = 67
        Me.lblTotalOwnerEquity.Text = "0"
        Me.lblTotalOwnerEquity.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotalNoneLiabilities
        '
        Me.lblTotalNoneLiabilities.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblTotalNoneLiabilities.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalNoneLiabilities.ForeColor = System.Drawing.Color.Navy
        Me.lblTotalNoneLiabilities.Location = New System.Drawing.Point(244, 102)
        Me.lblTotalNoneLiabilities.Name = "lblTotalNoneLiabilities"
        Me.lblTotalNoneLiabilities.Size = New System.Drawing.Size(130, 19)
        Me.lblTotalNoneLiabilities.TabIndex = 68
        Me.lblTotalNoneLiabilities.Text = "0"
        Me.lblTotalNoneLiabilities.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblCurrentLiabilities
        '
        Me.lblCurrentLiabilities.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCurrentLiabilities.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrentLiabilities.ForeColor = System.Drawing.Color.Navy
        Me.lblCurrentLiabilities.Location = New System.Drawing.Point(244, 165)
        Me.lblCurrentLiabilities.Name = "lblCurrentLiabilities"
        Me.lblCurrentLiabilities.Size = New System.Drawing.Size(130, 19)
        Me.lblCurrentLiabilities.TabIndex = 69
        Me.lblCurrentLiabilities.Text = "0"
        Me.lblCurrentLiabilities.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotalCapital
        '
        Me.lblTotalCapital.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalCapital.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblTotalCapital.Location = New System.Drawing.Point(211, 700)
        Me.lblTotalCapital.Name = "lblTotalCapital"
        Me.lblTotalCapital.Size = New System.Drawing.Size(179, 23)
        Me.lblTotalCapital.TabIndex = 70
        Me.lblTotalCapital.Text = "0"
        Me.lblTotalCapital.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblTotalAssets
        '
        Me.lblTotalAssets.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalAssets.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblTotalAssets.Location = New System.Drawing.Point(568, 700)
        Me.lblTotalAssets.Name = "lblTotalAssets"
        Me.lblTotalAssets.Size = New System.Drawing.Size(204, 23)
        Me.lblTotalAssets.TabIndex = 71
        Me.lblTotalAssets.Text = "0"
        Me.lblTotalAssets.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label38
        '
        Me.Label38.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label38.Location = New System.Drawing.Point(12, 116)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(778, 23)
        Me.Label38.TabIndex = 72
        Me.Label38.Text = "_________________________________________________________________________________" & _
    "_______________________________________________"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lnkSharedDeposit)
        Me.GroupBox1.Controls.Add(Me.lblSharedDeposit)
        Me.GroupBox1.Controls.Add(Me.Label17)
        Me.GroupBox1.Controls.Add(Me.LinkLabel1)
        Me.GroupBox1.Controls.Add(Me.LinkLabel2)
        Me.GroupBox1.Controls.Add(Me.LinkLabel3)
        Me.GroupBox1.Controls.Add(Me.lblTotalOwnerEquity)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(15, 147)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(380, 147)
        Me.GroupBox1.TabIndex = 73
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Onwer Equity"
        '
        'lnkSharedDeposit
        '
        Me.lnkSharedDeposit.AutoSize = True
        Me.lnkSharedDeposit.Location = New System.Drawing.Point(11, 52)
        Me.lnkSharedDeposit.Name = "lnkSharedDeposit"
        Me.lnkSharedDeposit.Size = New System.Drawing.Size(157, 28)
        Me.lnkSharedDeposit.TabIndex = 69
        Me.lnkSharedDeposit.TabStop = True
        Me.lnkSharedDeposit.Text = "Shared Deposit"
        '
        'lblSharedDeposit
        '
        Me.lblSharedDeposit.Location = New System.Drawing.Point(244, 50)
        Me.lblSharedDeposit.Name = "lblSharedDeposit"
        Me.lblSharedDeposit.Size = New System.Drawing.Size(130, 19)
        Me.lblSharedDeposit.TabIndex = 68
        Me.lblSharedDeposit.Text = "0"
        Me.lblSharedDeposit.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label28)
        Me.GroupBox2.Controls.Add(Me.LinkLabel12)
        Me.GroupBox2.Controls.Add(Me.LinkLabel13)
        Me.GroupBox2.Controls.Add(Me.LinkLabel14)
        Me.GroupBox2.Controls.Add(Me.Label15)
        Me.GroupBox2.Controls.Add(Me.lblTotalNoneCurrentAssets)
        Me.GroupBox2.Controls.Add(Me.Label29)
        Me.GroupBox2.Controls.Add(Me.Label30)
        Me.GroupBox2.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox2.Location = New System.Drawing.Point(423, 147)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(356, 147)
        Me.GroupBox2.TabIndex = 74
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Non Current Assets"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.LinkLabel4)
        Me.GroupBox3.Controls.Add(Me.LinkLabel5)
        Me.GroupBox3.Controls.Add(Me.LinkLabel6)
        Me.GroupBox3.Controls.Add(Me.lblTotalNoneLiabilities)
        Me.GroupBox3.Controls.Add(Me.Label8)
        Me.GroupBox3.Controls.Add(Me.Label21)
        Me.GroupBox3.Controls.Add(Me.Label22)
        Me.GroupBox3.Controls.Add(Me.Label23)
        Me.GroupBox3.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.Location = New System.Drawing.Point(15, 347)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(380, 127)
        Me.GroupBox3.TabIndex = 75
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Non Current Liabilities"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.LinkLabel7)
        Me.GroupBox4.Controls.Add(Me.LinkLabel8)
        Me.GroupBox4.Controls.Add(Me.lblCurrentLiabilities)
        Me.GroupBox4.Controls.Add(Me.LinkLabel9)
        Me.GroupBox4.Controls.Add(Me.Label14)
        Me.GroupBox4.Controls.Add(Me.LinkLabel10)
        Me.GroupBox4.Controls.Add(Me.Label24)
        Me.GroupBox4.Controls.Add(Me.Label25)
        Me.GroupBox4.Controls.Add(Me.Label26)
        Me.GroupBox4.Controls.Add(Me.Label27)
        Me.GroupBox4.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox4.Location = New System.Drawing.Point(15, 496)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(380, 191)
        Me.GroupBox4.TabIndex = 76
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Current Liabilities"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.lblProgress)
        Me.GroupBox5.Controls.Add(Me.LinkLabel18)
        Me.GroupBox5.Controls.Add(Me.lblTotalCurrentAssets)
        Me.GroupBox5.Controls.Add(Me.LinkLabel16)
        Me.GroupBox5.Controls.Add(Me.Label37)
        Me.GroupBox5.Controls.Add(Me.LinkLabel19)
        Me.GroupBox5.Controls.Add(Me.Label16)
        Me.GroupBox5.Controls.Add(Me.Label32)
        Me.GroupBox5.Controls.Add(Me.Label33)
        Me.GroupBox5.Controls.Add(Me.Label34)
        Me.GroupBox5.Controls.Add(Me.Label35)
        Me.GroupBox5.Controls.Add(Me.Label36)
        Me.GroupBox5.Controls.Add(Me.LinkLabel20)
        Me.GroupBox5.Controls.Add(Me.LinkLabel21)
        Me.GroupBox5.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox5.Location = New System.Drawing.Point(423, 347)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(356, 340)
        Me.GroupBox5.TabIndex = 77
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Current Assets"
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(6, 71)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 79
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.Filter = "Portable Doc Format|*.pdf"
        Me.SaveFileDialog1.Title = "Export File"
        '
        'LinkLabel15
        '
        Me.LinkLabel15.Font = New System.Drawing.Font("Calibri", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel15.Location = New System.Drawing.Point(434, 312)
        Me.LinkLabel15.Name = "LinkLabel15"
        Me.LinkLabel15.Size = New System.Drawing.Size(235, 22)
        Me.LinkLabel15.TabIndex = 78
        Me.LinkLabel15.TabStop = True
        Me.LinkLabel15.Text = "Other Long Term Assents"
        Me.LinkLabel15.UseCompatibleTextRendering = True
        '
        'chkIncludeUnPostedVouchers
        '
        Me.chkIncludeUnPostedVouchers.AutoSize = True
        Me.chkIncludeUnPostedVouchers.Location = New System.Drawing.Point(396, 98)
        Me.chkIncludeUnPostedVouchers.Name = "chkIncludeUnPostedVouchers"
        Me.chkIncludeUnPostedVouchers.Size = New System.Drawing.Size(233, 24)
        Me.chkIncludeUnPostedVouchers.TabIndex = 80
        Me.chkIncludeUnPostedVouchers.Text = "Include Unposted Vouchers"
        Me.chkIncludeUnPostedVouchers.UseVisualStyleBackColor = True
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 33)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(891, 50)
        Me.pnlHeader.TabIndex = 81
        '
        'frmBalanceSheet
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoScroll = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(891, 741)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.chkIncludeUnPostedVouchers)
        Me.Controls.Add(Me.LinkLabel15)
        Me.Controls.Add(Me.LinkLabel17)
        Me.Controls.Add(Me.lblTotalAssets)
        Me.Controls.Add(Me.lblTotalCapital)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.Label31)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.LinkLabel23)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.dtpToDate)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.Label38)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox5)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmBalanceSheet"
        Me.Text = "Balance Sheet"
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
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel3 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel4 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel5 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel6 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel7 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel8 As System.Windows.Forms.LinkLabel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents LinkLabel9 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel10 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel12 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel13 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel14 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel16 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel17 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel18 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel19 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel20 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel21 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel23 As System.Windows.Forms.LinkLabel
    Friend WithEvents PrintToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents Label28 As System.Windows.Forms.Label
    Friend WithEvents Label29 As System.Windows.Forms.Label
    Friend WithEvents Label30 As System.Windows.Forms.Label
    Friend WithEvents Label31 As System.Windows.Forms.Label
    Friend WithEvents Label32 As System.Windows.Forms.Label
    Friend WithEvents Label33 As System.Windows.Forms.Label
    Friend WithEvents Label34 As System.Windows.Forms.Label
    Friend WithEvents Label35 As System.Windows.Forms.Label
    Friend WithEvents Label36 As System.Windows.Forms.Label
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents lblTotalNoneCurrentAssets As System.Windows.Forms.Label
    Friend WithEvents lblTotalCurrentAssets As System.Windows.Forms.Label
    Friend WithEvents lblTotalOwnerEquity As System.Windows.Forms.Label
    Friend WithEvents lblTotalNoneLiabilities As System.Windows.Forms.Label
    Friend WithEvents lblCurrentLiabilities As System.Windows.Forms.Label
    Friend WithEvents lblTotalCapital As System.Windows.Forms.Label
    Friend WithEvents lblTotalAssets As System.Windows.Forms.Label
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents lnkSharedDeposit As System.Windows.Forms.LinkLabel
    Friend WithEvents lblSharedDeposit As System.Windows.Forms.Label
    Friend WithEvents LinkLabel15 As System.Windows.Forms.LinkLabel
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents chkIncludeUnPostedVouchers As System.Windows.Forms.CheckBox
    Friend WithEvents lblCostCentre As System.Windows.Forms.ToolStripLabel
    Friend WithEvents cmbCostCentre As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDealCompletion
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDealCompletion))
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnQuickDealComplete = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnCompleteDeal = New System.Windows.Forms.Button()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblSale = New System.Windows.Forms.Label()
        Me.lblPurchase = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblGrossProfit = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblCommissionAgentDealer = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblProfitShared = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblOtherExpenses = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblTotalExpenses = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblNetProfit = New System.Windows.Forms.Label()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblMargin = New System.Windows.Forms.Label()
        Me.lblCommission = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.lblNDCExpense = New System.Windows.Forms.Label()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel2.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Panel2.Controls.Add(Me.btnQuickDealComplete)
        Me.Panel2.Controls.Add(Me.btnCancel)
        Me.Panel2.Controls.Add(Me.btnCompleteDeal)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 512)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(551, 68)
        Me.Panel2.TabIndex = 22
        '
        'btnQuickDealComplete
        '
        Me.btnQuickDealComplete.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnQuickDealComplete.BackgroundImage = CType(resources.GetObject("btnQuickDealComplete.BackgroundImage"), System.Drawing.Image)
        Me.btnQuickDealComplete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnQuickDealComplete.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnQuickDealComplete.FlatAppearance.BorderSize = 0
        Me.btnQuickDealComplete.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnQuickDealComplete.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnQuickDealComplete.ForeColor = System.Drawing.Color.White
        Me.btnQuickDealComplete.Location = New System.Drawing.Point(323, 8)
        Me.btnQuickDealComplete.Name = "btnQuickDealComplete"
        Me.btnQuickDealComplete.Size = New System.Drawing.Size(104, 53)
        Me.btnQuickDealComplete.TabIndex = 2
        Me.btnQuickDealComplete.Text = "Quick Deal"
        Me.btnQuickDealComplete.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnQuickDealComplete.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackgroundImage = CType(resources.GetObject("btnCancel.BackgroundImage"), System.Drawing.Image)
        Me.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(13, 4)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(54, 62)
        Me.btnCancel.TabIndex = 0
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnCompleteDeal
        '
        Me.btnCompleteDeal.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCompleteDeal.BackgroundImage = CType(resources.GetObject("btnCompleteDeal.BackgroundImage"), System.Drawing.Image)
        Me.btnCompleteDeal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnCompleteDeal.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCompleteDeal.FlatAppearance.BorderSize = 0
        Me.btnCompleteDeal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCompleteDeal.Font = New System.Drawing.Font("Segoe UI Semibold", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCompleteDeal.ForeColor = System.Drawing.Color.White
        Me.btnCompleteDeal.Location = New System.Drawing.Point(424, 8)
        Me.btnCompleteDeal.Name = "btnCompleteDeal"
        Me.btnCompleteDeal.Size = New System.Drawing.Size(104, 53)
        Me.btnCompleteDeal.TabIndex = 1
        Me.btnCompleteDeal.Text = "Complete Deal"
        Me.btnCompleteDeal.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCompleteDeal.UseVisualStyleBackColor = False
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(551, 39)
        Me.pnlHeader.TabIndex = 21
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.White
        Me.lblHeader.Location = New System.Drawing.Point(8, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(160, 25)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Deal Completion"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(24, 85)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 21)
        Me.Label1.TabIndex = 23
        Me.Label1.Text = "Sale"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Panel1.Location = New System.Drawing.Point(28, 108)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(500, 1)
        Me.Panel1.TabIndex = 24
        '
        'lblSale
        '
        Me.lblSale.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSale.AutoSize = True
        Me.lblSale.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSale.Location = New System.Drawing.Point(395, 84)
        Me.lblSale.Name = "lblSale"
        Me.lblSale.Size = New System.Drawing.Size(19, 21)
        Me.lblSale.TabIndex = 25
        Me.lblSale.Text = "0"
        '
        'lblPurchase
        '
        Me.lblPurchase.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPurchase.AutoSize = True
        Me.lblPurchase.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPurchase.Location = New System.Drawing.Point(395, 115)
        Me.lblPurchase.Name = "lblPurchase"
        Me.lblPurchase.Size = New System.Drawing.Size(19, 21)
        Me.lblPurchase.TabIndex = 28
        Me.lblPurchase.Text = "0"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Panel3.Location = New System.Drawing.Point(28, 138)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(500, 1)
        Me.Panel3.TabIndex = 27
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(24, 115)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 21)
        Me.Label4.TabIndex = 26
        Me.Label4.Text = "Purchase"
        '
        'lblGrossProfit
        '
        Me.lblGrossProfit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblGrossProfit.AutoSize = True
        Me.lblGrossProfit.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblGrossProfit.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.lblGrossProfit.Location = New System.Drawing.Point(395, 211)
        Me.lblGrossProfit.Name = "lblGrossProfit"
        Me.lblGrossProfit.Size = New System.Drawing.Size(19, 21)
        Me.lblGrossProfit.TabIndex = 31
        Me.lblGrossProfit.Text = "0"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Panel4.Location = New System.Drawing.Point(28, 235)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(500, 1)
        Me.Panel4.TabIndex = 30
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(24, 212)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(96, 21)
        Me.Label6.TabIndex = 29
        Me.Label6.Text = "Gross Profit"
        '
        'lblCommissionAgentDealer
        '
        Me.lblCommissionAgentDealer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCommissionAgentDealer.AutoSize = True
        Me.lblCommissionAgentDealer.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCommissionAgentDealer.Location = New System.Drawing.Point(395, 272)
        Me.lblCommissionAgentDealer.Name = "lblCommissionAgentDealer"
        Me.lblCommissionAgentDealer.Size = New System.Drawing.Size(19, 21)
        Me.lblCommissionAgentDealer.TabIndex = 34
        Me.lblCommissionAgentDealer.Text = "0"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Panel5.Location = New System.Drawing.Point(28, 295)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(500, 1)
        Me.Panel5.TabIndex = 33
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(24, 251)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(119, 42)
        Me.Label8.TabIndex = 32
        Me.Label8.Text = "Commission" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "(Agent/Dealer)"
        '
        'lblProfitShared
        '
        Me.lblProfitShared.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblProfitShared.AutoSize = True
        Me.lblProfitShared.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProfitShared.Location = New System.Drawing.Point(395, 304)
        Me.lblProfitShared.Name = "lblProfitShared"
        Me.lblProfitShared.Size = New System.Drawing.Size(19, 21)
        Me.lblProfitShared.TabIndex = 37
        Me.lblProfitShared.Text = "0"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Panel6.Location = New System.Drawing.Point(31, 327)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(500, 1)
        Me.Panel6.TabIndex = 36
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(27, 304)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(106, 21)
        Me.Label10.TabIndex = 35
        Me.Label10.Text = "Profit Shared"
        '
        'lblOtherExpenses
        '
        Me.lblOtherExpenses.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblOtherExpenses.AutoSize = True
        Me.lblOtherExpenses.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOtherExpenses.Location = New System.Drawing.Point(395, 332)
        Me.lblOtherExpenses.Name = "lblOtherExpenses"
        Me.lblOtherExpenses.Size = New System.Drawing.Size(19, 21)
        Me.lblOtherExpenses.TabIndex = 40
        Me.lblOtherExpenses.Text = "0"
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Panel7.Location = New System.Drawing.Point(31, 356)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(500, 1)
        Me.Panel7.TabIndex = 39
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(27, 333)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(123, 21)
        Me.Label12.TabIndex = 38
        Me.Label12.Text = "Other Expenses"
        '
        'lblTotalExpenses
        '
        Me.lblTotalExpenses.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTotalExpenses.AutoSize = True
        Me.lblTotalExpenses.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalExpenses.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.lblTotalExpenses.Location = New System.Drawing.Point(395, 412)
        Me.lblTotalExpenses.Name = "lblTotalExpenses"
        Me.lblTotalExpenses.Size = New System.Drawing.Size(19, 21)
        Me.lblTotalExpenses.TabIndex = 43
        Me.lblTotalExpenses.Text = "0"
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Panel8.Location = New System.Drawing.Point(28, 435)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(500, 1)
        Me.Panel8.TabIndex = 42
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label14.Location = New System.Drawing.Point(24, 412)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(116, 21)
        Me.Label14.TabIndex = 41
        Me.Label14.Text = "Total Expenses"
        '
        'lblNetProfit
        '
        Me.lblNetProfit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNetProfit.AutoSize = True
        Me.lblNetProfit.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNetProfit.ForeColor = System.Drawing.Color.DarkGreen
        Me.lblNetProfit.Location = New System.Drawing.Point(394, 467)
        Me.lblNetProfit.Name = "lblNetProfit"
        Me.lblNetProfit.Size = New System.Drawing.Size(23, 25)
        Me.lblNetProfit.TabIndex = 46
        Me.lblNetProfit.Text = "0"
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Panel9.Location = New System.Drawing.Point(28, 494)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(500, 1)
        Me.Panel9.TabIndex = 45
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.DarkGreen
        Me.Label16.Location = New System.Drawing.Point(26, 466)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(100, 25)
        Me.Label16.TabIndex = 44
        Me.Label16.Text = "Net Profit"
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Panel10.Location = New System.Drawing.Point(28, 78)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(500, 1)
        Me.Panel10.TabIndex = 48
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.Location = New System.Drawing.Point(24, 55)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(44, 21)
        Me.lblDate.TabIndex = 47
        Me.lblDate.Text = "Date"
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(399, 55)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(129, 20)
        Me.dtpDate.TabIndex = 49
        '
        'Panel11
        '
        Me.Panel11.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Panel11.Location = New System.Drawing.Point(28, 169)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(500, 1)
        Me.Panel11.TabIndex = 50
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(27, 145)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 21)
        Me.Label2.TabIndex = 51
        Me.Label2.Text = "Margin"
        '
        'lblMargin
        '
        Me.lblMargin.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMargin.AutoSize = True
        Me.lblMargin.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMargin.Location = New System.Drawing.Point(395, 145)
        Me.lblMargin.Name = "lblMargin"
        Me.lblMargin.Size = New System.Drawing.Size(19, 21)
        Me.lblMargin.TabIndex = 52
        Me.lblMargin.Text = "0"
        '
        'lblCommission
        '
        Me.lblCommission.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCommission.AutoSize = True
        Me.lblCommission.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCommission.Location = New System.Drawing.Point(395, 176)
        Me.lblCommission.Name = "lblCommission"
        Me.lblCommission.Size = New System.Drawing.Size(19, 21)
        Me.lblCommission.TabIndex = 55
        Me.lblCommission.Text = "0"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(25, 176)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(85, 21)
        Me.Label7.TabIndex = 54
        Me.Label7.Text = "Comission"
        '
        'Panel12
        '
        Me.Panel12.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Panel12.Location = New System.Drawing.Point(26, 200)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(500, 1)
        Me.Panel12.TabIndex = 53
        '
        'lblNDCExpense
        '
        Me.lblNDCExpense.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNDCExpense.AutoSize = True
        Me.lblNDCExpense.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNDCExpense.Location = New System.Drawing.Point(395, 362)
        Me.lblNDCExpense.Name = "lblNDCExpense"
        Me.lblNDCExpense.Size = New System.Drawing.Size(19, 21)
        Me.lblNDCExpense.TabIndex = 58
        Me.lblNDCExpense.Text = "0"
        '
        'Panel13
        '
        Me.Panel13.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Panel13.Location = New System.Drawing.Point(31, 386)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(500, 1)
        Me.Panel13.TabIndex = 57
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(27, 363)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(114, 21)
        Me.Label11.TabIndex = 56
        Me.Label11.Text = "NDC Expenses"
        '
        'frmDealCompletion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(551, 580)
        Me.Controls.Add(Me.lblNDCExpense)
        Me.Controls.Add(Me.Panel13)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.lblCommission)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Panel12)
        Me.Controls.Add(Me.lblMargin)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel11)
        Me.Controls.Add(Me.dtpDate)
        Me.Controls.Add(Me.Panel10)
        Me.Controls.Add(Me.lblDate)
        Me.Controls.Add(Me.lblNetProfit)
        Me.Controls.Add(Me.Panel9)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.lblTotalExpenses)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.lblOtherExpenses)
        Me.Controls.Add(Me.Panel7)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.lblProfitShared)
        Me.Controls.Add(Me.Panel6)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.lblCommissionAgentDealer)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.lblGrossProfit)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.lblPurchase)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblSale)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDealCompletion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Deal Completion"
        Me.Panel2.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnCompleteDeal As System.Windows.Forms.Button
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblSale As System.Windows.Forms.Label
    Friend WithEvents lblPurchase As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblGrossProfit As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblCommissionAgentDealer As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblProfitShared As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblOtherExpenses As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblTotalExpenses As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblNetProfit As System.Windows.Forms.Label
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnQuickDealComplete As System.Windows.Forms.Button
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblMargin As System.Windows.Forms.Label
    Friend WithEvents lblCommission As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents lblNDCExpense As System.Windows.Forms.Label
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents Label11 As System.Windows.Forms.Label
End Class

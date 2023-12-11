<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSOCashReceipt
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
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn7 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Id")
        Dim UltraGridColumn8 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleDescription")
        Dim UltraGridColumn9 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Size")
        Dim UltraGridColumn10 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Combination")
        Dim UltraGridColumn11 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Price", 0)
        Dim UltraGridColumn12 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("PurchasePrice", 1)
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim grd_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSOCashReceipt))
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtChequeNo = New System.Windows.Forms.TextBox()
        Me.dtChequeDate = New System.Windows.Forms.DateTimePicker()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.cmbCashAccount = New System.Windows.Forms.ComboBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.cmbVoucherType = New System.Windows.Forms.ComboBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtVoucherNo = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtNetAmount = New System.Windows.Forms.TextBox()
        Me.label = New System.Windows.Forms.Label()
        Me.txtCustomer = New System.Windows.Forms.TextBox()
        Me.dtpSODate = New System.Windows.Forms.DateTimePicker()
        Me.txtSONo = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdDataHistory = New Janus.Windows.GridEX.GridEX()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbUnit = New System.Windows.Forms.ComboBox()
        Me.lblitems = New System.Windows.Forms.Label()
        Me.rdoName = New System.Windows.Forms.RadioButton()
        Me.rdoCode = New System.Windows.Forms.RadioButton()
        Me.cmbItem = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblHeading = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.UltraCombo1 = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.cmbType = New System.Windows.Forms.ComboBox()
        Me.cmbLocation = New System.Windows.Forms.ComboBox()
        Me.cmbProject = New System.Windows.Forms.ComboBox()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.txtPrice = New System.Windows.Forms.TextBox()
        Me.txtQty = New System.Windows.Forms.TextBox()
        Me.txtPQty = New System.Windows.Forms.TextBox()
        Me.txtDocNo = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.CutToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsbTask = New System.Windows.Forms.ToolStripButton()
        Me.tsbConfig = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdDataHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraCombo1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.lblProgress)
        Me.UltraTabPageControl1.Controls.Add(Me.Label20)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox1)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbCashAccount)
        Me.UltraTabPageControl1.Controls.Add(Me.Label19)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbVoucherType)
        Me.UltraTabPageControl1.Controls.Add(Me.TextBox2)
        Me.UltraTabPageControl1.Controls.Add(Me.Label17)
        Me.UltraTabPageControl1.Controls.Add(Me.txtVoucherNo)
        Me.UltraTabPageControl1.Controls.Add(Me.Label16)
        Me.UltraTabPageControl1.Controls.Add(Me.txtNetAmount)
        Me.UltraTabPageControl1.Controls.Add(Me.label)
        Me.UltraTabPageControl1.Controls.Add(Me.txtCustomer)
        Me.UltraTabPageControl1.Controls.Add(Me.dtpSODate)
        Me.UltraTabPageControl1.Controls.Add(Me.txtSONo)
        Me.UltraTabPageControl1.Controls.Add(Me.Label18)
        Me.UltraTabPageControl1.Controls.Add(Me.Label22)
        Me.UltraTabPageControl1.Controls.Add(Me.Label26)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(684, 522)
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(212, 210)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 18
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(27, 143)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(66, 13)
        Me.Label20.TabIndex = 13
        Me.Label20.Text = "Deposit In"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtChequeNo)
        Me.GroupBox1.Controls.Add(Me.dtChequeDate)
        Me.GroupBox1.Controls.Add(Me.Label23)
        Me.GroupBox1.Controls.Add(Me.Label24)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 166)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(370, 41)
        Me.GroupBox1.TabIndex = 17
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Bank Information"
        '
        'txtChequeNo
        '
        Me.txtChequeNo.Location = New System.Drawing.Point(113, 12)
        Me.txtChequeNo.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.txtChequeNo.Name = "txtChequeNo"
        Me.txtChequeNo.Size = New System.Drawing.Size(97, 20)
        Me.txtChequeNo.TabIndex = 1
        '
        'dtChequeDate
        '
        Me.dtChequeDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtChequeDate.Location = New System.Drawing.Point(249, 12)
        Me.dtChequeDate.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.dtChequeDate.Name = "dtChequeDate"
        Me.dtChequeDate.Size = New System.Drawing.Size(111, 20)
        Me.dtChequeDate.TabIndex = 3
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.Location = New System.Drawing.Point(5, 16)
        Me.Label23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(100, 13)
        Me.Label23.TabIndex = 0
        Me.Label23.Text = "Cheque Number"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(212, 16)
        Me.Label24.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(34, 13)
        Me.Label24.TabIndex = 2
        Me.Label24.Text = "Date"
        '
        'cmbCashAccount
        '
        Me.cmbCashAccount.BackColor = System.Drawing.SystemColors.Info
        Me.cmbCashAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCashAccount.FormattingEnabled = True
        Me.cmbCashAccount.Location = New System.Drawing.Point(129, 140)
        Me.cmbCashAccount.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbCashAccount.Name = "cmbCashAccount"
        Me.cmbCashAccount.Size = New System.Drawing.Size(247, 21)
        Me.cmbCashAccount.TabIndex = 14
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(27, 116)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(94, 13)
        Me.Label19.TabIndex = 9
        Me.Label19.Text = "Receipt Method"
        '
        'cmbVoucherType
        '
        Me.cmbVoucherType.BackColor = System.Drawing.SystemColors.Info
        Me.cmbVoucherType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbVoucherType.FormattingEnabled = True
        Me.cmbVoucherType.Location = New System.Drawing.Point(129, 113)
        Me.cmbVoucherType.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.cmbVoucherType.Name = "cmbVoucherType"
        Me.cmbVoucherType.Size = New System.Drawing.Size(247, 21)
        Me.cmbVoucherType.TabIndex = 10
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(482, 138)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(140, 20)
        Me.TextBox2.TabIndex = 16
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(392, 141)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(83, 13)
        Me.Label17.TabIndex = 15
        Me.Label17.Text = "Receipt Amount"
        '
        'txtVoucherNo
        '
        Me.txtVoucherNo.Location = New System.Drawing.Point(482, 112)
        Me.txtVoucherNo.Name = "txtVoucherNo"
        Me.txtVoucherNo.ReadOnly = True
        Me.txtVoucherNo.Size = New System.Drawing.Size(140, 20)
        Me.txtVoucherNo.TabIndex = 12
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(392, 115)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(34, 13)
        Me.Label16.TabIndex = 11
        Me.Label16.Text = "V No."
        '
        'txtNetAmount
        '
        Me.txtNetAmount.AcceptsTab = True
        Me.txtNetAmount.Location = New System.Drawing.Point(482, 86)
        Me.txtNetAmount.Name = "txtNetAmount"
        Me.txtNetAmount.ReadOnly = True
        Me.txtNetAmount.Size = New System.Drawing.Size(140, 20)
        Me.txtNetAmount.TabIndex = 8
        '
        'label
        '
        Me.label.AutoSize = True
        Me.label.Location = New System.Drawing.Point(392, 90)
        Me.label.Name = "label"
        Me.label.Size = New System.Drawing.Size(63, 13)
        Me.label.TabIndex = 7
        Me.label.Text = "Net Amount"
        '
        'txtCustomer
        '
        Me.txtCustomer.AcceptsTab = True
        Me.txtCustomer.Location = New System.Drawing.Point(129, 87)
        Me.txtCustomer.Name = "txtCustomer"
        Me.txtCustomer.ReadOnly = True
        Me.txtCustomer.Size = New System.Drawing.Size(247, 20)
        Me.txtCustomer.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.txtCustomer, "Select any customer")
        '
        'dtpSODate
        '
        Me.dtpSODate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpSODate.Enabled = False
        Me.dtpSODate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpSODate.Location = New System.Drawing.Point(482, 60)
        Me.dtpSODate.Name = "dtpSODate"
        Me.dtpSODate.Size = New System.Drawing.Size(140, 20)
        Me.dtpSODate.TabIndex = 4
        '
        'txtSONo
        '
        Me.txtSONo.Location = New System.Drawing.Point(129, 61)
        Me.txtSONo.Name = "txtSONo"
        Me.txtSONo.ReadOnly = True
        Me.txtSONo.Size = New System.Drawing.Size(114, 20)
        Me.txtSONo.TabIndex = 2
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(27, 90)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(51, 13)
        Me.Label18.TabIndex = 5
        Me.Label18.Text = "Customer"
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(27, 64)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(42, 13)
        Me.Label22.TabIndex = 1
        Me.Label22.Text = "SO No."
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Location = New System.Drawing.Point(392, 64)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(48, 13)
        Me.Label26.TabIndex = 3
        Me.Label26.Text = "SO Date"
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(12, 7)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(271, 23)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Sale Order Cash Receipt"
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grdDataHistory)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(684, 522)
        '
        'grdDataHistory
        '
        Me.grdDataHistory.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdDataHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdDataHistory.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdDataHistory.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdDataHistory.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdDataHistory.GroupByBoxVisible = False
        Me.grdDataHistory.Location = New System.Drawing.Point(0, 0)
        Me.grdDataHistory.Name = "grdDataHistory"
        Me.grdDataHistory.RecordNavigator = True
        Me.grdDataHistory.Size = New System.Drawing.Size(684, 522)
        Me.grdDataHistory.TabIndex = 0
        Me.grdDataHistory.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(436, 161)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(26, 13)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "Unit"
        '
        'cmbUnit
        '
        Me.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUnit.FormattingEnabled = True
        Me.cmbUnit.Items.AddRange(New Object() {"Loose", "Pack"})
        Me.cmbUnit.Location = New System.Drawing.Point(439, 177)
        Me.cmbUnit.Name = "cmbUnit"
        Me.cmbUnit.Size = New System.Drawing.Size(75, 21)
        Me.cmbUnit.TabIndex = 27
        '
        'lblitems
        '
        Me.lblitems.AutoSize = True
        Me.lblitems.Location = New System.Drawing.Point(258, 161)
        Me.lblitems.Name = "lblitems"
        Me.lblitems.Size = New System.Drawing.Size(35, 13)
        Me.lblitems.TabIndex = 13
        Me.lblitems.Text = "Items:"
        '
        'rdoName
        '
        Me.rdoName.AutoSize = True
        Me.rdoName.Location = New System.Drawing.Point(382, 159)
        Me.rdoName.Name = "rdoName"
        Me.rdoName.Size = New System.Drawing.Size(53, 17)
        Me.rdoName.TabIndex = 16
        Me.rdoName.Text = "Name"
        Me.rdoName.UseVisualStyleBackColor = True
        '
        'rdoCode
        '
        Me.rdoCode.AutoSize = True
        Me.rdoCode.Checked = True
        Me.rdoCode.Location = New System.Drawing.Point(316, 159)
        Me.rdoCode.Name = "rdoCode"
        Me.rdoCode.Size = New System.Drawing.Size(50, 17)
        Me.rdoCode.TabIndex = 15
        Me.rdoCode.TabStop = True
        Me.rdoCode.Text = "Code"
        Me.rdoCode.UseVisualStyleBackColor = True
        '
        'cmbItem
        '
        Me.cmbItem.CheckedListSettings.CheckStateMember = ""
        Me.cmbItem.Location = New System.Drawing.Point(0, 0)
        Me.cmbItem.Name = "cmbItem"
        Me.cmbItem.Size = New System.Drawing.Size(100, 22)
        Me.cmbItem.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(517, 161)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 17
        Me.Label2.Text = "Pac Qty"
        '
        'lblHeading
        '
        Me.lblHeading.AutoSize = True
        Me.lblHeading.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeading.ForeColor = System.Drawing.Color.Navy
        Me.lblHeading.Location = New System.Drawing.Point(9, 12)
        Me.lblHeading.Name = "lblHeading"
        Me.lblHeading.Size = New System.Drawing.Size(203, 23)
        Me.lblHeading.TabIndex = 0
        Me.lblHeading.Text = "Stock Adjustment"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(436, 161)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(26, 13)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "Unit"
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"Loose", "Pack"})
        Me.ComboBox1.Location = New System.Drawing.Point(439, 177)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(75, 21)
        Me.ComboBox1.TabIndex = 27
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(258, 161)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 13)
        Me.Label4.TabIndex = 13
        Me.Label4.Text = "Items:"
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(382, 159)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(53, 17)
        Me.RadioButton1.TabIndex = 16
        Me.RadioButton1.Text = "Name"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Checked = True
        Me.RadioButton2.Location = New System.Drawing.Point(316, 159)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(50, 17)
        Me.RadioButton2.TabIndex = 15
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "Code"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'UltraCombo1
        '
        Me.UltraCombo1.AlwaysInEditMode = True
        Me.UltraCombo1.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append
        Me.UltraCombo1.CheckedListSettings.CheckStateMember = ""
        Appearance7.BackColor = System.Drawing.Color.White
        Appearance7.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        Me.UltraCombo1.DisplayLayout.Appearance = Appearance7
        UltraGridColumn7.Header.VisiblePosition = 0
        UltraGridColumn7.Hidden = True
        UltraGridColumn8.Header.VisiblePosition = 1
        UltraGridColumn9.Header.VisiblePosition = 2
        UltraGridColumn9.Width = 70
        UltraGridColumn10.Header.VisiblePosition = 3
        UltraGridColumn10.Width = 80
        UltraGridColumn11.Header.VisiblePosition = 4
        UltraGridColumn11.MaxWidth = 80
        UltraGridColumn12.Header.VisiblePosition = 5
        UltraGridColumn12.Hidden = True
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn7, UltraGridColumn8, UltraGridColumn9, UltraGridColumn10, UltraGridColumn11, UltraGridColumn12})
        Me.UltraCombo1.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.UltraCombo1.DisplayLayout.InterBandSpacing = 10
        Me.UltraCombo1.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.UltraCombo1.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.UltraCombo1.DisplayLayout.Override.AllowGroupBy = Infragistics.Win.DefaultableBoolean.[True]
        Me.UltraCombo1.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Appearance8.BackColor = System.Drawing.Color.Transparent
        Me.UltraCombo1.DisplayLayout.Override.CardAreaAppearance = Appearance8
        Me.UltraCombo1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.UltraCombo1.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance9.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance9.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance9.ForeColor = System.Drawing.Color.White
        Appearance9.TextHAlignAsString = "Left"
        Appearance9.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent
        Me.UltraCombo1.DisplayLayout.Override.HeaderAppearance = Appearance9
        Me.UltraCombo1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance10.BorderColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.UltraCombo1.DisplayLayout.Override.RowAppearance = Appearance10
        Appearance11.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance11.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.UltraCombo1.DisplayLayout.Override.RowSelectorAppearance = Appearance11
        Me.UltraCombo1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Me.UltraCombo1.DisplayLayout.Override.RowSelectorWidth = 12
        Me.UltraCombo1.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance12.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance12.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance12.ForeColor = System.Drawing.Color.Black
        Me.UltraCombo1.DisplayLayout.Override.SelectedRowAppearance = Appearance12
        Me.UltraCombo1.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.UltraCombo1.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.UltraCombo1.DisplayLayout.Override.SelectTypeGroupByRow = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.UltraCombo1.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Extended
        Me.UltraCombo1.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.UltraCombo1.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.UltraCombo1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.UltraCombo1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.UltraCombo1.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControlOnLastCell
        Me.UltraCombo1.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.UltraCombo1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.UltraCombo1.LimitToList = True
        Me.UltraCombo1.Location = New System.Drawing.Point(259, 176)
        Me.UltraCombo1.MaxDropDownItems = 20
        Me.UltraCombo1.Name = "UltraCombo1"
        Me.UltraCombo1.Size = New System.Drawing.Size(176, 22)
        Me.UltraCombo1.TabIndex = 14
        '
        'grd
        '
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grd_DesignTimeLayout.LayoutString = resources.GetString("grd_DesignTimeLayout.LayoutString")
        Me.grd.DesignTimeLayout = grd_DesignTimeLayout
        Me.grd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grd.GroupByBoxVisible = False
        Me.grd.Location = New System.Drawing.Point(2, 205)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(861, 249)
        Me.grd.TabIndex = 0
        Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'cmbType
        '
        Me.cmbType.FormattingEnabled = True
        Me.cmbType.Location = New System.Drawing.Point(132, 177)
        Me.cmbType.Name = "cmbType"
        Me.cmbType.Size = New System.Drawing.Size(123, 21)
        Me.cmbType.TabIndex = 12
        '
        'cmbLocation
        '
        Me.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLocation.FormattingEnabled = True
        Me.cmbLocation.Location = New System.Drawing.Point(7, 177)
        Me.cmbLocation.Name = "cmbLocation"
        Me.cmbLocation.Size = New System.Drawing.Size(121, 21)
        Me.cmbLocation.TabIndex = 10
        '
        'cmbProject
        '
        Me.cmbProject.FormattingEnabled = True
        Me.cmbProject.Location = New System.Drawing.Point(75, 77)
        Me.cmbProject.Name = "cmbProject"
        Me.cmbProject.Size = New System.Drawing.Size(318, 21)
        Me.cmbProject.TabIndex = 6
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(259, 51)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(134, 20)
        Me.dtpDate.TabIndex = 4
        '
        'txtRemarks
        '
        Me.txtRemarks.AcceptsTab = True
        Me.txtRemarks.Location = New System.Drawing.Point(75, 104)
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(318, 20)
        Me.txtRemarks.TabIndex = 8
        '
        'txtTotal
        '
        Me.txtTotal.Location = New System.Drawing.Point(693, 177)
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.Size = New System.Drawing.Size(73, 20)
        Me.txtTotal.TabIndex = 25
        Me.txtTotal.TabStop = False
        '
        'txtPrice
        '
        Me.txtPrice.Location = New System.Drawing.Point(637, 177)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.Size = New System.Drawing.Size(52, 20)
        Me.txtPrice.TabIndex = 23
        '
        'txtQty
        '
        Me.txtQty.Location = New System.Drawing.Point(581, 177)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(52, 20)
        Me.txtQty.TabIndex = 20
        '
        'txtPQty
        '
        Me.txtPQty.Location = New System.Drawing.Point(518, 177)
        Me.txtPQty.Name = "txtPQty"
        Me.txtPQty.Size = New System.Drawing.Size(59, 20)
        Me.txtPQty.TabIndex = 18
        Me.txtPQty.TabStop = False
        '
        'txtDocNo
        '
        Me.txtDocNo.Location = New System.Drawing.Point(75, 51)
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.ReadOnly = True
        Me.txtDocNo.Size = New System.Drawing.Size(119, 20)
        Me.txtDocNo.TabIndex = 2
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(770, 176)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(30, 23)
        Me.Button1.TabIndex = 26
        Me.Button1.Text = "+"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(12, 80)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(40, 13)
        Me.Label10.TabIndex = 5
        Me.Label10.Text = "Project"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(130, 161)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(31, 13)
        Me.Label8.TabIndex = 11
        Me.Label8.Text = "Type"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(691, 161)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(31, 13)
        Me.Label11.TabIndex = 24
        Me.Label11.Text = "Total"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(635, 161)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(31, 13)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "Price"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 55)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(44, 13)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "Doc No"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(4, 161)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(48, 13)
        Me.Label7.TabIndex = 9
        Me.Label7.Text = "Location"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 107)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(49, 13)
        Me.Label6.TabIndex = 7
        Me.Label6.Text = "Remarks"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(582, 161)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(23, 13)
        Me.Label12.TabIndex = 19
        Me.Label12.Text = "Qty"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(200, 55)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(53, 13)
        Me.Label13.TabIndex = 3
        Me.Label13.Text = "Doc Date"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(517, 161)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(45, 13)
        Me.Label14.TabIndex = 17
        Me.Label14.Text = "Pac Qty"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Navy
        Me.Label15.Location = New System.Drawing.Point(9, 12)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(203, 23)
        Me.Label15.TabIndex = 0
        Me.Label15.Text = "Stock Adjustment"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.btnPrint, Me.toolStripSeparator, Me.CutToolStripButton, Me.toolStripSeparator1, Me.tsbTask, Me.tsbConfig, Me.ToolStripSeparator2, Me.HelpToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(686, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Enabled = False
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
        'CutToolStripButton
        '
        Me.CutToolStripButton.Image = CType(resources.GetObject("CutToolStripButton.Image"), System.Drawing.Image)
        Me.CutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CutToolStripButton.Name = "CutToolStripButton"
        Me.CutToolStripButton.Size = New System.Drawing.Size(60, 22)
        Me.CutToolStripButton.Text = "D&elete"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 25)
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
        Me.HelpToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.HelpToolStripButton.Text = "He&lp"
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
        Me.UltraTabControl1.Size = New System.Drawing.Size(686, 543)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 1
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "SO Cash Receipt"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "History"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(684, 522)
        '
        'pnlHeader
        '
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(684, 37)
        Me.pnlHeader.TabIndex = 19
        '
        'frmSOCashReceipt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(686, 568)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSOCashReceipt"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Sale Order Cash Receipt"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdDataHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraCombo1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbUnit As System.Windows.Forms.ComboBox
    Friend WithEvents lblitems As System.Windows.Forms.Label
    Friend WithEvents rdoName As System.Windows.Forms.RadioButton
    Friend WithEvents rdoCode As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents lblHeading As System.Windows.Forms.Label
    Friend WithEvents cmbItem As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents UltraCombo1 As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents cmbType As System.Windows.Forms.ComboBox
    Friend WithEvents cmbLocation As System.Windows.Forms.ComboBox
    Friend WithEvents cmbProject As System.Windows.Forms.ComboBox
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents txtTotal As System.Windows.Forms.TextBox
    Friend WithEvents txtPrice As System.Windows.Forms.TextBox
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents txtPQty As System.Windows.Forms.TextBox
    Friend WithEvents txtDocNo As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Private WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CutToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents dtpSODate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtSONo As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Private WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grdDataHistory As Janus.Windows.GridEX.GridEX
    Friend WithEvents txtCustomer As System.Windows.Forms.TextBox
    Friend WithEvents txtNetAmount As System.Windows.Forms.TextBox
    Friend WithEvents label As System.Windows.Forms.Label
    Friend WithEvents txtVoucherNo As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents cmbVoucherType As System.Windows.Forms.ComboBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtChequeNo As System.Windows.Forms.TextBox
    Friend WithEvents dtChequeDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents cmbCashAccount As System.Windows.Forms.ComboBox
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents tsbTask As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbConfig As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

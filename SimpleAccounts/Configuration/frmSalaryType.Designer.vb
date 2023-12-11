<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSalaryType
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSalaryType))
        Dim grdSaved_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.txtSalaryType = New System.Windows.Forms.TextBox()
        Me.chkDeduction = New System.Windows.Forms.CheckBox()
        Me.chkActive = New System.Windows.Forms.CheckBox()
        Me.txtSortOrder = New System.Windows.Forms.TextBox()
        Me.lblSalariesType = New System.Windows.Forms.Label()
        Me.lblSortOrder = New System.Windows.Forms.Label()
        Me.grdSaved = New Janus.Windows.GridEX.GridEX()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.txtAccountDescription = New System.Windows.Forms.TextBox()
        Me.chkReceiveableAccount = New System.Windows.Forms.CheckBox()
        Me.ChkIncomeTaxExempted = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.cmbAccountHead = New System.Windows.Forms.ComboBox()
        Me.lblAccountHead = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.optSiteVisitAllowance = New System.Windows.Forms.RadioButton()
        Me.rbtnNone = New System.Windows.Forms.RadioButton()
        Me.OptGrossSalaryType = New System.Windows.Forms.RadioButton()
        Me.OptAllowanceAgainstOverTime = New System.Windows.Forms.RadioButton()
        Me.OptDeductionAgaistLeaves = New System.Windows.Forms.RadioButton()
        Me.OptIncomeTaxDed = New System.Windows.Forms.RadioButton()
        Me.OptDeductionAgaistSalary = New System.Windows.Forms.RadioButton()
        Me.rbtExistingAccount = New System.Windows.Forms.RadioButton()
        Me.rbtCreateAccount = New System.Windows.Forms.RadioButton()
        Me.cmbApplyValue = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.btnPrint, Me.toolStripSeparator, Me.btnDelete, Me.toolStripSeparator1, Me.HelpToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1270, 32)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(75, 29)
        Me.btnNew.Text = "&New"
        '
        'btnEdit
        '
        Me.btnEdit.Image = CType(resources.GetObject("btnEdit.Image"), System.Drawing.Image)
        Me.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(70, 29)
        Me.btnEdit.Text = "&Edit"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(77, 29)
        Me.btnSave.Text = "&Save"
        '
        'btnPrint
        '
        Me.btnPrint.Enabled = False
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(76, 29)
        Me.btnPrint.Text = "&Print"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 32)
        '
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(90, 29)
        Me.btnDelete.Text = "D&elete"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 32)
        '
        'HelpToolStripButton
        '
        Me.HelpToolStripButton.Enabled = False
        Me.HelpToolStripButton.Image = CType(resources.GetObject("HelpToolStripButton.Image"), System.Drawing.Image)
        Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton.Name = "HelpToolStripButton"
        Me.HelpToolStripButton.Size = New System.Drawing.Size(77, 29)
        Me.HelpToolStripButton.Text = "He&lp"
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(22, 11)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(176, 36)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Salary Type"
        '
        'txtSalaryType
        '
        Me.txtSalaryType.Location = New System.Drawing.Point(141, 111)
        Me.txtSalaryType.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtSalaryType.MaxLength = 50
        Me.txtSalaryType.Name = "txtSalaryType"
        Me.txtSalaryType.Size = New System.Drawing.Size(390, 26)
        Me.txtSalaryType.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.txtSalaryType, "Salary Type")
        '
        'chkDeduction
        '
        Me.chkDeduction.AutoSize = True
        Me.chkDeduction.Location = New System.Drawing.Point(141, 268)
        Me.chkDeduction.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkDeduction.Name = "chkDeduction"
        Me.chkDeduction.Size = New System.Drawing.Size(108, 24)
        Me.chkDeduction.TabIndex = 13
        Me.chkDeduction.Text = "Deduction"
        Me.ToolTip1.SetToolTip(Me.chkDeduction, "Salary Deduction")
        Me.chkDeduction.UseVisualStyleBackColor = True
        '
        'chkActive
        '
        Me.chkActive.AutoSize = True
        Me.chkActive.Checked = True
        Me.chkActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkActive.Location = New System.Drawing.Point(141, 415)
        Me.chkActive.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(78, 24)
        Me.chkActive.TabIndex = 18
        Me.chkActive.Text = "Active"
        Me.ToolTip1.SetToolTip(Me.chkActive, "Salary Type Status Active Or Inactive")
        Me.chkActive.UseVisualStyleBackColor = True
        '
        'txtSortOrder
        '
        Me.txtSortOrder.Location = New System.Drawing.Point(141, 375)
        Me.txtSortOrder.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtSortOrder.Name = "txtSortOrder"
        Me.txtSortOrder.Size = New System.Drawing.Size(82, 26)
        Me.txtSortOrder.TabIndex = 17
        Me.txtSortOrder.Text = "1"
        Me.ToolTip1.SetToolTip(Me.txtSortOrder, "Sort Order")
        '
        'lblSalariesType
        '
        Me.lblSalariesType.AutoSize = True
        Me.lblSalariesType.Location = New System.Drawing.Point(18, 117)
        Me.lblSalariesType.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSalariesType.Name = "lblSalariesType"
        Me.lblSalariesType.Size = New System.Drawing.Size(91, 20)
        Me.lblSalariesType.TabIndex = 5
        Me.lblSalariesType.Text = "Salary Type"
        '
        'lblSortOrder
        '
        Me.lblSortOrder.AutoSize = True
        Me.lblSortOrder.Location = New System.Drawing.Point(18, 380)
        Me.lblSortOrder.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSortOrder.Name = "lblSortOrder"
        Me.lblSortOrder.Size = New System.Drawing.Size(83, 20)
        Me.lblSortOrder.TabIndex = 16
        Me.lblSortOrder.Text = "Sort Order"
        '
        'grdSaved
        '
        Me.grdSaved.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        grdSaved_DesignTimeLayout.LayoutString = resources.GetString("grdSaved_DesignTimeLayout.LayoutString")
        Me.grdSaved.DesignTimeLayout = grdSaved_DesignTimeLayout
        Me.grdSaved.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.grdSaved.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdSaved.GroupByBoxVisible = False
        Me.grdSaved.Location = New System.Drawing.Point(0, 451)
        Me.grdSaved.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdSaved.Name = "grdSaved"
        Me.grdSaved.RecordNavigator = True
        Me.grdSaved.Size = New System.Drawing.Size(1270, 426)
        Me.grdSaved.TabIndex = 21
        Me.grdSaved.TabStop = False
        Me.ToolTip1.SetToolTip(Me.grdSaved, "Define Salary Types")
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'btnAdd
        '
        Me.btnAdd.Image = Global.SimpleAccounts.My.Resources.Resources.pin_black
        Me.btnAdd.Location = New System.Drawing.Point(1084, 114)
        Me.btnAdd.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(51, 35)
        Me.btnAdd.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.btnAdd, "Opens Accounts")
        Me.btnAdd.UseVisualStyleBackColor = True
        Me.btnAdd.Visible = False
        '
        'txtAccountDescription
        '
        Me.txtAccountDescription.Enabled = False
        Me.txtAccountDescription.Location = New System.Drawing.Point(884, 117)
        Me.txtAccountDescription.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtAccountDescription.Name = "txtAccountDescription"
        Me.txtAccountDescription.Size = New System.Drawing.Size(190, 26)
        Me.txtAccountDescription.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtAccountDescription, "Payable Account Map")
        Me.txtAccountDescription.Visible = False
        '
        'chkReceiveableAccount
        '
        Me.chkReceiveableAccount.AutoSize = True
        Me.chkReceiveableAccount.Location = New System.Drawing.Point(141, 303)
        Me.chkReceiveableAccount.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkReceiveableAccount.Name = "chkReceiveableAccount"
        Me.chkReceiveableAccount.Size = New System.Drawing.Size(260, 25)
        Me.chkReceiveableAccount.TabIndex = 14
        Me.chkReceiveableAccount.Text = "Account Mapping By Employee"
        Me.chkReceiveableAccount.ThreeState = True
        Me.ToolTip1.SetToolTip(Me.chkReceiveableAccount, "This link salary type with employee receivable account")
        Me.chkReceiveableAccount.UseCompatibleTextRendering = True
        Me.chkReceiveableAccount.UseVisualStyleBackColor = True
        '
        'ChkIncomeTaxExempted
        '
        Me.ChkIncomeTaxExempted.AutoSize = True
        Me.ChkIncomeTaxExempted.Location = New System.Drawing.Point(141, 340)
        Me.ChkIncomeTaxExempted.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ChkIncomeTaxExempted.Name = "ChkIncomeTaxExempted"
        Me.ChkIncomeTaxExempted.Size = New System.Drawing.Size(193, 24)
        Me.ChkIncomeTaxExempted.TabIndex = 15
        Me.ChkIncomeTaxExempted.Text = "Income Tax Exempted"
        Me.ToolTip1.SetToolTip(Me.ChkIncomeTaxExempted, "Income Tax Exempted")
        Me.ChkIncomeTaxExempted.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(804, 122)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(68, 20)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Account"
        Me.Label1.Visible = False
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(448, 568)
        Me.lblProgress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(394, 69)
        Me.lblProgress.TabIndex = 0
        Me.lblProgress.Tag = " "
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'cmbAccountHead
        '
        Me.cmbAccountHead.BackColor = System.Drawing.Color.White
        Me.cmbAccountHead.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAccountHead.FormattingEnabled = True
        Me.cmbAccountHead.Location = New System.Drawing.Point(141, 226)
        Me.cmbAccountHead.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbAccountHead.Name = "cmbAccountHead"
        Me.cmbAccountHead.Size = New System.Drawing.Size(390, 28)
        Me.cmbAccountHead.TabIndex = 12
        '
        'lblAccountHead
        '
        Me.lblAccountHead.AutoSize = True
        Me.lblAccountHead.Location = New System.Drawing.Point(18, 232)
        Me.lblAccountHead.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAccountHead.Name = "lblAccountHead"
        Me.lblAccountHead.Size = New System.Drawing.Size(111, 20)
        Me.lblAccountHead.TabIndex = 11
        Me.lblAccountHead.Text = "Account Head"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.optSiteVisitAllowance)
        Me.GroupBox1.Controls.Add(Me.rbtnNone)
        Me.GroupBox1.Controls.Add(Me.OptGrossSalaryType)
        Me.GroupBox1.Controls.Add(Me.OptAllowanceAgainstOverTime)
        Me.GroupBox1.Controls.Add(Me.OptDeductionAgaistLeaves)
        Me.GroupBox1.Controls.Add(Me.OptIncomeTaxDed)
        Me.GroupBox1.Controls.Add(Me.OptDeductionAgaistSalary)
        Me.GroupBox1.Location = New System.Drawing.Point(561, 111)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(218, 289)
        Me.GroupBox1.TabIndex = 20
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Option"
        '
        'optSiteVisitAllowance
        '
        Me.optSiteVisitAllowance.AutoSize = True
        Me.optSiteVisitAllowance.Location = New System.Drawing.Point(16, 245)
        Me.optSiteVisitAllowance.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.optSiteVisitAllowance.Name = "optSiteVisitAllowance"
        Me.optSiteVisitAllowance.Size = New System.Drawing.Size(172, 24)
        Me.optSiteVisitAllowance.TabIndex = 6
        Me.optSiteVisitAllowance.Text = "Site Visit Allowance"
        Me.optSiteVisitAllowance.UseVisualStyleBackColor = True
        '
        'rbtnNone
        '
        Me.rbtnNone.AutoSize = True
        Me.rbtnNone.Checked = True
        Me.rbtnNone.Location = New System.Drawing.Point(16, 29)
        Me.rbtnNone.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtnNone.Name = "rbtnNone"
        Me.rbtnNone.Size = New System.Drawing.Size(72, 24)
        Me.rbtnNone.TabIndex = 0
        Me.rbtnNone.TabStop = True
        Me.rbtnNone.Text = "None"
        Me.rbtnNone.UseVisualStyleBackColor = True
        '
        'OptGrossSalaryType
        '
        Me.OptGrossSalaryType.AutoSize = True
        Me.OptGrossSalaryType.Location = New System.Drawing.Point(16, 65)
        Me.OptGrossSalaryType.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.OptGrossSalaryType.Name = "OptGrossSalaryType"
        Me.OptGrossSalaryType.Size = New System.Drawing.Size(121, 24)
        Me.OptGrossSalaryType.TabIndex = 1
        Me.OptGrossSalaryType.Text = "Basic Salary"
        Me.OptGrossSalaryType.UseVisualStyleBackColor = True
        '
        'OptAllowanceAgainstOverTime
        '
        Me.OptAllowanceAgainstOverTime.AutoSize = True
        Me.OptAllowanceAgainstOverTime.Location = New System.Drawing.Point(16, 209)
        Me.OptAllowanceAgainstOverTime.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.OptAllowanceAgainstOverTime.Name = "OptAllowanceAgainstOverTime"
        Me.OptAllowanceAgainstOverTime.Size = New System.Drawing.Size(181, 24)
        Me.OptAllowanceAgainstOverTime.TabIndex = 5
        Me.OptAllowanceAgainstOverTime.Text = "Over Time Allowance"
        Me.OptAllowanceAgainstOverTime.UseVisualStyleBackColor = True
        '
        'OptDeductionAgaistLeaves
        '
        Me.OptDeductionAgaistLeaves.AutoSize = True
        Me.OptDeductionAgaistLeaves.Location = New System.Drawing.Point(16, 174)
        Me.OptDeductionAgaistLeaves.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.OptDeductionAgaistLeaves.Name = "OptDeductionAgaistLeaves"
        Me.OptDeductionAgaistLeaves.Size = New System.Drawing.Size(154, 24)
        Me.OptDeductionAgaistLeaves.TabIndex = 4
        Me.OptDeductionAgaistLeaves.Text = "Leave Deduction"
        Me.OptDeductionAgaistLeaves.UseVisualStyleBackColor = True
        '
        'OptIncomeTaxDed
        '
        Me.OptIncomeTaxDed.AutoSize = True
        Me.OptIncomeTaxDed.Location = New System.Drawing.Point(16, 100)
        Me.OptIncomeTaxDed.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.OptIncomeTaxDed.Name = "OptIncomeTaxDed"
        Me.OptIncomeTaxDed.Size = New System.Drawing.Size(193, 24)
        Me.OptIncomeTaxDed.TabIndex = 2
        Me.OptIncomeTaxDed.Text = "Income Tax Deduction"
        Me.OptIncomeTaxDed.UseVisualStyleBackColor = True
        '
        'OptDeductionAgaistSalary
        '
        Me.OptDeductionAgaistSalary.AutoSize = True
        Me.OptDeductionAgaistSalary.Location = New System.Drawing.Point(16, 138)
        Me.OptDeductionAgaistSalary.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.OptDeductionAgaistSalary.Name = "OptDeductionAgaistSalary"
        Me.OptDeductionAgaistSalary.Size = New System.Drawing.Size(147, 24)
        Me.OptDeductionAgaistSalary.TabIndex = 3
        Me.OptDeductionAgaistSalary.Text = "Loan Deduction"
        Me.OptDeductionAgaistSalary.UseVisualStyleBackColor = True
        '
        'rbtExistingAccount
        '
        Me.rbtExistingAccount.AutoSize = True
        Me.rbtExistingAccount.Location = New System.Drawing.Point(141, 194)
        Me.rbtExistingAccount.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtExistingAccount.Name = "rbtExistingAccount"
        Me.rbtExistingAccount.Size = New System.Drawing.Size(152, 24)
        Me.rbtExistingAccount.TabIndex = 9
        Me.rbtExistingAccount.Text = "Existing Account"
        Me.rbtExistingAccount.UseVisualStyleBackColor = True
        '
        'rbtCreateAccount
        '
        Me.rbtCreateAccount.AutoSize = True
        Me.rbtCreateAccount.Checked = True
        Me.rbtCreateAccount.Location = New System.Drawing.Point(306, 194)
        Me.rbtCreateAccount.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtCreateAccount.Name = "rbtCreateAccount"
        Me.rbtCreateAccount.Size = New System.Drawing.Size(145, 24)
        Me.rbtCreateAccount.TabIndex = 10
        Me.rbtCreateAccount.TabStop = True
        Me.rbtCreateAccount.Text = "Create Account"
        Me.rbtCreateAccount.UseVisualStyleBackColor = True
        '
        'cmbApplyValue
        '
        Me.cmbApplyValue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbApplyValue.FormattingEnabled = True
        Me.cmbApplyValue.Items.AddRange(New Object() {"Fixed", "Percentage", "Variable"})
        Me.cmbApplyValue.Location = New System.Drawing.Point(141, 151)
        Me.cmbApplyValue.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbApplyValue.Name = "cmbApplyValue"
        Me.cmbApplyValue.Size = New System.Drawing.Size(154, 28)
        Me.cmbApplyValue.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 155)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 20)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Apply Value"
        '
        'pnlHeader
        '
        Me.pnlHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Location = New System.Drawing.Point(3, 43)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1268, 58)
        Me.pnlHeader.TabIndex = 22
        '
        'frmSalaryType
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1270, 877)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbApplyValue)
        Me.Controls.Add(Me.rbtCreateAccount)
        Me.Controls.Add(Me.rbtExistingAccount)
        Me.Controls.Add(Me.chkDeduction)
        Me.Controls.Add(Me.ChkIncomeTaxExempted)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lblAccountHead)
        Me.Controls.Add(Me.cmbAccountHead)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.chkReceiveableAccount)
        Me.Controls.Add(Me.grdSaved)
        Me.Controls.Add(Me.lblSortOrder)
        Me.Controls.Add(Me.lblSalariesType)
        Me.Controls.Add(Me.txtSortOrder)
        Me.Controls.Add(Me.chkActive)
        Me.Controls.Add(Me.txtSalaryType)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.txtAccountDescription)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSalaryType"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Salary Type"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents txtSalaryType As System.Windows.Forms.TextBox
    Friend WithEvents chkDeduction As System.Windows.Forms.CheckBox
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents txtSortOrder As System.Windows.Forms.TextBox
    Friend WithEvents lblSalariesType As System.Windows.Forms.Label
    Friend WithEvents lblSortOrder As System.Windows.Forms.Label
    Friend WithEvents grdSaved As Janus.Windows.GridEX.GridEX
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents txtAccountDescription As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkReceiveableAccount As System.Windows.Forms.CheckBox
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents cmbAccountHead As System.Windows.Forms.ComboBox
    Friend WithEvents lblAccountHead As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ChkIncomeTaxExempted As System.Windows.Forms.CheckBox
    Friend WithEvents OptDeductionAgaistLeaves As System.Windows.Forms.RadioButton
    Friend WithEvents OptDeductionAgaistSalary As System.Windows.Forms.RadioButton
    Friend WithEvents OptIncomeTaxDed As System.Windows.Forms.RadioButton
    Friend WithEvents OptAllowanceAgainstOverTime As System.Windows.Forms.RadioButton
    Friend WithEvents OptGrossSalaryType As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnNone As System.Windows.Forms.RadioButton
    Friend WithEvents rbtExistingAccount As System.Windows.Forms.RadioButton
    Friend WithEvents rbtCreateAccount As System.Windows.Forms.RadioButton
    Friend WithEvents cmbApplyValue As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents optSiteVisitAllowance As System.Windows.Forms.RadioButton
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPOSConfiguration
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
        Dim grdCreditDetail_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grd_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.grdCreditDetail = New Janus.Windows.GridEX.GridEX()
        Me.lblCreditCardAccount = New System.Windows.Forms.Label()
        Me.txtMachineNo = New System.Windows.Forms.TextBox()
        Me.cmbCreditCardAccount = New System.Windows.Forms.ComboBox()
        Me.lblMapAccount = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.cmbCompany = New System.Windows.Forms.ComboBox()
        Me.cmbLocation = New System.Windows.Forms.ComboBox()
        Me.cmbCostCenter = New System.Windows.Forms.ComboBox()
        Me.cmbCashAccount = New System.Windows.Forms.ComboBox()
        Me.lblCompany = New System.Windows.Forms.Label()
        Me.lblLocation = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.chkDeliveryOption = New System.Windows.Forms.CheckBox()
        Me.lblDeliveryOption = New System.Windows.Forms.Label()
        Me.lblPOSTitle = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.lblActive = New System.Windows.Forms.Label()
        Me.lblDiscountPer = New System.Windows.Forms.Label()
        Me.txtDiscountPer = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkActive = New System.Windows.Forms.CheckBox()
        Me.txtPOSTitle = New System.Windows.Forms.TextBox()
        Me.lblSalesPerson = New System.Windows.Forms.Label()
        Me.lblBankAccount = New System.Windows.Forms.Label()
        Me.cmbSalesPerson = New System.Windows.Forms.ComboBox()
        Me.cmbBankAccount = New System.Windows.Forms.ComboBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        CType(Me.grdCreditDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(525, 264)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(34, 23)
        Me.btnAdd.TabIndex = 17
        Me.btnAdd.Text = "+"
        Me.ToolTip1.SetToolTip(Me.btnAdd, "Add Data in Upper Grid")
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'grdCreditDetail
        '
        Me.grdCreditDetail.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdCreditDetail.ColumnAutoResize = True
        grdCreditDetail_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grdCreditDetail.DesignTimeLayout = grdCreditDetail_DesignTimeLayout
        Me.grdCreditDetail.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdCreditDetail.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdCreditDetail.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.grdCreditDetail.GroupByBoxVisible = False
        Me.grdCreditDetail.Location = New System.Drawing.Point(565, 23)
        Me.grdCreditDetail.Name = "grdCreditDetail"
        Me.grdCreditDetail.RecordNavigator = True
        Me.grdCreditDetail.Size = New System.Drawing.Size(401, 208)
        Me.grdCreditDetail.TabIndex = 22
        Me.grdCreditDetail.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'lblCreditCardAccount
        '
        Me.lblCreditCardAccount.AutoSize = True
        Me.lblCreditCardAccount.Location = New System.Drawing.Point(26, 269)
        Me.lblCreditCardAccount.Name = "lblCreditCardAccount"
        Me.lblCreditCardAccount.Size = New System.Drawing.Size(79, 13)
        Me.lblCreditCardAccount.TabIndex = 13
        Me.lblCreditCardAccount.Text = "Machine Name"
        '
        'txtMachineNo
        '
        Me.txtMachineNo.Location = New System.Drawing.Point(173, 266)
        Me.txtMachineNo.Name = "txtMachineNo"
        Me.txtMachineNo.Size = New System.Drawing.Size(135, 20)
        Me.txtMachineNo.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.txtMachineNo, "Enter Machine Name")
        '
        'cmbCreditCardAccount
        '
        Me.cmbCreditCardAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCreditCardAccount.FormattingEnabled = True
        Me.cmbCreditCardAccount.Location = New System.Drawing.Point(422, 266)
        Me.cmbCreditCardAccount.Name = "cmbCreditCardAccount"
        Me.cmbCreditCardAccount.Size = New System.Drawing.Size(97, 21)
        Me.cmbCreditCardAccount.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.cmbCreditCardAccount, "Select A Bank Account to associate that Machine Name")
        '
        'lblMapAccount
        '
        Me.lblMapAccount.AutoSize = True
        Me.lblMapAccount.Location = New System.Drawing.Point(314, 269)
        Me.lblMapAccount.Name = "lblMapAccount"
        Me.lblMapAccount.Size = New System.Drawing.Size(102, 13)
        Me.lblMapAccount.TabIndex = 15
        Me.lblMapAccount.Text = "Credit Card Account"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnRefresh, Me.btnSave})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(995, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = Global.SimpleAccounts.My.Resources.Resources.BtnNew_Image
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(51, 22)
        Me.btnNew.Text = "&New"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(66, 22)
        Me.btnRefresh.Text = "&Refresh"
        '
        'btnSave
        '
        Me.btnSave.Image = Global.SimpleAccounts.My.Resources.Resources.BtnSave_Image
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(51, 22)
        Me.btnSave.Text = "&Save"
        '
        'cmbCompany
        '
        Me.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCompany.FormattingEnabled = True
        Me.cmbCompany.Location = New System.Drawing.Point(173, 52)
        Me.cmbCompany.Name = "cmbCompany"
        Me.cmbCompany.Size = New System.Drawing.Size(385, 21)
        Me.cmbCompany.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.cmbCompany, "Select a Company")
        '
        'cmbLocation
        '
        Me.cmbLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLocation.FormattingEnabled = True
        Me.cmbLocation.Location = New System.Drawing.Point(173, 85)
        Me.cmbLocation.Name = "cmbLocation"
        Me.cmbLocation.Size = New System.Drawing.Size(385, 21)
        Me.cmbLocation.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.cmbLocation, "Select a Location")
        '
        'cmbCostCenter
        '
        Me.cmbCostCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCostCenter.FormattingEnabled = True
        Me.cmbCostCenter.Location = New System.Drawing.Point(173, 117)
        Me.cmbCostCenter.Name = "cmbCostCenter"
        Me.cmbCostCenter.Size = New System.Drawing.Size(385, 21)
        Me.cmbCostCenter.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.cmbCostCenter, "Select a Cost Center")
        '
        'cmbCashAccount
        '
        Me.cmbCashAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCashAccount.FormattingEnabled = True
        Me.cmbCashAccount.Location = New System.Drawing.Point(173, 146)
        Me.cmbCashAccount.Name = "cmbCashAccount"
        Me.cmbCashAccount.Size = New System.Drawing.Size(385, 21)
        Me.cmbCashAccount.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.cmbCashAccount, "Select a Cash Account")
        '
        'lblCompany
        '
        Me.lblCompany.AutoSize = True
        Me.lblCompany.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.lblCompany.Location = New System.Drawing.Point(26, 59)
        Me.lblCompany.Name = "lblCompany"
        Me.lblCompany.Size = New System.Drawing.Size(51, 13)
        Me.lblCompany.TabIndex = 3
        Me.lblCompany.Text = "Company"
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = True
        Me.lblLocation.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.lblLocation.Location = New System.Drawing.Point(26, 88)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(48, 13)
        Me.lblLocation.TabIndex = 5
        Me.lblLocation.Text = "Location"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(26, 120)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(62, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Cost Center"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(26, 149)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(74, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Cash Account"
        '
        'chkDeliveryOption
        '
        Me.chkDeliveryOption.AutoSize = True
        Me.chkDeliveryOption.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.chkDeliveryOption.Location = New System.Drawing.Point(173, 296)
        Me.chkDeliveryOption.Name = "chkDeliveryOption"
        Me.chkDeliveryOption.Size = New System.Drawing.Size(15, 14)
        Me.chkDeliveryOption.TabIndex = 19
        Me.chkDeliveryOption.UseVisualStyleBackColor = False
        '
        'lblDeliveryOption
        '
        Me.lblDeliveryOption.AutoSize = True
        Me.lblDeliveryOption.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.lblDeliveryOption.Location = New System.Drawing.Point(26, 296)
        Me.lblDeliveryOption.Name = "lblDeliveryOption"
        Me.lblDeliveryOption.Size = New System.Drawing.Size(124, 13)
        Me.lblDeliveryOption.TabIndex = 18
        Me.lblDeliveryOption.Text = "Delivery Option On Save"
        '
        'lblPOSTitle
        '
        Me.lblPOSTitle.AutoSize = True
        Me.lblPOSTitle.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.lblPOSTitle.Location = New System.Drawing.Point(26, 26)
        Me.lblPOSTitle.Name = "lblPOSTitle"
        Me.lblPOSTitle.Size = New System.Drawing.Size(52, 13)
        Me.lblPOSTitle.TabIndex = 0
        Me.lblPOSTitle.Text = "POS Title"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel1.Controls.Add(Me.grd)
        Me.Panel1.Controls.Add(Me.grdCreditDetail)
        Me.Panel1.Controls.Add(Me.btnAdd)
        Me.Panel1.Controls.Add(Me.lblDeliveryOption)
        Me.Panel1.Controls.Add(Me.lblActive)
        Me.Panel1.Controls.Add(Me.lblDiscountPer)
        Me.Panel1.Controls.Add(Me.txtDiscountPer)
        Me.Panel1.Controls.Add(Me.lblCreditCardAccount)
        Me.Panel1.Controls.Add(Me.txtMachineNo)
        Me.Panel1.Controls.Add(Me.cmbCreditCardAccount)
        Me.Panel1.Controls.Add(Me.chkDeliveryOption)
        Me.Panel1.Controls.Add(Me.lblMapAccount)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.chkActive)
        Me.Panel1.Controls.Add(Me.lblPOSTitle)
        Me.Panel1.Controls.Add(Me.txtPOSTitle)
        Me.Panel1.Controls.Add(Me.lblSalesPerson)
        Me.Panel1.Controls.Add(Me.lblBankAccount)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.lblCompany)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.cmbCompany)
        Me.Panel1.Controls.Add(Me.lblLocation)
        Me.Panel1.Controls.Add(Me.cmbSalesPerson)
        Me.Panel1.Controls.Add(Me.cmbLocation)
        Me.Panel1.Controls.Add(Me.cmbBankAccount)
        Me.Panel1.Controls.Add(Me.cmbCostCenter)
        Me.Panel1.Controls.Add(Me.cmbCashAccount)
        Me.Panel1.Location = New System.Drawing.Point(0, 68)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(996, 519)
        Me.Panel1.TabIndex = 0
        '
        'grd
        '
        Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grd.ColumnAutoResize = True
        grd_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grd.DesignTimeLayout = grd_DesignTimeLayout
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.grd.GroupByBoxVisible = False
        Me.grd.Location = New System.Drawing.Point(0, 316)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(996, 201)
        Me.grd.TabIndex = 23
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'lblActive
        '
        Me.lblActive.AutoSize = True
        Me.lblActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.lblActive.Location = New System.Drawing.Point(500, 295)
        Me.lblActive.Name = "lblActive"
        Me.lblActive.Size = New System.Drawing.Size(37, 13)
        Me.lblActive.TabIndex = 20
        Me.lblActive.Text = "Active"
        '
        'lblDiscountPer
        '
        Me.lblDiscountPer.AutoSize = True
        Me.lblDiscountPer.Location = New System.Drawing.Point(25, 239)
        Me.lblDiscountPer.Name = "lblDiscountPer"
        Me.lblDiscountPer.Size = New System.Drawing.Size(60, 13)
        Me.lblDiscountPer.TabIndex = 13
        Me.lblDiscountPer.Text = "Discount %"
        '
        'txtDiscountPer
        '
        Me.txtDiscountPer.Location = New System.Drawing.Point(172, 236)
        Me.txtDiscountPer.Name = "txtDiscountPer"
        Me.txtDiscountPer.Size = New System.Drawing.Size(386, 20)
        Me.txtDiscountPer.TabIndex = 14
        Me.txtDiscountPer.Text = "0"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(76, 25)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(11, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "*"
        '
        'chkActive
        '
        Me.chkActive.AutoSize = True
        Me.chkActive.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.chkActive.Checked = True
        Me.chkActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkActive.Location = New System.Drawing.Point(543, 295)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(15, 14)
        Me.chkActive.TabIndex = 21
        Me.chkActive.UseVisualStyleBackColor = False
        '
        'txtPOSTitle
        '
        Me.txtPOSTitle.Location = New System.Drawing.Point(173, 23)
        Me.txtPOSTitle.Name = "txtPOSTitle"
        Me.txtPOSTitle.Size = New System.Drawing.Size(385, 20)
        Me.txtPOSTitle.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.txtPOSTitle, "POS Title")
        '
        'lblSalesPerson
        '
        Me.lblSalesPerson.AutoSize = True
        Me.lblSalesPerson.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.lblSalesPerson.Location = New System.Drawing.Point(25, 209)
        Me.lblSalesPerson.Name = "lblSalesPerson"
        Me.lblSalesPerson.Size = New System.Drawing.Size(69, 13)
        Me.lblSalesPerson.TabIndex = 11
        Me.lblSalesPerson.Text = "Sales Person"
        '
        'lblBankAccount
        '
        Me.lblBankAccount.AutoSize = True
        Me.lblBankAccount.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.lblBankAccount.Location = New System.Drawing.Point(26, 181)
        Me.lblBankAccount.Name = "lblBankAccount"
        Me.lblBankAccount.Size = New System.Drawing.Size(75, 13)
        Me.lblBankAccount.TabIndex = 11
        Me.lblBankAccount.Text = "Bank Account"
        '
        'cmbSalesPerson
        '
        Me.cmbSalesPerson.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSalesPerson.FormattingEnabled = True
        Me.cmbSalesPerson.Location = New System.Drawing.Point(172, 206)
        Me.cmbSalesPerson.Name = "cmbSalesPerson"
        Me.cmbSalesPerson.Size = New System.Drawing.Size(385, 21)
        Me.cmbSalesPerson.TabIndex = 12
        '
        'cmbBankAccount
        '
        Me.cmbBankAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBankAccount.FormattingEnabled = True
        Me.cmbBankAccount.Location = New System.Drawing.Point(173, 178)
        Me.cmbBankAccount.Name = "cmbBankAccount"
        Me.cmbBankAccount.Size = New System.Drawing.Size(385, 21)
        Me.cmbBankAccount.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.cmbBankAccount, "Select a Bank Account")
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 25)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(995, 42)
        Me.pnlHeader.TabIndex = 3
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.lblHeader.Location = New System.Drawing.Point(24, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(209, 23)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "POS Configuration"
        '
        'frmPOSConfiguration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(995, 585)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmPOSConfiguration"
        Me.Text = "POS Configuration"
        CType(Me.grdCreditDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbCompany As System.Windows.Forms.ComboBox
    Friend WithEvents cmbLocation As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCostCenter As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCashAccount As System.Windows.Forms.ComboBox
    Friend WithEvents lblCompany As System.Windows.Forms.Label
    Friend WithEvents lblLocation As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkDeliveryOption As System.Windows.Forms.CheckBox
    Friend WithEvents lblDeliveryOption As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblPOSTitle As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents txtPOSTitle As System.Windows.Forms.TextBox
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents lblActive As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtMachineNo As System.Windows.Forms.TextBox
    Friend WithEvents lblMapAccount As System.Windows.Forms.Label
    Friend WithEvents lblCreditCardAccount As System.Windows.Forms.Label
    Friend WithEvents lblBankAccount As System.Windows.Forms.Label
    Friend WithEvents cmbCreditCardAccount As System.Windows.Forms.ComboBox
    Friend WithEvents cmbBankAccount As System.Windows.Forms.ComboBox
    Friend WithEvents grdCreditDetail As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblSalesPerson As System.Windows.Forms.Label
    Friend WithEvents cmbSalesPerson As System.Windows.Forms.ComboBox
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents lblDiscountPer As System.Windows.Forms.Label
    Friend WithEvents txtDiscountPer As System.Windows.Forms.TextBox
End Class

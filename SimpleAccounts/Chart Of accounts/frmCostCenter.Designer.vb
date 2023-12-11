<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCostCenter
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
        Dim GrdCostCenter_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCostCenter))
        Me.GrdCostCenter = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.BtnNew = New System.Windows.Forms.ToolStripButton()
        Me.BtnEdit = New System.Windows.Forms.ToolStripButton()
        Me.BtnSave = New System.Windows.Forms.ToolStripButton()
        Me.BtnDelete = New System.Windows.Forms.ToolStripButton()
        Me.BtnPrint = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.txtID = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtName = New System.Windows.Forms.TextBox()
        Me.TxtCode = New System.Windows.Forms.TextBox()
        Me.TxtSortOrder = New System.Windows.Forms.TextBox()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbHead = New System.Windows.Forms.ComboBox()
        Me.chkActive = New System.Windows.Forms.CheckBox()
        Me.chkOuwardgatepass = New System.Windows.Forms.CheckBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cbLogical = New System.Windows.Forms.CheckBox()
        Me.chkShift = New System.Windows.Forms.CheckBox()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.chkSalariedBudget = New System.Windows.Forms.CheckBox()
        Me.chkDepartmentBudget = New System.Windows.Forms.CheckBox()
        Me.chkSOBudget = New System.Windows.Forms.CheckBox()
        Me.chkContract = New System.Windows.Forms.CheckBox()
        Me.chkPurchaseDemand = New System.Windows.Forms.CheckBox()
        Me.txtRemainingAmount = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        CType(Me.GrdCostCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'GrdCostCenter
        '
        Me.GrdCostCenter.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        GrdCostCenter_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.GrdCostCenter.DesignTimeLayout = GrdCostCenter_DesignTimeLayout
        Me.GrdCostCenter.GroupByBoxVisible = False
        Me.GrdCostCenter.Location = New System.Drawing.Point(1, 289)
        Me.GrdCostCenter.Margin = New System.Windows.Forms.Padding(4)
        Me.GrdCostCenter.Name = "GrdCostCenter"
        Me.GrdCostCenter.RecordNavigator = True
        Me.GrdCostCenter.Size = New System.Drawing.Size(720, 265)
        Me.GrdCostCenter.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.GrdCostCenter, "Define Cost Center")
        Me.GrdCostCenter.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BtnNew, Me.BtnEdit, Me.BtnSave, Me.BtnDelete, Me.BtnPrint, Me.toolStripSeparator1, Me.HelpToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(723, 31)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'BtnNew
        '
        Me.BtnNew.Image = CType(resources.GetObject("BtnNew.Image"), System.Drawing.Image)
        Me.BtnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnNew.Name = "BtnNew"
        Me.BtnNew.Size = New System.Drawing.Size(67, 28)
        Me.BtnNew.Text = "&New"
        Me.BtnNew.ToolTipText = "Reset All Controls"
        '
        'BtnEdit
        '
        Me.BtnEdit.Image = CType(resources.GetObject("BtnEdit.Image"), System.Drawing.Image)
        Me.BtnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnEdit.Name = "BtnEdit"
        Me.BtnEdit.Size = New System.Drawing.Size(63, 28)
        Me.BtnEdit.Text = "&Edit"
        Me.BtnEdit.ToolTipText = "Change Record"
        '
        'BtnSave
        '
        Me.BtnSave.Image = CType(resources.GetObject("BtnSave.Image"), System.Drawing.Image)
        Me.BtnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(68, 28)
        Me.BtnSave.Text = "&Save"
        Me.BtnSave.ToolTipText = "Save Or Update Record"
        '
        'BtnDelete
        '
        Me.BtnDelete.Image = CType(resources.GetObject("BtnDelete.Image"), System.Drawing.Image)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.RightToLeftAutoMirrorImage = True
        Me.BtnDelete.Size = New System.Drawing.Size(81, 28)
        Me.BtnDelete.Text = "&Delete"
        Me.BtnDelete.ToolTipText = "Delete Record"
        '
        'BtnPrint
        '
        Me.BtnPrint.Image = CType(resources.GetObject("BtnPrint.Image"), System.Drawing.Image)
        Me.BtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(67, 28)
        Me.BtnPrint.Text = "&Print"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 31)
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
        'txtID
        '
        Me.txtID.Location = New System.Drawing.Point(287, 255)
        Me.txtID.Margin = New System.Windows.Forms.Padding(4)
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(49, 29)
        Me.txtID.TabIndex = 11
        Me.txtID.TabStop = False
        Me.txtID.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(24, 97)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 23)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Name"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(23, 129)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 23)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Code"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(24, 259)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(90, 23)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Sort Order"
        '
        'TxtName
        '
        Me.TxtName.Location = New System.Drawing.Point(129, 94)
        Me.TxtName.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtName.Name = "TxtName"
        Me.TxtName.Size = New System.Drawing.Size(206, 29)
        Me.TxtName.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.TxtName, "Cost Center Or Project Name")
        '
        'TxtCode
        '
        Me.TxtCode.Location = New System.Drawing.Point(129, 126)
        Me.TxtCode.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtCode.Name = "TxtCode"
        Me.TxtCode.Size = New System.Drawing.Size(206, 29)
        Me.TxtCode.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.TxtCode, "Cost Center Code")
        '
        'TxtSortOrder
        '
        Me.TxtSortOrder.Location = New System.Drawing.Point(129, 255)
        Me.TxtSortOrder.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtSortOrder.Name = "TxtSortOrder"
        Me.TxtSortOrder.Size = New System.Drawing.Size(64, 29)
        Me.TxtSortOrder.TabIndex = 9
        Me.TxtSortOrder.Text = "1"
        Me.ToolTip1.SetToolTip(Me.TxtSortOrder, "Sort Order")
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(10, 9)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(168, 35)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Cost Center"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(24, 160)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 23)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Head"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(258, 259)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(27, 23)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "ID"
        Me.Label5.Visible = False
        '
        'cmbHead
        '
        Me.cmbHead.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbHead.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbHead.FormattingEnabled = True
        Me.cmbHead.Location = New System.Drawing.Point(130, 157)
        Me.cmbHead.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbHead.Name = "cmbHead"
        Me.cmbHead.Size = New System.Drawing.Size(206, 29)
        Me.cmbHead.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.cmbHead, "Cost Center Head")
        '
        'chkActive
        '
        Me.chkActive.AutoSize = True
        Me.chkActive.Checked = True
        Me.chkActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkActive.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.chkActive.Location = New System.Drawing.Point(351, 203)
        Me.chkActive.Margin = New System.Windows.Forms.Padding(4)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(78, 27)
        Me.chkActive.TabIndex = 12
        Me.chkActive.Text = "Active"
        Me.ToolTip1.SetToolTip(Me.chkActive, "Cost Center Status Active Or Inactive")
        Me.chkActive.UseVisualStyleBackColor = True
        '
        'chkOuwardgatepass
        '
        Me.chkOuwardgatepass.AutoSize = True
        Me.chkOuwardgatepass.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.chkOuwardgatepass.Location = New System.Drawing.Point(484, 204)
        Me.chkOuwardgatepass.Margin = New System.Windows.Forms.Padding(4)
        Me.chkOuwardgatepass.Name = "chkOuwardgatepass"
        Me.chkOuwardgatepass.Size = New System.Drawing.Size(172, 27)
        Me.chkOuwardgatepass.TabIndex = 13
        Me.chkOuwardgatepass.Text = "Outward Gatepass"
        Me.ToolTip1.SetToolTip(Me.chkOuwardgatepass, "For Print Outward Gatepass In Store Issuence")
        Me.chkOuwardgatepass.UseVisualStyleBackColor = True
        '
        'cbLogical
        '
        Me.cbLogical.AutoSize = True
        Me.cbLogical.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.cbLogical.Location = New System.Drawing.Point(351, 232)
        Me.cbLogical.Margin = New System.Windows.Forms.Padding(4)
        Me.cbLogical.Name = "cbLogical"
        Me.cbLogical.Size = New System.Drawing.Size(85, 27)
        Me.cbLogical.TabIndex = 14
        Me.cbLogical.Text = "Logical"
        Me.ToolTip1.SetToolTip(Me.cbLogical, "Logical Cost Center")
        Me.cbLogical.UseVisualStyleBackColor = True
        '
        'chkShift
        '
        Me.chkShift.AutoSize = True
        Me.chkShift.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.chkShift.Location = New System.Drawing.Point(484, 232)
        Me.chkShift.Margin = New System.Windows.Forms.Padding(4)
        Me.chkShift.Name = "chkShift"
        Me.chkShift.Size = New System.Drawing.Size(160, 27)
        Me.chkShift.TabIndex = 15
        Me.chkShift.Text = "Day / Night Shift"
        Me.chkShift.UseVisualStyleBackColor = True
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(256, 410)
        Me.lblProgress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(307, 59)
        Me.lblProgress.TabIndex = 17
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 31)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(723, 50)
        Me.pnlHeader.TabIndex = 1
        '
        'txtAmount
        '
        Me.txtAmount.Location = New System.Drawing.Point(129, 189)
        Me.txtAmount.Margin = New System.Windows.Forms.Padding(4)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(206, 29)
        Me.txtAmount.TabIndex = 11
        Me.txtAmount.TabStop = False
        Me.txtAmount.Text = "0"
        Me.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(23, 192)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(65, 23)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Budget"
        '
        'chkSalariedBudget
        '
        Me.chkSalariedBudget.AutoSize = True
        Me.chkSalariedBudget.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.chkSalariedBudget.Location = New System.Drawing.Point(351, 173)
        Me.chkSalariedBudget.Margin = New System.Windows.Forms.Padding(4)
        Me.chkSalariedBudget.Name = "chkSalariedBudget"
        Me.chkSalariedBudget.Size = New System.Drawing.Size(137, 27)
        Me.chkSalariedBudget.TabIndex = 12
        Me.chkSalariedBudget.Text = "Salary Budget"
        Me.chkSalariedBudget.UseVisualStyleBackColor = True
        '
        'chkDepartmentBudget
        '
        Me.chkDepartmentBudget.AutoSize = True
        Me.chkDepartmentBudget.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.chkDepartmentBudget.Location = New System.Drawing.Point(484, 173)
        Me.chkDepartmentBudget.Margin = New System.Windows.Forms.Padding(4)
        Me.chkDepartmentBudget.Name = "chkDepartmentBudget"
        Me.chkDepartmentBudget.Size = New System.Drawing.Size(127, 27)
        Me.chkDepartmentBudget.TabIndex = 12
        Me.chkDepartmentBudget.Text = "Dep. Budget"
        Me.chkDepartmentBudget.UseVisualStyleBackColor = True
        '
        'chkSOBudget
        '
        Me.chkSOBudget.AutoSize = True
        Me.chkSOBudget.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.chkSOBudget.Location = New System.Drawing.Point(351, 144)
        Me.chkSOBudget.Margin = New System.Windows.Forms.Padding(4)
        Me.chkSOBudget.Name = "chkSOBudget"
        Me.chkSOBudget.Size = New System.Drawing.Size(114, 27)
        Me.chkSOBudget.TabIndex = 12
        Me.chkSOBudget.Text = "SO Budget"
        Me.chkSOBudget.UseVisualStyleBackColor = True
        '
        'chkContract
        '
        Me.chkContract.AutoSize = True
        Me.chkContract.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.chkContract.Location = New System.Drawing.Point(484, 144)
        Me.chkContract.Margin = New System.Windows.Forms.Padding(4)
        Me.chkContract.Name = "chkContract"
        Me.chkContract.Size = New System.Drawing.Size(98, 27)
        Me.chkContract.TabIndex = 12
        Me.chkContract.Text = "Contract"
        Me.chkContract.UseVisualStyleBackColor = True
        '
        'chkPurchaseDemand
        '
        Me.chkPurchaseDemand.AutoSize = True
        Me.chkPurchaseDemand.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.chkPurchaseDemand.Location = New System.Drawing.Point(351, 261)
        Me.chkPurchaseDemand.Margin = New System.Windows.Forms.Padding(4)
        Me.chkPurchaseDemand.Name = "chkPurchaseDemand"
        Me.chkPurchaseDemand.Size = New System.Drawing.Size(171, 27)
        Me.chkPurchaseDemand.TabIndex = 12
        Me.chkPurchaseDemand.Text = "Purchase Demand"
        Me.chkPurchaseDemand.UseVisualStyleBackColor = True
        '
        'txtRemainingAmount
        '
        Me.txtRemainingAmount.Location = New System.Drawing.Point(130, 223)
        Me.txtRemainingAmount.Margin = New System.Windows.Forms.Padding(4)
        Me.txtRemainingAmount.Name = "txtRemainingAmount"
        Me.txtRemainingAmount.Size = New System.Drawing.Size(206, 29)
        Me.txtRemainingAmount.TabIndex = 11
        Me.txtRemainingAmount.TabStop = False
        Me.txtRemainingAmount.Text = "0"
        Me.txtRemainingAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(23, 229)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(84, 23)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "R. Budget"
        '
        'frmCostCenter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 21.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(723, 556)
        Me.Controls.Add(Me.cbLogical)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.chkShift)
        Me.Controls.Add(Me.chkOuwardgatepass)
        Me.Controls.Add(Me.chkDepartmentBudget)
        Me.Controls.Add(Me.chkPurchaseDemand)
        Me.Controls.Add(Me.chkContract)
        Me.Controls.Add(Me.chkSOBudget)
        Me.Controls.Add(Me.chkSalariedBudget)
        Me.Controls.Add(Me.chkActive)
        Me.Controls.Add(Me.cmbHead)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtSortOrder)
        Me.Controls.Add(Me.TxtCode)
        Me.Controls.Add(Me.TxtName)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtRemainingAmount)
        Me.Controls.Add(Me.txtAmount)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtID)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.GrdCostCenter)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmCostCenter"
        Me.Text = "Cost Center"
        CType(Me.GrdCostCenter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GrdCostCenter As Janus.Windows.GridEX.GridEX
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents BtnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtName As System.Windows.Forms.TextBox
    Friend WithEvents TxtCode As System.Windows.Forms.TextBox
    Friend WithEvents TxtSortOrder As System.Windows.Forms.TextBox
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbHead As System.Windows.Forms.ComboBox
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents chkOuwardgatepass As System.Windows.Forms.CheckBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents chkShift As System.Windows.Forms.CheckBox
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents cbLogical As System.Windows.Forms.CheckBox
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents chkSalariedBudget As System.Windows.Forms.CheckBox
    Friend WithEvents chkDepartmentBudget As System.Windows.Forms.CheckBox
    Friend WithEvents chkSOBudget As System.Windows.Forms.CheckBox
    Friend WithEvents chkContract As System.Windows.Forms.CheckBox
    Friend WithEvents chkPurchaseDemand As System.Windows.Forms.CheckBox
    Friend WithEvents txtRemainingAmount As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
End Class

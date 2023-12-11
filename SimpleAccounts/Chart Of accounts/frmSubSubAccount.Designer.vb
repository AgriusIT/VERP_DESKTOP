<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSubSubAccount
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSubSubAccount))
        Dim DataGridView1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtCode = New System.Windows.Forms.TextBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.BtnNew = New System.Windows.Forms.ToolStripButton()
        Me.BtnEdit = New System.Windows.Forms.ToolStripButton()
        Me.BtnSave = New System.Windows.Forms.ToolStripButton()
        Me.BtnDelete = New System.Windows.Forms.ToolStripButton()
        Me.BtnPrint = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripDropDownButton1 = New System.Windows.Forms.ToolStripDropDownButton()
        Me.ChartsOfAccountToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.CashFlowStatmentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StanderCashFlowStatementToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtMainCode = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.uicmbType = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.cmbPLNotes = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboBSNotes = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.DataGridView1 = New Janus.Windows.GridEX.GridEX()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmbCompany = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(288, 157)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(14, 20)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "-"
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(312, 152)
        Me.txtCode.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtCode.MaxLength = 3
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(350, 26)
        Me.txtCode.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.txtCode, "Sub Sub Account Code")
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BtnNew, Me.BtnEdit, Me.BtnSave, Me.BtnDelete, Me.BtnPrint, Me.toolStripSeparator1, Me.HelpToolStripButton, Me.ToolStripDropDownButton1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1016, 32)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'BtnNew
        '
        Me.BtnNew.Image = CType(resources.GetObject("BtnNew.Image"), System.Drawing.Image)
        Me.BtnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnNew.Name = "BtnNew"
        Me.BtnNew.Size = New System.Drawing.Size(75, 29)
        Me.BtnNew.Text = "&New"
        '
        'BtnEdit
        '
        Me.BtnEdit.Image = CType(resources.GetObject("BtnEdit.Image"), System.Drawing.Image)
        Me.BtnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnEdit.Name = "BtnEdit"
        Me.BtnEdit.Size = New System.Drawing.Size(70, 29)
        Me.BtnEdit.Text = "&Edit"
        '
        'BtnSave
        '
        Me.BtnSave.Image = CType(resources.GetObject("BtnSave.Image"), System.Drawing.Image)
        Me.BtnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(77, 29)
        Me.BtnSave.Text = "&Save"
        '
        'BtnDelete
        '
        Me.BtnDelete.Image = CType(resources.GetObject("BtnDelete.Image"), System.Drawing.Image)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.RightToLeftAutoMirrorImage = True
        Me.BtnDelete.Size = New System.Drawing.Size(90, 29)
        Me.BtnDelete.Text = "&Delete"
        '
        'BtnPrint
        '
        Me.BtnPrint.Image = CType(resources.GetObject("BtnPrint.Image"), System.Drawing.Image)
        Me.BtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(76, 29)
        Me.BtnPrint.Text = "&Print"
        Me.BtnPrint.ToolTipText = "Print Trial Balance Report On Sub Sub Level"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 32)
        '
        'HelpToolStripButton
        '
        Me.HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HelpToolStripButton.Image = CType(resources.GetObject("HelpToolStripButton.Image"), System.Drawing.Image)
        Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton.Name = "HelpToolStripButton"
        Me.HelpToolStripButton.Size = New System.Drawing.Size(28, 29)
        Me.HelpToolStripButton.Text = "He&lp"
        '
        'ToolStripDropDownButton1
        '
        Me.ToolStripDropDownButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripDropDownButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ChartsOfAccountToolStripMenuItem1, Me.CashFlowStatmentToolStripMenuItem, Me.StanderCashFlowStatementToolStripMenuItem1})
        Me.ToolStripDropDownButton1.Image = Global.SimpleAccounts.My.Resources.Resources._27_Edit_Text_32x32
        Me.ToolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripDropDownButton1.Name = "ToolStripDropDownButton1"
        Me.ToolStripDropDownButton1.Size = New System.Drawing.Size(115, 29)
        Me.ToolStripDropDownButton1.Text = "Reports"
        '
        'ChartsOfAccountToolStripMenuItem1
        '
        Me.ChartsOfAccountToolStripMenuItem1.Name = "ChartsOfAccountToolStripMenuItem1"
        Me.ChartsOfAccountToolStripMenuItem1.Size = New System.Drawing.Size(325, 30)
        Me.ChartsOfAccountToolStripMenuItem1.Text = "Charts of Account"
        '
        'CashFlowStatmentToolStripMenuItem
        '
        Me.CashFlowStatmentToolStripMenuItem.Name = "CashFlowStatmentToolStripMenuItem"
        Me.CashFlowStatmentToolStripMenuItem.Size = New System.Drawing.Size(325, 30)
        Me.CashFlowStatmentToolStripMenuItem.Text = "Cash  & Bank Statment "
        '
        'StanderCashFlowStatementToolStripMenuItem1
        '
        Me.StanderCashFlowStatementToolStripMenuItem1.Name = "StanderCashFlowStatementToolStripMenuItem1"
        Me.StanderCashFlowStatementToolStripMenuItem1.Size = New System.Drawing.Size(325, 30)
        Me.StanderCashFlowStatementToolStripMenuItem1.Text = "Stander Cash Flow statement"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 197)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(143, 20)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Sub SubA/C Name"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(176, 192)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TextBox2.MaxLength = 50
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(487, 26)
        Me.TextBox2.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.TextBox2, "Sub Sub Account Title")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 157)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(143, 20)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Sub Sub A/C Code"
        '
        'txtMainCode
        '
        Me.txtMainCode.BackColor = System.Drawing.SystemColors.Window
        Me.txtMainCode.Location = New System.Drawing.Point(176, 152)
        Me.txtMainCode.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtMainCode.MaxLength = 6
        Me.txtMainCode.Name = "txtMainCode"
        Me.txtMainCode.ReadOnly = True
        Me.txtMainCode.Size = New System.Drawing.Size(102, 26)
        Me.txtMainCode.TabIndex = 6
        Me.txtMainCode.TabStop = False
        Me.ToolTip1.SetToolTip(Me.txtMainCode, "Sub Account Code")
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(674, 115)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(76, 20)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "(Refresh)"
        '
        'ComboBox1
        '
        Me.ComboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.ComboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(176, 111)
        Me.ComboBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(487, 28)
        Me.ComboBox1.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.ComboBox1, "Select Sub Account")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 115)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 20)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Sub A/C"
        '
        'uicmbType
        '
        Me.uicmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.uicmbType.FormattingEnabled = True
        Me.uicmbType.Items.AddRange(New Object() {"General", "Cash", "Bank", "Customer", "Vendor", "Expense", "Inventory", "Customer Service", "LC", "Clearing Agent", "Travel Agent"})
        Me.uicmbType.Location = New System.Drawing.Point(176, 274)
        Me.uicmbType.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.uicmbType.Name = "uicmbType"
        Me.uicmbType.Size = New System.Drawing.Size(487, 28)
        Me.uicmbType.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.uicmbType, "Select Account Type ")
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(18, 278)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(73, 20)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "A/C Type"
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(6, 12)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(251, 36)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Sub Sub Account"
        '
        'cmbPLNotes
        '
        Me.cmbPLNotes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPLNotes.FormattingEnabled = True
        Me.cmbPLNotes.Items.AddRange(New Object() {"General", "Cash", "Bank", "Customer", "Vendor"})
        Me.cmbPLNotes.Location = New System.Drawing.Point(176, 315)
        Me.cmbPLNotes.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbPLNotes.Name = "cmbPLNotes"
        Me.cmbPLNotes.Size = New System.Drawing.Size(487, 28)
        Me.cmbPLNotes.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.cmbPLNotes, "Select Profit & Loss Note")
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(18, 320)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(74, 20)
        Me.Label7.TabIndex = 15
        Me.Label7.Text = "PL Notes"
        '
        'cboBSNotes
        '
        Me.cboBSNotes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBSNotes.FormattingEnabled = True
        Me.cboBSNotes.Items.AddRange(New Object() {"General", "Cash", "Bank", "Customer", "Vendor"})
        Me.cboBSNotes.Location = New System.Drawing.Point(176, 357)
        Me.cboBSNotes.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cboBSNotes.Name = "cboBSNotes"
        Me.cboBSNotes.Size = New System.Drawing.Size(487, 28)
        Me.cboBSNotes.TabIndex = 18
        Me.ToolTip1.SetToolTip(Me.cboBSNotes, "Select Balance Sheet Note")
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(18, 362)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(77, 20)
        Me.Label8.TabIndex = 17
        Me.Label8.Text = "BS Notes"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DataGridView1_DesignTimeLayout.LayoutString = resources.GetString("DataGridView1_DesignTimeLayout.LayoutString")
        Me.DataGridView1.DesignTimeLayout = DataGridView1_DesignTimeLayout
        Me.DataGridView1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.DataGridView1.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.DataGridView1.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.DataGridView1.GroupByBoxVisible = False
        Me.DataGridView1.Location = New System.Drawing.Point(2, 445)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RecordNavigator = True
        Me.DataGridView1.Size = New System.Drawing.Size(1012, 298)
        Me.DataGridView1.TabIndex = 19
        Me.ToolTip1.SetToolTip(Me.DataGridView1, "Define Sub Sub Accounts")
        Me.DataGridView1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'cmbCompany
        '
        Me.cmbCompany.BackColor = System.Drawing.Color.White
        Me.cmbCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCompany.FormattingEnabled = True
        Me.cmbCompany.Items.AddRange(New Object() {"General", "Cash", "Bank", "Customer", "Vendor", "Expense", "Inventory"})
        Me.cmbCompany.Location = New System.Drawing.Point(176, 232)
        Me.cmbCompany.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbCompany.Name = "cmbCompany"
        Me.cmbCompany.Size = New System.Drawing.Size(487, 28)
        Me.cmbCompany.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.cmbCompany, "Select Account Type ")
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(18, 237)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(76, 20)
        Me.Label9.TabIndex = 11
        Me.Label9.Text = "Company"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label10.ForeColor = System.Drawing.Color.Black
        Me.Label10.Location = New System.Drawing.Point(681, 237)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(76, 20)
        Me.Label10.TabIndex = 20
        Me.Label10.Text = "(Refresh)"
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(310, 338)
        Me.lblProgress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(394, 69)
        Me.lblProgress.TabIndex = 21
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 32)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1016, 58)
        Me.pnlHeader.TabIndex = 22
        '
        'frmSubSubAccount
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1016, 745)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cmbCompany)
        Me.Controls.Add(Me.cboBSNotes)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.cmbPLNotes)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.uicmbType)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtCode)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtMainCode)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.DataGridView1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmSubSubAccount"
        Me.Text = "Sub Sub Account"
        Me.ToolTip1.SetToolTip(Me, "Define Sub Sub Accounts")
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents BtnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtMainCode As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents uicmbType As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents BtnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents cmbPLNotes As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents ToolStripDropDownButton1 As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents ChartsOfAccountToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CashFlowStatmentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StanderCashFlowStatementToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cboBSNotes As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents DataGridView1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents cmbCompany As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

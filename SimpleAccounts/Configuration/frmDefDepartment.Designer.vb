<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDefDepartment
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDefDepartment))
        Dim DataGridView1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.uichkActive = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.uitxtSortOrder = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.uitxtComments = New System.Windows.Forms.TextBox()
        Me.uitxtName = New System.Windows.Forms.TextBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.BtnNew = New System.Windows.Forms.ToolStripButton()
        Me.BtnEdit = New System.Windows.Forms.ToolStripButton()
        Me.BtnSave = New System.Windows.Forms.ToolStripButton()
        Me.BtnDelete = New System.Windows.Forms.ToolStripButton()
        Me.BtnPrint = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.DataGridView1 = New Janus.Windows.GridEX.GridEX()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.txtAccountDescription = New System.Windows.Forms.TextBox()
        Me.btnAddSalaryExpenseAc = New System.Windows.Forms.Button()
        Me.txtSalaryExpense = New System.Windows.Forms.TextBox()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.lblSalaryAccount = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.btnHeader = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'uichkActive
        '
        Me.uichkActive.AutoSize = True
        Me.uichkActive.Checked = True
        Me.uichkActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.uichkActive.ForeColor = System.Drawing.Color.Black
        Me.uichkActive.Location = New System.Drawing.Point(390, 135)
        Me.uichkActive.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.uichkActive.Name = "uichkActive"
        Me.uichkActive.Size = New System.Drawing.Size(78, 24)
        Me.uichkActive.TabIndex = 8
        Me.uichkActive.Text = "Active"
        Me.ToolTip1.SetToolTip(Me.uichkActive, "Department Status Active Or Inactive")
        Me.uichkActive.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(243, 137)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(83, 20)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Sort Order"
        '
        'uitxtSortOrder
        '
        Me.uitxtSortOrder.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.uitxtSortOrder.Location = New System.Drawing.Point(340, 132)
        Me.uitxtSortOrder.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.uitxtSortOrder.MaxLength = 50
        Me.uitxtSortOrder.Name = "uitxtSortOrder"
        Me.uitxtSortOrder.Size = New System.Drawing.Size(38, 26)
        Me.uitxtSortOrder.TabIndex = 7
        Me.uitxtSortOrder.Text = "1"
        Me.ToolTip1.SetToolTip(Me.uitxtSortOrder, "Sort Order")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(18, 32)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(86, 20)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Comments"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(10, 29)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 20)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Name"
        '
        'uitxtComments
        '
        Me.uitxtComments.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.uitxtComments.Location = New System.Drawing.Point(22, 57)
        Me.uitxtComments.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.uitxtComments.MaxLength = 50
        Me.uitxtComments.Multiline = True
        Me.uitxtComments.Name = "uitxtComments"
        Me.uitxtComments.Size = New System.Drawing.Size(442, 70)
        Me.uitxtComments.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.uitxtComments, "Comments")
        '
        'uitxtName
        '
        Me.uitxtName.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.uitxtName.Location = New System.Drawing.Point(172, 25)
        Me.uitxtName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.uitxtName.MaxLength = 50
        Me.uitxtName.Name = "uitxtName"
        Me.uitxtName.Size = New System.Drawing.Size(316, 26)
        Me.uitxtName.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.uitxtName, "Department Name")
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BtnNew, Me.BtnEdit, Me.BtnSave, Me.BtnDelete, Me.BtnPrint, Me.toolStripSeparator1, Me.HelpToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1064, 32)
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
        'DataGridView1
        '
        Me.DataGridView1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnAutoResize = True
        DataGridView1_DesignTimeLayout.LayoutString = resources.GetString("DataGridView1_DesignTimeLayout.LayoutString")
        Me.DataGridView1.DesignTimeLayout = DataGridView1_DesignTimeLayout
        Me.DataGridView1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.DataGridView1.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.DataGridView1.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.DataGridView1.GroupByBoxVisible = False
        Me.DataGridView1.Location = New System.Drawing.Point(2, 335)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RecordNavigator = True
        Me.DataGridView1.Size = New System.Drawing.Size(1060, 331)
        Me.DataGridView1.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.DataGridView1, "Define Dsepartments")
        Me.DataGridView1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'btnAdd
        '
        Me.btnAdd.Image = Global.SimpleAccounts.My.Resources.Resources.pin_black
        Me.btnAdd.Location = New System.Drawing.Point(444, 69)
        Me.btnAdd.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(46, 35)
        Me.btnAdd.TabIndex = 101
        Me.btnAdd.TabStop = False
        Me.ToolTip1.SetToolTip(Me.btnAdd, "Opens Accounts")
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'txtAccountDescription
        '
        Me.txtAccountDescription.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtAccountDescription.Enabled = False
        Me.txtAccountDescription.Location = New System.Drawing.Point(172, 74)
        Me.txtAccountDescription.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtAccountDescription.Name = "txtAccountDescription"
        Me.txtAccountDescription.Size = New System.Drawing.Size(260, 26)
        Me.txtAccountDescription.TabIndex = 100
        Me.ToolTip1.SetToolTip(Me.txtAccountDescription, "Payable Account Map")
        '
        'btnAddSalaryExpenseAc
        '
        Me.btnAddSalaryExpenseAc.Image = Global.SimpleAccounts.My.Resources.Resources.pin_black
        Me.btnAddSalaryExpenseAc.Location = New System.Drawing.Point(444, 117)
        Me.btnAddSalaryExpenseAc.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnAddSalaryExpenseAc.Name = "btnAddSalaryExpenseAc"
        Me.btnAddSalaryExpenseAc.Size = New System.Drawing.Size(46, 35)
        Me.btnAddSalaryExpenseAc.TabIndex = 104
        Me.btnAddSalaryExpenseAc.TabStop = False
        Me.ToolTip1.SetToolTip(Me.btnAddSalaryExpenseAc, "Opens Accounts")
        Me.btnAddSalaryExpenseAc.UseVisualStyleBackColor = True
        '
        'txtSalaryExpense
        '
        Me.txtSalaryExpense.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtSalaryExpense.Enabled = False
        Me.txtSalaryExpense.Location = New System.Drawing.Point(172, 118)
        Me.txtSalaryExpense.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtSalaryExpense.Name = "txtSalaryExpense"
        Me.txtSalaryExpense.Size = New System.Drawing.Size(260, 26)
        Me.txtSalaryExpense.TabIndex = 103
        Me.ToolTip1.SetToolTip(Me.txtSalaryExpense, "Payable Account Map")
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(321, 389)
        Me.lblProgress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(394, 69)
        Me.lblProgress.TabIndex = 18
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'lblSalaryAccount
        '
        Me.lblSalaryAccount.AutoSize = True
        Me.lblSalaryAccount.ForeColor = System.Drawing.Color.Black
        Me.lblSalaryAccount.Location = New System.Drawing.Point(10, 78)
        Me.lblSalaryAccount.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSalaryAccount.Name = "lblSalaryAccount"
        Me.lblSalaryAccount.Size = New System.Drawing.Size(111, 20)
        Me.lblSalaryAccount.TabIndex = 99
        Me.lblSalaryAccount.Text = "Account Head"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(10, 123)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(149, 20)
        Me.Label5.TabIndex = 102
        Me.Label5.Text = "Salary Expense A/C"
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnHeader)
        Me.pnlHeader.Controls.Add(Me.Button8)
        Me.pnlHeader.Controls.Add(Me.Button6)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Controls.Add(Me.btnRefresh)
        Me.pnlHeader.Controls.Add(Me.Button4)
        Me.pnlHeader.Controls.Add(Me.Button7)
        Me.pnlHeader.Controls.Add(Me.Button3)
        Me.pnlHeader.Controls.Add(Me.Button2)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 32)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1064, 65)
        Me.pnlHeader.TabIndex = 105
        '
        'btnHeader
        '
        Me.btnHeader.BackgroundImage = CType(resources.GetObject("btnHeader.BackgroundImage"), System.Drawing.Image)
        Me.btnHeader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnHeader.FlatAppearance.BorderSize = 0
        Me.btnHeader.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnHeader.Location = New System.Drawing.Point(4, 15)
        Me.btnHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnHeader.Name = "btnHeader"
        Me.btnHeader.Size = New System.Drawing.Size(32, 35)
        Me.btnHeader.TabIndex = 0
        Me.btnHeader.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button8.BackgroundImage = CType(resources.GetObject("Button8.BackgroundImage"), System.Drawing.Image)
        Me.Button8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button8.FlatAppearance.BorderSize = 0
        Me.Button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button8.Location = New System.Drawing.Point(834, 14)
        Me.Button8.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(38, 35)
        Me.Button8.TabIndex = 0
        Me.Button8.UseVisualStyleBackColor = True
        Me.Button8.Visible = False
        '
        'Button6
        '
        Me.Button6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button6.BackgroundImage = CType(resources.GetObject("Button6.BackgroundImage"), System.Drawing.Image)
        Me.Button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button6.FlatAppearance.BorderSize = 0
        Me.Button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button6.Location = New System.Drawing.Point(999, 9)
        Me.Button6.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(40, 46)
        Me.Button6.TabIndex = 0
        Me.Button6.UseVisualStyleBackColor = True
        Me.Button6.Visible = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(45, 15)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(181, 36)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Department"
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.BackgroundImage = CType(resources.GetObject("btnRefresh.BackgroundImage"), System.Drawing.Image)
        Me.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnRefresh.FlatAppearance.BorderSize = 0
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Location = New System.Drawing.Point(963, 12)
        Me.btnRefresh.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(45, 40)
        Me.btnRefresh.TabIndex = 0
        Me.btnRefresh.UseVisualStyleBackColor = True
        Me.btnRefresh.Visible = False
        '
        'Button4
        '
        Me.Button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button4.BackgroundImage = CType(resources.GetObject("Button4.BackgroundImage"), System.Drawing.Image)
        Me.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button4.FlatAppearance.BorderSize = 0
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Location = New System.Drawing.Point(930, 14)
        Me.Button4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(42, 37)
        Me.Button4.TabIndex = 0
        Me.Button4.UseVisualStyleBackColor = True
        Me.Button4.Visible = False
        '
        'Button7
        '
        Me.Button7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button7.BackgroundImage = CType(resources.GetObject("Button7.BackgroundImage"), System.Drawing.Image)
        Me.Button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button7.FlatAppearance.BorderSize = 0
        Me.Button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button7.Location = New System.Drawing.Point(802, 15)
        Me.Button7.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(33, 34)
        Me.Button7.TabIndex = 0
        Me.Button7.UseVisualStyleBackColor = True
        Me.Button7.Visible = False
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.BackgroundImage = CType(resources.GetObject("Button3.BackgroundImage"), System.Drawing.Image)
        Me.Button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button3.FlatAppearance.BorderSize = 0
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Location = New System.Drawing.Point(898, 14)
        Me.Button3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(38, 35)
        Me.Button3.TabIndex = 0
        Me.Button3.UseVisualStyleBackColor = True
        Me.Button3.Visible = False
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackgroundImage = CType(resources.GetObject("Button2.BackgroundImage"), System.Drawing.Image)
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button2.FlatAppearance.BorderSize = 0
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Location = New System.Drawing.Point(868, 14)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(38, 35)
        Me.Button2.TabIndex = 0
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.uitxtName)
        Me.Panel1.Controls.Add(Me.btnAddSalaryExpenseAc)
        Me.Panel1.Controls.Add(Me.lblSalaryAccount)
        Me.Panel1.Controls.Add(Me.txtSalaryExpense)
        Me.Panel1.Controls.Add(Me.txtAccountDescription)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.btnAdd)
        Me.Panel1.Location = New System.Drawing.Point(30, 134)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(508, 174)
        Me.Panel1.TabIndex = 106
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.Panel3.Controls.Add(Me.Label2)
        Me.Panel3.Controls.Add(Me.uitxtComments)
        Me.Panel3.Controls.Add(Me.Label3)
        Me.Panel3.Controls.Add(Me.uitxtSortOrder)
        Me.Panel3.Controls.Add(Me.uichkActive)
        Me.Panel3.Location = New System.Drawing.Point(552, 131)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(483, 177)
        Me.Panel3.TabIndex = 107
        '
        'frmDefDepartment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1064, 668)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmDefDepartment"
        Me.Text = "Department"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents uichkActive As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents uitxtSortOrder As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents uitxtComments As System.Windows.Forms.TextBox
    Friend WithEvents uitxtName As System.Windows.Forms.TextBox
    Friend WithEvents BtnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents BtnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents DataGridView1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents txtAccountDescription As System.Windows.Forms.TextBox
    Friend WithEvents lblSalaryAccount As System.Windows.Forms.Label
    Friend WithEvents btnAddSalaryExpenseAc As System.Windows.Forms.Button
    Friend WithEvents txtSalaryExpense As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents btnHeader As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
End Class

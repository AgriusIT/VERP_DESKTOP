<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmployeePromotion
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmployeePromotion))
        Dim grd_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripSplitButton()
        Me.PromotionIncreamentToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PromotionLetterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.TransferToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtDepartment = New System.Windows.Forms.TextBox()
        Me.cmbEmployee = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtDivision = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtDesignation = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtSalary = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtIncreament = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbPromotionType = New System.Windows.Forms.ComboBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbDesignation = New System.Windows.Forms.ComboBox()
        Me.cmbDivision = New System.Windows.Forms.ComboBox()
        Me.cmbDepartment = New System.Windows.Forms.ComboBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.dtpDocDate = New System.Windows.Forms.DateTimePicker()
        Me.txtDocNo = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.empPic = New System.Windows.Forms.PictureBox()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        CType(Me.empPic, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.btnPrint, Me.toolStripSeparator, Me.btnDelete, Me.toolStripSeparator1, Me.HelpToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(834, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
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
        Me.btnPrint.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PromotionIncreamentToolStripMenuItem, Me.PromotionLetterToolStripMenuItem, Me.TransferToolStripMenuItem})
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(64, 22)
        Me.btnPrint.Text = "&Print"
        '
        'PromotionIncreamentToolStripMenuItem
        '
        Me.PromotionIncreamentToolStripMenuItem.Name = "PromotionIncreamentToolStripMenuItem"
        Me.PromotionIncreamentToolStripMenuItem.Size = New System.Drawing.Size(131, 22)
        Me.PromotionIncreamentToolStripMenuItem.Text = "Increment"
        '
        'PromotionLetterToolStripMenuItem
        '
        Me.PromotionLetterToolStripMenuItem.Name = "PromotionLetterToolStripMenuItem"
        Me.PromotionLetterToolStripMenuItem.Size = New System.Drawing.Size(131, 22)
        Me.PromotionLetterToolStripMenuItem.Text = "Promotion"
        '
        'TransferToolStripMenuItem
        '
        Me.TransferToolStripMenuItem.Name = "TransferToolStripMenuItem"
        Me.TransferToolStripMenuItem.Size = New System.Drawing.Size(131, 22)
        Me.TransferToolStripMenuItem.Text = "Transfer"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(60, 22)
        Me.btnDelete.Text = "D&elete"
        Me.btnDelete.Visible = False
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 25)
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
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(11, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(233, 23)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Employee Promotion"
        '
        'grd
        '
        Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grd_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grd.DesignTimeLayout = grd_DesignTimeLayout
        Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.grd.GroupByBoxVisible = False
        Me.grd.Location = New System.Drawing.Point(0, 272)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(834, 375)
        Me.grd.TabIndex = 16
        Me.grd.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        Me.grd.TabStop = False
        Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 149)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Department"
        '
        'txtDepartment
        '
        Me.txtDepartment.BackColor = System.Drawing.SystemColors.Window
        Me.txtDepartment.Location = New System.Drawing.Point(74, 146)
        Me.txtDepartment.Name = "txtDepartment"
        Me.txtDepartment.ReadOnly = True
        Me.txtDepartment.Size = New System.Drawing.Size(208, 20)
        Me.txtDepartment.TabIndex = 6
        '
        'cmbEmployee
        '
        Me.cmbEmployee.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbEmployee.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbEmployee.FormattingEnabled = True
        Me.cmbEmployee.Location = New System.Drawing.Point(74, 119)
        Me.cmbEmployee.Name = "cmbEmployee"
        Me.cmbEmployee.Size = New System.Drawing.Size(208, 21)
        Me.cmbEmployee.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 122)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Employee"
        '
        'txtDivision
        '
        Me.txtDivision.BackColor = System.Drawing.SystemColors.Window
        Me.txtDivision.Location = New System.Drawing.Point(74, 172)
        Me.txtDivision.Name = "txtDivision"
        Me.txtDivision.ReadOnly = True
        Me.txtDivision.Size = New System.Drawing.Size(208, 20)
        Me.txtDivision.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 175)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Division"
        '
        'txtDesignation
        '
        Me.txtDesignation.BackColor = System.Drawing.SystemColors.Window
        Me.txtDesignation.Location = New System.Drawing.Point(74, 198)
        Me.txtDesignation.Name = "txtDesignation"
        Me.txtDesignation.ReadOnly = True
        Me.txtDesignation.Size = New System.Drawing.Size(208, 20)
        Me.txtDesignation.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 201)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(63, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Designation"
        '
        'txtSalary
        '
        Me.txtSalary.BackColor = System.Drawing.SystemColors.Window
        Me.txtSalary.Location = New System.Drawing.Point(74, 224)
        Me.txtSalary.Name = "txtSalary"
        Me.txtSalary.ReadOnly = True
        Me.txtSalary.Size = New System.Drawing.Size(99, 20)
        Me.txtSalary.TabIndex = 12
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 227)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(36, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Salary"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtIncreament)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.cmbPromotionType)
        Me.GroupBox1.Location = New System.Drawing.Point(298, 65)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(280, 44)
        Me.GroupBox1.TabIndex = 13
        Me.GroupBox1.TabStop = False
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(122, 21)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(60, 13)
        Me.Label11.TabIndex = 2
        Me.Label11.Text = "Increament"
        '
        'txtIncreament
        '
        Me.txtIncreament.BackColor = System.Drawing.SystemColors.Window
        Me.txtIncreament.Location = New System.Drawing.Point(188, 17)
        Me.txtIncreament.Name = "txtIncreament"
        Me.txtIncreament.Size = New System.Drawing.Size(77, 20)
        Me.txtIncreament.TabIndex = 3
        Me.txtIncreament.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(6, 21)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(31, 13)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Type"
        '
        'cmbPromotionType
        '
        Me.cmbPromotionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPromotionType.FormattingEnabled = True
        Me.cmbPromotionType.Items.AddRange(New Object() {"Increment", "Promotion", "Transfer"})
        Me.cmbPromotionType.Location = New System.Drawing.Point(43, 17)
        Me.cmbPromotionType.Name = "cmbPromotionType"
        Me.cmbPromotionType.Size = New System.Drawing.Size(73, 21)
        Me.cmbPromotionType.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbPromotionType, "Select increament type")
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.cmbDesignation)
        Me.GroupBox2.Controls.Add(Me.cmbDivision)
        Me.GroupBox2.Controls.Add(Me.cmbDepartment)
        Me.GroupBox2.Location = New System.Drawing.Point(298, 115)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(280, 103)
        Me.GroupBox2.TabIndex = 14
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Promotion"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 76)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(63, 13)
        Me.Label9.TabIndex = 4
        Me.Label9.Text = "Designation"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 49)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(44, 13)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "Division"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 22)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(62, 13)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Department"
        '
        'cmbDesignation
        '
        Me.cmbDesignation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbDesignation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbDesignation.FormattingEnabled = True
        Me.cmbDesignation.Location = New System.Drawing.Point(76, 73)
        Me.cmbDesignation.Name = "cmbDesignation"
        Me.cmbDesignation.Size = New System.Drawing.Size(189, 21)
        Me.cmbDesignation.TabIndex = 5
        '
        'cmbDivision
        '
        Me.cmbDivision.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbDivision.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbDivision.FormattingEnabled = True
        Me.cmbDivision.Location = New System.Drawing.Point(76, 46)
        Me.cmbDivision.Name = "cmbDivision"
        Me.cmbDivision.Size = New System.Drawing.Size(189, 21)
        Me.cmbDivision.TabIndex = 3
        '
        'cmbDepartment
        '
        Me.cmbDepartment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbDepartment.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbDepartment.FormattingEnabled = True
        Me.cmbDepartment.Location = New System.Drawing.Point(76, 19)
        Me.cmbDepartment.Name = "cmbDepartment"
        Me.cmbDepartment.Size = New System.Drawing.Size(189, 21)
        Me.cmbDepartment.TabIndex = 1
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.dtpDocDate)
        Me.GroupBox3.Controls.Add(Me.txtDocNo)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 64)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(280, 44)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        '
        'dtpDocDate
        '
        Me.dtpDocDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpDocDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDocDate.Location = New System.Drawing.Point(161, 17)
        Me.dtpDocDate.Name = "dtpDocDate"
        Me.dtpDocDate.Size = New System.Drawing.Size(109, 20)
        Me.dtpDocDate.TabIndex = 2
        '
        'txtDocNo
        '
        Me.txtDocNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDocNo.Location = New System.Drawing.Point(62, 17)
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.ReadOnly = True
        Me.txtDocNo.Size = New System.Drawing.Size(78, 20)
        Me.txtDocNo.TabIndex = 1
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(6, 21)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(47, 13)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "Doc No."
        '
        'empPic
        '
        Me.empPic.ErrorImage = Global.SimpleAccounts.My.Resources.Resources.Error_Image11
        Me.empPic.Image = Global.SimpleAccounts.My.Resources.Resources.no_image
        Me.empPic.Location = New System.Drawing.Point(584, 70)
        Me.empPic.Name = "empPic"
        Me.empPic.Size = New System.Drawing.Size(159, 145)
        Me.empPic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.empPic.TabIndex = 50
        Me.empPic.TabStop = False
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(255, 227)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 15
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'pnlHeader
        '
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 25)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(834, 42)
        Me.pnlHeader.TabIndex = 78
        '
        'frmEmployeePromotion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(834, 647)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.empPic)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtSalary)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtDesignation)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtDivision)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbEmployee)
        Me.Controls.Add(Me.txtDepartment)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmEmployeePromotion"
        Me.Text = "Employee Promotion"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        CType(Me.empPic, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtDepartment As System.Windows.Forms.TextBox
    Friend WithEvents cmbEmployee As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtDivision As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtDesignation As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtSalary As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbDesignation As System.Windows.Forms.ComboBox
    Friend WithEvents cmbDivision As System.Windows.Forms.ComboBox
    Friend WithEvents cmbDepartment As System.Windows.Forms.ComboBox
    Friend WithEvents txtIncreament As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmbPromotionType As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents empPic As System.Windows.Forms.PictureBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents dtpDocDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtDocNo As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents PromotionIncreamentToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PromotionLetterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents TransferToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

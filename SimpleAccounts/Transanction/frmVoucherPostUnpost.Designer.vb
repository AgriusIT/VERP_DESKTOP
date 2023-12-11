<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVoucherPostUnpost
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVoucherPostUnpost))
        Dim grd_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnPost = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.btnUnPost = New System.Windows.Forms.ToolStripButton()
        Me.CutToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.lblCloseReport = New System.Windows.Forms.ToolStripButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.rdoPost = New System.Windows.Forms.RadioButton()
        Me.rdoUnPost = New System.Windows.Forms.RadioButton()
        Me.rdoAll = New System.Windows.Forms.RadioButton()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbVtype = New System.Windows.Forms.ComboBox()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbGroupbyName = New System.Windows.Forms.RadioButton()
        Me.rbGroupbyType = New System.Windows.Forms.RadioButton()
        Me.rbGroupbyNone = New System.Windows.Forms.RadioButton()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.ToolStripSeparator1, Me.btnPost, Me.toolStripSeparator, Me.btnUnPost, Me.CutToolStripButton, Me.HelpToolStripButton, Me.lblCloseReport})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(824, 25)
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
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnPost
        '
        Me.btnPost.Image = CType(resources.GetObject("btnPost.Image"), System.Drawing.Image)
        Me.btnPost.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPost.Name = "btnPost"
        Me.btnPost.Size = New System.Drawing.Size(50, 22)
        Me.btnPost.Text = "&Post"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'btnUnPost
        '
        Me.btnUnPost.Image = Global.SimpleAccounts.My.Resources.Resources.disk
        Me.btnUnPost.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnUnPost.Name = "btnUnPost"
        Me.btnUnPost.Size = New System.Drawing.Size(65, 22)
        Me.btnUnPost.Text = "&UnPost"
        '
        'CutToolStripButton
        '
        Me.CutToolStripButton.Image = CType(resources.GetObject("CutToolStripButton.Image"), System.Drawing.Image)
        Me.CutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CutToolStripButton.Name = "CutToolStripButton"
        Me.CutToolStripButton.Size = New System.Drawing.Size(46, 22)
        Me.CutToolStripButton.Text = "C&ut"
        Me.CutToolStripButton.Visible = False
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
        'lblCloseReport
        '
        Me.lblCloseReport.Image = Global.SimpleAccounts.My.Resources.Resources._0681
        Me.lblCloseReport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.lblCloseReport.Name = "lblCloseReport"
        Me.lblCloseReport.Size = New System.Drawing.Size(94, 22)
        Me.lblCloseReport.Text = "Close Report"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(19, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "From Date"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(19, 51)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "To Date"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CalendarForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.dtpFromDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtpFromDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFromDate.Location = New System.Drawing.Point(99, 15)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(235, 20)
        Me.dtpFromDate.TabIndex = 3
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(99, 47)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(235, 20)
        Me.dtpToDate.TabIndex = 5
        '
        'rdoPost
        '
        Me.rdoPost.AutoSize = True
        Me.rdoPost.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoPost.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.rdoPost.Location = New System.Drawing.Point(99, 113)
        Me.rdoPost.Name = "rdoPost"
        Me.rdoPost.Size = New System.Drawing.Size(46, 17)
        Me.rdoPost.TabIndex = 8
        Me.rdoPost.TabStop = True
        Me.rdoPost.Text = "Post"
        Me.rdoPost.UseVisualStyleBackColor = True
        '
        'rdoUnPost
        '
        Me.rdoUnPost.AutoSize = True
        Me.rdoUnPost.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoUnPost.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.rdoUnPost.Location = New System.Drawing.Point(151, 113)
        Me.rdoUnPost.Name = "rdoUnPost"
        Me.rdoUnPost.Size = New System.Drawing.Size(60, 17)
        Me.rdoUnPost.TabIndex = 9
        Me.rdoUnPost.TabStop = True
        Me.rdoUnPost.Text = "UnPost"
        Me.rdoUnPost.UseVisualStyleBackColor = True
        '
        'rdoAll
        '
        Me.rdoAll.AutoSize = True
        Me.rdoAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoAll.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.rdoAll.Location = New System.Drawing.Point(217, 113)
        Me.rdoAll.Name = "rdoAll"
        Me.rdoAll.Size = New System.Drawing.Size(36, 17)
        Me.rdoAll.TabIndex = 10
        Me.rdoAll.TabStop = True
        Me.rdoAll.Text = "All"
        Me.rdoAll.UseVisualStyleBackColor = True
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.lblHeader.Location = New System.Drawing.Point(26, 13)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(187, 23)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Voucher Posting"
        '
        'grd
        '
        Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grd.ColumnAutoResize = True
        grd_DesignTimeLayout.LayoutString = resources.GetString("grd_DesignTimeLayout.LayoutString")
        Me.grd.DesignTimeLayout = grd_DesignTimeLayout
        Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.grd.GroupByBoxVisible = False
        Me.grd.Location = New System.Drawing.Point(3, 263)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(818, 291)
        Me.grd.TabIndex = 12
        Me.grd.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btnGenerate
        '
        Me.btnGenerate.BackColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGenerate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.btnGenerate.Location = New System.Drawing.Point(259, 107)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(75, 28)
        Me.btnGenerate.TabIndex = 11
        Me.btnGenerate.Text = "Generate"
        Me.btnGenerate.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(19, 83)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Transaction"
        '
        'cmbVtype
        '
        Me.cmbVtype.BackColor = System.Drawing.SystemColors.Control
        Me.cmbVtype.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.cmbVtype.FormattingEnabled = True
        Me.cmbVtype.Items.AddRange(New Object() {"......Select Any........", "Voucher", "Sale", "Sale Return", "Purchase", "Purchase Return", "Store Issuance", "Production", "Invoice Based Receipt", "Invoice Based Payment"})
        Me.cmbVtype.Location = New System.Drawing.Point(99, 79)
        Me.cmbVtype.Name = "cmbVtype"
        Me.cmbVtype.Size = New System.Drawing.Size(235, 21)
        Me.cmbVtype.TabIndex = 7
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(215, 311)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 22
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 25)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.pnlHeader)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblProgress)
        Me.SplitContainer1.Panel1.Controls.Add(Me.grd)
        Me.SplitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.SplitContainer1.Panel2Collapsed = True
        Me.SplitContainer1.Size = New System.Drawing.Size(824, 554)
        Me.SplitContainer1.SplitterDistance = 404
        Me.SplitContainer1.TabIndex = 23
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel2.Controls.Add(Me.GroupBox1)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.rdoAll)
        Me.Panel2.Controls.Add(Me.rdoUnPost)
        Me.Panel2.Controls.Add(Me.rdoPost)
        Me.Panel2.Controls.Add(Me.cmbVtype)
        Me.Panel2.Controls.Add(Me.dtpToDate)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.btnGenerate)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.dtpFromDate)
        Me.Panel2.Location = New System.Drawing.Point(16, 67)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(795, 190)
        Me.Panel2.TabIndex = 24
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbGroupbyName)
        Me.GroupBox1.Controls.Add(Me.rbGroupbyType)
        Me.GroupBox1.Controls.Add(Me.rbGroupbyNone)
        Me.GroupBox1.Location = New System.Drawing.Point(22, 141)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(312, 38)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        '
        'rbGroupbyName
        '
        Me.rbGroupbyName.AutoSize = True
        Me.rbGroupbyName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbGroupbyName.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.rbGroupbyName.Location = New System.Drawing.Point(160, 15)
        Me.rbGroupbyName.Name = "rbGroupbyName"
        Me.rbGroupbyName.Size = New System.Drawing.Size(47, 17)
        Me.rbGroupbyName.TabIndex = 13
        Me.rbGroupbyName.TabStop = True
        Me.rbGroupbyName.Text = "User"
        Me.rbGroupbyName.UseVisualStyleBackColor = True
        '
        'rbGroupbyType
        '
        Me.rbGroupbyType.AutoSize = True
        Me.rbGroupbyType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbGroupbyType.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.rbGroupbyType.Location = New System.Drawing.Point(63, 15)
        Me.rbGroupbyType.Name = "rbGroupbyType"
        Me.rbGroupbyType.Size = New System.Drawing.Size(92, 17)
        Me.rbGroupbyType.TabIndex = 12
        Me.rbGroupbyType.TabStop = True
        Me.rbGroupbyType.Text = "Voucher Type"
        Me.rbGroupbyType.UseVisualStyleBackColor = True
        '
        'rbGroupbyNone
        '
        Me.rbGroupbyNone.AutoSize = True
        Me.rbGroupbyNone.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbGroupbyNone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.rbGroupbyNone.Location = New System.Drawing.Point(4, 15)
        Me.rbGroupbyNone.Name = "rbGroupbyNone"
        Me.rbGroupbyNone.Size = New System.Drawing.Size(51, 17)
        Me.rbGroupbyNone.TabIndex = 11
        Me.rbGroupbyNone.TabStop = True
        Me.rbGroupbyNone.Text = "None"
        Me.rbGroupbyNone.UseVisualStyleBackColor = True
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(824, 50)
        Me.pnlHeader.TabIndex = 23
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(786, 0)
        Me.CtrlGrdBar1.MyGrid = Me.grd
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(38, 25)
        Me.CtrlGrdBar1.TabIndex = 24
        '
        'frmVoucherPostUnpost
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(824, 579)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.KeyPreview = True
        Me.Name = "frmVoucherPostUnpost"
        Me.Text = "Voucher Posting"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnPost As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnUnPost As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CutToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents rdoPost As System.Windows.Forms.RadioButton
    Friend WithEvents rdoUnPost As System.Windows.Forms.RadioButton
    Friend WithEvents rdoAll As System.Windows.Forms.RadioButton
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnGenerate As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbVtype As System.Windows.Forms.ComboBox
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents lblCloseReport As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents rbGroupbyName As RadioButton
    Friend WithEvents rbGroupbyType As RadioButton
    Friend WithEvents rbGroupbyNone As RadioButton
End Class

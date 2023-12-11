<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGrdRptPostDatedChequesSummary
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGrdRptPostDatedChequesSummary))
        Dim grd_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lblLayout = New System.Windows.Forms.Label()
        Me.cmbLayout = New System.Windows.Forms.ComboBox()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbBank = New System.Windows.Forms.ComboBox()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh, Me.btnPrint, Me.toolStripSeparator3, Me.HelpToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1258, 38)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 35)
        Me.btnRefresh.Text = "Refresh"
        '
        'btnPrint
        '
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(142, 35)
        Me.btnPrint.Text = "&Print preview"
        '
        'toolStripSeparator3
        '
        Me.toolStripSeparator3.Name = "toolStripSeparator3"
        Me.toolStripSeparator3.Size = New System.Drawing.Size(6, 38)
        '
        'HelpToolStripButton
        '
        Me.HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HelpToolStripButton.Image = CType(resources.GetObject("HelpToolStripButton.Image"), System.Drawing.Image)
        Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton.Name = "HelpToolStripButton"
        Me.HelpToolStripButton.Size = New System.Drawing.Size(28, 35)
        Me.HelpToolStripButton.Text = "He&lp"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.cmbPeriod)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.dtpTo)
        Me.GroupBox1.Controls.Add(Me.dtpFrom)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 115)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(375, 154)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Date Range"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 29)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(54, 20)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Period"
        '
        'cmbPeriod
        '
        Me.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPeriod.FormattingEnabled = True
        Me.cmbPeriod.Items.AddRange(New Object() {"Today", "Yesterday", "Current Week", "Current Month", "Current Year"})
        Me.cmbPeriod.Location = New System.Drawing.Point(136, 25)
        Me.cmbPeriod.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(222, 28)
        Me.cmbPeriod.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 112)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 20)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Date To"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 71)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 20)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Date From"
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd/MMM/yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(136, 108)
        Me.dtpTo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(222, 26)
        Me.dtpTo.TabIndex = 5
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(136, 66)
        Me.dtpFrom.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(222, 26)
        Me.dtpFrom.TabIndex = 3
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(13, 13)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(482, 41)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Post Dated Cheques Summary"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lblLayout)
        Me.GroupBox2.Controls.Add(Me.cmbLayout)
        Me.GroupBox2.Controls.Add(Me.btnGenerate)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.cmbBank)
        Me.GroupBox2.Location = New System.Drawing.Point(402, 115)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Size = New System.Drawing.Size(855, 154)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        '
        'lblLayout
        '
        Me.lblLayout.AutoSize = True
        Me.lblLayout.Location = New System.Drawing.Point(9, 71)
        Me.lblLayout.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblLayout.Name = "lblLayout"
        Me.lblLayout.Size = New System.Drawing.Size(57, 20)
        Me.lblLayout.TabIndex = 2
        Me.lblLayout.Text = "Layout"
        '
        'cmbLayout
        '
        Me.cmbLayout.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLayout.FormattingEnabled = True
        Me.cmbLayout.Items.AddRange(New Object() {"... Select Layout ...", "ABL", "HBL", "UBL", "Bank Al-Habib", "Bank Al-Falah", "Askari Bank", "Faysal Bank", "Meezan Bank", "Saadiq Standard Chartered", "MCB", "NBP", "Soneri Bank", "BIP", "Habib Metro", "NIB", "Kasab", "Silk Bank", "Habib Metro", "NIB", "Kasab", "Silk Bank"})
        Me.cmbLayout.Location = New System.Drawing.Point(87, 66)
        Me.cmbLayout.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbLayout.Name = "cmbLayout"
        Me.cmbLayout.Size = New System.Drawing.Size(433, 28)
        Me.cmbLayout.TabIndex = 3
        '
        'btnGenerate
        '
        Me.btnGenerate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnGenerate.Location = New System.Drawing.Point(531, 25)
        Me.btnGenerate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(112, 74)
        Me.btnGenerate.TabIndex = 4
        Me.btnGenerate.Text = "Generate Report"
        Me.btnGenerate.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 29)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 20)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Bank"
        '
        'cmbBank
        '
        Me.cmbBank.FormattingEnabled = True
        Me.cmbBank.Location = New System.Drawing.Point(87, 25)
        Me.cmbBank.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbBank.Name = "cmbBank"
        Me.cmbBank.Size = New System.Drawing.Size(433, 28)
        Me.cmbBank.TabIndex = 1
        '
        'grd
        '
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grd_DesignTimeLayout.LayoutString = resources.GetString("grd_DesignTimeLayout.LayoutString")
        Me.grd.DesignTimeLayout = grd_DesignTimeLayout
        Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grd.Location = New System.Drawing.Point(2, 278)
        Me.grd.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(1256, 469)
        Me.grd.TabIndex = 3
        Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(1200, 0)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar1.MyGrid = Me.grd
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(57, 38)
        Me.CtrlGrdBar1.TabIndex = 4
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.ForeColor = System.Drawing.Color.Black
        Me.pnlHeader.Location = New System.Drawing.Point(0, 38)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1258, 65)
        Me.pnlHeader.TabIndex = 83
        '
        'frmGrdRptPostDatedChequesSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1258, 749)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmGrdRptPostDatedChequesSummary"
        Me.Text = "Post Dated Cheques Summary"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnGenerate As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbBank As System.Windows.Forms.ComboBox
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents lblLayout As System.Windows.Forms.Label
    Friend WithEvents cmbLayout As System.Windows.Forms.ComboBox
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

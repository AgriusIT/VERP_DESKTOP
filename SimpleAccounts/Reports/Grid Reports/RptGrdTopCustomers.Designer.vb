<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RptGrdTopCustomers
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(RptGrdTopCustomers))
        Me.txtTop = New System.Windows.Forms.TextBox()
        Me.dtpDateTo = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.dtpDateFrom = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.grdTopCustomer = New Janus.Windows.GridEX.GridEX()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cmbItems = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.bntGenerate = New System.Windows.Forms.Button()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlHeader = New System.Windows.Forms.Panel()
        CType(Me.grdTopCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtTop
        '
        Me.txtTop.Location = New System.Drawing.Point(428, 68)
        Me.txtTop.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtTop.Name = "txtTop"
        Me.txtTop.Size = New System.Drawing.Size(70, 26)
        Me.txtTop.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.txtTop, "No Of Top Customers")
        '
        'dtpDateTo
        '
        Me.dtpDateTo.CustomFormat = "dd/MMM/yyy"
        Me.dtpDateTo.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtpDateTo.DropDownCalendar.Name = ""
        Me.dtpDateTo.Location = New System.Drawing.Point(98, 108)
        Me.dtpDateTo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpDateTo.Name = "dtpDateTo"
        Me.dtpDateTo.Size = New System.Drawing.Size(202, 26)
        Me.dtpDateTo.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.dtpDateTo, "To Date")
        '
        'dtpDateFrom
        '
        Me.dtpDateFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpDateFrom.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtpDateFrom.DropDownCalendar.Name = ""
        Me.dtpDateFrom.Location = New System.Drawing.Point(98, 68)
        Me.dtpDateFrom.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpDateFrom.Name = "dtpDateFrom"
        Me.dtpDateFrom.Size = New System.Drawing.Size(202, 26)
        Me.dtpDateFrom.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.dtpDateFrom, "From Date")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(309, 74)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(109, 20)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Top Cutomers"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 114)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 20)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Date To"
        '
        'grdTopCustomer
        '
        Me.grdTopCustomer.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdTopCustomer.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdTopCustomer.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdTopCustomer.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdTopCustomer.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdTopCustomer.GroupByBoxVisible = False
        Me.grdTopCustomer.Location = New System.Drawing.Point(3, 315)
        Me.grdTopCustomer.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdTopCustomer.Name = "grdTopCustomer"
        Me.grdTopCustomer.RecordNavigator = True
        Me.grdTopCustomer.Size = New System.Drawing.Size(1226, 551)
        Me.grdTopCustomer.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.grdTopCustomer, "Sales Top Customers")
        Me.grdTopCustomer.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdTopCustomer.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Location = New System.Drawing.Point(18, 115)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(952, 183)
        Me.Panel1.TabIndex = 2
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmbItems)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.bntGenerate)
        Me.GroupBox2.Controls.Add(Me.CheckBox1)
        Me.GroupBox2.Controls.Add(Me.RadioButton2)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.RadioButton1)
        Me.GroupBox2.Controls.Add(Me.txtTop)
        Me.GroupBox2.Location = New System.Drawing.Point(369, 15)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Size = New System.Drawing.Size(572, 151)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        '
        'cmbItems
        '
        Me.cmbItems.FormattingEnabled = True
        Me.cmbItems.Location = New System.Drawing.Point(104, 26)
        Me.cmbItems.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbItems.Name = "cmbItems"
        Me.cmbItems.Size = New System.Drawing.Size(397, 28)
        Me.cmbItems.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbItems, "Select Any Item")
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(18, 32)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(82, 20)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Item Desc"
        '
        'bntGenerate
        '
        Me.bntGenerate.Location = New System.Drawing.Point(357, 105)
        Me.bntGenerate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.bntGenerate.Name = "bntGenerate"
        Me.bntGenerate.Size = New System.Drawing.Size(142, 38)
        Me.bntGenerate.TabIndex = 7
        Me.bntGenerate.Text = "Generate"
        Me.ToolTip1.SetToolTip(Me.bntGenerate, "Generate Report")
        Me.bntGenerate.UseVisualStyleBackColor = True
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(512, 28)
        Me.CheckBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(52, 24)
        Me.CheckBox1.TabIndex = 2
        Me.CheckBox1.Text = "All"
        Me.ToolTip1.SetToolTip(Me.CheckBox1, "Sales Top Customer By All Item")
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(202, 71)
        Me.RadioButton2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(97, 24)
        Me.RadioButton2.TabIndex = 4
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "By Value"
        Me.ToolTip1.SetToolTip(Me.RadioButton2, "View Top Customer By Value")
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(102, 71)
        Me.RadioButton1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(80, 24)
        Me.RadioButton1.TabIndex = 3
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "By Qty"
        Me.ToolTip1.SetToolTip(Me.RadioButton1, "View Top Customer By Quantity")
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.cmbPeriod)
        Me.GroupBox1.Controls.Add(Me.dtpDateTo)
        Me.GroupBox1.Controls.Add(Me.dtpDateFrom)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(27, 15)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(315, 151)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Date Range"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(10, 31)
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
        Me.cmbPeriod.Location = New System.Drawing.Point(98, 26)
        Me.cmbPeriod.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(200, 28)
        Me.cmbPeriod.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbPeriod, "Select Period And Gets Date Range")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 72)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Date From"
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(1174, 2)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar1.MyGrid = Me.grdTopCustomer
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(57, 37)
        Me.CtrlGrdBar1.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.CtrlGrdBar1, "Settings")
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(4, 14)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(388, 36)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Sales Based Top Customers"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1232, 38)
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
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 38)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1232, 65)
        Me.pnlHeader.TabIndex = 83
        '
        'RptGrdTopCustomers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1232, 868)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.grdTopCustomer)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "RptGrdTopCustomers"
        Me.Text = "Sales Based Top Customers"
        CType(Me.grdTopCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtTop As System.Windows.Forms.TextBox
    Friend WithEvents dtpDateTo As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents dtpDateFrom As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents grdTopCustomer As Janus.Windows.GridEX.GridEX
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents cmbItems As System.Windows.Forms.ComboBox
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents bntGenerate As System.Windows.Forms.Button
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

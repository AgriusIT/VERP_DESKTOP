<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGrdRptCharts
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGrdRptCharts))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.grdEmployee = New Janus.Windows.GridEX.GridEX()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rbtPie = New System.Windows.Forms.RadioButton()
        Me.rbtLine = New System.Windows.Forms.RadioButton()
        Me.rbtBar = New System.Windows.Forms.RadioButton()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1221, 32)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnRefresh
        '
        Me.btnRefresh.Enabled = False
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 29)
        Me.btnRefresh.Text = "Refresh"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.cmbPeriod)
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.dtpTo)
        Me.GroupBox1.Controls.Add(Me.dtpFrom)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 115)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(378, 154)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Date Range"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(16, 31)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(54, 20)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Period"
        '
        'cmbPeriod
        '
        Me.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPeriod.FormattingEnabled = True
        Me.cmbPeriod.Items.AddRange(New Object() {"Today", "Yesterday", "Current Week", "Current Month", "Current Year"})
        Me.cmbPeriod.Location = New System.Drawing.Point(136, 26)
        Me.cmbPeriod.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(222, 28)
        Me.cmbPeriod.TabIndex = 1
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(16, 114)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(66, 20)
        Me.Label9.TabIndex = 4
        Me.Label9.Text = "Date To"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(16, 72)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(85, 20)
        Me.Label10.TabIndex = 2
        Me.Label10.Text = "Date From"
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd/MMM/yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(136, 109)
        Me.dtpTo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(222, 26)
        Me.dtpTo.TabIndex = 5
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(136, 68)
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
        Me.lblHeader.Location = New System.Drawing.Point(18, 15)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(237, 41)
        Me.lblHeader.TabIndex = 5
        Me.lblHeader.Text = "Graph Report"
        '
        'grdEmployee
        '
        Me.grdEmployee.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdEmployee.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdEmployee.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdEmployee.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdEmployee.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdEmployee.GroupByBoxVisible = False
        Me.grdEmployee.Location = New System.Drawing.Point(0, 278)
        Me.grdEmployee.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdEmployee.Name = "grdEmployee"
        Me.grdEmployee.RecordNavigator = True
        Me.grdEmployee.Size = New System.Drawing.Size(1221, 614)
        Me.grdEmployee.TabIndex = 6
        Me.grdEmployee.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(174, 106)
        Me.btnShow.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(112, 35)
        Me.btnShow.TabIndex = 7
        Me.btnShow.Text = "Show"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rbtPie)
        Me.GroupBox2.Controls.Add(Me.rbtLine)
        Me.GroupBox2.Controls.Add(Me.rbtBar)
        Me.GroupBox2.Controls.Add(Me.btnShow)
        Me.GroupBox2.Location = New System.Drawing.Point(405, 115)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Size = New System.Drawing.Size(296, 154)
        Me.GroupBox2.TabIndex = 8
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Chart Type"
        '
        'rbtPie
        '
        Me.rbtPie.AutoSize = True
        Me.rbtPie.Location = New System.Drawing.Point(10, 98)
        Me.rbtPie.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtPie.Name = "rbtPie"
        Me.rbtPie.Size = New System.Drawing.Size(56, 24)
        Me.rbtPie.TabIndex = 10
        Me.rbtPie.Text = "Pie"
        Me.rbtPie.UseVisualStyleBackColor = True
        '
        'rbtLine
        '
        Me.rbtLine.AutoSize = True
        Me.rbtLine.Checked = True
        Me.rbtLine.Location = New System.Drawing.Point(10, 28)
        Me.rbtLine.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtLine.Name = "rbtLine"
        Me.rbtLine.Size = New System.Drawing.Size(64, 24)
        Me.rbtLine.TabIndex = 9
        Me.rbtLine.TabStop = True
        Me.rbtLine.Text = "Line"
        Me.rbtLine.UseVisualStyleBackColor = True
        '
        'rbtBar
        '
        Me.rbtBar.AutoSize = True
        Me.rbtBar.Location = New System.Drawing.Point(10, 63)
        Me.rbtBar.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtBar.Name = "rbtBar"
        Me.rbtBar.Size = New System.Drawing.Size(59, 24)
        Me.rbtBar.TabIndex = 8
        Me.rbtBar.Text = "Bar"
        Me.rbtBar.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(710, 131)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(172, 138)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 9
        Me.PictureBox1.TabStop = False
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(1164, 0)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar1.MyGrid = Me.grdEmployee
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(57, 38)
        Me.CtrlGrdBar1.TabIndex = 10
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 32)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1221, 65)
        Me.pnlHeader.TabIndex = 11
        '
        'frmGrdRptCharts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1221, 892)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.grdEmployee)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmGrdRptCharts"
        Me.Text = "Graphs Report"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents grdEmployee As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents grdControl As SimpleAccounts.CtrlGrdBar
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtPie As System.Windows.Forms.RadioButton
    Friend WithEvents rbtLine As System.Windows.Forms.RadioButton
    Friend WithEvents rbtBar As System.Windows.Forms.RadioButton
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

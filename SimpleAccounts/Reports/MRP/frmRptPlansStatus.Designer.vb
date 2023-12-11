<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptPlansStatus
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
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRptPlansStatus))
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnClose = New System.Windows.Forms.Button()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'GridEX1
        '
        Me.GridEX1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        GridEX1_DesignTimeLayout.LayoutString = resources.GetString("GridEX1_DesignTimeLayout.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX1.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEX1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.Location = New System.Drawing.Point(1, 187)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.Size = New System.Drawing.Size(634, 288)
        Me.GridEX1.TabIndex = 10
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(636, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(66, 22)
        Me.ToolStripButton1.Text = "&Refresh"
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.Location = New System.Drawing.Point(13, 109)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(33, 13)
        Me.lblFrom.TabIndex = 5
        Me.lblFrom.Text = "From "
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(50, 106)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(200, 20)
        Me.dtpFrom.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.dtpFrom, "From date")
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd/MMM/yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(50, 132)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(200, 20)
        Me.dtpTo.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.dtpTo, "To date")
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.Location = New System.Drawing.Point(26, 135)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(20, 13)
        Me.lblTo.TabIndex = 7
        Me.lblTo.Text = "To"
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(175, 158)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(75, 23)
        Me.btnShow.TabIndex = 9
        Me.btnShow.Text = "Show"
        Me.ToolTip1.SetToolTip(Me.btnShow, "Show")
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(11, 7)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(325, 24)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Production Plans Progress Report"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.btnClose.Location = New System.Drawing.Point(588, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(36, 30)
        Me.btnClose.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.btnClose, "Close")
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(601, 0)
        Me.CtrlGrdBar1.MyGrid = Me.GridEX1
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(34, 25)
        Me.CtrlGrdBar1.TabIndex = 1
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnClose)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 25)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(636, 42)
        Me.pnlHeader.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 82)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(37, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Period"
        '
        'cmbPeriod
        '
        Me.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPeriod.FormattingEnabled = True
        Me.cmbPeriod.Items.AddRange(New Object() {"Today", "Yesterday", "Current Week", "Current Month", "Current Year"})
        Me.cmbPeriod.Location = New System.Drawing.Point(50, 79)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(200, 21)
        Me.cmbPeriod.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.cmbPeriod, "Select Period And Gets Date Range")
        '
        'frmRptPlansStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(636, 478)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbPeriod)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.btnShow)
        Me.Controls.Add(Me.dtpTo)
        Me.Controls.Add(Me.lblTo)
        Me.Controls.Add(Me.dtpFrom)
        Me.Controls.Add(Me.lblFrom)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.GridEX1)
        Me.Name = "frmRptPlansStatus"
        Me.Text = "Production Plans Progress Report"
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class rptSalesManTarget
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptSalesManTarget))
        Me.cmbSalesMan = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpMonth = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlPeriod = New System.Windows.Forms.Panel()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.pnlPeriod.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbSalesMan
        '
        Me.cmbSalesMan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSalesMan.FormattingEnabled = True
        Me.cmbSalesMan.Location = New System.Drawing.Point(120, 12)
        Me.cmbSalesMan.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbSalesMan.Name = "cmbSalesMan"
        Me.cmbSalesMan.Size = New System.Drawing.Size(286, 28)
        Me.cmbSalesMan.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbSalesMan, "Select Any SalesMan")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 17)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(84, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "SalesMan:"
        '
        'dtpMonth
        '
        Me.dtpMonth.CustomFormat = "MMM/yyyy"
        Me.dtpMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpMonth.Location = New System.Drawing.Point(120, 54)
        Me.dtpMonth.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpMonth.Name = "dtpMonth"
        Me.dtpMonth.Size = New System.Drawing.Size(286, 26)
        Me.dtpMonth.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.dtpMonth, "Select Month")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 63)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Month:"
        '
        'btnPreview
        '
        Me.btnPreview.Location = New System.Drawing.Point(336, 182)
        Me.btnPreview.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(112, 35)
        Me.btnPreview.TabIndex = 4
        Me.btnPreview.Text = "Preview"
        Me.ToolTip1.SetToolTip(Me.btnPreview, "Preview Report")
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'pnlPeriod
        '
        Me.pnlPeriod.BackColor = System.Drawing.Color.Transparent
        Me.pnlPeriod.Controls.Add(Me.Label1)
        Me.pnlPeriod.Controls.Add(Me.cmbSalesMan)
        Me.pnlPeriod.Controls.Add(Me.dtpMonth)
        Me.pnlPeriod.Controls.Add(Me.Label2)
        Me.pnlPeriod.Location = New System.Drawing.Point(18, 74)
        Me.pnlPeriod.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlPeriod.Name = "pnlPeriod"
        Me.pnlPeriod.Size = New System.Drawing.Size(430, 98)
        Me.pnlPeriod.TabIndex = 32
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnClose)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(598, 65)
        Me.pnlHeader.TabIndex = 114
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.btnClose.Location = New System.Drawing.Point(540, 9)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(54, 46)
        Me.btnClose.TabIndex = 116
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(4, 14)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(445, 36)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Sales Man Achievement Report"
        '
        'rptSalesManTarget
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(598, 237)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.pnlPeriod)
        Me.Controls.Add(Me.btnPreview)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MinimumSize = New System.Drawing.Size(22, 171)
        Me.Name = "rptSalesManTarget"
        Me.Text = "Sales Man Achievement Report"
        Me.pnlPeriod.ResumeLayout(False)
        Me.pnlPeriod.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbSalesMan As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpMonth As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pnlPeriod As System.Windows.Forms.Panel
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button

End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptAgingReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRptAgingReport))
        Me.BtnGenerate = New System.Windows.Forms.Button()
        Me.dtpCurrentDate = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbCostCenter = New System.Windows.Forms.ComboBox()
        Me.cmbCostCenterHead = New System.Windows.Forms.ComboBox()
        Me.lblProjectHead = New System.Windows.Forms.Label()
        Me.lblProjectName = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnGenerate
        '
        Me.BtnGenerate.Location = New System.Drawing.Point(381, 197)
        Me.BtnGenerate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.BtnGenerate.Name = "BtnGenerate"
        Me.BtnGenerate.Size = New System.Drawing.Size(112, 35)
        Me.BtnGenerate.TabIndex = 5
        Me.BtnGenerate.Text = "Generate"
        Me.BtnGenerate.UseVisualStyleBackColor = True
        '
        'dtpCurrentDate
        '
        Me.dtpCurrentDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpCurrentDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtpCurrentDate.DropDownCalendar.Name = ""
        Me.dtpCurrentDate.Location = New System.Drawing.Point(162, 112)
        Me.dtpCurrentDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpCurrentDate.Name = "dtpCurrentDate"
        Me.dtpCurrentDate.Size = New System.Drawing.Size(332, 26)
        Me.dtpCurrentDate.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(36, 117)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 20)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Date:"
        '
        'cmbCostCenter
        '
        Me.cmbCostCenter.FormattingEnabled = True
        Me.cmbCostCenter.Location = New System.Drawing.Point(162, 114)
        Me.cmbCostCenter.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbCostCenter.Name = "cmbCostCenter"
        Me.cmbCostCenter.Size = New System.Drawing.Size(330, 28)
        Me.cmbCostCenter.TabIndex = 2
        Me.cmbCostCenter.Visible = False
        '
        'cmbCostCenterHead
        '
        Me.cmbCostCenterHead.FormattingEnabled = True
        Me.cmbCostCenterHead.Location = New System.Drawing.Point(162, 155)
        Me.cmbCostCenterHead.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbCostCenterHead.Name = "cmbCostCenterHead"
        Me.cmbCostCenterHead.Size = New System.Drawing.Size(330, 28)
        Me.cmbCostCenterHead.TabIndex = 4
        Me.cmbCostCenterHead.Visible = False
        '
        'lblProjectHead
        '
        Me.lblProjectHead.AutoSize = True
        Me.lblProjectHead.Location = New System.Drawing.Point(18, 118)
        Me.lblProjectHead.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProjectHead.Name = "lblProjectHead"
        Me.lblProjectHead.Size = New System.Drawing.Size(101, 20)
        Me.lblProjectHead.TabIndex = 0
        Me.lblProjectHead.Text = "Project Head"
        Me.lblProjectHead.Visible = False
        '
        'lblProjectName
        '
        Me.lblProjectName.AutoSize = True
        Me.lblProjectName.Location = New System.Drawing.Point(18, 160)
        Me.lblProjectName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProjectName.Name = "lblProjectName"
        Me.lblProjectName.Size = New System.Drawing.Size(104, 20)
        Me.lblProjectName.TabIndex = 3
        Me.lblProjectName.Text = "Project Name"
        Me.lblProjectName.Visible = False
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(606, 77)
        Me.pnlHeader.TabIndex = 67
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(16, 22)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(201, 36)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Aging Report"
        '
        'frmRptAgingReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(606, 260)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.lblProjectName)
        Me.Controls.Add(Me.lblProjectHead)
        Me.Controls.Add(Me.cmbCostCenterHead)
        Me.Controls.Add(Me.cmbCostCenter)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtpCurrentDate)
        Me.Controls.Add(Me.BtnGenerate)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRptAgingReport"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmRptAgingReport"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BtnGenerate As System.Windows.Forms.Button
    Friend WithEvents dtpCurrentDate As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbCostCenter As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCostCenterHead As System.Windows.Forms.ComboBox
    Friend WithEvents lblProjectHead As System.Windows.Forms.Label
    Friend WithEvents lblProjectName As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
End Class

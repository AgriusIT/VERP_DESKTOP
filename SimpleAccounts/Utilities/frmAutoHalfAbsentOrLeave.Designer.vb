<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAutoHalfAbsentOrLeave
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAutoHalfAbsentOrLeave))
        Me.grdAutoOT = New Janus.Windows.GridEX.GridEX()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.dtpMonth = New System.Windows.Forms.DateTimePicker()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        CType(Me.grdAutoOT, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdAutoOT
        '
        Me.grdAutoOT.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdAutoOT.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdAutoOT.CardWidth = 804
        Me.grdAutoOT.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.grdAutoOT.GroupByBoxVisible = False
        Me.grdAutoOT.Location = New System.Drawing.Point(-2, 125)
        Me.grdAutoOT.Name = "grdAutoOT"
        Me.grdAutoOT.RecordNavigator = True
        Me.grdAutoOT.Size = New System.Drawing.Size(442, 302)
        Me.grdAutoOT.TabIndex = 17
        Me.grdAutoOT.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdAutoOT.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdAutoOT.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(12, 12)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(194, 23)
        Me.lblHeader.TabIndex = 18
        Me.lblHeader.Text = "Auto Half Absent"
        '
        'dtpMonth
        '
        Me.dtpMonth.CustomFormat = "dd/MMM/yyyy"
        Me.dtpMonth.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpMonth.Location = New System.Drawing.Point(53, 74)
        Me.dtpMonth.Name = "dtpMonth"
        Me.dtpMonth.Size = New System.Drawing.Size(173, 20)
        Me.dtpMonth.TabIndex = 20
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.Location = New System.Drawing.Point(13, 78)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(36, 13)
        Me.lblFrom.TabIndex = 21
        Me.lblFrom.Text = "From :"
        '
        'btnLoad
        '
        Me.btnLoad.Location = New System.Drawing.Point(232, 97)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(67, 23)
        Me.btnLoad.TabIndex = 23
        Me.btnLoad.Text = "Load"
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh, Me.btnSave})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(440, 25)
        Me.ToolStrip1.TabIndex = 24
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(66, 22)
        Me.btnRefresh.Text = "Refresh"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(51, 22)
        Me.btnSave.Text = "&Save"
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.Location = New System.Drawing.Point(13, 103)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(26, 13)
        Me.lblTo.TabIndex = 26
        Me.lblTo.Text = "To :"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd/MMM/yyyy"
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(53, 99)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(173, 20)
        Me.DateTimePicker1.TabIndex = 25
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 25)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(440, 43)
        Me.pnlHeader.TabIndex = 27
        '
        'frmAutoHalfAbsentOrLeave
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(440, 427)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.lblTo)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.lblFrom)
        Me.Controls.Add(Me.dtpMonth)
        Me.Controls.Add(Me.grdAutoOT)
        Me.Name = "frmAutoHalfAbsentOrLeave"
        Me.Text = "Employee Auto Half Absent"
        CType(Me.grdAutoOT, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdAutoOT As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents dtpMonth As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

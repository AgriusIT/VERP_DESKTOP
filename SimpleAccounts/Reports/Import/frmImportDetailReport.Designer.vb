<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImportDetailReport
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
        Me.grdImportDetail = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.gbDateRange = New System.Windows.Forms.GroupBox()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        CType(Me.grdImportDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.gbDateRange.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdImportDetail
        '
        Me.grdImportDetail.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdImportDetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdImportDetail.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdImportDetail.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdImportDetail.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdImportDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdImportDetail.Location = New System.Drawing.Point(4, 283)
        Me.grdImportDetail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdImportDetail.Name = "grdImportDetail"
        Me.grdImportDetail.Size = New System.Drawing.Size(852, 509)
        Me.grdImportDetail.TabIndex = 0
        Me.grdImportDetail.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdImportDetail.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdImportDetail.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(860, 32)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 29)
        Me.btnRefresh.Text = "&Refresh"
        Me.btnRefresh.ToolTipText = "This button helps to reset dates"
        '
        'gbDateRange
        '
        Me.gbDateRange.Controls.Add(Me.lblTo)
        Me.gbDateRange.Controls.Add(Me.lblFrom)
        Me.gbDateRange.Controls.Add(Me.dtpTo)
        Me.gbDateRange.Controls.Add(Me.dtpFrom)
        Me.gbDateRange.Location = New System.Drawing.Point(18, 105)
        Me.gbDateRange.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.gbDateRange.Name = "gbDateRange"
        Me.gbDateRange.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.gbDateRange.Size = New System.Drawing.Size(786, 123)
        Me.gbDateRange.TabIndex = 2
        Me.gbDateRange.TabStop = False
        Me.gbDateRange.Text = "Date Range"
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.Location = New System.Drawing.Point(372, 55)
        Me.lblTo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(27, 20)
        Me.lblTo.TabIndex = 3
        Me.lblTo.Text = "To"
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.Location = New System.Drawing.Point(9, 55)
        Me.lblFrom.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(46, 20)
        Me.lblFrom.TabIndex = 2
        Me.lblFrom.Text = "From"
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd/MM/yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(411, 49)
        Me.dtpTo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(298, 26)
        Me.dtpTo.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.dtpTo, "Data will be loaded till this date")
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd/MM/yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(63, 49)
        Me.dtpFrom.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(298, 26)
        Me.dtpFrom.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.dtpFrom, "Data will be loaded from this date")
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(12, 12)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(202, 36)
        Me.lblHeader.TabIndex = 3
        Me.lblHeader.Text = "Import Detail"
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(692, 237)
        Me.btnShow.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(112, 35)
        Me.btnShow.TabIndex = 5
        Me.btnShow.Text = "Show"
        Me.ToolTip1.SetToolTip(Me.btnShow, "Click this button to show data.")
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(807, 0)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar1.MyGrid = Me.grdImportDetail
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(50, 38)
        Me.CtrlGrdBar1.TabIndex = 4
        Me.CtrlGrdBar1.TabStop = False
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 32)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(860, 58)
        Me.pnlHeader.TabIndex = 15
        '
        'frmImportDetailReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(860, 794)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.btnShow)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.gbDateRange)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.grdImportDetail)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmImportDetailReport"
        Me.Text = "Import Detail Report"
        CType(Me.grdImportDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.gbDateRange.ResumeLayout(False)
        Me.gbDateRange.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdImportDetail As Janus.Windows.GridEX.GridEX
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents gbDateRange As System.Windows.Forms.GroupBox
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

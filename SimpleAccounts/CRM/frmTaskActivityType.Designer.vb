<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTaskActivityType
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
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.txtType = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.lblType = New System.Windows.Forms.Label()
        Me.lblRemarks = New System.Windows.Forms.Label()
        Me.lblProgress1 = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.ToolStrip1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Transparent
        Me.lblHeader.Location = New System.Drawing.Point(18, 8)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(227, 35)
        Me.lblHeader.TabIndex = 2
        Me.lblHeader.Text = "Activity Type"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(600, 32)
        Me.ToolStrip1.TabIndex = 3
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Image = Global.SimpleAccounts.My.Resources.Resources.save_labled
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(77, 29)
        Me.ToolStripButton1.Text = "Save"
        '
        'txtType
        '
        Me.txtType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtType.Location = New System.Drawing.Point(110, 109)
        Me.txtType.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtType.Name = "txtType"
        Me.txtType.Size = New System.Drawing.Size(470, 26)
        Me.txtType.TabIndex = 4
        '
        'txtRemarks
        '
        Me.txtRemarks.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtRemarks.Location = New System.Drawing.Point(110, 149)
        Me.txtRemarks.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(470, 53)
        Me.txtRemarks.TabIndex = 5
        '
        'lblType
        '
        Me.lblType.AutoSize = True
        Me.lblType.Location = New System.Drawing.Point(48, 111)
        Me.lblType.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblType.Name = "lblType"
        Me.lblType.Size = New System.Drawing.Size(51, 20)
        Me.lblType.TabIndex = 6
        Me.lblType.Text = "Type :"
        '
        'lblRemarks
        '
        Me.lblRemarks.AutoSize = True
        Me.lblRemarks.Location = New System.Drawing.Point(21, 154)
        Me.lblRemarks.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(81, 20)
        Me.lblRemarks.TabIndex = 7
        Me.lblRemarks.Text = "Remarks :"
        '
        'lblProgress1
        '
        Me.lblProgress1.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress1.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress1.Location = New System.Drawing.Point(105, 262)
        Me.lblProgress1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProgress1.Name = "lblProgress1"
        Me.lblProgress1.Size = New System.Drawing.Size(394, 69)
        Me.lblProgress1.TabIndex = 43
        Me.lblProgress1.Text = "Processing please wait ..."
        Me.lblProgress1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress1.Visible = False
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.Teal
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 32)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(600, 58)
        Me.pnlHeader.TabIndex = 44
        '
        'frmTaskActivityType
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(600, 345)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.lblProgress1)
        Me.Controls.Add(Me.lblRemarks)
        Me.Controls.Add(Me.lblType)
        Me.Controls.Add(Me.txtRemarks)
        Me.Controls.Add(Me.txtType)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmTaskActivityType"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Task Activity Type"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtType As System.Windows.Forms.TextBox
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents lblType As System.Windows.Forms.Label
    Friend WithEvents lblRemarks As System.Windows.Forms.Label
    Friend WithEvents lblProgress1 As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

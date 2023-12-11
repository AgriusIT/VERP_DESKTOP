<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDialog
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
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.cmbRawMaterial = New System.Windows.Forms.ComboBox()
        Me.txtQty = New System.Windows.Forms.TextBox()
        Me.lblMaterial = New System.Windows.Forms.Label()
        Me.lblQty = New System.Windows.Forms.Label()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(183, 109)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(67, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "&Cancel"
        '
        'btnSave
        '
        Me.btnSave.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnSave.Location = New System.Drawing.Point(110, 109)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(67, 23)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "&Save"
        '
        'cmbRawMaterial
        '
        Me.cmbRawMaterial.FormattingEnabled = True
        Me.cmbRawMaterial.Location = New System.Drawing.Point(62, 57)
        Me.cmbRawMaterial.Name = "cmbRawMaterial"
        Me.cmbRawMaterial.Size = New System.Drawing.Size(188, 21)
        Me.cmbRawMaterial.TabIndex = 1
        '
        'txtQty
        '
        Me.txtQty.Location = New System.Drawing.Point(62, 84)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(188, 20)
        Me.txtQty.TabIndex = 2
        '
        'lblMaterial
        '
        Me.lblMaterial.AutoSize = True
        Me.lblMaterial.Location = New System.Drawing.Point(12, 60)
        Me.lblMaterial.Name = "lblMaterial"
        Me.lblMaterial.Size = New System.Drawing.Size(44, 13)
        Me.lblMaterial.TabIndex = 4
        Me.lblMaterial.Text = "Material"
        '
        'lblQty
        '
        Me.lblQty.AutoSize = True
        Me.lblQty.Location = New System.Drawing.Point(12, 87)
        Me.lblQty.Name = "lblQty"
        Me.lblQty.Size = New System.Drawing.Size(23, 13)
        Me.lblQty.TabIndex = 4
        Me.lblQty.Text = "Qty"
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(11, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(153, 23)
        Me.lblHeader.TabIndex = 26
        Me.lblHeader.Text = "Raw Material"
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(303, 43)
        Me.pnlHeader.TabIndex = 27
        '
        'frmDialog
        '
        Me.AcceptButton = Me.btnSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(303, 177)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.lblQty)
        Me.Controls.Add(Me.lblMaterial)
        Me.Controls.Add(Me.txtQty)
        Me.Controls.Add(Me.cmbRawMaterial)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDialog"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Raw Material Dialog"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents cmbRawMaterial As ComboBox
    Friend WithEvents txtQty As TextBox
    Friend WithEvents lblMaterial As Label
    Friend WithEvents lblQty As Label
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMessages
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMessages))
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.btnNo = New System.Windows.Forms.Button()
        Me.btnYes = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkEnableVoucherPrints = New System.Windows.Forms.CheckBox()
        Me.chkEnableSlipPrints = New System.Windows.Forms.CheckBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.chkEnablePrint = New System.Windows.Forms.CheckBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblMessage
        '
        Me.lblMessage.BackColor = System.Drawing.Color.Transparent
        Me.lblMessage.Font = New System.Drawing.Font("Verdana", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.lblMessage.Location = New System.Drawing.Point(5, 18)
        Me.lblMessage.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(368, 103)
        Me.lblMessage.TabIndex = 0
        Me.lblMessage.Text = "Agrius ERP"
        Me.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnNo
        '
        Me.btnNo.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.btnNo.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnNo.FlatAppearance.BorderSize = 0
        Me.btnNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNo.ForeColor = System.Drawing.Color.White
        Me.btnNo.Location = New System.Drawing.Point(186, 165)
        Me.btnNo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnNo.Name = "btnNo"
        Me.btnNo.Size = New System.Drawing.Size(107, 28)
        Me.btnNo.TabIndex = 2
        Me.btnNo.Text = "No"
        Me.btnNo.UseVisualStyleBackColor = False
        '
        'btnYes
        '
        Me.btnYes.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnYes.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes
        Me.btnYes.FlatAppearance.BorderSize = 0
        Me.btnYes.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnYes.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnYes.ForeColor = System.Drawing.Color.White
        Me.btnYes.Location = New System.Drawing.Point(70, 165)
        Me.btnYes.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnYes.Name = "btnYes"
        Me.btnYes.Size = New System.Drawing.Size(107, 28)
        Me.btnYes.TabIndex = 1
        Me.btnYes.Text = "Yes"
        Me.btnYes.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.AutoSize = True
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Location = New System.Drawing.Point(165, 36)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(276, 82)
        Me.Panel1.TabIndex = 0
        '
        'chkEnableVoucherPrints
        '
        Me.chkEnableVoucherPrints.AutoSize = True
        Me.chkEnableVoucherPrints.BackColor = System.Drawing.Color.Transparent
        Me.chkEnableVoucherPrints.Checked = True
        Me.chkEnableVoucherPrints.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkEnableVoucherPrints.Location = New System.Drawing.Point(136, 97)
        Me.chkEnableVoucherPrints.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkEnableVoucherPrints.Name = "chkEnableVoucherPrints"
        Me.chkEnableVoucherPrints.Size = New System.Drawing.Size(83, 21)
        Me.chkEnableVoucherPrints.TabIndex = 39
        Me.chkEnableVoucherPrints.TabStop = False
        Me.chkEnableVoucherPrints.Text = "Voucher"
        Me.chkEnableVoucherPrints.UseVisualStyleBackColor = False
        Me.chkEnableVoucherPrints.Visible = False
        '
        'chkEnableSlipPrints
        '
        Me.chkEnableSlipPrints.AutoSize = True
        Me.chkEnableSlipPrints.BackColor = System.Drawing.Color.Transparent
        Me.chkEnableSlipPrints.Location = New System.Drawing.Point(232, 97)
        Me.chkEnableSlipPrints.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkEnableSlipPrints.Name = "chkEnableSlipPrints"
        Me.chkEnableSlipPrints.Size = New System.Drawing.Size(53, 21)
        Me.chkEnableSlipPrints.TabIndex = 40
        Me.chkEnableSlipPrints.TabStop = False
        Me.chkEnableSlipPrints.Text = "Slip"
        Me.chkEnableSlipPrints.UseVisualStyleBackColor = False
        Me.chkEnableSlipPrints.Visible = False
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Transparent
        Me.Panel3.BackgroundImage = CType(resources.GetObject("Panel3.BackgroundImage"), System.Drawing.Image)
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.chkEnablePrint)
        Me.Panel3.Controls.Add(Me.chkEnableSlipPrints)
        Me.Panel3.Controls.Add(Me.chkEnableVoucherPrints)
        Me.Panel3.Controls.Add(Me.lblMessage)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(5, 41)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(505, 200)
        Me.Panel3.TabIndex = 6
        '
        'chkEnablePrint
        '
        Me.chkEnablePrint.AutoSize = True
        Me.chkEnablePrint.BackColor = System.Drawing.Color.Transparent
        Me.chkEnablePrint.Checked = True
        Me.chkEnablePrint.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkEnablePrint.Location = New System.Drawing.Point(66, 97)
        Me.chkEnablePrint.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.chkEnablePrint.Name = "chkEnablePrint"
        Me.chkEnablePrint.Size = New System.Drawing.Size(59, 21)
        Me.chkEnablePrint.TabIndex = 41
        Me.chkEnablePrint.TabStop = False
        Me.chkEnablePrint.Text = "Print"
        Me.chkEnablePrint.UseVisualStyleBackColor = False
        Me.chkEnablePrint.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.lblTitle)
        Me.Panel2.Controls.Add(Me.Panel1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(5, 5)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(505, 36)
        Me.Panel2.TabIndex = 5
        '
        'lblTitle
        '
        Me.lblTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblTitle.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.White
        Me.lblTitle.Location = New System.Drawing.Point(16, 2)
        Me.lblTitle.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(497, 36)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "V-ERP"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmMessages
        '
        Me.AcceptButton = Me.btnYes
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(172, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(172, Byte), Integer))
        Me.CancelButton = Me.btnNo
        Me.ClientSize = New System.Drawing.Size(515, 246)
        Me.Controls.Add(Me.btnNo)
        Me.Controls.Add(Me.btnYes)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMessages"
        Me.Padding = New System.Windows.Forms.Padding(5, 5, 5, 5)
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SIRIUS ERP"
        Me.TopMost = True
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnNo As System.Windows.Forms.Button
    Friend WithEvents btnYes As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Public WithEvents chkEnableSlipPrints As System.Windows.Forms.CheckBox
    Public WithEvents chkEnableVoucherPrints As System.Windows.Forms.CheckBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Public WithEvents chkEnablePrint As System.Windows.Forms.CheckBox

End Class

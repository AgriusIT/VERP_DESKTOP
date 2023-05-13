<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImportPopup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImportPopup))
        Me.btnYes = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.cbPrint = New System.Windows.Forms.CheckBox()
        Me.lblVoucherDate = New System.Windows.Forms.Label()
        Me.lblVoucherNo = New System.Windows.Forms.Label()
        Me.lblVoucherReference = New System.Windows.Forms.Label()
        Me.txtVoucherReference = New System.Windows.Forms.TextBox()
        Me.btnNo = New System.Windows.Forms.Button()
        Me.dtpVoucherDate = New System.Windows.Forms.DateTimePicker()
        Me.txtVoucherNo = New System.Windows.Forms.TextBox()
        Me.rbAddNewVoucher = New System.Windows.Forms.RadioButton()
        Me.rbUpdateVoucher = New System.Windows.Forms.RadioButton()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnYes
        '
        Me.btnYes.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnYes.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(214, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.btnYes.DialogResult = System.Windows.Forms.DialogResult.Yes
        Me.btnYes.FlatAppearance.BorderSize = 0
        Me.btnYes.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnYes.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnYes.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnYes.Location = New System.Drawing.Point(229, 157)
        Me.btnYes.Name = "btnYes"
        Me.btnYes.Size = New System.Drawing.Size(80, 23)
        Me.btnYes.TabIndex = 8
        Me.btnYes.Text = "Update"
        Me.btnYes.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.AutoSize = True
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Location = New System.Drawing.Point(124, 29)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(233, 66)
        Me.Panel1.TabIndex = 2
        '
        'Panel3
        '
        Me.Panel3.BackgroundImage = CType(resources.GetObject("Panel3.BackgroundImage"), System.Drawing.Image)
        Me.Panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel3.Controls.Add(Me.cbPrint)
        Me.Panel3.Controls.Add(Me.lblVoucherDate)
        Me.Panel3.Controls.Add(Me.lblVoucherNo)
        Me.Panel3.Controls.Add(Me.lblVoucherReference)
        Me.Panel3.Controls.Add(Me.txtVoucherReference)
        Me.Panel3.Controls.Add(Me.btnNo)
        Me.Panel3.Controls.Add(Me.dtpVoucherDate)
        Me.Panel3.Controls.Add(Me.btnYes)
        Me.Panel3.Controls.Add(Me.txtVoucherNo)
        Me.Panel3.Controls.Add(Me.rbAddNewVoucher)
        Me.Panel3.Controls.Add(Me.rbUpdateVoucher)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(4, 33)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(405, 188)
        Me.Panel3.TabIndex = 0
        '
        'cbPrint
        '
        Me.cbPrint.AutoSize = True
        Me.cbPrint.BackColor = System.Drawing.Color.Transparent
        Me.cbPrint.Location = New System.Drawing.Point(304, 57)
        Me.cbPrint.Name = "cbPrint"
        Me.cbPrint.Size = New System.Drawing.Size(90, 17)
        Me.cbPrint.TabIndex = 10
        Me.cbPrint.Text = "Print Voucher"
        Me.cbPrint.UseVisualStyleBackColor = False
        '
        'lblVoucherDate
        '
        Me.lblVoucherDate.AutoSize = True
        Me.lblVoucherDate.BackColor = System.Drawing.Color.Transparent
        Me.lblVoucherDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVoucherDate.ForeColor = System.Drawing.Color.Black
        Me.lblVoucherDate.Location = New System.Drawing.Point(142, 36)
        Me.lblVoucherDate.Name = "lblVoucherDate"
        Me.lblVoucherDate.Size = New System.Drawing.Size(86, 16)
        Me.lblVoucherDate.TabIndex = 4
        Me.lblVoucherDate.Text = "Voucher Date"
        '
        'lblVoucherNo
        '
        Me.lblVoucherNo.AutoSize = True
        Me.lblVoucherNo.BackColor = System.Drawing.Color.Transparent
        Me.lblVoucherNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVoucherNo.ForeColor = System.Drawing.Color.Black
        Me.lblVoucherNo.Location = New System.Drawing.Point(10, 36)
        Me.lblVoucherNo.Name = "lblVoucherNo"
        Me.lblVoucherNo.Size = New System.Drawing.Size(75, 16)
        Me.lblVoucherNo.TabIndex = 2
        Me.lblVoucherNo.Text = "Voucher No"
        '
        'lblVoucherReference
        '
        Me.lblVoucherReference.AutoSize = True
        Me.lblVoucherReference.BackColor = System.Drawing.Color.Transparent
        Me.lblVoucherReference.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVoucherReference.ForeColor = System.Drawing.Color.Black
        Me.lblVoucherReference.Location = New System.Drawing.Point(12, 82)
        Me.lblVoucherReference.Name = "lblVoucherReference"
        Me.lblVoucherReference.Size = New System.Drawing.Size(117, 16)
        Me.lblVoucherReference.TabIndex = 6
        Me.lblVoucherReference.Text = "Voucher Reference"
        '
        'txtVoucherReference
        '
        Me.txtVoucherReference.Location = New System.Drawing.Point(10, 104)
        Me.txtVoucherReference.Multiline = True
        Me.txtVoucherReference.Name = "txtVoucherReference"
        Me.txtVoucherReference.Size = New System.Drawing.Size(385, 50)
        Me.txtVoucherReference.TabIndex = 7
        '
        'btnNo
        '
        Me.btnNo.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(214, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.btnNo.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnNo.FlatAppearance.BorderSize = 0
        Me.btnNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnNo.Location = New System.Drawing.Point(315, 157)
        Me.btnNo.Name = "btnNo"
        Me.btnNo.Size = New System.Drawing.Size(80, 23)
        Me.btnNo.TabIndex = 9
        Me.btnNo.Text = "Cancel"
        Me.btnNo.UseVisualStyleBackColor = False
        '
        'dtpVoucherDate
        '
        Me.dtpVoucherDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpVoucherDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpVoucherDate.Location = New System.Drawing.Point(145, 55)
        Me.dtpVoucherDate.Name = "dtpVoucherDate"
        Me.dtpVoucherDate.Size = New System.Drawing.Size(153, 20)
        Me.dtpVoucherDate.TabIndex = 5
        '
        'txtVoucherNo
        '
        Me.txtVoucherNo.Enabled = False
        Me.txtVoucherNo.Location = New System.Drawing.Point(10, 55)
        Me.txtVoucherNo.Name = "txtVoucherNo"
        Me.txtVoucherNo.Size = New System.Drawing.Size(129, 20)
        Me.txtVoucherNo.TabIndex = 3
        '
        'rbAddNewVoucher
        '
        Me.rbAddNewVoucher.AutoSize = True
        Me.rbAddNewVoucher.BackColor = System.Drawing.Color.Transparent
        Me.rbAddNewVoucher.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbAddNewVoucher.ForeColor = System.Drawing.Color.Black
        Me.rbAddNewVoucher.Location = New System.Drawing.Point(157, 4)
        Me.rbAddNewVoucher.Name = "rbAddNewVoucher"
        Me.rbAddNewVoucher.Size = New System.Drawing.Size(124, 20)
        Me.rbAddNewVoucher.TabIndex = 1
        Me.rbAddNewVoucher.TabStop = True
        Me.rbAddNewVoucher.Text = "Add new voucher"
        Me.rbAddNewVoucher.UseVisualStyleBackColor = False
        '
        'rbUpdateVoucher
        '
        Me.rbUpdateVoucher.AutoSize = True
        Me.rbUpdateVoucher.BackColor = System.Drawing.Color.Transparent
        Me.rbUpdateVoucher.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbUpdateVoucher.ForeColor = System.Drawing.Color.Black
        Me.rbUpdateVoucher.Location = New System.Drawing.Point(33, 4)
        Me.rbUpdateVoucher.Name = "rbUpdateVoucher"
        Me.rbUpdateVoucher.Size = New System.Drawing.Size(115, 20)
        Me.rbUpdateVoucher.TabIndex = 0
        Me.rbUpdateVoucher.TabStop = True
        Me.rbUpdateVoucher.Text = "Update voucher"
        Me.rbUpdateVoucher.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(104, Byte), Integer), CType(CType(214, Byte), Integer), CType(CType(243, Byte), Integer))
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.lblMessage)
        Me.Panel2.Controls.Add(Me.lblTitle)
        Me.Panel2.Controls.Add(Me.Panel1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(4, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(405, 29)
        Me.Panel2.TabIndex = 5
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = True
        Me.lblMessage.ForeColor = System.Drawing.Color.DarkRed
        Me.lblMessage.Location = New System.Drawing.Point(90, 9)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(39, 13)
        Me.lblMessage.TabIndex = 1
        Me.lblMessage.Text = "Label1"
        '
        'lblTitle
        '
        Me.lblTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblTitle.Font = New System.Drawing.Font("Arial", 11.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.lblTitle.Location = New System.Drawing.Point(0, 1)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(402, 29)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Agrius ERP"
        Me.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'frmImportPopup
        '
        Me.AcceptButton = Me.btnYes
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(172, Byte), Integer), CType(CType(172, Byte), Integer), CType(CType(172, Byte), Integer))
        Me.CancelButton = Me.btnNo
        Me.ClientSize = New System.Drawing.Size(413, 225)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmImportPopup"
        Me.Padding = New System.Windows.Forms.Padding(4)
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
    Friend WithEvents btnYes As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents txtVoucherReference As System.Windows.Forms.TextBox
    Friend WithEvents dtpVoucherDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtVoucherNo As System.Windows.Forms.TextBox
    Friend WithEvents rbAddNewVoucher As System.Windows.Forms.RadioButton
    Friend WithEvents rbUpdateVoucher As System.Windows.Forms.RadioButton
    Friend WithEvents lblVoucherDate As System.Windows.Forms.Label
    Friend WithEvents lblVoucherNo As System.Windows.Forms.Label
    Friend WithEvents lblVoucherReference As System.Windows.Forms.Label
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents cbPrint As System.Windows.Forms.CheckBox
    Friend WithEvents btnNo As System.Windows.Forms.Button

End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMixPayment
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMixPayment))
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtCash = New System.Windows.Forms.TextBox()
        Me.txtBank = New System.Windows.Forms.TextBox()
        Me.txtCredit = New System.Windows.Forms.TextBox()
        Me.txtCreditCard = New System.Windows.Forms.TextBox()
        Me.cmbBank = New System.Windows.Forms.ComboBox()
        Me.lblCash = New System.Windows.Forms.Label()
        Me.lblBank = New System.Windows.Forms.Label()
        Me.lblCredit = New System.Windows.Forms.Label()
        Me.lblCCAmount = New System.Windows.Forms.Label()
        Me.lblChequeNo = New System.Windows.Forms.Label()
        Me.txtChequeNo = New System.Windows.Forms.TextBox()
        Me.cmbCCAccount = New System.Windows.Forms.ComboBox()
        Me.lblCCAccount = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.chkOnline = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnClose)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(527, 44)
        Me.pnlHeader.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.btnClose.Location = New System.Drawing.Point(483, 10)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(32, 24)
        Me.btnClose.TabIndex = 1
        Me.btnClose.TabStop = False
        Me.ToolTip1.SetToolTip(Me.btnClose, "Close")
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 12.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.lblHeader.Location = New System.Drawing.Point(20, 11)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(131, 20)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Mix Payment"
        '
        'txtCash
        '
        Me.txtCash.Location = New System.Drawing.Point(24, 78)
        Me.txtCash.Name = "txtCash"
        Me.txtCash.Size = New System.Drawing.Size(172, 20)
        Me.txtCash.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.txtCash, "Enter Cash Amount")
        '
        'txtBank
        '
        Me.txtBank.Location = New System.Drawing.Point(207, 129)
        Me.txtBank.Name = "txtBank"
        Me.txtBank.Size = New System.Drawing.Size(172, 20)
        Me.txtBank.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.txtBank, "Enter Bank Amount")
        '
        'txtCredit
        '
        Me.txtCredit.Location = New System.Drawing.Point(207, 78)
        Me.txtCredit.Name = "txtCredit"
        Me.txtCredit.Size = New System.Drawing.Size(172, 20)
        Me.txtCredit.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.txtCredit, "Enter Credit Amount")
        '
        'txtCreditCard
        '
        Me.txtCreditCard.Location = New System.Drawing.Point(207, 179)
        Me.txtCreditCard.Name = "txtCreditCard"
        Me.txtCreditCard.Size = New System.Drawing.Size(172, 20)
        Me.txtCreditCard.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.txtCreditCard, "Enter Credit Card Amount")
        '
        'cmbBank
        '
        Me.cmbBank.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbBank.DropDownWidth = 200
        Me.cmbBank.FormattingEnabled = True
        Me.cmbBank.Location = New System.Drawing.Point(24, 128)
        Me.cmbBank.Name = "cmbBank"
        Me.cmbBank.Size = New System.Drawing.Size(172, 21)
        Me.cmbBank.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.cmbBank, "Select a Bank ")
        '
        'lblCash
        '
        Me.lblCash.AutoSize = True
        Me.lblCash.Location = New System.Drawing.Point(21, 62)
        Me.lblCash.Name = "lblCash"
        Me.lblCash.Size = New System.Drawing.Size(70, 13)
        Me.lblCash.TabIndex = 1
        Me.lblCash.Text = "Cash Amount"
        '
        'lblBank
        '
        Me.lblBank.AutoSize = True
        Me.lblBank.Location = New System.Drawing.Point(206, 112)
        Me.lblBank.Name = "lblBank"
        Me.lblBank.Size = New System.Drawing.Size(71, 13)
        Me.lblBank.TabIndex = 5
        Me.lblBank.Text = "Bank Amount"
        '
        'lblCredit
        '
        Me.lblCredit.AutoSize = True
        Me.lblCredit.Location = New System.Drawing.Point(204, 62)
        Me.lblCredit.Name = "lblCredit"
        Me.lblCredit.Size = New System.Drawing.Size(73, 13)
        Me.lblCredit.TabIndex = 3
        Me.lblCredit.Text = "Credit Amount"
        '
        'lblCCAmount
        '
        Me.lblCCAmount.AutoSize = True
        Me.lblCCAmount.Location = New System.Drawing.Point(204, 163)
        Me.lblCCAmount.Name = "lblCCAmount"
        Me.lblCCAmount.Size = New System.Drawing.Size(98, 13)
        Me.lblCCAmount.TabIndex = 9
        Me.lblCCAmount.Text = "Credit Card Amount"
        '
        'lblChequeNo
        '
        Me.lblChequeNo.AutoSize = True
        Me.lblChequeNo.Location = New System.Drawing.Point(392, 112)
        Me.lblChequeNo.Name = "lblChequeNo"
        Me.lblChequeNo.Size = New System.Drawing.Size(44, 13)
        Me.lblChequeNo.TabIndex = 7
        Me.lblChequeNo.Text = "Cheque"
        '
        'txtChequeNo
        '
        Me.txtChequeNo.Location = New System.Drawing.Point(395, 129)
        Me.txtChequeNo.Name = "txtChequeNo"
        Me.txtChequeNo.Size = New System.Drawing.Size(94, 20)
        Me.txtChequeNo.TabIndex = 8
        '
        'cmbCCAccount
        '
        Me.cmbCCAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCCAccount.FormattingEnabled = True
        Me.cmbCCAccount.Location = New System.Drawing.Point(24, 179)
        Me.cmbCCAccount.Name = "cmbCCAccount"
        Me.cmbCCAccount.Size = New System.Drawing.Size(172, 21)
        Me.cmbCCAccount.TabIndex = 12
        '
        'lblCCAccount
        '
        Me.lblCCAccount.AutoSize = True
        Me.lblCCAccount.Location = New System.Drawing.Point(21, 163)
        Me.lblCCAccount.Name = "lblCCAccount"
        Me.lblCCAccount.Size = New System.Drawing.Size(64, 13)
        Me.lblCCAccount.TabIndex = 11
        Me.lblCCAccount.Text = "CC Account"
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.Button1.Location = New System.Drawing.Point(395, 178)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(94, 21)
        Me.Button1.TabIndex = 13
        Me.Button1.Text = "OK"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'chkOnline
        '
        Me.chkOnline.AutoSize = True
        Me.chkOnline.Location = New System.Drawing.Point(144, 111)
        Me.chkOnline.Name = "chkOnline"
        Me.chkOnline.Size = New System.Drawing.Size(56, 17)
        Me.chkOnline.TabIndex = 16
        Me.chkOnline.Text = "Online"
        Me.chkOnline.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(21, 112)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 13)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Bank"
        '
        'frmMixPayment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(527, 234)
        Me.Controls.Add(Me.chkOnline)
        Me.Controls.Add(Me.cmbBank)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.cmbCCAccount)
        Me.Controls.Add(Me.txtCreditCard)
        Me.Controls.Add(Me.txtChequeNo)
        Me.Controls.Add(Me.txtBank)
        Me.Controls.Add(Me.txtCredit)
        Me.Controls.Add(Me.txtCash)
        Me.Controls.Add(Me.lblCCAmount)
        Me.Controls.Add(Me.lblCredit)
        Me.Controls.Add(Me.lblCCAccount)
        Me.Controls.Add(Me.lblChequeNo)
        Me.Controls.Add(Me.lblBank)
        Me.Controls.Add(Me.lblCash)
        Me.Controls.Add(Me.pnlHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmMixPayment"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mix Payment"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lblCash As System.Windows.Forms.Label
    Friend WithEvents txtCash As System.Windows.Forms.TextBox
    Friend WithEvents lblBank As System.Windows.Forms.Label
    Friend WithEvents txtBank As System.Windows.Forms.TextBox
    Friend WithEvents lblCredit As System.Windows.Forms.Label
    Friend WithEvents lblCCAmount As System.Windows.Forms.Label
    Friend WithEvents txtCredit As System.Windows.Forms.TextBox
    Friend WithEvents txtCreditCard As System.Windows.Forms.TextBox
    Friend WithEvents lblChequeNo As System.Windows.Forms.Label
    Friend WithEvents txtChequeNo As System.Windows.Forms.TextBox
    Friend WithEvents cmbCCAccount As System.Windows.Forms.ComboBox
    Friend WithEvents lblCCAccount As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents chkOnline As System.Windows.Forms.CheckBox
    Friend WithEvents cmbBank As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class

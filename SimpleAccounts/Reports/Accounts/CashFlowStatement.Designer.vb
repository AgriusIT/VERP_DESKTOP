<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CashFlowStatement
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CashFlowStatement))
        Me.pnlSalesType = New System.Windows.Forms.Panel()
        Me.rdoBoth = New System.Windows.Forms.RadioButton()
        Me.rdoBank = New System.Windows.Forms.RadioButton()
        Me.rdoCash = New System.Windows.Forms.RadioButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.pnlPeriod = New System.Windows.Forms.Panel()
        Me.chkIncludeCheque = New System.Windows.Forms.CheckBox()
        Me.chkUnposted = New System.Windows.Forms.CheckBox()
        Me.pnlVendorCustomer = New System.Windows.Forms.Panel()
        Me.lblAccount = New System.Windows.Forms.Label()
        Me.cmbCashAccount = New System.Windows.Forms.ComboBox()
        Me.pnlCost = New System.Windows.Forms.Panel()
        Me.lblCostCenter = New System.Windows.Forms.Label()
        Me.cmbCostCenter = New System.Windows.Forms.ComboBox()
        Me.pnlSalesType.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.pnlPeriod.SuspendLayout()
        Me.pnlVendorCustomer.SuspendLayout()
        Me.pnlCost.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlSalesType
        '
        Me.pnlSalesType.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.pnlSalesType.Controls.Add(Me.rdoBoth)
        Me.pnlSalesType.Controls.Add(Me.rdoBank)
        Me.pnlSalesType.Controls.Add(Me.rdoCash)
        Me.pnlSalesType.Location = New System.Drawing.Point(18, 386)
        Me.pnlSalesType.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlSalesType.Name = "pnlSalesType"
        Me.pnlSalesType.Size = New System.Drawing.Size(540, 60)
        Me.pnlSalesType.TabIndex = 26
        '
        'rdoBoth
        '
        Me.rdoBoth.AutoSize = True
        Me.rdoBoth.Location = New System.Drawing.Point(225, 17)
        Me.rdoBoth.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rdoBoth.Name = "rdoBoth"
        Me.rdoBoth.Size = New System.Drawing.Size(68, 24)
        Me.rdoBoth.TabIndex = 5
        Me.rdoBoth.Text = "Both"
        Me.rdoBoth.UseVisualStyleBackColor = True
        '
        'rdoBank
        '
        Me.rdoBank.AutoSize = True
        Me.rdoBank.Location = New System.Drawing.Point(122, 17)
        Me.rdoBank.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rdoBank.Name = "rdoBank"
        Me.rdoBank.Size = New System.Drawing.Size(71, 24)
        Me.rdoBank.TabIndex = 4
        Me.rdoBank.Text = "Bank"
        Me.rdoBank.UseVisualStyleBackColor = True
        '
        'rdoCash
        '
        Me.rdoCash.AutoSize = True
        Me.rdoCash.Checked = True
        Me.rdoCash.Location = New System.Drawing.Point(24, 17)
        Me.rdoCash.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rdoCash.Name = "rdoCash"
        Me.rdoCash.Size = New System.Drawing.Size(71, 24)
        Me.rdoCash.TabIndex = 3
        Me.rdoCash.TabStop = True
        Me.rdoCash.Text = "Cash"
        Me.rdoCash.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 15)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(54, 20)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Period"
        '
        'cmbPeriod
        '
        Me.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPeriod.FormattingEnabled = True
        Me.cmbPeriod.Items.AddRange(New Object() {"Today", "Yesterday", "Current Week", "Current Month", "Current Year"})
        Me.cmbPeriod.Location = New System.Drawing.Point(122, 11)
        Me.cmbPeriod.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(403, 28)
        Me.cmbPeriod.TabIndex = 19
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(304, 62)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 20)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "To"
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.Location = New System.Drawing.Point(8, 62)
        Me.lblFrom.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(46, 20)
        Me.lblFrom.TabIndex = 20
        Me.lblFrom.Text = "From"
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.CustomFormat = "dd/MMM/yyyy"
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2.Location = New System.Drawing.Point(351, 52)
        Me.DateTimePicker2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(174, 26)
        Me.DateTimePicker2.TabIndex = 23
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd/MMM/yyyy"
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(122, 52)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(172, 26)
        Me.DateTimePicker1.TabIndex = 21
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.Controls.Add(Me.btnPrint, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(314, 455)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(244, 45)
        Me.TableLayoutPanel1.TabIndex = 27
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnPrint.Location = New System.Drawing.Point(120, 5)
        Me.btnPrint.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(118, 35)
        Me.btnPrint.TabIndex = 1
        Me.btnPrint.Text = "Print"
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(4, 5)
        Me.OK_Button.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(106, 35)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "Show"
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
        Me.pnlHeader.Size = New System.Drawing.Size(576, 65)
        Me.pnlHeader.TabIndex = 112
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.btnClose.Location = New System.Drawing.Point(504, 8)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(54, 46)
        Me.btnClose.TabIndex = 116
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(4, 14)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(285, 35)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Cash Flow Statement"
        '
        'pnlPeriod
        '
        Me.pnlPeriod.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.pnlPeriod.Controls.Add(Me.chkIncludeCheque)
        Me.pnlPeriod.Controls.Add(Me.chkUnposted)
        Me.pnlPeriod.Controls.Add(Me.cmbPeriod)
        Me.pnlPeriod.Controls.Add(Me.Label3)
        Me.pnlPeriod.Controls.Add(Me.lblFrom)
        Me.pnlPeriod.Controls.Add(Me.DateTimePicker1)
        Me.pnlPeriod.Controls.Add(Me.Label2)
        Me.pnlPeriod.Controls.Add(Me.DateTimePicker2)
        Me.pnlPeriod.Location = New System.Drawing.Point(18, 212)
        Me.pnlPeriod.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlPeriod.Name = "pnlPeriod"
        Me.pnlPeriod.Size = New System.Drawing.Size(540, 165)
        Me.pnlPeriod.TabIndex = 31
        '
        'chkIncludeCheque
        '
        Me.chkIncludeCheque.AutoSize = True
        Me.chkIncludeCheque.Checked = True
        Me.chkIncludeCheque.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIncludeCheque.Location = New System.Drawing.Point(120, 128)
        Me.chkIncludeCheque.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkIncludeCheque.Name = "chkIncludeCheque"
        Me.chkIncludeCheque.Size = New System.Drawing.Size(231, 24)
        Me.chkIncludeCheque.TabIndex = 10
        Me.chkIncludeCheque.Text = "Include Post Dated Cheque"
        Me.chkIncludeCheque.UseVisualStyleBackColor = True
        '
        'chkUnposted
        '
        Me.chkUnposted.AutoSize = True
        Me.chkUnposted.Checked = True
        Me.chkUnposted.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkUnposted.Location = New System.Drawing.Point(120, 92)
        Me.chkUnposted.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkUnposted.Name = "chkUnposted"
        Me.chkUnposted.Size = New System.Drawing.Size(225, 24)
        Me.chkUnposted.TabIndex = 11
        Me.chkUnposted.Text = "Inclue UnPosted Vouchers"
        Me.chkUnposted.UseVisualStyleBackColor = True
        '
        'pnlVendorCustomer
        '
        Me.pnlVendorCustomer.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.pnlVendorCustomer.Controls.Add(Me.lblAccount)
        Me.pnlVendorCustomer.Controls.Add(Me.cmbCashAccount)
        Me.pnlVendorCustomer.Location = New System.Drawing.Point(18, 143)
        Me.pnlVendorCustomer.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlVendorCustomer.Name = "pnlVendorCustomer"
        Me.pnlVendorCustomer.Size = New System.Drawing.Size(540, 60)
        Me.pnlVendorCustomer.TabIndex = 2
        '
        'lblAccount
        '
        Me.lblAccount.AutoSize = True
        Me.lblAccount.Location = New System.Drawing.Point(4, 14)
        Me.lblAccount.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAccount.Name = "lblAccount"
        Me.lblAccount.Size = New System.Drawing.Size(68, 20)
        Me.lblAccount.TabIndex = 1
        Me.lblAccount.Text = "Account"
        '
        'cmbCashAccount
        '
        Me.cmbCashAccount.FormattingEnabled = True
        Me.cmbCashAccount.Location = New System.Drawing.Point(120, 9)
        Me.cmbCashAccount.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbCashAccount.Name = "cmbCashAccount"
        Me.cmbCashAccount.Size = New System.Drawing.Size(404, 28)
        Me.cmbCashAccount.TabIndex = 2
        '
        'pnlCost
        '
        Me.pnlCost.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.pnlCost.Controls.Add(Me.lblCostCenter)
        Me.pnlCost.Controls.Add(Me.cmbCostCenter)
        Me.pnlCost.Location = New System.Drawing.Point(18, 74)
        Me.pnlCost.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlCost.Name = "pnlCost"
        Me.pnlCost.Size = New System.Drawing.Size(540, 60)
        Me.pnlCost.TabIndex = 113
        Me.pnlCost.Visible = False
        '
        'lblCostCenter
        '
        Me.lblCostCenter.AutoSize = True
        Me.lblCostCenter.Location = New System.Drawing.Point(4, 17)
        Me.lblCostCenter.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCostCenter.Name = "lblCostCenter"
        Me.lblCostCenter.Size = New System.Drawing.Size(94, 20)
        Me.lblCostCenter.TabIndex = 0
        Me.lblCostCenter.Text = "Cost Center"
        '
        'cmbCostCenter
        '
        Me.cmbCostCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCostCenter.FormattingEnabled = True
        Me.cmbCostCenter.Location = New System.Drawing.Point(122, 11)
        Me.cmbCostCenter.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbCostCenter.Name = "cmbCostCenter"
        Me.cmbCostCenter.Size = New System.Drawing.Size(403, 28)
        Me.cmbCostCenter.TabIndex = 1
        '
        'CashFlowStatement
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(576, 517)
        Me.Controls.Add(Me.pnlCost)
        Me.Controls.Add(Me.pnlVendorCustomer)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.pnlSalesType)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.pnlPeriod)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "CashFlowStatement"
        Me.Text = "Cash Flow Statement"
        Me.pnlSalesType.ResumeLayout(False)
        Me.pnlSalesType.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlPeriod.ResumeLayout(False)
        Me.pnlPeriod.PerformLayout()
        Me.pnlVendorCustomer.ResumeLayout(False)
        Me.pnlVendorCustomer.PerformLayout()
        Me.pnlCost.ResumeLayout(False)
        Me.pnlCost.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlSalesType As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents pnlPeriod As System.Windows.Forms.Panel
    Friend WithEvents pnlVendorCustomer As System.Windows.Forms.Panel
    Friend WithEvents lblAccount As System.Windows.Forms.Label
    Friend WithEvents cmbCashAccount As System.Windows.Forms.ComboBox
    Friend WithEvents chkIncludeCheque As System.Windows.Forms.CheckBox
    Friend WithEvents chkUnposted As System.Windows.Forms.CheckBox
    Friend WithEvents pnlCost As System.Windows.Forms.Panel
    Friend WithEvents lblCostCenter As System.Windows.Forms.Label
    Friend WithEvents cmbCostCenter As System.Windows.Forms.ComboBox
    Friend WithEvents rdoBoth As System.Windows.Forms.RadioButton
    Friend WithEvents rdoBank As System.Windows.Forms.RadioButton
    Friend WithEvents rdoCash As System.Windows.Forms.RadioButton
    Friend WithEvents btnClose As System.Windows.Forms.Button
End Class

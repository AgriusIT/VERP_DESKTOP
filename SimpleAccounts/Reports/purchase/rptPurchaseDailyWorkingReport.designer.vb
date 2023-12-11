<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class rptPurchaseDailyWorkingReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptPurchaseDailyWorkingReport))
        Me.btnGenerateReport = New System.Windows.Forms.Button()
        Me.grdRcords = New Janus.Windows.GridEX.GridEX()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.rbtall = New System.Windows.Forms.RadioButton()
        Me.rbtcustomer = New System.Windows.Forms.RadioButton()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        Me.rbtRetailPrice = New System.Windows.Forms.RadioButton()
        Me.rbtInvoicePrice = New System.Windows.Forms.RadioButton()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.chkHideZeroValue = New System.Windows.Forms.CheckBox()
        Me.chkUnPostedVoucher = New System.Windows.Forms.CheckBox()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        CType(Me.grdRcords, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnGenerateReport
        '
        Me.btnGenerateReport.Location = New System.Drawing.Point(642, 289)
        Me.btnGenerateReport.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnGenerateReport.Name = "btnGenerateReport"
        Me.btnGenerateReport.Size = New System.Drawing.Size(154, 35)
        Me.btnGenerateReport.TabIndex = 6
        Me.btnGenerateReport.Text = "Generate"
        Me.ToolTip1.SetToolTip(Me.btnGenerateReport, "Generate Report")
        Me.btnGenerateReport.UseVisualStyleBackColor = True
        '
        'grdRcords
        '
        Me.grdRcords.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdRcords.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdRcords.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdRcords.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdRcords.Location = New System.Drawing.Point(2, 334)
        Me.grdRcords.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdRcords.Name = "grdRcords"
        Me.grdRcords.RecordNavigator = True
        Me.grdRcords.Size = New System.Drawing.Size(1058, 674)
        Me.grdRcords.TabIndex = 7
        Me.grdRcords.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 112)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 20)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "To"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 72)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 20)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "From "
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd/MMM/yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(75, 114)
        Me.dtpTo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(220, 26)
        Me.dtpTo.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.dtpTo, "To Date")
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(75, 74)
        Me.dtpFrom.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(220, 26)
        Me.dtpFrom.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.dtpFrom, "From Date")
        '
        'lblHeader
        '
        Me.lblHeader.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(12, 15)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(451, 36)
        Me.lblHeader.TabIndex = 2
        Me.lblHeader.Text = "Purchase Daily Working Report"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnPrint})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1060, 38)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnPrint
        '
        Me.btnPrint.Enabled = False
        Me.btnPrint.Image = Global.SimpleAccounts.My.Resources.Resources.print
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(76, 35)
        Me.btnPrint.Text = "Print"
        '
        'rbtall
        '
        Me.rbtall.AutoSize = True
        Me.rbtall.Checked = True
        Me.rbtall.Location = New System.Drawing.Point(9, 31)
        Me.rbtall.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtall.Name = "rbtall"
        Me.rbtall.Size = New System.Drawing.Size(114, 24)
        Me.rbtall.TabIndex = 0
        Me.rbtall.TabStop = True
        Me.rbtall.Text = "All Account"
        Me.ToolTip1.SetToolTip(Me.rbtall, "View By All Accounts")
        Me.rbtall.UseVisualStyleBackColor = True
        '
        'rbtcustomer
        '
        Me.rbtcustomer.AutoSize = True
        Me.rbtcustomer.Location = New System.Drawing.Point(136, 34)
        Me.rbtcustomer.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtcustomer.Name = "rbtcustomer"
        Me.rbtcustomer.Size = New System.Drawing.Size(121, 24)
        Me.rbtcustomer.TabIndex = 1
        Me.rbtcustomer.Text = "Only Vendor"
        Me.ToolTip1.SetToolTip(Me.rbtcustomer, "View By Only Vendors")
        Me.rbtcustomer.UseVisualStyleBackColor = True
        '
        'cmbPeriod
        '
        Me.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPeriod.FormattingEnabled = True
        Me.cmbPeriod.Items.AddRange(New Object() {"Today", "Yesterday", "Current Week", "Current Month", "Current Year"})
        Me.cmbPeriod.Location = New System.Drawing.Point(75, 32)
        Me.cmbPeriod.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(220, 28)
        Me.cmbPeriod.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbPeriod, "Select Period And Gets Date Range")
        '
        'rbtRetailPrice
        '
        Me.rbtRetailPrice.AutoSize = True
        Me.rbtRetailPrice.Checked = True
        Me.rbtRetailPrice.Location = New System.Drawing.Point(9, 31)
        Me.rbtRetailPrice.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtRetailPrice.Name = "rbtRetailPrice"
        Me.rbtRetailPrice.Size = New System.Drawing.Size(114, 24)
        Me.rbtRetailPrice.TabIndex = 0
        Me.rbtRetailPrice.TabStop = True
        Me.rbtRetailPrice.Text = "Retail Price"
        Me.ToolTip1.SetToolTip(Me.rbtRetailPrice, "View By All Accounts")
        Me.rbtRetailPrice.UseVisualStyleBackColor = True
        '
        'rbtInvoicePrice
        '
        Me.rbtInvoicePrice.AutoSize = True
        Me.rbtInvoicePrice.Location = New System.Drawing.Point(136, 31)
        Me.rbtInvoicePrice.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtInvoicePrice.Name = "rbtInvoicePrice"
        Me.rbtInvoicePrice.Size = New System.Drawing.Size(123, 24)
        Me.rbtInvoicePrice.TabIndex = 1
        Me.rbtInvoicePrice.Text = "Invoice Price"
        Me.ToolTip1.SetToolTip(Me.rbtInvoicePrice, "View By Only Customer")
        Me.rbtInvoicePrice.UseVisualStyleBackColor = True
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(1002, 2)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar1.MyGrid = Me.grdRcords
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(57, 37)
        Me.CtrlGrdBar1.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.CtrlGrdBar1, "Settings")
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cmbPeriod)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtpTo)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.dtpFrom)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 115)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(306, 160)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Date Range"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 37)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(54, 20)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Period"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.rbtall)
        Me.GroupBox2.Controls.Add(Me.rbtcustomer)
        Me.GroupBox2.Location = New System.Drawing.Point(332, 115)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Size = New System.Drawing.Size(711, 75)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.Controls.Add(Me.chkHideZeroValue)
        Me.GroupBox3.Controls.Add(Me.chkUnPostedVoucher)
        Me.GroupBox3.Controls.Add(Me.rbtRetailPrice)
        Me.GroupBox3.Controls.Add(Me.rbtInvoicePrice)
        Me.GroupBox3.Location = New System.Drawing.Point(332, 200)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox3.Size = New System.Drawing.Size(711, 75)
        Me.GroupBox3.TabIndex = 5
        Me.GroupBox3.TabStop = False
        '
        'chkHideZeroValue
        '
        Me.chkHideZeroValue.AutoSize = True
        Me.chkHideZeroValue.Checked = True
        Me.chkHideZeroValue.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkHideZeroValue.Location = New System.Drawing.Point(459, 31)
        Me.chkHideZeroValue.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkHideZeroValue.Name = "chkHideZeroValue"
        Me.chkHideZeroValue.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkHideZeroValue.Size = New System.Drawing.Size(150, 24)
        Me.chkHideZeroValue.TabIndex = 3
        Me.chkHideZeroValue.Text = "Hide Zero Value"
        Me.chkHideZeroValue.UseVisualStyleBackColor = True
        '
        'chkUnPostedVoucher
        '
        Me.chkUnPostedVoucher.AutoSize = True
        Me.chkUnPostedVoucher.Location = New System.Drawing.Point(276, 31)
        Me.chkUnPostedVoucher.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkUnPostedVoucher.Name = "chkUnPostedVoucher"
        Me.chkUnPostedVoucher.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.chkUnPostedVoucher.Size = New System.Drawing.Size(170, 24)
        Me.chkUnPostedVoucher.TabIndex = 2
        Me.chkUnPostedVoucher.Text = "UnPosted Voucher"
        Me.chkUnPostedVoucher.UseVisualStyleBackColor = True
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 38)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1060, 65)
        Me.pnlHeader.TabIndex = 12
        '
        'rptPurchaseDailyWorkingReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1060, 1009)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.btnGenerateReport)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.grdRcords)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "rptPurchaseDailyWorkingReport"
        Me.Text = "Purchase Daily Working Report"
        CType(Me.grdRcords, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnGenerateReport As System.Windows.Forms.Button
    Friend WithEvents grdRcords As Janus.Windows.GridEX.GridEX
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents rbtcustomer As System.Windows.Forms.RadioButton
    Friend WithEvents rbtall As System.Windows.Forms.RadioButton
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtRetailPrice As System.Windows.Forms.RadioButton
    Friend WithEvents rbtInvoicePrice As System.Windows.Forms.RadioButton
    Friend WithEvents chkUnPostedVoucher As System.Windows.Forms.CheckBox
    Friend WithEvents chkHideZeroValue As System.Windows.Forms.CheckBox
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

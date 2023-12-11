<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAverageRateUpdate
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
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpDateTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpDateFrom = New System.Windows.Forms.DateTimePicker()
        Me.prbOverall = New System.Windows.Forms.ProgressBar()
        Me.prbInvoices = New System.Windows.Forms.ProgressBar()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.BackgroundWorker2 = New System.ComponentModel.BackgroundWorker()
        Me.chkSales = New System.Windows.Forms.CheckBox()
        Me.chkSalesReturn = New System.Windows.Forms.CheckBox()
        Me.chkStoreIssuence = New System.Windows.Forms.CheckBox()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(18, 14)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(304, 36)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Update Average Rate"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtpDateTo)
        Me.GroupBox1.Controls.Add(Me.dtpDateFrom)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 95)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(585, 114)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Date Range"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 75)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 20)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Date To:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 35)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 20)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Date From:"
        '
        'dtpDateTo
        '
        Me.dtpDateTo.CustomFormat = "dd/MMM/yyyy"
        Me.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateTo.Location = New System.Drawing.Point(164, 69)
        Me.dtpDateTo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpDateTo.Name = "dtpDateTo"
        Me.dtpDateTo.Size = New System.Drawing.Size(410, 26)
        Me.dtpDateTo.TabIndex = 3
        '
        'dtpDateFrom
        '
        Me.dtpDateFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateFrom.Location = New System.Drawing.Point(164, 29)
        Me.dtpDateFrom.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpDateFrom.Name = "dtpDateFrom"
        Me.dtpDateFrom.Size = New System.Drawing.Size(410, 26)
        Me.dtpDateFrom.TabIndex = 1
        '
        'prbOverall
        '
        Me.prbOverall.Location = New System.Drawing.Point(9, 125)
        Me.prbOverall.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.prbOverall.Name = "prbOverall"
        Me.prbOverall.Size = New System.Drawing.Size(567, 35)
        Me.prbOverall.TabIndex = 3
        '
        'prbInvoices
        '
        Me.prbInvoices.Location = New System.Drawing.Point(9, 60)
        Me.prbInvoices.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.prbInvoices.Name = "prbInvoices"
        Me.prbInvoices.Size = New System.Drawing.Size(567, 35)
        Me.prbInvoices.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.prbInvoices)
        Me.GroupBox2.Controls.Add(Me.prbOverall)
        Me.GroupBox2.Location = New System.Drawing.Point(18, 218)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Size = New System.Drawing.Size(585, 174)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Progress"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(4, 35)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(87, 20)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Invoice No."
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(4, 100)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(131, 20)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Over all progress:"
        '
        'btnUpdate
        '
        Me.btnUpdate.Location = New System.Drawing.Point(405, 402)
        Me.btnUpdate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(189, 35)
        Me.btnUpdate.TabIndex = 6
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = True
        '
        'BackgroundWorker2
        '
        '
        'chkSales
        '
        Me.chkSales.AutoSize = True
        Me.chkSales.Checked = True
        Me.chkSales.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkSales.Location = New System.Drawing.Point(27, 408)
        Me.chkSales.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkSales.Name = "chkSales"
        Me.chkSales.Size = New System.Drawing.Size(75, 24)
        Me.chkSales.TabIndex = 3
        Me.chkSales.Text = "Sales"
        Me.chkSales.UseVisualStyleBackColor = True
        '
        'chkSalesReturn
        '
        Me.chkSalesReturn.AutoSize = True
        Me.chkSalesReturn.Location = New System.Drawing.Point(114, 408)
        Me.chkSalesReturn.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkSalesReturn.Name = "chkSalesReturn"
        Me.chkSalesReturn.Size = New System.Drawing.Size(128, 24)
        Me.chkSalesReturn.TabIndex = 4
        Me.chkSalesReturn.Text = "Sales Return"
        Me.chkSalesReturn.UseVisualStyleBackColor = True
        '
        'chkStoreIssuence
        '
        Me.chkStoreIssuence.AutoSize = True
        Me.chkStoreIssuence.Location = New System.Drawing.Point(254, 408)
        Me.chkStoreIssuence.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkStoreIssuence.Name = "chkStoreIssuence"
        Me.chkStoreIssuence.Size = New System.Drawing.Size(143, 24)
        Me.chkStoreIssuence.TabIndex = 5
        Me.chkStoreIssuence.Text = "Store Issuence"
        Me.chkStoreIssuence.UseVisualStyleBackColor = False
        Me.chkStoreIssuence.Visible = False
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(621, 66)
        Me.pnlHeader.TabIndex = 28
        '
        'frmAverageRateUpdate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(621, 452)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.chkStoreIssuence)
        Me.Controls.Add(Me.chkSalesReturn)
        Me.Controls.Add(Me.chkSales)
        Me.Controls.Add(Me.btnUpdate)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAverageRateUpdate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Update Average Rate"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpDateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents prbOverall As System.Windows.Forms.ProgressBar
    Friend WithEvents prbInvoices As System.Windows.Forms.ProgressBar
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundWorker2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents chkSales As System.Windows.Forms.CheckBox
    Friend WithEvents chkSalesReturn As System.Windows.Forms.CheckBox
    Friend WithEvents chkStoreIssuence As System.Windows.Forms.CheckBox
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

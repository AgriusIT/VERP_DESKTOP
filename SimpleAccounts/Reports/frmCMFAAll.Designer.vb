<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCMFAAll
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
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtApprovedAll = New System.Windows.Forms.RadioButton()
        Me.rbtUnApproved = New System.Windows.Forms.RadioButton()
        Me.rbtApproved = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rbtSalesAll = New System.Windows.Forms.RadioButton()
        Me.rbtWithoutSales = New System.Windows.Forms.RadioButton()
        Me.rbtWithSale = New System.Windows.Forms.RadioButton()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(135, 49)
        Me.dtpFromDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(254, 26)
        Me.dtpFromDate.TabIndex = 1
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(135, 89)
        Me.dtpToDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(254, 26)
        Me.dtpToDate.TabIndex = 3
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtApprovedAll)
        Me.GroupBox1.Controls.Add(Me.rbtUnApproved)
        Me.GroupBox1.Controls.Add(Me.rbtApproved)
        Me.GroupBox1.Location = New System.Drawing.Point(135, 129)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(442, 62)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "CMFA Status"
        '
        'rbtApprovedAll
        '
        Me.rbtApprovedAll.AutoSize = True
        Me.rbtApprovedAll.Location = New System.Drawing.Point(266, 26)
        Me.rbtApprovedAll.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtApprovedAll.Name = "rbtApprovedAll"
        Me.rbtApprovedAll.Size = New System.Drawing.Size(51, 24)
        Me.rbtApprovedAll.TabIndex = 2
        Me.rbtApprovedAll.TabStop = True
        Me.rbtApprovedAll.Text = "All"
        Me.rbtApprovedAll.UseVisualStyleBackColor = True
        '
        'rbtUnApproved
        '
        Me.rbtUnApproved.AutoSize = True
        Me.rbtUnApproved.Location = New System.Drawing.Point(124, 26)
        Me.rbtUnApproved.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtUnApproved.Name = "rbtUnApproved"
        Me.rbtUnApproved.Size = New System.Drawing.Size(127, 24)
        Me.rbtUnApproved.TabIndex = 1
        Me.rbtUnApproved.TabStop = True
        Me.rbtUnApproved.Text = "Un Approved"
        Me.rbtUnApproved.UseVisualStyleBackColor = True
        '
        'rbtApproved
        '
        Me.rbtApproved.AutoSize = True
        Me.rbtApproved.Location = New System.Drawing.Point(9, 26)
        Me.rbtApproved.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtApproved.Name = "rbtApproved"
        Me.rbtApproved.Size = New System.Drawing.Size(102, 24)
        Me.rbtApproved.TabIndex = 0
        Me.rbtApproved.TabStop = True
        Me.rbtApproved.Text = "Approved"
        Me.rbtApproved.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rbtSalesAll)
        Me.GroupBox2.Controls.Add(Me.rbtWithoutSales)
        Me.GroupBox2.Controls.Add(Me.rbtWithSale)
        Me.GroupBox2.Location = New System.Drawing.Point(135, 200)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Size = New System.Drawing.Size(442, 62)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "CMFA Sales"
        '
        'rbtSalesAll
        '
        Me.rbtSalesAll.AutoSize = True
        Me.rbtSalesAll.Location = New System.Drawing.Point(374, 26)
        Me.rbtSalesAll.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtSalesAll.Name = "rbtSalesAll"
        Me.rbtSalesAll.Size = New System.Drawing.Size(51, 24)
        Me.rbtSalesAll.TabIndex = 2
        Me.rbtSalesAll.TabStop = True
        Me.rbtSalesAll.Text = "All"
        Me.rbtSalesAll.UseVisualStyleBackColor = True
        '
        'rbtWithoutSales
        '
        Me.rbtWithoutSales.AutoSize = True
        Me.rbtWithoutSales.Location = New System.Drawing.Point(180, 26)
        Me.rbtWithoutSales.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtWithoutSales.Name = "rbtWithoutSales"
        Me.rbtWithoutSales.Size = New System.Drawing.Size(182, 24)
        Me.rbtWithoutSales.TabIndex = 1
        Me.rbtWithoutSales.TabStop = True
        Me.rbtWithoutSales.Text = "CMFA Without Sales"
        Me.rbtWithoutSales.UseVisualStyleBackColor = True
        '
        'rbtWithSale
        '
        Me.rbtWithSale.AutoSize = True
        Me.rbtWithSale.Location = New System.Drawing.Point(9, 26)
        Me.rbtWithSale.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtWithSale.Name = "rbtWithSale"
        Me.rbtWithSale.Size = New System.Drawing.Size(159, 24)
        Me.rbtWithSale.TabIndex = 0
        Me.rbtWithSale.TabStop = True
        Me.rbtWithSale.Text = "CMFA With Sales"
        Me.rbtWithSale.UseVisualStyleBackColor = True
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(465, 283)
        Me.btnShow.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(112, 35)
        Me.btnShow.TabIndex = 6
        Me.btnShow.Text = "Show"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 55)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(85, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Date From"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 95)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(66, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Date To"
        '
        'frmCMFAAll
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(597, 337)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnShow)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.dtpToDate)
        Me.Controls.Add(Me.dtpFromDate)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmCMFAAll"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "CMFA Status Report"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents rbtApprovedAll As System.Windows.Forms.RadioButton
    Friend WithEvents rbtUnApproved As System.Windows.Forms.RadioButton
    Friend WithEvents rbtApproved As System.Windows.Forms.RadioButton
    Friend WithEvents rbtSalesAll As System.Windows.Forms.RadioButton
    Friend WithEvents rbtWithoutSales As System.Windows.Forms.RadioButton
    Friend WithEvents rbtWithSale As System.Windows.Forms.RadioButton
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class

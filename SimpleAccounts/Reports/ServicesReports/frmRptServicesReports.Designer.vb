<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptServicesReports
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
        Me.cmbCustomer = New Infragistics.Win.UltraWinGrid.UltraCombo
        Me.dtpDateFrom = New System.Windows.Forms.DateTimePicker
        Me.dtpDateTo = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.btnShow = New System.Windows.Forms.Button
        Me.rbtIGP = New System.Windows.Forms.RadioButton
        Me.rbtWIP = New System.Windows.Forms.RadioButton
        Me.rbtProduction = New System.Windows.Forms.RadioButton
        Me.rbtSalesInvoice = New System.Windows.Forms.RadioButton
        Me.rbtDispatch = New System.Windows.Forms.RadioButton
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        CType(Me.cmbCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbCustomer
        '
        Me.cmbCustomer.CheckedListSettings.CheckStateMember = ""
        Me.cmbCustomer.Location = New System.Drawing.Point(85, 76)
        Me.cmbCustomer.Name = "cmbCustomer"
        Me.cmbCustomer.Size = New System.Drawing.Size(282, 23)
        Me.cmbCustomer.TabIndex = 5
        '
        'dtpDateFrom
        '
        Me.dtpDateFrom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.dtpDateFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateFrom.Location = New System.Drawing.Point(85, 22)
        Me.dtpDateFrom.Name = "dtpDateFrom"
        Me.dtpDateFrom.Size = New System.Drawing.Size(147, 21)
        Me.dtpDateFrom.TabIndex = 1
        '
        'dtpDateTo
        '
        Me.dtpDateTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.dtpDateTo.CustomFormat = "dd/MMM/yyyy"
        Me.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateTo.Location = New System.Drawing.Point(85, 49)
        Me.dtpDateTo.Name = "dtpDateTo"
        Me.dtpDateTo.Size = New System.Drawing.Size(147, 21)
        Me.dtpDateTo.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Date From"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Date To"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 81)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Customer"
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(292, 202)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(75, 23)
        Me.btnShow.TabIndex = 7
        Me.btnShow.Text = "&Show"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'rbtIGP
        '
        Me.rbtIGP.AutoSize = True
        Me.rbtIGP.Location = New System.Drawing.Point(6, 20)
        Me.rbtIGP.Name = "rbtIGP"
        Me.rbtIGP.Size = New System.Drawing.Size(46, 17)
        Me.rbtIGP.TabIndex = 0
        Me.rbtIGP.TabStop = True
        Me.rbtIGP.Text = "IGP"
        Me.rbtIGP.UseVisualStyleBackColor = True
        '
        'rbtWIP
        '
        Me.rbtWIP.AutoSize = True
        Me.rbtWIP.Location = New System.Drawing.Point(113, 20)
        Me.rbtWIP.Name = "rbtWIP"
        Me.rbtWIP.Size = New System.Drawing.Size(48, 17)
        Me.rbtWIP.TabIndex = 1
        Me.rbtWIP.TabStop = True
        Me.rbtWIP.Text = "WIP"
        Me.rbtWIP.UseVisualStyleBackColor = True
        '
        'rbtProduction
        '
        Me.rbtProduction.AutoSize = True
        Me.rbtProduction.Location = New System.Drawing.Point(6, 43)
        Me.rbtProduction.Name = "rbtProduction"
        Me.rbtProduction.Size = New System.Drawing.Size(85, 17)
        Me.rbtProduction.TabIndex = 2
        Me.rbtProduction.TabStop = True
        Me.rbtProduction.Text = "Production"
        Me.rbtProduction.UseVisualStyleBackColor = True
        '
        'rbtSalesInvoice
        '
        Me.rbtSalesInvoice.AutoSize = True
        Me.rbtSalesInvoice.Location = New System.Drawing.Point(113, 43)
        Me.rbtSalesInvoice.Name = "rbtSalesInvoice"
        Me.rbtSalesInvoice.Size = New System.Drawing.Size(102, 17)
        Me.rbtSalesInvoice.TabIndex = 3
        Me.rbtSalesInvoice.TabStop = True
        Me.rbtSalesInvoice.Text = "Sales Invoice"
        Me.rbtSalesInvoice.UseVisualStyleBackColor = True
        '
        'rbtDispatch
        '
        Me.rbtDispatch.AutoSize = True
        Me.rbtDispatch.Location = New System.Drawing.Point(6, 66)
        Me.rbtDispatch.Name = "rbtDispatch"
        Me.rbtDispatch.Size = New System.Drawing.Size(74, 17)
        Me.rbtDispatch.TabIndex = 4
        Me.rbtDispatch.TabStop = True
        Me.rbtDispatch.Text = "Dispatch"
        Me.rbtDispatch.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbtIGP)
        Me.GroupBox1.Controls.Add(Me.rbtDispatch)
        Me.GroupBox1.Controls.Add(Me.rbtWIP)
        Me.GroupBox1.Controls.Add(Me.rbtSalesInvoice)
        Me.GroupBox1.Controls.Add(Me.rbtProduction)
        Me.GroupBox1.Location = New System.Drawing.Point(85, 105)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(282, 91)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Report Option"
        '
        'frmRptServicesReports
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(379, 236)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnShow)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtpDateTo)
        Me.Controls.Add(Me.dtpDateFrom)
        Me.Controls.Add(Me.cmbCustomer)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRptServicesReports"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Services Report"
        CType(Me.cmbCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmbCustomer As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents dtpDateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents rbtIGP As System.Windows.Forms.RadioButton
    Friend WithEvents rbtWIP As System.Windows.Forms.RadioButton
    Friend WithEvents rbtProduction As System.Windows.Forms.RadioButton
    Friend WithEvents rbtSalesInvoice As System.Windows.Forms.RadioButton
    Friend WithEvents rbtDispatch As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class

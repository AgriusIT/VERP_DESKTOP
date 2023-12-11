<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptServicesStockLedger
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
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.dtpDateTo = New System.Windows.Forms.DateTimePicker
        Me.dtpDateFrom = New System.Windows.Forms.DateTimePicker
        Me.cmbCustomer = New Infragistics.Win.UltraWinGrid.UltraCombo
        Me.btnShow = New System.Windows.Forms.Button
        CType(Me.cmbCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 78)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "Customer"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 50)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Date To"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(67, 13)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "Date From"
        '
        'dtpDateTo
        '
        Me.dtpDateTo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.dtpDateTo.CustomFormat = "dd/MMM/yyyy"
        Me.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateTo.Location = New System.Drawing.Point(85, 46)
        Me.dtpDateTo.Name = "dtpDateTo"
        Me.dtpDateTo.Size = New System.Drawing.Size(147, 21)
        Me.dtpDateTo.TabIndex = 9
        '
        'dtpDateFrom
        '
        Me.dtpDateFrom.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.dtpDateFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateFrom.Location = New System.Drawing.Point(85, 19)
        Me.dtpDateFrom.Name = "dtpDateFrom"
        Me.dtpDateFrom.Size = New System.Drawing.Size(147, 21)
        Me.dtpDateFrom.TabIndex = 7
        '
        'cmbCustomer
        '
        Me.cmbCustomer.CheckedListSettings.CheckStateMember = ""
        Me.cmbCustomer.Location = New System.Drawing.Point(85, 73)
        Me.cmbCustomer.Name = "cmbCustomer"
        Me.cmbCustomer.Size = New System.Drawing.Size(282, 23)
        Me.cmbCustomer.TabIndex = 11
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(292, 191)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(75, 23)
        Me.btnShow.TabIndex = 12
        Me.btnShow.Text = "&Show"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'frmRptServicesStockLedger
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(379, 226)
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
        Me.Name = "frmRptServicesStockLedger"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Services Stock Register"
        CType(Me.cmbCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpDateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbCustomer As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents btnShow As System.Windows.Forms.Button
End Class

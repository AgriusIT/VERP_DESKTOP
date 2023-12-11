<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptBankReconciliation
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
        Me.btnShow = New System.Windows.Forms.Button()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.cmbAccounts = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.chkIncludeInProcess = New System.Windows.Forms.CheckBox()
        Me.dtpDateTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpDateFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.pnlInvType = New System.Windows.Forms.Panel()
        Me.pnlHeader.SuspendLayout()
        Me.pnlInvType.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnShow
        '
        Me.btnShow.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.btnShow.Location = New System.Drawing.Point(498, 252)
        Me.btnShow.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(112, 35)
        Me.btnShow.TabIndex = 1
        Me.btnShow.Text = "Show"
        Me.btnShow.UseVisualStyleBackColor = False
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(628, 65)
        Me.pnlHeader.TabIndex = 113
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(4, 14)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(439, 41)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Bank Reconciliation Report"
        '
        'cmbAccounts
        '
        Me.cmbAccounts.FormattingEnabled = True
        Me.cmbAccounts.Location = New System.Drawing.Point(126, 85)
        Me.cmbAccounts.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbAccounts.Name = "cmbAccounts"
        Me.cmbAccounts.Size = New System.Drawing.Size(451, 28)
        Me.cmbAccounts.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 89)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(109, 20)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Bank Account"
        '
        'chkIncludeInProcess
        '
        Me.chkIncludeInProcess.AutoSize = True
        Me.chkIncludeInProcess.Checked = True
        Me.chkIncludeInProcess.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIncludeInProcess.Location = New System.Drawing.Point(126, 126)
        Me.chkIncludeInProcess.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkIncludeInProcess.Name = "chkIncludeInProcess"
        Me.chkIncludeInProcess.Size = New System.Drawing.Size(220, 24)
        Me.chkIncludeInProcess.TabIndex = 6
        Me.chkIncludeInProcess.Text = "Include in process cheque"
        Me.chkIncludeInProcess.UseVisualStyleBackColor = True
        '
        'dtpDateTo
        '
        Me.dtpDateTo.CustomFormat = "dd/MMM/yyyy"
        Me.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateTo.Location = New System.Drawing.Point(126, 45)
        Me.dtpDateTo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpDateTo.Name = "dtpDateTo"
        Me.dtpDateTo.Size = New System.Drawing.Size(235, 26)
        Me.dtpDateTo.TabIndex = 3
        '
        'dtpDateFrom
        '
        Me.dtpDateFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateFrom.Location = New System.Drawing.Point(126, 5)
        Me.dtpDateFrom.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpDateFrom.Name = "dtpDateFrom"
        Me.dtpDateFrom.Size = New System.Drawing.Size(235, 26)
        Me.dtpDateFrom.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 14)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 20)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Date From"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(4, 54)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 20)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Date To"
        '
        'pnlInvType
        '
        Me.pnlInvType.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.pnlInvType.Controls.Add(Me.Label2)
        Me.pnlInvType.Controls.Add(Me.Label3)
        Me.pnlInvType.Controls.Add(Me.Label1)
        Me.pnlInvType.Controls.Add(Me.cmbAccounts)
        Me.pnlInvType.Controls.Add(Me.chkIncludeInProcess)
        Me.pnlInvType.Controls.Add(Me.dtpDateFrom)
        Me.pnlInvType.Controls.Add(Me.dtpDateTo)
        Me.pnlInvType.Location = New System.Drawing.Point(18, 74)
        Me.pnlInvType.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlInvType.Name = "pnlInvType"
        Me.pnlInvType.Size = New System.Drawing.Size(592, 169)
        Me.pnlInvType.TabIndex = 114
        '
        'frmRptBankReconciliation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(628, 298)
        Me.Controls.Add(Me.pnlInvType)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.btnShow)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmRptBankReconciliation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Bank Reconciliation Report"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlInvType.ResumeLayout(False)
        Me.pnlInvType.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents cmbAccounts As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkIncludeInProcess As System.Windows.Forms.CheckBox
    Friend WithEvents dtpDateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents pnlInvType As System.Windows.Forms.Panel
End Class

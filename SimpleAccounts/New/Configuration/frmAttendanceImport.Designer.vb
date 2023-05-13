<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAttendanceImport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAttendanceImport))
        Me.gpbDBPath = New System.Windows.Forms.GroupBox
        Me.btnAlternativeDBPath = New System.Windows.Forms.Button
        Me.txtDBPath = New System.Windows.Forms.TextBox
        Me.gpbDateRange = New System.Windows.Forms.GroupBox
        Me.lblDateTo = New System.Windows.Forms.Label
        Me.lblDateFrom = New System.Windows.Forms.Label
        Me.dtpDateTo = New System.Windows.Forms.DateTimePicker
        Me.dtpDateFrom = New System.Windows.Forms.DateTimePicker
        Me.btnImport = New System.Windows.Forms.Button
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.gpbDBPath.SuspendLayout()
        Me.gpbDateRange.SuspendLayout()
        Me.SuspendLayout()
        '
        'gpbDBPath
        '
        Me.gpbDBPath.BackColor = System.Drawing.Color.Transparent
        Me.gpbDBPath.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.gpbDBPath.Controls.Add(Me.btnAlternativeDBPath)
        Me.gpbDBPath.Controls.Add(Me.txtDBPath)
        Me.gpbDBPath.Location = New System.Drawing.Point(12, 12)
        Me.gpbDBPath.Name = "gpbDBPath"
        Me.gpbDBPath.Size = New System.Drawing.Size(399, 47)
        Me.gpbDBPath.TabIndex = 0
        Me.gpbDBPath.TabStop = False
        Me.gpbDBPath.Text = "DB Path"
        '
        'btnAlternativeDBPath
        '
        Me.btnAlternativeDBPath.Image = Global.SimpleAccounts.My.Resources.Resources.pin_black
        Me.btnAlternativeDBPath.Location = New System.Drawing.Point(368, 17)
        Me.btnAlternativeDBPath.Name = "btnAlternativeDBPath"
        Me.btnAlternativeDBPath.Size = New System.Drawing.Size(25, 23)
        Me.btnAlternativeDBPath.TabIndex = 1
        Me.btnAlternativeDBPath.UseVisualStyleBackColor = True
        '
        'txtDBPath
        '
        Me.txtDBPath.BackColor = System.Drawing.SystemColors.Window
        Me.txtDBPath.Location = New System.Drawing.Point(6, 19)
        Me.txtDBPath.Name = "txtDBPath"
        Me.txtDBPath.ReadOnly = True
        Me.txtDBPath.Size = New System.Drawing.Size(356, 20)
        Me.txtDBPath.TabIndex = 0
        '
        'gpbDateRange
        '
        Me.gpbDateRange.BackColor = System.Drawing.Color.Transparent
        Me.gpbDateRange.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.gpbDateRange.Controls.Add(Me.lblDateTo)
        Me.gpbDateRange.Controls.Add(Me.lblDateFrom)
        Me.gpbDateRange.Controls.Add(Me.dtpDateTo)
        Me.gpbDateRange.Controls.Add(Me.dtpDateFrom)
        Me.gpbDateRange.Location = New System.Drawing.Point(12, 65)
        Me.gpbDateRange.Name = "gpbDateRange"
        Me.gpbDateRange.Size = New System.Drawing.Size(399, 65)
        Me.gpbDateRange.TabIndex = 1
        Me.gpbDateRange.TabStop = False
        Me.gpbDateRange.Text = "Date Range"
        '
        'lblDateTo
        '
        Me.lblDateTo.AutoSize = True
        Me.lblDateTo.Location = New System.Drawing.Point(185, 23)
        Me.lblDateTo.Name = "lblDateTo"
        Me.lblDateTo.Size = New System.Drawing.Size(46, 13)
        Me.lblDateTo.TabIndex = 2
        Me.lblDateTo.Text = "Date To"
        '
        'lblDateFrom
        '
        Me.lblDateFrom.AutoSize = True
        Me.lblDateFrom.Location = New System.Drawing.Point(3, 23)
        Me.lblDateFrom.Name = "lblDateFrom"
        Me.lblDateFrom.Size = New System.Drawing.Size(56, 13)
        Me.lblDateFrom.TabIndex = 0
        Me.lblDateFrom.Text = "Date From"
        '
        'dtpDateTo
        '
        Me.dtpDateTo.CustomFormat = "dd/MMM/yyyy"
        Me.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateTo.Location = New System.Drawing.Point(188, 39)
        Me.dtpDateTo.Name = "dtpDateTo"
        Me.dtpDateTo.Size = New System.Drawing.Size(175, 20)
        Me.dtpDateTo.TabIndex = 3
        '
        'dtpDateFrom
        '
        Me.dtpDateFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateFrom.Location = New System.Drawing.Point(6, 39)
        Me.dtpDateFrom.Name = "dtpDateFrom"
        Me.dtpDateFrom.Size = New System.Drawing.Size(175, 20)
        Me.dtpDateFrom.TabIndex = 1
        '
        'btnImport
        '
        Me.btnImport.Location = New System.Drawing.Point(287, 160)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(118, 23)
        Me.btnImport.TabIndex = 3
        Me.btnImport.Text = "Import Attendance"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(18, 160)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(263, 23)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 2
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'frmAttendanceImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(423, 195)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.btnImport)
        Me.Controls.Add(Me.gpbDateRange)
        Me.Controls.Add(Me.gpbDBPath)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAttendanceImport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import Attendance"
        Me.gpbDBPath.ResumeLayout(False)
        Me.gpbDBPath.PerformLayout()
        Me.gpbDateRange.ResumeLayout(False)
        Me.gpbDateRange.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gpbDBPath As System.Windows.Forms.GroupBox
    Friend WithEvents txtDBPath As System.Windows.Forms.TextBox
    Friend WithEvents btnAlternativeDBPath As System.Windows.Forms.Button
    Friend WithEvents gpbDateRange As System.Windows.Forms.GroupBox
    Friend WithEvents lblDateTo As System.Windows.Forms.Label
    Friend WithEvents lblDateFrom As System.Windows.Forms.Label
    Friend WithEvents dtpDateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnImport As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
End Class

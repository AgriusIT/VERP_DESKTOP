<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSalaryConfig
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmbSalaryAccount = New System.Windows.Forms.ComboBox()
        Me.cmbSalaryPayableAccount = New System.Windows.Forms.ComboBox()
        Me.TxtTotalLeaves = New System.Windows.Forms.TextBox()
        Me.BtnLeaves = New System.Windows.Forms.Button()
        Me.BtnStartPeriod = New System.Windows.Forms.Button()
        Me.DtpStartDate = New System.Windows.Forms.DateTimePicker()
        Me.chkGrossSalaryCalc = New System.Windows.Forms.CheckBox()
        Me.btnGrossSalaryFormula = New System.Windows.Forms.Button()
        Me.txtGrossSalaryFormula = New System.Windows.Forms.TextBox()
        Me.nudDefaultWorkingHours = New System.Windows.Forms.NumericUpDown()
        Me.btnWorkingDays = New System.Windows.Forms.Button()
        Me.txtWorkingDays = New System.Windows.Forms.TextBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.chkAttendanceBaseSalary = New System.Windows.Forms.CheckBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        CType(Me.nudDefaultWorkingHours, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(123, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Salary Expense Account"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 43)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(120, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Salary Payable Account"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 16)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(97, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Total Leaves Allow"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(6, 45)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(120, 13)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Start Attendance Period"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 17)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(176, 13)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Gross Salary Calculation By Formula"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 44)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(106, 13)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Gross Salary Formula"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 51)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(115, 13)
        Me.Label8.TabIndex = 3
        Me.Label8.Text = "Default Working Hours"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(6, 26)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(74, 13)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Working Days"
        '
        'cmbSalaryAccount
        '
        Me.cmbSalaryAccount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbSalaryAccount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbSalaryAccount.FormattingEnabled = True
        Me.cmbSalaryAccount.Location = New System.Drawing.Point(139, 13)
        Me.cmbSalaryAccount.Name = "cmbSalaryAccount"
        Me.cmbSalaryAccount.Size = New System.Drawing.Size(189, 21)
        Me.cmbSalaryAccount.TabIndex = 1
        Me.cmbSalaryAccount.Tag = "SalariesAccountId"
        '
        'cmbSalaryPayableAccount
        '
        Me.cmbSalaryPayableAccount.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbSalaryPayableAccount.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbSalaryPayableAccount.FormattingEnabled = True
        Me.cmbSalaryPayableAccount.Location = New System.Drawing.Point(139, 40)
        Me.cmbSalaryPayableAccount.Name = "cmbSalaryPayableAccount"
        Me.cmbSalaryPayableAccount.Size = New System.Drawing.Size(189, 21)
        Me.cmbSalaryPayableAccount.TabIndex = 3
        Me.cmbSalaryPayableAccount.Tag = "SalariesPayableAccountId"
        '
        'TxtTotalLeaves
        '
        Me.TxtTotalLeaves.Location = New System.Drawing.Point(139, 12)
        Me.TxtTotalLeaves.Name = "TxtTotalLeaves"
        Me.TxtTotalLeaves.Size = New System.Drawing.Size(189, 20)
        Me.TxtTotalLeaves.TabIndex = 1
        '
        'BtnLeaves
        '
        Me.BtnLeaves.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.BtnLeaves.Image = Global.SimpleAccounts.My.Resources.Resources.save_labled
        Me.BtnLeaves.Location = New System.Drawing.Point(334, 12)
        Me.BtnLeaves.Name = "BtnLeaves"
        Me.BtnLeaves.Size = New System.Drawing.Size(24, 21)
        Me.BtnLeaves.TabIndex = 2
        Me.BtnLeaves.UseVisualStyleBackColor = True
        '
        'BtnStartPeriod
        '
        Me.BtnStartPeriod.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.BtnStartPeriod.Image = Global.SimpleAccounts.My.Resources.Resources.save_labled
        Me.BtnStartPeriod.Location = New System.Drawing.Point(334, 38)
        Me.BtnStartPeriod.Name = "BtnStartPeriod"
        Me.BtnStartPeriod.Size = New System.Drawing.Size(24, 21)
        Me.BtnStartPeriod.TabIndex = 4
        Me.BtnStartPeriod.UseVisualStyleBackColor = True
        '
        'DtpStartDate
        '
        Me.DtpStartDate.Checked = False
        Me.DtpStartDate.CustomFormat = "dd/MMM/yyyy"
        Me.DtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpStartDate.Location = New System.Drawing.Point(139, 39)
        Me.DtpStartDate.Name = "DtpStartDate"
        Me.DtpStartDate.ShowCheckBox = True
        Me.DtpStartDate.Size = New System.Drawing.Size(189, 20)
        Me.DtpStartDate.TabIndex = 3
        '
        'chkGrossSalaryCalc
        '
        Me.chkGrossSalaryCalc.AutoSize = True
        Me.chkGrossSalaryCalc.Location = New System.Drawing.Point(232, 16)
        Me.chkGrossSalaryCalc.Name = "chkGrossSalaryCalc"
        Me.chkGrossSalaryCalc.Size = New System.Drawing.Size(15, 14)
        Me.chkGrossSalaryCalc.TabIndex = 1
        Me.chkGrossSalaryCalc.Tag = "GrossSalaryCalcByFormula"
        Me.chkGrossSalaryCalc.UseVisualStyleBackColor = True
        '
        'btnGrossSalaryFormula
        '
        Me.btnGrossSalaryFormula.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnGrossSalaryFormula.Image = Global.SimpleAccounts.My.Resources.Resources.save_labled
        Me.btnGrossSalaryFormula.Location = New System.Drawing.Point(334, 42)
        Me.btnGrossSalaryFormula.Name = "btnGrossSalaryFormula"
        Me.btnGrossSalaryFormula.Size = New System.Drawing.Size(24, 21)
        Me.btnGrossSalaryFormula.TabIndex = 4
        Me.btnGrossSalaryFormula.UseVisualStyleBackColor = True
        '
        'txtGrossSalaryFormula
        '
        Me.txtGrossSalaryFormula.Location = New System.Drawing.Point(139, 41)
        Me.txtGrossSalaryFormula.Name = "txtGrossSalaryFormula"
        Me.txtGrossSalaryFormula.Size = New System.Drawing.Size(189, 20)
        Me.txtGrossSalaryFormula.TabIndex = 3
        '
        'nudDefaultWorkingHours
        '
        Me.nudDefaultWorkingHours.Location = New System.Drawing.Point(139, 49)
        Me.nudDefaultWorkingHours.Name = "nudDefaultWorkingHours"
        Me.nudDefaultWorkingHours.Size = New System.Drawing.Size(40, 20)
        Me.nudDefaultWorkingHours.TabIndex = 4
        Me.nudDefaultWorkingHours.Tag = "DefaultWorkingHours"
        '
        'btnWorkingDays
        '
        Me.btnWorkingDays.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnWorkingDays.Image = Global.SimpleAccounts.My.Resources.Resources.save_labled
        Me.btnWorkingDays.Location = New System.Drawing.Point(334, 23)
        Me.btnWorkingDays.Name = "btnWorkingDays"
        Me.btnWorkingDays.Size = New System.Drawing.Size(24, 21)
        Me.btnWorkingDays.TabIndex = 2
        Me.btnWorkingDays.UseVisualStyleBackColor = True
        '
        'txtWorkingDays
        '
        Me.txtWorkingDays.Location = New System.Drawing.Point(139, 23)
        Me.txtWorkingDays.Name = "txtWorkingDays"
        Me.txtWorkingDays.Size = New System.Drawing.Size(189, 20)
        Me.txtWorkingDays.TabIndex = 1
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnSave, Me.btnCancel})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(436, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnSave
        '
        Me.btnSave.Image = Global.SimpleAccounts.My.Resources.Resources.BtnSave_Image
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(51, 22)
        Me.btnSave.Text = "&Save"
        '
        'btnCancel
        '
        Me.btnCancel.Image = Global.SimpleAccounts.My.Resources.Resources.BtnDelete_Image
        Me.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(63, 22)
        Me.btnCancel.Text = "&Cancel"
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(132, 191)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(190, 41)
        Me.lblProgress.TabIndex = 6
        Me.lblProgress.Text = "Please Wait Processing...."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkAttendanceBaseSalary
        '
        Me.chkAttendanceBaseSalary.AutoSize = True
        Me.chkAttendanceBaseSalary.Location = New System.Drawing.Point(139, 19)
        Me.chkAttendanceBaseSalary.Name = "chkAttendanceBaseSalary"
        Me.chkAttendanceBaseSalary.Size = New System.Drawing.Size(15, 14)
        Me.chkAttendanceBaseSalary.TabIndex = 1
        Me.chkAttendanceBaseSalary.Tag = "AttendanceBasedSalary"
        Me.chkAttendanceBaseSalary.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(127, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Attendance Based Salary"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.txtWorkingDays)
        Me.GroupBox1.Controls.Add(Me.btnWorkingDays)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.nudDefaultWorkingHours)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 50)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(404, 78)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Time"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.chkAttendanceBaseSalary)
        Me.GroupBox2.Controls.Add(Me.DtpStartDate)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.BtnStartPeriod)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 134)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(404, 73)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Attendance"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.Label2)
        Me.GroupBox3.Controls.Add(Me.cmbSalaryAccount)
        Me.GroupBox3.Controls.Add(Me.cmbSalaryPayableAccount)
        Me.GroupBox3.Controls.Add(Me.Label3)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 213)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(404, 73)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Account"
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label4)
        Me.GroupBox4.Controls.Add(Me.TxtTotalLeaves)
        Me.GroupBox4.Controls.Add(Me.BtnLeaves)
        Me.GroupBox4.Location = New System.Drawing.Point(12, 292)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(404, 39)
        Me.GroupBox4.TabIndex = 4
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Leaves"
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.Label6)
        Me.GroupBox5.Controls.Add(Me.Label7)
        Me.GroupBox5.Controls.Add(Me.chkGrossSalaryCalc)
        Me.GroupBox5.Controls.Add(Me.txtGrossSalaryFormula)
        Me.GroupBox5.Controls.Add(Me.btnGrossSalaryFormula)
        Me.GroupBox5.Location = New System.Drawing.Point(12, 337)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(404, 69)
        Me.GroupBox5.TabIndex = 5
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Salary Formula"
        '
        'frmSalaryConfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(436, 428)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSalaryConfig"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Salaries Configurations"
        CType(Me.nudDefaultWorkingHours, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbSalaryAccount As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSalaryPayableAccount As System.Windows.Forms.ComboBox
    Friend WithEvents BtnLeaves As System.Windows.Forms.Button
    Friend WithEvents TxtTotalLeaves As System.Windows.Forms.TextBox
    Friend WithEvents BtnStartPeriod As System.Windows.Forms.Button
    Friend WithEvents DtpStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkGrossSalaryCalc As System.Windows.Forms.CheckBox
    Friend WithEvents btnGrossSalaryFormula As System.Windows.Forms.Button
    Friend WithEvents txtGrossSalaryFormula As System.Windows.Forms.TextBox
    Friend WithEvents nudDefaultWorkingHours As System.Windows.Forms.NumericUpDown
    Friend WithEvents btnWorkingDays As System.Windows.Forms.Button
    Friend WithEvents txtWorkingDays As System.Windows.Forms.TextBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents chkAttendanceBaseSalary As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
End Class

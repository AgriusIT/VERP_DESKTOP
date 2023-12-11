<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfigHREmployeeAccount
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
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.lblAttendance = New System.Windows.Forms.Label()
        Me.lblProvidentFund = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lblOverTime = New System.Windows.Forms.Label()
        Me.lblSalary = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.nudLeaveEncashment = New System.Windows.Forms.NumericUpDown()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbDayOff = New System.Windows.Forms.CheckedListBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblAccountLevel = New System.Windows.Forms.Label()
        Me.btnEmpPicPath = New System.Windows.Forms.Button()
        Me.cmbEmpDeptHeadAccountId = New System.Windows.Forms.ComboBox()
        Me.rbtDeptEmpAc = New System.Windows.Forms.RadioButton()
        Me.cmbEmployeeHeadAccountId = New System.Windows.Forms.ComboBox()
        Me.rbtSimpleEmpAc = New System.Windows.Forms.RadioButton()
        Me.txtEmployeePicturePath = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.nudLeaveEncashment, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel6.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1008, 61)
        Me.Panel2.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(12, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(352, 37)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Employee Account Mapping"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.lblAttendance)
        Me.Panel3.Controls.Add(Me.lblProvidentFund)
        Me.Panel3.Controls.Add(Me.Label17)
        Me.Panel3.Controls.Add(Me.Label18)
        Me.Panel3.Controls.Add(Me.lblOverTime)
        Me.Panel3.Controls.Add(Me.lblSalary)
        Me.Panel3.Controls.Add(Me.Label12)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(793, 61)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(215, 668)
        Me.Panel3.TabIndex = 14
        '
        'lblAttendance
        '
        Me.lblAttendance.AutoSize = True
        Me.lblAttendance.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblAttendance.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAttendance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.lblAttendance.Location = New System.Drawing.Point(20, 195)
        Me.lblAttendance.Name = "lblAttendance"
        Me.lblAttendance.Size = New System.Drawing.Size(88, 21)
        Me.lblAttendance.TabIndex = 34
        Me.lblAttendance.Text = "Attendance"
        '
        'lblProvidentFund
        '
        Me.lblProvidentFund.AutoSize = True
        Me.lblProvidentFund.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblProvidentFund.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProvidentFund.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.lblProvidentFund.Location = New System.Drawing.Point(20, 146)
        Me.lblProvidentFund.Name = "lblProvidentFund"
        Me.lblProvidentFund.Size = New System.Drawing.Size(116, 21)
        Me.lblProvidentFund.TabIndex = 33
        Me.lblProvidentFund.Text = "Provident Fund"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label17.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(20, 315)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(85, 21)
        Me.Label17.TabIndex = 32
        Me.Label17.Text = "Contact Us"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label18.Location = New System.Drawing.Point(20, 269)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(146, 21)
        Me.Label18.TabIndex = 31
        Me.Label18.Text = "Have a Question(s)"
        '
        'lblOverTime
        '
        Me.lblOverTime.AutoSize = True
        Me.lblOverTime.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblOverTime.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOverTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.lblOverTime.Location = New System.Drawing.Point(20, 103)
        Me.lblOverTime.Name = "lblOverTime"
        Me.lblOverTime.Size = New System.Drawing.Size(75, 21)
        Me.lblOverTime.TabIndex = 28
        Me.lblOverTime.Text = "Overtime"
        '
        'lblSalary
        '
        Me.lblSalary.AutoSize = True
        Me.lblSalary.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblSalary.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalary.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.lblSalary.Location = New System.Drawing.Point(20, 60)
        Me.lblSalary.Name = "lblSalary"
        Me.lblSalary.Size = New System.Drawing.Size(53, 21)
        Me.lblSalary.TabIndex = 27
        Me.lblSalary.Text = "Salary"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(20, 14)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(130, 21)
        Me.Label12.TabIndex = 26
        Me.Label12.Text = "Related Settings"
        '
        'Panel8
        '
        Me.Panel8.AutoScroll = True
        Me.Panel8.Controls.Add(Me.Panel1)
        Me.Panel8.Controls.Add(Me.nudLeaveEncashment)
        Me.Panel8.Controls.Add(Me.Panel6)
        Me.Panel8.Controls.Add(Me.Label7)
        Me.Panel8.Controls.Add(Me.cmbDayOff)
        Me.Panel8.Controls.Add(Me.Label3)
        Me.Panel8.Controls.Add(Me.lblAccountLevel)
        Me.Panel8.Controls.Add(Me.btnEmpPicPath)
        Me.Panel8.Controls.Add(Me.cmbEmpDeptHeadAccountId)
        Me.Panel8.Controls.Add(Me.rbtDeptEmpAc)
        Me.Panel8.Controls.Add(Me.cmbEmployeeHeadAccountId)
        Me.Panel8.Controls.Add(Me.rbtSimpleEmpAc)
        Me.Panel8.Controls.Add(Me.txtEmployeePicturePath)
        Me.Panel8.Controls.Add(Me.Label20)
        Me.Panel8.Controls.Add(Me.Panel12)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.Location = New System.Drawing.Point(0, 61)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(793, 668)
        Me.Panel8.TabIndex = 129
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.LemonChiffon
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(256, 354)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(405, 51)
        Me.Panel1.TabIndex = 201
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(3, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(146, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "configuration of off day"
        '
        'nudLeaveEncashment
        '
        Me.nudLeaveEncashment.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nudLeaveEncashment.Location = New System.Drawing.Point(256, 413)
        Me.nudLeaveEncashment.Name = "nudLeaveEncashment"
        Me.nudLeaveEncashment.Size = New System.Drawing.Size(79, 27)
        Me.nudLeaveEncashment.TabIndex = 199
        Me.nudLeaveEncashment.Tag = "LeaveEncashment"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.LemonChiffon
        Me.Panel6.Controls.Add(Me.Label4)
        Me.Panel6.Location = New System.Drawing.Point(256, 446)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(405, 51)
        Me.Panel6.TabIndex = 200
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(3, 17)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(114, 17)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Leave Encashment"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Label7.Location = New System.Drawing.Point(84, 413)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(138, 21)
        Me.Label7.TabIndex = 198
        Me.Label7.Text = "Leave Encashment"
        '
        'cmbDayOff
        '
        Me.cmbDayOff.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDayOff.FormattingEnabled = True
        Me.cmbDayOff.Items.AddRange(New Object() {"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"})
        Me.cmbDayOff.Location = New System.Drawing.Point(256, 256)
        Me.cmbDayOff.MultiColumn = True
        Me.cmbDayOff.Name = "cmbDayOff"
        Me.cmbDayOff.Size = New System.Drawing.Size(405, 92)
        Me.cmbDayOff.TabIndex = 197
        Me.cmbDayOff.Tag = "DayOff"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Label3.Location = New System.Drawing.Point(159, 256)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 21)
        Me.Label3.TabIndex = 196
        Me.Label3.Text = "Day Off"
        '
        'lblAccountLevel
        '
        Me.lblAccountLevel.AutoSize = True
        Me.lblAccountLevel.BackColor = System.Drawing.Color.Transparent
        Me.lblAccountLevel.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.lblAccountLevel.Location = New System.Drawing.Point(252, 119)
        Me.lblAccountLevel.Name = "lblAccountLevel"
        Me.lblAccountLevel.Size = New System.Drawing.Size(106, 21)
        Me.lblAccountLevel.TabIndex = 195
        Me.lblAccountLevel.Text = "Account Level"
        '
        'btnEmpPicPath
        '
        Me.btnEmpPicPath.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.btnEmpPicPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEmpPicPath.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEmpPicPath.ForeColor = System.Drawing.Color.White
        Me.btnEmpPicPath.Location = New System.Drawing.Point(586, 13)
        Me.btnEmpPicPath.Name = "btnEmpPicPath"
        Me.btnEmpPicPath.Size = New System.Drawing.Size(75, 27)
        Me.btnEmpPicPath.TabIndex = 194
        Me.btnEmpPicPath.Text = "Browse"
        Me.btnEmpPicPath.UseVisualStyleBackColor = False
        '
        'cmbEmpDeptHeadAccountId
        '
        Me.cmbEmpDeptHeadAccountId.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbEmpDeptHeadAccountId.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbEmpDeptHeadAccountId.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbEmpDeptHeadAccountId.FormattingEnabled = True
        Me.cmbEmpDeptHeadAccountId.Location = New System.Drawing.Point(256, 198)
        Me.cmbEmpDeptHeadAccountId.Name = "cmbEmpDeptHeadAccountId"
        Me.cmbEmpDeptHeadAccountId.Size = New System.Drawing.Size(311, 28)
        Me.cmbEmpDeptHeadAccountId.TabIndex = 192
        Me.cmbEmpDeptHeadAccountId.TabStop = False
        Me.cmbEmpDeptHeadAccountId.Tag = "EmployeeDeptHeadAccountId"
        '
        'rbtDeptEmpAc
        '
        Me.rbtDeptEmpAc.AutoSize = True
        Me.rbtDeptEmpAc.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtDeptEmpAc.Location = New System.Drawing.Point(120, 199)
        Me.rbtDeptEmpAc.Name = "rbtDeptEmpAc"
        Me.rbtDeptEmpAc.Size = New System.Drawing.Size(107, 24)
        Me.rbtDeptEmpAc.TabIndex = 191
        Me.rbtDeptEmpAc.Tag = "EmpDepartmentAccountHead"
        Me.rbtDeptEmpAc.Text = "Department"
        Me.rbtDeptEmpAc.UseVisualStyleBackColor = True
        '
        'cmbEmployeeHeadAccountId
        '
        Me.cmbEmployeeHeadAccountId.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbEmployeeHeadAccountId.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbEmployeeHeadAccountId.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbEmployeeHeadAccountId.FormattingEnabled = True
        Me.cmbEmployeeHeadAccountId.Location = New System.Drawing.Point(256, 152)
        Me.cmbEmployeeHeadAccountId.Name = "cmbEmployeeHeadAccountId"
        Me.cmbEmployeeHeadAccountId.Size = New System.Drawing.Size(311, 28)
        Me.cmbEmployeeHeadAccountId.TabIndex = 193
        Me.cmbEmployeeHeadAccountId.TabStop = False
        Me.cmbEmployeeHeadAccountId.Tag = "EmployeeHeadAccountId"
        '
        'rbtSimpleEmpAc
        '
        Me.rbtSimpleEmpAc.AutoSize = True
        Me.rbtSimpleEmpAc.Checked = True
        Me.rbtSimpleEmpAc.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbtSimpleEmpAc.Location = New System.Drawing.Point(154, 153)
        Me.rbtSimpleEmpAc.Name = "rbtSimpleEmpAc"
        Me.rbtSimpleEmpAc.Size = New System.Drawing.Size(73, 24)
        Me.rbtSimpleEmpAc.TabIndex = 190
        Me.rbtSimpleEmpAc.TabStop = True
        Me.rbtSimpleEmpAc.Tag = "EmpSimpleAccountHead"
        Me.rbtSimpleEmpAc.Text = "Simple"
        Me.rbtSimpleEmpAc.UseVisualStyleBackColor = True
        '
        'txtEmployeePicturePath
        '
        Me.txtEmployeePicturePath.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEmployeePicturePath.Location = New System.Drawing.Point(256, 13)
        Me.txtEmployeePicturePath.Name = "txtEmployeePicturePath"
        Me.txtEmployeePicturePath.Size = New System.Drawing.Size(311, 27)
        Me.txtEmployeePicturePath.TabIndex = 188
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Label20.Location = New System.Drawing.Point(63, 15)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(164, 21)
        Me.Label20.TabIndex = 111
        Me.Label20.Text = "Employee Picture Path"
        '
        'Panel12
        '
        Me.Panel12.BackColor = System.Drawing.Color.LemonChiffon
        Me.Panel12.Controls.Add(Me.Label21)
        Me.Panel12.Location = New System.Drawing.Point(256, 47)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(405, 51)
        Me.Panel12.TabIndex = 9
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label21.Location = New System.Drawing.Point(3, 9)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(353, 34)
        Me.Label21.TabIndex = 0
        Me.Label21.Text = "configuration off payable account generation according to " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "department or simple"
        '
        'frmConfigHREmployeeAccount
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Name = "frmConfigHREmployeeAccount"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "mployee Account Mapping Configuration"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.nudLeaveEncashment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel12.ResumeLayout(False)
        Me.Panel12.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents lblProvidentFund As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents lblOverTime As System.Windows.Forms.Label
    Friend WithEvents lblSalary As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents cmbEmpDeptHeadAccountId As System.Windows.Forms.ComboBox
    Friend WithEvents rbtDeptEmpAc As System.Windows.Forms.RadioButton
    Friend WithEvents cmbEmployeeHeadAccountId As System.Windows.Forms.ComboBox
    Friend WithEvents rbtSimpleEmpAc As System.Windows.Forms.RadioButton
    Friend WithEvents txtEmployeePicturePath As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents btnEmpPicPath As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents lblAccountLevel As System.Windows.Forms.Label
    Friend WithEvents lblAttendance As System.Windows.Forms.Label
    Friend WithEvents nudLeaveEncashment As System.Windows.Forms.NumericUpDown
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbDayOff As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfigHRAttendance
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
        Me.lblEmployeeAccountMapping = New System.Windows.Forms.Label()
        Me.lblProvidentFund = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.lblOvertime = New System.Windows.Forms.Label()
        Me.lblSalary = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.rdoAttendaacnceEmailAlertNo = New System.Windows.Forms.RadioButton()
        Me.rdoAttendaacnceEmailAlert = New System.Windows.Forms.RadioButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rdoAutoBreakAttendanceNo = New System.Windows.Forms.RadioButton()
        Me.rdoAutoBreakAttendance = New System.Windows.Forms.RadioButton()
        Me.TxtTotalLeaves = New System.Windows.Forms.TextBox()
        Me.btnAttendanceDbPath = New System.Windows.Forms.Button()
        Me.txtAttendanceDbPath = New System.Windows.Forms.TextBox()
        Me.txtWorkingDays = New System.Windows.Forms.TextBox()
        Me.DtpStartDate = New System.Windows.Forms.DateTimePicker()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel8.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel11.SuspendLayout()
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
        Me.Panel2.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(12, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(323, 37)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Attendance Configuration"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.lblEmployeeAccountMapping)
        Me.Panel3.Controls.Add(Me.lblProvidentFund)
        Me.Panel3.Controls.Add(Me.Label17)
        Me.Panel3.Controls.Add(Me.Label18)
        Me.Panel3.Controls.Add(Me.lblOvertime)
        Me.Panel3.Controls.Add(Me.lblSalary)
        Me.Panel3.Controls.Add(Me.Label12)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(793, 61)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(215, 668)
        Me.Panel3.TabIndex = 2
        '
        'lblEmployeeAccountMapping
        '
        Me.lblEmployeeAccountMapping.AutoSize = True
        Me.lblEmployeeAccountMapping.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblEmployeeAccountMapping.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmployeeAccountMapping.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.lblEmployeeAccountMapping.Location = New System.Drawing.Point(20, 193)
        Me.lblEmployeeAccountMapping.Name = "lblEmployeeAccountMapping"
        Me.lblEmployeeAccountMapping.Size = New System.Drawing.Size(138, 42)
        Me.lblEmployeeAccountMapping.TabIndex = 4
        Me.lblEmployeeAccountMapping.Text = "Employee Account" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Mapping"
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
        Me.lblProvidentFund.TabIndex = 3
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
        Me.Label17.TabIndex = 6
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
        Me.Label18.TabIndex = 5
        Me.Label18.Text = "Have a Question(s)"
        '
        'lblOvertime
        '
        Me.lblOvertime.AutoSize = True
        Me.lblOvertime.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblOvertime.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblOvertime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.lblOvertime.Location = New System.Drawing.Point(20, 103)
        Me.lblOvertime.Name = "lblOvertime"
        Me.lblOvertime.Size = New System.Drawing.Size(75, 21)
        Me.lblOvertime.TabIndex = 2
        Me.lblOvertime.Text = "Overtime"
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
        Me.lblSalary.TabIndex = 1
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
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Related Settings"
        '
        'Panel8
        '
        Me.Panel8.AutoScroll = True
        Me.Panel8.Controls.Add(Me.Panel4)
        Me.Panel8.Controls.Add(Me.Panel1)
        Me.Panel8.Controls.Add(Me.TxtTotalLeaves)
        Me.Panel8.Controls.Add(Me.btnAttendanceDbPath)
        Me.Panel8.Controls.Add(Me.txtAttendanceDbPath)
        Me.Panel8.Controls.Add(Me.txtWorkingDays)
        Me.Panel8.Controls.Add(Me.DtpStartDate)
        Me.Panel8.Controls.Add(Me.Panel14)
        Me.Panel8.Controls.Add(Me.Label24)
        Me.Panel8.Controls.Add(Me.Panel13)
        Me.Panel8.Controls.Add(Me.Label6)
        Me.Panel8.Controls.Add(Me.Panel7)
        Me.Panel8.Controls.Add(Me.Label8)
        Me.Panel8.Controls.Add(Me.Panel10)
        Me.Panel8.Controls.Add(Me.Panel11)
        Me.Panel8.Controls.Add(Me.Label11)
        Me.Panel8.Controls.Add(Me.Label16)
        Me.Panel8.Controls.Add(Me.Label20)
        Me.Panel8.Controls.Add(Me.Panel12)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel8.Location = New System.Drawing.Point(0, 61)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(793, 668)
        Me.Panel8.TabIndex = 1
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.rdoAttendaacnceEmailAlertNo)
        Me.Panel4.Controls.Add(Me.rdoAttendaacnceEmailAlert)
        Me.Panel4.Location = New System.Drawing.Point(256, 496)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(405, 37)
        Me.Panel4.TabIndex = 12
        '
        'rdoAttendaacnceEmailAlertNo
        '
        Me.rdoAttendaacnceEmailAlertNo.AutoSize = True
        Me.rdoAttendaacnceEmailAlertNo.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoAttendaacnceEmailAlertNo.Location = New System.Drawing.Point(89, 7)
        Me.rdoAttendaacnceEmailAlertNo.Name = "rdoAttendaacnceEmailAlertNo"
        Me.rdoAttendaacnceEmailAlertNo.Size = New System.Drawing.Size(47, 24)
        Me.rdoAttendaacnceEmailAlertNo.TabIndex = 1
        Me.rdoAttendaacnceEmailAlertNo.TabStop = True
        Me.rdoAttendaacnceEmailAlertNo.Text = "No"
        Me.rdoAttendaacnceEmailAlertNo.UseVisualStyleBackColor = True
        '
        'rdoAttendaacnceEmailAlert
        '
        Me.rdoAttendaacnceEmailAlert.AutoSize = True
        Me.rdoAttendaacnceEmailAlert.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoAttendaacnceEmailAlert.Location = New System.Drawing.Point(2, 7)
        Me.rdoAttendaacnceEmailAlert.Name = "rdoAttendaacnceEmailAlert"
        Me.rdoAttendaacnceEmailAlert.Size = New System.Drawing.Size(48, 24)
        Me.rdoAttendaacnceEmailAlert.TabIndex = 0
        Me.rdoAttendaacnceEmailAlert.TabStop = True
        Me.rdoAttendaacnceEmailAlert.Tag = "EnabledAttendanceEmailAlert"
        Me.rdoAttendaacnceEmailAlert.Text = "Yes"
        Me.rdoAttendaacnceEmailAlert.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rdoAutoBreakAttendanceNo)
        Me.Panel1.Controls.Add(Me.rdoAutoBreakAttendance)
        Me.Panel1.Location = New System.Drawing.Point(256, 394)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(405, 37)
        Me.Panel1.TabIndex = 10
        '
        'rdoAutoBreakAttendanceNo
        '
        Me.rdoAutoBreakAttendanceNo.AutoSize = True
        Me.rdoAutoBreakAttendanceNo.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoAutoBreakAttendanceNo.Location = New System.Drawing.Point(89, 7)
        Me.rdoAutoBreakAttendanceNo.Name = "rdoAutoBreakAttendanceNo"
        Me.rdoAutoBreakAttendanceNo.Size = New System.Drawing.Size(47, 24)
        Me.rdoAutoBreakAttendanceNo.TabIndex = 1
        Me.rdoAutoBreakAttendanceNo.TabStop = True
        Me.rdoAutoBreakAttendanceNo.Text = "No"
        Me.rdoAutoBreakAttendanceNo.UseVisualStyleBackColor = True
        '
        'rdoAutoBreakAttendance
        '
        Me.rdoAutoBreakAttendance.AutoSize = True
        Me.rdoAutoBreakAttendance.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoAutoBreakAttendance.Location = New System.Drawing.Point(2, 7)
        Me.rdoAutoBreakAttendance.Name = "rdoAutoBreakAttendance"
        Me.rdoAutoBreakAttendance.Size = New System.Drawing.Size(48, 24)
        Me.rdoAutoBreakAttendance.TabIndex = 0
        Me.rdoAutoBreakAttendance.TabStop = True
        Me.rdoAutoBreakAttendance.Tag = "AutoBreakAttendance"
        Me.rdoAutoBreakAttendance.Text = "Yes"
        Me.rdoAutoBreakAttendance.UseVisualStyleBackColor = True
        '
        'TxtTotalLeaves
        '
        Me.TxtTotalLeaves.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTotalLeaves.Location = New System.Drawing.Point(256, 304)
        Me.TxtTotalLeaves.Name = "TxtTotalLeaves"
        Me.TxtTotalLeaves.Size = New System.Drawing.Size(311, 27)
        Me.TxtTotalLeaves.TabIndex = 8
        '
        'btnAttendanceDbPath
        '
        Me.btnAttendanceDbPath.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.btnAttendanceDbPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAttendanceDbPath.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAttendanceDbPath.ForeColor = System.Drawing.Color.White
        Me.btnAttendanceDbPath.Location = New System.Drawing.Point(586, 208)
        Me.btnAttendanceDbPath.Name = "btnAttendanceDbPath"
        Me.btnAttendanceDbPath.Size = New System.Drawing.Size(75, 27)
        Me.btnAttendanceDbPath.TabIndex = 6
        Me.btnAttendanceDbPath.Text = "Browse"
        Me.btnAttendanceDbPath.UseVisualStyleBackColor = False
        '
        'txtAttendanceDbPath
        '
        Me.txtAttendanceDbPath.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAttendanceDbPath.Location = New System.Drawing.Point(256, 208)
        Me.txtAttendanceDbPath.Name = "txtAttendanceDbPath"
        Me.txtAttendanceDbPath.Size = New System.Drawing.Size(311, 27)
        Me.txtAttendanceDbPath.TabIndex = 5
        '
        'txtWorkingDays
        '
        Me.txtWorkingDays.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtWorkingDays.Location = New System.Drawing.Point(256, 113)
        Me.txtWorkingDays.Name = "txtWorkingDays"
        Me.txtWorkingDays.Size = New System.Drawing.Size(311, 27)
        Me.txtWorkingDays.TabIndex = 3
        '
        'DtpStartDate
        '
        Me.DtpStartDate.Checked = False
        Me.DtpStartDate.CustomFormat = "dd/MMM/yyyy"
        Me.DtpStartDate.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DtpStartDate.Location = New System.Drawing.Point(256, 14)
        Me.DtpStartDate.Name = "DtpStartDate"
        Me.DtpStartDate.ShowCheckBox = True
        Me.DtpStartDate.Size = New System.Drawing.Size(311, 27)
        Me.DtpStartDate.TabIndex = 1
        '
        'Panel14
        '
        Me.Panel14.BackColor = System.Drawing.Color.LemonChiffon
        Me.Panel14.Controls.Add(Me.Label23)
        Me.Panel14.Location = New System.Drawing.Point(256, 539)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(405, 51)
        Me.Panel14.TabIndex = 128
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label23.Location = New System.Drawing.Point(3, 17)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(197, 17)
        Me.Label23.TabIndex = 0
        Me.Label23.Text = "email attendace report to admin"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Label24.Location = New System.Drawing.Point(64, 512)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(158, 21)
        Me.Label24.TabIndex = 11
        Me.Label24.Text = "Attendace Email Alert"
        '
        'Panel13
        '
        Me.Panel13.BackColor = System.Drawing.Color.LemonChiffon
        Me.Panel13.Controls.Add(Me.Label22)
        Me.Panel13.Location = New System.Drawing.Point(256, 437)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(405, 51)
        Me.Panel13.TabIndex = 123
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label22.Location = New System.Drawing.Point(3, 17)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(253, 17)
        Me.Label22.TabIndex = 0
        Me.Label22.Text = "use to mark out attendance on break time"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Label6.Location = New System.Drawing.Point(45, 410)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(168, 21)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Auto Break Attendance"
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.LemonChiffon
        Me.Panel7.Controls.Add(Me.Label5)
        Me.Panel7.Location = New System.Drawing.Point(256, 337)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(405, 51)
        Me.Panel7.TabIndex = 122
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(3, 17)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(183, 17)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "use to fix leave for employees"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Label8.Location = New System.Drawing.Point(77, 308)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(136, 21)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Total Leaves Allow"
        '
        'Panel10
        '
        Me.Panel10.BackColor = System.Drawing.Color.LemonChiffon
        Me.Panel10.Controls.Add(Me.Label9)
        Me.Panel10.Location = New System.Drawing.Point(256, 239)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(405, 51)
        Me.Panel10.TabIndex = 118
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(3, 17)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(178, 17)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "Alternate Attendance DB Path"
        '
        'Panel11
        '
        Me.Panel11.BackColor = System.Drawing.Color.LemonChiffon
        Me.Panel11.Controls.Add(Me.Label10)
        Me.Panel11.Location = New System.Drawing.Point(256, 146)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(405, 51)
        Me.Panel11.TabIndex = 10
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(3, 17)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(185, 17)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "use fix working days for salary"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Label11.Location = New System.Drawing.Point(58, 210)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(155, 42)
        Me.Label11.TabIndex = 4
        Me.Label11.Text = "Alternate Attendance" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "DB Path"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Label16.Location = New System.Drawing.Point(24, 119)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(189, 21)
        Me.Label16.TabIndex = 2
        Me.Label16.Text = "Attendance Working Days"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Label20.Location = New System.Drawing.Point(48, 18)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(165, 21)
        Me.Label20.TabIndex = 0
        Me.Label20.Text = "Attendance Start From"
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
        Me.Label21.Location = New System.Drawing.Point(3, 17)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(282, 17)
        Me.Label21.TabIndex = 0
        Me.Label21.Text = "use to calculate attendance date from this date"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'frmConfigHRAttendance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Controls.Add(Me.Panel8)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Name = "frmConfigHRAttendance"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "HR Attendance Configuration"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel14.ResumeLayout(False)
        Me.Panel14.PerformLayout()
        Me.Panel13.ResumeLayout(False)
        Me.Panel13.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        Me.Panel11.ResumeLayout(False)
        Me.Panel11.PerformLayout()
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
    Friend WithEvents lblOvertime As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblSalary As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents rdoAutoBreakAttendanceNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdoAutoBreakAttendance As System.Windows.Forms.RadioButton
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents txtAttendanceDbPath As System.Windows.Forms.TextBox
    Friend WithEvents btnAttendanceDbPath As System.Windows.Forms.Button
    Friend WithEvents TxtTotalLeaves As System.Windows.Forms.TextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents rdoAttendaacnceEmailAlertNo As System.Windows.Forms.RadioButton
    Friend WithEvents rdoAttendaacnceEmailAlert As System.Windows.Forms.RadioButton
    Friend WithEvents txtWorkingDays As System.Windows.Forms.TextBox
    Friend WithEvents DtpStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblEmployeeAccountMapping As System.Windows.Forms.Label
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
End Class

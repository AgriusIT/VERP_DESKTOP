<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmActivityCalender
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
        Me.Schedule1 = New Janus.Windows.Schedule.Schedule()
        Me.cmbResponsiblePerson = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblFromDate = New System.Windows.Forms.Label()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.rbtnMonthlyView = New System.Windows.Forms.RadioButton()
        Me.rbtnWeeklyView = New System.Windows.Forms.RadioButton()
        Me.rbtnDailyView = New System.Windows.Forms.RadioButton()
        Me.cmbInside = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbManager = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        CType(Me.Schedule1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'Schedule1
        '
        Me.Schedule1.AddNewMode = Janus.Windows.Schedule.AddNewMode.Manual
        Me.Schedule1.AllowAppointmentDrag = Janus.Windows.Schedule.AllowAppointmentDrag.None
        Me.Schedule1.AllowEdit = False
        Me.Schedule1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Schedule1.BackColor = System.Drawing.SystemColors.GradientActiveCaption
        Me.Schedule1.Date = New Date(2017, 9, 25, 0, 0, 0, 0)
        Me.Schedule1.FirstDayOfWeek = Janus.Windows.Schedule.ScheduleDayOfWeek.Monday
        Me.Schedule1.FromText = ""
        Me.Schedule1.Location = New System.Drawing.Point(14, 249)
        Me.Schedule1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Schedule1.Name = "Schedule1"
        Me.Schedule1.Size = New System.Drawing.Size(1458, 577)
        Me.Schedule1.TabIndex = 5
        Me.Schedule1.ToText = ""
        Me.Schedule1.View = Janus.Windows.Schedule.ScheduleView.MonthView
        Me.Schedule1.VisualStyle = Janus.Windows.Schedule.VisualStyle.VS2005
        Me.Schedule1.WorkWeek = CType(((((((Janus.Windows.Schedule.ScheduleDayOfWeek.Sunday Or Janus.Windows.Schedule.ScheduleDayOfWeek.Monday) _
            Or Janus.Windows.Schedule.ScheduleDayOfWeek.Tuesday) _
            Or Janus.Windows.Schedule.ScheduleDayOfWeek.Wednesday) _
            Or Janus.Windows.Schedule.ScheduleDayOfWeek.Thursday) _
            Or Janus.Windows.Schedule.ScheduleDayOfWeek.Friday) _
            Or Janus.Windows.Schedule.ScheduleDayOfWeek.Saturday), Janus.Windows.Schedule.ScheduleDayOfWeek)
        '
        'cmbResponsiblePerson
        '
        Me.cmbResponsiblePerson.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbResponsiblePerson.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbResponsiblePerson.BackColor = System.Drawing.Color.White
        Me.cmbResponsiblePerson.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbResponsiblePerson.FormattingEnabled = True
        Me.cmbResponsiblePerson.Location = New System.Drawing.Point(704, 122)
        Me.cmbResponsiblePerson.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbResponsiblePerson.Name = "cmbResponsiblePerson"
        Me.cmbResponsiblePerson.Size = New System.Drawing.Size(214, 36)
        Me.cmbResponsiblePerson.TabIndex = 3
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(699, 91)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(179, 28)
        Me.Label9.TabIndex = 211
        Me.Label9.Text = "Responsible Person"
        '
        'lblFromDate
        '
        Me.lblFromDate.AutoSize = True
        Me.lblFromDate.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFromDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFromDate.Location = New System.Drawing.Point(246, 91)
        Me.lblFromDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(104, 28)
        Me.lblFromDate.TabIndex = 218
        Me.lblFromDate.Text = "From Date"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpFromDate.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(250, 122)
        Me.dtpFromDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(214, 33)
        Me.dtpFromDate.TabIndex = 1
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd-MMM-yyyy"
        Me.dtpToDate.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(477, 122)
        Me.dtpToDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(214, 33)
        Me.dtpToDate.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(472, 91)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 28)
        Me.Label1.TabIndex = 218
        Me.Label1.Text = "To Date"
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(1378, 118)
        Me.btnShow.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(106, 43)
        Me.btnShow.TabIndex = 4
        Me.btnShow.Text = "Show"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.Teal
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1490, 71)
        Me.pnlHeader.TabIndex = 6
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.White
        Me.lblHeader.Location = New System.Drawing.Point(12, 14)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(251, 40)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Activity Calendar"
        '
        'cmbPeriod
        '
        Me.cmbPeriod.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbPeriod.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPeriod.FormattingEnabled = True
        Me.cmbPeriod.Items.AddRange(New Object() {"Today", "Yesterday", "Current Week", "Current Month", "Current Year"})
        Me.cmbPeriod.Location = New System.Drawing.Point(14, 126)
        Me.cmbPeriod.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(226, 28)
        Me.cmbPeriod.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(9, 91)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 28)
        Me.Label3.TabIndex = 222
        Me.Label3.Text = "Period"
        '
        'rbtnMonthlyView
        '
        Me.rbtnMonthlyView.AutoSize = True
        Me.rbtnMonthlyView.Checked = True
        Me.rbtnMonthlyView.Location = New System.Drawing.Point(272, 192)
        Me.rbtnMonthlyView.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtnMonthlyView.Name = "rbtnMonthlyView"
        Me.rbtnMonthlyView.Size = New System.Drawing.Size(127, 24)
        Me.rbtnMonthlyView.TabIndex = 225
        Me.rbtnMonthlyView.TabStop = True
        Me.rbtnMonthlyView.Text = "Monthly View"
        Me.rbtnMonthlyView.UseVisualStyleBackColor = True
        '
        'rbtnWeeklyView
        '
        Me.rbtnWeeklyView.AutoSize = True
        Me.rbtnWeeklyView.Location = New System.Drawing.Point(132, 192)
        Me.rbtnWeeklyView.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtnWeeklyView.Name = "rbtnWeeklyView"
        Me.rbtnWeeklyView.Size = New System.Drawing.Size(123, 24)
        Me.rbtnWeeklyView.TabIndex = 224
        Me.rbtnWeeklyView.Text = "Weekly View"
        Me.rbtnWeeklyView.UseVisualStyleBackColor = True
        '
        'rbtnDailyView
        '
        Me.rbtnDailyView.AutoSize = True
        Me.rbtnDailyView.Location = New System.Drawing.Point(12, 192)
        Me.rbtnDailyView.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtnDailyView.Name = "rbtnDailyView"
        Me.rbtnDailyView.Size = New System.Drawing.Size(106, 24)
        Me.rbtnDailyView.TabIndex = 223
        Me.rbtnDailyView.Text = "Daily View"
        Me.rbtnDailyView.UseVisualStyleBackColor = True
        '
        'cmbInside
        '
        Me.cmbInside.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbInside.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbInside.BackColor = System.Drawing.Color.White
        Me.cmbInside.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbInside.FormattingEnabled = True
        Me.cmbInside.Location = New System.Drawing.Point(928, 122)
        Me.cmbInside.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbInside.Name = "cmbInside"
        Me.cmbInside.Size = New System.Drawing.Size(214, 36)
        Me.cmbInside.TabIndex = 226
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(924, 91)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(175, 28)
        Me.Label4.TabIndex = 227
        Me.Label4.Text = "Inside Sales Person"
        '
        'cmbManager
        '
        Me.cmbManager.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbManager.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbManager.BackColor = System.Drawing.Color.White
        Me.cmbManager.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbManager.FormattingEnabled = True
        Me.cmbManager.Location = New System.Drawing.Point(1154, 122)
        Me.cmbManager.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbManager.Name = "cmbManager"
        Me.cmbManager.Size = New System.Drawing.Size(214, 36)
        Me.cmbManager.TabIndex = 228
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(1149, 91)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(90, 28)
        Me.Label5.TabIndex = 229
        Me.Label5.Text = "Manager"
        '
        'frmActivityCalender
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1490, 846)
        Me.Controls.Add(Me.cmbManager)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmbInside)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.rbtnMonthlyView)
        Me.Controls.Add(Me.rbtnWeeklyView)
        Me.Controls.Add(Me.rbtnDailyView)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbPeriod)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.btnShow)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtpToDate)
        Me.Controls.Add(Me.lblFromDate)
        Me.Controls.Add(Me.dtpFromDate)
        Me.Controls.Add(Me.cmbResponsiblePerson)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Schedule1)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmActivityCalender"
        Me.Text = "frmActivityCalender"
        CType(Me.Schedule1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Schedule1 As Janus.Windows.Schedule.Schedule
    Friend WithEvents cmbResponsiblePerson As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblFromDate As System.Windows.Forms.Label
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents rbtnMonthlyView As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnWeeklyView As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnDailyView As System.Windows.Forms.RadioButton
    Friend WithEvents cmbInside As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbManager As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class

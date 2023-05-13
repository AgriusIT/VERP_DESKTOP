<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmployeeVisitPlan
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmployeeVisitPlan))
        Me.Schedule1 = New Janus.Windows.Schedule.Schedule()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.pnlPeriod = New System.Windows.Forms.Panel()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.rbtnMonthlyView = New System.Windows.Forms.RadioButton()
        Me.rbtnWeeklyView = New System.Windows.Forms.RadioButton()
        Me.rbtnDailyView = New System.Windows.Forms.RadioButton()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        Me.lblToDate = New System.Windows.Forms.Label()
        Me.lblDateFrom = New System.Windows.Forms.Label()
        Me.lblPeriod = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.Schedule1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.pnlPeriod.SuspendLayout()
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
        Me.Schedule1.Location = New System.Drawing.Point(0, 129)
        Me.Schedule1.Name = "Schedule1"
        Me.Schedule1.Size = New System.Drawing.Size(967, 481)
        Me.Schedule1.TabIndex = 2
        Me.Schedule1.View = Janus.Windows.Schedule.ScheduleView.MonthView
        Me.Schedule1.VisualStyle = Janus.Windows.Schedule.VisualStyle.Office2007
        Me.Schedule1.WorkWeek = CType(((((((Janus.Windows.Schedule.ScheduleDayOfWeek.Sunday Or Janus.Windows.Schedule.ScheduleDayOfWeek.Monday) _
            Or Janus.Windows.Schedule.ScheduleDayOfWeek.Tuesday) _
            Or Janus.Windows.Schedule.ScheduleDayOfWeek.Wednesday) _
            Or Janus.Windows.Schedule.ScheduleDayOfWeek.Thursday) _
            Or Janus.Windows.Schedule.ScheduleDayOfWeek.Friday) _
            Or Janus.Windows.Schedule.ScheduleDayOfWeek.Saturday), Janus.Windows.Schedule.ScheduleDayOfWeek)
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(400, -1)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(266, 50)
        Me.lblProgress.TabIndex = 1
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnClose)
        Me.pnlHeader.Controls.Add(Me.lblProgress)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(967, 47)
        Me.pnlHeader.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.btnClose.Location = New System.Drawing.Point(919, 8)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(36, 30)
        Me.btnClose.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.btnClose, "Close")
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lblHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.lblHeader.Location = New System.Drawing.Point(8, 10)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(273, 23)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Employee Visit Planning"
        '
        'pnlPeriod
        '
        Me.pnlPeriod.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlPeriod.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.pnlPeriod.Controls.Add(Me.btnShow)
        Me.pnlPeriod.Controls.Add(Me.rbtnMonthlyView)
        Me.pnlPeriod.Controls.Add(Me.rbtnWeeklyView)
        Me.pnlPeriod.Controls.Add(Me.rbtnDailyView)
        Me.pnlPeriod.Controls.Add(Me.dtpToDate)
        Me.pnlPeriod.Controls.Add(Me.dtpFromDate)
        Me.pnlPeriod.Controls.Add(Me.cmbPeriod)
        Me.pnlPeriod.Controls.Add(Me.lblToDate)
        Me.pnlPeriod.Controls.Add(Me.lblDateFrom)
        Me.pnlPeriod.Controls.Add(Me.lblPeriod)
        Me.pnlPeriod.Location = New System.Drawing.Point(12, 53)
        Me.pnlPeriod.Name = "pnlPeriod"
        Me.pnlPeriod.Size = New System.Drawing.Size(943, 70)
        Me.pnlPeriod.TabIndex = 1
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(597, 37)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(71, 23)
        Me.btnShow.TabIndex = 9
        Me.btnShow.Text = "Show"
        Me.ToolTip1.SetToolTip(Me.btnShow, "Show")
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'rbtnMonthlyView
        '
        Me.rbtnMonthlyView.AutoSize = True
        Me.rbtnMonthlyView.Checked = True
        Me.rbtnMonthlyView.Location = New System.Drawing.Point(503, 40)
        Me.rbtnMonthlyView.Name = "rbtnMonthlyView"
        Me.rbtnMonthlyView.Size = New System.Drawing.Size(88, 17)
        Me.rbtnMonthlyView.TabIndex = 8
        Me.rbtnMonthlyView.TabStop = True
        Me.rbtnMonthlyView.Text = "Monthly View"
        Me.ToolTip1.SetToolTip(Me.rbtnMonthlyView, "Monthly View")
        Me.rbtnMonthlyView.UseVisualStyleBackColor = True
        '
        'rbtnWeeklyView
        '
        Me.rbtnWeeklyView.AutoSize = True
        Me.rbtnWeeklyView.Location = New System.Drawing.Point(410, 40)
        Me.rbtnWeeklyView.Name = "rbtnWeeklyView"
        Me.rbtnWeeklyView.Size = New System.Drawing.Size(87, 17)
        Me.rbtnWeeklyView.TabIndex = 7
        Me.rbtnWeeklyView.Text = "Weekly View"
        Me.ToolTip1.SetToolTip(Me.rbtnWeeklyView, "Weekly View")
        Me.rbtnWeeklyView.UseVisualStyleBackColor = True
        '
        'rbtnDailyView
        '
        Me.rbtnDailyView.AutoSize = True
        Me.rbtnDailyView.Location = New System.Drawing.Point(330, 40)
        Me.rbtnDailyView.Name = "rbtnDailyView"
        Me.rbtnDailyView.Size = New System.Drawing.Size(74, 17)
        Me.rbtnDailyView.TabIndex = 6
        Me.rbtnDailyView.Text = "Daily View"
        Me.ToolTip1.SetToolTip(Me.rbtnDailyView, "Daily View")
        Me.rbtnDailyView.UseVisualStyleBackColor = True
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(211, 38)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(112, 20)
        Me.dtpToDate.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.dtpToDate, "To Date")
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(68, 38)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(112, 20)
        Me.dtpFromDate.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.dtpFromDate, "From Date")
        '
        'cmbPeriod
        '
        Me.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPeriod.FormattingEnabled = True
        Me.cmbPeriod.Items.AddRange(New Object() {"Today", "Yesterday", "Current Week", "Current Month", "Current Year"})
        Me.cmbPeriod.Location = New System.Drawing.Point(68, 8)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(255, 21)
        Me.cmbPeriod.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbPeriod, "Select Period")
        '
        'lblToDate
        '
        Me.lblToDate.AutoSize = True
        Me.lblToDate.Location = New System.Drawing.Point(186, 44)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(20, 13)
        Me.lblToDate.TabIndex = 4
        Me.lblToDate.Text = "To"
        '
        'lblDateFrom
        '
        Me.lblDateFrom.AutoSize = True
        Me.lblDateFrom.Location = New System.Drawing.Point(7, 44)
        Me.lblDateFrom.Name = "lblDateFrom"
        Me.lblDateFrom.Size = New System.Drawing.Size(30, 13)
        Me.lblDateFrom.TabIndex = 2
        Me.lblDateFrom.Text = "From"
        '
        'lblPeriod
        '
        Me.lblPeriod.AutoSize = True
        Me.lblPeriod.Location = New System.Drawing.Point(7, 11)
        Me.lblPeriod.Name = "lblPeriod"
        Me.lblPeriod.Size = New System.Drawing.Size(37, 13)
        Me.lblPeriod.TabIndex = 0
        Me.lblPeriod.Text = "Period"
        '
        'frmEmployeeVisitPlan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(967, 610)
        Me.Controls.Add(Me.pnlPeriod)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.Schedule1)
        Me.Name = "frmEmployeeVisitPlan"
        Me.Text = "frmEmployeeVisitPlan"
        CType(Me.Schedule1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlPeriod.ResumeLayout(False)
        Me.pnlPeriod.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Schedule1 As Janus.Windows.Schedule.Schedule
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents pnlPeriod As System.Windows.Forms.Panel
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents lblToDate As System.Windows.Forms.Label
    Friend WithEvents lblDateFrom As System.Windows.Forms.Label
    Friend WithEvents lblPeriod As System.Windows.Forms.Label
    Friend WithEvents rbtnMonthlyView As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnWeeklyView As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnDailyView As System.Windows.Forms.RadioButton
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

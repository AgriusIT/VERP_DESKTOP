<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmpAttendanceSumm
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
        Me.components = New System.ComponentModel.Container()
        Dim grdEmpAttendance_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmpAttendanceSumm))
        Me.cmbYear = New System.Windows.Forms.ComboBox()
        Me.cmbMonth = New System.Windows.Forms.ComboBox()
        Me.lblMonth = New System.Windows.Forms.Label()
        Me.lblMobileExp = New System.Windows.Forms.Label()
        Me.grdEmpAttendance = New Janus.Windows.GridEX.GridEX()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.txtBasicSalary = New System.Windows.Forms.TextBox()
        Me.txtWorkingDays = New System.Windows.Forms.TextBox()
        Me.txtGrossSalary = New System.Windows.Forms.TextBox()
        Me.btnGenerateSalary = New System.Windows.Forms.Button()
        Me.lblBasicSalary = New System.Windows.Forms.Label()
        Me.lblWorkingDays = New System.Windows.Forms.Label()
        Me.lblGrossSalary = New System.Windows.Forms.Label()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolTip2 = New System.Windows.Forms.ToolTip(Me.components)
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.grpMonth = New System.Windows.Forms.GroupBox()
        Me.grpDateRange = New System.Windows.Forms.GroupBox()
        Me.OptMonthly = New System.Windows.Forms.RadioButton()
        Me.OptDateRange = New System.Windows.Forms.RadioButton()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        CType(Me.grdEmpAttendance, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpMonth.SuspendLayout()
        Me.grpDateRange.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbYear
        '
        Me.cmbYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbYear.FormattingEnabled = True
        Me.cmbYear.Location = New System.Drawing.Point(60, 19)
        Me.cmbYear.Name = "cmbYear"
        Me.cmbYear.Size = New System.Drawing.Size(185, 21)
        Me.cmbYear.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbYear, "Select Year")
        '
        'cmbMonth
        '
        Me.cmbMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbMonth.FormattingEnabled = True
        Me.cmbMonth.Location = New System.Drawing.Point(60, 46)
        Me.cmbMonth.Name = "cmbMonth"
        Me.cmbMonth.Size = New System.Drawing.Size(185, 21)
        Me.cmbMonth.TabIndex = 3
        Me.ToolTip2.SetToolTip(Me.cmbMonth, "Select Month")
        '
        'lblMonth
        '
        Me.lblMonth.AutoSize = True
        Me.lblMonth.Location = New System.Drawing.Point(8, 50)
        Me.lblMonth.Name = "lblMonth"
        Me.lblMonth.Size = New System.Drawing.Size(37, 13)
        Me.lblMonth.TabIndex = 2
        Me.lblMonth.Text = "Month"
        '
        'lblMobileExp
        '
        Me.lblMobileExp.AutoSize = True
        Me.lblMobileExp.Location = New System.Drawing.Point(8, 22)
        Me.lblMobileExp.Name = "lblMobileExp"
        Me.lblMobileExp.Size = New System.Drawing.Size(29, 13)
        Me.lblMobileExp.TabIndex = 0
        Me.lblMobileExp.Text = "Year"
        '
        'grdEmpAttendance
        '
        Me.grdEmpAttendance.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdEmpAttendance_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grdEmpAttendance.DesignTimeLayout = grdEmpAttendance_DesignTimeLayout
        Me.grdEmpAttendance.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdEmpAttendance.GroupByBoxVisible = False
        Me.grdEmpAttendance.Location = New System.Drawing.Point(1, 251)
        Me.grdEmpAttendance.Name = "grdEmpAttendance"
        Me.grdEmpAttendance.RecordNavigator = True
        Me.grdEmpAttendance.Size = New System.Drawing.Size(465, 107)
        Me.grdEmpAttendance.TabIndex = 4
        Me.grdEmpAttendance.TabStop = False
        Me.grdEmpAttendance.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdEmpAttendance.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdEmpAttendance.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(171, 223)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(87, 23)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Load"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button1.UseVisualStyleBackColor = True
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(3, 6)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(388, 23)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Attendance Based Calculate Salary"
        '
        'txtBasicSalary
        '
        Me.txtBasicSalary.Location = New System.Drawing.Point(91, 364)
        Me.txtBasicSalary.Name = "txtBasicSalary"
        Me.txtBasicSalary.Size = New System.Drawing.Size(167, 20)
        Me.txtBasicSalary.TabIndex = 6
        '
        'txtWorkingDays
        '
        Me.txtWorkingDays.Location = New System.Drawing.Point(91, 390)
        Me.txtWorkingDays.Name = "txtWorkingDays"
        Me.txtWorkingDays.Size = New System.Drawing.Size(167, 20)
        Me.txtWorkingDays.TabIndex = 8
        '
        'txtGrossSalary
        '
        Me.txtGrossSalary.Location = New System.Drawing.Point(91, 416)
        Me.txtGrossSalary.Name = "txtGrossSalary"
        Me.txtGrossSalary.Size = New System.Drawing.Size(167, 20)
        Me.txtGrossSalary.TabIndex = 10
        '
        'btnGenerateSalary
        '
        Me.btnGenerateSalary.Location = New System.Drawing.Point(89, 442)
        Me.btnGenerateSalary.Name = "btnGenerateSalary"
        Me.btnGenerateSalary.Size = New System.Drawing.Size(95, 23)
        Me.btnGenerateSalary.TabIndex = 11
        Me.btnGenerateSalary.Text = "Generate Salary"
        Me.btnGenerateSalary.UseVisualStyleBackColor = True
        '
        'lblBasicSalary
        '
        Me.lblBasicSalary.AutoSize = True
        Me.lblBasicSalary.Location = New System.Drawing.Point(12, 371)
        Me.lblBasicSalary.Name = "lblBasicSalary"
        Me.lblBasicSalary.Size = New System.Drawing.Size(65, 13)
        Me.lblBasicSalary.TabIndex = 5
        Me.lblBasicSalary.Text = "Basic Salary"
        '
        'lblWorkingDays
        '
        Me.lblWorkingDays.AutoSize = True
        Me.lblWorkingDays.Location = New System.Drawing.Point(12, 393)
        Me.lblWorkingDays.Name = "lblWorkingDays"
        Me.lblWorkingDays.Size = New System.Drawing.Size(74, 13)
        Me.lblWorkingDays.TabIndex = 7
        Me.lblWorkingDays.Text = "Working Days"
        '
        'lblGrossSalary
        '
        Me.lblGrossSalary.AutoSize = True
        Me.lblGrossSalary.Location = New System.Drawing.Point(12, 419)
        Me.lblGrossSalary.Name = "lblGrossSalary"
        Me.lblGrossSalary.Size = New System.Drawing.Size(66, 13)
        Me.lblGrossSalary.TabIndex = 9
        Me.lblGrossSalary.Text = "Gross Salary"
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(190, 441)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(68, 23)
        Me.btnClose.TabIndex = 12
        Me.btnClose.Text = "Close"
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'dtpFromDate
        '
        Me.dtpFromDate.Checked = False
        Me.dtpFromDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(68, 21)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(177, 20)
        Me.dtpFromDate.TabIndex = 1
        '
        'dtpToDate
        '
        Me.dtpToDate.Checked = False
        Me.dtpToDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(68, 47)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(177, 20)
        Me.dtpToDate.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 13)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "From Date"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 51)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "To Date"
        '
        'grpMonth
        '
        Me.grpMonth.Controls.Add(Me.cmbYear)
        Me.grpMonth.Controls.Add(Me.lblMobileExp)
        Me.grpMonth.Controls.Add(Me.lblMonth)
        Me.grpMonth.Controls.Add(Me.cmbMonth)
        Me.grpMonth.Location = New System.Drawing.Point(13, 54)
        Me.grpMonth.Name = "grpMonth"
        Me.grpMonth.Size = New System.Drawing.Size(442, 74)
        Me.grpMonth.TabIndex = 1
        Me.grpMonth.TabStop = False
        Me.grpMonth.Text = "Month"
        '
        'grpDateRange
        '
        Me.grpDateRange.Controls.Add(Me.Label2)
        Me.grpDateRange.Controls.Add(Me.dtpFromDate)
        Me.grpDateRange.Controls.Add(Me.Label3)
        Me.grpDateRange.Controls.Add(Me.dtpToDate)
        Me.grpDateRange.Location = New System.Drawing.Point(13, 134)
        Me.grpDateRange.Name = "grpDateRange"
        Me.grpDateRange.Size = New System.Drawing.Size(442, 81)
        Me.grpDateRange.TabIndex = 2
        Me.grpDateRange.TabStop = False
        Me.grpDateRange.Text = "Date Range"
        '
        'OptMonthly
        '
        Me.OptMonthly.AutoSize = True
        Me.OptMonthly.Checked = True
        Me.OptMonthly.Location = New System.Drawing.Point(73, 41)
        Me.OptMonthly.Name = "OptMonthly"
        Me.OptMonthly.Size = New System.Drawing.Size(62, 17)
        Me.OptMonthly.TabIndex = 13
        Me.OptMonthly.TabStop = True
        Me.OptMonthly.Text = "Monthly"
        Me.OptMonthly.UseVisualStyleBackColor = True
        '
        'OptDateRange
        '
        Me.OptDateRange.AutoSize = True
        Me.OptDateRange.Location = New System.Drawing.Point(141, 41)
        Me.OptDateRange.Name = "OptDateRange"
        Me.OptDateRange.Size = New System.Drawing.Size(83, 17)
        Me.OptDateRange.TabIndex = 14
        Me.OptDateRange.Text = "Date Range"
        Me.OptDateRange.UseVisualStyleBackColor = True
        '
        'pnlHeader
        '
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(467, 35)
        Me.pnlHeader.TabIndex = 78
        '
        'frmEmpAttendanceSumm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(467, 471)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.OptDateRange)
        Me.Controls.Add(Me.OptMonthly)
        Me.Controls.Add(Me.grpDateRange)
        Me.Controls.Add(Me.grpMonth)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.lblGrossSalary)
        Me.Controls.Add(Me.lblWorkingDays)
        Me.Controls.Add(Me.lblBasicSalary)
        Me.Controls.Add(Me.btnGenerateSalary)
        Me.Controls.Add(Me.txtGrossSalary)
        Me.Controls.Add(Me.txtWorkingDays)
        Me.Controls.Add(Me.txtBasicSalary)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.grdEmpAttendance)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEmpAttendanceSumm"
        Me.Text = "frmEmpAttendanceSumm"
        CType(Me.grdEmpAttendance, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpMonth.ResumeLayout(False)
        Me.grpMonth.PerformLayout()
        Me.grpDateRange.ResumeLayout(False)
        Me.grpDateRange.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmbYear As System.Windows.Forms.ComboBox
    Friend WithEvents cmbMonth As System.Windows.Forms.ComboBox
    Friend WithEvents lblMonth As System.Windows.Forms.Label
    Friend WithEvents lblMobileExp As System.Windows.Forms.Label
    Friend WithEvents grdEmpAttendance As Janus.Windows.GridEX.GridEX
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents txtBasicSalary As System.Windows.Forms.TextBox
    Friend WithEvents txtWorkingDays As System.Windows.Forms.TextBox
    Friend WithEvents txtGrossSalary As System.Windows.Forms.TextBox
    Friend WithEvents btnGenerateSalary As System.Windows.Forms.Button
    Friend WithEvents lblBasicSalary As System.Windows.Forms.Label
    Friend WithEvents lblWorkingDays As System.Windows.Forms.Label
    Friend WithEvents lblGrossSalary As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents ToolTip2 As System.Windows.Forms.ToolTip
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents grpMonth As System.Windows.Forms.GroupBox
    Friend WithEvents grpDateRange As System.Windows.Forms.GroupBox
    Friend WithEvents OptMonthly As System.Windows.Forms.RadioButton
    Friend WithEvents OptDateRange As System.Windows.Forms.RadioButton
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

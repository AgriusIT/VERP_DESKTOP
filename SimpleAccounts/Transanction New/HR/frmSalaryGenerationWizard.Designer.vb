<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSalaryGenerationWizard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSalaryGenerationWizard))
        Me.btnAttendanceSetup = New System.Windows.Forms.Button()
        Me.btnOverTime = New System.Windows.Forms.Button()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnDone = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnSalaryGeneration = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblAutoSalaryGenerate = New System.Windows.Forms.Label()
        Me.lblEmployeeOverTime = New System.Windows.Forms.Label()
        Me.lblAutoOverTime = New System.Windows.Forms.Label()
        Me.lblLoanDeduction = New System.Windows.Forms.Label()
        Me.lblAttendanceRegister = New System.Windows.Forms.Label()
        Me.lblDailyAttendance = New System.Windows.Forms.Label()
        Me.lblAttendanceSetup = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnLoanDeduction = New System.Windows.Forms.Button()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnAttendanceSetup
        '
        Me.btnAttendanceSetup.BackColor = System.Drawing.Color.Transparent
        Me.btnAttendanceSetup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnAttendanceSetup.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAttendanceSetup.FlatAppearance.BorderSize = 0
        Me.btnAttendanceSetup.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnAttendanceSetup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnAttendanceSetup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAttendanceSetup.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAttendanceSetup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnAttendanceSetup.Image = CType(resources.GetObject("btnAttendanceSetup.Image"), System.Drawing.Image)
        Me.btnAttendanceSetup.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAttendanceSetup.Location = New System.Drawing.Point(3, 83)
        Me.btnAttendanceSetup.Name = "btnAttendanceSetup"
        Me.btnAttendanceSetup.Size = New System.Drawing.Size(153, 43)
        Me.btnAttendanceSetup.TabIndex = 1
        Me.btnAttendanceSetup.Text = "Attendance Setup"
        Me.btnAttendanceSetup.UseVisualStyleBackColor = False
        '
        'btnOverTime
        '
        Me.btnOverTime.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnOverTime.FlatAppearance.BorderSize = 0
        Me.btnOverTime.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnOverTime.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnOverTime.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnOverTime.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnOverTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnOverTime.Image = CType(resources.GetObject("btnOverTime.Image"), System.Drawing.Image)
        Me.btnOverTime.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnOverTime.Location = New System.Drawing.Point(325, 83)
        Me.btnOverTime.Name = "btnOverTime"
        Me.btnOverTime.Size = New System.Drawing.Size(153, 43)
        Me.btnOverTime.TabIndex = 3
        Me.btnOverTime.Text = "Over Time"
        Me.btnOverTime.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.Controls.Add(Me.btnDone)
        Me.Panel3.Controls.Add(Me.btnCancel)
        Me.Panel3.Controls.Add(Me.btnBack)
        Me.Panel3.Controls.Add(Me.Button1)
        Me.Panel3.Location = New System.Drawing.Point(162, 316)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(615, 34)
        Me.Panel3.TabIndex = 1
        '
        'btnDone
        '
        Me.btnDone.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.btnDone.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnDone.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnDone.Location = New System.Drawing.Point(252, 7)
        Me.btnDone.Name = "btnDone"
        Me.btnDone.Size = New System.Drawing.Size(75, 23)
        Me.btnDone.TabIndex = 4
        Me.btnDone.Text = "Done"
        Me.btnDone.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancel.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnCancel.Location = New System.Drawing.Point(171, 7)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.btnBack.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnBack.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBack.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnBack.Location = New System.Drawing.Point(90, 7)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(75, 23)
        Me.btnBack.TabIndex = 1
        Me.btnBack.Text = "Back"
        Me.btnBack.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.Button1.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Button1.Font = New System.Drawing.Font("Arial Narrow", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.Button1.Location = New System.Drawing.Point(9, 7)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Next"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'btnSalaryGeneration
        '
        Me.btnSalaryGeneration.BackColor = System.Drawing.Color.Transparent
        Me.btnSalaryGeneration.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSalaryGeneration.FlatAppearance.BorderSize = 0
        Me.btnSalaryGeneration.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSalaryGeneration.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnSalaryGeneration.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSalaryGeneration.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSalaryGeneration.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnSalaryGeneration.Image = CType(resources.GetObject("btnSalaryGeneration.Image"), System.Drawing.Image)
        Me.btnSalaryGeneration.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSalaryGeneration.Location = New System.Drawing.Point(486, 83)
        Me.btnSalaryGeneration.Name = "btnSalaryGeneration"
        Me.btnSalaryGeneration.Size = New System.Drawing.Size(153, 43)
        Me.btnSalaryGeneration.TabIndex = 4
        Me.btnSalaryGeneration.Text = "Salary Generation"
        Me.btnSalaryGeneration.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.Panel2.Controls.Add(Me.lblAutoSalaryGenerate)
        Me.Panel2.Controls.Add(Me.lblEmployeeOverTime)
        Me.Panel2.Controls.Add(Me.lblAutoOverTime)
        Me.Panel2.Controls.Add(Me.lblLoanDeduction)
        Me.Panel2.Controls.Add(Me.lblAttendanceRegister)
        Me.Panel2.Controls.Add(Me.lblDailyAttendance)
        Me.Panel2.Controls.Add(Me.lblAttendanceSetup)
        Me.Panel2.Location = New System.Drawing.Point(1, 134)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(156, 350)
        Me.Panel2.TabIndex = 0
        '
        'lblAutoSalaryGenerate
        '
        Me.lblAutoSalaryGenerate.BackColor = System.Drawing.Color.Transparent
        Me.lblAutoSalaryGenerate.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblAutoSalaryGenerate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAutoSalaryGenerate.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.lblAutoSalaryGenerate.Location = New System.Drawing.Point(14, 290)
        Me.lblAutoSalaryGenerate.Name = "lblAutoSalaryGenerate"
        Me.lblAutoSalaryGenerate.Size = New System.Drawing.Size(146, 34)
        Me.lblAutoSalaryGenerate.TabIndex = 0
        Me.lblAutoSalaryGenerate.Text = "Auto Salary Generate"
        Me.lblAutoSalaryGenerate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblEmployeeOverTime
        '
        Me.lblEmployeeOverTime.BackColor = System.Drawing.Color.Transparent
        Me.lblEmployeeOverTime.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblEmployeeOverTime.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmployeeOverTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.lblEmployeeOverTime.Location = New System.Drawing.Point(14, 201)
        Me.lblEmployeeOverTime.Name = "lblEmployeeOverTime"
        Me.lblEmployeeOverTime.Size = New System.Drawing.Size(146, 34)
        Me.lblEmployeeOverTime.TabIndex = 0
        Me.lblEmployeeOverTime.Text = "Manual OverTime"
        Me.lblEmployeeOverTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAutoOverTime
        '
        Me.lblAutoOverTime.BackColor = System.Drawing.Color.Transparent
        Me.lblAutoOverTime.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblAutoOverTime.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAutoOverTime.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.lblAutoOverTime.Location = New System.Drawing.Point(14, 245)
        Me.lblAutoOverTime.Name = "lblAutoOverTime"
        Me.lblAutoOverTime.Size = New System.Drawing.Size(146, 34)
        Me.lblAutoOverTime.TabIndex = 0
        Me.lblAutoOverTime.Text = "Auto OverTime"
        Me.lblAutoOverTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblLoanDeduction
        '
        Me.lblLoanDeduction.BackColor = System.Drawing.Color.Transparent
        Me.lblLoanDeduction.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblLoanDeduction.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLoanDeduction.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.lblLoanDeduction.Location = New System.Drawing.Point(14, 158)
        Me.lblLoanDeduction.Name = "lblLoanDeduction"
        Me.lblLoanDeduction.Size = New System.Drawing.Size(146, 34)
        Me.lblLoanDeduction.TabIndex = 0
        Me.lblLoanDeduction.Text = "Loan Deduction"
        Me.lblLoanDeduction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAttendanceRegister
        '
        Me.lblAttendanceRegister.BackColor = System.Drawing.Color.Transparent
        Me.lblAttendanceRegister.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblAttendanceRegister.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAttendanceRegister.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.lblAttendanceRegister.Location = New System.Drawing.Point(12, 115)
        Me.lblAttendanceRegister.Name = "lblAttendanceRegister"
        Me.lblAttendanceRegister.Size = New System.Drawing.Size(146, 34)
        Me.lblAttendanceRegister.TabIndex = 1
        Me.lblAttendanceRegister.Text = "Attendance Register"
        Me.lblAttendanceRegister.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDailyAttendance
        '
        Me.lblDailyAttendance.BackColor = System.Drawing.Color.Transparent
        Me.lblDailyAttendance.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblDailyAttendance.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDailyAttendance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.lblDailyAttendance.Location = New System.Drawing.Point(12, 71)
        Me.lblDailyAttendance.Name = "lblDailyAttendance"
        Me.lblDailyAttendance.Size = New System.Drawing.Size(146, 34)
        Me.lblDailyAttendance.TabIndex = 1
        Me.lblDailyAttendance.Text = "Daily Attendance"
        Me.lblDailyAttendance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblAttendanceSetup
        '
        Me.lblAttendanceSetup.BackColor = System.Drawing.Color.Transparent
        Me.lblAttendanceSetup.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblAttendanceSetup.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAttendanceSetup.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.lblAttendanceSetup.Location = New System.Drawing.Point(12, 27)
        Me.lblAttendanceSetup.Name = "lblAttendanceSetup"
        Me.lblAttendanceSetup.Size = New System.Drawing.Size(146, 34)
        Me.lblAttendanceSetup.TabIndex = 0
        Me.lblAttendanceSetup.Text = "Attendance Setup"
        Me.lblAttendanceSetup.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(780, 52)
        Me.pnlHeader.TabIndex = 0
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Arial", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.lblHeader.Location = New System.Drawing.Point(31, 15)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(245, 22)
        Me.lblHeader.TabIndex = 2
        Me.lblHeader.Text = "Salary Generation Wizard"
        '
        'Panel4
        '
        Me.Panel4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel4.Controls.Add(Me.btnSalaryGeneration)
        Me.Panel4.Controls.Add(Me.pnlHeader)
        Me.Panel4.Controls.Add(Me.btnOverTime)
        Me.Panel4.Controls.Add(Me.btnLoanDeduction)
        Me.Panel4.Controls.Add(Me.btnAttendanceSetup)
        Me.Panel4.Location = New System.Drawing.Point(1, -1)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(780, 129)
        Me.Panel4.TabIndex = 5
        '
        'btnLoanDeduction
        '
        Me.btnLoanDeduction.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLoanDeduction.FlatAppearance.BorderSize = 0
        Me.btnLoanDeduction.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnLoanDeduction.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnLoanDeduction.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoanDeduction.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLoanDeduction.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnLoanDeduction.Image = CType(resources.GetObject("btnLoanDeduction.Image"), System.Drawing.Image)
        Me.btnLoanDeduction.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnLoanDeduction.Location = New System.Drawing.Point(163, 83)
        Me.btnLoanDeduction.Name = "btnLoanDeduction"
        Me.btnLoanDeduction.Size = New System.Drawing.Size(153, 43)
        Me.btnLoanDeduction.TabIndex = 5
        Me.btnLoanDeduction.Text = "Loan Deduction"
        Me.btnLoanDeduction.UseVisualStyleBackColor = True
        '
        'Panel5
        '
        Me.Panel5.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(181, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(249, Byte), Integer))
        Me.Panel5.Controls.Add(Me.Panel6)
        Me.Panel5.Controls.Add(Me.Panel3)
        Me.Panel5.Location = New System.Drawing.Point(1, 133)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(780, 353)
        Me.Panel5.TabIndex = 6
        '
        'Panel6
        '
        Me.Panel6.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel6.BackColor = System.Drawing.Color.Transparent
        Me.Panel6.Location = New System.Drawing.Point(160, 3)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(617, 307)
        Me.Panel6.TabIndex = 2
        '
        'frmSalaryGenerationWizard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(783, 486)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel5)
        Me.Controls.Add(Me.Panel4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSalaryGenerationWizard"
        Me.ShowIcon = False
        Me.Text = "Salary Generation Wizard"
        Me.TopMost = True
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel3.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnAttendanceSetup As System.Windows.Forms.Button
    Friend WithEvents btnOverTime As System.Windows.Forms.Button
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnDone As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSalaryGeneration As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents btnLoanDeduction As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents lblAttendanceRegister As System.Windows.Forms.Label
    Friend WithEvents lblAttendanceSetup As System.Windows.Forms.Label
    Friend WithEvents lblDailyAttendance As System.Windows.Forms.Label
    Friend WithEvents lblAutoOverTime As System.Windows.Forms.Label
    Friend WithEvents lblEmployeeOverTime As System.Windows.Forms.Label
    Friend WithEvents lblAutoSalaryGenerate As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents lblLoanDeduction As System.Windows.Forms.Label
End Class

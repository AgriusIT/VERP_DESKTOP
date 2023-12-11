<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class rptTodayTasks
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
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.lblPeriod = New System.Windows.Forms.Label()
        Me.ccFrom = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.ccTo = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.BtnGenerate = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbPeriod
        '
        Me.cmbPeriod.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPeriod.FormattingEnabled = True
        Me.cmbPeriod.Items.AddRange(New Object() {"Today", "Yesterday", "Current Week", "Current Month", "Current Year"})
        Me.cmbPeriod.Location = New System.Drawing.Point(87, 29)
        Me.cmbPeriod.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(406, 28)
        Me.cmbPeriod.TabIndex = 0
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.lblFrom)
        Me.GroupBox1.Controls.Add(Me.lblPeriod)
        Me.GroupBox1.Controls.Add(Me.ccFrom)
        Me.GroupBox1.Controls.Add(Me.ccTo)
        Me.GroupBox1.Controls.Add(Me.cmbPeriod)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 40)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(504, 168)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Date Range"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(39, 117)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 20)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "To"
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.Location = New System.Drawing.Point(24, 77)
        Me.lblFrom.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(46, 20)
        Me.lblFrom.TabIndex = 4
        Me.lblFrom.Text = "From"
        '
        'lblPeriod
        '
        Me.lblPeriod.AutoSize = True
        Me.lblPeriod.Location = New System.Drawing.Point(14, 34)
        Me.lblPeriod.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPeriod.Name = "lblPeriod"
        Me.lblPeriod.Size = New System.Drawing.Size(54, 20)
        Me.lblPeriod.TabIndex = 3
        Me.lblPeriod.Text = "Period"
        '
        'ccFrom
        '
        Me.ccFrom.CustomFormat = "dd/MMM/yyyy"
        Me.ccFrom.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.ccFrom.DropDownCalendar.Name = ""
        Me.ccFrom.Location = New System.Drawing.Point(87, 71)
        Me.ccFrom.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ccFrom.Name = "ccFrom"
        Me.ccFrom.Size = New System.Drawing.Size(408, 26)
        Me.ccFrom.TabIndex = 2
        '
        'ccTo
        '
        Me.ccTo.CustomFormat = "dd/MMM/yyyy"
        Me.ccTo.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.ccTo.DropDownCalendar.Name = ""
        Me.ccTo.Location = New System.Drawing.Point(87, 111)
        Me.ccTo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ccTo.Name = "ccTo"
        Me.ccTo.Size = New System.Drawing.Size(408, 26)
        Me.ccTo.TabIndex = 1
        '
        'BtnGenerate
        '
        Me.BtnGenerate.Location = New System.Drawing.Point(378, 217)
        Me.BtnGenerate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.BtnGenerate.Name = "BtnGenerate"
        Me.BtnGenerate.Size = New System.Drawing.Size(144, 40)
        Me.BtnGenerate.TabIndex = 6
        Me.BtnGenerate.Text = "Generate Report"
        Me.BtnGenerate.UseVisualStyleBackColor = True
        '
        'rptTodayTasks
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(540, 268)
        Me.Controls.Add(Me.BtnGenerate)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "rptTodayTasks"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Today Tasks"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ccFrom As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents ccTo As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents lblPeriod As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    Friend WithEvents BtnGenerate As System.Windows.Forms.Button
End Class

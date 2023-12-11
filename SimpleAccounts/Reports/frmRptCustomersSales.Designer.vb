<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptCustomersSales
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRptCustomersSales))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpToDate = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.dtpFromDate = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CmbAccounts = New System.Windows.Forms.ComboBox()
        Me.BtnGenerate = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtpToDate)
        Me.GroupBox1.Controls.Add(Me.dtpFromDate)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 37)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(532, 52)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Date Range"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(292, 22)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(31, 20)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "To:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 22)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 20)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "From"
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpToDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtpToDate.DropDownCalendar.Name = ""
        Me.dtpToDate.Location = New System.Drawing.Point(351, 15)
        Me.dtpToDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(172, 26)
        Me.dtpToDate.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.dtpToDate, "To Date")
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFromDate.DateFormat = Janus.Windows.CalendarCombo.DateFormat.Custom
        '
        '
        '
        Me.dtpFromDate.DropDownCalendar.Name = ""
        Me.dtpFromDate.Location = New System.Drawing.Point(116, 15)
        Me.dtpFromDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(172, 26)
        Me.dtpFromDate.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.dtpFromDate, "From Date")
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.CmbAccounts)
        Me.GroupBox2.Location = New System.Drawing.Point(18, 98)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Size = New System.Drawing.Size(532, 62)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Customer List"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 26)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Customer"
        '
        'CmbAccounts
        '
        Me.CmbAccounts.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.CmbAccounts.FormattingEnabled = True
        Me.CmbAccounts.Location = New System.Drawing.Point(116, 20)
        Me.CmbAccounts.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CmbAccounts.Name = "CmbAccounts"
        Me.CmbAccounts.Size = New System.Drawing.Size(406, 28)
        Me.CmbAccounts.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.CmbAccounts, "Select Any Customer")
        '
        'BtnGenerate
        '
        Me.BtnGenerate.Location = New System.Drawing.Point(406, 169)
        Me.BtnGenerate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.BtnGenerate.Name = "BtnGenerate"
        Me.BtnGenerate.Size = New System.Drawing.Size(144, 40)
        Me.BtnGenerate.TabIndex = 2
        Me.BtnGenerate.Text = "Generate Report"
        Me.ToolTip1.SetToolTip(Me.BtnGenerate, "Generate Report ")
        Me.BtnGenerate.UseVisualStyleBackColor = True
        '
        'frmRptCustomersSales
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(570, 225)
        Me.Controls.Add(Me.BtnGenerate)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmRptCustomersSales"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Customers Sales"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnGenerate As System.Windows.Forms.Button
    Friend WithEvents CmbAccounts As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpFromDate As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpToDate As Janus.Windows.CalendarCombo.CalendarCombo
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

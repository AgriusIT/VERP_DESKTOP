<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CntrPendingTasks
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.lblUserTasks = New System.Windows.Forms.Label()
        Me.llblTodayTasks = New System.Windows.Forms.LinkLabel()
        Me.llblPreviousTasks = New System.Windows.Forms.LinkLabel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.llblUpcomingTasks = New System.Windows.Forms.LinkLabel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblTotalTasks = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblNewTask = New System.Windows.Forms.LinkLabel()
        Me.SuspendLayout()
        '
        'lblUserTasks
        '
        Me.lblUserTasks.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblUserTasks.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblUserTasks.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUserTasks.ForeColor = System.Drawing.SystemColors.Window
        Me.lblUserTasks.Location = New System.Drawing.Point(0, 0)
        Me.lblUserTasks.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblUserTasks.Name = "lblUserTasks"
        Me.lblUserTasks.Size = New System.Drawing.Size(275, 33)
        Me.lblUserTasks.TabIndex = 13
        Me.lblUserTasks.Text = "My Work"
        Me.lblUserTasks.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'llblTodayTasks
        '
        Me.llblTodayTasks.AutoSize = True
        Me.llblTodayTasks.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.llblTodayTasks.Location = New System.Drawing.Point(4, 40)
        Me.llblTodayTasks.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.llblTodayTasks.Name = "llblTodayTasks"
        Me.llblTodayTasks.Size = New System.Drawing.Size(48, 16)
        Me.llblTodayTasks.TabIndex = 15
        Me.llblTodayTasks.TabStop = True
        Me.llblTodayTasks.Text = "Today"
        '
        'llblPreviousTasks
        '
        Me.llblPreviousTasks.AutoSize = True
        Me.llblPreviousTasks.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.llblPreviousTasks.Location = New System.Drawing.Point(4, 80)
        Me.llblPreviousTasks.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.llblPreviousTasks.Name = "llblPreviousTasks"
        Me.llblPreviousTasks.Size = New System.Drawing.Size(60, 16)
        Me.llblPreviousTasks.TabIndex = 16
        Me.llblPreviousTasks.TabStop = True
        Me.llblPreviousTasks.Text = "Overdue"
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(144, 40)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label2.Size = New System.Drawing.Size(112, 17)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "0"
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(144, 79)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label3.Size = New System.Drawing.Size(112, 19)
        Me.Label3.TabIndex = 19
        Me.Label3.Text = "0"
        '
        'llblUpcomingTasks
        '
        Me.llblUpcomingTasks.AutoSize = True
        Me.llblUpcomingTasks.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.llblUpcomingTasks.Location = New System.Drawing.Point(4, 105)
        Me.llblUpcomingTasks.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.llblUpcomingTasks.Name = "llblUpcomingTasks"
        Me.llblUpcomingTasks.Size = New System.Drawing.Size(70, 16)
        Me.llblUpcomingTasks.TabIndex = 20
        Me.llblUpcomingTasks.TabStop = True
        Me.llblUpcomingTasks.Text = "Upcoming"
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(144, 104)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label4.Size = New System.Drawing.Size(112, 19)
        Me.Label4.TabIndex = 21
        Me.Label4.Text = "0"
        '
        'lblTotalTasks
        '
        Me.lblTotalTasks.BackColor = System.Drawing.SystemColors.Window
        Me.lblTotalTasks.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalTasks.Location = New System.Drawing.Point(144, 151)
        Me.lblTotalTasks.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTotalTasks.Name = "lblTotalTasks"
        Me.lblTotalTasks.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblTotalTasks.Size = New System.Drawing.Size(112, 25)
        Me.lblTotalTasks.TabIndex = 24
        Me.lblTotalTasks.Text = "0"
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Window
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(4, 151)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(141, 25)
        Me.Label6.TabIndex = 23
        Me.Label6.Text = "Total"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Window
        Me.Label5.Location = New System.Drawing.Point(4, 131)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(267, 28)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "................................................................................." & _
    "......."
        '
        'lblNewTask
        '
        Me.lblNewTask.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblNewTask.AutoSize = True
        Me.lblNewTask.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblNewTask.Location = New System.Drawing.Point(217, 11)
        Me.lblNewTask.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblNewTask.Name = "lblNewTask"
        Me.lblNewTask.Size = New System.Drawing.Size(35, 16)
        Me.lblNewTask.TabIndex = 25
        Me.lblNewTask.TabStop = True
        Me.lblNewTask.Text = "New"
        '
        'CntrPendingTasks
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.lblNewTask)
        Me.Controls.Add(Me.lblTotalTasks)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.llblUpcomingTasks)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.llblPreviousTasks)
        Me.Controls.Add(Me.llblTodayTasks)
        Me.Controls.Add(Me.lblUserTasks)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "CntrPendingTasks"
        Me.Size = New System.Drawing.Size(275, 148)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblUserTasks As System.Windows.Forms.Label
    Friend WithEvents llblTodayTasks As System.Windows.Forms.LinkLabel
    Friend WithEvents llblPreviousTasks As System.Windows.Forms.LinkLabel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents llblUpcomingTasks As System.Windows.Forms.LinkLabel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblTotalTasks As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblNewTask As System.Windows.Forms.LinkLabel

End Class

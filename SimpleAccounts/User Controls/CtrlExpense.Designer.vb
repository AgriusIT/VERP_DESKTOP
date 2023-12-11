<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrlExpense
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.lblExpense = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblExpenseAmt = New System.Windows.Forms.Label
        Me.bkgExpense = New System.ComponentModel.BackgroundWorker
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.SuspendLayout()
        '
        'lblExpense
        '
        Me.lblExpense.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblExpense.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblExpense.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblExpense.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExpense.ForeColor = System.Drawing.SystemColors.Window
        Me.lblExpense.Location = New System.Drawing.Point(0, 0)
        Me.lblExpense.Name = "lblExpense"
        Me.lblExpense.Size = New System.Drawing.Size(275, 26)
        Me.lblExpense.TabIndex = 0
        Me.lblExpense.Text = "Expense"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Window
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(275, 80)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Label2"
        '
        'lblExpenseAmt
        '
        Me.lblExpenseAmt.BackColor = System.Drawing.SystemColors.Window
        Me.lblExpenseAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExpenseAmt.Location = New System.Drawing.Point(93, 39)
        Me.lblExpenseAmt.Name = "lblExpenseAmt"
        Me.lblExpenseAmt.Size = New System.Drawing.Size(167, 29)
        Me.lblExpenseAmt.TabIndex = 3
        Me.lblExpenseAmt.Text = "0"
        Me.lblExpenseAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'bkgExpense
        '
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.BackColor = System.Drawing.SystemColors.Window
        Me.LinkLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.Location = New System.Drawing.Point(12, 47)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(95, 16)
        Me.LinkLabel1.TabIndex = 5
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Total Expense"
        '
        'CtrlExpense
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.lblExpenseAmt)
        Me.Controls.Add(Me.lblExpense)
        Me.Controls.Add(Me.Label2)
        Me.Name = "CtrlExpense"
        Me.Size = New System.Drawing.Size(275, 80)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblExpense As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblExpenseAmt As System.Windows.Forms.Label
    Friend WithEvents bkgExpense As System.ComponentModel.BackgroundWorker
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel

End Class

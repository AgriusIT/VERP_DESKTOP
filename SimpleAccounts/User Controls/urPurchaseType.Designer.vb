<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class urPurchaseType
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel
        Me.lblCashSales = New System.Windows.Forms.Label
        Me.lblCreditSales = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker
        Me.BackgroundWorker2 = New System.ComponentModel.BackgroundWorker
        Me.lblTotal = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.Window
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(275, 26)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Cash/Credit Purchase"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.Location = New System.Drawing.Point(8, 42)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LinkLabel1.Size = New System.Drawing.Size(79, 13)
        Me.LinkLabel1.TabIndex = 1
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Cash Purchase"
        '
        'LinkLabel2
        '
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel2.Location = New System.Drawing.Point(8, 75)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LinkLabel2.Size = New System.Drawing.Size(82, 13)
        Me.LinkLabel2.TabIndex = 2
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "Credit Purchase"
        '
        'lblCashSales
        '
        Me.lblCashSales.Location = New System.Drawing.Point(94, 41)
        Me.lblCashSales.Name = "lblCashSales"
        Me.lblCashSales.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblCashSales.Size = New System.Drawing.Size(161, 22)
        Me.lblCashSales.TabIndex = 3
        Me.lblCashSales.Text = "0"
        '
        'lblCreditSales
        '
        Me.lblCreditSales.Location = New System.Drawing.Point(94, 71)
        Me.lblCreditSales.Name = "lblCreditSales"
        Me.lblCreditSales.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblCreditSales.Size = New System.Drawing.Size(161, 22)
        Me.lblCreditSales.TabIndex = 4
        Me.lblCreditSales.Text = "0"
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Window
        Me.Label5.Location = New System.Drawing.Point(8, 89)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(244, 29)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "................................................................................." & _
            "...."
        '
        'BackgroundWorker1
        '
        '
        'BackgroundWorker2
        '
        '
        'lblTotal
        '
        Me.lblTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotal.Location = New System.Drawing.Point(94, 105)
        Me.lblTotal.Name = "lblTotal"
        Me.lblTotal.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.lblTotal.Size = New System.Drawing.Size(161, 22)
        Me.lblTotal.TabIndex = 6
        Me.lblTotal.Text = "0"
        '
        'Label2
        '
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 105)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.Label2.Size = New System.Drawing.Size(61, 22)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Total"
        '
        'urPurchaseType
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblTotal)
        Me.Controls.Add(Me.lblCreditSales)
        Me.Controls.Add(Me.lblCashSales)
        Me.Controls.Add(Me.LinkLabel2)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label5)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "urPurchaseType"
        Me.Size = New System.Drawing.Size(275, 131)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Friend WithEvents lblCashSales As System.Windows.Forms.Label
    Friend WithEvents lblCreditSales As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundWorker2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblTotal As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label

End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrlPurchase
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
        Me.lblPurchase = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.bkgPurchase = New System.ComponentModel.BackgroundWorker
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblPurchaseAmt = New System.Windows.Forms.Label
        Me.lblPurchaseReturnAmt = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.lblTotalPurchaseAmt = New System.Windows.Forms.Label
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel
        Me.SuspendLayout()
        '
        'lblPurchase
        '
        Me.lblPurchase.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblPurchase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblPurchase.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblPurchase.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPurchase.ForeColor = System.Drawing.SystemColors.Window
        Me.lblPurchase.Location = New System.Drawing.Point(0, 0)
        Me.lblPurchase.Name = "lblPurchase"
        Me.lblPurchase.Size = New System.Drawing.Size(275, 26)
        Me.lblPurchase.TabIndex = 2
        Me.lblPurchase.Text = "Purchase"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.SystemColors.Window
        Me.Label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Location = New System.Drawing.Point(0, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(275, 131)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Label2"
        '
        'bkgPurchase
        '
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.SystemColors.Window
        Me.Label5.Location = New System.Drawing.Point(14, 85)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(239, 23)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "................................................................................"
        '
        'lblPurchaseAmt
        '
        Me.lblPurchaseAmt.BackColor = System.Drawing.SystemColors.Window
        Me.lblPurchaseAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPurchaseAmt.Location = New System.Drawing.Point(124, 42)
        Me.lblPurchaseAmt.Name = "lblPurchaseAmt"
        Me.lblPurchaseAmt.Size = New System.Drawing.Size(129, 23)
        Me.lblPurchaseAmt.TabIndex = 2
        Me.lblPurchaseAmt.Text = "0"
        Me.lblPurchaseAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPurchaseReturnAmt
        '
        Me.lblPurchaseReturnAmt.BackColor = System.Drawing.SystemColors.Window
        Me.lblPurchaseReturnAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPurchaseReturnAmt.Location = New System.Drawing.Point(124, 70)
        Me.lblPurchaseReturnAmt.Name = "lblPurchaseReturnAmt"
        Me.lblPurchaseReturnAmt.Size = New System.Drawing.Size(129, 23)
        Me.lblPurchaseReturnAmt.TabIndex = 3
        Me.lblPurchaseReturnAmt.Text = "0"
        Me.lblPurchaseReturnAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Window
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(14, 98)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(107, 20)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Total"
        '
        'lblTotalPurchaseAmt
        '
        Me.lblTotalPurchaseAmt.BackColor = System.Drawing.SystemColors.Window
        Me.lblTotalPurchaseAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalPurchaseAmt.Location = New System.Drawing.Point(127, 98)
        Me.lblTotalPurchaseAmt.Name = "lblTotalPurchaseAmt"
        Me.lblTotalPurchaseAmt.Size = New System.Drawing.Size(126, 20)
        Me.lblTotalPurchaseAmt.TabIndex = 6
        Me.lblTotalPurchaseAmt.Text = "0"
        Me.lblTotalPurchaseAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.BackColor = System.Drawing.SystemColors.Window
        Me.LinkLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.Location = New System.Drawing.Point(15, 45)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(65, 16)
        Me.LinkLabel1.TabIndex = 7
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Purchase"
        '
        'LinkLabel2
        '
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.BackColor = System.Drawing.SystemColors.Window
        Me.LinkLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel2.Location = New System.Drawing.Point(15, 73)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(107, 16)
        Me.LinkLabel2.TabIndex = 8
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "Purchase Return"
        '
        'CtrlPurchase
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.Controls.Add(Me.LinkLabel2)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.lblTotalPurchaseAmt)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.lblPurchase)
        Me.Controls.Add(Me.lblPurchaseReturnAmt)
        Me.Controls.Add(Me.lblPurchaseAmt)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.Name = "CtrlPurchase"
        Me.Size = New System.Drawing.Size(275, 131)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblPurchase As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblPurchaseAmt As System.Windows.Forms.Label
    Friend WithEvents lblPurchaseReturnAmt As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblTotalPurchaseAmt As System.Windows.Forms.Label
    Friend WithEvents bkgPurchase As System.ComponentModel.BackgroundWorker
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel

End Class

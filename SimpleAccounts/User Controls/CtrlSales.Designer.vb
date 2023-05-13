<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrlSales
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
        Me.lblSale = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.bkgSales = New System.ComponentModel.BackgroundWorker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblSalesAmt = New System.Windows.Forms.Label()
        Me.lblSalesReturnAmt = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblTotalSalesAmt = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.SuspendLayout()
        '
        'lblSale
        '
        Me.lblSale.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblSale.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSale.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblSale.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSale.ForeColor = System.Drawing.SystemColors.Window
        Me.lblSale.Location = New System.Drawing.Point(0, 0)
        Me.lblSale.Name = "lblSale"
        Me.lblSale.Size = New System.Drawing.Size(275, 26)
        Me.lblSale.TabIndex = 2
        Me.lblSale.Text = "Sales"
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
        'bkgSales
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
        'lblSalesAmt
        '
        Me.lblSalesAmt.BackColor = System.Drawing.SystemColors.Window
        Me.lblSalesAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesAmt.Location = New System.Drawing.Point(124, 42)
        Me.lblSalesAmt.Name = "lblSalesAmt"
        Me.lblSalesAmt.Size = New System.Drawing.Size(129, 23)
        Me.lblSalesAmt.TabIndex = 2
        Me.lblSalesAmt.Text = "0"
        Me.lblSalesAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSalesReturnAmt
        '
        Me.lblSalesReturnAmt.BackColor = System.Drawing.SystemColors.Window
        Me.lblSalesReturnAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSalesReturnAmt.Location = New System.Drawing.Point(124, 70)
        Me.lblSalesReturnAmt.Name = "lblSalesReturnAmt"
        Me.lblSalesReturnAmt.Size = New System.Drawing.Size(129, 23)
        Me.lblSalesReturnAmt.TabIndex = 3
        Me.lblSalesReturnAmt.Text = "0"
        Me.lblSalesReturnAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.SystemColors.Window
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(14, 98)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 20)
        Me.Label6.TabIndex = 5
        Me.Label6.Text = "Total"
        '
        'lblTotalSalesAmt
        '
        Me.lblTotalSalesAmt.BackColor = System.Drawing.SystemColors.Window
        Me.lblTotalSalesAmt.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalSalesAmt.Location = New System.Drawing.Point(127, 98)
        Me.lblTotalSalesAmt.Name = "lblTotalSalesAmt"
        Me.lblTotalSalesAmt.Size = New System.Drawing.Size(126, 20)
        Me.lblTotalSalesAmt.TabIndex = 6
        Me.lblTotalSalesAmt.Text = "0"
        Me.lblTotalSalesAmt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.BackColor = System.Drawing.SystemColors.Window
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.Transparent
        Me.LinkLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.Location = New System.Drawing.Point(14, 45)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(43, 16)
        Me.LinkLabel1.TabIndex = 7
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Sales"
        '
        'LinkLabel2
        '
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.BackColor = System.Drawing.SystemColors.Window
        Me.LinkLabel2.DisabledLinkColor = System.Drawing.Color.Transparent
        Me.LinkLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel2.Location = New System.Drawing.Point(14, 73)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(85, 16)
        Me.LinkLabel2.TabIndex = 8
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "Sales Return"
        '
        'CtrlSales
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.Controls.Add(Me.LinkLabel2)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.lblTotalSalesAmt)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.lblSale)
        Me.Controls.Add(Me.lblSalesReturnAmt)
        Me.Controls.Add(Me.lblSalesAmt)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.Name = "CtrlSales"
        Me.Size = New System.Drawing.Size(275, 131)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblSale As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents bkgSales As System.ComponentModel.BackgroundWorker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblSalesAmt As System.Windows.Forms.Label
    Friend WithEvents lblSalesReturnAmt As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblTotalSalesAmt As System.Windows.Forms.Label
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel

End Class

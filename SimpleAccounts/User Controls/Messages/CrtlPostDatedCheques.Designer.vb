<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CrtlPostDatedCheques
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
        Me.lblAccount = New System.Windows.Forms.Label
        Me.bgwUpdates = New System.ComponentModel.BackgroundWorker
        Me.lblAmount = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker
        Me.BackgroundWorker2 = New System.ComponentModel.BackgroundWorker
        Me.SuspendLayout()
        '
        'lblAccount
        '
        Me.lblAccount.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.lblAccount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAccount.Dock = System.Windows.Forms.DockStyle.Top
        Me.lblAccount.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAccount.ForeColor = System.Drawing.SystemColors.Window
        Me.lblAccount.Location = New System.Drawing.Point(0, 0)
        Me.lblAccount.Name = "lblAccount"
        Me.lblAccount.Size = New System.Drawing.Size(275, 27)
        Me.lblAccount.TabIndex = 12
        Me.lblAccount.Text = "Post Dated Cheques"
        Me.lblAccount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'bgwUpdates
        '
        '
        'lblAmount
        '
        Me.lblAmount.BackColor = System.Drawing.SystemColors.Window
        Me.lblAmount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblAmount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblAmount.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAmount.ForeColor = System.Drawing.Color.Navy
        Me.lblAmount.Location = New System.Drawing.Point(0, 0)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(275, 86)
        Me.lblAmount.TabIndex = 13
        Me.lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.SystemColors.Window
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(240, 38)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(15, 16)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "0"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.SystemColors.Window
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(240, 60)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(15, 16)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "0"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.BackColor = System.Drawing.SystemColors.Window
        Me.LinkLabel1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.Location = New System.Drawing.Point(12, 38)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(105, 16)
        Me.LinkLabel1.TabIndex = 18
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Today Cheques"
        '
        'LinkLabel2
        '
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.BackColor = System.Drawing.SystemColors.Window
        Me.LinkLabel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel2.Location = New System.Drawing.Point(12, 60)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(126, 16)
        Me.LinkLabel2.TabIndex = 19
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "Tomorrow Cheques"
        '
        'BackgroundWorker1
        '
        '
        'BackgroundWorker2
        '
        '
        'CrtlPostDatedCheques
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.Transparent
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.LinkLabel2)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblAccount)
        Me.Controls.Add(Me.lblAmount)
        Me.Name = "CrtlPostDatedCheques"
        Me.Size = New System.Drawing.Size(275, 86)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblAccount As System.Windows.Forms.Label
    Friend WithEvents bgwUpdates As System.ComponentModel.BackgroundWorker
    Friend WithEvents lblAmount As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents BackgroundWorker2 As System.ComponentModel.BackgroundWorker

End Class

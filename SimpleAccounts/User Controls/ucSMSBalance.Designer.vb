<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ucSMSBalance
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblSMSBalance = New System.Windows.Forms.Label
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.SystemColors.ActiveCaption
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Label1.Font = New System.Drawing.Font("Verdana", 14.25!)
        Me.Label1.ForeColor = System.Drawing.SystemColors.Window
        Me.Label1.Location = New System.Drawing.Point(0, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(273, 26)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "SMS Balance"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(8, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 16)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Balance:"
        '
        'lblSMSBalance
        '
        Me.lblSMSBalance.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSMSBalance.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblSMSBalance.Location = New System.Drawing.Point(79, 44)
        Me.lblSMSBalance.Name = "lblSMSBalance"
        Me.lblSMSBalance.Size = New System.Drawing.Size(180, 16)
        Me.lblSMSBalance.TabIndex = 2
        '
        'BackgroundWorker1
        '
        '
        'ucSMSBalance
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.lblSMSBalance)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "ucSMSBalance"
        Me.Size = New System.Drawing.Size(273, 67)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblSMSBalance As System.Windows.Forms.Label
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker

End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTrialVersion
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
        Me.MaskedTextBox1 = New System.Windows.Forms.MaskedTextBox()
        Me.lblContinue = New System.Windows.Forms.LinkLabel()
        Me.lblBuy = New System.Windows.Forms.LinkLabel()
        Me.lblEnterKey = New System.Windows.Forms.LinkLabel()
        Me.lblClose = New System.Windows.Forms.LinkLabel()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'MaskedTextBox1
        '
        Me.MaskedTextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MaskedTextBox1.Location = New System.Drawing.Point(189, 320)
        Me.MaskedTextBox1.Name = "MaskedTextBox1"
        Me.MaskedTextBox1.Size = New System.Drawing.Size(237, 30)
        Me.MaskedTextBox1.TabIndex = 0
        Me.MaskedTextBox1.Visible = False
        '
        'lblContinue
        '
        Me.lblContinue.BackColor = System.Drawing.Color.Transparent
        Me.lblContinue.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblContinue.Location = New System.Drawing.Point(432, 324)
        Me.lblContinue.Name = "lblContinue"
        Me.lblContinue.Size = New System.Drawing.Size(88, 21)
        Me.lblContinue.TabIndex = 1
        '
        'lblBuy
        '
        Me.lblBuy.BackColor = System.Drawing.Color.Transparent
        Me.lblBuy.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblBuy.Location = New System.Drawing.Point(322, 324)
        Me.lblBuy.Name = "lblBuy"
        Me.lblBuy.Size = New System.Drawing.Size(87, 21)
        Me.lblBuy.TabIndex = 2
        '
        'lblEnterKey
        '
        Me.lblEnterKey.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblEnterKey.BackColor = System.Drawing.Color.Transparent
        Me.lblEnterKey.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblEnterKey.Location = New System.Drawing.Point(201, 325)
        Me.lblEnterKey.Name = "lblEnterKey"
        Me.lblEnterKey.Size = New System.Drawing.Size(91, 21)
        Me.lblEnterKey.TabIndex = 3
        '
        'lblClose
        '
        Me.lblClose.BackColor = System.Drawing.Color.Transparent
        Me.lblClose.Location = New System.Drawing.Point(686, 30)
        Me.lblClose.Name = "lblClose"
        Me.lblClose.Size = New System.Drawing.Size(24, 18)
        Me.lblClose.TabIndex = 4
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(231, 181)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 25
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'frmTrialVersion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SimpleAccounts.My.Resources.Resources._2
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ClientSize = New System.Drawing.Size(729, 413)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.lblClose)
        Me.Controls.Add(Me.lblEnterKey)
        Me.Controls.Add(Me.lblBuy)
        Me.Controls.Add(Me.lblContinue)
        Me.Controls.Add(Me.MaskedTextBox1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(466, 181)
        Me.Name = "frmTrialVersion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Trial Version"
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MaskedTextBox1 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents lblContinue As System.Windows.Forms.LinkLabel
    Friend WithEvents lblBuy As System.Windows.Forms.LinkLabel
    Friend WithEvents lblEnterKey As System.Windows.Forms.LinkLabel
    Friend WithEvents lblClose As System.Windows.Forms.LinkLabel
    Friend WithEvents lblProgress As System.Windows.Forms.Label
End Class

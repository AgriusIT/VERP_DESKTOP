<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRateConversion
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRateConversion))
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblConversionTitle = New System.Windows.Forms.Label()
        Me.txtRate = New System.Windows.Forms.TextBox()
        Me.lblConvertedRate = New System.Windows.Forms.Label()
        Me.lblHelp = New System.Windows.Forms.Label()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnClose)
        Me.pnlHeader.Controls.Add(Me.lblConversionTitle)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(260, 44)
        Me.pnlHeader.TabIndex = 1
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.btnClose.Location = New System.Drawing.Point(216, 10)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(32, 24)
        Me.btnClose.TabIndex = 1
        Me.btnClose.TabStop = False
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'lblConversionTitle
        '
        Me.lblConversionTitle.AutoSize = True
        Me.lblConversionTitle.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConversionTitle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.lblConversionTitle.Location = New System.Drawing.Point(12, 13)
        Me.lblConversionTitle.Name = "lblConversionTitle"
        Me.lblConversionTitle.Size = New System.Drawing.Size(0, 19)
        Me.lblConversionTitle.TabIndex = 0
        '
        'txtRate
        '
        Me.txtRate.Font = New System.Drawing.Font("Calibri", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtRate.Location = New System.Drawing.Point(76, 69)
        Me.txtRate.Name = "txtRate"
        Me.txtRate.Size = New System.Drawing.Size(100, 22)
        Me.txtRate.TabIndex = 2
        '
        'lblConvertedRate
        '
        Me.lblConvertedRate.AutoSize = True
        Me.lblConvertedRate.Font = New System.Drawing.Font("Calibri", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblConvertedRate.Location = New System.Drawing.Point(76, 108)
        Me.lblConvertedRate.Name = "lblConvertedRate"
        Me.lblConvertedRate.Size = New System.Drawing.Size(108, 23)
        Me.lblConvertedRate.TabIndex = 3
        Me.lblConvertedRate.Text = "Dummy Text"
        Me.lblConvertedRate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblHelp
        '
        Me.lblHelp.AutoSize = True
        Me.lblHelp.Location = New System.Drawing.Point(67, 147)
        Me.lblHelp.Name = "lblHelp"
        Me.lblHelp.Size = New System.Drawing.Size(123, 13)
        Me.lblHelp.TabIndex = 4
        Me.lblHelp.Text = "Press Enter to save rate."
        '
        'frmRateConversion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(260, 179)
        Me.Controls.Add(Me.lblHelp)
        Me.Controls.Add(Me.lblConvertedRate)
        Me.Controls.Add(Me.txtRate)
        Me.Controls.Add(Me.pnlHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmRateConversion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmRateConversion"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblConversionTitle As System.Windows.Forms.Label
    Friend WithEvents txtRate As System.Windows.Forms.TextBox
    Friend WithEvents lblConvertedRate As System.Windows.Forms.Label
    Friend WithEvents lblHelp As System.Windows.Forms.Label
End Class

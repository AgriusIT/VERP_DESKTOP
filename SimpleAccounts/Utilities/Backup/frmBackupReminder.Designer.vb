<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBackupReminder
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBackupReminder))
        Me.pnlReminder2 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnBackup = New System.Windows.Forms.Button()
        Me.btnSkip = New System.Windows.Forms.Button()
        Me.lblMonthYear = New System.Windows.Forms.Label()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.lblDay = New System.Windows.Forms.Label()
        Me.pnlReminder1 = New System.Windows.Forms.Panel()
        Me.pnlCloseForm = New System.Windows.Forms.Panel()
        Me.pnlReminder2.SuspendLayout()
        Me.pnlReminder1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlReminder2
        '
        Me.pnlReminder2.BackColor = System.Drawing.Color.White
        Me.pnlReminder2.BackgroundImage = CType(resources.GetObject("pnlReminder2.BackgroundImage"), System.Drawing.Image)
        Me.pnlReminder2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.pnlReminder2.Controls.Add(Me.Label1)
        Me.pnlReminder2.Controls.Add(Me.btnBackup)
        Me.pnlReminder2.Controls.Add(Me.btnSkip)
        Me.pnlReminder2.Controls.Add(Me.lblMonthYear)
        Me.pnlReminder2.Controls.Add(Me.lblDate)
        Me.pnlReminder2.Controls.Add(Me.lblDay)
        Me.pnlReminder2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlReminder2.Location = New System.Drawing.Point(0, 0)
        Me.pnlReminder2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnlReminder2.Name = "pnlReminder2"
        Me.pnlReminder2.Size = New System.Drawing.Size(848, 522)
        Me.pnlReminder2.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(211, 263)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(142, 50)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Need Backup"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnBackup
        '
        Me.btnBackup.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnBackup.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.btnBackup.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnBackup.FlatAppearance.BorderSize = 0
        Me.btnBackup.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnBackup.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.btnBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBackup.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBackup.ForeColor = System.Drawing.Color.White
        Me.btnBackup.Location = New System.Drawing.Point(216, 405)
        Me.btnBackup.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnBackup.Name = "btnBackup"
        Me.btnBackup.Size = New System.Drawing.Size(137, 32)
        Me.btnBackup.TabIndex = 4
        Me.btnBackup.Text = "Backup Now"
        Me.btnBackup.UseVisualStyleBackColor = False
        '
        'btnSkip
        '
        Me.btnSkip.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnSkip.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.btnSkip.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSkip.FlatAppearance.BorderSize = 0
        Me.btnSkip.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent
        Me.btnSkip.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.btnSkip.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSkip.Font = New System.Drawing.Font("Verdana", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSkip.ForeColor = System.Drawing.Color.White
        Me.btnSkip.Location = New System.Drawing.Point(47, 405)
        Me.btnSkip.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnSkip.Name = "btnSkip"
        Me.btnSkip.Size = New System.Drawing.Size(137, 32)
        Me.btnSkip.TabIndex = 3
        Me.btnSkip.Text = "Skip"
        Me.btnSkip.UseVisualStyleBackColor = False
        '
        'lblMonthYear
        '
        Me.lblMonthYear.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblMonthYear.BackColor = System.Drawing.Color.Transparent
        Me.lblMonthYear.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMonthYear.ForeColor = System.Drawing.Color.Black
        Me.lblMonthYear.Location = New System.Drawing.Point(44, 338)
        Me.lblMonthYear.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMonthYear.Name = "lblMonthYear"
        Me.lblMonthYear.Size = New System.Drawing.Size(140, 28)
        Me.lblMonthYear.TabIndex = 2
        Me.lblMonthYear.Text = "Label1"
        Me.lblMonthYear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDate
        '
        Me.lblDate.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblDate.BackColor = System.Drawing.Color.Transparent
        Me.lblDate.Font = New System.Drawing.Font("Verdana", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.ForeColor = System.Drawing.Color.Black
        Me.lblDate.Location = New System.Drawing.Point(37, 263)
        Me.lblDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(147, 50)
        Me.lblDate.TabIndex = 1
        Me.lblDate.Text = "Label1"
        Me.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblDay
        '
        Me.lblDay.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblDay.BackColor = System.Drawing.Color.Transparent
        Me.lblDay.Font = New System.Drawing.Font("Arial", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDay.ForeColor = System.Drawing.Color.Black
        Me.lblDay.Location = New System.Drawing.Point(44, 209)
        Me.lblDay.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDay.Name = "lblDay"
        Me.lblDay.Size = New System.Drawing.Size(140, 28)
        Me.lblDay.TabIndex = 0
        Me.lblDay.Text = "Label1"
        Me.lblDay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlReminder1
        '
        Me.pnlReminder1.BackColor = System.Drawing.Color.White
        Me.pnlReminder1.BackgroundImage = CType(resources.GetObject("pnlReminder1.BackgroundImage"), System.Drawing.Image)
        Me.pnlReminder1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.pnlReminder1.Controls.Add(Me.pnlCloseForm)
        Me.pnlReminder1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlReminder1.Location = New System.Drawing.Point(0, 0)
        Me.pnlReminder1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnlReminder1.Name = "pnlReminder1"
        Me.pnlReminder1.Size = New System.Drawing.Size(848, 522)
        Me.pnlReminder1.TabIndex = 0
        '
        'pnlCloseForm
        '
        Me.pnlCloseForm.BackColor = System.Drawing.Color.Transparent
        Me.pnlCloseForm.Cursor = System.Windows.Forms.Cursors.Hand
        Me.pnlCloseForm.Location = New System.Drawing.Point(997, 7)
        Me.pnlCloseForm.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnlCloseForm.Name = "pnlCloseForm"
        Me.pnlCloseForm.Size = New System.Drawing.Size(37, 34)
        Me.pnlCloseForm.TabIndex = 0
        '
        'frmBackupReminder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(848, 522)
        Me.Controls.Add(Me.pnlReminder2)
        Me.Controls.Add(Me.pnlReminder1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmBackupReminder"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Backup Reminder"
        Me.pnlReminder2.ResumeLayout(False)
        Me.pnlReminder1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlReminder2 As System.Windows.Forms.Panel
    Friend WithEvents pnlReminder1 As System.Windows.Forms.Panel
    Friend WithEvents pnlCloseForm As System.Windows.Forms.Panel
    Friend WithEvents btnBackup As System.Windows.Forms.Button
    Friend WithEvents btnSkip As System.Windows.Forms.Button
    Friend WithEvents lblMonthYear As System.Windows.Forms.Label
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents lblDay As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class

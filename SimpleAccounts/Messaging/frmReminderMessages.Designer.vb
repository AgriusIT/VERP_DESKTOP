<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReminderMessages
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReminderMessages))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.grdMsgDetail = New Janus.Windows.GridEX.GridEX()
        Me.btnSnooze = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnDismissAll = New System.Windows.Forms.Button()
        Me.btnDismiss = New System.Windows.Forms.Button()
        Me.cmbSnooze = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdMsgDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.SimpleAccounts.My.Resources.Resources.bell_icon
        Me.PictureBox1.Location = New System.Drawing.Point(5, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(20, 20)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'grdMsgDetail
        '
        Me.grdMsgDetail.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdMsgDetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdMsgDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdMsgDetail.GroupByBoxVisible = False
        Me.grdMsgDetail.Location = New System.Drawing.Point(1, 104)
        Me.grdMsgDetail.Name = "grdMsgDetail"
        Me.grdMsgDetail.Size = New System.Drawing.Size(410, 146)
        Me.grdMsgDetail.TabIndex = 1
        Me.grdMsgDetail.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'btnSnooze
        '
        Me.btnSnooze.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSnooze.Location = New System.Drawing.Point(326, 316)
        Me.btnSnooze.Name = "btnSnooze"
        Me.btnSnooze.Size = New System.Drawing.Size(85, 23)
        Me.btnSnooze.TabIndex = 6
        Me.btnSnooze.Text = "Snooze"
        Me.btnSnooze.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Label1.Location = New System.Drawing.Point(31, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(369, 83)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Label1"
        '
        'btnDismissAll
        '
        Me.btnDismissAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDismissAll.Location = New System.Drawing.Point(1, 256)
        Me.btnDismissAll.Name = "btnDismissAll"
        Me.btnDismissAll.Size = New System.Drawing.Size(99, 23)
        Me.btnDismissAll.TabIndex = 2
        Me.btnDismissAll.Text = "Dismiss All"
        Me.btnDismissAll.UseVisualStyleBackColor = True
        '
        'btnDismiss
        '
        Me.btnDismiss.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDismiss.Location = New System.Drawing.Point(106, 256)
        Me.btnDismiss.Name = "btnDismiss"
        Me.btnDismiss.Size = New System.Drawing.Size(75, 23)
        Me.btnDismiss.TabIndex = 3
        Me.btnDismiss.Text = "Dismiss"
        Me.btnDismiss.UseVisualStyleBackColor = True
        '
        'cmbSnooze
        '
        Me.cmbSnooze.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbSnooze.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSnooze.FormattingEnabled = True
        Me.cmbSnooze.Items.AddRange(New Object() {"5 Minutes", "10 Minutes", "15 Minutes", "30 Minutes", "1 Hour", "2 Hours", "4 Hours", "8 Hours", "1 Day", "2 Days", "3 Days", "4 Days", "1 Week", "2 Weeks"})
        Me.cmbSnooze.Location = New System.Drawing.Point(1, 316)
        Me.cmbSnooze.Name = "cmbSnooze"
        Me.cmbSnooze.Size = New System.Drawing.Size(319, 28)
        Me.cmbSnooze.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(-2, 300)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(339, 20)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Click Snooze to be reminded again in:"
        '
        'frmReminderMessages
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(412, 342)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbSnooze)
        Me.Controls.Add(Me.btnDismiss)
        Me.Controls.Add(Me.btnDismissAll)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnSnooze)
        Me.Controls.Add(Me.grdMsgDetail)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmReminderMessages"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reminder"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdMsgDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents grdMsgDetail As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnSnooze As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnDismissAll As System.Windows.Forms.Button
    Friend WithEvents btnDismiss As System.Windows.Forms.Button
    Friend WithEvents cmbSnooze As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class

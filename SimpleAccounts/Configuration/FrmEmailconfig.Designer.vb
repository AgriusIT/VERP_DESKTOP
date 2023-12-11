<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmEmailconfig
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmEmailconfig))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.lblProcess = New System.Windows.Forms.Label()
        Me.btnTestingEmail = New System.Windows.Forms.Button()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnhelp = New System.Windows.Forms.ToolStripButton()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbport = New System.Windows.Forms.ComboBox()
        Me.chckactive = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CmbSmtp = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtpassword = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtemail = New System.Windows.Forms.TextBox()
        Me.CmbMail = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtDisplayName = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GridEX1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.SplitContainer1.Panel2.Controls.Add(Me.pnlHeader)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblProgress)
        Me.SplitContainer1.Panel2.Controls.Add(Me.PictureBox1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblMessage)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblProcess)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnTestingEmail)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ToolStrip1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label7)
        Me.SplitContainer1.Panel2.Controls.Add(Me.cmbport)
        Me.SplitContainer1.Panel2.Controls.Add(Me.chckactive)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label6)
        Me.SplitContainer1.Panel2.Controls.Add(Me.CmbSmtp)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label5)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label4)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label3)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtpassword)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtemail)
        Me.SplitContainer1.Panel2.Controls.Add(Me.CmbMail)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtDisplayName)
        Me.SplitContainer1.Size = New System.Drawing.Size(1120, 694)
        Me.SplitContainer1.SplitterDistance = 409
        Me.SplitContainer1.SplitterWidth = 6
        Me.SplitContainer1.TabIndex = 0
        '
        'GridEX1
        '
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.Location = New System.Drawing.Point(0, 0)
        Me.GridEX1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.Size = New System.Drawing.Size(409, 694)
        Me.GridEX1.TabIndex = 0
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'pnlHeader
        '
        Me.pnlHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Location = New System.Drawing.Point(4, 43)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(696, 51)
        Me.pnlHeader.TabIndex = 31
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(15, 6)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(217, 36)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "E-Mail Setting"
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(93, 266)
        Me.lblProgress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(394, 69)
        Me.lblProgress.TabIndex = 30
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(279, 400)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(78, 38)
        Me.PictureBox1.TabIndex = 21
        Me.PictureBox1.TabStop = False
        '
        'lblMessage
        '
        Me.lblMessage.AutoSize = True
        Me.lblMessage.Location = New System.Drawing.Point(128, 418)
        Me.lblMessage.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(74, 20)
        Me.lblMessage.TabIndex = 17
        Me.lblMessage.Text = "Message"
        '
        'lblProcess
        '
        Me.lblProcess.AutoSize = True
        Me.lblProcess.Location = New System.Drawing.Point(128, 389)
        Me.lblProcess.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProcess.Name = "lblProcess"
        Me.lblProcess.Size = New System.Drawing.Size(66, 20)
        Me.lblProcess.TabIndex = 16
        Me.lblProcess.Text = "Process"
        '
        'btnTestingEmail
        '
        Me.btnTestingEmail.Location = New System.Drawing.Point(132, 349)
        Me.btnTestingEmail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnTestingEmail.Name = "btnTestingEmail"
        Me.btnTestingEmail.Size = New System.Drawing.Size(186, 35)
        Me.btnTestingEmail.TabIndex = 15
        Me.btnTestingEmail.Text = "Sending Test Mail"
        Me.ToolTip1.SetToolTip(Me.btnTestingEmail, "Sending Test Mail")
        Me.btnTestingEmail.UseVisualStyleBackColor = True
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.btnDelete, Me.toolStripSeparator, Me.btnPrint, Me.toolStripSeparator1, Me.btnhelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(705, 32)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(75, 29)
        Me.btnNew.Text = "&New"
        '
        'btnEdit
        '
        Me.btnEdit.Image = CType(resources.GetObject("btnEdit.Image"), System.Drawing.Image)
        Me.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(70, 29)
        Me.btnEdit.Text = "&Edit"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(77, 29)
        Me.btnSave.Text = "&Save"
        '
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(90, 29)
        Me.btnDelete.Text = "D&elete"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 32)
        '
        'btnPrint
        '
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(76, 29)
        Me.btnPrint.Text = "&Print"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 32)
        '
        'btnhelp
        '
        Me.btnhelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnhelp.Image = CType(resources.GetObject("btnhelp.Image"), System.Drawing.Image)
        Me.btnhelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnhelp.Name = "btnhelp"
        Me.btnhelp.Size = New System.Drawing.Size(28, 29)
        Me.btnhelp.Text = "He&lp"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(426, 155)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(14, 20)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "-"
        '
        'cmbport
        '
        Me.cmbport.FormattingEnabled = True
        Me.cmbport.Location = New System.Drawing.Point(132, 272)
        Me.cmbport.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbport.Name = "cmbport"
        Me.cmbport.Size = New System.Drawing.Size(283, 28)
        Me.cmbport.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.cmbport, "Select Port")
        '
        'chckactive
        '
        Me.chckactive.AutoSize = True
        Me.chckactive.Checked = True
        Me.chckactive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chckactive.Location = New System.Drawing.Point(132, 314)
        Me.chckactive.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chckactive.Name = "chckactive"
        Me.chckactive.Size = New System.Drawing.Size(78, 24)
        Me.chckactive.TabIndex = 14
        Me.chckactive.Text = "Active"
        Me.ToolTip1.SetToolTip(Me.chckactive, "Optional SSL Active")
        Me.chckactive.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(15, 315)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(44, 20)
        Me.Label6.TabIndex = 13
        Me.Label6.Text = "SSL:"
        '
        'CmbSmtp
        '
        Me.CmbSmtp.FormattingEnabled = True
        Me.CmbSmtp.Location = New System.Drawing.Point(132, 231)
        Me.CmbSmtp.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CmbSmtp.Name = "CmbSmtp"
        Me.CmbSmtp.Size = New System.Drawing.Size(283, 28)
        Me.CmbSmtp.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.CmbSmtp, "Select Smtp Server")
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(18, 277)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(98, 20)
        Me.Label5.TabIndex = 11
        Me.Label5.Text = "Port Number"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(15, 235)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(97, 20)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Smtp Server"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 195)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(78, 20)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Password"
        '
        'txtpassword
        '
        Me.txtpassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpassword.Location = New System.Drawing.Point(132, 191)
        Me.txtpassword.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtpassword.Name = "txtpassword"
        Me.txtpassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(124)
        Me.txtpassword.Size = New System.Drawing.Size(283, 26)
        Me.txtpassword.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.txtpassword, "Password")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 155)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 20)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "E-Mail"
        '
        'txtemail
        '
        Me.txtemail.Location = New System.Drawing.Point(132, 151)
        Me.txtemail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtemail.Name = "txtemail"
        Me.txtemail.Size = New System.Drawing.Size(283, 26)
        Me.txtemail.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.txtemail, "User Name")
        '
        'CmbMail
        '
        Me.CmbMail.FormattingEnabled = True
        Me.CmbMail.Items.AddRange(New Object() {"@siriussolution.com", "@yahoo.com", "@ymail.com", "@hotmail.com", "@live.com", "@msn.com", "@gmail.com", "@aol.com"})
        Me.CmbMail.Location = New System.Drawing.Point(450, 151)
        Me.CmbMail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CmbMail.Name = "CmbMail"
        Me.CmbMail.Size = New System.Drawing.Size(180, 28)
        Me.CmbMail.TabIndex = 18
        Me.ToolTip1.SetToolTip(Me.CmbMail, "Select Host")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 115)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(106, 20)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Display Name"
        '
        'txtDisplayName
        '
        Me.txtDisplayName.Location = New System.Drawing.Point(132, 111)
        Me.txtDisplayName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtDisplayName.Name = "txtDisplayName"
        Me.txtDisplayName.Size = New System.Drawing.Size(283, 26)
        Me.txtDisplayName.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtDisplayName, "Display Name")
        '
        'FrmEmailconfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1120, 694)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "FrmEmailconfig"
        Me.Text = "Email Setting"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtDisplayName As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents CmbSmtp As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtpassword As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtemail As System.Windows.Forms.TextBox
    Friend WithEvents CmbMail As System.Windows.Forms.ComboBox
    Friend WithEvents cmbport As System.Windows.Forms.ComboBox
    Friend WithEvents chckactive As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnhelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnTestingEmail As System.Windows.Forms.Button
    Friend WithEvents lblProcess As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

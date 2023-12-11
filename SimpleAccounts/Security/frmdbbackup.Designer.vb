<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmdbbackup
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmdbbackup))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.prgContinue1 = New System.Windows.Forms.ToolStripProgressBar()
        Me.lblRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSplitButton2 = New System.Windows.Forms.ToolStripSplitButton()
        Me.SaveDatabaseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblPrograssBar1 = New System.Windows.Forms.ToolStripLabel()
        Me.tsbConfig = New System.Windows.Forms.ToolStripButton()
        Me.lblPrograssBar = New System.Windows.Forms.ToolStripLabel()
        Me.prgContinue = New System.Windows.Forms.ToolStripProgressBar()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.txtlocation = New System.Windows.Forms.TextBox()
        Me.btnbrowse = New System.Windows.Forms.Button()
        Me.lblpath = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.fbDialog = New System.Windows.Forms.FolderBrowserDialog()
        Me.txtServer = New System.Windows.Forms.TextBox()
        Me.lblServer = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.txtOldPassword = New System.Windows.Forms.TextBox()
        Me.txtNewPassword = New System.Windows.Forms.TextBox()
        Me.txtDatabase = New System.Windows.Forms.TextBox()
        Me.ToolStripSplitButton1 = New System.Windows.Forms.ToolStripSplitButton()
        Me.DatabaseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.LocationToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.lblOld = New System.Windows.Forms.Label()
        Me.lblNew = New System.Windows.Forms.Label()
        Me.lblLinkChange = New System.Windows.Forms.LinkLabel()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.ToolStrip1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.prgContinue1, Me.lblRefresh, Me.ToolStripSplitButton2, Me.lblPrograssBar1, Me.tsbConfig})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(644, 37)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'prgContinue1
        '
        Me.prgContinue1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.prgContinue1.Name = "prgContinue1"
        Me.prgContinue1.Size = New System.Drawing.Size(150, 34)
        '
        'lblRefresh
        '
        Me.lblRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.lblRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.lblRefresh.Name = "lblRefresh"
        Me.lblRefresh.Size = New System.Drawing.Size(98, 34)
        Me.lblRefresh.Text = "Refresh"
        '
        'ToolStripSplitButton2
        '
        Me.ToolStripSplitButton2.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveDatabaseToolStripMenuItem})
        Me.ToolStripSplitButton2.Image = Global.SimpleAccounts.My.Resources.Resources.Control_Panel_1
        Me.ToolStripSplitButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripSplitButton2.Name = "ToolStripSplitButton2"
        Me.ToolStripSplitButton2.Size = New System.Drawing.Size(121, 34)
        Me.ToolStripSplitButton2.Text = "Settings"
        '
        'SaveDatabaseToolStripMenuItem
        '
        Me.SaveDatabaseToolStripMenuItem.Name = "SaveDatabaseToolStripMenuItem"
        Me.SaveDatabaseToolStripMenuItem.Size = New System.Drawing.Size(232, 30)
        Me.SaveDatabaseToolStripMenuItem.Text = "Default Database"
        '
        'lblPrograssBar1
        '
        Me.lblPrograssBar1.Name = "lblPrograssBar1"
        Me.lblPrograssBar1.Size = New System.Drawing.Size(81, 34)
        Me.lblPrograssBar1.Text = "Progress"
        '
        'tsbConfig
        '
        Me.tsbConfig.Image = Global.SimpleAccounts.My.Resources.Resources.Advanced_Options
        Me.tsbConfig.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbConfig.Name = "tsbConfig"
        Me.tsbConfig.Size = New System.Drawing.Size(93, 34)
        Me.tsbConfig.Text = "Config"
        '
        'lblPrograssBar
        '
        Me.lblPrograssBar.Name = "lblPrograssBar"
        Me.lblPrograssBar.Size = New System.Drawing.Size(52, 22)
        Me.lblPrograssBar.Text = "Prograss"
        Me.lblPrograssBar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'prgContinue
        '
        Me.prgContinue.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.prgContinue.Name = "prgContinue"
        Me.prgContinue.Size = New System.Drawing.Size(150, 22)
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(14, 17)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(235, 35)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Backup Database"
        '
        'txtlocation
        '
        Me.txtlocation.Location = New System.Drawing.Point(140, 192)
        Me.txtlocation.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtlocation.Name = "txtlocation"
        Me.txtlocation.Size = New System.Drawing.Size(410, 26)
        Me.txtlocation.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.txtlocation, "Backup Database Path")
        '
        'btnbrowse
        '
        Me.btnbrowse.Location = New System.Drawing.Point(140, 354)
        Me.btnbrowse.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnbrowse.Name = "btnbrowse"
        Me.btnbrowse.Size = New System.Drawing.Size(112, 35)
        Me.btnbrowse.TabIndex = 9
        Me.btnbrowse.Text = "Browse..."
        Me.ToolTip1.SetToolTip(Me.btnbrowse, "Browse Location")
        Me.btnbrowse.UseVisualStyleBackColor = True
        '
        'lblpath
        '
        Me.lblpath.AutoSize = True
        Me.lblpath.Location = New System.Drawing.Point(15, 197)
        Me.lblpath.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblpath.Name = "lblpath"
        Me.lblpath.Size = New System.Drawing.Size(107, 20)
        Me.lblpath.TabIndex = 7
        Me.lblpath.Text = "Path/Location"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(15, 155)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 20)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "Database"
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(440, 354)
        Me.btnStart.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(112, 35)
        Me.btnStart.TabIndex = 10
        Me.btnStart.Text = "Start"
        Me.ToolTip1.SetToolTip(Me.btnStart, "Start Backup Database")
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'txtServer
        '
        Me.txtServer.Location = New System.Drawing.Point(140, 111)
        Me.txtServer.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtServer.Name = "txtServer"
        Me.txtServer.ReadOnly = True
        Me.txtServer.Size = New System.Drawing.Size(410, 26)
        Me.txtServer.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtServer, "Server Name")
        '
        'lblServer
        '
        Me.lblServer.AutoSize = True
        Me.lblServer.Location = New System.Drawing.Point(15, 115)
        Me.lblServer.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblServer.Name = "lblServer"
        Me.lblServer.Size = New System.Drawing.Size(55, 20)
        Me.lblServer.TabIndex = 2
        Me.lblServer.Text = "Server"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(140, 232)
        Me.txtPassword.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(410, 26)
        Me.txtPassword.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.txtPassword, "Only type in if you want to set password first time by pressing enter key. Otherw" & _
        "ise you have to change it by clicking change label ahead in order to get old pas" & _
        "sword and new password fields appeared.")
        Me.txtPassword.UseSystemPasswordChar = True
        '
        'txtOldPassword
        '
        Me.txtOldPassword.Location = New System.Drawing.Point(140, 272)
        Me.txtOldPassword.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtOldPassword.Name = "txtOldPassword"
        Me.txtOldPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtOldPassword.Size = New System.Drawing.Size(410, 26)
        Me.txtOldPassword.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.txtOldPassword, "Type in old password in order to create new password.")
        Me.txtOldPassword.UseSystemPasswordChar = True
        '
        'txtNewPassword
        '
        Me.txtNewPassword.Location = New System.Drawing.Point(140, 314)
        Me.txtNewPassword.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtNewPassword.Name = "txtNewPassword"
        Me.txtNewPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtNewPassword.Size = New System.Drawing.Size(410, 26)
        Me.txtNewPassword.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.txtNewPassword, "Press enter key to set new password when cursor is in.")
        Me.txtNewPassword.UseSystemPasswordChar = True
        '
        'txtDatabase
        '
        Me.txtDatabase.Location = New System.Drawing.Point(140, 152)
        Me.txtDatabase.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtDatabase.Name = "txtDatabase"
        Me.txtDatabase.ReadOnly = True
        Me.txtDatabase.Size = New System.Drawing.Size(410, 26)
        Me.txtDatabase.TabIndex = 19
        Me.ToolTip1.SetToolTip(Me.txtDatabase, "Server Name")
        '
        'ToolStripSplitButton1
        '
        Me.ToolStripSplitButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DatabaseToolStripMenuItem, Me.LocationToolStripMenuItem})
        Me.ToolStripSplitButton1.Image = Global.SimpleAccounts.My.Resources.Resources.Control_Panel_1
        Me.ToolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripSplitButton1.Name = "ToolStripSplitButton1"
        Me.ToolStripSplitButton1.Size = New System.Drawing.Size(81, 22)
        Me.ToolStripSplitButton1.Text = "Settings"
        '
        'DatabaseToolStripMenuItem
        '
        Me.DatabaseToolStripMenuItem.Name = "DatabaseToolStripMenuItem"
        Me.DatabaseToolStripMenuItem.Size = New System.Drawing.Size(212, 30)
        Me.DatabaseToolStripMenuItem.Text = "Save Database"
        '
        'LocationToolStripMenuItem
        '
        Me.LocationToolStripMenuItem.Name = "LocationToolStripMenuItem"
        Me.LocationToolStripMenuItem.Size = New System.Drawing.Size(212, 30)
        Me.LocationToolStripMenuItem.Text = "Save Location"
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New System.Drawing.Point(15, 237)
        Me.lblPassword.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(78, 20)
        Me.lblPassword.TabIndex = 13
        Me.lblPassword.Text = "Password"
        '
        'lblOld
        '
        Me.lblOld.AutoSize = True
        Me.lblOld.Location = New System.Drawing.Point(15, 277)
        Me.lblOld.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblOld.Name = "lblOld"
        Me.lblOld.Size = New System.Drawing.Size(110, 20)
        Me.lblOld.TabIndex = 15
        Me.lblOld.Text = "Old Password "
        '
        'lblNew
        '
        Me.lblNew.AutoSize = True
        Me.lblNew.Location = New System.Drawing.Point(15, 317)
        Me.lblNew.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblNew.Name = "lblNew"
        Me.lblNew.Size = New System.Drawing.Size(117, 20)
        Me.lblNew.TabIndex = 17
        Me.lblNew.Text = "New Password "
        '
        'lblLinkChange
        '
        Me.lblLinkChange.AutoSize = True
        Me.lblLinkChange.Location = New System.Drawing.Point(566, 237)
        Me.lblLinkChange.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblLinkChange.Name = "lblLinkChange"
        Me.lblLinkChange.Size = New System.Drawing.Size(65, 20)
        Me.lblLinkChange.TabIndex = 18
        Me.lblLinkChange.TabStop = True
        Me.lblLinkChange.Text = "Change"
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.pnlHeader.Location = New System.Drawing.Point(0, 37)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(644, 66)
        Me.pnlHeader.TabIndex = 20
        '
        'frmdbbackup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(644, 463)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.txtDatabase)
        Me.Controls.Add(Me.lblLinkChange)
        Me.Controls.Add(Me.txtNewPassword)
        Me.Controls.Add(Me.lblNew)
        Me.Controls.Add(Me.lblOld)
        Me.Controls.Add(Me.txtOldPassword)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.lblServer)
        Me.Controls.Add(Me.txtServer)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblpath)
        Me.Controls.Add(Me.btnbrowse)
        Me.Controls.Add(Me.txtlocation)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmdbbackup"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Backup Database"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents txtlocation As System.Windows.Forms.TextBox
    Friend WithEvents btnbrowse As System.Windows.Forms.Button
    Friend WithEvents lblpath As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents fbDialog As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents ToolStripSplitButton1 As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents DatabaseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LocationToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblPrograssBar As System.Windows.Forms.ToolStripLabel
    Friend WithEvents prgContinue As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents txtServer As System.Windows.Forms.TextBox
    Friend WithEvents lblServer As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents prgContinue1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents ToolStripSplitButton2 As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents SaveDatabaseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblPrograssBar1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents lblOld As System.Windows.Forms.Label
    Friend WithEvents txtOldPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtNewPassword As System.Windows.Forms.TextBox
    Friend WithEvents lblNew As System.Windows.Forms.Label
    Friend WithEvents lblLinkChange As System.Windows.Forms.LinkLabel
    Friend WithEvents lblRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents txtDatabase As System.Windows.Forms.TextBox
    Friend WithEvents tsbConfig As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

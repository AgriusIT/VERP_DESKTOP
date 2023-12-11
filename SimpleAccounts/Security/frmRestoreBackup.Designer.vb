<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRestoreBackup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRestoreBackup))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripProgressBar2 = New System.Windows.Forms.ToolStripProgressBar()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripSplitButton()
        Me.SaveDatabaseToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblPrograssBar1 = New System.Windows.Forms.ToolStripLabel()
        Me.tsbConfig = New System.Windows.Forms.ToolStripButton()
        Me.btnControls = New System.Windows.Forms.ToolStripSplitButton()
        Me.SaveDatabaseToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.lblPrograss = New System.Windows.Forms.ToolStripLabel()
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar()
        Me.cmbDatabases = New System.Windows.Forms.ComboBox()
        Me.lblDatabase = New System.Windows.Forms.Label()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.txtLocation = New System.Windows.Forms.TextBox()
        Me.lblLocation = New System.Windows.Forms.Label()
        Me.btnBrowse = New System.Windows.Forms.Button()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.btnAddServer = New System.Windows.Forms.Button()
        Me.lblServer = New System.Windows.Forms.Label()
        Me.txtServer = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.ToolStrip1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripProgressBar2, Me.ToolStripButton1, Me.lblPrograssBar1, Me.tsbConfig})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(633, 37)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripProgressBar2
        '
        Me.ToolStripProgressBar2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripProgressBar2.Name = "ToolStripProgressBar2"
        Me.ToolStripProgressBar2.Size = New System.Drawing.Size(150, 34)
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveDatabaseToolStripMenuItem1})
        Me.ToolStripButton1.Image = Global.SimpleAccounts.My.Resources.Resources.Control_Panel_1
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(121, 34)
        Me.ToolStripButton1.Text = "Settings"
        '
        'SaveDatabaseToolStripMenuItem1
        '
        Me.SaveDatabaseToolStripMenuItem1.Name = "SaveDatabaseToolStripMenuItem1"
        Me.SaveDatabaseToolStripMenuItem1.Size = New System.Drawing.Size(232, 30)
        Me.SaveDatabaseToolStripMenuItem1.Text = "Default Database"
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
        'btnControls
        '
        Me.btnControls.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SaveDatabaseToolStripMenuItem})
        Me.btnControls.Image = Global.SimpleAccounts.My.Resources.Resources.Control_Panel_1
        Me.btnControls.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnControls.Name = "btnControls"
        Me.btnControls.Size = New System.Drawing.Size(81, 22)
        Me.btnControls.Text = "Settings"
        '
        'SaveDatabaseToolStripMenuItem
        '
        Me.SaveDatabaseToolStripMenuItem.Name = "SaveDatabaseToolStripMenuItem"
        Me.SaveDatabaseToolStripMenuItem.Size = New System.Drawing.Size(212, 30)
        Me.SaveDatabaseToolStripMenuItem.Text = "Save Database"
        '
        'lblPrograss
        '
        Me.lblPrograss.Name = "lblPrograss"
        Me.lblPrograss.Size = New System.Drawing.Size(52, 22)
        Me.lblPrograss.Text = "Prograss"
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        Me.ToolStripProgressBar1.Size = New System.Drawing.Size(150, 22)
        '
        'cmbDatabases
        '
        Me.cmbDatabases.FormattingEnabled = True
        Me.cmbDatabases.Location = New System.Drawing.Point(140, 151)
        Me.cmbDatabases.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbDatabases.Name = "cmbDatabases"
        Me.cmbDatabases.Size = New System.Drawing.Size(410, 28)
        Me.cmbDatabases.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.cmbDatabases, "Select Database")
        '
        'lblDatabase
        '
        Me.lblDatabase.AutoSize = True
        Me.lblDatabase.Location = New System.Drawing.Point(18, 155)
        Me.lblDatabase.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDatabase.Name = "lblDatabase"
        Me.lblDatabase.Size = New System.Drawing.Size(79, 20)
        Me.lblDatabase.TabIndex = 5
        Me.lblDatabase.Text = "Database"
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(16, 11)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(231, 36)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Restore Backup"
        '
        'txtLocation
        '
        Me.txtLocation.Location = New System.Drawing.Point(140, 192)
        Me.txtLocation.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(410, 26)
        Me.txtLocation.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.txtLocation, "Get Location Backup")
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = True
        Me.lblLocation.Location = New System.Drawing.Point(18, 197)
        Me.lblLocation.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(70, 20)
        Me.lblLocation.TabIndex = 7
        Me.lblLocation.Text = "Location"
        '
        'btnBrowse
        '
        Me.btnBrowse.Location = New System.Drawing.Point(140, 232)
        Me.btnBrowse.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(112, 35)
        Me.btnBrowse.TabIndex = 9
        Me.btnBrowse.Text = "Browse..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(291, 232)
        Me.btnStart.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(112, 35)
        Me.btnStart.TabIndex = 10
        Me.btnStart.Text = "Start"
        Me.ToolTip1.SetToolTip(Me.btnStart, "Start Restore Backup")
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(440, 232)
        Me.btnReset.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(112, 35)
        Me.btnReset.TabIndex = 11
        Me.btnReset.Text = "Reset"
        Me.ToolTip1.SetToolTip(Me.btnReset, "Reset")
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'btnAddServer
        '
        Me.btnAddServer.Image = Global.SimpleAccounts.My.Resources.Resources.pin_black
        Me.btnAddServer.Location = New System.Drawing.Point(561, 108)
        Me.btnAddServer.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnAddServer.Name = "btnAddServer"
        Me.btnAddServer.Size = New System.Drawing.Size(38, 35)
        Me.btnAddServer.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.btnAddServer, "Change Server Name")
        Me.btnAddServer.UseVisualStyleBackColor = True
        '
        'lblServer
        '
        Me.lblServer.AutoSize = True
        Me.lblServer.Location = New System.Drawing.Point(18, 115)
        Me.lblServer.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblServer.Name = "lblServer"
        Me.lblServer.Size = New System.Drawing.Size(55, 20)
        Me.lblServer.TabIndex = 2
        Me.lblServer.Text = "Server"
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
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.pnlHeader.Location = New System.Drawing.Point(0, 37)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(633, 60)
        Me.pnlHeader.TabIndex = 12
        '
        'frmRestoreBackup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(633, 303)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.btnAddServer)
        Me.Controls.Add(Me.lblServer)
        Me.Controls.Add(Me.txtServer)
        Me.Controls.Add(Me.btnReset)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.btnBrowse)
        Me.Controls.Add(Me.lblLocation)
        Me.Controls.Add(Me.txtLocation)
        Me.Controls.Add(Me.lblDatabase)
        Me.Controls.Add(Me.cmbDatabases)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRestoreBackup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Restore Backup"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents cmbDatabases As System.Windows.Forms.ComboBox
    Friend WithEvents lblDatabase As System.Windows.Forms.Label
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents txtLocation As System.Windows.Forms.TextBox
    Friend WithEvents lblLocation As System.Windows.Forms.Label
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents lblPrograss As System.Windows.Forms.ToolStripLabel
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnAddServer As System.Windows.Forms.Button
    Friend WithEvents lblServer As System.Windows.Forms.Label
    Friend WithEvents txtServer As System.Windows.Forms.TextBox
    Friend WithEvents btnControls As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents SaveDatabaseToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents ToolStripProgressBar2 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents SaveDatabaseToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lblPrograssBar1 As System.Windows.Forms.ToolStripLabel
    Friend WithEvents tsbConfig As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

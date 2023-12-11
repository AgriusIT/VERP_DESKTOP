<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmcreatenewdatabase
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmcreatenewdatabase))
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnCreateDatabase = New System.Windows.Forms.ToolStripButton()
        Me.prgContinue = New System.Windows.Forms.ToolStripProgressBar()
        Me.lblPrograss = New System.Windows.Forms.ToolStripLabel()
        Me.txttitle = New System.Windows.Forms.TextBox()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.lbltitle = New System.Windows.Forms.Label()
        Me.lblrestoredb = New System.Windows.Forms.Label()
        Me.rdoblankdb = New System.Windows.Forms.RadioButton()
        Me.rdosampledb = New System.Windows.Forms.RadioButton()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.cmbServers = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblDatabaseName = New System.Windows.Forms.Label()
        Me.txtDatabase = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtUserName = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtConnectionString = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.ToolStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 32)
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnCreateDatabase, Me.toolStripSeparator, Me.prgContinue, Me.lblPrograss})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(642, 32)
        Me.ToolStrip1.TabIndex = 1
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
        'btnCreateDatabase
        '
        Me.btnCreateDatabase.Image = CType(resources.GetObject("btnCreateDatabase.Image"), System.Drawing.Image)
        Me.btnCreateDatabase.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCreateDatabase.Name = "btnCreateDatabase"
        Me.btnCreateDatabase.Size = New System.Drawing.Size(172, 29)
        Me.btnCreateDatabase.Text = "&Create Company"
        '
        'prgContinue
        '
        Me.prgContinue.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.prgContinue.Name = "prgContinue"
        Me.prgContinue.Size = New System.Drawing.Size(150, 29)
        '
        'lblPrograss
        '
        Me.lblPrograss.Name = "lblPrograss"
        Me.lblPrograss.Size = New System.Drawing.Size(81, 29)
        Me.lblPrograss.Text = "Prograss"
        '
        'txttitle
        '
        Me.txttitle.Location = New System.Drawing.Point(180, 151)
        Me.txttitle.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txttitle.Name = "txttitle"
        Me.txttitle.Size = New System.Drawing.Size(376, 26)
        Me.txttitle.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.txttitle, "Company Title")
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(4, 17)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(315, 36)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Create New Company"
        '
        'lbltitle
        '
        Me.lbltitle.AutoSize = True
        Me.lbltitle.ForeColor = System.Drawing.Color.Black
        Me.lbltitle.Location = New System.Drawing.Point(18, 155)
        Me.lbltitle.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbltitle.Name = "lbltitle"
        Me.lbltitle.Size = New System.Drawing.Size(109, 20)
        Me.lbltitle.TabIndex = 4
        Me.lbltitle.Text = "Company Title"
        '
        'lblrestoredb
        '
        Me.lblrestoredb.AutoSize = True
        Me.lblrestoredb.ForeColor = System.Drawing.Color.Black
        Me.lblrestoredb.Location = New System.Drawing.Point(18, 397)
        Me.lblrestoredb.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblrestoredb.Name = "lblrestoredb"
        Me.lblrestoredb.Size = New System.Drawing.Size(105, 20)
        Me.lblrestoredb.TabIndex = 16
        Me.lblrestoredb.Text = "Restore Data"
        '
        'rdoblankdb
        '
        Me.rdoblankdb.AutoSize = True
        Me.rdoblankdb.Checked = True
        Me.rdoblankdb.ForeColor = System.Drawing.Color.Black
        Me.rdoblankdb.Location = New System.Drawing.Point(6, -3)
        Me.rdoblankdb.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rdoblankdb.Name = "rdoblankdb"
        Me.rdoblankdb.Size = New System.Drawing.Size(113, 24)
        Me.rdoblankdb.TabIndex = 0
        Me.rdoblankdb.TabStop = True
        Me.rdoblankdb.Text = "Blank Data"
        Me.ToolTip1.SetToolTip(Me.rdoblankdb, "Create Company By Blank Database")
        Me.rdoblankdb.UseVisualStyleBackColor = True
        '
        'rdosampledb
        '
        Me.rdosampledb.AutoSize = True
        Me.rdosampledb.ForeColor = System.Drawing.Color.Black
        Me.rdosampledb.Location = New System.Drawing.Point(132, -3)
        Me.rdosampledb.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rdosampledb.Name = "rdosampledb"
        Me.rdosampledb.Size = New System.Drawing.Size(127, 24)
        Me.rdosampledb.TabIndex = 1
        Me.rdosampledb.Text = "Sample Data"
        Me.rdosampledb.UseVisualStyleBackColor = True
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.ForeColor = System.Drawing.Color.Black
        Me.lblPassword.Location = New System.Drawing.Point(18, 277)
        Me.lblPassword.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(78, 20)
        Me.lblPassword.TabIndex = 10
        Me.lblPassword.Text = "Password"
        '
        'txtPassword
        '
        Me.txtPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.Location = New System.Drawing.Point(180, 272)
        Me.txtPassword.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(124)
        Me.txtPassword.Size = New System.Drawing.Size(376, 26)
        Me.txtPassword.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.txtPassword, "Password")
        '
        'cmbServers
        '
        Me.cmbServers.FormattingEnabled = True
        Me.cmbServers.Location = New System.Drawing.Point(180, 191)
        Me.cmbServers.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbServers.Name = "cmbServers"
        Me.cmbServers.Size = New System.Drawing.Size(376, 28)
        Me.cmbServers.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.cmbServers, "Select Any Server ")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(18, 195)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 20)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Server Name"
        '
        'lblDatabaseName
        '
        Me.lblDatabaseName.AutoSize = True
        Me.lblDatabaseName.ForeColor = System.Drawing.Color.Black
        Me.lblDatabaseName.Location = New System.Drawing.Point(18, 317)
        Me.lblDatabaseName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDatabaseName.Name = "lblDatabaseName"
        Me.lblDatabaseName.Size = New System.Drawing.Size(125, 20)
        Me.lblDatabaseName.TabIndex = 12
        Me.lblDatabaseName.Text = "Database Name"
        '
        'txtDatabase
        '
        Me.txtDatabase.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDatabase.Location = New System.Drawing.Point(180, 312)
        Me.txtDatabase.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtDatabase.Name = "txtDatabase"
        Me.txtDatabase.Size = New System.Drawing.Size(376, 26)
        Me.txtDatabase.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.txtDatabase, "Database Name")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(18, 237)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(89, 20)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "User Name"
        '
        'txtUserName
        '
        Me.txtUserName.Location = New System.Drawing.Point(180, 232)
        Me.txtUserName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(376, 26)
        Me.txtUserName.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.txtUserName, "User Name")
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(180, 352)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(150, 35)
        Me.Button1.TabIndex = 14
        Me.Button1.Text = "Test Connection"
        Me.ToolTip1.SetToolTip(Me.Button1, "Test Connection")
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtConnectionString
        '
        Me.txtConnectionString.Location = New System.Drawing.Point(338, 355)
        Me.txtConnectionString.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtConnectionString.Name = "txtConnectionString"
        Me.txtConnectionString.ReadOnly = True
        Me.txtConnectionString.Size = New System.Drawing.Size(218, 26)
        Me.txtConnectionString.TabIndex = 15
        Me.txtConnectionString.Visible = False
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Checked = True
        Me.RadioButton1.Location = New System.Drawing.Point(180, 115)
        Me.RadioButton1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(116, 24)
        Me.RadioButton1.TabIndex = 2
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "SQL Server"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(348, 115)
        Me.RadioButton2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(205, 24)
        Me.RadioButton2.TabIndex = 3
        Me.RadioButton2.Text = "Windows Authentication"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rdoblankdb)
        Me.Panel1.Controls.Add(Me.rdosampledb)
        Me.Panel1.Location = New System.Drawing.Point(180, 397)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(264, 32)
        Me.Panel1.TabIndex = 22
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.pnlHeader.Location = New System.Drawing.Point(0, 32)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(642, 66)
        Me.pnlHeader.TabIndex = 23
        '
        'frmcreatenewdatabase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(642, 502)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.RadioButton2)
        Me.Controls.Add(Me.RadioButton1)
        Me.Controls.Add(Me.txtConnectionString)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtUserName)
        Me.Controls.Add(Me.lblDatabaseName)
        Me.Controls.Add(Me.txtDatabase)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbServers)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.lblrestoredb)
        Me.Controls.Add(Me.lbltitle)
        Me.Controls.Add(Me.txttitle)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmcreatenewdatabase"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Create New Company"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnCreateDatabase As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents txttitle As System.Windows.Forms.TextBox
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents lbltitle As System.Windows.Forms.Label
    Friend WithEvents lblrestoredb As System.Windows.Forms.Label
    Friend WithEvents rdoblankdb As System.Windows.Forms.RadioButton
    Friend WithEvents rdosampledb As System.Windows.Forms.RadioButton
    Friend WithEvents prgContinue As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents lblPrograss As System.Windows.Forms.ToolStripLabel
    Friend WithEvents lblPassword As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents cmbServers As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblDatabaseName As System.Windows.Forms.Label
    Friend WithEvents txtDatabase As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtConnectionString As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmServerSetup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmServerSetup))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtServerName = New System.Windows.Forms.TextBox()
        Me.txtDatabase = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.txtUserId = New System.Windows.Forms.TextBox()
        Me.rbtSQLServer = New System.Windows.Forms.RadioButton()
        Me.rbtWindowsAthentication = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnTestConnection = New System.Windows.Forms.Button()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rbtSample = New System.Windows.Forms.RadioButton()
        Me.rbtBlank = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 37)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(101, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Server Name"
        '
        'txtServerName
        '
        Me.txtServerName.Location = New System.Drawing.Point(153, 32)
        Me.txtServerName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtServerName.Name = "txtServerName"
        Me.txtServerName.Size = New System.Drawing.Size(386, 26)
        Me.txtServerName.TabIndex = 1
        '
        'txtDatabase
        '
        Me.txtDatabase.Location = New System.Drawing.Point(153, 72)
        Me.txtDatabase.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtDatabase.Name = "txtDatabase"
        Me.txtDatabase.Size = New System.Drawing.Size(386, 26)
        Me.txtDatabase.TabIndex = 3
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtPassword)
        Me.GroupBox1.Controls.Add(Me.txtUserId)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 148)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(534, 109)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(20, 74)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(78, 20)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Password"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(20, 34)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 20)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "User ID"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(136, 69)
        Me.txtPassword.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(380, 26)
        Me.txtPassword.TabIndex = 3
        Me.txtPassword.UseSystemPasswordChar = True
        '
        'txtUserId
        '
        Me.txtUserId.Location = New System.Drawing.Point(136, 29)
        Me.txtUserId.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtUserId.Name = "txtUserId"
        Me.txtUserId.Size = New System.Drawing.Size(380, 26)
        Me.txtUserId.TabIndex = 1
        '
        'rbtSQLServer
        '
        Me.rbtSQLServer.AutoSize = True
        Me.rbtSQLServer.Location = New System.Drawing.Point(153, 112)
        Me.rbtSQLServer.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtSQLServer.Name = "rbtSQLServer"
        Me.rbtSQLServer.Size = New System.Drawing.Size(116, 24)
        Me.rbtSQLServer.TabIndex = 4
        Me.rbtSQLServer.Text = "SQL Server"
        Me.rbtSQLServer.UseVisualStyleBackColor = True
        '
        'rbtWindowsAthentication
        '
        Me.rbtWindowsAthentication.AutoSize = True
        Me.rbtWindowsAthentication.Checked = True
        Me.rbtWindowsAthentication.Location = New System.Drawing.Point(332, 112)
        Me.rbtWindowsAthentication.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtWindowsAthentication.Name = "rbtWindowsAthentication"
        Me.rbtWindowsAthentication.Size = New System.Drawing.Size(205, 24)
        Me.rbtWindowsAthentication.TabIndex = 5
        Me.rbtWindowsAthentication.TabStop = True
        Me.rbtWindowsAthentication.Text = "Windows Authentication"
        Me.rbtWindowsAthentication.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 77)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(79, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Database"
        '
        'btnNext
        '
        Me.btnNext.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnNext.Location = New System.Drawing.Point(471, 283)
        Me.btnNext.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(72, 35)
        Me.btnNext.TabIndex = 10
        Me.btnNext.Text = "Next"
        Me.btnNext.UseVisualStyleBackColor = False
        '
        'btnTestConnection
        '
        Me.btnTestConnection.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnTestConnection.Location = New System.Drawing.Point(238, 283)
        Me.btnTestConnection.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnTestConnection.Name = "btnTestConnection"
        Me.btnTestConnection.Size = New System.Drawing.Size(142, 35)
        Me.btnTestConnection.TabIndex = 8
        Me.btnTestConnection.Text = "Test Connection"
        Me.btnTestConnection.UseVisualStyleBackColor = False
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.btnBack.Location = New System.Drawing.Point(390, 283)
        Me.btnBack.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(72, 35)
        Me.btnBack.TabIndex = 9
        Me.btnBack.Text = "Back"
        Me.btnBack.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rbtSample)
        Me.GroupBox2.Controls.Add(Me.rbtBlank)
        Me.GroupBox2.Location = New System.Drawing.Point(22, 266)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Size = New System.Drawing.Size(207, 58)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Restore Data"
        '
        'rbtSample
        '
        Me.rbtSample.AutoSize = True
        Me.rbtSample.Location = New System.Drawing.Point(98, 22)
        Me.rbtSample.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtSample.Name = "rbtSample"
        Me.rbtSample.Size = New System.Drawing.Size(88, 24)
        Me.rbtSample.TabIndex = 1
        Me.rbtSample.Text = "Sample"
        Me.rbtSample.UseVisualStyleBackColor = True
        '
        'rbtBlank
        '
        Me.rbtBlank.AutoSize = True
        Me.rbtBlank.Checked = True
        Me.rbtBlank.Location = New System.Drawing.Point(10, 22)
        Me.rbtBlank.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtBlank.Name = "rbtBlank"
        Me.rbtBlank.Size = New System.Drawing.Size(74, 24)
        Me.rbtBlank.TabIndex = 0
        Me.rbtBlank.TabStop = True
        Me.rbtBlank.Text = "Blank"
        Me.rbtBlank.UseVisualStyleBackColor = True
        '
        'frmServerSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(628, 365)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.btnTestConnection)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.rbtWindowsAthentication)
        Me.Controls.Add(Me.rbtSQLServer)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtDatabase)
        Me.Controls.Add(Me.txtServerName)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmServerSetup"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Server"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtServerName As System.Windows.Forms.TextBox
    Friend WithEvents txtDatabase As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtSQLServer As System.Windows.Forms.RadioButton
    Friend WithEvents rbtWindowsAthentication As System.Windows.Forms.RadioButton
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents txtUserId As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnTestConnection As System.Windows.Forms.Button
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtSample As System.Windows.Forms.RadioButton
    Friend WithEvents rbtBlank As System.Windows.Forms.RadioButton
End Class

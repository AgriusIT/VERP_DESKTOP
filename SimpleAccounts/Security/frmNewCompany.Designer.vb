<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNewCompany
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNewCompany))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnLoadBKFile = New System.Windows.Forms.Button
        Me.btnNew = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtBKPath = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtPassword = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtUserID = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtServer = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.txtCompanyName = New System.Windows.Forms.TextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.grd = New System.Windows.Forms.DataGridView
        Me.BrowseBKFile = New System.Windows.Forms.OpenFileDialog
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.White
        Me.GroupBox1.Controls.Add(Me.btnLoadBKFile)
        Me.GroupBox1.Controls.Add(Me.btnNew)
        Me.GroupBox1.Controls.Add(Me.btnSave)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtBKPath)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtPassword)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtUserID)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtServer)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.txtCompanyName)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(383, 144)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'btnLoadBKFile
        '
        Me.btnLoadBKFile.Location = New System.Drawing.Point(348, 87)
        Me.btnLoadBKFile.Name = "btnLoadBKFile"
        Me.btnLoadBKFile.Size = New System.Drawing.Size(26, 23)
        Me.btnLoadBKFile.TabIndex = 11
        Me.btnLoadBKFile.Text = "..."
        Me.btnLoadBKFile.UseVisualStyleBackColor = True
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(218, 114)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(75, 23)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = "New"
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Location = New System.Drawing.Point(299, 114)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 12
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(5, 91)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Backup"
        '
        'txtBKPath
        '
        Me.txtBKPath.Enabled = False
        Me.txtBKPath.Location = New System.Drawing.Point(103, 88)
        Me.txtBKPath.MaxLength = 200
        Me.txtBKPath.Name = "txtBKPath"
        Me.txtBKPath.Size = New System.Drawing.Size(239, 20)
        Me.txtBKPath.TabIndex = 10
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(212, 67)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Password"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(271, 64)
        Me.txtPassword.MaxLength = 15
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(124)
        Me.txtPassword.Size = New System.Drawing.Size(103, 20)
        Me.txtPassword.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(5, 67)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(43, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "User ID"
        '
        'txtUserID
        '
        Me.txtUserID.Location = New System.Drawing.Point(103, 64)
        Me.txtUserID.MaxLength = 10
        Me.txtUserID.Name = "txtUserID"
        Me.txtUserID.Size = New System.Drawing.Size(103, 20)
        Me.txtUserID.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Server"
        '
        'txtServer
        '
        Me.txtServer.Location = New System.Drawing.Point(104, 39)
        Me.txtServer.MaxLength = 20
        Me.txtServer.Name = "txtServer"
        Me.txtServer.Size = New System.Drawing.Size(270, 20)
        Me.txtServer.TabIndex = 4
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(82, 13)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "Company Name"
        '
        'txtCompanyName
        '
        Me.txtCompanyName.Location = New System.Drawing.Point(104, 13)
        Me.txtCompanyName.MaxLength = 10
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.Size = New System.Drawing.Size(270, 20)
        Me.txtCompanyName.TabIndex = 2
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.White
        Me.GroupBox2.Controls.Add(Me.grd)
        Me.GroupBox2.Location = New System.Drawing.Point(8, 162)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(383, 225)
        Me.GroupBox2.TabIndex = 1
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Existing Companies"
        '
        'grd
        '
        Me.grd.AllowUserToAddRows = False
        Me.grd.AllowUserToDeleteRows = False
        Me.grd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grd.Location = New System.Drawing.Point(9, 19)
        Me.grd.Name = "grd"
        Me.grd.ReadOnly = True
        Me.grd.RowHeadersVisible = False
        Me.grd.RowHeadersWidth = 10
        Me.grd.Size = New System.Drawing.Size(365, 200)
        Me.grd.TabIndex = 0
        '
        'BrowseBKFile
        '
        Me.BrowseBKFile.FileName = "OpenFileDialog1"
        '
        'frmNewCompany
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(396, 395)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(412, 433)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(412, 433)
        Me.Name = "frmNewCompany"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add New Company"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtCompanyName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtBKPath As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtUserID As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtServer As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents grd As System.Windows.Forms.DataGridView
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnLoadBKFile As System.Windows.Forms.Button
    Friend WithEvents BrowseBKFile As System.Windows.Forms.OpenFileDialog
End Class

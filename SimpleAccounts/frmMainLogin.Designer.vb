<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMainLogin
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMainLogin))
        Me.btnBack = New System.Windows.Forms.Button()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.txtUsername = New System.Windows.Forms.TextBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.btnLogin = New System.Windows.Forms.Button()
        Me.cbStaySignedIn = New System.Windows.Forms.CheckBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.pnlErrorNotification = New System.Windows.Forms.Panel()
        Me.lblErrorNotification = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnDismissMessage = New System.Windows.Forms.Button()
        Me.tmrMessageNotificationLabel = New System.Windows.Forms.Timer(Me.components)
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnClose = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlErrorNotification.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.Transparent
        Me.btnBack.FlatAppearance.BorderSize = 0
        Me.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBack.Image = CType(resources.GetObject("btnBack.Image"), System.Drawing.Image)
        Me.btnBack.Location = New System.Drawing.Point(0, 5)
        Me.btnBack.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(90, 66)
        Me.btnBack.TabIndex = 3
        Me.btnBack.UseVisualStyleBackColor = False
        '
        'LinkLabel2
        '
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.BackColor = System.Drawing.Color.Transparent
        Me.LinkLabel2.Font = New System.Drawing.Font("Verdana", 9.75!)
        Me.LinkLabel2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.LinkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel2.LinkColor = System.Drawing.Color.Navy
        Me.LinkLabel2.Location = New System.Drawing.Point(130, 16)
        Me.LinkLabel2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(133, 20)
        Me.LinkLabel2.TabIndex = 2
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "New Company"
        Me.LinkLabel2.Visible = False
        '
        'Panel2
        '
        Me.Panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel2.Controls.Add(Me.PictureBox2)
        Me.Panel2.Controls.Add(Me.txtUsername)
        Me.Panel2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.Panel2.Location = New System.Drawing.Point(74, 185)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(348, 43)
        Me.Panel2.TabIndex = 1
        '
        'PictureBox2
        '
        Me.PictureBox2.Image = CType(resources.GetObject("PictureBox2.Image"), System.Drawing.Image)
        Me.PictureBox2.Location = New System.Drawing.Point(4, 2)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(34, 34)
        Me.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox2.TabIndex = 3
        Me.PictureBox2.TabStop = False
        '
        'txtUsername
        '
        Me.txtUsername.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtUsername.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUsername.ForeColor = System.Drawing.Color.FromArgb(CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.txtUsername.Location = New System.Drawing.Point(45, 4)
        Me.txtUsername.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtUsername.Name = "txtUsername"
        Me.txtUsername.Size = New System.Drawing.Size(297, 35)
        Me.txtUsername.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.txtUsername, "User Name")
        '
        'Panel3
        '
        Me.Panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel3.Controls.Add(Me.PictureBox3)
        Me.Panel3.Controls.Add(Me.txtPassword)
        Me.Panel3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(197, Byte), Integer), CType(CType(229, Byte), Integer), CType(CType(254, Byte), Integer))
        Me.Panel3.Location = New System.Drawing.Point(74, 235)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(348, 45)
        Me.Panel3.TabIndex = 2
        '
        'PictureBox3
        '
        Me.PictureBox3.Image = CType(resources.GetObject("PictureBox3.Image"), System.Drawing.Image)
        Me.PictureBox3.Location = New System.Drawing.Point(4, 5)
        Me.PictureBox3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(32, 34)
        Me.PictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox3.TabIndex = 3
        Me.PictureBox3.TabStop = False
        '
        'txtPassword
        '
        Me.txtPassword.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtPassword.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPassword.ForeColor = System.Drawing.Color.FromArgb(CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.txtPassword.Location = New System.Drawing.Point(45, 6)
        Me.txtPassword.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.Size = New System.Drawing.Size(297, 35)
        Me.txtPassword.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.txtPassword, "Password")
        Me.txtPassword.UseSystemPasswordChar = True
        '
        'btnLogin
        '
        Me.btnLogin.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.btnLogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnLogin.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnLogin.FlatAppearance.BorderSize = 0
        Me.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLogin.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnLogin.ForeColor = System.Drawing.Color.White
        Me.btnLogin.Location = New System.Drawing.Point(198, 369)
        Me.btnLogin.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnLogin.Name = "btnLogin"
        Me.btnLogin.Size = New System.Drawing.Size(101, 37)
        Me.btnLogin.TabIndex = 4
        Me.btnLogin.Text = "Login "
        Me.btnLogin.UseVisualStyleBackColor = False
        '
        'cbStaySignedIn
        '
        Me.cbStaySignedIn.AutoSize = True
        Me.cbStaySignedIn.BackColor = System.Drawing.Color.Transparent
        Me.cbStaySignedIn.CheckAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.cbStaySignedIn.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbStaySignedIn.ForeColor = System.Drawing.Color.FromArgb(CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.cbStaySignedIn.Location = New System.Drawing.Point(75, 334)
        Me.cbStaySignedIn.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cbStaySignedIn.Name = "cbStaySignedIn"
        Me.cbStaySignedIn.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.cbStaySignedIn.Size = New System.Drawing.Size(123, 24)
        Me.cbStaySignedIn.TabIndex = 3
        Me.cbStaySignedIn.Text = "Stay signed in"
        Me.cbStaySignedIn.UseVisualStyleBackColor = False
        Me.cbStaySignedIn.Visible = False
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.White
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TextBox1.Enabled = False
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold)
        Me.TextBox1.ForeColor = System.Drawing.SystemColors.MenuHighlight
        Me.TextBox1.Location = New System.Drawing.Point(692, 132)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.TextBox1.Multiline = True
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(78, 32)
        Me.TextBox1.TabIndex = 26
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TextBox1.Visible = False
        '
        'ComboBox1
        '
        Me.ComboBox1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.ComboBox1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.ComboBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.ComboBox1.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboBox1.ForeColor = System.Drawing.Color.Black
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(74, 288)
        Me.ComboBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(346, 40)
        Me.ComboBox1.TabIndex = 5
        '
        'pnlErrorNotification
        '
        Me.pnlErrorNotification.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(91, Byte), Integer), CType(CType(174, Byte), Integer))
        Me.pnlErrorNotification.Controls.Add(Me.lblErrorNotification)
        Me.pnlErrorNotification.Controls.Add(Me.Panel4)
        Me.pnlErrorNotification.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlErrorNotification.ForeColor = System.Drawing.Color.White
        Me.pnlErrorNotification.Location = New System.Drawing.Point(0, 457)
        Me.pnlErrorNotification.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnlErrorNotification.Name = "pnlErrorNotification"
        Me.pnlErrorNotification.Size = New System.Drawing.Size(917, 71)
        Me.pnlErrorNotification.TabIndex = 28
        Me.pnlErrorNotification.Visible = False
        '
        'lblErrorNotification
        '
        Me.lblErrorNotification.BackColor = System.Drawing.Color.Teal
        Me.lblErrorNotification.Font = New System.Drawing.Font("Calibri", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblErrorNotification.Location = New System.Drawing.Point(0, 0)
        Me.lblErrorNotification.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblErrorNotification.Name = "lblErrorNotification"
        Me.lblErrorNotification.Size = New System.Drawing.Size(756, 71)
        Me.lblErrorNotification.TabIndex = 0
        Me.lblErrorNotification.Text = "Something went wrong we are sorry for inconvenience"
        Me.lblErrorNotification.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Teal
        Me.Panel4.Controls.Add(Me.btnDismissMessage)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel4.Location = New System.Drawing.Point(755, 0)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(162, 71)
        Me.Panel4.TabIndex = 1
        '
        'btnDismissMessage
        '
        Me.btnDismissMessage.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnDismissMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDismissMessage.Font = New System.Drawing.Font("Calibri", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDismissMessage.Location = New System.Drawing.Point(34, 17)
        Me.btnDismissMessage.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnDismissMessage.Name = "btnDismissMessage"
        Me.btnDismissMessage.Size = New System.Drawing.Size(112, 39)
        Me.btnDismissMessage.TabIndex = 0
        Me.btnDismissMessage.Text = "Dismiss"
        Me.btnDismissMessage.UseVisualStyleBackColor = True
        '
        'tmrMessageNotificationLabel
        '
        Me.tmrMessageNotificationLabel.Interval = 5000
        '
        'btnClose
        '
        Me.btnClose.BackColor = System.Drawing.Color.Transparent
        Me.btnClose.FlatAppearance.BorderSize = 0
        Me.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.Location = New System.Drawing.Point(846, 457)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(45, 60)
        Me.btnClose.TabIndex = 3
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.Transparent
        Me.Button1.BackgroundImage = CType(resources.GetObject("Button1.BackgroundImage"), System.Drawing.Image)
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(473, -7)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(307, 158)
        Me.Button1.TabIndex = 29
        Me.Button1.UseVisualStyleBackColor = False
        Me.Button1.Visible = False
        '
        'frmMainLogin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(917, 528)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.LinkLabel2)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.pnlErrorNotification)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.cbStaySignedIn)
        Me.Controls.Add(Me.btnLogin)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMainLogin"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Login"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlErrorNotification.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents txtUsername As System.Windows.Forms.TextBox
    Friend WithEvents PictureBox2 As System.Windows.Forms.PictureBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents PictureBox3 As System.Windows.Forms.PictureBox
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents btnLogin As System.Windows.Forms.Button
    Friend WithEvents cbStaySignedIn As System.Windows.Forms.CheckBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents pnlErrorNotification As System.Windows.Forms.Panel
    Friend WithEvents lblErrorNotification As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents btnDismissMessage As System.Windows.Forms.Button
    Friend WithEvents tmrMessageNotificationLabel As System.Windows.Forms.Timer
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDefUser
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDefUser))
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCode = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.chkExport = New System.Windows.Forms.CheckBox()
        Me.chkPrint = New System.Windows.Forms.CheckBox()
        Me.chkDelete = New System.Windows.Forms.CheckBox()
        Me.chkUpdate = New System.Windows.Forms.CheckBox()
        Me.chkSave = New System.Windows.Forms.CheckBox()
        Me.chkView = New System.Windows.Forms.CheckBox()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton4 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton5 = New System.Windows.Forms.ToolStripButton()
        Me.grdForm = New System.Windows.Forms.DataGridView()
        Me.grdSaved = New System.Windows.Forms.DataGridView()
        Me.chkActive = New System.Windows.Forms.CheckBox()
        Me.txtConPsw = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtUserID = New System.Windows.Forms.TextBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.BtnNew = New System.Windows.Forms.ToolStripButton()
        Me.BtnEdit = New System.Windows.Forms.ToolStripButton()
        Me.BtnSave = New System.Windows.Forms.ToolStripButton()
        Me.BtnDelete = New System.Windows.Forms.ToolStripButton()
        Me.BtnPrint = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.lnklblSetDefault = New System.Windows.Forms.LinkLabel()
        Me.chkPostingUser = New System.Windows.Forms.CheckBox()
        Me.chkPriceAllowed = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.ToolStrip2.SuspendLayout()
        CType(Me.grdForm, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtName
        '
        Me.txtName.Location = New System.Drawing.Point(151, 72)
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(214, 28)
        Me.txtName.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.txtName, "User Name")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(12, 75)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(103, 20)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "User Name"
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(151, 99)
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(214, 28)
        Me.txtCode.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.txtCode, "User Code")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(12, 102)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(96, 20)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "User Code"
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(151, 126)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(214, 28)
        Me.txtPassword.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.txtPassword, "Password")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(12, 129)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(89, 20)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Password"
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Location = New System.Drawing.Point(371, 72)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(621, 438)
        Me.TabControl1.TabIndex = 4
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.chkExport)
        Me.TabPage1.Controls.Add(Me.chkPrint)
        Me.TabPage1.Controls.Add(Me.chkDelete)
        Me.TabPage1.Controls.Add(Me.chkUpdate)
        Me.TabPage1.Controls.Add(Me.chkSave)
        Me.TabPage1.Controls.Add(Me.chkView)
        Me.TabPage1.Controls.Add(Me.ToolStrip2)
        Me.TabPage1.Controls.Add(Me.grdForm)
        Me.TabPage1.Location = New System.Drawing.Point(4, 29)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(613, 405)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Forms"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'chkExport
        '
        Me.chkExport.AutoSize = True
        Me.chkExport.Location = New System.Drawing.Point(327, 39)
        Me.chkExport.Name = "chkExport"
        Me.chkExport.Size = New System.Drawing.Size(91, 24)
        Me.chkExport.TabIndex = 6
        Me.chkExport.Text = "Export"
        Me.ToolTip1.SetToolTip(Me.chkExport, "Export Rights")
        Me.chkExport.UseVisualStyleBackColor = True
        '
        'chkPrint
        '
        Me.chkPrint.AutoSize = True
        Me.chkPrint.Location = New System.Drawing.Point(269, 39)
        Me.chkPrint.Name = "chkPrint"
        Me.chkPrint.Size = New System.Drawing.Size(75, 24)
        Me.chkPrint.TabIndex = 5
        Me.chkPrint.Text = "Print"
        Me.ToolTip1.SetToolTip(Me.chkPrint, "Print Rights")
        Me.chkPrint.UseVisualStyleBackColor = True
        '
        'chkDelete
        '
        Me.chkDelete.AutoSize = True
        Me.chkDelete.Location = New System.Drawing.Point(200, 39)
        Me.chkDelete.Name = "chkDelete"
        Me.chkDelete.Size = New System.Drawing.Size(90, 24)
        Me.chkDelete.TabIndex = 4
        Me.chkDelete.Text = "Delete"
        Me.ToolTip1.SetToolTip(Me.chkDelete, "Delete Rights")
        Me.chkDelete.UseVisualStyleBackColor = True
        '
        'chkUpdate
        '
        Me.chkUpdate.AutoSize = True
        Me.chkUpdate.Location = New System.Drawing.Point(128, 39)
        Me.chkUpdate.Name = "chkUpdate"
        Me.chkUpdate.Size = New System.Drawing.Size(96, 24)
        Me.chkUpdate.TabIndex = 3
        Me.chkUpdate.Text = "Update"
        Me.ToolTip1.SetToolTip(Me.chkUpdate, "Update Rights")
        Me.chkUpdate.UseVisualStyleBackColor = True
        '
        'chkSave
        '
        Me.chkSave.AutoSize = True
        Me.chkSave.Location = New System.Drawing.Point(66, 39)
        Me.chkSave.Name = "chkSave"
        Me.chkSave.Size = New System.Drawing.Size(77, 24)
        Me.chkSave.TabIndex = 2
        Me.chkSave.Text = "Save"
        Me.ToolTip1.SetToolTip(Me.chkSave, "Save Rights")
        Me.chkSave.UseVisualStyleBackColor = True
        '
        'chkView
        '
        Me.chkView.AutoSize = True
        Me.chkView.Location = New System.Drawing.Point(7, 39)
        Me.chkView.Name = "chkView"
        Me.chkView.Size = New System.Drawing.Size(76, 24)
        Me.chkView.TabIndex = 1
        Me.chkView.Text = "View"
        Me.ToolTip1.SetToolTip(Me.chkView, "View Rights")
        Me.chkView.UseVisualStyleBackColor = True
        '
        'ToolStrip2
        '
        Me.ToolStrip2.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolStripButton2, Me.ToolStripButton3, Me.ToolStripButton4, Me.ToolStripSeparator2, Me.ToolStripButton5})
        Me.ToolStrip2.Location = New System.Drawing.Point(3, 3)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(607, 32)
        Me.ToolStrip2.TabIndex = 0
        Me.ToolStrip2.Text = "ToolStrip2"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(75, 29)
        Me.ToolStripButton1.Text = "&New"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(84, 29)
        Me.ToolStripButton2.Text = "&Open"
        Me.ToolStripButton2.Visible = False
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(77, 29)
        Me.ToolStripButton3.Text = "&Save"
        '
        'ToolStripButton4
        '
        Me.ToolStripButton4.Image = CType(resources.GetObject("ToolStripButton4.Image"), System.Drawing.Image)
        Me.ToolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton4.Name = "ToolStripButton4"
        Me.ToolStripButton4.Size = New System.Drawing.Size(76, 29)
        Me.ToolStripButton4.Text = "&Print"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 32)
        '
        'ToolStripButton5
        '
        Me.ToolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton5.Image = CType(resources.GetObject("ToolStripButton5.Image"), System.Drawing.Image)
        Me.ToolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton5.Name = "ToolStripButton5"
        Me.ToolStripButton5.Size = New System.Drawing.Size(28, 29)
        Me.ToolStripButton5.Text = "He&lp"
        '
        'grdForm
        '
        Me.grdForm.AllowUserToAddRows = False
        Me.grdForm.AllowUserToDeleteRows = False
        Me.grdForm.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdForm.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdForm.Location = New System.Drawing.Point(3, 65)
        Me.grdForm.Name = "grdForm"
        Me.grdForm.RowHeadersVisible = False
        Me.grdForm.Size = New System.Drawing.Size(607, 344)
        Me.grdForm.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.grdForm, "Forms Detail")
        '
        'grdSaved
        '
        Me.grdSaved.AllowUserToAddRows = False
        Me.grdSaved.AllowUserToDeleteRows = False
        Me.grdSaved.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grdSaved.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdSaved.Location = New System.Drawing.Point(14, 249)
        Me.grdSaved.Name = "grdSaved"
        Me.grdSaved.ReadOnly = True
        Me.grdSaved.RowHeadersVisible = False
        Me.grdSaved.Size = New System.Drawing.Size(351, 255)
        Me.grdSaved.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.grdSaved, "User Detail")
        '
        'chkActive
        '
        Me.chkActive.AutoSize = True
        Me.chkActive.BackColor = System.Drawing.Color.Transparent
        Me.chkActive.Checked = True
        Me.chkActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkActive.Location = New System.Drawing.Point(11, 20)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(88, 24)
        Me.chkActive.TabIndex = 0
        Me.chkActive.Text = "Active"
        Me.ToolTip1.SetToolTip(Me.chkActive, "User Block/UnBlock")
        Me.chkActive.UseVisualStyleBackColor = False
        '
        'txtConPsw
        '
        Me.txtConPsw.Location = New System.Drawing.Point(151, 153)
        Me.txtConPsw.Name = "txtConPsw"
        Me.txtConPsw.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtConPsw.Size = New System.Drawing.Size(214, 28)
        Me.txtConPsw.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.txtConPsw, "Confirm Password")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(12, 156)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(163, 20)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Confirm Password"
        '
        'txtUserID
        '
        Me.txtUserID.Location = New System.Drawing.Point(629, 67)
        Me.txtUserID.Name = "txtUserID"
        Me.txtUserID.Size = New System.Drawing.Size(116, 28)
        Me.txtUserID.TabIndex = 3
        Me.txtUserID.Visible = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BtnNew, Me.BtnEdit, Me.BtnSave, Me.BtnDelete, Me.BtnPrint, Me.toolStripSeparator1, Me.HelpToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1019, 32)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'BtnNew
        '
        Me.BtnNew.Image = CType(resources.GetObject("BtnNew.Image"), System.Drawing.Image)
        Me.BtnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnNew.Name = "BtnNew"
        Me.BtnNew.Size = New System.Drawing.Size(75, 29)
        Me.BtnNew.Text = "&New"
        '
        'BtnEdit
        '
        Me.BtnEdit.Image = CType(resources.GetObject("BtnEdit.Image"), System.Drawing.Image)
        Me.BtnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnEdit.Name = "BtnEdit"
        Me.BtnEdit.Size = New System.Drawing.Size(70, 29)
        Me.BtnEdit.Text = "&Edit"
        '
        'BtnSave
        '
        Me.BtnSave.Image = CType(resources.GetObject("BtnSave.Image"), System.Drawing.Image)
        Me.BtnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(77, 29)
        Me.BtnSave.Text = "&Save"
        '
        'BtnDelete
        '
        Me.BtnDelete.Image = CType(resources.GetObject("BtnDelete.Image"), System.Drawing.Image)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.RightToLeftAutoMirrorImage = True
        Me.BtnDelete.Size = New System.Drawing.Size(90, 29)
        Me.BtnDelete.Text = "&Delete"
        '
        'BtnPrint
        '
        Me.BtnPrint.Image = CType(resources.GetObject("BtnPrint.Image"), System.Drawing.Image)
        Me.BtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(76, 29)
        Me.BtnPrint.Text = "&Print"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 32)
        '
        'HelpToolStripButton
        '
        Me.HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HelpToolStripButton.Image = CType(resources.GetObject("HelpToolStripButton.Image"), System.Drawing.Image)
        Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton.Name = "HelpToolStripButton"
        Me.HelpToolStripButton.Size = New System.Drawing.Size(28, 29)
        Me.HelpToolStripButton.Text = "He&lp"
        '
        'lnklblSetDefault
        '
        Me.lnklblSetDefault.AutoSize = True
        Me.lnklblSetDefault.BackColor = System.Drawing.Color.Transparent
        Me.lnklblSetDefault.Location = New System.Drawing.Point(76, 129)
        Me.lnklblSetDefault.Name = "lnklblSetDefault"
        Me.lnklblSetDefault.Size = New System.Drawing.Size(104, 20)
        Me.lnklblSetDefault.TabIndex = 6
        Me.lnklblSetDefault.TabStop = True
        Me.lnklblSetDefault.Text = "Set default"
        Me.lnklblSetDefault.Visible = False
        '
        'chkPostingUser
        '
        Me.chkPostingUser.AutoSize = True
        Me.chkPostingUser.BackColor = System.Drawing.Color.Transparent
        Me.chkPostingUser.Checked = True
        Me.chkPostingUser.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPostingUser.Location = New System.Drawing.Point(11, 40)
        Me.chkPostingUser.Name = "chkPostingUser"
        Me.chkPostingUser.Size = New System.Drawing.Size(142, 24)
        Me.chkPostingUser.TabIndex = 2
        Me.chkPostingUser.Text = "Posting User"
        Me.ToolTip1.SetToolTip(Me.chkPostingUser, "Posting Rights Option")
        Me.chkPostingUser.UseVisualStyleBackColor = False
        '
        'chkPriceAllowed
        '
        Me.chkPriceAllowed.AutoSize = True
        Me.chkPriceAllowed.BackColor = System.Drawing.Color.Transparent
        Me.chkPriceAllowed.Checked = True
        Me.chkPriceAllowed.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPriceAllowed.Location = New System.Drawing.Point(176, 20)
        Me.chkPriceAllowed.Name = "chkPriceAllowed"
        Me.chkPriceAllowed.Size = New System.Drawing.Size(149, 24)
        Me.chkPriceAllowed.TabIndex = 1
        Me.chkPriceAllowed.Text = "Price Allowed"
        Me.ToolTip1.SetToolTip(Me.chkPriceAllowed, "Edit Price Allowd Rights Option")
        Me.chkPriceAllowed.UseVisualStyleBackColor = False
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.chkPriceAllowed)
        Me.GroupBox1.Controls.Add(Me.chkActive)
        Me.GroupBox1.Controls.Add(Me.chkPostingUser)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 180)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(348, 63)
        Me.GroupBox1.TabIndex = 10
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Security Rights"
        '
        'frmDefUser
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1019, 520)
        Me.Controls.Add(Me.lnklblSetDefault)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.txtUserID)
        Me.Controls.Add(Me.txtConPsw)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.grdSaved)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtCode)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmDefUser"
        Me.Text = "User Configuration"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        CType(Me.grdForm, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPassword As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents grdSaved As System.Windows.Forms.DataGridView
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents txtConPsw As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents grdForm As System.Windows.Forms.DataGridView
    Friend WithEvents txtUserID As System.Windows.Forms.TextBox
    Friend WithEvents ToolStrip2 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton4 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton5 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents BtnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents chkPrint As System.Windows.Forms.CheckBox
    Friend WithEvents chkDelete As System.Windows.Forms.CheckBox
    Friend WithEvents chkUpdate As System.Windows.Forms.CheckBox
    Friend WithEvents chkSave As System.Windows.Forms.CheckBox
    Friend WithEvents chkView As System.Windows.Forms.CheckBox
    Friend WithEvents chkExport As System.Windows.Forms.CheckBox
    Friend WithEvents lnklblSetDefault As System.Windows.Forms.LinkLabel
    Friend WithEvents chkPostingUser As System.Windows.Forms.CheckBox
    Friend WithEvents chkPriceAllowed As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

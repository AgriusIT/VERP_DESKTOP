<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfigEmail
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
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.linkDB = New System.Windows.Forms.Label()
        Me.linkPath = New System.Windows.Forms.Label()
        Me.linkCompany = New System.Windows.Forms.Label()
        Me.linkSMS = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel20 = New System.Windows.Forms.Panel()
        Me.rbEmailToUser = New System.Windows.Forms.RadioButton()
        Me.rbEmailToUserNot = New System.Windows.Forms.RadioButton()
        Me.Panel21 = New System.Windows.Forms.Panel()
        Me.Label26 = New System.Windows.Forms.Label()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.Panel19 = New System.Windows.Forms.Panel()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.lstEmailUsers = New SimpleAccounts.uiListControl()
        Me.Panel18 = New System.Windows.Forms.Panel()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.dtpEndat = New System.Windows.Forms.DateTimePicker()
        Me.dtpStartat = New System.Windows.Forms.DateTimePicker()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Panel16 = New System.Windows.Forms.Panel()
        Me.chkAutoFollowupEmail = New System.Windows.Forms.RadioButton()
        Me.chkAutoFollowupEmailNot = New System.Windows.Forms.RadioButton()
        Me.Panel17 = New System.Windows.Forms.Panel()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.chkEmailNotificationOnApproval = New System.Windows.Forms.RadioButton()
        Me.chkEmailNotificationOnApprovalNot = New System.Windows.Forms.RadioButton()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.lblEmailNotificationOnApproval = New System.Windows.Forms.Label()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.chkAutoEmail = New System.Windows.Forms.RadioButton()
        Me.chkAutoEmailNot = New System.Windows.Forms.RadioButton()
        Me.Panel13 = New System.Windows.Forms.Panel()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblAutoEmail = New System.Windows.Forms.Label()
        Me.Panel11 = New System.Windows.Forms.Panel()
        Me.chkEmailAlert = New System.Windows.Forms.RadioButton()
        Me.chkEmailAlertNot = New System.Windows.Forms.RadioButton()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.chkAttachments = New System.Windows.Forms.RadioButton()
        Me.chkAttachmentsNot = New System.Windows.Forms.RadioButton()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.nudDefaultReminder = New System.Windows.Forms.NumericUpDown()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtFileExportPath = New System.Windows.Forms.TextBox()
        Me.cmbDefaultEmail = New System.Windows.Forms.ComboBox()
        Me.txtAdminEmail = New System.Windows.Forms.TextBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblChangeDocumentNo = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel20.SuspendLayout()
        Me.Panel21.SuspendLayout()
        Me.Panel19.SuspendLayout()
        Me.Panel18.SuspendLayout()
        Me.Panel16.SuspendLayout()
        Me.Panel17.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.Panel15.SuspendLayout()
        Me.Panel12.SuspendLayout()
        Me.Panel13.SuspendLayout()
        Me.Panel11.SuspendLayout()
        Me.Panel10.SuspendLayout()
        Me.Panel9.SuspendLayout()
        CType(Me.nudDefaultReminder, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel8.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1512, 94)
        Me.Panel2.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Times New Roman", 21.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(18, 14)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(389, 48)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Email Configuration"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.Panel3.Controls.Add(Me.Label17)
        Me.Panel3.Controls.Add(Me.Label18)
        Me.Panel3.Controls.Add(Me.linkDB)
        Me.Panel3.Controls.Add(Me.linkPath)
        Me.Panel3.Controls.Add(Me.linkCompany)
        Me.Panel3.Controls.Add(Me.linkSMS)
        Me.Panel3.Controls.Add(Me.Label12)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(1190, 94)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(322, 956)
        Me.Panel3.TabIndex = 2
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label17.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(30, 485)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(130, 32)
        Me.Label17.TabIndex = 6
        Me.Label17.Text = "Contact Us"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label18.Location = New System.Drawing.Point(30, 414)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(222, 32)
        Me.Label18.TabIndex = 5
        Me.Label18.Text = "Have a Question(s)"
        '
        'linkDB
        '
        Me.linkDB.AutoSize = True
        Me.linkDB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.linkDB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.linkDB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.linkDB.Location = New System.Drawing.Point(30, 277)
        Me.linkDB.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.linkDB.Name = "linkDB"
        Me.linkDB.Size = New System.Drawing.Size(197, 32)
        Me.linkDB.TabIndex = 4
        Me.linkDB.Text = "Database Backup"
        '
        'linkPath
        '
        Me.linkPath.AutoSize = True
        Me.linkPath.Cursor = System.Windows.Forms.Cursors.Hand
        Me.linkPath.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.linkPath.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.linkPath.Location = New System.Drawing.Point(30, 215)
        Me.linkPath.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.linkPath.Name = "linkPath"
        Me.linkPath.Size = New System.Drawing.Size(154, 32)
        Me.linkPath.TabIndex = 3
        Me.linkPath.Text = "Path Settings"
        '
        'linkCompany
        '
        Me.linkCompany.AutoSize = True
        Me.linkCompany.Cursor = System.Windows.Forms.Cursors.Hand
        Me.linkCompany.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.linkCompany.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.linkCompany.Location = New System.Drawing.Point(30, 154)
        Me.linkCompany.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.linkCompany.Name = "linkCompany"
        Me.linkCompany.Size = New System.Drawing.Size(271, 32)
        Me.linkCompany.TabIndex = 2
        Me.linkCompany.Text = "Company Configuration"
        '
        'linkSMS
        '
        Me.linkSMS.AutoSize = True
        Me.linkSMS.Cursor = System.Windows.Forms.Cursors.Hand
        Me.linkSMS.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.linkSMS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.linkSMS.Location = New System.Drawing.Point(30, 92)
        Me.linkSMS.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.linkSMS.Name = "linkSMS"
        Me.linkSMS.Size = New System.Drawing.Size(217, 32)
        Me.linkSMS.TabIndex = 1
        Me.linkSMS.Text = "SMS Configuration"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(30, 22)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(192, 32)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Related Settings"
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Panel20)
        Me.Panel1.Controls.Add(Me.Panel21)
        Me.Panel1.Controls.Add(Me.Label27)
        Me.Panel1.Controls.Add(Me.Panel19)
        Me.Panel1.Controls.Add(Me.Label24)
        Me.Panel1.Controls.Add(Me.lstEmailUsers)
        Me.Panel1.Controls.Add(Me.Panel18)
        Me.Panel1.Controls.Add(Me.Label20)
        Me.Panel1.Controls.Add(Me.Label21)
        Me.Panel1.Controls.Add(Me.dtpEndat)
        Me.Panel1.Controls.Add(Me.dtpStartat)
        Me.Panel1.Controls.Add(Me.Label22)
        Me.Panel1.Controls.Add(Me.Panel16)
        Me.Panel1.Controls.Add(Me.Panel17)
        Me.Panel1.Controls.Add(Me.Label16)
        Me.Panel1.Controls.Add(Me.Panel14)
        Me.Panel1.Controls.Add(Me.Panel15)
        Me.Panel1.Controls.Add(Me.lblEmailNotificationOnApproval)
        Me.Panel1.Controls.Add(Me.Panel12)
        Me.Panel1.Controls.Add(Me.Panel13)
        Me.Panel1.Controls.Add(Me.lblAutoEmail)
        Me.Panel1.Controls.Add(Me.Panel11)
        Me.Panel1.Controls.Add(Me.Panel10)
        Me.Panel1.Controls.Add(Me.Panel9)
        Me.Panel1.Controls.Add(Me.nudDefaultReminder)
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.Panel8)
        Me.Panel1.Controls.Add(Me.Panel7)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.txtFileExportPath)
        Me.Panel1.Controls.Add(Me.cmbDefaultEmail)
        Me.Panel1.Controls.Add(Me.txtAdminEmail)
        Me.Panel1.Controls.Add(Me.Panel6)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Panel5)
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.lblChangeDocumentNo)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 94)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1190, 956)
        Me.Panel1.TabIndex = 1
        '
        'Panel20
        '
        Me.Panel20.Controls.Add(Me.rbEmailToUser)
        Me.Panel20.Controls.Add(Me.rbEmailToUserNot)
        Me.Panel20.Location = New System.Drawing.Point(378, 1922)
        Me.Panel20.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel20.Name = "Panel20"
        Me.Panel20.Size = New System.Drawing.Size(608, 52)
        Me.Panel20.TabIndex = 125
        '
        'rbEmailToUser
        '
        Me.rbEmailToUser.AutoSize = True
        Me.rbEmailToUser.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbEmailToUser.Location = New System.Drawing.Point(4, 8)
        Me.rbEmailToUser.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbEmailToUser.Name = "rbEmailToUser"
        Me.rbEmailToUser.Size = New System.Drawing.Size(72, 35)
        Me.rbEmailToUser.TabIndex = 0
        Me.rbEmailToUser.TabStop = True
        Me.rbEmailToUser.Tag = "EmailToUser"
        Me.rbEmailToUser.Text = "Yes"
        Me.ToolTip1.SetToolTip(Me.rbEmailToUser, "Yes")
        Me.rbEmailToUser.UseVisualStyleBackColor = True
        '
        'rbEmailToUserNot
        '
        Me.rbEmailToUserNot.AutoSize = True
        Me.rbEmailToUserNot.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rbEmailToUserNot.Location = New System.Drawing.Point(135, 8)
        Me.rbEmailToUserNot.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbEmailToUserNot.Name = "rbEmailToUserNot"
        Me.rbEmailToUserNot.Size = New System.Drawing.Size(69, 35)
        Me.rbEmailToUserNot.TabIndex = 1
        Me.rbEmailToUserNot.TabStop = True
        Me.rbEmailToUserNot.Text = "No"
        Me.ToolTip1.SetToolTip(Me.rbEmailToUserNot, "No")
        Me.rbEmailToUserNot.UseVisualStyleBackColor = True
        '
        'Panel21
        '
        Me.Panel21.BackColor = System.Drawing.Color.LemonChiffon
        Me.Panel21.Controls.Add(Me.Label26)
        Me.Panel21.Location = New System.Drawing.Point(378, 1985)
        Me.Panel21.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel21.Name = "Panel21"
        Me.Panel21.Size = New System.Drawing.Size(608, 78)
        Me.Panel21.TabIndex = 126
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label26.Location = New System.Drawing.Point(4, 23)
        Me.Label26.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(336, 28)
        Me.Label26.TabIndex = 0
        Me.Label26.Text = "Email should be sent to user or other."
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label27.Location = New System.Drawing.Point(38, 1929)
        Me.Label27.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(158, 32)
        Me.Label27.TabIndex = 124
        Me.Label27.Text = "Email To User"
        '
        'Panel19
        '
        Me.Panel19.BackColor = System.Drawing.Color.LemonChiffon
        Me.Panel19.Controls.Add(Me.Label25)
        Me.Panel19.Location = New System.Drawing.Point(382, 1834)
        Me.Panel19.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel19.Name = "Panel19"
        Me.Panel19.Size = New System.Drawing.Size(608, 78)
        Me.Panel19.TabIndex = 123
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label25.Location = New System.Drawing.Point(4, 12)
        Me.Label25.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(525, 28)
        Me.Label25.TabIndex = 0
        Me.Label25.Text = "Users which you want send follow up email on sales invoice"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label24.Location = New System.Drawing.Point(38, 1615)
        Me.Label24.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(247, 32)
        Me.Label24.TabIndex = 122
        Me.Label24.Text = "Follow up Email Users"
        '
        'lstEmailUsers
        '
        Me.lstEmailUsers.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstEmailUsers.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstEmailUsers.BackColor = System.Drawing.Color.Transparent
        Me.lstEmailUsers.disableWhenChecked = False
        Me.lstEmailUsers.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lstEmailUsers.HeadingLabelName = "lstEmpDepartment"
        Me.lstEmailUsers.HeadingText = ""
        Me.lstEmailUsers.Location = New System.Drawing.Point(386, 1595)
        Me.lstEmailUsers.Margin = New System.Windows.Forms.Padding(9, 9, 9, 9)
        Me.lstEmailUsers.Name = "lstEmailUsers"
        Me.lstEmailUsers.ShowAddNewButton = False
        Me.lstEmailUsers.ShowInverse = True
        Me.lstEmailUsers.ShowMagnifierButton = False
        Me.lstEmailUsers.ShowNoCheck = False
        Me.lstEmailUsers.ShowResetAllButton = False
        Me.lstEmailUsers.ShowSelectall = True
        Me.lstEmailUsers.Size = New System.Drawing.Size(432, 272)
        Me.lstEmailUsers.TabIndex = 121
        Me.lstEmailUsers.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'Panel18
        '
        Me.Panel18.BackColor = System.Drawing.Color.White
        Me.Panel18.Controls.Add(Me.Label23)
        Me.Panel18.Location = New System.Drawing.Point(380, 1503)
        Me.Panel18.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel18.Name = "Panel18"
        Me.Panel18.Size = New System.Drawing.Size(608, 78)
        Me.Panel18.TabIndex = 119
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label23.Location = New System.Drawing.Point(4, 12)
        Me.Label23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(549, 56)
        Me.Label23.TabIndex = 0
        Me.Label23.Text = "You can specify the time on which we start follow up email on " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "sales invoice"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label20.Location = New System.Drawing.Point(682, 1458)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(92, 28)
        Me.Label20.TabIndex = 118
        Me.Label20.Text = "End Time"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Font = New System.Drawing.Font("Segoe UI", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label21.Location = New System.Drawing.Point(369, 1458)
        Me.Label21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(100, 28)
        Me.Label21.TabIndex = 117
        Me.Label21.Text = "Start Time"
        '
        'dtpEndat
        '
        Me.dtpEndat.CustomFormat = "hh:mm:ss tt"
        Me.dtpEndat.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpEndat.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpEndat.Location = New System.Drawing.Point(807, 1455)
        Me.dtpEndat.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpEndat.Name = "dtpEndat"
        Me.dtpEndat.Size = New System.Drawing.Size(181, 33)
        Me.dtpEndat.TabIndex = 116
        Me.dtpEndat.Tag = "FollowUpEmailTime"
        Me.ToolTip1.SetToolTip(Me.dtpEndat, "End Time")
        Me.dtpEndat.Value = New Date(2016, 1, 6, 13, 0, 0, 0)
        '
        'dtpStartat
        '
        Me.dtpStartat.CustomFormat = "hh:mm:ss tt"
        Me.dtpStartat.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpStartat.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStartat.Location = New System.Drawing.Point(494, 1455)
        Me.dtpStartat.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpStartat.Name = "dtpStartat"
        Me.dtpStartat.Size = New System.Drawing.Size(181, 33)
        Me.dtpStartat.TabIndex = 115
        Me.dtpStartat.Tag = "FollowUpEmailTime"
        Me.ToolTip1.SetToolTip(Me.dtpStartat, "Start Time")
        Me.dtpStartat.Value = New Date(2016, 1, 6, 13, 0, 0, 0)
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label22.Location = New System.Drawing.Point(32, 1463)
        Me.Label22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(243, 32)
        Me.Label22.TabIndex = 114
        Me.Label22.Text = "Follow up Email Time"
        '
        'Panel16
        '
        Me.Panel16.Controls.Add(Me.chkAutoFollowupEmail)
        Me.Panel16.Controls.Add(Me.chkAutoFollowupEmailNot)
        Me.Panel16.Location = New System.Drawing.Point(380, 1306)
        Me.Panel16.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel16.Name = "Panel16"
        Me.Panel16.Size = New System.Drawing.Size(608, 52)
        Me.Panel16.TabIndex = 101
        '
        'chkAutoFollowupEmail
        '
        Me.chkAutoFollowupEmail.AutoSize = True
        Me.chkAutoFollowupEmail.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAutoFollowupEmail.Location = New System.Drawing.Point(4, 8)
        Me.chkAutoFollowupEmail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkAutoFollowupEmail.Name = "chkAutoFollowupEmail"
        Me.chkAutoFollowupEmail.Size = New System.Drawing.Size(72, 35)
        Me.chkAutoFollowupEmail.TabIndex = 0
        Me.chkAutoFollowupEmail.TabStop = True
        Me.chkAutoFollowupEmail.Tag = "AutoFollowUpEmail"
        Me.chkAutoFollowupEmail.Text = "Yes"
        Me.ToolTip1.SetToolTip(Me.chkAutoFollowupEmail, "Yes")
        Me.chkAutoFollowupEmail.UseVisualStyleBackColor = True
        '
        'chkAutoFollowupEmailNot
        '
        Me.chkAutoFollowupEmailNot.AutoSize = True
        Me.chkAutoFollowupEmailNot.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAutoFollowupEmailNot.Location = New System.Drawing.Point(135, 8)
        Me.chkAutoFollowupEmailNot.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkAutoFollowupEmailNot.Name = "chkAutoFollowupEmailNot"
        Me.chkAutoFollowupEmailNot.Size = New System.Drawing.Size(69, 35)
        Me.chkAutoFollowupEmailNot.TabIndex = 1
        Me.chkAutoFollowupEmailNot.TabStop = True
        Me.chkAutoFollowupEmailNot.Text = "No"
        Me.ToolTip1.SetToolTip(Me.chkAutoFollowupEmailNot, "No")
        Me.chkAutoFollowupEmailNot.UseVisualStyleBackColor = True
        '
        'Panel17
        '
        Me.Panel17.BackColor = System.Drawing.Color.White
        Me.Panel17.Controls.Add(Me.Label15)
        Me.Panel17.Location = New System.Drawing.Point(380, 1365)
        Me.Panel17.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel17.Name = "Panel17"
        Me.Panel17.Size = New System.Drawing.Size(608, 78)
        Me.Panel17.TabIndex = 102
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label15.Location = New System.Drawing.Point(4, 23)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(294, 28)
        Me.Label15.TabIndex = 0
        Me.Label15.Text = "auto send email on Sales Invoice"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label16.Location = New System.Drawing.Point(38, 1315)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(337, 64)
        Me.Label16.TabIndex = 100
        Me.Label16.Text = "Auto Follow up Email on Sales" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Invoice"
        '
        'Panel14
        '
        Me.Panel14.Controls.Add(Me.chkEmailNotificationOnApproval)
        Me.Panel14.Controls.Add(Me.chkEmailNotificationOnApprovalNot)
        Me.Panel14.Location = New System.Drawing.Point(374, 1160)
        Me.Panel14.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(608, 52)
        Me.Panel14.TabIndex = 98
        '
        'chkEmailNotificationOnApproval
        '
        Me.chkEmailNotificationOnApproval.AutoSize = True
        Me.chkEmailNotificationOnApproval.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEmailNotificationOnApproval.Location = New System.Drawing.Point(4, 8)
        Me.chkEmailNotificationOnApproval.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkEmailNotificationOnApproval.Name = "chkEmailNotificationOnApproval"
        Me.chkEmailNotificationOnApproval.Size = New System.Drawing.Size(72, 35)
        Me.chkEmailNotificationOnApproval.TabIndex = 0
        Me.chkEmailNotificationOnApproval.TabStop = True
        Me.chkEmailNotificationOnApproval.Tag = "EmailNotificationOnApproval"
        Me.chkEmailNotificationOnApproval.Text = "Yes"
        Me.ToolTip1.SetToolTip(Me.chkEmailNotificationOnApproval, "Yes")
        Me.chkEmailNotificationOnApproval.UseVisualStyleBackColor = True
        '
        'chkEmailNotificationOnApprovalNot
        '
        Me.chkEmailNotificationOnApprovalNot.AutoSize = True
        Me.chkEmailNotificationOnApprovalNot.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEmailNotificationOnApprovalNot.Location = New System.Drawing.Point(135, 8)
        Me.chkEmailNotificationOnApprovalNot.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkEmailNotificationOnApprovalNot.Name = "chkEmailNotificationOnApprovalNot"
        Me.chkEmailNotificationOnApprovalNot.Size = New System.Drawing.Size(69, 35)
        Me.chkEmailNotificationOnApprovalNot.TabIndex = 1
        Me.chkEmailNotificationOnApprovalNot.TabStop = True
        Me.chkEmailNotificationOnApprovalNot.Text = "No"
        Me.ToolTip1.SetToolTip(Me.chkEmailNotificationOnApprovalNot, "No")
        Me.chkEmailNotificationOnApprovalNot.UseVisualStyleBackColor = True
        '
        'Panel15
        '
        Me.Panel15.BackColor = System.Drawing.Color.White
        Me.Panel15.Controls.Add(Me.Label14)
        Me.Panel15.Location = New System.Drawing.Point(374, 1218)
        Me.Panel15.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(608, 78)
        Me.Panel15.TabIndex = 99
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label14.Location = New System.Drawing.Point(4, 23)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(305, 28)
        Me.Label14.TabIndex = 0
        Me.Label14.Text = "set to save email attachment path"
        '
        'lblEmailNotificationOnApproval
        '
        Me.lblEmailNotificationOnApproval.AutoSize = True
        Me.lblEmailNotificationOnApproval.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEmailNotificationOnApproval.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblEmailNotificationOnApproval.Location = New System.Drawing.Point(32, 1169)
        Me.lblEmailNotificationOnApproval.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblEmailNotificationOnApproval.Name = "lblEmailNotificationOnApproval"
        Me.lblEmailNotificationOnApproval.Size = New System.Drawing.Size(345, 32)
        Me.lblEmailNotificationOnApproval.TabIndex = 97
        Me.lblEmailNotificationOnApproval.Text = "Email Notification On Approval"
        '
        'Panel12
        '
        Me.Panel12.Controls.Add(Me.chkAutoEmail)
        Me.Panel12.Controls.Add(Me.chkAutoEmailNot)
        Me.Panel12.Location = New System.Drawing.Point(375, 1017)
        Me.Panel12.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(608, 52)
        Me.Panel12.TabIndex = 95
        '
        'chkAutoEmail
        '
        Me.chkAutoEmail.AutoSize = True
        Me.chkAutoEmail.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAutoEmail.Location = New System.Drawing.Point(4, 8)
        Me.chkAutoEmail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkAutoEmail.Name = "chkAutoEmail"
        Me.chkAutoEmail.Size = New System.Drawing.Size(72, 35)
        Me.chkAutoEmail.TabIndex = 0
        Me.chkAutoEmail.TabStop = True
        Me.chkAutoEmail.Tag = "AutoEmail"
        Me.chkAutoEmail.Text = "Yes"
        Me.ToolTip1.SetToolTip(Me.chkAutoEmail, "Yes")
        Me.chkAutoEmail.UseVisualStyleBackColor = True
        '
        'chkAutoEmailNot
        '
        Me.chkAutoEmailNot.AutoSize = True
        Me.chkAutoEmailNot.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAutoEmailNot.Location = New System.Drawing.Point(135, 8)
        Me.chkAutoEmailNot.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkAutoEmailNot.Name = "chkAutoEmailNot"
        Me.chkAutoEmailNot.Size = New System.Drawing.Size(69, 35)
        Me.chkAutoEmailNot.TabIndex = 1
        Me.chkAutoEmailNot.TabStop = True
        Me.chkAutoEmailNot.Text = "No"
        Me.ToolTip1.SetToolTip(Me.chkAutoEmailNot, "No")
        Me.chkAutoEmailNot.UseVisualStyleBackColor = True
        '
        'Panel13
        '
        Me.Panel13.BackColor = System.Drawing.Color.White
        Me.Panel13.Controls.Add(Me.Label13)
        Me.Panel13.Location = New System.Drawing.Point(375, 1075)
        Me.Panel13.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel13.Name = "Panel13"
        Me.Panel13.Size = New System.Drawing.Size(608, 78)
        Me.Panel13.TabIndex = 96
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label13.Location = New System.Drawing.Point(4, 23)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(305, 28)
        Me.Label13.TabIndex = 0
        Me.Label13.Text = "set to save email attachment path"
        '
        'lblAutoEmail
        '
        Me.lblAutoEmail.AutoSize = True
        Me.lblAutoEmail.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAutoEmail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblAutoEmail.Location = New System.Drawing.Point(32, 1029)
        Me.lblAutoEmail.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAutoEmail.Name = "lblAutoEmail"
        Me.lblAutoEmail.Size = New System.Drawing.Size(130, 32)
        Me.lblAutoEmail.TabIndex = 94
        Me.lblAutoEmail.Text = "Auto Email"
        '
        'Panel11
        '
        Me.Panel11.Controls.Add(Me.chkEmailAlert)
        Me.Panel11.Controls.Add(Me.chkEmailAlertNot)
        Me.Panel11.Location = New System.Drawing.Point(376, 697)
        Me.Panel11.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel11.Name = "Panel11"
        Me.Panel11.Size = New System.Drawing.Size(608, 54)
        Me.Panel11.TabIndex = 9
        '
        'chkEmailAlert
        '
        Me.chkEmailAlert.AutoSize = True
        Me.chkEmailAlert.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEmailAlert.Location = New System.Drawing.Point(4, 8)
        Me.chkEmailAlert.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkEmailAlert.Name = "chkEmailAlert"
        Me.chkEmailAlert.Size = New System.Drawing.Size(72, 35)
        Me.chkEmailAlert.TabIndex = 0
        Me.chkEmailAlert.TabStop = True
        Me.chkEmailAlert.Tag = "EmailAlert"
        Me.chkEmailAlert.Text = "Yes"
        Me.ToolTip1.SetToolTip(Me.chkEmailAlert, "Yes")
        Me.chkEmailAlert.UseVisualStyleBackColor = True
        '
        'chkEmailAlertNot
        '
        Me.chkEmailAlertNot.AutoSize = True
        Me.chkEmailAlertNot.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEmailAlertNot.Location = New System.Drawing.Point(135, 8)
        Me.chkEmailAlertNot.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkEmailAlertNot.Name = "chkEmailAlertNot"
        Me.chkEmailAlertNot.Size = New System.Drawing.Size(69, 35)
        Me.chkEmailAlertNot.TabIndex = 1
        Me.chkEmailAlertNot.TabStop = True
        Me.chkEmailAlertNot.Text = "No"
        Me.ToolTip1.SetToolTip(Me.chkEmailAlertNot, "No")
        Me.chkEmailAlertNot.UseVisualStyleBackColor = True
        '
        'Panel10
        '
        Me.Panel10.Controls.Add(Me.chkAttachments)
        Me.Panel10.Controls.Add(Me.chkAttachmentsNot)
        Me.Panel10.Location = New System.Drawing.Point(376, 554)
        Me.Panel10.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(608, 52)
        Me.Panel10.TabIndex = 7
        '
        'chkAttachments
        '
        Me.chkAttachments.AutoSize = True
        Me.chkAttachments.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAttachments.Location = New System.Drawing.Point(4, 8)
        Me.chkAttachments.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkAttachments.Name = "chkAttachments"
        Me.chkAttachments.Size = New System.Drawing.Size(72, 35)
        Me.chkAttachments.TabIndex = 0
        Me.chkAttachments.TabStop = True
        Me.chkAttachments.Tag = "EmailAttachment"
        Me.chkAttachments.Text = "Yes"
        Me.ToolTip1.SetToolTip(Me.chkAttachments, "Yes")
        Me.chkAttachments.UseVisualStyleBackColor = True
        '
        'chkAttachmentsNot
        '
        Me.chkAttachmentsNot.AutoSize = True
        Me.chkAttachmentsNot.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkAttachmentsNot.Location = New System.Drawing.Point(135, 8)
        Me.chkAttachmentsNot.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkAttachmentsNot.Name = "chkAttachmentsNot"
        Me.chkAttachmentsNot.Size = New System.Drawing.Size(69, 35)
        Me.chkAttachmentsNot.TabIndex = 1
        Me.chkAttachmentsNot.TabStop = True
        Me.chkAttachmentsNot.Text = "No"
        Me.ToolTip1.SetToolTip(Me.chkAttachmentsNot, "No")
        Me.chkAttachmentsNot.UseVisualStyleBackColor = True
        '
        'Panel9
        '
        Me.Panel9.BackColor = System.Drawing.Color.White
        Me.Panel9.Controls.Add(Me.Label19)
        Me.Panel9.Location = New System.Drawing.Point(376, 931)
        Me.Panel9.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(608, 78)
        Me.Panel9.TabIndex = 93
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label19.Location = New System.Drawing.Point(4, 25)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(318, 28)
        Me.Label19.TabIndex = 0
        Me.Label19.Text = "sent email by default of attendance"
        '
        'nudDefaultReminder
        '
        Me.nudDefaultReminder.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nudDefaultReminder.Location = New System.Drawing.Point(376, 868)
        Me.nudDefaultReminder.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.nudDefaultReminder.Name = "nudDefaultReminder"
        Me.nudDefaultReminder.Size = New System.Drawing.Size(60, 37)
        Me.nudDefaultReminder.TabIndex = 11
        Me.nudDefaultReminder.Tag = "DefaultReminder"
        Me.ToolTip1.SetToolTip(Me.nudDefaultReminder, "Default Reminder")
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(32, 868)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(202, 32)
        Me.Label11.TabIndex = 10
        Me.Label11.Text = "Default Reminder"
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.White
        Me.Panel8.Controls.Add(Me.Label9)
        Me.Panel8.Location = New System.Drawing.Point(376, 755)
        Me.Panel8.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(608, 78)
        Me.Panel8.TabIndex = 92
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(4, 25)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(396, 28)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "configuration to on auto email of document"
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.White
        Me.Panel7.Controls.Add(Me.Label8)
        Me.Panel7.Location = New System.Drawing.Point(376, 612)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(608, 78)
        Me.Panel7.TabIndex = 18
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(4, 23)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(305, 28)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "set to save email attachment path"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(32, 709)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(129, 32)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "Email Alert"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(32, 566)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(203, 32)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Email Attachment"
        '
        'txtFileExportPath
        '
        Me.txtFileExportPath.AcceptsReturn = True
        Me.txtFileExportPath.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFileExportPath.Location = New System.Drawing.Point(376, 403)
        Me.txtFileExportPath.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtFileExportPath.Name = "txtFileExportPath"
        Me.txtFileExportPath.Size = New System.Drawing.Size(517, 37)
        Me.txtFileExportPath.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.txtFileExportPath, "File Export Path")
        '
        'cmbDefaultEmail
        '
        Me.cmbDefaultEmail.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbDefaultEmail.FormattingEnabled = True
        Me.cmbDefaultEmail.Location = New System.Drawing.Point(376, 235)
        Me.cmbDefaultEmail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbDefaultEmail.Name = "cmbDefaultEmail"
        Me.cmbDefaultEmail.Size = New System.Drawing.Size(517, 39)
        Me.cmbDefaultEmail.TabIndex = 3
        Me.cmbDefaultEmail.Tag = "DefaultEmailId"
        Me.ToolTip1.SetToolTip(Me.cmbDefaultEmail, "Default Email")
        '
        'txtAdminEmail
        '
        Me.txtAdminEmail.Location = New System.Drawing.Point(376, 29)
        Me.txtAdminEmail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtAdminEmail.Multiline = True
        Me.txtAdminEmail.Name = "txtAdminEmail"
        Me.txtAdminEmail.Size = New System.Drawing.Size(517, 87)
        Me.txtAdminEmail.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtAdminEmail, "Enter Admin Email Address")
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.White
        Me.Panel6.Controls.Add(Me.Label6)
        Me.Panel6.Location = New System.Drawing.Point(376, 468)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(608, 78)
        Me.Panel6.TabIndex = 17
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(4, 23)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(196, 28)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "set email export path"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(32, 398)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(179, 32)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "File Export Path"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(32, 238)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(157, 32)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Default Email"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.White
        Me.Panel5.Controls.Add(Me.Label3)
        Me.Panel5.Location = New System.Drawing.Point(376, 300)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(608, 78)
        Me.Panel5.TabIndex = 16
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(4, 25)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(155, 28)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "set default email"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.White
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Location = New System.Drawing.Point(376, 137)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(608, 78)
        Me.Panel4.TabIndex = 13
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(6, 25)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(149, 28)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "set admin email"
        '
        'lblChangeDocumentNo
        '
        Me.lblChangeDocumentNo.AutoSize = True
        Me.lblChangeDocumentNo.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChangeDocumentNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblChangeDocumentNo.Location = New System.Drawing.Point(38, 29)
        Me.lblChangeDocumentNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblChangeDocumentNo.Name = "lblChangeDocumentNo"
        Me.lblChangeDocumentNo.Size = New System.Drawing.Size(149, 32)
        Me.lblChangeDocumentNo.TabIndex = 0
        Me.lblChangeDocumentNo.Text = "Admin Email"
        '
        'frmConfigEmail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1512, 1050)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel2)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmConfigEmail"
        Me.Text = "Email Configuration"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel20.ResumeLayout(False)
        Me.Panel20.PerformLayout()
        Me.Panel21.ResumeLayout(False)
        Me.Panel21.PerformLayout()
        Me.Panel19.ResumeLayout(False)
        Me.Panel19.PerformLayout()
        Me.Panel18.ResumeLayout(False)
        Me.Panel18.PerformLayout()
        Me.Panel16.ResumeLayout(False)
        Me.Panel16.PerformLayout()
        Me.Panel17.ResumeLayout(False)
        Me.Panel17.PerformLayout()
        Me.Panel14.ResumeLayout(False)
        Me.Panel14.PerformLayout()
        Me.Panel15.ResumeLayout(False)
        Me.Panel15.PerformLayout()
        Me.Panel12.ResumeLayout(False)
        Me.Panel12.PerformLayout()
        Me.Panel13.ResumeLayout(False)
        Me.Panel13.PerformLayout()
        Me.Panel11.ResumeLayout(False)
        Me.Panel11.PerformLayout()
        Me.Panel10.ResumeLayout(False)
        Me.Panel10.PerformLayout()
        Me.Panel9.ResumeLayout(False)
        Me.Panel9.PerformLayout()
        CType(Me.nudDefaultReminder, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents linkDB As System.Windows.Forms.Label
    Friend WithEvents linkPath As System.Windows.Forms.Label
    Friend WithEvents linkCompany As System.Windows.Forms.Label
    Friend WithEvents linkSMS As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblChangeDocumentNo As System.Windows.Forms.Label
    Friend WithEvents txtAdminEmail As System.Windows.Forms.TextBox
    Friend WithEvents cmbDefaultEmail As System.Windows.Forms.ComboBox
    Friend WithEvents txtFileExportPath As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents chkAttachmentsNot As System.Windows.Forms.RadioButton
    Friend WithEvents chkAttachments As System.Windows.Forms.RadioButton
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents chkEmailAlertNot As System.Windows.Forms.RadioButton
    Friend WithEvents chkEmailAlert As System.Windows.Forms.RadioButton
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents nudDefaultReminder As System.Windows.Forms.NumericUpDown
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents Panel11 As System.Windows.Forms.Panel
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Panel12 As System.Windows.Forms.Panel
    Friend WithEvents chkAutoEmail As System.Windows.Forms.RadioButton
    Friend WithEvents chkAutoEmailNot As System.Windows.Forms.RadioButton
    Friend WithEvents Panel13 As System.Windows.Forms.Panel
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents lblAutoEmail As System.Windows.Forms.Label
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents chkEmailNotificationOnApproval As System.Windows.Forms.RadioButton
    Friend WithEvents chkEmailNotificationOnApprovalNot As System.Windows.Forms.RadioButton
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents lblEmailNotificationOnApproval As System.Windows.Forms.Label
    Friend WithEvents Panel16 As System.Windows.Forms.Panel
    Friend WithEvents chkAutoFollowupEmail As System.Windows.Forms.RadioButton
    Friend WithEvents chkAutoFollowupEmailNot As System.Windows.Forms.RadioButton
    Friend WithEvents Panel17 As System.Windows.Forms.Panel
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents dtpEndat As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpStartat As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Panel18 As System.Windows.Forms.Panel
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents lstEmailUsers As SimpleAccounts.uiListControl
    Friend WithEvents Panel19 As System.Windows.Forms.Panel
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents Panel20 As System.Windows.Forms.Panel
    Friend WithEvents rbEmailToUser As System.Windows.Forms.RadioButton
    Friend WithEvents rbEmailToUserNot As System.Windows.Forms.RadioButton
    Friend WithEvents Panel21 As System.Windows.Forms.Panel
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents Label27 As System.Windows.Forms.Label
End Class

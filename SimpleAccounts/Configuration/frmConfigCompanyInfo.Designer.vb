<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfigCompanyInfo
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.chkCompanyWisePrefixOnVoucherNot = New System.Windows.Forms.RadioButton()
        Me.chkCompanyWisePrefixOnVoucher = New System.Windows.Forms.RadioButton()
        Me.lblChangeDocumentNo = New System.Windows.Forms.Label()
        Me.txtCompanyAddress = New System.Windows.Forms.TextBox()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.linkCompany = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.linkDB = New System.Windows.Forms.Label()
        Me.linkPath = New System.Windows.Forms.Label()
        Me.linkSMS = New System.Windows.Forms.Label()
        Me.linkSecurityRights = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.pbCompanyLogo = New System.Windows.Forms.PictureBox()
        Me.btnBrowseCompanyLogo = New System.Windows.Forms.Button()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtCompanyLogo = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtPartnerName = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtCompanyName = New System.Windows.Forms.TextBox()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.cmbMainMenuNavigatorColor = New System.Windows.Forms.ComboBox()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.pbCompanyLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel8.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel14.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(913, 61)
        Me.Panel2.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(12, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(277, 37)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Company Information"
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.cmbMainMenuNavigatorColor)
        Me.Panel1.Controls.Add(Me.Panel14)
        Me.Panel1.Controls.Add(Me.Label24)
        Me.Panel1.Controls.Add(Me.Panel7)
        Me.Panel1.Controls.Add(Me.Panel6)
        Me.Panel1.Controls.Add(Me.chkCompanyWisePrefixOnVoucherNot)
        Me.Panel1.Controls.Add(Me.chkCompanyWisePrefixOnVoucher)
        Me.Panel1.Controls.Add(Me.lblChangeDocumentNo)
        Me.Panel1.Controls.Add(Me.txtCompanyAddress)
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.pbCompanyLogo)
        Me.Panel1.Controls.Add(Me.btnBrowseCompanyLogo)
        Me.Panel1.Controls.Add(Me.Panel8)
        Me.Panel1.Controls.Add(Me.txtCompanyLogo)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.txtPartnerName)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.txtCompanyName)
        Me.Panel1.Controls.Add(Me.Panel5)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 61)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(913, 566)
        Me.Panel1.TabIndex = 1
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.LemonChiffon
        Me.Panel7.Controls.Add(Me.Label7)
        Me.Panel7.Location = New System.Drawing.Point(251, 151)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(405, 51)
        Me.Panel7.TabIndex = 118
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(3, 15)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(93, 17)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "name for lable"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.LemonChiffon
        Me.Panel6.Controls.Add(Me.Label6)
        Me.Panel6.Location = New System.Drawing.Point(251, 339)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(405, 51)
        Me.Panel6.TabIndex = 117
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(4, 16)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(323, 17)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "use to serial number of sales according to commpany"
        '
        'chkCompanyWisePrefixOnVoucherNot
        '
        Me.chkCompanyWisePrefixOnVoucherNot.AutoSize = True
        Me.chkCompanyWisePrefixOnVoucherNot.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCompanyWisePrefixOnVoucherNot.Location = New System.Drawing.Point(338, 309)
        Me.chkCompanyWisePrefixOnVoucherNot.Name = "chkCompanyWisePrefixOnVoucherNot"
        Me.chkCompanyWisePrefixOnVoucherNot.Size = New System.Drawing.Size(47, 24)
        Me.chkCompanyWisePrefixOnVoucherNot.TabIndex = 8
        Me.chkCompanyWisePrefixOnVoucherNot.TabStop = True
        Me.chkCompanyWisePrefixOnVoucherNot.Text = "No"
        Me.ToolTip1.SetToolTip(Me.chkCompanyWisePrefixOnVoucherNot, "No")
        Me.chkCompanyWisePrefixOnVoucherNot.UseVisualStyleBackColor = True
        '
        'chkCompanyWisePrefixOnVoucher
        '
        Me.chkCompanyWisePrefixOnVoucher.AutoSize = True
        Me.chkCompanyWisePrefixOnVoucher.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCompanyWisePrefixOnVoucher.Location = New System.Drawing.Point(251, 309)
        Me.chkCompanyWisePrefixOnVoucher.Name = "chkCompanyWisePrefixOnVoucher"
        Me.chkCompanyWisePrefixOnVoucher.Size = New System.Drawing.Size(48, 24)
        Me.chkCompanyWisePrefixOnVoucher.TabIndex = 7
        Me.chkCompanyWisePrefixOnVoucher.TabStop = True
        Me.chkCompanyWisePrefixOnVoucher.Tag = "CompanyWisePrefix"
        Me.chkCompanyWisePrefixOnVoucher.Text = "Yes"
        Me.ToolTip1.SetToolTip(Me.chkCompanyWisePrefixOnVoucher, "Yes")
        Me.chkCompanyWisePrefixOnVoucher.UseVisualStyleBackColor = True
        '
        'lblChangeDocumentNo
        '
        Me.lblChangeDocumentNo.AutoSize = True
        Me.lblChangeDocumentNo.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblChangeDocumentNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblChangeDocumentNo.Location = New System.Drawing.Point(45, 309)
        Me.lblChangeDocumentNo.Name = "lblChangeDocumentNo"
        Me.lblChangeDocumentNo.Size = New System.Drawing.Size(155, 21)
        Me.lblChangeDocumentNo.TabIndex = 6
        Me.lblChangeDocumentNo.Text = "Company wise prefix"
        '
        'txtCompanyAddress
        '
        Me.txtCompanyAddress.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyAddress.Location = New System.Drawing.Point(251, 209)
        Me.txtCompanyAddress.Name = "txtCompanyAddress"
        Me.txtCompanyAddress.Size = New System.Drawing.Size(393, 27)
        Me.txtCompanyAddress.TabIndex = 5
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.LemonChiffon
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Location = New System.Drawing.Point(251, 242)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(405, 51)
        Me.Panel4.TabIndex = 113
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(3, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(252, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "set default company address for print out"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(45, 209)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(137, 21)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Company Address"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.linkCompany)
        Me.Panel3.Controls.Add(Me.Label17)
        Me.Panel3.Controls.Add(Me.Label18)
        Me.Panel3.Controls.Add(Me.linkDB)
        Me.Panel3.Controls.Add(Me.linkPath)
        Me.Panel3.Controls.Add(Me.linkSMS)
        Me.Panel3.Controls.Add(Me.linkSecurityRights)
        Me.Panel3.Controls.Add(Me.Label12)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(681, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(215, 576)
        Me.Panel3.TabIndex = 12
        '
        'linkCompany
        '
        Me.linkCompany.AutoSize = True
        Me.linkCompany.Cursor = System.Windows.Forms.Cursors.Hand
        Me.linkCompany.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.linkCompany.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.linkCompany.Location = New System.Drawing.Point(20, 225)
        Me.linkCompany.Name = "linkCompany"
        Me.linkCompany.Size = New System.Drawing.Size(64, 21)
        Me.linkCompany.TabIndex = 5
        Me.linkCompany.Text = "General"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Label17.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label17.Location = New System.Drawing.Point(20, 315)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(85, 21)
        Me.Label17.TabIndex = 7
        Me.Label17.Text = "Contact Us"
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label18.Location = New System.Drawing.Point(20, 269)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(146, 21)
        Me.Label18.TabIndex = 6
        Me.Label18.Text = "Have a Question(s)"
        '
        'linkDB
        '
        Me.linkDB.AutoSize = True
        Me.linkDB.Cursor = System.Windows.Forms.Cursors.Hand
        Me.linkDB.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.linkDB.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.linkDB.Location = New System.Drawing.Point(20, 180)
        Me.linkDB.Name = "linkDB"
        Me.linkDB.Size = New System.Drawing.Size(128, 21)
        Me.linkDB.TabIndex = 4
        Me.linkDB.Text = "Database Backup"
        '
        'linkPath
        '
        Me.linkPath.AutoSize = True
        Me.linkPath.Cursor = System.Windows.Forms.Cursors.Hand
        Me.linkPath.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.linkPath.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.linkPath.Location = New System.Drawing.Point(20, 140)
        Me.linkPath.Name = "linkPath"
        Me.linkPath.Size = New System.Drawing.Size(100, 21)
        Me.linkPath.TabIndex = 3
        Me.linkPath.Text = "Path Settings"
        '
        'linkSMS
        '
        Me.linkSMS.AutoSize = True
        Me.linkSMS.Cursor = System.Windows.Forms.Cursors.Hand
        Me.linkSMS.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.linkSMS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.linkSMS.Location = New System.Drawing.Point(20, 100)
        Me.linkSMS.Name = "linkSMS"
        Me.linkSMS.Size = New System.Drawing.Size(42, 21)
        Me.linkSMS.TabIndex = 2
        Me.linkSMS.Text = "SMS"
        '
        'linkSecurityRights
        '
        Me.linkSecurityRights.AutoSize = True
        Me.linkSecurityRights.Cursor = System.Windows.Forms.Cursors.Hand
        Me.linkSecurityRights.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.linkSecurityRights.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.linkSecurityRights.Location = New System.Drawing.Point(20, 60)
        Me.linkSecurityRights.Name = "linkSecurityRights"
        Me.linkSecurityRights.Size = New System.Drawing.Size(114, 21)
        Me.linkSecurityRights.TabIndex = 1
        Me.linkSecurityRights.Text = "Security Rights"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Segoe UI Semibold", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(20, 14)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(130, 21)
        Me.Label12.TabIndex = 0
        Me.Label12.Text = "Related Settings"
        '
        'pbCompanyLogo
        '
        Me.pbCompanyLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.pbCompanyLogo.ErrorImage = Nothing
        Me.pbCompanyLogo.InitialImage = Nothing
        Me.pbCompanyLogo.Location = New System.Drawing.Point(53, 441)
        Me.pbCompanyLogo.Name = "pbCompanyLogo"
        Me.pbCompanyLogo.Size = New System.Drawing.Size(127, 43)
        Me.pbCompanyLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbCompanyLogo.TabIndex = 109
        Me.pbCompanyLogo.TabStop = False
        Me.pbCompanyLogo.Visible = False
        '
        'btnBrowseCompanyLogo
        '
        Me.btnBrowseCompanyLogo.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.btnBrowseCompanyLogo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseCompanyLogo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowseCompanyLogo.ForeColor = System.Drawing.Color.White
        Me.btnBrowseCompanyLogo.Location = New System.Drawing.Point(571, 406)
        Me.btnBrowseCompanyLogo.Name = "btnBrowseCompanyLogo"
        Me.btnBrowseCompanyLogo.Size = New System.Drawing.Size(75, 27)
        Me.btnBrowseCompanyLogo.TabIndex = 11
        Me.btnBrowseCompanyLogo.Text = "Browse"
        Me.btnBrowseCompanyLogo.UseVisualStyleBackColor = False
        '
        'Panel8
        '
        Me.Panel8.BackColor = System.Drawing.Color.LemonChiffon
        Me.Panel8.Controls.Add(Me.Label11)
        Me.Panel8.Location = New System.Drawing.Point(253, 444)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(405, 40)
        Me.Panel8.TabIndex = 22
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(3, 8)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(264, 17)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Select your company logo to set on page(s)"
        '
        'txtCompanyLogo
        '
        Me.txtCompanyLogo.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyLogo.Location = New System.Drawing.Point(253, 406)
        Me.txtCompanyLogo.Name = "txtCompanyLogo"
        Me.txtCompanyLogo.ReadOnly = True
        Me.txtCompanyLogo.Size = New System.Drawing.Size(291, 27)
        Me.txtCompanyLogo.TabIndex = 10
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(47, 406)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(163, 21)
        Me.Label10.TabIndex = 9
        Me.Label10.Text = "Company Logo Image"
        '
        'txtPartnerName
        '
        Me.txtPartnerName.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPartnerName.Location = New System.Drawing.Point(251, 118)
        Me.txtPartnerName.Name = "txtPartnerName"
        Me.txtPartnerName.Size = New System.Drawing.Size(393, 27)
        Me.txtPartnerName.TabIndex = 3
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(45, 118)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(164, 21)
        Me.Label9.TabIndex = 2
        Me.Label9.Text = "Company Label Name"
        '
        'txtCompanyName
        '
        Me.txtCompanyName.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCompanyName.Location = New System.Drawing.Point(252, 20)
        Me.txtCompanyName.Name = "txtCompanyName"
        Me.txtCompanyName.Size = New System.Drawing.Size(393, 27)
        Me.txtCompanyName.TabIndex = 1
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.LemonChiffon
        Me.Panel5.Controls.Add(Me.Label3)
        Me.Panel5.Location = New System.Drawing.Point(252, 53)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(405, 51)
        Me.Panel5.TabIndex = 13
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(3, 15)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(237, 17)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "set default company name for print out"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(46, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(123, 21)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Company Name"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'cmbMainMenuNavigatorColor
        '
        Me.cmbMainMenuNavigatorColor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbMainMenuNavigatorColor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbMainMenuNavigatorColor.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbMainMenuNavigatorColor.FormattingEnabled = True
        Me.cmbMainMenuNavigatorColor.Items.AddRange(New Object() {"Primary", "Red", "Blue", "Green"})
        Me.cmbMainMenuNavigatorColor.Location = New System.Drawing.Point(253, 490)
        Me.cmbMainMenuNavigatorColor.Name = "cmbMainMenuNavigatorColor"
        Me.cmbMainMenuNavigatorColor.Size = New System.Drawing.Size(311, 28)
        Me.cmbMainMenuNavigatorColor.TabIndex = 120
        Me.cmbMainMenuNavigatorColor.TabStop = False
        Me.cmbMainMenuNavigatorColor.Tag = "MainMenuNavigatorColor"
        '
        'Panel14
        '
        Me.Panel14.BackColor = System.Drawing.Color.LemonChiffon
        Me.Panel14.Controls.Add(Me.Label23)
        Me.Panel14.Location = New System.Drawing.Point(253, 525)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(405, 51)
        Me.Panel14.TabIndex = 121
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label23.Location = New System.Drawing.Point(3, 17)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(171, 17)
        Me.Label23.TabIndex = 0
        Me.Label23.Text = "Main menu navigation color"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.Label24.Location = New System.Drawing.Point(50, 497)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(119, 21)
        Me.Label24.TabIndex = 119
        Me.Label24.Text = "Company Color"
        '
        'frmConfigCompanyInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(913, 627)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Name = "frmConfigCompanyInfo"
        Me.Text = "frmConfigCompanyInfo"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.pbCompanyLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel14.ResumeLayout(False)
        Me.Panel14.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pbCompanyLogo As System.Windows.Forms.PictureBox
    Friend WithEvents btnBrowseCompanyLogo As System.Windows.Forms.Button
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtCompanyLogo As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtPartnerName As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtCompanyName As System.Windows.Forms.TextBox
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents linkDB As System.Windows.Forms.Label
    Friend WithEvents linkPath As System.Windows.Forms.Label
    Friend WithEvents linkSMS As System.Windows.Forms.Label
    Friend WithEvents linkSecurityRights As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents linkCompany As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents txtCompanyAddress As System.Windows.Forms.TextBox
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents chkCompanyWisePrefixOnVoucherNot As System.Windows.Forms.RadioButton
    Friend WithEvents chkCompanyWisePrefixOnVoucher As System.Windows.Forms.RadioButton
    Friend WithEvents lblChangeDocumentNo As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbMainMenuNavigatorColor As System.Windows.Forms.ComboBox
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Label24 As System.Windows.Forms.Label
End Class

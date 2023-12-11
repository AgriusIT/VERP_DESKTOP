<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGenerateLicense
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGenerateLicense))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblExpiryDate = New System.Windows.Forms.LinkLabel()
        Me.lblExpiryLabel = New System.Windows.Forms.Label()
        Me.lblAppVer = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblProduct = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.lblDatabaseName = New System.Windows.Forms.Label()
        Me.lblServerName = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblRegistrationKey = New System.Windows.Forms.LinkLabel()
        Me.pnlLicenseUpdate = New System.Windows.Forms.Panel()
        Me.btnSaveKey = New System.Windows.Forms.Button()
        Me.txtLicenseKey = New System.Windows.Forms.MaskedTextBox()
        Me.btnGetFingerPrints = New System.Windows.Forms.Button()
        Me.btnLoadLicense = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.dtpExpiryDate = New System.Windows.Forms.DateTimePicker()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnViewMessageLog = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.lblLicenseKey = New System.Windows.Forms.Label()
        Me.txtDBName = New System.Windows.Forms.TextBox()
        Me.txtUsers = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.pnlLicenseUpdate.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.lblExpiryDate)
        Me.GroupBox1.Controls.Add(Me.lblExpiryLabel)
        Me.GroupBox1.Controls.Add(Me.lblAppVer)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.lblProduct)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.lblVersion)
        Me.GroupBox1.Controls.Add(Me.lblDatabaseName)
        Me.GroupBox1.Controls.Add(Me.lblServerName)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.lblRegistrationKey)
        Me.GroupBox1.Controls.Add(Me.pnlLicenseUpdate)
        Me.GroupBox1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(539, 203)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(167, 128)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Version Information"
        Me.GroupBox1.Visible = False
        '
        'lblExpiryDate
        '
        Me.lblExpiryDate.AutoSize = True
        Me.lblExpiryDate.Location = New System.Drawing.Point(167, 204)
        Me.lblExpiryDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblExpiryDate.Name = "lblExpiryDate"
        Me.lblExpiryDate.Size = New System.Drawing.Size(102, 17)
        Me.lblExpiryDate.TabIndex = 17
        Me.lblExpiryDate.TabStop = True
        Me.lblExpiryDate.Text = "31-Dec-2016"
        '
        'lblExpiryLabel
        '
        Me.lblExpiryLabel.AutoSize = True
        Me.lblExpiryLabel.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExpiryLabel.Location = New System.Drawing.Point(21, 204)
        Me.lblExpiryLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblExpiryLabel.Name = "lblExpiryLabel"
        Me.lblExpiryLabel.Size = New System.Drawing.Size(90, 17)
        Me.lblExpiryLabel.TabIndex = 15
        Me.lblExpiryLabel.Text = "Expiry Date"
        '
        'lblAppVer
        '
        Me.lblAppVer.AutoSize = True
        Me.lblAppVer.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblAppVer.Location = New System.Drawing.Point(167, 118)
        Me.lblAppVer.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAppVer.Name = "lblAppVer"
        Me.lblAppVer.Size = New System.Drawing.Size(0, 17)
        Me.lblAppVer.TabIndex = 11
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(21, 118)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(84, 17)
        Me.Label14.TabIndex = 10
        Me.Label14.Text = "My Version"
        Me.Label14.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(21, 175)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(90, 17)
        Me.Label12.TabIndex = 8
        Me.Label12.Text = "License Key"
        Me.Label12.Visible = False
        '
        'lblProduct
        '
        Me.lblProduct.AutoSize = True
        Me.lblProduct.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProduct.Location = New System.Drawing.Point(167, 146)
        Me.lblProduct.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProduct.Name = "lblProduct"
        Me.lblProduct.Size = New System.Drawing.Size(0, 17)
        Me.lblProduct.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(21, 146)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(62, 17)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Product"
        Me.Label4.Visible = False
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblVersion.Location = New System.Drawing.Point(167, 90)
        Me.lblVersion.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(0, 17)
        Me.lblVersion.TabIndex = 5
        '
        'lblDatabaseName
        '
        Me.lblDatabaseName.AutoSize = True
        Me.lblDatabaseName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDatabaseName.Location = New System.Drawing.Point(167, 62)
        Me.lblDatabaseName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDatabaseName.Name = "lblDatabaseName"
        Me.lblDatabaseName.Size = New System.Drawing.Size(0, 17)
        Me.lblDatabaseName.TabIndex = 4
        '
        'lblServerName
        '
        Me.lblServerName.AutoSize = True
        Me.lblServerName.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblServerName.Location = New System.Drawing.Point(167, 33)
        Me.lblServerName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblServerName.Name = "lblServerName"
        Me.lblServerName.Size = New System.Drawing.Size(0, 17)
        Me.lblServerName.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(21, 90)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(131, 17)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Database Version"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(21, 62)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(118, 17)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Database Name"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(21, 33)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(98, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Server Name"
        '
        'lblRegistrationKey
        '
        Me.lblRegistrationKey.AutoSize = True
        Me.lblRegistrationKey.Location = New System.Drawing.Point(167, 175)
        Me.lblRegistrationKey.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRegistrationKey.Name = "lblRegistrationKey"
        Me.lblRegistrationKey.Size = New System.Drawing.Size(223, 17)
        Me.lblRegistrationKey.TabIndex = 19
        Me.lblRegistrationKey.TabStop = True
        Me.lblRegistrationKey.Text = "XXXXX-XXXXX-XXXXX-XXXXX"
        '
        'pnlLicenseUpdate
        '
        Me.pnlLicenseUpdate.Controls.Add(Me.btnSaveKey)
        Me.pnlLicenseUpdate.Controls.Add(Me.txtLicenseKey)
        Me.pnlLicenseUpdate.Location = New System.Drawing.Point(155, 175)
        Me.pnlLicenseUpdate.Margin = New System.Windows.Forms.Padding(4)
        Me.pnlLicenseUpdate.Name = "pnlLicenseUpdate"
        Me.pnlLicenseUpdate.Size = New System.Drawing.Size(327, 28)
        Me.pnlLicenseUpdate.TabIndex = 18
        Me.pnlLicenseUpdate.Visible = False
        '
        'btnSaveKey
        '
        Me.btnSaveKey.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSaveKey.Image = Global.SimpleAccounts.My.Resources.Resources.save_labled
        Me.btnSaveKey.Location = New System.Drawing.Point(288, -1)
        Me.btnSaveKey.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSaveKey.Name = "btnSaveKey"
        Me.btnSaveKey.Size = New System.Drawing.Size(32, 28)
        Me.btnSaveKey.TabIndex = 13
        Me.btnSaveKey.UseVisualStyleBackColor = True
        Me.btnSaveKey.Visible = False
        '
        'txtLicenseKey
        '
        Me.txtLicenseKey.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals
        Me.txtLicenseKey.Location = New System.Drawing.Point(0, 0)
        Me.txtLicenseKey.Margin = New System.Windows.Forms.Padding(4)
        Me.txtLicenseKey.Name = "txtLicenseKey"
        Me.txtLicenseKey.Size = New System.Drawing.Size(279, 24)
        Me.txtLicenseKey.TabIndex = 14
        Me.txtLicenseKey.Visible = False
        '
        'btnGetFingerPrints
        '
        Me.btnGetFingerPrints.Location = New System.Drawing.Point(0, 374)
        Me.btnGetFingerPrints.Margin = New System.Windows.Forms.Padding(4)
        Me.btnGetFingerPrints.Name = "btnGetFingerPrints"
        Me.btnGetFingerPrints.Size = New System.Drawing.Size(209, 28)
        Me.btnGetFingerPrints.TabIndex = 16
        Me.btnGetFingerPrints.Text = "Generate License"
        Me.btnGetFingerPrints.UseVisualStyleBackColor = True
        '
        'btnLoadLicense
        '
        Me.btnLoadLicense.Location = New System.Drawing.Point(0, 338)
        Me.btnLoadLicense.Margin = New System.Windows.Forms.Padding(4)
        Me.btnLoadLicense.Name = "btnLoadLicense"
        Me.btnLoadLicense.Size = New System.Drawing.Size(209, 28)
        Me.btnLoadLicense.TabIndex = 15
        Me.btnLoadLicense.Text = "Load Finger Print"
        Me.btnLoadLicense.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(256, 28)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(163, 25)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "V ERP Solution"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.Location = New System.Drawing.Point(112, 33)
        Me.LinkLabel1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(173, 17)
        Me.LinkLabel1.TabIndex = 10
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "www.siriussolution.com"
        Me.LinkLabel1.Visible = False
        '
        'LinkLabel2
        '
        Me.LinkLabel2.AutoSize = True
        Me.LinkLabel2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel2.Location = New System.Drawing.Point(112, 59)
        Me.LinkLabel2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(175, 17)
        Me.LinkLabel2.TabIndex = 11
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "info@siriussolution.com"
        Me.LinkLabel2.Visible = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(4, 33)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(77, 17)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Web Site:"
        Me.Label7.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(4, 59)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(50, 17)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "Email:"
        Me.Label8.Visible = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(4, 85)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(69, 17)
        Me.Label9.TabIndex = 14
        Me.Label9.Text = "Contact:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(112, 85)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(122, 17)
        Me.Label10.TabIndex = 15
        Me.Label10.Text = "03 444 114 114"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(4, 6)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(144, 17)
        Me.Label11.TabIndex = 16
        Me.Label11.Text = "© SIRIUS (Pvt) Ltd"
        Me.Label11.Visible = False
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = Global.SimpleAccounts.My.Resources.Resources.Icon_20__20About_20US
        Me.PictureBox1.Location = New System.Drawing.Point(0, 1)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(209, 330)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.FlatAppearance.BorderSize = 0
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.Location = New System.Drawing.Point(655, 15)
        Me.btnRefresh.Margin = New System.Windows.Forms.Padding(4)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(41, 38)
        Me.btnRefresh.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.btnRefresh, "Refresh")
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'dtpExpiryDate
        '
        Me.dtpExpiryDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.dtpExpiryDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpExpiryDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpExpiryDate.Location = New System.Drawing.Point(306, 166)
        Me.dtpExpiryDate.Name = "dtpExpiryDate"
        Me.dtpExpiryDate.Size = New System.Drawing.Size(139, 22)
        Me.dtpExpiryDate.TabIndex = 24
        Me.ToolTip1.SetToolTip(Me.dtpExpiryDate, "Sale Date")
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "sbl"
        Me.OpenFileDialog1.FileName = "License.sbl"
        Me.OpenFileDialog1.Filter = "SIRIUS License Files|*.sbl|All Files|*.*"
        Me.OpenFileDialog1.Title = "Browse license file"
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.DefaultExt = "sbfp"
        Me.SaveFileDialog1.FileName = "FingerPrint.sbfp"
        Me.SaveFileDialog1.Filter = "Finger Prints|*.sbfp|All files|*.*"
        Me.SaveFileDialog1.Title = "Save Finger Prints"
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(281, 14)
        Me.lblProgress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(351, 55)
        Me.lblProgress.TabIndex = 18
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'lblStatus
        '
        Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(0, 457)
        Me.lblStatus.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(209, 42)
        Me.lblStatus.TabIndex = 19
        Me.lblStatus.Text = "Pending"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblStatus.Visible = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label11)
        Me.Panel1.Controls.Add(Me.LinkLabel1)
        Me.Panel1.Controls.Add(Me.LinkLabel2)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.lblProgress)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Location = New System.Drawing.Point(217, 338)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(489, 110)
        Me.Panel1.TabIndex = 20
        Me.Panel1.Visible = False
        '
        'btnViewMessageLog
        '
        Me.btnViewMessageLog.Location = New System.Drawing.Point(363, 479)
        Me.btnViewMessageLog.Margin = New System.Windows.Forms.Padding(4)
        Me.btnViewMessageLog.Name = "btnViewMessageLog"
        Me.btnViewMessageLog.Size = New System.Drawing.Size(163, 39)
        Me.btnViewMessageLog.TabIndex = 21
        Me.btnViewMessageLog.Text = "View Message Log"
        Me.btnViewMessageLog.UseVisualStyleBackColor = True
        Me.btnViewMessageLog.Visible = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(0, 411)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(209, 28)
        Me.Button1.TabIndex = 22
        Me.Button1.Text = "Search License"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'lblLicenseKey
        '
        Me.lblLicenseKey.AutoSize = True
        Me.lblLicenseKey.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLicenseKey.Location = New System.Drawing.Point(621, 489)
        Me.lblLicenseKey.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblLicenseKey.Name = "lblLicenseKey"
        Me.lblLicenseKey.Size = New System.Drawing.Size(85, 17)
        Me.lblLicenseKey.TabIndex = 0
        Me.lblLicenseKey.Text = "LicenseKey"
        Me.lblLicenseKey.Visible = False
        '
        'txtDBName
        '
        Me.txtDBName.Location = New System.Drawing.Point(306, 105)
        Me.txtDBName.Name = "txtDBName"
        Me.txtDBName.Size = New System.Drawing.Size(139, 22)
        Me.txtDBName.TabIndex = 23
        '
        'txtUsers
        '
        Me.txtUsers.Location = New System.Drawing.Point(306, 223)
        Me.txtUsers.Name = "txtUsers"
        Me.txtUsers.Size = New System.Drawing.Size(139, 22)
        Me.txtUsers.TabIndex = 23
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(303, 85)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(73, 17)
        Me.Label6.TabIndex = 25
        Me.Label6.Text = "DB Name"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.Location = New System.Drawing.Point(303, 146)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(90, 17)
        Me.Label13.TabIndex = 26
        Me.Label13.Text = "Expiry Date"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.Location = New System.Drawing.Point(303, 203)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(91, 17)
        Me.Label15.TabIndex = 27
        Me.Label15.Text = "No of Users"
        '
        'frmGenerateLicense
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(711, 411)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.dtpExpiryDate)
        Me.Controls.Add(Me.txtUsers)
        Me.Controls.Add(Me.txtDBName)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnViewMessageLog)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lblStatus)
        Me.Controls.Add(Me.btnGetFingerPrints)
        Me.Controls.Add(Me.btnLoadLicense)
        Me.Controls.Add(Me.btnRefresh)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.lblLicenseKey)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGenerateLicense"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Generate License"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.pnlLicenseUpdate.ResumeLayout(False)
        Me.pnlLicenseUpdate.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblVersion As System.Windows.Forms.Label
    Friend WithEvents lblDatabaseName As System.Windows.Forms.Label
    Friend WithEvents lblServerName As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblProduct As System.Windows.Forms.Label
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Friend WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents lblAppVer As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents btnSaveKey As System.Windows.Forms.Button
    Friend WithEvents txtLicenseKey As System.Windows.Forms.MaskedTextBox
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lblExpiryLabel As System.Windows.Forms.Label
    Friend WithEvents lblExpiryDate As System.Windows.Forms.LinkLabel
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lblRegistrationKey As System.Windows.Forms.LinkLabel
    Friend WithEvents pnlLicenseUpdate As System.Windows.Forms.Panel
    Friend WithEvents btnGetFingerPrints As System.Windows.Forms.Button
    Friend WithEvents btnLoadLicense As System.Windows.Forms.Button
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnViewMessageLog As System.Windows.Forms.Button
    Friend WithEvents Button1 As Button
    Friend WithEvents lblLicenseKey As System.Windows.Forms.Label
    Friend WithEvents txtDBName As System.Windows.Forms.TextBox
    Friend WithEvents dtpExpiryDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtUsers As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfigCompanyPath
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
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnBrowseCMFADocument = New System.Windows.Forms.Button()
        Me.btnBrowseFilesAttachment = New System.Windows.Forms.Button()
        Me.btnBrowseAssetPicture = New System.Windows.Forms.Button()
        Me.btnBrowseArticlePicture = New System.Windows.Forms.Button()
        Me.btnBrowseBackupDB = New System.Windows.Forms.Button()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtCMFADocumentAttachmentPath = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtFilesAttachmentPath = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtAssetPicturePath = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtArticlePicturePath = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel23 = New System.Windows.Forms.Panel()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.txtBackupDBPath = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.linkGeneral = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.linkDB = New System.Windows.Forms.Label()
        Me.linkCompanyInfo = New System.Windows.Forms.Label()
        Me.linkSMS = New System.Windows.Forms.Label()
        Me.linkSecurityRights = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel23.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(984, 61)
        Me.Panel2.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(12, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(160, 37)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Path Setting"
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.btnBrowseCMFADocument)
        Me.Panel1.Controls.Add(Me.btnBrowseFilesAttachment)
        Me.Panel1.Controls.Add(Me.btnBrowseAssetPicture)
        Me.Panel1.Controls.Add(Me.btnBrowseArticlePicture)
        Me.Panel1.Controls.Add(Me.btnBrowseBackupDB)
        Me.Panel1.Controls.Add(Me.Panel7)
        Me.Panel1.Controls.Add(Me.txtCMFADocumentAttachmentPath)
        Me.Panel1.Controls.Add(Me.Label10)
        Me.Panel1.Controls.Add(Me.Panel6)
        Me.Panel1.Controls.Add(Me.txtFilesAttachmentPath)
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Controls.Add(Me.Panel5)
        Me.Panel1.Controls.Add(Me.txtAssetPicturePath)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Controls.Add(Me.txtArticlePicturePath)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Panel23)
        Me.Panel1.Controls.Add(Me.txtBackupDBPath)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 61)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(984, 566)
        Me.Panel1.TabIndex = 1
        '
        'btnBrowseCMFADocument
        '
        Me.btnBrowseCMFADocument.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.btnBrowseCMFADocument.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseCMFADocument.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowseCMFADocument.ForeColor = System.Drawing.Color.White
        Me.btnBrowseCMFADocument.Location = New System.Drawing.Point(664, 393)
        Me.btnBrowseCMFADocument.Name = "btnBrowseCMFADocument"
        Me.btnBrowseCMFADocument.Size = New System.Drawing.Size(75, 27)
        Me.btnBrowseCMFADocument.TabIndex = 14
        Me.btnBrowseCMFADocument.Text = "Browse"
        Me.btnBrowseCMFADocument.UseVisualStyleBackColor = False
        '
        'btnBrowseFilesAttachment
        '
        Me.btnBrowseFilesAttachment.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.btnBrowseFilesAttachment.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseFilesAttachment.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowseFilesAttachment.ForeColor = System.Drawing.Color.White
        Me.btnBrowseFilesAttachment.Location = New System.Drawing.Point(664, 297)
        Me.btnBrowseFilesAttachment.Name = "btnBrowseFilesAttachment"
        Me.btnBrowseFilesAttachment.Size = New System.Drawing.Size(75, 27)
        Me.btnBrowseFilesAttachment.TabIndex = 11
        Me.btnBrowseFilesAttachment.Text = "Browse"
        Me.btnBrowseFilesAttachment.UseVisualStyleBackColor = False
        '
        'btnBrowseAssetPicture
        '
        Me.btnBrowseAssetPicture.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.btnBrowseAssetPicture.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseAssetPicture.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowseAssetPicture.ForeColor = System.Drawing.Color.White
        Me.btnBrowseAssetPicture.Location = New System.Drawing.Point(664, 203)
        Me.btnBrowseAssetPicture.Name = "btnBrowseAssetPicture"
        Me.btnBrowseAssetPicture.Size = New System.Drawing.Size(75, 27)
        Me.btnBrowseAssetPicture.TabIndex = 8
        Me.btnBrowseAssetPicture.Text = "Browse"
        Me.btnBrowseAssetPicture.UseVisualStyleBackColor = False
        '
        'btnBrowseArticlePicture
        '
        Me.btnBrowseArticlePicture.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.btnBrowseArticlePicture.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseArticlePicture.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowseArticlePicture.ForeColor = System.Drawing.Color.White
        Me.btnBrowseArticlePicture.Location = New System.Drawing.Point(664, 107)
        Me.btnBrowseArticlePicture.Name = "btnBrowseArticlePicture"
        Me.btnBrowseArticlePicture.Size = New System.Drawing.Size(75, 27)
        Me.btnBrowseArticlePicture.TabIndex = 5
        Me.btnBrowseArticlePicture.Text = "Browse"
        Me.btnBrowseArticlePicture.UseVisualStyleBackColor = False
        '
        'btnBrowseBackupDB
        '
        Me.btnBrowseBackupDB.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.btnBrowseBackupDB.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBrowseBackupDB.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBrowseBackupDB.ForeColor = System.Drawing.Color.White
        Me.btnBrowseBackupDB.Location = New System.Drawing.Point(664, 14)
        Me.btnBrowseBackupDB.Name = "btnBrowseBackupDB"
        Me.btnBrowseBackupDB.Size = New System.Drawing.Size(75, 27)
        Me.btnBrowseBackupDB.TabIndex = 2
        Me.btnBrowseBackupDB.Text = "Browse"
        Me.btnBrowseBackupDB.UseVisualStyleBackColor = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.LemonChiffon
        Me.Panel7.Controls.Add(Me.Label9)
        Me.Panel7.Location = New System.Drawing.Point(244, 426)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(393, 51)
        Me.Panel7.TabIndex = 72
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(4, 16)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(201, 17)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "CMFA document attachment path"
        '
        'txtCMFADocumentAttachmentPath
        '
        Me.txtCMFADocumentAttachmentPath.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCMFADocumentAttachmentPath.Location = New System.Drawing.Point(244, 393)
        Me.txtCMFADocumentAttachmentPath.Name = "txtCMFADocumentAttachmentPath"
        Me.txtCMFADocumentAttachmentPath.ReadOnly = True
        Me.txtCMFADocumentAttachmentPath.Size = New System.Drawing.Size(393, 27)
        Me.txtCMFADocumentAttachmentPath.TabIndex = 13
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(53, 393)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(129, 42)
        Me.Label10.TabIndex = 12
        Me.Label10.Text = "CMFA document " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "attachment path"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.LemonChiffon
        Me.Panel6.Controls.Add(Me.Label7)
        Me.Panel6.Location = New System.Drawing.Point(244, 330)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(393, 51)
        Me.Panel6.TabIndex = 69
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(4, 16)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(153, 17)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "file attachment save path"
        '
        'txtFilesAttachmentPath
        '
        Me.txtFilesAttachmentPath.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFilesAttachmentPath.Location = New System.Drawing.Point(244, 297)
        Me.txtFilesAttachmentPath.Name = "txtFilesAttachmentPath"
        Me.txtFilesAttachmentPath.ReadOnly = True
        Me.txtFilesAttachmentPath.Size = New System.Drawing.Size(393, 27)
        Me.txtFilesAttachmentPath.TabIndex = 10
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(24, 297)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(158, 21)
        Me.Label8.TabIndex = 9
        Me.Label8.Text = "Files attachment path"
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.LemonChiffon
        Me.Panel5.Controls.Add(Me.Label5)
        Me.Panel5.Location = New System.Drawing.Point(244, 236)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(393, 51)
        Me.Panel5.TabIndex = 66
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(4, 16)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(154, 17)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "asstes pictures save path"
        '
        'txtAssetPicturePath
        '
        Me.txtAssetPicturePath.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAssetPicturePath.Location = New System.Drawing.Point(244, 203)
        Me.txtAssetPicturePath.Name = "txtAssetPicturePath"
        Me.txtAssetPicturePath.ReadOnly = True
        Me.txtAssetPicturePath.Size = New System.Drawing.Size(393, 27)
        Me.txtAssetPicturePath.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(48, 203)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(134, 21)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Asset picture path"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.LemonChiffon
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Location = New System.Drawing.Point(244, 140)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(393, 51)
        Me.Panel4.TabIndex = 63
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(4, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(147, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "article picture save path"
        '
        'txtArticlePicturePath
        '
        Me.txtArticlePicturePath.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtArticlePicturePath.Location = New System.Drawing.Point(244, 107)
        Me.txtArticlePicturePath.Name = "txtArticlePicturePath"
        Me.txtArticlePicturePath.ReadOnly = True
        Me.txtArticlePicturePath.Size = New System.Drawing.Size(393, 27)
        Me.txtArticlePicturePath.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(41, 107)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(141, 21)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Article picture path"
        '
        'Panel23
        '
        Me.Panel23.BackColor = System.Drawing.Color.LemonChiffon
        Me.Panel23.Controls.Add(Me.Label23)
        Me.Panel23.Location = New System.Drawing.Point(244, 47)
        Me.Panel23.Name = "Panel23"
        Me.Panel23.Size = New System.Drawing.Size(393, 51)
        Me.Panel23.TabIndex = 60
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.Color.FromArgb(CType(CType(170, Byte), Integer), CType(CType(171, Byte), Integer), CType(CType(173, Byte), Integer))
        Me.Label23.Location = New System.Drawing.Point(4, 16)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(138, 17)
        Me.Label23.TabIndex = 0
        Me.Label23.Text = "database backup path"
        '
        'txtBackupDBPath
        '
        Me.txtBackupDBPath.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBackupDBPath.Location = New System.Drawing.Point(244, 14)
        Me.txtBackupDBPath.Name = "txtBackupDBPath"
        Me.txtBackupDBPath.ReadOnly = True
        Me.txtBackupDBPath.Size = New System.Drawing.Size(393, 27)
        Me.txtBackupDBPath.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(63, 14)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(119, 21)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Backup DB path"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.linkGeneral)
        Me.Panel3.Controls.Add(Me.Label17)
        Me.Panel3.Controls.Add(Me.Label18)
        Me.Panel3.Controls.Add(Me.linkDB)
        Me.Panel3.Controls.Add(Me.linkCompanyInfo)
        Me.Panel3.Controls.Add(Me.linkSMS)
        Me.Panel3.Controls.Add(Me.linkSecurityRights)
        Me.Panel3.Controls.Add(Me.Label12)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(769, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(215, 566)
        Me.Panel3.TabIndex = 15
        '
        'linkGeneral
        '
        Me.linkGeneral.AutoSize = True
        Me.linkGeneral.Cursor = System.Windows.Forms.Cursors.Hand
        Me.linkGeneral.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.linkGeneral.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.linkGeneral.Location = New System.Drawing.Point(20, 225)
        Me.linkGeneral.Name = "linkGeneral"
        Me.linkGeneral.Size = New System.Drawing.Size(64, 21)
        Me.linkGeneral.TabIndex = 5
        Me.linkGeneral.Text = "General"
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
        'linkCompanyInfo
        '
        Me.linkCompanyInfo.AutoSize = True
        Me.linkCompanyInfo.Cursor = System.Windows.Forms.Cursors.Hand
        Me.linkCompanyInfo.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.linkCompanyInfo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.linkCompanyInfo.Location = New System.Drawing.Point(20, 140)
        Me.linkCompanyInfo.Name = "linkCompanyInfo"
        Me.linkCompanyInfo.Size = New System.Drawing.Size(108, 21)
        Me.linkCompanyInfo.TabIndex = 3
        Me.linkCompanyInfo.Text = "Company Info"
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
        'frmConfigCompanyPath
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(984, 627)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Panel2)
        Me.Name = "frmConfigCompanyPath"
        Me.Text = "frmConfigCompanyPath"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel7.ResumeLayout(False)
        Me.Panel7.PerformLayout()
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel23.ResumeLayout(False)
        Me.Panel23.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents linkDB As System.Windows.Forms.Label
    Friend WithEvents linkCompanyInfo As System.Windows.Forms.Label
    Friend WithEvents linkSMS As System.Windows.Forms.Label
    Friend WithEvents linkSecurityRights As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtBackupDBPath As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel23 As System.Windows.Forms.Panel
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtCMFADocumentAttachmentPath As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtFilesAttachmentPath As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtAssetPicturePath As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtArticlePicturePath As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents linkGeneral As System.Windows.Forms.Label
    Friend WithEvents btnBrowseBackupDB As System.Windows.Forms.Button
    Friend WithEvents btnBrowseCMFADocument As System.Windows.Forms.Button
    Friend WithEvents btnBrowseFilesAttachment As System.Windows.Forms.Button
    Friend WithEvents btnBrowseAssetPicture As System.Windows.Forms.Button
    Friend WithEvents btnBrowseArticlePicture As System.Windows.Forms.Button
End Class

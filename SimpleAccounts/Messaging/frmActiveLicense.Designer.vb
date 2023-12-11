<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmActiveLicense
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmActiveLicense))
        Me.btnActive = New System.Windows.Forms.Button()
        Me.txtSystemId = New System.Windows.Forms.TextBox()
        Me.txtSystemName = New System.Windows.Forms.TextBox()
        Me.txtProduct = New System.Windows.Forms.TextBox()
        Me.txtLocationsAllowed = New System.Windows.Forms.TextBox()
        Me.txtTerminalsAllowed = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblTerminals = New System.Windows.Forms.Label()
        Me.lblLocations = New System.Windows.Forms.Label()
        Me.lblProduct = New System.Windows.Forms.Label()
        Me.lblCustomerName = New System.Windows.Forms.Label()
        Me.lblCustomerCode = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Company = New System.Windows.Forms.Label()
        Me.lblCompany = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.LicenseProgressbar = New System.Windows.Forms.ToolStripProgressBar()
        Me.lblProcess = New System.Windows.Forms.ToolStripLabel()
        Me.pbRegistration = New System.Windows.Forms.PictureBox()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.txtLicenseKey = New System.Windows.Forms.TextBox()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.GroupBox1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.pbRegistration, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnActive
        '
        Me.btnActive.Location = New System.Drawing.Point(536, 87)
        Me.btnActive.Name = "btnActive"
        Me.btnActive.Size = New System.Drawing.Size(83, 23)
        Me.btnActive.TabIndex = 4
        Me.btnActive.Text = "Active"
        Me.btnActive.UseVisualStyleBackColor = True
        '
        'txtSystemId
        '
        Me.txtSystemId.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.txtSystemId.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSystemId.Location = New System.Drawing.Point(429, 70)
        Me.txtSystemId.Name = "txtSystemId"
        Me.txtSystemId.Size = New System.Drawing.Size(74, 19)
        Me.txtSystemId.TabIndex = 5
        Me.txtSystemId.Visible = False
        '
        'txtSystemName
        '
        Me.txtSystemName.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.txtSystemName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtSystemName.Location = New System.Drawing.Point(429, 70)
        Me.txtSystemName.Name = "txtSystemName"
        Me.txtSystemName.Size = New System.Drawing.Size(74, 19)
        Me.txtSystemName.TabIndex = 6
        Me.txtSystemName.Visible = False
        '
        'txtProduct
        '
        Me.txtProduct.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.txtProduct.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtProduct.Location = New System.Drawing.Point(429, 70)
        Me.txtProduct.Name = "txtProduct"
        Me.txtProduct.Size = New System.Drawing.Size(74, 19)
        Me.txtProduct.TabIndex = 7
        Me.txtProduct.Visible = False
        '
        'txtLocationsAllowed
        '
        Me.txtLocationsAllowed.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.txtLocationsAllowed.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtLocationsAllowed.Location = New System.Drawing.Point(429, 70)
        Me.txtLocationsAllowed.Name = "txtLocationsAllowed"
        Me.txtLocationsAllowed.Size = New System.Drawing.Size(74, 19)
        Me.txtLocationsAllowed.TabIndex = 12
        Me.txtLocationsAllowed.Visible = False
        '
        'txtTerminalsAllowed
        '
        Me.txtTerminalsAllowed.BackColor = System.Drawing.SystemColors.InactiveCaption
        Me.txtTerminalsAllowed.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.txtTerminalsAllowed.Location = New System.Drawing.Point(429, 70)
        Me.txtTerminalsAllowed.Name = "txtTerminalsAllowed"
        Me.txtTerminalsAllowed.Size = New System.Drawing.Size(74, 19)
        Me.txtTerminalsAllowed.TabIndex = 8
        Me.txtTerminalsAllowed.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(30, 92)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 20)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "License Key"
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(3, 7)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(266, 36)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "License Activation"
        '
        'BackgroundWorker1
        '
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(15, 108)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(128, 20)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Terminal Allowed"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(15, 134)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(129, 20)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Location Allowed"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(15, 160)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(110, 20)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Product Name"
        '
        'lblTerminals
        '
        Me.lblTerminals.Location = New System.Drawing.Point(127, 102)
        Me.lblTerminals.Name = "lblTerminals"
        Me.lblTerminals.Size = New System.Drawing.Size(373, 20)
        Me.lblTerminals.TabIndex = 7
        '
        'lblLocations
        '
        Me.lblLocations.Location = New System.Drawing.Point(127, 128)
        Me.lblLocations.Name = "lblLocations"
        Me.lblLocations.Size = New System.Drawing.Size(373, 20)
        Me.lblLocations.TabIndex = 9
        '
        'lblProduct
        '
        Me.lblProduct.Location = New System.Drawing.Point(127, 154)
        Me.lblProduct.Name = "lblProduct"
        Me.lblProduct.Size = New System.Drawing.Size(373, 20)
        Me.lblProduct.TabIndex = 11
        '
        'lblCustomerName
        '
        Me.lblCustomerName.Location = New System.Drawing.Point(127, 24)
        Me.lblCustomerName.Name = "lblCustomerName"
        Me.lblCustomerName.Size = New System.Drawing.Size(373, 20)
        Me.lblCustomerName.TabIndex = 1
        '
        'lblCustomerCode
        '
        Me.lblCustomerCode.Location = New System.Drawing.Point(127, 50)
        Me.lblCustomerCode.Name = "lblCustomerCode"
        Me.lblCustomerCode.Size = New System.Drawing.Size(373, 20)
        Me.lblCustomerCode.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(15, 28)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(124, 20)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Customer Name"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(15, 54)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(120, 20)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Customer Code"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Company)
        Me.GroupBox1.Controls.Add(Me.lblCompany)
        Me.GroupBox1.Controls.Add(Me.lblCustomerName)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.lblCustomerCode)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.lblTerminals)
        Me.GroupBox1.Controls.Add(Me.lblProduct)
        Me.GroupBox1.Controls.Add(Me.lblLocations)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 116)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(604, 195)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        '
        'Company
        '
        Me.Company.AutoSize = True
        Me.Company.Location = New System.Drawing.Point(15, 80)
        Me.Company.Name = "Company"
        Me.Company.Size = New System.Drawing.Size(76, 20)
        Me.Company.TabIndex = 4
        Me.Company.Text = "Company"
        '
        'lblCompany
        '
        Me.lblCompany.Location = New System.Drawing.Point(127, 76)
        Me.lblCompany.Name = "lblCompany"
        Me.lblCompany.Size = New System.Drawing.Size(373, 20)
        Me.lblCompany.TabIndex = 5
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.LicenseProgressbar, Me.lblProcess})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(644, 28)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'LicenseProgressbar
        '
        Me.LicenseProgressbar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.LicenseProgressbar.Name = "LicenseProgressbar"
        Me.LicenseProgressbar.Size = New System.Drawing.Size(300, 25)
        '
        'lblProcess
        '
        Me.lblProcess.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.lblProcess.ForeColor = System.Drawing.Color.Blue
        Me.lblProcess.Name = "lblProcess"
        Me.lblProcess.Size = New System.Drawing.Size(72, 25)
        Me.lblProcess.Text = "Process"
        '
        'pbRegistration
        '
        Me.pbRegistration.Location = New System.Drawing.Point(509, 89)
        Me.pbRegistration.Name = "pbRegistration"
        Me.pbRegistration.Size = New System.Drawing.Size(21, 20)
        Me.pbRegistration.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbRegistration.TabIndex = 19
        Me.pbRegistration.TabStop = False
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(544, 317)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        Me.btnClose.Visible = False
        '
        'txtLicenseKey
        '
        Me.txtLicenseKey.Location = New System.Drawing.Point(119, 89)
        Me.txtLicenseKey.Name = "txtLicenseKey"
        Me.txtLicenseKey.Size = New System.Drawing.Size(384, 26)
        Me.txtLicenseKey.TabIndex = 3
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 28)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(644, 36)
        Me.pnlHeader.TabIndex = 20
        '
        'frmActiveLicense
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(644, 365)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.pbRegistration)
        Me.Controls.Add(Me.btnActive)
        Me.Controls.Add(Me.txtSystemId)
        Me.Controls.Add(Me.txtSystemName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtProduct)
        Me.Controls.Add(Me.txtTerminalsAllowed)
        Me.Controls.Add(Me.txtLocationsAllowed)
        Me.Controls.Add(Me.txtLicenseKey)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmActiveLicense"
        Me.Text = "License Activation"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.pbRegistration, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSystemId As System.Windows.Forms.TextBox
    Friend WithEvents txtSystemName As System.Windows.Forms.TextBox
    Friend WithEvents txtProduct As System.Windows.Forms.TextBox
    Friend WithEvents txtLocationsAllowed As System.Windows.Forms.TextBox
    Friend WithEvents txtTerminalsAllowed As System.Windows.Forms.TextBox
    Friend WithEvents btnActive As System.Windows.Forms.Button
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents pbRegistration As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblTerminals As System.Windows.Forms.Label
    Friend WithEvents lblLocations As System.Windows.Forms.Label
    Friend WithEvents lblProduct As System.Windows.Forms.Label
    Friend WithEvents lblCustomerName As System.Windows.Forms.Label
    Friend WithEvents lblCustomerCode As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Company As System.Windows.Forms.Label
    Friend WithEvents lblCompany As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents LicenseProgressbar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents lblProcess As System.Windows.Forms.ToolStripLabel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents txtLicenseKey As System.Windows.Forms.TextBox
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

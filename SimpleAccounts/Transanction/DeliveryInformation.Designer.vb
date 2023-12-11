<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DeliveryInformation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DeliveryInformation))
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.pnlDeliveryType = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.pnlTransporter = New System.Windows.Forms.Panel()
        Me.cmbTransporterName = New System.Windows.Forms.ComboBox()
        Me.txtBiltyNo = New System.Windows.Forms.TextBox()
        Me.lblTransporterName = New System.Windows.Forms.Label()
        Me.lblBiltyNo = New System.Windows.Forms.Label()
        Me.rdoLater = New System.Windows.Forms.RadioButton()
        Me.pnlDriverInfo = New System.Windows.Forms.Panel()
        Me.dtpDepartureTime = New System.Windows.Forms.DateTimePicker()
        Me.dtpArrivalTime = New System.Windows.Forms.DateTimePicker()
        Me.txtVehicleNo = New System.Windows.Forms.TextBox()
        Me.txtDriverContactNo = New System.Windows.Forms.TextBox()
        Me.lblOutTime = New System.Windows.Forms.Label()
        Me.lblDriverName = New System.Windows.Forms.Label()
        Me.lblDriverContactNo = New System.Windows.Forms.Label()
        Me.lblInTime = New System.Windows.Forms.Label()
        Me.txtDriverName = New System.Windows.Forms.TextBox()
        Me.lblVehicleNo = New System.Windows.Forms.Label()
        Me.rdoHumayun = New System.Windows.Forms.RadioButton()
        Me.rdoBuilty = New System.Windows.Forms.RadioButton()
        Me.rdoSelf = New System.Windows.Forms.RadioButton()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlHeader.SuspendLayout()
        Me.pnlDeliveryType.SuspendLayout()
        Me.pnlTransporter.SuspendLayout()
        Me.pnlDriverInfo.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnClose)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(442, 42)
        Me.pnlHeader.TabIndex = 2
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.btnClose.Location = New System.Drawing.Point(394, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(36, 30)
        Me.btnClose.TabIndex = 1
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.lblHeader.Location = New System.Drawing.Point(3, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(236, 23)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Delivery Information" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'pnlDeliveryType
        '
        Me.pnlDeliveryType.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.pnlDeliveryType.Controls.Add(Me.Button1)
        Me.pnlDeliveryType.Controls.Add(Me.pnlTransporter)
        Me.pnlDeliveryType.Controls.Add(Me.rdoLater)
        Me.pnlDeliveryType.Controls.Add(Me.pnlDriverInfo)
        Me.pnlDeliveryType.Controls.Add(Me.rdoHumayun)
        Me.pnlDeliveryType.Controls.Add(Me.rdoBuilty)
        Me.pnlDeliveryType.Controls.Add(Me.rdoSelf)
        Me.pnlDeliveryType.Location = New System.Drawing.Point(0, 42)
        Me.pnlDeliveryType.Name = "pnlDeliveryType"
        Me.pnlDeliveryType.Size = New System.Drawing.Size(443, 223)
        Me.pnlDeliveryType.TabIndex = 28
        '
        'Button1
        '
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.Button1.Location = New System.Drawing.Point(343, 11)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(57, 23)
        Me.Button1.TabIndex = 31
        Me.Button1.Text = "OK"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'pnlTransporter
        '
        Me.pnlTransporter.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.pnlTransporter.Controls.Add(Me.cmbTransporterName)
        Me.pnlTransporter.Controls.Add(Me.txtBiltyNo)
        Me.pnlTransporter.Controls.Add(Me.lblTransporterName)
        Me.pnlTransporter.Controls.Add(Me.lblBiltyNo)
        Me.pnlTransporter.Location = New System.Drawing.Point(239, 37)
        Me.pnlTransporter.Name = "pnlTransporter"
        Me.pnlTransporter.Size = New System.Drawing.Size(194, 173)
        Me.pnlTransporter.TabIndex = 30
        Me.pnlTransporter.Visible = False
        '
        'cmbTransporterName
        '
        Me.cmbTransporterName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTransporterName.FormattingEnabled = True
        Me.cmbTransporterName.Location = New System.Drawing.Point(6, 25)
        Me.cmbTransporterName.Name = "cmbTransporterName"
        Me.cmbTransporterName.Size = New System.Drawing.Size(168, 21)
        Me.cmbTransporterName.TabIndex = 35
        Me.ToolTip1.SetToolTip(Me.cmbTransporterName, "Select a Transporter")
        '
        'txtBiltyNo
        '
        Me.txtBiltyNo.Location = New System.Drawing.Point(6, 64)
        Me.txtBiltyNo.Name = "txtBiltyNo"
        Me.txtBiltyNo.Size = New System.Drawing.Size(168, 20)
        Me.txtBiltyNo.TabIndex = 34
        Me.ToolTip1.SetToolTip(Me.txtBiltyNo, "Bilty Number")
        '
        'lblTransporterName
        '
        Me.lblTransporterName.AutoSize = True
        Me.lblTransporterName.Location = New System.Drawing.Point(3, 9)
        Me.lblTransporterName.Name = "lblTransporterName"
        Me.lblTransporterName.Size = New System.Drawing.Size(92, 13)
        Me.lblTransporterName.TabIndex = 30
        Me.lblTransporterName.Text = "Transporter Name"
        '
        'lblBiltyNo
        '
        Me.lblBiltyNo.AutoSize = True
        Me.lblBiltyNo.Location = New System.Drawing.Point(3, 48)
        Me.lblBiltyNo.Name = "lblBiltyNo"
        Me.lblBiltyNo.Size = New System.Drawing.Size(43, 13)
        Me.lblBiltyNo.TabIndex = 31
        Me.lblBiltyNo.Text = "Bilty No"
        '
        'rdoLater
        '
        Me.rdoLater.AutoSize = True
        Me.rdoLater.Location = New System.Drawing.Point(279, 14)
        Me.rdoLater.Name = "rdoLater"
        Me.rdoLater.Size = New System.Drawing.Size(49, 17)
        Me.rdoLater.TabIndex = 6
        Me.rdoLater.Text = "Later"
        Me.rdoLater.UseVisualStyleBackColor = True
        '
        'pnlDriverInfo
        '
        Me.pnlDriverInfo.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.pnlDriverInfo.Controls.Add(Me.dtpDepartureTime)
        Me.pnlDriverInfo.Controls.Add(Me.dtpArrivalTime)
        Me.pnlDriverInfo.Controls.Add(Me.txtVehicleNo)
        Me.pnlDriverInfo.Controls.Add(Me.txtDriverContactNo)
        Me.pnlDriverInfo.Controls.Add(Me.lblOutTime)
        Me.pnlDriverInfo.Controls.Add(Me.lblDriverName)
        Me.pnlDriverInfo.Controls.Add(Me.lblDriverContactNo)
        Me.pnlDriverInfo.Controls.Add(Me.lblInTime)
        Me.pnlDriverInfo.Controls.Add(Me.txtDriverName)
        Me.pnlDriverInfo.Controls.Add(Me.lblVehicleNo)
        Me.pnlDriverInfo.Location = New System.Drawing.Point(44, 37)
        Me.pnlDriverInfo.Name = "pnlDriverInfo"
        Me.pnlDriverInfo.Size = New System.Drawing.Size(192, 173)
        Me.pnlDriverInfo.TabIndex = 29
        '
        'dtpDepartureTime
        '
        Me.dtpDepartureTime.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpDepartureTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpDepartureTime.Location = New System.Drawing.Point(92, 142)
        Me.dtpDepartureTime.Name = "dtpDepartureTime"
        Me.dtpDepartureTime.Size = New System.Drawing.Size(80, 20)
        Me.dtpDepartureTime.TabIndex = 30
        Me.ToolTip1.SetToolTip(Me.dtpDepartureTime, "Time Out")
        '
        'dtpArrivalTime
        '
        Me.dtpArrivalTime.CustomFormat = "dd/MM/yyyy hh:mm:ss tt"
        Me.dtpArrivalTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpArrivalTime.Location = New System.Drawing.Point(6, 142)
        Me.dtpArrivalTime.Name = "dtpArrivalTime"
        Me.dtpArrivalTime.Size = New System.Drawing.Size(80, 20)
        Me.dtpArrivalTime.TabIndex = 31
        Me.ToolTip1.SetToolTip(Me.dtpArrivalTime, "Time In")
        '
        'txtVehicleNo
        '
        Me.txtVehicleNo.Location = New System.Drawing.Point(6, 103)
        Me.txtVehicleNo.Name = "txtVehicleNo"
        Me.txtVehicleNo.Size = New System.Drawing.Size(168, 20)
        Me.txtVehicleNo.TabIndex = 33
        Me.ToolTip1.SetToolTip(Me.txtVehicleNo, "Vehicle Number")
        '
        'txtDriverContactNo
        '
        Me.txtDriverContactNo.Location = New System.Drawing.Point(6, 64)
        Me.txtDriverContactNo.Name = "txtDriverContactNo"
        Me.txtDriverContactNo.Size = New System.Drawing.Size(168, 20)
        Me.txtDriverContactNo.TabIndex = 34
        Me.ToolTip1.SetToolTip(Me.txtDriverContactNo, "Driver Contact Number")
        '
        'lblOutTime
        '
        Me.lblOutTime.AutoSize = True
        Me.lblOutTime.Location = New System.Drawing.Point(89, 126)
        Me.lblOutTime.Name = "lblOutTime"
        Me.lblOutTime.Size = New System.Drawing.Size(50, 13)
        Me.lblOutTime.TabIndex = 35
        Me.lblOutTime.Text = "Time Out"
        '
        'lblDriverName
        '
        Me.lblDriverName.AutoSize = True
        Me.lblDriverName.Location = New System.Drawing.Point(3, 9)
        Me.lblDriverName.Name = "lblDriverName"
        Me.lblDriverName.Size = New System.Drawing.Size(66, 13)
        Me.lblDriverName.TabIndex = 30
        Me.lblDriverName.Text = "Driver Name"
        '
        'lblDriverContactNo
        '
        Me.lblDriverContactNo.AutoSize = True
        Me.lblDriverContactNo.Location = New System.Drawing.Point(3, 48)
        Me.lblDriverContactNo.Name = "lblDriverContactNo"
        Me.lblDriverContactNo.Size = New System.Drawing.Size(115, 13)
        Me.lblDriverContactNo.TabIndex = 31
        Me.lblDriverContactNo.Text = "Driver Contact Number"
        '
        'lblInTime
        '
        Me.lblInTime.AutoSize = True
        Me.lblInTime.Location = New System.Drawing.Point(4, 126)
        Me.lblInTime.Name = "lblInTime"
        Me.lblInTime.Size = New System.Drawing.Size(42, 13)
        Me.lblInTime.TabIndex = 32
        Me.lblInTime.Text = "Time In"
        '
        'txtDriverName
        '
        Me.txtDriverName.Location = New System.Drawing.Point(6, 25)
        Me.txtDriverName.Name = "txtDriverName"
        Me.txtDriverName.Size = New System.Drawing.Size(168, 20)
        Me.txtDriverName.TabIndex = 30
        Me.ToolTip1.SetToolTip(Me.txtDriverName, "Driver Name")
        '
        'lblVehicleNo
        '
        Me.lblVehicleNo.AutoSize = True
        Me.lblVehicleNo.Location = New System.Drawing.Point(4, 87)
        Me.lblVehicleNo.Name = "lblVehicleNo"
        Me.lblVehicleNo.Size = New System.Drawing.Size(82, 13)
        Me.lblVehicleNo.TabIndex = 33
        Me.lblVehicleNo.Text = "Vehicle Number"
        '
        'rdoHumayun
        '
        Me.rdoHumayun.AutoSize = True
        Me.rdoHumayun.Location = New System.Drawing.Point(152, 14)
        Me.rdoHumayun.Name = "rdoHumayun"
        Me.rdoHumayun.Size = New System.Drawing.Size(121, 17)
        Me.rdoHumayun.TabIndex = 5
        Me.rdoHumayun.Text = "Humayun Chemicals"
        Me.rdoHumayun.UseVisualStyleBackColor = True
        '
        'rdoBuilty
        '
        Me.rdoBuilty.AutoSize = True
        Me.rdoBuilty.Location = New System.Drawing.Point(96, 14)
        Me.rdoBuilty.Name = "rdoBuilty"
        Me.rdoBuilty.Size = New System.Drawing.Size(50, 17)
        Me.rdoBuilty.TabIndex = 4
        Me.rdoBuilty.Text = "Builty"
        Me.rdoBuilty.UseVisualStyleBackColor = True
        '
        'rdoSelf
        '
        Me.rdoSelf.AutoSize = True
        Me.rdoSelf.Checked = True
        Me.rdoSelf.Location = New System.Drawing.Point(47, 14)
        Me.rdoSelf.Name = "rdoSelf"
        Me.rdoSelf.Size = New System.Drawing.Size(43, 17)
        Me.rdoSelf.TabIndex = 3
        Me.rdoSelf.TabStop = True
        Me.rdoSelf.Text = "Self"
        Me.rdoSelf.UseVisualStyleBackColor = True
        '
        'DeliveryInformation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(442, 264)
        Me.Controls.Add(Me.pnlDeliveryType)
        Me.Controls.Add(Me.pnlHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "DeliveryInformation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "DeliveryInformation"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlDeliveryType.ResumeLayout(False)
        Me.pnlDeliveryType.PerformLayout()
        Me.pnlTransporter.ResumeLayout(False)
        Me.pnlTransporter.PerformLayout()
        Me.pnlDriverInfo.ResumeLayout(False)
        Me.pnlDriverInfo.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents pnlDeliveryType As System.Windows.Forms.Panel
    Friend WithEvents rdoLater As System.Windows.Forms.RadioButton
    Friend WithEvents rdoHumayun As System.Windows.Forms.RadioButton
    Friend WithEvents rdoBuilty As System.Windows.Forms.RadioButton
    Friend WithEvents rdoSelf As System.Windows.Forms.RadioButton
    Friend WithEvents pnlDriverInfo As System.Windows.Forms.Panel
    Friend WithEvents dtpDepartureTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpArrivalTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtVehicleNo As System.Windows.Forms.TextBox
    Friend WithEvents txtDriverContactNo As System.Windows.Forms.TextBox
    Friend WithEvents lblOutTime As System.Windows.Forms.Label
    Friend WithEvents lblDriverName As System.Windows.Forms.Label
    Friend WithEvents lblDriverContactNo As System.Windows.Forms.Label
    Friend WithEvents lblInTime As System.Windows.Forms.Label
    Friend WithEvents txtDriverName As System.Windows.Forms.TextBox
    Friend WithEvents lblVehicleNo As System.Windows.Forms.Label
    Friend WithEvents pnlTransporter As System.Windows.Forms.Panel
    Friend WithEvents txtBiltyNo As System.Windows.Forms.TextBox
    Friend WithEvents lblTransporterName As System.Windows.Forms.Label
    Friend WithEvents lblBiltyNo As System.Windows.Forms.Label
    Friend WithEvents cmbTransporterName As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

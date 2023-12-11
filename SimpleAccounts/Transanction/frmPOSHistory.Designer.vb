<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPOSHistory
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPOSHistory))
        Dim grd_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.btnUpdateAll = New System.Windows.Forms.Button()
        Me.Button12 = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.lblFromDate = New System.Windows.Forms.Label()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.lblToDate = New System.Windows.Forms.Label()
        Me.txtNo = New System.Windows.Forms.TextBox()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.lblNO = New System.Windows.Forms.Label()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.rdoClosed = New System.Windows.Forms.RadioButton()
        Me.RdoHold = New System.Windows.Forms.RadioButton()
        Me.rdoNonPaid = New System.Windows.Forms.RadioButton()
        Me.btnGetAllPOS = New System.Windows.Forms.Button()
        Me.pnlHeader.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnGetAllPOS)
        Me.pnlHeader.Controls.Add(Me.btnUpdateAll)
        Me.pnlHeader.Controls.Add(Me.Button12)
        Me.pnlHeader.Controls.Add(Me.btnClose)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Location = New System.Drawing.Point(-1, -1)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(715, 43)
        Me.pnlHeader.TabIndex = 0
        '
        'btnUpdateAll
        '
        Me.btnUpdateAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUpdateAll.BackgroundImage = Global.SimpleAccounts.My.Resources.Resources.refresh2
        Me.btnUpdateAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnUpdateAll.FlatAppearance.BorderSize = 0
        Me.btnUpdateAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnUpdateAll.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnUpdateAll.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnUpdateAll.Location = New System.Drawing.Point(591, 3)
        Me.btnUpdateAll.Name = "btnUpdateAll"
        Me.btnUpdateAll.Size = New System.Drawing.Size(31, 36)
        Me.btnUpdateAll.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.btnUpdateAll, "Update All")
        Me.btnUpdateAll.UseVisualStyleBackColor = True
        '
        'Button12
        '
        Me.Button12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button12.BackgroundImage = CType(resources.GetObject("Button12.BackgroundImage"), System.Drawing.Image)
        Me.Button12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button12.FlatAppearance.BorderSize = 0
        Me.Button12.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.Button12.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button12.Location = New System.Drawing.Point(628, 2)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(31, 36)
        Me.Button12.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.Button12, "Delete")
        Me.Button12.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.btnClose.Location = New System.Drawing.Point(665, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(38, 30)
        Me.btnClose.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.btnClose, "Close")
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.lblHeader.Location = New System.Drawing.Point(24, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(138, 23)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "POS History"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.Checked = False
        Me.dtpFromDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(90, 58)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.ShowCheckBox = True
        Me.dtpFromDate.Size = New System.Drawing.Size(121, 20)
        Me.dtpFromDate.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.dtpFromDate, "Select From Date")
        '
        'lblFromDate
        '
        Me.lblFromDate.AutoSize = True
        Me.lblFromDate.Location = New System.Drawing.Point(28, 60)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(56, 13)
        Me.lblFromDate.TabIndex = 1
        Me.lblFromDate.Text = "From Date"
        '
        'dtpToDate
        '
        Me.dtpToDate.Checked = False
        Me.dtpToDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(264, 58)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.ShowCheckBox = True
        Me.dtpToDate.Size = New System.Drawing.Size(121, 20)
        Me.dtpToDate.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.dtpToDate, "Select To Date")
        '
        'lblToDate
        '
        Me.lblToDate.AutoSize = True
        Me.lblToDate.Location = New System.Drawing.Point(217, 60)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(46, 13)
        Me.lblToDate.TabIndex = 3
        Me.lblToDate.Text = "To Date"
        '
        'txtNo
        '
        Me.txtNo.Location = New System.Drawing.Point(418, 57)
        Me.txtNo.Name = "txtNo"
        Me.txtNo.Size = New System.Drawing.Size(51, 20)
        Me.txtNo.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.txtNo, "Load Records Upto")
        '
        'btnLoad
        '
        Me.btnLoad.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnLoad.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnLoad.Location = New System.Drawing.Point(659, 55)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(43, 22)
        Me.btnLoad.TabIndex = 7
        Me.btnLoad.Text = "Load"
        Me.ToolTip1.SetToolTip(Me.btnLoad, "Load on Grid")
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'lblNO
        '
        Me.lblNO.AutoSize = True
        Me.lblNO.Location = New System.Drawing.Point(391, 60)
        Me.lblNO.Name = "lblNO"
        Me.lblNO.Size = New System.Drawing.Size(24, 13)
        Me.lblNO.TabIndex = 5
        Me.lblNO.Text = "No."
        '
        'grd
        '
        Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grd_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grd.DesignTimeLayout = grd_DesignTimeLayout
        Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grd.GroupByBoxVisible = False
        Me.grd.Location = New System.Drawing.Point(0, 84)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(714, 367)
        Me.grd.TabIndex = 8
        Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'rdoClosed
        '
        Me.rdoClosed.AutoSize = True
        Me.rdoClosed.Checked = True
        Me.rdoClosed.Location = New System.Drawing.Point(475, 57)
        Me.rdoClosed.Name = "rdoClosed"
        Me.rdoClosed.Size = New System.Drawing.Size(57, 17)
        Me.rdoClosed.TabIndex = 9
        Me.rdoClosed.TabStop = True
        Me.rdoClosed.Text = "Closed"
        Me.rdoClosed.UseVisualStyleBackColor = True
        '
        'RdoHold
        '
        Me.RdoHold.AutoSize = True
        Me.RdoHold.Location = New System.Drawing.Point(533, 57)
        Me.RdoHold.Name = "RdoHold"
        Me.RdoHold.Size = New System.Drawing.Size(47, 17)
        Me.RdoHold.TabIndex = 9
        Me.RdoHold.Text = "Hold"
        Me.RdoHold.UseVisualStyleBackColor = True
        '
        'rdoNonPaid
        '
        Me.rdoNonPaid.AutoSize = True
        Me.rdoNonPaid.Location = New System.Drawing.Point(583, 57)
        Me.rdoNonPaid.Name = "rdoNonPaid"
        Me.rdoNonPaid.Size = New System.Drawing.Size(69, 17)
        Me.rdoNonPaid.TabIndex = 9
        Me.rdoNonPaid.Text = "Non Paid"
        Me.rdoNonPaid.UseVisualStyleBackColor = True
        '
        'btnGetAllPOS
        '
        Me.btnGetAllPOS.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnGetAllPOS.BackgroundImage = Global.SimpleAccounts.My.Resources.Resources.order
        Me.btnGetAllPOS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnGetAllPOS.FlatAppearance.BorderSize = 0
        Me.btnGetAllPOS.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGetAllPOS.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnGetAllPOS.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnGetAllPOS.Location = New System.Drawing.Point(554, 3)
        Me.btnGetAllPOS.Name = "btnGetAllPOS"
        Me.btnGetAllPOS.Size = New System.Drawing.Size(31, 36)
        Me.btnGetAllPOS.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.btnGetAllPOS, "Get All POS")
        Me.btnGetAllPOS.UseVisualStyleBackColor = True
        '
        'frmPOSHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(713, 452)
        Me.Controls.Add(Me.rdoNonPaid)
        Me.Controls.Add(Me.RdoHold)
        Me.Controls.Add(Me.rdoClosed)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.lblNO)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.txtNo)
        Me.Controls.Add(Me.lblToDate)
        Me.Controls.Add(Me.dtpToDate)
        Me.Controls.Add(Me.lblFromDate)
        Me.Controls.Add(Me.dtpFromDate)
        Me.Controls.Add(Me.pnlHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "frmPOSHistory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmPOSHistory"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblFromDate As System.Windows.Forms.Label
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblToDate As System.Windows.Forms.Label
    Friend WithEvents txtNo As System.Windows.Forms.TextBox
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents lblNO As System.Windows.Forms.Label
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Button12 As System.Windows.Forms.Button
    Friend WithEvents rdoClosed As System.Windows.Forms.RadioButton
    Friend WithEvents RdoHold As System.Windows.Forms.RadioButton
    Friend WithEvents rdoNonPaid As System.Windows.Forms.RadioButton
    Friend WithEvents btnUpdateAll As System.Windows.Forms.Button
    Friend WithEvents btnGetAllPOS As System.Windows.Forms.Button
End Class

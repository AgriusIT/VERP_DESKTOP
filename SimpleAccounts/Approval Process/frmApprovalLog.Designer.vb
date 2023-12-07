<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmApprovalLog
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
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.BtnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnRejectRequest = New System.Windows.Forms.ToolStripButton()
        Me.btnApproveRequest = New System.Windows.Forms.ToolStripButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.grdLog = New Janus.Windows.GridEX.GridEX()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.RdoRejected = New System.Windows.Forms.RadioButton()
        Me.RdoApproved = New System.Windows.Forms.RadioButton()
        Me.RdoUnApproved = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbVtype = New System.Windows.Forms.ComboBox()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.grdLoandRequests = New Janus.Windows.GridEX.GridEX()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.grdLog, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        CType(Me.grdLoandRequests, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.BtnRefresh, Me.btnRejectRequest, Me.btnApproveRequest})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1099, 31)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = Global.SimpleAccounts.My.Resources.Resources.BtnNew_Image
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(67, 28)
        Me.btnNew.Text = "&New"
        '
        'BtnRefresh
        '
        Me.BtnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.BtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnRefresh.Name = "BtnRefresh"
        Me.BtnRefresh.Size = New System.Drawing.Size(86, 28)
        Me.BtnRefresh.Text = "Refresh"
        Me.BtnRefresh.Visible = False
        '
        'btnRejectRequest
        '
        Me.btnRejectRequest.Image = Global.SimpleAccounts.My.Resources.Resources.cross_icon
        Me.btnRejectRequest.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRejectRequest.Name = "btnRejectRequest"
        Me.btnRejectRequest.Size = New System.Drawing.Size(78, 28)
        Me.btnRejectRequest.Text = "&Reject"
        Me.btnRejectRequest.Visible = False
        '
        'btnApproveRequest
        '
        Me.btnApproveRequest.Image = Global.SimpleAccounts.My.Resources.Resources._20604_24_button_ok_icon
        Me.btnApproveRequest.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnApproveRequest.Name = "btnApproveRequest"
        Me.btnApproveRequest.Size = New System.Drawing.Size(94, 28)
        Me.btnApproveRequest.Text = "&Approve"
        Me.btnApproveRequest.Visible = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 31)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblProgress)
        Me.SplitContainer1.Panel1.Controls.Add(Me.grdLog)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel2)
        Me.SplitContainer1.Panel1.Controls.Add(Me.pnlHeader)
        Me.SplitContainer1.Panel1.Controls.Add(Me.grdLoandRequests)
        Me.SplitContainer1.Panel2Collapsed = True
        Me.SplitContainer1.Size = New System.Drawing.Size(1099, 682)
        Me.SplitContainer1.SplitterDistance = 399
        Me.SplitContainer1.SplitterWidth = 5
        Me.SplitContainer1.TabIndex = 5
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(370, 284)
        Me.lblProgress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(350, 55)
        Me.lblProgress.TabIndex = 27
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'grdLog
        '
        Me.grdLog.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdLog.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdLog.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdLog.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdLog.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdLog.GroupByBoxVisible = False
        Me.grdLog.Location = New System.Drawing.Point(0, 162)
        Me.grdLog.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grdLog.Name = "grdLog"
        Me.grdLog.RecordNavigator = True
        Me.grdLog.Size = New System.Drawing.Size(1099, 521)
        Me.grdLog.TabIndex = 26
        Me.grdLog.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdLog.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.Panel2.Controls.Add(Me.RdoRejected)
        Me.Panel2.Controls.Add(Me.RdoApproved)
        Me.Panel2.Controls.Add(Me.RdoUnApproved)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.cmbVtype)
        Me.Panel2.Controls.Add(Me.dtpToDate)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.btnShow)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.dtpFromDate)
        Me.Panel2.Location = New System.Drawing.Point(0, 78)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1098, 75)
        Me.Panel2.TabIndex = 25
        '
        'RdoRejected
        '
        Me.RdoRejected.AutoSize = True
        Me.RdoRejected.Location = New System.Drawing.Point(861, 50)
        Me.RdoRejected.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RdoRejected.Name = "RdoRejected"
        Me.RdoRejected.Size = New System.Drawing.Size(85, 21)
        Me.RdoRejected.TabIndex = 29
        Me.RdoRejected.Text = "Rejected"
        Me.RdoRejected.UseVisualStyleBackColor = True
        '
        'RdoApproved
        '
        Me.RdoApproved.AutoSize = True
        Me.RdoApproved.Location = New System.Drawing.Point(759, 50)
        Me.RdoApproved.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RdoApproved.Name = "RdoApproved"
        Me.RdoApproved.Size = New System.Drawing.Size(90, 21)
        Me.RdoApproved.TabIndex = 8
        Me.RdoApproved.Text = "Approved"
        Me.RdoApproved.UseVisualStyleBackColor = True
        '
        'RdoUnApproved
        '
        Me.RdoUnApproved.AutoSize = True
        Me.RdoUnApproved.Checked = True
        Me.RdoUnApproved.Location = New System.Drawing.Point(640, 50)
        Me.RdoUnApproved.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.RdoUnApproved.Name = "RdoUnApproved"
        Me.RdoUnApproved.Size = New System.Drawing.Size(108, 21)
        Me.RdoUnApproved.TabIndex = 7
        Me.RdoUnApproved.TabStop = True
        Me.RdoUnApproved.Text = "UnApproved"
        Me.RdoUnApproved.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(25, 23)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(74, 17)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "From Date"
        '
        'cmbVtype
        '
        Me.cmbVtype.BackColor = System.Drawing.SystemColors.Control
        Me.cmbVtype.ForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.cmbVtype.FormattingEnabled = True
        Me.cmbVtype.Items.AddRange(New Object() {"......Select Any........", "Voucher", "Purchase", "Purchase Return", "Purchase Demand", "Purchase Order", "Receiving Note", "Vendor Quotation", "Purchase Inquiry", "Sales Inquiry", "Sales Quotation", "Sales", "Sales Order", "Delivery Challan", "Sales Return", "Sales Invoice Transfer", "Cash Request", "Employee Request"})
        Me.cmbVtype.Location = New System.Drawing.Point(640, 18)
        Me.cmbVtype.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbVtype.Name = "cmbVtype"
        Me.cmbVtype.Size = New System.Drawing.Size(296, 24)
        Me.cmbVtype.TabIndex = 7
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(375, 18)
        Me.dtpToDate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(164, 22)
        Me.dtpToDate.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(306, 23)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 17)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "To Date"
        '
        'btnShow
        '
        Me.btnShow.BackColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnShow.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShow.ForeColor = System.Drawing.Color.Black
        Me.btnShow.Location = New System.Drawing.Point(946, 18)
        Me.btnShow.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(96, 30)
        Me.btnShow.TabIndex = 11
        Me.btnShow.Text = "Show"
        Me.btnShow.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(548, 23)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(83, 17)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Transaction"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CalendarForeColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.dtpFromDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.dtpFromDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(132, 18)
        Me.dtpFromDate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(164, 22)
        Me.dtpFromDate.TabIndex = 3
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1099, 68)
        Me.pnlHeader.TabIndex = 24
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(35, 16)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(177, 31)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Approval Log"
        '
        'grdLoandRequests
        '
        Me.grdLoandRequests.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdLoandRequests.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdLoandRequests.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdLoandRequests.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdLoandRequests.GroupByBoxVisible = False
        Me.grdLoandRequests.Location = New System.Drawing.Point(3, 82)
        Me.grdLoandRequests.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grdLoandRequests.Name = "grdLoandRequests"
        Me.grdLoandRequests.RecordNavigator = True
        Me.grdLoandRequests.Size = New System.Drawing.Size(1095, 521)
        Me.grdLoandRequests.TabIndex = 28
        Me.grdLoandRequests.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdLoandRequests.Visible = False
        Me.grdLoandRequests.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(1048, 0)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.CtrlGrdBar1.MyGrid = Me.grdLog
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(51, 30)
        Me.CtrlGrdBar1.TabIndex = 4
        '
        'frmApprovalLog
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1099, 713)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmApprovalLog"
        Me.Text = "Approval Log"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.grdLog, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.grdLoandRequests, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents BtnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbVtype As System.Windows.Forms.ComboBox
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents grdLog As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents btnApproveRequest As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRejectRequest As System.Windows.Forms.ToolStripButton
    Friend WithEvents grdLoandRequests As Janus.Windows.GridEX.GridEX
    Friend WithEvents RdoApproved As System.Windows.Forms.RadioButton
    Friend WithEvents RdoUnApproved As System.Windows.Forms.RadioButton
    Friend WithEvents RdoRejected As System.Windows.Forms.RadioButton
End Class

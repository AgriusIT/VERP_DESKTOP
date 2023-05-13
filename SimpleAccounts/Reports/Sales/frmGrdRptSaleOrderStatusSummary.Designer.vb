<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGrdRptSaleOrderStatusSummary
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGrdRptSaleOrderStatusSummary))
        Dim grd_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.BtnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.Print = New System.Windows.Forms.ToolStripButton()
        Me.cmbSetTo = New System.Windows.Forms.ToolStripComboBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chInvoices = New System.Windows.Forms.CheckBox()
        Me.chDeliveryChallans = New System.Windows.Forms.CheckBox()
        Me.txtOrderNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.rdbSpecificOrder = New System.Windows.Forms.RadioButton()
        Me.dtTo = New System.Windows.Forms.DateTimePicker()
        Me.rdbDateRange = New System.Windows.Forms.RadioButton()
        Me.dtFrom = New System.Windows.Forms.DateTimePicker()
        Me.rdbAllSalesOrders = New System.Windows.Forms.RadioButton()
        Me.cmbCustomer = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.CtrlGrdBarFront = New SimpleAccounts.CtrlGrdBar()
        Me.pnlHeader.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 33)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1228, 65)
        Me.pnlHeader.TabIndex = 113
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(14, 15)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(401, 36)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Sale Order Status Summary"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.toolStripSeparator1, Me.BtnNew, Me.btnSave, Me.btnRefresh, Me.Print, Me.cmbSetTo})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1228, 33)
        Me.ToolStrip1.TabIndex = 112
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 33)
        '
        'BtnNew
        '
        Me.BtnNew.Image = CType(resources.GetObject("BtnNew.Image"), System.Drawing.Image)
        Me.BtnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnNew.Name = "BtnNew"
        Me.BtnNew.Size = New System.Drawing.Size(75, 30)
        Me.BtnNew.Text = "&New"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(77, 30)
        Me.btnSave.Text = "&Save"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 30)
        Me.btnRefresh.Text = "Refresh"
        '
        'Print
        '
        Me.Print.Image = CType(resources.GetObject("Print.Image"), System.Drawing.Image)
        Me.Print.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Print.Name = "Print"
        Me.Print.Size = New System.Drawing.Size(76, 30)
        Me.Print.Text = "&Print"
        '
        'cmbSetTo
        '
        Me.cmbSetTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSetTo.Name = "cmbSetTo"
        Me.cmbSetTo.Size = New System.Drawing.Size(180, 33)
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.cmbStatus)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.btnLoad)
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.txtOrderNo)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.rdbSpecificOrder)
        Me.Panel1.Controls.Add(Me.dtTo)
        Me.Panel1.Controls.Add(Me.rdbDateRange)
        Me.Panel1.Controls.Add(Me.dtFrom)
        Me.Panel1.Controls.Add(Me.rdbAllSalesOrders)
        Me.Panel1.Controls.Add(Me.cmbCustomer)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(18, 112)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1192, 217)
        Me.Panel1.TabIndex = 0
        '
        'cmbStatus
        '
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Location = New System.Drawing.Point(138, 162)
        Me.cmbStatus.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(306, 28)
        Me.cmbStatus.TabIndex = 127
        Me.ToolTip1.SetToolTip(Me.cmbStatus, "Select Any Status")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(48, 166)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 20)
        Me.Label4.TabIndex = 126
        Me.Label4.Text = "S&tatus"
        '
        'btnLoad
        '
        Me.btnLoad.Location = New System.Drawing.Point(454, 155)
        Me.btnLoad.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(102, 42)
        Me.btnLoad.TabIndex = 7
        Me.btnLoad.Text = "Load"
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chInvoices)
        Me.GroupBox1.Controls.Add(Me.chDeliveryChallans)
        Me.GroupBox1.Location = New System.Drawing.Point(903, 18)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(267, 178)
        Me.GroupBox1.TabIndex = 125
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "View Closure For"
        Me.GroupBox1.Visible = False
        '
        'chInvoices
        '
        Me.chInvoices.AutoSize = True
        Me.chInvoices.Location = New System.Drawing.Point(9, 111)
        Me.chInvoices.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chInvoices.Name = "chInvoices"
        Me.chInvoices.Size = New System.Drawing.Size(195, 24)
        Me.chInvoices.TabIndex = 1
        Me.chInvoices.Text = "Show Closure Invoices"
        Me.chInvoices.UseVisualStyleBackColor = True
        '
        'chDeliveryChallans
        '
        Me.chDeliveryChallans.AutoSize = True
        Me.chDeliveryChallans.Location = New System.Drawing.Point(9, 68)
        Me.chDeliveryChallans.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chDeliveryChallans.Name = "chDeliveryChallans"
        Me.chDeliveryChallans.Size = New System.Drawing.Size(168, 24)
        Me.chDeliveryChallans.TabIndex = 0
        Me.chDeliveryChallans.Text = "Show Closure DCs"
        Me.chDeliveryChallans.UseVisualStyleBackColor = True
        '
        'txtOrderNo
        '
        Me.txtOrderNo.Location = New System.Drawing.Point(402, 75)
        Me.txtOrderNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtOrderNo.Name = "txtOrderNo"
        Me.txtOrderNo.Size = New System.Drawing.Size(152, 26)
        Me.txtOrderNo.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(48, 120)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 20)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "From Date"
        '
        'rdbSpecificOrder
        '
        Me.rdbSpecificOrder.AutoSize = True
        Me.rdbSpecificOrder.Location = New System.Drawing.Point(177, 77)
        Me.rdbSpecificOrder.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rdbSpecificOrder.Name = "rdbSpecificOrder"
        Me.rdbSpecificOrder.Size = New System.Drawing.Size(202, 24)
        Me.rdbSpecificOrder.TabIndex = 2
        Me.rdbSpecificOrder.TabStop = True
        Me.rdbSpecificOrder.Text = "Specific Sales Order No"
        Me.rdbSpecificOrder.UseVisualStyleBackColor = True
        '
        'dtTo
        '
        Me.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtTo.Location = New System.Drawing.Point(402, 115)
        Me.dtTo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtTo.Name = "dtTo"
        Me.dtTo.Size = New System.Drawing.Size(151, 26)
        Me.dtTo.TabIndex = 6
        '
        'rdbDateRange
        '
        Me.rdbDateRange.AutoSize = True
        Me.rdbDateRange.Location = New System.Drawing.Point(26, 118)
        Me.rdbDateRange.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rdbDateRange.Name = "rdbDateRange"
        Me.rdbDateRange.Size = New System.Drawing.Size(21, 20)
        Me.rdbDateRange.TabIndex = 2
        Me.rdbDateRange.TabStop = True
        Me.rdbDateRange.UseVisualStyleBackColor = True
        '
        'dtFrom
        '
        Me.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtFrom.Location = New System.Drawing.Point(138, 115)
        Me.dtFrom.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtFrom.Name = "dtFrom"
        Me.dtFrom.Size = New System.Drawing.Size(152, 26)
        Me.dtFrom.TabIndex = 5
        '
        'rdbAllSalesOrders
        '
        Me.rdbAllSalesOrders.AutoSize = True
        Me.rdbAllSalesOrders.Location = New System.Drawing.Point(26, 77)
        Me.rdbAllSalesOrders.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rdbAllSalesOrders.Name = "rdbAllSalesOrders"
        Me.rdbAllSalesOrders.Size = New System.Drawing.Size(139, 24)
        Me.rdbAllSalesOrders.TabIndex = 1
        Me.rdbAllSalesOrders.TabStop = True
        Me.rdbAllSalesOrders.Text = "All Sale Orders"
        Me.rdbAllSalesOrders.UseVisualStyleBackColor = True
        '
        'cmbCustomer
        '
        Me.cmbCustomer.FormattingEnabled = True
        Me.cmbCustomer.Location = New System.Drawing.Point(26, 31)
        Me.cmbCustomer.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbCustomer.Name = "cmbCustomer"
        Me.cmbCustomer.Size = New System.Drawing.Size(529, 28)
        Me.cmbCustomer.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(316, 122)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 20)
        Me.Label3.TabIndex = 113
        Me.Label3.Text = "To Date"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(21, 6)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 20)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Customer"
        '
        'grd
        '
        Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grd_DesignTimeLayout.LayoutString = resources.GetString("grd_DesignTimeLayout.LayoutString")
        Me.grd.DesignTimeLayout = grd_DesignTimeLayout
        Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.grd.GroupByBoxVisible = False
        Me.grd.Location = New System.Drawing.Point(0, 338)
        Me.grd.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grd.Name = "grd"
        Me.grd.Size = New System.Drawing.Size(1228, 422)
        Me.grd.TabIndex = 121
        Me.ToolTip1.SetToolTip(Me.grd, "Sales Order Status Detail")
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(416, 474)
        Me.lblProgress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(394, 69)
        Me.lblProgress.TabIndex = 128
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'CtrlGrdBarFront
        '
        Me.CtrlGrdBarFront.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBarFront.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBarFront.Email = Nothing
        Me.CtrlGrdBarFront.FormName = Nothing
        Me.CtrlGrdBarFront.Location = New System.Drawing.Point(1179, 0)
        Me.CtrlGrdBarFront.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBarFront.MyGrid = Nothing
        Me.CtrlGrdBarFront.Name = "CtrlGrdBarFront"
        Me.CtrlGrdBarFront.Size = New System.Drawing.Size(50, 38)
        Me.CtrlGrdBarFront.TabIndex = 120
        Me.CtrlGrdBarFront.TabStop = False
        '
        'frmGrdRptSaleOrderStatusSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1228, 760)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.CtrlGrdBarFront)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmGrdRptSaleOrderStatusSummary"
        Me.Text = "Sale Order Status Summary"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtOrderNo As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents rdbSpecificOrder As System.Windows.Forms.RadioButton
    Friend WithEvents dtTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents rdbDateRange As System.Windows.Forms.RadioButton
    Friend WithEvents dtFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents rdbAllSalesOrders As System.Windows.Forms.RadioButton
    Friend WithEvents cmbCustomer As System.Windows.Forms.ComboBox
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chInvoices As System.Windows.Forms.CheckBox
    Friend WithEvents chDeliveryChallans As System.Windows.Forms.CheckBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents CtrlGrdBarFront As SimpleAccounts.CtrlGrdBar
    Friend WithEvents BtnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents Print As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents cmbSetTo As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
End Class

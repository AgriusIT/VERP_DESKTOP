<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGrdRptContactList
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
        Dim grd_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdServices_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGrdRptContactList))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tabContactList = New System.Windows.Forms.TabPage()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtOtherMobileNo = New System.Windows.Forms.TextBox()
        Me.tabServicesCustomer = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.grdServices = New Janus.Windows.GridEX.GridEX()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtSMSTemplateName = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbSMSTemplates = New System.Windows.Forms.ComboBox()
        Me.btnSaveTemplate = New System.Windows.Forms.Button()
        Me.cmbLanguage = New System.Windows.Forms.ComboBox()
        Me.btnClear = New System.Windows.Forms.Button()
        Me.btnSend = New System.Windows.Forms.Button()
        Me.txtMsgBody = New System.Windows.Forms.TextBox()
        Me.lblProcess = New System.Windows.Forms.Label()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.tabContactList.SuspendLayout()
        Me.tabServicesCustomer.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdServices, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh, Me.ToolStripSeparator1, Me.ToolStripButton1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(834, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 22)
        Me.btnRefresh.Text = "Refresh"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Image = Global.SimpleAccounts.My.Resources.Resources.sendcontactdetails
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(157, 22)
        Me.ToolStripButton1.Text = "Write Message"
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
        Me.grd.GroupByBoxVisible = False
        Me.grd.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.Location = New System.Drawing.Point(0, 0)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(825, 385)
        Me.grd.TabIndex = 0
        Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(4, -2)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(173, 35)
        Me.lblHeader.TabIndex = 2
        Me.lblHeader.Text = "Contact List"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(1, 53)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TabControl1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label8)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtSMSTemplateName)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label7)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label3)
        Me.SplitContainer1.Panel2.Controls.Add(Me.cmbSMSTemplates)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSaveTemplate)
        Me.SplitContainer1.Panel2.Controls.Add(Me.cmbLanguage)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnClear)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btnSend)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtMsgBody)
        Me.SplitContainer1.Size = New System.Drawing.Size(832, 689)
        Me.SplitContainer1.SplitterDistance = 450
        Me.SplitContainer1.TabIndex = 4
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tabContactList)
        Me.TabControl1.Controls.Add(Me.tabServicesCustomer)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(832, 450)
        Me.TabControl1.TabIndex = 5
        '
        'tabContactList
        '
        Me.tabContactList.Controls.Add(Me.Label2)
        Me.tabContactList.Controls.Add(Me.txtOtherMobileNo)
        Me.tabContactList.Controls.Add(Me.grd)
        Me.tabContactList.Location = New System.Drawing.Point(4, 29)
        Me.tabContactList.Name = "tabContactList"
        Me.tabContactList.Padding = New System.Windows.Forms.Padding(3)
        Me.tabContactList.Size = New System.Drawing.Size(824, 417)
        Me.tabContactList.TabIndex = 0
        Me.tabContactList.Text = "Contact List"
        Me.tabContactList.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 394)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(191, 20)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Others Contact Mobile No"
        '
        'txtOtherMobileNo
        '
        Me.txtOtherMobileNo.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtOtherMobileNo.Location = New System.Drawing.Point(200, 391)
        Me.txtOtherMobileNo.Name = "txtOtherMobileNo"
        Me.txtOtherMobileNo.Size = New System.Drawing.Size(621, 26)
        Me.txtOtherMobileNo.TabIndex = 2
        '
        'tabServicesCustomer
        '
        Me.tabServicesCustomer.Controls.Add(Me.GroupBox1)
        Me.tabServicesCustomer.Controls.Add(Me.btnShow)
        Me.tabServicesCustomer.Controls.Add(Me.grdServices)
        Me.tabServicesCustomer.Location = New System.Drawing.Point(4, 29)
        Me.tabServicesCustomer.Name = "tabServicesCustomer"
        Me.tabServicesCustomer.Padding = New System.Windows.Forms.Padding(3)
        Me.tabServicesCustomer.Size = New System.Drawing.Size(824, 417)
        Me.tabServicesCustomer.TabIndex = 1
        Me.tabServicesCustomer.Text = "Services Contact List"
        Me.tabServicesCustomer.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.cmbPeriod)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.dtpTo)
        Me.GroupBox1.Controls.Add(Me.dtpFrom)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(608, 52)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Date Range"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 25)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(54, 20)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Period"
        '
        'cmbPeriod
        '
        Me.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPeriod.FormattingEnabled = True
        Me.cmbPeriod.Items.AddRange(New Object() {"Today", "Yesterday", "Current Week", "Current Month", "Current Year"})
        Me.cmbPeriod.Location = New System.Drawing.Point(49, 22)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(99, 28)
        Me.cmbPeriod.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(371, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 20)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Date To"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(154, 25)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 20)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Date From"
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd/MMM/yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(423, 23)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(158, 26)
        Me.dtpTo.TabIndex = 5
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(216, 23)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(149, 26)
        Me.dtpFrom.TabIndex = 3
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(622, 26)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(75, 23)
        Me.btnShow.TabIndex = 6
        Me.btnShow.Text = "Show"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'grdServices
        '
        Me.grdServices.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdServices_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grdServices.DesignTimeLayout = grdServices_DesignTimeLayout
        Me.grdServices.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdServices.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdServices.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdServices.GroupByBoxVisible = False
        Me.grdServices.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdServices.Location = New System.Drawing.Point(2, 64)
        Me.grdServices.Name = "grdServices"
        Me.grdServices.RecordNavigator = True
        Me.grdServices.Size = New System.Drawing.Size(822, 354)
        Me.grdServices.TabIndex = 1
        Me.grdServices.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdServices.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdServices.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label8
        '
        Me.Label8.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(704, 195)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(121, 20)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "Template Name"
        '
        'txtSMSTemplateName
        '
        Me.txtSMSTemplateName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSMSTemplateName.Location = New System.Drawing.Point(707, 209)
        Me.txtSMSTemplateName.Name = "txtSMSTemplateName"
        Me.txtSMSTemplateName.Size = New System.Drawing.Size(122, 26)
        Me.txtSMSTemplateName.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(189, 195)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(163, 20)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Select SMS Template"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 195)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(130, 20)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Select Language"
        '
        'cmbSMSTemplates
        '
        Me.cmbSMSTemplates.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbSMSTemplates.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbSMSTemplates.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbSMSTemplates.FormattingEnabled = True
        Me.cmbSMSTemplates.Location = New System.Drawing.Point(192, 211)
        Me.cmbSMSTemplates.Name = "cmbSMSTemplates"
        Me.cmbSMSTemplates.Size = New System.Drawing.Size(185, 28)
        Me.cmbSMSTemplates.TabIndex = 5
        '
        'btnSaveTemplate
        '
        Me.btnSaveTemplate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSaveTemplate.Location = New System.Drawing.Point(572, 209)
        Me.btnSaveTemplate.Name = "btnSaveTemplate"
        Me.btnSaveTemplate.Size = New System.Drawing.Size(129, 23)
        Me.btnSaveTemplate.TabIndex = 4
        Me.btnSaveTemplate.Text = "Save as Template"
        Me.btnSaveTemplate.UseVisualStyleBackColor = True
        '
        'cmbLanguage
        '
        Me.cmbLanguage.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbLanguage.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbLanguage.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbLanguage.FormattingEnabled = True
        Me.cmbLanguage.Items.AddRange(New Object() {"English", "Urdu"})
        Me.cmbLanguage.Location = New System.Drawing.Point(3, 211)
        Me.cmbLanguage.Name = "cmbLanguage"
        Me.cmbLanguage.Size = New System.Drawing.Size(183, 28)
        Me.cmbLanguage.TabIndex = 1
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Location = New System.Drawing.Point(491, 209)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(75, 23)
        Me.btnClear.TabIndex = 3
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'btnSend
        '
        Me.btnSend.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSend.Location = New System.Drawing.Point(383, 209)
        Me.btnSend.Name = "btnSend"
        Me.btnSend.Size = New System.Drawing.Size(102, 23)
        Me.btnSend.TabIndex = 2
        Me.btnSend.Text = "Send to Draftbox"
        Me.btnSend.UseVisualStyleBackColor = True
        '
        'txtMsgBody
        '
        Me.txtMsgBody.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMsgBody.Location = New System.Drawing.Point(0, 2)
        Me.txtMsgBody.Multiline = True
        Me.txtMsgBody.Name = "txtMsgBody"
        Me.txtMsgBody.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtMsgBody.Size = New System.Drawing.Size(830, 187)
        Me.txtMsgBody.TabIndex = 0
        '
        'lblProcess
        '
        Me.lblProcess.AutoSize = True
        Me.lblProcess.BackColor = System.Drawing.SystemColors.Info
        Me.lblProcess.Location = New System.Drawing.Point(715, 33)
        Me.lblProcess.Name = "lblProcess"
        Me.lblProcess.Size = New System.Drawing.Size(157, 20)
        Me.lblProcess.TabIndex = 3
        Me.lblProcess.Text = "Message Sending....."
        Me.lblProcess.Visible = False
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(797, 0)
        Me.CtrlGrdBar1.MyGrid = Me.grd
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(38, 25)
        Me.CtrlGrdBar1.TabIndex = 1
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 25)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(834, 22)
        Me.pnlHeader.TabIndex = 7
        '
        'frmGrdRptContactList
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(834, 741)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.lblProcess)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmGrdRptContactList"
        Me.Text = "Contact List"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.tabContactList.ResumeLayout(False)
        Me.tabContactList.PerformLayout()
        Me.tabServicesCustomer.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdServices, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnSend As System.Windows.Forms.Button
    Friend WithEvents txtMsgBody As System.Windows.Forms.TextBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblProcess As System.Windows.Forms.Label
    Friend WithEvents cmbLanguage As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtOtherMobileNo As System.Windows.Forms.TextBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tabContactList As System.Windows.Forms.TabPage
    Friend WithEvents tabServicesCustomer As System.Windows.Forms.TabPage
    Friend WithEvents grdServices As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnSaveTemplate As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtSMSTemplateName As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbSMSTemplates As System.Windows.Forms.ComboBox
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

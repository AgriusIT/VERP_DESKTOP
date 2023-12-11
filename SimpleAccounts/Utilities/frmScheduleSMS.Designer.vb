<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmScheduleSMS
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim grdData_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmScheduleSMS))
        Dim grdSaved_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.grdData = New Janus.Windows.GridEX.GridEX()
        Me.lblVendorType = New System.Windows.Forms.Label()
        Me.chkVendorType = New System.Windows.Forms.CheckBox()
        Me.lblCustomerType = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.dtpSchDate = New System.Windows.Forms.DateTimePicker()
        Me.lblSchDate = New System.Windows.Forms.Label()
        Me.chkCustomerType = New System.Windows.Forms.CheckBox()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtProudctAndCategory = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTerminationCondition = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtWarrantyCondition = New System.Windows.Forms.TextBox()
        Me.lblTermandCondition = New System.Windows.Forms.Label()
        Me.txtTermAndCondition = New System.Windows.Forms.TextBox()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdSaved = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.BtnNew = New System.Windows.Forms.ToolStripButton()
        Me.BtnSave = New System.Windows.Forms.ToolStripButton()
        Me.lblProgress = New System.Windows.Forms.ToolStripLabel()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        CType(Me.grdData, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.grdData)
        Me.UltraTabPageControl1.Controls.Add(Me.lblVendorType)
        Me.UltraTabPageControl1.Controls.Add(Me.chkVendorType)
        Me.UltraTabPageControl1.Controls.Add(Me.lblCustomerType)
        Me.UltraTabPageControl1.Controls.Add(Me.TextBox2)
        Me.UltraTabPageControl1.Controls.Add(Me.Label6)
        Me.UltraTabPageControl1.Controls.Add(Me.dtpSchDate)
        Me.UltraTabPageControl1.Controls.Add(Me.lblSchDate)
        Me.UltraTabPageControl1.Controls.Add(Me.chkCustomerType)
        Me.UltraTabPageControl1.Controls.Add(Me.SplitContainer1)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(739, 563)
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.ForeColor = System.Drawing.Color.Black
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(739, 66)
        Me.pnlHeader.TabIndex = 40
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(28, 14)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(213, 36)
        Me.lblHeader.TabIndex = 28
        Me.lblHeader.Text = "SMS Schedule"
        Me.lblHeader.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'grdData
        '
        Me.grdData.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdData_DesignTimeLayout.LayoutString = resources.GetString("grdData_DesignTimeLayout.LayoutString")
        Me.grdData.DesignTimeLayout = grdData_DesignTimeLayout
        Me.grdData.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdData.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.grdData.GroupByBoxVisible = False
        Me.grdData.Location = New System.Drawing.Point(0, 222)
        Me.grdData.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdData.Name = "grdData"
        Me.grdData.RecordNavigator = True
        Me.grdData.Size = New System.Drawing.Size(739, 348)
        Me.grdData.TabIndex = 39
        Me.grdData.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        Me.grdData.TabStop = False
        Me.grdData.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdData.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdData.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'lblVendorType
        '
        Me.lblVendorType.AutoSize = True
        Me.lblVendorType.BackColor = System.Drawing.Color.Transparent
        Me.lblVendorType.Location = New System.Drawing.Point(18, 109)
        Me.lblVendorType.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblVendorType.Name = "lblVendorType"
        Me.lblVendorType.Size = New System.Drawing.Size(99, 20)
        Me.lblVendorType.TabIndex = 36
        Me.lblVendorType.Text = "Vendor Type"
        '
        'chkVendorType
        '
        Me.chkVendorType.AutoSize = True
        Me.chkVendorType.BackColor = System.Drawing.Color.Transparent
        Me.chkVendorType.Location = New System.Drawing.Point(144, 109)
        Me.chkVendorType.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkVendorType.Name = "chkVendorType"
        Me.chkVendorType.Size = New System.Drawing.Size(124, 24)
        Me.chkVendorType.TabIndex = 35
        Me.chkVendorType.Text = "Enable SMS"
        Me.chkVendorType.UseVisualStyleBackColor = False
        '
        'lblCustomerType
        '
        Me.lblCustomerType.AutoSize = True
        Me.lblCustomerType.BackColor = System.Drawing.Color.Transparent
        Me.lblCustomerType.Location = New System.Drawing.Point(18, 74)
        Me.lblCustomerType.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCustomerType.Name = "lblCustomerType"
        Me.lblCustomerType.Size = New System.Drawing.Size(116, 20)
        Me.lblCustomerType.TabIndex = 34
        Me.lblCustomerType.Text = "Customer Type"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(144, 182)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(286, 26)
        Me.TextBox2.TabIndex = 33
        Me.TextBox2.Text = "Pending"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(18, 186)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 20)
        Me.Label6.TabIndex = 32
        Me.Label6.Text = "Status"
        '
        'dtpSchDate
        '
        Me.dtpSchDate.CustomFormat = "dd/MMM/yyyy hh:mm:ss tt"
        Me.dtpSchDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpSchDate.Location = New System.Drawing.Point(144, 142)
        Me.dtpSchDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpSchDate.Name = "dtpSchDate"
        Me.dtpSchDate.Size = New System.Drawing.Size(286, 26)
        Me.dtpSchDate.TabIndex = 31
        Me.dtpSchDate.Value = New Date(2014, 5, 19, 0, 0, 0, 0)
        '
        'lblSchDate
        '
        Me.lblSchDate.AutoSize = True
        Me.lblSchDate.BackColor = System.Drawing.Color.Transparent
        Me.lblSchDate.Location = New System.Drawing.Point(18, 145)
        Me.lblSchDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSchDate.Name = "lblSchDate"
        Me.lblSchDate.Size = New System.Drawing.Size(115, 20)
        Me.lblSchDate.TabIndex = 30
        Me.lblSchDate.Text = "Schedule Date"
        '
        'chkCustomerType
        '
        Me.chkCustomerType.AutoSize = True
        Me.chkCustomerType.BackColor = System.Drawing.Color.Transparent
        Me.chkCustomerType.Location = New System.Drawing.Point(144, 74)
        Me.chkCustomerType.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkCustomerType.Name = "chkCustomerType"
        Me.chkCustomerType.Size = New System.Drawing.Size(124, 24)
        Me.chkCustomerType.TabIndex = 29
        Me.chkCustomerType.Text = "Enable SMS"
        Me.chkCustomerType.UseVisualStyleBackColor = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(2, 628)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label4)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtProudctAndCategory)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label3)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtTerminationCondition)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtWarrantyCondition)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lblTermandCondition)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtTermAndCondition)
        Me.SplitContainer1.Panel2Collapsed = True
        Me.SplitContainer1.Size = New System.Drawing.Size(736, 98)
        Me.SplitContainer1.SplitterDistance = 25
        Me.SplitContainer1.SplitterWidth = 6
        Me.SplitContainer1.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(758, 37)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(165, 20)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Product And Category"
        '
        'txtProudctAndCategory
        '
        Me.txtProudctAndCategory.Location = New System.Drawing.Point(972, 32)
        Me.txtProudctAndCategory.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtProudctAndCategory.Multiline = True
        Me.txtProudctAndCategory.Name = "txtProudctAndCategory"
        Me.txtProudctAndCategory.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtProudctAndCategory.Size = New System.Drawing.Size(492, 96)
        Me.txtProudctAndCategory.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(758, 145)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(193, 20)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Termination of Agreement"
        '
        'txtTerminationCondition
        '
        Me.txtTerminationCondition.Location = New System.Drawing.Point(972, 140)
        Me.txtTerminationCondition.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtTerminationCondition.Multiline = True
        Me.txtTerminationCondition.Name = "txtTerminationCondition"
        Me.txtTerminationCondition.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTerminationCondition.Size = New System.Drawing.Size(492, 96)
        Me.txtTerminationCondition.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(30, 145)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(73, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Warranty"
        '
        'txtWarrantyCondition
        '
        Me.txtWarrantyCondition.Location = New System.Drawing.Point(190, 140)
        Me.txtWarrantyCondition.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtWarrantyCondition.Multiline = True
        Me.txtWarrantyCondition.Name = "txtWarrantyCondition"
        Me.txtWarrantyCondition.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtWarrantyCondition.Size = New System.Drawing.Size(492, 96)
        Me.txtWarrantyCondition.TabIndex = 3
        '
        'lblTermandCondition
        '
        Me.lblTermandCondition.AutoSize = True
        Me.lblTermandCondition.Location = New System.Drawing.Point(27, 32)
        Me.lblTermandCondition.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTermandCondition.Name = "lblTermandCondition"
        Me.lblTermandCondition.Size = New System.Drawing.Size(149, 20)
        Me.lblTermandCondition.TabIndex = 0
        Me.lblTermandCondition.Text = "Term And Condition"
        '
        'txtTermAndCondition
        '
        Me.txtTermAndCondition.Location = New System.Drawing.Point(190, 32)
        Me.txtTermAndCondition.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtTermAndCondition.Multiline = True
        Me.txtTermAndCondition.Name = "txtTermAndCondition"
        Me.txtTermAndCondition.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTermAndCondition.Size = New System.Drawing.Size(492, 96)
        Me.txtTermAndCondition.TabIndex = 1
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grdSaved)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(739, 563)
        '
        'grdSaved
        '
        Me.grdSaved.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        grdSaved_DesignTimeLayout.LayoutString = resources.GetString("grdSaved_DesignTimeLayout.LayoutString")
        Me.grdSaved.DesignTimeLayout = grdSaved_DesignTimeLayout
        Me.grdSaved.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSaved.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.grdSaved.GroupByBoxVisible = False
        Me.grdSaved.Location = New System.Drawing.Point(0, 0)
        Me.grdSaved.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdSaved.Name = "grdSaved"
        Me.grdSaved.RecordNavigator = True
        Me.grdSaved.Size = New System.Drawing.Size(739, 563)
        Me.grdSaved.TabIndex = 40
        Me.grdSaved.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        Me.grdSaved.TabStop = False
        Me.grdSaved.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdSaved.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BtnNew, Me.BtnSave, Me.lblProgress, Me.btnRefresh})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(741, 32)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'BtnNew
        '
        Me.BtnNew.Image = CType(resources.GetObject("BtnNew.Image"), System.Drawing.Image)
        Me.BtnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnNew.Name = "BtnNew"
        Me.BtnNew.Size = New System.Drawing.Size(75, 29)
        Me.BtnNew.Text = "&New"
        '
        'BtnSave
        '
        Me.BtnSave.Image = CType(resources.GetObject("BtnSave.Image"), System.Drawing.Image)
        Me.BtnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(77, 29)
        Me.BtnSave.Text = "&Save"
        '
        'lblProgress
        '
        Me.lblProgress.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(0, 29)
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = CType(resources.GetObject("btnRefresh.Image"), System.Drawing.Image)
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 29)
        Me.btnRefresh.Text = "&Refresh"
        Me.btnRefresh.ToolTipText = "Refresh"
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 32)
        Me.UltraTabControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(741, 590)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 2
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "SMS Configuration"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "History"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.VisualStudio2005
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(739, 563)
        '
        'Timer1
        '
        '
        'BackgroundWorker1
        '
        '
        'frmScheduleSMS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(741, 622)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmScheduleSMS"
        Me.Text = "frmScheduleSMS"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.grdData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents BtnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblProgress As System.Windows.Forms.ToolStripLabel
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtProudctAndCategory As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtTerminationCondition As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtWarrantyCondition As System.Windows.Forms.TextBox
    Friend WithEvents lblTermandCondition As System.Windows.Forms.Label
    Friend WithEvents txtTermAndCondition As System.Windows.Forms.TextBox
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents lblVendorType As System.Windows.Forms.Label
    Friend WithEvents chkVendorType As System.Windows.Forms.CheckBox
    Friend WithEvents lblCustomerType As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dtpSchDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblSchDate As System.Windows.Forms.Label
    Friend WithEvents chkCustomerType As System.Windows.Forms.CheckBox
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents grdData As Janus.Windows.GridEX.GridEX
    Friend WithEvents grdSaved As Janus.Windows.GridEX.GridEX
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

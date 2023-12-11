<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGrdRptAgingPayables
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
        Dim grdInvoiceWise_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGrdRptAgingPayables))
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraTab3 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.chkIncludeUnPosted = New System.Windows.Forms.CheckBox()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnAddFormate = New System.Windows.Forms.ToolStripButton()
        Me.cmbPropertyType = New System.Windows.Forms.ComboBox()
        Me.cmbFormate = New System.Windows.Forms.ComboBox()
        Me.lnkRefresh = New System.Windows.Forms.LinkLabel()
        Me.lblPropertyType = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdInvoiceWise = New Janus.Windows.GridEX.GridEX()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.ToolStrip3 = New System.Windows.Forms.ToolStrip()
        Me.btnBack = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.UltraTabPageControl3 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.chkGetPaidUnpaid = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.grdSchPayables = New Janus.Windows.GridEX.GridEX()
        Me.btnShowSchPayables = New System.Windows.Forms.Button()
        Me.dtpSchDate = New System.Windows.Forms.DateTimePicker()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.btnPaid = New System.Windows.Forms.ToolStripButton()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.CtrlGrdBar2 = New SimpleAccounts.CtrlGrdBar()
        Me.CtrlGrdBar3 = New SimpleAccounts.CtrlGrdBar()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdInvoiceWise, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip3.SuspendLayout()
        Me.UltraTabPageControl3.SuspendLayout()
        CType(Me.grdSchPayables, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip2.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.CtrlGrdBar1)
        Me.UltraTabPageControl1.Controls.Add(Me.grd)
        Me.UltraTabPageControl1.Controls.Add(Me.chkIncludeUnPosted)
        Me.UltraTabPageControl1.Controls.Add(Me.ToolStrip1)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbPropertyType)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbFormate)
        Me.UltraTabPageControl1.Controls.Add(Me.lnkRefresh)
        Me.UltraTabPageControl1.Controls.Add(Me.lblPropertyType)
        Me.UltraTabPageControl1.Controls.Add(Me.Label2)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1198, 735)
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 32)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1198, 69)
        Me.pnlHeader.TabIndex = 68
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(16, 14)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(225, 36)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Aging Payables"
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
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.grd.Location = New System.Drawing.Point(3, 160)
        Me.grd.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(1195, 583)
        Me.grd.TabIndex = 2
        Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'chkIncludeUnPosted
        '
        Me.chkIncludeUnPosted.AutoSize = True
        Me.chkIncludeUnPosted.Checked = True
        Me.chkIncludeUnPosted.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkIncludeUnPosted.Location = New System.Drawing.Point(934, 120)
        Me.chkIncludeUnPosted.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkIncludeUnPosted.Name = "chkIncludeUnPosted"
        Me.chkIncludeUnPosted.Size = New System.Drawing.Size(225, 24)
        Me.chkIncludeUnPosted.TabIndex = 7
        Me.chkIncludeUnPosted.Text = "Include Unposted Voucher"
        Me.chkIncludeUnPosted.UseVisualStyleBackColor = True
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh, Me.btnPrint, Me.ToolStripSeparator1, Me.btnAddFormate})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1198, 32)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 29)
        Me.btnRefresh.Text = "Refresh"
        '
        'btnPrint
        '
        Me.btnPrint.Image = Global.SimpleAccounts.My.Resources.Resources.print
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(76, 29)
        Me.btnPrint.Text = "Print"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 32)
        '
        'btnAddFormate
        '
        Me.btnAddFormate.Image = Global.SimpleAccounts.My.Resources.Resources.addIcon
        Me.btnAddFormate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAddFormate.Name = "btnAddFormate"
        Me.btnAddFormate.Size = New System.Drawing.Size(178, 29)
        Me.btnAddFormate.Text = "Add aging layout"
        '
        'cmbPropertyType
        '
        Me.cmbPropertyType.FormattingEnabled = True
        Me.cmbPropertyType.Location = New System.Drawing.Point(188, 117)
        Me.cmbPropertyType.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbPropertyType.Name = "cmbPropertyType"
        Me.cmbPropertyType.Size = New System.Drawing.Size(241, 28)
        Me.cmbPropertyType.TabIndex = 3
        '
        'cmbFormate
        '
        Me.cmbFormate.FormattingEnabled = True
        Me.cmbFormate.Location = New System.Drawing.Point(530, 117)
        Me.cmbFormate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbFormate.Name = "cmbFormate"
        Me.cmbFormate.Size = New System.Drawing.Size(300, 28)
        Me.cmbFormate.TabIndex = 3
        '
        'lnkRefresh
        '
        Me.lnkRefresh.AutoSize = True
        Me.lnkRefresh.Location = New System.Drawing.Point(840, 122)
        Me.lnkRefresh.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lnkRefresh.Name = "lnkRefresh"
        Me.lnkRefresh.Size = New System.Drawing.Size(66, 20)
        Me.lnkRefresh.TabIndex = 5
        Me.lnkRefresh.TabStop = True
        Me.lnkRefresh.Text = "Refresh"
        '
        'lblPropertyType
        '
        Me.lblPropertyType.AutoSize = True
        Me.lblPropertyType.Location = New System.Drawing.Point(69, 122)
        Me.lblPropertyType.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPropertyType.Name = "lblPropertyType"
        Me.lblPropertyType.Size = New System.Drawing.Size(106, 20)
        Me.lblPropertyType.TabIndex = 4
        Me.lblPropertyType.Text = "Property Type"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(459, 122)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(57, 20)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Layout"
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.CtrlGrdBar2)
        Me.UltraTabPageControl2.Controls.Add(Me.Button1)
        Me.UltraTabPageControl2.Controls.Add(Me.DateTimePicker2)
        Me.UltraTabPageControl2.Controls.Add(Me.DateTimePicker1)
        Me.UltraTabPageControl2.Controls.Add(Me.grdInvoiceWise)
        Me.UltraTabPageControl2.Controls.Add(Me.ToolStrip3)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1198, 735)
        '
        'grdInvoiceWise
        '
        Me.grdInvoiceWise.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdInvoiceWise.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdInvoiceWise_DesignTimeLayout.LayoutString = resources.GetString("grdInvoiceWise_DesignTimeLayout.LayoutString")
        Me.grdInvoiceWise.DesignTimeLayout = grdInvoiceWise_DesignTimeLayout
        Me.grdInvoiceWise.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdInvoiceWise.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdInvoiceWise.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdInvoiceWise.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdInvoiceWise.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.grdInvoiceWise.Location = New System.Drawing.Point(0, 98)
        Me.grdInvoiceWise.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdInvoiceWise.Name = "grdInvoiceWise"
        Me.grdInvoiceWise.RecordNavigator = True
        Me.grdInvoiceWise.Size = New System.Drawing.Size(1195, 645)
        Me.grdInvoiceWise.TabIndex = 12
        Me.grdInvoiceWise.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdInvoiceWise.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdInvoiceWise.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(400, 51)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(112, 35)
        Me.Button1.TabIndex = 15
        Me.Button1.Text = "Show"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.CustomFormat = "dd/MMM/yyyy"
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2.Location = New System.Drawing.Point(208, 54)
        Me.DateTimePicker2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(181, 26)
        Me.DateTimePicker2.TabIndex = 14
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd/MMM/yyyy"
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(16, 54)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(181, 26)
        Me.DateTimePicker1.TabIndex = 13
        '
        'ToolStrip3
        '
        Me.ToolStrip3.AutoSize = False
        Me.ToolStrip3.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnBack, Me.ToolStripButton2})
        Me.ToolStrip3.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip3.Name = "ToolStrip3"
        Me.ToolStrip3.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip3.Size = New System.Drawing.Size(1198, 38)
        Me.ToolStrip3.TabIndex = 11
        Me.ToolStrip3.Text = "ToolStrip3"
        '
        'btnBack
        '
        Me.btnBack.Image = Global.SimpleAccounts.My.Resources.Resources.back
        Me.btnBack.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(76, 35)
        Me.btnBack.Text = "Back"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Image = Global.SimpleAccounts.My.Resources.Resources.print
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(76, 35)
        Me.ToolStripButton2.Text = "Print"
        '
        'UltraTabPageControl3
        '
        Me.UltraTabPageControl3.Controls.Add(Me.chkGetPaidUnpaid)
        Me.UltraTabPageControl3.Controls.Add(Me.Label3)
        Me.UltraTabPageControl3.Controls.Add(Me.CtrlGrdBar3)
        Me.UltraTabPageControl3.Controls.Add(Me.btnShowSchPayables)
        Me.UltraTabPageControl3.Controls.Add(Me.dtpSchDate)
        Me.UltraTabPageControl3.Controls.Add(Me.grdSchPayables)
        Me.UltraTabPageControl3.Controls.Add(Me.ToolStrip2)
        Me.UltraTabPageControl3.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl3.Name = "UltraTabPageControl3"
        Me.UltraTabPageControl3.Size = New System.Drawing.Size(1198, 735)
        '
        'chkGetPaidUnpaid
        '
        Me.chkGetPaidUnpaid.AutoSize = True
        Me.chkGetPaidUnpaid.Location = New System.Drawing.Point(321, 57)
        Me.chkGetPaidUnpaid.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkGetPaidUnpaid.Name = "chkGetPaidUnpaid"
        Me.chkGetPaidUnpaid.Size = New System.Drawing.Size(122, 24)
        Me.chkGetPaidUnpaid.TabIndex = 22
        Me.chkGetPaidUnpaid.Text = "Include Paid"
        Me.chkGetPaidUnpaid.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(16, 58)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(100, 20)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "Due Date Till"
        '
        'grdSchPayables
        '
        Me.grdSchPayables.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSchPayables.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdSchPayables.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdSchPayables.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdSchPayables.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSchPayables.Location = New System.Drawing.Point(0, 98)
        Me.grdSchPayables.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdSchPayables.Name = "grdSchPayables"
        Me.grdSchPayables.Size = New System.Drawing.Size(1195, 646)
        Me.grdSchPayables.TabIndex = 17
        Me.grdSchPayables.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdSchPayables.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdSchPayables.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'btnShowSchPayables
        '
        Me.btnShowSchPayables.Location = New System.Drawing.Point(477, 51)
        Me.btnShowSchPayables.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnShowSchPayables.Name = "btnShowSchPayables"
        Me.btnShowSchPayables.Size = New System.Drawing.Size(112, 35)
        Me.btnShowSchPayables.TabIndex = 19
        Me.btnShowSchPayables.Text = "Show"
        Me.btnShowSchPayables.UseVisualStyleBackColor = True
        '
        'dtpSchDate
        '
        Me.dtpSchDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpSchDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpSchDate.Location = New System.Drawing.Point(129, 54)
        Me.dtpSchDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpSchDate.Name = "dtpSchDate"
        Me.dtpSchDate.Size = New System.Drawing.Size(181, 26)
        Me.dtpSchDate.TabIndex = 18
        '
        'ToolStrip2
        '
        Me.ToolStrip2.AutoSize = False
        Me.ToolStrip2.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnPaid})
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip2.Size = New System.Drawing.Size(1198, 38)
        Me.ToolStrip2.TabIndex = 16
        Me.ToolStrip2.Text = "ToolStrip2"
        '
        'btnPaid
        '
        Me.btnPaid.Image = Global.SimpleAccounts.My.Resources.Resources._20604_24_button_ok_icon
        Me.btnPaid.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPaid.Name = "btnPaid"
        Me.btnPaid.Size = New System.Drawing.Size(73, 35)
        Me.btnPaid.Text = "Paid"
        '
        'BackgroundWorker1
        '
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl3)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.UltraTabControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(1200, 762)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.VisualStudio
        Me.UltraTabControl1.TabIndex = 8
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        Appearance1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        UltraTab1.Appearance = Appearance1
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Payables"
        Appearance2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        UltraTab2.Appearance = Appearance2
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "Invoice Wise"
        Appearance3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        UltraTab3.Appearance = Appearance3
        UltraTab3.TabPage = Me.UltraTabPageControl3
        UltraTab3.Text = "Scheduled Payables"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2, UltraTab3})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.VisualStudio2005
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1198, 735)
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(1143, 0)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar1.MyGrid = Me.grd
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(57, 38)
        Me.CtrlGrdBar1.TabIndex = 8
        '
        'CtrlGrdBar2
        '
        Me.CtrlGrdBar2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar2.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar2.Email = Nothing
        Me.CtrlGrdBar2.FormName = Me
        Me.CtrlGrdBar2.Location = New System.Drawing.Point(1143, -2)
        Me.CtrlGrdBar2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar2.MyGrid = Me.grdInvoiceWise
        Me.CtrlGrdBar2.Name = "CtrlGrdBar2"
        Me.CtrlGrdBar2.Size = New System.Drawing.Size(57, 38)
        Me.CtrlGrdBar2.TabIndex = 16
        '
        'CtrlGrdBar3
        '
        Me.CtrlGrdBar3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar3.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar3.Email = Nothing
        Me.CtrlGrdBar3.FormName = Me
        Me.CtrlGrdBar3.Location = New System.Drawing.Point(1143, 0)
        Me.CtrlGrdBar3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar3.MyGrid = Me.grdSchPayables
        Me.CtrlGrdBar3.Name = "CtrlGrdBar3"
        Me.CtrlGrdBar3.Size = New System.Drawing.Size(57, 38)
        Me.CtrlGrdBar3.TabIndex = 20
        '
        'frmGrdRptAgingPayables
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1200, 762)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmGrdRptAgingPayables"
        Me.Text = "Aging Payables"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdInvoiceWise, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip3.ResumeLayout(False)
        Me.ToolStrip3.PerformLayout()
        Me.UltraTabPageControl3.ResumeLayout(False)
        Me.UltraTabPageControl3.PerformLayout()
        CType(Me.grdSchPayables, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbFormate As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lnkRefresh As System.Windows.Forms.LinkLabel
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnAddFormate As System.Windows.Forms.ToolStripButton
    Friend WithEvents chkIncludeUnPosted As System.Windows.Forms.CheckBox
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents grdInvoiceWise As Janus.Windows.GridEX.GridEX
    Friend WithEvents ToolStrip3 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnBack As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents CtrlGrdBar2 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents UltraTabPageControl3 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents CtrlGrdBar3 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents btnShowSchPayables As System.Windows.Forms.Button
    Friend WithEvents dtpSchDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents grdSchPayables As Janus.Windows.GridEX.GridEX
    Friend WithEvents ToolStrip2 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnPaid As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkGetPaidUnpaid As System.Windows.Forms.CheckBox
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents cmbPropertyType As System.Windows.Forms.ComboBox
    Friend WithEvents lblPropertyType As System.Windows.Forms.Label
End Class

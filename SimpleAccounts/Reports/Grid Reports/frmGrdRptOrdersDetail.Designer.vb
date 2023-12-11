<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGrdRptOrdersDetail
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
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance19 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance20 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance21 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance22 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance23 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance24 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.OptPurchasOrder = New System.Windows.Forms.RadioButton()
        Me.OptSalesOrder = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rbtPack = New System.Windows.Forms.RadioButton()
        Me.rbtLoose = New System.Windows.Forms.RadioButton()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.cmbAccount = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.LblType = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.tbProgressbar = New System.Windows.Forms.ToolStripProgressBar()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.cmbAccount, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox3)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox2)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox1)
        Me.UltraTabPageControl1.Controls.Add(Me.ToolStrip1)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1404, 551)
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 48)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1404, 65)
        Me.pnlHeader.TabIndex = 83
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(26, 14)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(236, 41)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Orders Detail "
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.OptPurchasOrder)
        Me.GroupBox3.Controls.Add(Me.OptSalesOrder)
        Me.GroupBox3.Location = New System.Drawing.Point(354, 115)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox3.Size = New System.Drawing.Size(465, 71)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Order Type"
        '
        'OptPurchasOrder
        '
        Me.OptPurchasOrder.AutoSize = True
        Me.OptPurchasOrder.Location = New System.Drawing.Point(228, 29)
        Me.OptPurchasOrder.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.OptPurchasOrder.Name = "OptPurchasOrder"
        Me.OptPurchasOrder.Size = New System.Drawing.Size(145, 24)
        Me.OptPurchasOrder.TabIndex = 1
        Me.OptPurchasOrder.Text = "Purchase Order"
        Me.OptPurchasOrder.UseVisualStyleBackColor = True
        '
        'OptSalesOrder
        '
        Me.OptSalesOrder.AutoSize = True
        Me.OptSalesOrder.Checked = True
        Me.OptSalesOrder.Location = New System.Drawing.Point(99, 29)
        Me.OptSalesOrder.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.OptSalesOrder.Name = "OptSalesOrder"
        Me.OptSalesOrder.Size = New System.Drawing.Size(118, 24)
        Me.OptSalesOrder.TabIndex = 0
        Me.OptSalesOrder.TabStop = True
        Me.OptSalesOrder.Text = "Sales Order"
        Me.OptSalesOrder.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.rbtPack)
        Me.GroupBox2.Controls.Add(Me.rbtLoose)
        Me.GroupBox2.Controls.Add(Me.btnShow)
        Me.GroupBox2.Controls.Add(Me.cmbAccount)
        Me.GroupBox2.Controls.Add(Me.LblType)
        Me.GroupBox2.Location = New System.Drawing.Point(354, 195)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Size = New System.Drawing.Size(465, 122)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        '
        'rbtPack
        '
        Me.rbtPack.AutoSize = True
        Me.rbtPack.Location = New System.Drawing.Point(228, 78)
        Me.rbtPack.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtPack.Name = "rbtPack"
        Me.rbtPack.Size = New System.Drawing.Size(69, 24)
        Me.rbtPack.TabIndex = 4
        Me.rbtPack.Text = "Pack"
        Me.rbtPack.UseVisualStyleBackColor = True
        '
        'rbtLoose
        '
        Me.rbtLoose.AutoSize = True
        Me.rbtLoose.Checked = True
        Me.rbtLoose.Location = New System.Drawing.Point(99, 78)
        Me.rbtLoose.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtLoose.Name = "rbtLoose"
        Me.rbtLoose.Size = New System.Drawing.Size(78, 24)
        Me.rbtLoose.TabIndex = 3
        Me.rbtLoose.TabStop = True
        Me.rbtLoose.Text = "Loose"
        Me.rbtLoose.UseVisualStyleBackColor = True
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(344, 74)
        Me.btnShow.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(112, 35)
        Me.btnShow.TabIndex = 2
        Me.btnShow.Text = "Show"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'cmbAccount
        '
        Me.cmbAccount.CheckedListSettings.CheckStateMember = ""
        Appearance4.BackColor = System.Drawing.SystemColors.Window
        Appearance4.BorderColor = System.Drawing.SystemColors.InactiveCaption
        Me.cmbAccount.DisplayLayout.Appearance = Appearance4
        Me.cmbAccount.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.cmbAccount.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Appearance1.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance1.BorderColor = System.Drawing.SystemColors.Window
        Me.cmbAccount.DisplayLayout.GroupByBox.Appearance = Appearance1
        Appearance2.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cmbAccount.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance2
        Me.cmbAccount.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance3.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance3.BackColor2 = System.Drawing.SystemColors.Control
        Appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance3.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cmbAccount.DisplayLayout.GroupByBox.PromptAppearance = Appearance3
        Me.cmbAccount.DisplayLayout.MaxColScrollRegions = 1
        Me.cmbAccount.DisplayLayout.MaxRowScrollRegions = 1
        Appearance12.BackColor = System.Drawing.SystemColors.Window
        Appearance12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmbAccount.DisplayLayout.Override.ActiveCellAppearance = Appearance12
        Appearance19.BackColor = System.Drawing.SystemColors.Highlight
        Appearance19.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.cmbAccount.DisplayLayout.Override.ActiveRowAppearance = Appearance19
        Me.cmbAccount.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.cmbAccount.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance20.BackColor = System.Drawing.SystemColors.Window
        Me.cmbAccount.DisplayLayout.Override.CardAreaAppearance = Appearance20
        Appearance5.BorderColor = System.Drawing.Color.Silver
        Appearance5.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.cmbAccount.DisplayLayout.Override.CellAppearance = Appearance5
        Me.cmbAccount.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.cmbAccount.DisplayLayout.Override.CellPadding = 0
        Appearance21.BackColor = System.Drawing.SystemColors.Control
        Appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance21.BorderColor = System.Drawing.SystemColors.Window
        Me.cmbAccount.DisplayLayout.Override.GroupByRowAppearance = Appearance21
        Appearance22.TextHAlignAsString = "Left"
        Me.cmbAccount.DisplayLayout.Override.HeaderAppearance = Appearance22
        Me.cmbAccount.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.cmbAccount.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance23.BackColor = System.Drawing.SystemColors.Window
        Appearance23.BorderColor = System.Drawing.Color.Silver
        Me.cmbAccount.DisplayLayout.Override.RowAppearance = Appearance23
        Me.cmbAccount.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance24.BackColor = System.Drawing.SystemColors.ControlLight
        Me.cmbAccount.DisplayLayout.Override.TemplateAddRowAppearance = Appearance24
        Me.cmbAccount.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbAccount.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbAccount.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.cmbAccount.LimitToList = True
        Me.cmbAccount.Location = New System.Drawing.Point(99, 31)
        Me.cmbAccount.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbAccount.Name = "cmbAccount"
        Me.cmbAccount.Size = New System.Drawing.Size(357, 29)
        Me.cmbAccount.TabIndex = 1
        '
        'LblType
        '
        Me.LblType.AutoSize = True
        Me.LblType.Location = New System.Drawing.Point(14, 38)
        Me.LblType.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblType.Name = "LblType"
        Me.LblType.Size = New System.Drawing.Size(78, 20)
        Me.LblType.TabIndex = 0
        Me.LblType.Text = "Customer"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cmbStatus)
        Me.GroupBox1.Controls.Add(Me.dtpFrom)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.dtpTo)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 115)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(327, 202)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Date Range"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 125)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 20)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "S&tatus"
        '
        'cmbStatus
        '
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Location = New System.Drawing.Point(82, 120)
        Me.cmbStatus.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(220, 28)
        Me.cmbStatus.TabIndex = 8
        '
        'dtpFrom
        '
        Me.dtpFrom.Checked = False
        Me.dtpFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(82, 40)
        Me.dtpFrom.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.ShowCheckBox = True
        Me.dtpFrom.Size = New System.Drawing.Size(220, 26)
        Me.dtpFrom.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 40)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "From "
        '
        'dtpTo
        '
        Me.dtpTo.Checked = False
        Me.dtpTo.CustomFormat = "dd/MMM/yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(82, 80)
        Me.dtpTo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.ShowCheckBox = True
        Me.dtpTo.Size = New System.Drawing.Size(220, 26)
        Me.dtpTo.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 80)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "To"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh, Me.tbProgressbar})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1404, 48)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 45)
        Me.btnRefresh.Text = "Refresh"
        '
        'tbProgressbar
        '
        Me.tbProgressbar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.tbProgressbar.Name = "tbProgressbar"
        Me.tbProgressbar.Size = New System.Drawing.Size(300, 45)
        Me.tbProgressbar.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grd)
        Me.UltraTabPageControl2.Controls.Add(Me.CtrlGrdBar1)
        Me.UltraTabPageControl2.Controls.Add(Me.ToolStrip2)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1404, 551)
        '
        'grd
        '
        Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.grd.Location = New System.Drawing.Point(0, 42)
        Me.grd.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(1404, 518)
        Me.grd.TabIndex = 2
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(1349, 0)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar1.MyGrid = Me.grd
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(57, 38)
        Me.CtrlGrdBar1.TabIndex = 1
        '
        'ToolStrip2
        '
        Me.ToolStrip2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ToolStrip2.AutoSize = False
        Me.ToolStrip2.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip2.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip2.Size = New System.Drawing.Size(1349, 38)
        Me.ToolStrip2.TabIndex = 0
        Me.ToolStrip2.Text = "ToolStrip2"
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.UltraTabControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(1406, 578)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 1
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Criteria"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "Result"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.VisualStudio2005
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1404, 551)
        '
        'frmGrdRptOrdersDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1406, 578)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmGrdRptOrdersDetail"
        Me.Text = "frmGrdRptOrdersDetail"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.cmbAccount, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents cmbAccount As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents LblType As System.Windows.Forms.Label
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents tbProgressbar As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents ToolStrip2 As System.Windows.Forms.ToolStrip
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents OptPurchasOrder As System.Windows.Forms.RadioButton
    Friend WithEvents OptSalesOrder As System.Windows.Forms.RadioButton
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents rbtPack As System.Windows.Forms.RadioButton
    Friend WithEvents rbtLoose As System.Windows.Forms.RadioButton
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGrdArticleLedgerByPack
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
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim Appearance16 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance15 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance24 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance19 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance18 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance17 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance21 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance23 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance22 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance20 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGrdArticleLedgerByPack))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.rbtByQty = New System.Windows.Forms.RadioButton()
        Me.rbtByAmount = New System.Windows.Forms.RadioButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rbtnByCode = New System.Windows.Forms.RadioButton()
        Me.rbtnByName = New System.Windows.Forms.RadioButton()
        Me.cmbItems = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbLocation = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.cmbItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1388, 38)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 35)
        Me.btnRefresh.Text = "Refresh"
        '
        'GridEX1
        '
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        GridEX1_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX1.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.Location = New System.Drawing.Point(2, 437)
        Me.GridEX1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.Size = New System.Drawing.Size(1384, 471)
        Me.GridEX1.TabIndex = 4
        Me.GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(18, 11)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(302, 36)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Item Ledger By Pack"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.cmbPeriod)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.dtpTo)
        Me.GroupBox1.Controls.Add(Me.dtpFrom)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 115)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(378, 278)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Date Range"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(16, 31)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
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
        Me.cmbPeriod.Location = New System.Drawing.Point(136, 26)
        Me.cmbPeriod.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(222, 28)
        Me.cmbPeriod.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(16, 114)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 20)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Date To"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(16, 72)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 20)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Date From"
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd/MMM/yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(136, 109)
        Me.dtpTo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(222, 26)
        Me.dtpTo.TabIndex = 5
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(136, 68)
        Me.dtpFrom.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(222, 26)
        Me.dtpFrom.TabIndex = 3
        '
        'btnGenerate
        '
        Me.btnGenerate.Location = New System.Drawing.Point(364, 232)
        Me.btnGenerate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(112, 35)
        Me.btnGenerate.TabIndex = 7
        Me.btnGenerate.Text = "Generate"
        Me.btnGenerate.UseVisualStyleBackColor = True
        '
        'rbtByQty
        '
        Me.rbtByQty.AutoSize = True
        Me.rbtByQty.Checked = True
        Me.rbtByQty.Location = New System.Drawing.Point(168, 29)
        Me.rbtByQty.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtByQty.Name = "rbtByQty"
        Me.rbtByQty.Size = New System.Drawing.Size(80, 24)
        Me.rbtByQty.TabIndex = 5
        Me.rbtByQty.TabStop = True
        Me.rbtByQty.Text = "By Qty"
        Me.rbtByQty.UseVisualStyleBackColor = True
        '
        'rbtByAmount
        '
        Me.rbtByAmount.AutoSize = True
        Me.rbtByAmount.Location = New System.Drawing.Point(261, 29)
        Me.rbtByAmount.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtByAmount.Name = "rbtByAmount"
        Me.rbtByAmount.Size = New System.Drawing.Size(112, 24)
        Me.rbtByAmount.TabIndex = 6
        Me.rbtByAmount.Text = "By Amount"
        Me.rbtByAmount.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.GroupBox3)
        Me.GroupBox2.Controls.Add(Me.Panel1)
        Me.GroupBox2.Controls.Add(Me.cmbItems)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.cmbLocation)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.btnGenerate)
        Me.GroupBox2.Location = New System.Drawing.Point(405, 115)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Size = New System.Drawing.Size(490, 278)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.rbtByQty)
        Me.GroupBox3.Controls.Add(Me.rbtByAmount)
        Me.GroupBox3.Location = New System.Drawing.Point(93, 157)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox3.Size = New System.Drawing.Size(384, 66)
        Me.GroupBox3.TabIndex = 8
        Me.GroupBox3.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rbtnByCode)
        Me.Panel1.Controls.Add(Me.rbtnByName)
        Me.Panel1.Location = New System.Drawing.Point(93, 69)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(384, 35)
        Me.Panel1.TabIndex = 2
        '
        'rbtnByCode
        '
        Me.rbtnByCode.AutoSize = True
        Me.rbtnByCode.Checked = True
        Me.rbtnByCode.Location = New System.Drawing.Point(171, 5)
        Me.rbtnByCode.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtnByCode.Name = "rbtnByCode"
        Me.rbtnByCode.Size = New System.Drawing.Size(94, 24)
        Me.rbtnByCode.TabIndex = 0
        Me.rbtnByCode.TabStop = True
        Me.rbtnByCode.Text = "By Code"
        Me.rbtnByCode.UseVisualStyleBackColor = True
        '
        'rbtnByName
        '
        Me.rbtnByName.AutoSize = True
        Me.rbtnByName.Location = New System.Drawing.Point(278, 5)
        Me.rbtnByName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtnByName.Name = "rbtnByName"
        Me.rbtnByName.Size = New System.Drawing.Size(98, 24)
        Me.rbtnByName.TabIndex = 1
        Me.rbtnByName.Text = "By Name"
        Me.rbtnByName.UseVisualStyleBackColor = True
        '
        'cmbItems
        '
        Me.cmbItems.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend
        Me.cmbItems.CheckedListSettings.CheckStateMember = ""
        Appearance16.BackColor = System.Drawing.SystemColors.Window
        Appearance16.BorderColor = System.Drawing.SystemColors.InactiveCaption
        Me.cmbItems.DisplayLayout.Appearance = Appearance16
        Me.cmbItems.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.cmbItems.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance13.BackColor = System.Drawing.SystemColors.ActiveBorder
        Appearance13.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance13.BorderColor = System.Drawing.SystemColors.Window
        Me.cmbItems.DisplayLayout.GroupByBox.Appearance = Appearance13
        Appearance14.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cmbItems.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance14
        Me.cmbItems.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance15.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance15.BackColor2 = System.Drawing.SystemColors.Control
        Appearance15.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance15.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cmbItems.DisplayLayout.GroupByBox.PromptAppearance = Appearance15
        Me.cmbItems.DisplayLayout.MaxColScrollRegions = 1
        Me.cmbItems.DisplayLayout.MaxRowScrollRegions = 1
        Appearance24.BackColor = System.Drawing.SystemColors.Window
        Appearance24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmbItems.DisplayLayout.Override.ActiveCellAppearance = Appearance24
        Appearance19.BackColor = System.Drawing.SystemColors.Highlight
        Appearance19.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.cmbItems.DisplayLayout.Override.ActiveRowAppearance = Appearance19
        Me.cmbItems.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.cmbItems.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance18.BackColor = System.Drawing.SystemColors.Window
        Me.cmbItems.DisplayLayout.Override.CardAreaAppearance = Appearance18
        Appearance17.BorderColor = System.Drawing.Color.Silver
        Appearance17.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.cmbItems.DisplayLayout.Override.CellAppearance = Appearance17
        Me.cmbItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.cmbItems.DisplayLayout.Override.CellPadding = 0
        Appearance21.BackColor = System.Drawing.SystemColors.Control
        Appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance21.BorderColor = System.Drawing.SystemColors.Window
        Me.cmbItems.DisplayLayout.Override.GroupByRowAppearance = Appearance21
        Appearance23.TextHAlignAsString = "Left"
        Me.cmbItems.DisplayLayout.Override.HeaderAppearance = Appearance23
        Me.cmbItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.cmbItems.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance22.BackColor = System.Drawing.SystemColors.Window
        Appearance22.BorderColor = System.Drawing.Color.Silver
        Me.cmbItems.DisplayLayout.Override.RowAppearance = Appearance22
        Me.cmbItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance20.BackColor = System.Drawing.SystemColors.ControlLight
        Me.cmbItems.DisplayLayout.Override.TemplateAddRowAppearance = Appearance20
        Me.cmbItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbItems.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.cmbItems.LimitToList = True
        Me.cmbItems.Location = New System.Drawing.Point(93, 114)
        Me.cmbItems.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbItems.Name = "cmbItems"
        Me.cmbItems.Size = New System.Drawing.Size(384, 29)
        Me.cmbItems.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 31)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 20)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Location"
        '
        'cmbLocation
        '
        Me.cmbLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbLocation.FormattingEnabled = True
        Me.cmbLocation.Location = New System.Drawing.Point(93, 26)
        Me.cmbLocation.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbLocation.Name = "cmbLocation"
        Me.cmbLocation.Size = New System.Drawing.Size(382, 28)
        Me.cmbLocation.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 118)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(41, 20)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Item"
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(1329, 0)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar1.MyGrid = Me.GridEX1
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(57, 38)
        Me.CtrlGrdBar1.TabIndex = 5
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnClose)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 38)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1388, 65)
        Me.pnlHeader.TabIndex = 6
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.btnClose.Location = New System.Drawing.Point(1312, 9)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(57, 46)
        Me.btnClose.TabIndex = 2
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'frmGrdArticleLedgerByPack
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1388, 914)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmGrdArticleLedgerByPack"
        Me.Text = "Item Ledger By Pack"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.cmbItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnGenerate As System.Windows.Forms.Button
    Friend WithEvents rbtByAmount As System.Windows.Forms.RadioButton
    Friend WithEvents rbtByQty As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbLocation As System.Windows.Forms.ComboBox
    Friend WithEvents rbtnByName As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnByCode As System.Windows.Forms.RadioButton
    Friend WithEvents cmbItems As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
End Class

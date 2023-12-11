<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMaterialDecomposition
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
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim grd_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMaterialDecomposition))
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.cmbEstimation = New System.Windows.Forms.ComboBox()
        Me.lblEstimation = New System.Windows.Forms.Label()
        Me.cmbCustomer = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.cmbTicket = New System.Windows.Forms.ComboBox()
        Me.lblTicket = New System.Windows.Forms.Label()
        Me.cmbCategory = New System.Windows.Forms.ComboBox()
        Me.lblSalesOrder = New System.Windows.Forms.Label()
        Me.cmbSalesOrder = New System.Windows.Forms.ComboBox()
        Me.lblLocation = New System.Windows.Forms.Label()
        Me.cmbPlan = New System.Windows.Forms.ComboBox()
        Me.lblPlan = New System.Windows.Forms.Label()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.lblCustomer = New System.Windows.Forms.Label()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.lblDocNo = New System.Windows.Forms.Label()
        Me.txtDocNo = New System.Windows.Forms.TextBox()
        Me.lblSpecialInstruction = New System.Windows.Forms.Label()
        Me.txtSpecialInstructions = New System.Windows.Forms.TextBox()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdSaved = New Janus.Windows.GridEX.GridEX()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripSplitButton()
        Me.ComparisonReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnProductSearch = New System.Windows.Forms.ToolStripButton()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CtrlGrdBar2 = New SimpleAccounts.CtrlGrdBar()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.UltraTabPageControl1.SuspendLayout()
        CType(Me.cmbCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.cmbEstimation)
        Me.UltraTabPageControl1.Controls.Add(Me.lblEstimation)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbCustomer)
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbTicket)
        Me.UltraTabPageControl1.Controls.Add(Me.lblTicket)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbCategory)
        Me.UltraTabPageControl1.Controls.Add(Me.lblSalesOrder)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbSalesOrder)
        Me.UltraTabPageControl1.Controls.Add(Me.lblLocation)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbPlan)
        Me.UltraTabPageControl1.Controls.Add(Me.lblPlan)
        Me.UltraTabPageControl1.Controls.Add(Me.grd)
        Me.UltraTabPageControl1.Controls.Add(Me.lblCustomer)
        Me.UltraTabPageControl1.Controls.Add(Me.lblDate)
        Me.UltraTabPageControl1.Controls.Add(Me.dtpDate)
        Me.UltraTabPageControl1.Controls.Add(Me.lblDocNo)
        Me.UltraTabPageControl1.Controls.Add(Me.txtDocNo)
        Me.UltraTabPageControl1.Controls.Add(Me.lblSpecialInstruction)
        Me.UltraTabPageControl1.Controls.Add(Me.txtSpecialInstructions)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(873, 417)
        '
        'cmbEstimation
        '
        Me.cmbEstimation.FormattingEnabled = True
        Me.cmbEstimation.Location = New System.Drawing.Point(616, 50)
        Me.cmbEstimation.Name = "cmbEstimation"
        Me.cmbEstimation.Size = New System.Drawing.Size(202, 21)
        Me.cmbEstimation.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.cmbEstimation, "Select estimation")
        Me.cmbEstimation.Visible = False
        '
        'lblEstimation
        '
        Me.lblEstimation.AutoSize = True
        Me.lblEstimation.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEstimation.Location = New System.Drawing.Point(544, 54)
        Me.lblEstimation.Name = "lblEstimation"
        Me.lblEstimation.Size = New System.Drawing.Size(66, 13)
        Me.lblEstimation.TabIndex = 8
        Me.lblEstimation.Text = "Estimation"
        Me.lblEstimation.Visible = False
        '
        'cmbCustomer
        '
        Me.cmbCustomer.CheckedListSettings.CheckStateMember = ""
        Appearance1.BackColor = System.Drawing.SystemColors.Window
        Appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption
        Me.cmbCustomer.DisplayLayout.Appearance = Appearance1
        Me.cmbCustomer.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.cmbCustomer.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder
        Appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance2.BorderColor = System.Drawing.SystemColors.Window
        Me.cmbCustomer.DisplayLayout.GroupByBox.Appearance = Appearance2
        Appearance3.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cmbCustomer.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance3
        Me.cmbCustomer.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance4.BackColor2 = System.Drawing.SystemColors.Control
        Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance4.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cmbCustomer.DisplayLayout.GroupByBox.PromptAppearance = Appearance4
        Me.cmbCustomer.DisplayLayout.MaxColScrollRegions = 1
        Me.cmbCustomer.DisplayLayout.MaxRowScrollRegions = 1
        Appearance5.BackColor = System.Drawing.SystemColors.Window
        Appearance5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmbCustomer.DisplayLayout.Override.ActiveCellAppearance = Appearance5
        Appearance6.BackColor = System.Drawing.SystemColors.Highlight
        Appearance6.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.cmbCustomer.DisplayLayout.Override.ActiveRowAppearance = Appearance6
        Me.cmbCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.cmbCustomer.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance7.BackColor = System.Drawing.SystemColors.Window
        Me.cmbCustomer.DisplayLayout.Override.CardAreaAppearance = Appearance7
        Appearance8.BorderColor = System.Drawing.Color.Silver
        Appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.cmbCustomer.DisplayLayout.Override.CellAppearance = Appearance8
        Me.cmbCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.cmbCustomer.DisplayLayout.Override.CellPadding = 0
        Appearance9.BackColor = System.Drawing.SystemColors.Control
        Appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance9.BorderColor = System.Drawing.SystemColors.Window
        Me.cmbCustomer.DisplayLayout.Override.GroupByRowAppearance = Appearance9
        Appearance10.TextHAlignAsString = "Left"
        Me.cmbCustomer.DisplayLayout.Override.HeaderAppearance = Appearance10
        Me.cmbCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.cmbCustomer.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance11.BackColor = System.Drawing.SystemColors.Window
        Appearance11.BorderColor = System.Drawing.Color.Silver
        Me.cmbCustomer.DisplayLayout.Override.RowAppearance = Appearance11
        Me.cmbCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance12.BackColor = System.Drawing.SystemColors.ControlLight
        Me.cmbCustomer.DisplayLayout.Override.TemplateAddRowAppearance = Appearance12
        Me.cmbCustomer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbCustomer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbCustomer.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.cmbCustomer.Location = New System.Drawing.Point(80, 78)
        Me.cmbCustomer.Name = "cmbCustomer"
        Me.cmbCustomer.Size = New System.Drawing.Size(181, 22)
        Me.cmbCustomer.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.cmbCustomer, "Select customer")
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(873, 43)
        Me.pnlHeader.TabIndex = 0
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(15, 11)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(266, 23)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Material Decomposition"
        '
        'cmbTicket
        '
        Me.cmbTicket.FormattingEnabled = True
        Me.cmbTicket.Location = New System.Drawing.Point(327, 105)
        Me.cmbTicket.Name = "cmbTicket"
        Me.cmbTicket.Size = New System.Drawing.Size(181, 21)
        Me.cmbTicket.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.cmbTicket, "Select ticket")
        '
        'lblTicket
        '
        Me.lblTicket.AutoSize = True
        Me.lblTicket.Location = New System.Drawing.Point(267, 108)
        Me.lblTicket.Name = "lblTicket"
        Me.lblTicket.Size = New System.Drawing.Size(37, 13)
        Me.lblTicket.TabIndex = 11
        Me.lblTicket.Text = "Ticket"
        '
        'cmbCategory
        '
        Me.cmbCategory.FormattingEnabled = True
        Me.cmbCategory.Location = New System.Drawing.Point(80, 134)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.Size = New System.Drawing.Size(181, 21)
        Me.cmbCategory.TabIndex = 14
        Me.ToolTip1.SetToolTip(Me.cmbCategory, "Ticket")
        '
        'lblSalesOrder
        '
        Me.lblSalesOrder.AutoSize = True
        Me.lblSalesOrder.Location = New System.Drawing.Point(266, 82)
        Me.lblSalesOrder.Name = "lblSalesOrder"
        Me.lblSalesOrder.Size = New System.Drawing.Size(57, 13)
        Me.lblSalesOrder.TabIndex = 7
        Me.lblSalesOrder.Text = "Sale Order"
        '
        'cmbSalesOrder
        '
        Me.cmbSalesOrder.FormattingEnabled = True
        Me.cmbSalesOrder.Location = New System.Drawing.Point(327, 78)
        Me.cmbSalesOrder.Name = "cmbSalesOrder"
        Me.cmbSalesOrder.Size = New System.Drawing.Size(181, 21)
        Me.cmbSalesOrder.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.cmbSalesOrder, "Select Sales Order")
        '
        'lblLocation
        '
        Me.lblLocation.AutoSize = True
        Me.lblLocation.Location = New System.Drawing.Point(16, 137)
        Me.lblLocation.Name = "lblLocation"
        Me.lblLocation.Size = New System.Drawing.Size(48, 13)
        Me.lblLocation.TabIndex = 13
        Me.lblLocation.Text = "Location"
        '
        'cmbPlan
        '
        Me.cmbPlan.FormattingEnabled = True
        Me.cmbPlan.Location = New System.Drawing.Point(80, 106)
        Me.cmbPlan.Name = "cmbPlan"
        Me.cmbPlan.Size = New System.Drawing.Size(181, 21)
        Me.cmbPlan.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.cmbPlan, "Select plan")
        '
        'lblPlan
        '
        Me.lblPlan.AutoSize = True
        Me.lblPlan.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPlan.Location = New System.Drawing.Point(16, 108)
        Me.lblPlan.Name = "lblPlan"
        Me.lblPlan.Size = New System.Drawing.Size(31, 13)
        Me.lblPlan.TabIndex = 9
        Me.lblPlan.Text = "Plan"
        '
        'grd
        '
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grd_DesignTimeLayout.LayoutString = resources.GetString("grd_DesignTimeLayout.LayoutString")
        Me.grd.DesignTimeLayout = grd_DesignTimeLayout
        Me.grd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grd.GridLines = Janus.Windows.GridEX.GridLines.None
        Me.grd.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.grd.GroupByBoxVisible = False
        Me.grd.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.grd.Location = New System.Drawing.Point(0, 176)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.Size = New System.Drawing.Size(870, 241)
        Me.grd.TabIndex = 17
        Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'lblCustomer
        '
        Me.lblCustomer.AutoSize = True
        Me.lblCustomer.Location = New System.Drawing.Point(16, 82)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(51, 13)
        Me.lblCustomer.TabIndex = 5
        Me.lblCustomer.Text = "Customer"
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDate.Location = New System.Drawing.Point(266, 57)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(34, 13)
        Me.lblDate.TabIndex = 3
        Me.lblDate.Text = "Date"
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(327, 53)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(181, 20)
        Me.dtpDate.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.dtpDate, "Select Date")
        '
        'lblDocNo
        '
        Me.lblDocNo.AutoSize = True
        Me.lblDocNo.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDocNo.Location = New System.Drawing.Point(16, 57)
        Me.lblDocNo.Name = "lblDocNo"
        Me.lblDocNo.Size = New System.Drawing.Size(48, 13)
        Me.lblDocNo.TabIndex = 1
        Me.lblDocNo.Text = "Doc No"
        '
        'txtDocNo
        '
        Me.txtDocNo.Enabled = False
        Me.txtDocNo.Location = New System.Drawing.Point(80, 54)
        Me.txtDocNo.Multiline = True
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(182, 19)
        Me.txtDocNo.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.txtDocNo, "Document No")
        '
        'lblSpecialInstruction
        '
        Me.lblSpecialInstruction.AutoSize = True
        Me.lblSpecialInstruction.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSpecialInstruction.Location = New System.Drawing.Point(267, 137)
        Me.lblSpecialInstruction.Name = "lblSpecialInstruction"
        Me.lblSpecialInstruction.Size = New System.Drawing.Size(58, 13)
        Me.lblSpecialInstruction.TabIndex = 15
        Me.lblSpecialInstruction.Text = "Remarks"
        '
        'txtSpecialInstructions
        '
        Me.txtSpecialInstructions.Location = New System.Drawing.Point(327, 134)
        Me.txtSpecialInstructions.Multiline = True
        Me.txtSpecialInstructions.Name = "txtSpecialInstructions"
        Me.txtSpecialInstructions.Size = New System.Drawing.Size(181, 21)
        Me.txtSpecialInstructions.TabIndex = 16
        Me.ToolTip1.SetToolTip(Me.txtSpecialInstructions, "Type remarks here")
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grdSaved)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(873, 417)
        '
        'grdSaved
        '
        Me.grdSaved.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdSaved.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSaved.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdSaved.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdSaved.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdSaved.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSaved.Location = New System.Drawing.Point(0, 0)
        Me.grdSaved.Name = "grdSaved"
        Me.grdSaved.RecordNavigator = True
        Me.grdSaved.Size = New System.Drawing.Size(873, 417)
        Me.grdSaved.TabIndex = 0
        Me.grdSaved.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdSaved.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 25)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(875, 438)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 2
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Material Decomposition"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "History"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(873, 417)
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.btnDelete, Me.btnPrint, Me.btnRefresh, Me.btnProductSearch})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(875, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = Global.SimpleAccounts.My.Resources.Resources.BtnNew_Image
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(51, 22)
        Me.btnNew.Text = "&New"
        '
        'btnEdit
        '
        Me.btnEdit.Image = Global.SimpleAccounts.My.Resources.Resources.BtnEdit_Image
        Me.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(47, 22)
        Me.btnEdit.Text = "&Edit"
        '
        'btnSave
        '
        Me.btnSave.Image = Global.SimpleAccounts.My.Resources.Resources.BtnSave_Image
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(51, 22)
        Me.btnSave.Text = "&Save"
        '
        'btnDelete
        '
        Me.btnDelete.Image = Global.SimpleAccounts.My.Resources.Resources.BtnDelete_Image
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(60, 22)
        Me.btnDelete.Text = "Delete"
        '
        'btnPrint
        '
        Me.btnPrint.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ComparisonReportToolStripMenuItem})
        Me.btnPrint.Image = Global.SimpleAccounts.My.Resources.Resources.print
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(64, 22)
        Me.btnPrint.Text = "&Print"
        '
        'ComparisonReportToolStripMenuItem
        '
        Me.ComparisonReportToolStripMenuItem.Name = "ComparisonReportToolStripMenuItem"
        Me.ComparisonReportToolStripMenuItem.Size = New System.Drawing.Size(177, 22)
        Me.ComparisonReportToolStripMenuItem.Text = "Comparison Report"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(66, 22)
        Me.btnRefresh.Text = "Refresh"
        '
        'btnProductSearch
        '
        Me.btnProductSearch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnProductSearch.Name = "btnProductSearch"
        Me.btnProductSearch.Size = New System.Drawing.Size(91, 22)
        Me.btnProductSearch.Text = "Product Search"
        Me.btnProductSearch.Visible = False
        '
        'CtrlGrdBar2
        '
        Me.CtrlGrdBar2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar2.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBar2.Email = Nothing
        Me.CtrlGrdBar2.FormName = Me
        Me.CtrlGrdBar2.Location = New System.Drawing.Point(842, 0)
        Me.CtrlGrdBar2.MyGrid = Me.grdSaved
        Me.CtrlGrdBar2.Name = "CtrlGrdBar2"
        Me.CtrlGrdBar2.Size = New System.Drawing.Size(33, 25)
        Me.CtrlGrdBar2.TabIndex = 1
        Me.CtrlGrdBar2.TabStop = False
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(842, 0)
        Me.CtrlGrdBar1.MyGrid = Me.grd
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(33, 25)
        Me.CtrlGrdBar1.TabIndex = 1
        Me.CtrlGrdBar1.TabStop = False
        '
        'frmMaterialDecomposition
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(875, 463)
        Me.Controls.Add(Me.CtrlGrdBar2)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "frmMaterialDecomposition"
        Me.Text = "Material Decomposition"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        CType(Me.cmbCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbPlan As System.Windows.Forms.ComboBox
    Friend WithEvents lblDocNo As System.Windows.Forms.Label
    Friend WithEvents txtDocNo As System.Windows.Forms.TextBox
    Friend WithEvents lblSpecialInstruction As System.Windows.Forms.Label
    Friend WithEvents txtSpecialInstructions As System.Windows.Forms.TextBox
    Friend WithEvents lblCustomer As System.Windows.Forms.Label
    Friend WithEvents lblTicket As System.Windows.Forms.Label
    Friend WithEvents lblPlan As System.Windows.Forms.Label
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grdSaved As Janus.Windows.GridEX.GridEX
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents ComparisonReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbTicket As System.Windows.Forms.ComboBox
    Friend WithEvents lblSalesOrder As System.Windows.Forms.Label
    Friend WithEvents cmbSalesOrder As System.Windows.Forms.ComboBox
    Friend WithEvents cmbEstimation As System.Windows.Forms.ComboBox
    Friend WithEvents lblEstimation As System.Windows.Forms.Label
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents cmbCustomer As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents cmbCategory As System.Windows.Forms.ComboBox
    Friend WithEvents lblLocation As System.Windows.Forms.Label
    Friend WithEvents CtrlGrdBar2 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents btnProductSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

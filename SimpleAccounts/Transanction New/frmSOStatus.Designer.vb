<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSOStatus
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
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn1 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ID")
        Dim UltraGridColumn2 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Name")
        Dim UltraGridColumn3 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Territory")
        Dim UltraGridColumn4 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("City")
        Dim UltraGridColumn5 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("State")
        Dim UltraGridColumn6 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("AcId")
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSOStatus))
        Dim grdSaved_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.chkAll = New System.Windows.Forms.CheckBox()
        Me.cmbCustomer = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.btnDetail = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.cmbSetTo = New System.Windows.Forms.ToolStripComboBox()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.grdSaved = New Janus.Windows.GridEX.GridEX()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnSummary = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmbOrderType = New System.Windows.Forms.ComboBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.btnSearh = New System.Windows.Forms.Button()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        CType(Me.cmbCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'dtpToDate
        '
        Me.dtpToDate.Checked = False
        Me.dtpToDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(182, 91)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.ShowCheckBox = True
        Me.dtpToDate.Size = New System.Drawing.Size(159, 21)
        Me.dtpToDate.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.dtpToDate, "To Date")
        '
        'dtpFromDate
        '
        Me.dtpFromDate.Checked = False
        Me.dtpFromDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(15, 91)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.ShowCheckBox = True
        Me.dtpFromDate.Size = New System.Drawing.Size(159, 21)
        Me.dtpFromDate.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.dtpFromDate, "From Date")
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(12, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(212, 23)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Sales Order Status"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 75)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "&From Date"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(179, 75)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "&To Date"
        '
        'cmbStatus
        '
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Location = New System.Drawing.Point(347, 91)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(161, 21)
        Me.cmbStatus.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.cmbStatus, "Select Any Status")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 115)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 13)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "&Csutomer"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(344, 75)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(43, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "S&tatus"
        '
        'chkAll
        '
        Me.chkAll.AutoSize = True
        Me.chkAll.Location = New System.Drawing.Point(15, 158)
        Me.chkAll.Name = "chkAll"
        Me.chkAll.Size = New System.Drawing.Size(142, 17)
        Me.chkAll.TabIndex = 13
        Me.chkAll.Text = "&Select / UnSelect All"
        Me.chkAll.UseVisualStyleBackColor = True
        Me.chkAll.Visible = False
        '
        'cmbCustomer
        '
        Me.cmbCustomer.CheckedListSettings.CheckStateMember = ""
        Appearance1.BackColor = System.Drawing.Color.White
        Me.cmbCustomer.DisplayLayout.Appearance = Appearance1
        UltraGridColumn1.Header.VisiblePosition = 0
        UltraGridColumn1.Hidden = True
        UltraGridColumn2.Header.VisiblePosition = 1
        UltraGridColumn2.Width = 141
        UltraGridColumn3.Header.VisiblePosition = 2
        UltraGridColumn4.Header.VisiblePosition = 3
        UltraGridColumn5.Header.VisiblePosition = 4
        UltraGridColumn6.Header.VisiblePosition = 5
        UltraGridColumn6.Hidden = True
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn1, UltraGridColumn2, UltraGridColumn3, UltraGridColumn4, UltraGridColumn5, UltraGridColumn6})
        Me.cmbCustomer.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbCustomer.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbCustomer.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbCustomer.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Appearance2.BackColor = System.Drawing.Color.Transparent
        Me.cmbCustomer.DisplayLayout.Override.CardAreaAppearance = Appearance2
        Me.cmbCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbCustomer.DisplayLayout.Override.CellPadding = 3
        Me.cmbCustomer.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance3.TextHAlignAsString = "Left"
        Me.cmbCustomer.DisplayLayout.Override.HeaderAppearance = Appearance3
        Me.cmbCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance4.BorderColor = System.Drawing.Color.LightGray
        Appearance4.TextVAlignAsString = "Middle"
        Me.cmbCustomer.DisplayLayout.Override.RowAppearance = Appearance4
        Appearance5.BackColor = System.Drawing.Color.LightSteelBlue
        Appearance5.BorderColor = System.Drawing.Color.Black
        Appearance5.ForeColor = System.Drawing.Color.Black
        Me.cmbCustomer.DisplayLayout.Override.SelectedRowAppearance = Appearance5
        Me.cmbCustomer.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbCustomer.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbCustomer.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbCustomer.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.cmbCustomer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbCustomer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbCustomer.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbCustomer.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbCustomer.LimitToList = True
        Me.cmbCustomer.Location = New System.Drawing.Point(15, 131)
        Me.cmbCustomer.Name = "cmbCustomer"
        Me.cmbCustomer.Size = New System.Drawing.Size(493, 23)
        Me.cmbCustomer.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.cmbCustomer, "Select Any Customer")
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnSave, Me.toolStripSeparator, Me.btnDetail, Me.ToolStripSeparator1, Me.cmbSetTo, Me.ToolStripSeparator2, Me.HelpToolStripButton, Me.ToolStripSeparator3, Me.btnPrint, Me.ToolStripSeparator4, Me.btnRefresh})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(779, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(51, 22)
        Me.btnNew.Text = "&New"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(51, 22)
        Me.btnSave.Text = "&Save"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'btnDetail
        '
        Me.btnDetail.Image = Global.SimpleAccounts.My.Resources.Resources.Copy
        Me.btnDetail.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDetail.Name = "btnDetail"
        Me.btnDetail.Size = New System.Drawing.Size(90, 22)
        Me.btnDetail.Text = "Order Detail"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'cmbSetTo
        '
        Me.cmbSetTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSetTo.Name = "cmbSetTo"
        Me.cmbSetTo.Size = New System.Drawing.Size(121, 25)
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'HelpToolStripButton
        '
        Me.HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HelpToolStripButton.Image = CType(resources.GetObject("HelpToolStripButton.Image"), System.Drawing.Image)
        Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton.Name = "HelpToolStripButton"
        Me.HelpToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.HelpToolStripButton.Text = "He&lp"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'btnPrint
        '
        Me.btnPrint.Image = Global.SimpleAccounts.My.Resources.Resources.print
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(52, 22)
        Me.btnPrint.Text = "&Print"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(66, 22)
        Me.btnRefresh.Text = "Refresh"
        '
        'grdSaved
        '
        Me.grdSaved.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        grdSaved_DesignTimeLayout.LayoutString = resources.GetString("grdSaved_DesignTimeLayout.LayoutString")
        Me.grdSaved.DesignTimeLayout = grdSaved_DesignTimeLayout
        Me.grdSaved.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSaved.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdSaved.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdSaved.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.grdSaved.GroupByBoxVisible = False
        Me.grdSaved.Location = New System.Drawing.Point(0, 0)
        Me.grdSaved.Name = "grdSaved"
        Me.grdSaved.Size = New System.Drawing.Size(778, 401)
        Me.grdSaved.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.grdSaved, "Sales Order Status Detail")
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(1, 199)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.grdSaved)
        Me.SplitContainer1.Panel2Collapsed = True
        Me.SplitContainer1.Size = New System.Drawing.Size(778, 401)
        Me.SplitContainer1.SplitterDistance = 200
        Me.SplitContainer1.TabIndex = 18
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Button1.Location = New System.Drawing.Point(12, 612)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 15
        Me.Button1.Text = "History"
        Me.ToolTip1.SetToolTip(Me.Button1, "History")
        Me.Button1.UseVisualStyleBackColor = False
        '
        'btnSummary
        '
        Me.btnSummary.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSummary.BackColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.btnSummary.Location = New System.Drawing.Point(227, 612)
        Me.btnSummary.Name = "btnSummary"
        Me.btnSummary.Size = New System.Drawing.Size(75, 23)
        Me.btnSummary.TabIndex = 16
        Me.btnSummary.Text = "Summary"
        Me.ToolTip1.SetToolTip(Me.btnSummary, "Summary")
        Me.btnSummary.UseVisualStyleBackColor = False
        '
        'cmbOrderType
        '
        Me.cmbOrderType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOrderType.FormattingEnabled = True
        Me.cmbOrderType.Items.AddRange(New Object() {"---- Select ----", "Default", "In Query", "In Production"})
        Me.cmbOrderType.Location = New System.Drawing.Point(514, 91)
        Me.cmbOrderType.Name = "cmbOrderType"
        Me.cmbOrderType.Size = New System.Drawing.Size(161, 21)
        Me.cmbOrderType.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.cmbOrderType, "Select Any Status")
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Button2.Location = New System.Drawing.Point(93, 612)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(128, 23)
        Me.Button2.TabIndex = 19
        Me.Button2.Text = "Plan History Status"
        Me.ToolTip1.SetToolTip(Me.Button2, "History")
        Me.Button2.UseVisualStyleBackColor = False
        '
        'btnSearh
        '
        Me.btnSearh.Image = Global.SimpleAccounts.My.Resources.Resources.search_button_green
        Me.btnSearh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearh.Location = New System.Drawing.Point(514, 121)
        Me.btnSearh.Name = "btnSearh"
        Me.btnSearh.Size = New System.Drawing.Size(161, 33)
        Me.btnSearh.TabIndex = 12
        Me.btnSearh.Text = "Search"
        Me.btnSearh.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ToolTip1.SetToolTip(Me.btnSearh, "Search Result")
        Me.btnSearh.UseVisualStyleBackColor = True
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(258, 297)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 14
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(511, 75)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 13)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Order Type"
        '
        'pnlHeader
        '
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 25)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(779, 42)
        Me.pnlHeader.TabIndex = 78
        '
        'frmSOStatus
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(779, 639)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cmbOrderType)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.btnSummary)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.cmbCustomer)
        Me.Controls.Add(Me.btnSearh)
        Me.Controls.Add(Me.chkAll)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cmbStatus)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dtpFromDate)
        Me.Controls.Add(Me.dtpToDate)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSOStatus"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Sales Order Status"
        CType(Me.cmbCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkAll As System.Windows.Forms.CheckBox
    Friend WithEvents btnSearh As System.Windows.Forms.Button
    Friend WithEvents cmbCustomer As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents grdSaved As Janus.Windows.GridEX.GridEX
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmbSetTo As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnDetail As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSummary As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbOrderType As System.Windows.Forms.ComboBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel

End Class

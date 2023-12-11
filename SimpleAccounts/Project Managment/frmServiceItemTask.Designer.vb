<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmServiceItemTask
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmServiceItemTask))
        Dim grd_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn1 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Id")
        Dim UltraGridColumn2 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleDescription")
        Dim UltraGridColumn3 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Size")
        Dim UltraGridColumn4 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Combination")
        Dim UltraGridColumn5 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("BatchNo", 0)
        Dim UltraGridColumn6 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Price", 1)
        Dim UltraGridColumn7 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Stock", 2)
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnHelp = New System.Windows.Forms.ToolStripButton()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.cmbItem = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.lblItem = New System.Windows.Forms.Label()
        Me.rbtnByName = New System.Windows.Forms.RadioButton()
        Me.rbtnByCode = New System.Windows.Forms.RadioButton()
        Me.grpTask = New System.Windows.Forms.GroupBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.chkActive = New System.Windows.Forms.CheckBox()
        Me.lblSortOrder = New System.Windows.Forms.Label()
        Me.txtSortOrder = New System.Windows.Forms.TextBox()
        Me.lblRate = New System.Windows.Forms.Label()
        Me.txtRate = New System.Windows.Forms.TextBox()
        Me.lblUnit = New System.Windows.Forms.Label()
        Me.txtUnit = New System.Windows.Forms.TextBox()
        Me.lblDetail = New System.Windows.Forms.Label()
        Me.txtDetail = New System.Windows.Forms.TextBox()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.pnlHeader.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpTask.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Controls.Add(Me.lblProgress)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 25)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(536, 49)
        Me.pnlHeader.TabIndex = 1
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.lblHeader.Location = New System.Drawing.Point(11, 15)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(288, 23)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Service / Work Item Task"
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(222, 2)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 1
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(51, 22)
        Me.btnNew.Text = "&New"
        '
        'btnEdit
        '
        Me.btnEdit.Image = CType(resources.GetObject("btnEdit.Image"), System.Drawing.Image)
        Me.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(47, 22)
        Me.btnEdit.Text = "&Edit"
        Me.btnEdit.Visible = False
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(51, 22)
        Me.btnSave.Text = "&Save"
        '
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.RightToLeftAutoMirrorImage = True
        Me.btnDelete.Size = New System.Drawing.Size(60, 22)
        Me.btnDelete.Text = "&Delete"
        '
        'btnPrint
        '
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(52, 22)
        Me.btnPrint.Text = "&Print"
        Me.btnPrint.Visible = False
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnHelp
        '
        Me.btnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(23, 22)
        Me.btnHelp.Text = "He&lp"
        Me.btnHelp.Visible = False
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.btnDelete, Me.btnPrint, Me.toolStripSeparator1, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(536, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'grd
        '
        Me.grd.AllowAddNew = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grd.ColumnAutoResize = True
        grd_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grd.DesignTimeLayout = grd_DesignTimeLayout
        Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.GroupByBoxVisible = False
        Me.grd.Location = New System.Drawing.Point(0, 243)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(536, 257)
        Me.grd.TabIndex = 7
        Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'cmbItem
        '
        Me.cmbItem.AlwaysInEditMode = True
        Me.cmbItem.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append
        Me.cmbItem.CheckedListSettings.CheckStateMember = ""
        Appearance1.BackColor = System.Drawing.Color.White
        Appearance1.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        Me.cmbItem.DisplayLayout.Appearance = Appearance1
        UltraGridColumn1.Header.VisiblePosition = 0
        UltraGridColumn1.Hidden = True
        UltraGridColumn2.Header.VisiblePosition = 1
        UltraGridColumn3.Header.VisiblePosition = 2
        UltraGridColumn3.Width = 60
        UltraGridColumn4.Header.VisiblePosition = 3
        UltraGridColumn4.Width = 80
        UltraGridColumn5.Header.Caption = "Batch No"
        UltraGridColumn5.Header.VisiblePosition = 6
        UltraGridColumn5.Width = 90
        UltraGridColumn6.Header.VisiblePosition = 4
        UltraGridColumn6.Width = 80
        UltraGridColumn7.Header.VisiblePosition = 5
        UltraGridColumn7.Width = 60
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn1, UltraGridColumn2, UltraGridColumn3, UltraGridColumn4, UltraGridColumn5, UltraGridColumn6, UltraGridColumn7})
        Me.cmbItem.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbItem.DisplayLayout.InterBandSpacing = 10
        Me.cmbItem.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbItem.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbItem.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Appearance2.BackColor = System.Drawing.Color.Transparent
        Me.cmbItem.DisplayLayout.Override.CardAreaAppearance = Appearance2
        Me.cmbItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbItem.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance3.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance3.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance3.ForeColor = System.Drawing.Color.White
        Appearance3.TextHAlignAsString = "Left"
        Appearance3.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent
        Me.cmbItem.DisplayLayout.Override.HeaderAppearance = Appearance3
        Me.cmbItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance4.BorderColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbItem.DisplayLayout.Override.RowAppearance = Appearance4
        Appearance5.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance5.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.cmbItem.DisplayLayout.Override.RowSelectorAppearance = Appearance5
        Me.cmbItem.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbItem.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance6.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance6.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance6.ForeColor = System.Drawing.Color.Black
        Me.cmbItem.DisplayLayout.Override.SelectedRowAppearance = Appearance6
        Me.cmbItem.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbItem.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbItem.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbItem.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbItem.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.cmbItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbItem.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbItem.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbItem.LimitToList = True
        Me.cmbItem.Location = New System.Drawing.Point(18, 93)
        Me.cmbItem.MaxDropDownItems = 20
        Me.cmbItem.Name = "cmbItem"
        Me.cmbItem.Size = New System.Drawing.Size(411, 22)
        Me.cmbItem.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.cmbItem, "Please select any item")
        '
        'lblItem
        '
        Me.lblItem.AutoSize = True
        Me.lblItem.Location = New System.Drawing.Point(15, 76)
        Me.lblItem.Name = "lblItem"
        Me.lblItem.Size = New System.Drawing.Size(27, 13)
        Me.lblItem.TabIndex = 2
        Me.lblItem.Text = "Item"
        '
        'rbtnByName
        '
        Me.rbtnByName.AutoSize = True
        Me.rbtnByName.Location = New System.Drawing.Point(184, 74)
        Me.rbtnByName.Name = "rbtnByName"
        Me.rbtnByName.Size = New System.Drawing.Size(68, 17)
        Me.rbtnByName.TabIndex = 5
        Me.rbtnByName.Text = "By Name"
        Me.rbtnByName.UseVisualStyleBackColor = True
        '
        'rbtnByCode
        '
        Me.rbtnByCode.AutoSize = True
        Me.rbtnByCode.Checked = True
        Me.rbtnByCode.Location = New System.Drawing.Point(115, 74)
        Me.rbtnByCode.Name = "rbtnByCode"
        Me.rbtnByCode.Size = New System.Drawing.Size(65, 17)
        Me.rbtnByCode.TabIndex = 4
        Me.rbtnByCode.TabStop = True
        Me.rbtnByCode.Text = "By Code"
        Me.rbtnByCode.UseVisualStyleBackColor = True
        '
        'grpTask
        '
        Me.grpTask.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpTask.Controls.Add(Me.btnAdd)
        Me.grpTask.Controls.Add(Me.chkActive)
        Me.grpTask.Controls.Add(Me.lblSortOrder)
        Me.grpTask.Controls.Add(Me.txtSortOrder)
        Me.grpTask.Controls.Add(Me.lblRate)
        Me.grpTask.Controls.Add(Me.txtRate)
        Me.grpTask.Controls.Add(Me.lblUnit)
        Me.grpTask.Controls.Add(Me.txtUnit)
        Me.grpTask.Controls.Add(Me.lblDetail)
        Me.grpTask.Controls.Add(Me.txtDetail)
        Me.grpTask.Controls.Add(Me.lblTitle)
        Me.grpTask.Controls.Add(Me.txtTitle)
        Me.grpTask.Location = New System.Drawing.Point(12, 121)
        Me.grpTask.Name = "grpTask"
        Me.grpTask.Size = New System.Drawing.Size(512, 116)
        Me.grpTask.TabIndex = 6
        Me.grpTask.TabStop = False
        Me.grpTask.Text = "Task Details"
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(342, 67)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 11
        Me.btnAdd.Text = "+"
        Me.ToolTip1.SetToolTip(Me.btnAdd, "Add to Grid bellow")
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'chkActive
        '
        Me.chkActive.AutoSize = True
        Me.chkActive.Checked = True
        Me.chkActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkActive.Location = New System.Drawing.Point(280, 71)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(56, 17)
        Me.chkActive.TabIndex = 10
        Me.chkActive.Text = "Active"
        Me.chkActive.UseVisualStyleBackColor = True
        '
        'lblSortOrder
        '
        Me.lblSortOrder.AutoSize = True
        Me.lblSortOrder.Location = New System.Drawing.Point(185, 55)
        Me.lblSortOrder.Name = "lblSortOrder"
        Me.lblSortOrder.Size = New System.Drawing.Size(55, 13)
        Me.lblSortOrder.TabIndex = 8
        Me.lblSortOrder.Text = "Sort Order"
        '
        'txtSortOrder
        '
        Me.txtSortOrder.Location = New System.Drawing.Point(188, 71)
        Me.txtSortOrder.Name = "txtSortOrder"
        Me.txtSortOrder.Size = New System.Drawing.Size(85, 20)
        Me.txtSortOrder.TabIndex = 9
        Me.txtSortOrder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtSortOrder, "Please enter the sort order #")
        '
        'lblRate
        '
        Me.lblRate.AutoSize = True
        Me.lblRate.Location = New System.Drawing.Point(94, 55)
        Me.lblRate.Name = "lblRate"
        Me.lblRate.Size = New System.Drawing.Size(30, 13)
        Me.lblRate.TabIndex = 6
        Me.lblRate.Text = "Rate"
        '
        'txtRate
        '
        Me.txtRate.Location = New System.Drawing.Point(97, 71)
        Me.txtRate.Name = "txtRate"
        Me.txtRate.Size = New System.Drawing.Size(85, 20)
        Me.txtRate.TabIndex = 7
        Me.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtRate, "Please enter the rate")
        '
        'lblUnit
        '
        Me.lblUnit.AutoSize = True
        Me.lblUnit.Location = New System.Drawing.Point(3, 55)
        Me.lblUnit.Name = "lblUnit"
        Me.lblUnit.Size = New System.Drawing.Size(26, 13)
        Me.lblUnit.TabIndex = 4
        Me.lblUnit.Text = "Unit"
        '
        'txtUnit
        '
        Me.txtUnit.Location = New System.Drawing.Point(6, 71)
        Me.txtUnit.Name = "txtUnit"
        Me.txtUnit.Size = New System.Drawing.Size(85, 20)
        Me.txtUnit.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.txtUnit, "Please enter the unit")
        '
        'lblDetail
        '
        Me.lblDetail.AutoSize = True
        Me.lblDetail.Location = New System.Drawing.Point(185, 16)
        Me.lblDetail.Name = "lblDetail"
        Me.lblDetail.Size = New System.Drawing.Size(34, 13)
        Me.lblDetail.TabIndex = 2
        Me.lblDetail.Text = "Detail"
        '
        'txtDetail
        '
        Me.txtDetail.Location = New System.Drawing.Point(188, 32)
        Me.txtDetail.Name = "txtDetail"
        Me.txtDetail.Size = New System.Drawing.Size(229, 20)
        Me.txtDetail.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtDetail, "Please enter the detail")
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Location = New System.Drawing.Point(3, 16)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(27, 13)
        Me.lblTitle.TabIndex = 0
        Me.lblTitle.Text = "Title"
        '
        'txtTitle
        '
        Me.txtTitle.Location = New System.Drawing.Point(6, 32)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(176, 20)
        Me.txtTitle.TabIndex = 1
        Me.txtTitle.Tag = ""
        Me.ToolTip1.SetToolTip(Me.txtTitle, "Please enter the title")
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(498, 0)
        Me.CtrlGrdBar1.MyGrid = Me.grd
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(38, 25)
        Me.CtrlGrdBar1.TabIndex = 8
        Me.CtrlGrdBar1.TabStop = False
        '
        'frmServiceItemTask
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(536, 500)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.grpTask)
        Me.Controls.Add(Me.cmbItem)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.lblItem)
        Me.Controls.Add(Me.rbtnByName)
        Me.Controls.Add(Me.rbtnByCode)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmServiceItemTask"
        Me.Text = "Service Item Task"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpTask.ResumeLayout(False)
        Me.grpTask.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents cmbItem As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents lblItem As System.Windows.Forms.Label
    Friend WithEvents rbtnByName As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnByCode As System.Windows.Forms.RadioButton
    Friend WithEvents grpTask As System.Windows.Forms.GroupBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents lblSortOrder As System.Windows.Forms.Label
    Friend WithEvents txtSortOrder As System.Windows.Forms.TextBox
    Friend WithEvents lblRate As System.Windows.Forms.Label
    Friend WithEvents txtRate As System.Windows.Forms.TextBox
    Friend WithEvents lblUnit As System.Windows.Forms.Label
    Friend WithEvents txtUnit As System.Windows.Forms.TextBox
    Friend WithEvents lblDetail As System.Windows.Forms.Label
    Friend WithEvents txtDetail As System.Windows.Forms.TextBox
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
End Class

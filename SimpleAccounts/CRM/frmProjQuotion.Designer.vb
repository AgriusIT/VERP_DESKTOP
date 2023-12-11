<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProjQuotion
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProjQuotion))
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.rbtByGuard = New System.Windows.Forms.RadioButton()
        Me.txtParticulars = New System.Windows.Forms.TextBox()
        Me.lblParticulars = New System.Windows.Forms.Label()
        Me.cmboxProject = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.lblAmount = New System.Windows.Forms.Label()
        Me.rbtByName = New System.Windows.Forms.RadioButton()
        Me.rbtByCode = New System.Windows.Forms.RadioButton()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.lblQuotationStatus = New System.Windows.Forms.Label()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblQuotationDate = New System.Windows.Forms.Label()
        Me.dtpQuotationDate = New System.Windows.Forms.DateTimePicker()
        Me.txtQuotationNo = New System.Windows.Forms.TextBox()
        Me.lblQuotation = New System.Windows.Forms.Label()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.GrdStatus = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnLoadAll = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnHelp = New System.Windows.Forms.ToolStripButton()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        CType(Me.cmboxProject, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.GrdStatus, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.rbtByGuard)
        Me.UltraTabPageControl1.Controls.Add(Me.txtParticulars)
        Me.UltraTabPageControl1.Controls.Add(Me.lblParticulars)
        Me.UltraTabPageControl1.Controls.Add(Me.cmboxProject)
        Me.UltraTabPageControl1.Controls.Add(Me.txtAmount)
        Me.UltraTabPageControl1.Controls.Add(Me.lblAmount)
        Me.UltraTabPageControl1.Controls.Add(Me.rbtByName)
        Me.UltraTabPageControl1.Controls.Add(Me.rbtByCode)
        Me.UltraTabPageControl1.Controls.Add(Me.lblProgress)
        Me.UltraTabPageControl1.Controls.Add(Me.lblQuotationStatus)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbStatus)
        Me.UltraTabPageControl1.Controls.Add(Me.Label5)
        Me.UltraTabPageControl1.Controls.Add(Me.lblQuotationDate)
        Me.UltraTabPageControl1.Controls.Add(Me.dtpQuotationDate)
        Me.UltraTabPageControl1.Controls.Add(Me.txtQuotationNo)
        Me.UltraTabPageControl1.Controls.Add(Me.lblQuotation)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1064, 727)
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.Teal
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1064, 58)
        Me.pnlHeader.TabIndex = 17
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.White
        Me.lblHeader.Location = New System.Drawing.Point(16, 8)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(303, 35)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Project Quotation"
        '
        'rbtByGuard
        '
        Me.rbtByGuard.AutoSize = True
        Me.rbtByGuard.Location = New System.Drawing.Point(606, 175)
        Me.rbtByGuard.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtByGuard.Name = "rbtByGuard"
        Me.rbtByGuard.Size = New System.Drawing.Size(142, 24)
        Me.rbtByGuard.TabIndex = 16
        Me.rbtByGuard.Text = "By Guard M.No"
        Me.rbtByGuard.UseVisualStyleBackColor = True
        '
        'txtParticulars
        '
        Me.txtParticulars.Location = New System.Drawing.Point(388, 302)
        Me.txtParticulars.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtParticulars.Multiline = True
        Me.txtParticulars.Name = "txtParticulars"
        Me.txtParticulars.Size = New System.Drawing.Size(360, 67)
        Me.txtParticulars.TabIndex = 8
        '
        'lblParticulars
        '
        Me.lblParticulars.AutoSize = True
        Me.lblParticulars.Location = New System.Drawing.Point(228, 306)
        Me.lblParticulars.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblParticulars.Name = "lblParticulars"
        Me.lblParticulars.Size = New System.Drawing.Size(157, 20)
        Me.lblParticulars.TabIndex = 7
        Me.lblParticulars.Text = "Quotation Particulars"
        '
        'cmboxProject
        '
        Me.cmboxProject.CheckedListSettings.CheckStateMember = ""
        Appearance16.BackColor = System.Drawing.SystemColors.Window
        Appearance16.BorderColor = System.Drawing.SystemColors.InactiveCaption
        Me.cmboxProject.DisplayLayout.Appearance = Appearance16
        Me.cmboxProject.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.cmboxProject.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance13.BackColor = System.Drawing.SystemColors.ActiveBorder
        Appearance13.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance13.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance13.BorderColor = System.Drawing.SystemColors.Window
        Me.cmboxProject.DisplayLayout.GroupByBox.Appearance = Appearance13
        Appearance14.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cmboxProject.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance14
        Me.cmboxProject.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance15.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance15.BackColor2 = System.Drawing.SystemColors.Control
        Appearance15.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance15.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cmboxProject.DisplayLayout.GroupByBox.PromptAppearance = Appearance15
        Me.cmboxProject.DisplayLayout.MaxColScrollRegions = 1
        Me.cmboxProject.DisplayLayout.MaxRowScrollRegions = 1
        Appearance24.BackColor = System.Drawing.SystemColors.Window
        Appearance24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmboxProject.DisplayLayout.Override.ActiveCellAppearance = Appearance24
        Appearance19.BackColor = System.Drawing.SystemColors.Highlight
        Appearance19.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.cmboxProject.DisplayLayout.Override.ActiveRowAppearance = Appearance19
        Me.cmboxProject.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.cmboxProject.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance18.BackColor = System.Drawing.SystemColors.Window
        Me.cmboxProject.DisplayLayout.Override.CardAreaAppearance = Appearance18
        Appearance17.BorderColor = System.Drawing.Color.Silver
        Appearance17.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.cmboxProject.DisplayLayout.Override.CellAppearance = Appearance17
        Me.cmboxProject.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.cmboxProject.DisplayLayout.Override.CellPadding = 0
        Appearance21.BackColor = System.Drawing.SystemColors.Control
        Appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance21.BorderColor = System.Drawing.SystemColors.Window
        Me.cmboxProject.DisplayLayout.Override.GroupByRowAppearance = Appearance21
        Appearance23.TextHAlignAsString = "Left"
        Me.cmboxProject.DisplayLayout.Override.HeaderAppearance = Appearance23
        Me.cmboxProject.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.cmboxProject.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance22.BackColor = System.Drawing.SystemColors.Window
        Appearance22.BorderColor = System.Drawing.Color.Silver
        Me.cmboxProject.DisplayLayout.Override.RowAppearance = Appearance22
        Me.cmboxProject.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance20.BackColor = System.Drawing.SystemColors.ControlLight
        Me.cmboxProject.DisplayLayout.Override.TemplateAddRowAppearance = Appearance20
        Me.cmboxProject.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmboxProject.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmboxProject.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.cmboxProject.Location = New System.Drawing.Point(388, 211)
        Me.cmboxProject.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmboxProject.Name = "cmboxProject"
        Me.cmboxProject.Size = New System.Drawing.Size(362, 29)
        Me.cmboxProject.TabIndex = 4
        Me.cmboxProject.Text = "--- Select Project ---"
        '
        'txtAmount
        '
        Me.txtAmount.Location = New System.Drawing.Point(388, 429)
        Me.txtAmount.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(360, 26)
        Me.txtAmount.TabIndex = 12
        '
        'lblAmount
        '
        Me.lblAmount.AutoSize = True
        Me.lblAmount.Location = New System.Drawing.Point(228, 435)
        Me.lblAmount.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(139, 20)
        Me.lblAmount.TabIndex = 11
        Me.lblAmount.Text = "Quotation Amount"
        '
        'rbtByName
        '
        Me.rbtByName.AutoSize = True
        Me.rbtByName.Checked = True
        Me.rbtByName.Location = New System.Drawing.Point(495, 175)
        Me.rbtByName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtByName.Name = "rbtByName"
        Me.rbtByName.Size = New System.Drawing.Size(98, 24)
        Me.rbtByName.TabIndex = 2
        Me.rbtByName.TabStop = True
        Me.rbtByName.Text = "By Name"
        Me.rbtByName.UseVisualStyleBackColor = True
        '
        'rbtByCode
        '
        Me.rbtByCode.AutoSize = True
        Me.rbtByCode.Location = New System.Drawing.Point(388, 175)
        Me.rbtByCode.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtByCode.Name = "rbtByCode"
        Me.rbtByCode.Size = New System.Drawing.Size(94, 24)
        Me.rbtByCode.TabIndex = 1
        Me.rbtByCode.Text = "By Code"
        Me.rbtByCode.UseVisualStyleBackColor = True
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(384, 360)
        Me.lblProgress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(333, 60)
        Me.lblProgress.TabIndex = 15
        Me.lblProgress.Text = "Processing please wait..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'lblQuotationStatus
        '
        Me.lblQuotationStatus.AutoSize = True
        Me.lblQuotationStatus.Location = New System.Drawing.Point(228, 475)
        Me.lblQuotationStatus.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblQuotationStatus.Name = "lblQuotationStatus"
        Me.lblQuotationStatus.Size = New System.Drawing.Size(130, 20)
        Me.lblQuotationStatus.TabIndex = 13
        Me.lblQuotationStatus.Text = "Quotation Status"
        '
        'cmbStatus
        '
        Me.cmbStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbStatus.BackColor = System.Drawing.SystemColors.Info
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Items.AddRange(New Object() {"Pending", "Revised", "Awarded", "Drop"})
        Me.cmbStatus.Location = New System.Drawing.Point(388, 469)
        Me.cmbStatus.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(360, 28)
        Me.cmbStatus.TabIndex = 14
        Me.cmbStatus.Text = "---- Select Status ----"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(228, 218)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 20)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Project"
        '
        'lblQuotationDate
        '
        Me.lblQuotationDate.AutoSize = True
        Me.lblQuotationDate.Location = New System.Drawing.Point(228, 395)
        Me.lblQuotationDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblQuotationDate.Name = "lblQuotationDate"
        Me.lblQuotationDate.Size = New System.Drawing.Size(118, 20)
        Me.lblQuotationDate.TabIndex = 9
        Me.lblQuotationDate.Text = "Quotation Date"
        '
        'dtpQuotationDate
        '
        Me.dtpQuotationDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpQuotationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpQuotationDate.Location = New System.Drawing.Point(388, 389)
        Me.dtpQuotationDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpQuotationDate.Name = "dtpQuotationDate"
        Me.dtpQuotationDate.Size = New System.Drawing.Size(360, 26)
        Me.dtpQuotationDate.TabIndex = 10
        '
        'txtQuotationNo
        '
        Me.txtQuotationNo.Location = New System.Drawing.Point(388, 254)
        Me.txtQuotationNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtQuotationNo.Name = "txtQuotationNo"
        Me.txtQuotationNo.Size = New System.Drawing.Size(360, 26)
        Me.txtQuotationNo.TabIndex = 6
        '
        'lblQuotation
        '
        Me.lblQuotation.AutoSize = True
        Me.lblQuotation.Location = New System.Drawing.Point(228, 260)
        Me.lblQuotation.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblQuotation.Name = "lblQuotation"
        Me.lblQuotation.Size = New System.Drawing.Size(103, 20)
        Me.lblQuotation.TabIndex = 5
        Me.lblQuotation.Text = "Quotation No"
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.GrdStatus)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1064, 727)
        '
        'GrdStatus
        '
        Me.GrdStatus.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GrdStatus.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrdStatus.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GrdStatus.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GrdStatus.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GrdStatus.GroupByBoxVisible = False
        Me.GrdStatus.Location = New System.Drawing.Point(0, 0)
        Me.GrdStatus.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GrdStatus.Name = "GrdStatus"
        Me.GrdStatus.Size = New System.Drawing.Size(1064, 727)
        Me.GrdStatus.TabIndex = 0
        Me.GrdStatus.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.btnPrint, Me.toolStripSeparator, Me.btnDelete, Me.toolStripSeparator1, Me.btnRefresh, Me.btnLoadAll, Me.ToolStripSeparator2, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1066, 32)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(75, 29)
        Me.btnNew.Text = "&New"
        '
        'btnEdit
        '
        Me.btnEdit.Image = CType(resources.GetObject("btnEdit.Image"), System.Drawing.Image)
        Me.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(70, 29)
        Me.btnEdit.Text = "&Edit"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(77, 29)
        Me.btnSave.Text = "&Save"
        '
        'btnPrint
        '
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(76, 29)
        Me.btnPrint.Text = "&Print"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 32)
        '
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(90, 29)
        Me.btnDelete.Text = "D&elete"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 32)
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 29)
        Me.btnRefresh.Text = "Refresh"
        '
        'btnLoadAll
        '
        Me.btnLoadAll.Image = Global.SimpleAccounts.My.Resources.Resources.sendcontactdetails
        Me.btnLoadAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnLoadAll.Name = "btnLoadAll"
        Me.btnLoadAll.Size = New System.Drawing.Size(104, 29)
        Me.btnLoadAll.Text = "Load All"
        Me.btnLoadAll.Visible = False
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 32)
        '
        'btnHelp
        '
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(77, 29)
        Me.btnHelp.Text = "He&lp"
        Me.btnHelp.Visible = False
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
        Me.UltraTabControl1.Size = New System.Drawing.Size(1066, 754)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 0
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Project Wizard"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "History "
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.TabStop = False
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.VisualStudio2005
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1064, 727)
        '
        'frmProjQuotion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1066, 786)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmProjQuotion"
        Me.Text = "frmProjQuotion"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.cmboxProject, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.GrdStatus, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnLoadAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents GrdStatus As Janus.Windows.GridEX.GridEX
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents rbtByName As System.Windows.Forms.RadioButton
    Friend WithEvents rbtByCode As System.Windows.Forms.RadioButton
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents lblQuotationStatus As System.Windows.Forms.Label
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblQuotationDate As System.Windows.Forms.Label
    Friend WithEvents dtpQuotationDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents txtQuotationNo As System.Windows.Forms.TextBox
    Friend WithEvents lblQuotation As System.Windows.Forms.Label
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents lblAmount As System.Windows.Forms.Label
    Friend WithEvents cmboxProject As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblParticulars As System.Windows.Forms.Label
    Friend WithEvents txtParticulars As System.Windows.Forms.TextBox
    Friend WithEvents rbtByGuard As System.Windows.Forms.RadioButton
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

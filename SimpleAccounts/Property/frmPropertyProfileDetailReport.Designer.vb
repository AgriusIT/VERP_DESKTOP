<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPropertyProfileDetailReport
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
        Dim GridResults_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPropertyProfileDetailReport))
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance15 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance16 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance17 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance18 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance19 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance20 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance21 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance22 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.UltraTabPageControl3 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        Me.rdoAllDeals = New System.Windows.Forms.RadioButton()
        Me.rdoPending = New System.Windows.Forms.RadioButton()
        Me.rdoCompleteDeals = New System.Windows.Forms.RadioButton()
        Me.txtAgentSearch = New System.Windows.Forms.TextBox()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.UltraTabPageControl4 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.GridResults = New Janus.Windows.GridEX.GridEX()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.refresh = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.UltraTabConrol1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage3 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraGrid1 = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.lstPropertyType = New SimpleAccounts.uiListControl()
        Me.lstAgentName = New SimpleAccounts.uiListControl()
        Me.lstProjectName = New SimpleAccounts.uiListControl()
        Me.lstProjectHead = New SimpleAccounts.uiListControl()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.UltraTabPageControl3.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.UltraTabPageControl4.SuspendLayout()
        CType(Me.GridResults, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel4.SuspendLayout()
        CType(Me.UltraTabConrol1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabConrol1.SuspendLayout()
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'UltraTabPageControl3
        '
        Me.UltraTabPageControl3.Controls.Add(Me.Panel5)
        Me.UltraTabPageControl3.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl3.Name = "UltraTabPageControl3"
        Me.UltraTabPageControl3.Size = New System.Drawing.Size(1004, 653)
        '
        'Panel5
        '
        Me.Panel5.Controls.Add(Me.Label4)
        Me.Panel5.Controls.Add(Me.cmbPeriod)
        Me.Panel5.Controls.Add(Me.rdoAllDeals)
        Me.Panel5.Controls.Add(Me.rdoPending)
        Me.Panel5.Controls.Add(Me.rdoCompleteDeals)
        Me.Panel5.Controls.Add(Me.txtAgentSearch)
        Me.Panel5.Controls.Add(Me.lstPropertyType)
        Me.Panel5.Controls.Add(Me.lstAgentName)
        Me.Panel5.Controls.Add(Me.lstProjectName)
        Me.Panel5.Controls.Add(Me.lstProjectHead)
        Me.Panel5.Controls.Add(Me.btnPrint)
        Me.Panel5.Controls.Add(Me.btnShow)
        Me.Panel5.Controls.Add(Me.dtpTo)
        Me.Panel5.Controls.Add(Me.Label2)
        Me.Panel5.Controls.Add(Me.dtpFrom)
        Me.Panel5.Controls.Add(Me.Label1)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1004, 653)
        Me.Panel5.TabIndex = 12
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(16, 27)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(46, 17)
        Me.Label4.TabIndex = 39
        Me.Label4.Text = "Period"
        '
        'cmbPeriod
        '
        Me.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPeriod.FormattingEnabled = True
        Me.cmbPeriod.Items.AddRange(New Object() {"Today", "Yesterday", "Current Week", "Current Month", "Current Year"})
        Me.cmbPeriod.Location = New System.Drawing.Point(80, 23)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(191, 21)
        Me.cmbPeriod.TabIndex = 40
        '
        'rdoAllDeals
        '
        Me.rdoAllDeals.AutoSize = True
        Me.rdoAllDeals.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoAllDeals.Location = New System.Drawing.Point(572, 85)
        Me.rdoAllDeals.Name = "rdoAllDeals"
        Me.rdoAllDeals.Size = New System.Drawing.Size(76, 21)
        Me.rdoAllDeals.TabIndex = 38
        Me.rdoAllDeals.TabStop = True
        Me.rdoAllDeals.Text = "All Deals"
        Me.rdoAllDeals.UseVisualStyleBackColor = True
        '
        'rdoPending
        '
        Me.rdoPending.AutoSize = True
        Me.rdoPending.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoPending.Location = New System.Drawing.Point(478, 85)
        Me.rdoPending.Name = "rdoPending"
        Me.rdoPending.Size = New System.Drawing.Size(73, 21)
        Me.rdoPending.TabIndex = 38
        Me.rdoPending.TabStop = True
        Me.rdoPending.Text = "Pending"
        Me.rdoPending.UseVisualStyleBackColor = True
        '
        'rdoCompleteDeals
        '
        Me.rdoCompleteDeals.AutoSize = True
        Me.rdoCompleteDeals.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.rdoCompleteDeals.Location = New System.Drawing.Point(339, 85)
        Me.rdoCompleteDeals.Name = "rdoCompleteDeals"
        Me.rdoCompleteDeals.Size = New System.Drawing.Size(118, 21)
        Me.rdoCompleteDeals.TabIndex = 37
        Me.rdoCompleteDeals.TabStop = True
        Me.rdoCompleteDeals.Text = "Complete Deals"
        Me.rdoCompleteDeals.UseVisualStyleBackColor = True
        '
        'txtAgentSearch
        '
        Me.txtAgentSearch.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtAgentSearch.Location = New System.Drawing.Point(536, 308)
        Me.txtAgentSearch.Name = "txtAgentSearch"
        Me.txtAgentSearch.Size = New System.Drawing.Size(114, 25)
        Me.txtAgentSearch.TabIndex = 36
        '
        'btnPrint
        '
        Me.btnPrint.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.ForeColor = System.Drawing.Color.White
        Me.btnPrint.Location = New System.Drawing.Point(349, 497)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(75, 27)
        Me.btnPrint.TabIndex = 30
        Me.btnPrint.Text = "Print"
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'btnShow
        '
        Me.btnShow.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnShow.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShow.ForeColor = System.Drawing.Color.White
        Me.btnShow.Location = New System.Drawing.Point(254, 497)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(75, 27)
        Me.btnShow.TabIndex = 16
        Me.btnShow.Text = "Show"
        Me.btnShow.UseVisualStyleBackColor = False
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd/MMM/yyyy"
        Me.dtpTo.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(158, 81)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(113, 25)
        Me.dtpTo.TabIndex = 19
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.Label2.Location = New System.Drawing.Point(155, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(53, 17)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "To Date"
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFrom.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(19, 81)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(113, 25)
        Me.dtpFrom.TabIndex = 17
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.Label1.Location = New System.Drawing.Point(16, 61)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 17)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "From Date"
        '
        'UltraTabPageControl4
        '
        Me.UltraTabPageControl4.Controls.Add(Me.GridResults)
        Me.UltraTabPageControl4.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl4.Name = "UltraTabPageControl4"
        Me.UltraTabPageControl4.Size = New System.Drawing.Size(1004, 653)
        '
        'GridResults
        '
        Me.GridResults.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        GridResults_DesignTimeLayout.LayoutString = resources.GetString("GridResults_DesignTimeLayout.LayoutString")
        Me.GridResults.DesignTimeLayout = GridResults_DesignTimeLayout
        Me.GridResults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridResults.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridResults.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridResults.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GridResults.GridLines = Janus.Windows.GridEX.GridLines.Horizontal
        Me.GridResults.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.GridResults.GroupByBoxVisible = False
        Me.GridResults.Location = New System.Drawing.Point(0, 0)
        Me.GridResults.Name = "GridResults"
        Me.GridResults.Size = New System.Drawing.Size(1004, 653)
        Me.GridResults.TabIndex = 0
        Me.GridResults.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridResults.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridResults.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.SteelBlue
        Me.pnlHeader.Controls.Add(Me.refresh)
        Me.pnlHeader.Controls.Add(Me.CtrlGrdBar1)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1008, 50)
        Me.pnlHeader.TabIndex = 1
        '
        'refresh
        '
        Me.refresh.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.refresh.BackgroundImage = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.refresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.refresh.FlatStyle = System.Windows.Forms.FlatStyle.Popup
        Me.refresh.Location = New System.Drawing.Point(925, 13)
        Me.refresh.Name = "refresh"
        Me.refresh.Size = New System.Drawing.Size(25, 23)
        Me.refresh.TabIndex = 91
        Me.refresh.UseVisualStyleBackColor = True
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Segoe UI", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.White
        Me.lblHeader.Location = New System.Drawing.Point(12, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(358, 32)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Property Profile Detail Report"
        '
        'Panel1
        '
        Me.Panel1.AutoScroll = True
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 50)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1008, 679)
        Me.Panel1.TabIndex = 2
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Panel4)
        Me.Panel3.Controls.Add(Me.UltraGrid1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1008, 679)
        Me.Panel3.TabIndex = 1
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.UltraTabConrol1)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1008, 679)
        Me.Panel4.TabIndex = 14
        '
        'UltraTabConrol1
        '
        Me.UltraTabConrol1.Controls.Add(Me.UltraTabSharedControlsPage3)
        Me.UltraTabConrol1.Controls.Add(Me.UltraTabPageControl3)
        Me.UltraTabConrol1.Controls.Add(Me.UltraTabPageControl4)
        Me.UltraTabConrol1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabConrol1.Location = New System.Drawing.Point(0, 0)
        Me.UltraTabConrol1.Name = "UltraTabConrol1"
        Me.UltraTabConrol1.SharedControlsPage = Me.UltraTabSharedControlsPage3
        Me.UltraTabConrol1.Size = New System.Drawing.Size(1008, 679)
        Me.UltraTabConrol1.TabIndex = 13
        Me.UltraTabConrol1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl3
        UltraTab1.Text = "Crietaria"
        UltraTab2.TabPage = Me.UltraTabPageControl4
        UltraTab2.Text = "Results"
        Me.UltraTabConrol1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        '
        'UltraTabSharedControlsPage3
        '
        Me.UltraTabSharedControlsPage3.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage3.Name = "UltraTabSharedControlsPage3"
        Me.UltraTabSharedControlsPage3.Size = New System.Drawing.Size(1004, 653)
        '
        'UltraGrid1
        '
        Appearance13.BackColor = System.Drawing.SystemColors.Window
        Appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption
        Me.UltraGrid1.DisplayLayout.Appearance = Appearance13
        Me.UltraGrid1.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.UltraGrid1.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder
        Appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance14.BorderColor = System.Drawing.SystemColors.Window
        Me.UltraGrid1.DisplayLayout.GroupByBox.Appearance = Appearance14
        Appearance15.ForeColor = System.Drawing.SystemColors.GrayText
        Me.UltraGrid1.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance15
        Me.UltraGrid1.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance16.BackColor2 = System.Drawing.SystemColors.Control
        Appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance16.ForeColor = System.Drawing.SystemColors.GrayText
        Me.UltraGrid1.DisplayLayout.GroupByBox.PromptAppearance = Appearance16
        Me.UltraGrid1.DisplayLayout.MaxColScrollRegions = 1
        Me.UltraGrid1.DisplayLayout.MaxRowScrollRegions = 1
        Appearance17.BackColor = System.Drawing.SystemColors.Window
        Appearance17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.UltraGrid1.DisplayLayout.Override.ActiveCellAppearance = Appearance17
        Appearance18.BackColor = System.Drawing.SystemColors.Highlight
        Appearance18.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.UltraGrid1.DisplayLayout.Override.ActiveRowAppearance = Appearance18
        Me.UltraGrid1.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.UltraGrid1.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance12.BackColor = System.Drawing.SystemColors.Window
        Me.UltraGrid1.DisplayLayout.Override.CardAreaAppearance = Appearance12
        Appearance19.BorderColor = System.Drawing.Color.Silver
        Appearance19.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.UltraGrid1.DisplayLayout.Override.CellAppearance = Appearance19
        Me.UltraGrid1.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.UltraGrid1.DisplayLayout.Override.CellPadding = 0
        Appearance20.BackColor = System.Drawing.SystemColors.Control
        Appearance20.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance20.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance20.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance20.BorderColor = System.Drawing.SystemColors.Window
        Me.UltraGrid1.DisplayLayout.Override.GroupByRowAppearance = Appearance20
        Appearance21.TextHAlignAsString = "Left"
        Me.UltraGrid1.DisplayLayout.Override.HeaderAppearance = Appearance21
        Me.UltraGrid1.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.UltraGrid1.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance11.BackColor = System.Drawing.SystemColors.Window
        Appearance11.BorderColor = System.Drawing.Color.Silver
        Me.UltraGrid1.DisplayLayout.Override.RowAppearance = Appearance11
        Me.UltraGrid1.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance22.BackColor = System.Drawing.SystemColors.ControlLight
        Me.UltraGrid1.DisplayLayout.Override.TemplateAddRowAppearance = Appearance22
        Me.UltraGrid1.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.UltraGrid1.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.UltraGrid1.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.UltraGrid1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraGrid1.Location = New System.Drawing.Point(0, 0)
        Me.UltraGrid1.Name = "UltraGrid1"
        Me.UltraGrid1.Size = New System.Drawing.Size(1008, 679)
        Me.UltraGrid1.TabIndex = 0
        Me.UltraGrid1.Text = "UltraGrid1"
        '
        'lstPropertyType
        '
        Me.lstPropertyType.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstPropertyType.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstPropertyType.BackColor = System.Drawing.Color.Transparent
        Me.lstPropertyType.disableWhenChecked = False
        Me.lstPropertyType.HeadingLabelName = "Property Type"
        Me.lstPropertyType.HeadingText = "Property Type"
        Me.lstPropertyType.Location = New System.Drawing.Point(19, 355)
        Me.lstPropertyType.Name = "lstPropertyType"
        Me.lstPropertyType.ShowAddNewButton = False
        Me.lstPropertyType.ShowInverse = True
        Me.lstPropertyType.ShowMagnifierButton = False
        Me.lstPropertyType.ShowNoCheck = False
        Me.lstPropertyType.ShowResetAllButton = False
        Me.lstPropertyType.ShowSelectall = True
        Me.lstPropertyType.Size = New System.Drawing.Size(189, 198)
        Me.lstPropertyType.TabIndex = 35
        Me.lstPropertyType.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstAgentName
        '
        Me.lstAgentName.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstAgentName.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstAgentName.BackColor = System.Drawing.Color.Transparent
        Me.lstAgentName.disableWhenChecked = False
        Me.lstAgentName.HeadingLabelName = "Agent Name"
        Me.lstAgentName.HeadingText = "Agent Name"
        Me.lstAgentName.Location = New System.Drawing.Point(489, 130)
        Me.lstAgentName.Name = "lstAgentName"
        Me.lstAgentName.ShowAddNewButton = False
        Me.lstAgentName.ShowInverse = True
        Me.lstAgentName.ShowMagnifierButton = False
        Me.lstAgentName.ShowNoCheck = False
        Me.lstAgentName.ShowResetAllButton = False
        Me.lstAgentName.ShowSelectall = True
        Me.lstAgentName.Size = New System.Drawing.Size(189, 198)
        Me.lstAgentName.TabIndex = 33
        Me.lstAgentName.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstProjectName
        '
        Me.lstProjectName.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstProjectName.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstProjectName.BackColor = System.Drawing.Color.Transparent
        Me.lstProjectName.disableWhenChecked = False
        Me.lstProjectName.HeadingLabelName = "Project Name"
        Me.lstProjectName.HeadingText = "Project Name"
        Me.lstProjectName.Location = New System.Drawing.Point(254, 130)
        Me.lstProjectName.Name = "lstProjectName"
        Me.lstProjectName.ShowAddNewButton = False
        Me.lstProjectName.ShowInverse = True
        Me.lstProjectName.ShowMagnifierButton = False
        Me.lstProjectName.ShowNoCheck = False
        Me.lstProjectName.ShowResetAllButton = False
        Me.lstProjectName.ShowSelectall = True
        Me.lstProjectName.Size = New System.Drawing.Size(189, 198)
        Me.lstProjectName.TabIndex = 32
        Me.lstProjectName.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstProjectHead
        '
        Me.lstProjectHead.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstProjectHead.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstProjectHead.BackColor = System.Drawing.Color.Transparent
        Me.lstProjectHead.disableWhenChecked = False
        Me.lstProjectHead.HeadingLabelName = "Project Head"
        Me.lstProjectHead.HeadingText = "Project Head"
        Me.lstProjectHead.Location = New System.Drawing.Point(19, 130)
        Me.lstProjectHead.Name = "lstProjectHead"
        Me.lstProjectHead.ShowAddNewButton = False
        Me.lstProjectHead.ShowInverse = True
        Me.lstProjectHead.ShowMagnifierButton = False
        Me.lstProjectHead.ShowNoCheck = False
        Me.lstProjectHead.ShowResetAllButton = False
        Me.lstProjectHead.ShowSelectall = True
        Me.lstProjectHead.Size = New System.Drawing.Size(189, 198)
        Me.lstProjectHead.TabIndex = 31
        Me.lstProjectHead.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.White
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(956, 12)
        Me.CtrlGrdBar1.MyGrid = Me.GridResults
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(40, 24)
        Me.CtrlGrdBar1.TabIndex = 90
        '
        'frmPropertyProfileDetailReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlHeader)
        Me.Name = "frmPropertyProfileDetailReport"
        Me.Text = "Property Profile Detail Report"
        Me.UltraTabPageControl3.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.UltraTabPageControl4.ResumeLayout(False)
        CType(Me.GridResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        CType(Me.UltraTabConrol1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabConrol1.ResumeLayout(False)
        CType(Me.UltraGrid1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents UltraGrid1 As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents UltraTabConrol1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage3 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl3 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents UltraTabPageControl4 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents GridResults As Janus.Windows.GridEX.GridEX
    Friend WithEvents lstPropertyType As SimpleAccounts.uiListControl
    Friend WithEvents lstAgentName As SimpleAccounts.uiListControl
    Friend WithEvents lstProjectName As SimpleAccounts.uiListControl
    Friend WithEvents lstProjectHead As SimpleAccounts.uiListControl
    Friend WithEvents refresh As System.Windows.Forms.Button
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents txtAgentSearch As System.Windows.Forms.TextBox
    Friend WithEvents rdoAllDeals As System.Windows.Forms.RadioButton
    Friend WithEvents rdoPending As System.Windows.Forms.RadioButton
    Friend WithEvents rdoCompleteDeals As System.Windows.Forms.RadioButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
End Class

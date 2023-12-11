<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class rptStockReportWithCritera
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
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn1 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleId")
        Dim UltraGridColumn2 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleDescription")
        Dim UltraGridColumn3 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleTypeName", -1, Nothing, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, False)
        Dim UltraGridColumn4 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleColorName")
        Dim UltraGridColumn5 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("AticleSizeName")
        Dim UltraGridColumn6 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("SalesQty")
        Dim UltraGridColumn7 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("PurchaseQty")
        Dim UltraGridColumn8 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("DispatchQty")
        Dim UltraGridColumn9 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("SalesReturnQty")
        Dim UltraGridColumn10 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("PurchaseReturnQty")
        Dim UltraGridColumn11 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("stock")
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
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptStockReportWithCritera))
        Me.TabStockReport = New System.Windows.Forms.TabControl()
        Me.TbCeritera = New System.Windows.Forms.TabPage()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.rdbBatchWise = New System.Windows.Forms.RadioButton()
        Me.rdbAll = New System.Windows.Forms.RadioButton()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.ItemCode2 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ItemCode = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.UiListCombination = New SimpleAccounts.uiListControl()
        Me.UiListSizes = New SimpleAccounts.uiListControl()
        Me.UiListGender = New SimpleAccounts.uiListControl()
        Me.UiListTypes = New SimpleAccounts.uiListControl()
        Me.UiListCategories = New SimpleAccounts.uiListControl()
        Me.TbResult = New System.Windows.Forms.TabPage()
        Me.grdStock = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.TabStockReport.SuspendLayout()
        Me.TbCeritera.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TbResult.SuspendLayout()
        CType(Me.grdStock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabStockReport
        '
        Me.TabStockReport.Controls.Add(Me.TbCeritera)
        Me.TabStockReport.Controls.Add(Me.TbResult)
        Me.TabStockReport.Location = New System.Drawing.Point(0, 59)
        Me.TabStockReport.Multiline = True
        Me.TabStockReport.Name = "TabStockReport"
        Me.TabStockReport.SelectedIndex = 0
        Me.TabStockReport.Size = New System.Drawing.Size(724, 537)
        Me.TabStockReport.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.TabStockReport.TabIndex = 1
        '
        'TbCeritera
        '
        Me.TbCeritera.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.TbCeritera.Controls.Add(Me.GroupBox1)
        Me.TbCeritera.Location = New System.Drawing.Point(4, 22)
        Me.TbCeritera.Name = "TbCeritera"
        Me.TbCeritera.Padding = New System.Windows.Forms.Padding(3)
        Me.TbCeritera.Size = New System.Drawing.Size(716, 511)
        Me.TbCeritera.TabIndex = 0
        Me.TbCeritera.Text = "Critera"
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.GroupBox1.Controls.Add(Me.Label9)
        Me.GroupBox1.Controls.Add(Me.rdbBatchWise)
        Me.GroupBox1.Controls.Add(Me.rdbAll)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.btnPrint)
        Me.GroupBox1.Controls.Add(Me.ItemCode2)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.ItemCode)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.UiListCombination)
        Me.GroupBox1.Controls.Add(Me.UiListSizes)
        Me.GroupBox1.Controls.Add(Me.UiListGender)
        Me.GroupBox1.Controls.Add(Me.UiListTypes)
        Me.GroupBox1.Controls.Add(Me.UiListCategories)
        Me.GroupBox1.Location = New System.Drawing.Point(8, 17)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(686, 513)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label9.Location = New System.Drawing.Point(5, 73)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(83, 13)
        Me.Label9.TabIndex = 4
        Me.Label9.Text = "Select Option"
        '
        'rdbBatchWise
        '
        Me.rdbBatchWise.AutoSize = True
        Me.rdbBatchWise.Location = New System.Drawing.Point(170, 73)
        Me.rdbBatchWise.Name = "rdbBatchWise"
        Me.rdbBatchWise.Size = New System.Drawing.Size(80, 17)
        Me.rdbBatchWise.TabIndex = 6
        Me.rdbBatchWise.Text = "&Batch Wise"
        Me.rdbBatchWise.UseVisualStyleBackColor = True
        '
        'rdbAll
        '
        Me.rdbAll.AutoSize = True
        Me.rdbAll.Checked = True
        Me.rdbAll.Location = New System.Drawing.Point(128, 73)
        Me.rdbAll.Name = "rdbAll"
        Me.rdbAll.Size = New System.Drawing.Size(36, 17)
        Me.rdbAll.TabIndex = 5
        Me.rdbAll.TabStop = True
        Me.rdbAll.Text = "&All"
        Me.rdbAll.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label8.Location = New System.Drawing.Point(547, 110)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(85, 13)
        Me.Label8.TabIndex = 15
        Me.Label8.Text = "Combinations"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label7.Location = New System.Drawing.Point(415, 110)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(37, 13)
        Me.Label7.TabIndex = 13
        Me.Label7.Text = "Sizes"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label6.Location = New System.Drawing.Point(276, 110)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(55, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "Genders"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label5.Location = New System.Drawing.Point(137, 110)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Types"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label4.Location = New System.Drawing.Point(9, 110)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(69, 13)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Cetagories"
        '
        'btnPrint
        '
        Me.btnPrint.Location = New System.Drawing.Point(481, 457)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(182, 38)
        Me.btnPrint.TabIndex = 17
        Me.btnPrint.Text = "Generate Report"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'ItemCode2
        '
        Me.ItemCode2.Location = New System.Drawing.Point(278, 38)
        Me.ItemCode2.Name = "ItemCode2"
        Me.ItemCode2.Size = New System.Drawing.Size(123, 20)
        Me.ItemCode2.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(257, 38)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(13, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "_"
        '
        'ItemCode
        '
        Me.ItemCode.Location = New System.Drawing.Point(128, 38)
        Me.ItemCode.Name = "ItemCode"
        Me.ItemCode.Size = New System.Drawing.Size(123, 20)
        Me.ItemCode.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.Label1.Location = New System.Drawing.Point(5, 41)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(108, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Item Code Range"
        '
        'UiListCombination
        '
        Me.UiListCombination.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.UiListCombination.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.UiListCombination.BackColor = System.Drawing.Color.Transparent
        Me.UiListCombination.disableWhenChecked = False
        Me.UiListCombination.HeadingLabelName = Nothing
        Me.UiListCombination.HeadingText = Nothing
        Me.UiListCombination.Location = New System.Drawing.Point(550, 126)
        Me.UiListCombination.Name = "UiListCombination"
        Me.UiListCombination.ShowAddNewButton = False
        Me.UiListCombination.ShowInverse = True
        Me.UiListCombination.ShowMagnifierButton = False
        Me.UiListCombination.ShowNoCheck = False
        Me.UiListCombination.ShowResetAllButton = False
        Me.UiListCombination.ShowSelectall = True
        Me.UiListCombination.Size = New System.Drawing.Size(132, 294)
        Me.UiListCombination.TabIndex = 16
        Me.UiListCombination.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'UiListSizes
        '
        Me.UiListSizes.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.UiListSizes.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.UiListSizes.BackColor = System.Drawing.Color.Transparent
        Me.UiListSizes.disableWhenChecked = False
        Me.UiListSizes.HeadingLabelName = Nothing
        Me.UiListSizes.HeadingText = Nothing
        Me.UiListSizes.Location = New System.Drawing.Point(415, 126)
        Me.UiListSizes.Name = "UiListSizes"
        Me.UiListSizes.ShowAddNewButton = False
        Me.UiListSizes.ShowInverse = True
        Me.UiListSizes.ShowMagnifierButton = False
        Me.UiListSizes.ShowNoCheck = False
        Me.UiListSizes.ShowResetAllButton = False
        Me.UiListSizes.ShowSelectall = True
        Me.UiListSizes.Size = New System.Drawing.Size(132, 294)
        Me.UiListSizes.TabIndex = 14
        Me.UiListSizes.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'UiListGender
        '
        Me.UiListGender.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.UiListGender.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.UiListGender.BackColor = System.Drawing.Color.Transparent
        Me.UiListGender.disableWhenChecked = False
        Me.UiListGender.HeadingLabelName = Nothing
        Me.UiListGender.HeadingText = Nothing
        Me.UiListGender.Location = New System.Drawing.Point(277, 126)
        Me.UiListGender.Name = "UiListGender"
        Me.UiListGender.ShowAddNewButton = False
        Me.UiListGender.ShowInverse = True
        Me.UiListGender.ShowMagnifierButton = False
        Me.UiListGender.ShowNoCheck = False
        Me.UiListGender.ShowResetAllButton = False
        Me.UiListGender.ShowSelectall = True
        Me.UiListGender.Size = New System.Drawing.Size(132, 294)
        Me.UiListGender.TabIndex = 12
        Me.UiListGender.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'UiListTypes
        '
        Me.UiListTypes.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.UiListTypes.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.UiListTypes.BackColor = System.Drawing.Color.Transparent
        Me.UiListTypes.disableWhenChecked = False
        Me.UiListTypes.HeadingLabelName = Nothing
        Me.UiListTypes.HeadingText = Nothing
        Me.UiListTypes.Location = New System.Drawing.Point(137, 126)
        Me.UiListTypes.Name = "UiListTypes"
        Me.UiListTypes.ShowAddNewButton = False
        Me.UiListTypes.ShowInverse = True
        Me.UiListTypes.ShowMagnifierButton = False
        Me.UiListTypes.ShowNoCheck = False
        Me.UiListTypes.ShowResetAllButton = False
        Me.UiListTypes.ShowSelectall = True
        Me.UiListTypes.Size = New System.Drawing.Size(134, 294)
        Me.UiListTypes.TabIndex = 10
        Me.UiListTypes.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'UiListCategories
        '
        Me.UiListCategories.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.UiListCategories.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.UiListCategories.BackColor = System.Drawing.Color.Transparent
        Me.UiListCategories.disableWhenChecked = False
        Me.UiListCategories.HeadingLabelName = Nothing
        Me.UiListCategories.HeadingText = Nothing
        Me.UiListCategories.Location = New System.Drawing.Point(6, 126)
        Me.UiListCategories.Name = "UiListCategories"
        Me.UiListCategories.ShowAddNewButton = False
        Me.UiListCategories.ShowInverse = True
        Me.UiListCategories.ShowMagnifierButton = False
        Me.UiListCategories.ShowNoCheck = False
        Me.UiListCategories.ShowResetAllButton = False
        Me.UiListCategories.ShowSelectall = True
        Me.UiListCategories.Size = New System.Drawing.Size(131, 294)
        Me.UiListCategories.TabIndex = 8
        Me.UiListCategories.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'TbResult
        '
        Me.TbResult.Controls.Add(Me.grdStock)
        Me.TbResult.Location = New System.Drawing.Point(4, 22)
        Me.TbResult.Name = "TbResult"
        Me.TbResult.Padding = New System.Windows.Forms.Padding(3)
        Me.TbResult.Size = New System.Drawing.Size(716, 548)
        Me.TbResult.TabIndex = 1
        Me.TbResult.Text = "Result"
        Me.TbResult.UseVisualStyleBackColor = True
        '
        'grdStock
        '
        Appearance1.BackColor = System.Drawing.Color.White
        Me.grdStock.DisplayLayout.Appearance = Appearance1
        UltraGridColumn1.Header.VisiblePosition = 0
        UltraGridColumn1.Hidden = True
        UltraGridColumn2.Header.Caption = "Item Name"
        UltraGridColumn2.Header.VisiblePosition = 1
        UltraGridColumn2.Width = 146
        UltraGridColumn3.Header.Caption = "Type"
        UltraGridColumn3.Header.VisiblePosition = 2
        UltraGridColumn4.Header.Caption = "Color"
        UltraGridColumn4.Header.VisiblePosition = 3
        UltraGridColumn4.Width = 86
        UltraGridColumn5.Header.Caption = "Size"
        UltraGridColumn5.Header.VisiblePosition = 4
        UltraGridColumn5.Width = 63
        UltraGridColumn6.Header.Caption = "Sales"
        UltraGridColumn6.Header.VisiblePosition = 5
        UltraGridColumn6.Width = 59
        UltraGridColumn7.Header.Caption = "Purchase"
        UltraGridColumn7.Header.VisiblePosition = 6
        UltraGridColumn7.Width = 87
        UltraGridColumn8.Header.Caption = "Issued"
        UltraGridColumn8.Header.VisiblePosition = 7
        UltraGridColumn8.Width = 71
        UltraGridColumn9.Header.Caption = "S R"
        UltraGridColumn9.Header.VisiblePosition = 8
        UltraGridColumn9.Width = 76
        UltraGridColumn10.Header.Caption = "P R"
        UltraGridColumn10.Header.VisiblePosition = 9
        UltraGridColumn10.Width = 69
        UltraGridColumn11.Header.Caption = "Stock"
        UltraGridColumn11.Header.VisiblePosition = 10
        UltraGridColumn11.Width = 68
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn1, UltraGridColumn2, UltraGridColumn3, UltraGridColumn4, UltraGridColumn5, UltraGridColumn6, UltraGridColumn7, UltraGridColumn8, UltraGridColumn9, UltraGridColumn10, UltraGridColumn11})
        Me.grdStock.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.grdStock.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.grdStock.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder
        Appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance2.BorderColor = System.Drawing.SystemColors.Window
        Me.grdStock.DisplayLayout.GroupByBox.Appearance = Appearance2
        Appearance3.ForeColor = System.Drawing.SystemColors.GrayText
        Me.grdStock.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance3
        Me.grdStock.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance4.BackColor2 = System.Drawing.SystemColors.Control
        Appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance4.ForeColor = System.Drawing.SystemColors.GrayText
        Me.grdStock.DisplayLayout.GroupByBox.PromptAppearance = Appearance4
        Me.grdStock.DisplayLayout.MaxColScrollRegions = 1
        Me.grdStock.DisplayLayout.MaxRowScrollRegions = 1
        Appearance5.BackColor = System.Drawing.SystemColors.Window
        Appearance5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grdStock.DisplayLayout.Override.ActiveCellAppearance = Appearance5
        Appearance6.BackColor = System.Drawing.SystemColors.Highlight
        Appearance6.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.grdStock.DisplayLayout.Override.ActiveRowAppearance = Appearance6
        Me.grdStock.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.grdStock.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.grdStock.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.[True]
        Me.grdStock.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.BasedOnDataType
        Me.grdStock.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.grdStock.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Me.grdStock.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance7.BackColor = System.Drawing.Color.Transparent
        Me.grdStock.DisplayLayout.Override.CardAreaAppearance = Appearance7
        Appearance8.BorderColor = System.Drawing.Color.Silver
        Appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.grdStock.DisplayLayout.Override.CellAppearance = Appearance8
        Me.grdStock.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.grdStock.DisplayLayout.Override.CellPadding = 3
        Appearance9.BackColor = System.Drawing.SystemColors.Control
        Appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance9.BorderColor = System.Drawing.SystemColors.Window
        Me.grdStock.DisplayLayout.Override.GroupByRowAppearance = Appearance9
        Appearance10.TextHAlignAsString = "Left"
        Me.grdStock.DisplayLayout.Override.HeaderAppearance = Appearance10
        Me.grdStock.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.grdStock.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance11.BorderColor = System.Drawing.Color.LightGray
        Appearance11.TextVAlignAsString = "Middle"
        Me.grdStock.DisplayLayout.Override.RowAppearance = Appearance11
        Me.grdStock.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance12.BackColor = System.Drawing.Color.LightSteelBlue
        Appearance12.BorderColor = System.Drawing.Color.Black
        Appearance12.ForeColor = System.Drawing.Color.Black
        Me.grdStock.DisplayLayout.Override.SelectedRowAppearance = Appearance12
        Appearance13.BackColor = System.Drawing.SystemColors.ControlLight
        Me.grdStock.DisplayLayout.Override.TemplateAddRowAppearance = Appearance13
        Me.grdStock.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.grdStock.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.grdStock.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.grdStock.DisplayLayout.UseFixedHeaders = True
        Me.grdStock.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdStock.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdStock.Location = New System.Drawing.Point(3, 3)
        Me.grdStock.Name = "grdStock"
        Me.grdStock.Size = New System.Drawing.Size(710, 542)
        Me.grdStock.TabIndex = 30
        Me.grdStock.Text = "grdStock"
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.lblHeader.Location = New System.Drawing.Point(17, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(122, 18)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Stock Report"
        Me.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlHeader
        '
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(723, 42)
        Me.pnlHeader.TabIndex = 83
        '
        'rptStockReportWithCritera
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.ClientSize = New System.Drawing.Size(723, 596)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.TabStockReport)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "rptStockReportWithCritera"
        Me.ShowInTaskbar = False
        Me.Text = "Stock Report"
        Me.TabStockReport.ResumeLayout(False)
        Me.TbCeritera.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TbResult.ResumeLayout(False)
        CType(Me.grdStock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabStockReport As System.Windows.Forms.TabControl
    Friend WithEvents TbCeritera As System.Windows.Forms.TabPage
    Friend WithEvents TbResult As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents UiListCombination As SimpleAccounts.uiListControl
    Friend WithEvents UiListSizes As SimpleAccounts.uiListControl
    Friend WithEvents UiListGender As SimpleAccounts.uiListControl
    Friend WithEvents UiListTypes As SimpleAccounts.uiListControl
    Friend WithEvents UiListCategories As SimpleAccounts.uiListControl
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents ItemCode2 As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ItemCode As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents grdStock As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents rdbBatchWise As System.Windows.Forms.RadioButton
    Friend WithEvents rdbAll As System.Windows.Forms.RadioButton
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

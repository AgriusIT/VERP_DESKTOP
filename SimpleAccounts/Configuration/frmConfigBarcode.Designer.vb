<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfigBarcode
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
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbBarcodeItems = New System.Windows.Forms.ListBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtBarCodeItems = New System.Windows.Forms.TextBox()
        Me.txtDisabledBarCodeItems = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lbDiabledBarcode = New System.Windows.Forms.ListBox()
        Me.btnMove = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lbPrinterList = New System.Windows.Forms.ListBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cbProname = New System.Windows.Forms.CheckBox()
        Me.cbProPrice = New System.Windows.Forms.CheckBox()
        Me.cbProductCode = New System.Windows.Forms.CheckBox()
        Me.cbVendorCode = New System.Windows.Forms.CheckBox()
        Me.cbPackQuantity = New System.Windows.Forms.CheckBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtPrintCount = New System.Windows.Forms.TextBox()
        Me.cmbFont = New System.Windows.Forms.ComboBox()
        Me.cmbFonts = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.lbUiBarcodeItems = New SimpleAccounts.uiListControl()
        Me.lbUiDiabledBarcode = New SimpleAccounts.uiListControl()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmbFontSize = New System.Windows.Forms.ComboBox()
        Me.txtBarCodeLeftGap = New System.Windows.Forms.TextBox()
        Me.txtBarCodeTopGap = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmbDefaultBarCodeSource = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cbProSize = New System.Windows.Forms.CheckBox()
        Me.cbPrintDate = New System.Windows.Forms.CheckBox()
        Me.cbCompanyName = New System.Windows.Forms.CheckBox()
        Me.Panel2.SuspendLayout()
        CType(Me.cmbFonts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1008, 61)
        Me.Panel2.TabIndex = 13
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(12, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(295, 37)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Barcode Configurations"
        '
        'lbBarcodeItems
        '
        Me.lbBarcodeItems.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbBarcodeItems.FormattingEnabled = True
        Me.lbBarcodeItems.ItemHeight = 20
        Me.lbBarcodeItems.Location = New System.Drawing.Point(19, 128)
        Me.lbBarcodeItems.Name = "lbBarcodeItems"
        Me.lbBarcodeItems.Size = New System.Drawing.Size(288, 164)
        Me.lbBarcodeItems.TabIndex = 14
        Me.lbBarcodeItems.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(14, 97)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(134, 25)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Barcode Items"
        '
        'txtBarCodeItems
        '
        Me.txtBarCodeItems.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBarCodeItems.Location = New System.Drawing.Point(19, 314)
        Me.txtBarCodeItems.Name = "txtBarCodeItems"
        Me.txtBarCodeItems.Size = New System.Drawing.Size(288, 29)
        Me.txtBarCodeItems.TabIndex = 16
        '
        'txtDisabledBarCodeItems
        '
        Me.txtDisabledBarCodeItems.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDisabledBarCodeItems.Location = New System.Drawing.Point(414, 314)
        Me.txtDisabledBarCodeItems.Name = "txtDisabledBarCodeItems"
        Me.txtDisabledBarCodeItems.Size = New System.Drawing.Size(288, 29)
        Me.txtDisabledBarCodeItems.TabIndex = 19
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(409, 97)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(213, 25)
        Me.Label3.TabIndex = 18
        Me.Label3.Text = "Disabled Barcode Items"
        '
        'lbDiabledBarcode
        '
        Me.lbDiabledBarcode.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbDiabledBarcode.FormattingEnabled = True
        Me.lbDiabledBarcode.ItemHeight = 20
        Me.lbDiabledBarcode.Location = New System.Drawing.Point(414, 128)
        Me.lbDiabledBarcode.Name = "lbDiabledBarcode"
        Me.lbDiabledBarcode.Size = New System.Drawing.Size(288, 164)
        Me.lbDiabledBarcode.TabIndex = 17
        Me.lbDiabledBarcode.Visible = False
        '
        'btnMove
        '
        Me.btnMove.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnMove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMove.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMove.Location = New System.Drawing.Point(334, 196)
        Me.btnMove.Name = "btnMove"
        Me.btnMove.Size = New System.Drawing.Size(53, 36)
        Me.btnMove.TabIndex = 20
        Me.btnMove.Text = "< >"
        Me.btnMove.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(14, 353)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(144, 25)
        Me.Label4.TabIndex = 22
        Me.Label4.Text = "Default Printer " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'lbPrinterList
        '
        Me.lbPrinterList.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbPrinterList.FormattingEnabled = True
        Me.lbPrinterList.ItemHeight = 20
        Me.lbPrinterList.Location = New System.Drawing.Point(19, 384)
        Me.lbPrinterList.Name = "lbPrinterList"
        Me.lbPrinterList.Size = New System.Drawing.Size(288, 164)
        Me.lbPrinterList.TabIndex = 21
        Me.lbPrinterList.Tag = "PrinterNameForBarCode"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(409, 353)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(257, 25)
        Me.Label5.TabIndex = 23
        Me.Label5.Text = "Barcode Dispaly Information"
        '
        'cbProname
        '
        Me.cbProname.AutoSize = True
        Me.cbProname.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbProname.Location = New System.Drawing.Point(414, 400)
        Me.cbProname.Name = "cbProname"
        Me.cbProname.Size = New System.Drawing.Size(129, 25)
        Me.cbProname.TabIndex = 24
        Me.cbProname.Tag = "BARCodeDisplayInformation"
        Me.cbProname.Text = "Product Name"
        Me.cbProname.UseVisualStyleBackColor = True
        '
        'cbProPrice
        '
        Me.cbProPrice.AutoSize = True
        Me.cbProPrice.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbProPrice.Location = New System.Drawing.Point(573, 400)
        Me.cbProPrice.Name = "cbProPrice"
        Me.cbProPrice.Size = New System.Drawing.Size(121, 25)
        Me.cbProPrice.TabIndex = 25
        Me.cbProPrice.Tag = "BARCodeDisplayInformation"
        Me.cbProPrice.Text = "Product Price"
        Me.cbProPrice.UseVisualStyleBackColor = True
        '
        'cbProductCode
        '
        Me.cbProductCode.AutoSize = True
        Me.cbProductCode.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbProductCode.Location = New System.Drawing.Point(414, 431)
        Me.cbProductCode.Name = "cbProductCode"
        Me.cbProductCode.Size = New System.Drawing.Size(123, 25)
        Me.cbProductCode.TabIndex = 26
        Me.cbProductCode.Tag = "BARCodeDisplayInformation"
        Me.cbProductCode.Text = "Product Code"
        Me.cbProductCode.UseVisualStyleBackColor = True
        '
        'cbVendorCode
        '
        Me.cbVendorCode.AutoSize = True
        Me.cbVendorCode.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbVendorCode.Location = New System.Drawing.Point(414, 493)
        Me.cbVendorCode.Name = "cbVendorCode"
        Me.cbVendorCode.Size = New System.Drawing.Size(119, 25)
        Me.cbVendorCode.TabIndex = 27
        Me.cbVendorCode.Tag = "BARCodeDisplayInformation"
        Me.cbVendorCode.Text = "Vendor Code"
        Me.cbVendorCode.UseVisualStyleBackColor = True
        '
        'cbPackQuantity
        '
        Me.cbPackQuantity.AutoSize = True
        Me.cbPackQuantity.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbPackQuantity.Location = New System.Drawing.Point(414, 462)
        Me.cbPackQuantity.Name = "cbPackQuantity"
        Me.cbPackQuantity.Size = New System.Drawing.Size(124, 25)
        Me.cbPackQuantity.TabIndex = 28
        Me.cbPackQuantity.Tag = "BARCodeDisplayInformation"
        Me.cbPackQuantity.Text = "Pack Quantity"
        Me.cbPackQuantity.UseVisualStyleBackColor = True
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(14, 577)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(110, 25)
        Me.Label7.TabIndex = 31
        Me.Label7.Text = "Print Count"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(799, 505)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(164, 25)
        Me.Label6.TabIndex = 32
        Me.Label6.Text = "Barcode Font Size"
        Me.Label6.Visible = False
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(779, 446)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(125, 25)
        Me.Label8.TabIndex = 33
        Me.Label8.Text = "Barcode Font"
        Me.Label8.Visible = False
        '
        'txtPrintCount
        '
        Me.txtPrintCount.Font = New System.Drawing.Font("Segoe UI", 12.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPrintCount.Location = New System.Drawing.Point(19, 605)
        Me.txtPrintCount.Name = "txtPrintCount"
        Me.txtPrintCount.Size = New System.Drawing.Size(153, 30)
        Me.txtPrintCount.TabIndex = 35
        Me.txtPrintCount.Tag = "PrintCountForBarCode"
        Me.txtPrintCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cmbFont
        '
        Me.cmbFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbFont.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.cmbFont.FormattingEnabled = True
        Me.cmbFont.Location = New System.Drawing.Point(784, 474)
        Me.cmbFont.Name = "cmbFont"
        Me.cmbFont.Size = New System.Drawing.Size(159, 28)
        Me.cmbFont.TabIndex = 38
        Me.cmbFont.Tag = "BarCodeFont"
        Me.cmbFont.Visible = False
        '
        'cmbFonts
        '
        Appearance1.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.cmbFonts.Appearance = Appearance1
        Me.cmbFonts.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cmbFonts.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbFonts.CheckedListSettings.CheckStateMember = ""
        Appearance2.BackColor = System.Drawing.SystemColors.Window
        Appearance2.BorderColor = System.Drawing.SystemColors.InactiveCaption
        Me.cmbFonts.DisplayLayout.Appearance = Appearance2
        Me.cmbFonts.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.cmbFonts.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance3.BackColor = System.Drawing.SystemColors.ActiveBorder
        Appearance3.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance3.BorderColor = System.Drawing.SystemColors.Window
        Me.cmbFonts.DisplayLayout.GroupByBox.Appearance = Appearance3
        Appearance4.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cmbFonts.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance4
        Me.cmbFonts.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance5.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance5.BackColor2 = System.Drawing.SystemColors.Control
        Appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance5.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cmbFonts.DisplayLayout.GroupByBox.PromptAppearance = Appearance5
        Me.cmbFonts.DisplayLayout.MaxColScrollRegions = 1
        Me.cmbFonts.DisplayLayout.MaxRowScrollRegions = 1
        Appearance6.BackColor = System.Drawing.SystemColors.Window
        Appearance6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmbFonts.DisplayLayout.Override.ActiveCellAppearance = Appearance6
        Appearance7.BackColor = System.Drawing.SystemColors.Highlight
        Appearance7.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.cmbFonts.DisplayLayout.Override.ActiveRowAppearance = Appearance7
        Me.cmbFonts.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.cmbFonts.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance8.BackColor = System.Drawing.SystemColors.Window
        Me.cmbFonts.DisplayLayout.Override.CardAreaAppearance = Appearance8
        Appearance9.BorderColor = System.Drawing.Color.Silver
        Appearance9.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.cmbFonts.DisplayLayout.Override.CellAppearance = Appearance9
        Me.cmbFonts.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.cmbFonts.DisplayLayout.Override.CellPadding = 0
        Appearance10.BackColor = System.Drawing.SystemColors.Control
        Appearance10.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance10.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance10.BorderColor = System.Drawing.SystemColors.Window
        Me.cmbFonts.DisplayLayout.Override.GroupByRowAppearance = Appearance10
        Appearance11.TextHAlignAsString = "Left"
        Me.cmbFonts.DisplayLayout.Override.HeaderAppearance = Appearance11
        Me.cmbFonts.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.cmbFonts.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance12.BackColor = System.Drawing.SystemColors.Window
        Appearance12.BorderColor = System.Drawing.Color.Silver
        Me.cmbFonts.DisplayLayout.Override.RowAppearance = Appearance12
        Me.cmbFonts.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance13.BackColor = System.Drawing.SystemColors.ControlLight
        Me.cmbFonts.DisplayLayout.Override.TemplateAddRowAppearance = Appearance13
        Me.cmbFonts.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbFonts.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbFonts.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.cmbFonts.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Office2007
        Me.cmbFonts.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.cmbFonts.LimitToList = True
        Me.cmbFonts.Location = New System.Drawing.Point(769, 603)
        Me.cmbFonts.Name = "cmbFonts"
        Me.cmbFonts.Size = New System.Drawing.Size(159, 30)
        Me.cmbFonts.TabIndex = 39
        Me.cmbFonts.Visible = False
        '
        'lbUiBarcodeItems
        '
        Me.lbUiBarcodeItems.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lbUiBarcodeItems.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lbUiBarcodeItems.BackColor = System.Drawing.Color.Transparent
        Me.lbUiBarcodeItems.disableWhenChecked = False
        Me.lbUiBarcodeItems.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lbUiBarcodeItems.HeadingLabelName = "lstEmpDepartment"
        Me.lbUiBarcodeItems.HeadingText = ""
        Me.lbUiBarcodeItems.Location = New System.Drawing.Point(19, 128)
        Me.lbUiBarcodeItems.Margin = New System.Windows.Forms.Padding(6)
        Me.lbUiBarcodeItems.Name = "lbUiBarcodeItems"
        Me.lbUiBarcodeItems.ShowAddNewButton = False
        Me.lbUiBarcodeItems.ShowInverse = True
        Me.lbUiBarcodeItems.ShowMagnifierButton = False
        Me.lbUiBarcodeItems.ShowNoCheck = False
        Me.lbUiBarcodeItems.ShowResetAllButton = False
        Me.lbUiBarcodeItems.ShowSelectall = True
        Me.lbUiBarcodeItems.Size = New System.Drawing.Size(288, 177)
        Me.lbUiBarcodeItems.TabIndex = 40
        Me.lbUiBarcodeItems.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lbUiDiabledBarcode
        '
        Me.lbUiDiabledBarcode.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lbUiDiabledBarcode.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lbUiDiabledBarcode.BackColor = System.Drawing.Color.Transparent
        Me.lbUiDiabledBarcode.disableWhenChecked = False
        Me.lbUiDiabledBarcode.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lbUiDiabledBarcode.HeadingLabelName = "lstEmpDepartment"
        Me.lbUiDiabledBarcode.HeadingText = ""
        Me.lbUiDiabledBarcode.Location = New System.Drawing.Point(414, 128)
        Me.lbUiDiabledBarcode.Margin = New System.Windows.Forms.Padding(6)
        Me.lbUiDiabledBarcode.Name = "lbUiDiabledBarcode"
        Me.lbUiDiabledBarcode.ShowAddNewButton = False
        Me.lbUiDiabledBarcode.ShowInverse = True
        Me.lbUiDiabledBarcode.ShowMagnifierButton = False
        Me.lbUiDiabledBarcode.ShowNoCheck = False
        Me.lbUiDiabledBarcode.ShowResetAllButton = False
        Me.lbUiDiabledBarcode.ShowSelectall = True
        Me.lbUiDiabledBarcode.Size = New System.Drawing.Size(288, 177)
        Me.lbUiDiabledBarcode.TabIndex = 41
        Me.lbUiDiabledBarcode.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(173, 575)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(155, 25)
        Me.Label9.TabIndex = 42
        Me.Label9.Text = "Barcode Top Gap"
        '
        'cmbFontSize
        '
        Me.cmbFontSize.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbFontSize.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbFontSize.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.cmbFontSize.FormattingEnabled = True
        Me.cmbFontSize.Location = New System.Drawing.Point(804, 533)
        Me.cmbFontSize.Name = "cmbFontSize"
        Me.cmbFontSize.Size = New System.Drawing.Size(159, 28)
        Me.cmbFontSize.TabIndex = 37
        Me.cmbFontSize.Tag = "BarCodeFontSize"
        Me.cmbFontSize.Visible = False
        '
        'txtBarCodeLeftGap
        '
        Me.txtBarCodeLeftGap.Font = New System.Drawing.Font("Segoe UI", 12.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBarCodeLeftGap.Location = New System.Drawing.Point(337, 605)
        Me.txtBarCodeLeftGap.Name = "txtBarCodeLeftGap"
        Me.txtBarCodeLeftGap.Size = New System.Drawing.Size(153, 30)
        Me.txtBarCodeLeftGap.TabIndex = 43
        Me.txtBarCodeLeftGap.Tag = "PrintBarcodeLeftGap"
        Me.txtBarCodeLeftGap.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtBarCodeTopGap
        '
        Me.txtBarCodeTopGap.Font = New System.Drawing.Font("Segoe UI", 12.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtBarCodeTopGap.Location = New System.Drawing.Point(178, 605)
        Me.txtBarCodeTopGap.Name = "txtBarCodeTopGap"
        Me.txtBarCodeTopGap.Size = New System.Drawing.Size(153, 30)
        Me.txtBarCodeTopGap.TabIndex = 44
        Me.txtBarCodeTopGap.Tag = "PrintBarcodeTopGap"
        Me.txtBarCodeTopGap.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(334, 577)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(158, 25)
        Me.Label10.TabIndex = 45
        Me.Label10.Text = "Barcode Left Gap"
        '
        'cmbDefaultBarCodeSource
        '
        Me.cmbDefaultBarCodeSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDefaultBarCodeSource.Font = New System.Drawing.Font("Segoe UI", 11.25!)
        Me.cmbDefaultBarCodeSource.FormattingEnabled = True
        Me.cmbDefaultBarCodeSource.Items.AddRange(New Object() {"Product ID", "Product Code"})
        Me.cmbDefaultBarCodeSource.Location = New System.Drawing.Point(496, 607)
        Me.cmbDefaultBarCodeSource.Name = "cmbDefaultBarCodeSource"
        Me.cmbDefaultBarCodeSource.Size = New System.Drawing.Size(223, 28)
        Me.cmbDefaultBarCodeSource.TabIndex = 46
        Me.cmbDefaultBarCodeSource.Tag = "BarCodeFont"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(491, 579)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(221, 25)
        Me.Label11.TabIndex = 47
        Me.Label11.Text = "Default Bar Code Source" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'cbProSize
        '
        Me.cbProSize.AutoSize = True
        Me.cbProSize.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbProSize.Location = New System.Drawing.Point(573, 431)
        Me.cbProSize.Name = "cbProSize"
        Me.cbProSize.Size = New System.Drawing.Size(115, 25)
        Me.cbProSize.TabIndex = 48
        Me.cbProSize.Tag = "BARCodeDisplayInformation"
        Me.cbProSize.Text = "Product Size"
        Me.cbProSize.UseVisualStyleBackColor = True
        '
        'cbPrintDate
        '
        Me.cbPrintDate.AutoSize = True
        Me.cbPrintDate.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbPrintDate.Location = New System.Drawing.Point(573, 462)
        Me.cbPrintDate.Name = "cbPrintDate"
        Me.cbPrintDate.Size = New System.Drawing.Size(98, 25)
        Me.cbPrintDate.TabIndex = 49
        Me.cbPrintDate.Tag = "BARCodeDisplayInformation"
        Me.cbPrintDate.Text = "Print Date"
        Me.cbPrintDate.UseVisualStyleBackColor = True
        '
        'cbCompanyName
        '
        Me.cbCompanyName.AutoSize = True
        Me.cbCompanyName.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cbCompanyName.Location = New System.Drawing.Point(573, 493)
        Me.cbCompanyName.Name = "cbCompanyName"
        Me.cbCompanyName.Size = New System.Drawing.Size(142, 25)
        Me.cbCompanyName.TabIndex = 50
        Me.cbCompanyName.Tag = "BARCodeDisplayInformation"
        Me.cbCompanyName.Text = "Company Name"
        Me.cbCompanyName.UseVisualStyleBackColor = True
        '
        'frmConfigBarcode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Controls.Add(Me.cbCompanyName)
        Me.Controls.Add(Me.cbPrintDate)
        Me.Controls.Add(Me.cbProSize)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.cmbDefaultBarCodeSource)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtBarCodeTopGap)
        Me.Controls.Add(Me.txtBarCodeLeftGap)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.lbUiDiabledBarcode)
        Me.Controls.Add(Me.lbUiBarcodeItems)
        Me.Controls.Add(Me.cmbFonts)
        Me.Controls.Add(Me.cmbFont)
        Me.Controls.Add(Me.cmbFontSize)
        Me.Controls.Add(Me.txtPrintCount)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cbPackQuantity)
        Me.Controls.Add(Me.cbVendorCode)
        Me.Controls.Add(Me.cbProductCode)
        Me.Controls.Add(Me.cbProPrice)
        Me.Controls.Add(Me.cbProname)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lbPrinterList)
        Me.Controls.Add(Me.btnMove)
        Me.Controls.Add(Me.txtDisabledBarCodeItems)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lbDiabledBarcode)
        Me.Controls.Add(Me.txtBarCodeItems)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lbBarcodeItems)
        Me.Controls.Add(Me.Panel2)
        Me.Name = "frmConfigBarcode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmConfigBarcode"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.cmbFonts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lbBarcodeItems As System.Windows.Forms.ListBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtBarCodeItems As System.Windows.Forms.TextBox
    Friend WithEvents txtDisabledBarCodeItems As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lbDiabledBarcode As System.Windows.Forms.ListBox
    Friend WithEvents btnMove As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lbPrinterList As System.Windows.Forms.ListBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cbProname As System.Windows.Forms.CheckBox
    Friend WithEvents cbProPrice As System.Windows.Forms.CheckBox
    Friend WithEvents cbProductCode As System.Windows.Forms.CheckBox
    Friend WithEvents cbVendorCode As System.Windows.Forms.CheckBox
    Friend WithEvents cbPackQuantity As System.Windows.Forms.CheckBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtPrintCount As System.Windows.Forms.TextBox
    Friend WithEvents cmbFont As System.Windows.Forms.ComboBox
    Friend WithEvents cmbFonts As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents lbUiBarcodeItems As SimpleAccounts.uiListControl
    Friend WithEvents lbUiDiabledBarcode As SimpleAccounts.uiListControl
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cmbFontSize As System.Windows.Forms.ComboBox
    Friend WithEvents txtBarCodeLeftGap As System.Windows.Forms.TextBox
    Friend WithEvents txtBarCodeTopGap As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cmbDefaultBarCodeSource As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cbProSize As System.Windows.Forms.CheckBox
    Friend WithEvents cbPrintDate As System.Windows.Forms.CheckBox
    Friend WithEvents cbCompanyName As System.Windows.Forms.CheckBox
End Class

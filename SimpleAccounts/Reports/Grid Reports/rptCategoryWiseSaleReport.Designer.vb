<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class rptCategoryWiseSaleReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(rptCategoryWiseSaleReport))
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
        Dim UltraDataColumn1 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("ArticleId")
        Dim UltraDataColumn2 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("ArticleDescription")
        Dim UltraDataColumn3 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("ArticleTypeName")
        Dim UltraDataColumn4 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("ArticleColorName")
        Dim UltraDataColumn5 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("AticleSizeName")
        Dim UltraDataColumn6 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("SalesQty")
        Dim UltraDataColumn7 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("PurchaseQty")
        Dim UltraDataColumn8 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("DispatchQty")
        Dim UltraDataColumn9 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("SalesReturnQty")
        Dim UltraDataColumn10 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("PurchaseReturnQty")
        Dim UltraDataColumn11 As Infragistics.Win.UltraWinDataSource.UltraDataColumn = New Infragistics.Win.UltraWinDataSource.UltraDataColumn("stock")
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.NewToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.OpenToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SaveToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.DeleteToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.PrintToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.grdStock = New Infragistics.Win.UltraWinGrid.UltraGrid()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.RefreshDataToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportToExcelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UltraDataSource1 = New Infragistics.Win.UltraWinDataSource.UltraDataSource(Me.components)
        Me.UltraGridExcelExporter1 = New Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter(Me.components)
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.cmbType = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.grdStock, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.UltraDataSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripButton, Me.OpenToolStripButton, Me.SaveToolStripButton, Me.DeleteToolStripButton, Me.PrintToolStripButton, Me.toolStripSeparator1, Me.HelpToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(814, 32)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'NewToolStripButton
        '
        Me.NewToolStripButton.Image = CType(resources.GetObject("NewToolStripButton.Image"), System.Drawing.Image)
        Me.NewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.NewToolStripButton.Name = "NewToolStripButton"
        Me.NewToolStripButton.Size = New System.Drawing.Size(75, 29)
        Me.NewToolStripButton.Text = "&New"
        Me.NewToolStripButton.Visible = False
        '
        'OpenToolStripButton
        '
        Me.OpenToolStripButton.Image = CType(resources.GetObject("OpenToolStripButton.Image"), System.Drawing.Image)
        Me.OpenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.OpenToolStripButton.Name = "OpenToolStripButton"
        Me.OpenToolStripButton.Size = New System.Drawing.Size(70, 29)
        Me.OpenToolStripButton.Text = "&Edit"
        Me.OpenToolStripButton.Visible = False
        '
        'SaveToolStripButton
        '
        Me.SaveToolStripButton.Image = CType(resources.GetObject("SaveToolStripButton.Image"), System.Drawing.Image)
        Me.SaveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.SaveToolStripButton.Name = "SaveToolStripButton"
        Me.SaveToolStripButton.Size = New System.Drawing.Size(77, 29)
        Me.SaveToolStripButton.Text = "&Save"
        Me.SaveToolStripButton.Visible = False
        '
        'DeleteToolStripButton
        '
        Me.DeleteToolStripButton.Image = CType(resources.GetObject("DeleteToolStripButton.Image"), System.Drawing.Image)
        Me.DeleteToolStripButton.Name = "DeleteToolStripButton"
        Me.DeleteToolStripButton.RightToLeftAutoMirrorImage = True
        Me.DeleteToolStripButton.Size = New System.Drawing.Size(90, 29)
        Me.DeleteToolStripButton.Text = "&Delete"
        Me.DeleteToolStripButton.Visible = False
        '
        'PrintToolStripButton
        '
        Me.PrintToolStripButton.Image = CType(resources.GetObject("PrintToolStripButton.Image"), System.Drawing.Image)
        Me.PrintToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.PrintToolStripButton.Name = "PrintToolStripButton"
        Me.PrintToolStripButton.Size = New System.Drawing.Size(76, 29)
        Me.PrintToolStripButton.Text = "&Print"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 32)
        '
        'HelpToolStripButton
        '
        Me.HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HelpToolStripButton.Image = CType(resources.GetObject("HelpToolStripButton.Image"), System.Drawing.Image)
        Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton.Name = "HelpToolStripButton"
        Me.HelpToolStripButton.Size = New System.Drawing.Size(28, 29)
        Me.HelpToolStripButton.Text = "He&lp"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 21)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(87, 20)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Sales Type"
        '
        'grdStock
        '
        Me.grdStock.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdStock.ContextMenuStrip = Me.ContextMenuStrip1
        Appearance1.BackColor = System.Drawing.SystemColors.Window
        Appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption
        Me.grdStock.DisplayLayout.Appearance = Appearance1
        Me.grdStock.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
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
        Appearance5.BackColor = System.Drawing.SystemColors.Window
        Appearance5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.grdStock.DisplayLayout.Override.ActiveCellAppearance = Appearance5
        Appearance6.BackColor = System.Drawing.SystemColors.Highlight
        Appearance6.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.grdStock.DisplayLayout.Override.ActiveRowAppearance = Appearance6
        Me.grdStock.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.grdStock.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.grdStock.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.[True]
        Me.grdStock.DisplayLayout.Override.AllowRowSummaries = Infragistics.Win.UltraWinGrid.AllowRowSummaries.[True]
        Me.grdStock.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.grdStock.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.grdStock.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance7.BackColor = System.Drawing.SystemColors.Window
        Me.grdStock.DisplayLayout.Override.CardAreaAppearance = Appearance7
        Appearance8.BorderColor = System.Drawing.Color.Silver
        Appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.grdStock.DisplayLayout.Override.CellAppearance = Appearance8
        Me.grdStock.DisplayLayout.Override.CellPadding = 0
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
        Appearance11.BackColor = System.Drawing.SystemColors.Window
        Appearance11.BorderColor = System.Drawing.Color.Silver
        Me.grdStock.DisplayLayout.Override.RowAppearance = Appearance11
        Appearance12.BackColor = System.Drawing.SystemColors.ControlLight
        Me.grdStock.DisplayLayout.Override.TemplateAddRowAppearance = Appearance12
        Me.grdStock.DisplayLayout.UseFixedHeaders = True
        Me.grdStock.Location = New System.Drawing.Point(2, 188)
        Me.grdStock.Name = "grdStock"
        Me.grdStock.Size = New System.Drawing.Size(810, 276)
        Me.grdStock.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.grdStock, "Category Wise Sales Detail")
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.RefreshDataToolStripMenuItem, Me.ExportToExcelToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(201, 64)
        '
        'RefreshDataToolStripMenuItem
        '
        Me.RefreshDataToolStripMenuItem.Name = "RefreshDataToolStripMenuItem"
        Me.RefreshDataToolStripMenuItem.Size = New System.Drawing.Size(200, 30)
        Me.RefreshDataToolStripMenuItem.Text = "Refresh Data"
        '
        'ExportToExcelToolStripMenuItem
        '
        Me.ExportToExcelToolStripMenuItem.Name = "ExportToExcelToolStripMenuItem"
        Me.ExportToExcelToolStripMenuItem.Size = New System.Drawing.Size(200, 30)
        Me.ExportToExcelToolStripMenuItem.Text = "Export to Excel"
        '
        'UltraDataSource1
        '
        Me.UltraDataSource1.Band.Columns.AddRange(New Object() {UltraDataColumn1, UltraDataColumn2, UltraDataColumn3, UltraDataColumn4, UltraDataColumn5, UltraDataColumn6, UltraDataColumn7, UltraDataColumn8, UltraDataColumn9, UltraDataColumn10, UltraDataColumn11})
        '
        'cmbType
        '
        Me.cmbType.FormattingEnabled = True
        Me.cmbType.Items.AddRange(New Object() {"Pair", "Value"})
        Me.cmbType.Location = New System.Drawing.Point(75, 18)
        Me.cmbType.Name = "cmbType"
        Me.cmbType.Size = New System.Drawing.Size(118, 28)
        Me.cmbType.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbType, "Select Sales Type")
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(110, 62)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(83, 25)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "&Generate "
        Me.ToolTip1.SetToolTip(Me.Button1, "Generate Report")
        Me.Button1.UseVisualStyleBackColor = True
        '
        'lblHeader
        '
        Me.lblHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(12, 10)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(213, 36)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Category Wise"
        '
        'cmbPeriod
        '
        Me.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPeriod.FormattingEnabled = True
        Me.cmbPeriod.Items.AddRange(New Object() {"Today", "Yesterday", "Current Week", "Current Month", "Current Year"})
        Me.cmbPeriod.Location = New System.Drawing.Point(77, 19)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(139, 28)
        Me.cmbPeriod.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbPeriod, "Select Period And Gets Date Range")
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(77, 46)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(139, 26)
        Me.dtpFrom.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.dtpFrom, "From Date")
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd/MMM/yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(77, 71)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(139, 26)
        Me.dtpTo.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.dtpTo, "To Date")
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 20)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "&From"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 74)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 20)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "To"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(10, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(54, 20)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Period"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.cmbType)
        Me.GroupBox1.Controls.Add(Me.Button1)
        Me.GroupBox1.Location = New System.Drawing.Point(243, 75)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(209, 102)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.dtpTo)
        Me.GroupBox2.Controls.Add(Me.cmbPeriod)
        Me.GroupBox2.Controls.Add(Me.dtpFrom)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 75)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(225, 102)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Date Range"
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 32)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(814, 42)
        Me.pnlHeader.TabIndex = 83
        '
        'rptCategoryWiseSaleReport
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(814, 464)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.grdStock)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "rptCategoryWiseSaleReport"
        Me.Text = "Category Wise Report"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.grdStock, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.UltraDataSource1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents NewToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents OpenToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SaveToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents PrintToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DeleteToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents grdStock As Infragistics.Win.UltraWinGrid.UltraGrid
    Friend WithEvents UltraDataSource1 As Infragistics.Win.UltraWinDataSource.UltraDataSource
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents RefreshDataToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExportToExcelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UltraGridExcelExporter1 As Infragistics.Win.UltraWinGrid.ExcelExport.UltraGridExcelExporter
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents cmbType As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

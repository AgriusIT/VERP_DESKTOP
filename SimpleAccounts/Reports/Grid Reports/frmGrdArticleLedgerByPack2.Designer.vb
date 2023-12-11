<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGrdArticleLedgerByPack2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGrdArticleLedgerByPack2))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX()
        Me.lblArticleLedger = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnGenerate = New System.Windows.Forms.Button()
        Me.rbtnByCode = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.rbtnByName = New System.Windows.Forms.RadioButton()
        Me.cmbLocation = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblArticle = New System.Windows.Forms.Label()
        Me.cmbItems = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolStrip1.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.cmbItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(924, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(66, 22)
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
        Me.GridEX1.Location = New System.Drawing.Point(1, 196)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.Size = New System.Drawing.Size(923, 394)
        Me.GridEX1.TabIndex = 4
        Me.GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'lblArticleLedger
        '
        Me.lblArticleLedger.AutoSize = True
        Me.lblArticleLedger.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblArticleLedger.ForeColor = System.Drawing.Color.Navy
        Me.lblArticleLedger.Location = New System.Drawing.Point(9, 35)
        Me.lblArticleLedger.Name = "lblArticleLedger"
        Me.lblArticleLedger.Size = New System.Drawing.Size(406, 23)
        Me.lblArticleLedger.TabIndex = 2
        Me.lblArticleLedger.Text = "Transactional Article Ledger By Pack"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.btnGenerate)
        Me.GroupBox1.Controls.Add(Me.rbtnByCode)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.rbtnByName)
        Me.GroupBox1.Controls.Add(Me.cmbLocation)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.lblArticle)
        Me.GroupBox1.Controls.Add(Me.cmbItems)
        Me.GroupBox1.Controls.Add(Me.cmbPeriod)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.dtpTo)
        Me.GroupBox1.Controls.Add(Me.dtpFrom)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 67)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(910, 123)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        '
        'btnGenerate
        '
        Me.btnGenerate.BackColor = System.Drawing.Color.Transparent
        Me.btnGenerate.Location = New System.Drawing.Point(534, 91)
        Me.btnGenerate.Name = "btnGenerate"
        Me.btnGenerate.Size = New System.Drawing.Size(75, 23)
        Me.btnGenerate.TabIndex = 12
        Me.btnGenerate.Text = "Generate"
        Me.ToolTip1.SetToolTip(Me.btnGenerate, "This button displays data.")
        Me.btnGenerate.UseVisualStyleBackColor = False
        '
        'rbtnByCode
        '
        Me.rbtnByCode.AutoSize = True
        Me.rbtnByCode.Checked = True
        Me.rbtnByCode.Location = New System.Drawing.Point(383, 43)
        Me.rbtnByCode.Name = "rbtnByCode"
        Me.rbtnByCode.Size = New System.Drawing.Size(65, 17)
        Me.rbtnByCode.TabIndex = 8
        Me.rbtnByCode.TabStop = True
        Me.rbtnByCode.Text = "By Code"
        Me.ToolTip1.SetToolTip(Me.rbtnByCode, "Filter item by code ")
        Me.rbtnByCode.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(331, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Location"
        '
        'rbtnByName
        '
        Me.rbtnByName.AutoSize = True
        Me.rbtnByName.Location = New System.Drawing.Point(454, 43)
        Me.rbtnByName.Name = "rbtnByName"
        Me.rbtnByName.Size = New System.Drawing.Size(68, 17)
        Me.rbtnByName.TabIndex = 9
        Me.rbtnByName.Text = "By Name"
        Me.ToolTip1.SetToolTip(Me.rbtnByName, "Filter item by name")
        Me.rbtnByName.UseVisualStyleBackColor = True
        '
        'cmbLocation
        '
        Me.cmbLocation.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbLocation.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbLocation.FormattingEnabled = True
        Me.cmbLocation.Location = New System.Drawing.Point(382, 17)
        Me.cmbLocation.Name = "cmbLocation"
        Me.cmbLocation.Size = New System.Drawing.Size(227, 21)
        Me.cmbLocation.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.cmbLocation, "Please select a location to filter data by it.")
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(30, 20)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(37, 13)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "Period"
        '
        'lblArticle
        '
        Me.lblArticle.AutoSize = True
        Me.lblArticle.Location = New System.Drawing.Point(343, 67)
        Me.lblArticle.Name = "lblArticle"
        Me.lblArticle.Size = New System.Drawing.Size(36, 13)
        Me.lblArticle.TabIndex = 10
        Me.lblArticle.Text = "Article"
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
        Me.cmbItems.Location = New System.Drawing.Point(382, 63)
        Me.cmbItems.Name = "cmbItems"
        Me.cmbItems.Size = New System.Drawing.Size(227, 22)
        Me.cmbItems.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.cmbItems, "An article can be selected from here.")
        '
        'cmbPeriod
        '
        Me.cmbPeriod.FormattingEnabled = True
        Me.cmbPeriod.Items.AddRange(New Object() {"Today", "Yesterday", "Current Week", "Current Month", "Current Year"})
        Me.cmbPeriod.Location = New System.Drawing.Point(70, 17)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(250, 21)
        Me.cmbPeriod.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbPeriod, "User can select period to filter dates.")
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(20, 74)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(46, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Date To"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 46)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 13)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Date From"
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd/MMM/yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(70, 71)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(250, 20)
        Me.dtpTo.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.dtpTo, "To date")
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(70, 44)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(250, 20)
        Me.dtpFrom.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.dtpFrom, "From date")
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(886, 0)
        Me.CtrlGrdBar1.MyGrid = Me.GridEX1
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(38, 25)
        Me.CtrlGrdBar1.TabIndex = 1
        '
        'frmGrdArticleLedgerByPack2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(925, 594)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.lblArticleLedger)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmGrdArticleLedgerByPack2"
        Me.Text = "Transactional Article Ledger By Pack"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.cmbItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblArticleLedger As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnGenerate As System.Windows.Forms.Button
    Friend WithEvents lblArticle As System.Windows.Forms.Label
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbLocation As System.Windows.Forms.ComboBox
    Friend WithEvents rbtnByName As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnByCode As System.Windows.Forms.RadioButton
    Friend WithEvents cmbItems As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

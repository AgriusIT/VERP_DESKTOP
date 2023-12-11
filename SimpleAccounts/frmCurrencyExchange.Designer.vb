<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCurrencyExchange
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
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance15 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance16 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance17 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance18 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance19 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance20 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance21 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance22 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance23 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance24 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCurrencyExchange))
        Dim grdAdvanceType_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.cmbCustomer = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.lblCustomer = New System.Windows.Forms.Label()
        Me.cmbUser = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.lblUser = New System.Windows.Forms.Label()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.grdAdvanceType = New Janus.Windows.GridEX.GridEX()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.cmbto = New System.Windows.Forms.ComboBox()
        Me.cmbfrom = New System.Windows.Forms.ComboBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        CType(Me.cmbCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbUser, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdAdvanceType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.cmbfrom)
        Me.Panel1.Controls.Add(Me.cmbto)
        Me.Panel1.Controls.Add(Me.txtAmount)
        Me.Panel1.Controls.Add(Me.lblProgress)
        Me.Panel1.Controls.Add(Me.cmbCustomer)
        Me.Panel1.Controls.Add(Me.lblCustomer)
        Me.Panel1.Controls.Add(Me.cmbUser)
        Me.Panel1.Controls.Add(Me.lblUser)
        Me.Panel1.Controls.Add(Me.btnExport)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btnAdd)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1344, 208)
        Me.Panel1.TabIndex = 18
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(484, 59)
        Me.lblProgress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(350, 55)
        Me.lblProgress.TabIndex = 105
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'cmbCustomer
        '
        Me.cmbCustomer.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append
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
        Me.cmbCustomer.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbCustomer.Location = New System.Drawing.Point(420, 139)
        Me.cmbCustomer.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbCustomer.Name = "cmbCustomer"
        Me.cmbCustomer.Size = New System.Drawing.Size(389, 32)
        Me.cmbCustomer.TabIndex = 45
        '
        'lblCustomer
        '
        Me.lblCustomer.AutoSize = True
        Me.lblCustomer.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomer.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblCustomer.Location = New System.Drawing.Point(416, 114)
        Me.lblCustomer.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(84, 23)
        Me.lblCustomer.TabIndex = 44
        Me.lblCustomer.Text = "Customer"
        '
        'cmbUser
        '
        Me.cmbUser.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append
        Me.cmbUser.CheckedListSettings.CheckStateMember = ""
        Appearance13.BackColor = System.Drawing.SystemColors.Window
        Appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption
        Me.cmbUser.DisplayLayout.Appearance = Appearance13
        Me.cmbUser.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.cmbUser.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder
        Appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance14.BorderColor = System.Drawing.SystemColors.Window
        Me.cmbUser.DisplayLayout.GroupByBox.Appearance = Appearance14
        Appearance15.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cmbUser.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance15
        Me.cmbUser.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance16.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance16.BackColor2 = System.Drawing.SystemColors.Control
        Appearance16.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance16.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cmbUser.DisplayLayout.GroupByBox.PromptAppearance = Appearance16
        Me.cmbUser.DisplayLayout.MaxColScrollRegions = 1
        Me.cmbUser.DisplayLayout.MaxRowScrollRegions = 1
        Appearance17.BackColor = System.Drawing.SystemColors.Window
        Appearance17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmbUser.DisplayLayout.Override.ActiveCellAppearance = Appearance17
        Appearance18.BackColor = System.Drawing.SystemColors.Highlight
        Appearance18.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.cmbUser.DisplayLayout.Override.ActiveRowAppearance = Appearance18
        Me.cmbUser.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.cmbUser.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance19.BackColor = System.Drawing.SystemColors.Window
        Me.cmbUser.DisplayLayout.Override.CardAreaAppearance = Appearance19
        Appearance20.BorderColor = System.Drawing.Color.Silver
        Appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.cmbUser.DisplayLayout.Override.CellAppearance = Appearance20
        Me.cmbUser.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.cmbUser.DisplayLayout.Override.CellPadding = 0
        Appearance21.BackColor = System.Drawing.SystemColors.Control
        Appearance21.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance21.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance21.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance21.BorderColor = System.Drawing.SystemColors.Window
        Me.cmbUser.DisplayLayout.Override.GroupByRowAppearance = Appearance21
        Appearance22.TextHAlignAsString = "Left"
        Me.cmbUser.DisplayLayout.Override.HeaderAppearance = Appearance22
        Me.cmbUser.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.cmbUser.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance23.BackColor = System.Drawing.SystemColors.Window
        Appearance23.BorderColor = System.Drawing.Color.Silver
        Me.cmbUser.DisplayLayout.Override.RowAppearance = Appearance23
        Me.cmbUser.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance24.BackColor = System.Drawing.SystemColors.ControlLight
        Me.cmbUser.DisplayLayout.Override.TemplateAddRowAppearance = Appearance24
        Me.cmbUser.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbUser.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbUser.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.cmbUser.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbUser.Location = New System.Drawing.Point(23, 139)
        Me.cmbUser.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbUser.Name = "cmbUser"
        Me.cmbUser.Size = New System.Drawing.Size(389, 32)
        Me.cmbUser.TabIndex = 43
        '
        'lblUser
        '
        Me.lblUser.AutoSize = True
        Me.lblUser.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUser.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.lblUser.Location = New System.Drawing.Point(19, 112)
        Me.lblUser.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Size = New System.Drawing.Size(44, 23)
        Me.lblUser.TabIndex = 42
        Me.lblUser.Text = "User"
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExport.Location = New System.Drawing.Point(1288, 30)
        Me.btnExport.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(40, 37)
        Me.btnExport.TabIndex = 17
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(30, 30)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(257, 31)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "User Wise Customer"
        '
        'btnAdd
        '
        Me.btnAdd.BackColor = System.Drawing.Color.Transparent
        Me.btnAdd.BackgroundImage = CType(resources.GetObject("btnAdd.BackgroundImage"), System.Drawing.Image)
        Me.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdd.Location = New System.Drawing.Point(836, 144)
        Me.btnAdd.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(27, 25)
        Me.btnAdd.TabIndex = 14
        Me.btnAdd.UseVisualStyleBackColor = False
        '
        'grdAdvanceType
        '
        Me.grdAdvanceType.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdAdvanceType.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.grdAdvanceType.BlendColor = System.Drawing.Color.White
        Me.grdAdvanceType.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat
        Me.grdAdvanceType.ControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Flat
        Me.grdAdvanceType.ControlStyle.ControlColor = System.Drawing.Color.White
        Me.grdAdvanceType.ControlStyle.ControlTextColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdAdvanceType.ControlStyle.WindowColor = System.Drawing.Color.White
        Me.grdAdvanceType.DefaultBackColorAlphaMode = Janus.Windows.GridEX.AlphaMode.Transparent
        grdAdvanceType_DesignTimeLayout.LayoutString = resources.GetString("grdAdvanceType_DesignTimeLayout.LayoutString")
        Me.grdAdvanceType.DesignTimeLayout = grdAdvanceType_DesignTimeLayout
        Me.grdAdvanceType.Dock = System.Windows.Forms.DockStyle.Top
        Me.grdAdvanceType.FlatBorderColor = System.Drawing.Color.White
        Me.grdAdvanceType.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.grdAdvanceType.GridLineColor = System.Drawing.Color.White
        Me.grdAdvanceType.GridLines = Janus.Windows.GridEX.GridLines.Horizontal
        Me.grdAdvanceType.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.grdAdvanceType.GroupByBoxVisible = False
        Me.grdAdvanceType.HeaderFormatStyle.Alpha = 1
        Me.grdAdvanceType.HeaderFormatStyle.Appearance = Janus.Windows.GridEX.Appearance.Empty
        Me.grdAdvanceType.HeaderFormatStyle.BackColor = System.Drawing.Color.White
        Me.grdAdvanceType.HeaderFormatStyle.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.grdAdvanceType.HeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.[False]
        Me.grdAdvanceType.HeaderFormatStyle.FontSize = 10.0!
        Me.grdAdvanceType.HeaderFormatStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdAdvanceType.HeaderFormatStyle.ImageHorizontalAlignment = Janus.Windows.GridEX.ImageHorizontalAlignment.Center
        Me.grdAdvanceType.HeaderFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.grdAdvanceType.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.grdAdvanceType.Location = New System.Drawing.Point(0, 208)
        Me.grdAdvanceType.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grdAdvanceType.Name = "grdAdvanceType"
        Me.grdAdvanceType.Office2007CustomColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.grdAdvanceType.PreviewRowFormatStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdAdvanceType.RecordNavigator = True
        Me.grdAdvanceType.RowFormatStyle.BackColor = System.Drawing.Color.Black
        Me.grdAdvanceType.RowFormatStyle.BackColorGradient = System.Drawing.Color.White
        Me.grdAdvanceType.RowFormatStyle.BackgroundGradientMode = Janus.Windows.GridEX.BackgroundGradientMode.Solid
        Me.grdAdvanceType.RowFormatStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdAdvanceType.Size = New System.Drawing.Size(1344, 690)
        Me.grdAdvanceType.TabIndex = 20
        Me.grdAdvanceType.ThemedAreas = Janus.Windows.GridEX.ThemedArea.None
        Me.grdAdvanceType.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdAdvanceType.TreeLineColor = System.Drawing.SystemColors.Control
        Me.grdAdvanceType.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrintToolStripMenuItem, Me.ExportToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(122, 52)
        '
        'PrintToolStripMenuItem
        '
        Me.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem"
        Me.PrintToolStripMenuItem.Size = New System.Drawing.Size(121, 24)
        Me.PrintToolStripMenuItem.Text = "Print"
        '
        'ExportToolStripMenuItem
        '
        Me.ExportToolStripMenuItem.Name = "ExportToolStripMenuItem"
        Me.ExportToolStripMenuItem.Size = New System.Drawing.Size(121, 24)
        Me.ExportToolStripMenuItem.Text = "Export"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Panel4.Controls.Add(Me.btnCancel)
        Me.Panel4.Controls.Add(Me.btnSave)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 734)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1344, 106)
        Me.Panel4.TabIndex = 103
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackgroundImage = CType(resources.GetObject("btnCancel.BackgroundImage"), System.Drawing.Image)
        Me.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.Black
        Me.btnCancel.Location = New System.Drawing.Point(1176, 10)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(72, 82)
        Me.btnCancel.TabIndex = 0
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnSave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.Black
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnSave.Location = New System.Drawing.Point(1260, 10)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(72, 82)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "Save"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'txtAmount
        '
        Me.txtAmount.Location = New System.Drawing.Point(904, 113)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(100, 22)
        Me.txtAmount.TabIndex = 106
        '
        'cmbto
        '
        Me.cmbto.FormattingEnabled = True
        Me.cmbto.Items.AddRange(New Object() {"PKR", "AED"})
        Me.cmbto.Location = New System.Drawing.Point(1052, 144)
        Me.cmbto.Name = "cmbto"
        Me.cmbto.Size = New System.Drawing.Size(121, 24)
        Me.cmbto.TabIndex = 107
        '
        'cmbfrom
        '
        Me.cmbfrom.FormattingEnabled = True
        Me.cmbfrom.Items.AddRange(New Object() {"USD"})
        Me.cmbfrom.Location = New System.Drawing.Point(904, 145)
        Me.cmbfrom.Name = "cmbfrom"
        Me.cmbfrom.Size = New System.Drawing.Size(121, 24)
        Me.cmbfrom.TabIndex = 107
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(1179, 144)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 104
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmCurrencyExchange
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1344, 840)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.grdAdvanceType)
        Me.Controls.Add(Me.Panel1)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmCurrencyExchange"
        Me.Text = "frmUserWiseCustomerList"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.cmbCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbUser, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdAdvanceType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents grdAdvanceType As Janus.Windows.GridEX.GridEX
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents PrintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmbCustomer As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents lblCustomer As System.Windows.Forms.Label
    Friend WithEvents cmbUser As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents lblUser As System.Windows.Forms.Label
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents cmbfrom As System.Windows.Forms.ComboBox
    Friend WithEvents cmbto As System.Windows.Forms.ComboBox
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
End Class

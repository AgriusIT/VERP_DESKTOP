<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmailConfiguration
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
        Dim grdEmailTemplate_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmailConfiguration))
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblDropFields = New System.Windows.Forms.Label()
        Me.lblDragFields = New System.Windows.Forms.Label()
        Me.lblTemplateTitle = New System.Windows.Forms.Label()
        Me.Editor1 = New SimpleAccounts.Editor()
        Me.cmbTemplateTitle = New System.Windows.Forms.ComboBox()
        Me.lstColumns = New System.Windows.Forms.ListBox()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.grdEmailTemplate = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.BtnNew = New System.Windows.Forms.ToolStripButton()
        Me.BtnEdit = New System.Windows.Forms.ToolStripButton()
        Me.BtnSave = New System.Windows.Forms.ToolStripButton()
        Me.BtnDelete = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.btnLoadFields = New System.Windows.Forms.ToolStripButton()
        Me.txtHtmlTemplate = New System.Windows.Forms.TextBox()
        Me.lblHtmlTemplate = New System.Windows.Forms.Label()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlPreview = New System.Windows.Forms.Panel()
        Me.lblPreview = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.Button10 = New System.Windows.Forms.Button()
        Me.Button11 = New System.Windows.Forms.Button()
        Me.Button12 = New System.Windows.Forms.Button()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        CType(Me.grdEmailTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        Me.pnlPreview.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.Panel1)
        Me.UltraTabPageControl1.Controls.Add(Me.Editor1)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbTemplateTitle)
        Me.UltraTabPageControl1.Controls.Add(Me.lstColumns)
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.btnAdd)
        Me.UltraTabPageControl1.Controls.Add(Me.grdEmailTemplate)
        Me.UltraTabPageControl1.Controls.Add(Me.ToolStrip1)
        Me.UltraTabPageControl1.Controls.Add(Me.txtHtmlTemplate)
        Me.UltraTabPageControl1.Controls.Add(Me.lblHtmlTemplate)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1211, 479)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblDropFields)
        Me.Panel1.Controls.Add(Me.lblDragFields)
        Me.Panel1.Controls.Add(Me.lblTemplateTitle)
        Me.Panel1.Location = New System.Drawing.Point(653, 92)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(552, 28)
        Me.Panel1.TabIndex = 3
        '
        'lblDropFields
        '
        Me.lblDropFields.AutoSize = True
        Me.lblDropFields.Location = New System.Drawing.Point(315, 6)
        Me.lblDropFields.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDropFields.Name = "lblDropFields"
        Me.lblDropFields.Size = New System.Drawing.Size(80, 17)
        Me.lblDropFields.TabIndex = 2
        Me.lblDropFields.Text = "Drop Fields"
        '
        'lblDragFields
        '
        Me.lblDragFields.AutoSize = True
        Me.lblDragFields.Location = New System.Drawing.Point(139, 6)
        Me.lblDragFields.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDragFields.Name = "lblDragFields"
        Me.lblDragFields.Size = New System.Drawing.Size(80, 17)
        Me.lblDragFields.TabIndex = 1
        Me.lblDragFields.Text = "Drag Fields"
        '
        'lblTemplateTitle
        '
        Me.lblTemplateTitle.AutoSize = True
        Me.lblTemplateTitle.Location = New System.Drawing.Point(1, 6)
        Me.lblTemplateTitle.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTemplateTitle.Name = "lblTemplateTitle"
        Me.lblTemplateTitle.Size = New System.Drawing.Size(98, 17)
        Me.lblTemplateTitle.TabIndex = 0
        Me.lblTemplateTitle.Text = "Template Title"
        '
        'Editor1
        '
        Me.Editor1._FontSize = SimpleAccounts.Editor.FontSize.Three
        Me.Editor1.BodyBackgroundColor = System.Drawing.Color.White
        Me.Editor1.BodyHtml = Nothing
        Me.Editor1.EditorBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.Editor1.EditorForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Editor1.Location = New System.Drawing.Point(4, 91)
        Me.Editor1.Margin = New System.Windows.Forms.Padding(5)
        Me.Editor1.Name = "Editor1"
        Me.Editor1.Size = New System.Drawing.Size(641, 249)
        Me.Editor1.TabIndex = 2
        '
        'cmbTemplateTitle
        '
        Me.cmbTemplateTitle.FormattingEnabled = True
        Me.cmbTemplateTitle.Items.AddRange(New Object() {"Purchase Inquiry", "Sales Quotation", "Sales Inquiry", "Approval Log", "Purchase Order", "Sales Order", "Purchase Return", "Sales Return", "Voucher Entry", "Payment", "Receipt", "Cash Request", "Employee Request", "Birthday", "Purchase Demand"})
        Me.cmbTemplateTitle.Location = New System.Drawing.Point(653, 127)
        Me.cmbTemplateTitle.Margin = New System.Windows.Forms.Padding(4)
        Me.cmbTemplateTitle.Name = "cmbTemplateTitle"
        Me.cmbTemplateTitle.Size = New System.Drawing.Size(128, 24)
        Me.cmbTemplateTitle.TabIndex = 4
        '
        'lstColumns
        '
        Me.lstColumns.AllowDrop = True
        Me.lstColumns.FormattingEnabled = True
        Me.lstColumns.ItemHeight = 16
        Me.lstColumns.Location = New System.Drawing.Point(796, 127)
        Me.lstColumns.Margin = New System.Windows.Forms.Padding(4)
        Me.lstColumns.Name = "lstColumns"
        Me.lstColumns.Size = New System.Drawing.Size(167, 180)
        Me.lstColumns.TabIndex = 5
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.pnlHeader.Location = New System.Drawing.Point(0, 27)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1211, 57)
        Me.pnlHeader.TabIndex = 1
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.White
        Me.lblHeader.Location = New System.Drawing.Point(24, 14)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(244, 32)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Email Configuration"
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(1152, 314)
        Me.btnAdd.Margin = New System.Windows.Forms.Padding(4)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(53, 28)
        Me.btnAdd.TabIndex = 1
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'grdEmailTemplate
        '
        Me.grdEmailTemplate.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdEmailTemplate.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdEmailTemplate_DesignTimeLayout.LayoutString = resources.GetString("grdEmailTemplate_DesignTimeLayout.LayoutString")
        Me.grdEmailTemplate.DesignTimeLayout = grdEmailTemplate_DesignTimeLayout
        Me.grdEmailTemplate.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdEmailTemplate.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdEmailTemplate.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdEmailTemplate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdEmailTemplate.GridLines = Janus.Windows.GridEX.GridLines.[Default]
        Me.grdEmailTemplate.GroupByBoxVisible = False
        Me.grdEmailTemplate.Location = New System.Drawing.Point(3, 350)
        Me.grdEmailTemplate.Margin = New System.Windows.Forms.Padding(4)
        Me.grdEmailTemplate.Name = "grdEmailTemplate"
        Me.grdEmailTemplate.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndMoveToFirstCellInNewRow
        Me.grdEmailTemplate.RecordNavigator = True
        Me.grdEmailTemplate.ScrollBars = Janus.Windows.GridEX.ScrollBars.Horizontal
        Me.grdEmailTemplate.Size = New System.Drawing.Size(1205, 127)
        Me.grdEmailTemplate.TabIndex = 0
        Me.grdEmailTemplate.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdEmailTemplate.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BtnNew, Me.BtnEdit, Me.BtnSave, Me.BtnDelete, Me.toolStripSeparator1, Me.HelpToolStripButton, Me.btnLoadFields})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1211, 27)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'BtnNew
        '
        Me.BtnNew.Image = CType(resources.GetObject("BtnNew.Image"), System.Drawing.Image)
        Me.BtnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnNew.Name = "BtnNew"
        Me.BtnNew.Size = New System.Drawing.Size(63, 24)
        Me.BtnNew.Text = "&New"
        '
        'BtnEdit
        '
        Me.BtnEdit.Image = CType(resources.GetObject("BtnEdit.Image"), System.Drawing.Image)
        Me.BtnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnEdit.Name = "BtnEdit"
        Me.BtnEdit.Size = New System.Drawing.Size(59, 24)
        Me.BtnEdit.Text = "&Edit"
        '
        'BtnSave
        '
        Me.BtnSave.Image = CType(resources.GetObject("BtnSave.Image"), System.Drawing.Image)
        Me.BtnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(64, 24)
        Me.BtnSave.Text = "&Save"
        '
        'BtnDelete
        '
        Me.BtnDelete.Image = CType(resources.GetObject("BtnDelete.Image"), System.Drawing.Image)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.RightToLeftAutoMirrorImage = True
        Me.BtnDelete.Size = New System.Drawing.Size(77, 24)
        Me.BtnDelete.Text = "&Delete"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 27)
        '
        'HelpToolStripButton
        '
        Me.HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HelpToolStripButton.Image = CType(resources.GetObject("HelpToolStripButton.Image"), System.Drawing.Image)
        Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton.Name = "HelpToolStripButton"
        Me.HelpToolStripButton.Size = New System.Drawing.Size(24, 24)
        Me.HelpToolStripButton.Text = "He&lp"
        '
        'btnLoadFields
        '
        Me.btnLoadFields.Enabled = False
        Me.btnLoadFields.Image = Global.SimpleAccounts.My.Resources.Resources.Copy
        Me.btnLoadFields.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnLoadFields.Name = "btnLoadFields"
        Me.btnLoadFields.Size = New System.Drawing.Size(191, 24)
        Me.btnLoadFields.Text = "Load Required Columns"
        '
        'txtHtmlTemplate
        '
        Me.txtHtmlTemplate.AllowDrop = True
        Me.txtHtmlTemplate.Location = New System.Drawing.Point(972, 127)
        Me.txtHtmlTemplate.Margin = New System.Windows.Forms.Padding(4)
        Me.txtHtmlTemplate.Multiline = True
        Me.txtHtmlTemplate.Name = "txtHtmlTemplate"
        Me.txtHtmlTemplate.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtHtmlTemplate.Size = New System.Drawing.Size(232, 181)
        Me.txtHtmlTemplate.TabIndex = 6
        '
        'lblHtmlTemplate
        '
        Me.lblHtmlTemplate.Location = New System.Drawing.Point(217, 350)
        Me.lblHtmlTemplate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHtmlTemplate.Name = "lblHtmlTemplate"
        Me.lblHtmlTemplate.Size = New System.Drawing.Size(752, 30)
        Me.lblHtmlTemplate.TabIndex = 9
        Me.lblHtmlTemplate.Text = "Html Template"
        Me.lblHtmlTemplate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.pnlPreview)
        Me.UltraTabPageControl2.Controls.Add(Me.WebBrowser1)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Margin = New System.Windows.Forms.Padding(4)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1211, 479)
        '
        'pnlPreview
        '
        Me.pnlPreview.BackColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlPreview.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.pnlPreview.Controls.Add(Me.lblPreview)
        Me.pnlPreview.Controls.Add(Me.Button2)
        Me.pnlPreview.Controls.Add(Me.Button8)
        Me.pnlPreview.Controls.Add(Me.Button9)
        Me.pnlPreview.Controls.Add(Me.Button10)
        Me.pnlPreview.Controls.Add(Me.Button11)
        Me.pnlPreview.Controls.Add(Me.Button12)
        Me.pnlPreview.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlPreview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.pnlPreview.Location = New System.Drawing.Point(0, 0)
        Me.pnlPreview.Margin = New System.Windows.Forms.Padding(4)
        Me.pnlPreview.Name = "pnlPreview"
        Me.pnlPreview.Size = New System.Drawing.Size(1211, 42)
        Me.pnlPreview.TabIndex = 4
        '
        'lblPreview
        '
        Me.lblPreview.AutoSize = True
        Me.lblPreview.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPreview.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.lblPreview.Location = New System.Drawing.Point(481, 6)
        Me.lblPreview.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPreview.Name = "lblPreview"
        Me.lblPreview.Size = New System.Drawing.Size(123, 29)
        Me.lblPreview.TabIndex = 0
        Me.lblPreview.Text = "Preview"
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackgroundImage = CType(resources.GetObject("Button2.BackgroundImage"), System.Drawing.Image)
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button2.FlatAppearance.BorderSize = 0
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Location = New System.Drawing.Point(999, 10)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(29, 26)
        Me.Button2.TabIndex = 1
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'Button8
        '
        Me.Button8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button8.BackgroundImage = CType(resources.GetObject("Button8.BackgroundImage"), System.Drawing.Image)
        Me.Button8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button8.FlatAppearance.BorderSize = 0
        Me.Button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button8.Location = New System.Drawing.Point(1031, 7)
        Me.Button8.Margin = New System.Windows.Forms.Padding(4)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(35, 30)
        Me.Button8.TabIndex = 2
        Me.Button8.UseVisualStyleBackColor = True
        Me.Button8.Visible = False
        '
        'Button9
        '
        Me.Button9.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button9.BackgroundImage = CType(resources.GetObject("Button9.BackgroundImage"), System.Drawing.Image)
        Me.Button9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button9.FlatAppearance.BorderSize = 0
        Me.Button9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button9.Location = New System.Drawing.Point(1066, 7)
        Me.Button9.Margin = New System.Windows.Forms.Padding(4)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(33, 32)
        Me.Button9.TabIndex = 3
        Me.Button9.UseVisualStyleBackColor = True
        Me.Button9.Visible = False
        '
        'Button10
        '
        Me.Button10.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button10.BackgroundImage = CType(resources.GetObject("Button10.BackgroundImage"), System.Drawing.Image)
        Me.Button10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button10.FlatAppearance.BorderSize = 0
        Me.Button10.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button10.Location = New System.Drawing.Point(1098, 7)
        Me.Button10.Margin = New System.Windows.Forms.Padding(4)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(29, 31)
        Me.Button10.TabIndex = 4
        Me.Button10.UseVisualStyleBackColor = True
        Me.Button10.Visible = False
        '
        'Button11
        '
        Me.Button11.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button11.BackgroundImage = CType(resources.GetObject("Button11.BackgroundImage"), System.Drawing.Image)
        Me.Button11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button11.FlatAppearance.BorderSize = 0
        Me.Button11.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button11.Location = New System.Drawing.Point(1127, 6)
        Me.Button11.Margin = New System.Windows.Forms.Padding(4)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(33, 33)
        Me.Button11.TabIndex = 5
        Me.Button11.UseVisualStyleBackColor = True
        Me.Button11.Visible = False
        '
        'Button12
        '
        Me.Button12.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button12.BackgroundImage = CType(resources.GetObject("Button12.BackgroundImage"), System.Drawing.Image)
        Me.Button12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Button12.FlatAppearance.BorderSize = 0
        Me.Button12.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button12.Location = New System.Drawing.Point(1163, 2)
        Me.Button12.Margin = New System.Windows.Forms.Padding(4)
        Me.Button12.Name = "Button12"
        Me.Button12.Size = New System.Drawing.Size(29, 37)
        Me.Button12.TabIndex = 6
        Me.Button12.UseVisualStyleBackColor = True
        Me.Button12.Visible = False
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WebBrowser1.Location = New System.Drawing.Point(-1, 41)
        Me.WebBrowser1.Margin = New System.Windows.Forms.Padding(4)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(27, 25)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(1209, 436)
        Me.WebBrowser1.TabIndex = 3
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.UltraTabControl1.Margin = New System.Windows.Forms.Padding(4)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(1215, 506)
        Me.UltraTabControl1.TabIndex = 0
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Email Comfiguration"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "Preview"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Margin = New System.Windows.Forms.Padding(4)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1211, 479)
        '
        'frmEmailConfiguration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1215, 506)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmEmailConfiguration"
        Me.Text = "Email Configuration"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.grdEmailTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        Me.pnlPreview.ResumeLayout(False)
        Me.pnlPreview.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtHtmlTemplate As System.Windows.Forms.TextBox
    Friend WithEvents lblHtmlTemplate As System.Windows.Forms.Label
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents WebBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents BtnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents grdEmailTemplate As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents pnlPreview As System.Windows.Forms.Panel
    Friend WithEvents lblPreview As System.Windows.Forms.Label
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents Button10 As System.Windows.Forms.Button
    Friend WithEvents Button11 As System.Windows.Forms.Button
    Friend WithEvents Button12 As System.Windows.Forms.Button
    Friend WithEvents btnLoadFields As System.Windows.Forms.ToolStripButton
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents lstColumns As System.Windows.Forms.ListBox
    Friend WithEvents cmbTemplateTitle As System.Windows.Forms.ComboBox
    Friend WithEvents Editor1 As SimpleAccounts.Editor
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblTemplateTitle As System.Windows.Forms.Label
    Friend WithEvents lblDropFields As System.Windows.Forms.Label
    Friend WithEvents lblDragFields As System.Windows.Forms.Label
End Class

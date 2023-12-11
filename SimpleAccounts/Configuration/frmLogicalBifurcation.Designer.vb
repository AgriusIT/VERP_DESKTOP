<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLogicalBifurcation
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLogicalBifurcation))
        Dim grdDetail_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn1 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Id")
        Dim UltraGridColumn2 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleDescription")
        Dim UltraGridColumn3 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Size")
        Dim UltraGridColumn4 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Color")
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand2 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn5 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Id")
        Dim UltraGridColumn6 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ArticleDescription")
        Dim UltraGridColumn9 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Size")
        Dim UltraGridColumn10 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Color")
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.BtnPrint = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.grdDetail = New Janus.Windows.GridEX.GridEX()
        Me.txtDocumentNo = New System.Windows.Forms.TextBox()
        Me.lblDocumentNo = New System.Windows.Forms.Label()
        Me.dtpDocumentDate = New System.Windows.Forms.DateTimePicker()
        Me.lblDocumentDate = New System.Windows.Forms.Label()
        Me.cmbFromCostCenter = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.lblFromCostCenter = New System.Windows.Forms.Label()
        Me.lblStartDate = New System.Windows.Forms.Label()
        Me.dtpStartDate = New System.Windows.Forms.DateTimePicker()
        Me.lblRemarks = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.gbDetail = New System.Windows.Forms.GroupBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.lblComments = New System.Windows.Forms.Label()
        Me.txtComments = New System.Windows.Forms.TextBox()
        Me.lblAmount = New System.Windows.Forms.Label()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.lblToCostCenter = New System.Windows.Forms.Label()
        Me.cmbToCostCenter = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.grdDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbFromCostCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbDetail.SuspendLayout()
        CType(Me.cmbToCostCenter, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Location = New System.Drawing.Point(2, 3)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1154, 62)
        Me.Panel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(6, 11)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(282, 36)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Logical Bifurcation"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Teal
        Me.Panel2.Controls.Add(Me.BtnPrint)
        Me.Panel2.Controls.Add(Me.btnCancel)
        Me.Panel2.Controls.Add(Me.btnSave)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 569)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1156, 125)
        Me.Panel2.TabIndex = 24
        '
        'BtnPrint
        '
        Me.BtnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnPrint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.BtnPrint.Cursor = System.Windows.Forms.Cursors.Hand
        Me.BtnPrint.FlatAppearance.BorderSize = 0
        Me.BtnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPrint.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPrint.ForeColor = System.Drawing.Color.White
        Me.BtnPrint.Image = CType(resources.GetObject("BtnPrint.Image"), System.Drawing.Image)
        Me.BtnPrint.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnPrint.Location = New System.Drawing.Point(969, 5)
        Me.BtnPrint.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(81, 108)
        Me.BtnPrint.TabIndex = 2
        Me.BtnPrint.Text = "Print"
        Me.BtnPrint.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.BtnPrint.UseVisualStyleBackColor = False
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackgroundImage = CType(resources.GetObject("btnCancel.BackgroundImage"), System.Drawing.Image)
        Me.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.ForeColor = System.Drawing.Color.White
        Me.btnCancel.Location = New System.Drawing.Point(879, 17)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(81, 95)
        Me.btnCancel.TabIndex = 0
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.BackgroundImage = CType(resources.GetObject("btnSave.BackgroundImage"), System.Drawing.Image)
        Me.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.btnSave.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSave.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSave.ForeColor = System.Drawing.Color.White
        Me.btnSave.Location = New System.Drawing.Point(1059, 17)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(81, 95)
        Me.btnSave.TabIndex = 0
        Me.btnSave.UseVisualStyleBackColor = False
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(267, 403)
        Me.lblProgress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(292, 69)
        Me.lblProgress.TabIndex = 13
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'grdDetail
        '
        Me.grdDetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdDetail_DesignTimeLayout.LayoutString = resources.GetString("grdDetail_DesignTimeLayout.LayoutString")
        Me.grdDetail.DesignTimeLayout = grdDetail_DesignTimeLayout
        Me.grdDetail.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdDetail.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdDetail.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdDetail.GroupByBoxVisible = False
        Me.grdDetail.Location = New System.Drawing.Point(0, 309)
        Me.grdDetail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdDetail.Name = "grdDetail"
        Me.grdDetail.RecordNavigator = True
        Me.grdDetail.Size = New System.Drawing.Size(1155, 254)
        Me.grdDetail.TabIndex = 12
        Me.grdDetail.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdDetail.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdDetail.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'txtDocumentNo
        '
        Me.txtDocumentNo.Location = New System.Drawing.Point(165, 77)
        Me.txtDocumentNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtDocumentNo.Name = "txtDocumentNo"
        Me.txtDocumentNo.ReadOnly = True
        Me.txtDocumentNo.Size = New System.Drawing.Size(283, 26)
        Me.txtDocumentNo.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.txtDocumentNo, "Type document no")
        '
        'lblDocumentNo
        '
        Me.lblDocumentNo.AutoSize = True
        Me.lblDocumentNo.Location = New System.Drawing.Point(45, 82)
        Me.lblDocumentNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDocumentNo.Name = "lblDocumentNo"
        Me.lblDocumentNo.Size = New System.Drawing.Size(111, 20)
        Me.lblDocumentNo.TabIndex = 1
        Me.lblDocumentNo.Text = "Document No:"
        '
        'dtpDocumentDate
        '
        Me.dtpDocumentDate.CustomFormat = "dd/MMM/yyyyy"
        Me.dtpDocumentDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDocumentDate.Location = New System.Drawing.Point(591, 75)
        Me.dtpDocumentDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpDocumentDate.Name = "dtpDocumentDate"
        Me.dtpDocumentDate.Size = New System.Drawing.Size(298, 26)
        Me.dtpDocumentDate.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.dtpDocumentDate, "Select document date")
        '
        'lblDocumentDate
        '
        Me.lblDocumentDate.AutoSize = True
        Me.lblDocumentDate.Location = New System.Drawing.Point(459, 82)
        Me.lblDocumentDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDocumentDate.Name = "lblDocumentDate"
        Me.lblDocumentDate.Size = New System.Drawing.Size(126, 20)
        Me.lblDocumentDate.TabIndex = 3
        Me.lblDocumentDate.Text = "Document Date:"
        '
        'cmbFromCostCenter
        '
        Me.cmbFromCostCenter.AlwaysInEditMode = True
        Me.cmbFromCostCenter.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cmbFromCostCenter.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbFromCostCenter.CheckedListSettings.CheckStateMember = ""
        Appearance1.BackColor = System.Drawing.Color.White
        Appearance1.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        Me.cmbFromCostCenter.DisplayLayout.Appearance = Appearance1
        UltraGridColumn1.Header.VisiblePosition = 0
        UltraGridColumn1.Hidden = True
        UltraGridColumn2.Header.VisiblePosition = 1
        UltraGridColumn3.Header.VisiblePosition = 2
        UltraGridColumn4.Header.VisiblePosition = 3
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn1, UltraGridColumn2, UltraGridColumn3, UltraGridColumn4})
        Me.cmbFromCostCenter.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbFromCostCenter.DisplayLayout.InterBandSpacing = 10
        Me.cmbFromCostCenter.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbFromCostCenter.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbFromCostCenter.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Appearance2.BackColor = System.Drawing.Color.Transparent
        Me.cmbFromCostCenter.DisplayLayout.Override.CardAreaAppearance = Appearance2
        Me.cmbFromCostCenter.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbFromCostCenter.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance3.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance3.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance3.ForeColor = System.Drawing.Color.White
        Appearance3.TextHAlignAsString = "Left"
        Appearance3.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent
        Me.cmbFromCostCenter.DisplayLayout.Override.HeaderAppearance = Appearance3
        Me.cmbFromCostCenter.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance4.BorderColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbFromCostCenter.DisplayLayout.Override.RowAppearance = Appearance4
        Appearance5.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance5.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.cmbFromCostCenter.DisplayLayout.Override.RowSelectorAppearance = Appearance5
        Me.cmbFromCostCenter.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbFromCostCenter.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance6.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance6.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance6.ForeColor = System.Drawing.Color.Black
        Me.cmbFromCostCenter.DisplayLayout.Override.SelectedRowAppearance = Appearance6
        Me.cmbFromCostCenter.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbFromCostCenter.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbFromCostCenter.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbFromCostCenter.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbFromCostCenter.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.cmbFromCostCenter.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbFromCostCenter.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbFromCostCenter.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbFromCostCenter.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbFromCostCenter.EditAreaDisplayStyle = Infragistics.Win.UltraWinGrid.EditAreaDisplayStyle.DisplayText
        Me.cmbFromCostCenter.LimitToList = True
        Me.cmbFromCostCenter.Location = New System.Drawing.Point(164, 117)
        Me.cmbFromCostCenter.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbFromCostCenter.MaxDropDownItems = 20
        Me.cmbFromCostCenter.Name = "cmbFromCostCenter"
        Me.cmbFromCostCenter.Size = New System.Drawing.Size(285, 29)
        Me.cmbFromCostCenter.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.cmbFromCostCenter, "Select From Cost Center")
        '
        'lblFromCostCenter
        '
        Me.lblFromCostCenter.AutoSize = True
        Me.lblFromCostCenter.Location = New System.Drawing.Point(6, 122)
        Me.lblFromCostCenter.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFromCostCenter.Name = "lblFromCostCenter"
        Me.lblFromCostCenter.Size = New System.Drawing.Size(152, 20)
        Me.lblFromCostCenter.TabIndex = 5
        Me.lblFromCostCenter.Text = "Logical Cost Center:"
        '
        'lblStartDate
        '
        Me.lblStartDate.AutoSize = True
        Me.lblStartDate.Location = New System.Drawing.Point(500, 122)
        Me.lblStartDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblStartDate.Name = "lblStartDate"
        Me.lblStartDate.Size = New System.Drawing.Size(87, 20)
        Me.lblStartDate.TabIndex = 7
        Me.lblStartDate.Text = "Start Date:"
        '
        'dtpStartDate
        '
        Me.dtpStartDate.CustomFormat = "dd/MMM/yyyyy"
        Me.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpStartDate.Location = New System.Drawing.Point(591, 115)
        Me.dtpStartDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpStartDate.Name = "dtpStartDate"
        Me.dtpStartDate.Size = New System.Drawing.Size(298, 26)
        Me.dtpStartDate.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.dtpStartDate, "Select a start date")
        '
        'lblRemarks
        '
        Me.lblRemarks.AutoSize = True
        Me.lblRemarks.Location = New System.Drawing.Point(81, 160)
        Me.lblRemarks.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(77, 20)
        Me.lblRemarks.TabIndex = 9
        Me.lblRemarks.Text = "Remarks:"
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(164, 160)
        Me.txtRemarks.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(726, 46)
        Me.txtRemarks.TabIndex = 10
        Me.ToolTip1.SetToolTip(Me.txtRemarks, "Type remarks here")
        '
        'gbDetail
        '
        Me.gbDetail.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gbDetail.Controls.Add(Me.btnAdd)
        Me.gbDetail.Controls.Add(Me.lblComments)
        Me.gbDetail.Controls.Add(Me.txtComments)
        Me.gbDetail.Controls.Add(Me.lblAmount)
        Me.gbDetail.Controls.Add(Me.txtAmount)
        Me.gbDetail.Controls.Add(Me.lblToCostCenter)
        Me.gbDetail.Controls.Add(Me.cmbToCostCenter)
        Me.gbDetail.Location = New System.Drawing.Point(2, 217)
        Me.gbDetail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.gbDetail.Name = "gbDetail"
        Me.gbDetail.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.gbDetail.Size = New System.Drawing.Size(1154, 83)
        Me.gbDetail.TabIndex = 11
        Me.gbDetail.TabStop = False
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(1041, 34)
        Me.btnAdd.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(82, 35)
        Me.btnAdd.TabIndex = 6
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'lblComments
        '
        Me.lblComments.AutoSize = True
        Me.lblComments.Location = New System.Drawing.Point(652, 42)
        Me.lblComments.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblComments.Name = "lblComments"
        Me.lblComments.Size = New System.Drawing.Size(90, 20)
        Me.lblComments.TabIndex = 4
        Me.lblComments.Text = "Comments:"
        '
        'txtComments
        '
        Me.txtComments.Location = New System.Drawing.Point(747, 37)
        Me.txtComments.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtComments.Name = "txtComments"
        Me.txtComments.Size = New System.Drawing.Size(283, 26)
        Me.txtComments.TabIndex = 5
        '
        'lblAmount
        '
        Me.lblAmount.AutoSize = True
        Me.lblAmount.Location = New System.Drawing.Point(462, 42)
        Me.lblAmount.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(83, 20)
        Me.lblAmount.TabIndex = 2
        Me.lblAmount.Text = "Amount%:"
        '
        'txtAmount
        '
        Me.txtAmount.Location = New System.Drawing.Point(546, 37)
        Me.txtAmount.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(100, 26)
        Me.txtAmount.TabIndex = 3
        '
        'lblToCostCenter
        '
        Me.lblToCostCenter.AutoSize = True
        Me.lblToCostCenter.Location = New System.Drawing.Point(60, 40)
        Me.lblToCostCenter.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblToCostCenter.Name = "lblToCostCenter"
        Me.lblToCostCenter.Size = New System.Drawing.Size(98, 20)
        Me.lblToCostCenter.TabIndex = 0
        Me.lblToCostCenter.Text = "Cost Center:"
        '
        'cmbToCostCenter
        '
        Me.cmbToCostCenter.AlwaysInEditMode = True
        Me.cmbToCostCenter.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cmbToCostCenter.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbToCostCenter.CheckedListSettings.CheckStateMember = ""
        Appearance7.BackColor = System.Drawing.Color.White
        Appearance7.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance7.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        Me.cmbToCostCenter.DisplayLayout.Appearance = Appearance7
        UltraGridColumn5.Header.VisiblePosition = 0
        UltraGridColumn5.Hidden = True
        UltraGridColumn6.Header.VisiblePosition = 1
        UltraGridColumn9.Header.VisiblePosition = 2
        UltraGridColumn10.Header.VisiblePosition = 3
        UltraGridBand2.Columns.AddRange(New Object() {UltraGridColumn5, UltraGridColumn6, UltraGridColumn9, UltraGridColumn10})
        Me.cmbToCostCenter.DisplayLayout.BandsSerializer.Add(UltraGridBand2)
        Me.cmbToCostCenter.DisplayLayout.InterBandSpacing = 10
        Me.cmbToCostCenter.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbToCostCenter.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbToCostCenter.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Appearance8.BackColor = System.Drawing.Color.Transparent
        Me.cmbToCostCenter.DisplayLayout.Override.CardAreaAppearance = Appearance8
        Me.cmbToCostCenter.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbToCostCenter.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance9.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance9.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance9.ForeColor = System.Drawing.Color.White
        Appearance9.TextHAlignAsString = "Left"
        Appearance9.ThemedElementAlpha = Infragistics.Win.Alpha.Transparent
        Me.cmbToCostCenter.DisplayLayout.Override.HeaderAppearance = Appearance9
        Me.cmbToCostCenter.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance10.BorderColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbToCostCenter.DisplayLayout.Override.RowAppearance = Appearance10
        Appearance11.BackColor = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance11.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Appearance11.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Me.cmbToCostCenter.DisplayLayout.Override.RowSelectorAppearance = Appearance11
        Me.cmbToCostCenter.DisplayLayout.Override.RowSelectorWidth = 12
        Me.cmbToCostCenter.DisplayLayout.Override.RowSpacingBefore = 2
        Appearance12.BackColor = System.Drawing.Color.FromArgb(CType(CType(129, Byte), Integer), CType(CType(169, Byte), Integer), CType(CType(226, Byte), Integer))
        Appearance12.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(221, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(254, Byte), Integer))
        Appearance12.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance12.ForeColor = System.Drawing.Color.Black
        Me.cmbToCostCenter.DisplayLayout.Override.SelectedRowAppearance = Appearance12
        Me.cmbToCostCenter.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbToCostCenter.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbToCostCenter.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbToCostCenter.DisplayLayout.RowConnectorColor = System.Drawing.Color.FromArgb(CType(CType(1, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(208, Byte), Integer))
        Me.cmbToCostCenter.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.Solid
        Me.cmbToCostCenter.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbToCostCenter.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbToCostCenter.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbToCostCenter.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbToCostCenter.EditAreaDisplayStyle = Infragistics.Win.UltraWinGrid.EditAreaDisplayStyle.DisplayText
        Me.cmbToCostCenter.LimitToList = True
        Me.cmbToCostCenter.Location = New System.Drawing.Point(162, 34)
        Me.cmbToCostCenter.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbToCostCenter.MaxDropDownItems = 20
        Me.cmbToCostCenter.Name = "cmbToCostCenter"
        Me.cmbToCostCenter.Size = New System.Drawing.Size(291, 29)
        Me.cmbToCostCenter.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbToCostCenter, "Select To Cost Center")
        '
        'frmLogicalBifurcation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1156, 694)
        Me.Controls.Add(Me.gbDetail)
        Me.Controls.Add(Me.lblRemarks)
        Me.Controls.Add(Me.txtRemarks)
        Me.Controls.Add(Me.lblStartDate)
        Me.Controls.Add(Me.dtpStartDate)
        Me.Controls.Add(Me.lblFromCostCenter)
        Me.Controls.Add(Me.cmbFromCostCenter)
        Me.Controls.Add(Me.lblDocumentDate)
        Me.Controls.Add(Me.dtpDocumentDate)
        Me.Controls.Add(Me.lblDocumentNo)
        Me.Controls.Add(Me.txtDocumentNo)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.grdDetail)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmLogicalBifurcation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        CType(Me.grdDetail, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbFromCostCenter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbDetail.ResumeLayout(False)
        Me.gbDetail.PerformLayout()
        CType(Me.cmbToCostCenter, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents BtnPrint As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents grdDetail As Janus.Windows.GridEX.GridEX
    Friend WithEvents txtDocumentNo As System.Windows.Forms.TextBox
    Friend WithEvents lblDocumentNo As System.Windows.Forms.Label
    Friend WithEvents dtpDocumentDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDocumentDate As System.Windows.Forms.Label
    Friend WithEvents cmbFromCostCenter As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents lblFromCostCenter As System.Windows.Forms.Label
    Friend WithEvents lblStartDate As System.Windows.Forms.Label
    Friend WithEvents dtpStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblRemarks As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents gbDetail As System.Windows.Forms.GroupBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents lblComments As System.Windows.Forms.Label
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents lblAmount As System.Windows.Forms.Label
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents lblToCostCenter As System.Windows.Forms.Label
    Friend WithEvents cmbToCostCenter As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

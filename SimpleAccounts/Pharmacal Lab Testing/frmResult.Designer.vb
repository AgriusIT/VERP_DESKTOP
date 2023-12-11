<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmResult
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
        Dim grd_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmResult))
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn1 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ID")
        Dim UltraGridColumn2 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Name")
        Dim UltraGridColumn3 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Territory")
        Dim UltraGridColumn4 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("City")
        Dim UltraGridColumn5 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("State")
        Dim UltraGridColumn6 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("AcId")
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim grdSaved_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.GrpResult = New System.Windows.Forms.GroupBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rdbRejected = New System.Windows.Forms.RadioButton()
        Me.rdbApproved = New System.Windows.Forms.RadioButton()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.cmbSupplier = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.dtpSampledDate = New System.Windows.Forms.DateTimePicker()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtQCSampled = New System.Windows.Forms.TextBox()
        Me.dtpExpDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpMfgDate = New System.Windows.Forms.DateTimePicker()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtNoofContainer = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtContainerType = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cmbGRNNo = New System.Windows.Forms.ComboBox()
        Me.btnParametersMapping = New System.Windows.Forms.Button()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtAnalyticalMethod = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtProSpecNo = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtDRNo = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtPackSize = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtResultNo = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtBatchSize = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbQCNo = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtBatchNo = New System.Windows.Forms.TextBox()
        Me.dtpResultDate = New System.Windows.Forms.DateTimePicker()
        Me.pnlResultEntry = New System.Windows.Forms.Panel()
        Me.lblObservationSample = New System.Windows.Forms.Label()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdSaved = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnHelp = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnPrintForm = New System.Windows.Forms.ToolStripButton()
        Me.btnPrintSticker = New System.Windows.Forms.ToolStripSplitButton()
        Me.SampleStrickerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReleasedStickerToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.UltraTabPageControl1.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpResult.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.cmbSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlResultEntry.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.grd)
        Me.UltraTabPageControl1.Controls.Add(Me.GrpResult)
        Me.UltraTabPageControl1.Controls.Add(Me.pnlResultEntry)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1308, 881)
        '
        'grd
        '
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grd.AutoEdit = True
        Me.grd.ColumnAutoResize = True
        grd_DesignTimeLayout.LayoutString = resources.GetString("grd_DesignTimeLayout.LayoutString")
        Me.grd.DesignTimeLayout = grd_DesignTimeLayout
        Me.grd.Location = New System.Drawing.Point(4, 603)
        Me.grd.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grd.Name = "grd"
        Me.grd.Size = New System.Drawing.Size(1300, 285)
        Me.grd.TabIndex = 21
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'GrpResult
        '
        Me.GrpResult.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GrpResult.BackColor = System.Drawing.Color.Transparent
        Me.GrpResult.Controls.Add(Me.GroupBox1)
        Me.GrpResult.Controls.Add(Me.lblProgress)
        Me.GrpResult.Controls.Add(Me.cmbSupplier)
        Me.GrpResult.Controls.Add(Me.Label19)
        Me.GrpResult.Controls.Add(Me.txtRemarks)
        Me.GrpResult.Controls.Add(Me.dtpSampledDate)
        Me.GrpResult.Controls.Add(Me.Label18)
        Me.GrpResult.Controls.Add(Me.Label17)
        Me.GrpResult.Controls.Add(Me.txtQCSampled)
        Me.GrpResult.Controls.Add(Me.dtpExpDate)
        Me.GrpResult.Controls.Add(Me.dtpMfgDate)
        Me.GrpResult.Controls.Add(Me.Label16)
        Me.GrpResult.Controls.Add(Me.txtNoofContainer)
        Me.GrpResult.Controls.Add(Me.Label15)
        Me.GrpResult.Controls.Add(Me.txtContainerType)
        Me.GrpResult.Controls.Add(Me.Label14)
        Me.GrpResult.Controls.Add(Me.Label13)
        Me.GrpResult.Controls.Add(Me.cmbGRNNo)
        Me.GrpResult.Controls.Add(Me.btnParametersMapping)
        Me.GrpResult.Controls.Add(Me.Label12)
        Me.GrpResult.Controls.Add(Me.txtAnalyticalMethod)
        Me.GrpResult.Controls.Add(Me.Label11)
        Me.GrpResult.Controls.Add(Me.txtProSpecNo)
        Me.GrpResult.Controls.Add(Me.Label10)
        Me.GrpResult.Controls.Add(Me.txtDRNo)
        Me.GrpResult.Controls.Add(Me.Label9)
        Me.GrpResult.Controls.Add(Me.txtPackSize)
        Me.GrpResult.Controls.Add(Me.Label8)
        Me.GrpResult.Controls.Add(Me.Label7)
        Me.GrpResult.Controls.Add(Me.Label6)
        Me.GrpResult.Controls.Add(Me.txtResultNo)
        Me.GrpResult.Controls.Add(Me.Label5)
        Me.GrpResult.Controls.Add(Me.txtBatchSize)
        Me.GrpResult.Controls.Add(Me.Label4)
        Me.GrpResult.Controls.Add(Me.cmbQCNo)
        Me.GrpResult.Controls.Add(Me.Label2)
        Me.GrpResult.Controls.Add(Me.Label1)
        Me.GrpResult.Controls.Add(Me.txtBatchNo)
        Me.GrpResult.Controls.Add(Me.dtpResultDate)
        Me.GrpResult.Location = New System.Drawing.Point(4, 82)
        Me.GrpResult.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GrpResult.Name = "GrpResult"
        Me.GrpResult.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GrpResult.Size = New System.Drawing.Size(1300, 512)
        Me.GrpResult.TabIndex = 1
        Me.GrpResult.TabStop = False
        Me.GrpResult.Text = "Product Description"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdbRejected)
        Me.GroupBox1.Controls.Add(Me.rdbApproved)
        Me.GroupBox1.Location = New System.Drawing.Point(939, 414)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(298, 89)
        Me.GroupBox1.TabIndex = 40
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Stage Result"
        '
        'rdbRejected
        '
        Me.rdbRejected.AutoSize = True
        Me.rdbRejected.Location = New System.Drawing.Point(171, 40)
        Me.rdbRejected.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rdbRejected.Name = "rdbRejected"
        Me.rdbRejected.Size = New System.Drawing.Size(98, 24)
        Me.rdbRejected.TabIndex = 20
        Me.rdbRejected.TabStop = True
        Me.rdbRejected.Text = "Rejected"
        Me.rdbRejected.UseVisualStyleBackColor = True
        '
        'rdbApproved
        '
        Me.rdbApproved.AutoSize = True
        Me.rdbApproved.Location = New System.Drawing.Point(38, 40)
        Me.rdbApproved.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rdbApproved.Name = "rdbApproved"
        Me.rdbApproved.Size = New System.Drawing.Size(102, 24)
        Me.rdbApproved.TabIndex = 19
        Me.rdbApproved.TabStop = True
        Me.rdbApproved.Text = "Approved"
        Me.rdbApproved.UseVisualStyleBackColor = True
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(502, 155)
        Me.lblProgress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(394, 69)
        Me.lblProgress.TabIndex = 24
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'cmbSupplier
        '
        Me.cmbSupplier.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append
        Me.cmbSupplier.CheckedListSettings.CheckStateMember = ""
        Appearance1.BackColor = System.Drawing.Color.White
        Appearance1.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        Me.cmbSupplier.DisplayLayout.Appearance = Appearance1
        UltraGridColumn1.Header.VisiblePosition = 0
        UltraGridColumn1.Hidden = True
        UltraGridColumn2.Header.VisiblePosition = 1
        UltraGridColumn2.Width = 141
        UltraGridColumn3.Header.VisiblePosition = 2
        UltraGridColumn4.Header.VisiblePosition = 3
        UltraGridColumn5.Header.VisiblePosition = 4
        UltraGridColumn6.Header.VisiblePosition = 5
        UltraGridColumn6.Hidden = True
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn1, UltraGridColumn2, UltraGridColumn3, UltraGridColumn4, UltraGridColumn5, UltraGridColumn6})
        Me.cmbSupplier.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbSupplier.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbSupplier.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbSupplier.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbSupplier.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Appearance2.BackColor = System.Drawing.Color.Transparent
        Me.cmbSupplier.DisplayLayout.Override.CardAreaAppearance = Appearance2
        Me.cmbSupplier.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbSupplier.DisplayLayout.Override.CellPadding = 3
        Me.cmbSupplier.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance3.TextHAlignAsString = "Left"
        Me.cmbSupplier.DisplayLayout.Override.HeaderAppearance = Appearance3
        Me.cmbSupplier.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance4.BorderColor = System.Drawing.Color.LightGray
        Appearance4.TextVAlignAsString = "Middle"
        Me.cmbSupplier.DisplayLayout.Override.RowAppearance = Appearance4
        Appearance5.BackColor = System.Drawing.Color.LightSteelBlue
        Appearance5.BorderColor = System.Drawing.Color.Black
        Appearance5.ForeColor = System.Drawing.Color.Black
        Me.cmbSupplier.DisplayLayout.Override.SelectedRowAppearance = Appearance5
        Me.cmbSupplier.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbSupplier.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbSupplier.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbSupplier.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.cmbSupplier.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbSupplier.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbSupplier.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbSupplier.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbSupplier.LimitToList = True
        Me.cmbSupplier.Location = New System.Drawing.Point(192, 109)
        Me.cmbSupplier.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbSupplier.Name = "cmbSupplier"
        Me.cmbSupplier.Size = New System.Drawing.Size(298, 29)
        Me.cmbSupplier.TabIndex = 3
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(9, 480)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(73, 20)
        Me.Label19.TabIndex = 22
        Me.Label19.Text = "Remarks"
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(192, 475)
        Me.txtRemarks.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(736, 26)
        Me.txtRemarks.TabIndex = 18
        '
        'dtpSampledDate
        '
        Me.dtpSampledDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpSampledDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpSampledDate.Location = New System.Drawing.Point(632, 349)
        Me.dtpSampledDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpSampledDate.Name = "dtpSampledDate"
        Me.dtpSampledDate.Size = New System.Drawing.Size(296, 26)
        Me.dtpSampledDate.TabIndex = 15
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(502, 358)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(111, 20)
        Me.Label18.TabIndex = 31
        Me.Label18.Text = "Date Sampled"
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(9, 360)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(147, 20)
        Me.Label17.TabIndex = 16
        Me.Label17.Text = "Qty Sampled by QC"
        '
        'txtQCSampled
        '
        Me.txtQCSampled.Location = New System.Drawing.Point(192, 355)
        Me.txtQCSampled.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtQCSampled.Name = "txtQCSampled"
        Me.txtQCSampled.Size = New System.Drawing.Size(296, 26)
        Me.txtQCSampled.TabIndex = 9
        '
        'dtpExpDate
        '
        Me.dtpExpDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpExpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpExpDate.Location = New System.Drawing.Point(632, 431)
        Me.dtpExpDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpExpDate.Name = "dtpExpDate"
        Me.dtpExpDate.Size = New System.Drawing.Size(296, 26)
        Me.dtpExpDate.TabIndex = 17
        '
        'dtpMfgDate
        '
        Me.dtpMfgDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpMfgDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpMfgDate.Location = New System.Drawing.Point(632, 391)
        Me.dtpMfgDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpMfgDate.Name = "dtpMfgDate"
        Me.dtpMfgDate.Size = New System.Drawing.Size(296, 26)
        Me.dtpMfgDate.TabIndex = 16
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(502, 238)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(120, 20)
        Me.Label16.TabIndex = 25
        Me.Label16.Text = "No of Container"
        '
        'txtNoofContainer
        '
        Me.txtNoofContainer.Location = New System.Drawing.Point(632, 235)
        Me.txtNoofContainer.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtNoofContainer.Name = "txtNoofContainer"
        Me.txtNoofContainer.Size = New System.Drawing.Size(296, 26)
        Me.txtNoofContainer.TabIndex = 12
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(9, 240)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(134, 20)
        Me.Label15.TabIndex = 10
        Me.Label15.Text = "Type of Container"
        '
        'txtContainerType
        '
        Me.txtContainerType.Location = New System.Drawing.Point(192, 235)
        Me.txtContainerType.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtContainerType.Name = "txtContainerType"
        Me.txtContainerType.Size = New System.Drawing.Size(296, 26)
        Me.txtContainerType.TabIndex = 6
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(9, 155)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(73, 20)
        Me.Label14.TabIndex = 6
        Me.Label14.Text = "GRN No."
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(9, 114)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(67, 20)
        Me.Label13.TabIndex = 4
        Me.Label13.Text = "Supplier"
        '
        'cmbGRNNo
        '
        Me.cmbGRNNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbGRNNo.FormattingEnabled = True
        Me.cmbGRNNo.Location = New System.Drawing.Point(192, 152)
        Me.cmbGRNNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbGRNNo.Name = "cmbGRNNo"
        Me.cmbGRNNo.Size = New System.Drawing.Size(296, 28)
        Me.cmbGRNNo.TabIndex = 4
        '
        'btnParametersMapping
        '
        Me.btnParametersMapping.Location = New System.Drawing.Point(939, 334)
        Me.btnParametersMapping.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnParametersMapping.Name = "btnParametersMapping"
        Me.btnParametersMapping.Size = New System.Drawing.Size(298, 71)
        Me.btnParametersMapping.TabIndex = 37
        Me.btnParametersMapping.Text = "Product Parameters Mapping"
        Me.btnParametersMapping.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(9, 445)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(147, 20)
        Me.Label12.TabIndex = 20
        Me.Label12.Text = "Analytical Method #"
        '
        'txtAnalyticalMethod
        '
        Me.txtAnalyticalMethod.Location = New System.Drawing.Point(192, 435)
        Me.txtAnalyticalMethod.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtAnalyticalMethod.Name = "txtAnalyticalMethod"
        Me.txtAnalyticalMethod.Size = New System.Drawing.Size(296, 26)
        Me.txtAnalyticalMethod.TabIndex = 11
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(9, 400)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(172, 20)
        Me.Label11.TabIndex = 18
        Me.Label11.Text = "Product Specification #"
        '
        'txtProSpecNo
        '
        Me.txtProSpecNo.Location = New System.Drawing.Point(192, 395)
        Me.txtProSpecNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtProSpecNo.Name = "txtProSpecNo"
        Me.txtProSpecNo.Size = New System.Drawing.Size(296, 26)
        Me.txtProSpecNo.TabIndex = 10
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(9, 320)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(61, 20)
        Me.Label10.TabIndex = 14
        Me.Label10.Text = "DR No."
        '
        'txtDRNo
        '
        Me.txtDRNo.Location = New System.Drawing.Point(192, 315)
        Me.txtDRNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtDRNo.Name = "txtDRNo"
        Me.txtDRNo.Size = New System.Drawing.Size(296, 26)
        Me.txtDRNo.TabIndex = 8
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(502, 320)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(79, 20)
        Me.Label9.TabIndex = 29
        Me.Label9.Text = "Pack Size"
        '
        'txtPackSize
        '
        Me.txtPackSize.Location = New System.Drawing.Point(632, 315)
        Me.txtPackSize.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPackSize.Name = "txtPackSize"
        Me.txtPackSize.Size = New System.Drawing.Size(296, 26)
        Me.txtPackSize.TabIndex = 14
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(502, 435)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(87, 20)
        Me.Label8.TabIndex = 35
        Me.Label8.Text = "Exp. Date: "
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(502, 400)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(87, 20)
        Me.Label7.TabIndex = 33
        Me.Label7.Text = "Mfg. Date: "
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 74)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(83, 20)
        Me.Label6.TabIndex = 2
        Me.Label6.Text = "Result No."
        '
        'txtResultNo
        '
        Me.txtResultNo.Location = New System.Drawing.Point(192, 69)
        Me.txtResultNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtResultNo.Name = "txtResultNo"
        Me.txtResultNo.Size = New System.Drawing.Size(178, 26)
        Me.txtResultNo.TabIndex = 2
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(502, 280)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(86, 20)
        Me.Label5.TabIndex = 27
        Me.Label5.Text = "Batch Size"
        '
        'txtBatchSize
        '
        Me.txtBatchSize.Location = New System.Drawing.Point(632, 275)
        Me.txtBatchSize.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtBatchSize.Name = "txtBatchSize"
        Me.txtBatchSize.Size = New System.Drawing.Size(296, 26)
        Me.txtBatchSize.TabIndex = 13
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 280)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 20)
        Me.Label4.TabIndex = 12
        Me.Label4.Text = "Batch No."
        '
        'cmbQCNo
        '
        Me.cmbQCNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbQCNo.FormattingEnabled = True
        Me.cmbQCNo.Location = New System.Drawing.Point(190, 194)
        Me.cmbQCNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbQCNo.Name = "cmbQCNo"
        Me.cmbQCNo.Size = New System.Drawing.Size(296, 28)
        Me.cmbQCNo.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 197)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 20)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "QC No."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 31)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(102, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Result Date: "
        '
        'txtBatchNo
        '
        Me.txtBatchNo.Location = New System.Drawing.Point(192, 275)
        Me.txtBatchNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtBatchNo.Name = "txtBatchNo"
        Me.txtBatchNo.Size = New System.Drawing.Size(296, 26)
        Me.txtBatchNo.TabIndex = 7
        '
        'dtpResultDate
        '
        Me.dtpResultDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpResultDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpResultDate.Location = New System.Drawing.Point(192, 29)
        Me.dtpResultDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpResultDate.MaximumSize = New System.Drawing.Size(178, 20)
        Me.dtpResultDate.Name = "dtpResultDate"
        Me.dtpResultDate.Size = New System.Drawing.Size(178, 20)
        Me.dtpResultDate.TabIndex = 1
        '
        'pnlResultEntry
        '
        Me.pnlResultEntry.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlResultEntry.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlResultEntry.Controls.Add(Me.lblObservationSample)
        Me.pnlResultEntry.Location = New System.Drawing.Point(0, 0)
        Me.pnlResultEntry.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlResultEntry.Name = "pnlResultEntry"
        Me.pnlResultEntry.Size = New System.Drawing.Size(1310, 72)
        Me.pnlResultEntry.TabIndex = 0
        '
        'lblObservationSample
        '
        Me.lblObservationSample.AutoSize = True
        Me.lblObservationSample.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblObservationSample.ForeColor = System.Drawing.Color.Black
        Me.lblObservationSample.Location = New System.Drawing.Point(4, 14)
        Me.lblObservationSample.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblObservationSample.Name = "lblObservationSample"
        Me.lblObservationSample.Size = New System.Drawing.Size(189, 36)
        Me.lblObservationSample.TabIndex = 0
        Me.lblObservationSample.Text = "Result Entry"
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grdSaved)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1308, 881)
        '
        'grdSaved
        '
        Me.grdSaved.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdSaved.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdSaved_DesignTimeLayout.LayoutString = resources.GetString("grdSaved_DesignTimeLayout.LayoutString")
        Me.grdSaved.DesignTimeLayout = grdSaved_DesignTimeLayout
        Me.grdSaved.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdSaved.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdSaved.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdSaved.GroupByBoxVisible = False
        Me.grdSaved.Location = New System.Drawing.Point(-2, 0)
        Me.grdSaved.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdSaved.Name = "grdSaved"
        Me.grdSaved.RecordNavigator = True
        Me.grdSaved.Size = New System.Drawing.Size(1312, 888)
        Me.grdSaved.TabIndex = 29
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.btnDelete, Me.btnRefresh, Me.btnHelp, Me.ToolStripSeparator1, Me.btnPrintForm, Me.btnPrintSticker})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1310, 38)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = Global.SimpleAccounts.My.Resources.Resources.BtnNew_Image
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(75, 35)
        Me.btnNew.Text = "&New"
        '
        'btnEdit
        '
        Me.btnEdit.Image = Global.SimpleAccounts.My.Resources.Resources.BtnEdit_Image
        Me.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(70, 35)
        Me.btnEdit.Text = "&Edit"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(77, 35)
        Me.btnSave.Text = "&Save"
        '
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(90, 35)
        Me.btnDelete.Text = "&Delete"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = CType(resources.GetObject("btnRefresh.Image"), System.Drawing.Image)
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 35)
        Me.btnRefresh.Text = "&Refresh"
        '
        'btnHelp
        '
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(77, 35)
        Me.btnHelp.Text = "&Help"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 38)
        '
        'btnPrintForm
        '
        Me.btnPrintForm.Image = CType(resources.GetObject("btnPrintForm.Image"), System.Drawing.Image)
        Me.btnPrintForm.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrintForm.Name = "btnPrintForm"
        Me.btnPrintForm.Size = New System.Drawing.Size(123, 35)
        Me.btnPrintForm.Text = "Print Form"
        '
        'btnPrintSticker
        '
        Me.btnPrintSticker.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SampleStrickerToolStripMenuItem, Me.ReleasedStickerToolStripMenuItem})
        Me.btnPrintSticker.Image = CType(resources.GetObject("btnPrintSticker.Image"), System.Drawing.Image)
        Me.btnPrintSticker.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrintSticker.Name = "btnPrintSticker"
        Me.btnPrintSticker.Size = New System.Drawing.Size(149, 35)
        Me.btnPrintSticker.Text = "Print Sticker"
        '
        'SampleStrickerToolStripMenuItem
        '
        Me.SampleStrickerToolStripMenuItem.Name = "SampleStrickerToolStripMenuItem"
        Me.SampleStrickerToolStripMenuItem.Size = New System.Drawing.Size(221, 30)
        Me.SampleStrickerToolStripMenuItem.Text = "Sample Sticker"
        '
        'ReleasedStickerToolStripMenuItem
        '
        Me.ReleasedStickerToolStripMenuItem.Name = "ReleasedStickerToolStripMenuItem"
        Me.ReleasedStickerToolStripMenuItem.Size = New System.Drawing.Size(221, 30)
        Me.ReleasedStickerToolStripMenuItem.Text = "Released Sticker"
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 43)
        Me.UltraTabControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(1310, 908)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 28
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Result"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "History"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.VisualStudio2005
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1308, 881)
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(1258, 0)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar1.MyGrid = Me.grdSaved
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(50, 38)
        Me.CtrlGrdBar1.TabIndex = 38
        Me.CtrlGrdBar1.TabStop = False
        '
        'frmResult
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1310, 949)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmResult"
        Me.Text = "Result Entry"
        Me.UltraTabPageControl1.ResumeLayout(False)
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GrpResult.ResumeLayout(False)
        Me.GrpResult.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.cmbSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlResultEntry.ResumeLayout(False)
        Me.pnlResultEntry.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlResultEntry As System.Windows.Forms.Panel
    Friend WithEvents lblObservationSample As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnPrintForm As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnPrintSticker As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents SampleStrickerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReleasedStickerToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtBatchNo As System.Windows.Forms.TextBox
    Friend WithEvents dtpResultDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents GrpResult As System.Windows.Forms.GroupBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtAnalyticalMethod As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtProSpecNo As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtDRNo As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtPackSize As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtResultNo As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtBatchSize As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbQCNo As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents btnParametersMapping As System.Windows.Forms.Button
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents grdSaved As Janus.Windows.GridEX.GridEX
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents dtpSampledDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents txtQCSampled As System.Windows.Forms.TextBox
    Friend WithEvents dtpExpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpMfgDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtNoofContainer As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents txtContainerType As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cmbGRNNo As System.Windows.Forms.ComboBox
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbSupplier As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents rdbApproved As System.Windows.Forms.RadioButton
    Friend WithEvents rdbRejected As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class

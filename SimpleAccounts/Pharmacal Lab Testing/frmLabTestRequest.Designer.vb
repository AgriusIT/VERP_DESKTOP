<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLabTestRequest
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
        Dim Appearance22 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand2 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn29 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ID")
        Dim UltraGridColumn30 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Name")
        Dim UltraGridColumn25 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Territory")
        Dim UltraGridColumn26 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("City")
        Dim UltraGridColumn27 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("State")
        Dim UltraGridColumn28 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("AcId")
        Dim Appearance28 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance29 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance30 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance31 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim grdLabRequest_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLabTestRequest))
        Dim grdSaved_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.cmbEmployee = New System.Windows.Forms.ComboBox()
        Me.lblBy = New System.Windows.Forms.Label()
        Me.txtStage = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.cmbPlan = New System.Windows.Forms.ComboBox()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.cmbTicket = New System.Windows.Forms.ComboBox()
        Me.cmbSupplier = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.cmbStage = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.cmbLCNo = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cmbPONo = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.dtpRetestDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpExpDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpMfgDate = New System.Windows.Forms.DateTimePicker()
        Me.rbName = New System.Windows.Forms.RadioButton()
        Me.rbCode = New System.Windows.Forms.RadioButton()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtComments = New System.Windows.Forms.TextBox()
        Me.cmbItem = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.txtBatchNo = New System.Windows.Forms.TextBox()
        Me.txtNoofContainers = New System.Windows.Forms.TextBox()
        Me.txtQty = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.grdLabRequest = New Janus.Windows.GridEX.GridEX()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtRequestNo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cmbDepartment = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cmbGRNNo = New System.Windows.Forms.ComboBox()
        Me.dtpRequestDate = New System.Windows.Forms.DateTimePicker()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblLabTestRequest = New System.Windows.Forms.Label()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdSaved = New Janus.Windows.GridEX.GridEX()
        Me.grpSearch = New System.Windows.Forms.GroupBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtSupplierName = New System.Windows.Forms.TextBox()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.btnSearchDetail = New System.Windows.Forms.Button()
        Me.txtPONo = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtGRNNo = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.btnPrintForm = New System.Windows.Forms.ToolStripButton()
        Me.btnLoadAll = New System.Windows.Forms.ToolStripButton()
        Me.btnSearch = New System.Windows.Forms.ToolStripButton()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnHelp = New System.Windows.Forms.ToolStripButton()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.miniToolStrip = New System.Windows.Forms.ToolStrip()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.BackgroundWorker2 = New System.ComponentModel.BackgroundWorker()
        Me.CtrlGrdBar2 = New SimpleAccounts.CtrlGrdBar()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.cmbSupplier, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        CType(Me.cmbItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdLabRequest, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.UltraTabPageControl1.SuspendLayout()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpSearch.SuspendLayout()
        Me.ToolStrip2.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.cmbEmployee)
        Me.UltraTabPageControl2.Controls.Add(Me.lblBy)
        Me.UltraTabPageControl2.Controls.Add(Me.txtStage)
        Me.UltraTabPageControl2.Controls.Add(Me.Label25)
        Me.UltraTabPageControl2.Controls.Add(Me.Label24)
        Me.UltraTabPageControl2.Controls.Add(Me.cmbPlan)
        Me.UltraTabPageControl2.Controls.Add(Me.Label23)
        Me.UltraTabPageControl2.Controls.Add(Me.cmbTicket)
        Me.UltraTabPageControl2.Controls.Add(Me.cmbSupplier)
        Me.UltraTabPageControl2.Controls.Add(Me.txtRemarks)
        Me.UltraTabPageControl2.Controls.Add(Me.cmbStage)
        Me.UltraTabPageControl2.Controls.Add(Me.Label12)
        Me.UltraTabPageControl2.Controls.Add(Me.Label20)
        Me.UltraTabPageControl2.Controls.Add(Me.cmbLCNo)
        Me.UltraTabPageControl2.Controls.Add(Me.Label11)
        Me.UltraTabPageControl2.Controls.Add(Me.cmbPONo)
        Me.UltraTabPageControl2.Controls.Add(Me.GroupBox1)
        Me.UltraTabPageControl2.Controls.Add(Me.grdLabRequest)
        Me.UltraTabPageControl2.Controls.Add(Me.Label4)
        Me.UltraTabPageControl2.Controls.Add(Me.Label6)
        Me.UltraTabPageControl2.Controls.Add(Me.txtRequestNo)
        Me.UltraTabPageControl2.Controls.Add(Me.Label3)
        Me.UltraTabPageControl2.Controls.Add(Me.Label2)
        Me.UltraTabPageControl2.Controls.Add(Me.cmbDepartment)
        Me.UltraTabPageControl2.Controls.Add(Me.Label1)
        Me.UltraTabPageControl2.Controls.Add(Me.Label9)
        Me.UltraTabPageControl2.Controls.Add(Me.cmbGRNNo)
        Me.UltraTabPageControl2.Controls.Add(Me.dtpRequestDate)
        Me.UltraTabPageControl2.Controls.Add(Me.Panel1)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1303, 931)
        '
        'cmbEmployee
        '
        Me.cmbEmployee.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbEmployee.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbEmployee.Location = New System.Drawing.Point(579, 203)
        Me.cmbEmployee.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbEmployee.Name = "cmbEmployee"
        Me.cmbEmployee.Size = New System.Drawing.Size(265, 28)
        Me.cmbEmployee.TabIndex = 12
        '
        'lblBy
        '
        Me.lblBy.AutoSize = True
        Me.lblBy.BackColor = System.Drawing.Color.Transparent
        Me.lblBy.Location = New System.Drawing.Point(465, 209)
        Me.lblBy.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBy.Name = "lblBy"
        Me.lblBy.Size = New System.Drawing.Size(79, 20)
        Me.lblBy.TabIndex = 32
        Me.lblBy.Text = "Employee"
        '
        'txtStage
        '
        Me.txtStage.Location = New System.Drawing.Point(996, 162)
        Me.txtStage.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtStage.Name = "txtStage"
        Me.txtStage.Size = New System.Drawing.Size(265, 26)
        Me.txtStage.TabIndex = 30
        Me.txtStage.Visible = False
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Location = New System.Drawing.Point(880, 166)
        Me.Label25.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(52, 20)
        Me.Label25.TabIndex = 29
        Me.Label25.Text = "Stage"
        Me.Label25.Visible = False
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Location = New System.Drawing.Point(464, 83)
        Me.Label24.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(40, 20)
        Me.Label24.TabIndex = 28
        Me.Label24.Text = "Plan"
        '
        'cmbPlan
        '
        Me.cmbPlan.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbPlan.FormattingEnabled = True
        Me.cmbPlan.Location = New System.Drawing.Point(579, 77)
        Me.cmbPlan.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbPlan.Name = "cmbPlan"
        Me.cmbPlan.Size = New System.Drawing.Size(265, 28)
        Me.cmbPlan.TabIndex = 9
        '
        'Label23
        '
        Me.Label23.AutoSize = True
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Location = New System.Drawing.Point(464, 125)
        Me.Label23.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label23.Name = "Label23"
        Me.Label23.Size = New System.Drawing.Size(51, 20)
        Me.Label23.TabIndex = 25
        Me.Label23.Text = "Ticket"
        '
        'cmbTicket
        '
        Me.cmbTicket.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbTicket.FormattingEnabled = True
        Me.cmbTicket.Location = New System.Drawing.Point(579, 118)
        Me.cmbTicket.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbTicket.Name = "cmbTicket"
        Me.cmbTicket.Size = New System.Drawing.Size(265, 28)
        Me.cmbTicket.TabIndex = 10
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
        Me.cmbSupplier.Location = New System.Drawing.Point(128, 158)
        Me.cmbSupplier.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbSupplier.Name = "cmbSupplier"
        Me.cmbSupplier.Size = New System.Drawing.Size(326, 29)
        Me.cmbSupplier.TabIndex = 5
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(128, 285)
        Me.txtRemarks.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(324, 26)
        Me.txtRemarks.TabIndex = 8
        '
        'cmbStage
        '
        Me.cmbStage.Location = New System.Drawing.Point(579, 162)
        Me.cmbStage.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbStage.Name = "cmbStage"
        Me.cmbStage.Size = New System.Drawing.Size(265, 28)
        Me.cmbStage.TabIndex = 11
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Location = New System.Drawing.Point(465, 291)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(53, 20)
        Me.Label12.TabIndex = 12
        Me.Label12.Text = "LC No"
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Location = New System.Drawing.Point(20, 289)
        Me.Label20.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(73, 20)
        Me.Label20.TabIndex = 18
        Me.Label20.Text = "Remarks"
        '
        'cmbLCNo
        '
        Me.cmbLCNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbLCNo.FormattingEnabled = True
        Me.cmbLCNo.Location = New System.Drawing.Point(579, 286)
        Me.cmbLCNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbLCNo.Name = "cmbLCNo"
        Me.cmbLCNo.Size = New System.Drawing.Size(265, 28)
        Me.cmbLCNo.TabIndex = 14
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Location = New System.Drawing.Point(462, 249)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(55, 20)
        Me.Label11.TabIndex = 8
        Me.Label11.Text = "PO No"
        '
        'cmbPONo
        '
        Me.cmbPONo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbPONo.FormattingEnabled = True
        Me.cmbPONo.Location = New System.Drawing.Point(579, 245)
        Me.cmbPONo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbPONo.Name = "cmbPONo"
        Me.cmbPONo.Size = New System.Drawing.Size(265, 28)
        Me.cmbPONo.TabIndex = 13
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.lblProgress)
        Me.GroupBox1.Controls.Add(Me.Label22)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.Label19)
        Me.GroupBox1.Controls.Add(Me.dtpRetestDate)
        Me.GroupBox1.Controls.Add(Me.dtpExpDate)
        Me.GroupBox1.Controls.Add(Me.dtpMfgDate)
        Me.GroupBox1.Controls.Add(Me.rbName)
        Me.GroupBox1.Controls.Add(Me.rbCode)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtComments)
        Me.GroupBox1.Controls.Add(Me.cmbItem)
        Me.GroupBox1.Controls.Add(Me.txtBatchNo)
        Me.GroupBox1.Controls.Add(Me.txtNoofContainers)
        Me.GroupBox1.Controls.Add(Me.txtQty)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.btnAdd)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 320)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(1293, 260)
        Me.GroupBox1.TabIndex = 20
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Item Description"
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(738, 97)
        Me.lblProgress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(394, 69)
        Me.lblProgress.TabIndex = 29
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.Location = New System.Drawing.Point(14, 186)
        Me.Label22.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(79, 20)
        Me.Label22.TabIndex = 32
        Me.Label22.Text = "Mfg. Date"
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(398, 186)
        Me.Label21.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(79, 20)
        Me.Label21.TabIndex = 31
        Me.Label21.Text = "Exp. Date"
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Location = New System.Drawing.Point(398, 146)
        Me.Label19.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(105, 20)
        Me.Label19.TabIndex = 30
        Me.Label19.Text = "Re-Test Date"
        '
        'dtpRetestDate
        '
        Me.dtpRetestDate.Checked = False
        Me.dtpRetestDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpRetestDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpRetestDate.Location = New System.Drawing.Point(528, 137)
        Me.dtpRetestDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpRetestDate.Name = "dtpRetestDate"
        Me.dtpRetestDate.ShowCheckBox = True
        Me.dtpRetestDate.Size = New System.Drawing.Size(259, 26)
        Me.dtpRetestDate.TabIndex = 20
        '
        'dtpExpDate
        '
        Me.dtpExpDate.Checked = False
        Me.dtpExpDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpExpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpExpDate.Location = New System.Drawing.Point(528, 177)
        Me.dtpExpDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpExpDate.Name = "dtpExpDate"
        Me.dtpExpDate.ShowCheckBox = True
        Me.dtpExpDate.Size = New System.Drawing.Size(259, 26)
        Me.dtpExpDate.TabIndex = 22
        '
        'dtpMfgDate
        '
        Me.dtpMfgDate.Checked = False
        Me.dtpMfgDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpMfgDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpMfgDate.Location = New System.Drawing.Point(122, 177)
        Me.dtpMfgDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpMfgDate.Name = "dtpMfgDate"
        Me.dtpMfgDate.ShowCheckBox = True
        Me.dtpMfgDate.Size = New System.Drawing.Size(265, 26)
        Me.dtpMfgDate.TabIndex = 21
        '
        'rbName
        '
        Me.rbName.AutoSize = True
        Me.rbName.Location = New System.Drawing.Point(206, 23)
        Me.rbName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbName.Name = "rbName"
        Me.rbName.Size = New System.Drawing.Size(76, 24)
        Me.rbName.TabIndex = 12
        Me.rbName.TabStop = True
        Me.rbName.Text = "Name"
        Me.rbName.UseVisualStyleBackColor = True
        '
        'rbCode
        '
        Me.rbCode.AutoSize = True
        Me.rbCode.Location = New System.Drawing.Point(122, 23)
        Me.rbCode.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbCode.Name = "rbCode"
        Me.rbCode.Size = New System.Drawing.Size(72, 24)
        Me.rbCode.TabIndex = 15
        Me.rbCode.TabStop = True
        Me.rbCode.Text = "Code"
        Me.rbCode.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(14, 223)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(86, 20)
        Me.Label8.TabIndex = 8
        Me.Label8.Text = "Comments"
        '
        'txtComments
        '
        Me.txtComments.Location = New System.Drawing.Point(122, 218)
        Me.txtComments.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtComments.Name = "txtComments"
        Me.txtComments.Size = New System.Drawing.Size(666, 26)
        Me.txtComments.TabIndex = 23
        '
        'cmbItem
        '
        Me.cmbItem.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Append
        Me.cmbItem.CheckedListSettings.CheckStateMember = ""
        Appearance22.BackColor = System.Drawing.Color.White
        Appearance22.BackColor2 = System.Drawing.Color.FromArgb(CType(CType(61, Byte), Integer), CType(CType(149, Byte), Integer), CType(CType(255, Byte), Integer))
        Appearance22.BackGradientStyle = Infragistics.Win.GradientStyle.ForwardDiagonal
        Me.cmbItem.DisplayLayout.Appearance = Appearance22
        UltraGridColumn29.Header.VisiblePosition = 0
        UltraGridColumn29.Hidden = True
        UltraGridColumn30.Header.VisiblePosition = 1
        UltraGridColumn30.Width = 141
        UltraGridColumn25.Header.VisiblePosition = 2
        UltraGridColumn26.Header.VisiblePosition = 3
        UltraGridColumn27.Header.VisiblePosition = 4
        UltraGridColumn28.Header.VisiblePosition = 5
        UltraGridColumn28.Hidden = True
        UltraGridBand2.Columns.AddRange(New Object() {UltraGridColumn29, UltraGridColumn30, UltraGridColumn25, UltraGridColumn26, UltraGridColumn27, UltraGridColumn28})
        Me.cmbItem.DisplayLayout.BandsSerializer.Add(UltraGridBand2)
        Me.cmbItem.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbItem.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbItem.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Appearance28.BackColor = System.Drawing.Color.Transparent
        Me.cmbItem.DisplayLayout.Override.CardAreaAppearance = Appearance28
        Me.cmbItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbItem.DisplayLayout.Override.CellPadding = 3
        Me.cmbItem.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance29.TextHAlignAsString = "Left"
        Me.cmbItem.DisplayLayout.Override.HeaderAppearance = Appearance29
        Me.cmbItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance30.BorderColor = System.Drawing.Color.LightGray
        Appearance30.TextVAlignAsString = "Middle"
        Me.cmbItem.DisplayLayout.Override.RowAppearance = Appearance30
        Appearance31.BackColor = System.Drawing.Color.LightSteelBlue
        Appearance31.BorderColor = System.Drawing.Color.Black
        Appearance31.ForeColor = System.Drawing.Color.Black
        Me.cmbItem.DisplayLayout.Override.SelectedRowAppearance = Appearance31
        Me.cmbItem.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbItem.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbItem.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbItem.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.cmbItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbItem.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbItem.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbItem.LimitToList = True
        Me.cmbItem.Location = New System.Drawing.Point(122, 54)
        Me.cmbItem.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbItem.Name = "cmbItem"
        Me.cmbItem.Size = New System.Drawing.Size(668, 29)
        Me.cmbItem.TabIndex = 16
        '
        'txtBatchNo
        '
        Me.txtBatchNo.Location = New System.Drawing.Point(122, 97)
        Me.txtBatchNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtBatchNo.Name = "txtBatchNo"
        Me.txtBatchNo.Size = New System.Drawing.Size(265, 26)
        Me.txtBatchNo.TabIndex = 17
        '
        'txtNoofContainers
        '
        Me.txtNoofContainers.Location = New System.Drawing.Point(528, 97)
        Me.txtNoofContainers.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtNoofContainers.Name = "txtNoofContainers"
        Me.txtNoofContainers.Size = New System.Drawing.Size(259, 26)
        Me.txtNoofContainers.TabIndex = 19
        '
        'txtQty
        '
        Me.txtQty.Location = New System.Drawing.Point(122, 137)
        Me.txtQty.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.Size = New System.Drawing.Size(265, 26)
        Me.txtQty.TabIndex = 18
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(398, 102)
        Me.Label13.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(128, 20)
        Me.Label13.TabIndex = 4
        Me.Label13.Text = "No of Containers"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(14, 146)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(33, 20)
        Me.Label10.TabIndex = 6
        Me.Label10.Text = "Qty"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(14, 54)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 20)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Item"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(14, 102)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(102, 20)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Lot/Batch No"
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(798, 215)
        Me.btnAdd.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(70, 35)
        Me.btnAdd.TabIndex = 24
        Me.btnAdd.Text = "+"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'grdLabRequest
        '
        Me.grdLabRequest.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdLabRequest.AutoEdit = True
        Me.grdLabRequest.ColumnAutoResize = True
        grdLabRequest_DesignTimeLayout.LayoutString = resources.GetString("grdLabRequest_DesignTimeLayout.LayoutString")
        Me.grdLabRequest.DesignTimeLayout = grdLabRequest_DesignTimeLayout
        Me.grdLabRequest.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdLabRequest.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdLabRequest.GroupByBoxVisible = False
        Me.grdLabRequest.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.grdLabRequest.Location = New System.Drawing.Point(-2, 589)
        Me.grdLabRequest.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdLabRequest.Name = "grdLabRequest"
        Me.grdLabRequest.Size = New System.Drawing.Size(1306, 350)
        Me.grdLabRequest.TabIndex = 25
        Me.grdLabRequest.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdLabRequest.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdLabRequest.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Location = New System.Drawing.Point(20, 125)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(66, 20)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Doc No."
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Location = New System.Drawing.Point(462, 166)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 20)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Stage"
        '
        'txtRequestNo
        '
        Me.txtRequestNo.Location = New System.Drawing.Point(128, 120)
        Me.txtRequestNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtRequestNo.Name = "txtRequestNo"
        Me.txtRequestNo.Size = New System.Drawing.Size(324, 26)
        Me.txtRequestNo.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Location = New System.Drawing.Point(20, 206)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 20)
        Me.Label3.TabIndex = 10
        Me.Label3.Text = "GRN No."
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Location = New System.Drawing.Point(20, 158)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 20)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Supplier"
        '
        'cmbDepartment
        '
        Me.cmbDepartment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbDepartment.FormattingEnabled = True
        Me.cmbDepartment.Location = New System.Drawing.Point(128, 243)
        Me.cmbDepartment.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbDepartment.Name = "cmbDepartment"
        Me.cmbDepartment.Size = New System.Drawing.Size(324, 28)
        Me.cmbDepartment.TabIndex = 7
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Location = New System.Drawing.Point(20, 89)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 20)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Date: "
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Location = New System.Drawing.Point(20, 248)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(94, 20)
        Me.Label9.TabIndex = 14
        Me.Label9.Text = "Department"
        '
        'cmbGRNNo
        '
        Me.cmbGRNNo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbGRNNo.FormattingEnabled = True
        Me.cmbGRNNo.Location = New System.Drawing.Point(128, 202)
        Me.cmbGRNNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbGRNNo.Name = "cmbGRNNo"
        Me.cmbGRNNo.Size = New System.Drawing.Size(324, 28)
        Me.cmbGRNNo.TabIndex = 6
        '
        'dtpRequestDate
        '
        Me.dtpRequestDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpRequestDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpRequestDate.Location = New System.Drawing.Point(128, 80)
        Me.dtpRequestDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpRequestDate.Name = "dtpRequestDate"
        Me.dtpRequestDate.Size = New System.Drawing.Size(324, 26)
        Me.dtpRequestDate.TabIndex = 3
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Panel1.Controls.Add(Me.lblLabTestRequest)
        Me.Panel1.Location = New System.Drawing.Point(0, -2)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1303, 72)
        Me.Panel1.TabIndex = 1
        '
        'lblLabTestRequest
        '
        Me.lblLabTestRequest.AutoSize = True
        Me.lblLabTestRequest.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLabTestRequest.ForeColor = System.Drawing.Color.Black
        Me.lblLabTestRequest.Location = New System.Drawing.Point(4, 12)
        Me.lblLabTestRequest.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblLabTestRequest.Name = "lblLabTestRequest"
        Me.lblLabTestRequest.Size = New System.Drawing.Size(283, 41)
        Me.lblLabTestRequest.TabIndex = 1
        Me.lblLabTestRequest.Text = "Lab Test Request"
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.grdSaved)
        Me.UltraTabPageControl1.Controls.Add(Me.grpSearch)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1303, 931)
        '
        'grdSaved
        '
        Me.grdSaved.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdSaved.ColumnAutoResize = True
        grdSaved_DesignTimeLayout.LayoutString = resources.GetString("grdSaved_DesignTimeLayout.LayoutString")
        Me.grdSaved.DesignTimeLayout = grdSaved_DesignTimeLayout
        Me.grdSaved.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSaved.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdSaved.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdSaved.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdSaved.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.grdSaved.Location = New System.Drawing.Point(0, 140)
        Me.grdSaved.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdSaved.Name = "grdSaved"
        Me.grdSaved.Size = New System.Drawing.Size(1303, 791)
        Me.grdSaved.TabIndex = 1
        Me.grdSaved.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdSaved.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'grpSearch
        '
        Me.grpSearch.BackColor = System.Drawing.Color.Transparent
        Me.grpSearch.Controls.Add(Me.Label18)
        Me.grpSearch.Controls.Add(Me.txtSupplierName)
        Me.grpSearch.Controls.Add(Me.btnReset)
        Me.grpSearch.Controls.Add(Me.btnSearchDetail)
        Me.grpSearch.Controls.Add(Me.txtPONo)
        Me.grpSearch.Controls.Add(Me.Label17)
        Me.grpSearch.Controls.Add(Me.Label16)
        Me.grpSearch.Controls.Add(Me.txtGRNNo)
        Me.grpSearch.Controls.Add(Me.Label15)
        Me.grpSearch.Controls.Add(Me.Label14)
        Me.grpSearch.Controls.Add(Me.dtpToDate)
        Me.grpSearch.Controls.Add(Me.dtpFromDate)
        Me.grpSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.grpSearch.Location = New System.Drawing.Point(0, 0)
        Me.grpSearch.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grpSearch.Name = "grpSearch"
        Me.grpSearch.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grpSearch.Size = New System.Drawing.Size(1303, 140)
        Me.grpSearch.TabIndex = 0
        Me.grpSearch.TabStop = False
        Me.grpSearch.Text = "Request Search"
        Me.grpSearch.Visible = False
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Location = New System.Drawing.Point(766, 31)
        Me.Label18.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(67, 20)
        Me.Label18.TabIndex = 8
        Me.Label18.Text = "Supplier"
        '
        'txtSupplierName
        '
        Me.txtSupplierName.Location = New System.Drawing.Point(771, 55)
        Me.txtSupplierName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtSupplierName.Name = "txtSupplierName"
        Me.txtSupplierName.Size = New System.Drawing.Size(180, 26)
        Me.txtSupplierName.TabIndex = 9
        '
        'btnReset
        '
        Me.btnReset.Location = New System.Drawing.Point(870, 95)
        Me.btnReset.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(82, 31)
        Me.btnReset.TabIndex = 11
        Me.btnReset.Text = "Reset"
        Me.btnReset.UseVisualStyleBackColor = True
        '
        'btnSearchDetail
        '
        Me.btnSearchDetail.Location = New System.Drawing.Point(771, 95)
        Me.btnSearchDetail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnSearchDetail.Name = "btnSearchDetail"
        Me.btnSearchDetail.Size = New System.Drawing.Size(82, 31)
        Me.btnSearchDetail.TabIndex = 10
        Me.btnSearchDetail.Text = "Search"
        Me.btnSearchDetail.UseVisualStyleBackColor = True
        '
        'txtPONo
        '
        Me.txtPONo.Location = New System.Drawing.Point(580, 55)
        Me.txtPONo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPONo.Name = "txtPONo"
        Me.txtPONo.Size = New System.Drawing.Size(180, 26)
        Me.txtPONo.TabIndex = 7
        '
        'Label17
        '
        Me.Label17.AutoSize = True
        Me.Label17.Location = New System.Drawing.Point(576, 31)
        Me.Label17.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(79, 20)
        Me.Label17.TabIndex = 6
        Me.Label17.Text = "PO/LC No"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Location = New System.Drawing.Point(386, 31)
        Me.Label16.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(69, 20)
        Me.Label16.TabIndex = 4
        Me.Label16.Text = "GRN No"
        '
        'txtGRNNo
        '
        Me.txtGRNNo.Location = New System.Drawing.Point(390, 55)
        Me.txtGRNNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtGRNNo.Name = "txtGRNNo"
        Me.txtGRNNo.Size = New System.Drawing.Size(180, 26)
        Me.txtGRNNo.TabIndex = 5
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(195, 31)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(66, 20)
        Me.Label15.TabIndex = 2
        Me.Label15.Text = "To Date"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(10, 31)
        Me.Label14.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(85, 20)
        Me.Label14.TabIndex = 0
        Me.Label14.Text = "From Date"
        '
        'dtpToDate
        '
        Me.dtpToDate.Checked = False
        Me.dtpToDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(200, 55)
        Me.dtpToDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.ShowCheckBox = True
        Me.dtpToDate.Size = New System.Drawing.Size(180, 26)
        Me.dtpToDate.TabIndex = 3
        '
        'dtpFromDate
        '
        Me.dtpFromDate.Checked = False
        Me.dtpFromDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(9, 55)
        Me.dtpFromDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.ShowCheckBox = True
        Me.dtpFromDate.Size = New System.Drawing.Size(180, 26)
        Me.dtpFromDate.TabIndex = 1
        '
        'ToolStrip2
        '
        Me.ToolStrip2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ToolStrip2.AutoSize = False
        Me.ToolStrip2.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip2.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.btnDelete, Me.btnPrintForm, Me.btnLoadAll, Me.btnSearch, Me.btnRefresh, Me.btnHelp})
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip2.Size = New System.Drawing.Size(1242, 38)
        Me.ToolStrip2.TabIndex = 0
        Me.ToolStrip2.Text = "ToolStrip2"
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
        Me.btnSave.Image = Global.SimpleAccounts.My.Resources.Resources.BtnSave_Image
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(77, 35)
        Me.btnSave.Text = "&Save"
        '
        'btnDelete
        '
        Me.btnDelete.Image = Global.SimpleAccounts.My.Resources.Resources.BtnDelete_Image
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(90, 35)
        Me.btnDelete.Text = "&Delete"
        '
        'btnPrintForm
        '
        Me.btnPrintForm.Image = Global.SimpleAccounts.My.Resources.Resources.print
        Me.btnPrintForm.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrintForm.Name = "btnPrintForm"
        Me.btnPrintForm.Size = New System.Drawing.Size(123, 35)
        Me.btnPrintForm.Text = "Print Form"
        '
        'btnLoadAll
        '
        Me.btnLoadAll.Image = Global.SimpleAccounts.My.Resources.Resources.sendcontactdetails
        Me.btnLoadAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnLoadAll.Name = "btnLoadAll"
        Me.btnLoadAll.Size = New System.Drawing.Size(104, 35)
        Me.btnLoadAll.Text = "Load All"
        '
        'btnSearch
        '
        Me.btnSearch.Image = Global.SimpleAccounts.My.Resources.Resources.search_32
        Me.btnSearch.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(92, 35)
        Me.btnSearch.Text = "Search"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 35)
        Me.btnRefresh.Text = "&Refresh"
        '
        'btnHelp
        '
        Me.btnHelp.Image = Global.SimpleAccounts.My.Resources.Resources.HelpToolStripButton_Image
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(77, 35)
        Me.btnHelp.Text = "&Help"
        '
        'miniToolStrip
        '
        Me.miniToolStrip.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.miniToolStrip.AutoSize = False
        Me.miniToolStrip.CanOverflow = False
        Me.miniToolStrip.Dock = System.Windows.Forms.DockStyle.None
        Me.miniToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.miniToolStrip.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.miniToolStrip.Location = New System.Drawing.Point(92, 3)
        Me.miniToolStrip.Name = "miniToolStrip"
        Me.miniToolStrip.Size = New System.Drawing.Size(924, 25)
        Me.miniToolStrip.TabIndex = 2
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Location = New System.Drawing.Point(2, 43)
        Me.UltraTabControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(1305, 958)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 0
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "Request"
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "History"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab2, UltraTab1})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.VisualStudio2005
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1303, 931)
        '
        'CtrlGrdBar2
        '
        Me.CtrlGrdBar2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar2.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar2.Email = Nothing
        Me.CtrlGrdBar2.FormName = Me
        Me.CtrlGrdBar2.Location = New System.Drawing.Point(1246, 0)
        Me.CtrlGrdBar2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar2.MyGrid = Me.grdSaved
        Me.CtrlGrdBar2.Name = "CtrlGrdBar2"
        Me.CtrlGrdBar2.Size = New System.Drawing.Size(57, 38)
        Me.CtrlGrdBar2.TabIndex = 1
        '
        'frmLabTestRequest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1305, 1002)
        Me.Controls.Add(Me.CtrlGrdBar2)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.ToolStrip2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmLabTestRequest"
        Me.Text = "Lab Test Request"
        Me.UltraTabPageControl2.ResumeLayout(False)
        Me.UltraTabPageControl2.PerformLayout()
        CType(Me.cmbSupplier, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.cmbItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdLabRequest, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.UltraTabPageControl1.ResumeLayout(False)
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpSearch.ResumeLayout(False)
        Me.grpSearch.PerformLayout()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents miniToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents dtpRequestDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblLabTestRequest As System.Windows.Forms.Label
    Friend WithEvents grdLabRequest As Janus.Windows.GridEX.GridEX
    Friend WithEvents cmbDepartment As System.Windows.Forms.ComboBox
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtRequestNo As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ToolStrip2 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbGRNNo As System.Windows.Forms.ComboBox
    Friend WithEvents BackgroundWorker2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cmbLCNo As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cmbPONo As System.Windows.Forms.ComboBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents grpSearch As System.Windows.Forms.GroupBox
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents btnSearchDetail As System.Windows.Forms.Button
    Friend WithEvents txtPONo As System.Windows.Forms.TextBox
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents txtGRNNo As System.Windows.Forms.TextBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtSupplierName As System.Windows.Forms.TextBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents cmbStage As System.Windows.Forms.ComboBox
    Friend WithEvents txtBatchNo As System.Windows.Forms.TextBox
    Friend WithEvents txtNoofContainers As System.Windows.Forms.TextBox
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbSupplier As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents cmbItem As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents grdSaved As Janus.Windows.GridEX.GridEX
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents btnPrintForm As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSearch As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnLoadAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rbCode As System.Windows.Forms.RadioButton
    Friend WithEvents rbName As System.Windows.Forms.RadioButton
    Friend WithEvents CtrlGrdBar2 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents Label22 As System.Windows.Forms.Label
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents dtpRetestDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpExpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpMfgDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label23 As System.Windows.Forms.Label
    Friend WithEvents cmbTicket As System.Windows.Forms.ComboBox
    Friend WithEvents Label24 As System.Windows.Forms.Label
    Friend WithEvents cmbPlan As System.Windows.Forms.ComboBox
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents txtStage As System.Windows.Forms.TextBox
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents cmbEmployee As System.Windows.Forms.ComboBox
    Friend WithEvents lblBy As System.Windows.Forms.Label

End Class

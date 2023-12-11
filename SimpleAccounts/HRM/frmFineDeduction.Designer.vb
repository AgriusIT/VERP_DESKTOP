<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFineDeduction
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
        Dim Appearance36 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance37 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn25 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("coa_detail_id")
        Dim UltraGridColumn26 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("detail_title")
        Dim UltraGridColumn27 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("detail_code")
        Dim UltraGridColumn28 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("account_type")
        Dim UltraGridColumn29 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("sub_sub_title")
        Dim UltraGridColumn30 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("sub_title")
        Dim UltraGridColumn31 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("main_title")
        Dim UltraGridColumn32 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("main_type")
        Dim Appearance38 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance39 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance40 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance41 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmFineDeduction))
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.pnlFineDetails = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.txtReason = New System.Windows.Forms.TextBox()
        Me.lblReason = New System.Windows.Forms.Label()
        Me.cmbEmployee = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.cmbDeductionType = New System.Windows.Forms.ComboBox()
        Me.dtpDocDate = New System.Windows.Forms.DateTimePicker()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.txtDocNo = New System.Windows.Forms.TextBox()
        Me.lblAmount = New System.Windows.Forms.Label()
        Me.lblDeductionType = New System.Windows.Forms.Label()
        Me.lblEmpName = New System.Windows.Forms.Label()
        Me.lblDocDate = New System.Windows.Forms.Label()
        Me.lblDocNo = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdSaved = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnHelp = New System.Windows.Forms.ToolStripButton()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.UltraTabPageControl1.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlFineDetails.SuspendLayout()
        CType(Me.cmbEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
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
        Me.UltraTabPageControl1.Controls.Add(Me.pnlFineDetails)
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(747, 420)
        '
        'grd
        '
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.GroupByBoxVisible = False
        Me.grd.Location = New System.Drawing.Point(0, 203)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(747, 217)
        Me.grd.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.grd, "Record")
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'pnlFineDetails
        '
        Me.pnlFineDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlFineDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.pnlFineDetails.Controls.Add(Me.Label1)
        Me.pnlFineDetails.Controls.Add(Me.btnAdd)
        Me.pnlFineDetails.Controls.Add(Me.txtReason)
        Me.pnlFineDetails.Controls.Add(Me.lblReason)
        Me.pnlFineDetails.Controls.Add(Me.cmbEmployee)
        Me.pnlFineDetails.Controls.Add(Me.cmbDeductionType)
        Me.pnlFineDetails.Controls.Add(Me.dtpDocDate)
        Me.pnlFineDetails.Controls.Add(Me.txtAmount)
        Me.pnlFineDetails.Controls.Add(Me.txtDocNo)
        Me.pnlFineDetails.Controls.Add(Me.lblAmount)
        Me.pnlFineDetails.Controls.Add(Me.lblDeductionType)
        Me.pnlFineDetails.Controls.Add(Me.lblEmpName)
        Me.pnlFineDetails.Controls.Add(Me.lblDocDate)
        Me.pnlFineDetails.Controls.Add(Me.lblDocNo)
        Me.pnlFineDetails.Location = New System.Drawing.Point(11, 56)
        Me.pnlFineDetails.Name = "pnlFineDetails"
        Me.pnlFineDetails.Size = New System.Drawing.Size(725, 141)
        Me.pnlFineDetails.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(48, 90)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(13, 17)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "*"
        '
        'btnAdd
        '
        Me.btnAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Location = New System.Drawing.Point(420, 105)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(51, 23)
        Me.btnAdd.TabIndex = 13
        Me.btnAdd.Text = "+"
        Me.ToolTip1.SetToolTip(Me.btnAdd, "Add Item To Grid")
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'txtReason
        '
        Me.txtReason.Location = New System.Drawing.Point(11, 107)
        Me.txtReason.Multiline = True
        Me.txtReason.Name = "txtReason"
        Me.txtReason.Size = New System.Drawing.Size(406, 20)
        Me.txtReason.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.txtReason, "Enter a valid reason")
        '
        'lblReason
        '
        Me.lblReason.AutoSize = True
        Me.lblReason.Location = New System.Drawing.Point(8, 91)
        Me.lblReason.Name = "lblReason"
        Me.lblReason.Size = New System.Drawing.Size(44, 13)
        Me.lblReason.TabIndex = 10
        Me.lblReason.Text = "Reason"
        Me.ToolTip1.SetToolTip(Me.lblReason, "Reason")
        '
        'cmbEmployee
        '
        Appearance36.BackColor = System.Drawing.SystemColors.Info
        Me.cmbEmployee.Appearance = Appearance36
        Me.cmbEmployee.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cmbEmployee.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbEmployee.CheckedListSettings.CheckStateMember = ""
        Appearance37.BackColor = System.Drawing.Color.White
        Me.cmbEmployee.DisplayLayout.Appearance = Appearance37
        UltraGridColumn25.Header.Caption = "ID"
        UltraGridColumn25.Header.VisiblePosition = 0
        UltraGridColumn25.Hidden = True
        UltraGridColumn25.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(8, 0)
        UltraGridColumn26.Header.Caption = "Employee Name"
        UltraGridColumn26.Header.VisiblePosition = 1
        UltraGridColumn26.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(128, 0)
        UltraGridColumn27.Header.Caption = "Code"
        UltraGridColumn27.Header.VisiblePosition = 2
        UltraGridColumn28.Header.Caption = "Designation"
        UltraGridColumn28.Header.VisiblePosition = 3
        UltraGridColumn28.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(96, 0)
        UltraGridColumn29.Header.Caption = "Department"
        UltraGridColumn29.Header.VisiblePosition = 4
        UltraGridColumn29.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(106, 0)
        UltraGridColumn30.Header.Caption = "Sub Ac"
        UltraGridColumn30.Header.VisiblePosition = 5
        UltraGridColumn30.Hidden = True
        UltraGridColumn30.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(102, 0)
        UltraGridColumn31.Header.Caption = "Main Ac"
        UltraGridColumn31.Header.VisiblePosition = 6
        UltraGridColumn31.Hidden = True
        UltraGridColumn31.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(82, 0)
        UltraGridColumn32.Header.Caption = "Ac Head"
        UltraGridColumn32.Header.VisiblePosition = 7
        UltraGridColumn32.Hidden = True
        UltraGridColumn32.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(84, 0)
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn25, UltraGridColumn26, UltraGridColumn27, UltraGridColumn28, UltraGridColumn29, UltraGridColumn30, UltraGridColumn31, UltraGridColumn32})
        Me.cmbEmployee.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbEmployee.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbEmployee.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbEmployee.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Appearance38.BackColor = System.Drawing.Color.Transparent
        Me.cmbEmployee.DisplayLayout.Override.CardAreaAppearance = Appearance38
        Me.cmbEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbEmployee.DisplayLayout.Override.CellPadding = 3
        Me.cmbEmployee.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance39.TextHAlignAsString = "Left"
        Me.cmbEmployee.DisplayLayout.Override.HeaderAppearance = Appearance39
        Me.cmbEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance40.BorderColor = System.Drawing.Color.LightGray
        Appearance40.TextVAlignAsString = "Middle"
        Me.cmbEmployee.DisplayLayout.Override.RowAppearance = Appearance40
        Appearance41.BackColor = System.Drawing.Color.LightSteelBlue
        Appearance41.BorderColor = System.Drawing.Color.Black
        Appearance41.ForeColor = System.Drawing.Color.Black
        Me.cmbEmployee.DisplayLayout.Override.SelectedRowAppearance = Appearance41
        Me.cmbEmployee.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbEmployee.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbEmployee.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbEmployee.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.cmbEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbEmployee.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbEmployee.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbEmployee.LimitToList = True
        Me.cmbEmployee.Location = New System.Drawing.Point(11, 66)
        Me.cmbEmployee.MaxDropDownItems = 16
        Me.cmbEmployee.Name = "cmbEmployee"
        Me.cmbEmployee.Size = New System.Drawing.Size(219, 22)
        Me.cmbEmployee.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.cmbEmployee, "Select any employee")
        '
        'cmbDeductionType
        '
        Me.cmbDeductionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDeductionType.FormattingEnabled = True
        Me.cmbDeductionType.Location = New System.Drawing.Point(236, 67)
        Me.cmbDeductionType.Name = "cmbDeductionType"
        Me.cmbDeductionType.Size = New System.Drawing.Size(132, 21)
        Me.cmbDeductionType.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.cmbDeductionType, "Select deduction type")
        '
        'dtpDocDate
        '
        Me.dtpDocDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpDocDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDocDate.Location = New System.Drawing.Point(122, 27)
        Me.dtpDocDate.Name = "dtpDocDate"
        Me.dtpDocDate.Size = New System.Drawing.Size(108, 20)
        Me.dtpDocDate.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.dtpDocDate, "Deduction Date")
        '
        'txtAmount
        '
        Me.txtAmount.Location = New System.Drawing.Point(374, 68)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(97, 20)
        Me.txtAmount.TabIndex = 9
        Me.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.ToolTip1.SetToolTip(Me.txtAmount, "Enter amount")
        '
        'txtDocNo
        '
        Me.txtDocNo.BackColor = System.Drawing.SystemColors.Control
        Me.txtDocNo.Enabled = False
        Me.txtDocNo.Location = New System.Drawing.Point(11, 27)
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(102, 20)
        Me.txtDocNo.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtDocNo, "Deduction No")
        '
        'lblAmount
        '
        Me.lblAmount.AutoSize = True
        Me.lblAmount.Location = New System.Drawing.Point(374, 50)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(43, 13)
        Me.lblAmount.TabIndex = 8
        Me.lblAmount.Text = "Amount"
        Me.ToolTip1.SetToolTip(Me.lblAmount, "Amount")
        '
        'lblDeductionType
        '
        Me.lblDeductionType.AutoSize = True
        Me.lblDeductionType.Location = New System.Drawing.Point(233, 51)
        Me.lblDeductionType.Name = "lblDeductionType"
        Me.lblDeductionType.Size = New System.Drawing.Size(83, 13)
        Me.lblDeductionType.TabIndex = 6
        Me.lblDeductionType.Text = "Deduction Type"
        Me.ToolTip1.SetToolTip(Me.lblDeductionType, "Deduction Type")
        '
        'lblEmpName
        '
        Me.lblEmpName.AutoSize = True
        Me.lblEmpName.Location = New System.Drawing.Point(8, 50)
        Me.lblEmpName.Name = "lblEmpName"
        Me.lblEmpName.Size = New System.Drawing.Size(84, 13)
        Me.lblEmpName.TabIndex = 4
        Me.lblEmpName.Text = "Employee Name"
        Me.ToolTip1.SetToolTip(Me.lblEmpName, "Employee Name")
        '
        'lblDocDate
        '
        Me.lblDocDate.AutoSize = True
        Me.lblDocDate.Location = New System.Drawing.Point(119, 11)
        Me.lblDocDate.Name = "lblDocDate"
        Me.lblDocDate.Size = New System.Drawing.Size(53, 13)
        Me.lblDocDate.TabIndex = 2
        Me.lblDocDate.Text = "Doc Date"
        Me.ToolTip1.SetToolTip(Me.lblDocDate, "Document Date")
        '
        'lblDocNo
        '
        Me.lblDocNo.AutoSize = True
        Me.lblDocNo.Location = New System.Drawing.Point(8, 11)
        Me.lblDocNo.Name = "lblDocNo"
        Me.lblDocNo.Size = New System.Drawing.Size(44, 13)
        Me.lblDocNo.TabIndex = 0
        Me.lblDocNo.Text = "Doc No"
        Me.ToolTip1.SetToolTip(Me.lblDocNo, "Document No")
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblProgress)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(747, 50)
        Me.pnlHeader.TabIndex = 0
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(373, 0)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(266, 50)
        Me.lblProgress.TabIndex = 1
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold)
        Me.lblHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.lblHeader.Location = New System.Drawing.Point(8, 10)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(284, 23)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Employee Fine Deduction"
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grdSaved)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(747, 420)
        '
        'grdSaved
        '
        Me.grdSaved.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdSaved.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSaved.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdSaved.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdSaved.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdSaved.GroupByBoxVisible = False
        Me.grdSaved.Location = New System.Drawing.Point(0, 0)
        Me.grdSaved.Name = "grdSaved"
        Me.grdSaved.RecordNavigator = True
        Me.grdSaved.Size = New System.Drawing.Size(747, 420)
        Me.grdSaved.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.grdSaved, "History")
        Me.grdSaved.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdSaved.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.btnDelete, Me.btnRefresh, Me.btnPrint, Me.toolStripSeparator1, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(749, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(51, 22)
        Me.btnNew.Text = "&New"
        '
        'btnEdit
        '
        Me.btnEdit.Image = CType(resources.GetObject("btnEdit.Image"), System.Drawing.Image)
        Me.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(47, 22)
        Me.btnEdit.Text = "&Edit"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(51, 22)
        Me.btnSave.Text = "&Save"
        '
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.RightToLeftAutoMirrorImage = True
        Me.btnDelete.Size = New System.Drawing.Size(60, 22)
        Me.btnDelete.Text = "&Delete"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(66, 22)
        Me.btnRefresh.Text = "&Refresh"
        '
        'btnPrint
        '
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(52, 22)
        Me.btnPrint.Text = "&Print"
        Me.btnPrint.Visible = False
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnHelp
        '
        Me.btnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(23, 22)
        Me.btnHelp.Text = "He&lp"
        Me.btnHelp.Visible = False
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 25)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(749, 441)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 9
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Fine Deduction"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "History"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(747, 420)
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(716, 0)
        Me.CtrlGrdBar1.MyGrid = Me.grd
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(34, 25)
        Me.CtrlGrdBar1.TabIndex = 1
        '
        'frmFineDeduction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(749, 466)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "frmFineDeduction"
        Me.Text = "Employee Fine Deduction"
        Me.UltraTabPageControl1.ResumeLayout(False)
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlFineDetails.ResumeLayout(False)
        Me.pnlFineDetails.PerformLayout()
        CType(Me.cmbEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grdSaved As Janus.Windows.GridEX.GridEX
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents pnlFineDetails As System.Windows.Forms.Panel
    Friend WithEvents lblAmount As System.Windows.Forms.Label
    Friend WithEvents lblDeductionType As System.Windows.Forms.Label
    Friend WithEvents lblEmpName As System.Windows.Forms.Label
    Friend WithEvents lblDocDate As System.Windows.Forms.Label
    Friend WithEvents lblDocNo As System.Windows.Forms.Label
    Friend WithEvents dtpDocDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtDocNo As System.Windows.Forms.TextBox
    Friend WithEvents cmbDeductionType As System.Windows.Forms.ComboBox
    Friend WithEvents cmbEmployee As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents lblReason As System.Windows.Forms.Label
    Friend WithEvents txtReason As System.Windows.Forms.TextBox
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnAdd As System.Windows.Forms.Button
End Class

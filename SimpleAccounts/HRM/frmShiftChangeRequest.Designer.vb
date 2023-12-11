<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmShiftChangeRequest
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
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn1 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("coa_detail_id")
        Dim UltraGridColumn2 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("detail_title")
        Dim UltraGridColumn3 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("detail_code")
        Dim UltraGridColumn4 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("account_type")
        Dim UltraGridColumn5 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("sub_sub_title")
        Dim UltraGridColumn6 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("sub_title")
        Dim UltraGridColumn7 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("main_title")
        Dim UltraGridColumn8 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("main_type")
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmShiftChangeRequest))
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.lblEmpName = New System.Windows.Forms.Label()
        Me.lblComents = New System.Windows.Forms.Label()
        Me.lblCurrentShift = New System.Windows.Forms.Label()
        Me.txtComments = New System.Windows.Forms.TextBox()
        Me.lblNewShift = New System.Windows.Forms.Label()
        Me.cmbNewCostCenter = New System.Windows.Forms.ComboBox()
        Me.cmbEmployee = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.lblNewCostCenter = New System.Windows.Forms.Label()
        Me.cmbCurrentShift = New System.Windows.Forms.ComboBox()
        Me.cmbCurrentCostCenter = New System.Windows.Forms.ComboBox()
        Me.cmbNewShift = New System.Windows.Forms.ComboBox()
        Me.lblCurrentCostCenter = New System.Windows.Forms.Label()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.pnlTerminateDetails = New System.Windows.Forms.Panel()
        Me.lblRemarks = New System.Windows.Forms.Label()
        Me.cmbRequestType = New System.Windows.Forms.ComboBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.lblRequestType = New System.Windows.Forms.Label()
        Me.dtpDocDate = New System.Windows.Forms.DateTimePicker()
        Me.txtDocNo = New System.Windows.Forms.TextBox()
        Me.lblDocumentDate = New System.Windows.Forms.Label()
        Me.lblDocumentNo = New System.Windows.Forms.Label()
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
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.Panel6.SuspendLayout()
        CType(Me.cmbEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.pnlTerminateDetails.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.Panel6)
        Me.UltraTabPageControl1.Controls.Add(Me.grd)
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.pnlTerminateDetails)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(794, 478)
        '
        'Panel6
        '
        Me.Panel6.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel6.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel6.Controls.Add(Me.btnAdd)
        Me.Panel6.Controls.Add(Me.lblEmpName)
        Me.Panel6.Controls.Add(Me.lblComents)
        Me.Panel6.Controls.Add(Me.lblCurrentShift)
        Me.Panel6.Controls.Add(Me.txtComments)
        Me.Panel6.Controls.Add(Me.lblNewShift)
        Me.Panel6.Controls.Add(Me.cmbNewCostCenter)
        Me.Panel6.Controls.Add(Me.cmbEmployee)
        Me.Panel6.Controls.Add(Me.lblNewCostCenter)
        Me.Panel6.Controls.Add(Me.cmbCurrentShift)
        Me.Panel6.Controls.Add(Me.cmbCurrentCostCenter)
        Me.Panel6.Controls.Add(Me.cmbNewShift)
        Me.Panel6.Controls.Add(Me.lblCurrentCostCenter)
        Me.Panel6.Location = New System.Drawing.Point(11, 158)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(772, 140)
        Me.Panel6.TabIndex = 8
        '
        'btnAdd
        '
        Me.btnAdd.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAdd.Location = New System.Drawing.Point(489, 106)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(48, 21)
        Me.btnAdd.TabIndex = 37
        Me.btnAdd.Text = "+"
        Me.ToolTip1.SetToolTip(Me.btnAdd, "Add")
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'lblEmpName
        '
        Me.lblEmpName.AutoSize = True
        Me.lblEmpName.Location = New System.Drawing.Point(10, 10)
        Me.lblEmpName.Name = "lblEmpName"
        Me.lblEmpName.Size = New System.Drawing.Size(53, 13)
        Me.lblEmpName.TabIndex = 6
        Me.lblEmpName.Text = "Employee"
        Me.ToolTip1.SetToolTip(Me.lblEmpName, "Employee")
        '
        'lblComents
        '
        Me.lblComents.AutoSize = True
        Me.lblComents.Location = New System.Drawing.Point(12, 91)
        Me.lblComents.Name = "lblComents"
        Me.lblComents.Size = New System.Drawing.Size(56, 13)
        Me.lblComents.TabIndex = 35
        Me.lblComents.Text = "Comments"
        Me.ToolTip1.SetToolTip(Me.lblComents, "comments")
        '
        'lblCurrentShift
        '
        Me.lblCurrentShift.AutoSize = True
        Me.lblCurrentShift.Location = New System.Drawing.Point(229, 10)
        Me.lblCurrentShift.Name = "lblCurrentShift"
        Me.lblCurrentShift.Size = New System.Drawing.Size(65, 13)
        Me.lblCurrentShift.TabIndex = 8
        Me.lblCurrentShift.Text = "Current Shift"
        Me.ToolTip1.SetToolTip(Me.lblCurrentShift, "Employee current shift")
        '
        'txtComments
        '
        Me.txtComments.Location = New System.Drawing.Point(11, 107)
        Me.txtComments.Name = "txtComments"
        Me.txtComments.Size = New System.Drawing.Size(472, 20)
        Me.txtComments.TabIndex = 36
        Me.ToolTip1.SetToolTip(Me.txtComments, "write comments")
        '
        'lblNewShift
        '
        Me.lblNewShift.AutoSize = True
        Me.lblNewShift.Location = New System.Drawing.Point(359, 10)
        Me.lblNewShift.Name = "lblNewShift"
        Me.lblNewShift.Size = New System.Drawing.Size(53, 13)
        Me.lblNewShift.TabIndex = 10
        Me.lblNewShift.Text = "New Shift"
        Me.ToolTip1.SetToolTip(Me.lblNewShift, "New shift")
        '
        'cmbNewCostCenter
        '
        Me.cmbNewCostCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNewCostCenter.FormattingEnabled = True
        Me.cmbNewCostCenter.Location = New System.Drawing.Point(232, 67)
        Me.cmbNewCostCenter.Name = "cmbNewCostCenter"
        Me.cmbNewCostCenter.Size = New System.Drawing.Size(251, 21)
        Me.cmbNewCostCenter.TabIndex = 34
        Me.ToolTip1.SetToolTip(Me.cmbNewCostCenter, "Select new cost center")
        '
        'cmbEmployee
        '
        Appearance7.BackColor = System.Drawing.SystemColors.Info
        Me.cmbEmployee.Appearance = Appearance7
        Me.cmbEmployee.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cmbEmployee.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbEmployee.CheckedListSettings.CheckStateMember = ""
        Appearance8.BackColor = System.Drawing.Color.White
        Me.cmbEmployee.DisplayLayout.Appearance = Appearance8
        UltraGridColumn1.Header.Caption = "ID"
        UltraGridColumn1.Header.VisiblePosition = 0
        UltraGridColumn1.Hidden = True
        UltraGridColumn1.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(8, 0)
        UltraGridColumn2.Header.Caption = "Employee Name"
        UltraGridColumn2.Header.VisiblePosition = 1
        UltraGridColumn2.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(128, 0)
        UltraGridColumn3.Header.Caption = "Code"
        UltraGridColumn3.Header.VisiblePosition = 2
        UltraGridColumn4.Header.Caption = "Designation"
        UltraGridColumn4.Header.VisiblePosition = 3
        UltraGridColumn4.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(96, 0)
        UltraGridColumn5.Header.Caption = "Department"
        UltraGridColumn5.Header.VisiblePosition = 4
        UltraGridColumn5.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(106, 0)
        UltraGridColumn6.Header.Caption = "Sub Ac"
        UltraGridColumn6.Header.VisiblePosition = 5
        UltraGridColumn6.Hidden = True
        UltraGridColumn6.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(102, 0)
        UltraGridColumn7.Header.Caption = "Main Ac"
        UltraGridColumn7.Header.VisiblePosition = 6
        UltraGridColumn7.Hidden = True
        UltraGridColumn7.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(82, 0)
        UltraGridColumn8.Header.Caption = "Ac Head"
        UltraGridColumn8.Header.VisiblePosition = 7
        UltraGridColumn8.Hidden = True
        UltraGridColumn8.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(84, 0)
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn1, UltraGridColumn2, UltraGridColumn3, UltraGridColumn4, UltraGridColumn5, UltraGridColumn6, UltraGridColumn7, UltraGridColumn8})
        Me.cmbEmployee.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbEmployee.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbEmployee.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbEmployee.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbEmployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Appearance9.BackColor = System.Drawing.Color.Transparent
        Me.cmbEmployee.DisplayLayout.Override.CardAreaAppearance = Appearance9
        Me.cmbEmployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbEmployee.DisplayLayout.Override.CellPadding = 3
        Me.cmbEmployee.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance10.TextHAlignAsString = "Left"
        Me.cmbEmployee.DisplayLayout.Override.HeaderAppearance = Appearance10
        Me.cmbEmployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance11.BorderColor = System.Drawing.Color.LightGray
        Appearance11.TextVAlignAsString = "Middle"
        Me.cmbEmployee.DisplayLayout.Override.RowAppearance = Appearance11
        Appearance12.BackColor = System.Drawing.Color.LightSteelBlue
        Appearance12.BorderColor = System.Drawing.Color.Black
        Appearance12.ForeColor = System.Drawing.Color.Black
        Me.cmbEmployee.DisplayLayout.Override.SelectedRowAppearance = Appearance12
        Me.cmbEmployee.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbEmployee.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbEmployee.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbEmployee.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.cmbEmployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbEmployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbEmployee.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbEmployee.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbEmployee.LimitToList = True
        Me.cmbEmployee.Location = New System.Drawing.Point(13, 26)
        Me.cmbEmployee.MaxDropDownItems = 16
        Me.cmbEmployee.Name = "cmbEmployee"
        Me.cmbEmployee.Size = New System.Drawing.Size(210, 22)
        Me.cmbEmployee.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.cmbEmployee, "Select Any Employee")
        '
        'lblNewCostCenter
        '
        Me.lblNewCostCenter.AutoSize = True
        Me.lblNewCostCenter.Location = New System.Drawing.Point(229, 51)
        Me.lblNewCostCenter.Name = "lblNewCostCenter"
        Me.lblNewCostCenter.Size = New System.Drawing.Size(87, 13)
        Me.lblNewCostCenter.TabIndex = 33
        Me.lblNewCostCenter.Text = "New Cost Center"
        Me.ToolTip1.SetToolTip(Me.lblNewCostCenter, "New cost center")
        '
        'cmbCurrentShift
        '
        Me.cmbCurrentShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCurrentShift.FormattingEnabled = True
        Me.cmbCurrentShift.Location = New System.Drawing.Point(232, 27)
        Me.cmbCurrentShift.Name = "cmbCurrentShift"
        Me.cmbCurrentShift.Size = New System.Drawing.Size(121, 21)
        Me.cmbCurrentShift.TabIndex = 27
        Me.ToolTip1.SetToolTip(Me.cmbCurrentShift, "Select employee current shift")
        '
        'cmbCurrentCostCenter
        '
        Me.cmbCurrentCostCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCurrentCostCenter.FormattingEnabled = True
        Me.cmbCurrentCostCenter.Location = New System.Drawing.Point(11, 67)
        Me.cmbCurrentCostCenter.Name = "cmbCurrentCostCenter"
        Me.cmbCurrentCostCenter.Size = New System.Drawing.Size(212, 21)
        Me.cmbCurrentCostCenter.TabIndex = 32
        Me.ToolTip1.SetToolTip(Me.cmbCurrentCostCenter, "Current cost center")
        '
        'cmbNewShift
        '
        Me.cmbNewShift.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNewShift.FormattingEnabled = True
        Me.cmbNewShift.Location = New System.Drawing.Point(362, 27)
        Me.cmbNewShift.Name = "cmbNewShift"
        Me.cmbNewShift.Size = New System.Drawing.Size(121, 21)
        Me.cmbNewShift.TabIndex = 28
        Me.ToolTip1.SetToolTip(Me.cmbNewShift, "Select employee new shift")
        '
        'lblCurrentCostCenter
        '
        Me.lblCurrentCostCenter.AutoSize = True
        Me.lblCurrentCostCenter.Location = New System.Drawing.Point(12, 51)
        Me.lblCurrentCostCenter.Name = "lblCurrentCostCenter"
        Me.lblCurrentCostCenter.Size = New System.Drawing.Size(99, 13)
        Me.lblCurrentCostCenter.TabIndex = 31
        Me.lblCurrentCostCenter.Text = "Current Cost Center"
        Me.ToolTip1.SetToolTip(Me.lblCurrentCostCenter, "Current cost center")
        '
        'grd
        '
        Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.GroupByBoxVisible = False
        Me.grd.Location = New System.Drawing.Point(0, 304)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(794, 172)
        Me.grd.TabIndex = 7
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblProgress)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(794, 47)
        Me.pnlHeader.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.pnlHeader, "TItle")
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
        Me.lblHeader.Size = New System.Drawing.Size(316, 23)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Shift Group Change Request"
        '
        'pnlTerminateDetails
        '
        Me.pnlTerminateDetails.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlTerminateDetails.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.pnlTerminateDetails.Controls.Add(Me.lblRemarks)
        Me.pnlTerminateDetails.Controls.Add(Me.cmbRequestType)
        Me.pnlTerminateDetails.Controls.Add(Me.txtRemarks)
        Me.pnlTerminateDetails.Controls.Add(Me.lblRequestType)
        Me.pnlTerminateDetails.Controls.Add(Me.dtpDocDate)
        Me.pnlTerminateDetails.Controls.Add(Me.txtDocNo)
        Me.pnlTerminateDetails.Controls.Add(Me.lblDocumentDate)
        Me.pnlTerminateDetails.Controls.Add(Me.lblDocumentNo)
        Me.pnlTerminateDetails.Location = New System.Drawing.Point(11, 53)
        Me.pnlTerminateDetails.Name = "pnlTerminateDetails"
        Me.pnlTerminateDetails.Size = New System.Drawing.Size(772, 99)
        Me.pnlTerminateDetails.TabIndex = 5
        '
        'lblRemarks
        '
        Me.lblRemarks.AutoSize = True
        Me.lblRemarks.Location = New System.Drawing.Point(10, 50)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(49, 13)
        Me.lblRemarks.TabIndex = 38
        Me.lblRemarks.Text = "Remarks"
        Me.ToolTip1.SetToolTip(Me.lblRemarks, "Remarks")
        '
        'cmbRequestType
        '
        Me.cmbRequestType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbRequestType.FormattingEnabled = True
        Me.cmbRequestType.Location = New System.Drawing.Point(232, 27)
        Me.cmbRequestType.Name = "cmbRequestType"
        Me.cmbRequestType.Size = New System.Drawing.Size(251, 21)
        Me.cmbRequestType.TabIndex = 26
        Me.ToolTip1.SetToolTip(Me.cmbRequestType, "Select Request Type")
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(11, 66)
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(472, 20)
        Me.txtRemarks.TabIndex = 39
        Me.ToolTip1.SetToolTip(Me.txtRemarks, "Enter Remarks")
        '
        'lblRequestType
        '
        Me.lblRequestType.AutoSize = True
        Me.lblRequestType.Location = New System.Drawing.Point(229, 11)
        Me.lblRequestType.Name = "lblRequestType"
        Me.lblRequestType.Size = New System.Drawing.Size(74, 13)
        Me.lblRequestType.TabIndex = 25
        Me.lblRequestType.Text = "Request Type"
        Me.ToolTip1.SetToolTip(Me.lblRequestType, "Request type")
        '
        'dtpDocDate
        '
        Me.dtpDocDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpDocDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDocDate.Location = New System.Drawing.Point(116, 27)
        Me.dtpDocDate.Name = "dtpDocDate"
        Me.dtpDocDate.Size = New System.Drawing.Size(107, 20)
        Me.dtpDocDate.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.dtpDocDate, "Request Date")
        '
        'txtDocNo
        '
        Me.txtDocNo.BackColor = System.Drawing.SystemColors.Control
        Me.txtDocNo.Enabled = False
        Me.txtDocNo.Location = New System.Drawing.Point(11, 27)
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(92, 20)
        Me.txtDocNo.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtDocNo, "Request No")
        '
        'lblDocumentDate
        '
        Me.lblDocumentDate.AutoSize = True
        Me.lblDocumentDate.Location = New System.Drawing.Point(113, 11)
        Me.lblDocumentDate.Name = "lblDocumentDate"
        Me.lblDocumentDate.Size = New System.Drawing.Size(73, 13)
        Me.lblDocumentDate.TabIndex = 2
        Me.lblDocumentDate.Text = "Request Date"
        Me.ToolTip1.SetToolTip(Me.lblDocumentDate, "Request date")
        '
        'lblDocumentNo
        '
        Me.lblDocumentNo.AutoSize = True
        Me.lblDocumentNo.Location = New System.Drawing.Point(10, 11)
        Me.lblDocumentNo.Name = "lblDocumentNo"
        Me.lblDocumentNo.Size = New System.Drawing.Size(64, 13)
        Me.lblDocumentNo.TabIndex = 0
        Me.lblDocumentNo.Text = "Request No"
        Me.ToolTip1.SetToolTip(Me.lblDocumentNo, "Request No")
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grdSaved)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(794, 478)
        '
        'grdSaved
        '
        Me.grdSaved.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdSaved.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSaved.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdSaved.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdSaved.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdSaved.Location = New System.Drawing.Point(0, 0)
        Me.grdSaved.Name = "grdSaved"
        Me.grdSaved.RecordNavigator = True
        Me.grdSaved.Size = New System.Drawing.Size(794, 478)
        Me.grdSaved.TabIndex = 0
        Me.grdSaved.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdSaved.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.btnDelete, Me.btnRefresh, Me.btnPrint, Me.toolStripSeparator1, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(796, 25)
        Me.ToolStrip1.TabIndex = 1
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
        Me.UltraTabControl1.Size = New System.Drawing.Size(796, 499)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 4
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Detail"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "History"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(794, 478)
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(758, -2)
        Me.CtrlGrdBar1.MyGrid = Me.grdSaved
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(37, 25)
        Me.CtrlGrdBar1.TabIndex = 5
        '
        'frmShiftChangeRequest
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(796, 524)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "frmShiftChangeRequest"
        Me.Text = "Employee Shift Change Request"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel6.PerformLayout()
        CType(Me.cmbEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlTerminateDetails.ResumeLayout(False)
        Me.pnlTerminateDetails.PerformLayout()
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
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grdSaved As Janus.Windows.GridEX.GridEX
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents pnlTerminateDetails As System.Windows.Forms.Panel
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents lblComents As System.Windows.Forms.Label
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents cmbNewCostCenter As System.Windows.Forms.ComboBox
    Friend WithEvents lblNewCostCenter As System.Windows.Forms.Label
    Friend WithEvents cmbCurrentCostCenter As System.Windows.Forms.ComboBox
    Friend WithEvents lblCurrentCostCenter As System.Windows.Forms.Label
    Friend WithEvents cmbNewShift As System.Windows.Forms.ComboBox
    Friend WithEvents cmbCurrentShift As System.Windows.Forms.ComboBox
    Friend WithEvents cmbRequestType As System.Windows.Forms.ComboBox
    Friend WithEvents lblRequestType As System.Windows.Forms.Label
    Friend WithEvents cmbEmployee As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents dtpDocDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtDocNo As System.Windows.Forms.TextBox
    Friend WithEvents lblNewShift As System.Windows.Forms.Label
    Friend WithEvents lblCurrentShift As System.Windows.Forms.Label
    Friend WithEvents lblEmpName As System.Windows.Forms.Label
    Friend WithEvents lblDocumentDate As System.Windows.Forms.Label
    Friend WithEvents lblDocumentNo As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents lblRemarks As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
End Class

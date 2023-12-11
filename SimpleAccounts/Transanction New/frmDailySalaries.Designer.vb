<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDailySalaries
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
        Dim grdDetail_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDailySalaries))
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtChargeType = New System.Windows.Forms.TextBox()
        Me.lblLabourType = New System.Windows.Forms.Label()
        Me.cmbLabourType = New System.Windows.Forms.ComboBox()
        Me.lblStages = New System.Windows.Forms.Label()
        Me.cmbStages = New System.Windows.Forms.ComboBox()
        Me.lblcostcenter = New System.Windows.Forms.Label()
        Me.lblemployee = New System.Windows.Forms.Label()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.cmbcostcenter = New System.Windows.Forms.ComboBox()
        Me.lblamount = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.lblRatePerHour = New System.Windows.Forms.Label()
        Me.lblRemarks = New System.Windows.Forms.Label()
        Me.lblworkingtime = New System.Windows.Forms.Label()
        Me.txtbasicesalary = New System.Windows.Forms.TextBox()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.txtworkingtime = New System.Windows.Forms.TextBox()
        Me.lbldalywage = New System.Windows.Forms.Label()
        Me.cmbemployee = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.lblDesignation = New System.Windows.Forms.Label()
        Me.lblDepartment = New System.Windows.Forms.Label()
        Me.txtDesignation = New System.Windows.Forms.TextBox()
        Me.txtDepartment = New System.Windows.Forms.TextBox()
        Me.txtDcNo = New System.Windows.Forms.TextBox()
        Me.lblrefrance = New System.Windows.Forms.Label()
        Me.txtrefrance = New System.Windows.Forms.TextBox()
        Me.dtpDcDate = New System.Windows.Forms.DateTimePicker()
        Me.chkPost = New System.Windows.Forms.CheckBox()
        Me.grdDetail = New Janus.Windows.GridEX.GridEX()
        Me.lbl = New System.Windows.Forms.Label()
        Me.lbldate = New System.Windows.Forms.Label()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdSaved = New Janus.Windows.GridEX.GridEX()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnLoadAll = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSplitButton1 = New System.Windows.Forms.ToolStripSplitButton()
        Me.AddNewCostCenterToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnHelp = New System.Windows.Forms.ToolStripButton()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.cmbemployee, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox1)
        Me.UltraTabPageControl1.Controls.Add(Me.lblProgress)
        Me.UltraTabPageControl1.Controls.Add(Me.lblDesignation)
        Me.UltraTabPageControl1.Controls.Add(Me.lblDepartment)
        Me.UltraTabPageControl1.Controls.Add(Me.txtDesignation)
        Me.UltraTabPageControl1.Controls.Add(Me.txtDepartment)
        Me.UltraTabPageControl1.Controls.Add(Me.txtDcNo)
        Me.UltraTabPageControl1.Controls.Add(Me.lblrefrance)
        Me.UltraTabPageControl1.Controls.Add(Me.txtrefrance)
        Me.UltraTabPageControl1.Controls.Add(Me.dtpDcDate)
        Me.UltraTabPageControl1.Controls.Add(Me.chkPost)
        Me.UltraTabPageControl1.Controls.Add(Me.grdDetail)
        Me.UltraTabPageControl1.Controls.Add(Me.lbl)
        Me.UltraTabPageControl1.Controls.Add(Me.lbldate)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(815, 491)
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtChargeType)
        Me.GroupBox1.Controls.Add(Me.lblLabourType)
        Me.GroupBox1.Controls.Add(Me.cmbLabourType)
        Me.GroupBox1.Controls.Add(Me.lblStages)
        Me.GroupBox1.Controls.Add(Me.cmbStages)
        Me.GroupBox1.Controls.Add(Me.lblcostcenter)
        Me.GroupBox1.Controls.Add(Me.lblemployee)
        Me.GroupBox1.Controls.Add(Me.btnAdd)
        Me.GroupBox1.Controls.Add(Me.cmbcostcenter)
        Me.GroupBox1.Controls.Add(Me.lblamount)
        Me.GroupBox1.Controls.Add(Me.txtRemarks)
        Me.GroupBox1.Controls.Add(Me.lblRatePerHour)
        Me.GroupBox1.Controls.Add(Me.lblRemarks)
        Me.GroupBox1.Controls.Add(Me.lblworkingtime)
        Me.GroupBox1.Controls.Add(Me.txtbasicesalary)
        Me.GroupBox1.Controls.Add(Me.txtAmount)
        Me.GroupBox1.Controls.Add(Me.txtworkingtime)
        Me.GroupBox1.Controls.Add(Me.lbldalywage)
        Me.GroupBox1.Controls.Add(Me.cmbemployee)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 157)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(803, 95)
        Me.GroupBox1.TabIndex = 13
        Me.GroupBox1.TabStop = False
        '
        'txtChargeType
        '
        Me.txtChargeType.Location = New System.Drawing.Point(4, 67)
        Me.txtChargeType.Name = "txtChargeType"
        Me.txtChargeType.ReadOnly = True
        Me.txtChargeType.Size = New System.Drawing.Size(66, 20)
        Me.txtChargeType.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.txtChargeType, "Charge Type")
        '
        'lblLabourType
        '
        Me.lblLabourType.AutoSize = True
        Me.lblLabourType.Location = New System.Drawing.Point(427, 11)
        Me.lblLabourType.Name = "lblLabourType"
        Me.lblLabourType.Size = New System.Drawing.Size(67, 13)
        Me.lblLabourType.TabIndex = 6
        Me.lblLabourType.Text = "Labour Type"
        Me.lblLabourType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbLabourType
        '
        Me.cmbLabourType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbLabourType.FormattingEnabled = True
        Me.cmbLabourType.Location = New System.Drawing.Point(430, 28)
        Me.cmbLabourType.Name = "cmbLabourType"
        Me.cmbLabourType.Size = New System.Drawing.Size(137, 21)
        Me.cmbLabourType.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.cmbLabourType, "Select a labour type")
        '
        'lblStages
        '
        Me.lblStages.AutoSize = True
        Me.lblStages.Location = New System.Drawing.Point(142, 11)
        Me.lblStages.Name = "lblStages"
        Me.lblStages.Size = New System.Drawing.Size(40, 13)
        Me.lblStages.TabIndex = 2
        Me.lblStages.Text = "Stages"
        Me.lblStages.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmbStages
        '
        Me.cmbStages.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbStages.FormattingEnabled = True
        Me.cmbStages.Location = New System.Drawing.Point(145, 27)
        Me.cmbStages.Name = "cmbStages"
        Me.cmbStages.Size = New System.Drawing.Size(137, 21)
        Me.cmbStages.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.cmbStages, "Select a stage")
        '
        'lblcostcenter
        '
        Me.lblcostcenter.AutoSize = True
        Me.lblcostcenter.Location = New System.Drawing.Point(1, 11)
        Me.lblcostcenter.Name = "lblcostcenter"
        Me.lblcostcenter.Size = New System.Drawing.Size(62, 13)
        Me.lblcostcenter.TabIndex = 0
        Me.lblcostcenter.Text = "Cost Center"
        Me.lblcostcenter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblemployee
        '
        Me.lblemployee.AutoSize = True
        Me.lblemployee.Location = New System.Drawing.Point(284, 11)
        Me.lblemployee.Name = "lblemployee"
        Me.lblemployee.Size = New System.Drawing.Size(53, 13)
        Me.lblemployee.TabIndex = 4
        Me.lblemployee.Text = "Employee"
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(406, 65)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(51, 23)
        Me.btnAdd.TabIndex = 18
        Me.btnAdd.Text = "Add"
        Me.ToolTip1.SetToolTip(Me.btnAdd, "Add Employee Salary To Grid")
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'cmbcostcenter
        '
        Me.cmbcostcenter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbcostcenter.FormattingEnabled = True
        Me.cmbcostcenter.Location = New System.Drawing.Point(4, 27)
        Me.cmbcostcenter.Name = "cmbcostcenter"
        Me.cmbcostcenter.Size = New System.Drawing.Size(137, 21)
        Me.cmbcostcenter.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbcostcenter, "Select Cost Center")
        '
        'lblamount
        '
        Me.lblamount.AutoSize = True
        Me.lblamount.Location = New System.Drawing.Point(213, 51)
        Me.lblamount.Name = "lblamount"
        Me.lblamount.Size = New System.Drawing.Size(43, 13)
        Me.lblamount.TabIndex = 14
        Me.lblamount.Text = "Amount"
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(288, 67)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(112, 20)
        Me.txtRemarks.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.txtRemarks, "Remarks for necessary")
        '
        'lblRatePerHour
        '
        Me.lblRatePerHour.AutoSize = True
        Me.lblRatePerHour.Location = New System.Drawing.Point(72, 51)
        Me.lblRatePerHour.Name = "lblRatePerHour"
        Me.lblRatePerHour.Size = New System.Drawing.Size(30, 13)
        Me.lblRatePerHour.TabIndex = 10
        Me.lblRatePerHour.Text = "Rate"
        '
        'lblRemarks
        '
        Me.lblRemarks.AutoSize = True
        Me.lblRemarks.Location = New System.Drawing.Point(285, 51)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(49, 13)
        Me.lblRemarks.TabIndex = 16
        Me.lblRemarks.Text = "Remarks"
        '
        'lblworkingtime
        '
        Me.lblworkingtime.AutoSize = True
        Me.lblworkingtime.Location = New System.Drawing.Point(142, 50)
        Me.lblworkingtime.Name = "lblworkingtime"
        Me.lblworkingtime.Size = New System.Drawing.Size(26, 13)
        Me.lblworkingtime.TabIndex = 12
        Me.lblworkingtime.Text = "Unit"
        '
        'txtbasicesalary
        '
        Me.txtbasicesalary.Location = New System.Drawing.Point(75, 67)
        Me.txtbasicesalary.Name = "txtbasicesalary"
        Me.txtbasicesalary.Size = New System.Drawing.Size(66, 20)
        Me.txtbasicesalary.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.txtbasicesalary, "Rate Per Hour")
        '
        'txtAmount
        '
        Me.txtAmount.Enabled = False
        Me.txtAmount.Location = New System.Drawing.Point(216, 67)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(66, 20)
        Me.txtAmount.TabIndex = 15
        Me.ToolTip1.SetToolTip(Me.txtAmount, "Total Amount")
        '
        'txtworkingtime
        '
        Me.txtworkingtime.Location = New System.Drawing.Point(145, 66)
        Me.txtworkingtime.Name = "txtworkingtime"
        Me.txtworkingtime.Size = New System.Drawing.Size(66, 20)
        Me.txtworkingtime.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.txtworkingtime, "Time")
        '
        'lbldalywage
        '
        Me.lbldalywage.AutoSize = True
        Me.lbldalywage.Location = New System.Drawing.Point(3, 51)
        Me.lbldalywage.Name = "lbldalywage"
        Me.lbldalywage.Size = New System.Drawing.Size(68, 13)
        Me.lbldalywage.TabIndex = 8
        Me.lbldalywage.Text = "Charge Type"
        '
        'cmbemployee
        '
        Me.cmbemployee.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cmbemployee.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbemployee.CheckedListSettings.CheckStateMember = ""
        Appearance1.BackColor = System.Drawing.Color.White
        Me.cmbemployee.DisplayLayout.Appearance = Appearance1
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
        Me.cmbemployee.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbemployee.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbemployee.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbemployee.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbemployee.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Appearance2.BackColor = System.Drawing.Color.Transparent
        Me.cmbemployee.DisplayLayout.Override.CardAreaAppearance = Appearance2
        Me.cmbemployee.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbemployee.DisplayLayout.Override.CellPadding = 3
        Me.cmbemployee.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance3.TextHAlignAsString = "Left"
        Me.cmbemployee.DisplayLayout.Override.HeaderAppearance = Appearance3
        Me.cmbemployee.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance4.BorderColor = System.Drawing.Color.LightGray
        Appearance4.TextVAlignAsString = "Middle"
        Me.cmbemployee.DisplayLayout.Override.RowAppearance = Appearance4
        Appearance5.BackColor = System.Drawing.Color.LightSteelBlue
        Appearance5.BorderColor = System.Drawing.Color.Black
        Appearance5.ForeColor = System.Drawing.Color.Black
        Me.cmbemployee.DisplayLayout.Override.SelectedRowAppearance = Appearance5
        Me.cmbemployee.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbemployee.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbemployee.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbemployee.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.cmbemployee.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbemployee.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbemployee.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbemployee.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbemployee.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear
        Me.cmbemployee.LimitToList = True
        Me.cmbemployee.Location = New System.Drawing.Point(287, 27)
        Me.cmbemployee.Name = "cmbemployee"
        Me.cmbemployee.Size = New System.Drawing.Size(137, 22)
        Me.cmbemployee.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.cmbemployee, "Select a employee")
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(541, 91)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 1
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'lblDesignation
        '
        Me.lblDesignation.AutoSize = True
        Me.lblDesignation.Location = New System.Drawing.Point(280, 103)
        Me.lblDesignation.Name = "lblDesignation"
        Me.lblDesignation.Size = New System.Drawing.Size(63, 13)
        Me.lblDesignation.TabIndex = 11
        Me.lblDesignation.Text = "Designation"
        '
        'lblDepartment
        '
        Me.lblDepartment.AutoSize = True
        Me.lblDepartment.Location = New System.Drawing.Point(281, 78)
        Me.lblDepartment.Name = "lblDepartment"
        Me.lblDepartment.Size = New System.Drawing.Size(62, 13)
        Me.lblDepartment.TabIndex = 9
        Me.lblDepartment.Text = "Department"
        '
        'txtDesignation
        '
        Me.txtDesignation.Location = New System.Drawing.Point(346, 100)
        Me.txtDesignation.Name = "txtDesignation"
        Me.txtDesignation.ReadOnly = True
        Me.txtDesignation.Size = New System.Drawing.Size(142, 20)
        Me.txtDesignation.TabIndex = 12
        Me.txtDesignation.TabStop = False
        Me.ToolTip1.SetToolTip(Me.txtDesignation, "Designation")
        '
        'txtDepartment
        '
        Me.txtDepartment.Location = New System.Drawing.Point(346, 75)
        Me.txtDepartment.Name = "txtDepartment"
        Me.txtDepartment.ReadOnly = True
        Me.txtDepartment.Size = New System.Drawing.Size(142, 20)
        Me.txtDepartment.TabIndex = 10
        Me.txtDepartment.TabStop = False
        Me.ToolTip1.SetToolTip(Me.txtDepartment, "Department")
        '
        'txtDcNo
        '
        Me.txtDcNo.Location = New System.Drawing.Point(91, 75)
        Me.txtDcNo.Name = "txtDcNo"
        Me.txtDcNo.ReadOnly = True
        Me.txtDcNo.Size = New System.Drawing.Size(185, 20)
        Me.txtDcNo.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtDcNo, "Document No")
        '
        'lblrefrance
        '
        Me.lblrefrance.AutoSize = True
        Me.lblrefrance.Location = New System.Drawing.Point(30, 129)
        Me.lblrefrance.Name = "lblrefrance"
        Me.lblrefrance.Size = New System.Drawing.Size(57, 13)
        Me.lblrefrance.TabIndex = 6
        Me.lblrefrance.Text = "Reference"
        '
        'txtrefrance
        '
        Me.txtrefrance.Location = New System.Drawing.Point(91, 126)
        Me.txtrefrance.Name = "txtrefrance"
        Me.txtrefrance.Size = New System.Drawing.Size(185, 20)
        Me.txtrefrance.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.txtrefrance, "Reference for necessary")
        '
        'dtpDcDate
        '
        Me.dtpDcDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpDcDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDcDate.Location = New System.Drawing.Point(91, 100)
        Me.dtpDcDate.Name = "dtpDcDate"
        Me.dtpDcDate.Size = New System.Drawing.Size(185, 20)
        Me.dtpDcDate.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.dtpDcDate, "Document Date")
        '
        'chkPost
        '
        Me.chkPost.AutoSize = True
        Me.chkPost.Checked = True
        Me.chkPost.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPost.Location = New System.Drawing.Point(311, 131)
        Me.chkPost.Name = "chkPost"
        Me.chkPost.Size = New System.Drawing.Size(59, 17)
        Me.chkPost.TabIndex = 8
        Me.chkPost.TabStop = False
        Me.chkPost.Text = "Posted"
        Me.ToolTip1.SetToolTip(Me.chkPost, "Posted Voucher If Checked On")
        Me.chkPost.UseVisualStyleBackColor = True
        '
        'grdDetail
        '
        Me.grdDetail.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdDetail_DesignTimeLayout.LayoutString = resources.GetString("grdDetail_DesignTimeLayout.LayoutString")
        Me.grdDetail.DesignTimeLayout = grdDetail_DesignTimeLayout
        Me.grdDetail.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdDetail.GroupByBoxVisible = False
        Me.grdDetail.Location = New System.Drawing.Point(1, 258)
        Me.grdDetail.Name = "grdDetail"
        Me.grdDetail.RecordNavigator = True
        Me.grdDetail.Size = New System.Drawing.Size(813, 232)
        Me.grdDetail.TabIndex = 14
        Me.grdDetail.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        Me.ToolTip1.SetToolTip(Me.grdDetail, "Employee Salary Detail")
        Me.grdDetail.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdDetail.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'lbl
        '
        Me.lbl.AutoSize = True
        Me.lbl.Location = New System.Drawing.Point(12, 78)
        Me.lbl.Name = "lbl"
        Me.lbl.Size = New System.Drawing.Size(76, 13)
        Me.lbl.TabIndex = 2
        Me.lbl.Text = "Document No."
        '
        'lbldate
        '
        Me.lbldate.AutoSize = True
        Me.lbldate.Location = New System.Drawing.Point(57, 105)
        Me.lbldate.Name = "lbldate"
        Me.lbldate.Size = New System.Drawing.Size(30, 13)
        Me.lbldate.TabIndex = 4
        Me.lbldate.Text = "Date"
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grdSaved)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(815, 491)
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
        Me.grdSaved.Size = New System.Drawing.Size(815, 491)
        Me.grdSaved.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.grdSaved, "Saved Record Detail")
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(817, 512)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 0
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Daily Salary"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "History"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(815, 491)
        '
        'pnlHeader
        '
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Location = New System.Drawing.Point(3, 27)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(811, 42)
        Me.pnlHeader.TabIndex = 81
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(10, 11)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(140, 23)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Daily Salary"
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
        'btnPrint
        '
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(52, 22)
        Me.btnPrint.Text = "&Print"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(60, 22)
        Me.btnDelete.Text = "D&elete"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(66, 22)
        Me.btnRefresh.Text = "Refresh"
        '
        'btnLoadAll
        '
        Me.btnLoadAll.Image = Global.SimpleAccounts.My.Resources.Resources.sendcontactdetails
        Me.btnLoadAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnLoadAll.Name = "btnLoadAll"
        Me.btnLoadAll.Size = New System.Drawing.Size(70, 22)
        Me.btnLoadAll.Text = "Load All"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripSplitButton1
        '
        Me.ToolStripSplitButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddNewCostCenterToolStripMenuItem})
        Me.ToolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripSplitButton1.Name = "ToolStripSplitButton1"
        Me.ToolStripSplitButton1.Size = New System.Drawing.Size(120, 22)
        Me.ToolStripSplitButton1.Text = "Add New Account"
        '
        'AddNewCostCenterToolStripMenuItem
        '
        Me.AddNewCostCenterToolStripMenuItem.Name = "AddNewCostCenterToolStripMenuItem"
        Me.AddNewCostCenterToolStripMenuItem.Size = New System.Drawing.Size(188, 22)
        Me.AddNewCostCenterToolStripMenuItem.Text = "Add New Cost Center"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'btnHelp
        '
        Me.btnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(23, 22)
        Me.btnHelp.Text = "He&lp"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.btnPrint, Me.toolStripSeparator, Me.btnDelete, Me.toolStripSeparator1, Me.btnRefresh, Me.btnLoadAll, Me.ToolStripSeparator2, Me.ToolStripSplitButton1, Me.ToolStripSeparator3, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(817, 25)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'frmDailySalaries
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(817, 512)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmDailySalaries"
        Me.Text = "Daily Salary"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.cmbemployee, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents txtDcNo As System.Windows.Forms.TextBox
    Friend WithEvents lblrefrance As System.Windows.Forms.Label
    Friend WithEvents txtrefrance As System.Windows.Forms.TextBox
    Friend WithEvents dtpDcDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkPost As System.Windows.Forms.CheckBox
    Friend WithEvents lbl As System.Windows.Forms.Label
    Friend WithEvents lbldate As System.Windows.Forms.Label
    Friend WithEvents grdDetail As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblemployee As System.Windows.Forms.Label
    Friend WithEvents cmbcostcenter As System.Windows.Forms.ComboBox
    Friend WithEvents lbldalywage As System.Windows.Forms.Label
    Friend WithEvents lblcostcenter As System.Windows.Forms.Label
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents lblamount As System.Windows.Forms.Label
    Friend WithEvents txtworkingtime As System.Windows.Forms.TextBox
    Friend WithEvents lblworkingtime As System.Windows.Forms.Label
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents grdSaved As Janus.Windows.GridEX.GridEX
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents lblRemarks As System.Windows.Forms.Label
    Friend WithEvents lblRatePerHour As System.Windows.Forms.Label
    Friend WithEvents txtDepartment As System.Windows.Forms.TextBox
    Friend WithEvents txtDesignation As System.Windows.Forms.TextBox
    Friend WithEvents lblDesignation As System.Windows.Forms.Label
    Friend WithEvents lblDepartment As System.Windows.Forms.Label
    Friend WithEvents txtbasicesalary As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents cmbemployee As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblLabourType As System.Windows.Forms.Label
    Friend WithEvents cmbLabourType As System.Windows.Forms.ComboBox
    Friend WithEvents lblStages As System.Windows.Forms.Label
    Friend WithEvents cmbStages As System.Windows.Forms.ComboBox
    Friend WithEvents txtChargeType As System.Windows.Forms.TextBox
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnLoadAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSplitButton1 As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents AddNewCostCenterToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
End Class

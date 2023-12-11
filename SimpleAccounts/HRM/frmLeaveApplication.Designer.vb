<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLeaveApplication
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
        Dim grdApproval_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLeaveApplication))
        Dim grdPending_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
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
        Dim grdSaved_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblStatus = New System.Windows.Forms.Label()
        Me.btnReject = New System.Windows.Forms.Button()
        Me.btnApprove = New System.Windows.Forms.Button()
        Me.grpApproval = New System.Windows.Forms.GroupBox()
        Me.grdApproval = New Janus.Windows.GridEX.GridEX()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.grdPending = New Janus.Windows.GridEX.GridEX()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cmbEmployees = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtApplicationNo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpApplicationDate = New System.Windows.Forms.DateTimePicker()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtContactNo = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtAddress = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbLeaveType = New System.Windows.Forms.ComboBox()
        Me.txtReason = New System.Windows.Forms.RichTextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.txtNoOfLeaves = New System.Windows.Forms.TextBox()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdSaved = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripSplitButton1 = New System.Windows.Forms.ToolStripSplitButton()
        Me.AppReq = New System.Windows.Forms.ToolStripMenuItem()
        Me.RejReq = New System.Windows.Forms.ToolStripMenuItem()
        Me.PendReq = New System.Windows.Forms.ToolStripMenuItem()
        Me.AllRequests = New System.Windows.Forms.ToolStripMenuItem()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.grpApproval.SuspendLayout()
        CType(Me.grdApproval, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        CType(Me.grdPending, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        CType(Me.cmbEmployees, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.Label12)
        Me.UltraTabPageControl1.Controls.Add(Me.lblStatus)
        Me.UltraTabPageControl1.Controls.Add(Me.btnReject)
        Me.UltraTabPageControl1.Controls.Add(Me.btnApprove)
        Me.UltraTabPageControl1.Controls.Add(Me.grpApproval)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox3)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox2)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox1)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1223, 781)
        '
        'pnlHeader
        '
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1223, 44)
        Me.pnlHeader.TabIndex = 35
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(9, 6)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(251, 29)
        Me.lblHeader.TabIndex = 29
        Me.lblHeader.Text = "Leave Application"
        '
        'Label12
        '
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.75!, System.Drawing.FontStyle.Bold)
        Me.Label12.Location = New System.Drawing.Point(837, 57)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(219, 44)
        Me.Label12.TabIndex = 34
        Me.Label12.Text = "Application Status:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblStatus
        '
        Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(1048, 57)
        Me.lblStatus.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(140, 44)
        Me.lblStatus.TabIndex = 27
        Me.lblStatus.Text = "Pending"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnReject
        '
        Me.btnReject.BackColor = System.Drawing.SystemColors.Menu
        Me.btnReject.ForeColor = System.Drawing.Color.Maroon
        Me.btnReject.Location = New System.Drawing.Point(568, 518)
        Me.btnReject.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnReject.Name = "btnReject"
        Me.btnReject.Size = New System.Drawing.Size(147, 50)
        Me.btnReject.TabIndex = 31
        Me.btnReject.Text = "Reject"
        Me.btnReject.UseVisualStyleBackColor = False
        '
        'btnApprove
        '
        Me.btnApprove.BackColor = System.Drawing.SystemColors.Menu
        Me.btnApprove.ForeColor = System.Drawing.Color.Green
        Me.btnApprove.Location = New System.Drawing.Point(377, 518)
        Me.btnApprove.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.btnApprove.Name = "btnApprove"
        Me.btnApprove.Size = New System.Drawing.Size(147, 50)
        Me.btnApprove.TabIndex = 30
        Me.btnApprove.Text = "Approve"
        Me.btnApprove.UseVisualStyleBackColor = False
        '
        'grpApproval
        '
        Me.grpApproval.Controls.Add(Me.grdApproval)
        Me.grpApproval.Controls.Add(Me.Label10)
        Me.grpApproval.Controls.Add(Me.txtRemarks)
        Me.grpApproval.Location = New System.Drawing.Point(15, 592)
        Me.grpApproval.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grpApproval.Name = "grpApproval"
        Me.grpApproval.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grpApproval.Size = New System.Drawing.Size(1173, 164)
        Me.grpApproval.TabIndex = 33
        Me.grpApproval.TabStop = False
        Me.grpApproval.Text = "Approval History"
        Me.grpApproval.Visible = False
        '
        'grdApproval
        '
        grdApproval_DesignTimeLayout.LayoutString = resources.GetString("grdApproval_DesignTimeLayout.LayoutString")
        Me.grdApproval.DesignTimeLayout = grdApproval_DesignTimeLayout
        Me.grdApproval.GroupByBoxVisible = False
        Me.grdApproval.Location = New System.Drawing.Point(12, 23)
        Me.grdApproval.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grdApproval.Name = "grdApproval"
        Me.grdApproval.Size = New System.Drawing.Size(729, 165)
        Me.grdApproval.TabIndex = 1
        Me.grdApproval.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(761, 25)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(69, 18)
        Me.Label10.TabIndex = 29
        Me.Label10.Text = "Remarks"
        Me.Label10.Visible = False
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(765, 47)
        Me.txtRemarks.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(399, 141)
        Me.txtRemarks.TabIndex = 28
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.grdPending)
        Me.GroupBox3.Location = New System.Drawing.Point(772, 98)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox3.Size = New System.Drawing.Size(416, 409)
        Me.GroupBox3.TabIndex = 32
        Me.GroupBox3.TabStop = False
        '
        'grdPending
        '
        grdPending_DesignTimeLayout.LayoutString = resources.GetString("grdPending_DesignTimeLayout.LayoutString")
        Me.grdPending.DesignTimeLayout = grdPending_DesignTimeLayout
        Me.grdPending.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdPending.GroupByBoxVisible = False
        Me.grdPending.Location = New System.Drawing.Point(8, 39)
        Me.grdPending.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grdPending.Name = "grdPending"
        Me.grdPending.Size = New System.Drawing.Size(400, 326)
        Me.grdPending.TabIndex = 0
        Me.grdPending.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdPending.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmbEmployees)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.txtApplicationNo)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.dtpApplicationDate)
        Me.GroupBox2.Location = New System.Drawing.Point(15, 98)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox2.Size = New System.Drawing.Size(749, 79)
        Me.GroupBox2.TabIndex = 31
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Request Informtion"
        '
        'cmbEmployees
        '
        Appearance36.BackColor = System.Drawing.SystemColors.Info
        Me.cmbEmployees.Appearance = Appearance36
        Me.cmbEmployees.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cmbEmployees.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbEmployees.CheckedListSettings.CheckStateMember = ""
        Appearance37.BackColor = System.Drawing.Color.White
        Me.cmbEmployees.DisplayLayout.Appearance = Appearance37
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
        Me.cmbEmployees.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbEmployees.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbEmployees.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbEmployees.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbEmployees.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Appearance38.BackColor = System.Drawing.Color.Transparent
        Me.cmbEmployees.DisplayLayout.Override.CardAreaAppearance = Appearance38
        Me.cmbEmployees.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbEmployees.DisplayLayout.Override.CellPadding = 3
        Me.cmbEmployees.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance39.TextHAlignAsString = "Left"
        Me.cmbEmployees.DisplayLayout.Override.HeaderAppearance = Appearance39
        Me.cmbEmployees.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance40.BorderColor = System.Drawing.Color.LightGray
        Appearance40.TextVAlignAsString = "Middle"
        Me.cmbEmployees.DisplayLayout.Override.RowAppearance = Appearance40
        Appearance41.BackColor = System.Drawing.Color.LightSteelBlue
        Appearance41.BorderColor = System.Drawing.Color.Black
        Appearance41.ForeColor = System.Drawing.Color.Black
        Me.cmbEmployees.DisplayLayout.Override.SelectedRowAppearance = Appearance41
        Me.cmbEmployees.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbEmployees.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbEmployees.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbEmployees.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.cmbEmployees.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbEmployees.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbEmployees.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbEmployees.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbEmployees.LimitToList = True
        Me.cmbEmployees.Location = New System.Drawing.Point(367, 39)
        Me.cmbEmployees.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbEmployees.MaxDropDownItems = 16
        Me.cmbEmployees.Name = "cmbEmployees"
        Me.cmbEmployees.Size = New System.Drawing.Size(383, 25)
        Me.cmbEmployees.TabIndex = 19
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(8, 20)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(111, 17)
        Me.Label9.TabIndex = 12
        Me.Label9.Text = "Application Date"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(363, 18)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 17)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Employee"
        '
        'txtApplicationNo
        '
        Me.txtApplicationNo.Location = New System.Drawing.Point(187, 39)
        Me.txtApplicationNo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtApplicationNo.Name = "txtApplicationNo"
        Me.txtApplicationNo.ReadOnly = True
        Me.txtApplicationNo.Size = New System.Drawing.Size(171, 22)
        Me.txtApplicationNo.TabIndex = 15
        Me.txtApplicationNo.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(183, 20)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 17)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Application No"
        '
        'dtpApplicationDate
        '
        Me.dtpApplicationDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpApplicationDate.Enabled = False
        Me.dtpApplicationDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpApplicationDate.Location = New System.Drawing.Point(12, 39)
        Me.dtpApplicationDate.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dtpApplicationDate.Name = "dtpApplicationDate"
        Me.dtpApplicationDate.Size = New System.Drawing.Size(165, 22)
        Me.dtpApplicationDate.TabIndex = 13
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtContactNo)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtAddress)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.cmbLeaveType)
        Me.GroupBox1.Controls.Add(Me.txtReason)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.dtpFrom)
        Me.GroupBox1.Controls.Add(Me.txtNoOfLeaves)
        Me.GroupBox1.Location = New System.Drawing.Point(15, 185)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.GroupBox1.Size = New System.Drawing.Size(749, 322)
        Me.GroupBox1.TabIndex = 30
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Leave Details"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(501, 222)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(117, 17)
        Me.Label8.TabIndex = 27
        Me.Label8.Text = "Contact Numbers"
        '
        'txtContactNo
        '
        Me.txtContactNo.Location = New System.Drawing.Point(505, 241)
        Me.txtContactNo.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtContactNo.Multiline = True
        Me.txtContactNo.Name = "txtContactNo"
        Me.txtContactNo.Size = New System.Drawing.Size(233, 73)
        Me.txtContactNo.TabIndex = 26
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(8, 222)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(233, 17)
        Me.Label7.TabIndex = 25
        Me.Label7.Text = "Address During Long/Station Leave"
        '
        'txtAddress
        '
        Me.txtAddress.Location = New System.Drawing.Point(12, 241)
        Me.txtAddress.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtAddress.Multiline = True
        Me.txtAddress.Name = "txtAddress"
        Me.txtAddress.Size = New System.Drawing.Size(484, 73)
        Me.txtAddress.TabIndex = 24
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 20)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(83, 17)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "Leave Type"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(8, 69)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(57, 17)
        Me.Label6.TabIndex = 23
        Me.Label6.Text = "Reason"
        '
        'cmbLeaveType
        '
        Me.cmbLeaveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbLeaveType.FormattingEnabled = True
        Me.cmbLeaveType.Items.AddRange(New Object() {"Select Leave Type", "Casual", "Sick", "Earned", "Maternity", "Umrah", "Bereavement", "Idaat", "Without Pay"})
        Me.cmbLeaveType.Location = New System.Drawing.Point(12, 39)
        Me.cmbLeaveType.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.cmbLeaveType.Name = "cmbLeaveType"
        Me.cmbLeaveType.Size = New System.Drawing.Size(260, 24)
        Me.cmbLeaveType.TabIndex = 16
        '
        'txtReason
        '
        Me.txtReason.Location = New System.Drawing.Point(12, 89)
        Me.txtReason.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtReason.Name = "txtReason"
        Me.txtReason.Size = New System.Drawing.Size(727, 128)
        Me.txtReason.TabIndex = 22
        Me.txtReason.Text = ""
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(449, 21)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 17)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "From"
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(324, 16)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(81, 20)
        Me.Label11.TabIndex = 21
        Me.Label11.Text = "Leaves"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(453, 41)
        Me.dtpFrom.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(245, 22)
        Me.dtpFrom.TabIndex = 19
        '
        'txtNoOfLeaves
        '
        Me.txtNoOfLeaves.Location = New System.Drawing.Point(317, 39)
        Me.txtNoOfLeaves.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.txtNoOfLeaves.Name = "txtNoOfLeaves"
        Me.txtNoOfLeaves.Size = New System.Drawing.Size(95, 22)
        Me.txtNoOfLeaves.TabIndex = 20
        Me.txtNoOfLeaves.Text = "1"
        Me.txtNoOfLeaves.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grdSaved)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-13333, -12308)
        Me.UltraTabPageControl2.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1223, 781)
        '
        'grdSaved
        '
        grdSaved_DesignTimeLayout.LayoutString = resources.GetString("grdSaved_DesignTimeLayout.LayoutString")
        Me.grdSaved.DesignTimeLayout = grdSaved_DesignTimeLayout
        Me.grdSaved.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSaved.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdSaved.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdSaved.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdSaved.GroupByBoxVisible = False
        Me.grdSaved.Location = New System.Drawing.Point(0, 0)
        Me.grdSaved.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.grdSaved.Name = "grdSaved"
        Me.grdSaved.Size = New System.Drawing.Size(1223, 781)
        Me.grdSaved.TabIndex = 4
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Font = New System.Drawing.Font("Segoe UI", 8.25!)
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.btnDelete, Me.toolStripSeparator1, Me.ToolStripSplitButton1, Me.HelpToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1225, 27)
        Me.ToolStrip1.TabIndex = 1
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(40, 24)
        Me.btnNew.Text = "&New"
        '
        'btnEdit
        '
        Me.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(36, 24)
        Me.btnEdit.Text = "&Edit"
        Me.btnEdit.Visible = False
        '
        'btnSave
        '
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(41, 24)
        Me.btnSave.Text = "&Save"
        '
        'btnDelete
        '
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.RightToLeftAutoMirrorImage = True
        Me.btnDelete.Size = New System.Drawing.Size(52, 24)
        Me.btnDelete.Text = "Delete"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 27)
        '
        'ToolStripSplitButton1
        '
        Me.ToolStripSplitButton1.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AppReq, Me.RejReq, Me.PendReq, Me.AllRequests})
        Me.ToolStripSplitButton1.Image = Global.SimpleAccounts.My.Resources.Resources.print
        Me.ToolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripSplitButton1.Name = "ToolStripSplitButton1"
        Me.ToolStripSplitButton1.Size = New System.Drawing.Size(77, 24)
        Me.ToolStripSplitButton1.Text = "&Print"
        '
        'AppReq
        '
        Me.AppReq.Name = "AppReq"
        Me.AppReq.Size = New System.Drawing.Size(197, 26)
        Me.AppReq.Text = "Approved Request"
        '
        'RejReq
        '
        Me.RejReq.Name = "RejReq"
        Me.RejReq.Size = New System.Drawing.Size(197, 26)
        Me.RejReq.Text = "Rejected Requests"
        '
        'PendReq
        '
        Me.PendReq.Name = "PendReq"
        Me.PendReq.Size = New System.Drawing.Size(197, 26)
        Me.PendReq.Text = "Pending Requests"
        '
        'AllRequests
        '
        Me.AllRequests.Name = "AllRequests"
        Me.AllRequests.Size = New System.Drawing.Size(197, 26)
        Me.AllRequests.Text = "All Requests"
        '
        'HelpToolStripButton
        '
        Me.HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton.Name = "HelpToolStripButton"
        Me.HelpToolStripButton.Size = New System.Drawing.Size(23, 24)
        Me.HelpToolStripButton.Text = "He&lp"
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 27)
        Me.UltraTabControl1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(1225, 803)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 29
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Request"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "History"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.TabStop = False
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1223, 781)
        '
        'frmLeaveApplication
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1225, 830)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Margin = New System.Windows.Forms.Padding(4, 4, 4, 4)
        Me.Name = "frmLeaveApplication"
        Me.Text = "Leave Application"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.grpApproval.ResumeLayout(False)
        Me.grpApproval.PerformLayout()
        CType(Me.grdApproval, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        CType(Me.grdPending, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        CType(Me.cmbEmployees, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
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
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSplitButton1 As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents AppReq As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RejReq As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PendReq As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AllRequests As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grpApproval As System.Windows.Forms.GroupBox
    Friend WithEvents grdApproval As Janus.Windows.GridEX.GridEX
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents btnReject As System.Windows.Forms.Button
    Friend WithEvents btnApprove As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents grdPending As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbEmployees As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtApplicationNo As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpApplicationDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtContactNo As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtAddress As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbLeaveType As System.Windows.Forms.ComboBox
    Friend WithEvents txtReason As System.Windows.Forms.RichTextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtNoOfLeaves As System.Windows.Forms.TextBox
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents grdSaved As Janus.Windows.GridEX.GridEX
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

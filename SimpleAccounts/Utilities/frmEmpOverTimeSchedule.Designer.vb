<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmpOverTimeSchedule
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmpOverTimeSchedule))
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtHours = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.dtpEndTime = New System.Windows.Forms.DateTimePicker()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.dtpStartTime = New System.Windows.Forms.DateTimePicker()
        Me.grdEmpOverTime = New Janus.Windows.GridEX.GridEX()
        Me.toolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.grdEmpHistory = New Janus.Windows.GridEX.GridEX()
        Me.TabEmpHistory = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.lstEmployee = New SimpleAccounts.uiListControl()
        Me.lstShiftGroup = New SimpleAccounts.uiListControl()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdEmpOverTime, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.toolStrip1.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        Me.ToolStrip2.SuspendLayout()
        CType(Me.grdEmpHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TabEmpHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabEmpHistory.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.txtSearch)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox1)
        Me.UltraTabPageControl1.Controls.Add(Me.lstEmployee)
        Me.UltraTabPageControl1.Controls.Add(Me.lstShiftGroup)
        Me.UltraTabPageControl1.Controls.Add(Me.grdEmpOverTime)
        Me.UltraTabPageControl1.Controls.Add(Me.toolStrip1)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(964, 922)
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.pnlHeader.Location = New System.Drawing.Point(0, 32)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(964, 53)
        Me.pnlHeader.TabIndex = 10
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(12, 9)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(383, 36)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Employee OverTime Entry"
        '
        'txtSearch
        '
        Me.txtSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.txtSearch.Location = New System.Drawing.Point(306, 491)
        Me.txtSearch.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(404, 26)
        Me.txtSearch.TabIndex = 6
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtHours)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.DateTimePicker2)
        Me.GroupBox1.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox1.Controls.Add(Me.dtpEndTime)
        Me.GroupBox1.Controls.Add(Me.btnAdd)
        Me.GroupBox1.Controls.Add(Me.dtpStartTime)
        Me.GroupBox1.Location = New System.Drawing.Point(22, 531)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(937, 117)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(584, 34)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(52, 20)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Hours"
        '
        'txtHours
        '
        Me.txtHours.Location = New System.Drawing.Point(645, 29)
        Me.txtHours.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtHours.Name = "txtHours"
        Me.txtHours.Size = New System.Drawing.Size(145, 26)
        Me.txtHours.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(328, 78)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(76, 20)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "End Time"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 77)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "End Date"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 35)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Start Date"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(328, 37)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(82, 20)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Start Time"
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.CustomFormat = "dd/MMM/yyyy"
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2.Location = New System.Drawing.Point(106, 71)
        Me.DateTimePicker2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(214, 26)
        Me.DateTimePicker2.TabIndex = 3
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd/MMM/yyyy"
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(106, 29)
        Me.DateTimePicker1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(214, 26)
        Me.DateTimePicker1.TabIndex = 1
        '
        'dtpEndTime
        '
        Me.dtpEndTime.Checked = False
        Me.dtpEndTime.CustomFormat = ""
        Me.dtpEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpEndTime.Location = New System.Drawing.Point(424, 72)
        Me.dtpEndTime.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpEndTime.Name = "dtpEndTime"
        Me.dtpEndTime.ShowCheckBox = True
        Me.dtpEndTime.Size = New System.Drawing.Size(152, 26)
        Me.dtpEndTime.TabIndex = 7
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(588, 71)
        Me.btnAdd.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(204, 34)
        Me.btnAdd.TabIndex = 8
        Me.btnAdd.Text = "Generate"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'dtpStartTime
        '
        Me.dtpStartTime.Checked = False
        Me.dtpStartTime.CustomFormat = ""
        Me.dtpStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtpStartTime.Location = New System.Drawing.Point(424, 31)
        Me.dtpStartTime.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpStartTime.Name = "dtpStartTime"
        Me.dtpStartTime.ShowCheckBox = True
        Me.dtpStartTime.Size = New System.Drawing.Size(152, 26)
        Me.dtpStartTime.TabIndex = 5
        '
        'grdEmpOverTime
        '
        Me.grdEmpOverTime.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdEmpOverTime.GroupByBoxVisible = False
        Me.grdEmpOverTime.Location = New System.Drawing.Point(2, 657)
        Me.grdEmpOverTime.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdEmpOverTime.Name = "grdEmpOverTime"
        Me.grdEmpOverTime.RecordNavigator = True
        Me.grdEmpOverTime.Size = New System.Drawing.Size(961, 271)
        Me.grdEmpOverTime.TabIndex = 0
        Me.grdEmpOverTime.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'toolStrip1
        '
        Me.toolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.btnDelete, Me.toolStripSeparator, Me.btnRefresh})
        Me.toolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.toolStrip1.Name = "toolStrip1"
        Me.toolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.toolStrip1.Size = New System.Drawing.Size(964, 32)
        Me.toolStrip1.TabIndex = 0
        Me.toolStrip1.Text = "toolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(75, 29)
        Me.btnNew.Text = "&New"
        '
        'btnEdit
        '
        Me.btnEdit.Image = CType(resources.GetObject("btnEdit.Image"), System.Drawing.Image)
        Me.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(70, 29)
        Me.btnEdit.Text = "&Edit"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(77, 29)
        Me.btnSave.Text = "&Save"
        '
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(90, 29)
        Me.btnDelete.Text = "D&elete"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 32)
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = CType(resources.GetObject("btnRefresh.Image"), System.Drawing.Image)
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 29)
        Me.btnRefresh.Text = "Refresh"
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.CtrlGrdBar1)
        Me.UltraTabPageControl2.Controls.Add(Me.ToolStrip2)
        Me.UltraTabPageControl2.Controls.Add(Me.grdEmpHistory)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(964, 922)
        '
        'ToolStrip2
        '
        Me.ToolStrip2.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator1})
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip2.Size = New System.Drawing.Size(964, 25)
        Me.ToolStrip2.TabIndex = 1
        Me.ToolStrip2.Text = "ToolStrip2"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'grdEmpHistory
        '
        Me.grdEmpHistory.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdEmpHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdEmpHistory.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdEmpHistory.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdEmpHistory.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdEmpHistory.GroupByBoxVisible = False
        Me.grdEmpHistory.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.grdEmpHistory.Location = New System.Drawing.Point(0, 0)
        Me.grdEmpHistory.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdEmpHistory.Name = "grdEmpHistory"
        Me.grdEmpHistory.RecordNavigator = True
        Me.grdEmpHistory.Size = New System.Drawing.Size(964, 922)
        Me.grdEmpHistory.TabIndex = 0
        Me.grdEmpHistory.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'TabEmpHistory
        '
        Me.TabEmpHistory.AllowTabClosing = False
        Me.TabEmpHistory.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.TabEmpHistory.Controls.Add(Me.UltraTabPageControl1)
        Me.TabEmpHistory.Controls.Add(Me.UltraTabPageControl2)
        Me.TabEmpHistory.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabEmpHistory.Location = New System.Drawing.Point(0, 0)
        Me.TabEmpHistory.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabEmpHistory.Name = "TabEmpHistory"
        Me.TabEmpHistory.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.TabEmpHistory.Size = New System.Drawing.Size(966, 949)
        Me.TabEmpHistory.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.TabEmpHistory.TabIndex = 3
        Me.TabEmpHistory.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Employee OverTime"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "History"
        Me.TabEmpHistory.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.TabEmpHistory.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.VisualStudio2005
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(964, 922)
        '
        'lstEmployee
        '
        Me.lstEmployee.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstEmployee.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstEmployee.BackColor = System.Drawing.Color.Transparent
        Me.lstEmployee.disableWhenChecked = False
        Me.lstEmployee.HeadingLabelName = "EmployeeList"
        Me.lstEmployee.HeadingText = "Employee List"
        Me.lstEmployee.Location = New System.Drawing.Point(306, 108)
        Me.lstEmployee.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstEmployee.Name = "lstEmployee"
        Me.lstEmployee.ShowAddNewButton = False
        Me.lstEmployee.ShowInverse = True
        Me.lstEmployee.ShowMagnifierButton = False
        Me.lstEmployee.ShowNoCheck = False
        Me.lstEmployee.ShowResetAllButton = False
        Me.lstEmployee.ShowSelectall = True
        Me.lstEmployee.Size = New System.Drawing.Size(446, 374)
        Me.lstEmployee.TabIndex = 3
        Me.lstEmployee.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstShiftGroup
        '
        Me.lstShiftGroup.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstShiftGroup.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstShiftGroup.BackColor = System.Drawing.Color.Transparent
        Me.lstShiftGroup.disableWhenChecked = False
        Me.lstShiftGroup.HeadingLabelName = "ShiftGroupList"
        Me.lstShiftGroup.HeadingText = "Shift Group List"
        Me.lstShiftGroup.Location = New System.Drawing.Point(22, 108)
        Me.lstShiftGroup.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstShiftGroup.Name = "lstShiftGroup"
        Me.lstShiftGroup.ShowAddNewButton = False
        Me.lstShiftGroup.ShowInverse = True
        Me.lstShiftGroup.ShowMagnifierButton = False
        Me.lstShiftGroup.ShowNoCheck = False
        Me.lstShiftGroup.ShowResetAllButton = False
        Me.lstShiftGroup.ShowSelectall = True
        Me.lstShiftGroup.Size = New System.Drawing.Size(274, 414)
        Me.lstShiftGroup.TabIndex = 2
        Me.lstShiftGroup.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Nothing
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(915, 0)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar1.MyGrid = Nothing
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(50, 38)
        Me.CtrlGrdBar1.TabIndex = 9
        Me.CtrlGrdBar1.TabStop = False
        '
        'frmEmpOverTimeSchedule
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(966, 949)
        Me.Controls.Add(Me.TabEmpHistory)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmEmpOverTimeSchedule"
        Me.Text = "OverTime Entry"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdEmpOverTime, System.ComponentModel.ISupportInitialize).EndInit()
        Me.toolStrip1.ResumeLayout(False)
        Me.toolStrip1.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        Me.UltraTabPageControl2.PerformLayout()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        CType(Me.grdEmpHistory, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TabEmpHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabEmpHistory.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabEmpHistory As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Private WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents grdEmpOverTime As Janus.Windows.GridEX.GridEX
    Private WithEvents toolStrip1 As System.Windows.Forms.ToolStrip
    Private WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Private WithEvents btnEdit As System.Windows.Forms.ToolStripButton
    Private WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Private WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Private WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grdEmpHistory As Janus.Windows.GridEX.GridEX
    Friend WithEvents lstEmployee As SimpleAccounts.uiListControl
    Friend WithEvents lstShiftGroup As SimpleAccounts.uiListControl
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents dtpEndTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpStartTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtHours As System.Windows.Forms.TextBox
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Private WithEvents ToolStrip2 As System.Windows.Forms.ToolStrip
    Private WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmStockComparisonReport
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
        Dim grdCostComparison_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmStockComparisonReport))
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lstCompany = New SimpleAccounts.uiListControl()
        Me.lstInventoryType = New SimpleAccounts.uiListControl()
        Me.lstItem = New SimpleAccounts.uiListControl()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label74 = New System.Windows.Forms.Label()
        Me.txtItemList = New System.Windows.Forms.TextBox()
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        Me.Label72 = New System.Windows.Forms.Label()
        Me.toDateTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.lstInventoryDepartment = New SimpleAccounts.uiListControl()
        Me.fromDateTimePicker = New System.Windows.Forms.DateTimePicker()
        Me.lstLocation = New SimpleAccounts.uiListControl()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lstInventoryCategory = New SimpleAccounts.uiListControl()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.grdCostComparison = New Janus.Windows.GridEX.GridEX()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.UltraTabPageControl1.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdCostComparison, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.UltraTabPageControl1.Controls.Add(Me.Panel5)
        Me.UltraTabPageControl1.Controls.Add(Me.Panel4)
        Me.UltraTabPageControl1.Controls.Add(Me.Panel3)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1800, 1024)
        '
        'Panel5
        '
        Me.Panel5.AutoScroll = True
        Me.Panel5.AutoSize = True
        Me.Panel5.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel5.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.Panel5.Controls.Add(Me.Label7)
        Me.Panel5.Controls.Add(Me.Label8)
        Me.Panel5.Controls.Add(Me.Label6)
        Me.Panel5.Controls.Add(Me.lstCompany)
        Me.Panel5.Controls.Add(Me.lstInventoryType)
        Me.Panel5.Controls.Add(Me.lstItem)
        Me.Panel5.Controls.Add(Me.Label5)
        Me.Panel5.Controls.Add(Me.Label4)
        Me.Panel5.Controls.Add(Me.Label74)
        Me.Panel5.Controls.Add(Me.txtItemList)
        Me.Panel5.Controls.Add(Me.cmbPeriod)
        Me.Panel5.Controls.Add(Me.Label72)
        Me.Panel5.Controls.Add(Me.toDateTimePicker)
        Me.Panel5.Controls.Add(Me.lstInventoryDepartment)
        Me.Panel5.Controls.Add(Me.fromDateTimePicker)
        Me.Panel5.Controls.Add(Me.lstLocation)
        Me.Panel5.Controls.Add(Me.Panel2)
        Me.Panel5.Controls.Add(Me.Label3)
        Me.Panel5.Controls.Add(Me.lstInventoryCategory)
        Me.Panel5.Controls.Add(Me.Label2)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel5.Location = New System.Drawing.Point(0, 86)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(1800, 836)
        Me.Panel5.TabIndex = 33
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(22, 417)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(98, 28)
        Me.Label7.TabIndex = 32
        Me.Label7.Text = "Category"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(352, 417)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(55, 28)
        Me.Label8.TabIndex = 32
        Me.Label8.Text = "Item"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(681, 131)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(57, 28)
        Me.Label6.TabIndex = 32
        Me.Label6.Text = "Type"
        '
        'lstCompany
        '
        Me.lstCompany.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstCompany.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstCompany.BackColor = System.Drawing.Color.Transparent
        Me.lstCompany.disableWhenChecked = False
        Me.lstCompany.HeadingLabelName = "lstEmpDepartment"
        Me.lstCompany.HeadingText = "Company"
        Me.lstCompany.Location = New System.Drawing.Point(1358, 466)
        Me.lstCompany.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstCompany.Name = "lstCompany"
        Me.lstCompany.ShowAddNewButton = False
        Me.lstCompany.ShowInverse = True
        Me.lstCompany.ShowMagnifierButton = False
        Me.lstCompany.ShowNoCheck = False
        Me.lstCompany.ShowResetAllButton = False
        Me.lstCompany.ShowSelectall = True
        Me.lstCompany.Size = New System.Drawing.Size(303, 274)
        Me.lstCompany.TabIndex = 21
        Me.lstCompany.Visible = False
        Me.lstCompany.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstInventoryType
        '
        Me.lstInventoryType.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstInventoryType.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstInventoryType.BackColor = System.Drawing.Color.Transparent
        Me.lstInventoryType.disableWhenChecked = False
        Me.lstInventoryType.HeadingLabelName = "lstEmpDepartment"
        Me.lstInventoryType.HeadingText = ""
        Me.lstInventoryType.Location = New System.Drawing.Point(686, 138)
        Me.lstInventoryType.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstInventoryType.Name = "lstInventoryType"
        Me.lstInventoryType.ShowAddNewButton = False
        Me.lstInventoryType.ShowInverse = True
        Me.lstInventoryType.ShowMagnifierButton = False
        Me.lstInventoryType.ShowNoCheck = False
        Me.lstInventoryType.ShowResetAllButton = False
        Me.lstInventoryType.ShowSelectall = True
        Me.lstInventoryType.Size = New System.Drawing.Size(303, 274)
        Me.lstInventoryType.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.lstInventoryType, "Type")
        Me.lstInventoryType.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstItem
        '
        Me.lstItem.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstItem.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstItem.BackColor = System.Drawing.Color.Transparent
        Me.lstItem.disableWhenChecked = False
        Me.lstItem.HeadingLabelName = "lstEmpDepartment"
        Me.lstItem.HeadingText = ""
        Me.lstItem.Location = New System.Drawing.Point(354, 422)
        Me.lstItem.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstItem.Name = "lstItem"
        Me.lstItem.ShowAddNewButton = False
        Me.lstItem.ShowInverse = True
        Me.lstItem.ShowMagnifierButton = False
        Me.lstItem.ShowNoCheck = False
        Me.lstItem.ShowResetAllButton = False
        Me.lstItem.ShowSelectall = True
        Me.lstItem.Size = New System.Drawing.Size(634, 274)
        Me.lstItem.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.lstItem, "Items")
        Me.lstItem.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(352, 131)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(136, 28)
        Me.Label5.TabIndex = 32
        Me.Label5.Text = "Departments"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(22, 131)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(93, 28)
        Me.Label4.TabIndex = 32
        Me.Label4.Text = "Location"
        '
        'Label74
        '
        Me.Label74.AutoSize = True
        Me.Label74.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label74.Location = New System.Drawing.Point(32, 22)
        Me.Label74.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label74.Name = "Label74"
        Me.Label74.Size = New System.Drawing.Size(68, 28)
        Me.Label74.TabIndex = 32
        Me.Label74.Text = "Period"
        '
        'txtItemList
        '
        Me.txtItemList.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtItemList.Location = New System.Drawing.Point(354, 735)
        Me.txtItemList.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtItemList.Name = "txtItemList"
        Me.txtItemList.Size = New System.Drawing.Size(594, 31)
        Me.txtItemList.TabIndex = 17
        Me.ToolTip1.SetToolTip(Me.txtItemList, "Item Search")
        '
        'cmbPeriod
        '
        Me.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPeriod.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbPeriod.FormattingEnabled = True
        Me.cmbPeriod.Items.AddRange(New Object() {"Today", "Yesterday", "Current Week", "Current Month", "Current Year"})
        Me.cmbPeriod.Location = New System.Drawing.Point(38, 52)
        Me.cmbPeriod.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(205, 33)
        Me.cmbPeriod.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbPeriod, "Period")
        '
        'Label72
        '
        Me.Label72.AutoSize = True
        Me.Label72.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label72.Location = New System.Drawing.Point(350, 708)
        Me.Label72.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label72.Name = "Label72"
        Me.Label72.Size = New System.Drawing.Size(105, 25)
        Me.Label72.TabIndex = 29
        Me.Label72.Text = "Item Search"
        '
        'toDateTimePicker
        '
        Me.toDateTimePicker.CustomFormat = "dd/MMM/yyyy"
        Me.toDateTimePicker.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.toDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.toDateTimePicker.Location = New System.Drawing.Point(506, 52)
        Me.toDateTimePicker.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.toDateTimePicker.Name = "toDateTimePicker"
        Me.toDateTimePicker.Size = New System.Drawing.Size(205, 30)
        Me.toDateTimePicker.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.toDateTimePicker, "To Date")
        '
        'lstInventoryDepartment
        '
        Me.lstInventoryDepartment.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstInventoryDepartment.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstInventoryDepartment.BackColor = System.Drawing.Color.Transparent
        Me.lstInventoryDepartment.disableWhenChecked = False
        Me.lstInventoryDepartment.HeadingLabelName = "lstEmpDepartment"
        Me.lstInventoryDepartment.HeadingText = ""
        Me.lstInventoryDepartment.Location = New System.Drawing.Point(354, 138)
        Me.lstInventoryDepartment.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstInventoryDepartment.Name = "lstInventoryDepartment"
        Me.lstInventoryDepartment.ShowAddNewButton = False
        Me.lstInventoryDepartment.ShowInverse = True
        Me.lstInventoryDepartment.ShowMagnifierButton = False
        Me.lstInventoryDepartment.ShowNoCheck = False
        Me.lstInventoryDepartment.ShowResetAllButton = False
        Me.lstInventoryDepartment.ShowSelectall = True
        Me.lstInventoryDepartment.Size = New System.Drawing.Size(303, 274)
        Me.lstInventoryDepartment.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.lstInventoryDepartment, "Department")
        Me.lstInventoryDepartment.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'fromDateTimePicker
        '
        Me.fromDateTimePicker.CustomFormat = "dd/MMM/yyyy"
        Me.fromDateTimePicker.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.fromDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.fromDateTimePicker.Location = New System.Drawing.Point(272, 54)
        Me.fromDateTimePicker.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.fromDateTimePicker.Name = "fromDateTimePicker"
        Me.fromDateTimePicker.Size = New System.Drawing.Size(205, 30)
        Me.fromDateTimePicker.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.fromDateTimePicker, "Form Date")
        '
        'lstLocation
        '
        Me.lstLocation.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstLocation.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstLocation.BackColor = System.Drawing.Color.Transparent
        Me.lstLocation.disableWhenChecked = False
        Me.lstLocation.HeadingLabelName = "lstEmpDepartment"
        Me.lstLocation.HeadingText = ""
        Me.lstLocation.Location = New System.Drawing.Point(27, 138)
        Me.lstLocation.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstLocation.Name = "lstLocation"
        Me.lstLocation.ShowAddNewButton = False
        Me.lstLocation.ShowInverse = True
        Me.lstLocation.ShowMagnifierButton = False
        Me.lstLocation.ShowNoCheck = False
        Me.lstLocation.ShowResetAllButton = False
        Me.lstLocation.ShowSelectall = True
        Me.lstLocation.Size = New System.Drawing.Size(303, 274)
        Me.lstLocation.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.lstLocation, "Location")
        Me.lstLocation.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Location = New System.Drawing.Point(18, 117)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1764, 2)
        Me.Panel2.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(501, 20)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(78, 28)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "To Date"
        '
        'lstInventoryCategory
        '
        Me.lstInventoryCategory.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstInventoryCategory.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstInventoryCategory.BackColor = System.Drawing.Color.Transparent
        Me.lstInventoryCategory.disableWhenChecked = False
        Me.lstInventoryCategory.HeadingLabelName = "lstEmpDepartment"
        Me.lstInventoryCategory.HeadingText = ""
        Me.lstInventoryCategory.Location = New System.Drawing.Point(27, 422)
        Me.lstInventoryCategory.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstInventoryCategory.Name = "lstInventoryCategory"
        Me.lstInventoryCategory.ShowAddNewButton = False
        Me.lstInventoryCategory.ShowInverse = True
        Me.lstInventoryCategory.ShowMagnifierButton = False
        Me.lstInventoryCategory.ShowNoCheck = False
        Me.lstInventoryCategory.ShowResetAllButton = False
        Me.lstInventoryCategory.ShowSelectall = True
        Me.lstInventoryCategory.Size = New System.Drawing.Size(303, 274)
        Me.lstInventoryCategory.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.lstInventoryCategory, "Category")
        Me.lstInventoryCategory.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(267, 20)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 28)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "From Date"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1800, 86)
        Me.Panel4.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 21.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(8, 14)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(465, 48)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Cost Comparison Report"
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.Teal
        Me.Panel3.Controls.Add(Me.btnShow)
        Me.Panel3.Controls.Add(Me.btnPrint)
        Me.Panel3.Controls.Add(Me.btnRefresh)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 922)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1800, 102)
        Me.Panel3.TabIndex = 18
        '
        'btnShow
        '
        Me.btnShow.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnShow.BackColor = System.Drawing.Color.Teal
        Me.btnShow.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnShow.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnShow.ForeColor = System.Drawing.Color.White
        Me.btnShow.Location = New System.Drawing.Point(1550, 29)
        Me.btnShow.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(112, 51)
        Me.btnShow.TabIndex = 1
        Me.btnShow.Text = "Show"
        Me.ToolTip1.SetToolTip(Me.btnShow, "Show")
        Me.btnShow.UseVisualStyleBackColor = False
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.BackColor = System.Drawing.Color.Teal
        Me.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPrint.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPrint.ForeColor = System.Drawing.Color.White
        Me.btnPrint.Location = New System.Drawing.Point(1672, 29)
        Me.btnPrint.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(112, 51)
        Me.btnPrint.TabIndex = 2
        Me.btnPrint.Text = "Print"
        Me.ToolTip1.SetToolTip(Me.btnPrint, "Print")
        Me.btnPrint.UseVisualStyleBackColor = False
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.BackColor = System.Drawing.Color.Teal
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRefresh.ForeColor = System.Drawing.Color.White
        Me.btnRefresh.Location = New System.Drawing.Point(1418, 29)
        Me.btnRefresh.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(123, 51)
        Me.btnRefresh.TabIndex = 0
        Me.btnRefresh.Text = "Refresh"
        Me.ToolTip1.SetToolTip(Me.btnRefresh, "Refresh (F5)")
        Me.btnRefresh.UseVisualStyleBackColor = False
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.CtrlGrdBar1)
        Me.UltraTabPageControl2.Controls.Add(Me.grdCostComparison)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-15000, -15385)
        Me.UltraTabPageControl2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1800, 1024)
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(1752, 0)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar1.MyGrid = Me.grdCostComparison
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(50, 38)
        Me.CtrlGrdBar1.TabIndex = 3
        Me.CtrlGrdBar1.TabStop = False
        '
        'grdCostComparison
        '
        Me.grdCostComparison.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        grdCostComparison_DesignTimeLayout.LayoutString = resources.GetString("grdCostComparison_DesignTimeLayout.LayoutString")
        Me.grdCostComparison.DesignTimeLayout = grdCostComparison_DesignTimeLayout
        Me.grdCostComparison.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdCostComparison.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdCostComparison.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdCostComparison.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdCostComparison.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.grdCostComparison.Location = New System.Drawing.Point(0, 0)
        Me.grdCostComparison.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdCostComparison.Name = "grdCostComparison"
        Me.grdCostComparison.RecordNavigator = True
        Me.grdCostComparison.Size = New System.Drawing.Size(1800, 1024)
        Me.grdCostComparison.TabIndex = 2
        Me.grdCostComparison.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.UltraTabControl1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1802, 1050)
        Me.Panel1.TabIndex = 4
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.UltraTabControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(1802, 1050)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Flat
        Me.UltraTabControl1.TabIndex = 5
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Criteria"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "Result"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1800, 1024)
        '
        'frmStockComparisonReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1802, 1050)
        Me.Controls.Add(Me.Panel1)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmStockComparisonReport"
        Me.Text = "Cost Comparison Report"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdCostComparison, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents Label74 As System.Windows.Forms.Label
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents Label72 As System.Windows.Forms.Label
    Friend WithEvents toDateTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents fromDateTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents lstItem As SimpleAccounts.uiListControl
    Friend WithEvents lstInventoryCategory As SimpleAccounts.uiListControl
    Friend WithEvents lstInventoryType As SimpleAccounts.uiListControl
    Friend WithEvents lstInventoryDepartment As SimpleAccounts.uiListControl
    Friend WithEvents lstCompany As SimpleAccounts.uiListControl
    Friend WithEvents lstLocation As SimpleAccounts.uiListControl
    Friend WithEvents txtItemList As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grdCostComparison As Janus.Windows.GridEX.GridEX
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGrdRptAssetsDetail
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
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGrdRptAssetsDetail))
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.cmbLocation = New SimpleAccounts.uiListControl()
        Me.cmbCondition = New SimpleAccounts.uiListControl()
        Me.cmbStatus = New SimpleAccounts.uiListControl()
        Me.cmbType = New SimpleAccounts.uiListControl()
        Me.cmbCategory = New SimpleAccounts.uiListControl()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.CtrlGrdBar2 = New SimpleAccounts.CtrlGrdBar()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox3)
        Me.UltraTabPageControl1.Controls.Add(Me.btnShow)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox2)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox1)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbLocation)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbCondition)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbStatus)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbType)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbCategory)
        Me.UltraTabPageControl1.Controls.Add(Me.ToolStrip1)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1249, 907)
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Font = New System.Drawing.Font("Times New Roman", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlHeader.Location = New System.Drawing.Point(0, 32)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1249, 63)
        Me.pnlHeader.TabIndex = 11
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(6, 14)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(281, 36)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Asset Detail Report"
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.dtpTo)
        Me.GroupBox3.Controls.Add(Me.dtpFrom)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Location = New System.Drawing.Point(18, 115)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox3.Size = New System.Drawing.Size(248, 123)
        Me.GroupBox3.TabIndex = 2
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Date Range"
        '
        'dtpTo
        '
        Me.dtpTo.Checked = False
        Me.dtpTo.CustomFormat = "dd/MMM/yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(74, 71)
        Me.dtpTo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.ShowCheckBox = True
        Me.dtpTo.Size = New System.Drawing.Size(156, 26)
        Me.dtpTo.TabIndex = 3
        '
        'dtpFrom
        '
        Me.dtpFrom.Checked = False
        Me.dtpFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(74, 31)
        Me.dtpFrom.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.ShowCheckBox = True
        Me.dtpFrom.Size = New System.Drawing.Size(156, 26)
        Me.dtpFrom.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(18, 75)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(27, 20)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "To"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(18, 35)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(46, 20)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "From"
        '
        'btnShow
        '
        Me.btnShow.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.btnShow.Location = New System.Drawing.Point(687, 782)
        Me.btnShow.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(130, 71)
        Me.btnShow.TabIndex = 10
        Me.btnShow.Text = "Show"
        Me.btnShow.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.ComboBox2)
        Me.GroupBox2.Controls.Add(Me.ComboBox1)
        Me.GroupBox2.Location = New System.Drawing.Point(532, 115)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Size = New System.Drawing.Size(488, 123)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(18, 77)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(79, 20)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Employee"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 35)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(61, 20)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Vendor"
        '
        'ComboBox2
        '
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Location = New System.Drawing.Point(118, 71)
        Me.ComboBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(328, 28)
        Me.ComboBox2.TabIndex = 3
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(118, 29)
        Me.ComboBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(328, 28)
        Me.ComboBox1.TabIndex = 1
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.TextBox2)
        Me.GroupBox1.Controls.Add(Me.TextBox1)
        Me.GroupBox1.Location = New System.Drawing.Point(273, 115)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(250, 123)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Purchase Price"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 75)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "To"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 35)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "From"
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(72, 69)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(158, 26)
        Me.TextBox2.TabIndex = 3
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(72, 29)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(158, 26)
        Me.TextBox1.TabIndex = 1
        '
        'cmbLocation
        '
        Me.cmbLocation.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.cmbLocation.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.cmbLocation.BackColor = System.Drawing.Color.Transparent
        Me.cmbLocation.disableWhenChecked = False
        Me.cmbLocation.HeadingLabelName = Nothing
        Me.cmbLocation.HeadingText = "Location"
        Me.cmbLocation.Location = New System.Drawing.Point(826, 269)
        Me.cmbLocation.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.cmbLocation.Name = "cmbLocation"
        Me.cmbLocation.ShowAddNewButton = False
        Me.cmbLocation.ShowInverse = True
        Me.cmbLocation.ShowMagnifierButton = False
        Me.cmbLocation.ShowNoCheck = False
        Me.cmbLocation.ShowResetAllButton = False
        Me.cmbLocation.ShowSelectall = True
        Me.cmbLocation.Size = New System.Drawing.Size(194, 466)
        Me.cmbLocation.TabIndex = 9
        Me.cmbLocation.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'cmbCondition
        '
        Me.cmbCondition.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.cmbCondition.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.cmbCondition.BackColor = System.Drawing.Color.Transparent
        Me.cmbCondition.disableWhenChecked = False
        Me.cmbCondition.HeadingLabelName = Nothing
        Me.cmbCondition.HeadingText = "Asset Condition"
        Me.cmbCondition.Location = New System.Drawing.Point(624, 269)
        Me.cmbCondition.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.cmbCondition.Name = "cmbCondition"
        Me.cmbCondition.ShowAddNewButton = False
        Me.cmbCondition.ShowInverse = True
        Me.cmbCondition.ShowMagnifierButton = False
        Me.cmbCondition.ShowNoCheck = False
        Me.cmbCondition.ShowResetAllButton = False
        Me.cmbCondition.ShowSelectall = True
        Me.cmbCondition.Size = New System.Drawing.Size(194, 466)
        Me.cmbCondition.TabIndex = 8
        Me.cmbCondition.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'cmbStatus
        '
        Me.cmbStatus.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.cmbStatus.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.cmbStatus.BackColor = System.Drawing.Color.Transparent
        Me.cmbStatus.disableWhenChecked = False
        Me.cmbStatus.HeadingLabelName = Nothing
        Me.cmbStatus.HeadingText = "Asset Status"
        Me.cmbStatus.Location = New System.Drawing.Point(422, 269)
        Me.cmbStatus.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.ShowAddNewButton = False
        Me.cmbStatus.ShowInverse = True
        Me.cmbStatus.ShowMagnifierButton = False
        Me.cmbStatus.ShowNoCheck = False
        Me.cmbStatus.ShowResetAllButton = False
        Me.cmbStatus.ShowSelectall = True
        Me.cmbStatus.Size = New System.Drawing.Size(194, 466)
        Me.cmbStatus.TabIndex = 7
        Me.cmbStatus.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'cmbType
        '
        Me.cmbType.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.cmbType.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.cmbType.BackColor = System.Drawing.Color.Transparent
        Me.cmbType.disableWhenChecked = False
        Me.cmbType.HeadingLabelName = Nothing
        Me.cmbType.HeadingText = "Asset Type"
        Me.cmbType.Location = New System.Drawing.Point(219, 269)
        Me.cmbType.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.cmbType.Name = "cmbType"
        Me.cmbType.ShowAddNewButton = False
        Me.cmbType.ShowInverse = True
        Me.cmbType.ShowMagnifierButton = False
        Me.cmbType.ShowNoCheck = False
        Me.cmbType.ShowResetAllButton = False
        Me.cmbType.ShowSelectall = True
        Me.cmbType.Size = New System.Drawing.Size(194, 466)
        Me.cmbType.TabIndex = 6
        Me.cmbType.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'cmbCategory
        '
        Me.cmbCategory.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.cmbCategory.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.cmbCategory.BackColor = System.Drawing.Color.Transparent
        Me.cmbCategory.disableWhenChecked = False
        Me.cmbCategory.HeadingLabelName = Nothing
        Me.cmbCategory.HeadingText = "Asset Category"
        Me.cmbCategory.Location = New System.Drawing.Point(16, 269)
        Me.cmbCategory.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.cmbCategory.Name = "cmbCategory"
        Me.cmbCategory.ShowAddNewButton = False
        Me.cmbCategory.ShowInverse = True
        Me.cmbCategory.ShowMagnifierButton = False
        Me.cmbCategory.ShowNoCheck = False
        Me.cmbCategory.ShowResetAllButton = False
        Me.cmbCategory.ShowSelectall = True
        Me.cmbCategory.Size = New System.Drawing.Size(194, 466)
        Me.cmbCategory.TabIndex = 5
        Me.cmbCategory.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1249, 32)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 29)
        Me.btnRefresh.Text = "Refresh"
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.CtrlGrdBar2)
        Me.UltraTabPageControl2.Controls.Add(Me.CtrlGrdBar1)
        Me.UltraTabPageControl2.Controls.Add(Me.grd)
        Me.UltraTabPageControl2.Controls.Add(Me.ToolStrip2)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1249, 907)
        '
        'CtrlGrdBar2
        '
        Me.CtrlGrdBar2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar2.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar2.Email = Nothing
        Me.CtrlGrdBar2.FormName = Me
        Me.CtrlGrdBar2.Location = New System.Drawing.Point(1193, 3)
        Me.CtrlGrdBar2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar2.MyGrid = Me.grd
        Me.CtrlGrdBar2.Name = "CtrlGrdBar2"
        Me.CtrlGrdBar2.Size = New System.Drawing.Size(57, 38)
        Me.CtrlGrdBar2.TabIndex = 17
        '
        'grd
        '
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.Location = New System.Drawing.Point(0, 40)
        Me.grd.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(1414, 875)
        Me.grd.TabIndex = 0
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(1359, 0)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar1.MyGrid = Me.grd
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(57, 38)
        Me.CtrlGrdBar1.TabIndex = 2
        '
        'ToolStrip2
        '
        Me.ToolStrip2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ToolStrip2.AutoSize = False
        Me.ToolStrip2.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip2.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip2.Size = New System.Drawing.Size(1251, 38)
        Me.ToolStrip2.TabIndex = 0
        Me.ToolStrip2.Text = "ToolStrip2"
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
        Me.UltraTabControl1.Size = New System.Drawing.Size(1251, 934)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 0
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Criteria"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "Result"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.VisualStudio2005
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1249, 907)
        '
        'BackgroundWorker1
        '
        '
        'frmGrdRptAssetsDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1251, 934)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmGrdRptAssetsDetail"
        Me.Text = "Assets Detail Report"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents ToolStrip2 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbType As SimpleAccounts.uiListControl
    Friend WithEvents cmbCategory As SimpleAccounts.uiListControl
    Friend WithEvents cmbLocation As SimpleAccounts.uiListControl
    Friend WithEvents cmbCondition As SimpleAccounts.uiListControl
    Friend WithEvents cmbStatus As SimpleAccounts.uiListControl
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents ComboBox2 As System.Windows.Forms.ComboBox
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents CtrlGrdBar2 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

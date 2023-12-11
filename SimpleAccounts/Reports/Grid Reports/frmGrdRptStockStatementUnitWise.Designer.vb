<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGrdRptStockStatementUnitWise
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGrdRptStockStatementUnitWise))
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.ToolStrip3 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.CtrlGrdBar2 = New SimpleAccounts.CtrlGrdBar()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.ToolStrip4 = New System.Windows.Forms.ToolStrip()
        Me.btnBack = New System.Windows.Forms.ToolStripButton()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.rbtItemCode = New System.Windows.Forms.RadioButton()
        Me.rbtItemName = New System.Windows.Forms.RadioButton()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.lblFrom = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.ToolStrip3.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip4.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.Button2)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox5)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox6)
        Me.UltraTabPageControl1.Controls.Add(Me.ToolStrip3)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1339, 992)
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 25)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1339, 65)
        Me.pnlHeader.TabIndex = 83
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(16, 14)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(381, 36)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Stock Statement Unit Wise"
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(250, 540)
        Me.Button2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(112, 35)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Show"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox5.Controls.Add(Me.ListBox1)
        Me.GroupBox5.Location = New System.Drawing.Point(18, 289)
        Me.GroupBox5.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox5.Size = New System.Drawing.Size(358, 242)
        Me.GroupBox5.TabIndex = 3
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Display Column"
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 20
        Me.ListBox1.Items.AddRange(New Object() {"Purchase", "Purchase Return", "Stock Receiving", "Sales", "Sales Return", "Stock Dispatch", "Store Issuance", "Production"})
        Me.ListBox1.Location = New System.Drawing.Point(15, 31)
        Me.ListBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
        Me.ListBox1.Size = New System.Drawing.Size(328, 184)
        Me.ListBox1.TabIndex = 4
        '
        'GroupBox6
        '
        Me.GroupBox6.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox6.Controls.Add(Me.Label7)
        Me.GroupBox6.Controls.Add(Me.cmbPeriod)
        Me.GroupBox6.Controls.Add(Me.Label8)
        Me.GroupBox6.Controls.Add(Me.Label9)
        Me.GroupBox6.Controls.Add(Me.dtpToDate)
        Me.GroupBox6.Controls.Add(Me.dtpFromDate)
        Me.GroupBox6.Location = New System.Drawing.Point(18, 115)
        Me.GroupBox6.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox6.Size = New System.Drawing.Size(358, 165)
        Me.GroupBox6.TabIndex = 2
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Date Range"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(15, 31)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(54, 20)
        Me.Label7.TabIndex = 0
        Me.Label7.Text = "Period"
        '
        'cmbPeriod
        '
        Me.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPeriod.FormattingEnabled = True
        Me.cmbPeriod.Items.AddRange(New Object() {"Today", "Yesterday", "Current Week", "Current Month", "Current Year"})
        Me.cmbPeriod.Location = New System.Drawing.Point(123, 26)
        Me.cmbPeriod.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(220, 28)
        Me.cmbPeriod.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(15, 117)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(27, 20)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "To"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(15, 74)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(46, 20)
        Me.Label9.TabIndex = 2
        Me.Label9.Text = "From"
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(123, 108)
        Me.dtpToDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(220, 26)
        Me.dtpToDate.TabIndex = 5
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(123, 68)
        Me.dtpFromDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(220, 26)
        Me.dtpFromDate.TabIndex = 3
        '
        'ToolStrip3
        '
        Me.ToolStrip3.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip3.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripProgressBar1})
        Me.ToolStrip3.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip3.Name = "ToolStrip3"
        Me.ToolStrip3.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip3.Size = New System.Drawing.Size(1339, 25)
        Me.ToolStrip3.TabIndex = 0
        Me.ToolStrip3.Text = "ToolStrip3"
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        Me.ToolStripProgressBar1.Size = New System.Drawing.Size(300, 34)
        Me.ToolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ToolStripProgressBar1.Visible = False
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.CtrlGrdBar2)
        Me.UltraTabPageControl2.Controls.Add(Me.grd)
        Me.UltraTabPageControl2.Controls.Add(Me.CtrlGrdBar1)
        Me.UltraTabPageControl2.Controls.Add(Me.ToolStrip4)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-15000, -15385)
        Me.UltraTabPageControl2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1339, 992)
        '
        'CtrlGrdBar2
        '
        Me.CtrlGrdBar2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar2.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar2.Email = Nothing
        Me.CtrlGrdBar2.FormName = Me
        Me.CtrlGrdBar2.Location = New System.Drawing.Point(1281, 2)
        Me.CtrlGrdBar2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar2.MyGrid = Me.grd
        Me.CtrlGrdBar2.Name = "CtrlGrdBar2"
        Me.CtrlGrdBar2.Size = New System.Drawing.Size(57, 38)
        Me.CtrlGrdBar2.TabIndex = 3
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
        Me.grd.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.Location = New System.Drawing.Point(0, 42)
        Me.grd.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(1339, 959)
        Me.grd.TabIndex = 2
        Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Nothing
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(1353, 2)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar1.MyGrid = Me.grd
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(57, 38)
        Me.CtrlGrdBar1.TabIndex = 1
        '
        'ToolStrip4
        '
        Me.ToolStrip4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ToolStrip4.AutoSize = False
        Me.ToolStrip4.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip4.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip4.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnBack, Me.btnRefresh})
        Me.ToolStrip4.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip4.Name = "ToolStrip4"
        Me.ToolStrip4.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip4.Size = New System.Drawing.Size(1281, 38)
        Me.ToolStrip4.TabIndex = 0
        Me.ToolStrip4.Text = "ToolStrip4"
        '
        'btnBack
        '
        Me.btnBack.Image = Global.SimpleAccounts.My.Resources.Resources.back
        Me.btnBack.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(76, 35)
        Me.btnBack.Text = "Back"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 35)
        Me.btnRefresh.Text = "Refresh"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.rbtItemCode)
        Me.GroupBox2.Controls.Add(Me.rbtItemName)
        Me.GroupBox2.Controls.Add(Me.btnShow)
        Me.GroupBox2.Location = New System.Drawing.Point(257, 75)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(207, 98)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        '
        'rbtItemCode
        '
        Me.rbtItemCode.AutoSize = True
        Me.rbtItemCode.Location = New System.Drawing.Point(113, 20)
        Me.rbtItemCode.Name = "rbtItemCode"
        Me.rbtItemCode.Size = New System.Drawing.Size(130, 24)
        Me.rbtItemCode.TabIndex = 1
        Me.rbtItemCode.Text = "By Item Code"
        Me.rbtItemCode.UseVisualStyleBackColor = True
        '
        'rbtItemName
        '
        Me.rbtItemName.AutoSize = True
        Me.rbtItemName.Checked = True
        Me.rbtItemName.Location = New System.Drawing.Point(16, 20)
        Me.rbtItemName.Name = "rbtItemName"
        Me.rbtItemName.Size = New System.Drawing.Size(134, 24)
        Me.rbtItemName.TabIndex = 0
        Me.rbtItemName.TabStop = True
        Me.rbtItemName.Text = "By Item Name"
        Me.rbtItemName.UseVisualStyleBackColor = True
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(126, 67)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(75, 23)
        Me.btnShow.TabIndex = 2
        Me.btnShow.Text = "Show"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(200, 100)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 20)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(40, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Period:"
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.Location = New System.Drawing.Point(10, 76)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(20, 13)
        Me.lblTo.TabIndex = 4
        Me.lblTo.Text = "To"
        '
        'lblFrom
        '
        Me.lblFrom.AutoSize = True
        Me.lblFrom.Location = New System.Drawing.Point(10, 48)
        Me.lblFrom.Name = "lblFrom"
        Me.lblFrom.Size = New System.Drawing.Size(30, 13)
        Me.lblFrom.TabIndex = 2
        Me.lblFrom.Text = "From"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Navy
        Me.Label1.Location = New System.Drawing.Point(9, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(272, 23)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Saleman Demand Detail"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(721, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.RadioButton1)
        Me.GroupBox3.Controls.Add(Me.RadioButton2)
        Me.GroupBox3.Controls.Add(Me.Button1)
        Me.GroupBox3.Location = New System.Drawing.Point(257, 75)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(207, 98)
        Me.GroupBox3.TabIndex = 3
        Me.GroupBox3.TabStop = False
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(113, 20)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(130, 24)
        Me.RadioButton1.TabIndex = 1
        Me.RadioButton1.Text = "By Item Code"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Checked = True
        Me.RadioButton2.Location = New System.Drawing.Point(16, 20)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(134, 24)
        Me.RadioButton2.TabIndex = 0
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "By Item Name"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(126, 67)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "Show"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label2)
        Me.GroupBox4.Controls.Add(Me.ComboBox1)
        Me.GroupBox4.Controls.Add(Me.Label3)
        Me.GroupBox4.Controls.Add(Me.Label5)
        Me.GroupBox4.Controls.Add(Me.DateTimePicker1)
        Me.GroupBox4.Controls.Add(Me.DateTimePicker2)
        Me.GroupBox4.Location = New System.Drawing.Point(12, 75)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(239, 98)
        Me.GroupBox4.TabIndex = 2
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Date Range"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 20)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(58, 20)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Period:"
        '
        'ComboBox1
        '
        Me.ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"Today", "Yesterday", "Current Week", "Current Month", "Current Year"})
        Me.ComboBox1.Location = New System.Drawing.Point(82, 17)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(148, 28)
        Me.ComboBox1.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 76)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(27, 20)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "To"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(10, 48)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(46, 20)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "From"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.CustomFormat = "dd/MMM/yyyy"
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(82, 70)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.Size = New System.Drawing.Size(148, 26)
        Me.DateTimePicker1.TabIndex = 5
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.CustomFormat = "dd/MMM/yyyy"
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2.Location = New System.Drawing.Point(82, 44)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.Size = New System.Drawing.Size(148, 26)
        Me.DateTimePicker2.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Navy
        Me.Label6.Location = New System.Drawing.Point(9, 38)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(272, 23)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "Saleman Demand Detail"
        '
        'ToolStrip2
        '
        Me.ToolStrip2.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(721, 25)
        Me.ToolStrip2.TabIndex = 0
        Me.ToolStrip2.Text = "ToolStrip1"
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1339, 992)
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
        Me.UltraTabControl1.Size = New System.Drawing.Size(1341, 1018)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 1
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Criteria"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "Result"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.VisualStudio2005
        '
        'frmGrdRptStockStatementUnitWise
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1341, 1018)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmGrdRptStockStatementUnitWise"
        Me.Text = "Stock Statement Unit Wise"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox6.ResumeLayout(False)
        Me.GroupBox6.PerformLayout()
        Me.ToolStrip3.ResumeLayout(False)
        Me.ToolStrip3.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip4.ResumeLayout(False)
        Me.ToolStrip4.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rbtItemCode As System.Windows.Forms.RadioButton
    Friend WithEvents rbtItemName As System.Windows.Forms.RadioButton
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    'Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents lblFrom As System.Windows.Forms.Label
    'Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    'Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ComboBox1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ToolStrip2 As System.Windows.Forms.ToolStrip
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents ToolStrip4 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnBack As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents ToolStrip3 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents CtrlGrdBar2 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmApplySalePriceUtility
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmApplySalePriceUtility))
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblProcessNo = New System.Windows.Forms.Label()
        Me.UltraProgressBar1 = New Infragistics.Win.UltraWinProgressBar.UltraProgressBar()
        Me.txtProcessNo = New System.Windows.Forms.TextBox()
        Me.gbItem = New System.Windows.Forms.GroupBox()
        Me.dtpDateFrom = New System.Windows.Forms.DateTimePicker()
        Me.rbName = New System.Windows.Forms.RadioButton()
        Me.dtpDateTo = New System.Windows.Forms.DateTimePicker()
        Me.rbCode = New System.Windows.Forms.RadioButton()
        Me.lblLoanPolicy = New System.Windows.Forms.Label()
        Me.cmbItem = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.gbApply = New System.Windows.Forms.GroupBox()
        Me.txtSalePrice = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtNewSalePrice = New System.Windows.Forms.TextBox()
        Me.lblSalePrice = New System.Windows.Forms.Label()
        Me.btnAlter = New System.Windows.Forms.Button()
        Me.dtpProcessDate = New System.Windows.Forms.DateTimePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnHelp = New System.Windows.Forms.ToolStripButton()
        Me.lblProgressbar = New System.Windows.Forms.ToolStripLabel()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.CtrlGrdBar2 = New SimpleAccounts.CtrlGrdBar()
        Me.grdSaved = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.UltraTabPageControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.gbItem.SuspendLayout()
        CType(Me.cmbItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbApply.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip2.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.CtrlGrdBar1)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox1)
        Me.UltraTabPageControl1.Controls.Add(Me.grd)
        Me.UltraTabPageControl1.Controls.Add(Me.ToolStrip1)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1372, 893)
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 32)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1372, 65)
        Me.pnlHeader.TabIndex = 26
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(10, 9)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(318, 36)
        Me.lblHeader.TabIndex = 2
        Me.lblHeader.Text = "Sale Price Application"
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(1324, -2)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar1.MyGrid = Me.grd
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(51, 38)
        Me.CtrlGrdBar1.TabIndex = 1
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
        Me.grd.Location = New System.Drawing.Point(2, 502)
        Me.grd.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(1369, 398)
        Me.grd.TabIndex = 5
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.lblProcessNo)
        Me.GroupBox1.Controls.Add(Me.UltraProgressBar1)
        Me.GroupBox1.Controls.Add(Me.txtProcessNo)
        Me.GroupBox1.Controls.Add(Me.gbItem)
        Me.GroupBox1.Controls.Add(Me.gbApply)
        Me.GroupBox1.Controls.Add(Me.dtpProcessDate)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 88)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(1339, 405)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        '
        'lblProcessNo
        '
        Me.lblProcessNo.AutoSize = True
        Me.lblProcessNo.Location = New System.Drawing.Point(20, 69)
        Me.lblProcessNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProcessNo.Name = "lblProcessNo"
        Me.lblProcessNo.Size = New System.Drawing.Size(90, 20)
        Me.lblProcessNo.TabIndex = 2
        Me.lblProcessNo.Text = "Process No"
        '
        'UltraProgressBar1
        '
        Me.UltraProgressBar1.Location = New System.Drawing.Point(534, 31)
        Me.UltraProgressBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraProgressBar1.Name = "UltraProgressBar1"
        Me.UltraProgressBar1.Size = New System.Drawing.Size(804, 35)
        Me.UltraProgressBar1.TabIndex = 3
        Me.UltraProgressBar1.Text = "[Formatted]"
        '
        'txtProcessNo
        '
        Me.txtProcessNo.Enabled = False
        Me.txtProcessNo.Location = New System.Drawing.Point(128, 65)
        Me.txtProcessNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtProcessNo.Name = "txtProcessNo"
        Me.txtProcessNo.Size = New System.Drawing.Size(360, 26)
        Me.txtProcessNo.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtProcessNo, "Process No")
        '
        'gbItem
        '
        Me.gbItem.Controls.Add(Me.dtpDateFrom)
        Me.gbItem.Controls.Add(Me.rbName)
        Me.gbItem.Controls.Add(Me.dtpDateTo)
        Me.gbItem.Controls.Add(Me.rbCode)
        Me.gbItem.Controls.Add(Me.lblLoanPolicy)
        Me.gbItem.Controls.Add(Me.cmbItem)
        Me.gbItem.Controls.Add(Me.btnShow)
        Me.gbItem.Controls.Add(Me.Label4)
        Me.gbItem.Controls.Add(Me.Label5)
        Me.gbItem.Location = New System.Drawing.Point(10, 100)
        Me.gbItem.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.gbItem.Name = "gbItem"
        Me.gbItem.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.gbItem.Size = New System.Drawing.Size(1310, 175)
        Me.gbItem.TabIndex = 4
        Me.gbItem.TabStop = False
        Me.gbItem.Text = "Search"
        '
        'dtpDateFrom
        '
        Me.dtpDateFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpDateFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateFrom.Location = New System.Drawing.Point(117, 29)
        Me.dtpDateFrom.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpDateFrom.Name = "dtpDateFrom"
        Me.dtpDateFrom.Size = New System.Drawing.Size(360, 26)
        Me.dtpDateFrom.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.dtpDateFrom, "Date from to search item from.")
        '
        'rbName
        '
        Me.rbName.AutoSize = True
        Me.rbName.Location = New System.Drawing.Point(208, 106)
        Me.rbName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbName.Name = "rbName"
        Me.rbName.Size = New System.Drawing.Size(76, 24)
        Me.rbName.TabIndex = 5
        Me.rbName.Text = "Name"
        Me.ToolTip1.SetToolTip(Me.rbName, "Search by name when this check box is checked.")
        Me.rbName.UseVisualStyleBackColor = True
        '
        'dtpDateTo
        '
        Me.dtpDateTo.CustomFormat = "dd/MMM/yyyy"
        Me.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateTo.Location = New System.Drawing.Point(117, 68)
        Me.dtpDateTo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpDateTo.Name = "dtpDateTo"
        Me.dtpDateTo.Size = New System.Drawing.Size(360, 26)
        Me.dtpDateTo.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.dtpDateTo, "To Date to search date to")
        '
        'rbCode
        '
        Me.rbCode.AutoSize = True
        Me.rbCode.Checked = True
        Me.rbCode.Location = New System.Drawing.Point(124, 106)
        Me.rbCode.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbCode.Name = "rbCode"
        Me.rbCode.Size = New System.Drawing.Size(72, 24)
        Me.rbCode.TabIndex = 4
        Me.rbCode.TabStop = True
        Me.rbCode.Text = "Code"
        Me.ToolTip1.SetToolTip(Me.rbCode, "Search by code when is this check is checked.")
        Me.rbCode.UseVisualStyleBackColor = True
        '
        'lblLoanPolicy
        '
        Me.lblLoanPolicy.AutoSize = True
        Me.lblLoanPolicy.Location = New System.Drawing.Point(9, 140)
        Me.lblLoanPolicy.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblLoanPolicy.Name = "lblLoanPolicy"
        Me.lblLoanPolicy.Size = New System.Drawing.Size(41, 20)
        Me.lblLoanPolicy.TabIndex = 6
        Me.lblLoanPolicy.Text = "Item"
        '
        'cmbItem
        '
        Me.cmbItem.CheckedListSettings.CheckStateMember = ""
        Me.cmbItem.DisplayLayout.MaxColScrollRegions = 1
        Me.cmbItem.DisplayLayout.MaxRowScrollRegions = 1
        Me.cmbItem.DisplayLayout.Override.CellPadding = 0
        Me.cmbItem.Location = New System.Drawing.Point(117, 132)
        Me.cmbItem.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbItem.Name = "cmbItem"
        Me.cmbItem.Size = New System.Drawing.Size(362, 29)
        Me.cmbItem.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.cmbItem, "This drop down this items.")
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(488, 132)
        Me.btnShow.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(94, 35)
        Me.btnShow.TabIndex = 8
        Me.btnShow.Text = "Show"
        Me.ToolTip1.SetToolTip(Me.btnShow, "This button displays item history in below grid.")
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 38)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 20)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Date From"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 78)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 20)
        Me.Label5.TabIndex = 2
        Me.Label5.Text = "Date To"
        '
        'gbApply
        '
        Me.gbApply.Controls.Add(Me.txtSalePrice)
        Me.gbApply.Controls.Add(Me.Label9)
        Me.gbApply.Controls.Add(Me.txtNewSalePrice)
        Me.gbApply.Controls.Add(Me.lblSalePrice)
        Me.gbApply.Controls.Add(Me.btnAlter)
        Me.gbApply.Location = New System.Drawing.Point(14, 275)
        Me.gbApply.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.gbApply.Name = "gbApply"
        Me.gbApply.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.gbApply.Size = New System.Drawing.Size(1310, 98)
        Me.gbApply.TabIndex = 5
        Me.gbApply.TabStop = False
        Me.gbApply.Text = "Alteration"
        '
        'txtSalePrice
        '
        Me.txtSalePrice.Location = New System.Drawing.Point(20, 49)
        Me.txtSalePrice.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtSalePrice.Name = "txtSalePrice"
        Me.txtSalePrice.ReadOnly = True
        Me.txtSalePrice.Size = New System.Drawing.Size(168, 26)
        Me.txtSalePrice.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtSalePrice, "This field shows the sale price of article  definition.")
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(194, 25)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(115, 20)
        Me.Label9.TabIndex = 2
        Me.Label9.Text = "New Sale Price"
        '
        'txtNewSalePrice
        '
        Me.txtNewSalePrice.Location = New System.Drawing.Point(198, 49)
        Me.txtNewSalePrice.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtNewSalePrice.Name = "txtNewSalePrice"
        Me.txtNewSalePrice.Size = New System.Drawing.Size(157, 26)
        Me.txtNewSalePrice.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtNewSalePrice, "Price in this field is applied as new sale price after loading into grid.")
        '
        'lblSalePrice
        '
        Me.lblSalePrice.AutoSize = True
        Me.lblSalePrice.Location = New System.Drawing.Point(15, 25)
        Me.lblSalePrice.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSalePrice.Name = "lblSalePrice"
        Me.lblSalePrice.Size = New System.Drawing.Size(80, 20)
        Me.lblSalePrice.TabIndex = 0
        Me.lblSalePrice.Text = "Sale Price"
        '
        'btnAlter
        '
        Me.btnAlter.Location = New System.Drawing.Point(366, 46)
        Me.btnAlter.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnAlter.Name = "btnAlter"
        Me.btnAlter.Size = New System.Drawing.Size(108, 35)
        Me.btnAlter.TabIndex = 4
        Me.btnAlter.Text = "Alter"
        Me.ToolTip1.SetToolTip(Me.btnAlter, "This button apply new sale price to grid record.")
        Me.btnAlter.UseVisualStyleBackColor = True
        '
        'dtpProcessDate
        '
        Me.dtpProcessDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpProcessDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpProcessDate.Location = New System.Drawing.Point(128, 25)
        Me.dtpProcessDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpProcessDate.Name = "dtpProcessDate"
        Me.dtpProcessDate.Size = New System.Drawing.Size(360, 26)
        Me.dtpProcessDate.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.dtpProcessDate, "Process Date")
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(20, 31)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(105, 20)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Process Date"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnSave, Me.btnRefresh, Me.toolStripSeparator1, Me.btnHelp, Me.lblProgressbar})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1372, 32)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(75, 29)
        Me.btnNew.Text = "&New"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(77, 29)
        Me.btnSave.Text = "&Save"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 29)
        Me.btnRefresh.Text = "&Refresh"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 32)
        '
        'btnHelp
        '
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(77, 29)
        Me.btnHelp.Text = "He&lp"
        '
        'lblProgressbar
        '
        Me.lblProgressbar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.lblProgressbar.Name = "lblProgressbar"
        Me.lblProgressbar.Size = New System.Drawing.Size(0, 29)
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.CtrlGrdBar2)
        Me.UltraTabPageControl2.Controls.Add(Me.ToolStrip2)
        Me.UltraTabPageControl2.Controls.Add(Me.grdSaved)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1372, 893)
        '
        'CtrlGrdBar2
        '
        Me.CtrlGrdBar2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar2.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBar2.Email = Nothing
        Me.CtrlGrdBar2.FormName = Me
        Me.CtrlGrdBar2.Location = New System.Drawing.Point(1323, 0)
        Me.CtrlGrdBar2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar2.MyGrid = Me.grdSaved
        Me.CtrlGrdBar2.Name = "CtrlGrdBar2"
        Me.CtrlGrdBar2.Size = New System.Drawing.Size(51, 38)
        Me.CtrlGrdBar2.TabIndex = 20
        '
        'grdSaved
        '
        Me.grdSaved.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdSaved.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSaved.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdSaved.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdSaved.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdSaved.GroupByBoxVisible = False
        Me.grdSaved.Location = New System.Drawing.Point(0, 43)
        Me.grdSaved.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdSaved.Name = "grdSaved"
        Me.grdSaved.RecordNavigator = True
        Me.grdSaved.Size = New System.Drawing.Size(1372, 853)
        Me.grdSaved.TabIndex = 1
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'ToolStrip2
        '
        Me.ToolStrip2.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnDelete})
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip2.Size = New System.Drawing.Size(1372, 32)
        Me.ToolStrip2.TabIndex = 2
        Me.ToolStrip2.Text = "ToolStrip2"
        '
        'btnDelete
        '
        Me.btnDelete.Image = Global.SimpleAccounts.My.Resources.Resources.BtnDelete_Image
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(90, 29)
        Me.btnDelete.Text = "&Delete"
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.UltraTabControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(1374, 920)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 1
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Sale Price"
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
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1372, 893)
        '
        'frmApplySalePriceUtility
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1374, 920)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmApplySalePriceUtility"
        Me.Text = "Sale Price Application"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.gbItem.ResumeLayout(False)
        Me.gbItem.PerformLayout()
        CType(Me.cmbItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbApply.ResumeLayout(False)
        Me.gbApply.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        Me.UltraTabPageControl2.PerformLayout()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dtpDateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDateFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpProcessDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grdSaved As Janus.Windows.GridEX.GridEX
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbItem As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents lblLoanPolicy As System.Windows.Forms.Label
    Friend WithEvents btnAlter As System.Windows.Forms.Button
    Friend WithEvents txtNewSalePrice As System.Windows.Forms.TextBox
    Friend WithEvents txtSalePrice As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents lblSalePrice As System.Windows.Forms.Label
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents lblProgressbar As System.Windows.Forms.ToolStripLabel
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents UltraProgressBar1 As Infragistics.Win.UltraWinProgressBar.UltraProgressBar
    Friend WithEvents gbApply As System.Windows.Forms.GroupBox
    Friend WithEvents rbName As System.Windows.Forms.RadioButton
    Friend WithEvents rbCode As System.Windows.Forms.RadioButton
    Friend WithEvents lblProcessNo As System.Windows.Forms.Label
    Friend WithEvents txtProcessNo As System.Windows.Forms.TextBox
    Friend WithEvents gbItem As System.Windows.Forms.GroupBox
    Friend WithEvents ToolStrip2 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents CtrlGrdBar2 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

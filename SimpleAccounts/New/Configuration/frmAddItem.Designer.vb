<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddItem
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAddItem))
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.lstSize = New SimpleAccounts.uiListControl()
        Me.lstCombination = New SimpleAccounts.uiListControl()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdItemLocation = New Janus.Windows.GridEX.GridEX()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbltype = New System.Windows.Forms.Label()
        Me.cmbDepartment = New System.Windows.Forms.ComboBox()
        Me.cmbtype = New System.Windows.Forms.ComboBox()
        Me.cmbUnit = New System.Windows.Forms.ComboBox()
        Me.txtItemCode = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtItem = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblunit = New System.Windows.Forms.Label()
        Me.txtPuchasePrice = New System.Windows.Forms.TextBox()
        Me.txtSaleprice = New System.Windows.Forms.TextBox()
        Me.txtPackQty = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnHelp = New System.Windows.Forms.ToolStripButton()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtLargestPackQty = New System.Windows.Forms.TextBox()
        Me.btnAddDept = New System.Windows.Forms.Button()
        Me.btnAddType = New System.Windows.Forms.Button()
        Me.btnUnit = New System.Windows.Forms.Button()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lnkUploadPic = New System.Windows.Forms.LinkLabel()
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdItemLocation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.lstSize)
        Me.UltraTabPageControl1.Controls.Add(Me.lstCombination)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 20)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(489, 269)
        '
        'lstSize
        '
        Me.lstSize.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstSize.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstSize.BackColor = System.Drawing.Color.Transparent
        Me.lstSize.disableWhenChecked = False
        Me.lstSize.HeadingLabelName = "lblSize"
        Me.lstSize.HeadingText = "Size"
        Me.lstSize.Location = New System.Drawing.Point(9, 7)
        Me.lstSize.Name = "lstSize"
        Me.lstSize.ShowAddNewButton = False
        Me.lstSize.ShowInverse = True
        Me.lstSize.ShowMagnifierButton = False
        Me.lstSize.ShowNoCheck = False
        Me.lstSize.ShowResetAllButton = False
        Me.lstSize.ShowSelectall = True
        Me.lstSize.Size = New System.Drawing.Size(209, 245)
        Me.lstSize.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.lstSize, "Select Multiple Sizes")
        Me.lstSize.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstCombination
        '
        Me.lstCombination.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstCombination.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstCombination.BackColor = System.Drawing.Color.Transparent
        Me.lstCombination.disableWhenChecked = False
        Me.lstCombination.HeadingLabelName = "lblCombination"
        Me.lstCombination.HeadingText = "Combination"
        Me.lstCombination.Location = New System.Drawing.Point(239, 7)
        Me.lstCombination.Name = "lstCombination"
        Me.lstCombination.ShowAddNewButton = False
        Me.lstCombination.ShowInverse = True
        Me.lstCombination.ShowMagnifierButton = False
        Me.lstCombination.ShowNoCheck = False
        Me.lstCombination.ShowResetAllButton = False
        Me.lstCombination.ShowSelectall = True
        Me.lstCombination.Size = New System.Drawing.Size(209, 245)
        Me.lstCombination.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.lstCombination, "Select Multiple Combination")
        Me.lstCombination.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grdItemLocation)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(489, 269)
        '
        'grdItemLocation
        '
        Me.grdItemLocation.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdItemLocation.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdItemLocation.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdItemLocation.GroupByBoxVisible = False
        Me.grdItemLocation.Location = New System.Drawing.Point(0, 0)
        Me.grdItemLocation.Name = "grdItemLocation"
        Me.grdItemLocation.RecordNavigator = True
        Me.grdItemLocation.Size = New System.Drawing.Size(489, 269)
        Me.grdItemLocation.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.grdItemLocation, "Define Articles With Locations")
        Me.grdItemLocation.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(7, 8)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(165, 23)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Add New Item"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 75)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Department"
        '
        'lbltype
        '
        Me.lbltype.AutoSize = True
        Me.lbltype.Location = New System.Drawing.Point(12, 102)
        Me.lbltype.Name = "lbltype"
        Me.lbltype.Size = New System.Drawing.Size(31, 13)
        Me.lbltype.TabIndex = 5
        Me.lbltype.Text = "Type"
        '
        'cmbDepartment
        '
        Me.cmbDepartment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDepartment.FormattingEnabled = True
        Me.cmbDepartment.Location = New System.Drawing.Point(109, 72)
        Me.cmbDepartment.Name = "cmbDepartment"
        Me.cmbDepartment.Size = New System.Drawing.Size(170, 21)
        Me.cmbDepartment.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.cmbDepartment, "Select Article Department")
        '
        'cmbtype
        '
        Me.cmbtype.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbtype.FormattingEnabled = True
        Me.cmbtype.Location = New System.Drawing.Point(109, 99)
        Me.cmbtype.Name = "cmbtype"
        Me.cmbtype.Size = New System.Drawing.Size(170, 21)
        Me.cmbtype.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.cmbtype, "Select Article Type")
        '
        'cmbUnit
        '
        Me.cmbUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbUnit.FormattingEnabled = True
        Me.cmbUnit.Location = New System.Drawing.Point(109, 178)
        Me.cmbUnit.Name = "cmbUnit"
        Me.cmbUnit.Size = New System.Drawing.Size(170, 21)
        Me.cmbUnit.TabIndex = 13
        Me.ToolTip1.SetToolTip(Me.cmbUnit, "Select Article Unit")
        '
        'txtItemCode
        '
        Me.txtItemCode.Location = New System.Drawing.Point(109, 126)
        Me.txtItemCode.Name = "txtItemCode"
        Me.txtItemCode.Size = New System.Drawing.Size(170, 20)
        Me.txtItemCode.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.txtItemCode, "Item Code")
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 129)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(55, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Item Code"
        '
        'txtItem
        '
        Me.txtItem.Location = New System.Drawing.Point(109, 152)
        Me.txtItem.Name = "txtItem"
        Me.txtItem.Size = New System.Drawing.Size(170, 20)
        Me.txtItem.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.txtItem, "Item Name")
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 155)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(58, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Item Name"
        '
        'lblunit
        '
        Me.lblunit.AutoSize = True
        Me.lblunit.Location = New System.Drawing.Point(12, 181)
        Me.lblunit.Name = "lblunit"
        Me.lblunit.Size = New System.Drawing.Size(26, 13)
        Me.lblunit.TabIndex = 12
        Me.lblunit.Text = "Unit"
        '
        'txtPuchasePrice
        '
        Me.txtPuchasePrice.Location = New System.Drawing.Point(109, 251)
        Me.txtPuchasePrice.Name = "txtPuchasePrice"
        Me.txtPuchasePrice.Size = New System.Drawing.Size(70, 20)
        Me.txtPuchasePrice.TabIndex = 20
        Me.ToolTip1.SetToolTip(Me.txtPuchasePrice, "Purchase Price")
        '
        'txtSaleprice
        '
        Me.txtSaleprice.Location = New System.Drawing.Point(295, 251)
        Me.txtSaleprice.Name = "txtSaleprice"
        Me.txtSaleprice.Size = New System.Drawing.Size(70, 20)
        Me.txtSaleprice.TabIndex = 22
        Me.ToolTip1.SetToolTip(Me.txtSaleprice, "Sales Price")
        '
        'txtPackQty
        '
        Me.txtPackQty.Location = New System.Drawing.Point(109, 225)
        Me.txtPackQty.Name = "txtPackQty"
        Me.txtPackQty.Size = New System.Drawing.Size(70, 20)
        Me.txtPackQty.TabIndex = 16
        Me.txtPackQty.Text = "1"
        Me.ToolTip1.SetToolTip(Me.txtPackQty, "Pack Quantity")
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 254)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(79, 13)
        Me.Label8.TabIndex = 19
        Me.Label8.Text = "Purchase Price"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(188, 255)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(55, 13)
        Me.Label9.TabIndex = 21
        Me.Label9.Text = "Sale Price"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(12, 228)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(51, 13)
        Me.Label10.TabIndex = 15
        Me.Label10.Text = "Pack Qty"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnSave, Me.toolStripSeparator1, Me.btnRefresh, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(493, 25)
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
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(51, 22)
        Me.btnSave.Text = "&Save"
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
        'btnHelp
        '
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(52, 22)
        Me.btnHelp.Text = "He&lp"
        '
        'txtLargestPackQty
        '
        Me.txtLargestPackQty.Location = New System.Drawing.Point(295, 225)
        Me.txtLargestPackQty.Name = "txtLargestPackQty"
        Me.txtLargestPackQty.Size = New System.Drawing.Size(70, 20)
        Me.txtLargestPackQty.TabIndex = 18
        Me.txtLargestPackQty.Text = "1"
        Me.ToolTip1.SetToolTip(Me.txtLargestPackQty, "Pack Quantity")
        '
        'btnAddDept
        '
        Me.btnAddDept.Location = New System.Drawing.Point(285, 71)
        Me.btnAddDept.Name = "btnAddDept"
        Me.btnAddDept.Size = New System.Drawing.Size(30, 23)
        Me.btnAddDept.TabIndex = 4
        Me.btnAddDept.Text = "..."
        Me.btnAddDept.UseVisualStyleBackColor = True
        '
        'btnAddType
        '
        Me.btnAddType.Location = New System.Drawing.Point(285, 98)
        Me.btnAddType.Name = "btnAddType"
        Me.btnAddType.Size = New System.Drawing.Size(30, 23)
        Me.btnAddType.TabIndex = 7
        Me.btnAddType.Text = "..."
        Me.btnAddType.UseVisualStyleBackColor = True
        '
        'btnUnit
        '
        Me.btnUnit.Location = New System.Drawing.Point(285, 177)
        Me.btnUnit.Name = "btnUnit"
        Me.btnUnit.Size = New System.Drawing.Size(30, 23)
        Me.btnUnit.TabIndex = 14
        Me.btnUnit.Text = "..."
        Me.btnUnit.UseVisualStyleBackColor = True
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Location = New System.Drawing.Point(1, 284)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(491, 290)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 24
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Size and Color Information"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "Article Location Information"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(489, 269)
        '
        'PictureBox1
        '
        Me.PictureBox1.Location = New System.Drawing.Point(348, 72)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(133, 127)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 24
        Me.PictureBox1.TabStop = False
        '
        'lnkUploadPic
        '
        Me.lnkUploadPic.AutoSize = True
        Me.lnkUploadPic.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.lnkUploadPic.Location = New System.Drawing.Point(373, 208)
        Me.lnkUploadPic.Name = "lnkUploadPic"
        Me.lnkUploadPic.Size = New System.Drawing.Size(77, 13)
        Me.lnkUploadPic.TabIndex = 23
        Me.lnkUploadPic.TabStop = True
        Me.lnkUploadPic.Text = "Upload Picture"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(188, 228)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 13)
        Me.Label1.TabIndex = 17
        Me.Label1.Text = "Largest Pack Qty"
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(115, 265)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 25
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'pnlHeader
        '
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 25)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(493, 38)
        Me.pnlHeader.TabIndex = 26
        '
        'frmAddItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(493, 575)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtLargestPackQty)
        Me.Controls.Add(Me.lnkUploadPic)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.btnUnit)
        Me.Controls.Add(Me.btnAddType)
        Me.Controls.Add(Me.btnAddDept)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtPackQty)
        Me.Controls.Add(Me.txtSaleprice)
        Me.Controls.Add(Me.txtPuchasePrice)
        Me.Controls.Add(Me.lblunit)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtItem)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtItemCode)
        Me.Controls.Add(Me.cmbUnit)
        Me.Controls.Add(Me.cmbtype)
        Me.Controls.Add(Me.cmbDepartment)
        Me.Controls.Add(Me.lbltype)
        Me.Controls.Add(Me.Label2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAddItem"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add New Item"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdItemLocation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lbltype As System.Windows.Forms.Label
    Friend WithEvents cmbDepartment As System.Windows.Forms.ComboBox
    Friend WithEvents cmbtype As System.Windows.Forms.ComboBox
    Friend WithEvents cmbUnit As System.Windows.Forms.ComboBox
    Friend WithEvents txtItemCode As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtItem As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents lblunit As System.Windows.Forms.Label
    Friend WithEvents lstSize As SimpleAccounts.uiListControl
    Friend WithEvents lstCombination As SimpleAccounts.uiListControl
    Friend WithEvents txtPuchasePrice As System.Windows.Forms.TextBox
    Friend WithEvents txtSaleprice As System.Windows.Forms.TextBox
    Friend WithEvents txtPackQty As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnAddDept As System.Windows.Forms.Button
    Friend WithEvents btnAddType As System.Windows.Forms.Button
    Friend WithEvents btnUnit As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grdItemLocation As Janus.Windows.GridEX.GridEX
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents lnkUploadPic As System.Windows.Forms.LinkLabel
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtLargestPackQty As System.Windows.Forms.TextBox
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

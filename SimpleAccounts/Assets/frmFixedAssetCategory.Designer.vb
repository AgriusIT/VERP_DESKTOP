<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmFixedAssetCategory
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
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab3 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.lblAssetAccount = New System.Windows.Forms.Label()
        Me.lblTitle = New System.Windows.Forms.Label()
        Me.lblSortOrder = New System.Windows.Forms.Label()
        Me.lblRemarks = New System.Windows.Forms.Label()
        Me.cbActive = New System.Windows.Forms.CheckBox()
        Me.txtSortOrder = New System.Windows.Forms.TextBox()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.gbDepreciation = New System.Windows.Forms.GroupBox()
        Me.lblDepreciationMethod = New System.Windows.Forms.Label()
        Me.lblRate = New System.Windows.Forms.Label()
        Me.lblFrequency = New System.Windows.Forms.Label()
        Me.lblAccumulativeAccount = New System.Windows.Forms.Label()
        Me.txtRate = New System.Windows.Forms.TextBox()
        Me.lblExpenseAccount = New System.Windows.Forms.Label()
        Me.cmbFrequency = New System.Windows.Forms.ComboBox()
        Me.cmbExpenseAccount = New System.Windows.Forms.ComboBox()
        Me.cmbAccumulativeAccount = New System.Windows.Forms.ComboBox()
        Me.cmbDepreciationMethod = New System.Windows.Forms.ComboBox()
        Me.txtTitle = New System.Windows.Forms.TextBox()
        Me.cmbAssetAccount = New System.Windows.Forms.ComboBox()
        Me.txtCode = New System.Windows.Forms.TextBox()
        Me.lblCode = New System.Windows.Forms.Label()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdSaved = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.gbDepreciation.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.lblAssetAccount)
        Me.UltraTabPageControl1.Controls.Add(Me.lblTitle)
        Me.UltraTabPageControl1.Controls.Add(Me.lblSortOrder)
        Me.UltraTabPageControl1.Controls.Add(Me.lblRemarks)
        Me.UltraTabPageControl1.Controls.Add(Me.cbActive)
        Me.UltraTabPageControl1.Controls.Add(Me.txtSortOrder)
        Me.UltraTabPageControl1.Controls.Add(Me.txtRemarks)
        Me.UltraTabPageControl1.Controls.Add(Me.gbDepreciation)
        Me.UltraTabPageControl1.Controls.Add(Me.txtTitle)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbAssetAccount)
        Me.UltraTabPageControl1.Controls.Add(Me.txtCode)
        Me.UltraTabPageControl1.Controls.Add(Me.lblCode)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(739, 354)
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(5, 7)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(198, 25)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Fixed Asset Category"
        '
        'lblAssetAccount
        '
        Me.lblAssetAccount.AutoSize = True
        Me.lblAssetAccount.Location = New System.Drawing.Point(337, 47)
        Me.lblAssetAccount.Name = "lblAssetAccount"
        Me.lblAssetAccount.Size = New System.Drawing.Size(76, 13)
        Me.lblAssetAccount.TabIndex = 3
        Me.lblAssetAccount.Text = "Asset Account"
        '
        'lblTitle
        '
        Me.lblTitle.AutoSize = True
        Me.lblTitle.Location = New System.Drawing.Point(89, 73)
        Me.lblTitle.Name = "lblTitle"
        Me.lblTitle.Size = New System.Drawing.Size(27, 13)
        Me.lblTitle.TabIndex = 5
        Me.lblTitle.Text = "Title"
        '
        'lblSortOrder
        '
        Me.lblSortOrder.AutoSize = True
        Me.lblSortOrder.Location = New System.Drawing.Point(61, 231)
        Me.lblSortOrder.Name = "lblSortOrder"
        Me.lblSortOrder.Size = New System.Drawing.Size(55, 13)
        Me.lblSortOrder.TabIndex = 10
        Me.lblSortOrder.Text = "Sort Order"
        '
        'lblRemarks
        '
        Me.lblRemarks.AutoSize = True
        Me.lblRemarks.Location = New System.Drawing.Point(67, 179)
        Me.lblRemarks.Name = "lblRemarks"
        Me.lblRemarks.Size = New System.Drawing.Size(49, 13)
        Me.lblRemarks.TabIndex = 8
        Me.lblRemarks.Text = "Remarks"
        '
        'cbActive
        '
        Me.cbActive.AutoSize = True
        Me.cbActive.Location = New System.Drawing.Point(193, 229)
        Me.cbActive.Name = "cbActive"
        Me.cbActive.Size = New System.Drawing.Size(56, 17)
        Me.cbActive.TabIndex = 12
        Me.cbActive.Text = "Active"
        Me.ToolTip1.SetToolTip(Me.cbActive, "Select active or inactive")
        Me.cbActive.UseVisualStyleBackColor = True
        '
        'txtSortOrder
        '
        Me.txtSortOrder.Location = New System.Drawing.Point(120, 227)
        Me.txtSortOrder.Name = "txtSortOrder"
        Me.txtSortOrder.Size = New System.Drawing.Size(67, 20)
        Me.txtSortOrder.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.txtSortOrder, "Type sort order here")
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(120, 176)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(487, 46)
        Me.txtRemarks.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.txtRemarks, "Type remarks here")
        '
        'gbDepreciation
        '
        Me.gbDepreciation.Controls.Add(Me.lblDepreciationMethod)
        Me.gbDepreciation.Controls.Add(Me.lblRate)
        Me.gbDepreciation.Controls.Add(Me.lblFrequency)
        Me.gbDepreciation.Controls.Add(Me.lblAccumulativeAccount)
        Me.gbDepreciation.Controls.Add(Me.txtRate)
        Me.gbDepreciation.Controls.Add(Me.lblExpenseAccount)
        Me.gbDepreciation.Controls.Add(Me.cmbFrequency)
        Me.gbDepreciation.Controls.Add(Me.cmbExpenseAccount)
        Me.gbDepreciation.Controls.Add(Me.cmbAccumulativeAccount)
        Me.gbDepreciation.Controls.Add(Me.cmbDepreciationMethod)
        Me.gbDepreciation.Location = New System.Drawing.Point(3, 95)
        Me.gbDepreciation.Name = "gbDepreciation"
        Me.gbDepreciation.Size = New System.Drawing.Size(733, 75)
        Me.gbDepreciation.TabIndex = 7
        Me.gbDepreciation.TabStop = False
        Me.gbDepreciation.Text = "Depreciation"
        '
        'lblDepreciationMethod
        '
        Me.lblDepreciationMethod.AutoSize = True
        Me.lblDepreciationMethod.Location = New System.Drawing.Point(8, 53)
        Me.lblDepreciationMethod.Name = "lblDepreciationMethod"
        Me.lblDepreciationMethod.Size = New System.Drawing.Size(106, 13)
        Me.lblDepreciationMethod.TabIndex = 4
        Me.lblDepreciationMethod.Text = "Depreciation Method"
        '
        'lblRate
        '
        Me.lblRate.AutoSize = True
        Me.lblRate.Location = New System.Drawing.Point(511, 51)
        Me.lblRate.Name = "lblRate"
        Me.lblRate.Size = New System.Drawing.Size(41, 13)
        Me.lblRate.TabIndex = 8
        Me.lblRate.Text = "Rate %"
        '
        'lblFrequency
        '
        Me.lblFrequency.AutoSize = True
        Me.lblFrequency.Location = New System.Drawing.Point(353, 52)
        Me.lblFrequency.Name = "lblFrequency"
        Me.lblFrequency.Size = New System.Drawing.Size(57, 13)
        Me.lblFrequency.TabIndex = 6
        Me.lblFrequency.Text = "Frequency"
        '
        'lblAccumulativeAccount
        '
        Me.lblAccumulativeAccount.AutoSize = True
        Me.lblAccumulativeAccount.Location = New System.Drawing.Point(296, 25)
        Me.lblAccumulativeAccount.Name = "lblAccumulativeAccount"
        Me.lblAccumulativeAccount.Size = New System.Drawing.Size(114, 13)
        Me.lblAccumulativeAccount.TabIndex = 2
        Me.lblAccumulativeAccount.Text = "Accumulative Account"
        '
        'txtRate
        '
        Me.txtRate.Location = New System.Drawing.Point(556, 48)
        Me.txtRate.Name = "txtRate"
        Me.txtRate.Size = New System.Drawing.Size(48, 20)
        Me.txtRate.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.txtRate, "Type rate here")
        '
        'lblExpenseAccount
        '
        Me.lblExpenseAccount.AutoSize = True
        Me.lblExpenseAccount.Location = New System.Drawing.Point(22, 25)
        Me.lblExpenseAccount.Name = "lblExpenseAccount"
        Me.lblExpenseAccount.Size = New System.Drawing.Size(91, 13)
        Me.lblExpenseAccount.TabIndex = 0
        Me.lblExpenseAccount.Text = "Expense Account"
        '
        'cmbFrequency
        '
        Me.cmbFrequency.FormattingEnabled = True
        Me.cmbFrequency.Items.AddRange(New Object() {"Monthly", "Yearly"})
        Me.cmbFrequency.Location = New System.Drawing.Point(414, 48)
        Me.cmbFrequency.Name = "cmbFrequency"
        Me.cmbFrequency.Size = New System.Drawing.Size(94, 21)
        Me.cmbFrequency.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.cmbFrequency, "Please frequency here")
        '
        'cmbExpenseAccount
        '
        Me.cmbExpenseAccount.FormattingEnabled = True
        Me.cmbExpenseAccount.Location = New System.Drawing.Point(117, 22)
        Me.cmbExpenseAccount.Name = "cmbExpenseAccount"
        Me.cmbExpenseAccount.Size = New System.Drawing.Size(171, 21)
        Me.cmbExpenseAccount.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbExpenseAccount, "Please select expense account")
        '
        'cmbAccumulativeAccount
        '
        Me.cmbAccumulativeAccount.FormattingEnabled = True
        Me.cmbAccumulativeAccount.Location = New System.Drawing.Point(414, 22)
        Me.cmbAccumulativeAccount.Name = "cmbAccumulativeAccount"
        Me.cmbAccumulativeAccount.Size = New System.Drawing.Size(190, 21)
        Me.cmbAccumulativeAccount.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.cmbAccumulativeAccount, "Please select accumulative account here")
        '
        'cmbDepreciationMethod
        '
        Me.cmbDepreciationMethod.FormattingEnabled = True
        Me.cmbDepreciationMethod.Items.AddRange(New Object() {"Straight", "Zigzag"})
        Me.cmbDepreciationMethod.Location = New System.Drawing.Point(117, 49)
        Me.cmbDepreciationMethod.Name = "cmbDepreciationMethod"
        Me.cmbDepreciationMethod.Size = New System.Drawing.Size(171, 21)
        Me.cmbDepreciationMethod.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.cmbDepreciationMethod, "Please select depreciation method.")
        '
        'txtTitle
        '
        Me.txtTitle.Location = New System.Drawing.Point(120, 70)
        Me.txtTitle.Name = "txtTitle"
        Me.txtTitle.Size = New System.Drawing.Size(487, 20)
        Me.txtTitle.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.txtTitle, "Type title here")
        '
        'cmbAssetAccount
        '
        Me.cmbAssetAccount.FormattingEnabled = True
        Me.cmbAssetAccount.Location = New System.Drawing.Point(417, 44)
        Me.cmbAssetAccount.Name = "cmbAssetAccount"
        Me.cmbAssetAccount.Size = New System.Drawing.Size(190, 21)
        Me.cmbAssetAccount.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.cmbAssetAccount, "Please select asset account")
        '
        'txtCode
        '
        Me.txtCode.Location = New System.Drawing.Point(120, 44)
        Me.txtCode.Name = "txtCode"
        Me.txtCode.Size = New System.Drawing.Size(171, 20)
        Me.txtCode.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.txtCode, "Type code here")
        '
        'lblCode
        '
        Me.lblCode.AutoSize = True
        Me.lblCode.Location = New System.Drawing.Point(84, 47)
        Me.lblCode.Name = "lblCode"
        Me.lblCode.Size = New System.Drawing.Size(32, 13)
        Me.lblCode.TabIndex = 1
        Me.lblCode.Text = "Code"
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grdSaved)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(739, 354)
        '
        'grdSaved
        '
        Me.grdSaved.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdSaved.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSaved.Location = New System.Drawing.Point(0, 0)
        Me.grdSaved.Name = "grdSaved"
        Me.grdSaved.Size = New System.Drawing.Size(739, 354)
        Me.grdSaved.TabIndex = 0
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnSave, Me.btnDelete, Me.btnRefresh})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(741, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = Global.SimpleAccounts.My.Resources.Resources.BtnNew_Image
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(51, 22)
        Me.btnNew.Text = "&New"
        '
        'btnSave
        '
        Me.btnSave.Image = Global.SimpleAccounts.My.Resources.Resources.BtnSave_Image
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(51, 22)
        Me.btnSave.Text = "&Save"
        '
        'btnDelete
        '
        Me.btnDelete.Image = Global.SimpleAccounts.My.Resources.Resources.BtnDelete_Image
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
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
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 25)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(741, 375)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 1
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Fixed Assets Category"
        UltraTab3.TabPage = Me.UltraTabPageControl2
        UltraTab3.Text = "History"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab3})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(739, 354)
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(703, 0)
        Me.CtrlGrdBar1.MyGrid = Me.grdSaved
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(38, 25)
        Me.CtrlGrdBar1.TabIndex = 1
        '
        'pnlHeader
        '
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 25)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(741, 41)
        Me.pnlHeader.TabIndex = 10
        '
        'frmFixedAssetCategory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(741, 400)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "frmFixedAssetCategory"
        Me.Text = "Fixed Asset Category"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        Me.gbDepreciation.ResumeLayout(False)
        Me.gbDepreciation.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grdSaved As Janus.Windows.GridEX.GridEX
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents txtSortOrder As System.Windows.Forms.TextBox
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents txtRate As System.Windows.Forms.TextBox
    Friend WithEvents gbDepreciation As System.Windows.Forms.GroupBox
    Friend WithEvents cmbFrequency As System.Windows.Forms.ComboBox
    Friend WithEvents cmbExpenseAccount As System.Windows.Forms.ComboBox
    Friend WithEvents cmbAccumulativeAccount As System.Windows.Forms.ComboBox
    Friend WithEvents cmbDepreciationMethod As System.Windows.Forms.ComboBox
    Friend WithEvents txtTitle As System.Windows.Forms.TextBox
    Friend WithEvents cmbAssetAccount As System.Windows.Forms.ComboBox
    Friend WithEvents txtCode As System.Windows.Forms.TextBox
    Friend WithEvents lblCode As System.Windows.Forms.Label
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents lblAssetAccount As System.Windows.Forms.Label
    Friend WithEvents lblTitle As System.Windows.Forms.Label
    Friend WithEvents lblSortOrder As System.Windows.Forms.Label
    Friend WithEvents lblRemarks As System.Windows.Forms.Label
    Friend WithEvents cbActive As System.Windows.Forms.CheckBox
    Friend WithEvents lblDepreciationMethod As System.Windows.Forms.Label
    Friend WithEvents lblRate As System.Windows.Forms.Label
    Friend WithEvents lblFrequency As System.Windows.Forms.Label
    Friend WithEvents lblAccumulativeAccount As System.Windows.Forms.Label
    Friend WithEvents lblExpenseAccount As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

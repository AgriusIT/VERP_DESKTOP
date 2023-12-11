<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGrdRptSOPlanStatusDetail
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
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn7 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ID")
        Dim UltraGridColumn8 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Name")
        Dim UltraGridColumn9 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Territory")
        Dim UltraGridColumn10 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("City")
        Dim UltraGridColumn11 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("State")
        Dim UltraGridColumn12 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("AcId")
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lblCustomer = New System.Windows.Forms.Label()
        Me.cmbVendor = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblFromDate = New System.Windows.Forms.Label()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdReport = New Janus.Windows.GridEX.GridEX()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.CtrlGrdBar2 = New SimpleAccounts.CtrlGrdBar()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.cmbVendor, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdReport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox1)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(689, 375)
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.lblCustomer)
        Me.GroupBox1.Controls.Add(Me.cmbVendor)
        Me.GroupBox1.Controls.Add(Me.btnShow)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.lblFromDate)
        Me.GroupBox1.Controls.Add(Me.dtpToDate)
        Me.GroupBox1.Controls.Add(Me.dtpFromDate)
        Me.GroupBox1.Location = New System.Drawing.Point(11, 53)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(667, 140)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'lblCustomer
        '
        Me.lblCustomer.AutoSize = True
        Me.lblCustomer.Location = New System.Drawing.Point(16, 79)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(63, 13)
        Me.lblCustomer.TabIndex = 4
        Me.lblCustomer.Text = "Customer"
        '
        'cmbVendor
        '
        Me.cmbVendor.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cmbVendor.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbVendor.CheckedListSettings.CheckStateMember = ""
        Appearance7.BackColor = System.Drawing.Color.White
        Me.cmbVendor.DisplayLayout.Appearance = Appearance7
        UltraGridColumn7.Header.VisiblePosition = 0
        UltraGridColumn7.Hidden = True
        UltraGridColumn8.Header.VisiblePosition = 1
        UltraGridColumn8.Width = 141
        UltraGridColumn9.Header.VisiblePosition = 2
        UltraGridColumn10.Header.VisiblePosition = 3
        UltraGridColumn11.Header.VisiblePosition = 4
        UltraGridColumn12.Header.VisiblePosition = 5
        UltraGridColumn12.Hidden = True
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn7, UltraGridColumn8, UltraGridColumn9, UltraGridColumn10, UltraGridColumn11, UltraGridColumn12})
        Me.cmbVendor.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbVendor.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbVendor.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbVendor.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbVendor.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Appearance8.BackColor = System.Drawing.Color.Transparent
        Me.cmbVendor.DisplayLayout.Override.CardAreaAppearance = Appearance8
        Me.cmbVendor.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbVendor.DisplayLayout.Override.CellPadding = 3
        Me.cmbVendor.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance9.TextHAlignAsString = "Left"
        Me.cmbVendor.DisplayLayout.Override.HeaderAppearance = Appearance9
        Me.cmbVendor.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance10.BorderColor = System.Drawing.Color.LightGray
        Appearance10.TextVAlignAsString = "Middle"
        Me.cmbVendor.DisplayLayout.Override.RowAppearance = Appearance10
        Appearance11.BackColor = System.Drawing.Color.LightSteelBlue
        Appearance11.BorderColor = System.Drawing.Color.Black
        Appearance11.ForeColor = System.Drawing.Color.Black
        Me.cmbVendor.DisplayLayout.Override.SelectedRowAppearance = Appearance11
        Me.cmbVendor.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbVendor.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbVendor.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbVendor.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.cmbVendor.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbVendor.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbVendor.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbVendor.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbVendor.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear
        Me.cmbVendor.LimitToList = True
        Me.cmbVendor.Location = New System.Drawing.Point(89, 74)
        Me.cmbVendor.Name = "cmbVendor"
        Me.cmbVendor.Size = New System.Drawing.Size(437, 23)
        Me.cmbVendor.TabIndex = 5
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(451, 103)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(75, 23)
        Me.btnShow.TabIndex = 6
        Me.btnShow.Text = "&Show"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(16, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "To Date"
        '
        'lblFromDate
        '
        Me.lblFromDate.AutoSize = True
        Me.lblFromDate.Location = New System.Drawing.Point(16, 24)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(67, 13)
        Me.lblFromDate.TabIndex = 0
        Me.lblFromDate.Text = "From Date"
        '
        'dtpToDate
        '
        Me.dtpToDate.Checked = False
        Me.dtpToDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(89, 47)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.ShowCheckBox = True
        Me.dtpToDate.Size = New System.Drawing.Size(149, 21)
        Me.dtpToDate.TabIndex = 3
        '
        'dtpFromDate
        '
        Me.dtpFromDate.Checked = False
        Me.dtpFromDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(89, 20)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.ShowCheckBox = True
        Me.dtpFromDate.Size = New System.Drawing.Size(149, 21)
        Me.dtpFromDate.TabIndex = 1
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grdReport)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(689, 375)
        '
        'grdReport
        '
        Me.grdReport.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdReport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdReport.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdReport.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdReport.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdReport.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.grdReport.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdReport.Location = New System.Drawing.Point(0, 0)
        Me.grdReport.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.grdReport.Name = "grdReport"
        Me.grdReport.RecordNavigator = True
        Me.grdReport.Size = New System.Drawing.Size(689, 375)
        Me.grdReport.TabIndex = 0
        Me.grdReport.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdReport.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdReport.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(12, 9)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(207, 23)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Plan Status Detail"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(691, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
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
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 25)
        Me.UltraTabControl1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(691, 396)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 2
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Criteria"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "Report"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(689, 375)
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(718, 0)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.CtrlGrdBar1.MyGrid = Me.grdReport
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(44, 25)
        Me.CtrlGrdBar1.TabIndex = 3
        '
        'CtrlGrdBar2
        '
        Me.CtrlGrdBar2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar2.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar2.Email = Nothing
        Me.CtrlGrdBar2.FormName = Me
        Me.CtrlGrdBar2.Location = New System.Drawing.Point(652, 0)
        Me.CtrlGrdBar2.MyGrid = Me.grdReport
        Me.CtrlGrdBar2.Name = "CtrlGrdBar2"
        Me.CtrlGrdBar2.Size = New System.Drawing.Size(38, 25)
        Me.CtrlGrdBar2.TabIndex = 1
        '
        'pnlHeader
        '
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(689, 42)
        Me.pnlHeader.TabIndex = 78
        '
        'frmGrdRptSOPlanStatusDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(691, 421)
        Me.Controls.Add(Me.CtrlGrdBar2)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.Name = "frmGrdRptSOPlanStatusDetail"
        Me.Text = "Plan Status Detail"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.cmbVendor, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdReport, System.ComponentModel.ISupportInitialize).EndInit()
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
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents grdReport As Janus.Windows.GridEX.GridEX
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblFromDate As System.Windows.Forms.Label
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblCustomer As System.Windows.Forms.Label
    Friend WithEvents cmbVendor As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents CtrlGrdBar2 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

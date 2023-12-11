<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGrdRptLocationWiseStockStatementNew
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
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.lstLocation = New SimpleAccounts.uiListControl()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label38 = New System.Windows.Forms.Label()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.cmbProject = New System.Windows.Forms.ComboBox()
        Me.RbtLocationWiseSummary = New System.Windows.Forms.RadioButton()
        Me.rbtnSummaryView = New System.Windows.Forms.RadioButton()
        Me.lblReportView = New System.Windows.Forms.Label()
        Me.rbtnDetailView = New System.Windows.Forms.RadioButton()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.GrdLocationWiseSummary = New Janus.Windows.GridEX.GridEX()
        Me.grdSummary = New Janus.Windows.GridEX.GridEX()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.CtrlGrdBar2 = New SimpleAccounts.CtrlGrdBar()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.UltraTabPageControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.GrdLocationWiseSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.lstLocation)
        Me.UltraTabPageControl1.Controls.Add(Me.btnShow)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox1)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(732, 558)
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(732, 42)
        Me.pnlHeader.TabIndex = 83
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(17, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(441, 36)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Location Wise Stock Statement"
        '
        'lstLocation
        '
        Me.lstLocation.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstLocation.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstLocation.BackColor = System.Drawing.Color.Transparent
        Me.lstLocation.disableWhenChecked = False
        Me.lstLocation.HeadingLabelName = "lblLocation"
        Me.lstLocation.HeadingText = "Location"
        Me.lstLocation.Location = New System.Drawing.Point(11, 230)
        Me.lstLocation.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.lstLocation.Name = "lstLocation"
        Me.lstLocation.ShowAddNewButton = False
        Me.lstLocation.ShowInverse = True
        Me.lstLocation.ShowMagnifierButton = False
        Me.lstLocation.ShowNoCheck = False
        Me.lstLocation.ShowResetAllButton = False
        Me.lstLocation.ShowSelectall = True
        Me.lstLocation.Size = New System.Drawing.Size(457, 224)
        Me.lstLocation.TabIndex = 2
        Me.lstLocation.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(393, 460)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(75, 23)
        Me.btnShow.TabIndex = 3
        Me.btnShow.Text = "Show"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.Label38)
        Me.GroupBox1.Controls.Add(Me.cmbStatus)
        Me.GroupBox1.Controls.Add(Me.Label21)
        Me.GroupBox1.Controls.Add(Me.cmbProject)
        Me.GroupBox1.Controls.Add(Me.RbtLocationWiseSummary)
        Me.GroupBox1.Controls.Add(Me.rbtnSummaryView)
        Me.GroupBox1.Controls.Add(Me.lblReportView)
        Me.GroupBox1.Controls.Add(Me.rbtnDetailView)
        Me.GroupBox1.Controls.Add(Me.dtpFrom)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.dtpTo)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 55)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(458, 169)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Date Range"
        '
        'Label38
        '
        Me.Label38.AutoSize = True
        Me.Label38.Location = New System.Drawing.Point(6, 111)
        Me.Label38.Name = "Label38"
        Me.Label38.Size = New System.Drawing.Size(65, 20)
        Me.Label38.TabIndex = 6
        Me.Label38.Text = "Status"
        '
        'cmbStatus
        '
        Me.cmbStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Location = New System.Drawing.Point(114, 105)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(159, 28)
        Me.cmbStatus.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.cmbStatus, "Select Status")
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.Location = New System.Drawing.Point(6, 81)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(68, 20)
        Me.Label21.TabIndex = 4
        Me.Label21.Text = "Project"
        '
        'cmbProject
        '
        Me.cmbProject.FormattingEnabled = True
        Me.cmbProject.Location = New System.Drawing.Point(114, 78)
        Me.cmbProject.Name = "cmbProject"
        Me.cmbProject.Size = New System.Drawing.Size(159, 28)
        Me.cmbProject.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.cmbProject, "Select Project")
        '
        'RbtLocationWiseSummary
        '
        Me.RbtLocationWiseSummary.AutoSize = True
        Me.RbtLocationWiseSummary.Location = New System.Drawing.Point(279, 137)
        Me.RbtLocationWiseSummary.Name = "RbtLocationWiseSummary"
        Me.RbtLocationWiseSummary.Size = New System.Drawing.Size(242, 24)
        Me.RbtLocationWiseSummary.TabIndex = 11
        Me.RbtLocationWiseSummary.Text = "Location Wise Summary"
        Me.ToolTip1.SetToolTip(Me.RbtLocationWiseSummary, "Location Wise Summary Report")
        Me.RbtLocationWiseSummary.UseVisualStyleBackColor = True
        '
        'rbtnSummaryView
        '
        Me.rbtnSummaryView.AutoSize = True
        Me.rbtnSummaryView.Checked = True
        Me.rbtnSummaryView.Location = New System.Drawing.Point(114, 137)
        Me.rbtnSummaryView.Name = "rbtnSummaryView"
        Me.rbtnSummaryView.Size = New System.Drawing.Size(118, 24)
        Me.rbtnSummaryView.TabIndex = 9
        Me.rbtnSummaryView.TabStop = True
        Me.rbtnSummaryView.Text = "Summary"
        Me.ToolTip1.SetToolTip(Me.rbtnSummaryView, "Summary Report")
        Me.rbtnSummaryView.UseVisualStyleBackColor = True
        '
        'lblReportView
        '
        Me.lblReportView.AutoSize = True
        Me.lblReportView.Location = New System.Drawing.Point(6, 139)
        Me.lblReportView.Name = "lblReportView"
        Me.lblReportView.Size = New System.Drawing.Size(113, 20)
        Me.lblReportView.TabIndex = 8
        Me.lblReportView.Text = "Report View"
        '
        'rbtnDetailView
        '
        Me.rbtnDetailView.AutoSize = True
        Me.rbtnDetailView.Location = New System.Drawing.Point(201, 137)
        Me.rbtnDetailView.Name = "rbtnDetailView"
        Me.rbtnDetailView.Size = New System.Drawing.Size(105, 24)
        Me.rbtnDetailView.TabIndex = 10
        Me.rbtnDetailView.Text = "Detailed"
        Me.ToolTip1.SetToolTip(Me.rbtnDetailView, "Detailed Report")
        Me.rbtnDetailView.UseVisualStyleBackColor = True
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(114, 26)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(159, 28)
        Me.dtpFrom.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "From "
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd/MMM/yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(114, 52)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(159, 28)
        Me.dtpTo.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "To"
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.GrdLocationWiseSummary)
        Me.UltraTabPageControl2.Controls.Add(Me.grdSummary)
        Me.UltraTabPageControl2.Controls.Add(Me.grd)
        Me.UltraTabPageControl2.Controls.Add(Me.CtrlGrdBar2)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(732, 558)
        '
        'GrdLocationWiseSummary
        '
        Me.GrdLocationWiseSummary.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GrdLocationWiseSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrdLocationWiseSummary.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GrdLocationWiseSummary.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GrdLocationWiseSummary.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GrdLocationWiseSummary.Location = New System.Drawing.Point(0, 0)
        Me.GrdLocationWiseSummary.Name = "GrdLocationWiseSummary"
        Me.GrdLocationWiseSummary.RecordNavigator = True
        Me.GrdLocationWiseSummary.Size = New System.Drawing.Size(732, 558)
        Me.GrdLocationWiseSummary.TabIndex = 1
        Me.GrdLocationWiseSummary.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GrdLocationWiseSummary.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GrdLocationWiseSummary.Visible = False
        Me.GrdLocationWiseSummary.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'grdSummary
        '
        Me.grdSummary.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSummary.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdSummary.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdSummary.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdSummary.Location = New System.Drawing.Point(0, 0)
        Me.grdSummary.Name = "grdSummary"
        Me.grdSummary.RecordNavigator = True
        Me.grdSummary.Size = New System.Drawing.Size(732, 558)
        Me.grdSummary.TabIndex = 2
        Me.grdSummary.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdSummary.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdSummary.Visible = False
        Me.grdSummary.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'grd
        '
        Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.Location = New System.Drawing.Point(0, 0)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(732, 558)
        Me.grd.TabIndex = 0
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'CtrlGrdBar2
        '
        Me.CtrlGrdBar2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar2.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar2.Email = Nothing
        Me.CtrlGrdBar2.FormName = Nothing
        Me.CtrlGrdBar2.Location = New System.Drawing.Point(695, 0)
        Me.CtrlGrdBar2.MyGrid = Me.grd
        Me.CtrlGrdBar2.Name = "CtrlGrdBar2"
        Me.CtrlGrdBar2.Size = New System.Drawing.Size(38, 25)
        Me.CtrlGrdBar2.TabIndex = 1
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(734, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(696, 0)
        Me.CtrlGrdBar1.MyGrid = Me.grd
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(38, 25)
        Me.CtrlGrdBar1.TabIndex = 2
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
        Me.UltraTabControl1.Size = New System.Drawing.Size(734, 586)
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
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(732, 558)
        '
        'frmGrdRptLocationWiseStockStatementNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(11.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(734, 611)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmGrdRptLocationWiseStockStatementNew"
        Me.Text = "Location Wise Stock Statement"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.GrdLocationWiseSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdSummary, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents CtrlGrdBar2 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents lblReportView As System.Windows.Forms.Label
    Friend WithEvents rbtnDetailView As System.Windows.Forms.RadioButton
    Friend WithEvents rbtnSummaryView As System.Windows.Forms.RadioButton
    Friend WithEvents grdSummary As Janus.Windows.GridEX.GridEX
    Friend WithEvents RbtLocationWiseSummary As System.Windows.Forms.RadioButton
    Friend WithEvents GrdLocationWiseSummary As Janus.Windows.GridEX.GridEX
    Friend WithEvents lstLocation As SimpleAccounts.uiListControl
    Friend WithEvents Label21 As System.Windows.Forms.Label
    Friend WithEvents cmbProject As System.Windows.Forms.ComboBox
    Friend WithEvents Label38 As System.Windows.Forms.Label
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

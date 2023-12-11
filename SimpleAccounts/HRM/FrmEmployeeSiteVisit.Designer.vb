<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmEmployeeSiteVisit
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
        Dim grdEmployee_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmEmployeeSiteVisit))
        Dim grdAllRecords_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim UltraTab4 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab3 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpMY = New System.Windows.Forms.DateTimePicker()
        Me.grdEmployee = New Janus.Windows.GridEX.GridEX()
        Me.UltraTabPageControl3 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdAllRecords = New Janus.Windows.GridEX.GridEX()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.btnLoadHistory = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.CtrlGrdBar2 = New SimpleAccounts.CtrlGrdBar()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl3.SuspendLayout()
        CType(Me.grdAllRecords, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox2.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.CtrlGrdBar2)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox1)
        Me.UltraTabPageControl1.Controls.Add(Me.ToolStrip1)
        Me.UltraTabPageControl1.Controls.Add(Me.grdEmployee)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(727, 384)
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnLoad)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.dtpMY)
        Me.GroupBox1.Location = New System.Drawing.Point(2, 39)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(391, 47)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Criteria"
        '
        'btnLoad
        '
        Me.btnLoad.Location = New System.Drawing.Point(255, 18)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(75, 23)
        Me.btnLoad.TabIndex = 5
        Me.btnLoad.Text = "&Load"
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 23)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Month"
        '
        'dtpMY
        '
        Me.dtpMY.CustomFormat = ""
        Me.dtpMY.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpMY.Location = New System.Drawing.Point(49, 19)
        Me.dtpMY.Name = "dtpMY"
        Me.dtpMY.Size = New System.Drawing.Size(200, 20)
        Me.dtpMY.TabIndex = 0
        '
        'grdEmployee
        '
        Me.grdEmployee.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdEmployee_DesignTimeLayout.LayoutString = resources.GetString("grdEmployee_DesignTimeLayout.LayoutString")
        Me.grdEmployee.DesignTimeLayout = grdEmployee_DesignTimeLayout
        Me.grdEmployee.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdEmployee.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdEmployee.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdEmployee.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdEmployee.GroupByBoxVisible = False
        Me.grdEmployee.Location = New System.Drawing.Point(4, 105)
        Me.grdEmployee.Name = "grdEmployee"
        Me.grdEmployee.RecordNavigator = True
        Me.grdEmployee.Size = New System.Drawing.Size(719, 251)
        Me.grdEmployee.TabIndex = 2
        Me.grdEmployee.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdEmployee.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdEmployee.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'UltraTabPageControl3
        '
        Me.UltraTabPageControl3.Controls.Add(Me.grdAllRecords)
        Me.UltraTabPageControl3.Controls.Add(Me.CtrlGrdBar1)
        Me.UltraTabPageControl3.Controls.Add(Me.GroupBox2)
        Me.UltraTabPageControl3.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl3.Name = "UltraTabPageControl3"
        Me.UltraTabPageControl3.Size = New System.Drawing.Size(727, 359)
        '
        'grdAllRecords
        '
        Me.grdAllRecords.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdAllRecords.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdAllRecords.ColumnAutoResize = True
        grdAllRecords_DesignTimeLayout.LayoutString = resources.GetString("grdAllRecords_DesignTimeLayout.LayoutString")
        Me.grdAllRecords.DesignTimeLayout = grdAllRecords_DesignTimeLayout
        Me.grdAllRecords.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdAllRecords.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdAllRecords.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdAllRecords.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdAllRecords.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.grdAllRecords.Location = New System.Drawing.Point(2, 65)
        Me.grdAllRecords.Name = "grdAllRecords"
        Me.grdAllRecords.RecordNavigator = True
        Me.grdAllRecords.Size = New System.Drawing.Size(723, 291)
        Me.grdAllRecords.TabIndex = 5
        Me.grdAllRecords.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdAllRecords.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdAllRecords.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(691, 3)
        Me.CtrlGrdBar1.MyGrid = Me.grdAllRecords
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(33, 25)
        Me.CtrlGrdBar1.TabIndex = 1
        Me.CtrlGrdBar1.TabStop = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Controls.Add(Me.dtpToDate)
        Me.GroupBox2.Controls.Add(Me.btnLoadHistory)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.dtpFromDate)
        Me.GroupBox2.Location = New System.Drawing.Point(11, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(620, 56)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Criteria"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(274, 23)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "To Date"
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = ""
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(326, 19)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(200, 20)
        Me.dtpToDate.TabIndex = 6
        '
        'btnLoadHistory
        '
        Me.btnLoadHistory.Location = New System.Drawing.Point(532, 18)
        Me.btnLoadHistory.Name = "btnLoadHistory"
        Me.btnLoadHistory.Size = New System.Drawing.Size(75, 23)
        Me.btnLoadHistory.TabIndex = 5
        Me.btnLoadHistory.Text = "&Load"
        Me.btnLoadHistory.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "From Date"
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = ""
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(68, 19)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(200, 20)
        Me.dtpFromDate.TabIndex = 0
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnSave, Me.btnRefresh})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(685, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnSave
        '
        Me.btnSave.Image = Global.SimpleAccounts.My.Resources.Resources.BtnSave_Image
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(51, 22)
        Me.btnSave.Text = "&Save"
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
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl3)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(729, 405)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 4
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab4.TabPage = Me.UltraTabPageControl1
        UltraTab4.Text = "Site Visit"
        UltraTab3.TabPage = Me.UltraTabPageControl3
        UltraTab3.Text = "All Records"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab4, UltraTab3})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(727, 384)
        '
        'CtrlGrdBar2
        '
        Me.CtrlGrdBar2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar2.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBar2.Email = Nothing
        Me.CtrlGrdBar2.FormName = Me
        Me.CtrlGrdBar2.Location = New System.Drawing.Point(686, -1)
        Me.CtrlGrdBar2.MyGrid = Me.grdEmployee
        Me.CtrlGrdBar2.Name = "CtrlGrdBar2"
        Me.CtrlGrdBar2.Size = New System.Drawing.Size(42, 26)
        Me.CtrlGrdBar2.TabIndex = 4
        '
        'FrmEmployeeSiteVisit
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(729, 405)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmEmployeeSiteVisit"
        Me.Text = "Employee Site Visit"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl3.ResumeLayout(False)
        CType(Me.grdAllRecords, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents grdEmployee As Janus.Windows.GridEX.GridEX
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpMY As System.Windows.Forms.DateTimePicker
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl3 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grdAllRecords As Janus.Windows.GridEX.GridEX
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnLoadHistory As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents CtrlGrdBar2 As SimpleAccounts.CtrlGrdBar
End Class

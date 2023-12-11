<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGrdRptSalesSummaries
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
        Dim UltraTab3 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab4 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rbtArea = New System.Windows.Forms.RadioButton()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.rbtManager = New System.Windows.Forms.RadioButton()
        Me.rbtSaleman = New System.Windows.Forms.RadioButton()
        Me.rbtCustomer = New System.Windows.Forms.RadioButton()
        Me.rbtItem = New System.Windows.Forms.RadioButton()
        Me.rbtItemType = New System.Windows.Forms.RadioButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpDateTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpDateForm = New System.Windows.Forms.DateTimePicker()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.GroupBox1)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(1272, 755)
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1272, 65)
        Me.pnlHeader.TabIndex = 83
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(21, 15)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(252, 41)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Sales Summary"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.rbtArea)
        Me.GroupBox1.Controls.Add(Me.btnShow)
        Me.GroupBox1.Controls.Add(Me.rbtManager)
        Me.GroupBox1.Controls.Add(Me.rbtSaleman)
        Me.GroupBox1.Controls.Add(Me.rbtCustomer)
        Me.GroupBox1.Controls.Add(Me.rbtItem)
        Me.GroupBox1.Controls.Add(Me.rbtItemType)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.dtpDateTo)
        Me.GroupBox1.Controls.Add(Me.dtpDateForm)
        Me.GroupBox1.Location = New System.Drawing.Point(20, 83)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(1236, 375)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'rbtArea
        '
        Me.rbtArea.AutoSize = True
        Me.rbtArea.Location = New System.Drawing.Point(141, 215)
        Me.rbtArea.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtArea.Name = "rbtArea"
        Me.rbtArea.Size = New System.Drawing.Size(90, 24)
        Me.rbtArea.TabIndex = 7
        Me.rbtArea.TabStop = True
        Me.rbtArea.Text = "Area By"
        Me.rbtArea.UseVisualStyleBackColor = True
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(141, 322)
        Me.btnShow.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(112, 35)
        Me.btnShow.TabIndex = 10
        Me.btnShow.Text = "Show"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'rbtManager
        '
        Me.rbtManager.AutoSize = True
        Me.rbtManager.Location = New System.Drawing.Point(141, 286)
        Me.rbtManager.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtManager.Name = "rbtManager"
        Me.rbtManager.Size = New System.Drawing.Size(119, 24)
        Me.rbtManager.TabIndex = 9
        Me.rbtManager.TabStop = True
        Me.rbtManager.Text = "Manager By"
        Me.rbtManager.UseVisualStyleBackColor = True
        '
        'rbtSaleman
        '
        Me.rbtSaleman.AutoSize = True
        Me.rbtSaleman.Location = New System.Drawing.Point(141, 251)
        Me.rbtSaleman.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtSaleman.Name = "rbtSaleman"
        Me.rbtSaleman.Size = New System.Drawing.Size(127, 24)
        Me.rbtSaleman.TabIndex = 8
        Me.rbtSaleman.TabStop = True
        Me.rbtSaleman.Text = "Salesman By"
        Me.rbtSaleman.UseVisualStyleBackColor = True
        '
        'rbtCustomer
        '
        Me.rbtCustomer.AutoSize = True
        Me.rbtCustomer.Location = New System.Drawing.Point(141, 180)
        Me.rbtCustomer.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtCustomer.Name = "rbtCustomer"
        Me.rbtCustomer.Size = New System.Drawing.Size(125, 24)
        Me.rbtCustomer.TabIndex = 6
        Me.rbtCustomer.TabStop = True
        Me.rbtCustomer.Text = "Customer By"
        Me.rbtCustomer.UseVisualStyleBackColor = True
        '
        'rbtItem
        '
        Me.rbtItem.AutoSize = True
        Me.rbtItem.Location = New System.Drawing.Point(141, 145)
        Me.rbtItem.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtItem.Name = "rbtItem"
        Me.rbtItem.Size = New System.Drawing.Size(88, 24)
        Me.rbtItem.TabIndex = 5
        Me.rbtItem.TabStop = True
        Me.rbtItem.Text = "Item By"
        Me.rbtItem.UseVisualStyleBackColor = True
        '
        'rbtItemType
        '
        Me.rbtItemType.AutoSize = True
        Me.rbtItemType.Location = New System.Drawing.Point(141, 109)
        Me.rbtItemType.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtItemType.Name = "rbtItemType"
        Me.rbtItemType.Size = New System.Drawing.Size(126, 24)
        Me.rbtItemType.TabIndex = 4
        Me.rbtItemType.TabStop = True
        Me.rbtItemType.Text = "Item Type By"
        Me.rbtItemType.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 75)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 20)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "End Date"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 35)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(85, 20)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "From Date"
        '
        'dtpDateTo
        '
        Me.dtpDateTo.CustomFormat = "dd/MMM/yyyy"
        Me.dtpDateTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateTo.Location = New System.Drawing.Point(141, 69)
        Me.dtpDateTo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpDateTo.Name = "dtpDateTo"
        Me.dtpDateTo.Size = New System.Drawing.Size(229, 26)
        Me.dtpDateTo.TabIndex = 3
        '
        'dtpDateForm
        '
        Me.dtpDateForm.CustomFormat = "dd/MMM/yyyy"
        Me.dtpDateForm.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDateForm.Location = New System.Drawing.Point(141, 29)
        Me.dtpDateForm.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpDateForm.Name = "dtpDateForm"
        Me.dtpDateForm.Size = New System.Drawing.Size(229, 26)
        Me.dtpDateForm.TabIndex = 1
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grd)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(1272, 755)
        '
        'grd
        '
        Me.grd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.Location = New System.Drawing.Point(0, 0)
        Me.grd.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(1272, 755)
        Me.grd.TabIndex = 0
        Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1274, 32)
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
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 32)
        Me.UltraTabControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(1274, 782)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 2
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab3.TabPage = Me.UltraTabPageControl1
        UltraTab3.Text = "Criteria"
        UltraTab4.TabPage = Me.UltraTabPageControl2
        UltraTab4.Text = "Sales Summary Report"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab3, UltraTab4})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.VisualStudio2005
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(1272, 755)
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(1216, 0)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar1.MyGrid = Me.grd
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(57, 38)
        Me.CtrlGrdBar1.TabIndex = 1
        '
        'frmGrdRptSalesSummaries
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1274, 814)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmGrdRptSalesSummaries"
        Me.Text = "Sales Summary"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpDateTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpDateForm As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents rbtManager As System.Windows.Forms.RadioButton
    Friend WithEvents rbtSaleman As System.Windows.Forms.RadioButton
    Friend WithEvents rbtCustomer As System.Windows.Forms.RadioButton
    Friend WithEvents rbtItem As System.Windows.Forms.RadioButton
    Friend WithEvents rbtItemType As System.Windows.Forms.RadioButton
    Friend WithEvents rbtArea As System.Windows.Forms.RadioButton
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

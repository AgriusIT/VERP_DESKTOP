<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHistorysalespurchase
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
        Dim grdItemSales_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdItemPurchse_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.UltraTabPageControl7 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdItemSales = New Janus.Windows.GridEX.GridEX()
        Me.UltraTabPageControl8 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdItemPurchse = New Janus.Windows.GridEX.GridEX()
        Me.UltraTabControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage3 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnLoadAll = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.Label57 = New System.Windows.Forms.Label()
        Me.cmbCurrency = New System.Windows.Forms.ComboBox()
        Me.txtCurrencyRate = New System.Windows.Forms.TextBox()
        Me.lblCurrencyRate = New System.Windows.Forms.Label()
        Me.btnApply = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.UltraTabPageControl7.SuspendLayout()
        CType(Me.grdItemSales, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl8.SuspendLayout()
        CType(Me.grdItemPurchse, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl2.SuspendLayout()
        Me.UltraTabPageControl1.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl7
        '
        Me.UltraTabPageControl7.Controls.Add(Me.grdItemSales)
        Me.UltraTabPageControl7.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl7.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl7.Name = "UltraTabPageControl7"
        Me.UltraTabPageControl7.Size = New System.Drawing.Size(1078, 521)
        '
        'grdItemSales
        '
        Me.grdItemSales.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdItemSales.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdItemSales_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grdItemSales.DesignTimeLayout = grdItemSales_DesignTimeLayout
        Me.grdItemSales.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.grdItemSales.GroupByBoxVisible = False
        Me.grdItemSales.Location = New System.Drawing.Point(-2, -2)
        Me.grdItemSales.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdItemSales.Name = "grdItemSales"
        Me.grdItemSales.RecordNavigator = True
        Me.grdItemSales.Size = New System.Drawing.Size(1083, 531)
        Me.grdItemSales.TabIndex = 3
        Me.grdItemSales.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        Me.grdItemSales.TabStop = False
        Me.grdItemSales.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdItemSales.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdItemSales.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'UltraTabPageControl8
        '
        Me.UltraTabPageControl8.Controls.Add(Me.grdItemPurchse)
        Me.UltraTabPageControl8.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl8.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabPageControl8.Name = "UltraTabPageControl8"
        Me.UltraTabPageControl8.Size = New System.Drawing.Size(1078, 521)
        '
        'grdItemPurchse
        '
        Me.grdItemPurchse.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdItemPurchse.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdItemPurchse_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grdItemPurchse.DesignTimeLayout = grdItemPurchse_DesignTimeLayout
        Me.grdItemPurchse.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.grdItemPurchse.GroupByBoxVisible = False
        Me.grdItemPurchse.Location = New System.Drawing.Point(-2, -2)
        Me.grdItemPurchse.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdItemPurchse.Name = "grdItemPurchse"
        Me.grdItemPurchse.RecordNavigator = True
        Me.grdItemPurchse.Size = New System.Drawing.Size(1081, 531)
        Me.grdItemPurchse.TabIndex = 2
        Me.grdItemPurchse.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        Me.grdItemPurchse.TabStop = False
        Me.grdItemPurchse.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdItemPurchse.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdItemPurchse.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'UltraTabControl2
        '
        Me.UltraTabControl2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UltraTabControl2.Controls.Add(Me.UltraTabSharedControlsPage3)
        Me.UltraTabControl2.Controls.Add(Me.UltraTabPageControl7)
        Me.UltraTabControl2.Controls.Add(Me.UltraTabPageControl8)
        Me.UltraTabControl2.Location = New System.Drawing.Point(0, 94)
        Me.UltraTabControl2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabControl2.Name = "UltraTabControl2"
        Me.UltraTabControl2.SharedControlsPage = Me.UltraTabSharedControlsPage3
        Me.UltraTabControl2.Size = New System.Drawing.Size(1080, 548)
        Me.UltraTabControl2.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl2.TabIndex = 15
        Me.UltraTabControl2.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl7
        UltraTab1.Text = "Sales"
        UltraTab2.TabPage = Me.UltraTabPageControl8
        UltraTab2.Text = "Purchase"
        Me.UltraTabControl2.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl2.TabStop = False
        Me.UltraTabControl2.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.VisualStudio2005
        '
        'UltraTabSharedControlsPage3
        '
        Me.UltraTabSharedControlsPage3.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UltraTabSharedControlsPage3.Name = "UltraTabSharedControlsPage3"
        Me.UltraTabSharedControlsPage3.Size = New System.Drawing.Size(1078, 521)
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(1, 20)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(196, 77)
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.GridEX1)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(718, 365)
        '
        'GridEX1
        '
        Me.GridEX1.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        GridEX1_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.Location = New System.Drawing.Point(-1, -1)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.Size = New System.Drawing.Size(721, 366)
        Me.GridEX1.TabIndex = 3
        Me.GridEX1.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        Me.GridEX1.TabStop = False
        Me.GridEX1.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.GridEX1.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 0)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(200, 100)
        Me.UltraTabControl1.TabIndex = 0
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnLoadAll, Me.toolStripSeparator1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1080, 32)
        Me.ToolStrip1.TabIndex = 16
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnLoadAll
        '
        Me.btnLoadAll.Image = Global.SimpleAccounts.My.Resources.Resources.sendcontactdetails
        Me.btnLoadAll.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnLoadAll.Name = "btnLoadAll"
        Me.btnLoadAll.Size = New System.Drawing.Size(104, 29)
        Me.btnLoadAll.Text = "&Load All"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 32)
        '
        'Label57
        '
        Me.Label57.AutoSize = True
        Me.Label57.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label57.Location = New System.Drawing.Point(45, 51)
        Me.Label57.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.Label57.Name = "Label57"
        Me.Label57.Size = New System.Drawing.Size(86, 20)
        Me.Label57.TabIndex = 17
        Me.Label57.Text = "Currency"
        '
        'cmbCurrency
        '
        Me.cmbCurrency.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbCurrency.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCurrency.FormattingEnabled = True
        Me.cmbCurrency.Location = New System.Drawing.Point(141, 46)
        Me.cmbCurrency.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbCurrency.Name = "cmbCurrency"
        Me.cmbCurrency.Size = New System.Drawing.Size(259, 28)
        Me.cmbCurrency.TabIndex = 18
        Me.ToolTip1.SetToolTip(Me.cmbCurrency, "Select a Currency")
        '
        'txtCurrencyRate
        '
        Me.txtCurrencyRate.Location = New System.Drawing.Point(474, 46)
        Me.txtCurrencyRate.Margin = New System.Windows.Forms.Padding(6, 5, 6, 5)
        Me.txtCurrencyRate.Name = "txtCurrencyRate"
        Me.txtCurrencyRate.Size = New System.Drawing.Size(259, 26)
        Me.txtCurrencyRate.TabIndex = 20
        Me.ToolTip1.SetToolTip(Me.txtCurrencyRate, "Rate")
        '
        'lblCurrencyRate
        '
        Me.lblCurrencyRate.AutoSize = True
        Me.lblCurrencyRate.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrencyRate.Location = New System.Drawing.Point(412, 51)
        Me.lblCurrencyRate.Margin = New System.Windows.Forms.Padding(6, 0, 6, 0)
        Me.lblCurrencyRate.Name = "lblCurrencyRate"
        Me.lblCurrencyRate.Size = New System.Drawing.Size(48, 20)
        Me.lblCurrencyRate.TabIndex = 19
        Me.lblCurrencyRate.Text = "Rate"
        '
        'btnApply
        '
        Me.btnApply.Location = New System.Drawing.Point(746, 43)
        Me.btnApply.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnApply.Name = "btnApply"
        Me.btnApply.Size = New System.Drawing.Size(86, 35)
        Me.btnApply.TabIndex = 21
        Me.btnApply.Text = "Apply"
        Me.ToolTip1.SetToolTip(Me.btnApply, "Apply")
        Me.btnApply.UseVisualStyleBackColor = True
        '
        'frmHistorysalespurchase
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1080, 642)
        Me.Controls.Add(Me.btnApply)
        Me.Controls.Add(Me.Label57)
        Me.Controls.Add(Me.cmbCurrency)
        Me.Controls.Add(Me.txtCurrencyRate)
        Me.Controls.Add(Me.lblCurrencyRate)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.UltraTabControl2)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmHistorysalespurchase"
        Me.Text = "History"
        Me.UltraTabPageControl7.ResumeLayout(False)
        CType(Me.grdItemSales, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl8.ResumeLayout(False)
        CType(Me.grdItemPurchse, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTabControl2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl2.ResumeLayout(False)
        Me.UltraTabPageControl1.ResumeLayout(False)
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents UltraTabControl2 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage3 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl7 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grdItemSales As Janus.Windows.GridEX.GridEX
    Friend WithEvents UltraTabPageControl8 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grdItemPurchse As Janus.Windows.GridEX.GridEX
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnLoadAll As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Label57 As System.Windows.Forms.Label
    Friend WithEvents cmbCurrency As System.Windows.Forms.ComboBox
    Friend WithEvents txtCurrencyRate As System.Windows.Forms.TextBox
    Friend WithEvents lblCurrencyRate As System.Windows.Forms.Label
    Friend WithEvents btnApply As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip

End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUpdateCurrency
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
        Dim grdCurrencyRate_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUpdateCurrency))
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.dtpDate = New System.Windows.Forms.DateTimePicker()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.grdCurrencyRate = New Janus.Windows.GridEX.GridEX()
        Me.lblBaseCurrency = New System.Windows.Forms.Label()
        Me.cmbCurrency = New System.Windows.Forms.ComboBox()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.grdCurrencyRate, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(9, 17)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(383, 45)
        Me.lblHeader.TabIndex = 46
        Me.lblHeader.Text = "Update currency rate"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnSave, Me.btnRefresh})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(950, 32)
        Me.ToolStrip1.TabIndex = 47
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnSave
        '
        Me.btnSave.Image = Global.SimpleAccounts.My.Resources.Resources.BtnSave_Image
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(98, 29)
        Me.btnSave.Text = "&Update"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 29)
        Me.btnRefresh.Text = "&Refresh"
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(60, 123)
        Me.dtpDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(302, 26)
        Me.dtpDate.TabIndex = 48
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.Location = New System.Drawing.Point(10, 128)
        Me.lblDate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(44, 20)
        Me.lblDate.TabIndex = 49
        Me.lblDate.Text = "Date"
        '
        'grdCurrencyRate
        '
        Me.grdCurrencyRate.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdCurrencyRate_DesignTimeLayout.LayoutString = resources.GetString("grdCurrencyRate_DesignTimeLayout.LayoutString")
        Me.grdCurrencyRate.DesignTimeLayout = grdCurrencyRate_DesignTimeLayout
        Me.grdCurrencyRate.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdCurrencyRate.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdCurrencyRate.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdCurrencyRate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdCurrencyRate.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.grdCurrencyRate.Location = New System.Drawing.Point(0, 177)
        Me.grdCurrencyRate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdCurrencyRate.Name = "grdCurrencyRate"
        Me.grdCurrencyRate.RecordNavigator = True
        Me.grdCurrencyRate.Size = New System.Drawing.Size(950, 620)
        Me.grdCurrencyRate.TabIndex = 50
        Me.grdCurrencyRate.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdCurrencyRate.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdCurrencyRate.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'lblBaseCurrency
        '
        Me.lblBaseCurrency.AutoSize = True
        Me.lblBaseCurrency.Location = New System.Drawing.Point(396, 129)
        Me.lblBaseCurrency.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBaseCurrency.Name = "lblBaseCurrency"
        Me.lblBaseCurrency.Size = New System.Drawing.Size(113, 20)
        Me.lblBaseCurrency.TabIndex = 52
        Me.lblBaseCurrency.Text = "Base Currency"
        '
        'cmbCurrency
        '
        Me.cmbCurrency.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Append
        Me.cmbCurrency.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbCurrency.Enabled = False
        Me.cmbCurrency.FormattingEnabled = True
        Me.cmbCurrency.Location = New System.Drawing.Point(519, 123)
        Me.cmbCurrency.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbCurrency.Name = "cmbCurrency"
        Me.cmbCurrency.Size = New System.Drawing.Size(290, 28)
        Me.cmbCurrency.TabIndex = 53
        Me.cmbCurrency.TabStop = False
        Me.cmbCurrency.Tag = "Currency"
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Font = New System.Drawing.Font("Times New Roman", 8.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.pnlHeader.ForeColor = System.Drawing.Color.Black
        Me.pnlHeader.Location = New System.Drawing.Point(0, 32)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(950, 66)
        Me.pnlHeader.TabIndex = 54
        '
        'frmUpdateCurrency
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(950, 694)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.cmbCurrency)
        Me.Controls.Add(Me.lblBaseCurrency)
        Me.Controls.Add(Me.grdCurrencyRate)
        Me.Controls.Add(Me.lblDate)
        Me.Controls.Add(Me.dtpDate)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmUpdateCurrency"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Update Currency"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.grdCurrencyRate, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents grdCurrencyRate As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblBaseCurrency As System.Windows.Forms.Label
    Friend WithEvents cmbCurrency As System.Windows.Forms.ComboBox
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

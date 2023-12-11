<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDefCommissionBySaleman
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDefCommissionBySaleman))
        Dim grd_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.OpenToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtStartValue = New System.Windows.Forms.TextBox()
        Me.txtEndValue = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cmbVendor = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.chkActive = New System.Windows.Forms.CheckBox()
        Me.txtSortOrder = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtPercentage = New System.Windows.Forms.TextBox()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.btnPrint, Me.toolStripSeparator2, Me.btnDelete, Me.toolStripSeparator3, Me.btnRefresh, Me.HelpToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1185, 32)
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
        'btnEdit
        '
        Me.btnEdit.Image = CType(resources.GetObject("btnEdit.Image"), System.Drawing.Image)
        Me.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(70, 29)
        Me.btnEdit.Text = "&Edit"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(77, 29)
        Me.btnSave.Text = "&Save"
        '
        'btnPrint
        '
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(76, 29)
        Me.btnPrint.Text = "&Print"
        Me.btnPrint.Visible = False
        '
        'toolStripSeparator2
        '
        Me.toolStripSeparator2.Name = "toolStripSeparator2"
        Me.toolStripSeparator2.Size = New System.Drawing.Size(6, 32)
        '
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(90, 29)
        Me.btnDelete.Text = "D&elete"
        '
        'toolStripSeparator3
        '
        Me.toolStripSeparator3.Name = "toolStripSeparator3"
        Me.toolStripSeparator3.Size = New System.Drawing.Size(6, 32)
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 29)
        Me.btnRefresh.Text = "Refresh"
        '
        'HelpToolStripButton
        '
        Me.HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HelpToolStripButton.Image = CType(resources.GetObject("HelpToolStripButton.Image"), System.Drawing.Image)
        Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton.Name = "HelpToolStripButton"
        Me.HelpToolStripButton.Size = New System.Drawing.Size(28, 29)
        Me.HelpToolStripButton.Text = "He&lp"
        '
        'OpenToolStripButton
        '
        Me.OpenToolStripButton.Image = CType(resources.GetObject("OpenToolStripButton.Image"), System.Drawing.Image)
        Me.OpenToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.OpenToolStripButton.Name = "OpenToolStripButton"
        Me.OpenToolStripButton.Size = New System.Drawing.Size(47, 22)
        Me.OpenToolStripButton.Text = "&Edit"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(24, 14)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(285, 35)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Saleman Commission"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 157)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(89, 20)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Start Value"
        '
        'txtStartValue
        '
        Me.txtStartValue.Location = New System.Drawing.Point(123, 152)
        Me.txtStartValue.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtStartValue.Name = "txtStartValue"
        Me.txtStartValue.Size = New System.Drawing.Size(352, 26)
        Me.txtStartValue.TabIndex = 5
        '
        'txtEndValue
        '
        Me.txtEndValue.Location = New System.Drawing.Point(123, 192)
        Me.txtEndValue.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtEndValue.Name = "txtEndValue"
        Me.txtEndValue.Size = New System.Drawing.Size(352, 26)
        Me.txtEndValue.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 197)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(83, 20)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "End Value"
        '
        'cmbVendor
        '
        Me.cmbVendor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbVendor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbVendor.FormattingEnabled = True
        Me.cmbVendor.Location = New System.Drawing.Point(123, 111)
        Me.cmbVendor.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbVendor.Name = "cmbVendor"
        Me.cmbVendor.Size = New System.Drawing.Size(352, 28)
        Me.cmbVendor.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(18, 115)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 20)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Saleman"
        '
        'chkActive
        '
        Me.chkActive.AutoSize = True
        Me.chkActive.Checked = True
        Me.chkActive.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkActive.Location = New System.Drawing.Point(123, 312)
        Me.chkActive.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkActive.Name = "chkActive"
        Me.chkActive.Size = New System.Drawing.Size(78, 24)
        Me.chkActive.TabIndex = 12
        Me.chkActive.Text = "Active"
        Me.chkActive.UseVisualStyleBackColor = True
        '
        'txtSortOrder
        '
        Me.txtSortOrder.Location = New System.Drawing.Point(123, 272)
        Me.txtSortOrder.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtSortOrder.Name = "txtSortOrder"
        Me.txtSortOrder.Size = New System.Drawing.Size(78, 26)
        Me.txtSortOrder.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(18, 277)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(83, 20)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Sort Order"
        '
        'grd
        '
        Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grd_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grd.DesignTimeLayout = grd_DesignTimeLayout
        Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.grd.GroupByBoxVisible = False
        Me.grd.Location = New System.Drawing.Point(0, 348)
        Me.grd.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(1185, 514)
        Me.grd.TabIndex = 13
        Me.grd.TabStop = False
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(18, 237)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(91, 20)
        Me.Label6.TabIndex = 8
        Me.Label6.Text = "Percentage"
        '
        'txtPercentage
        '
        Me.txtPercentage.Location = New System.Drawing.Point(123, 232)
        Me.txtPercentage.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtPercentage.Name = "txtPercentage"
        Me.txtPercentage.Size = New System.Drawing.Size(352, 26)
        Me.txtPercentage.TabIndex = 9
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(396, 397)
        Me.lblProgress.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(394, 69)
        Me.lblProgress.TabIndex = 22
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'pnlHeader
        '
        Me.pnlHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Location = New System.Drawing.Point(0, 42)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1185, 60)
        Me.pnlHeader.TabIndex = 23
        '
        'frmDefCommissionBySaleman
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1185, 862)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtPercentage)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtSortOrder)
        Me.Controls.Add(Me.chkActive)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbVendor)
        Me.Controls.Add(Me.txtEndValue)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtStartValue)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmDefCommissionBySaleman"
        Me.Text = "Saleman Commission"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtStartValue As System.Windows.Forms.TextBox
    Friend WithEvents txtEndValue As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cmbVendor As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkActive As System.Windows.Forms.CheckBox
    Friend WithEvents txtSortOrder As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtPercentage As System.Windows.Forms.TextBox
    Friend WithEvents OpenToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

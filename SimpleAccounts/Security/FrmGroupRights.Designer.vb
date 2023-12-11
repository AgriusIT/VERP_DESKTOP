<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmGroupRights
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmGroupRights))
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.grdAllRecords = New Janus.Windows.GridEX.GridEX()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lstFormCategories = New SimpleAccounts.uiListControl()
        Me.chkExportAll = New System.Windows.Forms.CheckBox()
        Me.chkPrintAll = New System.Windows.Forms.CheckBox()
        Me.chkViewAll = New System.Windows.Forms.CheckBox()
        Me.chkDeleteAll = New System.Windows.Forms.CheckBox()
        Me.chkUpdateAll = New System.Windows.Forms.CheckBox()
        Me.chkSaveAll = New System.Windows.Forms.CheckBox()
        Me.btnExpnd1stLevel = New System.Windows.Forms.Button()
        Me.btnExpnd2ndLevel = New System.Windows.Forms.Button()
        Me.cboFormGroups = New System.Windows.Forms.ComboBox()
        Me.lblFormGroups = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdAllRecords, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.grdAllRecords)
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBox2.Location = New System.Drawing.Point(255, 75)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(773, 656)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        '
        'grdAllRecords
        '
        Me.grdAllRecords.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdAllRecords.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdAllRecords.AutomaticSort = False
        Me.grdAllRecords.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.grdAllRecords.EmptyRows = True
        Me.grdAllRecords.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdAllRecords.GroupByBoxVisible = False
        Me.grdAllRecords.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.grdAllRecords.Location = New System.Drawing.Point(3, 9)
        Me.grdAllRecords.Name = "grdAllRecords"
        Me.grdAllRecords.RecordNavigator = True
        Me.grdAllRecords.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdAllRecords.Size = New System.Drawing.Size(768, 644)
        Me.grdAllRecords.TabIndex = 0
        Me.grdAllRecords.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        Me.grdAllRecords.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdAllRecords.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.GroupBox1.Controls.Add(Me.lstFormCategories)
        Me.GroupBox1.Controls.Add(Me.chkExportAll)
        Me.GroupBox1.Controls.Add(Me.chkPrintAll)
        Me.GroupBox1.Controls.Add(Me.chkViewAll)
        Me.GroupBox1.Controls.Add(Me.chkDeleteAll)
        Me.GroupBox1.Controls.Add(Me.chkUpdateAll)
        Me.GroupBox1.Controls.Add(Me.chkSaveAll)
        Me.GroupBox1.Controls.Add(Me.btnExpnd1stLevel)
        Me.GroupBox1.Controls.Add(Me.btnExpnd2ndLevel)
        Me.GroupBox1.Controls.Add(Me.cboFormGroups)
        Me.GroupBox1.Controls.Add(Me.lblFormGroups)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBox1.Location = New System.Drawing.Point(12, 75)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(237, 646)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        '
        'lstFormCategories
        '
        Me.lstFormCategories.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstFormCategories.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstFormCategories.BackColor = System.Drawing.Color.Transparent
        Me.lstFormCategories.disableWhenChecked = False
        Me.lstFormCategories.HeadingLabelName = Nothing
        Me.lstFormCategories.HeadingText = Nothing
        Me.lstFormCategories.Location = New System.Drawing.Point(3, 69)
        Me.lstFormCategories.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.lstFormCategories.Name = "lstFormCategories"
        Me.lstFormCategories.ShowAddNewButton = False
        Me.lstFormCategories.ShowInverse = True
        Me.lstFormCategories.ShowMagnifierButton = False
        Me.lstFormCategories.ShowNoCheck = False
        Me.lstFormCategories.ShowResetAllButton = False
        Me.lstFormCategories.ShowSelectall = True
        Me.lstFormCategories.Size = New System.Drawing.Size(203, 194)
        Me.lstFormCategories.TabIndex = 2
        Me.lstFormCategories.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'chkExportAll
        '
        Me.chkExportAll.AutoSize = True
        Me.chkExportAll.Location = New System.Drawing.Point(6, 428)
        Me.chkExportAll.Name = "chkExportAll"
        Me.chkExportAll.Size = New System.Drawing.Size(102, 24)
        Me.chkExportAll.TabIndex = 10
        Me.chkExportAll.Text = "Export All"
        Me.chkExportAll.UseVisualStyleBackColor = True
        '
        'chkPrintAll
        '
        Me.chkPrintAll.AutoSize = True
        Me.chkPrintAll.Location = New System.Drawing.Point(6, 405)
        Me.chkPrintAll.Name = "chkPrintAll"
        Me.chkPrintAll.Size = New System.Drawing.Size(88, 24)
        Me.chkPrintAll.TabIndex = 9
        Me.chkPrintAll.Text = "Print All"
        Me.chkPrintAll.UseVisualStyleBackColor = True
        '
        'chkViewAll
        '
        Me.chkViewAll.AutoSize = True
        Me.chkViewAll.Location = New System.Drawing.Point(6, 316)
        Me.chkViewAll.Name = "chkViewAll"
        Me.chkViewAll.Size = New System.Drawing.Size(90, 24)
        Me.chkViewAll.TabIndex = 5
        Me.chkViewAll.Text = "View All"
        Me.chkViewAll.UseVisualStyleBackColor = True
        '
        'chkDeleteAll
        '
        Me.chkDeleteAll.AutoSize = True
        Me.chkDeleteAll.Location = New System.Drawing.Point(6, 382)
        Me.chkDeleteAll.Name = "chkDeleteAll"
        Me.chkDeleteAll.Size = New System.Drawing.Size(103, 24)
        Me.chkDeleteAll.TabIndex = 8
        Me.chkDeleteAll.Text = "Delete All"
        Me.chkDeleteAll.UseVisualStyleBackColor = True
        '
        'chkUpdateAll
        '
        Me.chkUpdateAll.AutoSize = True
        Me.chkUpdateAll.Location = New System.Drawing.Point(6, 359)
        Me.chkUpdateAll.Name = "chkUpdateAll"
        Me.chkUpdateAll.Size = New System.Drawing.Size(109, 24)
        Me.chkUpdateAll.TabIndex = 7
        Me.chkUpdateAll.Text = "Update All"
        Me.chkUpdateAll.UseVisualStyleBackColor = True
        '
        'chkSaveAll
        '
        Me.chkSaveAll.AutoSize = True
        Me.chkSaveAll.Location = New System.Drawing.Point(6, 339)
        Me.chkSaveAll.Name = "chkSaveAll"
        Me.chkSaveAll.Size = New System.Drawing.Size(92, 24)
        Me.chkSaveAll.TabIndex = 6
        Me.chkSaveAll.Text = "Save All"
        Me.chkSaveAll.UseVisualStyleBackColor = True
        '
        'btnExpnd1stLevel
        '
        Me.btnExpnd1stLevel.Location = New System.Drawing.Point(3, 269)
        Me.btnExpnd1stLevel.Name = "btnExpnd1stLevel"
        Me.btnExpnd1stLevel.Size = New System.Drawing.Size(99, 41)
        Me.btnExpnd1stLevel.TabIndex = 3
        Me.btnExpnd1stLevel.Tag = ""
        Me.btnExpnd1stLevel.Text = " Expand/Collapse to 1st Level"
        Me.btnExpnd1stLevel.UseVisualStyleBackColor = True
        '
        'btnExpnd2ndLevel
        '
        Me.btnExpnd2ndLevel.Location = New System.Drawing.Point(107, 269)
        Me.btnExpnd2ndLevel.Name = "btnExpnd2ndLevel"
        Me.btnExpnd2ndLevel.Size = New System.Drawing.Size(99, 41)
        Me.btnExpnd2ndLevel.TabIndex = 4
        Me.btnExpnd2ndLevel.Tag = ""
        Me.btnExpnd2ndLevel.Text = " Expand/Collapse to 2nd Level"
        Me.btnExpnd2ndLevel.UseVisualStyleBackColor = True
        '
        'cboFormGroups
        '
        Me.cboFormGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFormGroups.FormattingEnabled = True
        Me.cboFormGroups.Location = New System.Drawing.Point(6, 42)
        Me.cboFormGroups.Name = "cboFormGroups"
        Me.cboFormGroups.Size = New System.Drawing.Size(206, 28)
        Me.cboFormGroups.TabIndex = 1
        '
        'lblFormGroups
        '
        Me.lblFormGroups.Location = New System.Drawing.Point(12, 24)
        Me.lblFormGroups.Name = "lblFormGroups"
        Me.lblFormGroups.Size = New System.Drawing.Size(100, 15)
        Me.lblFormGroups.TabIndex = 0
        Me.lblFormGroups.Text = "Form Groups"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.btnDelete, Me.btnPrint, Me.toolStripSeparator1, Me.HelpToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1030, 32)
        Me.ToolStrip1.TabIndex = 1
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
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.RightToLeftAutoMirrorImage = True
        Me.btnDelete.Size = New System.Drawing.Size(90, 29)
        Me.btnDelete.Text = "&Delete"
        '
        'btnPrint
        '
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(76, 29)
        Me.btnPrint.Text = "&Print"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 32)
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
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(14, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(174, 36)
        Me.lblHeader.TabIndex = 42
        Me.lblHeader.Text = "User Rights"
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.ForeColor = System.Drawing.Color.Black
        Me.pnlHeader.Location = New System.Drawing.Point(0, 32)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1030, 43)
        Me.pnlHeader.TabIndex = 10
        '
        'FrmGroupRights
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(1030, 733)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmGroupRights"
        Me.Text = "Group Rights"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grdAllRecords, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents grdAllRecords As Janus.Windows.GridEX.GridEX
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cboFormGroups As System.Windows.Forms.ComboBox
    Friend WithEvents lblFormGroups As System.Windows.Forms.Label
    Friend WithEvents btnExpnd1stLevel As System.Windows.Forms.Button
    Friend WithEvents btnExpnd2ndLevel As System.Windows.Forms.Button
    Friend WithEvents chkExportAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkPrintAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkViewAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkDeleteAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkUpdateAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkSaveAll As System.Windows.Forms.CheckBox
    Friend WithEvents lstFormCategories As uiListControl
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

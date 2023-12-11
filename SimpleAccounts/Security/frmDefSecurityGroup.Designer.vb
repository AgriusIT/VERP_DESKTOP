<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDefSecurityGroup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDefSecurityGroup))
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.grdAllRecords = New Janus.Windows.GridEX.GridEX()
        Me.UiCtrlGridBar1 = New SimpleAccounts.uiCtrlGridBar()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.cboGroupType = New System.Windows.Forms.ComboBox()
        Me.lblComments = New System.Windows.Forms.Label()
        Me.lblGroupType = New System.Windows.Forms.Label()
        Me.txtComments = New System.Windows.Forms.TextBox()
        Me.txtGroupName = New System.Windows.Forms.TextBox()
        Me.lblGroupName = New System.Windows.Forms.Label()
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
        Me.GroupBox2.Controls.Add(Me.UiCtrlGridBar1)
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBox2.Location = New System.Drawing.Point(5, 284)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(1013, 446)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        '
        'grdAllRecords
        '
        Me.grdAllRecords.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdAllRecords.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdAllRecords.EditorsControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Regular
        Me.grdAllRecords.EmptyRows = True
        Me.grdAllRecords.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdAllRecords.GroupByBoxVisible = False
        Me.grdAllRecords.HideSelection = Janus.Windows.GridEX.HideSelection.Highlight
        Me.grdAllRecords.Location = New System.Drawing.Point(3, 40)
        Me.grdAllRecords.Name = "grdAllRecords"
        Me.grdAllRecords.RecordNavigator = True
        Me.grdAllRecords.RowHeaders = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdAllRecords.Size = New System.Drawing.Size(1007, 403)
        Me.grdAllRecords.TabIndex = 1
        Me.grdAllRecords.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        Me.grdAllRecords.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'UiCtrlGridBar1
        '
        Me.UiCtrlGridBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UiCtrlGridBar1.Location = New System.Drawing.Point(4, 10)
        Me.UiCtrlGridBar1.MyGrid = Me.grdAllRecords
        Me.UiCtrlGridBar1.Name = "UiCtrlGridBar1"
        Me.UiCtrlGridBar1.Size = New System.Drawing.Size(1002, 25)
        Me.UiCtrlGridBar1.TabIndex = 0
        Me.UiCtrlGridBar1.TabStop = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.cboGroupType)
        Me.GroupBox1.Controls.Add(Me.lblComments)
        Me.GroupBox1.Controls.Add(Me.lblGroupType)
        Me.GroupBox1.Controls.Add(Me.txtComments)
        Me.GroupBox1.Controls.Add(Me.txtGroupName)
        Me.GroupBox1.Controls.Add(Me.lblGroupName)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.GroupBox1.Location = New System.Drawing.Point(12, 75)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(1013, 203)
        Me.GroupBox1.TabIndex = 3
        Me.GroupBox1.TabStop = False
        '
        'cboGroupType
        '
        Me.cboGroupType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboGroupType.FormattingEnabled = True
        Me.cboGroupType.Location = New System.Drawing.Point(641, 49)
        Me.cboGroupType.Name = "cboGroupType"
        Me.cboGroupType.Size = New System.Drawing.Size(212, 28)
        Me.cboGroupType.TabIndex = 4
        Me.cboGroupType.Tag = "IsRequired"
        Me.cboGroupType.Visible = False
        '
        'lblComments
        '
        Me.lblComments.Location = New System.Drawing.Point(12, 52)
        Me.lblComments.Name = "lblComments"
        Me.lblComments.Size = New System.Drawing.Size(100, 15)
        Me.lblComments.TabIndex = 2
        Me.lblComments.Text = "Group Description"
        '
        'lblGroupType
        '
        Me.lblGroupType.Location = New System.Drawing.Point(638, 89)
        Me.lblGroupType.Name = "lblGroupType"
        Me.lblGroupType.Size = New System.Drawing.Size(100, 15)
        Me.lblGroupType.TabIndex = 5
        Me.lblGroupType.Text = "Group Type"
        Me.lblGroupType.Visible = False
        '
        'txtComments
        '
        Me.txtComments.Location = New System.Drawing.Point(114, 47)
        Me.txtComments.MaxLength = 250
        Me.txtComments.Multiline = True
        Me.txtComments.Name = "txtComments"
        Me.txtComments.Size = New System.Drawing.Size(348, 145)
        Me.txtComments.TabIndex = 3
        '
        'txtGroupName
        '
        Me.txtGroupName.Location = New System.Drawing.Point(114, 21)
        Me.txtGroupName.MaxLength = 50
        Me.txtGroupName.Name = "txtGroupName"
        Me.txtGroupName.Size = New System.Drawing.Size(348, 26)
        Me.txtGroupName.TabIndex = 1
        Me.txtGroupName.Tag = "IsRequired"
        '
        'lblGroupName
        '
        Me.lblGroupName.Location = New System.Drawing.Point(12, 24)
        Me.lblGroupName.Name = "lblGroupName"
        Me.lblGroupName.Size = New System.Drawing.Size(100, 15)
        Me.lblGroupName.TabIndex = 0
        Me.lblGroupName.Text = "Group Name"
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
        Me.lblHeader.Location = New System.Drawing.Point(8, 10)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(225, 36)
        Me.lblHeader.TabIndex = 2
        Me.lblHeader.Text = "Security Group"
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.pnlHeader.Location = New System.Drawing.Point(0, 32)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1030, 43)
        Me.pnlHeader.TabIndex = 10
        '
        'frmDefSecurityGroup
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1030, 744)
        Me.ControlBox = False
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmDefSecurityGroup"
        Me.Text = "Security Group"
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
    Friend WithEvents UiCtrlGridBar1 As uiCtrlGridBar
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents lblComments As System.Windows.Forms.Label
    Friend WithEvents lblGroupType As System.Windows.Forms.Label
    Friend WithEvents txtComments As System.Windows.Forms.TextBox
    Friend WithEvents txtGroupName As System.Windows.Forms.TextBox
    Friend WithEvents lblGroupName As System.Windows.Forms.Label
    Friend WithEvents cboGroupType As System.Windows.Forms.ComboBox
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

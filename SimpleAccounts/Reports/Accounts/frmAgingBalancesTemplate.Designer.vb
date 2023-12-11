<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAgingBalancesTemplate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAgingBalancesTemplate))
        Dim grdConnectionInfo_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.grdConnectionInfo = New Janus.Windows.GridEX.GridEX()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.txtAging = New System.Windows.Forms.TextBox()
        Me.txt3rdAging = New System.Windows.Forms.TextBox()
        Me.txt3rdAgingName = New System.Windows.Forms.TextBox()
        Me.txt2ndAging = New System.Windows.Forms.TextBox()
        Me.txt2ndAgingName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txt1stAging = New System.Windows.Forms.TextBox()
        Me.txt1stAgingName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtFormatName = New System.Windows.Forms.TextBox()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.grdConnectionInfo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.toolStripSeparator, Me.btnDelete, Me.toolStripSeparator1, Me.HelpToolStripButton})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(693, 32)
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
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 32)
        '
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(90, 29)
        Me.btnDelete.Text = "D&elete"
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
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 32)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.grdConnectionInfo)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.pnlHeader)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtAging)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txt3rdAging)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txt3rdAgingName)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txt2ndAging)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txt2ndAgingName)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label4)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label3)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txt1stAging)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txt1stAgingName)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.txtFormatName)
        Me.SplitContainer1.Size = New System.Drawing.Size(693, 514)
        Me.SplitContainer1.SplitterDistance = 275
        Me.SplitContainer1.TabIndex = 1
        '
        'grdConnectionInfo
        '
        Me.grdConnectionInfo.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdConnectionInfo.CardViewGridlines = Janus.Windows.GridEX.CardViewGridlines.FieldsOnly
        Me.grdConnectionInfo.ColumnSetNavigation = Janus.Windows.GridEX.ColumnSetNavigation.ColumnSet
        grdConnectionInfo_DesignTimeLayout.LayoutString = resources.GetString("grdConnectionInfo_DesignTimeLayout.LayoutString")
        Me.grdConnectionInfo.DesignTimeLayout = grdConnectionInfo_DesignTimeLayout
        Me.grdConnectionInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdConnectionInfo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grdConnectionInfo.GroupByBoxVisible = False
        Me.grdConnectionInfo.GroupMode = Janus.Windows.GridEX.GroupMode.Collapsed
        Me.grdConnectionInfo.Location = New System.Drawing.Point(0, 0)
        Me.grdConnectionInfo.Name = "grdConnectionInfo"
        Me.grdConnectionInfo.RecordNavigator = True
        Me.grdConnectionInfo.Size = New System.Drawing.Size(275, 514)
        Me.grdConnectionInfo.TabIndex = 0
        Me.grdConnectionInfo.TableViewHorizontalScrollIncrement = 20
        Me.grdConnectionInfo.TabStop = False
        Me.grdConnectionInfo.UseCompatibleTextRendering = True
        Me.grdConnectionInfo.View = Janus.Windows.GridEX.View.CardView
        Me.grdConnectionInfo.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(414, 41)
        Me.pnlHeader.TabIndex = 66
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(3, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(347, 36)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Template Aging Balance"
        '
        'txtAging
        '
        Me.txtAging.Location = New System.Drawing.Point(103, 102)
        Me.txtAging.Name = "txtAging"
        Me.txtAging.Size = New System.Drawing.Size(50, 26)
        Me.txtAging.TabIndex = 5
        '
        'txt3rdAging
        '
        Me.txt3rdAging.Location = New System.Drawing.Point(103, 178)
        Me.txt3rdAging.Name = "txt3rdAging"
        Me.txt3rdAging.Size = New System.Drawing.Size(50, 26)
        Me.txt3rdAging.TabIndex = 10
        '
        'txt3rdAgingName
        '
        Me.txt3rdAgingName.Enabled = False
        Me.txt3rdAgingName.Location = New System.Drawing.Point(158, 178)
        Me.txt3rdAgingName.Name = "txt3rdAgingName"
        Me.txt3rdAgingName.Size = New System.Drawing.Size(93, 26)
        Me.txt3rdAgingName.TabIndex = 11
        '
        'txt2ndAging
        '
        Me.txt2ndAging.Location = New System.Drawing.Point(103, 152)
        Me.txt2ndAging.Name = "txt2ndAging"
        Me.txt2ndAging.Size = New System.Drawing.Size(50, 26)
        Me.txt2ndAging.TabIndex = 8
        '
        'txt2ndAgingName
        '
        Me.txt2ndAgingName.Enabled = False
        Me.txt2ndAgingName.Location = New System.Drawing.Point(158, 152)
        Me.txt2ndAgingName.Name = "txt2ndAgingName"
        Me.txt2ndAgingName.Size = New System.Drawing.Size(93, 26)
        Me.txt2ndAgingName.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(155, 86)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(91, 20)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Aging Desc"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(100, 86)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 20)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Aging"
        '
        'txt1stAging
        '
        Me.txt1stAging.Location = New System.Drawing.Point(103, 126)
        Me.txt1stAging.Name = "txt1stAging"
        Me.txt1stAging.Size = New System.Drawing.Size(50, 26)
        Me.txt1stAging.TabIndex = 6
        '
        'txt1stAgingName
        '
        Me.txt1stAgingName.Enabled = False
        Me.txt1stAgingName.Location = New System.Drawing.Point(158, 126)
        Me.txt1stAgingName.Name = "txt1stAgingName"
        Me.txt1stAgingName.Size = New System.Drawing.Size(93, 26)
        Me.txt1stAgingName.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 53)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(106, 20)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Format Name"
        '
        'txtFormatName
        '
        Me.txtFormatName.Location = New System.Drawing.Point(103, 50)
        Me.txtFormatName.Name = "txtFormatName"
        Me.txtFormatName.Size = New System.Drawing.Size(148, 26)
        Me.txtFormatName.TabIndex = 2
        '
        'frmAgingBalancesTemplate
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(693, 546)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAgingBalancesTemplate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Template Aging Balance"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.grdConnectionInfo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents grdConnectionInfo As Janus.Windows.GridEX.GridEX
    Friend WithEvents txt1stAgingName As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtFormatName As System.Windows.Forms.TextBox
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents txt3rdAging As System.Windows.Forms.TextBox
    Friend WithEvents txt3rdAgingName As System.Windows.Forms.TextBox
    Friend WithEvents txt2ndAging As System.Windows.Forms.TextBox
    Friend WithEvents txt2ndAgingName As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txt1stAging As System.Windows.Forms.TextBox
    Friend WithEvents txtAging As System.Windows.Forms.TextBox
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

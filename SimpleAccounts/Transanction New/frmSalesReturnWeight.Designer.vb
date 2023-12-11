<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSalesReturnWeight
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
        Me.components = New System.ComponentModel.Container()
        Dim grd_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSalesReturnWeight))
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.btnAbout = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.BtnLoad = New System.Windows.Forms.Button()
        Me.cmbDepartment = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'grd
        '
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grd_DesignTimeLayout.LayoutString = resources.GetString("grd_DesignTimeLayout.LayoutString")
        Me.grd.DesignTimeLayout = grd_DesignTimeLayout
        Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grd.GroupByBoxVisible = False
        Me.grd.Location = New System.Drawing.Point(1, 113)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(553, 174)
        Me.grd.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.grd, "Items Weight Detail")
        Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnSave, Me.toolStripSeparator, Me.btnAbout, Me.toolStripSeparator1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(555, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(51, 22)
        Me.btnNew.Text = "&New"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(51, 22)
        Me.btnSave.Text = "&Save"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 25)
        '
        'btnAbout
        '
        Me.btnAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnAbout.Image = CType(resources.GetObject("btnAbout.Image"), System.Drawing.Image)
        Me.btnAbout.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAbout.Name = "btnAbout"
        Me.btnAbout.Size = New System.Drawing.Size(23, 22)
        Me.btnAbout.Text = "He&lp"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(11, 8)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(179, 23)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Item Calculator"
        '
        'BtnLoad
        '
        Me.BtnLoad.Location = New System.Drawing.Point(289, 76)
        Me.BtnLoad.Name = "BtnLoad"
        Me.BtnLoad.Size = New System.Drawing.Size(75, 23)
        Me.BtnLoad.TabIndex = 5
        Me.BtnLoad.Text = "Load"
        Me.ToolTip1.SetToolTip(Me.BtnLoad, "Load Items")
        Me.BtnLoad.UseVisualStyleBackColor = True
        '
        'cmbDepartment
        '
        Me.cmbDepartment.FormattingEnabled = True
        Me.cmbDepartment.Location = New System.Drawing.Point(80, 78)
        Me.cmbDepartment.Name = "cmbDepartment"
        Me.cmbDepartment.Size = New System.Drawing.Size(203, 21)
        Me.cmbDepartment.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.cmbDepartment, "Select Any Item Department")
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 81)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 13)
        Me.Label2.TabIndex = 7
        Me.Label2.Text = "Department"
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(146, 122)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 18
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 25)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(555, 45)
        Me.pnlHeader.TabIndex = 19
        '
        'frmSalesReturnWeight
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(555, 288)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbDepartment)
        Me.Controls.Add(Me.BtnLoad)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.grd)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmSalesReturnWeight"
        Me.Text = "Item Weight"
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnAbout As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents BtnLoad As System.Windows.Forms.Button
    Friend WithEvents cmbDepartment As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

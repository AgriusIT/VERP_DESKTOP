<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBudgetConfiguration
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
        Dim grdPayment_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBudgetConfiguration))
        Dim grdSimple_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.cmbBudget = New System.Windows.Forms.ComboBox()
        Me.lblBudget = New System.Windows.Forms.Label()
        Me.grdPayment = New Janus.Windows.GridEX.GridEX()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnHelp = New System.Windows.Forms.ToolStripButton()
        Me.grdSimple = New Janus.Windows.GridEX.GridEX()
        CType(Me.grdPayment, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.grdSimple, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmbBudget
        '
        Me.cmbBudget.FormattingEnabled = True
        Me.cmbBudget.Location = New System.Drawing.Point(198, 123)
        Me.cmbBudget.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbBudget.Name = "cmbBudget"
        Me.cmbBudget.Size = New System.Drawing.Size(286, 28)
        Me.cmbBudget.TabIndex = 0
        '
        'lblBudget
        '
        Me.lblBudget.AutoSize = True
        Me.lblBudget.Location = New System.Drawing.Point(99, 128)
        Me.lblBudget.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblBudget.Name = "lblBudget"
        Me.lblBudget.Size = New System.Drawing.Size(61, 20)
        Me.lblBudget.TabIndex = 1
        Me.lblBudget.Text = "Budget"
        '
        'grdPayment
        '
        Me.grdPayment.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdPayment.ColumnAutoResize = True
        grdPayment_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grdPayment.DesignTimeLayout = grdPayment_DesignTimeLayout
        Me.grdPayment.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdPayment.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdPayment.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.grdPayment.GroupByBoxVisible = False
        Me.grdPayment.Location = New System.Drawing.Point(-2, 166)
        Me.grdPayment.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdPayment.Name = "grdPayment"
        Me.grdPayment.RecordNavigator = True
        Me.grdPayment.Size = New System.Drawing.Size(1134, 615)
        Me.grdPayment.TabIndex = 34
        Me.grdPayment.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdPayment.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdPayment.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Panel4
        '
        Me.Panel4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Panel4.Controls.Add(Me.btnCancel)
        Me.Panel4.Controls.Add(Me.Label15)
        Me.Panel4.Location = New System.Drawing.Point(-2, 38)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1134, 74)
        Me.Panel4.TabIndex = 35
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnCancel.BackgroundImage = CType(resources.GetObject("btnCancel.BackgroundImage"), System.Drawing.Image)
        Me.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancel.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.btnCancel.Location = New System.Drawing.Point(1058, 11)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(57, 46)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.UseVisualStyleBackColor = False
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(18, 14)
        Me.Label15.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(318, 36)
        Me.Label15.TabIndex = 1
        Me.Label15.Text = "Budget Configuration"
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(512, 120)
        Me.btnShow.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(112, 35)
        Me.btnShow.TabIndex = 36
        Me.btnShow.Text = "Show"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.btnDelete, Me.btnRefresh, Me.btnPrint, Me.toolStripSeparator1, Me.btnHelp})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1131, 32)
        Me.ToolStrip1.TabIndex = 37
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
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 29)
        Me.btnRefresh.Text = "&Refresh"
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
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 32)
        '
        'btnHelp
        '
        Me.btnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(28, 29)
        Me.btnHelp.Text = "He&lp"
        Me.btnHelp.Visible = False
        '
        'grdSimple
        '
        Me.grdSimple.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSimple.ColumnAutoResize = True
        grdSimple_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grdSimple.DesignTimeLayout = grdSimple_DesignTimeLayout
        Me.grdSimple.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdSimple.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdSimple.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.grdSimple.GroupByBoxVisible = False
        Me.grdSimple.Location = New System.Drawing.Point(-2, 166)
        Me.grdSimple.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdSimple.Name = "grdSimple"
        Me.grdSimple.RecordNavigator = True
        Me.grdSimple.Size = New System.Drawing.Size(1134, 615)
        Me.grdSimple.TabIndex = 38
        Me.grdSimple.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdSimple.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdSimple.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'frmBudgetConfiguration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1131, 782)
        Me.Controls.Add(Me.grdSimple)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.btnShow)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.grdPayment)
        Me.Controls.Add(Me.lblBudget)
        Me.Controls.Add(Me.cmbBudget)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmBudgetConfiguration"
        Me.Text = "frmBudgetConfiguration"
        CType(Me.grdPayment, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.grdSimple, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmbBudget As System.Windows.Forms.ComboBox
    Friend WithEvents lblBudget As System.Windows.Forms.Label
    Friend WithEvents grdPayment As Janus.Windows.GridEX.GridEX
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents grdSimple As Janus.Windows.GridEX.GridEX
End Class

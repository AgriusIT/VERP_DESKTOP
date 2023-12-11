<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDefUnitOfItem
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
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Fixed Assets")
        Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Cash In Hand")
        Dim TreeNode3 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Cash Account", New System.Windows.Forms.TreeNode() {TreeNode2})
        Dim TreeNode4 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Meezan Bank")
        Dim TreeNode5 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Bank Account", New System.Windows.Forms.TreeNode() {TreeNode4})
        Dim TreeNode6 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Current Assets", New System.Windows.Forms.TreeNode() {TreeNode3, TreeNode5})
        Dim TreeNode7 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Liquid Assets")
        Dim TreeNode8 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Assets", New System.Windows.Forms.TreeNode() {TreeNode1, TreeNode6, TreeNode7})
        Dim TreeNode9 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Liabilities")
        Dim TreeNode10 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Capital")
        Dim GridEX1_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmDefUnitOfItem))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnShowAll = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TreeView1 = New System.Windows.Forms.TreeView()
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX()
        Me.ContextMenuStrip2 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnShowAll)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btnAdd)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1057, 88)
        Me.Panel1.TabIndex = 11
        '
        'btnShowAll
        '
        Me.btnShowAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnShowAll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnShowAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnShowAll.Image = Global.SimpleAccounts.My.Resources.Resources.btn_settings_blue2
        Me.btnShowAll.Location = New System.Drawing.Point(965, 12)
        Me.btnShowAll.Name = "btnShowAll"
        Me.btnShowAll.Size = New System.Drawing.Size(68, 64)
        Me.btnShowAll.TabIndex = 17
        Me.btnShowAll.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(12, 19)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(180, 30)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Unit Information"
        '
        'TreeView1
        '
        Me.TreeView1.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.TreeView1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView1.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TreeView1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.TreeView1.Location = New System.Drawing.Point(0, 88)
        Me.TreeView1.Name = "TreeView1"
        TreeNode1.Name = "FixedAssets"
        TreeNode1.Text = "Fixed Assets"
        TreeNode2.Name = "CashInHand"
        TreeNode2.Text = "Cash In Hand"
        TreeNode3.Name = "CashAccount"
        TreeNode3.Text = "Cash Account"
        TreeNode4.Name = "MeezanBank"
        TreeNode4.Text = "Meezan Bank"
        TreeNode5.Name = "BankAccount"
        TreeNode5.Text = "Bank Account"
        TreeNode6.Name = "CurrentAssets"
        TreeNode6.Text = "Current Assets"
        TreeNode7.Name = "LiquidAssets"
        TreeNode7.Text = "Liquid Assets"
        TreeNode8.Name = "Assets"
        TreeNode8.Text = "Assets"
        TreeNode9.Name = "Liabilities"
        TreeNode9.Text = "Liabilities"
        TreeNode10.Name = "Capital"
        TreeNode10.Text = "Capital"
        Me.TreeView1.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode8, TreeNode9, TreeNode10})
        Me.TreeView1.Size = New System.Drawing.Size(1057, 527)
        Me.TreeView1.TabIndex = 12
        '
        'GridEX1
        '
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX1.ColumnAutoResize = True
        GridEX1_DesignTimeLayout.LayoutString = resources.GetString("GridEX1_DesignTimeLayout.LayoutString")
        Me.GridEX1.DesignTimeLayout = GridEX1_DesignTimeLayout
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX1.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEX1.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.GridEX1.GroupByBoxVisible = False
        Me.GridEX1.Location = New System.Drawing.Point(0, 88)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.RecordNavigator = True
        Me.GridEX1.Size = New System.Drawing.Size(1057, 527)
        Me.GridEX1.TabIndex = 13
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'ContextMenuStrip2
        '
        Me.ContextMenuStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrintToolStripMenuItem, Me.ExportToolStripMenuItem})
        Me.ContextMenuStrip2.Name = "ContextMenuStrip2"
        Me.ContextMenuStrip2.Size = New System.Drawing.Size(108, 48)
        '
        'PrintToolStripMenuItem
        '
        Me.PrintToolStripMenuItem.Name = "PrintToolStripMenuItem"
        Me.PrintToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.PrintToolStripMenuItem.Text = "Print"
        '
        'ExportToolStripMenuItem
        '
        Me.ExportToolStripMenuItem.Name = "ExportToolStripMenuItem"
        Me.ExportToolStripMenuItem.Size = New System.Drawing.Size(107, 22)
        Me.ExportToolStripMenuItem.Text = "Export"
        '
        'btnAdd
        '
        Me.btnAdd.BackColor = System.Drawing.Color.Transparent
        Me.btnAdd.BackgroundImage = CType(resources.GetObject("btnAdd.BackgroundImage"), System.Drawing.Image)
        Me.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdd.Location = New System.Drawing.Point(203, 25)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(20, 20)
        Me.btnAdd.TabIndex = 14
        Me.btnAdd.UseVisualStyleBackColor = False
        '
        'frmDefUnitOfItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1057, 615)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.TreeView1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmDefUnitOfItem"
        Me.Text = "frmDefUnitOfItem"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnShowAll As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents ContextMenuStrip2 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents PrintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class

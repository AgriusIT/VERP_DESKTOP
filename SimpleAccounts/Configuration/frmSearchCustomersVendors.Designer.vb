<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSearchCustomersVendors
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSearchCustomersVendors))
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.rbtCustomers = New System.Windows.Forms.CheckBox()
        Me.rbtVendors = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.btnClose = New System.Windows.Forms.Button()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(16, 15)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(430, 36)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Search customers and vendors"
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(135, 23)
        Me.txtSearch.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(620, 26)
        Me.txtSearch.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.txtSearch, "Type a word to be searched")
        '
        'grd
        '
        Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.Location = New System.Drawing.Point(2, 175)
        Me.grd.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(1648, 738)
        Me.grd.TabIndex = 7
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'rbtCustomers
        '
        Me.rbtCustomers.AutoSize = True
        Me.rbtCustomers.ForeColor = System.Drawing.Color.Black
        Me.rbtCustomers.Location = New System.Drawing.Point(16, 63)
        Me.rbtCustomers.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtCustomers.Name = "rbtCustomers"
        Me.rbtCustomers.Size = New System.Drawing.Size(112, 24)
        Me.rbtCustomers.TabIndex = 5
        Me.rbtCustomers.Text = "Customers"
        Me.ToolTip1.SetToolTip(Me.rbtCustomers, "Check customers to search only customers.")
        Me.rbtCustomers.UseVisualStyleBackColor = True
        '
        'rbtVendors
        '
        Me.rbtVendors.AutoSize = True
        Me.rbtVendors.ForeColor = System.Drawing.Color.Black
        Me.rbtVendors.Location = New System.Drawing.Point(138, 63)
        Me.rbtVendors.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtVendors.Name = "rbtVendors"
        Me.rbtVendors.Size = New System.Drawing.Size(95, 24)
        Me.rbtVendors.TabIndex = 6
        Me.rbtVendors.Text = "Vendors"
        Me.ToolTip1.SetToolTip(Me.rbtVendors, "Check vendors to search only vendors.")
        Me.rbtVendors.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(16, 29)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(107, 20)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Search Here :"
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.btnClose.Location = New System.Drawing.Point(1574, 18)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(57, 46)
        Me.btnClose.TabIndex = 119
        Me.btnClose.TabStop = False
        Me.ToolTip1.SetToolTip(Me.btnClose, "Close")
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'pnlHeader
        '
        Me.pnlHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnClose)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Location = New System.Drawing.Point(2, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1648, 82)
        Me.pnlHeader.TabIndex = 8
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.txtSearch)
        Me.Panel2.Controls.Add(Me.rbtCustomers)
        Me.Panel2.Controls.Add(Me.rbtVendors)
        Me.Panel2.Location = New System.Drawing.Point(2, 82)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1648, 94)
        Me.Panel2.TabIndex = 9
        '
        'frmSearchCustomersVendors
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1650, 923)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.grd)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmSearchCustomersVendors"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Search Customers And Vendors"
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents rbtCustomers As System.Windows.Forms.CheckBox
    Friend WithEvents rbtVendors As System.Windows.Forms.CheckBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
End Class

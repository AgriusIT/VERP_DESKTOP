<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptList
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
        Dim grdFavorites_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim grdSales_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.grdFavorites = New Janus.Windows.GridEX.GridEX()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.grdSales = New Janus.Windows.GridEX.GridEX()
        Me.pnlHeader.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.grdFavorites, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.grdSales, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(813, 44)
        Me.pnlHeader.TabIndex = 2
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.lblHeader.Location = New System.Drawing.Point(28, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(134, 25)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Report list"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.Panel1.Controls.Add(Me.TextBox1)
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 44)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(813, 44)
        Me.Panel1.TabIndex = 22
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(683, 8)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(116, 20)
        Me.TextBox1.TabIndex = 21
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(571, 7)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(99, 23)
        Me.Button1.TabIndex = 20
        Me.Button1.Text = "Add New Report"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 88)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(813, 336)
        Me.TabControl1.TabIndex = 23
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.grdFavorites)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(805, 310)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Favorites"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'grdFavorites
        '
        Me.grdFavorites.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdFavorites.ColumnAutoResize = True
        grdFavorites_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grdFavorites.DesignTimeLayout = grdFavorites_DesignTimeLayout
        Me.grdFavorites.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdFavorites.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdFavorites.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdFavorites.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.grdFavorites.GroupByBoxVisible = False
        Me.grdFavorites.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdFavorites.Location = New System.Drawing.Point(3, 3)
        Me.grdFavorites.Name = "grdFavorites"
        Me.grdFavorites.RecordNavigator = True
        Me.grdFavorites.Size = New System.Drawing.Size(799, 304)
        Me.grdFavorites.TabIndex = 15
        Me.grdFavorites.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.grdSales)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(805, 311)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Sales"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'grdSales
        '
        Me.grdSales.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdSales.ColumnAutoResize = True
        grdSales_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grdSales.DesignTimeLayout = grdSales_DesignTimeLayout
        Me.grdSales.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSales.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdSales.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdSales.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.grdSales.GroupByBoxVisible = False
        Me.grdSales.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdSales.Location = New System.Drawing.Point(3, 3)
        Me.grdSales.Name = "grdSales"
        Me.grdSales.RecordNavigator = True
        Me.grdSales.Size = New System.Drawing.Size(799, 305)
        Me.grdSales.TabIndex = 15
        Me.grdSales.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'frmRptList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(813, 424)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlHeader)
        Me.Name = "frmRptList"
        Me.Text = "frmRptList"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.grdFavorites, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.grdSales, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents grdFavorites As Janus.Windows.GridEX.GridEX
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents grdSales As Janus.Windows.GridEX.GridEX
End Class

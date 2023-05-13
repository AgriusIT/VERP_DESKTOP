<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmVehicleLogList
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmVehicleLogList))
        Dim grdAdvanceType_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnExport = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.grdAdvanceType = New Janus.Windows.GridEX.GridEX()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.PrintToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Panel1.SuspendLayout()
        CType(Me.grdAdvanceType, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnExport)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btnAdd)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1008, 88)
        Me.Panel1.TabIndex = 17
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExport.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        ' Me.btnExport.Image = Global.SimpleAccounts.My.Resources.Resources.btn_settings_blue
        Me.btnExport.Location = New System.Drawing.Point(966, 25)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(30, 30)
        Me.btnExport.TabIndex = 17
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(12, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(126, 30)
        Me.Label1.TabIndex = 15
        Me.Label1.Text = "Vehicle Log"
        '
        'btnAdd
        '
        Me.btnAdd.BackColor = System.Drawing.Color.Transparent
        Me.btnAdd.BackgroundImage = CType(resources.GetObject("btnAdd.BackgroundImage"), System.Drawing.Image)
        Me.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdd.Location = New System.Drawing.Point(144, 35)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(20, 20)
        Me.btnAdd.TabIndex = 14
        Me.btnAdd.UseVisualStyleBackColor = False
        '
        'grdAdvanceType
        '
        Me.grdAdvanceType.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdAdvanceType.BackColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.grdAdvanceType.BlendColor = System.Drawing.Color.White
        Me.grdAdvanceType.BorderStyle = Janus.Windows.GridEX.BorderStyle.Flat
        Me.grdAdvanceType.ControlStyle.ButtonAppearance = Janus.Windows.GridEX.ButtonAppearance.Flat
        Me.grdAdvanceType.ControlStyle.ControlColor = System.Drawing.Color.White
        Me.grdAdvanceType.ControlStyle.ControlTextColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdAdvanceType.ControlStyle.WindowColor = System.Drawing.Color.White
        Me.grdAdvanceType.DefaultBackColorAlphaMode = Janus.Windows.GridEX.AlphaMode.Transparent
        grdAdvanceType_DesignTimeLayout.LayoutString = resources.GetString("grdAdvanceType_DesignTimeLayout.LayoutString")
        Me.grdAdvanceType.DesignTimeLayout = grdAdvanceType_DesignTimeLayout
        Me.grdAdvanceType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdAdvanceType.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdAdvanceType.FlatBorderColor = System.Drawing.Color.White
        Me.grdAdvanceType.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.grdAdvanceType.GridLineColor = System.Drawing.Color.White
        Me.grdAdvanceType.GridLines = Janus.Windows.GridEX.GridLines.Horizontal
        Me.grdAdvanceType.GridLineStyle = Janus.Windows.GridEX.GridLineStyle.Solid
        Me.grdAdvanceType.GroupByBoxVisible = False
        Me.grdAdvanceType.HeaderFormatStyle.Alpha = 1
        Me.grdAdvanceType.HeaderFormatStyle.Appearance = Janus.Windows.GridEX.Appearance.Empty
        Me.grdAdvanceType.HeaderFormatStyle.BackColor = System.Drawing.Color.White
        Me.grdAdvanceType.HeaderFormatStyle.Font = New System.Drawing.Font("Segoe UI", 9.75!)
        Me.grdAdvanceType.HeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.[False]
        Me.grdAdvanceType.HeaderFormatStyle.FontSize = 10.0!
        Me.grdAdvanceType.HeaderFormatStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdAdvanceType.HeaderFormatStyle.ImageHorizontalAlignment = Janus.Windows.GridEX.ImageHorizontalAlignment.Center
        Me.grdAdvanceType.HeaderFormatStyle.LineAlignment = Janus.Windows.GridEX.TextAlignment.Center
        Me.grdAdvanceType.HeaderFormatStyle.TextAlignment = Janus.Windows.GridEX.TextAlignment.Near
        Me.grdAdvanceType.Location = New System.Drawing.Point(0, 88)
        Me.grdAdvanceType.Name = "grdAdvanceType"
        Me.grdAdvanceType.Office2007CustomColor = System.Drawing.Color.FromArgb(CType(CType(241, Byte), Integer), CType(CType(245, Byte), Integer), CType(CType(248, Byte), Integer))
        Me.grdAdvanceType.PreviewRowFormatStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdAdvanceType.RecordNavigator = True
        Me.grdAdvanceType.RowFormatStyle.BackColor = System.Drawing.Color.Black
        Me.grdAdvanceType.RowFormatStyle.BackColorGradient = System.Drawing.Color.White
        Me.grdAdvanceType.RowFormatStyle.BackgroundGradientMode = Janus.Windows.GridEX.BackgroundGradientMode.Solid
        Me.grdAdvanceType.RowFormatStyle.ForeColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.grdAdvanceType.Size = New System.Drawing.Size(1008, 641)
        Me.grdAdvanceType.TabIndex = 19
        Me.grdAdvanceType.ThemedAreas = Janus.Windows.GridEX.ThemedArea.None
        Me.grdAdvanceType.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grdAdvanceType.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grdAdvanceType.TreeLineColor = System.Drawing.SystemColors.Control
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.PrintToolStripMenuItem, Me.ExportToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(108, 48)
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
        'frmVehicleLogList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Controls.Add(Me.grdAdvanceType)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmVehicleLogList"
        Me.Text = "frmVehicleLogList"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.grdAdvanceType, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents grdAdvanceType As Janus.Windows.GridEX.GridEX
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents PrintToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class

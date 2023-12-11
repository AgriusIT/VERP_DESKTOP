<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptGrdAdvances
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRptGrdAdvances))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.rdoVendorAdvances = New System.Windows.Forms.RadioButton()
        Me.rdoCustomerAdvances = New System.Windows.Forms.RadioButton()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlHeader = New System.Windows.Forms.Panel()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(924, 38)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'rdoVendorAdvances
        '
        Me.rdoVendorAdvances.AutoSize = True
        Me.rdoVendorAdvances.Checked = True
        Me.rdoVendorAdvances.Location = New System.Drawing.Point(18, 115)
        Me.rdoVendorAdvances.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rdoVendorAdvances.Name = "rdoVendorAdvances"
        Me.rdoVendorAdvances.Size = New System.Drawing.Size(160, 24)
        Me.rdoVendorAdvances.TabIndex = 2
        Me.rdoVendorAdvances.TabStop = True
        Me.rdoVendorAdvances.Text = "Vendor Advances"
        Me.ToolTip1.SetToolTip(Me.rdoVendorAdvances, "View Vendor Advances")
        Me.rdoVendorAdvances.UseVisualStyleBackColor = True
        '
        'rdoCustomerAdvances
        '
        Me.rdoCustomerAdvances.AutoSize = True
        Me.rdoCustomerAdvances.Location = New System.Drawing.Point(210, 115)
        Me.rdoCustomerAdvances.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rdoCustomerAdvances.Name = "rdoCustomerAdvances"
        Me.rdoCustomerAdvances.Size = New System.Drawing.Size(177, 24)
        Me.rdoCustomerAdvances.TabIndex = 3
        Me.rdoCustomerAdvances.Text = "Customer Advances"
        Me.ToolTip1.SetToolTip(Me.rdoCustomerAdvances, "View Customer Advances")
        Me.rdoCustomerAdvances.UseVisualStyleBackColor = True
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(32, 15)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(280, 41)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Vendor Advances"
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
        Me.grd.GroupByBoxVisible = False
        Me.grd.Location = New System.Drawing.Point(3, 158)
        Me.grd.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(918, 348)
        Me.grd.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.grd, "Advances Detail")
        Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(867, 2)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar1.MyGrid = Me.grd
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(57, 37)
        Me.CtrlGrdBar1.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.CtrlGrdBar1, "Settings")
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 38)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(924, 65)
        Me.pnlHeader.TabIndex = 83
        '
        'frmRptGrdAdvances
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(924, 508)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.rdoCustomerAdvances)
        Me.Controls.Add(Me.rdoVendorAdvances)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmRptGrdAdvances"
        Me.Text = "Advances"
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents rdoVendorAdvances As System.Windows.Forms.RadioButton
    Friend WithEvents rdoCustomerAdvances As System.Windows.Forms.RadioButton
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

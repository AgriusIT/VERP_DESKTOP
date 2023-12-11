<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGrdRptAgingInstallment
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
        Dim grd_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGrdRptAgingInstallment))
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cmbFormate = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ToolStrip2 = New System.Windows.Forms.ToolStrip()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.btnAddLayout = New System.Windows.Forms.ToolStripButton()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip2.SuspendLayout()
        Me.SuspendLayout()
        '
        'grd
        '
        Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grd_DesignTimeLayout.LayoutString = resources.GetString("grd_DesignTimeLayout.LayoutString")
        Me.grd.DesignTimeLayout = grd_DesignTimeLayout
        Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grd.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
        Me.grd.Location = New System.Drawing.Point(0, 63)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(813, 605)
        Me.grd.TabIndex = 2
        Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Navy
        Me.Label1.Location = New System.Drawing.Point(8, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(206, 23)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Aging Installment"
        '
        'cmbFormate
        '
        Me.cmbFormate.FormattingEnabled = True
        Me.cmbFormate.Location = New System.Drawing.Point(274, 36)
        Me.cmbFormate.Name = "cmbFormate"
        Me.cmbFormate.Size = New System.Drawing.Size(201, 21)
        Me.cmbFormate.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(223, 39)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(39, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Layout"
        '
        'ToolStrip2
        '
        Me.ToolStrip2.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh, Me.btnPrint, Me.btnAddLayout})
        Me.ToolStrip2.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip2.Name = "ToolStrip2"
        Me.ToolStrip2.Size = New System.Drawing.Size(813, 25)
        Me.ToolStrip2.TabIndex = 15
        Me.ToolStrip2.Text = "ToolStrip2"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(66, 22)
        Me.btnRefresh.Text = "Refresh"
        '
        'btnPrint
        '
        Me.btnPrint.Image = Global.SimpleAccounts.My.Resources.Resources.print
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(52, 22)
        Me.btnPrint.Text = "Print"
        '
        'btnAddLayout
        '
        Me.btnAddLayout.Image = Global.SimpleAccounts.My.Resources.Resources.addIcon
        Me.btnAddLayout.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnAddLayout.Name = "btnAddLayout"
        Me.btnAddLayout.Size = New System.Drawing.Size(118, 22)
        Me.btnAddLayout.Text = "Add aging layout"
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Nothing
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(775, 0)
        Me.CtrlGrdBar1.MyGrid = Me.grd
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(38, 25)
        Me.CtrlGrdBar1.TabIndex = 16
        '
        'frmGrdRptAgingInstallment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(813, 668)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.ToolStrip2)
        Me.Controls.Add(Me.cmbFormate)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.grd)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmGrdRptAgingInstallment"
        Me.Text = "Aging Installment Report"
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip2.ResumeLayout(False)
        Me.ToolStrip2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cmbFormate As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ToolStrip2 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnAddLayout As System.Windows.Forms.ToolStripButton
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
End Class

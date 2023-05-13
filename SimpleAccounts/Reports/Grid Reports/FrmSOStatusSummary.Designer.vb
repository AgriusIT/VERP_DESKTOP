<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSOStatusSummary
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSOStatusSummary))
        Me.GrdSOSummary = New Janus.Windows.GridEX.GridEX
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.GrdSOSummary, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GrdSOSummary
        '
        Me.GrdSOSummary.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GrdSOSummary.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GrdSOSummary.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GrdSOSummary.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GrdSOSummary.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GrdSOSummary.Location = New System.Drawing.Point(0, 0)
        Me.GrdSOSummary.Name = "GrdSOSummary"
        Me.GrdSOSummary.RecordNavigator = True
        Me.GrdSOSummary.Size = New System.Drawing.Size(626, 402)
        Me.GrdSOSummary.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.GrdSOSummary, "Sales Order Status Summary")
        Me.GrdSOSummary.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'FrmSOStatusSummary
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.ClientSize = New System.Drawing.Size(626, 402)
        Me.Controls.Add(Me.GrdSOSummary)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmSOStatusSummary"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "SO Status Summary"
        CType(Me.GrdSOSummary, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GrdSOSummary As Janus.Windows.GridEX.GridEX
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

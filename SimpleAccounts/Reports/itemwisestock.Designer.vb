<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class itemwisestock
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(itemwisestock))
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridEX1
        '
        Me.GridEX1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX1.Location = New System.Drawing.Point(0, 0)
        Me.GridEX1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.Size = New System.Drawing.Size(634, 671)
        Me.GridEX1.TabIndex = 0
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'itemwisestock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(634, 671)
        Me.Controls.Add(Me.GridEX1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "itemwisestock"
        Me.Text = "itemwisestock"
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
End Class

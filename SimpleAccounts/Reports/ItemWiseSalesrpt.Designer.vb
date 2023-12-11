<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ItemWiseSalesrpt
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ItemWiseSalesrpt))
        Me.GridEX2 = New Janus.Windows.GridEX.GridEX()
        CType(Me.GridEX2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GridEX2
        '
        Me.GridEX2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GridEX2.Location = New System.Drawing.Point(0, 0)
        Me.GridEX2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GridEX2.Name = "GridEX2"
        Me.GridEX2.Size = New System.Drawing.Size(597, 606)
        Me.GridEX2.TabIndex = 0
        Me.GridEX2.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'ItemWiseSalesrpt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(597, 606)
        Me.Controls.Add(Me.GridEX2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "ItemWiseSalesrpt"
        Me.Text = "ItemWiseSalesrpt"
        CType(Me.GridEX2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents GridEX2 As Janus.Windows.GridEX.GridEX
End Class

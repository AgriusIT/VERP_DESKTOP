<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMyCompany
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMyCompany))
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel
        Me.SuspendLayout()
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Padding = New System.Windows.Forms.Padding(10)
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(669, 397)
        Me.FlowLayoutPanel1.TabIndex = 0
        '
        'frmMyCompany
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(226, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(669, 397)
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMyCompany"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "My Company"
        Me.ResumeLayout(False)

    End Sub
    Private WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
End Class

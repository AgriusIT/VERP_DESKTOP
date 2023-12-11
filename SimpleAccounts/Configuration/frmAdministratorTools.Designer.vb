<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAdministratorTools
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAdministratorTools))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.UiButton4 = New Janus.Windows.EditControls.UIButton()
        Me.UiButton2 = New Janus.Windows.EditControls.UIButton()
        Me.UiButton1 = New Janus.Windows.EditControls.UIButton()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.UiButton4)
        Me.GroupBox1.Controls.Add(Me.UiButton2)
        Me.GroupBox1.Controls.Add(Me.UiButton1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 46)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(598, 278)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Utility"
        '
        'UiButton4
        '
        Me.UiButton4.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.UiButton4.Image = Global.SimpleAccounts.My.Resources.Resources.download_database_48
        Me.UiButton4.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.UiButton4.ImageSize = New System.Drawing.Size(32, 32)
        Me.UiButton4.Location = New System.Drawing.Point(68, 185)
        Me.UiButton4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UiButton4.Name = "UiButton4"
        Me.UiButton4.Size = New System.Drawing.Size(470, 62)
        Me.UiButton4.TabIndex = 2
        Me.UiButton4.Text = "Restore Backup"
        Me.ToolTip1.SetToolTip(Me.UiButton4, "Restore Database")
        Me.UiButton4.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'UiButton2
        '
        Me.UiButton2.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.UiButton2.Image = Global.SimpleAccounts.My.Resources.Resources.icon_profiles_create_db
        Me.UiButton2.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.UiButton2.ImageSize = New System.Drawing.Size(32, 32)
        Me.UiButton2.Location = New System.Drawing.Point(68, 114)
        Me.UiButton2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UiButton2.Name = "UiButton2"
        Me.UiButton2.Size = New System.Drawing.Size(470, 62)
        Me.UiButton2.TabIndex = 1
        Me.UiButton2.Text = "Create New Company"
        Me.ToolTip1.SetToolTip(Me.UiButton2, "Create New Company")
        Me.UiButton2.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'UiButton1
        '
        Me.UiButton1.Image = Global.SimpleAccounts.My.Resources.Resources._3687481
        Me.UiButton1.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.UiButton1.ImageSize = New System.Drawing.Size(32, 32)
        Me.UiButton1.Location = New System.Drawing.Point(68, 43)
        Me.UiButton1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UiButton1.Name = "UiButton1"
        Me.UiButton1.Size = New System.Drawing.Size(470, 62)
        Me.UiButton1.TabIndex = 0
        Me.UiButton1.Text = "Connect To Server"
        Me.ToolTip1.SetToolTip(Me.UiButton1, "Define Company & Connection Information")
        Me.UiButton1.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'frmAdministratorTools
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(628, 365)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAdministratorTools"
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Utility"
        Me.ToolTip1.SetToolTip(Me, "Utility")
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents UiButton1 As Janus.Windows.EditControls.UIButton
    Friend WithEvents UiButton2 As Janus.Windows.EditControls.UIButton
    Friend WithEvents UiButton4 As Janus.Windows.EditControls.UIButton
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

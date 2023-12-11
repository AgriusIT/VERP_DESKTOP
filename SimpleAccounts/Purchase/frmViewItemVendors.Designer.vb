<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmViewItemVendors
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
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lstVendors = New SimpleAccounts.uiListControl()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.Panel4.Controls.Add(Me.Label1)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 0)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(580, 86)
        Me.Panel4.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Times New Roman", 21.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(8, 14)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(247, 48)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Vendors List"
        '
        'lstVendors
        '
        Me.lstVendors.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstVendors.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstVendors.BackColor = System.Drawing.Color.Transparent
        Me.lstVendors.disableWhenChecked = False
        Me.lstVendors.HeadingLabelName = "lstEmpDepartment"
        Me.lstVendors.HeadingText = ""
        Me.lstVendors.Location = New System.Drawing.Point(22, 118)
        Me.lstVendors.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstVendors.Name = "lstVendors"
        Me.lstVendors.ShowAddNewButton = False
        Me.lstVendors.ShowInverse = True
        Me.lstVendors.ShowMagnifierButton = False
        Me.lstVendors.ShowNoCheck = False
        Me.lstVendors.ShowResetAllButton = False
        Me.lstVendors.ShowSelectall = True
        Me.lstVendors.Size = New System.Drawing.Size(540, 274)
        Me.lstVendors.TabIndex = 8
        Me.lstVendors.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(18, 109)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(88, 28)
        Me.Label6.TabIndex = 33
        Me.Label6.Text = "Vendors"
        '
        'frmViewItemVendors
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(580, 454)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.lstVendors)
        Me.Controls.Add(Me.Panel4)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmViewItemVendors"
        Me.Text = "Vendors List"
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lstVendors As SimpleAccounts.uiListControl
    Friend WithEvents Label6 As System.Windows.Forms.Label
End Class

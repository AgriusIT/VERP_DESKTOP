<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfigColor
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtColors = New System.Windows.Forms.TextBox()
        Me.btnMove = New System.Windows.Forms.Button()
        Me.lbUiSelectedColors = New SimpleAccounts.uiListControl()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtSelectedColors = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.UiListColor = New SimpleAccounts.uiListControl()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(14, 79)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 25)
        Me.Label1.TabIndex = 43
        Me.Label1.Text = "Color"
        '
        'txtColors
        '
        Me.txtColors.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtColors.Location = New System.Drawing.Point(12, 301)
        Me.txtColors.Name = "txtColors"
        Me.txtColors.Size = New System.Drawing.Size(261, 29)
        Me.txtColors.TabIndex = 44
        Me.ToolTip1.SetToolTip(Me.txtColors, "Search Colors")
        '
        'btnMove
        '
        Me.btnMove.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnMove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMove.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMove.Location = New System.Drawing.Point(318, 167)
        Me.btnMove.Name = "btnMove"
        Me.btnMove.Size = New System.Drawing.Size(53, 36)
        Me.btnMove.TabIndex = 47
        Me.btnMove.Text = " >"
        Me.ToolTip1.SetToolTip(Me.btnMove, "Move To Selected")
        Me.btnMove.UseVisualStyleBackColor = False
        '
        'lbUiSelectedColors
        '
        Me.lbUiSelectedColors.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lbUiSelectedColors.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lbUiSelectedColors.BackColor = System.Drawing.Color.Transparent
        Me.lbUiSelectedColors.disableWhenChecked = False
        Me.lbUiSelectedColors.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lbUiSelectedColors.HeadingLabelName = "lstEmpDepartment"
        Me.lbUiSelectedColors.HeadingText = ""
        Me.lbUiSelectedColors.Location = New System.Drawing.Point(394, 101)
        Me.lbUiSelectedColors.Margin = New System.Windows.Forms.Padding(6)
        Me.lbUiSelectedColors.Name = "lbUiSelectedColors"
        Me.lbUiSelectedColors.ShowAddNewButton = False
        Me.lbUiSelectedColors.ShowInverse = True
        Me.lbUiSelectedColors.ShowMagnifierButton = False
        Me.lbUiSelectedColors.ShowNoCheck = False
        Me.lbUiSelectedColors.ShowResetAllButton = False
        Me.lbUiSelectedColors.ShowSelectall = True
        Me.lbUiSelectedColors.Size = New System.Drawing.Size(288, 177)
        Me.lbUiSelectedColors.TabIndex = 48
        Me.ToolTip1.SetToolTip(Me.lbUiSelectedColors, "Selected Colors")
        Me.lbUiSelectedColors.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(389, 79)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(143, 25)
        Me.Label3.TabIndex = 45
        Me.Label3.Text = "Selected Colors"
        '
        'txtSelectedColors
        '
        Me.txtSelectedColors.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSelectedColors.Location = New System.Drawing.Point(394, 301)
        Me.txtSelectedColors.Name = "txtSelectedColors"
        Me.txtSelectedColors.Size = New System.Drawing.Size(258, 29)
        Me.txtSelectedColors.TabIndex = 46
        Me.ToolTip1.SetToolTip(Me.txtSelectedColors, "Search Non Selected Colors")
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(91, Byte), Integer), CType(CType(174, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(867, 61)
        Me.Panel2.TabIndex = 49
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(12, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(200, 37)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Inventory Color"
        '
        'UiListColor
        '
        Me.UiListColor.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.UiListColor.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.UiListColor.BackColor = System.Drawing.Color.Transparent
        Me.UiListColor.disableWhenChecked = False
        Me.UiListColor.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.UiListColor.HeadingLabelName = "lstEmpDepartment"
        Me.UiListColor.HeadingText = ""
        Me.UiListColor.Location = New System.Drawing.Point(12, 101)
        Me.UiListColor.Margin = New System.Windows.Forms.Padding(6)
        Me.UiListColor.Name = "UiListColor"
        Me.UiListColor.ShowAddNewButton = False
        Me.UiListColor.ShowInverse = True
        Me.UiListColor.ShowMagnifierButton = False
        Me.UiListColor.ShowNoCheck = False
        Me.UiListColor.ShowResetAllButton = False
        Me.UiListColor.ShowSelectall = True
        Me.UiListColor.Size = New System.Drawing.Size(288, 177)
        Me.UiListColor.TabIndex = 50
        Me.ToolTip1.SetToolTip(Me.UiListColor, "Colors")
        Me.UiListColor.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBack.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBack.Location = New System.Drawing.Point(318, 209)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(53, 36)
        Me.btnBack.TabIndex = 51
        Me.btnBack.Text = "< "
        Me.ToolTip1.SetToolTip(Me.btnBack, "Move Back to Unselected")
        Me.btnBack.UseVisualStyleBackColor = False
        '
        'frmConfigColor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(867, 371)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.UiListColor)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtColors)
        Me.Controls.Add(Me.btnMove)
        Me.Controls.Add(Me.lbUiSelectedColors)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtSelectedColors)
        Me.Name = "frmConfigColor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtColors As System.Windows.Forms.TextBox
    Friend WithEvents btnMove As System.Windows.Forms.Button
    Friend WithEvents lbUiSelectedColors As SimpleAccounts.uiListControl
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtSelectedColors As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents UiListColor As SimpleAccounts.uiListControl
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

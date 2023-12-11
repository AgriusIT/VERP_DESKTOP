<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfigSizes
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
        Me.btnBack = New System.Windows.Forms.Button()
        Me.UiListSizes = New SimpleAccounts.uiListControl()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtSizes = New System.Windows.Forms.TextBox()
        Me.btnMove = New System.Windows.Forms.Button()
        Me.lbUiSelectedSizes = New SimpleAccounts.uiListControl()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtSelectedSizes = New System.Windows.Forms.TextBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBack.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBack.Location = New System.Drawing.Point(309, 198)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(53, 36)
        Me.btnBack.TabIndex = 60
        Me.btnBack.Text = "< "
        Me.ToolTip1.SetToolTip(Me.btnBack, "Move Back to Sizes")
        Me.btnBack.UseVisualStyleBackColor = False
        '
        'UiListSizes
        '
        Me.UiListSizes.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.UiListSizes.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.UiListSizes.BackColor = System.Drawing.Color.Transparent
        Me.UiListSizes.disableWhenChecked = False
        Me.UiListSizes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.UiListSizes.HeadingLabelName = "lstEmpDepartment"
        Me.UiListSizes.HeadingText = ""
        Me.UiListSizes.Location = New System.Drawing.Point(12, 86)
        Me.UiListSizes.Margin = New System.Windows.Forms.Padding(6)
        Me.UiListSizes.Name = "UiListSizes"
        Me.UiListSizes.ShowAddNewButton = False
        Me.UiListSizes.ShowInverse = True
        Me.UiListSizes.ShowMagnifierButton = False
        Me.UiListSizes.ShowNoCheck = False
        Me.UiListSizes.ShowResetAllButton = False
        Me.UiListSizes.ShowSelectall = True
        Me.UiListSizes.Size = New System.Drawing.Size(288, 177)
        Me.UiListSizes.TabIndex = 59
        Me.ToolTip1.SetToolTip(Me.UiListSizes, "Colors")
        Me.UiListSizes.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(91, Byte), Integer), CType(CType(174, Byte), Integer))
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(735, 61)
        Me.Panel2.TabIndex = 58
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 20.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(12, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(193, 37)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Inventory Sizes"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(14, 65)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 25)
        Me.Label1.TabIndex = 52
        Me.Label1.Text = "Sizes"
        '
        'txtSizes
        '
        Me.txtSizes.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSizes.Location = New System.Drawing.Point(12, 286)
        Me.txtSizes.Name = "txtSizes"
        Me.txtSizes.Size = New System.Drawing.Size(263, 29)
        Me.txtSizes.TabIndex = 53
        Me.ToolTip1.SetToolTip(Me.txtSizes, "Search Sizes")
        '
        'btnMove
        '
        Me.btnMove.BackColor = System.Drawing.Color.WhiteSmoke
        Me.btnMove.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnMove.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnMove.Location = New System.Drawing.Point(309, 156)
        Me.btnMove.Name = "btnMove"
        Me.btnMove.Size = New System.Drawing.Size(53, 36)
        Me.btnMove.TabIndex = 56
        Me.btnMove.Text = " >"
        Me.ToolTip1.SetToolTip(Me.btnMove, "Move To Selected Sizes" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        Me.btnMove.UseVisualStyleBackColor = False
        '
        'lbUiSelectedSizes
        '
        Me.lbUiSelectedSizes.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lbUiSelectedSizes.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lbUiSelectedSizes.BackColor = System.Drawing.Color.Transparent
        Me.lbUiSelectedSizes.disableWhenChecked = False
        Me.lbUiSelectedSizes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lbUiSelectedSizes.HeadingLabelName = "lstEmpDepartment"
        Me.lbUiSelectedSizes.HeadingText = ""
        Me.lbUiSelectedSizes.Location = New System.Drawing.Point(394, 86)
        Me.lbUiSelectedSizes.Margin = New System.Windows.Forms.Padding(6)
        Me.lbUiSelectedSizes.Name = "lbUiSelectedSizes"
        Me.lbUiSelectedSizes.ShowAddNewButton = False
        Me.lbUiSelectedSizes.ShowInverse = True
        Me.lbUiSelectedSizes.ShowMagnifierButton = False
        Me.lbUiSelectedSizes.ShowNoCheck = False
        Me.lbUiSelectedSizes.ShowResetAllButton = False
        Me.lbUiSelectedSizes.ShowSelectall = True
        Me.lbUiSelectedSizes.Size = New System.Drawing.Size(288, 177)
        Me.lbUiSelectedSizes.TabIndex = 57
        Me.ToolTip1.SetToolTip(Me.lbUiSelectedSizes, "Selected Colors")
        Me.lbUiSelectedSizes.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI Semibold", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(389, 65)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(131, 25)
        Me.Label3.TabIndex = 54
        Me.Label3.Text = "Selected Sizes" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'txtSelectedSizes
        '
        Me.txtSelectedSizes.BackColor = System.Drawing.SystemColors.Window
        Me.txtSelectedSizes.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSelectedSizes.Location = New System.Drawing.Point(394, 286)
        Me.txtSelectedSizes.Name = "txtSelectedSizes"
        Me.txtSelectedSizes.Size = New System.Drawing.Size(264, 29)
        Me.txtSelectedSizes.TabIndex = 55
        Me.ToolTip1.SetToolTip(Me.txtSelectedSizes, "Search Selected Sizes" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10))
        '
        'frmConfigSizes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(735, 360)
        Me.Controls.Add(Me.btnBack)
        Me.Controls.Add(Me.UiListSizes)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtSizes)
        Me.Controls.Add(Me.btnMove)
        Me.Controls.Add(Me.lbUiSelectedSizes)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtSelectedSizes)
        Me.Name = "frmConfigSizes"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnBack As System.Windows.Forms.Button
    Friend WithEvents UiListSizes As SimpleAccounts.uiListControl
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtSizes As System.Windows.Forms.TextBox
    Friend WithEvents btnMove As System.Windows.Forms.Button
    Friend WithEvents lbUiSelectedSizes As SimpleAccounts.uiListControl
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtSelectedSizes As System.Windows.Forms.TextBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

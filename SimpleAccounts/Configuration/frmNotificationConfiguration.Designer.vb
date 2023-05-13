<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNotificationConfiguration
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
        Me.lstCategories = New System.Windows.Forms.ListBox()
        Me.lstEvents = New System.Windows.Forms.ListBox()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnPrevious = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.btnFinish = New System.Windows.Forms.Button()
        Me.txtDescription = New System.Windows.Forms.TextBox()
        Me.lblNotification = New System.Windows.Forms.Label()
        Me.lstRoles = New SimpleAccounts.uiListControl()
        Me.lstUsers = New SimpleAccounts.uiListControl()
        Me.lstUserGroups = New SimpleAccounts.uiListControl()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lstCategories
        '
        Me.lstCategories.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lstCategories.FormattingEnabled = True
        Me.lstCategories.Location = New System.Drawing.Point(6, 36)
        Me.lstCategories.Name = "lstCategories"
        Me.lstCategories.Size = New System.Drawing.Size(246, 368)
        Me.lstCategories.TabIndex = 0
        '
        'lstEvents
        '
        Me.lstEvents.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lstEvents.FormattingEnabled = True
        Me.lstEvents.Location = New System.Drawing.Point(258, 36)
        Me.lstEvents.Name = "lstEvents"
        Me.lstEvents.Size = New System.Drawing.Size(246, 368)
        Me.lstEvents.TabIndex = 0
        '
        'btnNext
        '
        Me.btnNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNext.Location = New System.Drawing.Point(705, 411)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(75, 23)
        Me.btnNext.TabIndex = 1
        Me.btnNext.Text = "Next"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(790, 433)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnPrevious
        '
        Me.btnPrevious.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrevious.Location = New System.Drawing.Point(624, 411)
        Me.btnPrevious.Name = "btnPrevious"
        Me.btnPrevious.Size = New System.Drawing.Size(75, 23)
        Me.btnPrevious.TabIndex = 1
        Me.btnPrevious.Text = "Previous"
        Me.btnPrevious.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(877, 468)
        Me.TabControl1.TabIndex = 2
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.lstEvents)
        Me.TabPage1.Controls.Add(Me.btnNext)
        Me.TabPage1.Controls.Add(Me.lstCategories)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(869, 442)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Notification"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(255, 15)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(40, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Event"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(9, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(126, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Notification Category"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.btnPrevious)
        Me.TabPage2.Controls.Add(Me.btnFinish)
        Me.TabPage2.Controls.Add(Me.txtDescription)
        Me.TabPage2.Controls.Add(Me.lstRoles)
        Me.TabPage2.Controls.Add(Me.lstUsers)
        Me.TabPage2.Controls.Add(Me.lstUserGroups)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(869, 442)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Delivery"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'btnFinish
        '
        Me.btnFinish.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnFinish.Location = New System.Drawing.Point(705, 411)
        Me.btnFinish.Name = "btnFinish"
        Me.btnFinish.Size = New System.Drawing.Size(75, 23)
        Me.btnFinish.TabIndex = 8
        Me.btnFinish.Text = "Finish"
        Me.btnFinish.UseVisualStyleBackColor = True
        '
        'txtDescription
        '
        Me.txtDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDescription.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold)
        Me.txtDescription.ForeColor = System.Drawing.Color.Navy
        Me.txtDescription.Location = New System.Drawing.Point(6, 19)
        Me.txtDescription.Name = "txtDescription"
        Me.txtDescription.Size = New System.Drawing.Size(857, 31)
        Me.txtDescription.TabIndex = 6
        Me.txtDescription.Text = "Notification Description"
        '
        'lblNotification
        '
        Me.lblNotification.AutoSize = True
        Me.lblNotification.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblNotification.ForeColor = System.Drawing.Color.Navy
        Me.lblNotification.Location = New System.Drawing.Point(12, 27)
        Me.lblNotification.Name = "lblNotification"
        Me.lblNotification.Size = New System.Drawing.Size(268, 23)
        Me.lblNotification.TabIndex = 3
        Me.lblNotification.Text = "No notification selected"
        '
        'lstRoles
        '
        Me.lstRoles.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstRoles.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lstRoles.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstRoles.BackColor = System.Drawing.Color.Transparent
        Me.lstRoles.disableWhenChecked = False
        Me.lstRoles.HeadingLabelName = Nothing
        Me.lstRoles.HeadingText = "Roles"
        Me.lstRoles.Location = New System.Drawing.Point(6, 56)
        Me.lstRoles.Name = "lstRoles"
        Me.lstRoles.ShowAddNewButton = False
        Me.lstRoles.ShowInverse = True
        Me.lstRoles.ShowMagnifierButton = False
        Me.lstRoles.ShowNoCheck = False
        Me.lstRoles.ShowResetAllButton = False
        Me.lstRoles.ShowSelectall = True
        Me.lstRoles.Size = New System.Drawing.Size(272, 349)
        Me.lstRoles.TabIndex = 5
        Me.lstRoles.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstUsers
        '
        Me.lstUsers.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstUsers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lstUsers.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstUsers.BackColor = System.Drawing.Color.Transparent
        Me.lstUsers.disableWhenChecked = False
        Me.lstUsers.HeadingLabelName = Nothing
        Me.lstUsers.HeadingText = "Users"
        Me.lstUsers.Location = New System.Drawing.Point(562, 56)
        Me.lstUsers.Name = "lstUsers"
        Me.lstUsers.ShowAddNewButton = False
        Me.lstUsers.ShowInverse = True
        Me.lstUsers.ShowMagnifierButton = False
        Me.lstUsers.ShowNoCheck = False
        Me.lstUsers.ShowResetAllButton = False
        Me.lstUsers.ShowSelectall = True
        Me.lstUsers.Size = New System.Drawing.Size(272, 349)
        Me.lstUsers.TabIndex = 4
        Me.lstUsers.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstUserGroups
        '
        Me.lstUserGroups.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstUserGroups.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lstUserGroups.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstUserGroups.BackColor = System.Drawing.Color.Transparent
        Me.lstUserGroups.disableWhenChecked = False
        Me.lstUserGroups.HeadingLabelName = Nothing
        Me.lstUserGroups.HeadingText = "User Groups"
        Me.lstUserGroups.Location = New System.Drawing.Point(284, 56)
        Me.lstUserGroups.Name = "lstUserGroups"
        Me.lstUserGroups.ShowAddNewButton = False
        Me.lstUserGroups.ShowInverse = True
        Me.lstUserGroups.ShowMagnifierButton = False
        Me.lstUserGroups.ShowNoCheck = False
        Me.lstUserGroups.ShowResetAllButton = False
        Me.lstUserGroups.ShowSelectall = True
        Me.lstUserGroups.Size = New System.Drawing.Size(272, 349)
        Me.lstUserGroups.TabIndex = 3
        Me.lstUserGroups.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'frmNotificationConfiguration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(877, 468)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.lblNotification)
        Me.Name = "frmNotificationConfiguration"
        Me.Text = "Notification Configuration"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lstCategories As System.Windows.Forms.ListBox
    Friend WithEvents lstEvents As System.Windows.Forms.ListBox
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnPrevious As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents lstRoles As SimpleAccounts.uiListControl
    Friend WithEvents lstUsers As SimpleAccounts.uiListControl
    Friend WithEvents lstUserGroups As SimpleAccounts.uiListControl
    Friend WithEvents lblNotification As System.Windows.Forms.Label
    Friend WithEvents txtDescription As System.Windows.Forms.TextBox
    Friend WithEvents btnFinish As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class

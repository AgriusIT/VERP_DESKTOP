<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSalesInquiryUserSelection
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.lblInquiryNo = New System.Windows.Forms.Label()
        Me.lblCustomerName = New System.Windows.Forms.Label()
        Me.lstUsers = New SimpleAccounts.uiListControl()
        Me.lstUserGroups = New SimpleAccounts.uiListControl()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(621, 571)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(219, 45)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(4, 5)
        Me.OK_Button.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(100, 35)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "OK"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(114, 5)
        Me.Cancel_Button.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(100, 35)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'lblInquiryNo
        '
        Me.lblInquiryNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblInquiryNo.ForeColor = System.Drawing.Color.Black
        Me.lblInquiryNo.Location = New System.Drawing.Point(18, 14)
        Me.lblInquiryNo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblInquiryNo.Name = "lblInquiryNo"
        Me.lblInquiryNo.Size = New System.Drawing.Size(237, 45)
        Me.lblInquiryNo.TabIndex = 26
        Me.lblInquiryNo.Text = "Inquiry No"
        Me.lblInquiryNo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCustomerName
        '
        Me.lblCustomerName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCustomerName.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCustomerName.ForeColor = System.Drawing.Color.Black
        Me.lblCustomerName.Location = New System.Drawing.Point(264, 14)
        Me.lblCustomerName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCustomerName.Name = "lblCustomerName"
        Me.lblCustomerName.Size = New System.Drawing.Size(576, 45)
        Me.lblCustomerName.TabIndex = 25
        Me.lblCustomerName.Text = "Vendor"
        Me.lblCustomerName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.lstUsers.Location = New System.Drawing.Point(435, 63)
        Me.lstUsers.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstUsers.Name = "lstUsers"
        Me.lstUsers.ShowAddNewButton = False
        Me.lstUsers.ShowInverse = True
        Me.lstUsers.ShowMagnifierButton = False
        Me.lstUsers.ShowNoCheck = False
        Me.lstUsers.ShowResetAllButton = False
        Me.lstUsers.ShowSelectall = True
        Me.lstUsers.Size = New System.Drawing.Size(408, 498)
        Me.lstUsers.TabIndex = 28
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
        Me.lstUserGroups.Location = New System.Drawing.Point(18, 63)
        Me.lstUserGroups.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstUserGroups.Name = "lstUserGroups"
        Me.lstUserGroups.ShowAddNewButton = False
        Me.lstUserGroups.ShowInverse = True
        Me.lstUserGroups.ShowMagnifierButton = False
        Me.lstUserGroups.ShowNoCheck = False
        Me.lstUserGroups.ShowResetAllButton = False
        Me.lstUserGroups.ShowSelectall = True
        Me.lstUserGroups.Size = New System.Drawing.Size(408, 498)
        Me.lstUserGroups.TabIndex = 27
        Me.lstUserGroups.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'frmSalesInquiryUserSelection
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(858, 634)
        Me.Controls.Add(Me.lstUsers)
        Me.Controls.Add(Me.lstUserGroups)
        Me.Controls.Add(Me.lblInquiryNo)
        Me.Controls.Add(Me.lblCustomerName)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSalesInquiryUserSelection"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "User Selection of Sales Inquiry"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents lblInquiryNo As System.Windows.Forms.Label
    Friend WithEvents lblCustomerName As System.Windows.Forms.Label
    Friend WithEvents lstUsers As SimpleAccounts.uiListControl
    Friend WithEvents lstUserGroups As SimpleAccounts.uiListControl

End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCustomerTypeWisePriceList
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
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX()
        Me.cmbCustomerType = New System.Windows.Forms.ComboBox()
        Me.lblCustomerType = New System.Windows.Forms.Label()
        Me.gbItems = New System.Windows.Forms.GroupBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtsearch = New System.Windows.Forms.TextBox()
        Me.lstItems = New SimpleAccounts.uiListControl()
        Me.gbCustomerType = New System.Windows.Forms.GroupBox()
        Me.UiCustomers = New SimpleAccounts.uiListControl()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gbItems.SuspendLayout()
        Me.gbCustomerType.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(725, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(66, 22)
        Me.btnRefresh.Text = "&Refresh"
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(7, 7)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(295, 24)
        Me.lblHeader.TabIndex = 2
        Me.lblHeader.Text = "Customer Type Wise Price List"
        '
        'GridEX1
        '
        Me.GridEX1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX1.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEX1.Location = New System.Drawing.Point(4, 287)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.Size = New System.Drawing.Size(716, 150)
        Me.GridEX1.TabIndex = 6
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'cmbCustomerType
        '
        Me.cmbCustomerType.FormattingEnabled = True
        Me.cmbCustomerType.Location = New System.Drawing.Point(85, 27)
        Me.cmbCustomerType.Name = "cmbCustomerType"
        Me.cmbCustomerType.Size = New System.Drawing.Size(202, 21)
        Me.cmbCustomerType.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbCustomerType, "Select customer type")
        '
        'lblCustomerType
        '
        Me.lblCustomerType.AutoSize = True
        Me.lblCustomerType.Location = New System.Drawing.Point(1, 30)
        Me.lblCustomerType.Name = "lblCustomerType"
        Me.lblCustomerType.Size = New System.Drawing.Size(78, 13)
        Me.lblCustomerType.TabIndex = 0
        Me.lblCustomerType.Text = "Customer Type"
        '
        'gbItems
        '
        Me.gbItems.Controls.Add(Me.Label4)
        Me.gbItems.Controls.Add(Me.txtsearch)
        Me.gbItems.Controls.Add(Me.lstItems)
        Me.gbItems.Location = New System.Drawing.Point(332, 65)
        Me.gbItems.Name = "gbItems"
        Me.gbItems.Size = New System.Drawing.Size(295, 216)
        Me.gbItems.TabIndex = 4
        Me.gbItems.TabStop = False
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(7, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(41, 13)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Search"
        '
        'txtsearch
        '
        Me.txtsearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtsearch.Location = New System.Drawing.Point(54, 21)
        Me.txtsearch.Name = "txtsearch"
        Me.txtsearch.Size = New System.Drawing.Size(214, 20)
        Me.txtsearch.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.txtsearch, "Search items")
        '
        'lstItems
        '
        Me.lstItems.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstItems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstItems.AutoScroll = True
        Me.lstItems.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstItems.BackColor = System.Drawing.Color.Transparent
        Me.lstItems.disableWhenChecked = False
        Me.lstItems.HeadingLabelName = Nothing
        Me.lstItems.HeadingText = "Items"
        Me.lstItems.Location = New System.Drawing.Point(6, 47)
        Me.lstItems.Margin = New System.Windows.Forms.Padding(3, 3, 3, 5)
        Me.lstItems.Name = "lstItems"
        Me.lstItems.ShowAddNewButton = False
        Me.lstItems.ShowInverse = True
        Me.lstItems.ShowMagnifierButton = False
        Me.lstItems.ShowNoCheck = False
        Me.lstItems.ShowResetAllButton = False
        Me.lstItems.ShowSelectall = True
        Me.lstItems.Size = New System.Drawing.Size(289, 160)
        Me.lstItems.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.lstItems, "Items list")
        Me.lstItems.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'gbCustomerType
        '
        Me.gbCustomerType.Controls.Add(Me.UiCustomers)
        Me.gbCustomerType.Controls.Add(Me.cmbCustomerType)
        Me.gbCustomerType.Controls.Add(Me.lblCustomerType)
        Me.gbCustomerType.Location = New System.Drawing.Point(6, 54)
        Me.gbCustomerType.Name = "gbCustomerType"
        Me.gbCustomerType.Size = New System.Drawing.Size(320, 227)
        Me.gbCustomerType.TabIndex = 3
        Me.gbCustomerType.TabStop = False
        '
        'UiCustomers
        '
        Me.UiCustomers.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.UiCustomers.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UiCustomers.AutoScroll = True
        Me.UiCustomers.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.UiCustomers.BackColor = System.Drawing.Color.Transparent
        Me.UiCustomers.disableWhenChecked = False
        Me.UiCustomers.HeadingLabelName = Nothing
        Me.UiCustomers.HeadingText = "Customers"
        Me.UiCustomers.Location = New System.Drawing.Point(6, 50)
        Me.UiCustomers.Margin = New System.Windows.Forms.Padding(3, 3, 3, 7)
        Me.UiCustomers.Name = "UiCustomers"
        Me.UiCustomers.ShowAddNewButton = False
        Me.UiCustomers.ShowInverse = True
        Me.UiCustomers.ShowMagnifierButton = False
        Me.UiCustomers.ShowNoCheck = False
        Me.UiCustomers.ShowResetAllButton = False
        Me.UiCustomers.ShowSelectall = True
        Me.UiCustomers.Size = New System.Drawing.Size(308, 171)
        Me.UiCustomers.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.UiCustomers, "Display customer list against customer type")
        Me.UiCustomers.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(633, 247)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(75, 28)
        Me.btnShow.TabIndex = 5
        Me.btnShow.Text = "Show"
        Me.ToolTip1.SetToolTip(Me.btnShow, "Show record")
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'pnlHeader
        '
        Me.pnlHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Location = New System.Drawing.Point(6, 22)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(721, 45)
        Me.pnlHeader.TabIndex = 24
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(692, 0)
        Me.CtrlGrdBar1.MyGrid = Me.GridEX1
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(33, 25)
        Me.CtrlGrdBar1.TabIndex = 1
        Me.CtrlGrdBar1.TabStop = False
        '
        'frmCustomerTypeWisePriceList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(725, 434)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.btnShow)
        Me.Controls.Add(Me.gbCustomerType)
        Me.Controls.Add(Me.gbItems)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "frmCustomerTypeWisePriceList"
        Me.Text = "Customer Type Wise Price List"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gbItems.ResumeLayout(False)
        Me.gbItems.PerformLayout()
        Me.gbCustomerType.ResumeLayout(False)
        Me.gbCustomerType.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents cmbCustomerType As System.Windows.Forms.ComboBox
    Friend WithEvents lblCustomerType As System.Windows.Forms.Label
    Friend WithEvents gbItems As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtsearch As System.Windows.Forms.TextBox
    Friend WithEvents lstItems As SimpleAccounts.uiListControl
    Friend WithEvents gbCustomerType As System.Windows.Forms.GroupBox
    Friend WithEvents UiCustomers As SimpleAccounts.uiListControl
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

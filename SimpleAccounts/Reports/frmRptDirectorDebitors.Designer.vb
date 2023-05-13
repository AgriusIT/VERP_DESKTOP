<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptDirectorDebitors
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRptDirectorDebitors))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.lstDirector = New SimpleAccounts.uiListControl()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.lstSaleman = New SimpleAccounts.uiListControl()
        Me.lstManager = New SimpleAccounts.uiListControl()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lstArea = New SimpleAccounts.uiListControl()
        Me.lstCity = New SimpleAccounts.uiListControl()
        Me.lstRegion = New SimpleAccounts.uiListControl()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.ToolStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(974, 32)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 29)
        Me.btnRefresh.Text = "&Refresh"
        '
        'lstDirector
        '
        Me.lstDirector.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstDirector.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstDirector.BackColor = System.Drawing.Color.Transparent
        Me.lstDirector.disableWhenChecked = False
        Me.lstDirector.HeadingLabelName = Nothing
        Me.lstDirector.HeadingText = "Directors"
        Me.lstDirector.Location = New System.Drawing.Point(9, 29)
        Me.lstDirector.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstDirector.Name = "lstDirector"
        Me.lstDirector.ShowAddNewButton = False
        Me.lstDirector.ShowInverse = True
        Me.lstDirector.ShowMagnifierButton = False
        Me.lstDirector.ShowNoCheck = False
        Me.lstDirector.ShowResetAllButton = False
        Me.lstDirector.ShowSelectall = True
        Me.lstDirector.Size = New System.Drawing.Size(288, 323)
        Me.lstDirector.TabIndex = 0
        Me.lstDirector.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lstSaleman)
        Me.GroupBox1.Controls.Add(Me.lstManager)
        Me.GroupBox1.Controls.Add(Me.lstDirector)
        Me.GroupBox1.Location = New System.Drawing.Point(18, 98)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(938, 368)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        '
        'lstSaleman
        '
        Me.lstSaleman.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstSaleman.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstSaleman.BackColor = System.Drawing.Color.Transparent
        Me.lstSaleman.disableWhenChecked = False
        Me.lstSaleman.HeadingLabelName = Nothing
        Me.lstSaleman.HeadingText = "Sales Man"
        Me.lstSaleman.Location = New System.Drawing.Point(603, 29)
        Me.lstSaleman.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstSaleman.Name = "lstSaleman"
        Me.lstSaleman.ShowAddNewButton = False
        Me.lstSaleman.ShowInverse = True
        Me.lstSaleman.ShowMagnifierButton = False
        Me.lstSaleman.ShowNoCheck = False
        Me.lstSaleman.ShowResetAllButton = False
        Me.lstSaleman.ShowSelectall = True
        Me.lstSaleman.Size = New System.Drawing.Size(288, 323)
        Me.lstSaleman.TabIndex = 2
        Me.lstSaleman.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstManager
        '
        Me.lstManager.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstManager.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstManager.BackColor = System.Drawing.Color.Transparent
        Me.lstManager.disableWhenChecked = False
        Me.lstManager.HeadingLabelName = Nothing
        Me.lstManager.HeadingText = "Managers"
        Me.lstManager.Location = New System.Drawing.Point(306, 29)
        Me.lstManager.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstManager.Name = "lstManager"
        Me.lstManager.ShowAddNewButton = False
        Me.lstManager.ShowInverse = True
        Me.lstManager.ShowMagnifierButton = False
        Me.lstManager.ShowNoCheck = False
        Me.lstManager.ShowResetAllButton = False
        Me.lstManager.ShowSelectall = True
        Me.lstManager.Size = New System.Drawing.Size(288, 323)
        Me.lstManager.TabIndex = 1
        Me.lstManager.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lstArea)
        Me.GroupBox2.Controls.Add(Me.lstCity)
        Me.GroupBox2.Controls.Add(Me.lstRegion)
        Me.GroupBox2.Location = New System.Drawing.Point(18, 475)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Size = New System.Drawing.Size(938, 366)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        '
        'lstArea
        '
        Me.lstArea.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstArea.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstArea.BackColor = System.Drawing.Color.Transparent
        Me.lstArea.disableWhenChecked = False
        Me.lstArea.HeadingLabelName = Nothing
        Me.lstArea.HeadingText = "Area"
        Me.lstArea.Location = New System.Drawing.Point(603, 29)
        Me.lstArea.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstArea.Name = "lstArea"
        Me.lstArea.ShowAddNewButton = False
        Me.lstArea.ShowInverse = True
        Me.lstArea.ShowMagnifierButton = False
        Me.lstArea.ShowNoCheck = False
        Me.lstArea.ShowResetAllButton = False
        Me.lstArea.ShowSelectall = True
        Me.lstArea.Size = New System.Drawing.Size(288, 323)
        Me.lstArea.TabIndex = 2
        Me.lstArea.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstCity
        '
        Me.lstCity.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstCity.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstCity.BackColor = System.Drawing.Color.Transparent
        Me.lstCity.disableWhenChecked = False
        Me.lstCity.HeadingLabelName = Nothing
        Me.lstCity.HeadingText = "City"
        Me.lstCity.Location = New System.Drawing.Point(306, 29)
        Me.lstCity.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstCity.Name = "lstCity"
        Me.lstCity.ShowAddNewButton = False
        Me.lstCity.ShowInverse = True
        Me.lstCity.ShowMagnifierButton = False
        Me.lstCity.ShowNoCheck = False
        Me.lstCity.ShowResetAllButton = False
        Me.lstCity.ShowSelectall = True
        Me.lstCity.Size = New System.Drawing.Size(288, 323)
        Me.lstCity.TabIndex = 1
        Me.lstCity.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstRegion
        '
        Me.lstRegion.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstRegion.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstRegion.BackColor = System.Drawing.Color.Transparent
        Me.lstRegion.disableWhenChecked = False
        Me.lstRegion.HeadingLabelName = Nothing
        Me.lstRegion.HeadingText = "Region"
        Me.lstRegion.Location = New System.Drawing.Point(9, 29)
        Me.lstRegion.Margin = New System.Windows.Forms.Padding(6, 8, 6, 8)
        Me.lstRegion.Name = "lstRegion"
        Me.lstRegion.ShowAddNewButton = False
        Me.lstRegion.ShowInverse = True
        Me.lstRegion.ShowMagnifierButton = False
        Me.lstRegion.ShowNoCheck = False
        Me.lstRegion.ShowResetAllButton = False
        Me.lstRegion.ShowSelectall = True
        Me.lstRegion.Size = New System.Drawing.Size(288, 323)
        Me.lstRegion.TabIndex = 0
        Me.lstRegion.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(848, 851)
        Me.btnShow.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(108, 42)
        Me.btnShow.TabIndex = 5
        Me.btnShow.Text = "Show"
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd/MMM/yyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(646, 852)
        Me.dtpToDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(190, 26)
        Me.dtpToDate.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(579, 862)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 20)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Up to: "
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnClose)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 32)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(974, 65)
        Me.pnlHeader.TabIndex = 113
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.btnClose.Location = New System.Drawing.Point(902, 9)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(54, 46)
        Me.btnClose.TabIndex = 116
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(4, 14)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(253, 36)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Director Debitors"
        '
        'frmRptDirectorDebitors
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(974, 923)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtpToDate)
        Me.Controls.Add(Me.btnShow)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmRptDirectorDebitors"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Director Debitors"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents lstDirector As SimpleAccounts.uiListControl
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lstArea As SimpleAccounts.uiListControl
    Friend WithEvents lstCity As SimpleAccounts.uiListControl
    Friend WithEvents lstRegion As SimpleAccounts.uiListControl
    Friend WithEvents lstSaleman As SimpleAccounts.uiListControl
    Friend WithEvents lstManager As SimpleAccounts.uiListControl
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblHeader As System.Windows.Forms.Label
End Class

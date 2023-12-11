<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptEmpDetailWithBasicPayAndAllownces
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmRptEmpDetailWithBasicPayAndAllownces))
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance14 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance16 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance15 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance19 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance22 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance24 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance20 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance18 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance17 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance23 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance21 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.pnlLstBox = New System.Windows.Forms.Panel()
        Me.cmbEmp = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbEmployee = New System.Windows.Forms.ComboBox()
        Me.btnShow = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.lstCostCenter = New SimpleAccounts.uiListControl()
        Me.lstHeadCostCenter = New SimpleAccounts.uiListControl()
        Me.lstDepartment = New SimpleAccounts.uiListControl()
        Me.lstDesignation = New SimpleAccounts.uiListControl()
        Me.lstShiftGroup = New SimpleAccounts.uiListControl()
        Me.lstCity = New SimpleAccounts.uiListControl()
        Me.pnlHeader.SuspendLayout()
        Me.pnlLstBox.SuspendLayout()
        CType(Me.cmbEmp, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnClose)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(833, 42)
        Me.pnlHeader.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.btnClose.Location = New System.Drawing.Point(785, 6)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(36, 30)
        Me.btnClose.TabIndex = 1
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.lblHeader.Location = New System.Drawing.Point(8, 9)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(594, 23)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Employee Detail Report with Basic Pay and Allownces" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'pnlLstBox
        '
        Me.pnlLstBox.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.pnlLstBox.Controls.Add(Me.cmbEmp)
        Me.pnlLstBox.Controls.Add(Me.GridEX1)
        Me.pnlLstBox.Controls.Add(Me.Button1)
        Me.pnlLstBox.Controls.Add(Me.Label5)
        Me.pnlLstBox.Controls.Add(Me.cmbEmployee)
        Me.pnlLstBox.Controls.Add(Me.btnShow)
        Me.pnlLstBox.Controls.Add(Me.lstCostCenter)
        Me.pnlLstBox.Controls.Add(Me.lstHeadCostCenter)
        Me.pnlLstBox.Controls.Add(Me.lstDepartment)
        Me.pnlLstBox.Controls.Add(Me.lstDesignation)
        Me.pnlLstBox.Controls.Add(Me.lstShiftGroup)
        Me.pnlLstBox.Controls.Add(Me.lstCity)
        Me.pnlLstBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlLstBox.Location = New System.Drawing.Point(0, 42)
        Me.pnlLstBox.Name = "pnlLstBox"
        Me.pnlLstBox.Size = New System.Drawing.Size(833, 367)
        Me.pnlLstBox.TabIndex = 1
        '
        'cmbEmp
        '
        Me.cmbEmp.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.SuggestAppend
        Me.cmbEmp.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbEmp.CheckedListSettings.CheckStateMember = ""
        Appearance13.BackColor = System.Drawing.SystemColors.Window
        Appearance13.BorderColor = System.Drawing.SystemColors.InactiveCaption
        Me.cmbEmp.DisplayLayout.Appearance = Appearance13
        Me.cmbEmp.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.cmbEmp.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance14.BackColor = System.Drawing.SystemColors.ActiveBorder
        Appearance14.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance14.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance14.BorderColor = System.Drawing.SystemColors.Window
        Me.cmbEmp.DisplayLayout.GroupByBox.Appearance = Appearance14
        Appearance16.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cmbEmp.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance16
        Me.cmbEmp.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance15.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance15.BackColor2 = System.Drawing.SystemColors.Control
        Appearance15.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance15.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cmbEmp.DisplayLayout.GroupByBox.PromptAppearance = Appearance15
        Me.cmbEmp.DisplayLayout.MaxColScrollRegions = 1
        Me.cmbEmp.DisplayLayout.MaxRowScrollRegions = 1
        Appearance19.BackColor = System.Drawing.SystemColors.Window
        Appearance19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmbEmp.DisplayLayout.Override.ActiveCellAppearance = Appearance19
        Appearance22.BackColor = System.Drawing.SystemColors.Highlight
        Appearance22.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.cmbEmp.DisplayLayout.Override.ActiveRowAppearance = Appearance22
        Me.cmbEmp.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.cmbEmp.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance24.BackColor = System.Drawing.SystemColors.Window
        Me.cmbEmp.DisplayLayout.Override.CardAreaAppearance = Appearance24
        Appearance20.BorderColor = System.Drawing.Color.Silver
        Appearance20.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.cmbEmp.DisplayLayout.Override.CellAppearance = Appearance20
        Me.cmbEmp.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.cmbEmp.DisplayLayout.Override.CellPadding = 0
        Appearance18.BackColor = System.Drawing.SystemColors.Control
        Appearance18.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance18.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance18.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance18.BorderColor = System.Drawing.SystemColors.Window
        Me.cmbEmp.DisplayLayout.Override.GroupByRowAppearance = Appearance18
        Appearance17.TextHAlignAsString = "Left"
        Me.cmbEmp.DisplayLayout.Override.HeaderAppearance = Appearance17
        Me.cmbEmp.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.cmbEmp.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance23.BackColor = System.Drawing.SystemColors.Window
        Appearance23.BorderColor = System.Drawing.Color.Silver
        Me.cmbEmp.DisplayLayout.Override.RowAppearance = Appearance23
        Me.cmbEmp.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance21.BackColor = System.Drawing.SystemColors.ControlLight
        Me.cmbEmp.DisplayLayout.Override.TemplateAddRowAppearance = Appearance21
        Me.cmbEmp.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbEmp.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbEmp.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.cmbEmp.LimitToList = True
        Me.cmbEmp.Location = New System.Drawing.Point(76, 21)
        Me.cmbEmp.Name = "cmbEmp"
        Me.cmbEmp.Size = New System.Drawing.Size(303, 22)
        Me.cmbEmp.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.cmbEmp, "Select Employee")
        '
        'GridEX1
        '
        Me.GridEX1.Location = New System.Drawing.Point(617, 332)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.Size = New System.Drawing.Size(140, 29)
        Me.GridEX1.TabIndex = 10
        Me.GridEX1.Visible = False
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(515, 332)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 9
        Me.Button1.Text = "&Show"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(17, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 13)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Employee"
        '
        'cmbEmployee
        '
        Me.cmbEmployee.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEmployee.FormattingEnabled = True
        Me.cmbEmployee.Location = New System.Drawing.Point(530, 22)
        Me.cmbEmployee.Name = "cmbEmployee"
        Me.cmbEmployee.Size = New System.Drawing.Size(267, 21)
        Me.cmbEmployee.TabIndex = 1
        Me.cmbEmployee.Visible = False
        '
        'btnShow
        '
        Me.btnShow.Location = New System.Drawing.Point(424, 332)
        Me.btnShow.Name = "btnShow"
        Me.btnShow.Size = New System.Drawing.Size(75, 23)
        Me.btnShow.TabIndex = 8
        Me.btnShow.Text = "&Show"
        Me.ToolTip1.SetToolTip(Me.btnShow, "Print Report")
        Me.btnShow.UseVisualStyleBackColor = True
        '
        'lstCostCenter
        '
        Me.lstCostCenter.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstCostCenter.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstCostCenter.BackColor = System.Drawing.Color.Transparent
        Me.lstCostCenter.disableWhenChecked = False
        Me.lstCostCenter.HeadingLabelName = "lstCostCenter"
        Me.lstCostCenter.HeadingText = "Cost Center"
        Me.lstCostCenter.Location = New System.Drawing.Point(219, 223)
        Me.lstCostCenter.Name = "lstCostCenter"
        Me.lstCostCenter.ShowAddNewButton = False
        Me.lstCostCenter.ShowInverse = True
        Me.lstCostCenter.ShowMagnifierButton = False
        Me.lstCostCenter.ShowNoCheck = False
        Me.lstCostCenter.ShowResetAllButton = False
        Me.lstCostCenter.ShowSelectall = True
        Me.lstCostCenter.Size = New System.Drawing.Size(189, 159)
        Me.lstCostCenter.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.lstCostCenter, "Select Cost Center")
        Me.lstCostCenter.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstHeadCostCenter
        '
        Me.lstHeadCostCenter.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstHeadCostCenter.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstHeadCostCenter.BackColor = System.Drawing.Color.Transparent
        Me.lstHeadCostCenter.disableWhenChecked = False
        Me.lstHeadCostCenter.HeadingLabelName = "lstHeadCostCenter"
        Me.lstHeadCostCenter.HeadingText = "Head Cost Center"
        Me.lstHeadCostCenter.Location = New System.Drawing.Point(12, 223)
        Me.lstHeadCostCenter.Name = "lstHeadCostCenter"
        Me.lstHeadCostCenter.ShowAddNewButton = False
        Me.lstHeadCostCenter.ShowInverse = True
        Me.lstHeadCostCenter.ShowMagnifierButton = False
        Me.lstHeadCostCenter.ShowNoCheck = False
        Me.lstHeadCostCenter.ShowResetAllButton = False
        Me.lstHeadCostCenter.ShowSelectall = True
        Me.lstHeadCostCenter.Size = New System.Drawing.Size(201, 159)
        Me.lstHeadCostCenter.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.lstHeadCostCenter, "Select Head Cost Center")
        Me.lstHeadCostCenter.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstDepartment
        '
        Me.lstDepartment.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstDepartment.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstDepartment.BackColor = System.Drawing.Color.Transparent
        Me.lstDepartment.disableWhenChecked = False
        Me.lstDepartment.HeadingLabelName = "lstEmpDepartment"
        Me.lstDepartment.HeadingText = "Department"
        Me.lstDepartment.Location = New System.Drawing.Point(12, 58)
        Me.lstDepartment.Name = "lstDepartment"
        Me.lstDepartment.ShowAddNewButton = False
        Me.lstDepartment.ShowInverse = True
        Me.lstDepartment.ShowMagnifierButton = False
        Me.lstDepartment.ShowNoCheck = False
        Me.lstDepartment.ShowResetAllButton = False
        Me.lstDepartment.ShowSelectall = True
        Me.lstDepartment.Size = New System.Drawing.Size(201, 159)
        Me.lstDepartment.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.lstDepartment, "Select Department")
        Me.lstDepartment.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstDesignation
        '
        Me.lstDesignation.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstDesignation.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstDesignation.BackColor = System.Drawing.Color.Transparent
        Me.lstDesignation.disableWhenChecked = False
        Me.lstDesignation.HeadingLabelName = "lstDesignation"
        Me.lstDesignation.HeadingText = "Designation"
        Me.lstDesignation.Location = New System.Drawing.Point(219, 58)
        Me.lstDesignation.Name = "lstDesignation"
        Me.lstDesignation.ShowAddNewButton = False
        Me.lstDesignation.ShowInverse = True
        Me.lstDesignation.ShowMagnifierButton = False
        Me.lstDesignation.ShowNoCheck = False
        Me.lstDesignation.ShowResetAllButton = False
        Me.lstDesignation.ShowSelectall = True
        Me.lstDesignation.Size = New System.Drawing.Size(189, 159)
        Me.lstDesignation.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.lstDesignation, "Select Designation")
        Me.lstDesignation.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstShiftGroup
        '
        Me.lstShiftGroup.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstShiftGroup.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstShiftGroup.BackColor = System.Drawing.Color.Transparent
        Me.lstShiftGroup.disableWhenChecked = False
        Me.lstShiftGroup.HeadingLabelName = "lstShiftGroup"
        Me.lstShiftGroup.HeadingText = "Shift Group"
        Me.lstShiftGroup.Location = New System.Drawing.Point(414, 58)
        Me.lstShiftGroup.Name = "lstShiftGroup"
        Me.lstShiftGroup.ShowAddNewButton = False
        Me.lstShiftGroup.ShowInverse = True
        Me.lstShiftGroup.ShowMagnifierButton = False
        Me.lstShiftGroup.ShowNoCheck = False
        Me.lstShiftGroup.ShowResetAllButton = False
        Me.lstShiftGroup.ShowSelectall = True
        Me.lstShiftGroup.Size = New System.Drawing.Size(188, 159)
        Me.lstShiftGroup.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.lstShiftGroup, "Select Shift")
        Me.lstShiftGroup.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'lstCity
        '
        Me.lstCity.AddWhichConfiguration = SBUtility.Utility.EnumProjectForms.ForAllForms
        Me.lstCity.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange
        Me.lstCity.BackColor = System.Drawing.Color.Transparent
        Me.lstCity.disableWhenChecked = False
        Me.lstCity.HeadingLabelName = "lblCity"
        Me.lstCity.HeadingText = "City"
        Me.lstCity.Location = New System.Drawing.Point(608, 58)
        Me.lstCity.Name = "lstCity"
        Me.lstCity.ShowAddNewButton = False
        Me.lstCity.ShowInverse = True
        Me.lstCity.ShowMagnifierButton = False
        Me.lstCity.ShowNoCheck = False
        Me.lstCity.ShowResetAllButton = False
        Me.lstCity.ShowSelectall = True
        Me.lstCity.Size = New System.Drawing.Size(187, 159)
        Me.lstCity.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.lstCity, "Select City")
        Me.lstCity.WhichHelp = SimpleAccounts.uiListControl.enumWhichHelpForm._ProductSearchHelp
        '
        'frmRptEmpDetailWithBasicPayAndAllownces
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(833, 409)
        Me.Controls.Add(Me.pnlLstBox)
        Me.Controls.Add(Me.pnlHeader)
        Me.Name = "frmRptEmpDetailWithBasicPayAndAllownces"
        Me.Text = "Employee Detail With Basic Pay And Allownces"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlLstBox.ResumeLayout(False)
        Me.pnlLstBox.PerformLayout()
        CType(Me.cmbEmp, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents pnlLstBox As System.Windows.Forms.Panel
    Friend WithEvents btnShow As System.Windows.Forms.Button
    Friend WithEvents lstCostCenter As SimpleAccounts.uiListControl
    Friend WithEvents lstHeadCostCenter As SimpleAccounts.uiListControl
    Friend WithEvents lstDepartment As SimpleAccounts.uiListControl
    Friend WithEvents lstDesignation As SimpleAccounts.uiListControl
    Friend WithEvents lstShiftGroup As SimpleAccounts.uiListControl
    Friend WithEvents lstCity As SimpleAccounts.uiListControl
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cmbEmployee As System.Windows.Forms.ComboBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents cmbEmp As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSOPopup
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSOPopup))
        Dim grd_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim Appearance22 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn26 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("ID")
        Dim UltraGridColumn27 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Name")
        Dim UltraGridColumn28 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("Territory")
        Dim UltraGridColumn29 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("City")
        Dim UltraGridColumn30 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("State")
        Dim UltraGridColumn31 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("AcId")
        Dim Appearance23 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance24 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance25 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance26 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.lblProductionProcess = New System.Windows.Forms.Label()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.lblSO = New System.Windows.Forms.Label()
        Me.cmbSO = New System.Windows.Forms.ComboBox()
        Me.lblCustomer = New System.Windows.Forms.Label()
        Me.DateTimePicker1 = New System.Windows.Forms.DateTimePicker()
        Me.DateTimePicker2 = New System.Windows.Forms.DateTimePicker()
        Me.lblFromDate = New System.Windows.Forms.Label()
        Me.lblTodate = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.rbDC = New System.Windows.Forms.RadioButton()
        Me.rbSO = New System.Windows.Forms.RadioButton()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmbCustomer = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.Panel3.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.cmbCustomer, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel3
        '
        Me.Panel3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.Panel3.Controls.Add(Me.Button8)
        Me.Panel3.Controls.Add(Me.Button6)
        Me.Panel3.Controls.Add(Me.Button1)
        Me.Panel3.Controls.Add(Me.Button4)
        Me.Panel3.Controls.Add(Me.Button7)
        Me.Panel3.Controls.Add(Me.Button3)
        Me.Panel3.Controls.Add(Me.Button2)
        Me.Panel3.Controls.Add(Me.lblProductionProcess)
        Me.Panel3.Location = New System.Drawing.Point(3, 2)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(773, 46)
        Me.Panel3.TabIndex = 0
        '
        'Button8
        '
        Me.Button8.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button8.BackgroundImage = CType(resources.GetObject("Button8.BackgroundImage"), System.Drawing.Image)
        Me.Button8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button8.FlatAppearance.BorderSize = 0
        Me.Button8.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button8.Location = New System.Drawing.Point(620, 9)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(25, 23)
        Me.Button8.TabIndex = 3
        Me.Button8.UseVisualStyleBackColor = True
        Me.Button8.Visible = False
        '
        'Button6
        '
        Me.Button6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button6.BackgroundImage = CType(resources.GetObject("Button6.BackgroundImage"), System.Drawing.Image)
        Me.Button6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button6.FlatAppearance.BorderSize = 0
        Me.Button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button6.Location = New System.Drawing.Point(730, 6)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(27, 30)
        Me.Button6.TabIndex = 8
        Me.Button6.UseVisualStyleBackColor = True
        Me.Button6.Visible = False
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackgroundImage = CType(resources.GetObject("Button1.BackgroundImage"), System.Drawing.Image)
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(706, 8)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(30, 26)
        Me.Button1.TabIndex = 7
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button4.BackgroundImage = CType(resources.GetObject("Button4.BackgroundImage"), System.Drawing.Image)
        Me.Button4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button4.FlatAppearance.BorderSize = 0
        Me.Button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button4.Location = New System.Drawing.Point(684, 9)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(28, 24)
        Me.Button4.TabIndex = 6
        Me.Button4.UseVisualStyleBackColor = True
        Me.Button4.Visible = False
        '
        'Button7
        '
        Me.Button7.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button7.BackgroundImage = CType(resources.GetObject("Button7.BackgroundImage"), System.Drawing.Image)
        Me.Button7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button7.FlatAppearance.BorderSize = 0
        Me.Button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button7.ForeColor = System.Drawing.SystemColors.Control
        Me.Button7.Location = New System.Drawing.Point(599, 10)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(20, 22)
        Me.Button7.TabIndex = 2
        Me.Button7.UseVisualStyleBackColor = True
        Me.Button7.Visible = False
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.BackgroundImage = CType(resources.GetObject("Button3.BackgroundImage"), System.Drawing.Image)
        Me.Button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button3.FlatAppearance.BorderSize = 0
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Location = New System.Drawing.Point(663, 9)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(25, 23)
        Me.Button3.TabIndex = 5
        Me.Button3.UseVisualStyleBackColor = True
        Me.Button3.Visible = False
        '
        'Button2
        '
        Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button2.BackgroundImage = CType(resources.GetObject("Button2.BackgroundImage"), System.Drawing.Image)
        Me.Button2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button2.FlatAppearance.BorderSize = 0
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Location = New System.Drawing.Point(643, 9)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(25, 23)
        Me.Button2.TabIndex = 4
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'lblProductionProcess
        '
        Me.lblProductionProcess.AutoSize = True
        Me.lblProductionProcess.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblProductionProcess.ForeColor = System.Drawing.Color.White
        Me.lblProductionProcess.Location = New System.Drawing.Point(29, 9)
        Me.lblProductionProcess.Name = "lblProductionProcess"
        Me.lblProductionProcess.Size = New System.Drawing.Size(146, 23)
        Me.lblProductionProcess.TabIndex = 1
        Me.lblProductionProcess.Text = "Sales Orders"
        '
        'grd
        '
        Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grd_DesignTimeLayout.LayoutString = resources.GetString("grd_DesignTimeLayout.LayoutString")
        Me.grd.DesignTimeLayout = grd_DesignTimeLayout
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.GroupByBoxVisible = False
        Me.grd.Location = New System.Drawing.Point(3, 124)
        Me.grd.Name = "grd"
        Me.grd.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndMoveToFirstCellInNewRow
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(773, 375)
        Me.grd.TabIndex = 10
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'lblSO
        '
        Me.lblSO.AutoSize = True
        Me.lblSO.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblSO.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblSO.Location = New System.Drawing.Point(244, 100)
        Me.lblSO.Name = "lblSO"
        Me.lblSO.Size = New System.Drawing.Size(22, 13)
        Me.lblSO.TabIndex = 7
        Me.lblSO.Text = "SO"
        '
        'cmbSO
        '
        Me.cmbSO.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbSO.FormattingEnabled = True
        Me.cmbSO.Location = New System.Drawing.Point(294, 97)
        Me.cmbSO.Name = "cmbSO"
        Me.cmbSO.Size = New System.Drawing.Size(165, 21)
        Me.cmbSO.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.cmbSO, "Please select SO ")
        '
        'lblCustomer
        '
        Me.lblCustomer.AutoSize = True
        Me.lblCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblCustomer.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCustomer.Location = New System.Drawing.Point(18, 100)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(51, 13)
        Me.lblCustomer.TabIndex = 5
        Me.lblCustomer.Text = "Customer"
        '
        'DateTimePicker1
        '
        Me.DateTimePicker1.Checked = False
        Me.DateTimePicker1.CustomFormat = "dd/MM/yyyy"
        Me.DateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker1.Location = New System.Drawing.Point(75, 52)
        Me.DateTimePicker1.Name = "DateTimePicker1"
        Me.DateTimePicker1.ShowCheckBox = True
        Me.DateTimePicker1.Size = New System.Drawing.Size(165, 20)
        Me.DateTimePicker1.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.DateTimePicker1, "Please select From Date")
        '
        'DateTimePicker2
        '
        Me.DateTimePicker2.Checked = False
        Me.DateTimePicker2.CustomFormat = "dd/MM/yyyy"
        Me.DateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DateTimePicker2.Location = New System.Drawing.Point(294, 52)
        Me.DateTimePicker2.Name = "DateTimePicker2"
        Me.DateTimePicker2.ShowCheckBox = True
        Me.DateTimePicker2.Size = New System.Drawing.Size(165, 20)
        Me.DateTimePicker2.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.DateTimePicker2, "Please select To Date")
        '
        'lblFromDate
        '
        Me.lblFromDate.AutoSize = True
        Me.lblFromDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblFromDate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblFromDate.Location = New System.Drawing.Point(15, 55)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(54, 13)
        Me.lblFromDate.TabIndex = 1
        Me.lblFromDate.Text = "From date"
        '
        'lblTodate
        '
        Me.lblTodate.AutoSize = True
        Me.lblTodate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.lblTodate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTodate.Location = New System.Drawing.Point(244, 57)
        Me.lblTodate.Name = "lblTodate"
        Me.lblTodate.Size = New System.Drawing.Size(44, 13)
        Me.lblTodate.TabIndex = 3
        Me.lblTodate.Text = "To date"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rbDC)
        Me.Panel1.Controls.Add(Me.rbSO)
        Me.Panel1.Location = New System.Drawing.Point(294, 73)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(165, 21)
        Me.Panel1.TabIndex = 8
        '
        'rbDC
        '
        Me.rbDC.AutoSize = True
        Me.rbDC.Location = New System.Drawing.Point(49, 3)
        Me.rbDC.Name = "rbDC"
        Me.rbDC.Size = New System.Drawing.Size(40, 17)
        Me.rbDC.TabIndex = 1
        Me.rbDC.Text = "DC"
        Me.rbDC.UseVisualStyleBackColor = True
        '
        'rbSO
        '
        Me.rbSO.AutoSize = True
        Me.rbSO.Checked = True
        Me.rbSO.Location = New System.Drawing.Point(3, 3)
        Me.rbSO.Name = "rbSO"
        Me.rbSO.Size = New System.Drawing.Size(40, 17)
        Me.rbSO.TabIndex = 0
        Me.rbSO.TabStop = True
        Me.rbSO.Text = "SO"
        Me.rbSO.UseVisualStyleBackColor = True
        '
        'cmbCustomer
        '
        Me.cmbCustomer.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cmbCustomer.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbCustomer.CheckedListSettings.CheckStateMember = ""
        Appearance22.BackColor = System.Drawing.Color.White
        Me.cmbCustomer.DisplayLayout.Appearance = Appearance22
        UltraGridColumn26.Header.VisiblePosition = 0
        UltraGridColumn26.Hidden = True
        UltraGridColumn27.Header.VisiblePosition = 1
        UltraGridColumn27.Width = 141
        UltraGridColumn28.Header.VisiblePosition = 2
        UltraGridColumn29.Header.VisiblePosition = 3
        UltraGridColumn30.Header.VisiblePosition = 4
        UltraGridColumn31.Header.VisiblePosition = 5
        UltraGridColumn31.Hidden = True
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn26, UltraGridColumn27, UltraGridColumn28, UltraGridColumn29, UltraGridColumn30, UltraGridColumn31})
        Me.cmbCustomer.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbCustomer.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbCustomer.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbCustomer.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbCustomer.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Appearance23.BackColor = System.Drawing.Color.Transparent
        Me.cmbCustomer.DisplayLayout.Override.CardAreaAppearance = Appearance23
        Me.cmbCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbCustomer.DisplayLayout.Override.CellPadding = 3
        Me.cmbCustomer.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance24.TextHAlignAsString = "Left"
        Me.cmbCustomer.DisplayLayout.Override.HeaderAppearance = Appearance24
        Me.cmbCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance25.BorderColor = System.Drawing.Color.LightGray
        Appearance25.TextVAlignAsString = "Middle"
        Me.cmbCustomer.DisplayLayout.Override.RowAppearance = Appearance25
        Appearance26.BackColor = System.Drawing.Color.LightSteelBlue
        Appearance26.BorderColor = System.Drawing.Color.Black
        Appearance26.ForeColor = System.Drawing.Color.Black
        Me.cmbCustomer.DisplayLayout.Override.SelectedRowAppearance = Appearance26
        Me.cmbCustomer.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbCustomer.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbCustomer.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbCustomer.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.cmbCustomer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbCustomer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbCustomer.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbCustomer.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbCustomer.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear
        Me.cmbCustomer.LimitToList = True
        Me.cmbCustomer.Location = New System.Drawing.Point(73, 96)
        Me.cmbCustomer.Name = "cmbCustomer"
        Me.cmbCustomer.Size = New System.Drawing.Size(167, 22)
        Me.cmbCustomer.TabIndex = 12
        Me.ToolTip1.SetToolTip(Me.cmbCustomer, "Select Customer To Sales")
        '
        'btnLoad
        '
        Me.btnLoad.Location = New System.Drawing.Point(465, 96)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(75, 23)
        Me.btnLoad.TabIndex = 11
        Me.btnLoad.Text = "Load"
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'frmSOPopup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(776, 502)
        Me.Controls.Add(Me.cmbCustomer)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lblTodate)
        Me.Controls.Add(Me.lblFromDate)
        Me.Controls.Add(Me.DateTimePicker2)
        Me.Controls.Add(Me.DateTimePicker1)
        Me.Controls.Add(Me.lblSO)
        Me.Controls.Add(Me.cmbSO)
        Me.Controls.Add(Me.lblCustomer)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.Panel3)
        Me.Name = "frmSOPopup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.cmbCustomer, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents lblProductionProcess As System.Windows.Forms.Label
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblSO As System.Windows.Forms.Label
    Friend WithEvents cmbSO As System.Windows.Forms.ComboBox
    Friend WithEvents lblCustomer As System.Windows.Forms.Label
    Friend WithEvents DateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateTimePicker2 As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblFromDate As System.Windows.Forms.Label
    Friend WithEvents lblTodate As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents rbSO As System.Windows.Forms.RadioButton
    Friend WithEvents rbDC As System.Windows.Forms.RadioButton
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents cmbCustomer As Infragistics.Win.UltraWinGrid.UltraCombo
End Class

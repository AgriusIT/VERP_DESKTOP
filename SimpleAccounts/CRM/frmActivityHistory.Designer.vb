<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmActivityHistory
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
        Dim grdActivityHistory_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.grdActivityHistory = New Janus.Windows.GridEX.GridEX()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblResponsible = New System.Windows.Forms.Label()
        Me.cmbResponsible = New System.Windows.Forms.ComboBox()
        Me.cmbManger = New System.Windows.Forms.ComboBox()
        Me.cmbInside = New System.Windows.Forms.ComboBox()
        Me.cmbActivityType = New System.Windows.Forms.ComboBox()
        Me.cmbStatus = New System.Windows.Forms.ComboBox()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.pnlHeader.SuspendLayout()
        CType(Me.grdActivityHistory, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.Teal
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Controls.Add(Me.CtrlGrdBar1)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1650, 58)
        Me.pnlHeader.TabIndex = 15
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.White
        Me.lblHeader.Location = New System.Drawing.Point(4, 6)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(254, 45)
        Me.lblHeader.TabIndex = 11
        Me.lblHeader.Text = "Activity History"
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(1576, 11)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar1.MyGrid = Me.grdActivityHistory
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(57, 38)
        Me.CtrlGrdBar1.TabIndex = 7
        Me.CtrlGrdBar1.TabStop = False
        '
        'grdActivityHistory
        '
        Me.grdActivityHistory.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdActivityHistory.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdActivityHistory.ColumnAutoResize = True
        grdActivityHistory_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grdActivityHistory.DesignTimeLayout = grdActivityHistory_DesignTimeLayout
        Me.grdActivityHistory.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdActivityHistory.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdActivityHistory.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdActivityHistory.GroupByBoxVisible = False
        Me.grdActivityHistory.Location = New System.Drawing.Point(15, 240)
        Me.grdActivityHistory.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdActivityHistory.Name = "grdActivityHistory"
        Me.grdActivityHistory.RecordNavigator = True
        Me.grdActivityHistory.Size = New System.Drawing.Size(1614, 680)
        Me.grdActivityHistory.TabIndex = 8
        Me.grdActivityHistory.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(512, 157)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(80, 20)
        Me.Label6.TabIndex = 33
        Me.Label6.Text = "Activity To"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(14, 155)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 20)
        Me.Label5.TabIndex = 32
        Me.Label5.Text = "Status"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(262, 157)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(99, 20)
        Me.Label4.TabIndex = 31
        Me.Label4.Text = "Activity From"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(758, 83)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 20)
        Me.Label3.TabIndex = 30
        Me.Label3.Text = "Activity Type"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(508, 83)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(150, 20)
        Me.Label2.TabIndex = 29
        Me.Label2.Text = "Inside Sales Person"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(260, 83)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(72, 20)
        Me.Label1.TabIndex = 28
        Me.Label1.Text = "Manager"
        '
        'lblResponsible
        '
        Me.lblResponsible.AutoSize = True
        Me.lblResponsible.Location = New System.Drawing.Point(10, 83)
        Me.lblResponsible.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblResponsible.Name = "lblResponsible"
        Me.lblResponsible.Size = New System.Drawing.Size(97, 20)
        Me.lblResponsible.TabIndex = 27
        Me.lblResponsible.Text = "Responsible"
        '
        'cmbResponsible
        '
        Me.cmbResponsible.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbResponsible.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbResponsible.FormattingEnabled = True
        Me.cmbResponsible.Location = New System.Drawing.Point(15, 108)
        Me.cmbResponsible.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbResponsible.Name = "cmbResponsible"
        Me.cmbResponsible.Size = New System.Drawing.Size(238, 28)
        Me.cmbResponsible.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.cmbResponsible, "Responsible Person")
        '
        'cmbManger
        '
        Me.cmbManger.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbManger.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbManger.FormattingEnabled = True
        Me.cmbManger.Location = New System.Drawing.Point(264, 108)
        Me.cmbManger.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbManger.Name = "cmbManger"
        Me.cmbManger.Size = New System.Drawing.Size(238, 28)
        Me.cmbManger.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.cmbManger, "Manager")
        '
        'cmbInside
        '
        Me.cmbInside.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbInside.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbInside.FormattingEnabled = True
        Me.cmbInside.Location = New System.Drawing.Point(513, 108)
        Me.cmbInside.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbInside.Name = "cmbInside"
        Me.cmbInside.Size = New System.Drawing.Size(238, 28)
        Me.cmbInside.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.cmbInside, "Inside Sales")
        '
        'cmbActivityType
        '
        Me.cmbActivityType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbActivityType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbActivityType.FormattingEnabled = True
        Me.cmbActivityType.Location = New System.Drawing.Point(762, 108)
        Me.cmbActivityType.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbActivityType.Name = "cmbActivityType"
        Me.cmbActivityType.Size = New System.Drawing.Size(238, 28)
        Me.cmbActivityType.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.cmbActivityType, "Activity Type")
        '
        'cmbStatus
        '
        Me.cmbStatus.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbStatus.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbStatus.FormattingEnabled = True
        Me.cmbStatus.Location = New System.Drawing.Point(18, 180)
        Me.cmbStatus.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbStatus.Name = "cmbStatus"
        Me.cmbStatus.Size = New System.Drawing.Size(238, 28)
        Me.cmbStatus.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.cmbStatus, "Status")
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd-MMM-yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(267, 182)
        Me.dtpFrom.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(238, 26)
        Me.dtpFrom.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.dtpFrom, "Activity From")
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd-MMM-yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(516, 182)
        Me.dtpTo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(238, 26)
        Me.dtpTo.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.dtpTo, "Activity To")
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(765, 182)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(118, 32)
        Me.btnSearch.TabIndex = 7
        Me.btnSearch.Text = "Search"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'frmActivityHistory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1650, 938)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.dtpTo)
        Me.Controls.Add(Me.dtpFrom)
        Me.Controls.Add(Me.cmbStatus)
        Me.Controls.Add(Me.cmbActivityType)
        Me.Controls.Add(Me.cmbInside)
        Me.Controls.Add(Me.cmbManger)
        Me.Controls.Add(Me.cmbResponsible)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblResponsible)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.grdActivityHistory)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmActivityHistory"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Activity History"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.grdActivityHistory, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents grdActivityHistory As Janus.Windows.GridEX.GridEX
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblResponsible As System.Windows.Forms.Label
    Friend WithEvents cmbResponsible As System.Windows.Forms.ComboBox
    Friend WithEvents cmbManger As System.Windows.Forms.ComboBox
    Friend WithEvents cmbInside As System.Windows.Forms.ComboBox
    Friend WithEvents cmbActivityType As System.Windows.Forms.ComboBox
    Friend WithEvents cmbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
End Class

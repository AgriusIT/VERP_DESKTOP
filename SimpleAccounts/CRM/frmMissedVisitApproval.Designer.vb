<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMissedVisitApproval
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
        Dim grdMissedVisitedApproval_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.grdMissedVisitedApproval = New Janus.Windows.GridEX.GridEX()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.cmbEmployeeName = New System.Windows.Forms.ComboBox()
        Me.lblResponsible = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.btnSearch = New System.Windows.Forms.Button()
        CType(Me.grdMissedVisitedApproval, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdMissedVisitedApproval
        '
        Me.grdMissedVisitedApproval.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdMissedVisitedApproval.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdMissedVisitedApproval.ColumnAutoResize = True
        grdMissedVisitedApproval_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grdMissedVisitedApproval.DesignTimeLayout = grdMissedVisitedApproval_DesignTimeLayout
        Me.grdMissedVisitedApproval.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdMissedVisitedApproval.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdMissedVisitedApproval.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdMissedVisitedApproval.GroupByBoxVisible = False
        Me.grdMissedVisitedApproval.Location = New System.Drawing.Point(18, 302)
        Me.grdMissedVisitedApproval.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdMissedVisitedApproval.Name = "grdMissedVisitedApproval"
        Me.grdMissedVisitedApproval.RecordNavigator = True
        Me.grdMissedVisitedApproval.Size = New System.Drawing.Size(1614, 508)
        Me.grdMissedVisitedApproval.TabIndex = 9
        Me.grdMissedVisitedApproval.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
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
        Me.pnlHeader.TabIndex = 16
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.White
        Me.lblHeader.Location = New System.Drawing.Point(4, 6)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(347, 45)
        Me.lblHeader.TabIndex = 11
        Me.lblHeader.Text = "Missed Visit Approval"
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Nothing
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(1576, 11)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar1.MyGrid = Nothing
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(57, 38)
        Me.CtrlGrdBar1.TabIndex = 7
        Me.CtrlGrdBar1.TabStop = False
        '
        'cmbEmployeeName
        '
        Me.cmbEmployeeName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbEmployeeName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbEmployeeName.FormattingEnabled = True
        Me.cmbEmployeeName.Location = New System.Drawing.Point(18, 108)
        Me.cmbEmployeeName.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbEmployeeName.Name = "cmbEmployeeName"
        Me.cmbEmployeeName.Size = New System.Drawing.Size(238, 28)
        Me.cmbEmployeeName.TabIndex = 17
        '
        'lblResponsible
        '
        Me.lblResponsible.AutoSize = True
        Me.lblResponsible.Location = New System.Drawing.Point(18, 83)
        Me.lblResponsible.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblResponsible.Name = "lblResponsible"
        Me.lblResponsible.Size = New System.Drawing.Size(125, 20)
        Me.lblResponsible.TabIndex = 28
        Me.lblResponsible.Text = "Employee Name"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(267, 109)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(238, 26)
        Me.TextBox1.TabIndex = 29
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(262, 85)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 20)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "Status"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(518, 112)
        Me.CheckBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(120, 24)
        Me.CheckBox1.TabIndex = 31
        Me.CheckBox1.Text = "Is Justified?"
        Me.CheckBox1.UseVisualStyleBackColor = True
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(18, 151)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TextBox2.Multiline = True
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(487, 96)
        Me.TextBox2.TabIndex = 32
        '
        'btnSearch
        '
        Me.btnSearch.Location = New System.Drawing.Point(520, 180)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(118, 32)
        Me.btnSearch.TabIndex = 33
        Me.btnSearch.Text = "Save"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'frmMissedVisitApproval
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1650, 938)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.CheckBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.lblResponsible)
        Me.Controls.Add(Me.cmbEmployeeName)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.grdMissedVisitedApproval)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmMissedVisitApproval"
        Me.Text = "frmMissedVisitApproval"
        CType(Me.grdMissedVisitedApproval, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdMissedVisitedApproval As Janus.Windows.GridEX.GridEX
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents cmbEmployeeName As System.Windows.Forms.ComboBox
    Friend WithEvents lblResponsible As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
End Class

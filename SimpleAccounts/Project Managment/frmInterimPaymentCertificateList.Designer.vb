<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInterimPaymentCertificateList
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInterimPaymentCertificateList))
        Dim grdItermList_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.grdItermList = New Janus.Windows.GridEX.GridEX()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.pnlHeader.SuspendLayout()
        CType(Me.grdItermList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(215, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnRefresh)
        Me.pnlHeader.Controls.Add(Me.btnAdd)
        Me.pnlHeader.Controls.Add(Me.CtrlGrdBar1)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1008, 38)
        Me.pnlHeader.TabIndex = 17
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.BackgroundImage = CType(resources.GetObject("btnRefresh.BackgroundImage"), System.Drawing.Image)
        Me.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnRefresh.FlatAppearance.BorderSize = 0
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.Location = New System.Drawing.Point(931, 4)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(30, 26)
        Me.btnRefresh.TabIndex = 18
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAdd.BackColor = System.Drawing.Color.Transparent
        Me.btnAdd.BackgroundImage = CType(resources.GetObject("btnAdd.BackgroundImage"), System.Drawing.Image)
        Me.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnAdd.FlatAppearance.BorderSize = 0
        Me.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAdd.Location = New System.Drawing.Point(902, 8)
        Me.btnAdd.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(23, 26)
        Me.btnAdd.TabIndex = 17
        Me.btnAdd.UseVisualStyleBackColor = False
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(967, 6)
        Me.CtrlGrdBar1.MyGrid = Me.grdItermList
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(38, 25)
        Me.CtrlGrdBar1.TabIndex = 16
        Me.CtrlGrdBar1.TabStop = False
        '
        'grdItermList
        '
        Me.grdItermList.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        grdItermList_DesignTimeLayout.LayoutString = "<GridEXLayoutData><RootTable><GroupCondition /></RootTable></GridEXLayoutData>"
        Me.grdItermList.DesignTimeLayout = grdItermList_DesignTimeLayout
        Me.grdItermList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdItermList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdItermList.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdItermList.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdItermList.GroupByBoxVisible = False
        Me.grdItermList.Location = New System.Drawing.Point(0, 38)
        Me.grdItermList.Name = "grdItermList"
        Me.grdItermList.RecordNavigator = True
        Me.grdItermList.Size = New System.Drawing.Size(1008, 691)
        Me.grdItermList.TabIndex = 18
        Me.grdItermList.TreeLineColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.grdItermList.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.White
        Me.lblHeader.Location = New System.Drawing.Point(12, 4)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(283, 30)
        Me.lblHeader.TabIndex = 7
        Me.lblHeader.Text = "Interim Payment Certificate"
        '
        'frmInterimPaymentCertificateList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1008, 729)
        Me.Controls.Add(Me.grdItermList)
        Me.Controls.Add(Me.pnlHeader)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmInterimPaymentCertificateList"
        Me.Text = "Interim Payment Certificate"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.grdItermList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents grdItermList As Janus.Windows.GridEX.GridEX
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnRefresh As System.Windows.Forms.Button
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLogicalBifurcationList
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
        Dim grdBifurcation_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLogicalBifurcationList))
        Me.grdBifurcation = New Janus.Windows.GridEX.GridEX()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.CtrlGrdBar3 = New SimpleAccounts.CtrlGrdBar()
        Me.btnAddDock = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        CType(Me.grdBifurcation, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'grdBifurcation
        '
        Me.grdBifurcation.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdBifurcation.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdBifurcation_DesignTimeLayout.LayoutString = resources.GetString("grdBifurcation_DesignTimeLayout.LayoutString")
        Me.grdBifurcation.DesignTimeLayout = grdBifurcation_DesignTimeLayout
        Me.grdBifurcation.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdBifurcation.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdBifurcation.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdBifurcation.GroupByBoxVisible = False
        Me.grdBifurcation.Location = New System.Drawing.Point(0, 75)
        Me.grdBifurcation.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdBifurcation.Name = "grdBifurcation"
        Me.grdBifurcation.RecordNavigator = True
        Me.grdBifurcation.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both
        Me.grdBifurcation.Size = New System.Drawing.Size(1636, 618)
        Me.grdBifurcation.TabIndex = 18
        Me.grdBifurcation.TreeLineColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.grdBifurcation.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.CtrlGrdBar3)
        Me.pnlHeader.Controls.Add(Me.btnAddDock)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1641, 71)
        Me.pnlHeader.TabIndex = 19
        '
        'CtrlGrdBar3
        '
        Me.CtrlGrdBar3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar3.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBar3.Email = Nothing
        Me.CtrlGrdBar3.FormName = Me
        Me.CtrlGrdBar3.Location = New System.Drawing.Point(1544, 14)
        Me.CtrlGrdBar3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar3.MyGrid = Me.grdBifurcation
        Me.CtrlGrdBar3.Name = "CtrlGrdBar3"
        Me.CtrlGrdBar3.Size = New System.Drawing.Size(51, 38)
        Me.CtrlGrdBar3.TabIndex = 8
        '
        'btnAddDock
        '
        Me.btnAddDock.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddDock.BackColor = System.Drawing.Color.Transparent
        Me.btnAddDock.BackgroundImage = CType(resources.GetObject("btnAddDock.BackgroundImage"), System.Drawing.Image)
        Me.btnAddDock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAddDock.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddDock.Location = New System.Drawing.Point(1606, 18)
        Me.btnAddDock.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnAddDock.Name = "btnAddDock"
        Me.btnAddDock.Size = New System.Drawing.Size(30, 31)
        Me.btnAddDock.TabIndex = 7
        Me.btnAddDock.UseVisualStyleBackColor = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(13, 18)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(282, 36)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Logical Bifurcation"
        '
        'frmLogicalBifurcationList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1641, 700)
        Me.Controls.Add(Me.grdBifurcation)
        Me.Controls.Add(Me.pnlHeader)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmLogicalBifurcationList"
        Me.Text = "Logical Bifurcation"
        CType(Me.grdBifurcation, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grdBifurcation As Janus.Windows.GridEX.GridEX
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents CtrlGrdBar3 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents btnAddDock As System.Windows.Forms.Button
    Friend WithEvents lblHeader As System.Windows.Forms.Label
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmProAgentList
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
        Dim grdAgentList_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmProAgentList))
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.grdAgentList = New Janus.Windows.GridEX.GridEX()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.btnAddDock = New System.Windows.Forms.Button()
        Me.pnlHeader.SuspendLayout()
        CType(Me.grdAgentList, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.White
        Me.pnlHeader.Controls.Add(Me.CtrlGrdBar1)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Controls.Add(Me.btnAddDock)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1100, 38)
        Me.pnlHeader.TabIndex = 13
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.Color.Transparent
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(1024, 6)
        Me.CtrlGrdBar1.MyGrid = Me.grdAgentList
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(38, 25)
        Me.CtrlGrdBar1.TabIndex = 17
        Me.CtrlGrdBar1.TabStop = False
        '
        'grdAgentList
        '
        Me.grdAgentList.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        grdAgentList_DesignTimeLayout.LayoutString = resources.GetString("grdAgentList_DesignTimeLayout.LayoutString")
        Me.grdAgentList.DesignTimeLayout = grdAgentList_DesignTimeLayout
        Me.grdAgentList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdAgentList.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdAgentList.GroupByBoxVisible = False
        Me.grdAgentList.Location = New System.Drawing.Point(0, 38)
        Me.grdAgentList.Name = "grdAgentList"
        Me.grdAgentList.RecordNavigator = True
        Me.grdAgentList.ScrollBars = Janus.Windows.GridEX.ScrollBars.Both
        Me.grdAgentList.Size = New System.Drawing.Size(1100, 572)
        Me.grdAgentList.TabIndex = 14
        Me.grdAgentList.TreeLineColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.grdAgentList.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Segoe UI", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer), CType(CType(68, Byte), Integer))
        Me.lblHeader.Location = New System.Drawing.Point(12, 4)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(82, 30)
        Me.lblHeader.TabIndex = 7
        Me.lblHeader.Text = "Agents"
        '
        'btnAddDock
        '
        Me.btnAddDock.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnAddDock.BackColor = System.Drawing.Color.Transparent
        Me.btnAddDock.BackgroundImage = CType(resources.GetObject("btnAddDock.BackgroundImage"), System.Drawing.Image)
        Me.btnAddDock.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAddDock.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAddDock.Location = New System.Drawing.Point(1068, 8)
        Me.btnAddDock.Name = "btnAddDock"
        Me.btnAddDock.Size = New System.Drawing.Size(20, 20)
        Me.btnAddDock.TabIndex = 6
        Me.btnAddDock.UseVisualStyleBackColor = False
        '
        'frmProAgentList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1100, 610)
        Me.Controls.Add(Me.grdAgentList)
        Me.Controls.Add(Me.pnlHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmProAgentList"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Agent List"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        CType(Me.grdAgentList, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents btnAddDock As System.Windows.Forms.Button
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents grdAgentList As Janus.Windows.GridEX.GridEX
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
End Class

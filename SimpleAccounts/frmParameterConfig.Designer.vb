<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmParameterConfig
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
        Dim grdParametersConfig_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmParameterConfig))
        Dim grdSaved_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblObservationSample = New System.Windows.Forms.Label()
        Me.cmbParameterName = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.grdParametersConfig = New Janus.Windows.GridEX.GridEX()
        Me.cmbProductName = New System.Windows.Forms.ComboBox()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdSaved = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.btnHelp = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.grdParametersConfig, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.Panel1)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbParameterName)
        Me.UltraTabPageControl1.Controls.Add(Me.Label2)
        Me.UltraTabPageControl1.Controls.Add(Me.grdParametersConfig)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbProductName)
        Me.UltraTabPageControl1.Controls.Add(Me.btnAdd)
        Me.UltraTabPageControl1.Controls.Add(Me.Label1)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(674, 438)
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.Panel1.Controls.Add(Me.lblObservationSample)
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(675, 47)
        Me.Panel1.TabIndex = 37
        '
        'lblObservationSample
        '
        Me.lblObservationSample.AutoSize = True
        Me.lblObservationSample.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblObservationSample.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(242, Byte), Integer))
        Me.lblObservationSample.Location = New System.Drawing.Point(3, 12)
        Me.lblObservationSample.Name = "lblObservationSample"
        Me.lblObservationSample.Size = New System.Drawing.Size(379, 23)
        Me.lblObservationSample.TabIndex = 4
        Me.lblObservationSample.Text = "Product Parameters Configuration"
        '
        'cmbParameterName
        '
        Me.cmbParameterName.FormattingEnabled = True
        Me.cmbParameterName.Location = New System.Drawing.Point(96, 80)
        Me.cmbParameterName.Name = "cmbParameterName"
        Me.cmbParameterName.Size = New System.Drawing.Size(199, 21)
        Me.cmbParameterName.TabIndex = 36
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(4, 56)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(75, 13)
        Me.Label2.TabIndex = 30
        Me.Label2.Text = "Product Name"
        '
        'grdParametersConfig
        '
        Me.grdParametersConfig.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdParametersConfig.AutoEdit = True
        Me.grdParametersConfig.ColumnAutoResize = True
        grdParametersConfig_DesignTimeLayout.LayoutString = resources.GetString("grdParametersConfig_DesignTimeLayout.LayoutString")
        Me.grdParametersConfig.DesignTimeLayout = grdParametersConfig_DesignTimeLayout
        Me.grdParametersConfig.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdParametersConfig.Location = New System.Drawing.Point(-1, 107)
        Me.grdParametersConfig.Name = "grdParametersConfig"
        Me.grdParametersConfig.Size = New System.Drawing.Size(676, 331)
        Me.grdParametersConfig.TabIndex = 35
        Me.grdParametersConfig.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'cmbProductName
        '
        Me.cmbProductName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbProductName.FormattingEnabled = True
        Me.cmbProductName.Location = New System.Drawing.Point(96, 53)
        Me.cmbProductName.Name = "cmbProductName"
        Me.cmbProductName.Size = New System.Drawing.Size(199, 21)
        Me.cmbProductName.TabIndex = 29
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(301, 79)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(55, 21)
        Me.btnAdd.TabIndex = 34
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(4, 83)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "Parameter Name"
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grdSaved)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(674, 438)
        '
        'grdSaved
        '
        Me.grdSaved.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdSaved.AutoEdit = True
        Me.grdSaved.ColumnAutoResize = True
        grdSaved_DesignTimeLayout.LayoutString = resources.GetString("grdSaved_DesignTimeLayout.LayoutString")
        Me.grdSaved.DesignTimeLayout = grdSaved_DesignTimeLayout
        Me.grdSaved.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grdSaved.Location = New System.Drawing.Point(-1, 0)
        Me.grdSaved.Name = "grdSaved"
        Me.grdSaved.Size = New System.Drawing.Size(676, 438)
        Me.grdSaved.TabIndex = 36
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'ToolStrip1
        '
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnEdit, Me.btnSave, Me.btnDelete, Me.btnRefresh, Me.btnHelp, Me.ToolStripSeparator1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(676, 24)
        Me.ToolStrip1.TabIndex = 31
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(51, 21)
        Me.btnNew.Text = "&New"
        '
        'btnEdit
        '
        Me.btnEdit.Image = CType(resources.GetObject("btnEdit.Image"), System.Drawing.Image)
        Me.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(47, 21)
        Me.btnEdit.Text = "&Edit"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(51, 21)
        Me.btnSave.Text = "&Save"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = CType(resources.GetObject("btnRefresh.Image"), System.Drawing.Image)
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(66, 21)
        Me.btnRefresh.Text = "&Refresh"
        '
        'btnHelp
        '
        Me.btnHelp.Image = CType(resources.GetObject("btnHelp.Image"), System.Drawing.Image)
        Me.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnHelp.Name = "btnHelp"
        Me.btnHelp.Size = New System.Drawing.Size(52, 21)
        Me.btnHelp.Text = "&Help"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 24)
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 27)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(676, 461)
        Me.UltraTabControl1.TabIndex = 36
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Criteria"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "History"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(674, 438)
        '
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(60, 21)
        Me.btnDelete.Text = "&Delete"
        '
        'frmParameterConfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(676, 485)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmParameterConfig"
        Me.Text = "Parameter Configuration"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.grdParametersConfig, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbProductName As System.Windows.Forms.ComboBox
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnHelp As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents grdParametersConfig As Janus.Windows.GridEX.GridEX
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grdSaved As Janus.Windows.GridEX.GridEX
    Friend WithEvents cmbParameterName As System.Windows.Forms.ComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblObservationSample As System.Windows.Forms.Label
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
End Class

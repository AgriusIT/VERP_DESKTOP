<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSearchMenu
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
        Dim grdMenuforms_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSearchMenu))
        Me.grdMenuforms = New Janus.Windows.GridEX.GridEX()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblProgress = New System.Windows.Forms.Label()
        CType(Me.grdMenuforms, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grdMenuforms
        '
        Me.grdMenuforms.AllowDrop = True
        Me.grdMenuforms.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        grdMenuforms_DesignTimeLayout.LayoutString = resources.GetString("grdMenuforms_DesignTimeLayout.LayoutString")
        Me.grdMenuforms.DesignTimeLayout = grdMenuforms_DesignTimeLayout
        Me.grdMenuforms.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdMenuforms.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.None
        Me.grdMenuforms.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.grdMenuforms.GroupByBoxVisible = False
        Me.grdMenuforms.GroupRowVisualStyle = Janus.Windows.GridEX.GroupRowVisualStyle.Outlook2003
        Me.grdMenuforms.Location = New System.Drawing.Point(0, 26)
        Me.grdMenuforms.Name = "grdMenuforms"
        Me.grdMenuforms.RecordNavigator = True
        Me.grdMenuforms.Size = New System.Drawing.Size(539, 386)
        Me.grdMenuforms.TabIndex = 1
        Me.grdMenuforms.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'txtSearch
        '
        Me.txtSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.txtSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource
        Me.txtSearch.Dock = System.Windows.Forms.DockStyle.Top
        Me.txtSearch.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtSearch.Location = New System.Drawing.Point(0, 0)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(539, 26)
        Me.txtSearch.TabIndex = 0
        '
        'lblProgress
        '
        Me.lblProgress.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(139, 167)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 13
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'frmSearchMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.DarkRed
        Me.ClientSize = New System.Drawing.Size(539, 412)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.grdMenuforms)
        Me.Controls.Add(Me.txtSearch)
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(319, 85)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSearchMenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        CType(Me.grdMenuforms, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdMenuforms As Janus.Windows.GridEX.GridEX
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents lblProgress As System.Windows.Forms.Label
End Class

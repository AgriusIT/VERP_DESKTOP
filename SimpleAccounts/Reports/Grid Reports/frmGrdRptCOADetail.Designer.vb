<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGrdRptCOADetail
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
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.UltraTree1 = New Infragistics.Win.UltraWinTree.UltraTree()
        Me.UiPanelManager1 = New Janus.Windows.UI.Dock.UIPanelManager(Me.components)
        Me.uiPanel0 = New Janus.Windows.UI.Dock.UIPanel()
        Me.uiPanel0Container = New Janus.Windows.UI.Dock.UIPanelInnerContainer()
        CType(Me.UltraTree1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UiPanelManager1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.uiPanel0, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.uiPanel0.SuspendLayout()
        Me.uiPanel0Container.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(730, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'UltraTree1
        '
        Me.UltraTree1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTree1.Location = New System.Drawing.Point(0, 0)
        Me.UltraTree1.Name = "UltraTree1"
        Me.UltraTree1.Size = New System.Drawing.Size(722, 467)
        Me.UltraTree1.TabIndex = 5
        Me.UltraTree1.ViewStyle = Infragistics.Win.UltraWinTree.ViewStyle.OutlookExpress
        '
        'UiPanelManager1
        '
        Me.UiPanelManager1.ContainerControl = Me
        Me.UiPanelManager1.Tag = Nothing
        Me.UiPanelManager1.VisualStyle = Janus.Windows.UI.Dock.PanelVisualStyle.Office2007
        Me.uiPanel0.Id = New System.Guid("2efbc4ef-e287-4b6e-b8c5-cbdf38ec60ea")
        Me.UiPanelManager1.Panels.Add(Me.uiPanel0)
        '
        'Design Time Panel Info:
        '
        Me.UiPanelManager1.BeginPanelInfo()
        Me.UiPanelManager1.AddDockPanelInfo(New System.Guid("2efbc4ef-e287-4b6e-b8c5-cbdf38ec60ea"), Janus.Windows.UI.Dock.PanelDockStyle.Fill, New System.Drawing.Size(724, 491), True)
        Me.UiPanelManager1.AddFloatingPanelInfo(New System.Guid("2efbc4ef-e287-4b6e-b8c5-cbdf38ec60ea"), New System.Drawing.Point(-1, -1), New System.Drawing.Size(-1, -1), False)
        Me.UiPanelManager1.EndPanelInfo()
        '
        'uiPanel0
        '
        Me.uiPanel0.CaptionFormatStyle.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.uiPanel0.CloseButtonVisible = Janus.Windows.UI.InheritableBoolean.[False]
        Me.uiPanel0.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.uiPanel0.InnerContainer = Me.uiPanel0Container
        Me.uiPanel0.Location = New System.Drawing.Point(3, 28)
        Me.uiPanel0.Name = "uiPanel0"
        Me.uiPanel0.Size = New System.Drawing.Size(724, 491)
        Me.uiPanel0.TabIndex = 4
        Me.uiPanel0.Text = "Chart of Account Report"
        '
        'uiPanel0Container
        '
        Me.uiPanel0Container.Controls.Add(Me.UltraTree1)
        Me.uiPanel0Container.Location = New System.Drawing.Point(1, 23)
        Me.uiPanel0Container.Name = "uiPanel0Container"
        Me.uiPanel0Container.Size = New System.Drawing.Size(722, 467)
        Me.uiPanel0Container.TabIndex = 0
        '
        'frmGrdRptCOADetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(730, 522)
        Me.Controls.Add(Me.uiPanel0)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Name = "frmGrdRptCOADetail"
        Me.Text = "Charto of Account"
        CType(Me.UltraTree1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UiPanelManager1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.uiPanel0, System.ComponentModel.ISupportInitialize).EndInit()
        Me.uiPanel0.ResumeLayout(False)
        Me.uiPanel0Container.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents UiPanelManager1 As Janus.Windows.UI.Dock.UIPanelManager
    Friend WithEvents uiPanel0 As Janus.Windows.UI.Dock.UIPanel
    Friend WithEvents uiPanel0Container As Janus.Windows.UI.Dock.UIPanelInnerContainer
    Friend WithEvents UltraTree1 As Infragistics.Win.UltraWinTree.UltraTree

End Class

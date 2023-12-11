<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CtrlGrdBar
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(CtrlGrdBar))
        Me.GridEXPrintDocument1 = New Janus.Windows.GridEX.GridEXPrintDocument()
        Me.GridEXExporter1 = New Janus.Windows.GridEX.Export.GridEXExporter(Me.components)
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog()
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnGridControl = New System.Windows.Forms.ToolStripSplitButton()
        Me.mGridPrint = New System.Windows.Forms.ToolStripMenuItem()
        Me.mGridExport = New System.Windows.Forms.ToolStripMenuItem()
        Me.txtGridTitle = New System.Windows.Forms.ToolStripTextBox()
        Me.mGridChooseFielder = New System.Windows.Forms.ToolStripMenuItem()
        Me.mGridSaveLayouts = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupCollaps = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupExpand = New System.Windows.Forms.ToolStripMenuItem()
        Me.GridResizeColumnToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SendEmailToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GridEXPrintDocument1
        '
        Me.GridEXPrintDocument1.FitColumns = Janus.Windows.GridEX.FitColumnsMode.Zooming
        '
        'PrintPreviewDialog1
        '
        Me.PrintPreviewDialog1.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.PrintPreviewDialog1.ClientSize = New System.Drawing.Size(400, 300)
        Me.PrintPreviewDialog1.Document = Me.GridEXPrintDocument1
        Me.PrintPreviewDialog1.Enabled = True
        Me.PrintPreviewDialog1.Icon = CType(resources.GetObject("PrintPreviewDialog1.Icon"), System.Drawing.Icon)
        Me.PrintPreviewDialog1.Name = "PrintPreviewDialog1"
        Me.PrintPreviewDialog1.Visible = False
        '
        'PageSetupDialog1
        '
        Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
        '
        'ToolStrip1
        '
        Me.ToolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnGridControl})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(38, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnGridControl
        '
        Me.btnGridControl.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnGridControl.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mGridPrint, Me.mGridExport, Me.txtGridTitle, Me.mGridChooseFielder, Me.mGridSaveLayouts, Me.GroupCollaps, Me.GroupExpand, Me.GridResizeColumnToolStripMenuItem, Me.SendEmailToolStripMenuItem})
        Me.btnGridControl.Image = CType(resources.GetObject("btnGridControl.Image"), System.Drawing.Image)
        Me.btnGridControl.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnGridControl.Name = "btnGridControl"
        Me.btnGridControl.Size = New System.Drawing.Size(32, 20)
        Me.btnGridControl.Text = "Grid Control"
        '
        'mGridPrint
        '
        Me.mGridPrint.Image = CType(resources.GetObject("mGridPrint.Image"), System.Drawing.Image)
        Me.mGridPrint.Name = "mGridPrint"
        Me.mGridPrint.Size = New System.Drawing.Size(183, 22)
        Me.mGridPrint.Text = "Print"
        '
        'mGridExport
        '
        Me.mGridExport.Image = CType(resources.GetObject("mGridExport.Image"), System.Drawing.Image)
        Me.mGridExport.Name = "mGridExport"
        Me.mGridExport.Size = New System.Drawing.Size(183, 22)
        Me.mGridExport.Text = "Export"
        '
        'txtGridTitle
        '
        Me.txtGridTitle.Name = "txtGridTitle"
        Me.txtGridTitle.Size = New System.Drawing.Size(100, 23)
        '
        'mGridChooseFielder
        '
        Me.mGridChooseFielder.Image = CType(resources.GetObject("mGridChooseFielder.Image"), System.Drawing.Image)
        Me.mGridChooseFielder.Name = "mGridChooseFielder"
        Me.mGridChooseFielder.Size = New System.Drawing.Size(183, 22)
        Me.mGridChooseFielder.Text = "Field Chooser"
        '
        'mGridSaveLayouts
        '
        Me.mGridSaveLayouts.Image = CType(resources.GetObject("mGridSaveLayouts.Image"), System.Drawing.Image)
        Me.mGridSaveLayouts.Name = "mGridSaveLayouts"
        Me.mGridSaveLayouts.Size = New System.Drawing.Size(183, 22)
        Me.mGridSaveLayouts.Text = "Save Layout"
        '
        'GroupCollaps
        '
        Me.GroupCollaps.Image = Global.SimpleAccounts.My.Resources.Resources.toggle_collapse1
        Me.GroupCollaps.Name = "GroupCollaps"
        Me.GroupCollaps.Size = New System.Drawing.Size(183, 22)
        Me.GroupCollaps.Text = "Group Collapse"
        '
        'GroupExpand
        '
        Me.GroupExpand.Image = Global.SimpleAccounts.My.Resources.Resources.expand_icon
        Me.GroupExpand.Name = "GroupExpand"
        Me.GroupExpand.Size = New System.Drawing.Size(183, 22)
        Me.GroupExpand.Text = "Group Expand"
        '
        'GridResizeColumnToolStripMenuItem
        '
        Me.GridResizeColumnToolStripMenuItem.Image = Global.SimpleAccounts.My.Resources.Resources.go_last
        Me.GridResizeColumnToolStripMenuItem.Name = "GridResizeColumnToolStripMenuItem"
        Me.GridResizeColumnToolStripMenuItem.Size = New System.Drawing.Size(183, 22)
        Me.GridResizeColumnToolStripMenuItem.Text = "Auto Adjust Column"
        '
        'SendEmailToolStripMenuItem
        '
        Me.SendEmailToolStripMenuItem.Image = Global.SimpleAccounts.My.Resources.Resources.Email_Envelope
        Me.SendEmailToolStripMenuItem.Name = "SendEmailToolStripMenuItem"
        Me.SendEmailToolStripMenuItem.Size = New System.Drawing.Size(183, 22)
        Me.SendEmailToolStripMenuItem.Text = "Send Email"
        '
        'CtrlGrdBar
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "CtrlGrdBar"
        Me.Size = New System.Drawing.Size(38, 25)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GridEXPrintDocument1 As Janus.Windows.GridEX.GridEXPrintDocument
    Friend WithEvents GridEXExporter1 As Janus.Windows.GridEX.Export.GridEXExporter
    Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnGridControl As System.Windows.Forms.ToolStripSplitButton
    Friend WithEvents mGridChooseFielder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mGridSaveLayouts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mGridExport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mGridPrint As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents GroupCollaps As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GridResizeColumnToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents txtGridTitle As System.Windows.Forms.ToolStripTextBox
    Friend WithEvents GroupExpand As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SendEmailToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class

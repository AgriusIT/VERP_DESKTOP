<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmNotification
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
        Dim ListViewGroup4 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Current", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup5 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Yesterday", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewGroup6 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("Old", System.Windows.Forms.HorizontalAlignment.Left)
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmNotification))
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.lblCloseReport = New System.Windows.Forms.ToolStripButton()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ImageList2 = New System.Windows.Forms.ImageList(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.bgwGetPendingNotification = New System.ComponentModel.BackgroundWorker()
        Me.bgwGetNotifications = New System.ComponentModel.BackgroundWorker()
        Me.AgriusNotificationCenter = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.pnlHeader.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(465, 77)
        Me.pnlHeader.TabIndex = 24
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(4, 20)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(210, 41)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Notifications"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.HelpToolStripButton, Me.ToolStripSeparator1, Me.lblCloseReport})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 77)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(465, 38)
        Me.ToolStrip1.TabIndex = 25
        Me.ToolStrip1.Text = "ToolStrip1"
        Me.ToolStrip1.Visible = False
        '
        'btnNew
        '
        Me.btnNew.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(98, 35)
        Me.btnNew.Text = "&Refresh"
        '
        'HelpToolStripButton
        '
        Me.HelpToolStripButton.Image = Global.SimpleAccounts.My.Resources.Resources.ok
        Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton.Name = "HelpToolStripButton"
        Me.HelpToolStripButton.Size = New System.Drawing.Size(104, 35)
        Me.HelpToolStripButton.Text = "Clear All"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 38)
        '
        'lblCloseReport
        '
        Me.lblCloseReport.Image = Global.SimpleAccounts.My.Resources.Resources.cross_icon
        Me.lblCloseReport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.lblCloseReport.Name = "lblCloseReport"
        Me.lblCloseReport.Size = New System.Drawing.Size(83, 35)
        Me.lblCloseReport.Text = "&Close"
        '
        'ListView1
        '
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1})
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        ListViewGroup4.Header = "Current"
        ListViewGroup4.Name = "Current"
        ListViewGroup5.Header = "Yesterday"
        ListViewGroup5.Name = "Yesterday"
        ListViewGroup6.Header = "Old"
        ListViewGroup6.Name = "Old"
        Me.ListView1.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup4, ListViewGroup5, ListViewGroup6})
        Me.ListView1.LargeImageList = Me.ImageList1
        Me.ListView1.Location = New System.Drawing.Point(0, 77)
        Me.ListView1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(465, 643)
        Me.ListView1.SmallImageList = Me.ImageList1
        Me.ListView1.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.ListView1.TabIndex = 27
        Me.ListView1.TileSize = New System.Drawing.Size(300, 50)
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Tile
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "accounts.png")
        Me.ImageList1.Images.SetKeyName(1, "administration.png")
        Me.ImageList1.Images.SetKeyName(2, "asset management.png")
        Me.ImageList1.Images.SetKeyName(3, "cash.png")
        Me.ImageList1.Images.SetKeyName(4, "configuration.png")
        Me.ImageList1.Images.SetKeyName(5, "crm.png")
        Me.ImageList1.Images.SetKeyName(6, "hr 3.png")
        Me.ImageList1.Images.SetKeyName(7, "hr.png")
        Me.ImageList1.Images.SetKeyName(8, "human resourse.png")
        Me.ImageList1.Images.SetKeyName(9, "import.png")
        Me.ImageList1.Images.SetKeyName(10, "inventory.png")
        Me.ImageList1.Images.SetKeyName(11, "reportss.png")
        Me.ImageList1.Images.SetKeyName(12, "sales.png")
        Me.ImageList1.Images.SetKeyName(13, "utilities.png")
        '
        'ImageList2
        '
        Me.ImageList2.ImageStream = CType(resources.GetObject("ImageList2.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList2.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList2.Images.SetKeyName(0, "accounts.png")
        Me.ImageList2.Images.SetKeyName(1, "administration.png")
        Me.ImageList2.Images.SetKeyName(2, "asset management.png")
        Me.ImageList2.Images.SetKeyName(3, "cash.png")
        Me.ImageList2.Images.SetKeyName(4, "configuration.png")
        Me.ImageList2.Images.SetKeyName(5, "crm.png")
        Me.ImageList2.Images.SetKeyName(6, "hr.png")
        Me.ImageList2.Images.SetKeyName(7, "imports.png")
        Me.ImageList2.Images.SetKeyName(8, "inventory.png")
        Me.ImageList2.Images.SetKeyName(9, "production.png")
        Me.ImageList2.Images.SetKeyName(10, "purchase.png")
        Me.ImageList2.Images.SetKeyName(11, "reports.png")
        Me.ImageList2.Images.SetKeyName(12, "sales.png")
        Me.ImageList2.Images.SetKeyName(13, "utilities.png")
        '
        'Timer1
        '
        Me.Timer1.Enabled = True
        '
        'bgwGetPendingNotification
        '
        '
        'bgwGetNotifications
        '
        '
        'AgriusNotificationCenter
        '
        Me.AgriusNotificationCenter.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info
        Me.AgriusNotificationCenter.BalloonTipTitle = "V ERP"
        Me.AgriusNotificationCenter.ContextMenuStrip = Me.ContextMenuStrip1
        Me.AgriusNotificationCenter.Icon = CType(resources.GetObject("AgriusNotificationCenter.Icon"), System.Drawing.Icon)
        Me.AgriusNotificationCenter.Text = "Welcome to V ERP"
        Me.AgriusNotificationCenter.Visible = True
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripMenuItem1})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(112, 34)
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(111, 30)
        Me.ToolStripMenuItem1.Text = "E&xit"
        Me.ToolStripMenuItem1.ToolTipText = "Click to close Agrius ERP"
        '
        'frmNotification
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(465, 720)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.pnlHeader)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmNotification"
        Me.Text = "Notifications"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents lblCloseReport As System.Windows.Forms.ToolStripButton
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents ImageList2 As System.Windows.Forms.ImageList
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents bgwGetPendingNotification As System.ComponentModel.BackgroundWorker
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents bgwGetNotifications As System.ComponentModel.BackgroundWorker
    Friend WithEvents AgriusNotificationCenter As System.Windows.Forms.NotifyIcon
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
End Class

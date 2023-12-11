<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmToDoList
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
        Dim ListViewGroup1 As System.Windows.Forms.ListViewGroup = New System.Windows.Forms.ListViewGroup("High Priority", System.Windows.Forms.HorizontalAlignment.Left)
        Dim ListViewItem1 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("New Task: Develop new task application", 0)
        Dim ListViewItem2 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Critical: You have not backed up your data since 2 weeks", 3)
        Dim ListViewItem3 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Test: Test notification", 4)
        Dim ListViewItem4 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Test Old Items", 7)
        Dim ListViewItem5 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Test Old Items, Test Old Items, Test Old Items, Test Old Items", 5)
        Dim ListViewItem6 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Test Old Items", 2)
        Dim ListViewItem7 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Test Old Items", 5)
        Dim ListViewItem8 As System.Windows.Forms.ListViewItem = New System.Windows.Forms.ListViewItem("Test Old Items", 1)
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmToDoList))
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.lblCloseReport = New System.Windows.Forms.ToolStripButton()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.TaskTitle = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ImageList2 = New System.Windows.Forms.ImageList(Me.components)
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton()
        Me.pnlHeader.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 25)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(310, 50)
        Me.pnlHeader.TabIndex = 28
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.lblHeader.Location = New System.Drawing.Point(3, 13)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(101, 23)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Task list"
        '
        'btnNew
        '
        Me.btnNew.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(66, 22)
        Me.btnNew.Text = "&Refresh"
        '
        'HelpToolStripButton
        '
        Me.HelpToolStripButton.Image = Global.SimpleAccounts.My.Resources.Resources.ok
        Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton.Name = "HelpToolStripButton"
        Me.HelpToolStripButton.Size = New System.Drawing.Size(71, 22)
        Me.HelpToolStripButton.Text = "Clear All"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'lblCloseReport
        '
        Me.lblCloseReport.Image = Global.SimpleAccounts.My.Resources.Resources.cross_icon
        Me.lblCloseReport.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.lblCloseReport.Name = "lblCloseReport"
        Me.lblCloseReport.Size = New System.Drawing.Size(56, 22)
        Me.lblCloseReport.Text = "&Close"
        '
        'ListView1
        '
        Me.ListView1.CheckBoxes = True
        Me.ListView1.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.TaskTitle})
        Me.ListView1.Dock = System.Windows.Forms.DockStyle.Fill
        ListViewGroup1.Header = "High Priority"
        ListViewGroup1.Name = "High Priority"
        Me.ListView1.Groups.AddRange(New System.Windows.Forms.ListViewGroup() {ListViewGroup1})
        ListViewItem1.Group = ListViewGroup1
        ListViewItem1.StateImageIndex = 0
        ListViewItem2.Group = ListViewGroup1
        ListViewItem2.StateImageIndex = 0
        ListViewItem3.Group = ListViewGroup1
        ListViewItem3.StateImageIndex = 0
        ListViewItem4.Group = ListViewGroup1
        ListViewItem4.StateImageIndex = 0
        ListViewItem5.StateImageIndex = 0
        ListViewItem6.StateImageIndex = 0
        ListViewItem7.Group = ListViewGroup1
        ListViewItem7.StateImageIndex = 0
        ListViewItem8.StateImageIndex = 0
        Me.ListView1.Items.AddRange(New System.Windows.Forms.ListViewItem() {ListViewItem1, ListViewItem2, ListViewItem3, ListViewItem4, ListViewItem5, ListViewItem6, ListViewItem7, ListViewItem8})
        Me.ListView1.Location = New System.Drawing.Point(0, 75)
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(310, 393)
        Me.ListView1.TabIndex = 30
        Me.ListView1.UseCompatibleStateImageBehavior = False
        Me.ListView1.View = System.Windows.Forms.View.Details
        '
        'TaskTitle
        '
        Me.TaskTitle.Text = "TaskTitle"
        Me.TaskTitle.Width = 500
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
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolStripButton2, Me.ToolStripSeparator2, Me.ToolStripButton3})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(310, 25)
        Me.ToolStrip1.TabIndex = 31
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(66, 22)
        Me.ToolStripButton1.Text = "&Refresh"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Image = Global.SimpleAccounts.My.Resources.Resources.ok
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(71, 22)
        Me.ToolStripButton2.Text = "Clear All"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.Image = Global.SimpleAccounts.My.Resources.Resources.cross_icon
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(56, 22)
        Me.ToolStripButton3.Text = "&Close"
        '
        'frmToDoList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(310, 468)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmToDoList"
        Me.Text = "To Do List"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents lblCloseReport As System.Windows.Forms.ToolStripButton
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents ImageList2 As System.Windows.Forms.ImageList
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents TaskTitle As System.Windows.Forms.ColumnHeader
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
End Class

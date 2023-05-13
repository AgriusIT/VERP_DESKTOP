<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Editor
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Editor))
        Me.toolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.fontComboBox = New System.Windows.Forms.ToolStripComboBox()
        Me.fontSizeComboBox = New System.Windows.Forms.ToolStripComboBox()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.boldButton = New System.Windows.Forms.ToolStripButton()
        Me.italicButton = New System.Windows.Forms.ToolStripButton()
        Me.underlineButton = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.colorButton = New System.Windows.Forms.ToolStripButton()
        Me.backColorButton = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.linkButton = New System.Windows.Forms.ToolStripButton()
        Me.imageButton = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.justifyLeftButton = New System.Windows.Forms.ToolStripButton()
        Me.justifyCenterButton = New System.Windows.Forms.ToolStripButton()
        Me.justifyRightButton = New System.Windows.Forms.ToolStripButton()
        Me.justifyFullButton = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.orderedListButton = New System.Windows.Forms.ToolStripButton()
        Me.unorderedListButton = New System.Windows.Forms.ToolStripButton()
        Me.outdentButton = New System.Windows.Forms.ToolStripButton()
        Me.indentButton = New System.Windows.Forms.ToolStripButton()
        Me.webBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.timer = New System.Windows.Forms.Timer(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.CutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.CopyToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.PasteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.BackgroundColorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.toolStrip1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'toolStrip1
        '
        Me.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.toolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.fontComboBox, Me.fontSizeComboBox, Me.toolStripSeparator1, Me.boldButton, Me.italicButton, Me.underlineButton, Me.toolStripSeparator4, Me.colorButton, Me.backColorButton, Me.toolStripSeparator2, Me.linkButton, Me.imageButton, Me.toolStripSeparator3, Me.justifyLeftButton, Me.justifyCenterButton, Me.justifyRightButton, Me.justifyFullButton, Me.toolStripSeparator5, Me.orderedListButton, Me.unorderedListButton, Me.outdentButton, Me.indentButton})
        Me.toolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.toolStrip1.Name = "toolStrip1"
        Me.toolStrip1.Size = New System.Drawing.Size(656, 25)
        Me.toolStrip1.TabIndex = 2
        Me.toolStrip1.Text = "toolStrip1"
        '
        'fontComboBox
        '
        Me.fontComboBox.Name = "fontComboBox"
        Me.fontComboBox.Size = New System.Drawing.Size(140, 25)
        Me.fontComboBox.ToolTipText = "Font"
        '
        'fontSizeComboBox
        '
        Me.fontSizeComboBox.Name = "fontSizeComboBox"
        Me.fontSizeComboBox.Size = New System.Drawing.Size(75, 25)
        Me.fontSizeComboBox.ToolTipText = "Font Size"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'boldButton
        '
        Me.boldButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.boldButton.Image = CType(resources.GetObject("boldButton.Image"), System.Drawing.Image)
        Me.boldButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.boldButton.Name = "boldButton"
        Me.boldButton.Size = New System.Drawing.Size(23, 22)
        Me.boldButton.Text = "toolStripButton1"
        Me.boldButton.ToolTipText = "Bold"
        '
        'italicButton
        '
        Me.italicButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.italicButton.Image = CType(resources.GetObject("italicButton.Image"), System.Drawing.Image)
        Me.italicButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.italicButton.Name = "italicButton"
        Me.italicButton.Size = New System.Drawing.Size(23, 22)
        Me.italicButton.Text = "toolStripButton2"
        Me.italicButton.ToolTipText = "Italic"
        '
        'underlineButton
        '
        Me.underlineButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.underlineButton.Image = CType(resources.GetObject("underlineButton.Image"), System.Drawing.Image)
        Me.underlineButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.underlineButton.Name = "underlineButton"
        Me.underlineButton.Size = New System.Drawing.Size(23, 22)
        Me.underlineButton.Text = "toolStripButton3"
        Me.underlineButton.ToolTipText = "Underline"
        '
        'toolStripSeparator4
        '
        Me.toolStripSeparator4.Name = "toolStripSeparator4"
        Me.toolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'colorButton
        '
        Me.colorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.colorButton.Image = CType(resources.GetObject("colorButton.Image"), System.Drawing.Image)
        Me.colorButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.colorButton.Name = "colorButton"
        Me.colorButton.Size = New System.Drawing.Size(23, 22)
        Me.colorButton.Text = "toolStripButton3"
        Me.colorButton.ToolTipText = "Font Color"
        '
        'backColorButton
        '
        Me.backColorButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.backColorButton.Image = CType(resources.GetObject("backColorButton.Image"), System.Drawing.Image)
        Me.backColorButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.backColorButton.Name = "backColorButton"
        Me.backColorButton.Size = New System.Drawing.Size(23, 22)
        Me.backColorButton.Text = "toolStripButton3"
        Me.backColorButton.ToolTipText = "Back Color"
        '
        'toolStripSeparator2
        '
        Me.toolStripSeparator2.Name = "toolStripSeparator2"
        Me.toolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'linkButton
        '
        Me.linkButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.linkButton.Image = CType(resources.GetObject("linkButton.Image"), System.Drawing.Image)
        Me.linkButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.linkButton.Name = "linkButton"
        Me.linkButton.Size = New System.Drawing.Size(23, 22)
        Me.linkButton.Text = "toolStripButton3"
        Me.linkButton.ToolTipText = "Hyperlink"
        '
        'imageButton
        '
        Me.imageButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.imageButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.imageButton.Name = "imageButton"
        Me.imageButton.Size = New System.Drawing.Size(23, 22)
        Me.imageButton.Text = "toolStripButton3"
        Me.imageButton.ToolTipText = "Image"
        '
        'toolStripSeparator3
        '
        Me.toolStripSeparator3.Name = "toolStripSeparator3"
        Me.toolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'justifyLeftButton
        '
        Me.justifyLeftButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.justifyLeftButton.Image = CType(resources.GetObject("justifyLeftButton.Image"), System.Drawing.Image)
        Me.justifyLeftButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.justifyLeftButton.Name = "justifyLeftButton"
        Me.justifyLeftButton.Size = New System.Drawing.Size(23, 22)
        Me.justifyLeftButton.Text = "toolStripButton3"
        Me.justifyLeftButton.ToolTipText = "Justify Left"
        '
        'justifyCenterButton
        '
        Me.justifyCenterButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.justifyCenterButton.Image = CType(resources.GetObject("justifyCenterButton.Image"), System.Drawing.Image)
        Me.justifyCenterButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.justifyCenterButton.Name = "justifyCenterButton"
        Me.justifyCenterButton.Size = New System.Drawing.Size(23, 22)
        Me.justifyCenterButton.Text = "toolStripButton4"
        Me.justifyCenterButton.ToolTipText = "Justify Center"
        '
        'justifyRightButton
        '
        Me.justifyRightButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.justifyRightButton.Image = CType(resources.GetObject("justifyRightButton.Image"), System.Drawing.Image)
        Me.justifyRightButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.justifyRightButton.Name = "justifyRightButton"
        Me.justifyRightButton.Size = New System.Drawing.Size(23, 22)
        Me.justifyRightButton.Text = "toolStripButton5"
        Me.justifyRightButton.ToolTipText = "Justify Right"
        '
        'justifyFullButton
        '
        Me.justifyFullButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.justifyFullButton.Image = CType(resources.GetObject("justifyFullButton.Image"), System.Drawing.Image)
        Me.justifyFullButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.justifyFullButton.Name = "justifyFullButton"
        Me.justifyFullButton.Size = New System.Drawing.Size(23, 22)
        Me.justifyFullButton.Text = "toolStripButton6"
        Me.justifyFullButton.ToolTipText = "Justify Full"
        '
        'toolStripSeparator5
        '
        Me.toolStripSeparator5.Name = "toolStripSeparator5"
        Me.toolStripSeparator5.Size = New System.Drawing.Size(6, 25)
        '
        'orderedListButton
        '
        Me.orderedListButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.orderedListButton.Image = CType(resources.GetObject("orderedListButton.Image"), System.Drawing.Image)
        Me.orderedListButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.orderedListButton.Name = "orderedListButton"
        Me.orderedListButton.Size = New System.Drawing.Size(23, 22)
        Me.orderedListButton.Text = "toolStripButton3"
        Me.orderedListButton.ToolTipText = "Ordered List"
        '
        'unorderedListButton
        '
        Me.unorderedListButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.unorderedListButton.Image = CType(resources.GetObject("unorderedListButton.Image"), System.Drawing.Image)
        Me.unorderedListButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.unorderedListButton.Name = "unorderedListButton"
        Me.unorderedListButton.Size = New System.Drawing.Size(23, 22)
        Me.unorderedListButton.Text = "toolStripButton4"
        Me.unorderedListButton.ToolTipText = "Unordered List"
        '
        'outdentButton
        '
        Me.outdentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.outdentButton.Image = CType(resources.GetObject("outdentButton.Image"), System.Drawing.Image)
        Me.outdentButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.outdentButton.Name = "outdentButton"
        Me.outdentButton.Size = New System.Drawing.Size(23, 22)
        Me.outdentButton.Text = "toolStripButton3"
        Me.outdentButton.ToolTipText = "Outdent"
        '
        'indentButton
        '
        Me.indentButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.indentButton.Image = CType(resources.GetObject("indentButton.Image"), System.Drawing.Image)
        Me.indentButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.indentButton.Name = "indentButton"
        Me.indentButton.Size = New System.Drawing.Size(23, 22)
        Me.indentButton.Text = "toolStripButton4"
        Me.indentButton.ToolTipText = "Indent"
        '
        'webBrowser1
        '
        Me.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.webBrowser1.Location = New System.Drawing.Point(0, 25)
        Me.webBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.webBrowser1.Name = "webBrowser1"
        Me.webBrowser1.Size = New System.Drawing.Size(656, 216)
        Me.webBrowser1.TabIndex = 3
        '
        'timer
        '
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CutToolStripMenuItem, Me.CopyToolStripMenuItem, Me.PasteToolStripMenuItem, Me.DeleteToolStripMenuItem, Me.BackgroundColorToolStripMenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(169, 114)
        '
        'CutToolStripMenuItem
        '
        Me.CutToolStripMenuItem.Name = "CutToolStripMenuItem"
        Me.CutToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.CutToolStripMenuItem.Text = "Cut"
        '
        'CopyToolStripMenuItem
        '
        Me.CopyToolStripMenuItem.Name = "CopyToolStripMenuItem"
        Me.CopyToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.CopyToolStripMenuItem.Text = "Copy"
        '
        'PasteToolStripMenuItem
        '
        Me.PasteToolStripMenuItem.Name = "PasteToolStripMenuItem"
        Me.PasteToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.PasteToolStripMenuItem.Text = "Paste"
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'BackgroundColorToolStripMenuItem
        '
        Me.BackgroundColorToolStripMenuItem.Name = "BackgroundColorToolStripMenuItem"
        Me.BackgroundColorToolStripMenuItem.Size = New System.Drawing.Size(168, 22)
        Me.BackgroundColorToolStripMenuItem.Text = "Background color"
        '
        'Editor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.webBrowser1)
        Me.Controls.Add(Me.toolStrip1)
        Me.Name = "Editor"
        Me.Size = New System.Drawing.Size(656, 241)
        Me.toolStrip1.ResumeLayout(False)
        Me.toolStrip1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents toolStrip1 As System.Windows.Forms.ToolStrip
    Private WithEvents fontComboBox As System.Windows.Forms.ToolStripComboBox
    Private WithEvents fontSizeComboBox As System.Windows.Forms.ToolStripComboBox
    Private WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents boldButton As System.Windows.Forms.ToolStripButton
    Private WithEvents italicButton As System.Windows.Forms.ToolStripButton
    Private WithEvents underlineButton As System.Windows.Forms.ToolStripButton
    Private WithEvents toolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents colorButton As System.Windows.Forms.ToolStripButton
    Private WithEvents backColorButton As System.Windows.Forms.ToolStripButton
    Private WithEvents toolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents linkButton As System.Windows.Forms.ToolStripButton
    Private WithEvents imageButton As System.Windows.Forms.ToolStripButton
    Private WithEvents toolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents justifyLeftButton As System.Windows.Forms.ToolStripButton
    Private WithEvents justifyCenterButton As System.Windows.Forms.ToolStripButton
    Private WithEvents justifyRightButton As System.Windows.Forms.ToolStripButton
    Private WithEvents justifyFullButton As System.Windows.Forms.ToolStripButton
    Private WithEvents toolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Private WithEvents orderedListButton As System.Windows.Forms.ToolStripButton
    Private WithEvents unorderedListButton As System.Windows.Forms.ToolStripButton
    Private WithEvents outdentButton As System.Windows.Forms.ToolStripButton
    Private WithEvents indentButton As System.Windows.Forms.ToolStripButton
    Private WithEvents webBrowser1 As System.Windows.Forms.WebBrowser
    Friend WithEvents timer As System.Windows.Forms.Timer
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents CutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents CopyToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PasteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BackgroundColorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class uiCtrlGridBar
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(uiCtrlGridBar))
        Me.btnPrint = New System.Windows.Forms.Button
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.btnExport = New System.Windows.Forms.Button
        Me.lblGridTitle = New System.Windows.Forms.Label
        Me.btnAdjust = New System.Windows.Forms.Button
        Me.btnBold = New System.Windows.Forms.Button
        Me.btnIncreaseFont = New System.Windows.Forms.Button
        Me.btnDecreaseFont = New System.Windows.Forms.Button
        Me.txtGridTitle = New System.Windows.Forms.TextBox
        Me.PrintPreviewDialog1 = New System.Windows.Forms.PrintPreviewDialog
        Me.GridEXPrintDocument1 = New Janus.Windows.GridEX.GridEXPrintDocument
        Me.PageSetupDialog1 = New System.Windows.Forms.PageSetupDialog
        Me.chkCollapsExpand = New System.Windows.Forms.CheckBox
        Me.GridEXExporter1 = New Janus.Windows.GridEX.Export.GridEXExporter(Me.components)
        Me.SuspendLayout()
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnPrint.ImageIndex = 2
        Me.btnPrint.ImageList = Me.ImageList1
        Me.btnPrint.Location = New System.Drawing.Point(461, 0)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(26, 26)
        Me.btnPrint.TabIndex = 8
        Me.btnPrint.Tag = "HideText"
        Me.btnPrint.Text = " "
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "Adjust.ico")
        Me.ImageList1.Images.SetKeyName(1, "Excel.ico")
        Me.ImageList1.Images.SetKeyName(2, "print.ico")
        '
        'btnExport
        '
        Me.btnExport.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnExport.ImageIndex = 1
        Me.btnExport.ImageList = Me.ImageList1
        Me.btnExport.Location = New System.Drawing.Point(434, 0)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(26, 26)
        Me.btnExport.TabIndex = 7
        Me.btnExport.Tag = "HideText"
        Me.btnExport.Text = " "
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'lblGridTitle
        '
        Me.lblGridTitle.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblGridTitle.BackColor = System.Drawing.Color.Transparent
        Me.lblGridTitle.Location = New System.Drawing.Point(167, 5)
        Me.lblGridTitle.Name = "lblGridTitle"
        Me.lblGridTitle.Size = New System.Drawing.Size(46, 18)
        Me.lblGridTitle.TabIndex = 5
        Me.lblGridTitle.Text = "Title"
        '
        'btnAdjust
        '
        Me.btnAdjust.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnAdjust.ImageIndex = 0
        Me.btnAdjust.ImageList = Me.ImageList1
        Me.btnAdjust.Location = New System.Drawing.Point(81, 0)
        Me.btnAdjust.Name = "btnAdjust"
        Me.btnAdjust.Size = New System.Drawing.Size(26, 25)
        Me.btnAdjust.TabIndex = 4
        Me.btnAdjust.Tag = "HideText"
        Me.btnAdjust.Text = " "
        Me.btnAdjust.UseVisualStyleBackColor = True
        '
        'btnBold
        '
        Me.btnBold.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnBold.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBold.ImageList = Me.ImageList1
        Me.btnBold.Location = New System.Drawing.Point(55, 0)
        Me.btnBold.Name = "btnBold"
        Me.btnBold.Size = New System.Drawing.Size(26, 25)
        Me.btnBold.TabIndex = 3
        Me.btnBold.Tag = "HideText"
        Me.btnBold.Text = "B"
        Me.btnBold.UseVisualStyleBackColor = True
        '
        'btnIncreaseFont
        '
        Me.btnIncreaseFont.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnIncreaseFont.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnIncreaseFont.ImageList = Me.ImageList1
        Me.btnIncreaseFont.Location = New System.Drawing.Point(29, 0)
        Me.btnIncreaseFont.Name = "btnIncreaseFont"
        Me.btnIncreaseFont.Size = New System.Drawing.Size(26, 25)
        Me.btnIncreaseFont.TabIndex = 2
        Me.btnIncreaseFont.Tag = "HideText"
        Me.btnIncreaseFont.Text = " F"
        Me.btnIncreaseFont.UseVisualStyleBackColor = True
        '
        'btnDecreaseFont
        '
        Me.btnDecreaseFont.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.btnDecreaseFont.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDecreaseFont.ImageList = Me.ImageList1
        Me.btnDecreaseFont.Location = New System.Drawing.Point(3, 0)
        Me.btnDecreaseFont.Name = "btnDecreaseFont"
        Me.btnDecreaseFont.Size = New System.Drawing.Size(26, 25)
        Me.btnDecreaseFont.TabIndex = 1
        Me.btnDecreaseFont.Tag = "HideText"
        Me.btnDecreaseFont.Text = " F"
        Me.btnDecreaseFont.UseVisualStyleBackColor = True
        '
        'txtGridTitle
        '
        Me.txtGridTitle.Anchor = CType((System.Windows.Forms.AnchorStyles.Left Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtGridTitle.Location = New System.Drawing.Point(219, 3)
        Me.txtGridTitle.MaxLength = 100
        Me.txtGridTitle.Name = "txtGridTitle"
        Me.txtGridTitle.Size = New System.Drawing.Size(213, 20)
        Me.txtGridTitle.TabIndex = 9
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
        'GridEXPrintDocument1
        '
        Me.GridEXPrintDocument1.PageHeaderFormatStyle.FontBold = Janus.Windows.GridEX.TriState.[True]
        Me.GridEXPrintDocument1.PageHeaderFormatStyle.FontUnderline = Janus.Windows.GridEX.TriState.[True]
        '
        'PageSetupDialog1
        '
        Me.PageSetupDialog1.Document = Me.GridEXPrintDocument1
        '
        'chkCollapsExpand
        '
        Me.chkCollapsExpand.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkCollapsExpand.AutoSize = True
        Me.chkCollapsExpand.Checked = True
        Me.chkCollapsExpand.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkCollapsExpand.Location = New System.Drawing.Point(114, 5)
        Me.chkCollapsExpand.Name = "chkCollapsExpand"
        Me.chkCollapsExpand.Size = New System.Drawing.Size(44, 17)
        Me.chkCollapsExpand.TabIndex = 10
        Me.chkCollapsExpand.Text = "Exp"
        Me.chkCollapsExpand.UseVisualStyleBackColor = True
        '
        'uiCtrlGridBar
        '
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.Controls.Add(Me.chkCollapsExpand)
        Me.Controls.Add(Me.txtGridTitle)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.lblGridTitle)
        Me.Controls.Add(Me.btnBold)
        Me.Controls.Add(Me.btnDecreaseFont)
        Me.Controls.Add(Me.btnIncreaseFont)
        Me.Controls.Add(Me.btnAdjust)
        Me.Name = "uiCtrlGridBar"
        Me.Size = New System.Drawing.Size(490, 25)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnDecreaseFont As System.Windows.Forms.Button
    Friend WithEvents btnAdjust As System.Windows.Forms.Button
    Friend WithEvents btnBold As System.Windows.Forms.Button
    Friend WithEvents btnIncreaseFont As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents lblGridTitle As System.Windows.Forms.Label
    Friend WithEvents txtGridTitle As System.Windows.Forms.TextBox
    Friend WithEvents PrintPreviewDialog1 As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents GridEXPrintDocument1 As Janus.Windows.GridEX.GridEXPrintDocument
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents PageSetupDialog1 As System.Windows.Forms.PageSetupDialog
    Friend WithEvents chkCollapsExpand As System.Windows.Forms.CheckBox
    Friend WithEvents GridEXExporter1 As Janus.Windows.GridEX.Export.GridEXExporter

End Class

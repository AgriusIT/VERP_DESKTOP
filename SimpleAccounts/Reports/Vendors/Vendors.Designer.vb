<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Vendors
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Vendors))
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.UiButton1 = New Janus.Windows.EditControls.UIButton()
        Me.UiButton2 = New Janus.Windows.EditControls.UIButton()
        Me.UiButton3 = New Janus.Windows.EditControls.UIButton()
        Me.UiButton4 = New Janus.Windows.EditControls.UIButton()
        Me.UiButton5 = New Janus.Windows.EditControls.UIButton()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(18, 12)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(106, 35)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Vendor"
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.UiButton1)
        Me.FlowLayoutPanel1.Controls.Add(Me.UiButton2)
        Me.FlowLayoutPanel1.Controls.Add(Me.UiButton3)
        Me.FlowLayoutPanel1.Controls.Add(Me.UiButton4)
        Me.FlowLayoutPanel1.Controls.Add(Me.UiButton5)
        Me.FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(18, 115)
        Me.FlowLayoutPanel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(630, 485)
        Me.FlowLayoutPanel1.TabIndex = 2
        '
        'UiButton1
        '
        Me.UiButton1.Image = CType(resources.GetObject("UiButton1.Image"), System.Drawing.Image)
        Me.UiButton1.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.UiButton1.ImageSize = New System.Drawing.Size(32, 32)
        Me.UiButton1.ImageVerticalAlignment = Janus.Windows.EditControls.ImageVerticalAlignment.Empty
        Me.UiButton1.Location = New System.Drawing.Point(4, 5)
        Me.UiButton1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UiButton1.Name = "UiButton1"
        Me.UiButton1.Size = New System.Drawing.Size(334, 49)
        Me.UiButton1.TabIndex = 0
        Me.UiButton1.Text = "V01. Vendors List"
        Me.UiButton1.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'UiButton2
        '
        Me.UiButton2.Image = CType(resources.GetObject("UiButton2.Image"), System.Drawing.Image)
        Me.UiButton2.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.UiButton2.ImageSize = New System.Drawing.Size(32, 32)
        Me.UiButton2.ImageVerticalAlignment = Janus.Windows.EditControls.ImageVerticalAlignment.Empty
        Me.UiButton2.Location = New System.Drawing.Point(4, 64)
        Me.UiButton2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UiButton2.Name = "UiButton2"
        Me.UiButton2.Size = New System.Drawing.Size(334, 49)
        Me.UiButton2.TabIndex = 1
        Me.UiButton2.Text = "V02. Vendors Payables"
        Me.UiButton2.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'UiButton3
        '
        Me.UiButton3.Image = CType(resources.GetObject("UiButton3.Image"), System.Drawing.Image)
        Me.UiButton3.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.UiButton3.ImageSize = New System.Drawing.Size(32, 32)
        Me.UiButton3.ImageVerticalAlignment = Janus.Windows.EditControls.ImageVerticalAlignment.Empty
        Me.UiButton3.Location = New System.Drawing.Point(4, 123)
        Me.UiButton3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UiButton3.Name = "UiButton3"
        Me.UiButton3.Size = New System.Drawing.Size(334, 49)
        Me.UiButton3.TabIndex = 2
        Me.UiButton3.Text = "V03. Summary Of Purchase "
        Me.UiButton3.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'UiButton4
        '
        Me.UiButton4.Image = CType(resources.GetObject("UiButton4.Image"), System.Drawing.Image)
        Me.UiButton4.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.UiButton4.ImageSize = New System.Drawing.Size(32, 32)
        Me.UiButton4.ImageVerticalAlignment = Janus.Windows.EditControls.ImageVerticalAlignment.Empty
        Me.UiButton4.Location = New System.Drawing.Point(4, 182)
        Me.UiButton4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UiButton4.Name = "UiButton4"
        Me.UiButton4.Size = New System.Drawing.Size(334, 49)
        Me.UiButton4.TabIndex = 3
        Me.UiButton4.Text = "V04. Summary Of Purchase  Return"
        Me.UiButton4.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'UiButton5
        '
        Me.UiButton5.Image = CType(resources.GetObject("UiButton5.Image"), System.Drawing.Image)
        Me.UiButton5.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.UiButton5.ImageSize = New System.Drawing.Size(32, 32)
        Me.UiButton5.ImageVerticalAlignment = Janus.Windows.EditControls.ImageVerticalAlignment.Empty
        Me.UiButton5.Location = New System.Drawing.Point(4, 241)
        Me.UiButton5.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UiButton5.Name = "UiButton5"
        Me.UiButton5.Size = New System.Drawing.Size(334, 49)
        Me.UiButton5.TabIndex = 4
        Me.UiButton5.Text = "V05. Purchase Detail With Weight"
        Me.UiButton5.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(675, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 25)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(675, 65)
        Me.pnlHeader.TabIndex = 114
        '
        'Vendors
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoScroll = True
        Me.ClientSize = New System.Drawing.Size(675, 618)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "Vendors"
        Me.Text = "Vendors"
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents UiButton1 As Janus.Windows.EditControls.UIButton
    Friend WithEvents UiButton2 As Janus.Windows.EditControls.UIButton
    Friend WithEvents UiButton3 As Janus.Windows.EditControls.UIButton
    Friend WithEvents UiButton4 As Janus.Windows.EditControls.UIButton
    Friend WithEvents UiButton5 As Janus.Windows.EditControls.UIButton
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Customer
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Customer))
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.UiButton1 = New Janus.Windows.EditControls.UIButton()
        Me.UiButton5 = New Janus.Windows.EditControls.UIButton()
        Me.UiButton6 = New Janus.Windows.EditControls.UIButton()
        Me.VisualStyleManager1 = New Janus.Windows.Common.VisualStyleManager(Me.components)
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel()
        Me.UiButton2 = New Janus.Windows.EditControls.UIButton()
        Me.UiButton3 = New Janus.Windows.EditControls.UIButton()
        Me.UiButton4 = New Janus.Windows.EditControls.UIButton()
        Me.UiButton8 = New Janus.Windows.EditControls.UIButton()
        Me.UiButton11 = New Janus.Windows.EditControls.UIButton()
        Me.UiButton9 = New Janus.Windows.EditControls.UIButton()
        Me.UiButton10 = New Janus.Windows.EditControls.UIButton()
        Me.UiButton7 = New Janus.Windows.EditControls.UIButton()
        Me.UiButton12 = New Janus.Windows.EditControls.UIButton()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.pnlHeader.SuspendLayout()
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(18, 14)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(150, 36)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "Customer"
        '
        'UiButton1
        '
        Me.UiButton1.Image = CType(resources.GetObject("UiButton1.Image"), System.Drawing.Image)
        Me.UiButton1.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.UiButton1.ImageSize = New System.Drawing.Size(32, 32)
        Me.UiButton1.Location = New System.Drawing.Point(4, 5)
        Me.UiButton1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UiButton1.Name = "UiButton1"
        Me.UiButton1.Size = New System.Drawing.Size(300, 49)
        Me.UiButton1.TabIndex = 0
        Me.UiButton1.Text = "C01. Customers List"
        Me.UiButton1.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'UiButton5
        '
        Me.UiButton5.Image = CType(resources.GetObject("UiButton5.Image"), System.Drawing.Image)
        Me.UiButton5.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.UiButton5.ImageSize = New System.Drawing.Size(32, 32)
        Me.UiButton5.Location = New System.Drawing.Point(4, 64)
        Me.UiButton5.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UiButton5.Name = "UiButton5"
        Me.UiButton5.Size = New System.Drawing.Size(300, 49)
        Me.UiButton5.TabIndex = 1
        Me.UiButton5.Text = "C02. Daily Working"
        Me.UiButton5.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'UiButton6
        '
        Me.UiButton6.Image = CType(resources.GetObject("UiButton6.Image"), System.Drawing.Image)
        Me.UiButton6.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.UiButton6.ImageSize = New System.Drawing.Size(32, 32)
        Me.UiButton6.Location = New System.Drawing.Point(4, 123)
        Me.UiButton6.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UiButton6.Name = "UiButton6"
        Me.UiButton6.Size = New System.Drawing.Size(300, 49)
        Me.UiButton6.TabIndex = 2
        Me.UiButton6.Text = "C03. Discount Net Rate"
        Me.UiButton6.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1023, 57)
        Me.pnlHeader.TabIndex = 1
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Controls.Add(Me.UiButton1)
        Me.FlowLayoutPanel1.Controls.Add(Me.UiButton5)
        Me.FlowLayoutPanel1.Controls.Add(Me.UiButton6)
        Me.FlowLayoutPanel1.Controls.Add(Me.UiButton2)
        Me.FlowLayoutPanel1.Controls.Add(Me.UiButton3)
        Me.FlowLayoutPanel1.Controls.Add(Me.UiButton4)
        Me.FlowLayoutPanel1.Controls.Add(Me.UiButton8)
        Me.FlowLayoutPanel1.Controls.Add(Me.UiButton11)
        Me.FlowLayoutPanel1.Controls.Add(Me.UiButton9)
        Me.FlowLayoutPanel1.Controls.Add(Me.UiButton10)
        Me.FlowLayoutPanel1.Controls.Add(Me.UiButton7)
        Me.FlowLayoutPanel1.Controls.Add(Me.UiButton12)
        Me.FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(18, 115)
        Me.FlowLayoutPanel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(986, 472)
        Me.FlowLayoutPanel1.TabIndex = 2
        '
        'UiButton2
        '
        Me.UiButton2.Image = CType(resources.GetObject("UiButton2.Image"), System.Drawing.Image)
        Me.UiButton2.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.UiButton2.ImageSize = New System.Drawing.Size(32, 32)
        Me.UiButton2.Location = New System.Drawing.Point(4, 182)
        Me.UiButton2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UiButton2.Name = "UiButton2"
        Me.UiButton2.Size = New System.Drawing.Size(300, 49)
        Me.UiButton2.TabIndex = 3
        Me.UiButton2.Text = "C04. Saleman/Dealer Voucher"
        Me.UiButton2.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'UiButton3
        '
        Me.UiButton3.Image = CType(resources.GetObject("UiButton3.Image"), System.Drawing.Image)
        Me.UiButton3.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.UiButton3.ImageSize = New System.Drawing.Size(32, 32)
        Me.UiButton3.Location = New System.Drawing.Point(4, 241)
        Me.UiButton3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UiButton3.Name = "UiButton3"
        Me.UiButton3.Size = New System.Drawing.Size(300, 49)
        Me.UiButton3.TabIndex = 4
        Me.UiButton3.Text = "C05. Customer Receivables"
        Me.UiButton3.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'UiButton4
        '
        Me.UiButton4.Image = CType(resources.GetObject("UiButton4.Image"), System.Drawing.Image)
        Me.UiButton4.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.UiButton4.ImageSize = New System.Drawing.Size(32, 32)
        Me.UiButton4.Location = New System.Drawing.Point(4, 300)
        Me.UiButton4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UiButton4.Name = "UiButton4"
        Me.UiButton4.Size = New System.Drawing.Size(300, 49)
        Me.UiButton4.TabIndex = 5
        Me.UiButton4.Text = "C06. Summary Of Sales"
        Me.UiButton4.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'UiButton8
        '
        Me.UiButton8.Image = CType(resources.GetObject("UiButton8.Image"), System.Drawing.Image)
        Me.UiButton8.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.UiButton8.ImageSize = New System.Drawing.Size(32, 32)
        Me.UiButton8.Location = New System.Drawing.Point(4, 359)
        Me.UiButton8.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UiButton8.Name = "UiButton8"
        Me.UiButton8.Size = New System.Drawing.Size(300, 49)
        Me.UiButton8.TabIndex = 6
        Me.UiButton8.Text = "C07. Top of Customers"
        Me.UiButton8.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'UiButton11
        '
        Me.UiButton11.Image = CType(resources.GetObject("UiButton11.Image"), System.Drawing.Image)
        Me.UiButton11.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.UiButton11.ImageSize = New System.Drawing.Size(32, 32)
        Me.UiButton11.Location = New System.Drawing.Point(4, 418)
        Me.UiButton11.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UiButton11.Name = "UiButton11"
        Me.UiButton11.Size = New System.Drawing.Size(300, 49)
        Me.UiButton11.TabIndex = 7
        Me.UiButton11.Text = "C08. Customer Sales Analysis"
        Me.UiButton11.Visible = False
        Me.UiButton11.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'UiButton9
        '
        Me.UiButton9.Image = CType(resources.GetObject("UiButton9.Image"), System.Drawing.Image)
        Me.UiButton9.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.UiButton9.ImageSize = New System.Drawing.Size(32, 32)
        Me.UiButton9.Location = New System.Drawing.Point(312, 5)
        Me.UiButton9.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UiButton9.Name = "UiButton9"
        Me.UiButton9.Size = New System.Drawing.Size(300, 49)
        Me.UiButton9.TabIndex = 8
        Me.UiButton9.Text = "C09. Item Wise Sales Summary"
        Me.UiButton9.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'UiButton10
        '
        Me.UiButton10.Image = CType(resources.GetObject("UiButton10.Image"), System.Drawing.Image)
        Me.UiButton10.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.UiButton10.ImageSize = New System.Drawing.Size(32, 32)
        Me.UiButton10.Location = New System.Drawing.Point(312, 64)
        Me.UiButton10.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UiButton10.Name = "UiButton10"
        Me.UiButton10.Size = New System.Drawing.Size(300, 49)
        Me.UiButton10.TabIndex = 9
        Me.UiButton10.Text = " C10. Item Wise Sales Detail"
        Me.UiButton10.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'UiButton7
        '
        Me.UiButton7.Image = CType(resources.GetObject("UiButton7.Image"), System.Drawing.Image)
        Me.UiButton7.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.UiButton7.ImageSize = New System.Drawing.Size(32, 32)
        Me.UiButton7.Location = New System.Drawing.Point(312, 123)
        Me.UiButton7.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UiButton7.Name = "UiButton7"
        Me.UiButton7.Size = New System.Drawing.Size(300, 49)
        Me.UiButton7.TabIndex = 10
        Me.UiButton7.Text = "C11. Item Wise Sales History"
        Me.UiButton7.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'UiButton12
        '
        Me.UiButton12.Image = CType(resources.GetObject("UiButton12.Image"), System.Drawing.Image)
        Me.UiButton12.ImageHorizontalAlignment = Janus.Windows.EditControls.ImageHorizontalAlignment.Near
        Me.UiButton12.ImageSize = New System.Drawing.Size(32, 32)
        Me.UiButton12.Location = New System.Drawing.Point(312, 182)
        Me.UiButton12.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UiButton12.Name = "UiButton12"
        Me.UiButton12.Size = New System.Drawing.Size(300, 49)
        Me.UiButton12.TabIndex = 11
        Me.UiButton12.Text = "C12. Customer Monthly Sales"
        Me.UiButton12.VisualStyle = Janus.Windows.UI.VisualStyle.VS2005
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 57)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1023, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'Customer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1023, 618)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "Customer"
        Me.Text = "Customer"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents UiButton1 As Janus.Windows.EditControls.UIButton
    Friend WithEvents UiButton5 As Janus.Windows.EditControls.UIButton
    Friend WithEvents UiButton6 As Janus.Windows.EditControls.UIButton
    Friend WithEvents VisualStyleManager1 As Janus.Windows.Common.VisualStyleManager
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents UiButton2 As Janus.Windows.EditControls.UIButton
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents UiButton3 As Janus.Windows.EditControls.UIButton
    Friend WithEvents UiButton4 As Janus.Windows.EditControls.UIButton
    Friend WithEvents UiButton8 As Janus.Windows.EditControls.UIButton
    Friend WithEvents UiButton11 As Janus.Windows.EditControls.UIButton
    Friend WithEvents UiButton9 As Janus.Windows.EditControls.UIButton
    Friend WithEvents UiButton10 As Janus.Windows.EditControls.UIButton
    Friend WithEvents UiButton7 As Janus.Windows.EditControls.UIButton
    Friend WithEvents UiButton12 As Janus.Windows.EditControls.UIButton
End Class

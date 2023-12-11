<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEmployeeCard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEmployeeCard))
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.Welcome = New System.Windows.Forms.TabPage()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.P1 = New System.Windows.Forms.Button()
        Me.N1 = New System.Windows.Forms.Button()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.pnlHeader3 = New System.Windows.Forms.Panel()
        Me.lblHeader3 = New System.Windows.Forms.Label()
        Me.lstViewDepartments = New System.Windows.Forms.ListView()
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.P2 = New System.Windows.Forms.Button()
        Me.N2 = New System.Windows.Forms.Button()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.pnlHeader2 = New System.Windows.Forms.Panel()
        Me.lblHeader2 = New System.Windows.Forms.Label()
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX()
        Me.P3 = New System.Windows.Forms.Button()
        Me.N3 = New System.Windows.Forms.Button()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.pnlHeader1 = New System.Windows.Forms.Panel()
        Me.lblHeader1 = New System.Windows.Forms.Label()
        Me.P4 = New System.Windows.Forms.Button()
        Me.N4 = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.Welcome.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.pnlHeader3.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.pnlHeader2.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage3.SuspendLayout()
        Me.pnlHeader1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom
        Me.TabControl1.Controls.Add(Me.Welcome)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabControl1.Multiline = True
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(1114, 675)
        Me.TabControl1.TabIndex = 0
        '
        'Welcome
        '
        Me.Welcome.Controls.Add(Me.pnlHeader)
        Me.Welcome.Controls.Add(Me.P1)
        Me.Welcome.Controls.Add(Me.N1)
        Me.Welcome.Location = New System.Drawing.Point(4, 4)
        Me.Welcome.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Welcome.Name = "Welcome"
        Me.Welcome.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Welcome.Size = New System.Drawing.Size(1106, 642)
        Me.Welcome.TabIndex = 0
        Me.Welcome.Text = "Welcome"
        Me.Welcome.UseVisualStyleBackColor = True
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(4, 5)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1098, 75)
        Me.pnlHeader.TabIndex = 36
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 26.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(21, 3)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(965, 60)
        Me.lblHeader.TabIndex = 4
        Me.lblHeader.Text = "Welcome to employee card printing wizard"
        '
        'P1
        '
        Me.P1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.P1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.P1.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.P1.ForeColor = System.Drawing.Color.White
        Me.P1.Location = New System.Drawing.Point(856, 591)
        Me.P1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.P1.Name = "P1"
        Me.P1.Size = New System.Drawing.Size(112, 35)
        Me.P1.TabIndex = 3
        Me.P1.Text = "< Previous"
        Me.P1.UseVisualStyleBackColor = False
        '
        'N1
        '
        Me.N1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.N1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(87, Byte), Integer))
        Me.N1.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.N1.ForeColor = System.Drawing.Color.White
        Me.N1.Location = New System.Drawing.Point(978, 591)
        Me.N1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.N1.Name = "N1"
        Me.N1.Size = New System.Drawing.Size(112, 35)
        Me.N1.TabIndex = 2
        Me.N1.Text = "Next >"
        Me.N1.UseVisualStyleBackColor = False
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.pnlHeader3)
        Me.TabPage2.Controls.Add(Me.lstViewDepartments)
        Me.TabPage2.Controls.Add(Me.P2)
        Me.TabPage2.Controls.Add(Me.N2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 4)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabPage2.Size = New System.Drawing.Size(1106, 644)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Select Departments"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'pnlHeader3
        '
        Me.pnlHeader3.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader3.Controls.Add(Me.lblHeader3)
        Me.pnlHeader3.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader3.Location = New System.Drawing.Point(4, 5)
        Me.pnlHeader3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader3.Name = "pnlHeader3"
        Me.pnlHeader3.Size = New System.Drawing.Size(1098, 89)
        Me.pnlHeader3.TabIndex = 36
        '
        'lblHeader3
        '
        Me.lblHeader3.AutoSize = True
        Me.lblHeader3.Font = New System.Drawing.Font("Times New Roman", 27.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader3.ForeColor = System.Drawing.Color.Black
        Me.lblHeader3.Location = New System.Drawing.Point(8, 3)
        Me.lblHeader3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader3.Name = "lblHeader3"
        Me.lblHeader3.Size = New System.Drawing.Size(472, 62)
        Me.lblHeader3.TabIndex = 5
        Me.lblHeader3.Text = "Select Departments"
        '
        'lstViewDepartments
        '
        Me.lstViewDepartments.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lstViewDepartments.LargeImageList = Me.ImageList1
        Me.lstViewDepartments.Location = New System.Drawing.Point(20, 103)
        Me.lstViewDepartments.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.lstViewDepartments.Name = "lstViewDepartments"
        Me.lstViewDepartments.Size = New System.Drawing.Size(1069, 478)
        Me.lstViewDepartments.SmallImageList = Me.ImageList1
        Me.lstViewDepartments.TabIndex = 6
        Me.lstViewDepartments.UseCompatibleStateImageBehavior = False
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "2")
        Me.ImageList1.Images.SetKeyName(1, "1")
        '
        'P2
        '
        Me.P2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.P2.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.P2.Location = New System.Drawing.Point(856, 593)
        Me.P2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.P2.Name = "P2"
        Me.P2.Size = New System.Drawing.Size(112, 35)
        Me.P2.TabIndex = 3
        Me.P2.Text = "< Previous"
        Me.P2.UseVisualStyleBackColor = True
        '
        'N2
        '
        Me.N2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.N2.Font = New System.Drawing.Font("Verdana", 8.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.N2.Location = New System.Drawing.Point(978, 593)
        Me.N2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.N2.Name = "N2"
        Me.N2.Size = New System.Drawing.Size(112, 35)
        Me.N2.TabIndex = 2
        Me.N2.Text = "Next >"
        Me.N2.UseVisualStyleBackColor = True
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.pnlHeader2)
        Me.TabPage1.Controls.Add(Me.GridEX1)
        Me.TabPage1.Controls.Add(Me.P3)
        Me.TabPage1.Controls.Add(Me.N3)
        Me.TabPage1.Location = New System.Drawing.Point(4, 4)
        Me.TabPage1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Size = New System.Drawing.Size(1106, 642)
        Me.TabPage1.TabIndex = 2
        Me.TabPage1.Text = "Select Employees"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'pnlHeader2
        '
        Me.pnlHeader2.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader2.Controls.Add(Me.lblHeader2)
        Me.pnlHeader2.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader2.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader2.Name = "pnlHeader2"
        Me.pnlHeader2.Size = New System.Drawing.Size(1106, 83)
        Me.pnlHeader2.TabIndex = 36
        '
        'lblHeader2
        '
        Me.lblHeader2.AutoSize = True
        Me.lblHeader2.Font = New System.Drawing.Font("Times New Roman", 27.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader2.ForeColor = System.Drawing.Color.Black
        Me.lblHeader2.Location = New System.Drawing.Point(12, 8)
        Me.lblHeader2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader2.Name = "lblHeader2"
        Me.lblHeader2.Size = New System.Drawing.Size(421, 62)
        Me.lblHeader2.TabIndex = 6
        Me.lblHeader2.Text = "Select Employees"
        '
        'GridEX1
        '
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridEX1.Location = New System.Drawing.Point(-6, 92)
        Me.GridEX1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.Size = New System.Drawing.Size(1068, 489)
        Me.GridEX1.TabIndex = 7
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'P3
        '
        Me.P3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.P3.Location = New System.Drawing.Point(856, 591)
        Me.P3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.P3.Name = "P3"
        Me.P3.Size = New System.Drawing.Size(112, 35)
        Me.P3.TabIndex = 3
        Me.P3.Text = "< Previous"
        Me.P3.UseVisualStyleBackColor = True
        '
        'N3
        '
        Me.N3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.N3.Location = New System.Drawing.Point(978, 591)
        Me.N3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.N3.Name = "N3"
        Me.N3.Size = New System.Drawing.Size(112, 35)
        Me.N3.TabIndex = 2
        Me.N3.Text = "Next >"
        Me.N3.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.pnlHeader1)
        Me.TabPage3.Controls.Add(Me.P4)
        Me.TabPage3.Controls.Add(Me.N4)
        Me.TabPage3.Location = New System.Drawing.Point(4, 4)
        Me.TabPage3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(1106, 642)
        Me.TabPage3.TabIndex = 3
        Me.TabPage3.Text = "Finish"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'pnlHeader1
        '
        Me.pnlHeader1.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader1.Controls.Add(Me.lblHeader1)
        Me.pnlHeader1.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader1.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader1.Name = "pnlHeader1"
        Me.pnlHeader1.Size = New System.Drawing.Size(1106, 82)
        Me.pnlHeader1.TabIndex = 36
        '
        'lblHeader1
        '
        Me.lblHeader1.AutoSize = True
        Me.lblHeader1.Font = New System.Drawing.Font("Times New Roman", 27.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader1.ForeColor = System.Drawing.Color.Black
        Me.lblHeader1.Location = New System.Drawing.Point(12, 8)
        Me.lblHeader1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader1.Name = "lblHeader1"
        Me.lblHeader1.Size = New System.Drawing.Size(376, 62)
        Me.lblHeader1.TabIndex = 5
        Me.lblHeader1.Text = "Ready To Print"
        '
        'P4
        '
        Me.P4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.P4.Location = New System.Drawing.Point(856, 591)
        Me.P4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.P4.Name = "P4"
        Me.P4.Size = New System.Drawing.Size(112, 35)
        Me.P4.TabIndex = 1
        Me.P4.Text = "< Previous"
        Me.P4.UseVisualStyleBackColor = True
        '
        'N4
        '
        Me.N4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.N4.Location = New System.Drawing.Point(978, 591)
        Me.N4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.N4.Name = "N4"
        Me.N4.Size = New System.Drawing.Size(112, 35)
        Me.N4.TabIndex = 0
        Me.N4.Text = "Finish"
        Me.N4.UseVisualStyleBackColor = True
        '
        'frmEmployeeCard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1114, 675)
        Me.Controls.Add(Me.TabControl1)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmEmployeeCard"
        Me.Text = "Print Employee Cards"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TabControl1.ResumeLayout(False)
        Me.Welcome.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.pnlHeader3.ResumeLayout(False)
        Me.pnlHeader3.PerformLayout()
        Me.TabPage1.ResumeLayout(False)
        Me.pnlHeader2.ResumeLayout(False)
        Me.pnlHeader2.PerformLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage3.ResumeLayout(False)
        Me.pnlHeader1.ResumeLayout(False)
        Me.pnlHeader1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents Welcome As System.Windows.Forms.TabPage
    Friend WithEvents P1 As System.Windows.Forms.Button
    Friend WithEvents N1 As System.Windows.Forms.Button
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents P2 As System.Windows.Forms.Button
    Friend WithEvents N2 As System.Windows.Forms.Button
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents P3 As System.Windows.Forms.Button
    Friend WithEvents N3 As System.Windows.Forms.Button
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents P4 As System.Windows.Forms.Button
    Friend WithEvents N4 As System.Windows.Forms.Button
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents lstViewDepartments As System.Windows.Forms.ListView
    Friend WithEvents lblHeader3 As System.Windows.Forms.Label
    Friend WithEvents lblHeader2 As System.Windows.Forms.Label
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents lblHeader1 As System.Windows.Forms.Label
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents pnlHeader3 As System.Windows.Forms.Panel
    Friend WithEvents pnlHeader2 As System.Windows.Forms.Panel
    Friend WithEvents pnlHeader1 As System.Windows.Forms.Panel
End Class

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Notes
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Notes))
        Dim grd_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.btnRefresh = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.lblDescription1 = New System.Windows.Forms.Label()
        Me.lblDetails1 = New System.Windows.Forms.Label()
        Me.lblTitle1 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.PictureBox2 = New System.Windows.Forms.PictureBox()
        Me.lblDescription2 = New System.Windows.Forms.Label()
        Me.lblDetails2 = New System.Windows.Forms.Label()
        Me.lblTitle2 = New System.Windows.Forms.Label()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.PictureBox3 = New System.Windows.Forms.PictureBox()
        Me.lblDescription3 = New System.Windows.Forms.Label()
        Me.lblDetails3 = New System.Windows.Forms.Label()
        Me.lblTitle3 = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.pnlHeader.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pnlHeader
        '
        Me.pnlHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnRefresh)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(1360, 75)
        Me.pnlHeader.TabIndex = 32
        '
        'btnRefresh
        '
        Me.btnRefresh.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRefresh.BackgroundImage = CType(resources.GetObject("btnRefresh.BackgroundImage"), System.Drawing.Image)
        Me.btnRefresh.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRefresh.FlatAppearance.BorderSize = 0
        Me.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRefresh.ForeColor = System.Drawing.Color.Black
        Me.btnRefresh.Location = New System.Drawing.Point(1293, 17)
        Me.btnRefresh.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(45, 46)
        Me.btnRefresh.TabIndex = 2
        Me.btnRefresh.UseVisualStyleBackColor = True
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(45, 18)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(115, 45)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Notes"
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.Panel2.Controls.Add(Me.PictureBox1)
        Me.Panel2.Controls.Add(Me.lblDescription1)
        Me.Panel2.Controls.Add(Me.lblDetails1)
        Me.Panel2.Controls.Add(Me.lblTitle1)
        Me.Panel2.Location = New System.Drawing.Point(20, 102)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(309, 440)
        Me.Panel2.TabIndex = 33
        '
        'PictureBox1
        '
        Me.PictureBox1.BackgroundImage = CType(resources.GetObject("PictureBox1.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox1.Location = New System.Drawing.Point(249, 3)
        Me.PictureBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(46, 54)
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'lblDescription1
        '
        Me.lblDescription1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription1.ForeColor = System.Drawing.Color.Black
        Me.lblDescription1.Location = New System.Drawing.Point(22, 126)
        Me.lblDescription1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDescription1.Name = "lblDescription1"
        Me.lblDescription1.Size = New System.Drawing.Size(250, 188)
        Me.lblDescription1.TabIndex = 0
        Me.lblDescription1.Text = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula e" & _
    "get dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient m" & _
    "ontes, nascetur"
        Me.lblDescription1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDetails1
        '
        Me.lblDetails1.AutoSize = True
        Me.lblDetails1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetails1.ForeColor = System.Drawing.Color.Black
        Me.lblDetails1.Location = New System.Drawing.Point(22, 83)
        Me.lblDetails1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDetails1.Name = "lblDetails1"
        Me.lblDetails1.Size = New System.Drawing.Size(65, 22)
        Me.lblDetails1.TabIndex = 0
        Me.lblDetails1.Text = "Details"
        '
        'lblTitle1
        '
        Me.lblTitle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle1.ForeColor = System.Drawing.Color.Black
        Me.lblTitle1.Location = New System.Drawing.Point(22, 20)
        Me.lblTitle1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTitle1.Name = "lblTitle1"
        Me.lblTitle1.Size = New System.Drawing.Size(232, 63)
        Me.lblTitle1.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.Panel3.Controls.Add(Me.PictureBox2)
        Me.Panel3.Controls.Add(Me.lblDescription2)
        Me.Panel3.Controls.Add(Me.lblDetails2)
        Me.Panel3.Controls.Add(Me.lblTitle2)
        Me.Panel3.Location = New System.Drawing.Point(338, 102)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(309, 440)
        Me.Panel3.TabIndex = 33
        '
        'PictureBox2
        '
        Me.PictureBox2.BackgroundImage = CType(resources.GetObject("PictureBox2.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox2.Location = New System.Drawing.Point(249, 3)
        Me.PictureBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.PictureBox2.Name = "PictureBox2"
        Me.PictureBox2.Size = New System.Drawing.Size(46, 54)
        Me.PictureBox2.TabIndex = 1
        Me.PictureBox2.TabStop = False
        '
        'lblDescription2
        '
        Me.lblDescription2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription2.ForeColor = System.Drawing.Color.Black
        Me.lblDescription2.Location = New System.Drawing.Point(22, 126)
        Me.lblDescription2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDescription2.Name = "lblDescription2"
        Me.lblDescription2.Size = New System.Drawing.Size(250, 188)
        Me.lblDescription2.TabIndex = 0
        Me.lblDescription2.Text = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula e" & _
    "get dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient m" & _
    "ontes, nascetur"
        Me.lblDescription2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDetails2
        '
        Me.lblDetails2.AutoSize = True
        Me.lblDetails2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetails2.ForeColor = System.Drawing.Color.Black
        Me.lblDetails2.Location = New System.Drawing.Point(22, 83)
        Me.lblDetails2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDetails2.Name = "lblDetails2"
        Me.lblDetails2.Size = New System.Drawing.Size(65, 22)
        Me.lblDetails2.TabIndex = 0
        Me.lblDetails2.Text = "Details"
        '
        'lblTitle2
        '
        Me.lblTitle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle2.ForeColor = System.Drawing.Color.Black
        Me.lblTitle2.Location = New System.Drawing.Point(22, 20)
        Me.lblTitle2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTitle2.Name = "lblTitle2"
        Me.lblTitle2.Size = New System.Drawing.Size(232, 63)
        Me.lblTitle2.TabIndex = 0
        Me.lblTitle2.Text = "Title"
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.Panel4.Controls.Add(Me.PictureBox3)
        Me.Panel4.Controls.Add(Me.lblDescription3)
        Me.Panel4.Controls.Add(Me.lblDetails3)
        Me.Panel4.Controls.Add(Me.lblTitle3)
        Me.Panel4.Location = New System.Drawing.Point(20, 552)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(309, 440)
        Me.Panel4.TabIndex = 33
        '
        'PictureBox3
        '
        Me.PictureBox3.BackgroundImage = CType(resources.GetObject("PictureBox3.BackgroundImage"), System.Drawing.Image)
        Me.PictureBox3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.PictureBox3.Location = New System.Drawing.Point(249, 3)
        Me.PictureBox3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.PictureBox3.Name = "PictureBox3"
        Me.PictureBox3.Size = New System.Drawing.Size(46, 54)
        Me.PictureBox3.TabIndex = 1
        Me.PictureBox3.TabStop = False
        '
        'lblDescription3
        '
        Me.lblDescription3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDescription3.ForeColor = System.Drawing.Color.Black
        Me.lblDescription3.Location = New System.Drawing.Point(22, 129)
        Me.lblDescription3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDescription3.Name = "lblDescription3"
        Me.lblDescription3.Size = New System.Drawing.Size(250, 188)
        Me.lblDescription3.TabIndex = 0
        Me.lblDescription3.Text = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula e" & _
    "get dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient m" & _
    "ontes, nascetur"
        Me.lblDescription3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDetails3
        '
        Me.lblDetails3.AutoSize = True
        Me.lblDetails3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblDetails3.ForeColor = System.Drawing.Color.Black
        Me.lblDetails3.Location = New System.Drawing.Point(22, 83)
        Me.lblDetails3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDetails3.Name = "lblDetails3"
        Me.lblDetails3.Size = New System.Drawing.Size(65, 22)
        Me.lblDetails3.TabIndex = 0
        Me.lblDetails3.Text = "Details"
        '
        'lblTitle3
        '
        Me.lblTitle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitle3.ForeColor = System.Drawing.Color.Black
        Me.lblTitle3.Location = New System.Drawing.Point(22, 20)
        Me.lblTitle3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTitle3.Name = "lblTitle3"
        Me.lblTitle3.Size = New System.Drawing.Size(232, 63)
        Me.lblTitle3.TabIndex = 0
        Me.lblTitle3.Text = "Title"
        '
        'Button1
        '
        Me.Button1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Font = New System.Drawing.Font("Arial", 110.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Button1.ForeColor = System.Drawing.Color.Teal
        Me.Button1.Location = New System.Drawing.Point(338, 552)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(309, 440)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "+"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'grd
        '
        Me.grd.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grd.ColumnAutoResize = True
        grd_DesignTimeLayout.LayoutString = resources.GetString("grd_DesignTimeLayout.LayoutString")
        Me.grd.DesignTimeLayout = grd_DesignTimeLayout
        Me.grd.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grd.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grd.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grd.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.grd.GroupByBoxVisible = False
        Me.grd.Location = New System.Drawing.Point(656, 102)
        Me.grd.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(687, 891)
        Me.grd.TabIndex = 34
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'Notes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1360, 1050)
        Me.Controls.Add(Me.grd)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel4)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.pnlHeader)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "Notes"
        Me.Text = "Notes"
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.PictureBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        CType(Me.PictureBox3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pnlHeader As Panel
    Friend WithEvents btnRefresh As Button
    Friend WithEvents lblHeader As Label
    Friend WithEvents Panel2 As Panel
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents lblDescription1 As Label
    Friend WithEvents lblDetails1 As Label
    Friend WithEvents lblTitle1 As Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents PictureBox2 As PictureBox
    Friend WithEvents lblDescription2 As Label
    Friend WithEvents lblDetails2 As Label
    Friend WithEvents lblTitle2 As Label
    Friend WithEvents Panel4 As Panel
    Friend WithEvents PictureBox3 As PictureBox
    Friend WithEvents lblDescription3 As Label
    Friend WithEvents lblDetails3 As Label
    Friend WithEvents lblTitle3 As Label
    Friend WithEvents Button1 As Button
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
End Class

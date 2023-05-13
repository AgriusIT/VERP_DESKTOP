<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMessageView
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMessageView))
        Me.SplitMessage = New System.Windows.Forms.SplitContainer()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtpMsgDate = New Janus.Windows.CalendarCombo.CalendarCombo()
        Me.txtUserID = New System.Windows.Forms.TextBox()
        Me.txtMsgID = New System.Windows.Forms.TextBox()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.grdMsgDetail = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.NewToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator = New System.Windows.Forms.ToolStripSeparator()
        Me.CutToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripComboBox1 = New System.Windows.Forms.ToolStripComboBox()
        CType(Me.SplitMessage, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitMessage.Panel1.SuspendLayout()
        Me.SplitMessage.Panel2.SuspendLayout()
        Me.SplitMessage.SuspendLayout()
        CType(Me.grdMsgDetail, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitMessage
        '
        Me.SplitMessage.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitMessage.Location = New System.Drawing.Point(0, 33)
        Me.SplitMessage.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.SplitMessage.Name = "SplitMessage"
        Me.SplitMessage.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitMessage.Panel1
        '
        Me.SplitMessage.Panel1.Controls.Add(Me.Label4)
        Me.SplitMessage.Panel1.Controls.Add(Me.Label3)
        Me.SplitMessage.Panel1.Controls.Add(Me.Label2)
        Me.SplitMessage.Panel1.Controls.Add(Me.Label1)
        Me.SplitMessage.Panel1.Controls.Add(Me.dtpMsgDate)
        Me.SplitMessage.Panel1.Controls.Add(Me.txtUserID)
        Me.SplitMessage.Panel1.Controls.Add(Me.txtMsgID)
        Me.SplitMessage.Panel1.Controls.Add(Me.RichTextBox1)
        Me.SplitMessage.Panel1.Controls.Add(Me.TextBox2)
        Me.SplitMessage.Panel1.Controls.Add(Me.TextBox1)
        '
        'SplitMessage.Panel2
        '
        Me.SplitMessage.Panel2.Controls.Add(Me.grdMsgDetail)
        Me.SplitMessage.Panel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SplitMessage.Size = New System.Drawing.Size(1092, 758)
        Me.SplitMessage.SplitterDistance = 474
        Me.SplitMessage.SplitterWidth = 6
        Me.SplitMessage.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(18, 235)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(74, 20)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Message"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(18, 195)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(84, 20)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "From User"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(18, 155)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(63, 20)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Subject"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 115)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 20)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Date"
        '
        'dtpMsgDate
        '
        '
        '
        '
        Me.dtpMsgDate.DropDownCalendar.Name = ""
        Me.dtpMsgDate.Location = New System.Drawing.Point(106, 109)
        Me.dtpMsgDate.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpMsgDate.Name = "dtpMsgDate"
        Me.dtpMsgDate.Size = New System.Drawing.Size(232, 26)
        Me.dtpMsgDate.TabIndex = 1
        '
        'txtUserID
        '
        Me.txtUserID.Location = New System.Drawing.Point(686, 111)
        Me.txtUserID.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtUserID.Name = "txtUserID"
        Me.txtUserID.Size = New System.Drawing.Size(139, 26)
        Me.txtUserID.TabIndex = 2
        Me.txtUserID.Visible = False
        '
        'txtMsgID
        '
        Me.txtMsgID.Location = New System.Drawing.Point(837, 111)
        Me.txtMsgID.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtMsgID.Name = "txtMsgID"
        Me.txtMsgID.Size = New System.Drawing.Size(168, 26)
        Me.txtMsgID.TabIndex = 3
        Me.txtMsgID.Visible = False
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Location = New System.Drawing.Point(106, 231)
        Me.RichTextBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(898, 232)
        Me.RichTextBox1.TabIndex = 9
        Me.RichTextBox1.Text = ""
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(106, 191)
        Me.TextBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(898, 26)
        Me.TextBox2.TabIndex = 7
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(106, 151)
        Me.TextBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(898, 26)
        Me.TextBox1.TabIndex = 5
        '
        'grdMsgDetail
        '
        Me.grdMsgDetail.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdMsgDetail.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdMsgDetail.GroupByBoxVisible = False
        Me.grdMsgDetail.Location = New System.Drawing.Point(0, 0)
        Me.grdMsgDetail.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.grdMsgDetail.Name = "grdMsgDetail"
        Me.grdMsgDetail.Size = New System.Drawing.Size(1092, 278)
        Me.grdMsgDetail.TabIndex = 0
        Me.grdMsgDetail.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.NewToolStripButton, Me.toolStripSeparator, Me.CutToolStripButton, Me.toolStripSeparator1, Me.HelpToolStripButton, Me.ToolStripComboBox1})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(1092, 33)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'NewToolStripButton
        '
        Me.NewToolStripButton.Image = CType(resources.GetObject("NewToolStripButton.Image"), System.Drawing.Image)
        Me.NewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.NewToolStripButton.Name = "NewToolStripButton"
        Me.NewToolStripButton.Size = New System.Drawing.Size(75, 30)
        Me.NewToolStripButton.Text = "&New"
        '
        'toolStripSeparator
        '
        Me.toolStripSeparator.Name = "toolStripSeparator"
        Me.toolStripSeparator.Size = New System.Drawing.Size(6, 33)
        '
        'CutToolStripButton
        '
        Me.CutToolStripButton.Image = CType(resources.GetObject("CutToolStripButton.Image"), System.Drawing.Image)
        Me.CutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.CutToolStripButton.Name = "CutToolStripButton"
        Me.CutToolStripButton.Size = New System.Drawing.Size(79, 30)
        Me.CutToolStripButton.Text = "Clear"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 33)
        '
        'HelpToolStripButton
        '
        Me.HelpToolStripButton.Image = CType(resources.GetObject("HelpToolStripButton.Image"), System.Drawing.Image)
        Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton.Name = "HelpToolStripButton"
        Me.HelpToolStripButton.Size = New System.Drawing.Size(77, 30)
        Me.HelpToolStripButton.Text = "He&lp"
        '
        'ToolStripComboBox1
        '
        Me.ToolStripComboBox1.Name = "ToolStripComboBox1"
        Me.ToolStripComboBox1.Size = New System.Drawing.Size(180, 33)
        '
        'frmMessageView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1092, 791)
        Me.Controls.Add(Me.SplitMessage)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmMessageView"
        Me.Text = "Message View"
        Me.SplitMessage.Panel1.ResumeLayout(False)
        Me.SplitMessage.Panel1.PerformLayout()
        Me.SplitMessage.Panel2.ResumeLayout(False)
        CType(Me.SplitMessage, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitMessage.ResumeLayout(False)
        CType(Me.grdMsgDetail, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitMessage As System.Windows.Forms.SplitContainer
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents NewToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents CutToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripComboBox1 As System.Windows.Forms.ToolStripComboBox
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents grdMsgDetail As Janus.Windows.GridEX.GridEX
    Friend WithEvents txtUserID As System.Windows.Forms.TextBox
    Friend WithEvents txtMsgID As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents dtpMsgDate As Janus.Windows.CalendarCombo.CalendarCombo

End Class

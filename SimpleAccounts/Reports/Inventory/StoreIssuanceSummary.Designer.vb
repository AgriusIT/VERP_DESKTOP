<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StoreIssuanceSummary
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(StoreIssuanceSummary))
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.UiButton1 = New Janus.Windows.EditControls.UIButton()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.UiButton2 = New Janus.Windows.EditControls.UIButton()
        Me.dtpTo = New System.Windows.Forms.DateTimePicker()
        Me.dtpFrom = New System.Windows.Forms.DateTimePicker()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cmbPeriod = New System.Windows.Forms.ComboBox()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.cmbCostCenter = New System.Windows.Forms.ComboBox()
        Me.lblCostCenter = New System.Windows.Forms.Label()
        Me.lblArticleType = New System.Windows.Forms.Label()
        Me.cmbArticleType = New System.Windows.Forms.ComboBox()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.pnlInvType = New System.Windows.Forms.Panel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.pnlInvType.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(4, 66)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 20)
        Me.Label1.TabIndex = 6
        Me.Label1.Text = "From"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(309, 66)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(27, 20)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "To"
        '
        'UiButton1
        '
        Me.UiButton1.Location = New System.Drawing.Point(4, 5)
        Me.UiButton1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UiButton1.Name = "UiButton1"
        Me.UiButton1.Size = New System.Drawing.Size(99, 30)
        Me.UiButton1.TabIndex = 0
        Me.UiButton1.Text = "Generate"
        Me.UiButton1.ToolTipText = "Generate Report"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.UiButton2, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.UiButton1, 0, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(348, 366)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(218, 40)
        Me.TableLayoutPanel1.TabIndex = 10
        '
        'UiButton2
        '
        Me.UiButton2.Location = New System.Drawing.Point(113, 5)
        Me.UiButton2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.UiButton2.Name = "UiButton2"
        Me.UiButton2.Size = New System.Drawing.Size(99, 30)
        Me.UiButton2.TabIndex = 1
        Me.UiButton2.Text = "Print"
        Me.ToolTip1.SetToolTip(Me.UiButton2, "Direct Print Report")
        '
        'dtpTo
        '
        Me.dtpTo.CustomFormat = "dd/MMM/yyyy"
        Me.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpTo.Location = New System.Drawing.Point(352, 57)
        Me.dtpTo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpTo.Name = "dtpTo"
        Me.dtpTo.Size = New System.Drawing.Size(176, 26)
        Me.dtpTo.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.dtpTo, "To Date")
        '
        'dtpFrom
        '
        Me.dtpFrom.CustomFormat = "dd/MMM/yyyy"
        Me.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFrom.Location = New System.Drawing.Point(122, 57)
        Me.dtpFrom.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtpFrom.Name = "dtpFrom"
        Me.dtpFrom.Size = New System.Drawing.Size(176, 26)
        Me.dtpFrom.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.dtpFrom, "From Date")
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(6, 20)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(54, 20)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "Period"
        '
        'cmbPeriod
        '
        Me.cmbPeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbPeriod.FormattingEnabled = True
        Me.cmbPeriod.Items.AddRange(New Object() {"Today", "Yesterday", "Current Week", "Current Month", "Current Year"})
        Me.cmbPeriod.Location = New System.Drawing.Point(122, 15)
        Me.cmbPeriod.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbPeriod.Name = "cmbPeriod"
        Me.cmbPeriod.Size = New System.Drawing.Size(408, 28)
        Me.cmbPeriod.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.cmbPeriod, "Select Period And Gets Date Range ")
        '
        'cmbCostCenter
        '
        Me.cmbCostCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCostCenter.FormattingEnabled = True
        Me.cmbCostCenter.Items.AddRange(New Object() {"Today", "Yesterday", "Current Week", "Current Month", "Current Year"})
        Me.cmbCostCenter.Location = New System.Drawing.Point(122, 14)
        Me.cmbCostCenter.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbCostCenter.Name = "cmbCostCenter"
        Me.cmbCostCenter.Size = New System.Drawing.Size(408, 28)
        Me.cmbCostCenter.TabIndex = 1
        '
        'lblCostCenter
        '
        Me.lblCostCenter.AutoSize = True
        Me.lblCostCenter.Location = New System.Drawing.Point(4, 18)
        Me.lblCostCenter.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCostCenter.Name = "lblCostCenter"
        Me.lblCostCenter.Size = New System.Drawing.Size(94, 20)
        Me.lblCostCenter.TabIndex = 0
        Me.lblCostCenter.Text = "Cost Center"
        '
        'lblArticleType
        '
        Me.lblArticleType.AutoSize = True
        Me.lblArticleType.Location = New System.Drawing.Point(6, 18)
        Me.lblArticleType.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblArticleType.Name = "lblArticleType"
        Me.lblArticleType.Size = New System.Drawing.Size(91, 20)
        Me.lblArticleType.TabIndex = 2
        Me.lblArticleType.Text = "Article Type"
        '
        'cmbArticleType
        '
        Me.cmbArticleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbArticleType.FormattingEnabled = True
        Me.cmbArticleType.Items.AddRange(New Object() {"Today", "Yesterday", "Current Week", "Current Month", "Current Year"})
        Me.cmbArticleType.Location = New System.Drawing.Point(126, 14)
        Me.cmbArticleType.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbArticleType.Name = "cmbArticleType"
        Me.cmbArticleType.Size = New System.Drawing.Size(408, 28)
        Me.cmbArticleType.TabIndex = 3
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.btnClose)
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(582, 65)
        Me.pnlHeader.TabIndex = 113
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.btnClose.BackgroundImage = CType(resources.GetObject("btnClose.BackgroundImage"), System.Drawing.Image)
        Me.btnClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.btnClose.Location = New System.Drawing.Point(510, 9)
        Me.btnClose.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(54, 46)
        Me.btnClose.TabIndex = 117
        Me.btnClose.UseVisualStyleBackColor = False
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(4, 14)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(353, 36)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Store Issuance Summary"
        '
        'pnlInvType
        '
        Me.pnlInvType.BackColor = System.Drawing.Color.Transparent
        Me.pnlInvType.Controls.Add(Me.lblArticleType)
        Me.pnlInvType.Controls.Add(Me.cmbArticleType)
        Me.pnlInvType.Location = New System.Drawing.Point(18, 74)
        Me.pnlInvType.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlInvType.Name = "pnlInvType"
        Me.pnlInvType.Size = New System.Drawing.Size(548, 60)
        Me.pnlInvType.TabIndex = 114
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.lblCostCenter)
        Me.Panel1.Controls.Add(Me.cmbCostCenter)
        Me.Panel1.Location = New System.Drawing.Point(18, 143)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(548, 60)
        Me.Panel1.TabIndex = 115
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.Label2)
        Me.Panel2.Controls.Add(Me.dtpTo)
        Me.Panel2.Controls.Add(Me.dtpFrom)
        Me.Panel2.Controls.Add(Me.cmbPeriod)
        Me.Panel2.Location = New System.Drawing.Point(18, 212)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(548, 145)
        Me.Panel2.TabIndex = 116
        '
        'StoreIssuanceSummary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(582, 418)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.pnlInvType)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "StoreIssuanceSummary"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Store Issuance Summary"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.pnlInvType.ResumeLayout(False)
        Me.pnlInvType.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents UiButton1 As Janus.Windows.EditControls.UIButton
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents UiButton2 As Janus.Windows.EditControls.UIButton
    Friend WithEvents dtpTo As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpFrom As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cmbPeriod As System.Windows.Forms.ComboBox
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents cmbCostCenter As System.Windows.Forms.ComboBox
    Friend WithEvents lblCostCenter As System.Windows.Forms.Label
    Friend WithEvents lblArticleType As System.Windows.Forms.Label
    Friend WithEvents cmbArticleType As System.Windows.Forms.ComboBox
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents pnlInvType As System.Windows.Forms.Panel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
End Class

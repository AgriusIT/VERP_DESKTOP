<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSkippingSalesInvoicesNumbers
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
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.cmbCompany = New System.Windows.Forms.ComboBox()
        Me.lblInvoiceNoToSkip = New System.Windows.Forms.Label()
        Me.txtInvoiceNoToSkip = New System.Windows.Forms.TextBox()
        Me.txtReason = New System.Windows.Forms.TextBox()
        Me.lblReason = New System.Windows.Forms.Label()
        Me.GridEX1 = New Janus.Windows.GridEX.GridEX()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.txtYear = New System.Windows.Forms.TextBox()
        Me.txtInvoiceNo = New System.Windows.Forms.TextBox()
        Me.cmbMonth = New System.Windows.Forms.ComboBox()
        Me.CtrlGrdBar1 = New SimpleAccounts.CtrlGrdBar()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnSave, Me.btnDelete, Me.btnRefresh})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Padding = New System.Windows.Forms.Padding(0, 0, 2, 0)
        Me.ToolStrip1.Size = New System.Drawing.Size(816, 32)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = Global.SimpleAccounts.My.Resources.Resources.BtnNew_Image
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(75, 29)
        Me.btnNew.Text = "&New"
        '
        'btnSave
        '
        Me.btnSave.Image = Global.SimpleAccounts.My.Resources.Resources.BtnSave_Image
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(77, 29)
        Me.btnSave.Text = "&Save"
        '
        'btnDelete
        '
        Me.btnDelete.Image = Global.SimpleAccounts.My.Resources.Resources.BtnDelete_Image
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(90, 29)
        Me.btnDelete.Text = "&Delete"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(98, 29)
        Me.btnRefresh.Text = "&Refresh"
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Times New Roman", 16.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Black
        Me.lblHeader.Location = New System.Drawing.Point(6, 6)
        Me.lblHeader.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(305, 36)
        Me.lblHeader.TabIndex = 2
        Me.lblHeader.Text = "Skip Invoice Number"
        '
        'Label25
        '
        Me.Label25.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label25.AutoSize = True
        Me.Label25.Location = New System.Drawing.Point(102, 106)
        Me.Label25.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(76, 20)
        Me.Label25.TabIndex = 3
        Me.Label25.Text = "Company"
        '
        'cmbCompany
        '
        Me.cmbCompany.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cmbCompany.FormattingEnabled = True
        Me.cmbCompany.Location = New System.Drawing.Point(188, 102)
        Me.cmbCompany.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbCompany.Name = "cmbCompany"
        Me.cmbCompany.Size = New System.Drawing.Size(464, 28)
        Me.cmbCompany.TabIndex = 4
        Me.cmbCompany.TabStop = False
        Me.ToolTip1.SetToolTip(Me.cmbCompany, "Please select a company")
        '
        'lblInvoiceNoToSkip
        '
        Me.lblInvoiceNoToSkip.AutoSize = True
        Me.lblInvoiceNoToSkip.Location = New System.Drawing.Point(8, 148)
        Me.lblInvoiceNoToSkip.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblInvoiceNoToSkip.Name = "lblInvoiceNoToSkip"
        Me.lblInvoiceNoToSkip.Size = New System.Drawing.Size(167, 20)
        Me.lblInvoiceNoToSkip.TabIndex = 5
        Me.lblInvoiceNoToSkip.Text = "Invoice number to skip"
        '
        'txtInvoiceNoToSkip
        '
        Me.txtInvoiceNoToSkip.Location = New System.Drawing.Point(344, 143)
        Me.txtInvoiceNoToSkip.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtInvoiceNoToSkip.Name = "txtInvoiceNoToSkip"
        Me.txtInvoiceNoToSkip.Size = New System.Drawing.Size(112, 26)
        Me.txtInvoiceNoToSkip.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.txtInvoiceNoToSkip, "Please enter a invoice number")
        '
        'txtReason
        '
        Me.txtReason.Location = New System.Drawing.Point(188, 183)
        Me.txtReason.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtReason.Multiline = True
        Me.txtReason.Name = "txtReason"
        Me.txtReason.Size = New System.Drawing.Size(464, 49)
        Me.txtReason.TabIndex = 11
        Me.ToolTip1.SetToolTip(Me.txtReason, "Please type a reason.")
        '
        'lblReason
        '
        Me.lblReason.AutoSize = True
        Me.lblReason.Location = New System.Drawing.Point(112, 183)
        Me.lblReason.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblReason.Name = "lblReason"
        Me.lblReason.Size = New System.Drawing.Size(65, 20)
        Me.lblReason.TabIndex = 10
        Me.lblReason.Text = "Reason"
        '
        'GridEX1
        '
        Me.GridEX1.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.GridEX1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GridEX1.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.GridEX1.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.GridEX1.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.GridEX1.Location = New System.Drawing.Point(8, 243)
        Me.GridEX1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GridEX1.Name = "GridEX1"
        Me.GridEX1.Size = New System.Drawing.Size(800, 434)
        Me.GridEX1.TabIndex = 12
        Me.GridEX1.VisualStyle = Janus.Windows.GridEX.VisualStyle.VS2005
        '
        'txtYear
        '
        Me.txtYear.Location = New System.Drawing.Point(188, 143)
        Me.txtYear.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtYear.Name = "txtYear"
        Me.txtYear.Size = New System.Drawing.Size(60, 26)
        Me.txtYear.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.txtYear, "Please type a year")
        '
        'txtInvoiceNo
        '
        Me.txtInvoiceNo.Location = New System.Drawing.Point(466, 143)
        Me.txtInvoiceNo.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.txtInvoiceNo.Name = "txtInvoiceNo"
        Me.txtInvoiceNo.ReadOnly = True
        Me.txtInvoiceNo.Size = New System.Drawing.Size(186, 26)
        Me.txtInvoiceNo.TabIndex = 9
        Me.ToolTip1.SetToolTip(Me.txtInvoiceNo, "This field show complete skip no")
        '
        'cmbMonth
        '
        Me.cmbMonth.FormattingEnabled = True
        Me.cmbMonth.Items.AddRange(New Object() {"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"})
        Me.cmbMonth.Location = New System.Drawing.Point(258, 143)
        Me.cmbMonth.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbMonth.Name = "cmbMonth"
        Me.cmbMonth.Size = New System.Drawing.Size(74, 28)
        Me.cmbMonth.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.cmbMonth, "Please select a month")
        '
        'CtrlGrdBar1
        '
        Me.CtrlGrdBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBar1.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBar1.Email = Nothing
        Me.CtrlGrdBar1.FormName = Me
        Me.CtrlGrdBar1.Location = New System.Drawing.Point(765, 0)
        Me.CtrlGrdBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.CtrlGrdBar1.MyGrid = Me.GridEX1
        Me.CtrlGrdBar1.Name = "CtrlGrdBar1"
        Me.CtrlGrdBar1.Size = New System.Drawing.Size(51, 38)
        Me.CtrlGrdBar1.TabIndex = 1
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(196, Byte), Integer), CType(CType(212, Byte), Integer), CType(CType(232, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.pnlHeader.Location = New System.Drawing.Point(0, 32)
        Me.pnlHeader.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(816, 54)
        Me.pnlHeader.TabIndex = 13
        '
        'frmSkippingSalesInvoicesNumbers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(816, 680)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.txtInvoiceNo)
        Me.Controls.Add(Me.cmbMonth)
        Me.Controls.Add(Me.txtYear)
        Me.Controls.Add(Me.GridEX1)
        Me.Controls.Add(Me.CtrlGrdBar1)
        Me.Controls.Add(Me.lblReason)
        Me.Controls.Add(Me.txtReason)
        Me.Controls.Add(Me.txtInvoiceNoToSkip)
        Me.Controls.Add(Me.lblInvoiceNoToSkip)
        Me.Controls.Add(Me.Label25)
        Me.Controls.Add(Me.cmbCompany)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "frmSkippingSalesInvoicesNumbers"
        Me.Text = "Skip Invoice Number"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.GridEX1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents Label25 As System.Windows.Forms.Label
    Friend WithEvents cmbCompany As System.Windows.Forms.ComboBox
    Friend WithEvents lblInvoiceNoToSkip As System.Windows.Forms.Label
    Friend WithEvents txtInvoiceNoToSkip As System.Windows.Forms.TextBox
    Friend WithEvents txtReason As System.Windows.Forms.TextBox
    Friend WithEvents lblReason As System.Windows.Forms.Label
    Friend WithEvents CtrlGrdBar1 As SimpleAccounts.CtrlGrdBar
    Friend WithEvents GridEX1 As Janus.Windows.GridEX.GridEX
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents cmbMonth As System.Windows.Forms.ComboBox
    Friend WithEvents txtYear As System.Windows.Forms.TextBox
    Friend WithEvents txtInvoiceNo As System.Windows.Forms.TextBox
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

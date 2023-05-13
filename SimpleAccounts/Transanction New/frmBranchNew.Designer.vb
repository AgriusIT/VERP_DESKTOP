<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBranchNew
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmBranchNew))
        Me.txtHolderName = New System.Windows.Forms.TextBox()
        Me.txtAccountNumber = New System.Windows.Forms.TextBox()
        Me.txtBrabchCode = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.grdBranch = New Janus.Windows.GridEX.GridEX()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtBankName = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtBranchPhoneNo = New System.Windows.Forms.TextBox()
        Me.cmbChequeLayout = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.cmbBankType = New System.Windows.Forms.ComboBox()
        Me.lblBankType = New System.Windows.Forms.Label()
        Me.txtDesignatedTo = New System.Windows.Forms.TextBox()
        Me.lblDesignatedTo = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.grdBranch, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtHolderName
        '
        Me.txtHolderName.Location = New System.Drawing.Point(132, 124)
        Me.txtHolderName.Name = "txtHolderName"
        Me.txtHolderName.Size = New System.Drawing.Size(229, 20)
        Me.txtHolderName.TabIndex = 7
        '
        'txtAccountNumber
        '
        Me.txtAccountNumber.Location = New System.Drawing.Point(132, 98)
        Me.txtAccountNumber.Name = "txtAccountNumber"
        Me.txtAccountNumber.Size = New System.Drawing.Size(229, 20)
        Me.txtAccountNumber.TabIndex = 5
        '
        'txtBrabchCode
        '
        Me.txtBrabchCode.Location = New System.Drawing.Point(132, 150)
        Me.txtBrabchCode.Name = "txtBrabchCode"
        Me.txtBrabchCode.Size = New System.Drawing.Size(229, 20)
        Me.txtBrabchCode.TabIndex = 9
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 127)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(112, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Account Holder Name"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 101)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(87, 13)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Account Number"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 153)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(102, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Branch Area / Code"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnSave, Me.btnPrint, Me.btnDelete, Me.toolStripSeparator1, Me.btnRefresh})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(532, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = CType(resources.GetObject("btnNew.Image"), System.Drawing.Image)
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(51, 22)
        Me.btnNew.Text = "&New"
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(51, 22)
        Me.btnSave.Text = "&Save"
        '
        'btnPrint
        '
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(52, 22)
        Me.btnPrint.Text = "&Print"
        Me.btnPrint.Visible = False
        '
        'btnDelete
        '
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(60, 22)
        Me.btnDelete.Text = "&Delete"
        Me.btnDelete.Visible = False
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(66, 22)
        Me.btnRefresh.Text = "Refresh"
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(12, 7)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(201, 23)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Bank Information"
        '
        'grdBranch
        '
        Me.grdBranch.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdBranch.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdBranch.GroupByBoxVisible = False
        Me.grdBranch.Location = New System.Drawing.Point(0, 282)
        Me.grdBranch.Name = "grdBranch"
        Me.grdBranch.RecordNavigator = True
        Me.grdBranch.Size = New System.Drawing.Size(532, 192)
        Me.grdBranch.TabIndex = 14
        Me.grdBranch.TabStop = False
        Me.grdBranch.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 75)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Bank Name"
        '
        'txtBankName
        '
        Me.txtBankName.Location = New System.Drawing.Point(132, 72)
        Me.txtBankName.Name = "txtBankName"
        Me.txtBankName.ReadOnly = True
        Me.txtBankName.Size = New System.Drawing.Size(229, 20)
        Me.txtBankName.TabIndex = 3
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(12, 179)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(95, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Branch Phone No."
        '
        'txtBranchPhoneNo
        '
        Me.txtBranchPhoneNo.Location = New System.Drawing.Point(132, 176)
        Me.txtBranchPhoneNo.Name = "txtBranchPhoneNo"
        Me.txtBranchPhoneNo.Size = New System.Drawing.Size(229, 20)
        Me.txtBranchPhoneNo.TabIndex = 11
        '
        'cmbChequeLayout
        '
        Me.cmbChequeLayout.FormattingEnabled = True
        Me.cmbChequeLayout.Location = New System.Drawing.Point(132, 202)
        Me.cmbChequeLayout.Name = "cmbChequeLayout"
        Me.cmbChequeLayout.Size = New System.Drawing.Size(229, 21)
        Me.cmbChequeLayout.TabIndex = 13
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 205)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(79, 13)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "Cheque Layout"
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(183, 85)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 15
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'cmbBankType
        '
        Me.cmbBankType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cmbBankType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbBankType.FormattingEnabled = True
        Me.cmbBankType.Location = New System.Drawing.Point(132, 229)
        Me.cmbBankType.Name = "cmbBankType"
        Me.cmbBankType.Size = New System.Drawing.Size(229, 21)
        Me.cmbBankType.TabIndex = 16
        '
        'lblBankType
        '
        Me.lblBankType.AutoSize = True
        Me.lblBankType.Location = New System.Drawing.Point(12, 232)
        Me.lblBankType.Name = "lblBankType"
        Me.lblBankType.Size = New System.Drawing.Size(59, 13)
        Me.lblBankType.TabIndex = 12
        Me.lblBankType.Text = "Bank Type"
        '
        'txtDesignatedTo
        '
        Me.txtDesignatedTo.Location = New System.Drawing.Point(132, 256)
        Me.txtDesignatedTo.Name = "txtDesignatedTo"
        Me.txtDesignatedTo.Size = New System.Drawing.Size(229, 20)
        Me.txtDesignatedTo.TabIndex = 11
        '
        'lblDesignatedTo
        '
        Me.lblDesignatedTo.AutoSize = True
        Me.lblDesignatedTo.Location = New System.Drawing.Point(12, 259)
        Me.lblDesignatedTo.Name = "lblDesignatedTo"
        Me.lblDesignatedTo.Size = New System.Drawing.Size(77, 13)
        Me.lblDesignatedTo.TabIndex = 10
        Me.lblDesignatedTo.Text = "Designated To"
        '
        'pnlHeader
        '
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 25)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(532, 38)
        Me.pnlHeader.TabIndex = 76
        '
        'frmBranchNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(532, 472)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.cmbBankType)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.lblBankType)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cmbChequeLayout)
        Me.Controls.Add(Me.lblDesignatedTo)
        Me.Controls.Add(Me.txtDesignatedTo)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtBranchPhoneNo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtBankName)
        Me.Controls.Add(Me.grdBranch)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtBrabchCode)
        Me.Controls.Add(Me.txtAccountNumber)
        Me.Controls.Add(Me.txtHolderName)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmBranchNew"
        Me.Text = "Bank Information"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.grdBranch, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtHolderName As System.Windows.Forms.TextBox
    Friend WithEvents txtAccountNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtBrabchCode As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents grdBranch As Janus.Windows.GridEX.GridEX
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtBankName As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtBranchPhoneNo As System.Windows.Forms.TextBox
    Friend WithEvents cmbChequeLayout As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents cmbBankType As System.Windows.Forms.ComboBox
    Friend WithEvents lblBankType As System.Windows.Forms.Label
    Friend WithEvents txtDesignatedTo As System.Windows.Forms.TextBox
    Friend WithEvents lblDesignatedTo As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

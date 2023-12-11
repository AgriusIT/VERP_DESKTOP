<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOrderStatusChange
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
        Dim grdLetterOfCreadit_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmOrderStatusChange))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripProgressBar1 = New System.Windows.Forms.ToolStripProgressBar()
        Me.btnGenerateInvoices = New System.Windows.Forms.ToolStripButton()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.cmbRootPlan = New System.Windows.Forms.ComboBox()
        Me.dtpOrderDate = New System.Windows.Forms.DateTimePicker()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.grdLetterOfCreadit = New Janus.Windows.GridEX.GridEX()
        Me.cmbSalesPerson = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblProgress = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.grdLetterOfCreadit, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripProgressBar1, Me.btnGenerateInvoices})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(688, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripProgressBar1
        '
        Me.ToolStripProgressBar1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripProgressBar1.Name = "ToolStripProgressBar1"
        Me.ToolStripProgressBar1.Size = New System.Drawing.Size(200, 22)
        Me.ToolStripProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ToolStripProgressBar1.Visible = False
        '
        'btnGenerateInvoices
        '
        Me.btnGenerateInvoices.Image = Global.SimpleAccounts.My.Resources.Resources.copy_doc
        Me.btnGenerateInvoices.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnGenerateInvoices.Name = "btnGenerateInvoices"
        Me.btnGenerateInvoices.Size = New System.Drawing.Size(115, 22)
        Me.btnGenerateInvoices.Text = "Generate Invoice"
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(5, 7)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(159, 23)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Order Process"
        '
        'cmbRootPlan
        '
        Me.cmbRootPlan.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbRootPlan.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbRootPlan.FormattingEnabled = True
        Me.cmbRootPlan.Location = New System.Drawing.Point(88, 97)
        Me.cmbRootPlan.Name = "cmbRootPlan"
        Me.cmbRootPlan.Size = New System.Drawing.Size(270, 21)
        Me.cmbRootPlan.TabIndex = 2
        '
        'dtpOrderDate
        '
        Me.dtpOrderDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpOrderDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpOrderDate.Location = New System.Drawing.Point(88, 71)
        Me.dtpOrderDate.Name = "dtpOrderDate"
        Me.dtpOrderDate.Size = New System.Drawing.Size(117, 20)
        Me.dtpOrderDate.TabIndex = 3
        '
        'btnLoad
        '
        Me.btnLoad.Location = New System.Drawing.Point(88, 151)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(80, 23)
        Me.btnLoad.TabIndex = 4
        Me.btnLoad.Text = "Load Orders"
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 75)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Order Date:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 100)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(63, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Route Plan:"
        '
        'grdLetterOfCreadit
        '
        Me.grdLetterOfCreadit.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grdLetterOfCreadit_DesignTimeLayout.LayoutString = resources.GetString("grdLetterOfCreadit_DesignTimeLayout.LayoutString")
        Me.grdLetterOfCreadit.DesignTimeLayout = grdLetterOfCreadit_DesignTimeLayout
        Me.grdLetterOfCreadit.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdLetterOfCreadit.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdLetterOfCreadit.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdLetterOfCreadit.GroupByBoxVisible = False
        Me.grdLetterOfCreadit.Location = New System.Drawing.Point(1, 211)
        Me.grdLetterOfCreadit.Name = "grdLetterOfCreadit"
        Me.grdLetterOfCreadit.RecordNavigator = True
        Me.grdLetterOfCreadit.Size = New System.Drawing.Size(686, 499)
        Me.grdLetterOfCreadit.TabIndex = 7
        Me.grdLetterOfCreadit.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'cmbSalesPerson
        '
        Me.cmbSalesPerson.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cmbSalesPerson.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cmbSalesPerson.FormattingEnabled = True
        Me.cmbSalesPerson.Location = New System.Drawing.Point(88, 124)
        Me.cmbSalesPerson.Name = "cmbSalesPerson"
        Me.cmbSalesPerson.Size = New System.Drawing.Size(270, 21)
        Me.cmbSalesPerson.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 127)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(67, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Sale Person:"
        '
        'lblProgress
        '
        Me.lblProgress.BackColor = System.Drawing.Color.LightYellow
        Me.lblProgress.ForeColor = System.Drawing.Color.Navy
        Me.lblProgress.Location = New System.Drawing.Point(214, 211)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(263, 45)
        Me.lblProgress.TabIndex = 21
        Me.lblProgress.Text = "Processing please wait ..."
        Me.lblProgress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblProgress.Visible = False
        '
        'pnlHeader
        '
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 25)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(688, 38)
        Me.pnlHeader.TabIndex = 22
        '
        'frmOrderStatusChange
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(688, 712)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.lblProgress)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbSalesPerson)
        Me.Controls.Add(Me.grdLetterOfCreadit)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.dtpOrderDate)
        Me.Controls.Add(Me.cmbRootPlan)
        Me.Controls.Add(Me.ToolStrip1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOrderStatusChange"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "frmOrderStatusChange"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.grdLetterOfCreadit, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripProgressBar1 As System.Windows.Forms.ToolStripProgressBar
    Friend WithEvents btnGenerateInvoices As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents cmbRootPlan As System.Windows.Forms.ComboBox
    Friend WithEvents dtpOrderDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents grdLetterOfCreadit As Janus.Windows.GridEX.GridEX
    Friend WithEvents cmbSalesPerson As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

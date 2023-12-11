<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPOInvoicePerforma
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPOInvoicePerforma))
        Dim UltraTab1 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab2 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.txtPerformaNo = New System.Windows.Forms.TextBox()
        Me.lblPerformaNo = New System.Windows.Forms.Label()
        Me.cmbOrigin = New System.Windows.Forms.ComboBox()
        Me.cmbShippment = New System.Windows.Forms.ComboBox()
        Me.lblShippment = New System.Windows.Forms.Label()
        Me.dtpIndentGenerate = New System.Windows.Forms.DateTimePicker()
        Me.lblOrigin = New System.Windows.Forms.Label()
        Me.lblDate = New System.Windows.Forms.Label()
        Me.txtInstructions = New System.Windows.Forms.TextBox()
        Me.lblSpecialProvisions = New System.Windows.Forms.Label()
        Me.txtIndent = New System.Windows.Forms.TextBox()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.lblIndentNo = New System.Windows.Forms.Label()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdSaved = New Janus.Windows.GridEX.GridEX()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnNew = New System.Windows.Forms.ToolStripButton()
        Me.btnSave = New System.Windows.Forms.ToolStripButton()
        Me.btnEdit = New System.Windows.Forms.ToolStripButton()
        Me.btnDelete = New System.Windows.Forms.ToolStripButton()
        Me.btnPrint = New System.Windows.Forms.ToolStripButton()
        Me.btnCancel = New System.Windows.Forms.ToolStripButton()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.lblErrorStatus = New System.Windows.Forms.Label()
        Me.gboxAddStockDispatchStatus = New System.Windows.Forms.GroupBox()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.gboxAddStockDispatchStatus.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.txtPerformaNo)
        Me.UltraTabPageControl1.Controls.Add(Me.lblPerformaNo)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbOrigin)
        Me.UltraTabPageControl1.Controls.Add(Me.cmbShippment)
        Me.UltraTabPageControl1.Controls.Add(Me.lblShippment)
        Me.UltraTabPageControl1.Controls.Add(Me.dtpIndentGenerate)
        Me.UltraTabPageControl1.Controls.Add(Me.lblOrigin)
        Me.UltraTabPageControl1.Controls.Add(Me.lblDate)
        Me.UltraTabPageControl1.Controls.Add(Me.txtInstructions)
        Me.UltraTabPageControl1.Controls.Add(Me.lblSpecialProvisions)
        Me.UltraTabPageControl1.Controls.Add(Me.txtIndent)
        Me.UltraTabPageControl1.Controls.Add(Me.lblIndentNo)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(543, 349)
        '
        'txtPerformaNo
        '
        Me.txtPerformaNo.Location = New System.Drawing.Point(249, 75)
        Me.txtPerformaNo.Name = "txtPerformaNo"
        Me.txtPerformaNo.ReadOnly = True
        Me.txtPerformaNo.Size = New System.Drawing.Size(182, 21)
        Me.txtPerformaNo.TabIndex = 14
        '
        'lblPerformaNo
        '
        Me.lblPerformaNo.AutoSize = True
        Me.lblPerformaNo.Location = New System.Drawing.Point(144, 78)
        Me.lblPerformaNo.Name = "lblPerformaNo"
        Me.lblPerformaNo.Size = New System.Drawing.Size(79, 13)
        Me.lblPerformaNo.TabIndex = 13
        Me.lblPerformaNo.Text = "Performa No"
        '
        'cmbOrigin
        '
        Me.cmbOrigin.FormattingEnabled = True
        Me.cmbOrigin.Location = New System.Drawing.Point(249, 264)
        Me.cmbOrigin.Name = "cmbOrigin"
        Me.cmbOrigin.Size = New System.Drawing.Size(182, 21)
        Me.cmbOrigin.TabIndex = 10
        '
        'cmbShippment
        '
        Me.cmbShippment.FormattingEnabled = True
        Me.cmbShippment.Items.AddRange(New Object() {"By Sea", "By Air", "By Train"})
        Me.cmbShippment.Location = New System.Drawing.Point(249, 156)
        Me.cmbShippment.Name = "cmbShippment"
        Me.cmbShippment.Size = New System.Drawing.Size(182, 21)
        Me.cmbShippment.TabIndex = 6
        '
        'lblShippment
        '
        Me.lblShippment.AutoSize = True
        Me.lblShippment.Location = New System.Drawing.Point(144, 159)
        Me.lblShippment.Name = "lblShippment"
        Me.lblShippment.Size = New System.Drawing.Size(68, 13)
        Me.lblShippment.TabIndex = 5
        Me.lblShippment.Text = "Shippment"
        '
        'dtpIndentGenerate
        '
        Me.dtpIndentGenerate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpIndentGenerate.Location = New System.Drawing.Point(249, 129)
        Me.dtpIndentGenerate.Name = "dtpIndentGenerate"
        Me.dtpIndentGenerate.Size = New System.Drawing.Size(182, 21)
        Me.dtpIndentGenerate.TabIndex = 4
        '
        'lblOrigin
        '
        Me.lblOrigin.AutoSize = True
        Me.lblOrigin.Location = New System.Drawing.Point(144, 267)
        Me.lblOrigin.Name = "lblOrigin"
        Me.lblOrigin.Size = New System.Drawing.Size(41, 13)
        Me.lblOrigin.TabIndex = 9
        Me.lblOrigin.Text = "Origin"
        '
        'lblDate
        '
        Me.lblDate.AutoSize = True
        Me.lblDate.Location = New System.Drawing.Point(144, 135)
        Me.lblDate.Name = "lblDate"
        Me.lblDate.Size = New System.Drawing.Size(34, 13)
        Me.lblDate.TabIndex = 3
        Me.lblDate.Text = "Date"
        '
        'txtInstructions
        '
        Me.txtInstructions.Location = New System.Drawing.Point(249, 183)
        Me.txtInstructions.Multiline = True
        Me.txtInstructions.Name = "txtInstructions"
        Me.txtInstructions.Size = New System.Drawing.Size(182, 75)
        Me.txtInstructions.TabIndex = 8
        '
        'lblSpecialProvisions
        '
        Me.lblSpecialProvisions.AutoSize = True
        Me.lblSpecialProvisions.Location = New System.Drawing.Point(144, 186)
        Me.lblSpecialProvisions.Name = "lblSpecialProvisions"
        Me.lblSpecialProvisions.Size = New System.Drawing.Size(74, 13)
        Me.lblSpecialProvisions.TabIndex = 7
        Me.lblSpecialProvisions.Text = "Instructions"
        '
        'txtIndent
        '
        Me.txtIndent.Location = New System.Drawing.Point(249, 102)
        Me.txtIndent.Name = "txtIndent"
        Me.txtIndent.ReadOnly = True
        Me.txtIndent.Size = New System.Drawing.Size(182, 21)
        Me.txtIndent.TabIndex = 2
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(11, 14)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(233, 23)
        Me.lblHeader.TabIndex = 0
        Me.lblHeader.Text = "PO Performa Invoice"
        '
        'lblIndentNo
        '
        Me.lblIndentNo.AutoSize = True
        Me.lblIndentNo.Location = New System.Drawing.Point(144, 105)
        Me.lblIndentNo.Name = "lblIndentNo"
        Me.lblIndentNo.Size = New System.Drawing.Size(84, 13)
        Me.lblIndentNo.TabIndex = 1
        Me.lblIndentNo.Text = "Document No"
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grdSaved)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(543, 349)
        '
        'grdSaved
        '
        Me.grdSaved.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdSaved.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSaved.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdSaved.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdSaved.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdSaved.GroupByBoxVisible = False
        Me.grdSaved.Location = New System.Drawing.Point(0, 0)
        Me.grdSaved.Name = "grdSaved"
        Me.grdSaved.RecordNavigator = True
        Me.grdSaved.Size = New System.Drawing.Size(543, 349)
        Me.grdSaved.TabIndex = 0
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnNew, Me.btnSave, Me.btnEdit, Me.btnDelete, Me.btnPrint, Me.btnCancel})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(545, 25)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnNew
        '
        Me.btnNew.Image = Global.SimpleAccounts.My.Resources.Resources.BtnNew_Image
        Me.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(51, 22)
        Me.btnNew.Text = "&New"
        '
        'btnSave
        '
        Me.btnSave.Image = Global.SimpleAccounts.My.Resources.Resources.BtnSave_Image
        Me.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(51, 22)
        Me.btnSave.Text = "&Save"
        '
        'btnEdit
        '
        Me.btnEdit.Image = Global.SimpleAccounts.My.Resources.Resources.BtnEdit_Image
        Me.btnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(47, 22)
        Me.btnEdit.Text = "&Edit"
        '
        'btnDelete
        '
        Me.btnDelete.Image = Global.SimpleAccounts.My.Resources.Resources.BtnDelete_Image
        Me.btnDelete.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(60, 22)
        Me.btnDelete.Text = "&Delete"
        '
        'btnPrint
        '
        Me.btnPrint.Image = Global.SimpleAccounts.My.Resources.Resources.print
        Me.btnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(52, 22)
        Me.btnPrint.Text = "&Print"
        '
        'btnCancel
        '
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(63, 22)
        Me.btnCancel.Text = "&Cancel"
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 25)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(545, 370)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 1
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab1.TabPage = Me.UltraTabPageControl1
        UltraTab1.Text = "Performa Generation"
        UltraTab2.TabPage = Me.UltraTabPageControl2
        UltraTab2.Text = "History"
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab1, UltraTab2})
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(543, 349)
        '
        'lblErrorStatus
        '
        Me.lblErrorStatus.AutoSize = True
        Me.lblErrorStatus.ForeColor = System.Drawing.Color.Red
        Me.lblErrorStatus.Location = New System.Drawing.Point(113, 37)
        Me.lblErrorStatus.Name = "lblErrorStatus"
        Me.lblErrorStatus.Size = New System.Drawing.Size(69, 13)
        Me.lblErrorStatus.TabIndex = 4
        Me.lblErrorStatus.Text = "lblErrorStatus"
        Me.lblErrorStatus.Visible = False
        '
        'gboxAddStockDispatchStatus
        '
        Me.gboxAddStockDispatchStatus.Controls.Add(Me.txtStatus)
        Me.gboxAddStockDispatchStatus.Controls.Add(Me.Label1)
        Me.gboxAddStockDispatchStatus.Location = New System.Drawing.Point(49, 53)
        Me.gboxAddStockDispatchStatus.Name = "gboxAddStockDispatchStatus"
        Me.gboxAddStockDispatchStatus.Size = New System.Drawing.Size(270, 56)
        Me.gboxAddStockDispatchStatus.TabIndex = 2
        Me.gboxAddStockDispatchStatus.TabStop = False
        Me.gboxAddStockDispatchStatus.Text = "Add Status"
        '
        'txtStatus
        '
        Me.txtStatus.Location = New System.Drawing.Point(49, 26)
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.Size = New System.Drawing.Size(206, 20)
        Me.txtStatus.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 29)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(37, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Status"
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(543, 54)
        Me.pnlHeader.TabIndex = 17
        '
        'frmPOInvoicePerforma
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(545, 395)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPOInvoicePerforma"
        Me.Text = "frmPOInvoicePerforma"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.UltraTabPageControl1.PerformLayout()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.gboxAddStockDispatchStatus.ResumeLayout(False)
        Me.gboxAddStockDispatchStatus.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents dtpIndentGenerate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblDate As System.Windows.Forms.Label
    Friend WithEvents txtIndent As System.Windows.Forms.TextBox
    Friend WithEvents lblIndentNo As System.Windows.Forms.Label
    Friend WithEvents txtInstructions As System.Windows.Forms.TextBox
    Friend WithEvents lblSpecialProvisions As System.Windows.Forms.Label
    Friend WithEvents lblOrigin As System.Windows.Forms.Label
    Friend WithEvents cmbShippment As System.Windows.Forms.ComboBox
    Friend WithEvents lblShippment As System.Windows.Forms.Label
    Friend WithEvents cmbOrigin As System.Windows.Forms.ComboBox
    Friend WithEvents grdSaved As Janus.Windows.GridEX.GridEX
    Friend WithEvents txtPerformaNo As System.Windows.Forms.TextBox
    Friend WithEvents lblPerformaNo As System.Windows.Forms.Label
    Friend WithEvents btnCancel As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblErrorStatus As System.Windows.Forms.Label
    Friend WithEvents gboxAddStockDispatchStatus As System.Windows.Forms.GroupBox
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

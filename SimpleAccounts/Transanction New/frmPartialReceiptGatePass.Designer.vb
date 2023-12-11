<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPartialReceiptGatePass
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
        Dim grd_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPartialReceiptGatePass))
        Dim UltraTab6 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Dim UltraTab7 As Infragistics.Win.UltraWinTabControl.UltraTab = New Infragistics.Win.UltraWinTabControl.UltraTab()
        Me.UltraTabPageControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtReceivingDate = New System.Windows.Forms.DateTimePicker()
        Me.txtDocNo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.grd = New Janus.Windows.GridEX.GridEX()
        Me.UltraTabPageControl2 = New Infragistics.Win.UltraWinTabControl.UltraTabPageControl()
        Me.grdSaved = New Janus.Windows.GridEX.GridEX()
        Me.UltraTabControl1 = New Infragistics.Win.UltraWinTabControl.UltraTabControl()
        Me.UltraTabSharedControlsPage1 = New Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage()
        Me.BtnNew = New System.Windows.Forms.ToolStripButton()
        Me.BtnEdit = New System.Windows.Forms.ToolStripButton()
        Me.BtnSave = New System.Windows.Forms.ToolStripButton()
        Me.BtnDelete = New System.Windows.Forms.ToolStripButton()
        Me.BtnPrint = New System.Windows.Forms.ToolStripButton()
        Me.toolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.HelpToolStripButton = New System.Windows.Forms.ToolStripButton()
        Me.btnLoadDemand = New System.Windows.Forms.ToolStripButton()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CtrlGrdBarFront = New SimpleAccounts.CtrlGrdBar()
        Me.CtrlGrdBarHistory = New SimpleAccounts.CtrlGrdBar()
        Me.UltraTabPageControl1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabPageControl2.SuspendLayout()
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.UltraTabControl1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'UltraTabPageControl1
        '
        Me.UltraTabPageControl1.Controls.Add(Me.pnlHeader)
        Me.UltraTabPageControl1.Controls.Add(Me.Panel1)
        Me.UltraTabPageControl1.Controls.Add(Me.grd)
        Me.UltraTabPageControl1.Location = New System.Drawing.Point(1, 1)
        Me.UltraTabPageControl1.Name = "UltraTabPageControl1"
        Me.UltraTabPageControl1.Size = New System.Drawing.Size(910, 359)
        '
        'pnlHeader
        '
        Me.pnlHeader.BackColor = System.Drawing.Color.FromArgb(CType(CType(12, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 0)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(910, 42)
        Me.pnlHeader.TabIndex = 117
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.lblHeader.Location = New System.Drawing.Point(23, 10)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(315, 23)
        Me.lblHeader.TabIndex = 1
        Me.lblHeader.Text = "Partial Receipts of GatePass"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.dtReceivingDate)
        Me.Panel1.Controls.Add(Me.txtDocNo)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.txtRemarks)
        Me.Panel1.Location = New System.Drawing.Point(2, 48)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(888, 55)
        Me.Panel1.TabIndex = 115
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(278, 8)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 13)
        Me.Label2.TabIndex = 18
        Me.Label2.Text = "Remarks"
        '
        'dtReceivingDate
        '
        Me.dtReceivingDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.dtReceivingDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtReceivingDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtReceivingDate.Location = New System.Drawing.Point(144, 24)
        Me.dtReceivingDate.Name = "dtReceivingDate"
        Me.dtReceivingDate.Size = New System.Drawing.Size(131, 20)
        Me.dtReceivingDate.TabIndex = 17
        '
        'txtDocNo
        '
        Me.txtDocNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.txtDocNo.Enabled = False
        Me.txtDocNo.Location = New System.Drawing.Point(11, 24)
        Me.txtDocNo.MaxLength = 50
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(129, 20)
        Me.txtDocNo.TabIndex = 15
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(143, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 16
        Me.Label3.Text = "Doc Date:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(8, 8)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(44, 13)
        Me.Label4.TabIndex = 14
        Me.Label4.Text = "Doc No"
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(281, 24)
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(240, 20)
        Me.txtRemarks.TabIndex = 11
        '
        'grd
        '
        Me.grd.AllowDelete = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        grd_DesignTimeLayout.LayoutString = resources.GetString("grd_DesignTimeLayout.LayoutString")
        Me.grd.DesignTimeLayout = grd_DesignTimeLayout
        Me.grd.Font = New System.Drawing.Font("Verdana", 8.25!)
        Me.grd.GroupByBoxVisible = False
        Me.grd.Location = New System.Drawing.Point(11, 109)
        Me.grd.Name = "grd"
        Me.grd.RecordNavigator = True
        Me.grd.Size = New System.Drawing.Size(888, 247)
        Me.grd.TabIndex = 116
        Me.grd.TabKeyBehavior = Janus.Windows.GridEX.TabKeyBehavior.ControlNavigation
        Me.grd.TabStop = False
        Me.grd.TotalRow = Janus.Windows.GridEX.InheritableBoolean.[True]
        Me.grd.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed
        Me.grd.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'UltraTabPageControl2
        '
        Me.UltraTabPageControl2.Controls.Add(Me.grdSaved)
        Me.UltraTabPageControl2.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabPageControl2.Name = "UltraTabPageControl2"
        Me.UltraTabPageControl2.Size = New System.Drawing.Size(910, 359)
        '
        'grdSaved
        '
        Me.grdSaved.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.[False]
        Me.grdSaved.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdSaved.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdSaved.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdSaved.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdSaved.Location = New System.Drawing.Point(0, 0)
        Me.grdSaved.Name = "grdSaved"
        Me.grdSaved.RecordNavigator = True
        Me.grdSaved.Size = New System.Drawing.Size(910, 359)
        Me.grdSaved.TabIndex = 0
        Me.grdSaved.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'UltraTabControl1
        '
        Me.UltraTabControl1.Controls.Add(Me.UltraTabSharedControlsPage1)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl2)
        Me.UltraTabControl1.Controls.Add(Me.UltraTabPageControl1)
        Me.UltraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UltraTabControl1.Location = New System.Drawing.Point(0, 25)
        Me.UltraTabControl1.Name = "UltraTabControl1"
        Me.UltraTabControl1.SharedControlsPage = Me.UltraTabSharedControlsPage1
        Me.UltraTabControl1.Size = New System.Drawing.Size(912, 380)
        Me.UltraTabControl1.Style = Infragistics.Win.UltraWinTabControl.UltraTabControlStyle.Excel
        Me.UltraTabControl1.TabIndex = 117
        Me.UltraTabControl1.TabOrientation = Infragistics.Win.UltraWinTabs.TabOrientation.BottomLeft
        UltraTab6.TabPage = Me.UltraTabPageControl1
        UltraTab6.Text = "Partial Receipt GatePass"
        UltraTab7.TabPage = Me.UltraTabPageControl2
        UltraTab7.Text = "History "
        Me.UltraTabControl1.Tabs.AddRange(New Infragistics.Win.UltraWinTabControl.UltraTab() {UltraTab6, UltraTab7})
        Me.UltraTabControl1.TabStop = False
        Me.UltraTabControl1.ViewStyle = Infragistics.Win.UltraWinTabControl.ViewStyle.Office2007
        '
        'UltraTabSharedControlsPage1
        '
        Me.UltraTabSharedControlsPage1.Location = New System.Drawing.Point(-10000, -10000)
        Me.UltraTabSharedControlsPage1.Name = "UltraTabSharedControlsPage1"
        Me.UltraTabSharedControlsPage1.Size = New System.Drawing.Size(910, 359)
        '
        'BtnNew
        '
        Me.BtnNew.Image = CType(resources.GetObject("BtnNew.Image"), System.Drawing.Image)
        Me.BtnNew.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnNew.Name = "BtnNew"
        Me.BtnNew.Size = New System.Drawing.Size(51, 22)
        Me.BtnNew.Text = "&New"
        '
        'BtnEdit
        '
        Me.BtnEdit.Image = CType(resources.GetObject("BtnEdit.Image"), System.Drawing.Image)
        Me.BtnEdit.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnEdit.Name = "BtnEdit"
        Me.BtnEdit.Size = New System.Drawing.Size(47, 22)
        Me.BtnEdit.Text = "&Edit"
        '
        'BtnSave
        '
        Me.BtnSave.Image = CType(resources.GetObject("BtnSave.Image"), System.Drawing.Image)
        Me.BtnSave.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnSave.Name = "BtnSave"
        Me.BtnSave.Size = New System.Drawing.Size(51, 22)
        Me.BtnSave.Text = "&Save"
        '
        'BtnDelete
        '
        Me.BtnDelete.Image = CType(resources.GetObject("BtnDelete.Image"), System.Drawing.Image)
        Me.BtnDelete.Name = "BtnDelete"
        Me.BtnDelete.RightToLeftAutoMirrorImage = True
        Me.BtnDelete.Size = New System.Drawing.Size(60, 22)
        Me.BtnDelete.Text = "&Delete"
        '
        'BtnPrint
        '
        Me.BtnPrint.Image = CType(resources.GetObject("BtnPrint.Image"), System.Drawing.Image)
        Me.BtnPrint.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.BtnPrint.Name = "BtnPrint"
        Me.BtnPrint.Size = New System.Drawing.Size(52, 22)
        Me.BtnPrint.Text = "&Print"
        '
        'toolStripSeparator1
        '
        Me.toolStripSeparator1.Name = "toolStripSeparator1"
        Me.toolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'HelpToolStripButton
        '
        Me.HelpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.HelpToolStripButton.Image = CType(resources.GetObject("HelpToolStripButton.Image"), System.Drawing.Image)
        Me.HelpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.HelpToolStripButton.Name = "HelpToolStripButton"
        Me.HelpToolStripButton.Size = New System.Drawing.Size(23, 22)
        Me.HelpToolStripButton.Text = "He&lp"
        '
        'btnLoadDemand
        '
        Me.btnLoadDemand.Image = Global.SimpleAccounts.My.Resources.Resources.Copy
        Me.btnLoadDemand.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnLoadDemand.Name = "btnLoadDemand"
        Me.btnLoadDemand.Size = New System.Drawing.Size(162, 22)
        Me.btnLoadDemand.Text = "Load GatePass Document"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.BtnNew, Me.BtnEdit, Me.BtnSave, Me.BtnDelete, Me.BtnPrint, Me.toolStripSeparator1, Me.HelpToolStripButton, Me.btnLoadDemand})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(912, 25)
        Me.ToolStrip1.TabIndex = 113
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'CtrlGrdBarFront
        '
        Me.CtrlGrdBarFront.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBarFront.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBarFront.Email = Nothing
        Me.CtrlGrdBarFront.FormName = Nothing
        Me.CtrlGrdBarFront.Location = New System.Drawing.Point(828, 0)
        Me.CtrlGrdBarFront.MyGrid = Me.grd
        Me.CtrlGrdBarFront.Name = "CtrlGrdBarFront"
        Me.CtrlGrdBarFront.Size = New System.Drawing.Size(33, 25)
        Me.CtrlGrdBarFront.TabIndex = 119
        Me.CtrlGrdBarFront.TabStop = False
        Me.CtrlGrdBarFront.Visible = False
        '
        'CtrlGrdBarHistory
        '
        Me.CtrlGrdBarHistory.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CtrlGrdBarHistory.BackColor = System.Drawing.SystemColors.Control
        Me.CtrlGrdBarHistory.Email = Nothing
        Me.CtrlGrdBarHistory.FormName = Nothing
        Me.CtrlGrdBarHistory.Location = New System.Drawing.Point(867, 0)
        Me.CtrlGrdBarHistory.MyGrid = Me.grdSaved
        Me.CtrlGrdBarHistory.Name = "CtrlGrdBarHistory"
        Me.CtrlGrdBarHistory.Size = New System.Drawing.Size(33, 25)
        Me.CtrlGrdBarHistory.TabIndex = 118
        Me.CtrlGrdBarHistory.TabStop = False
        Me.CtrlGrdBarHistory.Visible = False
        '
        'frmPartialReceiptGatePass
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(912, 405)
        Me.Controls.Add(Me.CtrlGrdBarFront)
        Me.Controls.Add(Me.CtrlGrdBarHistory)
        Me.Controls.Add(Me.UltraTabControl1)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Name = "frmPartialReceiptGatePass"
        Me.Text = "Partial Receipt GatePass"
        Me.UltraTabPageControl1.ResumeLayout(False)
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabPageControl2.ResumeLayout(False)
        CType(Me.grdSaved, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UltraTabControl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.UltraTabControl1.ResumeLayout(False)
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents grd As Janus.Windows.GridEX.GridEX
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtReceivingDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtDocNo As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents UltraTabControl1 As Infragistics.Win.UltraWinTabControl.UltraTabControl
    Friend WithEvents UltraTabSharedControlsPage1 As Infragistics.Win.UltraWinTabControl.UltraTabSharedControlsPage
    Friend WithEvents UltraTabPageControl2 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents grdSaved As Janus.Windows.GridEX.GridEX
    Friend WithEvents UltraTabPageControl1 As Infragistics.Win.UltraWinTabControl.UltraTabPageControl
    Friend WithEvents BtnNew As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnEdit As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnSave As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnDelete As System.Windows.Forms.ToolStripButton
    Friend WithEvents BtnPrint As System.Windows.Forms.ToolStripButton
    Friend WithEvents toolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents HelpToolStripButton As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnLoadDemand As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents CtrlGrdBarHistory As SimpleAccounts.CtrlGrdBar
    Friend WithEvents CtrlGrdBarFront As SimpleAccounts.CtrlGrdBar
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
    Friend WithEvents lblHeader As System.Windows.Forms.Label
End Class

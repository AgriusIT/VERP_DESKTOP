<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLoadJobCards
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
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.lblJobCard = New System.Windows.Forms.Label()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.cmbJobCard = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.dtpFromDate = New System.Windows.Forms.DateTimePicker()
        Me.lblFromDate = New System.Windows.Forms.Label()
        Me.lblToDate = New System.Windows.Forms.Label()
        Me.dtpToDate = New System.Windows.Forms.DateTimePicker()
        Me.lnkRefresh = New System.Windows.Forms.LinkLabel()
        Me.rboRegNo = New System.Windows.Forms.RadioButton()
        Me.rboCustomer = New System.Windows.Forms.RadioButton()
        Me.rdoNewInvocie = New System.Windows.Forms.RadioButton()
        Me.rdoPreInvoice = New System.Windows.Forms.RadioButton()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.pnlHeader = New System.Windows.Forms.Panel()
        Me.ToolStrip1.SuspendLayout()
        CType(Me.cmbJobCard, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.pnlHeader.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblJobCard
        '
        Me.lblJobCard.AutoSize = True
        Me.lblJobCard.Location = New System.Drawing.Point(5, 115)
        Me.lblJobCard.Name = "lblJobCard"
        Me.lblJobCard.Size = New System.Drawing.Size(49, 13)
        Me.lblJobCard.TabIndex = 0
        Me.lblJobCard.Text = "Job Card"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(436, 25)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'btnRefresh
        '
        Me.btnRefresh.Image = Global.SimpleAccounts.My.Resources.Resources.Refresh
        Me.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnRefresh.Name = "btnRefresh"
        Me.btnRefresh.Size = New System.Drawing.Size(66, 22)
        Me.btnRefresh.Text = "&Refresh"
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.Navy
        Me.lblHeader.Location = New System.Drawing.Point(4, 5)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(114, 23)
        Me.lblHeader.TabIndex = 3
        Me.lblHeader.Text = "Job Cards"
        '
        'cmbJobCard
        '
        Me.cmbJobCard.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cmbJobCard.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbJobCard.CheckedListSettings.CheckStateMember = ""
        Appearance1.BackColor = System.Drawing.SystemColors.Window
        Appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption
        Me.cmbJobCard.DisplayLayout.Appearance = Appearance1
        Me.cmbJobCard.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.cmbJobCard.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder
        Appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance2.BorderColor = System.Drawing.SystemColors.Window
        Me.cmbJobCard.DisplayLayout.GroupByBox.Appearance = Appearance2
        Appearance4.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cmbJobCard.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance4
        Me.cmbJobCard.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance3.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance3.BackColor2 = System.Drawing.SystemColors.Control
        Appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance3.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cmbJobCard.DisplayLayout.GroupByBox.PromptAppearance = Appearance3
        Me.cmbJobCard.DisplayLayout.MaxColScrollRegions = 1
        Me.cmbJobCard.DisplayLayout.MaxRowScrollRegions = 1
        Appearance7.BackColor = System.Drawing.SystemColors.Window
        Appearance7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmbJobCard.DisplayLayout.Override.ActiveCellAppearance = Appearance7
        Appearance10.BackColor = System.Drawing.SystemColors.Highlight
        Appearance10.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.cmbJobCard.DisplayLayout.Override.ActiveRowAppearance = Appearance10
        Me.cmbJobCard.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.cmbJobCard.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance12.BackColor = System.Drawing.SystemColors.Window
        Me.cmbJobCard.DisplayLayout.Override.CardAreaAppearance = Appearance12
        Appearance8.BorderColor = System.Drawing.Color.Silver
        Appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.cmbJobCard.DisplayLayout.Override.CellAppearance = Appearance8
        Me.cmbJobCard.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.cmbJobCard.DisplayLayout.Override.CellPadding = 0
        Appearance6.BackColor = System.Drawing.SystemColors.Control
        Appearance6.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance6.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance6.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance6.BorderColor = System.Drawing.SystemColors.Window
        Me.cmbJobCard.DisplayLayout.Override.GroupByRowAppearance = Appearance6
        Appearance5.TextHAlignAsString = "Left"
        Me.cmbJobCard.DisplayLayout.Override.HeaderAppearance = Appearance5
        Me.cmbJobCard.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.cmbJobCard.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance11.BackColor = System.Drawing.SystemColors.Window
        Appearance11.BorderColor = System.Drawing.Color.Silver
        Me.cmbJobCard.DisplayLayout.Override.RowAppearance = Appearance11
        Me.cmbJobCard.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance9.BackColor = System.Drawing.SystemColors.ControlLight
        Me.cmbJobCard.DisplayLayout.Override.TemplateAddRowAppearance = Appearance9
        Me.cmbJobCard.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbJobCard.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbJobCard.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.cmbJobCard.Location = New System.Drawing.Point(67, 111)
        Me.cmbJobCard.Name = "cmbJobCard"
        Me.cmbJobCard.Size = New System.Drawing.Size(313, 22)
        Me.cmbJobCard.TabIndex = 4
        '
        'btnLoad
        '
        Me.btnLoad.Location = New System.Drawing.Point(329, 139)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(51, 23)
        Me.btnLoad.TabIndex = 5
        Me.btnLoad.Text = "Load"
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'dtpFromDate
        '
        Me.dtpFromDate.CustomFormat = "dd/M/yyyy"
        Me.dtpFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpFromDate.Location = New System.Drawing.Point(67, 66)
        Me.dtpFromDate.Name = "dtpFromDate"
        Me.dtpFromDate.Size = New System.Drawing.Size(126, 20)
        Me.dtpFromDate.TabIndex = 6
        '
        'lblFromDate
        '
        Me.lblFromDate.AutoSize = True
        Me.lblFromDate.Location = New System.Drawing.Point(5, 70)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(56, 13)
        Me.lblFromDate.TabIndex = 7
        Me.lblFromDate.Text = "From Date"
        '
        'lblToDate
        '
        Me.lblToDate.AutoSize = True
        Me.lblToDate.Location = New System.Drawing.Point(199, 70)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(46, 13)
        Me.lblToDate.TabIndex = 8
        Me.lblToDate.Text = "To Date"
        '
        'dtpToDate
        '
        Me.dtpToDate.CustomFormat = "dd/M/yyyy"
        Me.dtpToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpToDate.Location = New System.Drawing.Point(254, 64)
        Me.dtpToDate.Name = "dtpToDate"
        Me.dtpToDate.Size = New System.Drawing.Size(126, 20)
        Me.dtpToDate.TabIndex = 9
        '
        'lnkRefresh
        '
        Me.lnkRefresh.AutoSize = True
        Me.lnkRefresh.Location = New System.Drawing.Point(386, 115)
        Me.lnkRefresh.Name = "lnkRefresh"
        Me.lnkRefresh.Size = New System.Drawing.Size(50, 13)
        Me.lnkRefresh.TabIndex = 10
        Me.lnkRefresh.TabStop = True
        Me.lnkRefresh.Text = "(Refresh)"
        '
        'rboRegNo
        '
        Me.rboRegNo.AutoSize = True
        Me.rboRegNo.Checked = True
        Me.rboRegNo.Location = New System.Drawing.Point(3, 4)
        Me.rboRegNo.Name = "rboRegNo"
        Me.rboRegNo.Size = New System.Drawing.Size(62, 17)
        Me.rboRegNo.TabIndex = 11
        Me.rboRegNo.TabStop = True
        Me.rboRegNo.Text = "Reg No"
        Me.rboRegNo.UseVisualStyleBackColor = True
        '
        'rboCustomer
        '
        Me.rboCustomer.AutoSize = True
        Me.rboCustomer.Location = New System.Drawing.Point(78, 4)
        Me.rboCustomer.Name = "rboCustomer"
        Me.rboCustomer.Size = New System.Drawing.Size(69, 17)
        Me.rboCustomer.TabIndex = 12
        Me.rboCustomer.Text = "Customer"
        Me.rboCustomer.UseVisualStyleBackColor = True
        '
        'rdoNewInvocie
        '
        Me.rdoNewInvocie.AutoSize = True
        Me.rdoNewInvocie.Location = New System.Drawing.Point(165, 142)
        Me.rdoNewInvocie.Name = "rdoNewInvocie"
        Me.rdoNewInvocie.Size = New System.Drawing.Size(85, 17)
        Me.rdoNewInvocie.TabIndex = 13
        Me.rdoNewInvocie.Text = "New Invoice"
        Me.rdoNewInvocie.UseVisualStyleBackColor = True
        '
        'rdoPreInvoice
        '
        Me.rdoPreInvoice.AutoSize = True
        Me.rdoPreInvoice.Checked = True
        Me.rdoPreInvoice.Location = New System.Drawing.Point(247, 142)
        Me.rdoPreInvoice.Name = "rdoPreInvoice"
        Me.rdoPreInvoice.Size = New System.Drawing.Size(82, 17)
        Me.rdoPreInvoice.TabIndex = 14
        Me.rdoPreInvoice.TabStop = True
        Me.rdoPreInvoice.Text = "Pre. Inovice"
        Me.rdoPreInvoice.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.rboRegNo)
        Me.Panel1.Controls.Add(Me.rboCustomer)
        Me.Panel1.Location = New System.Drawing.Point(239, 88)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(145, 21)
        Me.Panel1.TabIndex = 15
        '
        'pnlHeader
        '
        Me.pnlHeader.Controls.Add(Me.lblHeader)
        Me.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlHeader.Location = New System.Drawing.Point(0, 25)
        Me.pnlHeader.Name = "pnlHeader"
        Me.pnlHeader.Size = New System.Drawing.Size(436, 33)
        Me.pnlHeader.TabIndex = 16
        '
        'frmLoadJobCards
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(436, 176)
        Me.Controls.Add(Me.pnlHeader)
        Me.Controls.Add(Me.rdoPreInvoice)
        Me.Controls.Add(Me.rdoNewInvocie)
        Me.Controls.Add(Me.lnkRefresh)
        Me.Controls.Add(Me.dtpToDate)
        Me.Controls.Add(Me.lblToDate)
        Me.Controls.Add(Me.lblFromDate)
        Me.Controls.Add(Me.dtpFromDate)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.cmbJobCard)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.lblJobCard)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "frmLoadJobCards"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Job Cards"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.cmbJobCard, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.pnlHeader.ResumeLayout(False)
        Me.pnlHeader.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblJobCard As System.Windows.Forms.Label
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblHeader As System.Windows.Forms.Label
    Friend WithEvents cmbJobCard As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents dtpFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblFromDate As System.Windows.Forms.Label
    Friend WithEvents lblToDate As System.Windows.Forms.Label
    Friend WithEvents dtpToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lnkRefresh As System.Windows.Forms.LinkLabel
    Friend WithEvents rboRegNo As System.Windows.Forms.RadioButton
    Friend WithEvents rboCustomer As System.Windows.Forms.RadioButton
    Friend WithEvents rdoNewInvocie As System.Windows.Forms.RadioButton
    Friend WithEvents rdoPreInvoice As System.Windows.Forms.RadioButton
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents pnlHeader As System.Windows.Forms.Panel
End Class

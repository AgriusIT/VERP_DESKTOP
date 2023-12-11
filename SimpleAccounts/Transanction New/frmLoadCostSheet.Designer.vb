<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLoadCostSheet
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
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.btnRefresh = New System.Windows.Forms.ToolStripButton
        Me.cmbItem = New Infragistics.Win.UltraWinGrid.UltraCombo
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnLoad = New System.Windows.Forms.Button
        Me.txtPlanQty = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.ToolStrip1.SuspendLayout()
        CType(Me.cmbItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnRefresh})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(358, 25)
        Me.ToolStrip1.TabIndex = 0
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
        'cmbItem
        '
        Me.cmbItem.CheckedListSettings.CheckStateMember = ""
        Appearance4.BackColor = System.Drawing.SystemColors.Window
        Appearance4.BorderColor = System.Drawing.SystemColors.InactiveCaption
        Me.cmbItem.DisplayLayout.Appearance = Appearance4
        Me.cmbItem.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.cmbItem.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Appearance1.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance1.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance1.BorderColor = System.Drawing.SystemColors.Window
        Me.cmbItem.DisplayLayout.GroupByBox.Appearance = Appearance1
        Appearance2.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cmbItem.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance2
        Me.cmbItem.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance3.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance3.BackColor2 = System.Drawing.SystemColors.Control
        Appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance3.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cmbItem.DisplayLayout.GroupByBox.PromptAppearance = Appearance3
        Me.cmbItem.DisplayLayout.MaxColScrollRegions = 1
        Me.cmbItem.DisplayLayout.MaxRowScrollRegions = 1
        Appearance12.BackColor = System.Drawing.SystemColors.Window
        Appearance12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmbItem.DisplayLayout.Override.ActiveCellAppearance = Appearance12
        Appearance7.BackColor = System.Drawing.SystemColors.Highlight
        Appearance7.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.cmbItem.DisplayLayout.Override.ActiveRowAppearance = Appearance7
        Me.cmbItem.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.cmbItem.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance6.BackColor = System.Drawing.SystemColors.Window
        Me.cmbItem.DisplayLayout.Override.CardAreaAppearance = Appearance6
        Appearance5.BorderColor = System.Drawing.Color.Silver
        Appearance5.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.cmbItem.DisplayLayout.Override.CellAppearance = Appearance5
        Me.cmbItem.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.cmbItem.DisplayLayout.Override.CellPadding = 0
        Appearance9.BackColor = System.Drawing.SystemColors.Control
        Appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance9.BorderColor = System.Drawing.SystemColors.Window
        Me.cmbItem.DisplayLayout.Override.GroupByRowAppearance = Appearance9
        Appearance11.TextHAlignAsString = "Left"
        Me.cmbItem.DisplayLayout.Override.HeaderAppearance = Appearance11
        Me.cmbItem.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.cmbItem.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance10.BackColor = System.Drawing.SystemColors.Window
        Appearance10.BorderColor = System.Drawing.Color.Silver
        Me.cmbItem.DisplayLayout.Override.RowAppearance = Appearance10
        Me.cmbItem.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance8.BackColor = System.Drawing.SystemColors.ControlLight
        Me.cmbItem.DisplayLayout.Override.TemplateAddRowAppearance = Appearance8
        Me.cmbItem.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbItem.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbItem.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.cmbItem.Location = New System.Drawing.Point(61, 47)
        Me.cmbItem.Name = "cmbItem"
        Me.cmbItem.Size = New System.Drawing.Size(285, 22)
        Me.cmbItem.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(8, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(27, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Item"
        '
        'btnLoad
        '
        Me.btnLoad.Location = New System.Drawing.Point(271, 137)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(75, 23)
        Me.btnLoad.TabIndex = 5
        Me.btnLoad.Text = "Load"
        Me.btnLoad.UseVisualStyleBackColor = True
        '
        'txtPlanQty
        '
        Me.txtPlanQty.Location = New System.Drawing.Point(61, 75)
        Me.txtPlanQty.Name = "txtPlanQty"
        Me.txtPlanQty.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.txtPlanQty.Size = New System.Drawing.Size(76, 20)
        Me.txtPlanQty.TabIndex = 4
        Me.txtPlanQty.Text = "1"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(8, 78)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Plan Qty"
        '
        'frmLoadCostSheet
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(358, 172)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtPlanQty)
        Me.Controls.Add(Me.btnLoad)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmbItem)
        Me.Controls.Add(Me.ToolStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLoadCostSheet"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Load Cost Sheet"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        CType(Me.cmbItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents btnRefresh As System.Windows.Forms.ToolStripButton
    Friend WithEvents cmbItem As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents txtPlanQty As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class

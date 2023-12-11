<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSearchSalesInquiry
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
        Dim Appearance118 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance119 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance120 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance121 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance122 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance123 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance124 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance125 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance126 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance127 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance128 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance129 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance130 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance1 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance2 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance7 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance8 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance9 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance10 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance11 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance12 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance13 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim grdItems_DesignTimeLayout As Janus.Windows.GridEX.GridEXLayout = New Janus.Windows.GridEX.GridEXLayout()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSearchSalesInquiry))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.OK_Button = New System.Windows.Forms.Button()
        Me.Cancel_Button = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.dtpInquiryToDate = New System.Windows.Forms.DateTimePicker()
        Me.dtpInquiryFromDate = New System.Windows.Forms.DateTimePicker()
        Me.cmbInquiryNumber = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.cmbReference = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.grdItems = New Janus.Windows.GridEX.GridEX()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.cmbInquiryNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmbReference, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OK_Button, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Cancel_Button, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(749, 402)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(146, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OK_Button
        '
        Me.OK_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.OK_Button.Location = New System.Drawing.Point(3, 3)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(67, 23)
        Me.OK_Button.TabIndex = 0
        Me.OK_Button.Text = "Load"
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Cancel_Button.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Cancel_Button.Location = New System.Drawing.Point(76, 3)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(67, 23)
        Me.Cancel_Button.TabIndex = 1
        Me.Cancel_Button.Text = "Cancel"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.AutoScroll = True
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.Panel1.Controls.Add(Me.Button1)
        Me.Panel1.Controls.Add(Me.dtpInquiryToDate)
        Me.Panel1.Controls.Add(Me.dtpInquiryFromDate)
        Me.Panel1.Controls.Add(Me.cmbInquiryNumber)
        Me.Panel1.Controls.Add(Me.cmbReference)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(883, 48)
        Me.Panel1.TabIndex = 1
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(802, 13)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 18
        Me.Button1.Text = "Show"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'dtpInquiryToDate
        '
        Me.dtpInquiryToDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.dtpInquiryToDate.Checked = False
        Me.dtpInquiryToDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpInquiryToDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpInquiryToDate.Location = New System.Drawing.Point(351, 16)
        Me.dtpInquiryToDate.Name = "dtpInquiryToDate"
        Me.dtpInquiryToDate.ShowCheckBox = True
        Me.dtpInquiryToDate.Size = New System.Drawing.Size(131, 20)
        Me.dtpInquiryToDate.TabIndex = 15
        '
        'dtpInquiryFromDate
        '
        Me.dtpInquiryFromDate.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.dtpInquiryFromDate.Checked = False
        Me.dtpInquiryFromDate.CustomFormat = "dd/MMM/yyyy"
        Me.dtpInquiryFromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpInquiryFromDate.Location = New System.Drawing.Point(214, 16)
        Me.dtpInquiryFromDate.Name = "dtpInquiryFromDate"
        Me.dtpInquiryFromDate.ShowCheckBox = True
        Me.dtpInquiryFromDate.Size = New System.Drawing.Size(131, 20)
        Me.dtpInquiryFromDate.TabIndex = 15
        '
        'cmbInquiryNumber
        '
        Me.cmbInquiryNumber.AlwaysInEditMode = True
        Appearance118.BackColor = System.Drawing.Color.White
        Me.cmbInquiryNumber.Appearance = Appearance118
        Me.cmbInquiryNumber.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cmbInquiryNumber.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbInquiryNumber.CheckedListSettings.CheckStateMember = ""
        Appearance119.BackColor = System.Drawing.SystemColors.Window
        Appearance119.BorderColor = System.Drawing.SystemColors.InactiveCaption
        Me.cmbInquiryNumber.DisplayLayout.Appearance = Appearance119
        Me.cmbInquiryNumber.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.cmbInquiryNumber.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance120.BackColor = System.Drawing.SystemColors.ActiveBorder
        Appearance120.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance120.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance120.BorderColor = System.Drawing.SystemColors.Window
        Me.cmbInquiryNumber.DisplayLayout.GroupByBox.Appearance = Appearance120
        Appearance121.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cmbInquiryNumber.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance121
        Me.cmbInquiryNumber.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance122.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance122.BackColor2 = System.Drawing.SystemColors.Control
        Appearance122.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance122.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cmbInquiryNumber.DisplayLayout.GroupByBox.PromptAppearance = Appearance122
        Me.cmbInquiryNumber.DisplayLayout.MaxColScrollRegions = 1
        Me.cmbInquiryNumber.DisplayLayout.MaxRowScrollRegions = 1
        Appearance123.BackColor = System.Drawing.SystemColors.Window
        Appearance123.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmbInquiryNumber.DisplayLayout.Override.ActiveCellAppearance = Appearance123
        Appearance124.BackColor = System.Drawing.SystemColors.Highlight
        Appearance124.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.cmbInquiryNumber.DisplayLayout.Override.ActiveRowAppearance = Appearance124
        Me.cmbInquiryNumber.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.cmbInquiryNumber.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance125.BackColor = System.Drawing.SystemColors.Window
        Me.cmbInquiryNumber.DisplayLayout.Override.CardAreaAppearance = Appearance125
        Appearance126.BorderColor = System.Drawing.Color.Silver
        Appearance126.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.cmbInquiryNumber.DisplayLayout.Override.CellAppearance = Appearance126
        Me.cmbInquiryNumber.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.cmbInquiryNumber.DisplayLayout.Override.CellPadding = 0
        Appearance127.BackColor = System.Drawing.SystemColors.Control
        Appearance127.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance127.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance127.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance127.BorderColor = System.Drawing.SystemColors.Window
        Me.cmbInquiryNumber.DisplayLayout.Override.GroupByRowAppearance = Appearance127
        Appearance128.TextHAlignAsString = "Left"
        Me.cmbInquiryNumber.DisplayLayout.Override.HeaderAppearance = Appearance128
        Me.cmbInquiryNumber.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.cmbInquiryNumber.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance129.BackColor = System.Drawing.SystemColors.Window
        Appearance129.BorderColor = System.Drawing.Color.Silver
        Me.cmbInquiryNumber.DisplayLayout.Override.RowAppearance = Appearance129
        Me.cmbInquiryNumber.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance130.BackColor = System.Drawing.SystemColors.ControlLight
        Me.cmbInquiryNumber.DisplayLayout.Override.TemplateAddRowAppearance = Appearance130
        Me.cmbInquiryNumber.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbInquiryNumber.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbInquiryNumber.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.cmbInquiryNumber.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Office2007
        Me.cmbInquiryNumber.DropDownSearchMethod = Infragistics.Win.UltraWinGrid.DropDownSearchMethod.Linear
        Me.cmbInquiryNumber.LimitToList = True
        Me.cmbInquiryNumber.Location = New System.Drawing.Point(488, 14)
        Me.cmbInquiryNumber.MaxDropDownItems = 20
        Me.cmbInquiryNumber.Name = "cmbInquiryNumber"
        Me.cmbInquiryNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbInquiryNumber.Size = New System.Drawing.Size(308, 22)
        Me.cmbInquiryNumber.TabIndex = 17
        '
        'cmbReference
        '
        Appearance1.BackColor = System.Drawing.Color.FromArgb(CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer), CType(CType(235, Byte), Integer))
        Me.cmbReference.Appearance = Appearance1
        Me.cmbReference.CheckedListSettings.CheckStateMember = ""
        Appearance2.BackColor = System.Drawing.SystemColors.Window
        Appearance2.BorderColor = System.Drawing.SystemColors.InactiveCaption
        Me.cmbReference.DisplayLayout.Appearance = Appearance2
        Me.cmbReference.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Me.cmbReference.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.[False]
        Appearance3.BackColor = System.Drawing.SystemColors.ActiveBorder
        Appearance3.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance3.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical
        Appearance3.BorderColor = System.Drawing.SystemColors.Window
        Me.cmbReference.DisplayLayout.GroupByBox.Appearance = Appearance3
        Appearance4.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cmbReference.DisplayLayout.GroupByBox.BandLabelAppearance = Appearance4
        Me.cmbReference.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid
        Appearance5.BackColor = System.Drawing.SystemColors.ControlLightLight
        Appearance5.BackColor2 = System.Drawing.SystemColors.Control
        Appearance5.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance5.ForeColor = System.Drawing.SystemColors.GrayText
        Me.cmbReference.DisplayLayout.GroupByBox.PromptAppearance = Appearance5
        Me.cmbReference.DisplayLayout.MaxColScrollRegions = 1
        Me.cmbReference.DisplayLayout.MaxRowScrollRegions = 1
        Appearance6.BackColor = System.Drawing.SystemColors.Window
        Appearance6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmbReference.DisplayLayout.Override.ActiveCellAppearance = Appearance6
        Appearance7.BackColor = System.Drawing.SystemColors.Highlight
        Appearance7.ForeColor = System.Drawing.SystemColors.HighlightText
        Me.cmbReference.DisplayLayout.Override.ActiveRowAppearance = Appearance7
        Me.cmbReference.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted
        Me.cmbReference.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted
        Appearance8.BackColor = System.Drawing.SystemColors.Window
        Me.cmbReference.DisplayLayout.Override.CardAreaAppearance = Appearance8
        Appearance9.BorderColor = System.Drawing.Color.Silver
        Appearance9.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter
        Me.cmbReference.DisplayLayout.Override.CellAppearance = Appearance9
        Me.cmbReference.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText
        Me.cmbReference.DisplayLayout.Override.CellPadding = 0
        Appearance10.BackColor = System.Drawing.SystemColors.Control
        Appearance10.BackColor2 = System.Drawing.SystemColors.ControlDark
        Appearance10.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element
        Appearance10.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal
        Appearance10.BorderColor = System.Drawing.SystemColors.Window
        Me.cmbReference.DisplayLayout.Override.GroupByRowAppearance = Appearance10
        Appearance11.TextHAlignAsString = "Left"
        Me.cmbReference.DisplayLayout.Override.HeaderAppearance = Appearance11
        Me.cmbReference.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Me.cmbReference.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand
        Appearance12.BackColor = System.Drawing.SystemColors.Window
        Appearance12.BorderColor = System.Drawing.Color.Silver
        Me.cmbReference.DisplayLayout.Override.RowAppearance = Appearance12
        Me.cmbReference.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.[False]
        Appearance13.BackColor = System.Drawing.SystemColors.ControlLight
        Me.cmbReference.DisplayLayout.Override.TemplateAddRowAppearance = Appearance13
        Me.cmbReference.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbReference.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbReference.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy
        Me.cmbReference.DisplayStyle = Infragistics.Win.EmbeddableElementDisplayStyle.Office2007
        Me.cmbReference.LimitToList = True
        Me.cmbReference.Location = New System.Drawing.Point(6, 14)
        Me.cmbReference.Name = "cmbReference"
        Me.cmbReference.Size = New System.Drawing.Size(202, 22)
        Me.cmbReference.TabIndex = 17
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(350, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(83, 13)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Inquiry Date To:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(485, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(78, 13)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "Inquiry Number"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(213, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(93, 13)
        Me.Label2.TabIndex = 14
        Me.Label2.Text = "Inquiry Date From:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(20, Byte), Integer), CType(CType(120, Byte), Integer), CType(CType(148, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(3, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 16
        Me.Label5.Text = "Customer"
        '
        'grdItems
        '
        Me.grdItems.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdItems.AutoEdit = True
        grdItems_DesignTimeLayout.LayoutString = resources.GetString("grdItems_DesignTimeLayout.LayoutString")
        Me.grdItems.DesignTimeLayout = grdItems_DesignTimeLayout
        Me.grdItems.FilterMode = Janus.Windows.GridEX.FilterMode.Automatic
        Me.grdItems.FilterRowButtonStyle = Janus.Windows.GridEX.FilterRowButtonStyle.ConditionOperatorDropDown
        Me.grdItems.FilterRowUpdateMode = Janus.Windows.GridEX.FilterRowUpdateMode.WhenValueChanges
        Me.grdItems.GroupByBoxVisible = False
        Me.grdItems.Location = New System.Drawing.Point(12, 66)
        Me.grdItems.Name = "grdItems"
        Me.grdItems.NewRowEnterKeyBehavior = Janus.Windows.GridEX.NewRowEnterKeyBehavior.AddRowAndMoveToFirstCellInNewRow
        Me.grdItems.Size = New System.Drawing.Size(880, 330)
        Me.grdItems.TabIndex = 2
        Me.grdItems.VisualStyle = Janus.Windows.GridEX.VisualStyle.Office2007
        '
        'frmSearchSalesInquiry
        '
        Me.AcceptButton = Me.OK_Button
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Cancel_Button
        Me.ClientSize = New System.Drawing.Size(907, 443)
        Me.Controls.Add(Me.grdItems)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSearchSalesInquiry"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Search Sales Inquiry"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.cmbInquiryNumber, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmbReference, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.grdItems, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OK_Button As System.Windows.Forms.Button
    Friend WithEvents Cancel_Button As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents dtpInquiryToDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpInquiryFromDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cmbInquiryNumber As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents cmbReference As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents grdItems As Janus.Windows.GridEX.GridEX

End Class

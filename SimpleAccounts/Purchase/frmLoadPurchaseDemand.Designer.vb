<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLoadPurchaseDemand
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.btnLoad = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.cmbInquiryNumber = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.cmbInquiryNumber, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.btnLoad, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.btnCancel, 1, 0)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(165, 122)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(219, 45)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'btnLoad
        '
        Me.btnLoad.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnLoad.Location = New System.Drawing.Point(4, 5)
        Me.btnLoad.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnLoad.Name = "btnLoad"
        Me.btnLoad.Size = New System.Drawing.Size(100, 35)
        Me.btnLoad.TabIndex = 0
        Me.btnLoad.Text = "Load"
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(114, 5)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(100, 35)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.AutoScroll = True
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.cmbInquiryNumber)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Location = New System.Drawing.Point(18, 18)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(366, 94)
        Me.Panel1.TabIndex = 1
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
        Me.cmbInquiryNumber.Location = New System.Drawing.Point(30, 37)
        Me.cmbInquiryNumber.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cmbInquiryNumber.MaxDropDownItems = 20
        Me.cmbInquiryNumber.Name = "cmbInquiryNumber"
        Me.cmbInquiryNumber.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmbInquiryNumber.Size = New System.Drawing.Size(303, 29)
        Me.cmbInquiryNumber.TabIndex = 17
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(26, 15)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(165, 20)
        Me.Label4.TabIndex = 16
        Me.Label4.Text = "Purchase Demand No"
        '
        'frmLoadPurchaseDemand
        '
        Me.AcceptButton = Me.btnLoad
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(223, Byte), Integer), CType(CType(211, Byte), Integer))
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(402, 183)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLoadPurchaseDemand"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Load Purchase Demand"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.cmbInquiryNumber, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents btnLoad As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents cmbInquiryNumber As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents Label4 As System.Windows.Forms.Label

End Class

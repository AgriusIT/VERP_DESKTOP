<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmApprovalRejectionDetail
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
        Dim UltraGridBand1 As Infragistics.Win.UltraWinGrid.UltraGridBand = New Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1)
        Dim UltraGridColumn1 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("coa_detail_id")
        Dim UltraGridColumn2 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("detail_title")
        Dim UltraGridColumn3 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("detail_code")
        Dim UltraGridColumn4 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("account_type")
        Dim UltraGridColumn5 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("sub_sub_title")
        Dim UltraGridColumn6 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("sub_title")
        Dim UltraGridColumn7 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("main_title")
        Dim UltraGridColumn8 As Infragistics.Win.UltraWinGrid.UltraGridColumn = New Infragistics.Win.UltraWinGrid.UltraGridColumn("main_type")
        Dim Appearance3 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance4 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance5 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Dim Appearance6 As Infragistics.Win.Appearance = New Infragistics.Win.Appearance()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cmbRejectionReason = New Infragistics.Win.UltraWinGrid.UltraCombo()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtRemarks = New System.Windows.Forms.TextBox()
        Me.btnOk = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        CType(Me.cmbRejectionReason, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Navy
        Me.Label4.Location = New System.Drawing.Point(24, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(57, 17)
        Me.Label4.TabIndex = 2
        Me.Label4.Text = "Reason"
        '
        'cmbRejectionReason
        '
        Appearance1.BackColor = System.Drawing.SystemColors.Info
        Me.cmbRejectionReason.Appearance = Appearance1
        Me.cmbRejectionReason.AutoCompleteMode = Infragistics.Win.AutoCompleteMode.Suggest
        Me.cmbRejectionReason.AutoSuggestFilterMode = Infragistics.Win.AutoSuggestFilterMode.Contains
        Me.cmbRejectionReason.CheckedListSettings.CheckStateMember = ""
        Appearance2.BackColor = System.Drawing.Color.White
        Me.cmbRejectionReason.DisplayLayout.Appearance = Appearance2
        UltraGridColumn1.Header.Caption = "ID"
        UltraGridColumn1.Header.VisiblePosition = 0
        UltraGridColumn1.Hidden = True
        UltraGridColumn1.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(8, 0)
        UltraGridColumn2.Header.Caption = "Account"
        UltraGridColumn2.Header.VisiblePosition = 1
        UltraGridColumn2.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(128, 0)
        UltraGridColumn3.Header.Caption = "Code"
        UltraGridColumn3.Header.VisiblePosition = 2
        UltraGridColumn4.Header.Caption = "Type"
        UltraGridColumn4.Header.VisiblePosition = 3
        UltraGridColumn4.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(96, 0)
        UltraGridColumn5.Header.Caption = "Sub Sub Ac"
        UltraGridColumn5.Header.VisiblePosition = 4
        UltraGridColumn5.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(106, 0)
        UltraGridColumn6.Header.Caption = "Sub Ac"
        UltraGridColumn6.Header.VisiblePosition = 5
        UltraGridColumn6.Hidden = True
        UltraGridColumn6.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(102, 0)
        UltraGridColumn7.Header.Caption = "Main Ac"
        UltraGridColumn7.Header.VisiblePosition = 6
        UltraGridColumn7.Hidden = True
        UltraGridColumn7.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(82, 0)
        UltraGridColumn8.Header.Caption = "Ac Head"
        UltraGridColumn8.Header.VisiblePosition = 7
        UltraGridColumn8.Hidden = True
        UltraGridColumn8.RowLayoutColumnInfo.PreferredCellSize = New System.Drawing.Size(84, 0)
        UltraGridBand1.Columns.AddRange(New Object() {UltraGridColumn1, UltraGridColumn2, UltraGridColumn3, UltraGridColumn4, UltraGridColumn5, UltraGridColumn6, UltraGridColumn7, UltraGridColumn8})
        Me.cmbRejectionReason.DisplayLayout.BandsSerializer.Add(UltraGridBand1)
        Me.cmbRejectionReason.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No
        Me.cmbRejectionReason.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbRejectionReason.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.[False]
        Me.cmbRejectionReason.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.None
        Appearance3.BackColor = System.Drawing.Color.Transparent
        Me.cmbRejectionReason.DisplayLayout.Override.CardAreaAppearance = Appearance3
        Me.cmbRejectionReason.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect
        Me.cmbRejectionReason.DisplayLayout.Override.CellPadding = 3
        Me.cmbRejectionReason.DisplayLayout.Override.ColumnAutoSizeMode = Infragistics.Win.UltraWinGrid.ColumnAutoSizeMode.AllRowsInBand
        Appearance4.TextHAlignAsString = "Left"
        Me.cmbRejectionReason.DisplayLayout.Override.HeaderAppearance = Appearance4
        Me.cmbRejectionReason.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti
        Appearance5.BorderColor = System.Drawing.Color.LightGray
        Appearance5.TextVAlignAsString = "Middle"
        Me.cmbRejectionReason.DisplayLayout.Override.RowAppearance = Appearance5
        Appearance6.BackColor = System.Drawing.Color.LightSteelBlue
        Appearance6.BorderColor = System.Drawing.Color.Black
        Appearance6.ForeColor = System.Drawing.Color.Black
        Me.cmbRejectionReason.DisplayLayout.Override.SelectedRowAppearance = Appearance6
        Me.cmbRejectionReason.DisplayLayout.Override.SelectTypeCell = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbRejectionReason.DisplayLayout.Override.SelectTypeCol = Infragistics.Win.UltraWinGrid.SelectType.None
        Me.cmbRejectionReason.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.[Single]
        Me.cmbRejectionReason.DisplayLayout.RowConnectorStyle = Infragistics.Win.UltraWinGrid.RowConnectorStyle.None
        Me.cmbRejectionReason.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill
        Me.cmbRejectionReason.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate
        Me.cmbRejectionReason.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl
        Me.cmbRejectionReason.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand
        Me.cmbRejectionReason.LimitToList = True
        Me.cmbRejectionReason.Location = New System.Drawing.Point(27, 44)
        Me.cmbRejectionReason.MaxDropDownItems = 16
        Me.cmbRejectionReason.Name = "cmbRejectionReason"
        Me.cmbRejectionReason.Size = New System.Drawing.Size(403, 22)
        Me.cmbRejectionReason.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Navy
        Me.Label1.Location = New System.Drawing.Point(24, 69)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 17)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Remarks"
        '
        'txtRemarks
        '
        Me.txtRemarks.Location = New System.Drawing.Point(27, 89)
        Me.txtRemarks.Multiline = True
        Me.txtRemarks.Name = "txtRemarks"
        Me.txtRemarks.Size = New System.Drawing.Size(403, 104)
        Me.txtRemarks.TabIndex = 5
        '
        'btnOk
        '
        Me.btnOk.ForeColor = System.Drawing.Color.Navy
        Me.btnOk.Location = New System.Drawing.Point(274, 199)
        Me.btnOk.Name = "btnOk"
        Me.btnOk.Size = New System.Drawing.Size(75, 23)
        Me.btnOk.TabIndex = 6
        Me.btnOk.Text = "Ok"
        Me.btnOk.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.ForeColor = System.Drawing.Color.Navy
        Me.btnCancel.Location = New System.Drawing.Point(355, 199)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 7
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'frmApprovalRejectionDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(211, Byte), Integer), CType(CType(247, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(495, 275)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOk)
        Me.Controls.Add(Me.txtRemarks)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cmbRejectionReason)
        Me.Name = "frmApprovalRejectionDetail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reject "
        CType(Me.cmbRejectionReason, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbRejectionReason As Infragistics.Win.UltraWinGrid.UltraCombo
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtRemarks As System.Windows.Forms.TextBox
    Friend WithEvents btnOk As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
End Class

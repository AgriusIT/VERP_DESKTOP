Public Class frmGrdImportLedger

    Private Sub frmGrdImportLedger_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.Back Then
                Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            End If
            If e.KeyCode = Keys.F5 Then
                btnRefresh_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmGrdImportLedger_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            FillCombos("Vendor")
            FillCombos("Bank")
            FillCombos("Department")
            FillCombos("Type")
            FillCombos("Category")
            FillCombos("SubCategory")
            FillCombos("Gender")
            FillCombos("ItemFrom")
            FillCombos("ItemTo")
            Me.cmbLCType.SelectedIndex = 0
            Me.cmbDocType.SelectedIndex = 0
            If Not Me.cmbBank.SelectedIndex = -1 Then Me.cmbBank.SelectedIndex = 0
            Me.txtLCNo.Text = String.Empty
            Me.cmbPeriod.Text = "Current Month"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillCombos(Optional ByVal Condition As String = "")
        Try
            If Condition = "Vendor" Then
                FillUltraDropDown(Me.cmbAccount, "Select Coa_detail_id, detail_title From vwCOAdetail where account_type='Vendor'")
                If Me.cmbAccount.DisplayLayout.Bands.Count > 0 Then
                    Me.cmbAccount.DisplayLayout.Bands(0).Columns(0).Hidden = True
                End If
                Me.cmbAccount.Rows(0).Activate()
            ElseIf Condition = "Bank" Then
                FillDropDown(Me.cmbBank, "Select DISTINCT Bank, Bank From tblLetterofCredit Order By 1 Asc")
            ElseIf Condition = "ItemFrom" Then
                FillUltraDropDown(Me.cmbItem, "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item, ArticleSizeName as Size, ArticleColorName as Combination, Isnull(SalePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID], 0 as Stock FROM ArticleDefView where Active=1 AND SalesItem=1")
                Me.cmbItem.Rows(0).Activate()
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Stock").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Price").Hidden = True
            ElseIf Condition = "ItemTo" Then
                FillUltraDropDown(Me.cmbItemTo, "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item, ArticleSizeName as Size, ArticleColorName as Combination, Isnull(SalePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID], 0 as Stock FROM ArticleDefView where Active=1 AND SalesItem=1")
                Me.cmbItemTo.Rows(0).Activate()
                Me.cmbItemTo.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
                Me.cmbItemTo.DisplayLayout.Bands(0).Columns("Stock").Hidden = True
                Me.cmbItemTo.DisplayLayout.Bands(0).Columns("Price").Hidden = True
            ElseIf Condition = "Department" Then
                FillListBox(Me.cmbDepartment.ListItem, "Select ArticleGroupId, ArticleGroupName From ArticleGroupDefTable")
            ElseIf Condition = "Type" Then
                FillListBox(Me.cmbType.ListItem, "Select ArticleTypeId, ArticleTypeName From ArticleTypeDefTable")
            ElseIf Condition = "Category" Then
                FillListBox(Me.cmbCategory.ListItem, "Select ArticleCompanyId, ArticleCompanyName From ArticleCompanyDefTable")
            ElseIf Condition = "SubCategory" Then
                FillListBox(Me.cmbSubCategory.ListItem, "Select ArticleLpoId, ArticleLpoName From ArticleLpoDefTable")
            ElseIf Condition = "Gender" Then
                FillListBox(Me.lstGender.ListItem, "Select ArticleGenderId, ArticleGenderName From ArticleGenderDefTable")
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub FillGrid()
        Try
            Dim strProcedure As String = String.Empty
            strProcedure = "SP_ImportLedger"
            Dim dv As New DataView
            Dim dt As New DataTable
            dt = GetDataTable(strProcedure)
            dt.TableName = "ImportLedger"
            dt.Columns.Add("CurrencyPrice", GetType(System.Double))
            dt.Columns.Add("Amount", GetType(System.Double))

            dt.Columns("CurrencyPrice").Expression = "IIF(CurrencyRate=0,Price,IIF(Price=0,0,(Price/CurrencyRate)))"
            dt.Columns("Amount").Expression = "iif(CurrencyRate=0,(Price*Qty),IIF(Price=0,0,((Price/CurrencyRate)*Qty)))"
            dv.Table = dt

            Dim strFilter As String = String.Empty
            strFilter = "ReceivingDate >= '" & Me.dtpFrom.Value.ToString("yyyy-M-d 00:00:00") & "' AND ReceivingDate <=  '" & Me.dtpTo.Value.ToString("yyyy-M-d 23:59:59") & "'"

            If Me.txtLCNo.Text.Length > 0 Then
                strFilter += " AND lcdoc_no LIKE '%" & Me.txtLCNo.Text & "%'"
            End If

            If Me.cmbAccount.SelectedRow.Cells(0).Value > 0 Then
                strFilter += " AND VendorId=" & Me.cmbAccount.Value & ""
            End If

            If Me.cmbBank.SelectedIndex > 0 Then
                strFilter += " AND Bank='" & Me.cmbBank.Text & "'"
            End If

            If Me.cmbLCType.SelectedIndex > 0 Then
                strFilter += " AND LCtype='" & Me.cmbLCType.Text & "'"
            End If

            If Me.cmbDocType.SelectedIndex > 0 Then
                strFilter += " AND LCdoc_Type='" & Me.cmbDocType.Text & "'"
            End If

            If Not Me.rbtVendor.Checked = True Then

                If Me.cmbItem.SelectedRow.Cells(0).Value > 0 Then
                    strFilter += " AND ArticleDefId >= " & Me.cmbItem.Value & ""
                End If
                If Me.cmbItem.SelectedRow.Cells(0).Value > 0 Then
                    strFilter += " AND ArticleDefId <= " & Me.cmbItem.Value & ""
                End If

                If Me.cmbDepartment.SelectedIDs.Length > 0 Then
                    strFilter += " AND ArticleGroupId in (" & Me.cmbDepartment.SelectedIDs & ")"
                End If

                If Me.cmbCategory.SelectedIDs.Length > 0 Then
                    strFilter += " AND ArticleCompanyId in (" & Me.cmbCategory.SelectedIDs & ")"
                End If


                If Me.cmbSubCategory.SelectedIDs.Length > 0 Then
                    strFilter += " AND ArticleLPOId in (" & Me.cmbSubCategory.SelectedIDs & ")"
                End If


                If Me.cmbType.SelectedIDs.Length > 0 Then
                    strFilter += " AND ArticleTypeId in (" & Me.cmbType.SelectedIDs & ")"
                End If


                If Me.lstGender.SelectedIDs.Length > 0 Then
                    strFilter += " AND ArticleGenderId in (" & Me.lstGender.SelectedIDs & ")"
                End If

            End If

            dv.RowFilter = strFilter
            Me.grdSaved.DataSource = dv
            Me.grdSaved.RetrieveStructure()
            Me.grdSaved.TotalRow = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grdSaved.TotalRowPosition = Janus.Windows.GridEX.TotalRowPosition.BottomFixed

            For c As Integer = 0I To Me.grdSaved.RootTable.Columns.Count - 1
                Me.grdSaved.RootTable.Columns(c).Visible = False
            Next


            Me.grdSaved.RootTable.Columns("ReceivingDate").Visible = True
            Me.grdSaved.RootTable.Columns("vendor_invoice_no").Visible = True
            Me.grdSaved.RootTable.Columns("LcDoc_Type").Visible = True
            Me.grdSaved.RootTable.Columns("Bank").Visible = True
            Me.grdSaved.RootTable.Columns("LcDoc_Type").Visible = True
            Me.grdSaved.RootTable.Columns("LcDoc_No").Visible = True
            Me.grdSaved.RootTable.Columns("LcDoc_Date").Visible = True
            Me.grdSaved.RootTable.Columns("LcType").Visible = True
            Me.grdSaved.RootTable.Columns("ArticleDescription").Visible = True
            Me.grdSaved.RootTable.Columns("ArticleUnitName").Visible = True
            Me.grdSaved.RootTable.Columns("Qty").Visible = True
            Me.grdSaved.RootTable.Columns("Currency_Code").Visible = True
            Me.grdSaved.RootTable.Columns("CurrencyPrice").Visible = True
            Me.grdSaved.RootTable.Columns("Amount").Visible = True


            Me.grdSaved.RootTable.Columns("ReceivingDate").Caption = "Date"
            Me.grdSaved.RootTable.Columns("vendor_invoice_no").Caption = "P.Inv#"
            Me.grdSaved.RootTable.Columns("LcDoc_Type").Caption = "LC Or TT"
            Me.grdSaved.RootTable.Columns("Bank").Caption = "Bank"
            Me.grdSaved.RootTable.Columns("LcDoc_No").Caption = "LC No"
            Me.grdSaved.RootTable.Columns("LcDoc_Date").Caption = "LC Date"
            Me.grdSaved.RootTable.Columns("LcType").Caption = "CRF/FOB"
            Me.grdSaved.RootTable.Columns("ArticleDescription").Caption = "Goods Description"
            Me.grdSaved.RootTable.Columns("ArticleUnitName").Caption = "UOM"
            Me.grdSaved.RootTable.Columns("Currency_Code").Caption = "Currency"
            Me.grdSaved.RootTable.Columns("CurrencyPrice").Caption = "Unit Price"


            Me.grdSaved.RootTable.Columns("CurrencyPrice").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("CurrencyPrice").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Qty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Qty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grdSaved.RootTable.Columns("Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grdSaved.RootTable.Columns("ReceivingDate").FormatString = "dd/MMM/yyyy"

            Me.grdSaved.RootTable.Columns("Qty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grdSaved.RootTable.Columns("Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grdSaved.AutoSizeColumns()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub cmbPeriod_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbPeriod.SelectedIndexChanged
        Try
            If Me.cmbPeriod.Text = "Today" Then
                Me.dtpFrom.Value = Date.Today
                Me.dtpTo.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Yesterday" Then
                Me.dtpFrom.Value = Date.Today.AddDays(-1)
                Me.dtpTo.Value = Date.Today.AddDays(-1)
            ElseIf Me.cmbPeriod.Text = "Current Week" Then
                Me.dtpFrom.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
                Me.dtpTo.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Month" Then
                Me.dtpFrom.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
                Me.dtpTo.Value = Date.Today
            ElseIf Me.cmbPeriod.Text = "Current Year" Then
                Me.dtpFrom.Value = New Date(Date.Now.Year, 1, 1)
                Me.dtpTo.Value = Date.Today
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnShow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShow.Click
        Me.ToolStripProgressBar1.Visible = True
        Try
            FillGrid()
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(1).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.ToolStripProgressBar1.Visible = False
        End Try
    End Sub
    Private Sub rbtVendor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbtVendor.CheckedChanged, RadioButton1.CheckedChanged
        Try
            Dim rd As RadioButton = CType(sender, RadioButton)
            Select Case rd.Name
                Case rbtVendor.Name
                    Me.GroupBox3.Enabled = False
                    Me.GroupBox4.Enabled = False
                    Me.GroupBox1.Enabled = True
                    Me.GroupBox2.Enabled = True
                Case Me.RadioButton1.Name
                    Me.GroupBox3.Enabled = True
                    Me.GroupBox4.Enabled = True
                    Me.GroupBox1.Enabled = True
                    Me.GroupBox2.Enabled = True
            End Select
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnRefresh1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh1.Click
        Try
            btnShow_Click(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0I
            id = Me.cmbAccount.Value
            FillCombos("Vendor")
            Me.cmbAccount.Value = id
            FillCombos("Bank")
            FillCombos("Department")
            FillCombos("Type")
            FillCombos("Category")
            FillCombos("SubCategory")
            FillCombos("Gender")
            id = Me.cmbItem.Value
            FillCombos("ItemFrom")
            Me.cmbItem.Value = id
            id = Me.cmbItemTo.Value
            FillCombos("ItemTo")
            Me.cmbItemTo.Value = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdSaved_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdSaved.KeyDown
        If e.KeyCode = Keys.F5 Then
            btnRefresh_Click(Nothing, Nothing)
        End If
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grdSaved.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "Import Ledger" & Chr(10) & "From Date: " & dtpFrom.Value.ToString("dd-MM-yyyy").ToString & Chr(10) & "To Date: " & dtpTo.Value.ToString("dd-MM-yyyy").ToString & ""
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class

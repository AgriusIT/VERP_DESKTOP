''27-6-2015 TASKM276151 Imran Ali Add Feild Of Category In Item List Andalso Set Editable Field Price 
Imports System
Imports System.Data


Public Class frmGrdRptCostSheetMarginCalculationDetail
    Dim IsOpenForm As Boolean = False
    Private Sub frmGrdRptCostSheetQtyWise_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Try

            FillCombo()
            IsOpenForm = True
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try

            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            If IsOpenForm = True Then Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "LOM Detail" & Chr(10) & "Quotation No. " & IIf(Me.cmbQuotation.ActiveRow Is Nothing, "", Me.cmbQuotation.ActiveRow.Cells("Quotation No").Value.ToString) & ""

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub FillCombo(Optional ByVal Condition As String = "")
        Try
            FillUltraDropDown(Me.cmbQuotation, "SELECT Quot.QuotationId, Quot.QuotationNo as [Quotation No], Quot.QuotationDate as [Date], Quot.VendorId, COA.detail_title as [Customer], COA.detail_code as [Account Code], COA.account_type as [Type]  FROM dbo.QuotationMasterTable AS Quot LEFT OUTER JOIN dbo.vwCOADetail AS COA ON Quot.VendorId = COA.coa_detail_id Order By QuotationNo DESC")
            Me.cmbQuotation.Rows(0).Activate()
            Me.cmbQuotation.DisplayLayout.Bands(0).Columns("QuotationId").Hidden = True
            Me.cmbQuotation.DisplayLayout.Bands(0).Columns("VendorId").Hidden = True
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub FillGrid()
        Try

            Dim strSQL As String = "SP_QuotationItemSpecification " & Me.cmbQuotation.Value & ""
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()
            dt.Columns("After Discount Value").Expression = "((IsNull([Discount],0)/100) * (IsNull([PurchasePrice],0)))"
            dt.Columns("Quoted").Expression = "(iif(IsNull([Discount],0)=0,IsNull([PurchasePrice],0),(IsNull([PurchasePrice],0)-IsNull([After Discount Value],0))))"
            dt.Columns("Total Amount").Expression = "(IsNull([Quoted],0)*IsNull([CostQty],0))"
            dt.Columns("Net Amount").Expression = "((IsNull([Tax],0)/100)*IsNull([Total Amount],0))"
            dt.AcceptChanges()
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()

            'grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.False
            grd.GroupByBoxVisible = True
            Me.grd.AllowEdit = Janus.Windows.GridEX.InheritableBoolean.True
            Me.grd.RootTable.Columns("QuotationId").Visible = False
            Me.grd.RootTable.Columns("QuotationDetailId").Visible = False
            Me.grd.RootTable.Columns("CostSheetId").Visible = False
            Me.grd.RootTable.Columns("ArticleId").Visible = False
            Me.grd.RootTable.Columns("MasterID").Visible = False
            Me.grd.RootTable.Columns("ArticleCode").Visible = False
            Me.grd.RootTable.Columns("ArticleSizeName").Visible = False
            Me.grd.RootTable.Columns("ArticleColorName").Visible = False
            Me.grd.RootTable.Columns("Qty").Visible = False
            Me.grd.RootTable.Columns("Specification_Qty").Visible = False
            Me.grd.RootTable.Columns("PurchasePrice").Visible = True
            Me.grd.RootTable.Columns("SalePrice").Visible = False
            Me.grd.RootTable.Columns("Cost Value").Visible = True

            Me.grd.RootTable.Columns("Detail_Code").Caption = "A/Code"
            Me.grd.RootTable.Columns("Detail_Title").Caption = "Customer"
            Me.grd.RootTable.Columns("CityName").Caption = "City"
            Me.grd.RootTable.Columns("StateName").Caption = "Region"
            Me.grd.RootTable.Columns("TerritoryName").Caption = "Area"
            Me.grd.RootTable.Columns("PurchasePrice").Caption = "Price"
            Me.grd.RootTable.Columns("Specification_Item_Description").Caption = "Item"
            Me.grd.RootTable.Columns("Specification_Item_Code").Caption = "Item Code"
            Me.grd.RootTable.Columns("CostQty").Caption = "Qty"
            Me.grd.RootTable.Columns("Remarks").Caption = "Make"

            Me.grd.RootTable.Columns("CostQty").FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns("CostQty").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("CostQty").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("CostQty").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("Quoted").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Quoted").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Quoted").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("After Discount Value").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("After Discount Value").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("After Discount Value").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("Total Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Total Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Total Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far


            Me.grd.RootTable.Columns("Net Amount").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Net Amount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Net Amount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("Tax").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Tax").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far


            Me.grd.RootTable.Columns("After Discount Value").Caption = "Disc Value"

            'Me.grd.RootTable.Columns("After Discount Value").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns("Discount").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Discount").HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far

            Me.grd.RootTable.Columns("PurchasePrice").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Cost Value").TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns("Cost Value").AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum

            Me.grd.RootTable.Columns("PurchasePrice").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Cost Value").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Total Amount").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("After Discount Value").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Quoted").FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns("Net Amount").FormatString = "N" & DecimalPointInValue

            Me.grd.GroupTotals = Janus.Windows.GridEX.GroupTotals.Always
            Me.grd.RootTable.HideColumnsWhenGrouped = Janus.Windows.GridEX.InheritableBoolean.True
            Dim grpArticleDescription As New Janus.Windows.GridEX.GridEXGroup
            grpArticleDescription.Column = Me.grd.RootTable.Columns("ArticleDescription")
            Me.grd.RootTable.Groups.Add(grpArticleDescription)

            ''27-6-2015 TASKM276151 Imran Ali Add Feild Of Category In Item List Andalso Set Editable Field Price 
            For c As Integer = 0 To Me.grd.RootTable.Columns.Count - 1
                If Me.grd.RootTable.Columns(c).DataMember <> "Discount" AndAlso Me.grd.RootTable.Columns(c).DataMember <> "CostQty" AndAlso Me.grd.RootTable.Columns(c).DataMember <> "Quoted" AndAlso Me.grd.RootTable.Columns(c).DataMember <> "Total Amount" AndAlso Me.grd.RootTable.Columns(c).DataMember <> "PurchasePrice" Then
                    Me.grd.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.NoEdit
                End If

                Me.grd.RootTable.Columns(c).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Next
            Me.grd.RootTable.Columns("Discount").AllowSort = False
            'End TASKM276151
            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grd.AutoSizeColumns()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnLoad_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLoad.Click
        Me.Cursor = Cursors.WaitCursor
        Try
            If Me.cmbQuotation.IsItemInList = False Then Exit Sub
            If Me.cmbQuotation.ActiveRow Is Nothing Then
                ShowErrorMessage("Invalid Quotation No.")
                Me.cmbQuotation.Focus()
                Exit Sub
            End If
            If Me.cmbQuotation.ActiveRow.Cells(0).Value <= 0 Then
                ShowErrorMessage("Please select quotation.")
                Me.cmbQuotation.Focus()
                Exit Sub
            End If
            If Me.grd.RowCount > 0 Then
                Me.grd.ClearStructure()
            End If
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0I
            If Me.cmbQuotation.ActiveRow Is Nothing Then
                Me.cmbQuotation.Rows(0).Activate()
            End If
            id = Me.cmbQuotation.ActiveRow.Cells(0).Value
            FillCombo()
            Me.cmbQuotation.Value = id
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click


        If Me.cmbQuotation.ActiveRow Is Nothing Then
            ShowErrorMessage("Invlid Quotation No.")
            Me.cmbQuotation.Focus()
            Exit Sub
        End If
        If Me.grd.RowCount = 0 Then
            ShowErrorMessage("Record not in grid.")
            Exit Sub
        End If
        Me.grd.UpdateData()

        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As OleDb.OleDbTransaction = Con.BeginTransaction
        Dim objCommand As New OleDb.OleDbCommand

        Try

            objCommand.Connection = Con
            objCommand.Transaction = trans
            objCommand.CommandTimeout = 120
            objCommand.CommandType = CommandType.Text

            Dim dt As New DataTable
            dt = CType(Me.grd.DataSource, DataTable)
            dt.AcceptChanges()

            objCommand.CommandText = "Delete From tblCostSheetDisountcDetail WHERE QuotationId=" & Me.cmbQuotation.Value & ""
            objCommand.ExecuteNonQuery()


            For Each dRow As DataRow In dt.Rows
                objCommand.CommandText = ""
                objCommand.CommandText = "INSERT INTO tblCostSheetDisountcDetail(QuotationId,MasterID,ArticleId,Discount,Quoted,Category,CostPrice) VALUES(" & Me.cmbQuotation.Value & "," & Val(dRow.Item("MasterID").ToString) & "," & Val(dRow.Item("ArticleId").ToString) & "," & Val(dRow.Item("Discount").ToString) & "," & Val(dRow.Item("Quoted").ToString) & ",'" & dRow.Item("Category").ToString.Replace("'", "''") & "'," & Val(dRow.Item("PurchasePrice").ToString) & ")"
                objCommand.ExecuteNonQuery()
            Next
            trans.Commit()
            Me.cmbQuotation.Rows(0).Activate()
            FillGrid()
        Catch ex As Exception
            trans.Rollback()
            ShowErrorMessage(ex.Message)
        Finally
            Con.Close()
        End Try
    End Sub
    Private Sub grd_ColumnHeaderClick(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnHeaderClick
        Try
            Me.grd.UpdateData()
            If Me.grd.RowCount > 0 Then
                If e.Column.Key = "Discount" Then
                    Dim dblDiscount As Double = 0D
                    Dim dt As DataTable = CType(Me.grd.DataSource, DataTable)
                    dt.AcceptChanges()
                    For Each r As DataRow In dt.Rows
                        r.BeginEdit()
                        If Val(dblDiscount) = 0 Then
                            dblDiscount = Val(r.Item("Discount").ToString)
                        Else
                            If Val(r("Discount").ToString) = 0 Then
                                r("Discount") = dblDiscount
                            End If
                        End If
                        r.EndEdit()
                    Next
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
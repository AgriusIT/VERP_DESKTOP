Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.Text
Imports System.Net

Public Class frmItemBulk
    Enum enmItem
        'ArticleId
        'ArticleCode
        'ArticleDescription
        'ArticleGroupId
        'ArticleTypeId
        'ArticleBrandId
        'ArticleGenderId
        'ArticleCompanyId
        'ArticleLPOId
        'ArticleStatusId
        'ArticleUnitId
        'PackQty
        'PurchasePrice
        'SalePrice
        'Cost_Price
        'StockLevel
        'StockLevelOpt
        'StockLevelMax
        'Active
        'AccountId
        ArticleId
        ArticleCode
        ArticleDescription
        ArticleGroupId
        ArticleTypeId
        ArticleBrandId
        ArticleGenderId
        ArticleCompanyId
        ArticleLPOId
        ArticleStatusId
        ArticleUnitId
        PackQty
        PurchasePrice
        SalePrice
        Cost_Price
        StockLevel
        StockLevelOpt
        StockLevelMax
        Active
        AccountID
        SortOrder
    End Enum
    Private Sub frmItemBulk_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Cursor = Cursors.WaitCursor
        Try
            GetSecurityRights()
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try

    End Sub
    Sub FillGrid()
        Try

            Dim strSQL As String = String.Empty
            'strSQL = "Select ArticleId, ArticleCode, ArticleDescription,ArticleGroupId, ArticleTypeId, IsNull(ArticleBrandId,0) as ArticleBrandId, ArticleGenderId, ArticleCategoryId as ArticleCompanyId, ArticleDefTableMaster.ArticleLPOId,IsNull(ArticleStatusId,0) as ArticleStatusId, ArticleUnitId, PackQty, IsNull(PurchasePrice,0) as PurchasePrice,IsNull(SalePrice,0) as SalePrice,IsNull(Cost_Price,0) as Cost_Price, StockLevel, StockLevelOpt,StockLevelMax,Convert(bit,IsNull(ArticleDefTableMaster.Active,0)) as Active,AccountId From ArticleDefTableMaster LEFT OUTER JOIN ArticleLPODefTable On ArticleLPODefTable.ArticleLPOId = ArticleDefTableMaster.ArticleLPOId Order By ArticleDefTableMaster.SortOrder ASC "
            ''Start TFS4159
            strSQL = "Select ArticleId, ArticleCode, ArticleDescription,ArticleGroupId, ArticleTypeId, IsNull(ArticleBrandId,0) as ArticleBrandId, ArticleGenderId, ArticleCategoryId as ArticleCompanyId, Case When ArticleDefTableMaster.ArticleLPOId = 0 then '' else convert(varchar(30), ArticleDefTableMaster.ArticleLPOId ) end As ArticleLPOId ,IsNull(ArticleStatusId,0) as ArticleStatusId, ArticleUnitId, PackQty, IsNull(PurchasePrice,0) as PurchasePrice,IsNull(SalePrice,0) as SalePrice,IsNull(Cost_Price,0) as Cost_Price, StockLevel, StockLevelOpt,StockLevelMax,Convert(bit,IsNull(ArticleDefTableMaster.Active,0)) as Active,AccountId From ArticleDefTableMaster LEFT OUTER JOIN ArticleLPODefTable On ArticleLPODefTable.ArticleLPOId = ArticleDefTableMaster.ArticleLPOId Order By ArticleDefTableMaster.SortOrder ASC "
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()


            Me.grd.DataSource = dt

            'ArticleGroup
            strSQL = String.Empty
            strSQL = "SELECT ArticleGroupId, ArticleGroupName,SubSubId From ArticleGroupDefTable"
            Dim dtArtGroup As New DataTable
            dtArtGroup = GetDataTable(strSQL)
            dtArtGroup.AcceptChanges()
            grd.RootTable.Columns("ArticleGroupId").ValueList.PopulateValueList(dtArtGroup.DefaultView, "ArticleGroupId", "ArticleGroupName")

            'ArticleType
            strSQL = String.Empty
            strSQL = "SELECT ArticleTypeId, ArticleTypeName From ArticleTypeDefTable"
            Dim dtArtType As New DataTable
            dtArtType = GetDataTable(strSQL)
            dtArtType.AcceptChanges()
            grd.RootTable.Columns("ArticleTypeId").ValueList.PopulateValueList(dtArtType.DefaultView, "ArticleTypeId", "ArticleTypeName")

            'Article Origin
            strSQL = String.Empty
            strSQL = "SELECT ArticleGenderId, ArticleGenderName From ArticleGenderDefTable"
            Dim dtArtGender As New DataTable
            dtArtGender = GetDataTable(strSQL)
            dtArtGender.AcceptChanges()
            grd.RootTable.Columns("ArticleGenderId").ValueList.PopulateValueList(dtArtGender.DefaultView, "ArticleGenderId", "ArticleGenderName")


            strSQL = String.Empty
            strSQL = "SELECT ArticleCompanyId, ArticleCompanyDefTable.ArticleCompanyName From ArticleCompanyDefTable"
            Dim dtArtCategory As New DataTable
            dtArtCategory.TableName = "Category"
            dtArtCategory = GetDataTable(strSQL)
            dtArtCategory.AcceptChanges()
            grd.RootTable.Columns("ArticleCompanyId").ValueList.PopulateValueList(dtArtCategory.DefaultView, "ArticleCompanyId", "ArticleCompanyName")

            'Article Sub Category
            strSQL = String.Empty
            strSQL = "SELECT ArticleLPOId, ArticleCompanyDefTable.ArticleCompanyName + ' > ' + ArticleLPOName as ArticleLPOName ,ArticleLPODefTable.ArticleCompanyId From ArticleLPODefTable LEFT OUTER JOIN ArticleCompanyDefTable On ArticleCompanyDefTable.ArticleCompanyId = ArticleLPODefTable.ArticleCompanyId "
            Dim dtArtSubCategory As New DataTable
            dtArtSubCategory.TableName = "LPO"
            dtArtSubCategory = GetDataTable(strSQL)
            dtArtSubCategory.AcceptChanges()
            grd.RootTable.Columns("ArticleLPOId").ValueList.PopulateValueList(dtArtSubCategory.DefaultView, "ArticleLPOId", "ArticleLPOName")


            strSQL = String.Empty
            strSQL = "SELECT ArticleStatusID, ArticleStatusName From ArticleStatus"
            Dim dtArtSatus As New DataTable
            dtArtSatus = GetDataTable(strSQL)
            dtArtSatus.AcceptChanges()
            grd.RootTable.Columns("ArticleStatusID").ValueList.PopulateValueList(dtArtSatus.DefaultView, "ArticleStatusID", "ArticleStatusName")

            'Article Unit
            strSQL = String.Empty
            strSQL = "SELECT ArticleUnitId, ArticleUnitName From ArticleUnitDefTable"
            Dim dtArtUnit As New DataTable
            dtArtUnit = GetDataTable(strSQL)
            dtArtUnit.AcceptChanges()
            grd.RootTable.Columns("ArticleUnitId").ValueList.PopulateValueList(dtArtUnit.DefaultView, "ArticleUnitId", "ArticleUnitName")



            strSQL = String.Empty
            strSQL = "SELECT ArticleBrandId, ArticleBrandName From ArticleBrandDefTable"
            Dim dtArtBrand As New DataTable
            dtArtBrand = GetDataTable(strSQL)
            dtArtBrand.AcceptChanges()
            grd.RootTable.Columns("ArticleBrandId").ValueList.PopulateValueList(dtArtBrand.DefaultView, "ArticleBrandId", "ArticleBrandName")

            'Dim AircraftTypeValueListItemCollection As Janus.Windows.GridEX.GridEXValueListItemCollection
            'AircraftTypeValueListItemCollection = Me.grd.Tables("LPO").ChildTables("LPO").ChildTables("LPO").Columns("ArticleLPOId").ValueList

            CtrlGrdBar1_Load(Nothing, Nothing)
            Me.grd.AutoSizeColumns()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function GetId(ByVal Id As Integer, ByVal Type As String) As Integer
        Try


            Dim strSQL As String = String.Empty
            Dim dt As New DataTable

            Select Case Type
                Case "ArticleGroup"
                    strSQL = "Select IsNull(SubSubId,0) as AccountId From ArticleGroupDefTable WHERE ArticleGroupId=" & Id & ""
                Case "ArticleLPO"
                    strSQL = "Select IsNull(ArticleCompanyId,0) as ArticleCompanyId From ArticleLPODefTable WHERE ArticleLPOId=" & Id & ""
            End Select


            If strSQL.Length > 0 Then
                dt = GetDataTable(strSQL)
                dt.AcceptChanges()
                If dt.Rows.Count > 0 Then
                    Return Val(dt.Rows(0).Item(0).ToString)
                Else
                    Return 0
                End If
            Else
                Return 0
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub grd_DropDown(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.DropDown
        Try
            'ShowErrorMessage("a")

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grd_UpdatingCell(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.UpdatingCellEventArgs) Handles grd.UpdatingCell
        Try
            If grd.DataSource Is Nothing Then Exit Sub
            Me.grd.UpdateData()
            If Me.grd.RowCount = -1 Then Exit Sub
            If Me.grd.RowCount > 0 Then
                If e.Column.Key = "ArticleGroupId" Then
                    Me.grd.GetRow.Cells("AccountId").Value = GetId(e.Value, "ArticleGroup")
                ElseIf e.Column.Key = "ArticleLPOId" Then
                    Me.grd.GetRow.Cells("ArticleCompanyId").Value = GetId(e.Value, "ArticleLPO")
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try

    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnSave.Enabled = True
                CtrlGrdBar1.mGridPrint.Enabled = True
                CtrlGrdBar1.mGridExport.Enabled = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                Dim dt As DataTable = GetFormRights(EnumForms.frmItemBulk)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If

                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
            Else
                Me.Visible = False
                Me.btnSave.Enabled = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False


                For i As Integer = 0 To Rights.Count - 1
                    If Rights.Item(i).FormControlName = "View" Then
                        Me.Visible = True
                    ElseIf Rights.Item(i).FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True

                        CtrlGrdBar1.mGridPrint.Enabled = True
                    ElseIf Rights.Item(i).FormControlName = "Export" Then
                        CtrlGrdBar1.mGridExport.Enabled = True

                    End If
                Next
            End If
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub
    Private Sub CtrlGrdBar1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            Me.CtrlGrdBar1.txtGridTitle.Text = "Daily Working Report"
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs1 As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                Me.grd.LoadLayoutFile(fs1)
                fs1.Close()
                fs1.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "All Items Detail"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub NewToolStripButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            FillGrid()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        If Me.grd.RowCount = 0 Then Exit Sub
        Me.grd.UpdateData()
        Me.Cursor = Cursors.WaitCursor
        Dim strSQL As String = String.Empty
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As OleDb.OleDbTransaction = Con.BeginTransaction
        Dim cmd As New OleDb.OleDbCommand
        Try

            cmd.Connection = Con
            cmd.Transaction = trans
            cmd.CommandTimeout = 120
            cmd.CommandType = CommandType.Text

            Dim dt As DataTable = CType(Me.grd.DataSource, DataTable)
            'dt.AcceptChanges()
            If dt IsNot Nothing Then
                For Each jRow As DataRow In dt.GetChanges.Rows




                    strSQL = String.Empty
                    strSQL = "Update ArticleDefTable Set ArticledefTable.ArticleCode=N'" & jRow.Item(enmItem.ArticleCode).ToString.Replace("'", "''") & "' + Convert(nvarchar,SubString(ArticleDefTable.articleCode,Len(a.ArticleCode)+1,3000)) From ArticleDefTable,ArticleDefTableMaster a WHERE ArticledefTable.MasterId = a.ArticleId AND  ArticledefTable.MasterId=" & Val(jRow.Item(enmItem.ArticleId).ToString) & ""
                    cmd.CommandText = String.Empty
                    cmd.CommandText = strSQL
                    cmd.ExecuteNonQuery()


                    strSQL = String.Empty
                    strSQL = "Update ArticleDefTableMaster  Set ArticleCode=N'" & jRow(enmItem.ArticleCode).ToString.Replace("'", "''") & "'," _
                             & " ArticleDescription=N'" & jRow(enmItem.ArticleDescription).ToString.Replace("'", "''") & "', " _
                             & " ArticleGroupId=" & Val(jRow(enmItem.ArticleGroupId).ToString) & ", " _
                             & " ArticleTypeId=" & Val(jRow(enmItem.ArticleTypeId).ToString) & ", " _
                             & " ArticleLPOId=" & Val(jRow(enmItem.ArticleLPOId).ToString) & ", " _
                             & " ArticleStatusId=" & Val(jRow(enmItem.ArticleStatusId).ToString) & ", " _
                             & " ArticleUnitId=" & Val(jRow(enmItem.ArticleUnitId).ToString) & ", " _
                             & " ArticleGenderId=" & Val(jRow(enmItem.ArticleGenderId).ToString) & ", " _
                             & " PackQty=" & Val(jRow(enmItem.PackQty).ToString) & ", " _
                             & " PurchasePrice=" & Val(jRow(enmItem.PurchasePrice).ToString) & ", " _
                             & " SalePrice=" & Val(jRow(enmItem.SalePrice).ToString) & ", " _
                             & " Cost_Price=" & Val(jRow(enmItem.Cost_Price).ToString) & ", " _
                             & " StockLevel=" & Val(jRow(enmItem.StockLevel).ToString) & ", " _
                             & " StockLevelOpt=" & Val(jRow(enmItem.StockLevelOpt).ToString) & ", " _
                             & " StockLevelMax=" & Val(jRow(enmItem.StockLevelMax).ToString) & ", " _
                             & " Active=" & IIf(jRow(enmItem.Active) = True, 1, 0) & ", " _
                             & " AccountId=" & Val(jRow(enmItem.AccountID).ToString) & ",ArticleCategoryId=" & Val(jRow.Item("ArticleCompanyId").ToString) & ",ArticleBrandId=" & Val(jRow(enmItem.ArticleBrandId).ToString) & "  WHERE ArticleId=" & Val(jRow(enmItem.ArticleId).ToString) & ""

                    cmd.CommandText = String.Empty
                    cmd.CommandText = strSQL
                    cmd.ExecuteNonQuery()


                    strSQL = String.Empty
                    strSQL = "Update ArticleDefTable Set ArticleDefTable.ArticleDescription=a.ArticleDescription, ArticleDefTable.ArticleUnitId = a.ArticleUnitId, ArticleDefTable.ArticleTypeId = a.ArticleTypeId, ArticleDefTable.ArticleLPOId = a.ArticleLPOId,ArticleDefTable.ArticleStatusId=a.ArticleStatusId,ArticleDefTable.ArticleGroupId = a.ArticleGroupId, ArticleDefTable.PurchasePrice = a.PurchasePrice,ArticleDefTable.SalePrice = a.SalePrice, ArticleDefTable.Cost_Price=a.Cost_Price, ArticleDefTable.PackQty = a.PackQty, ArticleDefTable.StockLevel = a.StockLevel,ArticleDefTable.StockLevelOpt = a.StockLevelOpt, ArticleDefTable.StockLevelMax = a.StockLevelMax, ArticleDefTable.AccountId = a.AccountId, ArticleDefTable.Active = a.Active,ArticleDefTable.ArticleCategoryId = a.ArticleCategoryId From ArticleDefTable,ArticleDefTableMaster a WHERE ArticledefTable.MasterId = a.ArticleId AND  ArticledefTable.MasterId=" & Val(jRow.Item(enmItem.ArticleId).ToString) & ""
                    cmd.CommandText = String.Empty
                    cmd.CommandText = strSQL
                    cmd.ExecuteNonQuery()


                    cmd.CommandText = String.Empty
                    cmd.CommandText = "Select ArticleId From ArticleDefTable WHERE MasterID=" & Val(jRow.Item(enmItem.ArticleId).ToString) & ""
                    Dim dtArt As New DataTable
                    Dim daArt As New OleDbDataAdapter
                    daArt.SelectCommand = cmd
                    daArt.Fill(dtArt)
                    dtArt.AcceptChanges()

                    For Each r As DataRow In dtArt.Rows


                        cmd.CommandText = ""
                        cmd.CommandText = "Select  * from IncrementReductionTable WHERE ArticleDefID=" & Val(r.Item("ArticleId").ToString) & " AND ID IN (Select Max(ID) From IncrementReductionTable WHERE ArticleDefID=" & Val(r.Item("ArticleId").ToString) & " Group By ArticleDefID)"
                        Dim da As New OleDbDataAdapter
                        Dim dtOldPrice As New DataTable
                        da.SelectCommand = cmd
                        da.Fill(dtOldPrice)
                        dtOldPrice.AcceptChanges()


                        cmd.CommandText = ""
                        cmd.CommandText = "Select GetDate() as ServerDate "
                        Dim daSrvDate As New OleDbDataAdapter
                        Dim dtSrvDate As New DataTable
                        daSrvDate.SelectCommand = cmd
                        daSrvDate.Fill(dtSrvDate)
                        dtSrvDate.AcceptChanges()




                        Dim dblOlPurchasePrice As Double = 0D
                        Dim dbloldSalePrice As Double = 0D
                        Dim dbloldCostPrice As Double = 0D

                        If dtOldPrice.Rows.Count > 0 Then
                            dblOlPurchasePrice = Val(dtOldPrice.Rows(0).Item("PurchaseNewPrice").ToString)
                            dbloldSalePrice = Val(dtOldPrice.Rows(0).Item("SaleNewPrice").ToString)
                            dbloldCostPrice = Val(dtOldPrice.Rows(0).Item("Cost_Price_New").ToString)
                        End If


                        cmd.CommandText = ""
                        cmd.CommandText = "INSERT INTO IncrementReductionTable( " _
                        & " IncrementReductionDate, " _
                        & " ArticleDefId," _
                        & " StockQty," _
                        & " PurchaseOldPrice," _
                        & " PurchaseNewPrice," _
                        & " SaleOldPrice," _
                        & " SaleNewPrice," _
                        & " Cost_Price_Old," _
                        & " Cost_Price_New ) " _
                        & " VALUES(" _
                        & " Convert(DateTime,'" & CDate(dtSrvDate.Rows(0).Item("ServerDate").ToString).ToString("yyyy-M-d hh:mm:ss tt") & "',102),  " _
                        & " " & Val(r.Item("ArticleId").ToString) & ",  " _
                        & " 0,  " _
                        & " " & dblOlPurchasePrice & ",  " _
                        & " " & Val(jRow(enmItem.PurchasePrice).ToString) & ",  " _
                        & " " & dbloldSalePrice & ",  " _
                        & " " & Val(jRow(enmItem.SalePrice).ToString) & ",  " _
                        & " " & dbloldCostPrice & ",  " _
                        & Val(jRow(enmItem.Cost_Price).ToString) & "  " _
                        & " )"
                        cmd.ExecuteNonQuery()

                    Next
                Next
            End If


            trans.Commit()
            Con.Close()
            FillGrid()

        Catch ex As Exception
            trans.Rollback()
            ShowErrorMessage(ex.Message)
        Finally
            Con.Close()
            Me.Cursor = Cursors.Default
        End Try
    End Sub

End Class
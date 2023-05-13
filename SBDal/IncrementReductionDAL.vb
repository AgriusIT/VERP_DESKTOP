''12-May-2014 TASK:2617 Imran Ali Price Update Problem On New Inventory Item 
Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Public Class IncrementReductionDAL

    Public Function Add(ByVal objModel As IncrementReduction, ByVal trans As SqlTransaction) As Boolean

        Try
            ''12-May-2014 TASK:2617 Imran Ali Price Update Problem On New Inventory Item 
            Dim strSQL As String = String.Empty
            strSQL = "Select ArticleDefId, IsNull(StockQty,0) as StockQty, IsNull(PurchaseOldPrice,0) as PurchaseOldPrice, IsNull(PurchaseNewPrice,0) as PurchaseNewPrice, IsNull(SaleOldPrice,0) as SaleOldPrice, IsNull(SaleNewPrice,0) as SaleNewPrice, IsNull(Cost_Price_Old,0) as Cost_Price_Old, IsNull(Cost_Price_New,0) as Cost_Price_New From IncrementReductionTable WHERE ArticleDefId=" & objModel.ArticleID & " AND ID IN(Select Max(ID) From IncrementReductionTable WHERE ArticleDefId=" & objModel.ArticleID & ")"
            Dim objDt As New DataTable
            objDt = UtilityDAL.GetDataTable(strSQL, trans)

            'check if price changed or not
            'Before against task:2617
            'If objModel.PurchasePriceOld = objModel.PurchasePriceNew AndAlso objModel.SalePriceNew = objModel.SalePriceOld Then
            '    Return True
            'End If
            If objDt IsNot Nothing Then
                If objDt.Rows.Count > 0 Then
                    If objModel.PurchasePriceOld = Val(objDt.Rows(0).Item("PurchaseOldPrice").ToString) AndAlso objModel.PurchasePriceNew = Val(objDt.Rows(0).Item("PurchaseNewPrice").ToString) AndAlso objModel.SalePriceOld = Val(objDt.Rows(0).Item("SaleOldPrice").ToString) AndAlso objModel.SalePriceNew = Val(objDt.Rows(0).Item("SaleNewPrice").ToString) Then
                        Return True
                    Else
                        objModel.Old_Cost_Price = Val(objDt.Rows(0).Item("Cost_Price_Old").ToString)
                    End If
                End If
            End If
            'End Task:2617

            'get current stock
            objModel.Stock = UtilityDAL.getStock(objModel.ArticleID, trans)

            'check if record already exits for samme date against item id
            'Dim strSQL As String = String.Empty
            strSQL = "SELECT   Id" _
            & " FROM IncrementReductionTable " _
            & " WHERE     (ArticleDefId = " & objModel.ArticleID & ") AND " _
            & " (CONVERT(datetime, CONVERT(varchar, LEFT(IncrementReductionDate, 11), 102)," _
            & " 102) = '" & objModel.IncrementDate.ToString("yyyy-MM-dd") & "')"
            Dim dr As DataRow = UtilityDAL.ReturnDataRow(strSQL, trans)

            If Not dr Is Nothing Then 'If Existing Record Here ....

                strSQL = " update IncrementReductionTable set PurchaseOldPrice = " & objModel.PurchasePriceOld & " , " _
                                    & " PurchaseNewPrice = " & objModel.PurchasePriceNew & " , " _
                                    & " SaleOldPrice = " & objModel.SalePriceOld & " , " _
                                    & " SaleNewPrice = " & objModel.SalePriceNew & ", Cost_Price_Old=" & objModel.Old_Cost_Price & ", Cost_Price_New=" & objModel.New_Cost_Price & " WHERE Id='" & dr.Item("Id").ToString & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL, Nothing)
                Return True
            Else
                objModel.Old_Cost_Price = 0D
                strSQL = "INSERT INTO IncrementReductionTable" _
                                                             & " (IncrementReductionDate, ArticleDefId, StockQty, PurchaseOldPrice, PurchaseNewPrice, SaleOldPrice, SaleNewPrice, Cost_Price_Old, Cost_Price_New) " _
                                                             & " VALUES('" & objModel.IncrementDate & "'," & objModel.ArticleID & "," & objModel.Stock & " ," & objModel.PurchasePriceOld & " ," & objModel.PurchasePriceNew & " ," & objModel.SalePriceOld & " ," & objModel.SalePriceNew & ", " & objModel.Old_Cost_Price & "," & objModel.New_Cost_Price & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                Return True
            End If


        Catch ex As SqlException
            Throw ex
        Catch ex As Exception
            Throw ex
        End Try

    End Function


End Class

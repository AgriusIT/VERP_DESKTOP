'' Muhammad Amin TASK TFS3776 made new feature of Bulk Stock Transfer. Dated 04-07-18
Imports SBDal
Imports SBModel
Imports System.Data.SqlClient

Public Class BulkStockTransferDAL
    Public Shared Function ProcessStockTransfer(ByVal DispatchNo As String, ByVal ReceivingNo As String, ByVal DispatchDate As Date, ByVal FromLocation As Integer, ByVal ToLocation As Integer, ByVal IsAverageRate As Boolean) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        Con.Open() ' Connection Open
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            ''Getting location wise stock
            Dim Query As String = String.Empty
            Dim TotalQty As Double = 0
            Dim TotalAmount As Double = 0
            Dim DispatchId As Integer = 0
            Dim ReceivingId As Integer = 0
            Dim DispatchStockTransferId As Integer = 0
            Dim ReceivingStockTransferId As Integer = 0
            If IsAverageRate = True Then
                Query = "SELECT Detail.ArticleDefId, Sum(IsNull(Detail.InQty, 0)-IsNull(Detail.OutQty, 0)) AS TotalQty, Sum(IsNull(Detail.In_PackQty, 0)-IsNull(Detail.Out_PackQty, 0)) AS Qty, IsNull(Detail.Pack_Qty, 0) AS PackQty, ISNULL(Article.PurchasePrice, 0) As Price, IsNull(Article.SalePrice, 0) AS SalePrice, IsNull(Article.Cost_Price, 0) AS CostPrice, Sum(IsNull(Detail.InQty, 0)-IsNull(Detail.OutQty, 0)) * ISNULL(Article.PurchasePrice, 0) As Amount, IsNull(Detail.LocationId, 0) AS LocationId " _
                    & " FROM StockDetailTable AS Detail LEFT OUTER JOIN ArticleDefView AS Article ON Detail.ArticleDefId = Article.ArticleId WHERE Detail.LocationId = " & FromLocation & " Group By Detail.ArticleDefId, ISNULL(Detail.LocationId, 0), ISNULL(Detail.Pack_Qty, 0), ISNULL(Article.PurchasePrice, 0), ISNULL(Article.Cost_Price, 0), ISNULL(Article.SalePrice, 0) HAVING Sum(IsNull(Detail.InQty, 0)-IsNull(Detail.OutQty, 0)) > 0"
            Else
                Query = "SELECT Detail.ArticleDefId, Sum(IsNull(Detail.InQty, 0)-IsNull(Detail.OutQty, 0)) AS TotalQty, Sum(IsNull(Detail.In_PackQty, 0)-IsNull(Detail.Out_PackQty, 0)) AS Qty, IsNull(Detail.Pack_Qty, 0) AS PackQty, Case When ISNULL(Article.Cost_Price, 0) > 0 Then ISNULL(Article.Cost_Price, 0) Else ISNULL(Article.PurchasePrice, 0) End AS Price, IsNull(Article.SalePrice, 0) AS SalePrice, IsNull(Article.Cost_Price, 0) AS CostPrice, Sum(IsNull(Detail.InQty, 0)-IsNull(Detail.OutQty, 0)) * Case When ISNULL(Article.Cost_Price, 0) > 0 Then ISNULL(Article.Cost_Price, 0) Else ISNULL(Article.PurchasePrice, 0) End AS Amount, ISNULL(Detail.LocationId, 0) AS LocationId " _
                  & " FROM StockDetailTable AS Detail LEFT OUTER JOIN ArticleDefView AS Article ON Detail.ArticleDefId = Article.ArticleId WHERE Detail.LocationId = " & FromLocation & " Group By Detail.ArticleDefId, ISNULL(Detail.LocationId, 0), ISNULL(Detail.Pack_Qty, 0), ISNULL(Article.PurchasePrice, 0), ISNULL(Article.Cost_Price, 0), ISNULL(Article.SalePrice, 0) HAVING Sum(IsNull(Detail.InQty, 0)-IsNull(Detail.OutQty, 0)) > 0"
            End If
            Dim dtStock As DataTable = UtilityDAL.GetDataTable(Query, trans)
            If dtStock.Rows.Count > 0 Then
                TotalQty = CDbl(dtStock.Compute("Sum(TotalQty)", "LocationId = " & FromLocation & ""))
                TotalAmount = CDbl(dtStock.Compute("Sum(Amount)", "LocationId = " & FromLocation & ""))

                ''Inserting record into DispatchMasterTable
                Query = " Insert Into DispatchMasterTable(locationId,DispatchNo,DispatchDate,DispatchQty,DispatchAmount, CashPaid, Remarks, VendorID, IsBulkStockTransferred) " _
                        & " Values(" & ToLocation & ", '" & DispatchNo.Replace("''", "'") & "', '" & DispatchDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & TotalQty & ", " & TotalAmount & ", " & TotalAmount & ", 'This transaction is made from Bulk Stock Transfer', " & ToLocation & ", 1) Select @@Identity"
                DispatchId = SQLHelper.ExecuteScaler(trans, CommandType.Text, Query)
                ''Inserting record into ReceivingMasterTable
                Query = " Insert Into ReceivingMasterTable(locationId, ReceivingNo, ReceivingDate, ReceivingQty, ReceivingAmount, CashPaid, PurchaseOrderID, Remarks, VendorId) " _
                     & " Values(" & FromLocation & ", '" & ReceivingNo.Replace("''", "'") & "', '" & DispatchDate.ToString("yyyy-M-d h:mm:ss tt") & "',  " & TotalQty & ", " & TotalAmount & ", " & TotalAmount & ", " & DispatchId & ", 'This transaction is made from Bulk Stock Transfer', " & FromLocation & ") Select @@Identity"
                ReceivingId = SQLHelper.ExecuteScaler(trans, CommandType.Text, Query)
                ''Stock master entries
                ''Stock Dispatch entry INSERT INTO StockMasterTable (DocNo, DocDate, DocType, Remarks)
                Query = " INSERT INTO StockMasterTable (DocNo, DocDate, DocType, Remarks) " _
                    & " Values('" & DispatchNo.Replace("''", "'") & "', '" & DispatchDate.ToString("yyyy-M-d h:mm:ss tt") & "', 7, 'This transaction is made from Bulk Stock Transfer') Select @@Identity"
                DispatchStockTransferId = SQLHelper.ExecuteScaler(trans, CommandType.Text, Query)
                ''Stock Receiving entry INSERT INTO StockMasterTable (DocNo, DocDate, DocType, Remarks)
                Query = " INSERT INTO StockMasterTable(DocNo, DocDate, DocType, Remarks) " _
                    & " Values('" & ReceivingNo.Replace("''", "'") & "', '" & DispatchDate.ToString("yyyy-M-d h:mm:ss tt") & "', 5, 'This transaction is made from Bulk Stock Transfer') Select @@Identity"
                ReceivingStockTransferId = SQLHelper.ExecuteScaler(trans, CommandType.Text, Query)
                Dim Count As Integer = 0
                For Each _ROW As DataRow In dtStock.Rows
                    Count += 1
                    ''SELECT Detail.ArticleDefId, Sum(IsNull(Detail.InQty, 0)-IsNull(Detail.OutQty, 0)) AS TotalQty, Sum(IsNull(Detail.In_PackQty, 0)-IsNull(Detail.Out_PackQty, 0)) AS Qty, IsNull(Article.Pack_Qty, 0) AS PackQty, Article.PurchasePrice As Price, IsNull(Article.SalePrice, 0) AS SalePrice, Detail.LocationId
                    ''Dispatch detail entry
                    Dim DispatchDetailId As Integer = 0
                    Query = " Insert Into DispatchDetailTable (DispatchId, ArticleDefId, ArticleSize, Sz1,Qty,Price,Sz7,CurrentPrice, BatchNo, BatchID,LocationID, Pack_Desc, Engine_No, Chassis_No, SalePrice) " _
                           & " Values(" & DispatchId & ", " & _ROW.Item("ArticleDefId") & ", '', " & _ROW.Item("Qty") & ", " & _ROW.Item("TotalQty") & ", " & _ROW.Item("Price") & ", " & _ROW.Item("PackQty") & ", " & _ROW.Item("Price") & ", '', 0, " & FromLocation & ", '', '', '', " & _ROW.Item("SalePrice") & " ) SELECT @@IDENTITY"
                    DispatchDetailId = SQLHelper.ExecuteScaler(trans, CommandType.Text, Query)
                    ''Receiving detail entry
                    Query = " INSERT INTO ReceivingDetailTable(ReceivingId, LocationId, ArticleDefId, ArticleSize, Sz1, Sz7, Qty, Price, CurrentPrice, BatchNo, BatchID, ReceivedQty, DispatchDetailId, FromLocationId) " _
                          & " Values(" & ReceivingId & ", " & ToLocation & ", " & _ROW.Item("ArticleDefId") & " ,'', " & _ROW.Item("Qty") & ", " & _ROW.Item("PackQty") & ", " & _ROW.Item("TotalQty") & ", " & _ROW.Item("Price") & ", " & _ROW.Item("Price") & ", '', 0, " & _ROW.Item("TotalQty") & ", " & DispatchDetailId & ",  " & FromLocation & " )"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, Query)
                    ''Dispatch Stock detail entry
                    Query = "Insert Into StockDetailTable(StockTransId, LocationId, ArticleDefId, InQty, OutQty, Rate, InAmount, OutAmount, Remarks,Engine_No, Chassis_No, Cost_Price, Pack_Qty, In_PackQty, Out_PackQty , BatchNo) " _
                          & " Values(" & DispatchStockTransferId & ", " & FromLocation & ", " & _ROW.Item("ArticleDefId") & " , 0, " & _ROW.Item("TotalQty") & ", " & _ROW.Item("Price") & ", 0, " & _ROW.Item("Price") * _ROW.Item("TotalQty") & ", '', '', '', " & _ROW.Item("CostPrice") & ", " & _ROW.Item("PackQty") & ", 0, " & _ROW.Item("Qty") & ", '') "
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, Query)

                    ''Receiving Stock detail entry
                    Query = "Insert Into StockDetailTable(StockTransId, LocationId, ArticleDefId, InQty, OutQty, Rate, InAmount, OutAmount, Remarks,Engine_No, Chassis_No, Cost_Price, Pack_Qty, In_PackQty, Out_PackQty , BatchNo) " _
                          & " Values(" & ReceivingStockTransferId & ", " & ToLocation & ", " & _ROW.Item("ArticleDefId") & " , " & _ROW.Item("TotalQty") & ", 0, " & _ROW.Item("Price") & ", " & _ROW.Item("Price") * _ROW.Item("TotalQty") & ", 0, '', '', '', " & _ROW.Item("CostPrice") & ", " & _ROW.Item("PackQty") & ", " & _ROW.Item("Qty") & ", 0, '') "
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, Query)
                Next
                ''Trans Commit here... 
                trans.Commit()
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            trans.Rollback()
            Return False
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
End Class

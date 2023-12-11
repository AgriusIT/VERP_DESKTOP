''19-May-2014 TASK:2642 Imran Ali Adjustment Avg Rate In ERP
''01-Jul-2014 TASK:2707 Imran Ali AvgRate Adjustment Voucher In Adjustment Average Rate
''02-Jul-2014 TASK:2712 Imran Ali Purchase Price Update through Adjustment Average Rate
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility

Public Class AdjustmentAveerageRate_Dal
    Enum enmAdj
        Doc_Id
        Doc_Date
        Doc_No
        Post
        Comments
        UserId
        EntryDate
    End Enum
    Public Function SaveRecord(ByVal objAdjAvg As AdjustmentAveragerateBE) As Boolean
        Dim objCon As New SqlConnection(SQLHelper.CON_STR)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim objTrans As SqlTransaction = objCon.BeginTransaction
        Try

            objAdjAvg.Doc_No = GetDocNo(objAdjAvg.Doc_No.Substring(0, objAdjAvg.Doc_No.Length - 5).ToString, objTrans)

            Dim strSQL As String = String.Empty
            ''''''''''' Adjustment Document ''''''''
            strSQL = "INSERT INTO AdjustmentAvgRateMasterTable(Doc_Date, Doc_No, Post,Comments,UserId,EntryDate) VALUES(Convert(DateTime, N'" & objAdjAvg.Doc_Date & "',102), N'" & objAdjAvg.Doc_No.Replace("'", "''") & "', " & IIf(objAdjAvg.Post = True, 1, 0) & ", N'" & objAdjAvg.Comments.Replace("'", "''") & "', " & objAdjAvg.UserId & ", Convert(DateTime, N'" & objAdjAvg.EntryDate & "', 102)) Select @@Identity"
            objAdjAvg.Doc_Id = SQLHelper.ExecuteScaler(objTrans, CommandType.Text, strSQL)

            ''''''''''' Stock Document ''''''''''''
            strSQL = String.Empty
            strSQL = "INSERT INTO StockMasterTable(DocNo, DocDate, DocType, Remarks) " _
            & " VALUES(N'" & objAdjAvg.Doc_No.Replace("'", "''") & "', Convert(Datetime, N'" & objAdjAvg.Doc_Date & "', 102), 0, N'" & objAdjAvg.Comments.Replace("'", "''") & "') Select @@Identity"
            objAdjAvg.StockTransId = SQLHelper.ExecuteScaler(objTrans, CommandType.Text, strSQL)


            ''01-Jul-2014 TASK:2707 Imran Ali AvgRate Adjustment Voucher In Adjustment Average Rate
            ''''''''' Voucher Document '''''''''''''
            strSQL = String.Empty
            strSQL = "INSERT INTO tblVoucher(Voucher_Date, Voucher_No, Voucher_Code, Voucher_Type_Id, Location_Id, Source, Post, Reference) " _
            & " VALUES(Convert(DateTime, N'" & objAdjAvg.Doc_Date & "', 102), N'" & objAdjAvg.Doc_No.Replace("'", "''") & "', N'" & objAdjAvg.Doc_No.Replace("'", "''") & "',1,0,'frmAdjeustmentAveragerate', " & IIf(objAdjAvg.Post = True, 1, 0) & ", N'" & objAdjAvg.Comments.Replace("'", "''") & "') Select @@Identity"
            objAdjAvg.VoucherId = SQLHelper.ExecuteScaler(objTrans, CommandType.Text, strSQL)
            'End Task:2707

            Call SaveDetailRecord(objAdjAvg, objTrans)
            '''''''''''''''''''''''''''''''''''''''

            objTrans.Commit()
            Return True
        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function
    Public Function UpdateRecord(ByVal objAdjAvg As AdjustmentAveragerateBE) As Boolean
        Dim objCon As New SqlConnection(SQLHelper.CON_STR)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim objTrans As SqlTransaction = objCon.BeginTransaction
        Dim objDt As New DataTable
        Try

            'objAdjAvg.Doc_No = GetDocNo(objAdjAvg.Doc_No.Substring(0, objAdjAvg.Doc_No.Length - 5).ToString, objTrans)

            Dim strSQL As String = String.Empty
            ''''''''''' Adjustment Document ''''''''

            strSQL = "UPDATE AdjustmentAvgRateMasterTable SET Doc_Date=Convert(DateTime, N'" & objAdjAvg.Doc_Date & "',102), Doc_No=N'" & objAdjAvg.Doc_No.Replace("'", "''") & "', Post=" & IIf(objAdjAvg.Post = True, 1, 0) & ",Comments=N'" & objAdjAvg.Comments.Replace("'", "''") & "',UserId=" & objAdjAvg.UserId & ",EntryDate=Convert(DateTime, N'" & objAdjAvg.EntryDate & "', 102) WHERE Doc_Id=" & objAdjAvg.Doc_Id & ""
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)
            strSQL = String.Empty

            strSQL = "Delete From AdjustmentAvgRateDetailTable WHERE Doc_Id In (select Doc_Id From AdjustmentAvgRateMasterTable WHERE Doc_No=N'" & objAdjAvg.Doc_No.Replace("'", "''") & "')"
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)


            ''''''''''' Stock Document ''''''''''''
            strSQL = String.Empty
            strSQL = "UPDATE StockMasterTable SET DocNo=N'" & objAdjAvg.Doc_No.Replace("'", "''") & "', DocDate=Convert(Datetime, N'" & objAdjAvg.Doc_Date & "', 102), DocType=0, Remarks=N'" & objAdjAvg.Comments.Replace("'", "''") & "' WHERE DocNo=N'" & objAdjAvg.Doc_No.Replace("'", "''") & "'"
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

            strSQL = String.Empty
            strSQL = "Delete From StockDetailTable WHERE StockTransId In (select StockTransId From StockMasterTable WHERE DocNo=N'" & objAdjAvg.Doc_No.Replace("'", "''") & "')"
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

            objAdjAvg.StockTransId = UtilityDAL.GetDataTable("Select StockTransId From StockMasterTable WHERE DocNo=N'" & objAdjAvg.Doc_No.Replace("'", "''") & "'", objTrans).Rows(0).Item(0).ToString

            '''''''''' Voucher Document '''''''''''''

            strSQL = "Select Voucher_Id From tblVoucher WHERE Voucher_No=N'" & objAdjAvg.Doc_No.Replace("'", "''") & "'"
            objAdjAvg.VoucherId = SQLHelper.ExecuteScaler(objTrans, CommandType.Text, strSQL)


            strSQL = String.Empty
            strSQL = "Update  tblVoucher SET Voucher_Date=Convert(DateTime, N'" & objAdjAvg.Doc_Date & "', 102), Post=" & IIf(objAdjAvg.Post = True, 1, 0) & ", Reference=N'" & objAdjAvg.Comments.Replace("'", "''") & "' WHERE Voucher_Id=" & objAdjAvg.VoucherId & ""
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)


            strSQL = String.Empty
            strSQL = "Delete From tblVoucherDetail WHERE Voucher_Id In (select Voucher_Id From tblVoucher WHERE Voucher_No=N'" & objAdjAvg.Doc_No.Replace("'", "''") & "')"
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)


            Call SaveDetailRecord(objAdjAvg, objTrans)
            '''''''''''''''''''''''''''''''''''''''

            objTrans.Commit()
            Return True
        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function

    Public Function DeleteRecord(ByVal objAdjAvg As AdjustmentAveragerateBE) As Boolean
        Dim objCon As New SqlConnection(SQLHelper.CON_STR)
        If objCon.State = ConnectionState.Closed Then objCon.Open()
        Dim objTrans As SqlTransaction = objCon.BeginTransaction
        Dim objDt As New DataTable
        Try

            'objAdjAvg.Doc_No = GetDocNo(objAdjAvg.Doc_No.Substring(0, objAdjAvg.Doc_No.Length - 5).ToString, objTrans)

            Dim strSQL As String = String.Empty
            ''''''''''' Adjustment Document ''''''''
            strSQL = String.Empty
            strSQL = "Delete From AdjustmentAvgRateDetailTable WHERE Doc_Id In (select Doc_Id From AdjustmentAvgRateMasterTable WHERE Doc_No=N'" & objAdjAvg.Doc_No.Replace("'", "''") & "')"
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

            strSQL = String.Empty
            strSQL = "Delete From AdjustmentAvgRateMasterTable WHERE Doc_Id=" & objAdjAvg.Doc_Id & ""
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

            ''''''''''' Stock Document ''''''''''''

            strSQL = String.Empty
            strSQL = "Delete From StockDetailTable WHERE StockTransId In (select StockTransId From StockMasterTable WHERE DocNo=N'" & objAdjAvg.Doc_No.Replace("'", "''") & "')"
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

            strSQL = String.Empty
            strSQL = "Delete From StockMasterTable WHERE DocNo=N'" & objAdjAvg.Doc_No.Replace("'", "''") & "'"
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

            ''01-Jul-2014 TASK:2707 Imran Ali AvgRate Adjustment Voucher In Adjustment Average Rate
            ''''''''' Voucher Document '''''''''''''

            strSQL = String.Empty
            strSQL = "Delete From tblVoucherDetail WHERE Voucher_Id In (select Voucher_Id From tblVoucher WHERE Voucher_No=N'" & objAdjAvg.Doc_No.Replace("'", "''") & "')"
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

            strSQL = String.Empty
            strSQL = "Delete From tblVoucher WHERE Voucher_No=N'" & objAdjAvg.Doc_No.Replace("'", "''") & "'"
            SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)
            'End Task:2707

            'Call SaveDetailRecord(objAdjAvg, objTrans)
            '''''''''''''''''''''''''''''''''''''''

            objTrans.Commit()
            Return True
        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        Finally
            objCon.Close()
        End Try
    End Function

    Public Function SaveDetailRecord(ByVal objAdjAvg As AdjustmentAveragerateBE, ByVal objTrans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            If Not objAdjAvg.AdjustmentAvgRateDetail Is Nothing Then
                For Each objAdjAvgDt As AdjustmentAvgRateDetailBE In objAdjAvg.AdjustmentAvgRateDetail

                    '''''''''' Adjustment Detail Document '''''''''''''
                    strSQL = String.Empty
                    strSQL = "INSERT INTO AdjustmentAvgRateDetailTable(Doc_Id, LocationId,ArticleDefId,ArticleSize,Current_Stock, Current_Avg_Rate, Adj_New_Cost_Price,Adj_Amount, PurchaseAccountId, CGSAccountId, FromDate, ToDate) " _
                    & " VALUES(" & objAdjAvg.Doc_Id & ", " & objAdjAvgDt.LocationId & ", " & objAdjAvgDt.ArticleDefId & ", N'" & objAdjAvgDt.ArticleSize.Replace("'", "''") & "', " & objAdjAvgDt.CurrentStock & ", " & objAdjAvgDt.Current_Avg_Rate & ", " & objAdjAvgDt.Adj_New_Cost_Price & ", " & objAdjAvgDt.Adj_Amount & "," & objAdjAvgDt.PurchaseAccountId & "," & objAdjAvgDt.CGSAccountId & " , " & IIf(objAdjAvgDt.FromDate = Date.MinValue, "Null", " Convert(DateTime, '" & objAdjAvgDt.FromDate & "', 102 )") & " , " & IIf(objAdjAvgDt.ToDate = Date.MinValue, "Null", " Convert(DateTime, '" & objAdjAvgDt.ToDate & "', 102 )") & " ) "
                    SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

                    ''''''''''''''''' Stock Detail Document ''''''''''''

                    strSQL = String.Empty
                    strSQL = "INSERT INTO StockDetailTable(StockTransId, LocationId,ArticleDefId,InQty,OutQty,Rate,InAmount,OutAmount,Cost_Price) " _
                    & " VALUES(" & objAdjAvg.StockTransId & ", " & objAdjAvgDt.LocationId & ", " & objAdjAvgDt.ArticleDefId & ", 0,0," & objAdjAvgDt.Adj_New_Cost_Price & ", " & IIf(objAdjAvgDt.Adj_Amount < 0, 0, Math.Abs(objAdjAvgDt.Adj_Amount)) & ", " & IIf(objAdjAvgDt.Adj_Amount > 0, 0, Math.Abs(objAdjAvgDt.Adj_Amount)) & "," & objAdjAvgDt.Adj_New_Cost_Price & ")"
                    SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)


                    ''01-Jul-2014 TASK:2707 Imran Ali AvgRate Adjustment Voucher In Adjustment Average Rate

                    ''''''''''''''''' Voucher Detail Table ''''''''''''''
                    If objAdjAvgDt.Adj_Amount < 0 Then

                        strSQL = String.Empty
                        strSQL = "INSERT INTO tblVoucherDetail(Location_Id,Voucher_Id, coa_detail_id, debit_amount,credit_amount,comments) " _
                        & " VALUES(1," & objAdjAvg.VoucherId & ", " & objAdjAvgDt.PurchaseAccountId & ", " & Math.Abs(objAdjAvgDt.Adj_Amount) & ",0,'Adjustment stock in trade')"
                        SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

                        strSQL = String.Empty
                        strSQL = "INSERT INTO tblVoucherDetail(Location_Id,Voucher_Id, coa_detail_id, debit_amount,credit_amount,comments) " _
                        & " VALUES(1," & objAdjAvg.VoucherId & ", " & objAdjAvgDt.CGSAccountId & ",0, " & Math.Abs(objAdjAvgDt.Adj_Amount) & ",'Adjustment cost of goods')"
                        SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

                    Else

                        strSQL = String.Empty
                        strSQL = "INSERT INTO tblVoucherDetail(Location_Id,Voucher_Id, coa_detail_id, debit_amount,credit_amount,comments) " _
                        & " VALUES(1," & objAdjAvg.VoucherId & ", " & objAdjAvgDt.PurchaseAccountId & ", 0," & objAdjAvgDt.Adj_Amount & ",'Adjustment stock in trade')"
                        SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)

                        strSQL = String.Empty
                        strSQL = "INSERT INTO tblVoucherDetail(Location_Id,Voucher_Id, coa_detail_id, debit_amount,credit_amount,comments) " _
                        & " VALUES(1," & objAdjAvg.VoucherId & ", " & objAdjAvgDt.CGSAccountId & ", " & objAdjAvgDt.Adj_Amount & ",0,'Adjustment cost of goods')"
                        SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL)
                    End If
                    'End Task:2707

                    ''02-Jul-2014 TASK:2712 Imran Ali Purchase Price Update through Adjustment Average Rate

                    strSQL = String.Empty
                    strSQL = "UPDATE ArticleDefTableMaster SET PurchasePrice=" & objAdjAvgDt.Adj_New_Cost_Price & ", Cost_Price=" & objAdjAvgDt.Adj_New_Cost_Price & " WHERE ArticleId IN(Select MasterId From ArticleDefTable WHERE ArticleId=" & objAdjAvgDt.ArticleDefId & ")"
                    SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL, Nothing)

                    strSQL = String.Empty
                    strSQL = "UPDATE ArticleDefTable SET PurchasePrice=" & objAdjAvgDt.Adj_New_Cost_Price & ",Cost_Price=" & objAdjAvgDt.Adj_New_Cost_Price & " WHERE ArticleId=" & objAdjAvgDt.ArticleDefId & ""
                    SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL, Nothing)

                    strSQL = String.Empty
                    strSQL = "Select SalePrice From ArticleDefTable WHERE ArticleId=" & objAdjAvgDt.ArticleDefId & ""
                    Dim dblSalePrice As Double = SQLHelper.ExecuteScaler(objTrans, CommandType.Text, strSQL)

                    strSQL = String.Empty
                    strSQL = "Select PurchasePrice From ArticleDefTable WHERE ArticleId=" & objAdjAvgDt.ArticleDefId & ""
                    Dim dblPurchasePrice As Double = SQLHelper.ExecuteScaler(objTrans, CommandType.Text, strSQL)

                    strSQL = String.Empty
                    strSQL = "Select Cost_Price From ArticleDefTable WHERE ArticleId=" & objAdjAvgDt.ArticleDefId & ""
                    Dim dblCostPrice As Double = SQLHelper.ExecuteScaler(objTrans, CommandType.Text, strSQL)

                    strSQL = String.Empty
                    strSQL = "INSERT INTO IncrementReductionTable(IncrementReductionDate, ArticleDefId, StockQty,PurchaseOldPrice,PurchaseNewPrice,SaleOldPrice,SaleNewPrice,Cost_Price_Old,Cost_Price_New) " _
                    & " Values(Convert(datetime, N'" & Now & "', 102)," & objAdjAvgDt.ArticleDefId & " , " & objAdjAvgDt.CurrentStock & "," & dblPurchasePrice & "," & objAdjAvgDt.Adj_New_Cost_Price & "," & dblSalePrice & "," & dblSalePrice & "," & dblCostPrice & "," & objAdjAvgDt.Adj_New_Cost_Price & ")"
                    SQLHelper.ExecuteNonQuery(objTrans, CommandType.Text, strSQL, Nothing)
                    'End Task:2712

                Next
            End If
            Return True
        Catch ex As Exception
            objTrans.Rollback()
            Throw ex
        End Try
    End Function

    Public Function GetAllRecords(Optional ByVal Condition As String = "")
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SELECT " & IIf(Condition = "All", "", "TOP 50") & " Doc_Id,Doc_Date,Doc_No, Post,Comments,UserId, EntryDate From AdjustmentAvgRateMasterTable ORDER BY Doc_No DESC"
            Dim objdt As New DataTable
            objdt = UtilityDAL.GetDataTable(strSQL)
            Return objdt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetAllRecordDetail(ByVal DocId As Integer)
        Try
            Dim strSQL As String = String.Empty
            strSQL = "SELECT dbo.AdjustmentAvgRateDetailTable.Doc_Id, dbo.AdjustmentAvgRateDetailTable.LocationId, dbo.AdjustmentAvgRateDetailTable.ArticleDefId, " _
                    & " dbo.ArticleDefView.ArticleDescription, dbo.ArticleDefView.ArticleCode, dbo.ArticleDefView.ArticleSizeName, dbo.ArticleDefView.ArticleColorName, " _
                    & " dbo.AdjustmentAvgRateDetailTable.ArticleSize, IsNull(dbo.AdjustmentAvgRateDetailTable.Current_Stock,0) as Current_Stock, IsNull(dbo.AdjustmentAvgRateDetailTable.Current_Avg_Rate,0) as Current_Avg_Rate,  " _
                    & " ISNULL(dbo.AdjustmentAvgRateDetailTable.Adj_New_Cost_Price,0) as Adj_New_Cost_Price, ISnull(dbo.AdjustmentAvgRateDetailTable.Adj_Amount,0) as Adj_Amount, ISNULL(dbo.AdjustmentAvgRateDetailTable.PurchaseAccountId,0) as PurchaseAccountId, IsNull(dbo.AdjustmentAvgRateDetailTable.CGSAccountId,0) as CGSAccountId, AdjustmentAvgRateDetailTable.FromDate, AdjustmentAvgRateDetailTable.ToDate  " _
                    & " FROM dbo.ArticleDefView INNER JOIN " _
                    & " dbo.AdjustmentAvgRateDetailTable ON dbo.ArticleDefView.ArticleId = dbo.AdjustmentAvgRateDetailTable.ArticleDefId " _
                    & " WHERE dbo.AdjustmentAvgRateDetailTable.Doc_Id = " & DocId & ""
            Dim objdt As New DataTable
            objdt = UtilityDAL.GetDataTable(strSQL)
            Return objdt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetDocNo(ByVal FirstChar As String, Optional ByVal objTrans As SqlTransaction = Nothing) As String
        Try
            Dim serial As Integer = 0I
            Dim sererialno As String = String.Empty
            Dim strSQL As String = ""
            strSQL = "SELECT IsNull(Max(Right(Doc_No,5)),0)+1 Ser From AdjustmentAvgRateMasterTable WHERE LEFT(Doc_No," & FirstChar.Length & ")=N'" & FirstChar & "'"
            Dim objDt As New DataTable
            objDt = UtilityDAL.GetDataTable(strSQL, objTrans)
            If objDt IsNot Nothing Then
                If objDt.Rows.Count > 0 Then
                    serial = Val(objDt.Rows(0).Item(0).ToString)
                Else
                    serial = 1
                End If
            Else
                serial = 1
            End If
            sererialno = "" & FirstChar & "" & Right("00000" + CStr(serial), 5)
            Return sererialno
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetCurrentStock(ByVal ItemId As Integer) As DataTable
        Try

            Dim objDt As New DataTable
            objDt = UtilityDAL.GetDataTable("Select ArticleDefId, SUM(Isnull(InQty,0)-IsNull(OutQty,0)) as Current_Stock, SUM(((Isnull(InAmount,0)-IsNull(OutAmount,0)))) as CurrentAvgRate From StockDetailTable WHERE ArticleDefId=" & ItemId & " Group By ArticleDefId ")
            Return objDt

        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

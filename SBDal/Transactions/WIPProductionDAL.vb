'Task 201507015 Ali Ansari Save,edit,delete record in stock table 07/11/2015
Imports SBDal
Imports SBModel
Imports SBUtility
Imports System.Data
Imports System
Imports System.Data.SqlClient

Public Class WIPProductionDAL
    Public Function Add(ByVal ObjMod As WIPProductionMasterBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty
            strSQL = "INSERT INTO WIPProductionMasterTable(DocNo,DocDate,LotNo,CustomerCode,Remarks,UserName,EntryDate,RefReceivingId) " _
            & " VALUES('" & ObjMod.DocNo.Replace("'", "''") & "',Convert(Datetime,'" & ObjMod.DocDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),'" & ObjMod.LotNo.Replace("'", "''") & "'," & ObjMod.CustomerCode & ",'" & ObjMod.Remarks.Replace("'", "''") & "','" & ObjMod.UserName.Replace("'", "''") & "',Convert(DateTime,'" & ObjMod.EntryDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102)," & ObjMod.RefReceivingId & ")Select @@Identity"
            ObjMod.DocId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)


            AddDetail(ObjMod, trans)
            'AddVoucher(ObjMod.Voucher, trans)
            'Altered By Ali Ansari against Task# 201507015 save stock records
            'AddStockMaster(ObjMod, trans)
            'Altered By Ali Ansari against Task# 201507015 save stock records

            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function Update(ByVal ObjMod As WIPProductionMasterBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction

        Try
            Dim strSQL As String = String.Empty
            'Altered By Ali Ansari against Task# 201507015 update stock master and detail
            'strSQL = String.Empty
            'strSQL = "Update Stockmastertable SET DocNo='" & ObjMod.DocNo.Replace("'", "''") & "',DocDate=Convert(Datetime,'" & ObjMod.DocDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),Remarks='" & ObjMod.Remarks.Replace("'", "''") & "' WHERE docno='" & ObjMod.DocNo & "'"
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)


            'strSQL = String.Empty
            'strSQL = "Delete From Stockdetailtable WHERE StockTransId In(Select StockTransId From StockMasterTable WHERE DocNo='" & ObjMod.DocNo & "')"
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)


            'AddStockDetail(ObjMod, trans)
            'Altered By Ali Ansari against Task# 201507015 update stock master and detail
            strSQL = String.Empty
            strSQL = "Update WIPProductionMasterTable SET DocNo='" & ObjMod.DocNo.Replace("'", "''") & "',DocDate=Convert(Datetime,'" & ObjMod.DocDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),LotNo='" & ObjMod.LotNo.Replace("'", "''") & "',CustomerCode=" & ObjMod.CustomerCode & ",Remarks='" & ObjMod.Remarks.Replace("'", "''") & "',UserName='" & ObjMod.UserName.Replace("'", "''") & "',EntryDate=Convert(DateTime,'" & ObjMod.EntryDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),RefReceivingId=" & ObjMod.RefReceivingId & " WHERE DocId=" & ObjMod.DocId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)



            strSQL = String.Empty
            strSQL = "Delete From WIPProductionDetailTable WHERE DocId=" & ObjMod.DocId & " AND TransType='" & ObjMod.WIPProductionDetail.Item(0).TransType.Replace("'", "''") & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            AddDetail(ObjMod, trans)
            'UpdateVoucher(ObjMod.Voucher, trans)


            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function Delete(ByVal ObjMod As WIPProductionMasterBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try

            Dim strSQL As String = String.Empty


            'strSQL = String.Empty
            'strSQL = "Delete From tblVoucherDetail where voucher_id in(Select voucher_Id from tblVoucher where voucher_no='" & ObjMod.Voucher.VNo & "')"
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            'strSQL = String.Empty
            'strSQL = "Delete From tblVoucher where voucher_no='" & ObjMod.Voucher.VNo & "'"
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            strSQL = String.Empty
            strSQL = "Delete From WIPProductionDetailTable WHERE DocId=" & ObjMod.DocId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            ''Altered By Ali Ansari against Task# 201507015 Delete from stock detail table
            'strSQL = String.Empty
            'strSQL = "Delete From Stockdetailtable where StockTransId in(Select StockTransId from stockmastertable where DocNo='" & ObjMod.DocNo & "')"
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            ''Altered By Ali Ansari against Task# 201507015 Delete from stock detail table

            ''Altered By Ali Ansari against Task# 201507015 Delete from stock master table
            'strSQL = String.Empty
            'strSQL = "Delete From  StockMastertable WHERE Docno='" & ObjMod.DocNo & "'"
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            ''Altered By Ali Ansari against Task# 201507015 Delete from stock master table

            strSQL = String.Empty
            strSQL = "Delete From  WIPProductionMasterTable WHERE Docid='" & ObjMod.DocId & "'"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)


            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function
    Public Function AddDetail(ByVal objMod As WIPProductionMasterBE, ByVal trans As SqlTransaction)
        Try
            Dim strSQL As String = String.Empty
            If objMod.WIPProductionDetail IsNot Nothing Then
                If objMod.WIPProductionDetail.Count > 0 Then
                    For Each objDetailMod As WIPProductionDetailBE In objMod.WIPProductionDetail
                        strSQL = "INSERT INTO WIPProductionDetailTable(DocId, LocationId, ArticleDefId,PackQty,Qty,TotalQty,Rate,TotalAmount,TransType,Comments,VehicleNo,GatePassNo,TruckNo) " _
                        & " Values(" & objMod.DocId & ", " & objDetailMod.LocationId & "," & objDetailMod.ArticleDefId & "," & objDetailMod.PackQty & "," & objDetailMod.Qty & "," & objDetailMod.TotalQty & "," & objDetailMod.Rate & "," & objDetailMod.TotalAmount & ",'" & objDetailMod.TransType.Replace("'", "''") & "','" & objDetailMod.Comments.Replace("'", "''") & "', '" & objDetailMod.VehicleNo.Replace("'", "''") & "','" & objDetailMod.GatepassNo.Replace("'", "''") & "','" & objDetailMod.TruckNo.Replace("'", "''") & "')"
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                    Next
                End If
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Altered By Ali Ansari against Task# 201507015 Save stock Detail table
    Public Function AddStockDetail(ByVal objMod As WIPProductionMasterBE, ByVal trans As SqlTransaction)
        Try
            Dim strSQL As String = String.Empty


            Dim intStockTransId As Integer = 0I
            intStockTransId = SQLHelper.ExecuteScaler(trans, CommandType.Text, "Select StockTransId From StockMasterTable WHERE DocNo='" & objMod.DocNo & "'")



            If objMod.WIPProductionDetail IsNot Nothing Then
                If objMod.WIPProductionDetail.Count > 0 Then
                    For Each objDetailMod As WIPProductionDetailBE In objMod.WIPProductionDetail
                        strSQL = "INSERT INTO StockDetailTable(StockTransId,LocationId,ArticleDefId,InQty,OutQty,Rate,InAmount,OutAmount,Remarks,Cost_Price) " _
                        & " Values(" & intStockTransId & ", " & objDetailMod.LocationId & "," & objDetailMod.ArticleDefId & "," & objDetailMod.InQty & "," & objDetailMod.OutQty & "," & objDetailMod.Rate & "," & objDetailMod.InAmount & "," & objDetailMod.OutAmount & ",'" & objDetailMod.Comments.Replace("'", "''") & "', 0)"
                        SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                    Next
                End If
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Altered By Ali Ansari against Task# 201507015 Save Detail  table
    Public Function GetAll() As DataTable
        Try
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable("SELECT WIPProductionMasterTable.DocId, WIPProductionMasterTable.DocNo, WIPProductionMasterTable.DocDate, WIPProductionMasterTable.LotNo, WIPProductionMasterTable.CustomerCode, " _
           & " vwCOADetail.detail_code as [Account Code], vwCOADetail.detail_title as [Customer], WIPProductionMasterTable.Remarks, WIPProductionMasterTable.UserName, WIPProductionMasterTable.EntryDate, IsNull(RefReceivingId,0) as RefReceivingId " _
           & "  FROM  vwCOADetail RIGHT OUTER JOIN WIPProductionMasterTable ON vwCOADetail.coa_detail_id = WIPProductionMasterTable.CustomerCode Order BY WIPProductionMasterTable.DocId DESC ")
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetDetailRecords(ByVal DocId As Integer, ByVal TransType As String) As DataTable
        Try
            Dim dt As New DataTable
            'dt = UtilityDAL.GetDataTable("SELECT WIPProductionDetailTable.LocationId, WIPProductionDetailTable.ArticleDefId, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleColorName,  " _
            '    & " ArticleDefView.ArticleSizeName, WIPProductionDetailTable.ArticleSize, WIPProductionDetailTable.PackQty,WIPProductionDetailTable.Qty, WIPProductionDetailTable.TotalQty, " _
            '    & " WIPProductionDetailTable.Rate, WIPProductionDetailTable.TotalAmount, WIPProductionDetailTable.Comments FROM WIPProductionDetailTable INNER JOIN ArticleDefView ON WIPProductionDetailTable.ArticleDefId = ArticleDefView.ArticleId WHERE WIPProductionDetailTable.DocId=" & DocId & " AND WIPProductionDetailTable.TransType=N'" & TransType.Replace("'", "''") & "' Order By WIPProductionDetailTable.DocDetailID ASC ")
            dt = UtilityDAL.GetDataTable("SELECT Case When IsNull(WIPProductionDetailTable.LocationId,0)=0 then IsNull(loc.Location_Id,0) Else IsNull(WIPProductionDetailTable.LocationId,0) End As LocationId, WIPProductionDetailTable.ArticleDefId, IsNull(WIPProductionDetailTable.PackQty,0) as PackQty, IsNull(WIPProductionDetailTable.Qty,0) as Qty, IsNull(WIPProductionDetailTable.TotalQty,0) as TotalQty, " _
               & " IsNull(WIPProductionDetailTable.Rate,0) as Rate, IsNull(WIPProductionDetailTable.TotalAmount,0) as TotalAmount, WIPProductionDetailTable.Comments, WIPProductionDetailTable.VehicleNo,WIPProductionDetailTable.GatepassNo, WIPProductionDetailTable.TruckNo, WIPProductionDetailTable.TransType,isnull(articledefview.SalesAccountId,0) as SalesAccountId FROM WIPProductionDetailTable INNER JOIN ArticleDefView ON WIPProductionDetailTable.ArticleDefId = ArticleDefView.ArticleId LEFT OUTER JOIN(Select Max(Location_Id) as Location_Id From tblDefLocation) Loc on Loc.Location_Id = WIPProductionDetailTable.LocationId WHERE WIPProductionDetailTable.DocId=" & DocId & " AND WIPProductionDetailTable.TransType=N'" & TransType.Replace("'", "''") & "' Order By WIPProductionDetailTable.DocDetailID ASC ")
            dt.AcceptChanges()
            dt.Columns("TotalAmount").Expression = "([TotalQty]*[Rate])"
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetInvoiceDetailRecords(ByVal DocId As Integer, ByVal InvoiceNo As String) As DataTable
        Try
            Dim dt As New DataTable
            'dt = UtilityDAL.GetDataTable("SELECT WIPProductionDetailTable.LocationId, WIPProductionDetailTable.ArticleDefId, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleColorName,  " _
            '    & " ArticleDefView.ArticleSizeName, WIPProductionDetailTable.ArticleSize, WIPProductionDetailTable.PackQty,WIPProductionDetailTable.Qty, WIPProductionDetailTable.TotalQty, " _
            '    & " WIPProductionDetailTable.Rate, WIPProductionDetailTable.TotalAmount, WIPProductionDetailTable.Comments FROM WIPProductionDetailTable INNER JOIN ArticleDefView ON WIPProductionDetailTable.ArticleDefId = ArticleDefView.ArticleId WHERE WIPProductionDetailTable.DocId=" & DocId & " AND WIPProductionDetailTable.TransType=N'" & TransType.Replace("'", "''") & "' Order By WIPProductionDetailTable.DocDetailID ASC ")
            dt = UtilityDAL.GetDataTable("SELECT Case When IsNull(WIPProductionDetailTable.LocationId,0)=0 then IsNull(loc.Location_Id,0) Else IsNull(WIPProductionDetailTable.LocationId,0) End As LocationId, WIPProductionDetailTable.ArticleDefId, IsNull(WIPProductionDetailTable.PackQty,0) as PackQty, IsNull(WIPProductionDetailTable.Qty,0) as Qty, IsNull(WIPProductionDetailTable.TotalQty,0) as TotalQty, " _
               & " IsNull(WIPProductionDetailTable.Rate,0) as Rate, IsNull(WIPProductionDetailTable.TotalAmount,0) as TotalAmount, WIPProductionDetailTable.Comments, WIPProductionDetailTable.VehicleNo,WIPProductionDetailTable.GatepassNo, WIPProductionDetailTable.TruckNo, WIPProductionDetailTable.TransType,isnull(articledefview.SalesAccountId,0) as SalesAccountId  FROM WIPProductionDetailTable INNER JOIN ArticleDefView ON WIPProductionDetailTable.ArticleDefId = ArticleDefView.ArticleId LEFT OUTER JOIN(Select Max(Location_Id) as Location_Id From tblDefLocation) Loc on Loc.Location_Id = WIPProductionDetailTable.LocationId WHERE WIPProductionDetailTable.DocId=" & DocId & " AND WIPProductionDetailTable.Invoice_No=N'" & InvoiceNo.Replace("'", "''") & "' Order By WIPProductionDetailTable.DocDetailID ASC ")
            dt.AcceptChanges()
            dt.Columns("TotalAmount").Expression = "([TotalQty]*[Rate])"
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetRecvDetailRecords(ByVal DocId As Integer) As DataTable
        Try
            Dim dt As New DataTable
            'dt = UtilityDAL.GetDataTable("SELECT WIPProductionDetailTable.LocationId, WIPProductionDetailTable.ArticleDefId, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleColorName,  " _
            '    & " ArticleDefView.ArticleSizeName, WIPProductionDetailTable.ArticleSize, WIPProductionDetailTable.PackQty,WIPProductionDetailTable.Qty, WIPProductionDetailTable.TotalQty, " _
            '    & " WIPProductionDetailTable.Rate, WIPProductionDetailTable.TotalAmount, WIPProductionDetailTable.Comments FROM WIPProductionDetailTable INNER JOIN ArticleDefView ON WIPProductionDetailTable.ArticleDefId = ArticleDefView.ArticleId WHERE WIPProductionDetailTable.DocId=" & DocId & " AND WIPProductionDetailTable.TransType=N'" & TransType.Replace("'", "''") & "' Order By WIPProductionDetailTable.DocDetailID ASC ")
            dt = UtilityDAL.GetDataTable("SELECT Case When IsNull(Recv_D.LocationId,0)=0 then IsNull(loc.Location_Id,0) Else IsNull(Recv_D.LocationId,0) End As LocationId, Recv_D.ArticleDefId, IsNull(Recv_D.Sz7,0) as PackQty, IsNull(Recv_D.Sz1,0) as Qty, IsNull(Recv_D.Qty,0) as TotalQty, " _
               & " IsNull(Recv_D.Price,0) as Rate, (IsNull(Recv_D.Qty,0) * IsNull(Recv_D.Price,0)) as TotalAmount, Recv_D.Comments, '' as VehicleNo,'' as GatepassNo, '' as TruckNo, 'WIP' as TransType FROM ReceivingDetailTable Recv_D INNER JOIN ArticleDefView ON Recv_D.ArticleDefId = ArticleDefView.ArticleId LEFT OUTER JOIN(Select Max(Location_Id) as Location_Id From tblDefLocation) Loc on Loc.Location_Id = Recv_D.LocationId WHERE Recv_D.ReceivingId=" & DocId & " Order By Recv_D.ReceivingDetailId ASC ")
            dt.AcceptChanges()
            dt.Columns("TotalAmount").Expression = "([TotalQty]*[Rate])"
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function AddVoucher(ByVal voucher As VouchersMaster, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim bln As Boolean = False
            bln = New VouchersDAL().Add(voucher, trans)
            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function UpdateVoucher(ByVal voucher As VouchersMaster, ByVal trans As SqlTransaction) As Boolean
        Try
            Dim bln As Boolean = False
            bln = New VouchersDAL().Update(voucher, trans)
            Return bln
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Altered By Ali Ansari against Task# 201507015 Save stock Master table
    Public Function AddStockMaster(ByVal objMod As WIPProductionMasterBE, ByVal trans As SqlTransaction)
        Try
            Dim strSQL As String = String.Empty
            strSQL = String.Empty
            strSQL = "INSERT INTO stockmastertable(DocNo,DocDate,Doctype,Remarks) " _
                       & " VALUES('" & objMod.DocNo & "',Convert(Datetime,'" & objMod.DocDate.ToString("yyyy-M-d hh:mm:ss tt") & "',102),3,'" & objMod.Remarks.Replace("'", "''") & "')Select @@Identity"
            SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            AddStockDetail(objMod, trans)

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Altered By Ali Ansari against Task# 201507015 Save stock Master table
End Class

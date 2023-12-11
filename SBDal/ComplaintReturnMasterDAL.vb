Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Imports System.Data.OleDb

Public Class ComplaintReturnMasterDAL
    Dim ComplaintReturnId As Integer
    Dim ComplaintId As Integer
    Dim post As Boolean = False
    Dim stocktransId As Integer
    Function Add(ByVal objModel As ComplaintReturnMasterBE, Optional ByVal Voucher As VouchersMaster = Nothing) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Add(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Add(ByVal objModel As ComplaintReturnMasterBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            ComplaintId = objModel.ComplaintId
            post = objModel.Post
            strSQL = "insert into  ComplaintReturnMaster (ComplaintReturnNo, ComplaintReturnDate, ComplaintId, CustomerId, ReceivedBy, ContactNo, Remarks, Posted) values (N'" & objModel.ComplaintReturnNo.Replace("'", "''") & "', N'" & objModel.ComplaintReturnDate & "', N'" & objModel.ComplaintId & "', N'" & objModel.CustomerId & "', N'" & objModel.ReceivedBy.Replace("'", "''") & "', N'" & objModel.ContactNo.Replace("'", "''") & "', N'" & objModel.Remarks.Replace("'", "''") & "', N'" & objModel.Post & "') Select @@Identity"
            ComplaintReturnId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            'objModel.ActivityLog.ActivityName = "Save"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function AddComplaintReturnDetail(ByVal list As List(Of ComplaintReturnDetailBE)) As Boolean
        Dim objCommand As New OleDbCommand
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            For Each obj As ComplaintReturnDetailBE In list
                str = "insert into  ComplaintReturnDetail (ComplaintReturnId, LocationId, ItemId, AlternateId, Unit, Price, Sz1, Sz7, Qty, Comments, RequestDetailId, PurchasePrice) values (N'" & ComplaintReturnId & "', N'" & obj.LocationId & "', N'" & obj.ItemId & "', N'" & obj.AlternateId & "', N'" & obj.Unit.Replace("'", "''") & "', N'" & obj.Price & "', N'" & obj.Sz1 & "', N'" & obj.Sz7 & "', N'" & obj.Qty & "', N'" & obj.Comments.Replace("'", "''") & "', N'" & obj.RequestDetailId & "', " & obj.PurchasePrice & ")"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                str = "Update ComplaintRequestDetail set DeliverdQty = (ISNULL(DeliverdQty, 0) + " & obj.Sz1 & ") where ComplaintDetailId = " & obj.RequestDetailId & ""
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                If post = True Then
                    str = "Insert Into StockDetailTable(StockTransId, LocationId, ArticleDefId, InQty, OutQty, Rate, InAmount, OutAmount, Remarks, Pack_Qty, In_PackQty, Out_PackQty , BatchNo) " _
              & " Values (" & stocktransId & ", " & obj.LocationId & ",  " & obj.AlternateId & ", " & 0 & ", " & obj.Sz1 * obj.Sz7 & ", " & obj.PurchasePrice & ", " & obj.Sz7 * obj.Sz1 * obj.PurchasePrice & ", " & 0 & ", N'" & obj.Comments.Replace("'", "''") & "', " & 1 & ", " & 0 & ", " & obj.Sz1 * obj.Sz7 & ", N'" & 0 & "')"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                End If
            Next
            str = "UPDATE ComplaintRequestMaster set Status =Case When IsNull(Complaint.RemainingQty,0) > 0 then 'Open' ELSE 'Close' END From ComplaintRequestMaster, (Select ComplaintId, IsNull(SUM(IsNull(Sz1,0)-IsNull(DeliverdQty,0)),0) as RemainingQty From ComplaintRequestDetail WHERE ComplaintId=" & ComplaintId & " Group By ComplaintId) Complaint WHERE Complaint.ComplaintId = ComplaintRequestMaster.ComplaintId AND ComplaintRequestMaster.ComplaintId = " & ComplaintId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function

    Function Update(ByVal objModel As ComplaintReturnMasterBE, Optional ByVal Voucher As VouchersMaster = Nothing) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Update(objModel, trans)
            '' TASK TFS4849 DONE ON 29-10-2018

            If Voucher IsNot Nothing Then
                If objModel.Post = True Then
                    If Voucher.VoucherDatail.Count > 0 Then
                        Call New VouchersDAL().Update(Voucher, trans)
                    End If
                Else
                    Call New VouchersDAL().Delete(Voucher, trans)
                End If
            End If

            '' END TASK 
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Update(ByVal objModel As ComplaintReturnMasterBE, trans As SqlTransaction) As Boolean
        Try
            post = objModel.Post
            ComplaintId = objModel.ComplaintId
            ComplaintReturnId = objModel.ComplaintReturnId
            Dim strSQL As String = String.Empty
            strSQL = "update ComplaintReturnMaster set ComplaintReturnNo= N'" & objModel.ComplaintReturnNo.Replace("'", "''") & "', ComplaintReturnDate= N'" & objModel.ComplaintReturnDate & "', ComplaintId= N'" & objModel.ComplaintId & "', CustomerId= N'" & objModel.CustomerId & "', ReceivedBy= N'" & objModel.ReceivedBy.Replace("'", "''") & "', ContactNo= N'" & objModel.ContactNo.Replace("'", "''") & "', Remarks= N'" & objModel.Remarks.Replace("'", "''") & "',Posted= N'" & objModel.Post & "' where ComplaintReturnId=" & objModel.ComplaintReturnId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Dim str As String = "SELECT isnull(sz1, 0) as Qty, ItemId , RequestDetailId from ComplaintReturnDetail where ComplaintReturnId=" & objModel.ComplaintReturnId
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)
            strSQL = "DELETE FROM ComplaintReturnDetail WHERE ComplaintReturnId=" & ComplaintReturnId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            If dt.Rows.Count > 0 Then
                For Each r As DataRow In dt.Rows
                    strSQL = "Update ComplaintRequestDetail set DeliverdQty = abs(Isnull(DeliverdQty,0) - " & r.Item(0) & ") where ItemId = " & r.Item(1) & " And ComplaintDetailId = " & r.Item(2) & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                Next
            End If
            stocktransId = objModel.StockTransId
            If post = True Then
                If stocktransId = 0 Then
                    strSQL = "Insert Into StockMasterTable (DocNo, DocDate, Remarks, Project,Account_Id) " _
                           & " Values(N'" & objModel.ComplaintReturnNo & "', N'" & objModel.ComplaintReturnDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'Complaint Return Stock Management', " & 0 & ", " & 0 & ") Select @@Identity "
                    stocktransId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
                Else
                    strSQL = "Update StockMasterTable Set DocNo=N'" & objModel.ComplaintReturnNo & "', DocDate=N'" & objModel.ComplaintReturnDate.ToString("yyyy-M-d h:mm:ss tt") & "', Remarks=N'Complaint Return Updation', Project=" & 0 & ",Account_Id=" & 0 & " WHERE StockTransId=" & stocktransId & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

                    strSQL = "DELETE FROM StockDetailTable WHERE StockTransId=" & stocktransId & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                End If
            Else
                strSQL = "Delete from StockDetailTable  where StockTransId= " & stocktransId
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                strSQL = "Delete from StockMasterTable  where StockTransId= " & stocktransId
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Delete(ByVal objModel As ComplaintReturnMasterBE, Optional ByVal Voucher As VouchersMaster = Nothing) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Delete(objModel, trans)
            '' TASK TFS4849 DONE ON 29-10-2018
            If Voucher IsNot Nothing AndAlso objModel.Post = True Then
                Call New VouchersDAL().Delete(Voucher, trans)
            End If
            '' END TASK 
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Delete(ByVal objModel As ComplaintReturnMasterBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from ComplaintReturnMaster  where ComplaintReturnId= " & objModel.ComplaintReturnId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Dim str1 As String = "SELECT isnull(sz1, 0) as Qty, ItemId , RequestDetailId from ComplaintReturnDetail where ComplaintReturnId=" & objModel.ComplaintReturnId
            Dim dt1 As DataTable = UtilityDAL.GetDataTable(str1)
            strSQL = "DELETE FROM ComplaintReturnDetail WHERE ComplaintReturnId=" & ComplaintReturnId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            If dt1.Rows.Count > 0 Then
                For Each r As DataRow In dt1.Rows
                    strSQL = "Update ComplaintRequestDetail set DeliverdQty = abs(Isnull(DeliverdQty,0) - " & r.Item(0) & ") where ItemId = " & r.Item(1) & " And ComplaintDetailId = " & r.Item(2) & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                Next
            End If
            strSQL = "Delete from ComplaintReturnDetail  where ComplaintReturnId= " & objModel.ComplaintReturnId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            ''
            strSQL = "UPDATE ComplaintRequestMaster set Status =Case When IsNull(Complaint.RemainingQty,0) > 0 then 'Open' ELSE 'Close' END From ComplaintRequestMaster, (Select ComplaintId, IsNull(SUM(IsNull(Sz1,0)-IsNull(DeliverdQty,0)),0) as RemainingQty From ComplaintRequestDetail WHERE ComplaintId=" & objModel.ComplaintId & " Group By ComplaintId) Complaint WHERE Complaint.ComplaintId = ComplaintRequestMaster.ComplaintId AND ComplaintRequestMaster.ComplaintId = " & objModel.ComplaintId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            ''
            Dim str As String
            str = "Select StockTransId from StockMasterTable where DocNo = '" & objModel.ComplaintReturnNo & "'"
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)
            Dim StockTransId As Integer
            If dt.Rows.Count > 0 Then
                StockTransId = dt.Rows(0).Item("StockTransId")
            End If
            strSQL = "Delete from StockDetailTable  where StockTransId= " & StockTransId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = "Delete from StockMasterTable  where StockTransId= " & StockTransId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " SELECT ComplaintReturnMaster.ComplaintReturnId, ComplaintReturnMaster.ComplaintReturnNo, ComplaintReturnMaster.ComplaintReturnDate, ComplaintReturnMaster.ComplaintId, ComplaintRequestMaster.ComplaintNo, ComplaintReturnMaster.CustomerId, vwCOADetail.detail_title as Customer, ComplaintReturnMaster.ReceivedBy, ComplaintReturnMaster.ContactNo, ComplaintReturnMaster.Remarks, ComplaintReturnMaster.Posted FROM ComplaintReturnMaster LEFT OUTER JOIN vwCOADetail ON ComplaintReturnMaster.CustomerId = vwCOADetail.coa_detail_id LEFT OUTER JOIN ComplaintRequestMaster ON ComplaintReturnMaster.ComplaintId = ComplaintRequestMaster.ComplaintId Order by 1 DESC "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ComplaintReturnId, ComplaintReturnNo, ComplaintReturnDate, ComplaintId, CustomerId, ReceivedBy, ContactNo, Remarks, Posted from ComplaintReturnMaster  where ComplaintReturnId=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Public Class ComplaintRequestDAL
    Dim ComplaintId As Integer
    Dim post As Boolean
    Dim stocktransId As Integer
    Function Add(ByVal objModel As ComplaintRequestBE) As Boolean
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
    Function Add(ByVal objModel As ComplaintRequestBE, trans As SqlTransaction) As Boolean
        Try

            Dim strSQL As String = String.Empty
            strSQL = "insert into  ComplaintRequestMaster (ComplaintNo, ComplaintDate, ComplaintReturnDate, CustomerId, PersonName, ContactNo, Remarks, Posted, Status) values (N'" & objModel.ComplaintNo.Replace("'", "''") & "', N'" & objModel.ComplaintDate & "', N'" & objModel.ComplaintReturnDate & "', N'" & objModel.CustomerId & "', N'" & objModel.PersonName.Replace("'", "''") & "', N'" & objModel.ContactNo.Replace("'", "''") & "', N'" & objModel.Remarks.Replace("'", "''") & "', '" & objModel.post & "', 'Open') Select @@Identity"
            ComplaintId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function AddComplaintDetail(ByVal list As List(Of ComplaintRequestDetailBE)) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            For Each obj As ComplaintRequestDetailBE In list
                str = "insert into  ComplaintRequestDetail (ComplaintId, LocationId, ItemId, Unit, Price, Sz1, Sz7, Qty, Comments) values (" & ComplaintId & ", " & obj.LocationId & ", " & obj.ItemId & ", N'" & obj.Unit.Replace("'", "''") & "', N'" & obj.Price & "', N'" & obj.Sz1 & "', N'" & obj.Sz7 & "', N'" & obj.Sz1 * obj.Sz7 & "', N'" & obj.Comments.Replace("'", "''") & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                If post = True Then
                    str = "Insert Into StockDetailTable(StockTransId, LocationId, ArticleDefId, InQty, OutQty, Rate, InAmount, OutAmount, Remarks, Pack_Qty, In_PackQty, Out_PackQty , BatchNo) " _
              & " Values (" & stocktransId & ", " & obj.LocationId & ",  " & obj.ItemId & ", " & obj.Sz1 * obj.Sz7 & ", " & 0 & ", " & obj.Price & ", " & obj.Sz7 * obj.Sz1 * obj.Price & ", " & 0 & ", N'" & obj.Comments.Replace("'", "''") & "', " & 1 & ", " & obj.Sz1 * obj.Sz7 & ", " & 0 & ", N'" & 0 & "')"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                End If
            Next
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function

    Function Update(ByVal objModel As ComplaintRequestBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Update(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Update(ByVal objModel As ComplaintRequestBE, trans As SqlTransaction) As Boolean
        Try
            post = objModel.post
            ComplaintId = objModel.ComplaintId
            Dim strSQL As String = String.Empty
            strSQL = "update ComplaintRequestMaster set ComplaintNo= N'" & objModel.ComplaintNo.Replace("'", "''") & "', ComplaintDate= N'" & objModel.ComplaintDate & "', ComplaintReturnDate= N'" & objModel.ComplaintReturnDate & "', CustomerId= N'" & objModel.CustomerId & "', PersonName= N'" & objModel.PersonName.Replace("'", "''") & "', ContactNo= N'" & objModel.ContactNo.Replace("'", "''") & "', Remarks= N'" & objModel.Remarks.Replace("'", "''") & "', Posted= '" & objModel.post & "' where ComplaintId=" & objModel.ComplaintId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = "DELETE FROM ComplaintRequestDetail WHERE ComplaintId=" & ComplaintId & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            stocktransId = objModel.StockTransId
            If post = True Then
                If stocktransId = 0 Then
                    strSQL = "Insert Into StockMasterTable (DocNo, DocDate, Remarks, Project,Account_Id) " _
                           & " Values(N'" & objModel.ComplaintNo & "', N'" & objModel.ComplaintDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'Complaint Request Stock Management', " & 0 & ", " & 0 & ") Select @@Identity "
                    stocktransId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
                Else
                    strSQL = "Update StockMasterTable Set DocNo=N'" & objModel.ComplaintNo & "', DocDate=N'" & objModel.ComplaintDate.ToString("yyyy-M-d h:mm:ss tt") & "', Remarks=N'Complaint Request Updation', Project=" & 0 & ",Account_Id=" & 0 & " WHERE StockTransId=" & stocktransId & ""
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

    Function Delete(ByVal objModel As ComplaintRequestBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Delete(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Delete(ByVal objModel As ComplaintRequestBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from ComplaintRequestMaster  where ComplaintId= " & objModel.ComplaintId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = "Delete from ComplaintRequestDetail  where ComplaintId= " & objModel.ComplaintId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Dim str As String
            str = "Select StockTransId from StockMasterTable where DocNo = '" & objModel.ComplaintNo & "'"
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
            strSQL = " SELECT ComplaintRequestMaster.ComplaintId, ComplaintRequestMaster.ComplaintNo, ComplaintRequestMaster.ComplaintDate, ComplaintRequestMaster.ComplaintReturnDate, ComplaintRequestMaster.CustomerId, vwCOADetail.detail_title as CustomerName, ComplaintRequestMaster.PersonName, ComplaintRequestMaster.ContactNo, ComplaintRequestMaster.Remarks, ComplaintRequestMaster.Posted  FROM ComplaintRequestMaster LEFT OUTER JOIN vwCOADetail ON ComplaintRequestMaster.CustomerId = vwCOADetail.coa_detail_id Order By 1 DESC "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ComplaintId, ComplaintNo, ComplaintDate, ComplaintReturnDate, CustomerId, PersonName, ContactNo, Remarks, Posted from ComplaintRequestMaster  where ComplaintId=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBDal
Imports SBModel
Imports SBUtility.Utility
Public Class WarrantyClaimDAL
    Dim WarrantyMasterId As Integer = 0
    Dim StockTransId As Integer = 0
    Function Add(ByVal objModel As WarrantyMasterTable) As Boolean
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
    Function Add(ByVal objModel As WarrantyMasterTable, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "insert into  WarrantyMasterTable (WarrantyNo, WarrantyDate, SOId, PlanId, TicketId, FinishGoodStandardId) values (N'" & objModel.WarrantyNo.Replace("'", "''") & "', N'" & objModel.WarrantyDate & "', " & objModel.SOId & ", " & objModel.PlanId & ", " & objModel.TicketId & ", N'" & objModel.FinishGoodStandardId & "') Select @@Identity"
            WarrantyMasterId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)

            strSQL = "Insert Into StockMasterTable (DocNo, DocDate, DocType, Remarks, Project,Account_Id) " _
            & " Values(N'" & objModel.WarrantyNo & "', N'" & objModel.WarrantyDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & 3 & ", N'Warranty Claim Stock Management', " & 0 & ", " & 0 & ") Select @@Identity "
            StockTransId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            'objModel.ActivityLog.ActivityName = "Save"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function AddWarrantyDetail(ByVal list As List(Of WarrantyDetailTable)) As Boolean
        Dim str As String = ""
        Dim conn As New SqlConnection(SQLHelper.CON_STR)
        Dim trans As SqlTransaction
        Try
            If conn.State = ConnectionState.Closed Then
                conn.Open()
            End If
            trans = conn.BeginTransaction
            For Each obj As WarrantyDetailTable In list
                str = "INSERT into WarrantyDetailTable(WarrantyMasterId, FinishGoodId, Qty, Amount, Remarks) Values(" & WarrantyMasterId & ", " & obj.FinishGoodId & ", " & obj.Qty & ", " & obj.Amount & ", '" & obj.Remarks & "') "
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

                str = "Insert Into StockDetailTable(StockTransId, LocationId, ArticleDefId, InQty, OutQty, Rate, InAmount, OutAmount, Remarks, Pack_Qty, In_PackQty, Out_PackQty , BatchNo) " _
              & " Values (" & StockTransId & ", " & 1 & ",  " & obj.FinishGoodId & ", " & 0 & ", " & obj.Qty & ", " & obj.Amount & ", " & 0 & ", " & obj.Qty * obj.Amount & ", N'" & obj.Remarks.Replace("'", "''") & "', " & 1 & ", " & 0 & ", " & obj.Qty & ", N'" & 0 & "')"
                'End Task:M16
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

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

    Function Update(ByVal objModel As WarrantyMasterTable) As Boolean
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
    Function Update(ByVal objModel As WarrantyMasterTable, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            WarrantyMasterId = objModel.WarrantyMasterId
            StockTransId = objModel.StockTransId
            strSQL = "update WarrantyMasterTable set WarrantyNo= N'" & objModel.WarrantyNo.Replace("'", "''") & "', WarrantyDate= N'" & objModel.WarrantyDate & "', SOId= N'" & objModel.SOId & "', PlanId= N'" & objModel.PlanId & "', TicketId= N'" & objModel.TicketId & "', FinishGoodStandardId= N'" & objModel.FinishGoodStandardId & "' where WarrantyMasterId=" & objModel.WarrantyMasterId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            strSQL = "Update StockMasterTable Set DocNo=N'" & objModel.WarrantyNo & "', DocDate=N'" & objModel.WarrantyDate.ToString("yyyy-M-d h:mm:ss tt") & "', DocType= " & 3 & ", Remarks=N'Warranty Claim Stock Updation', Project=" & 0 & ",Account_Id=" & 0 & " WHERE StockTransId=" & StockTransId & ""

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            strSQL = "DELETE FROM WarrantyDetailTable WHERE WarrantyMasterId=" & WarrantyMasterId & ""

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            strSQL = "DELETE FROM StockDetailTable WHERE StockTransId=" & StockTransId & ""

            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'objModel.ActivityLog.ActivityName = "Update"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Public Function UpdateWarrantyDetail(ByVal list As List(Of WarrantyDetailTable)) As Boolean
    '    Dim str As String = ""
    '    Dim conn As New SqlConnection(SQLHelper.CON_STR)
    '    Dim trans As SqlTransaction
    '    Try
    '        If conn.State = ConnectionState.Closed Then
    '            conn.Open()
    '        End If
    '        trans = conn.BeginTransaction
    '        For Each obj As WarrantyDetailTable In list
    '            str = "If Exists(Select ISNULL(WarrantyDetailId, 0) as WarrantyDetailId From WarrantyDetailTable Where WarrantyDetailId=" & obj.WarrantyDetailId & ") Update WarrantyDetailTable Set WarrantyMasterId =" & WarrantyMasterId & ", FinishGoodId=" & obj.FinishGoodId & ",Qty =" & obj.Qty & ",Amount =" & obj.Amount & ", Remarks=N'" & obj.Remarks & "' WHERE WarrantyDetailId = " & obj.WarrantyDetailId & "" _
    '             & " Else INSERT into WarrantyDetailTable(WarrantyMasterId, FinishGoodId, Qty, Amount, Remarks) Values(" & WarrantyMasterId & ", " & obj.FinishGoodId & ", " & obj.Qty & ", " & obj.Amount & ", '" & obj.Remarks & "')"
    '            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)

    '            str = "Insert Into StockDetailTable(StockTransId, LocationId, ArticleDefId, InQty, OutQty, Rate, InAmount, OutAmount, Remarks, Pack_Qty, In_PackQty, Out_PackQty , BatchNo) " _
    '          & " Values (" & StockTransId & ", " & 1 & ",  " & obj.FinishGoodId & ", " & 0 & ", " & obj.Qty & ", " & obj.Amount & ", " & 0 & ", " & obj.Qty * obj.Amount & ", N'" & obj.Remarks.Replace("'", "''") & "', " & 1 & ", " & 0 & ", " & obj.Qty & ", N'" & 0 & "')"
    '            'End Task:M16
    '            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
    '        Next
    '        trans.Commit()
    '        Return True
    '    Catch ex As Exception
    '        trans.Rollback()
    '    End Try
    'End Function



    Function Delete(ByVal objModel As WarrantyMasterTable) As Boolean
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
    Function Delete(ByVal objModel As WarrantyMasterTable, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from WarrantyMasterTable  where WarrantyMasterId= " & objModel.WarrantyMasterId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Dim str As String
            str = "Select StockTransId from StockMasterTable where DocNo = '" & objModel.WarrantyNo & "'"
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)
            Dim StockTransId As Integer
            If dt.Rows.Count > 0 Then
                StockTransId = dt.Rows(0).Item("StockTransId")
            End If
            strSQL = "Delete from StockDetailTable  where StockTransId= " & StockTransId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = "Delete from StockMasterTable  where StockTransId= " & StockTransId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Dim str1 As String
            str1 = "Select DispatchId from DispatchMasterTable where PoNo = '" & objModel.WarrantyNo & "'"
            Dim dt1 As DataTable = UtilityDAL.GetDataTable(str1)
            Dim DispatchId As Integer
            If dt1.Rows.Count > 0 Then
                DispatchId = dt1.Rows(0).Item("DispatchId")
            End If
            strSQL = "Delete from DispatchDetailTable  where DispatchId= " & DispatchId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = "Delete from DispatchMasterTable  where DispatchId= " & DispatchId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Dim str2 As String
            str2 = "Select voucher_id from tblVoucher where voucher_code = '" & objModel.WarrantyNo & "'"
            Dim dt2 As DataTable = UtilityDAL.GetDataTable(str2)
            Dim VId As Integer
            If dt2.Rows.Count > 0 Then
                VId = dt2.Rows(0).Item("voucher_id")
            End If
            strSQL = "Delete from tblVoucherDetail  where voucher_id= " & VId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = "Delete from tblVoucher where voucher_id= " & VId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'objModel.ActivityLog.ActivityName = "Delete"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " SELECT WarrantyMasterTable.WarrantyMasterId, WarrantyMasterTable.WarrantyNo, WarrantyMasterTable.WarrantyDate, WarrantyMasterTable.SOId, WarrantyMasterTable.PlanId, WarrantyMasterTable.TicketId,  WarrantyMasterTable.FinishGoodStandardId, SalesOrderMasterTable.SalesOrderNo, PlanMasterTable.PlanNo, PlanTicketsMaster.TicketNo, FinishGoodMaster.StandardNo + ' ~ ' + FinishGoodMaster.StandardName as [Manufactured Item] FROM WarrantyMasterTable LEFT OUTER JOIN PlanMasterTable ON WarrantyMasterTable.PlanId = PlanMasterTable.PlanId LEFT OUTER JOIN FinishGoodMaster ON WarrantyMasterTable.FinishGoodStandardId = FinishGoodMaster.Id LEFT OUTER JOIN SalesOrderMasterTable ON WarrantyMasterTable.SOId = SalesOrderMasterTable.SalesOrderId LEFT OUTER JOIN PlanTicketsMaster ON WarrantyMasterTable.TicketId = PlanTicketsMaster.PlanTicketsMasterID Order BY 1 DESC "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select WarrantyMasterId, WarrantyNo, WarrantyDate, SOId, PlanId, TicketId, FinishGoodStandardId from WarrantyMasterTable  where WarrantyMasterId=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

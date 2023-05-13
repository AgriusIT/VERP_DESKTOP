
Imports SBModel
Imports System.Data.SqlClient

Public Class PlanMasterTableDAL
    Function Add(ByVal objModel As PlanMasterTableBE) As Boolean
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
    Function Add(ByVal objModel As PlanMasterTableBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "insert into  PlanMasterTable (LocationId, PlanNo, PlanDate, CustomerId, PartyInvoiceNo, PartySlipNo, PlanQty, PlanAmount, Remarks, UserName, Status, PoId, EmployeeCode, ProductionStatus, CompletionDate, StartDate) values (N'" & objModel.LocationId & "', N'" & objModel.PlanNo.Replace("'", "''") & "', N'" & objModel.PlanDate & "', N'" & objModel.CustomerId & "', N'" & objModel.PartyInvoiceNo.Replace("'", "''") & "', N'" & objModel.PartySlipNo.Replace("'", "''") & "', N'" & objModel.PlanQty & "', N'" & objModel.PlanAmount & "', N'" & objModel.Remarks.Replace("'", "''") & "', N'" & objModel.UserName.Replace("'", "''") & "', N'" & objModel.Status.Replace("'", "''") & "', N'" & objModel.PoId & "', N'" & objModel.EmployeeCode & "', N'" & objModel.ProductionStatus.Replace("'", "''") & "', N'" & objModel.CompletionDate & "', N'" & objModel.StartDate & "') Select @@Identity"
            objModel.PlanId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.ActivityName = "Save"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Insert(ByVal objModel As PlanMasterTableBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Insert(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function Insert(ByVal objModel As PlanMasterTableBE, trans As SqlTransaction) As Boolean
        'Plan.PlanNo = GetNextPlanNo()
        'Plan.PlanDate = Now
        'Plan.StartDate = Me.dtpStartDate.Value
        'Plan.CustomerId = Val(CType(Me.cmbSalesOrder.SelectedItem, DataRowView).Item("CustomerId").ToString)
        'Plan.LocationId = Val(CType(Me.cmbSalesOrder.SelectedItem, DataRowView).Item("LocationId").ToString)
        'Plan.PoId = Me.cmbSalesOrder.SelectedValue
        'Plan.UserName = LoginUserName
        'Plan.Remarks = String.Empty
        Try
            Dim strSQL As String = String.Empty
            strSQL = "insert into  PlanMasterTable (LocationId, PlanNo, PlanDate, CustomerId, Remarks, UserName, PoId, StartDate) values (N'" & objModel.LocationId & "', N'" & objModel.PlanNo.Replace("'", "''") & "', N'" & objModel.PlanDate & "', N'" & objModel.CustomerId & "', N'" & objModel.Remarks.Replace("'", "''") & "', N'" & objModel.UserName.Replace("'", "''") & "', N'" & objModel.PoId & "', N'" & objModel.StartDate & "') Select @@Identity"
            objModel.PlanId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            'objModel.ActivityLog.ActivityName = "Save"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Update(ByVal objModel As PlanMasterTableBE) As Boolean
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
    Function Update(ByVal objModel As PlanMasterTableBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update PlanMasterTable set LocationId= N'" & objModel.LocationId & "', PlanNo= N'" & objModel.PlanNo.Replace("'", "''") & "', PlanDate= N'" & objModel.PlanDate & "', CustomerId= N'" & objModel.CustomerId & "', PartyInvoiceNo= N'" & objModel.PartyInvoiceNo.Replace("'", "''") & "', PartySlipNo= N'" & objModel.PartySlipNo.Replace("'", "''") & "', PlanQty= N'" & objModel.PlanQty & "', PlanAmount= N'" & objModel.PlanAmount & "', Remarks= N'" & objModel.Remarks.Replace("'", "''") & "', UserName= N'" & objModel.UserName.Replace("'", "''") & "', Status= N'" & objModel.Status.Replace("'", "''") & "', PoId= N'" & objModel.PoId & "', EmployeeCode= N'" & objModel.EmployeeCode & "', ProductionStatus= N'" & objModel.ProductionStatus.Replace("'", "''") & "', CompletionDate= N'" & objModel.CompletionDate & "', StartDate= N'" & objModel.StartDate & "' where PlanId=" & objModel.PlanId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.ActivityName = "Update"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Delete(ByVal objModel As PlanMasterTableBE) As Boolean
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
    Function Delete(ByVal objModel As PlanMasterTableBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from PlanMasterTable  where PlanId= " & objModel.PlanId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.ActivityName = "Delete"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select PlanId, LocationId, PlanNo, PlanDate, CustomerId, PartyInvoiceNo, PartySlipNo, PlanQty, PlanAmount, Remarks, UserName, Status, PoId, EmployeeCode, ProductionStatus, CompletionDate, StartDate from PlanMasterTable  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select PlanId, LocationId, PlanNo, PlanDate, CustomerId, PartyInvoiceNo, PartySlipNo, PlanQty, PlanAmount, Remarks, UserName, Status, PoId, EmployeeCode, ProductionStatus, CompletionDate, StartDate from PlanMasterTable  where PlanId=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class

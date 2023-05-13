Imports SBModel
Imports System.Data.SqlClient

Public Class OpportunitySupportDetailDAL
    Function Add(ByVal objModel As OpportunityBE) As Boolean
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
    Function Add(ByVal Obj As OpportunityBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            For Each objModel As OpportunitySupportDetailBE In Obj.SupportDetail
                If objModel.OpportunitySupportDetailId < 1 Then
                    strSQL = "insert into  tblDefOpportunitySupportDetail (OpportunityId, Brand, ModelNo, SerialNo, SLACoverage, Address, City, Province, Country, StartDate, EndDate, Type, UnitPrice, FilePath, SLAInterventionTime, SLAFixTime) " _
                        & " values (" & Obj.OpportunityId & ", N'" & objModel.Brand.Replace("'", "''") & "', N'" & objModel.ModelNo.Replace("'", "''") & "', N'" & objModel.SerialNo.Replace("'", "''") & "', N'" & objModel.SLACoverage.Replace("'", "''") & "', N'" & objModel.Address.Replace("'", "''") & "', N'" & objModel.City.Replace("'", "''") & "', N'" & objModel.Province.Replace("'", "''") & "', N'" & objModel.Country.Replace("'", "''") & "', N'" & objModel.StartDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', N'" & objModel.EndDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', N'" & objModel.Type.Replace("'", "''") & "', " & objModel.UnitPrice & ", N'" & objModel.FilePath.Replace("'", "''") & "', N'" & objModel.SLAInterventionTime.Replace("'", "''") & "', N'" & objModel.SLAFixTime.Replace("'", "''") & "') Select @@Identity"
                    objModel.OpportunitySupportDetailId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
                Else
                    strSQL = " UPDATE tblDefOpportunitySupportDetail SET OpportunityId = " & Obj.OpportunityId & ", Brand = N'" & objModel.Brand.Replace("'", "''") & "', ModelNo = N'" & objModel.ModelNo.Replace("'", "''") & "', " _
                        & " SerialNo = N'" & objModel.SerialNo.Replace("'", "''") & "', SLACoverage = N'" & objModel.SLACoverage.Replace("'", "''") & "', " _
                        & " Address = N'" & objModel.Address.Replace("'", "''") & "', City=N'" & objModel.City.Replace("'", "''") & "', Province = N'" & objModel.Province.Replace("'", "''") & "', Country = N'" & objModel.Country.Replace("'", "''") & "', " _
                        & " StartDate = N'" & objModel.StartDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', EndDate=N'" & objModel.EndDate.ToString("yyyy-M-dd hh:mm:ss tt") & "', Type = N'" & objModel.Type.Replace("'", "''") & "', UnitPrice = " & objModel.UnitPrice & ", FilePath = N'" & objModel.FilePath.Replace("'", "''") & "', SLAInterventionTime = N'" & objModel.SLAInterventionTime.Replace("'", "''") & "', SLAFixTime = N'" & objModel.SLAFixTime.Replace("'", "''") & "' WHERE OpportunitySupportDetailId = " & objModel.OpportunitySupportDetailId & ""
                    objModel.OpportunitySupportDetailId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)

                End If
            Next
            'objModel.ActivityLog.ActivityName = "Save"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Update(ByVal objModel As OpportunitySupportDetailBE) As Boolean
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
    Function Update(ByVal objModel As OpportunitySupportDetailBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update tblDefOpportunitySupportDetail set OpportunityId= N'" & objModel.OpportunityId & "', Brand= N'" & objModel.Brand.Replace("'", "''") & "', ModelNo= N'" & objModel.ModelNo.Replace("'", "''") & "', SerialNo= N'" & objModel.SerialNo.Replace("'", "''") & "', SLACoverage= N'" & objModel.SLACoverage.Replace("'", "''") & "', Address= N'" & objModel.Address.Replace("'", "''") & "', City= N'" & objModel.City.Replace("'", "''") & "', Province= N'" & objModel.Province.Replace("'", "''") & "', Country= N'" & objModel.Country.Replace("'", "''") & "', StartDate= N'" & objModel.StartDate & "', EndDate= N'" & objModel.EndDate & "', Type= N'" & objModel.Type.Replace("'", "''") & "', UnitPrice= N'" & objModel.UnitPrice & "', SLA = N'" & objModel.SLA.Replace("'", "''") & "', SLAInterventionTime = N'" & objModel.SLAInterventionTime.Replace("'", "''") & "', SLAFixTime = N'" & objModel.SLAFixTime.Replace("'", "''") & "' where OpportunitySupportDetailId=" & objModel.OpportunitySupportDetailId
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

    Function Delete(ByVal OpportunitySupportDetailId As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Delete(OpportunitySupportDetailId, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Delete(ByVal OpportunitySupportDetailId As Integer, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from tblDefOpportunitySupportDetail  WHERE OpportunitySupportDetailId= " & OpportunitySupportDetailId
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
            strSQL = " Select OpportunitySupportDetailId, OpportunityId, Brand, ModelNo, SerialNo, SLACoverage, SLAInterventionTime, SLAFixTime, Address, City, Province, Country, StartDate, EndDate, Type, UnitPrice, FilePath from tblDefOpportunitySupportDetail  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Rafay:Task Start
    Public Function IsSerialNoExisted(ByVal SerialNo As String) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "SELECT Count(*) as Count1 FROM tblDefOpportunitySupportDetail WHERE SerialNo = '" & SerialNo & "'"
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    If dt.Rows(0).Item(0) > 0 Then
                        Return True
                    Else
                        Return False
                    End If
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'rafay

    Function GetDetail(ByVal OpportunityId As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            'rafay
            strSQL = " Select OpportunitySupportDetailId, OpportunityId, Brand, ModelNo, SerialNo, SLACoverage, SLAInterventionTime, SLAFixTime, Address, City, Province, Country, StartDate, EndDate, Type, UnitPrice, FilePath from tblDefOpportunitySupportDetail  where OpportunityId=" & OpportunityId
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

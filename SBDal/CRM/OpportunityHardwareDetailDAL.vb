Imports SBModel
Imports System.Data.SqlClient
Public Class OpportunityHardwareDetailDAL
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
            For Each objModel As OpportunityHardwareDetailBE In Obj.HardwareDetail
                If objModel.OpportunityHardwareDetailId < 1 Then
                    strSQL = "INSERT INTO  tblDefOpportunityHardwareDetail (OpportunityId, PartNo, Type, BrandNo, Description, Status, Warranty,LeadTime, Qty, Price, Discount, TotalAmount, FilePath) " _
                        & " VALUES (" & Obj.OpportunityId & ", N'" & objModel.PartNo.Replace("'", "''") & "', N'" & objModel.Type.Replace("'", "''") & "', N'" & objModel.BrandNo.Replace("'", "''") & "', " _
                        & " N'" & objModel.Description.Replace("'", "''") & "', N'" & objModel.Status.Replace("'", "''") & "', N'" & objModel.Warranty.Replace("'", "''") & "', N'" & objModel.LeadTime.Replace("'", "''") & "', " _
                        & " " & objModel.Qty & ", " & objModel.Price & ", " & objModel.Discount & ", " & objModel.TotalAmount & ", N'" & objModel.FilePath.Replace("'", "''") & "') Select @@Identity"
                    objModel.OpportunityHardwareDetailId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
                Else
                    strSQL = "UPDATE tblDefOpportunityHardwareDetail SET OpportunityId = " & objModel.OpportunityId & ", PartNo = N'" & objModel.PartNo.Replace("'", "''") & "', Type=N'" & objModel.Type.Replace("'", "''") & "', BrandNo =N'" & objModel.BrandNo.Replace("'", "''") & "', Description =N'" & objModel.Description.Replace("'", "''") & "', " _
                        & " Status = N'" & objModel.Status.Replace("'", "''") & "', Warranty=N'" & objModel.Warranty.Replace("'", "''") & "', LeadTime=N'" & objModel.LeadTime.Replace("'", "''") & "', Qty=" & objModel.Qty & ", Price=" & objModel.Price & ", Discount = " & objModel.Discount & ", TotalAmount = " & objModel.TotalAmount & ", FilePath = N'" & objModel.FilePath.Replace("'", "''") & "' WHERE OpportunityHardwareDetailId = " & objModel.OpportunityHardwareDetailId & ""
                    objModel.OpportunityHardwareDetailId = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
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

    Function Update(ByVal objModel As OpportunityHardwareDetailBE) As Boolean
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
    Function Update(ByVal objModel As OpportunityHardwareDetailBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update tblDefOpportunityHardwareDetail set OpportunityId= N'" & objModel.OpportunityId & "', PartNo= N'" & objModel.PartNo.Replace("'", "''") & "', Type= N'" & objModel.Type.Replace("'", "''") & "', BrandNo= N'" & objModel.BrandNo.Replace("'", "''") & "', Description= N'" & objModel.Description.Replace("'", "''") & "', Status= N'" & objModel.Status.Replace("'", "''") & "', Warranty= N'" & objModel.Warranty.Replace("'", "''") & "', Qty= N'" & objModel.Qty.Replace("'", "''") & "', Price= N'" & objModel.Price & "', Discount= N'" & objModel.Discount & "', TotalAmount= N'" & objModel.TotalAmount & "' where OpportunityHardwareDetailId=" & objModel.OpportunityHardwareDetailId
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

    Function DeleteSingle(ByVal OpportunityHardwareDetailId As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            DeleteSingle(OpportunityHardwareDetailId, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function DeleteSingle(ByVal OpportunityHardwareDetailId As Integer, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete FROM tblDefOpportunityHardwareDetail  where OpportunityHardwareDetailId= " & OpportunityHardwareDetailId
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
            strSQL = " Select OpportunityHardwareDetailId, OpportunityId, PartNo, Type, BrandNo, Description, Status, Warranty,LeadTime, Qty, Price, Discount, TotalAmount, FilePath from tblDefOpportunityHardwareDetail  "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetDetail(ByVal OpportunityId As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select OpportunityHardwareDetailId, OpportunityId, PartNo, Type, BrandNo, Description, Status, Warranty,LeadTime, Qty, Price, Discount, TotalAmount, FilePath from tblDefOpportunityHardwareDetail  WHERE OpportunityId=" & OpportunityId
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

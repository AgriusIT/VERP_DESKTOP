Imports SBDal
Imports SBModel
Imports System.Data.SqlClient

Public Class ReturnFromFactoryMasterDAL
    Function Add(ByVal objModel As ReturnFromFactoryMasterBE) As Boolean
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
    Function Add(ByVal objModel As ReturnFromFactoryMasterBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "insert into  ReturnFromFactoryMaster(ReturnNo, ReturnDate, VendorId, DriverName, VehicleNo, Remarks) Values (N'" & objModel.ReturnNo.Replace("'", "''") & "', N'" & objModel.ReturnDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & objModel.PartyId & ", N'" & objModel.DriverName.Replace("'", "''") & "', N'" & objModel.VehicleNo.Replace("'", "''") & "', N'" & objModel.Remarks.Replace("'", "''") & "') Select @@Identity"
            objModel.ID = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            Call New ReturnFromFactoryDetailDAL().AddDetail(objModel, trans)
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Update(ByVal objModel As ReturnFromFactoryMasterBE) As Boolean
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
    Function Update(ByVal objModel As ReturnFromFactoryMasterBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "UPDATE ReturnFromFactoryMaster SET ReturnNo= N'" & objModel.ReturnNo.Replace("'", "''") & "', ReturnDate= N'" & objModel.ReturnDate & "', VendorId= " & objModel.PartyId & ", DriverName= N'" & objModel.DriverName.Replace("'", "''") & "', VehicleNo= N'" & objModel.VehicleNo.Replace("'", "''") & "', Remarks= N'" & objModel.Remarks.Replace("''", "'") & "' WHERE ID=" & objModel.ID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Call New ReturnFromFactoryDetailDAL().AddDetail(objModel, trans)
            'Call New StockDAL().Update(objModel.Stock, trans)
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Post(ByVal objModel As ReturnFromFactoryMasterBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Post(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Post(ByVal objModel As ReturnFromFactoryMasterBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "UPDATE ReturnFromFactoryMaster SET IsPosted = 1 WHERE ID = " & objModel.ID & ""
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'Call New ReturnToFactoryDetailDAL().AddDetail(objModel.Detail, trans)
            Update(objModel, trans)
            Call New StockDAL().Add(objModel.Stock, trans)
            objModel.ActivityLog.ActivityName = "Post"
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function UnPost(ByVal objModel As ReturnFromFactoryMasterBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            UnPost(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function UnPost(ByVal objModel As ReturnFromFactoryMasterBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "UPDATE ReturnFromFactoryMaster SET IsPosted = 0 WHERE ID = " & objModel.ID & ""
            objModel.ID = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            'Call New ReturnToFactoryDetailDAL().AddDetail(objModel.Detail, trans)

            Call New StockDAL().Delete(objModel.Stock, trans)
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Function Post(ByVal objModel As ReturnToFactoryMasterBE) As Boolean
    '    Try
    '        'Dim strSQL As String = String.Empty
    '        'strSQL = "insert into  ReturnToFactoryMaster (ReturnNo, ReturnDate, PartyId, DriverName, VehicleNo, Remarks) values (N'" & objModel.ReturnNo.Replace("'", "''") & "', N'" & objModel.ReturnDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & objModel.PartyId & ", N'" & objModel.DriverName.Replace("'", "''") & "', N'" & objModel.VehicleNo.Replace("'", "''") & "', N'" & objModel.Remarks.Replace("'", "''") & "') Select @@Identity"
    '        'objModel.ID = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
    '        'Call New ReturnToFactoryDetailDAL().AddDetail(objModel.Detail, trans)
    '        Call New StockDAL().Add(objModel.Stock)
    '        Return True
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    'Function UnpostPost(ByVal objModel As ReturnToFactoryMasterBE) As Boolean
    '    Try
    '        'Dim strSQL As String = String.Empty
    '        'strSQL = "insert into  ReturnToFactoryMaster (ReturnNo, ReturnDate, PartyId, DriverName, VehicleNo, Remarks) values (N'" & objModel.ReturnNo.Replace("'", "''") & "', N'" & objModel.ReturnDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & objModel.PartyId & ", N'" & objModel.DriverName.Replace("'", "''") & "', N'" & objModel.VehicleNo.Replace("'", "''") & "', N'" & objModel.Remarks.Replace("'", "''") & "') Select @@Identity"
    '        'objModel.ID = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
    '        'Call New ReturnToFactoryDetailDAL().AddDetail(objModel.Detail, trans)
    '        Call New StockDAL().Add(objModel.Stock)
    '        Return True
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    Function Delete(ByVal objModel As ReturnFromFactoryMasterBE) As Boolean
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
    Function Delete(ByVal objModel As ReturnFromFactoryMasterBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from ReturnFromFactoryDetail WHERE ReturnFromFactoryId = " & objModel.ID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            strSQL = "Delete from ReturnFromFactoryMaster  WHERE ID = " & objModel.ID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            If objModel.IsPosted = True Then
                strSQL = "Delete from StockDetailTable WHERE StockTransId IN (SELECT StockTransId FROM StockMasterTable WHERE DocNo = '" & objModel.ReturnNo & "')"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                strSQL = "Delete from StockMasterTable WHERE DocNo = '" & objModel.ReturnNo & "'"
                SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            End If
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ReturnFromFactoryMaster.ID, ReturnFromFactoryMaster.ReturnNo, ReturnFromFactoryMaster.ReturnDate, IsNull(ReturnFromFactoryMaster.VendorId, 0) AS VendorId, Vendor.detail_title AS Vendor, ReturnFromFactoryMaster.DriverName, ReturnFromFactoryMaster.VehicleNo, ReturnFromFactoryMaster.Remarks, ISNULL(ReturnFromFactoryMaster.IsPosted, 0) AS IsPosted from ReturnFromFactoryMaster LEFT OUTER JOIN vwCOADetail AS Vendor ON ReturnFromFactoryMaster.VendorId = Vendor.coa_detail_id  Order By ReturnFromFactoryMaster.ID DESC "
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ID, ReturnNo, ReturnDate, VendorId, DriverName, VehicleNo, Remarks from ReturnFromFactoryMaster  where ID=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class


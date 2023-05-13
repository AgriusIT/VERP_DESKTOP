Imports SBDal
Imports SBModel
Imports system.Data.SqlClient
Imports SBUtility.Utility


Public Class Vehicle_LogDAL
    Dim EntryNo As String = String.Empty
    Public Function Save(ByVal Vehicle As Vehicle_Log) As Boolean
        Dim conn As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction
        Try
            EntryNo = GetDocumentNo(trans)
            Dim strquery As String = String.Empty
            strquery = "insert into tblVehicleLog(LogDate,Vehicle_Id,Person,Purpose,Previouse,[Current],Fuel, EntryNo, EntryDateTime, UserName) values (N'" & Vehicle.LogDate.ToString("yyyy-M-d h:mm:ss tt") & "',N'" & Vehicle.Vehicle_Id & "',N'" & Vehicle.Person & "',N'" & Vehicle.Purpose & "',N'" & Vehicle.Previous & "',N'" & Vehicle.Current & "',N'" & Vehicle.Fuel & "', N'" & EntryNo & "', N'" & Vehicle.EntryDateTime.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & Vehicle.UserName & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strquery)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function

    Public Function Update(ByVal Vehicle As Vehicle_Log) As Boolean
        Dim conn As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction
        Try
            Dim strquery As String = String.Empty
            strquery = "update tblVehicleLog set LogDate = N'" & Vehicle.LogDate.ToString("yyyy-M-d h:mm:ss tt") & "'," _
            & " Vehicle_Id = N'" & Vehicle.Vehicle_Id & "', " _
            & " Person = N'" & Vehicle.Person & "'," _
            & " Purpose = N'" & Vehicle.Purpose & "'," _
            & " Previouse = N'" & Vehicle.Previous & "'," _
            & " [Current] = N'" & Vehicle.Current & "'," _
            & " Fuel = N'" & Vehicle.Fuel & "'," _
            & " UserName = N'" & Vehicle.UserName & "'," _
            & " EntryDatetime = N'" & Vehicle.EntryDateTime.ToString("yyyy-M-d h:mm:ss tt") & "', EntryNo=N'" & Vehicle.EntryNo & "' where LogId = " & Vehicle.LogId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strquery)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function

    Public Function Delete(ByVal Vehicle As Vehicle_Log) As Boolean
        Dim conn As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction
        Try
            Dim strquery As String = String.Empty
            strquery = "Delete From tblVehicleLog where LogId = " & Vehicle.LogId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strquery)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            conn.Close()
        End Try
    End Function
    Public Function GetAllRecords() As DataTable
        Try
            Dim strsql As String = String.Empty
            strsql = "SELECT   dbo.tblVehicleLog.LogId, tblVehicleLog.EntryNo, dbo.tblVehicleLog.LogDate, dbo.tblVehicleLog.Vehicle_Id, dbo.tblDefVehicle.Vehicle_Name, dbo.tblVehicleLog.Person, " _
            & " dbo.tblVehicleLog.Purpose, dbo.tblVehicleLog.Previouse, dbo.tblVehicleLog.[Current], (ISNULL(tblVehicleLog.Previouse,0)-ISNULL(tblVehicleLog.[Current],0)) as Usage, dbo.tblVehicleLog.Fuel" _
            & " FROM dbo.tblDefVehicle INNER JOIN" _
            & " dbo.tblVehicleLog ON dbo.tblDefVehicle.Vehicle_ID = dbo.tblVehicleLog.Vehicle_Id"
            Return UtilityDAL.GetDataTable(strsql)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function strvehicle() As String
        Try
            Return "select Vehicle_Id , Vehicle_Name from tblDefVehicle"
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function GetDocumentNo(Optional ByVal trans As SqlTransaction = Nothing) As String
        Try
            Return UtilityDAL.GetSerialNo("VL-" & Right(Date.Now.Year, 2) & "-", "tblVehicleLog", "EntryNo", trans)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

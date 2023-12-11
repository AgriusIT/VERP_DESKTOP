Imports SBDal
Imports SBModel
Imports system.Data.SqlClient
Imports SBUtility.Utility


Public Class VehicleDAL
    Public Function Save(ByVal Vehicle As Vehicle) As Boolean
        Dim conn As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "insert into tblDefVehicle(Vehicle_Name,Vehicle_Type,Engine_No,Chassis_No,Registartion_No,Maker,Model,Color,Power) values(N'" & Vehicle.Vehicle_Name & "',N'" & Vehicle.Vehicle_Type & "',N'" & Vehicle.Engine_No & "',N'" & Vehicle.Chassis_No & "',N'" & Vehicle.Registration_No & "',N'" & Vehicle.Maker & "',N'" & Vehicle.Model & "',N'" & Vehicle.Color & "',N'" & Vehicle.Power & "')"
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

    Public Function Update(ByVal Vehicle As Vehicle) As Boolean
        Dim conn As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction
        Try
            Dim strquery As String = String.Empty
            strquery = "update tblDefVehicle set Vehicle_Name = N'" & Vehicle.Vehicle_Name & "'," _
            & " Vehicle_Type = N'" & Vehicle.Vehicle_Type & "', " _
            & " Engine_No = N'" & Vehicle.Engine_No & "'," _
            & " Chassis_No = N'" & Vehicle.Chassis_No & "'," _
            & " Registartion_No = N'" & Vehicle.Registration_No & "'," _
            & " Maker = N'" & Vehicle.Maker & "'," _
            & " Model = N'" & Vehicle.Model & "'," _
            & " Color = N'" & Vehicle.Color & "'," _
            & " Power = N'" & Vehicle.Power & "' where Vehicle_ID = " & Vehicle.Vehicle_ID
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

    Public Function Delete(ByVal Vehicle As Vehicle) As Boolean
        Dim conn As New SqlClient.SqlConnection(SQLHelper.CON_STR)
        If conn.State = ConnectionState.Closed Then conn.Open()
        Dim trans As SqlTransaction = conn.BeginTransaction
        Try
            Dim strquery As String = String.Empty
            strquery = "Delete From tblDefVehicle where Vehicle_ID = " & Vehicle.Vehicle_ID
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
            strsql = "Select * from tblDefVehicle"
            Return UtilityDAL.GetDataTable(strsql)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

Imports SBUtility.Utility
Imports SBModel
Imports System
Imports System.Data
Imports System.Data.SqlClient
Public Class ConfigSystemDAL
    Public Enum ConfigSysEnm
        config_id
        config_Type
        config_Value
        Comments
        IsActive
    End Enum
    Public Function SaveConfigSys(ByVal ConfigList As List(Of ConfigSystem)) As Boolean
        Try

            Dim str As String = String.Empty
            Dim Con As SqlConnection
            Con = New SqlConnection(SQLHelper.CON_STR)
            If Con.State = ConnectionState.Closed Then Con.Open()
            Dim trans As SqlTransaction = Con.BeginTransaction
            Dim ConfigSysList As List(Of ConfigSystem) = ConfigList
            For Each Config As ConfigSystem In ConfigSysList
                Dim ConfigKeys As String = UtilityDAL.GetConfigValue(Config.Config_Type, trans).ToString
                If ConfigKeys.Length > 0 Then
                    str = "Update ConfigValuesTable set Config_Value=N'" & Config.Config_Value & "' WHERE Config_Type=N'" & Config.Config_Type & "' "
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                    'Else
                    '    str = "Insert Into ConfigValuesTable(Config_Id, Config_Type, Config_Value, Comments) Select Max(ISNULL(Config_Id,0))+1, N'" & Config.Config_Type & "', N'" & Config.Config_Value & "', Null From ConfigValuesTable"
                    '    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
                End If
            Next
            trans.Commit()
            Con.Close()
            Return True
        Catch ex As Exception
        End Try
    End Function
    Public Function GetConfigKeys(ByVal ConfigKey As String) As String
        Try
            Dim strSQL As String = String.Empty
            Dim Dt As New DataTable
            Dim Adp As New SqlDataAdapter
            Dim Cn As New SqlConnection
            Cn = New SqlConnection(SQLHelper.CON_STR)
            If Cn.State = ConnectionState.Closed Then Cn.Open()
            strSQL = "select Config_Type From ConfigValuesTable WHERE Config_Type=NN'" & ConfigKey & "'"
            Adp = New SqlDataAdapter(strSQL, Cn)
            Adp.Fill(Dt)
            If Dt.Rows.Count > 0 Then
                Return Dt.Rows(0).Item("Config_Type").ToString
            Else
                Return "Err"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAllRecords() As DataTable
        Try
            Dim str As String = String.Empty
            Dim Con As SqlConnection
            Con = New SqlConnection(SQLHelper.CON_STR)
            If Con.State = ConnectionState.Closed Then Con.Open()
            Dim dt As New DataTable
            Dim adp As SqlDataAdapter
            str = "Select * From ConfigValuesTable"
            adp = New SqlDataAdapter(str, Con)
            adp.Fill(dt)
            Con.Close()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetConfigValueByAll() As List(Of ConfigSystem)
        Dim ConfigList As New List(Of ConfigSystem)
        Dim Config As ConfigSystem
        Try
            Dim strSQL As String = "Select Config_Id, Config_Value, Config_Type From ConfigValuesTable"
            Dim dr As SqlDataReader = SQLHelper.ExecuteReader(SQLHelper.CON_STR, CommandType.Text, strSQL, Nothing)
            If dr.HasRows Then
                Do While dr.Read
                    Config = New ConfigSystem
                    Config.Config_Id = Val(dr.GetValue(0).ToString)
                    Config.Config_Value = dr.GetValue(1).ToString
                    Config.Config_Type = dr.GetValue(2).ToString
                    ConfigList.Add(Config)
                Loop
            End If
            Return ConfigList
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

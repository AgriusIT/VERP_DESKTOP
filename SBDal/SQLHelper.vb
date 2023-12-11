''1-Jan-2014 Imran Ali          Tsk:2365    Problem Fix Connection String 
Imports System.Net
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBUtility.Utility
Public Class SQLHelper
    'Before against task:2366
    Public Shared CON_STR As String = "Data Source=" & DBServerName & ";Initial Catalog=" & DBName & "; " & IIf(DBUserName.ToString <> "", "User ID=" & DBUserName & ";Password=" & DBPassword & ";Integrated Security=False", "Integrated Security=True") & ";Connect Timeout=120 " 'GetConnectionString()   '"Data Source=" & DBServerName & ";Initial Catalog=" & DBName & "; User ID=" & DBUserName & ";Password=" & DBPassword & ";Connect Timeout=120"
    'Added New Function of connection string 
    'Public Shared Function CON_STR() As String
    '    Try
    '        Dim ConString As String = "Data Source=" & DBServerName & ";Initial Catalog=" & DBName & "; " & IIf(DBUserName.ToString <> "", "User ID=" & DBUserName & ";Password=" & DBPassword & ";Integrated Security=False", "Integrated Security=True") & ";Connect Timeout=120 " 'GetConnectionString()   '"Data Source=" & DBServerName & ";Initial Catalog=" & DBName & "; User ID=" & DBUserName & ";Password=" & DBPassword & ";Connect Timeout=120"
    '        Return ConString
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
    'End TAsk:2366
    Public Shared Function GetConnectionString() As String
        Try
            Dim str As New SqlConnectionStringBuilder(SQLHelper.CON_STR)
            'Before Against Tsk:2365
            'str.DataSource = DBServerName
            'str.InitialCatalog = DBName
            'str.DataSource = DBServerName.ToString
            'str.InitialCatalog = DBName.ToString
            ''Before against Tsk:2365
            ''If DBUserName <> "" Then
            'If DBUserName.ToString <> "" Then
            '    'Before against Tsk:2365
            '    'str.UserID = DBUserName
            '    'str.Password = DBPassword
            '    str.UserID = DBUserName.ToString
            '    str.Password = DBPassword.ToString
            '    str.IntegratedSecurity = False
            'Else
            '    str.IntegratedSecurity = True
            'End If
            'str.ConnectTimeout = 120
            'End Tsk:2365
            Return str.ConnectionString
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#Region "Without Transactions "

    ''Public Shared Function ExecuteNonQuery(ByVal connectionString As String, ByVal commandType As CommandType, ByVal commandText As String, ByVal commandParameters As SqlParameter()) As Boolean

    ''    Dim cmd As New SqlCommand()
    ''    Using conn As SqlConnection = New SqlConnection(connectionString)

    ''        Try


    ''            PrepareCommand(cmd, conn, Nothing, commandType, commandText, commandParameters)
    ''            Dim Val As Integer = cmd.ExecuteNonQuery()
    ''            cmd.Parameters.Clear()
    ''            conn.Dispose()
    ''            Return Val

    ''        Catch ex As Exception
    ''            Throw ex
    ''        End Try
    ''    End Using


    ''End Function

    ''Public Shared Function ExecuteScaler(ByVal connectionString As String, ByVal commandType As CommandType, ByVal commandText As String, ByVal commandParameters() As SqlParameter) As Object

    ''    Dim cmd As New SqlCommand()

    ''    Using conn As SqlConnection = New SqlConnection(connectionString)


    ''        PrepareCommand(cmd, conn, Nothing, commandType, commandText, commandParameters)
    ''        Dim Val As Integer = cmd.ExecuteScalar
    ''        cmd.Parameters.Clear()
    ''        conn.Dispose()
    ''        Return Val

    ''    End Using

    ''End Function

#End Region


#Region "With Transactions "


    Private Shared Sub PrepareCommand(ByVal cmd As SqlCommand, ByVal conn As SqlConnection, ByVal trans As SqlTransaction, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal cmdParms() As SqlParameter)
        Try


            If (conn.State <> ConnectionState.Open) Then conn.Open()

            cmd.Connection = conn
            cmd.CommandText = cmdText
            cmd.CommandTimeout = 500

            If Not (trans Is Nothing) Then cmd.Transaction = trans

            cmd.CommandType = cmdType

            If Not (cmdParms Is Nothing) Then
                For Each parm As SqlParameter In cmdParms
                    cmd.Parameters.Add(parm)
                Next
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Shared Sub PrepareCommand(ByVal cmd As OleDb.OleDbCommand, ByVal conn As OleDb.OleDbConnection, ByVal trans As OleDb.OleDbTransaction, ByVal cmdType As CommandType, ByVal cmdText As String, ByVal cmdParms() As OleDb.OleDbParameter)
        Try


            If (conn.State <> ConnectionState.Open) Then conn.Open()

            cmd.Connection = conn
            cmd.CommandText = cmdText
            cmd.CommandTimeout = 500

            If Not (trans Is Nothing) Then cmd.Transaction = trans

            cmd.CommandType = cmdType

            If Not (cmdParms Is Nothing) Then
                For Each parm As OleDb.OleDbParameter In cmdParms
                    cmd.Parameters.Add(parm)
                Next
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    Public Shared Function ExecuteNonQuery(ByVal trans As SqlTransaction, ByVal commandType As CommandType, ByVal commandText As String, Optional ByVal commandParameters As SqlParameter() = Nothing, Optional ByRef RecrodAffected As Long = 0) As Boolean
        Try
            Dim cmd As New SqlCommand()
            PrepareCommand(cmd, trans.Connection, trans, commandType, commandText, commandParameters)
            Dim Val As Integer = cmd.ExecuteNonQuery()
            RecrodAffected = Val
            cmd.Parameters.Clear()
            Return Val
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function ExecuteNonQuery(ByVal trans As OleDb.OleDbTransaction, ByVal commandType As CommandType, ByVal commandText As String, Optional ByVal commandParameters As OleDb.OleDbParameter() = Nothing, Optional ByRef RecrodAffected As Long = 0) As Boolean
        Try
            Dim cmd As New OleDb.OleDbCommand()
            PrepareCommand(cmd, trans.Connection, trans, commandType, commandText, commandParameters)
            Dim Val As Integer = cmd.ExecuteNonQuery()
            RecrodAffected = Val
            cmd.Parameters.Clear()
            Return Val
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function Get_DataTable(ByVal trans As SqlTransaction, ByVal commandType As CommandType, ByVal commandText As String, Optional ByVal commandParameters As SqlParameter() = Nothing, Optional ByRef RecrodAffected As Long = 0) As DataTable
        Try
            Dim cmd As New SqlCommand()
            'PrepareCommand(cmd, trans.Connection, trans, commandType, commandText, commandParameters)
            'Dim Val As Integer = cmd.ExecuteNonQuery()
            'RecrodAffected = Val
            'cmd.Parameters.Clear()
            'Return Val
            If trans Is Nothing Then
                cmd.Connection = New SqlClient.SqlConnection(SQLHelper.CON_STR)
            Else
                cmd.Connection = trans.Connection
            End If
            cmd.CommandText = commandText
            Dim dt As New DataTable
            Dim adp As SqlClient.SqlDataAdapter
            adp = New SqlClient.SqlDataAdapter(cmd)
            adp.Fill(dt)
            If trans Is Nothing Then cmd.Connection.Close()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function ExecuteScaler(ByVal trans As SqlTransaction, ByVal commandType As CommandType, ByVal commandText As String, Optional ByVal commandParameters() As SqlParameter = Nothing) As Object
        Try
            Dim cmd As New SqlCommand()
            PrepareCommand(cmd, trans.Connection, trans, commandType, commandText, commandParameters)
            Dim Val As Object = cmd.ExecuteScalar
            cmd.Parameters.Clear()
            If IsDBNull(Val) Then Val = Nothing
            Return Val
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Shared Function ExecuteScaler(ByVal trans As OleDb.OleDbTransaction, ByVal commandType As CommandType, ByVal commandText As String, Optional ByVal commandParameters() As OleDb.OleDbParameter = Nothing) As Object
        Try
            Dim cmd As New OleDb.OleDbCommand()
            PrepareCommand(cmd, trans.Connection, trans, commandType, commandText, commandParameters)
            Dim Val As Object = cmd.ExecuteScalar
            cmd.Parameters.Clear()
            If IsDBNull(Val) Then Val = Nothing
            Return Val
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Shared Function ExecuteScaler(ByVal connectionString As String, ByVal commandType As CommandType, ByVal commandText As String, Optional ByVal commandParameters() As SqlParameter = Nothing) As Object

        Dim cmd As New SqlCommand()

        Using conn As SqlConnection = New SqlConnection(connectionString)


            PrepareCommand(cmd, conn, Nothing, commandType, commandText, commandParameters)
            Dim Val As Object = cmd.ExecuteScalar
            cmd.Parameters.Clear()
            conn.Dispose()
            Return Val

        End Using

    End Function


    Public Shared Function ExecuteReader(ByVal connectionString As String, ByVal commandType As CommandType, ByVal commandText As String, ByVal commandParameters() As SqlParameter) As SqlDataReader

        Dim cmd As SqlCommand = New SqlCommand
        Dim conn As SqlConnection = New SqlConnection(connectionString)

        Try

            PrepareCommand(cmd, conn, Nothing, commandType, commandText, commandParameters)
            Dim rdr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection)
            cmd.Parameters.Clear()
            Return rdr

        Catch ex As Exception
            conn.Close()
            Throw ex

        End Try

    End Function

    Public Shared Function ExecuteReader(ByVal Trans As SqlTransaction, ByVal commandType As CommandType, ByVal commandText As String, ByVal commandParameters() As SqlParameter) As SqlDataReader

        Dim cmd As SqlCommand = New SqlCommand
        cmd.Connection = Trans.Connection ' New SqlConnection(SQLHelper.CON_STR)
        cmd.Transaction = Trans
        'Dim conn As SqlConnection = New SqlConnection(connectionString)

        Try

            PrepareCommand(cmd, cmd.Connection, Trans, commandType, commandText, commandParameters)
            Dim rdr As SqlDataReader = cmd.ExecuteReader(CommandBehavior.Default)
            cmd.Parameters.Clear()
            Return rdr

        Catch ex As Exception
            'conn.Close()
            Throw ex

        End Try

    End Function

    Public Shared Function CreateParameter(ByVal name As String, ByVal dbType As SqlDbType, ByVal value As Object) As SqlParameter
        Try
            Dim prm As SqlParameter = New SqlParameter(name, dbType)
            prm.Value = IIf(value Is Nothing, DBNull.Value, value)
            Return prm
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region
    Public Shared Function GetSQLErrorMessage(ByVal ErrorCode As Int32) As String
        Try

            Dim strErrorMessage As String = String.Empty
            Select Case ErrorCode
                Case 2601
                    strErrorMessage = "Duplicate record exists"
                Case 2602
                    strErrorMessage = ""
            End Select
            Return strErrorMessage
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

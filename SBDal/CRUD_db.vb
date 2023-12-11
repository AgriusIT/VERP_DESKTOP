Imports SBUtility.Utility
Imports System.Data.Sql
Imports System.Data.SqlClient
Public Class CRUD_db

    Public  Sub Save(ByVal query As String)
        Dim Conn As New SqlConnection(SQLHelper.CON_STR)
        Dim objCommand As New SqlCommand
        Try

            'Dim objCon As SqlConnection
            Dim i As Integer
            If Conn.State = ConnectionState.Open Then Conn.Close()

            Conn.Open()
            objCommand.Connection = Conn


            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = query
            objCommand.ExecuteNonQuery()
        Catch ex As Exception
            Conn.Close()

        End Try

    End Sub
    Public Sub Master_Insertion(ByVal MasterQuery As String, ByVal MasterDetailQuery As List(Of String))
        Dim Conn As New SqlConnection(SQLHelper.CON_STR)
        Dim objCommand As New SqlCommand
        'Dim objCon As SqlConnection
        Dim i As Integer
        If Conn.State = ConnectionState.Open Then Conn.Close()

        Conn.Open()
        objCommand.Connection = Conn

        Dim trans As SqlTransaction = Conn.BeginTransaction
        Try
            objCommand.CommandType = CommandType.Text
            objCommand.Transaction = trans
            objCommand.CommandText = MasterQuery
            Dim MasterID As Integer = objCommand.ExecuteScalar()

            ' Dim obj As Object = objCommand.ExecuteNonQuery()
            Master_Detail_Insertion(MasterID, trans, MasterDetailQuery)


            trans.Commit()


            'InsertVoucher()
        Catch ex As Exception
            trans.Rollback()
        Finally
            Conn.Close()
        End Try

    End Sub

    Public Sub Master_Detail_Insertion(ByVal masterID As Integer, ByVal trans As SqlTransaction, ByVal MasterDetailQuery As List(Of String))
        Dim objCommand As New SqlCommand
        'Dim objCon As SqlConnection
        Dim Conn As New SqlConnection(SQLHelper.CON_STR)
        Dim i As Integer

        Try
            If Conn.State = ConnectionState.Open Then Conn.Close()
            Conn.Open()
            objCommand.CommandType = CommandType.Text
            objCommand.Connection = trans.Connection
            objCommand.Transaction = trans
            For Each item As String In MasterDetailQuery
                objCommand.CommandText = item.ToString().Replace("`", "" & masterID & " ")
                objCommand.ExecuteNonQuery()
            Next
            'trans.Commit()
        Catch ex As Exception
            trans.Rollback()
        Finally
            Conn.Close()
        End Try
    End Sub

    Public Function Read_SP(ByVal Id As Integer) As DataTable

        Dim Conn As New SqlConnection(SQLHelper.CON_STR)
        Dim command As SqlCommand = New SqlCommand()
        command.Connection = Conn
        'command.CommandText = "SP_MaterialAnalysis_QuantityStock" ''SP_MaterialAnalysisQuanityCalculation
        command.CommandText = "SP_MaterialAnalysisQuanityCalculation"

        command.CommandType = CommandType.StoredProcedure
        command.Parameters.AddWithValue("MaterialEstimationId", Id)
        'Dim objCon As SqlConnection
        Dim Adapter As SqlDataAdapter
        Dim table As DataTable

        Dim i As Integer
        Try
            If Conn.State = ConnectionState.Open Then Conn.Close()
            Conn.Open()
            Adapter = New SqlDataAdapter(command)
            table = New DataTable()
            Adapter.Fill(table)
            Conn.Close()
            Return table
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function Read_SP_Command(ByVal Command As SqlCommand) As DataTable

        Dim Conn As New SqlConnection(SQLHelper.CON_STR)
        Command.Connection = Conn
        'Dim objCon As SqlConnection
        Dim Adapter As SqlDataAdapter
        Dim table As DataTable

        Dim i As Integer
        Try
            If Conn.State = ConnectionState.Open Then Conn.Close()
            Conn.Open()
            Adapter = New SqlDataAdapter(Command)
            table = New DataTable()
            Adapter.Fill(table)
            Conn.Close()
            Return table
        Catch ex As Exception
            Throw
        End Try
    End Function
    Public Function ReadTable(ByVal query As String) As DataTable
        Dim Conn As New SqlConnection(SQLHelper.CON_STR)
        Dim command As SqlCommand = New SqlCommand()
        'Dim objCon As SqlConnection
        Dim Adapter As SqlDataAdapter
        Dim table As DataTable

        Dim i As Integer
        Try
            If Conn.State = ConnectionState.Open Then Conn.Close()
            Conn.Open()
            Adapter = New SqlDataAdapter(query, Conn)
            table = New DataTable()
            Adapter.Fill(table)
            Conn.Close()
            Return table
        Catch ex As Exception
            Throw
        End Try
    End Function

    Public Sub Master_Deletion(ByVal DetailQuery As String, ByVal MasterQuery As String)
        Dim Conn As New SqlConnection(SQLHelper.CON_STR)
        'Dim connection As SqlConnection
        Dim command As SqlCommand = New SqlCommand()
        Conn.Open()
        Dim transcation As SqlTransaction = Conn.BeginTransaction()
        Try
            command.CommandType = CommandType.Text
            command.Connection = Conn
            command.Transaction = transcation

            command.CommandText = DetailQuery
            command.ExecuteNonQuery()

            command.CommandText = ""
            command.CommandText = MasterQuery
            command.ExecuteNonQuery()
            transcation.Commit()
        Catch ex As Exception
            transcation.Rollback()
        Finally
            Conn.Close()
        End Try
    End Sub
    Public Sub Master_Update(ByVal MasterID As Integer, ByVal MasterQuery As String, ByVal MasterDetailQuery As List(Of String), ByVal Deletequery As String)
        Dim Conn As New SqlConnection(SQLHelper.CON_STR)
        Dim objCommand As New SqlCommand
        'Dim objCon As SqlConnection
        Dim i As Integer
        If Conn.State = ConnectionState.Open Then Conn.Close()
        Conn.Open()
        objCommand.Connection = Conn

        Dim trans As SqlTransaction = Conn.BeginTransaction
        Try
            objCommand.Transaction = trans
            objCommand.CommandText = Deletequery
            objCommand.ExecuteNonQuery()
            objCommand.CommandText = MasterQuery
            'Dim MasterID As Integer = objCommand.ExecuteScalar()
            objCommand.ExecuteNonQuery()
            Master_Detail_Insertion(MasterID, trans, MasterDetailQuery)
            trans.Commit()
            'Conn.Close()
            'InsertVoucher()
        Catch ex As Exception
            trans.Rollback()
        Finally
            Conn.Close()
        End Try
    End Sub
End Class

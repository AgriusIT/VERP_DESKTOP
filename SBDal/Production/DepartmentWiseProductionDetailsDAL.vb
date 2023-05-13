Imports System.Data.SqlClient
Imports SBModel
Imports SBDal


Public Class DepartmentWiseProductionDetailDAL
    Public Property masterid As Integer
    Public Sub Detail_Insertion(ByVal MasterID As Integer, ByVal MasterDetailQuery As List(Of String))
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
            'objCommand.CommandText = MasterQuery
            'Dim MasterID As Integer = objCommand.ExecuteScalar()


            DB_Detail_Insertion(MasterID, trans, MasterDetailQuery)



        Catch ex As Exception
            trans.Rollback()
        Finally
            Conn.Close()
        End Try
    End Sub
    Public Sub DB_Detail_Insertion(ByVal masterID As Integer, ByVal trans As SqlTransaction, ByVal MasterDetailQuery As List(Of String))
        Dim objCommand As New SqlCommand
        'Dim objCon As SqlConnection
        Dim Conn As New SqlConnection(SQLHelper.CON_STR)
        Dim i As Integer

        Try
            If Conn.State = ConnectionState.Open Then Conn.Close()
            Conn.Open()
            objCommand.Connection = trans.Connection
            objCommand.Transaction = trans
            For Each item As String In MasterDetailQuery
                objCommand.CommandText = item.ToString().Replace("`", " " & masterID & " ")
                objCommand.ExecuteNonQuery()
            Next
            trans.Commit()
        Catch ex As Exception
            trans.Rollback()
        Finally
            Conn.Close()
        End Try
    End Sub
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
    Public Sub Master_Deletion(ByVal DetailQuery)
        Dim Conn As New SqlConnection(SQLHelper.CON_STR)
        'Dim connection As SqlConnection
        Dim command As SqlCommand = New SqlCommand()
        If Conn.State = ConnectionState.Open Then Conn.Close()
        Conn.Open()
        Dim transcation As SqlTransaction = Conn.BeginTransaction()
        Try
            command.CommandType = CommandType.Text
            command.Connection = Conn
            command.Transaction = transcation

            command.CommandText = DetailQuery
            command.ExecuteNonQuery()


            transcation.Commit()
        Catch ex As Exception
            transcation.Rollback()
        Finally
            Conn.Close()
        End Try
    End Sub
    Public Sub Detail_Update(ByVal MasterID As Integer, ByVal DetailQuery As List(Of String), ByVal Deletequery As String)
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


            DB_Detail_Insertion(MasterID, trans, DetailQuery)

        Catch ex As Exception
            trans.Rollback()
        Finally
            Conn.Close()
        End Try
    End Sub
End Class


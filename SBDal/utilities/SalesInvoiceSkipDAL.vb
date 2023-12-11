Imports SBModel
Imports System.Data.SqlClient

Public Class SalesInvoiceSkipDAL
    Dim trans As SqlTransaction
    Dim cmd As SqlCommand
    Dim str As String



    Public Function Save(ByVal model As SalesInvoiceSkip) As Boolean
        Try
            Dim connection As New SqlConnection(SQLHelper.CON_STR)
            cmd = New SqlCommand()
            If connection.State = ConnectionState.Open Then
                connection.Close()
            End If
            connection.Open()
            trans = connection.BeginTransaction()
            cmd.Transaction = trans
            cmd.Connection = connection
            cmd.CommandType = CommandType.Text
            cmd.CommandText = "Insert Into tblSalesInvoiceSkip(SalesInvoiceSkipNo, Reason, CompanyId) " _
                            & " Values(N'" & model.SalesInvoiceSkipNo.Replace("'", "''") & "', N'" & model.Reason.Replace("'", "''") & "', " & model.CompanyId & ") "
            cmd.ExecuteNonQuery()
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Return False
            Throw ex
        End Try
    End Function
    Public Function Update(ByVal model As SalesInvoiceSkip) As Boolean
        Try
            Dim connection As New SqlConnection(SQLHelper.CON_STR)
            cmd = New SqlCommand()
            If connection.State = ConnectionState.Open Then
                connection.Close()
            End If
            connection.Open()
            trans = connection.BeginTransaction()
            cmd.Connection = connection
            cmd.CommandType = CommandType.Text
            cmd.Transaction = trans
            cmd.CommandText = " Update tblSalesInvoiceSkip SET SalesInvoiceSkipNo =N'" & model.SalesInvoiceSkipNo.Replace("'", "''") & "', CompanyId =" & model.CompanyId & ", Reason = N'" & model.Reason.Replace("'", "''") & "' WHERE SalesInvoiceSkipId = " & model.SalesInvoiceSkipId & ""
            cmd.ExecuteNonQuery()
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Return False
            Throw ex
        End Try
    End Function

    Public Function Delete(ByVal SalesInvoiceSkipId As Integer) As Boolean
        Try
            Dim connection As New SqlConnection(SQLHelper.CON_STR)
            cmd = New SqlCommand()
            If connection.State = ConnectionState.Open Then
                connection.Close()
            End If
            connection.Open()
            trans = connection.BeginTransaction()
            cmd.Connection = connection
            cmd.CommandType = CommandType.Text
            cmd.Transaction = trans
            cmd.CommandText = " Delete From tblSalesInvoiceSkip WHERE SalesInvoiceSkipId = " & SalesInvoiceSkipId & ""
            cmd.ExecuteNonQuery()
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Return False
            Throw ex
        End Try
    End Function
    Public Function GetAll() As DataTable
        Try
            str = "SELECT * FROM tblSalesInvoiceSkip"
            Dim dt As DataTable = UtilityDAL.GetDataTable(str)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function IsIssued(ByVal InvoiceNo As String) As Boolean
        Dim Str1 As String = String.Empty
        Try
            Str1 = "SELECT Count(*) AS Counts FROM SalesMasterTable WHERE SalesNo='" & InvoiceNo & "'"
            Dim dt As DataTable = UtilityDAL.GetDataTable(Str1)
            If dt.Rows.Count > 0 Then
                If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                    Return True
                Else
                    Return False
                End If

            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function IsSkipped(ByVal InvoiceNo As String) As Boolean
        Dim Str1 As String = String.Empty
        Try
            Str1 = "SELECT Count(*) AS Counts FROM tblSalesInvoiceSkip WHERE SalesInvoiceSkipNo='" & InvoiceNo & "'"
            Dim dt As DataTable = UtilityDAL.GetDataTable(Str1)
            If dt.Rows.Count > 0 Then
                If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

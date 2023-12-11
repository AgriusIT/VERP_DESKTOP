Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports SBModel
Public Class ServicesDAL
    Enum enmServices
        ServicesID
        ServicesType
        Tax_Percent
        WHT_Percent
        Opex_Sale_Percent
        Region
    End Enum
    Public Function Add(ByVal Services As ServicesBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try


            Dim strSQL As String = String.Empty
            strSQL = "INSERT INTO tblDefServices(ServicesType, Tax_Percent, WHT_Percent, Opex_Sale_Percent, Region) VALUES(N'" & Services.ServicesType.Replace("'", "''") & "', " & Services.Tax_Percent & ", " & Services.WHTax & "," & Services.Opex & ",'" & Services.Region.Replace("'", "''") & "')"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function Update(ByVal Services As ServicesBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try


            Dim strSQL As String = String.Empty
            strSQL = "UPDATE tblDefServices  SET ServicesType=N'" & Services.ServicesType.Replace("'", "''") & "', Tax_Percent=" & Services.Tax_Percent & ", WHT_Percent=" & Services.WHTax & ", Opex_Sale_Percent=" & Services.Opex & ", Region=N'" & Services.Region.Replace("'", "''") & "' WHERE ServicesID=" & Services.ServicesID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function


    Public Function Delete(ByVal Services As ServicesBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try


            Dim strSQL As String = String.Empty
            strSQL = "Delete From tblDefServices WHERE ServicesID=" & Services.ServicesID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)

            trans.Commit()
            Return True

        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function GetAllRecord() As DataTable
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Select * From tblDefServices"
            Dim dt As New DataTable
            dt = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

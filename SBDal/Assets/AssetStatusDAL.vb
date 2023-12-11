Imports System.Data.SqlClient
Imports SBModel
Imports SBDal.UtilityDAL

Public Class AssetStatusDAL

    Public Function Save(ByVal cat As AssetStatusBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "insert into tblAssetStatus(Asset_Status_Name,Asset_Status_Description,Sort_Order,Active)values(N'" & cat.Asset_Status_Name & "',N'" & cat.Asset_Status_Description & "',N'" & cat.Sort_Order & "'," & IIf(cat.Active = True, 1, 0) & ")"
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function Update(ByVal cat As AssetStatusBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "Update tblAssetStatus set Asset_Status_Name = N'" & cat.Asset_Status_Name & "', " _
            & " Asset_Status_Description = N'" & cat.Asset_Status_Description & "', " _
            & " Sort_Order = N'" & cat.Sort_Order & "', " _
            & " Active = " & IIf(cat.Active = True, 1, 0) & " where Asset_Status_Id = " & cat.Asset_Status_Id
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function Delete(ByVal cat As AssetStatusBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "Delete from tblAssetStatus where Asset_Status_Id = " & cat.Asset_Status_Id
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, str)
            trans.Commit()
            Return True
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function GetRecords() As DataTable
        Try
            Dim str As String = String.Empty
            str = "Select * from tblAssetStatus"
            Return GetDatatable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

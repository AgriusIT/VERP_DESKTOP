Imports System.Data.SqlClient
Imports SBModel
Imports SBDal.UtilityDAL

Public Class AssetLocationDAL

    Public Function Save(ByVal loc As AssetLocationBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "insert into tblAssetLocation(Asset_Location_Name,Sort_Order,Active)values(N'" & loc.Asset_Location_Name & "',N'" & loc.Sort_Order & "',N'" & IIf(loc.Active = True, 1, 0) & "')"
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

    Public Function Update(ByVal loc As AssetLocationBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "Update tblAssetLocation set Asset_Location_Name = N'" & loc.Asset_Location_Name & "', " _
            & " Sort_Order = N'" & loc.Sort_Order & "', " _
            & " Active = " & IIf(loc.Active = True, 1, 0) & " where Asset_Location_Id = " & loc.Asset_Location_Id
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

    Public Function Delete(ByVal loc As AssetLocationBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "Delete from tblAssetLocation where Asset_Location_Id = " & loc.Asset_Location_Id
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
            str = "Select * from tblAssetLocation "
            Return GetDatatable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

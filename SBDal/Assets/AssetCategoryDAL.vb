Imports System.Data.SqlClient
Imports SBModel
Imports SBDal.UtilityDAL

Public Class AssetCategoryDAL
    Public Function Save(ByVal category As AssetCategoryBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "insert into tblAssetCategory(Asset_Category_Name,Asset_Category_Description,Sort_Order,Active)values(N'" & category.Asset_Category_Name & "',N'" & category.Asset_Category_Description & "',N'" & category.Sort_Order & "',N'" & IIf(category.Active = True, 1, 0) & "')"
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

    Public Function Update(ByVal cat As AssetCategoryBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "Update tblAssetCategory set Asset_Category_Name = N'" & cat.Asset_Category_Name & "', " _
            & " Asset_Category_Description = N'" & cat.Asset_Category_Description & "', " _
            & " Sort_Order = " & cat.Sort_Order & ", " _
            & " Active = " & IIf(cat.Active = True, 1, 0) & " where Asset_Category_Id =" & cat.Asset_Category_Id
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

    Public Function Delete(ByVal cat As AssetCategoryBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "Delete from tblAssetCategory where Asset_Category_Id = " & cat.Asset_Category_Id
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
            str = "Select * from tblAssetCategory "
            Return GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

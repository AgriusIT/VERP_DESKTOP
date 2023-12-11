Imports System.Data.SqlClient
Imports SBModel
Imports SBDal.UtilityDAL

Public Class AssetTypeDAL

    Public Function Save(ByVal category As AssetTypeBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "insert into tblAssetType(Asset_Type_Name,Asset_Type_Description,Sort_Order,Active, Asset_Category_Id)values(N'" & category.Asset_Type_Name & "',N'" & category.Asset_Type_Description & "',N'" & category.Sort_Order & "',N'" & IIf(category.Active = True, 1, 0) & "', " & category.Asset_Category_Id & ")"
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

    Public Function Update(ByVal cat As AssetTypeBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "Update tblAssetType set Asset_Type_Name = N'" & cat.Asset_Type_Name & "', " _
            & " Asset_Type_Description = N'" & cat.Asset_Type_Description & "', " _
            & " Sort_Order = N'" & cat.Sort_Order & "', " _
            & " Active = " & IIf(cat.Active = True, 1, 0) & ", Asset_Category_Id=" & cat.Asset_Category_Id & " where Asset_Type_Id = " & cat.Asset_Type_Id
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

    Public Function Delete(ByVal cat As AssetTypeBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "Delete from tblAssetType where Asset_Type_Id = " & cat.Asset_Type_Id
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
            str = "SELECT dbo.tblAssetCategory.Asset_Category_Name, dbo.tblAssetType.Asset_Type_Name, dbo.tblAssetType.Asset_Type_Description, dbo.tblAssetType.Sort_Order, " _
            & " dbo.tblAssetType.Active, dbo.tblAssetType.Asset_Category_Id, dbo.tblAssetType.Asset_Type_Id " _
            & " FROM  dbo.tblAssetType INNER JOIN " _
            & "  dbo.tblAssetCategory ON dbo.tblAssetType.Asset_Category_Id = dbo.tblAssetCategory.Asset_Category_Id "
            Return GetDatatable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

Imports SBModel
Imports System.Data.SqlClient

Public Class ItemWiseDiscountDetailDAL
  
    Function Add(ByVal objModel As ItemWiseDiscountMasterBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Add(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
   
    Function Add(ByVal Obj As ItemWiseDiscountMasterBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            For Each objModel As ItemWiseDiscountDetailBE In Obj.Detail
                If objModel.ID = 0 Then
                    strSQL = "insert into  ItemWiseDiscountDetail (ItemWiseDiscountId, ArticleId) values (" & Obj.ID & ", " & objModel.ArticleId & ")"
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                ElseIf objModel.ID > 0 AndAlso objModel.IsDeleted = False Then
                    strSQL = "UPDATE  ItemWiseDiscountDetail SET ItemWiseDiscountId = " & Obj.ID & ", ArticleId = " & objModel.ArticleId & " WHERE ID = " & objModel.ID & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                ElseIf objModel.IsDeleted = True Then
                    strSQL = "DELETE FROM ItemWiseDiscountDetail WHERE ID = " & objModel.ID & ""
                    SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
                End If
            Next
            'objModel.ActivityLog.ActivityName = "Save"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Update(ByVal objModel As ItemWiseDiscountDetailBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Update(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Update(ByVal objModel As ItemWiseDiscountDetailBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update ItemWiseDiscountDetail set ItemWiseDiscountId= N'" & objModel.ItemWiseDiscountId & "', ArticleId= N'" & objModel.ArticleId & "' where ID=" & objModel.ID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.ActivityName = "Update"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Delete(ByVal objModel As ItemWiseDiscountDetailBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Delete(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Delete(ByVal objModel As ItemWiseDiscountDetailBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from ItemWiseDiscountDetail  where ID= " & objModel.ID
            'SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'objModel.ActivityLog.ActivityName = "Delete"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetAll(ByVal VendorId As Integer, ByVal CategoryId As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = "SELECT 0 AS ID, 0 AS ItemWiseDiscountId, Article.ArticleId, Article.ArticleCode AS Code, Article.ArticleDescription AS Article, Article.ArticleGroupName As Department, Article.ArticleTypeName AS [Type], Article.ArticleCompanyName AS Category, Article.ArticleGenderName AS Origin, Article.ArticleUnitName AS Unit, Article.SalePrice FROM ArticleDefView AS Article " _
                & " WHERE Article.Active= 1 AND Article.ArticleId IN (SELECT ArticleDefId FROM StockDetailTable GROUP BY ArticleDefId HAVING SUM(ISNULL(InQty, 0)-ISNULL(OutQty, 0)) > 0) "
            If VendorId > 0 Then
                strSQL += " AND Article.ArticleGenderId = " & VendorId & ""
            End If
            If CategoryId > 0 Then
                strSQL += " AND Article.ArticleCompanyId = " & CategoryId & ""
            End If
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function DisplayDetail(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = "SELECT Detail.ID, Detail.ItemWiseDiscountId, Article.ArticleId, Article.ArticleCode AS Code, Article.ArticleDescription AS Article, Article.ArticleGroupName As Department, Article.ArticleTypeName AS [Type], Article.ArticleCompanyName AS Category, Article.ArticleGenderName AS Origin, Article.ArticleUnitName AS Unit, Article.SalePrice FROM ArticleDefView AS Article INNER JOIN ItemWiseDiscountDetail AS Detail ON Article.ArticleId = Detail.ArticleId WHERE Detail.ItemWiseDiscountId = " & ID & ""
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ID, ItemWiseDiscountId, ArticleId from ItemWiseDiscountDetail  WHERE ID=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class

Imports SBModel
Imports System.Data.SqlClient

Public Class ItemWiseDiscountMasterDAL
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
    Function Add(ByVal objModel As ItemWiseDiscountMasterBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "insert into  ItemWiseDiscountMaster (FromDate, ToDate, VendorId, CategoryId, DiscountType, Discount, RepeatingNextYear) values (N'" & objModel.FromDate.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & objModel.ToDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & objModel.VendorId & ", " & objModel.CategoryId & ", " & objModel.DiscountType & ", " & objModel.Discount & ", " & IIf(objModel.RepeatingNextYear = True, 1, 0) & ") Select @@Identity"
            objModel.ID = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            Call New ItemWiseDiscountDetailDAL().Add(objModel, trans)
            'objModel.ActivityLog.ActivityName = "Save"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Update(ByVal objModel As ItemWiseDiscountMasterBE) As Boolean
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
    Function Update(ByVal objModel As ItemWiseDiscountMasterBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "UPDATE ItemWiseDiscountMaster SET FromDate= N'" & objModel.FromDate.ToString("yyyy-M-d h:mm:ss tt") & "', ToDate= N'" & objModel.ToDate.ToString("yyyy-M-d h:mm:ss tt") & "', VendorId= " & objModel.VendorId & ", CategoryId= " & objModel.CategoryId & ", DiscountType= " & objModel.DiscountType & ", Discount= " & objModel.Discount & ", RepeatingNextYear= " & IIf(objModel.RepeatingNextYear = True, 1, 0) & " WHERE ID=" & objModel.ID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            Call New ItemWiseDiscountDetailDAL().Add(objModel, trans)
            'objModel.ActivityLog.ActivityName = "Update"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Delete(ByVal objModel As ItemWiseDiscountMasterBE) As Boolean
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
    Function Delete(ByVal objModel As ItemWiseDiscountMasterBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from ItemWiseDiscountMaster  WHERE ID= " & objModel.ID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            strSQL = "Delete from ItemWiseDiscountDetail  WHERE ItemWiseDiscountId= " & objModel.ID
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'objModel.ActivityLog.ActivityName = "Delete"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        'Query = " SELECT   ArticleCompanyId AS ID, ArticleCompanyName AS Name, CategoryCode " & _
        '                " FROM ArticleCompanyDefTable " & _
        '                " WHERE Active = 1"
        'Query = " SELECT ArticleGenderId as Id, ArticleGenderName as Name from ArticleGenderDefTable WHERE active=1 order by sortOrder"
        Try
            strSQL = " Select Discount.ID, Discount.FromDate, Discount.ToDate, Discount.VendorId, Vendor.ArticleGenderName AS Name, Discount.CategoryId, Category.ArticleCompanyName AS Category, ISNULL(Discount.DiscountType, 0) AS DiscountId, tblDiscountType.DiscountType, Discount.Discount, IsNull(Discount.RepeatingNextYear, 0) AS RepeatingNextYear FROM ItemWiseDiscountMaster AS Discount " _
                & " LEFT OUTER JOIN ArticleGenderDefTable AS Vendor ON Discount.VendorId = Vendor.ArticleGenderId " _
                & " LEFT OUTER JOIN ArticleCompanyDefTable AS Category ON Discount.CategoryId = Category.ArticleCompanyId " _
                & " LEFT OUTER JOIN tblDiscountType ON Discount.DiscountType = tblDiscountType.DiscountID ORDER BY Discount.ID DESC"
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select ID, FromDate, ToDate, VendorId, CategoryId, DiscountType, Discount, RepeatingNextYear from ItemWiseDiscountMaster  where ID=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

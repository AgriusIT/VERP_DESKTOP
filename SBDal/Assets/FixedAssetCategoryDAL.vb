Imports SBDal
Imports SBModel
Imports System.Data.SqlClient

Public Class FixedAssetCategoryDAL
    Public Shared Function Add(ByVal objModel As AssetCategoryBE) As Boolean

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
    Public Shared Function Add(ByVal objModel As AssetCategoryBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Insert Into  tblAssetCategory(Asset_Category_Name, Asset_Category_Description, Sort_Order, Active, Code, AssetAccount_coa_detail_id, ExpenseAccount_coa_detail_id, AccumulativeAccount_coa_detail_id, DepreciationMethod, Frequency, Rate, Remarks) values (N'" & objModel.Asset_Category_Name.Replace("'", "''") & "', N'" & objModel.Asset_Category_Description.Replace("'", "''") & "', " & objModel.Sort_Order & ", " & IIf(objModel.Active = True, 1, 0) & ", N'" & objModel.Code & "', " & objModel.AssetAccount_coa_detail_id & ", " & objModel.ExpenseAccount_coa_detail_id & ", " & objModel.AccumulativeAccount_coa_detail_id & ", N'" & objModel.DepreciationMethod.Replace("'", "''") & "', N'" & objModel.Frequency.Replace("'", "''") & "', " & objModel.Rate & ", N'" & objModel.Remarks.Replace("'", "''") & "') Select @@Identity"
            objModel.Asset_Category_Id = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            'objModel.ActivityLog.ActivityName = "Save"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Update(ByVal objModel As AssetCategoryBE) As Boolean
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
    Public Shared Function Update(ByVal objModel As AssetCategoryBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Update tblAssetCategory set Asset_Category_Name= N'" & objModel.Asset_Category_Name.Replace("'", "''") & "', Asset_Category_Description= N'" & objModel.Asset_Category_Description.Replace("'", "''") & "', Sort_Order= " & objModel.Sort_Order & ", Active= " & IIf(objModel.Active = True, 1, 0) & ", Code= N'" & objModel.Code & "', AssetAccount_coa_detail_id= " & objModel.AssetAccount_coa_detail_id & ", ExpenseAccount_coa_detail_id= " & objModel.ExpenseAccount_coa_detail_id & ", AccumulativeAccount_coa_detail_id= " & objModel.AccumulativeAccount_coa_detail_id & ", DepreciationMethod= N'" & objModel.DepreciationMethod.Replace("'", "''") & "', Frequency= N'" & objModel.Frequency.Replace("'", "''") & "', Rate= " & objModel.Rate & ", Remarks= N'" & objModel.Remarks.Replace("'", "''") & "' WHERE Asset_Category_Id=" & objModel.Asset_Category_Id
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'objModel.ActivityLog.ActivityName = "Update"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function Delete(ByVal objModel As AssetCategoryBE) As Boolean
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
    Public Shared Function Delete(ByVal objModel As AssetCategoryBE, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from tblAssetCategory  WHERE Asset_Category_Id= " & objModel.Asset_Category_Id
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
    Public Shared Function Delete(ByVal AssetCategoryId As Integer) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            Delete(AssetCategoryId, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function Delete(ByVal AssetCategoryId As Integer, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from tblAssetCategory WHERE Asset_Category_Id= " & AssetCategoryId
            SQLHelper.ExecuteNonQuery(trans, CommandType.Text, strSQL)
            'objModel.ActivityLog.ActivityName = "Delete"
            'objModel.ActivityLog.RecordType = "Configuration"
            'objModel.ActivityLog.RefNo = ""
            'UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function GetAll() As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select Category.Asset_Category_Id AS AssetCategoryId, Category.Asset_Category_Name, Category.Asset_Category_Description, Category.Sort_Order, Category.Code, Category.AssetAccount_coa_detail_id AS AssetAccountId, AssetAccount.detail_title AS AssetAccount, Category.ExpenseAccount_coa_detail_id AS ExpenseAccountId, ExpenseAccount.detail_title AS ExpenseAccount, Category.AccumulativeAccount_coa_detail_id AS AccumulativeAccountId, AccumulativeAccount.detail_title AS AccumulativeAccount, Category.DepreciationMethod, Category.Frequency, Category.Rate, Category.Remarks, IsNull(Category.Active, 0) As Active " _
                   & " from tblAssetCategory AS Category LEFT OUTER JOIN vwCOADetail AS AssetAccount ON Category.AssetAccount_coa_detail_id = AssetAccount.coa_detail_id LEFT OUTER JOIN vwCOADetail AS ExpenseAccount ON Category.ExpenseAccount_coa_detail_id = ExpenseAccount.coa_detail_id " _
                   & " LEFT OUTER JOIN vwCOADetail AS AccumulativeAccount ON Category.AccumulativeAccount_coa_detail_id = AccumulativeAccount.coa_detail_id"
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Shared Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select Asset_Category_Id, Asset_Category_Name, Asset_Category_Description, Sort_Order, Active, Code, AssetAccount_coa_detail_id, ExpenseAccount_coa_detail_id, AccumulativeAccount_coa_detail_id, DepreciationMethod, Frequency, Rate, Remarks from tblAssetCategory  where Asset_Category_Id=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

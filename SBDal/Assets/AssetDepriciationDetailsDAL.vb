Imports SBModel
Imports System.Data.SqlClient
Imports SBDal.UtilityDAL

Public Class AssetDepriciationDetailsDAL

    Function Add(ByVal objModel As AssetDepriciationDetailsBE) As Boolean
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
    Function Add(ByVal objModel As AssetDepriciationDetailsBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "insert into  tblDepriciationDetails (Asset_Id, Rate, DepriciationAmount, Closing_Value, DepriciationMasterID) values (N'" & objModel.Asset_Id & "', N'" & objModel.Rate & "', N'" & objModel.DepriciationAmount & "', N'" & objModel.Closing_Value & "', N'" & objModel.DepriciationMasterID & "') Select @@Identity"
            objModel.DepriciationDetailsID = SQLHelper.ExecuteScaler(trans, CommandType.Text, strSQL)
            objModel.ActivityLog.ActivityName = "Save"
            objModel.ActivityLog.RecordType = "Configuration"
            objModel.ActivityLog.RefNo = ""
            UtilityDAL.BuildActivityLog(objModel.ActivityLog, trans)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Update(ByVal objModel As AssetDepriciationDetailsBE) As Boolean
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
    Function Update(ByVal objModel As AssetDepriciationDetailsBE, trans As SqlTransaction) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "update tblDepriciationDetails set Asset_Id= N'" & objModel.Asset_Id & "', Rate= N'" & objModel.Rate & "', DepriciationAmount= N'" & objModel.DepriciationAmount & "', Closing_Value= N'" & objModel.Closing_Value & "', DepriciationMasterID= N'" & objModel.DepriciationMasterID & "' where DepriciationDetailsID=" & objModel.DepriciationDetailsID
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

    Function Delete(ByVal objModel As AssetDepriciationDetailsBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction()
        Try
            'Delete(objModel, trans)
            trans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Shared Function Delete(ByVal DepreciationId As Integer, trans As SqlTransaction) As Boolean
        Dim strSQL As String = String.Empty
        Try
            strSQL = "Delete from tblDepriciationDetails  where DepriciationMasterID= " & DepreciationId
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

    Public Shared Function GetDetail(ByVal DepreciationId As Integer) As DataTable
        Dim strSQL As String = String.Empty
        'C.Asset_Category_Name as Category , A.Asset_Number as AssetNo , C.DepreciationMethod as Method , A.PurchasePrice as AcquireCost , A.PurchasePrice - (SELECT IsNull (sum(IsNull (DepriciationAmount , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id) as CurrentValue , C.Rate as Rate , Case WHEN C.DepreciationMethod = 'ZicZac' Then ((A.PurchasePrice - (SELECT IsNull (sum(IsNull (DepriciationAmount , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id)) * (C.Rate/100)) * ((DateDiff(Month, A.PurchaseDate, '" & _date & "')+1) - (SELECT IsNull (sum(IsNull (DepriciationMonths , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id))  Else (A.PurchasePrice*(C.Rate/100))*((DateDiff(Month, A.PurchaseDate, '" & _date & "')+1) - (SELECT IsNull (sum(IsNull (DepriciationMonths , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id)) End as DepriciationAmount, Case WHEN C.DepreciationMethod = 'ZicZac' Then (A.PurchasePrice - (SELECT IsNull (sum(IsNull (DepriciationAmount , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id)) - (A.PurchasePrice*(C.Rate/100))*((DateDiff(Month, A.PurchaseDate, '" & _date & "')+1) - (SELECT IsNull (sum(IsNull (DepriciationMonths , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id)) Else A.PurchasePrice - (A.PurchasePrice*(C.Rate/100))*((DateDiff(Month, A.PurchaseDate, '" & _date & "')+1)) End as ClosingValue , A.Asset_id , (DateDiff(Month, A.PurchaseDate, '" & _date & "')+1) as DepriciationMonths
        Try
            strSQL = "select D.DepriciationDetailsID AS DepreciationDetailId, C.Asset_Category_Name as Category , A.Asset_Number as AssetNo  , C.DepreciationMethod as Method , " _
                      & "A.PurchasePrice as AcquireCost , D.Rate , D.CurrentValue , D.DepriciationAmount , D.Closing_Value as ClosingValue , D.DepriciationMonths, C.ExpenseAccount_coa_detail_id as ExpenseAccountID , C.AccumulativeAccount_coa_detail_id as AccumulativeAccountID  from tblDepriciationDetails as " _
                      & "D INNER JOIN tblAssets as A ON D.Asset_id = A.Asset_id LEFT JOIN tblAssetCategory as C on A.Asset_Category_id = C.Asset_Category_id where DepriciationMasterID = " & DepreciationId

            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GetById(ByVal ID As Integer) As DataTable
        Dim strSQL As String = String.Empty
        Try
            strSQL = " Select DepriciationDetailsID, Asset_Id, Rate, DepriciationAmount, Closing_Value, DepriciationMasterID from tblDepriciationDetails  where DepriciationDetailsID=" & ID
            Dim dt As DataTable = UtilityDAL.GetDataTable(strSQL)
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

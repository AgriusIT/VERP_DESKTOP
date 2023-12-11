Imports SBModel
Imports System.Data.SqlClient
Imports SBDal.UtilityDAL

Public Class AssetsDAL

    Public Function Save(ByVal asset As AssetBE) As Integer
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            If Con.State = ConnectionState.Closed Then Con.Open()
            Dim str As String = String.Empty
            str = "Insert into tblAssets(Asset_Name,Asset_Number,Asset_Description,Asset_Location,Asset_Manufacturer,Asset_Brand,Asset_Model,Asset_Serial_No,Asset_Picture,Asset_Detail,Sort_Order,Active,Asset_Category_Id,Asset_Type_Id,Asset_Status_Id,Asset_Condition_Id, VendorId, PurchaseDate, PurchasePrice, CurrentValue, Warranty_Expire_Date, EmployeeId)values(N'" & asset.Asset_Name & "',N'" & asset.Asset_Number & "',N'" & asset.Asset_Description & "',N'" & asset.Asset_Location & "',N'" & asset.Asset_Manufacturer & "',N'" & asset.Asset_Brand & "',N'" & asset.Asset_Model & "',N'" & asset.Asset_Serial_No & "',N'" & asset.Asset_Picture & "',N'" & asset.Asset_Detail & "',N'" & asset.Sort_Order & "',N'" & IIf(asset.Active = True, 1, 0) & "',N'" & asset.Asset_Category_Id & "',N'" & asset.Asset_Type_Id & "',N'" & asset.Asset_Status_Id & "',N'" & asset.Asset_Condition_Id & "', " & asset.VendorId & ", N'" & asset.PurchaseDate.ToString("yyyy-M-d h:mm:ss tt") & "', " & asset.PurchasePrice & ", " & asset.CurrentValue & ", N'" & asset.Warranty_Expire_Date.ToString("yyyy-M-d h:mm:ss tt") & "', " & asset.EmployeeId & ") Select @@Identity"
            asset.Asset_Id = SQLHelper.ExecuteScaler(trans, CommandType.Text, str)
            trans.Commit()
            Return asset.Asset_Id
        Catch ex As Exception
            trans.Rollback()
            Throw ex
        Finally
            Con.Close()
        End Try
    End Function

    Public Function Update(ByVal asset As AssetBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "Update tblAssets set Asset_Name = N'" & asset.Asset_Name & "', " _
            & " Asset_Number = N'" & asset.Asset_Number & "', " _
            & " Asset_Description = N'" & asset.Asset_Description & "', " _
            & " Asset_Location = N'" & asset.Asset_Location & "', " _
            & " Asset_Manufacturer = N'" & asset.Asset_Manufacturer & "', " _
            & " Asset_Brand = N'" & asset.Asset_Brand & "', " _
            & " Asset_Model = N'" & asset.Asset_Model & "', " _
            & " Asset_Serial_No = N'" & asset.Asset_Serial_No & "', " _
            & " Asset_Picture = N'" & asset.Asset_Picture & "', " _
            & " Asset_Detail = N'" & asset.Asset_Detail & "', " _
            & " Sort_Order = N'" & asset.Sort_Order & "', " _
            & " Active = N'" & IIf(asset.Active = True, 1, 0) & "', " _
            & " Asset_Category_Id = N'" & asset.Asset_Category_Id & "', " _
            & " Asset_Type_Id = N'" & asset.Asset_Type_Id & "', " _
            & " Asset_Status_Id = N'" & asset.Asset_Status_Id & "', " _
            & " Asset_Condition_Id = N'" & asset.Asset_Condition_Id & "',  " _
            & " VendorId=" & asset.VendorId & ", " _
            & " PurchaseDate=N'" & asset.PurchaseDate.ToString("yyyy-M-d h:mm:ss tt") & "', " _
            & " PurchasePrice=" & asset.PurchasePrice & " , " _
            & " CurrentValue=" & asset.CurrentValue & ", " _
            & " Warranty_Expire_Date=N'" & asset.Warranty_Expire_Date.ToString("yyyy-M-d h:mm:ss tt") & "', " _
            & " EmployeeId=" & asset.EmployeeId & " where Asset_Id = " & asset.Asset_Id
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

    Public Function Delete(ByVal asset As AssetBE) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = String.Empty
            str = "Delete from tblAssets where Asset_Id = " & asset.Asset_Id
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
            str = "Select * from tblAssets "
            Return GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function SaveImage(ByVal Asset_Id As Integer, ByVal ImagePath As String) As Boolean
        Dim Con As New SqlConnection(SQLHelper.CON_STR)
        If Con.State = ConnectionState.Closed Then Con.Open()
        Dim trans As SqlTransaction = Con.BeginTransaction
        Try
            Dim str As String = "Update tblAssets set Asset_Picture = N'" & ImagePath & "' where Asset_Id = " & Asset_Id
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

    Public Function GetAssetsDepriciations(_date As String) As DataTable
        Try
            Dim str As String = String.Empty

            'str = "select C.Asset_Category_Name as Category , A.Asset_Number as AssetNo , C.DepreciationMethod as Method , A.PurchasePrice as AcquireCost , A.PurchasePrice - (SELECT IsNull (sum(IsNull (DepriciationAmount , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id) as CurrentValue , C.Rate as Rate , Case WHEN C.DepreciationMethod = 'ZicZag' Then ((A.PurchasePrice - (SELECT IsNull (sum(IsNull (DepriciationAmount , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id)) * (C.Rate/100)) * ((DateDiff(Month, A.PurchaseDate, '" & _date & "')+1) - (SELECT IsNull (sum(IsNull (DepriciationMonths , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id))  Else (A.PurchasePrice*(C.Rate/100))*((DateDiff(Month, A.PurchaseDate, '" & _date & "')+1) - (SELECT IsNull (sum(IsNull (DepriciationMonths , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id)) End as DepriciationAmount, Case WHEN C.DepreciationMethod = 'ZicZag' Then (A.PurchasePrice - (SELECT IsNull (sum(IsNull (DepriciationAmount , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id)) - (A.PurchasePrice*(C.Rate/100))*((DateDiff(Month, A.PurchaseDate, '" & _date & "')+1) - (SELECT IsNull (sum(IsNull (DepriciationMonths , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id)) Else A.PurchasePrice - (A.PurchasePrice*(C.Rate/100))*((DateDiff(Month, A.PurchaseDate, '" & _date & "')+1)) End as ClosingValue , A.Asset_id , (DateDiff(Month, A.PurchaseDate, '" & _date & "')+1) as DepriciationMonths , C.ExpenseAccount_coa_detail_id as ExpenseAccountID , C.AccumulativeAccount_coa_detail_id as AccumulativeAccountID , A.PurchaseDate from tblAssets as A INNER JOIN tblAssetCategory as C ON A.Asset_Category_id = C.Asset_Category_id"
            'str = "select 0 AS DepreciationDetailId, C.Asset_Category_Name as Category , A.Asset_Number as AssetNo , C.DepreciationMethod as Method , A.PurchasePrice as AcquireCost , A.PurchasePrice - (SELECT IsNull (sum(IsNull (DepriciationAmount , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id) as CurrentValue , C.Rate as Rate , Case WHEN C.DepreciationMethod = 'ZigZag' Then ((A.PurchasePrice - (SELECT IsNull (sum(IsNull (DepriciationAmount , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id)) * (C.Rate/100)) * ((DateDiff(Month, A.PurchaseDate, '" & _date & "')) - (SELECT IsNull (sum(IsNull (DepriciationMonths , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id))  Else (A.PurchasePrice*(C.Rate/100))*((DateDiff(Month, A.PurchaseDate, '" & _date & "')) - (SELECT IsNull (sum(IsNull (DepriciationMonths , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id)) End as DepriciationAmount, Case WHEN C.DepreciationMethod = 'ZigZag' Then (A.PurchasePrice - (SELECT IsNull (sum(IsNull (DepriciationAmount , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id)) - ((A.PurchasePrice - (SELECT IsNull (sum(IsNull (DepriciationAmount , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id))*(C.Rate/100))*((DateDiff(Month, A.PurchaseDate, '" & _date & "')) - (SELECT IsNull (sum(IsNull (DepriciationMonths , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id)) Else A.PurchasePrice - (A.PurchasePrice*(C.Rate/100))*((DateDiff(Month, A.PurchaseDate, '" & _date & "'))) End as ClosingValue , A.Asset_id , ((DateDiff(Month, A.PurchaseDate, '" & _date & "')) - (SELECT IsNull (sum(IsNull (DepriciationMonths , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id)) as DepriciationMonths , C.ExpenseAccount_coa_detail_id as ExpenseAccountID , C.AccumulativeAccount_coa_detail_id as AccumulativeAccountID , A.PurchaseDate , C.Frequency as Frequency from tblAssets as A INNER JOIN tblAssetCategory as C ON A.Asset_Category_id = C.Asset_Category_id " _
            '& "Union All select 0 AS DepreciationDetailId , C.Asset_Category_Name as Category , A.Asset_Number as AssetNo , C.DepreciationMethod as Method , A.PurchasePrice as AcquireCost , A.PurchasePrice - (SELECT IsNull (sum(IsNull (DepriciationAmount , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id) as CurrentValue , C.Rate as Rate , Case WHEN C.DepreciationMethod = 'ZigZag' Then ((A.PurchasePrice - (SELECT IsNull (sum(IsNull (DepriciationAmount , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id)) * (C.Rate/100)) * ((DateDiff(Year, A.PurchaseDate, '" & _date & "')) - (SELECT IsNull (sum(IsNull (DepriciationMonths , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id)) Else (A.PurchasePrice*(C.Rate/100))*((DateDiff(Year, A.PurchaseDate, '" & _date & "')) - (SELECT IsNull (sum(IsNull (DepriciationMonths , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id)) End as DepriciationAmount, Case WHEN C.DepreciationMethod = 'ZigZag' Then (A.PurchasePrice - (SELECT IsNull (sum(IsNull (DepriciationAmount , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id)) - ((A.PurchasePrice - (SELECT IsNull (sum(IsNull (DepriciationAmount , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id)) *(C.Rate/100))*((DateDiff(Year, A.PurchaseDate, '" & _date & "')) - (SELECT IsNull (sum(IsNull (DepriciationMonths , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id)) Else A.PurchasePrice - (A.PurchasePrice*(C.Rate/100))*((DateDiff(Year, A.PurchaseDate, '" & _date & "'))) End as ClosingValue , A.Asset_id , ((DateDiff(Year, A.PurchaseDate, '" & _date & "')) - (SELECT IsNull (sum(IsNull (DepriciationMonths , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id)) as DepriciationMonth , C.ExpenseAccount_coa_detail_id as ExpenseAccountID , C.AccumulativeAccount_coa_detail_id as AccumulativeAccountID , A.PurchaseDate , C.Frequency as Frequency from tblAssets as A INNER JOIN tblAssetCategory as C ON A.Asset_Category_id = C.Asset_Category_id"

            str = "select C.Asset_Category_Name as Category , A.Asset_Number as AssetNo , C.DepreciationMethod as Method , A.PurchasePrice as AcquireCost , " _
                  & "A.PurchasePrice - (SELECT ISNull (sum(ISNull (DepriciationAmount , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id) as CurrentValue , " _
                  & "isNull(C.Rate,0) as Rate , Case " _
                  & "When C.Frequency = 'Monthly' AND C.DepreciationMethod = 'ZigZag' " _
                  & "Then ((A.PurchasePrice - (SELECT ISNull (sum(ISNull (DepriciationAmount , 0)), 0) " _
                  & "FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id)) * (isNull(C.Rate,0)/100)) * ((DateDiff(Month, A.PurchaseDate, '" & _date & "')) - " _
                  & "(SELECT ISNull (sum(ISNull (DepriciationMonths , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id))  " _
                  & "When C.Frequency = 'Monthly' AND C.DepreciationMethod = 'Straight' " _
                  & "Then (A.PurchasePrice*(isNull(C.Rate,0)/100))*((DateDiff(Month, A.PurchaseDate, '" & _date & "')) - (SELECT ISNull (sum(ISNull (DepriciationMonths , 0)), 0) " _
                  & "FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id)) " _
                  & "When C.Frequency = 'Yearly' AND C.DepreciationMethod = 'ZigZag' " _
                  & "Then ((A.PurchasePrice - (SELECT ISNull (sum(ISNull (DepriciationAmount , 0)), 0) " _
                  & "FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id)) * (isNull(C.Rate,0)/100)) * ((DateDiff(Year, A.PurchaseDate, '" & _date & "')) - " _
                  & "(SELECT ISNull (sum(ISNull (DepriciationMonths , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id))  " _
                  & "When C.Frequency = 'Yearly' AND C.DepreciationMethod = 'Straight' " _
                  & "Then (A.PurchasePrice*(isNull(C.Rate,0)/100))*((DateDiff(Year, A.PurchaseDate, '" & _date & "')) - (SELECT ISNull (sum(ISNull (DepriciationMonths , 0)), 0) " _
                  & "FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id)) " _
                  & "Else NULL " _
                  & "End as DepriciationAmount, " _
                  & "Case " _
                  & "When C.Frequency = 'Monthly' AND C.DepreciationMethod = 'ZigZag' " _
                  & "Then (A.PurchasePrice - (SELECT ISNull (sum(ISNull (DepriciationAmount , 0)), 0) FROM " _
                  & "tblDepriciationDetails WHERE Asset_id = A.Asset_id)) - ((A.PurchasePrice - (SELECT ISNull (sum(ISNull (DepriciationAmount , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id)) " _
                  & "*(isNull(C.Rate,0)/100))*((DateDiff(Month, A.PurchaseDate, '" & _date & "')) - " _
                  & "(SELECT ISNull (sum(ISNull (DepriciationMonths , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id)) " _
                  & "When C.Frequency = 'Monthly' AND C.DepreciationMethod = 'Straight' " _
                  & "Then A.PurchasePrice - (A.PurchasePrice*(isNull(C.Rate,0)/100))*((DateDiff(Month, A.PurchaseDate, '" & _date & "'))) " _
                  & "When C.Frequency = 'Yearly' AND C.DepreciationMethod = 'ZigZag' " _
                  & "Then (A.PurchasePrice - (SELECT ISNull (sum(ISNull (DepriciationAmount , 0)), 0) FROM " _
                  & "tblDepriciationDetails WHERE Asset_id = A.Asset_id)) - ((A.PurchasePrice - (SELECT ISNull (sum(ISNull (DepriciationAmount , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id)) " _
                  & "*(isNull(C.Rate,0)/100))*((DateDiff(Year, A.PurchaseDate, '" & _date & "')) - " _
                  & "(SELECT ISNull (sum(ISNull (DepriciationMonths , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id)) " _
                  & "When C.Frequency = 'Yearly' AND C.DepreciationMethod = 'Straight' " _
                  & "Then A.PurchasePrice - (A.PurchasePrice*(isNull(C.Rate,0)/100))*((DateDiff(Year, A.PurchaseDate, '" & _date & "'))) " _
                  & "Else NULL " _
                  & "End as ClosingValue  " _
                  & ", A.Asset_id , " _
                  & "Case WHEN C.Frequency = 'Monthly' Then " _
                  & "((DateDiff(Month, A.PurchaseDate, '" & _date & "')) - (SELECT ISNull (sum(ISNull (DepriciationMonths , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id)) " _
                  & "Else " _
                  & "((DateDiff(Year, A.PurchaseDate, '" & _date & "')) - (SELECT ISNull (sum(ISNull (DepriciationMonths , 0)), 0) FROM tblDepriciationDetails WHERE Asset_id = A.Asset_id))" _
                  & " End as DepriciationMonths " _
                  & ", C.ExpenseAccount_coa_detail_id as " _
                  & "ExpenseAccountID , C.AccumulativeAccount_coa_detail_id as AccumulativeAccountID , A.PurchaseDate , C.Frequency as Frequency from tblAssets as A INNER JOIN tblAssetCategory as " _
                  & "C ON A.Asset_Category_id = C.Asset_Category_id"

            Return GetDataTable(str)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class

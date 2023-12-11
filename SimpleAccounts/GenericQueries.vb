Imports Infragistics.Win.UltraWinTabControl
Module GenericQueries

    Private Query As String = String.Empty
    Public Function GetArticleItems(Optional ByVal CompanyRights As Boolean = False, Optional ByVal CompanyID As Integer = 0, Optional ByVal LocationID As Integer = 0, Optional ByVal Customer As Boolean = False, Optional ByVal ActiveRow As Infragistics.Win.UltraWinGrid.UltraGridRow = Nothing, Optional ByVal VendorID As Integer = 0, Optional ByVal ItemSortOrder As Boolean = False, Optional ByVal ItemSortOrderByCode As Boolean = False, Optional ByVal ItemSortOrderByName As Boolean = False, Optional ByVal ItemAscending As Boolean = False) As String
        Try
            Query = "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item, ArticleSizeName as Size, ArticleColorName as Combination, Isnull(SalePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID], 0 as Stock, Isnull(PurchasePrice,0) as PurchasePrice, Isnull(SubSubId,0) as AccountId, Isnull(ServiceItem,0) as ServiceItem, ArticleDefView.SortOrder , ArticleGroupName as [Dept], ArticleTypeName as [Type], ArticleGenderName as [Origin],ArticleLPOName as [Brand], SalesAccountId, CGSAccountId, IsNull(Cost_Price,0) as Cost_Price FROM ArticleDefView where Active=1 AND SalesItem=1"
            If CompanyRights = True Then
                Query += " AND ArticleDefView.CompanyId=" & CompanyID
            End If
            If getConfigValueByType("ArticleFilterByLocation") = "True" Then
                If GetRestrictedItemFlg(LocationID) = True Then
                    Query += " AND ArticleId In (Select ArticleDefId From RestrictedItemByLocationTable WHERE LocationId=" & LocationID & " AND Restricted=1)"
                End If
            End If
            If Customer = True Then
                'Dim ActiveRow As Infragistics.Win.UltraWinGrid.UltraGridRow
                If ActiveRow.IsActiveRow = False Then Exit Function
                Query += " AND MasterId in(Select ArticleDefId From ArticleDefCustomers WHERE CustomerId=N'" & VendorID & "')"
            End If
            'str += " ORDER By ArticleDefView.SortOrder Asc "
            ''03-Mar-2014  Task:2452    Imran Ali  4-ALPHABETIC order of items in sale and purchase window
            If ItemSortOrder = True Then
                Query += " ORDER By ArticleDefView.SortOrder " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            ElseIf ItemSortOrderByCode = True Then
                Query += " ORDER By ArticleDefView.ArticleCode " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            ElseIf ItemSortOrderByName = True Then
                Query += " ORDER By ArticleDefView.ArticleDescription " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            Else
                Query += " ORDER By ArticleDefView.SortOrder " & IIf(ItemAscending = True, "Asc", "Desc") & ""
            End If
            Return Query
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Transporters(Optional ByVal TransporterID As Integer = 0) As String
        Try
            Query = "select * from tbldeftransporter where " & IIf(TransporterID > 0, " TransporterId =" & TransporterID & "", "") & " active=1 order by sortorder,2"
            Return Query
        Catch ex As Exception
            Throw ex
        End Try


    End Function
    Public Function Category(ByVal LoginUserID As Integer, Optional ByVal CategoryID As Integer = 0) As String
        Try
            Query = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserID & ") " _
                    & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserID & ") order by sort_order " _
                    & " Else " _
                    & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"
            Return Query
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    '' Yet for sale only
    Public Function Vendor(Optional ByVal CompanyRights As Boolean = False, Optional ByVal CompanyID As Integer = 0, Optional ByVal IsEditMode As Boolean = False, Optional ByVal ConfigValueByType As String = "") As String
        Try
            If getConfigValueByType(ConfigValueByType) = "True" Then

                Query = "SELECT     vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name, vwCOADetail.detail_code as [Code],tblListState.StateName as State, tblListCity.CityName as City,  " & _
                                   "tblListTerritory.TerritoryName as Territory , tblCustomer.ExpiryDate, tblCustomer.Discper as [Discount] ,tblCustomer.otherexpanses as [Other Expense], tblCustomer.Fuel as Fuel, tblCustomer.CNG as CNG , tblCustomer.Cridtlimt as Limit, dbo.vwCOADetail.account_type as Type, isnull(customertypes,0) as typeid, vwCOADetail.Contact_Email as Email, vwCOADetail.Contact_Phone as Phone, vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title, IsNull(vwCOADetail.SaleMan,0) as SaleManId " & _
                                   "FROM  tblCustomer LEFT OUTER JOIN " & _
                                   "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                                   "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                                   "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                                   "vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " & _
                                   "WHERE (vwCOADetail.account_type in( 'Customer','Vendor' )) and  vwCOADetail.coa_detail_id is not  null "

                If CompanyRights = True Then
                    Query += " AND vwCOADetail.CompanyId=" & CompanyID
                End If
            Else
                Query = "SELECT     vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name,vwCOADetail.detail_code as [Code], tblListState.StateName as State, tblListCity.CityName as City,  " & _
                                  "tblListTerritory.TerritoryName as Territory , tblCustomer.ExpiryDate,tblCustomer.Discper as [Discount] ,tblCustomer.otherexpanses as [Other Expense], tblCustomer.Fuel as Fuel , tblCustomer.CNG as CNG , tblCustomer.Cridtlimt as Limit, dbo.vwCOADetail.account_type as Type, isnull(customertypes,0) as typeid, vwCOADetail.Contact_Email as Email, vwCOADetail.Contact_Phone as Phone, vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title, IsNull(vwCOADetail.SaleMan,0) as SaleManId " & _
                                  "FROM         tblCustomer LEFT OUTER JOIN " & _
                                  "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                                  "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                                  "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                                  "vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " & _
                                  "WHERE (vwCOADetail.account_type='Customer') and  vwCOADetail.coa_detail_id is not  null "
                If CompanyRights = True Then
                    Query += " AND vwCOADetail.CompanyId=" & CompanyID
                End If
            End If
            If IsEditMode = False Then
                Query += " AND vwCOADetail.Active=1"
            Else
                Query += " AND vwCOADetail.Active in(0,1,NULL)"
            End If
            Query += " order by tblCustomer.Sortorder, vwCOADetail.detail_title "

            Return Query
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function Company(Optional ByVal LoginUserID As Integer = 0, Optional ByVal CompanyRights As Boolean = False) As String
        Try
            Query = "If  exists(select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserID & ") " _
           & "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId, IsNull(CommercialInvoice,0) as CommercialInvoice from CompanyDefTable WHERE CompanyName <> '' " & IIf(CompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & " And CompanyId in (select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserID & ")" _
           & "Else " _
           & "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId, IsNull(CommercialInvoice,0) as CommercialInvoice from CompanyDefTable " & IIf(CompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & ""
            Return Query
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function CostCentre() As String
        Try
            Query = "Select CostCenterId, Name From tblDefCostCenter WHERE Active=1"
            Return Query
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function Location() As String
        Try
            Query = "Select Location_Id, Location_Code from tblDefLocation order by sort_order"
            Return Query
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function Salesman() As String
        Try
            Query = "Select Employee_ID, Employee_Name  + ' - ' + employee_Code as EmployeeName from tblDefEmployee WHERE SalePerson <> 0 And Active = 1"  ''TASKTFS75 added and set active =1
            Return Query
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Public Function Employees() As String
        Try
            Query = "Select Employee_ID, Employee_Name  + ' - ' + employee_Code as EmployeeName from tblDefEmployee Where Active = 1" ''TASKTFS75 added and set active =1
            Return Query
        Catch ex As Exception
            Throw ex
        End Try

    End Function

End Module

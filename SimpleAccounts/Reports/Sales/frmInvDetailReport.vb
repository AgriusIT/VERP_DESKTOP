Public Class frmInvDetailReport

    Public flgCompanyRights As Boolean = False
    Dim dt As DataTable
    Dim dtItem As DataTable
    Dim dtCustomer As DataTable
    Public dvItem As New DataView
    Public dvCustomer As New DataView
    Dim Str As String = ""
    Public DoHaveGridPrintRights As Boolean = False
    Public DoHaveGridExportRights As Boolean = False
    Public DoHaveGridFeildChosserRights As Boolean = False

    Private Sub cmbPeriod_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPeriod.SelectedIndexChanged
        If Me.cmbPeriod.Text = "Today" Then
            Me.dtpFromDate.Value = Date.Today
            Me.dtpToDate.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Yesterday" Then
            Me.dtpFromDate.Value = Date.Today.AddDays(-1)
            Me.dtpToDate.Value = Date.Today.AddDays(-1)
        ElseIf Me.cmbPeriod.Text = "Current Week" Then
            Me.dtpFromDate.Value = Date.Today.AddDays(-(Date.Now.DayOfWeek))
            Me.dtpToDate.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Month" Then
            Me.dtpFromDate.Value = New Date(Date.Now.Year, Date.Now.Month, 1)
            Me.dtpToDate.Value = Date.Today
        ElseIf Me.cmbPeriod.Text = "Current Year" Then
            Me.dtpFromDate.Value = New Date(Date.Now.Year, 1, 1)
            Me.dtpToDate.Value = Date.Today
        End If
    End Sub

    Private Sub frmInvDetailReport_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Me.cmbPeriod.Text = "Current Month"
            If Not GetConfigValue("CompanyRights").ToString = "Error" Then
                flgCompanyRights = GetConfigValue("CompanyRights")
            End If
            GetSecurityRights()
            FillCombos()
            Me.lstCompany.DeSelect()
            Me.lstCostCenter.DeSelect()
            Me.lstCostCenterHead.DeSelect()
            Me.lstCity.DeSelect()
            Me.lstCustomer.DeSelect()
            Me.lstCustomerCategory.DeSelect()
            Me.lstCustomerType.DeSelect()
            Me.lstBelt.DeSelect()
            Me.lstZone.DeSelect()
            Me.lstRegion.DeSelect()
            Me.lstState.DeSelect()
            Me.lstDirector.DeSelect()
            Me.lstSalesMan.DeSelect()
            Me.lstManager.DeSelect()
            Me.lstItems.DeSelect()
            Me.lstTerritory.DeSelect()
            Me.lstLocation.DeSelect()
            Me.lstItemCategory.DeSelect()
            Me.lstItemDepartment.DeSelect()
            Me.lstItemSubCategory.DeSelect()
            Me.lstItemType.DeSelect()
            'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
            Me.cmbPeriod.Text = "Current Month"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillCombos()
        Try
            Dim Str As String = ""
            Str = "If  exists(select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserId & " And CompanyId Is Not Null) " _
                & "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId, IsNull(CommercialInvoice,0) as CommercialInvoice from CompanyDefTable WHERE CompanyName <> '' " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & " And CompanyId in (select CompanyId from tblUserCompanyRights where User_Id = " & LoginUserId & ")" _
                & "Else " _
                & "Select CompanyId, CompanyName, Isnull(CostCenterId,0) as CostCenterId, IsNull(CommercialInvoice,0) as CommercialInvoice from CompanyDefTable " & IIf(flgCompanyRights = True, " WHERE CompanyId=" & MyCompanyId, "") & ""
            FillListBox(Me.lstCompany.ListItem, Str)

            Str = "If  exists(select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") " _
                   & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation where Location_id in (select Location_Id FROM tblUserLocationRights where UserID = " & LoginUserId & ") order by sort_order " _
                   & " Else " _
                   & " Select Location_Id, Location_Code,IsNull(AllowMinusStock,0) as AllowMinusStock from tblDefLocation order by sort_order"
            FillListBox(Me.lstLocation.ListItem, Str)

            Str = " If exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) SELECT  CostCenterID,Name,Code FROM tblDefCostCenter  where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1 Else SELECT  CostCenterID,Name,Code FROM tblDefCostCenter WHERE Active=1 "
            FillListBox(Me.lstCostCenter.ListItem, Str)

            Str = "Select distinct CostCenterGroup, CostCenterGroup from tbldefCostCenter WHERE (CostCenterGroup <> '') AND Active = 1"
            FillListBox(Me.lstCostCenterHead.ListItem, Str)




            Str = "Select Employee_Id, Employee_Name, Employee_Code From tblDefEmployee WHERE SalePerson=1 And Active = 1 ORDER BY 2 ASC"
            FillListBox(Me.lstSalesMan.ListItem, Str)
            Str = "Select Employee_Id, Employee_Name, Employee_Code From tblDefEmployee WHERE SalePerson <> 1 And Active = 1 ORDER BY 2 ASC"
            FillListBox(Me.lstManager.ListItem, Str)
            Str = "Select Employee_Id, Employee_Name, Employee_Code From tblDefEmployee WHERE SalePerson <> 1 And Active = 1 ORDER BY 2 ASC"
            FillListBox(Me.lstDirector.ListItem, Str)

            Str = "Select * from tblListstate"
            FillListBox(Me.lstState.ListItem, Str)
            Str = "Select * from tblListCity"
            FillListBox(Me.lstCity.ListItem, Str)
            Str = "Select * from tblListregion"
            FillListBox(Me.lstRegion.ListItem, Str)
            Str = "Select * from tblListTerritory"
            FillListBox(Me.lstTerritory.ListItem, Str)
            Str = "Select * from tblListbelt"
            FillListBox(Me.lstBelt.ListItem, Str)
            Str = "Select * from tblListzone"
            FillListBox(Me.lstZone.ListItem, Str)



            Str = "SELECT * FROM tblCustomerCategory "
            FillListBox(Me.lstCustomerCategory.ListItem, Str)
            Str = "SELECT Typeid, Name FROM dbo.TblDefCustomerType "
            FillListBox(Me.lstCustomerType.ListItem, Str)
            Str = "Select coa_detail_id, detail_title + '~' +detail_code as Customer_Name from vwCOADetail WHERE detail_title <> '' " & IIf(flgCompanyRights = True, " AND CompanyId=" & MyCompanyId & "", "") & " "
            If Not getConfigValueByType("Show Vendor On Sales") = "True" Then
                Str += " AND (Account_Type = 'Customer')  "
            Else
                Str += " AND (Account_Type in('Customer','Vendor'))  "
            End If
            Str += " ORDER BY 2 ASC"
            dtCustomer = GetDataTable(Str)
            dtCustomer.TableName = "Customer"
            dvCustomer.Table = dtCustomer
            FillListBox(Me.lstCustomer.ListItem, Str)
            ' Str = "Select * from tblCustomer where Active= 1 "

            Str = "SELECT  ArticleCompanyId AS ID, ArticleCompanyName AS Name, CategoryCode" & _
                  " FROM ArticleCompanyDefTable" & _
                  " WHERE Active = 1"
            FillListBox(Me.lstItemCategory.ListItem, Str)
            Str = "SELECT dbo.ArticleLpoDefTable.ArticleLpoId AS ID, dbo.ArticleLpoDefTable.ArticleLpoName AS Name, ArticleLpoDefTable.ArticleCompanyId , dbo.ArticleLpoDefTable.SubCategoryCode  FROM         dbo.ArticleLpoDefTable INNER JOIN               dbo.ArticleCompanyDefTable ON dbo.ArticleLpoDefTable.ArticleCompanyId = dbo.ArticleCompanyDefTable.ArticleCompanyId"
            FillListBox(Me.lstItemSubCategory.ListItem, Str)
            Str = "select ArticleTypeId as Id, ArticleTypeName as Name, TypeCode from ArticleTypeDefTable where active=1 order by sortOrder"
            FillListBox(Me.lstItemType.ListItem, Str)
            Str = "SELECT  ArticleGroupDefTable.ArticleGroupId AS Id, ArticleGroupDefTable.ArticleGroupName AS Name, ArticleGroupDefTable.SubSubID, ArticleGroupDefTable.GroupCode " & _
                  " FROM ArticleGroupDefTable LEFT OUTER JOIN " & _
                    "    tblCOAMainSubSubDetail ON ArticleGroupDefTable.SubSubID = tblCOAMainSubSubDetail.coa_detail_id" & _
                  " WHERE     (ArticleGroupDefTable.Active = 1) AND (tblCOAMainSubSubDetail.main_sub_sub_id In(Select main_sub_sub_id from tblCOAMainSubSub WHERE Account_Type='Inventory'))" & _
                  " ORDER BY ArticleGroupDefTable.SortOrder"
            FillListBox(Me.lstItemDepartment.ListItem, Str)
            Str = "Select ArticleId , ArticleCode + ' ~ ' + ArticleDescription + '~' + ArticleSizeName + '~' + ArticleColorName Item_Name from ArticleDefView  order by ArticleId desc "
            dtItem = GetDataTable(Str)
            dtItem.TableName = "Item"
            dvItem.Table = dtItem
            FillListBox(Me.lstItems.ListItem, Str)



        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub txtSearch_TextChanged(sender As Object, e As EventArgs) Handles txtSearch.TextChanged
        Try
            If txtSearch.Text <> "" Then
                dvItem.RowFilter = "Item_Name LIKE '%" & Me.txtSearch.Text & "%' "
            End If
            Me.lstItems.ListItem.DataSource = dvItem
            Me.lstItems.DeSelect()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lstState_SelectedIndexChaned(sender As Object, e As IndexEventArgs) Handles lstState.SelectedIndexChaned
        Try
            FillListBox(Me.lstRegion.ListItem, "Select * from tblListregion where stateId in (" & Me.lstState.SelectedIDs & ")")
            FillListBox(Me.lstCity.ListItem, "Select * from tblListCity where STateId in  (" & Me.lstState.SelectedIDs & ")")
            Me.lstCity.DeSelect()
            Me.lstRegion.DeSelect()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lstZone_SelectedIndexChaned(sender As Object, e As IndexEventArgs) Handles lstZone.SelectedIndexChaned
        Try
            FillListBox(Me.lstBelt.ListItem, "Select * from tblListbelt where ZoneId in  (" & Me.lstZone.SelectedIDs & ")")
            Me.lstBelt.DeSelect()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lstRegion_SelectedIndexChaned(sender As Object, e As IndexEventArgs) Handles lstRegion.SelectedIndexChaned
        Try
            FillListBox(Me.lstZone.ListItem, "Select * from tblListzone where regionId in  (" & Me.lstRegion.SelectedIDs & ")")
            Me.lstZone.DeSelect()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lstCity_SelectedIndexChaned(sender As Object, e As IndexEventArgs) Handles lstCity.SelectedIndexChaned
        Try
            FillListBox(Me.lstTerritory.ListItem, "Select * from tblListTerritory where CityId in  (" & Me.lstCity.SelectedIDs & ")")
            Me.lstTerritory.DeSelect()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lstCostCenterHead_SelectedIndexChaned(sender As Object, e As IndexEventArgs) Handles lstCostCenterHead.SelectedIndexChaned
        Try
            If Me.lstCostCenterHead.SelectedIDs.Length > 0 Then
                FillListBox(Me.lstCostCenter.ListItem, "If exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) SELECT  CostCenterID,Name FROM tblDefCostCenter  where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1 AND CostCenterGroup IN (" & Me.lstCostCenterHead.SelectedItems & ") Else SELECT  CostCenterID,Name FROM tblDefCostCenter WHERE Active=1 AND CostCenterGroup IN (" & Me.lstCostCenterHead.SelectedItems & ") ")
                Me.lstCostCenter.DeSelect()
            Else
                FillListBox(Me.lstCostCenter.ListItem, "If exists(select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) SELECT  CostCenterID,Name FROM tblDefCostCenter  where CostCenterID in (select CostCentre_Id FROM tblUserCostCentreRights where UserID = " & LoginUserId & " ) And Active = 1 Else SELECT  CostCenterID,Name FROM tblDefCostCenter WHERE Active=1 ")
                Me.lstCostCenter.DeSelect()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub FillGridForSalesDetail()
        Try
            If chkCustomerWise.Checked = False Then
                'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
                Dim flgExcludeTaxPrice As Boolean = False
                If Not getConfigValueByType("ExcludeTaxPrice").ToString = "Error" Then
                    flgExcludeTaxPrice = Convert.ToBoolean(getConfigValueByType("ExcludeTaxPrice").ToString)
                End If

                If flgExcludeTaxPrice = True Then
                    Str = "SELECT CompanyDefTable.CompanyName AS Company, tblDefCostCenter.Name AS Project, tblDefLocation.location_name AS Location, SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, vwCOADetail.detail_code AS [Account Code], vwCOADetail.detail_title AS [Account Title], vwCOADetail.Category AS [Customer Category], vwCOADetail.CustomerType, vwCOADetail.NTN_NO, vwCOADetail.StateName AS Province, vwCOADetail.CityName AS City, vwCOADetail.TerritoryName AS Territory, vwCOADetail.DirectorName AS Director, vwCOADetail.ManagerName AS Manager, vwCOADetail.SalesManName AS SalesMan, ArticleDefView.ArticleGroupName AS Department, ArticleDefView.ArticleTypeName AS Type, ArticleDefView.ArticleCompanyName AS Category, ArticleDefView.ArticleLpoName AS [Sub Category], ArticleDefView.ArticleGenderName AS Origin, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleColorName AS Color, ArticleDefView.ArticleSizeName AS Size, ArticleDefView.ArticleUnitName AS UOM, SalesDetailTable.ArticleSize AS Unit, SalesDetailTable.Sz7 AS [Pack Qty], SalesDetailTable.Sz1 AS Qty, SalesDetailTable.Qty AS [Total Qty], SalesDetailTable.PostDiscountPrice AS PDP, ISNULL(SalesDetailTable.PostDiscountPrice, 0) * ISNULL(SalesDetailTable.Qty, 0) AS GrossAmount, CASE WHEN SalesDetailTable.DiscountId > 1 THEN 'Flat' ELSE 'Percentage' END AS DiscountType, SalesDetailTable.DiscountFactor, SalesDetailTable.DiscountValue, SalesDetailTable.DiscountValue * SalesDetailTable.Qty AS [Total Disc Amount], SalesDetailTable.Price AS [Price After Disc.], 0 As DiscRatio, ISNULL(SalesDetailTable.TaxPercent, 0) AS GST, SUM(ISNULL(SalesDetailTable.Qty, 0) * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0))- (SUM(ISNULL(SalesDetailTable.Qty, 0) * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0))/(1+(ISNULL(SalesDetailTable.TaxPercent, 0) / 100))) AS TaxAmount, (SUM(ISNULL(SalesDetailTable.Qty, 0) * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0)))/(1+(ISNULL(SalesDetailTable.TaxPercent, 0) / 100)) AS ExclusiveTaxAmount, SUM(ISNULL(SalesDetailTable.Qty, 0) * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0)) AS NetAmount, SalesDetailTable.BatchNo, SalesDetailTable.ExpiryDate " & _
                   " FROM tblDefLocation INNER JOIN vwCOADetail INNER JOIN SalesMasterTable INNER JOIN SalesDetailTable ON SalesMasterTable.SalesId = SalesDetailTable.SalesId INNER JOIN ArticleDefView ON SalesDetailTable.ArticleDefId = ArticleDefView.ArticleId ON vwCOADetail.coa_detail_id = SalesMasterTable.CustomerCode ON tblDefLocation.location_id = SalesDetailTable.LocationId LEFT OUTER JOIN tblDefCostCenter ON SalesMasterTable.CostCenterId = tblDefCostCenter.CostCenterID LEFT OUTER JOIN CompanyDefTable ON SalesMasterTable.LocationId = CompanyDefTable.CompanyId  " & _
                   " WHERE  (Convert(varchar, SalesMasterTable.SalesDate,102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.Date.ToString("yyyy-M-d 23:59:59") & "', 102)) AND ArticleDefview.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & ""
                Else
                    Str = "SELECT CompanyDefTable.CompanyName AS Company, tblDefCostCenter.Name AS Project, tblDefLocation.location_name AS Location, SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, vwCOADetail.detail_code AS [Account Code], vwCOADetail.detail_title AS [Account Title], vwCOADetail.Category AS [Customer Category], vwCOADetail.CustomerType, vwCOADetail.NTN_NO, vwCOADetail.StateName AS Province, vwCOADetail.CityName AS City, vwCOADetail.TerritoryName AS Territory, vwCOADetail.DirectorName AS Director, vwCOADetail.ManagerName AS Manager, vwCOADetail.SalesManName AS SalesMan, ArticleDefView.ArticleGroupName AS Department, ArticleDefView.ArticleTypeName AS Type, ArticleDefView.ArticleCompanyName AS Category, ArticleDefView.ArticleLpoName AS [Sub Category], ArticleDefView.ArticleGenderName AS Origin, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleColorName AS Color, ArticleDefView.ArticleSizeName AS Size, ArticleDefView.ArticleUnitName AS UOM, SalesDetailTable.ArticleSize AS Unit, SalesDetailTable.Sz7 AS [Pack Qty], SalesDetailTable.Sz1 AS Qty, SalesDetailTable.Qty AS [Total Qty], SalesDetailTable.PostDiscountPrice AS PDP, ISNULL(SalesDetailTable.PostDiscountPrice, 0) * ISNULL(SalesDetailTable.Qty, 0) AS GrossAmount, CASE WHEN SalesDetailTable.DiscountId > 1 THEN 'Flat' ELSE 'Percentage' END AS DiscountType, SalesDetailTable.DiscountFactor, SalesDetailTable.DiscountValue, SalesDetailTable.DiscountValue * SalesDetailTable.Qty AS [Total Disc Amount], SalesDetailTable.Price AS [Price After Disc.], 0 As DiscRatio, ISNULL(SalesDetailTable.TaxPercent, 0) AS GST, SUM(ISNULL(SalesDetailTable.Qty, 0) * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0))- (SUM(ISNULL(SalesDetailTable.Qty, 0) * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0))/((ISNULL(SalesDetailTable.TaxPercent, 0) / 100))) AS TaxAmount, (SUM(ISNULL(SalesDetailTable.Qty, 0) * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0)))/((ISNULL(SalesDetailTable.TaxPercent, 0) / 100)) AS ExclusiveTaxAmount, SUM(ISNULL(SalesDetailTable.Qty, 0) * ISNULL(SalesDetailTable.Price, 0) - ISNULL(SalesDetailTable.SampleQty, 0) * ISNULL(SalesDetailTable.Price, 0)) AS NetAmount, SalesDetailTable.BatchNo, SalesDetailTable.ExpiryDate " & _
                   " FROM tblDefLocation INNER JOIN vwCOADetail INNER JOIN SalesMasterTable INNER JOIN SalesDetailTable ON SalesMasterTable.SalesId = SalesDetailTable.SalesId INNER JOIN ArticleDefView ON SalesDetailTable.ArticleDefId = ArticleDefView.ArticleId ON vwCOADetail.coa_detail_id = SalesMasterTable.CustomerCode ON tblDefLocation.location_id = SalesDetailTable.LocationId LEFT OUTER JOIN tblDefCostCenter ON SalesMasterTable.CostCenterId = tblDefCostCenter.CostCenterID LEFT OUTER JOIN CompanyDefTable ON SalesMasterTable.LocationId = CompanyDefTable.CompanyId  " & _
                   " WHERE  (Convert(varchar, SalesMasterTable.SalesDate,102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.Date.ToString("yyyy-M-d 23:59:59") & "', 102)) AND ArticleDefview.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & ""
                End If

                If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                    Str += " AND SalesMasterTable.CostCenterId  in (" & Me.lstCostCenter.SelectedIDs & ")"
                End If
                If Me.lstCompany.SelectedIDs.Length > 0 Then
                    Str += " AND SalesMasterTable.LocationId in (" & Me.lstCompany.SelectedIDs & ")"
                End If
                If Me.lstLocation.SelectedIDs.Length > 0 Then
                    Str += " AND SalesDetailTable.LocationId in (" & Me.lstLocation.SelectedIDs & ")"
                End If
                If Me.lstState.SelectedIDs.Length > 0 Then
                    Str += " AND vwCOADetail.StateId in (" & Me.lstState.SelectedIDs & ")"
                End If
                If Me.lstRegion.SelectedIDs.Length > 0 Then
                    Str += " AND vwCOADetail.RegionId  in (" & Me.lstRegion.SelectedIDs & ")"
                End If
                If Me.lstZone.SelectedIDs.Length > 0 Then
                    Str += " AND vwCOADetail.ZoneId in (" & Me.lstZone.SelectedIDs & ")"
                End If
                If Me.lstBelt.SelectedIDs.Length > 0 Then
                    Str += " AND vwCOADetail.BeltId in (" & Me.lstBelt.SelectedIDs & ")"
                End If
                If Me.lstTerritory.SelectedIDs.Length > 0 Then
                    Str += " AND vwCOADetail.TerritoryId in (" & Me.lstTerritory.SelectedIDs & ")"
                End If
                If Me.lstCity.SelectedIDs.Length > 0 Then
                    Str += " AND vwCOADetail.cityId in(" & Me.lstCity.SelectedIDs & ")"
                End If
                If Me.lstItemDepartment.SelectedIDs.Length > 0 Then
                    Str += " AND ArticleDefview.ArticleGroupId in (" & Me.lstItemDepartment.SelectedIDs & ")"
                End If
                If Me.lstItemSubCategory.SelectedIDs.Length > 0 Then
                    Str += " AND ArticleDefview.ArticleLPOId in (" & Me.lstItemSubCategory.SelectedIDs & ")"
                End If
                If Me.lstItemCategory.SelectedIDs.Length > 0 Then
                    Str += " AND ArticleDefview.ArticleCompanyId in (" & Me.lstItemCategory.SelectedIDs & ")"
                End If
                If Me.lstItemType.SelectedIDs.Length > 0 Then
                    Str += " AND ArticleDefView.ArticleTypeId in(" & Me.lstItemType.SelectedIDs & ")"
                End If
                If Me.lstItems.SelectedIDs.Length > 0 Then
                    Str += " AND SalesDetailTable.ArticleDefId in(" & Me.lstItems.SelectedIDs & ")"
                End If
                If Me.lstCustomer.SelectedIDs.Length > 0 Then
                    Str += " And vwCOADetail.coa_detail_id in (" & Me.lstCustomer.SelectedIDs & ")"
                End If
                If Me.lstDirector.SelectedIDs.Length > 0 Then
                    Str += " And vwCOADetail.Director in (" & Me.lstDirector.SelectedIDs & ")"
                End If
                If Me.lstSalesMan.SelectedIDs.Length > 0 Then
                    Str += " And vwCOADetail.SaleMan in (" & Me.lstSalesMan.SelectedIDs & ")"
                End If
                If Me.lstManager.SelectedIDs.Length > 0 Then
                    Str += " And vwCOADetail.Manager in (" & Me.lstManager.SelectedIDs & ")"
                End If
                Str += " GROUP BY SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, SalesDetailTable.Qty, SalesDetailTable.Price, ISNULL(SalesDetailTable.SEDPercent, 0), SalesDetailTable.BatchNo, ISNULL(SalesDetailTable.TaxPercent, 0), vwCOADetail.NTN_NO, vwCOADetail.detail_title, SalesDetailTable.SaleDetailId, vwCOADetail.CityName, vwCOADetail.TerritoryName, vwCOADetail.detail_code, vwCOADetail.StateName, vwCOADetail.CustomerType, vwCOADetail.SalesManName, vwCOADetail.ManagerName, vwCOADetail.DirectorName, vwCOADetail.Category, CompanyDefTable.CompanyName, tblDefCostCenter.Name, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleTypeName, ArticleDefView.ArticleGenderName, ArticleDefView.ArticleCompanyName, ArticleDefView.ArticleLpoName, ArticleDefView.ArticleColorName, ArticleDefView.ArticleSizeName, ArticleDefView.ArticleUnitName, ArticleDefView.ArticleGroupName, tblDefLocation.location_name, SalesDetailTable.ArticleSize, SalesDetailTable.Sz1, SalesDetailTable.Sz7, SalesDetailTable.PostDiscountPrice, SalesDetailTable.DiscountType, SalesDetailTable.DiscountValue, SalesDetailTable.DiscountFactor, SalesDetailTable.DiscountId, SalesDetailTable.ExpiryDate ORDER BY ArticleDefview.ArticleDescription "

                dt = GetDataTable(Str)

                dt.Columns("DiscRatio").Expression = "(1-(NetAmount/GrossAmount))*100"

                Dim frmReport As New frmGrdRptSalesSummaryandDetail(dt, "Sales Detail Report", DoHaveGridPrintRights, DoHaveGridExportRights, DoHaveGridFeildChosserRights)
                frmReport.ShowDialog()

            Else
                'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
                Str = "SELECT CompanyDefTable.CompanyName AS Company, tblDefCostCenter.Name AS Project, ISNULL(SalesOrderMasterTable.SalesOrderNo,'') AS SalesOrderNo, SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, vwCOADetail.detail_code AS [Account Code], vwCOADetail.detail_title AS [Account Title], vwCOADetail.Category AS [Customer Category], vwCOADetail.CustomerType, vwCOADetail.NTN_NO, vwCOADetail.StateName AS Province, vwCOADetail.CityName AS City, vwCOADetail.TerritoryName AS Territory, vwCOADetail.DirectorName AS Director, vwCOADetail.ManagerName AS Manager, vwCOADetail.SalesManName AS SalesMan, ISNULL(SalesMasterTable.SalesAmount, 0) AS [Sales Amount], ISNULL(AmountReceived.[Receipt Amount], 0) AS [Received Amount], ISNULL(SalesReturnMasterTable.SalesReturnAmount, 0) AS [Return Amount], ISNULL(SalesMasterTable.SalesAmount, 0) - ISNULL(AmountReceived.[Receipt Amount], 0) - ISNULL(SalesReturnMasterTable.SalesReturnAmount, 0) AS Balance, (CASE WHEN ((ISNULL(SalesMasterTable.SalesAmount, 0) - ISNULL(AmountReceived.[Receipt Amount], 0) = 0)) THEN ISNULL(DATEDIFF(DAY, SalesMasterTable.SalesDate, AmountReceived.[Receipt Date]), '') ELSE 0 END) AS [Cleared After Days] " & _
                        " FROM  tblDefCostCenter RIGHT OUTER JOIN SalesMasterTable LEFT OUTER JOIN SalesOrderMasterTable ON SalesMasterTable.POId = SalesOrderMasterTable.SalesOrderId ON tblDefCostCenter.CostCenterID = SalesMasterTable.CostCenterId LEFT OUTER JOIN CompanyDefTable ON SalesMasterTable.LocationId = CompanyDefTable.CompanyId LEFT OUTER JOIN SalesReturnMasterTable ON SalesMasterTable.SalesId = SalesReturnMasterTable.POId LEFT OUTER JOIN vwCOADetail ON SalesMasterTable.CustomerCode = vwCOADetail.coa_detail_id LEFT OUTER JOIN (SELECT InvoiceId, SUM([Receipt Amount]) AS [Receipt Amount], MIN([Received Date]) AS [Receipt Date] FROM (SELECT tblVoucherDetail.InvoiceId, SUM(ISNULL(tblVoucherDetail.credit_amount, 0)) AS [Receipt Amount], MIN(tblVoucher.voucher_date) AS [Received Date] FROM tblVoucherDetail INNER JOIN tblVoucher ON tblVoucherDetail.voucher_id = tblVoucher.voucher_id LEFT OUTER JOIN SalesMasterTable AS SalesMasterTable_2 ON tblVoucherDetail.InvoiceId = SalesMasterTable_2.SalesId WHERE (tblVoucherDetail.InvoiceId IS NOT NULL) AND (tblVoucher.voucher_type_id IN (3, 5)) GROUP BY tblVoucherDetail.InvoiceId  " & _
                        " UNION SELECT InvoiceBasedReceiptsDetails_1.InvoiceId, SUM(ISNULL(InvoiceBasedReceipts_1.ReceiptAmount, 0)) AS [Receipt Amount], MIN(InvoiceBasedReceipts_1.ReceiptDate) AS [Receipt Date] FROM InvoiceBasedReceipts AS InvoiceBasedReceipts_1 INNER JOIN InvoiceBasedReceiptsDetails AS InvoiceBasedReceiptsDetails_1 ON InvoiceBasedReceipts_1.ReceiptId = InvoiceBasedReceiptsDetails_1.ReceiptId LEFT OUTER JOIN SalesMasterTable AS SalesMasterTable_1 ON InvoiceBasedReceipts_1.ReceiptId = SalesMasterTable_1.SalesId WHERE (InvoiceBasedReceiptsDetails_1.InvoiceId IS NOT NULL) GROUP BY InvoiceBasedReceiptsDetails_1.InvoiceId) AS A GROUP BY InvoiceId) AS AmountReceived ON AmountReceived.InvoiceId = SalesMasterTable.SalesId " & _
                        " WHERE  (Convert(varchar, SalesMasterTable.SalesDate,102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.Date.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & ""

                If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                    Str += " AND SalesMasterTable.CostCenterId  in (" & Me.lstCostCenter.SelectedIDs & ")"
                End If
                If Me.lstCompany.SelectedIDs.Length > 0 Then
                    Str += " AND SalesMasterTable.LocationId in (" & Me.lstCompany.SelectedIDs & ")"
                End If
                'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
                'If Me.lstLocation.SelectedIDs.Length > 0 Then
                '    Str += " AND SalesDetailTable.LocationId in (" & Me.lstLocation.SelectedIDs & ")"
                'End If
                If Me.lstState.SelectedIDs.Length > 0 Then
                    Str += " AND vwCoaDetail.StateId in (" & Me.lstState.SelectedIDs & ")"
                End If
                If Me.lstRegion.SelectedIDs.Length > 0 Then
                    Str += " AND vwCoaDetail.RegionId  in (" & Me.lstRegion.SelectedIDs & ")"
                End If
                If Me.lstZone.SelectedIDs.Length > 0 Then
                    Str += " AND vwCoaDetail.ZoneId in (" & Me.lstZone.SelectedIDs & ")"
                End If
                If Me.lstBelt.SelectedIDs.Length > 0 Then
                    Str += " AND vwCoaDetail.BeltId in (" & Me.lstBelt.SelectedIDs & ")"
                End If
                If Me.lstTerritory.SelectedIDs.Length > 0 Then
                    Str += " AND vwCoaDetail.TerritoryId in (" & Me.lstTerritory.SelectedIDs & ")"
                End If
                If Me.lstCity.SelectedIDs.Length > 0 Then
                    Str += " AND vwCoaDetail.cityId in(" & Me.lstCity.SelectedIDs & ")"
                End If
                'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
                'If Me.lstItemDepartment.SelectedIDs.Length > 0 Then
                '    Str += " AND ArticleDefview.ArticleGroupId in (" & Me.lstItemDepartment.SelectedIDs & ")"
                'End If
                'If Me.lstItemSubCategory.SelectedIDs.Length > 0 Then
                '    Str += " AND ArticleDefview.ArticleLPOId in (" & Me.lstItemSubCategory.SelectedIDs & ")"
                'End If
                'If Me.lstItemCategory.SelectedIDs.Length > 0 Then
                '    Str += " AND ArticleDefview.ArticleCompanyId in (" & Me.lstItemCategory.SelectedIDs & ")"
                'End If
                'If Me.lstItemType.SelectedIDs.Length > 0 Then
                '    Str += " AND ArticleDefView.ArticleTypeId in(" & Me.lstItemType.SelectedIDs & ")"
                'End If
                'If Me.lstItems.SelectedIDs.Length > 0 Then
                '    Str += " AND SalesDetailTable.ArticleDefId in(" & Me.lstItems.SelectedIDs & ")"
                'End If
                If Me.lstCustomer.SelectedIDs.Length > 0 Then
                    Str += " And vwCOADetail.coa_detail_id in (" & Me.lstCustomer.SelectedIDs & ")"
                End If
                If Me.lstDirector.SelectedIDs.Length > 0 Then
                    Str += " And vwCOADetail.Director in (" & Me.lstDirector.SelectedIDs & ")"
                End If
                If Me.lstSalesMan.SelectedIDs.Length > 0 Then
                    Str += " And vwCOADetail.SaleMan in (" & Me.lstSalesMan.SelectedIDs & ")"
                End If
                If Me.lstManager.SelectedIDs.Length > 0 Then
                    Str += " And vwCOADetail.Manager in (" & Me.lstManager.SelectedIDs & ")"
                End If

                dt = GetDataTable(Str)
                Dim frmReport As New frmGrdRptSalesSummaryandDetail(dt, "Sales Customer Wise Detail Report", DoHaveGridPrintRights, DoHaveGridExportRights, DoHaveGridFeildChosserRights)
                frmReport.ShowDialog()

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FillGridForSalesSummary()
        Try
            If chkCustomerWise.Checked = False Then

                'Str = " SELECT        SalesMasterTable.SalesNo, SalesMasterTable.SalesDate,tblDefLocation.Location_Name, vwCOADetail.detail_code As [Account Code], vwCOADetail.detail_title As [Account Title], " _
                '      & " vwCOADetail.sub_sub_title, tblDefEmployee.Employee_Code, tblDefEmployee.Employee_Name, ISNULL(SalesMasterTable.SalesQty, 0) AS SalesQty, ISNULL(SalesMasterTable.SalesAmount, 0) AS SalesAmount, " _
                '      & "   (ISNULL(SalesMasterTable.SalesAmount, 0) + ISNULL(_Tax.Inc_Tax_Amount, 0) + ISNULL(SalesMasterTable.TransitInsurance, 0)) - (ISNULL(SalesMasterTable.FuelExpense, 0) " _
                '      & "       + ISNULL(SalesMasterTable.OtherExpense, 0) + ISNULL(SalesMasterTable.Adjustment, 0)) AS Net_Value, ISNULL(SalesMasterTable.TransitInsurance, 0) AS TransitInsurance, ISNULL(_Tax.Inc_Tax_Amount, 0) " _
                '      & "    AS Inc_Tax_Amount, ISNULL(SalesMasterTable.FuelExpense, 0) AS FuelExpense, ISNULL(SalesMasterTable.OtherExpense, 0) AS OtherExpense, ISNULL(SalesMasterTable.Adjustment, 0) AS Adjustment, " _
                '      & "   ISNULL(SalesMasterTable.DueDays, 0) AS DueDays, tblDefCostCenter.Name AS CostCenter,  CompanyDefTable.CompanyName, vwCOADetail.StateName, " _
                '      & "    vwCOADetail.CityName, vwCOADetail.TerritoryName, SalesMasterTable.Remarks, SalesMasterTable.UserName, SalesMasterTable.BiltyNo, " _
                '      & "   SalesOrderMasterTable.PONo, SalesOrderMasterTable.PO_Date, SalesOrderMasterTable.Terms_And_Condition, SalesMasterTable.InvoiceType, " _
                '      & "  vwCOADetail.CustomerType, vwCOADetail.account_type, vwCOADetail.CreditDays, vwCOADetail.Contact_Email, vwCOADetail.Contact_Address, vwCOADetail.Contact_Mobile, " _
                '      & " vwCOADetail.Contact_Phone, IsNull(_Weightage.[Weight], 0) As [Weight] " _
                '      & "  FROM vwCOADetail INNER JOIN " _
                '      & "  SalesMasterTable ON vwCOADetail.coa_detail_id = SalesMasterTable.CustomerCode LEFT OUTER JOIN " _
                '      & " SalesOrderMasterTable ON SalesMasterTable.POId = SalesOrderMasterTable.SalesOrderId LEFT OUTER JOIN " _
                '      & " SalesDetailTable  ON SalesDetailTable.SalesId  = SalesMasterTable.SalesId Inner JOIN " _
                '      & " ArticleDefView on ArticleDefView.ArticleId = SalesDetailTable.ArticleDefId LEFT OUTER JOIN " _
                '      & " CompanyDefTable ON SalesMasterTable.LocationId = CompanyDefTable.CompanyId LEFT OUTER JOIN " _
                '      & " tblDefLocation ON tblDefLocation.Location_Id = SalesDetailTable.LocationId LEFT OUTER JOIN  " _
                '      & " tblDefCostCenter ON SalesMasterTable.CostCenterId = tblDefCostCenter.CostCenterID LEFT OUTER JOIN " _
                '      & "  tblDefEmployee ON SalesMasterTable.EmployeeCode = tblDefEmployee.Employee_ID LEFT OUTER JOIN " _
                '      & " (SELECT        SalesId, SUM((ISNULL(TaxPercent, 0) / 100) * (ISNULL(Qty, 0) * ISNULL(Price, 0)) + (ISNULL(SEDPercent, 0) / 100) * (ISNULL(Qty, 0) * ISNULL(Price, 0))) AS Inc_Tax_Amount " _
                '      & " FROM SalesDetailTable " _
                '      & " GROUP BY SalesId  HAVING         (SUM((ISNULL(TaxPercent, 0) / 100) * (ISNULL(Qty, 0) * ISNULL(Price, 0)) + (ISNULL(SEDPercent, 0) / 100) * (ISNULL(Qty, 0) * ISNULL(Price, 0))) <> 0)) AS _Tax ON " _
                '      & "  _Tax.SalesId = SalesMasterTable.SalesId LEFT OUTER JOIN(Select SalesId,ArticleDefId, SalesDetailTable.LocationId , tblDefLocation.Location_Name ,ArticleDefview.ArticleGroupId,ArticleDefview.ArticleLPOId,ArticleDefview.ArticleCompanyId,ArticleDefView.ArticleTypeId,SUM((ISNULL(ArticleDefView.ItemWeight, 0) * ISNULL(Qty, 0))) As [Weight]  FROM SalesDetailTable LEFT OUTER JOIN ArticleDefView On SalesDetailTable.ArticleDefId = ArticleDefView.ArticleId  LEFT OUTER JOIN  tblDefLocation ON tblDefLocation.Location_Id = SalesDetailTable.LocationId Group by SalesId,Location_Name,SalesDetailTable.LocationId) " _
                '      & " As _Weightage ON SalesMasterTable.SalesId = _Weightage.SalesId " _
                '      & " where SalesMasterTable.SalesNo <> '' And (Convert(varchar, SalesMasterTable.SalesDate,102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.Date.ToString("yyyy-M-d 23:59:59") & "', 102)) "
                'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
                Str = "SELECT CompanyDefTable.CompanyName, tblDefCostCenter.Name AS Project, _ArticleLocaton.location_name AS Location, ISNULL(SalesOrderMasterTable.SalesOrderNo,'') AS SalesOrderNo, SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, vwCOADetail.detail_code AS [Account Code], vwCOADetail.detail_title AS [Account Title], vwCOADetail.Category AS [Customer Category], vwCOADetail.CustomerType, vwCOADetail.NTN_NO AS [NTN No], vwCOADetail.DirectorName AS Director, vwCOADetail.ManagerName AS Manager, vwCOADetail.SalesManName AS SalesMan, vwCOADetail.StateName AS Province, vwCOADetail.CityName AS City, vwCOADetail.TerritoryName AS Territory, ISNULL(SalesMasterTable.SalesQty, 0) AS SalesQty, ISNULL(SalesMasterTable.SalesAmount, 0) AS SalesAmount, ISNULL(SalesMasterTable.Adjustment, 0) AS AdjDiscount, (ISNULL(SalesMasterTable.TransitInsurance, 0) + ISNULL(SalesMasterTable.FuelExpense, 0) + ISNULL(SalesMasterTable.OtherExpense, 0)) AS OtherExpenses, (ISNULL(SalesMasterTable.SalesAmount, 0) + ISNULL(SalesMasterTable.TransitInsurance, 0) + ISNULL(SalesMasterTable.FuelExpense, 0) + (ISNULL(SalesMasterTable.OtherExpense, 0))) - ISNULL(SalesMasterTable.Adjustment, 0) AS Net_Value " _
                      & " FROM vwCOADetail INNER JOIN " _
                      & " SalesMasterTable ON vwCOADetail.coa_detail_id = SalesMasterTable.CustomerCode " _
                      & " LEFT OUTER JOIN  SalesOrderMasterTable ON SalesMasterTable.POId = SalesOrderMasterTable.SalesOrderId " _
                      & " LEFT OUTER JOIN  CompanyDefTable ON SalesMasterTable.LocationId = CompanyDefTable.CompanyId " _
                      & " LEFT OUTER JOIN   tblDefCostCenter ON SalesMasterTable.CostCenterId = tblDefCostCenter.CostCenterID " _
                      & " LEFT OUTER JOIN   tblDefEmployee ON SalesMasterTable.EmployeeCode = tblDefEmployee.Employee_ID " _
                      & " LEFT OUTER JOIN(Select SalesId ,SUM((ISNULL(ArticleDefView.ItemWeight, 0) * ISNULL(Qty, 0))) As [Weight]  FROM SalesDetailTable left outer join  ArticleDefView On ArticleDefView.ArticleId = SalesDetailTable.ArticleDefId " _
                      & " Group by SalesId)  As _Weightage ON SalesMasterTable.SalesId = _Weightage.SalesId " _
                      & " LEFT OUTER JOIN(Select SalesId,ArticleDefId,ArticleDefview.ArticleGroupId,   ArticleDefview.ArticleLPOId,ArticleDefview.ArticleCompanyId,ArticleDefView.ArticleTypeId  FROM SalesDetailTable left outer join  ArticleDefView On ArticleDefView.ArticleId = SalesDetailTable.ArticleDefId  " _
                      & " group by SalesId ,  ArticleDefId ,ArticleDefview.ArticleGroupId,ArticleDefview.ArticleLPOId,ArticleDefview.ArticleCompanyId,ArticleDefView.ArticleTypeId " _
                      & " )  As _Article ON SalesMasterTable.SalesId = _Article.SalesId " _
                      & " LEFT OUTER JOIN(Select SalesId ,SalesDetailTable.LocationId ,tblDefLocation.Location_Name FROM SalesDetailTable Inner JOIN  tblDefLocation ON  tblDefLocation.Location_Id = SalesDetailTable.LocationId " _
                      & " Group by SalesId ,LocationId ,Location_Name)  As _ArticleLocaton ON SalesMasterTable.SalesId = _ArticleLocaton.SalesId " _
                       & " where SalesMasterTable.SalesNo <> '' And (Convert(varchar, SalesMasterTable.SalesDate,102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.Date.ToString("yyyy-M-d 23:59:59") & "', 102)) "


                If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                    Str += " AND SalesMasterTable.CostCenterId  in (" & Me.lstCostCenter.SelectedIDs & ")"
                End If
                If Me.lstCompany.SelectedIDs.Length > 0 Then
                    Str += " AND SalesMasterTable.LocationId in (" & Me.lstCompany.SelectedIDs & ")"
                End If
                If Me.lstLocation.SelectedIDs.Length > 0 Then
                    Str += " AND _ArticleLocaton.LocationId in (" & Me.lstLocation.SelectedIDs & ")"
                End If
                If Me.lstState.SelectedIDs.Length > 0 Then
                    Str += " AND vwCoaDetail.StateId in (" & Me.lstState.SelectedIDs & ")"
                End If
                If Me.lstRegion.SelectedIDs.Length > 0 Then
                    Str += " AND vwCoaDetail.RegionId  in (" & Me.lstRegion.SelectedIDs & ")"
                End If
                If Me.lstZone.SelectedIDs.Length > 0 Then
                    Str += " AND vwCoaDetail.ZoneId in (" & Me.lstZone.SelectedIDs & ")"
                End If
                If Me.lstBelt.SelectedIDs.Length > 0 Then
                    Str += " AND vwCoaDetail.BeltId in (" & Me.lstBelt.SelectedIDs & ")"
                End If
                If Me.lstTerritory.SelectedIDs.Length > 0 Then
                    Str += " AND vwCoaDetail.TerritoryId in (" & Me.lstTerritory.SelectedIDs & ")"
                End If
                If Me.lstCity.SelectedIDs.Length > 0 Then
                    Str += " AND vwCoaDetail.cityId in(" & Me.lstCity.SelectedIDs & ")"
                End If
                If Me.lstItemDepartment.SelectedIDs.Length > 0 Then
                    Str += " AND _Article.ArticleGroupId in (" & Me.lstItemDepartment.SelectedIDs & ")"
                End If
                If Me.lstItemSubCategory.SelectedIDs.Length > 0 Then
                    Str += " AND _Article.ArticleLPOId in (" & Me.lstItemSubCategory.SelectedIDs & ")"
                End If
                If Me.lstItemCategory.SelectedIDs.Length > 0 Then
                    Str += " AND _Article.ArticleCompanyId in (" & Me.lstItemCategory.SelectedIDs & ")"
                End If
                If Me.lstItemType.SelectedIDs.Length > 0 Then
                    Str += " AND _Article.ArticleTypeId in(" & Me.lstItemType.SelectedIDs & ")"
                End If
                If Me.lstItems.SelectedIDs.Length > 0 Then
                    Str += " AND _Article.ArticleDefId in(" & Me.lstItems.SelectedIDs & ")"
                End If
                If Me.lstCustomer.SelectedIDs.Length > 0 Then
                    Str += " And vwCOADetail.coa_detail_id in (" & Me.lstCustomer.SelectedIDs & ")"
                End If
                Str += " group by CompanyDefTable.CompanyName, tblDefCostCenter.Name, _ArticleLocaton.location_name,SalesMasterTable.SalesNo, SalesMasterTable.SalesDate, vwCOADetail.detail_code,vwCOADetail.detail_title, vwCOADetail.Category, vwCOADetail.CustomerType, vwCOADetail.NTN_NO, vwCOADetail.DirectorName, vwCOADetail.ManagerName, vwCOADetail.SalesManName, vwCOADetail.StateName, vwCOADetail.CityName, vwCOADetail.TerritoryName, SalesQty,SalesAmount,SalesMasterTable.TransitInsurance,SalesMasterTable.FuelExpense,SalesMasterTable.OtherExpense,SalesMasterTable.Adjustment,TerritoryName, SalesOrderMasterTable.SalesOrderNo "
                dt = GetDataTable(Str)
                Dim frmReport As New frmGrdRptSalesSummaryandDetail(dt, "Sales Summary Report", DoHaveGridPrintRights, DoHaveGridExportRights, DoHaveGridFeildChosserRights)
                'frmReport.DoHaveGridExportRights = DoHaveGridExportRights
                'frmReport.DoHaveGridFeildChosserRights = DoHaveGridFeildChosserRights
                'frmReport.DoHaveGridPrintRights = DoHaveGridPrintRights
                'frmReport.formText = "Sales Summary Report"
                frmReport.ShowDialog()
            Else
                'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
                Str = "SELECT CompanyDefTable.CompanyName AS Company, tblDefCostCenter.Name AS Project, vwCOADetail.detail_code AS [Account Code], vwCOADetail.detail_title AS [Account Title], vwCOADetail.Category AS [Customer Category], ISNULL(vwCOADetail.CustomerType, '') AS CustomerType, vwCOADetail.NTN_NO, ISNULL(vwCOADetail.StateName, '') AS Province, ISNULL(vwCOADetail.CityName, '') AS City, ISNULL(vwCOADetail.TerritoryName, '') AS Territory, vwCOADetail.DirectorName AS Director, ISNULL(vwCOADetail.ManagerName, '') AS Manager, ISNULL(vwCOADetail.SalesManName, '') AS SaleMan, ISNULL(Count.SalesCount,0) AS [No of Invoices], SUM(ISNULL(SalesMasterTable.SalesAmount, 0)) AS [Total Amount], SUM(ISNULL(AmountReceived.[Receipt Amount], 0)) AS [Total Received], SUM(ISNULL(SalesReturnMasterTable.SalesReturnAmount, 0)) AS [Total Return], SUM(ISNULL(SalesMasterTable.SalesAmount, 0) - ISNULL(AmountReceived.[Receipt Amount], 0)) - SUM(ISNULL(SalesReturnMasterTable.SalesReturnAmount, 0)) AS Balance " & _
                            "FROM SalesMasterTable LEFT OUTER JOIN tblDefCostCenter ON SalesMasterTable.CostCenterId = tblDefCostCenter.CostCenterID LEFT OUTER JOIN CompanyDefTable ON SalesMasterTable.LocationId = CompanyDefTable.CompanyId LEFT OUTER JOIN SalesReturnMasterTable ON SalesMasterTable.SalesId = SalesReturnMasterTable.POId LEFT OUTER JOIN vwCOADetail ON SalesMasterTable.CustomerCode = vwCOADetail.coa_detail_id LEFT OUTER JOIN (SELECT COUNT(SalesId) AS SalesCount, CustomerCode FROM SalesMasterTable WHERE (Convert(varchar, SalesDate,102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.Date.ToString("yyyy-M-d 23:59:59") & "', 102)) GROUP BY CustomerCode) AS Count ON SalesMasterTable.CustomerCode = Count.CustomerCode LEFT OUTER JOIN (SELECT InvoiceId, SUM([Receipt Amount]) AS [Receipt Amount], MIN([Received Date]) AS [Receipt Date] FROM (SELECT tblVoucherDetail.InvoiceId, SUM(ISNULL(tblVoucherDetail.credit_amount, 0)) AS [Receipt Amount], MIN(tblVoucher.voucher_date) AS [Received Date] FROM tblVoucherDetail INNER JOIN tblVoucher ON tblVoucherDetail.voucher_id = tblVoucher.voucher_id LEFT OUTER JOIN SalesMasterTable AS SalesMasterTable_2 ON tblVoucherDetail.InvoiceId = SalesMasterTable_2.SalesId WHERE (tblVoucherDetail.InvoiceId IS NOT NULL) AND (tblVoucher.voucher_type_id IN (3, 5)) GROUP BY tblVoucherDetail.InvoiceId " & _
                            "UNION SELECT InvoiceBasedReceiptsDetails_1.InvoiceId, SUM(ISNULL(InvoiceBasedReceipts_1.ReceiptAmount, 0)) AS [Receipt Amount], MIN(InvoiceBasedReceipts_1.ReceiptDate) AS [Receipt Date] FROM InvoiceBasedReceipts AS InvoiceBasedReceipts_1 INNER JOIN InvoiceBasedReceiptsDetails AS InvoiceBasedReceiptsDetails_1 ON InvoiceBasedReceipts_1.ReceiptId = InvoiceBasedReceiptsDetails_1.ReceiptId LEFT OUTER JOIN SalesMasterTable AS SalesMasterTable_1 ON InvoiceBasedReceipts_1.ReceiptId = SalesMasterTable_1.SalesId WHERE (InvoiceBasedReceiptsDetails_1.InvoiceId IS NOT NULL) GROUP BY InvoiceBasedReceiptsDetails_1.InvoiceId) AS A GROUP BY InvoiceId) AS AmountReceived ON AmountReceived.InvoiceId = SalesMasterTable.SalesId " & _
                            "WHERE  (Convert(varchar, SalesMasterTable.SalesDate,102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.Date.ToString("yyyy-M-d 23:59:59") & "', 102)) " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & ""
                If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                    Str += " AND SalesMasterTable.CostCenterId  in (" & Me.lstCostCenter.SelectedIDs & ")"
                End If
                If Me.lstCompany.SelectedIDs.Length > 0 Then
                    Str += " AND SalesMasterTable.LocationId in (" & Me.lstCompany.SelectedIDs & ")"
                End If
                'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
                'If Me.lstLocation.SelectedIDs.Length > 0 Then
                '    Str += " AND SalesDetailTable.LocationId in (" & Me.lstLocation.SelectedIDs & ")"
                'End If
                If Me.lstState.SelectedIDs.Length > 0 Then
                    Str += " AND vwCoaDetail.StateId in (" & Me.lstState.SelectedIDs & ")"
                End If
                If Me.lstRegion.SelectedIDs.Length > 0 Then
                    Str += " AND vwCoaDetail.RegionId  in (" & Me.lstRegion.SelectedIDs & ")"
                End If
                If Me.lstZone.SelectedIDs.Length > 0 Then
                    Str += " AND vwCoaDetail.ZoneId in (" & Me.lstZone.SelectedIDs & ")"
                End If
                If Me.lstBelt.SelectedIDs.Length > 0 Then
                    Str += " AND vwCoaDetail.BeltId in (" & Me.lstBelt.SelectedIDs & ")"
                End If
                If Me.lstTerritory.SelectedIDs.Length > 0 Then
                    Str += " AND vwCoaDetail.TerritoryId in (" & Me.lstTerritory.SelectedIDs & ")"
                End If
                If Me.lstCity.SelectedIDs.Length > 0 Then
                    Str += " AND vwCoaDetail.cityId in(" & Me.lstCity.SelectedIDs & ")"
                End If
                'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
                'If Me.lstItemDepartment.SelectedIDs.Length > 0 Then
                '    Str += " AND ArticleDefview.ArticleGroupId in (" & Me.lstItemDepartment.SelectedIDs & ")"
                'End If
                'If Me.lstItemSubCategory.SelectedIDs.Length > 0 Then
                '    Str += " AND ArticleDefview.ArticleLPOId in (" & Me.lstItemSubCategory.SelectedIDs & ")"
                'End If
                'If Me.lstItemCategory.SelectedIDs.Length > 0 Then
                '    Str += " AND ArticleDefview.ArticleCompanyId in (" & Me.lstItemCategory.SelectedIDs & ")"
                'End If
                'If Me.lstItemType.SelectedIDs.Length > 0 Then
                '    Str += " AND ArticleDefView.ArticleTypeId in(" & Me.lstItemType.SelectedIDs & ")"
                'End If
                'If Me.lstItems.SelectedIDs.Length > 0 Then
                '    Str += " AND SalesDetailTable.ArticleDefId in(" & Me.lstItems.SelectedIDs & ")"
                'End If
                If Me.lstCustomer.SelectedIDs.Length > 0 Then
                    Str += " And vwCOADetail.coa_detail_id in (" & Me.lstCustomer.SelectedIDs & ")"
                End If
                If Me.lstDirector.SelectedIDs.Length > 0 Then
                    Str += " And vwCOADetail.Director in (" & Me.lstDirector.SelectedIDs & ")"
                End If
                If Me.lstSalesMan.SelectedIDs.Length > 0 Then
                    Str += " And vwCOADetail.SaleMan in (" & Me.lstSalesMan.SelectedIDs & ")"
                End If
                If Me.lstManager.SelectedIDs.Length > 0 Then
                    Str += " And vwCOADetail.Manager in (" & Me.lstManager.SelectedIDs & ")"
                End If
                Str += " GROUP BY vwCOADetail.detail_code, vwCOADetail.detail_title, vwCOADetail.CityName, vwCOADetail.CustomerType, vwCOADetail.SalesManName, vwCOADetail.ManagerName, vwCOADetail.TerritoryName, vwCOADetail.StateName, CompanyDefTable.CompanyName, tblDefCostCenter.Name, vwCOADetail.DirectorName, vwCOADetail.NTN_NO, vwCOADetail.Category, Count.SalesCount"

                dt = GetDataTable(Str)
                Dim frmReport As New frmGrdRptSalesSummaryandDetail(dt, "Sales Customer Wise Summary Report", DoHaveGridPrintRights, DoHaveGridExportRights, DoHaveGridFeildChosserRights)
                frmReport.ShowDialog()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FillGridForSalesReturnDetail()
        Try
            'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
            Str = "SELECT CompanyDefTable.CompanyName AS Company, tblDefCostCenter.Name AS Project, tblDefLocation.location_name AS Location, SalesReturnMasterTable.SalesReturnNo, SalesReturnMasterTable.SalesReturnDate, vwCOADetail.detail_code AS [Account Code], vwCOADetail.detail_title AS [Account Title], vwCOADetail.Category AS CustomerCategory, vwCOADetail.CustomerType, vwCOADetail.NTN_NO, vwCOADetail.StateName AS Province, vwCOADetail.CityName AS City, vwCOADetail.TerritoryName AS Territory, vwCOADetail.DirectorName AS Director, vwCOADetail.ManagerName AS Manager, vwCOADetail.SalesManName AS SalesMan, ArticleDefView.ArticleGroupName AS Department, ArticleDefView.ArticleTypeName AS Type, ArticleDefView.ArticleCompanyName AS Category, ArticleDefView.ArticleLpoName AS [Sub Category], ArticleDefView.ArticleGenderName AS Origin, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleSizeName AS Size, ArticleDefView.ArticleColorName AS Color, ArticleDefView.ArticleUnitName AS Unit, SalesReturnDetailTable.Price, SUM(SalesReturnDetailTable.Qty) AS Qty, SUM(ISNULL(SalesReturnDetailTable.Price, 0) * ISNULL(SalesReturnDetailTable.Qty, 0)) AS Amount, ISNULL(SalesReturnDetailTable.Tax_Percent, 0) AS GST, SUM((ISNULL(SalesReturnDetailTable.Tax_Percent, 0) / 100) * (ISNULL(SalesReturnDetailTable.Qty, 0) * ISNULL(SalesReturnDetailTable.Price, 0))) AS SalesTax, SUM(ISNULL(SalesReturnDetailTable.Price, 0) * ISNULL(SalesReturnDetailTable.Qty, 0)) + SUM((ISNULL(SalesReturnDetailTable.Tax_Percent, 0) / 100) * (ISNULL(SalesReturnDetailTable.Qty, 0) * ISNULL(SalesReturnDetailTable.Price, 0))) AS NetAmount  " _
                     & " FROM SalesReturnMasterTable INNER JOIN SalesReturnDetailTable ON SalesReturnMasterTable.SalesReturnId = SalesReturnDetailTable.SalesReturnId INNER JOIN ArticleDefView ON SalesReturnDetailTable.ArticleDefId = ArticleDefView.ArticleId INNER JOIN vwCOADetail ON SalesReturnMasterTable.CustomerCode = vwCOADetail.coa_detail_id INNER JOIN tblDefLocation ON tblDefLocation.location_id = SalesReturnDetailTable.LocationId INNER JOIN CompanyDefTable ON SalesReturnMasterTable.LocationId = CompanyDefTable.CompanyId LEFT OUTER JOIN tblDefCostCenter ON SalesReturnMasterTable.CostCenterId = tblDefCostCenter.CostCenterID " _
                     & " WHERE  (Convert(varchar, SalesReturnMasterTable.SalesReturnDate,102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.Date.ToString("dd/MMM/yyyy") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.Date.ToString("dd/MMM/yyyy") & "', 102)) AND ArticleDefView.Active=1 " & IIf(flgCompanyRights = True, " AND vwCOADetail.CompanyId=" & MyCompanyId & "", "") & ""
            If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                Str += " AND SalesReturnMasterTable.CostCenterId  in (" & Me.lstCostCenter.SelectedIDs & ")"
            End If
            If Me.lstCompany.SelectedIDs.Length > 0 Then
                Str += " AND SalesReturnMasterTable.LocationId in (" & Me.lstCompany.SelectedIDs & ")"
            End If
            If Me.lstLocation.SelectedIDs.Length > 0 Then
                Str += " AND SalesReturnDetailTable.LocationId in (" & Me.lstLocation.SelectedIDs & ")"
            End If
            If Me.lstState.SelectedIDs.Length > 0 Then
                Str += " AND vwCoaDetail.StateId in (" & Me.lstState.SelectedIDs & ")"
            End If
            If Me.lstRegion.SelectedIDs.Length > 0 Then
                Str += " AND vwCoaDetail.RegionId  in (" & Me.lstRegion.SelectedIDs & ")"
            End If
            If Me.lstZone.SelectedIDs.Length > 0 Then
                Str += " AND vwCoaDetail.ZoneId in (" & Me.lstZone.SelectedIDs & ")"
            End If
            If Me.lstBelt.SelectedIDs.Length > 0 Then
                Str += " AND vwCoaDetail.BeltId in (" & Me.lstBelt.SelectedIDs & ")"
            End If
            If Me.lstTerritory.SelectedIDs.Length > 0 Then
                Str += " AND vwCoaDetail.TerritoryId in (" & Me.lstTerritory.SelectedIDs & ")"
            End If
            If Me.lstCity.SelectedIDs.Length > 0 Then
                Str += " AND vwCoaDetail.cityId in(" & Me.lstCity.SelectedIDs & ")"
            End If
            If Me.lstItemDepartment.SelectedIDs.Length > 0 Then
                Str += " AND ArticleDefview.ArticleGroupId in (" & Me.lstItemDepartment.SelectedIDs & ")"
            End If
            If Me.lstItemSubCategory.SelectedIDs.Length > 0 Then
                Str += " AND ArticleDefview.ArticleLPOId in (" & Me.lstItemSubCategory.SelectedIDs & ")"
            End If
            If Me.lstItemCategory.SelectedIDs.Length > 0 Then
                Str += " AND ArticleDefview.ArticleCompanyId in (" & Me.lstItemCategory.SelectedIDs & ")"
            End If
            If Me.lstItemType.SelectedIDs.Length > 0 Then
                Str += " AND ArticleDefView.ArticleTypeId in(" & Me.lstItemType.SelectedIDs & ")"
            End If
            If Me.lstItems.SelectedIDs.Length > 0 Then
                Str += " AND SalesReturnDetailTable.ArticleDefId in(" & Me.lstItems.SelectedIDs & ")"
            End If
            If Me.lstCustomer.SelectedIDs.Length > 0 Then
                Str += " And vwCOADetail.coa_detail_id in (" & Me.lstCustomer.SelectedIDs & ")"
            End If
            If Me.lstDirector.SelectedIDs.Length > 0 Then
                Str += " And vwCOADetail.Director in (" & Me.lstDirector.SelectedIDs & ")"
            End If
            If Me.lstSalesMan.SelectedIDs.Length > 0 Then
                Str += " And vwCOADetail.SaleMan in (" & Me.lstSalesMan.SelectedIDs & ")"
            End If
            If Me.lstManager.SelectedIDs.Length > 0 Then
                Str += " And vwCOADetail.Manager in (" & Me.lstManager.SelectedIDs & ")"
            End If

            Str += " GROUP BY vwCOADetail.detail_title, SalesReturnMasterTable.SalesReturnDate, SalesReturnMasterTable.SalesReturnNo, SalesReturnDetailTable.Price, SalesReturnDetailTable.SalesReturnDetailId, SalesReturnDetailTable.Qty, vwCOADetail.NTN_NO, tblDefLocation.location_name, ISNULL(SalesReturnDetailTable.Tax_Percent, 0), vwCOADetail.CityName, vwCOADetail.TerritoryName, CompanyDefTable.CompanyName, tblDefCostCenter.Name, vwCOADetail.detail_code, vwCOADetail.StateName, vwCOADetail.DirectorName, vwCOADetail.ManagerName, vwCOADetail.SalesManName, vwCOADetail.Category, vwCOADetail.CustomerType, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, ArticleDefView.ArticleGenderName, ArticleDefView.ArticleTypeName, ArticleDefView.ArticleCompanyName, ArticleDefView.ArticleLpoName, ArticleDefView.ArticleSizeName, ArticleDefView.ArticleColorName, ArticleDefView.ArticleUnitName, ArticleDefView.ArticleGroupName"

            dt = GetDataTable(Str)
            Dim frmReport As New frmGrdRptSalesSummaryandDetail(dt, "Sales Return Detail Report", DoHaveGridPrintRights, DoHaveGridExportRights, DoHaveGridFeildChosserRights)
            'frmReport.DoHaveGridExportRights = DoHaveGridExportRights
            'frmReport.DoHaveGridFeildChosserRights = DoHaveGridFeildChosserRights
            'frmReport.DoHaveGridPrintRights = DoHaveGridPrintRights
            'frmReport.formText = "Sales Return Detail Report"
            frmReport.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FillGridForSalesReturnSummary()
        Try
            'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
            Str = "SELECT SalesReturn.CompanyName AS Company, SalesReturn.location_name AS Location, SalesReturn.Name AS Project, SalesReturn.SalesReturnNo, SalesReturn.SalesReturnDate, vwCOADetail.detail_code AS [Account Code], vwCOADetail.detail_title AS [Account Title], vwCOADetail.Category AS [Customer Category], vwCOADetail.CustomerType, vwCOADetail.NTN_NO AS [NTN No], vwCOADetail.StateName AS Province, b.RegionName AS Region, c.ZoneName AS Zone, d.BeltName AS Belt, vwCOADetail.CityName AS City, vwCOADetail.TerritoryName AS Territory, vwCOADetail.DirectorName AS Director, vwCOADetail.ManagerName AS Manager, vwCOADetail.SalesManName AS SalesMan, SalesReturn.ReturnQty, SalesReturn.Amount AS ReturnAmount " _
                  & " From vwCoaDetail " _
                  & " left join tblcustomer a on a.accountid = vwCoaDetail.coa_detail_id " _
                  & " left join tblListState AS [State] ON State.StateId= vwCOADetail.StateId " _
                  & " left join tbllistregion b on b.regionid = vwCOADetail.RegionId  " _
                  & " left join tbllistzone c on c.zoneid = vwCOADetail.ZoneId  " _
                  & " left join tbllistbelt d on d.beltid =  vwCOADetail.BeltId  " _
                  & " left join tblListCity e on   e.CityId = vwCOADetail.CityId  " _
                  & " left join tblListTerritory f on f.TerritoryId = vwCOADetail.TerritoryId  " _
                  & " LEFT OUTER JOIN( " _
                  & " SELECT     SalesReturnMasterTable.CustomerCode, SalesReturnMasterTable.SalesReturnNo, SalesReturnMasterTable.SalesReturnDate, SalesReturnDetailTable.SalesReturnId, " _
                  & " SUM(ISNULL(SalesReturnDetailTable.Qty, 0) * ISNULL(SalesReturnDetailTable.Price, 0)) AS Amount, SUM(ISNULL(SalesReturnDetailTable.Qty, 0) * ISNULL(SalesReturnDetailTable.CurrentPrice, 0) - ISNULL(SalesReturnDetailTable.Qty, 0) * ISNULL(SalesReturnDetailTable.Price, 0)) AS MC, SUM(SalesReturnDetailTable.Qty) AS ReturnQty, " _
                  & " SUM(ISNULL(ArticleDefTable.ItemWeight, 0) * ISNULL(SalesReturnDetailTable.Qty, 0)) AS Weight, SalesReturnMasterTable.CostCenterId ,tblDefCostCenter.Name, CompanyDefTable.CompanyName, tblDefLocation.location_name " _
                  & " FROM         SalesReturnDetailTable INNER JOIN " _
                  & " SalesReturnMasterTable ON SalesReturnDetailTable.SalesReturnId = SalesReturnMasterTable.SalesReturnId LEFT OUTER JOIN " _
                  & " ArticleDefTable ON SalesReturnDetailTable.ArticleDefId = ArticleDefTable.ArticleId " _
                  & " LEFT OUTER JOIN   CompanyDefTable ON SalesReturnMasterTable.LocationId = CompanyDefTable.CompanyId " _
                  & " LEFT OUTER JOIN tblDefCostCenter ON SalesReturnMasterTable.CostCenterId = tblDefCostCenter.CostCenterID " _
                  & " Left join ArticleDefview on ArticleDefview.ArticleId = ArticleDefTable.ArticleId " _
                  & " left join ArticleCompanyDefTable on ArticleDefview.ArticleCompanyId = ArticleCompanyDefTable.ArticleCompanyId " _
                  & " left join ArticleLpoDefTable on ArticleDefview.ArticleLPOId = ArticleLpoDefTable.ArticleLpoId " _
                  & " left join ArticleGroupDefTable on  ArticleDefview.ArticleGroupId = ArticleGroupDefTable.ArticleGroupId " _
                  & " left join tblDefLocation on SalesReturnDetailTable.LocationId = tblDefLocation.location_id " _
                  & " where SalesReturnMasterTable.SalesReturnNo <> '' And (Convert(varchar, SalesReturnMasterTable.SalesReturnDate,102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.Date.ToString("yyyy-M-d 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                Str += " AND SalesReturnMasterTable.CostCenterId in (" & Me.lstCostCenter.SelectedIDs & ")"
            End If
            If Me.lstCompany.SelectedIDs.Length > 0 Then
                Str += " AND SalesReturnMasterTable.LocationId in (" & Me.lstCompany.SelectedIDs & ")"
            End If
            If Me.lstLocation.SelectedIDs.Length > 0 Then
                Str += " AND SalesReturnDetailTable.LocationId in (" & Me.lstLocation.SelectedIDs & ")"
            End If
            If Me.lstItemDepartment.SelectedIDs.Length > 0 Then
                Str += " AND ArticleDefview.ArticleGroupId in (" & Me.lstItemDepartment.SelectedIDs & ")"
            End If
            If Me.lstItemSubCategory.SelectedIDs.Length > 0 Then
                Str += " AND ArticleDefview.ArticleLPOId in (" & Me.lstItemSubCategory.SelectedIDs & ")"
            End If
            If Me.lstItemCategory.SelectedIDs.Length > 0 Then
                Str += " AND ArticleDefview.ArticleCompanyId in (" & Me.lstItemCategory.SelectedIDs & ")"
            End If
            If Me.lstItemType.SelectedIDs.Length > 0 Then
                Str += " AND ArticleDefView.ArticleTypeId in(" & Me.lstItemType.SelectedIDs & ")"
            End If
            If Me.lstItems.SelectedIDs.Length > 0 Then
                Str += " AND SalesReturnDetailTable.ArticleDefId in(" & Me.lstItems.SelectedIDs & ")"
            End If
            Str += " GROUP BY SalesReturnMasterTable.CustomerCode, SalesReturnMasterTable.SalesReturnNo, SalesReturnMasterTable.SalesReturnDate, SalesReturnDetailTable.SalesReturnId, " _
                   & " SalesReturnMasterTable.SalesReturnAmount, SalesReturnMasterTable.CostCenterId,tblDefCostCenter.Name, CompanyDefTable.CompanyName, tblDefLocation.location_name)SalesReturn on SalesReturn.CustomerCode = vwCoaDetail.coa_detail_id " _
                   & " WHERE SalesReturnNo <> ''"
            If Me.lstState.SelectedIDs.Length > 0 Then
                Str += " AND State.StateId in (" & Me.lstState.SelectedIDs & ")"
            End If
            If Me.lstRegion.SelectedIDs.Length > 0 Then
                Str += " AND b.regionid in (" & Me.lstRegion.SelectedIDs & ")"
            End If
            If Me.lstZone.SelectedIDs.Length > 0 Then
                Str += " AND c.zoneid in (" & Me.lstZone.SelectedIDs & ")"
            End If
            If Me.lstBelt.SelectedIDs.Length > 0 Then
                Str += " AND d.beltid in (" & Me.lstBelt.SelectedIDs & ")"
            End If
            If Me.lstCity.SelectedIDs.Length > 0 Then
                Str += " AND e.CityId in(" & Me.lstCity.SelectedIDs & ")"
            End If
            If Me.lstTerritory.SelectedIDs.Length > 0 Then
                Str += " AND f.TerritoryId in(" & Me.lstTerritory.SelectedIDs & ")"
            End If
            If Me.lstCustomer.SelectedIDs.Length > 0 Then
                Str += " And vwCOADetail.coa_detail_id in (" & Me.lstCustomer.SelectedIDs & ")"
            End If
            Str += " ORDER BY detail_title ASC "
            dt = GetDataTable(Str)
            Dim frmReport As New frmGrdRptSalesSummaryandDetail(dt, "Sales Return Summary Report", DoHaveGridPrintRights, DoHaveGridExportRights, DoHaveGridFeildChosserRights)
            'frmReport.DoHaveGridExportRights = DoHaveGridExportRights
            'frmReport.DoHaveGridFeildChosserRights = DoHaveGridFeildChosserRights
            'frmReport.DoHaveGridPrintRights = DoHaveGridPrintRights
            'frmReport.formText = "Sales Return Summary Report"
            frmReport.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnComapny_Click(sender As Object, e As EventArgs) Handles btnComapny.Click
        Try
            Me.UltraTabControl2.SelectedTab = UltraTabPageControl2.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnArea_Click(sender As Object, e As EventArgs) Handles btnArea.Click
        Try
            Me.UltraTabControl2.SelectedTab = UltraTabPageControl3.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnCustomer_Click(sender As Object, e As EventArgs) Handles btnCustomer.Click
        Try
            Me.UltraTabControl2.SelectedTab = Me.UltraTabPageControl4.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSalesMan_Click(sender As Object, e As EventArgs) Handles btnSalesMan.Click
        Try
            Me.UltraTabControl2.SelectedTab = Me.UltraTabPageControl5.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnItemPre_Click(sender As Object, e As EventArgs) Handles btnItemPre.Click
        Try
            Me.UltraTabControl2.SelectedTab = Me.UltraTabPageControl4.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSalesManPre_Click(sender As Object, e As EventArgs) Handles btnSalesManPre.Click
        Try
            Me.UltraTabControl2.SelectedTab = Me.UltraTabPageControl3.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnCustomerPre_Click(sender As Object, e As EventArgs) Handles btnCustomerPre.Click
        Try
            Me.UltraTabControl2.SelectedTab = Me.UltraTabPageControl2.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnAreaPre_Click(sender As Object, e As EventArgs) Handles btnAreaPre.Click
        Try
            Me.UltraTabControl2.SelectedTab = Me.UltraTabPageControl1.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnShow_Click(sender As Object, e As EventArgs) Handles btnShow.Click
        Try
            If rdSales.Checked = True Then
                If rbDetail.Checked = True Then
                    FillGridForSalesDetail()
                Else
                    FillGridForSalesSummary()
                End If
            Else
                If rbDetail.Checked = True Then
                    FillGridForSalesReturnDetail()
                Else
                    FillGridForSalesReturnSummary()
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtSearchCustomer_TextChanged(sender As Object, e As EventArgs) Handles txtSearchCustomer.TextChanged
        Try
            If txtSearchCustomer.Text <> "" Then
                dvCustomer.RowFilter = "Customer_Name LIKE '%" & Me.txtSearchCustomer.Text & "%' "
            End If
            Me.lstCustomer.ListItem.DataSource = dvCustomer
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub lstItemDepartment_SelectedIndexChaned(sender As Object, e As IndexEventArgs) Handles lstItemDepartment.SelectedIndexChaned
        Try
            Str = "Select ArticleId , ArticleCode + ' ~ ' + ArticleDescription + '~' + ArticleSizeName + '~' + ArticleColorName Item_Name from ArticleDefView where 1 = 1"
            If Me.lstItemDepartment.SelectedIDs.Length > 0 Then
                Str += " AND ArticleDefview.ArticleGroupId in (" & Me.lstItemDepartment.SelectedIDs & ")"
            End If
            If Me.lstItemSubCategory.SelectedIDs.Length > 0 Then
                Str += " AND ArticleDefview.ArticleLPOId in (" & Me.lstItemSubCategory.SelectedIDs & ")"
            End If
            If Me.lstItemCategory.SelectedIDs.Length > 0 Then
                Str += " AND ArticleDefview.ArticleCompanyId in (" & Me.lstItemCategory.SelectedIDs & ")"
            End If
            If Me.lstItemType.SelectedIDs.Length > 0 Then
                Str += " AND ArticleDefView.ArticleTypeId in(" & Me.lstItemType.SelectedIDs & ")"
            End If
            Str += " order by ArticleId desc "
        dtItem = GetDataTable(Str)
        dtItem.TableName = "Item"
        dvItem.Table = dtItem
            FillListBox(Me.lstItems.ListItem, Str)
            Me.lstItems.DeSelect()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lstItemType_SelectedIndexChaned(sender As Object, e As IndexEventArgs) Handles lstItemType.SelectedIndexChaned
        Try
            Str = "Select ArticleId , ArticleCode + ' ~ ' + ArticleDescription + '~' + ArticleSizeName + '~' + ArticleColorName Item_Name from ArticleDefView  where 1 = 1 "
            If Me.lstItemDepartment.SelectedIDs.Length > 0 Then
                Str += " AND ArticleDefview.ArticleGroupId in (" & Me.lstItemDepartment.SelectedIDs & ")"
            End If
            If Me.lstItemSubCategory.SelectedIDs.Length > 0 Then
                Str += " AND ArticleDefview.ArticleLPOId in (" & Me.lstItemSubCategory.SelectedIDs & ")"
            End If
            If Me.lstItemCategory.SelectedIDs.Length > 0 Then
                Str += " AND ArticleDefview.ArticleCompanyId in (" & Me.lstItemCategory.SelectedIDs & ")"
            End If
            If Me.lstItemType.SelectedIDs.Length > 0 Then
                Str += " AND ArticleDefView.ArticleTypeId in(" & Me.lstItemType.SelectedIDs & ")"
            End If
            Str += " order by ArticleId desc "
            dtItem = GetDataTable(Str)
            dtItem.TableName = "Item"
            dvItem.Table = dtItem
            FillListBox(Me.lstItems.ListItem, Str)
            Me.lstItems.DeSelect()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lstItemSubCategory_SelectedIndexChaned(sender As Object, e As IndexEventArgs) Handles lstItemSubCategory.SelectedIndexChaned
        Try
            Str = "Select ArticleId , ArticleCode + ' ~ ' + ArticleDescription + '~' + ArticleSizeName + '~' + ArticleColorName Item_Name from ArticleDefView where 1=1"
            If Me.lstItemDepartment.SelectedIDs.Length > 0 Then
                Str += " AND ArticleDefview.ArticleGroupId in (" & Me.lstItemDepartment.SelectedIDs & ")"
            End If
            If Me.lstItemSubCategory.SelectedIDs.Length > 0 Then
                Str += " AND ArticleDefview.ArticleLPOId in (" & Me.lstItemSubCategory.SelectedIDs & ")"
            End If
            If Me.lstItemCategory.SelectedIDs.Length > 0 Then
                Str += " AND ArticleDefview.ArticleCompanyId in (" & Me.lstItemCategory.SelectedIDs & ")"
            End If
            If Me.lstItemType.SelectedIDs.Length > 0 Then
                Str += " AND ArticleDefView.ArticleTypeId in(" & Me.lstItemType.SelectedIDs & ")"
            End If
            Str += " order by ArticleId desc "
            dtItem = GetDataTable(Str)
            dtItem.TableName = "Item"
            dvItem.Table = dtItem
            FillListBox(Me.lstItems.ListItem, Str)
            Me.lstItems.DeSelect()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lstItemCategory_SelectedIndexChaned(sender As Object, e As IndexEventArgs) Handles lstItemCategory.SelectedIndexChaned
        Try

            Str = "SELECT dbo.ArticleLpoDefTable.ArticleLpoId AS ID, dbo.ArticleLpoDefTable.ArticleLpoName AS Name, ArticleLpoDefTable.ArticleCompanyId , dbo.ArticleLpoDefTable.SubCategoryCode  FROM dbo.ArticleLpoDefTable INNER JOIN dbo.ArticleCompanyDefTable ON dbo.ArticleLpoDefTable.ArticleCompanyId = dbo.ArticleCompanyDefTable.ArticleCompanyId where ArticleCompanyDefTable.ArticleCompanyId in (" & Me.lstItemCategory.SelectedIDs & ")"
            FillListBox(Me.lstItemSubCategory.ListItem, Str)
            Str = "Select ArticleId , ArticleCode + ' ~ ' + ArticleDescription + '~' + ArticleSizeName + '~' + ArticleColorName Item_Name from ArticleDefView where 1=1 "
            If Me.lstItemDepartment.SelectedIDs.Length > 0 Then
                Str += " AND ArticleDefview.ArticleGroupId in (" & Me.lstItemDepartment.SelectedIDs & ")"
            End If
            If Me.lstItemSubCategory.SelectedIDs.Length > 0 Then
                Str += " AND ArticleDefview.ArticleLPOId in (" & Me.lstItemSubCategory.SelectedIDs & ")"
            End If
            If Me.lstItemCategory.SelectedIDs.Length > 0 Then
                Str += " AND ArticleDefview.ArticleCompanyId in (" & Me.lstItemCategory.SelectedIDs & ")"
            End If
            If Me.lstItemType.SelectedIDs.Length > 0 Then
                Str += " AND ArticleDefView.ArticleTypeId in(" & Me.lstItemType.SelectedIDs & ")"
            End If
            Str += " order by ArticleId desc "
            dtItem = GetDataTable(Str)
            dtItem.TableName = "Item"
            dvItem.Table = dtItem
            FillListBox(Me.lstItems.ListItem, Str)
            Me.lstItems.DeSelect()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(sender As Object, e As EventArgs) Handles btnRefresh.Click
        Try
            Me.cmbPeriod.Text = "Current Month"
            If Not GetConfigValue("CompanyRights").ToString = "Error" Then
                flgCompanyRights = GetConfigValue("CompanyRights")
            End If
            GetSecurityRights()
            FillCombos()
            Me.lstCompany.DeSelect()
            Me.lstCostCenter.DeSelect()
            Me.lstCostCenterHead.DeSelect()
            Me.lstCity.DeSelect()
            Me.lstCustomer.DeSelect()
            Me.lstCustomerCategory.DeSelect()
            Me.lstCustomerType.DeSelect()
            Me.lstBelt.DeSelect()
            Me.lstZone.DeSelect()
            Me.lstRegion.DeSelect()
            Me.lstState.DeSelect()
            Me.lstDirector.DeSelect()
            Me.lstSalesMan.DeSelect()
            Me.lstManager.DeSelect()
            Me.lstItems.DeSelect()
            Me.lstTerritory.DeSelect()
            Me.lstLocation.DeSelect()
            Me.lstItemCategory.DeSelect()
            Me.lstItemDepartment.DeSelect()
            Me.lstItemSubCategory.DeSelect()
            Me.lstItemType.DeSelect()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.btnShow.Enabled = True
                DoHaveGridPrintRights = True
                DoHaveGridExportRights = True
                DoHaveGridFeildChosserRights = True
                Exit Sub
            End If
            Me.Visible = False
            Me.btnShow.Enabled = False
            DoHaveGridPrintRights = False
            DoHaveGridExportRights = False
            DoHaveGridFeildChosserRights = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Show" Then
                    Me.btnShow.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "GridPrint" Then
                    DoHaveGridPrintRights = True
                ElseIf Rights.Item(i).FormControlName = "GridExport" Then
                    DoHaveGridExportRights = True
                ElseIf Rights.Item(i).FormControlName = "GridFeildChosser" Then
                    DoHaveGridFeildChosserRights = True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub lstCustomerCategory_SelectedIndexChaned(sender As Object, e As IndexEventArgs) Handles lstCustomerCategory.SelectedIndexChaned
        Try
            If Me.lstCustomerCategory.SelectedIDs.Length > 0 Then
                FillListBox(Me.lstCustomerType.ListItem, "SELECT * FROM dbo.TblDefCustomerType where SubCustomerTypeId in (" & Me.lstCustomerCategory.SelectedIDs & ")")
                Me.lstCustomerType.DeSelect()
                Str = "Select coa_detail_id, detail_title + '~' +detail_code as Customer_Name from vwCOADetail WHERE detail_title <> '' " & IIf(flgCompanyRights = True, " AND CompanyId=" & MyCompanyId & "", "") & " "
                If Not getConfigValueByType("Show Vendor On Sales") = "True" Then
                    Str += " AND (Account_Type = 'Customer')  "
                Else
                    Str += " AND (Account_Type in('Customer','Vendor'))  "
                End If

                Str += " And CustomerTypeID in (SELECT Typeid  FROM dbo.TblDefCustomerType where SubCustomerTypeId in (" & Me.lstCustomerCategory.SelectedIDs & ") ) "

                Str += " ORDER BY 2 ASC "
                dtCustomer = GetDataTable(Str)
                dtCustomer.TableName = "Customer"
                dvCustomer.Table = dtCustomer
                FillListBox(Me.lstCustomer.ListItem, Str)
                Me.lstCustomer.DeSelect()
            Else
                FillListBox(Me.lstCustomerType.ListItem, "Select * From dbo.TblDefCustomerType ")
                Me.lstCustomerType.DeSelect()
                Str = "Select coa_detail_id, detail_title + '~' +detail_code as Customer_Name from vwCOADetail WHERE detail_title <> '' " & IIf(flgCompanyRights = True, " AND CompanyId=" & MyCompanyId & "", "") & " "
                If Not getConfigValueByType("Show Vendor On Sales") = "True" Then
                    Str += " AND (Account_Type = 'Customer')  "
                Else
                    Str += " AND (Account_Type in('Customer','Vendor'))  "
                End If
                Str += " ORDER BY 2 ASC "
                dtCustomer = GetDataTable(Str)
                dtCustomer.TableName = "Customer"
                dvCustomer.Table = dtCustomer
                FillListBox(Me.lstCustomer.ListItem, Str)
                Me.lstCustomer.DeSelect()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub lstCustomerType_SelectedIndexChaned(sender As Object, e As IndexEventArgs) Handles lstCustomerType.SelectedIndexChaned
        Try

            Str = "Select coa_detail_id, detail_title + '~' +detail_code as Customer_Name from vwCOADetail WHERE detail_title <> '' " & IIf(flgCompanyRights = True, " AND CompanyId=" & MyCompanyId & "", "") & " "
            If Not getConfigValueByType("Show Vendor On Sales") = "True" Then
                Str += " AND (Account_Type = 'Customer')  "
            Else
                Str += " AND (Account_Type in('Customer','Vendor'))  "
            End If
            If Me.lstCustomerType.SelectedIDs.Length > 0 Then
                Str += " And CustomerTypeID in (" & Me.lstCustomerType.SelectedIDs & ") "
            End If
            Str += " ORDER BY 2 ASC "
            dtCustomer = GetDataTable(Str)
            dtCustomer.TableName = "Customer"
            dvCustomer.Table = dtCustomer
            FillListBox(Me.lstCustomer.ListItem, Str)
            Me.lstCustomer.DeSelect()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
    Private Sub rdSales_CheckedChanged(sender As Object, e As EventArgs) Handles rdSales.CheckedChanged
        Try
            If rdSales.Checked = True Then
                Me.chkCustomerWise.Visible = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
    Private Sub rdSalesturn_CheckedChanged(sender As Object, e As EventArgs) Handles rdSalesReturn.CheckedChanged
        Try
            If rdSalesReturn.Checked = True Then
                Me.chkCustomerWise.Visible = False
            Else
                Me.chkCustomerWise.Visible = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Ali Faisal : UDL : Changes for Reports and other for UDL on 14-16 Nov 2018.
    Private Sub chkCustomerWise_CheckedChanged(sender As Object, e As EventArgs) Handles chkCustomerWise.CheckedChanged
        Try
            If Me.chkCustomerWise.Checked = True Then
                Me.rdSalesReturn.Visible = False
            Else
                Me.rdSalesReturn.Visible = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
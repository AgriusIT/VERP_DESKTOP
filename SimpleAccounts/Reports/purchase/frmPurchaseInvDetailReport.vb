''TFS4584 : Ayesha Rehman : 22-10-2018 : Purchase and Purchase Return Detail and Summary Report.
Public Class frmPurchaseInvDetailReport

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
            Me.cmbPeriod.Text = "Today"
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

            Me.lstCustomerType.DeSelect()
            Me.lstBelt.DeSelect()
            Me.lstZone.DeSelect()
            Me.lstRegion.DeSelect()
            Me.lstState.DeSelect()
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




            Str = "SELECT VendorTypeId , VendorType FROM dbo.tblVendorType "
            FillListBox(Me.lstCustomerType.ListItem, Str)
            Str = "Select coa_detail_id, detail_title + '~' +detail_code as Vendor_Name from vwCOADetail WHERE detail_title <> '' " & IIf(flgCompanyRights = True, " AND CompanyId=" & MyCompanyId & "", "") & " "
            If getConfigValueByType("Show Customer On Purchase") = "False" Then
                Str += " AND (Account_Type = 'Vendor')  "
            Else
                Str += " AND (Account_Type in('Customer','Vendor'))  "
            End If
            Str += " ORDER BY 2 ASC"
            dtCustomer = GetDataTable(Str)
            dtCustomer.TableName = "Customer"
            dvCustomer.Table = dtCustomer
            FillListBox(Me.lstCustomer.ListItem, Str)

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
    Private Sub FillGridForPurchaseSummary()
        Try
            Str = "SELECT      ReceivingMasterTable.ReceivingNo AS DocNo, ReceivingMasterTable.ReceivingDate AS DocDate, tblDefCostCenter.Name AS CostCenterName, " & _
                  "  vwCOADetail.detail_code AS [Detail Code], vwCOADetail.detail_title As [Detail Title], vwCOADetail.account_type As [Account Type], " & _
                  "     ReceivingMasterTable.ReceivingQty AS Qty, " & _
                  "  ReceivingMasterTable.ReceivingAmount AS Amount, ISNULL(Pur_Tax.Rec_Tax, 0) AS [Tax], ISNULL(Pur_Tax.Ad_Tax, 0) AS [Additional Tax],IsNull(ReceivingMasterTable.TotalInwardExpense,0) as TotalInvwardExpense,  " & _
                  "  vwCOADetail.CustomerType, vwCOADetail.Contact_Email, vwCOADetail.Contact_Address, vwCOADetail.Contact_Mobile, vwCOADetail.Contact_Phone, " & _
                  "    vwCOADetail.StateName, vwCOADetail.CityName, vwCOADetail.TerritoryName, ReceivingMasterTable.Remarks, " & _
                  "   ReceivingMasterTable.UserName, ReceivingMasterTable.UpdateUserName " & _
                  "    FROM         ReceivingMasterTable INNER JOIN vwCOADetail ON ReceivingMasterTable.VendorId = vwCOADetail.coa_detail_id LEFT OUTER JOIN  " & _
                  " tblDefCostCenter ON ReceivingMasterTable.CostCenterId = tblDefCostCenter.CostCenterID LEFT OUTER JOIN " & _
                  "   (SELECT     ReceivingId, SUM((ISNULL(TaxPercent, 0) / 100) * (ISNULL(Qty, 0) * ISNULL(Price, 0))) AS Rec_Tax, SUM((ISNULL(AdTax_Percent, 0) / 100) * (ISNULL(Qty, 0) * ISNULL(Price, 0))) AS Ad_Tax " & _
                 "  FROM ReceivingDetailTable  GROUP BY ReceivingId) AS Pur_Tax ON Pur_Tax.ReceivingId = ReceivingMasterTable.ReceivingId " _
                  & " where ReceivingMasterTable.ReceivingNo Like 'Pur%' And (Convert(varchar,ReceivingMasterTable.ReceivingDate,102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.Date.ToString("yyyy-M-d 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                Str += " AND ReceivingMasterTable.CostCenterId  in (" & Me.lstCostCenter.SelectedIDs & ")"
            End If
            If Me.lstCompany.SelectedIDs.Length > 0 Then
                Str += " AND ReceivingMasterTable.LocationId in (" & Me.lstCompany.SelectedIDs & ")"
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
            If Me.lstCustomer.SelectedIDs.Length > 0 Then
                Str += " And vwCOADetail.coa_detail_id in (" & Me.lstCustomer.SelectedIDs & ")"
            End If
            Str += "  order by ReceivingMasterTable.ReceivingDate desc "
            dt = GetDataTable(Str)
            Dim frmReport As New frmGrdRptPurchaseSummaryandDetail(dt, "Purchase Summary Report", DoHaveGridPrintRights, DoHaveGridExportRights, DoHaveGridFeildChosserRights)
            frmReport.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FillGridForPurchaseDetail()
        Try
            'Str = " SELECT        ReceivingMasterTable.ReceivingNo AS DocNo, ReceivingMasterTable.ReceivingDate AS DocDate, CompanyDefTable.CompanyName, tblDefCostCenter.Name AS CostCenter, tblDefLocation.Location_Name, vwCOADetail.detail_code As [Account Code], vwCOADetail.detail_title As [Account Title], " _
            '      & "  vwCOADetail.account_type As [Account Type], ISNULL(ReceivingDetailTable.ReceivedQty, 0) AS ReceivingQty, ISNULL(ReceivingDetailTable.ReceivedQty, 0) * (ReceivingDetailTable.Price) As Amount, " _
            '      & "    _Article.ArticleGroupName, _Article.ArticleTypeName, _Article.ArticleGenderName, _Article.ArticleLpoName As ArticleSubCategory, _Article.ArticleCode, _Article.ArticleDescription, IsNull(ReceivingMasterTable.TotalInwardExpense,0) as TotalInvwardExpense,  ISNULL(Pur_Tax.Rec_Tax, 0) AS [Tax]," _
            '      & "    ISNULL(Pur_Tax.Ad_Tax, 0) AS [Additional Tax], " _
            '      & "     vwCOADetail.StateName, " _
            '      & "    vwCOADetail.CityName, vwCOADetail.TerritoryName, ReceivingMasterTable.Remarks , ReceivingMasterTable.Driver_Name, ReceivingMasterTable.Vehicle_No, " _
            '      & "  vwCOADetail.CustomerType,  vwCOADetail.CreditDays, vwCOADetail.Contact_Email, vwCOADetail.Contact_Address, vwCOADetail.Contact_Mobile, " _
            '      & " vwCOADetail.Contact_Phone " _
            '      & "  FROM vwCOADetail INNER JOIN " _
            '      & "  ReceivingMasterTable ON vwCOADetail.coa_detail_id = ReceivingMasterTable.VendorId INNER JOIN " _
            '      & " ReceivingDetailTable ON ReceivingDetailTable.ReceivingId = ReceivingMasterTable.ReceivingId  " _
            '      & " LEFT OUTER JOIN (Select ReceivingId, ArticleDefId,_Article.ArticleGroupId,_Article.ArticleLPOId,_Article.ArticleCompanyId,_Article.ArticleTypeId,_Article.ArticleGroupName, _Article.ArticleTypeName, _Article.ArticleGenderName, _Article.ArticleLpoName, _Article.ArticleCode, _Article.ArticleDescription from ArticleDefView As _Article Inner join ReceivingDetailTable ON _Article.ArticleId  = ReceivingDetailTable.ArticleDefId group by  ReceivingId,ArticleDefId,_Article.ArticleGroupId,_Article.ArticleLPOId,_Article.ArticleCompanyId,_Article.ArticleTypeId,_Article.ArticleGroupName, _Article.ArticleTypeName, _Article.ArticleGenderName, _Article.ArticleLpoName, _Article.ArticleCode, _Article.ArticleDescription ) _Article on _Article.ReceivingId = ReceivingMasterTable.ReceivingId " _
            '      & " LEFT OUTER JOIN  CompanyDefTable ON ReceivingMasterTable.LocationId = CompanyDefTable.CompanyId LEFT OUTER JOIN " _
            '      & " ( Select ReceivingId , ReceivingDetailTable.LocationId,Location_Name from tblDefLocation Inner join ReceivingDetailTable ON tblDefLocation.Location_Id = ReceivingDetailTable.LocationId group by ReceivingId, ReceivingDetailTable.LocationId,Location_Name ) tblDefLocation on tblDefLocation.ReceivingId = ReceivingMasterTable.ReceivingId LEFT OUTER JOIN  " _
            '      & " tblDefCostCenter ON ReceivingMasterTable.CostCenterId = tblDefCostCenter.CostCenterID LEFT OUTER JOIN " _
            '      & " (Select ReceivingId, SUM((IsNull(TaxPercent,0)/100) *  (IsNull(Qty,0) * Isnull(Price,0))) as Rec_Tax, SUM((IsNull(AdTax_Percent,0)/100) *  (IsNull(Qty,0) * Isnull(Price,0))) as Ad_Tax From ReceivingDetailTable Group By ReceivingId " _
            '      & "	 ) Pur_Tax on Pur_Tax.ReceivingId = ReceivingMasterTable.ReceivingId " _
            '      & " where ReceivingMasterTable.ReceivingNo Like 'Pur%' And (Convert(varchar,ReceivingMasterTable.ReceivingDate,102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.Date.ToString("yyyy-M-d 23:59:59") & "', 102)) "



            'If Me.lstCostCenter.SelectedIDs.Length > 0 Then
            '    Str += " AND ReceivingMasterTable.CostCenterId  in (" & Me.lstCostCenter.SelectedIDs & ")"
            'End If
            'If Me.lstCompany.SelectedIDs.Length > 0 Then
            '    Str += " AND ReceivingMasterTable.LocationId in (" & Me.lstCompany.SelectedIDs & ")"
            'End If
            'If Me.lstLocation.SelectedIDs.Length > 0 Then
            '    Str += " AND tblDefLocation.LocationId in (" & Me.lstLocation.SelectedIDs & ")"
            'End If
            'If Me.lstState.SelectedIDs.Length > 0 Then
            '    Str += " AND vwCoaDetail.StateId in (" & Me.lstState.SelectedIDs & ")"
            'End If
            'If Me.lstRegion.SelectedIDs.Length > 0 Then
            '    Str += " AND vwCoaDetail.RegionId  in (" & Me.lstRegion.SelectedIDs & ")"
            'End If
            'If Me.lstZone.SelectedIDs.Length > 0 Then
            '    Str += " AND vwCoaDetail.ZoneId in (" & Me.lstZone.SelectedIDs & ")"
            'End If
            'If Me.lstBelt.SelectedIDs.Length > 0 Then
            '    Str += " AND vwCoaDetail.BeltId in (" & Me.lstBelt.SelectedIDs & ")"
            'End If
            'If Me.lstTerritory.SelectedIDs.Length > 0 Then
            '    Str += " AND vwCoaDetail.TerritoryId in (" & Me.lstTerritory.SelectedIDs & ")"
            'End If
            'If Me.lstCity.SelectedIDs.Length > 0 Then
            '    Str += " AND vwCoaDetail.cityId in(" & Me.lstCity.SelectedIDs & ")"
            'End If
            'If Me.lstItemDepartment.SelectedIDs.Length > 0 Then
            '    Str += " AND _Article.ArticleGroupId in (" & Me.lstItemDepartment.SelectedIDs & ")"
            'End If
            'If Me.lstItemSubCategory.SelectedIDs.Length > 0 Then
            '    Str += " AND _Article.ArticleLPOId in (" & Me.lstItemSubCategory.SelectedIDs & ")"
            'End If
            'If Me.lstItemCategory.SelectedIDs.Length > 0 Then
            '    Str += " AND _Article.ArticleCompanyId in (" & Me.lstItemCategory.SelectedIDs & ")"
            'End If
            'If Me.lstItemType.SelectedIDs.Length > 0 Then
            '    Str += " AND _Article.ArticleTypeId in(" & Me.lstItemType.SelectedIDs & ")"
            'End If
            'If Me.lstItems.SelectedIDs.Length > 0 Then
            '    Str += " AND _Article.ArticleDefId in(" & Me.lstItems.SelectedIDs & ")"
            'End If
            'If Me.lstCustomer.SelectedIDs.Length > 0 Then
            '    Str += " And vwCOADetail.coa_detail_id in (" & Me.lstCustomer.SelectedIDs & ")"
            'End If
            'Str += "  order by ReceivingMasterTable.ReceivingDate desc "

            Str = " SELECT ReceivingMasterTable.ReceivingNo AS DocNo, ReceivingMasterTable.ReceivingDate AS DocDate," _
                  & " CompanyDefTable.CompanyName, tblDefCostCenter.Name AS CostCenter, tblDefLocation.Location_Name, vwCOADetail.detail_code As [Account Code], vwCOADetail.detail_title As [Account Title],   vwCOADetail.account_type As [Account Type], " _
                  & " Sum(ISNULL(ReceivingDetailTable.ReceivedQty, 0)) As ReceivingQty,Sum(ISNULL(ReceivingDetailTable.ReceivedQty, 0) * (ReceivingDetailTable.CurrentPrice)) As ReceivingAmount, Case When IsNull(ReceivingDetailTable.CurrentPrice,0) > (IsNull(Price,0)+ISNULL(ReceivingDetailTable.Discount_Price,0)) then ((IsNull(ReceivingDetailTable.CurrentPrice,0)-IsNull(Price,0)+ISNULL(ReceivingDetailTable.Discount_Price,0))/IsNull(ReceivingDetailTable.CurrentPrice,0))*100 else 0 end as [Disc %],Sum(ISNULL(ReceivingDetailTable.ReceivedQty, 0) * (ReceivingDetailTable.Price)) As DisAmount,SUM(IsNull(TaxPercent,0)) As TaxPercent,  SUM((IsNull(TaxPercent,0)/100) *  (IsNull(Qty,0) * Isnull(Price,0))) as [Tax],SUM(IsNull(AdTax_Percent,0)) As AdTax_Percent, SUM((IsNull(AdTax_Percent,0)/100) *  (IsNull(Qty,0) * Isnull(Price,0))) as [Additional Tax] , " _
                  & " (Convert(float, (ReceivingDetailTable.Qty * ReceivingDetailTable.Price * Case When IsNull(ReceivingDetailTable.CurrencyRate, 0) = 0 Then 1 Else ReceivingDetailTable.CurrencyRate End)) + SUM((IsNull(TaxPercent,0)/100) *  (IsNull(Qty,0) * Isnull(Price,0))) +  SUM((IsNull(AdTax_Percent,0)/100) *  (IsNull(Qty,0) * Isnull(Price,0))) ) As TotalAmount, " _
                  & " ArticleDefView.ArticleGroupName, ArticleDefView.ArticleTypeName, ArticleDefView.ArticleGenderName, ArticleDefView.ArticleLpoName As ArticleSubCategory, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, " _
                  & " IsNull(ReceivingMasterTable.TotalInwardExpense,0) as TotalInvwardExpense, vwCOADetail.StateName,     vwCOADetail.CityName, vwCOADetail.TerritoryName, ReceivingMasterTable.Remarks , ReceivingMasterTable.Driver_Name, ReceivingMasterTable.Vehicle_No,   vwCOADetail.CustomerType,  vwCOADetail.CreditDays, vwCOADetail.Contact_Email, vwCOADetail.Contact_Address, vwCOADetail.Contact_Mobile,  vwCOADetail.Contact_Phone   FROM vwCOADetail " _
                  & " INNER JOIN   ReceivingMasterTable ON vwCOADetail.coa_detail_id = ReceivingMasterTable.VendorId " _
                  & " INNER JOIN  ReceivingDetailTable On  ReceivingDetailTable.ReceivingId = ReceivingMasterTable.ReceivingId " _
                  & " INNER JOIN  ArticleDefView ON ReceivingDetailTable.ArticleDefId = ArticleDefView.ArticleId " _
                  & " LEFT OUTER JOIN  CompanyDefTable ON ReceivingMasterTable.LocationId = CompanyDefTable.CompanyId " _
                  & " LEFT OUTER JOIN  tblDefLocation ON tblDefLocation.Location_Id = ReceivingDetailTable.LocationId  " _
                  & " LEFT OUTER JOIN   tblDefCostCenter ON ReceivingMasterTable.CostCenterId = tblDefCostCenter.CostCenterID " _
                  & " where ReceivingMasterTable.ReceivingNo Like 'Pur%' And (Convert(varchar,ReceivingMasterTable.ReceivingDate,102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.Date.ToString("yyyy-M-d 23:59:59") & "', 102)) "

            If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                Str += " AND ReceivingMasterTable.CostCenterId  in (" & Me.lstCostCenter.SelectedIDs & ")"
            End If
            If Me.lstCompany.SelectedIDs.Length > 0 Then
                Str += " AND ReceivingMasterTable.LocationId in (" & Me.lstCompany.SelectedIDs & ")"
            End If
            If Me.lstLocation.SelectedIDs.Length > 0 Then
                Str += " AND ReceivingDetailTable.LocationId in (" & Me.lstLocation.SelectedIDs & ")"
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
                Str += " AND ArticleDefView.ArticleGroupId in (" & Me.lstItemDepartment.SelectedIDs & ")"
            End If
            If Me.lstItemSubCategory.SelectedIDs.Length > 0 Then
                Str += " AND ArticleDefView.ArticleLPOId in (" & Me.lstItemSubCategory.SelectedIDs & ")"
            End If
            If Me.lstItemCategory.SelectedIDs.Length > 0 Then
                Str += " AND ArticleDefView.ArticleCompanyId in (" & Me.lstItemCategory.SelectedIDs & ")"
            End If
            If Me.lstItemType.SelectedIDs.Length > 0 Then
                Str += " AND ArticleDefView.ArticleTypeId in(" & Me.lstItemType.SelectedIDs & ")"
            End If
            If Me.lstItems.SelectedIDs.Length > 0 Then
                Str += " AND ArticleDefView.ArticleId in(" & Me.lstItems.SelectedIDs & ")"
            End If
            If Me.lstCustomer.SelectedIDs.Length > 0 Then
                Str += " And vwCOADetail.coa_detail_id in (" & Me.lstCustomer.SelectedIDs & ")"
            End If

            Str += " Group By ReceivingNo,ReceivingDate, CompanyDefTable.CompanyName, tblDefCostCenter.Name, tblDefLocation.Location_Name, vwCOADetail.detail_code, vwCOADetail.detail_title ,  vwCOADetail.account_type, " _
                & " ArticleDefView.ArticleGroupName, ArticleDefView.ArticleTypeName, ArticleDefView.ArticleGenderName, ArticleDefView.ArticleLpoName, ArticleDefView.ArticleCode, ArticleDefView.ArticleDescription, " _
                & " ReceivingMasterTable.TotalInwardExpense,ReceivingDetailTable.Qty,ReceivingDetailTable.CurrencyRate, ReceivingDetailTable.CurrentPrice, ReceivingDetailTable.Price, ReceivingDetailTable.Discount_Price," _
                & " vwCOADetail.StateName, vwCOADetail.CityName, vwCOADetail.TerritoryName, ReceivingMasterTable.Remarks, ReceivingMasterTable.Driver_Name, ReceivingMasterTable.Vehicle_No, vwCOADetail.CustomerType, vwCOADetail.CreditDays, vwCOADetail.Contact_Email, vwCOADetail.Contact_Address, vwCOADetail.Contact_Mobile, vwCOADetail.Contact_Phone "
            Str += " order by ReceivingMasterTable.ReceivingDate desc "
            dt = GetDataTable(Str)
            Dim frmReport As New frmGrdRptPurchaseSummaryandDetail(dt, "Purchase Detail Report", DoHaveGridPrintRights, DoHaveGridExportRights, DoHaveGridFeildChosserRights)
            'frmReport.DoHaveGridExportRights = DoHaveGridExportRights
            'frmReport.DoHaveGridFeildChosserRights = DoHaveGridFeildChosserRights
            'frmReport.DoHaveGridPrintRights = DoHaveGridPrintRights
            'frmReport.formText = "Sales Summary Report"
            frmReport.ShowDialog()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FillGridForPurchaseReturnDetail()
        Try
            'Str = " SELECT   PurchaseReturnMasterTable.PurchaseReturnNo AS DocNo, PurchaseReturnMasterTable.PurchaseReturnDate AS DocDate,tblDefCostCenter.Name As [Cost Center],tblDefLocation.Location_Name, " _
            '        & "  vwCOADetail.detail_code AS [Detail Code], vwCOADetail.detail_title As [Detail Title],  " _
            '        & "  vwCOADetail.account_type As [Account Type],   " _
            '        & "  _Article.ArticleGroupName, _Article.ArticleTypeName, _Article.ArticleGenderName, _Article.ArticleLpoName As ArticleSubCategory, _Article.ArticleCode, _Article.ArticleDescription, " _
            '        & " PurchaseReturnMasterTable.PurchaseReturnQty, PurchaseReturnMasterTable.PurchaseReturnAmount,ISNULL(Pur_Tax.Rec_Tax, 0) AS [Tax], ISNULL(Pur_Tax.Ad_Tax, 0) AS [Additional Tax], vwCOADetail.Contact_Email, vwCOADetail.Contact_Address, vwCOADetail.Contact_Mobile, " _
            '        & " vwCOADetail.Contact_Phone,   " _
            '        & "  vwCOADetail.CustomerType, vwCOADetail.StateName, vwCOADetail.CityName, " _
            '        & " vwCOADetail.TerritoryName , PurchaseReturnMasterTable.Remarks, PurchaseReturnMasterTable.UserName,  PurchaseReturnMasterTable.UpdateUserName " _
            '        & " FROM         PurchaseReturnMasterTable INNER JOIN " _
            '        & "  vwCOADetail ON PurchaseReturnMasterTable.VendorId = vwCOADetail.coa_detail_id  " _
            '        & " LEFT OUTER JOIN (Select PurchaseReturnId, ArticleDefId,_Article.ArticleGroupId,_Article.ArticleLPOId,_Article.ArticleCompanyId,_Article.ArticleTypeId,_Article.ArticleGroupName, _Article.ArticleTypeName, _Article.ArticleGenderName, _Article.ArticleLpoName, _Article.ArticleCode, _Article.ArticleDescription from ArticleDefView As _Article Inner join PurchaseReturnDetailTable ON _Article.ArticleId  = PurchaseReturnDetailTable.ArticleDefId group by  PurchaseReturnId,ArticleDefId,_Article.ArticleGroupId,_Article.ArticleLPOId,_Article.ArticleCompanyId,_Article.ArticleTypeId,_Article.ArticleGroupName, _Article.ArticleTypeName, _Article.ArticleGenderName, _Article.ArticleLpoName, _Article.ArticleCode, _Article.ArticleDescription ) _Article on _Article.PurchaseReturnId = PurchaseReturnMasterTable.PurchaseReturnId " _
            '        & " LEFT OUTER JOIN ( Select PurchaseReturnId ,PurchaseReturnDetailTable.LocationId, Location_Name from tblDefLocation Inner join PurchaseReturnDetailTable ON tblDefLocation.Location_Id = PurchaseReturnDetailTable.LocationId group by PurchaseReturnId,PurchaseReturnDetailTable.LocationId,Location_Name ) tblDefLocation on tblDefLocation.PurchaseReturnId = PurchaseReturnMasterTable.PurchaseReturnId LEFT OUTER JOIN  " _
            '        & " tblDefCostCenter ON PurchaseReturnMasterTable.CostCenterId = tblDefCostCenter.CostCenterID " _
            '        & " LEFT OUTER JOIN(Select PurchaseReturnId, SUM((IsNull(Tax_Percent,0)/100) *  (IsNull(Qty,0) * Isnull(Price,0))) as Rec_Tax, SUM((IsNull(AdTax_Percent,0)/100) *  (IsNull(Qty,0) * Isnull(Price,0))) as Ad_Tax From PurchaseReturnDetailTable Group By PurchaseReturnId " _
            '        & " ) Pur_Tax on Pur_Tax.PurchaseReturnId = PurchaseReturnMasterTable.PurchaseReturnId " _
            '        & " where PurchaseReturnMasterTable.PurchaseReturnNo <> '' And (Convert(varchar, PurchaseReturnMasterTable.PurchaseReturnDate,102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.Date.ToString("yyyy-M-d 23:59:59") & "', 102)) "

            'If Me.lstCostCenter.SelectedIDs.Length > 0 Then
            '    Str += " AND PurchaseReturnMasterTable.CostCenterId  in (" & Me.lstCostCenter.SelectedIDs & ")"
            'End If
            'If Me.lstCompany.SelectedIDs.Length > 0 Then
            '    Str += " AND PurchaseReturnMasterTable.LocationId in (" & Me.lstCompany.SelectedIDs & ")"
            'End If
            'If Me.lstLocation.SelectedIDs.Length > 0 Then
            '    Str += " AND tblDefLocation.LocationId in (" & Me.lstLocation.SelectedIDs & ")"
            'End If
            'If Me.lstState.SelectedIDs.Length > 0 Then
            '    Str += " AND vwCoaDetail.StateId in (" & Me.lstState.SelectedIDs & ")"
            'End If
            'If Me.lstRegion.SelectedIDs.Length > 0 Then
            '    Str += " AND vwCoaDetail.RegionId  in (" & Me.lstRegion.SelectedIDs & ")"
            'End If
            'If Me.lstZone.SelectedIDs.Length > 0 Then
            '    Str += " AND vwCoaDetail.ZoneId in (" & Me.lstZone.SelectedIDs & ")"
            'End If
            'If Me.lstBelt.SelectedIDs.Length > 0 Then
            '    Str += " AND vwCoaDetail.BeltId in (" & Me.lstBelt.SelectedIDs & ")"
            'End If
            'If Me.lstTerritory.SelectedIDs.Length > 0 Then
            '    Str += " AND vwCoaDetail.TerritoryId in (" & Me.lstTerritory.SelectedIDs & ")"
            'End If
            'If Me.lstCity.SelectedIDs.Length > 0 Then
            '    Str += " AND vwCoaDetail.cityId in(" & Me.lstCity.SelectedIDs & ")"
            'End If
            'If Me.lstItemDepartment.SelectedIDs.Length > 0 Then
            '    Str += " AND _Article.ArticleGroupId in (" & Me.lstItemDepartment.SelectedIDs & ")"
            'End If
            'If Me.lstItemSubCategory.SelectedIDs.Length > 0 Then
            '    Str += " AND _Article.ArticleLPOId in (" & Me.lstItemSubCategory.SelectedIDs & ")"
            'End If
            'If Me.lstItemCategory.SelectedIDs.Length > 0 Then
            '    Str += " AND _Article.ArticleCompanyId in (" & Me.lstItemCategory.SelectedIDs & ")"
            'End If
            'If Me.lstItemType.SelectedIDs.Length > 0 Then
            '    Str += " AND _Article.ArticleTypeId in(" & Me.lstItemType.SelectedIDs & ")"
            'End If
            'If Me.lstItems.SelectedIDs.Length > 0 Then
            '    Str += " AND _Article.ArticleDefId in(" & Me.lstItems.SelectedIDs & ")"
            'End If
            'If Me.lstCustomer.SelectedIDs.Length > 0 Then
            '    Str += " And vwCOADetail.coa_detail_id in (" & Me.lstCustomer.SelectedIDs & ")"
            'End If
            'Str += "  order by PurchaseReturnMasterTable.PurchaseReturnDate desc "


            Str = " SELECT   PurchaseReturnMasterTable.PurchaseReturnNo AS DocNo, PurchaseReturnMasterTable.PurchaseReturnDate AS DocDate,tblDefCostCenter.Name As [Cost Center],tblDefLocation.Location_Name, " _
                    & "  vwCOADetail.detail_code AS [Detail Code], vwCOADetail.detail_title As [Detail Title],  " _
                    & "  vwCOADetail.account_type As [Account Type],   " _
                    & "  _Article.ArticleGroupName, _Article.ArticleTypeName, _Article.ArticleGenderName, _Article.ArticleLpoName As ArticleSubCategory, _Article.ArticleCode, _Article.ArticleDescription, " _
                    & " Sum(ISNULL(PurchaseReturnDetailTable.Qty, 0)) As PurchaseReturnQty, Sum(ISNULL(PurchaseReturnDetailTable.Qty, 0) * (PurchaseReturnDetailTable.CurrentPrice)) As PurchaseReturnAmount,Case When IsNull(PurchaseReturnDetailTable.CurrentPrice,0) > (IsNull(Price,0)) then ((IsNull(PurchaseReturnDetailTable.CurrentPrice,0)-IsNull(Price,0))/IsNull(PurchaseReturnDetailTable.CurrentPrice,0))*100 else 0 end as [Disc %],Sum(ISNULL(PurchaseReturnDetailTable.Qty, 0) * (PurchaseReturnDetailTable.Price)) As DisAmount,  SUM(IsNull(Tax_Percent,0)) As TaxPercent, SUM((IsNull(Tax_Percent,0)/100) *  (IsNull(Qty,0) * Isnull(Price,0))) AS [Tax],SUM(IsNull(AdTax_Percent,0)) As AdTax_Percent, SUM((IsNull(AdTax_Percent,0)/100) *  (IsNull(Qty,0) * Isnull(Price,0))) AS [Additional Tax], " _
                    & " (Convert(float, (PurchaseReturnDetailTable.Qty * PurchaseReturnDetailTable.Price * Case When IsNull(PurchaseReturnDetailTable.CurrencyRate, 0) = 0 Then 1 Else PurchaseReturnDetailTable.CurrencyRate End)) + SUM((IsNull(Tax_Percent,0)/100) *  (IsNull(Qty,0) * Isnull(Price,0))) +  SUM((IsNull(AdTax_Percent,0)/100) *  (IsNull(Qty,0) * Isnull(Price,0))) ) As TotalAmount, " _
                    & "  vwCOADetail.Contact_Email, vwCOADetail.Contact_Address, vwCOADetail.Contact_Mobile,vwCOADetail.Contact_Phone,   " _
                    & "  vwCOADetail.CustomerType, vwCOADetail.StateName, vwCOADetail.CityName, " _
                    & " vwCOADetail.TerritoryName , PurchaseReturnMasterTable.Remarks, PurchaseReturnMasterTable.UserName,  PurchaseReturnMasterTable.UpdateUserName " _
                    & " FROM         PurchaseReturnMasterTable INNER JOIN " _
                    & "  vwCOADetail ON PurchaseReturnMasterTable.VendorId = vwCOADetail.coa_detail_id  " _
                    & " INNER JOIN  PurchaseReturnDetailTable On  PurchaseReturnDetailTable.PurchaseReturnId  = PurchaseReturnMasterTable.PurchaseReturnId  " _
                    & " INNER JOIN  ArticleDefView _Article ON PurchaseReturnDetailTable.ArticleDefId = _Article.ArticleId  " _
                    & " Inner join tblDefLocation ON tblDefLocation.Location_Id = PurchaseReturnDetailTable.LocationId " _
                    & " LEFT OUTER JOIN   tblDefCostCenter ON PurchaseReturnMasterTable.CostCenterId = tblDefCostCenter.CostCenterID   " _
                    & " where PurchaseReturnMasterTable.PurchaseReturnNo <> '' And (Convert(varchar, PurchaseReturnMasterTable.PurchaseReturnDate,102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.Date.ToString("yyyy-M-d 23:59:59") & "', 102)) "

            If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                Str += " AND PurchaseReturnMasterTable.CostCenterId  in (" & Me.lstCostCenter.SelectedIDs & ")"
            End If
            If Me.lstCompany.SelectedIDs.Length > 0 Then
                Str += " AND PurchaseReturnMasterTable.LocationId in (" & Me.lstCompany.SelectedIDs & ")"
            End If
            If Me.lstLocation.SelectedIDs.Length > 0 Then
                Str += " AND PurchaseReturnDetailTable.LocationId in (" & Me.lstLocation.SelectedIDs & ")"
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
                Str += " AND _Article.ArticleId in(" & Me.lstItems.SelectedIDs & ")"
            End If
            If Me.lstCustomer.SelectedIDs.Length > 0 Then
                Str += " And vwCOADetail.coa_detail_id in (" & Me.lstCustomer.SelectedIDs & ")"
            End If

            Str += " Group By PurchaseReturnNo,PurchaseReturnDate, tblDefCostCenter.Name, tblDefLocation.Location_Name, vwCOADetail.detail_code, vwCOADetail.detail_title ,  vwCOADetail.account_type," _
                  & " _Article.ArticleGroupName, _Article.ArticleTypeName, _Article.ArticleGenderName, _Article.ArticleLpoName , _Article.ArticleCode, _Article.ArticleDescription,PurchaseReturnDetailTable.Qty,PurchaseReturnDetailTable.CurrencyRate,  " _
                  & " PurchaseReturnDetailTable.CurrentPrice, PurchaseReturnDetailTable.Price,PurchaseReturnMasterTable.Remarks,PurchaseReturnMasterTable.UserName,PurchaseReturnMasterTable.UpdateUserName , " _
                  & " vwCOADetail.StateName,     vwCOADetail.CityName, vwCOADetail.TerritoryName,    vwCOADetail.CustomerType,  vwCOADetail.CreditDays, vwCOADetail.Contact_Email, vwCOADetail.Contact_Address, vwCOADetail.Contact_Mobile,  vwCOADetail.Contact_Phone "
            Str += "  order by PurchaseReturnMasterTable.PurchaseReturnDate desc "

            dt = GetDataTable(Str)
            Dim frmReport As New frmGrdRptPurchaseSummaryandDetail(dt, "Purchase Return Detail Report", DoHaveGridPrintRights, DoHaveGridExportRights, DoHaveGridFeildChosserRights)
            frmReport.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub FillGridForPurchaseReturnSummary()
        Try
            Str = "SELECT      PurchaseReturnMasterTable.PurchaseReturnNo AS DocNo, " _
                     & " PurchaseReturnMasterTable.PurchaseReturnDate AS DocDate,   " _
                     & " vwCOADetail.account_type As [Account Type], " _
                     & " tblDefCostCenter.Name AS CostCenterName, PurchaseReturnMasterTable.PurchaseReturnQty AS Qty, PurchaseReturnMasterTable.PurchaseReturnAmount AS Amount, ISNULL(Pur_Tax.Rec_Tax, 0) " _
                     & " AS [Tax], ISNULL(Pur_Tax.Ad_Tax, 0) AS [Additional Tax], vwCOADetail.CustomerType, vwCOADetail.Contact_Email,  " _
                     & " vwCOADetail.Contact_Address, vwCOADetail.Contact_Mobile, vwCOADetail.Contact_Phone,   " _
                     & " vwCOADetail.StateName, vwCOADetail.CityName, vwCOADetail.TerritoryName, PurchaseReturnMasterTable.Remarks,  " _
                     & "  PurchaseReturnMasterTable.UserName, PurchaseReturnMasterTable.UpdateUserName " _
                     & " FROM         tblDefCostCenter RIGHT OUTER JOIN PurchaseReturnMasterTable INNER JOIN " _
                     & " vwCOADetail ON PurchaseReturnMasterTable.VendorId = vwCOADetail.coa_detail_id ON tblDefCostCenter.CostCenterID = PurchaseReturnMasterTable.CostCenterId LEFT OUTER JOIN " _
                     & "   (SELECT     PurchaseReturnId, SUM((ISNULL(Tax_Percent, 0) / 100) * (ISNULL(Qty, 0) * ISNULL(Price, 0))) AS Rec_Tax, SUM((ISNULL(AdTax_Percent, 0) / 100) * (ISNULL(Qty, 0) * ISNULL(Price, 0))) AS Ad_Tax " _
                     & "  FROM PurchaseReturnDetailTable GROUP BY PurchaseReturnId) AS Pur_Tax ON PurchaseReturnMasterTable.PurchaseReturnId = Pur_Tax.PurchaseReturnId " _
                     & " where PurchaseReturnMasterTable.PurchaseReturnNo <> '' And (Convert(varchar, PurchaseReturnMasterTable.PurchaseReturnDate,102) BETWEEN Convert(Datetime, '" & Me.dtpFromDate.Value.Date.ToString("yyyy-M-d 00:00:00") & "', 102) AND Convert(Datetime, '" & Me.dtpToDate.Value.Date.ToString("yyyy-M-d 23:59:59") & "', 102)) "
            If Me.lstCostCenter.SelectedIDs.Length > 0 Then
                Str += " AND PurchaseReturnMasterTable.CostCenterId  in (" & Me.lstCostCenter.SelectedIDs & ")"
            End If
            If Me.lstCompany.SelectedIDs.Length > 0 Then
                Str += " AND PurchaseReturnMasterTable.LocationId in (" & Me.lstCompany.SelectedIDs & ")"
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
            Str += "  order by PurchaseReturnMasterTable.PurchaseReturnDate desc "
            dt = GetDataTable(Str)
            Dim frmReport As New frmGrdRptPurchaseSummaryandDetail(dt, "Purchase Return Summary Report", DoHaveGridPrintRights, DoHaveGridExportRights, DoHaveGridFeildChosserRights)
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
            Me.UltraTabControl2.SelectedTab = Me.UltraTabPageControl5.Tab
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnItemPre_Click(sender As Object, e As EventArgs) Handles btnItemPre.Click
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
            If rdPurchase.Checked = True Then
                If rbDetail.Checked = True Then
                    FillGridForPurchaseDetail()
                Else
                    FillGridForPurchaseSummary()
                End If
            Else
                If rbDetail.Checked = True Then
                    FillGridForPurchaseReturnDetail()
                Else
                    FillGridForPurchaseReturnSummary()
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtSearchCustomer_TextChanged(sender As Object, e As EventArgs) Handles txtSearchCustomer.TextChanged
        Try
            If txtSearchCustomer.Text <> "" Then
                dvCustomer.RowFilter = "Vendor_Name LIKE '%" & Me.txtSearchCustomer.Text & "%' "
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
            Me.cmbPeriod.Text = "Today"
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
            Me.lstCustomerType.DeSelect()
            Me.lstBelt.DeSelect()
            Me.lstZone.DeSelect()
            Me.lstRegion.DeSelect()
            Me.lstState.DeSelect()
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

    Private Sub lstCustomerType_SelectedIndexChaned(sender As Object, e As IndexEventArgs) Handles lstCustomerType.SelectedIndexChaned
        Try

            Str = "Select coa_detail_id, detail_title + '~' +detail_code as Vendor_Name from vwCOADetail WHERE detail_title <> '' " & IIf(flgCompanyRights = True, " AND CompanyId=" & MyCompanyId & "", "") & " "
            If getConfigValueByType("Show Customer On Purchase") = "False" Then
                Str += " AND (Account_Type = 'Vendor')  "
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


End Class


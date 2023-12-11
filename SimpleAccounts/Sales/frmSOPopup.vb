Public Class frmSOPopup
    Dim _SO As String = "SO"
    Dim _DC As String = "DC"
    Dim _IsDC As Boolean = False
    Dim SalesOrderId As Integer = 0
    Dim DeliveryChalanId As Integer = 0
    Dim _dtSales As DataTable
    Dim _dtDeliveryChalan As DataTable
    Dim flgMultipleSalesOrder As Boolean = False
    Dim flgLoadMultiChalan As Boolean = False
    Dim flgLoadItemAfterDeliveredOnDC As Boolean = False ''TFS2825
    Private Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub New(Optional ByVal dtSales As DataTable = Nothing, Optional ByVal dtDeliveryChalan As DataTable = Nothing, Optional ByVal IsDC As Boolean = False)
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        _IsDC = IsDC
        _dtSales = dtSales
        _dtDeliveryChalan = dtDeliveryChalan
        GetCustomers()
        If _IsDC = True Then
            Me.rbSO.Checked = True
            Me.rbDC.Checked = False
            Me.rbDC.Enabled = False
            Me.rbDC.Visible = False
            Me.rbSO.Visible = False
        Else
            Me.rbSO.Checked = True
            Me.rbDC.Checked = False
            Me.rbDC.Enabled = True
            Me.rbDC.Visible = True
            Me.rbSO.Visible = True
        End If
        If Not getConfigValueByType("MultipleSalesOrder").ToString = "Error" Then
            flgMultipleSalesOrder = Convert.ToBoolean(getConfigValueByType("MultipleSalesOrder").ToString)
        Else
            flgMultipleSalesOrder = False
        End If
        If Not getConfigValueByType("LoadMultiChalanOnSale").ToString = "Error" Then
            flgLoadMultiChalan = Convert.ToBoolean(getConfigValueByType("LoadMultiChalanOnSale").ToString)
        Else
            flgLoadMultiChalan = False
        End If
        'Boolean.TryParse(getConfigValueByType("LoadMultiChalanOnSale"), flgLoadMultiChalan)
        ''Start TFS2825
        If Not getConfigValueByType("LoadItemAfterDeliveredOnDC").ToString = "Error" Then
            flgLoadItemAfterDeliveredOnDC = Convert.ToBoolean(getConfigValueByType("LoadItemAfterDeliveredOnDC").ToString)
        Else
            flgLoadItemAfterDeliveredOnDC = False
        End If
        ''End TFS2825
    End Sub
    '    If getConfigValueByType("Show Vendor On Sales") = "True"


    'If getConfigValueByType("ShowMiscAccountsOnSales") = "True"
    Private Sub GetCustomers()
        Dim Query As String = String.Empty
        Dim ShowVendorOnSales As Boolean = False
        Dim ShowMiscAccountsOnSales As Boolean = False
        Dim formName As String = "" ''This Variable is added to get COA Account which are mapped with Sales :TFS3419
        If _IsDC Then
            formName = "frmDeliveryChalan"
        Else
            formName = "frmSales"
        End If
        Try
            If Not getConfigValueByType("Show Vendor On Sales") = "Error" Then
                ShowVendorOnSales = CBool(getConfigValueByType("Show Vendor On Sales"))
            End If
            If Not getConfigValueByType("ShowMiscAccountsOnSales") = "Error" Then
                ShowMiscAccountsOnSales = CBool(getConfigValueByType("ShowMiscAccountsOnSales"))
            End If

            Query = "SELECT    vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name, vwCOADetail.detail_code as [Code], tblListState.StateName as State, tblListCity.CityName as City,  " & _
                         " tblListTerritory.TerritoryName as Territory , tblCustomer.ExpiryDate, tblCustomer.Discper as [Discount] ,tblCustomer.otherexpanses as [Other Expense], tblCustomer.Fuel as Fuel, tblCustomer.CNG as CNG , tblCustomer.Cridtlimt as Limit, dbo.vwCOADetail.account_type as Type, isnull(customertypes,0) as typeid, vwCOADetail.Contact_Email as Email, vwCOADetail.Contact_Phone as Phone, vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title, IsNull(vwCOADetail.SaleMan,0) as SaleManId,IsNull(vwCOADetail.CreditDays,0) as CreditDays " & _
                         " FROM  tblCustomer LEFT OUTER JOIN " & _
                         " tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                         " tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                         " tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                         " vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " _
                              & " WHERE dbo.vwCOADetail.detail_title Is Not NULL " & IIf(ShowVendorOnSales = True, " AND (dbo.vwCOADetail.account_type in ('Customer','Vendor'))", " AND (dbo.vwCOADetail.account_type in ('Customer'))") & "" _
                                       & "" & IIf(ShowMiscAccountsOnSales = True, " OR vwCOADetail.coa_detail_id IN (SELECT  DISTINCT tblCOAMainSubSubDetail.coa_detail_id " & _
                                      "FROM tblMiscAccountsonSales INNER JOIN   tblCOAMainSubSubDetail ON tblMiscAccountsonSales.AccountId = tblCOAMainSubSubDetail.main_sub_sub_id where tblMiscAccountsonSales.Active = 1) ", "") & ""
            ''Start TFS3322 : Ayesha Rehman : 15-05-2018
            If LoginGroup = "Administrator" Then
            ElseIf GetMappedUserId() > 0 And getGroupAccountsConfigforSales(formName) Then
                Query = "SELECT    vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name, vwCOADetail.detail_code as [Code], tblListState.StateName as State, tblListCity.CityName as City,  " & _
                         " tblListTerritory.TerritoryName as Territory , tblCustomer.ExpiryDate, tblCustomer.Discper as [Discount] ,tblCustomer.otherexpanses as [Other Expense], tblCustomer.Fuel as Fuel, tblCustomer.CNG as CNG , tblCustomer.Cridtlimt as Limit, dbo.vwCOADetail.account_type as Type, isnull(customertypes,0) as typeid, vwCOADetail.Contact_Email as Email, vwCOADetail.Contact_Phone as Phone, vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title, IsNull(vwCOADetail.SaleMan,0) as SaleManId,IsNull(vwCOADetail.CreditDays,0) as CreditDays " & _
                         " FROM  tblCustomer LEFT OUTER JOIN " & _
                         " tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                         " tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                         " tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                         " vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " _
                              & " WHERE dbo.vwCOADetail.detail_title Is Not NULL "
                Query += " And (coa_detail_id in (Select COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 3) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                       & " or main_sub_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 2) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                       & " or main_sub_id in (SELECT COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 1) and COAUserMapping.[User_Id]= " & LoginGroupId & " ) " _
                       & " or coa_main_id in (SELECT   COAAccountMapping.AccountId FROM COAAccountMapping INNER JOIN COAGroups ON COAAccountMapping.COAGroupId = COAGroups.COAGroupId INNER JOIN COAUserMapping ON COAGroups.COAGroupId = COAUserMapping.COAGroupId WHERE (COAAccountMapping.AccountLevel = 0) and COAUserMapping.[User_Id]= " & LoginGroupId & ") ) "
            End If
            ''End TFS3322
            Query += " order by tblCustomer.Sortorder, vwCOADetail.detail_title"

            FillUltraDropDown(Me.cmbCustomer, Query)
            Me.cmbCustomer.Rows(0).Activate()
            Me.cmbCustomer.DisplayLayout.Bands(0).Columns("Id").Hidden = True
            Me.cmbCustomer.DisplayLayout.Bands(0).Columns("Territory").Hidden = True
            Me.cmbCustomer.DisplayLayout.Bands(0).Columns("ExpiryDate").Hidden = True
            Me.cmbCustomer.DisplayLayout.Bands(0).Columns("Discount").Hidden = True
            Me.cmbCustomer.DisplayLayout.Bands(0).Columns("Other Expense").Hidden = True
            Me.cmbCustomer.DisplayLayout.Bands(0).Columns("Fuel").Hidden = True
            Me.cmbCustomer.DisplayLayout.Bands(0).Columns("CNG").Hidden = True
            Me.cmbCustomer.DisplayLayout.Bands(0).Columns("Limit").Hidden = True
            'Me.cmbCustomer.DisplayLayout.Bands(0).Columns("Type").Hidden = True
            Me.cmbCustomer.DisplayLayout.Bands(0).Columns("typeid").Hidden = True
            Me.cmbCustomer.DisplayLayout.Bands(0).Columns("Email").Hidden = True
            Me.cmbCustomer.DisplayLayout.Bands(0).Columns("Phone").Hidden = True
            Me.cmbCustomer.DisplayLayout.Bands(0).Columns("Mobile").Hidden = True
            Me.cmbCustomer.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Hidden = True
            Me.cmbCustomer.DisplayLayout.Bands(0).Columns("SaleManId").Hidden = True
            Me.cmbCustomer.DisplayLayout.Bands(0).Columns("CreditDays").Hidden = True

            'Me.cmbCustomer.L
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetSOForSales(ByVal CustomerId As Integer)
        Dim Query As String = String.Empty
        Try
            Query = "Select SalesOrderID, SalesOrderNo + ' ~ ' + Convert(varchar(12), SalesOrderMasterTable.SalesOrderDate,113) as SalesOrderNo,Remarks, CostCenterId, PONo,PO_Date,SOP_ID,Delivery_Date, ISNULL(SpecialAdjustment,0) as SpecialAdjustment, IsNull(SalesOrderMasterTable.UserId, 0) As UserId, IsNull(TransporterId, 0) AS TransporterId from SalesOrderMasterTable WHERE VendorID=" & CustomerId & " AND SalesOrderNo  <> '' AND IsNull(Posted,0)=1 AND Status='Open' AND ISNULL(DCStatus, 'Open') = 'Open' "
            If Me.DateTimePicker1.Checked = True AndAlso DateTimePicker2.Checked = True Then
                Query += " And CONVERT(varchar, SalesOrderMasterTable.SalesOrderDate, 102) BETWEEN Convert(datetime, N'" & DateTimePicker2.Value.ToString("yyyy-M-d 00:00:00") & "') AND Convert(datetime, N'" & DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "')"
            End If
            If Me.DateTimePicker1.Checked = True AndAlso DateTimePicker2.Checked = False Then
                Query += " And CONVERT(varchar, SalesOrderMasterTable.SalesOrderDate, 102) >= Convert(datetime, N'" & DateTimePicker2.Value.ToString("yyyy-M-d 00:00:00") & "')"
            End If
            If Me.DateTimePicker1.Checked = False AndAlso DateTimePicker2.Checked = True Then
                Query += " And CONVERT(varchar, SalesOrderMasterTable.SalesOrderDate, 102) <= Convert(datetime, N'" & DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "')"
            End If
            Query += " ORDER BY SalesOrderID DESC "
            FillDropDown(Me.cmbSO, Query)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetSOForDC(ByVal CustomerId As Integer)
        Dim Query As String = String.Empty
        Try
            Query = "Select SalesOrderID, SalesOrderNo + ' ~ ' + Convert(varchar(12), SalesOrderMasterTable.SalesOrderDate,113) as SalesOrderNo,Remarks, CostCenterId, PONo,PO_Date,SOP_ID,Delivery_Date, ISNULL(SpecialAdjustment,0) as SpecialAdjustment, IsNull(SalesOrderMasterTable.UserId, 0) As UserId, IsNull(TransporterId, 0) AS TransporterId from SalesOrderMasterTable WHERE VendorID=" & CustomerId & " AND SalesOrderNo  <> '' AND IsNull(Posted,0)=1 AND ISNULL(DCStatus, 'Open')='Open' "
            If Me.DateTimePicker1.Checked = True AndAlso DateTimePicker2.Checked = True Then
                Query += " And CONVERT(varchar, SalesOrderMasterTable.SalesOrderDate, 102) BETWEEN Convert(datetime, N'" & DateTimePicker2.Value.ToString("yyyy-M-d 00:00:00") & "') AND Convert(datetime, N'" & DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "')"
            End If
            If Me.DateTimePicker1.Checked = True AndAlso DateTimePicker2.Checked = False Then
                Query += " And CONVERT(varchar, SalesOrderMasterTable.SalesOrderDate, 102) >= Convert(datetime, N'" & DateTimePicker2.Value.ToString("yyyy-M-d 00:00:00") & "')"
            End If
            If Me.DateTimePicker1.Checked = False AndAlso DateTimePicker2.Checked = True Then
                Query += " And CONVERT(varchar, SalesOrderMasterTable.SalesOrderDate, 102) <= Convert(datetime, N'" & DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "')"
            End If
            Query += " ORDER BY SalesOrderID DESC "
            FillDropDown(Me.cmbSO, Query)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetDCForSales(ByVal CustomerId As Integer)
        Dim Query As String = String.Empty
        Try
            Query = "Select DeliveryChalanMasterTable.DeliveryId, DeliveryChalanMasterTable.DeliveryNo, DeliveryChalanMasterTable.BiltyNo, DeliveryChalanMasterTable.Remarks, IsNull(DeliveryChalanMasterTable.EmployeeCode,0) as EmployeeCode, IsNull(DeliveryChalanMasterTable.LocationId,0) as LocationId, IsNull(DeliveryChalanMasterTable.CostCenterId,0) as Project, SO.PONo, SO.PO_Date, Other_Company, IsNull(DeliveryChalanMasterTable.CustomerCode,0) as CustomerCode, DeliveryChalanMasterTable.Arrival_Time, DeliveryChalanMasterTable.Departure_Time, IsNull(DeliveryChalanMasterTable.JobCardId, 0) As JobCardId, IsNull(DeliveryChalanMasterTable.UserId, 0) As UserId, IsNull(DeliveryChalanMasterTable.POId, 0) As SalesOrderId1  From DeliveryChalanMasterTable LEFT OUTER JOIN SalesOrderMasterTable SO On So.SalesOrderID = DeliveryChalanMasterTable.POId  WHERE IsNull(Post,0)=1 AND (DeliveryChalanMasterTable.Status='Open' OR DeliveryChalanMasterTable.Status is null) AND CustomerCode=" & CustomerId & ""
            If Me.DateTimePicker1.Checked = True AndAlso DateTimePicker2.Checked = True Then
                Query += " And CONVERT(varchar, DeliveryChalanMasterTable.DeliveryDate, 102) BETWEEN Convert(datetime, N'" & DateTimePicker2.Value.ToString("yyyy-M-d 00:00:00") & "') AND Convert(datetime, N'" & DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "')"
            End If
            If Me.DateTimePicker1.Checked = True AndAlso DateTimePicker2.Checked = False Then
                Query += " And CONVERT(varchar, DeliveryChalanMasterTable.DeliveryDate, 102) >= Convert(datetime, N'" & DateTimePicker2.Value.ToString("yyyy-M-d 00:00:00") & "')"
            End If
            If Me.DateTimePicker1.Checked = False AndAlso DateTimePicker2.Checked = True Then
                Query += " And CONVERT(varchar, DeliveryChalanMasterTable.DeliveryDate, 102) <= Convert(datetime, N'" & DateTimePicker2.Value.ToString("yyyy-M-d 23:59:59") & "')"
            End If
            Query += " "
            FillDropDown(Me.cmbSO, Query)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function DisplaySOrders(ByVal SalesOrderId As Integer)
        Dim dt As DataTable
        Dim Query As String = String.Empty
        Dim DcQtyInSales = 0
        Try

            ''Start TFS3326
            '' Qty , Load Qty ,total Qty Changes according to DCQty and DeliveredQty
            If IsDCExistAgainstSO(SalesOrderId) Then
                If IsDCExistInSales(SalesOrderId, DcQtyInSales) Then
                    Query = "SELECT Recv_D.LocationId, Article.ArticleCode as Code, Article.ArticleDescription AS Item, Article.ArticleSizeName as Size, Article.ArticleColorName as Color, Recv_D.ArticleSize AS Unit, Convert(Decimal(18, " & DecimalPointInQty & "), (ISNULL(Recv_D.Sz1,0) - ((Isnull(DeliveredQty , 0) +(IsNull(Recv_D.DCQty, 0) - " & DcQtyInSales & " )))), 1) AS Qty, " _
                & " Recv_D.PostDiscountPrice As PostDiscountPrice,IsNull(Recv_D.DiscountId,1) as DiscountId, '' as [DiscountType],ISNULL(Recv_D.Discount_Percentage,0) as Discount_Percentage, Convert(float, 0) As FlatDiscount, IsNull(Recv_D.DiscountFactor , 0) As DiscountFactor,  IsNull(Recv_D.DiscountValue , 0) As DiscountValue ,  " _
                & " Recv_D.Price as Rate,  IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, Convert(float,0) as [Total Currency Amount] , " _
                & " ((IsNull(Recv_D.Qty, 0) * Recv_D.Price *  Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) - isnull(Recv_D.DeliveredTotalQty,0) * Recv_D.Price *  Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) AS Total, " _
                & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as [Pack Qty] ,Recv_D.CurrentPrice as CurrentPrice, 0 as PackPrice, 0 As SaleDetailId, Convert(float, 0) as RetailPrice, ISNULL(Recv_D.TradePrice,0) as TradePrice,  ISNULL(Recv_D.SalesTax_Percentage,0) as Tax, Convert(float,0) as [Tax Amount], Convert(float,0) as [Currency Tax Amount], Convert(float, IsNull(Recv_D.SED_Tax_Percent,0)) as SED,Convert(float,0) as [Total Amount], 0 as savedqty, (ISNULL(Recv_D.SchemeQty,0)-ISNULL(Recv_D.DeliveredSchemeQty,0)) as [Sample Qty], ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, IsNull(Recv_D.SalesOrderID,0) as So_Id, Convert(Decimal(18, " & DecimalPointInQty & "), (Isnull(Recv_D.Qty,0) - (Isnull(DeliveredTotalQty , 0)+ (IsNull(Recv_D.DCTotalQty, 0) - " & DcQtyInSales & "))), 1)  as LoadQty, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, 0 as NetBill, 0 as BatchID, IsNull(Recv_D.BatchNo,'xxxx') as [Batch No], IsNull(Recv_D.ExpiryDate,DATEADD(Month , 1 , getDate())) as ExpiryDate, Convert(float, 0) AS [Bardana Deduction], Convert(float, 0) AS [Other Deduction], Convert(float,0) AS [After Deduction Qty], Recv_D.Comments, Recv_D.Other_Comments as [Other Comments], Recv_D.Engine_No, Recv_D.Chassis_No, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article.SubSubId,0) as InvAccountId, Article.SalesAccountId, Article.CGSAccountId,IsNull(Recv_D.CostPrice,0) as CostPrice, 0 as CR_SalesDetailId, 0 as SaleCertificateId, IsNull(Stock.CurrStock,0) as Stock, Convert(float, 0) as SalesReturnQty, 0 as DeliveryChalanId, 0 as DeliveryChalanDetailId, Convert(float, 0) as Transport_Charges, ISNULL(Article.ApplyAdjustmentFuelExp,1) as  ApplyAdjustmentFuelExp, Convert(float, 0) As Gross_Weights,  Convert(float, 0) As Tray_Weights, Convert(float, 0) As Net_Weights, Convert(Decimal(18, " & DecimalPointInQty & "), (IsNull(Recv_D.Qty,0) - (IsNull(Recv_D.DeliveredTotalQty,0)+(IsNull(Recv_D.DCTotalQty, 0)- " & DcQtyInSales & "))), 1) as TotalQty, IsNull(Recv_D.SalesOrderDetailId, 0) As SODetailId, '' As ArticleLpoName, Convert(float, 0) As OldSalePrice, 0 As SalePriceProcessId, Convert(bit,0) as [LogicalItem] FROM SalesOrderDetailTable Recv_D INNER JOIN " _
                & " ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                & " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id INNER JOIN SalesOrderMasterTable Recv ON Recv.SalesOrderId = Recv_D.SalesOrderId LEFT OUTER JOIN(Select ArticleDefID, SUM(IsNull(InQty,0)-ISNull(OutQty,0)) as CurrStock From StockDetailTable Group By ArticleDefId Having SUM(IsNull(InQty,0)-ISNull(OutQty,0)) <> 0) Stock On Stock.ArticleDefId = Recv_D.ArticleDefId   " _
                & " Where  (Isnull(Recv_D.Sz1,0) - ((Isnull(Recv_D.DeliveredQty,0) + (IsNull(Recv_D.DCQty, 0) - " & DcQtyInSales & "))) > 0)"
                Else
                    Query = "SELECT Recv_D.LocationId, Article.ArticleCode as Code, Article.ArticleDescription AS Item, Article.ArticleSizeName as Size, Article.ArticleColorName as Color, Recv_D.ArticleSize AS Unit, Convert(Decimal(18, " & DecimalPointInQty & "), (ISNULL(Recv_D.Sz1,0) - (Isnull(DeliveredQty , 0)+IsNull(Recv_D.DCQty, 0))), 1) AS Qty, " _
                & " Recv_D.PostDiscountPrice As PostDiscountPrice,IsNull(Recv_D.DiscountId,1) as DiscountId, '' as [DiscountType],ISNULL(Recv_D.Discount_Percentage,0) as Discount_Percentage, Convert(float, 0) As FlatDiscount, IsNull(Recv_D.DiscountFactor , 0) As DiscountFactor,  IsNull(Recv_D.DiscountValue , 0) As DiscountValue ,  " _
                & " Recv_D.Price as Rate,  IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, Convert(float,0) as [Total Currency Amount] , " _
                & " ((IsNull(Recv_D.Qty, 0) * Recv_D.Price *  Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) - isnull(Recv_D.DeliveredTotalQty,0) * Recv_D.Price *  Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) AS Total, " _
                & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as [Pack Qty] ,Recv_D.CurrentPrice as CurrentPrice, 0 as PackPrice, 0 As SaleDetailId, Convert(float, 0) as RetailPrice, ISNULL(Recv_D.TradePrice,0) as TradePrice,  ISNULL(Recv_D.SalesTax_Percentage,0) as Tax, Convert(float,0) as [Tax Amount], Convert(float,0) as [Currency Tax Amount], Convert(float, IsNull(Recv_D.SED_Tax_Percent,0)) as SED,Convert(float,0) as [Total Amount], 0 as savedqty, (ISNULL(Recv_D.SchemeQty,0)-ISNULL(Recv_D.DeliveredSchemeQty,0)) as [Sample Qty], ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, IsNull(Recv_D.SalesOrderID,0) as So_Id, Convert(Decimal(18, " & DecimalPointInQty & "), (Isnull(Recv_D.Qty,0) - (Isnull(DeliveredTotalQty , 0)+IsNull(Recv_D.DCTotalQty, 0))), 1)  as LoadQty, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, 0 as NetBill, 0 as BatchID, IsNull(Recv_D.BatchNo,'xxxx')  as [Batch No], IsNull(Recv_D.ExpiryDate,DATEADD(Month , 1 , getDate())) as ExpiryDate, Convert(float, 0) AS [Bardana Deduction], Convert(float, 0) AS [Other Deduction], Convert(float,0) AS [After Deduction Qty], Recv_D.Comments, Recv_D.Other_Comments as [Other Comments], Recv_D.Engine_No, Recv_D.Chassis_No, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article.SubSubId,0) as InvAccountId, Article.SalesAccountId, Article.CGSAccountId,IsNull(Recv_D.CostPrice,0) as CostPrice, 0 as CR_SalesDetailId, 0 as SaleCertificateId, IsNull(Stock.CurrStock,0) as Stock, Convert(float, 0) as SalesReturnQty, 0 as DeliveryChalanId, 0 as DeliveryChalanDetailId, Convert(float, 0) as Transport_Charges, ISNULL(Article.ApplyAdjustmentFuelExp,1) as  ApplyAdjustmentFuelExp, Convert(float, 0) As Gross_Weights,  Convert(float, 0) As Tray_Weights, Convert(float, 0) As Net_Weights, Convert(Decimal(18, " & DecimalPointInQty & "), (IsNull(Recv_D.Qty,0) - (IsNull(Recv_D.DeliveredTotalQty,0)+IsNull(Recv_D.DCTotalQty, 0))), 1) as TotalQty, IsNull(Recv_D.SalesOrderDetailId, 0) As SODetailId, '' As ArticleLpoName, Convert(float, 0) As OldSalePrice, 0 As SalePriceProcessId, Convert(bit,0) as [LogicalItem] FROM SalesOrderDetailTable Recv_D INNER JOIN " _
                & " ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
                & " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id INNER JOIN SalesOrderMasterTable Recv ON Recv.SalesOrderId = Recv_D.SalesOrderId LEFT OUTER JOIN(Select ArticleDefID, SUM(IsNull(InQty,0)-ISNull(OutQty,0)) as CurrStock From StockDetailTable Group By ArticleDefId Having SUM(IsNull(InQty,0)-ISNull(OutQty,0)) <> 0) Stock On Stock.ArticleDefId = Recv_D.ArticleDefId   " _
                & " Where  (Isnull(Recv_D.Sz1,0) - (Isnull(Recv_D.DeliveredQty,0)+IsNull(Recv_D.DCQty, 0)) > 0)"
                End If
            Else
                Query = "SELECT Recv_D.LocationId, Article.ArticleCode as Code, Article.ArticleDescription AS Item, Article.ArticleSizeName as Size, Article.ArticleColorName as Color, Recv_D.ArticleSize AS Unit, Convert(Decimal(18, " & DecimalPointInQty & "), (ISNULL(Recv_D.Sz1,0) - (Isnull(DeliveredQty , 0))), 1) AS Qty, " _
       & " Recv_D.PostDiscountPrice As PostDiscountPrice,IsNull(Recv_D.DiscountId,1) as DiscountId, '' as [DiscountType],ISNULL(Recv_D.Discount_Percentage,0) as Discount_Percentage, Convert(float, 0) As FlatDiscount, IsNull(Recv_D.DiscountFactor , 0) As DiscountFactor,  IsNull(Recv_D.DiscountValue , 0) As DiscountValue ,  " _
       & " Recv_D.Price as Rate,  IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, Convert(float,0) as [Total Currency Amount] , " _
       & " ((IsNull(Recv_D.Qty, 0) * Recv_D.Price *  Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) - isnull(Recv_D.DeliveredTotalQty,0) * Recv_D.Price *  Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) AS Total, " _
       & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as [Pack Qty] ,Recv_D.CurrentPrice as CurrentPrice, 0 as PackPrice, 0 As SaleDetailId, Convert(float, 0) as RetailPrice, ISNULL(Recv_D.TradePrice,0) as TradePrice,  ISNULL(Recv_D.SalesTax_Percentage,0) as Tax, Convert(float,0) as [Tax Amount], Convert(float,0) as [Currency Tax Amount], Convert(float, IsNull(Recv_D.SED_Tax_Percent,0)) as SED,Convert(float,0) as [Total Amount], 0 as savedqty, (ISNULL(Recv_D.SchemeQty,0)-ISNULL(Recv_D.DeliveredSchemeQty,0)) as [Sample Qty], ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, IsNull(Recv_D.SalesOrderID,0) as So_Id, Convert(Decimal(18, " & DecimalPointInQty & "), (Isnull(Recv_D.Qty,0) - (Isnull(DeliveredTotalQty , 0))), 1)  as LoadQty, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, 0 as NetBill, 0 as BatchID, IsNull(Recv_D.BatchNo,'xxxx') as [Batch No],  IsNull(Recv_D.ExpiryDate,DATEADD(Month , 1 , getDate())) as ExpiryDate, Convert(float, 0) AS [Bardana Deduction], Convert(float, 0) AS [Other Deduction], Convert(float,0) AS [After Deduction Qty], Recv_D.Comments, Recv_D.Other_Comments as [Other Comments], Recv_D.Engine_No, Recv_D.Chassis_No, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article.SubSubId,0) as InvAccountId, Article.SalesAccountId, Article.CGSAccountId,IsNull(Recv_D.CostPrice,0) as CostPrice, 0 as CR_SalesDetailId, 0 as SaleCertificateId, IsNull(Stock.CurrStock,0) as Stock, Convert(float, 0) as SalesReturnQty, 0 as DeliveryChalanId, 0 as DeliveryChalanDetailId, Convert(float, 0) as Transport_Charges, ISNULL(Article.ApplyAdjustmentFuelExp,1) as  ApplyAdjustmentFuelExp, Convert(float, 0) As Gross_Weights,  Convert(float, 0) As Tray_Weights, Convert(float, 0) As Net_Weights, Convert(Decimal(18, " & DecimalPointInQty & "), (IsNull(Recv_D.Qty,0) - (IsNull(Recv_D.DeliveredTotalQty,0))), 1) as TotalQty, IsNull(Recv_D.SalesOrderDetailId, 0) As SODetailId, '' As ArticleLpoName, Convert(float, 0) As OldSalePrice, 0 As SalePriceProcessId, Convert(bit,0) as [LogicalItem] FROM SalesOrderDetailTable Recv_D INNER JOIN " _
       & " ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
       & " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id INNER JOIN SalesOrderMasterTable Recv ON Recv.SalesOrderId = Recv_D.SalesOrderId LEFT OUTER JOIN(Select ArticleDefID, SUM(IsNull(InQty,0)-ISNull(OutQty,0)) as CurrStock From StockDetailTable Group By ArticleDefId Having SUM(IsNull(InQty,0)-ISNull(OutQty,0)) <> 0) Stock On Stock.ArticleDefId = Recv_D.ArticleDefId   " _
       & " Where  (Isnull(Recv_D.Sz1,0) - Isnull(Recv_D.DeliveredQty,0) > 0)"
            End If
            ''End TFS3326
            ''Commented Against TFS3326
            '    Query = "SELECT Recv_D.LocationId, Article.ArticleCode as Code, Article.ArticleDescription AS Item, Article.ArticleSizeName as Size, Article.ArticleColorName as Color, Recv_D.ArticleSize AS Unit, Convert(Decimal(18, " & DecimalPointInQty & "), (ISNULL(Recv_D.Sz1,0) - (Isnull(DeliveredQty , 0)+IsNull(Recv_D.DCQty, 0))), 1) AS Qty, " _
            '& " Recv_D.PostDiscountPrice As PostDiscountPrice,IsNull(Recv_D.DiscountId,1) as DiscountId, '' as [DiscountType],ISNULL(Recv_D.Discount_Percentage,0) as Discount_Percentage, Convert(float, 0) As FlatDiscount, IsNull(Recv_D.DiscountFactor , 0) As DiscountFactor,  IsNull(Recv_D.DiscountValue , 0) As DiscountValue ,  " _
            '& " Recv_D.Price as Rate,  IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, Convert(float,0) as [Total Currency Amount] , " _
            '& " ((IsNull(Recv_D.Qty, 0) * Recv_D.Price *  Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) - isnull(Recv_D.DeliveredTotalQty,0) * Recv_D.Price *  Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) AS Total, " _
            '& " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as [Pack Qty] ,Recv_D.CurrentPrice as CurrentPrice, 0 as PackPrice, 0 As SaleDetailId, Convert(float, 0) as RetailPrice, ISNULL(Recv_D.TradePrice,0) as TradePrice,  ISNULL(Recv_D.SalesTax_Percentage,0) as Tax, Convert(float,0) as [Tax Amount], Convert(float,0) as [Currency Tax Amount], Convert(float, IsNull(Recv_D.SED_Tax_Percent,0)) as SED,Convert(float,0) as [Total Amount], 0 as savedqty, (ISNULL(Recv_D.SchemeQty,0)-ISNULL(Recv_D.DeliveredSchemeQty,0)) as [Sample Qty], ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, IsNull(Recv_D.SalesOrderID,0) as So_Id, Convert(Decimal(18, " & DecimalPointInQty & "), (Isnull(Recv_D.Qty,0) - (Isnull(DeliveredTotalQty , 0)+IsNull(Recv_D.DCTotalQty, 0))), 1)  as LoadQty, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, 0 as NetBill, 0 as BatchID,'' as [Batch No], Convert(DateTime,Null) as ExpiryDate, Convert(float, 0) AS [Bardana Deduction], Convert(float, 0) AS [Other Deduction], Convert(float,0) AS [After Deduction Qty], Recv_D.Comments, Recv_D.Other_Comments as [Other Comments], Recv_D.Engine_No, Recv_D.Chassis_No, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article.SubSubId,0) as InvAccountId, Article.SalesAccountId, Article.CGSAccountId,IsNull(Recv_D.CostPrice,0) as CostPrice, 0 as CR_SalesDetailId, 0 as SaleCertificateId, IsNull(Stock.CurrStock,0) as Stock, Convert(float, 0) as SalesReturnQty, 0 as DeliveryChalanId, 0 as DeliveryChalanDetailId, Convert(float, 0) as Transport_Charges, ISNULL(Article.ApplyAdjustmentFuelExp,1) as  ApplyAdjustmentFuelExp, Convert(float, 0) As Gross_Weights,  Convert(float, 0) As Tray_Weights, Convert(float, 0) As Net_Weights, Convert(Decimal(18, " & DecimalPointInQty & "), (IsNull(Recv_D.Qty,0) - (IsNull(Recv_D.DeliveredTotalQty,0)+IsNull(Recv_D.DCTotalQty, 0))), 1) as TotalQty, IsNull(Recv_D.SalesOrderDetailId, 0) As SODetailId, '' As ArticleLpoName, Convert(float, 0) As OldSalePrice, 0 As SalePriceProcessId, Convert(bit,0) as [LogicalItem] FROM SalesOrderDetailTable Recv_D INNER JOIN " _
            '& " ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '& " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id INNER JOIN SalesOrderMasterTable Recv ON Recv.SalesOrderId = Recv_D.SalesOrderId LEFT OUTER JOIN(Select ArticleDefID, SUM(IsNull(InQty,0)-ISNull(OutQty,0)) as CurrStock From StockDetailTable Group By ArticleDefId Having SUM(IsNull(InQty,0)-ISNull(OutQty,0)) <> 0) Stock On Stock.ArticleDefId = Recv_D.ArticleDefId   " _
            '& " Where  (Isnull(Recv_D.Sz1,0) - (Isnull(Recv_D.DeliveredQty,0)+IsNull(Recv_D.DCQty, 0)) > 0)"

            'If Not FromDate = Nothing AndAlso Not ToDate = Nothing Then
            '    Query += " And CONVERT(varchar, Recv.SalesOrderDate, 102) BETWEEN Convert(datetime, N'" & FromDate.ToString("yyyy-M-d 00:00:00") & "') AND Convert(datetime, N'" & ToDate.ToString("yyyy-M-d 23:59:59") & "')"
            'End If
            'If Not FromDate = Nothing AndAlso ToDate = Nothing Then
            '    Query += " And CONVERT(varchar, Recv.SalesOrderDate, 102) >= Convert(datetime, N'" & FromDate.ToString("yyyy-M-d 00:00:00") & "')"
            'End If
            'If FromDate = Nothing AndAlso Not ToDate = Nothing Then
            '    Query += " And CONVERT(varchar, Recv.SalesOrderDate, 102) <= Convert(datetime, N'" & ToDate.ToString("yyyy-M-d 23:59:59") & "')"
            'End If
            'If CustomerId > 0 AndAlso SalesOrderId < 1 Then
            '    Query += " And Recv.VendorId = " & CustomerId & ""
            'ElseIf CustomerId > 0 AndAlso SalesOrderId > 0 Then
            '    Query += " And Recv.VendorId = " & CustomerId & " And Recv.SalesOrderId = " & SalesOrderId & ""
            'ElseIf CustomerId < 1 AndAlso SalesOrderId > 0 Then
            Query += " And Recv.SalesOrderId = " & SalesOrderId & ""
            'End If
            dt = GetDataTable(Query)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Private Function DisplaySOrdersForDC(ByVal SalesOrderId As Integer)
        Dim dt As DataTable
        Dim Query As String = String.Empty
        Try


            'Str = "SELECT Recv_D.LocationId, Article.ArticleCode as Code, Article.ArticleDescription AS Item, Article.ArticleSizeName AS Size, Article.ArticleColorName AS Color , Recv_D.ArticleSize AS Unit, ISNULL(SalesOrderDetailTable.Sz1,0) As OrderQty, ISNULL(Recv_D.PerviouslyDeliveredQty,0) AS DeliverQty, Isnull(Recv_D.Sz1 , 0) AS Qty, ISNULL(SalesOrderDetailTable.Sz1,0) -  ISNULL(Recv_D.PerviouslyDeliveredQty,0) - Isnull(Recv_D.Sz1 , 0)  AS RemainingQty, Recv_D.Price as Rate, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, " _
            '          & " (Recv_D.Qty * Recv_D.Price *(Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End)) AS Total, " _
            '          & " Article.ArticleGroupId,Recv_D.ArticleDefId, Recv_D.Sz7 as [Pack Qty], ISNULL(Recv_D.CurrentPrice,0) as CurrentPrice,  ISNULL(Recv_D.PackPrice,0) as PackPrice, Recv_D.DeliveryDetailId, ISNULL(Recv_D.TradePrice,0) as TradePrice, isnull(Recv_D.TaxPercent,0) as Tax, ISNULL(Recv_D.SEDPercent,0) As SED, Convert(float, 0) as savedqty , SampleQty as [Sample Qty], ISNULL(Recv_D.Discount_Percentage,0) as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, ISNULL(So_ID,0) as So_Id, Recv_D.UOM, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice, 0.0 as NetBill, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Engine_No, Recv_D.Chassis_No, Recv_D.BatchID, isnull(Recv_D.BatchNo,'xxxx') as [Batch No],Recv_D.ExpiryDate, ISNULL(Recv_D.BardanaDeduction, 0) AS [Bardana Deduction] , ISNULL(Recv_D.OtherDeduction, 0) AS [Other Deduction], 0 AS [After Deduction Qty], Recv_D.Comments, Recv_D.Other_Comments as [Other Comments], IsNull(Recv_D.DeliveredQty,0) as DeliveredQty,IsNull(Stock.CurrStock,0) as Stock, IsNull(Recv_D.CostPrice,0) as CostPrice, Recv_D.DeliveryID, ISNULL(Recv_D.SO_Detail_ID, 0) As SalesOrderDetailId,IsNull(Recv_D.Gross_Weights, 0) As Gross_Weights,IsNull(Recv_D.Tray_Weights, 0) As Tray_Weights ,IsNull(Recv_D.Net_Weights, 0) As Net_Weights , IsNull(Recv_D.Qty, 0) as TotalQty  FROM DeliveryChalanDetailTable Recv_D INNER JOIN " _
            '          & " ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId inner JOIN " _
            '          & " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id LEFT OUTER JOIN SalesOrderDetailTable ON Recv_D.SO_Detail_ID = SalesOrderDetailTable.SalesOrderDetailId LEFT OUTER JOIN (Select ArticleDefID, SUM(IsNull(InQty,0)-ISNull(OutQty,0)) as CurrStock From StockDetailTable Group By ArticleDefId Having SUM(IsNull(InQty,0)-ISNull(OutQty,0)) <> 0) Stock On Stock.ArticleDefId = Recv_D.ArticleDefId " _
            '          & " Where Recv_D.DeliveryID =" & ReceivingID & " ORDER BY Recv_D.DeliveryDetailId Asc"
            ''Commented by Ayesha Rehman
            'Query = "SELECT Recv_D.LocationId, Article.ArticleCode as Code, Article.ArticleDescription AS Item, Article.ArticleSizeName as Size, Article.ArticleColorName as Color,  Recv_D.ArticleSize AS Unit, ISNULL(Recv_D.Sz1,0) OrderQty, ISNULL(DCQty,0) DeliverQty, ISNULL(Recv_D.Sz1,0) - Isnull(DCQty , 0) AS Qty, ISNULL(Recv_D.Sz1,0) - ISNULL(DCQty,0) RemainingQty, Recv_D.Price as Rate, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, " _
            '   & " ((IsNull(Recv_D.Qty, 0) * Recv_D.Price * Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) - isnull(Recv_D.DeliveredTotalQty,0)  * Recv_D.Price * Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) AS Total, " _
            '   & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as [Pack Qty] ,Recv_D.Price as CurrentPrice, IsNull(Recv_D.PackPrice, 0) as PackPrice, 0 as DeliveryDetailId, ISNULL(Recv_D.TradePrice,0) as TradePrice,  ISNULL(Recv_D.SalesTax_Percentage,0) as Tax,  Convert(float,IsNull(Recv_D.SED_Tax_Percent,0)) as SED, Convert(float,0) as savedqty,(ISNULL(Recv_D.SchemeQty,0)-ISNULL(Recv_D.DeliveredSchemeQty,0)) as [Sample Qty], ISNULL(Recv_D.Discount_Percentage,0) as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, 0 as So_Id, '' as UOM, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice,  0.0 as NetBill, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Engine_No, Recv_D.Chassis_No, 0 as BatchID, 'xxxx' as [Batch No], Convert(DateTime,Null) as ExpiryDate, Convert(float, 0) AS [Bardana Deduction] , Convert(float, 0) AS [Other Deduction], 0 AS [After Deduction Qty], Recv_D.Comments as Comments, Recv_D.Other_Comments as [Other Comments], Convert(float, 0) as DeliveredQty, IsNull(Stock.CurrStock,0) as Stock, IsNull(Recv_D.CostPrice,0) as CostPrice, 0 as DeliveryID, ISNULL(Recv_D.SalesOrderDetailId, 0) As SalesOrderDetailId, Convert(float, 0) As Gross_Weights, Convert(float, 0) As Tray_Weights , Convert(float, 0) As Net_Weights , (IsNull(Recv_D.Qty,0)- IsNull(Recv_D.DCTotalQty,0)) as TotalQty  FROM SalesOrderDetailTable Recv_D INNER JOIN " _
            '   & " ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
            '   & " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id INNER JOIN SalesOrderMasterTable Recv ON Recv.SalesOrderID = Recv_D.SalesOrderID  LEFT OUTER JOIN(Select ArticleDefID, SUM(IsNull(InQty,0)-ISNull(OutQty,0)) as CurrStock From StockDetailTable Group By ArticleDefId Having SUM(IsNull(InQty,0)-ISNull(OutQty,0)) <> 0) Stock On Stock.ArticleDefId = Recv_D.ArticleDefId  " _
            '   & " Where Recv_D.SalesOrderID =" & SalesOrderId & " AND (IsNull(Recv_D.Sz1,0) - Isnull(DCQty,0)) > 0 "

            Query = "SELECT Recv_D.LocationId, Article.ArticleCode as Code, Article.ArticleDescription AS Item, Article.ArticleSizeName as Size, Article.ArticleColorName as Color,  Recv_D.ArticleSize AS Unit, ISNULL(Recv_D.Sz1,0) OrderQty, ISNULL(DCQty,0) DeliverQty, ISNULL(Recv_D.Sz1,0) - Isnull(DCQty , 0) AS Qty, ISNULL(Recv_D.Sz1,0) - ISNULL(DCQty,0) RemainingQty,Recv_D.PostDiscountPrice, Recv_D.Price as Rate, IsNull(Recv_D.BaseCurrencyId, 0) As BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) As BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) As CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End As CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) As CurrencyAmount, " _
              & " ((IsNull(Recv_D.Qty, 0) * Recv_D.Price * Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) - isnull(Recv_D.DeliveredTotalQty,0)  * Recv_D.Price * Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End) AS Total,Isnull(Recv_D.DiscountId,1) as DiscountId , IsNull(Recv_D.DiscountFactor, 0) AS DiscountFactor, IsNull(Recv_D.DiscountValue, 0) As DiscountValue," _
              & " Article.ArticleGroupId, Recv_D.ArticleDefId,Sz7 as [Pack Qty] ,Recv_D.Price as CurrentPrice, IsNull(Recv_D.PackPrice, 0) as PackPrice, 0 as DeliveryDetailId, ISNULL(Recv_D.TradePrice,0) as TradePrice,  ISNULL(Recv_D.SalesTax_Percentage,0) as Tax,  Convert(float,IsNull(Recv_D.SED_Tax_Percent,0)) as SED, Convert(float,0) as savedqty,(ISNULL(Recv_D.SchemeQty,0)-ISNULL(Recv_D.DeliveredSchemeQty,0)) as [Sample Qty], ISNULL(Recv_D.Discount_Percentage,0) as Discount_Percentage, ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, ISNULL(Recv_D.SalesOrderId,0) as So_Id, '' as UOM, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice,  0.0 as NetBill, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Recv_D.Engine_No, Recv_D.Chassis_No, 0 as BatchID,  IsNull(Recv_D.BatchNo,'xxxx') as [Batch No], IsNull(Recv_D.ExpiryDate,DATEADD(Month , 1 , getDate())) as ExpiryDate, Convert(float, 0) AS [Bardana Deduction] , Convert(float, 0) AS [Other Deduction], 0 AS [After Deduction Qty], Recv_D.Comments as Comments, Recv_D.Other_Comments as [Other Comments], Convert(float, 0) as DeliveredQty, IsNull(Stock.CurrStock,0) as Stock, IsNull(Recv_D.CostPrice,0) as CostPrice, 0 as DeliveryID, ISNULL(Recv_D.SalesOrderDetailId, 0) As SalesOrderDetailId, Convert(float, 0) As Gross_Weights, Convert(float, 0) As Tray_Weights , Convert(float, 0) As Net_Weights , (IsNull(Recv_D.Qty,0)- IsNull(Recv_D.DCTotalQty,0)) as TotalQty  FROM SalesOrderDetailTable Recv_D INNER JOIN " _
               & " ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT OUTER JOIN " _
               & " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id INNER JOIN SalesOrderMasterTable Recv ON Recv.SalesOrderID = Recv_D.SalesOrderID  LEFT OUTER JOIN(Select ArticleDefID, SUM(IsNull(InQty,0)-ISNull(OutQty,0)) as CurrStock From StockDetailTable Group By ArticleDefId Having SUM(IsNull(InQty,0)-ISNull(OutQty,0)) <> 0) Stock On Stock.ArticleDefId = Recv_D.ArticleDefId  " _
              & " Where Recv_D.SalesOrderID =" & SalesOrderId & ""
            If flgLoadItemAfterDeliveredOnDC = False Then
                Query += " AND (IsNull(Recv_D.Sz1,0) - Isnull(DCQty,0)) > 0 "
            End If
            dt = GetDataTable(Query)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function DisplayDeliveryChalans(ByVal DeliveryChalanId As Integer)
        Dim dt As DataTable
        Dim Query As String = String.Empty
        Try
            ''Commented Against TFS3053 : Ayesha Rehman : 17-04-2018
            'Query = " SELECT Recv_D.LocationId, Article.ArticleCode as Code, Article.ArticleDescription AS Item, Article.ArticleSizeName AS Size, Article.ArticleColorName AS Color , Recv_D.ArticleSize AS Unit, Convert(Decimal(18, " & DecimalPointInQty & "), (Isnull(Recv_D.Sz1,0)-(isnull(Recv_D.DeliveredQty,0)+IsNull(SODetail.DeliveredQty, 0))), 1) AS Qty, " _
            '       & " Recv_D.Price As PostDiscountPrice,  0 As DiscountId, ' ' as DiscountType,ISNULL(Recv_D.Discount_Percentage,0) as Discount_Percentage, Convert(float, 0) As FlatDiscount, Convert(float, 0) As DiscountFactor, Convert(float, 0) As DiscountValue,   " _
            '       & " Recv_D.Price as Rate, IsNull(Recv_D.BaseCurrencyId, 0) AS BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) AS BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) AS CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End AS CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) AS CurrencyAmount, Convert(float,0) as [Total Currency Amount], " _
            '       & " ((IsNull(Recv_D.Qty, 0) * Recv_D.Price * (Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End)) - IsNull(Recv_D.DeliveredTotalQty, 0) * Recv_D.Price * Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End) AS Total, " _
            '       & " Article.ArticleGroupId,Recv_D.ArticleDefId, Recv_D.Sz7 as [Pack Qty], ISNULL(Recv_D.CurrentPrice,0) as CurrentPrice, 0 as PackPrice, 0 As SaleDetailId, Convert(float, 0) as RetailPrice,  ISNULL(Recv_D.TradePrice,0) as TradePrice, isnull(Recv_D.TaxPercent,0) as Tax, Convert(float,0) as [Tax Amount], Convert(float,0) as [Currency Tax Amount], ISNULL(Recv_D.SEDPercent,0) As SED, Convert(float,0) as [Total Amount], 0 as savedqty , Convert(float, 0) as [Sample Qty], ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, ISNULL(So_ID,0) as So_Id,  Convert(Decimal(18, " & DecimalPointInQty & "), isnull(Recv_D.Sz1,0), 1) as LoadQty, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice , 0 as NetBill,  Recv_D.BatchID, isnull(Recv_D.BatchNo,'') as [Batch No], Recv_D.ExpiryDate, IsNull(Recv_D.BardanaDeduction, 0) AS [Bardana Deduction], IsNull(Recv_D.OtherDeduction, 0) AS [Other Deduction], Convert(float,0) AS [After Deduction Qty], Recv_D.comments as Comments, Recv_D.Other_Comments as [Other Comments], Recv_D.Engine_No, Recv_D.Chassis_No, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article.SubSubId,0) as InvAccountId, Article.SalesAccountId, Article.CGSAccountId, Convert(float, 0) as CostPrice, 0 as CR_SalesDetailId, 0 as SaleCertificateId, IsNull(Stock.CurrStock,0) as Stock, Convert(float, 0) as SalesReturnQty, IsNull(Recv_D.DeliveryID,0) as DeliveryChalanId, IsNull(Recv_D.DeliveryDetailId,0) as DeliveryChalanDetailId, Convert(float, 0) as Transport_Charges, Convert(bit,1) as ApplyAdjustmentFuelExp, IsNull(Recv_D.Gross_Weights, 0) As Gross_Weights,IsNull(Recv_D.Tray_Weights, 0) As Tray_Weights, IsNull(Recv_D.Net_Weights, 0) As Net_Weights, Convert(Decimal(18, " & DecimalPointInQty & "), (IsNull(Recv_D.Qty,0) - (IsNull(Recv_D.DeliveredTotalQty, 0)+IsNull(SODetail.DeliveredTotalQty, 0))), 1) as TotalQty, IsNull(Recv_D.SO_Detail_ID, 0) As SODetailId, '' As ArticleLpoName, Convert(float, 0) As OldSalePrice, 0 As SalePriceProcessId  , IsNull(Article.LogicalItem, 0) AS LogicalItem  FROM DeliveryChalanDetailTable Recv_D INNER JOIN " _
            '       & " ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT JOIN " _
            '       & " SalesOrderDetailTable AS SODetail ON Recv_D.SO_Detail_ID = SODetail.SalesOrderDetailId LEFT JOIN " _
            '       & " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id  LEFT OUTER JOIN(Select ArticleDefID, SUM(IsNull(InQty,0)-ISNull(OutQty,0)) as CurrStock From StockDetailTable Group By ArticleDefId Having SUM(IsNull(InQty,0)-ISNull(OutQty,0)) <> 0) Stock On Stock.ArticleDefId = Recv_D.ArticleDefId " _
            '       & " Where (Isnull(Recv_D.Sz1,0)-(isnull(Recv_D.DeliveredQty,0)+IsNull(SODetail.DeliveredQty, 0))) > 0 "

            Query = " SELECT Recv_D.LocationId, Article.ArticleCode as Code, Article.ArticleDescription AS Item, Article.ArticleSizeName AS Size, Article.ArticleColorName AS Color , Recv_D.ArticleSize AS Unit, Convert(Decimal(18, " & DecimalPointInQty & "), (Isnull(Recv_D.Sz1,0)-isnull(Recv_D.DeliveredQty,0)), 1) AS Qty, " _
                  & " Recv_D.PostDiscountPrice As PostDiscountPrice, Recv_D.DiscountId  As DiscountId, '' as DiscountType,ISNULL(Recv_D.Discount_Percentage,0) as Discount_Percentage, Convert(float, 0) As FlatDiscount, Recv_D.DiscountFactor As DiscountFactor, Recv_D.DiscountValue As DiscountValue,   " _
                   & " Recv_D.Price as Rate, IsNull(Recv_D.BaseCurrencyId, 0) AS BaseCurrencyId, IsNull(Recv_D.BaseCurrencyRate, 0) AS BaseCurrencyRate, IsNull(Recv_D.CurrencyId, 0) AS CurrencyId, Case When IsNull(Recv_D.CurrencyRate, 0)=0 Then 1 Else Recv_D.CurrencyRate End AS CurrencyRate, IsNull(Recv_D.CurrencyAmount, 0) AS CurrencyAmount, Convert(float,0) as [Total Currency Amount], " _
                   & " ((IsNull(Recv_D.Qty, 0) * Recv_D.Price * (Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End)) - IsNull(Recv_D.DeliveredTotalQty, 0) * Recv_D.Price * Case When IsNull(Recv_D.CurrencyRate, 0) = 0 Then 1 Else Recv_D.CurrencyRate End) AS Total, " _
                   & " Article.ArticleGroupId,Recv_D.ArticleDefId, Recv_D.Sz7 as [Pack Qty], ISNULL(Recv_D.CurrentPrice,0) as CurrentPrice, 0 as PackPrice, 0 As SaleDetailId, Convert(float, 0) as RetailPrice,  ISNULL(Recv_D.TradePrice,0) as TradePrice, isnull(Recv_D.TaxPercent,0) as Tax, Convert(float,0) as [Tax Amount], Convert(float,0) as [Currency Tax Amount], ISNULL(Recv_D.SEDPercent,0) As SED, Convert(float,0) as [Total Amount], 0 as savedqty , Convert(float, 0) as [Sample Qty], ISNULL(Recv_D.Freight,0) as Freight, ISNULL(Recv_D.MarketReturns,0) as MarketReturns, ISNULL(So_ID,0) as So_Id,  Convert(Decimal(18, " & DecimalPointInQty & "), isnull(Recv_D.Sz1,0), 1) as LoadQty, Isnull(Recv_D.PurchasePrice,0) as PurchasePrice , 0 as NetBill,  Recv_D.BatchID, isnull(Recv_D.BatchNo,'xxxx') as [Batch No], IsNull(Recv_D.ExpiryDate,DATEADD(Month , 1 , getDate())) as ExpiryDate, IsNull(Recv_D.BardanaDeduction, 0) AS [Bardana Deduction], IsNull(Recv_D.OtherDeduction, 0) AS [Other Deduction], Convert(float,0) AS [After Deduction Qty], Recv_D.comments as Comments, Recv_D.Other_Comments as [Other Comments], Recv_D.Engine_No, Recv_D.Chassis_No, Isnull(Recv_D.Pack_Desc,Recv_D.ArticleSize) as Pack_Desc, Isnull(Article.SubSubId,0) as InvAccountId, Article.SalesAccountId, Article.CGSAccountId, IsNull(Recv_D.CostPrice,0) as CostPrice, 0 as CR_SalesDetailId, 0 as SaleCertificateId, IsNull(Stock.CurrStock,0) as Stock, Convert(float, 0) as SalesReturnQty, IsNull(Recv_D.DeliveryID,0) as DeliveryChalanId, IsNull(Recv_D.DeliveryDetailId,0) as DeliveryChalanDetailId, Convert(float, 0) as Transport_Charges, ISNULL(Article.ApplyAdjustmentFuelExp,1) as  ApplyAdjustmentFuelExp, IsNull(Recv_D.Gross_Weights, 0) As Gross_Weights,IsNull(Recv_D.Tray_Weights, 0) As Tray_Weights, IsNull(Recv_D.Net_Weights, 0) As Net_Weights, Convert(Decimal(18, " & DecimalPointInQty & "), (IsNull(Recv_D.Qty,0) - (IsNull(Recv_D.DeliveredTotalQty, 0))), 1) as TotalQty, IsNull(Recv_D.SO_Detail_ID, 0) As SODetailId, '' As ArticleLpoName, Convert(float, 0) As OldSalePrice, 0 As SalePriceProcessId  , IsNull(Article.LogicalItem, 0) AS LogicalItem  FROM DeliveryChalanDetailTable Recv_D INNER JOIN " _
                   & " ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId LEFT JOIN " _
                   & " SalesOrderDetailTable AS SODetail ON Recv_D.SO_Detail_ID = SODetail.SalesOrderDetailId LEFT JOIN " _
                   & " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id  LEFT OUTER JOIN(Select ArticleDefID, SUM(IsNull(InQty,0)-ISNull(OutQty,0)) as CurrStock From StockDetailTable Group By ArticleDefId Having SUM(IsNull(InQty,0)-ISNull(OutQty,0)) <> 0) Stock On Stock.ArticleDefId = Recv_D.ArticleDefId " _
                  & " Where (Isnull(Recv_D.Sz1,0)-isnull(Recv_D.DeliveredQty,0)) > 0 "
            'If Not FromDate = Nothing AndAlso Not ToDate = Nothing Then
            '    Query += " And CONVERT(varchar, Recv.DeliveryDate, 102) BETWEEN Convert(datetime, N'" & FromDate.ToString("yyyy-M-d 00:00:00") & "') AND Convert(datetime, N'" & ToDate.ToString("yyyy-M-d 23:59:59") & "')"
            'End If
            'If Not FromDate = Nothing AndAlso ToDate = Nothing Then
            '    Query += " And CONVERT(varchar, Recv.DeliveryDate, 102) >= Convert(datetime, N'" & FromDate.ToString("yyyy-M-d 00:00:00") & "')"
            'End If
            'If FromDate = Nothing AndAlso Not ToDate = Nothing Then
            '    Query += " And CONVERT(varchar, Recv.DeliveryDate, 102) <= Convert(datetime, N'" & ToDate.ToString("yyyy-M-d 23:59:59") & "')"
            'End If
            'If CustomerId > 0 AndAlso DeliveryChalanId < 1 Then
            '    Query += " And Recv.CustomerCode = " & CustomerId & ""
            'ElseIf CustomerId > 0 AndAlso DeliveryChalanId > 0 Then
            '    Query += " And Recv.CustomerCode = " & CustomerId & " And Recv.DeliveryId = " & DeliveryChalanId & ""
            'ElseIf CustomerId < 1 AndAlso DeliveryChalanId > 0 Then
            Query += " And Recv_D.DeliveryId = " & DeliveryChalanId & " ORDER BY Recv_D.DeliveryDetailId Asc "
            'End If
            dt = GetDataTable(Query)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Private Sub FillGrid(ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal CustomerId As Integer, ByVal DeliveryChalanId As Integer)
        Try
            'If 
            If Me.rbSO.Checked = True Then
                If flgMultipleSalesOrder = True Then
                    For Each _Row As Janus.Windows.GridEX.GridEXRow In grd.GetRows
                        If _Row.Cells("SalesOrderId").Value = DeliveryChalanId Then
                            ShowErrorMessage("Selected Sales Order has already been added.")
                            Exit Sub
                        End If
                    Next
                    Dim dtGrid As DataTable = CType(Me.grd.DataSource, DataTable)
                    If Not dtGrid Is Nothing Then
                        Dim dtNew As DataTable = Me.GetSalesOrders(FromDate, ToDate, CustomerId, DeliveryChalanId)
                        dtGrid.Merge(dtNew)
                    Else
                        Me.grd.DataSource = Me.GetSalesOrders(FromDate, ToDate, CustomerId, DeliveryChalanId)
                    End If
                Else
                    Me.grd.DataSource = Me.GetSalesOrders(FromDate, ToDate, CustomerId, DeliveryChalanId)
                End If
            Else
                If flgLoadMultiChalan = True Then
                    For Each _Row As Janus.Windows.GridEX.GridEXRow In grd.GetRows
                        If _Row.Cells("DeliveryChalanId").Value = DeliveryChalanId Then
                            ShowErrorMessage("Selected Delivery chalan has already been added.")
                            Exit Sub
                        End If
                    Next
                    Dim dtGrid As DataTable = CType(Me.grd.DataSource, DataTable)
                    If Not dtGrid Is Nothing Then
                        Dim dtNew As DataTable = Me.GetDeliveryChalans(FromDate, ToDate, CustomerId, DeliveryChalanId)
                        dtGrid.Merge(dtNew)
                    Else
                        Me.grd.DataSource = Me.GetDeliveryChalans(FromDate, ToDate, CustomerId, DeliveryChalanId)
                    End If
                Else
                    Me.grd.DataSource = Me.GetDeliveryChalans(FromDate, ToDate, CustomerId, DeliveryChalanId)
                End If
                'Me.grd.DataSource = Me.GetDeliveryChalans(FromDate, ToDate, CustomerId, DeliveryChalanId)
            End If
            Me.grd.RetrieveStructure()
            Me.grd.RootTable.Columns("Column1").ActAsSelector = True
            Me.grd.RootTable.Columns("Column1").UseHeaderSelector = True
            Me.grd.RootTable.Columns("Column1").Width = 25
            'Me.grd.RootTable.Columns("Column1").CheckBoxTrueValue = True
            Me.grd.RootTable.Columns("DeliveryChalanId").Visible = False
            Me.grd.RootTable.Columns("SalesOrderId").Visible = False
            'End
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub rbSO_CheckedChanged(sender As Object, e As EventArgs) Handles rbSO.CheckedChanged
        Try
            If rbSO.Checked = True Then
                Me.lblSO.Text = _SO
                GetSOForSales(Me.cmbCustomer.Value)
            Else
                Me.lblSO.Text = _DC
                GetSOForSales(Me.cmbCustomer.Value)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbCustomer_SelectedIndexChanged(sender As Object, e As EventArgs)
        Try
            If Me.cmbCustomer.Value > 0 Then
                If Me.rbSO.Checked = True AndAlso _IsDC = False Then
                    GetSOForSales(Me.cmbCustomer.Value)
                ElseIf Me.rbDC.Checked = True AndAlso _IsDC = False Then
                    GetDCForSales(Me.cmbCustomer.Value)
                ElseIf _IsDC = True Then
                    GetSOForDC(Me.cmbCustomer.Value)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub rbDC_CheckedChanged(sender As Object, e As EventArgs) Handles rbDC.CheckedChanged
        Try
            If rbDC.Checked = True Then
                Me.lblSO.Text = _DC
                GetDCForSales(Me.cmbCustomer.Value)
                If Me.grd.RowCount > 0 Then
                    Me.grd.DataSource = Nothing
                End If
            Else
                Me.lblSO.Text = _SO
                GetSOForSales(Me.cmbCustomer.Value)
                If Me.grd.RowCount > 0 Then
                    Me.grd.DataSource = Nothing
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbSO_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSO.SelectedIndexChanged
        Try
            Dim FromDate As DateTime
            Dim ToDate As DateTime
            If Me.DateTimePicker1.Checked = True Then
                FromDate = Me.DateTimePicker1.Value
            Else
                FromDate = Nothing
            End If
            If Me.DateTimePicker2.Checked = True Then
                ToDate = Me.DateTimePicker2.Value
            Else
                ToDate = Nothing
            End If
            If Me.cmbSO.SelectedValue > 0 Then
                FillGrid(FromDate, ToDate, Me.cmbCustomer.Value, Me.cmbSO.SelectedValue)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_KeyDown(sender As Object, e As KeyEventArgs) Handles grd.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim CheckedRows() As Janus.Windows.GridEX.GridEXRow = Me.grd.GetCheckedRows
                Dim _row1 As Janus.Windows.GridEX.GridEXRow
                Dim globeDT As DataTable
                If _IsDC = False Then
                    globeDT = _dtSales.Clone
                Else
                    globeDT = _dtDeliveryChalan.Clone
                End If

                If Not CheckedRows.Length > 0 Then
                    ShowErrorMessage("At least one selected row is required.")
                    Exit Sub
                Else
                    Dim localDT As DataTable = globeDT.Clone()
                    For Each _Row As Janus.Windows.GridEX.GridEXRow In CheckedRows
                        'Dim Row As DataRow = CType(_Row.DataRow, DataRowView).Row
                        If _IsDC = False Then
                            If Me.rbSO.Checked = True Then
                                localDT = DisplaySOrders(Val(_Row.Cells("SalesOrderId").Value.ToString))
                                globeDT.Merge(localDT)
                            ElseIf Me.rbDC.Checked = True Then
                                localDT = DisplayDeliveryChalans(Val(_Row.Cells("DeliveryChalanId").Value.ToString))
                                globeDT.Merge(localDT)
                            End If
                        Else
                            localDT = DisplaySOrdersForDC(Val(_Row.Cells("SalesOrderId").Value.ToString))
                            globeDT.Merge(localDT)
                        End If
                    Next
                    Dim SalesOrderId As Integer = 0
                    Dim DeliveryChalanId As Integer = 0
                    If rbDC.Checked = True Then
                        DeliveryChalanId = Me.cmbSO.SelectedValue
                        SalesOrderId = Val(CType(Me.cmbSO.SelectedItem, DataRowView).Item("SalesOrderId1"))
                    Else
                        SalesOrderId = Me.cmbSO.SelectedValue
                        DeliveryChalanId = 0
                    End If
                    If _IsDC = False Then
                        frmSales.LoadFromPopup(globeDT, Me.cmbCustomer.Value, SalesOrderId, DeliveryChalanId)
                    Else
                        frmDeliveryChalan.LoadSO(globeDT, Me.cmbCustomer.Value, Me.cmbSO.SelectedValue)
                    End If
                    'frmSales.grd.DataSource = CheckedRows
                    'frmSales.grd.RetrieveStructure()
                    Me.Close()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Function GetSalesOrders(ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal CustomerId As Integer, ByVal SalesOrderId As Integer)
        Dim dt As DataTable
        Dim Query As String = String.Empty
        Try
            Query = "SELECT 0 AS Column1, vwCOADetail.detail_title AS Customer, Recv.SalesOrderId AS SalesOrderId, Recv.SalesOrderNo AS [SO No], " _
        & " Recv.SalesOrderDate AS [SO Date], 0 AS DeliveryChalanId, '' AS [DC No], '' AS [DC Date], ISNULL(Recv.SalesOrderQty, 0) AS Qty, ISNULL(Recv.SalesOrderAmount, 0) AS Amount " _
        & " FROM  SalesOrderMasterTable AS Recv LEFT OUTER JOIN vwCOADetail ON Recv.VendorId = vwCOADetail.coa_detail_id " _
        & " WHERE Recv.SalesOrderId > 0 "
            If Not FromDate = Nothing AndAlso Not ToDate = Nothing Then
                Query += " And CONVERT(varchar, Recv.SalesOrderDate, 102) BETWEEN Convert(datetime, N'" & FromDate.ToString("yyyy-M-d 00:00:00") & "') AND Convert(datetime, N'" & ToDate.ToString("yyyy-M-d 23:59:59") & "')"
            End If
            If Not FromDate = Nothing AndAlso ToDate = Nothing Then
                Query += " And CONVERT(varchar, Recv.SalesOrderDate, 102) >= Convert(datetime, N'" & FromDate.ToString("yyyy-M-d 00:00:00") & "')"
            End If
            If FromDate = Nothing AndAlso Not ToDate = Nothing Then
                Query += " And CONVERT(varchar, Recv.SalesOrderDate, 102) <= Convert(datetime, N'" & ToDate.ToString("yyyy-M-d 23:59:59") & "')"
            End If
            If CustomerId > 0 AndAlso SalesOrderId < 1 Then
                Query += " And Recv.VendorId = " & CustomerId & ""
            ElseIf CustomerId > 0 AndAlso SalesOrderId > 0 Then
                Query += " And Recv.VendorId = " & CustomerId & " And Recv.SalesOrderId = " & SalesOrderId & ""
            ElseIf CustomerId < 1 AndAlso SalesOrderId > 0 Then
                Query += " And Recv.SalesOrderId = " & SalesOrderId & ""
            End If
            dt = GetDataTable(Query)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function GetDeliveryChalans(ByVal FromDate As DateTime, ByVal ToDate As DateTime, ByVal CustomerId As Integer, ByVal DeliveryChalanId As Integer)
        Dim dt As DataTable
        Dim Query As String = String.Empty
        Try
            Query = "SELECT 0 AS Column1, vwCOADetail.detail_title AS Customer, 0 AS SalesOrderId, '' AS [SO No], " _
        & " '' AS [SO Date], Recv.DeliveryId AS DeliveryChalanId, Recv.DeliveryNo AS [DC No], Recv.DeliveryDate AS [DC Date], ISNULL(Recv.DeliveryQty, 0) AS Qty, ISNULL(Recv.DeliveryAmount, 0) AS Amount " _
        & " FROM  DeliveryChalanMasterTable AS Recv LEFT OUTER JOIN vwCOADetail ON Recv.CustomerCode = vwCOADetail.coa_detail_id " _
        & " WHERE Recv.DeliveryId > 0 "
            If Not FromDate = Nothing AndAlso Not ToDate = Nothing Then
                Query += " And CONVERT(varchar, Recv.DeliveryDate, 102) BETWEEN Convert(datetime, N'" & FromDate.ToString("yyyy-M-d 00:00:00") & "') AND Convert(datetime, N'" & ToDate.ToString("yyyy-M-d 23:59:59") & "')"
            End If
            If Not FromDate = Nothing AndAlso ToDate = Nothing Then
                Query += " And CONVERT(varchar, Recv.DeliveryDate, 102) >= Convert(datetime, N'" & FromDate.ToString("yyyy-M-d 00:00:00") & "')"
            End If
            If FromDate = Nothing AndAlso Not ToDate = Nothing Then
                Query += " And CONVERT(varchar, Recv.DeliveryDate, 102) <= Convert(datetime, N'" & ToDate.ToString("yyyy-M-d 23:59:59") & "')"
            End If
            If CustomerId > 0 AndAlso DeliveryChalanId < 1 Then
                Query += " And Recv.CustomerCode = " & CustomerId & ""
            ElseIf CustomerId > 0 AndAlso DeliveryChalanId > 0 Then
                Query += " And Recv.CustomerCode = " & CustomerId & " And Recv.DeliveryId = " & DeliveryChalanId & ""
            ElseIf CustomerId < 1 AndAlso DeliveryChalanId > 0 Then
                Query += " And Recv.DeliveryId = " & DeliveryChalanId & ""
            End If
            dt = GetDataTable(Query)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim ID As Integer = 0
        Try
            Me.rbSO.Checked = True
            GetCustomers()
            Me.grd.DataSource = Nothing
            'ID = Me.cmbCustomer.SelectedValue
            'GetCustomers()
            'Me.cmbCustomer.SelectedValue = ID
            ''ID = Me.cmbCustomer.SelectedValue
            'GetCustomers()
            'Me.cmbCustomer.SelectedValue = ID
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnLoad_Click(sender As Object, e As EventArgs) Handles btnLoad.Click
        Try
            LoadRecord()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub LoadRecord()
        Try
            Dim CheckedRows() As Janus.Windows.GridEX.GridEXRow = Me.grd.GetCheckedRows
            Dim _row1 As Janus.Windows.GridEX.GridEXRow
            Dim globeDT As DataTable
            If _IsDC = False Then
                globeDT = _dtSales.Clone
            Else
                globeDT = _dtDeliveryChalan.Clone
            End If

            If Not CheckedRows.Length > 0 Then
                ShowErrorMessage("At least one selected row is required.")
                Exit Sub
            Else
                Dim localDT As DataTable = globeDT.Clone()
                For Each _Row As Janus.Windows.GridEX.GridEXRow In CheckedRows
                    'Dim Row As DataRow = CType(_Row.DataRow, DataRowView).Row
                    If _IsDC = False Then
                        If Me.rbSO.Checked = True Then
                            localDT = DisplaySOrders(Val(_Row.Cells("SalesOrderId").Value.ToString))
                            globeDT.Merge(localDT)
                        ElseIf Me.rbDC.Checked = True Then
                            localDT = DisplayDeliveryChalans(Val(_Row.Cells("DeliveryChalanId").Value.ToString))
                            globeDT.Merge(localDT)
                        End If
                    Else
                        localDT = DisplaySOrdersForDC(Val(_Row.Cells("SalesOrderId").Value.ToString))
                        globeDT.Merge(localDT)
                    End If
                Next
                Dim SalesOrderId As Integer = 0
                Dim DeliveryChalanId As Integer = 0
                If rbDC.Checked = True Then
                    DeliveryChalanId = Me.cmbSO.SelectedValue
                    SalesOrderId = Val(CType(Me.cmbSO.SelectedItem, DataRowView).Item("SalesOrderId1"))
                Else
                    SalesOrderId = Me.cmbSO.SelectedValue
                    DeliveryChalanId = 0
                End If
                If _IsDC = False Then
                    frmSales.LoadFromPopup(globeDT, Me.cmbCustomer.Value, SalesOrderId, DeliveryChalanId)
                Else
                    frmDeliveryChalan.LoadSO(globeDT, Me.cmbCustomer.Value, Me.cmbSO.SelectedValue)
                End If
                'frmSales.grd.DataSource = CheckedRows
                'frmSales.grd.RetrieveStructure()
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cmbCustomer_ValueChanged(sender As Object, e As EventArgs) Handles cmbCustomer.ValueChanged
        Try
            If Me.cmbCustomer.Value > 0 Then
                If Me.rbSO.Checked = True AndAlso _IsDC = False Then
                    GetSOForSales(Me.cmbCustomer.Value)
                ElseIf Me.rbDC.Checked = True AndAlso _IsDC = False Then
                    GetDCForSales(Me.cmbCustomer.Value)
                ElseIf _IsDC = True Then
                    GetSOForDC(Me.cmbCustomer.Value)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''Start TFS3326
    ''' <summary>
    ''' This Function is made to check if any Delivery chalan exist against the selected SalesOrder
    ''' </summary>
    ''' <param name="SalesOrderId"></param>
    ''' <returns></returns>
    ''' <remarks>TFS3326 : Ayesha Rehman : 31-05-2018</remarks>
    Private Function IsDCExistAgainstSO(ByVal SalesOrderId As Integer) As Boolean
        Try

            Dim str As String = "Select DeliveryId from DeliveryChalanMasterTable Where POId = " & SalesOrderId & " "
            Dim dt As DataTable = GetDataTable(str)
            If dt.Rows.Count > 0 Then
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function IsDCExistInSales(ByVal SalesOrderId As Integer, ByRef DcQtyInSales As Integer) As Boolean
        Try

            Dim str As String = "Select DeliveryId from DeliveryChalanMasterTable Where POId = " & SalesOrderId & " "
            Dim dtDC As DataTable = GetDataTable(str)
            DcQtyInSales = 0
            If dtDC.Rows.Count > 0 Then
                For Each Row As DataRow In dtDC.Rows
                    Dim dtSales As DataTable = GetDataTable("Select * from SalesMasterTable inner join SalesDetailTable on SalesMasterTable.SalesId = SalesDetailTable.SalesId inner join SalesOrderMasterTable on SalesDetailTable.So_Id = SalesOrderMasterTable.SalesOrderId where So_Id = " & SalesOrderId & " " _
                                                            & " and SalesDetailTable.DeliveryChalanID = " & Val(Row(0).ToString) & "")
                    If dtSales.Rows.Count > 0 Then
                        For Each r As DataRow In dtSales.Rows
                            Dim dtDCQty As DataTable = GetDataTable("SELECT Sum(ISNULL(Sz1,0)) as Qty FROM DeliveryChalanDetailTable WHERE DeliveryId = " & Val(Row(0).ToString) & " And SO_Id = " & SalesOrderId & "")
                            If dtDCQty.Rows.Count > 0 Then
                                DcQtyInSales = Val(dtDCQty.Rows(0).Item(0).ToString)

                            End If
                        Next
                        Return True
                    End If

                Next
            End If
            Return False
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''End TFS3326
End Class
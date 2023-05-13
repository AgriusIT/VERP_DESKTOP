'TFS4393 : Ayesha Rehman : 12-Sep-2018 : Loss data save on POS screen if system is powered off directly.
''TFS4733 : Ayesha Rehman : 04-Oct-2018 : When item is scanned then scroll should move to the last on that item.
''TFS4732 : Ayesha Rehman : 04-Oct-2018 : Qty on POS can be entered in decimal points.
''TFS4738 : Ayesha Rehman : 04-Oct-2018 : Option to show Last Scanned item on POS screen is required.
Imports System.Data.OleDb
Imports SBModel
Imports SBDal
Imports System
Public Class frmPOSEntry
    Implements IGeneral

    Enum grdDetail
        ArticleId
        ArticleCode
        ArticleDescription
        ArticleColorName
        ArticleSizeName
        ArticleUnitName
        PackSize
        Qty
        PackQty
        TotalQty
        PDP
        DiscountId
        DiscFactor
        DiscValue
        Rate
        PurchasePrice
        PackRate
        TotalAmount
        Tax
        NetAmount
        DeleteButton
    End Enum
    ''TFS4732 : Query Edit to get Qty,Pack Qty ,Toatl Qty in Decimal Points
    Dim ItemQry = "SELECT     ArticleId, ArticleCode, ArticleDescription, ArticleColorName, ArticleSizeName, ArticleUnitName,  '' as PackSize, CONVERT(Decimal(18," & DecimalPointInQty & "), 1) AS Qty, CONVERT(Decimal(18," & DecimalPointInQty & "), 1) AS PackQty,  CONVERT(Decimal(18," & DecimalPointInQty & "), 1) AS TotalQty,  CONVERT(Decimal(18," & DecimalPointInValue & "), 0) PDP, 0 as DiscountId, CONVERT(Decimal(18," & DecimalPointInValue & "), 0) DiscFactor, CONVERT(Decimal(18," & DecimalPointInValue & "), 0) DiscValue, CONVERT(Decimal(18," & DecimalPointInValue & "), 0) Rate, ISNULL(PurchasePrice, 0) as PurchasePrice, 0 AS PackRate, CONVERT(Decimal(18," & DecimalPointInValue & "), 0) TotalAmount, CONVERT(Decimal(18," & DecimalPointInValue & "), 0) Tax, CONVERT(Decimal(18," & DecimalPointInValue & "), 0) NetAmount FROM ArticleDefView WHERE ArticleId = "

    Dim TransId As Integer
    Dim GridDT As DataTable
    Dim SalesDetailId As Integer = 0
    Dim flgCompanyRights As Boolean = False
    Dim IsEditMode As Boolean = False
    Public CID As Integer
    Public LID As Integer
    Public CCID As Integer
    Public CAID As Integer
    Public BAID As Integer
    Public SPID As Integer
    Public DevOption As String
    Public Shared Title As String
    Private Mobile As String
    Private Code As Integer = 0
    Private Limit As Double
    Private CName As String
    Private PreviousBalance As Double
    Dim CompanyBasePrefix As Boolean = False
    Dim flgLoadAllItems As Boolean = False
    Dim flgExcludeTaxPrice As Boolean = False
    Dim IsFormLoaded As Boolean = False
    Dim setVoucherNo As String = String.Empty
    Dim InvId As Integer = 0
    Dim StockList As List(Of StockDetail)
    Dim StockDetail As StockDetail
    Dim flgAvgRate As Boolean = False
    Dim PrintLog As PrintLogBE
    Dim AdjustmentExpAccount As Integer
    Dim Price As String = String.Empty
    Dim PDP As String = String.Empty
    Dim StockMaster As StockMaster
    Dim flgMargeItem As Boolean = False
    Public Shared Cash As Double
    Public Shared Credit As Double
    Public Shared Bank As Double
    Public Shared CreditCard As Double
    Public Shared ChequeNo As Double
    Public Shared CCAID As Integer
    Private CreditCardId As Integer
    Public Shared LocationId As Integer
    Public Shared Email As Integer
    Public Shared SalesId As Integer
    Dim VoucherId As Integer = 0I
    Dim Voucher_No As String = ""
    Dim VoucherDate As DateTime
    Dim SalesStartTime As DateTime = Date.MinValue
    Dim SalesEndTime As DateTime = Date.MinValue
    Dim HoldStartTime As DateTime = Date.MinValue
    Dim HoldEndTime As DateTime = Date.MinValue
    Dim CreditRight As Boolean = False
    Dim RateVisible As Boolean = False
    Public Shared Online As Boolean = False
    Dim ArticleId As Integer
    Dim DiscountId As Integer
    Dim DiscFactor As Double
    Dim PackName As String
    Dim PrintId As Integer = 0
    Public Shared IsDeleteRights As Boolean = False
    Public Shared IsUpdateAllRights As Boolean = False
    Public Shared IsGetAllPOSRights As Boolean = False
    Dim ExistingVoucherFlg As Boolean = False
    'Ali Faisal : TFS4415 : Configuration based POS revision is require
    Dim IsRevisionRestrictedFirstTime As Boolean = False

    Shared Property DiscountPer As Double
    Dim Remarks As String = ""
    Dim Comments As String = ""

    Public Sub ApplyGridSettings(Optional Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try

            If Me.grd.RootTable.Columns.Contains("Delete") = False Then
                Me.grd.RootTable.Columns.Add("Delete")
                Me.grd.RootTable.Columns("Delete").ButtonDisplayMode = Janus.Windows.GridEX.CellButtonDisplayMode.Always
                Me.grd.RootTable.Columns("Delete").ButtonStyle = Janus.Windows.GridEX.ButtonStyle.ButtonCell
                Me.grd.RootTable.Columns("Delete").ButtonText = "Delete"
                Me.grd.RootTable.Columns("Delete").Key = "Delete"
                Me.grd.RootTable.Columns("Delete").Caption = "Action"
            End If
            Me.grd.RootTable.Columns(grdDetail.ArticleId).Visible = False
            Me.grd.RootTable.Columns(grdDetail.Qty).FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns(grdDetail.PackQty).FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns(grdDetail.PackQty).TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns(grdDetail.Qty).TotalFormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns(grdDetail.PDP).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdDetail.PDP).TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdDetail.PDP).Caption = "Price"
            Me.grd.RootTable.Columns(grdDetail.DiscountId).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdDetail.DiscFactor).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdDetail.DiscValue).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdDetail.Rate).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdDetail.Rate).TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdDetail.PurchasePrice).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdDetail.PackRate).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdDetail.PackRate).Visible = False
            Me.grd.RootTable.Columns(grdDetail.TotalAmount).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdDetail.TotalAmount).TotalFormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns(grdDetail.TotalAmount).FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns(grdDetail.Tax).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdDetail.Tax).TotalFormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdDetail.NetAmount).FormatString = "N" & DecimalPointInValue
            Me.grd.RootTable.Columns(grdDetail.NetAmount).FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns(grdDetail.NetAmount).TotalFormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns(grdDetail.TotalQty).FormatString = "N" & DecimalPointInQty
            Me.grd.RootTable.Columns(grdDetail.TotalQty).TotalFormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns(grdDetail.TotalQty).FormatString = "N" & TotalAmountRounding
            Me.grd.RootTable.Columns(grdDetail.ArticleId).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns(grdDetail.ArticleCode).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns(grdDetail.ArticleCode).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grd.RootTable.Columns(grdDetail.ArticleDescription).EditType = Janus.Windows.GridEX.EditType.TextBox
            Me.grd.RootTable.Columns(grdDetail.ArticleColorName).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns(grdDetail.ArticleColorName).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grd.RootTable.Columns(grdDetail.ArticleSizeName).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns(grdDetail.ArticleSizeName).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grd.RootTable.Columns(grdDetail.ArticleUnitName).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns(grdDetail.ArticleUnitName).FilterEditType = Janus.Windows.GridEX.FilterEditType.TextBox
            Me.grd.RootTable.Columns(grdDetail.TotalQty).EditType = Janus.Windows.GridEX.EditType.NoEdit
            If RateVisible = True Then
                Me.grd.RootTable.Columns(grdDetail.PDP).EditType = Janus.Windows.GridEX.EditType.TextBox
            Else
                Me.grd.RootTable.Columns(grdDetail.PDP).EditType = Janus.Windows.GridEX.EditType.NoEdit
            End If
            Me.grd.RootTable.Columns(grdDetail.DiscValue).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns(grdDetail.PurchasePrice).Visible = False
            Me.grd.RootTable.Columns(grdDetail.TotalAmount).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns(grdDetail.NetAmount).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns(grdDetail.Rate).EditType = Janus.Windows.GridEX.EditType.NoEdit
            Me.grd.RootTable.Columns(grdDetail.Qty).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns(grdDetail.PackQty).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns(grdDetail.TotalQty).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns(grdDetail.PDP).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns(grdDetail.DiscFactor).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns(grdDetail.DiscValue).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns(grdDetail.Rate).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns(grdDetail.PurchasePrice).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns(grdDetail.PackRate).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns(grdDetail.TotalAmount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns(grdDetail.Tax).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns(grdDetail.NetAmount).AggregateFunction = Janus.Windows.GridEX.AggregateFunction.Sum
            Me.grd.RootTable.Columns(grdDetail.Qty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdDetail.PackQty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdDetail.TotalQty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdDetail.TotalQty).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdDetail.PDP).HeaderAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdDetail.PDP).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdDetail.DiscFactor).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdDetail.DiscValue).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdDetail.Rate).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdDetail.PurchasePrice).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdDetail.PackRate).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdDetail.TotalAmount).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdDetail.Tax).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdDetail.NetAmount).TextAlignment = Janus.Windows.GridEX.TextAlignment.Far
            Me.grd.RootTable.Columns(grdDetail.DiscountId).HasValueList = True
            Me.grd.RootTable.Columns(grdDetail.DiscountId).LimitToList = True
            Me.grd.RootTable.Columns(grdDetail.DiscountId).Caption = "Discount Type"
            Me.grd.RootTable.Columns(grdDetail.DiscountId).EditType = Janus.Windows.GridEX.EditType.Combo
            FillCombos("grdDiscountType")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ApplySecurity(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.ApplySecurity

    End Sub
    Public Sub GetSecurityRights()
        Try
            If LoginGroup = "Administrator" Then
                Me.Visible = True
                Me.BtnSave.Enabled = True
                Me.cmbPayMode.SelectedText = "Cash"
                Me.lblDiscount.Visible = True
                Me.Label1.Visible = True
                Me.txtDiscount.Visible = True
                Me.txtDisPercentage.Visible = True
                RateVisible = True
                IsDeleteRights = True
                IsUpdateAllRights = True
                IsGetAllPOSRights = True
                CtrlGrdBar2.mGridChooseFielder.Enabled = True
                CtrlGrdBar2.mGridExport.Enabled = True
                CtrlGrdBar2.mGridPrint.Enabled = True
                Exit Sub
            End If
            'Me.Visible = False
            Me.BtnSave.Enabled = False
            Me.lblDiscount.Visible = False
            Me.Label1.Visible = False
            Me.cmbPayMode.SelectedText = "Cash"
            Me.txtDiscount.Visible = False
            Me.txtDisPercentage.Visible = False
            IsDeleteRights = False
            IsUpdateAllRights = False
            IsGetAllPOSRights = False
            CtrlGrdBar2.mGridChooseFielder.Enabled = False
            CtrlGrdBar2.mGridExport.Enabled = False
            CtrlGrdBar2.mGridPrint.Enabled = False
            For i As Integer = 0 To Rights.Count - 1
                If Rights.Item(i).FormControlName = "View" Then
                    Me.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Save" Then
                    If Me.BtnSave.Text = "&Save" Then BtnSave.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Update" Then
                    If Me.BtnSave.Text = "&Update" Then BtnSave.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Delete" Then
                    IsDeleteRights = True
                ElseIf Rights.Item(i).FormControlName = "Update All" Then
                    IsUpdateAllRights = True
                ElseIf Rights.Item(i).FormControlName = "Get All POS" Then
                    IsGetAllPOSRights = True
                ElseIf Rights.Item(i).FormControlName = "Credit" Then
                    cmbPayMode.SelectedText = "Credit"
                    CreditRight = True
                ElseIf Rights.Item(i).FormControlName = "Allow Price Editing" Then
                    RateVisible = True
                ElseIf Rights.Item(i).FormControlName = "Discount" Then
                    Me.txtDiscount.Visible = True
                    Me.txtDisPercentage.Visible = True
                    Me.lblDiscount.Visible = True
                    Me.Label1.Visible = True
                ElseIf Rights.Item(i).FormControlName = "Print" Then
                    CtrlGrdBar2.mGridPrint.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Export" Then
                    CtrlGrdBar2.mGridExport.Enabled = True
                ElseIf Rights.Item(i).FormControlName = "Field Chooser" Then
                    CtrlGrdBar2.mGridChooseFielder.Enabled = True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function Delete(Optional Condition As String = "") As Boolean Implements IGeneral.Delete

    End Function
    Public Sub FillCombos(Optional Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = String.Empty
            If Condition = "SM" Then
                str = "Select Employee_ID, Employee_Name  + ' - ' + employee_Code as EmployeeName from tblDefEmployee WHERE SalePerson <> 0 And Active = 1"
                FillDropDown(Me.cmbSalesPerson, str)
                'ElseIf Condition = "POS" Then
                '    str = "If exists(select POSId FROM tblUserPOSRights where UserID = " & LoginUserId & ") Select POSId, POSTitle from tblPOSConfiguration where POSId in (select POSId FROM tblUserPOSRights where UserID = " & LoginUserId & ") AND Active = 1 order by POSId Else Select POSId, POSTitle from tblPOSConfiguration WHERE Active order by POSId"
                '    FillDropDown(Me.cmbPOS, str, False)
            ElseIf Condition = "BM" Then
                str = "Select Employee_ID, Employee_Name  + ' - ' + employee_Code as EmployeeName from tblDefEmployee WHERE SalePerson <> 0 And Active = 1"
                FillDropDown(Me.cmbBillMaker, str)
            ElseIf Condition = "PM" Then
                str = "Select Employee_ID, Employee_Name  + ' - ' + employee_Code as EmployeeName from tblDefEmployee WHERE SalePerson <> 0 And Active = 1"
                FillDropDown(Me.cmbPackingMan, str)
            ElseIf Condition = "grdDiscountType" Then
                str = "select DiscountID, DiscountType from tblDiscountType"
                Dim dt As DataTable = GetDataTable(str)
                Me.grd.RootTable.Columns("DiscountId").ValueList.PopulateValueList(dt.DefaultView, "DiscountID", "DiscountType")
            ElseIf Condition = "grdUnit" Then
                str = "Select ArticlePackId, PackName From ArticleDefPackTable WHERE ArticleMasterId In (Select MasterId From ArticleDefView WHERE ArticleId=" & frmItemSearch.ArticleId & ") ORDER BY PackName ASC "
                Dim dt As DataTable = GetDataTable(str)
                Me.grd.RootTable.Columns("ArticlePackId").ValueList.PopulateValueList(dt.DefaultView, "ArticlePackId", "PackName")
            ElseIf Condition = "Bank" Then
                str = "If  exists(select Account_Id FROM tblUserAccountRights where UserID = " & LoginUserId & " And Rights = 1 And Account_Id Is Not Null) " _
                    & " select coa_detail_id, detail_title from vwCoaDetail where account_type=N'Bank' And coa_detail_id in (select Account_Id FROM tblUserAccountRights where UserID = " & LoginUserId & " And Rights = 1) " _
                    & " Else " _
                    & " select coa_detail_id, detail_title from vwCoaDetail where account_type=N'Bank'"
                FillDropDown(cmbBank, str, True)
            ElseIf Condition = "CreditCard" Then
                'Dim str1 As String
                'str = txtDocNo.Text
                'Dim words As String() = str.Split("-")
                'Dim title As String = words(0)
                str = "SELECT CreditCardId, MachineTitle, BankAccountId from tblCreditCardAccount where POStitle = '" & Title & "'"
                FillDropDown(cmbCCAccount, str, False)
                'ElseIf Condition = "Customer" Then
                '    If getConfigValueByType("Show Vendor On Sales") = "True" Then
                '        str = "SELECT     vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name, vwCOADetail.detail_code as [Code],tblListState.StateName as State, tblListCity.CityName as City,  " & _
                '                       "tblListTerritory.TerritoryName as Territory , tblCustomer.ExpiryDate, tblCustomer.Discper as [Discount] ,tblCustomer.otherexpanses as [Other Expense], tblCustomer.Fuel as Fuel, tblCustomer.CNG as CNG , tblCustomer.Cridtlimt as Limit, dbo.vwCOADetail.account_type as Type, isnull(customertypes,0) as typeid, vwCOADetail.Contact_Email as Email, vwCOADetail.Contact_Phone as Phone, vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title, IsNull(vwCOADetail.SaleMan,0) as SaleManId,IsNull(vwCOADetail.CreditDays,0) as CreditDays " & _
                '                       "FROM  tblCustomer LEFT OUTER JOIN " & _
                '                       "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                '                       "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                '                       "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                '                       "vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " & _
                '                       "WHERE (vwCOADetail.account_type in( 'Customer','Vendor' )) and  vwCOADetail.coa_detail_id is not  null "
                '    Else

                '        str = "SELECT     vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name,vwCOADetail.detail_code as [Code], tblListState.StateName as State, tblListCity.CityName as City,  " & _
                '                      "tblListTerritory.TerritoryName as Territory , tblCustomer.ExpiryDate,tblCustomer.Discper as [Discount] ,tblCustomer.otherexpanses as [Other Expense], tblCustomer.Fuel as Fuel , tblCustomer.CNG as CNG , tblCustomer.Cridtlimt as Limit, dbo.vwCOADetail.account_type as Type, isnull(customertypes,0) as typeid, vwCOADetail.Contact_Email as Email, vwCOADetail.Contact_Phone as Phone, vwCOADetail.Contact_Mobile as Mobile, vwCOADetail.Sub_Sub_Title, IsNull(vwCOADetail.SaleMan,0) as SaleManId, ISNULL(vwCOADetail.CreditDays,0) as CreditDays " & _
                '                      "FROM         tblCustomer LEFT OUTER JOIN " & _
                '                      "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                '                      "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                '                      "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                '                      "vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " & _
                '                      "WHERE (vwCOADetail.account_type='Customer') and  vwCOADetail.coa_detail_id is not  null "
                '    End If
                '    If IsEditMode = False Then
                '        str += " AND vwCOADetail.Active=1"
                '    Else
                '        str += " AND vwCOADetail.Active in(0,1,NULL)"
                '    End If
                '    str += " order by tblCustomer.Sortorder, vwCOADetail.detail_title "
                '    FillUltraDropDown(cmbCustomer, str)
                '    If cmbCustomer.DisplayLayout.Bands(0).Columns.Count > 0 Then
                '        Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.Id).Hidden = True
                '        Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.Territory).Hidden = False
                '        Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.State).Hidden = True
                '        Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.ExpiryDate).Hidden = True
                '        Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.Fuel).Hidden = True
                '        Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.CNG).Hidden = True
                '        Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.Other_Exp).Hidden = True
                '        Me.cmbCustomer.DisplayLayout.Bands(0).Columns("typeid").Hidden = True
                '        Me.cmbCustomer.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                '        Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.Discount).Hidden = True
                '        Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.Credit_Limit).Hidden = True
                '        Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.Type).Hidden = True
                '        Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.Name).Width = 300
                '        Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.Credit_Limit).Width = 80
                '        Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.Discount).Width = 80
                '        Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.SubSubTitle).Header.Caption = "Ac Head"
                '        Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.SubSubTitle).Width = 120
                '        Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.SaleMan).Hidden = True
                '        Me.cmbCustomer.DisplayLayout.Bands(0).Columns("CreditDays").Hidden = True
                'End If
            End If
            'Me.cmbPayMode.SelectedIndex = 0
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub GetAllRecords(Optional Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            Dim str As String = ""
            Dim dt As DataTable
            str = "SELECT ArticleId, ArticleCode, ArticleDescription, ArticleColorName, ArticleSizeName, ArticleUnitName,'' AS PackSize, 0.00 AS PackQty, 0.00 AS Qty, 0.00 AS TotalQty, 0.00 AS PDP, '' AS DiscountId, 0.00 AS DiscFactor, 0.00 AS DiscValue, 0.00 AS Rate, 0.00 AS PurchasePrice, 0.00 AS PackRate, 0.00 AS TotalAmount, 0.00 AS Tax, 0.00 AS NetAmount FROM ArticleDefView WHERE ArticleId = -1"
            dt = GetDataTable(str)
            Me.grd.DataSource = dt
            Me.grd.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CheckCurrentStockByItem(ByVal ItemId As Integer, ByVal Qty As Double, Optional ByVal GridEx As Janus.Windows.GridEX.GridEX = Nothing, Optional ByVal DocNo As String = "", Optional ByVal trans As OleDbTransaction = Nothing) As Boolean
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Select ArticleDefId, IsNull(SUM(Isnull(InQty,0)-Isnull(OutQty,0)),0) as CurrentStock From StockDetailTable WHERE ArticleDefId=" & ItemId & " " & IIf(DocNo.Length > 0, " AND StockTransId Not In(Select StockTransId From StockMasterTable WHERE DocNo='" & DocNo.Replace("'", "''") & "')", "") & "  Group By ArticleDefId"
            Dim dt As New DataTable
            dt = GetDataTable(strSQL, trans)
            Dim dblCurrentStock As Double = 0D
            Dim RowFormat As New Janus.Windows.GridEX.GridEXFormatStyle
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    dblCurrentStock = Val(dt.Rows(0).Item(1).ToString)
                    If Qty > dblCurrentStock Then
                        If GridEx IsNot Nothing Then
                            RowFormat.BackColor = Color.LightPink
                            GridEx.CurrentRow.RowStyle = RowFormat
                        End If
                        Throw New Exception(Chr(10) & "Stock is not enough.")
                        Return True
                    Else
                        Return False
                    End If
                Else
                    If GridEx IsNot Nothing Then
                        RowFormat.BackColor = Color.LightPink
                        GridEx.CurrentRow.RowStyle = RowFormat
                    End If
                    Throw New Exception(Chr(10) & "Stock is not enough.")
                    Return True
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ResetBarcode()
        Try
            txtRate.Text = "0"
            txtQty.Text = "0"
            txtPackQty.Text = "0"
            txtTotalQty.Text = "0"
            lblItemDescription.Text = ""
            txtBardCodeScan.Text = ""
            txtCNIC.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Sub ReSetControls(Optional Condition As String = "") Implements IGeneral.ReSetControls
        Try
            'GetInfo()
            txtMobile.Focus()
            Me.txtBalance.Text = ""
            Me.txtCash.Text = ""
            Me.txtCNIC.Text = ""
            Me.txtCreditLimit.Text = ""
            Me.txtDisPercentage.Text = ""
            'Ali Faisal : TFS4415 : Configuration based POS revision is require
            Me.txtDocNo.Size = New System.Drawing.Size(222, 23)
            Me.lblRev.Visible = False
            Me.lnkLblRevisions.Visible = False
            Me.cmbRevisionNumber.Visible = False
            Me.btnPrint.Visible = False
            Me.txtDocNo.Text = GetDocumentNo()
            Me.txtMobile.Text = ""
            Me.txtNetTotal.Text = ""
            Me.txtPaymentBalance.Text = ""
            Me.txtRemarks.Text = ""
            Me.txtTax.Text = ""
            Me.txtTotal.Text = ""
            Me.txtCustomer.Text = ""
            SalesId = 0
            Code = 0
            Remarks = ""
            Comments = ""
            If Me.BtnSave.Text = "&Update" Then
                Me.BtnSave.Text = "&Save"
            End If
            Me.chkOnline.Checked = False
            frmMixPayment.ResetControl()
            'If Me.cmbCustomer.Rows.Count > 0 Then cmbCustomer.Rows(0).Activate()
            'If CreditRight = False Then
            '    Me.cmbPayMode.SelectedIndex = 0
            'Else
            '    Me.cmbPayMode.SelectedIndex = 1
            'End If
            Me.cmbPayMode.SelectedIndex = 0
            Me.cmbSalesPerson.SelectedValue = SPID
            Me.cmbPackingMan.SelectedValue = 0
            Me.cmbBillMaker.SelectedValue = 0
            frmSearchCustomersVendors.CustomerCode = 0
            frmSearchCustomersVendors.CustomerName = ""
            lblItemName.Text = "" ''TFS4738 
            lblItemPrice.Text = "" ''TFS4738 
            lblItemPackQty.Text = "" ''TFS4738 
            FillCombos("CreditCard")
            InitializeGrid()
            ResetBarcode()
            If Not Me.cmbPayMode.Items().Contains("Cash") Then
                Me.cmbPayMode.Items().Insert(0, "Cash")
            End If
            If Not Me.cmbPayMode.Items().Contains("Credit") Then
                Me.cmbPayMode.Items().Insert(1, "Credit")
            End If
            If Not Me.cmbPayMode.Items().Contains("Bank") Then
                Me.cmbPayMode.Items().Insert(2, "Bank")
            End If
            If Not Me.cmbPayMode.Items().Contains("Credit Card") Then
                Me.cmbPayMode.Items().Insert(3, "Credit Card")
            End If
            If Not Me.cmbPayMode.Items().Contains("Mix") Then
                Me.cmbPayMode.Items().Insert(4, "Mix")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        If Code <= 0 Then
            Code = frmSearchCustomersVendors.CustomerCode
        End If
        Try
            StockMaster = New StockMaster
            StockMaster.StockTransId = 0I
            StockMaster.DocNo = Me.txtDocNo.Text.ToString.Replace("'", "''")
            StockMaster.DocDate = Me.dtpPOSDate.Value.Date
            StockMaster.DocType = 3
            StockMaster.Remaks = Remarks.Replace("'", "''")
            StockMaster.Project = CCID
            StockMaster.AccountId = Code
            StockMaster.StockDetailList = StockList
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function Save(Optional Condition As String = "") As Boolean Implements IGeneral.Save
        Dim trans As OleDbTransaction
        Try
            If Me.grd.RowCount = 0 Then Exit Function
            If frmSearchCustomersVendors.CustomerCode > 0 Then
                Code = frmSearchCustomersVendors.CustomerCode
            End If
            If frmSearchCustomersVendors.CustomerName = "" Or frmSearchCustomersVendors.CustomerName Is Nothing Then
                CName = frmSearchCustomersVendors.CustomerName
                Code = Val(getConfigValueByType("DefaultAccountInPlaceCustomer").ToString)
            End If
            Me.txtDocNo.Text = GetDocumentNo()
            setVoucherNo = Me.txtDocNo.Text
            Dim objCommand As New OleDbCommand
            Dim objCon As OleDbConnection
            Dim i As Integer
            Dim lngVoucherMasterId As Integer = GetVoucherId(Me.Name, Me.txtDocNo.Text)
            Dim InvAccountId As Integer = Val(getConfigValueByType("InvAccountId").ToString)
            Dim CgsAccountId As Integer = Val(getConfigValueByType("CGSAccountId").ToString)
            Dim AccountId As Integer = Val(getConfigValueByType("SalesCreditAccount").ToString)
            Dim SalesTaxId As Integer = 0I
            If CID > 0 Then
                Dim str As String = "SELECT SalesTaxAccountId FROM CompanyDefTable WHERE CompanyId = " & CID & " AND SalesTaxAccountId IS NOT NULL"
                Dim dt As DataTable = GetDataTable(str)
                If dt.Rows.Count > 0 Then
                    SalesTaxId = dt.Rows(0).Item(0)
                End If
            End If
            If SalesTaxId = 0 Then
                SalesTaxId = Val(getConfigValueByType("SalesTaxCreditAccount").ToString)
            End If
            Dim SEDAccountId As Integer = Val(getConfigValueByType("SEDAccountId").ToString)
            Dim InsuranceAccountId As Integer = Val(getConfigValueByType("TransitInsuranceAccountId").ToString)
            AdjustmentExpAccount = Val(getConfigValueByType("AdjustmentExpAccount").ToString)
            Dim SalesDiscountAccount As Integer = Val(getConfigValueByType("SalesDiscountAccount").ToString)
            Dim IsDiscountVoucher As Boolean = Convert.ToBoolean(getConfigValueByType("DiscountVoucherOnSale").ToString)
            Dim GLAccountArticleDepartment As Boolean
            If Not getConfigValueByType("GLAccountArticleDepartment").ToString = "Error" Then
                GLAccountArticleDepartment = Convert.ToBoolean(getConfigValueByType("GLAccountArticleDepartment"))
            Else
                GLAccountArticleDepartment = False
            End If
            Dim blnCheckCurrentStockByItem As Boolean = False
            If Not getConfigValueByType("CheckCurrentStockByItem").ToString = "Error" Then
                blnCheckCurrentStockByItem = Convert.ToBoolean(getConfigValueByType("CheckCurrentStockByItem").ToString)
            End If
            If InvAccountId <= 0 Then
                ShowErrorMessage("Purchase account is not map.")
                Me.dtpChequeDate1.Focus()
                Return False
            ElseIf CgsAccountId <= 0 Then
                ShowErrorMessage("Cost of good sold account is not map.")
                Me.dtpChequeDate1.Focus()
                Return False
            End If
            If AccountId <= 0 Then
                ShowErrorMessage("Sales account is not map.")
                Me.dtpChequeDate1.Focus()
                Return False
            End If
            If Val(Me.txtTax.Text) > 0 Then
                If SalesTaxId <= 0 Then
                    ShowErrorMessage("Sales tax account is not map.")
                    Me.dtpChequeDate1.Focus()
                    Return False
                End If
            End If
            If Val(Me.txtDisPercentage.Text) <> 0 AndAlso Val(Me.txtDiscount.Text) <> 0 Then
                If AdjustmentExpAccount <= 0 Then
                    ShowErrorMessage("Adjustment account is not map.")
                    Me.dtpPOSDate.Focus()
                    Return False
                End If
            End If
            If IsDiscountVoucher = True Then
                If SalesDiscountAccount <= 0 Then
                    ShowErrorMessage("Discount account is not map.")
                    Me.dtpChequeDate1.Focus()
                    Return False
                End If
            End If
            Me.lblProgress.Text = "Processing Please Wait ..."
            Me.lblProgress.Visible = True
            Application.DoEvents()
            Dim ReceiptVoucherFlg As String = Convert.ToString(getConfigValueByType("ReceiptVoucherOnSales").ToString)
            Dim VoucherNo As String = GetVoucherNo()
            Dim DiscountedPrice As Double = 0
            'Dim SalesAccount As Integer = 0I
            'Dim AccountId As Integer = Val(getConfigValueByType("SalesCreditAccount").ToString)
            Dim CurrentBalance As Double = CDbl(GetAccountBalance(Code))
            Dim ExpenseChargeToCustomer As Boolean
            ExpenseChargeToCustomer = Convert.ToBoolean(getConfigValueByType("ExpenseChargeToCustomer").ToString)
            Dim flgExcludeTaxPrice As Boolean = Convert.ToBoolean(getConfigValueByType("ExcludeTaxPrice").ToString)
            Dim DCStockImpact As Boolean = False
            If Not getConfigValueByType("DCStockImpact").ToString = "Error" Then
                DCStockImpact = Convert.ToBoolean(getConfigValueByType("DCStockImpact").ToString)
            End If
            ''TASK TFS4548
            If Me.txtCustomer.Text.Length > 0 Then
                Remarks = "Customer Name : " & txtCustomer.Text & ""
            End If
            If Me.txtCNIC.Text.Length > 0 Then
                If Remarks.Length > 0 Then
                    Remarks += ", CNIC : " & txtCNIC.Text & ""
                Else
                    Remarks += " CNIC : " & txtCNIC.Text & ""
                End If
            End If
            If Me.txtMobile.Text.Length > 0 Then
                If Remarks.Length > 0 Then
                    Remarks += ", Mobile : " & txtMobile.Text & ""
                Else
                    Remarks += "  Mobile : " & txtMobile.Text & ""
                End If
            End If
            If Me.txtRemarks.Text.Length > 0 Then
                If Remarks.Length > 0 Then
                    Remarks += ", Remarks : " & txtRemarks.Text & ""
                Else
                    Remarks += "  Remarks : " & txtRemarks.Text & ""
                End If
            End If

            ''END TASK TFS4548

            objCon = Con
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Open()
            objCommand.Connection = objCon


            Try
                trans = objCon.BeginTransaction
                objCommand.CommandType = CommandType.Text
                objCommand.Transaction = trans
                'Ali Faisal : TFS4415 : Configuration based POS revision is require
                'Aashir: Costcenter was not saving
                objCommand.CommandText = "Insert into SalesMasterTable (LocationId,SalesNo,SalesDate,CustomerCode,Employeecode,SalesQty,SalesAmount, CashPaid, Remarks,UserName,PreviousBalance, Adj_Flag, Adj_Percentage, InvoiceType,MobileNo, CNIC, POSFlag, CustomerName, HoldFlag, SalesStartTime, SalesEndTime, HoldStartTime, HoldEndTime, PackingManId, BillMakerId, RevisionNumber, Remarks1, CostCenterId) values( " _
                & CID & ", N'" & txtDocNo.Text & "',N'" & dtpChequeDate1.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & Code & "," & Me.cmbSalesPerson.SelectedValue & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & "," & Val(txtNetTotal.Text) & ", " & Val(txtCash.Text) & ",N'" & Remarks.Replace("'", "''") & "',N'" & LoginUserName & "'," & Val(txtPaymentBalance.Text) & ", " & Val(txtDiscount.Text) & ", " & Val(txtDisPercentage.Text) & ",N'" & Me.cmbPayMode.Text.Replace("'", "''") & "','" & Me.txtMobile.Text.Replace("'", "''") & "','" & Me.txtCNIC.Text.Replace("'", "''") & "'," & 1 & " ,'" & Me.txtCustomer.Text.Replace("'", "''") & "', 0, " & IIf(SalesStartTime = Date.MinValue, "NULL", "'" & SalesStartTime.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", '" & Date.Now.ToString("yyyy-M-d h:mm:ss tt") & "', " & IIf(HoldStartTime = Date.MinValue, "NULL", "'" & HoldStartTime.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", " & IIf(HoldEndTime = Date.MinValue, "NULL", "'" & HoldEndTime.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", " & Me.cmbPackingMan.SelectedValue & ", " & Me.cmbBillMaker.SelectedValue & " , 0, N'" & txtRemarks.Text.Replace("'", "''") & "'," & CCID & ")" _
                & " SELECT @@IDENTITY"
                'TFS2211: Waqar Added these Lines for Adjustment od Bardana
                InvId = objCommand.ExecuteScalar
                'End TFS2211
                objCommand.CommandText = ""
                objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                                       & " cheque_no, cheque_date,Source,voucher_code, Employee_Id, Remarks, UserName, Post)" _
                                       & " VALUES(" & CID & ", 1,  7 , N'" & Me.txtDocNo.Text & "', N'" & Me.dtpChequeDate1.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                                       & " NULL, NULL,N'" & Me.Name & "',N'" & Me.txtDocNo.Text & "', " & IIf(Me.cmbSalesPerson.SelectedIndex = -1, 0, Me.cmbSalesPerson.SelectedValue) & ", N'" & Me.Remarks.Replace("'", "''") & "', '" & LoginUserName & "', 1)" _
                                       & " SELECT @@IDENTITY"
                lngVoucherMasterId = objCommand.ExecuteScalar
                '***********************
                'Deleting Detail
                '***********************
                objCommand.CommandText = ""
                objCommand.CommandText = "delete from tblVoucherDetail where voucher_Id =" & lngVoucherMasterId
                objCommand.ExecuteNonQuery()
            Catch ex As Exception
                Throw ex
            End Try
            objCommand.CommandText = ""
            StockList = New List(Of StockDetail)

            For i = 0 To grd.GetRows.Length - 1
                If blnCheckCurrentStockByItem = True Then
                    CheckCurrentStockByItem(Me.grd.GetRows(i).Cells(grdDetail.ArticleId).Value, Val(Me.grd.GetRows(i).Cells("TotalQty").Value.ToString), Me.grd, , trans)
                End If
                'If GLAccountArticleDepartment = True Then
                '    InvAccountId = Val(grd.GetRows(i).Cells("InvAccountId").Value.ToString)
                '    AccountId = Val(grd.GetRows(i).Cells("SalesAccountId").Value.ToString)
                '    CgsAccountId = Val(grd.GetRows(i).Cells("CGSAccountId").Value.ToString)
                'End If
                Dim CostPrice As Double = 0D
                Dim CrrStock As Double = 0D
                DiscountedPrice = Me.grd.GetRows(i).Cells(grdDetail.DiscValue).Value
                Dim dblPurchasePrice As Double = 0D
                Dim dblCostPrice As Double = 0D
                Dim strPriceData() As String = GetRateByItem(Val(Me.grd.GetRows(i).Cells(grdDetail.ArticleId).Value.ToString)).Split(",")
                If strPriceData.Length > 1 Then
                    dblCostPrice = Val(strPriceData(0).ToString)
                    dblPurchasePrice = Val(strPriceData(1).ToString)
                End If
                If flgAvgRate = True Then
                    If dblCostPrice > 0 Then
                        CostPrice = dblCostPrice
                    Else
                        CostPrice = dblPurchasePrice
                    End If
                Else
                    CostPrice = dblPurchasePrice
                End If

                ''TASK TFS4548
                Comments = "Item : " & grd.GetRows(i).Cells(grdDetail.ArticleDescription).Value.ToString & ", Qty: " & Val(grd.GetRows(i).Cells(grdDetail.TotalQty).Value) & " "
                'If Remarks.Length > 0 Then
                '    Comments += ", " & Remarks & ""
                'End If
                ''END TASK TFS4548


                If Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) > 0 Then
                    StockDetail = New StockDetail
                    StockDetail.StockTransId = 0
                    StockDetail.LocationId = LID
                    StockDetail.ArticleDefId = grd.GetRows(i).Cells("ArticleId").Value
                    StockDetail.InQty = 0
                    StockDetail.OutQty = Val(grd.GetRows(i).Cells("TotalQty").Value.ToString)
                    StockDetail.Rate = IIf(CostPrice = 0, Val(grd.GetRows(i).Cells(grdDetail.PurchasePrice).Value), CostPrice)
                    StockDetail.InAmount = 0
                    StockDetail.OutAmount = ((Val(grd.GetRows(i).Cells("TotalQty").Value)) * IIf(CostPrice = 0, Val(grd.GetRows(i).Cells(grdDetail.PurchasePrice).Value), CostPrice))
                    StockDetail.Remarks = Comments.Replace("'", "''") & ", Rate : " & StockDetail.Rate.ToString & ", " & Remarks.Replace("'", "''")
                    CostPrice = StockDetail.Rate
                    StockDetail.PackQty = Val(grd.GetRows(i).Cells("PackQty").Value.ToString)
                    StockDetail.Out_PackQty = Val(grd.GetRows(i).Cells("Qty").Value.ToString)
                    StockDetail.In_PackQty = 0
                    StockList.Add(StockDetail)
                End If
                'TFS2211: Waqar Added these Lines for Adjustment od Bardana
                objCommand.CommandText = "Insert into SalesDetailTable (SalesId, ArticleDefId,ArticleSize, Sz1, Sz7, Qty, LocationId,PostDiscountPrice,DiscountId,DiscountFactor,DiscountValue,Price, PurchasePrice, PackPrice, TaxPercent, CurrencyAmount, Comments) values( " _
                                  & " " & "ident_current('SalesMasterTable')" & " ," & Val(grd.GetRows(i).Cells(grdDetail.ArticleId).Value) & ", '" & Me.grd.GetRows(i).Cells(grdDetail.PackSize).Value.ToString & "', " & Val(grd.GetRows(i).Cells(grdDetail.PackQty).Value) & " ," & Val(grd.GetRows(i).Cells(grdDetail.Qty).Value) & ", " _
                                  & " " & Val(grd.GetRows(i).Cells("TotalQty").Value) & ", " & LID & ", " _
                                  & " " & Val(grd.GetRows(i).Cells("PDP").Value) & ", " & grd.GetRows(i).Cells(grdDetail.DiscountId).Value & "," & Val(Me.grd.GetRows(i).Cells("DiscFactor").Value) & ", " & Val(Me.grd.GetRows(i).Cells(grdDetail.DiscValue).Value) & ", " & Val(grd.GetRows(i).Cells(grdDetail.Rate).Value) & ", " & Val(Me.grd.GetRows(i).Cells(grdDetail.PurchasePrice).Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells(grdDetail.PackRate).Value.ToString) & ", " & IIf(grd.GetRows(i).Cells(grdDetail.Tax).Value.ToString = "", 0, grd.GetRows(i).Cells(grdDetail.Tax).Value) & "," & Val(Me.grd.GetRows(i).Cells("NetAmount").Value.ToString) & ", '" & Comments.Replace("'", "''") & ", Rate : " & grd.GetRows(i).Cells(grdDetail.Rate).Value.ToString & ", " & Remarks.Replace("'", "''") & "') Select @@Identity "
                objCommand.ExecuteNonQuery()

                'TFS2211: Waqar Raza: Added These Lines to add Bardana into Inventroy when an Item Pack Quantity completes Using Loose
                'Start TFS2211:
                Dim strmaster As String
                strmaster = "Select MasterID from ArticleDefView where ArticleId = " & Val(grd.GetRows(i).Cells(grdDetail.ArticleId).Value) & ""
                Dim dtmaster As DataTable = GetDataTable(strmaster)
                Dim MasterId As Integer
                If dtmaster.Rows.Count > 0 Then
                    MasterId = dtmaster.Rows(0).Item("MasterID")
                End If
                Dim strBardanaItem As String
                Dim BardanaItem As Integer
                strBardanaItem = "Select RelatedArticleId from tblRelatedItem where ArticleId = " & MasterId & ""
                Dim BardanaDT As DataTable
                BardanaDT = GetDataTable(strBardanaItem)
                If BardanaDT.Rows.Count > 0 Then
                    BardanaItem = BardanaDT.Rows(0).Item("RelatedArticleId")
                Else
                    BardanaItem = 0
                End If
                If BardanaItem > 0 Then
                    'Dim value As Object = objCommand.ExecuteScalar()
                    'If value Is DBNull.Value Or value Is Nothing Then
                    '    InvId = 0
                    'Else
                    '    InvId = Convert.ToInt32(value)
                    'End If
                    'InvId = objCommand.ExecuteScalar()
                    'If SalesDetailId > 0 AndAlso InvId = 0 Then
                    '    InvId = SalesDetailId
                    'End If
                    If Me.grd.GetRows(i).Cells(grdDetail.PackSize).Value.ToString = "Loose" AndAlso getConfigValueByType("BardanaAdjustmentOnPOS").ToString = "True" Then
                        objCommand.CommandText = "INSERT INTO tblBardanaAdjustment(SalesDetailId, ArticleId, Qty) VALUES(" & InvId & "," & Val(grd.GetRows(i).Cells(grdDetail.ArticleId).Value) & "," & Val(grd.GetRows(i).Cells(grdDetail.TotalQty).Value) & ")"
                        objCommand.ExecuteNonQuery()

                        objCommand.CommandText = "SELECT tblBardanaAdjustment.ArticleId, isnull(SUM(tblBardanaAdjustment.Qty),0) - isnull(SUM(tblBardanaAdjustment.AdjustedQty),0) AS AdjustedQty, ArticleDefTable.PackQty FROM tblBardanaAdjustment INNER JOIN ArticleDefTable ON tblBardanaAdjustment.ArticleId = ArticleDefTable.ArticleId WHERE ArticleDefTable.ArticleId = " & Val(grd.GetRows(i).Cells(grdDetail.ArticleId).Value) & " GROUP BY tblBardanaAdjustment.ArticleId, ArticleDefTable.PackQty"
                        Dim da As New OleDbDataAdapter
                        Dim dt As New DataTable
                        da.SelectCommand = objCommand
                        da.Fill(dt)
                        dt.AcceptChanges()

                        'For Each r As DataRow In dt.Rows
                        If Val(dt.Rows(0).Item(1).ToString) >= Val(dt.Rows(0).Item(2).ToString) Then
                            'objCommand.CommandText = "Insert Into StockMasterTable (DocNo, DocDate, Project) " _
                            '                             & " Values(N'" & txtDocNo.Text & "', N'" & dtpPOSDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " & IIf(CCID = Nothing, "NULL", CCID) & ") Select @@Identity "
                            'TransId = objCommand.ExecuteScalar()
                            Dim value1 As Integer = Val(dt.Rows(0).Item(1).ToString) / Val(dt.Rows(0).Item(2).ToString)
                            For i1 As Integer = 1 To value1
                                objCommand.CommandText = "INSERT INTO tblBardanaAdjustment(SalesDetailId,ArticleId,AdjustedQty) VALUES(" & InvId & "," & dt.Rows(0).Item(0).ToString & "," & dt.Rows(0).Item(2).ToString & ")"
                                objCommand.ExecuteNonQuery()
                            Next
                            StockDetail = New StockDetail
                            StockDetail.StockTransId = 0
                            StockDetail.LocationId = LID
                            StockDetail.ArticleDefId = BardanaItem
                            StockDetail.InQty = value1
                            StockDetail.Remarks = "'Added through POS Bardana Adjustment Against " & txtDocNo.Text & "'"
                            StockList.Add(StockDetail)
                            'objCommand.CommandText = "Insert Into StockDetailTable(StockTransId, LocationId, ArticleDefId, InQty, Remarks) " _
                            '                            & " Values (" & TransId & ", " & LID & ",  " & BardanaItem & ", " & 1 & ", 'Added through POS Bardana Adjustment Against " & txtDocNo.Text & "')"
                            ''End Task:M16
                            'objCommand.ExecuteNonQuery()
                        End If
                    End If
                End If
                'Start TFS2211:

                If IsDiscountVoucher = True Then
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, direction, sp_refrence, ArticleDefId) " _
                & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & Code & ", " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * Val(grd.GetRows(i).Cells("PDP").Value.ToString) & ", 0, '" & Comments.Replace("'", "''") & ", Rate : " & grd.GetRows(i).Cells("PDP").Value.ToString & ", " & Remarks.Replace("'", "''") & "', " & CCID & ", " & grd.GetRows(i).Cells(grdDetail.ArticleId).Value & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells(grdDetail.ArticleId).Value.ToString) & ")"
                    objCommand.ExecuteNonQuery()
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
                                              & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & AccountId & ", " & 0 & ",  " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * Val(grd.GetRows(i).Cells("PDP").Value.ToString) & ", '" & Comments.Replace("'", "''") & ", Rate : " & grd.GetRows(i).Cells("PDP").Value.ToString & ", " & Remarks.Replace("'", "''") & "', " & CCID & ", " & grd.GetRows(i).Cells(grdDetail.ArticleId).Value & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells(grdDetail.ArticleId).Value.ToString) & ")"
                    objCommand.ExecuteNonQuery()
                Else
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, direction, sp_refrence, ArticleDefId) " _
                & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & Code & ", " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * Val(grd.GetRows(i).Cells("Rate").Value.ToString) & ", 0, N'" & Comments.Replace("'", "''") & ", Rate : " & grd.GetRows(i).Cells("Rate").Value.ToString & ", " & Remarks.Replace("'", "''") & "', " & CCID & ", " & grd.GetRows(i).Cells(grdDetail.ArticleId).Value & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells(grdDetail.ArticleId).Value.ToString) & ")"
                    objCommand.ExecuteNonQuery()
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
                                              & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & AccountId & ", " & 0 & ",  " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * Val(grd.GetRows(i).Cells("Rate").Value.ToString) & ", '" & Comments.Replace("'", "''") & ", Rate : " & grd.GetRows(i).Cells("PDP").Value.ToString & ", " & Remarks.Replace("'", "''") & "', " & CCID & ", " & grd.GetRows(i).Cells(grdDetail.ArticleId).Value & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells(grdDetail.ArticleId).Value.ToString) & ")"
                    objCommand.ExecuteNonQuery()
                End If
                'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, direction, sp_refrence, ArticleDefId) " _
                '& " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & Code & ", " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * Val(grd.GetRows(i).Cells("PDP").Value.ToString) & ", 0, N'" & CName & "', " & CCID & ", " & grd.GetRows(i).Cells(grdDetail.ArticleId).Value & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells(grdDetail.ArticleId).Value.ToString) & ")"
                'objCommand.ExecuteNonQuery()
                'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
                '                          & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & AccountId & ", " & 0 & ",  " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * Val(grd.GetRows(i).Cells("PDP").Value.ToString) & ",'" & txtRemarks.Text & "', " & CCID & ", " & grd.GetRows(i).Cells(grdDetail.ArticleId).Value & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells(grdDetail.ArticleId).Value.ToString) & ")"
                'objCommand.ExecuteNonQuery()
                Price = Val(grd.GetRows(i).Cells(grdDetail.Rate).Value)
                PDP = Val(grd.GetRows(i).Cells(grdDetail.PDP).Value)
                If PDP = 0 Then
                    PDP = Price
                End If
                If Price < PDP Then
                    Price = PDP
                End If
                If IsDiscountVoucher = True Then
                    If DiscountedPrice > 0 Then
                        objCommand.CommandText = ""
                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, direction, sp_refrence, ArticleDefId) " _
                                                                                            & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & Val(SalesDiscountAccount) & ", " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * DiscountedPrice & ", 0, N'" & Comments.Replace("'", "''") & ", Rate : " & DiscountedPrice.ToString & ", " & Remarks.Replace("'", "''") & "', " & CCID & ", " & grd.GetRows(i).Cells(grdDetail.ArticleId).Value & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells(grdDetail.ArticleId).Value.ToString) & ")"
                        objCommand.ExecuteNonQuery()
                        objCommand.CommandText = ""
                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction, sp_refrence, ArticleDefId) " _
                                                                                            & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & Code & ", 0," & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * DiscountedPrice & ",N'Ref Discount On Sales: " & Comments.Replace("'", "''") & ", Rate : " & DiscountedPrice.ToString & ", " & Remarks.Replace("'", "''") & "', " & CCID & ", " & grd.GetRows(i).Cells(grdDetail.ArticleId).Value & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells(grdDetail.ArticleId).Value.ToString) & ")"
                        objCommand.ExecuteNonQuery()
                    End If
                End If
                If Val(Me.txtTax.Text) > 0 Then
                    If flgExcludeTaxPrice = False Then
                        objCommand.CommandText = ""
                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
                                               & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & Code & ", " & Val(Me.txtTax.Text) & ", 0, 'Tax Ref: " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & " , Document No : " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                        objCommand.ExecuteNonQuery()
                    End If
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                         & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & SalesTaxId & ", " & 0 & ",  " & Val(Me.txtTax.Text) & ", 'Tax Ref By " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No : " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                    objCommand.ExecuteNonQuery()
                End If
            Next
            'Ali Faisal : TFS4415 : Configuration based POS revision is require
            If getConfigValueByType("IsDuplicateSales").ToString.ToUpper = "TRUE" Then
                Call CreateDuplicateSales(InvId, "Save", trans)
            End If
            If Val(Me.txtDiscount.Text) > 0 Then
                objCommand.CommandText = ""
                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
                                       & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & Me.AdjustmentExpAccount & ", " & Val(txtDiscount.Text) & ", 0, 'Adjustment Ref: " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No : " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "')"
                objCommand.ExecuteNonQuery()
                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                     & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & Code & ", " & 0 & ",  " & Val(txtDiscount.Text) & ", 'Adjustment Ref: " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No : " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "')"
                objCommand.ExecuteNonQuery()
            End If

            If cmbPayMode.Text = "Cash" Then

                objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                           & " cheque_no, cheque_date,Source,voucher_code, coa_detail_id, Employee_Id, Reference, UserName, other_voucher , Post, Remarks)" _
                           & " VALUES(" & CID & ", 1, 3, N'" & VoucherNo & "', N'" & Me.dtpPOSDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                           & " NULL, NULL, N'" & Me.Name & "',N'" & Me.txtDocNo.Text.Replace("'", "''") & "', " & CAID & ", " & IIf(Me.cmbSalesPerson.SelectedIndex = -1, 0, Me.cmbSalesPerson.SelectedValue) & ", " _
                           & " N'" & Me.txtDocNo.Text & " " & txtCustomer.Text & "', '" & LoginUserName & "', 0  , 1, N'" & Remarks.Replace("'", "''") & "')" _
                           & " SELECT @@IDENTITY"
                lngVoucherMasterId = objCommand.ExecuteScalar


                objCommand.CommandText = ""
                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
                                        & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & CAID & ", " & Val(Me.txtCash.Text) & ", 0, N'Receipt Ref By " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No : " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                objCommand.ExecuteNonQuery()
                objCommand.CommandText = ""
                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                     & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & Code & ", " & 0 & ",  " & Val(Me.txtCash.Text) & ", N'Receipt Ref: " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No : " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                objCommand.ExecuteNonQuery()
            End If

            If cmbPayMode.Text = "Bank" Then

                objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                           & " cheque_no, cheque_date,Source,voucher_code, coa_detail_id, Employee_Id, Reference, UserName, other_voucher, Post, Remarks)" _
                           & " VALUES(" & CID & ", 1, 5, N'" & VoucherNo & "', N'" & Me.dtpPOSDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                           & "  N'" & IIf(Me.chkOnline.Checked = True, "Online", Me.txtChequeNo.Text.Replace("'", "''")) & "', N'" & dtpChequeDate1.Value.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & Me.Name & "',N'" & Me.txtDocNo.Text.Replace("'", "''") & "', " & BAID & ", " & IIf(Me.cmbSalesPerson.SelectedIndex = -1, 0, Me.cmbSalesPerson.SelectedValue) & ", " _
                           & " N'" & Me.txtDocNo.Text & " " & txtCustomer.Text & "', '" & LoginUserName & "', 0 , 1, N'" & Remarks.Replace("'", "''") & "' )" _
                           & " SELECT @@IDENTITY"
                lngVoucherMasterId = objCommand.ExecuteScalar


                objCommand.CommandText = ""
                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
                                        & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & BAID & ", " & Val(Me.txtCash.Text) & ", 0, N'Receipt Ref By " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No : " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                objCommand.ExecuteNonQuery()
                objCommand.CommandText = ""
                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                     & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & Code & ", " & 0 & ",  " & Val(Me.txtCash.Text) & ", N'Receipt Ref: " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No : " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                objCommand.ExecuteNonQuery()
            End If

            If cmbPayMode.Text = "Credit Card" Then

                objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                           & " cheque_no, cheque_date,Source,voucher_code, coa_detail_id, Employee_Id, Reference, UserName, other_voucher, Post, Remarks )" _
                           & " VALUES(" & CID & ", 1, 5, N'" & VoucherNo & "', N'" & Me.dtpPOSDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                           & "  N'CreditCard : " & Me.txtCreditCardNo.Text.Replace("'", "''") & "', NULL, N'" & Me.Name & "',N'" & Me.txtDocNo.Text.Replace("'", "''") & "', " & BAID & ", " & IIf(Me.cmbSalesPerson.SelectedIndex = -1, 0, Me.cmbSalesPerson.SelectedValue) & ", " _
                           & " N'" & Me.txtDocNo.Text & " " & txtCustomer.Text & "', '" & LoginUserName & "', 0 , 1, N'" & Remarks.Replace("'", "''") & "' )" _
                           & " SELECT @@IDENTITY"
                lngVoucherMasterId = objCommand.ExecuteScalar


                objCommand.CommandText = ""
                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
                                        & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & CreditCardId & ", " & Val(Me.txtNetTotal.Text) & ", 0, N'Receipt Ref By " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No : " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                objCommand.ExecuteNonQuery()
                objCommand.CommandText = ""
                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                     & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & Code & ", " & 0 & ",  " & Val(Me.txtNetTotal.Text) & ", N'Receipt Ref: " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No : " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                objCommand.ExecuteNonQuery()
            End If

            If cmbPayMode.Text = "Mix" Then

                If Cash > 0 Then
                    objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                           & " cheque_no, cheque_date,Source,voucher_code, coa_detail_id, Employee_Id, Reference, UserName, other_voucher, Post, Remarks )" _
                           & " VALUES(" & CID & ", 1, 3, N'" & VoucherNo & "', N'" & Me.dtpPOSDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                           & " NULL, NULL, N'" & Me.Name & "',N'" & Me.txtDocNo.Text.Replace("'", "''") & "', " & CAID & ", " & IIf(Me.cmbSalesPerson.SelectedIndex = -1, 0, Me.cmbSalesPerson.SelectedValue) & ", " _
                           & " N'" & Me.txtDocNo.Text & " " & txtCustomer.Text & "', '" & LoginUserName & "', 0 , 1, N'" & Remarks.Replace("'", "''") & "' )" _
                           & " SELECT @@IDENTITY"
                    lngVoucherMasterId = objCommand.ExecuteScalar


                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
                                            & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & CAID & ", " & Cash & ", 0, N'Receipt Ref By " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No : " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                    objCommand.ExecuteNonQuery()
                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                         & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & Code & ", " & 0 & ",  " & Cash & ", N'Receipt Ref: " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No : " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                    objCommand.ExecuteNonQuery()
                End If

                If Bank > 0 Then
                    objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                           & " cheque_no, cheque_date,Source,voucher_code, coa_detail_id, Employee_Id, Reference, UserName, other_voucher, Post, Remarks )" _
                           & " VALUES(" & CID & ", 1, 5, N'" & VoucherNo & "', N'" & Me.dtpPOSDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                           & "  N'" & IIf(Online = True, "Online", ChequeNo) & "', N'" & dtpChequeDate1.Value.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & Me.Name & "',N'" & Me.txtDocNo.Text.Replace("'", "''") & "', " & BAID & ", " & IIf(Me.cmbSalesPerson.SelectedIndex = -1, 0, Me.cmbSalesPerson.SelectedValue) & ", " _
                           & " N'" & Me.txtDocNo.Text & " " & txtCustomer.Text & "', '" & LoginUserName & "', 0 , 1, N'" & Remarks.Replace("'", "''") & "' )" _
                           & " SELECT @@IDENTITY"
                    lngVoucherMasterId = objCommand.ExecuteScalar
                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
                                            & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & BAID & ", " & Bank & ", 0, N'Receipt Ref By " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No : " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                    objCommand.ExecuteNonQuery()
                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                         & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & Code & ", " & 0 & ",  " & Bank & ", N'Receipt Ref: " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No : " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                    objCommand.ExecuteNonQuery()
                End If

                If CreditCard > 0 Then
                    objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                           & " cheque_no, cheque_date,Source,voucher_code, coa_detail_id, Employee_Id, Reference, UserName, other_voucher, Post, Remarks )" _
                           & " VALUES(" & CID & ", 1, 5, N'" & VoucherNo & "', N'" & Me.dtpPOSDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                           & "  NULL, NULL, N'" & Me.Name & "',N'" & Me.txtDocNo.Text.Replace("'", "''") & "', " & BAID & ", " & IIf(Me.cmbSalesPerson.SelectedIndex = -1, 0, Me.cmbSalesPerson.SelectedValue) & ", " _
                           & " N'" & Me.txtDocNo.Text & " " & txtCustomer.Text & "', '" & LoginUserName & "', 0 , 1, N'" & Remarks.Replace("'", "''") & "' )" _
                           & " SELECT @@IDENTITY"
                    lngVoucherMasterId = objCommand.ExecuteScalar


                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
                                            & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & CCAID & ", " & CreditCard & ", 0, N'Receipt Ref By " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No : " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                    objCommand.ExecuteNonQuery()
                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                         & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & Code & ", " & 0 & ",  " & CreditCard & ", N'Receipt Ref: " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No : " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                    objCommand.ExecuteNonQuery()
                End If

            End If


            If IsValidate() = True Then
                Call New StockDAL().Add(StockMaster, trans)
            End If
            PrintId = InvId
            trans.Commit()
            SaveActivityLog("POS", Me.Text, EnumActions.Save, LoginUserId, EnumRecordType.Sales, Me.txtDocNo.Text, True)
            ReSetControls()
            Save = True
        Catch ex As Exception
            trans.Rollback()
            Save = False
            ShowErrorMessage("An error occured while saving record" & ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Function

    Function GetVoucherNo() As String
        Dim docNo As String = String.Empty
        Dim VType As String = String.Empty
        If Me.cmbPayMode.Text = "Bank" Then
            VType = "BRV"
        ElseIf Bank > 0 Or CreditCard > 0 Then
            VType = "BRV"
        Else
            VType = "CRV"
        End If
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                Return GetSerialNo(VType + "-" + Microsoft.VisualBasic.Right(Me.dtpPOSDate.Value.Year, 2) + "-", "tblVoucher", "voucher_no")
            Else
                Dim strSQL As String = "Select * from ConfigValuesTable Where Config_type='VoucherNo'"
                Dim dr As DataRow = SBDal.UtilityDAL.ReturnDataRow(strSQL)
                If Not dr Is Nothing Then
                    If dr("config_Value") = "Monthly" Then
                        Return GetNextDocNo(VType & "-" & Format(Me.dtpPOSDate.Value, "yy") & Me.dtpPOSDate.Value.Month.ToString("00"), 4, "tblVoucher", "voucher_no")
                    Else
                        docNo = GetNextDocNo(VType, 6, "tblVoucher", "voucher_no")
                        Return docNo
                    End If
                Else
                    docNo = GetNextDocNo(VType, 6, "tblVoucher", "voucher_no")
                    Return docNo
                End If
                Return ""
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub
    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub
    Public Sub SetNavigationButtons(Mode As SBUtility.Utility.EnumDataMode, Optional Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub
    Public Function Update1(Optional Condition As String = "") As Boolean Implements IGeneral.Update
        Dim trans As OleDbTransaction
        Try
            If Code <= 0 Then
                If frmSearchCustomersVendors.CustomerCode > 0 AndAlso Code = 0 Then
                    Code = frmSearchCustomersVendors.CustomerCode
                End If
                If frmSearchCustomersVendors.CustomerName = "" Then
                    CName = frmSearchCustomersVendors.CustomerName
                    Code = Val(getConfigValueByType("DefaultAccountInPlaceCustomer").ToString)
                End If
            End If
            Dim objCommand As New OleDbCommand
            Dim objCon As OleDbConnection
            Dim i As Integer
            gobjLocationId = MyCompanyId
            Dim lngVoucherMasterId As Integer = GetVoucherId(Me.Name, Me.txtDocNo.Text)
            Dim InvAccountId As Integer = Val(getConfigValueByType("InvAccountId").ToString) 'GetConfigValue("InvAccountId") 'Inventory Account
            Dim CgsAccountId As Integer = Val(getConfigValueByType("CGSAccountId").ToString) 'GetConfigValue("CGSAccountId") 'Cost Of Good Sold Account
            Dim AccountId As Integer = Val(getConfigValueByType("SalesCreditAccount").ToString) 'GetConfigValue("SalesCreditAccount")
            Dim SalesTaxId As Integer = 0I
            If CID > 0 Then
                Dim str As String = "SELECT SalesTaxAccountId FROM CompanyDefTable WHERE CompanyId = " & CID & " AND SalesTaxAccountId IS NOT NULL"
                Dim dt As DataTable = GetDataTable(str)
                If dt.Rows.Count > 0 Then
                    SalesTaxId = Val(dt.Rows(0).Item(0))
                End If
            End If
            If SalesTaxId = 0 Then
                SalesTaxId = Val(getConfigValueByType("SalesTaxCreditAccount").ToString) 'GetConfigValue("SalesTaxCreditAccount")
            End If
            Dim SEDAccountId As Integer = Val(getConfigValueByType("SEDAccountId").ToString) 'Val(GetConfigValue("SEDAccountId").ToString)
            Dim InsuranceAccountId As Integer = Val(getConfigValueByType("TransitInsuranceAccountId").ToString) 'Val(GetConfigValue("TransitInsuranceAccountId").ToString)
            AdjustmentExpAccount = Val(getConfigValueByType("AdjustmentExpAccount").ToString)
            Dim IsDiscountVoucher As Boolean = Convert.ToBoolean(getConfigValueByType("DiscountVoucherOnSale").ToString) 'Convert.ToBoolean(GetConfigValue("DiscountVoucherOnSale").ToString)
            Dim SalesDiscountAccount As Integer = Val(getConfigValueByType("SalesDiscountAccount").ToString) 'Val(GetConfigValue("SalesDiscountAccount").ToString)
            Dim GLAccountArticleDepartment As Boolean
            If Not getConfigValueByType("GLAccountArticleDepartment").ToString = "Error" Then
                GLAccountArticleDepartment = Convert.ToBoolean(getConfigValueByType("GLAccountArticleDepartment"))
            Else
                GLAccountArticleDepartment = False
            End If
            Dim blnCheckCurrentStockByItem As Boolean = False
            If Not getConfigValueByType("CheckCurrentStockByItem").ToString = "Error" Then
                blnCheckCurrentStockByItem = Convert.ToBoolean(getConfigValueByType("CheckCurrentStockByItem").ToString)
            End If
            'Validtion on Configuration Account Id 
            '25-9-2013 by imran ali....

            If InvAccountId <= 0 Then
                ShowErrorMessage("Purchase account is not map.")
                Me.dtpChequeDate1.Focus()
                Return False
            ElseIf CgsAccountId <= 0 Then
                ShowErrorMessage("Cost of good sold account is not map.")
                Me.dtpChequeDate1.Focus()
                Return False
            End If
            If AccountId <= 0 Then
                ShowErrorMessage("Sales account is not map.")
                Me.dtpChequeDate1.Focus()
                Return False
            End If
            If Val(Me.txtTax.Text) > 0 Then
                If SalesTaxId <= 0 Then
                    ShowErrorMessage("Sales tax account is not map.")
                    Me.dtpChequeDate1.Focus()
                    Return False
                End If
            End If
            If Val(Me.txtDisPercentage.Text) <> 0 AndAlso Val(Me.txtDiscount.Text) <> 0 Then
                If AdjustmentExpAccount <= 0 Then
                    ShowErrorMessage("Adjustment account is not map.")
                    Me.dtpPOSDate.Focus()
                    Return False
                End If
            End If
            If IsDiscountVoucher = True Then
                If SalesDiscountAccount <= 0 Then
                    ShowErrorMessage("Discount account is not map.")
                    Me.dtpChequeDate1.Focus()
                    Return False
                End If
            End If
            If Code = 0 Then
                ShowErrorMessage("Customer account is not map.")
                Return False
            End If




            Dim ReceiptVoucherFlg As String = Convert.ToString(getConfigValueByType("ReceiptVoucherOnSales").ToString) 'GetConfigValue("ReceiptVoucherOnSales").ToString
            Dim VoucherNo As String = GetVoucherNo()
            Dim DiscountedPrice As Double = 0
            Dim ExpenseChargeToCustomer As Boolean
            ExpenseChargeToCustomer = Convert.ToBoolean(getConfigValueByType("ExpenseChargeToCustomer").ToString) 'Convert.ToBoolean(GetConfigValue("ExpenseChargeToCustomer").ToString)
            Dim flgExcludeTaxPrice As Boolean = Convert.ToBoolean(getConfigValueByType("ExcludeTaxPrice").ToString)
            Dim DCStockImpact As Boolean = False
            If Not getConfigValueByType("DCStockImpact").ToString = "Error" Then
                DCStockImpact = Convert.ToBoolean(getConfigValueByType("DCStockImpact").ToString)
            End If
            Dim AdjustmentAmountNew As Integer = 0I
            Dim SalesAccount As Integer = 0I


            ''TASK TFS4548
            If Me.txtCustomer.Text.Length > 0 Then
                Remarks = "Customer Name : " & txtCustomer.Text & ""
            End If
            If Me.txtCNIC.Text.Length > 0 Then
                If Remarks.Length > 0 Then
                    Remarks += ", CNIC : " & txtCNIC.Text & ""
                Else
                    Remarks += " CNIC : " & txtCNIC.Text & ""
                End If
            End If
            If Me.txtMobile.Text.Length > 0 Then
                If Remarks.Length > 0 Then
                    Remarks += ", Mobile : " & txtMobile.Text & ""
                Else
                    Remarks += "  Mobile : " & txtMobile.Text & ""
                End If
            End If
            If Me.txtRemarks.Text.Length > 0 Then
                If Remarks.Length > 0 Then
                    Remarks += ", Remarks : " & txtRemarks.Text & ""
                Else
                    Remarks += "  Remarks : " & txtRemarks.Text & ""
                End If
            End If
            ''END TASK TFS4548



            objCon = Con
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Open()
            objCommand.Connection = objCon

            Dim cmd As New OleDbCommand
            cmd.Connection = objCon
            cmd.CommandType = CommandType.Text
            Dim dtSavedItems As New DataTable
            trans = objCon.BeginTransaction
            objCommand.CommandType = CommandType.Text
            objCommand.Transaction = trans
            'Ali Faisal : TFS4415 : Configuration based POS revision is require
            objCommand.CommandText = ""
            objCommand.CommandText = "Update SalesMasterTable set SalesNo =N'" & txtDocNo.Text & "',SalesDate=N'" & dtpPOSDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "',CustomerCode=" & Code & ",EmployeeCode=" & Val(cmbSalesPerson.SelectedValue.ToString) & ", " _
                 & " SalesQty=" & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & ", SalesAmount=" & Val(txtNetTotal.Text) & ", CashPaid=" & Val(txtCash.Text) & ", Remarks=N'" & Remarks.Replace("'", "''") & "', UpdateUserName=N'" & LoginUserName & "', CostCenterId=" & CCID & ", Adj_Flag=" & Val(txtDiscount.Text) & ", Adj_Percentage=" & Val(txtDisPercentage.Text) & ", InvoiceType=N'" & Me.cmbPayMode.Text.Replace("'", "''") & "', MobileNo = '" & txtMobile.Text.Replace("'", "''") & "', CustomerName = '" & txtCustomer.Text.Replace("'", "''") & "', CNIC = '" & Me.txtCNIC.Text.Replace("'", "''") & "', HoldFlag = " & 0 & ",SalesStartTime= " & IIf(SalesStartTime = Date.MinValue, "NULL", "'" & SalesStartTime.ToString("yyyy-M-d h:mm:ss tt") & "'") & ",HoldStartTime=" & IIf(HoldStartTime = Date.MinValue, "NULL", "'" & HoldStartTime.ToString("yyyy-M-d h:mm:ss tt") & "'") & ",HoldEndTime=" & IIf(HoldEndTime = Date.MinValue, "NULL", "'" & HoldEndTime.ToString("yyyy-M-d h:mm:ss tt") & "'") & ", PackingManId = " & Me.cmbPackingMan.SelectedValue & ", BillMakerId = " & Me.cmbBillMaker.SelectedValue & ",RevisionNumber = IsNull(RevisionNumber, 0) + 1, Remarks1 = N'" & txtRemarks.Text.Replace("'", "''") & "' Where SalesID= " & SalesId & ""
            objCommand.ExecuteNonQuery()
            'TFS2211: Waqar Added these Lines for Adjustment od Bardana
            'Start TFS2211:
            Dim str3 As String = "Select SalesId from SalesMasterTable where SalesNo = '" & txtDocNo.Text & "'"
            Dim dt3 As DataTable = GetDataTable(str3)
            If dt3.Rows.Count > 0 Then
                InvId = dt3.Rows(0).Item(0)
            End If
            'End TFS2211
            objCommand.CommandText = ""
            If lngVoucherMasterId > 0 Then
                objCommand.CommandText = "update tblVoucher set voucher_date=N'" & dtpPOSDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "'" _
                                   & " , Employee_Id=" & IIf(Me.cmbSalesPerson.SelectedIndex = -1, 0, Me.cmbSalesPerson.SelectedValue) & ", Remarks=N'" & Remarks.Replace("'", "''") & "', voucher_type_id = 7, Post = 1  where voucher_id=" & lngVoucherMasterId
                'End Task:M101
                objCommand.ExecuteNonQuery()
            Else

                objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                                    & " cheque_no, cheque_date,Source,voucher_code, Employee_Id,Remarks, UserName, Post)" _
                                    & " VALUES(" & gobjLocationId & ", 1,  7 , N'" & Me.txtDocNo.Text & "', N'" & Me.dtpPOSDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                                    & " NULL, NULL, N'" & Me.Name & "',N'" & Me.txtDocNo.Text & "', " & IIf(Me.cmbSalesPerson.SelectedIndex = -1, 0, Me.cmbSalesPerson.SelectedValue) & ", N'" & Me.Remarks.Replace("'", "''") & "', N'" & LoginUserName & "', 1)" _
                                    & " SELECT @@IDENTITY"
                lngVoucherMasterId = objCommand.ExecuteScalar

            End If
            objCommand.CommandText = ""
            objCommand.CommandText = "Delete from SalesDetailTable where SalesID = " & SalesId
            objCommand.ExecuteNonQuery()
            '***********************
            'Deleting Detail
            '***********************
            objCommand.CommandText = ""
            objCommand.CommandText = "delete from tblVoucherDetail where voucher_Id =" & lngVoucherMasterId
            objCommand.ExecuteNonQuery()

            objCommand.CommandText = ""
            StockList = New List(Of StockDetail)
            For i = 0 To grd.GetRows.Length - 1
                If blnCheckCurrentStockByItem = True Then
                    CheckCurrentStockByItem(Me.grd.GetRows(i).Cells(grdDetail.ArticleId).Value, Val(Me.grd.GetRows(i).Cells(grdDetail.TotalQty).Value.ToString), Me.grd, , trans)
                End If
                'If GLAccountArticleDepartment = True Then
                '    InvAccountId = Val(grd.GetRows(i).Cells("InvAccountId").Value.ToString)
                '    AccountId = Val(grd.GetRows(i).Cells("SalesAccountId").Value.ToString)
                '    CgsAccountId = Val(grd.GetRows(i).Cells("CGSAccountId").Value.ToString)
                'End If
                Dim CostPrice As Double = 0D
                Dim CrrStock As Double = 0D
                DiscountedPrice = Me.grd.GetRows(i).Cells("DiscValue").Value
                Dim dblPurchasePrice As Double = 0D
                Dim dblCostPrice As Double = 0D
                Dim strPriceData() As String = GetRateByItem(Val(Me.grd.GetRows(i).Cells(grdDetail.ArticleId).Value.ToString)).Split(",")
                If strPriceData.Length > 1 Then
                    dblCostPrice = Val(strPriceData(0).ToString)
                    dblPurchasePrice = Val(strPriceData(1).ToString)
                End If
                If flgAvgRate = True Then
                    If dblCostPrice > 0 Then
                        CostPrice = dblCostPrice
                    Else
                        CostPrice = dblPurchasePrice
                    End If
                Else
                    CostPrice = dblPurchasePrice
                End If

                ''TASK TFS4548
                Comments = "Item : " & grd.GetRows(i).Cells(grdDetail.ArticleDescription).Value.ToString & ", Qty: " & Val(grd.GetRows(i).Cells(grdDetail.TotalQty).Value) & " "
                'If Remarks.Length > 0 Then
                '    Comments += ", " & Remarks & ""
                'End If
                ''END TASK TFS4548
                If Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) > 0 Then
                    StockDetail = New StockDetail
                    StockDetail.StockTransId = 0
                    StockDetail.LocationId = LID
                    StockDetail.ArticleDefId = grd.GetRows(i).Cells("ArticleId").Value
                    StockDetail.InQty = 0
                    StockDetail.OutQty = Val(grd.GetRows(i).Cells("TotalQty").Value.ToString)
                    StockDetail.Rate = IIf(CostPrice = 0, Val(grd.GetRows(i).Cells(grdDetail.PurchasePrice).Value), CostPrice)
                    StockDetail.InAmount = 0
                    StockDetail.OutAmount = ((Val(grd.GetRows(i).Cells("TotalQty").Value)) * IIf(CostPrice = 0, Val(grd.GetRows(i).Cells(grdDetail.PurchasePrice).Value), CostPrice))
                    StockDetail.Remarks = Comments.Replace("'", "''") & ", Rate : " & StockDetail.Rate.ToString & ", " & Remarks.Replace("'", "''")
                    CostPrice = StockDetail.Rate
                    StockDetail.PackQty = Val(grd.GetRows(i).Cells("PackQty").Value.ToString)
                    StockDetail.Out_PackQty = Val(grd.GetRows(i).Cells("Qty").Value.ToString)
                    StockDetail.In_PackQty = 0
                    StockList.Add(StockDetail)
                End If
                objCommand.CommandText = ""
                objCommand.CommandText = "Insert into SalesDetailTable (SalesId, ArticleDefId,ArticleSize, Sz1, Sz7, Qty, LocationId,PostDiscountPrice,DiscountId,DiscountFactor,DiscountValue,Price, PurchasePrice, PackPrice, TaxPercent, CurrencyAmount, Comments) values( " _
                              & " " & SalesId & " ," & Val(grd.GetRows(i).Cells(grdDetail.ArticleId).Value) & ", '" & Me.grd.GetRows(i).Cells(grdDetail.PackSize).Value.ToString & "', " & Val(grd.GetRows(i).Cells(grdDetail.PackQty).Value) & " ," & Val(grd.GetRows(i).Cells(grdDetail.Qty).Value) & ", " _
                              & " " & Val(grd.GetRows(i).Cells("TotalQty").Value) & ", " & LID & ", " _
                              & " " & Val(grd.GetRows(i).Cells("PDP").Value) & ", " & grd.GetRows(i).Cells(grdDetail.DiscountId).Value & "," & Val(Me.grd.GetRows(i).Cells("DiscFactor").Value) & ", " & Val(Me.grd.GetRows(i).Cells(grdDetail.DiscValue).Value) & ", " & Val(grd.GetRows(i).Cells(grdDetail.Rate).Value) & ", " & Val(Me.grd.GetRows(i).Cells(grdDetail.PurchasePrice).Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells(grdDetail.PackRate).Value.ToString) & ", " & IIf(grd.GetRows(i).Cells(grdDetail.Tax).Value.ToString = "", 0, grd.GetRows(i).Cells(grdDetail.Tax).Value) & ", " & Val(Me.grd.GetRows(i).Cells("NetAmount").Value.ToString) & ", '" & Comments.Replace("'", "''") & ", Rate : " & grd.GetRows(i).Cells(grdDetail.Rate).Value.ToString & ", " & Remarks.Replace("'", "''") & "')Select @@Identity"
                objCommand.ExecuteNonQuery()

                'TFS2211: Waqar Raza: Added These Lines to add Bardana into Inventroy when an Item Pack Quantity completes Using Loose
                'Start TFS2211:
                Dim strmaster As String
                strmaster = "Select MasterID from ArticleDefView where ArticleId = " & Val(grd.GetRows(i).Cells(grdDetail.ArticleId).Value) & ""
                Dim dtmaster As DataTable = GetDataTable(strmaster)
                Dim MasterId As Integer
                If dtmaster.Rows.Count > 0 Then
                    MasterId = dtmaster.Rows(0).Item("MasterID")
                End If
                Dim strBardanaItem As String
                Dim BardanaItem As Integer
                strBardanaItem = "Select RelatedArticleId from tblRelatedItem where ArticleId = " & MasterId & ""
                Dim BardanaDT As DataTable
                BardanaDT = GetDataTable(strBardanaItem)
                If BardanaDT.Rows.Count > 0 Then
                    BardanaItem = BardanaDT.Rows(0).Item("RelatedArticleId")
                Else
                    BardanaItem = 0
                End If
                If BardanaItem > 0 Then
                    objCommand.CommandText = ""
                    objCommand.CommandText = "Delete from tblBardanaAdjustment where SalesDetailId = " & InvId
                    objCommand.ExecuteNonQuery()
                    objCommand.CommandText = ""
                    objCommand.CommandText = "Delete from StockDetailTable where StockTransId = " & Convert.ToInt32(StockTransId(txtDocNo.Text))
                    objCommand.ExecuteNonQuery()
                    objCommand.CommandText = ""
                    objCommand.CommandText = "Delete from StockMasterTable where StockTransId = " & Convert.ToInt32(StockTransId(txtDocNo.Text))
                    objCommand.ExecuteNonQuery()

                    'Dim value As Object = objCommand.ExecuteScalar()
                    'If value Is DBNull.Value Or value Is Nothing Then
                    '    InvId = 0
                    'Else
                    '    InvId = Convert.ToInt32(value)
                    'End If
                    ''InvId = objCommand.ExecuteScalar()
                    'If SalesDetailId > 0 AndAlso InvId = 0 Then
                    '    InvId = SalesDetailId
                    'End If
                    If Me.grd.GetRows(i).Cells(grdDetail.PackSize).Value.ToString = "Loose" AndAlso getConfigValueByType("BardanaAdjustmentOnPOS").ToString = "True" Then
                        objCommand.CommandText = "INSERT INTO tblBardanaAdjustment(SalesDetailId, ArticleId, Qty) VALUES(" & InvId & "," & Val(grd.GetRows(i).Cells(grdDetail.ArticleId).Value) & "," & Val(grd.GetRows(i).Cells(grdDetail.TotalQty).Value) & ")"
                        objCommand.ExecuteNonQuery()

                        objCommand.CommandText = "SELECT tblBardanaAdjustment.ArticleId, isnull(SUM(tblBardanaAdjustment.Qty),0) - isnull(SUM(tblBardanaAdjustment.AdjustedQty),0) AS AdjustedQty, ArticleDefTable.PackQty FROM tblBardanaAdjustment INNER JOIN ArticleDefTable ON tblBardanaAdjustment.ArticleId = ArticleDefTable.ArticleId WHERE ArticleDefTable.ArticleId = " & Val(grd.GetRows(i).Cells(grdDetail.ArticleId).Value) & " GROUP BY tblBardanaAdjustment.ArticleId, ArticleDefTable.PackQty"
                        Dim da As New OleDbDataAdapter
                        Dim dt As New DataTable
                        da.SelectCommand = objCommand
                        da.Fill(dt)
                        dt.AcceptChanges()

                        'For Each r As DataRow In dt.Rows
                        If Val(dt.Rows(0).Item(1).ToString) >= Val(dt.Rows(0).Item(2).ToString) Then
                            'objCommand.CommandText = "Insert Into StockMasterTable (DocNo, DocDate, Project) " _
                            '                             & " Values(N'" & txtDocNo.Text & "', N'" & dtpPOSDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " & IIf(CCID = Nothing, "NULL", CCID) & ") Select @@Identity "
                            'TransId = objCommand.ExecuteScalar()
                            Dim value1 As Integer = Val(dt.Rows(0).Item(1).ToString) / Val(dt.Rows(0).Item(2).ToString)
                            For i1 As Integer = 1 To value1
                                objCommand.CommandText = "INSERT INTO tblBardanaAdjustment(SalesDetailId,ArticleId,AdjustedQty) VALUES(" & InvId & "," & dt.Rows(0).Item(0).ToString & "," & dt.Rows(0).Item(2).ToString & ")"
                                objCommand.ExecuteNonQuery()
                            Next
                            StockDetail = New StockDetail
                            StockDetail.StockTransId = 0
                            StockDetail.LocationId = LID
                            StockDetail.ArticleDefId = BardanaItem
                            StockDetail.InQty = value1
                            StockDetail.Remarks = "'Added through POS Bardana Adjustment Against " & txtDocNo.Text & "'"
                            StockList.Add(StockDetail)
                            'objCommand.CommandText = "Insert Into StockDetailTable(StockTransId, LocationId, ArticleDefId, InQty, Remarks) " _
                            '                            & " Values (" & TransId & ", " & LID & ",  " & BardanaItem & ", " & 1 & ", 'Added through POS Bardana Adjustment Against " & txtDocNo.Text & "')"
                            ''End Task:M16
                            'objCommand.ExecuteNonQuery()

                        End If
                    End If
                End If
                'Start TFS2211:

                If IsDiscountVoucher = True Then
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, direction, sp_refrence, ArticleDefId) " _
                & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & Code & ", " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * Val(grd.GetRows(i).Cells("PDP").Value.ToString) & ", 0, N'" & Comments.Replace("'", "''") & ", Rate : " & grd.GetRows(i).Cells("PDP").Value.ToString & ", " & Remarks.Replace("'", "''") & "', " & CCID & ", " & grd.GetRows(i).Cells(grdDetail.ArticleId).Value & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells(grdDetail.ArticleId).Value.ToString) & ")"
                    objCommand.ExecuteNonQuery()
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
                                              & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & AccountId & ", " & 0 & ",  " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * Val(grd.GetRows(i).Cells("PDP").Value.ToString) & ", '" & Comments.Replace("'", "''") & ", Rate : " & grd.GetRows(i).Cells("PDP").Value.ToString & ", " & Remarks.Replace("'", "''") & "', " & CCID & ", " & grd.GetRows(i).Cells(grdDetail.ArticleId).Value & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells(grdDetail.ArticleId).Value.ToString) & ")"
                    objCommand.ExecuteNonQuery()
                Else
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, direction, sp_refrence, ArticleDefId) " _
                & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & Code & ", " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * Val(grd.GetRows(i).Cells("Rate").Value.ToString) & ", 0, N'" & Comments.Replace("'", "''") & ", Rate : " & grd.GetRows(i).Cells("Rate").Value.ToString & ", " & Remarks.Replace("'", "''") & "', " & CCID & ", " & grd.GetRows(i).Cells(grdDetail.ArticleId).Value & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells(grdDetail.ArticleId).Value.ToString) & ")"
                    objCommand.ExecuteNonQuery()
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
                                              & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & AccountId & ", " & 0 & ",  " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * Val(grd.GetRows(i).Cells("Rate").Value.ToString) & ",'" & Comments.Replace("'", "''") & ", Rate : " & grd.GetRows(i).Cells("PDP").Value.ToString & ", " & Remarks.Replace("'", "''") & "', " & CCID & ", " & grd.GetRows(i).Cells(grdDetail.ArticleId).Value & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells(grdDetail.ArticleId).Value.ToString) & ")"
                    objCommand.ExecuteNonQuery()
                End If
                'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, direction, sp_refrence, ArticleDefId) " _
                '    & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & Code & ", " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * Val(grd.GetRows(i).Cells("PDP").Value.ToString) & ", 0, N'" & CName & "', " & CCID & ", " & grd.GetRows(i).Cells(grdDetail.ArticleId).Value & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells(grdDetail.ArticleId).Value.ToString) & ")"
                'objCommand.ExecuteNonQuery()
                'objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction, sp_refrence,ArticleDefId) " _
                '                          & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & AccountId & ", " & 0 & ",  " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * Val(grd.GetRows(i).Cells("PDP").Value.ToString) & ",N'" & Me.txtRemarks.Text.Replace("'", "''") & "', " & CCID & ", " & grd.GetRows(i).Cells(grdDetail.ArticleId).Value & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells(grdDetail.ArticleId).Value.ToString) & ")"
                'objCommand.ExecuteNonQuery()
                Price = Val(grd.GetRows(i).Cells(grdDetail.Rate).Value)
                PDP = Val(grd.GetRows(i).Cells(grdDetail.PDP).Value)
                If PDP = 0 Then
                    PDP = Price
                End If
                If Price < PDP Then
                    Price = PDP
                End If
                If IsDiscountVoucher = True Then
                    If DiscountedPrice > 0 Then
                        objCommand.CommandText = ""
                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, direction, sp_refrence, ArticleDefId) " _
                                                                                            & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & Val(SalesDiscountAccount) & ", " & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * DiscountedPrice & ", 0, N'" & Comments.Replace("'", "''") & ", Rate : " & DiscountedPrice.ToString & ", " & Remarks.Replace("'", "''") & "', " & CCID & ", " & grd.GetRows(i).Cells(grdDetail.ArticleId).Value & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells(grdDetail.ArticleId).Value.ToString) & ")"
                        objCommand.ExecuteNonQuery()
                        objCommand.CommandText = ""
                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, direction, sp_refrence, ArticleDefId) " _
                                                                                            & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & Code & ", 0," & Val(grd.GetRows(i).Cells("TotalQty").Value.ToString) * DiscountedPrice & ", N'Ref Discount On Sales: " & Comments.Replace("'", "''") & ", Rate : " & DiscountedPrice.ToString & ", " & Remarks.Replace("'", "''") & "', " & CCID & ", " & grd.GetRows(i).Cells(grdDetail.ArticleId).Value & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "', " & Val(Me.grd.GetRows(i).Cells(grdDetail.ArticleId).Value.ToString) & ")"
                        objCommand.ExecuteNonQuery()
                    End If
                End If
                If Val(Me.txtTax.Text) > 0 Then
                    If flgExcludeTaxPrice = False Then
                        objCommand.CommandText = ""
                        objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
                                               & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & Code & ", " & Val(Me.txtTax.Text) & ", 0, 'Tax Ref: " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No: " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                        objCommand.ExecuteNonQuery()
                    End If
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                         & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & SalesTaxId & ", " & 0 & ",  " & Val(Me.txtTax.Text) & ", 'Tax Ref By " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No: " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                    objCommand.ExecuteNonQuery()
                End If
            Next
            'Ali Faisal : TFS4415 : Configuration based POS revision is require
            If getConfigValueByType("IsDuplicateSales").ToString.ToUpper = "TRUE" Then
                Call CreateDuplicateSales(SalesId, "Update", trans)
            End If
            If Val(Me.txtDiscount.Text) > 0 Then
                objCommand.CommandText = ""
                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
                                       & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & Me.AdjustmentExpAccount & ", " & Val(txtDiscount.Text) & ", 0, 'Adjustment Ref: " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No: " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "')"
                objCommand.ExecuteNonQuery()
                objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                     & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & Code & ", " & 0 & ",  " & Val(txtDiscount.Text) & ", 'Adjustment Ref: " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No: " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "')"
                objCommand.ExecuteNonQuery()
            End If

            ''''***************''''
            '''Receipt Vouchers''''
            ''''***************''''

            If cmbPayMode.Text = "Credit" Then
                If ExistingVoucherFlg = True Then

                    objCommand.CommandText = ""
                    objCommand.CommandText = "DELETE FROM tblVoucherDetail WHERE Voucher_Id=" & VoucherId
                    objCommand.ExecuteNonQuery()

                    objCommand.CommandText = ""
                    objCommand.CommandText = "DELETE FROM tblVoucher WHERE Voucher_Id=" & VoucherId
                    objCommand.ExecuteNonQuery()

                End If
            End If

            If cmbPayMode.Text = "Cash" AndAlso txtCash.Text <> 0 Then
                If ExistingVoucherFlg = False Then

                    objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                               & " cheque_no, cheque_date,Source,voucher_code, coa_detail_id, Employee_Id, Reference, UserName, other_voucher, Post, Remarks )" _
                               & " VALUES(" & CID & ", 1, 3, N'" & VoucherNo & "', N'" & Me.dtpPOSDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                               & " NULL, NULL, N'" & Me.Name & "',N'" & Me.txtDocNo.Text.Replace("'", "''") & "', " & CAID & ", " & IIf(Me.cmbSalesPerson.SelectedIndex = -1, 0, Me.cmbSalesPerson.SelectedValue) & ", " _
                               & " N'" & Me.txtDocNo.Text & " " & txtCustomer.Text & "', '" & LoginUserName & "', 0 , 1, '" & Remarks.Replace("'", "''") & "' )" _
                               & " SELECT @@IDENTITY"
                    lngVoucherMasterId = objCommand.ExecuteScalar

                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
                                            & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & CAID & ", " & Val(Me.txtCash.Text) & ", 0, N'Receipt Ref By " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No: " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                    objCommand.ExecuteNonQuery()
                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                         & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & Code & ", " & 0 & ",  " & Val(Me.txtCash.Text) & ", N'Receipt Ref: " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No: " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                    objCommand.ExecuteNonQuery()

                Else

                    objCommand.CommandText = ""
                    objCommand.CommandText = "Update tblVoucher Set Voucher_Type_Id= 3, Voucher_No=N'" & Voucher_No & "',  Voucher_Date=N'" & dtpPOSDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', Cheque_No= NULL , Cheque_Date= NULL , coa_detail_id=" & CAID & ", Employee_Id=" & IIf(Me.cmbSalesPerson.SelectedIndex = -1, 0, Me.cmbSalesPerson.SelectedValue) & ", Post = 1, Remarks = '" & Remarks.Replace("'", "''") & "' WHERE Voucher_Id=" & VoucherId
                    objCommand.ExecuteNonQuery()

                    objCommand.CommandText = ""
                    objCommand.CommandText = "DELETE FROM tblVoucherDetail WHERE Voucher_Id=" & VoucherId
                    objCommand.ExecuteNonQuery()

                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
                                            & " VALUES(" & VoucherId & ", " & CID & ", " & CAID & ", " & Val(Me.txtCash.Text) & ", 0, N'Receipt Ref By " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No: " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                    objCommand.ExecuteNonQuery()
                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                         & " VALUES(" & VoucherId & ", " & CID & ", " & Code & ", " & 0 & ",  " & Val(Me.txtCash.Text) & ", N'Receipt Ref: " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No: " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                    objCommand.ExecuteNonQuery()
                End If
            End If

            If cmbPayMode.Text = "Bank" Then
                If ExistingVoucherFlg = False Then
                    objCommand.CommandText = ""
                    objCommand.CommandText = "Update tblVoucher Set Voucher_Type_Id= 5, Voucher_No=N'" & Voucher_No & "',  Voucher_Date=N'" & VoucherDate.ToString("yyyy-M-d h:mm:ss tt") & "', Cheque_No= NULL , Cheque_Date= NULL , coa_detail_id=" & BAID & ", Employee_Id=" & IIf(Me.cmbSalesPerson.SelectedIndex = -1, 0, Me.cmbSalesPerson.SelectedValue) & ", Post = 1, Remarks = '" & Remarks.Replace("'", "''") & "' WHERE Voucher_Id=" & VoucherId
                    objCommand.ExecuteNonQuery()

                    objCommand.CommandText = ""
                    objCommand.CommandText = "DELETE FROM tblVoucherDetail WHERE Voucher_Id=" & VoucherId
                    objCommand.ExecuteNonQuery()

                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
                                            & " VALUES(" & VoucherId & ", " & CID & ", " & BAID & ", " & Val(Me.txtCash.Text) & ", 0, N'Receipt Ref By " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No: " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                    objCommand.ExecuteNonQuery()
                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                         & " VALUES(" & VoucherId & ", " & CID & ", " & Code & ", " & 0 & ",  " & Val(Me.txtCash.Text) & ", N'Receipt Ref: " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No: " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                    objCommand.ExecuteNonQuery()

                Else

                    objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                               & " cheque_no, cheque_date,Source,voucher_code, coa_detail_id, Employee_Id, Reference, UserName, other_voucher, Post, Remarks )" _
                               & " VALUES(" & CID & ", 1, 5, N'" & VoucherNo & "', N'" & Me.dtpPOSDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                               & "  N'" & Me.txtChequeNo.Text.Replace("'", "''") & "', N'" & dtpChequeDate1.Value.ToString("yyyy-M-d h:mm:ss tt") & "', N'" & Me.Name & "',N'" & Me.txtDocNo.Text.Replace("'", "''") & "', " & BAID & ", " & IIf(Me.cmbSalesPerson.SelectedIndex = -1, 0, Me.cmbSalesPerson.SelectedValue) & ", " _
                               & " N'" & Me.txtDocNo.Text & " " & txtCustomer.Text & "', '" & LoginUserName & "', 0 , 1, '" & Remarks.Replace("'", "''") & "' )" _
                               & " SELECT @@IDENTITY"
                    lngVoucherMasterId = objCommand.ExecuteScalar


                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
                                            & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & BAID & ", " & Val(Me.txtCash.Text) & ", 0, N'Receipt Ref By " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No: " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                    objCommand.ExecuteNonQuery()
                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                         & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & Code & ", " & 0 & ",  " & Val(Me.txtCash.Text) & ", N'Receipt Ref: " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No: " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                    objCommand.ExecuteNonQuery()
                End If
            End If

            If cmbPayMode.Text = "Credit Card" Then
                If ExistingVoucherFlg = True Then
                    objCommand.CommandText = ""
                    objCommand.CommandText = "Update tblVoucher Set Voucher_Type_Id= 5, Voucher_No=N'" & Voucher_No & "',  Voucher_Date=N'" & VoucherDate.ToString("yyyy-M-d h:mm:ss tt") & "', Cheque_No= NULL , Cheque_Date= NULL , coa_detail_id=" & BAID & ", Employee_Id=" & IIf(Me.cmbSalesPerson.SelectedIndex = -1, 0, Me.cmbSalesPerson.SelectedValue) & ", Post = 1, Remarks = '" & Remarks.Replace("'", "''") & "' WHERE Voucher_Id=" & VoucherId
                    objCommand.ExecuteNonQuery()

                    objCommand.CommandText = ""
                    objCommand.CommandText = "DELETE FROM tblVoucherDetail WHERE Voucher_Id=" & VoucherId
                    objCommand.ExecuteNonQuery()

                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
                                            & " VALUES(" & VoucherId & ", " & CID & ", " & CreditCardId & ", " & Val(Me.txtNetTotal.Text) & ", 0, N'Receipt Ref By " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No: " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                    objCommand.ExecuteNonQuery()
                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                         & " VALUES(" & VoucherId & ", " & CID & ", " & Code & ", " & 0 & ",  " & Val(Me.txtNetTotal.Text) & ", N'Receipt Ref: " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No: " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                    objCommand.ExecuteNonQuery()
                Else
                    objCommand.CommandText = "INSERT INTO tblVoucher(location_id, finiancial_year_id, voucher_type_id, voucher_no, voucher_date, " _
                               & " cheque_no, cheque_date,Source,voucher_code, coa_detail_id, Employee_Id, Reference, UserName, other_voucher, Post, Remarks )" _
                               & " VALUES(" & CID & ", 1, 5, N'" & VoucherNo & "', N'" & Me.dtpPOSDate.Value.ToString("yyyy-M-d h:mm:ss tt") & "', " _
                               & "  N'CreditCard : " & Me.txtCreditCardNo.Text.Replace("'", "''") & "', NULL, N'" & Me.Name & "',N'" & Me.txtDocNo.Text.Replace("'", "''") & "', " & BAID & ", " & IIf(Me.cmbSalesPerson.SelectedIndex = -1, 0, Me.cmbSalesPerson.SelectedValue) & ", " _
                               & " N'" & Me.txtDocNo.Text & " " & txtCustomer.Text & "', '" & LoginUserName & "', 0 , 1, '" & Remarks.Replace("'", "''") & "' )" _
                               & " SELECT @@IDENTITY"
                    lngVoucherMasterId = objCommand.ExecuteScalar


                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
                                            & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & CreditCardId & ", " & Val(Me.txtNetTotal.Text) & ", 0, N'Receipt Ref By " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No: " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                    objCommand.ExecuteNonQuery()
                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                         & " VALUES(" & lngVoucherMasterId & ", " & CID & ", " & Code & ", " & 0 & ",  " & Val(Me.txtNetTotal.Text) & ", N'Receipt Ref: " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No: " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                    objCommand.ExecuteNonQuery()
                End If
            End If

            If cmbPayMode.Text = "Mix" Then

                If Cash > 0 Then
                    objCommand.CommandText = ""
                    objCommand.CommandText = "Update tblVoucher Set Voucher_Type_Id= 3, Voucher_No=N'" & Voucher_No & "',  Voucher_Date=N'" & VoucherDate.ToString("yyyy-M-d h:mm:ss tt") & "', Cheque_No= NULL , Cheque_Date= NULL , coa_detail_id=" & CAID & ", Employee_Id=" & IIf(Me.cmbSalesPerson.SelectedIndex = -1, 0, Me.cmbSalesPerson.SelectedValue) & ", Post = 1, Remarks = '" & Remarks.Replace("'", "''") & "' WHERE Voucher_Id=" & VoucherId
                    objCommand.ExecuteNonQuery()

                    objCommand.CommandText = ""
                    objCommand.CommandText = "DELETE FROM tblVoucherDetail WHERE Voucher_Id=" & VoucherId
                    objCommand.ExecuteNonQuery()

                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
                                            & " VALUES(" & VoucherId & ", " & CID & ", " & CAID & ", " & Val(Me.txtCash.Text) & ", 0, N'Receipt Ref By " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No: " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                    objCommand.ExecuteNonQuery()
                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                         & " VALUES(" & VoucherId & ", " & CID & ", " & Code & ", " & 0 & ",  " & Val(Me.txtCash.Text) & ", N'Receipt Ref: " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No: " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                    objCommand.ExecuteNonQuery()
                End If

                If Bank > 0 Then
                    objCommand.CommandText = ""
                    objCommand.CommandText = "Update tblVoucher Set Voucher_Type_Id= 5, Voucher_No=N'" & Voucher_No & "',  Voucher_Date=N'" & VoucherDate.ToString("yyyy-M-d h:mm:ss tt") & "', Cheque_No= NULL , Cheque_Date= NULL , coa_detail_id=" & BAID & ", Employee_Id=" & IIf(Me.cmbSalesPerson.SelectedIndex = -1, 0, Me.cmbSalesPerson.SelectedValue) & ", Post = 1, Remarks = '" & Remarks.Replace("'", "''") & "' WHERE Voucher_Id=" & VoucherId
                    objCommand.ExecuteNonQuery()

                    objCommand.CommandText = ""
                    objCommand.CommandText = "DELETE FROM tblVoucherDetail WHERE Voucher_Id=" & VoucherId
                    objCommand.ExecuteNonQuery()

                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
                                            & " VALUES(" & VoucherId & ", " & CID & ", " & BAID & ", " & Val(Me.txtCash.Text) & ", 0, N'Receipt Ref By " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No: " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                    objCommand.ExecuteNonQuery()
                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                         & " VALUES(" & VoucherId & ", " & CID & ", " & Code & ", " & 0 & ",  " & Val(Me.txtCash.Text) & ", N'Receipt Ref: " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No: " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                    objCommand.ExecuteNonQuery()
                End If

                If CreditCard > 0 Then
                    objCommand.CommandText = ""
                    objCommand.CommandText = "Update tblVoucher Set Voucher_Type_Id= 5, Voucher_No=N'" & Voucher_No & "',  Voucher_Date=N'" & VoucherDate.ToString("yyyy-M-d h:mm:ss tt") & "', Cheque_No= NULL , Cheque_Date= NULL , coa_detail_id=" & BAID & ", Employee_Id=" & IIf(Me.cmbSalesPerson.SelectedIndex = -1, 0, Me.cmbSalesPerson.SelectedValue) & ", Post = 1, Remarks = '" & Remarks.Replace("'", "''") & "' WHERE Voucher_Id=" & VoucherId
                    objCommand.ExecuteNonQuery()

                    objCommand.CommandText = ""
                    objCommand.CommandText = "DELETE FROM tblVoucherDetail WHERE Voucher_Id=" & VoucherId
                    objCommand.ExecuteNonQuery()

                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount, comments, CostCenterId, sp_refrence) " _
                                            & " VALUES(" & VoucherId & ", " & CID & ", " & CCAID & ", " & CreditCard & ", 0, N'Receipt Ref By " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No: " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                    objCommand.ExecuteNonQuery()
                    objCommand.CommandText = ""
                    objCommand.CommandText = "INSERT INTO tblVoucherDetail(voucher_id, location_id, coa_detail_id, debit_amount, credit_amount,comments, CostCenterId, sp_refrence) " _
                                                         & " VALUES(" & VoucherId & ", " & CID & ", " & Code & ", " & 0 & ",  " & CreditCard & ", N'Receipt Ref: " & Comments.Replace("'", "''") & ", " & Remarks.Replace("'", "''") & ", Document No: " & txtDocNo.Text & "', " & CCID & ", N'" & Me.txtRemarks.Text.Replace("'", "''") & "' )"
                    objCommand.ExecuteNonQuery()
                End If

            End If
            If IsValidate() = True Then
                StockMaster.StockTransId = StockTransId(Me.txtDocNo.Text, trans)
                Call New StockDAL().Update(StockMaster, trans)
            End If
            PrintId = SalesId
            Update1 = True
            trans.Commit()
            SaveActivityLog("POS", Me.Text, EnumActions.Update, LoginUserId, EnumRecordType.Sales, Me.txtDocNo.Text, True)
            ReSetControls()

        Catch ex As Exception
            trans.Rollback()
            Update1 = False
            ShowErrorMessage("An error occured while saving record" & ex.Message)
        Finally
            Me.lblProgress.Visible = False
        End Try
    End Function
    Private Sub frmPOSEntry_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F2 Then
                Code = 0
                frmItemSearch.formName = Me.Name
                frmPOSHistory.ShowDialog()
            End If
            If e.KeyCode = Keys.F9 Then
                Me.grd.MoveLast()
            End If
            If e.KeyCode = Keys.F3 Then
                Me.txtBardCodeScan.Focus()
            End If
            If e.KeyCode = Keys.F1 Then
                Code = 0
                If txtMobile.Focused = False AndAlso txtCustomer.Focused = False Then
                    frmItemSearch.formName = "FrmPOSEntry"
                    frmItemSearch.ShowDialog()
                    Me.grd.MoveLast()
                    SalesStartTime = Date.Now.ToString("yyyy-M-d h:mm:ss tt")
                End If
            End If
            If e.KeyCode = Keys.F4 Then
                If BtnSave.Enabled = True Then
                    BtnSave_Click(Nothing, Nothing)
                End If
            End If
            If e.KeyCode = Keys.F5 Then
                BtnRefresh_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.F6 Then
                frmWalkInCustomerHistory.ShowDialog()
            End If
            If e.KeyCode = Keys.F7 Then
                frmSearchWalkInCustomer.ShowDialog()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbCustomer_Leave(sender As Object, e As EventArgs)
        Try
            'Me.txtMobile.Text = Val(Me.cmbCustomer.ActiveRow.Cells("Mobile").Value.ToString)
            'Me.txtCreditLimit.Text = Val(Me.cmbCustomer.ActiveRow.Cells("Limit").Value.ToString)
            'Me.txtBalance.Text = GetCurrentBalance(Me.cmbCustomer.Value)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub frmPOSEntry_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            GetSecurityRights()
            IsFormLoaded = True
            FillCombos("POS")
            If Not getConfigValueByType("Company-Based-Prefix").ToString = "Error" Then
                CompanyBasePrefix = Convert.ToBoolean(getConfigValueByType("Company-Based-Prefix").ToString)
            End If
            If Not getConfigValueByType("LoadAllItemsInSales").ToString = "Error" Then
                flgLoadAllItems = getConfigValueByType("LoadAllItemsInSales")
            End If
            If Not getConfigValueByType("ExcludeTaxPrice").ToString = "Error" Then
                flgExcludeTaxPrice = Convert.ToBoolean(getConfigValueByType("ExcludeTaxPrice").ToString)
            End If
            If Not getConfigValueByType("flgMargeItem").ToString = "Error" Then
                flgMargeItem = getConfigValueByType("flgMargeItem")
            Else
                flgMargeItem = False
            End If
            FillCombos("SM")
            FillCombos("PM")
            FillCombos("BM")
            FillCombos("Customer")
            FillCombos("Bank")
            FillCombos("CreditCard")
            GetAllRecords()
            ReSetControls()
            InitializeGrid()
            DisplayTempDetail()
            Me.txtBardCodeScan.Focus()
            CtrlGrdBar2_Load(Nothing, Nothing)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub EditRecord()
        Try
            If Me.grd.RecordCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub
            End If
            If BtnSave.Text = "&Save" Then
                BtnSave.Text = "&Update"
            End If
            If frmPOSHistory.RdoHold.Checked = True Then
                HoldStartTime = frmPOSHistory.grd.CurrentRow.Cells("HoldStartTime").Value
                HoldEndTime = Date.Now.ToString("yyyy-M-d h:mm:ss tt")
            Else
                VoucherId = Val(frmPOSHistory.grd.CurrentRow.Cells("VoucherId").Value.ToString)
                Voucher_No = frmPOSHistory.grd.CurrentRow.Cells("VoucherNo").Value.ToString
                VoucherDate = frmPOSHistory.grd.CurrentRow.Cells("VoucherDate").Value
            End If
            SalesStartTime = frmPOSHistory.grd.CurrentRow.Cells("SalesStartTime").Value
            SalesId = Val(frmPOSHistory.grd.CurrentRow.Cells("SalesId").Value.ToString)
            Code = Val(frmPOSHistory.grd.CurrentRow.Cells("CustomerCode").Value.ToString)
            txtCustomer.Text = frmPOSHistory.grd.CurrentRow.Cells("CustomerName").Value.ToString
            txtDocNo.Text = frmPOSHistory.grd.CurrentRow.Cells("SalesNo").Value.ToString
            cmbSalesPerson.SelectedValue = Val(frmPOSHistory.grd.CurrentRow.Cells("EmployeeCode").Value.ToString)
            cmbPackingMan.SelectedValue = Val(frmPOSHistory.grd.CurrentRow.Cells("PackingManId").Value.ToString)
            cmbBillMaker.SelectedValue = Val(frmPOSHistory.grd.CurrentRow.Cells("BillMakerId").Value.ToString)
            '' Changed Remarks to Remarks1 againt TASK TFS4548 ON 19-09-2018
            txtRemarks.Text = frmPOSHistory.grd.CurrentRow.Cells("Remarks1").Value.ToString
            ''END TASK TFS4548
            dtpPOSDate.Value = CType(frmPOSHistory.grd.CurrentRow.Cells("SalesDate").Value, Date)
            txtMobile.Text = frmPOSHistory.grd.CurrentRow.Cells("MobileNo").Value.ToString
            txtCNIC.Text = frmPOSHistory.grd.CurrentRow.Cells("CNIC").Value.ToString
            cmbPayMode.Text = frmPOSHistory.grd.CurrentRow.Cells("InvoiceType").Value.ToString
            txtCash.Text = Val(frmPOSHistory.grd.CurrentRow.Cells("CashPaid").Value.ToString)
            txtDiscount.Text = Val(frmPOSHistory.grd.CurrentRow.Cells("Adj_Flag").Value.ToString)
            txtDisPercentage.Text = Val(frmPOSHistory.grd.CurrentRow.Cells("Adj_Percentage").Value.ToString)
            DisplayDetail(Val(frmPOSHistory.grd.CurrentRow.Cells("SalesId").Value.ToString))
            txtTotal.Text = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(grdDetail.NetAmount), Janus.Windows.GridEX.AggregateFunction.Sum))
            txtCash.Text = txtTotal.Text
            GetSecurityRights()
            VoucherDetail(txtDocNo.Text)
            'Ali Faisal : TFS4415 : Configuration based POS revision is require
            Dim RevisionNumber As Integer = Me.CheckRevisionNumber(SalesId)
            If RevisionNumber > 0 Then
                IsRevisionRestrictedFirstTime = True
                Me.FillRevisionCombo(SalesId)
                Me.cmbRevisionNumber.Visible = False
                Me.lblRev.Visible = False
                Me.txtDocNo.Size = New System.Drawing.Size(143, 23)
                Me.lnkLblRevisions.Visible = True
                Me.lnkLblRevisions.Text = "Rev (" & RevisionNumber & ")"
            Else
                Me.cmbRevisionNumber.Visible = False
                Me.lblRev.Visible = False
                Me.lnkLblRevisions.Visible = False
                Me.txtDocNo.Size = New System.Drawing.Size(222, 23)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Function GetDocumentNo() As String
        Try
            If getConfigValueByType("VoucherNo").ToString = "Yearly" Then
                If CompanyBasePrefix = True AndAlso GetPrefix(IIf(CID = -1, 0, CID)).Length > 0 Then
                    Return GetSerialNo("" & GetPrefix(CID) & "" & "-", "SalesMasterTable", "SalesNo")
                Else
                    Return GetSerialNo(Title & CID & "-" + Microsoft.VisualBasic.Right(Me.dtpChequeDate1.Value.Year, 2) + "-", "SalesMasterTable", "SalesNo")
                End If
            ElseIf getConfigValueByType("VoucherNo").ToString = "Monthly" Then
                Return GetNextDocNo(Title & CID & "-" & Format(Me.dtpChequeDate1.Value, "yy") & Me.dtpChequeDate1.Value.Month.ToString("00"), 4, "SalesMasterTable", "SalesNo")
            Else
                Return GetNextDocNo(Title & CID, 6, "SalesMasterTable", "SalesNo")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Try
            ''Start TFS4393 : Deleting temporary Record of sales from tempSalesDetailtable if exist any against this LoginUser 
            DeleteTempRecord()
            ''End TFS4393
            Me.Close()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub BtnNew_Click(sender As Object, e As EventArgs) Handles BtnNew.Click
        Try
            If Me.grd.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) Then Exit Sub
            End If
            ''Start TFS4393 : Deleting temporary Record of sales from tempSalesDetailtable if exist any against this LoginUser 
            DeleteTempRecord()
            ''End TFS4393
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtDisPercentage_TextChanged(sender As Object, e As EventArgs) Handles txtDisPercentage.TextChanged
        Try
            If txtDisPercentage.Text <> "" Then
                txtDiscount.Text = Val((Val(txtTotal.Text) * Val(txtDisPercentage.Text)) / 100)
                txtNetTotal.Text = Val(txtTotal.Text) - Val(txtDiscount.Text)
            Else
                txtDiscount.Text = "0"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtDiscount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDiscount.KeyPress, txtDisPercentage.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtDiscount_TextChanged(sender As Object, e As EventArgs) Handles txtDiscount.TextChanged
        Try
            If txtDiscount.Text <> "" Then
                txtDisPercentage.Text = Val((Val(txtDiscount.Text) / Val(txtTotal.Text)) * 100)
                txtNetTotal.Text = Val(txtTotal.Text) - Val(txtDiscount.Text)
                txtCash.Text = Val(txtNetTotal.Text)
            End If
            If txtDisPercentage.Text = "NaN" Then
                txtDisPercentage.Text = "0"
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    'Private Sub cmbPOS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbPOS.SelectedIndexChanged
    '    Try
    '        If Me.IsFormLoaded = True Then
    '            ReSetControls()
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    'Public Sub GetInfo()
    '    Try
    '        Dim str As String = "SELECT CompanyId, LocationId, CostCenterId, CashAccountId, BankAccountId, DeliveryOption FROM tblPOSConfiguration where POSId = " & Me.cmbPOS.SelectedValue & ""
    '        Dim dt As DataTable
    '        dt = GetDataTable(str)
    '        If dt IsNot Nothing Then
    '            CID = dt.Rows(0).Item("CompanyId")
    '            LID = dt.Rows(0).Item("LocationId")
    '            CCID = dt.Rows(0).Item("CostCenterId")
    '            CAID = dt.Rows(0).Item("CashAccountId")
    '            BAID = dt.Rows(0).Item("BankAccountId")
    '            DevOption = dt.Rows(0).Item("DeliveryOption")
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Public Sub GetDetailInfo()
    '    Try
    '        Dim str As String = "SELECT CreditCardId, MachineTitle, BankAccountId from tblCreditCardAccount where POSTitle = '" & cmbPOS.Text & "' "
    '        Dim dt As DataTable
    '        dt = GetDataTable(str)
    '        If dt IsNot Nothing Then
    '            CreditId = dt.Rows(0).Item("CreditCardId")
    '            MachineTitle = dt.Rows(0).Item("MachineTitle")
    '            BankId = dt.Rows(0).Item("BankAccountId")
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Try
            If IsFormValidate() = True Then
                If BtnSave.Text = "&Save" Then
                    If Save() = True Then
                        ''Start TFS4393 : Deleting temporary Record of sales from tempSalesDetailtable if exist any against this LoginUser 
                        DeleteTempRecord()
                        ''End TFS4393
                        If chkDirectPrint.Checked = True Then
                            Print()
                            PrintId = 0
                            SalesId = 0
                        End If
                        If DevOption = "True" Then
                            DeliveryInformation.ShowDialog()
                        End If
                        msg_Information("Record has been saved successfully.")
                    Else : Exit Sub
                    End If
                Else
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update1() = True Then
                        If chkDirectPrint.Checked = True Then
                            Print()
                            PrintId = 0
                            SalesId = 0
                        End If
                        If DevOption = "True" Then
                            DeliveryInformation.ShowDialog()
                        End If
                        msg_Information("Record has been Updated successfully.")
                    Else : Exit Sub
                    End If
                End If
            End If
            If Not Me.cmbPayMode.Items.Contains("Credit") Then
                Me.cmbPayMode.Items().Insert(1, "Credit")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Function IsFormValidate() As Boolean
        Try
            If Not Me.grd.RecordCount > 0 Then
                msg_Error(str_ErrorNoRecordFound)
                txtCash.Focus() : IsFormValidate = False : Exit Function
            Else
                IsFormValidate = True
            End If

            For Each r As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                If r.Cells("Qty").Value <= 0 Then
                    msg_Error("Qty must be greate than 0")
                    Return False
                End If
                If r.Cells("Rate").Value <= 0 Then
                    msg_Error("Price must be greate than 0")
                    Return False
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub txtTax_TextChanged(sender As Object, e As EventArgs) Handles txtTax.TextChanged, txtChequeNo.TextChanged
        Try
            'If Val(txtTax.Text) >= 0 Then
            '    txtNetTotal.Text = Val(txtNetTotal.Text) + Val(txtTax.Text)
            'End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Function InitializeGrid()

        Dim DummryQry As String = ItemQry & "-1"
        GridDT = GetDataTable(DummryQry)
        grd.DataSource = GridDT
    End Function
    Private Function RecalculateTotals()
        'Return the running total
        Dim RunningTotal As Double = 0.0
        txtTotal.Text = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(grdDetail.NetAmount), Janus.Windows.GridEX.AggregateFunction.Sum))
        Return RunningTotal
    End Function
    Public Function AddItemInGrid(ByVal ItemID As Integer, ByVal ItemQty As Double, ByVal PackName As String, ByVal PackQty As Double, ByVal Rate As Double, ByVal DiscountId As Integer, ByVal DiscFactor As Double)
        Dim Qry As String
        Dim dt As DataTable
        Try
            'Variable [Qry] select item
            Qry = ItemQry & ItemID
            dt = GetDataTable(Qry)
            If dt.Rows.Count > 0 Then
                Dim dtGrid As DataTable = CType(Me.grd.DataSource, DataTable)
                Dim NewDataRow As DataRow
                'NewDataRow = GridDT.NewRow
                NewDataRow = dtGrid.NewRow
                Dim r As DataRow
                r = dt.Rows(0)
                r("Qty") = ItemQty
                r("PackSize") = PackName
                r("PackQty") = PackQty
                r("PDP") = Rate
                'r("DiscountId") = 1
                ''TASK TFS4070
                If DiscountId > 0 Then
                    r("DiscountId") = DiscountId
                Else
                    r("DiscountId") = 1
                End If
                If DiscountPer > 0 Then
                    r("DiscFactor") = DiscountPer
                Else
                    r("DiscFactor") = DiscFactor
                End If
                ''END TASK TFS4070

                NewDataRow.ItemArray = r.ItemArray
                dtGrid.Rows.Add(NewDataRow)
                dtGrid.Columns("DiscValue").Expression = "IIF(DiscountId= 1,((IsNull(PDP,0)*IsNull(Discfactor,0))/100), IsNull(DiscFactor,0))"
                dtGrid.Columns("Rate").Expression = "IIF(DiscountId= 1, IsNull(PDP,0)-((IsNull(PDP,0)*IsNull(Discfactor,0))/100), IsNull(PDP,0)-IsNull(Discfactor,0))"
                dtGrid.Columns("TotalQty").Expression = "IIF(PackQty > 0, IsNull(Qty, 0) * IsNull(PackQty, 0), Qty)"
                dtGrid.Columns("TotalAmount").Expression = "isnull(TotalQty,0) * isnull(Rate,0)"
                dtGrid.Columns("NetAmount").Expression = "IIF(IsNull(rate,0)=0,0,(IsNull(TotalAmount, 0)) + ((IsNull(TotalAmount, 0)) * (IsNull(Tax,0)) / 100))"
                'GridDT.Columns("DiscValue").Expression = "IIF(DiscountId= 1,((IsNull(PDP,0)*IsNull(Discfactor,0))/100), IsNull(DiscFactor,0))"
                'GridDT.Columns("Rate").Expression = "IIF(DiscountId= 1, IsNull(PDP,0)-((IsNull(PDP,0)*IsNull(Discfactor,0))/100), IsNull(PDP,0)-IsNull(Discfactor,0))"
                'GridDT.Columns("TotalQty").Expression = "IIF(PackQty > 0, IsNull(Qty, 0) * IsNull(PackQty, 0), Qty)"
                'GridDT.Columns("TotalAmount").Expression = "isnull(TotalQty,0) * isnull(Rate,0)"
                'GridDT.Columns("NetAmount").Expression = "IIF(IsNull(rate,0)=0,0,(IsNull(TotalAmount, 0)) + ((IsNull(TotalAmount, 0)) * (IsNull(Tax,0)) / 100))"
                lblItemName.Text = dt.Rows(0).Item("ArticleDescription").ToString  ''TFS4738 
                lblItemPrice.Text = Rate ''TFS4738 
                lblItemPackQty.Text = PackQty ''TFS4738 
                grd.MoveLast() ''TFS4733 : When item is scanned then scroll should move to the last on that item.
            Else
                ShowErrorMessage("No rows fetched ..........")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
        txtTotal.Text = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(grdDetail.NetAmount), Janus.Windows.GridEX.AggregateFunction.Sum))
        txtTax.Text = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(grdDetail.Tax), Janus.Windows.GridEX.AggregateFunction.Sum))
        txtCash.Text = txtTotal.Text
        ''Start TFS4393 : Deleting temporary Record of sales from tempSalesDetailtable if exist any against this LoginUser n then Saving the new data
        DeleteTempRecord()
        SaveTempRecord()
        ''End TFS4393
        Return True
    End Function
    Private Function SaveTempRecord() As Boolean
        Try
            Dim trans As OleDbTransaction
            Dim objCommand As New OleDbCommand
            Dim objCon As OleDbConnection
            Dim i As Integer
            objCon = Con
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Open()
            objCommand.Connection = objCon
            If BtnSave.Text = "&Save" Then
                If Me.grd.RowCount > 0 Then
                    Try
                        trans = objCon.BeginTransaction
                        objCommand.CommandType = CommandType.Text
                        objCommand.Transaction = trans
                        For i = 0 To grd.GetRows.Length - 1

                            objCommand.CommandText = "Insert into tempSalesDetailTable (SalesId, ArticleDefId,ArticleSize, Sz1, Sz7, Qty, LocationId,PostDiscountPrice,DiscountId,DiscountFactor,DiscountValue,Price, PurchasePrice, PackPrice, TaxPercent,CurrencyAmount,LoginUserId,MachineName,DocNo) values( " _
                                              & " " & SalesId & " ," & Val(grd.GetRows(i).Cells(grdDetail.ArticleId).Value) & ", '" & Me.grd.GetRows(i).Cells(grdDetail.PackSize).Value.ToString & "', " & Val(grd.GetRows(i).Cells(grdDetail.PackQty).Value) & " ," & Val(grd.GetRows(i).Cells(grdDetail.Qty).Value) & ", " _
                                              & " " & Val(grd.GetRows(i).Cells("TotalQty").Value) & ", " & LID & ", " _
                                              & " " & Val(grd.GetRows(i).Cells("PDP").Value) & ", " & grd.GetRows(i).Cells(grdDetail.DiscountId).Value & "," & Val(Me.grd.GetRows(i).Cells("DiscFactor").Value) & ", " & Val(Me.grd.GetRows(i).Cells(grdDetail.DiscValue).Value) & ", " & Val(grd.GetRows(i).Cells(grdDetail.Rate).Value) & ", " & Val(Me.grd.GetRows(i).Cells(grdDetail.PurchasePrice).Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells(grdDetail.PackRate).Value.ToString) & ", " & IIf(grd.GetRows(i).Cells(grdDetail.Tax).Value.ToString = "", 0, grd.GetRows(i).Cells(grdDetail.Tax).Value) & "," & Val(Me.grd.GetRows(i).Cells("NetAmount").Value.ToString) & "," & LoginUserId & ",'" & Environment.MachineName & "','" & txtDocNo.Text.Substring(0, 3) & "')"
                            objCommand.ExecuteNonQuery()
                        Next
                        trans.Commit()
                    Catch ex As Exception
                        trans.Rollback()
                        SaveTempRecord = False
                        ShowErrorMessage("An error occured while saving temp record" & ex.Message)
                    End Try
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function DeleteTempRecord() As Boolean
        Try
            Dim trans As OleDbTransaction
            Dim objCommand As New OleDbCommand
            Dim objCon As OleDbConnection
            Dim i As Integer
            objCon = Con
            If objCon.State = ConnectionState.Open Then objCon.Close()
            objCon.Open()
            objCommand.Connection = objCon

            Try
                trans = objCon.BeginTransaction
                objCommand.CommandType = CommandType.Text
                objCommand.Transaction = trans

                objCommand.CommandText = "Delete from tempSalesDetailTable where [LoginUserId] = " & LoginUserId & " and MachineName ='" & Environment.MachineName & "' and DocNo='" & txtDocNo.Text.Substring(0, 3) & "' "
                objCommand.ExecuteNonQuery()

                trans.Commit()
            Catch ex As Exception
                trans.Rollback()
                DeleteTempRecord = False
                ShowErrorMessage("An error occured while Deleting temp record" & ex.Message)
            End Try

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    Try
    '        AddItemInGrid(1, 2)
    '        AddItemInGrid(100, 3)
    '        AddItemInGrid(200, 4)
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    'Private Sub grd_CellEdited(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellEdited
    '    Try

    '        Select Case e.Column.Key
    '            Case "Qty"
    '                If Val(grd.GetRow.Cells(grdDetail.Qty).Value) < 0 Then
    '                    ShowErrorMessage("Quantity should be greater than zero")
    '                    grd.CancelCurrentEdit()
    '                End If
    '            Case "Rate"
    '                If Val(grd.GetRow.Cells(grdDetail.Rate).Value) < 100 Then
    '                    ShowErrorMessage("Quantity should be greater than hundred")
    '                    grd.CancelCurrentEdit()
    '                End If
    '        End Select
    '        txtTotal.Text = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(grdDetail.NetAmount), Janus.Windows.GridEX.AggregateFunction.Sum))
    '        txtNetTotal.Text = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(grdDetail.NetAmount), Janus.Windows.GridEX.AggregateFunction.Sum))
    '        txtCash.Text = Val(txtNetTotal.Text)
    '    Catch ex As Exception
    '        msg_Error(ex.Message)
    '    End Try

    'End Sub

    Private Sub grd_CellUpdated(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellUpdated
        Try

            Select Case e.Column.Key
                Case "Qty"
                    If Val(grd.GetRow.Cells(grdDetail.Qty).Value) < 0 Then
                        ShowErrorMessage("Quantity should be greater than zero")
                        grd.CancelCurrentEdit()
                    End If
                Case "Rate"
                    If Val(grd.GetRow.Cells(grdDetail.Rate).Value) < 0 Then
                        ShowErrorMessage("Rate should be greater than Zero")
                        grd.CancelCurrentEdit()
                    End If
            End Select
            grd.UpdateData()
            ''Start TFS4393 : Deleting temporary Record of sales from tempSalesDetailtable if exist any against this LoginUser n then Saving the new updated data
            DeleteTempRecord()
            SaveTempRecord()
            ''End TFS4393
            txtTotal.Text = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(grdDetail.NetAmount), Janus.Windows.GridEX.AggregateFunction.Sum))
            txtNetTotal.Text = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(grdDetail.NetAmount), Janus.Windows.GridEX.AggregateFunction.Sum))
            txtCash.Text = Val(txtNetTotal.Text)
        Catch ex As Exception
            msg_Error(ex.Message)
        End Try
    End Sub

    Private Sub grd_ColumnButtonClick(sender As Object, e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            If e.Column.Key = "Delete" Then
                Me.grd.GetRow.Delete()
                grd.UpdateData()
                ''Start TFS4393 : Deleting temporary Record of sales from tempSalesDetailtable if exist any against this LoginUser n then Saving the new updated data
                DeleteTempRecord()
                SaveTempRecord()
                ''End TFS4393
                'Ali Faisal : Total invoice value is not changing on removing item.
                txtTotal.Text = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(grdDetail.NetAmount), Janus.Windows.GridEX.AggregateFunction.Sum))
                txtNetTotal.Text = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(grdDetail.NetAmount), Janus.Windows.GridEX.AggregateFunction.Sum))
                txtCash.Text = Val(txtNetTotal.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbPayMode_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbPayMode.SelectedValueChanged, cmbCCAccount.SelectedValueChanged
        Try
            If cmbPayMode.Text = "Cash" Then
                txtCash.Text = txtNetTotal.Text
                pnlBank.Visible = False
                pnlCreditCard.Visible = False
            ElseIf cmbPayMode.Text = "Credit" Then
                txtCash.Enabled = False
                pnlBank.Visible = False
                pnlCreditCard.Visible = False
            ElseIf cmbPayMode.Text = "Bank" Then
                pnlBank.Visible = True
                pnlCreditCard.Visible = False
            ElseIf cmbPayMode.Text = "Credit Card" Then
                If cmbCCAccount.SelectedValue > 0 Then
                    CreditCardId = Val(CType(Me.cmbCCAccount.SelectedItem, DataRowView).Item("BankAccountId").ToString)
                End If
                pnlBank.Visible = False
                pnlCreditCard.Visible = True
            ElseIf cmbPayMode.Text = "Mix" Then
                frmMixPayment.ShowDialog()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtTotal_TextChanged(sender As Object, e As EventArgs) Handles txtTotal.TextChanged
        Try
            txtDiscount.Text = (Val(txtTotal.Text) * Val(txtDisPercentage.Text)) / 100
            txtNetTotal.Text = Val(txtTotal.Text) - Val(txtDiscount.Text)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_UpdatingRecord(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles grd.UpdatingRecord
        Try
            txtTotal.Text = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(grdDetail.NetAmount), Janus.Windows.GridEX.AggregateFunction.Sum))
            txtNetTotal.Text = Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(grdDetail.NetAmount), Janus.Windows.GridEX.AggregateFunction.Sum))
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub Print()
        SalesId = PrintId
        Dim IsPreviewSaleInvoice As Boolean = Convert.ToBoolean(getConfigValueByType("PreviewInvoice").ToString)
        Dim newinvoice As Boolean = False
        Dim strCriteria As String = "Nothing"
        Me.Cursor = Cursors.WaitCursor
        Try

            If frmPOSHistory.grd.RowCount = 0 Then Exit Sub
            PrintLog = New SBModel.PrintLogBE
            PrintLog.DocumentNo = frmPOSHistory.grd.GetRow.Cells("SalesNo").Value.ToString
            PrintLog.UserName = LoginUserName
            PrintLog.PrintDateTime = Date.Now
            Call SBDal.PrintLogDAL.PrintLog(PrintLog)
            newinvoice = getConfigValueByType("NewInvoice")
            If newinvoice = True Then
                If SalesId Then
                    str_ReportParam = "@SaleID|" & SalesId
                Else
                    str_ReportParam = "@SaleID|" & frmPOSHistory.grd.CurrentRow.Cells("SalesId").Value
                End If
            Else
                str_ReportParam = String.Empty
                strCriteria = "{SalesDetailTable.SalesId} = " & IIf(SalesId > 0, SalesId, Val(frmPOSHistory.grd.CurrentRow.Cells("SalesId").Value))
            End If
            'ShowReport(IIf(newinvoice = False, "SalesInvoice", "SalesInvoiceNew") & LID, strCriteria, "Nothing", "Nothing", True, , "New", , , , , )

            If IsPreviewSaleInvoice = False Then
                ShowReport(IIf(newinvoice = False, "SalesInvoice", "SalesInvoiceNew") & LID, strCriteria, "Nothing", "Nothing", True, , "New", , , , , )
            Else
                ShowReport(IIf(newinvoice = False, "SalesInvoice", "SalesInvoiceNew") & LID, strCriteria, "Nothing", "Nothing", False, , "New", , , , , )
            End If
            SaveActivityLog("POS", Me.Text, EnumActions.Print, LoginUserId, EnumRecordType.Sales, frmPOSHistory.grd.GetRow.Cells("SalesNo").Value.ToString, True)
            ReSetControls()
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    ''' <summary>
    ''' Ali Faisal : TFS4415 : Configuration based POS revision is require
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RevisionPrint()
        Dim IsPreviewSaleInvoice As Boolean = Convert.ToBoolean(getConfigValueByType("PreviewInvoice").ToString)
        Me.Cursor = Cursors.WaitCursor
        Try
            If frmPOSHistory.grd.RowCount = 0 Then Exit Sub
            If IsPreviewSaleInvoice = False Then
                If Not Me.cmbRevisionNumber.SelectedValue Is Nothing AndAlso Me.cmbRevisionNumber.SelectedValue > 0 Then
                    AddRptParam("@RevisionNumber", Val(Me.cmbRevisionNumber.Text))
                    AddRptParam("@SalesHistoryId", Me.cmbRevisionNumber.SelectedValue)
                    ShowReport("rptSalesHistory", , , , True)
                End If
            Else
                If Not Me.cmbRevisionNumber.SelectedValue Is Nothing AndAlso Me.cmbRevisionNumber.SelectedValue > 0 Then
                    AddRptParam("@RevisionNumber", Val(Me.cmbRevisionNumber.Text))
                    AddRptParam("@SalesHistoryId", Me.cmbRevisionNumber.SelectedValue)
                    ShowReport("rptSalesHistory", , , , False)
                End If
            End If
            SaveActivityLog("POS", Me.Text, EnumActions.Print, LoginUserId, EnumRecordType.Sales, frmPOSHistory.grd.GetRow.Cells("SalesNo").Value.ToString, True)
            ReSetControls()
        Catch ex As Exception
            Throw ex
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub DisplayDetail(ByVal ID As Integer)
        Try
            Dim str As String
            Dim dt As DataTable
            str = "SELECT SalesDetailTable.ArticleDefId AS ArticleId, ArticleDefView.ArticleCode as ArticleCode, ArticleDefView.ArticleDescription as ArticleDescription, ArticleDefView.ArticleColorName as ArticleColorName, ArticleDefView.ArticleSizeName as ArticleSizeName, ArticleDefView.ArticleUnitName as ArticleUnitName,SalesDetailTable.ArticleSize as PackSize, SalesDetailTable.Sz7 AS PackQty, SalesDetailTable.Sz1 AS Qty, SalesDetailTable.Qty AS TotalQty, SalesDetailTable.PostDiscountPrice AS PDP, SalesDetailTable.DiscountId as DiscountId, SalesDetailTable.DiscountFactor AS DiscFactor, SalesDetailTable.DiscountValue AS DiscValue, SalesDetailTable.Price AS Rate, SalesDetailTable.PurchasePrice, SalesDetailTable.PackPrice as PackRate, CONVERT(Decimal(18," & DecimalPointInValue & "), 0) TotalAmount, SalesDetailTable.TaxPercent as Tax, CONVERT(Decimal(18," & DecimalPointInValue & "), 0) NetAmount FROM SalesDetailTable LEFT OUTER JOIN ArticleDefView ON SalesDetailTable.ArticleDefId = ArticleDefView.ArticleId where SalesDetailTable.SalesId =" & ID
            dt = GetDataTable(str)
            dt.Columns("DiscValue").Expression = "IIF(DiscountId= 1,((IsNull(PDP,0)*IsNull(Discfactor,0))/100), IsNull(DiscFactor,0))"
            dt.Columns("Rate").Expression = "IIF(DiscountId= 1, IsNull(PDP,0)-((IsNull(PDP,0)*IsNull(Discfactor,0))/100), IsNull(PDP,0)-IsNull(Discfactor,0))"
            dt.Columns("TotalQty").Expression = "IIF(PackQty > 0, IsNull(Qty, 0) * IsNull(PackQty, 0), Qty)"
            dt.Columns("TotalAmount").Expression = "isnull(TotalQty,0) * isnull(Rate,0)"
            dt.Columns("NetAmount").Expression = "IIF(IsNull(rate,0)=0,0,(IsNull(TotalAmount, 0)) + ((IsNull(TotalAmount, 0)) * (IsNull(Tax,0)) / 100))"
            grd.DataSource = dt
            Me.grd.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub DisplayTempDetail()
        Try
            Dim str As String
            Dim dt As DataTable
            str = " SELECT tempSalesDetailTable.ArticleDefId AS ArticleId, ArticleDefView.ArticleCode as ArticleCode, ArticleDefView.ArticleDescription as ArticleDescription, ArticleDefView.ArticleColorName as ArticleColorName, ArticleDefView.ArticleSizeName as ArticleSizeName, ArticleDefView.ArticleUnitName as ArticleUnitName,tempSalesDetailTable.ArticleSize as PackSize, tempSalesDetailTable.Sz7 AS PackQty, tempSalesDetailTable.Sz1 AS Qty, tempSalesDetailTable.Qty AS TotalQty, tempSalesDetailTable.PostDiscountPrice AS PDP, tempSalesDetailTable.DiscountId as DiscountId, tempSalesDetailTable.DiscountFactor AS DiscFactor, tempSalesDetailTable.DiscountValue AS DiscValue, tempSalesDetailTable.Price AS Rate, tempSalesDetailTable.PurchasePrice, tempSalesDetailTable.PackPrice as PackRate, CONVERT(Decimal(18," & DecimalPointInValue & "), 0) TotalAmount, tempSalesDetailTable.TaxPercent as Tax, CONVERT(Decimal(18," & DecimalPointInValue & "), 0) NetAmount FROM tempSalesDetailTable LEFT OUTER JOIN ArticleDefView ON tempSalesDetailTable.ArticleDefId = ArticleDefView.ArticleId where tempSalesDetailTable.LoginUserId = " & LoginUserId & " and tempSalesDetailTable.MachineName ='" & Environment.MachineName & "' and DocNo='" & txtDocNo.Text.Substring(0, 3) & "' order by tempSaleDetailId "
            dt = GetDataTable(str)
            dt.Columns("DiscValue").Expression = "IIF(DiscountId= 1,((IsNull(PDP,0)*IsNull(Discfactor,0))/100), IsNull(DiscFactor,0))"
            dt.Columns("Rate").Expression = "IIF(DiscountId= 1, IsNull(PDP,0)-((IsNull(PDP,0)*IsNull(Discfactor,0))/100), IsNull(PDP,0)-IsNull(Discfactor,0))"
            dt.Columns("TotalQty").Expression = "IIF(PackQty > 0, IsNull(Qty, 0) * IsNull(PackQty, 0), Qty)"
            dt.Columns("TotalAmount").Expression = "isnull(TotalQty,0) * isnull(Rate,0)"
            dt.Columns("NetAmount").Expression = "IIF(IsNull(rate,0)=0,0,(IsNull(TotalAmount, 0)) + ((IsNull(TotalAmount, 0)) * (IsNull(Tax,0)) / 100))"
            grd.DataSource = dt
            Me.grd.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Function FormValidate() As Boolean
        Try
            If txtCustomer.Text Is Nothing Then
                msg_Error("Please Select Customer")
                txtCustomer.Focus() : FormValidate = False : Exit Function
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub txtMobile_KeyDown(sender As Object, e As KeyEventArgs) Handles txtMobile.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                frmSearchCustomersVendors.ShowDialog()
                If frmSearchCustomersVendors.CustomerCode > 0 Then
                    Me.cmbPayMode.SelectedIndex = 1
                End If
            ElseIf e.KeyCode = Keys.Enter Then
                If Not txtMobile.Text = "" Then
                    Dim str10 As String = txtMobile.Text.Replace("-", "").Replace("+", "").Replace(" ", "").Replace("P", "").Replace("#", "").Replace(".", "").Replace(",", ";")
                    Dim substring As String = str10.Substring(str10.Length - 9, 9)
                    'strPhoneNo = txtMobile.Text.Replace("-", "").Replace("+", "").Replace(" ", "").Replace("P", "").Replace("#", "").Replace(".", "").Replace(",", ";").Replace("92", "0")
                    Dim str As String
                    str = " SELECT dbo.vwCOADetail.coa_detail_id AS Id,detail_title AS Name, detail_code AS Code, account_type AS Type, Contact_Mobile AS Mobile, CityName AS City, Contact_Address AS Address, Isnull(tblCustomer.CridtLimt, 0) as CreditLimit " _
                    & " FROM vwCOADetail LEFT OUTER JOIN tblCustomer ON vwCOADetail.coa_detail_id = tblCustomer.CustomerID where Contact_Mobile LIKE '%" & substring & "'"
                    Dim dt As DataTable
                    dt = GetDataTable(str)
                    If dt.Rows.Count = 0 Then
                        Dim str1 As String
                        str1 = "SELECT SalesMasterTable.CustomerCode, SalesMasterTable.CustomerName, SalesMasterTable.MobileNo, SalesMasterTable.CNIC FROM SalesMasterTable where MobileNo LIKE '%" & substring & "'"
                        Dim dt1 As DataTable
                        dt1 = GetDataTable(str1)
                        If dt1.Rows.Count > 0 Then
                            Code = dt1.Rows(0).Item("CustomerCode")
                            CName = dt1.Rows(0).Item("CustomerName")
                            txtCNIC.Text = dt1.Rows(0).Item("CNIC")
                            'Limit = dt1.Rows(0).Item("CridtLimt")
                        Else
                            Code = "-1"
                            CName = ""
                            Limit = 0
                        End If
                    Else
                        Code = dt.Rows(0).Item("Id")
                        CName = dt.Rows(0).Item("Name")
                        Limit = dt.Rows(0).Item("CreditLimit")
                    End If
                    PreviousBalance = GetCurrentBalance(Code)
                    txtCustomer.Text = CName
                    txtCreditLimit.Text = Limit
                    txtBalance.Text = PreviousBalance
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Public Sub GetCustomerDetail(ByVal Code As Integer, ByVal Name As String, ByVal mobile As String, ByVal Limit As Integer, ByVal Balance As Double)
        Try
            txtCustomer.Text = Name
            txtMobile.Text = mobile
            txtCreditLimit.Text = Limit
            txtBalance.Text = Balance
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub txtCash_TextChanged(sender As Object, e As EventArgs) Handles txtCash.TextChanged
        Try
            If Val(txtBalance.Text) <> 0 Then
                txtPaymentBalance.Text = (Val(txtBalance.Text) + Val(txtNetTotal.Text)) - Val(txtCash.Text)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub BtnRefresh_Click(sender As Object, e As EventArgs) Handles BtnRefresh.Click
        Try
            Dim id As Integer
            id = 0
            id = Me.cmbSalesPerson.SelectedIndex
            FillCombos("SM")
            Me.cmbSalesPerson.SelectedIndex = id
            FillCombos("Customer")
            FillCombos("Bank")
            FillCombos("CreditCard")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtCustomer_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCustomer.KeyDown
        Try
            If e.KeyCode = Keys.F1 Then
                frmSearchCustomersVendors.ShowDialog()
                If frmSearchCustomersVendors.CustomerCode > 0 Then
                    Me.cmbPayMode.SelectedIndex = 1
                End If
            ElseIf e.KeyCode = Keys.Enter Then
                If Not txtCustomer.Text = "" Then
                    Dim str As String
                    str = " SELECT dbo.vwCOADetail.coa_detail_id AS Id,detail_title AS Name, detail_code AS Code, account_type AS Type, Contact_Mobile AS Mobile, CityName AS City, Contact_Address AS Address, Isnull(tblCustomer.CridtLimt, 0) as CreditLimit " _
                    & " FROM vwCOADetail LEFT OUTER JOIN tblCustomer ON vwCOADetail.coa_detail_id = tblCustomer.CustomerID where detail_title LIKE '%" & Me.txtCustomer.Text & "'"
                    Dim dt As DataTable
                    dt = GetDataTable(str)
                    If dt.Rows.Count = 0 Then
                        Dim str1 As String
                        str1 = "SELECT SalesMasterTable.CustomerCode, SalesMasterTable.CustomerName, SalesMasterTable.MobileNo, SalesMasterTable.CNIC FROM SalesMasterTable where SalesMasterTable.CustomerName LIKE '%" & txtCustomer.Text & "'"
                        Dim dt1 As DataTable
                        dt1 = GetDataTable(str1)
                        If dt1.Rows.Count > 0 Then
                            Code = dt1.Rows(0).Item("CustomerCode")
                            CName = dt1.Rows(0).Item("CustomerName")
                            txtCNIC.Text = dt1.Rows(0).Item("CNIC")
                            txtMobile.Text = dt1.Rows(0).Item("MobileNo")
                            Limit = 0
                        Else
                            Code = "-1"
                            CName = ""
                            Limit = 0
                        End If
                    Else
                        Code = dt.Rows(0).Item("Id")
                        CName = dt.Rows(0).Item("Name")
                        Limit = dt.Rows(0).Item("CreditLimit")
                    End If
                    PreviousBalance = GetCurrentBalance(Code)
                    txtCustomer.Text = CName
                    txtCreditLimit.Text = Limit
                    txtBalance.Text = PreviousBalance
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                'Me.grdSaved.SaveLayoutFile(fs)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar1.txtGridTitle.Text = CompanyTitle & Chr(10) & "POS"
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnHold_Click(sender As Object, e As EventArgs) Handles btnHold.Click
        Dim trans As OleDbTransaction
        Try
            If frmSearchCustomersVendors.CustomerCode > 0 Then
                Code = frmSearchCustomersVendors.CustomerCode
            End If
            If frmSearchCustomersVendors.CustomerName = "" Then
                CName = frmSearchCustomersVendors.CustomerName
                Code = Val(getConfigValueByType("DefaultAccountInPlaceCustomer").ToString)
            End If
            Dim blnCheckCurrentStockByItem As Boolean = False
            If Not getConfigValueByType("CheckCurrentStockByItem").ToString = "Error" Then
                blnCheckCurrentStockByItem = Convert.ToBoolean(getConfigValueByType("CheckCurrentStockByItem").ToString)
            End If
            Dim DiscountedPrice As Double = 0
            Me.txtDocNo.Text = GetDocumentNo()
            setVoucherNo = Me.txtDocNo.Text
            Dim i As Integer
            Dim objCommand As New OleDbCommand
            Dim objCon As OleDbConnection
            If Con.State = ConnectionState.Closed Then Con.Open()
            objCommand.Connection = Con
            objCommand.CommandType = CommandType.Text
            objCommand.CommandText = "Insert into SalesMasterTable (LocationId,SalesNo,SalesDate,CustomerCode,Employeecode,SalesQty,SalesAmount, CashPaid, Remarks,UserName,PreviousBalance, Adj_Flag, Adj_Percentage, InvoiceType,MobileNo, CNIC, POSFlag, CustomerName, HoldFlag, SalesStartTime, HoldStartTime, PackingManId, BillMakerId) values( " _
            & CID & ", N'" & txtDocNo.Text & "',N'" & dtpChequeDate1.Value.ToString("yyyy-M-d h:mm:ss tt") & "'," & Code & "," & Me.cmbSalesPerson.SelectedValue & ", " & Val(Me.grd.GetTotal(Me.grd.RootTable.Columns("TotalQty"), Janus.Windows.GridEX.AggregateFunction.Sum)) & "," & Val(txtNetTotal.Text) & ", " & Val(txtCash.Text) & ",N'" & txtRemarks.Text.Replace("'", "''") & "',N'" & LoginUserName & "'," & Val(txtPaymentBalance.Text) & ", " & Val(txtDiscount.Text) & ", " & Val(txtDisPercentage.Text) & ",N'" & Me.cmbPayMode.Text.Replace("'", "''") & "','" & Me.txtMobile.Text.Replace("'", "''") & "','" & Me.txtCNIC.Text.Replace("'", "''") & "'," & 1 & " ,'" & Me.txtCustomer.Text.Replace("'", "''") & "'," & 1 & ",N'" & SalesStartTime.ToString("yyyy-M-d h:mm:ss tt") & "',N'" & Date.Now.ToString("yyyy-M-d h:mm:ss tt") & "', " & Me.cmbPackingMan.SelectedValue & ", " & Me.cmbBillMaker.SelectedValue & " )" _
            & " SELECT @@IDENTITY"
            InvId = objCommand.ExecuteScalar

            For i = 0 To grd.GetRows.Length - 1
                If blnCheckCurrentStockByItem = True Then
                    CheckCurrentStockByItem(Me.grd.GetRows(i).Cells(grdDetail.ArticleId).Value, Val(Me.grd.GetRows(i).Cells("TotalQty").Value.ToString), Me.grd, , trans)
                End If
                Dim CostPrice As Double = 0D
                Dim CrrStock As Double = 0D
                DiscountedPrice = Me.grd.GetRows(i).Cells(grdDetail.DiscValue).Value
                Dim strPriceData() As String = GetRateByItem(Val(Me.grd.GetRows(i).Cells(grdDetail.ArticleId).Value.ToString)).Split(",")
                objCommand.CommandText = "Insert into SalesDetailTable (SalesId, ArticleDefId,ArticleSize, Sz1, Sz7, Qty, LocationId,PostDiscountPrice,DiscountId,DiscountFactor,DiscountValue,Price, PurchasePrice, PackPrice, TaxPercent) values( " _
                                  & " " & "ident_current('SalesMasterTable')" & " ," & Val(grd.GetRows(i).Cells(grdDetail.ArticleId).Value) & ", '" & Me.grd.GetRows(i).Cells(grdDetail.PackSize).Value.ToString & "', " & Val(grd.GetRows(i).Cells(grdDetail.PackQty).Value) & " ," & Val(grd.GetRows(i).Cells(grdDetail.Qty).Value) & ", " _
                                  & " " & Val(grd.GetRows(i).Cells("TotalQty").Value) & ", " & LID & ", " _
                                  & " " & Val(grd.GetRows(i).Cells("PDP").Value) & ", " & grd.GetRows(i).Cells(grdDetail.DiscountId).Value & "," & Val(Me.grd.GetRows(i).Cells("DiscFactor").Value) & ", " & Val(Me.grd.GetRows(i).Cells(grdDetail.DiscValue).Value) & ", " & Val(grd.GetRows(i).Cells(grdDetail.Rate).Value) & ", " & Val(Me.grd.GetRows(i).Cells(grdDetail.PurchasePrice).Value.ToString) & ", " & Val(Me.grd.GetRows(i).Cells(grdDetail.PackRate).Value.ToString) & ", " & IIf(grd.GetRows(i).Cells(grdDetail.Tax).Value.ToString = "", 0, grd.GetRows(i).Cells(grdDetail.Tax).Value) & ")Select @@Identity"
                objCommand.ExecuteNonQuery()
            Next
            msg_Information("Invoice on Hold successfully")
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub



    Private Sub txtQty_KeyDown(sender As Object, e As KeyEventArgs) Handles txtQty.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                AddItemInGrid(ArticleId, txtQty.Text, PackName, txtPackQty.Text, txtRate.Text, DiscountId, DiscFactor)
                If SalesStartTime = Date.MinValue Then
                    SalesStartTime = Date.Now.ToString("yyyy-M-d h:mm:ss tt")
                End If
                ResetBarcode()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtBardCodeScan_KeyDown(sender As Object, e As KeyEventArgs) Handles txtBardCodeScan.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim Barcode As String = Me.txtBardCodeScan.Text
                If Barcode = String.Empty Then
                    ShowErrorMessage("Please Enter a Barcode")
                    Exit Sub
                End If
                Dim str As String
                str = "SELECT ArticleDefView.ArticleId, ArticleDefView.ArticleCode as [Item Code], ArticleDescription AS Item_Name , ArticleTypeName as Type , ArticleGroupName AS Department, PurchasePrice, SalePrice, ArticleColorName As Color ,ArticleSizeName As Size, PackQty as [Pack Qty], 'Open' As PackInfo , StockLevel, IsNull([No Of Attachment],0) as Attachments , StockLevelOpt, StockLevelMax , Active, SortOrder, ArticleBrandName as Brand, ArticleGenderName as Origin, ArticleUnitName as Unit, ArticleDefView.ArticleBARCode,ArticleCompanyName AS Category ,ArticleLpoName AS SubCategory, Remarks, ServiceItem as ServiceItem, ArticlePicture, TradePrice, Freight, ISNULL(MarketReturns, 0) as MarketReturns, ISNULL(GST_Applicable, 0) AS GST_Applicable, ISNULL(FlatRate_Applicable,0) as FlatRate_Applicable,ISNULL(FlatRate, 0) as FlatRate, ISNULL(ItemWeight, 0) AS ItemWeight, HS_Code, ISNULL(LargestPackQty, 0) AS LargestPackQty, IsNull(ArticleDefView.MasterId, 0) AS MasterId, ISNULL(Cost_Price, 0) AS Cost_Price, ISNULL(ArticleBrandId, 0) AS ArticleBrandId, ISNULL(ApplyAdjustmentFuelExp, 0) AS ApplyAdjustmentFuelExp, IsNull(Discount.DiscountId, 0) AS DiscountId, IsNull(Discount.Discount, 0) AS DiscFactor ,ArticleDefView.ArticleBARCodeDisable from ArticleDefView LEFT OUTER JOIN (SELECT IsNull(ArticleId, 0) AS ArticleId, IsNull(DiscountMaster.DiscountType, 0) AS DiscountId, IsNull(DiscountMaster.Discount, 0) AS Discount FROM ItemWiseDiscountDetail AS Detail INNER JOIN ItemWiseDiscountMaster AS DiscountMaster ON Detail.ItemWiseDiscountId = DiscountMaster.ID WHERE Convert(varchar, GETDATE(), 102) BETWEEN Convert(varchar, DiscountMaster.FromDate, 102) AND Convert(varchar, DiscountMaster.ToDate, 102)) AS Discount ON ArticleDefView.ArticleId = Discount.ArticleId LEFT OUTER JOIN (Select Count(*) as [No Of Attachment],DocId From DocumentAttachment WHERE (source = 'frmDefArticle') Group By DocId, Source) Doc_Att on Doc_Att.DocId = ArticleDefView.MasterID Left Outer Join ArticleBarcodeDefTAble on ArticleBarcodeDefTAble.ArticleId = ArticleDefView.ArticleId WHERE Active=1 and (ArticleDefView.ArticleBARCode ='" & txtBardCodeScan.Text & "' or ArticleBarcodeDefTAble.ArticleBARCode ='" & txtBardCodeScan.Text & "')order by ArticleId Desc"
                Dim dt As DataTable = GetDataTable(str)
                If dt.Rows.Count > 0 Then
                    ArticleId = dt.Rows(0).Item("ArticleId")
                    lblItemDescription.Text = dt.Rows(0).Item("Item_Name")
                    lblItemName.Text = dt.Rows(0).Item("Item_Name").ToString ''TFS4738
                    lblItemPrice.Text = dt.Rows(0).Item("SalePrice") ''TFS4738
                    lblItemPackQty.Text = dt.Rows(0).Item("Pack Qty") ''TFS4738
                    txtRate.Text = dt.Rows(0).Item("SalePrice")
                    txtQty.Text = "1"
                    txtPackQty.Text = dt.Rows(0).Item("Pack Qty")
                    txtTotalQty.Text = Val(txtQty.Text) * Val(txtPackQty.Text)
                    If RateVisible = True Then
                        Me.txtRate.Enabled = True
                    Else
                        Me.txtRate.Enabled = False
                    End If
                    DiscountId = dt.Rows(0).Item("DiscountId")
                    DiscFactor = dt.Rows(0).Item("DiscFactor")
                    PackName = "Pack"
                    Me.txtStock.Text = Convert.ToDouble(GetStockById(ArticleId, LID, "Loose"))
                    txtPackQty.Focus()
                    If chkAutoLoad.Checked = True Then
                        txtQty_KeyDown(Nothing, New KeyEventArgs(Keys.Enter))
                    End If
                Else
                    ShowErrorMessage("Could not found an Item against this Barcode")
                    txtCNIC.Focus()
                    txtBardCodeScan.SelectAll()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtQty_TextChanged(sender As Object, e As EventArgs) Handles txtQty.TextChanged, txtPackQty.TextChanged
        Try
            txtTotalQty.Text = Val(txtQty.Text) * Val(txtPackQty.Text)
            If Val(txtQty.Text) = Val(0) AndAlso Val(txtPackQty.Text) = Val(0) Then
                ResetBarcode()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtCustomer_Leave(sender As Object, e As EventArgs) Handles txtCustomer.Leave
        Try
            If frmSearchCustomersVendors.CustomerCode <= 0 AndAlso Me.txtCustomer.Text <> "" Then
                If Me.cmbPayMode.Items().Contains("Credit") Then
                    Me.cmbPayMode.Items.RemoveAt(1)
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtCustomer_TextChanged(sender As Object, e As EventArgs) Handles txtCustomer.TextChanged
        Try
            If txtCustomer.Text = "" Then
                Code = 0
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub VoucherDetail(ByVal SalesNo As String)
        Try
            Dim str As String = String.Empty
            str = "SELECT dbo.tblVoucher.voucher_id, dbo.tblVoucher.location_id, dbo.tblVoucher.voucher_code, dbo.tblVoucher.voucher_type_id, dbo.tblVoucher.voucher_no, " _
                  & " dbo.tblVoucherDetail.credit_amount, tblVoucherDetail.debit_amount, dbo.tblVoucherDetail.CostCenterID, tblVoucher.coa_detail_id, tblVoucher.Cheque_No, tblVoucher.Cheque_Date " _
                  & " FROM  dbo.tblVoucher INNER JOIN " _
                  & " dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id " _
                  & " WHERE (dbo.tblVoucher.voucher_type_id = 3 Or dbo.tblVoucher.voucher_type_id=5) AND (tblVoucherDetail.coa_detail_id=" & Code & ") AND (dbo.tblVoucher.voucher_Code = N'" & SalesNo & "')"
            Dim dt As DataTable = GetDataTable(str)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    VoucherId = dt.Rows(0).ItemArray(0)
                    Voucher_No = dt.Rows(0).ItemArray(4)
                    ExistingVoucherFlg = True
                Else
                    ExistingVoucherFlg = False
                End If
            Else
                ExistingVoucherFlg = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS4415 : Configuration based POS revision is require
    ''' </summary>
    ''' <param name="HistoryId"></param>
    ''' <param name="RevisionNumber"></param>
    ''' <remarks></remarks>
    Public Sub DisplayDetailHistory(ByVal HistoryId As Integer, ByVal RevisionNumber As Integer)
        Dim str As String = ""
        Dim dt As DataTable
        Try
            str = "SELECT Recv_D.ArticleDefId AS ArticleId, Article.ArticleCode as ArticleCode, Article.ArticleDescription AS ArticleDescription, Article.ArticleColorName AS ArticleColorName, Article.ArticleSizeName AS ArticleSizeName, Recv_D.ArticleSize AS ArticleUnitName, Recv_D.ArticleSize as PackSize, Recv_D.Sz7 AS PackQty, Convert(Decimal(18, " & DecimalPointInQty & "), Recv_D.Sz1, 1) AS Qty, Recv_D.Qty AS TotalQty, " _
                    & " IsNull(Recv_D.PostDiscountPrice, 0) As PDP, IsNull(Recv_D.DiscountId, 0) As DiscountId, IsNull(Recv_D.DiscountFactor, 0) As DiscFactor, IsNull(Recv_D.DiscountValue, 0) As DiscValue,  " _
                    & " Recv_D.Price as Rate, Recv_D.PurchasePrice, Recv_D.PackPrice as PackRate, CONVERT(Decimal(18," & DecimalPointInValue & "), 0) TotalAmount, Recv_D.TaxPercent as Tax, CONVERT(Decimal(18," & DecimalPointInValue & "), 0) NetAmount " _
                    & " FROM SalesDetailHistory Recv_D INNER JOIN SalesHistory ON Recv_D.SaleHistoryId = SalesHistory.SalesHistoryId INNER JOIN " _
                    & " ArticleDefView Article ON Recv_D.ArticleDefId = Article.ArticleId inner JOIN " _
                    & " tblDefLocation ON Recv_D.LocationId = tblDefLocation.Location_id  " _
                    & " LEFT OUTER JOIN SalesCertificateTable CR On CR.SalesDetailId = Recv_D.SaleDetailId Left Outer Join ArticleLpoDefTable ON Article.ArticleLPOId = ArticleLpoDefTable.ArticleLpoId" _
                    & " Where Recv_D.SaleHistoryId =" & HistoryId & " AND SalesHistory.RevisionNumber = " & RevisionNumber & " ORDER BY Recv_D.SaleDetailId Asc"
            dt = GetDataTable(str)
            dt.Columns("DiscValue").Expression = "IIF(DiscountId= 1,((IsNull(PDP,0)*IsNull(Discfactor,0))/100), IsNull(DiscFactor,0))"
            dt.Columns("Rate").Expression = "IIF(DiscountId= 1, IsNull(PDP,0)-((IsNull(PDP,0)*IsNull(Discfactor,0))/100), IsNull(PDP,0)-IsNull(Discfactor,0))"
            dt.Columns("TotalQty").Expression = "IIF(PackQty > 0, IsNull(Qty, 0) * IsNull(PackQty, 0), Qty)"
            dt.Columns("TotalAmount").Expression = "isnull(TotalQty,0) * isnull(Rate,0)"
            dt.Columns("NetAmount").Expression = "IIF(IsNull(rate,0)=0,0,(IsNull(TotalAmount, 0)) + ((IsNull(TotalAmount, 0)) * (IsNull(Tax,0)) / 100))"
            grd.DataSource = dt
            Me.grd.RetrieveStructure()
            ApplyGridSettings()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS4415 : Configuration based POS revision is require
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub lnkLblRevisions_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lnkLblRevisions.LinkClicked
        Try
            If lblRev.Visible = False AndAlso cmbRevisionNumber.Visible = False Then
                Me.lblRev.Visible = True
                Me.cmbRevisionNumber.Visible = True
                lnkLblRevisions.Visible = False
                Me.btnPrint.Visible = True
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS4415 : Configuration based POS revision is require
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbRevisionNumber_SelectedValueChanged(sender As Object, e As EventArgs) Handles cmbRevisionNumber.SelectedValueChanged
        Try
            If cmbRevisionNumber.SelectedValue > 0 AndAlso IsRevisionRestrictedFirstTime = False Then
                DisplayDetailHistory(cmbRevisionNumber.SelectedValue, Val(cmbRevisionNumber.Text))
            End If
            IsRevisionRestrictedFirstTime = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Ali Faisal : TFS4415 : Configuration based POS revision is require
    ''' </summary>
    ''' <param name="SalesId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CheckRevisionNumber(ByVal SalesId As Integer) As Integer
        Dim str As String = String.Empty
        Dim dt As New DataTable
        Try
            str = "Select Distinct RevisionNumber, RevisionNumber FROM SalesHistory Where SalesId =" & SalesId & " ORDER BY 1 DESC"
            dt = GetDataTable(str)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                Return Val(dt.Rows.Item(0).Item(0).ToString)
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : TFS4415 : Configuration based POS revision is require
    ''' </summary>
    ''' <param name="SalesId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function FillRevisionCombo(ByVal SalesId As Integer) As Integer
        Dim str As String = String.Empty
        Dim dt As New DataTable
        Try
            str = "Select SalesHistoryId, RevisionNumber FROM SalesHistory Where SalesId =" & SalesId & " ORDER BY 1 DESC"
            FillDropDown(Me.cmbRevisionNumber, str, False)
            dt = GetDataTable(str)
            dt.AcceptChanges()
            If dt.Rows.Count > 0 Then
                Return dt.Rows.Item(0).Item(0)
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Ali Faisal : TFS4415 : Configuration based POS revision is require
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(sender As Object, e As EventArgs) Handles btnPrint.Click
        Try
            RevisionPrint()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar2_Load(sender As Object, e As EventArgs) Handles CtrlGrdBar2.Load
        Try
            If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite, IO.FileShare.ReadWrite)
                'Me.grd.SaveLayoutFile(fs)
                Me.grd.LoadLayoutFile(fs)
                fs.Close()
                fs.Dispose()
            End If
            Me.CtrlGrdBar2.txtGridTitle.Text = CompanyTitle
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class
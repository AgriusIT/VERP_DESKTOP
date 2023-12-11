''19-June-2014 TASK:2697 IMRAN ALI Optional Note  Entry On CMFA Document(Ravi)
''20-June-2014 TASK:2701 IMRAN ALI Expense Entry on CMFA Document(Ravi)
''25-June-2014 TASK:M56 IMRAN ALI Last Opex Sale Percentage
''25-June-2014 TASK:2703 IMRAN ALI Enhancement In CMFA (RAVI)
''02-Jul-2014 TASK:2710 IMRAN ALI CMFA Document Delete In CMFA (Ravi)
''07-Jul-2014 TASK:2723 IMRAN ALI Add Column Comments In CMFA Detail (Ravi)
''11-Jul-2014 Task:2734 IMRAN ALI Ehancement CMFA Document
''16-Jul-2014 TASK:2744 Imran Ali Problem Facing Record not in grid and cmfa Document Attachment On CMFA Document (Ravi)
''13-Aug-2014 Task:2783 Imran Ali  Frequently CMFA on Home Page (Ravi)
''03-Sep-2014 Task:2824 Imran Ali Apply all rights of admin on CMFA Document
''11-Sep-2014 Task:2836 Imran Ali CMFA Bug fixed and enhancement
''27-Sep-2014 Task:2856 Imran Ali CMFA Detail Report
Imports SBModel
Imports SBDal
Imports SBUtility.Utility
Imports System
Imports System.Data
Imports System.IO
Imports System.Data.OleDb
Imports System.Data.SqlClient
'jhjh
Public Class frmCmfa
    Implements IGeneral
    Dim DocId As Integer = 0I
    Dim IsFormOpended As Boolean = False
    Dim CMFA As CmfaBE
    Dim blnReturnStatus As Boolean = False 'Task:2703 TODO Return CMFA
    Dim IsDirectorApprovedRights As Boolean = False
    Dim CheckedByRights As Boolean = False
    Dim blnModifyDocument As Boolean = False
    Dim CheckedStatus As Boolean = False
    Dim ApprovedStatus As Boolean = False
    Dim SaveRecordStatus As Boolean = False

    Enum Customer
        Id
        Name
        State
        City
        Territory
        ExpiryDate
        Discount
        Other_Exp
        Fuel
        CNG
        TransitInsurance
        Credit_Limit
        Type
        Email
        PhoneNo
        SubSubTitle
    End Enum
    Enum enmGrdDetail
        LocationId
        ArticleDefId
        Code
        Item
        Size
        Color
        UOM
        Unit
        PackQty
        Qty
        Price
        Total
        Current_Price
        VendorId
        PackDesc
        InvoicePrice
        Comments
        DeleteButton
    End Enum
    'Task:2701 Added enum CMFA Expense Detail Grid
    Enum enmCMFAExpVoucher
        ID
        DocId
        coa_detail_id
        detail_code
        detail_title
        Amount
    End Enum
    Enum enmServices
        ServicesID
        ServicesType
        Tax_Percent
        WHT_Percent
        Opex_Sale_Percent
    End Enum
    'End Task:2701
    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub



    Public Sub ApplyGridSettings(Optional ByVal Condition As String = "") Implements IGeneral.ApplyGridSettings
        Try
            If Condition = String.Empty Then
                For c As Integer = 0 To Me.grd.RootTable.Columns.Count - 1
                    If Me.grd.RootTable.Columns(c).Index <> enmGrdDetail.Qty AndAlso Me.grd.RootTable.Columns(c).Index <> enmGrdDetail.Price AndAlso Me.grd.RootTable.Columns(c).Index <> enmGrdDetail.LocationId AndAlso Me.grd.RootTable.Columns(c).Index <> enmGrdDetail.VendorId AndAlso Me.grd.RootTable.Columns(c).Index <> enmGrdDetail.InvoicePrice AndAlso Me.grd.RootTable.Columns(c).Index <> enmGrdDetail.Comments Then
                        Me.grd.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.NoEdit
                    End If
                Next
                'Task:2701 Setting CMFA Expense Detail Grid
            ElseIf Condition = "CMFAExpVoucher" Then
                For c As Integer = 0 To Me.grdExp.RootTable.Columns.Count - 1
                    If Me.grdExp.RootTable.Columns(c).Index <> enmCMFAExpVoucher.Amount Then
                        Me.grdExp.RootTable.Columns(c).EditType = Janus.Windows.GridEX.EditType.NoEdit
                    End If
                    Me.grdExp.RootTable.Columns(c).FilterEditType = Janus.Windows.GridEX.FilterEditType.SameAsEditType
                Next
                'End Task:2701
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub ApplySecurity(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.ApplySecurity
        Try
            If LoginGroup = "Administrator" Then
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnPrint.Enabled = True
                'Me.grdActualExpense.RootTable.Columns("Actual Exp").Visible = True
                'Me.grdActualExpense.RootTable.Columns("Difference").Visible = True
                CheckedByRights = False
                Me.btnCheckedByManager.Enabled = True
                Me.btnApprovedByDirector.Enabled = True
                IsDirectorApprovedRights = True
                blnReturnStatus = True
                Me.UltraTabPageControl6.Enabled = True
                blnModifyDocument = True
                Exit Sub
            End If
            If getConfigValueByType("NewSecurityRights").ToString = "False" Or getConfigValueByType("NewSecurityRights").ToString = "Error" Then
                If RegisterStatus = EnumRegisterStatus.Expired Then
                    Me.btnSave.Enabled = False
                    Me.btnDelete.Enabled = False
                    Me.btnPrint.Enabled = False
                    Exit Sub
                End If
                Dim dt As DataTable = GetFormRights(EnumForms.frmVendorPayment)
                If Not dt Is Nothing Then
                    If Not dt.Rows.Count = 0 Then
                        If Me.btnSave.Text = "Save" Or Me.btnSave.Text = "&Save" Then
                            Me.btnSave.Enabled = dt.Rows(0).Item("Save_Rights").ToString()
                        Else
                            Me.btnSave.Enabled = dt.Rows(0).Item("Update_Rights").ToString
                        End If
                        Me.btnDelete.Enabled = dt.Rows(0).Item("Delete_Rights").ToString
                        Me.btnPrint.Enabled = dt.Rows(0).Item("Print_Rights").ToString
                        Me.Visible = dt.Rows(0).Item("View_Rights").ToString
                    End If
                End If
                UserPostingRights = GetUserPostingRights(LoginUserId)
                If UserPostingRights = True Then
                    Me.chkApproved.Visible = True
                Else
                    Me.chkApproved.Visible = False
                    Me.chkApproved.Checked = False
                End If
                GetSecurityByPostingUser(UserPostingRights, btnSave, btnDelete)
            Else
                'Me.Visible = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnPrint.Enabled = False
                Me.chkApproved.Visible = False
                CtrlGrdBar1.mGridPrint.Enabled = False
                CtrlGrdBar1.mGridExport.Enabled = False
                'Me.grdActualExpense.RootTable.Columns("Actual Exp").Visible = False
                'Me.grdActualExpense.RootTable.Columns("Difference").Visible = False
                Me.grd.RootTable.Columns("InvoicePrice").Visible = False
                Me.btnApprovedByDirector.Enabled = False
                Me.btnCheckedByManager.Enabled = False
                IsDirectorApprovedRights = False
                CheckedByRights = False
                blnModifyDocument = False
                'For i As Integer = 0 To Rights.Count - 1
                For Each RightsDt As GroupRights In Rights
                    If RightsDt.FormControlName = "View" Then
                        'Me.Visible = True
                    ElseIf RightsDt.FormControlName = "Save" Then
                        If Me.btnSave.Text = "&Save" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Update" Then
                        If Me.btnSave.Text = "&Update" Then btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Delete" Then
                        Me.btnDelete.Enabled = True
                        ElseIf RightsDt.FormControlName = "Print" Then
                            Me.btnPrint.Enabled = True
                            CtrlGrdBar1.mGridPrint.Enabled = True
                        ElseIf RightsDt.FormControlName = "Export" Then
                            CtrlGrdBar1.mGridExport.Enabled = True
                        ElseIf RightsDt.FormControlName = "Checked By Manager" Then
                        If Not Me.btnSave.Text = "&Save" Then Me.btnCheckedByManager.Enabled = True
                            CheckedByRights = True
                        ElseIf RightsDt.FormControlName = "Approved" Then
                        If Not Me.btnSave.Text = "&Save" Then Me.btnApprovedByDirector.Enabled = True
                            IsDirectorApprovedRights = True
                        ElseIf RightsDt.FormControlName = "Modify Document" Then
                        blnModifyDocument = True
                            Me.btnSave.Enabled = True
                    ElseIf RightsDt.FormControlName = "Invoice Price" Then
                        Me.grd.RootTable.Columns("InvoicePrice").Visible = True
                        ElseIf RightsDt.FormControlName = "Return CMFA" Then
                            Me.UltraTabPageControl6.Enabled = True
                        End If
                Next
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function Delete(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Delete
        Try
            If New CmfaDal().Delete(CMFA) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Sub FillCombos(Optional ByVal Condition As String = "") Implements IGeneral.FillCombos
        Try
            Dim str As String = String.Empty
            If Condition = "Item" Then
                str = "SELECT ArticleId as Id, ArticleCode as Code, ArticleDescription as Item, ArticleSizeName as Size, ArticleColorName as Combination, Isnull(SalePrice,0) as Price,  ArticleDefView.SizeRangeID as [Size ID], 0 as Stock, Isnull(PurchasePrice,0) as PurchasePrice, Isnull(SubSubId,0) as AccountId, Isnull(ServiceItem,0) as ServiceItem, ArticleDefView.SortOrder, IsNull(ArticleDefView.MasterId,0) as MasterId, ArticleDefView.ArticleUnitName as [UOM] FROM ArticleDefView where Active=1 AND SalesItem=1"
                'End Task:2388
                If Me.cmbItemType.ActiveRow IsNot Nothing Then
                    If Me.cmbItemType.ActiveRow.Cells(0).Value > 0 Then
                        str += " AND ArticleTypeId=" & Me.cmbItemType.Value & ""
                    End If
                End If
                If ItemSortOrder = True Then
                    str += " ORDER By ArticleDefView.SortOrder " & IIf(ItemAscending = True, "Asc", "Desc") & ""
                ElseIf ItemSortOrderByCode = True Then
                    str += " ORDER By ArticleDefView.ArticleCode " & IIf(ItemAscending = True, "Asc", "Desc") & ""
                ElseIf ItemSortOrderByName = True Then
                    str += " ORDER By ArticleDefView.ArticleDescription " & IIf(ItemAscending = True, "Asc", "Desc") & ""
                Else
                    str += " ORDER By ArticleDefView.SortOrder " & IIf(ItemAscending = True, "Asc", "Desc") & ""
                End If
                'End Task:2452

                FillUltraDropDown(Me.cmbItem, str)
                Me.cmbItem.Rows(0).Activate()
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Size ID").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("Stock").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("PurchasePrice").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("AccountId").Hidden = True '' Add New Column for Sales Account Id
                Me.cmbItem.DisplayLayout.Bands(0).Columns("ServiceItem").Hidden = True '' Task:2388 Servicie Item Column Hidden
                Me.cmbItem.DisplayLayout.Bands(0).Columns("SortOrder").Hidden = True
                Me.cmbItem.DisplayLayout.Bands(0).Columns("MasterId").Hidden = True

            ElseIf Condition = "Customer" Then

                str = "SELECT vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name, tblListState.StateName as State, tblListCity.CityName as City,  " & _
                                    "tblListTerritory.TerritoryName as Territory , tblCustomer.ExpiryDate, tblCustomer.Discper as [Discount] ,tblCustomer.otherexpanses as [Other Expense], tblCustomer.Fuel as Fuel, tblCustomer.CNG as CNG , tblCustomer.Cridtlimt as Limit, dbo.vwCOADetail.account_type as Type, isnull(customertypes,0) as typeid, tblCustomer.Email,tblCustomer.Phone, vwCOADetail.Sub_Sub_Title " & _
                                    "FROM  tblCustomer LEFT OUTER JOIN " & _
                                    "tblListTerritory ON tblCustomer.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                                    "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                                    "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                                    "vwCOADetail ON tblCustomer.AccountId = vwCOADetail.coa_detail_id " & _
                                    "WHERE (vwCOADetail.account_type in( 'Customer')) and  vwCOADetail.coa_detail_id is not  null "
                str += " order by tblCustomer.Sortorder, vwCOADetail.detail_title "
                FillUltraDropDown(cmbCustomer, str)
                Me.cmbCustomer.Rows(0).Activate()
                If cmbCustomer.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.Id).Hidden = True
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.Territory).Hidden = False
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.State).Hidden = True
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.ExpiryDate).Hidden = True
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.Fuel).Hidden = True
                    ' CNG Changes
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.CNG).Hidden = True
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.Other_Exp).Hidden = True
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns("typeid").Hidden = True
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.Discount).Hidden = True
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.Credit_Limit).Hidden = True
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.Type).Hidden = True
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.Name).Width = 300
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.Credit_Limit).Width = 80
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.Discount).Width = 80
                    'Task:2373 Column Formating
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.SubSubTitle).Header.Caption = "Ac Head"
                    Me.cmbCustomer.DisplayLayout.Bands(0).Columns(Customer.SubSubTitle).Width = 120
                    'End Task:2373
                End If

            ElseIf Condition = "Vendor" Then

                str = "SELECT vwCOADetail.coa_detail_id AS Id, vwCOADetail.detail_title as Name, tblListState.StateName as State, tblListCity.CityName as City,  " & _
                                    "tblListTerritory.TerritoryName as Territory ,dbo.vwCOADetail.account_type as Type, tblVendor.Email,tblVendor.Phone, vwCOADetail.Sub_Sub_Title " & _
                                    "FROM  tblVendor LEFT OUTER JOIN " & _
                                    "tblListTerritory ON tblVendor.Territory = tblListTerritory.TerritoryId LEFT OUTER JOIN " & _
                                    "tblListCity ON tblListTerritory.CityId = tblListCity.CityId LEFT OUTER JOIN " & _
                                    "tblListState ON tblListCity.StateId = tblListState.StateId RIGHT OUTER JOIN " & _
                                    "vwCOADetail ON tblVendor.AccountId = vwCOADetail.coa_detail_id " & _
                                    "WHERE (vwCOADetail.account_type in( 'Vendor')) and  vwCOADetail.coa_detail_id is not  null "
                str += " order by  vwCOADetail.detail_title "
                FillUltraDropDown(cmbVendor, str)
                Me.cmbVendor.Rows(0).Activate()
                If cmbVendor.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("Id").Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("Territory").Hidden = False
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("State").Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("Email").Hidden = True
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("Name").Width = 300
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Header.Caption = "Ac Head"
                    Me.cmbVendor.DisplayLayout.Bands(0).Columns("Sub_Sub_Title").Width = 120
                End If
            ElseIf Condition = "Company" Then
                FillDropDown(Me.cmbCompany, "Select CompanyId, CompanyName From CompanyDefTable", False)
            ElseIf Condition = "CostCenter" Then
                FillDropDown(Me.cmbProject, "Select CostCenterId, Name,Code, CostCenterGroup From tblDefCostCenter WHERE Upper(CostCenterGroup)='MEDIUM'")
            ElseIf Condition = "SalesOrder" Then
                FillDropDown(Me.cmbSaleOrder, "Select SalesOrderMasterTable.SalesOrderId, SalesOrderNo + ' ~ ' + Convert(Varchar,SalesOrderDate,102) as SalesOrderNo, VendorId, IsNull(SOrder.Amount,0) as Budget, ISNull(SOrder.SalesTax,0) as SalesTax  From SalesOrderMasterTable LEFT OUTER JOIN (Select SalesOrderId, Amount, SalesTax From(Select SalesOrderId, SUM(ISNull(Qty,0)*ISNULL(Price,0)) as Amount, SUM((ISNULL(SalesTax_Percentage,0)/100)*(ISNull(Qty,0)*ISNULL(Price,0))) as SalesTax From SalesOrderDetailTable Group By SalesOrderId)a WHERE a.Amount <> 0) SOrder On SOrder.SalesOrderId = SalesOrderMasterTable.SalesOrderId WHERE VendorId=" & IIf(Me.cmbCustomer.Value > 0, Me.cmbCustomer.Value, 0) & " AND Status='Open'")
            ElseIf Condition = "Location" Then
                FillDropDown(Me.cmbLocation, "Select Location_Id, Location_Code, Location_Name, Location_Type From tblDefLocation", False)
            ElseIf Condition = "ItemType" Then
                FillUltraDropDown(Me.cmbItemType, "Select ArticleTypeId,ArticleTypeName From ArticleTypeDefTable")
                Me.cmbItemType.Rows(0).Activate()
                If Me.cmbItemType.DisplayLayout.Bands(0).Columns.Count > 0 Then
                    Me.cmbItemType.DisplayLayout.Bands(0).Columns(0).Hidden = True
                    Me.cmbItemType.DisplayLayout.Bands(0).Columns("Price").Hidden = True
                    Me.cmbItemType.DisplayLayout.Bands(0).Columns("Stock").Hidden = True
                    Me.cmbItemType.DisplayLayout.Bands(0).Columns("ArticleTypeName").Width = 150
                End If
            ElseIf Condition = "ArticlePack" Then
                FillDropDown(Me.cmbUnit, "Select ArticlePackId, PackName, PackQty, ArticleMasterId From ArticleDefPackTable WHERE ArticleMasterId=" & IIf(Me.cmbItem.ActiveRow.Cells(0).Value > 0, Me.cmbItem.ActiveRow.Cells("MasterId").Value, 0) & "", False)
            ElseIf Condition = "ExpenseAccount" Then
                FillDropDown(Me.cmbAccounts, "Select coa_detail_id, detail_title,detail_code From vwCOADetail WHERE Account_Type='Expense' AND detail_title <> '' ORDER BY 2 ASC")
            ElseIf Condition = "Services" Then
                FillDropDown(Me.cmbServicesType, "Select ServicesID, ServicesType, IsNull(Tax_Percent,0) as Tax_Percent, IsNull(WHT_Percent,0) as WHT_Percent, IsNull(Opex_Sale_Percent,0) as Opex_Sale_Percent From tblDefServices")
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub FillModel(Optional ByVal Condition As String = "") Implements IGeneral.FillModel
        Try

            CMFA = New CmfaBE
            CMFA.DocId = DocId
            CMFA.docNo = Me.txtDocumentNo.Text
            CMFA.DocDate = Me.dtpDocDate.Value
            CMFA.locationId = cmbCompany.SelectedValue
            CMFA.CustomerCode = Me.cmbCustomer.Value
            CMFA.ProjectId = Me.cmbProject.SelectedValue
            CMFA.POId = Me.cmbSaleOrder.SelectedValue
            CMFA.Remarks = Me.txtActivity.Text
            CMFA.TotalQty = Me.grd.GetTotal(grd.RootTable.Columns(enmGrdDetail.Qty), Janus.Windows.GridEX.AggregateFunction.Sum)
            CMFA.TotalAmount = Me.grd.GetTotal(grd.RootTable.Columns(enmGrdDetail.Total), Janus.Windows.GridEX.AggregateFunction.Sum)
            CMFA.TaxPercent = Val(Me.txtGSTPercentage.Text)
            CMFA.ApproveBudget = Val(Me.txtApproveBudget.Text)
            CMFA.WHTaxPercent = Val(Me.txtWHTaxPercentage.Text)
            ''19-June-2014 TASK:2697 Optional Note Job Completion Date e.g
            CMFA.ExptJobCompDate = IIf(Me.dtpExpctJobCompletionDate.Checked = True, Me.dtpExpctJobCompletionDate.Value, Nothing)
            CMFA.ExptPaymentFromClient = Val(Me.txtExpectPaymentFromClient.Text)
            CMFA.JobStartingTime = IIf(Me.dtpJobStartingDate.Checked = True, Me.dtpExpctJobCompletionDate.Value, Nothing)
            CMFA.TentiveInvoiceDate = IIf(Me.dtpTentiveInvoiceDate.Checked = True, Me.dtpTentiveInvoiceDate.Value, Nothing)
            CMFA.VerificationPeriodAfterCompletionJob = IIf(Me.dtpVerficationCompJob.Checked = True, Me.dtpVerficationCompJob.Value, Nothing) 'Me.txtVerficationPeriodJob.Text
            'End Task:2697
            CMFA.UserName = LoginUserName
            CMFA.Status = True
            CMFA.PONo = Me.txtPONo.Text
            CMFA.EstimateExpense = Val(Me.txtEstimateExpense.Text) 'Task:2703 Set Estimate Expense Value.
            CMFA.ReturnComments = Me.txtReturnComments.Text 'Task:2703 Set ReturnComments Value.
            CMFA.ReturnStatus = blnReturnStatus 'Task:2703 Set ReturnStatus Value.
            CMFA.CMFAType = Me.cmbServicesType.Text
            CMFA.OpexSalePercent = Val(Me.txtOpexSalePercent.Text)
            If IsDirectorApprovedRights = True Then
                CMFA.ApprovedUserId = LoginUserId
            Else
                CMFA.ApprovedUserId = 0
            End If
            CMFA.Approved = Me.chkApproved.Checked
            CMFA.EntryDate = Now
            If Me.grdSaved.RowCount > 0 Then
                CMFA.UserID = IIf(LoginUserId <> Me.grdSaved.GetRow.Cells("UserId").Value, Me.grdSaved.GetRow.Cells("UserId").Value, LoginUserId)
            Else
                CMFA.UserID = LoginUserId
            End If

            CMFA.CheckedByUserID = IIf(CheckedByRights = True, LoginUserId, 0)
            ''27-Sep-2014 Task:2856 Imran Ali CMFA Detail Report
            CMFA.ApprovedUserName = IIf(Me.chkApproved.Checked = True, LoginUserName, String.Empty)
            CMFA.CheckedUserName = IIf(Me.CheckedStatus = True, LoginUserName, String.Empty)
            'End Task:2856
            CMFA.ProjectedExpAmount = Val(Me.txtNetProjectExp.Text) 'Task:2734 Set Net Projected Exp Value.
            CMFA.CMFADetail = New List(Of CMFADetailBE)
            Dim objCMFADetail As CMFADetailBE
            Me.grd.UpdateData()
            For Each objRow As Janus.Windows.GridEX.GridEXRow In Me.grd.GetRows
                objCMFADetail = New CMFADetailBE
                objCMFADetail.LocationId = Val(objRow.Cells(enmGrdDetail.LocationId).Value.ToString)
                objCMFADetail.ArticleDefId = Val(objRow.Cells(enmGrdDetail.ArticleDefId).Value.ToString)
                objCMFADetail.ArticleSize = objRow.Cells(enmGrdDetail.Unit).Value.ToString
                objCMFADetail.Sz1 = Val(objRow.Cells(enmGrdDetail.Qty).Value.ToString)
                objCMFADetail.Sz2 = 0
                objCMFADetail.Sz7 = Val(objRow.Cells(enmGrdDetail.PackQty).Value.ToString)
                objCMFADetail.Qty = Val(objRow.Cells(enmGrdDetail.Qty).Value.ToString) * (IIf(Val(objRow.Cells(enmGrdDetail.PackQty).Value.ToString) = 0, 1, Val(objRow.Cells(enmGrdDetail.PackQty).Value.ToString)))
                objCMFADetail.Price = Val(objRow.Cells(enmGrdDetail.Price).Value.ToString)
                objCMFADetail.Current_Price = Val(objRow.Cells(enmGrdDetail.Current_Price).Value.ToString)
                objCMFADetail.VendorId = Val(objRow.Cells(enmGrdDetail.VendorId).Value.ToString)
                objCMFADetail.PackDesc = objRow.Cells(enmGrdDetail.PackDesc).Value.ToString
                If Me.cmbSaleOrder.SelectedIndex > 0 Then
                    objCMFADetail.POQty = IIf(Val(objRow.Cells(enmGrdDetail.PackQty).Value.ToString) = 0, 1 * Val(objRow.Cells(enmGrdDetail.Qty).Value.ToString), Val(objRow.Cells(enmGrdDetail.PackQty).Value.ToString) * Val(objRow.Cells(enmGrdDetail.Qty).Value.ToString))
                End If
                objCMFADetail.InvoicePrice = Val(objRow.Cells(enmGrdDetail.InvoicePrice).Value.ToString)
                objCMFADetail.Comments = objRow.Cells(enmGrdDetail.Comments).Value.ToString 'Task:2723 Get Comments
                CMFA.CMFADetail.Add(objCMFADetail)
            Next

            'Task:2701 Fill Properties CMFA Expense Detail
            CMFA.CMFAExpVoucher = New List(Of CMFAExpVoucherBE)
            Dim CMFAExp As CMFAExpVoucherBE
            If Not Me.grdExp.RootTable Is Nothing Then
                If Me.grdExp.RowCount > 0 Then
                    For Each objRow As Janus.Windows.GridEX.GridEXRow In Me.grdExp.GetRows
                        If Val(objRow.Cells("Amount").Value.ToString) <> 0 Then
                            CMFAExp = New CMFAExpVoucherBE
                            CMFAExp.ID = 0
                            CMFAExp.DocID = DocId
                            CMFAExp.coa_detail_id = objRow.Cells("coa_detail_id").Value
                            CMFAExp.Amount = objRow.Cells("Amount").Value
                            CMFA.CMFAExpVoucher.Add(CMFAExp)
                        End If
                    Next
                End If
            End If
            'End Task:2701



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetAllRecords(Optional ByVal Condition As String = "") Implements IGeneral.GetAllRecords
        Try
            If Condition = "Master" Then

                Dim str As String = String.Empty
                str = "SELECT CMFA.DocId,CMFA.LocationId,CMFA.ProjectId,COA.coa_detail_id, Isnull(CMFA.POId,0) as POId, CMFA.DocNo, CMFA.DocDate, CMFA.UserName, Comp.CompanyName as Company, COA.detail_title as [Customer], Project.Name as Project, SO.SalesOrderNo as [SO No], CMFA.Remarks, CMFA.InvoiceNo, CMFA.ExptJobCompDate, " _
                      & " CMFA.ExptPaymentFromClient, CMFA.JobStartingTime, CMFA.TentiveInvoiceDate, CMFA.VerificationPeriodAfterCompletionJob, CMFA.Status, IsNull(CMFA.ApprovedBudget,0) as ApprovedBudget, IsNull(CMFA.TaxPercent,0) as TaxPercent, Isnull(CMFA.WHTaxPercent,0) as WHTaxPercent, IsNull(CMFA.ApprovedUserId,0) as ApprovedUserId, IsNull(CMFA.Approved,0) as Approved, ISNULL(CMFA.OPEX_Sale_Percent,0) as OPEX_Sale_Percent, Isnull(CMFA.EstimateExpense,0) as EstimateExpense, CMFA.ReturnComment, ISNULL(CMFA.ReturnStatus,0) as ReturnStatus, CMFA.CMFAType, IsNull(CMFA.UserId,0) as UserId,IsNull(CMFA.CheckedByUserId,0) as CheckedByUserId, IsNull(CMFA.CheckedStatus,0) as CheckedStatus  " _
                      & " FROM dbo.CMFAMasterTable AS CMFA LEFT OUTER JOIN " _
                      & " dbo.tblDefCostCenter AS Project ON CMFA.ProjectId = Project.CostCenterID LEFT OUTER JOIN " _
                      & " dbo.vwCOADetail AS COA ON CMFA.CustomerCode = COA.coa_detail_id LEFT OUTER JOIN " _
                      & " dbo.CompanyDefTable AS Comp ON CMFA.LocationId = Comp.CompanyId LEFT OUTER JOIN " _
                      & " dbo.SalesOrderMasterTable AS SO ON CMFA.POId = SO.SalesOrderId  WHERE  CMFA.DocId <> 0 "
                If Not LoginGroup.ToString = "Administrator" Then ''03-Sep-2014 Task:2824 Imran Ali Apply all rights of admin on CMFA Document
                    If CheckedByRights = True Then
                        str += " AND (CMFA.UserID IN(Select User_Id From tblUser WHERE RefUserID=" & LoginUserId & ") Or CMFA.UserID=" & LoginUserId & " OR CMFA.CheckedByUserId =" & LoginUserId & ")"
                    End If
                    If IsDirectorApprovedRights = True Then
                        str += " AND (CheckedByUserId <> 0 Or CMFA.UserID=" & LoginUserId & " Or CMFA.ApprovedUserId =" & LoginUserId & ")"
                    End If

                    If Not (CheckedByRights = True Or IsDirectorApprovedRights = True) Then
                        str += " AND (CMFA.UserID =" & LoginUserId & ")"
                    End If
                End If 'End Task:2824
                str += " ORDER BY CMFA.DocNo DESC "

                Dim dt As New DataTable
                dt = GetDataTable(str)
                dt.AcceptChanges()

                Me.grdSaved.DataSource = dt
                Me.grdSaved.RetrieveStructure()
                Me.grdSaved.RootTable.Columns("DocId").Visible = False
                Me.grdSaved.RootTable.Columns("locationId").Visible = False
                Me.grdSaved.RootTable.Columns("coa_detail_id").Visible = False
                Me.grdSaved.RootTable.Columns("ProjectId").Visible = False
                Me.grdSaved.RootTable.Columns("POId").Visible = False
                Me.grdSaved.RootTable.Columns("ApprovedUserId").Visible = False
                Me.grdSaved.RootTable.Columns("OPEX_Sale_Percent").Visible = False

                Me.grdSaved.RootTable.Columns("UserId").Visible = False
                Me.grdSaved.RootTable.Columns("CheckedByUserId").Visible = False

                Me.grdSaved.RootTable.Columns("DocDate").FormatString = "dd/MMM/yyyy"
                Me.grdSaved.RootTable.Columns("ExptJobCompDate").FormatString = "dd/MMM/yyyy"
                Me.grdSaved.RootTable.Columns("JobStartingTime").FormatString = "dd/MMM/yyyy"
                Me.grdSaved.RootTable.Columns("TentiveInvoiceDate").FormatString = "dd/MMM/yyyy"
                Me.grdSaved.AutoSizeColumns()
            ElseIf Condition = "Detail" Then
                Dim dt As New DataTable
                dt = New CmfaDal().DetailRecord(DocId)
                dt.Columns("Total").Expression = "IIF(Unit='Pack', ((qty*packQty)*price),(qty*price))"
                dt.AcceptChanges()
                Me.grd.DataSource = dt
                Dim objDt As New DataTable
                objDt = GetDataTable("Select Location_Id, Location_Code From tblDefLocation")
                Dim objDtVendor As New DataTable
                objDtVendor = GetDataTable("Select coa_detail_id,detail_title, detail_code from vwCOADetail where Account_Type='Vendor'")
                Me.grd.RootTable.Columns("LocationId").ValueList.PopulateValueList(objDt.DefaultView, "Location_Id", "Location_Code")
                Me.grd.RootTable.Columns("VendorId").ValueList.PopulateValueList(objDtVendor.DefaultView, "coa_detail_id", "detail_title")
                Me.grd.RootTable.Columns("Total").TotalFormatString = "N" & DecimalPointInValue
                Me.grd.RootTable.Columns("Total").FormatString = "N" & DecimalPointInValue
                Me.grd.RootTable.Columns("Price").FormatString = "N" & DecimalPointInValue
                ApplyGridSettings()
            ElseIf Condition = "CMFAExp" Then
                Dim objDt As New DataTable
                'objDt = GetDataTable("SP_CMFAExp " & DocId & "")
                objDt = GetAcutalExpenseDetail(DocId) 'GetDataTable("SP_CMFAExpense " & DocId & "")
                'objDt.Columns.Add("Difference", GetType(System.Double))
                'objDt.Columns("Difference").Expression = "[Projected Exp]-[Actual Exp]"
                objDt.AcceptChanges()
                Me.grdActualExpense.DataSource = objDt
                Me.grdActualExpense.AutoSizeColumns()
            ElseIf Condition = "SalesOrder" Then
                If Me.cmbSaleOrder.SelectedIndex = -1 Then Exit Sub
                If Me.grd.RowCount > 0 Then
                    Me.grd.DataSource = Nothing
                End If
                Dim dt As New DataTable
                dt = New CmfaDal().SalesOrderDetailRecord(Me.cmbSaleOrder.SelectedValue)
                dt.Columns("Total").Expression = "IIF(Unit='Pack', ((qty*packQty)*price),(qty*price))"
                dt.AcceptChanges()
                Me.grd.DataSource = dt
                Dim objDt As New DataTable
                objDt = GetDataTable("Select Location_Id, Location_Code From tblDefLocation")
                Dim objDtVendor As New DataTable
                objDtVendor = GetDataTable("Select coa_detail_id,detail_title, detail_code from vwCOADetail where Account_Type='Vendor'")
                Me.grd.RootTable.Columns("LocationId").ValueList.PopulateValueList(objDt.DefaultView, "Location_Id", "Location_Code")
                Me.grd.RootTable.Columns("VendorId").ValueList.PopulateValueList(objDtVendor.DefaultView, "coa_detail_id", "detail_title")
                ApplyGridSettings()
            ElseIf Condition = "CMFAExpVoucher" Then
                Dim strSQL As String = String.Empty
                strSQL = " SELECT CMFAExp.ID, CMFAExp.DocId, COA.coa_detail_id, COA.detail_code, COA.detail_title, CMFAExp.Amount " _
                        & "  FROM dbo.vwCOADetail AS COA INNER JOIN (SELECT ID, DocId, coa_detail_id, Amount FROM dbo.CMFAExpenseTable " _
                        & "  WHERE (DocId = " & DocId & ")) AS CMFAExp ON COA.coa_detail_id = CMFAExp.coa_detail_id  "
                Dim dt As New DataTable
                dt = GetDataTable(strSQL)
                Me.grdExp.DataSource = dt
                ApplyGridSettings("CMFAExpVoucher")
            End If
            CtrlGrdBar1_Load(Nothing, Nothing)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function IsValidate(Optional ByVal Mode As SBUtility.Utility.EnumDataMode = SBUtility.Utility.EnumDataMode.Disabled, Optional ByVal Condition As String = "") As Boolean Implements IGeneral.IsValidate
        Try
            If Me.txtDocumentNo.Text = String.Empty Then
                ShowErrorMessage("Document no not valid.")
                Me.txtDocumentNo.Focus()
                Return False
            End If
            If Me.txtActivity.Text = String.Empty Then
                ShowErrorMessage("Please enter activity.")
                Me.txtActivity.Focus()
                Return False
            End If
            If Me.cmbCustomer.Value <= 0 Then
                ShowErrorMessage("Please select customer")
                Me.cmbCustomer.Focus()
                Return False
            End If
            If Me.cmbProject.SelectedIndex <= 0 Then
                ShowErrorMessage("Please select project.")
                Me.cmbProject.Focus()
                Return False
            End If
            If Me.dtpDocDate.Value = Date.MinValue Then
                ShowErrorMessage("Document date not valid.")
                Me.dtpDocDate.Focus()
                Return False
            End If
            If Not Me.grd.RowCount > 0 Then
                ShowErrorMessage("Record not in grid.")
                Me.grd.Focus()
            End If
            If Me.dtpExpctJobCompletionDate.Checked = False Then
                ShowErrorMessage("Please enter Expected job completion date")
                Me.dtpExpctJobCompletionDate.Focus()
                Return False
            End If
            If Me.txtExpectPaymentFromClient.Text = String.Empty Then
                ShowErrorMessage("Please enter credit period")
                Me.txtExpectPaymentFromClient.Focus()
                Return False
            End If
            If Me.dtpJobStartingDate.Checked = False Then
                ShowErrorMessage("Please enter Job starting date.")
                Me.dtpJobStartingDate.Focus()
                Return False
            End If
            If Me.dtpTentiveInvoiceDate.Checked = False Then
                ShowErrorMessage("Please enter Tentive invoice date.")
                Me.dtpTentiveInvoiceDate.Focus()
                Return False
            End If
            If Me.dtpVerficationCompJob.Checked = False Then
                ShowErrorMessage("Please enter Verification period date.")
                Me.dtpVerficationCompJob.Focus()
                Return False
            End If
            If Me.txtOpexSalePercent.Text = String.Empty Then
                ShowErrorMessage("Please enter Opex on sale percentage.")
                Me.txtOpexSalePercent.Focus()
                Return False
            End If
            FillModel()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ReSetControls(Optional ByVal Condition As String = "") Implements IGeneral.ReSetControls
        Try
            Me.btnSave.Text = "&Save"
            Me.btnDelete.Visible = False
            DocId = 0I
            If Not Me.cmbCompany.SelectedIndex = -1 Then Me.cmbCompany.SelectedIndex = Me.cmbCompany.SelectedIndex
            If Not Me.cmbProject.SelectedIndex = -1 Then Me.cmbProject.SelectedIndex = 0
            If Not Me.cmbCustomer.ActiveRow Is Nothing Then Me.cmbCustomer.Rows(0).Activate()
            If Not Me.cmbVendor.ActiveRow Is Nothing Then Me.cmbVendor.Rows(0).Activate()
            If Not Me.cmbItem.ActiveRow Is Nothing Then Me.cmbItem.Rows(0).Activate()
            If Not Me.cmbItemType.ActiveRow Is Nothing Then Me.cmbItemType.Rows(0).Activate()
            If Not Me.cmbLocation.SelectedIndex = -1 Then Me.cmbLocation.SelectedIndex = 0
            If Not Me.cmbAccounts.SelectedIndex = -1 Then Me.cmbAccounts.SelectedIndex = 0
            'Me.txtDocumentNo.Text = GetNextDocNo("CMFA-" & Me.dtpDocDate.Value.ToString("yy") & "", 6, "CMFAMasterTable", "DocNo")
            Me.txtDocumentNo.Text = GetNextDocNo("CMFA-" & Me.dtpDocDate.Value.ToString("yyMM") & "", 6, "CMFAMasterTable", "DocNo")
            Me.dtpDocDate.Value = Date.Now 'Task:2836 Resting Current Date
            Me.txtActivity.Text = String.Empty
            Me.txtApproveBudget.Text = String.Empty
            Me.txtGSTPercentage.Text = String.Empty
            Me.txtAddSalesTax.Text = String.Empty
            Me.txtWHTaxPercentage.Text = String.Empty
            Me.txtWHTax.Text = String.Empty
            Me.txtNetAmount.Text = String.Empty
            Me.txtPONo.Text = String.Empty
            Me.txtPackQty.Enabled = False
            Me.dtpDocDate.Focus()
            Me.btnDelete.Visible = False
            Me.btnRefresh.Visible = True
            Me.dtpExpctJobCompletionDate.Value = Now
            Me.txtExpectPaymentFromClient.Text = String.Empty
            Me.dtpJobStartingDate.Value = Now
            Me.dtpTentiveInvoiceDate.Value = Now
            Me.dtpExpctJobCompletionDate.Value = Now
            Me.dtpVerficationCompJob.Value = Now
            Me.dtpExpctJobCompletionDate.Checked = False
            Me.dtpJobStartingDate.Checked = False
            Me.dtpTentiveInvoiceDate.Checked = False
            Me.dtpVerficationCompJob.Checked = False
            Me.dtpExpctJobCompletionDate.Checked = False
            If Me.chkApproved.Visible = False Then
                Me.chkApproved.Checked = False
            End If
            ''25-June-2014 TASK:M56 IMRAN ALI Last Opex Sale Percentage
            Me.txtOpexSalePercent.Text = String.Empty
            'End Task:M56
            If Not Me.cmbSaleOrder.SelectedIndex = -1 Then Me.cmbSaleOrder.SelectedIndex = 0
            ApplySecurity(EnumDataMode.[New])
            GetAllRecords("Master")
            GetAllRecords("Detail")
            Me.GetAllRecords("CMFAExp")
            Me.GetAllRecords("CMFAExpVoucher")
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            Me.TabExp.SelectedTab = Me.TabExp.Tabs(0).TabPage.Tab
            Me.txtAmount.Text = String.Empty
            Me.txtProjExpAmount.Text = String.Empty
            Me.txtActualExpAmount.Text = String.Empty
            Me.txtDiffProjExp.Text = String.Empty
            Me.txtContribution.Text = String.Empty
            Me.txtContributionPercentage.Text = String.Empty
            Me.txtNetProjectExp.Text = String.Empty
            Me.UltraTabPageControl6.Enabled = False
            Me.UltraTabPageControl7.Enabled = False
            If Me.cmbServicesType.SelectedIndex < 1 Then Me.cmbServicesType.SelectedIndex = 0
            Me.cmbServicesType_SelectedIndexChanged(Nothing, Nothing)
            CheckedStatus = False
            ApprovedStatus = False
            SaveRecordStatus = False
            'GetLatestSeriviceType()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub Clear(Optional ByVal Condition As String = "")
        Try
            Me.txtPackQty.Text = String.Empty
            Me.txtQty.Text = String.Empty
            Me.txtPrice.Text = String.Empty
            Me.txtTotalAmount.Text = String.Empty
            Me.cmbItem.PerformAction(Win.UltraWinGrid.UltraComboAction.Dropdown)
            Me.cmbItem.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function Save(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Save
        Try


            ''Task:2734 Unchecked MCFA by Manager.
            'If CheckedByRights = True Then
            '    If Me.btnSave.Text = "&Save" Then
            '        If CheckedStatus = False Then
            '            CMFA.CheckedByUserID = 0
            '        End If
            '    End If
            'End If
            'If IsDirectorApprovedRights = True Then
            '    If Me.btnSave.Text = "&Save" Then
            '        If ApprovedStatus = False Then
            '            CMFA.ApprovedUserId = 0
            '        End If
            '    End If
            'End If
            ''End Task:2734

            If New CmfaDal().Save(CMFA) = True Then
                Return True
            Else
                Return False
            End If



        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub SetButtonImages() Implements IGeneral.SetButtonImages

    End Sub

    Public Sub SetConfigurationBaseSetting() Implements IGeneral.SetConfigurationBaseSetting

    End Sub

    Public Sub SetNavigationButtons(ByVal Mode As SBUtility.Utility.EnumDataMode, Optional ByVal Condition As String = "") Implements IGeneral.SetNavigationButtons

    End Sub

    Public Function Update1(Optional ByVal Condition As String = "") As Boolean Implements IGeneral.Update
        Try

            ''Task:2734 Unchecked MCFA by Manager.
            'If CheckedByRights = True Then
            '    If Me.btnSave.Text = "&Update" Then
            '        If CheckedStatus = False Then
            '            CMFA.CheckedByUserID = 0
            '        End If
            '    End If
            'End If

            'If IsDirectorApprovedRights = True Then
            '    If Me.btnSave.Text = "&Update" Then
            '        If ApprovedStatus = False Then
            '            CMFA.ApprovedUserId = 0
            '        End If
            '    End If
            'End If

            If New CmfaDal().Update(CMFA) = True Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub frmCmfa_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.KeyCode = Keys.F4 Then
                Me.btnSave_Click(Nothing, Nothing)
            End If
            If e.KeyCode = Keys.Escape Then
                If Me.UltraTabControl1.SelectedTab.Index = 1 Then
                    If Me.grd.RowCount > 0 Then
                        If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub : ReSetControls()
                    End If
                    Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
                End If
            End If
            If e.KeyCode = Keys.Delete Then
                If Me.UltraTabControl1.SelectedTab.Index = 1 Then
                    If Me.grdSaved.RowCount > 0 Then
                        Me.btnDelete_Click(Nothing, Nothing)
                    End If
                End If
            End If
            If e.KeyCode = Keys.F2 Then
                Me.btnEdit_Click(Nothing, Nothing)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub frmCmfa_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown

        Me.lblProgress.Text = "Loading Please Wait ..."
        Me.lblProgress.BackColor = Color.LightYellow
        Me.lblProgress.Visible = True
        Me.Cursor = Cursors.WaitCursor
        Application.DoEvents()
        Try
            FillCombos("Company")
            FillCombos("CostCenter")
            FillCombos("Customer")
            FillCombos("SalesOrder")
            FillCombos("Vendor")
            FillCombos("Location")
            FillCombos("ItemType")
            FillCombos("Item")
            FillCombos("ArticlePack")
            FillCombos("ExpenseAccount")
            FillCombos("Services")
            IsFormOpended = True
            ReSetControls()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        Finally
            Me.lblProgress.Visible = False
            Me.Cursor = Cursors.Default
            Application.DoEvents()
        End Try
    End Sub

    Private Sub cmbCustomer_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCustomer.LostFocus
        Try
            If IsFormOpended = True Then
                If Me.cmbCustomer.ActiveRow IsNot Nothing Then
                    'If Me.cmbCustomer.ActiveRow.Cells(0).Value > 0 Then
                    FillCombos("SalesOrder")
                    'End If
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbItem_RowSelected(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbItem.RowSelected
        Try
            If IsFormOpended = True Then
                If cmbItem.IsItemInList = False Then Exit Sub
                If Me.cmbItem.ActiveRow IsNot Nothing Then
                    If Me.cmbItem.ActiveRow.Cells(0).Value > 0 Then
                        FillCombos("ArticlePack")
                        Me.txtPrice.Text = Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString)
                    End If
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbItemType_RowSelected(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinGrid.RowSelectedEventArgs) Handles cmbItemType.RowSelected
        Try
            If IsFormOpended = True Then
                If Me.cmbItemType.ActiveRow IsNot Nothing Then
                    If Me.cmbItemType.ActiveRow.Cells(0).Value >= 0 Then
                        FillCombos("Item")
                    End If
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbUnit_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbUnit.SelectedIndexChanged
        Try
            If IsFormOpended = True Then
                If Me.cmbItem.ActiveRow IsNot Nothing Then
                    If Me.cmbItem.Value > 0 Then
                        If Not Me.cmbUnit.SelectedIndex = -1 Then
                            Me.txtPackQty.Text = Val(CType(Me.cmbUnit.SelectedItem, DataRowView).Row.Item("PackQty").ToString)
                            If Me.cmbUnit.Text = "Loose" Then
                                Me.txtPackQty.Text = 1
                                Me.txtPackQty.Enabled = False
                            Else
                                Me.txtPackQty.Enabled = True
                                Me.txtPackQty.Text = CType(Me.cmbUnit.SelectedItem, DataRowView).Row.Item("PackQty").ToString
                            End If
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Try
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try

            'If blnModifyDocument = True Then
            '    ApprovedStatus = False
            '    Me.chkApproved.Checked = False
            '    CheckedStatus = False
            'End If

            If IsDateLock(Me.dtpDocDate.Value) = True Then
                ShowErrorMessage(str_ErrorPreviouseDateRecordUpdateAllow) : Exit Sub
            End If
            If IsValidate() = True Then
                If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                    If Save() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    ReSetControls()
                Else
                    Dim dt As New DataTable
                    dt = GetDataTable("Select Count(*) From PurchaseOrderMasterTable WHERE RefCMFADocId=" & Me.grdSaved.GetRow.Cells("DocId").Value.ToString & "")
                    If dt IsNot Nothing Then
                        If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                            ShowErrorMessage("Sorry record can't be updated because dependent record exist")
                            Exit Sub
                        End If
                    End If

                    Dim dt1 As New DataTable
                    dt1 = GetDataTable("Select Count(*) From SalesMasterTable WHERE CMFADocID=" & Me.grdSaved.GetRow.Cells("DocId").Value.ToString & "")
                    If dt1 IsNot Nothing Then
                        If Val(dt1.Rows(0).Item(0).ToString) > 0 Then
                            ShowErrorMessage("Sorry record can't be updated because dependent record exist")
                            Exit Sub
                        End If
                    End If

                    Dim dt2 As New DataTable
                    dt2 = GetDataTable("Select Count(*) From CashRequestHead WHERE CMFADocId=" & Me.grdSaved.GetRow.Cells("DocId").Value.ToString & "")
                    If dt2 IsNot Nothing Then
                        If Val(dt2.Rows(0).Item(0).ToString) > 0 Then
                            ShowErrorMessage("Sorry record can't be updated because dependent record exist")
                            Exit Sub
                        End If
                    End If
                    If Not msg_Confirm(str_ConfirmUpdate) = True Then Exit Sub
                    If Update1() = True Then DialogResult = Windows.Forms.DialogResult.Yes
                    ReSetControls()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            If IsDateLock(Me.dtpDocDate.Value) = True Then
                ShowErrorMessage(str_ErrorPreviouseDateRecordDeleteAllow) : Exit Sub
            End If
            Dim dt As New DataTable
            dt = GetDataTable("Select Count(*) From PurchaseOrderMasterTable WHERE RefCMFADocId=" & Me.grdSaved.GetRow.Cells("DocId").Value.ToString & "")
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    If Val(dt.Rows(0).Item(0).ToString) > 0 Then
                        ShowErrorMessage(str_ErrorDependentRecordFound)
                        Exit Sub
                    End If
                End If
            End If
            Dim dt1 As New DataTable
            dt1 = GetDataTable("Select Count(*) From SalesMasterTable WHERE CMFADocID=" & Me.grdSaved.GetRow.Cells("DocId").Value.ToString & "")
            If dt1 IsNot Nothing Then
                If dt1.Rows.Count > 0 Then
                    If Val(dt1.Rows(0).Item(0).ToString) > 0 Then
                        ShowErrorMessage("Sorry record can't be updated because dependent record exist")
                        Exit Sub
                    End If
                End If
            End If
            Dim dt2 As New DataTable
            dt1 = GetDataTable("Select Count(*) From CashRequestHead WHERE CMFADocId=" & Me.grdSaved.GetRow.Cells("DocId").Value.ToString & "")
            If dt2 IsNot Nothing Then
                If dt2.Rows.Count > 0 Then
                    If Val(dt2.Rows(0).Item(0).ToString) > 0 Then
                        ShowErrorMessage("Sorry record can't be updated because dependent record exist")
                        Exit Sub
                    End If
                End If
            End If

            If Not msg_Confirm(str_ConfirmDelete) = True Then Exit Sub
            CMFA = New CmfaBE
            CMFA.DocId = Me.grdSaved.GetRow.Cells("DocId").Value
            ''13-Aug-2014 Task:2783 Imran Ali  Frequently CMFA on Home Page (Ravi)
            CMFA.docNo = Me.grdSaved.GetRow.Cells("DocNo").Value
            CMFA.UserID = LoginUserId
            'End Task:2783
            If Delete() = True Then DialogResult = Windows.Forms.DialogResult.Yes
            ReSetControls()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtApproveBudget_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtApproveBudget.KeyDown
        Try
            If Me.txtApproveBudget.Text.Length > 0 Then
                SetFillBackgroundColor(CType(sender, TextBox))
            Else
                SetDefaultFillBackgroundColor(CType(sender, TextBox))
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtApproveBudget_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtApproveBudget.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtApproveBudget_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtApproveBudget.TextChanged
        Try
            GetTotalBudget()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub GetTotalBudget()
        Try
            'Me.txtAddSalesTax.Text = ((Val(Me.txtGSTPercentage.Text) / 100) * Val(Me.txtApproveBudget.Text))
            'Me.txtWHTax.Text = ((Val(Me.txtWHTaxPercentage.Text) / 100) * (Val(Me.txtAddSalesTax.Text) + Val(Me.txtApproveBudget.Text)))
            'Me.txtNetAmount.Text = (Val(Me.txtApproveBudget.Text) - Val(Me.txtWHTax.Text))
            Me.txtAddSalesTax.Text = ((Val(Me.txtGSTPercentage.Text) / 100) * Val(Me.txtApproveBudget.Text))
            Me.txtWHTax.Text = ((Val(Me.txtWHTaxPercentage.Text) / 100) * (Val(Me.txtAddSalesTax.Text) + Val(Me.txtApproveBudget.Text)))
            Me.txtNetAmount.Text = (Val(Me.txtApproveBudget.Text) - Val(Me.txtWHTax.Text))
            Me.txtOpexSalePercent_TextChanged(Nothing, Nothing)
            GetTotalContribution()
            ''11-Sep-2014 Task:2836 Imran Ali CMFA Bug fixed and enhancement

            'Me.txtNetAmount.Text = FormatNumber(Val(Me.txtNetAmount.Text), 0, TriState.False, , TriState.True)
            'End Task:2836
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtGSTPercentage_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtGSTPercentage.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then SetDefaultFillBackgroundColor(CType(sender, TextBox))
            If Me.txtApproveBudget.Text.Length > 0 Then
                SetFillBackgroundColor(CType(sender, TextBox))
            Else
                SetDefaultFillBackgroundColor(CType(sender, TextBox))
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtGSTPercentage_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGSTPercentage.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub


    Private Sub txtGSTPercentage_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGSTPercentage.TextChanged
        Try
            GetTotalBudget()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtWHTaxPercentage_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtWHTaxPercentage.KeyDown
        Try
            If Me.txtApproveBudget.Text.Length > 0 Then
                SetFillBackgroundColor(CType(sender, TextBox))
            Else
                SetDefaultFillBackgroundColor(CType(sender, TextBox))
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtWHTaxPercentage_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWHTaxPercentage.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtWHTaxPercentage_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtWHTaxPercentage.TextChanged
        Try
            GetTotalBudget()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            If gridValidate() = False Then Exit Sub
            Dim objDt As DataTable = CType(Me.grd.DataSource, DataTable)
            objDt.AcceptChanges()
            Dim objDr As DataRow
            objDr = objDt.NewRow
            objDr(enmGrdDetail.LocationId) = Me.cmbLocation.SelectedValue
            objDr(enmGrdDetail.ArticleDefId) = Val(Me.cmbItem.Value)
            objDr(enmGrdDetail.Code) = Me.cmbItem.ActiveRow.Cells("Code").Value.ToString
            objDr(enmGrdDetail.Item) = Me.cmbItem.ActiveRow.Cells("Item").Value.ToString
            objDr(enmGrdDetail.Unit) = IIf(Me.cmbUnit.Text <> "Loose", "Pack", "Loose")
            objDr(enmGrdDetail.PackQty) = Val(Me.txtPackQty.Text)
            objDr(enmGrdDetail.Qty) = Val(Me.txtQty.Text)
            objDr(enmGrdDetail.Price) = Val(Me.txtPrice.Text)
            objDr(enmGrdDetail.Current_Price) = Val(Me.cmbItem.ActiveRow.Cells("PurchasePrice").Value.ToString)
            objDr(enmGrdDetail.InvoicePrice) = Val(Me.cmbItem.ActiveRow.Cells("Price").Value.ToString)
            objDr(enmGrdDetail.VendorId) = Val(Me.cmbVendor.Value)
            objDr(enmGrdDetail.PackDesc) = Me.cmbUnit.Text
            objDr(enmGrdDetail.UOM) = Me.cmbItem.ActiveRow.Cells("UOM").Value.ToString
            objDr(enmGrdDetail.InvoicePrice) = Val(Me.cmbItem.ActiveRow.Cells("Price").Value.ToString)
            objDr(enmGrdDetail.Comments) = String.Empty 'Task:2723 Added Column Comments 
            objDt.Rows.Add(objDr)
            objDt.AcceptChanges()
            GetTotal()
            txtOpexSalePercent_TextChanged(Nothing, Nothing)
            GetTotalContribution()
            Clear()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Function gridValidate() As Boolean
        Try
            If Me.cmbLocation.SelectedIndex < 0 Then
                ShowErrorMessage("Please define location")
                Me.cmbLocation.Focus()
                Return False
            End If
            If Not Me.cmbItem.ActiveRow.Cells(0).Value > 0 Then
                ShowErrorMessage("Please select item.")
                Me.cmbItem.Focus()
                Me.cmbItem.PerformAction(Win.UltraWinGrid.UltraComboAction.Dropdown)
                Return False
            End If
            If Val(Me.txtQty.Text) <= 0 Then
                ShowErrorMessage("Please enter qty")
                Me.txtQty.Focus()
                Return False
            End If
            If Val(Me.txtPrice.Text) <= 0 Then
                ShowErrorMessage("Please enter price")
                Me.txtPrice.Focus()
                Return False
            End If
            'If Not Me.cmbVendor.ActiveRow.Cells(0).Value > 0 Then
            '    ShowErrorMessage("Please enter vendor")
            '    Me.cmbVendor.Focus()
            '    Return False
            'End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub txtQty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty.TextChanged
        Try
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub GetTotal()
        Try
            If Not Me.cmbUnit.SelectedIndex < 0 Then
                If Me.cmbUnit.Text <> "Loose" Then
                    'Me.txtTotalAmount.Text = ((Val(Me.txtQty.Text) * Val(Me.txtPackQty.Text)) * Val(Me.txtPrice.Text))
                    Me.txtTotalAmount.Text = ((Val(Me.txtQty.Text) * Val(Me.txtPackQty.Text)) * Val(Me.txtPrice.Text))
                Else
                    Me.txtTotalAmount.Text = (Val(Me.txtQty.Text) * Val(Me.txtPrice.Text))
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtPrice_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrice.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtPrice_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPrice.TextChanged
        Try
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub txtPackQty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPackQty.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtPackQty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPackQty.TextChanged
        Try
            GetTotal()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Public Sub EditRecords(Optional ByVal Condition As String = "")
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            If Me.grd.RowCount > 0 Then
                If Not msg_Confirm(str_ConfirmGridClear) = True Then Exit Sub : ReSetControls()
            End If

            If Not (IsDirectorApprovedRights = True Or CheckedByRights = True Or blnModifyDocument = True) Then
                If LoginUserName.ToUpper <> Me.grdSaved.GetRow.Cells("UserName").Value.ToString.ToUpper Then
                    If LoginGroup <> "Administrator" Then
                        ShowErrorMessage("You are not authorized.")
                        Exit Sub
                    End If
                End If
            End If
            DocId = Me.grdSaved.GetRow.Cells("DocId").Value.ToString
            Me.txtDocumentNo.Text = Me.grdSaved.GetRow.Cells("DocNo").Value.ToString
            Me.dtpDocDate.Value = Me.grdSaved.GetRow.Cells("DocDate").Value
            Me.txtActivity.Text = Me.grdSaved.GetRow.Cells("Remarks").Value.ToString
            If Not IsDBNull(Me.grdSaved.GetRow.Cells("Approved").Value) Then
                Me.chkApproved.Checked = Me.grdSaved.GetRow.Cells("Approved").Value
            Else
                Me.chkApproved.Checked = False
            End If
            Me.cmbCompany.SelectedValue = Val(Me.grdSaved.GetRow.Cells("LocationId").Value.ToString)
            Me.cmbProject.SelectedValue = Val(Me.grdSaved.GetRow.Cells("ProjectId").Value.ToString)
            Me.cmbCustomer.Value = Val(Me.grdSaved.GetRow.Cells("coa_detail_id").Value.ToString)
            FillCombos("SalesOrder")
            Me.cmbSaleOrder.SelectedValue = Val(Me.grdSaved.GetRow.Cells("POId").Value.ToString)
            Me.txtPONo.Text = Me.grdSaved.GetRow.Cells("InvoiceNo").Value.ToString
            Me.txtApproveBudget.Text = Val(Me.grdSaved.GetRow.Cells("ApprovedBudget").Value.ToString)
            Me.txtGSTPercentage.Text = Val(Me.grdSaved.GetRow.Cells("TaxPercent").Value.ToString)
            Me.txtWHTaxPercentage.Text = Val(Me.grdSaved.GetRow.Cells("WHTaxPercent").Value.ToString)
            If Not IsDBNull(Me.grdSaved.GetRow.Cells("ExptJobCompDate").Value) Then
                Me.dtpExpctJobCompletionDate.Value = Me.grdSaved.GetRow.Cells("ExptJobCompDate").Value
                Me.dtpExpctJobCompletionDate.Checked = True
            Else
                Me.dtpExpctJobCompletionDate.Value = Now
                Me.dtpExpctJobCompletionDate.Checked = False
            End If
            txtExpectPaymentFromClient.Text = Val(Me.grdSaved.GetRow.Cells("ExptPaymentFromClient").Value.ToString)
            If Not IsDBNull(Me.grdSaved.GetRow.Cells("JobStartingTime").Value) Then
                Me.dtpJobStartingDate.Value = Me.grdSaved.GetRow.Cells("JobStartingTime").Value
                Me.dtpJobStartingDate.Checked = True
            Else
                Me.dtpJobStartingDate.Value = Now
                Me.dtpJobStartingDate.Checked = False
            End If
            If Not IsDBNull(Me.grdSaved.GetRow.Cells("TentiveInvoiceDate").Value) Then
                Me.dtpTentiveInvoiceDate.Value = Me.grdSaved.GetRow.Cells("TentiveInvoiceDate").Value
                Me.dtpTentiveInvoiceDate.Checked = True
            Else
                Me.dtpTentiveInvoiceDate.Value = Now
                Me.dtpTentiveInvoiceDate.Checked = False
            End If
            If Not IsDBNull(Me.grdSaved.GetRow.Cells("VerificationPeriodAfterCompletionJob").Value) Then
                Me.dtpVerficationCompJob.Value = Me.grdSaved.GetRow.Cells("VerificationPeriodAfterCompletionJob").Value
                Me.dtpVerficationCompJob.Checked = True
            Else
                Me.dtpVerficationCompJob.Value = Now
                Me.dtpVerficationCompJob.Checked = False
            End If
            Me.txtOpexSalePercent.Text = Val(Me.grdSaved.GetRow.Cells("OPEX_Sale_Percent").Value.ToString)

            ''25-June-2014 TASK:2703 IMRAN ALI Enhancement In CMFA (RAVI)
            Me.txtEstimateExpense.Text = Val(Me.grdSaved.GetRow.Cells("EstimateExpense").Value.ToString)
            Me.txtReturnComments.Text = Me.grdSaved.GetRow.Cells("ReturnComment").Value.ToString
            If IsDBNull(Me.grdSaved.GetRow.Cells("ReturnStatus").Value) Then
                blnReturnStatus = False
            Else
                blnReturnStatus = Me.grdSaved.GetRow.Cells("ReturnStatus").Value
            End If
            RemoveHandler cmbServicesType.SelectedIndexChanged, AddressOf cmbServicesType_SelectedIndexChanged
            Me.cmbServicesType.Text = Me.grdSaved.GetRow.Cells("CMFAType").Value.ToString
            AddHandler cmbServicesType.SelectedIndexChanged, AddressOf cmbServicesType_SelectedIndexChanged
            'End Task:2703
            ApprovedStatus = Me.grdSaved.GetRow.Cells("Approved").Value
            If IsDBNull(Me.grdSaved.GetRow.Cells("CheckedStatus").Value) Then
                CheckedStatus = False
            Else
                CheckedStatus = Me.grdSaved.GetRow.Cells("CheckedStatus").Value
            End If

            Me.GetAllRecords("Detail")
            Me.GetAllRecords("CMFAExpVoucher")
            Me.GetAllRecords("CMFAExp")
            Me.btnSave.Text = "&Update"
            Me.grdFiles.DataSource = New CmfaDal().CMFAGetAllDocument(DocId)
            Me.UltraTabControl1.SelectedTab = Me.UltraTabControl1.Tabs(0).TabPage.Tab
            Me.txtOpexSalePercent_TextChanged(Nothing, Nothing)
            'GetAcutalExpenseDetail(DocId)
            GetTotalContribution()
            ApplySecurity(EnumDataMode.Edit)

            'Task:2703 Apply Rights On Approved/ReturnStatus
            'If btnSave.Enabled = True Then
            '    If Me.grdSaved.GetRow IsNot Nothing Then
            '        If blnModifyDocument = True Then
            '            Me.btnSave.Enabled = True
            '            Me.btnApprovedByDirector.Enabled = False
            '            Me.btnCheckedByManager.Enabled = False
            '            Exit Sub
            '        ElseIf Me.grdSaved.GetRow.Cells("ReturnStatus").Value = False Then
            '            Me.btnSave.Enabled = True
            '            If IsDirectorApprovedRights = True Then Me.btnApprovedByDirector.Enabled = True
            '            If Not IsDirectorApprovedRights = True Then Me.btnCheckedByManager.Enabled = True
            '            Exit Sub
            '        ElseIf Me.grdSaved.GetRow.Cells("ReturnStatus").Value = True Then
            '            If IsDirectorApprovedRights = True Then
            '                Me.btnSave.Enabled = False
            '                Me.btnApprovedByDirector.Enabled = False
            '                Me.btnCheckedByManager.Enabled = False
            '            Else
            '                Me.btnSave.Enabled = True
            '                Me.btnCheckedByManager.Enabled = True
            '            End If
            '            Exit Sub
            '        ElseIf IsDirectorApprovedRights = True Then
            '            If Not Me.grdSaved.GetRow.Cells("ApprovedUserId").Value > 0 Or Me.grdSaved.GetRow.Cells("ReturnStatus").Value = True Then
            '                Me.btnSave.Enabled = True
            '                Me.btnApprovedByDirector.Enabled = True
            '                Me.btnCheckedByManager.Enabled = False
            '            Else
            '                Me.btnSave.Enabled = False
            '                Me.btnApprovedByDirector.Enabled = False
            '                Me.btnCheckedByManager.Enabled = False
            '                Me.UltraTabPageControl6.Enabled = False
            '            End If
            '            Exit Sub
            '        ElseIf Me.grdSaved.GetRow.Cells("CheckedByUserId").Value > 0 Or Me.grdSaved.GetRow.Cells("ApprovedUserId").Value > 0 Then
            '            Me.btnSave.Enabled = False
            '            Me.btnCheckedByManager.Enabled = False
            '            Exit Sub
            '        End If
            '        End If
            '    End If

            'End Task:2703
            'Me.btnDelete.Visible = True
            If Not LoginGroup.ToString = "Administrator" Then ''03-Sep-2014 Task:2824 Imran Ali Apply all rights of admin on CMFA Document
                If Me.grdSaved.GetRow.Cells("Approved").Value = True Then
                    Me.btnSave.Enabled = False
                    Me.btnCheckedByManager.Enabled = False
                    Me.btnApprovedByDirector.Enabled = False
                End If
                If Me.grdSaved.GetRow.Cells("ReturnStatus").Value = True Then
                    If CheckedByRights = True Then Me.btnSave.Enabled = True
                    If CheckedByRights = True Then Me.btnCheckedByManager.Enabled = True
                    Me.btnApprovedByDirector.Enabled = False
                End If
                If Me.grdSaved.GetRow.Cells("CheckedStatus").Value = True Then
                    If Not IsDirectorApprovedRights = True Then Me.btnSave.Enabled = False
                    Me.btnCheckedByManager.Enabled = False
                    If Not IsDirectorApprovedRights = True Then Me.btnApprovedByDirector.Enabled = False
                End If
            End If
            'End If 'End Task:2824





        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GridEX2_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSaved.DoubleClick
        Try
            EditRecords()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub UltraTabControl1_SelectedTabChanged(ByVal sender As Object, ByVal e As Infragistics.Win.UltraWinTabControl.SelectedTabChangedEventArgs) Handles UltraTabControl1.SelectedTabChanged
        Try

            If e.Tab.Index = 1 Then
                Me.btnRefresh.Visible = False
                Me.btnDelete.Visible = True
            ElseIf e.Tab.Index = 4 Then
                Me.grdFiles.DataSource = New CmfaDal().CMFAGetAllDocument(DocId)
            Else
                Me.btnRefresh.Visible = True
                If Me.btnSave.Text = "&Update" Then
                    Me.btnDelete.Visible = True
                Else
                    Me.btnDelete.Visible = False
                End If
            End If

            'Task:2703 Apply Secutity 
            If Me.btnSave.Text = "&Save" Or Me.btnSave.Text = "Save" Then
                'Me.UltraTabPageControl6.Enabled = False
                Me.UltraTabPageControl7.Enabled = False
            Else
                'Me.UltraTabPageControl6.Enabled = True
                Me.UltraTabPageControl7.Enabled = True
            End If
            'End Task:2703

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim id As Integer = 0I
            id = Me.cmbCompany.SelectedIndex
            FillCombos("Company")
            Me.cmbCompany.SelectedIndex = id
            id = Me.cmbProject.SelectedIndex
            FillCombos("CostCenter")
            Me.cmbProject.SelectedIndex = id
            id = Me.cmbCustomer.ActiveRow.Cells(0).Value
            FillCombos("Customer")
            Me.cmbCustomer.Value = id
            FillCombos("SalesOrder")
            id = Me.cmbVendor.ActiveRow.Cells(0).Value
            FillCombos("Vendor")
            Me.cmbVendor.Value = id
            id = Me.cmbLocation.SelectedIndex
            FillCombos("Location")
            Me.cmbLocation.SelectedIndex = id
            id = Me.cmbItemType.ActiveRow.Cells(0).Value
            FillCombos("ItemType")
            Me.cmbItemType.Value = id
            id = Me.cmbItem.ActiveRow.Cells(0).Value
            FillCombos("Item")
            Me.cmbItem.Value = id
            FillCombos("ArticlePack")

            id = Me.cmbServicesType.SelectedIndex
            FillCombos("Services")
            Me.cmbServicesType.SelectedIndex = id

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_CellUpdated(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.CellUpdated
        Try
            txtOpexSalePercent_TextChanged(Nothing, Nothing)
            GetTotalContribution()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub grd_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grd.ColumnButtonClick
        Try
            If e.Column.Key = "Delete" Then
                Me.grd.GetRow.Delete()
                Me.grd.UpdateData()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub CtrlGrdBar1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles CtrlGrdBar1.Load
        Try
            If IsFormOpended = False Then Exit Sub
            If Me.UltraTabControl1.SelectedTab.Index = 0 Then
                CtrlGrdBar1.MyGrid = Me.grd
                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name) Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grd.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                    Me.grd.LoadLayoutFile(fs)
                    fs.Close()
                    fs.Dispose()
                End If
            Else
                CtrlGrdBar1.MyGrid = Me.grdSaved
                If IO.File.Exists(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name) Then
                    Dim fs As New IO.FileStream(Application.ExecutablePath & "\..\Layouts\" & Me.Name & "_" & Me.grdSaved.Name, IO.FileMode.Open, IO.FileAccess.ReadWrite)
                    Me.grdSaved.LoadLayoutFile(fs)
                    fs.Close()
                    fs.Dispose()
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbSaleOrder_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSaleOrder.SelectedIndexChanged
        Try
            If IsFormOpended = True Then
                If Me.cmbSaleOrder.SelectedIndex > 0 Then
                    Me.txtApproveBudget.Text = Val(CType(Me.cmbSaleOrder.SelectedItem, DataRowView).Row.Item("Budget").ToString)
                    GetAllRecords("SalesOrder")
                End If
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If Me.grdSaved.RowCount = 0 Then Exit Sub
            AddRptParam("@DocId", Me.grdSaved.GetRow.Cells("DocId").Value)
            ShowReport("RptCmfaDocument")
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtOpexSalePercent_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtOpexSalePercent.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtOpexSalePercent_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtOpexSalePercent.TextChanged
        Try
            If IsFormOpended = True Then
                Me.grd.UpdateData()
                Me.txtOpexOnSale.Text = ((Val(Me.txtOpexSalePercent.Text) / 100) * Val(Me.txtApproveBudget.Text))
                Me.txtNetProjectExp.Text = Val(Me.txtOpexOnSale.Text) + Val(Me.grd.GetTotal(Me.grd.RootTable.Columns(enmGrdDetail.Total), Janus.Windows.GridEX.AggregateFunction.Sum)) + Val(Me.grdExp.GetTotal(Me.grdExp.RootTable.Columns("Amount"), Janus.Windows.GridEX.AggregateFunction.Sum))

                'Me.txtOpexOnSale.Text = FormatNumber(Val(Me.txtOpexOnSale.Text), 0, TriState.False, , TriState.True)
                'Me.txtNetProjectExp.Text = FormatNumber(Val(Me.txtNetProjectExp.Text), 0, TriState.False, , TriState.True)
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grd_RecordsDeleted(ByVal sender As Object, ByVal e As System.EventArgs) Handles grd.RecordsDeleted
        Try
            txtOpexSalePercent_TextChanged(Nothing, Nothing)
            GetTotalContribution()
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnExpAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExpAdd.Click
        Try
            If Me.cmbAccounts.SelectedIndex <= 0 Then
                ShowErrorMessage("Please select Expense Account.")
                Me.cmbAccounts.Focus()
                Exit Sub
            End If
            If Val(Me.txtAmount.Text) <= 0 Then
                ShowErrorMessage("Please enter Expense amount.")
                Me.txtAmount.Focus()
                Exit Sub
            End If
            grdExp.UpdateData()
            Dim objdt As DataTable = CType(Me.grdExp.DataSource, DataTable)
            Dim chkDr() As DataRow = objdt.Select("coa_detail_id=" & Me.cmbAccounts.SelectedValue & "")
            If chkDr IsNot Nothing Then
                If chkDr.Length > 0 Then
                    If Me.cmbAccounts.SelectedValue = Val(chkDr(0).Item(2).ToString) Then
                        ShowErrorMessage("Expense Account already exists into grid.")
                        Me.cmbAccounts.Focus()
                        Exit Sub
                    End If
                End If
            End If
            Dim dr As DataRow
            dr = objdt.NewRow
            dr(0) = 0
            dr(1) = DocId
            dr(2) = Me.cmbAccounts.SelectedValue
            dr(3) = CType(Me.cmbAccounts.SelectedItem, DataRowView).Row.Item("detail_code").ToString
            dr(4) = Me.cmbAccounts.Text
            dr(5) = Val(Me.txtAmount.Text)
            objdt.Rows.Add(dr)
            objdt.AcceptChanges()
            Me.txtAmount.Text = String.Empty
            Me.cmbAccounts.Focus()
            Me.cmbAccounts.DroppedDown = True

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub cmbAccounts_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbAccounts.Enter
        Try
            Me.cmbAccounts.DroppedDown = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub txtAmount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmount.KeyPress
        Try
            NumValidation(sender, e)
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Public Sub GetTotalContribution()
        Try
            Me.grd.Update()
            Me.grdActualExpense.UpdateData()
            GetTotal()
            'Me.txtProjExpAmount.Text = Me.grdActualExpense.GetTotal(Me.grdActualExpense.RootTable.Columns("Projected Exp"), Janus.Windows.GridEX.AggregateFunction.Sum)
            'Me.txtActualExpAmount.Text = Val(Me.grdActualExpense.GetTotal(Me.grdActualExpense.RootTable.Columns("Actual Exp"), Janus.Windows.GridEX.AggregateFunction.Sum))
            'Me.txtDiffProjExp.Text = Val(Me.txtProjExpAmount.Text) - Val(Me.txtActualExpAmount.Text)
            'Me.txtContribution.Text = Val(Me.txtNetAmount.Text) - Val(Me.txtProjExpAmount.Text)
            'Me.txtContributionPercentage.Text = Math.Round(Val(Me.txtContribution.Text) / Val(Me.txtNetAmount.Text) * 100, 2)
            'Me.txtProjExpAmount.Text = Val(Me.txtNetProjectExp.Text) 'Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)
            'Me.txtActualExpAmount.Text = GetTotalActualExpense(DocId) 'Val(Me.grdActualExpense.GetTotal(Me.grdActualExpense.RootTable.Columns("Actual Exp"), Janus.Windows.GridEX.AggregateFunction.Sum))
            'Me.txtDiffProjExp.Text = Val(Me.txtProjExpAmount.Text) - Val(Me.txtActualExpAmount.Text)
            'Me.txtContribution.Text = Val(Me.txtNetAmount.Text) - Val(Me.txtProjExpAmount.Text)
            'Me.txtContributionPercentage.Text = Math.Round(Val(Me.txtContribution.Text) / Val(Me.txtNetAmount.Text) * 100, 2)
            Me.txtProjExpAmount.Text = Val(Me.txtNetProjectExp.Text) 'Me.grd.GetTotal(Me.grd.RootTable.Columns("Total"), Janus.Windows.GridEX.AggregateFunction.Sum)
            Me.txtActualExpAmount.Text = GetTotalActualExpense(DocId) 'Val(Me.grdActualExpense.GetTotal(Me.grdActualExpense.RootTable.Columns("Actual Exp"), Janus.Windows.GridEX.AggregateFunction.Sum))
            Me.txtDiffProjExp.Text = (Val(Me.txtProjExpAmount.Text) - Val(Me.txtActualExpAmount.Text))
            Me.txtContribution.Text = (Val(Me.txtNetAmount.Text) - Val(Me.txtProjExpAmount.Text))
            Me.txtContributionPercentage.Text = Math.Round(Val(Me.txtContribution.Text) / Val(Me.txtNetAmount.Text) * 100, 2)
            ''11-Sep-2014 Task:2836 Imran Ali CMFA Bug fixed and enhancement
            Me.txtProjExpAmount.Text = FormatNumber(Val(Me.txtProjExpAmount.Text), 0, TriState.False, , TriState.True)
            Me.txtActualExpAmount.Text = FormatNumber(Val(Me.txtActualExpAmount.Text), 0, TriState.False, , TriState.True)
            Me.txtDiffProjExp.Text = FormatNumber(Val(Me.txtDiffProjExp.Text), 0, TriState.False, , TriState.True)
            Me.txtContribution.Text = FormatNumber(Val(Me.txtContribution.Text), 0, TriState.False, , TriState.True)
            'Me.txtNetAmount.Text = FormatNumber(Val(Me.txtNetAmount.Text), 0, TriState.False, , TriState.True)
            'Me.txtOpexOnSale.Text = FormatNumber(Val(Me.txtOpexOnSale.Text), 0, TriState.False, , TriState.True)
            'Me.txtAddSalesTax.Text = FormatNumber(Val(txtAddSalesTax.Text), 0, TriState.False, , TriState.True)
            'Me.txtWHTax.Text = FormatNumber(Val(Me.txtWHTax.Text), 0, TriState.False, , TriState.True)
            'End Task:2836
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''25-June-2014 TASK:M56 IMRAN ALI Last Opex Sale Percentage
    Public Function GetLastOpexSalePercentage() As Double
        Try
            Dim strSQL As String = String.Empty
            strSQL = "Select Max(Isnull(OPEX_Sale_Percent,0)) From CMFAMasterTable"
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Return Val(dt.Rows(0).Item(0).ToString)
                Else
                    Return 0
                End If
            Else
                Return 0
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'End Task:M56

    Private Sub btnReturnCMFA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReturnCMFA.Click
        Try

            blnReturnStatus = True
            If New CmfaDal().UpdateReturn(DocId, Me.txtReturnComments.Text, blnReturnStatus) = True Then
                'Me.txtReturnComments.Text = String.Empty
                ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try

            Me.OpenFileDialog1.FileName = ""
            Me.OpenFileDialog1.Filter = "All|*.*"
            If Me.OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                If IO.File.Exists(Me.OpenFileDialog1.FileName) Then
                    Me.txtFile.Text = Me.OpenFileDialog1.FileName
                    Dim intID As Integer = New CmfaDal().CMFADocumentSave(DocId)
                    If intID > 0 Then
                        Dim strFileName As String = String.Empty
                        Dim strFilePath As String = String.Empty
                        Dim strFile As String = String.Empty
                        Dim strPath As String = String.Empty
                        If Not getConfigValueByType("CMFADocumentAttachmentPath").ToString = "Error" Then
                            strPath = getConfigValueByType("CMFADocumentAttachmentPath").ToString
                        End If
                        If Not strPath.Length > 0 Then
                            If Not IO.Directory.Exists(Application.StartupPath & "\CMFADocuments") Then
                                IO.Directory.CreateDirectory(Application.StartupPath & "\CMFADocuments")
                            End If
                            strPath = Application.StartupPath & "\CMFADocuments"
                        End If

                        strFile = Me.txtFile.Text
                        strFileName = intID & "_" & DocId & "_" & strFile.Substring(strFile.LastIndexOf("\") + 1)
                        File.Copy(Me.txtFile.Text, strPath & "\" & strFileName)

                        Dim bln As Boolean = New CmfaDal().CMFADocumentUpdate(intID, strFileName, strPath)
                        If bln = True Then
                            Me.grdFiles.DataSource = New CmfaDal().CMFAGetAllDocument(DocId)
                            Me.txtFile.Text = String.Empty
                        End If
                    End If
                End If
            Else
                Exit Sub
            End If

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub grdFiles_ColumnButtonClick(ByVal sender As System.Object, ByVal e As Janus.Windows.GridEX.ColumnActionEventArgs) Handles grdFiles.ColumnButtonClick
        Try
            If e.Column.Key = "Column1" Then
                If IO.File.Exists(Me.grdFiles.GetRow.Cells("strFilePath").Value.ToString) Then
                    Process.Start(Me.grdFiles.GetRow.Cells("strFilePath").Value.ToString)
                End If

                ''02-Jul-2014 TASK:2710 IMRAN ALI CMFA Document Delete In CMFA (Ravi)
            ElseIf e.Column.Key = "Delete" Then
                ''11-Sep-2014 Task:2836 Imran Ali CMFA Bug fixed and enhancement
                If Me.grdSaved.GetRow.Cells("Approved").Value = True Then
                    ShowErrorMessage("You can not delete this file because this approved.")
                    Exit Sub
                End If
                'End Task:2836
                If File.Exists(Me.grdFiles.GetRow.Cells("strFilePath").Value.ToString) Then

                    If Con.State = ConnectionState.Closed Then Con.Open()
                    Dim cmd As New OleDb.OleDbCommand
                    cmd.Connection = Con
                    cmd.CommandText = "Delete From CMFAAttachDocumentsTable WHERE ID=" & Me.grdFiles.GetRow.Cells("ID").Value & ""
                    cmd.ExecuteNonQuery()

                    IO.File.Delete(Me.grdFiles.GetRow.Cells("StrFilePath").Value.ToString)
                    Me.grdFiles.DataSource = New CmfaDal().CMFAGetAllDocument(DocId)

                End If
                'End Task:2710
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub cmbServicesType_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbServicesType.SelectedIndexChanged
        Try
            If IsFormOpended = False Then Exit Sub
            If Me.cmbServicesType.DataSource Is Nothing Then Exit Sub
            Dim dr = CType(Me.cmbServicesType.SelectedItem, DataRowView)

            Me.txtOpexSalePercent.Text = Val(dr(enmServices.Opex_Sale_Percent).ToString) ' Val(dt.Rows(0).Item("OPEX_Sale_Percent").ToString)
            Me.txtGSTPercentage.Text = Val(dr(enmServices.Tax_Percent).ToString) 'Val(dt.Rows(0).Item("TaxPercent").ToString)
            Me.txtWHTaxPercentage.Text = Val(dr(enmServices.WHT_Percent).ToString) 'Val(dt.Rows(0).Item("WHTaxPercent").ToString)


        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    'Public Sub GetLatestSeriviceType()
    '    Try
    '        If IsFormOpended = False Then Exit Sub
    '        Dim strSQL As String = String.Empty
    '        strSQL = "SELECT     dbo.CMFAMasterTable.CMFAType, MAX(dbo.CMFAMasterTable.OPEX_Sale_Percent) AS OPEX_Sale_Percent, MAX(dbo.CMFAMasterTable.TaxPercent) AS TaxPercent, " _
    '                 & " MAX(dbo.CMFAMasterTable.WHTaxPercent) AS WHTaxPercent " _
    '                 & " FROM         dbo.CMFADetailTable INNER JOIN " _
    '                 & " dbo.CMFAMasterTable ON dbo.CMFADetailTable.DocId = dbo.CMFAMasterTable.DocId " _
    '                 & " WHERE     (dbo.CMFAMasterTable.CMFAType <> '') AND dbo.CMFAMasterTable.CMFAType='" & Me.cmbServicesType.Text & "' " _
    '                 & " GROUP BY dbo.CMFAMasterTable.CMFAType "
    '        Dim dt As New DataTable
    '        dt = GetDataTable(strSQL)

    '        If dt IsNot Nothing Then
    '            If dt.Rows.Count > 0 Then
    '                Me.txtOpexSalePercent.Text = Val(dt.Rows(0).Item("OPEX_Sale_Percent").ToString)
    '                Me.txtGSTPercentage.Text = Val(dt.Rows(0).Item("TaxPercent").ToString)
    '                Me.txtWHTaxPercentage.Text = Val(dt.Rows(0).Item("WHTaxPercent").ToString)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub
    'Private Sub btnApproved_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApproved.Click
    '    Try
    '        If Me.grdSaved.RowCount <= 0 Then
    '            ReSetControls()
    '        End If
    '        CMFA = New CmfaBE
    '        CMFA.Approved = True
    '        CMFA.ApprovedUserId = LoginUserId
    '        CMFA.DocId = Me.grdSaved.GetRow.Cells("DocId").Value
    '        If New CmfaDal().UpdateApprovedStatus(CMFA) Then
    '            ReSetControls()
    '        End If
    '    Catch ex As Exception
    '        ShowErrorMessage(ex.Message)
    '    End Try
    'End Sub
    Private Sub btnApprovedByDirector_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApprovedByDirector.Click
        Try
            ''03-Sep-2014 Task:2824 Imran Ali Apply all rights of admin on CMFA Document
            CMFA = New CmfaBE
            CMFA.ApprovedUserId = LoginUserId
            CMFA.DocId = DocId
            CMFA.docNo = Me.txtDocumentNo.Text
            CMFA.DocDate = Me.dtpDocDate.Value
            CMFA.Remarks = Me.txtActivity.Text
            CMFA.Approved = True
            If Me.grdSaved.RowCount > 0 Then
                CMFA.UserID = IIf(LoginUserId <> Me.grdSaved.GetRow.Cells("UserId").Value, Me.grdSaved.GetRow.Cells("UserId").Value, LoginUserId)
            Else
                CMFA.UserID = LoginUserId
            End If
            CMFA.ApprovedUserName = LoginUserName
            If New CmfaDal().UpdateApprovedStatus(CMFA) = True Then
                If SaveRecordStatus = False Then
                    msg_Information("Document [" & Me.txtDocumentNo.Text & "] has been approved.")
                    GetAllRecords("Master")
                End If
            End If
            'End If
            'eND tASK:2824

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnApprovedByManager_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCheckedByManager.Click
        Try
            'If Me.grdSaved.RowCount = 0 Then Exit Sub
            ''03-Sep-2014 Task:2824 Imran Ali Apply all rights of admin on CMFA Document
            CMFA = New CmfaBE
            CMFA.CheckedByUserID = LoginUserId
            CMFA.DocId = DocId
            CMFA.docNo = Me.txtDocumentNo.Text
            CMFA.DocDate = Me.dtpDocDate.Value
            CMFA.Remarks = Me.txtActivity.Text
            CMFA.CheckedStatus = True
            If Me.grdSaved.RowCount > 0 Then
                CMFA.UserID = IIf(LoginUserId <> Me.grdSaved.GetRow.Cells("UserId").Value, Me.grdSaved.GetRow.Cells("UserId").Value, LoginUserId)
            Else
                CMFA.UserID = LoginUserId
            End If
            CMFA.CheckedUserName = LoginUserName
            If New CmfaDal().UpdateCheckByManager(CMFA) = True Then
                If SaveRecordStatus = False Then
                    msg_Information("Document [" & Me.txtDocumentNo.Text & "] has been checked.")
                    GetAllRecords("Master")
                End If
            End If
            'eND tASK:2824
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    ''11-Jul-2014 Task:2734 IMRAN ALI Ehancement CMFA Document
    Public Function GetTotalActualExpense(ByVal DocId As Integer) As Double
        Try

            Dim strSQL As String = String.Empty
            strSQL = "Select CMFA.DocId, (IsNull(RecvAmount,0)+IsNull(Exp.Expense,0)+IsNull(VExp.VExpense,0)) as ActualExpense From CMFAMasterTable CMFA " _
                     & " LEFT OUTER JOIN ( SELECT Recv.DocId, ISNULL(Recv.Pur, 0) AS RecvAmount FROM (SELECT ISNULL(dbo.ReceivingMasterTable.RefCMFADocId, 0) AS DocId, SUM(ISNULL(dbo.ReceivingDetailTable.Qty, 0) * ISNULL(dbo.ReceivingDetailTable.Price,0))+SUM((IsNull(TaxPercent,0)/100)*(IsNull(Qty,0)*IsNull(Price,0))) AS Pur " _
                     & " FROM  dbo.ReceivingMasterTable INNER JOIN dbo.ReceivingDetailTable ON dbo.ReceivingMasterTable.ReceivingId = dbo.ReceivingDetailTable.ReceivingId " _
                     & " WHERE(ISNULL(dbo.ReceivingMasterTable.RefCMFADocId, 0) = " & DocId & ") " _
                     & " GROUP BY dbo.ReceivingMasterTable.RefCMFADocId) AS Recv) Receiving On Receiving.DocId =  CMFA.DocId " _
                     & " LEFT OUTER JOIN " _
                     & " (SELECT ISNULL(ReceivingMasterTable_1.RefCMFADocId, 0) AS DocId, SUM(ISNULL(dbo.InwardExpenseDetailTable.Exp_Amount, 0)) AS Expense " _
                     & " FROM dbo.InwardExpenseDetailTable INNER JOIN " _
                     & " dbo.ReceivingMasterTable AS ReceivingMasterTable_1 ON ReceivingMasterTable_1.ReceivingId = dbo.InwardExpenseDetailTable.PurchaseId " _
                     & " WHERE(ISNULL(ReceivingMasterTable_1.RefCMFADocId, 0) = " & DocId & ") " _
                     & " GROUP BY ISNULL(ReceivingMasterTable_1.RefCMFADocId, 0)) " _
                     & " AS Exp ON Exp.DocId = CMFA.DocId " _
                     & " LEFT OUTER JOIN " _
                     & " (SELECT  ISNULL(dbo.tblVoucher.CMFADocId, 0) AS CMFADocId, SUM(ISNULL(dbo.tblVoucherDetail.debit_amount, 0)) AS VExpense " _
                     & " FROM dbo.tblVoucherDetail INNER JOIN " _
                     & " dbo.tblVoucher ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id " _
                     & " WHERE(ISNULL(dbo.tblVoucher.CMFADocId, 0) = " & DocId & ") " _
                     & " GROUP BY ISNULL(dbo.tblVoucher.CMFADocId, 0)) " _
                     & " AS VExp ON VExp.CMFADocId = CMFA.DocId " _
                     & " WHERE(ISNULL(CMFA.DocId, 0) = " & DocId & ") "
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)

            If dt IsNot Nothing Then
                If dt.Rows.Count > 0 Then
                    Return Val(dt.Rows(0).Item(1).ToString)
                Else
                    Return 0D
                End If
            Else
                Return 0D
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetAcutalExpenseDetail(ByVal DocId As Integer) As DataTable
        Try
            Dim strSQL As String = String.Empty
            DocId = IIf(DocId = 0, -1, DocId)
            strSQL = " Select DocId, Description, Amount From (SELECT ISNULL(dbo.tblVoucher.CMFADocId, 0) AS DocId, dbo.vwCOADetail.detail_title as [Description], Round(SUM(IsNull(dbo.tblVoucherDetail.debit_amount,0)),0) AS Amount" _
                    & " FROM dbo.tblVoucher INNER JOIN dbo.tblVoucherDetail ON dbo.tblVoucher.voucher_id = dbo.tblVoucherDetail.voucher_id INNER JOIN dbo.vwCOADetail ON dbo.tblVoucherDetail.coa_detail_id = dbo.vwCOADetail.coa_detail_id " _
                    & " WHERE (dbo.vwCOADetail.account_type = 'Expense') AND (ISNULL(dbo.tblVoucher.CMFADocId, 0) = " & DocId & ")  GROUP BY ISNULL(dbo.tblVoucher.CMFADocId, 0), dbo.vwCOADetail.detail_title  " _
                    & " UNION ALL " _
                    & " SELECT ISNULL(dbo.ReceivingMasterTable.RefCMFADocId, 0) AS DocId, dbo.ReceivingMasterTable.ReceivingNo as [Description], Round(SUM(ISNULL(dbo.ReceivingDetailTable.Qty, 0)  " _
                    & " * ISNULL(dbo.ReceivingDetailTable.Price, 0)),0) AS Amount " _
                    & " FROM dbo.ReceivingMasterTable INNER JOIN " _
                    & " dbo.ReceivingDetailTable ON dbo.ReceivingMasterTable.ReceivingId = dbo.ReceivingDetailTable.ReceivingId " _
                    & " WHERE(ISNULL(dbo.ReceivingMasterTable.RefCMFADocId, 0) = " & DocId & ") " _
                    & " GROUP BY ISNULL(dbo.ReceivingMasterTable.RefCMFADocId, 0), dbo.ReceivingMasterTable.ReceivingNo) a WHERE a.docId=" & DocId & ""
            Dim dt As New DataTable
            dt = GetDataTable(strSQL)
            dt.AcceptChanges()
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    'End Task:2734
    Private Sub grdSaved_LoadingRow(ByVal sender As Object, ByVal e As Janus.Windows.GridEX.RowLoadEventArgs) Handles grdSaved.LoadingRow
        Try


            If e.Row.Cells("Approved").Value = True Then
                Dim MyGrdFormatStyle As Janus.Windows.GridEX.GridEXFormatStyle
                MyGrdFormatStyle = New Janus.Windows.GridEX.GridEXFormatStyle
                MyGrdFormatStyle.BackColor = Color.Cyan
                e.Row.RowStyle = MyGrdFormatStyle
            ElseIf e.Row.Cells("ReturnStatus").Value = True Then
                Dim MyGrdFormatStyle1 As Janus.Windows.GridEX.GridEXFormatStyle
                MyGrdFormatStyle1 = New Janus.Windows.GridEX.GridEXFormatStyle
                MyGrdFormatStyle1.BackColor = Color.HotPink
                e.Row.RowStyle = MyGrdFormatStyle1
            ElseIf e.Row.Cells("CheckedStatus").Value = True Then
                Dim MyGrdFormatStyle3 As Janus.Windows.GridEX.GridEXFormatStyle
                MyGrdFormatStyle3 = New Janus.Windows.GridEX.GridEXFormatStyle
                MyGrdFormatStyle3.BackColor = Color.Azure
                e.Row.RowStyle = MyGrdFormatStyle3
            Else
                Dim MyGrdFormatStyle2 As Janus.Windows.GridEX.GridEXFormatStyle
                MyGrdFormatStyle2 = New Janus.Windows.GridEX.GridEXFormatStyle
                MyGrdFormatStyle2.BackColor = Color.White
                e.Row.RowStyle = MyGrdFormatStyle2
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
    Private Sub btnSaveCheckedByManager_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            btnSave_Click(Nothing, Nothing)
            SaveRecordStatus = True
            btnApprovedByManager_Click(Nothing, Nothing)
            SaveRecordStatus = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub btnSaveApprovedByDirector_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            btnSave_Click(Nothing, Nothing)
            SaveRecordStatus = True
            btnApprovedByDirector_Click(Nothing, Nothing)
            SaveRecordStatus = False
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub tsbTask_Click(sender As Object, e As EventArgs) Handles tsbTask.Click
        Try
            If Not grdSaved.GetRow Is Nothing AndAlso grdSaved.GetRow.RowType = Janus.Windows.GridEX.RowType.Record Then
                Dim Lcontrol As String = String.Empty
                Dim control As String = String.Empty
                'Dim VNo = v
                Lcontrol = frmModProperty.fname.Name
                control = "frmCmfa"
                'frmMain.LoadControl("Tasks")
                Dim frmtask As New frmTasks
                frmtask.Ref_No = grdSaved.CurrentRow.Cells("docNo").Value.ToString
                frmtask.ReferenceForm = control
                'frmtask.GetReferenceTasks(frmtask.Ref_No)
                'tsbAssign.Text = frmtask.CountReferenceTasks(frmtask.Ref_No).ToString()
                frmtask.StartPosition = FormStartPosition.CenterScreen
                frmtask.Text = "CMFA Document (" & frmtask.Ref_No & ") "
                frmtask.Width = 950
                frmtask.ShowDialog()
                frmtask.UltraTabControl1.SelectedTab = frmTasks.UltraTabControl1.Tabs(1).TabPage.Tab
                'frmtask.ReSetControls()
            End If
        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub

    Private Sub tsbConfig_Click(sender As Object, e As EventArgs) Handles tsbConfig.Click
        Try
            If Not frmMain.Panel2.Controls.Contains(frmSystemConfigurationNew) Then
                frmMain.LoadControl("frmSystemConfiguration")
            End If
            frmSystemConfigurationNew.ScreenName = frmSystemConfigurationNew.enmScreen.Accounts
            frmMain.LoadControl("frmSystemConfiguration")
            frmSystemConfigurationNew.SelectTab()

        Catch ex As Exception
            ShowErrorMessage(ex.Message)
        End Try
    End Sub
End Class